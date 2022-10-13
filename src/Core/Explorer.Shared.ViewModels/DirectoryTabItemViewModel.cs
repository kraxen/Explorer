using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Explorer.Shared.ViewModels
{
    public class DirectoryTabItemViewModel : BaseViewModel
    {
        #region public Properties
        public string FilePath { get; set; }
        public string Name { get; set; }
        public FileEntityViewModel SelectedFileEntity { get; set; }
        public ObservableCollection<FileEntityViewModel> DirectoriesAndFiles { get; set; } = new();
        #endregion
        
        #region Commands
        public ICommand OpenCommand { get; }
        public ICommand CloseCommand { get; }
        #endregion

        #region Events
        public event EventHandler OnClosed;
        #endregion

        #region Constructor
        public DirectoryTabItemViewModel()
        {
            Name = "Мой компьютер";
            OpenCommand = new DelegateCommand(Open);
            CloseCommand = new DelegateCommand(Close);
            foreach (var logicalDrive in Directory.GetLogicalDrives())
            {
                DirectoriesAndFiles.Add(new DirectoryViewModel(logicalDrive));
            }
        }

        private void Close(object obj)
        {
            OnClosed.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region private Methods
        private void Open(object parametr)
        {
            if (parametr is DirectoryViewModel directoryViewModel)
            {
                FilePath = directoryViewModel.FullName;
                Name = directoryViewModel.Name;

                DirectoriesAndFiles.Clear();

                var directoryInfo = new DirectoryInfo(FilePath);

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    DirectoriesAndFiles.Add(new DirectoryViewModel(directory));
                }
                foreach (var fileInfo in directoryInfo.GetFiles())
                {
                    DirectoriesAndFiles.Add(new FileViewModel(fileInfo));
                }
            }
        }
        #endregion
    }
}