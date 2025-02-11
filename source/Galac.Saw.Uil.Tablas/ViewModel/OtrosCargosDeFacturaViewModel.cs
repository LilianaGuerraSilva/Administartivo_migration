using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Ccl.Tablas;
using LibGalac.Aos.Uil;
using Galac.Saw.Uil.Contabilizacion.ViewModel;
using Galac.Contab.Ccl.WinCont;

namespace Galac.Saw.Uil.Tablas.ViewModel {
    public class OtrosCargosDeFacturaViewModel : LibInputViewModelMfc<OtrosCargosDeFactura> {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string DescripcionPropertyName = "Descripcion";
        public const string StatusPropertyName = "Status";
        public const string SeCalculaEnBaseaPropertyName = "SeCalculaEnBasea";
        public const string MontoPropertyName = "Monto";
        public const string BaseFormulaPropertyName = "BaseFormula";
        public const string PorcentajeSobreBasePropertyName = "PorcentajeSobreBase";
        public const string SustraendoPropertyName = "Sustraendo";
        public const string ComoAplicaAlTotalFacturaPropertyName = "ComoAplicaAlTotalFactura";
        public const string CuentaContableOtrosCargosPropertyName = "CuentaContableOtrosCargos";
        public const string CuentaContableOtrosCargosDescripcionPropertyName = "CuentaContableOtrosCargosDescripcion";
        public const string PorcentajeComisionPropertyName = "PorcentajeComision";
        public const string ExcluirDeComisionPropertyName = "ExcluirDeComision";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string IsEnabledExcluirDeComisionPropertyName = "IsEnabledExcluirDeComision";
        public const string IsVisibleMontoPropertyName = "IsVisibleMonto";
        public const string IsVisibleBaseFormulaPropertyName = "IsVisibleBaseFormula";
        public const string IsVisibleSeCalculaEnBaseaPropertyName = "IsVisibleSeCalculaEnBasea";
        public const string IsEnabledCuentaContableOtrosCargosPropertyName = "IsEnabledCuentaContableOtrosCargos";
        public const string IsVisibleEtiquetaFormulaPropertyName = "IsVisibleEtiquetaFormula";

        #endregion
        # region Variables
        private FkCuentaViewModel _ConexionCuentaContableOtrosCargos = null;
        #endregion

        #region Propiedades

        public override string ModuleName {
            get { return "Otros Cargos de Factura"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código del Cargo es requerido.")]
        [LibGridColum("Código del Cargo", MaxLength=15)]
        public string  Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", MaxLength=255)]
        public string  Descripcion {
            get {
                return Model.Descripcion;
            }
            set {
                if (Model.Descripcion != value) {
                    Model.Descripcion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
            }
        }

        public eStatusOtrosCargosyDescuentosDeFactura  Status {
            get {
                return Model.StatusAsEnum;
            }
            set {
                if (Model.StatusAsEnum != value) {
                    Model.StatusAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusPropertyName);
                }
            }
        }

        public eBaseCalculoOtrosCargosDeFactura  SeCalculaEnBasea {
            get {
                return Model.SeCalculaEnBaseaAsEnum;
            }
            set {
                if (Model.SeCalculaEnBaseaAsEnum != value) {
                    Model.SeCalculaEnBaseaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(SeCalculaEnBaseaPropertyName);
                    RaisePropertyChanged(IsVisibleMontoPropertyName);
                    RaisePropertyChanged(IsVisibleSeCalculaEnBaseaPropertyName);
                    RaisePropertyChanged(IsVisibleEtiquetaFormulaPropertyName);
                }
            }
        }

        public decimal  Monto {
            get {
                return Model.Monto;
            }
            set {
                if (Model.Monto != value) {
                    Model.Monto = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoPropertyName);
                }
            }
        }

        public eBaseFormulaOtrosCargosDeFactura  BaseFormula {
            get {
                return Model.BaseFormulaAsEnum;
            }
            set {
                if (Model.BaseFormulaAsEnum != value) {
                    Model.BaseFormulaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(BaseFormulaPropertyName);
                }
            }
        }

        [LibCustomValidation("PorcentajeSobreBaseValidating")]
        public decimal  PorcentajeSobreBase {
            get {
                return Model.PorcentajeSobreBase;
            }
            set {
                if (Model.PorcentajeSobreBase != value) {
                    Model.PorcentajeSobreBase = value;
                    RaisePropertyChanged(PorcentajeSobreBasePropertyName);
                }
            }
        }

        public decimal  Sustraendo {
            get {
                return Model.Sustraendo;
            }
            set {
                if (Model.Sustraendo != value) {
                    Model.Sustraendo = value;
                    IsDirty = true;
                    RaisePropertyChanged(SustraendoPropertyName);
                }
            }
        }

        public eComoAplicaOtrosCargosDeFactura  ComoAplicaAlTotalFactura {
            get {
                return Model.ComoAplicaAlTotalFacturaAsEnum;
            }
            set {
                if (Model.ComoAplicaAlTotalFacturaAsEnum != value) {
                    Model.ComoAplicaAlTotalFacturaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComoAplicaAlTotalFacturaPropertyName);
                }
            }
        }
        [LibCustomValidation("CuentaContableOtrosCargosValidating")]
        public string  CuentaContableOtrosCargos {
            get {
                return Model.CuentaContableOtrosCargos;
            }
            set {
                if (Model.CuentaContableOtrosCargos != value) {
                    Model.CuentaContableOtrosCargos = value;
                    IsDirty = true;
                    RaisePropertyChanged(CuentaContableOtrosCargosPropertyName);
                    RaisePropertyChanged(CuentaContableOtrosCargosDescripcionPropertyName);
                }
            }
        }

        public string CuentaContableOtrosCargosDescripcion {
            get;
            set;
        }


        [LibCustomValidation("PorcentajeComisionValidating")]
        public decimal  PorcentajeComision {
            get {
                return Model.PorcentajeComision;
            }
            set {
                if (Model.PorcentajeComision != value) {
                    Model.PorcentajeComision = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeComisionPropertyName);
                }
            }
        }

        public bool  ExcluirDeComision {
            get {
                return Model.ExcluirDeComisionAsBool;
            }
            set {
                if (Model.ExcluirDeComisionAsBool != value) {
                    Model.ExcluirDeComisionAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ExcluirDeComisionPropertyName);
                    RaisePropertyChanged(IsEnabledExcluirDeComisionPropertyName);
                }
            }
        }

        public string  NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime  FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eStatusOtrosCargosyDescuentosDeFactura[] ArrayStatusOtrosCargosyDescuentosDeFactura {
            get {
                return LibEnumHelper<eStatusOtrosCargosyDescuentosDeFactura>.GetValuesInArray();
            }
        }

        public eBaseCalculoOtrosCargosDeFactura[] ArrayBaseCalculoOtrosCargosDeFactura {
            get {
                return LibEnumHelper<eBaseCalculoOtrosCargosDeFactura>.GetValuesInArray();
            }
        }

        public eBaseFormulaOtrosCargosDeFactura[] ArrayBaseFormulaOtrosCargosDeFactura {
            get {
                return LibEnumHelper<eBaseFormulaOtrosCargosDeFactura>.GetValuesInArray();
            }
        }

        public eComoAplicaOtrosCargosDeFactura[] ArrayComoAplicaOtrosCargosDeFactura {
            get {
                return LibEnumHelper<eComoAplicaOtrosCargosDeFactura>.GetValuesInArray();
            }
        }

        public RelayCommand<string> ChooseCuentaContableCommand {
            get;
            private set;
        }


        #endregion //Propiedades
        #region Constructores
        public OtrosCargosDeFacturaViewModel()
            : this(new OtrosCargosDeFactura(), eAccionSR.Insertar) {
        }
        public OtrosCargosDeFacturaViewModel(OtrosCargosDeFactura initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(OtrosCargosDeFactura valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCuentaContableCommand = new RelayCommand<string>(ExecuteChooseCuentaContableCommand);
        }

        private void ExecuteChooseCuentaContableCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("TieneSubCuentas", false);
                LibSearchCriteria vActivoFijoCriteria = LibSearchCriteria.CreateCriteria("EsActivoFijo", false);
                vFixedCriteria.Add(vActivoFijoCriteria, eLogicOperatorType.And);
                ConexionCuentaContableOtrosCargos = ChooseRecord<FkCuentaViewModel>("Cuenta", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public FkCuentaViewModel ConexionCuentaContableOtrosCargos {
            get {
                return _ConexionCuentaContableOtrosCargos;
            }
            set {
                if (_ConexionCuentaContableOtrosCargos != value) {
                    _ConexionCuentaContableOtrosCargos = value;
                    if (ConexionCuentaContableOtrosCargos != null) {
                        CuentaContableOtrosCargos = ConexionCuentaContableOtrosCargos.Codigo;
                        CuentaContableOtrosCargosDescripcion = ConexionCuentaContableOtrosCargos.Descripcion;
                    }
                    RaisePropertyChanged(CuentaContableOtrosCargosPropertyName);
                    RaisePropertyChanged(CuentaContableOtrosCargosDescripcionPropertyName);
                }
                if (ConexionCuentaContableOtrosCargos == null) {
                    CuentaContableOtrosCargos = string.Empty;
                    CuentaContableOtrosCargosDescripcion = string.Empty;
                    RaisePropertyChanged(CuentaContableOtrosCargosPropertyName);
                    RaisePropertyChanged(CuentaContableOtrosCargosDescripcionPropertyName);
                }
            }
        }

        protected override OtrosCargosDeFactura FindCurrentRecord(OtrosCargosDeFactura valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("Codigo", valModel.Codigo, 15);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "OtrosCargosDeFacturaGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<OtrosCargosDeFactura>, IList<OtrosCargosDeFactura>> GetBusinessComponent() {
            return new clsOtrosCargosDeFacturaNav();
        }

        public bool IsVisibleMonto {
            get {
                bool vResult = false;
                if ((SeCalculaEnBasea == eBaseCalculoOtrosCargosDeFactura.IndicarMonto)) {
                    vResult = true;
                }
                return vResult;
            }
        }

        public bool IsVisibleEtiquetaFormula {
            get {
                bool vResult = true;
                if ((SeCalculaEnBasea != eBaseCalculoOtrosCargosDeFactura.Formula)) {
                    vResult = false;
                }
                return vResult;
            }
        }

        public bool IsVisibleSeCalculaEnBasea {
            get {
                bool vResult = true;
                if ((SeCalculaEnBasea == eBaseCalculoOtrosCargosDeFactura.IndicarMonto)) {
                    vResult = false;
                    RaisePropertyChanged(BaseFormulaPropertyName);
                    RaisePropertyChanged(SustraendoPropertyName);
                    RaisePropertyChanged(PorcentajeSobreBasePropertyName);
                    RaisePropertyChanged(IsVisibleEtiquetaFormulaPropertyName);
                    RaisePropertyChanged(IsVisibleMontoPropertyName);
                } else if ((SeCalculaEnBasea == eBaseCalculoOtrosCargosDeFactura.MontoFijo)) {
                    vResult = false;
                    RaisePropertyChanged(BaseFormulaPropertyName);
                    RaisePropertyChanged(SustraendoPropertyName);
                    RaisePropertyChanged(PorcentajeSobreBasePropertyName);
                    RaisePropertyChanged(IsVisibleEtiquetaFormulaPropertyName);
                    RaisePropertyChanged(IsVisibleMontoPropertyName);
                }
                return vResult;
            }
        }

        public bool IsEnabledExcluirDeComision {
            get {
                return IsEnabled && !ExcluirDeComision;
            }
        }

        public bool IsEnabledCuentaContableOtrosCargos {
            get {
                return IsEnabled && (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaModuloDeContabilidad")); ;
            }
        }

        private ValidationResult PorcentajeSobreBaseValidating()    {
            ValidationResult vResult = ValidationResult.Success;
            if (SeCalculaEnBasea == eBaseCalculoOtrosCargosDeFactura.Formula && (PorcentajeSobreBase < 0) || (PorcentajeSobreBase > 1000)) {
                vResult = new ValidationResult("El Porcentaje a Aplicar Sobre la Base seleccionada debe ser mayor a 0 y menor igual a 1000");
            } else {
                return ValidationResult.Success;
            }
            return vResult;
        }

        private ValidationResult CuentaContableOtrosCargosValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaModuloDeContabilidad")) {
                if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                    return ValidationResult.Success;
                } else {
                    if (LibString.IsNullOrEmpty(CuentaContableOtrosCargos)) {
                        vResult = new ValidationResult("La Cuenta de Otros Cargos y Descuentos es requerida.");
                    }
                }
            }
            return vResult;
        }

        private ValidationResult PorcentajeComisionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((PorcentajeComision < 0) || (PorcentajeComision > 100)) {
                vResult = new ValidationResult("El Porcentaje Comisión debe ser mayor a 0 y menor que 100");
            } else {
                return ValidationResult.Success;
            }
            return vResult;
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ReloadRelatedConnectionsCuenta();

        }

        private void ReloadRelatedConnectionsCuenta() {
            if (!LibString.IsNullOrEmpty(CuentaContableOtrosCargos))
                ConexionCuentaContableOtrosCargos = FirstConnectionRecordOrDefault<FkCuentaViewModel>("Cuenta", SearchCriteriaConexionCuenta(CuentaContableOtrosCargos));
        }

        private LibSearchCriteria SearchCriteriaConexionCuenta(string codigo) {
            LibSearchCriteria vSearchcriteria;
            LibSearchCriteria vTituloCriteria;
            LibSearchCriteria vActivoFijoCriteria;
            vTituloCriteria = LibSearchCriteria.CreateCriteria("TieneSubCuentas", false);
            vActivoFijoCriteria = LibSearchCriteria.CreateCriteria("EsActivoFijo", false);

            vSearchcriteria = LibSearchCriteria.CreateCriteria("Codigo", codigo);
            vSearchcriteria.Add(vTituloCriteria, eLogicOperatorType.And);
            vSearchcriteria.Add(vActivoFijoCriteria, eLogicOperatorType.And);
            return vSearchcriteria;
        }
        #endregion //Metodos Generados
    } //End of class otrosCargosDeFacturaViewModel

} //End of namespace Galac.Saw.Uil.Tablas

