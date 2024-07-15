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

namespace Galac.Adm.Uil. GestionProduccion.Reportes {

    public class clsRequisicionDeMaterialesViewModel : LibInputRptViewModelBase<OrdenDeProduccion> {

        #region Variables y Constantes

        private const string GeneradoPorPropertyName = "GeneradoPor";
        private const string CodigoDeOrdenPropertyName = "CodigoDeOrden";
        private const string IsEnabledCodigoDeOrdenPropertyName = "IsEnabledCodigoDeOrden";
        private const string IsEnabledFechaPropertyName = "IsEnabledFecha";
        private const string FechaInicialPropertyName = "FechaInicial";
        private const string FechaFinalPropertyName = "FechaFinal";
        private const string MostrarSoloExistenciaInsuficientePropertyName = "MostrarSoloExistenciaInsuficiente";

        private Galac.Adm.Ccl.GestionProduccion.eGeneradoPor _GeneradoPor;
        private string _CodigoDeOrden;
        private bool _IsEnabledCodigoDeOrden;
        private bool _IsEnabledFecha;
        private DateTime _FechaInicial;
        private DateTime _FechaFinal;
        private bool _MostrarSoloExistenciaInsuficiente;
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
                    if (eGeneradoPor.Orden.Equals(_GeneradoPor)) {
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

        public DateTime FechaInicial {
            get {
                return _FechaInicial;
            }
            set {
                if (_FechaInicial != value) {
                    _FechaInicial = value;
                    RaisePropertyChanged(FechaInicialPropertyName);
                    if (LibDate.F1IsGreaterThanF2(FechaInicial, FechaFinal)) {
                        FechaFinal = FechaInicial;
                        RaisePropertyChanged(FechaFinalPropertyName);
                    }
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
                    if (LibDate.F1IsLessThanF2(FechaFinal, FechaInicial)) {
                        FechaInicial = FechaFinal;
                        RaisePropertyChanged(FechaFinalPropertyName);
                    }
                }
            }
        }

        public bool MostrarSoloExistenciaInsuficiente {
            get {
                return _MostrarSoloExistenciaInsuficiente;
            }
            set {
                if (_MostrarSoloExistenciaInsuficiente != value) {
                    _MostrarSoloExistenciaInsuficiente = value;
                    RaisePropertyChanged(MostrarSoloExistenciaInsuficientePropertyName);
                }
            }
        }

        public override string DisplayName {
            get { return "Requisición de Materiales"; }
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

        public clsRequisicionDeMaterialesViewModel() {
            _GeneradoPor = eGeneradoPor.Orden;
            _CodigoDeOrden = string.Empty;
            _FechaInicial = DateTime.Today;
            _FechaFinal = DateTime.Today;
            _IsEnabledFecha = false;
            _IsEnabledCodigoDeOrden = true;
            _MostrarSoloExistenciaInsuficiente = false;

        }

        #endregion //Constructores

        #region Metodos 

        public eGeneradoPor[] EGeneradoPor {
            get {
                return LibEnumHelper<eGeneradoPor>.GetValuesInArray();
            }
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
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Galac.Adm.Rpt.GestionProduccion.clsListaDeMaterialesDeSalida.ReportName);
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoDeOrdenCommand = new RelayCommand<string>(ExecuteChooseCodigoDeOrdenCommand);
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

    } //End of class clsRequisicionDeMaterialesViewModel

} //End of namespace Galac.Adm.Uil. GestionProduccion

