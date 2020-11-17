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
    public class clsUnidadDeVentaIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<UnidadDeVenta>, IList<UnidadDeVenta>> _Reglas;
        IList<UnidadDeVenta> _ListUnidadDeVenta;
        #endregion //Variables
        #region Propiedades

        public IList<UnidadDeVenta> ListUnidadDeVenta {
            get { return _ListUnidadDeVenta; }
            set { _ListUnidadDeVenta = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsUnidadDeVentaIpl() {
            ListUnidadDeVenta = new List<UnidadDeVenta>();
            ListUnidadDeVenta.Add(new UnidadDeVenta());
        }
        public clsUnidadDeVentaIpl(string initNombre) {
            ListUnidadDeVenta = new List<UnidadDeVenta>();
            FindAndSetObject(initNombre);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(UnidadDeVenta refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<UnidadDeVenta>, IList<UnidadDeVenta>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsUnidadDeVentaNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((UnidadDeVenta)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((UnidadDeVenta)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Unidad De Venta"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((UnidadDeVenta)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((UnidadDeVenta)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        internal bool InsertRecord(UnidadDeVenta refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<UnidadDeVenta> vBusinessObject = new List<UnidadDeVenta>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(UnidadDeVenta refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<UnidadDeVenta> vBusinessObject = new List<UnidadDeVenta>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord(UnidadDeVenta refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<UnidadDeVenta> vBusinessObject = new List<UnidadDeVenta>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valNombre) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valNombre, 20);
            ListUnidadDeVenta = _Reglas.GetData(eProcessMessageType.SpName, "UnidadDeVentaGET", vParams.Get());
        }

        public bool ValidateAll(UnidadDeVenta refRecord, eAccionSR valAction, out string outErrorMessage) {
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
                BuildValidationInfo(MsgRequiredField("Unidad de Venta"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsUnidadDeVentaIpl

} //End of namespace Galac.Saw.Uil.Tablas

