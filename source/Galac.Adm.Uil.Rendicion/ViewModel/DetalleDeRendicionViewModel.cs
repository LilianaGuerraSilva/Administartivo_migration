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
using Galac.Adm.Brl.CajaChica;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Ccl.Banco;
using Galac.Comun.Ccl.Impuesto;
using Galac.Comun.Brl.Impuesto;
using Galac.Comun.Ccl.SttDef;

namespace Galac.Adm.Uil.CajaChica.ViewModel {
    public class DetalleDeRendicionViewModel : LibInputDetailViewModelMfc<DetalleDeRendicion> {
        #region Constantes
        public const string NumeroDocumentoPropertyName = "NumeroDocumento";
        public const string NumeroControlPropertyName = "NumeroControl";
        public const string FechaPropertyName = "Fecha";
        public const string CodigoProveedorPropertyName = "CodigoProveedor";
        public const string NombreProveedorPropertyName = "NombreProveedor";
        public const string MontoExentoPropertyName = "MontoExento";
        public const string MontoGravablePropertyName = "MontoGravable";
        public const string MontoIVAPropertyName = "MontoIVA";
        public const string MontoTotalPropertyName = "MontoTotal";
        public const string MontoRetencionPropertyName = "MontoRetencion";
        public const string AplicaParaLibroDeComprasPropertyName = "AplicaParaLibroDeCompras";
        public const string ObservacionesCxPPropertyName = "ObservacionesCxP";
        public const string LabelIVAPropertyName = "LabelIVA";
        public const string ValidoAsBoolPropertyName = "ValidoAsBool";
        public const string GeneradaPorPropertyName = "GeneradaPor";
        public const string AplicaIvaAlicuotaEspecialPropertyName = "AplicaIvaAlicuotaEspecial";
        public const string MontoGravableAlicuotaEspecial1PropertyName = "MontoGravableAlicuotaEspecial1";
        public const string MontoIVAAlicuotaEspecial1PropertyName = "MontoIVAAlicuotaEspecial1";
        public const string PorcentajeIvaAlicuotaEspecial1PropertyName = "PorcentajeIvaAlicuotaEspecial1";
        public const string MontoGravableAlicuotaEspecial2PropertyName = "MontoGravableAlicuotaEspecial2";
        public const string MontoIVAAlicuotaEspecial2PropertyName = "MontoIVAAlicuotaEspecial2";
        public const string PorcentajeIvaAlicuotaEspecial2PropertyName = "PorcentajeIvaAlicuotaEspecial2";
        decimal _Alicuota1;
        decimal _Alicuota2;
        DateTime _FechaDesdeAlicuota;
        DateTime _FechaHastaAlicuota;
        #endregion
        #region Variables
        private FkProveedorViewModel _ConexionCodigoProveedor = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Detalle De Rendicion"; }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        public int ConsecutivoRendicion {
            get {
                return Model.ConsecutivoRendicion;
            }
            set {
                if (Model.ConsecutivoRendicion != value) {
                    Model.ConsecutivoRendicion = value;
                }
            }
        }

        public int ConsecutivoRenglon {
            get {
                return Model.ConsecutivoRenglon;
            }
            set {
                if (Model.ConsecutivoRenglon != value) {
                    Model.ConsecutivoRenglon = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Número del Documento es requerido.")]
        [LibGridColum("N° Documento", Width = 150)]
        public string NumeroDocumento {
            get {
                return Model.NumeroDocumento;
            }
            set {
                if (Model.NumeroDocumento != value) {
                    Model.NumeroDocumento = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroDocumentoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Número de Control es requerido.")]
        [LibGridColum("N° Control")]
        public string NumeroControl {
            get {
                return Model.NumeroControl;
            }
            set {
                if (Model.NumeroControl != value) {
                    Model.NumeroControl = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroControlPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Fecha es requerido.")]
        [LibCustomValidation("FechaValidating")]
        [LibGridColum("Fecha", eGridColumType.DatePicker, Width = 80)]
        public DateTime Fecha {
            get {
                return Model.Fecha;
            }
            set {
                if (Model.Fecha != value) {
                    Model.Fecha = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaPropertyName);
                    RaisePropertyChanged(AplicaIvaAlicuotaEspecialPropertyName);
                    RaisePropertyChanged("IsVisibleDeAlicuotaEspecial");
                    RaisePropertyChanged("IsEnabledValidaFechaDeAlicuotaEspecial");
                    BuscarAlicuotaIVAGeneral();
                    CalcularIVA();
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código del Proveedor es requerido.")]
        public string CodigoProveedor {
            get {
                return Model.CodigoProveedor;
            }
            set {
                if (Model.CodigoProveedor != value) {
                    Model.CodigoProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoProveedorPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoProveedor, true)) {
                        ConexionCodigoProveedor = null;
                    }
                    CalcularIVA();
                    Master.CalcularTotales();
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Proveedor es requerido.")]
        [LibGridColum("Proveedor")]
        public string NombreProveedor {
            get {
                return Model.NombreProveedor;
            }
            set {
                if (Model.NombreProveedor != value) {
                    Model.NombreProveedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreProveedorPropertyName);
                    CalcularIVA();
                    Master.CalcularTotales();
                }
            }
        }

        [LibGridColum("Monto Exento", eGridColumType.Numeric)]
        public decimal MontoExento {
            get {
                return Model.MontoExento;
            }
            set {
                if (Model.MontoExento != value) {
                    Model.MontoExento = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoExentoPropertyName);
                    RaisePropertyChanged(MontoTotalPropertyName);
                    Master.CalcularTotales();
                }
            }
        }

        [LibGridColum("Monto Gravable", eGridColumType.Numeric)]
        public decimal MontoGravable {
            get {
                return Model.MontoGravable;
            }
            set {
                if (Model.MontoGravable != value) {
                    Model.MontoGravable = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoGravablePropertyName);
                    RaisePropertyChanged(MontoTotalPropertyName);
                    Master.CalcularTotales();
                    CalcularIVA();
                }
            }
        }

        [LibGridColum("Monto IVA", eGridColumType.Numeric)]
        public decimal MontoIVA {
            get {
                return Model.MontoIVA;
            }
            set {
                if (Model.MontoIVA != value) {
                    Model.MontoIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoIVAPropertyName);
                    RaisePropertyChanged(MontoTotalPropertyName);
                    Master.CalcularTotales();
                }
            }
        }

        [LibGridColum("Monto Retención", eGridColumType.Numeric)]
        public decimal MontoRetencion {
            get {
                return Model.MontoRetencion;
            }
            set {
                if (Model.MontoRetencion != value) {
                    Model.MontoRetencion = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoRetencionPropertyName);
                }
            }
        }

        [LibGridColum("Monto Total", eGridColumType.Numeric)]
        [LibCustomValidation("MontoTotalValidating")]
        public decimal MontoTotal {
            get {
                return MontoExento + MontoGravable + MontoGravableAlicuotaEspecial1 + MontoGravableAlicuotaEspecial2 + MontoIVA + MontoIVAAlicuotaEspecial1 + MontoIVAAlicuotaEspecial2 - MontoRetencion;
            }
        }

        public bool AplicaParaLibroDeCompras {
            get {
                return Model.AplicaParaLibroDeComprasAsBool;
            }
            set {
                if (Model.AplicaParaLibroDeComprasAsBool != value) {
                    Model.AplicaParaLibroDeComprasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AplicaParaLibroDeComprasPropertyName);
                }
            }
        }

        public string ObservacionesCxP {
            get {
                return Model.ObservacionesCxP;
            }
            set {
                if (Model.ObservacionesCxP != value) {
                    Model.ObservacionesCxP = value;
                    IsDirty = true;
                    RaisePropertyChanged(ObservacionesCxPPropertyName);
                }
            }
        }

        [LibGridColum("Generado por")]
        public eGeneradoPor GeneradaPor {
            get {
                return Model.GeneradaPorAsEnum;
            }
            set {
                if (Model.GeneradaPorAsEnum != value) {
                    Model.GeneradaPorAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(GeneradaPorPropertyName);
                }
            }
        }

        public bool AplicaIvaAlicuotaEspecial {
            get {
                return Model.AplicaIvaAlicuotaEspecialAsBool;
            }
            set {
                if (Model.AplicaIvaAlicuotaEspecialAsBool != value) {
                    Model.AplicaIvaAlicuotaEspecialAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AplicaIvaAlicuotaEspecialPropertyName);
                    RaisePropertyChanged("IsEnabledActivarAlicuotaEspecial");
                    RaisePropertyChanged("IsEnabledActivarAlicuotaGeneral");
                    Master.CalcularTotales();
                    CalcularIVA();
                }
            }
        }

        [LibGridColum("Monto Gravable IVA 7 %", eGridColumType.Numeric)]
        public decimal MontoGravableAlicuotaEspecial1 {
            get {
                return Model.MontoGravableAlicuotaEspecial1;
            }
            set {
                if (Model.MontoGravableAlicuotaEspecial1 != value) {
                    Model.MontoGravableAlicuotaEspecial1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoGravableAlicuotaEspecial1PropertyName);
                    RaisePropertyChanged(MontoIVAAlicuotaEspecial1PropertyName);
                    RaisePropertyChanged(MontoTotalPropertyName);
                    CalcularIVA();
                    Master.CalcularTotales();
                }
            }
        }

        [LibGridColum("Monto IVA 7 %", eGridColumType.Numeric)]
        [LibCustomValidation("AlicuotaEspecial1Validating")]
        public decimal MontoIVAAlicuotaEspecial1 {
            get {
                return Model.MontoIVAAlicuotaEspecial1;
            }
            set {
                if (Model.MontoIVAAlicuotaEspecial1 != value) {
                    Model.MontoIVAAlicuotaEspecial1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoIVAAlicuotaEspecial1PropertyName);
                    RaisePropertyChanged(MontoTotalPropertyName);
                    Master.CalcularTotales();
                }
            }
        }

        public decimal PorcentajeIvaAlicuotaEspecial1 {
            get {
                return Model.PorcentajeIvaAlicuotaEspecial1;
            }
            set {
                if (Model.PorcentajeIvaAlicuotaEspecial1 != value) {
                    Model.PorcentajeIvaAlicuotaEspecial1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeIvaAlicuotaEspecial1PropertyName);
                }
            }
        }

        [LibGridColum("Monto Gravable IVA 9 %", eGridColumType.Numeric)]
        public decimal MontoGravableAlicuotaEspecial2 {
            get {
                return Model.MontoGravableAlicuotaEspecial2;
            }
            set {
                if (Model.MontoGravableAlicuotaEspecial2 != value) {
                    Model.MontoGravableAlicuotaEspecial2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoGravableAlicuotaEspecial2PropertyName);
                    RaisePropertyChanged(MontoIVAAlicuotaEspecial2PropertyName);
                    RaisePropertyChanged(MontoTotalPropertyName);
                    CalcularIVA();
                    Master.CalcularTotales();
                }
            }
        }

        [LibGridColum("Monto IVA 9 %", eGridColumType.Numeric)]
        [LibCustomValidation("AlicuotaEspecial2Validating")]
        public decimal MontoIVAAlicuotaEspecial2 {
            get {
                return Model.MontoIVAAlicuotaEspecial2;
            }
            set {
                if (Model.MontoIVAAlicuotaEspecial2 != value) {
                    Model.MontoIVAAlicuotaEspecial2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoIVAAlicuotaEspecial2PropertyName);
                    RaisePropertyChanged(MontoTotalPropertyName);
                    Master.CalcularTotales();
                }
            }
        }

        public decimal PorcentajeIvaAlicuotaEspecial2 {
            get {
                return Model.PorcentajeIvaAlicuotaEspecial2;
            }
            set {
                if (Model.PorcentajeIvaAlicuotaEspecial2 != value) {
                    Model.PorcentajeIvaAlicuotaEspecial2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeIvaAlicuotaEspecial2PropertyName);
                }
            }
        }

        public eGeneradoPor[] ArrayGeneradoPor {
            get {
                return LibEnumHelper<eGeneradoPor>.GetValuesInArray();
            }
        }

        public RendicionViewModel Master {
            get;
            set;
        }

        public FkProveedorViewModel ConexionCodigoProveedor {
            get {
                return _ConexionCodigoProveedor;
            }
            set {
                if (_ConexionCodigoProveedor != value) {
                    _ConexionCodigoProveedor = value;
                    RaisePropertyChanged(CodigoProveedorPropertyName);
                }
                if (_ConexionCodigoProveedor == null) {
                    CodigoProveedor = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCodigoProveedorCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public DetalleDeRendicionViewModel()
            : base(new DetalleDeRendicion(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public DetalleDeRendicionViewModel(RendicionViewModel initMaster, DetalleDeRendicion initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
            alicuotaIVANav = new clsAlicuotaIVANav();
            BuscarAlicuotaIVAGeneral();
            ObtenerAlicuotasEspeciales();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(DetalleDeRendicion valModel) {
            base.InitializeLookAndFeel(valModel);
            GeneradaPor = eGeneradoPor.Rendicion;
        }

        protected override ILibBusinessDetailComponent<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>> GetBusinessComponent() {
            return new clsDetalleDeRendicionNav();
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCodigoProveedorCommand = new RelayCommand<string>(ExecuteChooseCodigoProveedorCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCodigoProveedor = Master.FirstConnectionRecordOrDefault<FkProveedorViewModel>("Proveedor", LibSearchCriteria.CreateCriteria("CodigoProveedor", CodigoProveedor));
        }

        private void ExecuteChooseCodigoProveedorCommand(string valCodigoProveedor) {
            try {
                if (valCodigoProveedor == null) {
                    valCodigoProveedor = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("CodigoProveedor", valCodigoProveedor);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_Proveedor_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCodigoProveedor = Master.ChooseRecord<FkProveedorViewModel>("Proveedor", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCodigoProveedor != null) {
                    CodigoProveedor = ConexionCodigoProveedor.CodigoProveedor;
                    NombreProveedor = ConexionCodigoProveedor.NombreProveedor;
                } else {
                    CodigoProveedor = string.Empty;
                    NombreProveedor = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult FechaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(Fecha, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha"));
                }
                if (LibDate.DateIsGreaterThanToday(Fecha, false, "")) {
                    vResult = new ValidationResult(LibDate.MsgDateIsGreaterThanToday("Fecha"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        private string _LabelIVA;
        private clsAlicuotaIVANav alicuotaIVANav;

        public string LabelIVA {
            get {
                if (LibText.IsNullOrEmpty(_LabelIVA))
                    return "IVA";
                return _LabelIVA;
            }
            set {
                if (_LabelIVA != value) {
                    _LabelIVA = string.Format("IVA ({0}%)", value);
                    RaisePropertyChanged(LabelIVAPropertyName);
                }
            }
        }

        private decimal AlicuotaIVAGeneral {
            get;
            set;
        }

        private void BuscarAlicuotaIVAGeneral() {
            AlicuotaIVAGeneral = alicuotaIVANav.GetAlicuotaGeneral(Model.Fecha);
            LabelIVA = LibConvert.ToStr(AlicuotaIVAGeneral);
        }

        private void CalcularIVA() {
            PorcentajeIvaAlicuotaEspecial1 = LibConvert.ToDec("0");
            PorcentajeIvaAlicuotaEspecial2 = LibConvert.ToDec("0");
            if (AplicaIvaAlicuotaEspecial) {
                MontoIVAAlicuotaEspecial1 = CalcularIva(_Alicuota1, MontoGravableAlicuotaEspecial1);
                MontoIVAAlicuotaEspecial2 = CalcularIva(_Alicuota2, MontoGravableAlicuotaEspecial2);
                MontoGravable = LibConvert.ToDec("0");
                MontoIVA = LibConvert.ToDec("0");
                if (MontoIVAAlicuotaEspecial1 != 0) {
                    PorcentajeIvaAlicuotaEspecial1 = _Alicuota1;
                } else if (MontoIVAAlicuotaEspecial2 != 0) {
                    PorcentajeIvaAlicuotaEspecial2 = _Alicuota2;
                }
            } else {
                MontoIVA = alicuotaIVANav.CalcularIva(AlicuotaIVAGeneral, Model.MontoGravable);
                MontoGravableAlicuotaEspecial1 = LibConvert.ToDec("0");
                MontoGravableAlicuotaEspecial2 = LibConvert.ToDec("0");
                MontoIVAAlicuotaEspecial1 = LibConvert.ToDec("0");
                MontoIVAAlicuotaEspecial2 = LibConvert.ToDec("0");
            }
            Master.CalcularTotales();
        }

        private ValidationResult MontoTotalValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if (MontoTotal > 0) {
                ValidoAsBool = true;
                return ValidationResult.Success;
            } else {
                vResult = new ValidationResult("El monto de la factura no puede ser 0 (cero).");
                ValidoAsBool = false;
            }
            return vResult;
        }

        public bool ValidoAsBool {
            get {
                return Model.ValidoAsBool;
            }
            set {
                if (Model.ValidoAsBool != value) {
                    Model.ValidoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ValidoAsBoolPropertyName);
                }
            }
        }

        public void ValidoAsBoolRaisePropertyChanged() {
            RaisePropertyChanged(ValidoAsBoolPropertyName);
        }

        internal void RaisePropertyChangeds() {
            GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
                    .ToList().ForEach(p => RaisePropertyChanged(p.Name));
        }

        public bool GeneradoPorUsuario {
            get {
                return LibEnumDescriptionAttribute.Equals(GeneradaPor, eGeneradoPor.Usuario);
            }
        }

        public void GeneradoPorUsuarioAsBoolRaisePropertyChanged() {
            RaisePropertyChanged(GeneradaPorPropertyName);
        }

        public bool IsEnabledActivarAlicuotaEspecial {
            get {
                return AplicaIvaAlicuotaEspecial && IsEnabled;
            }
        }

        public bool IsEnabledActivarAlicuotaGeneral {
            get {
                return !AplicaIvaAlicuotaEspecial && IsEnabled;
            }
        }

        public string lblIvaEspecial1 {
            get {
                return "IVA (" + LibConvert.NumToString(_Alicuota1, 2) + ") %";
            }
        }

        public string lblIvaEspecial2 {
            get {
                return "IVA (" + LibConvert.NumToString(_Alicuota2, 2) + ") %";
            }
        }

        public string lblMontoGravable1 {
            get {
                return "Monto Gravable (" + LibConvert.NumToString(_Alicuota1, 2) + ") %";
            }
        }

        public string lblMontoGravable2 {
            get {
                return "Monto Gravable (" + LibConvert.NumToString(_Alicuota2, 2) + ") %";
            }
        }

        void ObtenerAlicuotasEspeciales() {
            IAlicuotaImpuestoEspecialPdn insAlicuota = new clsAlicuotaImpuestoEspecialNav();
            XElement xElement = insAlicuota.ObtenerAlicuotaEspecialOrderByMontoMinimo(Fecha);
            if (xElement != null && xElement.HasElements) {
                var entity1 = xElement.Descendants("GpResult")
                    .Select(c => new {
                        PorcentajeAlicuota = LibImportData.ToDec(c.Element("Alicuota").Value, 2),
                        FechaDesde = LibConvert.ToDate(c.Element("FechaDesde").Value),
                        FechaHasta = LibConvert.ToDate(c.Element("FechaHasta").Value)
                    }).OrderBy(c => c.PorcentajeAlicuota);
                foreach (var item in entity1) {
                    _Alicuota1 = item.PorcentajeAlicuota;
                    _FechaDesdeAlicuota = item.FechaDesde;
                    _FechaHastaAlicuota = item.FechaHasta;
                    break;
                }

                var entity2 = xElement.Descendants("GpResult")
                    .Select(c => new {
                        PorcentajeAlicuota = LibImportData.ToDec(c.Element("Alicuota").Value, 2)
                    }).OrderByDescending(c => c.PorcentajeAlicuota);
                foreach (var item in entity2) {
                    _Alicuota2 = item.PorcentajeAlicuota;
                    break;
                }
            }
        }

        decimal CalcularIva(decimal PorcentajeAlicuota, decimal MontoACalcular) {
            return (PorcentajeAlicuota * MontoACalcular) / 100;
        }

        public bool IsEnabledValidaFechaDeAlicuotaEspecial {
            get {
                if (Fecha >= _FechaDesdeAlicuota && Fecha <= _FechaHastaAlicuota) {
                    return true && IsEnabled;
                } else {
                    return false;
                }
            }
        }

        public bool IsVisibleDeAlicuotaEspecial {
            get {
                if (Fecha >= _FechaDesdeAlicuota && Fecha <= _FechaHastaAlicuota) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        private bool AlicuotaEspecial1Validating() {
            bool vResult = true;
            bool vContinuar = false;
            vContinuar = MontoGravable == 0 && MontoGravableAlicuotaEspecial2 == 0 && MontoIVA == 0 && MontoIVAAlicuotaEspecial2 == 0;
            if (!vContinuar) {
               if (MontoGravableAlicuotaEspecial1 != 0) {
                   LibMessages.MessageBox.Alert(this, "Ingresó un monto para la alícuota al " + LibConvert.NumToString(_Alicuota2, 2) + " %, el sistema llevará estos montos a Cero (0), ya que existen valores cargados en alícuota al " + LibConvert.NumToString(_Alicuota1, 2) + " %.", "Información");
                   MontoGravableAlicuotaEspecial1 = 0;
                   MontoIVAAlicuotaEspecial1 = 0;
               }
            }
            return vResult;
        }

        private bool AlicuotaEspecial2Validating() {
            bool vResult = true;
            bool vContinuar = false;
            vContinuar = MontoGravable == 0 && MontoGravableAlicuotaEspecial1 == 0 && MontoIVA == 0 && MontoIVAAlicuotaEspecial1 == 0;
            if (!vContinuar) {
                if (MontoGravableAlicuotaEspecial2 != 0) {
                    LibMessages.MessageBox.Alert(this, "Ingresó un monto para la alícuota al " + LibConvert.NumToString(_Alicuota1, 2) + " %, el sistema llevará estos montos a Cero (0), ya que existen valores cargados en alícuota al " + LibConvert.NumToString(_Alicuota2, 2) + " %.", "Información");
                    MontoGravableAlicuotaEspecial2 = 0;
                    MontoIVAAlicuotaEspecial2 = 0;
                }
            }
            return vResult;
        }

    } //End of class DetalleDeRendicionViewModel
} //End of namespace Galac.Adm.Uil.CajaChica

