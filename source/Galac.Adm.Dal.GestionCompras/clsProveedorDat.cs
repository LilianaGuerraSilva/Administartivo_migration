using System;
using System.Linq;
using System.Collections.Generic;
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
using LibGalac.Aos.Base.Report;
using System.Data;

namespace Galac.Adm.Dal.GestionCompras {
    public class clsProveedorDat: LibData, ILibDataComponentWithSearch<IList<Proveedor>, IList<Proveedor>>, ILibDataImport, ILibDataRpt {
        #region Variables
        Proveedor _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Proveedor CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsProveedorDat() {
            DbSchema = "Adm";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Proveedor valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoProveedor", valRecord.CodigoProveedor, 10);
            vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
            vParams.AddInString("NombreProveedor", valRecord.NombreProveedor, 160);
            vParams.AddInString("Contacto", valRecord.Contacto, 35);
            vParams.AddInString("NumeroRIF", valRecord.NumeroRIF, 20);
            vParams.AddInString("NumeroNIT", valRecord.NumeroNIT, 12);
            vParams.AddInEnum("TipoDePersona", valRecord.TipoDePersonaAsDB);
            vParams.AddInString("CodigoRetencionUsual", valRecord.CodigoRetencionUsual, 6);
            vParams.AddInString("Telefonos", valRecord.Telefonos, 40);
            vParams.AddInString("Direccion", valRecord.Direccion, 255);
            vParams.AddInString("Fax", valRecord.Fax, 25);
            vParams.AddInString("Email", valRecord.Email, 100);
            vParams.AddInString("TipodeProveedor", valRecord.TipodeProveedor, 20);
            vParams.AddInEnum("TipoDeProveedorDeLibrosFiscales", valRecord.TipoDeProveedorDeLibrosFiscalesAsDB);
            vParams.AddInDecimal("PorcentajeRetencionIVA", valRecord.PorcentajeRetencionIVA, 2);
            vParams.AddInString("CuentaContableCxP", valRecord.CuentaContableCxP, 30);
            vParams.AddInString("CuentaContableGastos", valRecord.CuentaContableGastos, 30);
            vParams.AddInString("CuentaContableAnticipo", valRecord.CuentaContableAnticipo, 30);
            vParams.AddInString("CodigoLote", valRecord.CodigoLote, 10);
            vParams.AddInString("Beneficiario", valRecord.Beneficiario, 60);
            vParams.AddInBoolean("UsarBeneficiarioImpCheq", valRecord.UsarBeneficiarioImpCheqAsBool);
            vParams.AddInEnum("TipoDocumentoIdentificacion", valRecord.TipoDocumentoIdentificacionAsDB);
            vParams.AddInBoolean("EsAgenteDeRetencionIva", valRecord.EsAgenteDeRetencionIvaAsBool);
            vParams.AddInString("Nombre", valRecord.Nombre, 20);
            vParams.AddInString("ApellidoPaterno", valRecord.ApellidoPaterno, 20);
            vParams.AddInString("ApellidoMaterno", valRecord.ApellidoMaterno, 20);
            vParams.AddInString("NumeroCuentaBancaria", valRecord.NumeroCuentaBancaria, 20);
            vParams.AddInString("CodigoContribuyente", valRecord.CodigoContribuyente, 20);
            vParams.AddInString("NumeroRUC", valRecord.NumeroRUC, 20);
            vParams.AddInEnum("TipoDePersonaLibrosElectronicos", valRecord.TipoDePersonaLibrosElectronicosAsDB);
            vParams.AddInString("CodigoPaisResidencia", valRecord.CodigoPaisResidencia, 4);
            vParams.AddInString("CodigoConveniosSunat", valRecord.CodigoConveniosSunat, 2);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Proveedor valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoProveedor", valRecord.CodigoProveedor, 10);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClaveOtros(Proveedor valRecord, string valCampo, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            switch (valCampo) {
                case "NombreProveedor":
                    vParams.AddInString(valCampo, valRecord.NombreProveedor, 160);
                    break;
                case "NumeroRIF":
                    vParams.AddInString(valCampo, valRecord.NumeroRIF, 20);
                    break;
            }
            if (valIncludeTimestamp) {
               vParams.AddInTimestamp("fldTimeStamp", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Proveedor valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosActualizacionAuxiliar(Proveedor valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoPeriodo", valRecord.ConsecutivoPeriodo);
            vParams.AddInString("TipoDeAuxiliar", "1", 1);
            vParams.AddInString("Codigo", valRecord.CodigoProveedor, 10);
            vParams.AddInString("Nombre", valRecord.NombreProveedor, 35);
            vParams.AddInString("NoRif", valRecord.NumeroRIF, 10);
            vParams.AddInString("NoNit", valRecord.NumeroNIT, 10);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 20);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClaveAuxiliar(Proveedor valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoPeriodo", valRecord.ConsecutivoPeriodo);
            vParams.AddInString("Codigo", valRecord.CodigoProveedor, 10);
            vParams.AddInString("TipoDeAuxiliar", "1", 1);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        #region Miembros de ILibDataComponent<IList<Proveedor>, IList<Proveedor>>

        LibResponse ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.CanBeChoosen(IList<Proveedor> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Proveedor vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("Adm.DetalleDeRendicion", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Rendicion");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Anticipo", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Anticipo");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Pago", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Pago");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Retpago", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Retpago");
                }
                if (insDB.ExistsValueOnMultifile("dbo.CxP", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("CxP");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Compra", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Compra");
                }
                if (insDB.ExistsValueOnMultifile("dbo.ARCV", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("ARCV");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Eliminar")]
        LibResponse ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.Delete(IList<Proveedor> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ProveedorDEL"), ParametrosClave(CurrentRecord, true, true));
                if (vResult.Success && CurrentRecord.AccesoCaracteristicaDeContabilidadActiva && CurrentRecord.UsaAuxiliares) {
                    vResult.Success = vResult.Success && insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName("dbo", "AuxiliarDELFromProveedor"), ParametrosClaveAuxiliar(CurrentRecord, false, false));
                }
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Proveedor> ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Proveedor> vResult = new List<Proveedor>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Proveedor>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Proveedor>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
        LibResponse ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.Insert(IList<Proveedor> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {

                    LibDatabase insDb = new LibDatabase();
                    CurrentRecord.Consecutivo = insDb.NextLngConsecutive(DbSchema + ".Proveedor", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ProveedorINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    if (vResult.Success && CurrentRecord.AccesoCaracteristicaDeContabilidadActiva && CurrentRecord.UsaAuxiliares) {
                        vResult.Success = vResult.Success && insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName("dbo", "AuxiliarINSFromProveedor"), ParametrosActualizacionAuxiliar(CurrentRecord, eAccionSR.Insertar));
                    }
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoCodigoProveedor")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".Proveedor", "CodigoProveedor", valParameters, true, 10), "CodigoProveedor");
                    }
                    if (valProcessMessage == "ProximoConsecutivo") {
                        vResult = LibXml.ToXElement(LibXml.ValueToXmlDocument(insDb.NextLngConsecutive("Adm.Proveedor", "Consecutivo", valParameters.ToString()), "Consecutivo"));
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Modificar")]
        LibResponse ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.Update(IList<Proveedor> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ProveedorUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    if (vResult.Success && CurrentRecord.AccesoCaracteristicaDeContabilidadActiva && CurrentRecord.UsaAuxiliares) {
                        vResult.Success = vResult.Success && insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName("dbo", "AuxiliarUPDFromProveedor"), ParametrosActualizacionAuxiliar(CurrentRecord, eAccionSR.Modificar));
                    }
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.ValidateAll(IList<Proveedor> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Proveedor vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.SpecializedUpdate(IList<Proveedor> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Proveedor>, IList<Proveedor>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoProveedor);
            vResult = IsValidCodigoProveedor(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoProveedor) && vResult;
            vResult = IsValidNombreProveedor(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NombreProveedor) && vResult;
            vResult = IsValidNumeroRIF(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.NumeroRIF) && vResult;
            vResult = IsValidCodigoRetencionUsual(valAction, CurrentRecord.CodigoRetencionUsual) && vResult;
            vResult = IsValidTipodeProveedor(valAction, CurrentRecord.TipodeProveedor) && vResult;
            vResult = IsValidCuentaContableCxP(valAction, CurrentRecord.CuentaContableCxP) && vResult;
            vResult = IsValidCuentaContableGastos(valAction, CurrentRecord.CuentaContableGastos) && vResult;
            vResult = IsValidCuentaContableAnticipo(valAction, CurrentRecord.CuentaContableAnticipo) && vResult;
			outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valCodigoProveedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCompania <= 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoProveedor(eAccionSR valAction, int valConsecutivoCompania, string valCodigoProveedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoProveedor = LibString.Trim(valCodigoProveedor);
            if (LibString.IsNullOrEmpty(valCodigoProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valCodigoProveedor)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código", valCodigoProveedor));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidNombreProveedor(eAccionSR valAction, int valConsecutivoCompania, string valNombreProveedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNombreProveedor = LibString.Trim(valNombreProveedor);
            if (LibString.IsNullOrEmpty(valNombreProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre Proveedor"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar || valAction == eAccionSR.Modificar) {
                Proveedor vRecBusqueda = new Proveedor();
                vRecBusqueda.NombreProveedor = valNombreProveedor;
                vRecBusqueda.ConsecutivoCompania = valConsecutivoCompania;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda, "NombreProveedor", valAction)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Nombre Proveedor", valNombreProveedor));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidNumeroRIF(eAccionSR valAction, int valConsecutivoCompania, string valNumeroRIF){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroRIF = LibString.Trim(valNumeroRIF);
            if (LibString.IsNullOrEmpty(valNumeroRIF, true)) {
                BuildValidationInfo(MsgRequiredField("N° R.I.F."));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Proveedor vRecBusqueda = new Proveedor();
                vRecBusqueda.NumeroRIF = valNumeroRIF;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda, "NumeroRIF", valAction)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("N° R.I.F.", valNumeroRIF));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoRetencionUsual(eAccionSR valAction, string valCodigoRetencionUsual){
            bool vResult = true;
            //if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
            //   return true;
            //}
            //valCodigoRetencionUsual = LibString.Trim(valCodigoRetencionUsual);
            //if (LibString.IsNullOrEmpty(valCodigoRetencionUsual, true)) {
            //   BuildValidationInfo(MsgRequiredField("Codigo Retención Usual"));
            //   vResult = false;
            //}
            return vResult;
        }

        private bool IsValidTipodeProveedor(eAccionSR valAction, string valTipodeProveedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valTipodeProveedor = LibString.Trim(valTipodeProveedor);
            if (LibString.IsNullOrEmpty(valTipodeProveedor , true)) {
                BuildValidationInfo(MsgRequiredField("Tipo de Proveedor"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.TipoProveedor", "nombre", insDb.InsSql.ToSqlValue(valTipodeProveedor), true)) {
                    BuildValidationInfo("El valor asignado al campo Tipode Proveedor no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaContableCxP(eAccionSR valAction, string valCuentaContableCxP){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCuentaContableCxP = LibString.Trim(valCuentaContableCxP);
            if (!LibString.IsNullOrEmpty(valCuentaContableCxP , true)) {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("dbo.Cuenta", "codigo", insDb.InsSql.ToSqlValue(valCuentaContableCxP), true)) {
                    BuildValidationInfo("El valor asignado al campo CxP Proveedor no existe, escoja nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCuentaContableGastos(eAccionSR valAction, string valCuentaContableGastos) {
           bool vResult = true;
           if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
              return true;
           }
           valCuentaContableGastos = LibString.Trim(valCuentaContableGastos);
           if (!LibString.IsNullOrEmpty(valCuentaContableGastos, true)) {
              LibDatabase insDb = new LibDatabase();
              if (!insDb.ExistsValue("dbo.Cuenta", "codigo", insDb.InsSql.ToSqlValue(valCuentaContableGastos), true)) {
                 BuildValidationInfo("El valor asignado al campo Gastos no existe, escoja nuevamente.");
                 vResult = false;
              }
           }
           return vResult;
        }

        private bool IsValidCuentaContableAnticipo(eAccionSR valAction, string valCuentaContableAnticipo) {
           bool vResult = true;
           if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
              return true;
           }
           valCuentaContableAnticipo = LibString.Trim(valCuentaContableAnticipo);
           if (!LibString.IsNullOrEmpty(valCuentaContableAnticipo, true)) {
              LibDatabase insDb = new LibDatabase();
              if (!insDb.ExistsValue("dbo.Cuenta", "codigo", insDb.InsSql.ToSqlValue(valCuentaContableAnticipo), true)) {
                 BuildValidationInfo("El valor asignado al campo Anticipo no existe, escoga nuevamente.");
                 vResult = false;
              }
           }
           return vResult;
        }

        private bool IsValidBeneficiario(eAccionSR valAction, int valConsecutivoCompania, string valBeneficiario, bool valUsaImpCheque){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valBeneficiario = LibString.Trim(valBeneficiario);
            if (LibString.IsNullOrEmpty(valBeneficiario, true) && valUsaImpCheque) {
                BuildValidationInfo(MsgRequiredField("Beneficiario"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoProveedor) {
            bool vResult = false;
            Proveedor vRecordBusqueda = new Proveedor();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoProveedor = valCodigoProveedor;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Proveedor", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Proveedor valRecordBusqueda, string valCampo, eAccionSR valAction) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".Proveedor", "ConsecutivoCompania", ParametrosClaveOtros(valRecordBusqueda, valCampo, valAction == eAccionSR.Modificar, false));
            insDb.Dispose();
            return vResult;
        }
        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Importar")]
        LibXmlResult ILibDataImport.Import(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
            throw new ProgrammerMissingCodeException("PROGRAMADOR: El codigo generado bajo el atributo IMPEXP del record, es solo referencial. DEBE AJUSTARLO ya que el Narrador actualmente desconoce la estructura de su archivo de importacion!!!!");
            try {
                string vMessage = "";
                int vIndex = 0;
                LibXmlResult vResult = new LibXmlResult();
                vResult.AddTitle("Importación Proveedor");
                List<Proveedor> vList = ParseToListEntity(refRecord);
                LibDatabase insDb = new LibDatabase();
                int vTotal = vList.Count();
                foreach (Proveedor item in vList) {
                    try {
                        vMessage = string.Format("Insertando {0:n0} de {1:n0}", vIndex, vTotal);
                        insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ProveedorINST"), ParametrosActualizacion(item, eAccionSR.Insertar));
                    } catch (System.Data.SqlClient.SqlException vEx) {
                        if (LibExceptionMng.IsPrimaryKeyViolation(vEx)) {
                            vResult.AddDetailWithAttribute(item.CodigoProveedor, "Ya existe", eXmlResultType.Error);
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

        private List<Proveedor> ParseToListEntity(XmlReader valXmlEntity) {
            List<Proveedor> vResult = new List<Proveedor>();
            XDocument xDoc = XDocument.Load(valXmlEntity);
            var vEntity = from vRecord in xDoc.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Proveedor vRecord = new Proveedor();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoProveedor"), null))) {
                    vRecord.CodigoProveedor = vItem.Element("CodigoProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreProveedor"), null))) {
                    vRecord.NombreProveedor = vItem.Element("NombreProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Contacto"), null))) {
                    vRecord.Contacto = vItem.Element("Contacto").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroRIF"), null))) {
                    vRecord.NumeroRIF = vItem.Element("NumeroRIF").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroNIT"), null))) {
                    vRecord.NumeroNIT = vItem.Element("NumeroNIT").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDePersona"), null))) {
                    vRecord.TipoDePersona = vItem.Element("TipoDePersona").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoRetencionUsual"), null))) {
                    vRecord.CodigoRetencionUsual = vItem.Element("CodigoRetencionUsual").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Telefonos"), null))) {
                    vRecord.Telefonos = vItem.Element("Telefonos").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Direccion"), null))) {
                    vRecord.Direccion = vItem.Element("Direccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fax"), null))) {
                    vRecord.Fax = vItem.Element("Fax").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Email"), null))) {
                    vRecord.Email = vItem.Element("Email").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipodeProveedor"), null))) {
                    vRecord.TipodeProveedor = vItem.Element("TipodeProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeProveedorDeLibrosFiscales"), null))) {
                    vRecord.TipoDeProveedorDeLibrosFiscales = vItem.Element("TipoDeProveedorDeLibrosFiscales").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeRetencionIVA"), null))) {
                   vRecord.PorcentajeRetencionIVA = LibConvert.ToDec(vItem.Element("PorcentajeRetencionIVA"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableCxP"), null))) {
                    vRecord.CuentaContableCxP = vItem.Element("CuentaContableCxP").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableGastos"), null))) {
                    vRecord.CuentaContableGastos = vItem.Element("CuentaContableGastos").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CuentaContableAnticipo"), null))) {
                    vRecord.CuentaContableAnticipo = vItem.Element("CuentaContableAnticipo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null))) {
                    vRecord.CodigoLote = vItem.Element("CodigoLote").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Beneficiario"), null))) {
                    vRecord.Beneficiario = vItem.Element("Beneficiario").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("UsarBeneficiarioImpCheq"), null))) {
                    vRecord.UsarBeneficiarioImpCheq = vItem.Element("UsarBeneficiarioImpCheq").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDocumentoIdentificacion"), null))) {
                    vRecord.TipoDocumentoIdentificacion = vItem.Element("TipoDocumentoIdentificacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("EsAgenteDeRetencionIva"), null))) {
                    vRecord.EsAgenteDeRetencionIva = vItem.Element("EsAgenteDeRetencionIva").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null))) {
                    vRecord.Nombre = vItem.Element("Nombre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ApellidoPaterno"), null))) {
                    vRecord.ApellidoPaterno = vItem.Element("ApellidoPaterno").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ApellidoMaterno"), null))) {
                    vRecord.ApellidoMaterno = vItem.Element("ApellidoMaterno").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroCuentaBancaria"), null))) {
                    vRecord.NumeroCuentaBancaria = vItem.Element("NumeroCuentaBancaria").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoContribuyente"), null))) {
                    vRecord.CodigoContribuyente = vItem.Element("CodigoContribuyente").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }

        DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion //Metodos Generados


    } //End of class clsProveedorDat

} //End of namespace Galac.Adm.Dal.GestionCompras

