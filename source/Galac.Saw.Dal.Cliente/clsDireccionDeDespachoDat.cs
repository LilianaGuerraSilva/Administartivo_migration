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
using Entity = Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Cliente;

namespace Galac.Saw.Dal.Cliente {
    public class clsDireccionDeDespachoDat: LibData, ILibDataDetailComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>> {
        #region Variables
        DireccionDeDespacho _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private DireccionDeDespacho CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsDireccionDeDespachoDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(DireccionDeDespacho valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInInteger("ConsecutivoDireccion", valRecord.ConsecutivoDireccion);
            vParams.AddInString("PersonaContacto", valRecord.PersonaContacto, 20);
            vParams.AddInString("Direccion", valRecord.Direccion, 100);
            vParams.AddInString("Ciudad", valRecord.Ciudad, 100);
            vParams.AddInString("ZonaPostal", valRecord.ZonaPostal, 7);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(Entity.Cliente valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoCliente", valRecord.Codigo, 10);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(DireccionDeDespacho valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInInteger("ConsecutivoDireccion", valRecord.ConsecutivoDireccion);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(DireccionDeDespacho valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(Entity.Cliente valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInString("CodigoCliente", valMaster.Codigo, 10);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(Entity.Cliente valEntidad) {
            List<Entity.Cliente> vListEntidades = new List<Entity.Cliente>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Codigo", vEntity.Codigo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(Entity.Cliente valMaster) {
            XElement vXElement = new XElement("GpDataDireccionDeDespacho",
                from vEntity in valMaster.DetailDireccionDeDespacho
                select new XElement("GpDetailDireccionDeDespacho",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("CodigoCliente", valMaster.Codigo),
                    new XElement("ConsecutivoDireccion", vEntity.ConsecutivoDireccion),
                    new XElement("PersonaContacto", vEntity.PersonaContacto),
                    new XElement("Direccion", vEntity.Direccion),
                    new XElement("Ciudad", vEntity.Ciudad),
                    new XElement("ZonaPostal", vEntity.ZonaPostal)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>>

        IList<DireccionDeDespacho> ILibDataDetailComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<DireccionDeDespacho> vResult = new List<DireccionDeDespacho>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<DireccionDeDespacho>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<DireccionDeDespacho>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cliente.Insertar")]
        LibResponse ILibDataDetailComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>>.Insert(IList<DireccionDeDespacho> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.ConsecutivoDireccion = insDb.NextLngConsecutive(DbSchema + ".DireccionDeDespacho", "ConsecutivoDireccion", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "DireccionDeDespachoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<DireccionDeDespacho>, IList<DireccionDeDespacho>>

        public bool InsertChild(Entity.Cliente valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "DireccionDeDespachoInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoDireccion(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoDireccion);
            vResult = IsValidDireccion(valAction, CurrentRecord.Direccion) && vResult;
            vResult = IsValidCiudad(valAction, CurrentRecord.Ciudad) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoDireccion(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoDireccion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if (valConsecutivoDireccion == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Direccion"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidDireccion(eAccionSR valAction, string valDireccion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valDireccion = LibString.Trim(valDireccion);
            if (LibString.IsNullOrEmpty(valDireccion, true)) {
                BuildValidationInfo(MsgRequiredField("Direcci?n"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCiudad(eAccionSR valAction, string valCiudad){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCiudad = LibString.Trim(valCiudad);
            if (LibString.IsNullOrEmpty(valCiudad , true)) {
                BuildValidationInfo(MsgRequiredField("Ciudad"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Ciudad", "NombreCiudad", insDb.InsSql.ToSqlValue(valCiudad), true)) {
                    BuildValidationInfo("El valor asignado al campo Ciudad no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoCliente, int valConsecutivoDireccion) {
            bool vResult = false;
            DireccionDeDespacho vRecordBusqueda = new DireccionDeDespacho();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoCliente = valCodigoCliente;
            vRecordBusqueda.ConsecutivoDireccion = valConsecutivoDireccion;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".DireccionDeDespacho", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<Entity.Cliente>  refMaster) {
            bool vResult = false;
            IList<DireccionDeDespacho> vDetail = null;
            foreach (Entity.Cliente vItemMaster in refMaster) {
                vItemMaster.DetailDireccionDeDespacho = new ObservableCollection<DireccionDeDespacho>();
                vDetail = new LibDatabase().LoadFromSp<DireccionDeDespacho>("dbo.Gp_DireccionDeDespachoSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (DireccionDeDespacho vItemDetail in vDetail) {
                    vItemMaster.DetailDireccionDeDespacho.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsDireccionDeDespachoDat

} //End of namespace Galac.Saw.Dal.Cliente

