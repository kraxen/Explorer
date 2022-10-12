using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Explorer.Shared.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string mainDiskName;

        public MainViewModel()
        {
            MainDiskName = Environment.SystemDirectory;
        }

        public string MainDiskName { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}