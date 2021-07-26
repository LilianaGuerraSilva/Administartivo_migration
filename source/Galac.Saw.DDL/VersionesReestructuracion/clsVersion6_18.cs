using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Dal;
using System;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_18 : clsVersionARestructurar {

        public clsVersion6_18(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.18";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            CrearNuevosCamposDeRenglonSolicitudDePago();
            AgregarParametroFormaDeCalcularElPrecioConIvaEnRenglonFactura();
            ActualizaPermisoSolicitudesDePago();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void CrearNuevosCamposDeRenglonSolicitudDePago() { 
            try {
                AddColumnString("Saw.RenglonSolicitudesDePago", "CodigoMoneda", 4, "d_RenSolDePagCoMo NOT NULL", CodigoMonedaValido());
                AddColumnDecimal("Saw.RenglonSolicitudesDePago", "TasaDeCambio", 25, 4, "d_RenSolDePagTaCa NOT NULL", LibConvert.ToDec("1"));
            }
            catch (Exception) {
                throw;
            }
        }

        private string CodigoMonedaValido() {
            string vResult = string.Empty;
            vResult = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "CodigoMoneda");
            if (LibString.IsNullOrEmpty(vResult, true)) {
                if (LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.IsCountryEcuador()) {
                    vResult = "USD";
                } else {
                    vResult = "VES";
                }
            }
            return vResult;
        private void AgregarParametroFormaDeCalcularElPrecioConIvaEnRenglonFactura() {
            AgregarNuevoParametro("FormaDeCalculoDePrecioRenglonFactura", "Factura", 2, "2.1.- Facturación", 1, "", eTipoDeDatoParametros.Enumerativo, "", 'N', LibConvert.EnumToDbValue((int)eFormaDeCalculoDePrecioRenglonFactura.APartirDelPrecioConIVA));
        }

        private void ActualizaPermisoSolicitudesDePago() {
            string vSql;
            vSql = "UPDATE Lib.GUserSecurity SET ProjectModule = 'Solicitudes de Pago' WHERE ProjectModule = 'Solicitudes de Pago'";
            Execute(vSql, 0);
        }
    }
}

