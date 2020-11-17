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
    public class clsZonaCobranzaIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessComponentWithSearch<IList<ZonaCobranza>, IList<ZonaCobranza>> _Reglas;
        IList<ZonaCobranza> _ListZonaCobranza;
        #endregion //Variables
        #region Propiedades

        public IList<ZonaCobranza> ListZonaCobranza {
            get { return _ListZonaCobranza; }
            set { _ListZonaCobranza = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsZonaCobranzaIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListZonaCobranza = new List<ZonaCobranza>();
            ListZonaCobranza.Add( new ZonaCobranza());
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(ZonaCobranza refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<ZonaCobranza>, IList<ZonaCobranza>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsZonaCobranzaNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((ZonaCobranza)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((ZonaCobranza)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Zona Cobranza"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((ZonaCobranza)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((ZonaCobranza)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            throw new NotImplementedException();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        internal bool InsertRecord(ZonaCobranza refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<ZonaCobranza> vBusinessObject = new List<ZonaCobranza>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Modificar")]
        internal bool UpdateRecord(ZonaCobranza refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<ZonaCobranza> vBusinessObject = new List<ZonaCobranza>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Eliminar")]
        internal bool DeleteRecord(ZonaCobranza refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<ZonaCobranza> vBusinessObject = new List<ZonaCobranza>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, string valNombre) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Nombre", valNombre, 100);
            ListZonaCobranza = _Reglas.GetData(eProcessMessageType.SpName, "ZonaCobranzaGET", vParams.Get());
        }

        public bool ValidateAll(ZonaCobranza refRecord, eAccionSR valAction, out string outErrorMessage) {
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


    } //End of class clsZonaCobranzaIpl

} //End of namespace Galac.Saw.Uil.Tablas

