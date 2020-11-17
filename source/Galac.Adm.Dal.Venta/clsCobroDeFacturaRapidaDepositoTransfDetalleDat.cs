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
    public class clsCobroDeFacturaRapidaDepositoTransfDetalleDat: LibData, ILibDataDetailComponent<IList<CobroDeFacturaRapidaDepositoTransfDetalle>, IList<CobroDeFacturaRapidaDepositoTransfDetalle>> {
        #region Variables
        CobroDeFacturaRapidaDepositoTransfDetalle _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CobroDeFacturaRapidaDepositoTransfDetalle CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaDepositoTransfDetalleDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CobroDeFacturaRapidaDepositoTransfDetalle valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.CodigoFormaDelCobro, 5);
            vParams.AddInString("NumeroDelDocumento", valRecord.NumeroDelDocumento, 30);
            vParams.AddInInteger("CodigoBanco", valRecord.CodigoBanco);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vParams.AddInString("NumeroDocumentoAprobacion", valRecord.NumeroDocumentoAprobacion, 30);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(CobroDeFacturaRapidaDepositoTransf valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.NumeroFactura, 5);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CobroDeFacturaRapidaDepositoTransfDetalle valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(CobroDeFacturaRapidaDepositoTransfDetalle valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(CobroDeFacturaRapidaDepositoTransf valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valMaster.NumeroFactura, 5);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(CobroDeFacturaRapidaDepositoTransf valEntidad) {
            List<CobroDeFacturaRapidaDepositoTransf> vListEntidades = new List<CobroDeFacturaRapidaDepositoTransf>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("NumeroFactura", vEntity.NumeroFactura),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(CobroDeFacturaRapidaDepositoTransf valMaster) {
            XElement vXElement = new XElement("GpDataCobroDeFacturaRapidaDepositoTransfDetalle",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaDepositoTransfDetalle
                select new XElement("GpDetailCobroDeFacturaRapidaDepositoTransfDetalle",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("CodigoFormaDelCobro", valMaster.NumeroFactura),
                    new XElement("NumeroDelDocumento", vEntity.NumeroDelDocumento),
                    new XElement("CodigoBanco", vEntity.CodigoBanco),
                    new XElement("Monto", vEntity.Monto),
                    new XElement("NumeroDocumentoAprobacion", vEntity.NumeroDocumentoAprobacion)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<CobroDeFacturaRapidaDepositoTransfDetalle>, IList<CobroDeFacturaRapidaDepositoTransfDetalle>>

        IList<CobroDeFacturaRapidaDepositoTransfDetalle> ILibDataDetailComponent<IList<CobroDeFacturaRapidaDepositoTransfDetalle>, IList<CobroDeFacturaRapidaDepositoTransfDetalle>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CobroDeFacturaRapidaDepositoTransfDetalle> vResult = new List<CobroDeFacturaRapidaDepositoTransfDetalle>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CobroDeFacturaRapidaDepositoTransfDetalle>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CobroDeFacturaRapidaDepositoTransfDetalle>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Deposito Transferencia.Insertar")]
        LibResponse ILibDataDetailComponent<IList<CobroDeFacturaRapidaDepositoTransfDetalle>, IList<CobroDeFacturaRapidaDepositoTransfDetalle>>.Insert(IList<CobroDeFacturaRapidaDepositoTransfDetalle> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CobroDeFacturaRapidaDepositoTransfDetalleINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<CobroDeFacturaRapidaDepositoTransfDetalle>, IList<CobroDeFacturaRapidaDepositoTransfDetalle>>

        public bool InsertChild(CobroDeFacturaRapidaDepositoTransf valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaDepositoTransfDetalleInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoBanco(valAction, CurrentRecord.CodigoBanco);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidCodigoBanco(eAccionSR valAction, int valCodigoBanco){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCodigoBanco == 0) {
                BuildValidationInfo(MsgRequiredField("Código del Banco"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoFormaDelCobro) {
            bool vResult = false;
            CobroDeFacturaRapidaDepositoTransfDetalle vRecordBusqueda = new CobroDeFacturaRapidaDepositoTransfDetalle();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoFormaDelCobro = valCodigoFormaDelCobro;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CobroDeFacturaRapidaDepositoTransfDetalle", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<CobroDeFacturaRapidaDepositoTransf>  refMaster) {
            bool vResult = false;
            IList<CobroDeFacturaRapidaDepositoTransfDetalle> vDetail = null;
            foreach (CobroDeFacturaRapidaDepositoTransf vItemMaster in refMaster) {
                vItemMaster.DetailCobroDeFacturaRapidaDepositoTransfDetalle = new ObservableCollection<CobroDeFacturaRapidaDepositoTransfDetalle>();
                vDetail = new LibDatabase().LoadFromSp<CobroDeFacturaRapidaDepositoTransfDetalle>("Adm.Gp_CobroDeFacturaRapidaDepositoTransfDetalleSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (CobroDeFacturaRapidaDepositoTransfDetalle vItemDetail in vDetail) {
                    vItemMaster.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaDepositoTransfDetalleDat

} //End of namespace Galac.Adm.Dal.Venta

