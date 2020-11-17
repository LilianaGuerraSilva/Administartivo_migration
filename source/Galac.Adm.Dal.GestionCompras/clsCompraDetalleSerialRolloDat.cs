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
    public class clsCompraDetalleSerialRolloDat: LibData, ILibDataDetailComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>> {
        #region Variables
        CompraDetalleSerialRollo _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private CompraDetalleSerialRollo CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCompraDetalleSerialRolloDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(CompraDetalleSerialRollo valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoCompra", valRecord.ConsecutivoCompra);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("CodigoArticulo", valRecord.CodigoArticulo, 30);
            vParams.AddInString("Serial", valRecord.Serial, 50);
            vParams.AddInString("Rollo", valRecord.Rollo, 20);
            vParams.AddInDecimal("Cantidad", valRecord.Cantidad, 2);
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

        private StringBuilder ParametrosClave(CompraDetalleSerialRollo valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

        private StringBuilder ParametrosProximoConsecutivo(CompraDetalleSerialRollo valRecord) {
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
            XElement vXElement = new XElement("GpDataCompraDetalleSerialRollo",
                from vEntity in valMaster.DetailCompraDetalleSerialRollo
                select new XElement("GpDetailCompraDetalleSerialRollo",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoCompra", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo),
                    new XElement("Serial", vEntity.Serial),
                    new XElement("Rollo", vEntity.Rollo),
                    new XElement("Cantidad", vEntity.Cantidad)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>>

        IList<CompraDetalleSerialRollo> ILibDataDetailComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<CompraDetalleSerialRollo> vResult = new List<CompraDetalleSerialRollo>();
            LibDataScope insDb = new LibDataScope();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<CompraDetalleSerialRollo>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<CompraDetalleSerialRollo>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Compra.Insertar")]
        LibResponse ILibDataDetailComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>>.Insert(IList<CompraDetalleSerialRollo> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDataScope insDb = new LibDataScope();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CompraDetalleSerialRolloINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<CompraDetalleSerialRollo>, IList<CompraDetalleSerialRollo>>

        public bool InsertChild(Compra valRecord, LibDataScope insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "CompraDetalleSerialRolloInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidCantidad(valAction, CurrentRecord.Cantidad);
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidCantidad(eAccionSR valAction, decimal valCantidad){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
           
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoCompra, int valConsecutivo) {
            bool vResult = false;
            CompraDetalleSerialRollo vRecordBusqueda = new CompraDetalleSerialRollo();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoCompra = valConsecutivoCompra;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDataScope insDb = new LibDataScope();
            vResult = insDb.ExistsRecord(DbSchema + ".CompraDetalleSerialRollo", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<Compra>  refMaster) {
            bool vResult = false;
            IList<CompraDetalleSerialRollo> vDetail = null;
            foreach (Compra vItemMaster in refMaster) {
                vItemMaster.DetailCompraDetalleSerialRollo = new ObservableCollection<CompraDetalleSerialRollo>();
                vDetail = new LibDataScope().LoadFromSp<CompraDetalleSerialRollo>("Adm.Gp_CompraDetalleSerialRolloSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (CompraDetalleSerialRollo vItemDetail in vDetail) {
                    vItemMaster.DetailCompraDetalleSerialRollo.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsCompraDetalleSerialRolloDat

} //End of namespace Galac.Adm.Dal.GestionCompras

