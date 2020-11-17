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
    public class clsCobroDeFacturaRapidaTarjetaDetalleDat: LibData, ILibDataDetailComponent<IList<CobroDeFacturaRapidaTarjetaDetalle>, IList<CobroDeFacturaRapidaTarjetaDetalle>> {
        #region Variables
        CobroDeFacturaRapidaTarjetaDetalle _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CobroDeFacturaRapidaTarjetaDetalle CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaTarjetaDetalleDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CobroDeFacturaRapidaTarjetaDetalle valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.CodigoFormaDelCobro, 5);
            vParams.AddInString("NumeroDelDocumento", valRecord.NumeroDelDocumento, 30);
            vParams.AddInInteger("CodigoBanco", valRecord.CodigoBanco);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vParams.AddInInteger("CodigoPuntoDeVenta", valRecord.CodigoPuntoDeVenta);
            vParams.AddInString("NumeroDocumentoAprobacion", valRecord.NumeroDocumentoAprobacion, 30);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(CobroDeFacturaRapidaTarjeta valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.NumeroFactura, 5);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CobroDeFacturaRapidaTarjetaDetalle valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(CobroDeFacturaRapidaTarjetaDetalle valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(CobroDeFacturaRapidaTarjeta valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valMaster.NumeroFactura, 5);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(CobroDeFacturaRapidaTarjeta valEntidad) {
            List<CobroDeFacturaRapidaTarjeta> vListEntidades = new List<CobroDeFacturaRapidaTarjeta>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("NumeroFactura", vEntity.NumeroFactura),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(CobroDeFacturaRapidaTarjeta valMaster) {
            XElement vXElement = new XElement("GpDataCobroDeFacturaRapidaTarjetaDetalle",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaTarjetaDetalle
                select new XElement("GpDetailCobroDeFacturaRapidaTarjetaDetalle",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("CodigoFormaDelCobro", valMaster.NumeroFactura),
                    new XElement("NumeroDelDocumento", vEntity.NumeroDelDocumento),
                    new XElement("CodigoBanco", vEntity.CodigoBanco),
                    new XElement("Monto", vEntity.Monto),
                    new XElement("CodigoPuntoDeVenta", vEntity.CodigoPuntoDeVenta),
                    new XElement("NumeroDocumentoAprobacion", vEntity.NumeroDocumentoAprobacion)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<CobroDeFacturaRapidaTarjetaDetalle>, IList<CobroDeFacturaRapidaTarjetaDetalle>>

        IList<CobroDeFacturaRapidaTarjetaDetalle> ILibDataDetailComponent<IList<CobroDeFacturaRapidaTarjetaDetalle>, IList<CobroDeFacturaRapidaTarjetaDetalle>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CobroDeFacturaRapidaTarjetaDetalle> vResult = new List<CobroDeFacturaRapidaTarjetaDetalle>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CobroDeFacturaRapidaTarjetaDetalle>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CobroDeFacturaRapidaTarjetaDetalle>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobro Tarjeta de Factura.Insertar")]
        LibResponse ILibDataDetailComponent<IList<CobroDeFacturaRapidaTarjetaDetalle>, IList<CobroDeFacturaRapidaTarjetaDetalle>>.Insert(IList<CobroDeFacturaRapidaTarjetaDetalle> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CobroDeFacturaRapidaTarjetaDetalleINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<CobroDeFacturaRapidaTarjetaDetalle>, IList<CobroDeFacturaRapidaTarjetaDetalle>>

        public bool InsertChild(CobroDeFacturaRapidaTarjeta valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaTarjetaDetalleInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoBanco(valAction, CurrentRecord.CodigoBanco);
            vResult = IsValidCodigoPuntoDeVenta(valAction, CurrentRecord.CodigoPuntoDeVenta) && vResult;
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

        private bool IsValidCodigoPuntoDeVenta(eAccionSR valAction, int valCodigoPuntoDeVenta){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valCodigoPuntoDeVenta == 0) {
                BuildValidationInfo(MsgRequiredField("Código del Banco del Punto de Venta"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoFormaDelCobro) {
            bool vResult = false;
            CobroDeFacturaRapidaTarjetaDetalle vRecordBusqueda = new CobroDeFacturaRapidaTarjetaDetalle();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoFormaDelCobro = valCodigoFormaDelCobro;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CobroDeFacturaRapidaTarjetaDetalle", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<CobroDeFacturaRapidaTarjeta>  refMaster) {
            bool vResult = false;
            IList<CobroDeFacturaRapidaTarjetaDetalle> vDetail = null;
            foreach (CobroDeFacturaRapidaTarjeta vItemMaster in refMaster) {
                vItemMaster.DetailCobroDeFacturaRapidaTarjetaDetalle = new ObservableCollection<CobroDeFacturaRapidaTarjetaDetalle>();
                vDetail = new LibDatabase().LoadFromSp<CobroDeFacturaRapidaTarjetaDetalle>("Adm.Gp_CobroDeFacturaRapidaTarjetaDetalleSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (CobroDeFacturaRapidaTarjetaDetalle vItemDetail in vDetail) {
                    vItemMaster.DetailCobroDeFacturaRapidaTarjetaDetalle.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaTarjetaDetalleDat

} //End of namespace Galac.Adm.Dal.Venta

