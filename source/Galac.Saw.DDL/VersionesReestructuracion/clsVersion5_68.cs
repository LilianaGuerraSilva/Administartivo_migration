using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using Galac.Comun.Ccl.TablasLey;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_68 : clsVersionARestructurar {

      public clsVersion5_68(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.68";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         CrearCamposCobranza();
         CrearCamposAnticipo();
         FillNullValuesCobranza();
         FillNullValuesAnticipo();
         InsertarCamposExtrasEnReglonFactura();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void CrearCamposCobranza()
      {
         if (!ColumnExists("dbo.Cobranza", "TipoDeDocumento"))
         {
            AddColumnString("dbo.Cobranza", "TipoDeDocumento", 1, "", "");
         }
         if (!ColumnExists("dbo.Cobranza", "NumeroDeComprobanteISLR"))
         {
            AddColumnInteger("dbo.Cobranza", "NumeroDeComprobanteISLR", ""); 
         }
      }      
 
      private void CrearCamposAnticipo()
      {
         if (!ColumnExists("dbo.Anticipo", "GeneradoPor"))
         {
            AddColumnString("dbo.Anticipo", "GeneradoPor", 1, "", "");
         }
         if (!ColumnExists("dbo.Anticipo", "NumeroDeCobranzaAsociado"))
         {
            AddColumnString("dbo.Anticipo", "NumeroDeCobranzaAsociado", 12, "", "");
         }
      }

      private bool FillNullValuesCobranza()
      {
         StringBuilder vSql = new StringBuilder();
         vSql.AppendLine("UPDATE dbo.Cobranza ");
         vSql.AppendLine("SET dbo.Cobranza.TipoDeDocumento =  '0' ");
         vSql.AppendLine(" WHERE dbo.Cobranza.TipoDeDocumento IS NULL OR dbo.Cobranza.TipoDeDocumento = ''");
         Execute(vSql.ToString(), -1);
         return true;
      }

      private bool FillNullValuesAnticipo()
      {
         StringBuilder vSql = new StringBuilder();
         vSql.AppendLine("UPDATE dbo.anticipo ");
         vSql.AppendLine("SET dbo.anticipo.GeneradoPor =  '0' ");
         vSql.AppendLine(" WHERE (dbo.anticipo.GeneradoPor IS NULL OR dbo.anticipo.GeneradoPor  = '') AND dbo.anticipo.EsUnaDevolucion = 'N'");
         Execute(vSql.ToString(), -1);
         vSql.Clear();
         vSql.AppendLine("UPDATE dbo.anticipo ");
         vSql.AppendLine("SET dbo.anticipo.GeneradoPor =  '8' "); 
         vSql.AppendLine(" WHERE (dbo.anticipo.GeneradoPor IS NULL OR dbo.anticipo.GeneradoPor  = '') AND dbo.anticipo.EsUnaDevolucion = 'S'");
         Execute(vSql.ToString(), -1);
         return true;
      }
	  
      private void InsertarCamposExtrasEnReglonFactura()
      {
         if (!ColumnExists("dbo.RenglonFactura", "CampoExtraEnRenglonFactura1"))
         {
            AddColumnString("dbo.RenglonFactura", "CampoExtraEnRenglonFactura1", 60, "", "");
         }
         if (!ColumnExists("dbo.RenglonFactura", "CampoExtraEnRenglonFactura2"))
         {
            AddColumnString("dbo.RenglonFactura", "CampoExtraEnRenglonFactura2", 60, "", "");
         }
      }
    }
}
