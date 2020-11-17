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
   class clsVersion5_72 : clsVersionARestructurar {

      public clsVersion5_72(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.72";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         VerificarRegistrosNULLEnTablaRetDocumentoPagado();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void VerificarRegistrosNULLEnTablaRetDocumentoPagado()
      {
         StringBuilder vSql = new StringBuilder();
         vSql.AppendLine("UPDATE dbo.RetDocumentoPagado");
         vSql.AppendLine(" SET dbo.RetDocumentoPagado.MontoRetencion = 0");
         vSql.AppendLine(" WHERE dbo.RetDocumentoPagado.MontoRetencion IS NULL");
         Execute(vSql.ToString(), -1);
         vSql.Clear();
         vSql.AppendLine("UPDATE dbo.RetDocumentoPagado");
         vSql.AppendLine(" SET  dbo.RetDocumentoPagado.MontoBaseImponible = 0");
         vSql.AppendLine(" WHERE dbo.RetDocumentoPagado.MontoBaseImponible IS NULL");
         Execute(vSql.ToString(), -1);
         vSql.Clear();
         vSql.AppendLine("UPDATE dbo.RetDocumentoPagado");
         vSql.AppendLine(" SET  dbo.RetDocumentoPagado.PorcentajeDeRetencion = 0");
         vSql.AppendLine(" WHERE dbo.RetDocumentoPagado.PorcentajeDeRetencion IS NULL");
         Execute(vSql.ToString(), -1);
      }
   }
}
