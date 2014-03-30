using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ChrisTaylor.TfsQuickTasks.Collections;

namespace ChrisTaylor.TfsQuickTasks.WorkItems
{
    public class TfsWorkItem:TfsQueryItemBase
    {
          public TfsWorkItem()
        {
            this.ChildWorkItems = new ObservableWorkItemCollection();
        }

        #region Public Properties

        public string Title
        {
            get;
            set;
        }
        public int WorkItemId
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string ScopeItemTitle
        {
            get;
            set;
        }
        public string FunctionPointTitle
        {
            get;
            set;
        }
        public Dictionary<string, string> Properties
        {
            get;
            set;
        }
        public string WorkItemType
        {
            get;
            set;
        }
        public string SolutionDesigner
        {
            get;
            set;
        }
        public string PrimaryEngineer
        {
            get;
            set;
        }
        public string BusinessSystemsAnalyst
        {
            get;
            set;
        }
        public string QualityAssuranceAnalyst
        {
            get;
            set;
        }
        public string UATAnalyst
        {
            get;
            set;
        }

        public int ClosestCharacterPosition
        {
            get;
            set;
        }

        public string ItemPath
        {
            get;
            set;
        }
        #endregion

        public TfsWorkItem ParentWorkItem
        {
            get;
            set;
        }

        public ObservableWorkItemCollection ChildWorkItems
        {
            get;
            set;
        }
        
        public override TfsQueryItemType ItemType
        {
            get { return TfsQueryItemType.WorkItem; }
        }

        public static DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(TfsWorkItem), new PropertyMetadata(false));

        public static DependencyProperty ControlBrushProperty =
            DependencyProperty.Register("ControlBrush", typeof(Brush), typeof(TfsWorkItem), new PropertyMetadata(Brushes.Black));

        public static DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register("BackgroundBrush", typeof(Brush), typeof(TfsWorkItem), new PropertyMetadata(Brushes.LightGreen));

        public static DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(TfsWorkItem), new PropertyMetadata(Colors.LightGreen));


        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);

            }

        }
        public Brush ControlBrush
        {
            get
            {
                return (Brush)GetValue(ControlBrushProperty);
            }
            set
            {
                SetValue(ControlBrushProperty, value);
            }
        }
        public Brush BackgroundBrush
        {
            get
            {
                return (Brush)GetValue(BackgroundBrushProperty);
            }
            set
            {
                SetValue(BackgroundBrushProperty, value);
            }
        }
        public Color BackgroundColor
        {
            get
            {
                return (Color)GetValue(BackgroundColorProperty);
            }
            set
            {
                SetValue(BackgroundColorProperty, value);
            }
        }


        public override string ToString()
        {
            return string.Format("{0} [{1}]", Title, this.ItemType);


        }

    }
}
