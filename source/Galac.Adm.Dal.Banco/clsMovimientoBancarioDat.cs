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
using Galac.Adm.Ccl.Banco;
using System.ComponentModel.Composition;
using System.Transactions;

namespace Galac.Adm.Dal.Banco { 
    public class clsMovimientoBancarioDat: LibData, ILibDataComponentWithSearch<IList<MovimientoBancario>, IList<MovimientoBancario>> {
        #region Variables
        MovimientoBancario _CurrentRecord;
        int valConsecutivoCompania;
        int valConsecutivoMovimiento;
        string valCodigoCtaBancaria; 
        string valCodigoConcepto; 
        DateTime valFecha;         
        
        #endregion //Variables
        #region Propiedades
        private MovimientoBancario CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsMovimientoBancarioDat() {
            DbSchema = "Saw";
        }
        #endregion //Constructores
        #region Metodos Generados

        private StringBuilder ParametrosActualizacion(MovimientoBancario valRecord, eAccionSR valAction) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoMovimiento", valRecord.ConsecutivoMovimiento);
            vParams.AddInString("CodigoCtaBancaria", valRecord.CodigoCtaBancaria, 5);
            vParams.AddInString("CodigoConcepto", valRecord.CodigoConcepto, 8);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            vParams.AddInEnum("TipoConcepto", valRecord.TipoConceptoAsDB);
            vParams.AddInDecimal("Monto", valRecord.Monto, 2);
            vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 15);
            vParams.AddInString("Descripcion", valRecord.Descripcion, 255);
            vParams.AddInBoolean("GeneraImpuestoBancario", valRecord.GeneraImpuestoBancarioAsBool);
            vParams.AddInDecimal("AlicuotaImpBancario", valRecord.AlicuotaImpBancario, 2);
            vParams.AddInString("NroMovimientoRelacionado", valRecord.NroMovimientoRelacionado, 15);
            vParams.AddInEnum("GeneradoPor", valRecord.GeneradoPorAsDB);
            vParams.AddInDecimal("CambioABolivares", valRecord.CambioABolivares, 2);
            vParams.AddInBoolean("ImprimirCheque", valRecord.ImprimirChequeAsBool);
            vParams.AddInBoolean("ConciliadoSN", valRecord.ConciliadoSNAsBool);
            vParams.AddInString("NroConciliacion", valRecord.NroConciliacion, 9);
            vParams.AddInBoolean("GenerarAsientoDeRetiroEnCuenta", valRecord.GenerarAsientoDeRetiroEnCuentaAsBool);
            vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            if (valAction == eAccionSR.Modificar) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosClave(MovimientoBancario valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            if (valAddReturnParameter) {
                vParams.AddReturn();
            }
            //vParams.AddInDateFormat("DateFormat");
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoMovimiento", valRecord.ConsecutivoMovimiento);
            vParams.AddInString("CodigoCtaBancaria", valRecord.CodigoCtaBancaria, 5);
            vParams.AddInString("CodigoConcepto", valRecord.CodigoConcepto, 8);
            vParams.AddInDateTime("Fecha", valRecord.Fecha);
            if (valIncludeTimestamp) {
                vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
            }
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder ParametrosProximoConsecutivo(MovimientoBancario valRecord) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }
        #region Miembros de ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>

        LibResponse ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.CanBeChoosen(IList<MovimientoBancario> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();
            MovimientoBancario vRecord = refRecord[0];
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

        [PrincipalPermission(SecurityAction.Demand, Role = "Movimiento Bancario.Eliminar")]
        LibResponse ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.Delete(IList<MovimientoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
                LibDatabase insDb = new LibDatabase();
                vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "MovimientoBancarioDEL"), ParametrosClave(CurrentRecord, true, true));
                insDb.Dispose();
            } else {
                throw new GalacValidationException(vErrMsg);
            }
            return vResult;
        }

        IList<MovimientoBancario> ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<MovimientoBancario> vResult = new List<MovimientoBancario>();
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
                    vResult = insDb.LoadFromSp<MovimientoBancario>(valProcessMessage, valParameters, CmdTimeOut);
                    break;
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        } 
		       
        [PrincipalPermission(SecurityAction.Demand, Role = "Punto de Venta.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Factura.Insertar")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Movimiento Bancario.Insertar")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Transferencia entre Cuentas.Insertar")]
        LibResponse ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.Insert(IList<MovimientoBancario> refRecord) {
            LibResponse vResult = new LibResponse();                        
            string vErrMsg = "";
            CurrentRecord = refRecord[0];
            LibDataScope insDb = new LibDataScope();
            int consecutivo = insDb.NextLngConsecutive("dbo.MovimientoBancario", "ConsecutivoMovimiento", ParametrosProximoConsecutivo(CurrentRecord));
            foreach (MovimientoBancario mb in refRecord) {
                CurrentRecord = mb;
                if (ExecuteProcessBeforeInsert()) {
                    if (Validate(eAccionSR.Insertar, out vErrMsg)) {
						if (CurrentRecord.GeneradoPorAsEnum == eGeneradoPor.TransferenciaBancaria) {
							CurrentRecord.NroMovimientoRelacionado = LibString.S1StartsWithS2(CurrentRecord.Descripcion, "Impuesto") ? LibConvert.ToStr(consecutivo - 1) : CurrentRecord.NumeroDocumento;
						}
                        CurrentRecord.ConsecutivoMovimiento = consecutivo++;
                        vResult.Success = insDb.ExecSpNonQueryWithScope(insDb.ToSpName(DbSchema, "MovimientoBancarioINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
                        insDb.Dispose();
                    } else {
                        throw new GalacValidationException(vErrMsg);
                    }
                }
            }
            return vResult;
        }

        XElement ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                default: throw new NotImplementedException();
            }
            insDb.Dispose();
            return vResult;
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Movimiento Bancario.Modificar")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Movimiento Bancario.Anular")]
        LibResponse ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.Update(IList<MovimientoBancario> refRecord) {
            LibResponse vResult = new LibResponse();
            string vErrMsg ="";
			foreach (MovimientoBancario mb in refRecord) {
				CurrentRecord = mb;
            if (ExecuteProcessBeforeUpdate()) {
                if (Validate(eAccionSR.Modificar, out vErrMsg)) {
                    LibDatabase insDb = new LibDatabase();
                    vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "MovimientoBancarioUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
                    insDb.Dispose();
                } else {
                    throw new GalacValidationException(vErrMsg);
					}
                }
            }
            return vResult;
        }

        bool ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.ValidateAll(IList<MovimientoBancario> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            string vErroMessage = "";
            foreach (MovimientoBancario vItem in refRecords) {
                CurrentRecord = vItem;
                vResult = vResult && Validate(valAction, out vErroMessage);
                if (LibString.IsNullOrEmpty(vErroMessage, true)) {
                    refErrorMessage.AppendLine(vErroMessage);
                }
            }
            return vResult;
        }

        LibResponse ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>.SpecializedUpdate(IList<MovimientoBancario> refRecord, string valSpecializedAction) {
            throw new NotImplementedException();
        }
        #endregion //ILibDataComponent<IList<MovimientoBancario>, IList<MovimientoBancario>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;

             valConsecutivoCompania=CurrentRecord.ConsecutivoCompania;
            valConsecutivoMovimiento=CurrentRecord.ConsecutivoMovimiento;
            valCodigoCtaBancaria=CurrentRecord.CodigoCtaBancaria;
            valCodigoConcepto=CurrentRecord.CodigoConcepto;
            valFecha = CurrentRecord.Fecha;         
            
            ClearValidationInfo();
            vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoMovimiento, CurrentRecord.CodigoCtaBancaria, CurrentRecord.CodigoConcepto, CurrentRecord.Fecha);
            vResult = IsValidConsecutivoMovimiento(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.ConsecutivoMovimiento) && vResult;
            vResult = IsValidCodigoCtaBancaria(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoCtaBancaria) && vResult;
            vResult = IsValidCodigoConcepto(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoConcepto) && vResult;
            vResult = IsValidFecha(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Fecha) && vResult;
            outErrorMessage = Information.ToString();
            return vResult;
        }

        private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoMovimiento, string valCodigoCtaBancaria, string valCodigoConcepto, DateTime valFecha){
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

        private bool IsValidConsecutivoMovimiento(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivoMovimiento){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Insertar)) {
                return true;
            }
            if (valConsecutivoMovimiento == 0) {
                BuildValidationInfo(MsgRequiredField("Nº Movimiento"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                //if (KeyExists(valConsecutivoCompania, valConsecutivoMovimiento, valCodigoCtaBancaria, valCodigoConcepto, valFecha)) {
                //    BuildValidationInfo(MsgFieldValueAlreadyExist("Nº Movimiento", valConsecutivoMovimiento));
                //    vResult = false;
                //}
            }
            return vResult;
        }

        private bool IsValidCodigoCtaBancaria(eAccionSR valAction, int valConsecutivoCompania, string valCodigoCtaBancaria){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoCtaBancaria = LibString.Trim(valCodigoCtaBancaria);
            if (LibString.IsNullOrEmpty(valCodigoCtaBancaria, true)) {
                BuildValidationInfo(MsgRequiredField("Cuenta Bancaria"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivoMovimiento, valCodigoCtaBancaria, valCodigoConcepto, valFecha)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Cuenta Bancaria", valCodigoCtaBancaria));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidCodigoConcepto(eAccionSR valAction, int valConsecutivoCompania, string valCodigoConcepto){
            bool vResult = true;
            if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
                return true;
            }
            valCodigoConcepto = LibString.Trim(valCodigoConcepto);
            if (LibString.IsNullOrEmpty(valCodigoConcepto, true)) {
                BuildValidationInfo(MsgRequiredField("Código Concepto"));
                vResult = false;
            } else if (valAction == eAccionSR.Insertar) {
                if (KeyExists(valConsecutivoCompania, valConsecutivoMovimiento, valCodigoCtaBancaria, valCodigoConcepto, valFecha)) {
                    BuildValidationInfo(MsgFieldValueAlreadyExist("Código Concepto", valCodigoConcepto));
                    vResult = false;
                }
            }
            return vResult;
        }

        private bool IsValidFecha(eAccionSR valAction, int valConsecutivoCompania, DateTime valFecha){
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

        private bool KeyExists(int valConsecutivoCompania, int valConsecutivoMovimiento, string valCodigoCtaBancaria, string valCodigoConcepto, DateTime valFecha) {
            bool vResult = false;
            //MovimientoBancario vRecordBusqueda = new MovimientoBancario();
            //vRecordBusqueda.ConsecutivoCompania = this.valConsecutivoCompania;
            //vRecordBusqueda.ConsecutivoMovimiento = this.valConsecutivoMovimiento;
            //vRecordBusqueda.CodigoCtaBancaria = this.valCodigoCtaBancaria;
            //vRecordBusqueda.CodigoConcepto = this. valCodigoConcepto;
            //vRecordBusqueda.Fecha = this.valFecha;
            //LibDatabase insDb = new LibDataScope();
            //vResult = insDb.ExistsRecord("dbo.MovimientoBancario", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
            //insDb.Dispose();
            vResult = false;
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


    } //End of class clsMovimientoBancarioDat

} //End of namespace Galac.Adm.Dal.Banco

