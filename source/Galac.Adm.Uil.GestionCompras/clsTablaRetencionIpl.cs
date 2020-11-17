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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras {
    public class clsTablaRetencionIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<TablaRetencion>, IList<TablaRetencion>> _Reglas;
        IList<TablaRetencion> _ListTablaRetencion;
        #endregion //Variables
        #region Propiedades

        public IList<TablaRetencion> ListTablaRetencion {
            get { return _ListTablaRetencion; }
            set { _ListTablaRetencion = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsTablaRetencionIpl() {
            ListTablaRetencion = new List<TablaRetencion>();
            ListTablaRetencion.Add(new TablaRetencion());
        }
        public clsTablaRetencionIpl(eTipodePersonaRetencion initTipoDePersona, string initCodigo) {
            ListTablaRetencion = new List<TablaRetencion>();
            FindAndSetObject(initTipoDePersona, initCodigo);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(TablaRetencion refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<TablaRetencion>, IList<TablaRetencion>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.GestionCompras.clsTablaRetencionNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((TablaRetencion)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((TablaRetencion)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Tabla Retencion"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((TablaRetencion)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((TablaRetencion)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tabla Retencion.Insertar")]
        internal bool InsertRecord(TablaRetencion refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<TablaRetencion> vBusinessObject = new List<TablaRetencion>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tabla Retencion.Modificar")]
        internal bool UpdateRecord(TablaRetencion refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<TablaRetencion> vBusinessObject = new List<TablaRetencion>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tabla Retencion.Eliminar")]
        internal bool DeleteRecord(TablaRetencion refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<TablaRetencion> vBusinessObject = new List<TablaRetencion>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(eTipodePersonaRetencion valTipoDePersona, string valCodigo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInEnum("Adm.Gv_TablaRetencion_B1.TipoDePersona", LibConvert.EnumToDbValue((int)valTipoDePersona));
            vParams.AddInString("Adm.Gv_TablaRetencion_B1.Codigo", valCodigo, 6);
            ListTablaRetencion = _Reglas.GetData(eProcessMessageType.SpName, "TablaRetencionGET", vParams.Get());
        }

        public bool ValidateAll(TablaRetencion refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidTipoDePersona(valAction, refRecord.TipoDePersonaAsEnum, false);
            vResult = IsValidCodigo(valAction, refRecord.Codigo, false) && vResult;
            vResult = IsValidFechaAplicacion(valAction, refRecord.FechaAplicacion, false) && vResult;
            vResult = IsValidCodigoMoneda(valAction, refRecord.CodigoMoneda, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidTipoDePersona(eAccionSR valAction, eTipodePersonaRetencion valTipoDePersona, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            return vResult;
        }

        public bool IsValidCodigo(eAccionSR valAction, string valCodigo, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Código de Retención"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidFechaAplicacion(eAccionSR valAction, DateTime valFechaAplicacion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAplicacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoMoneda(eAccionSR valAction, string valCodigoMoneda, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoMoneda, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsTablaRetencionIpl

} //End of namespace Galac.Adm.Uil.GestionCompras

