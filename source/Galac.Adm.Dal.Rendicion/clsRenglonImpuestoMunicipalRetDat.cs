using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    public class clsRenglonImpuestoMunicipalRetDat: LibData, ILibDataDetailComponent<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>> {
        #region Variables
        RenglonImpuestoMunicipalRet _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private RenglonImpuestoMunicipalRet CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRenglonImpuestoMunicipalRetDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(RenglonImpuestoMunicipalRet valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInInteger("ConsecutivoCxp", valRecord.ConsecutivoCxp);
            vParams.AddInString("CodigoRetencion", valRecord.CodigoRetencion, 10);
            vParams.AddInDecimal("MontoBaseImponible", valRecord.MontoBaseImponible, 2);
            vParams.AddInDecimal("AlicuotaRetencion", valRecord.AlicuotaRetencion, 2);
            vParams.AddInDecimal("MontoRetencion", valRecord.MontoRetencion, 2);
            vParams.AddInEnum("TipoDeTransaccion", valRecord.TipoDeTransaccionAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(CxP valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.ConsecutivoCxP);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(RenglonImpuestoMunicipalRet valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(RenglonImpuestoMunicipalRet valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(CxP valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valMaster.ConsecutivoCxP);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(CxP valEntidad) {
            List<CxP> vListEntidades = new List<CxP>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("ConsecutivoCxP", vEntity.ConsecutivoCxP),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(CxP valMaster) {
            XElement vXElement = new XElement("GpDataRenglonImpuestoMunicipalRet",
                from vEntity in valMaster.DetailRenglonImpuestoMunicipalRet
                select new XElement("GpDetailRenglonImpuestoMunicipalRet",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("Consecutivo", valMaster.ConsecutivoCxP),
                    new XElement("ConsecutivoCxp", vEntity.ConsecutivoCxp),
                    new XElement("CodigoRetencion", vEntity.CodigoRetencion),
                    new XElement("MontoBaseImponible", vEntity.MontoBaseImponible),
                    new XElement("AlicuotaRetencion", vEntity.AlicuotaRetencion),
                    new XElement("MontoRetencion", vEntity.MontoRetencion),
                    new XElement("TipoDeTransaccion", vEntity.TipoDeTransaccionAsDB),
                    new XElement("NombreOperador", vEntity.NombreOperador),
                    new XElement("FechaUltimaModificacion", vEntity.FechaUltimaModificacion)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>>

        IList<RenglonImpuestoMunicipalRet> ILibDataDetailComponent<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<RenglonImpuestoMunicipalRet> vResult = new List<RenglonImpuestoMunicipalRet>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<RenglonImpuestoMunicipalRet>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Renglon Impuesto Municipal Ret.Insertar")]
        LibResponse ILibDataDetailComponent<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>>.Insert(IList<RenglonImpuestoMunicipalRet> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RenglonImpuestoMunicipalRetINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<RenglonImpuestoMunicipalRet>, IList<RenglonImpuestoMunicipalRet>>

        public bool InsertChild(CxP valRecord, LibDataScope insDb) {
            bool vResult = false;
            vResult = insDb.ExecSpNonQueryWithScope(insDb.ToSpName(DbSchema, "RenglonImpuestoMunicipalRetInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCxp(valAction, CurrentRecord.ConsecutivoCxp);
            vResult = IsValidCodigoRetencion(valAction, CurrentRecord.CodigoRetencion) && vResult;
            vResult = IsValidMontoBaseImponible(valAction, CurrentRecord.MontoBaseImponible) && vResult;
            vResult = IsValidAlicuotaRetencion(valAction, CurrentRecord.AlicuotaRetencion) && vResult;
            vResult = IsValidMontoRetencion(valAction, CurrentRecord.MontoRetencion) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCxp(eAccionSR valAction, int valConsecutivoCxp){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCxp == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Cxp"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoRetencion(eAccionSR valAction, string valCodigoRetencion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoRetencion = LibString.Trim(valCodigoRetencion);
            if (LibString.IsNullOrEmpty(valCodigoRetencion, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Retencion"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidMontoBaseImponible(eAccionSR valAction, decimal valMontoBaseImponible){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidAlicuotaRetencion(eAccionSR valAction, decimal valAlicuotaRetencion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidMontoRetencion(eAccionSR valAction, decimal valMontoRetencion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            bool vResult = false;
            RenglonImpuestoMunicipalRet vRecordBusqueda = new RenglonImpuestoMunicipalRet();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("dbo.RenglonImpuestoMunicipalRet", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<CxP>  refMaster) {
            bool vResult = false;
            IList<RenglonImpuestoMunicipalRet> vDetail = null;
            foreach (CxP vItemMaster in refMaster) {
                vItemMaster.DetailRenglonImpuestoMunicipalRet = new ObservableCollection<RenglonImpuestoMunicipalRet>();
                vDetail = new LibDatabase().LoadFromSp<RenglonImpuestoMunicipalRet>("dbo.Gp_RenglonImpuestoMunicipalRetSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (RenglonImpuestoMunicipalRet vItemDetail in vDetail) {
                    vItemMaster.DetailRenglonImpuestoMunicipalRet.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonImpuestoMunicipalRetDat

} //End of namespace Galac.Dbo.Dal.CajaChica

