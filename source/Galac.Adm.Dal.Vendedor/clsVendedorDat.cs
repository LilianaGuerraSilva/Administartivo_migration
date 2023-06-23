using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Entity = Galac.Adm.Ccl.Vendedor;
using Galac.Adm.Ccl.Vendedor;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Adm.Dal.Vendedor {
    public class clsVendedorDat: LibData, ILibDataMasterComponentWithSearch<IList<Entity.Vendedor>, IList<Entity.Vendedor>>, ILibDataImport, IVendedorDatPdn {
        #region Variables
        LibDataScope insTrn;
        Entity.Vendedor _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Entity.Vendedor CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores
        public clsVendedorDat() {
            DbSchema = "Adm";
            insTrn = new LibDataScope();
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Entity.Vendedor valRecord, eAccionSR valAction) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("Codigo", valRecord.Codigo, 5);
            vParams.AddInString("Nombre", valRecord.Nombre, 35);
            vParams.AddInString("RIF", valRecord.RIF, 20);
            vParams.AddInEnum("StatusVendedor", valRecord.StatusVendedorAsDB);
            vParams.AddInString("Direccion", valRecord.Direccion, 255);
            vParams.AddInString("Ciudad", valRecord.Ciudad, 100);
            vParams.AddInString("ZonaPostal", valRecord.ZonaPostal, 7);
            vParams.AddInString("Telefono", valRecord.Telefono, 40);
            vParams.AddInString("Fax", valRecord.Fax, 20);
            vParams.AddInString("Email", valRecord.Email, 40);
            vParams.AddInString("Notas", valRecord.Notas, 255);
            vParams.AddInInteger("ConsecutivoRutaDeComercializacion", valRecord.ConsecutivoRutaDeComercializacion);
            vParams.AddInDecimal("ComisionPorVenta", valRecord.ComisionPorVenta, 2);
            vParams.AddInDecimal("ComisionPorCobro", valRecord.ComisionPorCobro, 2);
            vParams.AddInDecimal("TopeInicialVenta1", valRecord.TopeInicialVenta1, 2);
            vParams.AddInDecimal("TopeFinalVenta1", valRecord.TopeFinalVenta1, 2);
            vParams.AddInDecimal("PorcentajeVentas1", valRecord.PorcentajeVentas1, 2);
            vParams.AddInDecimal("TopeFinalVenta2", valRecord.TopeFinalVenta2, 2);
            vParams.AddInDecimal("PorcentajeVentas2", valRecord.PorcentajeVentas2, 2);
            vParams.AddInDecimal("TopeFinalVenta3", valRecord.TopeFinalVenta3, 2);
            vParams.AddInDecimal("PorcentajeVentas3", valRecord.PorcentajeVentas3, 2);
            vParams.AddInDecimal("TopeFinalVenta4", valRecord.TopeFinalVenta4, 2);
            vParams.AddInDecimal("PorcentajeVentas4", valRecord.PorcentajeVentas4, 2);
            vParams.AddInDecimal("TopeFinalVenta5", valRecord.TopeFinalVenta5, 2);
            vParams.AddInDecimal("PorcentajeVentas5", valRecord.PorcentajeVentas5, 2);
            vParams.AddInDecimal("TopeInicialCobranza1", valRecord.TopeInicialCobranza1, 2);
            vParams.AddInDecimal("TopeFinalCobranza1", valRecord.TopeFinalCobranza1, 2);
            vParams.AddInDecimal("PorcentajeCobranza1", valRecord.PorcentajeCobranza1, 2);
            vParams.AddInDecimal("TopeFinalCobranza2", valRecord.TopeFinalCobranza2, 2);
            vParams.AddInDecimal("PorcentajeCobranza2", valRecord.PorcentajeCobranza2, 2);
            vParams.AddInDecimal("TopeFinalCobranza3", valRecord.TopeFinalCobranza3, 2);
            vParams.AddInDecimal("PorcentajeCobranza3", valRecord.PorcentajeCobranza3, 2);
            vParams.AddInDecimal("TopeFinalCobranza4", valRecord.TopeFinalCobranza4, 2);
            vParams.AddInDecimal("PorcentajeCobranza4", valRecord.PorcentajeCobranza4, 2);
            vParams.AddInDecimal("TopeFinalCobranza5", valRecord.TopeFinalCobranza5, 2);
            vParams.AddInDecimal("PorcentajeCobranza5", valRecord.PorcentajeCobranza5, 2);
            vParams.AddInBoolean("UsaComisionPorVenta", valRecord.UsaComisionPorVentaAsBool);
            vParams.AddInBoolean("UsaComisionPorCobranza", valRecord.UsaComisionPorCobranzaAsBool);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            return vParams.Get();
        }

        private StringBuilder ParametrosClavePrimaria(Entity.Vendedor valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClaveUnica(Entity.Vendedor valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Codigo", valRecord.Codigo, 5);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Entity.Vendedor valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        #region Miembros de ILibDataMasterComponent<IList<Vendedor>, IList<Vendedor>>

        LibResponse ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.CanBeChoosen(IList<Entity.Vendedor> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Entity.Vendedor vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.Cliente", "ConsecutivoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Cliente");
                }
                if (insDB.ExistsValueOnMultifile("dbo.CxC", "ConsecutivoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("CxC");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Cotizacion", "ConsecutivoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Cotizacion");
                }
                if (getParametrosCompania<string>("CodigoGenericoVendedor", this).Equals(vRecord.Codigo)) {
                    vSbInfo.AppendLine("Parámetros Compañía");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Cobranza", "ConsecutivoCobrador", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Cobranza");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Factura", "ConsecutivoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Factura");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RenglonFactura", "CodigoVendedor1", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Renglon Factura");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RenglonFactura", "CodigoVendedor2", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Renglon Factura");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RenglonFactura", "CodigoVendedor3", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Renglon Factura");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Contrato", "ConsecutivoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Consecutivo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Contrato");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Contrato", "NombreVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Nombre), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Contrato");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RetirosACuenta", "CodigoVendedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Retiros A Cuenta");
                }
                if (vSbInfo.Length == 0) {
                    vResult.Success = true;
                }
            } else {
                vResult.Success = true;
            }
            insDB.Dispose();
            if (!vResult.Success) {
                vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
                throw new GalacAlertException(vErrMsg);
            } else {
                return vResult;
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vendedor.Eliminar")]
        LibResponse ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.Delete(IList<Entity.Vendedor> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "VendedorDEL"), ParametrosClavePrimaria(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Entity.Vendedor> ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters, bool valUseDetail) {
            List<Entity.Vendedor> vResult = new List<Entity.Vendedor>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Entity.Vendedor>(valProcessMessage, valParameters, CmdTimeOut);
                    if (valUseDetail && vResult != null && vResult.Count > 0) {
                        new clsVendedorDetalleComisionesDat().GetDetailAndAppendToMaster(ref vResult);
                    }
                    break;
                default:
                    break;
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vendedor.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.Insert(IList<Entity.Vendedor> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out string vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive(DbSchema + ".Vendedor", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    if (insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "VendedorINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                        if (valUseDetail) {
                            vResult.Success = true;
                            InsertDetail(CurrentRecord);
                        } else {
                            vResult.Success = true;
                        }
                        if (vResult.Success) {
                            ExecuteProcessAfterInsert();
                        }
                    }
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoCodigo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".Vendedor", "Codigo", valParameters, true, 5), "Codigo");
                    }
                    if (valProcessMessage == "ProximoConsecutivo") {
                        vResult = LibXml.ToXElement(LibXml.ValueToXmlDocument(insDb.NextLngConsecutive(DbSchema + ".Vendedor", "Consecutivo", valParameters.ToString()), "Consecutivo"));
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default:
                    throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.SpecializedUpdate(IList<Entity.Vendedor> refRecord, bool valUseDetail, string valSpecializedAction) {
            throw new NotImplementedException();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Vendedor.Modificar")]
        LibResponse ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.Update(IList<Entity.Vendedor> refRecord, bool valUseDetail, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            try {
                CurrentRecord = refRecord[0];
                if (ValidateMasterDetail(valAction, CurrentRecord, valUseDetail)) {
                    if (ExecuteProcessBeforeUpdate()) {
                        if (valUseDetail) {
                            vResult = UpdateMasterAndDetail(CurrentRecord, valAction);
                        } else {
                            vResult = UpdateMaster(CurrentRecord, valAction);
                        }
                        if (vResult.Success) {
                            ExecuteProcessAfterUpdate();
                        }
                    }
                }
                return vResult;
            } finally {

            }
        }

        bool ILibDataMasterComponent<IList<Entity.Vendedor>, IList<Entity.Vendedor>>.ValidateAll(IList<Entity.Vendedor> refRecords, bool valUseDetail, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Entity.Vendedor vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }
        #endregion //ILibDataMasterComponent<IList<Vendedor>, IList<Vendedor>>

        LibResponse UpdateMaster(Entity.Vendedor refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            vResult.Success = insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "VendedorUPD"), ParametrosActualizacion(refRecord, valAction));
            return vResult;
        }

        LibResponse UpdateMasterAndDetail(Entity.Vendedor refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            string vErrorMessage = "";
            if (ValidateDetail(refRecord, eAccionSR.Modificar, out vErrorMessage)) {
                if (UpdateDetail(refRecord)) {
                    vResult = UpdateMaster(refRecord, valAction);
                }
            }
            return vResult;
        }

        private bool InsertDetail(Entity.Vendedor valRecord) {
            bool vResult = true;
            vResult = vResult && SetPkInDetailVendedorDetalleComisionesAndUpdateDb(valRecord);
            return vResult;
        }

        private bool SetPkInDetailVendedorDetalleComisionesAndUpdateDb(Ccl.Vendedor.Vendedor valRecord) {
            int vConsecutivo = 1;
            clsVendedorDetalleComisionesDat insVendedorDetalleComisiones = new clsVendedorDetalleComisionesDat();
            foreach (VendedorDetalleComisiones vDetail in valRecord.DetailVendedorDetalleComisiones) {
                vDetail.ConsecutivoCompania = valRecord.ConsecutivoCompania;
                vDetail.ConsecutivoVendedor = valRecord.Consecutivo;
                vDetail.Consecutivo = vConsecutivo;
                vConsecutivo++;
            }
            return insVendedorDetalleComisiones.InsertChild(valRecord, insTrn);
        }

        private bool UpdateDetail(Entity.Vendedor valRecord) {
            return SetPkInDetailVendedorDetalleComisionesAndUpdateDb(valRecord);
        }

        #region Validaciones

        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            ClearValidationInfo();
            bool vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo);
            vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
            vResult = IsValidCodigo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo) && vResult;
            vResult = IsValidNombre(valAction, CurrentRecord.Nombre) && vResult;
            vResult = IsValidCiudad(valAction, CurrentRecord.Ciudad) && vResult;
            vResult = IsValidConsecutivoRutaDeComercializacion(valAction, CurrentRecord.ConsecutivoRutaDeComercializacion) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCompania <= 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
                return false;
            }
            return true;
        }

        private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo", valConsecutivo));
                    return false;
                }
            } else {
                if (valConsecutivo == 0) {
                    BuildValidationInfo(MsgRequiredField("Consecutivo"));
                    return false;
                }
            }
            return true;
        }

        private bool IsValidCodigo(eAccionSR valAction, int valConsecutivoCompania, string valCodigo) {
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibString.IsNullOrEmpty(valCodigo, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                return false;
            } else if (valAction == eAccionSR.Insertar) {
                if (UniqueKeyExists(valConsecutivoCompania, valCodigo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código", valCodigo));
                    return false;
                }
            }
            return true;
        }

        private bool IsValidNombre(eAccionSR valAction, string valNombre) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombre = LibString.Trim(valNombre);
            if (LibString.IsNullOrEmpty(valNombre, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCiudad(eAccionSR valAction, string valCiudad) {
            bool vResult = true;
            valCiudad = LibString.Trim(valCiudad);
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibString.IsNullOrEmpty(valCiudad, true)) {
                BuildValidationInfo(MsgRequiredField("Ciudad"));
                return false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.MunicipioCiudad", "NombreCiudad", insDb.InsSql.ToSqlValue(valCiudad), true)) {
                    BuildValidationInfo("El valor asignado al campo ciudad no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidConsecutivoRutaDeComercializacion(eAccionSR valAction, int valConsecutivoRutaDeComercializacion) {
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoRutaDeComercializacion == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Ruta De Comercializacion"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Saw.RutaDeComercializacion", "Consecutivo", insDb.InsSql.ToSqlValue(valConsecutivoRutaDeComercializacion), true)) {
                    BuildValidationInfo("El valor asignado al campo Consecutivo Ruta De Comercializacion no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
            Entity.Vendedor vRecordBusqueda = new Entity.Vendedor();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Consecutivo = valConsecutivo;
            LibDatabase insDb = new LibDatabase();
            bool vResult = insDb.ExistsRecord(DbSchema + ".Vendedor", "ConsecutivoCompania", ParametrosClavePrimaria(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(Entity.Vendedor valRecordBusqueda) {
            LibDatabase insDb = new LibDatabase();
            bool vResult = insDb.ExistsRecord(DbSchema + ".Vendedor", "ConsecutivoCompania", ParametrosClavePrimaria(valRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool UniqueKeyExists(int valConsecutivoCompania, string valCodigo) {
            Entity.Vendedor vRecordBusqueda = new Entity.Vendedor();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Codigo = valCodigo;
            LibDatabase insDb = new LibDatabase();
            bool vResult = insDb.ExistsRecord(DbSchema + ".Vendedor", "ConsecutivoCompania", ParametrosClaveUnica(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool ValidateMasterDetail(eAccionSR valAction, Entity.Vendedor valRecordMaster, bool valUseDetail) {
            string vErrMsg;
            if (Validate(valAction, out vErrMsg)) {
                if (valUseDetail) {
                    if (!ValidateDetail(valRecordMaster, eAccionSR.Insertar, out vErrMsg)) {
                        throw new GalacValidationException("Vendedor (detalle)\n" + vErrMsg);
                    }
                }
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return true;
        }

        private bool ValidateDetail(Entity.Vendedor valRecord, eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            vResult = ValidateDetailVendedorDetalleComisiones(valRecord, valAction, out outErrorMessage);
            return vResult;
        }

        private bool ValidateDetailVendedorDetalleComisiones(Entity.Vendedor valRecord, eAccionSR eAccionSR, out string outErrorMessage) {
            bool vResult = true;
            StringBuilder vSbErrorInfo = new StringBuilder();
            int vNumeroDeLinea = 1;
            outErrorMessage = string.Empty;
            foreach (VendedorDetalleComisiones vDetail in valRecord.DetailVendedorDetalleComisiones) {
                bool vLineHasError = true;
                if (LibString.IsNullOrEmpty(vDetail.NombreDeLineaDeProducto)) {
                    vSbErrorInfo.AppendLine("Línea " + vNumeroDeLinea.ToString() + ": No fue asignado el Nombre de la Línea de Producto.");
                } else if (vDetail.TipoDeComisionAsEnum == eTipoComision.PorPorcentaje && vDetail.Porcentaje >= 100) {
                    vSbErrorInfo.AppendLine("El porcentaje debe ser menor al 100%.");
                } else if (vDetail.TipoDeComisionAsEnum == eTipoComision.PorPorcentaje && vDetail.Porcentaje == 0) {
                    vSbErrorInfo.AppendLine("El porcentaje requerido.");
                } else {
                    vLineHasError = false;
                }
                vResult = vResult && (!vLineHasError);
                vNumeroDeLinea++;
            }
            if (!vResult) {
                outErrorMessage = "Comisiones de Vendedor" + Environment.NewLine + vSbErrorInfo.ToString();
            }
            return vResult;
        }

        private T getParametrosCompania<T>(string ValParametro, ILibDataMasterComponentWithSearch<IList<Entity.Vendedor>, IList<Entity.Vendedor>> instanciaDal) {
            string vDbSchema = "";
            if (LibDefGen.IsProduct(LibProduct.GetInitialsAdmEcuador())) {
                vDbSchema = "Adme";
            } else {
                vDbSchema = "Comun";
            }
            StringBuilder sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS Valor FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE (" + vDbSchema + ".SettDefinition.Name = '" + ValParametro + "')", 300);
            XElement Auxiliar = instanciaDal.QueryInfo(eProcessMessageType.Query, sql.ToString(), null);
            Object vValor = LibXml.GetPropertyString(Auxiliar, "Valor");
            return (T)vValor;
        }

        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch

        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase(clsCkn.ConfigKeyForDbServiceAdministrativoVendedor);
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }

        #endregion //Miembros de ILibDataFKSearch

        [PrincipalPermission(SecurityAction.Demand, Role = "Vendedor.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Vendedor.Importar")]
        LibXmlResult ILibDataImport.Import(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
            try {
                string vMessage = "";
                int vIndex = 0;
                LibXmlResult vResult = new LibXmlResult();
                vResult.AddTitle("Importación Vendedor");
                List<Entity.Vendedor> vList = ParseToListEntity(refRecord);
                LibDatabase insDb = new LibDatabase();
                int vTotal = vList.Count();
                foreach (Entity.Vendedor item in vList) {
                    try {
                        vMessage = string.Format("Insertando {0:n0} de {1:n0}", vIndex, vTotal);
                        insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "VendedorINS"), ParametrosActualizacion(item, eAccionSR.Insertar));
                    } catch (System.Data.SqlClient.SqlException vEx) {
                        if (LibExceptionMng.IsPrimaryKeyViolation(vEx)) {
                            vResult.AddDetailWithAttribute(item.Codigo, "Ya existe", eXmlResultType.Error);
                        } else {
                            throw;
                        }
                    }
                    if (valManager.CancellationPending) {
                        break;
                    }
                    vIndex++;
                    valManager.ReportProgress(vIndex, "Ejecutando por favor espere...", vMessage, (vIndex >= vTotal) && (valShowMessage));
                }
                insDb.Dispose();
                return vResult;
            } catch (Exception) {
                throw;
            }
        }

        private List<Entity.Vendedor> ParseToListEntity(XmlReader valXmlEntity) {
            List<Entity.Vendedor> vResult = new List<Entity.Vendedor>();
            XDocument xDoc = XDocument.Load(valXmlEntity);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Entity.Vendedor vRecord = new Entity.Vendedor();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Codigo"), null))) {
                    vRecord.Codigo = vItem.Element("Codigo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null))) {
                    vRecord.Nombre = vItem.Element("Nombre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("RIF"), null))) {
                    vRecord.RIF = vItem.Element("RIF").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusVendedor"), null))) {
                    vRecord.StatusVendedor = vItem.Element("StatusVendedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Direccion"), null))) {
                    vRecord.Direccion = vItem.Element("Direccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Ciudad"), null))) {
                    vRecord.Ciudad = vItem.Element("Ciudad").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ZonaPostal"), null))) {
                    vRecord.ZonaPostal = vItem.Element("ZonaPostal").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Telefono"), null))) {
                    vRecord.Telefono = vItem.Element("Telefono").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fax"), null))) {
                    vRecord.Fax = vItem.Element("Fax").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Email"), null))) {
                    vRecord.Email = vItem.Element("Email").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Notas"), null))) {
                    vRecord.Notas = vItem.Element("Notas").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRutaDeComercializacion"), null))) {
                    vRecord.ConsecutivoRutaDeComercializacion = LibConvert.ToInt(vItem.Element("ConsecutivoRutaDeComercializacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ComisionPorVenta"), null))) {
                    vRecord.ComisionPorVenta = LibConvert.ToDec(vItem.Element("ComisionPorVenta"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ComisionPorCobro"), null))) {
                    vRecord.ComisionPorCobro = LibConvert.ToDec(vItem.Element("ComisionPorCobro"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeInicialVenta1"), null))) {
                    vRecord.TopeInicialVenta1 = LibConvert.ToDec(vItem.Element("TopeInicialVenta1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalVenta1"), null))) {
                    vRecord.TopeFinalVenta1 = LibConvert.ToDec(vItem.Element("TopeFinalVenta1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeVentas1"), null))) {
                    vRecord.PorcentajeVentas1 = LibConvert.ToDec(vItem.Element("PorcentajeVentas1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalVenta2"), null))) {
                    vRecord.TopeFinalVenta2 = LibConvert.ToDec(vItem.Element("TopeFinalVenta2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeVentas2"), null))) {
                    vRecord.PorcentajeVentas2 = LibConvert.ToDec(vItem.Element("PorcentajeVentas2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalVenta3"), null))) {
                    vRecord.TopeFinalVenta3 = LibConvert.ToDec(vItem.Element("TopeFinalVenta3"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeVentas3"), null))) {
                    vRecord.PorcentajeVentas3 = LibConvert.ToDec(vItem.Element("PorcentajeVentas3"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalVenta4"), null))) {
                    vRecord.TopeFinalVenta4 = LibConvert.ToDec(vItem.Element("TopeFinalVenta4"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeVentas4"), null))) {
                    vRecord.PorcentajeVentas4 = LibConvert.ToDec(vItem.Element("PorcentajeVentas4"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalVenta5"), null))) {
                    vRecord.TopeFinalVenta5 = LibConvert.ToDec(vItem.Element("TopeFinalVenta5"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeVentas5"), null))) {
                    vRecord.PorcentajeVentas5 = LibConvert.ToDec(vItem.Element("PorcentajeVentas5"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeInicialCobranza1"), null))) {
                    vRecord.TopeInicialCobranza1 = LibConvert.ToDec(vItem.Element("TopeInicialCobranza1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalCobranza1"), null))) {
                    vRecord.TopeFinalCobranza1 = LibConvert.ToDec(vItem.Element("TopeFinalCobranza1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeCobranza1"), null))) {
                    vRecord.PorcentajeCobranza1 = LibConvert.ToDec(vItem.Element("PorcentajeCobranza1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalCobranza2"), null))) {
                    vRecord.TopeFinalCobranza2 = LibConvert.ToDec(vItem.Element("TopeFinalCobranza2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeCobranza2"), null))) {
                    vRecord.PorcentajeCobranza2 = LibConvert.ToDec(vItem.Element("PorcentajeCobranza2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalCobranza3"), null))) {
                    vRecord.TopeFinalCobranza3 = LibConvert.ToDec(vItem.Element("TopeFinalCobranza3"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeCobranza3"), null))) {
                    vRecord.PorcentajeCobranza3 = LibConvert.ToDec(vItem.Element("PorcentajeCobranza3"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalCobranza4"), null))) {
                    vRecord.TopeFinalCobranza4 = LibConvert.ToDec(vItem.Element("TopeFinalCobranza4"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeCobranza4"), null))) {
                    vRecord.PorcentajeCobranza4 = LibConvert.ToDec(vItem.Element("PorcentajeCobranza4"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TopeFinalCobranza5"), null))) {
                    vRecord.TopeFinalCobranza5 = LibConvert.ToDec(vItem.Element("TopeFinalCobranza5"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeCobranza5"), null))) {
                    vRecord.PorcentajeCobranza5 = LibConvert.ToDec(vItem.Element("PorcentajeCobranza5"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("UsaComisionPorVenta"), null))) {
                    vRecord.UsaComisionPorVenta = vItem.Element("UsaComisionPorVenta").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("UsaComisionPorCobranza"), null))) {
                    vRecord.UsaComisionPorCobranza = vItem.Element("UsaComisionPorCobranza").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null))) {
                    vRecord.CodigoLote = vItem.Element("CodigoLote").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }

        public LibResponse InsertarListaDeVendedores(IList<Entity.Vendedor> valListOfRecords) {
            return InsertRecord(valListOfRecords);
        }

        private LibResponse InsertRecord(IList<Entity.Vendedor> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg;
            foreach (var item in refRecord) {
                CurrentRecord = item;
                if (ExecuteProcessBeforeInsert()) {
                    if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                        if (insTrn.ExecSpNonQueryWithScope(insTrn.ToSpName(DbSchema, "VendedorINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar))) {
                            vResult.Success = true;
                            if (vResult.Success) {
                                ExecuteProcessAfterInsert();
                            }
                        }
                    }
                }
            }
            return vResult;
        }

        #endregion //Metodos Generados
    } //End of class clsVendedorDat

} //End of namespace Galac.Adm.Dal.Vendedor

