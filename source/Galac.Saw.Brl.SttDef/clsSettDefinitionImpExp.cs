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
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Brl.SttDef {

    public class clsSettDefinitionImpExp: LibMRO, ILibImpExp {
        #region Variables
        ILibDataImport _Db;
        #endregion //Variables
        #region Constructores

        public clsSettDefinitionImpExp() {
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Db = (ILibDataImport)RegisterType();
            } else {
                _Db = new Galac.Saw.Dal.SttDef.clsSettDefinitionDat();
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
                    new XElement("Name", items[0]),
                    new XElement("Module", items[1]),
                    new XElement("LevelModule", items[2]),
                    new XElement("GroupName", items[3]),
                    new XElement("LevelGroup", items[4]),
                    new XElement("Label", items[5]),
                    new XElement("DataType", items[6]),
                    new XElement("Validationrules", items[7]),
                    new XElement("IsSetForAllEnterprise", items[8])));
            return xmlTree.CreateReader();
        }

        bool ILibImpExp.VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument refXmlDocResult, StringBuilder refErrorMessage) {
            bool vResult = false;
            XElement vXElement = DistributeLine(valRecord);
            XmlDocument vXDoc = new XmlDocument();
            vXDoc.Load(vXElement.CreateReader());

            clsSettDefinitionNav insSettDefinitionNav = new clsSettDefinitionNav();
            if (insSettDefinitionNav.ValidateAll(vXElement.CreateReader(), eAccionSR.Insertar, refErrorMessage)) {
               if (vXDoc.DocumentElement.ChildNodes[0] != null) {
                  refXmlDocResult.DocumentElement.AppendChild(refXmlDocResult.ImportNode(vXDoc.DocumentElement.ChildNodes[0], true));
               }
               vResult = true;
            }
            return vResult;
        }

        private XElement DistributeLine(XmlReader valRecord) {
            //throw new NotImplementedException("PROGRAMADOR: El codigo generado bajo el atributo IMPEXP del record, es solo referencial. DEBE AJUSTARLO ya que el Narrador actualmente desconece la estructura de su archivo de importacion!!!!");
            try {
                LibXmlDataParse vParser = new LibXmlDataParse(valRecord);
                XElement vXElement = new XElement("GpData",
                        new XElement("GpResult",
                        new XElement("Name", vParser.GetString(0, "Name", "")),
                        new XElement("Module", vParser.GetString(0, "Module", "")),
                        new XElement("LevelModule", vParser.GetString(0, "LevelModule", "")),
                        new XElement("GroupName", vParser.GetString(0, "GroupName", "")),
                        new XElement("LevelGroup", vParser.GetString(0, "LevelGroup", "")),
                        new XElement("Label", vParser.GetString(0, "Label", "")),
                        new XElement("DataType", vParser.GetString(0, "DataType", "")),
                        new XElement("Validationrules", vParser.GetString(0, "Validationrules", "")),
                        new XElement("IsSetForAllEnterprise", vParser.GetString(0, "IsSetForAllEnterprise", ""))));
                return vXElement;
            } catch (XmlException vEx) {
                throw new GalacException("Error distribuyendo la línea del archivo." , eExceptionManagementType.Uncontrolled, vEx);
            } catch (Exception) {
                throw;
            }
        }
        #endregion //Miembros de ILibImpExp
        #endregion //Metodos Generados


    } //End of class clsSettDefinitionImpExp

} //End of namespace Galac.Saw.Brl.SttDef

