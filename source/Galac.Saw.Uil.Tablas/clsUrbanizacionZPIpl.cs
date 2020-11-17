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
    public class clsUrbanizacionZPIpl: LibMRO, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<UrbanizacionZP>, IList<UrbanizacionZP>> _Reglas;
        IList<UrbanizacionZP> _ListUrbanizacionZP;
        #endregion //Variables
        #region Propiedades

        public IList<UrbanizacionZP> ListUrbanizacionZP {
            get { return _ListUrbanizacionZP; }
            set { _ListUrbanizacionZP = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsUrbanizacionZPIpl() {
            ListUrbanizacionZP = new List<UrbanizacionZP>();
            ListUrbanizacionZP.Add(new UrbanizacionZP());
        }
        public clsUrbanizacionZPIpl(string initUrbanizacion) {
            ListUrbanizacionZP = new List<UrbanizacionZP>();
            FindAndSetObject(initUrbanizacion);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(UrbanizacionZP refRecord) {
            refRecord.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<UrbanizacionZP>, IList<UrbanizacionZP>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsUrbanizacionZPNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((UrbanizacionZP)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((UrbanizacionZP)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Urbanización - Zona Postal"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((UrbanizacionZP)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((UrbanizacionZP)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        internal bool InsertRecord(UrbanizacionZP refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<UrbanizacionZP> vBusinessObject = new List<UrbanizacionZP>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(UrbanizacionZP refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<UrbanizacionZP> vBusinessObject = new List<UrbanizacionZP>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord(UrbanizacionZP refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<UrbanizacionZP> vBusinessObject = new List<UrbanizacionZP>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(string valUrbanizacion) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Urbanizacion", valUrbanizacion, 30);
            ListUrbanizacionZP = _Reglas.GetData(eProcessMessageType.SpName, "UrbanizacionZPGET", vParams.Get());
        }

        public bool ValidateAll(UrbanizacionZP refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidUrbanizacion(valAction, refRecord.Urbanizacion, false);
            vResult = IsValidZonaPostal(valAction, refRecord.ZonaPostal, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidUrbanizacion(eAccionSR valAction, string valUrbanizacion, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valUrbanizacion, true)) {
                BuildValidationInfo(MsgRequiredField("Urbanización"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidZonaPostal(eAccionSR valAction, string valZonaPostal, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valZonaPostal, true)) {
                BuildValidationInfo(MsgRequiredField("Zona Postal"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsUrbanizacionZPIpl

} //End of namespace Galac.Saw.Uil.Tablas

