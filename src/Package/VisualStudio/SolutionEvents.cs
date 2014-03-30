using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TeamFoundation.VersionControl;

namespace ChrisTaylor.TfsQuickTasks.VisualStudio
{
    class SolutionEvents : IVsSolutionEvents, IDisposable
    {
        private IVsSolution _solution;
        private uint _solutionEventsCookie;
        public event Action AfterSolutionLoaded;
        public event Action BeforeSolutionClosed;
        public event Action AfterSolutionClosed;

        public SolutionEvents(IServiceProvider serviceProvider)
        {
            AfterSolutionLoaded += () => { };
            BeforeSolutionClosed += () => { };

            _solution = serviceProvider.GetService(typeof(SVsSolution)) as IVsSolution;

            if (_solution != null)
                _solution.AdviseSolutionEvents(this, out _solutionEventsCookie);

        }

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            if (AfterSolutionClosed != null)
                AfterSolutionClosed();

            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            if (AfterSolutionLoaded != null)
                AfterSolutionLoaded();

            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            if (BeforeSolutionClosed != null)
                BeforeSolutionClosed();

            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }



        public void Dispose()
        {
            if (_solution != null && _solutionEventsCookie != 0)
            {
                GC.SuppressFinalize(this);
                _solution.UnadviseSolutionEvents(_solutionEventsCookie);
                _solutionEventsCookie = 0;
                _solution = null;
                AfterSolutionLoaded = null;
                BeforeSolutionClosed = null;
                AfterSolutionClosed = null;

            }
        }
    }
}

