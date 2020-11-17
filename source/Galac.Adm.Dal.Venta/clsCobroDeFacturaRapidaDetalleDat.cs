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
    public class clsCobroDeFacturaRapidaDetalleDat: LibData, ILibDataDetailComponent<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>> {
        #region Variables
        CobroDeFacturaRapidaDetalle _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CobroDeFacturaRapidaDetalle CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaDetalleDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CobroDeFacturaRapidaDetalle valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.CodigoFormaDelCobro, 5);
            vParams.AddInDecimal("MontoEfectivo", valRecord.MontoEfectivo, 2);
            vParams.AddInDecimal("MontoCheque", valRecord.MontoCheque, 2);
            vParams.AddInDecimal("MontoTarjeta", valRecord.MontoTarjeta, 2);
            vParams.AddInDecimal("MontoDeposito", valRecord.MontoDeposito, 2);
            vParams.AddInDecimal("MontoAnticipo", valRecord.MontoAnticipo, 2);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(CobroDeFacturaRapida valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valRecord.NumeroFactura, 5);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CobroDeFacturaRapidaDetalle valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(CobroDeFacturaRapidaDetalle valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(CobroDeFacturaRapida valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInString("CodigoFormaDelCobro", valMaster.NumeroFactura, 5);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(CobroDeFacturaRapida valEntidad) {
            List<CobroDeFacturaRapida> vListEntidades = new List<CobroDeFacturaRapida>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("NumeroFactura", vEntity.NumeroFactura),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(CobroDeFacturaRapida valMaster) {
            XElement vXElement = new XElement("GpDataCobroDeFacturaRapidaDetalle",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaDetalle
                select new XElement("GpDetailCobroDeFacturaRapidaDetalle",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("CodigoFormaDelCobro", valMaster.NumeroFactura),
                    new XElement("MontoEfectivo", vEntity.MontoEfectivo),
                    new XElement("MontoCheque", vEntity.MontoCheque),
                    new XElement("MontoTarjeta", vEntity.MontoTarjeta),
                    new XElement("MontoDeposito", vEntity.MontoDeposito),
                    new XElement("MontoAnticipo", vEntity.MontoAnticipo)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>>

        IList<CobroDeFacturaRapidaDetalle> ILibDataDetailComponent<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CobroDeFacturaRapidaDetalle> vResult = new List<CobroDeFacturaRapidaDetalle>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CobroDeFacturaRapidaDetalle>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CobroDeFacturaRapidaDetalle>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Forma de Cobro.Insertar")]
        LibResponse ILibDataDetailComponent<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>>.Insert(IList<CobroDeFacturaRapidaDetalle> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CobroDeFacturaRapidaDetalleINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<CobroDeFacturaRapidaDetalle>, IList<CobroDeFacturaRapidaDetalle>>

        public bool InsertChild(CobroDeFacturaRapida valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "CobroDeFacturaRapidaDetalleInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoFormaDelCobro) {
            bool vResult = false;
            CobroDeFacturaRapidaDetalle vRecordBusqueda = new CobroDeFacturaRapidaDetalle();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoFormaDelCobro = valCodigoFormaDelCobro;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".CobroDeFacturaRapidaDetalle", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<CobroDeFacturaRapida>  refMaster) {
            bool vResult = false;
            IList<CobroDeFacturaRapidaDetalle> vDetail = null;
            foreach (CobroDeFacturaRapida vItemMaster in refMaster) {
                vItemMaster.DetailCobroDeFacturaRapidaDetalle = new ObservableCollection<CobroDeFacturaRapidaDetalle>();
                vDetail = new LibDatabase().LoadFromSp<CobroDeFacturaRapidaDetalle>("Adm.Gp_CobroDeFacturaRapidaDetalleSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (CobroDeFacturaRapidaDetalle vItemDetail in vDetail) {
                    vItemMaster.DetailCobroDeFacturaRapidaDetalle.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCobroDeFacturaRapidaDetalleDat

} //End of namespace Galac.Adm.Dal.Venta

