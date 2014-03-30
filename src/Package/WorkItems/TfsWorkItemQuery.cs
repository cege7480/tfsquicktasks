using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisTaylor.TfsQuickTasks.WorkItems
{
    public class TfsWorkItemQuery:TfsQueryItemBase
    {
            private bool _isLoaded = false;

        public string Name { get; set; }
        public string ItemPath { get; set; }
        public Guid QueryId { get; set; }
        public TfsWorkItemQueryFolder ParentFolder { get; set; }
        public ObservableCollection<TfsWorkItem> WorkItems { get; set; }
        public bool IsNeedingRefresh { get; set; }

        public bool IsLoaded
        {
            get { return _isLoaded; }
        }

        public int NestLevel { get; set; }

        public TfsWorkItemQuery()
        {
            WorkItems = new ObservableCollection<TfsWorkItem>();
            WorkItems.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(WorkItems_CollectionChanged);
        }

        void WorkItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("AllFoldersAndQueries"));

            _isLoaded = true;
        }

        public override string ToString()
        {
            return ItemPath;
        }



        ObservableCollection<object> _allFoldersAndQueries = null;
        public ObservableCollection<object> AllFoldersAndQueries
        {
            get
            {

                _allFoldersAndQueries = new ObservableCollection<object>();
                _allFoldersAndQueries.AddRange(WorkItems);


                return _allFoldersAndQueries;

            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public override TfsQueryItemType ItemType
        {
            get { return TfsQueryItemType.Query; }
        }
    }
}
