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
    public class clsDetalleDeRendicionDat: LibData, ILibDataDetailComponent<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>> {
        #region Variables
        DetalleDeRendicion _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private DetalleDeRendicion CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsDetalleDeRendicionDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(DetalleDeRendicion valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoRendicion", valRecord.ConsecutivoRendicion);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 25);
            vParams.AddInString("NumeroControl", valRecord.NumeroControl, 20);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInString("CodigoProveedor", valRecord.CodigoProveedor, 10);
            vParams.AddInDecimal("MontoExento", valRecord.MontoExento, 2);
            vParams.AddInDecimal("MontoGravable", valRecord.MontoGravable, 2);
            vParams.AddInDecimal("MontoIVA", valRecord.MontoIVA, 2);
            vParams.AddInDecimal("MontoRetencion", valRecord.MontoRetencion, 2);
            vParams.AddInBoolean("AplicaParaLibroDeCompras", valRecord.AplicaParaLibroDeComprasAsBool);
            vParams.AddInString("ObservacionesCxP", valRecord.ObservacionesCxP, 50);
            vParams.AddInEnum("GeneradaPor", valRecord.GeneradaPorAsDB);
            vParams.AddInBoolean("AplicaIvaAlicuotaEspecial", valRecord.AplicaIvaAlicuotaEspecialAsBool);
            vParams.AddInDecimal("MontoGravableAlicuotaEspecial1", valRecord.MontoGravableAlicuotaEspecial1, 2);
            vParams.AddInDecimal("MontoIVAAlicuotaEspecial1", valRecord.MontoIVAAlicuotaEspecial1, 2);
            vParams.AddInDecimal("PorcentajeIvaAlicuotaEspecial1", valRecord.PorcentajeIvaAlicuotaEspecial1, 2);
            vParams.AddInDecimal("MontoGravableAlicuotaEspecial2", valRecord.MontoGravableAlicuotaEspecial2, 2);
            vParams.AddInDecimal("MontoIVAAlicuotaEspecial2", valRecord.MontoIVAAlicuotaEspecial2, 2);
            vParams.AddInDecimal("PorcentajeIvaAlicuotaEspecial2", valRecord.PorcentajeIvaAlicuotaEspecial2, 2);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(Rendicion valRecord, eAccionSR eAccionSR, string ValidaMaestro) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoRendicion", valRecord.Consecutivo);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vParams.AddInInteger("TimeStampAsInt",(int)valRecord.fldTimeStamp);
            vParams.AddInString("ValidaMaestro", ValidaMaestro,1);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(DetalleDeRendicion valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoRendicion", valRecord.ConsecutivoRendicion);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(DetalleDeRendicion valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoRendicion", valRecord.ConsecutivoRendicion);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(Rendicion valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoRendicion", valMaster.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(Rendicion valEntidad) {
            List<Rendicion> vListEntidades = new List<Rendicion>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(Rendicion valMaster) {
            XElement vXElement = new XElement("GpDataDetalleDeRendicion",
                from vEntity in valMaster.DetailDetalleDeRendicion
                select new XElement("GpDetailDetalleDeRendicion",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoRendicion", valMaster.Consecutivo),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
                    new XElement("NumeroDocumento", vEntity.NumeroDocumento),
                    new XElement("NumeroControl", vEntity.NumeroControl),
                    new XElement("Fecha", vEntity.Fecha),
                    new XElement("CodigoProveedor", vEntity.CodigoProveedor),
                    new XElement("MontoExento", vEntity.MontoExento),
                    new XElement("MontoGravable", vEntity.MontoGravable),
                    new XElement("MontoIVA", vEntity.MontoIVA),
                    new XElement("MontoRetencion", vEntity.MontoRetencion),
                    new XElement("AplicaParaLibroDeCompras", LibConvert.BoolToSN(vEntity.AplicaParaLibroDeComprasAsBool)),
                    new XElement("ObservacionesCxP", vEntity.ObservacionesCxP),
                    new XElement("GeneradaPor", vEntity.GeneradaPorAsDB),
                    new XElement("AplicaIvaAlicuotaEspecial", LibConvert.BoolToSN(vEntity.AplicaIvaAlicuotaEspecialAsBool)),
                    new XElement("MontoGravableAlicuotaEspecial1", vEntity.MontoGravableAlicuotaEspecial1),
                    new XElement("MontoIVAAlicuotaEspecial1", vEntity.MontoIVAAlicuotaEspecial1),
                    new XElement("PorcentajeIvaAlicuotaEspecial1", vEntity.PorcentajeIvaAlicuotaEspecial1),
                    new XElement("MontoGravableAlicuotaEspecial2", vEntity.MontoGravableAlicuotaEspecial2),
                    new XElement("MontoIVAAlicuotaEspecial2", vEntity.MontoIVAAlicuotaEspecial2),
                    new XElement("PorcentajeIvaAlicuotaEspecial2", vEntity.PorcentajeIvaAlicuotaEspecial2)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>>

        IList<DetalleDeRendicion> ILibDataDetailComponent<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<DetalleDeRendicion> vResult = new List<DetalleDeRendicion>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<DetalleDeRendicion>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<DetalleDeRendicion>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Reposicion de Caja Chica.Insertar")]
        LibResponse ILibDataDetailComponent<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>>.Insert(IList<DetalleDeRendicion> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "DetalleDeRendicionINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<DetalleDeRendicion>, IList<DetalleDeRendicion>>

        public bool InsertChild(Rendicion valRecord, LibDataScope insTrn,string validaMaestro) {
            bool vResult = false;
            
            vResult = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "DetalleDeRendicionInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar,validaMaestro));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidNumeroDocumento(valAction, CurrentRecord.NumeroDocumento);
            vResult = IsValidNumeroControl(valAction, CurrentRecord.NumeroControl) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidCodigoProveedor(valAction, CurrentRecord.CodigoProveedor) && vResult;
            vResult = IsValidMontoExento(valAction, CurrentRecord.MontoExento) && vResult;
            vResult = IsValidMontoGravable(valAction, CurrentRecord.MontoGravable) && vResult;
            vResult = IsValidMontoIVA(valAction, CurrentRecord.MontoIVA) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidNumeroDocumento(eAccionSR valAction, string valNumeroDocumento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroDocumento = LibString.Trim(valNumeroDocumento);
            if (LibString.IsNullOrEmpty(valNumeroDocumento, true)) {
                BuildValidationInfo(MsgRequiredField("Número del Documento"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNumeroControl(eAccionSR valAction, string valNumeroControl){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroControl = LibString.Trim(valNumeroControl);
            if (LibString.IsNullOrEmpty(valNumeroControl, true)) {
                BuildValidationInfo(MsgRequiredField("Número de Control"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, DateTime valFecha){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoProveedor(eAccionSR valAction, string valCodigoProveedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoProveedor = LibString.Trim(valCodigoProveedor);
            if (LibString.IsNullOrEmpty(valCodigoProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Proveedor"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidMontoExento(eAccionSR valAction, decimal valMontoExento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidMontoGravable(eAccionSR valAction, decimal valMontoGravable){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidMontoIVA(eAccionSR valAction, decimal valMontoIVA){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoRendicion, int valConsecutivoRenglon) {
            bool vResult = false;
            DetalleDeRendicion vRecordBusqueda = new DetalleDeRendicion();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoRendicion = valConsecutivoRendicion;
            vRecordBusqueda.ConsecutivoRenglon = valConsecutivoRenglon;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".DetalleDeRendicion", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<Rendicion>  refMaster) {
            bool vResult = false;
            IList<DetalleDeRendicion> vDetail = null;
            foreach (Rendicion vItemMaster in refMaster) {
                vItemMaster.DetailDetalleDeRendicion = new ObservableCollection<DetalleDeRendicion>();
                vDetail = new LibDatabase().LoadFromSp<DetalleDeRendicion>("Adm.Gp_DetalleDeRendicionSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (DetalleDeRendicion vItemDetail in vDetail) {
                    vItemMaster.DetailDetalleDeRendicion.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsDetalleDeRendicionDat

} //End of namespace Galac.Saw.Dal.Rendicion

