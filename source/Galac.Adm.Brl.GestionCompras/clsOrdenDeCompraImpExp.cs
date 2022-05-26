using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Security.Permissions;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.GestionCompras;
using System.ComponentModel;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Brl;
using Galac.Adm.Dal.GestionCompras;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;

namespace Galac.Adm.Brl.GestionCompras {

    public class clsOrdenDeCompraImpExp: LibMRO, ILibImpExp {
        #region Variables
        ILibDataImport _Db;
        IList<OrdenDeCompra> vOrdenCompraList;
        IList<OrdenDeCompraDetalleArticuloInventarioImportar> vOrdenCompraDetalleListImportar;
        IList<OrdenDeCompraDetalleArticuloInventario> vOrdenCompraDetalleList;
        IList<OrdenDeCompraRechazadas> vOrdenCompraRechazadas;
        string vNumero;
        string vFecha;
        string vCodigoProveedor;
        string vTipoDeCompra;
        string vCodigoMoneda; 
        string vCambioABolivares;
        string vComentarios; 
        string vCondicionesDeEntrega; 
        string vCondicionesDePago;
        string vCondicionesDeImportacion;
        string vCodigoArticulo;
        string vCantidad;
        string vCostoUnitario;
        private object refErrorMessage;
        #endregion //Variables
        #region Constructores

        public clsOrdenDeCompraImpExp() {
            vOrdenCompraList = new List<OrdenDeCompra>();
            vOrdenCompraDetalleListImportar = new List<OrdenDeCompraDetalleArticuloInventarioImportar>();
            vOrdenCompraDetalleList = new List<OrdenDeCompraDetalleArticuloInventario>();
            vOrdenCompraRechazadas = new List<OrdenDeCompraRechazadas>();
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Db = (ILibDataImport)RegisterType();
            } else {
                _Db = new Galac.Adm.Dal.GestionCompras.clsOrdenDeCompraDat();
            }
            if (_Db is ILibDataImportBulkInsert) {
                ((ILibDataImportBulkInsert)_Db).Action = ((IlibImpExpBulkInsert)this).Action;
            }
        }
        #endregion //Inicializacion DAL - a modificar si Remoting
        #region Miembros de ILibImpExp

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Base de Datos.Creación")]

        LibXmlResult ILibImpExp.AddRecord(XmlReader refRecord, LibProgressManager refManager, bool valShowMessage){
            RegistraCliente();
            return _Db.Import(refRecord, refManager, valShowMessage);
        }
        public LibResponse Importar(string valPath, eExportDelimiterType valSeparador, BackgroundWorker valBWorker){
            LibResponse vResultOperacion = new LibResponse();
            LibResponse vResult = new LibResponse();
            StringBuilder vErrorMessage = new StringBuilder();
            IOrdenDeCompraDatPdn insOrdenDeCompraDatPdn = new clsOrdenDeCompraDat();
            IOrdenDeCompraDatDetallePdn insOrdenDeCompraDatDetallePdn = new clsOrdenDeCompraDetalleArticuloInventarioDat();
            IOrdenDeCompraPdn insOrdenDeCompraPdn = new clsOrdenDeCompraNav();
            int vIndex = 1;
            foreach (var item in vOrdenCompraList){
                try{
                    vResultOperacion = insOrdenDeCompraDatPdn.InsertarListaDeOrdenDeCompraMaster(new List<OrdenDeCompra>() { item });
                    vResult.Success = vResult.Success && vResultOperacion.Success;
                    if (!vResultOperacion.Success){
                        vResult.AddInformation(vResultOperacion.GetInformation());
                    }
                }
                catch{
                    vResult.AddError(vResultOperacion.GetInformation());
                }
                valBWorker.ReportProgress(1, string.Format("Insertando {0:n0} de {1:n0}", vIndex, vOrdenCompraList.Count()));
                vIndex++;
            }
            CargaListaDetalleImportar(ref vOrdenCompraDetalleList);
            foreach (var item1 in vOrdenCompraDetalleList) {
                try {
                    vResultOperacion = insOrdenDeCompraDatDetallePdn.InsertarListaDeOrdenDeCompraDetail(new List<OrdenDeCompraDetalleArticuloInventario>() { item1 });
                    vResult.Success = vResult.Success && vResultOperacion.Success;
                    if (!vResultOperacion.Success){
                        vResult.AddInformation(vResultOperacion.GetInformation());
                    }
                }
                catch
                {
                    vResult.AddError(vResultOperacion.GetInformation());
                }
                valBWorker.ReportProgress(1, string.Format("Insertando {0:n0} de {1:n0}", vIndex, vOrdenCompraList.Count()));
                vIndex++;
            }

            return vResult;
        }
        bool DistributeLine(XElement valRecord, ref IList<OrdenDeCompra> valOrdenCompraValida, ref IList<OrdenDeCompraDetalleArticuloInventarioImportar> valOrdenCompraDetalleValida, ref IList<OrdenDeCompraDetalleArticuloInventario> valOrdenCompraDetalle, ref IList<OrdenDeCompraRechazadas> valOrdenCompraRechazadas, BackgroundWorker valBWorker, StringBuilder refErrorMessage, ref int valCantidadRegistrosValidos) {
            int i = 1;
            bool vResult = false;
            bool vRegistrosValidas = true;
            bool vRegistroValidoImport = false;
            StringBuilder vErrMssg = new StringBuilder();
            IOrdenDeCompraPdn OrdenDeCompraPdn = new clsOrdenDeCompraNav();
            foreach (XElement vItem in valRecord.Nodes()){
                OrdenDeCompraRechazadas vOrdenCompraRechazadas = new OrdenDeCompraRechazadas();
                vRegistrosValidas = new clsOrdenDeCompraNav().ValidarDatosParaImportarOrdenCompra(eAccionSR.Importar, vItem, out vErrMssg);
                if (!vErrMssg.ToString().Equals("")){
                    refErrorMessage.AppendLine("Línea " + i + ", Orden de Compra N°: " + LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero"))  + "\n" + vErrMssg.ToString());
                }
                if (!vRegistrosValidas){
                    vOrdenCompraRechazadas.NumeroOC = LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero"));
                    valOrdenCompraRechazadas.Add(vOrdenCompraRechazadas);
                }
                i++;
            }
            foreach (XElement vItem in valRecord.Nodes()){
                OrdenDeCompra vOrdenCompra = new OrdenDeCompra();
                OrdenDeCompraDetalleArticuloInventarioImportar vOrdenCompraDetalleImport = new OrdenDeCompraDetalleArticuloInventarioImportar();
                if (valOrdenCompraRechazadas.Count > 0){
                    foreach (var vItemRechazado in valOrdenCompraRechazadas) {
                        if (LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero")) != LibConvert.ToStr(vItemRechazado.NumeroOC)) {
                            vOrdenCompra.Serie = "OC";
                            vOrdenCompra.Numero = LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero"));
                            vOrdenCompra.Fecha = LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vItem, "Fecha"));
                            vOrdenCompra.ConsecutivoProveedor = OrdenDeCompraPdn.InfoProveedor(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CodigoProveedor")));
                            vOrdenCompra.TipoDeCompraAsEnum = (eTipoCompra)(OrdenDeCompraPdn.ValidaTipoDeCompra(LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "TipoDeCompra"))));
                            vOrdenCompra.Moneda = ((IMonedaPdn)new clsMonedaNav()).GetNombreMoneda(LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "CodigoMoneda")));
                            vOrdenCompra.CodigoMoneda = LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "CodigoMoneda"));
                            vOrdenCompra.CambioABolivares = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "CambioABolivares"));
                            vOrdenCompra.TotalRenglones = OrdenDeCompraPdn.CalculaTotalRenglonOC(valRecord, LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero")), vOrdenCompra.CodigoMoneda, vOrdenCompra.CambioABolivares);
                            vOrdenCompra.TotalCompra = vOrdenCompra.TotalRenglones;
                            vOrdenCompra.Comentarios = LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "Comentarios"));
                            vOrdenCompra.CondicionesDeEntrega = LibString.Mid((LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CondicionesDeEntrega"))), 0, 500);
                            vOrdenCompra.CondicionesDePago = LibConvert.ToInt(LibXml.GetElementValueOrEmpty(vItem, "CondicionesDePago"));
                            vOrdenCompra.CondicionesDeImportacionAsEnum = (eCondicionDeImportacion)LibConvert.DbValueToEnum(LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CondicionesDeImportacion")));
                            vOrdenCompraDetalleImport.NumeroOC = LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "Numero"));
                            vOrdenCompraDetalleImport.CodigoArticulo = LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CodigoArticulo"));
                            vOrdenCompraDetalleImport.DescripcionArticulo = OrdenDeCompraPdn.InfoArticulo(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CodigoArticulo")));
                            vOrdenCompraDetalleImport.Cantidad = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "Cantidad"));
                            vOrdenCompraDetalleImport.CostoUnitario = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "CostoUnitario"));
                            vRegistroValidoImport = false;
                        }
                    }
                } else {
                    vOrdenCompra.Serie = "OC";
                    vOrdenCompra.Numero = LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero"));
                    vOrdenCompra.Fecha = LibConvert.ToDate(LibXml.GetElementValueOrEmpty(vItem, "Fecha"));
                    vOrdenCompra.ConsecutivoProveedor = OrdenDeCompraPdn.InfoProveedor(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CodigoProveedor")));
                    vOrdenCompra.TipoDeCompraAsEnum = (eTipoCompra)(OrdenDeCompraPdn.ValidaTipoDeCompra(LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "TipoDeCompra"))));
                    vOrdenCompra.Moneda = ((IMonedaPdn)new clsMonedaNav()).GetNombreMoneda(LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "CodigoMoneda")));
                    vOrdenCompra.CodigoMoneda = LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "CodigoMoneda"));
                    vOrdenCompra.CambioABolivares = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "CambioABolivares"));
                    vOrdenCompra.TotalRenglones = OrdenDeCompraPdn.CalculaTotalRenglonOC(valRecord, LibText.UCase(LibXml.GetElementValueOrEmpty(vItem, "Numero")), vOrdenCompra.CodigoMoneda, vOrdenCompra.CambioABolivares);
                    vOrdenCompra.TotalCompra = vOrdenCompra.TotalRenglones;
                    vOrdenCompra.Comentarios = LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "Comentarios"));
                    vOrdenCompra.CondicionesDeEntrega = LibString.Mid((LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CondicionesDeEntrega"))), 0, 500);
                    vOrdenCompra.CondicionesDePago = LibConvert.ToInt(LibXml.GetElementValueOrEmpty(vItem, "CondicionesDePago"));
                    vOrdenCompra.CondicionesDeImportacionAsEnum = (eCondicionDeImportacion)LibConvert.DbValueToEnum(LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CondicionesDeImportacion")));
                    vOrdenCompraDetalleImport.NumeroOC = LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "Numero"));
                    vOrdenCompraDetalleImport.CodigoArticulo = LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CodigoArticulo"));
                    vOrdenCompraDetalleImport.DescripcionArticulo = OrdenDeCompraPdn.InfoArticulo(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibConvert.ToStr(LibXml.GetElementValueOrEmpty(vItem, "CodigoArticulo")));
                    vOrdenCompraDetalleImport.Cantidad = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "Cantidad"));
                    vOrdenCompraDetalleImport.CostoUnitario = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(vItem, "CostoUnitario"));
                    vRegistroValidoImport = true;
                }    
                if (vRegistroValidoImport) {
                    valOrdenCompraValida.Add(vOrdenCompra);
                    valOrdenCompraDetalleValida.Add(vOrdenCompraDetalleImport);
                }
                valBWorker.ReportProgress(1, string.Format("Validando {0:n0} de {1:n0}", i, valRecord.Nodes().Count()));
                i++;
            }
            valCantidadRegistrosValidos = valCantidadRegistrosValidos + valOrdenCompraValida.Count;
            vResult = valCantidadRegistrosValidos > 0 ? true : false;
            return vResult;
        }
        public XElement ImportFile(string valPathFile, string valSeparator) {
            StreamReader vStreamReader = new StreamReader(valPathFile, System.Text.Encoding.UTF8);
            vNumero = string.Empty;
            vFecha = string.Empty;
            vCodigoProveedor = string.Empty;
            vCodigoMoneda = string.Empty;
            vCambioABolivares = string.Empty;
            vComentarios = string.Empty;
            vCondicionesDeEntrega = string.Empty;
            vCondicionesDePago = string.Empty;
            vCondicionesDeImportacion = string.Empty;
            vTipoDeCompra = string.Empty;
            int j = 0;
            XElement xmlTree = new XElement("GpData");
            List<string> vLista = vStreamReader.Lines().ToList();
            for (int i = 0; i <= vLista.Count - 1; i++) {
                string[] vAtributo = LibString.Split(vLista[i], valSeparator, false);
                if (vAtributo.Length > 0) {
                    for (j = 0; j <= vAtributo.Length - 1; j++) {
                        if (vAtributo[j] != null) {
                            AsignarValores(j, vAtributo[j]);
                        }
                    }
                    AsignarValoresPorDefecto(j);
                    xmlTree.Add(new XElement("GpResult",
                    new XElement("Numero", vNumero),
                    new XElement("Fecha", vFecha),
                    new XElement("CodigoProveedor", vCodigoProveedor),
                    new XElement("TipoDeCompra", vTipoDeCompra),
                    new XElement("CodigoMoneda", vCodigoMoneda),
                    new XElement("CambioABolivares", vCambioABolivares),
                    new XElement("Comentarios", vComentarios),
                    new XElement("CondicionesDeEntrega", vCondicionesDeEntrega),
                    new XElement("CondicionesDePago", vCondicionesDePago),
                    new XElement("CondicionesDeImportacion", vCondicionesDeImportacion),
                    new XElement("CodigoArticulo", vCodigoArticulo),
                    new XElement("Cantidad", vCantidad),
                    new XElement("CostoUnitario", vCostoUnitario)));
                }
            }
            return xmlTree;
        }
        private void AsignarValores(int vIndice, string vValor){
            switch (vIndice){
                case 0: vNumero = vValor; break;
                case 1: vFecha = vValor; break;
                case 2: vCodigoProveedor = vValor; break;
                case 3: vTipoDeCompra = vValor; break;
                case 4: vCodigoMoneda = vValor; break;
                case 5: vCambioABolivares = vValor; break;
                case 6: vComentarios = vValor; break;
                case 7: vCondicionesDeEntrega = vValor; break;
                case 8: vCondicionesDePago = vValor; break;
                case 9: vCondicionesDeImportacion = vValor; break;
                case 10: vCodigoArticulo = vValor; break;
                case 11: vCantidad = vValor; break;
                case 12: vCostoUnitario = vValor; break;
            }
        }
        private void AsignarValoresPorDefecto(int vIndice){
            for (int i = vIndice; i <= 19; i++){
                switch (i){
                    case 0: vNumero = ""; break;
                    case 1: vFecha = ""; break;
                    case 2: vCodigoProveedor = ""; break;
                    case 3: vTipoDeCompra = LibConvert.ToStr(eTipoCompra.Nacional); break;
                    case 4: vCodigoMoneda = ""; break;
                    case 5: vCambioABolivares = ""; break;
                    case 6: vComentarios = ""; break;
                    case 7: vCondicionesDeEntrega = ""; break;
                    case 8: vCondicionesDePago = ""; break;
                    case 9: vCondicionesDeImportacion = ""; break;
                    case 10: vCodigoArticulo = ""; break;
                    case 11: vCantidad = ""; break;
                    case 12: vCostoUnitario = ""; break;
                }
            }
        }
        public bool VerifyIntegrityOfRecord(XElement valRecord, StringBuilder refErrorMessage, BackgroundWorker valBWorker, ref int vCantidadRegistrosValidos) {
            LibResponse vResult = new LibResponse();
            return vResult.Success = DistributeLine(valRecord, ref vOrdenCompraList, ref vOrdenCompraDetalleListImportar, ref vOrdenCompraDetalleList, ref vOrdenCompraRechazadas, valBWorker, refErrorMessage, ref vCantidadRegistrosValidos);
        }
        private bool CargaListaDetalleImportar(ref IList<OrdenDeCompraDetalleArticuloInventario> valOrdenCompraDetalleValida) {
            bool vResult = false;
            int vConsecutivo = 1;
            string vPrimerNumero = LibConvert.ToStr(vOrdenCompraDetalleListImportar[0].NumeroOC);
            foreach (var vItem in vOrdenCompraDetalleListImportar){
                IOrdenDeCompraPdn insOrdenDeCompraPdn = new clsOrdenDeCompraNav();
                OrdenDeCompraDetalleArticuloInventario vOrdenCompraDetalle = new OrdenDeCompraDetalleArticuloInventario();
                if (vPrimerNumero == LibConvert.ToStr(vItem.NumeroOC)){
                    vConsecutivo++;
                    vOrdenCompraDetalle.Consecutivo = vConsecutivo;
                    vPrimerNumero = LibConvert.ToStr(vItem.NumeroOC);
                } else{
                    vConsecutivo = 1;
                    vOrdenCompraDetalle.Consecutivo = vConsecutivo;
                    vPrimerNumero = LibConvert.ToStr(vItem.NumeroOC);
                }
                vOrdenCompraDetalle.ConsecutivoOrdenDeCompra = insOrdenDeCompraPdn.InfoNumeroOC(vItem.ConsecutivoCompania, vItem.NumeroOC);
                vOrdenCompraDetalle.CodigoArticulo = vItem.CodigoArticulo;
                vOrdenCompraDetalle.DescripcionArticulo = insOrdenDeCompraPdn.InfoArticulo(LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"), LibConvert.ToStr(vItem.CodigoArticulo));
                vOrdenCompraDetalle.Cantidad = vItem.Cantidad;
                vOrdenCompraDetalle.CostoUnitario = vItem.CostoUnitario;
                valOrdenCompraDetalleValida.Add(vOrdenCompraDetalle);
                vResult = true;
            }
            return vResult;
        }

        bool ILibImpExp.VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument refXmlDocResult, StringBuilder refErrorMessage){
            throw new NotImplementedException();
        }
        XmlReader ILibImpExp.ImportFile(string valPathFile, string valSeparator){
            throw new NotImplementedException();
        }
        #endregion //Miembros de ILibImpExp
        #endregion //Metodos Generados


    } //End of class clsOrdenDeCompraImpExp

} //End of namespace Galac.Adm.Brl.GestionCompras


