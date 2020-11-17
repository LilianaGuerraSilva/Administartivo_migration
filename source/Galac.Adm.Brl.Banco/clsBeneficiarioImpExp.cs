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
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Brl.Banco {

    public class clsBeneficiarioImpExp : LibMRO, ILibImpExp, IlibImpExpBulkInsert {
        #region Variables
        ILibDataImport _Db;
         eActionImpExp IlibImpExpBulkInsert.Action { get; set; }
        #endregion //Variables
        #region Constructores

        public clsBeneficiarioImpExp() {
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Db = (ILibDataImport)RegisterType();
            } else {
                _Db = new Galac.Adm.Dal.Banco.clsBeneficiarioDat();
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
                    new XElement("ConsecutivoCompania", items[0]),
                    new XElement("Consecutivo", items[1]),
                    new XElement("Codigo", items[2]),
                    new XElement("NumeroRIF", items[3]),
                    new XElement("NombreBeneficiario", items[4]),
                    new XElement("Origen", items[5]),
                    new XElement("TipoDeBeneficiario", items[6])));
            return xmlTree.CreateReader();
        }

        bool ILibImpExp.VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument refXmlDocResult, StringBuilder refErrorMessage) {
            bool vResult = false;
            XElement vXElement = DistributeLine(valRecord);
            XmlDocument vXDoc = new XmlDocument();
            vXDoc.Load(vXElement.CreateReader());
            //PROGRAMADOR IMPLEMENTE LA VALIDACION QUE APLIQUE EN EL NAVEGADOR Y DESCOMENTE ESTAS LINEAS
            //clsBeneficiarioNav insBeneficiarioNav = new clsBeneficiarioNav();
            //if (insBeneficiarioNav.ValidateAll(vXElement.CreateReader(), eAccionSR.Insertar, refErrorMessage)) {
                if (vXDoc.DocumentElement.ChildNodes[0] != null) {
                    refXmlDocResult.DocumentElement.AppendChild(refXmlDocResult.ImportNode(vXDoc.DocumentElement.ChildNodes[0], true));
                }
                vResult = true;
            //}
            return vResult;
        }

        private XElement DistributeLine(XmlReader valRecord) {
            //throw new NotImplementedException("PROGRAMADOR: El codigo generado bajo el atributo IMPEXP del record, es solo referencial. DEBE AJUSTARLO ya que el Narrador actualmente desconece la estructura de su archivo de importacion!!!!");
            try {
                LibXmlDataParse vParser = new LibXmlDataParse(valRecord);
                XElement vXElement = new XElement("GpData",
                        new XElement("GpResult",
                        new XElement("ConsecutivoCompania", vParser.GetString(0, "ConsecutivoCompania", "")),
                        new XElement("Consecutivo", vParser.GetString(0, "Consecutivo", "")),
                        new XElement("Codigo", vParser.GetString(0, "Codigo", "")),
                        new XElement("NumeroRIF", vParser.GetString(0, "NumeroRIF", "")),
                        new XElement("NombreBeneficiario", vParser.GetString(0, "NombreBeneficiario", "")),
                        new XElement("Origen", vParser.GetString(0, "Origen", "")),
                        new XElement("TipoDeBeneficiario", vParser.GetString(0, "TipoDeBeneficiario", ""))));
                return vXElement;
            } catch (XmlException vEx) {
                throw new GalacException("Error distribuyendo la línea del archivo." , eExceptionManagementType.Uncontrolled, vEx);
            } catch (Exception) {
                throw;
            }
        }
        #endregion //Miembros de ILibImpExp
        #endregion //Metodos Generados


    } //End of class clsBeneficiarioImpExp

} //End of namespace Galac.Adm.Brl.Banco

