using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        public string Direct {
            get => dir;
            set
            {
                dir = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Direct)));
            }
        }
        public string Input{
            get => _input;
            set {
                _input = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Input)));
            }
        }
        string dir;
        public VM()
        {
            Direct = Directory.GetCurrentDirectory();
            if (!File.Exists(dir + "//1.txt"))
            {
                Write("default");
                Input = "default";
            }
            else {
                using (FileStream file = File.OpenRead(dir + "//1.txt"))
                {
                    using (StreamReader writer = new StreamReader(file))
                    {
                        Input = writer.ReadLine();
                    }
                }
            }
            
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
        private void Write(string input) {
            using (FileStream file = File.OpenWrite(dir + "//1.txt")) {
                using (StreamWriter writer = new StreamWriter(file)) {
                    writer.WriteLine(input);
                    writer.Flush();
                }
            }
        }
    }
}
