using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChrisTaylor.TfsQuickTasks.WPF;
namespace ChrisTaylor.TfsQuickTasks.ViewModel
{
    public class QuickTaskExplorerControlViewModel : ViewModelBase
    {


        //public event PropertyChangedEventHandler PropertyChanged;

       

        public static readonly DependencyProperty SampleStringProperty = DependencyProperty.Register(
            "SampleString", typeof (string), typeof (QuickTaskExplorerControlViewModel), new PropertyMetadata(default(string)));

        public string SampleString
        {
            get { return (string) GetValue(SampleStringProperty); }
            set { SetValue(SampleStringProperty, value); }
        }

    }
}
