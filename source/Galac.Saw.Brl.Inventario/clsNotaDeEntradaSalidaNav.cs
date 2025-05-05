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
using System.Security.Policy;
using LibGalac.Aos.Dal;
using Galac.Saw.Ccl.SttDef;

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
                case "Almac�n":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsAlmacenNav();
                    vResult = vPdnModule.GetDataForList("Nota de Entrada/Salida", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Art�culo Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
                    vResult = vPdnModule.GetDataForList("Nota de Entrada/Salida", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Lote de Inventario":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsLoteDeInventarioNav();
                    vResult = vPdnModule.GetDataForList("Nota de Entrada/Salida", ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Serial y Rollo":
                    vPdnModule = new Galac.Saw.Brl.Inventario.clsRenglonExistenciaAlmacenNav();
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
                return ((INotaDeEntradaSalidaPdn)this).AnularRecord(vDataEntradaSalida);
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
                        string vCodigos;
                        IList<NotaDeEntradaSalida> vItemList = new List<NotaDeEntradaSalida>();
                        vItemList.Add(vItem);
                        if (vItem.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario) {
                            if (HayArticulosRepetidosEnLosRenglones(vItem, vItem.TipodeOperacionAsEnum, out vCodigos)) {
                                throw new LibGalac.Aos.Catching.GalacValidationException("Existen art�culos repetidos.\nDebe corregir la informaci�n antes de continuar.  \n" + vCodigos);
                            }
                            if (ValidaRegistroDeSerial(vItem)) {    // Para validar que un serial se registre una �nica vez. Tenga una sola entrada para el tipo ->Serial
                                vResult = base.InsertRecord(vItemList, valUseDetail);
                                if (vResult.Success) {
                                    ActualizaExistenciaDeArticulos(vItem, eAccionSR.Insertar);
                                }
                            }
                        } else {                           
                            vCodigos = string.Empty;
                            if (!HayExistenciaParaNotaDeSalidaDeInventario(vItem, out vCodigos)) {
                                throw new LibGalac.Aos.Catching.GalacValidationException("No hay existencia suficiente de algunos �tems (" + vCodigos + ") en la Nota: " + vItem.NumeroDocumento + " para realizar la acci�n. El proceso ser� cancelado.");
                            }
                            vResult = base.InsertRecord(vItemList, valUseDetail);
                            if (vResult.Success) {
                                ActualizaExistenciaDeArticulos(vItem, eAccionSR.Insertar);
                            }
                        }
                        if (vResult.Success) {
                            ActualizaInformacionDeLoteDeInventario(vItem, true);
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
                            DetalleArticuloInventarioExistenciaSerial = GeneraDetalleArticuloInventarioSerial(valItemNotaES.ConsecutivoCompania, vItem.CodigoArticulo, valItemNotaES.CodigoAlmacen, valItemNotaES.ConsecutivoAlmacen, vCantidad, vItem.Serial, vItem.Rollo, true),
                            DetalleExistenciasPorAlmacenDetLoteInv = GeneraDetalleExistenciasPorAlmacenDetLoteInv(valItemNotaES.ConsecutivoCompania, vItem.CodigoArticulo, valItemNotaES.ConsecutivoAlmacen, vCantidad, vItem.LoteDeInventario)
                        });
                    }
                }
                IArticuloInventarioPdn vArticuloPdn = new clsArticuloInventarioNav();
                vArticuloPdn.ActualizarExistencia(valItemNotaES.ConsecutivoCompania, vList);
            }
        }

        private List<ExistenciaPorAlmacenDetLoteInv> GeneraDetalleExistenciasPorAlmacenDetLoteInv(int valConsecutivoCompania, string valCodigoArticulo,  int valConsecutivoAlmacen, decimal valCantidad, string valCodigoLoteDeInventario) {
            List<ExistenciaPorAlmacenDetLoteInv> vResult = new List<ExistenciaPorAlmacenDetLoteInv>();
            ExistenciaPorAlmacenDetLoteInv vExistencia = new ExistenciaPorAlmacenDetLoteInv {
                ConsecutivoCompania = valConsecutivoCompania,
                CodigoArticulo = valCodigoArticulo,
                ConsecutivoAlmacen = valConsecutivoAlmacen,
                Cantidad = valCantidad,
                ConsecutivoLoteInventario = BuscarConsecutivoDeLoteInventario(valConsecutivoCompania, valCodigoArticulo, valCodigoLoteDeInventario),
                Ubicacion = string.Empty
            };
            vResult.Add(vExistencia);
            return vResult;
        }

        private int BuscarConsecutivoDeLoteInventario(int valConsecutivoCompania, string valCodigoArticulo, string valCodigoLoteDeInventario) {
            int vConsecutivoLote = 0;
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("CodigoArticulo", valCodigoArticulo, 30);
            vParams.AddInString("CodigoLote", valCodigoLoteDeInventario, 30);
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT Consecutivo FROM Saw.LoteDeInventario");
            vSql.AppendLine("WHERE CodigoLote = @CodigoLote");
            vSql.AppendLine("AND CodigoArticulo = @CodigoArticulo");
            vSql.AppendLine("AND ConsecutivoCompania = @ConsecutivoCompania");
            XElement vXElementLote = LibBusiness.ExecuteSelect(vSql.ToString(), vParams.Get(), "", 0);
            if (vXElementLote != null && vXElementLote.HasElements) {
                vConsecutivoLote = LibConvert.ToInt(vXElementLote.Descendants("GpResult").FirstOrDefault().Element("Consecutivo").Value);
            }
            return vConsecutivoLote;
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
                            vResult.AddError("No hay existencia suficiente de algunos �tems (" + vCodigos + ") en la Nota: " + vItem.NumeroDocumento + " para anular. El proceso ser� cancelado.");
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
                        ActualizaInformacionDeLoteDeInventario(vItem, false);   // Se debe pasar en falso el 2do argumento porque en el m�todo ActualizaLoteDeInventarioInsertaMovimientoDeLoteDeInventario
                                                                                // la cantidad se va a multiplicar por -1 dos veces 
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
                            vResult.AddError("No hay existencia suficiente de algunos �tems (" + vCodigos + ") en la Nota: " + vItem.NumeroDocumento + " para eliminar. El proceso ser� cancelado.");
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
                        ActualizaInformacionDeLoteDeInventario(vItem, false);
                    }
                }
            }
            return vResult;
        }

        private void ActualizaInformacionDeLoteDeInventario(NotaDeEntradaSalida valItemNotaES, bool valAumentaCantidad) {
            foreach (RenglonNotaES vItemRenglon in valItemNotaES.DetailRenglonNotaES) {
                if (vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeElaboracion) {
                    if (((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).ExisteLoteDeInventario(vItemRenglon.ConsecutivoCompania, vItemRenglon.CodigoArticulo, vItemRenglon.LoteDeInventario)) {
                        ActualizaLoteDeInventarioInsertaMovimientoDeLoteDeInventario(valItemNotaES, vItemRenglon, valAumentaCantidad);
                    }
                }
            }
        }

        private void ActualizaLoteDeInventarioInsertaMovimientoDeLoteDeInventario(NotaDeEntradaSalida valItemNotaES, RenglonNotaES valItemRenglonNotaES, bool valAumentaCantidad) {
            XElement vLoteXElement = ((ILoteDeInventarioPdn)new clsLoteDeInventarioNav()).FindByConsecutivoCompaniaCodigoLoteCodigoArticulo(valItemRenglonNotaES.ConsecutivoCompania, valItemRenglonNotaES.LoteDeInventario, valItemRenglonNotaES.CodigoArticulo);
            if (vLoteXElement != null && vLoteXElement.HasElements) {
                LoteDeInventario vLote = new clsLoteDeInventarioNav().ParseToListEntity(vLoteXElement)[0];
                if (vLote != null) {
                    decimal vCant = (valItemNotaES.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario) ? valItemRenglonNotaES.Cantidad : valItemRenglonNotaES.Cantidad * -1;                    
                    eStatusDocOrigenLoteInv vStatusDocOrigen = (valItemNotaES.StatusNotaEntradaSalidaAsEnum == eStatusNotaEntradaSalida.Vigente) ? eStatusDocOrigenLoteInv.Vigente : eStatusDocOrigenLoteInv.Anulado;
                    if (vStatusDocOrigen == eStatusDocOrigenLoteInv.Anulado &&
                        valItemNotaES.GeneradoPorAsEnum == eTipoGeneradoPorNotaDeEntradaSalida.OrdenDeProduccion) {
                        vCant = vCant * -1;
                    }
                    vCant = (valAumentaCantidad) ? vCant : vCant * -1;

                    LoteDeInventarioMovimiento vLoteMov = new LoteDeInventarioMovimiento();
                    vLoteMov.ConsecutivoCompania = valItemRenglonNotaES.ConsecutivoCompania;
                    vLoteMov.ConsecutivoLote = vLote.Consecutivo;
                    vLoteMov.Fecha = valItemNotaES.Fecha;
                    if (valItemNotaES.GeneradoPorAsEnum == eTipoGeneradoPorNotaDeEntradaSalida.OrdenDeProduccion) {
                        vLoteMov.ModuloAsEnum = eOrigenLoteInv.Produccion;
                    } else {
                        vLoteMov.ModuloAsEnum = eOrigenLoteInv.NotaEntradaSalida;
                    }
                    
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
                    decimal vDisponibilidad = 0;
                    bool vEsArticuloTipoLote = (vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeElaboracion);
                    if (vEsArticuloTipoLote) {
                        vDisponibilidad = insArticuloInventarioNav.DisponibilidadDeArticulo(valItemNotaES.ConsecutivoCompania, vItemRenglon.CodigoArticulo, BuscarConsecutivoDeLoteInventario(valItemNotaES.ConsecutivoCompania, vItemRenglon.CodigoArticulo, vItemRenglon.LoteDeInventario), valItemNotaES.ConsecutivoAlmacen);
                    } else {
                        if (vItemRenglon.TipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColor) {
                            vDisponibilidad = insArticuloInventarioNav.DisponibilidadDeArticuloTallaColor(valItemNotaES.ConsecutivoCompania, valItemNotaES.CodigoAlmacen, vItemRenglon.CodigoArticulo);
                        } else {
                            vDisponibilidad = insArticuloInventarioNav.DisponibilidadDeArticulo(valItemNotaES.ConsecutivoCompania, valItemNotaES.CodigoAlmacen, vItemRenglon.CodigoArticulo, (int)eTipoDeArticulo.Mercancia, vItemRenglon.Serial, vItemRenglon.Rollo);
                        }
                    }
                    bool vHayExistencia = vItemRenglon.Cantidad <= vDisponibilidad;
                    if (!vHayExistencia) {
                        if (LibString.Len(vCodigos) > 0) vCodigos += ", ";
                        if (vEsArticuloTipoLote) {
                            vCodigos += vItemRenglon.CodigoArticulo + " Lote:" + vItemRenglon.LoteDeInventario;
                        } else {
                            vCodigos += vItemRenglon.CodigoArticulo;
                        }
                    }
                }
                outCodigos = vCodigos;
                return (LibString.Len(vCodigos) == 0);
            }
        }	
       
        bool PermitirSobregiro() {
            return LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "PermitirSobregiro");
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Nota de Entrada/Salida.Insertar")]
        LibResponse INotaDeEntradaSalidaPdn.ReversarNotaES(int valConsecutivoCompania, string valNumeroDocumento) {
            LibResponse vResult = new LibResponse();
            LibGpParams vParams = new LibGpParams();
            string vComentario = "Reverso de la Nota E/S: " + valNumeroDocumento;
            RegisterClient();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroDocumento", valNumeroDocumento, 11);
            ILibBusinessMasterComponent<IList<NotaDeEntradaSalida>, IList<NotaDeEntradaSalida>> insNotaES = new clsNotaDeEntradaSalidaNav();
            NotaDeEntradaSalida vNotaES = insNotaES.GetData(eProcessMessageType.SpName, "NotaDeEntradaSalidaGET", vParams.Get(), true).FirstOrDefault();
            if (vNotaES == null) {
                vResult.AddError("No se encontr� la informaci�n para la Nota de E/S: " + valNumeroDocumento);
                return vResult;
            }
            if (vNotaES.TipodeOperacionAsEnum == eTipodeOperacion.Retiro) {
                vResult.AddError("El tipo de operaci�n Retiro no puede ser reversado.");
                return vResult;
            }
            if (vNotaES.GeneradoPorAsEnum != eTipoGeneradoPorNotaDeEntradaSalida.Usuario) {
                vResult.AddError("Solo se pueden reversar las Notas de E/S ingresadas por Usuarios.");
                return vResult;
            }
            if (vNotaES.StatusNotaEntradaSalidaAsEnum != eStatusNotaEntradaSalida.Vigente) {
                vResult.AddError("Solo se pueden reversar las Notas de E/S con status Vigente.");
                return vResult;
            }
            if (LibString.S1StartsWithS2(vNotaES.Comentarios, "Reverso de la Nota E/S:")) {
                vResult.AddError("Esta Nota de E/S ya es un reverso y no puede ser reversada.");
                return vResult;
            }
            if (ExisteReversoParaEstaNota(valConsecutivoCompania, vComentario)) {
                vResult.AddError("Ya existe un reverso para esta Nota de E/S");
                return vResult;
            }
            string vNextNumero;
            LibGpParams vParms = new LibGpParams();
            vParms.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            XElement vResulset = _Db.QueryInfo(eProcessMessageType.Message, "ProximoNumeroDocumento", vParms.Get());
            vNextNumero = LibXml.GetPropertyString(vResulset, "NumeroDocumento");

            vNotaES.NumeroDocumento = vNextNumero;
            vNotaES.Comentarios = vComentario;
            vNotaES.TipodeOperacionAsEnum = vNotaES.TipodeOperacionAsEnum == eTipodeOperacion.EntradadeInventario ? eTipodeOperacion.SalidadeInventario : eTipodeOperacion.EntradadeInventario;
            vNotaES.Fecha = LibDate.Today();

            List<NotaDeEntradaSalida> vListaNotaES = new List<NotaDeEntradaSalida>() { vNotaES };

            vResult = insNotaES.DoAction(vListaNotaES, eAccionSR.Insertar, null, true);

            return vResult;
        }

        private bool ExisteReversoParaEstaNota(int valConsecutivoCompania, string valComentario) {
            bool vResult = false;
            QAdvSql insSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendFormat("SELECT * FROM NotaDeEntradaSalida WHERE ");
            vSql.AppendFormat(" ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
            vSql.AppendFormat(" AND Comentarios LIKE " + insSql.ToSqlValue(valComentario + "%"));            
            vResult = (new LibDatabase().RecordCountOfSql(vSql.ToString()) > 0);
            return vResult;
        }
        private List<ArticuloInventarioExistenciaSerial> GeneraDetalleArticuloInventarioSerial(int valConsecutivoCompania, string valCodigoArticulo, string valCodigoAlmacen, int valConsecutivoAlmacen, decimal valCantidad, string valSerial, string valRollo, bool valAumentaCantidad) {
            List<ArticuloInventarioExistenciaSerial> vResult = new List<ArticuloInventarioExistenciaSerial>();
            decimal vCantidad = 0;

            vCantidad = valCantidad;
            if (!valAumentaCantidad) {
                vCantidad = vCantidad * -1;
            }
            ArticuloInventarioExistenciaSerial vArticuloInventarioExistenciaSerial = new ArticuloInventarioExistenciaSerial() {
                ConsecutivoCompania = valConsecutivoCompania,
                CodigoAlmacen = valCodigoAlmacen,
                CodigoArticulo = valCodigoArticulo,
                ConsecutivoRenglon = 0,
                CodigoSerial = valSerial,
                CodigoRollo = (!LibString.IsNullOrEmpty(valRollo) ? valRollo : "0"),
                Cantidad = vCantidad,
                Ubicacion = "",
                ConsecutivoAlmacen = valConsecutivoAlmacen
            };
            vResult.Add(vArticuloInventarioExistenciaSerial);
            return vResult;
        }

        private bool ValidaRegistroDeSerial(NotaDeEntradaSalida refRecord) {
            XElement vData = new XElement("GpData");
            foreach (RenglonNotaES item in refRecord.DetailRenglonNotaES) {
                vData.Add(new XElement("GpResult",
                   new XElement("CodigoArticulo", item.CodigoArticulo),
                   new XElement("Serial", item.Serial),
                   new XElement("Rollo", item.Rollo)));
            }
            Galac.Saw.Ccl.Inventario.IArticuloInventarioPdn vArticuloPdn = new Galac.Saw.Brl.Inventario.clsArticuloInventarioNav();
            return vArticuloPdn.ValidaExistenciaDeArticuloSerial(refRecord.ConsecutivoCompania, vData);

        }
        private bool HayArticulosRepetidosEnLosRenglones(NotaDeEntradaSalida refRecord, eTipodeOperacion vTipoDeOperacion, out string vArtDuplicados) {
            LibResponse vResult = new LibResponse();
            string vArticulo = string.Empty, vCodigos = string.Empty;
            vArtDuplicados = string.Empty;
            int vlinea = 1;

            foreach (RenglonNotaES item in refRecord.DetailRenglonNotaES) {
                if (item.TipoArticuloInvAsEnum == eTipoArticuloInv.Simple || item.TipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColor) {
                    vArticulo = item.CodigoArticulo; 
                } else if (item.TipoArticuloInvAsEnum == eTipoArticuloInv.UsaSerial || item.TipoArticuloInvAsEnum == eTipoArticuloInv.UsaSerialRollo || item.TipoArticuloInvAsEnum == eTipoArticuloInv.UsaTallaColorySerial) {
                    if (vTipoDeOperacion == eTipodeOperacion.EntradadeInventario) {
                        vArticulo = item.CodigoArticulo + item.Serial + item.Rollo;
                    }
                } else if (item.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeVencimiento || item.TipoArticuloInvAsEnum == eTipoArticuloInv.Lote || item.TipoArticuloInvAsEnum == eTipoArticuloInv.LoteFechadeElaboracion ) {
                    vArticulo = item.CodigoArticulo + item.LoteDeInventario;
                }
                if (LibString.Len(vArticulo) > 0) {
                    if (LibString.S1IsInS2(vArticulo, vCodigos)) {
                        vArtDuplicados += item.CodigoArticulo + " L�nea " + LibConvert.ToStr(vlinea) + "\n";
                    } else {
                        vCodigos += vArticulo + ", ";
                    }
                }
                vArticulo = string.Empty;
                vlinea += 1;
            }
            return (LibString.Len(vArtDuplicados) > 0);
        }
    } //End of class clsNotaDeEntradaSalidaNav
} //End of namespace Galac.Saw.Brl.Inventario