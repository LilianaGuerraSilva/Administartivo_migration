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
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.CAnticipo;
using Galac.Adm.Ccl.CAnticipo;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Brl.Venta {
    public partial class clsCobroDeFacturaRapidaNav:LibBaseNavMaster<IList<CobroDeFacturaRapida>, IList<CobroDeFacturaRapida>>, ILibPdn {
        private int _ContaforErrorPK;
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<CobroDeFacturaRapida>, IList<CobroDeFacturaRapida>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<CobroDeFacturaRapida>, IList<CobroDeFacturaRapida>> instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Forma de Cobro":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Forma Del Cobro":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
                    vResult = vPdnModule.GetDataForList("Forma de Cobro", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<CobroDeFacturaRapida> refData) {
            FillWithForeignInfoCobroDeFacturaRapidaDetalle(ref refData);
        }
        #region CobroDeFacturaRapidaDetalle

        private void FillWithForeignInfoCobroDeFacturaRapidaDetalle(ref IList<CobroDeFacturaRapida> refData) {
            XElement vInfoConexion = FindInfoFormaDelCobro(refData);
            var vListFormaDelCobro = (from vRecord in vInfoConexion.Descendants("GpResult")
                                      select new {
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          TipoDePago = vRecord.Element("TipoDePago").Value
                                      }).Distinct();
            foreach(CobroDeFacturaRapida vItem in refData) {
                vItem.DetailCobroDeFacturaRapidaDetalle = 
                    new System.Collections.ObjectModel.ObservableCollection<CobroDeFacturaRapidaDetalle>((
                        from vDetail in vItem.DetailCobroDeFacturaRapidaDetalle
                        join vFormaDelCobro in vListFormaDelCobro
                        on new {Codigo = vDetail.CodigoFormaDelCobro}
                        equals
                        new { Codigo = vFormaDelCobro.Codigo}
                        select new CobroDeFacturaRapidaDetalle {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania, 
                            CodigoFormaDelCobro = vDetail.CodigoFormaDelCobro, 
                            
                            MontoEfectivo = vDetail.MontoEfectivo, 
                            
                            MontoCheque = vDetail.MontoCheque, 
                            
                            MontoTarjeta = vDetail.MontoTarjeta, 
                            
                            MontoDeposito = vDetail.MontoDeposito, 
                            
                            MontoAnticipo = vDetail.MontoAnticipo
                        }).ToList<CobroDeFacturaRapidaDetalle>());
            }
        }

        private XElement FindInfoFormaDelCobro(IList<CobroDeFacturaRapida> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(CobroDeFacturaRapida vItem in valData) {
                vXElement.Add(FilterCobroDeFacturaRapidaDetalleByDistinctFormaDelCobro(vItem).Descendants("GpResult"));
            }
            ILibPdn insFormaDelCobro = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
            XElement vXElementResult = insFormaDelCobro.GetFk("CobroDeFacturaRapida", ParametersGetFKFormaDelCobroForXmlSubSet(vXElement));
            return vXElementResult;
        }

        private XElement FilterCobroDeFacturaRapidaDetalleByDistinctFormaDelCobro(CobroDeFacturaRapida valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaDetalle.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoFormaDelCobro", vEntity.CodigoFormaDelCobro)));
            return vXElement;
        }

        public void ActualizarCamposEnFactura(XElement xElementFacturaRapida, string valComprobanteFiscal, string valSerialMaquinaFiscal, List<RenglonCobroDeFactura> ListDeCobro) {
            XElement xFacturaClone = new XElement(xElementFacturaRapida);
            int vConsecutivoCompania = 0;
            string vCodigoAlmacen = "";
            DateTime vFechaFactura;
            string vNumeroFactura = "";
            string vNumeroBorrador = "";
            int vNumeroParaResumen = 0;
            int vConsecutivoMovimientoBancario = 0;
            string vNumeroCobranza = string.Empty;
            bool vResult = false;
            string vCodigoCliente = "";
            string vCodigoMoneda = "";
            IFacturaRapidaPdn insFacturaRapida = new clsFacturaRapidaNav();
            IActualizarArticuloInventarioPdn insActualizarArticulo = new clsActualizarArticuloInventarioNav();
            IMovimientoBancarioPuntoDeVentaPdn insGenerarMovimientoBancario = new clsMovimientoBancarioPuntoDeVentaNav();
            ICXCPdn insCXC = new clsCXCNav();
            ICobranzaPdn insCobranza = new clsCobranzaNav();
            IAnticipoPdn insCAnticipo = new clsAnticipoNav();

            vConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            vCodigoAlmacen = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida", "CodigoAlmacen");
            vFechaFactura = LibConvert.ToDate(LibXml.GetPropertyString(xElementFacturaRapida, "Fecha"));
            vNumeroBorrador = LibXml.GetPropertyString(xElementFacturaRapida, "Numero");
            vCodigoCliente = LibXml.GetPropertyString(xElementFacturaRapida, "CodigoCliente");
            vCodigoMoneda = LibXml.GetPropertyString(xElementFacturaRapida, "CodigoMoneda");

            try {
                using (System.Transactions.TransactionScope vScope = LibBusiness.CreateScope()) {
                    vNumeroParaResumen = insFacturaRapida.SiguienteNumeroParaResumen(vConsecutivoCompania, xElementFacturaRapida, valSerialMaquinaFiscal);
                    vNumeroFactura = insFacturaRapida.GenerarNumeroDeFactura(vConsecutivoCompania);
                    ActualizarCamposEnXmlFactura(xElementFacturaRapida, vNumeroFactura, valComprobanteFiscal, valSerialMaquinaFiscal);
                    vResult = insFacturaRapida.ActualizarFacturaEmitida(vConsecutivoCompania, xElementFacturaRapida, vNumeroBorrador, vNumeroParaResumen);
                    InsertarDatosParaVerificar(vConsecutivoCompania, xElementFacturaRapida, valComprobanteFiscal);
                    vResult = vResult && insActualizarArticulo.DescontarExistencia(vConsecutivoCompania, vNumeroFactura, vCodigoAlmacen, eTipoDocumentoFactura.ComprobanteFiscal, vFechaFactura);
                    vResult = vResult && insActualizarArticulo.DescontarEnAlmacen(vConsecutivoCompania, vNumeroFactura, vCodigoAlmacen, eTipoDocumentoFactura.ComprobanteFiscal, vFechaFactura);
                    vResult = vResult && insActualizarArticulo.DescontarExistenciaProductoCompuesto(vConsecutivoCompania, vNumeroFactura, vCodigoAlmacen, eTipoDocumentoFactura.ComprobanteFiscal, vFechaFactura);
                    vResult = vResult && insActualizarArticulo.DescontarEnAlmacenProductoCompuesto(vConsecutivoCompania, vNumeroFactura, vCodigoAlmacen, eTipoDocumentoFactura.ComprobanteFiscal, vFechaFactura);
                    vResult = vResult && insCXC.Insert(vConsecutivoCompania, xElementFacturaRapida);
                    if(!LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaCobroDirectoEnMultimoneda")) && vResult) {
                        vNumeroCobranza = insCobranza.GenerarProximoNumeroCobranza(vConsecutivoCompania);
                        vResult = vResult && insCobranza.InsertarCobranzaDesdePuntoDeVenta(vConsecutivoCompania,xElementFacturaRapida,vNumeroCobranza);
                        vResult = vResult && insCobranza.InsertarDocumentoCobradoDesdePuntoDeVenta(vConsecutivoCompania,xElementFacturaRapida,vNumeroCobranza);
                        vConsecutivoMovimientoBancario = insGenerarMovimientoBancario.GenerarProximoConsecutivoMovimiento(vConsecutivoCompania);
                        vResult = vResult && insGenerarMovimientoBancario.Insert(vConsecutivoCompania,vConsecutivoMovimientoBancario,vNumeroCobranza,xElementFacturaRapida);
                        vResult = vResult && insGenerarMovimientoBancario.ActualizarSaldoCuentaBancariaPuntoDeVenta(vConsecutivoCompania,xElementFacturaRapida);
                        vResult = vResult && insCAnticipo.GenerarAnticiposCobrados(vNumeroCobranza,vCodigoCliente,vCodigoMoneda,ListDeCobro);
                        if(vResult) {
                            vScope.Complete();
                        } else {
                            throw new GalacAlertException("Reintentando...");
                        }
                    } else {
                        if(vResult) {
                            vScope.Complete();
                        } else {
                            throw new GalacAlertException("Reintentando...");
                        }
                    }
                    
                }
            } catch (Exception vEx) {
                _ContaforErrorPK++;
                if (_ContaforErrorPK < 100) {
                    ActualizarCamposEnFactura(xFacturaClone, valComprobanteFiscal, valSerialMaquinaFiscal, ListDeCobro);
                } else {
                    throw vEx;
                }
            } finally {               
                LibBusiness.EndScopedBusinessProcess("");
            }
            if(LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","UsaCobroDirectoEnMultimoneda")) && vResult) {               
                clsCobroDeFacturaNav vCobroNav = new clsCobroDeFacturaNav();
                IList<string> outCobranzasGeneradas= new  List<string>();
                vCobroNav.GenerarCobranzaYMovimientoBancarioDeCobroEnMultimoneda(vConsecutivoCompania, vNumeroFactura, eTipoDocumentoFactura.ComprobanteFiscal, out outCobranzasGeneradas);
            }
        }

        private void ActualizarCamposEnXmlFactura(XElement valXmlFactura, string valNumerofactura, string valNumeroComporbanteFiscal, string valSerialMaquinaFiscal) {
            if (valXmlFactura != null) {
                var vRecords = valXmlFactura.Descendants("GpResult");
                if (vRecords != null && vRecords.FirstOrDefault() != null) {
                    var vRecord = vRecords.FirstOrDefault();
                    var vProperty = vRecord.Element("Numero");
                    if (vProperty != null && !LibString.IsNullOrEmpty(valNumerofactura, true)) {
                        vProperty.Value = valNumerofactura;
                    }
                    vProperty = vRecord.Element("NumeroComprobanteFiscal");
                    if (vProperty != null && LibString.IsNullOrEmpty(vProperty.Value, true)) {
                        vProperty.Value = valNumeroComporbanteFiscal;
                    }
                    vProperty = vRecord.Element("SerialMaquinaFiscal");
                    if (vProperty != null && LibString.IsNullOrEmpty(vProperty.Value, true)) {
                        vProperty.Value = valSerialMaquinaFiscal;
                    }
                }
            }
        }

        private StringBuilder ParametersGetFKFormaDelCobroForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //CobroDeFacturaRapidaDetalle
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICobroDeFacturaRapidaPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CobroDeFacturaRapida>, IList<CobroDeFacturaRapida>> instanciaDal = new clsCobroDeFacturaRapidaDat();
            IList<CobroDeFacturaRapida> vLista = new List<CobroDeFacturaRapida>();
            CobroDeFacturaRapida vCurrentRecord = new Galac.Adm.Dal.VentaCobroDeFacturaRapida();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroFactura = "";
            vCurrentRecord.TotalACobrar = 0;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CobroDeFacturaRapida> ParseToListEntity(XElement valXmlEntity) {
            List<CobroDeFacturaRapida> vResult = new List<CobroDeFacturaRapida>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CobroDeFacturaRapida vRecord = new CobroDeFacturaRapida();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroFactura"), null))) {
                    vRecord.NumeroFactura = vItem.Element("NumeroFactura").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalACobrar"), null))) {
                    vRecord.TotalACobrar = LibConvert.ToDec(vItem.Element("TotalACobrar"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo
        private void InsertarDatosParaVerificar(int valConsecutivoCompania, XElement xElementFacturaRapida, string valComprobanteFiscal) {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vQAdvSql = new QAdvSql("");
            LibGpParams vParams = new LibGpParams();
            string vNumeroFactura = LibXml.GetPropertyString(xElementFacturaRapida, "Numero");
            eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)LibConvert.DbValueToEnum(LibXml.GetPropertyString(xElementFacturaRapida, "TipoDeDocumento"));
            DateTime vFecha = LibConvert.ToDate(LibXml.GetPropertyString(xElementFacturaRapida, "Fecha"));
            string vTipoDeDocumentoFactura = LibConvert.ToStr((int)eTipoDocumentoFactura.ComprobanteFiscal);
            Random rnd = new Random();
            string valueIni = LibConvert.ToStr(LibConvert.ToInt(rnd.Next(1, 100)));
            string valueLast = LibConvert.ToStr(LibConvert.ToInt(rnd.Next(1, 100)));
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInEnum("TipoDeDocumento", (int)eTipoDocumentoFactura.ComprobanteFiscal);
            vParams.AddInString("NumeroFactura", vNumeroFactura,11);
            vParams.AddInDateTime("FechaFactura", vFecha);
            StringBuilder vSqlFactura =
            vSql.AppendLine("set dateformat dmy");
            vSql.AppendLine("INSERT INTO Escalada");
            vSql.AppendLine(" (Id, Escalada41, Escalada32, Escalada73, Escalada24, Escalada85, Escalada100) VALUES (NEWID(),@ConsecutivoCompania ");
            vSql.AppendLine(", @FechaFactura");
            vSql.AppendLine(", " + vQAdvSql.ToSqlValue(valueIni + "##" + vNumeroFactura + vTipoDeDocumentoFactura + "@@" + valueLast));
            vSql.AppendLine(", " + vQAdvSql.ToSqlValue(valComprobanteFiscal));
            vSql.AppendLine(", " + vQAdvSql.ToSqlValue(""));
            vSql.AppendLine(",  HASHBYTES('SHA2_256',( " +SqlFacturaParaVerificacion().ToString() +  ")))");
            bool vresult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;

        }

        private StringBuilder SqlFacturaParaVerificacion()
        {
            StringBuilder vSqlSb = new StringBuilder();
            vSqlSb.AppendLine("SELECT TOP 1 CONCAT(");
            vSqlSb.AppendLine("	CASE WHEN TipoDeDocumento = '5' OR TipoDeDocumento = '7' OR TipoDeDocumento = '9' THEN");
            vSqlSb.AppendLine("		factura.Numero");
            vSqlSb.AppendLine("	ELSE");
            vSqlSb.AppendLine("		factura.NumeroComprobanteFiscal");
            vSqlSb.AppendLine("	END");
            vSqlSb.AppendLine("	,TipoDeDocumento");
            vSqlSb.AppendLine("	,Fecha");
            vSqlSb.AppendLine("	, factura.HoraModificacion");
            vSqlSb.AppendLine("	, Cliente.NumeroRIF,factura.NumeroControl");
            vSqlSb.AppendLine("	, factura.MontoGravableAlicuota1");
            vSqlSb.AppendLine("	, factura.MontoGravableAlicuota2");
            vSqlSb.AppendLine("	, factura.MontoGravableAlicuota3");
            vSqlSb.AppendLine("	, factura.MontoIVAAlicuota1");
            vSqlSb.AppendLine("	, factura.MontoIVAAlicuota2");
            vSqlSb.AppendLine("	, factura.MontoIVAAlicuota3");
            vSqlSb.AppendLine("	, factura.TotalMontoExento");
            vSqlSb.AppendLine("	, factura.TotalRenglones");
            vSqlSb.AppendLine("	, factura.TotalBaseImponible");
            vSqlSb.AppendLine("	, factura.TotalIVA");
            vSqlSb.AppendLine("	, factura.TotalFactura");
            vSqlSb.AppendLine("	)");
            vSqlSb.AppendLine("FROM factura INNER JOIN Cliente ON factura.ConsecutivoCompania = Cliente.ConsecutivoCompania");
            vSqlSb.AppendLine("AND factura.CodigoCliente = Cliente.Codigo");
            vSqlSb.AppendLine("WHERE");
            vSqlSb.AppendLine("Numero = @NumeroFactura");
            vSqlSb.AppendLine("AND");
            vSqlSb.AppendLine("TipoDeDocumento = @TipoDeDocumento");
            vSqlSb.AppendLine("AND factura.ConsecutivoCompania = @ConsecutivoCompania");
            vSqlSb.AppendLine("AND StatusFactura = '0'");
            return vSqlSb;
        }
    } //End of class clsCobroDeFacturaRapidaNav

} //End of namespace Galac.Adm.Brl.Venta

