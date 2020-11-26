using Galac.Comun.Brl.TablasGen;
using Galac.Saw.Ccl.SttDef;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Lib.Uil;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Uil;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class VerificadorDePreciosViewModel : LibGenericViewModel, ILibInputViewModel {

        #region Constantes

        private const string OcultaDatosArticuloPropertyName = "OcultaDatosArticulo";
        private const string MuestraDatosArticuloProperyName = "MuestraDatosArticulo";
        private const string ConexionArticuloPropertyName = "ConexionArticulo";
        private const string MensajeAUsuarioPropertyName = "MensajeAUsuario";
        private const string IsEnabledBusquedaPropertyName = "IsEnabledBusqueda";
        private const string PrecioSinIVADivisaPropertyName = "PrecioSinIVADivisa";
        private const string PrecioConIVADivisaPropertyName = "PrecioConIVADivisa";
        private const string SimboloMonedaDivisaPropertyName = "SimboloMonedaDivisa";
        private const string ImpuestoDivisaPropertyName = "ImpuestoDivisa";
        private const string UsaMostrarPreciosEnDivisaPropertyName = "UsaMostrarPreciosEnDivisa";
        private const string PrecioSinIVAPropertyName = "PrecioSinIVA";
        private const string PrecioConIVAPropertyName = "PrecioConIVA";
        private const string ImpuestoPropertyName = "Impuesto";
        private const string CodigoMonedaDivisaPropertyName = "CodigoMonedaDivisa";
        private const string UsaMonedaExtranjeraPropertyName = "UsaMonedaExtranjera";

        #endregion

        #region Variables Privadas
        private bool _MuestraDatosArticulo;
        private FkArticuloInventarioViewModel _ConexionArticulo;
        private string _MensajeAUsuario;
        private bool _IsEnabledBusqueda;
        private bool _OcultaDatosArticulo;
        private int _TiempoDeEsperaEnSegundos = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "DuracionEnPantallaEnSegundos");
        private eNivelDePrecio _NivelDePrecioAMostrar = (eNivelDePrecio)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "NivelDePrecioAMostrar");
        private eTipoDeBusquedaArticulo _TipoDeBusquedaArticulo = (eTipoDeBusquedaArticulo)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "TipoDeBusquedaArticulo");
        private eTipoDePrecioAMostrarEnVerificador _TipoDePrecioAMostrarEnVerificador = (eTipoDePrecioAMostrarEnVerificador)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "TipoDePrecioAMostrarEnVerificador");
        private decimal _PrecioSinIVADivisa;
        private decimal _PrecioConIVADivisa;
        private string _CodigoMonedaDivisa; 
        private string _SimboloMonedaDivisa;
        private decimal _Tasa;
        private decimal _Impuesto;
        private bool _UsaMostrarPreciosEnDivisa;
        private eTipoDeConversionParaPrecios _TipoDeConversionParaPrecios = (eTipoDeConversionParaPrecios)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "TipoDeConversionParaPrecios");
        private decimal _PrecioSinIVA;
        private decimal _PrecioConIVA;
        private decimal _ImpuestoDivisa;
        private FkMonedaViewModel _ConexionMonedaDivisa = null;
        private bool _UsaMonedaExtranjera;
        #endregion

        #region Propiedades

        public CuadroDeBusquedaDeArticulosVerificadorViewModel CuadroDeBusquedaDeArticulosViewModel { get; set; }
        public string NombreImpuesto { get; set; }
        public bool BusquedaPorCodigo { get; set; }

        public bool IsEnabledBusqueda {
            get { return _IsEnabledBusqueda; }
            set {
                if (_IsEnabledBusqueda != value) {
                    _IsEnabledBusqueda = value;
                    if (CuadroDeBusquedaDeArticulosViewModel != null) {
                        CuadroDeBusquedaDeArticulosViewModel.IsEnabled = value;
                    }
                    RaisePropertyChanged(IsEnabledBusquedaPropertyName);
                }
            }
        }

        public bool MostrarPrecioDesglosado {
            get { return _TipoDePrecioAMostrarEnVerificador == eTipoDePrecioAMostrarEnVerificador.PrecioDesglosado; }
        }

        public FkArticuloInventarioViewModel ConexionArticulo {
            get { return _ConexionArticulo; }
            set {
                if (_ConexionArticulo != value) {
                    _ConexionArticulo = value;
                    RaisePropertyChanged(ConexionArticuloPropertyName);
                }
            }
        }

        public string MensajeAUsuario {
            get { return _MensajeAUsuario; }
            set {
                if (_MensajeAUsuario != value) {
                    _MensajeAUsuario = value;
                    RaisePropertyChanged(MensajeAUsuarioPropertyName);
                }
            }
        }

        public bool OcultaDatosArticulo {
            get { return _OcultaDatosArticulo; }
            set {
                if (_OcultaDatosArticulo != value) {
                    _OcultaDatosArticulo = value;
                    RaisePropertyChanged(OcultaDatosArticuloPropertyName);
                }
            }
        }

        public bool MuestraDatosArticulo {
            get { return _MuestraDatosArticulo; }
            set {
                if (_MuestraDatosArticulo != value) {
                    _MuestraDatosArticulo = value;
                    RaisePropertyChanged(MuestraDatosArticuloProperyName);
                }
            }
        }

        public string LogoEmpresa {
            get { return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "RutaImagen"); }
        }

        public override string ModuleName {
            get { return "Verificador de Precios"; }
        }

        public bool IsDirty { get; set; }

        public eAccionSR Action { get; set; }

        public RelayCommand ExecuteActionCommand { get; set; }

        public RelayCommand<string> ChooseArticuloCommand { get; set; }

        public bool RequestLoginAtClosing { get; set; }

        public string CodigoMoneda { get; set; }

        public decimal PrecioSinIVADivisa {
            get { return _PrecioSinIVADivisa; }
            set {
                if (_PrecioSinIVADivisa != value) {
                    _PrecioSinIVADivisa = value;
                    RaisePropertyChanged(PrecioSinIVADivisaPropertyName);
                }
            }
        }

        public decimal PrecioConIVADivisa {
            get { return _PrecioConIVADivisa; }
            set {
                if (_PrecioConIVADivisa != value) {
                    _PrecioConIVADivisa = value;
                    RaisePropertyChanged(PrecioConIVADivisaPropertyName);
                }
            }
        }

        public FkMonedaViewModel ConexionMonedaDivisa {
            get { return _ConexionMonedaDivisa; }
            set {
                if (_ConexionMonedaDivisa != value) {
                    _ConexionMonedaDivisa = value;
                }
            }
        }

        public string SimboloMonedaDivisa {
            get { return _SimboloMonedaDivisa; }
            set {
                if (_SimboloMonedaDivisa != value) {
                    _SimboloMonedaDivisa = value;
                    RaisePropertyChanged(SimboloMonedaDivisaPropertyName);
                }
            }
        }

        public decimal ImpuestoDivisa {
            get { return _ImpuestoDivisa; }
            set {
                if (_ImpuestoDivisa != value) {
                    _ImpuestoDivisa = value;
                    RaisePropertyChanged(ImpuestoDivisaPropertyName);
                }
            }
        }

        public bool UsaMostrarPreciosEnDivisa {
            get { return _UsaMostrarPreciosEnDivisa; }
            set {
                if (_UsaMostrarPreciosEnDivisa != value) {
                    _UsaMostrarPreciosEnDivisa = value;
                    RaisePropertyChanged(UsaMostrarPreciosEnDivisaPropertyName);
                }
            }
        }

        public decimal PrecioSinIVA {
            get { return _PrecioSinIVA; }
            set {
                if (_PrecioSinIVA != value) {
                    _PrecioSinIVA = value;
                    RaisePropertyChanged(PrecioSinIVAPropertyName);
                }
            }
        }

        public decimal PrecioConIVA {
            get { return _PrecioConIVA; }
            set {
                if (_PrecioConIVA != value) {
                    _PrecioConIVA = value;
                    RaisePropertyChanged(PrecioConIVAPropertyName);
                }
            }
        }

        public decimal Impuesto {
            get { return _Impuesto; }
            set {
                if (_Impuesto != value) {
                    _Impuesto = value;
                    RaisePropertyChanged(ImpuestoPropertyName);
                }
            }
        }

        public bool MostrarPrecioDesglosadoDivisa {
            get { return UsaMonedaExtranjera && UsaMostrarPreciosEnDivisa && MostrarPrecioDesglosado; }
        }

        public string CodigoMonedaDivisa {
            get { return _CodigoMonedaDivisa; }
            set {
                if (_CodigoMonedaDivisa != value) {
                    _CodigoMonedaDivisa = value;
                    RaisePropertyChanged(CodigoMonedaDivisaPropertyName);
                }
            }
        }

        public bool UsaMonedaExtranjera {
            get { return _UsaMonedaExtranjera; }
            set {
                if (_UsaMonedaExtranjera != value) {
                    _UsaMonedaExtranjera = value;
                    RaisePropertyChanged(UsaMonedaExtranjeraPropertyName);
                }
            }
        }

        public bool MostrarPreciosEnDivisaConIva {
            get { return UsaMonedaExtranjera && UsaMostrarPreciosEnDivisa; }
        }

        #endregion

        #region Constructor
        public VerificadorDePreciosViewModel() : this(new ArticuloInventario(), eAccionSR.Abrir) {
        }

        public VerificadorDePreciosViewModel(ArticuloInventario valArticuloInventario, eAccionSR valActionSr) {
            CuadroDeBusquedaDeArticulosViewModel = new CuadroDeBusquedaDeArticulosVerificadorViewModel();
            InitializeTipoDeBusqueda();
            InitializeCommands();
        }

        private void InitializeTipoDeBusqueda() {
            if (_TipoDeBusquedaArticulo == eTipoDeBusquedaArticulo.Codigo) {
                BusquedaPorCodigo = true;
                CuadroDeBusquedaDeArticulosViewModel.IsControlVisible = false;
            } else {
                BusquedaPorCodigo = false;
                CuadroDeBusquedaDeArticulosViewModel.IsControlVisible = true;
            }
            RaisePropertyChanged("BusquedaDinamica");
            RaisePropertyChanged("BusquedaPorCodigo");
        }
        #endregion

        #region Inicializacion

        public void InitializeViewModel(eAccionSR valAction) {
            CuadroDeBusquedaDeArticulosViewModel.ItemSelected += ArticuloSeleccionado;
            NombreImpuesto = LibDefGen.ProgramInfo.IsCountryPeru() ? "IGV" : "IVA";
            CodigoMoneda = LibDefGen.ProgramInfo.IsCountryPeru() ? "S/" : "Bs.";
            UsaMonedaExtranjera = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMonedaExtranjera"));
            if (UsaMonedaExtranjera) {
                CodigoMonedaDivisa = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
                UsaMostrarPreciosEnDivisa = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaMostrarPreciosEnDivisa"));
                ConexionMonedaDivisa = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkMonedaViewModel>("Moneda", LibSearchCriteria.CreateCriteriaFromText("Codigo", CodigoMonedaDivisa), new clsArticuloInventarioNav());
            } else {
                UsaMostrarPreciosEnDivisa = false;
            }
            
            LimpiarPantalla();
        }

        private void ArticuloSeleccionado(LookupItem articulo) {
            ChooseArticuloCommand.Execute(_TipoDeBusquedaArticulo == eTipoDeBusquedaArticulo.Codigo ? articulo.Code : articulo.Description);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseArticuloCommand = new RelayCommand<string>(ExecuteChooseArticulo);
        }

        #endregion

        #region Execute Commands
        private void ExecuteChooseArticulo(string valBusquedaArticulo) {
            if (LibString.IsNullOrEmpty(valBusquedaArticulo)) {
                return;
            }
            if (_TiempoDeEsperaEnSegundos < 3) {
                _TiempoDeEsperaEnSegundos = 3;
            }
            string vCampoABuscar = _TipoDeBusquedaArticulo == eTipoDeBusquedaArticulo.Codigo ? "Codigo" : "Descripcion";
            LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1." + vCampoABuscar, valBusquedaArticulo);
            vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("dbo.Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania")), eLogicOperatorType.And);
            vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("TipoArticuloInv", eTipoArticuloInv.Simple), eLogicOperatorType.And);
            var vConexionArticulo = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkArticuloInventarioViewModel>("Articulo Inventario", vDefaultCriteria, new clsArticuloInventarioNav());
            if (vConexionArticulo != null) {
                AjustarPrecioAMostrarSegunNivelDePrecio(ref vConexionArticulo);
                ConexionArticulo = vConexionArticulo;
                IsEnabledBusqueda = false;
                MuestraDatosArticulo = true;
                OcultaDatosArticulo = false;
                Task.Factory.StartNew(() => Thread.Sleep(_TiempoDeEsperaEnSegundos * 1000))
                    .ContinueWith((t) => LimpiarPantalla(),
                TaskScheduler.FromCurrentSynchronizationContext());
                if (UsaMonedaExtranjera) {
                    SimboloMonedaDivisa = ConexionMonedaDivisa.Simbolo;
                    AsignaTasaDelDia();
                    CalcularPrecios(vConexionArticulo.PrecioSinIVA, vConexionArticulo.PrecioConIVA, _Tasa, vConexionArticulo.Impuesto, vConexionArticulo.MePrecioSinIva, vConexionArticulo.MePrecioConIva, vConexionArticulo.ImpuestoMe);
                }
                else {
                    CalcularPrecios(vConexionArticulo.PrecioSinIVA, vConexionArticulo.PrecioConIVA, _Tasa, vConexionArticulo.Impuesto, vConexionArticulo.MePrecioSinIva, vConexionArticulo.MePrecioConIva, vConexionArticulo.ImpuestoMe);
                }
            } else {
                ConexionArticulo = null;
                IsEnabledBusqueda = false;
                MensajeAUsuario = "Artículo no encontrado o Inexistente";
                Task.Factory.StartNew(() => Thread.Sleep(_TiempoDeEsperaEnSegundos * 1000))
                    .ContinueWith((t) => LimpiarPantalla(),
                TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        #endregion

        #region Métodos Auxiliares
        private void LimpiarPantalla() {
            CuadroDeBusquedaDeArticulosViewModel.Filter = string.Empty;
            CuadroDeBusquedaDeArticulosViewModel.HideListCommand.Execute(null);
            MensajeAUsuario = "Consulte el precio de su Artículo";
            OcultaDatosArticulo = true;
            MuestraDatosArticulo = false;
            IsEnabledBusqueda = true;
            ConexionArticulo = null;
            FocusCuadroBusquedaPorCodigo();
            CuadroDeBusquedaDeArticulosViewModel.NotifyFocusAndSelect();
        }

        public void FocusCuadroBusquedaPorCodigo() {
            RaiseMoveFocus(ConexionArticuloPropertyName);
        }

        private void AjustarPrecioAMostrarSegunNivelDePrecio(ref FkArticuloInventarioViewModel refConexionArticulo) {
            switch (_NivelDePrecioAMostrar) {
                case eNivelDePrecio.Nivel2:
                    refConexionArticulo.PrecioConIVA = refConexionArticulo.PrecioConIVA2;
                    refConexionArticulo.PrecioSinIVA = refConexionArticulo.PrecioSinIVA2;
                    refConexionArticulo.MePrecioConIva = refConexionArticulo.MePrecioConIva2;
                    refConexionArticulo.MePrecioSinIva = refConexionArticulo.MePrecioSinIva2;
                    break;
                case eNivelDePrecio.Nivel3:
                    refConexionArticulo.PrecioConIVA = refConexionArticulo.PrecioConIVA3;
                    refConexionArticulo.PrecioSinIVA = refConexionArticulo.PrecioSinIVA3;
                    refConexionArticulo.MePrecioConIva = refConexionArticulo.MePrecioConIva3;
                    refConexionArticulo.MePrecioSinIva = refConexionArticulo.MePrecioSinIva3;
                    break;
                case eNivelDePrecio.Nivel4:
                    refConexionArticulo.PrecioConIVA = refConexionArticulo.PrecioConIVA4;
                    refConexionArticulo.PrecioSinIVA = refConexionArticulo.PrecioSinIVA4;
                    refConexionArticulo.MePrecioConIva = refConexionArticulo.MePrecioConIva4;
                    refConexionArticulo.MePrecioSinIva = refConexionArticulo.MePrecioSinIva4;
                    break;
                default:
                    break;
            }
        }

        private void CalcularPreciosEnDivisas(decimal valPrecioSinIva, decimal valPrecioConIva, decimal valTasa, decimal valImpuesto, decimal valPrecioSinIvaDivisa, decimal valPrecioConIvaDivisa, decimal valImpuestoDivisa) {
            if (valTasa > 0) {
                    switch (UsaMostrarPreciosEnDivisa) {
                        case true:
                            switch (_TipoDeConversionParaPrecios) {
                                case eTipoDeConversionParaPrecios.MonedaLocalADivisa:
                                if ((valPrecioConIva > 0) && (valPrecioSinIva > 0)) {
                                    PrecioSinIVADivisa = (valPrecioSinIva / valTasa);
                                    PrecioConIVADivisa = (valPrecioConIva / valTasa);
                                    ImpuestoDivisa = (valImpuesto / valTasa);
                                    PrecioSinIVA = valPrecioSinIva;
                                    PrecioConIVA = valPrecioConIva;
                                    Impuesto = valImpuesto;
                                } else {
                                    MensajeInformativo();
                                }
                                break;
                            case eTipoDeConversionParaPrecios.DivisaAMonedaLocal:
                                if ((valPrecioConIvaDivisa > 0) && (valPrecioSinIvaDivisa > 0)) {
                                    PrecioSinIVA = (valPrecioSinIvaDivisa * valTasa);
                                    PrecioConIVA = (valPrecioConIvaDivisa * valTasa);
                                    Impuesto = (valImpuestoDivisa * valTasa);
                                    PrecioSinIVADivisa = valPrecioSinIvaDivisa;
                                    PrecioConIVADivisa = valPrecioConIvaDivisa;
                                    ImpuestoDivisa = valImpuestoDivisa;
                                }
                                else {
                                    MensajeInformativo();
                                }
                                break;
                            }
                            break;
                        case false:
                            CalcularPreciosEnMonedaLocal(valPrecioSinIva, valPrecioConIva, valTasa, valImpuesto, valPrecioSinIvaDivisa, valPrecioConIvaDivisa, valImpuestoDivisa);
                            break;
                        default:
                            break;
                    }
                } else {
                    MensajeInformativo();
                }
        }

        public bool AsignaTasaDelDia() {
            var vFechaActual = LibDate.Today();
            decimal vTasa = 1;
            bool vResult = false;
            if (ConexionMonedaDivisa != null) {
                if (((ICambioPdn)new clsCambioNav()).ExisteTasaDeCambioParaElDia(CodigoMonedaDivisa, vFechaActual, out vTasa)) {
                    _Tasa = vTasa;
                    vResult = true;
                } else {
                    CambioViewModel vViewModel = new CambioViewModel();
                    vViewModel.InitializeViewModel(eAccionSR.Insertar);
                    vViewModel.OnCambioAMonedaLocalChanged += (cambio) => _Tasa = cambio;
                    vViewModel.FechaDeVigencia = vFechaActual;
                    vViewModel.CodigoMoneda = ConexionMonedaDivisa.Codigo;
                    vViewModel.NombreMoneda = ConexionMonedaDivisa.Nombre;
                    vViewModel.IsEnabledFecha = false;
                    vResult = LibMessages.EditViewModel.ShowEditor(vViewModel, true);
                    if (!vResult) {
                        vResult = false;
                        LibMessages.MessageBox.Alert(this, "No puede continuar sin haber introducido una tasa.", "No hay tasa asignada");
                    }
                }
            } else {
                LibMessages.MessageBox.Alert(this, "No puede continuar sin haber introducido una tasa.", "No hay tasa asignada");
            }
            return vResult;
        }

        private void MensajeInformativo() {
            IsEnabledBusqueda = false;
            MuestraDatosArticulo = false;
            OcultaDatosArticulo = true;
            MensajeAUsuario = "La información del producto no está actualizada. Por favor, diríjase a caja.";
            Task.Factory.StartNew(() => Thread.Sleep(_TiempoDeEsperaEnSegundos * 10000))
                .ContinueWith((t) => LimpiarPantalla(),
            TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void CalcularPreciosEnMonedaLocal(decimal valPrecioSinIva, decimal valPrecioConIva, decimal valTasa, decimal valImpuesto, decimal valPrecioSinIvaDivisa, decimal valPrecioConIvaDivisa, decimal valImpuestoDivisa) {
            if(valPrecioSinIva > 0 && valPrecioConIva > 0) {
                PrecioSinIVA = valPrecioSinIva;
                PrecioConIVA = valPrecioConIva;
                Impuesto = valImpuesto;
            } else {
                MensajeInformativo();
            }
        }

        private void CalcularPrecios(decimal valPrecioSinIva, decimal valPrecioConIva, decimal valTasa, decimal valImpuesto, decimal valPrecioSinIvaDivisa, decimal valPrecioConIvaDivisa, decimal valImpuestoDivisa) {
            if (UsaMonedaExtranjera) {
                CalcularPreciosEnDivisas(valPrecioSinIva, valPrecioConIva, valTasa, valImpuesto, valPrecioSinIvaDivisa, valPrecioConIvaDivisa, valImpuestoDivisa);
            } else {
                CalcularPreciosEnMonedaLocal(valPrecioSinIva, valPrecioConIva, valTasa, valImpuesto, valPrecioSinIvaDivisa, valPrecioConIvaDivisa, valImpuestoDivisa);
            }
        }

        #endregion

        #region Métodos Heredados

        public object GetModel() {
            return null;
        }

        #endregion
    }
}
