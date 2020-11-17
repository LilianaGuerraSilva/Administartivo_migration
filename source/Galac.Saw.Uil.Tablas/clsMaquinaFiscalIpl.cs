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
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas {
    public class clsMaquinaFiscalIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>> _Reglas;
        IList<MaquinaFiscal> _ListMaquinaFiscal;
        #endregion //Variables
        #region Propiedades

        public IList<MaquinaFiscal> ListMaquinaFiscal {
            get { return _ListMaquinaFiscal; }
            set { _ListMaquinaFiscal = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsMaquinaFiscalIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListMaquinaFiscal = new List<MaquinaFiscal>();
            ListMaquinaFiscal.Add(new MaquinaFiscal());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(MaquinaFiscal refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsMaquinaFiscalNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((MaquinaFiscal)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((MaquinaFiscal)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Máquina Fiscal"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((MaquinaFiscal)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((MaquinaFiscal)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            return GenerarProximoConsecutivoMaquinaFiscal();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        internal bool InsertRecord(MaquinaFiscal refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<MaquinaFiscal> vBusinessObject = new List<MaquinaFiscal>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(MaquinaFiscal refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<MaquinaFiscal> vBusinessObject = new List<MaquinaFiscal>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord(MaquinaFiscal refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<MaquinaFiscal> vBusinessObject = new List<MaquinaFiscal>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valConsecutivoMaquinaFiscal) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("ConsecutivoMaquinaFiscal", valConsecutivoMaquinaFiscal, 9);
            ListMaquinaFiscal = _Reglas.GetData(eProcessMessageType.SpName, "MaquinaFiscalGET", vParams.Get());
        }

        private string GenerarProximoConsecutivoMaquinaFiscal() {
            string vResult = "";
            RegistraCliente();
            XElement vResulset = _Reglas.QueryInfo(eProcessMessageType.Message, "ProximoConsecutivoMaquinaFiscal", Mfc.GetIntAsParam("Compania"));
            vResult = LibXml.GetPropertyString(vResulset, "ConsecutivoMaquinaFiscal");
            return vResult;
        }

        public bool ValidateAll(MaquinaFiscal refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoMaquinaFiscal(valAction, refRecord.ConsecutivoMaquinaFiscal, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidConsecutivoMaquinaFiscal(eAccionSR valAction, string valConsecutivoMaquinaFiscal, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valConsecutivoMaquinaFiscal, true)) {
                BuildValidationInfo(MsgRequiredField("Consecutivo"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsMaquinaFiscalIpl

} //End of namespace Galac.Saw.Uil.Tablas

