using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Linq;
using Galac.Adm.Ccl.Banco;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.Banco {
	public partial class clsCuentaBancariaNav : LibBaseNav<IList<CuentaBancaria>, IList<CuentaBancaria>>, ICuentaBancariaPdn {
		#region Variables
		#endregion //Variables

		#region Propiedades
		#endregion //Propiedades

		#region Constructores
		public clsCuentaBancariaNav() {
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override ILibDataComponentWithSearch<IList<CuentaBancaria>, IList<CuentaBancaria>> GetDataInstance() {
			return new Dal.Banco.clsCuentaBancariaDat();
		}

		#region Miembros de ILibPdn
		bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
			bool vResult = false;
			ILibDataFKSearch instanciaDal = new Dal.Banco.clsCuentaBancariaDat();
			switch (valCallingModule) {
				default:
					vResult = true;
					break;
			}
			return vResult;
		}

		bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
			ILibDataFKSearch instanciaDal = new Dal.Banco.clsCuentaBancariaDat();
			return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_CuentaBancariaSCH", valXmlParamsExpression);
		}

		XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
			ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal = new Dal.Banco.clsCuentaBancariaDat();
			return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_CuentaBancariaGetFk", valParameters);
		}
		#endregion //Miembros de ILibPdn

		protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
			bool vResult = false;
			ILibPdn vPdnModule;
			switch (valModule) {
				case "Cuenta Bancaria":
					vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
					break;
				case "Banco":
					vPdnModule = new Comun.Brl.TablasGen.clsBancoNav();
					vResult = vPdnModule.GetDataForList("Cuenta Bancaria", ref refXmlDocument, valXmlParamsExpression);
					break;
				case "Moneda":
					vPdnModule = new Comun.Brl.TablasGen.clsMonedaNav();
					vResult = vPdnModule.GetDataForList("Cuenta Bancaria", ref refXmlDocument, valXmlParamsExpression);
					break;
				case "Cuenta":
					vPdnModule = new Contab.Brl.WinCont.clsCuentaNav();
					vResult = vPdnModule.GetDataForList("Cuenta Bancaria", ref refXmlDocument, valXmlParamsExpression);
					break;
				default: throw new NotImplementedException();
			}
			return vResult;
		}
		#endregion //Metodos Generados

		#region Código Programador
		private bool ExisteTablaSettValueByCompany() {
			return new Dal.Banco.clsBeneficiarioDat().ExisteTablaSettValueByCompany();
		}

		XElement GetPametrosCompaniaParaInsertarCuentaBancariaGenericaSiCiaNoGeneraMovimientosBancarios(int valConsecutivoCompania, ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal) {
			StringBuilder sql;
			string vDbSchema = "";
			if (ExisteTablaSettValueByCompany()) {
				if (LibDefGen.ProgramInfo.IsCountryEcuador()) {
					vDbSchema = "Adme";
				} else {
					vDbSchema = "Comun";
				}
				sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS CodigoGenericoCuentaBancaria," + vDbSchema + ".SettValueByCompany.NameSettDefinition FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE     (" + vDbSchema + ".SettDefinition.Name = 'CodigoGenericoCuentaBancaria') AND (" + vDbSchema + ".SettValueByCompany.ConsecutivoCompania = " + valConsecutivoCompania + ")", 300);
			} else {
				sql = new StringBuilder("SELECT CodigoGenericoCuentaBancaria FROM ParametrosCompania WHERE ConsecutivoCompania = " + valConsecutivoCompania, 200);
			}
			return instanciaDal.QueryInfo(eProcessMessageType.Query, null, sql);
		}

		XElement GetPametrosAdministrativoCodigoMonedaLocal(int valConsecutivoCompania, ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal) {
			StringBuilder sql;
			string vDbSchema = "";
			if (ExisteTablaSettValueByCompany()) {
				if (LibDefGen.ProgramInfo.IsCountryEcuador()) {
					vDbSchema = "Adme";
				} else {
					vDbSchema = "Comun";
				}
				sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS CodigoMonedaLocal," + vDbSchema + ".SettValueByCompany.NameSettDefinition FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE     (" + vDbSchema + ".SettDefinition.Name = 'CodigoMonedaLocal') AND (" + vDbSchema + ".SettValueByCompany.ConsecutivoCompania = " + valConsecutivoCompania + ")", 300);
			} else {
				sql = new StringBuilder("SELECT CodigoMonedaLocal FROM ParametrosAdministrativo");
			}
			return instanciaDal.QueryInfo(eProcessMessageType.Query, null, sql);
		}

		XElement GetPametrosAdministrativoNombreMonedaLocal(int valConsecutivoCompania, ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal) {
			StringBuilder sql;
			string vDbSchema = "";
			if (ExisteTablaSettValueByCompany()) {
				if (LibDefGen.ProgramInfo.IsCountryEcuador()) {
					vDbSchema = "Adme";
				} else {
					vDbSchema = "Comun";
				}
				sql = new StringBuilder("SELECT " + vDbSchema + ".SettValueByCompany.Value AS NombreMonedaLocal," + vDbSchema + ".SettValueByCompany.NameSettDefinition FROM " + vDbSchema + ".SettDefinition INNER JOIN " + vDbSchema + ".SettValueByCompany ON " + vDbSchema + ".SettDefinition.Name = " + vDbSchema + ".SettValueByCompany.NameSettDefinition WHERE     (" + vDbSchema + ".SettDefinition.Name = 'NombreMonedaLocal') AND (" + vDbSchema + ".SettValueByCompany.ConsecutivoCompania = " + valConsecutivoCompania + ")", 300);
			} else {
				sql = new StringBuilder("SELECT NombreMonedaLocal FROM ParametrosAdministrativo");
			}
			return instanciaDal.QueryInfo(eProcessMessageType.Query, null, sql);
		}

		private StringBuilder ParametroCompania(int valConsecutivoCompania) {
			StringBuilder vResult = new StringBuilder();
			LibGpParams vParams = new LibGpParams();
			vParams.AddReturn();
			vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
			vResult = vParams.Get();
			return vResult;
		}

		string GetNextCodigo(int valConsecutivoCompania, ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal) {
			XElement vResult;
			string vNumero;
			vResult = instanciaDal.QueryInfo(eProcessMessageType.Message, "ProximoNumero", ParametroCompania(valConsecutivoCompania));
			vNumero = LibXml.GetPropertyString(vResult, "Codigo");
			return vNumero;
		}

		bool ExisteCuentaBancaria(int valConsecutivoCompania, string valCodigoCuenta) {
			Dal.Banco.clsCuentaBancariaDat insDal = new Dal.Banco.clsCuentaBancariaDat();
			return insDal.KeyExists(valConsecutivoCompania, valCodigoCuenta);
		}

		void ICuentaBancariaPdn.GeneraCuentaBancariaGenericaSiCiaNoGeneraMovimientosBancarios(int valConsecutivoCompania, int vCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal) {
			ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal = new Galac.Adm.Dal.Banco.clsCuentaBancariaDat();
			IList<CuentaBancaria> vLista = new List<CuentaBancaria>();
			CuentaBancaria vCurrentRecord = new CuentaBancaria();

			string vCodigoGenericoCuentaBancaria = CuentaBancariaGenericaPorDefecto();
			vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
			if (vCodigoGenericoCuentaBancaria == "") {
				vCurrentRecord.Codigo = GetNextCodigo(valConsecutivoCompania, instanciaDal);
			} else {
				if (ExisteCuentaBancaria(valConsecutivoCompania, vCodigoGenericoCuentaBancaria)) {
					vCurrentRecord.Codigo = vCodigoGenericoCuentaBancaria;
				} else {
					vCurrentRecord.Codigo = GetNextCodigo(valConsecutivoCompania, instanciaDal);
				}
			}
			string vCodigoMonedaLocal = valCodigoMonedaLocal;
			string vNombreMonedaLocal = valNombreMonedaLocal;
			vCurrentRecord.CodigoBanco = vCodigoBanco;
			vCurrentRecord.StatusAsEnum = eStatusCtaBancaria.Activo;
			vCurrentRecord.NumeroCuenta = "00001";
			vCurrentRecord.NombreCuenta = "CUENTA BANCARIA GENERICA DE AJUSTE";
			vCurrentRecord.NombreSucursal = "";
			vCurrentRecord.TipoCtaBancariaAsEnum = eTipoDeCtaBancaria.Ahorros;
			vCurrentRecord.ManejaCreditoBancarioAsBool = false;
			vCurrentRecord.ManejaDebitoBancarioAsBool = false;
			vCurrentRecord.SaldoDisponible = 0;
			vCurrentRecord.CodigoMoneda = vCodigoMonedaLocal;
			vCurrentRecord.NombreDeLaMoneda = vNombreMonedaLocal;
			vCurrentRecord.CuentaContable = "";
			vCurrentRecord.NombrePlantillaCheque = "";
			vCurrentRecord.TipoDeAlicuotaPorContribuyenteAsEnum = eTipoAlicPorContIGTF.NoAsignado;
			vCurrentRecord.GeneraMovBancarioPorIGTF = LibConvert.BoolToSN(false);
			vLista.Add(vCurrentRecord);
			instanciaDal.Insert(vLista);
		}

		void ICuentaBancariaPdn.InsertaCuentaBancariaGenericaSiHaceFalta(int valConsecutivoCompania, int vCodigoBanco, string valCodigoMonedaLocal, string valNombreMonedaLocal) {
			ILibDataComponent<IList<CuentaBancaria>, IList<CuentaBancaria>> instanciaDal = new Galac.Adm.Dal.Banco.clsCuentaBancariaDat();
			IList<CuentaBancaria> vLista = new List<CuentaBancaria>();
			CuentaBancaria vCurrentRecord = new CuentaBancaria();
			XElement vParamResultAdm = GetPametrosAdministrativoCodigoMonedaLocal(valConsecutivoCompania, instanciaDal);
			string vCodigoMonedaLocal = LibXml.GetPropertyString(vParamResultAdm, "CodigoMonedaLocal");
			vParamResultAdm = GetPametrosAdministrativoNombreMonedaLocal(valConsecutivoCompania, instanciaDal);
			string vNombreMonedaLocal = LibXml.GetPropertyString(vParamResultAdm, "NombreMonedaLocal");
			if (vCodigoMonedaLocal == "") { vCodigoMonedaLocal = valCodigoMonedaLocal; }
			if (vNombreMonedaLocal == "") { vNombreMonedaLocal = valNombreMonedaLocal; }
			vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
			vCurrentRecord.Codigo = "00000";
			vCurrentRecord.CodigoBanco = vCodigoBanco;
			vCurrentRecord.StatusAsEnum = eStatusCtaBancaria.Activo;
			vCurrentRecord.NumeroCuenta = "CUENTA BANCARIA GENÉRICA";
			vCurrentRecord.NombreCuenta = "CAJA PRINCIPAL";
			vCurrentRecord.NombreSucursal = "";
			vCurrentRecord.TipoCtaBancariaAsEnum = eTipoDeCtaBancaria.Corriente;
			vCurrentRecord.ManejaCreditoBancarioAsBool = false;
			vCurrentRecord.ManejaDebitoBancarioAsBool = false;
			vCurrentRecord.SaldoDisponible = 0;
			vCurrentRecord.CodigoMoneda = vCodigoMonedaLocal;
			vCurrentRecord.NombreDeLaMoneda = vNombreMonedaLocal;
			vCurrentRecord.CuentaContable = "";
			vCurrentRecord.NombrePlantillaCheque = "rpxChequeGenerico";
			vCurrentRecord.TipoDeAlicuotaPorContribuyenteAsEnum = eTipoAlicPorContIGTF.NoAsignado;
			vCurrentRecord.GeneraMovBancarioPorIGTF = LibConvert.BoolToSN(false);
			vLista.Add(vCurrentRecord);
			instanciaDal.Insert(vLista);
		}

		bool ICuentaBancariaPdn.ActualizaSaldoDisponibleEnCuenta(int valConsecutivoCompania, string valCodigoCuenta, string valMonto, string valIngresoEgreso, int valmAction, string valMontoOriginal, bool valSeModificoTipoConcepto) {
			bool vResult;
			vResult = false;
			vResult = new Dal.Banco.clsCuentaBancariaDat().ActualizaSaldoDisponibleEnCuenta(valConsecutivoCompania, valCodigoCuenta, valMonto, valIngresoEgreso, valmAction, valMontoOriginal, valSeModificoTipoConcepto);
			return vResult;
		}

		bool ICuentaBancariaPdn.RecalculaSaldoCuentasBancarias(int valConsecutivoCompania) {
			bool vResult;
			vResult = false;
			vResult = new Dal.Banco.clsCuentaBancariaDat().RecalculaSaldoCuentasBancarias(valConsecutivoCompania);
			return vResult;
		}

		string ICuentaBancariaPdn.GetCuentaBancariaGenericaPorDefecto() {
			string vResult = "";
			vResult = CuentaBancariaGenericaPorDefecto();
			return vResult;
		}

		string CuentaBancariaGenericaPorDefecto() {
			return "00000";
		}

		bool ICuentaBancariaPdn.TieneMovimientosBancariosDeReposicionCajaChica(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
			int vResult;
			XElement vResultset;
			vResultset = new Dal.Banco.clsCuentaBancariaDat().MovimientosBancariosDeReposicionCajaChica(valConsecutivoCompania, valCodigoCuentaBancaria);
			if (vResultset != null) {
				vResult = (from vRecord in vResultset.Descendants("GpResult")
						   select vRecord).Count();
			} else {
				vResult = 0;
			}
			return vResult > 0;

		}

		bool ICuentaBancariaPdn.EsValidaCuentaBancariaCajaChica(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
			int vResult;
			XElement vResultset;
			vResultset = new Dal.Banco.clsCuentaBancariaDat().EsValidaCuentaBancariaCajaChica(valConsecutivoCompania, valCodigoCuentaBancaria);
			if (vResultset != null) {
				vResult = (from vRecord in vResultset.Descendants("GpResult")
						   select vRecord).Count();
			} else {
				vResult = 0;
			}
			return vResult > 0;

		}

		bool ICuentaBancariaPdn.ExistenMovimientosCuentaBancaria(int valConsecutivoCompania) {
			int vResult;
			XElement vResultset;
			vResultset = new Dal.Banco.clsCuentaBancariaDat().MovimientosBancariosPorCuentaBancaria(valConsecutivoCompania);
			if (vResultset != null) {
				vResult = (from vRecord in vResultset.Descendants("GpResult")
						   select vRecord).Count();
			} else {
				vResult = 0;
			}
			return vResult > 0;
		}

		bool ICuentaBancariaPdn.ExistenMovimientosPorCuentaBancariaPosterioresAUnaFecha(int valConsecutivoCompania, string valCodigoCuentaBancaria, DateTime valFecha) {
			int vResult;
			XElement vResultset;
			vResultset = new Dal.Banco.clsCuentaBancariaDat().MovimientosBancariosPorCuentaBancariaPosterioresAUnaFecha(valConsecutivoCompania, valCodigoCuentaBancaria, valFecha);
			if (vResultset == null) {
				vResult = 0;
			} else {
				vResult = (from vRecord in vResultset.Descendants("GpResult") select vRecord).Count();
			}
			return vResult > 0;
		}

		bool ICuentaBancariaPdn.ExistenMovimientosPorCuentaBancariaPosterioresAReformaIGTFGO6687ConIGTFMarcado(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
			int vResult;
			XElement vResultset;
			vResultset = new Dal.Banco.clsCuentaBancariaDat().MovimientosBancariosPorCuentaBancariaPosterioresAReformaIGTFGO6687ConIGTFMarcado(valConsecutivoCompania, valCodigoCuentaBancaria);
			if (vResultset == null) {
				vResult = 0;
			} else {
				vResult = (from vRecord in vResultset.Descendants("GpResult") select vRecord).Count();
			}
			return vResult > 0;
		}

		decimal ICuentaBancariaPdn.ObtieneAlicuotaIGTF(int valConsecutivoCompania, string valCodigoCuentaBancaria, DateTime valFechaMovimiento) {
			decimal vResult = 0;
			if (LibDate.F1IsGreaterOrEqualThanF2(valFechaMovimiento, Ccl.Banco.LibBanco.FechaReformaIGTFGO6687)) {
				vResult = GetAlicuotaImpuestoTransaccionesGO6687(valConsecutivoCompania, valCodigoCuentaBancaria, valFechaMovimiento);
			} else {
				vResult = GetAlicuotaImpuestoTransacciones(valFechaMovimiento);
			}
			return vResult;
		}

		private decimal GetAlicuotaImpuestoTransacciones(DateTime valFecha) {
			decimal vResult = 0;
			LibGpParams vParams = new LibGpParams();
			vParams.AddInDateTime("FechaDeInicioDeVigencia", valFecha);
			RegisterClient();
			string vSql = "SELECT TOP(1) AlicuotaAlDebito FROM dbo.imptransacBancarias WHERE FechaDeInicioDeVigencia <= @FechaDeInicioDeVigencia Order by FechaDeInicioDeVigencia Desc";
			XElement vResultset = LibBusiness.ExecuteSelect(vSql, vParams.Get(), "", 0);
			if (vResultset != null) {
				var vEntity = from vRecord in vResultset.Descendants("GpResult")
							  select vRecord;
				foreach (XElement vItem in vEntity) {
					if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaAlDebito"), null))) {
						vResult = LibConvert.ToDec(vItem.Element("AlicuotaAlDebito"));
						break;
					}
				}
			}
			return vResult;
		}

		private decimal GetAlicuotaImpuestoTransaccionesGO6687(int valConsecutivoCompania, string valCodigoCuentaBancaria, DateTime valFechaMovimiento) {
			decimal vResult = 0;
			StringBuilder vSql = new StringBuilder();
			LibGpParams vParam = new LibGpParams();
			eTipoAlicPorContIGTF vTipoAlicuota;
			vParam.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
			vParam.AddInString("CodigoCuenta", valCodigoCuentaBancaria, 5);
			vSql.AppendLine("SELECT TipoDeAlicuotaPorContribuyente ");
			vSql.AppendLine("FROM Saw.CuentaBancaria ");
			vSql.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
			vSql.AppendLine("AND Codigo= @CodigoCuenta");
			XElement vReq = LibBusiness.ExecuteSelect(vSql.ToString(), vParam.Get(), "", 0);
			if (vReq != null && vReq.HasElements) {
				vTipoAlicuota = (eTipoAlicPorContIGTF) LibConvert.DbValueToEnum(vReq.Element("GpResult").Element("TipoDeAlicuotaPorContribuyente"));
			} else {
				vTipoAlicuota = eTipoAlicPorContIGTF.NoAsignado;
			}
			if (vTipoAlicuota == eTipoAlicPorContIGTF.NoAsignado) {
				return 0;
			}
			Saw.Brl.Tablas.clsImpuestoBancarioNav insImpBancario = new Saw.Brl.Tablas.clsImpuestoBancarioNav();
			string vAlicuota = insImpBancario.BuscaAlicuotasReformaIGTFGO6687(valFechaMovimiento, (int)vTipoAlicuota);
			vResult = LibConvert.ToDec(vAlicuota, 2);
			return vResult;
		}
		bool ICuentaBancariaPdn.GeneraMovimientoDeITF(int valConsecutivoCompania, string valCodigoCuentaBancaria) {
			return new Dal.Banco.clsCuentaBancariaDat().GeneraMovimientoDeITF(valConsecutivoCompania, valCodigoCuentaBancaria);
		}
		#endregion //Código Programador
	} //End of class clsCuentaBancariaNav
} //End of namespace Galac.Adm.Brl.Banco

