using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InstallerTestProject
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        Action _action;
        public Command(Action action)
        {
            _action = action;
        }
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _action();
        }
    }
    public class VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        string _input;
        public string Input{
            get => _input;
            set {
                _input = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Input)));
            }
        }
        public VM()
        {
            Input = Settings1.Default.inputText;
            
        }
        public Command Click
        {
            get {
                return new Command(() => {
                    Settings1.Default.inputText = Input;
                    Settings1.Default.Save();
                });
            }
        }
    }
}
