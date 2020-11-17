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
using Galac.Adm.Ccl.GestionCompras;
using Excel = Microsoft.Office.Interop.Excel;
using LibGalac.Aos.DefGen;
using System.ComponentModel;
using LibGalac.Aos.Cnf;
using System.IO;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.Catching;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsLibroDeComprasNav : LibBaseNav<IList<LibroDeCompras>, IList<LibroDeCompras>>, ILibPdn, ILibroDeComprasPdn {
        #region Variables
        Saw.Lib.clsNoComunSaw _NoComunSaw;
        clsLibroElectronicoHelper insLibroElectronicoHelper;
        string _Separador = "|";
        string _DirectorioArchivos;
        private string _IdentificadorRegistroDeCompras;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsLibroDeComprasNav() {
            _NoComunSaw = new Saw.Lib.clsNoComunSaw();
            insLibroElectronicoHelper = new clsLibroElectronicoHelper();
            _DirectorioArchivos = insLibroElectronicoHelper.PathLibroElectronico("Registro de Compras");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<LibroDeCompras>, IList<LibroDeCompras>> GetDataInstance() {
            return null;
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen( string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow ) {
            bool vResult = false;
            switch (valCallingModule) {
            default:
            vResult = true;
            break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList( string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression ) {
            return false;
        }

        System.Xml.Linq.XElement ILibPdn.GetFk( string valCallingModule, StringBuilder valParameters ) {
            return null;
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo( string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression ) {
            bool vResult = false;
            switch (valModule) {
            case "Libro De Compras":
            vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
            break;
            default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo( ref IList<LibroDeCompras> refData ) {
        }
        #endregion //Metodos Generados

        private XElement CargarXML( int valConsecutivoCompania, string valMes, string valAno, bool valSujetoDomiciliado ) {
            LibGpParams vParams = new LibGpParams();
            XElement vDatos = null; ;
            try {
                vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
                vParams.AddInInteger("Mes",LibConvert.ToInt(valMes));
                vParams.AddInInteger("Ano", LibConvert.ToInt(valAno));
                vDatos = LibBusiness.ExecuteSelect(SQLConsultaParaLibroDeCompras(valConsecutivoCompania, valMes, valAno, valSujetoDomiciliado), vParams.Get(), "", 0);
                return vDatos;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        private bool WritePLE( string valNameFile, string valTextFile ) {
            try {
                string vPathFile = System.IO.Path.Combine(_DirectorioArchivos, valNameFile);
                File.WriteAllText(vPathFile, valTextFile);
                return true;
            } catch (Exception) {
                return false;
            }
        }

        #region SQLRegistros
        private string SQLConsultaParaLibroDeCompras( int valConsecutivoCompania, string valMes, string valAno, bool valSujetoNoDomiciliado ) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vUtilSql = new QAdvSql("");
            vSql.AppendLine(";");
            vSql.AppendLine("WITH LibroDeCompras");
            vSql.AppendLine("AS (");
            vSql.AppendLine("SELECT ");
            vSql.AppendLine(" ROW_NUMBER() OVER(ORDER BY cxP.Numero DESC) AS CorrelativoOperacion ");
            vSql.AppendLine(", cxP.ConsecutivoCxp AS Consecutivo ");
            vSql.AppendLine(", cxP.Fecha AS FechaDeEmision ");
            vSql.AppendLine(", cxP.FechaVencimiento AS FechaDeVencimiento ");
            vSql.AppendLine(", cxP.CodigoTipoDeComprobante AS TipoDeComprobante");
            vSql.AppendLine(",'0' + cxP.TipoDeCompra AS TipoDeCompra ");
            vSql.AppendLine(", cxP.NumeroSerie  AS NumeroDeSerie ");
            vSql.AppendLine(", '' AS AnoDeEmisionDUA ");
            vSql.AppendLine("," + " cxP.NumeroDeDocumento AS NumeroDeComprobante ");
            vSql.AppendLine(", '' AS UltimoNumeroOperSinDerechoCreditoFiscal ");
            vSql.AppendLine(", " + vUtilSql.IIF("Adm.Proveedor.TipoDocumentoIdentificacion = " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoIdentificacion.RUC), vUtilSql.ToSqlValue("6"),
               vUtilSql.IIF(" Adm.Proveedor.TipoDocumentoIdentificacion = " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoIdentificacion.DNI), vUtilSql.ToSqlValue("1"),
               vUtilSql.IIF(" Adm.Proveedor.TipoDocumentoIdentificacion = " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoIdentificacion.CarnetdeExtranjeria), vUtilSql.ToSqlValue("4"),
               vUtilSql.IIF(" Adm.Proveedor.TipoDocumentoIdentificacion = " + vUtilSql.EnumToSqlValue((int)eTipoDocumentoIdentificacion.Pasaporte), vUtilSql.ToSqlValue("7"), "6", true), true), true), true) + "AS TipoDocumentoIdentificacion ");
            vSql.AppendLine(" ,Adm.Proveedor.NumeroRIF AS NumeroRUC ");
            vSql.AppendLine("," + vUtilSql.IIF("cxp.Status=" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), vUtilSql.ToSqlValue("ANULADO"), "Adm.Proveedor.NombreProveedor", true) + " AS NombreProveedor ");
            vSql.AppendLine(" ,Adm.Proveedor.Direccion AS DireccionProveedor ");
            vSql.AppendLine(", " + vUtilSql.IIF("cxP.CreditoFiscal = " + vUtilSql.EnumToSqlValue((int)eCreditoFiscal.Deducible), vUtilSql.IIF("cxp.Status=" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0.000", vUtilSql.IIF("cxp.montogravado = 0 OR cxp.montoiva = 0", "0.00", " cxp.montoiva * cxP.CambioABolivares ", true), true), vUtilSql.ToSqlValue(0.00), true) + " AS MontoIGVOperacionGravada ");
            vSql.AppendLine(", " + vUtilSql.IIF("cxP.CreditoFiscal = " + vUtilSql.EnumToSqlValue((int)eCreditoFiscal.Deducible), vUtilSql.IIF("cxp.Status=" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0.000", vUtilSql.IIF("cxp.montogravado = 0 OR cxp.montoiva = 0", "0.00", " cxp.montogravado * cxP.CambioABolivares ", true), true), vUtilSql.ToSqlValue(0.00), true) + " AS BaseImponibleOperacionGravada ");

            vSql.AppendLine(", " + vUtilSql.IIF("cxP.CreditoFiscal = " + vUtilSql.EnumToSqlValue((int)eCreditoFiscal.Prorrateable), vUtilSql.IIF("cxp.Status=" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0.000", " (cxP.montoiva * cxP.CambioABolivares) ", true), vUtilSql.ToSqlValue(0.00), true) + " AS MontoIGVOperacionGravadaYNoGravada ");
            vSql.AppendLine(", " + vUtilSql.IIF("cxP.CreditoFiscal = " + vUtilSql.EnumToSqlValue((int)eCreditoFiscal.Prorrateable), vUtilSql.IIF("cxp.Status=" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0.000", " (cxp.montogravado) * cxP.CambioABolivares ", true), vUtilSql.ToSqlValue(0.00), true) + " AS BaseImponibleOperacionGravadaYNoGravada ");

            vSql.AppendLine(", " + vUtilSql.IIF("cxP.CreditoFiscal = " + vUtilSql.EnumToSqlValue((int)eCreditoFiscal.NoDeducible), vUtilSql.IIF("cxp.Status=" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0.000", " (cxP.montoiva * cxP.CambioABolivares) ", true), vUtilSql.ToSqlValue(0.00), true) + " AS MontoIGVOperacionNoGravada ");
            vSql.AppendLine(", " + vUtilSql.IIF("cxP.CreditoFiscal = " + vUtilSql.EnumToSqlValue((int)eCreditoFiscal.NoDeducible), vUtilSql.IIF("cxp.Status=" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado), "0.000", " (cxp.montogravado) * cxP.CambioABolivares ", true), vUtilSql.ToSqlValue(0.00), true) + " AS BaseImponibleOperacionNoGravada ");

            vSql.AppendLine(" ,0.000 AS ValorAdquisicionesNoGravadas ");
            vSql.AppendLine(" ,0.000 AS ISC ");
            vSql.AppendLine(" ,0.000 AS OtrosCargos ");
            vSql.AppendLine(" ,Cxp.MontoAbonado * cxP.CambioABolivares AS ImporteTotal ");
            vSql.AppendLine("," + vUtilSql.IsNull("Cxp.CodigoMoneda", vUtilSql.ToSqlValue("")) + " AS CodigoMoneda ");
            vSql.AppendLine(" ,'' AS NumeroComprobanteDePago ");
            vSql.AppendLine(",'' AS NumeroDetraccion ");
            vSql.AppendLine(",'' AS FechaDetraccion ");
            vSql.AppendLine(", Cxp.CambioABolivares as TipoDeCambio ");
            vSql.AppendLine("," + vUtilSql.IIF("cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCredito) +
                " OR cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCreditoCompFiscal), " MAX(cxp.FechaDeclaracionAduana) ", vUtilSql.ToSqlValue(""), true) + " AS FechaFacturaAfectada ");
            vSql.AppendLine(", " + vUtilSql.IIF("cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCredito) +
            " OR cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCreditoCompFiscal), vUtilSql.ToSqlValue("1"), vUtilSql.ToSqlValue(""), true) + " AS TipoFacturaAfectada ");
            vSql.AppendLine("," + vUtilSql.IIF("cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCredito) +
           " OR cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCreditoCompFiscal), vUtilSql.ToSqlValue("SERIE"), vUtilSql.ToSqlValue(""), true) + " AS SerieFacturaAfectada ");
            vSql.AppendLine("," + vUtilSql.IIF("cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCredito) +
            " OR cxp.TipoDeCxp = " + vUtilSql.EnumToSqlValue((int)eTipoDeTransaccion.NotaDeCreditoCompFiscal), " MAX(cxp.NumeroDeDocumentoAfectado)", vUtilSql.ToSqlValue(""), true) + " AS NumeroFacturaAfectada ");
            if (valSujetoNoDomiciliado) {
                vSql.AppendLine(", Adm.Proveedor.CodigoPaisResidencia AS PaisResidencia ");
                vSql.AppendLine(", Adm.Proveedor.CodigoConveniosSunat AS CodigoConveniosSunat ");
                vSql.AppendLine(", Adm.Proveedor.Beneficiario AS Beneficiario ");
            }
            vSql.AppendLine(" FROM ");
            vSql.AppendLine(" cxP INNER JOIN Adm.Proveedor ON ");
            vSql.AppendLine(" cxP.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania ");
            vSql.AppendLine(" AND cxP.CodigoProveedor = Adm.Proveedor.CodigoProveedor ");
            vSql.AppendLine(" WHERE ");
            vSql.AppendLine(" cxP.ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND cxP.Status In(" + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Cancelado) + ", "
                + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Abonado) + ","
                + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.PorCancelar) + ","
                + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Refinanciado) + ","
                + vUtilSql.EnumToSqlValue((int)eStatusDocumentoCxP.Anulado) + ")");
            vSql.AppendLine(" AND cxP.AnoDeAplicacion = @Ano ");
            vSql.AppendLine(" AND cxP.MesDeAplicacion = @Mes ");
            if (valSujetoNoDomiciliado) {
                vSql.AppendLine(" AND Proveedor.TipoDePersonaLibrosElectronicos IN (" + vUtilSql.EnumToSqlValue((int)eTipoDePersonaLibrosElectronicos.JuridicoNoDomiciliado) + "," + vUtilSql.EnumToSqlValue((int)eTipoDePersonaLibrosElectronicos.NaturalNoDomiciliado) + ")");
            } else {
                vSql.AppendLine(" AND Proveedor.TipoDePersonaLibrosElectronicos IN (" + vUtilSql.EnumToSqlValue((int)eTipoDePersonaLibrosElectronicos.JuridicoDomiciliado) + "," + vUtilSql.EnumToSqlValue((int)eTipoDePersonaLibrosElectronicos.NaturalDomciliado) + ")");
            }
            vSql.AppendLine(" GROUP BY cxp.Status, cxp.ConsecutivoCxp ,cxp.fecha, fechavencimiento, TipoDeCxp , cxP.CodigoTipoDeComprobante, tipodecompra, numeroserie, ");
            vSql.AppendLine(" cxP.NumeroDeDocumento, cxp.Numero, cxP.CreditoFiscal  ,Proveedor.TipoDocumentoIdentificacion,Proveedor.NumeroRIF ,Proveedor.NombreProveedor , ");
            vSql.AppendLine(" Proveedor.Direccion, cxp.MontoExento , cxp.Montogravado , cxp.MontoIva, CXP.CodigoMoneda  ,Cxp.Status , ");
            vSql.AppendLine(" cxp.CambioAbolivares ,Cxp.montoabonado");
            if (valSujetoNoDomiciliado) {
                vSql.AppendLine(" , Adm.Proveedor.CodigoPaisResidencia ");
                vSql.AppendLine(" , Adm.Proveedor.CodigoConveniosSunat ");
                vSql.AppendLine(" ,Adm.Proveedor.Beneficiario");
            }
            //vSql.AppendLine(" ORDER BY cxP.Fecha");
            vSql.AppendLine(")");
            vSql.AppendLine("SELECT CorrelativoOperacion, Consecutivo, FechaDeEmision, FechaDeVencimiento, TipoDeComprobante, NumeroDeSerie,");
            vSql.AppendLine("		AnoDeEmisionDUA, NumeroDeComprobante, UltimoNumeroOperSinDerechoCreditoFiscal, TipoDocumentoIdentificacion,");
            vSql.AppendLine("		NumeroRUC, NombreProveedor, DireccionProveedor,");
            vSql.AppendLine("		MontoIGVOperacionGravada, BaseImponibleOperacionGravada,");
            vSql.AppendLine("		MontoIGVOperacionGravadaYNoGravada, BaseImponibleOperacionGravadaYNoGravada,");
            vSql.AppendLine("		MontoIGVOperacionNoGravada, BaseImponibleOperacionNoGravada,");
            vSql.AppendLine("		ValorAdquisicionesNoGravadas, ISC, OtrosCargos,");
            vSql.AppendLine("		(MontoIGVOperacionGravada + BaseImponibleOperacionGravada +");
            vSql.AppendLine("		MontoIGVOperacionGravadaYNoGravada + BaseImponibleOperacionGravadaYNoGravada +");
            vSql.AppendLine("		MontoIGVOperacionNoGravada + BaseImponibleOperacionNoGravada +");
            vSql.AppendLine("		ValorAdquisicionesNoGravadas + ISC) AS  ImporteTotal,");
            vSql.AppendLine("		CodigoMoneda, NumeroComprobanteDePago, NumeroDetraccion, FechaDetraccion, TipoDeCambio,");
            vSql.AppendLine("		(CASE WHEN LibroDeCompras.FechaFacturaAfectada = '1900-01-01' THEN NULL ELSE FechaFacturaAfectada END) AS FechaFacturaAfectada,");
            vSql.AppendLine("		TipoFacturaAfectada, SerieFacturaAfectada, NumeroFacturaAfectada");
            vSql.AppendLine("FROM LibroDeCompras");
            vSql.AppendLine("ORDER BY FechaDeEmision;");
            return vSql.ToString();
        }
        #endregion

        private bool VerificarExistenciaDeRegistros( XElement vDat ) {
            bool vResult = false;
            if (vDat != null && vDat.HasElements) {
                int vContNodos = vDat.Descendants("GpResult").Count();
                if (vContNodos != 0) {
                    vResult = true;
                }
            }
            return vResult;
        }

        void ILibroDeComprasPdn.GenerarLibroDeCompras( int valConsecutivoCompania, string valMes, string valAno, string valNombreCompaniaParaInformes, string valNumeroRIF, BackgroundWorker valBWorker ) {
            int vContNodos = 0;
            object vOpcional = Type.Missing;
            string vTimeFile;
            int vContReg;
            int vContCeldas;
            int vCorrelativoOperacion;
            decimal vPercent = 0;
            decimal vProporcion;
            XElement vDat = CargarXML(valConsecutivoCompania, valMes, valAno, false);
            bool vExistenRegistros = VerificarExistenciaDeRegistros(vDat);
            string vHora = DateTime.Now.Hour.ToString();
            string vMinuto = DateTime.Now.Minute.ToString();
            vTimeFile = LibText.NCar('0', 2 - LibText.Len(vHora)) + vHora + LibText.NCar('0', 2 - LibText.Len(vMinuto)) + vMinuto;
            string FileName = "RegistroDeCompras_" + LibConvert.ToStr(LibDate.Today(), "ddMMyy") + "_" + vTimeFile + ".xlsx";

            try {
                if (vExistenRegistros) {
                    vContNodos = vDat.Descendants("GpResult").Count();
                    vProporcion = 100m / vContNodos;
                    string vNombreMesParaPeriodo = LibDate.MonthToName(LibConvert.ToInt(valMes));
                    List<XElement> vRecord = vDat.Descendants("GpResult").ToList();
                    #region CrearExcel
                    Excel.Application vNuevaInstExcel = new Excel.Application();
                    vNuevaInstExcel.DefaultFilePath = _DirectorioArchivos;
                    Excel.Workbook vNuevoLibroExcel = vNuevaInstExcel.Workbooks.Add(vOpcional);
                    Excel.Worksheet vHojaCalculos = (Excel.Worksheet)vNuevaInstExcel.ActiveWorkbook.Worksheets[1];

                    vHojaCalculos.get_Range("A1", "A1").Font.Bold = true;
                    vHojaCalculos.get_Range("A1", "A1").Font.Size = 20;
                    vHojaCalculos.get_Range("A1", "A1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    vHojaCalculos.Cells[1, "A"] = "FORMATO 8.1";
                    vHojaCalculos.get_Range("A2", "A2").ColumnWidth = 7.5;
                    vHojaCalculos.get_Range("A3", "A3").Font.Bold = true;
                    vHojaCalculos.get_Range("A3", "A3").Font.Size = 20;
                    vHojaCalculos.get_Range("A3", "A3").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    vHojaCalculos.Cells[3, "A"] = "REGISTRO DE COMPRAS";
                    vHojaCalculos.Cells[5, "A"] = "EJERCICIO: " + vNombreMesParaPeriodo.ToUpper() + " " + valAno;
                    vHojaCalculos.Cells[6, "A"] = "RUC: " + valNumeroRIF;
                    vHojaCalculos.Cells[7, "A"] = "APELLIDOS Y NOMBRES, DENOMINACIÓN O RAZÓN SOCIAL: " + valNombreCompaniaParaInformes;
                    vHojaCalculos.get_Range("A5", "A7").Font.Bold = true;

                    vHojaCalculos.get_Range("A9", "AB9").Font.Bold = true;
                    vHojaCalculos.get_Range("A10", "AB10").Font.Bold = true;
                    vHojaCalculos.get_Range("A11", "AB11").Font.Bold = true;
                    vHojaCalculos.get_Range("A9", "AB9").Font.Size = 8;
                    vHojaCalculos.get_Range("A10", "AB10").Font.Size = 8;
                    vHojaCalculos.get_Range("A11", "AB11").Font.Size = 8;
                    vHojaCalculos.get_Range("A9", "AB9").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    vHojaCalculos.get_Range("A9", "AB9").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    vHojaCalculos.get_Range("A10", "AB10").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    vHojaCalculos.get_Range("A10", "AB10").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    vHojaCalculos.get_Range("A11", "AB11").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    vHojaCalculos.get_Range("A11", "AB11").VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    vHojaCalculos.Cells[9, "A"] = "NÚMERO CORRELATIVO DEL REGISTRO O CÓDIGO ÚNICO DE LA OPERACIÓN";
                    vHojaCalculos.Cells[9, "A"].WrapText = true;
                    vHojaCalculos.Cells[9, "A"].ColumnWidth = 8;
                    vHojaCalculos.Cells[9, "A"].RowHeight = 52;
                    vHojaCalculos.Cells[10, "A"].RowHeight = 40;
                    vHojaCalculos.Cells[11, "A"].RowHeight = 60;
                    vHojaCalculos.get_Range("A9", "A11").Merge();

                    vHojaCalculos.get_Range("A1", "AB1").Merge();
                    vHojaCalculos.get_Range("A3", "AB3").Merge();

                    vHojaCalculos.Cells[9, "B"] = "FECHA DE EMISIÓN DEL COMPROBANTE DE PAGO O DOCUMENTO";
                    vHojaCalculos.Cells[9, "B"].WrapText = true;
                    vHojaCalculos.Cells[9, "B"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("B9", "B11").Merge();

                    vHojaCalculos.Cells[9, "C"] = "FECHA DE VENCIMIENTO O FECHA DE PAGO";
                    vHojaCalculos.Cells[9, "C"].WrapText = true;
                    vHojaCalculos.Cells[9, "C"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("C9", "C11").Merge();

                    vHojaCalculos.Cells[9, "D"] = "COMPROBANTE DE PAGO O DOCUMENTO";
                    vHojaCalculos.Cells[9, "D"].WrapText = true;
                    vHojaCalculos.Cells[9, "D"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("D9", "D10").Merge();
                    vHojaCalculos.get_Range("D9", "F9").Merge();

                    vHojaCalculos.Cells[11, "D"] = "TIPO (TABLA 10)";                    
                    vHojaCalculos.Cells[11, "D"].WrapText = true;
                    vHojaCalculos.Cells[11, "E"] = "SERIE O CODIGO DE LA DEPENDENCIA ADUANERA";
                    vHojaCalculos.Cells[11, "E"].WrapText = true;
                    vHojaCalculos.Cells[11, "E"].ColumnWidth = 6;
                    vHojaCalculos.Cells[11, "F"] = "AÑO DE EMISIÓN DE LA DUA O DSI";
                    vHojaCalculos.Cells[11, "F"].WrapText = true;
                    vHojaCalculos.Cells[11, "F"].ColumnWidth = 10;

                    vHojaCalculos.Cells[9, "G"] = "N° DEL COMPROBANTE DE PAGO, DOCUMENTO, N° DE ORDEN DEL FORMULARIO FÍSICO O VIRTUAL, N° DE DUA, DSI O LIQUIDACIÓN DE COBRANZA U OTROS DOCUMENTOS EMITIDOS POR SUNAT PARA ACREDITAR EL CRÉDITO FISCAL EN LA IMPORTACIÓN";
                    vHojaCalculos.Cells[9, "G"].WrapText = true;
                    vHojaCalculos.Cells[9, "G"].ColumnWidth = 12;
                    vHojaCalculos.get_Range("G9", "G11").Merge();

                    vHojaCalculos.Cells[9, "H"] = "INFORMACIÓN DEL PROVEEDOR";
                    vHojaCalculos.Cells[9, "H"].WrapText = true;
                    vHojaCalculos.Cells[9, "H"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("H9", "J9").Merge();

                    vHojaCalculos.Cells[10, "H"] = "DOCUMENTO DE IDENTIDAD";
                    vHojaCalculos.Cells[10, "H"].WrapText = true;
                    vHojaCalculos.Cells[10, "H"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("H10", "I10").Merge();

                    vHojaCalculos.Cells[11, "H"] = "TIPO (TABLA 2)";
                    vHojaCalculos.Cells[11, "H"].WrapText = true;
                    vHojaCalculos.Cells[11, "I"] = "NÚMERO";
                    vHojaCalculos.Cells[11, "I"].WrapText = true;
                    vHojaCalculos.Cells[11, "I"].ColumnWidth = 10;

                    vHojaCalculos.Cells[10, "J"] = "APELLIDOS Y NOMBRES DENOMINACIÓN O RAZÓN SOCIAL";
                    vHojaCalculos.Cells[10, "J"].WrapText = true;
                    vHojaCalculos.Cells[10, "J"].ColumnWidth = 32;
                    vHojaCalculos.get_Range("J10", "J11").Merge();

                    vHojaCalculos.Cells[9, "K"] = "ADQUISICIONES GRAVADAS DESTINADAS A OPERACIONES GRAVADAS Y/O DE EXPORTACIÓN";
                    vHojaCalculos.Cells[9, "K"].WrapText = true;
                    vHojaCalculos.Cells[9, "K"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("K9", "L9").Merge();
                    vHojaCalculos.get_Range("K9", "K10").Merge();

                    vHojaCalculos.Cells[11, "K"] = "BASE IMPONIBLE";
                    vHojaCalculos.Cells[11, "K"].WrapText = true;
                    vHojaCalculos.Cells[11, "K"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "L"] = "IGV";
                    vHojaCalculos.Cells[11, "L"].WrapText = true;
                    vHojaCalculos.Cells[11, "L"].ColumnWidth = 8;

                    vHojaCalculos.Cells[9, "M"] = "ADQUISICIONES GRAVADAS DESTINADAS A OPERACIONES GRAVADAS Y/O DE EXPORTACIÓN Y A OPERACIONES NO GRAVADAS";
                    vHojaCalculos.Cells[9, "M"].WrapText = true;
                    vHojaCalculos.Cells[9, "M"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("M9", "N9").Merge();
                    vHojaCalculos.get_Range("M9", "M10").Merge();

                    vHojaCalculos.Cells[11, "M"] = "BASE IMPONIBLE";
                    vHojaCalculos.Cells[11, "M"].WrapText = true;
                    vHojaCalculos.Cells[11, "M"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "N"] = "IGV";
                    vHojaCalculos.Cells[11, "N"].WrapText = true;
                    vHojaCalculos.Cells[11, "N"].ColumnWidth = 8;

                    vHojaCalculos.Cells[9, "O"] = "ADQUISICIONES GRAVADAS DESTINADAS A OPERACIONES NO GRAVADAS";
                    vHojaCalculos.Cells[9, "O"].WrapText = true;
                    vHojaCalculos.Cells[9, "O"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("O9", "P9").Merge();
                    vHojaCalculos.get_Range("O9", "O10").Merge();

                    vHojaCalculos.Cells[11, "O"] = "BASE IMPONIBLE";
                    vHojaCalculos.Cells[11, "O"].WrapText = true;
                    vHojaCalculos.Cells[11, "O"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "P"] = "IGV";
                    vHojaCalculos.Cells[11, "P"].WrapText = true;
                    vHojaCalculos.Cells[11, "P"].ColumnWidth = 8;

                    vHojaCalculos.Cells[9, "Q"] = "VALOR DE LAS ADQUISICIONES NO GRAVADAS";
                    vHojaCalculos.Cells[9, "Q"].WrapText = true;
                    vHojaCalculos.Cells[9, "Q"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("Q9", "Q11").Merge();

                    vHojaCalculos.Cells[9, "R"] = "ISC";
                    vHojaCalculos.Cells[9, "R"].WrapText = true;
                    vHojaCalculos.Cells[9, "R"].ColumnWidth = 6;
                    vHojaCalculos.get_Range("R9", "R11").Merge();

                    vHojaCalculos.Cells[9, "S"] = "OTROS TRIBUTOS Y CARGOS";
                    vHojaCalculos.Cells[9, "S"].WrapText = true;
                    vHojaCalculos.Cells[9, "S"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("S9", "S11").Merge();

                    vHojaCalculos.Cells[9, "T"] = "IMPORTE TOTAL";
                    vHojaCalculos.Cells[9, "T"].WrapText = true;
                    vHojaCalculos.Cells[9, "T"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("T9", "T11").Merge();

                    vHojaCalculos.Cells[9, "U"] = "N° DE COMPROBANTE DE PAGO EMITIDO POR SUJETO NO DOMICILIADO";
                    vHojaCalculos.Cells[9, "U"].WrapText = true;
                    vHojaCalculos.Cells[9, "U"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("U9", "U11").Merge();

                    vHojaCalculos.Cells[9, "V"] = "CONSTANCIA DE DEPÓSITO DE DETRACCIÓN";
                    vHojaCalculos.Cells[9, "V"].WrapText = true;
                    vHojaCalculos.Cells[9, "V"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("V9", "W9").Merge();
                    vHojaCalculos.get_Range("V9", "V10").Merge();

                    vHojaCalculos.Cells[11, "V"] = "NÚMERO";
                    vHojaCalculos.Cells[11, "V"].WrapText = true;
                    vHojaCalculos.Cells[11, "V"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "W"] = "FECHA DE EMISIÓN";
                    vHojaCalculos.Cells[11, "W"].WrapText = true;
                    vHojaCalculos.Cells[11, "W"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "X"] = "TIPO DE CAMBIO";
                    vHojaCalculos.Cells[11, "X"].WrapText = true;
                    vHojaCalculos.Cells[11, "X"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("X9", "X11").Merge();

                    vHojaCalculos.Cells[9, "Y"] = "REFERENCIA DEL COMPROBANTE DE PAGO O DOCUMENTO ORIGINAL QUE SE MODIFICA";
                    vHojaCalculos.Cells[9, "Y"].WrapText = true;
                    vHojaCalculos.Cells[9, "Y"].ColumnWidth = 8;
                    vHojaCalculos.get_Range("Y9", "AB9").Merge();
                    vHojaCalculos.get_Range("Y9", "Y10").Merge();

                    vHojaCalculos.Cells[11, "Y"] = "FECHA";
                    vHojaCalculos.Cells[11, "Y"].WrapText = true;
                    vHojaCalculos.Cells[11, "Y"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "Z"] = "TIPO (TABLA 10)";
                    vHojaCalculos.Cells[11, "Z"].WrapText = true;
                    vHojaCalculos.Cells[11, "Z"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "AA"] = "SERIE";
                    vHojaCalculos.Cells[11, "AA"].WrapText = true;
                    vHojaCalculos.Cells[11, "AA"].ColumnWidth = 8;

                    vHojaCalculos.Cells[11, "AB"] = "N° DEL COMPROBANTE DE PAGO O DOCUMENTO";
                    vHojaCalculos.Cells[11, "AB"].WrapText = true;
                    vHojaCalculos.Cells[11, "AB"].ColumnWidth = 10;

                    vContReg = 12;
                    vCorrelativoOperacion = 0;
                    foreach (XElement vXElement in vRecord) {
                        vCorrelativoOperacion++;
                        vHojaCalculos.Cells[vContReg, 1] = vCorrelativoOperacion.ToString();
                        vHojaCalculos.Cells[vContReg, 2] = LibString.SubString(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeEmision"), 0, 10).ToString();
                        vHojaCalculos.Cells[vContReg, 3] = LibString.SubString(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeVencimiento"), 0, 10).ToString();
                        vHojaCalculos.Cells[vContReg, 4].NumberFormat = "@";
                        vHojaCalculos.Cells[vContReg, 4] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDeComprobante");
                        vHojaCalculos.Cells[vContReg, 5] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeSerie");
                        vHojaCalculos.Cells[vContReg, 6] = LibXml.GetElementValueOrEmpty(vXElement, "AnoDeEmisionDUA");
                        vHojaCalculos.Cells[vContReg, 7] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeComprobante");
                        vHojaCalculos.Cells[vContReg, 8].NumberFormat = "@";
                        vHojaCalculos.Cells[vContReg, 8] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDocumentoIdentificacion");
                        vHojaCalculos.Cells[vContReg, 9] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroRUC");
                        vHojaCalculos.Cells[vContReg, 10] = LibXml.GetElementValueOrEmpty(vXElement, "NombreProveedor");
                        vHojaCalculos.Cells[vContReg, 11] = LibXml.GetElementValueOrEmpty(vXElement, "BaseImponibleOperacionGravada");
                        vHojaCalculos.Cells[vContReg, 12] = LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionGravada");
                        vHojaCalculos.Cells[vContReg, 13] = LibXml.GetElementValueOrEmpty(vXElement, "BaseImponibleOperacionGravadaYNoGravada");
                        vHojaCalculos.Cells[vContReg, 14] = LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionGravadaYNoGravada");
                        vHojaCalculos.Cells[vContReg, 15] = LibXml.GetElementValueOrEmpty(vXElement, "BaseImponibleOperacionNoGravada");
                        vHojaCalculos.Cells[vContReg, 16] = LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionNoGravada");
                        vHojaCalculos.Cells[vContReg, 17] = LibXml.GetElementValueOrEmpty(vXElement, "ValorAdquisicionesNoGravadas");
                        vHojaCalculos.Cells[vContReg, 18] = LibXml.GetElementValueOrEmpty(vXElement, "ISC");
                        vHojaCalculos.Cells[vContReg, 19] = LibXml.GetElementValueOrEmpty(vXElement, "OtrosCargos");
                        vHojaCalculos.Cells[vContReg, 20] = LibXml.GetElementValueOrEmpty(vXElement, "ImporteTotal");
                        vHojaCalculos.Cells[vContReg, 21] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroComprobanteDePago");
                        vHojaCalculos.Cells[vContReg, 22] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroDetraccion");
                        vHojaCalculos.Cells[vContReg, 23] = LibXml.GetElementValueOrEmpty(vXElement, "FechaDetraccion");
                        vHojaCalculos.Cells[vContReg, 24].NumberFormat = "0,0000";
                        vHojaCalculos.Cells[vContReg, 24] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDeCambio");
                        vHojaCalculos.Cells[vContReg, 25] = LibXml.GetElementValueOrEmpty(vXElement, "FechaFacturaAfectada");
                        vHojaCalculos.Cells[vContReg, 26] = LibXml.GetElementValueOrEmpty(vXElement, "TipoFacturaAfectada");
                        vHojaCalculos.Cells[vContReg, 27] = LibXml.GetElementValueOrEmpty(vXElement, "SerieFacturaAfectada");
                        vHojaCalculos.Cells[vContReg, 28] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroFacturaAfectada");
                        for (vContCeldas = 1; vContCeldas < 28; vContCeldas++) {
                            vHojaCalculos.Cells[vContReg, vContCeldas].Font.Size = 8;
                        }
                        vContReg++;
                        vPercent += vProporcion;
                        valBWorker.ReportProgress((int)vPercent, null);
                    }
                    int vCeldaParaTotales = vContReg - 1;
                    vHojaCalculos.Cells[vContReg, 10] = "TOTALES";
                    vHojaCalculos.Cells[vContReg, 11] = "=SUM(K12:K" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 12] = "=SUM(L12:L" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 13] = "=SUM(M12:M" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 14] = "=SUM(N12:N" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 15] = "=SUM(O12:O" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 16] = "=SUM(P12:P" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 17] = "=SUM(Q12:Q" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 18] = "=SUM(R12:R" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 19] = "=SUM(S12:S" + vCeldaParaTotales + ")";
                    vHojaCalculos.Cells[vContReg, 20] = "=SUM(T12:T" + vCeldaParaTotales + ")";
                    vHojaCalculos.get_Range("J" + vContReg, "T" + vContReg).Font.Bold = true;
                    vHojaCalculos.get_Range("J" + vContReg, "T" + vContReg).Font.Size = 8;
                    vNuevoLibroExcel.SaveAs(FileName);
                    vNuevaInstExcel.Visible = true;
                    vNuevaInstExcel.ActiveWindow.WindowState = Excel.XlWindowState.xlMaximized;
                    #endregion
                } else {
                    throw new GalacException("No existen registros válidos para el período consultado", eExceptionManagementType.Alert);
                }
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        void ILibroDeComprasPdn.GenerarPLELibroDeCompras( int valConsecutivoCompania, string valMes, string valAno, string valNombreCompaniaParaInformes, string valNumeroRIF, bool valRegistroDeComprasCompleto ) {
            try {
                if (valRegistroDeComprasCompleto) {
                    sGenerarPLELibroDeCompras(valConsecutivoCompania, valMes, valAno, valNombreCompaniaParaInformes, valNumeroRIF);
                    sGenerarPLELibroDeComprasSujetoNoDomiciliado(valConsecutivoCompania, valMes, valAno, valNombreCompaniaParaInformes, valNumeroRIF);
                } else {
                    sGenerarPLELibroDeComprasSimplificado(valConsecutivoCompania, valMes, valAno, valNombreCompaniaParaInformes, valNumeroRIF);
                }
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        void sGenerarPLELibroDeCompras( int valConsecutivoCompania, string valMes, string valAno, string valNombreCompaniaParaInformes, string valNumeroRIFRUC ) {
            string vNombreDelArchivoPLE = "";
            int vCantidadRegistros;
            int vContReg = 1;
            StringBuilder vBuilder = new StringBuilder();
            XElement vDat = CargarXML(valConsecutivoCompania, valMes, valAno, false);
            List<string> iColumna = new List<string>(new string[42]);
            string vCadenaStrParaPLE = "";
            string CodigoMonedaLocal = "";
            bool vSoloMonedaLocal = false;
            bool vExistenRegistros = VerificarExistenciaDeRegistros(vDat);

            _IdentificadorRegistroDeCompras = insLibroElectronicoHelper.IdentificadorRegistroPLE(eTipoDeRegistroPLE.RegistroDeCompras);
            try {
                if (vExistenRegistros) {
                    List<XElement> vRecord = vDat.Descendants("GpResult").ToList();
                    vCantidadRegistros = vRecord.Count;
                    CodigoMonedaLocal = _NoComunSaw.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                    vSoloMonedaLocal = vRecord.Exists(x => x.Element("CodigoMoneda").Value == CodigoMonedaLocal);
                    vNombreDelArchivoPLE = insLibroElectronicoHelper.NombreDelArchivoConFormatoSunat(_IdentificadorRegistroDeCompras, valNumeroRIFRUC, valAno, valMes, vCantidadRegistros, vSoloMonedaLocal);
                    vNombreDelArchivoPLE += ".txt";
                    foreach (XElement vXElement in vRecord) {
                        iColumna[1] = valAno + valMes + "00";
                        iColumna[2] = LibXml.GetElementValueOrEmpty(vXElement, "Consecutivo");
                        iColumna[3] = "M-" + LibText.FillWithCharToLeft(LibConvert.ToStr(vContReg), "0", 2);
                        iColumna[4] = LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeEmision")), "dd/MM/yyyy");
                        iColumna[5] = LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeVencimiento")), "dd/MM/yyyy");
                        iColumna[6] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDeComprobante");
                        iColumna[7] = LibText.FillWithCharToLeft(LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeSerie"), "0", 4);
                        iColumna[8] = LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeEmision")), "yyyy");
                        iColumna[9] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeComprobante");
                        iColumna[10] = LibXml.GetElementValueOrEmpty(vXElement, "UltimoNumeroOperSinDerechoCreditoFiscal");
                        iColumna[11] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDocumentoIdentificacion");
                        iColumna[12] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroRUC");
                        iColumna[13] = LibXml.GetElementValueOrEmpty(vXElement, "NombreProveedor");
                        iColumna[14] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "BaseImponibleOperacionGravada"), 2);
                        iColumna[15] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionGravada"), 2);
                        iColumna[16] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "BaseImponibleOperacionGravadaYNoGravada"), 2);
                        iColumna[17] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionGravadaYNoGravada"), 2);
                        iColumna[18] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "BaseImponibleOperacionNoGravada"), 2);
                        iColumna[19] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionNoGravada"), 2);
                        iColumna[20] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "ValorAdquisicionesNoGravadas"), 2);
                        iColumna[21] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "ISC"), 2);
                        iColumna[22] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "OtrosCargos"), 2);
                        iColumna[23] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "ImporteTotal"), 2);
                        iColumna[24] = LibXml.GetElementValueOrEmpty(vXElement, "CodigoMoneda");
                        iColumna[25] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "TipoDeCambio"), 3);
                        iColumna[26] = LibConvert.ToStr(LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaFacturaAfectada")), "dd/MM/yyyy"));
                        iColumna[27] = LibXml.GetElementValueOrEmpty(vXElement, "TipoFacturaAfectada");
                        iColumna[28] = LibXml.GetElementValueOrEmpty(vXElement, "SerieFacturaAfectada");
                        iColumna[29] = ""; //Código de la dependencia Aduanera de la Declaración Única de Aduanas (DUA) o de la Declaración Simplificada de Importación (DSI)
                        iColumna[30] = ""; //Número del comprobante de pago que se modifica (4)
                        iColumna[31] = ""; //Fecha de emisión de la Constancia de Depósito de Detracción (6)
                        iColumna[32] = ""; //Número de la Constancia de Depósito de Detracción (6)
                        iColumna[33] = ""; //Marca del comprobante de pago sujeto a retención
                        iColumna[34] = ""; //Clasificación de los bienes y servicios adquiridos (Tabla 30) Aplicable solo a los contribuyentes que hayan obtenido ingresos mayores a 1,500 UIT en el ejercicio anterior
                        iColumna[35] = ""; //Identificación del Contrato o del proyecto en el caso de los Operadores de las sociedades irregulares, consorcios, joint ventures u otras formas de contratos de colaboración empresarial, que no lleven contabilidad independiente
                        iColumna[36] = ""; //Error tipo 1: inconsistencia en el tipo de cambio
                        iColumna[37] = ""; //Error tipo 2: inconsistencia por proveedores no habidos
                        iColumna[38] = ""; //Error tipo 3:
                        iColumna[39] = ""; //Error Tipo 4:
                        iColumna[40] = ""; //Indicador de comprobantes de pago cancelados con medios de pago
                        iColumna[41] = "1"; //Estado que identifica la oportunidad de la anotación o indicación si ésta corresponde a un ajuste                    
                        for (int vcount = 1; vcount < iColumna.Count; vcount++) {
                            vCadenaStrParaPLE += iColumna[vcount] + _Separador;
                        }
                        vBuilder.AppendLine(vCadenaStrParaPLE);
                        vCadenaStrParaPLE = string.Empty;
                        vContReg++;
                    }
                    vCadenaStrParaPLE = vBuilder.ToString();
                    vCantidadRegistros = LibString.Len(vCadenaStrParaPLE);
                    vCadenaStrParaPLE = LibString.SubString(vCadenaStrParaPLE, 0, vCantidadRegistros - 2);
                    WritePLE(vNombreDelArchivoPLE, vCadenaStrParaPLE);
                } else {
                    throw new GalacException("No existen registros válidos para el período consultado", eExceptionManagementType.Alert);
                }
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        void sGenerarPLELibroDeComprasSimplificado( int valConsecutivoCompania, string valMes, string valAno, string valNombreCompaniaParaInformes, string valNumeroRifRuc ) {
            string vNombreDelArchivoPLE;
            int vContReg = 1;
            XElement vDat = CargarXML(valConsecutivoCompania, valMes, valAno, false);
            List<string> iColumna = new List<string>(new string[32]);
            string vCadenaStrParaPLE = "";
            string vCodigoMonedaLocal = "";
            bool vSoloMonedaLocal = false;
            int vCantidadRegistros = 0;
            StringBuilder vBuilder = new StringBuilder();
            bool vExistenRegistros = VerificarExistenciaDeRegistros(vDat);
            _IdentificadorRegistroDeCompras = insLibroElectronicoHelper.IdentificadorRegistroPLE(eTipoDeRegistroPLE.RegistroDeComprasSimple);

            try {
                if (vExistenRegistros) {
                    List<XElement> vRecord = vDat.Descendants("GpResult").ToList();
                    vCantidadRegistros = vRecord.Count;
                    vCodigoMonedaLocal = _NoComunSaw.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                    vSoloMonedaLocal = vRecord.Exists(x => x.Element("CodigoMoneda").Value == vCodigoMonedaLocal);
                    vNombreDelArchivoPLE = insLibroElectronicoHelper.NombreDelArchivoConFormatoSunat(_IdentificadorRegistroDeCompras, valNumeroRifRuc, valAno, valMes, vCantidadRegistros, vSoloMonedaLocal);
                    vNombreDelArchivoPLE += ".txt";
                    foreach (XElement vXElement in vRecord) {
                        iColumna[1] = valAno + valMes + "00";
                        iColumna[2] = LibXml.GetElementValueOrEmpty(vXElement, "Consecutivo");
                        iColumna[3] = "M-" + LibText.FillWithCharToLeft(LibConvert.ToStr(vContReg), "0", 2);
                        iColumna[4] = LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeEmision")), "dd/MM/yyyy");
                        iColumna[5] = LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeVencimiento")), "dd/MM/yyyy");
                        iColumna[6] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDeComprobante"); //TipoDeComprobante
                        iColumna[7] = LibText.FillWithCharToLeft(LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeSerie"), "0", 4);
                        iColumna[8] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeComprobante");
                        iColumna[9] = LibXml.GetElementValueOrEmpty(vXElement, "UltimoNumeroOperSinDerechoCreditoFiscal");
                        iColumna[10] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDocumentoIdentificacion");
                        iColumna[11] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroRUC");
                        iColumna[12] = LibXml.GetElementValueOrEmpty(vXElement, "NombreProveedor");
                        iColumna[13] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "BaseImponibleOperacionGravada"), 2);
                        iColumna[14] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionGravada"), 2);
                        iColumna[15] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "OtrosCargos"), 2);
                        iColumna[16] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "ImporteTotal"), 2);
                        iColumna[17] = LibXml.GetElementValueOrEmpty(vXElement, "CodigoMoneda");
                        iColumna[18] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "TipoDeCambio"), 3);
                        iColumna[19] = LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaFacturaAfectada")), "dd/MM/yyyy");
                        iColumna[20] = LibXml.GetElementValueOrEmpty(vXElement, "TipoFacturaAfectada"); //TipoFacturaAfectada
                        iColumna[21] = LibXml.GetElementValueOrEmpty(vXElement, "SerieFacturaAfectada");//SerieFacturaAfectada
                        iColumna[22] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroFacturaAfectada");// NumeroFacturaAfectada
                        iColumna[23] = "";//;Fecha de emisión de la Constancia de Depósito de Detracción (6)
                        iColumna[24] = "";//Número de la Constancia de Depósito de Detracción (6)
                        iColumna[25] = "";//Marca del comprobante de pago sujeto a retención
                        iColumna[26] = "";//"Clasificación de los bienes y servicios adquiridos (Tabla 30) Aplicable solo a los contribuyentes que hayan obtenido ingresos mayores a 1,500 UIT en el ejercicio anterior"
                        iColumna[27] = "";//Error tipo 1: inconsistencia en el tipo de cambio
                        iColumna[28] = "";//Error tipo 2: inconsistencia por proveedores no habidos
                        iColumna[29] = "";//Error tipo 3: inconsistencia por proveedores que renunciaron a la exoneración del Apéndice I del IGV
                        iColumna[30] = ""; //Indicador de Comprobantes de pago cancelados con medios de pago""; 
                        iColumna[31] = "1";//Estado que identifica la oportunidad de la anotación o indicación si ésta corresponde a un ajuste.
                        for (int vcount = 1; vcount < iColumna.Count; vcount++) {
                            vCadenaStrParaPLE += iColumna[vcount] + _Separador;
                        }
                        vBuilder.AppendLine(vCadenaStrParaPLE);
                        vCadenaStrParaPLE = string.Empty;
                        vContReg++;
                    }
                    vCadenaStrParaPLE = vBuilder.ToString();
                    vCantidadRegistros = LibString.Len(vCadenaStrParaPLE);
                    vCadenaStrParaPLE = LibString.SubString(vCadenaStrParaPLE, 0, vCantidadRegistros - 2);
                    WritePLE(vNombreDelArchivoPLE, vCadenaStrParaPLE);
                } else {
                    throw new GalacException("No existen registros válidos para el período consultado", eExceptionManagementType.Alert);
                }
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        void sGenerarPLELibroDeComprasSujetoNoDomiciliado( int valConsecutivoCompania, string valMes, string valAno, string valNombreCompaniaParaInformes, string valNumeroRIFRUC ) {
            string vNombreDelArchivoPLE;
            int vContReg = 1;
            XElement vDat = CargarXML(valConsecutivoCompania, valMes, valAno, true);
            List<string> iColumna = new List<string>(new string[37]);
            string vCadenaStrParaPLE = "";
            string vCodigoMonedaLocal = "";
            bool vSoloMonedaLocal = false;
            int vCantidadRegistros = 0;
            StringBuilder vBuilder = new StringBuilder();
            bool vExistenRegistros = VerificarExistenciaDeRegistros(vDat);
            _IdentificadorRegistroDeCompras = insLibroElectronicoHelper.IdentificadorRegistroPLE(eTipoDeRegistroPLE.RegistroDeComprasSujtNoDomiciliado);
            try {

                if (vExistenRegistros) {
                    List<XElement> vRecord = vDat.Descendants("GpResult").ToList();
                    vCantidadRegistros = vRecord.Count;
                    vCodigoMonedaLocal = _NoComunSaw.InstanceMonedaLocalActual.CodigoMoneda(LibDate.Today());
                    vSoloMonedaLocal = vRecord.Exists(x => x.Element("CodigoMoneda").Value == vCodigoMonedaLocal);                   
                    foreach (XElement vXElement in vRecord) {
                        iColumna[1] = valAno + valMes + "00";
                        iColumna[2] = LibXml.GetElementValueOrEmpty(vXElement, "Consecutivo");
                        iColumna[3] = "M-" + LibText.FillWithCharToLeft(LibConvert.ToStr(vContReg), "0", 2);
                        iColumna[4] = LibConvert.ToStr(LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vXElement, "FechaDeEmision")), "dd/MM/yyyy");
                        iColumna[5] = LibXml.GetElementValueOrEmpty(vXElement, "TipoDeComprobante"); //TipoDeComprobante S.N.D
                        iColumna[6] = LibText.FillWithCharToLeft(LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeSerie"), "0", 4);
                        iColumna[7] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeComprobante");
                        iColumna[8] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionGravada"), 2);//Valor de la Adquisición
                        iColumna[9] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "OtrosCargos"), 2);
                        iColumna[10] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "ImporteTotal"), 2);
                        iColumna[11] = "00"; //(agregar desde cxp cuando exista)CodigoTipoDeComprobante Tipo de Comprobante D.C.F                        
                        iColumna[12] = LibText.FillWithCharToLeft(LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeSerie"), "0", 4);// NumeroDeSerie Serie D.C.F  del comprobante de pago o documento que sustenta el crédito fiscal. En los casos de la Declaración Única de Aduanas (DUA) o de la Declaración Simplificada de Importación (DSI) se consignará el código de la dependencia Aduanera.
                        iColumna[13] = ""; //Año de emisión de la DUA o DSI que sustenta el crédito fiscal
                        iColumna[14] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroDeComprobante");// NumeroDeComprobante" de pago o documento o número de orden del formulario físico o virtual donde conste el pago del impuesto, tratándose de la utilización de servicios prestados por no domiciliados u otros, número de la DUA o de la DSI, que sustente el crédito fiscal.
                        iColumna[15] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "MontoIGVOperacionGravada"), 2);
                        iColumna[16] = LibXml.GetElementValueOrEmpty(vXElement, "CodigoMoneda");
                        iColumna[17] = insLibroElectronicoHelper.DarFormatoMonetarioParaPLE(LibXml.GetElementValueOrEmpty(vXElement, "TipoDeCambio"), 3);
                        iColumna[18] = vCadenaStrParaPLE + LibXml.GetElementValueOrEmpty(vXElement, "PaisResidencia");
                        iColumna[19] = vCadenaStrParaPLE + LibXml.GetElementValueOrEmpty(vXElement, "NombreProveedor");
                        iColumna[20] = vCadenaStrParaPLE + LibXml.GetElementValueOrEmpty(vXElement, "DireccionProveedor");
                        iColumna[21] = LibXml.GetElementValueOrEmpty(vXElement, "NumeroRUC");
                        iColumna[22] = ""; //Número de identificación fiscal del beneficiario efectivo de los pagos
                        iColumna[23] = LibXml.GetElementValueOrEmpty(vXElement, "Beneficiario"); // Apellidos y nombres, denominación o razón social  del beneficiario efectivo de los pagos. En caso de personas naturales se debe consignar los datos en el siguiente orden: apellido paterno, apellido materno y nombre completo.
                        iColumna[24] = ""; //Pais de la residencia del beneficiario efectivo de los pagos
                        iColumna[25] = ""; //Vínculo entre el contribuyente y el residente en el extranjero
                        iColumna[26] = ""; //Renta Bruta
                        iColumna[27] = ""; //Deducción / Costo de Enajenación de bienes de capital
                        iColumna[28] = ""; //Renta Neta
                        iColumna[29] = ""; //Tasa de Retención
                        iColumna[30] = ""; //Impuesto Retenido
                        iColumna[31] = vCadenaStrParaPLE + LibXml.GetElementValueOrEmpty(vXElement, "CodigoConveniosSunat");
                        iColumna[32] = ""; //Exoneración aplicada
                        iColumna[33] = ""; //Tipo de Renta
                        iColumna[34] = ""; //Modalidad del servicio prestado por el no domiciliado 
                        iColumna[35] = ""; //Aplicación del penultimo parrafo del Art. 76° de la Ley del Impuesto a la Renta
                        iColumna[36] = "0";// Estado                      
                        for (int vcount = 1; vcount < iColumna.Count; vcount++) {
                            vCadenaStrParaPLE += iColumna[vcount] + _Separador;
                        }
                        vBuilder.AppendLine(vCadenaStrParaPLE);
                        vCadenaStrParaPLE = string.Empty;
                        vContReg++;
                    }
                } else {
                    vCantidadRegistros = 0;
                    vSoloMonedaLocal = true;
                }
                vNombreDelArchivoPLE = insLibroElectronicoHelper.NombreDelArchivoConFormatoSunat(_IdentificadorRegistroDeCompras, valNumeroRIFRUC, valAno, valMes, vCantidadRegistros, vSoloMonedaLocal);
                vNombreDelArchivoPLE += ".txt";
                vCadenaStrParaPLE = vBuilder.ToString();
                vCantidadRegistros = LibString.Len(vCadenaStrParaPLE);
                vCadenaStrParaPLE = LibString.SubString(vCadenaStrParaPLE, 0, vCantidadRegistros - 2);
                WritePLE(vNombreDelArchivoPLE, vCadenaStrParaPLE);
            } catch (Exception vEx) {
                throw vEx;
            }
        }
    } //End of class clsLibroDeComprasNav
} //End of namespace Galac.Adm.Brl.Compras