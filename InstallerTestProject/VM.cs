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
        string _version;
        string _update;
        public string Input{
            get => _input;
            set {
                _input = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Input)));
            }
        }
        public string Version
        {
            get => _version;
            set
            {
                _version = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Version)));
            }
        }
        public string Update
        {
            get => _update;
            set
            {
                _update = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Update)));
            }
        }
        public VM()
        {
            Input = Settings1.Default.inputText;
            Version = "";
            Update = "";
            
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
