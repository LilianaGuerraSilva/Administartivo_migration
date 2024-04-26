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

namespace Galac.Adm.Uil.GestionProduccion.Reportes {
    public class clsListaDeMaterialesViewModel : LibInputRptViewModelBase<ListaDeMateriales> {

        #region Constantes

        private const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        private const string CodigoListaMaterialesPropertyName = "CodigoListaMateriales";
        private const string NombreListaDeMaterialesPropertyName = "NombreListaDeMateriales";
        private const string IsEnabledCodigoListaDeMaterialesPropertyName = "IsEnabledCodigoListaDeMateriales";
        private const string CantidadAProducirPropertyName = "CantidadAProducir";
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

        private string _MonedaDelInforme;
        #endregion //Variables

        #region Propiedades

        public FkListaDeMaterialesInformeViewModel ConexionListaDeMateriales {
            get {
                return _ConexionListaDeMateriales;
            }
            set {
                if (_ConexionListaDeMateriales != value) {
                    _ConexionListaDeMateriales = value;
                    RaisePropertyChanged(CodigoListaMaterialesPropertyName);
                }
                if (_ConexionListaDeMateriales == null) {
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
                if (_CodigoListaMateriales != value) {
                    _CodigoListaMateriales = value;
                    RaisePropertyChanged(CodigoListaMaterialesPropertyName);
                    if (LibString.IsNullOrEmpty(_CodigoListaMateriales, true)) {
                        ConexionListaDeMateriales = null;
                        NombreListaDeMateriales = string.Empty;
                    } else {
                        NombreListaDeMateriales = ConexionListaDeMateriales.Nombre;
                    }
                }
            }
        }

        public string NombreListaDeMateriales {
            get {
                return _NombreListaDeMateriales;
            }
            set {
                if (_NombreListaDeMateriales != value) {
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
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;
                    if (eCantidadAImprimir.One.Equals(_CantidadAImprimir)) {
                        IsEnabledCodigoListaDeMateriales = true;
                    } else {
                        IsEnabledCodigoListaDeMateriales = false;
                        NombreListaDeMateriales = string.Empty;
                        CodigoListaMateriales= string.Empty;
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
                    RaisePropertyChanged(CantidadAProducirPropertyName);
                    if (_CantidadAProducir == 0) {
                        _CantidadAProducir = 1;
                    }
                }
            }
        }

        public override string DisplayName {
            get { return "Lista de Materiales de Inventario a Producir"; }
        }

        public override bool IsSSRS {
            get { return false; }
        }

        public RelayCommand<string> ChooseCodigoListaDeMaterialesCommand {
            get;
            private set;
        }

        public eTasaDeCambioParaImpresion TipoTasaDeCambio {
			get { return _TipoTasaDeCambioAsEnum; }
			set {
				if (_TipoTasaDeCambioAsEnum != value) {
					_TipoTasaDeCambioAsEnum = value;
					RaisePropertyChanged(() => TipoTasaDeCambio);
				}
			}
		}

		public eTasaDeCambioParaImpresion[] ArrayTiposTasaDeCambio {
			get { return LibEnumHelper<eTasaDeCambioParaImpresion>.GetValuesInArray(); }
		}

		public ObservableCollection<string> ListaMonedaDelInforme {
			get { return _ListaMonedaDelInforme; }
			set { _ListaMonedaDelInforme = value; }
		}
        
        public ObservableCollection<string> ListaMonedasActivas { get; set; }
        public string MonedaDelInforme {
            get {
                return _MonedaDelInforme;
            }
            set {
                if (_MonedaDelInforme != value) {
                    _MonedaDelInforme = value;
                    LlenarEnumerativosMonedas();
                    RaisePropertyChanged(MonedaDelInformePropertyName);                 
                    RaisePropertyChanged(() => IsVisibleTipoTasaDeCambio);
                }
            }
        }

        public string Moneda { get; set; }            

        public bool IsEnabledCodigoListaDeMateriales {
            get {
                return _IsEnabledCodigoListaDeMateriales;
            }
            set {
                if (_IsEnabledCodigoListaDeMateriales != value) {
                    _IsEnabledCodigoListaDeMateriales = value;
                    RaisePropertyChanged(IsEnabledCodigoListaDeMaterialesPropertyName);
                }
            }
        }

        public bool IsVisibleTipoTasaDeCambio { get { return !LibString.S1IsEqualToS2(MonedaDelInforme, "Bolívares"); } }		
        #endregion

        #region Constructores

        public clsListaDeMaterialesViewModel() {
            _CantidadAImprimir = eCantidadAImprimir.All;
            _CodigoListaMateriales = string.Empty;
            _CantidadAProducir = 1;            
            TipoTasaDeCambio = eTasaDeCambioParaImpresion.DelDia;
            Moneda = string.Empty;
            LlenarListaMonedasActivas();
            LlenarEnumerativosMonedas();
           
        }
        #endregion //Constructores

        #region Metodos Generados

        public eCantidadAImprimir[] ECantidadAImprimir {
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
                if (valCodigo == null) {
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
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Galac.Adm.Rpt.GestionProduccion.clsListaDeMaterialesDeSalida.ReportName);
            }
        }

        #endregion //Metodos Generados

        #region Código Programador       

        private void LlenarEnumerativosMonedas() {
            ListaMonedaDelInforme = new ObservableCollection<string>() {
               "Bolívares",
               Moneda,
               "Bolívares expresados en " + Moneda,
               Moneda + "expresados  Bolívares"

            };
            MonedaDelInforme = ListaMonedaDelInforme[0];
        }

		void LlenarListaMonedasActivas() {
            ListaMonedasActivas = new Galac.Saw.Lib.clsLibSaw().ListaDeMonedasActivasParaInformes(false, true);
			if (ListaMonedasActivas.Count > 0) {
				Moneda = ListaMonedasActivas[0];
			}
		}         

        private ValidationResult IsCodigoListaRequired() {
            ValidationResult vResult = ValidationResult.Success;
            if (IsEnabledCodigoListaDeMateriales && LibText.IsNullOrEmpty(CodigoListaMateriales)) {
                vResult = new ValidationResult("Debe seleccionar una lista de materiales a consultar.");
            }
            return vResult;
        }
        #endregion //Código Programador
    } //End of class clsListaDeMaterialesDeInventarioAProducirViewModel
} //End of namespace Galac.Adm.Uil.GestionProduccion

