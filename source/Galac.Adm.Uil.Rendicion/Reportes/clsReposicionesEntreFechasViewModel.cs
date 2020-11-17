using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Brl.CajaChica;
using Galac.Adm.Uil.CajaChica.ViewModel;
using LibGalac.Aos.UI.Mvvm.Validation;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Uil.CajaChica.Reportes {

    public class clsReposicionesEntreFechasViewModel : LibInputRptViewModelBase<Rendicion> {
    #region Constantes
        public const string FechaInicialPropertyName = "FechaInicial";
        public const string FechaFinalPropertyName = "FechaFinal";
        public const string CodigoCtaBancariaCajaChicaPropertyName = "CodigoCtaBancariaCajaChica";
        public const string NombreCtaBancariaCajaChicaPropertyName = "NombreCtaBancariaCajaChica";
        public const string IsEnabledCajaChicaPropertyName = "IsEnabledCajaChica";
        public const string IsEnabledUnaPaginaPorCajaChicaPropertyName = "IsEnabledUnaPaginaPorCajaChica";
        public const string UnaPaginaPorCajaChicaPropertyName = "UnaPaginaPorCajaChica";
        public const string CantidadAImprimirPropertyName = "CantidadAImprimir";
    #endregion
    #region Variables
        private DateTime _FechaInicial { get; set; }
        private DateTime _FechaFinal { get; set; }
        public bool ImprimeUna { get; set; }        
        private string _CodigoCtaBancariaCajaChica { get; set; }
        private string _NombreCtaBancariaCajaChica { get; set; }
        private eCantidadAImprimir _CantidadAImprimir;
        private bool _UnaPaginaPorCajaChica { get; set; }
        private FkCuentaBancariaViewModel _ConexionCodigoCtaBancariaCajaChica = null;
    #endregion //Variables
        
    #region Propiedades        
        public FkCuentaBancariaViewModel ConexionCodigoCtaBancariaCajaChica {
            get {
                return _ConexionCodigoCtaBancariaCajaChica;
            }
            set {
                if (_ConexionCodigoCtaBancariaCajaChica != value) {
                    _ConexionCodigoCtaBancariaCajaChica = value;
                    RaisePropertyChanged(CodigoCtaBancariaCajaChicaPropertyName);
                }
                if (_ConexionCodigoCtaBancariaCajaChica == null) {
                    CodigoCtaBancariaCajaChica = string.Empty;
                }
            }
        }
        [LibCustomValidation("IsCajaChicaRequired")]
        [LibGridColum("Caja Chica", eGridColumType.Connection, ConnectionDisplayMemberPath = "Codigo", ConnectionModelPropertyName = "CodigoCtaBancariaCajaChica", ConnectionSearchCommandName = "ChooseCodigoCtaBancariaCajaChicaCommand")]
        public string CodigoCtaBancariaCajaChica {
            get {
                return _CodigoCtaBancariaCajaChica;
            }
            set {
                if (_CodigoCtaBancariaCajaChica != value) {
                    _CodigoCtaBancariaCajaChica = value;
                    RaisePropertyChanged(CodigoCtaBancariaCajaChicaPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoCtaBancariaCajaChica, true)) {
                        ConexionCodigoCtaBancariaCajaChica = null;
                        NombreCtaBancariaCajaChica = string.Empty;
                    } else {
                        NombreCtaBancariaCajaChica = ConexionCodigoCtaBancariaCajaChica.NombreCuenta;
                    }
                }
            }
        }

        public string NombreCtaBancariaCajaChica {
            get {
                return _NombreCtaBancariaCajaChica;
            }
            set {
                if (_NombreCtaBancariaCajaChica != value) {
                    _NombreCtaBancariaCajaChica = value;
                    RaisePropertyChanged(NombreCtaBancariaCajaChicaPropertyName);
                }
            }
        }

        public RelayCommand<string> ChooseCodigoCtaBancariaCajaChicaCommand {
            get;
            private set;
        }

        public override string DisplayName {
            get { return "Reposiciones de Caja Chica Entre Fechas";}
        }
                
        public override bool IsSSRS {
            get { return false; }
        }

        public bool UnaPaginaPorCajaChica {
            get {
                return _UnaPaginaPorCajaChica;
            }
            set {
                if (_UnaPaginaPorCajaChica != value) {
                    _UnaPaginaPorCajaChica = value;
                    RaisePropertyChanged(UnaPaginaPorCajaChicaPropertyName);
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

        public bool IsEnabledCajaChica {
            get {
                return eCantidadAImprimir.One.Equals(CantidadAImprimir);
            }
        }

        public bool IsEnabledUnaPaginaPorCajaChica {
            get {
                return eCantidadAImprimir.All.Equals(CantidadAImprimir);
            }
        }

    #endregion //Propiedades
    #region Constructores

        public clsReposicionesEntreFechasViewModel() {            
            _FechaInicial = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            _FechaFinal = LibDate.Today();
            ImprimeUna = true;
        }
    #endregion //Constructores
    #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsRendicionNav();
        }
    #endregion //Metodos Generados        

        public eCantidadAImprimir CantidadAImprimir {
            get {
                return _CantidadAImprimir;
            }
            set {
                if (_CantidadAImprimir != value) {
                    _CantidadAImprimir = value;                    
                    RaisePropertyChanged(CantidadAImprimirPropertyName);
                    RaisePropertyChanged(IsEnabledCajaChicaPropertyName);
                    RaisePropertyChanged(IsEnabledUnaPaginaPorCajaChicaPropertyName);
                    if (eCantidadAImprimir.One.Equals(_CantidadAImprimir)){
                        ImprimeUna = true;
                        UnaPaginaPorCajaChica = false;
                    } else {
                        ImprimeUna = false;
                    }                    
                }
            }
        }
        
        public eCantidadAImprimir[] ECantidadAImprimir {
            get {
                return LibEnumHelper<eCantidadAImprimir>.GetValuesInArray();
            }
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoCtaBancariaCajaChicaCommand = new RelayCommand<string>(ExecuteChooseCodigoCtaBancariaCajaChicaCommand);            
        }

        private void ExecuteChooseCodigoCtaBancariaCajaChicaCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = CriteriaParaConexionCajaChica(valCodigo);
                LibSearchCriteria vFixedCriteria = CriteriaParaConexionCajaChica();

                ConexionCodigoCtaBancariaCajaChica = ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoCtaBancariaCajaChica != null) {
                    CodigoCtaBancariaCajaChica = ConexionCodigoCtaBancariaCajaChica.Codigo;
                    _NombreCtaBancariaCajaChica = ConexionCodigoCtaBancariaCajaChica.NombreCuenta;
                } else {
                    CodigoCtaBancariaCajaChica = string.Empty;
                    _NombreCtaBancariaCajaChica = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private LibSearchCriteria CriteriaParaConexionCajaChica(string codigo) {
            LibSearchCriteria vSearchcriteria;
            vSearchcriteria = LibSearchCriteria.CreateCriteria("Saw.Gv_CuentaBancaria_B1.Codigo", codigo);
            vSearchcriteria.Add(CriteriaParaConexionCajaChica(), eLogicOperatorType.And);
            return vSearchcriteria;
        }

        private LibSearchCriteria CriteriaParaConexionCajaChica() {
            LibSearchCriteria vConsecutivoCompania;
            LibSearchCriteria vTipoCajaChica;

            vConsecutivoCompania = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"));
            vTipoCajaChica = LibSearchCriteria.CreateCriteria("EsCajaChica", true);

            vConsecutivoCompania.Add(vTipoCajaChica, eLogicOperatorType.And);
            return vConsecutivoCompania;
        }

        private ValidationResult IsCajaChicaRequired() {
            ValidationResult vResult = ValidationResult.Success;
            if (IsEnabledCajaChica && LibText.IsNullOrEmpty(CodigoCtaBancariaCajaChica)) {
                vResult = new ValidationResult("Debe seleccionar la caja chica a consultar");
            }
            return vResult;
        }
    } //End of class clsReposicionesEntreFechasViewModel

} //End of namespace Galac.Adm.Uil.CajaChica