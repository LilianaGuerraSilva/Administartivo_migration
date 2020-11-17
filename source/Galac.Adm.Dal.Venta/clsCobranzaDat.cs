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
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.Venta {
    public class clsCobranzaDat: LibData, ILibDataComponentWithSearch<IList<Cobranza>, IList<Cobranza>>, ILibDataRpt {
        #region Variables
        Cobranza _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Cobranza CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsCobranzaDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Cobranza valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 10);
            vParams.AddInEnum("StatusCobranza", valRecord.StatusCobranzaAsDB);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInDateTime("FechaAnulacion", valRecord.FechaAnulacion);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInString("CodigoCobrador", valRecord.CodigoCobrador, 5);
            vParams.AddInDecimal("TotalDocumentos", valRecord.TotalDocumentos, 2);
            vParams.AddInDecimal("RetencionIslr", valRecord.RetencionIslr, 2);
            vParams.AddInDecimal("TotalCobrado", valRecord.TotalCobrado, 2);
            vParams.AddInDecimal("CobradoEfectivo", valRecord.CobradoEfectivo, 2);
            vParams.AddInDecimal("CobradoCheque", valRecord.CobradoCheque, 2);
            vParams.AddInString("NumerodelCheque", valRecord.NumerodelCheque, 10);
            vParams.AddInDecimal("CobradoTarjeta", valRecord.CobradoTarjeta, 2);
            vParams.AddInEnum("CualTarjeta", valRecord.CualTarjetaAsDB);
            vParams.AddInString("NroDeLaTarjeta", valRecord.NroDeLaTarjeta, 20);
            vParams.AddInEnum("Origen", valRecord.OrigenAsDB);
            vParams.AddInDecimal("TotalOtros", valRecord.TotalOtros, 2);
            vParams.AddInString("NombreBanco", valRecord.NombreBanco, 20);
            vParams.AddInString("CodigoCuentaBancaria", valRecord.CodigoCuentaBancaria, 5);
            vParams.AddInString("CodigoConcepto", valRecord.CodigoConcepto, 8);
            vParams.AddInString("Moneda", valRecord.Moneda, 80);
            vParams.AddInDecimal("CambioAbolivares", valRecord.CambioAbolivares, 2);
            vParams.AddInDecimal("RetencionIva", valRecord.RetencionIva, 2);
            vParams.AddInString("NroComprobanteRetIva", valRecord.NroComprobanteRetIva, 20);
            vParams.AddInEnum("StatusRetencionIVA", valRecord.StatusRetencionIVAAsDB);
            vParams.AddInBoolean("GeneraMovBancario", valRecord.GeneraMovBancarioAsBool);
            vParams.AddInDecimal("CobradoAnticipo", valRecord.CobradoAnticipo, 2);
            vParams.AddInDecimal("Vuelto", valRecord.Vuelto, 2);
            vParams.AddInDecimal("DescProntoPago", valRecord.DescProntoPago, 2);
            vParams.AddInDecimal("DescProntoPagoPorc", valRecord.DescProntoPagoPorc, 2);
            vParams.AddInDecimal("ComisionVendedor", valRecord.ComisionVendedor, 2);
            vParams.AddInBoolean("AplicaCreditoBancario", valRecord.AplicaCreditoBancarioAsBool);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInInteger("NumeroDeComprobanteISLR", valRecord.NumeroDeComprobanteISLR);
            vParams.AddInEnum("TipoDeDocumento", valRecord.TipoDeDocumentoAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Cobranza valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInString("Numero", valRecord.Numero, 10);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Cobranza valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Cobranza>, IList<Cobranza>>

        LibResponse ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.CanBeChoosen(IList<Cobranza> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Cobranza vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobranza.Eliminar")]
        LibResponse ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.Delete(IList<Cobranza> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CobranzaDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Cobranza> ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Cobranza> vResult = new List<Cobranza>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Cobranza>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Cobranza>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobranza.Insertar")]
        LibResponse ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.Insert(IList<Cobranza> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CobranzaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoNumero")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".Cobranza", "Numero", valParameters, true, 10), "Numero");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Cobranza.Modificar")]
        LibResponse ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.Update(IList<Cobranza> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CobranzaUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.ValidateAll(IList<Cobranza> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Cobranza vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<Cobranza>, IList<Cobranza>>.SpecializedUpdate(IList<Cobranza> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<Cobranza>, IList<Cobranza>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero);
            vResult = IsValidNumero(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Numero) && vResult;
            vResult = IsValidStatusCobranza(valAction, CurrentRecord.StatusCobranzaAsEnum) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidFechaAnulacion(valAction, CurrentRecord.FechaAnulacion) && vResult;
            //vResult = IsValidTipoDeDocumento(valAction, CurrentRecord.TipoDeDocumentoAsEnum) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valNumero){
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

        private bool IsValidNumero(eAccionSR valAction, int valConsecutivoCompania, string valNumero){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Número Cobranza"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Cobranza vRecBusqueda = new Cobranza();
                vRecBusqueda.Numero = valNumero;
                //if (KeyExists(valConsecutivoCompania, valNumerovRecBusqueda)) {
                //    BuildValidationInfo(MsgFieldValueAlreadyExist("Número Cobranza", valNumero));
                //    vResult = false;
                //}
            }
            return vResult;
        }

        private bool IsValidStatusCobranza(eAccionSR valAction, eStatusCobranza valStatusCobranza){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, DateTime valFecha){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFecha, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaAnulacion(eAccionSR valAction, DateTime valFechaAnulacion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaAnulacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidTipoDeDocumento(eAccionSR valAction, eTipoDeTransaccion valTipoDeDocumento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, string valNumero) {
            bool vResult = false;
            Cobranza vRecordBusqueda = new Cobranza();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.Numero = valNumero;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Cobranza", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Cobranza valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".Cobranza", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
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

        #region //Miembros de ILibDataRpt

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion ////Miembros de ILibDataRpt
        #endregion //Metodos Generados


    } //End of class clsCobranzaDat

} //End of namespace Galac.Adm.Dal.Venta

