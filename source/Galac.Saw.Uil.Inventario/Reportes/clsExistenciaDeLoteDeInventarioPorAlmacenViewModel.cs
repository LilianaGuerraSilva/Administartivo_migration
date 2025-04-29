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
using LibGalac.Aos.Uil;

namespace Galac.Saw.Uil.Inventario.Reportes {

    public class clsExistenciaDeLoteDeInventarioPorAlmacenViewModel :  LibInputRptViewModelBase<LoteDeInventario> {
        #region Constantes
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string CodigoArticuloPropertyName = "CodigoArticulo";
        public const string FechaInicialPropertyName = "FechaInicial";
        public const string FechaFinalPropertyName = "FechaFinal";
        public const string CodigoAlmacenGenericoPropertyName = "CodigoAlmacenGenerico";

        #endregion
        #region Variables
        private FkLoteDeInventarioViewModel _ConexionCodigoLote = null;
        private FkArticuloInventarioRptViewModel _ConexionCodigoArticulo = null;
        private FkAlmacenViewModel _ConexionCodigoAlmacenGenerico = null;
        private eSeleccionAlmacen _SeleccionAlmacen;
        private string _CodigoLote;
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private string _CodigoArticulo;
        private string _CodigoAlmacen;

        #endregion //Variables
        #region Propiedades
        public override string DisplayName {
            get { return "Existencia de Lote de Inventario por Almacén"; }
        }
        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }
        #region Propiedades

        public override bool IsSSRS { get { return false; } }

        [LibRequired(ErrorMessage = "El Código del Artículo es requerido.")]
        public string CodigoArticulo {
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

        public DateTime FechaInicial {
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

        public DateTime FechaFinal {
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
		
        public string CodigoAlmacenGenerico {
            get {
                return _CodigoAlmacen;
            }
            set {
                if (_CodigoAlmacen != value) {
                    _CodigoAlmacen = value;
                    RaisePropertyChanged(CodigoAlmacenGenericoPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoAlmacenGenerico, true)) {
                        ConexionAlmacenGenerico = null;
                    }
                }
            }
        }

        [LibRequired(ErrorMessage = "Debe seleccionar el Almacén.")]
        public eSeleccionAlmacen SeleccionAlmacen {
            get {
                return _SeleccionAlmacen;
            }
            set {
                if (_SeleccionAlmacen != value) {
                    _SeleccionAlmacen = value;
                    if (_SeleccionAlmacen == eSeleccionAlmacen.Todos) {
                        CodigoAlmacenGenerico = string.Empty;
                    } else if (_SeleccionAlmacen == eSeleccionAlmacen.UnAlmacen) {
                        CodigoAlmacenGenerico = string.Empty;
                    }
                    RaisePropertyChanged(CodigoAlmacenGenericoPropertyName);
                    RaisePropertyChanged("IsVisibleCodigoAlmacen");
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
                } else {
                    CodigoLote = ConexionCodigoLote.CodigoLote;  
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
                } else {
                    CodigoArticulo = _ConexionCodigoArticulo.Codigo;
                }
            }
        }

        public FkAlmacenViewModel ConexionAlmacenGenerico {
            get {
                return _ConexionCodigoAlmacenGenerico;
            }
            set {
                if (_ConexionCodigoAlmacenGenerico != value) {
                    _ConexionCodigoAlmacenGenerico = value;
                    RaisePropertyChanged(CodigoAlmacenGenericoPropertyName);
                }
                if (_ConexionCodigoAlmacenGenerico == null) {
                    CodigoAlmacenGenerico = string.Empty;
                } else {
                    CodigoAlmacenGenerico = ConexionAlmacenGenerico.Codigo;
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

        public RelayCommand<string> ChooseAlmacenGenericoCommand {
            get;
            private set;
        }

        public eSeleccionAlmacen[] ArraySeleccionAlmacen {
            get {
                return LibEnumHelper<eSeleccionAlmacen>.GetValuesInArray();
            }
        }
        #endregion //Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsExistenciaDeLoteDeInventarioPorAlmacenViewModel() {
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
            ChooseAlmacenGenericoCommand = new RelayCommand<string>(ExecuteChooseCodigoAlmacenGenericoCommand);
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
                LibSearchCriteria vDefaultCriteria2 = LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.TipoArticuloInv", eTipoArticuloInv.Lote);
                vDefaultCriteria2.Add(LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.TipoArticuloInv", eTipoArticuloInv.LoteFechadeVencimiento), eLogicOperatorType.Or);
                vDefaultCriteria2.Add(LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.TipoArticuloInv", eTipoArticuloInv.LoteFechadeElaboracion), eLogicOperatorType.Or);
                vDefaultCriteria.Add(vDefaultCriteria2, eLogicOperatorType.And);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_ArticuloInventario_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                ConexionCodigoArticulo = ChooseRecord<FkArticuloInventarioRptViewModel>("Artículo Inventario", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoArticulo != null) {
                    CodigoArticulo = ConexionCodigoArticulo.Codigo;
                } else {
                    CodigoArticulo = string.Empty;
                }
                CodigoLote = string.Empty;
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }
        private void ExecuteChooseCodigoAlmacenGenericoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;

                }

                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Saw.Gv_Almacen_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionAlmacenGenerico = ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteria, vFixedCriteria, "Codigo");
                if (ConexionAlmacenGenerico != null) {
                    CodigoAlmacenGenerico = ConexionAlmacenGenerico.Codigo;
                } else { 
                    CodigoAlmacenGenerico = String.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

        public bool IsVisibleCodigoAlmacen {
            get {
                return SeleccionAlmacen == eSeleccionAlmacen.UnAlmacen;
            }
        }

        #endregion //Metodos Generados

    } //End of class clsExistenciaDeLoteDeInventarioPorAlmacenViewModel

} //End of namespace Galac.Saw.Uil.Inventario

