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

namespace Galac.Adm.Dal.Banco {
	public class clsTransferenciaEntreCuentasBancariasDat : LibData, ILibDataComponentWithSearch<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>> {
		#region Propiedades
		private TransferenciaEntreCuentasBancarias CurrentRecord { get; set; }
		#endregion //Propiedades

		#region Constructores
		public clsTransferenciaEntreCuentasBancariasDat() {
			DbSchema = "Adm";
		}
		#endregion //Constructores

		#region Metodos Generados
		private StringBuilder ParametrosActualizacion(TransferenciaEntreCuentasBancarias valRecord, eAccionSR valAction) {
			LibGpParams vParams = new LibGpParams();
			vParams.AddReturn();
			vParams.AddInDateFormat("DateFormat");
			vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
			vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
			vParams.AddInEnum("Status", valRecord.StatusAsDB);
			vParams.AddInDateTime("Fecha", valRecord.Fecha);
			vParams.AddInString("NumeroDocumento", valRecord.NumeroDocumento, 20);
			vParams.AddInString("Descripcion", valRecord.Descripcion, 255);
			vParams.AddInDateTime("FechaDeAnulacion", valRecord.FechaDeAnulacion);
			vParams.AddInString("CodigoCuentaBancariaOrigen", valRecord.CodigoCuentaBancariaOrigen, 5);
			vParams.AddInDecimal("CambioABolivaresEgreso", valRecord.CambioABolivaresEgreso, 4);
			vParams.AddInDecimal("MontoTransferenciaEgreso", valRecord.MontoTransferenciaEgreso, 2);
			vParams.AddInString("CodigoConceptoEgreso", valRecord.CodigoConceptoEgreso, 8);
			vParams.AddInBoolean("GeneraComisionEgreso", valRecord.GeneraComisionEgresoAsBool);
			vParams.AddInDecimal("MontoComisionEgreso", valRecord.MontoComisionEgreso, 2);
			vParams.AddInString("CodigoConceptoComisionEgreso", valRecord.CodigoConceptoComisionEgreso, 8);
			vParams.AddInBoolean("GeneraImpuestoBancarioEgreso", valRecord.GeneraImpuestoBancarioEgresoAsBool);
			vParams.AddInString("CodigoCuentaBancariaDestino", valRecord.CodigoCuentaBancariaDestino, 5);
			vParams.AddInDecimal("CambioABolivaresIngreso", valRecord.CambioABolivaresIngreso, 4);
			vParams.AddInDecimal("MontoTransferenciaIngreso", valRecord.MontoTransferenciaIngreso, 2);
			vParams.AddInString("CodigoConceptoIngreso", valRecord.CodigoConceptoIngreso, 8);
			vParams.AddInBoolean("GeneraComisionIngreso", valRecord.GeneraComisionIngresoAsBool);
			vParams.AddInDecimal("MontoComisionIngreso", valRecord.MontoComisionIngreso, 2);
			vParams.AddInString("CodigoConceptoComisionIngreso", valRecord.CodigoConceptoComisionIngreso, 8);
			vParams.AddInBoolean("GeneraImpuestoBancarioIngreso", valRecord.GeneraImpuestoBancarioIngresoAsBool);
			vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
			vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
			if (valAction == eAccionSR.Modificar) {
				vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
			}
			return vParams.Get();
		}

		private StringBuilder ParametrosClave(TransferenciaEntreCuentasBancarias valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
			LibGpParams vParams = new LibGpParams();
			if (valAddReturnParameter) {
				vParams.AddReturn();
			}
			vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
			vParams.AddInInteger("Consecutivo", valRecord.Consecutivo);
			if (valIncludeTimestamp) {
				vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
			}
			return vParams.Get();
		}

		private StringBuilder ParametrosProximoConsecutivo(TransferenciaEntreCuentasBancarias valRecord) {
			LibGpParams vParams = new LibGpParams();
			vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
			return vParams.Get();
		}

		#region Miembros de ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>
		LibResponse ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.CanBeChoosen(IList<TransferenciaEntreCuentasBancarias> refRecord, eAccionSR valAction) {
			LibResponse vResult = new LibResponse();
			StringBuilder vSbInfo = new StringBuilder();
			LibDatabase insDB = new LibDatabase();
			if (valAction == eAccionSR.Eliminar) {
				if (vSbInfo.Length == 0) {
					vResult.Success = true;
				}
			} else if (valAction == eAccionSR.Anular) {
				if (insDB.ExistsRecord("dbo.Conciliacion", "ConsecutivoCompania", SqlFechaPerteneceAConciliacionCerradaParametros(refRecord[0], valIsOrigen: true).Get()) ||
					insDB.ExistsRecord("dbo.Conciliacion", "ConsecutivoCompania", SqlFechaPerteneceAConciliacionCerradaParametros(refRecord[0], valIsOrigen: false).Get()) ||
					insDB.ExistsRecord("dbo.MovimientoBancario", "ConsecutivoCompania", SqlConciliacionCerradaEnMovimientosBancariosParametros(refRecord[0]).Get())) {
					vSbInfo.AppendLine("- Los Movimientos Bancarios están conciliados.");
				}
				if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaModuloDeContabilidad"))) {
					if (insDB.RecordCountOfSql(SqlFechaPerteneceAPeriodoCerrado(refRecord[0])) > 0) {
						vSbInfo.AppendLine("- El Período Contable está cerrado.");
					} else if (insDB.RecordCountOfSql(SqlFechaPerteneceAMesCerrado(refRecord[0])) > 0) {
						vSbInfo.AppendLine("- El Mes Contable está cerrado.");
					}
				}
				if (vSbInfo.Length == 0) {
					vResult.Success = true;
				}
			} else {
				vResult.Success = true;
			}
			insDB.Dispose();
			if (!vResult.Success) {
				string vErrMsg;
				if (valAction == eAccionSR.Eliminar) {
					vErrMsg = LibResMsg.InfoForeignKeyCanNotBeDeleted(vSbInfo.ToString());
				} else {
					vErrMsg = "Este registro no se puede anular porque:" + Environment.NewLine + vSbInfo.ToString();
				}
				throw new GalacAlertException(vErrMsg);
			} else {
				return vResult;
			}
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Transferencia entre Cuentas.Eliminar")]
		LibResponse ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.Delete(IList<TransferenciaEntreCuentasBancarias> refRecord) {
			LibResponse vResult = new LibResponse();
			CurrentRecord = refRecord[0];
			if (Validate(eAccionSR.Eliminar, out string vErrMsg)) {
				LibDatabase insDb = new LibDatabase();
				vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TransferenciaEntreCuentasBancariasDEL"), ParametrosClave(CurrentRecord, true, true));
				insDb.Dispose();
			} else {
				throw new GalacValidationException(vErrMsg);
			}
			return vResult;
		}

		IList<TransferenciaEntreCuentasBancarias> ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
			List<TransferenciaEntreCuentasBancarias> vResult;
			LibDatabase insDb = new LibDatabase();
			switch (valType) {
				case eProcessMessageType.SpName:
					valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
					vResult = insDb.LoadFromSp<TransferenciaEntreCuentasBancarias>(valProcessMessage, valParameters, CmdTimeOut);
					break;
				case eProcessMessageType.Query:
					vResult = insDb.LoadData<TransferenciaEntreCuentasBancarias>(valProcessMessage, CmdTimeOut, valParameters);
					break;
				default: throw new ProgrammerMissingCodeException();
			}
			insDb.Dispose();
			return vResult;
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Transferencia entre Cuentas.Insertar")]
		LibResponse ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.Insert(IList<TransferenciaEntreCuentasBancarias> refRecord) {
			LibResponse vResult = new LibResponse();
			CurrentRecord = refRecord[0];
			if (ExecuteProcessBeforeInsert()) {
				if (Validate(eAccionSR.Insertar, out string vErrMsg)) {
					LibDatabase insDb = new LibDatabase();
					CurrentRecord.Consecutivo = insDb.NextLngConsecutive(DbSchema + ".TransferenciaEntreCuentasBancarias", "Consecutivo", ParametrosProximoConsecutivo(CurrentRecord));
					vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TransferenciaEntreCuentasBancariasINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
					insDb.Dispose();
				} else {
					throw new GalacValidationException(vErrMsg);
				}
			}
			return vResult;
		}

		XElement ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
			XElement vResult = null;
			LibDatabase insDb = new LibDatabase();
			switch (valType) {
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

		[PrincipalPermission(SecurityAction.Demand, Role = "Transferencia entre Cuentas.Modificar")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Transferencia entre Cuentas.Anular")]
		LibResponse ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.Update(IList<TransferenciaEntreCuentasBancarias> refRecord) {
			LibResponse vResult = new LibResponse();
			CurrentRecord = refRecord[0];
			if (ExecuteProcessBeforeUpdate()) {
				if (Validate(eAccionSR.Modificar, out string vErrMsg)) {
					LibDatabase insDb = new LibDatabase();
					vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "TransferenciaEntreCuentasBancariasUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
					insDb.Dispose();
				} else {
					throw new GalacValidationException(vErrMsg);
				}
			}
			return vResult;
		}

		bool ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.ValidateAll(IList<TransferenciaEntreCuentasBancarias> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
			bool vResult = true;
			string vErroMessage = string.Empty;
			foreach (TransferenciaEntreCuentasBancarias vItem in refRecords) {
				CurrentRecord = vItem;
				vResult = vResult && Validate(valAction, out vErroMessage);
				if (LibString.IsNullOrEmpty(vErroMessage, true)) {
					refErrorMessage.AppendLine(vErroMessage);
				}
			}
			return vResult;
		}

		LibResponse ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>.SpecializedUpdate(IList<TransferenciaEntreCuentasBancarias> refRecord, string valSpecializedAction) {
			throw new ProgrammerMissingCodeException();
		}
		#endregion //ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>

		#region Validaciones
		protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
			ClearValidationInfo();
			bool vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania);
			vResult = IsValidConsecutivo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Consecutivo) && vResult;
			vResult = IsValidFecha(valAction, CurrentRecord.Fecha) && vResult;
			vResult = IsValidNumeroDocumento(valAction, CurrentRecord.NumeroDocumento) && vResult;
			vResult = IsValidDescripcion(valAction, CurrentRecord.Descripcion) && vResult;
			vResult = IsValidCodigoCuentaBancariaOrigen(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoCuentaBancariaOrigen) && vResult;
			vResult = IsValidCambioABolivaresEgreso(valAction, CurrentRecord.CambioABolivaresEgreso) && vResult;
			vResult = IsValidMontoTransferenciaEgreso(valAction, CurrentRecord.MontoTransferenciaEgreso) && vResult;
			vResult = IsValidCodigoConceptoEgreso(valAction, CurrentRecord.CodigoConceptoEgreso) && vResult;
			vResult = IsValidMontoComisionEgreso(valAction, CurrentRecord.MontoComisionEgreso, CurrentRecord.GeneraComisionEgresoAsBool) && vResult;
			vResult = IsValidCodigoConceptoComisionEgreso(valAction, CurrentRecord.CodigoConceptoComisionEgreso, CurrentRecord.GeneraComisionEgresoAsBool) && vResult;
			vResult = IsValidCodigoCuentaBancariaDestino(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.CodigoCuentaBancariaDestino) && vResult;
			vResult = IsValidCambioABolivaresIngreso(valAction, CurrentRecord.CambioABolivaresIngreso) && vResult;
			vResult = IsValidMontoTransferenciaIngreso(valAction, CurrentRecord.MontoTransferenciaIngreso) && vResult;
			vResult = IsValidCodigoConceptoIngreso(valAction, CurrentRecord.CodigoConceptoIngreso) && vResult;
			vResult = IsValidMontoComisionIngreso(valAction, CurrentRecord.MontoComisionIngreso, CurrentRecord.GeneraComisionIngresoAsBool) && vResult;
			vResult = IsValidCodigoConceptoComisionIngreso(valAction, CurrentRecord.CodigoConceptoComisionIngreso, CurrentRecord.GeneraComisionIngresoAsBool) && vResult;
			vResult = IsValidConceptoImpuestoBancarioEgreso(valAction, CurrentRecord.GeneraImpuestoBancarioEgresoAsBool) && vResult;
			vResult = IsValidConceptoImpuestoBancarioIngreso(valAction, CurrentRecord.GeneraImpuestoBancarioIngresoAsBool) && vResult;
			vResult = IsValidCuentasBancariasDiferentes(valAction, CurrentRecord.CodigoCuentaBancariaOrigen, CurrentRecord.CodigoCuentaBancariaDestino) && vResult;
			vResult = IsValidMontosConMonedasIguales(valAction, CurrentRecord) & vResult;
			vResult = IsValidFechaParaConciliacionYPeriodoContable(valAction, CurrentRecord) && vResult;
			outErrorMessage = Information.ToString();
			return vResult;
		}

		private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valConsecutivoCompania <= 0) {
				BuildValidationInfo(MsgRequiredField("Consecutivo Compania"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidConsecutivo(eAccionSR valAction, int valConsecutivoCompania, int valConsecutivo) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular) || (valAction == eAccionSR.Insertar)) {
				return true;
			}
			if (valConsecutivo == 0) {
				BuildValidationInfo(MsgRequiredField("Nº Movimiento"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidFecha(eAccionSR valAction, DateTime valFecha) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(valFecha, false, valAction)) {
				BuildValidationInfo(LibDefGen.MessageDateRestrictionDemoProgram());
				vResult = false;
			} else if (LibDate.DateIsGreaterThanToday(valFecha, false, string.Empty)) {
				BuildValidationInfo(LibDate.MsgDateIsGreaterThanToday("Fecha"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidNumeroDocumento(eAccionSR valAction, string valNumeroDocumento) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (LibString.IsNullOrEmpty(valNumeroDocumento, true)) {
				BuildValidationInfo(MsgRequiredField("Nº Documento"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidDescripcion(eAccionSR valAction, string valDescripcion) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (LibString.IsNullOrEmpty(valDescripcion, true)) {
				BuildValidationInfo(MsgRequiredField("Descripción"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidCodigoCuentaBancariaOrigen(eAccionSR valAction, int valConsecutivoCompania, string valCodigoCuentaBancariaOrigen) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (LibString.IsNullOrEmpty(valCodigoCuentaBancariaOrigen, true)) {
				BuildValidationInfo(MsgRequiredField("Cuenta Bancaria Origen"));
				vResult = false;
			} else {
				clsCuentaBancariaDat vClsCuentaBancariaDat = new clsCuentaBancariaDat();
				if (!vClsCuentaBancariaDat.ExisteYEstaActiva(valConsecutivoCompania, valCodigoCuentaBancariaOrigen)) {
					BuildValidationInfo(MsgCuentaBancariaNoExisteOEsInactiva("Cuenta Bancaria Origen"));
					vResult = false;
				} else if (vClsCuentaBancariaDat.ConfiguracionParaIGTFIncompleta(valConsecutivoCompania, valCodigoCuentaBancariaOrigen, eIngresoEgreso.Egreso)) {
					BuildValidationInfo(MsgCuentaBancariaNoConfiguradaPraIGTF("Cuenta Bancaria Origen"));
					vResult = false;
				}
			}
			return vResult;
		}

		private bool IsValidCambioABolivaresEgreso(eAccionSR valAction, decimal valCambioABolivaresEgreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valCambioABolivaresEgreso <= 0) {
				BuildValidationInfo("El campo Cambio de Egreso debe ser mayor que 0.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidMontoTransferenciaEgreso(eAccionSR valAction, decimal valMontoTransferenciaEgreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valMontoTransferenciaEgreso <= 0) {
				BuildValidationInfo("El campo Monto de Egreso debe ser mayor que 0.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidCodigoConceptoEgreso(eAccionSR valAction, string valCodigoConceptoEgreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (LibString.IsNullOrEmpty(valCodigoConceptoEgreso, true)) {
				BuildValidationInfo(MsgRequiredField("Concepto Bancario de Egreso"));
				vResult = false;
			} else if (!new clsConceptoBancarioDat().ExisteYEsEgresoIngreso(valCodigoConceptoEgreso, eIngresoEgreso.Egreso)) {
				BuildValidationInfo(MsgConceptoBancarioNoExisteOEsTipoInadecuado("Concepto Bancario de Egreso", eIngresoEgreso.Egreso));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidMontoComisionEgreso(eAccionSR valAction, decimal valMontoComisionEgreso, bool valGeneraComisionEgreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valGeneraComisionEgreso && valMontoComisionEgreso <= 0) {
				BuildValidationInfo("El campo Monto Comisión de Egreso debe ser mayor que 0.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidCodigoConceptoComisionEgreso(eAccionSR valAction, string valCodigoConceptoComisionEgreso, bool valGeneraComisionEgreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valGeneraComisionEgreso) {
				if (LibString.IsNullOrEmpty(valCodigoConceptoComisionEgreso, true)) {
					BuildValidationInfo(MsgRequiredField("Concepto Bancario Comisión de Egreso"));
					vResult = false;
				} else if (!new clsConceptoBancarioDat().ExisteYEsEgresoIngreso(valCodigoConceptoComisionEgreso, eIngresoEgreso.Egreso)) {
					BuildValidationInfo(MsgConceptoBancarioNoExisteOEsTipoInadecuado("Concepto Bancario Comisión de Egreso", eIngresoEgreso.Egreso));
					vResult = false;
				}
			}
			return vResult;
		}

		private bool IsValidCodigoCuentaBancariaDestino(eAccionSR valAction, int valConsecutivoCompania, string valCodigoCuentaBancariaDestino) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (LibString.IsNullOrEmpty(valCodigoCuentaBancariaDestino, true)) {
				BuildValidationInfo(MsgRequiredField("Cuenta Bancaria Destino"));
				vResult = false;
			} else {
				clsCuentaBancariaDat vClsCuentaBancariaDat = new clsCuentaBancariaDat();
				if (!vClsCuentaBancariaDat.ExisteYEstaActiva(valConsecutivoCompania, valCodigoCuentaBancariaDestino)) {
					BuildValidationInfo(MsgCuentaBancariaNoExisteOEsInactiva("Cuenta Bancaria Destino"));
					vResult = false;
				} else if (vClsCuentaBancariaDat.ConfiguracionParaIGTFIncompleta(valConsecutivoCompania, valCodigoCuentaBancariaDestino, eIngresoEgreso.Ingreso)) {
					BuildValidationInfo(MsgCuentaBancariaNoConfiguradaPraIGTF("Cuenta Bancaria Destino"));
					vResult = false;
				}
			}
			return vResult;
		}

		private bool IsValidCambioABolivaresIngreso(eAccionSR valAction, decimal valCambioABolivaresIngreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valCambioABolivaresIngreso <= 0) {
				BuildValidationInfo("El campo Cambio de Ingreso debe ser mayor que 0.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidMontoTransferenciaIngreso(eAccionSR valAction, decimal valMontoTransferenciaIngreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valMontoTransferenciaIngreso <= 0) {
				BuildValidationInfo("El Monto de Ingreso debe ser mayor que 0.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidCodigoConceptoIngreso(eAccionSR valAction, string valCodigoConceptoIngreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (LibString.IsNullOrEmpty(valCodigoConceptoIngreso, true)) {
				BuildValidationInfo(MsgRequiredField("Concepto Bancario de Ingreso"));
				vResult = false;
			} else if (!new clsConceptoBancarioDat().ExisteYEsEgresoIngreso(valCodigoConceptoIngreso, eIngresoEgreso.Ingreso)) {
				BuildValidationInfo(MsgConceptoBancarioNoExisteOEsTipoInadecuado("Concepto Bancario de Ingreso", eIngresoEgreso.Ingreso));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidMontoComisionIngreso(eAccionSR valAction, decimal valMontoComisionIngreso, bool valGeneraComisionIngreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valGeneraComisionIngreso && valMontoComisionIngreso <= 0) {
				BuildValidationInfo("El campo Monto Comisión de Ingreso debe ser mayor que 0.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidCodigoConceptoComisionIngreso(eAccionSR valAction, string valCodigoConceptoComisionIngreso, bool valGeneraComisionIngreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valGeneraComisionIngreso) {
				if (LibString.IsNullOrEmpty(valCodigoConceptoComisionIngreso, true)) {
					BuildValidationInfo(MsgRequiredField("Concepto Bancario Comisión de Ingreso"));
					vResult = false;
				} else if (!new clsConceptoBancarioDat().ExisteYEsEgresoIngreso(valCodigoConceptoComisionIngreso, eIngresoEgreso.Egreso )) {
					BuildValidationInfo(MsgConceptoBancarioNoExisteOEsTipoInadecuado("Concepto Bancario Comisión de Ingreso", eIngresoEgreso.Egreso ));
					vResult = false;
				}
			}
			return vResult;
		}

		private bool IsValidConceptoImpuestoBancarioEgreso(eAccionSR valAction, bool valGeneraImpuestoBancarioEgreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valGeneraImpuestoBancarioEgreso && LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoDebitoBancario"), true)) {
				BuildValidationInfo(MsgRequiredField("Concepto Débito Bancario") + " Debe configurarse desde Parámetros");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidConceptoImpuestoBancarioIngreso(eAccionSR valAction, bool valGeneraImpuestoBancarioIngreso) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (valGeneraImpuestoBancarioIngreso && LibString.IsNullOrEmpty(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoCreditoBancario"), true)) {
				BuildValidationInfo(MsgRequiredField("Concepto Crédito Bancario") + " Debe configurarse desde Parámetros");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidCuentasBancariasDiferentes(eAccionSR valAction, string valCodigoCuentaBancariaOrigen, string valCodigoCuentaBancariaDestino) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (!LibString.IsNullOrEmpty(valCodigoCuentaBancariaOrigen, true)
					&& !LibString.IsNullOrEmpty(valCodigoCuentaBancariaDestino, true)
					&& LibString.S1IsEqualToS2(valCodigoCuentaBancariaOrigen, valCodigoCuentaBancariaDestino)) {
				BuildValidationInfo("La cuenta a debitar debe ser diferente a la cuenta a acreditar.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidMontosConMonedasIguales(eAccionSR valAction, TransferenciaEntreCuentasBancarias refRecord) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			if (!LibString.IsNullOrEmpty(refRecord.CodigoMonedaCuentaBancariaOrigen, true)
					&& !LibString.IsNullOrEmpty(refRecord.CodigoMonedaCuentaBancariaDestino, true)
					&& LibString.S1IsEqualToS2(refRecord.CodigoMonedaCuentaBancariaOrigen, refRecord.CodigoMonedaCuentaBancariaDestino)
					&& refRecord.MontoTransferenciaEgreso < refRecord.MontoTransferenciaIngreso) {
				BuildValidationInfo("Si las Monedas de las Cuentas Bancarias coinciden, El Monto de Ingreso no debe ser mayor al Monto de Egreso.");
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidFechaParaConciliacionYPeriodoContable(eAccionSR valAction, TransferenciaEntreCuentasBancarias refRecord) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar) || (valAction == eAccionSR.Anular)) {
				return true;
			}
			LibDatabase insDB = new LibDatabase();
			if (insDB.ExistsRecord("dbo.Conciliacion", "ConsecutivoCompania", SqlFechaPerteneceAConciliacionCerradaParametros(refRecord, valIsOrigen: true).Get())) {
				BuildValidationInfo("La Fecha no puede pertenecer a una Conciliación Cerrada para la Cuenta Bancaria Origen.");
				vResult = false;
			}
			if (insDB.ExistsRecord("dbo.Conciliacion", "ConsecutivoCompania", SqlFechaPerteneceAConciliacionCerradaParametros(refRecord, valIsOrigen: false).Get())) {
				BuildValidationInfo("La Fecha no puede pertenecer a una Conciliación Cerrada para la Cuenta Bancaria Destino.");
				vResult = false;
			}
			if (LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "UsaModuloDeContabilidad"))) {
				if (insDB.RecordCountOfSql(SqlFechaPerteneceAPeriodoCerrado(refRecord)) > 0) {
					BuildValidationInfo("La Fecha no puede pertenecer a un Período Contable Cerrado.");
					vResult = false;
				} else if (insDB.RecordCountOfSql(SqlFechaPerteneceAMesCerrado(refRecord)) > 0) {
					BuildValidationInfo("La Fecha no puede pertenecer a un Mes Contable Cerrado.");
					vResult = false;
				}
			}
			insDB.Dispose();
			return vResult;
		}

		private bool KeyExists(int valConsecutivoCompania, int valConsecutivo) {
			TransferenciaEntreCuentasBancarias vRecordBusqueda = new TransferenciaEntreCuentasBancarias();
			vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
			vRecordBusqueda.Consecutivo = valConsecutivo;
			LibDatabase insDb = new LibDatabase();
			bool vResult = insDb.ExistsRecord(DbSchema + ".TransferenciaEntreCuentasBancarias", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
			insDb.Dispose();
			return vResult;
		}

		private bool KeyExists(int valConsecutivoCompania, TransferenciaEntreCuentasBancarias valRecordBusqueda) {
			valRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
			LibDatabase insDb = new LibDatabase();
			bool vResult = insDb.ExistsRecord(DbSchema + ".TransferenciaEntreCuentasBancarias", "ConsecutivoCompania", ParametrosClave(valRecordBusqueda, false, false));
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

		#region Código Programador
		private string MsgCuentaBancariaNoExisteOEsInactiva(string valCuentaBancaria) {
			return "La " + valCuentaBancaria + " no existe o es inactiva.";
		}

		private string MsgCuentaBancariaNoConfiguradaPraIGTF(string valCuentaBancaria) {
			return "La " + valCuentaBancaria + " genera I.G.T.F., pero no tiene un Tipo de Alícuota por Contribuyente asignado. Debe modificar esta cuenta bancaria antes de continuar.";
		}

		private string MsgConceptoBancarioNoExisteOEsTipoInadecuado(string valCampo, eIngresoEgreso valTipo) {
			return "El campo " + valCampo + " no existe o es de un tipo inadecuado (Debe ser " + valTipo.GetDescription() + ").";
		}

		private LibGpParams SqlFechaPerteneceAConciliacionCerradaParametros(TransferenciaEntreCuentasBancarias refRecord, bool valIsOrigen) {
			LibGpParams dbParam = new LibGpParams();
			dbParam.AddInInteger("ConsecutivoCompania", refRecord.ConsecutivoCompania);
			dbParam.AddInString("CodigoCuenta", valIsOrigen ? refRecord.CodigoCuentaBancariaOrigen : refRecord.CodigoCuentaBancariaDestino, 5);
			dbParam.AddInInteger("MesDeAplicacion", refRecord.Fecha.Month);
			dbParam.AddInInteger("AnoDeAplicacion", refRecord.Fecha.Year);
			dbParam.AddInEnum("Status", (int) eStatusConciliacion.Cerrada);
			return dbParam;
		}

		private LibGpParams SqlConciliacionCerradaEnMovimientosBancariosParametros(TransferenciaEntreCuentasBancarias refRecord) {
			LibGpParams dbParam = new LibGpParams();
			dbParam.AddInInteger("ConsecutivoCompania", refRecord.ConsecutivoCompania);
			dbParam.AddInString("NumeroDocumento", LibConvert.ToStr(refRecord.Consecutivo), 20);
			dbParam.AddInEnum("GeneradoPor", (int) eGeneradoPor.TransferenciaBancaria);
			dbParam.AddInBoolean("ConciliadoSn", true);
			return dbParam;
		}

		private string SqlFechaPerteneceAPeriodoCerrado(TransferenciaEntreCuentasBancarias refRecord) {
			QAdvSql vSqlUtil = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "";

			vSql.AppendLine("SELECT ConsecutivoPeriodo");
			vSql.AppendLine("FROM dbo.PERIODO");

			vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "ConsecutivoCompania", refRecord.ConsecutivoCompania);
			vSQLWhere = vSqlUtil.SqlDateValueWithOperators(vSQLWhere, "FechaAperturaDelPeriodo", refRecord.Fecha, vSqlUtil.CurrentDateFormat, "AND", "<=");
			vSQLWhere = vSqlUtil.SqlDateValueWithOperators(vSQLWhere, "FechaCierreDelPeriodo", refRecord.Fecha, vSqlUtil.CurrentDateFormat, "AND", ">=");
			vSQLWhere = vSqlUtil.SqlBoolValueWithAnd(vSQLWhere, "PeriodoCerrado", true);
			vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
			vSql.AppendLine(vSQLWhere);

			return vSql.ToString();
		}

		private string SqlFechaPerteneceAMesCerrado(TransferenciaEntreCuentasBancarias refRecord) {
			QAdvSql vSqlUtil = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();
			string vSQLWhere = "", vSQLWhereMeses = "", vSQLWhereParaMes;

			vSql.AppendLine("SELECT ConsecutivoPeriodo");
			vSql.AppendLine("FROM dbo.PERIODO");

			vSQLWhere = vSqlUtil.SqlIntValueWithAnd(vSQLWhere, "ConsecutivoCompania", refRecord.ConsecutivoCompania);
			vSQLWhere = vSqlUtil.SqlBoolValueWithAnd(vSQLWhere, "UsaCierreDeMes", true);
			vSQLWhere = vSqlUtil.SqlBoolValueWithAnd(vSQLWhere, "PeriodoCerrado", false);
			vSQLWhere = vSqlUtil.SqlDateValueWithOperators(vSQLWhere, "FechaAperturaDelPeriodo", refRecord.Fecha, vSqlUtil.CurrentDateFormat, "AND", "<=");
			vSQLWhere = vSqlUtil.SqlDateValueWithOperators(vSQLWhere, "FechaCierreDelPeriodo", refRecord.Fecha, vSqlUtil.CurrentDateFormat, "AND", ">");

			for (int i = 1; i < 12; i++) {
				vSQLWhereParaMes = "";
				vSQLWhereParaMes = vSqlUtil.SqlBoolValueWithAnd(vSQLWhereParaMes, "Mes" + LibConvert.ToStr(i) + "Cerrado", true);
				vSQLWhereParaMes = vSqlUtil.SqlDateValueWithOperators(vSQLWhereParaMes, "FechaDeCierre" + LibConvert.ToStr(i), refRecord.Fecha, vSqlUtil.CurrentDateFormat, "AND", ">=");

				vSQLWhereMeses += "(" + vSQLWhereParaMes + (i < 11 ? ") OR " : ")");
			}

			vSQLWhere += " AND (" + vSQLWhereMeses + ")";
			vSQLWhere = vSqlUtil.WhereSql(vSQLWhere);
			vSql.AppendLine(vSQLWhere);

			return vSql.ToString();
		}
		#endregion

	} //End of class clsTransferenciaEntreCuentasBancariasDat

} //End of namespace Galac.Adm.Dal.Banco
