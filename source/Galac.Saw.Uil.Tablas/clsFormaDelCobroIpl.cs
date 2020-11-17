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
    public class clsFormaDelCobroIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>> _Reglas;
        IList<FormaDelCobro> _ListFormaDelCobro;
        #endregion //Variables
        #region Propiedades

        public IList<FormaDelCobro> ListFormaDelCobro {
            get { return _ListFormaDelCobro; }
            set { _ListFormaDelCobro = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsFormaDelCobroIpl() {
            ListFormaDelCobro = new List<FormaDelCobro>();
            ListFormaDelCobro.Add(new FormaDelCobro());
        }
        public clsFormaDelCobroIpl(string initCodigo) {
            ListFormaDelCobro = new List<FormaDelCobro>();
            FindAndSetObject(initCodigo);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(FormaDelCobro refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((FormaDelCobro)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((FormaDelCobro)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Forma Del Cobro"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((FormaDelCobro)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((FormaDelCobro)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            return GenerarProximoCodigo();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        internal bool InsertRecord(FormaDelCobro refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<FormaDelCobro> vBusinessObject = new List<FormaDelCobro>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(FormaDelCobro refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<FormaDelCobro> vBusinessObject = new List<FormaDelCobro>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord(FormaDelCobro refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<FormaDelCobro> vBusinessObject = new List<FormaDelCobro>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valCodigo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Codigo", valCodigo, 5);
            ListFormaDelCobro = _Reglas.GetData(eProcessMessageType.SpName, "FormaDelCobroGET", vParams.Get());
        }

        private string GenerarProximoCodigo() {
            string vResult = "";
            RegistraCliente();
            XElement vResulset = _Reglas.QueryInfo(eProcessMessageType.Message, "ProximoCodigo", null);
            vResult = LibXml.GetPropertyString(vResulset, "Codigo");
            return vResult;
        }

        public bool ValidateAll(FormaDelCobro refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigo(valAction, refRecord.Codigo, false);
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
        #endregion //Metodos Generados


    } //End of class clsFormaDelCobroIpl

} //End of namespace Galac.Saw.Uil.Tablas

