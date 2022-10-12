using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Explorer.Shared.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string FilePath { get; set; }
        public ICommand OpenCommand { get; }

        public FileEntityViewModel SelectedFileEntity { get; set; }
        public ObservableCollection<FileEntityViewModel> DirectoriesAndFiles { get; set; } = new();
        public MainViewModel()
        {
            OpenCommand = new DelegateCommand(Open);
            foreach (var logicalDrive in Directory.GetLogicalDrives())
            {
                DirectoriesAndFiles.Add(new DirectoryViewModel(logicalDrive));
            }
        }
        private void Open(object parametr)
        {
            if(parametr is DirectoryViewModel directoryViewModel)
            {
                FilePath = directoryViewModel.FullName;

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

    }
}