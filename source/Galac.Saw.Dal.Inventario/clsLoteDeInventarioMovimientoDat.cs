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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    public class clsLoteDeInventarioMovimientoDat: LibData, ILibDataDetailComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>> {
        #region Variables
        LoteDeInventarioMovimiento _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private LoteDeInventarioMovimiento CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsLoteDeInventarioMovimientoDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(LoteDeInventarioMovimiento valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoLote", valRecord.ConsecutivoLote);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInEnum("Modulo", valRecord.ModuloAsDB);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 2);
            vParams.AddInInteger("ConsecutivoDocumentoOrigen", valRecord.ConsecutivoDocumentoOrigen);
            vParams.AddInString("NumeroDocumentoOrigen", valRecord.NumeroDocumentoOrigen, 30);
            vParams.AddInEnum("StatusDocumentoOrigen", valRecord.StatusDocumentoOrigenAsDB);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(LoteDeInventario valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoLote", valRecord.Consecutivo);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(LoteDeInventarioMovimiento valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoLote", valRecord.ConsecutivoLote);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(LoteDeInventarioMovimiento valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoLote", valRecord.ConsecutivoLote);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(LoteDeInventario valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoLote", valMaster.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(LoteDeInventario valEntidad) {
            List<LoteDeInventario> vListEntidades = new List<LoteDeInventario>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(LoteDeInventario valMaster) {
            XElement vXElement = new XElement("GpDataLoteDeInventarioMovimiento",
                from vEntity in valMaster.DetailLoteDeInventarioMovimiento
                select new XElement("GpDetailLoteDeInventarioMovimiento",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoLote", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("Fecha", vEntity.Fecha),
                    new XElement("Modulo", vEntity.ModuloAsDB),
                    new XElement("Cantidad", vEntity.Cantidad),
                    new XElement("ConsecutivoDocumentoOrigen", vEntity.ConsecutivoDocumentoOrigen),
                    new XElement("NumeroDocumentoOrigen", vEntity.NumeroDocumentoOrigen),
                    new XElement("StatusDocumentoOrigen", vEntity.StatusDocumentoOrigenAsDB)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>>

        IList<LoteDeInventarioMovimiento> ILibDataDetailComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<LoteDeInventarioMovimiento> vResult = new List<LoteDeInventarioMovimiento>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<LoteDeInventarioMovimiento>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<LoteDeInventarioMovimiento>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Lote de Inventario.Insertar")]
        LibResponse ILibDataDetailComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>>.Insert(IList<LoteDeInventarioMovimiento> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "LoteDeInventarioMovimientoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<LoteDeInventarioMovimiento>, IList<LoteDeInventarioMovimiento>>

        public bool InsertChild(LoteDeInventario valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "LoteDeInventarioMovimientoInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha);
            outErrorMessage = Information.ToString();
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

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoLote, int valConsecutivo) {
            bool vResult = false;
            LoteDeInventarioMovimiento vRecordBusqueda = new LoteDeInventarioMovimiento();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoLote = valConsecutivoLote;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".LoteDeInventarioMovimiento", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<LoteDeInventario>  refMaster) {
            bool vResult = false;
            IList<LoteDeInventarioMovimiento> vDetail = null;
            foreach (LoteDeInventario vItemMaster in refMaster) {
                vItemMaster.DetailLoteDeInventarioMovimiento = new ObservableCollection<LoteDeInventarioMovimiento>();
                vDetail = new LibDatabase().LoadFromSp<LoteDeInventarioMovimiento>("Saw.Gp_LoteDeInventarioMovimientoSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (LoteDeInventarioMovimiento vItemDetail in vDetail) {
                    vItemMaster.DetailLoteDeInventarioMovimiento.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsLoteDeInventarioMovimientoDat

} //End of namespace Galac.Saw.Dal.Inventario

