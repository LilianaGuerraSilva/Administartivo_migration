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

namespace Galac.Adm.Brl.Venta {
    public partial class clsCobroDeFacturaRapidaAnticipoNav : LibBaseNavMaster<IList<CobroDeFacturaRapidaAnticipo>, IList<CobroDeFacturaRapidaAnticipo>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsCobroDeFacturaRapidaAnticipoNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<CobroDeFacturaRapidaAnticipo>, IList<CobroDeFacturaRapidaAnticipo>> GetDataInstance() {
            return new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaAnticipoDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaAnticipoDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaAnticipoDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaAnticipoSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<CobroDeFacturaRapidaAnticipo>, IList<CobroDeFacturaRapidaAnticipo>> instanciaDal = new Galac.Adm.Dal.Venta.clsCobroDeFacturaRapidaAnticipoDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Adm.Gp_CobroDeFacturaRapidaAnticipoGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Cobro de Factura con Anticipo":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                //case "Forma Del Cobro":
                //    vPdnModule = new Galac.Adm.Brl.Tablas.clsFormaDelCobroNav();
                //    vResult = vPdnModule.GetDataForList("Cobro de Factura con Anticipo", ref refXmlDocument, valXmlParamsExpression);
                //    break;
                case "Anticipo":
                    vPdnModule = new Galac.Adm.Brl.CAnticipo.clsAnticipoNav();
                    vResult = vPdnModule.GetDataForList("Cobro de Factura con Anticipo", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        //protected override void FillWithForeignInfo(ref IList<CobroDeFacturaRapidaAnticipo> refData) {
        //    FillWithForeignInfoCobroDeFacturaRapidaAnticipoDetalle(ref refData);
        //}
        #region CobroDeFacturaRapidaAnticipoDetalle

        //        private void FillWithForeignInfoCobroDeFacturaRapidaAnticipoDetalle(ref IList<CobroDeFacturaRapidaAnticipo> refData) {
        //            XElement vInfoConexion = FindInfoFormaDelCobro(refData);
        //            var vListFormaDelCobro = (from vRecord in vInfoConexion.Descendants("GpResult")
        //                                      select new {
        //                                          Codigo = vRecord.Element("Codigo").Value, 
        //                                          Nombre = vRecord.Element("Nombre").Value, 
        //                                          TipoDePago = vRecord.Element("TipoDePago").Value
        //                                      }).Distinct();
        //            foreach(CobroDeFacturaRapidaAnticipo vItem in refData) {
        //                vItem.DetailCobroDeFacturaRapidaAnticipoDetalle = 
        //                    new System.Collections.ObjectModel.ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle>((
        //                        from vDetail in vItem.DetailCobroDeFacturaRapidaAnticipoDetalle
        //                        join vFormaDelCobro in vListFormaDelCobro
        //                        on new {Codigo = vDetail.CodigoFormaDelCobro}
        //                        equals
        //                        new { Codigo = vFormaDelCobro.Codigo}
        //                        select new CobroDeFacturaRapidaAnticipoDetalle {
        //            XElement vInfoConexion = FindInfoAnticipo(refData);
        //            var vListAnticipo = (from vRecord in vInfoConexion.Descendants("GpResult")
        //                                      select new {
        //                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),
        //                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")), 
        //                                          ConsecutivoAnticipo = LibConvert.ToInt(vRecord.Element("ConsecutivoAnticipo")), 
        //                                          Status = vRecord.Element("Status").Value, 
        //                                          Tipo = vRecord.Element("Tipo").Value, 
        //                                          Fecha = vRecord.Element("Fecha").Value, 
        //                                          Numero = vRecord.Element("Numero").Value, 
        //                                          CodigoCliente = vRecord.Element("CodigoCliente").Value, 
        //                                          NombreCliente = vRecord.Element("NombreCliente").Value, 
        //                                          CodigoProveedor = vRecord.Element("CodigoProveedor").Value, 
        //                                          NombreProveedor = vRecord.Element("NombreProveedor").Value, 
        //                                          Moneda = vRecord.Element("Moneda").Value, 
        //, 
        //                                          GeneraMovBancario = vRecord.Element("GeneraMovBancario").Value, 
        //                                          CodigoCuentaBancaria = vRecord.Element("CodigoCuentaBancaria").Value, 
        //                                          NombreCuentaBancaria = vRecord.Element("NombreCuentaBancaria").Value, 
        //                                          CodigoConceptoBancario = vRecord.Element("CodigoConceptoBancario").Value, 
        //                                          NombreConceptoBancario = vRecord.Element("NombreConceptoBancario").Value, 
        //                                          GeneraImpuestoBancario = vRecord.Element("GeneraImpuestoBancario").Value, 
        //                                          FechaAnulacion = vRecord.Element("FechaAnulacion").Value, 
        //                                          FechaCancelacion = vRecord.Element("FechaCancelacion").Value, 
        //                                          FechaDevolucion = vRecord.Element("FechaDevolucion").Value, 
        //                                          Descripcion = vRecord.Element("Descripcion").Value, 
        //, 
        //, 
        //, 
        //, 
        //                                          DiferenciaEsIDB = vRecord.Element("DiferenciaEsIDB").Value, 
        //                                          EsUnaDevolucion = vRecord.Element("EsUnaDevolucion").Value, 
        //                                          NumeroDelAnticipoDevuelto = LibConvert.ToInt(vRecord.Element("NumeroDelAnticipoDevuelto")), 
        //                                          NumeroCheque = vRecord.Element("NumeroCheque").Value, 
        //                                          AsociarAnticipoACotiz = vRecord.Element("AsociarAnticipoACotiz").Value, 
        //                                          NumeroCotizacion = vRecord.Element("NumeroCotizacion").Value, 
        //                                          ConsecutivoRendicion = LibConvert.ToInt(vRecord.Element("ConsecutivoRendicion"))
        //                                      }).Distinct();
        //            foreach(CobroDeFacturaRapidaAnticipo vItem in refData) {
        //                vItem.DetailCobroDeFacturaRapidaAnticipoDetalle = 
        //                    new System.Collections.ObjectModel.ObservableCollection<CobroDeFacturaRapidaAnticipoDetalle>((
        //                        from vDetail in vItem.DetailCobroDeFacturaRapidaAnticipoDetalle
        //                        join vAnticipo in vListAnticipo
        //                        on new {codigo = vDetail.CodigoAnticipo, ConsecutivoCompania = vDetail.ConsecutivoCompania}
        //                        equals
        //                        new { codigo = vAnticipo.codigo, ConsecutivoCompania = vAnticipo.ConsecutivoCompania}
        //                        select new CobroDeFacturaRapidaAnticipoDetalle {
        //                            ConsecutivoCompania = vDetail.ConsecutivoCompania, 
        //                            CodigoFormaDelCobro = vDetail.CodigoFormaDelCobro, 
        //                            CodigoAnticipo = vDetail.CodigoAnticipo, 
        //                            NumeroAnticipo = vDetail.NumeroAnticipo, 
        //                            MontoDisponible = vDetail.MontoDisponible, 
        //                            Monto = vDetail.Monto
        //                        }).ToList<CobroDeFacturaRapidaAnticipoDetalle>());
        //            }
        //        }

        //private XElement FindInfoFormaDelCobro(IList<CobroDeFacturaRapidaAnticipo> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(CobroDeFacturaRapidaAnticipo vItem in valData) {
        //        vXElement.Add(FilterCobroDeFacturaRapidaAnticipoDetalleByDistinctFormaDelCobro(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insFormaDelCobro = new Galac.SAW.Brl.Tablas.clsFormaDelCobroNav();
        //    XElement vXElementResult = insFormaDelCobro.GetFk("CobroDeFacturaRapidaAnticipo", ParametersGetFKFormaDelCobroForXmlSubSet(vXElement));
        //    return vXElementResult;
        //}

        private XElement FilterCobroDeFacturaRapidaAnticipoDetalleByDistinctFormaDelCobro(CobroDeFacturaRapidaAnticipo valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaAnticipoDetalle.Distinct()
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

        //private XElement FindInfoAnticipo(IList<CobroDeFacturaRapidaAnticipo> valData) {
        //    XElement vXElement = new XElement("GpData");
        //    foreach(CobroDeFacturaRapidaAnticipo vItem in valData) {
        //        vXElement.Add(FilterCobroDeFacturaRapidaAnticipoDetalleByDistinctAnticipo(vItem).Descendants("GpResult"));
        //    }
        //    ILibPdn insAnticipo = new Galac.dbo.Brl.ComponenteNoEspecificado.clsAnticipoNav();
        //    XElement vXElementResult = insAnticipo.GetFk("CobroDeFacturaRapidaAnticipo", ParametersGetFKAnticipoForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
        //    return vXElementResult;
        //}

        private XElement FilterCobroDeFacturaRapidaAnticipoDetalleByDistinctAnticipo(CobroDeFacturaRapidaAnticipo valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailCobroDeFacturaRapidaAnticipoDetalle.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoAnticipo", vEntity.CodigoAnticipo)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKAnticipoForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //CobroDeFacturaRapidaAnticipoDetalle
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool ICobroDeFacturaRapidaAnticipoPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<CobroDeFacturaRapidaAnticipo>, IList<CobroDeFacturaRapidaAnticipo>> instanciaDal = new clsCobroDeFacturaRapidaAnticipoDat();
            IList<CobroDeFacturaRapidaAnticipo> vLista = new List<CobroDeFacturaRapidaAnticipo>();
            CobroDeFacturaRapidaAnticipo vCurrentRecord = new Galac.Adm.Dal.VentaCobroDeFacturaRapidaAnticipo();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroFactura = "";
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<CobroDeFacturaRapidaAnticipo> ParseToListEntity(XElement valXmlEntity) {
            List<CobroDeFacturaRapidaAnticipo> vResult = new List<CobroDeFacturaRapidaAnticipo>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                CobroDeFacturaRapidaAnticipo vRecord = new CobroDeFacturaRapidaAnticipo();
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


    } //End of class clsCobroDeFacturaRapidaAnticipoNav

} //End of namespace Galac.Adm.Brl.Venta

