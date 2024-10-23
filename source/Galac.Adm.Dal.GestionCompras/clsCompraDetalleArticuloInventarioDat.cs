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
    public class clsCompraDetalleArticuloInventarioDat: LibData, ILibDataDetailComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>> {
        #region Variables
        CompraDetalleArticuloInventario _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CompraDetalleArticuloInventario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCompraDetalleArticuloInventarioDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CompraDetalleArticuloInventario valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valRecord.ConsecutivoCompra);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 2);
            vParams.AddInDecimal("PrecioUnitario", valRecord.PrecioUnitario, 2);
            vParams.AddInDecimal("CantidadRecibida", valRecord.CantidadRecibida, 2);
            vParams.AddInDecimal("PorcentajeDeDistribucion", valRecord.PorcentajeDeDistribucion, 2);
            vParams.AddInDecimal("MontoDistribucion", valRecord.MontoDistribucion, 2);
            vParams.AddInDecimal("PorcentajeSeguro", valRecord.PorcentajeSeguro, 2);
            vParams.AddInInteger("ConsecutivoLoteDeInventario", valRecord.ConsecutivoLoteDeInventario);
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

        private StringBuilder ParametrosClave(CompraDetalleArticuloInventario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valRecord.ConsecutivoCompra);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(CompraDetalleArticuloInventario valRecord) {
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
            XElement vXElement = new XElement("GpDataCompraDetalleArticuloInventario",
                from vEntity in valMaster.DetailCompraDetalleArticuloInventario
                select new XElement("GpDetailCompraDetalleArticuloInventario",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoCompra", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo),
                    new XElement("Cantidad", vEntity.Cantidad),
                    new XElement("PrecioUnitario", vEntity.PrecioUnitario),
                    new XElement("CantidadRecibida", vEntity.CantidadRecibida),
                    new XElement("PorcentajeDeDistribucion", vEntity.PorcentajeDeDistribucion),
                    new XElement("MontoDistribucion", vEntity.MontoDistribucion),
                    new XElement("PorcentajeSeguro", vEntity.PorcentajeSeguro),
                    new XElement("ConsecutivoLoteDeInventario", vEntity.ConsecutivoLoteDeInventario)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>>

        IList<CompraDetalleArticuloInventario> ILibDataDetailComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CompraDetalleArticuloInventario> vResult = new List<CompraDetalleArticuloInventario>();
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CompraDetalleArticuloInventario>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CompraDetalleArticuloInventario>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Compra.Insertar")]
        LibResponse ILibDataDetailComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>>.Insert(IList<CompraDetalleArticuloInventario> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDataScope insDb = new LibDataScope();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CompraDetalleArticuloInventarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<CompraDetalleArticuloInventario>, IList<CompraDetalleArticuloInventario>>

        public bool InsertChild(Compra valRecord, LibDataScope insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQueryWithScope (insTrn.ToSpName(DbSchema, "CompraDetalleArticuloInventarioInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCodigoArticulo(valAction, CurrentRecord.CodigoArticulo);
            vResult = IsValidCantidad(valAction, CurrentRecord.Cantidad) && vResult;
            vResult = IsValidPrecioUnitario(valAction, CurrentRecord.PrecioUnitario) && vResult;
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
            
            return vResult;
        }

        private bool IsValidPrecioUnitario(eAccionSR valAction, decimal valPrecioUnitario){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
           
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoCompra, int valConsecutivo) {
            bool vResult = false;
            CompraDetalleArticuloInventario vRecordBusqueda = new CompraDetalleArticuloInventario();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoCompra = valConsecutivoCompra;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDataScope insDb = new LibDataScope();
            vResult = insDb.ExistsRecord(DbSchema + ".CompraDetalleArticuloInventario", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<Compra>  refMaster) {
            bool vResult = false;
            IList<CompraDetalleArticuloInventario> vDetail = null;
            foreach (Compra vItemMaster in refMaster) {
                vItemMaster.DetailCompraDetalleArticuloInventario = new ObservableCollection<CompraDetalleArticuloInventario>();
                vDetail = new LibDataScope().LoadFromSp<CompraDetalleArticuloInventario>("Adm.Gp_CompraDetalleArticuloInventarioSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (CompraDetalleArticuloInventario vItemDetail in vDetail) {
                    vItemMaster.DetailCompraDetalleArticuloInventario.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCompraDetalleArticuloInventarioDat

} //End of namespace Galac.Adm.Dal.GestionCompras

