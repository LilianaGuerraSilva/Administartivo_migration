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
    public class clsOrdenDeProduccionDetalleArticuloDat: LibData, ILibDataDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>> {
        #region Variables
        LibDataScope insTrn;
        OrdenDeProduccionDetalleArticulo _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private OrdenDeProduccionDetalleArticulo CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsOrdenDeProduccionDetalleArticuloDat() {
            DbSchema = "Adm";
            insTrn = new LibDataScope();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(OrdenDeProduccionDetalleArticulo valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeProduccion", valRecord.ConsecutivoOrdenDeProduccion);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInDecimal("CantidadOriginalLista", valRecord.CantidadOriginalLista, 8);
            vParams.AddInDecimal("CantidadSolicitada", valRecord.CantidadSolicitada, 8);
            vParams.AddInDecimal("CantidadProducida", valRecord.CantidadProducida, 8);
            vParams.AddInDecimal("CostoUnitario", valRecord.CostoUnitario, 2);
            vParams.AddInDecimal("MontoSubTotal", valRecord.MontoSubTotal, 2);
            vParams.AddInBoolean("AjustadoPostCierre", valRecord.AjustadoPostCierreAsBool);
            vParams.AddInDecimal("CantidadAjustada", valRecord.CantidadAjustada, 2);
            vParams.AddInDecimal("PorcentajeCostoEstimado", valRecord.PorcentajeCostoEstimado, 8);
            vParams.AddInDecimal("PorcentajeCostoCierre", valRecord.PorcentajeCostoCierre, 8);
            vParams.AddInDecimal("Costo", valRecord.Costo, 2);
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

        private StringBuilder ParametrosClave(OrdenDeProduccionDetalleArticulo valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(OrdenDeProduccionDetalleArticulo valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoOrdenDeProduccion", valRecord.ConsecutivoOrdenDeProduccion);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
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
                    new XElement("ConsecutivoOrdenDeProduccion", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(OrdenDeProduccion valMaster) {
            XElement vXElement = new XElement("GpDataOrdenDeProduccionDetalleArticulo",
                from vEntity in valMaster.DetailOrdenDeProduccionDetalleArticulo
                select new XElement("GpDetailOrdenDeProduccionDetalleArticulo",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoOrdenDeProduccion", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("ConsecutivoAlmacen", vEntity.ConsecutivoAlmacen),
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo),
                    new XElement("CantidadOriginalLista", vEntity.CantidadOriginalLista),
                    new XElement("CantidadSolicitada", vEntity.CantidadSolicitada),
                    new XElement("CantidadProducida", vEntity.CantidadProducida),
                    new XElement("CostoUnitario", vEntity.CostoUnitario),
                    new XElement("MontoSubTotal", vEntity.MontoSubTotal),
                    new XElement("AjustadoPostCierre", LibConvert.BoolToSN(vEntity.AjustadoPostCierreAsBool)),
                    new XElement("CantidadAjustada", vEntity.CantidadAjustada),
                    new XElement("PorcentajeCostoEstimado", vEntity.PorcentajeCostoEstimado),
                    new XElement("PorcentajeCostoCierre", vEntity.PorcentajeCostoCierre),
                    new XElement("Costo", vEntity.Costo)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>>

        IList<OrdenDeProduccionDetalleArticulo> ILibDataDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<OrdenDeProduccionDetalleArticulo> vResult = new List<OrdenDeProduccionDetalleArticulo>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<OrdenDeProduccionDetalleArticulo>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<OrdenDeProduccionDetalleArticulo>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Orden de Producción.Insertar")]
        LibResponse ILibDataDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>>.Insert(IList<OrdenDeProduccionDetalleArticulo> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "OrdenDeProduccionDetalleArticuloINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<OrdenDeProduccionDetalleArticulo>, IList<OrdenDeProduccionDetalleArticulo>>

        public bool InsertChild(OrdenDeProduccion valRecord, LibDataScope insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "OrdenDeProduccionDetalleArticuloInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));            
            return vResult;
        }		
		
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();            
            outErrorMessage = Information.ToString();
            return vResult;
        }     
        
        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoOrdenDeProduccion, int valConsecutivo) {
            bool vResult = false;
            OrdenDeProduccionDetalleArticulo vRecordBusqueda = new OrdenDeProduccionDetalleArticulo();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoOrdenDeProduccion = valConsecutivoOrdenDeProduccion;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeProduccionDetalleArticulo", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
       
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<OrdenDeProduccion>  refMaster) {
            bool vResult = false;
            List<OrdenDeProduccionDetalleArticulo> vDetail = null;
            foreach (OrdenDeProduccion vItemMaster in refMaster) {
                vItemMaster.DetailOrdenDeProduccionDetalleArticulo = new ObservableCollection<OrdenDeProduccionDetalleArticulo>();
                vDetail = new LibDatabase().LoadFromSp<OrdenDeProduccionDetalleArticulo>("Adm.Gp_OrdenDeProduccionDetalleArticuloSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (OrdenDeProduccionDetalleArticulo vItemDetail in vDetail) {
                    vItemMaster.DetailOrdenDeProduccionDetalleArticulo.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionDetalleArticuloDat

} //End of namespace Galac.Adm.Dal.GestionProduccion

