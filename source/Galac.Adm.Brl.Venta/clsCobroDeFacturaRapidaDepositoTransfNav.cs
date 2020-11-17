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

namespace Galac.Adm.Brl.Venta {
    public partial class clsCobroDeFacturaRapidaDepositoTransfNav: LibBaseNavMaster<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaDepositoTransfNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDepositoTransfDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDepositoTransfDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDepositoTransfDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaDepositoTransfSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>> instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaDepositoTransfDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaDepositoTransfGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Cobro Tarjeta de Deposito Transferencia":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Forma Del Cobro":
                //    vPdnModule = new Galac.Adm.Brl.Tablas.clsFormaDelCobroNav();
                //    vResult = vPdnModule.GetDataForList("Cobro Tarjeta de Deposito Transferencia", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                case "Banco":
                    vPdnModule = new Galac.Comun.Brl.TablasGen.clsBancoNav();
                    vResult = vPdnModule.GetDataForList("Cobro Tarjeta de Deposito Transferencia", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        //protected override void FillWithForeignInfo(ref IList<CobroDeFacturaRapidaDepositoTransf> refData) {
        //    FillWithForeignInfoCobroDeFacturaRapidaDepositoTransfDetalle(ref refData);
        //}
        #region CobroDeFacturaRapidaDepositoTransfDetalle

        //private void FillWithForeignInfoCobroDeFacturaRapidaDepositoTransfDetalle(ref IList<CobroDeFacturaRapidaDepositoTransf> refData) {
        //    XElement vInfoConexion = FindInfoFormaDelCobro(refData);
        //    var vListFormaDelCobro = (from vRecord in vInfoConexion.Descendants("GpResult")
        //                              select new {
        //                                  Codigo = vRecord.Element("Codigo").Value, 
        //                                  Nombre = vRecord.Element("Nombre").Value, 
        //                                  TipoDePago = vRecord.Element("TipoDePago").Value
        //                              }).Distinct();
        //    foreach(CobroDeFacturaRapidaDepositoTransf vItem in refData) {
        //        vItem.DetailCobroDeFacturaRapidaDepositoTransfDetalle = 
        //            new System.Collections.ObjectModel.ObservableCollection<CobroDeFacturaRapidaDepositoTransfDetalle>((
        //                from vDetail in vItem.DetailCobroDeFacturaRapidaDepositoTransfDetalle
        //                join vFormaDelCobro in vListFormaDelCobro
        //                on new {Codigo = vDetail.CodigoFormaDelCobro}
        //                equals
        //                new { Codigo = vFormaDelCobro.Codigo}
        //                select new CobroDeFacturaRapidaDepositoTransfDetalle {
            //XElement vInfoConexion = FindInfoBanco(refData);
            //var vListBanco = (from vRecord in vInfoConexion.Descendants("GpResult")
            //                          select new {
            //                              Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")), 
            //                              Codigo = vRecord.Element("Codigo").Value, 
            //                              Nombre = vRecord.Element("Nombre").Value, 
            //                              Status = vRecord.Element("Status").Value, 
            //                              fldOrigen = vRecord.Element("fldOrigen").Value
            //                          }).Distinct();
            //foreach(CobroDeFacturaRapidaDepositoTransf vItem in refData) {
            //    vItem.DetailCobroDeFacturaRapidaDepositoTransfDetalle = 
            //        new System.Collections.ObjectModel.ObservableCollection<CobroDeFacturaRapidaDepositoTransfDetalle>((
            //            from vDetail in vItem.DetailCobroDeFacturaRapidaDepositoTransfDetalle
            //            join vBanco in vListBanco
            //            on new {codigo = vDetail.CodigoBanco}
            //            equals
            //            new { codigo = vBanco.Codigo}
            //            select new CobroDeFacturaRapidaDepositoTransfDetalle {
            //                ConsecutivoCompania = vDetail.ConsecutivoCompania, 
            //                CodigoFormaDelCobro = vDetail.CodigoFormaDelCobro, 
            //                NumeroDelDocumento = vDetail.NumeroDelDocumento, 
            //                CodigoBanco = vDetail.CodigoBanco, 
            //                NombreBanco = vBanco.Nombre, 
            //                Monto = vDetail.Monto
            //            }).ToList<CobroDeFacturaRapidaDepositoTransfDetalle>());
            //}
        //}

        //private XElement FindInfoFormaDelCobro(IList<CobroDeFacturaRapidaDepositoTransf> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(CobroDeFacturaRapidaDepositoTransf vItem in valData) {
        //        vXElement.Add(FilterCobroDeFacturaRapidaDepositoTransfDetalleByDistinctFormaDelCobro(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insFormaDelCobro = new Galac.SAW.Brl.Tablas.clsFormaDelCobroNav();
        //    XElement vXElementResult = insFormaDelCobro.GetFk("CobroDeFacturaRapidaDepositoTransf", ParametersGetFKFormaDelCobroForXmlSubSet(vXElement));
        //    return vXElementResult;
        //}

        private XElement FilterCobroDeFacturaRapidaDepositoTransfDetalleByDistinctFormaDelCobro(CobroDeFacturaRapidaDepositoTransf valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Distinct()
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

        private XElement FindInfoBanco(IList<CobroDeFacturaRapidaDepositoTransf> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(CobroDeFacturaRapidaDepositoTransf vItem in valData) {
                vXElement.Add(FilterCobroDeFacturaRapidaDepositoTransfDetalleByDistinctBanco(vItem).Descendants("GpResult"));
            }
            ILibPdn insBanco = new Galac.Comun.Brl.TablasGen.clsBancoNav();
            XElement vXElementResult = insBanco.GetFk("CobroDeFacturaRapidaDepositoTransf", ParametersGetFKBancoForXmlSubSet(vXElement));
            return vXElementResult;
        }

        private XElement FilterCobroDeFacturaRapidaDepositoTransfDetalleByDistinctBanco(CobroDeFacturaRapidaDepositoTransf valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaDepositoTransfDetalle.Distinct()
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
        #endregion //CobroDeFacturaRapidaDepositoTransfDetalle
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICobroDeFacturaRapidaDepositoTransfPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CobroDeFacturaRapidaDepositoTransf>, IList<CobroDeFacturaRapidaDepositoTransf>> instanciaDal = new clsCobroDeFacturaRapidaDepositoTransfDat();
            IList<CobroDeFacturaRapidaDepositoTransf> vLista = new List<CobroDeFacturaRapidaDepositoTransf>();
            CobroDeFacturaRapidaDepositoTransf vCurrentRecord = new Galac.Adm.Dal.VentaCobroDeFacturaRapidaDepositoTransf();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroFactura = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CobroDeFacturaRapidaDepositoTransf> ParseToListEntity(XElement valXmlEntity) {
            List<CobroDeFacturaRapidaDepositoTransf> vResult = new List<CobroDeFacturaRapidaDepositoTransf>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CobroDeFacturaRapidaDepositoTransf vRecord = new CobroDeFacturaRapidaDepositoTransf();
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


    } //End of class clsCobroDeFacturaRapidaDepositoTransfNav

} //End of namespace Galac.Adm.Brl.Venta

