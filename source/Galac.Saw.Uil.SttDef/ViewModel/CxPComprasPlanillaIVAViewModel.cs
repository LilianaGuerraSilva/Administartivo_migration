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
    public class CxPComprasPlanillaIVAViewModel : LibInputViewModelMfc<PlanillaDeIVAStt> {
        #region Constantes
        public const string CedulaContadorPropertyName = "CedulaContador";
        public const string ModeloPlanillaForma00030PropertyName = "ModeloPlanillaForma00030";
        public const string NombreContadorPropertyName = "NombreContador";
        public const string ImprimirCentimosEnPlanillaPropertyName = "ImprimirCentimosEnPlanilla";
        public const string NumeroCpcPropertyName = "NumeroCpc";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "6.5.- Planilla de IVA (Forma 30)"; }
        }

        public string  CedulaContador {
            get {
                return Model.CedulaContador;
            }
            set {
                if (Model.CedulaContador != value) {
                    Model.CedulaContador = value;
                    IsDirty = true;
                    RaisePropertyChanged(CedulaContadorPropertyName);
                }
            }
        }

        public eModeloPlanillaForma00030  ModeloPlanillaForma00030 {
            get {
                return Model.ModeloPlanillaForma00030AsEnum;
            }
            set {
                if (Model.ModeloPlanillaForma00030AsEnum != value) {
                    Model.ModeloPlanillaForma00030AsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModeloPlanillaForma00030PropertyName);
                }
            }
        }

        public string  NombreContador {
            get {
                return Model.NombreContador;
            }
            set {
                if (Model.NombreContador != value) {
                    Model.NombreContador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreContadorPropertyName);
                }
            }
        }

        public bool  ImprimirCentimosEnPlanilla {
            get {
                return Model.ImprimirCentimosEnPlanillaAsBool;
            }
            set {
                if (Model.ImprimirCentimosEnPlanillaAsBool != value) {
                    Model.ImprimirCentimosEnPlanillaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ImprimirCentimosEnPlanillaPropertyName);
                }
            }
        }

        public string  NumeroCpc {
            get {
                return Model.NumeroCPC;
            }
            set {
                if (Model.NumeroCPC != value) {
                    Model.NumeroCPC = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumeroCpcPropertyName);
                }
            }
        }

        public eModeloPlanillaForma00030[] ArrayModeloPlanillaForma00030 {
            get {
                return LibEnumHelper<eModeloPlanillaForma00030>.GetValuesInArray();
            }
        }
        #endregion //Propiedades
        #region Constructores
        #region Variables
        bool mEsFacturadorBasico;
        #endregion
        public CxPComprasPlanillaIVAViewModel()
            : this(new PlanillaDeIVAStt(), eAccionSR.Insertar) {
        }
        public CxPComprasPlanillaIVAViewModel(PlanillaDeIVAStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CedulaContadorPropertyName;
            mEsFacturadorBasico = new clsLibSaw().EsFacturadorBasico();
            // Model.ConsecutivoCompania = Mfc.GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(PlanillaDeIVAStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override PlanillaDeIVAStt FindCurrentRecord(PlanillaDeIVAStt valModel) {
            if (valModel == null) {
                return new PlanillaDeIVAStt();
            }
            //LibGpParams vParams = new LibGpParams();
            //vParams.AddInString("CedulaContador", valModel.CedulaContador, 20);
            //return BusinessComponent.GetData(eProcessMessageType.SpName, "CxPComprasPlanillaIVAGET", vParams.Get()).FirstOrDefault();
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<PlanillaDeIVAStt>, IList<PlanillaDeIVAStt>> GetBusinessComponent() {
            return null;
        }

        public bool IsVisiblePlanillaIVAContador {
            get {
                return !mEsFacturadorBasico;
            }
        }
        public bool IsVisiblePlanillaIVAForma30 {
            get {
                return !mEsFacturadorBasico;
            }
        }

        #endregion //Metodos Generados


    } //End of class CxPComprasPlanillaIVAViewModel

} //End of namespace Galac.Saw.Uil.SttDef

