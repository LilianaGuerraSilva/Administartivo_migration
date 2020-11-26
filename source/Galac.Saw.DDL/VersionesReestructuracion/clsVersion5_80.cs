using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;
using Galac.Adm.Ccl.Banco;
using Galac.Saw.Ccl.SttDef;


namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_80 : clsVersionARestructurar {

        public clsVersion5_80(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.80";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarParametroFechaSugeridaRetenciones();
            CrearCamposDetalleDeRendicion();
            AumentarCaracteresEnCampoDetalleDeRendicion();
            AgregaParametroNombrePlantillaRetencionImpuestoMunicipalInforme();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarParametroFechaSugeridaRetenciones() {
            AgregarNuevoParametro("FechaSugeridaRetencionesCxP", "CxP/Compras", 6, "6.2.- CxP / Proveedor / Pagos", 2, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "0");
        }

        private void CrearCamposDetalleDeRendicion() {
            if (!ColumnExists("Adm.DetalleDeRendicion", "GeneradaPor")) {
                AddColumnEnumerative("Adm.DetalleDeRendicion", "GeneradaPor", "GeneradaPor NULL", LibConvert.EnumToDbValue((int)eGeneradoPor.Rendicion));
            }
            if (!ColumnExists("Adm.DetalleDeRendicion", "MontoRetencion")) {
                AddColumnCurrency("Adm.DetalleDeRendicion", "MontoRetencion", "MontoRetencion NULL", 0);
            }
        }

        private void AumentarCaracteresEnCampoDetalleDeRendicion() {
            ModifyLengthOfColumnString("Adm.DetalleDeRendicion", "NumeroDocumento", 25, "Not Null");
        }
        
        private void AgregaParametroNombrePlantillaRetencionImpuestoMunicipalInforme() {
            AgregarNuevoParametro("NombrePlantillaRetencionImpuestoMunicipalInforme", "CxP/Compras", 6, "6.2.- CxP / Proveedor / Pagos", 2, "", eTipoDeDatoParametros.String, "", 'N', "rpxResumenRetencionDeActividadesEconomicas");
        }
    }
}
