using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace Explorer.Shared.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<DirectoryTabItemViewModel> DirectoryTabItems { get; set; } = new();
        public DirectoryTabItemViewModel CurentDirectoryTabItem { get; set; }
        #region Commands
        #endregion
        public MainViewModel()
        {
            DirectoryTabItems.CollectionChanged += OnDirectoryTabItemsAddNewElement;

            DirectoryTabItems.Add(new());
            DirectoryTabItems.Add(new());
            DirectoryTabItems.Add(new());

            CurentDirectoryTabItem = DirectoryTabItems.FirstOrDefault();
        }

        private void OnDirectoryTabItemsAddNewElement(object s, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action is System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is DirectoryTabItemViewModel tabItem)
                    {
                        tabItem.OnClosed += OnTabItemClosed;
                    }
                }
            }
        }

        private void OnTabItemClosed(object sender, System.EventArgs e)
        {
            if(sender is DirectoryTabItemViewModel tabItem)
            {
                tabItem.OnClosed -= OnTabItemClosed;
                DirectoryTabItems.Remove(tabItem);
                if (tabItem == CurentDirectoryTabItem)
                    CurentDirectoryTabItem = DirectoryTabItems.FirstOrDefault();
            }
            
        }
    }
}