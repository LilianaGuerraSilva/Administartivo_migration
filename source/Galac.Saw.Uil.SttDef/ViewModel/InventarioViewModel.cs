using System.Collections.Generic;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Uil;
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class InventarioViewModel : LibInputViewModelMfc<InventarioStt> {
        #region Constantes
        public const string SinonimoColorPropertyName = "SinonimoColor";
        public const string SinonimoGrupoPropertyName = "SinonimoGrupo";
        public const string SinonimoRolloPropertyName = "SinonimoRollo";
        public const string SinonimoSerialPropertyName = "SinonimoSerial";
        public const string SinonimoTallaPropertyName = "SinonimoTalla";
        public const string PermitirSobregiroPropertyName = "PermitirSobregiro";
        public const string VerificarStockPropertyName = "VerificarStock";
        public const string UsarBaseImponibleDiferenteA0Y100PropertyName = "UsarBaseImponibleDiferenteA0Y100";
        public const string UsaAlmacenPropertyName = "UsaAlmacen";
        public const string ImprimeSerialRolloLuegoDeDescripArticuloPropertyName = "ImprimeSerialRolloLuegoDeDescripArticulo";
        public const string ImprimirTransferenciaAlInsertarPropertyName = "ImprimirTransferenciaAlInsertar";
        public const string NombreCampoDefinibleInventario1PropertyName = "NombreCampoDefinibleInventario1";
        public const string NombreCampoDefinibleInventario2PropertyName = "NombreCampoDefinibleInventario2";
        public const string NombreCampoDefinibleInventario3PropertyName = "NombreCampoDefinibleInventario3";
        public const string NombreCampoDefinibleInventario4PropertyName = "NombreCampoDefinibleInventario4";
        public const string NombreCampoDefinibleInventario5PropertyName = "NombreCampoDefinibleInventario5";
        public const string FormaDeAsociarCentroDeCostoPropertyName = "FormaDeAsociarCentroDeCosto";
        public const string ActivarFacturacionPorAlmacenPropertyName = "ActivarFacturacionPorAlmacen";
        public const string AvisoDeReservasvencidasPropertyName = "AvisoDeReservasvencidas";
        public const string CantidadDeDecimalesPropertyName = "CantidadDeDecimales";
        public const string CodigoAlmacenGenericoPropertyName = "CodigoAlmacenGenerico";
        public const string ConsecutivoAlmacenGenericoPropertyName = "ConsecutivoAlmacenGenerico";
        public const string NombreAlmacenGenericoPropertyName = "NombreAlmacenGenerico";
        public const string IsEnabledDatosAlmacenPropertyName = "IsEnabledDatosAlmacen";
        public const string IsEnabledAsociarCentroDeCostosPropertyName = "IsEnabledAsociarCentrosDeCosto";      
        #endregion
        #region Variables
        private FkAlmacenViewModel _ConexionCodigoAlmacenGenerico = null;
        bool mEsFacturadorBasico;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "5.1.- Inventario"; }
        }

        public string  SinonimoColor {
            get {
                return Model.SinonimoColor;
            }
            set {
                if (Model.SinonimoColor != value) {
                    Model.SinonimoColor = value;
                    IsDirty = true;
                    RaisePropertyChanged(SinonimoColorPropertyName);                    
                }
            }
        }

        public string  SinonimoGrupo {
            get {
                return Model.SinonimoGrupo;
            }
            set {
                if (Model.SinonimoGrupo != value) {
                    Model.SinonimoGrupo = value;
                    IsDirty = true;
                    RaisePropertyChanged(SinonimoGrupoPropertyName);
                }
            }
        }

        public string  SinonimoRollo {
            get {
                return Model.SinonimoRollo;
            }
            set {
                if (Model.SinonimoRollo != value) {
                    Model.SinonimoRollo = value;
                    IsDirty = true;
                    RaisePropertyChanged(SinonimoRolloPropertyName);
                }
            }
        }

        public string  SinonimoSerial {
            get {
                return Model.SinonimoSerial;
            }
            set {
                if (Model.SinonimoSerial != value) {
                    Model.SinonimoSerial = value;
                    IsDirty = true;
                    RaisePropertyChanged(SinonimoSerialPropertyName);
                }
            }
        }

        public string  SinonimoTalla {
            get {
                return Model.SinonimoTalla;
            }
            set {
                if (Model.SinonimoTalla != value) {
                    Model.SinonimoTalla = value;
                    IsDirty = true;
                    RaisePropertyChanged(SinonimoTallaPropertyName);
                }
            }
        }

        public ePermitirSobregiro  PermitirSobregiro {
            get {
                return Model.PermitirSobregiroAsEnum;
            }
            set {
                if (Model.PermitirSobregiroAsEnum != value) {
                    Model.PermitirSobregiroAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(PermitirSobregiroPropertyName);
                }
            }
        }

        public bool  VerificarStock {
            get {
                return Model.VerificarStockAsBool;
            }
            set {
                if (Model.VerificarStockAsBool != value) {
                    Model.VerificarStockAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(VerificarStockPropertyName);
                }
            }
        }

        public bool  UsarBaseImponibleDiferenteA0Y100 {
            get {
                return Model.UsarBaseImponibleDiferenteA0Y100AsBool;
            }
            set {
                if (Model.UsarBaseImponibleDiferenteA0Y100AsBool != value) {
                    Model.UsarBaseImponibleDiferenteA0Y100AsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarBaseImponibleDiferenteA0Y100PropertyName);
                }
            }
        }

        public bool  UsaAlmacen {
            get {
                return Model.UsaAlmacenAsBool;
            }
            set {
                if (Model.UsaAlmacenAsBool != value) {
                    Model.UsaAlmacenAsBool = value;
                    IsDirty = true;
                    if (value) {
                        UsaLoteFechaDeVencimiento = false;
                        RaisePropertyChanged(() => UsaLoteFechaDeVencimiento);
                    } else { 
                        ActivarFacturacionPorAlmacen = false;
                    }
                    RaisePropertyChanged(UsaAlmacenPropertyName);
                    RaisePropertyChanged(IsEnabledDatosAlmacenPropertyName);
                    RaisePropertyChanged(() => IsEnabledUsalAlmacen);
                    RaisePropertyChanged(() => IsEnabledUsaLoteFechaDeVencimiento);
                }
            }
        }

        public bool  ImprimeSerialRolloLuegoDeDescripArticulo {
            get {
                return Model.ImprimeSerialRolloLuegoDeDescripArticuloAsBool;
            }
            set {
                if (Model.ImprimeSerialRolloLuegoDeDescripArticuloAsBool != value) {
                    Model.ImprimeSerialRolloLuegoDeDescripArticuloAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimeSerialRolloLuegoDeDescripArticuloPropertyName);
                }
            }
        }

        public bool  ImprimirTransferenciaAlInsertar {
            get {
                return Model.ImprimirTransferenciaAlInsertarAsBool;
            }
            set {
                if (Model.ImprimirTransferenciaAlInsertarAsBool != value) {
                    Model.ImprimirTransferenciaAlInsertarAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirTransferenciaAlInsertarPropertyName);
                }
            }
        }

        public string  NombreCampoDefinibleInventario1 {
            get {
                return Model.NombreCampoDefinibleInventario1;
            }
            set {
                if (Model.NombreCampoDefinibleInventario1 != value) {
                    Model.NombreCampoDefinibleInventario1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinibleInventario1PropertyName);
                }
            }
        }

        public string  NombreCampoDefinibleInventario2 {
            get {
                return Model.NombreCampoDefinibleInventario2;
            }
            set {
                if (Model.NombreCampoDefinibleInventario2 != value) {
                    Model.NombreCampoDefinibleInventario2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinibleInventario2PropertyName);
                }
            }
        }

        public string  NombreCampoDefinibleInventario3 {
            get {
                return Model.NombreCampoDefinibleInventario3;
            }
            set {
                if (Model.NombreCampoDefinibleInventario3 != value) {
                    Model.NombreCampoDefinibleInventario3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinibleInventario3PropertyName);
                }
            }
        }

        public string  NombreCampoDefinibleInventario4 {
            get {
                return Model.NombreCampoDefinibleInventario4;
            }
            set {
                if (Model.NombreCampoDefinibleInventario4 != value) {
                    Model.NombreCampoDefinibleInventario4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinibleInventario4PropertyName);
                }
            }
        }

        public string  NombreCampoDefinibleInventario5 {
            get {
                return Model.NombreCampoDefinibleInventario5;
            }
            set {
                if (Model.NombreCampoDefinibleInventario5 != value) {
                    Model.NombreCampoDefinibleInventario5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCampoDefinibleInventario5PropertyName);
                }
            }
        }

        public eFormaDeAsociarCentroDeCostos  AsociarCentroDeCostos {
            get {
                return Model.AsociarCentroDeCostosAsEnum;
            }
            set {
                if (Model.AsociarCentroDeCostosAsEnum != value) {
                    Model.AsociarCentroDeCostosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(FormaDeAsociarCentroDeCostoPropertyName);
                }
            }
        }
        public bool IsEnabledAsociarCentroDeCostos {
            get {
                return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaModuloDeContabilidad") && 
                       LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaCentroDeCostos");
            }
        }

        public bool  ActivarFacturacionPorAlmacen {
            get {
                return Model.ActivarFacturacionPorAlmacenAsBool;
            }
            set {
                if (Model.ActivarFacturacionPorAlmacenAsBool != value) {
                    Model.ActivarFacturacionPorAlmacenAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ActivarFacturacionPorAlmacenPropertyName);
                }
            }
        }

        public bool  AvisoDeReservasvencidas {
            get {
                return Model.AvisoDeReservasvencidasAsBool;
            }
            set {
                if (Model.AvisoDeReservasvencidasAsBool != value) {
                    Model.AvisoDeReservasvencidasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(AvisoDeReservasvencidasPropertyName);
                }
            }
        }

        public eCantidadDeDecimales  CantidadDeDecimales {
            get {
                return Model.CantidadDeDecimalesAsEnum;
            }
            set {
                if (Model.CantidadDeDecimalesAsEnum != value) {
                    Model.CantidadDeDecimalesAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadDeDecimalesPropertyName);
                    LibMessages.Notification.Send<eCantidadDeDecimales>(Model.CantidadDeDecimalesAsEnum, CantidadDeDecimalesPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Almacén Genérico es requerido.")]
        public string  CodigoAlmacenGenerico {
            get {
                return Model.CodigoAlmacenGenerico;
            }
            set {
                if (Model.CodigoAlmacenGenerico != value) {
                    Model.CodigoAlmacenGenerico = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoAlmacenGenericoPropertyName);
                    RaisePropertyChanged(NombreAlmacenGenericoPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoAlmacenGenerico, true)) {
                        ConexionAlmacenGenerico = null;
                    }
                }
            }
        }

        public int  ConsecutivoAlmacenGenerico {
            get {
                return Model.ConsecutivoAlmacenGenerico;
            }
            set {
                if (Model.ConsecutivoAlmacenGenerico != value) {
                    Model.ConsecutivoAlmacenGenerico = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoAlmacenGenericoPropertyName);
                }
            }
        }

        public string NombreAlmacenGenerico {
            get {
                return (_ConexionCodigoAlmacenGenerico != null ? _ConexionCodigoAlmacenGenerico.NombreAlmacen : string.Empty);
            }
           
        }

        public bool UsaLoteFechaDeVencimiento {
            get { return Model.UsaLoteFechaDeVencimientoAsBool; }
            set {
                if (Model.UsaLoteFechaDeVencimientoAsBool != value) {
                    Model.UsaLoteFechaDeVencimientoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(() => UsaLoteFechaDeVencimiento);
                    if (value) {
                        UsaAlmacen = false;
                        RaisePropertyChanged(() => UsaAlmacen);
                    }
                    RaisePropertyChanged(() => IsEnabledUsalAlmacen);
                    RaisePropertyChanged(() => IsEnabledUsaLoteFechaDeVencimiento);
                }
            }
        }

        public ePermitirSobregiro[] ArrayPermitirSobregiro {
            get {
                return LibEnumHelper<ePermitirSobregiro>.GetValuesInArray();
            }
        }

        public eCantidadDeDecimales[] ArrayCantidadDeDecimales {
            get {
                return LibEnumHelper<eCantidadDeDecimales>.GetValuesInArray();
            }
        }

        public FkAlmacenViewModel ConexionAlmacenGenerico {
            get {
                return _ConexionCodigoAlmacenGenerico;
            }
            set {
                if (_ConexionCodigoAlmacenGenerico != value) {
                    _ConexionCodigoAlmacenGenerico = value;
                    if(_ConexionCodigoAlmacenGenerico != null) {
                        CodigoAlmacenGenerico = _ConexionCodigoAlmacenGenerico.Codigo;
                    } else if(_ConexionCodigoAlmacenGenerico == null) {
                        CodigoAlmacenGenerico = string.Empty;
                    }
                }
            }
        }

        public RelayCommand<string> ChooseAlmacenGenericoCommand {
            get;
            private set;
        }

        public bool IsEnabledUsalAlmacen {
            get {
                return IsEnabled && !UsaLoteFechaDeVencimiento && !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsEmprendedor");
            }
        }
		
        public eFormaDeAsociarCentroDeCostos[] ArrayAsociarCentroDeCostos {
            get {
                return LibEnumHelper<eFormaDeAsociarCentroDeCostos>.GetValuesInArray();
            }
        }
		
        public bool IsEnabledDatosAlmacen {
            get {
                return IsEnabled && UsaAlmacen && !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsEmprendedor");
            }
        }

        public bool IsEnabledUsaLoteFechaDeVencimiento {
            get {
                return IsEnabled && !UsaAlmacen && SePuedeModificarUsaLoteFechaDeVencimiento();
            }
        }
        #endregion //Propiedades
        #region Constructores
        public InventarioViewModel()
            : this(new InventarioStt(), eAccionSR.Insertar) {
        }
        public InventarioViewModel(InventarioStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = SinonimoColorPropertyName;
            LibMessages.Notification.Send<eCantidadDeDecimales>(Model.CantidadDeDecimalesAsEnum, CantidadDeDecimalesPropertyName);
            mEsFacturadorBasico = new clsLibSaw().EsVersionFacturadorBasico();
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(InventarioStt valModel) {
            base.InitializeLookAndFeel(valModel);         
        }

        protected override InventarioStt FindCurrentRecord(InventarioStt valModel) {
            if (valModel == null) {
                return new InventarioStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("SinonimoColor", valModel.SinonimoColor, 10);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "InventarioGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<InventarioStt>, IList<InventarioStt>> GetBusinessComponent() {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseAlmacenGenericoCommand = new RelayCommand<string>(ExecuteChooseCodigoAlmacenGenericoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
            vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.Codigo", CodigoAlmacenGenerico), eLogicOperatorType.And);
            ConexionAlmacenGenerico = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkAlmacenViewModel>("Almacén", vFixedCriteria, new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseCodigoAlmacenGenericoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Almacen_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Almacen_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionAlmacenGenerico = null;
                ConexionAlmacenGenerico = LibFKRetrievalHelper.ChooseRecord<FkAlmacenViewModel>("Almacén", vDefaultCriteria, vFixedCriteria, string.Empty);                
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private bool SePuedeModificarUsaLoteFechaDeVencimiento() {
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            bool vSePuedeModificarUsaLoteFechaDeVencimiento = true;
            ISettValueByCompanyPdn insParametrosByCompany = new clsSettValueByCompanyNav();
            if (UsaLoteFechaDeVencimiento) {
                vSePuedeModificarUsaLoteFechaDeVencimiento = !insParametrosByCompany.ExistenArticulosLoteFdV(vConsecutivoCompania);
            } else {
                vSePuedeModificarUsaLoteFechaDeVencimiento = !insParametrosByCompany.ExistenArticulosMercanciaNoSimpleNoLoteFDV(vConsecutivoCompania);
            }
            
            return vSePuedeModificarUsaLoteFechaDeVencimiento;
        }

        public bool IsVisibleUsaLoteFechaDeVencimientCatDecimales {
            get {
                return !mEsFacturadorBasico;
            }
        }
        public bool IsVisibleSinonimos {
            get {
                return !mEsFacturadorBasico;
            }
        }
        public bool IsVisibleBaseImpInsertarTrasnferencia {
            get {
                return !mEsFacturadorBasico;
            }
        }
        #endregion //Metodos Generados
    } //End of class InventarioViewModel

} //End of namespace Galac.Saw.Uil.SttDef

