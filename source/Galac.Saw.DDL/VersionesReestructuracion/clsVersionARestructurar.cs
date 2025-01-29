using System;
using System.Collections.Generic;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Xml.Linq;
using System.Xml;


namespace Galac.Saw.DDL.VersionesReestructuracion {
    abstract class clsVersionARestructurar : QAdvReest {
        public string _TodayAsSqlValue { get; set; }
        public string _CurrentDataBaseName { get; set; }
        public string _VersionDataBase { get; set; }

        public abstract bool UpdateToVersion();

        public clsVersionARestructurar(string valCurrentDataBaseName) {
            _TodayAsSqlValue = InsSql.ToSqlValue(LibDate.Today());
            _CurrentDataBaseName = valCurrentDataBaseName;
        }

        public bool AgregarNuevoParametro(string valNombre, string valModulo, int valNivelModulo, string valNombreDelGrupo, int valNivelDelGrupo, string valEtiqueta, char valTipoDeDato, string valReglasValidacion, char valEsParaTodasLasEmpresas, string valValorPorDefecto) {
            bool vResult = true;
            IList<int> vLstCompanias;
            try {
                AgregarDefinicionDeParametro(valNombre, valModulo, valNivelModulo, valNombreDelGrupo, valNivelDelGrupo, valEtiqueta, valTipoDeDato, valReglasValidacion, valEsParaTodasLasEmpresas);
                vLstCompanias = ObtenerTodosLosConsecutivosDeLasCompanias();
                if (vLstCompanias != null) {
                    foreach (int vCompania in vLstCompanias) {
                        AgregarValorParametroPorCompania(vCompania, valNombre, valValorPorDefecto);
                    }
                }
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        public bool AgregarNuevoParametro(string valNombre, string valModulo, int valNivelModulo, string valNombreDelGrupo, int valNivelDelGrupo, string valEtiqueta, eTipoDeDatoParametros valTipoDeDato, string valReglasValidacion, char valEsParaTodasLasEmpresas, string valValorPorDefecto) {
            bool vResult = true;
            IList<int> vLstCompanias;
            try {
                AgregarDefinicionDeParametro(valNombre, valModulo, valNivelModulo, valNombreDelGrupo, valNivelDelGrupo, valEtiqueta, LibConvert.ToChar(LibConvert.EnumToDbValue((int)valTipoDeDato)), valReglasValidacion, valEsParaTodasLasEmpresas);
                vLstCompanias = ObtenerTodosLosConsecutivosDeLasCompanias();
                if (vLstCompanias != null) {
                    foreach (int vCompania in vLstCompanias) {
                        AgregarValorParametroPorCompania(vCompania, valNombre, valValorPorDefecto);
                    }
                }
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        private bool AgregarDefinicionDeParametro(string valNombre, string valModulo, int valNivelModulo, string valNombreDelGrupo, int valNivelDelGrupo, string valEtiqueta, char valTipoDeDato, string valReglasValidacion, char valEsParaTodasLasEmpresas) {
            bool vResult = false;
            try {
                if (!ExisteLaDefinicionDelParametro(valNombre)) {
                    #region Insertar Definicion Parametros
                    StringBuilder vSqlInsertarDefinicionParametro = new StringBuilder();
                    vSqlInsertarDefinicionParametro.AppendLine("INSERT INTO Comun.SettDefinition (");
                    vSqlInsertarDefinicionParametro.AppendLine("Name, Module, LevelModule, GroupName, LevelGroup, Label, DataType, Validationrules, IsSetForAllEnterprise");
                    vSqlInsertarDefinicionParametro.AppendLine(") VALUES ( ");
                    vSqlInsertarDefinicionParametro.AppendLine("'" + valNombre + "',");
                    vSqlInsertarDefinicionParametro.AppendLine("'" + valModulo + "',");
                    vSqlInsertarDefinicionParametro.AppendLine("" + valNivelModulo + ",");
                    vSqlInsertarDefinicionParametro.AppendLine("'" + valNombreDelGrupo + "',");
                    vSqlInsertarDefinicionParametro.AppendLine("" + valNivelDelGrupo + ",");
                    vSqlInsertarDefinicionParametro.AppendLine("'" + valEtiqueta + "',");
                    vSqlInsertarDefinicionParametro.AppendLine("'" + valTipoDeDato + "',");
                    vSqlInsertarDefinicionParametro.AppendLine("'" + valReglasValidacion + "',");
                    vSqlInsertarDefinicionParametro.AppendLine("'" + valEsParaTodasLasEmpresas + "'");
                    vSqlInsertarDefinicionParametro.AppendLine(") ");
                    Execute(vSqlInsertarDefinicionParametro.ToString(), -1);
                    vResult = true;
                    #endregion Insertar Definicion Parametros
                }
                return vResult;
            } catch (Exception vException) {
                throw vException;
            }
        }

        private bool ExisteElValorDelParametroPorCompania(string valNombre, int valConsecutivoCompania) {
            bool vResult = true;
            try {
                #region Verificar Definicion Parametros
                StringBuilder vSqlExisteValorDelParametro = new StringBuilder();
                vSqlExisteValorDelParametro.AppendLine("SELECT * FROM Comun.SettValueByCompany WHERE NameSettDefinition='" + valNombre + "' AND ConsecutivoCompania=" + valConsecutivoCompania);
                System.Data.DataSet vDSValorDelParametro = ExecuteDataset(vSqlExisteValorDelParametro.ToString(), -1);
                #endregion Verificar Definicion Parametros
                if (vDSValorDelParametro != null && vDSValorDelParametro.Tables != null && vDSValorDelParametro.Tables[0] != null && vDSValorDelParametro.Tables[0].Rows.Count <= 0) {
                    vResult = false;
                }
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        private bool ExisteLaDefinicionDelParametro(string valNombre) {
            bool vResult = true;
            try {
                #region Verificar Definicion Parametros
                StringBuilder vSqlExisteDefinicionParametro = new StringBuilder();
                vSqlExisteDefinicionParametro.AppendLine("SELECT * FROM Comun.SettDefinition WHERE Name='" + valNombre + "'");
                System.Data.DataSet vDSDefinicionDeParametros = ExecuteDataset(vSqlExisteDefinicionParametro.ToString(), -1);
                #endregion Verificar Definicion Parametros
                if (vDSDefinicionDeParametros != null && vDSDefinicionDeParametros.Tables != null && vDSDefinicionDeParametros.Tables[0] != null && vDSDefinicionDeParametros.Tables[0].Rows.Count <= 0) {
                    vResult = false;
                }
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        private bool AgregarValorParametroPorCompania(int valConsecutivoCompania, string valNameSettDefinition, string valValor) {
            bool vResult = false;
            try {
                if (!ExisteElValorDelParametroPorCompania(valNameSettDefinition, valConsecutivoCompania)) {
                    StringBuilder vInsertarValorParametro = new StringBuilder();
                    vInsertarValorParametro.AppendLine("INSERT INTO Comun.SettValueByCompany (");
                    vInsertarValorParametro.AppendLine("ConsecutivoCompania, NameSettDefinition, Value, NombreOperador, FechaUltimaModificacion");
                    vInsertarValorParametro.AppendLine(") VALUES ( ");
                    vInsertarValorParametro.AppendLine(valConsecutivoCompania + ",");
                    vInsertarValorParametro.AppendLine("'" + valNameSettDefinition + "',");
                    vInsertarValorParametro.AppendLine("'" + valValor + "',");
                    vInsertarValorParametro.AppendLine("'JEFE',");
                    vInsertarValorParametro.AppendLine(_TodayAsSqlValue);
                    vInsertarValorParametro.AppendLine(" ) ");
                    Execute(vInsertarValorParametro.ToString(), -1);
                    vResult = true;
                }
                return vResult;
            } catch (Exception vEx) {
                throw vEx;
            }
        }

        private IList<int> ObtenerTodosLosConsecutivosDeLasCompanias() {
            StringBuilder vSQL = new StringBuilder();
            XElement vConsultaConsecutivo = null;
            LibDatabase insDb = new LibDatabase();
            IList<int> vResult = null;
            vSQL.Append("SELECT ConsecutivoCompania FROM COMPANIA");
            XmlDocument vData = insDb.LoadData(vSQL.ToString(), -1);
            if (!LibXml.IsEmptyOrNull(vData)) {
                vConsultaConsecutivo = LibXml.ToXElement(vData);
                vResult = CompaniaToList(vConsultaConsecutivo);
            }
            return vResult;
        }

        IList<int> CompaniaToList(XElement valItem) {
            IList<int> vDetailList = new List<int>();
            foreach (XElement vItemDetail in valItem.Descendants("GpResult")) {
                vDetailList.Add(LibConvert.ToInt(vItemDetail.Element("ConsecutivoCompania")));
            }
            return vDetailList;
        }

        public bool CreateViewAndSP(string valModulo) {
            bool vResult = false;
            try {
                string[] vModulo = new string[1];
                vModulo[0] = valModulo;
                if (valModulo != null && !valModulo.Trim().Equals("")) {
                    clsCrearDatabase insBdd = new clsCrearDatabase();
                    insBdd.CrearVistasYProcedimientos(vModulo);
                }
                vResult = true;
            } catch (Exception e) {
                throw e;
            }
            return vResult;
        }

        public bool UpgradeDBVersion() {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("UPDATE Version SET fldVersionBDD = '" + _VersionDataBase + "', NombreOperador='JEFE'");
            Execute(vSql.ToString(), -1);
            return true;
        }

        public void MoverGroupName(string valNameSettDefinition, string valGroupNameActual, string valGroupNameNuevo, int valLevelGroupNuevo) {
            string vSql = "UPDATE Comun.SettDefinition SET GroupName = " + InsSql.ToSqlValue(valGroupNameNuevo) + ", LevelGroup = " + InsSql.ToSqlValue(valLevelGroupNuevo) + " WHERE GroupName = " + InsSql.ToSqlValue(valGroupNameActual) + " AND Name = " + InsSql.ToSqlValue(valNameSettDefinition);
            Execute(vSql, -1);
        }

    }
}