using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChrisTaylor.TfsQuickTasks.WorkItems
{
    [Serializable]
    public abstract class TfsQueryItemBase : DependencyObject
    {
        public abstract TfsQueryItemType ItemType { get; }

    }

    [Serializable]
    public enum TfsQueryItemType
    {
        Folder,
        Query,
        WorkItem

    }
}
