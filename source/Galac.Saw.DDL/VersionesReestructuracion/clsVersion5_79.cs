using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.SttDef;


namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_79 : clsVersionARestructurar {

        public clsVersion5_79(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.79";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarMunicipioCristobalRojasAFomatoIM();
            AgregaPermisoCopiarParametros();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarMunicipioCristobalRojasAFomatoIM() {
            if (RecordCountOfSql(@"SELECT Codigo FROM Comun.FormatosImpMunicipales WHERE Codigo = 24") <= 0) {
                StringBuilder vSqlSb = new StringBuilder();
                vSqlSb.AppendLine("INSERT INTO Comun.FormatosImpMunicipales");
                vSqlSb.AppendLine("(Codigo");
                vSqlSb.AppendLine(",CodigoMunicipio");
                vSqlSb.AppendLine(",Columna");
                vSqlSb.AppendLine(",OrigenDeDatos");
                vSqlSb.AppendLine(",Longitud");
                vSqlSb.AppendLine(",Posicion");
                vSqlSb.AppendLine(",Linea");
                vSqlSb.AppendLine(",Formato");
                vSqlSb.AppendLine(",UsaSeparador");
                vSqlSb.AppendLine(",Separador");
                vSqlSb.AppendLine(",Condicion");
                vSqlSb.AppendLine(",NombreOperador");
                vSqlSb.AppendLine(",FechaUltimaModificacion");
                vSqlSb.AppendLine(")");
                vSqlSb.AppendLine("VALUES");
                vSqlSb.AppendLine("(");
                vSqlSb.AppendLine("24,");
                vSqlSb.AppendLine("'VENMIR0008',");
                vSqlSb.AppendLine("'NroDeComprobantedeRetencion',");
                vSqlSb.AppendLine("'NumeroComprobanteImpuestoMunicipal',");
                vSqlSb.AppendLine("12,");
                vSqlSb.AppendLine("12,");
                vSqlSb.AppendLine("1,");
                vSqlSb.AppendLine("'YYYY+MM+000000',");
                vSqlSb.AppendLine("'S',");
                vSqlSb.AppendLine("CHAR(9),");
                vSqlSb.AppendLine("'YEAR(FechaAplicacionImpuestoMunicipal)=@Ano AND MONTH(FechaAplicacionImpuestoMunicipal) =@Mes',");
                vSqlSb.AppendLine("'JEFE',");
                vSqlSb.AppendLine("GETDATE()");
                vSqlSb.AppendLine(")");
                Execute(vSqlSb.ToString(), 0);
            }
        }

        private void AgregaPermisoCopiarParametros() {
            StringBuilder vSqlSelect = new StringBuilder();
            vSqlSelect.AppendLine("SELECT Lib.GUser.UserName FROM Lib.GUser WHERE " + InsSql.SqlValueWithAnd("", "IsSuperviser", "S"));
            System.Data.DataSet vDSChkUsuario = ExecuteDataset(vSqlSelect.ToString(), -1);
            if (vDSChkUsuario.Tables[0].Rows.Count > 0) {
                foreach (System.Data.DataRow row in vDSChkUsuario.Tables[0].Rows) {
                    foreach (object item in row.ItemArray) {
                        AgregarPermisoDeCopiarParametros((string)item);
                    }
                }
            }
        }

        private void AgregarPermisoDeCopiarParametros(string valUsername) {
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT Lib.GUserSecurity.UserName ");
            vSql.AppendLine("FROM Lib.GUserSecurity ");
            vSql.AppendLine("WHERE Lib.GUserSecurity.ProjectModule = 'Compañía' AND Lib.GUserSecurity.ProjectAction = 'Copiar Parámetros Administrativos' AND Lib.GUserSecurity.UserName = '" + valUsername + "'");            
            if (RecordCountOfSql(vSql.ToString()) == 0) {
                vSql.Clear();
                vSql.AppendLine("INSERT INTO Lib.GUserSecurity (UserName, ProjectModule, ProjectAction, HasAccess, FunctionalGroup, FunctionalGroupLevel, ProgramInitials) ");
                vSql.AppendLine(" VALUES ('" + valUsername + "', 'Compañía', 'Copiar Parámetros Administrativos', 'S', 'Compañía / Parámetros / Niveles de Precio', 2, 'SAW')");                
                Execute(vSql.ToString(), 0);
            }
        }
    }
}
