using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.GestionCompras;
using System.Threading;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Dal;
using Galac.Comun.Ccl.TablasLey;
using Galac.Comun.Brl.TablasLey;

namespace Galac.Adm.Brl.GestionCompras {
    public partial class clsCxPNav :LibBaseNavMaster<IList<CxP>,IList<CxP>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCxPNav() {
        }
        #endregion //Constructores
        #region Metodos Generados


        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule,eAccionSR valAction,string valExtendedAction,XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.dbo.Dal.ComponenteNoEspecificado.clsCxPDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            switch (valCallingModule) {
                case "Compra":
                    return GetDataForListInCompra(valXmlParamsExpression,ref refXmlDocument);

                default: throw new NotImplementedException();
            }
        }



        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule,StringBuilder valParameters) {
            return null;
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            bool vResult = false;

            return vResult;
        }


        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICxPPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CxP>, IList<CxP>> instanciaDal = new clsCxPDat();
            IList<CxP> vLista = new List<CxP>();
            CxP vCurrentRecord = new Galac..Dal.ComponenteNoEspecificadoCxP();
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
            vCurrentRecord.ConsecutivoRendicion = 0;
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
            vCurrentRecord.SeHizoLaDetraccionAsBool = false;
            vCurrentRecord.AplicaIvaAlicuotaEspecialAsBool = false;
            vCurrentRecord.MontoGravableAlicuotaEspecial1 = 0;
            vCurrentRecord.MontoIVAAlicuotaEspecial1 = 0;
            vCurrentRecord.PorcentajeIvaAlicuotaEspecial1 = 0;
            vCurrentRecord.MontoGravableAlicuotaEspecial2 = 0;
            vCurrentRecord.MontoIVAAlicuotaEspecial2 = 0;
            vCurrentRecord.PorcentajeIvaAlicuotaEspecial2 = 0;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

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
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("IvaPorImportacionAG"), null))) {
                    vRecord.IvaPorImportacionAG = LibConvert.ToDec(vItem.Element("IvaPorImportacionAG"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("IvaPorImportacionA2"), null))) {
                    vRecord.IvaPorImportacionA2 = LibConvert.ToDec(vItem.Element("IvaPorImportacionA2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("IvaPorImportacionA3"), null))) {
                    vRecord.IvaPorImportacionA3 = LibConvert.ToDec(vItem.Element("IvaPorImportacionA3"));
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
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoRendicion"), null))) {
                    vRecord.ConsecutivoRendicion = LibConvert.ToInt(vItem.Element("ConsecutivoRendicion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoTipoDeDocumentoLey"), null))) {
                    vRecord.CodigoTipoDeDocumentoLey = vItem.Element("CodigoTipoDeDocumentoLey").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AplicaDetraccion"), null))) {
                    vRecord.AplicaDetraccion = vItem.Element("AplicaDetraccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDetraccion"), null))) {
                    vRecord.NumeroDetraccion = vItem.Element("NumeroDetraccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoDetraccion"), null))) {
                    vRecord.CodigoDetraccion = vItem.Element("CodigoDetraccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("DescripcionDeDetraccion"), null))) {
                    vRecord.DescripcionDeDetraccion = vItem.Element("DescripcionDeDetraccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeDetraccion"), null))) {
                    vRecord.PorcentajeDetraccion = LibConvert.ToDec(vItem.Element("PorcentajeDetraccion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TotalDetraccion"), null))) {
                    vRecord.TotalDetraccion = LibConvert.ToDec(vItem.Element("TotalDetraccion"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusDetraccion"), null))) {
                    vRecord.StatusDetraccion = vItem.Element("StatusDetraccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoMovimiento"), null))) {
                    vRecord.ConsecutivoMovimiento = LibConvert.ToInt(vItem.Element("ConsecutivoMovimiento"));
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
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("SeHizoLaDetraccion"), null))) {
                    vRecord.SeHizoLaDetraccion = vItem.Element("SeHizoLaDetraccion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("AplicaIvaAlicuotaEspecial"), null))) {
                    vRecord.AplicaIvaAlicuotaEspecial = vItem.Element("AplicaIvaAlicuotaEspecial").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoGravableAlicuotaEspecial1"), null))) {
                    vRecord.MontoGravableAlicuotaEspecial1 = LibConvert.ToDec(vItem.Element("MontoGravableAlicuotaEspecial1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoIVAAlicuotaEspecial1"), null))) {
                    vRecord.MontoIVAAlicuotaEspecial1 = LibConvert.ToDec(vItem.Element("MontoIVAAlicuotaEspecial1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeIvaAlicuotaEspecial1"), null))) {
                    vRecord.PorcentajeIvaAlicuotaEspecial1 = LibConvert.ToDec(vItem.Element("PorcentajeIvaAlicuotaEspecial1"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoGravableAlicuotaEspecial2"), null))) {
                    vRecord.MontoGravableAlicuotaEspecial2 = LibConvert.ToDec(vItem.Element("MontoGravableAlicuotaEspecial2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("MontoIVAAlicuotaEspecial2"), null))) {
                    vRecord.MontoIVAAlicuotaEspecial2 = LibConvert.ToDec(vItem.Element("MontoIVAAlicuotaEspecial2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("PorcentajeIvaAlicuotaEspecial2"), null))) {
                    vRecord.PorcentajeIvaAlicuotaEspecial2 = LibConvert.ToDec(vItem.Element("PorcentajeIvaAlicuotaEspecial2"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo

        private bool GetDataForListInCompra(StringBuilder valXmlParamsExpression,ref XmlDocument refXmlDocument) {
            bool vResult = false;
            XElement vParams = LibXml.ToXElement(valXmlParamsExpression.ToString());
            StringBuilder vSQL = new StringBuilder();
            string vSqlUseTop = ExtraerValorDeParametro(vParams,"@UseTopClausule");
            if (!LibString.IsNullOrEmpty(vSqlUseTop) && LibString.S1IsEqualToS2(vSqlUseTop,"S")) {
                vSQL.AppendLine("SELECT TOP 500 ");
            } else {
                vSQL.AppendLine("SELECT ");
            }
            vSQL.AppendLine(" dbo.Cxp.ConsecutivoCompania , dbo.Cxp.ConsecutivoCxP, cxP.Numero, Status, dbo.Cxp.CodigoProveedor , NombreProveedor, cxP.Fecha, (MontoExento + MontoGravado + MontoIva) AS Monto, CxP.CodigoMoneda");
            vSQL.AppendLine(" FROM cxP ");
            vSQL.AppendLine("   INNER JOIN  Adm.Proveedor ON cxP.ConsecutivoCompania = Adm.Proveedor.ConsecutivoCompania AND cxP.CodigoProveedor = Adm.Proveedor.CodigoProveedor");
            string vSqlWhere = ExtraerValorDeParametro(vParams,"@SQLWhere");
            if (!LibString.IsNullOrEmpty(vSqlWhere)) {
                vSQL.AppendLine("WHERE ");
                vSQL.AppendLine(vSqlWhere);
            }
            string vSqlOrderBy = ExtraerValorDeParametro(vParams,"@SQLOrderBy");
            if (!LibString.IsNullOrEmpty(vSqlOrderBy)) {
                vSQL.AppendLine("ORDER By " + vSqlOrderBy);
            }
            XElement vData = LibBusiness.ExecuteSelect(vSQL.ToString(),null,"",0);


            if (vData != null && vData.HasElements) {
                vResult = true;
                refXmlDocument = LibXml.ToXmlDocument(vData);
            }
            return vResult;
        }

        private string ExtraerValorDeParametro(XElement vParams,string valNameAtributte) {
            var vParamStr = vParams.Descendants("param");
            foreach (var vItem in vParamStr) {
                bool vContinue = false;
                foreach (var vAtributte in vItem.Attributes()) {
                    if (vAtributte.Name.LocalName == "nombre" && vAtributte.Value == valNameAtributte) {
                        vContinue = true;
                    }
                    if (vContinue && vAtributte.Name.LocalName == "valor") {
                        return vAtributte.Value;
                    }

                }
            }
            return "";
        }

        internal bool GenerarDesdeCompra(int valConsecutivoCompania,string valNumero,string valNumeroControl,DateTime valFecha,string valMoneda,decimal valCambioAbolivares,string valCodigoMoneda,string valNumeroSerie,eTipoCompra valTipoDeCompra,decimal valMontoExento,decimal valMontoGravableIVAGeneral,decimal valGravableMontoIVA2,decimal valMontoGravableIVA3,Proveedor valProveedor,decimal valMontoServicios, string valSimboloMoneda) {
            LibDatabase insDb = new LibDatabase();
            decimal vAlicuotaIVAdiv100 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros","AlicuotaIVAdiv100");
            decimal vAlicuotaIVA2div100 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros","AlicuotaIVA2div100");
            decimal vAlicuotaIVA3div100 = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetDecimal("Parametros","AlicuotaIVA3div100");
            decimal vMontoGravado = valMontoGravableIVAGeneral + valGravableMontoIVA2 + valMontoGravableIVA3;
            decimal vMontoIVAGeneral = LibMath.RoundToNDecimals(valMontoGravableIVAGeneral * vAlicuotaIVAdiv100,2);
            decimal vMontoIVA2 = LibMath.RoundToNDecimals(valGravableMontoIVA2 * vAlicuotaIVA2div100,2);
            decimal vMontoIVA3 = LibMath.RoundToNDecimals(valMontoGravableIVA3 * vAlicuotaIVA3div100,2);
            decimal vMontoIVA = vMontoIVAGeneral + vMontoIVA2 + vMontoIVA3;
            bool vSeHizoLaRetencion = false;
            bool vSeHizoLaRetencionIva = false; 
            string vNumeroComprobanteRetencion = "0"; 
            decimal vPorcentajeRetencionAplicado = 0; 
            decimal vMontoRetenidoIVACxP = 0; 
            string vOrigenDeLaRetencion = "0"; 
            int vDondeContabilRetIva = 0; 
            int vOrigenDeLaRetencionISLR = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","OrigenDeLaRetencionISLR"); // DONDE RETIENE ISLR
            int vDondeContabilISLR = 0; 
            int vOrigenInformacionRetencion = 0; 
            decimal vMontoRetencionISLR = 0;
            decimal vMontoAbonado = 0;
            decimal vPorcentajeRetencionISLR = 0;
            decimal vMontoRetenidoIVAPago = 0;
            if (valCambioAbolivares == 0) {
                valCambioAbolivares = (decimal)0.9999;
            }
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","PuedoUsarOpcionesDeContribuyenteEspecial") 
                && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","EnDondeRetenerIVA") == 1) {
                vSeHizoLaRetencionIva = true;
                vOrigenDeLaRetencion = "1";
                vPorcentajeRetencionAplicado = valProveedor.PorcentajeRetencionIVA;
                vMontoRetenidoIVACxP = vMontoIVA * (vPorcentajeRetencionAplicado / 100) ;
                vMontoRetenidoIVACxP = LibMath.RoundToNDecimals(vMontoRetenidoIVACxP * valCambioAbolivares,2);
                if (vMontoRetenidoIVACxP == 0) {
                    if( !LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","GenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAcero")){
                        vNumeroComprobanteRetencion = GenerateNextNumeroCompRetIVA(valConsecutivoCompania,LibDate.Today());
                    }
                } else {
                    vNumeroComprobanteRetencion = GenerateNextNumeroCompRetIVA(valConsecutivoCompania,LibDate.Today());
                }
                vOrigenInformacionRetencion = 2;
                vDondeContabilRetIva = 1;
                vMontoRetenidoIVAPago = vMontoRetenidoIVACxP;
            }
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","PuedoUsarOpcionesDeContribuyenteEspecial")
                && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","EnDondeRetenerIVA") == 2) {
                vOrigenDeLaRetencion = "2";
                vPorcentajeRetencionAplicado = valProveedor.PorcentajeRetencionIVA;
                vMontoRetenidoIVACxP = vMontoIVA * (vPorcentajeRetencionAplicado / 100);
                vOrigenInformacionRetencion = 2;
                vDondeContabilRetIva = 2;
            }
            int vNumeroConsecutivoComprobanteRetISLR = 0;
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","OrigenDeLaRetencionISLR") == 1) {
                vNumeroConsecutivoComprobanteRetISLR = GenerateNextNumeroCompRetISLR(valConsecutivoCompania) + 1;
                vSeHizoLaRetencion = true;
                vOrigenInformacionRetencion = 1;
                vDondeContabilISLR = 1;
                vMontoRetencionISLR = CalculaMontoRetencionISLR(valProveedor,valMontoServicios,ref vPorcentajeRetencionISLR, valCambioAbolivares);
                vMontoAbonado += LibMath.RoundToNDecimals(vMontoRetenidoIVAPago,2);
                vMontoAbonado += LibMath.RoundToNDecimals(vMontoRetencionISLR/ valCambioAbolivares,2);
                vMontoRetencionISLR = LibMath.RoundToNDecimals(vMontoRetencionISLR,2);
            }
            if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","OrigenDeLaRetencionISLR") == 2) {
                vDondeContabilISLR = 2;
            }
            LibGpParams vParamsDePago = new LibGpParams();
            vParamsDePago.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            string vNumeroCxP = valNumero;
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                vNumeroCxP = valNumeroSerie + "-" + valNumero;
            }
            string vTipoDeCompra = "1";
            if (valTipoDeCompra == eTipoCompra.Nacional) {
                vTipoDeCompra = "2";
            };
            LibGpParams vParamsNextConsecutive = new LibGpParams();
            vParamsNextConsecutive.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            int vConsecutivoCxP = insDb.NextLngConsecutive("dbo.Cxp","ConsecutivoCXP",vParamsNextConsecutive.Get());
            LibGpParams vParamsCxP = ParamsInsertarCxP(valConsecutivoCompania,valNumero,valNumeroControl,valFecha,valMoneda,valCambioAbolivares,valCodigoMoneda,valNumeroSerie,valMontoExento,valMontoGravableIVAGeneral,valGravableMontoIVA2,valProveedor,vMontoGravado,vMontoIVAGeneral,vMontoIVA2,vMontoIVA3,vMontoIVA,vConsecutivoCxP,vSeHizoLaRetencion,vSeHizoLaRetencionIva,vNumeroComprobanteRetencion,vPorcentajeRetencionAplicado,vMontoRetenidoIVACxP,vOrigenDeLaRetencion,vDondeContabilRetIva,vOrigenDeLaRetencionISLR,vDondeContabilISLR,vOrigenInformacionRetencion,vMontoRetencionISLR,vMontoAbonado,vNumeroCxP,vTipoDeCompra);
            try {
                int vResult = LibBusiness.ExecuteUpdateOrDelete(SQLInsertarCxP(),vParamsCxP.Get(),"",0);
                if (LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","OrigenDeLaRetencionISLR") == 1) {
                    if (vMontoAbonado > 0) {
                        InsertarPagoDeImpuestos(valConsecutivoCompania,vNumeroConsecutivoComprobanteRetISLR,valProveedor.CodigoProveedor,vMontoRetencionISLR,0,vNumeroCxP,valCodigoMoneda,valMoneda,valCambioAbolivares,vConsecutivoCxP,vNumeroCxP,vMontoGravado + vMontoIVA + valMontoExento,valProveedor.NombreProveedor,vMontoRetenidoIVAPago,valSimboloMoneda);
                    }
                    LibGpParams vParamRetPago = GetPAramsParaRetPago(valConsecutivoCompania,vNumeroConsecutivoComprobanteRetISLR,valNumero,LibDate.Today(),LibDate.Today().Month,LibDate.Today().Year,valProveedor.CodigoProveedor,vNumeroConsecutivoComprobanteRetISLR);
                    LibBusiness.ExecuteUpdateOrDelete(SQLInsertRetPago().ToString(),vParamRetPago.Get(),"",-1);
                    string vCodigoRetencionUsual = "NORET";
                    if (vMontoRetencionISLR > 0) {
                        vCodigoRetencionUsual = valProveedor.CodigoRetencionUsual;
                    }
                    LibGpParams vParamRetDocumentoPagado = GetPAramsParaRetDocumentoPagado(valConsecutivoCompania,vNumeroConsecutivoComprobanteRetISLR,vNumeroCxP,valNumeroControl,vCodigoRetencionUsual,LibMath.RoundToNDecimals( (vMontoGravado+valMontoExento) * valCambioAbolivares, 2),LibMath.RoundToNDecimals(valMontoServicios * valCambioAbolivares,2),vMontoRetencionISLR,vPorcentajeRetencionISLR);
                    LibBusiness.ExecuteUpdateOrDelete(SQLInsertRetDocumentoPagado().ToString(),vParamRetDocumentoPagado.Get(),"",-1);
                }
                return vResult > 0;
            } catch (LibGalac.Aos.Catching.GalacAlertException vEx) {
                if (LibString.IndexOf (vEx.Message,"Ya existe")>0) {
                    throw new LibGalac.Aos.Catching.GalacException("No se pueden generar la CxP porque ya existe el Número de Compra para este Proveedor",LibGalac.Aos.Catching.eExceptionManagementType.Controlled,vEx);
                }
                throw vEx;    
            } catch (Exception ) {
                throw ;
            }
        }

        private LibGpParams ParamsInsertarCxP(int valConsecutivoCompania,string valNumero,string valNumeroControl,DateTime valFecha,string valMoneda,decimal valCambioAbolivares,string valCodigoMoneda,string valNumeroSerie,decimal valMontoExento,decimal valMontoGravableIVAGeneral,decimal valGravableMontoIVA2,Proveedor valProveedor,decimal valMontoGravado,decimal valMontoIVAGeneral,decimal valMontoIVA2,decimal valMontoIVA3,decimal vMontoIVA,int vConsecutivo,bool vSeHizoLaRetencion,bool valSeHizoLaRetencionIva,string valNumeroComprobanteRetencion,decimal valPorcentajeRetencionAplicado,decimal vMontoRetenido,string vOrigenDeLaRetencion,int vDondeContabilRetIva,int vOrigenDeLaRetencionISLR,int vDondeContabilISLR,int valOrigenInformacionRetencion,decimal valMontoRetencionISLR, decimal valMontoAbonado,string valNumeroCxP,string valTipoDeCompra) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo",vConsecutivo);
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInString("Numero",valNumeroCxP,25);
            vParams.AddInString("CodigoProveedor",valProveedor.CodigoProveedor,10);
            vParams.AddInDateTime("Fecha",valFecha);
            vParams.AddInString("Moneda",valMoneda,80);
            vParams.AddInDecimal("CambioAbolivares",valCambioAbolivares,4);
            vParams.AddInDecimal("MontoGravado",valMontoGravado,4);
            vParams.AddInDecimal("MontoIva",vMontoIVA,4);
            vParams.AddInInteger("DiaDeAplicacion",LibDate.Today().Day);
            vParams.AddInInteger("MesDeAplicacion",LibDate.Today().Month);
            vParams.AddInInteger("AnoDeAplicacion",LibDate.Today().Year);
            vParams.AddInDecimal("MontoGravableAlicuotaGeneral",valMontoGravableIVAGeneral,4);
            vParams.AddInDecimal("MontoIvaalicuotaGeneral",valMontoIVAGeneral,4);
            vParams.AddInString("NumeroControl",valNumeroControl,20);
            vParams.AddInString("CodigoMoneda",valCodigoMoneda,4);
            vParams.AddInString("NombreOperador",((CustomIdentity)Thread.CurrentPrincipal.Identity).Login,10);
            vParams.AddInString("NumeroSerie",valNumeroSerie,10);
            vParams.AddInString("NumeroDeDocumento",valNumero,20);
            vParams.AddInString("TipoDeCompra",valTipoDeCompra,1);
            vParams.AddInDecimal("MontoExento",valMontoExento,4);
            vParams.AddInBoolean("SeHizoLaRetencion",vSeHizoLaRetencion);
            vParams.AddInDecimal("MontoGravableAlicuota2",valGravableMontoIVA2,4);
            vParams.AddInDecimal("MontoGravableAlicuota3",valGravableMontoIVA2,4);
            vParams.AddInDecimal("MontoIvaAlicuota2",valMontoIVA2,4);
            vParams.AddInDecimal("MontoIvaAlicuota3",valMontoIVA3,4);
            vParams.AddInBoolean("SeHizoLaRetencionIva",valSeHizoLaRetencionIva);
            vParams.AddInString("NumeroComprobanteRetencion",valNumeroComprobanteRetencion,8);
            vParams.AddInDateTime("FechaAplicacionRetIva",LibDate.Today());
            vParams.AddInDecimal("PorcentajeRetencionAplicado",valPorcentajeRetencionAplicado,4);
            vParams.AddInDecimal("MontoRetenido",vMontoRetenido,4);
            vParams.AddInString("OrigenDeLaRetencion",vOrigenDeLaRetencion,1);
            vParams.AddInEnum("DondeContabilRetIva",vDondeContabilRetIva);
            vParams.AddInEnum("OrigenDeLaRetencionISLR",vOrigenDeLaRetencionISLR);
            vParams.AddInEnum("DondeContabilISLR",vDondeContabilISLR);
            vParams.AddInDecimal("MontoRetenidoISLR",valMontoRetencionISLR,2);
            vParams.AddInDateTime("FechaAplicacionImpuestoMunicipal",LibDate.Today());
            vParams.AddInDateTime("FechaUltimaModificacion",LibDate.Today());
            vParams.AddInEnum("OrigenInformacionRetencion",valOrigenInformacionRetencion);
            vParams.AddInDecimal("MontoAbonado",valMontoAbonado,2);
            return vParams;
        }

        private string SQLInsertarCxP() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine(" INSERT INTO dbo.cxP");
            vSQL.AppendLine(" (ConsecutivoCompania, ConsecutivoCxp, Numero, TipoDeCxp, Status, CodigoProveedor, Fecha, FechaCancelacion, FechaVencimiento, FechaAnulacion");
            vSQL.AppendLine(" , Moneda, CambioAbolivares, AplicaParaLibrodeCompras, MontoExento, MontoGravado, MontoIva, MontoAbonado, MesDeAplicacion, AnoDeAplicacion");
            vSQL.AppendLine(" , Observaciones, CreditoFiscal, TipoDeCompra, SeHizoLaRetencion, MontoGravableAlicuotaGeneral, MontoGravableAlicuota2, MontoGravableAlicuota3");
            vSQL.AppendLine(" , MontoIvaAlicuotaGeneral, MontoIvaAlicuota2, MontoIvaAlicuota3, NumeroPlanillaDeImportacion, NumeroExpedienteDeImportacion, TipoDeTransaccion");
            vSQL.AppendLine(" , NumeroDeFacturaAfectada, NumeroControl, SeHizoLaRetencionIva, NumeroComprobanteRetencion, FechaAplicacionRetIva, PorcentajeRetencionAplicado");
            vSQL.AppendLine(" , MontoRetenido, OrigenDeLaRetencion, RetencionAplicadaEnPago, OrigenInformacionRetencion, CxPgeneradaPor, EsCxPhistorica, NumDiasDeVencimiento");
            vSQL.AppendLine(" , NumeroDocOrigen, CodigoLote, GenerarAsientoDeRetiroEnCuenta, TotalOtrosImpuestos, SeContabilRetIva, DondeContabilRetIva, CodigoMoneda");
            vSQL.AppendLine(" , OrigenDeLaRetencionISLR, DondeContabilISLR, ISLRAplicadaEnPago, MontoRetenidoISLR, SeContabilISLR, FechaAplicacionImpuestoMunicipal");
            vSQL.AppendLine(" , NumeroComprobanteImpuestoMunicipal, MontoRetenidoImpuestoMunicipal, ImpuestoMunicipalRetenido, NumeroControlDeFacturaAfectada, NombreOperador");
            vSQL.AppendLine(" , FechaUltimaModificacion, NumeroDeclaracionAduana, FechaDeclaracionAduana, UsaPrefijoSerie, EstaAsociadoARendicion, ConsecutivoRendicion");
            vSQL.AppendLine(" , EsUnaCuentaATerceros, CodigoProveedorOriginalServicio, SeHizoLaDetraccion, AplicaIvaAlicuotaEspecial, MontoGravableAlicuotaEspecial1");
            vSQL.AppendLine(" , MontoIVAAlicuotaEspecial1, PorcentajeIvaAlicuotaEspecial1, MontoGravableAlicuotaEspecial2, MontoIVAAlicuotaEspecial2, PorcentajeIvaAlicuotaEspecial2");
            vSQL.AppendLine(" , NumeroSerie, NumeroDeDocumento, NumeroSerieDocAfectado, NumeroDeDocumentoAfectado ");
            if(LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                vSQL.AppendLine(" ,DiaDeAplicacion ");
            }
            vSQL.AppendLine(" , CodigoTipoDeComprobante)");
            vSQL.AppendLine(" VALUES (@ConsecutivoCompania, @Consecutivo , @Numero, '0', '0', @CodigoProveedor, @Fecha, @Fecha, @Fecha, @Fecha");
            vSQL.AppendLine(" , @Moneda, @CambioAbolivares, 'S', @MontoExento, @MontoGravado, @MontoIva, @MontoAbonado,@MesDeAplicacion, @AnoDeAplicacion");
            vSQL.AppendLine(" , 'Generado desde Compra', 0, @TipoDeCompra, @SeHizoLaRetencion, @MontoGravableAlicuotaGeneral, @MontoGravableAlicuota2, @MontoGravableAlicuota3");
            vSQL.AppendLine(" , @MontoIvaalicuotaGeneral, @MontoIvaAlicuota2, @MontoIvaAlicuota3, '', '', '0'");
            vSQL.AppendLine(" , '', @NumeroControl, @SeHizoLaRetencionIva, @NumeroComprobanteRetencion, @FechaAplicacionRetIva, @PorcentajeRetencionAplicado");
            vSQL.AppendLine(" , @MontoRetenido, @OrigenDeLaRetencion, 'N', @OrigenInformacionRetencion, '7', 'N', 0");
            vSQL.AppendLine(" , '', '', 'N', 0, 'N', @DondeContabilRetIva, @CodigoMoneda");
            vSQL.AppendLine(" , @OrigenDeLaRetencionISLR, @DondeContabilISLR, 'N', @MontoRetenidoISLR, 'N', @FechaAplicacionImpuestoMunicipal");
            vSQL.AppendLine(" , '', 0, 'N', '', @NombreOperador");
            vSQL.AppendLine(" , @FechaUltimaModificacion, '', @Fecha, 'N', 'N', 0");
            vSQL.AppendLine(" , 'N', '', 'N', 'N', 0");
            vSQL.AppendLine(" , 0, 0, 0, 0, 0");
            vSQL.AppendLine(" , @NumeroSerie, @NumeroDeDocumento, '', '' ");
            if (LibDefGen.ProgramInfo.IsCountryVenezuela()) {
                vSQL.AppendLine(" ,@DiaDeAplicacion ");
            }
            vSQL.AppendLine(" , '01')");
            return vSQL.ToString();
        }
        internal void EliminarCxPDesdeCompra(int valConsecutivoCompania, string valNumero, string valCodigoProveedor) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Numero", valNumero, 25);
            vParams.AddInString("CodigoProveedor", valCodigoProveedor, 10);
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT Status, ConsecutivoCxP FROM dbo.CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero AND CodigoProveedor = @CodigoProveedor");
            XElement vXmlData = LibBusiness.ExecuteSelect(vSQL.ToString(), vParams.Get(), "", 0);
            int vConsecutivoCxP = 0;
            if (vXmlData != null) {
                if (LibXml.GetPropertyString(vXmlData, "Status") == "1") {
                    throw new LibGalac.Aos.Catching.GalacAlertException("No se puede ELIMINAR una CxP que este Cancelada");
                }
                vConsecutivoCxP = LibConvert.ToInt(LibXml.GetPropertyString(vXmlData,"ConsecutivoCxP"));
            }
            EliminaPagosYOtrosDocumentosAsociadosACxP(valConsecutivoCompania,vConsecutivoCxP,valCodigoProveedor);
            vSQL = new StringBuilder();
            vSQL.AppendLine("DELETE FROM dbo.CxP WHERE ConsecutivoCompania = @ConsecutivoCompania AND Numero = @Numero AND CodigoProveedor = @CodigoProveedor");
            LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(), vParams.Get(), "", 0);
        }

        private void EliminaPagosYOtrosDocumentosAsociadosACxP(int valConsecutivoCompania, int vConsecutivoCxP, string valCodigoProveedor) {
            XElement vXMLPagoDeCxp = LibBusiness.ExecuteSelect(SQLBuscarPagoAsociadoACxP(),GetParamForBuscarPagoAsociadoACxP(valConsecutivoCompania,vConsecutivoCxP,valCodigoProveedor).Get(),"",0);
            int vNumeroComprobantePago = -1;
            if (vXMLPagoDeCxp != null) {
                vNumeroComprobantePago = LibConvert.ToInt(LibXml.GetPropertyString(vXMLPagoDeCxp,"NumeroComprobante"));
            }
            if (vNumeroComprobantePago > 0) {
                StringBuilder vSQL = new StringBuilder();
                vSQL.AppendLine("DELETE FROM dbo.RetPago WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroComprobante = @NumeroComprobante");
                LibGpParams vParamsElminarPago = new LibGpParams();
                vParamsElminarPago.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
                vParamsElminarPago.AddInInteger("NumeroComprobante",vNumeroComprobantePago);
                LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(),vParamsElminarPago.Get(),"",0);
                vSQL.Clear();
                vSQL.AppendLine("DELETE FROM dbo.Pago WHERE ConsecutivoCompania = @ConsecutivoCompania AND NumeroComprobante = @NumeroComprobante");
                LibBusiness.ExecuteUpdateOrDelete(vSQL.ToString(),vParamsElminarPago.Get(),"",0);
            }
        }

        private string SQLBuscarPagoAsociadoACxP() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT Pago.NumeroComprobante");
            vSQL.AppendLine("	FROM Pago");
            vSQL.AppendLine("		INNER JOIN DocumentoPagado");
            vSQL.AppendLine("			ON Pago.ConsecutivoCompania = DocumentoPagado.ConsecutivoCompania");
            vSQL.AppendLine("			AND Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante");
            vSQL.AppendLine("		INNER JOIN cxP");
            vSQL.AppendLine("			ON DocumentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania");
            vSQL.AppendLine("		AND DocumentoPagado.ConsecutivoCxP = cxP.ConsecutivoCxp");
            vSQL.AppendLine("	WHERE Pago.ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine("		AND cxP.ConsecutivoCxP = @ConsecutivoCxP");
            vSQL.AppendLine("		AND Pago.CodigoProveedor = @CodigoProveedor");
            return vSQL.ToString();
        }

        private LibGpParams GetParamForBuscarPagoAsociadoACxP(int valConsecutivoCompania,int vConsecutivoCxP,string valCodigoProveedor) {
            LibGpParams vResult = new LibGpParams();
            vResult.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vResult.AddInInteger("ConsecutivoCxP",vConsecutivoCxP);
            vResult.AddInString("CodigoProveedor",valCodigoProveedor,10);
            return vResult;
        }
		
        private decimal CalculaMontoRetencionISLR(Proveedor valProveedor,decimal valMontoBaseImponibleYExento, ref decimal refPorcentajeDeRetencion, decimal valCambioABolivares) {
            decimal vResult = 0;
            if (valProveedor.CodigoRetencionUsual != "NORET") {
                ITablaRetencionPdn tablaRetencion = new clsTablaRetencionNav();
                TablaRetencion retencion = tablaRetencion.ObtenerDatosDeTablaRetencionPorFecha(valProveedor.TipoDePersonaAsDB,valProveedor.CodigoRetencionUsual,LibDate.Today());
                decimal vMontoBaseImponibleYExentoEnMonedaLocal = valMontoBaseImponibleYExento * valCambioABolivares;
                if (vMontoBaseImponibleYExentoEnMonedaLocal >= retencion.ParaPagosMayoresDe) {
                    vResult = (vMontoBaseImponibleYExentoEnMonedaLocal * (retencion.BaseImponible  /100) * (retencion.Tarifa /100)) - retencion.Sustraendo;
                    if (vResult < 0) {
                        vResult = 0;
                    }
                    refPorcentajeDeRetencion = retencion.Tarifa;
                }
            }
            return vResult;
        }

        private string GenerateNextNumeroCompRetIVA(int valConsecutivoCompania, DateTime valFechaRetencion) {
            StringBuilder vSQL = new StringBuilder();
            string vUltimoNumero;
            int vPrimerNumeroComprobanteRetIVA = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","PrimerNumeroComprobanteRetIva");
            int vFormaDeReiniciar = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros","FormaDeReiniciarElNumeroDeComprobanteRetIVA");
            vSQL.AppendLine("SELECT MAX(NumeroComprobanteRetencion) AS Maximo FROM ");
            vSQL.AppendLine(" cxP WHERE ");
            vSQL.AppendLine("ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine(" AND EsCxpHistorica <> 'S'");
            switch (vFormaDeReiniciar) {
                case 3:
                    break;
                case 2:
                    vSQL.AppendLine(" AND YEAR(FechaAplicacionRetIva)  = @AnoDeRetencion");
                    break;
                case 1:
                    vSQL.AppendLine(" AND MONTH(FechaAplicacionRetIva) = @MesDeRetencion");
                    vSQL.AppendLine(" AND YEAR(FechaAplicacionRetIva) = @AnoDeRetencion");
                    break;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("AnoDeRetencion",valFechaRetencion.Year);
            vParams.AddInInteger("MesDeRetencion",valFechaRetencion.Month);
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            XElement vXmlData = LibBusiness.ExecuteSelect(vSQL.ToString(),vParams.Get(),"",0);
            if (vXmlData != null) {
                vUltimoNumero = LibXml.GetPropertyString(vXmlData,"Maximo");
                vUltimoNumero = LibString.CleanFromCharsToLeft(vUltimoNumero,"0");
                vUltimoNumero = (LibConvert.ToInt(vUltimoNumero) + 1).ToString();
            } else {
                vUltimoNumero = "1";
            }
			if (vFormaDeReiniciar == 3 ) {
				if (vPrimerNumeroComprobanteRetIVA  > LibConvert.ToInt(vUltimoNumero) ) {
					vUltimoNumero = LibConvert.ToStr (vPrimerNumeroComprobanteRetIVA);
				} else if (LibConvert.ToInt(vUltimoNumero)  >= 99999999)  {
					vUltimoNumero = "1";
				}
			}
			vUltimoNumero = LibText.FillWithCharToLeft(vUltimoNumero , "0",8);
			return vUltimoNumero;
		}

        private bool InsertarPagoDeImpuestos(int valConsecutivoCompania,int valNumeroConsecutivoComprobanteRetISLR, string valCodigoProveedor,decimal valMontoRetencion, decimal valMontoOtrosImpuestos, string valNumeroDeCheque, string valCodigoMoneda, string valNombreMoneda, decimal valCambioABolivares, int valConsecutivoCxP, string valNumeroCxP, decimal valTotalCxP, string valBeneficiario, decimal valTotalRetenidoIVA, string valSimboloMoneda) {
            string vDescripcionPago = "Pago de RETENCIÓN";
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                vDescripcionPago = "Pago de DETRACCIÓN";
                valTotalRetenidoIVA = 0;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInInteger("NumeroComprobante", valNumeroConsecutivoComprobanteRetISLR);
            vParams.AddInEnum("StatusOrdenDePago",0);
            vParams.AddInDateTime ("Fecha",LibDate.Today());
            vParams.AddInDateTime("FechaAnulacion",LibDate.Today());
            vParams.AddInString("CodigoProveedor", valCodigoProveedor, 10);
            vParams.AddInDecimal ("TotalDocumentos",LibMath.RoundToNDecimals((valMontoRetencion + valTotalRetenidoIVA)/valCambioABolivares, 2),2);
            vParams.AddInDecimal("TotalOtros",LibMath.RoundToNDecimals(valMontoOtrosImpuestos,2),2);
            vParams.AddInDecimal("MontoCheque",0,2);
            vParams.AddInEnum("FormaDePago",0);
            vParams.AddInString("CodigoCuentaBancaria",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","CodigoGenericoCuentaBancaria"),5);
            vParams.AddInString("CodigoConcepto",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros","CodigoConceptoPagoRetenciones"),8);
            vParams.AddInString("AplicaDebitoBancario","N",1);
            vParams.AddInString("DescripcionPago", vDescripcionPago, 7000);
            vParams.AddInString("Beneficiario", valBeneficiario, 60);
            vParams.AddInString("Moneda",valNombreMoneda,80);
            vParams.AddInDecimal("CambioaBolivares",valCambioABolivares,2);
            vParams.AddInDecimal("TotalRetenido",LibMath.RoundToNDecimals(valMontoRetencion/valCambioABolivares,2),2);
            vParams.AddInDecimal("TotalRetenidoIva",LibMath.RoundToNDecimals(valTotalRetenidoIVA/valCambioABolivares,2),2);
            vParams.AddInString("SimboloMoneda",valSimboloMoneda, 4);
            vParams.AddInString("EsPagoHistorico","N",1);
            vParams.AddInDecimal("PagadoAnticipo",0,2);
            vParams.AddInDecimal("TotalAnticiposSinUsar", 0, 2);            
            vParams.AddInString("CodigoMoneda",valCodigoMoneda,4);
            vParams.AddInString("NombreOperador",((CustomIdentity)Thread.CurrentPrincipal.Identity).Login,10);
            vParams.AddInDateTime("FechaUltimaModificacion",LibDate.Today());
            vParams.AddInInteger("ConsecutivoCxP",valConsecutivoCxP);
            vParams.AddInString("NumeroDeCxP",valNumeroCxP, 25);
            vParams.AddInEnum ("Origen",0);
            vParams.AddInInteger("NumeroLote",0);
            vParams.AddInDecimal("TotalCxP",valTotalCxP, 2);
            vParams.AddInString("NumeroCheque", valNumeroDeCheque,25);
            vParams.AddInDecimal("TotalRetenidoImpuestoMunicipal",0,2);
            LibDatabase insDb = new LibDatabase();
            bool vResult = insDb.ExecSpNonQueryNonTransaction(insDb.ToSpName("dbo","CxPInsertarPagodesdeCxP"),vParams.Get(),0);
            insDb.Dispose();
            return  vResult;
        }

        private int GenerateNextNumeroCompRetISLR(int valConsecutivoCompania) {
            StringBuilder vSQL = new StringBuilder();
            int vUltimoNumero;
            int vUltimoNumeroPago = 1;
            int vUltimoNumeroRetPago =1;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vSQL.AppendLine("SELECT MAX(NumeroComprobante) AS Maximo FROM dbo.Pago ");
            vSQL.AppendLine("  WHERE ConsecutivoCompania = @ConsecutivoCompania ");
            XElement vXmlDataPago = LibBusiness.ExecuteSelect(vSQL.ToString(),vParams.Get(),"",0);
            if (vXmlDataPago != null) {
                vUltimoNumeroPago = LibConvert.ToInt(LibXml.GetPropertyString(vXmlDataPago,"Maximo"));
            }
            vSQL.Clear();
            vSQL.AppendLine("SELECT MAX(NumeroComprobante) AS Maximo FROM dbo.retPago ");
            vSQL.AppendLine("  WHERE ConsecutivoCompania = @ConsecutivoCompania");
            XElement vXmlDataRetPago = LibBusiness.ExecuteSelect(vSQL.ToString(),vParams.Get(),"",0);
            if (vXmlDataRetPago != null) {
                vUltimoNumeroRetPago = LibConvert.ToInt(LibXml.GetPropertyString(vXmlDataRetPago,"Maximo"));
            }
            if (vUltimoNumeroRetPago > vUltimoNumeroPago) {
                vUltimoNumero = vUltimoNumeroRetPago;
            } else  {
                vUltimoNumero = vUltimoNumeroPago;
            }
            return vUltimoNumero;
        }

        private LibGpParams GetPAramsParaRetPago(
            int valConsecutivoCompania
            , int valNumeroComprobante
            , string valNumeroReferencia
            , DateTime valFecha
            , int valMesAplicacion
            , int valAnoAplicacion
            , string valCodigoProveedor
            , int valNroComprobanteRetISLR) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("NumeroComprobante", valNumeroComprobante);
            vParams.AddInString("NumeroReferencia", valNumeroReferencia,25);
            vParams.AddInDateTime("Fecha", valFecha, true );
            vParams.AddInInteger("MesAplicacion", valMesAplicacion);
            vParams.AddInInteger("AnoAplicacion", valAnoAplicacion);
            vParams.AddInString("CodigoProveedor", valCodigoProveedor, 10);
            vParams.AddInString("CodigoLote", "", 10);
            vParams.AddInString("GeneradoPor", "", 1);
            vParams.AddInString("CampoExtra1", "", 20);
            vParams.AddInString("CampoExtra2", "", 20);
            vParams.AddInString("CampoExtra3", "", 20);
            vParams.AddInString("OrdenadoPorCodRet", "N", 1);
            vParams.AddInInteger("PrimerNumeroCompRetencion", 1);
            vParams.AddInInteger("NroComprobanteRetISLR", valNroComprobanteRetISLR);
            vParams.AddInEnum("Origen", 1);
            vParams.AddInString("NombreOperador",((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            return vParams;
        }

        private string SQLInsertRetPago() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("INSERT INTO dbo.RetPago ");
            vSQL.AppendLine("   (");
            vSQL.AppendLine("   ConsecutivoCompania");
            vSQL.AppendLine("   ,NumeroComprobante");
            vSQL.AppendLine("   ,NumeroReferencia");
            vSQL.AppendLine("   ,Fecha");
            vSQL.AppendLine("   ,MesAplicacion");
            vSQL.AppendLine("   ,AnoAplicacion");
            vSQL.AppendLine("   ,CodigoProveedor");
            vSQL.AppendLine("   ,CodigoLote");
            vSQL.AppendLine("   ,GeneradoPor");
            vSQL.AppendLine("   ,CampoExtra1");
            vSQL.AppendLine("   ,CampoExtra2");
            vSQL.AppendLine("   ,CampoExtra3");
            vSQL.AppendLine("   ,OrdenadoPorCodRet");
            vSQL.AppendLine("   ,PrimerNumeroCompRetencion");
            vSQL.AppendLine("   ,NroComprobanteRetISLR");
            vSQL.AppendLine("   ,Origen");
            vSQL.AppendLine("   ,NombreOperador");
            vSQL.AppendLine("   ,FechaUltimaModificacion");
            vSQL.AppendLine("   )");
            vSQL.AppendLine(" VALUES");
            vSQL.AppendLine("   (");
            vSQL.AppendLine("   @ConsecutivoCompania");
            vSQL.AppendLine("   ,@NumeroComprobante");
            vSQL.AppendLine("   ,@NumeroReferencia");
            vSQL.AppendLine("   ,@Fecha");
            vSQL.AppendLine("   ,@MesAplicacion");
            vSQL.AppendLine("   ,@AnoAplicacion");
            vSQL.AppendLine("   ,@CodigoProveedor");
            vSQL.AppendLine("   ,@CodigoLote");
            vSQL.AppendLine("   ,@GeneradoPor");
            vSQL.AppendLine("   ,@CampoExtra1");
            vSQL.AppendLine("   ,@CampoExtra2");
            vSQL.AppendLine("   ,@CampoExtra3");
            vSQL.AppendLine("   ,@OrdenadoPorCodRet");
            vSQL.AppendLine("   ,@PrimerNumeroCompRetencion");
            vSQL.AppendLine("   ,@NroComprobanteRetISLR");
            vSQL.AppendLine("   ,@Origen");
            vSQL.AppendLine("   ,@NombreOperador");
            vSQL.AppendLine("   ,@FechaUltimaModificacion");
            vSQL.AppendLine("   )");
            return vSQL.ToString();
        }

        private string SQLInsertRetDocumentoPagado() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("INSERT INTO dbo.RetDocumentoPagado");
            vSQL.AppendLine("           (ConsecutivoCompania");
            vSQL.AppendLine("           ,NumeroComprobante");
            vSQL.AppendLine("           ,SecuencialDocPagado");
            vSQL.AppendLine("           ,NumeroDelDocumento");
            vSQL.AppendLine("           ,NumeroControl");
            vSQL.AppendLine("           ,CodigoRetencion");
            vSQL.AppendLine("           ,MontoOriginal");
            vSQL.AppendLine("           ,MontoBaseImponible");
            vSQL.AppendLine("           ,MontoRetencion");
            vSQL.AppendLine("           ,TipoOperacion");
            vSQL.AppendLine("           ,PorcentajeDeRetencion");
            vSQL.AppendLine("           ,CodigoProveedorOriginalServicio");
            vSQL.AppendLine("           ,EsUnaCuentaATerceros)");
            vSQL.AppendLine("     VALUES");
            vSQL.AppendLine("           (@ConsecutivoCompania");
            vSQL.AppendLine("           ,@NumeroComprobante");
            vSQL.AppendLine("           ,@SecuencialDocPagado");
            vSQL.AppendLine("           ,@NumeroDelDocumento");
            vSQL.AppendLine("           ,@NumeroControl");
            vSQL.AppendLine("           ,@CodigoRetencion");
            vSQL.AppendLine("           ,@MontoOriginal");
            vSQL.AppendLine("           ,@MontoBaseImponible");
            vSQL.AppendLine("           ,@MontoRetencion");
            vSQL.AppendLine("           ,@TipoOperacion");
            vSQL.AppendLine("           ,@PorcentajeDeRetencion");
            vSQL.AppendLine("           ,@CodigoProveedorOriginalServicio");
            vSQL.AppendLine("           ,@EsUnaCuentaATerceros");
            vSQL.AppendLine("		   )");
            return vSQL.ToString();
        }
        private LibGpParams GetPAramsParaRetDocumentoPagado(
            int valConsecutivoCompania
            ,int valNumeroComprobante
            ,string valNumeroDelDocumentoCxP
            ,string valNumeroDeControlCxP
            ,string valCodigoRetencion
            ,decimal valMontoOriginal
            ,decimal valMontoBaseImponible
            ,decimal valMontoRetencionISLR
            ,decimal valPorcentajeDeRetencion
            ) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInInteger("NumeroComprobante",valNumeroComprobante);
            vParams.AddInInteger("SecuencialDocPagado",1);
            vParams.AddInString("NumeroDelDocumento",valNumeroDelDocumentoCxP,25);
            vParams.AddInString("NumeroControl",valNumeroDeControlCxP,20);
            vParams.AddInString("CodigoRetencion",valCodigoRetencion,6);
            vParams.AddInDecimal("MontoOriginal",valMontoOriginal,2);
            vParams.AddInDecimal("MontoBaseImponible",valMontoBaseImponible,2);
            vParams.AddInDecimal("MontoRetencion",valMontoRetencionISLR,2);
            vParams.AddInEnum("TipoOperacion",0);
            vParams.AddInDecimal("PorcentajeDeRetencion",valPorcentajeDeRetencion,2);
            vParams.AddInString("CodigoProveedorOriginalServicio","",10);
            vParams.AddInBoolean ("EsUnaCuentaATerceros",false);
            return vParams;
        }

        internal bool VerificaSiCxPEstaPorCancelar(int valConsecutivoCompania,string valNumeroCxP,string valCodigoProveedor) {
            int vNumeroDeCxp = 0;
            XElement vXmlDataCxP = LibBusiness.ExecuteSelect(SQLBuscaSiExisteCxP().ToString(),BuscarCxPParams(valConsecutivoCompania, valNumeroCxP, valCodigoProveedor).Get(),"",0);
            if (vXmlDataCxP != null) {
                vNumeroDeCxp = LibConvert.ToInt(LibXml.GetPropertyString(vXmlDataCxP,"NumeroDeCxP"));
            }
            return vNumeroDeCxp > 0;
        }

        private LibGpParams BuscarCxPParams(int valConsecutivoCompania,string valNumeroCxP,string valCodigoProveedor) {
            LibGpParams vResult = new LibGpParams();
            vResult.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vResult.AddInString("CodigoProveedor",valCodigoProveedor,10);
            vResult.AddInString("NumeroCxP",valNumeroCxP,25);
            return vResult;
        }

        private string SQLBuscaSiExisteCxP() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.AppendLine("SELECT COUNT(*) AS NumeroDeCxP");
            vSQL.AppendLine("	FROM cxP");
            vSQL.AppendLine("	INNER JOIN Adm.Compra");
            vSQL.AppendLine("		ON cxP.ConsecutivoCompania = Adm.Compra.ConsecutivoCompania");
            vSQL.AppendLine("		AND cxP.Numero = adm.Compra.Numero");
            vSQL.AppendLine("	WHERE");
            vSQL.AppendLine("		Status <> '0'");
            vSQL.AppendLine("            AND CodigoProveedor = @CodigoProveedor");
            vSQL.AppendLine("            AND cxP.Numero = @NumeroCxP");
            vSQL.AppendLine("            AND cxP.ConsecutivoCompania = @ConsecutivoCompania");
            vSQL.AppendLine("			AND Adm.Compra.GenerarCXP  = 'S'");

            return vSQL.ToString();
        }

        internal bool VerificaSiExisteCxP(int valConsecutivoCompania,string valNumeroCxP,string valCodigoProveedor) {
            int vNumeroDeCxp = 0;
            XElement vXmlDataCxP = LibBusiness.ExecuteSelect(SQLBuscaSiExisteCxPSinStatus().ToString(),BuscarCxPParams(valConsecutivoCompania,valNumeroCxP,valCodigoProveedor).Get(),"",0);
            if (vXmlDataCxP != null) {
                vNumeroDeCxp = LibConvert.ToInt(LibXml.GetPropertyString(vXmlDataCxP,"NumeroDeCxP"));
            }
            return vNumeroDeCxp > 0;
        }

        private string SQLBuscaSiExisteCxPSinStatus() {
            StringBuilder vSQL = new StringBuilder();
            vSQL.Append("SELECT COUNT(*) AS NumeroDeCxP FROM cxP ");
            vSQL.Append(" WHERE ");
            vSQL.Append(" CodigoProveedor = @CodigoProveedor");
            vSQL.Append(" AND Numero = @NumeroCxP");
            vSQL.Append(" AND ConsecutivoCompania = @ConsecutivoCompania");
            return vSQL.ToString();
        }
    }
    //End of class clsCxPNav
} //End of namespace Galac..Brl.ComponenteNoEspecificado

