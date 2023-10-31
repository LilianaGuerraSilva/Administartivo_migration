using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Saw.Ccl.Integracion;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using LibGalac.Aos.Catching;
using Galac.Saw.Dal.Integracion;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.DefGen;


namespace Galac.Saw.Brl.Integracion {
    public partial class clsIntegracionSawNav : LibBaseNav<IList<IntegracionSaw>, IList<IntegracionSaw>>, ILibPdn, IIntegracionSawPdn {
        #region Variables
        IIntegracionSawDat _DbIntegracion;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsIntegracionSawNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>> GetDataInstance() {
            return new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            StringBuilder vXmlParamsModificado = valXmlParamsExpression.Replace("<param nombre=\"@UseTopClausule\" valor=\"S\" tipo=\"System.String\" tamano=\"1\" direccionParametro=\"entrada\" />", "");
            if ("Integracion Saw" == valCallingModule) {
                return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_IntegracionSawSCH", vXmlParamsModificado);
            } else {
                return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "dbo.Gp_CompaniaFND", vXmlParamsModificado);
            }
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>> instanciaDal = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_IntegracionSawGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn


        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Integracion Saw":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                case "Compania":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;

                default: break;
            }
            return vResult;
        }
        #endregion //Metodos Generados



        #region Inicializacion DAL - a modificar si Remoting

        #endregion //Inicializacion DAL - a modificar si Remoting

        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _DbIntegracion = (IIntegracionSawDat)RegisterType();
            } else {
                _DbIntegracion = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            }
        }

        bool IIntegracionSawPdn.ActualizaVersion() {
            RegistraCliente();
            IList<IntegracionSaw> vListIntegracionSaw = new List<IntegracionSaw>();
            vListIntegracionSaw.Add(GetValoIntegracionSaw(listValoresIntegracionSaw()));
            return _DbIntegracion.ActualizaVersion(vListIntegracionSaw);

        }

        bool IIntegracionSawPdn.ConectarCompanias(string valCondigoCompania, string valCodigoConexion) {
            RegistraCliente();
            return _DbIntegracion.ConectarCompanias(valCondigoCompania, valCodigoConexion);
        }

        bool IIntegracionSawPdn.DesConectarCompanias(string valCodigoConexion) {
            RegistraCliente();
            return _DbIntegracion.DesConectarCompanias(valCodigoConexion);
        }

        bool IIntegracionSawPdn.InsertaValorPorDefecto() {
            RegistraCliente();
            IList<IntegracionSaw> vBusinessObject = new List<IntegracionSaw>();
            IntegracionSaw insIntegracion = new IntegracionSaw();
            insIntegracion.TipoIntegracionAsEnum = eTipoIntegracion.NOMINA;
            insIntegracion.version = Version();
            vBusinessObject.Add(insIntegracion);
            ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>> instanciaDal = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            return  instanciaDal.Insert(vBusinessObject).Success;
             
        }

        

        bool IIntegracionSawPdn.VersionesCompatibles() {
            RegistraCliente();
            return _DbIntegracion.VersionesCompatibles(Version());
        }


        private string Version() {
            string vResult = "";
            clsDespro insDespro = new clsDespro();
            if (LibDefGen.ProgramInfo.IsCountryVenezuela())
                vResult = insDespro.Version();
            else {
                vResult = "2.60.2.0";
            }
            return vResult;
        }
        private StringBuilder ParametrosActualizacionVersion() {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddReturn();
            vParams.AddInEnum("TipoIntegracion", LibConvert.EnumToDbValue((int)eTipoIntegracion.NOMINA));
            vParams.AddInString("version", Version(), 8);
            vParams.AddInString("NombreOperador", ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login, 10);
            vParams.AddInDateTime("FechaUltimaModificacion", LibDate.Today());
            vResult = vParams.Get();
            return vResult;
        }

        private StringBuilder GetPkParams() {
            LibGpParams vParams = new LibGpParams();
            return vParams.Get();
        }

        private XmlDocument listValoresIntegracionSaw() {
            XmlDocument vXmlDocument = new XmlDocument();
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            if (instanciaDal.ConnectFk(ref vXmlDocument, eProcessMessageType.SpName, "Saw.Gp_IntegracionSawSCH", GetPkParams())) {
                return vXmlDocument;
            } else {
                return null;
            }
        }
        private IntegracionSaw GetValoIntegracionSaw(XmlDocument valRecord) {
            IntegracionSaw vResult = new IntegracionSaw();
            XDocument xDoc = XDocument.Load(new XmlNodeReader(valRecord));
            var vEntidad = from Data in xDoc.Descendants("GpResult")
                           select Data;
            foreach (XElement vItem in vEntidad) {
                if (LibConvert.DbValueToEnum(vItem.Element("TipoIntegracion")) == (int)eTipoIntegracion.NOMINA) {
                    vResult.fldTimeStamp = LibConvert.ToLong(vItem.Element("fldTimeStampBigint"));
                    vResult.TipoIntegracionAsEnum = eTipoIntegracion.NOMINA;
                    vResult.versionOriginal = vItem.Element("version").Value;
                    vResult.version = Version();
                    break;
                }
            }
            return vResult;
        }



        int IIntegracionSawPdn.GetConsecutivoCompaniaIntegrada(string valCodigoConexion) {
            int vResult = 0;
            StringBuilder vSql = new StringBuilder();
            RegistraCliente();
            vSql.Append("SELECT ConsecutivoCompania FROM COMPANIA WHERE CodigoDeIntegracion = " + new QAdvSql("").ToSqlValue(valCodigoConexion));
            ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>> instanciaDal = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString (),null);
            vResult = LibConvert.ToInt(LibXml.GetPropertyString(vResulset, "ConsecutivoCompania"));
            return vResult;
        }


        string IIntegracionSawPdn.GetCuentaBancariaGenerica(int valConcecutivoCompania) {
            string vResult = "";
            StringBuilder vSql = new StringBuilder();
            RegistraCliente();
            bool Existe = ExisteTablaSettValueByCompany();
            if (Existe) {
                vSql.Append(FindCuentaBancariaGenerica(valConcecutivoCompania));
            } else {
                vSql.Append(FindCodigoEntidad(valConcecutivoCompania, "parametrosCompania", "CodigoGenericoCuentaBancaria"));
            }
            ILibDataComponent<IList<IntegracionSaw>, IList<IntegracionSaw>> instanciaDal = new Galac.Saw.Dal.Integracion.clsIntegracionSawDat();
            XElement vResulset = instanciaDal.QueryInfo(eProcessMessageType.Query, vSql.ToString(), null);
            if (Existe) {
                vResult = LibConvert.ToStr(LibXml.GetPropertyString(vResulset, "value"));
            } else {
                vResult = LibConvert.ToStr(LibXml.GetPropertyString(vResulset, "CodigoGenericoCuentaBancaria"));
            }
            return vResult;
        }


        private string FindCuentaBancariaGenerica(int valUniqueKeyCMFValue) {
            StringBuilder vSqlSb = new StringBuilder();
            string vResult = "";
            string vSqlWhere = "";
            string vDbSchema = "";
            if(LibDefGen.ProgramInfo.IsCountryEcuador()) {
                vDbSchema = "Adme";
            } else {
                vDbSchema = "Comun";
            }
            QAdvSql insQAdvSql = new QAdvSql("");
            vSqlWhere = insQAdvSql.SqlIntValueWithAnd(vSqlWhere,"" + vDbSchema + ".SettValueByCompany.ConsecutivoCompania",valUniqueKeyCMFValue);
            vSqlWhere = insQAdvSql.SqlValueWithAnd(vSqlWhere,"" + vDbSchema + ".SettValueByCompany.NameSettDefinition","CodigoGenericoCuentaBancaria");
            vSqlWhere = insQAdvSql.WhereSql(vSqlWhere);
            vSqlSb.AppendLine(" SELECT value FROM " + vDbSchema + ".SettValueByCompany");
            vSqlSb.AppendLine(vSqlWhere);
            vResult = vSqlSb.ToString();
            return vResult;
        }

        private string FindCodigoEntidad(int valUniqueKeyCMFValue, string valNameTable, string valNameColumn) {
            StringBuilder vSqlSb = new StringBuilder();
            string vResult = "";
            string vSqlWhere = "";
            QAdvSql insQAdvSql = new QAdvSql("");
            vSqlWhere = insQAdvSql.SqlIntValueWithAnd(vSqlWhere, valNameTable + ".ConsecutivoCompania", valUniqueKeyCMFValue);
            vSqlWhere = insQAdvSql.WhereSql(vSqlWhere);
            vSqlSb.AppendLine(" SELECT " + valNameColumn + " FROM " + valNameTable);
            vSqlSb.AppendLine(vSqlWhere);
            vResult = vSqlSb.ToString();
            return vResult;
        }

        private bool ExisteTablaSettValueByCompany() {
            return ((Galac.Saw.Dal.Integracion.clsIntegracionSawDat)_DbIntegracion).ExisteTablaSettValueByCompany();
        }
        
        int IIntegracionSawPdn.GetConsecutivoBeneficiarioGenerico(int valConcecutivoCompania) {
            int vResult = 0;
            return vResult;
        }
    } //End of class clsIntegracionSawNav

}//End of namespace Galac.Saw.Brl.Integracion

