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
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Dal.CajaChica {
    public class clsProveedorDat: LibData, ILibDataComponentWithSearch<IList<Proveedor>, IList<Proveedor>> {
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
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Proveedor valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("CodigoProveedor", valRecord.CodigoProveedor, 10);
            vParams.AddInString("NombreProveedor", valRecord.NombreProveedor, 60);
            vParams.AddInString("Contacto", valRecord.Contacto, 35);
            vParams.AddInString("NumeroRIF", valRecord.NumeroRIF, 20);
            vParams.AddInString("NumeroNIT", valRecord.NumeroNIT, 12);
            vParams.AddInEnum("TipoDePersona", valRecord.TipoDePersonaAsDB);
            vParams.AddInString("CodigoRetencionUsual", valRecord.CodigoRetencionUsual, 6);
            vParams.AddInString("Telefonos", valRecord.Telefonos, 40);
            vParams.AddInString("Direccion", valRecord.Direccion, 255);
            vParams.AddInString("Fax", valRecord.Fax, 25);
            vParams.AddInString("Email", valRecord.Email, 40);
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
            vParams.AddInString("CodigoContribuyente", valRecord.CodigoContribuyente, 20);
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

        private StringBuilder ParametrosProximoConsecutivo(Proveedor valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
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
                if (insDB.ExistsValueOnMultifile("dbo.CxP", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Cx P");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Pago", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Pago");
                }
                if (insDB.ExistsValueOnMultifile("dbo.RetPago", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Ret Pago");
                }
                if (insDB.ExistsValueOnMultifile("dbo.Compra", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Compra");
                }
                if (insDB.ExistsValueOnMultifile("Adm.Anticipo", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Anticipo");
                }
                if (insDB.ExistsValueOnMultifile("Adm.DetalleDeRendicion", "CodigoProveedor", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.CodigoProveedor), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Detalle De Rendicion");
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
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Proveedor.Insertar")]
        LibResponse ILibDataComponent<IList<Proveedor>, IList<Proveedor>>.Insert(IList<Proveedor> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "ProveedorINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
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
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive("dbo.Proveedor", "CodigoProveedor", valParameters, true, 10), "CodigoProveedor");
                    }
                    break;
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                default: throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            vResult = IsValidBeneficiario(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Beneficiario) && vResult;
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
            } else if (valAction == eAccionSR.Insertar) {
                Proveedor vRecBusqueda = new Proveedor();
                vRecBusqueda.NombreProveedor = valNombreProveedor;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
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
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("N° R.I.F.", valNumeroRIF));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoRetencionUsual(eAccionSR valAction, string valCodigoRetencionUsual){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoRetencionUsual = LibString.Trim(valCodigoRetencionUsual);
            if (LibString.IsNullOrEmpty(valCodigoRetencionUsual, true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Retencion Usual"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidBeneficiario(eAccionSR valAction, int valConsecutivoCompania, string valBeneficiario){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valBeneficiario = LibString.Trim(valBeneficiario);
            if (LibString.IsNullOrEmpty(valBeneficiario, true)) {
                BuildValidationInfo(MsgRequiredField("Beneficiario"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Proveedor vRecBusqueda = new Proveedor();
                vRecBusqueda.Beneficiario = valBeneficiario;
                if (KeyExists(valConsecutivoCompania, vRecBusqueda)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Beneficiario", valBeneficiario));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valCodigoProveedor) {
            bool vResult = false;
            Proveedor vRecordBusqueda = new Proveedor();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.CodigoProveedor = valCodigoProveedor;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord("dbo.Proveedor", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Proveedor valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord("dbo.Proveedor", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
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
        #endregion //Metodos Generados


    } //End of class clsProveedorDat

} //End of namespace Galac.Dbo.Dal.CajaChica

