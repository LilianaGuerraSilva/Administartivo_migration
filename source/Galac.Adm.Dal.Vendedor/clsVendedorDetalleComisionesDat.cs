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
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Dal.Vendedor {
    public class clsVendedorDetalleComisionesDat: LibData, ILibDataDetailComponent<IList<VendedorDetalleComisiones>, IList<VendedorDetalleComisiones>> {
        #region Variables
        VendedorDetalleComisiones _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private VendedorDetalleComisiones CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsVendedorDetalleComisionesDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(VendedorDetalleComisiones valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoVendedor", valRecord.ConsecutivoVendedor);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vParams.AddInString("NombreDeLineaDeProducto", valRecord.NombreDeLineaDeProducto, 20);
            vParams.AddInEnum("TipoDeComision", valRecord.TipoDeComisionAsDB);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vParams.AddInDecimal("Porcentaje", valRecord.Porcentaje, 2);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionDetail(Galac.Adm.Ccl.Vendedor.Vendedor valRecord, eAccionSR eAccionSR) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInXml("XmlDataDetail", ParseToXml(valRecord));
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(VendedorDetalleComisiones valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoRenglon", valRecord.ConsecutivoRenglon);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(VendedorDetalleComisiones valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        StringBuilder ParametrosDetail(Galac.Adm.Ccl.Vendedor.Vendedor valMaster) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valMaster.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoVendedor", valMaster.Consecutivo);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement ParseToXml(Galac.Adm.Ccl.Vendedor.Vendedor valEntidad) {
            List<Galac.Adm.Ccl.Vendedor.Vendedor> vListEntidades = new List<Galac.Adm.Ccl.Vendedor.Vendedor>();
            vListEntidades.Add(valEntidad);
            XElement vXElement = new XElement("GpData",
                from vEntity in vListEntidades
                select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", vEntity.ConsecutivoCompania),
                    new XElement(BuildElementDetail(vEntity))));
            return vXElement;
        }

        private XElement BuildElementDetail(Galac.Adm.Ccl.Vendedor.Vendedor valMaster) {
            XElement vXElement = new XElement("GpDataVendedorDetalleComisiones",
                from vEntity in valMaster.DetailVendedorDetalleComisiones
                select new XElement("GpDetailVendedorDetalleComisiones",
                    new XElement("ConsecutivoCompania", valMaster.ConsecutivoCompania),
                    new XElement("ConsecutivoVendedor", vEntity.ConsecutivoVendedor),
                    new XElement("ConsecutivoRenglon", vEntity.ConsecutivoRenglon),
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
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
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

        public bool InsertChild(Galac.Adm.Ccl.Vendedor.Vendedor valRecord, LibTrn insTrn) {
            bool vResult = false;
            vResult = insTrn.ExecSpNonQuery(insTrn.ToSpName(DbSchema, "VendedorDetalleComisionesInsDet"), ParametrosActualizacionDetail(valRecord, eAccionSR.Insertar));
            return vResult;
        }
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoVendedor(valAction, CurrentRecord.ConsecutivoVendedor);
            vResult = IsValidNombreDeLineaDeProducto(valAction, CurrentRecord.NombreDeLineaDeProducto) && vResult;
            vResult = IsValidTipoDeComision(valAction, CurrentRecord.TipoDeComisionAsEnum) && vResult;
            vResult = IsValidMonto(valAction, CurrentRecord.Monto) && vResult;
            vResult = IsValidPorcentaje(valAction, CurrentRecord.Porcentaje) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoVendedor(eAccionSR valAction, int valConsecutivoVendedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoVendedor == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Vendedor"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNombreDeLineaDeProducto(eAccionSR valAction, string valNombreDeLineaDeProducto){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombreDeLineaDeProducto = LibString.Trim(valNombreDeLineaDeProducto);
            if (LibString.IsNullOrEmpty(valNombreDeLineaDeProducto, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre De Linea De Producto"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidTipoDeComision(eAccionSR valAction, eTipoComision valTipoDeComision){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidMonto(eAccionSR valAction, decimal valMonto){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            throw new ProgrammerMissingCodeException("Campo Decimal Obligatorio, debe especificar cual es su validacion");
            return vResult;
        }

        private bool IsValidPorcentaje(eAccionSR valAction, decimal valPorcentaje){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            throw new ProgrammerMissingCodeException("Campo Decimal Obligatorio, debe especificar cual es su validacion");
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoRenglon) {
            bool vResult = false;
            VendedorDetalleComisiones vRecordBusqueda = new VendedorDetalleComisiones();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoRenglon = valConsecutivoRenglon;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".VendedorDetalleComisiones", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones

        public bool GetDetailAndAppendToMaster(ref List<Galac.Adm.Ccl.Vendedor.Vendedor>  refMaster) {
            bool vResult = false;
            IList<VendedorDetalleComisiones> vDetail = null;
            foreach (Galac.Adm.Ccl.Vendedor.Vendedor vItemMaster in refMaster) {
                vItemMaster.DetailVendedorDetalleComisiones = new ObservableCollection<VendedorDetalleComisiones>();
                vDetail = new LibDatabase().LoadFromSp<VendedorDetalleComisiones>("Adm.Gp_VendedorDetalleComisionesSelDet", ParametrosDetail(vItemMaster), CmdTimeOut);
                foreach (VendedorDetalleComisiones vItemDetail in vDetail) {
                    vItemMaster.DetailVendedorDetalleComisiones.Add(vItemDetail);
                }
            }
            vResult = true;
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class clsVendedorDetalleComisionesDat

} //End of namespace Galac..Dal.ComponenteNoEspecificado

