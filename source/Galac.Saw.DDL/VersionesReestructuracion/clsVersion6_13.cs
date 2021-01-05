using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_13:clsVersionARestructurar {

        public clsVersion6_13(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.13";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroUsaDivisaComoMonedaPrincipalDeIngresoDeDatos();
            InsertarElementoDelCostoPorDefectos();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroUsaDivisaComoMonedaPrincipalDeIngresoDeDatos() {
            AgregarNuevoParametro("UsaDivisaComoMonedaPrincipalDeIngresoDeDatos","Bancos",7,"7.2-Moneda",2,"",eTipoDeDatoParametros.String,"",'N',"N");
        }

        public bool InsertarElementoDelCostoPorDefectos() {
            bool vResult = false;
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("SELECT ConsecutivoCompania FROM dbo.Compania WHERE ConsecutivoCompania NOT IN(SELECT ConsecutivoCompania FROM Comun.ElementoDelCosto) ");
            XElement RetXml = LibBusiness.ExecuteSelect(vSql.ToString(),null,"",0);
            if(RetXml != null && RetXml.HasElements) {
                List<XElement> vListCompania = RetXml.Descendants("GpResult").ToList();
                vSql.Clear();
                foreach(XElement vXmlCompania in vListCompania) {
                    int vConsecutivoCompania = LibImportData.ToInt(LibXml.GetElementValueOrEmpty(vXmlCompania,"ConsecutivoCompania"));
                    vSql.AppendLine("IF(SELECT UsaModuloDeContabilidad FROM Compania WHERE ConsecutivoCompania = " + _insSql.ToSqlValue(vConsecutivoCompania) + ") =" + _insSql.ToSqlValue(true));
                    vSql.AppendLine(" BEGIN ");
                    vSql.AppendLine("INSERT INTO Comun.ElementoDelCosto");
                    vSql.AppendLine("(ConsecutivoCompania,Consecutivo,Tipo,OrdenParaInforme,Nombre,NombreOperador,FechaUltimaModificacion)");
                    vSql.AppendLine("VALUES");
                    vSql.AppendLine(ElementoDelCostoPorDefectoRow(vConsecutivoCompania,1,1,0,"Sin Asignar") + ",");
                    vSql.AppendLine(ElementoDelCostoPorDefectoRow(vConsecutivoCompania,2,0,1,"Adquisición") + ",");
                    vSql.AppendLine(ElementoDelCostoPorDefectoRow(vConsecutivoCompania,3,0,1,"Conversión"));
                    vSql.AppendLine(" END ");
                    Execute(vSql.ToString(),0);
                    vSql.Clear();
                }
            }
            return vResult;
        }

        public string ElementoDelCostoPorDefectoRow(int valConsecutivoCompania,int valConsecutivo,int valTipo,int valOrdenParaInforme,string valNombre) {
            string vResult = "";
            vResult = "(" + InsSql.ToSqlValue(valConsecutivoCompania) + ",";
            vResult += InsSql.ToSqlValue(valConsecutivo) + ",";
            vResult += InsSql.ToSqlValue(LibConvert.ToStr(valTipo)) + ",";
            vResult += InsSql.ToSqlValue(LibConvert.ToStr(valOrdenParaInforme)) + ",";
            vResult += InsSql.ToSqlValue(valNombre) + ",";
            vResult += InsSql.ToSqlValue(((CustomIdentity)Thread.CurrentPrincipal.Identity).Login) + ",";
            vResult += InsSql.ToSqlValue(LibDate.Today()) + ")";
            return vResult;
        }
    }
}

