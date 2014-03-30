using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using ChrisTaylor.TfsQuickTasks.Services;
using ChrisTaylor.TfsQuickTasks.ToolWindows;
using ChrisTaylor.TfsQuickTasks.VisualStudio;
using ChrisTaylor.TfsQuickTasks.WorkItems;
using Microsoft.TeamFoundation.Server;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.VisualStudio.TeamFoundation;
using Microsoft.VisualStudio.TeamFoundation.VersionControl;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using IServiceProvider = System.IServiceProvider;

namespace ChrisTaylor.TfsQuickTasks
{
   
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(QuickTaskExplorerToolWindow))]
    [ProvideService(typeof(IQuickTaskService))]
    [Guid(GuidList.guidTfsQuickTasksPkgString)]
    public sealed class TfsQuickTasksPackage : Package, IQuickTaskService
    {

        #region Visual Studio Team Foundation Services
        private static TeamFoundationServerExt _teamFoundationsServerExt = null;
        private static VersionControlExt _versionControlExt = null;
        private static EnvDTE80.DTE2 _dte2 = null;

        private static EnvDTE80.DTE2 DTE2
        {
            get
            {
                if (_dte2 == null)
                    _dte2 = ServiceProvider.GlobalProvider.GetService(typeof(SDTE)) as EnvDTE80.DTE2;

                return _dte2;
            }
        }
        public static TeamFoundationServerExt TeamFoundationServerExt
        {
            get
            {
                if (_teamFoundationsServerExt == null && DTE2 != null)
                    _teamFoundationsServerExt = DTE2.GetObject("Microsoft.VisualStudio.TeamFoundation.TeamFoundationServerExt") as TeamFoundationServerExt;
                
                return _teamFoundationsServerExt;
            }
        }
        public static VersionControlExt VersionControlExt
        {
            get
            {
                if (_versionControlExt == null && DTE2 != null)
                    _versionControlExt = DTE2.GetObject("Microsoft.VisualStudio.TeamFoundation.VersionControl.VersionControlExt") as VersionControlExt;

                return _versionControlExt;
            }
        }
        private static TeamProject TeamProject
        {
            get;
            set;
        }
        #endregion



        public TfsQuickTasksPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
          

            
            var solutionEvents = new SolutionEvents(this);
            solutionEvents.AfterSolutionLoaded += SolutionEvents_Opened;
            solutionEvents.BeforeSolutionClosed += () => { };

        }


        void SolutionEvents_Opened()
        {
            // need to find the proper source control item and then map that isntead of hte projectCOntext changed event handler.

            string solutionName = DTE2.Solution.FullName;
            string solutionPath = System.IO.Path.GetDirectoryName(solutionName);

            if (VersionControlExt != null)
            {

                TeamProject = VersionControlExt.SolutionWorkspace.GetTeamProjectForLocalPath(solutionPath);
                // get all the proper users for this project

                // null is valid value, it will cause it ot clear all the queries
              
               

            }



        }
        //private void NotifySubscribers(TeamProject teamProject)
        //{
        //    lock (Synchronized)
        //    {
        //        ProjectChangedNotificationSubscribers.ForEach(subscriber =>
        //        {
        //            subscriber.ProjectContextChanged(this,
        //                new TeamFoundationServerProjectContextChangedEventArgs(teamProject));
        //        });
        //    }
        //}
      

        private void ShowToolWindow(object sender, EventArgs e)
        {
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            ToolWindowPane window = this.FindToolWindow(typeof(QuickTaskExplorerToolWindow), 0, true);
            if ((null == window) || (null == window.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }
            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
           
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the tool window
                CommandID toolwndCommandID = new CommandID(GuidList.guidTfsQuickTasksCmdSet, (int)PkgCmdIDList.cmdidTfsQuickTaskExplorer);
                MenuCommand menuToolWin = new MenuCommand(ShowToolWindow, toolwndCommandID);
                mcs.AddCommand(menuToolWin);
            }
        }
        #endregion


        public event EventHandler<TfsProjectQueryChangedEventArgs> ActiveQueryChanged;
        public event EventHandler<TfsProjectChangedEventArgs> ActiveProjectChanged;

        private WorkItems.TfsWorkItemQuery _activeQuery = null;

        public WorkItems.TfsWorkItemQuery ActiveQuery
        {
            get { return _activeQuery; }
            set { _activeQuery = value; }
        }

        private string _activeProjectName = "";
        private WorkItems.TfsWorkItemQueryFolder _sharedQueryTree = null;
        private WorkItems.TfsWorkItemQueryFolder _myQueryTree = null;
        public void SetActiveQuery(WorkItems.TfsWorkItemQuery newWorkItemQuery)
        {
            ActiveQuery = newWorkItemQuery;
        }

        public void SetActiveProject(string newProjectName)
        {
            _activeProjectName = newProjectName;
        }

        public WorkItems.TfsWorkItemQueryFolder SharedQueryTree
        {
            get { return _sharedQueryTree; }
        }

        public WorkItems.TfsWorkItemQueryFolder MyQueryTree
        {
            get { return _myQueryTree; }
        }
    }
}
