using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VisualRepresentation.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _foundCsFiles = new List<string>();
        }

        private List<string> _foundCsFiles;
        public List<string> FoundCsFiles
        {
            get => _foundCsFiles;
            set
            {
                _foundCsFiles = value;
                OnPropertyChanged("FoundCsFiles");
            }
        }



        private string _pathToFolder = "Choose folder with files to graph.";
        public string PathToFolder
        {
            get
            {
                return _pathToFolder;
            }
            set
            {
                _pathToFolder = value;
                OnPropertyChanged("PathToFolder");
            }
        }

        private bool _story1 = false;
        public bool Story1Checkbox
        {
            get => _story1;
            set
            {
                _story1 = value;
                OnPropertyChanged("Story1");
            }
        }

        private bool _story2 = false;
        public bool Story2Checkbox
        {
            get => _story2;
            set
            {
                _story2 = value;
                OnPropertyChanged("Story2");
            }
        }

        private bool _story3 = false;
        public bool Story3Checkbox
        {
            get => _story3;
            set
            {
                _story3 = value;
                OnPropertyChanged("Story3");
            }
        }



        #region Property changed interaction
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
