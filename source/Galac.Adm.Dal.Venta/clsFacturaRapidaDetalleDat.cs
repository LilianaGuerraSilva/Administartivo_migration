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
using Galac.Comun.Ccl.SttDef;

namespace Galac.Adm.Dal.Venta {
    public class clsFacturaRapidaDetalleDat: LibData, ILibDataDetailComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>> {
        #region Variables
        FacturaRapidaDetalle _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private FacturaRapidaDetalle CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsFacturaRapidaDetalleDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(FacturaRapidaDetalle valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valRecord.NumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vParams.AddInString("Articulo", valRecord.Articulo, 30);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 255);
            vParams.AddInString("CodigoVendedor1", valRecord.CodigoVendedor1, 5);
            vParams.AddInString("CodigoVendedor2", valRecord.CodigoVendedor2, 5);
            vParams.AddInString("CodigoVendedor3", valRecord.CodigoVendedor3, 5);
            vParams.AddInEnum("AlicuotaIva", valRecord.AlicuotaIvaAsDB);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 2);
            vParams.AddInDecimal("PrecioSinIVA", valRecord.PrecioSinIVA, 2);
            vParams.AddInDecimal("PrecioConIVA", valRecord.PrecioConIVA, 2);
            vParams.AddInDecimal("PorcentajeDescuento", valRecord.PorcentajeDescuento, 2);
            vParams.AddInDecimal("TotalRenglon", valRecord.TotalRenglon, 2);
            vParams.AddInDecimal("PorcentajeBaseImponible", valRecord.PorcentajeBaseImponible, 2);
            vParams.AddInString("Serial", valRecord.Serial, 50);
            vParams.AddInString("Rollo", valRecord.Rollo, 20);
            vParams.AddInDecimal("PorcentajeAlicuota", valRecord.PorcentajeAlicuota, 2);
            vParams.AddInString("CampoExtraEnRenglonFactura1", valRecord.CampoExtraEnRenglonFactura1, 60);
            vParams.AddInString("CampoExtraEnRenglonFactura2", valRecord.CampoExtraEnRenglonFactura2, 60);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(FacturaRapida valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valRecord.Numero, 11);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(FacturaRapidaDetalle valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(FacturaRapidaDetalle valRecord) {
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
            vParams.AddInEnum("TipoDeDocumento", LibConvert.EnumToDbValue((int)valMaster.TipoDeDocumentoAsEnum));
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(FacturaRapida valEntidad) {
            List<FacturaRapida> vListEntidades = new List<FacturaRapida>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Numero", vEntity.Numero),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(FacturaRapida valMaster) {
            XElement vXElement = new XElement("GpDatarenglonFactura",
                from vEntity in valMaster.DetailFacturaRapidaDetalle
                select new XElement("GpDetailrenglonFactura",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("NumeroFactura", valMaster.Numero),
                    new XElement("TipoDeDocumento", vEntity.TipoDeDocumentoAsDB),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
                    new XElement("Articulo", vEntity.Articulo),
                    new XElement("Descripcion", vEntity.Descripcion),
                    new XElement("CodigoVendedor1", vEntity.CodigoVendedor1),
                    new XElement("CodigoVendedor2", vEntity.CodigoVendedor2),
                    new XElement("CodigoVendedor3", vEntity.CodigoVendedor3),
                    new XElement("AlicuotaIva", vEntity.AlicuotaIvaAsDB),
                    new XElement("Cantidad", vEntity.Cantidad),
                    new XElement("PrecioSinIVA", vEntity.PrecioSinIVA),
                    new XElement("PrecioConIVA", vEntity.PrecioConIVA),
                    new XElement("PorcentajeDescuento", vEntity.PorcentajeDescuento),
                    new XElement("TotalRenglon", vEntity.TotalRenglon),
                    new XElement("PorcentajeBaseImponible", vEntity.PorcentajeBaseImponible),
                    new XElement("Serial", vEntity.Serial),
                    new XElement("Rollo", vEntity.Rollo),
                    new XElement("PorcentajeAlicuota", vEntity.PorcentajeAlicuota),
                    new XElement("CampoExtraEnRenglonFactura1", vEntity.CampoExtraEnRenglonFactura1),
                    new XElement("CampoExtraEnRenglonFactura2", vEntity.CampoExtraEnRenglonFactura2)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>>

        IList<FacturaRapidaDetalle> ILibDataDetailComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<FacturaRapidaDetalle> vResult = new List<FacturaRapidaDetalle>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<FacturaRapidaDetalle>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<FacturaRapidaDetalle>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Punto de Venta.Insertar")]
        LibResponse ILibDataDetailComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>>.Insert(IList<FacturaRapidaDetalle> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.ConsecutivoRenglon = insDb.NextLngConsecutive(DbSchema + ".renglonFactura", "ConsecutivoRenglon", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "renglonFacturaRapidaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<FacturaRapidaDetalle>, IList<FacturaRapidaDetalle>>

        public bool InsertChild(FacturaRapida valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "renglonFacturaRapidaINSDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidArticulo(valAction, CurrentRecord.Articulo);
            vResult = IsValidDescripcion(valAction, CurrentRecord.Descripcion) && vResult;
            vResult = IsValidCantidad(valAction, CurrentRecord.Cantidad) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidArticulo(eAccionSR valAction, string valArticulo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valArticulo = LibString.Trim(valArticulo);
            if (LibString.IsNullOrEmpty(valArticulo , true)) {
                BuildValidationInfo(MsgRequiredField("Código Inventario"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.ArticuloInventario", "Codigo", insDb.InsSql.ToSqlValue(valArticulo), true)) {
                    BuildValidationInfo("El valor asignado al campo Código Inventario no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidDescripcion(eAccionSR valAction, string valDescripcion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valDescripcion = LibString.Trim(valDescripcion);
            if (LibString.IsNullOrEmpty(valDescripcion , true)) {
                BuildValidationInfo(MsgRequiredField("Descripción"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.ArticuloInventario", "Descripcion", insDb.InsSql.ToSqlValue(valDescripcion), true)) {
                    BuildValidationInfo("El valor asignado al campo Descripción no existe, escoja nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCantidad(eAccionSR valAction, decimal valCantidad){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            throw new ProgrammerMissingCodeException("Campo Decimal Obligatorio, debe especificar cual es su validación");
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumeroFactura, eTipoDocumentoFactura valTipoDeDocumento, int valConsecutivoRenglon) {
            bool vResult = false;
            FacturaRapidaDetalle vRecordBusqueda = new FacturaRapidaDetalle();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroFactura = valNumeroFactura;
            vRecordBusqueda.TipoDeDocumentoAsEnum = valTipoDeDocumento;
            vRecordBusqueda.ConsecutivoRenglon = valConsecutivoRenglon;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".renglonFactura", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<FacturaRapida>  refMaster) {
            bool vResult = false;
            IList<FacturaRapidaDetalle> vDetail = null;
            foreach (FacturaRapida vItemMaster in refMaster) {
                vItemMaster.DetailFacturaRapidaDetalle = new ObservableCollection<FacturaRapidaDetalle>();
                //por qué no llegan los parámetros del detalle?
                vDetail = new LibDatabase().LoadFromSp<FacturaRapidaDetalle>("dbo.Gp_renglonFacturaRapidaSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (FacturaRapidaDetalle vItemDetail in vDetail) {
                    vItemMaster.DetailFacturaRapidaDetalle.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsFacturaRapidaDetalleDat

} //End of namespace Galac.Adm.Dal.Venta

