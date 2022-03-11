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

namespace Galac.Saw.Brl.Tablas {

    public class clsImpuestoBancarioImpExp: LibMRO, ILibImpExp ,IlibImpExpBulkInsert {
        #region Variables
        ILibDataImport _Db;
        eActionImpExp IlibImpExpBulkInsert.Action { get; set; }
        #endregion //Variables
        #region Constructores

        public clsImpuestoBancarioImpExp() {
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Db = (ILibDataImport)RegisterType();
            } else {
                _Db = new Galac.Saw.Dal.Tablas.clsImpuestoBancarioDat();
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
            XElement xmlTree = new XElement("GpData",
                  from line in vStreamReader.Lines()
                  let items = LibString.Split(line, valSeparator, false)
                  select new XElement("GpResult",
                    new XElement("FechaDeInicioDeVigencia", items[0]),
                    new XElement("AlicuotaAlDebito", items[1]),
                    new XElement("AlicuotaAlCredito", items[2]),
                    new XElement("AlicuotaC1Al4", items[3]),
                    new XElement("AlicuotaC5", items[4]),
                    new XElement("AlicuotaC6", items[5])));
            return xmlTree.CreateReader();
        }

        bool ILibImpExp.VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument refXmlDocResult, StringBuilder refErrorMessage) {
            bool vResult = false;
            XElement vXElement = DistributeLine(valRecord);
            XmlDocument vXDoc = new XmlDocument();
            vXDoc.Load(vXElement.CreateReader());            
            clsImpuestoBancarioNav insImpuestoBancarioNav = new clsImpuestoBancarioNav();
            if (insImpuestoBancarioNav.ValidateAll(vXElement, eAccionSR.Insertar, refErrorMessage)) {
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
                        new XElement("FechaDeInicioDeVigencia", vParser.GetString(0, "FechaDeInicioDeVigencia", "")),
                        new XElement("AlicuotaAlDebito", vParser.GetString(0, "AlicuotaAlDebito", "")),
                        new XElement("AlicuotaAlCredito", vParser.GetString(0, "AlicuotaAlCredito", "")),
                        new XElement("AlicuotaC1Al4", vParser.GetString(0, "AlicuotaC1Al4", "")),
                        new XElement("AlicuotaC5", vParser.GetString(0, "AlicuotaC5", "")),
                        new XElement("AlicuotaC6", vParser.GetString(0, "AlicuotaC6", ""))));
                return vXElement;
            } catch (XmlException vEx) {
                throw new GalacException("Error distribuyendo la línea del archivo." , eExceptionManagementType.Uncontrolled, vEx);
            } catch (Exception) {
                throw;
            }
        }
        #endregion //Miembros de ILibImpExp
        #endregion //Metodos Generados


    } //End of class clsImpuestoBancarioImpExp

} //End of namespace Galac.Saw.Brl.Tablas

