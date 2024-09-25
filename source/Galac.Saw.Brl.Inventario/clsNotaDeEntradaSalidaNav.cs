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
    public partial class clsNotaDeEntradaSalidaNav : LibBaseNavMaster<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>>, INotaDeEntradaSalidaPdn {
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
                case "Lote de Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsLoteDeInventarioNav();
                    vResult = vPdnModule.GetDataForList("Nota de Entrada/Salida", ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<NotaDeEntradaSalida> refData) {
            FillWithForeignInfoNotaDeEntradaSalida(ref refData);
        }
        #region NotaDeEntradaSalida
        private void FillWithForeignInfoNotaDeEntradaSalida(ref IList<NotaDeEntradaSalida> refData) {
            if (refData != null) {
                XElement vInfoConexionCliente = FindInfoCliente(refData);
                XElement vInfoConexionAlmacen = FindInfoAlmacen(refData);
                foreach (NotaDeEntradaSalida vItem in refData) {
                    vItem.NombreCliente = vInfoConexionCliente.Descendants("GpResult").Where(p => p.Element("Codigo").Value == vItem.CodigoCliente).Select(p => p.Element("Nombre").Value).FirstOrDefault();
                    vItem.NombreAlmacen = vInfoConexionAlmacen.Descendants("GpResult").Where(p => p.Element("Codigo").Value == vItem.CodigoAlmacen).Select(p => p.Element("NombreAlmacen").Value).FirstOrDefault();
                }
            }
        }

        private XElement FindInfoCliente(IList<NotaDeEntradaSalida> valData) {
            XElement vXElementResult = new XElement("GpData");
            if (valData != null) {
                foreach (NotaDeEntradaSalida vItem in valData) {
                    LibGpParams vParms = new LibGpParams();
                    vParms.AddInInteger("ConsecutivoCompania", vItem.ConsecutivoCompania);
                    vParms.AddInString("Codigo", vItem.CodigoCliente, 10);
                    StringBuilder vSql = new StringBuilder();
                    vSql.AppendLine("SELECT * FROM Cliente WHERE ConsecutivoCompania= @ConsecutivoCompania AND Codigo= @Codigo");
                    XElement vXElementCliente = LibBusiness.ExecuteSelect(vSql.ToString(), vParms.Get(), "", 0);
                    vXElementResult.Add(vXElementCliente.Descendants("GpResult"));
                }
            }
            return vXElementResult;
        }

        private XElement FindInfoAlmacen(IList<NotaDeEntradaSalida> valData) {
            XElement vXElementResult = new XElement("GpData");
            if (valData != null) {
                foreach (NotaDeEntradaSalida vItem in valData) {
                    LibGpParams vParms = new LibGpParams();
                    vParms.AddInInteger("ConsecutivoCompania", vItem.ConsecutivoCompania);
                    vParms.AddInInteger("Consecutivo", vItem.ConsecutivoAlmacen);
                    StringBuilder vSql = new StringBuilder();
                    vSql.AppendLine("SELECT * FROM Almacen WHERE ConsecutivoCompania= @ConsecutivoCompania AND Consecutivo= @Consecutivo");
                    XElement vXElementAlmacen = LibBusiness.ExecuteSelect(vSql.ToString(), vParms.Get(), "", 0);
                    vXElementResult.Add(vXElementAlmacen.Descendants("GpResult"));
                }
            }
            return vXElementResult;
        }
        #endregion //NotaDeEntradaSalida
        #region RenglonNotaES
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
                vResult.Success = InsertRecord(new List<NotaDeEntradaSalida>() { item }, true).Success && vResult.Success;
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
            vParams.AddInString("NumeroDocumento", vNumeroDocumento, 11);
            IList<NotaDeEntradaSalida> vDataEntradaSalida = _Db.GetData(eProcessMessageType.SpName, "NotaDeEntradaSalidaGET", vParams.Get(), true);
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
            if (valUseDetail) {               
                foreach (NotaDeEntradaSalida vItem in refRecord) {
                    if (vItem != null) {                        
                        IList<NotaDeEntradaSalida> vItemList = new List<NotaDeEntradaSalida>();
                        vItemList.Add(vItem);
                        if (vItem.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario) {                            
                            vResult = base.InsertRecord(vItemList, valUseDetail);
                            if (vResult.Success) {
                                ActualizaExistenciaDeArticulos(vItem, eAccionSR.Insertar);
                            }
                        } else {
                            string vCodigos;
                            if (!HayExistenciaParaNotaDeSalidaDeInventario(vItem, out vCodigos)) {
                                vResult.AddError("No hay existencia suficiente de algunos ítems (" + vCodigos + ") en la Nota: " + vItem.NumeroDocumento + " para realizar la acción. El proceso será cancelado.");
                                return vResult;
                            }
                            vResult = base.InsertRecord(vItemList, valUseDetail);
                            if (vResult.Success) {
                                ActualizaExistenciaDeArticulos(vItem, eAccionSR.Insertar);
                            }
                        }
                        if (vResult.Success) {
                            ActualizaInformacionDeLoteDeInventario(vItem);
                        }
                    }
                }
            } else {
                vResult = base.InsertRecord(refRecord, valUseDetail);
            }
            return vResult;
        }

        private void ActualizaExistenciaDeArticulos(NotaDeEntradaSalida valItemNotaES, eAccionSR valAccion) {
            if (valItemNotaES != null) {
                List<ArticuloInventarioExistencia> vList = new List<ArticuloInventarioExistencia>();
                foreach (RenglonNotaES vItem in valItemNotaES.DetailRenglonNotaES) {
                    if (vItem != null) {
                        decimal vCantidad = valItemNotaES.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario ? vItem.Cantidad : vItem.Cantidad * -1;
                        if (valAccion == eAccionSR.Eliminar || valAccion == eAccionSR.Anular) {
                            vCantidad *= -1;
                        }
                        vList.Add(new ArticuloInventarioExistencia() {
                            ConsecutivoCompania = valItemNotaES.ConsecutivoCompania,
                            CodigoAlmacen = valItemNotaES.CodigoAlmacen,
                            CodigoArticulo = vItem.CodigoArticulo,
                            Cantidad = vCantidad,
                            Ubicacion = "",
                            ConsecutivoAlmacen = valItemNotaES.ConsecutivoAlmacen,
                            DetalleArticuloInventarioExistenciaSerial = new List<ArticuloInventarioExistenciaSerial>()
                        });
                    }
                }
                IArticuloInventarioPdn vArticuloPdn = new clsArticuloInventarioNav();
                vArticuloPdn.ActualizarExistencia(valItemNotaES.ConsecutivoCompania, vList);
            }
        }

        LibResponse INotaDeEntradaSalidaPdn.AnularRecord(IList<NotaDeEntradaSalida> refRecord) {
            LibResponse vResult = new LibResponse();
            foreach (NotaDeEntradaSalida vItem in refRecord) {
                if (vItem != null) {
                    IList<NotaDeEntradaSalida> vItemList = new List<NotaDeEntradaSalida>();
                    vItemList.Add(vItem);
                    RegisterClient();
                    if (vItem.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario) {
                        string vCodigos;
                        if (!HayExistenciaParaNotaDeSalidaDeInventario(vItem, out vCodigos)) {
                            vResult.AddError("No hay existencia suficiente de algunos ítems (" + vCodigos + ") en la Nota: " + vItem.NumeroDocumento + " para anular. El proceso será cancelado.");
                            return vResult;
                        }
                        vResult = base.UpdateRecord(vItemList, false, eAccionSR.Modificar);
                        if (vResult.Success) {
                            ActualizaExistenciaDeArticulos(vItem, eAccionSR.Anular);
                        }
                    } else {
                        if (vItem.StatusNotaEntradaSalidaAsEnum == eStatusNotaEntradaSalida.Anulada) {
                            vResult.AddError("La Nota: " + vItem.NumeroDocumento + " ya fue anulada.");
                            return vResult;
                        } else {
                            vItem.StatusNotaEntradaSalidaAsEnum = eStatusNotaEntradaSalida.Anulada;
                        }
                        vResult = base.UpdateRecord(vItemList, false, eAccionSR.Modificar);
                        if (vResult.Success) {
                            ActualizaExistenciaDeArticulos(vItem, eAccionSR.Anular);
                        }
                    }
                    if (vResult.Success) {
                        ActualizaInformacionDeLoteDeInventario(vItem);
                    }
                }
            }
            return vResult;
        }

        protected override LibResponse DeleteRecord(IList<NotaDeEntradaSalida> refRecord) {
            LibResponse vResult = new LibResponse();
            foreach (NotaDeEntradaSalida vItem in refRecord) {
                if (vItem != null) {
                    IList<NotaDeEntradaSalida> vItemList = new List<NotaDeEntradaSalida>();
                    vItemList.Add(vItem);
                    if (vItem.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario) {
                        string vCodigos;
                        if (!HayExistenciaParaNotaDeSalidaDeInventario(vItem, out vCodigos)) {
                            vResult.AddError("No hay existencia suficiente de algunos ítems (" + vCodigos + ") en la Nota: " + vItem.NumeroDocumento + " para eliminar. El proceso será cancelado.");
                            return vResult;
                        }
                        vResult = base.DeleteRecord(vItemList);
                        if (vResult.Success) {
                            ActualizaExistenciaDeArticulos(vItem, eAccionSR.Eliminar);
                        }
                    } else {
                        vResult = base.DeleteRecord(vItemList);
                        if (vResult.Success) {
                            ActualizaExistenciaDeArticulos(vItem, eAccionSR.Eliminar);
                        }
                    }
                    if (vResult.Success) {
                        ActualizaInformacionDeLoteDeInventario(vItem);
                    }
                }
            }
            return vResult;
        }

        private void ActualizaInformacionDeLoteDeInventario(NotaDeEntradaSalida valItemNotaES) {
            foreach (RenglonNotaES vItemRenglon in valItemNotaES.DetailRenglonNotaES) {
                if (vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote) {
                    if (((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).ExisteLoteDeInventario(vItemRenglon.ConsecutivoCompania, vItemRenglon.CodigoArticulo, vItemRenglon.LoteDeInventario)) {
                        ActualizaLoteDeInventarioInsertaMovimientoDeLoteDeInventario(valItemNotaES, vItemRenglon);
                    }
                }
            }
        }

        private void ActualizaLoteDeInventarioInsertaMovimientoDeLoteDeInventario(NotaDeEntradaSalida valItemNotaES, RenglonNotaES valItemRenglonNotaES) {
            XElement vLoteXElement = ((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).FindByConsecutivoCompaniaCodigoLoteCodigoArticulo(valItemRenglonNotaES.ConsecutivoCompania, valItemRenglonNotaES.LoteDeInventario, valItemRenglonNotaES.CodigoArticulo);
            if (vLoteXElement != null && vLoteXElement.HasElements) {
                LoteDeInventario vLote = new clsLoteDeInventarioNav().ParseToListEntity(vLoteXElement)[0];
                if (vLote != null) {
                    decimal vCant = (valItemNotaES.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario) ? valItemRenglonNotaES.Cantidad : valItemRenglonNotaES.Cantidad * -1;
                    eStatusDocOrigenLoteInv vStatusDocOrigen = (valItemNotaES.StatusNotaEntradaSalidaAsEnum == eStatusNotaEntradaSalida.Vigente) ? eStatusDocOrigenLoteInv.Vigente : eStatusDocOrigenLoteInv.Anulado;
                    LoteDeInventarioMovimiento vLoteMov = new LoteDeInventarioMovimiento();
                    vLoteMov.ConsecutivoCompania = valItemRenglonNotaES.ConsecutivoCompania;
                    vLoteMov.ConsecutivoLote = vLote.Consecutivo;
                    vLoteMov.Fecha = valItemNotaES.Fecha;
                    vLoteMov.ModuloAsEnum = eOrigenLoteInv.NotaEntradaSalida;
                    vLoteMov.Cantidad = vCant;
                    vLoteMov.ConsecutivoDocumentoOrigen = 0;
                    vLoteMov.NumeroDocumentoOrigen = valItemNotaES.NumeroDocumento;
                    vLoteMov.StatusDocumentoOrigenAsEnum = vStatusDocOrigen;
                    vLoteMov.TipoOperacionAsEnum = valItemNotaES.TipodeOperacionAsEnum;
                    vLote.Existencia += vCant;
                    vLote.DetailLoteDeInventarioMovimiento.Add(vLoteMov);

                    ILoteDeInventarioPdn vLotePnd = new clsLoteDeInventarioNav();
                    IList<LoteDeInventario> vListLote = new List<LoteDeInventario>();
                    vListLote.Add(vLote);
                    vLotePnd.ActualizarLote(vListLote);
                }
            }
        }

        private bool HayExistenciaParaNotaDeSalidaDeInventario(NotaDeEntradaSalida valItemNotaES, out string outCodigos) {
            outCodigos = string.Empty;
            if (PermitirSobregiro()) {
                return true;
            } else {
                IArticuloInventarioPdn insArticuloInventarioNav = new clsArticuloInventarioNav();
                string vCodigos = string.Empty;
                foreach (RenglonNotaES vItemRenglon in valItemNotaES.DetailRenglonNotaES) {
                    decimal vDisponibilidad = insArticuloInventarioNav.DisponibilidadDeArticulo(valItemNotaES.ConsecutivoCompania, valItemNotaES.CodigoAlmacen, vItemRenglon.CodigoArticulo, (int)eTipoDeArticulo.Mercancia, vItemRenglon.Serial, vItemRenglon.Rollo);
                    bool vHayExistencia = vItemRenglon.Cantidad < vDisponibilidad;
                    if (!vHayExistencia) {
                        if (LibString.Len(vCodigos) > 0) vCodigos += ", ";
                        vCodigos += vItemRenglon.CodigoArticulo;
                    }
                }
                outCodigos = vCodigos;
                return (LibString.Len(vCodigos) <= 0);
            }
        }	
       
        bool PermitirSobregiro() {
            Ccl.SttDef.ePermitirSobregiro vParametroPermitirSobregiro = (Ccl.SttDef.ePermitirSobregiro)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("FacturaRapida", "PermitirSobregiro");
            return vParametroPermitirSobregiro == Ccl.SttDef.ePermitirSobregiro.PermitirSobregiro || vParametroPermitirSobregiro == Ccl.SttDef.ePermitirSobregiro.NoChequearExistencia;
        }
        
    } //End of class clsNotaDeEntradaSalidaNav
} //End of namespace Galac.Saw.Brl.Inventario