using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Galac.Saw.Lib.Uil {
    public interface ISearchBoxViewModel {
        void LoadPreviousPage();
        bool LoadNextPage();
        Task<bool> LoadListAsync(bool loadNextPage = false);
        void NotifyFocusAndSelect(string viewModelName=null);
        int CurrentPage { get; set; }
        bool IsControlVisible { get; set; }
        bool ListVisibility { get; set; }
        string Filter { get; set; }
        Action<LookupItem> ItemSelected { get; set; }

        ICommand ShowListCommand { get; set; }
        ICommand HideListCommand { get; set; }
        ICommand SelectItemFromListCommand { get; set; }
        ICommand SelectItemFromTextboxCommand { get; set; }
        ILookupDataService DataService { get; }

        LookupItem SelectedItem { get; set; }
        ObservableCollection<LookupItem> ItemsList { get; set; }
    }
}