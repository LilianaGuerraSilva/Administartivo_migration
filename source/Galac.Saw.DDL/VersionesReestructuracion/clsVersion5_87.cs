using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal.Settings;
using Galac.Adm.Ccl.Banco;
using Galac.Comun.Ccl.SttDef;
using LibGalac.Aos.Base;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion5_87 : clsVersionARestructurar {
        public clsVersion5_87(string valCurrentDataBaseName)
            : base(valCurrentDataBaseName) {
            _VersionDataBase = "5.87";
        }

        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            ModificarParametrosFechaDecreto();
            StartConnectionNoTransaction();
            CreaCamposCxp();
			CrearColumnasEnFactura();
            AsignarValoresACamposNuevos();
            CrearClaveUnicaEnFactura();
            CrearParametroNumeroSerie();
            CrearColumnasEnCxC();
            CrearCampoTablaArticuloInventario();
            CrearTablaMotivoDeTraslado();
            CrearTablaConductor();
            CrearTablaGuiaDeRemision();
            CrearAdmCompra();
            CrearComunAranceles();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void ModificarParametrosFechaDecreto() {
            StringBuilder vSql = new StringBuilder();
            QAdvSql vQAdvSQL = new QAdvSql("");

            string vFechaInicio = "";
            string vFechaFin = "";
            vFechaInicio = new DateTime(2016, 12, 24).ToString("yyyy-MM-dd HH:mm:ss");
            vFechaFin = new DateTime(2017, 03, 23).ToString("yyyy-MM-dd HH:mm:ss");

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + vQAdvSQL.ToSqlValue(vFechaInicio));
            vSql.AppendLine(" WHERE Comun.SettValueByCompany.NameSettDefinition = " + vQAdvSQL.ToSqlValue("FechaInicioAlicuotaIva10Porciento"));
            Execute(vSql.ToString(), -1);
            vSql.Clear();

            vSql.AppendLine("UPDATE Comun.SettValueByCompany");
            vSql.AppendLine(" SET Value = " + vQAdvSQL.ToSqlValue(vFechaFin));
            vSql.AppendLine(" WHERE Comun.SettValueByCompany.NameSettDefinition = " + vQAdvSQL.ToSqlValue("FechaFinAlicuotaIva10Porciento"));
            Execute(vSql.ToString(), -1);
        }

        private void CreaCamposCxp() {
            if (!ColumnExists("dbo.cxp", "NumeroSerie")) {
                AddColumnString("dbo.cxp", "NumeroSerie",10,"","");
            }
            if (!ColumnExists("dbo.cxp", "NumeroDeDocumento")) {
                AddColumnString("dbo.cxp", "NumeroDeDocumento",20,"","");
            }
            if (!ColumnExists("dbo.cxp", "NumeroSerieDocAfectado")) {
                AddColumnString("dbo.cxp", "NumeroSerieDocAfectado",10,"","");
            }
            if (!ColumnExists("dbo.cxp", "NumeroDeDocumentoAfectado")) {
                AddColumnString("dbo.cxp", "NumeroDeDocumentoAfectado",20,"","");
            }
        }

        private void CrearColumnasEnFactura() {            
            if (!ColumnExists("dbo.Factura","NumeroDeSerie")) {
                AddColumnString("dbo.Factura", "NumeroDeSerie", 10, "", "");
            }
            if (!ColumnExists("dbo.Factura", "NumeroDeDocumento")) {
                AddColumnString("dbo.Factura", "NumeroDeDocumento", 20, "", "");
            }            
        }

        private void AsignarValoresACamposNuevos() {
            String vSql = "";
            if (ColumnExists("dbo.Factura", "NumeroDeSerie") && ColumnExists("dbo.Factura", "NumeroDeDocumento")) {
                vSql = "UPDATE dbo.Factura SET NumeroDeSerie = Talonario, NumeroDeDocumento = Numero";
                Execute(vSql, -1);
            }
        }


        private void CrearClaveUnicaEnFactura() {
            AddNotNullConstraint("dbo.factura", "NumeroDeSerie", InsSql.VarCharTypeForDb(10));
            AddNotNullConstraint("dbo.factura", "NumeroDeDocumento", InsSql.VarCharTypeForDb(20));
            if (!(UniqueKeyContraintNameExists("dbo.factura", "u_NumSerieDocumentoFactura"))) {
               AddUniqueKey("dbo.factura", "ConsecutivoCompania, NumeroDeSerie, NumeroDeDocumento, TipoDeDocumento", "u_NumSerieDocumentoFactura");
			}
        }
		

        private void CrearParametroNumeroSerie() {
            AgregarNuevoParametro("NumeroSerieTalonario1", "Factura", 2, "2.4.- Modelo de Factura ", 4, "", eTipoDeDatoParametros.String, "", 'N', "");
            AgregarNuevoParametro("NumeroSerieTalonario2", "Factura", 2, "2.4.- Modelo de Factura ", 4, "", eTipoDeDatoParametros.String, "", 'N', "");
        }

        private void CrearColumnasEnCxC() {
            if (!ColumnExists("dbo.CxC", "NumeroDeSerie")) {
                AddColumnString("dbo.CxC", "NumeroDeSerie", 10, "", "");
            }
            if (!ColumnExists("dbo.CxC", "NumeroDeDocumento")) {
                AddColumnString("dbo.CxC", "NumeroDeDocumento", 20, "", "");
            }
        }

        private void CrearCampoTablaArticuloInventario() {
            if (!ColumnExists("ArticuloInventario", "Peso")) {
                AddColumnCurrency("ArticuloInventario", "Peso", "", 0);
            }
            if (!ColumnExists("ArticuloInventario", "ArancelesCodigo")) {
                AddColumnString("ArticuloInventario", "ArancelesCodigo", 13, "", "");
            }
        }
		
        private void CrearTablaMotivoDeTraslado() {
            //if (!TableExists("Adm.MotivoDeTraslado")) {
            //    new Galac.Adm.Dal.Venta.clsMotivoDeTrasladoED().InstalarTabla();
            //}
        }
		
        private void CrearTablaConductor() {
            //if (!TableExists("Adm.Conductor")) {
            //    new Galac.Adm.Dal.Vehiculo.clsConductorED().InstalarTabla();
            //}
        }

        private void CrearTablaGuiaDeRemision() {
            //if (!TableExists("Adm.GuiaDeRemision")) {
            //    new Galac.Adm.Dal.Venta.clsGuiaDeRemisionED().InstalarTabla();
            //}
        }

        private void CrearAdmCompra() {
            //if (!TableExists("Adm.Compra")) {
            //    new Galac.Adm.Dal.GestionCompras.clsCompraED().InstalarTabla();
            //    DeleteTabladboCompra();
            //}
            // Galac.Saw.DDL.clsCompatViews.CrearVistaDboCompra();
        }

        private void CrearComunAranceles() {
            if (!TableExists("Comun.Aranceles")) {
                new Galac.Comun.Dal.Impuesto.clsArancelesED().InstalarTabla();
            }
        }

        private void DeleteTabladboCompra() {
            if (TableExists("dbo.RenglonCompra")) {
                ExecuteDropTable("dbo.RenglonCompra");
                ExecuteDropTable("dbo.Compra");
            }
        }
    }
}
