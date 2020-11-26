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

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class SettDefinitionViewModel : LibInputViewModel <SettDefinition> {
        #region Constantes
        public const string NamePropertyName = "Name";
        public const string ModulePropertyName = "Module";
        public const string LevelModulePropertyName = "LevelModule";
        public const string GroupNamePropertyName = "GroupName";
        public const string LevelGroupPropertyName = "LevelGroup";
        public const string LabelPropertyName = "Label";
        public const string DataTypePropertyName = "DataType";
        public const string ValidationrulesPropertyName = "Validationrules";
        public const string IsSetForAllEnterprisePropertyName = "IsSetForAllEnterprise";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Sett Definition"; }
        }

        [LibRequired(ErrorMessage = "El campo Name es requerido.")]
        public string  Name {
            get {
                return Model.Name;
            }
            set {
                if (Model.Name != value) {
                    Model.Name = value;
                    IsDirty = true;
                    RaisePropertyChanged(NamePropertyName);
                }
            }
        }

        public string  Module {
            get {
                return Model.Module;
            }
            set {
                if (Model.Module != value) {
                    Model.Module = value;
                    IsDirty = true;
                    RaisePropertyChanged(ModulePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nivel Modulo es requerido.")]
        public int  LevelModule {
            get {
                return Model.LevelModule;
            }
            set {
                if (Model.LevelModule != value) {
                    Model.LevelModule = value;
                    IsDirty = true;
                    RaisePropertyChanged(LevelModulePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre del Grupo es requerido.")]
        public string  GroupName {
            get {
                return Model.GroupName;
            }
            set {
                if (Model.GroupName != value) {
                    Model.GroupName = value;
                    IsDirty = true;
                    RaisePropertyChanged(GroupNamePropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nivel del Grupo es requerido.")]
        public int  LevelGroup {
            get {
                return Model.LevelGroup;
            }
            set {
                if (Model.LevelGroup != value) {
                    Model.LevelGroup = value;
                    IsDirty = true;
                    RaisePropertyChanged(LevelGroupPropertyName);
                }
            }
        }

        public string  Label {
            get {
                return Model.Label;
            }
            set {
                if (Model.Label != value) {
                    Model.Label = value;
                    IsDirty = true;
                    RaisePropertyChanged(LabelPropertyName);
                }
            }
        }

        public eTipoDeDatoParametros  DataType {
            get {
                return Model.DataTypeAsEnum;
            }
            set {
                if (Model.DataTypeAsEnum != value) {
                    Model.DataTypeAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(DataTypePropertyName);
                }
            }
        }

        public string  Validationrules {
            get {
                return Model.Validationrules;
            }
            set {
                if (Model.Validationrules != value) {
                    Model.Validationrules = value;
                    IsDirty = true;
                    RaisePropertyChanged(ValidationrulesPropertyName);
                }
            }
        }

        public bool  IsSetForAllEnterprise {
            get {
                return Model.IsSetForAllEnterpriseAsBool;
            }
            set {
                if (Model.IsSetForAllEnterpriseAsBool != value) {
                    Model.IsSetForAllEnterpriseAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(IsSetForAllEnterprisePropertyName);
                }
            }
        }

        public eTipoDeDatoParametros[] ArrayTipoDeDatoParametros {
            get {
                return LibEnumHelper<eTipoDeDatoParametros>.GetValuesInArray();
            }
        }
        #endregion //Propiedades
        #region Constructores
        public SettDefinitionViewModel()
            : this(new SettDefinition(), eAccionSR.Insertar) {
        }
        public SettDefinitionViewModel(SettDefinition initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = NamePropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(SettDefinition valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override SettDefinition FindCurrentRecord(SettDefinition valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Name", valModel.Name, 50);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "SettDefinitionGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<SettDefinition>, IList<SettDefinition>> GetBusinessComponent() {
            return null;
        }
        #endregion //Metodos Generados


    } //End of class SettDefinitionViewModel

} //End of namespace Galac.Saw.Uil.SttDef

