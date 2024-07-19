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
    public class clsRenglonNotaESDat: LibData, ILibDataDetailComponent<IList<RenglonNotaES>, IList<RenglonNotaES>> {
        #region Variables
        RenglonNotaES _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private RenglonNotaES CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRenglonNotaESDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(RenglonNotaES valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 11);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 2);
            vParams.AddInEnum("TipoArticuloInv", valRecord.TipoArticuloInvAsDB);
            vParams.AddInString("Serial", valRecord.Serial, 50);
            vParams.AddInString("Rollo", valRecord.Rollo, 20);
            vParams.AddInDecimal("CostoUnitario", valRecord.CostoUnitario, 2);
            vParams.AddInDecimal("CostoUnitarioME", valRecord.CostoUnitarioME, 2);
            vParams.AddInString("LoteDeInventario", valRecord.LoteDeInventario, 30);
            vParams.AddInDateTime("FechaDeElaboracion", valRecord.FechaDeElaboracion);
            vParams.AddInDateTime("FechaDeVencimiento", valRecord.FechaDeVencimiento);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(NotaDeEntradaSalida valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 11);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(RenglonNotaES valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 11);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(RenglonNotaES valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 11);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(NotaDeEntradaSalida valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valMaster.NumeroDocumento, 11);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(NotaDeEntradaSalida valEntidad) {
            List<NotaDeEntradaSalida> vListEntidades = new List<NotaDeEntradaSalida>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("NumeroDocumento", vEntity.NumeroDocumento),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(NotaDeEntradaSalida valMaster) {
            XElement vXElement = new XElement("GpDataRenglonNotaES",
                from vEntity in valMaster.DetailRenglonNotaES
                select new XElement("GpDetailRenglonNotaES",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("NumeroDocumento", valMaster.NumeroDocumento),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo),
                    new XElement("Cantidad", vEntity.Cantidad),
                    new XElement("TipoArticuloInv", vEntity.TipoArticuloInvAsDB),
                    new XElement("Serial", vEntity.Serial),
                    new XElement("Rollo", vEntity.Rollo),
                    new XElement("CostoUnitario", vEntity.CostoUnitario),
                    new XElement("CostoUnitarioME", vEntity.CostoUnitarioME),
                    new XElement("LoteDeInventario", vEntity.LoteDeInventario),
                    new XElement("FechaDeElaboracion", vEntity.FechaDeElaboracion),
                    new XElement("FechaDeVencimiento", vEntity.FechaDeVencimiento)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<RenglonNotaES>, IList<RenglonNotaES>>

        IList<RenglonNotaES> ILibDataDetailComponent<IList<RenglonNotaES>, IList<RenglonNotaES>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<RenglonNotaES> vResult = new List<RenglonNotaES>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<RenglonNotaES>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<RenglonNotaES>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Nota de Entrada/Salida.Insertar")]
        LibResponse ILibDataDetailComponent<IList<RenglonNotaES>, IList<RenglonNotaES>>.Insert(IList<RenglonNotaES> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "RenglonNotaESINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<RenglonNotaES>, IList<RenglonNotaES>>

        public bool InsertChild(NotaDeEntradaSalida valRecord, LibDataScope insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "RenglonNotaESInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoArticulo(valAction, CurrentRecord.CodigoArticulo);
            vResult = IsValidCantidad(valAction, CurrentRecord.Cantidad) && vResult;
            vResult = IsValidTipoArticuloInv(valAction, CurrentRecord.TipoArticuloInvAsEnum) && vResult;
            vResult = IsValidFechaDeElaboracion(valAction, CurrentRecord.FechaDeElaboracion) && vResult;
            vResult = IsValidFechaDeVencimiento(valAction, CurrentRecord.FechaDeVencimiento) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidCodigoArticulo(eAccionSR valAction, string valCodigoArticulo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoArticulo = LibString.Trim(valCodigoArticulo);
            if (LibString.IsNullOrEmpty(valCodigoArticulo, true)) {
                BuildValidationInfo(MsgRequiredField("Código Inventario"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCantidad(eAccionSR valAction, decimal valCantidad){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            throw new ProgrammerMissingCodeException("Campo Decimal Obligatorio, debe especificar cual es su validacion");
            return vResult;
        }

        private bool IsValidTipoArticuloInv(eAccionSR valAction, eTipoArticuloInv valTipoArticuloInv){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidFechaDeElaboracion(eAccionSR valAction, DateTime valFechaDeElaboracion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeElaboracion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaDeVencimiento(eAccionSR valAction, DateTime valFechaDeVencimiento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDeVencimiento, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumeroDocumento, int valConsecutivoRenglon) {
            bool vResult = false;
            RenglonNotaES vRecordBusqueda = new RenglonNotaES();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.NumeroDocumento = valNumeroDocumento;
            vRecordBusqueda.ConsecutivoRenglon = valConsecutivoRenglon;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".RenglonNotaES", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<NotaDeEntradaSalida>  refMaster) {
            bool vResult = false;
            IList<RenglonNotaES> vDetail = null;
            foreach (NotaDeEntradaSalida vItemMaster in refMaster) {
                vItemMaster.DetailRenglonNotaES = new ObservableCollection<RenglonNotaES>();
                vDetail = new LibDatabase().LoadFromSp<RenglonNotaES>("dbo.Gp_RenglonNotaESSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (RenglonNotaES vItemDetail in vDetail) {
                    vItemMaster.DetailRenglonNotaES.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsRenglonNotaESDat

} //End of namespace Galac.Saw.Dal.Inventario

