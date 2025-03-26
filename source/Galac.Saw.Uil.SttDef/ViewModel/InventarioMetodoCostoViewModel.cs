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
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Lib;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class InventarioMetodoCostoViewModel : LibInputViewModelMfc<MetododecostosStt> {
        #region Constantes
        public const string MetodoDeCosteoPropertyName = "MetodoDeCosteo";
        public const string MaximoGastosAdmisiblesPropertyName = "MaximoGastosAdmisibles";
        public const string FechaContabilizacionDeCosteoPropertyName = "FechaContabilizacionDeCosteo";
        public const string FechaDesdeUsoMetodoDeCosteoPropertyName = "FechaDesdeUsoMetodoDeCosteo";
        public const string ComprobanteCostoDetalladoPropertyName = "ComprobanteCostoDetallado";
        public const string CalculoAutomaticoDeCostoPropertyName = "CalculoAutomaticoDeCosto";
        public const string IsVisibleDetalleCostoPropertyName = "IsVisibleDetalleCosto";
        #endregion

        #region Variables
        bool mEsFacturadorBasico;
        #endregion

        #region Propiedades

        public override string ModuleName {
            get { return "5.3.- Método  de costos"; }
        }

        public eTipoDeMetodoDeCosteo  MetodoDeCosteo {
            get {
                return Model.MetodoDeCosteoAsEnum;
            }
            set {
                if (Model.MetodoDeCosteoAsEnum != value) {
                    Model.MetodoDeCosteoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(MetodoDeCosteoPropertyName);
                    RaisePropertyChanged(IsVisibleDetalleCostoPropertyName);
                }
            }
        }

        public decimal  MaximoGastosAdmisibles {
            get {
                return Model.MaximoGastosAdmisibles;
            }
            set {
                if (Model.MaximoGastosAdmisibles != value) {
                    Model.MaximoGastosAdmisibles = value;
                    IsDirty = true;
                    RaisePropertyChanged(MaximoGastosAdmisiblesPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaContabilizacionDeCosteoValidating")]
        public DateTime  FechaContabilizacionDeCosteo {
            get {
                return Model.FechaContabilizacionDeCosteo;
            }
            set {
                if (Model.FechaContabilizacionDeCosteo != value) {
                    Model.FechaContabilizacionDeCosteo = value;
                    //if(Model.FechaDesdeUsoMetodoDeCosteo.Day != 1) {
                    //    DateTime ValDate = value;
                    //    Model.FechaContabilizacionDeCosteo = new DateTime(ValDate.Year, ValDate.Month, 1);
                    //    LibMessages.MessageBox.Warning(this, "Usted debe ingresar fecha 1° (primero de mes), como fecha de contabilización del metodo de costo.", string.Empty);
                    //}
                    IsDirty = true;
                    RaisePropertyChanged(FechaContabilizacionDeCosteoPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaDesdeUsoMetodoDeCosteoValidating")]  
        public DateTime  FechaDesdeUsoMetodoDeCosteo {
            get {
                return Model.FechaDesdeUsoMetodoDeCosteo;
            }
            set {
                if (Model.FechaDesdeUsoMetodoDeCosteo != value) {
                    Model.FechaDesdeUsoMetodoDeCosteo = value;
                    //if(Model.FechaDesdeUsoMetodoDeCosteo.Day != 1) {
                    //    DateTime ValDate = value;
                    //    Model.FechaDesdeUsoMetodoDeCosteo = new DateTime(ValDate.Year, ValDate.Month, 1);
                    //    LibMessages.MessageBox.Warning(this, "Usted debe ingresar fecha 1° (primero de mes), como fecha de inicio del uso del metodo de costo.", string.Empty);
                    //}
                    IsDirty = true;
                    RaisePropertyChanged(FechaDesdeUsoMetodoDeCosteoPropertyName);
                }
            }
        }

        public bool  ComprobanteCostoDetallado {
            get {
                return Model.ComprobanteCostoDetalladoAsBool;
            }
            set {
                if (Model.ComprobanteCostoDetalladoAsBool != value) {
                    Model.ComprobanteCostoDetalladoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComprobanteCostoDetalladoPropertyName);
                }
            }
        }

        public bool  CalculoAutomaticoDeCosto {
            get {
                return Model.CalculoAutomaticoDeCostoAsBool;
            }
            set {
                if (Model.CalculoAutomaticoDeCostoAsBool != value) {
                    Model.CalculoAutomaticoDeCostoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(CalculoAutomaticoDeCostoPropertyName);
                }
            }
        }

        public eTipoDeMetodoDeCosteo[] ArrayTipoDeMetodoDeCosteo {
            get {
                return LibEnumHelper<eTipoDeMetodoDeCosteo>.GetValuesInArray();
            }
        }
        
        public bool IsVisibleMetodoCosto {
            get {
                return !mEsFacturadorBasico;
            }
        }

        public bool IsVisibleDetalleCosto {
            get {
                return !mEsFacturadorBasico;
            }
        }

        #endregion //Propiedades
        #region Constructores
        public InventarioMetodoCostoViewModel()
            : this(new MetododecostosStt(), eAccionSR.Insertar) {
        }
        public InventarioMetodoCostoViewModel(MetododecostosStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = MetodoDeCosteoPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
            // Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(MetododecostosStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override MetododecostosStt FindCurrentRecord(MetododecostosStt valModel) {
            if (valModel == null) {
                return new MetododecostosStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInEnum("MetodoDeCosteo", LibConvert.EnumToDbValue((int)valModel.MetodoDeCosteoAsEnum));
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "InventarioMetodoCostoGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<MetododecostosStt>, IList<MetododecostosStt>> GetBusinessComponent() {
            return null;
        }

        private ValidationResult FechaContabilizacionDeCosteoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {                
                if (MetodoDeCosteo == eTipoDeMetodoDeCosteo.CostoPromedio) {
                    if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaContabilizacionDeCosteo, false, Action)) {
                        vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha Contabilización de Costeo"));
                    } else if (FechaContabilizacionDeCosteo.Day != 1) {
                        vResult = new ValidationResult("Usted debe ingresar fecha 1° (primero de mes), como fecha de contabilización del metodo de costo.");
                    } else if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaModuloDeContabilidad") && LibGalac.Aos.Base.LibDate.F1IsLessThanF2(FechaContabilizacionDeCosteo, GetValueFechaDeInicioContabilizacion())) {
                        vResult = new ValidationResult("La fecha de inicio de contabilización de costos no puede ser menor a la fecha de inicio de contabilización general del sistema.");
                    } else if (MetodoDeCosteo == eTipoDeMetodoDeCosteo.CostoPromedio && LibGalac.Aos.Base.LibDate.F1IsLessThanF2(FechaContabilizacionDeCosteo, FechaDesdeUsoMetodoDeCosteo)) {
                        vResult = new ValidationResult("La fecha de inicio de contabilización de costos no puede ser menor a la fecha de inicio del uso del metodo de costo.");
                    }
                }
            }
            return vResult;
            }

        private ValidationResult FechaDesdeUsoMetodoDeCosteoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (MetodoDeCosteo == eTipoDeMetodoDeCosteo.CostoPromedio) {
                    if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDesdeUsoMetodoDeCosteo, false, Action)) {
                        vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha de Inicio de Uso del Método de Costo"));
                    } else if (FechaDesdeUsoMetodoDeCosteo.Day != 1) {
                        vResult = new ValidationResult("Usted debe ingresar fecha 1° (primero de mes), como fecha de inicio del uso del metodo de costo.");
                    }
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados

        private DateTime GetValueFechaDeInicioContabilizacion() {
            try {
                return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDateTime("Parametros", "FechaDeInicioContabilizacion");
            } catch (Exception) {
                return LibDate.Today();
            }
        }

    } //End of class InventarioMetodoCostoViewModel

} //End of namespace Galac.Saw.Uil.SttDef

