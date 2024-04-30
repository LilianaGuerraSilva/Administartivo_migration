using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
using Galac.Saw.Lib;
using System.Collections.ObjectModel;
using Galac.Adm.Uil.Banco.ViewModel;
using Galac.Comun.Uil.TablasGen.ViewModel;
using Galac.Comun.Ccl.TablasGen;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Uil;
using Galac.Comun.Brl.TablasGen;

namespace Galac.Adm.Uil.GestionProduccion.Reportes {
    public class clsListaDeMaterialesViewModel : LibInputRptViewModelBase<ListaDeMateriales> {

        #region Constantes

        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string CodigoListaMaterialesPropertyName = "CodigoListaMateriales";
        private const string NombreListaDeMaterialesPropertyName = "NombreListaDeMateriales";
        private const string IsEnabledCodigoListaDeMaterialesPropertyName = "IsEnabledCodigoListaDeMateriales";
        private const string CantidadAProducirPropertyName = "CantidadAProducir";        
        private const string ListaMonedaDelInformePropertyName = "ListaMonedaDelInforme";
        private const string TasaDeCambioPropertyName = "TasaDeCambio";
        private const string IsVisibleTasaDeCambioPropertyName = "IsVisibleTasaDeCambio";
        private const string MonedaDelInformePropertyName = "MonedaDelInforme";
        #endregion

        #region Variables
        private eCantidadAImprimir _CantidadAImprimir;
        private string _CodigoListaMateriales;
        private string _NombreListaDeMateriales;
        private decimal _CantidadAProducir;
        private FkListaDeMaterialesInformeViewModel _ConexionListaDeMateriales = null;
        private bool _IsEnabledCodigoListaDeMateriales;
        private eTasaDeCambioParaImpresion _TipoTasaDeCambioAsEnum;
        private ObservableCollection<string> _ListaMonedaDelInforme;                
        private decimal _TasaDeCambio;
        private string _MonedaDelInforme;
        #endregion //Variables
        #region Propiedades

        public FkListaDeMaterialesInformeViewModel ConexionListaDeMateriales {
            get {
                return _ConexionListaDeMateriales;
            }
            set {
                if(_ConexionListaDeMateriales != value) {
                    _ConexionListaDeMateriales = value;
                    RaisePropertyChanged(CodigoListaMaterialesPropertyName);
                }
                if(_ConexionListaDeMateriales == null) {
                    _CodigoListaMateriales = string.Empty;
                }
            }
        }

        [LibCustomValidation("IsCodigoListaRequired")]
        public string CodigoListaMateriales {
            get {
                return _CodigoListaMateriales;
            }
            set {
                if(_CodigoListaMateriales != value) {
                    _CodigoListaMateriales = value;
                    if(LibString.IsNullOrEmpty(_CodigoListaMateriales, true)) {
                        ConexionListaDeMateriales = null;
                        NombreListaDeMateriales = string.Empty;
                    } else {
                        NombreListaDeMateriales = ConexionListaDeMateriales.Nombre;
                    }
                    RaisePropertyChanged(CodigoListaMaterialesPropertyName);
                }
            }
        }

        public string NombreListaDeMateriales {
            get {
                return _NombreListaDeMateriales;
            }
            set {
                if(_NombreListaDeMateriales != value) {
                    _NombreListaDeMateriales = value;
                    RaisePropertyChanged(NombreListaDeMaterialesPropertyName);
                }
            }
        }

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if(_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    if(eCantidadAImprimir.One.Equals(_CantidadAImprimir)) {
                        IsEnabledCodigoListaDeMateriales = true;
                    } else {
                        IsEnabledCodigoListaDeMateriales = false;
                        NombreListaDeMateriales = string.Empty;
                        CodigoListaMateriales = string.Empty;
                    }
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsEnabledCodigoListaDeMaterialesPropertyName);
                }
            }
        }

        public decimal CantidadAProducir {
            get {
                return _CantidadAProducir;
            }
            set {
                if (_CantidadAProducir != value) {
                    _CantidadAProducir = value;
                    if (_CantidadAProducir == 0) {
                        _CantidadAProducir = 1;
                    }
                    RaisePropertyChanged(CantidadAProducirPropertyName);
                }
            }
        }

        public override string DisplayName {
            get {
                return "Lista de Materiales a Producir";
            }
        }

        public override bool IsSSRS {
            get {
                return false;
            }
        }

        public RelayCommand<string> ChooseCodigoListaDeMaterialesCommand {
            get;
            private set;
        }
        public RelayCommand<string> ChooseMonedaCommand {
            get;
            private set;
        }

        public eTasaDeCambioParaImpresion TipoTasaDeCambio {
            get {
                return _TipoTasaDeCambioAsEnum;
            }
            set {
                if(_TipoTasaDeCambioAsEnum != value) {
                    _TipoTasaDeCambioAsEnum = value;
                    RaisePropertyChanged(() => TipoTasaDeCambio);
                }
            }
        }

        public eTasaDeCambioParaImpresion[] ArrayTiposTasaDeCambio {
            get {
                return LibEnumHelper<eTasaDeCambioParaImpresion>.GetValuesInArray();
            }
        }

        public ObservableCollection<string> ListaMonedaDelInforme {
            get {
                return _ListaMonedaDelInforme;
            }
            set {
                _ListaMonedaDelInforme = value;
                RaisePropertyChanged(ListaMonedaDelInformePropertyName);
            }
        }
       
        public string MonedaDelInforme {
            get {
                return _MonedaDelInforme;
            }
            set {
                if (_MonedaDelInforme != value) {
                    _MonedaDelInforme = value;
                    RaisePropertyChanged(MonedaDelInformePropertyName);
                    RaisePropertyChanged(IsVisibleTasaDeCambioPropertyName);
                    if (IsVisibleTasaDeCambio) {
                        AsignaTasaDelDia();
                    }
                }
            }
        }
        public string CodigoMoneda { get; set; }        
        public string CodigoMonedaExtranjera { get; set; } 

        public bool IsEnabledCodigoListaDeMateriales {
            get {
                return _IsEnabledCodigoListaDeMateriales;
            }
            set {
                if(_IsEnabledCodigoListaDeMateriales != value) {
                    _IsEnabledCodigoListaDeMateriales = value;
                    RaisePropertyChanged(IsEnabledCodigoListaDeMaterialesPropertyName);
                }
            }
        }

        public bool IsVisibleTasaDeCambio {
            get {
                return LibString.S1IsInS2("expresado en", MonedaDelInforme);
            }
        }

        public decimal TasaDeCambio {
            get {
                return _TasaDeCambio;
            }
            set {
                if (_TasaDeCambio != value) {
                    _TasaDeCambio = value;
                    RaisePropertyChanged(TasaDeCambioPropertyName);
                }
            }
        }
        #endregion

        #region Constructores

        public clsListaDeMaterialesViewModel() {
            _CantidadAImprimir = eCantidadAImprimir.All;
            _CodigoListaMateriales = string.Empty;
            _CantidadAProducir = 1;
            TipoTasaDeCambio = eTasaDeCambioParaImpresion.DelDia;
            CantidadAImprimir = eCantidadAImprimir.One;
            CodigoMoneda = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMoneda");
            CodigoMonedaExtranjera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaExtranjera");
            LlenarEnumerativosMonedas();
        }        
        #endregion //Constructores
        #region Metodos Generados        

        public eCantidadAImprimir[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsListaDeMaterialesNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoListaDeMaterialesCommand = new RelayCommand<string>(ExecuteChooseCodigoListaMaterialesCommand);            
        }
        
        private void ExecuteChooseCodigoListaMaterialesCommand(string valCodigo) {
            try {
                if(valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_ListaDeMateriales_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionListaDeMateriales = ChooseRecord<FkListaDeMaterialesInformeViewModel>("Lista de Materiales", vDefaultCriteria, vFixedCriteria, string.Empty);
                if(ConexionListaDeMateriales != null) {
                    CodigoListaMateriales = ConexionListaDeMateriales.Codigo;
                    NombreListaDeMateriales = ConexionListaDeMateriales.Nombre;                    
                } else {
                    CodigoListaMateriales = string.Empty;
                    NombreListaDeMateriales = string.Empty;
                }
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Galac.Adm.Rpt.GestionProduccion.clsListaDeMaterialesDeSalida.ReportName);
            }
        }
        
        #endregion //Metodos Generados
        #region Código Programador     
        private void LlenarEnumerativosMonedas() {
            IMonedaPdn insMoneda = new clsMonedaNav();
            string vMonedaExtranjera = insMoneda.GetNombreMoneda(CodigoMonedaExtranjera);
            string vMonedLocal = insMoneda.GetNombreMoneda(CodigoMoneda);
            ListaMonedaDelInforme = new ObservableCollection<string>() {
               vMonedLocal,
               vMonedaExtranjera,
               $"{vMonedLocal} expresado en {vMonedaExtranjera}",
                 $"{vMonedaExtranjera} expresado en {vMonedLocal}"
            };
            MonedaDelInforme = ListaMonedaDelInforme[0];
        }

        private void AsignaTasaDelDia() {
            bool vElProgramaEstaEnModoAvanzado = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsModoAvanzado");
            bool vUsarLimiteMaximoParaIngresoDeTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsarLimiteMaximoParaIngresoDeTasaDeCambio");
            decimal vMaximoLimitePermitidoParaLaTasaDeCambio = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros", "MaximoLimitePermitidoParaLaTasaDeCambio");
            bool vObtenerAutomaticamenteTasaDeCambioDelBCV = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "ObtenerAutomaticamenteTasaDeCambioDelBCV");
            TasaDeCambio = clsSawCambio.InsertaTasaDeCambioParaElDia(CodigoMonedaExtranjera, LibDate.Today(), vUsarLimiteMaximoParaIngresoDeTasaDeCambio, vMaximoLimitePermitidoParaLaTasaDeCambio, vElProgramaEstaEnModoAvanzado, vObtenerAutomaticamenteTasaDeCambioDelBCV);
        }

        private ValidationResult IsCodigoListaRequired() {
            ValidationResult vResult = ValidationResult.Success;
            if(IsEnabledCodigoListaDeMateriales && LibText.IsNullOrEmpty(CodigoListaMateriales)) {
                vResult = new ValidationResult("Debe seleccionar una lista de materiales a consultar.");
            }
            return vResult;
        }
        #endregion //Código Programador
    } //End of class clsListaDeMaterialesDeInventarioAProducirViewModel
} //End of namespace Galac.Adm.Uil.GestionProduccion

