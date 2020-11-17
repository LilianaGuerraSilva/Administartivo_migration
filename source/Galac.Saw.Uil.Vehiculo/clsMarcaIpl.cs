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
using Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo {
    public class clsMarcaIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Marca>, IList<Marca>> _Reglas;
        IList<Marca> _ListMarca;
        #endregion //Variables
        #region Propiedades

        public IList<Marca> ListMarca {
            get { return _ListMarca; }
            set { _ListMarca = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsMarcaIpl() {
            ListMarca = new List<Marca>();
            ListMarca.Add(new Marca());
        }
        public clsMarcaIpl(string initNombre) {
            ListMarca = new List<Marca>();
            FindAndSetObject(initNombre);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Marca refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Marca>, IList<Marca>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Vehiculo.clsMarcaNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Marca)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Marca)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Marca"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Marca)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Marca)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Marca.Insertar")]
        internal bool InsertRecord(Marca refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Marca> vBusinessObject = new List<Marca>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Marca.Modificar")]
        internal bool UpdateRecord(Marca refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Marca> vBusinessObject = new List<Marca>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Marca.Eliminar")]
        internal bool DeleteRecord(Marca refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Marca> vBusinessObject = new List<Marca>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valNombre) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valNombre, 20);
            ListMarca = _Reglas.GetData(eProcessMessageType.SpName, "MarcaGET", vParams.Get());
        }

        public bool ValidateAll(Marca refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNombre(valAction, refRecord.Nombre, false);
            outErrorMessage = Information.ToString();
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
        #endregion //Metodos Generados


    } //End of class clsMarcaIpl

} //End of namespace Galac.Saw.Uil.Vehiculo

