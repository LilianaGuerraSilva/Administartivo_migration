using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Saw.Ccl.SttDef;
using System.IO;
using System.Xml.Linq;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.Brl.SttDef {
    public partial class clsSettDefinitionNav: LibBaseNav<IList<SettDefinition>, IList<SettDefinition>>, ILibPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsSettDefinitionNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<SettDefinition>, IList<SettDefinition>> GetDataInstance() {
            return new Galac.Saw.Dal.SttDef.clsSettDefinitionDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.SttDef.clsSettDefinitionDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.SttDef.clsSettDefinitionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Comun.Gp_SettDefinitionSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>> instanciaDal = new Galac.Saw.Dal.SttDef.clsSettDefinitionDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Comun.Gp_SettDefinitionGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Sett Definition":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: break;
            }
            return vResult;
        }
        #endregion //Metodos Generados

        public XElement GetModuleNames() {
            XElement vResult;
            QAdvSql insQAdvSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ");
            vSql.AppendLine("   LevelModule, ");
            vSql.AppendLine("   Module, ");
            vSql.AppendLine("   CAST(LevelModule AS nvarchar) + ' - ' + Module AS DisplayName ");
            vSql.AppendLine("FROM Comun.SettDefinition");
            vSql.AppendLine("GROUP BY ");
            vSql.AppendLine("   LevelModule, ");
            vSql.AppendLine("   Module, ");
            vSql.AppendLine("   CAST(LevelModule AS nvarchar) + ' - ' + Module ");
            ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>> instanciaDal = GetDataInstance();
            vResult = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), null);
            return vResult;
        }

        public bool ValidateAll(XmlReader valRecord, eAccionSR eAccionSR, StringBuilder refErrorMessage) {
           bool vResult = true;
           LibXmlDataParse vParser = new LibXmlDataParse(valRecord);
           vResult = IsValidName(eAccionSR, vParser.GetString(0, "Name", ""), refErrorMessage);
           vResult = IsValidModule(eAccionSR, vParser.GetString(0, "Module", ""), refErrorMessage);
           return vResult;
        }

        private bool IsValidName(eAccionSR valAction, string valName, StringBuilder refErrorMessage) {
           bool vResult = true;
           if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
              return true;
           }
           valName = LibString.Trim(valName);
           if(LibString.IsNullOrEmpty(valName, true)) {
              refErrorMessage.AppendLine(LibResText.InfoRequiredField("Name"));
              vResult = false;
           }
           return vResult;
        }

        private bool IsValidModule(eAccionSR valAction, string valName, StringBuilder refErrorMessage) {
           bool vResult = true;
           if((valAction == eAccionSR.Consultar) || (valAction == eAccionSR.Eliminar)) {
              return true;
           }
           valName = LibString.Trim(valName);
           if(LibString.IsNullOrEmpty(valName, true)) {
              refErrorMessage.AppendLine(LibResText.InfoRequiredField("Module"));
              vResult = false;
           }
           return vResult;
        }

        public int GetTotalParametrosAdministrativos() {
            int vResult = 0;
            StringBuilder vSql = new StringBuilder();
            LibGpParams vParametro = new LibGpParams();
            vSql.AppendLine("Select COUNT(*) AS Cantidad FROM Comun.Gv_SettDefinition_B1 WHERE Name NOT IN ('SolicitarIngresoDeTasaDeCambioAlEmitir')");
            ILibDataComponent<IList<SettDefinition>, IList<SettDefinition>> instanciaDal = new Galac.Saw.Dal.SttDef.clsSettDefinitionDat();
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), vParametro.Get());
            vResult = (from vRecord in vResulset.Descendants("GpResult")
                       select new {
                           Cantidad = LibConvert.ToInt(vRecord.Element("Cantidad"))
                       }).FirstOrDefault().Cantidad;
            return vResult;
        }

    }
        public class clsSettDefinitionImpEx : LibMRO, ILibImpExp {
            ILibDataImport _Db;
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
                              new XElement("Datatype", items[6]),
                              new XElement("Validationrules", items[7]),
                              new XElement("IsSetForAllEnterprise", items[8])));
                return xmlTree.CreateReader();
            }

           
 

            [PrincipalPermission(SecurityAction.Demand, Role = "Tablas.Insertar")]
            LibXmlResult ILibImpExp.AddRecord(XmlReader refRecord, LibProgressManager valManager, bool valShowMessage) {
                RegistraCliente();
                return _Db.Import(refRecord, valManager, valShowMessage);
            }

            bool ILibImpExp.VerifyIntegrityOfRecord(XmlReader valRecord, XmlDocument valXmlDocResult, StringBuilder refErrorMessage) {
                bool vResult = false;
                XElement vXElement = DistributeLine(valRecord);
                XmlDocument vXDoc = new XmlDocument();
                vXDoc.Load(vXElement.CreateReader());
                if (vXDoc.DocumentElement.ChildNodes[0] != null) {
                    valXmlDocResult.DocumentElement.AppendChild(valXmlDocResult.ImportNode(vXDoc.DocumentElement.ChildNodes[0], true));
                }
                vResult = true;
                return vResult;
            }

            private XElement DistributeLine(XmlReader valRecord) {
             string vName="";
             string vModule = "";
             int vLevelModule = 0;
             string vGroupName = "";
             int vlevelGroup = 0;
             string vlabel = "";
             string vDatatype = "";
             string vValidationrules = "";
             bool vIsSetForAllEnterprise = false; 
               
                try {
                    LibXmlDataParse vParser = new LibXmlDataParse(valRecord);
                    vName = vParser.GetString(0, "Name", "");
                    vModule = vParser.GetString(0, "Module", "");
                    vLevelModule = vParser.GetInt(0, "LevelModule", 0);
                    vGroupName = vParser.GetString(0, "GroupName", "");
                    vlevelGroup = vParser.GetInt(0, "LevelGroup", 0);
                    vlabel = vParser.GetString(0, "Label", "");
                    vDatatype = vParser.GetString(0, "Datatype", "");
                    vValidationrules = vParser.GetString(0, "Validationrules", "");
                    vIsSetForAllEnterprise = vParser.GetBool(0, "IsSetForAllEnterprise", false); 
                    XElement vXElement = new XElement("GpData",
                            new XElement("GpResult",
                            new XElement("Name", vName),
                            new XElement("Module", vModule),
                            new XElement("LevelModule", vLevelModule),
                            new XElement("GroupName", vGroupName),
                            new XElement("LevelGroup", vlevelGroup),
                            new XElement("Label", vlabel),
                            new XElement("Datatype", vDatatype),
                            new XElement("Validationrules", vValidationrules),
                           new XElement("IsSetForAllEnterprise", LibConvert.BoolToSN(vIsSetForAllEnterprise))));


                    
                    return vXElement;
                } catch (XmlException vEx) {
                    throw new GalacException("Error distribuyendo la línea del archivo." + "\n(Configuracion=" + vName + ")", eExceptionManagementType.Uncontrolled, vEx);
                } catch (Exception) {
                    throw;
                }
            }
            #endregion
        }//   
     //End of class clsSettDefinitionNav

} //End of namespace Galac.Saw.Brl.SttDef

