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
using Galac.Adm.Ccl.GestionProduccion;
using Galac.Adm.Brl.GestionProduccion;
using Galac.Adm.Uil.GestionProduccion.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using System.ComponentModel.DataAnnotations;
namespace Galac.Adm.Uil.GestionProduccion.Reportes {

    public class clsOrdenDeProduccionRptViewModel : LibInputRptViewModelBase<OrdenDeProduccion> {

        #region Variables y Constantes

        private const string SeleccionarPorPropertyName = "SeleccionarPor";
        private const string GeneradoPorPropertyName = "GeneradoPor";
        private const string CodigoDeOrdenPropertyName = "CodigoDeOrden";
        private const string IsEnabledCodigoDeOrdenPropertyName = "IsEnabledCodigoDeOrden";
        private const string IsEnabledFechaPropertyName = "IsEnabledFecha";
        private const string FechaDesdePropertyName = "FechaDesde";
        private const string FechaHastaPropertyName = "FechaHasta";

        private eGeneradoPor _GeneradoPor;
        private eSeleccionarOrdenPor _SeleccionarPor;
        private string _CodigoDeOrden;
        private DateTime _FechaDesde;
        private DateTime _FechaHasta;
        private bool _IsEnabledCodigoDeOrden;
        private bool _IsEnabledFecha;
        private FkOrdenDeProduccionViewModel _ConexionCodigoDeOrden = null;

        #endregion //Variables y Constantes

        #region Propiedades

        public FkOrdenDeProduccionViewModel ConexionCodigoDeOrden {
            get {
                return _ConexionCodigoDeOrden;
            }
            set {
                if (_ConexionCodigoDeOrden != value) {
                    _ConexionCodigoDeOrden = value;
                    RaisePropertyChanged(CodigoDeOrdenPropertyName);
                }
                if (_ConexionCodigoDeOrden == null) {
                    _CodigoDeOrden = string.Empty;
                }
            }
        }

        [LibCustomValidation("IsCodigoDeOrdenRequired")]
        public string CodigoDeOrden {
            get {
                return _CodigoDeOrden;
            }
            set {
                if (_CodigoDeOrden != value) {
                    _CodigoDeOrden = value;
                    RaisePropertyChanged(CodigoDeOrdenPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoDeOrden, true)) {
                        ConexionCodigoDeOrden = null;
                    }
                }
            }
        }

        public eGeneradoPor GeneradoPor {
            get {
                return _GeneradoPor;
            }
            set {
                if (_GeneradoPor != value) {
                    _GeneradoPor = value;
                    RaisePropertyChanged(GeneradoPorPropertyName);
                    RaisePropertyChanged(IsEnabledCodigoDeOrdenPropertyName);
                    RaisePropertyChanged(IsEnabledFechaPropertyName);
                    if (eGeneradoPor.Orden.Equals(_SeleccionarPor)) {
                        IsEnabledCodigoDeOrden = true;
                        IsEnabledFecha = false;
                    } else {
                        IsEnabledCodigoDeOrden = false;
                        IsEnabledFecha = true;
                        CodigoDeOrden = string.Empty;
                    }
                }
            }
        }        public eSeleccionarOrdenPor SeleccionarPor {
            get {
                return _SeleccionarPor;
            }
            set {
                if (_SeleccionarPor != value) {
                    _SeleccionarPor = value;
                    RaisePropertyChanged(SeleccionarPorPropertyName);
                    RaisePropertyChanged(IsEnabledCodigoDeOrdenPropertyName);
                    RaisePropertyChanged(IsEnabledFechaPropertyName);
                    if (eSeleccionarOrdenPor.NumeroDeOrden.Equals(_SeleccionarPor)) {
                        IsEnabledCodigoDeOrden = true;
                        IsEnabledFecha = false;
                    } else {
                        IsEnabledCodigoDeOrden = false;
                        IsEnabledFecha = true;
                        CodigoDeOrden = string.Empty;
                    }
                }
            }
        }

        public bool IsEnabledCodigoDeOrden {
            get {
                return _IsEnabledCodigoDeOrden;
            }
            set {
                if (_IsEnabledCodigoDeOrden != value) {
                    _IsEnabledCodigoDeOrden = value;
                    RaisePropertyChanged(IsEnabledCodigoDeOrdenPropertyName);
                }
            }
        }

        public bool IsEnabledFecha {
            get {
                return _IsEnabledFecha;
            }
            set {
                if (_IsEnabledFecha != value) {
                    _IsEnabledFecha = value;
                    RaisePropertyChanged(IsEnabledFechaPropertyName);
                }
            }
        }

        public DateTime FechaDesde {
            get {
                return _FechaDesde;
            }
            set {
                if (_FechaDesde != value) {
                    _FechaDesde = value;
                    RaisePropertyChanged(FechaDesdePropertyName);
                    if (LibDate.F1IsGreaterThanF2(FechaDesde, FechaHasta)) {
                        FechaHasta = FechaDesde;
                        RaisePropertyChanged(FechaHastaPropertyName);
                    }
                }
            }
        }

        public DateTime FechaHasta {
            get {
                return _FechaHasta;
            }
            set {
                if (_FechaHasta != value) {
                    _FechaHasta = value;
                    RaisePropertyChanged(FechaHastaPropertyName);
                    if (LibDate.F1IsLessThanF2(FechaHasta, FechaDesde)) {
                        FechaDesde = FechaHasta;
                        RaisePropertyChanged(FechaHastaPropertyName);
                    }
                }
            }
        }

        public override string DisplayName {
            //get { return "Orden de Producción"; }
            get { return "PreCierre Orden de Producción"; }
        }

        public override bool IsSSRS {
            get { return false; }
        }

        public RelayCommand<string> ChooseCodigoDeOrdenCommand {
            get;
            private set;
        }

        #endregion //Propiedades

        #region Constructores
        
        public clsOrdenDeProduccionRptViewModel() {
            _GeneradoPor = eGeneradoPor.Orden;
            _SeleccionarPor = eSeleccionarOrdenPor.NumeroDeOrden;
            _CodigoDeOrden = string.Empty;
            _FechaDesde = DateTime.Today;
            _FechaHasta = DateTime.Today;
            _IsEnabledFecha = false;
            _IsEnabledCodigoDeOrden = true;
        }

        #endregion //Constructores

        #region Metodos

        public eGeneradoPor[] EGeneradoPor {
            get {
                return LibEnumHelper<eGeneradoPor>.GetValuesInArray();
            }
        }        
        public eSeleccionarOrdenPor[] ESeleccionarPor {
            get {
                return LibEnumHelper<eSeleccionarOrdenPor>.GetValuesInArray();
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoDeOrdenCommand = new RelayCommand<string>(ExecuteChooseCodigoDeOrdenCommand);
        }

        private void ExecuteChooseCodigoDeOrdenCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Adm.Gv_OrdenDeProduccion_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Adm.Gv_OrdenDeProduccion_B1.StatusOp", (int)Galac.Adm.Ccl.GestionProduccion.eTipoStatusOrdenProduccion.Ingresada), eLogicOperatorType.And);
                ConexionCodigoDeOrden = ChooseRecord<FkOrdenDeProduccionViewModel>("Orden de Producción", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoDeOrden != null) {
                    CodigoDeOrden = ConexionCodigoDeOrden.Codigo;
                } else {
                    CodigoDeOrden = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Galac.Adm.Rpt.GestionProduccion.clsListaDeMaterialesDeInventarioAProducir.ReportName);
            }
        }

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsOrdenDeProduccionNav();
        }

        private ValidationResult IsCodigoDeOrdenRequired() {
            ValidationResult vResult = ValidationResult.Success;
            if (IsEnabledCodigoDeOrden && LibText.IsNullOrEmpty(CodigoDeOrden)) {
                vResult = new ValidationResult("Debe seleccionar una Orden de Producción a consultar.");
            }
            return vResult;
        }

        #endregion //Metodos

    } //End of class clsOrdenDeProduccionRptViewModel
} //End of namespace Galac.Adm.Uil. GestionProduccion

