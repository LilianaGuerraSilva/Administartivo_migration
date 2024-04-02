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
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Brl.CajaChica {
    public partial class clsCxPNav : LibBaseNavMaster<IList<CxP>, IList<CxP>>, ICXPPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCxPNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<CxP>, IList<CxP>> GetDataInstance() {
            return new Galac.Adm.Dal.CajaChica.clsCxPDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.dbo.Dal.CajaChica.clsCxPDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.CajaChica.clsCxPDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_CxPSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<CxP>, IList<CxP>> instanciaDal = new Galac.Adm.Dal.CajaChica.clsCxPDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_CxPGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Cx P":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Proveedor":
                    vPdnModule = new Galac.Adm.Brl.GestionCompras.clsProveedorNav();
                    vResult = vPdnModule.GetDataForList("Cx P", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Moneda":
                //    vPdnModule = new Galac.Adm.Brl.Moneda.clsMonedaNav();
                //    vResult = vPdnModule.GetDataForList("Cx P", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                //case "Tipo De Documento Ley":
                //    vPdnModule = new Galac.Adm.Brl.TipoDeDocumentoLey.clsTipoDeDocumentoLeyNav();
                //    vResult = vPdnModule.GetDataForList("Cx P", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                case "Rendicion":
                    vPdnModule = new Galac.Adm.Brl.CajaChica.clsRendicionNav();
                    vResult = vPdnModule.GetDataForList("Cx P", ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Clasificador Actividad Económica":
                //    vPdnModule = new Galac.Comun.Brl.Impuesto.clsClasificadorActividadEconomicaNav();
                //    vResult = vPdnModule.GetDataForList("Cx P", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<CxP> refData) {
            FillWithForeignInfoRenglonImpuestoMunicipalRet(ref refData);
        }
        #region RenglonImpuestoMunicipalRet

        private void FillWithForeignInfoRenglonImpuestoMunicipalRet(ref IList<CxP> refData) {
            XElement vInfoConexion = FindInfoClasificadorActividadEconomica(refData);
            var vListClasificadorActividadEconomica = (from vRecord in vInfoConexion.Descendants("GpResult")
                                      select new {
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          CodigoActividad = vRecord.Element("CodigoActividad").Value
                                      }).Distinct();
            foreach(CxP vItem in refData) {
                vItem.DetailRenglonImpuestoMunicipalRet = 
                    new System.Collections.ObjectModel.ObservableCollection<RenglonImpuestoMunicipalRet>((
                        from vDetail in vItem.DetailRenglonImpuestoMunicipalRet
                        join vClasificadorActividadEconomica in vListClasificadorActividadEconomica
                        on new {Codigo = vDetail.CodigoRetencion}
                        equals
                        new { Codigo = vClasificadorActividadEconomica.Codigo}
                        select new RenglonImpuestoMunicipalRet {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania, 
                            Consecutivo = vDetail.Consecutivo, 
                            ConsecutivoCxp = vDetail.ConsecutivoCxp, 
                            CodigoRetencion = vDetail.CodigoRetencion, 
                            CodigoActividad = vClasificadorActividadEconomica.CodigoActividad, 
                            MontoBaseImponible = vDetail.MontoBaseImponible, 
                            AlicuotaRetencion = vDetail.AlicuotaRetencion, 
                            MontoRetencion = vDetail.MontoRetencion, 
                            TipoDeTransaccionAsEnum = vDetail.TipoDeTransaccionAsEnum, 
                            NombreOperador = vDetail.NombreOperador, 
                            FechaUltimaModificacion = vDetail.FechaUltimaModificacion
                        }).ToList<RenglonImpuestoMunicipalRet>());
            }
        }

        private XElement FindInfoClasificadorActividadEconomica(IList<CxP> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(CxP vItem in valData) {
                vXElement.Add(FilterRenglonImpuestoMunicipalRetByDistinctClasificadorActividadEconomica(vItem).Descendants("GpResult"));
            }
            //ILibPdn insClasificadorActividadEconomica = new Galac.Comun.Brl.Impuesto.clsClasificadorActividadEconomicaNav();
            //XElement vXElementResult = insClasificadorActividadEconomica.GetFk("CxP", ParametersGetFKClasificadorActividadEconomicaForXmlSubSet(vXElement));
            return null; // vXElementResult;
        }

        private XElement FilterRenglonImpuestoMunicipalRetByDistinctClasificadorActividadEconomica(CxP valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailRenglonImpuestoMunicipalRet.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoRetencion", vEntity.CodigoRetencion)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKClasificadorActividadEconomicaForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //RenglonImpuestoMunicipalRet
        #endregion //Metodos Generados
        /* Codigo de Ejemplo

        bool ICxPPdn.InsertarRegistroPorDefecto(int valConsecutivoCompania) {
            ILibDataComponent<IList<CxP>, IList<CxP>> instanciaDal = new clsCxPDat();
            IList<CxP> vLista = new List<CxP>();
            CxP vCurrentRecord = new CxP();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Numero = "";
            vCurrentRecord.TipoDeCxPAsEnum = eTipoDeCxC.Factura;
            vCurrentRecord.StatusAsEnum = eStatusDocumento.PorCancelar;
            vCurrentRecord.CodigoProveedor = "";
            vCurrentRecord.Fecha = LibDate.Today();
            vCurrentRecord.FechaCancelacion = LibDate.Today();
            vCurrentRecord.FechaVencimiento = LibDate.Today();
            vCurrentRecord.FechaAnulacion = LibDate.Today();
            vCurrentRecord.Moneda = "";
            vCurrentRecord.CambioABolivares = 0;
            vCurrentRecord.AplicaParaLibrodeComprasAsBool = false;
            vCurrentRecord.MontoExento = 0;
            vCurrentRecord.MontoGravado = 0;
            vCurrentRecord.MontoIva = 0;
            vCurrentRecord.MontoAbonado = 0;
            vCurrentRecord.MesDeAplicacion = 0;
            vCurrentRecord.AnoDeAplicacion = 0;
            vCurrentRecord.Observaciones = "";
            vCurrentRecord.CreditoFiscalAsEnum = eCreditoFiscal.Deducible;
            vCurrentRecord.TipoDeCompraAsEnum = eTipoDeCompra.ComprasExentas;
            vCurrentRecord.SeHizoLaRetencionAsBool = false;
            vCurrentRecord.MontoGravableAlicuotaGeneral = 0;
            vCurrentRecord.MontoGravableAlicuota2 = 0;
            vCurrentRecord.MontoGravableAlicuota3 = 0;
            vCurrentRecord.MontoIVAAlicuotaGeneral = 0;
            vCurrentRecord.MontoIVAAlicuota2 = 0;
            vCurrentRecord.MontoIVAAlicuota3 = 0;
            vCurrentRecord.NumeroPlanillaDeImportacion = "";
            vCurrentRecord.NumeroExpedienteDeImportacion = "";
            vCurrentRecord.TipoDeTransaccionAsEnum = eTipoDeTransaccionDeLibrosFiscales.Registro;
            vCurrentRecord.NumeroDeFacturaAfectada = "";
            vCurrentRecord.NumeroControl = "";
            vCurrentRecord.SeHizoLaRetencionIVAAsBool = false;
            vCurrentRecord.NumeroComprobanteRetencion = "";
            vCurrentRecord.FechaAplicacionRetIVA = LibDate.Today();
            vCurrentRecord.PorcentajeRetencionAplicado = 0;
            vCurrentRecord.MontoRetenido = 0;
            vCurrentRecord.OrigenDeLaRetencionAsEnum = eDondeSeEfectuaLaRetencionIVA.NoRetenida;
            vCurrentRecord.RetencionAplicadaEnPagoAsBool = false;
            vCurrentRecord.OrigenInformacionRetencionAsEnum = eTipoDeContribuyenteDelIva.ContribuyenteFormal;
            vCurrentRecord.CxPgeneradaPorAsEnum = eGeneradoPor.Usuario;
            vCurrentRecord.EsCxPhistoricaAsBool = false;
            vCurrentRecord.NumDiasDeVencimiento = 0;
            vCurrentRecord.IvaPorImportacionAG = 0;
            vCurrentRecord.IvaPorImportacionA2 = 0;
            vCurrentRecord.IvaPorImportacionA3 = 0;
            vCurrentRecord.NumeroDocOrigen = "";
            vCurrentRecord.CodigoLote = "";
            vCurrentRecord.GenerarAsientoDeRetiroEnCuentaAsBool = false;
            vCurrentRecord.TotalOtrosImpuestos = 0;
            vCurrentRecord.ConsecutivoCxP = 0;
            vCurrentRecord.EstaAsociadoARendicionAsBool = false;
            vCurrentRecord.CodigoTipoDeDocumentoLey = "";
            vCurrentRecord.AplicaDetraccionAsBool = false;
            vCurrentRecord.NumeroDetraccion = "";
            vCurrentRecord.CodigoDetraccion = "";
            vCurrentRecord.DescripcionDeDetraccion = "";
            vCurrentRecord.PorcentajeDetraccion = 0;
            vCurrentRecord.TotalDetraccion = 0;
            vCurrentRecord.StatusDetraccionAsEnum = eStatusDetraccion.Autodetraccion;
            vCurrentRecord.ConsecutivoMovimiento = 0;
            vCurrentRecord.FechaAplicacionImpuestoMunicipal = LibDate.Today();
            vCurrentRecord.NumeroComprobanteImpuestoMunicipal = "";
            vCurrentRecord.MontoRetenidoImpuestoMunicipal = 0;
            vCurrentRecord.ImpuestoMunicipalRetenidoAsBool = false;
            vCurrentRecord.NumeroControlDeFacturaAfectada = "";
            vCurrentRecord.AplicaIvaAlicuotaEspecialAsBool = false;
            vCurrentRecord.BaseImponibleIGTFML = 0;
            vCurrentRecord.AlicuotaIGTFML = 0;
            vCurrentRecord.MontoIGTFML = 0;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.ConsecutivoRendicion = 0;
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }
        */
        #region Metodos Creados
        private List<CxP> ParseToListEntity(XElement valXmlEntity) {
            List<CxP> vResult = new List<CxP>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CxP vRecord = new CxP();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Numero"), null))) {
                    vRecord.Numero = vItem.Element("Numero").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeCxP"), null))) {
                    vRecord.TipoDeCxP = vItem.Element("TipoDeCxP").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Status"), null))) {
                    vRecord.Status = vItem.Element("Status").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoProveedor"), null))) {
                    vRecord.CodigoProveedor = vItem.Element("CodigoProveedor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fecha"), null))) {
                    vRecord.Fecha = LibConvert.ToDate(vItem.Element("Fecha"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaCancelacion"), null))) {
                    vRecord.FechaCancelacion = LibConvert.ToDate(vItem.Element("FechaCancelacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaVencimiento"), null))) {
                    vRecord.FechaVencimiento = LibConvert.ToDate(vItem.Element("FechaVencimiento"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAnulacion"), null))) {
                    vRecord.FechaAnulacion = LibConvert.ToDate(vItem.Element("FechaAnulacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Moneda"), null))) {
                    vRecord.Moneda = vItem.Element("Moneda").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CambioABolivares"), null))) {
                    vRecord.CambioABolivares = LibConvert.ToDec(vItem.Element("CambioABolivares"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AplicaParaLibrodeCompras"), null))) {
                    vRecord.AplicaParaLibrodeCompras = vItem.Element("AplicaParaLibrodeCompras").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoExento"), null))) {
                    vRecord.MontoExento = LibConvert.ToDec(vItem.Element("MontoExento"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoGravado"), null))) {
                    vRecord.MontoGravado = LibConvert.ToDec(vItem.Element("MontoGravado"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoIva"), null))) {
                    vRecord.MontoIva = LibConvert.ToDec(vItem.Element("MontoIva"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoAbonado"), null))) {
                    vRecord.MontoAbonado = LibConvert.ToDec(vItem.Element("MontoAbonado"));
                }
                if(!(System.NullReferenceException.ReferenceEquals(vItem.Element("DiaDeAplicacion"),null))) {
                    vRecord.DiaDeAplicacion = LibConvert.ToInt(vItem.Element("DiaDeAplicacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MesDeAplicacion"), null))) {
                    vRecord.MesDeAplicacion = LibConvert.ToInt(vItem.Element("MesDeAplicacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AnoDeAplicacion"), null))) {
                    vRecord.AnoDeAplicacion = LibConvert.ToInt(vItem.Element("AnoDeAplicacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Observaciones"), null))) {
                    vRecord.Observaciones = vItem.Element("Observaciones").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CreditoFiscal"), null))) {
                    vRecord.CreditoFiscal = vItem.Element("CreditoFiscal").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeCompra"), null))) {
                    vRecord.TipoDeCompra = vItem.Element("TipoDeCompra").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("SeHizoLaRetencion"), null))) {
                    vRecord.SeHizoLaRetencion = vItem.Element("SeHizoLaRetencion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoGravableAlicuotaGeneral"), null))) {
                    vRecord.MontoGravableAlicuotaGeneral = LibConvert.ToDec(vItem.Element("MontoGravableAlicuotaGeneral"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoGravableAlicuota2"), null))) {
                    vRecord.MontoGravableAlicuota2 = LibConvert.ToDec(vItem.Element("MontoGravableAlicuota2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoGravableAlicuota3"), null))) {
                    vRecord.MontoGravableAlicuota3 = LibConvert.ToDec(vItem.Element("MontoGravableAlicuota3"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoIVAAlicuotaGeneral"), null))) {
                    vRecord.MontoIVAAlicuotaGeneral = LibConvert.ToDec(vItem.Element("MontoIVAAlicuotaGeneral"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoIVAAlicuota2"), null))) {
                    vRecord.MontoIVAAlicuota2 = LibConvert.ToDec(vItem.Element("MontoIVAAlicuota2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoIVAAlicuota3"), null))) {
                    vRecord.MontoIVAAlicuota3 = LibConvert.ToDec(vItem.Element("MontoIVAAlicuota3"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroPlanillaDeImportacion"), null))) {
                    vRecord.NumeroPlanillaDeImportacion = vItem.Element("NumeroPlanillaDeImportacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroExpedienteDeImportacion"), null))) {
                    vRecord.NumeroExpedienteDeImportacion = vItem.Element("NumeroExpedienteDeImportacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoDeTransaccion"), null))) {
                    vRecord.TipoDeTransaccion = vItem.Element("TipoDeTransaccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDeFacturaAfectada"), null))) {
                    vRecord.NumeroDeFacturaAfectada = vItem.Element("NumeroDeFacturaAfectada").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroControl"), null))) {
                    vRecord.NumeroControl = vItem.Element("NumeroControl").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("SeHizoLaRetencionIVA"), null))) {
                    vRecord.SeHizoLaRetencionIVA = vItem.Element("SeHizoLaRetencionIVA").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroComprobanteRetencion"), null))) {
                    vRecord.NumeroComprobanteRetencion = vItem.Element("NumeroComprobanteRetencion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAplicacionRetIVA"), null))) {
                    vRecord.FechaAplicacionRetIVA = LibConvert.ToDate(vItem.Element("FechaAplicacionRetIVA"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeRetencionAplicado"), null))) {
                    vRecord.PorcentajeRetencionAplicado = LibConvert.ToDec(vItem.Element("PorcentajeRetencionAplicado"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoRetenido"), null))) {
                    vRecord.MontoRetenido = LibConvert.ToDec(vItem.Element("MontoRetenido"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("OrigenDeLaRetencion"), null))) {
                    vRecord.OrigenDeLaRetencion = vItem.Element("OrigenDeLaRetencion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("RetencionAplicadaEnPago"), null))) {
                    vRecord.RetencionAplicadaEnPago = vItem.Element("RetencionAplicadaEnPago").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("OrigenInformacionRetencion"), null))) {
                    vRecord.OrigenInformacionRetencion = vItem.Element("OrigenInformacionRetencion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CxPgeneradaPor"), null))) {
                    vRecord.CxPgeneradaPor = vItem.Element("CxPgeneradaPor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("EsCxPhistorica"), null))) {
                    vRecord.EsCxPhistorica = vItem.Element("EsCxPhistorica").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumDiasDeVencimiento"), null))) {
                    vRecord.NumDiasDeVencimiento = LibConvert.ToInt(vItem.Element("NumDiasDeVencimiento"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocOrigen"), null))) {
                    vRecord.NumeroDocOrigen = vItem.Element("NumeroDocOrigen").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null))) {
                    vRecord.CodigoLote = vItem.Element("CodigoLote").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("GenerarAsientoDeRetiroEnCuenta"), null))) {
                    vRecord.GenerarAsientoDeRetiroEnCuenta = vItem.Element("GenerarAsientoDeRetiroEnCuenta").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalOtrosImpuestos"), null))) {
                    vRecord.TotalOtrosImpuestos = LibConvert.ToDec(vItem.Element("TotalOtrosImpuestos"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCxP"), null))) {
                    vRecord.ConsecutivoCxP = LibConvert.ToInt(vItem.Element("ConsecutivoCxP"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("EstaAsociadoARendicion"), null))) {
                    vRecord.EstaAsociadoARendicion = vItem.Element("EstaAsociadoARendicion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaAplicacionImpuestoMunicipal"), null))) {
                    vRecord.FechaAplicacionImpuestoMunicipal = LibConvert.ToDate(vItem.Element("FechaAplicacionImpuestoMunicipal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroComprobanteImpuestoMunicipal"), null))) {
                    vRecord.NumeroComprobanteImpuestoMunicipal = vItem.Element("NumeroComprobanteImpuestoMunicipal").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoRetenidoImpuestoMunicipal"), null))) {
                    vRecord.MontoRetenidoImpuestoMunicipal = LibConvert.ToDec(vItem.Element("MontoRetenidoImpuestoMunicipal"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ImpuestoMunicipalRetenido"), null))) {
                    vRecord.ImpuestoMunicipalRetenido = vItem.Element("ImpuestoMunicipalRetenido").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroControlDeFacturaAfectada"), null))) {
                    vRecord.NumeroControlDeFacturaAfectada = vItem.Element("NumeroControlDeFacturaAfectada").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRendicion"), null))) {
                    vRecord.ConsecutivoRendicion = LibConvert.ToInt(vItem.Element("ConsecutivoRendicion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDeclaracionAduana"), null))) {
                    vRecord.NumeroDeclaracionAduana = vItem.Element("NumeroDeclaracionAduana").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaDeclaracionAduana"), null))) {
                    vRecord.FechaDeclaracionAduana = LibConvert.ToDate(vItem.Element("FechaDeclaracionAduana"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("UsaPrefijoSerie"), null))) {
                    vRecord.UsaPrefijoSerie = vItem.Element("UsaPrefijoSerie").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoProveedorOriginalServicio"), null))) {
                    vRecord.CodigoProveedorOriginalServicio = vItem.Element("CodigoProveedorOriginalServicio").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("EsUnaCuentaATerceros"), null))) {
                    vRecord.EsUnaCuentaATerceros = vItem.Element("EsUnaCuentaATerceros").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("SeHizoLaDetraccion"), null))) {
                    vRecord.SeHizoLaDetraccion = vItem.Element("SeHizoLaDetraccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AplicaIvaAlicuotaEspecial"), null))) {
                    vRecord.AplicaIvaAlicuotaEspecial = vItem.Element("AplicaIvaAlicuotaEspecial").Value;
                }
				if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("BaseImponibleIGTFML"), null))) {
                    vRecord.BaseImponibleIGTFML = LibConvert.ToDec(vItem.Element("BaseImponibleIGTFML"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AlicuotaIGTFML"), null))) {
                    vRecord.AlicuotaIGTFML = LibConvert.ToDec(vItem.Element("AlicuotaIGTFML"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoIGTFML"), null))) {
                    vRecord.MontoIGTFML = LibConvert.ToDec(vItem.Element("MontoIGTFML"));
                }
				if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("fldTimeStampBigint"), null))) {
                    vRecord.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint"));
                }

                vResult.Add(vRecord);
            }
            return vResult;
        }
        public bool insert(List<CxP> list) {
            ILibDataMasterComponent<IList<CxP>, IList<CxP>> instanciaDal = new Galac.Adm.Dal.CajaChica.clsCxPDat();
            return instanciaDal.Insert(list, false).Success;
        }
        public IList<CxP> buscarCxp(StringBuilder parametros) {
            Galac.Adm.Dal.CajaChica.clsCxPDat insCXPDat = new Galac.Adm.Dal.CajaChica.clsCxPDat();
            XmlDocument XmlProperties = new XmlDocument();
            ((ILibDataFKSearch)insCXPDat).ConnectFk(ref XmlProperties, eProcessMessageType.SpName, "dbo.Gp_CXPSCH", parametros);
            if (XmlProperties.OuterXml == string.Empty)
                return new List<CxP>();
            return ParseToListEntity(XElement.Parse(XmlProperties.OuterXml));
        }

        public bool eliminarCXP(List<CxP> list) {
            ILibDataMasterComponentWithSearch<IList<CxP>, IList<CxP>> insCXPDat = new Galac.Adm.Dal.CajaChica.clsCxPDat();
            return insCXPDat.Delete(list).Success;
        }

        public bool actualizar(List<CxP> list) {
            throw new NotImplementedException();
        }
        #endregion

        internal void ActualizarCxPAsociadas(List<CxP> ListaCxp, DateTime valFechaCierre) {
            foreach (var item in ListaCxp) {
                StringBuilder sql = new StringBuilder();
                LibGpParams vParams = new LibGpParams();
                decimal vMontoAbonado = item.MontoExento + item.MontoGravado + item.MontoIva;
                vParams.AddInEnum("Status", (int)eStatusDocumento.Cancelado);
                vParams.AddInDecimal("MontoAbonado", vMontoAbonado, 2);
                vParams.AddInInteger("ConsecutivoCompania", item.ConsecutivoCompania);
                vParams.AddInDateTime("FechaCancelacion", valFechaCierre);
                vParams.AddInString("Numero", item.Numero, 25);
                vParams.AddInString("CodigoProveedor", item.CodigoProveedor, 10);
                sql.Append(" UPDATE CxP ");
                sql.Append(" SET Status = @Status, ");
                sql.Append(" MontoAbonado = @MontoAbonado, ");
                sql.Append(" FechaCancelacion = @FechaCancelacion ");
                sql.Append(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
                sql.Append(" AND Numero = @Numero");
                sql.Append(" AND CodigoProveedor = @CodigoProveedor");
                LibBusiness.ExecuteUpdateOrDelete(sql.ToString(), vParams.Get(), "", 0);
            }
        }

        internal void DesasociarCxP(List<CxP> Lista) {
            foreach (var item in Lista) {
                StringBuilder sql = new StringBuilder();
                LibGpParams vParams = new LibGpParams();

                vParams.AddInEnum("Status", (int)eStatusDocumento.PorCancelar);
                vParams.AddInDecimal("MontoAbonado", 0, 2);
                vParams.AddInBoolean("EstaAsociadoARendicion", false);
                vParams.AddInInteger("ConsecutivoRendicionNew", 0);
                vParams.AddInInteger("ConsecutivoCxP", item.ConsecutivoCxP);
                vParams.AddInInteger("ConsecutivoCompania", item.ConsecutivoCompania);
                sql.Append(" UPDATE CxP ");
                sql.Append(" SET Status = @Status, ");
                sql.Append("    MontoAbonado = @MontoAbonado, ");
                sql.Append("    EstaAsociadoARendicion = @EstaAsociadoARendicion, ");
                sql.Append("    ConsecutivoRendicion = @ConsecutivoRendicionNew");
                sql.Append(" WHERE ConsecutivoCompania = @ConsecutivoCompania");
                sql.Append(" AND ConsecutivoCxP = @ConsecutivoCxP");
                LibBusiness.ExecuteUpdateOrDelete(sql.ToString(), vParams.Get(), "", 0);
            }
        }

    } //End of class clsCxPNav

} //End of namespace Galac.Dbo.Brl.CajaChica

