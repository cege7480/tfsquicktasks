using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChrisTaylor.TfsQuickTasks.WorkItems;

namespace ChrisTaylor.TfsQuickTasks.WPF
{
    public class WorkItemQueryTemplateSelector : DataTemplateSelector
    {
        public HierarchicalDataTemplate WorkItemQueryFolderTemplate
        {
            get;
            set;
        }

        public HierarchicalDataTemplate WorkItemQueryTemplate
        {
            get;
            set;
        }

        public DataTemplate WorkItemTemplate
        {
            get;
            set;
        }

        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            base.SelectTemplate(item, container);

            if (item is TfsWorkItemQueryFolder)
                return WorkItemQueryFolderTemplate;
            else if (item is TfsWorkItemQuery)
                return WorkItemQueryTemplate;
            else if (item is TfsWorkItem)
                return WorkItemTemplate;

            return null;

        }
    }
}
