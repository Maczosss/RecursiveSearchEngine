using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualRepresentation.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _pathToFile = "Choose folder with files to graph.";

        public string PathToFile
        {
            get
            {
                return _pathToFile;
            }
            set
            {
                _pathToFile = value;
                OnPropertyChanged("PathToFile");
            }
        }
    }
}
