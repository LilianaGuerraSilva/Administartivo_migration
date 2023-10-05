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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Dal.Venta {
    public class clsRenglonCobroDeFacturaDat: LibData, ILibDataDetailComponent<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>> {
        #region Variables
        RenglonCobroDeFactura _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private RenglonCobroDeFactura CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRenglonCobroDeFacturaDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(RenglonCobroDeFactura valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valRecord.NumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.CodigoFormaDelCobro, 5);
            vParams.AddInString("NumeroDelDocumento", valRecord.NumeroDelDocumento, 30);
            vParams.AddInInteger("CodigoBanco", valRecord.CodigoBanco);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vParams.AddInInteger("CodigoPuntoDeVenta", valRecord.CodigoPuntoDeVenta);
            vParams.AddInString("NumeroDocumentoAprobacion", valRecord.NumeroDocumentoAprobacion, 30);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInDecimal("CambioAMonedaLocal", valRecord.CambioAMonedaLocal, 4);
            vParams.AddInString("InfoAdicional", valRecord.InfoAdicional, 250);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(int valConsecutivoCompania, string valNumero, eTipoDocumentoFactura valTipoDeDocumento, List<RenglonCobroDeFactura> vListRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valNumero, 11);
            vParams.AddInEnum("TipoDeDocumento", (int)valTipoDeDocumento);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valConsecutivoCompania, valNumero, valTipoDeDocumento, vListRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(RenglonCobroDeFactura valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valRecord.NumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(RenglonCobroDeFactura valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valRecord.NumeroFactura, 11);
            vParams.AddInString("TipoDeDocumento", valRecord.TipoDeDocumentoAsString, 0);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(FacturaRapida valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valMaster.Numero, 11);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(int valConsecutivoCompania, string valNumero, eTipoDocumentoFactura valTipoDeDocumento, List<RenglonCobroDeFactura> valList) {
            int vConsecutivo = 1;
            foreach (RenglonCobroDeFactura item in valList) {
                item.ConsecutivoRenglon = vConsecutivo;
                vConsecutivo++;
            }
            XElement vXElement = new XElement("GpData",
                from vEntity in valList
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", valConsecutivoCompania),
                    new XElement("NumeroFactura", valNumero),
                    new XElement("TipoDeDocumento", vEntity.TipoDeDocumentoAsDB),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
                    new XElement("CodigoFormaDelCobro", vEntity.CodigoFormaDelCobro),
                    new XElement("NumeroDelDocumento", vEntity.NumeroDelDocumento),
                    new XElement("CodigoBanco", vEntity.CodigoBanco),
                    new XElement("Monto", vEntity.Monto),
                    new XElement("CodigoPuntoDeVenta", vEntity.CodigoPuntoDeVenta),
                    new XElement("NumeroDocumentoAprobacion", vEntity.NumeroDocumentoAprobacion),
                    new XElement("CodigoMoneda", vEntity.CodigoMoneda),
                    new XElement("CambioAMonedaLocal", vEntity.CambioAMonedaLocal),
                    new XElement("InfoAdicional", vEntity.InfoAdicional)));
            return vXElement;
        }

        private XElement BuildElementDetail(FacturaRapida valMaster) {
            XElement vXElement = new XElement("GpDataRenglonCobroDeFactura",
                from vEntity in valMaster.DetailRenglonCobroDeFactura
                select new XElement("GpDetailRenglonCobroDeFactura",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("NumeroFactura", valMaster.Numero),
                    new XElement("TipoDeDocumento", vEntity.TipoDeDocumentoAsDB),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
                    new XElement("CodigoFormaDelCobro", vEntity.CodigoFormaDelCobro),
                    new XElement("NumeroDelDocumento", vEntity.NumeroDelDocumento),
                    new XElement("CodigoBanco", vEntity.CodigoBanco),
                    new XElement("Monto", vEntity.Monto),
                    new XElement("CodigoPuntoDeVenta", vEntity.CodigoPuntoDeVenta),
                    new XElement("NumeroDocumentoAprobacion", vEntity.NumeroDocumentoAprobacion),
                    new XElement("InfoAdicional", vEntity.InfoAdicional)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>>

        IList<RenglonCobroDeFactura> ILibDataDetailComponent<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<RenglonCobroDeFactura> vResult = new List<RenglonCobroDeFactura>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<RenglonCobroDeFactura>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<RenglonCobroDeFactura>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Factura.Insertar")]
        LibResponse ILibDataDetailComponent<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>>.Insert(IList<RenglonCobroDeFactura> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RenglonCobroDeFacturaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<RenglonCobroDeFactura>, IList<RenglonCobroDeFactura>>

        public LibResponse InsertChild(int valConsecutivoCompania, string valNumeroFactura, eTipoDocumentoFactura valTipoDocumento, List<RenglonCobroDeFactura> valRecord) {
            LibResponse vResult = new LibResponse();
            StringBuilder vBuilder = ParametrosActualizacionDetail(valConsecutivoCompania, valNumeroFactura, valTipoDocumento, valRecord, eAccionSR.Insertar);
            LibGalac.Aos.Dal.LibDataScope valDataScope = new LibGalac.Aos.Dal.LibDataScope();
            vResult.Success = valDataScope.ExecSpNonQueryWithScope(valDataScope.ToSpName(DbSchema, "RenglonCobroDeFacturaInsDet"), vBuilder);
            return vResult;
        }

        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoFormaDelCobro(valAction, CurrentRecord.CodigoFormaDelCobro);
            vResult = IsValidCodigoBanco(valAction, CurrentRecord.CodigoBanco) && vResult;
            vResult = IsValidCodigoPuntoDeVenta(valAction, CurrentRecord.CodigoPuntoDeVenta) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidCodigoFormaDelCobro(eAccionSR valAction, string valCodigoFormaDelCobro) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoFormaDelCobro = LibString.Trim(valCodigoFormaDelCobro);
            if (LibString.IsNullOrEmpty(valCodigoFormaDelCobro, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Forma Del Cobro"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("SAW.FormaDelCobro", "Codigo", insDb.InsSql.ToSqlValue(valCodigoFormaDelCobro), true)) {
                    BuildValidationInfo("El valor asignado al campo Codigo Forma Del Cobro no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoBanco(eAccionSR valAction, int valCodigoBanco) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibString.IsNullOrEmpty(valCodigoBanco.ToString(), true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Banco"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Banco", "codigo", insDb.InsSql.ToSqlValue(valCodigoBanco), true)) {
                    BuildValidationInfo("El valor asignado al campo Codigo Banco no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoPuntoDeVenta(eAccionSR valAction, int valCodigoPuntoDeVenta) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibString.IsNullOrEmpty(valCodigoPuntoDeVenta.ToString(), true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Punto De Venta"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Banco", "codigo", insDb.InsSql.ToSqlValue(valCodigoPuntoDeVenta), true)) {
                    BuildValidationInfo("El valor asignado al campo Codigo Punto De Venta no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumeroFactura, eTipoDocumentoFactura valTipoDeDocumento, int valConsecutivoRenglon) {
            bool vResult = false;
            RenglonCobroDeFactura vRecordBusqueda = new RenglonCobroDeFactura();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroFactura = valNumeroFactura;
            vRecordBusqueda.TipoDeDocumentoAsEnum = valTipoDeDocumento;
            vRecordBusqueda.ConsecutivoRenglon = valConsecutivoRenglon;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".RenglonCobroDeFactura", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<FacturaRapida> refMaster) {
            bool vResult = false;
            IList<RenglonCobroDeFactura> vDetail = null;
            foreach (FacturaRapida vItemMaster in refMaster) {
                vItemMaster.DetailRenglonCobroDeFactura = new ObservableCollection<RenglonCobroDeFactura>();
                vDetail = new LibDatabase().LoadFromSp<RenglonCobroDeFactura>("dbo.Gp_RenglonCobroDeFacturaSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (RenglonCobroDeFactura vItemDetail in vDetail) {
                    vItemMaster.DetailRenglonCobroDeFactura.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonCobroDeFacturaDat

} //End of namespace Galac..Dal.ComponenteNoEspecificado

