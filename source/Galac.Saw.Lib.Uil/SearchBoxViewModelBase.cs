using LibGalac.Aos.UI.Mvvm.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.Base;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Lib.Uil {
    public abstract class SearchBoxViewModelBase : ObservableObject, ISearchBoxViewModel {

        #region Constantes
        private const string FilterPropertyName = "Filter";
        private const string CurrentPagePropertyName = "CurrentPage";
        private const string ListVisibilityPropertyName = "ListVisibility";
        private const string IsControlVisiblePropertyName = "IsControlVisible";
        private const string SelectedItemPropertyName = "SelectedItem";
        private const string ItemsListPropertyName = "ItemsList";
        private const string IsEnabledPropertyName = "IsEnabled";
        #endregion

        #region Variables
        private int _CurrentPage;
        private int _ConsecutivoCompania;
        private bool _ListVisibility;
        private bool _IsControlVisible;
        private string _ViewModelName;
        private string _CodigoORif;
        private string _NombreODescripcion;
        private ObservableCollection<LookupItem> _ItemsList;

        protected string _filter;
        protected LookupItem _SelectedItem;
        protected LookupItem _LastSelectedItem;
        private bool _IsEnabled;
        #endregion

        #region Propiedades
        public string Filter {
            get { return _filter; }
            set {
                if (_filter != value) {
                    if (_filter == null) _filter = string.Empty;
                    CurrentPage = 1;
                    _filter = value;
                    RaisePropertyChanged(FilterPropertyName);
                    LoadListAsync();
                    if (SelectedItem != null && SelectedItem.Description != value && !ListVisibility ||
                        SelectedItem == null && !ListVisibility)
                        ShowList();
                }
            }
        }

        public int CurrentPage {
            get { return _CurrentPage; }
            set {
                if (_CurrentPage != value) {
                    _CurrentPage = value;
                    RaisePropertyChanged(CurrentPagePropertyName);
                }
            }
        }

        public bool IsEnabled {
            get { return _IsEnabled; }
            set {
                if (_IsEnabled!=value) {
                    _IsEnabled = value;
                    RaisePropertyChanged(IsEnabledPropertyName);
                }
            }
        }

        public bool ListVisibility {
            get { return _ListVisibility; }
            set {
                if (_ListVisibility != value) {
                    _ListVisibility = value;
                    RaisePropertyChanged(ListVisibilityPropertyName);
                }
            }
        }

        public bool IsControlVisible {
            get { return _IsControlVisible; }
            set {
                if (_IsControlVisible != value) {
                    _IsControlVisible = value;
                    RaisePropertyChanged(IsControlVisiblePropertyName);
                }
            }

        }

        public LookupItem SelectedItem {
            get { return _SelectedItem; }
            set {
                if (_SelectedItem != value) {
                    if (value != null) {
                        _LastSelectedItem = value;
                    }
                    _SelectedItem = value;
                    RaisePropertyChanged(SelectedItemPropertyName);
                }
            }
        }

        public ObservableCollection<LookupItem> ItemsList {
            get { return _ItemsList; }
            set {
                _ItemsList = value;
                RaisePropertyChanged(ItemsListPropertyName);
            }
        }

        #region Commands
        public ICommand ShowListCommand { get; set; }
        public ICommand HideListCommand { get; set; }
        public ICommand SelectItemFromListCommand { get; set; }
        public ICommand SelectItemFromTextboxCommand { get; set; }
        #endregion

        public ILookupDataService DataService { get; set; }
        public Action<LookupItem> ItemSelected { get; set; }
        #endregion

        #region Constructor
        public SearchBoxViewModelBase(string viewModelName, eTypeOfSearchInDb typeOfSearchInDb) {
            CurrentPage = 1;
            IsEnabled = true;
            ListVisibility = false;
            ItemsList = new ObservableCollection<LookupItem>();
            this._ViewModelName = viewModelName;
            InitializeCommands();
            _ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            switch (typeOfSearchInDb) {
                case eTypeOfSearchInDb.Codigo_Descripcion:
                    _CodigoORif = "Codigo";
                    _NombreODescripcion = "Descripcion";
                    break;
                case eTypeOfSearchInDb.RIF_Nombre:
                    _CodigoORif = "NumeroRIF";
                    _NombreODescripcion = "Nombre";
                    break;
            }
        }
        #endregion

        public void SelectItem() {
            SelectItem(false);
        }

        public void SelectItem(bool valDoubleClick) {
            if (!valDoubleClick && (LibString.IsNullOrEmpty(Filter) ||
                ItemsList != null && ItemsList.Count > 1 && SelectedItem == null ||
                LibString.IsNullOrEmpty(Filter) && SelectedItem == null && _LastSelectedItem == null)) {
                return;
            }
            if (ItemsList!=null && ItemsList.Count == 1) {
                SelectedItem = ItemsList[0];
                Filter = SelectedItem.Description;
                HideList();
            }
            ItemSelected.Invoke(
                (SelectedItem == null)
                    ? (!LibString.IsNullOrEmpty(Filter) && ItemsList == null)
                        ? new LookupItem() { Description = Filter }
                        : _LastSelectedItem
                    : SelectedItem);
            if (!LibString.IsNullOrEmpty(Filter)) {
                NotifyFocusAndSelect();
            }
        }

        public virtual void NotifyFocusAndSelect(string newViewModelName=null) {
            LibMessages.Notification.Send<string>(newViewModelName == null ? _ViewModelName : newViewModelName, "Focus");
        }

        public void LoadPreviousPage() {
            CurrentPage--;
            LoadListAsync();
        }

        public bool LoadNextPage() {
            return LoadListAsync(true).Result;
        }

        public void SelectItem(LookupItem selectedItem) {
            if (selectedItem == null && SelectedItem != null) {
                SelectItem(true);
            }
            else if (selectedItem == null) return;
            HideList();
            Filter = SelectedItem.Description;
        }

        public Task<bool> LoadListAsync(bool valLoadNextPage = false) {
            try {
                Task<XElement> vTask = Task<XElement>.Factory.StartNew(() => {
                    var vArticulosPorCodigo = DataService.GetDataPageByCode(Filter, _ConsecutivoCompania,
                        (valLoadNextPage) ? CurrentPage + 1 : CurrentPage);
                    return (vArticulosPorCodigo == null)
                            ? DataService.GetDataPageByDescription(Filter, _ConsecutivoCompania, CurrentPage)
                            : vArticulosPorCodigo;
                });
                Task<bool> vTaskResult = vTask.ContinueWith<bool>((t) => {
                    if (valLoadNextPage && t.Result != null) {
                        CurrentPage++;
                        LoadListFromXMLAsync(t.Result);
                        return true;
                    } else if (!valLoadNextPage) {
                        LoadListFromXMLAsync(t.Result);
                    }
                    return false;
                });
                return vTaskResult;
            } catch (Exception ex) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.MessageBox.Programmer(this, ex.Message);
                return new Task<bool>(() => { return false; });
            }
        }

        private void LoadListFromXMLAsync(XElement valDatosXML) {
            if (valDatosXML == null) {
                ItemsList = null;
                return;
            }
            var vTask = Task<List<LookupItem>>.Factory.StartNew(() => {
                var Codigos = valDatosXML.Descendants().Where(y => y.Name == _CodigoORif).Select((t) => t.Value).ToList();
                var Descripciones = valDatosXML.Descendants().Where(y => y.Name == _NombreODescripcion).Select((t) => t.Value).ToList();

                List<LookupItem> articulos = new List<LookupItem>();
                for (int i = 0; i < Codigos.Count; i++) {
                    articulos.Add(new LookupItem() { Code = Codigos[i], Description = Descripciones[i] });
                }
                return articulos;
            });
            vTask.ContinueWith((t) => {
                ItemsList = t.Result.ToObservableCollection();
            });
        }

        private void InitializeCommands() {
            ShowListCommand = new RelayCommand(ShowList);
            HideListCommand = new RelayCommand(HideList);
            SelectItemFromListCommand = new RelayCommand<LookupItem>(SelectItem);
            SelectItemFromTextboxCommand = new RelayCommand(SelectItem);
        }

        protected void ShowList() {
            if (LibString.IsNullOrEmpty(Filter) && ItemsList==null || LibString.IsNullOrEmpty(Filter) && ItemsList.Count == 0) {
                LoadListAsync();
            }
            ListVisibility = true;
        }

        protected void HideList() {
            ListVisibility = false;
        }
    }
}
