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
using Entity = Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Uil.Cliente {
    public class clsClienteIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>> _Reglas;
        IList<Entity.Cliente> _ListCliente;
        #endregion //Variables
        #region Propiedades

        public IList<Entity.Cliente> ListCliente {
            get { return _ListCliente; }
            set { _ListCliente = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsClienteIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListCliente = new List<Entity.Cliente>();
            ListCliente.Add(new Entity.Cliente());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Entity.Cliente refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Entity.Cliente>, IList<Entity.Cliente>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Cliente.clsClienteNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Entity.Cliente)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Entity.Cliente)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Cliente"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Entity.Cliente)refRecord, valAction, out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Entity.Cliente)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            return GenerarProximoCodigo();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Insertar")]
        internal bool InsertRecord(Entity.Cliente refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Entity.Cliente> vBusinessObject = new List<Entity.Cliente>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Modificar")]
        internal bool UpdateRecord(Entity.Cliente refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Entity.Cliente> vBusinessObject = new List<Entity.Cliente>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Eliminar")]
        internal bool DeleteRecord(Entity.Cliente refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Entity.Cliente> vBusinessObject = new List<Entity.Cliente>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Codigo", valCodigo, 10);
            ListCliente = _Reglas.GetData(eProcessMessageType.SpName, "ClienteGET", vParams.Get());
        }

        private string GenerarProximoCodigo() {
            string vResult = "";
            RegistraCliente();
            XElement vResulset = _Reglas.QueryInfo(eProcessMessageType.Message, "ProximoCodigo", Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset, "Codigo");
            return vResult;
        }

        public bool ValidateAll(Entity.Cliente refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigo(valAction, refRecord.Codigo, false);
            vResult = IsValidNombre(valAction, refRecord.Nombre, false) && vResult;
            vResult = IsValidNumeroRIF(valAction, refRecord.NumeroRIF, false) && vResult;
            vResult = IsValidNumeroNIT(valAction, refRecord.NumeroNIT, false) && vResult;
            vResult = IsValidCiudad(valAction, refRecord.Ciudad, false) && vResult;
            vResult = IsValidZonaDeCobranza(valAction, refRecord.ZonaDeCobranza, false) && vResult;
            vResult = IsValidCodigoVendedor(valAction, refRecord.CodigoVendedor, false) && vResult;
            vResult = IsValidCuentaContableCxC(valAction, refRecord.CuentaContableCxC, false) && vResult;
            vResult = IsValidCuentaContableIngresos(valAction, refRecord.CuentaContableIngresos, false) && vResult;
            vResult = IsValidCuentaContableAnticipo(valAction, refRecord.CuentaContableAnticipo, false) && vResult;
            vResult = IsValidSectorDeNegocio(valAction, refRecord.SectorDeNegocio, false) && vResult;
            vResult = IsValidClienteDesdeFecha(valAction, refRecord.ClienteDesdeFecha, false) && vResult;
            outErrorMessage = Information.ToString();
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
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombre(eAccionSR valAction, string valNombre, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNombre, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumeroRIF(eAccionSR valAction, string valNumeroRIF, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNumeroRIF, true)) {
                BuildValidationInfo(MsgRequiredField("N° R.I.F."));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumeroNIT(eAccionSR valAction, string valNumeroNIT, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNumeroNIT, true)) {
                BuildValidationInfo(MsgRequiredField("N° N.I.T."));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCiudad(eAccionSR valAction, string valCiudad, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCiudad, true)) {
                BuildValidationInfo(MsgRequiredField("Ciudad"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidZonaDeCobranza(eAccionSR valAction, string valZonaDeCobranza, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valZonaDeCobranza, true)) {
                BuildValidationInfo(MsgRequiredField("Zona De Cobranza"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoVendedor(eAccionSR valAction, string valCodigoVendedor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoVendedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Vendedor"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaContableCxC(eAccionSR valAction, string valCuentaContableCxC, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaContableCxC, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Contable Cx C"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaContableIngresos(eAccionSR valAction, string valCuentaContableIngresos, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaContableIngresos, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Contable Ingresos"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCuentaContableAnticipo(eAccionSR valAction, string valCuentaContableAnticipo, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCuentaContableAnticipo, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Contable Anticipo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidSectorDeNegocio(eAccionSR valAction, string valSectorDeNegocio, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valSectorDeNegocio, true)) {
                BuildValidationInfo(MsgRequiredField("Sector De Negocio"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidClienteDesdeFecha(eAccionSR valAction, DateTime valClienteDesdeFecha, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valClienteDesdeFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsClienteIpl

} //End of namespace Galac.Saw.Uil.Clientes

