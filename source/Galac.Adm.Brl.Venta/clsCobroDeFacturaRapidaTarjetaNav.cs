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
using Galac.Comun.Brl.TablasGen;



namespace Galac.Adm.Brl.Venta {
    public partial class clsCobroDeFacturaRapidaTarjetaNav: LibBaseNavMaster<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaTarjetaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaTarjetaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaTarjetaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaTarjetaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaTarjetaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>> instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaTarjetaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaTarjetaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Cobro Tarjeta de Factura":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Forma Del Cobro":
                    vPdnModule = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
                    vResult = vPdnModule.GetDataForList("Cobro Tarjeta de Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Banco":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsBancoNav();
                    vResult = vPdnModule.GetDataForList("Cobro Tarjeta de Factura", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        //protected override void FillWithForeignInfo(ref IList<CobroDeFacturaRapidaTarjeta> refData) {
        //    FillWithForeignInfoCobroDeFacturaRapidaTarjetaDetalle(ref refData);
        //}
        #region CobroDeFacturaRapidaTarjetaDetalle

        //private void FillWithForeignInfoCobroDeFacturaRapidaTarjetaDetalle(ref IList<CobroDeFacturaRapidaTarjeta> refData) {
        //    XElement vInfoConexion = FindInfoFormaDelCobro(refData);
        //    var vListFormaDelCobro = (from vRecord in vInfoConexion.Descendants("GpResult")
        //                              select new {
        //                                  Codigo = vRecord.Element("Codigo").Value, 
        //                                  Nombre = vRecord.Element("Nombre").Value, 
        //                                  TipoDePago = vRecord.Element("TipoDePago").Value
        //                              }).Distinct();
        //    foreach(CobroDeFacturaRapidaTarjeta vItem in refData) {
        //        vItem.DetailCobroDeFacturaRapidaTarjetaDetalle = 
        //            new System.Collections.ObjectModel.ObservableCollection<CobroDeFacturaRapidaTarjetaDetalle>((
        //                from vDetail in vItem.DetailCobroDeFacturaRapidaTarjetaDetalle
        //                join vFormaDelCobro in vListFormaDelCobro
        //                on new {Codigo = vDetail.CodigoFormaDelCobro}
        //                equals
        //                new { Codigo = vFormaDelCobro.Codigo}
        //                select new CobroDeFacturaRapidaTarjetaDetalle {
        //    XElement vInfoConexion = FindInfoBanco(refData);
        //    var vListBanco = (from vRecord in vInfoConexion.Descendants("GpResult")
        //                              select new {
        //                                  Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")), 
        //                                  Codigo = vRecord.Element("Codigo").Value, 
        //                                  Nombre = vRecord.Element("Nombre").Value, 
        //                                  Status = vRecord.Element("Status").Value, 
        //                                  fldOrigen = vRecord.Element("fldOrigen").Value
        //                              }).Distinct();
        //    foreach(CobroDeFacturaRapidaTarjeta vItem in refData) {
        //        vItem.DetailCobroDeFacturaRapidaTarjetaDetalle = 
        //            new System.Collections.ObjectModel.ObservableCollection<CobroDeFacturaRapidaTarjetaDetalle>((
        //                from vDetail in vItem.DetailCobroDeFacturaRapidaTarjetaDetalle
        //                join vBanco in vListBanco
        //                on new {codigo = vDetail.CodigoBanco}
        //                equals
        //                new { codigo = vBanco.codigo}
        //                on new {codigo = vDetail.CodigoPuntoDeVenta}
        //                equals
        //                new { codigo = vBanco.codigo}
        //                select new CobroDeFacturaRapidaTarjetaDetalle {
        //                    ConsecutivoCompania = vDetail.ConsecutivoCompania, 
        //                    CodigoFormaDelCobro = vDetail.CodigoFormaDelCobro, 
        //                    NumeroDelDocumento = vDetail.NumeroDelDocumento, 
        //                    CodigoBanco = vDetail.CodigoBanco, 
        //                    NombreBanco = vBanco.Nombre, 
        //                    Monto = vDetail.Monto, 
        //                    CodigoPuntoDeVenta = vDetail.CodigoPuntoDeVenta, 
        //                    NombreBancoPuntoDeVenta = vBanco.Nombre, 
        //                    NumeroDocumentoAprobacion = vDetail.NumeroDocumentoAprobacion
        //                }).ToList<CobroDeFacturaRapidaTarjetaDetalle>());
        //    }
        //}

        //private XElement FindInfoFormaDelCobro(IList<CobroDeFacturaRapidaTarjeta> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(CobroDeFacturaRapidaTarjeta vItem in valData) {
        //        vXElement.Add(FilterCobroDeFacturaRapidaTarjetaDetalleByDistinctFormaDelCobro(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insFormaDelCobro = new Galac.SAW.Brl.Tablas.clsFormaDelCobroNav();
        //    XElement vXElementResult = insFormaDelCobro.GetFk("CobroDeFacturaRapidaTarjeta", ParametersGetFKFormaDelCobroForXmlSubSet(vXElement));
        //    return vXElementResult;
        //}

        private XElement FilterCobroDeFacturaRapidaTarjetaDetalleByDistinctFormaDelCobro(CobroDeFacturaRapidaTarjeta valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaTarjetaDetalle.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoFormaDelCobro", vEntity.CodigoFormaDelCobro)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKFormaDelCobroForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        //private XElement FindInfoBanco(IList<CobroDeFacturaRapidaTarjeta> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(CobroDeFacturaRapidaTarjeta vItem in valData) {
        //        vXElement.Add(FilterCobroDeFacturaRapidaTarjetaDetalleByDistinctBanco(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insBanco = new Galac.Comun.Brl.TablasGen.clsBancoNav();
        //    XElement vXElementResult = insBanco.GetFk("CobroDeFacturaRapidaTarjeta", ParametersGetFKBancoForXmlSubSet(vXElement));
        //    return vXElementResult;
        //}

        private XElement FilterCobroDeFacturaRapidaTarjetaDetalleByDistinctBanco(CobroDeFacturaRapidaTarjeta valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaTarjetaDetalle.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoBanco", vEntity.CodigoBanco)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKBancoForXmlSubSet(XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        //private XElement FindInfoBanco(IList<CobroDeFacturaRapidaTarjeta> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(CobroDeFacturaRapidaTarjeta vItem in valData) {
        //        vXElement.Add(FilterCobroDeFacturaRapidaTarjetaDetalleByDistinctBanco(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insBanco = new Galac.Comun.Brl.TablasGen.clsBancoNav();
        //    XElement vXElementResult = insBanco.GetFk("CobroDeFacturaRapidaTarjeta", ParametersGetFKBancoForXmlSubSet(vXElement));
        //    return vXElementResult;
        //}

        //private XElement FilterCobroDeFacturaRapidaTarjetaDetalleByDistinctBanco(CobroDeFacturaRapidaTarjeta valMaster) {
        //    XElement vXElement = new XElement("GpData",
        //        from vEntity in valMaster.DetailCobroDeFacturaRapidaTarjetaDetalle.Distinct()
        //        select new XElement("GpResult",
        //            new XElement("CodigoPuntoDeVenta", vEntity.CodigoPuntoDeVenta)));
        //    return vXElement;
        //}

        //private StringBuilder ParametersGetFKBancoForXmlSubSet(XElement valXElement) {
        //    StringBuilder vResult = new StringBuilder();
        //    LibGpParams vParams = new LibGpParams();
        //    vParams.AddReturn();
        //    vParams.AddInXml("XmlData", valXElement);
        //    vResult = vParams.Get();
        //    return vResult;
        //}
        #endregion //CobroDeFacturaRapidaTarjetaDetalle
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICobroDeFacturaRapidaTarjetaPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CobroDeFacturaRapidaTarjeta>, IList<CobroDeFacturaRapidaTarjeta>> instanciaDal = new clsCobroDeFacturaRapidaTarjetaDat();
            IList<CobroDeFacturaRapidaTarjeta> vLista = new List<CobroDeFacturaRapidaTarjeta>();
            CobroDeFacturaRapidaTarjeta vCurrentRecord = new Galac.Adm.Dal.VentaCobroDeFacturaRapidaTarjeta();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroFactura = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CobroDeFacturaRapidaTarjeta> ParseToListEntity(XElement valXmlEntity) {
            List<CobroDeFacturaRapidaTarjeta> vResult = new List<CobroDeFacturaRapidaTarjeta>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CobroDeFacturaRapidaTarjeta vRecord = new CobroDeFacturaRapidaTarjeta();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroFactura"), null))) {
                    vRecord.NumeroFactura = vItem.Element("NumeroFactura").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo


    } //End of class clsCobroDeFacturaRapidaTarjetaNav

} //End of namespace Galac.Adm.Brl.Venta

