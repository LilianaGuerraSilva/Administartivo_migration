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
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valRecord.ConsecutivoListaDeMateriales);
            vParams.AddInInteger("ConsecutivoAlmacen", valRecord.ConsecutivoAlmacen);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInDecimal("CantidadSolicitada", valRecord.CantidadSolicitada, 8);
            vParams.AddInDecimal("CantidadProducida", valRecord.CantidadProducida, 8);
            vParams.AddInDecimal("CostoUnitario", valRecord.CostoUnitario, 2);
            vParams.AddInDecimal("MontoSubTotal", valRecord.MontoSubTotal, 2);
            vParams.AddInBoolean("AjustadoPostCierre", valRecord.AjustadoPostCierreAsBool);
            vParams.AddInDecimal("CantidadAjustada", valRecord.CantidadAjustada, 2);
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
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
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valRecord.ConsecutivoListaDeMateriales);
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
                    new XElement("ConsecutivoListaDeMateriales", vEntity.ConsecutivoListaDeMateriales),
                    new XElement("ConsecutivoAlmacen", vEntity.ConsecutivoAlmacen),
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo),
                    new XElement("CantidadSolicitada", vEntity.CantidadSolicitada),
                    new XElement("CantidadProducida", vEntity.CantidadProducida),
                    new XElement("CostoUnitario", vEntity.CostoUnitario),
                    new XElement("MontoSubTotal", vEntity.MontoSubTotal),
                    new XElement("AjustadoPostCierre", LibConvert.BoolToSN(vEntity.AjustadoPostCierreAsBool)),
                    new XElement("CantidadAjustada", vEntity.CantidadAjustada)));
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
            foreach (var item in valRecord.DetailOrdenDeProduccionDetalleArticulo ) {
                vResult = vResult && InsertDetail(item, insTrn);
            }
            
            return vResult;
        }

        LibResponse UpdateMaster(OrdenDeProduccionDetalleArticulo refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "OrdenDeProduccionDetalleArticuloUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(OrdenDeProduccionDetalleArticulo refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar,out vErrorMessage)) {
                if (UpdateDetail(refRecord, insTrn)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(OrdenDeProduccionDetalleArticulo valRecord, LibDataScope insTrn) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOrdenDeProduccionDetalleMaterialesAndUpdateDb(valRecord, insTrn);
            return vResult;
        }

        private bool SetPkInDetailOrdenDeProduccionDetalleMaterialesAndUpdateDb(OrdenDeProduccionDetalleArticulo valRecord, LibDataScope insTrn) {
            bool vResult = false;
            int vConsecutivo = 1;
            clsOrdenDeProduccionDetalleMaterialesDat insOrdenDeProduccionDetalleMateriales = new clsOrdenDeProduccionDetalleMaterialesDat(insTrn);
            foreach (OrdenDeProduccionDetalleMateriales vDetail in valRecord.DetailOrdenDeProduccionDetalleMateriales) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoOrdenDeProduccion = valRecord.ConsecutivoOrdenDeProduccion;
                vDetail.ConsecutivoOrdenDeProduccionDetalleArticulo = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            vResult = insOrdenDeProduccionDetalleMateriales.InsertChild(valRecord, insTrn);
            return vResult;
        }

        private bool UpdateDetail(OrdenDeProduccionDetalleArticulo valRecord, LibDataScope insTrn) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailOrdenDeProduccionDetalleMaterialesAndUpdateDb(valRecord, insTrn);
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoListaDeMateriales(valAction, CurrentRecord.ConsecutivoListaDeMateriales);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoListaDeMateriales(eAccionSR valAction, int valConsecutivoListaDeMateriales){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoListaDeMateriales == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Lista De Materiales"));
                vResult = false;
            }
            return vResult;
        }

        
        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoOrdenDeProduccion, int valConsecutivo, int valConsecutivoListaDeMateriales) {
            bool vResult = false;
            OrdenDeProduccionDetalleArticulo vRecordBusqueda = new OrdenDeProduccionDetalleArticulo();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoOrdenDeProduccion = valConsecutivoOrdenDeProduccion;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            vRecordBusqueda.ConsecutivoListaDeMateriales =  valConsecutivoListaDeMateriales;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".OrdenDeProduccionDetalleArticulo", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, OrdenDeProduccionDetalleArticulo valRecordMaster, bool valUseDetail) {
            bool vResult = false;
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        vResult = true;
                    } else {
                        throw new GalacValidationException("Orden De Produccion Detalle Articulo (detalle)\n" + vErrMsg);
                    }
                } else {
                    vResult = true;
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        private bool ValidateDetail(OrdenDeProduccionDetalleArticulo valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            outErrorMessage = "";
            vResult = vResult && ValidateDetailOrdenDeProduccionDetalleMateriales(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailOrdenDeProduccionDetalleMateriales(OrdenDeProduccionDetalleArticulo valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (OrdenDeProduccionDetalleMateriales vDetail in valRecord.DetailOrdenDeProduccionDetalleMateriales) {
                bool vLineHasError = true;
                //agregar validaciones
                if (vDetail.ConsecutivoAlmacen == 0) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Consecutivo Almacen.");
                } else if (LibString.IsNullOrEmpty(vDetail.CodigoArticulo)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el CodigoArticulo.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Orden De Produccion Detalle Materiales"  + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<OrdenDeProduccion>  refMaster) {
            bool vResult = false;
            List<OrdenDeProduccionDetalleArticulo> vDetail = null;
            foreach (OrdenDeProduccion vItemMaster in refMaster) {
                vItemMaster.DetailOrdenDeProduccionDetalleArticulo = new ObservableCollection<OrdenDeProduccionDetalleArticulo>();
                vDetail = new LibDatabase().LoadFromSp<OrdenDeProduccionDetalleArticulo>("Adm.Gp_OrdenDeProduccionDetalleArticuloSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                if (vDetail != null && vDetail.Count > 0) {
                    new clsOrdenDeProduccionDetalleMaterialesDat().GetDetailAndAppendToMaster(ref vDetail);
                    foreach (OrdenDeProduccionDetalleArticulo vItemDetail in vDetail) {
                        vItemMaster.DetailOrdenDeProduccionDetalleArticulo.Add(vItemDetail);
                    }
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsOrdenDeProduccionDetalleArticuloDat

} //End of namespace Galac.Adm.Dal.GestionProduccion

