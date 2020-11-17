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
    public class clsModeloIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Modelo>, IList<Modelo>> _Reglas;
        IList<Modelo> _ListModelo;
        #endregion //Variables
        #region Propiedades

        public IList<Modelo> ListModelo {
            get { return _ListModelo; }
            set { _ListModelo = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsModeloIpl() {
            ListModelo = new List<Modelo>();
            ListModelo.Add(new Modelo());
        }
        public clsModeloIpl(string initNombre) {
            ListModelo = new List<Modelo>();
            FindAndSetObject(initNombre);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Modelo refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Modelo>, IList<Modelo>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Vehiculo.clsModeloNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Modelo)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Modelo)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Modelo"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Modelo)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Modelo)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Modelo.Insertar")]
        internal bool InsertRecord(Modelo refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Modelo> vBusinessObject = new List<Modelo>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modelo.Modificar")]
        internal bool UpdateRecord(Modelo refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Modelo> vBusinessObject = new List<Modelo>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Modelo.Eliminar")]
        internal bool DeleteRecord(Modelo refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Modelo> vBusinessObject = new List<Modelo>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valNombre) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valNombre, 20);
            ListModelo = _Reglas.GetData(eProcessMessageType.SpName, "ModeloGET", vParams.Get());
        }

        public bool ValidateAll(Modelo refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNombre(valAction, refRecord.Nombre, false);
            vResult = IsValidMarca(valAction, refRecord.Marca, false) && vResult;
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

        public bool IsValidMarca(eAccionSR valAction, string valMarca, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valMarca, true)) {
                BuildValidationInfo(MsgRequiredField("Marca"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsModeloIpl

} //End of namespace Galac.Saw.Uil.Vehiculo

