using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisTaylor.TfsQuickTasks.WorkItems
{
    public class TfsWorkItemQueryFolder : TfsQueryItemBase
    {
         public ObservableCollection<TfsWorkItemQueryFolder> Folders { get; set; }
        public ObservableCollection<TfsWorkItemQuery> Queries { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ItemPath { get; set; }
        public int NestLevel { get; set; }
        public bool IsEnabled
        {
            get { return false; }           
        }


        public TfsWorkItemQueryFolder()
        {
            Folders = new ObservableCollection<TfsWorkItemQueryFolder>();
            Queries = new ObservableCollection<TfsWorkItemQuery>();
        }

        ObservableCollection<object> _allFoldersAndQueries = null;
        public ObservableCollection<object> AllFoldersAndQueries
        {
            get
            {
//                if (_allFoldersAndQueries == null)
  //              {
                    _allFoldersAndQueries = new ObservableCollection<object>();
                    Folders.ToList().ForEach(folder =>
                    {
                        _allFoldersAndQueries.Add(folder);
                    });

                    Queries.ToList().ForEach(query =>
                    {
                        _allFoldersAndQueries.Add(query);
                    });
    //            }
                return _allFoldersAndQueries;

            }
        }
        public override string ToString()
        {
            return ItemPath;
        }



        public override TfsQueryItemType ItemType
        {
            get { return TfsQueryItemType.Folder; }
        }
    }
    }
