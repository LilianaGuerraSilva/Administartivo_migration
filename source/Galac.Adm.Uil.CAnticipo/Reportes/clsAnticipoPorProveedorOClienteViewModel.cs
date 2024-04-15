using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.CAnticipo;
using Galac.Adm.Brl.CAnticipo;
using Galac.Saw.Lib;
using Galac.Saw.Uil.Cliente.ViewModel;
using System.Collections.ObjectModel;
using Galac.Adm.Uil.GestionCompras.ViewModel;

namespace Galac.Adm.Uil.CAnticipo.Reportes {

    public class clsAnticipoPorProveedorOClienteViewModel : LibInputRptViewModelBase<Anticipo> {

        #region Constantes
        public const string EstatusAnticipoPropertyName = "EstatusAnticipo";
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
        public const string CodigoClienteProveedorPropertyName = "CodigoClienteProveedor";
        public const string NombreClientProveedorPropertyName = "NombreClientProveedor";
        public const string OrdenamientoPorStatusPropertyName = "OrdenamientoPorStatus";
        public const string MonedaDelInformePropertyName = "MonedaDelInforme";
        public const string ClienteProveedorIsEnablePropertyName = "ClienteProveedorIsEnable";
        public const string OrdenarPorClienteIsEnablePropertyName = "OrdenarPorClienteIsEnable";
        public const string CantidadAImprimirClienteProveedorPropertyName = "CantidadAImprimirClienteProveedor";
        private const string IsVisibleEstatusAnticipoPropertyName = "IsVisibleEstatusAnticipo";
        #endregion
        #region Variables
        private eStatusAnticipo _EstatusAnticipo;
        private eCantidadAImprimir _CantidadAImprimir;
        private string _CodigoClientProveedor;
        private string _NombreClientProveedor;
        private bool _OrdenamientoPorStatus;
        private bool _EsCliente;
        private eMonedaDelInformeMM _MonedaDelInforme;
        private FkClienteViewModel _ConexionCodigoCliente = null;
        private FkProveedorViewModel _ConexionCodigoProveedor = null;
        private eTasaDeCambioParaImpresion _TipoTasaDeCambioAsEnum;            
        private eCantidadAImprimir _CantidadAImprimirClienteProveedor;        
		private ObservableCollection<eMonedaDelInformeMM> _ListaMonedaDelInforme = new ObservableCollection<eMonedaDelInformeMM>();
        #endregion //Variables

        #region Propiedades
        public override string DisplayName {
            get {
                return "Anticipos por " + (EsCliente ? "Cliente" : "Proveedor");
            }
        }

        public  string lblClienteProveedor {
            get {
                return (EsCliente ? "Cliente" : "Proveedor");
            }
        }

         public string lblOrdenarPorClienteOProveedor {
            get {
                return "Ordenar por Estatus o " + (EsCliente ? "Cliente" : "Proveedor");
            }
        }

        public eStatusAnticipo EstatusAnticipo {
            get {
                return _EstatusAnticipo;
            }
            set {
                if (_EstatusAnticipo != value) {
                    _EstatusAnticipo = value;
                    RaisePropertyChanged(EstatusAnticipoPropertyName);                    
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
                    RaisePropertyChanged(CantidadAImprimirPropertyName); 
                    RaisePropertyChanged(IsVisibleEstatusAnticipoPropertyName);
                }
            }
        }


        public eCantidadAImprimir CantidadAImprimirClienteProveedor {
            get {
                return _CantidadAImprimirClienteProveedor;
            }
            set {
                if (_CantidadAImprimirClienteProveedor != value) {
                    _CantidadAImprimirClienteProveedor = value;
                    if (_CantidadAImprimirClienteProveedor == eCantidadAImprimir.All) {
                        CodigoClienteProveedor = string.Empty;
                        NombreClientProveedor = string.Empty;
                    }
                    RaisePropertyChanged(CantidadAImprimirClienteProveedorPropertyName);
                    RaisePropertyChanged(ClienteProveedorIsEnablePropertyName);
                    RaisePropertyChanged(OrdenarPorClienteIsEnablePropertyName);
                }
            }
        }

        public string CodigoClienteProveedor {
            get {
                return _CodigoClientProveedor;
            }
            set {
                if (_CodigoClientProveedor != value) {
                    _CodigoClientProveedor = value;
                    if (LibString.IsNullOrEmpty(CodigoClienteProveedor, true)) {                        
                        ConexionCodigoProveedor = null;
                    }
                    RaisePropertyChanged(CodigoClienteProveedorPropertyName);
                }
            }
        }

        public string NombreClientProveedor {
            get {
                return _NombreClientProveedor;
            }
            set {
                if (_NombreClientProveedor != value) {
                    _NombreClientProveedor = value;
                    RaisePropertyChanged(NombreClientProveedorPropertyName);
                }
            }
        }

        public bool OrdenamientoPorStatus {
            get {
                return _OrdenamientoPorStatus;
            }
            set {
                if (_OrdenamientoPorStatus != value) {
                    _OrdenamientoPorStatus = value;
                    RaisePropertyChanged(OrdenamientoPorStatusPropertyName);
                }
            }
        }

        public eMonedaDelInformeMM MonedaDelInforme {
            get {
                return _MonedaDelInforme;
            }
            set {
                if (_MonedaDelInforme != value) {
                    _MonedaDelInforme = value;
                    RaisePropertyChanged(MonedaDelInformePropertyName);
                    RaisePropertyChanged(() => IsVisibleMonedasActivas);
					RaisePropertyChanged(() => IsVisibleTipoTasaDeCambio);
                }
            }
        }

        public bool EsCliente {
            get {
                return _EsCliente;
            }
            set {
                if (_EsCliente != value) {
                    _EsCliente = value;
                }
            }
        }

        public eStatusAnticipo[] ArrayStatusAnticipo {
            get {
                return LibEnumHelper<eStatusAnticipo>.GetValuesInArray();
            }
        }

        public eCantidadAImprimir[] ArrayCantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }

         public eCantidadAImprimir[] ArrayCantidadAImprimirClienteProveedor {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }       

        public FkClienteViewModel ConexionCodigoCliente {
            get {
                return _ConexionCodigoCliente;
            }
            set {
                if (_ConexionCodigoCliente != value) {
                    _ConexionCodigoCliente = value;
                    RaisePropertyChanged(CodigoClienteProveedorPropertyName);
                }
                if (_ConexionCodigoCliente == null) {
                    CodigoClienteProveedor = string.Empty;
                    NombreClientProveedor = string.Empty;
                }
            }
        }

         public FkProveedorViewModel ConexionCodigoProveedor {
            get {
                return _ConexionCodigoProveedor;
            }
            set {
                if (_ConexionCodigoProveedor != value) {
                    _ConexionCodigoProveedor = value;
                    RaisePropertyChanged(CodigoClienteProveedorPropertyName);
                }
                if (_ConexionCodigoProveedor == null) {
                    CodigoClienteProveedor = string.Empty;
                    NombreClientProveedor = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoClienteProveedorCommand {
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

		public ObservableCollection<eMonedaDelInformeMM> ListaMonedaDelInforme {
			get { return _ListaMonedaDelInforme; }
			set { _ListaMonedaDelInforme = value; }
		}
        
        public string Moneda { get; set; }            

        public bool ClienteProveedorIsEnable {
            get {
                return CantidadAImprimirClienteProveedor == eCantidadAImprimir.One;
            }
        }

        public bool OrdenarPorClienteIsEnable {
            get {
                return CantidadAImprimirClienteProveedor == eCantidadAImprimir.All;
            }
        }        
        public bool IsVisibleEstatusAnticipo {
            get {
                return CantidadAImprimir == eCantidadAImprimir.One;
            }
        }

		public ObservableCollection<string> ListaMonedasActivas { get; set; }
        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS => throw new NotImplementedException();
        #endregion //Propiedades
        #region Constructores

        public clsAnticipoPorProveedorOClienteViewModel(bool valEsCliente) {
            EsCliente = valEsCliente;
            MonedaDelInforme = eMonedaDelInformeMM.EnMonedaOriginal;
			TipoTasaDeCambio = eTasaDeCambioParaImpresion.DelDia;
            EstatusAnticipo = eStatusAnticipo.Vigente;
            CantidadAImprimir = eCantidadAImprimir.One;
            CantidadAImprimirClienteProveedor = eCantidadAImprimir.All;
            Moneda = string.Empty;
            CodigoClienteProveedor = string.Empty;
			LlenarEnumerativosMonedas();
			LlenarListaMonedasActivas();
        }
        #endregion //Constructores        
        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsAnticipoNav();
        }
        #region Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoClienteProveedorCommand = new RelayCommand<string>(ExecuteChooseCodigoClienteProveedorCommand);
        }

        private void ExecuteChooseCodigoClienteProveedorCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                if (EsCliente) {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Cliente_B1.Codigo", valCodigo);
                    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                    ConexionCodigoCliente = ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
                    if (ConexionCodigoCliente != null) {
                        CodigoClienteProveedor = ConexionCodigoCliente.Codigo;
                        NombreClientProveedor = ConexionCodigoCliente.Nombre;
                    } else {
                        CodigoClienteProveedor = string.Empty;
                        NombreClientProveedor = string.Empty;
                    }
                } else {
                    LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("adm.GV_Proveedor_B1.CodigoProveedor", valCodigo);
                    LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("adm.GV_Proveedor_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                    ConexionCodigoProveedor = ChooseRecord<FkProveedorViewModel>("Proveedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                    if (ConexionCodigoProveedor != null) {
                        CodigoClienteProveedor = ConexionCodigoProveedor.CodigoProveedor;
                        NombreClientProveedor = ConexionCodigoProveedor.NombreProveedor;
                    } else {
                        CodigoClienteProveedor = string.Empty;
                        NombreClientProveedor = string.Empty;
                    }
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        public bool IsVisibleTipoTasaDeCambio { get { return MonedaDelInforme == eMonedaDelInformeMM.EnBolivares || MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }
		public bool IsVisibleMonedasActivas { get { return MonedaDelInforme == eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa; } }

		private void LlenarEnumerativosMonedas() {
			ListaMonedaDelInforme = new ObservableCollection<eMonedaDelInformeMM>();
			ListaMonedaDelInforme.Clear();
			ListaMonedaDelInforme.Add(eMonedaDelInformeMM.EnBolivares);
			ListaMonedaDelInforme.Add(eMonedaDelInformeMM.EnMonedaOriginal);
			ListaMonedaDelInforme.Add(eMonedaDelInformeMM.BolivaresExpresadosEnEnDivisa);
			MonedaDelInforme = eMonedaDelInformeMM.EnMonedaOriginal;
		}

		void LlenarListaMonedasActivas() {
			ListaMonedasActivas = new Galac.Saw.Lib.clsLibSaw().ListaDeMonedasActivasParaInformes(false);
			if (ListaMonedasActivas.Count > 0) {
				Moneda = ListaMonedasActivas[0];
			}
		}
        #endregion //Metodos Generados
    } //End of class clsAnticipoPorProveedorOClienteViewModel

} //End of namespace Galac.Dbo.Uil.CAnticipo

