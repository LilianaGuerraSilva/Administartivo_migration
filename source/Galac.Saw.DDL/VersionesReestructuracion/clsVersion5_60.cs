using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using Galac.Contab.Dal.Tablas;
using System.Threading;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_60 :clsVersionARestructurar {

      public clsVersion5_60(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.60";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         AgregarMunicipioGirardotAFomatoIM();
         AgregaImpuestoTransaccionesFinancieras();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AgregarMunicipioGirardotAFomatoIM() {
         if(RecordCountOfSql(@"SELECT Codigo FROM Comun.FormatosImpMunicipales WHERE Codigo = 22") <= 0) {
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
            vSqlSb.AppendLine("22,");
            vSqlSb.AppendLine("'VENARA0003',");
            vSqlSb.AppendLine("'NroDeComprobantedeRetencion',");
            vSqlSb.AppendLine("'NumeroComprobanteImpuestoMunicipal',");
            vSqlSb.AppendLine("11,");
            vSqlSb.AppendLine("11,");
            vSqlSb.AppendLine("1,");
            vSqlSb.AppendLine("'YYYY+MM+00000',");
            vSqlSb.AppendLine("'S',");
            vSqlSb.AppendLine("CHAR(9),");
            vSqlSb.AppendLine("'YEAR(FechaAplicacionImpuestoMunicipal)=@Ano AND MONTH(FechaAplicacionImpuestoMunicipal) =@Mes',");
            vSqlSb.AppendLine("'JEFE',");
            vSqlSb.AppendLine("GETDATE()");
            vSqlSb.AppendLine(")");           
            Execute(vSqlSb.ToString(), 0);
         }
      }

      private void AgregaImpuestoTransaccionesFinancieras() {
         if(RecordCountOfSql(@"Select FechaDeInicioDeVigencia FROM dbo.ImpTransacBancarias WHERE FechaDeInicioDeVigencia = (Convert(datetime,'01/02/2016',103))") <= 0) {
            StringBuilder vSqlSb = new StringBuilder();
            vSqlSb.AppendLine("INSERT INTO dbo.ImpTransacBancarias");
            vSqlSb.AppendLine("(FechaDeInicioDeVigencia, AlicuotaAlDebito, AlicuotaAlCredito)");
            vSqlSb.AppendLine("VALUES");
            vSqlSb.AppendLine("(Convert(datetime,'01/02/2016',103), 0.75,0)");
            Execute(vSqlSb.ToString(), 0);
         }
      }

   }
}
