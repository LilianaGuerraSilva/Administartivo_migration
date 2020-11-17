using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_73 : clsVersionARestructurar {

      public clsVersion5_73(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.73";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         AgregarParametrosDeFechasDecretoIva10();
         InsertarTipoDeCobroAnticipo();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void AgregarParametrosDeFechasDecretoIva10()
      {
         string vFechaInicio = "";
         string vFechaFin = "";
         vFechaInicio = new DateTime(2016, 12, 24).ToString("yyyy-MM-dd HH:mm:ss");
         vFechaFin = new DateTime(2017, 03, 23).ToString("yyyy-MM-dd HH:mm:ss");
         AgregarNuevoParametro("FechaInicioAlicuotaIva10Porciento", "DatosGenerales", 1, "1.1.-Compania", 1, "", '0', "", 'N',vFechaInicio);
         AgregarNuevoParametro("FechaFinAlicuotaIva10Porciento", "DatosGenerales", 1, "1.1.-Compania", 1, "", '0', "", 'N', vFechaFin);
      }
      
      private void InsertarTipoDeCobroAnticipo()
      {
         StringBuilder vSQL = new StringBuilder();
         vSQL.AppendLine("SELECT Codigo FROM SAW.FormaDelCobro WHERE TipoDePago = '4'");
         if (RecordCountOfSql(vSQL.ToString()) <= 0)
         {
            string vNextCodigo = "";                        
            vNextCodigo = new LibGalac.Aos.Dal.LibDatabase("").NextStrConsecutive("SAW.FormaDelCobro", "Codigo", "", true, 5);
            vSQL.Clear();
            vSQL.AppendLine("INSERT INTO SAW.FormaDelCobro (Codigo, Nombre, TipoDePago) VALUES (" + InsSql.ToSqlValue(vNextCodigo) + ", " + InsSql.ToSqlValue("ANTICIPO") + ", " + InsSql.ToSqlValue("4") + ")");
            Execute(vSQL.ToString(), 0);
         }
      }
   }
}
