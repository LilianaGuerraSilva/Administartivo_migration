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
    public class clsListaDeMaterialesDetalleSalidasDat: LibData, ILibDataDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>> {
        #region Variables
        ListaDeMaterialesDetalleSalidas _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private ListaDeMaterialesDetalleSalidas CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsListaDeMaterialesDetalleSalidasDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(ListaDeMaterialesDetalleSalidas valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valRecord.ConsecutivoListaDeMateriales);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("CodigoArticuloInventario", valRecord.CodigoArticuloInventario, 30);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 8);
            vParams.AddInDecimal("PorcentajeDeCosto", valRecord.PorcentajeDeCosto, 4);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(ListaDeMateriales valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valRecord.Consecutivo);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(ListaDeMaterialesDetalleSalidas valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valRecord.ConsecutivoListaDeMateriales);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(ListaDeMaterialesDetalleSalidas valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valRecord.ConsecutivoListaDeMateriales);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(ListaDeMateriales valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoListaDeMateriales", valMaster.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(ListaDeMateriales valEntidad) {
            List<ListaDeMateriales> vListEntidades = new List<ListaDeMateriales>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(ListaDeMateriales valMaster) {
            XElement vXElement = new XElement("GpDataListaDeMaterialesDetalleSalidas",
                from vEntity in valMaster.DetailListaDeMaterialesDetalleSalidas
                select new XElement("GpDetailListaDeMaterialesDetalleSalidas",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoListaDeMateriales", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("CodigoArticuloInventario", vEntity.CodigoArticuloInventario),
                    new XElement("Cantidad", vEntity.Cantidad),
                    new XElement("PorcentajeDeCosto", vEntity.PorcentajeDeCosto)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>>

        IList<ListaDeMaterialesDetalleSalidas> ILibDataDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<ListaDeMaterialesDetalleSalidas> vResult = new List<ListaDeMaterialesDetalleSalidas>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<ListaDeMaterialesDetalleSalidas>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<ListaDeMaterialesDetalleSalidas>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Lista de Materiales.Insertar")]
        LibResponse ILibDataDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>>.Insert(IList<ListaDeMaterialesDetalleSalidas> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ListaDeMaterialesDetalleSalidasINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<ListaDeMaterialesDetalleSalidas>, IList<ListaDeMaterialesDetalleSalidas>>

        public bool InsertChild(ListaDeMateriales valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "ListaDeMaterialesDetalleSalidasInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoArticuloInventario(valAction, CurrentRecord.CodigoArticuloInventario);
            vResult = IsValidCantidad(valAction, CurrentRecord.Cantidad) && vResult;
            vResult = IsValidPorcentajeDeCosto(valAction, CurrentRecord.PorcentajeDeCosto) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidCodigoArticuloInventario(eAccionSR valAction, string valCodigoArticuloInventario){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoArticuloInventario = LibString.Trim(valCodigoArticuloInventario);
            if (LibString.IsNullOrEmpty(valCodigoArticuloInventario , true)) {
                BuildValidationInfo(MsgRequiredField("Código Inventario"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.ArticuloInventario", "Codigo", insDb.InsSql.ToSqlValue(valCodigoArticuloInventario), true)) {
                    BuildValidationInfo("El valor asignado al campo Código Inventario no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCantidad(eAccionSR valAction, decimal valCantidad){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                vResult = true;
            } else if (valCantidad < 0) {
                throw new GalacValidationException("La Cantidad debe ser mayor a 0");
            }
            return vResult;
        }

        private bool IsValidPorcentajeDeCosto(eAccionSR valAction, decimal valPorcentajeDeCosto){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                vResult = true;
            } else if (valPorcentajeDeCosto < 0 || valPorcentajeDeCosto >100) {
                throw new GalacValidationException("El %Costo debe ser mayor igual a 0 y menor igual a 100");
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoListaDeMateriales, int valConsecutivo) {
            bool vResult = false;
            ListaDeMaterialesDetalleSalidas vRecordBusqueda = new ListaDeMaterialesDetalleSalidas();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoListaDeMateriales = valConsecutivoListaDeMateriales;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".ListaDeMaterialesDetalleSalidas", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<ListaDeMateriales>  refMaster) {
            bool vResult = false;
            IList<ListaDeMaterialesDetalleSalidas> vDetail = null;
            foreach (ListaDeMateriales vItemMaster in refMaster) {
                vItemMaster.DetailListaDeMaterialesDetalleSalidas = new ObservableCollection<ListaDeMaterialesDetalleSalidas>();
                vDetail = new LibDatabase().LoadFromSp<ListaDeMaterialesDetalleSalidas>("Adm.Gp_ListaDeMaterialesDetalleSalidasSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (ListaDeMaterialesDetalleSalidas vItemDetail in vDetail) {
                    vItemMaster.DetailListaDeMaterialesDetalleSalidas.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsListaDeMaterialesDetalleSalidasDat

} //End of namespace Galac.Adm.Dal.GestionProduccion

