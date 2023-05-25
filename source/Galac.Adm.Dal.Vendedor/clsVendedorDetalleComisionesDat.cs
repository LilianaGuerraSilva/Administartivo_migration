using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Dal.Vendedor {
    public class clsVendedorDetalleComisionesDat: LibData, ILibDataDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>>, IVendedorDatDetalleComisionesPdn {
        #region Variables
        VendedorDetalleComisiones _CurrentRecord;
        LibDataScope insTrn;
        #endregion //Variables
        #region Propiedades
        private VendedorDetalleComisiones CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsVendedorDetalleComisionesDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(VendedorDetalleComisiones valRecord, eAccionSR valAction) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoVendedor", valRecord.ConsecutivoVendedor);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("NombreDeLineaDeProducto", valRecord.NombreDeLineaDeProducto, 20);
            vParams.AddInEnum("TipoDeComision", valRecord.TipoDeComisionAsDB);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vParams.AddInDecimal("Porcentaje", valRecord.Porcentaje, 2);
            return vParams.Get();
        }

        private StringBuilder ParametrosActualizacionDetail(Ccl.Vendedor.Vendedor valRecord, eAccionSR eAccionSR) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoVendedor", valRecord.Consecutivo);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            return vParams.Get();
        }

        private StringBuilder ParametrosClave(VendedorDetalleComisiones valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoVendedor", valRecord.ConsecutivoVendedor);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            return vParams.Get();
        }

        private StringBuilder ParametrosProximoConsecutivo(VendedorDetalleComisiones valRecord) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoVendedor", valRecord.ConsecutivoVendedor);
            return vParams.Get();
        }

        StringBuilder ParametrosDetail(Ccl.Vendedor.Vendedor valMaster) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoVendedor", valMaster.Consecutivo);
            return vParams.Get();
        }

        private XElement ParseToXml(Ccl.Vendedor.Vendedor valEntidad) {
            List<Ccl.Vendedor.Vendedor> vListEntidades = new List<Ccl.Vendedor.Vendedor>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(Ccl.Vendedor.Vendedor valMaster) {
            XElement vXElement = new XElement("GpDataVendedorDetalleComisiones",
                from vEntity in valMaster.DetailVendedorDetalleComisiones
                select new XElement("GpDetailVendedorDetalleComisiones",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoVendedor", valMaster.Consecutivo),
                    new XElement("Consecutivo", vEntity.Consecutivo),
                    new XElement("NombreDeLineaDeProducto", vEntity.NombreDeLineaDeProducto),
                    new XElement("TipoDeComision", vEntity.TipoDeComisionAsDB),
                    new XElement("Monto", vEntity.Monto),
                    new XElement("Porcentaje", vEntity.Porcentaje)));
            return vXElement;
        }
        #region Miembros de ILibDataDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>>

        IList<VendedorDetalleComisiones> ILibDataDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<VendedorDetalleComisiones> vResult = new List<VendedorDetalleComisiones>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<VendedorDetalleComisiones>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<VendedorDetalleComisiones>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vendedor.Insertar")]
        LibResponse ILibDataDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>>.Insert(IList<VendedorDetalleComisiones> refRecord, XmlReader valExtended) {
            LibResponse vResult = new LibResponse();
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                string vErrMsg;
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "VendedorDetalleComisionesINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }
        #endregion //ILibDataDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>>

        public bool InsertChild(Ccl.Vendedor.Vendedor valRecord, LibDataScope insTrn) {
            return insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "VendedorDetalleComisionesInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
        }

        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoVendedor(valAction, CurrentRecord.ConsecutivoVendedor);
            vResult = IsValidNombreDeLineaDeProducto(valAction, CurrentRecord.NombreDeLineaDeProducto) && vResult;
            vResult = IsValidTipoDeComision(valAction, CurrentRecord.TipoDeComisionAsEnum)&& vResult;
            vResult = IsValidMonto(valAction, CurrentRecord.Monto)&& vResult;
            vResult = IsValidPorcentaje(valAction, CurrentRecord.Porcentaje)&& vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoVendedor(eAccionSR valAction, int valConsecutivoVendedor){
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoVendedor == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Vendedor"));
                return false;
            }
            return true;
        }

        private bool IsValidNombreDeLineaDeProducto(eAccionSR valAction, string valNombreDeLineaDeProducto){
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombreDeLineaDeProducto = LibString.Trim(valNombreDeLineaDeProducto);
            if (LibString.IsNullOrEmpty(valNombreDeLineaDeProducto, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre De Linea De Producto"));
                return false;
            }
            return true;
        }

        private bool IsValidTipoDeComision(eAccionSR valAction, eTipoComision valTipoDeComision){
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return (valTipoDeComision == eTipoComision.PorPorcentaje) || (valTipoDeComision == eTipoComision.PorMonto);
        }

        private bool IsValidMonto(eAccionSR valAction, decimal valMonto) {
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            } else {
                return valMonto >= 0;
            }
        }

        private bool IsValidPorcentaje(eAccionSR valAction, decimal valPorcentaje) {
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            } else {
                return valPorcentaje >= 0;
            }
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoVendedor, int valConsecutivoRenglon) {
            VendedorDetalleComisiones vRecordBusqueda = new VendedorDetalleComisiones();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoVendedor = valConsecutivoVendedor;
            vRecordBusqueda.Consecutivo = valConsecutivoVendedor;
            LibDatabase insDb = new LibDatabase();
            bool vResult = insDb.ExistsRecord(DbSchema + ".VendedorDetalleComisiones", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<Ccl.Vendedor.Vendedor>  refMaster) {
            foreach (Ccl.Vendedor.Vendedor vItemMaster in refMaster) {
                vItemMaster.DetailVendedorDetalleComisiones = new ObservableCollection<VendedorDetalleComisiones>();
                IList<VendedorDetalleComisiones> vDetail = new LibDatabase().LoadFromSp<VendedorDetalleComisiones>("Adm.Gp_VendedorDetalleComisionesSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (VendedorDetalleComisiones vItemDetail in vDetail) {
                    vItemMaster.DetailVendedorDetalleComisiones.Add(vItemDetail);
                }
            }
            bool vResult = true;
            return vResult;
        }

        public LibResponse InsertarListaDeVendedorDetail(IList<VendedorDetalleComisiones> valListOfRecords) {
            return InsertDetail(valListOfRecords);
        }
        private LibResponse InsertDetail(IList<VendedorDetalleComisiones> refRecord) {
            LibResponse vResult = new LibResponse();
            try {
                foreach (var item in refRecord) {
                    CurrentRecord = item;
                    if (ExecuteProcessBeforeInsert()) {
                        string vErrMsg;
                        if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                            if (insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "VendedorDetalleComisionesINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                                vResult.Success = true;
                                if (vResult.Success) {
                                    ExecuteProcessAfterInsert();
                                }
                            }
                        }
                    }
                }
                return vResult;
            } finally {
                if (!vResult.Success) {
                }
            }
        }
        #endregion //Metodos Generados


    } //End of class clsVendedorDetalleComisionesDat

} //End of namespace Galac..Dal.ComponenteNoEspecificado

