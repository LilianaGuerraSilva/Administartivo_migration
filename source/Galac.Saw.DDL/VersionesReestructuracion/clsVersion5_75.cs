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
   class clsVersion5_75 : clsVersionARestructurar {

      public clsVersion5_75(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.75";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         CrearCamposRenglonCobroDeFactura();
         CrearCamposFactura();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void CrearCamposRenglonCobroDeFactura() {
          if (!ColumnExists("dbo.renglonCobroDeFactura", "CodigoPuntoDeVenta")) {
              AddColumnInteger("dbo.renglonCobroDeFactura", "CodigoPuntoDeVenta","");
          }
          if (!ColumnExists("dbo.renglonCobroDeFactura", "NumeroDocumentoAprobacion")) {
              AddColumnString("dbo.renglonCobroDeFactura", "NumeroDocumentoAprobacion", 30, "", "");
          }
      }

      private void CrearCamposFactura() {
          if (!ColumnExists("Factura", "EsGeneradaPorPuntoDeVenta")) {
              AddColumnBoolean("Factura", "EsGeneradaPorPuntoDeVenta", "EsGeneradaPorPuntoDeVenta NOT NULL", false);
          }
      }
   }
}
