using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using LibGalac.Aos.Base;
using System.Threading;
using System.Data;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using System.Transactions;
using Galac.Comun.Ccl.SttDef;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_04:clsVersionARestructurar {
        public clsVersion6_04(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.04";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            AgregarColumnaEnTablaFactura();
            AgregaParametroListaPrecioOtraMonedaCXC();
            AgregarNuevosParametrosEnInventario();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void AgregarColumnaEnTablaFactura() {
            if(!ColumnExists("dbo.Factura","CambioMonedaCXC")) {
                AddColumnCurrency("dbo.Factura","CambioMonedaCXC","",1m);
            }
        }

        private void AgregaParametroListaPrecioOtraMonedaCXC() {
            StringBuilder vSql = new StringBuilder();
            AgregarNuevoParametro("UsaListaDePrecioEnMonedaExtranjeraCXC","Factura",2,"2.1.- Facturación",1,"",eTipoDeDatoParametros.String,"",'N',"N");
            //Correcciones en Definicion de Parámetros
            vSql.AppendLine("UPDATE Comun.SettDefinition ");
            vSql.AppendLine("SET LevelGroup = 1 ");
            vSql.AppendLine("WHERE Name = 'UsaListaDePrecioEnMonedaExtranjera'");
            Execute(vSql.ToString(),0);
            vSql.Clear();
            vSql.AppendLine("UPDATE Comun.SettDefinition ");
            vSql.AppendLine("SET GroupName = '2.1.- Facturación' ");
            vSql.AppendLine("WHERE Name = 'UsaListaDePrecioEnMonedaExtranjera'");
            Execute(vSql.ToString(),0);
        }

        private void AgregarNuevosParametrosEnInventario() {
            AgregarNuevoParametro("DuracionEnPantallaEnSegundos","Inventario",5,"5.4-. Verificador de Precios",4,"",eTipoDeDatoParametros.Int,"",'N',"5");
            AgregarNuevoParametro("RutaImagen","Inventario",5,"5.4-. Verificador de Precios",4,"",eTipoDeDatoParametros.String,"",'N',"");
            AgregarNuevoParametro("TipoDePrecioAMostrarEnVerificador","Inventario",5,"5.4-. Verificador de Precios",4,"",eTipoDeDatoParametros.Enumerativo,"",'N',"0");
            AgregarNuevoParametro("TipoDeBusquedaArticulo","Inventario",5,"5.4-. Verificador de Precios",4,"",eTipoDeDatoParametros.Enumerativo,"",'N',"0");
            AgregarNuevoParametro("NivelDePrecioAMostrar","Inventario",5,"5.4-. Verificador de Precios",4,"",eTipoDeDatoParametros.Enumerativo,"",'N',"0");
        }

    }
}
