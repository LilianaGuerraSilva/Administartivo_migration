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
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using System.ComponentModel;

namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CompraCambiarStatusViewModel : LibInputMasterViewModelMfc<Compra> {
        #region Constantes
        public const string SeriePropertyName = "Serie";
        public const string NumeroPropertyName = "Numero";
        public const string FechaPropertyName = "Fecha";        
        public const string CodigoProveedorPropertyName = "CodigoProveedor";
        public const string NombreProveedorPropertyName = "NombreProveedor";        
        public const string CodigoAlmacenPropertyName = "CodigoAlmacen";
        public const string NombreAlmacenPropertyName = "NombreAlmacen";
        public const string MonedaPropertyName = "Moneda";
        public const string CodigoMonedaPropertyName = "CodigoMoneda";
        public const string CambioABolivaresPropertyName = "CambioABolivares";
        public const string GenerarCXPPropertyName = "GenerarCXP";
        public const string UsaSeguroPropertyName = "UsaSeguro";
        public const string TipoDeDistribucionPropertyName = "TipoDeDistribucion";
        public const string TotalRenglonesPropertyName = "TotalRenglones";
        public const string TotalOtrosGastosPropertyName = "TotalOtrosGastos";
        public const string TotalCompraPropertyName = "TotalCompra";
        public const string ComentariosPropertyName = "Comentarios";
        public const string StatusCompraPropertyName = "StatusCompra";
        public const string FechaDeAnulacionPropertyName = "FechaDeAnulacion";



        #endregion
        #region Variables        
        Saw.Lib.clsNoComunSaw _clsNoComun = null;
        #endregion //Variables
        #region Propiedades

        internal eTipoCompra TipoModulo { get; set; }

        public override string ModuleName {
            get {
                if (TipoModulo == eTipoCompra.Importacion) {
                    return "Importación";
                }
                return "Compra Nacional";
            }
        }

       
        public string Serie {
            get {
                return Model.Serie;
            }
            set {
                if (Model.Serie != value) {
                    Model.Serie = value;                
                }
            }
        }

        public string Numero {
            get {
                return Model.Numero;
            }
            set {
                if (Model.Numero != value) {
                    Model.Numero = value;        
                }
            }
        }
        public DateTime Fecha {
            get {
                return Model.Fecha;
            }
            set {
                if (Model.Fecha != value) {
                    Model.Fecha = value;
                }
            }
        }

        public string CodigoProveedor {
            get {
                return Model.CodigoProveedor;
            }
            set {
                if (Model.CodigoProveedor != value) {
                    Model.CodigoProveedor = value;
                }
            }
        }
        public string NombreProveedor {
            get {
                return Model.NombreProveedor;
            }
            set {
                if (Model.NombreProveedor != value) {
                    Model.NombreProveedor = value;
                }
            }
        }

        public string CodigoAlmacen {
            get {
                return Model.CodigoAlmacen;
            }
            set {
                if (Model.CodigoAlmacen != value) {
                    Model.CodigoAlmacen = value;
                }
            }
        }


        public string NombreAlmacen {
            get {
                return Model.NombreAlmacen;
            }
            set {
                if (Model.NombreAlmacen != value) {
                    Model.NombreAlmacen = value;
                }
            }
        }

        public string Moneda {
            get {
                return Model.Moneda;
            }
            set {
                if (Model.Moneda != value) {
                    Model.Moneda = value;
                }
            }
        }

        public string CodigoMoneda {
            get {
                return Model.CodigoMoneda;
            }
            set {
                if (Model.CodigoMoneda != value) {
                    Model.CodigoMoneda = value;
                }
            }
        }
        public decimal CambioABolivares {
            get {
                return Model.CambioABolivares;
            }
            set {
                if (Model.CambioABolivares != value) {
                    Model.CambioABolivares = value;
                }
            }
        }

        public bool GenerarCXP {
            get {
                return Model.GenerarCXPAsBool;
            }
            set {
                if (Model.GenerarCXPAsBool != value) {
                    Model.GenerarCXPAsBool = value;
                }
            }
        }

        public bool UsaSeguro {
            get {
                return Model.UsaSeguroAsBool;
            }
            set {
                if (Model.UsaSeguroAsBool != value) {
                    Model.UsaSeguroAsBool = value;
                    IsDirty = true;
                }
            }
        }
        public eTipoDeDistribucion TipoDeDistribucion {
            get {
                return Model.TipoDeDistribucionAsEnum;
            }
            set {
                if (Model.TipoDeDistribucionAsEnum != value) {
                    Model.TipoDeDistribucionAsEnum = value;
                }
            }
        }

        public decimal TotalRenglones {
            get {
                return Model.TotalRenglones;
            }
            set {
                if (Model.TotalRenglones != value) {
                    Model.TotalRenglones = value;
                }
            }
        }

        public decimal TotalOtrosGastos {
            get {
                return Model.TotalOtrosGastos;
            }
            set {
                if (Model.TotalOtrosGastos != value) {
                    Model.TotalOtrosGastos = value;
                }
            }
        }

        public decimal TotalCompra {
            get {
                return Model.TotalCompra;
            }
            set {
                if (Model.TotalCompra != value) {
                    Model.TotalCompra = value;
                }
            }
        }

        public string Comentarios {
            get {
                return Model.Comentarios;
            }
            set {
                if (Model.Comentarios != value) {
                    Model.Comentarios = value;
                }
            }
        }

        public eStatusCompra StatusCompra {
            get {
                return Model.StatusCompraAsEnum;
            }
            set {
                if (Model.StatusCompraAsEnum != value) {
                    Model.StatusCompraAsEnum = value;
                }
            }
        }

        public eTipoCompra TipoDeCompra {
            get {
                return Model.TipoDeCompraAsEnum;
            }
            set {
                if (Model.TipoDeCompraAsEnum != value) {
                    Model.TipoDeCompraAsEnum = value;
                }
            }
        }

        [LibCustomValidation("FechaDeAnulacionValidating")]
        public DateTime FechaDeAnulacion {
            get {
                return Model.FechaDeAnulacion;
            }
            set {
                if (Model.FechaDeAnulacion != value) {
                    Model.FechaDeAnulacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeAnulacionPropertyName);
                }
            }
        }


        public string NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                   // RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public DateTime FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                   /// RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public string MonedaActual {
            get {
                return _clsNoComun.InstanceMonedaLocalActual.NombreMoneda(Fecha);
            }
        }

        public bool IsVisibleMonedaActual {
            get {
                return CodigoMoneda == _clsNoComun.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today()) || CodigoMoneda == _clsNoComun.InstanceMonedaLocalActual.CodigoDeMonedaAnterior(LibDate.Today());
            }
        }

        public bool UsaBolivarFuerte {
            get {
                return _clsNoComun.InstanceMonedaLocalActual.CodigoMoneda(Fecha) == _clsNoComun.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
            }
        }

        public string lblCambioAMonedaLocalActual {
            get {
                return "Cambio a " + _clsNoComun.InstanceMonedaLocalActual.SimboloMoneda(LibDate.Today());
            }
        }

        

        public bool IsVisibleUsaSeguro {
            get {
                return TipoDeCompra == eTipoCompra.Importacion;
            }
        }

        bool _IsEnabledTipoDistribucion;
        public bool IsEnabledTipoDistribucion {
            get {
                return IsEnabled &&  _IsEnabledTipoDistribucion;
            }
            set {
                if (_IsEnabledTipoDistribucion != value) {
                    _IsEnabledTipoDistribucion = value;                    
                }
            }
        }

        public bool IsVisibleDiferenciaDistribucion
        {
            get
            {
                return TipoDeDistribucion == eTipoDeDistribucion.ManualPorMonto;
            }
        }
        #endregion //Propiedades
        #region Constructores
        public CompraCambiarStatusViewModel()
            : this(new Compra(), eAccionSR.Insertar) {
        }
        public CompraCambiarStatusViewModel(Compra initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = SeriePropertyName;
            _clsNoComun = new Saw.Lib.clsNoComunSaw();
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados
        
        protected override void InitializeLookAndFeel(Compra valModel) {
            base.InitializeLookAndFeel(valModel);            
            _clsNoComun.InstanceMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
            TipoDeCompra = TipoModulo;
            if (Action == eAccionSR.Insertar) {
                IsEnabledTipoDistribucion = true;
            }else {
                IsEnabledTipoDistribucion = false;
            }
        }

        protected override Compra FindCurrentRecord(Compra valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "CompraGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Compra>, IList<Compra>> GetBusinessComponent() {
            return new clsCompraNav();
        }

      
      
     
        protected override void InitializeCommands() {
            base.InitializeCommands();
      
        }
        
        private ValidationResult FechaDeAnulacionValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeAnulacion, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha De Anulacion"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados
        bool Continue = true;      
            

        protected override void ExecuteAction() {
            base.ExecuteAction();
            TipoDeCompra = TipoModulo;           
        }

        protected override void InitializeDetails() {            
        }

        protected override void ExecuteSpecialAction(eAccionSR valAction) { 
        if ((valAction == eAccionSR.Anular) || (valAction == eAccionSR.Abrir)) {
                string vConfirmMsgFormat = string.Format("¿Está seguro de que desea {0} la Compra?", LibString.LCase(valAction.GetDescription()));
                if (LibMessages.MessageBox.YesNo(this, vConfirmMsgFormat, ModuleName)) {
                    ChangeStatus();
                    UseDetail = false;
                    bool vResult = ((Ccl.GestionCompras.ICompraPdn)BusinessComponent).CambiarStatusCompra(Model, valAction);
                    if (valAction == eAccionSR.Anular && vResult && Model.GenerarCXPAsBool) {
                        LibMessages.MessageBox.Alert(this, "Debido a que la anulación de la compra no necesariamente implica  la anulación de la CxP dicho proceso no se ejecutara en línea. Si desea anular el documento generado a partir de la compra, por favor diríjase al módulo CxP y ejecute allí la opción", ModuleName);
                    }
                    DialogResult = vResult;
                    CloseOnActionComplete = vResult;
                    LibMessages.RefreshList.Send(ModuleName);

                } else {
                    IsDirty = false;
                    DialogResult = false;
                    RaiseRequestCloseEvent();
                }
            } else {
                base.ExecuteSpecialAction(valAction);
            }
        
        }

        void ChangeStatus(){
            if (Action == eAccionSR.Abrir ){
                Model.StatusCompraAsEnum = eStatusCompra.Vigente;
            }else{
                Model.StatusCompraAsEnum = eStatusCompra.Anulada ;
            }
        }
    } //End of class CompraViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

