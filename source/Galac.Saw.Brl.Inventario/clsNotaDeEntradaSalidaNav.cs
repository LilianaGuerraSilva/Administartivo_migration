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
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Brl.Inventario {
    public partial class clsNotaDeEntradaSalidaNav: LibBaseNavMaster<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>, INotaDeEntradaSalidaPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsNotaDeEntradaSalidaNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataMasterComponentWithSearch<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>> GetDataInstance() {
            return new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_NotaDeEntradaSalidaSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>> instanciaDal = new Galac.Saw.Dal.Inventario.clsNotaDeEntradaSalidaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "dbo.Gp_NotaDeEntradaSalidaGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch (valModule) {
                case "Nota de Entrada/Salida":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Cliente":
                    vPdnModule = new Galac.Saw.Brl.Cliente.clsClienteNav();
                    vResult = vPdnModule.GetDataForList("Nota de Entrada/Salida", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Almacén":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
                    vResult = vPdnModule.GetDataForList("Nota de Entrada/Salida", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Artículo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Nota de Entrada/Salida", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<NotaDeEntradaSalida> refData) {
            FillWithForeignInfoNotaDeEntradaSalida(ref refData);
            FillWithForeignInfoRenglonNotaES(ref refData);
        }
        #region NotaDeEntradaSalida

        private void FillWithForeignInfoNotaDeEntradaSalida(ref IList<NotaDeEntradaSalida> refData) {
            XElement vInfoConexionCliente = FindInfoCliente(refData);
            var vListCliente = (from vRecord in vInfoConexionCliente.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),                                          
                                          Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")), 
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Nombre = vRecord.Element("Nombre").Value, 
                                          NumeroRIF = vRecord.Element("NumeroRIF").Value,                                           
                                          Status = vRecord.Element("Status").Value,                                           
                                          TipoDeContribuyente = vRecord.Element("TipoDeContribuyente").Value, 
                                          CampoDefinible1 = vRecord.Element("CampoDefinible1").Value
                                      }).Distinct();
            XElement vInfoConexionAlmacen = FindInfoAlmacen(refData);
            var vListAlmacen = (from vRecord in vInfoConexionAlmacen.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),                                          
                                          Consecutivo = LibConvert.ToInt(vRecord.Element("Consecutivo")), 
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          NombreAlmacen = vRecord.Element("NombreAlmacen").Value, 
                                          TipoDeAlmacen = vRecord.Element("TipoDeAlmacen").Value, 
                                          ConsecutivoCliente = LibConvert.ToInt(vRecord.Element("ConsecutivoCliente")), 
                                          CodigoCliente = vRecord.Element("CodigoCliente").Value, 
                                          NombreCliente = vRecord.Element("NombreCliente").Value, 
                                          CodigoCc = vRecord.Element("CodigoCc").Value, 
                                          Descripcion = vRecord.Element("Descripcion").Value
                                      }).Distinct();

            foreach (NotaDeEntradaSalida vItem in refData) {
                vItem.NombreCliente = vInfoConexionCliente.Descendants("GpResult")
                    .Where(p => p.Element("codigo").Value == vItem.CodigoCliente)
                    .Select(p => p.Element("nombre").Value).FirstOrDefault();
                vItem.NombreAlmacen = vInfoConexionAlmacen.Descendants("GpResult")
                    .Where(p => p.Element("Codigo").Value == vItem.CodigoAlmacen)
                    .Select(p => p.Element("NombreAlmacen").Value).FirstOrDefault();
            }
        }

        private XElement FindInfoCliente(IList<NotaDeEntradaSalida> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(NotaDeEntradaSalida vItem in valData) {
                vXElement.Add(FilterNotaDeEntradaSalidaByDistinctCliente(vItem).Descendants("GpResult"));
            }
            ILibPdn insCliente = new Galac.Saw.Brl.Cliente.clsClienteNav();
            XElement vXElementResult = insCliente.GetFk("NotaDeEntradaSalida", ParametersGetFKClienteForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterNotaDeEntradaSalidaByDistinctCliente(NotaDeEntradaSalida valMaster) {
            XElement vXElement = new XElement("GpData",
                new XElement("GpResult",
                    new XElement("CodigoCliente", valMaster.CodigoCliente)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKClienteForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlDataDetail", valXElement);
            vResult = vParams.Get();
            return vResult;
        }

        private XElement FindInfoAlmacen(IList<NotaDeEntradaSalida> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(NotaDeEntradaSalida vItem in valData) {
                vXElement.Add(FilterNotaDeEntradaSalidaByDistinctAlmacen(vItem).Descendants("GpResult"));
            }
            ILibPdn insAlmacen = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
            XElement vXElementResult = insAlmacen.GetFk("NotaDeEntradaSalida", ParametersGetFKAlmacenForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterNotaDeEntradaSalidaByDistinctAlmacen(NotaDeEntradaSalida valMaster) {
            XElement vXElement = new XElement("GpData",                
                new XElement("GpResult",
                    new XElement("CodigoAlmacen", valMaster.CodigoAlmacen)));
            return vXElement;
        }

        private StringBuilder ParametersGetFKAlmacenForXmlSubSet(int valConsecutivoCompania, XElement valXElement) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInXml("XmlData", valXElement);
            vResult = vParams.Get();
            return vResult;
        }
        #endregion //NotaDeEntradaSalida
        #region RenglonNotaES

        private void FillWithForeignInfoRenglonNotaES(ref IList<NotaDeEntradaSalida> refData) {
            XElement vInfoConexionArticuloInventario = FindInfoArticuloInventario(refData);
            var vListArticuloInventario = (from vRecord in vInfoConexionArticuloInventario.Descendants("GpResult")
                                      select new {
                                          ConsecutivoCompania = LibConvert.ToInt(vRecord.Element("ConsecutivoCompania")),                                          
                                          Codigo = vRecord.Element("Codigo").Value, 
                                          Descripcion = vRecord.Element("Descripcion").Value, 
                                          LineaDeProducto = vRecord.Element("LineaDeProducto").Value, 
                                          StatusdelArticulo = vRecord.Element("StatusdelArticulo").Value, 
                                          TipoDeArticulo = vRecord.Element("TipoDeArticulo").Value, 
                                          AlicuotaIVA = vRecord.Element("AlicuotaIVA").Value,                                           
                                          CostoUnitario = LibConvert.ToDec(vRecord.Element("CostoUnitario")), 
                                          Existencia = LibConvert.ToDec(vRecord.Element("Existencia")),                                           
                                          TipoDeProducto = vRecord.Element("TipoDeProducto").Value                                          
                                      }).Distinct();
            foreach(NotaDeEntradaSalida vItem in refData) {
                vItem.DetailRenglonNotaES = 
                    new System.Collections.ObjectModel.ObservableCollection<RenglonNotaES>((
                        from vDetail in vItem.DetailRenglonNotaES
                        join vArticuloInventario in vListArticuloInventario
                        on new {Codigo = vDetail.CodigoArticulo, ConsecutivoCompania = vDetail.ConsecutivoCompania}
                        equals
                        new { Codigo = vArticuloInventario.Codigo, ConsecutivoCompania = vArticuloInventario.ConsecutivoCompania}
                        select new RenglonNotaES {
                            ConsecutivoCompania = vDetail.ConsecutivoCompania, 
                            NumeroDocumento = vDetail.NumeroDocumento, 
                            ConsecutivoRenglon = vDetail.ConsecutivoRenglon, 
                            CodigoArticulo = vDetail.CodigoArticulo, 
                            Cantidad = vDetail.Cantidad, 
                            TipoArticuloInvAsEnum = vDetail.TipoArticuloInvAsEnum, 
                            Serial = vDetail.Serial, 
                            Rollo = vDetail.Rollo, 
                            CostoUnitario = vDetail.CostoUnitario
                        }).ToList<RenglonNotaES>());
            }
        }

        private XElement FindInfoArticuloInventario(IList<NotaDeEntradaSalida> valData) {
            XElement vXElement = new XElement("GpData");
            foreach(NotaDeEntradaSalida vItem in valData) {
                vXElement.Add(FilterRenglonNotaESByDistinctArticuloInventario(vItem).Descendants("GpResult"));
            }
            ILibPdn insArticuloInventario = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            XElement vXElementResult = insArticuloInventario.GetFk("NotaDeEntradaSalida", ParametersGetFKArticuloInventarioForXmlSubSet(valData[0].ConsecutivoCompania, vXElement));
            return vXElementResult;
        }

        private XElement FilterRenglonNotaESByDistinctArticuloInventario(NotaDeEntradaSalida valMaster) {
            XElement vXElement = new XElement("GpData",
                from vEntity in valMaster.DetailRenglonNotaES.Distinct()
                select new XElement("GpResult",
                    new XElement("CodigoArticulo", vEntity.CodigoArticulo)));
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
        #endregion //RenglonNotaES

        XElement INotaDeEntradaSalidaPdn.FindByNumeroDocumento(int valConsecutivoCompania, string valNumeroDocumento) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("NumeroDocumento", valNumeroDocumento, 11);
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM dbo.NotaDeEntradaSalida");
            SQL.AppendLine("WHERE NumeroDocumento = @NumeroDocumento");
            SQL.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }

        LibResponse INotaDeEntradaSalidaPdn.AgregarNotaDeEntradaSalida(IList<NotaDeEntradaSalida> valListNotaDeEntradaSalida) {
            LibResponse vResult = new LibResponse();
            RegisterClient();
            vResult.Success = true;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valListNotaDeEntradaSalida[0].ConsecutivoCompania);
            XElement vData = _Db.QueryInfo(eProcessMessageType.Message, "ProximoNumeroDocumento", vParams.Get());
            string vSecuencial = LibXml.GetPropertyString(vData, "NumeroDocumento");

            foreach (NotaDeEntradaSalida item in valListNotaDeEntradaSalida) {
                item.NumeroDocumento = vSecuencial;
                vSecuencial = LibText.NextSequential(vSecuencial, 11);
                vResult.Success = InsertRecord(new List<NotaDeEntradaSalida>() { item } , true).Success && vResult.Success;
            }
            return vResult;
        }

        LibResponse INotaDeEntradaSalidaPdn.AnularNotaDeSalidaAsociadaProduccion(int valConsecutivoCompania, int valConsecutivoOrdenDeProduccion) {
            RegisterClient();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInInteger("ConsecutivoDocumentoOrigen", valConsecutivoOrdenDeProduccion);
            XElement vData = _Db.QueryInfo(eProcessMessageType.Query, "SELECT NumeroDocumento FROM dbo.NotaDeEntradaSalida WHERE ConsecutivoCompania = @ConsecutivoCompania AND ConsecutivoDocumentoOrigen = @ConsecutivoDocumentoOrigen", vParams.Get());
            string vNumeroDocumento = LibXml.GetPropertyString(vData, "NumeroDocumento");
            vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", vNumeroDocumento,11);
            IList<NotaDeEntradaSalida> vDataEntradaSalida = _Db.GetData(eProcessMessageType.SpName, "NotaDeEntradaSalidaGET", vParams.Get(),true);
            if (vDataEntradaSalida != null && vDataEntradaSalida.Count > 0) {
                vDataEntradaSalida[0].StatusNotaEntradaSalidaAsEnum = eStatusNotaEntradaSalida.Anulada;
                return UpdateRecord(vDataEntradaSalida, true, eAccionSR.Anular);
            }
            return new LibResponse();
        }

        XElement INotaDeEntradaSalidaPdn.FindByConsecutivoCompaniaNumeroDocumento(int valConsecutivoCompania, string valNumeroDocumento) {
            throw new NotImplementedException();
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool INotaDeEntradaSalidaPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>> instanciaDal = new clsNotaDeEntradaSalidaDat();
            IList<NotaDeEntradaSalida> vLista = new List<NotaDeEntradaSalida>();
            NotaDeEntradaSalida vCurrentRecord = new Galac.Saw.Dal.InventarioNotaDeEntradaSalida();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.NumeroDocumento = "";
            vCurrentRecord.TipodeOperacionAsEnum = eTipodeOperacion.EntradadeInventario;
            vCurrentRecord.CodigoCliente = "";
            vCurrentRecord.CodigoAlmacen = "";
            vCurrentRecord.Fecha = LibDate.Today();
            vCurrentRecord.Comentarios = "";
            vCurrentRecord.CodigoLote = "";
            vCurrentRecord.StatusNotaEntradaSalidaAsEnum = eStatusNotaEntradaSalida.Vigente;
            vCurrentRecord.ConsecutivoAlmacen = 0;
            vCurrentRecord.GeneradoPorAsEnum = eTipoGeneradoPorNotaDeEntradaSalida.Usuario;
            vCurrentRecord.ConsecutivoDocumentoOrigen = 0;
            vCurrentRecord.TipoNotaProduccionAsEnum = eTipoNotaProduccion.NoAplica;
            vCurrentRecord.NombreOperador = "";
            vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<NotaDeEntradaSalida> ParseToListEntity(XElement valXmlEntity) {
            List<NotaDeEntradaSalida> vResult = new List<NotaDeEntradaSalida>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                NotaDeEntradaSalida vRecord = new NotaDeEntradaSalida();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NumeroDocumento"), null))) {
                    vRecord.NumeroDocumento = vItem.Element("NumeroDocumento").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipodeOperacion"), null))) {
                    vRecord.TipodeOperacion = vItem.Element("TipodeOperacion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoCliente"), null))) {
                    vRecord.CodigoCliente = vItem.Element("CodigoCliente").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoAlmacen"), null))) {
                    vRecord.CodigoAlmacen = vItem.Element("CodigoAlmacen").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Fecha"), null))) {
                    vRecord.Fecha = LibConvert.ToDate(vItem.Element("Fecha"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Comentarios"), null))) {
                    vRecord.Comentarios = vItem.Element("Comentarios").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("CodigoLote"), null))) {
                    vRecord.CodigoLote = vItem.Element("CodigoLote").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("StatusNotaEntradaSalida"), null))) {
                    vRecord.StatusNotaEntradaSalida = vItem.Element("StatusNotaEntradaSalida").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoAlmacen"), null))) {
                    vRecord.ConsecutivoAlmacen = LibConvert.ToInt(vItem.Element("ConsecutivoAlmacen"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("GeneradoPor"), null))) {
                    vRecord.GeneradoPor = vItem.Element("GeneradoPor").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoDocumentoOrigen"), null))) {
                    vRecord.ConsecutivoDocumentoOrigen = LibConvert.ToInt(vItem.Element("ConsecutivoDocumentoOrigen"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("TipoNotaProduccion"), null))) {
                    vRecord.TipoNotaProduccion = vItem.Element("TipoNotaProduccion").Value;
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

        protected override LibResponse InsertRecord(IList<NotaDeEntradaSalida> refRecord, bool valUseDetail) {
            LibResponse vResult = new LibResponse();
            foreach (NotaDeEntradaSalida vItem in refRecord) {
                if (valUseDetail) {
                    if (vItem!= null) {
                        if (vItem.TipodeOperacionAsEnum != eTipodeOperacion.EntradadeInventario) {
                            string vCodigos;
                            if (!HayExistenciaParaNotaDeSalidaDeInventario(vItem, out vCodigos)) {
                                vResult = new LibResponse();
                                vResult.Success = false;
                                vResult.AddError("No hay existencia suficiente de algunos ítems (" + vCodigos + ") en la Nota: " + vItem.NumeroDocumento + " para realizar la acción. El proceso será cancelado.");
                                return vResult;
                            }
                            vResult = base.InsertRecord(refRecord, valUseDetail);
                        } else {
                            vResult = base.InsertRecord(refRecord, valUseDetail);
                        }
                        if (vResult.Success) {
                            InsertarLoteDeInventario(vItem);
                        }
                    }
                } else {
                    vResult = base.InsertRecord(refRecord, valUseDetail);
                }
            }
            return vResult;
        }

        protected override LibResponse UpdateRecord(IList<NotaDeEntradaSalida> refRecord, bool valUseDetail, eAccionSR valAction) {
            //en principio solo entra por acá si la acción es anular, es decir, acción especial = anular
            return base.UpdateRecord(refRecord, valUseDetail, valAction);
        }

        protected override LibResponse DeleteRecord(IList<NotaDeEntradaSalida> refRecord) {
            return base.DeleteRecord(refRecord);
        }

        protected override bool CanBeChoosenForAction(IList<NotaDeEntradaSalida> refRecord, eAccionSR valAction) {
            if ((valAction == eAccionSR.Eliminar) ||(valAction == eAccionSR.Anular)) {
                if (ExisteAlMenosUnArticuloDeLoteFdV(refRecord)) {
                    return false;
                } else {
                    return base.CanBeChoosenForAction(refRecord, valAction);
                }
            } else {
                return base.CanBeChoosenForAction(refRecord, valAction);
            }
        }

        private bool ExisteAlMenosUnArticuloDeLoteFdV(IList<NotaDeEntradaSalida> refRecord) {
            bool vResult = false;
            foreach (NotaDeEntradaSalida vItemNotaES in refRecord) {
                if (HayAlMenosUnArtLoteFdV(vItemNotaES)) {
                    return true;
                }
            }
            return vResult;
        }

        private bool HayAlMenosUnArtLoteFdV(NotaDeEntradaSalida valItemNotaES) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valItemNotaES.ConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valItemNotaES.NumeroDocumento, 11);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT COUNT(*) AS Cantidad FROM RenglonNotaES INNER JOIN ArticuloInventario ");
            vSql.AppendLine("ON RenglonNotaES.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania ");
            vSql.AppendLine("AND RenglonNotaES.CodigoArticulo = ArticuloInventario.Codigo ");
            vSql.AppendLine("WHERE RenglonNotaES.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine("AND RenglonNotaES.NumeroDocumento = @NumeroDocumento");
            vSql.AppendLine("AND ArticuloInventario.TipoDeArticulo = '0'");
            vSql.AppendLine("AND ArticuloInventario.TipoArticuloInv = '5'");            
            XElement vCantidad = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", -1);
            if (vCantidad != null) {
                vResult = (LibConvert.ToInt(LibXml.GetPropertyString(vCantidad, "Cantidad")) > 0);
            }
            return vResult;
        }

        private void InsertarLoteDeInventario(NotaDeEntradaSalida valItemNotaES) {
            foreach (RenglonNotaES vItemRenglon in valItemNotaES.DetailRenglonNotaES) {
                if (vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento) {
                    if (ExisteLoteDeInventario(vItemRenglon.ConsecutivoCompania, vItemRenglon.CodigoArticulo, vItemRenglon.LoteDeInventario)) {
                        ActualizaLoteDeInventarioInsertaMovimientoDeLoteDeInventario(valItemNotaES, vItemRenglon);
                    } else {
                        InsertaLoteDeInventario(valItemNotaES, vItemRenglon);
                    }
                }
            }
        }

        private void InsertaLoteDeInventario(NotaDeEntradaSalida valItemNotaES, RenglonNotaES valItemRenglonNotaES) {
            LoteDeInventarioMovimiento vLoteMov = new LoteDeInventarioMovimiento();
            vLoteMov.ConsecutivoCompania = valItemRenglonNotaES.ConsecutivoCompania;
            vLoteMov.Fecha = valItemNotaES.Fecha;
            vLoteMov.ModuloAsEnum = eOrigenLoteInv.NotaEntradaSalida;
            vLoteMov.Cantidad = valItemRenglonNotaES.Cantidad;
            vLoteMov.ConsecutivoDocumentoOrigen = 0;
            vLoteMov.NumeroDocumentoOrigen = valItemNotaES.NumeroDocumento;
            vLoteMov.StatusDocumentoOrigenAsEnum = eStatusDocOrigenLoteInv.Vigente;

            LoteDeInventario vLote = new LoteDeInventario();
            vLote.ConsecutivoCompania = valItemRenglonNotaES.ConsecutivoCompania;
            vLote.CodigoLote = valItemRenglonNotaES.LoteDeInventario;
            vLote.CodigoArticulo = valItemRenglonNotaES.CodigoArticulo;
            vLote.FechaDeElaboracion = valItemRenglonNotaES.FechaDeElaboracion;
            vLote.FechaDeVencimiento = valItemRenglonNotaES.FechaDeVencimiento;
            vLote.Existencia = valItemRenglonNotaES.Cantidad;
            vLote.StatusLoteInvAsEnum = eStatusLoteDeInventario.Vigente;
            vLote.DetailLoteDeInventarioMovimiento.Add(vLoteMov);

            ILoteDeInventarioPdn vLotePdn = new clsLoteDeInventarioNav();
            IList<LoteDeInventario> vListLote = new List<LoteDeInventario>();
            vListLote.Add(vLote);
            vLotePdn.AgregarLote(vListLote);
        }

        private void ActualizaLoteDeInventarioInsertaMovimientoDeLoteDeInventario(NotaDeEntradaSalida valItemNotaES, RenglonNotaES valItemRenglonNotaES) {
            XElement vLoteXElemnt = ((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).FindByConsecutivoCompaniaCodigoLoteCodigoArticulo(valItemRenglonNotaES.ConsecutivoCompania, valItemRenglonNotaES.LoteDeInventario, valItemRenglonNotaES.CodigoArticulo);
            if (vLoteXElemnt != null) {
                LoteDeInventario vLote = (new clsLoteDeInventarioNav().ParseToListEntity(vLoteXElemnt))[0];
                if (vLote != null) {
                    decimal vCant = (valItemNotaES.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario) ? valItemRenglonNotaES.Cantidad : valItemRenglonNotaES.Cantidad * -1;
                    LoteDeInventarioMovimiento vLoteMov = new LoteDeInventarioMovimiento();
                    vLoteMov.ConsecutivoCompania = valItemRenglonNotaES.ConsecutivoCompania;
                    vLoteMov.ConsecutivoLote = vLote.Consecutivo;
                    vLoteMov.Fecha = valItemNotaES.Fecha;
                    vLoteMov.ModuloAsEnum = eOrigenLoteInv.NotaEntradaSalida;
                    vLoteMov.Cantidad = vCant;
                    vLoteMov.ConsecutivoDocumentoOrigen = 0;
                    vLoteMov.NumeroDocumentoOrigen = valItemNotaES.NumeroDocumento;
                    vLoteMov.StatusDocumentoOrigenAsEnum = eStatusDocOrigenLoteInv.Vigente;

                    vLote.Existencia += vCant;
                    vLote.DetailLoteDeInventarioMovimiento.Add(vLoteMov);

                    ILoteDeInventarioPdn vLotePnd = new clsLoteDeInventarioNav();
                    IList<LoteDeInventario> vListLote = new List<LoteDeInventario>();
                    vListLote.Add(vLote);
                    vLotePnd.ActualizarLote(vListLote);
                }
            }
        }

        private bool ExisteLoteDeInventario(int valConsecutivoCompania, string valCodigoArticulo, string valLoteDeInventario) {
            return ((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).ExisteLoteDeInventario(valConsecutivoCompania, valCodigoArticulo, valLoteDeInventario);
        }

        private bool HayExistenciaParaNotaDeSalidaDeInventario(NotaDeEntradaSalida valItemNotaES, out string outCodigos) {
            outCodigos = string.Empty;
            if ((int)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida", "PermitirSobregiro") == (int)Galac.Saw.Ccl.SttDef.ePermitirSobregiro.NoPermitirSobregiro) {
                IArticuloInventarioPdn insArticuloInventarioNav = new clsArticuloInventarioNav();
                string vCodigos = string.Empty;
                foreach (RenglonNotaES vItemRenglon in valItemNotaES.DetailRenglonNotaES) {
                    decimal vDisponibilidad = insArticuloInventarioNav.DisponibilidadDeArticulo(valItemNotaES.ConsecutivoCompania, valItemNotaES.CodigoAlmacen, vItemRenglon.CodigoArticulo, (int)eTipoDeArticulo.Mercancia, vItemRenglon.Serial, vItemRenglon.Rollo);
                    bool vHayExistencia = (vItemRenglon.Cantidad < vDisponibilidad);
                    if (!vHayExistencia) {
                        if (LibString.Len(vCodigos) > 0) vCodigos += ", ";
                        vCodigos += vItemRenglon.CodigoArticulo;
                    }
                }
                outCodigos = vCodigos;
                return (LibString.Len(vCodigos) <= 0);
            } else {
                return true;
            }
            throw new NotImplementedException();
        }
    } //End of class clsNotaDeEntradaSalidaNav

} //End of namespace Galac.Saw.Brl.Inventario

