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
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Brl;

namespace Galac.Saw.Uil.Inventario {
    public class clsTallaIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Talla>, IList<Talla>> _Reglas;
        IList<Talla> _ListTalla;
        #endregion //Variables
        #region Propiedades

        public IList<Talla> ListTalla {
            get { return _ListTalla; }
            set { _ListTalla = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsTallaIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListTalla = new List<Talla>();
            ListTalla.Add(new Talla());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Talla refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Talla>, IList<Talla>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Inventario.clsTallaNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Talla)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Talla)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Talla"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Talla)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Talla)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Talla.Insertar")]
        internal bool InsertRecord(Talla refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Talla> vBusinessObject = new List<Talla>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Talla.Modificar")]
        internal bool UpdateRecord(Talla refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Talla> vBusinessObject = new List<Talla>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Talla.Eliminar")]
        internal bool DeleteRecord(Talla refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Talla> vBusinessObject = new List<Talla>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valCodigoTalla) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("CodigoTalla", valCodigoTalla, 3);
            ListTalla = _Reglas.GetData(eProcessMessageType.SpName, "TallaGET", vParams.Get());
        }

        public bool ValidateAll(Talla refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoTalla(valAction, refRecord.CodigoTalla, false);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidCodigoTalla(eAccionSR valAction, string valCodigoTalla, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoTalla, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Talla"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsTallaIpl

} //End of namespace Galac.Saw.Uil.Inventario

