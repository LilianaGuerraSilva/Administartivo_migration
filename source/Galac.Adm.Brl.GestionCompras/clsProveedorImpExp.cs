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

namespace Galac.Adm.Brl.GestionCompras {

    public class clsProveedorImpExp: LibMRO, ILibImpExp {
        #region Variables
        ILibDataImport _Db;
        #endregion //Variables
        #region Constructores

        public clsProveedorImpExp() {
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Db = (ILibDataImport)RegisterType();
            } else {
                _Db = new Galac.Adm.Dal.GestionCompras.clsProveedorDat();
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

        XmlReader ILibImpExp.ImportFile(string valPathFile, string valSeparator) {
            StreamReader vStreamReader = new StreamReader(valPathFile, System.Text.Encoding.Default);
            XElement xmlTree = new XElement("GpData",
                  from line in vStreamReader.Lines()
                  let items = LibString.Split(line, valSeparator, false)
                  select new XElement("GpResult",
                    new XElement("ConsecutivoCompania", items[0]),
                    new XElement("CodigoProveedor", items[1]),
                    new XElement("Consecutivo", items[2]),
                    new XElement("NombreProveedor", items[3]),
                    new XElement("Contacto", items[4]),
                    new XElement("NumeroRIF", items[5]),
                    new XElement("NumeroNIT", items[6]),
                    new XElement("TipoDePersona", items[7]),
                    new XElement("CodigoRetencionUsual", items[8]),
                    new XElement("Telefonos", items[9]),
                    new XElement("Direccion", items[10]),
                    new XElement("Fax", items[11]),
                    new XElement("Email", items[12]),
                    new XElement("TipodeProveedor", items[13]),
                    new XElement("TipoDeProveedorDeLibrosFiscales", items[14]),
                    new XElement("PorcentajeRetencionIVA", items[15]),
                    new XElement("CuentaContableCxP", items[16]),
                    new XElement("CuentaContableGastos", items[17]),
                    new XElement("CuentaContableAnticipo", items[18]),
                    new XElement("CodigoLote", items[19]),
                    new XElement("Beneficiario", items[20]),
                    new XElement("UsarBeneficiarioImpCheq", items[21]),
                    new XElement("TipoDocumentoIdentificacion", items[22]),
                    new XElement("EsAgenteDeRetencionIva", items[23]),
                    new XElement("Nombre", items[24]),
                    new XElement("ApellidoPaterno", items[25]),
                    new XElement("ApellidoMaterno", items[26]),
                    new XElement("NumeroCuentaBancaria", items[27]),
                    new XElement("CodigoContribuyente", items[28]),
                    new XElement("NumeroRUC", items[29]),
                    new XElement("TipoDePersonaLibrosElectronicos", items[30]),
                    new XElement("CodigoPaisResidencia", items[31])));
            return xmlTree.CreateReader();
        }

        bool ILibImpExp.VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument refXmlDocResult, StringBuilder refErrorMessage) {
            bool vResult = false;
            XElement vXElement = DistributeLine(valRecord);
            XmlDocument vXDoc = new XmlDocument();
            vXDoc.Load(vXElement.CreateReader());
            //PROGRAMADOR IMPLEMENTE LA VALIDACION QUE APLIQUE EN EL NAVEGADOR Y DESCOMENTE ESTAS LINEAS
            //clsProveedorNav insProveedorNav = new clsProveedorNav();
            //if (insProveedorNav.ValidateAll(vXElement.CreateReader(), eAccionSR.Insertar, refErrorMessage)) {
                if (vXDoc.DocumentElement.ChildNodes[0] != null) {
                    refXmlDocResult.DocumentElement.AppendChild(refXmlDocResult.ImportNode(vXDoc.DocumentElement.ChildNodes[0], true));
                }
                vResult = true;
            //}
            return vResult;
        }

        private XElement DistributeLine(XmlReader valRecord) {
            throw new NotImplementedException("PROGRAMADOR: El codigo generado bajo el atributo IMPEXP del record, es solo referencial. DEBE AJUSTARLO ya que el Narrador actualmente desconece la estructura de su archivo de importacion!!!!");
            try {
                LibXmlDataParse vParser = new LibXmlDataParse(valRecord);
                XElement vXElement = new XElement("GpData",
                        new XElement("GpResult",
                        new XElement("ConsecutivoCompania", vParser.GetString(0, "ConsecutivoCompania", "")),
                        new XElement("CodigoProveedor", vParser.GetString(0, "CodigoProveedor", "")),
                        new XElement("Consecutivo", vParser.GetString(0, "Consecutivo", "")),
                        new XElement("NombreProveedor", vParser.GetString(0, "NombreProveedor", "")),
                        new XElement("Contacto", vParser.GetString(0, "Contacto", "")),
                        new XElement("NumeroRIF", vParser.GetString(0, "NumeroRIF", "")),
                        new XElement("NumeroNIT", vParser.GetString(0, "NumeroNIT", "")),
                        new XElement("TipoDePersona", vParser.GetString(0, "TipoDePersona", "")),
                        new XElement("CodigoRetencionUsual", vParser.GetString(0, "CodigoRetencionUsual", "")),
                        new XElement("Telefonos", vParser.GetString(0, "Telefonos", "")),
                        new XElement("Direccion", vParser.GetString(0, "Direccion", "")),
                        new XElement("Fax", vParser.GetString(0, "Fax", "")),
                        new XElement("Email", vParser.GetString(0, "Email", "")),
                        new XElement("TipodeProveedor", vParser.GetString(0, "TipodeProveedor", "")),
                        new XElement("TipoDeProveedorDeLibrosFiscales", vParser.GetString(0, "TipoDeProveedorDeLibrosFiscales", "")),
                        new XElement("PorcentajeRetencionIVA", vParser.GetString(0, "PorcentajeRetencionIVA", "")),
                        new XElement("CuentaContableCxP", vParser.GetString(0, "CuentaContableCxP", "")),
                        new XElement("CuentaContableGastos", vParser.GetString(0, "CuentaContableGastos", "")),
                        new XElement("CuentaContableAnticipo", vParser.GetString(0, "CuentaContableAnticipo", "")),
                        new XElement("CodigoLote", vParser.GetString(0, "CodigoLote", "")),
                        new XElement("Beneficiario", vParser.GetString(0, "Beneficiario", "")),
                        new XElement("UsarBeneficiarioImpCheq", vParser.GetString(0, "UsarBeneficiarioImpCheq", "")),
                        new XElement("TipoDocumentoIdentificacion", vParser.GetString(0, "TipoDocumentoIdentificacion", "")),
                        new XElement("EsAgenteDeRetencionIva", vParser.GetString(0, "EsAgenteDeRetencionIva", "")),
                        new XElement("Nombre", vParser.GetString(0, "Nombre", "")),
                        new XElement("ApellidoPaterno", vParser.GetString(0, "ApellidoPaterno", "")),
                        new XElement("ApellidoMaterno", vParser.GetString(0, "ApellidoMaterno", "")),
                        new XElement("NumeroCuentaBancaria", vParser.GetString(0, "NumeroCuentaBancaria", "")),
                        new XElement("CodigoContribuyente", vParser.GetString(0, "CodigoContribuyente", "")),
                        new XElement("NumeroRUC", vParser.GetString(0, "NumeroRUC", "")),
                        new XElement("TipoDePersonaLibrosElectronicos", vParser.GetString(0, "TipoDePersonaLibrosElectronicos", "")),
                        new XElement("CodigoPaisResidencia", vParser.GetString(0, "CodigoPaisResidencia", ""))));                        
                return vXElement;
            } catch (XmlException vEx) {
                throw new GalacException("Error distribuyendo la línea del archivo." , eExceptionManagementType.Uncontrolled, vEx);
            } catch (Exception) {
                throw;
            }
        }
        #endregion //Miembros de ILibImpExp
        #endregion //Metodos Generados


    } //End of class clsProveedorImpExp

} //End of namespace Galac.Adm.Brl.GestionCompras

