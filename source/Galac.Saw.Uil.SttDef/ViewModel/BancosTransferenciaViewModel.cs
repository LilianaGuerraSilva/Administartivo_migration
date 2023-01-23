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
using LibGalac.Aos.Uil;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class BancosTransferenciaViewModel : LibInputViewModelMfc<TransferenciaStt> {
        #region Constantes
        public const string ConceptoBancarioReversoTransfIngresoPropertyName = "ConceptoBancarioReversoTransfIngreso";
        public const string ConceptoBancarioReversoTransfEgresoPropertyName = "ConceptoBancarioReversoTransfEgreso";
        #endregion
        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioReversoTransfIngreso = null;
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioReversoTransfEgreso = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "7.5.- Transferencia Bancaria"; }
        }

        public string  ConceptoBancarioReversoTransfIngreso {
            get {
                return Model.ConceptoBancarioReversoTransfIngreso;
            }
            set {
                if (Model.ConceptoBancarioReversoTransfIngreso != value) {
                    Model.ConceptoBancarioReversoTransfIngreso = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioReversoTransfIngresoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioReversoTransfIngreso, true)) {
                        ConexionConceptoBancarioReversoTransfIngreso = null;
                    }
                }
            }
        }

        public string  ConceptoBancarioReversoTransfEgreso {
            get {
                return Model.ConceptoBancarioReversoTransfEgreso;
            }
            set {
                if (Model.ConceptoBancarioReversoTransfEgreso != value) {
                    Model.ConceptoBancarioReversoTransfEgreso = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioReversoTransfEgresoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioReversoTransfEgreso, true)) {
                        ConexionConceptoBancarioReversoTransfEgreso = null;
                    }
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioReversoTransfIngreso {
            get {
                return _ConexionConceptoBancarioReversoTransfIngreso;
            }
            set {
                if (_ConexionConceptoBancarioReversoTransfIngreso != value) {
                    _ConexionConceptoBancarioReversoTransfIngreso = value;
                    if (_ConexionConceptoBancarioReversoTransfIngreso != null) {
                        ConceptoBancarioReversoTransfIngreso = _ConexionConceptoBancarioReversoTransfIngreso.Codigo;
                    }
                    RaisePropertyChanged(ConceptoBancarioReversoTransfIngresoPropertyName);
                }
                if (_ConexionConceptoBancarioReversoTransfIngreso == null) {
                    ConceptoBancarioReversoTransfIngreso = string.Empty;
                }
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioReversoTransfEgreso {
            get {
                return _ConexionConceptoBancarioReversoTransfEgreso;
            }
            set {
                if (_ConexionConceptoBancarioReversoTransfEgreso != value) {
                    _ConexionConceptoBancarioReversoTransfEgreso = value;
                    if (_ConexionConceptoBancarioReversoTransfEgreso != null) {
                        ConceptoBancarioReversoTransfEgreso = _ConexionConceptoBancarioReversoTransfEgreso.Codigo;
                    }
                    RaisePropertyChanged(ConceptoBancarioReversoTransfEgresoPropertyName);
                }
                if (_ConexionConceptoBancarioReversoTransfEgreso == null) {
                    ConceptoBancarioReversoTransfEgreso = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseConceptoBancarioReversoTransfIngresoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoBancarioReversoTransfEgresoCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public BancosTransferenciaViewModel()
            : this(new TransferenciaStt(), eAccionSR.Insertar) {
        }
        public BancosTransferenciaViewModel(TransferenciaStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ConceptoBancarioReversoTransfIngresoPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(TransferenciaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override TransferenciaStt FindCurrentRecord(TransferenciaStt valModel) {
            if (valModel == null) {
                return new TransferenciaStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<TransferenciaStt>, IList<TransferenciaStt>> GetBusinessComponent() {
            return null;
        }

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseConceptoBancarioReversoTransfIngresoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioReversoTransfIngresoCommand);
            ChooseConceptoBancarioReversoTransfEgresoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioReversoTransfEgresoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            
            LibSearchCriteria vFixedCriteriaIngreso = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
            vFixedCriteriaIngreso.Add(LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioReversoTransfIngreso), eLogicOperatorType.And);
            ConexionConceptoBancarioReversoTransfIngreso = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteriaIngreso, new clsSettValueByCompanyNav());
            LibSearchCriteria vFixedCriteriaEgreso = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
            vFixedCriteriaEgreso.Add(LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioReversoTransfEgreso), eLogicOperatorType.And);
            ConexionConceptoBancarioReversoTransfEgreso = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", vFixedCriteriaEgreso, new clsSettValueByCompanyNav());
        }

        private void ExecuteChooseConceptoBancarioReversoTransfIngresoCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioReversoTransfIngreso = null;
                ConexionConceptoBancarioReversoTransfIngreso = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoBancarioReversoTransfEgresoCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Egreso));
                ConexionConceptoBancarioReversoTransfEgreso = null;
                ConexionConceptoBancarioReversoTransfEgreso = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //Metodos Generados


    } //End of class BancosTransferenciaViewModel

} //End of namespace Galac.Comun.Uil.SttDef

