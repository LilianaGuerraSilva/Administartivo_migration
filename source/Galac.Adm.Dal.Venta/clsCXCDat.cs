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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Dal.Venta {
    public class clsCXCDat: LibData, ILibDataRpt {

        public string CXCSqlInsertar() {

            StringBuilder vSql = new StringBuilder();

            vSql.AppendLine(" INSERT INTO cxc");
            vSql.AppendLine(" (ConsecutivoCompania");
            vSql.AppendLine(" ,Numero, Status, TipoCxc, CodigoCliente, CodigoVendedor, Origen ");
            vSql.AppendLine(" ,Fecha ,FechaCancelacion, FechaVencimiento ");
            vSql.AppendLine(" ,MontoExento, MontoGravado, MontoIva, MontoAbonado ");
            vSql.AppendLine(" ,Descripcion, Moneda, CambioAbolivares, CodigoCc, CentroDeCostos, SeRetuvoIva ");
            vSql.AppendLine(" ,NumeroDocumentoOrigen, RefinanciadoSn, NoAplicaParaLibroDeVentas, CodigoLote, CodigoMoneda ");
            vSql.AppendLine(" ,NombreOperador, FechaUltimaModificacion, NumeroControl ,NumeroComprobanteFiscal, FechaLimiteCambioAMonedaLocal) ");
            vSql.AppendLine(" VALUES( " + "@ConsecutivoCompania");
            vSql.AppendLine(" , " + "@NumeroCXC");
            vSql.AppendLine(" , " + "@StatusCXC");
            vSql.AppendLine(" , " + "@TipoCxC");
            vSql.AppendLine(" , " + "@CodigoCliente");
            vSql.AppendLine(" , " + "@CodigoVendedor");
            vSql.AppendLine(" , " + "@OrigenDocumento");
            vSql.AppendLine(" , " + "@Fecha");
            vSql.AppendLine(" , " + "@FechaCancelacion");
            vSql.AppendLine(" , " + "@FechaVencimiento");            
            vSql.AppendLine(" , " + "@MontoExento");
            vSql.AppendLine(" , " + "@BaseImponible");
            vSql.AppendLine(" , " + "@MontoIva");
            vSql.AppendLine(" , " + "@MontoAbonado");
            vSql.AppendLine(" , " + "@Descripcion");
            vSql.AppendLine(" , " + "@Moneda");
            vSql.AppendLine(" , " + "@CambioABolivar");
            vSql.AppendLine(" , " + "@CodigoCC");
            vSql.AppendLine(" , " + "@CentroDeCostos");
            vSql.AppendLine(" , " + "@SeRetuvoIva");
            vSql.AppendLine(" , " + "@NumeroDocumentoOrigen");
            vSql.AppendLine(" , " + "@Refinanciado");
            vSql.AppendLine(" , " + "@AplicaParalibroDeVentas");
            vSql.AppendLine(" , " + "@CodigoLote");
            vSql.AppendLine(" , " + "@CodigoMoneda");
            vSql.AppendLine(" , " + "@NombreOperador");
            vSql.AppendLine(" , " + "@FechaUltimaModificacion");
            vSql.AppendLine(" , " + "@NumeroControl");
            vSql.AppendLine(" , " + "@ComprobanteFiscal");
            vSql.AppendLine(" , " + "@FechaLimiteCambioAMonedaLocal)");
            return vSql.ToString();
        }
        
        public StringBuilder CXCParametrosInsertar(int valConsecutivoCompania, XElement valData) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();

            decimal vTotalBaseImponible = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalBaseImponible"), 2);
            decimal vTotalMontoIva = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalIVA"), 2);
            decimal vMontoAbonado = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalFactura"), 2);
            decimal vTotalMontoExento = LibImportData.ToDec(LibXml.GetPropertyString(valData, "TotalMontoExento"), 2);           
            eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(LibXml.GetPropertyString(valData, "TipoDeDocumento"));
            eTipoDeTransaccion vTipoCxC = eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA;
            string vCodigoCliente = LibXml.GetPropertyString(valData, "CodigoCliente");
            string vCodigoVendedor = LibXml.GetPropertyString(valData, "CodigoVendedor");
            string vComprobanteFiscal = LibXml.GetPropertyString(valData, "NumeroComprobanteFiscal");
            string vNumeroFactura = LibXml.GetPropertyString(valData, "Numero");
            string vNumeroCXC = vNumeroFactura;
            eStatusCXC vStatusCXC = eStatusCXC.CANCELADO;
            DateTime vFechaFactura = LibConvert.ToDate(LibXml.GetPropertyString(valData, "Fecha"));
            eTipoDeTransaccion vOrigenDocumento = eTipoDeTransaccion.TICKETMAQUINAREGISTRADORA;
            bool vSeRetuvoIva = false;
            bool vRefinanciado = false;
            string vCodigoCC = "0";
            string vDescripcion = "";
            bool vAplicaParalibroDeVentas = false;
            string vNombreOperador = LibXml.GetPropertyString(valData, "NombreOperador");
            string vCodigoMoneda = LibXml.GetPropertyString(valData, "CodigoMoneda");
            string vMoneda = LibXml.GetPropertyString(valData, "Moneda");
            decimal vCambioABolivar = AsignarCambioABolivares(valData);
            string vNumeroControl = "0";
            string vCodigoLote = "0";
            string vCentroDeCostos = "";
            DateTime vFechaLimiteCambioAMonedaLocal = LibConvert.ToDate(LibXml.GetPropertyString(valData,"Fecha"));

            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroCXC", vNumeroCXC, 20);
            vParams.AddInEnum("StatusCXC", (int)vStatusCXC);
            vParams.AddInEnum("TipoCxC", (int)vTipoCxC);
            vParams.AddInString("CodigoCliente", vCodigoCliente, 10);
            vParams.AddInString("CodigoVendedor", vCodigoVendedor, 5);
            vParams.AddInEnum("OrigenDocumento", (int)vOrigenDocumento);
            vParams.AddInDateTime("Fecha", vFechaFactura);
            vParams.AddInDateTime("FechaCancelacion", vFechaFactura);
            vParams.AddInDateTime("FechaVencimiento", vFechaFactura);            
            vParams.AddInDecimal("MontoExento", vTotalMontoExento, 3);
            vParams.AddInDecimal("BaseImponible", vTotalBaseImponible, 3);
            vParams.AddInDecimal("MontoIva", vTotalMontoIva, 3);
            vParams.AddInDecimal("MontoAbonado", vMontoAbonado, 3);
            vParams.AddInString("Descripcion", vDescripcion, 150);
            vParams.AddInString("Moneda", vMoneda, 80);
            vParams.AddInDecimal("CambioABolivar", vCambioABolivar, 3);
            vParams.AddInString("CodigoCC", vCodigoCC, 5);
            vParams.AddInString("CentroDeCostos", vCentroDeCostos, 40);
            vParams.AddInBoolean("SeRetuvoIva", vSeRetuvoIva);
            vParams.AddInString("NumeroDocumentoOrigen", vNumeroFactura, 20);
            vParams.AddInBoolean("Refinanciado", vRefinanciado);
            vParams.AddInBoolean("AplicaParalibroDeVentas", vAplicaParalibroDeVentas);
            vParams.AddInString("CodigoLote", vCodigoLote, 10);
            vParams.AddInString("CodigoMoneda", vCodigoMoneda, 4);            
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", vFechaFactura);
            vParams.AddInString("NumeroControl", vNumeroControl, 11);
            vParams.AddInString("ComprobanteFiscal", vComprobanteFiscal, 12);
            vParams.AddInDateTime("FechaLimiteCambioAMonedaLocal", vFechaLimiteCambioAMonedaLocal);
            vResult = vParams.Get();
            return vResult;
        }

        private decimal AsignarCambioABolivares(XElement valFactura) {
            decimal vCambioABolivares = 1;
            if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaDivisaComoMonedaPrincipalDeIngresoDeDatos")) {
                vCambioABolivares = LibImportData.ToDec(LibXml.GetPropertyString(valFactura, "CambioABolivares"), 2);
            }
            return vCambioABolivares;
        }

        #region Miembros de ILibDataRpt

        System.Data.DataTable ILibDataRpt.GetDt(string valSqlStringCommand, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSqlStringCommand, valCmdTimeout);
        }

        System.Data.DataTable ILibDataRpt.GetDt(string valSpName, StringBuilder valXmlParamsExpression, int valCmdTimeout) {
            return new LibDataReport().GetDataTableForReport(valSpName, valXmlParamsExpression, valCmdTimeout);
        }
        #endregion //Miembros de ILibDataRpt
    }
}
