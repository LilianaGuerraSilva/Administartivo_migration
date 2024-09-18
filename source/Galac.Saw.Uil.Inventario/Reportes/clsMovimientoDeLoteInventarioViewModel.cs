using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsMovimientoDeLoteInventarioViewModel : LibInputRptViewModelBase<LoteDeInventario> {
        #region Constantes
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string FechaInicialPropertyName = "FechaInicial";
        public const string FechaFinalPropertyName = "FechaFinal";
        #endregion
        #region Variables
        private FkLoteDeInventarioViewModel _ConexionCodigoLote = null;
        private FkArticuloInventarioRptViewModel _ConexionCodigoArticulo = null;
        private string _CodigoLote;
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private string _CodigoArticulo;
        #endregion //Variables
        #region Propiedades
        public override string DisplayName {

            get { return "Movimientos de Lote de Inventario";}
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public override bool IsSSRS { get { return false; } }

        [LibRequired(ErrorMessage = "El Código del Artículo es requerido.")]
        public string  CodigoArticulo {
            get {
                return _CodigoArticulo;
            }
            set {
                if (_CodigoArticulo != value) {
                    _CodigoArticulo = value;                    
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoArticulo, true)) {
                        ConexionCodigoArticulo = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "El Lote es requerido.")]
        public string CodigoLote {
            get {
                return _CodigoLote;
            }
            set {
                if (_CodigoLote != value) {
                    _CodigoLote = value;
                    RaisePropertyChanged(CodigoLotePropertyName);
                    if (LibString.IsNullOrEmpty(CodigoLote, true)) {
                        ConexionCodigoLote = null;
                    }
                }
            }
        }

        public DateTime  FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value) {
                    _FechaInicial = value;                   
                    RaisePropertyChanged(FechaInicialPropertyName);
                }
            }
        }


        public DateTime  FechaFinal {
            get {
                return _FechaFinal;
            }
            set {
                if (_FechaFinal != value) {
                    _FechaFinal = value;                   
                    RaisePropertyChanged(FechaFinalPropertyName);
                }
            }
        }
       
        public FkLoteDeInventarioViewModel ConexionCodigoLote {
            get {
                return _ConexionCodigoLote;
            }
            set {
                if (_ConexionCodigoLote != value) {
                    _ConexionCodigoLote = value;
                    RaisePropertyChanged(CodigoLotePropertyName);
                }
                if (_ConexionCodigoLote == null) {
                    CodigoLote = string.Empty;
                }
            }
        }

        
        public FkArticuloInventarioRptViewModel ConexionCodigoArticulo {
            get {
                return _ConexionCodigoArticulo;
            }
            set {
                if (_ConexionCodigoArticulo != value) {
                    _ConexionCodigoArticulo = value;
                    RaisePropertyChanged(CodigoArticuloPropertyName);
                }
                if (_ConexionCodigoArticulo == null) {
                    CodigoArticulo = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoLoteCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoArticuloCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public clsMovimientoDeLoteInventarioViewModel() {
            FechaInicial = LibDate.Today();
            FechaFinal = LibDate.AddDays(LibDate.Today(), 30);
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsLoteDeInventarioNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoLoteCommand = new RelayCommand<string>(ExecuteChooseCodigoLoteCommand);
            ChooseCodigoArticuloCommand = new RelayCommand<string>(ExecuteChooseCodigoArticuloCommand);
        }
        
 		
        private void ExecuteChooseCodigoLoteCommand(string valCodigoLote) {
            try {
                if (valCodigoLote == null) {
                    valCodigoLote = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoLote", valCodigoLote);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("CodigoArticulo", CodigoArticulo), eLogicOperatorType.And);
                ConexionCodigoLote = ChooseRecord<FkLoteDeInventarioViewModel>("Lote de Inventario", vDefaultCriteria, vFixedCriteria, "FechaDeVencimiento, FechaDeElaboracion, CodigoLote");
                if (ConexionCodigoLote != null) {
                    CodigoLote = ConexionCodigoLote.CodigoLote;
                } else {
                    CodigoLote = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        private void ExecuteChooseCodigoArticuloCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_ArticuloInventario_B1.Codigo", valCodigo);
                vDefaultCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.TipoArticuloInv", eTipoArticuloInv.LoteFechadeVencimiento), eLogicOperatorType.And);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioRptViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticulo != null) {
                    CodigoArticulo = ConexionCodigoArticulo.Codigo;
                } else {
                    CodigoArticulo = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }       
    #endregion //Metodos Generados
    } //End of class clsMovimientoDeLoteInventarioViewModel
} //End of namespace Galac.Saw.Uil.Inventario

