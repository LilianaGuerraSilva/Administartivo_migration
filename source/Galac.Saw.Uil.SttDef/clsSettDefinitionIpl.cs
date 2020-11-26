using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Security.Permissions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef {
    public class clsSettDefinitionIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<SettDefinition>, IList<SettDefinition>> _Reglas;
        IList<SettDefinition> _ListSettDefinition;
        #endregion //Variables
        #region Propiedades

        public IList<SettDefinition> ListSettDefinition {
            get { return _ListSettDefinition; }
            set { _ListSettDefinition = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsSettDefinitionIpl() {
            ListSettDefinition = new List<SettDefinition>();
            ListSettDefinition.Add(new SettDefinition());
        }
        public clsSettDefinitionIpl(string initName) {
            ListSettDefinition = new List<SettDefinition>();
            FindAndSetObject(initName);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(SettDefinition refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<SettDefinition>, IList<SettDefinition>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.SttDef.clsSettDefinitionNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((SettDefinition)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((SettDefinition)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Sett Definition"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((SettDefinition)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((SettDefinition)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Definition.Insertar")]
        internal bool InsertRecord(SettDefinition refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<SettDefinition> vBusinessObject = new List<SettDefinition>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Definition.Modificar")]
        internal bool UpdateRecord(SettDefinition refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<SettDefinition> vBusinessObject = new List<SettDefinition>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Sett Definition.Eliminar")]
        internal bool DeleteRecord(SettDefinition refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<SettDefinition> vBusinessObject = new List<SettDefinition>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valName) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Name", valName, 50);
            ListSettDefinition = _Reglas.GetData(eProcessMessageType.SpName, "SettDefinitionGET", vParams.Get());
        }

        public bool ValidateAll(SettDefinition refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidName(valAction, refRecord.Name, false);
            vResult = IsValidLevelModule(valAction, refRecord.LevelModule, false) && vResult;
            vResult = IsValidGroupName(valAction, refRecord.GroupName, false) && vResult;
            vResult = IsValidLevelGroup(valAction, refRecord.LevelGroup, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidName(eAccionSR valAction, string valName, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valName, true)) {
                BuildValidationInfo(MsgRequiredField("Name"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidLevelModule(eAccionSR valAction, int valLevelModule, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valLevelModule == 0) {
                BuildValidationInfo(MsgRequiredField("Nivel Modulo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidGroupName(eAccionSR valAction, string valGroupName, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valGroupName, true)) {
                BuildValidationInfo(MsgRequiredField("Group Name"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidLevelGroup(eAccionSR valAction, int valLevelGroup, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valLevelGroup == 0) {
                BuildValidationInfo(MsgRequiredField("Nivel del Grupo"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsSettDefinitionIpl

} //End of namespace Galac.Saw.Uil.SttDef

