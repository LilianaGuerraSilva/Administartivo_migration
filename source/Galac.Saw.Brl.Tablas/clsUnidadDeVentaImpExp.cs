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
using Galac.Saw.Ccl.Tablas;
using LibGalac.Aos.DefGen;

namespace Galac.Saw.Brl.Tablas {

    public class clsUnidadDeVentaImpExp: LibMRO, ILibImpExp,libImpExpBulkInsert {
        #region Variables
        ILibDataImport _Db;
        eActionImpExp IlibImpExpBulkInsert.Action { get; set; }
        #endregion //Variables
        #region Constructores

        public clsUnidadDeVentaImpExp() {
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Db = (ILibDataImport)RegisterType();
            } else {
                _Db = new Galac.Saw.Dal.Tablas.clsUnidadDeVentaDat();
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

        XmlReader ILibImpExp.ImportFile(string valPathFile, string valSeparator) {
            StreamReader vStreamReader = new StreamReader(valPathFile, System.Text.Encoding.Default);
            XElement xmlTree;
            if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                xmlTree = new XElement("GpData",
                      from line in vStreamReader.Lines()
                      let items = LibString.Split(line, valSeparator, false)
                      select new XElement("GpResult",
                        new XElement("Nombre", items[0]),
                        new XElement("Codigo", items[1])));
            } else {
                xmlTree = new XElement("GpData",
                      from line in vStreamReader.Lines()
                      let items = LibString.Split(line, valSeparator, false)
                      select new XElement("GpResult",
                        new XElement("Nombre", items[0])));
            }

            return xmlTree.CreateReader();
        }

        bool ILibImpExp.VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument refXmlDocResult, StringBuilder refErrorMessage) {
            bool vResult = false;
            XElement vXElement = DistributeLine(valRecord);
            XmlDocument vXDoc = new XmlDocument();
            vXDoc.Load(vXElement.CreateReader());
            clsUnidadDeVentaNav insUnidadDeVentaNav = new clsUnidadDeVentaNav();
            if (insUnidadDeVentaNav.ValidateAll(vXElement.CreateReader(), eAccionSR.Insertar, refErrorMessage)) {
                if (vXDoc.DocumentElement.ChildNodes[0] != null) {
                    refXmlDocResult.DocumentElement.AppendChild(refXmlDocResult.ImportNode(vXDoc.DocumentElement.ChildNodes[0], true));
                }
                vResult = true;
            }
            return vResult;
        }

        private XElement DistributeLine(XmlReader valRecord) {
            try {
                LibXmlDataParse vParser = new LibXmlDataParse(valRecord);
                XElement vXElement = new XElement("GpData",
                        new XElement("GpResult",
                        new XElement("Nombre", vParser.GetString(0, "Nombre", "")),
                        new XElement("Codigo", vParser.GetString(0, "Codigo", ""))));
                return vXElement;
            } catch (XmlException vEx) {
                throw new GalacException("Error distribuyendo la línea del archivo." , eExceptionManagementType.Uncontrolled, vEx);
            } catch (Exception) {
                throw;
            }
        }
        #endregion //Miembros de ILibImpExp
        #endregion //Metodos Generados


    } //End of class clsUnidadDeVentaImpExp

} //End of namespace Galac.Saw.Brl.Tablas

