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
using Galac.Adm.Ccl.CAnticipo;
using Galac.Adm.Ccl.Venta;

namespace Galac.Adm.Dal.CAnticipo {
    public class clsAnticipoDat: LibData, ILibDataComponentWithSearch<IList<Anticipo>, IList<Anticipo>>, ILibDataRpt {
        #region Variables
        Anticipo _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private Anticipo CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsAnticipoDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(Anticipo valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoAnticipo", valRecord.ConsecutivoAnticipo);
            vParams.AddInEnum("Status", valRecord.StatusAsDB);
            vParams.AddInEnum("Tipo", valRecord.TipoAsDB);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInString("Numero", valRecord.Numero, 20);
            vParams.AddInString("CodigoCliente", valRecord.CodigoCliente, 10);
            vParams.AddInString("CodigoProveedor", valRecord.CodigoProveedor, 10);
            vParams.AddInString("Moneda", valRecord.Moneda, 10);
            vParams.AddInDecimal("Cambio", valRecord.Cambio, 2);
            vParams.AddInBoolean("GeneraMovBancario", valRecord.GeneraMovBancarioAsBool);
            vParams.AddInString("CodigoCuentaBancaria", valRecord.CodigoCuentaBancaria, 5);
            vParams.AddInString("CodigoConceptoBancario", valRecord.CodigoConceptoBancario, 8);
            vParams.AddInBoolean("GeneraImpuestoBancario", valRecord.GeneraImpuestoBancarioAsBool);
            vParams.AddInDateTime("FechaAnulacion", valRecord.FechaAnulacion);
            vParams.AddInDateTime("FechaCancelacion", valRecord.FechaCancelacion);
            vParams.AddInDateTime("FechaDevolucion", valRecord.FechaDevolucion);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 255);
            vParams.AddInDecimal("MontoTotal", valRecord.MontoTotal, 2);
            vParams.AddInDecimal("MontoUsado", valRecord.MontoUsado, 2);
            vParams.AddInDecimal("MontoDevuelto", valRecord.MontoDevuelto, 2);
            vParams.AddInDecimal("MontoDiferenciaEnDevolucion", valRecord.MontoDiferenciaEnDevolucion, 2);
            vParams.AddInBoolean("DiferenciaEsIDB", valRecord.DiferenciaEsIDBAsBool);
            vParams.AddInBoolean("EsUnaDevolucion", valRecord.EsUnaDevolucionAsBool);
            vParams.AddInInteger("NumeroDelAnticipoDevuelto", valRecord.NumeroDelAnticipoDevuelto);
            vParams.AddInString("NumeroCheque", valRecord.NumeroCheque, 15);
            vParams.AddInBoolean("AsociarAnticipoACotiz", valRecord.AsociarAnticipoACotizAsBool);
            vParams.AddInString("NumeroCotizacion", valRecord.NumeroCotizacion, 11);
            vParams.AddInInteger("ConsecutivoRendicion", valRecord.ConsecutivoRendicion);
            vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
            vParams.AddInString("NombreBeneficiario", valRecord.NombreBeneficiario, 60);
            vParams.AddInInteger("ConsecutivoRendicion", valRecord.ConsecutivoRendicion);
            vParams.AddInString("CodigoBeneficiario", valRecord.CodigoBeneficiario, 10);
            vParams.AddInBoolean("AsociarAnticipoACaja", valRecord.AsociarAnticipoACajaAsBool);
            vParams.AddInInteger("ConsecutivoCaja", valRecord.ConsecutivoCaja);
            vParams.AddInString("NumeroDeCobranzaAsociado", valRecord.NumeroDeCobranzaAsociado, 12);
            vParams.AddInEnum("GeneradoPor", valRecord.GeneradoPorAsDB);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(Anticipo valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoAnticipo", valRecord.ConsecutivoAnticipo);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(Anticipo valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<Anticipo>, IList<Anticipo>>

        LibResponse ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.CanBeChoosen(IList<Anticipo> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            Anticipo vRecord = refRecord[0];
            StringBuilder vSbInfo = new StringBuilder();
            string vErrMsg = "";
            LibDatabase insDB = new LibDatabase();
            if (valAction == eAccionSR.Eliminar) {
                if (insDB.ExistsValueOnMultifile("dbo.AnticipoPagado", "ConsecutivoAnticipoUsado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.ConsecutivoAnticipo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Anticipo Pagado");
                }
                if (insDB.ExistsValueOnMultifile("dbo.AnticipoPagado", "NumeroAnticipo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Numero), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Anticipo Pagado");
                }
                if (insDB.ExistsValueOnMultifile("Adm.AnticipoCobrado", "ConsecutivoAnticipoUsado", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.ConsecutivoAnticipo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Anticipo Cobrado");
                }
                if (insDB.ExistsValueOnMultifile("Adm.AnticipoCobrado", "NumeroAnticipo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Numero), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
                    vSbInfo.AppendLine("Anticipo Cobrado");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Anticipo.Eliminar")]
        LibResponse ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.Delete(IList<Anticipo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AnticipoDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<Anticipo> ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<Anticipo> vResult = new List<Anticipo>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<Anticipo>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                case eProcessMessageType.Query:
                    vResult = insDb.LoadData<Anticipo>(valProcessMessage, CmdTimeOut, valParameters);
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Anticipo.Insertar")]
        LibResponse ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.Insert(IList<Anticipo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeInsert()) {
                if (Validate(eAccionSR.Insertar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AnticipoINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.Message:
                    if (valProcessMessage.Equals("ProximoConsecutivoAnticipo")) {
                        vResult = LibXml.ValueToXElement(insDb.NextLngConsecutive(DbSchema + ".Anticipo", "ConsecutivoAnticipo", valParameters), "ConsecutivoAnticipo");
                    }
                    else if (valProcessMessage.Equals("ProximoNumero")) {
                        vResult = LibXml.ValueToXElement(insDb.NextStrConsecutive(DbSchema + ".Anticipo", "Numero", valParameters, true, 20), "Numero");
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Anticipo.Modificar")]
        LibResponse ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.Update(IList<Anticipo> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
            CurrentRecord = refRecord[0];
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "AnticipoUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.ValidateAll(IList<Anticipo> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (Anticipo vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        // aqui programar lo relacionado con la llamada al procedimiento 
        
        
        LibResponse ILibDataComponent<IList<Anticipo>, IList<Anticipo>>.SpecializedUpdate(IList<Anticipo> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }

        public bool GenerarAnticipoCobradoActualizado(int valConsecutivoCompania, string valNumeroDeCobranza, string valcodigocliente,
                                              decimal valMontoCobranza, string valNumeroAnticipo, int valSecuencial,
                                              string valSimboloMoneda ) {
            
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroCobranza", valNumeroDeCobranza, 20);
            vParams.AddInString("CodigoCliente", valcodigocliente, 10);
            vParams.AddInDecimal("MontoCobranza", valMontoCobranza, 2);
            vParams.AddInString("NumeroAnticipo", valNumeroAnticipo, 10);
            vParams.AddInString("SimboloMoneda", valSimboloMoneda, 4);
            vParams.AddInInteger("Secuencial", valSecuencial);
            StringBuilder vParamsText = vParams.Get();
            LibDatabase insDb = new LibDatabase();
            //LibDataScope insDb = new LibDataScope();
            vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "GenerarAnticipoCobrado"), vParamsText);
            return vResult;

        }
       

        #endregion //ILibDataComponent<IList<Anticipo>, IList<Anticipo>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoAnticipo);
            vResult = IsValidConsecutivoAnticipo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoAnticipo) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
            vResult = IsValidNumero(valAction, CurrentRecord.Numero,CurrentRecord.ConsecutivoCompania,CurrentRecord.ConsecutivoAnticipo ) && vResult;
            vResult = IsValidCodigoCliente(valAction, CurrentRecord.CodigoCliente) && vResult;
            vResult = IsValidCodigoProveedor(valAction, CurrentRecord.CodigoProveedor) && vResult;
            vResult = IsValidMoneda(valAction, CurrentRecord.Moneda) && vResult;
            vResult = IsValidCodigoCuentaBancaria(valAction, CurrentRecord.CodigoCuentaBancaria) && vResult;
            vResult = IsValidCodigoConceptoBancario(valAction, CurrentRecord.CodigoConceptoBancario) && vResult;
            vResult = IsValidFechaAnulacion(valAction, CurrentRecord.FechaAnulacion) && vResult;
            vResult = IsValidFechaCancelacion(valAction, CurrentRecord.FechaCancelacion) && vResult;
            vResult = IsValidFechaDevolucion(valAction, CurrentRecord.FechaDevolucion) && vResult;
            vResult = IsValidNumeroCheque(valAction, CurrentRecord.NumeroCheque) && vResult;
            vResult = IsValidNumeroCotizacion(valAction, CurrentRecord.NumeroCotizacion) && vResult;
            vResult = IsValidConsecutivoRendicion(valAction, CurrentRecord.ConsecutivoRendicion) && vResult;
            vResult = IsValidCodigoMoneda(valAction, CurrentRecord.CodigoMoneda) && vResult;
            vResult = IsValidConsecutivoCaja(valAction, CurrentRecord.ConsecutivoCaja) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoAnticipo){
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

        private bool IsValidConsecutivoAnticipo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoAnticipo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoAnticipo == 0) {
                BuildValidationInfo(MsgRequiredField("Consecutivo Anticipo"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                Anticipo vRecBusqueda = new Anticipo();
                vRecBusqueda.ConsecutivoAnticipo = valConsecutivoAnticipo;
                if (KeyExists(valConsecutivoCompania, valConsecutivoAnticipo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Consecutivo Anticipo", valConsecutivoAnticipo));
                    vResult = false;
                }
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

        private bool IsValidNumero(eAccionSR valAction, string valNumero, int valConsecutivoCompania, int valConsecutivoAnticipo){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumero = LibString.Trim(valNumero);
            if (LibString.IsNullOrEmpty(valNumero, true)) {
                BuildValidationInfo(MsgRequiredField("Numero"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivoAnticipo)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Numero", valNumero));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoCliente(eAccionSR valAction, string valCodigoCliente){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoCliente = LibString.Trim(valCodigoCliente);
            if (LibString.IsNullOrEmpty(valCodigoCliente, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Proveedor"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoProveedor(eAccionSR valAction, string valCodigoProveedor){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoProveedor = LibString.Trim(valCodigoProveedor);
            if (LibString.IsNullOrEmpty(valCodigoProveedor, true)) {
                BuildValidationInfo(MsgRequiredField("Código del Proveedor"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidMoneda(eAccionSR valAction, string valMoneda){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valMoneda = LibString.Trim(valMoneda);
            if (LibString.IsNullOrEmpty(valMoneda, true)) {
                BuildValidationInfo(MsgRequiredField("Nombre de la Moneda"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoCuentaBancaria(eAccionSR valAction, string valCodigoCuentaBancaria){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoCuentaBancaria = LibString.Trim(valCodigoCuentaBancaria);
            if (LibString.IsNullOrEmpty(valCodigoCuentaBancaria, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Bancaria"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoConceptoBancario(eAccionSR valAction, string valCodigoConceptoBancario){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoConceptoBancario = LibString.Trim(valCodigoConceptoBancario);
            if (LibString.IsNullOrEmpty(valCodigoConceptoBancario, true)) {
                BuildValidationInfo(MsgRequiredField("Código Concepto"));
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

        private bool IsValidFechaCancelacion(eAccionSR valAction, DateTime valFechaCancelacion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaCancelacion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidFechaDevolucion(eAccionSR valAction, DateTime valFechaDevolucion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFechaDevolucion, false, valAction)) {
                BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNumeroCheque(eAccionSR valAction, string valNumeroCheque){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroCheque = LibString.Trim(valNumeroCheque);
            if (LibString.IsNullOrEmpty(valNumeroCheque, true)) {
                BuildValidationInfo(MsgRequiredField("Número Cheque"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidNumeroCotizacion(eAccionSR valAction, string valNumeroCotizacion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valNumeroCotizacion = LibString.Trim(valNumeroCotizacion);
            if (LibString.IsNullOrEmpty(valNumeroCotizacion , true)) {
                BuildValidationInfo(MsgRequiredField("Número de Cot. Asociada"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Cotizacion", "Numero", insDb.InsSql.ToSqlValue(valNumeroCotizacion), true)) {
                    BuildValidationInfo("El valor asignado al campo Número de Cot. Asociada no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidConsecutivoRendicion(eAccionSR valAction, int valConsecutivoRendicion){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoRendicion == 0) {
                BuildValidationInfo(MsgRequiredField("Rendicion"));
                vResult = false;
            }
            return vResult;
        }

        private bool IsValidCodigoMoneda(eAccionSR valAction, string valCodigoMoneda){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoMoneda = LibString.Trim(valCodigoMoneda);
            if (LibString.IsNullOrEmpty(valCodigoMoneda , true)) {
                BuildValidationInfo(MsgRequiredField("Codigo Moneda"));
                vResult = false;
            } else {
                LibDatabase insDb = new LibDatabase();
                if (!insDb.ExistsValue("Comun.Moneda", "Codigo", insDb.InsSql.ToSqlValue(valCodigoMoneda), true)) {
                    BuildValidationInfo("El valor asignado al campo Codigo Moneda no existe, escoga nuevamente.");
                    vResult = false;
                }
            }
            return vResult;
        }

        //private bool IsValidConsecutivoRendicion(eAccionSR valAction, int valConsecutivoRendicion){
        //    bool vResult = true;
        //    if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
        //        return true;
        //    }
        //    if (valConsecutivoRendicion == 0) {
        //        BuildValidationInfo(MsgRequiredField("Consecutivo Rendicion"));
        //        vResult = false;
        //    }
        //    return vResult;
        //}

        private bool IsValidConsecutivoCaja(eAccionSR valAction, int valConsecutivoCaja){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            if (valConsecutivoCaja == 0) {
                BuildValidationInfo(MsgRequiredField("Caja"));
                vResult = false;
            }
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoAnticipo) {
            bool vResult = false;
            Anticipo vRecordBusqueda = new Anticipo();
            vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            vRecordBusqueda.ConsecutivoAnticipo = valConsecutivoAnticipo;
            LibDatabase insDb = new LibDatabase();
            vResult = insDb.ExistsRecord(DbSchema + ".Anticipo", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            insDb.Dispose();
            return vResult;
        }

        private bool KeyExists(int valConsecutivoCompania, Anticipo valRecordBusqueda) {
            bool vResult = false;
            valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
            LibDatabase insDb = new LibDatabase();
            //Programador ajuste el codigo necesario para busqueda de claves unicas;
            vResult = insDb.ExistsRecord(DbSchema + ".Anticipo", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
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


    } //End of class clsAnticipoDat

} //End of namespace Galac.Adm.Dal.CAnticipo

