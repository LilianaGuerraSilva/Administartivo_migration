using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.SttDef;
using Galac.Adm.Ccl.Venta;
using Galac.Saw.Ccl.Cliente;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Brl.Venta {
    public partial class clsFacturaRapidaNav : LibBaseNavMaster<IList<FacturaRapida>, IList<FacturaRapida>>, IFacturaRapidaPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsFacturaRapidaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<FacturaRapida>, IList<FacturaRapida>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_FacturaRapidaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<FacturaRapida>, IList<FacturaRapida>> instanciaDal = new Galac.Adm.Dal.Venta.clsFacturaRapidaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_FacturaRapidaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Punto de Venta":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Punto de Venta", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Vendedor":
                    vPdnModule = new Galac.Saw.Brl.Vendedor.clsVendedorNav();
                    vResult = vPdnModule.GetDataForList("Punto de Venta", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Caja":
                //    vPdnModule = new Galac.dbo.Brl.ComponenteNoEspecificado.clsCajaNav();
                //    vResult = vPdnModule.GetDataForList("Punto de Venta", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                case "Articulo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Punto de Venta", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Moneda":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsMonedaNav();
                    vResult = vPdnModule.GetDataForList("Punto de Venta", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<FacturaRapida> refData) {
            FillWithForeignInfoFacturaRapidaDetalle(ref refData);
        }
        #region FacturaRapidaDetalle

        private void FillWithForeignInfoFacturaRapidaDetalle(ref IList<FacturaRapida> refData) {
            //XElement vInfoConexion = FindInfoArticuloInventario(refData);
            //var vListArticuloInventario = (from vRecord in vInfoConexion.Descendants("GpResult")
            //                          select new {
            //                              ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
            //                              Codigo = vRecord.Element("Codigo").Value, 
            //                              Descripcion = vRecord.Element("Descripcion").Value, 
            //                              LineaDeProducto = vRecord.Element("LineaDeProducto").Value, 
            //                              StatusdelArticulo = vRecord.Element("StatusdelArticulo").Value, 
            //                              TipoDeArticulo = vRecord.Element("TipoDeArticulo").Value, 
            //                              AlicuotaIVA = vRecord.Element("AlicuotaIVA").Value, 
            //                              Categoria = vRecord.Element("Categoria").Value, 
            //                              Marca = vRecord.Element("Marca").Value, 
            //                              FechaDeVencimiento = vRecord.Element("FechaDeVencimiento").Value, 
            //                              UnidadDeVenta = vRecord.Element("UnidadDeVenta").Value, 
            //                              TipoArticuloInv = vRecord.Element("TipoArticuloInv").Value
            //                          }).Distinct();
            //foreach(FacturaRapida vItem in refData) {
            //    vItem.DetailFacturaRapidaDetalle = 
            //        new System.Collections.ObjectModel.ObservableCollection<FacturaRapidaDetalle>((
            //            from vDetail in vItem.DetailFacturaRapidaDetalle
            //            join vArticuloInventario in vListArticuloInventario
            //            on new {Codigo = vDetail.Articulo, ConsecutivoCompania = vDetail.ConsecutivoCompania}
            //            equals
            //            new { Codigo = vArticuloInventario.Codigo, ConsecutivoCompania = vArticuloInventario.ConsecutivoCompania}
            //            on new {Descripcion = vDetail.Descripcion, ConsecutivoCompania = vDetail.ConsecutivoCompania}
            //            equals
            //            new { Descripcion = vArticuloInventario.Descripcion, ConsecutivoCompania = vArticuloInventario.ConsecutivoCompania}
            //            select new FacturaRapidaDetalle {
            //                ConsecutivoCompania = vDetail.ConsecutivoCompania, 
            //                NumeroFactura = vDetail.NumeroFactura, 
            //                TipoDeDocumentoAsEnum = vDetail.TipoDeDocumentoAsEnum, 
            //                ConsecutivoRenglon = vDetail.ConsecutivoRenglon, 
            //                Articulo = vDetail.Articulo, 
            //                Descripcion = vDetail.Descripcion, 
            //                Cantidad = vDetail.Cantidad, 
            //                PrecioSinIVA = vDetail.PrecioSinIVA, 
            //                PrecioConIVA = vDetail.PrecioConIVA, 
            //                MontoBrutoSinIva = vDetail.MontoBrutoSinIva, 
            //                MontoBrutoConIva = vDetail.MontoBrutoConIva, 
            //                TotalRenglon = vDetail.TotalRenglon, 
            //                Serial = vDetail.Serial, 
            //                Rollo = vDetail.Rollo
            //            }).ToList<FacturaRapidaDetalle>());
            //}
        }

        private XElement FindInfoArticuloInventario(IList<FacturaRapida> valData) {
            XElement vXElement = new XElement("GpData");
            foreach (FacturaRapida vItem in valData) {
                vXElement.Add(FilterFacturaRapidaDetalleByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("FacturaRapida", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterFacturaRapidaDetalleByDistinctArticuloInventario(FacturaRapida valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailFacturaRapidaDetalle.Distinct()
                select new XElement("GpResult",
                    new XElement("Articulo", vEntity.Articulo)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        //private XElement FindInfoArticuloInventario(IList<FacturaRapida> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(FacturaRapida vItem in valData) {
        //        vXElement.Add(FilterFacturaRapidaDetalleByDistinctArticuloInventario(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
        //    XElement vXElementResult = insArticuloInventario.GetFk("FacturaRapida", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
        //    return vXElementResult;
        //}

        //private XElement FilterFacturaRapidaDetalleByDistinctArticuloInventario(FacturaRapida valMaster) {
        //    XElement vXElement = new XElement("GpData",
        //        from vEntity in valMaster.DetailFacturaRapidaDetalle.Distinct()
        //        select new XElement("GpResult",
        //            new XElement("Descripcion", vEntity.Descripcion)));
        //    return vXElement;
        //}

        //private StringBuilder ParametersGetFKArticuloInventarioForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
        //    StringBuilder vResult = new StringBuilder();
        //    LibGpParams vParams = new LibGpParams();
        //    vParams.AddReturn();
        //    vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
        //    vParams.AddInXml("XmlData", valXElement);
        //    vResult = vParams.Get();
        //    return vResult;
        //}
        #endregion //FacturaRapidaDetalle

        XElement IFacturaRapidaPdn.FindByNumero(int valConsecutivoCompania, string valNumero) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Numero", valNumero, 11);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Factura");
            SQL.AppendLine("WHERE Numero = @Numero");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados


        LibResponse IFacturaRapidaPdn.InsertarClienteDesdeFacturaRapida(string valNombre, string valNumeroRIF, string valDireccion, string valTelefono, ref string refCodigo, eTipoDocumentoIdentificacion valTipoDocumentoIdentificacion) {
            Galac.Saw.Ccl.Cliente.IClientePdn vClientePdn = new Galac.Saw.Brl.Cliente.clsClienteNav();
            return vClientePdn.InsertClienteForExternalRecord(valNombre, valNumeroRIF, valDireccion, valTelefono, ref refCodigo, valTipoDocumentoIdentificacion);
        }

        string IFacturaRapidaPdn.GenerarNumeroDeFactura(int valConsecutivoCompania) {
            string vResult = "";
            RegisterClient();
            LibGpParams vParams = new LibGpParams();
            int vNroCerosAlaIzquiera = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("FacturaRapida", "NroCerosAlaIzquiera");
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("PrefijoFactura", "", vNroCerosAlaIzquiera);
            vParams.AddInEnum("TipoDeDocumento", (int)eTipoDocumentoFactura.ComprobanteFiscal);
            vParams.AddInEnum("StatusFactura", (int)eStatusFactura.Emitida);
            vParams.AddInInteger("NroCerosAlaIzquiera", vNroCerosAlaIzquiera);
            StringBuilder vParamsText = vParams.Get();
            XElement xResult = _Db.QueryInfo(eProcessMessageType.SpName, "Gp_FacturaGeneraProximoNumero", vParams.Get());
            vResult = LibXml.GetPropertyString(xResult, "Numero");
            return vResult;
        }

        int IFacturaRapidaPdn.SiguienteNumeroParaResumen(int valConsecutivoCompania, XElement xData, string valSerialMaquinaFiscal) {

            int vResult = 0;
            int vMaximo = 0;
            string vNumero = "";
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(LibXml.GetPropertyString(xData, "TipoDeDocumento"));
            DateTime vFechaFactura = LibConvert.ToDate(LibXml.GetPropertyString(xData, "Fecha"));
            eTipoDeVenta vTipoDeVenta = (eTipoDeVenta)LibConvert.DbValueToEnum(LibXml.GetPropertyString(xData, "TipoDeVenta"));
            DateTime vFechaDelUltimoResumen;

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("SerialMaquinaFiscal", valSerialMaquinaFiscal, 15);
            vSql.AppendLine(" SELECT TOP 1 MAX(NumeroParaResumen) AS maximo, ");
            vSql.AppendLine(" TipoDeVenta, Fecha, Numero, TipoDeDocumento ");
            vSql.AppendLine(" FROM Factura WHERE ConsecutivoCompania= @ConsecutivoCompania ");
            vSql.AppendLine(" AND SerialMaquinaFiscal= @SerialMaquinaFiscal ");
            vSql.AppendLine(" GROUP BY Factura.TipoDeVenta, Factura.NumeroParaResumen,Factura.Fecha, ");
            vSql.AppendLine(" Factura.TipoDeDocumento, Factura.Numero ORDER BY NumeroParaResumen DESC ");
            XElement xResult = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);

            if (xResult != null) {
                vMaximo = LibConvert.ToInt(LibXml.GetPropertyString(xResult, "maximo"));
                vTipoDeVenta = (eTipoDeVenta)LibConvert.DbValueToEnum(LibXml.GetPropertyString(xResult, "TipoDeVenta"));
                vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(LibXml.GetPropertyString(xResult, "TipoDeDocumento"));
                vNumero = LibXml.GetPropertyString(xResult, "Numero");
                vFechaDelUltimoResumen = LibConvert.ToDate(LibXml.GetPropertyString(xResult, "Fecha"));
                if (!vTipoDeVenta.Equals(vTipoDeVenta) || vTipoDeVenta.Equals(eTipoDeVenta.AContribuyente) || !vTipoDeDocumento.Equals(vTipoDeDocumento) || !vFechaDelUltimoResumen.Equals(vFechaFactura)) {
                    vResult = vMaximo + 1;
                } else {
                    vResult = vMaximo;
                }
            } else {
                vResult = 1;
            }
            return vResult;
        }

        bool IFacturaRapidaPdn.ActualizarFacturaEmitida(int valConsecutivoCompania, XElement xElementFacturaRapida, string valNumeroBorrador, int valNumeroParaResumen) {
            bool vResult = false;
            bool vUsaMaquinafiscal = true;
            string vNumeroFactura = LibXml.GetPropertyString(xElementFacturaRapida, "Numero");
            eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(LibXml.GetPropertyString(xElementFacturaRapida, "TipoDeDocumento"));
            int vConsecutivoCaja = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ConsecutivoCaja");
            eTipoDeVenta vTipoDeVenta = (eTipoDeVenta)LibConvert.DbValueToEnum(LibXml.GetPropertyString(xElementFacturaRapida, "TipoDeVenta"));
            string vSerialMaquinaFiscal = LibXml.GetPropertyString(xElementFacturaRapida, "SerialMaquinaFiscal");
            string vNumeroComprobanteFiscal = LibXml.GetPropertyString(xElementFacturaRapida, "NumeroComprobanteFiscal");
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();
            QAdvSql insQAdvSql = new QAdvSql("");

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Numero", vNumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", (int)eTipoDocumentoFactura.ComprobanteFiscal);
            vParams.AddInEnum("StatusFactura", (int)eStatusFactura.Emitida);
            vParams.AddInString("NumeroComprobanteFiscal", vNumeroComprobanteFiscal, 12);
            vParams.AddInString("SerialMaquinaFiscal", vSerialMaquinaFiscal, 15);
            vParams.AddInBoolean("UsaMaquinaFiscal", vUsaMaquinafiscal);
            vParams.AddInInteger("ConsecutivoCaja", vConsecutivoCaja);
            vParams.AddInBoolean("ImprimeFiscal", vUsaMaquinafiscal);
            vParams.AddInInteger("NumeroParaResumen", valNumeroParaResumen);

            vSql.AppendLine("UPDATE dbo.Factura");
            vSql.AppendLine(" SET Numero = @Numero ");
            vSql.AppendLine(" ,StatusFactura = @StatusFactura ");
            vSql.AppendLine(" ,SerialMaquinaFiscal = @SerialMaquinaFiscal ");
            vSql.AppendLine(" ,UsaMaquinaFiscal = @UsaMaquinaFiscal ");
            vSql.AppendLine(" ,NumeroComprobanteFiscal = @NumeroComprobanteFiscal ");
            vSql.AppendLine(" ,ImprimeFiscal = @ImprimeFiscal ");
            vSql.AppendLine(" ,NumeroParaResumen = @NumeroParaResumen ");
            vSql.AppendLine(" ,ConsecutivoCaja = ConsecutivoCaja ");
            vSql.AppendLine(" WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            vSql.AppendLine(" AND Numero = " + insQAdvSql.ToSqlValue(valNumeroBorrador));
            vSql.AppendLine(" AND TipoDeDocumento = @TipoDeDocumento ");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        bool IFacturaRapidaPdn.ExisteTasaDeCambioParaElDia(string valMoneda, DateTime valFecha, out decimal outTasa) {
            StringBuilder vSQL = new StringBuilder();
            bool vResult = false;
            outTasa = 0;
            vSQL.AppendLine("SELECT CodigoMoneda, CambioAbolivares FROM dbo.Cambio WHERE CodigoMoneda = @CodigoMoneda AND FechaDeVigencia = @Fecha ");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDateTime("Fecha", valFecha);
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (vData != null) {
                var vEntity = (from vRecord in vData.Descendants("GpResult")
                               select new {
                                   CodigoMoneda = vRecord.Element("CodigoMoneda").Value,
                                   CambioAbolivares = LibConvert.ToDec(vRecord.Element("CambioAbolivares"), 4)
                               }).Distinct();
                foreach (var item in vEntity) {
                    outTasa = item.CambioAbolivares;
                    vResult = true;
                }
            }

            return vResult;
        }

        bool IFacturaRapidaPdn.InsertaTasaDeCambioParaElDia(string valMoneda, DateTime valFechaVigencia, string valNombre, decimal valCambioAbolivares) {
            StringBuilder vSQL = new StringBuilder();
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInDateTime("FechaDeVigencia", valFechaVigencia);
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            vParams.AddInString("Nombre", valNombre, 80);
            vParams.AddInDecimal("CambioAbolivares", valCambioAbolivares, 4);
            vParams.AddInString("NombreOperador", ((CustomIdentity)System.Threading.Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vSQL.AppendLine("INSERT INTO dbo.Cambio (CodigoMoneda,FechaDeVigencia,Nombre,CambioAbolivares,NombreOperador, FechaUltimaModificacion) VALUES (@CodigoMoneda,@FechaDeVigencia,@Nombre,@CambioAbolivares,@NombreOperador,@FechaUltimaModificacion)");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0) > 0;
            return vResult;
        }

        decimal IFacturaRapidaPdn.TasaDeDolarVigente(string valMoneda) {
            StringBuilder vSQL = new StringBuilder();
            decimal vResult = 0;
            vSQL.AppendLine("SELECT CodigoMoneda, CambioAbolivares FROM dbo.Cambio WHERE CodigoMoneda = @CodigoMoneda ORDER BY FechaDeVigencia DESC ");
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("CodigoMoneda", valMoneda, 4);
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            if (vData != null) {
                var vEntity = (from vRecord in vData.Descendants("GpResult")
                               select new {
                                   CodigoMoneda = vRecord.Element("CodigoMoneda").Value,
                                   CambioAbolivares = LibConvert.ToDec(vRecord.Element("CambioAbolivares"), 4)
                               }).Distinct();
                foreach (var item in vEntity) {
                    vResult = item.CambioAbolivares;
                    break;
                }
            }
            return vResult;
        }

        bool IFacturaRapidaPdn.EsCuentaBancariaValidaParaCobro(int valConsecutivoCompania, string valCodigoCuentaBancariaDeCobroDirecto, string valCodigoMonedaEmpresa, out string outNombreMonedaCuentaBancaria) {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Status", LibConvert.EnumToDbValue((int) eStatusCtaBancaria.Activo), 1);
            vParams.AddInString("Codigo", valCodigoCuentaBancariaDeCobroDirecto, 5);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vSql.AppendLine("   SELECT TOP 1");
            vSql.AppendLine("   CodigoMoneda");
            vSql.AppendLine("   , NombreDeLaMoneda");
            vSql.AppendLine("   FROM Saw.CuentaBancaria");
            vSql.AppendLine("   WHERE");
            vSql.AppendLine("	    Status = @Status");
            vSql.AppendLine("	    AND Codigo = @Codigo");
            vSql.AppendLine("	    AND ConsecutivoCompania = @ConsecutivoCompania");
            XElement vData = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            outNombreMonedaCuentaBancaria = string.Empty;
            if (vData != null && vData.HasElements) {
                string vCodigoMonedaCuentaBancaria = LibXml.GetPropertyString(vData, "CodigoMoneda");
                outNombreMonedaCuentaBancaria = LibXml.GetPropertyString(vData, "NombreDeLaMoneda");
                if(LibString.S1IsEqualToS2(vCodigoMonedaCuentaBancaria, valCodigoMonedaEmpresa)) {
                    vResult = true;
                }
            }
            return vResult;
        }

    } //End of class clsFacturaRapidaNav

} //End of namespace Galac.Adm.Brl.Venta

