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

namespace Galac.Saw.Uil.Tablas.ViewModel {
    public class LineaDeProductoViewModel : LibInputViewModelMfc<LineaDeProducto> {
        #region Constantes
        public const string NombrePropertyName = "Nombre";
        public const string PorcentajeComisionPropertyName = "PorcentajeComision";
        public const string CentroDeCostoPropertyName = "CentroDeCosto";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string IsEnabledCentroDeCostoPropertyName = "IsEnabledCentroDeCosto";

        #endregion
        #region Variables
        private FkCentrodeCostosViewModel _ConexionCentroDeCosto = null;

        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Línea de Producto"; }
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

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre de la Línea es requerido.")]
        [LibGridColum("Nombre de la Línea", WidthForPrinting = 20, Width = 200)]
        public string  Nombre {
            get {
                return Model.Nombre;
            }
            set {
                if (Model.Nombre != value) {
                    Model.Nombre = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }
          
        [LibCustomValidation("ValidatePorcentaje")]
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
        //[LibGridColum("Centro De Costos", WidthForPrinting = 20, Width = 200)]
        public string  CentroDeCosto {
            get {
                return Model.CentroDeCosto;
            }
            set {
                if (Model.CentroDeCosto != value) {
                    Model.CentroDeCosto = value;
                    IsDirty = true;
                    RaisePropertyChanged(CentroDeCostoPropertyName);
                    if (LibString.IsNullOrEmpty(CentroDeCosto, true)) {
                        ConexionCentroDeCosto = null;
                    }
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

        public bool IsEnabledCentroDeCosto {
            get {
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaModuloDeContabilidad")) {
                    return true;
                } else {
                    return false;
                }
            }
        }
        public FkCentrodeCostosViewModel ConexionCentroDeCosto {
            get {
                return _ConexionCentroDeCosto;
            }
            set {
                if (_ConexionCentroDeCosto != value) {
                    _ConexionCentroDeCosto = value;
                    RaisePropertyChanged(CentroDeCostoPropertyName);
                }
                if (_ConexionCentroDeCosto == null) {
                    CentroDeCosto = string.Empty;
                }
            }
        }
        public RelayCommand<string> ChooseCentroDeCostoCommand {
            get;
            private set;
        }

        private ValidationResult ValidatePorcentaje() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (Model.PorcentajeComision > 100) {
                vResult = new ValidationResult(string.Format("El procentaje de la comisión no puede ser mayor que el cien(100%)", PorcentajeComision));
            }
            return vResult;
        }
        public bool IsVisibleCentroDeCostos{
            get {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaModuloDeContabilidad") &&
                    LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Compania", "UsaCentroDeCostos");

            }
        }

        #endregion //Propiedades  
        #region Constructores
        public LineaDeProductoViewModel()
            : this(new LineaDeProducto(), eAccionSR.Insertar) {
        }
        public LineaDeProductoViewModel(LineaDeProducto initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = NombrePropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(LineaDeProducto valModel) {
            base.InitializeLookAndFeel(valModel);
            if (Consecutivo == 0) {
                Consecutivo = GenerarProximoConsecutivo();
            }
        }

        protected override LineaDeProducto FindCurrentRecord(LineaDeProducto valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "LineaDeProductoGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<LineaDeProducto>, IList<LineaDeProducto>> GetBusinessComponent() {
            return new clsLineaDeProductoNav();
        }
		   protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCentroDeCostoCommand = new RelayCommand<string>(ExecuteChooseCentroDeCostoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionCentroDeCosto = FirstConnectionRecordOrDefault<FkCentrodeCostosViewModel>("Centro de Costos", LibSearchCriteria.CreateCriteria("Codigo", CentroDeCosto));
        }

        private void ExecuteChooseCentroDeCostoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = null;
                if (valCodigo != CentroDeCosto) {
                    ConexionCentroDeCosto = ChooseRecord<FkCentrodeCostosViewModel>("Centro de Costos", vDefaultCriteria, vFixedCriteria, string.Empty);
                }
                if (ConexionCentroDeCosto != null) {
                    CentroDeCosto = ConexionCentroDeCosto.Codigo;
                } else {
                    CentroDeCosto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private int GenerarProximoConsecutivo() {
            int vResult = 0;
            LibGpParams vParams = new LibGpParams();
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoConsecutivo", Mfc.GetIntAsParam("Compania"));
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "Consecutivo"));              
            return vResult;
        }
        #endregion //Metodos Generados

    } //End of class LineaDeProductoViewModel

} //End of namespace Galac.Saw.Uil.Tablas

