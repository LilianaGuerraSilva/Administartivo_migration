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
	public class clsCuentaBancariaDat : LibData, ILibDataComponentWithSearch<IList<CuentaBancaria>, IList<CuentaBancaria>>, ILibDataRpt {
		#region Variables
		CuentaBancaria _CurrentRecord;
		#endregion //Variables

		#region Propiedades
		private CuentaBancaria CurrentRecord {
			get { return _CurrentRecord; }
			set { _CurrentRecord = value; }
		}
		#endregion //Propiedades

		#region Constructores
		public clsCuentaBancariaDat() {
			DbSchema = "Saw";
		}
		#endregion //Constructores

		#region Metodos Generados
		private StringBuilder ParametrosActualizacion(CuentaBancaria valRecord, eAccionSR valAction) {
			StringBuilder vResult = new StringBuilder();
			LibGpParams vParams = new LibGpParams();
			vParams.AddReturn();
			vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
			vParams.AddInString("Codigo", valRecord.Codigo, 5);
			vParams.AddInEnum("Status", valRecord.StatusAsDB);
			vParams.AddInString("NumeroCuenta", valRecord.NumeroCuenta, 30);
			vParams.AddInString("NombreCuenta", valRecord.NombreCuenta, 40);
			vParams.AddInInteger("CodigoBanco", valRecord.CodigoBanco);
			vParams.AddInString("NombreSucursal", valRecord.NombreSucursal, 40);
			vParams.AddInEnum("TipoCtaBancaria", valRecord.TipoCtaBancariaAsDB);
			vParams.AddInBoolean("ManejaDebitoBancario", valRecord.ManejaDebitoBancarioAsBool);
			vParams.AddInBoolean("ManejaCreditoBancario", valRecord.ManejaCreditoBancarioAsBool);
			vParams.AddInDecimal("SaldoDisponible", valRecord.SaldoDisponible, 2);
			vParams.AddInString("NombreDeLaMoneda", valRecord.NombreDeLaMoneda, 80);
			vParams.AddInString("NombrePlantillaCheque", valRecord.NombrePlantillaCheque, 50);
			vParams.AddInString("CuentaContable", valRecord.CuentaContable, 30);
			vParams.AddInString("CodigoMoneda", valRecord.CodigoMoneda, 4);
			vParams.AddInBoolean("EsCajaChica", valRecord.EsCajaChicaAsBool);
			vParams.AddInEnum("TipoDeAlicuotaPorContribuyente", valRecord.TipoDeAlicuotaPorContribuyenteAsDB);
			vParams.AddInBoolean("GeneraMovBancarioPorIGTF", valRecord.GeneraMovBancarioPorIGTFAsBool);
			vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
			vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
			if (valAction == eAccionSR.Modificar) {
				vParams.AddInTimestamp("TimeStampAsInt", valRecord.fldTimeStamp);
			}
			vResult = vParams.Get();
			return vResult;
		}

		private StringBuilder ParametrosActualizaSaldoDisponible(int valConsecutivoCompania, string valCodigoCuenta, string valMonto, string valIngresoEgreso, int valmAction, string valMontoOriginal, bool valSeModificoTipoConcepto) {
			StringBuilder vResult = new StringBuilder();
			LibGpParams vParams = new LibGpParams();
			vParams.AddReturn();
			vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
			vParams.AddInString("CodigoCuenta", valCodigoCuenta, 5);
			vParams.AddInDecimal("Monto", LibConvert.ToDec(valMonto, 2), 2);
			vParams.AddInString("IngresoEgreso", valIngresoEgreso, 1);
			vParams.AddInInteger("mAction", valmAction);
			vParams.AddInDecimal("MontoOriginal", LibConvert.ToDec(valMontoOriginal, 2), 2);
			vParams.AddInBoolean("SeModificoTipoConcepto", valSeModificoTipoConcepto);
			vParams.AddInString("NombreOperador", ((CustomIdentity) Thread.CurrentPrincipal.Identity).Login, 10);
			vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
			vResult = vParams.Get();
			return vResult;
		}

		private StringBuilder ParametroCompania(int valConsecutivoCompania) {
			StringBuilder vResult = new StringBuilder();
			LibGpParams vParams = new LibGpParams();
			vParams.AddReturn();
			vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
			vResult = vParams.Get();
			return vResult;
		}

		private StringBuilder ParametrosClave(CuentaBancaria valRecord, bool valIncludeTimestamp, bool valAddReturnParameter) {
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

		private StringBuilder ParametrosProximoConsecutivo(CuentaBancaria valRecord) {
			StringBuilder vResult = new StringBuilder();
			LibGpParams vParams = new LibGpParams();
			vParams.AddInInteger("ConsecutivoCompania", valRecord.ConsecutivoCompania);
			vResult = vParams.Get();
			return vResult;
		}

		#region Miembros de ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>
		LibResponse ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.CanBeChoosen(IList<CuentaBancaria> refRecord, eAccionSR valAction) {
			LibResponse vResult = new LibResponse();
			CuentaBancaria vRecord = refRecord[0];
			StringBuilder vSbInfo = new StringBuilder();
			string vErrMsg = "";
			LibDatabase insDB = new LibDatabase();
			if (valAction == eAccionSR.Eliminar) {
				if (LibDefGen.IsProduct(LibProduct.GetInitialsSaw())) {
					if (ExisteTablaSettValueByCompany()) {
						if (getParametrosCompania<string>(vRecord.ConsecutivoCompania, "CodigoGenericoCuentaBancaria", this).Equals(vRecord.Codigo)) {
							vSbInfo.AppendLine("Parámetros Compañía");
						}
						if (getParametrosCompania<string>(vRecord.ConsecutivoCompania, "CuentaBancariaCobroDirecto", this).Equals(vRecord.Codigo)) {
							vSbInfo.AppendLine("Parámetros Compañía");
						}
						if (getParametrosCompania<string>(vRecord.ConsecutivoCompania, "CuentaBancariaAnticipo", this).Equals(vRecord.Codigo)) {
							vSbInfo.AppendLine("Parámetros Compañía");
						}
						if (insDB.ExistsValueOnMultifile("Saw.RenglonSolicitudesDePago", "CuentaBancaria", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
							vSbInfo.AppendLine("Renglon Solicitudes De Pago");
						}
						if (insDB.ExistsValueOnMultifile("Adm.Rendicion", "CodigoCtaBancariaCajaChica", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
							vSbInfo.AppendLine("Caja Chica");
						}
					}
				} else {
					if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "CuentaBancariaAnticipo", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
						vSbInfo.AppendLine("Parámetros Compañía");
					}
					if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "CuentaBancariaCobroDirecto", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
						vSbInfo.AppendLine("Parámetros Compañía");
					}
					if (insDB.ExistsValueOnMultifile("dbo.ParametrosCompania", "CodigoGenericoCuentaBancaria", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
						vSbInfo.AppendLine("Parámetros Compañía");
					}
				}
				if (insDB.ExistsValueOnMultifile("dbo.Pago", "CodigoCuentaBancaria", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
					vSbInfo.AppendLine("Pago");
				}
				if (insDB.ExistsValueOnMultifile("dbo.MovimientoBancario", "CodigoCtaBancaria", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
					vSbInfo.AppendLine("Movimiento Bancario");
				}
				if (insDB.ExistsValueOnMultifile("dbo.Anticipo", "CodigoCuentaBancaria", "ConsecutivoCompania", insDB.InsSql.ToSqlValue(vRecord.Codigo), insDB.InsSql.ToSqlValue(vRecord.ConsecutivoCompania), true)) {
					vSbInfo.AppendLine("Anticipo");
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

		private T getParametrosCompania<T>(int valConsecutivoCompania, string ValParametro, ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal) {
			string vDbSchema = "";
			if (LibDefGen.IsProduct(LibProduct.GetInitialsAdmEcuador())) {
				vDbSchema = "Adme";
			} else {
				vDbSchema = "Comun";
			}
			StringBuilder sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS Valor, " + vDbSchema + ".SettValueByCompany.NameSettDefinition FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE (" + vDbSchema + ".SettDefinition.Name = '" + ValParametro + "') AND (" + vDbSchema + ".SettValueByCompany.ConsecutivoCompania = " + valConsecutivoCompania + ")", 300);
			XElement Auxiliar = instanciaDal.QueryInfo(eProcessMessageType.Query, null, sql);
			Object vValor = LibXml.GetPropertyString(Auxiliar, "Valor");
			return (T) vValor;
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Cuenta Bancaria.Eliminar")]
		LibResponse ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.Delete(IList<CuentaBancaria> refRecord) {
			LibResponse vResult = new LibResponse();
			string vErrMsg = "";
			CurrentRecord = refRecord[0];
			if (Validate(eAccionSR.Eliminar, out vErrMsg)) {
				LibDatabase insDb = new LibDatabase();
				vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CuentaBancariaDEL"), ParametrosClave(CurrentRecord, true, true));
				insDb.Dispose();
			} else {
				throw new GalacValidationException(vErrMsg);
			}
			return vResult;
		}

		IList<CuentaBancaria> ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
			List<CuentaBancaria> vResult = new List<CuentaBancaria>();
			LibDatabase insDb = new LibDatabase();
			switch (valType) {
				case eProcessMessageType.SpName:
					valProcessMessage = insDb.ToSpName(DbSchema, valProcessMessage);
					vResult = insDb.LoadFromSp<CuentaBancaria>(valProcessMessage, valParameters, CmdTimeOut);
					break;
				default: break;
			}
			insDb.Dispose();
			return vResult;
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Cuenta Bancaria.Insertar")]
		[PrincipalPermission(SecurityAction.Demand, Role = "Compañía.Insertar")]
		LibResponse ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.Insert(IList<CuentaBancaria> refRecord) {
			LibResponse vResult = new LibResponse();
			string vErrMsg = "";
			CurrentRecord = refRecord[0];
			if (ExecuteProcessBeforeInsert()) {
				if (Validate(eAccionSR.Insertar, out vErrMsg)) {
					LibDatabase insDb = new LibDatabase();
					vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CuentaBancariaINS"), ParametrosActualizacion(CurrentRecord, eAccionSR.Insertar));
					insDb.Dispose();
				} else {
					throw new GalacValidationException(vErrMsg);
				}
			}
			return vResult;
		}

		XElement ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
			XElement vResult = null;
			bool vHayCtasParaCompania = false;
			LibDatabase insDb = new LibDatabase();
			switch (valType) {
				case eProcessMessageType.SpName:
					vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
					break;
				case eProcessMessageType.Query:
					vResult = LibXml.ToXElement(insDb.LoadData(valParameters.ToString(), CmdTimeOut));
					break;
				case eProcessMessageType.Message:
					CuentaBancaria vRecordBusqueda = new CuentaBancaria();
					vHayCtasParaCompania = insDb.ExistsRecord("Saw.CuentaBancaria", "ConsecutivoCompania", valParameters);
					if (vHayCtasParaCompania) {
						if (valProcessMessage == "ProximoNumero") {
							vResult = LibXml.ToXElement(LibXml.ValueToXmlDocument(insDb.NextStrConsecutive("Saw.CuentaBancaria", "Codigo", valParameters.ToString(), true, 5), "Codigo"));
						}
					} else {
						vResult = LibXml.ToXElement(LibXml.ValueToXmlDocument("00001", "Codigo"));
					}
					break;
				default: break;
			}
			insDb.Dispose();
			return vResult;
		}

		[PrincipalPermission(SecurityAction.Demand, Role = "Cuenta Bancaria.Modificar")]
		LibResponse ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.Update(IList<CuentaBancaria> refRecord) {
			LibResponse vResult = new LibResponse();
			string vErrMsg = "";
			CurrentRecord = refRecord[0];
			if (ExecuteProcessBeforeUpdate()) {
				if (Validate(eAccionSR.Modificar, out vErrMsg)) {
					LibDatabase insDb = new LibDatabase();
					vResult.Success = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CuentaBancariaUPD"), ParametrosActualizacion(CurrentRecord, eAccionSR.Modificar));
					insDb.Dispose();
				} else {
					throw new GalacValidationException(vErrMsg);
				}
			}
			return vResult;
		}

		bool ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.ValidateAll(IList<CuentaBancaria> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
			bool vResult = true;
			string vErroMessage = "";
			foreach (CuentaBancaria vItem in refRecords) {
				CurrentRecord = vItem;
				vResult = vResult && Validate(valAction, out vErroMessage);
				if (LibString.IsNullOrEmpty(vErroMessage, true)) {
					refErrorMessage.AppendLine(vErroMessage);
				}
			}
			return vResult;
		}

		LibResponse ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>.SpecializedUpdate(IList<CuentaBancaria> refRecord, string valSpecializedAction) {
			throw new NotImplementedException();
		}
		#endregion //ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>

		#region Validaciones
		protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
			bool vResult = true;
			ClearValidationInfo();
			vResult = IsValidConsecutivoCompania(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo);
			vResult = IsValidCodigo(valAction, CurrentRecord.ConsecutivoCompania, CurrentRecord.Codigo) && vResult;
			vResult = IsValidNombreDeLaMoneda(valAction, CurrentRecord.NombreDeLaMoneda) && vResult;
			vResult = IsValidNumeroCuenta(valAction, CurrentRecord.NumeroCuenta) && vResult;
			vResult = IsValidNombreCuenta(valAction, CurrentRecord.NombreCuenta) && vResult;
			vResult = IsValidCodigoBanco(valAction, CurrentRecord.CodigoBanco) && vResult;
			outErrorMessage = Information.ToString();
			return vResult;
		}

		private bool IsValidConsecutivoCompania(eAccionSR valAction, int valConsecutivoCompania, string valCodigo) {
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

		private bool IsValidCodigo(eAccionSR valAction, int valConsecutivoCompania, string valCodigo) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
				return true;
			}
			valCodigo = LibString.Trim(valCodigo);
			if (LibString.IsNullOrEmpty(valCodigo, true)) {
				BuildValidationInfo(MsgRequiredField("Código Cuenta Bancaria"));
				vResult = false;
			} else if (valAction == eAccionSR.Insertar) {
				if (KeyExists(valConsecutivoCompania, valCodigo)) {
					BuildValidationInfo(MsgFieldValueAlreadyExist("Código Cuenta Bancaria", valCodigo));
					vResult = false;
				}
			}
			return vResult;
		}

		private bool IsValidNumeroCuenta(eAccionSR valAction, string valNumeroCuenta) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
				return true;
			}
			valNumeroCuenta = LibString.Trim(valNumeroCuenta);
			if (LibString.IsNullOrEmpty(valNumeroCuenta, true)) {
				BuildValidationInfo(MsgRequiredField("Número Cuenta Bancaria"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidNombreCuenta(eAccionSR valAction, string valNombreCuenta) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
				return true;
			}
			valNombreCuenta = LibString.Trim(valNombreCuenta);
			if (LibString.IsNullOrEmpty(valNombreCuenta, true)) {
				BuildValidationInfo(MsgRequiredField("Nombre Cuenta Bancaria"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidCodigoBanco(eAccionSR valAction, int valCodigoBanco) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
				return true;
			}
			if (valCodigoBanco < 0) {
				BuildValidationInfo(MsgRequiredField("Código del Banco"));
				vResult = false;
			}
			return vResult;
		}

		private bool IsValidNombreDeLaMoneda(eAccionSR valAction, string valNombreDeLaMoneda) {
			bool vResult = true;
			if ((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
				return true;
			}
			valNombreDeLaMoneda = LibString.Trim(valNombreDeLaMoneda);
			if (LibString.IsNullOrEmpty(valNombreDeLaMoneda, true)) {
				BuildValidationInfo(MsgRequiredField("Nombre de la Moneda"));
				vResult = false;
			}
			return vResult;
		}

		public bool KeyExists(int valConsecutivoCompania, string valCodigo) {
			bool vResult = false;
			CuentaBancaria vRecordBusqueda = new CuentaBancaria();
			vRecordBusqueda.ConsecutivoCompania = valConsecutivoCompania;
			vRecordBusqueda.Codigo = valCodigo;
			LibDatabase insDb = new LibDatabase();
			vResult = insDb.ExistsRecord("Saw.CuentaBancaria", "ConsecutivoCompania", ParametrosClave(vRecordBusqueda, false, false));
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

		#region Metodos Creados
		public bool ActualizaSaldoDisponibleEnCuenta(int valConsecutivoCompania, string valCodigoCuenta, string valMonto, string valIngresoEgreso, int valmAction, string valMontoOriginal, bool valSeModificoTipoConcepto) {
			bool vResult = false;
			LibDataScope insDb = new LibDataScope();
			vResult = insDb.ExecSpNonQueryWithScope(insDb.ToSpName(DbSchema, "CuentaBancariaActualizaSaldoDisponible"), ParametrosActualizaSaldoDisponible(valConsecutivoCompania, valCodigoCuenta, valMonto, valIngresoEgreso, valmAction, valMontoOriginal, valSeModificoTipoConcepto));
			insDb.Dispose();
			return vResult;
		}

		public bool RecalculaSaldoCuentasBancarias(int valConsecutivoCompania) {
			bool vResult = false;
			LibDatabase insDb = new LibDatabase();
			vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName(DbSchema, "CuentaBancariaRecalculaSaldo"), ParametroCompania(valConsecutivoCompania));
			insDb.Dispose();
			return vResult;
		}

		public bool ExisteTablaSettValueByCompany() {
			return new LibDbo(LibCkn.ConfigKeyForDbService).Exists("Comun.SettValueByCompany", eDboType.Tabla);
		}

		public XElement MovimientosBancariosDeReposicionCajaChica(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
			XElement vResultset;
			QAdvSql insQAdvSql = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();

			string vWhere = insQAdvSql.SqlIntValueWithAnd("", "dbo.MovimientoBancario.ConsecutivoCompania", valConsecutivoCompania);
			vWhere = insQAdvSql.SqlEnumValueWithAnd(vWhere, "dbo.MovimientoBancario.GeneradoPor", (int) Galac.Adm.Ccl.Banco.eGeneradoPor.ReposicionDeCajaChica);
			vWhere = insQAdvSql.SqlValueWithAnd(vWhere, "CodigoCtaBancaria", valCodigoCuentaBancaria);
			vWhere = insQAdvSql.WhereSql(vWhere);
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("    CodigoCtaBancaria ");
			vSql.AppendLine("FROM dbo.MovimientoBancario ");
			vSql.AppendLine("INNER JOIN ");
			vSql.AppendLine("	Adm.Rendicion ON  Adm.Rendicion.ConsecutivoCompania = dbo.MovimientoBancario.ConsecutivoCompania ");
			vSql.AppendLine("	AND   Adm.Rendicion.CodigoCtaBancariaCajaChica  = dbo.MovimientoBancario.CodigoCtaBancaria  ");
			vSql.AppendLine(vWhere);
			vResultset = ((ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>) this).QueryInfo(eProcessMessageType.Query, "", vSql);
			return vResultset;
		}

		public XElement EsValidaCuentaBancariaCajaChica(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
			XElement vResultset;
			QAdvSql insQAdvSql = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();

			string vWhere = insQAdvSql.SqlIntValueWithAnd("", "ConsecutivoCompania", valConsecutivoCompania);
			vWhere = insQAdvSql.SqlBoolValueWithOperators(vWhere, "EsCajaChica", true, "AND", "=");
			vWhere = insQAdvSql.SqlValueWithAnd(vWhere, "Codigo", valCodigoCuentaBancaria);
			vWhere = insQAdvSql.WhereSql(vWhere);
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("    EsCajaChica ");
			vSql.AppendLine("FROM Saw.CuentaBancaria ");
			vSql.AppendLine(vWhere);
			vResultset = ((ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>) this).QueryInfo(eProcessMessageType.Query, "", vSql);
			return vResultset;
		}

		public XElement MovimientosBancariosPorCuentaBancaria(int valConsecutivoCompania) {
			XElement vResultset;
			QAdvSql insQAdvSql = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();

			string vWhere = insQAdvSql.SqlIntValueWithAnd("", "ConsecutivoCompania", valConsecutivoCompania);
			//vWhere = insQAdvSql.SqlValueWithAnd(vWhere, "CodigoCtaBancaria", valCodigoCuentaBancaria);
			vWhere = insQAdvSql.WhereSql(vWhere);
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("    CodigoCtaBancaria ");
			vSql.AppendLine("FROM dbo.MovimientoBancario ");
			vSql.AppendLine(vWhere);
			vResultset = ((ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>>) this).QueryInfo(eProcessMessageType.Query, "", vSql);
			return vResultset;
		}
		#endregion

		#region //Miembros de ILibDataRpt
		System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
			return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
		}

		System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
			return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
		}
		#endregion ////Miembros de ILibDataRpt

		#endregion //Metodos Generados

	} //End of class clsCuentaBancariaDat

} //End of namespace Galac.Adm.Dal.Banco

