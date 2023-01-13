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
using Galac.Adm.Ccl.Vendedor;
using System.ComponentModel;
using Galac.Adm.Dal.Vendedor;

namespace Galac.Adm.Brl.Vendedor {
    public class clsVendedorImpExp : LibMRO, ILibImpExp, IlibImpExpBulkInsert {
        #region Variables
        ILibDataImport _Db;
        eActionImpExp IlibImpExpBulkInsert.Action { get; set; }
        IList<Ccl.Vendedor.Vendedor> vVendedorList;
        IList<VendedorDetalleComisionesImportar> vVendedorDetalleComisionesListImportar;
        IList<VendedorDetalleComisiones> vVendedorDetalleComisionesList;
        #endregion //Variables
        #region Constructores

        public clsVendedorImpExp() {
            vVendedorList = new List<Ccl.Vendedor.Vendedor>();
            vVendedorDetalleComisionesListImportar = new List<VendedorDetalleComisionesImportar>();
            vVendedorDetalleComisionesList = new List<VendedorDetalleComisiones>();
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Db = (ILibDataImport)RegisterType();
            } else {
                _Db = new Dal.Vendedor.clsVendedorDat();
            }
            if (_Db is ILibDataImportBulkInsert) {
                ((ILibDataImportBulkInsert)_Db).Action = ((IlibImpExpBulkInsert)this).Action;
            }
        }
        #endregion //Inicializacion DAL - a modificar si Remoting
        #region Miembros de ILibImpExp

        [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Base de Datos.Creación")]
        LibXmlResult ILibImpExp.AddRecord(XmlReader refRecord, LibProgressManager refManager, bool valShowMessage) {
            RegistraCliente();
            return _Db.Import(refRecord, refManager, valShowMessage);
        }

        public XElement ImportFile(string valPathFile, string valSeparator) {
            StreamReader vStreamReader = new StreamReader(valPathFile, Encoding.Default);
            XElement xmlTree = new XElement("GpData",
                  from line in vStreamReader.Lines()
                  let items = LibString.Split(line, valSeparator, false)
                  select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", items[0]),
                    new XElement("Consecutivo", items[1]),
                    new XElement("Codigo", items[2]),
                    new XElement("Nombre", items[3]),
                    new XElement("RIF", items[4]),
                    new XElement("StatusVendedor", items[5]),
                    new XElement("Direccion", items[6]),
                    new XElement("Ciudad", items[7]),
                    new XElement("ZonaPostal", items[8]),
                    new XElement("Telefono", items[9]),
                    new XElement("Fax", items[10]),
                    new XElement("Email", items[11]),
                    new XElement("Notas", items[12]),
                    new XElement("ComisionPorVenta", items[13]),
                    new XElement("ComisionPorCobro", items[14]),
                    new XElement("TopeInicialVenta1", items[15]),
                    new XElement("TopeFinalVenta1", items[16]),
                    new XElement("PorcentajeVentas1", items[17]),
                    new XElement("TopeFinalVenta2", items[18]),
                    new XElement("PorcentajeVentas2", items[19]),
                    new XElement("TopeFinalVenta3", items[20]),
                    new XElement("PorcentajeVentas3", items[21]),
                    new XElement("TopeFinalVenta4", items[22]),
                    new XElement("PorcentajeVentas4", items[23]),
                    new XElement("TopeFinalVenta5", items[24]),
                    new XElement("PorcentajeVentas5", items[25]),
                    new XElement("TopeInicialCobranza1", items[26]),
                    new XElement("TopeFinalCobranza1", items[27]),
                    new XElement("PorcentajeCobranza1", items[28]),
                    new XElement("TopeFinalCobranza2", items[29]),
                    new XElement("PorcentajeCobranza2", items[30]),
                    new XElement("TopeFinalCobranza3", items[31]),
                    new XElement("PorcentajeCobranza3", items[32]),
                    new XElement("TopeFinalCobranza4", items[33]),
                    new XElement("PorcentajeCobranza4", items[34]),
                    new XElement("TopeFinalCobranza5", items[35]),
                    new XElement("PorcentajeCobranza5", items[36]),
                    new XElement("UsaComisionPorVenta", items[37]),
                    new XElement("UsaComisionPorCobranza", items[38]),
                    new XElement("CodigoLote", items[39]),
                    new XElement("TipoDocumentoIdentificacion", items[40]),
                    new XElement("RutaDeComercializacion", items[41])));
            return xmlTree;
        }

        public bool VerifyIntegrityOfRecord(XElement valRecord, StringBuilder refErrorMessage) {
            bool vResult = false;
            XElement vXElement = DistributeLine(valRecord);
            XmlDocument vXDoc = new XmlDocument();
            vXDoc.Load(vXElement.CreateReader());
            if (vXDoc != null) {
                vResult = true;
            }
            //PROGRAMADOR IMPLEMENTE LA VALIDACION QUE APLIQUE EN EL NAVEGADOR Y DESCOMENTE ESTAS LINEAS
            //clsVendedorNav insVendedorNav = new clsVendedorNav();
            //if (insVendedorNav.ValidateAll(vXElement.CreateReader(), eAccionSR.Insertar, refErrorMessage)) {
            //if (vXDoc.DocumentElement.ChildNodes[0] != null) {
            //    refXmlDocResult.DocumentElement.AppendChild(refXmlDocResult.ImportNode(vXDoc.DocumentElement.ChildNodes[0], true));
            //}
            //vResult = true;
            //}
            return vResult;
        }
        public LibResponse Importar(string valPath, eExportDelimiterType valSeparador, BackgroundWorker valBWorker) {
            LibResponse vResultOperacion = new LibResponse();
            LibResponse vResult = new LibResponse();
            StringBuilder vErrorMessage = new StringBuilder();
            IVendedorDatPdn insVendedorDatPdn = new clsVendedorDat();
            IVendedorDatDetalleComisionesPdn insVendedorDatDetalleComisionesPdn = new clsVendedorDetalleComisionesDat();
            IVendedorPdn insVendedorPdn = new clsVendedorNav();
            int vIndex = 1;
            foreach (var item in vVendedorList) {
                try {
                    vResultOperacion = insVendedorDatPdn.InsertarListaDeVendedorMaster(new List<Ccl.Vendedor.Vendedor>() { item });
                    vResult.Success = vResult.Success && vResultOperacion.Success;
                    if (!vResultOperacion.Success) {
                        vResult.AddInformation(vResultOperacion.GetInformation());
                    }
                } catch {
                    vResult.AddError(vResultOperacion.GetInformation());
                }
                valBWorker.ReportProgress(1, string.Format("Insertando {0:n0} de {1:n0}", vIndex, vVendedorList.Count()));
                vIndex++;
            }
            CargaListaDetalleImportar(ref vVendedorDetalleComisionesList);
            foreach (var item1 in vVendedorDetalleComisionesList) {
                try {
                    vResultOperacion = insVendedorDatDetalleComisionesPdn.InsertarListaDeVendedorDetail(new List<VendedorDetalleComisiones>() { item1 });
                    vResult.Success = vResult.Success && vResultOperacion.Success;
                    if (!vResultOperacion.Success) {
                        vResult.AddInformation(vResultOperacion.GetInformation());
                    }
                } catch {
                    vResult.AddError(vResultOperacion.GetInformation());
                }
                valBWorker.ReportProgress(1, string.Format("Insertando {0:n0} de {1:n0}", vIndex, vVendedorList.Count()));
                vIndex++;
            }
            return vResult;
        }
        private XElement DistributeLine(XElement valRecord) {
            try {
                LibXmlDataParse vParser = new LibXmlDataParse(valRecord);
                XElement vXElement = new XElement("GpData",
                        new XElement("GpResult",
                        new XElement("ConsecutivoCompania", vParser.GetString(0, "ConsecutivoCompania", "")),
                        new XElement("Consecutivo", vParser.GetString(0, "Consecutivo", "")),
                        new XElement("Codigo", vParser.GetString(0, "Codigo", "")),
                        new XElement("Nombre", vParser.GetString(0, "Nombre", "")),
                        new XElement("RIF", vParser.GetString(0, "RIF", "")),
                        new XElement("StatusVendedor", vParser.GetString(0, "StatusVendedor", "")),
                        new XElement("Direccion", vParser.GetString(0, "Direccion", "")),
                        new XElement("Ciudad", vParser.GetString(0, "Ciudad", "")),
                        new XElement("ZonaPostal", vParser.GetString(0, "ZonaPostal", "")),
                        new XElement("Telefono", vParser.GetString(0, "Telefono", "")),
                        new XElement("Fax", vParser.GetString(0, "Fax", "")),
                        new XElement("Email", vParser.GetString(0, "Email", "")),
                        new XElement("Notas", vParser.GetString(0, "Notas", "")),
                        new XElement("ComisionPorVenta", vParser.GetString(0, "ComisionPorVenta", "")),
                        new XElement("ComisionPorCobro", vParser.GetString(0, "ComisionPorCobro", "")),
                        new XElement("TopeInicialVenta1", vParser.GetString(0, "TopeInicialVenta1", "")),
                        new XElement("TopeFinalVenta1", vParser.GetString(0, "TopeFinalVenta1", "")),
                        new XElement("PorcentajeVentas1", vParser.GetString(0, "PorcentajeVentas1", "")),
                        new XElement("TopeFinalVenta2", vParser.GetString(0, "TopeFinalVenta2", "")),
                        new XElement("PorcentajeVentas2", vParser.GetString(0, "PorcentajeVentas2", "")),
                        new XElement("TopeFinalVenta3", vParser.GetString(0, "TopeFinalVenta3", "")),
                        new XElement("PorcentajeVentas3", vParser.GetString(0, "PorcentajeVentas3", "")),
                        new XElement("TopeFinalVenta4", vParser.GetString(0, "TopeFinalVenta4", "")),
                        new XElement("PorcentajeVentas4", vParser.GetString(0, "PorcentajeVentas4", "")),
                        new XElement("TopeFinalVenta5", vParser.GetString(0, "TopeFinalVenta5", "")),
                        new XElement("PorcentajeVentas5", vParser.GetString(0, "PorcentajeVentas5", "")),
                        new XElement("TopeInicialCobranza1", vParser.GetString(0, "TopeInicialCobranza1", "")),
                        new XElement("TopeFinalCobranza1", vParser.GetString(0, "TopeFinalCobranza1", "")),
                        new XElement("PorcentajeCobranza1", vParser.GetString(0, "PorcentajeCobranza1", "")),
                        new XElement("TopeFinalCobranza2", vParser.GetString(0, "TopeFinalCobranza2", "")),
                        new XElement("PorcentajeCobranza2", vParser.GetString(0, "PorcentajeCobranza2", "")),
                        new XElement("TopeFinalCobranza3", vParser.GetString(0, "TopeFinalCobranza3", "")),
                        new XElement("PorcentajeCobranza3", vParser.GetString(0, "PorcentajeCobranza3", "")),
                        new XElement("TopeFinalCobranza4", vParser.GetString(0, "TopeFinalCobranza4", "")),
                        new XElement("PorcentajeCobranza4", vParser.GetString(0, "PorcentajeCobranza4", "")),
                        new XElement("TopeFinalCobranza5", vParser.GetString(0, "TopeFinalCobranza5", "")),
                        new XElement("PorcentajeCobranza5", vParser.GetString(0, "PorcentajeCobranza5", "")),
                        new XElement("UsaComisionPorVenta", vParser.GetString(0, "UsaComisionPorVenta", "")),
                        new XElement("UsaComisionPorCobranza", vParser.GetString(0, "UsaComisionPorCobranza", "")),
                        new XElement("CodigoLote", vParser.GetString(0, "CodigoLote", "")),
                        new XElement("TipoDocumentoIdentificacion", vParser.GetString(0, "TipoDocumentoIdentificacion", "")),
                        new XElement("RutaDeComercializacion", vParser.GetString(0, "RutaDeComercializacion", ""))));
                foreach (XElement vVendedor in vXElement.Nodes()) {

                }
                return vXElement;
            } catch (XmlException vEx) {
                throw new GalacException("Error distribuyendo la línea del archivo.", eExceptionManagementType.Uncontrolled, vEx);
            } catch (Exception) {
                throw;
            }
        }
        private void CargaListaDetalleImportar(ref IList<VendedorDetalleComisiones> valVendedorDetalleComisionesValido) {
            int vConsecutivo = 1;
            string vConsecutivoVendedor = LibConvert.ToStr(vVendedorDetalleComisionesList[0].ConsecutivoVendedor);
            foreach (var vItem in vVendedorDetalleComisionesList) {
                IVendedorPdn insVendedorPdn = new clsVendedorNav();
                VendedorDetalleComisiones vVendedorDetalleComisiones = new VendedorDetalleComisiones();
                vVendedorDetalleComisiones.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
                if (vConsecutivoVendedor == LibConvert.ToStr(vItem.ConsecutivoVendedor)) {
                    vVendedorDetalleComisiones.ConsecutivoRenglon = vConsecutivo;
                    vConsecutivo++;
                } else {
                    vConsecutivo = 1;
                    vVendedorDetalleComisiones.ConsecutivoRenglon = vConsecutivo;
                    vConsecutivoVendedor = LibConvert.ToStr(vItem.ConsecutivoVendedor);
                }
                vVendedorDetalleComisiones.NombreDeLineaDeProducto = vItem.NombreDeLineaDeProducto;
                vVendedorDetalleComisiones.TipoDeComision = vItem.TipoDeComisionAsDB;
                vVendedorDetalleComisiones.Monto = vItem.Monto;
                vVendedorDetalleComisiones.Porcentaje = vItem.Porcentaje;
                valVendedorDetalleComisionesValido.Add(vVendedorDetalleComisiones);
            }
        }

        #endregion //Miembros de ILibImpExp
        public void LlenarListaDeVendedores(XElement valVendedor) {
            Ccl.Vendedor.Vendedor vVendedor = new Ccl.Vendedor.Vendedor();
            vVendedor.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            //vVendedor.Consecutivo = ;
            vVendedor.Codigo = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Codigo"));
            vVendedor.Nombre = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Nombre"));
            vVendedor.RIF = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "RIF"));
            vVendedor.StatusVendedorAsEnum = (eStatusVendedor)LibConvert.ToInt(LibXml.GetElementValueOrEmpty(valVendedor, "StatusVendedor"));
            vVendedor.Direccion = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Direccion"));
            vVendedor.Ciudad = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Ciudad"));
            vVendedor.ZonaPostal = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "ZonaPostal"));
            vVendedor.Telefono = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Telefono"));
            vVendedor.Fax = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Fax"));
            vVendedor.Email = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Email"));
            vVendedor.Notas = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "Notas"));
            vVendedor.ComisionPorVenta = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "ComisionPorVenta"));
            vVendedor.ComisionPorCobro = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "ComisionPorCobro"));
            vVendedor.TopeInicialVenta1 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeInicialVenta1"));
            vVendedor.TopeFinalVenta1 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalVenta1"));
            vVendedor.PorcentajeVentas1 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeVentas1"));
            vVendedor.TopeFinalVenta2 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalVenta2"));
            vVendedor.PorcentajeVentas2 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeVentas2"));
            vVendedor.TopeFinalVenta3 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalVenta3"));
            vVendedor.PorcentajeVentas3 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeVentas3"));
            vVendedor.TopeFinalVenta4 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalVenta4"));
            vVendedor.PorcentajeVentas4 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeVentas4"));
            vVendedor.TopeFinalVenta5 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalVenta5"));
            vVendedor.PorcentajeVentas5 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeVentas5"));
            vVendedor.TopeInicialCobranza1 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeInicialCobranza1"));
            vVendedor.TopeFinalCobranza1 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalCobranza1"));
            vVendedor.PorcentajeCobranza1 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeCobranza1"));
            vVendedor.TopeFinalCobranza2 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalCobranza2"));
            vVendedor.PorcentajeCobranza2 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeCobranza2"));
            vVendedor.TopeFinalCobranza3 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalCobranza3"));
            vVendedor.PorcentajeCobranza3 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeCobranza3"));
            vVendedor.TopeFinalCobranza4 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalCobranza4"));
            vVendedor.PorcentajeCobranza4 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeCobranza4"));
            vVendedor.TopeFinalCobranza5 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "TopeFinalCobranza5"));
            vVendedor.PorcentajeCobranza5 = LibImportData.ToDec(LibXml.GetElementValueOrEmpty(valVendedor, "PorcentajeCobranza5"));
            vVendedor.UsaComisionPorVentaAsBool = LibConvert.ToBool(LibXml.GetElementValueOrEmpty(valVendedor, "UsaComisionPorVenta"));
            vVendedor.UsaComisionPorCobranzaAsBool = LibConvert.ToBool(LibXml.GetElementValueOrEmpty(valVendedor, "UsaComisionPorCobranza"));
            vVendedor.CodigoLote = LibString.UCase(LibXml.GetElementValueOrEmpty(valVendedor, "CodigoLote"));
            vVendedor.TipoDocumentoIdentificacionAsEnum = (eTipoDocumentoIdentificacion)LibConvert.ToInt(LibXml.GetElementValueOrEmpty(valVendedor, "TipoDocumentoIdentificacion"));
            vVendedor.RutaDeComercializacionAsEnum = (eRutaDeComercializacion)LibConvert.ToInt(LibXml.GetElementValueOrEmpty(valVendedor, "RutaDeComercializacion"));
        }
        public bool VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument refXmlDocResult, StringBuilder refErrorMessage) {
            throw new NotImplementedException();
        }
        XmlReader ILibImpExp.ImportFile(string valPathFile, string valSeparator) {
            throw new NotImplementedException();
        }
        #endregion //Metodos Generados

    } //End of class clsVendedorImpExp
} //End of namespace Galac.Adm.Brl.Vendedor
