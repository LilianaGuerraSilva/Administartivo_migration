using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_86 : clsVersionARestructurar {
        public clsVersion5_86(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.86";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CreaCamposTablaDetalleDeRendicion();
            CreaCamposTablaPlanillaForma00030();
			CreaCamposTablaCotizacion();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CreaCamposTablaDetalleDeRendicion() {
            if (!ColumnExists("Adm.DetalleDeRendicion", "AplicaIvaAlicuotaEspecial")) {
                AddColumnBoolean("Adm.DetalleDeRendicion", "AplicaIvaAlicuotaEspecial", "CONSTRAINT AplicaIvaA NOT NULL", false);
            }
            if (!ColumnExists("Adm.DetalleDeRendicion", "MontoGravableAlicuotaEspecial1")) {
                AddColumnCurrency("Adm.DetalleDeRendicion", "MontoGravableAlicuotaEspecial1", "MontoGravableAlicuotaEspecial1 NULL", 0);
            }
            if (!ColumnExists("Adm.DetalleDeRendicion", "MontoIVAAlicuotaEspecial1")) {
                AddColumnCurrency("Adm.DetalleDeRendicion", "MontoIVAAlicuotaEspecial1", "MontoIVAAlicuotaEspecial1 NULL", 0);
            }
            if (!ColumnExists("Adm.DetalleDeRendicion", "PorcentajeIvaAlicuotaEspecial1")) {
                AddColumnCurrency("Adm.DetalleDeRendicion", "PorcentajeIvaAlicuotaEspecial1", "PorcentajeIvaAlicuotaEspecial1 NULL", 0);
            }
            if (!ColumnExists("Adm.DetalleDeRendicion", "MontoGravableAlicuotaEspecial2")) {
                AddColumnCurrency("Adm.DetalleDeRendicion", "MontoGravableAlicuotaEspecial2", "MontoGravableAlicuotaEspecial2 NULL", 0);
            }
            if (!ColumnExists("Adm.DetalleDeRendicion", "MontoIVAAlicuotaEspecial2")) {
                AddColumnCurrency("Adm.DetalleDeRendicion", "MontoIVAAlicuotaEspecial2", "MontoIVAAlicuotaEspecial2 NULL", 0);
            }
            if (!ColumnExists("Adm.DetalleDeRendicion", "PorcentajeIvaAlicuotaEspecial2")) {
                AddColumnCurrency("Adm.DetalleDeRendicion", "PorcentajeIvaAlicuotaEspecial2", "PorcentajeIvaAlicuotaEspecial2 NULL", 0);
            }
        }

        private void CreaCamposTablaPlanillaForma00030() {
            if (!ColumnExists("dbo.planillaForma00030", "VentasAlicuotaGeneralBase")) {
                AddColumnCurrency("dbo.planillaForma00030", "VentasAlicuotaGeneralBase", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "VentasAlicEspecial1Base")) {
                AddColumnCurrency("dbo.planillaForma00030", "VentasAlicEspecial1Base", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "VentasAlicEspecial2Base")) {
                AddColumnCurrency("dbo.planillaForma00030", "VentasAlicEspecial2Base", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "VentasAlicuotaGeneralDebito")) {
                AddColumnCurrency("dbo.planillaForma00030", "VentasAlicuotaGeneralDebito", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "VentasAlicEspecial1Debito")) {
                AddColumnCurrency("dbo.planillaForma00030", "VentasAlicEspecial1Debito", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "VentasAlicEspecial2Debito")) {
                AddColumnCurrency("dbo.planillaForma00030", "VentasAlicEspecial2Debito", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "ComprasIntAlicGeneralBase")) {
                AddColumnCurrency("dbo.planillaForma00030", "ComprasIntAlicGeneralBase", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "ComprasIntAlicEsp1Base")) {
                AddColumnCurrency("dbo.planillaForma00030", "ComprasIntAlicEsp1Base", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "ComprasIntAlicEsp2Base")) {
                AddColumnCurrency("dbo.planillaForma00030", "ComprasIntAlicEsp2Base", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "ComprasIntAlicGeneralCredito")) {
                AddColumnCurrency("dbo.planillaForma00030", "ComprasIntAlicGeneralCredito", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "ComprasIntAlicEsp1Credito")) {
                AddColumnCurrency("dbo.planillaForma00030", "ComprasIntAlicEsp1Credito", "", 0);
            }
            if (!ColumnExists("dbo.planillaForma00030", "ComprasIntAlicEsp2Credito")) {
                AddColumnCurrency("dbo.planillaForma00030", "ComprasIntAlicEsp2Credito", "", 0);
            }
        }
		private void CreaCamposTablaCotizacion() {
            if (!ColumnExists("dbo.Cotizacion", "AplicaIvaAlicuotaEspecial")) {
                AddColumnBoolean("dbo.Cotizacion", "AplicaIvaAlicuotaEspecial", "CONSTRAINT AplicaIvaA NOT NULL", false);
            }
        }      

    }
}
