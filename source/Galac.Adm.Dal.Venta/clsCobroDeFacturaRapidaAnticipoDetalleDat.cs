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
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    public class clsCobroDeFacturaRapidaAnticipoDetalleDat: LibData, ILibDataDetailComponent<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>> {
        #region Variables
        CobroDeFacturaRapidaAnticipoDetalle _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CobroDeFacturaRapidaAnticipoDetalle CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaAnticipoDetalleDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CobroDeFacturaRapidaAnticipoDetalle valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.CodigoFormaDelCobro, 5);
            vParams.AddInInteger("CodigoAnticipo", valRecord.CodigoAnticipo);
            vParams.AddInString("NumeroAnticipo", valRecord.NumeroAnticipo, 20);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(CobroDeFacturaRapidaAnticipo valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.NumeroFactura, 5);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CobroDeFacturaRapidaAnticipoDetalle valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.CodigoFormaDelCobro, 5);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(CobroDeFacturaRapidaAnticipoDetalle valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(CobroDeFacturaRapidaAnticipo valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valMaster.NumeroFactura, 5);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(CobroDeFacturaRapidaAnticipo valEntidad) {
            List<CobroDeFacturaRapidaAnticipo> vListEntidades = new List<CobroDeFacturaRapidaAnticipo>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("NumeroFactura", vEntity.NumeroFactura),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(CobroDeFacturaRapidaAnticipo valMaster) {
            XElement vXElement = new XElement("GpDataCobroDeFacturaRapidaAnticipoDetalle",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaAnticipoDetalle
                select new XElement("GpDetailCobroDeFacturaRapidaAnticipoDetalle",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("CodigoFormaDelCobro", valMaster.NumeroFactura),
                    new XElement("CodigoAnticipo", vEntity.CodigoAnticipo),
                    new XElement("NumeroAnticipo", vEntity.NumeroAnticipo),
                    new XElement("Monto", vEntity.Monto)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>>

        IList<CobroDeFacturaRapidaAnticipoDetalle> ILibDataDetailComponent<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CobroDeFacturaRapidaAnticipoDetalle> vResult = new List<CobroDeFacturaRapidaAnticipoDetalle>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CobroDeFacturaRapidaAnticipoDetalle>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CobroDeFacturaRapidaAnticipoDetalle>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro de Factura con Anticipo.Insertar")]
        LibResponse ILibDataDetailComponent<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>>.Insert(IList<CobroDeFacturaRapidaAnticipoDetalle> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CobroDeFacturaRapidaAnticipoDetalleINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<CobroDeFacturaRapidaAnticipoDetalle>, IList<CobroDeFacturaRapidaAnticipoDetalle>>

        public bool InsertChild(CobroDeFacturaRapidaAnticipo valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaAnticipoDetalleInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoAnticipo(valAction, CurrentRecord.CodigoAnticipo);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidCodigoAnticipo(eAccionSR valAction, int valCodigoAnticipo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCodigoAnticipo == 0) {
                BuildValidationInfo(MsgRequiredField("Código del Anticipo"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoFormaDelCobro) {
            bool vResult = false;
            CobroDeFacturaRapidaAnticipoDetalle vRecordBusqueda = new CobroDeFacturaRapidaAnticipoDetalle();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoFormaDelCobro = valCodigoFormaDelCobro;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CobroDeFacturaRapidaAnticipoDetalle", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<CobroDeFacturaRapidaAnticipo>  refMaster) {
            bool vResult = false;
            IList<CobroDeFacturaRapidaAnticipoDetalle> vDetail = null;
            foreach (CobroDeFacturaRapidaAnticipo vItemMaster in refMaster) {
                vItemMaster.DetailCobroDeFacturaRapidaAnticipoDetalle = new ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle>();
                vDetail = new LibDatabase().LoadFromSp<CobroDeFacturaRapidaAnticipoDetalle>("Adm.Gp_CobroDeFacturaRapidaAnticipoDetalleSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (CobroDeFacturaRapidaAnticipoDetalle vItemDetail in vDetail) {
                    vItemMaster.DetailCobroDeFacturaRapidaAnticipoDetalle.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaAnticipoDetalleDat

} //End of namespace Galac.Adm.Dal.Venta

