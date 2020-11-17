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
using Entity = Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo {
    public class clsVehiculoIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>> _Reglas;
        IList<Entity.Vehiculo> _ListVehiculo;
        #endregion //Variables
        #region Propiedades

        public IList<Entity.Vehiculo> ListVehiculo {
            get { return _ListVehiculo; }
            set { _ListVehiculo = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsVehiculoIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListVehiculo = new List<Entity.Vehiculo>();
            ListVehiculo.Add(new Entity.Vehiculo());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(Entity.Vehiculo refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Entity.Vehiculo>, IList<Entity.Vehiculo>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Vehiculo.clsVehiculoNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((Entity.Vehiculo)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((Entity.Vehiculo)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Vehiculo"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((Entity.Vehiculo)refRecord, valAction, out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((Entity.Vehiculo)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehiculo.Insertar")]
        internal bool InsertRecord(Entity.Vehiculo refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<Entity.Vehiculo> vBusinessObject = new List<Entity.Vehiculo>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehiculo.Modificar")]
        internal bool UpdateRecord(Entity.Vehiculo refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<Entity.Vehiculo> vBusinessObject = new List<Entity.Vehiculo>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vehiculo.Eliminar")]
        internal bool DeleteRecord(Entity.Vehiculo refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<Entity.Vehiculo> vBusinessObject = new List<Entity.Vehiculo>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, int valConsecutivo) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valConsecutivo);
            ListVehiculo = _Reglas.GetData(eProcessMessageType.SpName, "VehiculoGET", vParams.Get());
        }

        public bool ValidateAll(Entity.Vehiculo refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidPlaca(valAction, refRecord.Placa, false);
            vResult = IsValidNombreModelo(valAction, refRecord.NombreModelo, false) && vResult;
            vResult = IsValidCodigoColor(valAction, refRecord.CodigoColor, false) && vResult;
            vResult = IsValidCodigoCliente(valAction, refRecord.CodigoCliente, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidPlaca(eAccionSR valAction, string valPlaca, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valPlaca, true)) {
                BuildValidationInfo(MsgRequiredField("Placa"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNombreModelo(eAccionSR valAction, string valNombreModelo, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valNombreModelo, true)) {
                BuildValidationInfo(MsgRequiredField("Modelo Vehículo"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoColor(eAccionSR valAction, string valCodigoColor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoColor, true)) {
                BuildValidationInfo(MsgRequiredField("Color"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidCodigoCliente(eAccionSR valAction, string valCodigoCliente, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibString.IsNullOrEmpty(valCodigoCliente, true)) {
                BuildValidationInfo(MsgRequiredField("Código Cliente"));
                vResult = false;
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsVehiculoIpl

} //End of namespace Galac.Saw.Uil.Vehiculo

