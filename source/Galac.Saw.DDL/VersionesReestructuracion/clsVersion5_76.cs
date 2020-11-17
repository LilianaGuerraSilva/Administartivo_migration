using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_76 : clsVersionARestructurar {

      public clsVersion5_76(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.76";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         CrearParametrosPuntoDeVenta();
         AgregarMunicipioPlazaAFomatoIM();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void CrearParametrosPuntoDeVenta() {         
         AgregarNuevoParametro("AcumularItemsEnRenglonesDeFactura", "Factura", 2, "2.6.- Punto De Venta", 6, "", '2', "",'N',"S");
      }

      private void AgregarMunicipioPlazaAFomatoIM() {
          if (RecordCountOfSql(@"SELECT Codigo FROM Comun.FormatosImpMunicipales WHERE Codigo = 23") <= 0) {
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
              vSqlSb.AppendLine("23,");
              vSqlSb.AppendLine("'VENMIR0017',");
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
   }
}
