using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Dal.Banco {
    public class clsRenglonSolicitudesDePagoDat: LibData, ILibDataDetailComponent<IList<RenglonSolicitudesDePago>, IList<RenglonSolicitudesDePago>> {
        #region Variables
        RenglonSolicitudesDePago _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private RenglonSolicitudesDePago CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRenglonSolicitudesDePagoDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(RenglonSolicitudesDePago valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valRecord.ConsecutivoSolicitud);
            vParams.AddInInteger("consecutivoRenglon", valRecord.consecutivoRenglon);
            vParams.AddInString("CuentaBancaria", valRecord.CuentaBancaria, 5);
            vParams.AddInInteger("ConsecutivoBeneficiario", valRecord.ConsecutivoBeneficiario);
            vParams.AddInEnum("FormaDePago", valRecord.FormaDePagoAsDB);
            vParams.AddInEnum("Status", valRecord.StatusAsDB);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 15);
            vParams.AddInBoolean("Contabilizado", valRecord.ContabilizadoAsBool);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(SolicitudesDePago valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valRecord.ConsecutivoSolicitud);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(RenglonSolicitudesDePago valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valRecord.ConsecutivoSolicitud);
            vParams.AddInInteger("consecutivoRenglon", valRecord.consecutivoRenglon);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(RenglonSolicitudesDePago valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        StringBuilder ParametrosDetail(SolicitudesDePago valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoSolicitud", valMaster.ConsecutivoSolicitud);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(SolicitudesDePago valEntidad) {
            List<SolicitudesDePago> vListEntidades = new List<SolicitudesDePago>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("ConsecutivoSolicitud", vEntity.ConsecutivoSolicitud),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(SolicitudesDePago valMaster) {
            XElement vXElement = new XElement("GpDataRenglonSolicitudesDePago",
                from vEntity in valMaster.DetailRenglonSolicitudesDePago
                select new XElement("GpDetailRenglonSolicitudesDePago",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoSolicitud", valMaster.ConsecutivoSolicitud),
                    new XElement("consecutivoRenglon", vEntity.consecutivoRenglon),
                    new XElement("CuentaBancaria", vEntity.CuentaBancaria),
                    new XElement("ConsecutivoBeneficiario", vEntity.ConsecutivoBeneficiario),
                    new XElement("FormaDePago", vEntity.FormaDePagoAsDB),
                    new XElement("Status", vEntity.StatusAsDB),
                    new XElement("Monto", vEntity.Monto),
                    new XElement("NumeroDocumento", vEntity.NumeroDocumento),
                    new XElement("Contabilizado", LibConvert.BoolToSN(vEntity.ContabilizadoAsBool))));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<RenglonSolicitudesDePago>, IList<RenglonSolicitudesDePago>>

        IList<RenglonSolicitudesDePago> ILibDataDetailComponent<IList<RenglonSolicitudesDePago>, IList<RenglonSolicitudesDePago>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<RenglonSolicitudesDePago> vResult = new List<RenglonSolicitudesDePago>();
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<RenglonSolicitudesDePago>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Renglon Solicitudes De Pago.Insertar")]
        LibResponse ILibDataDetailComponent<IList<RenglonSolicitudesDePago>, IList<RenglonSolicitudesDePago>>.Insert(IList<RenglonSolicitudesDePago> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                   LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
                    CurrentRecord.ConsecutivoSolicitud = insDb.NextLngConsecutive("Saw.RenglonSolicitudesDePago", "ConsecutivoSolicitud", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RenglonSolicitudesDePagoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<RenglonSolicitudesDePago>, IList<RenglonSolicitudesDePago>>

        public bool InsertChild(SolicitudesDePago valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "RenglonSolicitudesDePagoInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoSolicitud(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoSolicitud);
            vResult = IsValidCuentaBancaria(valAction, CurrentRecord.CuentaBancaria) && vResult;
            vResult = IsValidConsecutivoBeneficiario(valAction, CurrentRecord.ConsecutivoBeneficiario) && vResult;
            vResult = IsValidMonto(valAction, CurrentRecord.Monto) && vResult;
            vResult = IsValidNumeroDocumento(valAction, CurrentRecord.NumeroDocumento) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoSolicitud(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoSolicitud){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if (valConsecutivoSolicitud == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Solicitud"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCuentaBancaria(eAccionSR valAction, string valCuentaBancaria){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaBancaria = LibString.Trim(valCuentaBancaria);
            if (LibString.IsNullOrEmpty(valCuentaBancaria, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Bancaria"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidConsecutivoBeneficiario(eAccionSR valAction, int valConsecutivoBeneficiario){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoBeneficiario == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Beneficiario"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidMonto(eAccionSR valAction, decimal valMonto){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            //if (LibString.IsNullOrEmpty(valMonto, true)) {
            //    BuildValidationInfo(MsgRequiredField("Monto"));
            //    vResult = false;
            //}
            return vResult;
        }

        private bool IsValidNumeroDocumento(eAccionSR valAction, string valNumeroDocumento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroDocumento = LibString.Trim(valNumeroDocumento);
            if (LibString.IsNullOrEmpty(valNumeroDocumento, true)) {
                BuildValidationInfo(MsgRequiredField("Numero Documento "));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoSolicitud, int valconsecutivoRenglon) {
            bool vResult = false;
            RenglonSolicitudesDePago vRecordBusqueda = new RenglonSolicitudesDePago();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoSolicitud = valConsecutivoSolicitud;
            vRecordBusqueda.consecutivoRenglon = valconsecutivoRenglon;
            LibDatabase insDb = new LibDatabase(LibCkn.ConfigKeyForDbService);
            vResult = insDb.ExistsRecord("Saw.RenglonSolicitudesDePago", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<SolicitudesDePago>  refMaster) {
            bool vResult = false;
            IList<RenglonSolicitudesDePago> vDetail = null;
            foreach (SolicitudesDePago vItemMaster in refMaster) {
                vItemMaster.DetailRenglonSolicitudesDePago = new GBindingList<RenglonSolicitudesDePago>();
                vDetail = new LibDatabase(LibCkn.ConfigKeyForDbService).LoadFromSp<RenglonSolicitudesDePago>("Saw.Gp_RenglonSolicitudesDePagoSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (RenglonSolicitudesDePago vItemDetail in vDetail) {
                    vItemMaster.DetailRenglonSolicitudesDePago.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonSolicitudesDePagoDat

} //End of namespace Galac.Dbo.Dal.RenglonSolicitudesDePago

