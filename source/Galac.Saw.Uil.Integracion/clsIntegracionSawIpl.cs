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
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Uil.Integracion {
    public class clsIntegracionSawIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>> _Reglas;
        IList<IntegracionSaw> _ListIntegracionSaw;
        #endregion //Variables
        #region Propiedades

        public IList<IntegracionSaw> ListIntegracionSaw {
            get { return _ListIntegracionSaw; }
            set { _ListIntegracionSaw = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsIntegracionSawIpl() {
            ListIntegracionSaw = new List<IntegracionSaw>();
            ListIntegracionSaw.Add(new IntegracionSaw());
        }
        public clsIntegracionSawIpl(eTipoIntegracion initTipoIntegracion, string initversion) {
            ListIntegracionSaw = new List<IntegracionSaw>();
            FindAndSetObject(initTipoIntegracion, initversion);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(IntegracionSaw refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Integracion.clsIntegracionSawNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((IntegracionSaw)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((IntegracionSaw)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Integracion Saw"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((IntegracionSaw)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((IntegracionSaw)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Integracion Saw.Insertar")]
        internal bool InsertRecord(IntegracionSaw refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<IntegracionSaw> vBusinessObject = new List<IntegracionSaw>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Integracion Saw.Modificar")]
        internal bool UpdateRecord(IntegracionSaw refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<IntegracionSaw> vBusinessObject = new List<IntegracionSaw>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Integracion Saw.Eliminar")]
        internal bool DeleteRecord(IntegracionSaw refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<IntegracionSaw> vBusinessObject = new List<IntegracionSaw>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(eTipoIntegracion valTipoIntegracion, string valversion) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInEnum("TipoIntegracion", LibConvert.EnumToDbValue((int)valTipoIntegracion));
            vParams.AddInString("version", valversion, 8);
            ListIntegracionSaw = _Reglas.GetData(eProcessMessageType.SpName, "IntegracionSawGET", vParams.Get());
        }

        public bool ValidateAll(IntegracionSaw refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidTipoIntegracion(valAction, refRecord.TipoIntegracionAsEnum, false);
            vResult = IsValidversion(valAction, refRecord.version, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidTipoIntegracion(eAccionSR valAction, eTipoIntegracion valTipoIntegracion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            return vResult;
        }

        public bool IsValidversion(eAccionSR valAction, string valversion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valversion, true)) {
                BuildValidationInfo(MsgRequiredField("version"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsIntegracionSawIpl

} //End of namespace Galac.Saw.Uil.Integracion

