using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_82 : clsVersionARestructurar {
        public clsVersion5_82(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.82";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearTablaDetraccion();
            AgregarParametroEnFactura();
            ActualizarOtrosImpuestosCxP();
            CrearCampoEnTablaCxP();
            DisposeConnectionNoTransaction();
            return true;
        }

        public void CrearTablaDetraccion() {
            if (!TableExists("Comun.TablaDetraccion")) {
                new Galac.Comun.Dal.Impuesto.clsTablaDetraccionED().InstalarTabla();

            }
        }

        public void AgregarParametroEnFactura() {
            AgregarNuevoParametro("PermitirCambioTasaMondExtrajalEmitirFactura", "Factura", 2, "2.1.- Facturación ", 1, "", eTipoDeDatoParametros.String, "", 'N', "N");
        }

        public void ActualizarOtrosImpuestosCxP() {
            if (!ColumnExists("OtrosImpuestosCxP", "Descripcion")) {
                AddColumnString("OtrosImpuestosCxP", "Descripcion", 255, "Descripcion NULL", "");
            }
            if (!ColumnExists("OtrosImpuestosCxP", "CodigoOI")) {
                AddColumnString("OtrosImpuestosCxP", "CodigoOI", 10, "CodigoOI NULL", "");
            }
            if (!ColumnExists("OtrosImpuestosCxP", "CodigoOI")) {
                AddColumnString("OtrosImpuestosCxP", "CodigoOI", 10, "CodigoOI NULL", "");
            }
            if (!ColumnExists("OtrosImpuestosCxP", "AlicuotaOI")) {
                AddColumnCurrency("OtrosImpuestosCxP", "AlicuotaOI", "AlicuotaOI NULL", 0);
            }
            if (!ColumnExists("OtrosImpuestosCxP", "MontoBaseImponible")) {
                AddColumnCurrency("OtrosImpuestosCxP", "MontoBaseImponible", "MontoBaseImponible NULL", 0);
            }
        }

        public void CrearCampoEnTablaCxP() {
            if (!ColumnExists("CxP", "SeHizoLaDetraccion")) {
                AddColumnBoolean("CxP", "SeHizoLaDetraccion", "CONSTRAINT SeHizoLaDe NOT NULL", false);
            }
        }
    }
}
