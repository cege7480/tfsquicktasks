using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ChrisTaylor.TfsQuickTasks.WorkItems;
using System.Windows;

namespace ChrisTaylor.TfsQuickTasks.Services
{

    public interface IQuickTaskService
    {

        event EventHandler<TfsProjectQueryChangedEventArgs> ActiveQueryChanged;
        TfsWorkItemQuery ActiveQuery { get; set; }

        event EventHandler<TfsProjectChangedEventArgs> ActiveProjectChanged;

        void SetActiveQuery(TfsWorkItemQuery newWorkItemQuery);
        void SetActiveProject(string newProjectName);

        TfsWorkItemQueryFolder SharedQueryTree { get; }
        TfsWorkItemQueryFolder MyQueryTree { get; }



    }


    public class TfsProjectChangedEventArgs : EventArgs
    {

    }
    public class TfsProjectQueryChangedEventArgs : EventArgs
    {
        private TfsWorkItemQuery _workItemQuery;

        public TfsWorkItemQuery WorkItemQuery
        {
            get { return _workItemQuery; }
        }

        public TfsProjectQueryChangedEventArgs(TfsWorkItemQuery workItemQuery)
        {
            _workItemQuery = workItemQuery;
        }
    }
}
