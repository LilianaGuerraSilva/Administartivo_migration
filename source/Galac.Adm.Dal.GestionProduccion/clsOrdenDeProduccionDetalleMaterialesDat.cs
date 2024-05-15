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
using Galac.Adm.Ccl.GestionProduccion;

namespace Galac.Adm.Dal.GestionProduccion {
    public class clsOrdenDeProduccionDetalleMaterialesDat : LibData, ILibDataDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>> {
        #region Variables
        LibDataScope insTrn;
        OrdenDeProduccionDetalleMateriales _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private OrdenDeProduccionDetalleMateriales CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeProduccionDetalleMaterialesDat() {
            DbSchema = "Adm";
            insTrn = new LibDataScope();
        }
		
        public clsOrdenDeProduccionDetalleMaterialesDat(LibDataScope initTrn) {
            DbSchema = "Adm";
            insTrn = initTrn;
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(OrdenDeProduccionDetalleMateriales valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeProduccion", valRecord.ConsecutivoOrdenDeProduccion);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 8);
            vParams.AddInDecimal("CantidadReservadaInventario", valRecord.CantidadReservadaInventario, 4);
            vParams.AddInDecimal("CantidadConsumida", valRecord.CantidadConsumida, 4);
            vParams.AddInDecimal("CostoUnitarioArticuloInventario", valRecord.CostoUnitarioArticuloInventario, 2);
            vParams.AddInDecimal("MontoSubtotal", valRecord.MontoSubtotal, 2);
            vParams.AddInBoolean("AjustadoPostCierre", valRecord.AjustadoPostCierreAsBool);
            vParams.AddInDecimal("CantidadAjustada", valRecord.CantidadAjustada, 2);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(OrdenDeProduccion valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeProduccion", valRecord.Consecutivo);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(OrdenDeProduccionDetalleMateriales valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeProduccion", valRecord.ConsecutivoOrdenDeProduccion);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(OrdenDeProduccionDetalleMateriales valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeProduccion", valRecord.ConsecutivoOrdenDeProduccion);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(OrdenDeProduccion valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeProduccion", valMaster.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(OrdenDeProduccion valEntidad) {
            List<OrdenDeProduccion> vListEntidades = new List<OrdenDeProduccion>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(OrdenDeProduccion valMaster) {
            XElement vXElement = new XElement("GpDataOrdenDeProduccionDetalleMateriales",
                from vEntity in valMaster.DetailOrdenDeProduccionDetalleMateriales
                select new XElement("GpDetailOrdenDeProduccionDetalleMateriales",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoOrdenDeProduccion", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("ConsecutivoAlmacen", vEntity.ConsecutivoAlmacen),
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo),
                    new XElement("Cantidad", vEntity.Cantidad),
                    new XElement("CantidadReservadaInventario", vEntity.CantidadReservadaInventario),
                    new XElement("CantidadConsumida", vEntity.CantidadConsumida),
                    new XElement("CostoUnitarioArticuloInventario", vEntity.CostoUnitarioArticuloInventario),
                    new XElement("MontoSubtotal", vEntity.MontoSubtotal),
                    new XElement("AjustadoPostCierre", LibConvert.BoolToSN(vEntity.AjustadoPostCierreAsBool)),
                    new XElement("CantidadAjustada", vEntity.CantidadAjustada)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>>

        IList<OrdenDeProduccionDetalleMateriales> ILibDataDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<OrdenDeProduccionDetalleMateriales> vResult = new List<OrdenDeProduccionDetalleMateriales>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<OrdenDeProduccionDetalleMateriales>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<OrdenDeProduccionDetalleMateriales>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Insertar")]
        LibResponse ILibDataDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>>.Insert(IList<OrdenDeProduccionDetalleMateriales> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "OrdenDeProduccionDetalleMaterialesINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<OrdenDeProduccionDetalleMateriales>, IList<OrdenDeProduccionDetalleMateriales>>

        public bool InsertChild(OrdenDeProduccion valRecord, LibDataScope insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "OrdenDeProduccionDetalleMaterialesInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoAlmacen(valAction, CurrentRecord.ConsecutivoAlmacen);
            vResult = IsValidCodigoArticulo(valAction, CurrentRecord.CodigoArticulo) && vResult;            
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoAlmacen(eAccionSR valAction, int valConsecutivoAlmacen) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoAlmacen == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Almacen"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Saw.Almacen", "Consecutivo", insDb.InsSql.ToSqlValue(valConsecutivoAlmacen), true)) {
                    BuildValidationInfo("El valor asignado al campo Consecutivo Almacen no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoArticulo(eAccionSR valAction, string valCodigoArticulo) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoArticulo = LibString.Trim(valCodigoArticulo);
            if (LibString.IsNullOrEmpty(valCodigoArticulo, true)) {
                BuildValidationInfo(MsgRequiredField("CodigoArticulo"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.ArticuloInventario", "Codigo", insDb.InsSql.ToSqlValue(valCodigoArticulo), true)) {
                    BuildValidationInfo("El valor asignado al campo CodigoArticulo no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }


        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoOrdenDeProduccion, int valConsecutivo) {
            bool vResult = false;
            OrdenDeProduccionDetalleMateriales vRecordBusqueda = new OrdenDeProduccionDetalleMateriales();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoOrdenDeProduccion = valConsecutivoOrdenDeProduccion;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeProduccionDetalleMateriales", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<OrdenDeProduccion>  refMaster) {
            bool vResult = false;
            List<OrdenDeProduccionDetalleMateriales> vDetail = null;
            foreach (OrdenDeProduccion vItemMaster in refMaster) {
                vItemMaster.DetailOrdenDeProduccionDetalleMateriales = new ObservableCollection<OrdenDeProduccionDetalleMateriales>();
                vDetail = new LibDatabase().LoadFromSp<OrdenDeProduccionDetalleMateriales>("Adm.Gp_OrdenDeProduccionDetalleMaterialesSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (OrdenDeProduccionDetalleMateriales vItemDetail in vDetail) {
                    vItemMaster.DetailOrdenDeProduccionDetalleMateriales.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionDetalleMaterialesDat

} //End of namespace Galac.Adm.Dal.GestionProduccion

