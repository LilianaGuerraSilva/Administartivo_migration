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
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Dal.GestionCompras {
    public class clsCompraDetalleGastoDat: LibData, ILibDataDetailComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>> {
        #region Variables
        CompraDetalleGasto _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CompraDetalleGasto CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCompraDetalleGastoDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CompraDetalleGasto valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valRecord.ConsecutivoCompra);
            vParams.AddInInteger("ConsecutivoCxP", valRecord.ConsecutivoCxP);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vParams.AddInEnum("TipoDeCosto", valRecord.TipoDeCostoAsDB);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(Compra valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valRecord.Consecutivo);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(CompraDetalleGasto valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valRecord.ConsecutivoCompra);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(CompraDetalleGasto valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valRecord.ConsecutivoCompra);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(Compra valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valMaster.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(Compra valEntidad) {
            List<Compra> vListEntidades = new List<Compra>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(Compra valMaster) {
            XElement vXElement = new XElement("GpDataCompraDetalleGasto",
                from vEntity in valMaster.DetailCompraDetalleGasto
                select new XElement("GpDetailCompraDetalleGasto",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoCompra", valMaster.Consecutivo),
                    new XElement("ConsecutivoCxP", vEntity.ConsecutivoCxP),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
                    new XElement("TipoDeCosto", vEntity.TipoDeCostoAsDB),
                    new XElement("Monto", vEntity.Monto)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>>

        IList<CompraDetalleGasto> ILibDataDetailComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CompraDetalleGasto> vResult = new List<CompraDetalleGasto>();
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CompraDetalleGasto>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CompraDetalleGasto>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Compra.Insertar")]
        LibResponse ILibDataDetailComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>>.Insert(IList<CompraDetalleGasto> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDataScope insDb = new LibDataScope();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CompraDetalleGastoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<CompraDetalleGasto>, IList<CompraDetalleGasto>>

        public bool InsertChild(Compra valRecord, LibDataScope insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQueryWithScope (insTrn.ToSpName(DbSchema, "CompraDetalleGastoInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCxP(valAction, CurrentRecord.ConsecutivoCxP);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCxP(eAccionSR valAction, int valConsecutivoCxP){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCxP == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Cx P"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoCompra, int valConsecutivoRenglon) {
            bool vResult = false;
            CompraDetalleGasto vRecordBusqueda = new CompraDetalleGasto();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoCompra = valConsecutivoCompra;
            vRecordBusqueda.ConsecutivoRenglon = valConsecutivoRenglon;
            LibDataScope insDb = new LibDataScope();
            vResult = insDb.ExistsRecord(DbSchema + ".CompraDetalleGasto", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, CompraDetalleGasto valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDataScope insDb = new LibDataScope();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".CompraDetalleGasto", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<Compra>  refMaster) {
            bool vResult = false;
            IList<CompraDetalleGasto> vDetail = null;
            foreach (Compra vItemMaster in refMaster) {
                vItemMaster.DetailCompraDetalleGasto = new ObservableCollection<CompraDetalleGasto>();
                vDetail = new LibDataScope().LoadFromSp<CompraDetalleGasto>("Adm.Gp_CompraDetalleGastoSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (CompraDetalleGasto vItemDetail in vDetail) {
                    vItemMaster.DetailCompraDetalleGasto.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCompraDetalleGastoDat

} //End of namespace Galac.Adm.Dal.GestionCompras

