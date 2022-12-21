using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.DDL {
    public class clsCompatViews {
        public clsCompatViews() {
        }

        public static bool CrearVistaDboCiudad() {
            return LibViews.CreateCompatibilityView("Ciudad", "SELECT NombreCiudad FROM Comun.Ciudad", true);
        }

        public static bool CrearVistaDboSectorDeNegocio() {
            return LibViews.CreateCompatibilityView("SectorDeNegocio", "SELECT Descripcion FROM Comun.SectorDeNegocio", true);
        }

        public static bool CrearVistaDboBanco() {
            return LibViews.CreateCompatibilityView("Banco", "SELECT Consecutivo AS Codigo,Nombre FROM Comun.Banco", true);
        }

        public static bool CrearVistaDboPais() {
            return LibViews.CreateCompatibilityView("Pais", "SELECT Codigo,Nombre FROM Comun.Pais", true);
        }

        public static bool CrearVistaDboCiiu() {
            bool vResult = true;
            int vTipoTabla = 1; //tabla anterior 2004
            vResult = vResult && CrearVistaDboCiiu(vTipoTabla);
            vTipoTabla = 2; //tabla 2004
            vResult = vResult && CrearVistaDboCiiu(vTipoTabla);
            return vResult;
        }

        private static bool CrearVistaDboCiiu(int valTipoTabla) {
            string vNombreVista = "";
            if (valTipoTabla == 1) {
                vNombreVista = "CIIU";
            } else if (valTipoTabla == 2) {
                vNombreVista = "CIIU2004";
            }
            return LibViews.CreateCompatibilityView(vNombreVista, "SELECT Codigo AS CodigoCIIU,Descripcion,EsTitulo FROM Comun.Ciiu WHERE TipoTabla = " + new QAdvSql("").EnumToSqlValue(valTipoTabla), true);
        }
        public static bool CrearVistaDboCategoria() {
            return LibViews.CreateCompatibilityView("Categoria", "SELECT ConsecutivoCompania, Descripcion FROM Saw.Categoria", true);
        }


        public static bool CrearVistaDboUnidadDeVenta() {
            return LibViews.CreateCompatibilityView("UnidadDeVenta", "SELECT Nombre, NombreOperador, FechaUltimaModificacion, fldTimeStamp FROM Saw.UnidadDeVenta", true);
        }

        public static bool CrearVistaDboUrbanizacionZP() {
            return LibViews.CreateCompatibilityView("UrbanizacionZP", "SELECT Urbanizacion, ZonaPostal, fldTimeStamp FROM Saw.UrbanizacionZP", true);
        }

        public static bool CrearVistaDboZonaCobranza() {
            return LibViews.CreateCompatibilityView("ZonaCobranza", "SELECT ConsecutivoCompania, Nombre, NombreOperador, FechaUltimaModificacion, fldTimeStamp FROM Saw.ZonaCobranza", true);
        }

        public static bool CrearVistaDboMaquinaFiscal() {
            return LibViews.CreateCompatibilityView("MaquinaFiscal", "SELECT ConsecutivoCompania, ConsecutivoMaquinaFiscal, Descripcion, NumeroRegistro, Status, LongitudNumeroFiscal, NombreOperador, FechaUltimaModificacion, fldTimeStamp FROM Saw.MaquinaFiscal", true);
        }

        public static bool CrearVistaDboPropAnalisisVenc() {
            return LibViews.CreateCompatibilityView("PropAnalisisVenc", "SELECT SecuencialUnique0, PrimerVencimiento, SegundoVencimiento, TercerVencimiento, NombreOperador, FechaUltimaModificacion, fldTimeStamp FROM Saw.PropAnalisisVenc", true);
        }
        public static bool CrearVistaDboTalla() {
            return LibViews.CreateCompatibilityView("Talla", "SELECT ConsecutivoCompania, CodigoTalla, DescripcionTalla, CodigoLote, NombreOperador, FechaUltimaModificacion, fldTimeStamp FROM Saw.Talla", true);
        }
        public static bool CrearVistaDboColor() {
            return LibViews.CreateCompatibilityView("Color", "SELECT ConsecutivoCompania, CodigoColor, DescripcionColor, CodigoLote,NombreOperador, FechaUltimaModificacion, fldTimeStamp FROM Saw.Color", true);
        }

        public static bool CrearVistaDboNotaFinal() {
            return LibViews.CreateCompatibilityView("NotaFinal", "SELECT ConsecutivoCompania, CodigoDeLaNota, Descripcion, NombreOperador, FechaUltimaModificacion, fldTimeStamp FROM Saw.NotaFinal", true);
        }
        public static bool CrearVistaDboTipoProveedor() {
            return LibViews.CreateCompatibilityView("TipoProveedor", "SELECT ConsecutivoCompania, Nombre FROM Saw.TipoProveedor", true);
        }
        public static bool CrearVistaDboFormaDelCobro() {
            return LibViews.CreateCompatibilityView("FormaDelCobro", "SELECT Codigo, Nombre, TipoDePago FROM Saw.FormaDelCobro", true);
        }

        public static bool CrearVistaDboCierreCostoDelPeriodo() {
            return LibViews.CreateCompatibilityView("CierreCostoDelPeriodo", "SELECT ConsecutivoCompania, CodigoArticulo AS Codigo, Fecha, Existencia, Costo, EsCargaInicial, fldTimeStamp FROM Adm.CargaInicial", true);
        }
		
        public static bool CrearVistaDboReglasDeContabilizacion() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.Append(" SELECT    ");
            vSQL.Append("ConsecutivoCompania");
            vSQL.Append(",Numero");
            vSQL.Append(",DiferenciaEnCambioyCalculo");
            vSQL.Append(",CuentaIva1Credito");
            vSQL.Append(",CuentaIva1Debito");
            vSQL.Append(",DondeContabilizarRetIva");
            vSQL.Append(",CuentaRetencionIva");
            vSQL.Append(",TipoContabilizacionCxc");
            vSQL.Append(",ContabIndividualCxc");
            vSQL.Append(",ContabPorLoteCxc");
            vSQL.Append(",CuentaCxCclientes");
            vSQL.Append(",CuentaCxCingresos");
            vSQL.Append(",TipoContabilizacionCxp");
            vSQL.Append(",ContabIndividualCxp");
            vSQL.Append(",ContabPorLoteCxp");
            vSQL.Append(",CuentaCxPgasto");
            vSQL.Append(",CuentaCxPproveedores");
            vSQL.Append(",TipoContabilizacionCobranza");
            vSQL.Append(",ContabIndividualCobranza");
            vSQL.Append(",ContabPorLoteCobranza");
            vSQL.Append(",CuentaCobranzaCobradoEnEfectivo");
            vSQL.Append(",CuentaCobranzaCobradoEnCheque");
            vSQL.Append(",CuentaCobranzaCobradoEnTarjeta");
            vSQL.Append(",CuentaCobranzaRetencionIslr");
            vSQL.Append(",CuentaCobranzaRetencionIva");
            vSQL.Append(",CuentaCobranzaOtros");
            vSQL.Append(",CuentaCobranzaCxCclientes");
            vSQL.Append(",CuentaCobranzaCobradoAnticipo");
            vSQL.Append(",TipoContabilizacionPagos");
            vSQL.Append(",ContabIndividualPagos");
            vSQL.Append(",ContabPorLotePagos");
            vSQL.Append(",CuentaPagosCxPproveedores");
            vSQL.Append(",CuentaPagosRetencionIslr");
            vSQL.Append(",CuentaPagosOtros");
            vSQL.Append(",CuentaPagosBanco");
            vSQL.Append(",CuentaPagosPagadoAnticipo");
            vSQL.Append(",TipoContabilizacionFacturacion");
            vSQL.Append(",ContabIndividualFacturacion");
            vSQL.Append(",ContabPorLoteFacturacion");
            vSQL.Append(",CuentaFacturacionCxCclientes");
            vSQL.Append(",CuentaFacturacionMontoTotalFactura");
            vSQL.Append(",CuentaFacturacionCargos");
            vSQL.Append(",CuentaFacturacionDescuentos");
            vSQL.Append(",ContabilizarPorArticulo");
            vSQL.Append(",AgruparPorCuentaDeArticulo");
            vSQL.Append(",AgruparPorCargosDescuentos");
            vSQL.Append(",TipoContabilizacionRdvtas");
            vSQL.Append(",ContabIndividualRdvtas");
            vSQL.Append(",ContabPorLoteRdvtas");
            vSQL.Append(",CuentaRdvtasCaja");
            vSQL.Append(",CuentaRdvtasMontoTotal");
            vSQL.Append(",ContabilizarPorArticuloRdvtas");
            vSQL.Append(",AgruparPorCuentaDeArticuloRdvtas");
            vSQL.Append(",TipoContabilizacionMovBancario");
            vSQL.Append(",ContabIndividualMovBancario");
            vSQL.Append(",ContabPorLoteMovBancario");
            vSQL.Append(",CuentaMovBancarioGasto");
            vSQL.Append(",CuentaMovBancarioBancosHaber");
            vSQL.Append(",CuentaMovBancarioBancosDebe");
            vSQL.Append(",CuentaMovBancarioIngresos");
            vSQL.Append(",CuentaDebitoBancarioGasto");
            vSQL.Append(",CuentaDebitoBancarioBancos");
            vSQL.Append(",CuentaCreditoBancarioGasto");
            vSQL.Append(",CuentaCreditoBancarioBancos");
            vSQL.Append(",TipoContabilizacionAnticipo");
            vSQL.Append(",ContabIndividualAnticipo");
            vSQL.Append(",ContabPorLoteAnticipo");
            vSQL.Append(",CuentaAnticipoCaja");
            vSQL.Append(",CuentaAnticipoCobrado");
            vSQL.Append(",CuentaAnticipoOtrosIngresos");
            vSQL.Append(",CuentaAnticipoPagado");
            vSQL.Append(",CuentaAnticipoBanco");
            vSQL.Append(",CuentaAnticipoOtrosEgresos");
            vSQL.Append(",FacturaTipoComprobante");
            vSQL.Append(",CxCtipoComprobante");
            vSQL.Append(",CxPtipoComprobante");
            vSQL.Append(",CobranzaTipoComprobante");
            vSQL.Append(",PagoTipoComprobante");
            vSQL.Append(",MovimientoBancarioTipoComprobante");
            vSQL.Append(",AnticipoTipoComprobante");
            vSQL.Append(",CuentaCostoDeVenta");
            vSQL.Append(",CuentaInventario");
            vSQL.Append(",TipoContabilizacionInventario");
            vSQL.Append(",AgruparPorCuentaDeArticuloInven");
            vSQL.Append(",InventarioTipoComprobante");
            vSQL.Append(",CtaDePagosSueldos");
            vSQL.Append(",CtaDePagosSueldosBanco");
            vSQL.Append(",ContabIndividualPagosSueldos");
            vSQL.Append(",PagosSueldosTipoComprobante");
            vSQL.Append(",TipoContabilizacionDePagosSueldos");
            vSQL.Append(",EditarComprobanteDePagosSueldos");
            vSQL.Append(",EditarComprobanteAfterInsertCxC");
            vSQL.Append(",EditarComprobanteAfterInsertCxP");
            vSQL.Append(",EditarComprobanteAfterInsertCobranza");
            vSQL.Append(",EditarComprobanteAfterInsertPagos");
            vSQL.Append(",EditarComprobanteAfterInsertFactura");
            vSQL.Append(",EditarComprobanteAfterInsertResDia");
            vSQL.Append(",EditarComprobanteAfterInsertMovBan");
            vSQL.Append(",EditarComprobanteAfterInsertImpTraBan");
            vSQL.Append(",EditarComprobanteAfterInsertAnticipo");
            vSQL.Append(",EditarComprobanteAfterInsertInventario");
            vSQL.Append(",EditarComprobanteAfterInsertCajaChica");
            //vSQL.Append(",TipoContabilizacionTransfCtas");
            //vSQL.Append(",ContabIndividualTransfCtas");
            //vSQL.Append(",ContabPorLoteTransfCtas");
            //vSQL.Append(",CuentaTransfCtasBancoDestino");
            //vSQL.Append(",CuentaTransfCtasGastoComOrigen");
            //vSQL.Append(",CuentaTransfCtasGastoComDestino");
            //vSQL.Append(",CuentaTransfCtasBancoOrigen");
            //vSQL.Append(",TransfCtasSigasTipoComprobante");
            //vSQL.Append(",EditarComprobanteAfterInsertTransfCtas");
            vSQL.Append(",NombreOperador");
            vSQL.Append(",FechaUltimaModificacion ");
            return LibViews.CreateCompatibilityView("ReglasDeContabilizacion", vSQL.ToString() + "  FROM Saw.ReglasDeContabilizacion", true);
        }
        public static bool CrearVistaDboVehiculo() {
            return LibViews.CreateCompatibilityView("Vehiculo", "SELECT ConsecutivoCompania, Consecutivo, Placa, serialVIN, NombreModelo, Ano, CodigoColor, CodigoCliente, NumeroPoliza, SerialMotor, NombreOperador, FechaUltimaModificacion FROM Saw.Vehiculo", true);
        }
        public static bool CrearVistaDboAuxiliar() {
            return new Galac.Contab.Dal.WinCont.clsAuxiliarMD().CrearVistaDboAuxiliar();
        }
        public static bool CrearVistaDboAlmacen() {
            return LibViews.CreateCompatibilityView("Almacen", "SELECT ConsecutivoCompania, Consecutivo, Codigo, NombreAlmacen, TipoDeAlmacen, ConsecutivoCliente, CodigoCc, Descripcion, NombreOperador, FechaUltimaModificacion FROM Saw.Almacen", true);
        }
        public static bool CrearVistaDboCuentaBancaria() {
            return LibViews.CreateCompatibilityView("CuentaBancaria", "SELECT ConsecutivoCompania, Codigo, Status, NumeroCuenta, NombreCuenta,CodigoBanco, NombreSucursal, TipoCtaBancaria, ManejaDebitoBancario, ManejaCreditoBancario, SaldoDisponible, NombreDeLaMoneda, NombrePlantillaCheque, CuentaContable, CodigoMoneda, NombreOperador, FechaUltimaModificacion FROM Saw.CuentaBancaria", true);
        }
        public static bool CrearVistaDboConceptoBancario() {
            return LibViews.CreateCompatibilityView("ConceptoBancario", "SELECT Consecutivo, Codigo, Descripcion, Tipo, NombreOperador, FechaUltimaModificacion FROM Adm.ConceptoBancario", true);
        }
        public static bool CrearVistaDboTarifaN2() {
            return LibViews.CreateCompatibilityView("TarifaN2", "SELECT  Consecutivo, UTDesde, UTHasta, Porcentaje, Sustraendo, NombreOperador, FechaUltimaModificacion FROM Comun.TarifaN2", true);
        }
        public static bool CrearVistaDboTablaRetencion() {
            return LibViews.CreateCompatibilityView("TablaRetencion", "SELECT TipoDePersona, Codigo, CodigoSeniat, TipoDePago, Comentarios, BaseImponible, Tarifa, ParaPagosMayoresDe, FechaAplicacion, Sustraendo, AcumulaParaPJND, SecuencialDePlantilla, CodigoMoneda, FechaDeInicioDeVigencia, NombreOperador, FechaUltimaModificacion FROM Comun.TablaRetencion", true);
        }
        public static bool CrearVistaDboProveedor() {
            return LibViews.CreateCompatibilityView("Proveedor", "SELECT ConsecutivoCompania,CodigoProveedor,Consecutivo,NombreProveedor,Contacto,NumeroRIF,NumeroNit,TipoDePersona,CodigoRetencionUsual,Telefonos,Direccion,Fax,Email,TipodeProveedor,TipoDeProveedorDeLibrosFiscales,PorcentajeRetencionIva,CuentaContableCxp,CuentaContableGastos,CuentaContableAnticipo,CodigoLote,Beneficiario,UsarBeneficiarioImpCheq,TipoDocumentoIdentificacion,EsAgenteDeRetencionIva,ApellidoPaterno,ApellidoMaterno,Nombre,NumeroCuentaBancaria,CodigoContribuyente,NumeroRUC,NombreOperador,FechaUltimaModificacion FROM Adm.Proveedor", true);
        }

        public static bool CrearVistaDboCompra() {
            bool vResult = false;
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT Adm.Compra.ConsecutivoCompania, ");
            vSQL.AppendLine("Adm.Compra.Consecutivo, ");
            vSQL.AppendLine("'N' + CAST(Adm.Compra.Consecutivo AS VARCHAR(10)) AS NumeroSecuencial,  ");
            if(LibDefGen.ProgramInfo.IsCountryPeru()) {
                vSQL.AppendLine("Adm.Compra.Serie + CASE LEN(Adm.Compra.Serie) WHEN 0 THEN '' ELSE '-' END + Adm.Compra.Numero AS Numero, ");
            } else {
                vSQL.AppendLine("Adm.Compra.Numero AS Numero, ");
            }
            vSQL.AppendLine("Adm.Compra.Fecha, Adm.Proveedor.CodigoProveedor,  ");
            vSQL.AppendLine("Adm.Compra.Moneda,  ");
            vSQL.AppendLine("CAST(Adm.Compra.CambioABolivares AS MONEY) AS CambioABolivares,  ");
            vSQL.AppendLine("Adm.Compra.GenerarCXP,  ");
            vSQL.AppendLine("CAST(Adm.Compra.TotalRenglones AS MONEY) AS TotalRenglones,  ");
            vSQL.AppendLine("CAST(Adm.Compra.TotalOtrosGastos  AS MONEY) AS OtrosGastos,  ");
            vSQL.AppendLine("CAST(Adm.Compra.TotalCompra AS MONEY) AS TotalCompra,  ");
            vSQL.AppendLine("Adm.Compra.Comentarios,  ");
            vSQL.AppendLine("CAST(SUM(Adm.CompraDetalleArticuloInventario.MontoDistribucion) AS MONEY) AS DistribuirGastos,  ");
            vSQL.AppendLine("CAST(0  AS MONEY) AS PorcentajeDeDistribucion,  ");
            vSQL.AppendLine("'N' AS EsUnaOrdenDeCompra, ");
            vSQL.AppendLine("Adm.Compra.StatusCompra,  ");
            vSQL.AppendLine("Adm.Compra.FechaDeAnulacion,  ");
            vSQL.AppendLine("Adm.Compra.TipoDeCompra,  ");
            vSQL.AppendLine("Adm.Compra.NoFacturaNotaEntrega,  ");
            vSQL.AppendLine("Adm.Compra.TipoDeCompraParaCxP,  ");
            vSQL.AppendLine("Adm.Compra.CodigoMoneda,  ");
            vSQL.AppendLine("Adm.Compra.ConsecutivoAlmacen,  ");
            vSQL.AppendLine("Adm.Compra.NombreOperador,  ");
            vSQL.AppendLine("Saw.Almacen.Codigo AS CodigoAlmacen  ");
            vSQL.AppendLine("FROM Adm.Compra INNER JOIN ");
            vSQL.AppendLine("Adm.Proveedor ON Adm.Proveedor.ConsecutivoCompania = Adm.Compra.ConsecutivoCompania AND Adm.Proveedor.Consecutivo = Adm.Compra.ConsecutivoProveedor INNER JOIN ");
            vSQL.AppendLine("Adm.CompraDetalleArticuloInventario ON Adm.Compra.ConsecutivoCompania = Adm.CompraDetalleArticuloInventario.ConsecutivoCompania AND ");
            vSQL.AppendLine("Adm.Compra.Consecutivo = Adm.CompraDetalleArticuloInventario.ConsecutivoCompra ");
            vSQL.AppendLine("INNER JOIN Saw.Almacen ON Adm.Compra.ConsecutivoCompania = Saw.Almacen.ConsecutivoCompania AND Adm.Compra.ConsecutivoAlmacen = Saw.Almacen.Consecutivo ");
            vSQL.AppendLine("GROUP BY Adm.Compra.ConsecutivoCompania, Adm.Compra.Consecutivo, Adm.Compra.Numero, Adm.Compra.Serie + CASE LEN(Adm.Compra.Serie) WHEN 0 THEN '' ELSE '-' END + Adm.Compra.Numero, Adm.Compra.Fecha,  ");
            vSQL.AppendLine("Adm.Proveedor.CodigoProveedor, Adm.Compra.Moneda, Adm.Compra.CambioABolivares, Adm.Compra.GenerarCXP, Adm.Compra.TotalRenglones, Adm.Compra.TotalOtrosGastos, Adm.Compra.TotalCompra, ");
            vSQL.AppendLine("Adm.Compra.Comentarios, Adm.Compra.StatusCompra, Adm.Compra.FechaDeAnulacion, Adm.Compra.TipoDeCompra, Adm.Compra.NoFacturaNotaEntrega, Adm.Compra.TipoDeCompraParaCxP,  ");
            vSQL.AppendLine("Adm.Compra.CodigoMoneda, Adm.Compra.ConsecutivoAlmacen, Adm.Compra.NombreOperador, Saw.Almacen.Codigo ");

            vSQL.AppendLine("UNION SELECT Adm.OrdenDeCompra.ConsecutivoCompania, ");
            vSQL.AppendLine("Adm.OrdenDeCompra.Consecutivo, ");
            vSQL.AppendLine("'S' + CAST(Adm.OrdenDeCompra.Consecutivo AS VARCHAR(10)) AS NumeroSecuencial,  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.Serie + CASE LEN(Adm.OrdenDeCompra.Serie) WHEN 0 THEN '' ELSE '-' END + Adm.OrdenDeCompra.Numero AS Numero, ");
            vSQL.AppendLine("Adm.OrdenDeCompra.Fecha, Adm.Proveedor.CodigoProveedor,  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.Moneda,  ");
            vSQL.AppendLine("CAST(Adm.OrdenDeCompra.CambioABolivares AS MONEY) AS CambioABolivares,  ");
            vSQL.AppendLine("'N',  ");
            vSQL.AppendLine("CAST(Adm.OrdenDeCompra.TotalRenglones AS MONEY) AS TotalRenglones,  ");
            vSQL.AppendLine("CAST(0  AS MONEY) AS OtrosGastos,  ");
            vSQL.AppendLine("CAST(Adm.OrdenDeCompra.TotalCompra AS MONEY) AS TotalCompra,  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.Comentarios,  ");
            vSQL.AppendLine("CAST(SUM(0) AS MONEY) AS DistribuirGastos,  ");
            vSQL.AppendLine("CAST(0  AS MONEY) AS PorcentajeDeDistribucion,  ");
            vSQL.AppendLine("'S' AS EsUnaOrdenDeCompra, ");
            vSQL.AppendLine("Adm.OrdenDeCompra.StatusOrdenDeCompra,  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.FechaDeAnulacion,  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.TipoDeCompra,  ");
            vSQL.AppendLine("'',  ");
            vSQL.AppendLine("'0',  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.CodigoMoneda,  ");
            vSQL.AppendLine("0,  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.NombreOperador,  ");
            vSQL.AppendLine("''  ");
            vSQL.AppendLine("FROM Adm.OrdenDeCompra INNER JOIN ");
            vSQL.AppendLine("Adm.Proveedor ON Adm.Proveedor.ConsecutivoCompania = Adm.OrdenDeCompra.ConsecutivoCompania AND Adm.Proveedor.Consecutivo = Adm.OrdenDeCompra.ConsecutivoProveedor INNER JOIN ");
            vSQL.AppendLine("Adm.OrdenDeCompraDetalleArticuloInventario ON Adm.OrdenDeCompra.ConsecutivoCompania = Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoCompania AND ");
            vSQL.AppendLine("Adm.OrdenDeCompra.Consecutivo = Adm.OrdenDeCompraDetalleArticuloInventario.ConsecutivoOrdenDeCompra ");
            
            vSQL.AppendLine("GROUP BY Adm.OrdenDeCompra.ConsecutivoCompania, Adm.OrdenDeCompra.Consecutivo, Adm.OrdenDeCompra.Serie + CASE LEN(Adm.OrdenDeCompra.Serie) WHEN 0 THEN '' ELSE '-' END + Adm.OrdenDeCompra.Numero, Adm.OrdenDeCompra.Fecha,  ");
            vSQL.AppendLine("Adm.Proveedor.CodigoProveedor, Adm.OrdenDeCompra.Moneda, Adm.OrdenDeCompra.CambioABolivares,  Adm.OrdenDeCompra.TotalRenglones, Adm.OrdenDeCompra.TotalCompra, ");
            vSQL.AppendLine("Adm.OrdenDeCompra.Comentarios, Adm.OrdenDeCompra.StatusOrdenDeCompra, Adm.OrdenDeCompra.FechaDeAnulacion, Adm.OrdenDeCompra.TipoDeCompra,  ");
            vSQL.AppendLine("Adm.OrdenDeCompra.CodigoMoneda,  Adm.OrdenDeCompra.NombreOperador");

            vResult = LibGalac.Aos.Dal.LibViews.CreateCompatibilityView("Compra", vSQL.ToString(), true);
            vSQL = new StringBuilder();
            vSQL.AppendLine("  SELECT  ");
            vSQL.AppendLine(" ConsecutivoCompania,  ");
            vSQL.AppendLine(" 'N' + CAST(ConsecutivoCompra AS VARCHAR(10)) AS NumeroSecuencialCompra,  ");
            vSQL.AppendLine(" Consecutivo AS ConsecutivoRenglon,  ");
            vSQL.AppendLine(" CodigoArticulo,  ");
            vSQL.AppendLine(" CAST(Cantidad AS MONEY) AS Cantidad, ");
            vSQL.AppendLine(" CAST(PrecioUnitario AS MONEY) AS CostoUnitario,  ");
            vSQL.AppendLine(" CAST(CantidadRecibida AS MONEY) AS CantidadRecibida, ");
            vSQL.AppendLine(" 'N' AS EsRenglonDeOrdenDeCompra ");
            vSQL.AppendLine(" FROM Adm.CompraDetalleArticuloInventario ");

            vSQL.AppendLine(" UNION SELECT  ");
            vSQL.AppendLine(" ConsecutivoCompania,  ");
            vSQL.AppendLine(" 'S' + CAST(ConsecutivoOrdenDeCompra AS VARCHAR(10)) AS NumeroSecuencialCompra,  ");
            vSQL.AppendLine(" Consecutivo AS ConsecutivoRenglon,  ");
            vSQL.AppendLine(" CodigoArticulo,  ");
            vSQL.AppendLine(" CAST(Cantidad AS MONEY) AS Cantidad, ");
            vSQL.AppendLine(" CAST(CostoUnitario AS MONEY) AS CostoUnitario,  ");
            vSQL.AppendLine(" CAST(CantidadRecibida AS MONEY) AS CantidadRecibida, ");
            vSQL.AppendLine(" 'S' AS EsRenglonDeOrdenDeCompra ");
            vSQL.AppendLine(" FROM Adm.OrdenDeCompraDetalleArticuloInventario ");


            vResult = vResult & LibGalac.Aos.Dal.LibViews.CreateCompatibilityView("RenglonCompra", vSQL.ToString(), true);
            vSQL = new StringBuilder();

            vSQL.AppendLine(" SELECT ");
            vSQL.AppendLine(" ConsecutivoCompania, ");
            vSQL.AppendLine(" 'N' + CAST(ConsecutivoCompra AS VARCHAR(10)) AS NumeroSecuencialCompra       ");
            vSQL.AppendLine(" ,Consecutivo AS ConsecutivoRenglon ");
            vSQL.AppendLine(" ,CodigoArticulo ");
            vSQL.AppendLine(" ,Serial ");
            vSQL.AppendLine(" ,Rollo ");
            vSQL.AppendLine(" ,CAST(Cantidad AS MONEY) AS Cantidad ");
            vSQL.AppendLine(" FROM Adm.CompraDetalleSerialRollo ");


            vResult = vResult & LibGalac.Aos.Dal.LibViews.CreateCompatibilityView("RenglonCompraXSerial", vSQL.ToString(), true);
            return vResult;
        }
	
	     public static bool CrearVistaDboLineaDeProducto() {
            return LibViews.CreateCompatibilityView("LineaDeProducto", "SELECT ConsecutivoCompania,Nombre,CentroDeCosto,PorcentajeComision FROM Adm.LineaDeProducto", true);
        }
		
        public static bool CrearVistaDboImpTrasnBancarias() {
            return LibViews.CreateCompatibilityView("ImpTransacBancarias","SELECT FechaDeInicioDeVigencia,AlicuotaAlDebito,AlicuotaAlCredito FROM Adm.ImpTransacBancarias",true);
        }

        public static bool CrearVistaDboCaja() {
            bool vResult = false;
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT ");
            vSQL.AppendLine("ConsecutivoCompania,");
            vSQL.AppendLine("Consecutivo AS ConsecutivoCaja,");
            vSQL.AppendLine("NombreCaja,");
            vSQL.AppendLine("UsaGaveta,");
            vSQL.AppendLine("Puerto,");
            vSQL.AppendLine("Comando,");
            vSQL.AppendLine("PermitirAbrirSinSupervisor,");
            vSQL.AppendLine("UsaAccesoRapido,");
            vSQL.AppendLine("UsaMaquinaFiscal,");
            vSQL.AppendLine("FamiliaImpresoraFiscal,");
            vSQL.AppendLine("ModeloDeMaquinaFiscal,");
            vSQL.AppendLine("SerialDeMaquinaFiscal,");
            vSQL.AppendLine("PuertoMaquinaFiscal,");
            vSQL.AppendLine("AbrirGavetaDeDinero,");
            vSQL.AppendLine("UltimoNumeroCompFiscal AS PrimerNumeroCompFiscal,");
            vSQL.AppendLine("UltimoNumeroNCFiscal AS NumeroNCFiscal,");          
            vSQL.AppendLine("IpParaConexion,");
            vSQL.AppendLine("MascaraSubred,");
            vSQL.AppendLine("Gateway,");
            vSQL.AppendLine("'0' AS NumeroDeCaja,");
            vSQL.AppendLine("PermitirDescripcionDelArticuloExtendida,");
            vSQL.AppendLine("PermitirNombreDelClienteExtendido,");
            vSQL.AppendLine("UsarModoDotNet,");
            vSQL.AppendLine("TipoConexion,");
            vSQL.AppendLine("NombreOperador,");
            vSQL.AppendLine("FechaUltimaModificacion ");
            vSQL.AppendLine("FROM Adm.Caja");
            vResult = LibViews.CreateCompatibilityView("Caja",vSQL.ToString(),true);
            vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT ");
            vSQL.AppendLine("ConsecutivoCompania,");
            vSQL.AppendLine("Consecutivo AS ConsecutivoACC,");
            vSQL.AppendLine("ConsecutivoCaja,");
            vSQL.AppendLine("NombreDelUsuario,");
            vSQL.AppendLine("MontoApertura,");
            vSQL.AppendLine("MontoCierre,");
            vSQL.AppendLine("MontoEfectivo,");
            vSQL.AppendLine("MontoTarjeta,");
            vSQL.AppendLine("MontoCheque,");
            vSQL.AppendLine("Fecha,");
            vSQL.AppendLine("HoraApertura,");
            vSQL.AppendLine("HoraCierre,");
            vSQL.AppendLine("CajaCerrada,");
            vSQL.AppendLine("NombreOperador,");
            vSQL.AppendLine("FechaUltimaModificacion ");
            vSQL.AppendLine("FROM  Adm.CajaApertura ");
            vResult &= LibViews.CreateCompatibilityView("CajaApertura",vSQL.ToString(),true);
            return vResult;
        }

        public static bool CrearVistaDboCambio() {
            return LibViews.CreateCompatibilityView("Cambio", "SELECT Cambio.CodigoMoneda, Cambio.FechaDeVigencia, Nombre, Cambio.CambioAMonedaLocal As CambioABolivares, Cambio.NombreOperador, Cambio.FechaUltimaModificacion FROM Comun.Cambio INNER JOIN dbo.Moneda ON dbo.Moneda.Codigo = Comun.Cambio.CodigoMoneda", true);
        }

        public static bool CrearVistaDboVendedor() {
            return LibViews.CreateCompatibilityView("Vendedor", "SELECT ConsecutivoCompania, Codigo, Nombre, RIF, StatusVendedor, Direccion, Ciudad, ZonaPostal, Telefono, Fax, Email, Notas, ComisionPorVenta, ComisionPorCobro, TopeInicialVenta1, TopeFinalVenta1, PorcentajeVentas1, TopeFinalVenta2, PorcentajeVentas2, TopeFinalVenta3, PorcentajeVentas3, TopeFinalVenta4, PorcentajeVentas4, TopeFinalVenta5, PorcentajeVentas5, TopeInicialCobranza1, TopeFinalCobranza1, PorcentajeCobranza1, TopeFinalCobranza2, PorcentajeCobranza2, TopeFinalCobranza3, PorcentajeCobranza3, TopeFinalCobranza4, PorcentajeCobranza4, TopeFinalCobranza5, PorcentajeCobranza5, UsaComisionPorVenta, UsaComisionPorCobranza, CodigoLote, TipoDocumentoIdentificacion, NombreOperador, FechaUltimaModificacion, RutaDeComercializacion FROM Adm.Vendedor", true);
        }
    }

}
  
