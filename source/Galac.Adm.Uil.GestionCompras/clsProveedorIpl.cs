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
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Adm.Uil.GestionCompras {
    public class clsProveedorIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Proveedor>, IList<Proveedor>> _Reglas;
        IList<Proveedor> _ListProveedor;
        #endregion //Variables
        #region Propiedades

        public IList<Proveedor> ListProveedor {
            get { return _ListProveedor; }
            set { _ListProveedor = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsProveedorIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListProveedor = new List<Proveedor>();
            ListProveedor.Add(new Proveedor());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Proveedor refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Proveedor>, IList<Proveedor>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Proveedor)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Proveedor)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Proveedor"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Proveedor)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Proveedor)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            return GenerarProximoCodigoProveedor();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Insertar")]
        internal bool InsertRecord(Proveedor refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Proveedor> vBusinessObject = new List<Proveedor>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Modificar")]
        internal bool UpdateRecord(Proveedor refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Proveedor> vBusinessObject = new List<Proveedor>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Eliminar")]
        internal bool DeleteRecord(Proveedor refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Proveedor> vBusinessObject = new List<Proveedor>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigoProveedor) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoProveedor", valCodigoProveedor, 10);
            ListProveedor = _Reglas.GetData(eProcessMessageType.SpName, "ProveedorGET", vParams.Get());
        }

        private string GenerarProximoCodigoProveedor() {
            string vResult = "";
            RegistraCliente();
            XElement vResulset = _Reglas.QueryInfo(eProcessMessageType.Message, "ProximoCodigoProveedor", Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset, "CodigoProveedor");
            return vResult;
        }

        public bool ValidateAll(Proveedor refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoProveedor(valAction, refRecord.CodigoProveedor, false);
            vResult = IsValidTipodeProveedor(valAction, refRecord.TipodeProveedor, false);
            vResult = IsValidNombreProveedor(valAction, refRecord.NombreProveedor, false) && vResult;
            vResult = IsValidNumeroRIF(valAction, refRecord.NumeroRIF, false) && vResult;
            BuildValidationInfo(refRecord.TipoDePersonaDeCodigoRetencionAsEnum.ToString());
            BuildValidationInfo(refRecord.TipoDePersonaAsEnum.ToString());
            vResult = IsValidCodigoRetencionUsual(valAction, refRecord.CodigoRetencionUsual, refRecord.TipoDePersonaDeCodigoRetencionAsEnum, refRecord.TipoDePersonaAsEnum, false) && vResult;
            vResult = IsValidCtaBancaria(valAction, refRecord.NumeroCuentaBancaria, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigoProveedor(eAccionSR valAction, string valCodigoProveedor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombreProveedor(eAccionSR valAction, string valNombreProveedor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNombreProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre Proveedor"));
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

        public bool IsValidCodigoRetencionUsual(eAccionSR valAction, string valCodigoRetencionUsual, eTipodePersonaRetencion valTipoDePersonaDeCodigoretencionEnum, eTipodePersonaRetencion valTipoDePersonaEnum, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (AppMemoryInfo.GlobalValuesGetBool("RecordName", "UsaRetencion")) {
                if (LibString.IsNullOrEmpty(valCodigoRetencionUsual, true)) {
                    BuildValidationInfo(MsgRequiredField("Codigo Retencion Usual"));
                    vResult = false;
                }
                if (!LibString.IsNullOrEmpty(valCodigoRetencionUsual, true) && valTipoDePersonaDeCodigoretencionEnum != valTipoDePersonaEnum) {
                    BuildValidationInfo("El Código de Retención debe ajustarse al Tipo de Persona seleccionado.");
                    vResult = false;
                }
            }
           
            return vResult;
        }

        public bool IsValidCtaBancaria(eAccionSR valAction, string valCtaBancaria, bool valCleanInfoBeforeStart) {
            bool vResult = true;
            decimal number;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibText.Trim(valCtaBancaria).Length > 0 && LibText.Trim(valCtaBancaria).Length < 20) {
                BuildValidationInfo("El número de cuenta debe contener 20 caracteres");
                vResult = false;
            }
            if (LibText.Trim(valCtaBancaria).Length > 0 && !Decimal.TryParse(valCtaBancaria, out number)) {
                BuildValidationInfo("El número de cuenta debe contener únicamente caracteres numéricos");
                vResult = vResult && false;
            } else {
                vResult = vResult && true;
            }
            
            return vResult;
        }

        public bool IsValidTipodeProveedor(eAccionSR valAction, string valTipodeProveedor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valTipodeProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Tipo de Proveedor"));
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

        public bool IsValidBeneficiario(eAccionSR valAction, string valBeneficiario, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valBeneficiario, true)) {
                BuildValidationInfo(MsgRequiredField("Beneficiario"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados

        public string ValidaRifWeb(string valRif) {
            string vResult;
            IProveedorPdn insBrlProveedor = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
            vResult = insBrlProveedor.ValidaRifWeb(valRif);
            return vResult;
        }

    } //End of class clsProveedorIpl

} //End of namespace Galac.Adm.Uil.GestionCompras

