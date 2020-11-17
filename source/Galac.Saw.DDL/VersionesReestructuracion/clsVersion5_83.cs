using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal.PASOnLine;
using LibGalac.Aos.Dal.Settings;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_83 : clsVersionARestructurar {
         public clsVersion5_83(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.83";
        }

         public override bool UpdateToVersion() {
             StartConnectionNoTransaction();
             CorregirCampoNotaDeEntrega();
             CrearTablaAlicuotaImpuestoEspecial();
             CrearEstructurasParaDiagnostico();
             DisposeConnectionNoTransaction();
             return true;
         }

         private void CorregirCampoNotaDeEntrega() {
             StringBuilder vSql = new StringBuilder();
             vSql.AppendLine("UPDATE dbo.factura ");
             vSql.AppendLine(" SET GeneradaPorNotaEntrega =  '0' ");
             vSql.AppendLine(" WHERE GeneradaPorNotaEntrega = 'N'");
             Execute(vSql.ToString(), -1);
         }

         public void CrearTablaAlicuotaImpuestoEspecial() {
             if (!TableExists("Comun.AlicuotaImpuestoEspecial")) {
                 new Galac.Comun.Dal.Impuesto.clsAlicuotaImpuestoEspecialED().InstalarTabla();

             }
         }

         private void CrearEstructurasParaDiagnostico() {
             if (!TableExists("Lib.Diagnostic")) {
                 new LibDiagnosticED().InstalarTabla();
             }
             if (!TableExists("Lib.DiagnosticStt")) {
                 new LibDiagnosticSttED().InstalarTabla();
             }
         }

    }
}
