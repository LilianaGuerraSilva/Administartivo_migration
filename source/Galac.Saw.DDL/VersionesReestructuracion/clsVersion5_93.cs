using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_93:clsVersionARestructurar {
        public clsVersion5_93(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.93";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarColmnaEnArticuloInventario();
            CrearTablaBalanza();
            CrearParametroUsarBalanzaPuntoVenta();
            DropConstraintCompraNumero();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarColmnaEnArticuloInventario() {
            if(!ColumnExists("ArticuloInventario","UsaBalanza")) {
                AddColumnBoolean("ArticuloInventario","UsaBalanza","",false);
            }
        }

        private void CrearTablaBalanza() {
            try {
                Galac.Adm.Dal.DispositivosExternos.clsBalanzaED insBalanzaED = new Galac.Adm.Dal.DispositivosExternos.clsBalanzaED();
                if(!TableExists("Adm.Balanza")) {
                    insBalanzaED.InstalarTabla();
                } else {
                    insBalanzaED.BorrarVistasYSps();
                    insBalanzaED.InstalarVistasYSps();
                }
            } catch(Exception) {
                throw;
            }
        }

        private void CrearParametroUsarBalanzaPuntoVenta() {
            AgregarNuevoParametro("UsarBalanza","Factura",2,"2.6.- Punto De Venta",6,"",'2',"",'N',"N");
        }

        private void DropConstraintCompraNumero() {
            ExecuteDropConstraint("dbo.Compra","u_Compranumero",true);
        }
    }
}
