using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ChrisTaylor.TfsQuickTasks.ViewModel;

namespace ChrisTaylor.TfsQuickTasks.WPF
{
    public class GetTeamProjectQueriesCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _viewModel.SampleString = "Test From the string";
        }

        private QuickTaskExplorerControlViewModel _viewModel;

        public GetTeamProjectQueriesCommand(QuickTaskExplorerControlViewModel viewModel)
        {
            _viewModel = viewModel;
        }
      
    }

    
    
    
    public class SimpleDelegateCommand : ICommand
    {
        Action<object> _executeDelegate;

        public SimpleDelegateCommand(Action<object> executeDelegate)
        {
            _executeDelegate = executeDelegate;
        }

        public void Execute(object parameter)
        {
            _executeDelegate(parameter);
        }

        public bool CanExecute(object parameter) { return true; }
        public event EventHandler CanExecuteChanged;
    }
 
}
