using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class VerificadorDePreciosViewModel : LibInputViewModelMfc<VerificadorDePreciosStt> {
        #region Constantes
        private const string DuracionEnPantallaPropertyName = "DuracionEnPantalla";
        private const string NivelDePrecioAMostrarPropertyName = "NivelDePrecioAMostrar";
        private const string TipoDeBusquedaArticuloPropertyName = "TipoDeBusquedaArticulo";
        private const string TipoDePrecioAMostrarEnVerificadorPropertyName = "TipoDePrecioAMostrarEnVerificador";
        private const string IsEnabledTipoDeConversionParaPreciosPropertyName = "IsEnabledTipoDeConversionParaPrecios";
        private const string UsaMostrarPreciosEnDivisaPropertyName = "UsaMostrarPreciosEnDivisa";
        private const string TipoDeConversionParaPreciosPropertyName = "TipoDeConversionParaPrecios";
        private const string UsaMonedaExtranjeraPropertyName = "UsaMonedaExtranjera";
        private const string IsEnabledUsaMonedaExtranjeraPropertyName = "IsEnabledUsaMonedaExtranjera";
        #endregion

        #region Propiedades
        public override string ModuleName {
            get { return "5.4.- Verificador de Precios"; }
        }

        public int DuracionEnPantalla {
            get { return Model.DuracionEnPantallaEnSegundos; }
            set {
                if (Model.DuracionEnPantallaEnSegundos!=value) {
                    Model.DuracionEnPantallaEnSegundos = value;
                    RaisePropertyChanged(DuracionEnPantallaPropertyName);
                }
            }
        }

        public eNivelDePrecio NivelDePrecioAMostrar {
            get { return Model.NivelDePrecioAMostrarAsEnum; }
            set {
                if (Model.NivelDePrecioAMostrarAsEnum!=value) {
                    Model.NivelDePrecioAMostrarAsEnum = value;
                    RaisePropertyChanged(NivelDePrecioAMostrarPropertyName);
                }
            }
        }

        public eTipoDeBusquedaArticulo TipoDeBusquedaArticulo {
            get { return Model.TipoDeBusquedaArticuloAsEnum; }
            set {
                if (Model.TipoDeBusquedaArticuloAsEnum != value) {
                    Model.TipoDeBusquedaArticuloAsEnum = value;
                    RaisePropertyChanged(TipoDeBusquedaArticuloPropertyName);
                }
            }
        }

        public eTipoDePrecioAMostrarEnVerificador TipoDePrecioAMostrarEnVerificador {
            get { return Model.TipoDePrecioAMostrarEnVerificadorAsEnum; }
            set {
                if (Model.TipoDePrecioAMostrarEnVerificadorAsEnum!=value) {
                    Model.TipoDePrecioAMostrarEnVerificadorAsEnum = value;
                    RaisePropertyChanged(TipoDePrecioAMostrarEnVerificadorPropertyName);
                }
            }
        }

        public string RutaImagen {
            get { return Model.RutaImagen; }
            set {
                if (Model.RutaImagen!=value) {
                    Model.RutaImagen = value;
                    RaisePropertyChanged("RutaImagen");
                }
            }
        }

        public ObservableCollection<eNivelDePrecio> NivelesDePrecios { get;  set; }
           
        public ObservableCollection<eTipoDePrecioAMostrarEnVerificador> TiposDePrecios { get; set; }

        public ObservableCollection<eTipoDeBusquedaArticulo> TiposDeBusquedaArticulo { get; set; }

        public RelayCommand<string> BuscarLogoCommand { get; set; }

        public bool UsaMostrarPreciosEnDivisa {
            get {
                return Model.UsaMostrarPreciosEnDivisaAsBool;
            }
            set {
                if (Model.UsaMostrarPreciosEnDivisaAsBool != value) {
                    Model.UsaMostrarPreciosEnDivisaAsBool = value;
                    if (value) {
                        TipoDeConversionDePreciosAMostrar = eTipoDeConversionParaPrecios.MonedaLocalADivisa;
                    }
                    RaisePropertyChanged(UsaMostrarPreciosEnDivisaPropertyName);
                    RaisePropertyChanged(IsEnabledTipoDeConversionParaPreciosPropertyName);
                }
            }
        }

        public bool IsEnabledTipoDeConversionParaPrecios {
            get {
                return IsEnabled && UsaMostrarPreciosEnDivisa;
            }

        }

        public ObservableCollection<eTipoDeConversionParaPrecios> TipoDeConversionPrecios { get; set; }

        public eTipoDeConversionParaPrecios TipoDeConversionDePreciosAMostrar {
            get { return Model.TipoDeConversionParaPreciosAsEnum; }
            set {
                if (Model.TipoDeConversionParaPreciosAsEnum != value) {
                    Model.TipoDeConversionParaPreciosAsEnum = value;
                    RaisePropertyChanged(TipoDeConversionParaPreciosPropertyName);
                }
            }
        }
        private bool _IsEnabledUsaMonedaExtranjera;

        public bool IsEnabledUsaMonedaExtranjera {
            get { return _IsEnabledUsaMonedaExtranjera; }
            set {
                if (_IsEnabledUsaMonedaExtranjera != value) {
                    _IsEnabledUsaMonedaExtranjera = value;
                    RaisePropertyChanged(IsEnabledUsaMonedaExtranjeraPropertyName);
                }
            }
        }

        #endregion //Propiedades

        #region Constructores
        public VerificadorDePreciosViewModel()
            : this(new VerificadorDePreciosStt(), eAccionSR.Insertar) {
            NivelesDePrecios = new ObservableCollection<eNivelDePrecio>();
            TiposDePrecios = new ObservableCollection<eTipoDePrecioAMostrarEnVerificador>();
            TiposDeBusquedaArticulo = new ObservableCollection<eTipoDeBusquedaArticulo>();
            TipoDeConversionPrecios = new ObservableCollection<eTipoDeConversionParaPrecios>();
        }

        public VerificadorDePreciosViewModel(VerificadorDePreciosStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            NivelesDePrecios = new ObservableCollection<eNivelDePrecio>();
            TiposDePrecios = new ObservableCollection<eTipoDePrecioAMostrarEnVerificador>();
            TiposDeBusquedaArticulo = new ObservableCollection<eTipoDeBusquedaArticulo>();
            TipoDeConversionPrecios = new ObservableCollection<eTipoDeConversionParaPrecios>();
            LibMessages.Notification.Register<bool>(this, OnUsaMonedaExtranjeraChanged);
        }
        #endregion //Constructores

        #region Metodos Generados

        protected override void InitializeCommands() {
            base.InitializeCommands();
            BuscarLogoCommand = new RelayCommand<string>(ExecuteBuscarLogo);
        }

        protected override void InitializeLookAndFeel(VerificadorDePreciosStt valModel) {
            base.InitializeLookAndFeel(valModel);
            CargarNivelesDePrecios();
            CargarTiposDePrecios();
            CargarTipoDeBusquedaDeArticulos();
            CargarTipoDeConversionParaPrecios();
            ActivarUsaMostrarPrecioEnDivisa();
        }

        private void ExecuteBuscarLogo(string ruta) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Imagenes (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            bool vSeleccionoImagen = (bool)openFileDialog.ShowDialog();
            if (vSeleccionoImagen) {
                RutaImagen = openFileDialog.FileName;
            } else {
                RutaImagen = string.Empty;
            }
            
        }

        private void CargarTipoDeBusquedaDeArticulos() {
            TiposDeBusquedaArticulo.Clear();
            foreach (var tipoDeBusqueda in (IEnumerable<eTipoDeBusquedaArticulo>)LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeBusquedaArticulo))) {
                TiposDeBusquedaArticulo.Add(tipoDeBusqueda);
            }
        }

        private void CargarTiposDePrecios() {
            TiposDePrecios.Clear();
            foreach (var tipoDePrecio in (IEnumerable<eTipoDePrecioAMostrarEnVerificador>)LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDePrecioAMostrarEnVerificador))) {
                TiposDePrecios.Add(tipoDePrecio);
            }
        }

        private void CargarNivelesDePrecios() {
            NivelesDePrecios.Clear();
            foreach (var nivelDePrecio in (IEnumerable<eNivelDePrecio>)LibEnumHelper.GetValuesInEnumeration(typeof(eNivelDePrecio))) {
                if (nivelDePrecio != eNivelDePrecio.Todos) {
                    NivelesDePrecios.Add(nivelDePrecio);
                }
            }
        }

        protected override VerificadorDePreciosStt FindCurrentRecord(VerificadorDePreciosStt valModel) {
            if (valModel == null) {
                return null;

            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<VerificadorDePreciosStt>, IList<VerificadorDePreciosStt>> GetBusinessComponent() {
            return null;
        }
        
        private void CargarTipoDeConversionParaPrecios() {
            TipoDeConversionPrecios.Clear();
            foreach (var vtipoDeConversionPrecios in (IEnumerable<eTipoDeConversionParaPrecios>)LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDeConversionParaPrecios))) {
                TipoDeConversionPrecios.Add(vtipoDeConversionPrecios);
            }
        }

        public eTipoDeConversionParaPrecios TipoDeConversionParaPrecios {
            get { return Model.TipoDeConversionParaPreciosAsEnum; }
            set {
                if (Model.TipoDeConversionParaPreciosAsEnum != value) {
                    Model.TipoDeConversionParaPreciosAsEnum = value;
                    RaisePropertyChanged(TipoDeConversionParaPreciosPropertyName);
                }
            }
        }

        private void OnUsaMonedaExtranjeraChanged(NotificationMessage<bool> valMessage) {
            if (LibString.S1IsEqualToS2(valMessage.Notification, "UsaMonedaExtranjera")) {
                IsEnabledUsaMonedaExtranjera = valMessage.Content || LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
				if (!IsEnabledUsaMonedaExtranjera) {
                	UsaMostrarPreciosEnDivisa = false;
            	}
            }
        }

        private void ActivarUsaMostrarPrecioEnDivisa() {
            IsEnabledUsaMonedaExtranjera = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
            if (!IsEnabledUsaMonedaExtranjera) {
                UsaMostrarPreciosEnDivisa = false;
            }
        }

        #endregion //Metodos Generados
    } //End of class VerificadorDePreciosViewModel

}
