using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;


namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class CxPComprasViewModel : LibInputViewModelMfc<ComprasStt> {
        #region Constantes
        public const string ImprimirOrdenDeCompraPropertyName = "ImprimirOrdenDeCompra";
        public const string ImprimirCompraAlInsertarPropertyName = "ImprimirCompraAlInsertar";
        public const string IvaEsCostoEnComprasPropertyName = "IvaEsCostoEnCompras";
        public const string GenerarCxpDesdeCompraPropertyName = "GenerarCxpDesdeCompra";
        public const string NombrePlantillaOrdenDeCompraPropertyName = "NombrePlantillaOrdenDeCompra";
        public const string NombrePlantillaImpresionCodigoBarrasComprasPropertyName = "NombrePlantillaImpresionCodigoBarrasCompras";
        public const string NombrePlantillaCompraPropertyName = "NombrePlantillaCompra";
        public const string SugerirNumeroDeOrdenDeCompraPropertyName = "SugerirNumeroDeOrdenDeCompra";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "6.1.- Compras"; }
        }

        public bool  ImprimirOrdenDeCompra {
            get {
                return Model.ImprimirOrdenDeCompraAsBool;
            }
            set {
                if (Model.ImprimirOrdenDeCompraAsBool != value) {
                    Model.ImprimirOrdenDeCompraAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirOrdenDeCompraPropertyName);
                }
            }
        }

        public bool  ImprimirCompraAlInsertar {
            get {
                return Model.ImprimirCompraAlInsertarAsBool;
            }
            set {
                if (Model.ImprimirCompraAlInsertarAsBool != value) {
                    Model.ImprimirCompraAlInsertarAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirCompraAlInsertarPropertyName);
                }
            }
        }

        public bool  IvaEsCostoEnCompras {
            get {
                return Model.IvaEsCostoEnComprasAsBool;
            }
            set {
                if (Model.IvaEsCostoEnComprasAsBool != value) {
                    Model.IvaEsCostoEnComprasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(IvaEsCostoEnComprasPropertyName);
                }
            }
        }

        public bool  GenerarCxpDesdeCompra {
            get {
                return Model.GenerarCxPdesdeCompraAsBool;
            }
            set {
                if (Model.GenerarCxPdesdeCompraAsBool != value) {
                    Model.GenerarCxPdesdeCompraAsBool = value;
                    IsDirty = true;
                    AdvertirQuePoseeCxPGeneradasDesdeCompra();
                    RaisePropertyChanged(GenerarCxpDesdeCompraPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaOrdenDeCompraValidating")]
        public string  NombrePlantillaOrdenDeCompra {
            get {
                return Model.NombrePlantillaOrdenDeCompra;
            }
            set {
                if (Model.NombrePlantillaOrdenDeCompra != value) {
                    Model.NombrePlantillaOrdenDeCompra = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaOrdenDeCompra)) {
                        ExecuteBuscarPlantillaCommandOrden();
                    }
                    RaisePropertyChanged(NombrePlantillaOrdenDeCompraPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaImpresionCodigoBarrasComprasValidating")]
        public string NombrePlantillaImpresionCodigoBarrasCompras
        {
            get
            {
                return Model.NombrePlantillaImpresionCodigoBarrasCompras;
            }
            set
            {
                if (Model.NombrePlantillaImpresionCodigoBarrasCompras != value)
                {
                    Model.NombrePlantillaImpresionCodigoBarrasCompras = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaImpresionCodigoBarrasCompras))
                    {
                        ExecuteBuscarPlantillaCommandBarras();
                    }
                    RaisePropertyChanged(NombrePlantillaImpresionCodigoBarrasComprasPropertyName);
                }
            }
        }

        [LibCustomValidation("NombrePlantillaCompraValidating")]
        public string  NombrePlantillaCompra {
            get {
                return Model.NombrePlantillaCompra;
            }
            set {
                if (Model.NombrePlantillaCompra != value) {
                    Model.NombrePlantillaCompra = value;
                    IsDirty = true;
                    if (LibString.IsNullOrEmpty(NombrePlantillaCompra)) {
                        ExecuteBuscarPlantillaCommandCompra();
                    }
                    RaisePropertyChanged(NombrePlantillaCompraPropertyName);
                }
            }
        }

        public RelayCommand ChooseTemplateCommandOrden {
            get;
            private set;
        }
        public RelayCommand ChooseTemplateCommandEtiquetasCompra {
            get;
            private set;
        }

        public RelayCommand ChooseTemplateCommandCompra {
            get;
            private set;
        }

        public string CaptionIvaEsCostoEnCompras {
           get {
              return string.Format("{0} es Costo en Compras.......................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }
        public bool SugerirNumeroDeOrdenDeCompra {
            get {
                return Model.SugerirNumeroDeOrdenDeCompraAsBool;
            }
            set {
                if (Model.SugerirNumeroDeOrdenDeCompraAsBool != value) {
                    Model.SugerirNumeroDeOrdenDeCompraAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(SugerirNumeroDeOrdenDeCompraPropertyName);
                }
            }
        }
        #endregion //Propiedades
        #region Variables
        private bool _GenerarCxPDesdeCompraOriginal;
        #endregion
        #region Constructores
        public CxPComprasViewModel()
            : this(new ComprasStt(), eAccionSR.Insertar) {
        }
        public CxPComprasViewModel(ComprasStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ImprimirOrdenDeCompraPropertyName;
            _GenerarCxPDesdeCompraOriginal = initModel.GenerarCxPdesdeCompraAsBool;
            //Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeCommands() {
            ChooseTemplateCommandOrden = new RelayCommand(ExecuteBuscarPlantillaCommandOrden);
            ChooseTemplateCommandEtiquetasCompra = new RelayCommand(ExecuteBuscarPlantillaCommandBarras);
            ChooseTemplateCommandCompra = new RelayCommand(ExecuteBuscarPlantillaCommandCompra);
        }

        protected override void InitializeLookAndFeel(ComprasStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ComprasStt FindCurrentRecord(ComprasStt valModel) {
            if (valModel == null) {
                return new ComprasStt();
            }            
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<ComprasStt>, IList<ComprasStt>> GetBusinessComponent() {
            return null;
        }

        private void ExecuteBuscarPlantillaCommandOrden() {
            try {
                NombrePlantillaOrdenDeCompra = new clsUtilParameters().BuscarNombrePlantilla("rpx de Orden de Compra (*.rpx)|*Compra*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandBarras()
        {
            try
            {
                NombrePlantillaImpresionCodigoBarrasCompras = new clsUtilParameters().BuscarNombrePlantilla("rpx de Impresión de Etiquetas por Compra (*.rpx)|*Barra*.rpx");
            }
            catch (System.AccessViolationException)
            {
                throw;
            }
            catch (System.Exception vEx)
            {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteBuscarPlantillaCommandCompra() {
            try {
                NombrePlantillaCompra = new clsUtilParameters().BuscarNombrePlantilla("rpx de Compra (*.rpx)|*Compra*.rpx");
            } catch(System.AccessViolationException) {
                throw;
            } catch(System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados

        private ValidationResult NombrePlantillaOrdenDeCompraValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaOrdenDeCompra)) {
                    vResult = new ValidationResult("En la sección " + ModuleName + "-> El Campo Planilla de Impresión de Orden de Compra, es requerido.");
                } else if (!LibString.IsNullOrEmpty(NombrePlantillaOrdenDeCompra) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaOrdenDeCompra)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaOrdenDeCompra + ", en " + this.ModuleName + ", no EXISTE.");
                } else if (!(new Galac.Saw.Lib.clsUtilRpt().EsFormatoRpxValidoParaAOS(NombrePlantillaOrdenDeCompra))) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaOrdenDeCompra + ", en " + this.ModuleName + ", no tiene el formato requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaImpresionCodigoBarrasComprasValidating()
        {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaImpresionCodigoBarrasCompras)) {
                    vResult = new ValidationResult("En la sección " + ModuleName + "-> El Campo Planilla de Impresión de Etiquetas por Compras, es requerido.");
                } else if (!LibString.IsNullOrEmpty(NombrePlantillaImpresionCodigoBarrasCompras) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaImpresionCodigoBarrasCompras)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaImpresionCodigoBarrasCompras + ", en " + this.ModuleName + ", no EXISTE.");
                } else if (!(new Galac.Saw.Lib.clsUtilRpt().EsFormatoRpxValidoParaAOS(NombrePlantillaImpresionCodigoBarrasCompras))) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaImpresionCodigoBarrasCompras + ", en " + this.ModuleName + ", no tiene el formato requerido.");
                }
            }
            return vResult;
        }

        private ValidationResult NombrePlantillaCompraValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibString.IsNullOrEmpty(NombrePlantillaCompra)) {
                    vResult = new ValidationResult("En la sección " + ModuleName + "-> El Campo Planilla de Impresión de Compra, es requerido.");
                }else if (!LibString.IsNullOrEmpty(NombrePlantillaCompra) && !clsUtilParameters.EsValidoNombrePlantilla(NombrePlantillaCompra)) {
                    vResult = new ValidationResult("El RPX " + NombrePlantillaCompra + ", en " + this.ModuleName + ", no EXISTE.");
                }
            }
            return vResult;
        }

        private void AdvertirQuePoseeCxPGeneradasDesdeCompra() {
            ISettValueByCompanyPdn insParametrosByCompany = new clsSettValueByCompanyNav();
            int vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            bool vExistenCxPGeneradasDesdeCompra = insParametrosByCompany.ExistenCxPGeneradasDesdeCompra(vConsecutivoCompania);
            StringBuilder vMensaje = new StringBuilder();
            vMensaje.Append("Desactivar este parámetro puede traer inconsistencias");
            vMensaje.AppendLine(" en sus Cuentas por Pagar (CxP) generadas desde compras.");
            vMensaje.AppendLine("Si necesita más información, comuníquese con Gálac Software.\n");
            vMensaje.Append("Presione \"Si\" para continuar.");

            if (Action == eAccionSR.Modificar) {
                if (_GenerarCxPDesdeCompraOriginal && !Model.GenerarCxPdesdeCompraAsBool && vExistenCxPGeneradasDesdeCompra) {
                    bool vDesactivarParametro = LibMessages.MessageBox.YesNo(this, vMensaje.ToString(), "Parámetro \"Generar CxP desde Compra\"");
                    if(vDesactivarParametro == true) {
                        GenerarCxpDesdeCompra = false;
                    } else {
                        GenerarCxpDesdeCompra = true;
                    }
                }
            }
        }
    } //End of class CxPComprasViewModel

} //End of namespace Galac.Saw.Uil.SttDef

