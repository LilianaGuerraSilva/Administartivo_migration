using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
//using Galac.Comun.Ccl.SttDef;
using Galac.Adm.Ccl.Banco;
//using Galac.Adm.Brl.Banco;

namespace Galac.Adm.Dal.Venta {
    public class clsMovimientoBancarioPuntoDeVentaDat : LibData {
        public string InsertGenerarMovimientoBancarioPuntoDeVenta() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("INSERT INTO " + "dbo" + ".MovimientoBancario(");
            vSql.AppendLine("ConsecutivoCompania,");
            vSql.AppendLine("ConsecutivoMovimiento,");
            vSql.AppendLine("CodigoCtaBancaria,");
            vSql.AppendLine("CodigoConcepto,");
            vSql.AppendLine("Fecha,");
            vSql.AppendLine("TipoConcepto,");
            vSql.AppendLine("Monto,");
            vSql.AppendLine("NumeroDocumento,");
            vSql.AppendLine("Descripcion,");
            vSql.AppendLine("GeneraImpuestoBancario,");
            vSql.AppendLine("NroMovimientoRelacionado,");
            vSql.AppendLine("GeneradoPor,");
            vSql.AppendLine("CambioABolivares,");
            vSql.AppendLine("ImprimirCheque,");
            vSql.AppendLine("ConciliadoSN,");
            vSql.AppendLine("NroConciliacion,");
            vSql.AppendLine("GenerarAsientoDeRetiroEnCuenta,");
            vSql.AppendLine("NombreOperador,");
            vSql.AppendLine("FechaUltimaModificacion)");
            vSql.AppendLine("VALUES(");
            vSql.AppendLine("@ConsecutivoCompania,");
            vSql.AppendLine("@ConsecutivoMovimiento,");
            vSql.AppendLine("@CodigoCtaBancaria,");
            vSql.AppendLine("@CodigoConcepto,");
            vSql.AppendLine("@Fecha,");
            vSql.AppendLine("@TipoConcepto,");
            vSql.AppendLine("@Monto,");
            vSql.AppendLine("@NumeroDocumento,");
            vSql.AppendLine("@Descripcion,");
            vSql.AppendLine("@GeneraImpuestoBancario,");
            vSql.AppendLine("@NroMovimientoRelacionado,");
            vSql.AppendLine("@GeneradoPor,");
            vSql.AppendLine("@CambioABolivares,");
            vSql.AppendLine("@ImprimirCheque,");
            vSql.AppendLine("@ConciliadoSN,");
            vSql.AppendLine("@NroConciliacion,");
            vSql.AppendLine("@GenerarAsientoDeRetiroEnCuenta,");
            vSql.AppendLine("@NombreOperador,");
            vSql.AppendLine("@FechaUltimaModificacion)");
            return vSql.ToString();
        }

        public StringBuilder ParametrosMovimientoBancarioPuntoDeVenta(int valConsecutivoCompania, int valConsecutivoMovimiento, string valNumeroCobranza, XElement valData) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            string vNumeroCXC = "";
            QAdvSql insQAdvSql = new QAdvSql("");
            decimal vTotalMovimiento = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalFactura"));
            string vCodigoCliente = LibXml.GetPropertyString(valData, "CodigoCliente");
            string vNombreCliente = LibXml.GetPropertyString(valData, "NombreCliente");
            string vCodigoVendedor = LibXml.GetPropertyString(valData, "CodigoVendedor");
            string vComprobanteFiscal = LibXml.GetPropertyString(valData, "NumeroComprobanteFiscal");
            string vNumeroFactura = LibXml.GetPropertyString(valData, "Numero");
            string vCodigoCXC = vNumeroFactura + "-" + vNumeroFactura;
            DateTime vFechaFactura = LibConvert.ToDate(LibXml.GetPropertyString(valData, "Fecha"));
            bool vGeneraImpuestoBancario = false;
            bool vImprimirCheque = false;
            bool vConciliadoSN = false;
            bool vGenerarAsientoDeRetiroEnCuenta = false;
            string vCodigoVacio = "";
            decimal vCambioABolivares = 1;
            string vNombreOperador = LibXml.GetPropertyString(valData, "NombreOperador");
            string vCodigoMoneda = LibXml.GetPropertyString(valData, "CodigoMoneda");
            string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "CuentaBancariaCobroDirecto");
            string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "ConceptoBancarioCobroDirecto");
            eIngresoEgreso vTipoConcepto = eIngresoEgreso.Ingreso;
            eGeneradoPor vGeneradoPor = eGeneradoPor.Cobranza;
            string vDescripcionMovimiento = "Cobro a - " + vCodigoCliente + " " + vNombreCliente;
            string vNumeroConciliacion = "";

            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoMovimiento", valConsecutivoMovimiento);
            vParams.AddInString("CodigoCtaBancaria", vCodigoCuentaBancaria, 5);
            vParams.AddInString("CodigoConcepto", vConceptoBancario, 8);
            vParams.AddInDateTime("Fecha", vFechaFactura);
            vParams.AddInEnum("TipoConcepto", (int)vTipoConcepto);
            vParams.AddInDecimal("Monto", vTotalMovimiento, 2);
            vParams.AddInString("NumeroDocumento", valNumeroCobranza, 15);
            vParams.AddInString("Descripcion", vDescripcionMovimiento, 255);
            vParams.AddInBoolean("GeneraImpuestoBancario", vGeneraImpuestoBancario);
            vParams.AddInString("NroMovimientoRelacionado", valNumeroCobranza, 15);
            vParams.AddInEnum("GeneradoPor", (int)vGeneradoPor);
            vParams.AddInDecimal("CambioABolivares", vCambioABolivares, 2);
            vParams.AddInBoolean("ImprimirCheque", vImprimirCheque);
            vParams.AddInBoolean("ConciliadoSN", vConciliadoSN);
            vParams.AddInString("NroConciliacion", vNumeroConciliacion, 9);
            vParams.AddInBoolean("GenerarAsientoDeRetiroEnCuenta", vGenerarAsientoDeRetiroEnCuenta);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vResult = vParams.Get();
            return vResult;
        }

        
    }

}
