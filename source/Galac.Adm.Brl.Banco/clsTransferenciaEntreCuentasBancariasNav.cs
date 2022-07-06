using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Banco;
using System.Transactions;

namespace Galac.Adm.Brl.Banco {
	public partial class clsTransferenciaEntreCuentasBancariasNav : LibBaseNav<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>, ITransferenciaEntreCuentasBancariasPdn {
		#region Constructores
		public clsTransferenciaEntreCuentasBancariasNav() {
		}
		#endregion //Constructores

		#region Metodos Generados
		protected override ILibDataComponentWithSearch<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>> GetDataInstance() {
			return new Dal.Banco.clsTransferenciaEntreCuentasBancariasDat();
		}

		#region Miembros de ILibPdn
		bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
			bool vResult = false;
			ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasDat();
			switch (valCallingModule) {
				default:
					vResult = true;
					break;
			}
			return vResult;
		}

		bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
			ILibDataFKSearch instanciaDal = new Dal.Banco.clsTransferenciaEntreCuentasBancariasDat();
			return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_TransferenciaEntreCuentasBancariasSCH", valXmlParamsExpression);
		}

		XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
			ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>> instanciaDal = new Dal.Banco.clsTransferenciaEntreCuentasBancariasDat();
			return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_TransferenciaEntreCuentasBancariasGetFk", valParameters);
		}
		#endregion //Miembros de ILibPdn

		protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
			bool vResult = false;
			ILibPdn vPdnModule;
			switch (valModule) {
				case "Transferencia entre Cuentas Bancarias":
					vResult = ((ILibPdn) this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
					break;
				case "Cuenta Bancaria":
					vPdnModule = new clsCuentaBancariaNav();
					vResult = vPdnModule.GetDataForList("Transferencia entre Cuentas Bancarias", ref refXmlDocument, valXmlParamsExpression);
					break;
				case "Concepto Bancario":
					vPdnModule = new clsConceptoBancarioNav();
					vResult = vPdnModule.GetDataForList("Transferencia entre Cuentas Bancarias", ref refXmlDocument, valXmlParamsExpression);
					break;
				case "Moneda":
					vPdnModule = new Comun.Brl.TablasGen.clsMonedaNav();
					vResult = vPdnModule.GetDataForList("Compra", ref refXmlDocument, valXmlParamsExpression);
					break;
				default: throw new NotImplementedException();
			}
			return vResult;
		}

		XElement ITransferenciaEntreCuentasBancariasPdn.FindByConsecutivoCompaniaNumeroDocumentoCodigoCuentaBancariaOrigen(int valConsecutivoCompania, string valNumeroDocumento, string valCodigoCuentaBancariaOrigen) {
			LibGpParams vParams = new LibGpParams();
			vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
			vParams.AddInString("NumeroDocumento", valNumeroDocumento, 20);
			vParams.AddInString("CodigoCuentaBancariaOrigen", valCodigoCuentaBancariaOrigen, 5);
			StringBuilder SQL = new StringBuilder();
			SQL.AppendLine("SELECT * FROM Adm.TransferenciaEntreCuentasBancarias");
			SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
			SQL.AppendLine("AND NumeroDocumento = @NumeroDocumento");
			SQL.AppendLine("AND CodigoCuentaBancariaOrigen = @CodigoCuentaBancariaOrigen");
			return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
		}
		#endregion //Metodos Generados

		#region Código Programador
		protected override LibResponse InsertRecord(IList<TransferenciaEntreCuentasBancarias> refRecord) {
			LibResponse vResult = new LibResponse();
			using (TransactionScope vScope = LibBusiness.CreateScope()) {
				vResult = base.InsertRecord(refRecord);
				if (vResult.Success) {
					vResult = GenerarMovimientosBancariosAsociados(refRecord);
				}
				if (vResult.Success) {
					vScope.Complete();
				}
			}
			return vResult;
		}

		private LibResponse GenerarMovimientosBancariosAsociados(IList<TransferenciaEntreCuentasBancarias> refRecord) {
			LibResponse vResult = new LibResponse();
			IMovimientoBancarioPdn vMovimientoBancarioPdn = new clsMovimientoBancarioNav();
			List<MovimientoBancario> vList = new List<MovimientoBancario>();
			decimal vAlicuota;
			ICuentaBancariaPdn vCuentaBancariaPdn = new clsCuentaBancariaNav();
			foreach (TransferenciaEntreCuentasBancarias item in refRecord) {
				vAlicuota = vCuentaBancariaPdn.ObtieneAlicuotaIGTF(item.ConsecutivoCompania, item.CodigoCuentaBancariaOrigen, item.Fecha);
				vList.Add(GenerarMovimientoBancarioEgreso(item, vAlicuota));
				if (item.GeneraImpuestoBancarioEgresoAsBool && vCuentaBancariaPdn.GeneraMovimientoDeITF(item.ConsecutivoCompania, item.CodigoCuentaBancariaOrigen) && vAlicuota > 0.0m) {
					vList.Add(GenerarMovimientoBancarioImpuestoEgreso(item, vAlicuota / 100.0m));
				}
				if (item.GeneraComisionEgresoAsBool) {
					vList.Add(GenerarMovimientoBancarioComisionEgreso(item));
				}

				vAlicuota = vCuentaBancariaPdn.ObtieneAlicuotaIGTF(item.ConsecutivoCompania, item.CodigoCuentaBancariaDestino, item.Fecha);
				vList.Add(GenerarMovimientoBancarioIngreso(item, vAlicuota));
				if (item.GeneraImpuestoBancarioIngresoAsBool && vCuentaBancariaPdn.GeneraMovimientoDeITF(item.ConsecutivoCompania, item.CodigoCuentaBancariaOrigen) && vAlicuota > 0.0m) {
					vList.Add(GenerarMovimientoBancarioImpuestoIngreso(item, vAlicuota / 100.0m));
				}
				if (item.GeneraComisionIngresoAsBool) {
					vList.Add(GenerarMovimientoBancarioComisionIngreso(item));
				}
			}
			if (vResult.Success = vMovimientoBancarioPdn.Insert(vList)) {
				vCuentaBancariaPdn.RecalculaSaldoCuentasBancarias(vList[0].ConsecutivoCompania);
			}
			return vResult;
		}

		private MovimientoBancario GenerarMovimientoBancarioEgreso(TransferenciaEntreCuentasBancarias item, decimal valAlicuota) {
			return new MovimientoBancario() {
				ConsecutivoCompania = item.ConsecutivoCompania,
				CodigoCtaBancaria = item.CodigoCuentaBancariaOrigen,
				CodigoConcepto = item.CodigoConceptoEgreso,
				Fecha = item.Fecha,
				TipoConceptoAsEnum = eIngresoEgreso.Egreso,
				Monto = item.MontoTransferenciaEgreso,
				NumeroDocumento = LibConvert.ToStr(item.Consecutivo),
				Descripcion = item.Descripcion,
				GeneraImpuestoBancarioAsBool = item.GeneraImpuestoBancarioEgresoAsBool,
				AlicuotaImpBancario = item.GeneraImpuestoBancarioEgresoAsBool ? valAlicuota : 0.0m,
				GeneradoPorAsEnum = eGeneradoPor.TransferenciaBancaria,
				CambioABolivares = item.CambioABolivaresEgreso,
				ImprimirChequeAsBool = false,
				ConciliadoSNAsBool = false,
				NroConciliacion = string.Empty,
				GenerarAsientoDeRetiroEnCuentaAsBool = false,
				NombreOperador = item.NombreOperador,
				FechaUltimaModificacion = LibDate.Today(),
			};
		}

		private MovimientoBancario GenerarMovimientoBancarioIngreso(TransferenciaEntreCuentasBancarias item, decimal valAlicuota) {
			return new MovimientoBancario() {
				ConsecutivoCompania = item.ConsecutivoCompania,
				CodigoCtaBancaria = item.CodigoCuentaBancariaDestino,
				CodigoConcepto = item.CodigoConceptoIngreso,
				Fecha = item.Fecha,
				TipoConceptoAsEnum = eIngresoEgreso.Ingreso,
				Monto = item.MontoTransferenciaIngreso,
				NumeroDocumento = LibConvert.ToStr(item.Consecutivo),
				Descripcion = item.Descripcion,
				GeneraImpuestoBancarioAsBool = item.GeneraImpuestoBancarioIngresoAsBool,
				AlicuotaImpBancario = item.GeneraImpuestoBancarioIngresoAsBool ? valAlicuota : 0.0m,
				GeneradoPorAsEnum = eGeneradoPor.TransferenciaBancaria,
				CambioABolivares = item.CambioABolivaresIngreso,
				ImprimirChequeAsBool = false,
				ConciliadoSNAsBool = false,
				NroConciliacion = string.Empty,
				GenerarAsientoDeRetiroEnCuentaAsBool = false,
				NombreOperador = item.NombreOperador,
				FechaUltimaModificacion = LibDate.Today(),
			};
		}

		private MovimientoBancario GenerarMovimientoBancarioImpuestoEgreso(TransferenciaEntreCuentasBancarias item, decimal valAlicuota) {
			return new MovimientoBancario() {
				ConsecutivoCompania = item.ConsecutivoCompania,
				CodigoCtaBancaria = item.CodigoCuentaBancariaOrigen,
				CodigoConcepto = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoDebitoBancario"),
				Fecha = item.Fecha,
				TipoConceptoAsEnum = eIngresoEgreso.Egreso,
				Monto = item.MontoTransferenciaEgreso * valAlicuota,
				NumeroDocumento = LibConvert.ToStr(item.Consecutivo),
				Descripcion = string.Format("Impuesto por egreso transferencia - {0}", LibConvert.ToStr(item.Consecutivo)),
				GeneraImpuestoBancarioAsBool = false,
				AlicuotaImpBancario = 0,
				GeneradoPorAsEnum = eGeneradoPor.TransferenciaBancaria,
				CambioABolivares = item.CambioABolivaresEgreso,
				ImprimirChequeAsBool = false,
				ConciliadoSNAsBool = false,
				NroConciliacion = string.Empty,
				GenerarAsientoDeRetiroEnCuentaAsBool = false,
				NombreOperador = item.NombreOperador,
				FechaUltimaModificacion = LibDate.Today(),
			};
		}

		private MovimientoBancario GenerarMovimientoBancarioImpuestoIngreso(TransferenciaEntreCuentasBancarias item, decimal valAlicuota) {
			return new MovimientoBancario() {
				ConsecutivoCompania = item.ConsecutivoCompania,
				CodigoCtaBancaria = item.CodigoCuentaBancariaDestino,
				CodigoConcepto = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ConceptoCreditoBancario"),
				Fecha = item.Fecha,
				TipoConceptoAsEnum = eIngresoEgreso.Egreso,
				Monto = item.MontoTransferenciaIngreso * valAlicuota,
				NumeroDocumento = LibConvert.ToStr(item.Consecutivo),
				Descripcion = string.Format("Impuesto por ingreso transferencia - {0}", LibConvert.ToStr(item.Consecutivo)),
				GeneraImpuestoBancarioAsBool = false,
				AlicuotaImpBancario = 0,
				GeneradoPorAsEnum = eGeneradoPor.TransferenciaBancaria,
				CambioABolivares = item.CambioABolivaresIngreso,
				ImprimirChequeAsBool = false,
				ConciliadoSNAsBool = false,
				NroConciliacion = string.Empty,
				GenerarAsientoDeRetiroEnCuentaAsBool = false,
				NombreOperador = item.NombreOperador,
				FechaUltimaModificacion = LibDate.Today(),
			};
		}

		private MovimientoBancario GenerarMovimientoBancarioComisionEgreso(TransferenciaEntreCuentasBancarias item) {
			return new MovimientoBancario() {
				ConsecutivoCompania = item.ConsecutivoCompania,
				CodigoCtaBancaria = item.CodigoCuentaBancariaOrigen,
				CodigoConcepto = item.CodigoConceptoComisionEgreso,
				Fecha = item.Fecha,
				TipoConceptoAsEnum = eIngresoEgreso.Egreso,
				Monto = item.MontoComisionEgreso,
				NumeroDocumento = LibConvert.ToStr(item.Consecutivo),
				Descripcion = string.Format("Comisión por egreso transferencia - {0}", LibConvert.ToStr(item.Consecutivo)),
				GeneraImpuestoBancarioAsBool = false,
				AlicuotaImpBancario = 0,
				GeneradoPorAsEnum = eGeneradoPor.TransferenciaBancaria,
				CambioABolivares = item.CambioABolivaresEgreso,
				ImprimirChequeAsBool = false,
				ConciliadoSNAsBool = false,
				NroConciliacion = string.Empty,
				GenerarAsientoDeRetiroEnCuentaAsBool = false,
				NombreOperador = item.NombreOperador,
				FechaUltimaModificacion = LibDate.Today(),
			};
		}

		private MovimientoBancario GenerarMovimientoBancarioComisionIngreso(TransferenciaEntreCuentasBancarias item) {
			return new MovimientoBancario() {
				ConsecutivoCompania = item.ConsecutivoCompania,
				CodigoCtaBancaria = item.CodigoCuentaBancariaDestino,
				CodigoConcepto = item.CodigoConceptoComisionIngreso,
				Fecha = item.Fecha,
				TipoConceptoAsEnum = eIngresoEgreso.Egreso,
				Monto = item.MontoComisionIngreso,
				NumeroDocumento = LibConvert.ToStr(item.Consecutivo),
				Descripcion = string.Format("Comisión por ingreso transferencia - {0}", LibConvert.ToStr(item.Consecutivo)),
				GeneraImpuestoBancarioAsBool = false,
				AlicuotaImpBancario = 0,
				GeneradoPorAsEnum = eGeneradoPor.TransferenciaBancaria,
				CambioABolivares = item.CambioABolivaresIngreso,
				ImprimirChequeAsBool = false,
				ConciliadoSNAsBool = false,
				NroConciliacion = string.Empty,
				GenerarAsientoDeRetiroEnCuentaAsBool = false,
				NombreOperador = item.NombreOperador,
				FechaUltimaModificacion = LibDate.Today(),
			};
		}

		bool ITransferenciaEntreCuentasBancariasPdn.CambiarStatusTransferencia(TransferenciaEntreCuentasBancarias valTransferencia, eAccionSR valAction) {
			RegisterClient();
			IList<TransferenciaEntreCuentasBancarias> vList = new List<TransferenciaEntreCuentasBancarias>();
			vList.Add(valTransferencia);
			LibResponse vResult;
			using (TransactionScope vScope = LibBusiness.CreateScope()) {
				vResult = ((ILibDataComponent<IList<TransferenciaEntreCuentasBancarias>, IList<TransferenciaEntreCuentasBancarias>>) new Dal.Banco.clsTransferenciaEntreCuentasBancariasDat()).CanBeChoosen(vList, eAccionSR.Anular);
				if (vResult.Success) {
					vResult = base.UpdateRecord(vList);
				}
				if (vResult.Success) {
					vResult = AnularMovimientosBancariosAsociados(valTransferencia);
				}
				if (vResult.Success) {
					((ICuentaBancariaPdn) new clsCuentaBancariaNav()).RecalculaSaldoCuentasBancarias(vList[0].ConsecutivoCompania);
					vScope.Complete();
				}
			}
			return vResult.Success;
		}

		private LibResponse AnularMovimientosBancariosAsociados(TransferenciaEntreCuentasBancarias refRecord) {
			LibResponse vResult = new LibResponse();
			vResult.Success = ((IMovimientoBancarioPdn) new clsMovimientoBancarioNav()).AnularMovimientosBancarios(refRecord.ConsecutivoCompania, LibConvert.ToStr(refRecord.Consecutivo), eGeneradoPor.TransferenciaBancaria);
			return vResult;
		}
		#endregion //Codigo Ejemplo

	} //End of class clsTransferenciaEntreCuentasBancariasNav

} //End of namespace Galac.Adm.Brl.Banco

