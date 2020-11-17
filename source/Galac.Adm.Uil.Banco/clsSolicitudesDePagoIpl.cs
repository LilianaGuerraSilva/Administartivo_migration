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
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco {
    public class clsSolicitudesDePagoIpl: LibMROMF, ILibView {
        #region Variables
        ILibBusinessMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>> _Reglas;
        IList<SolicitudesDePago> _ListSolicitudesDePago;
        #endregion //Variables
        #region Propiedades

        public IList<SolicitudesDePago> ListSolicitudesDePago {
            get { return _ListSolicitudesDePago; }
            set { _ListSolicitudesDePago = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsSolicitudesDePagoIpl(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc):base(initAppMemoryInfo, initMfc) {
            ListSolicitudesDePago = new List<SolicitudesDePago>();
            ListSolicitudesDePago.Add(new SolicitudesDePago());
            Clear(ListSolicitudesDePago[0]);
        }
        #endregion //Constructores
        #region Metodos Generados
        internal void Clear(SolicitudesDePago refRecord) {
            refRecord.Clear();
            refRecord.ConsecutivoCompania = Mfc.GetInt("Compania");
			refRecord.DetailRenglonSolicitudesDePago.Clear();
        }

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessMasterComponent<IList<SolicitudesDePago>, IList<SolicitudesDePago>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.Banco.clsSolicitudesDePagoNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting
        #region ILibView

        void ILibView.Clear(object refRecord) {
            Clear((SolicitudesDePago)refRecord);
        }

        void IDisposable.Dispose() {
            throw new NotImplementedException();
        }

        bool ILibView.InsertRecord(object refRecord, out string outErrorMsg) {
            return InsertRecord((SolicitudesDePago)refRecord, out outErrorMsg);
        }

        string ILibView.MessageName {
            get { return "Solicitudes De Pago"; }
        }

        bool ILibView.UpdateRecord(object refRecord, eAccionSR valAction, out string outErrorMsg) {
            return UpdateRecord((SolicitudesDePago)refRecord, valAction,  out outErrorMsg);
        }

        bool ILibView.DeleteRecord(object refRecord) {
            return DeleteRecord((SolicitudesDePago)refRecord);
        }

        object ILibView.NextSequential(string valSequentialName) {
            return GenerarProximoConsecutivoSolicitud();
        }

        bool ILibView.SpecializedUpdateRecord(object refRecord, string valAction, out string outErrorMsg) {
            throw new NotImplementedException();
        }
        #endregion //ILibView

        [PrincipalPermission(SecurityAction.Demand, Role = "Solicitudes De Pago.Insertar")]
        internal bool InsertRecord(SolicitudesDePago refRecord, out string outErrorMsg) {
            bool vResult = false;
            if (ValidateAll(refRecord, eAccionSR.Insertar, out outErrorMsg)) {
                RegistraCliente();
                IList<SolicitudesDePago> vBusinessObject = new List<SolicitudesDePago>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Insertar, null, true).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Solicitudes De Pago.Modificar")]
        internal bool UpdateRecord(SolicitudesDePago refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = false;
            if (ValidateAll(refRecord, valAction, out outErrorMessage)) {
                RegistraCliente();
                IList<SolicitudesDePago> vBusinessObject = new List<SolicitudesDePago>();
                vBusinessObject.Add(refRecord);
                vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Modificar, null, true).Success;
            }
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Solicitudes De Pago.Eliminar")]
        internal bool DeleteRecord(SolicitudesDePago refRecord) {
            bool vResult = false;
            RegistraCliente();
            IList<SolicitudesDePago> vBusinessObject = new List<SolicitudesDePago>();
            vBusinessObject.Add(refRecord);
            vResult = _Reglas.DoAction(vBusinessObject, eAccionSR.Eliminar, null, true).Success;
            return vResult;
        }

        public void FindAndSetObject(int valConsecutivoCompania, int valConsecutivoSolicitud) {
            RegistraCliente();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valConsecutivoSolicitud);
            ListSolicitudesDePago = _Reglas.GetData(eProcessMessageType.SpName, "SolicitudesDePagoGET", vParams.Get(), true);
        }

        private int GenerarProximoConsecutivoSolicitud() {
            int vResult = 0;
            RegistraCliente();
            XElement vResulset = _Reglas.QueryInfo(eProcessMessageType.Message, "ProximoConsecutivoSolicitud", Mfc.GetIntAsParam("Compania"), false);
            //vResult = LibXml.GetPropertyString(vResulset, "ConsecutivoSolicitud");
            return vResult;
        }

        public bool ValidateAll(SolicitudesDePago refRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNumeroDocumentoOrigen(valAction, refRecord.NumeroDocumentoOrigen, false);
            vResult = IsValidFechaSolicitud(valAction, refRecord.FechaSolicitud, false) && vResult;
            vResult = IsValidStatus(valAction, refRecord.StatusAsEnum, false) && vResult;
            vResult = IsValidGeneradoPor(valAction, refRecord.GeneradoPorAsEnum, false) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        public bool IsValidConsecutivoSolicitud(eAccionSR valAction, int valConsecutivoSolicitud, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valConsecutivoSolicitud == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Solicitud"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidNumeroDocumentoOrigen(eAccionSR valAction, int valNumeroDocumentoOrigen, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (valNumeroDocumentoOrigen == 0) {
                BuildValidationInfo(MsgRequiredField("Numero Documento Origen"));
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidFechaSolicitud(eAccionSR valAction, DateTime valFechaSolicitud, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaSolicitud, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        public bool IsValidStatus(eAccionSR valAction, eStatusSolicitud valStatus, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            return vResult;
        }

        public bool IsValidGeneradoPor(eAccionSR valAction, eSolicitudGeneradaPor valGeneradoPor, bool valCleanInfoBeforeStart){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCleanInfoBeforeStart) {
                ClearValidationInfo();
            }
            return vResult;
        }

        public void InitDetailForInsert() {
            InitDetailRenSolDePagForInsert();
        }

        void InitDetailRenSolDePagForInsert() {
            if (ListSolicitudesDePago[0].DetailRenglonSolicitudesDePago == null) {
                ListSolicitudesDePago[0].DetailRenglonSolicitudesDePago = new GBindingList<RenglonSolicitudesDePago>();
            }
            Clear(ListSolicitudesDePago[0]);
            ListSolicitudesDePago[0].DetailRenglonSolicitudesDePago.Add(new RenglonSolicitudesDePago());
            ListSolicitudesDePago[0].DetailRenglonSolicitudesDePago.AllowNew = true;
            ListSolicitudesDePago[0].DetailRenglonSolicitudesDePago.AllowEdit = true;
            ListSolicitudesDePago[0].DetailRenglonSolicitudesDePago.AllowRemove = true;
        }
        #endregion //Metodos Generados


    } //End of class clsSolicitudesDePagoIpl

} //End of namespace Galac.Adm.Uil.Banco

