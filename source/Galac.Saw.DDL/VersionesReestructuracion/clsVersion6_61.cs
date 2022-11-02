using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using System.Text;
using LibGalac.Aos.Brl;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Contab.Ccl.WinCont;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersion6_61 : clsVersionARestructurar {
        public clsVersion6_61(string valCurrentDataBaseName) : base(valCurrentDataBaseName) {
            _VersionDataBase = "6.61";
        }
		
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
            InsertarSiNoExisteLineaDeProducto();
            InsertarSiNoExisteCategoria();
            InsertarSiNoExisteUnidadDeVenta();
            CrearArticulosEspecialesIGTF_ML();
            CrearArticulosEspecialesIGTF_ME();
            AgregarParametroAsociarCentroDeCostos();
            CrearCampoExcluirDelInformeDeDeclaracionIGTF();
            CrearCampoRegistroTXTEnCajaRegistradora();
            DisposeConnectionNoTransaction();
            return true;
        }

        private void InsertarSiNoExisteLineaDeProducto() {
            QAdvSql InsSql = new QAdvSql("");
            string vFechaUltimaModificacion = InsSql.ToSqlValue(LibDate.Today());
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("INSERT INTO Adm.LineaDeProducto (ConsecutivoCompania, Consecutivo, Nombre, PorcentajeComision, CentroDeCosto, NombreOperador, FechaUltimaModificacion)");
            vSql.AppendLine(" SELECT COMPANIA.ConsecutivoCompania, ISNULL((SELECT MAX(LineaDeProducto.Consecutivo) From Adm.LineaDeProducto), 0) + 1, 'LINEA DE PRODUCTO', 0, '', 'JEFE', " + vFechaUltimaModificacion + " FROM COMPANIA");
            vSql.AppendLine(" WHERE NOT EXISTS(SELECT LineaDeProducto.Nombre FROM Adm.LineaDeProducto WHERE LineaDeProducto.ConsecutivoCompania = COMPANIA.ConsecutivoCompania AND LineaDeProducto.Nombre = 'LINEA DE PRODUCTO')");
            Execute(vSql.ToString(), 0);
        }

        private void InsertarSiNoExisteCategoria() {
            QAdvSql InsSql = new QAdvSql("");
            string vFechaUltimaModificacion = InsSql.ToSqlValue(LibDate.Today());
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("INSERT INTO Saw.Categoria (ConsecutivoCompania, Consecutivo, Descripcion, NombreOperador, FechaUltimaModificacion)");
            vSql.AppendLine(" SELECT COMPANIA.ConsecutivoCompania, ISNULL((SELECT MAX(Categoria.Consecutivo) From Saw.Categoria), 0) + 1, 'CATEGORIA', 'JEFE', " + vFechaUltimaModificacion + " FROM COMPANIA");
            vSql.AppendLine(" WHERE NOT EXISTS(SELECT Categoria.ConsecutivoCompania, Categoria.Descripcion FROM Saw.Categoria WHERE Categoria.ConsecutivoCompania = COMPANIA.ConsecutivoCompania AND Categoria.Descripcion = 'CATEGORIA')");
            Execute(vSql.ToString(), 0);
        }

        private void InsertarSiNoExisteUnidadDeVenta() {
            QAdvSql InsSql = new QAdvSql("");
            string vFechaUltimaModificacion = InsSql.ToSqlValue(LibDate.Today());
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("INSERT INTO Saw.UnidadDeVenta (Nombre, Codigo, NombreOperador, FechaUltimaModificacion)");
            vSql.AppendLine(" SELECT 'UNIDADES','','JEFE', " + vFechaUltimaModificacion );
            vSql.AppendLine(" WHERE NOT EXISTS (SELECT Nombre FROM Saw.UnidadDeVenta WHERE Nombre = 'UNIDADES')");
            Execute(vSql.ToString(), 0);
        }

        private void CrearArticulosEspecialesIGTF_ML() {
            QAdvSql InsSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vFecha = InsSql.ToSqlValue(LibDate.Today());
            vSql.AppendLine("INSERT INTO dbo.articuloInventario(ConsecutivoCompania, Codigo, Descripcion, LineaDeProducto, StatusdelArticulo, TipoDeArticulo, AlicuotaIva, ");
            vSql.AppendLine("PrecioSinIva, PrecioConIva, PrecioSinIva2, PrecioConIva2, PrecioSinIva3, PrecioConIva3, PrecioSinIva4, PrecioConIva4, PorcentajeBaseImponible, ");
            vSql.AppendLine("CostoUnitario, Existencia, CantidadMinima, CantidadMaxima, Categoria, TipoDeProducto, NombrePrograma, Marca, FechaDeVencimiento, UnidadDeVenta, ");
            vSql.AppendLine("CampoDefinible1, CampoDefinible2, CampoDefinible3, CampoDefinible4, CampoDefinible5, MeCostoUnitario, UnidadDeVentaSecundaria, CuentaContableIngreso, ");
            vSql.AppendLine("ExcluirDeComision, CodigoLote, NombreOperador, FechaUltimaModificacion, TipoArticuloInv , CostoPromedio, RecalcularCierre , RecalcularCosto, ");
            vSql.AppendLine("FechaCierreActualizada , CuentaCostoDeVenta, CuentaInventario, ComisionaPorcentaje)");
            vSql.AppendLine("SELECT COMPANIA.ConsecutivoCompania , 'ND-NC IGTF @', " + InsSql.ToSqlValue("Ajuste del I.G.T.F. por cambios en la forma de pago.") + "," + InsSql.ToSqlValue("LINEA DE PRODUCTO"));
            vSql.AppendLine(", '0', '1', '0', 0, 0, 0, 0, 0, 0, 0, 0, 100, 0, 0, 0, 0, " + InsSql.ToSqlValue("CATEGORIA") + ", '0', '', '', " + vFecha + ", " + InsSql.ToSqlValue("UNIDADES"));
            vSql.AppendLine(", '', '', '', '', '', 0, 0, '','N', '0', '1', " + vFecha + ",'0', 0 , 'N', 'N', " + vFecha + ", '', '', 'S' FROM COMPANIA");
            vSql.AppendLine("WHERE NOT EXISTS(SELECT articuloInventario.ConsecutivoCompania, Codigo FROM dbo.articuloInventario WHERE articuloInventario.ConsecutivoCompania = COMPANIA.ConsecutivoCompania AND articuloInventario.Codigo = 'ND-NC IGTF @')");
            Execute(vSql.ToString(), 0);
        }
        private void CrearArticulosEspecialesIGTF_ME() {
            QAdvSql InsSql = new QAdvSql("");
            StringBuilder vSql = new StringBuilder();
            string vFecha = InsSql.ToSqlValue(LibDate.Today());
            vSql.AppendLine("INSERT INTO dbo.CamposMonedaExtranjera(ConsecutivoCompania, Codigo, MePrecioSinIva, MePrecioConIva, MePrecioSinIva2, MePrecioConIva2, ");
            vSql.AppendLine("MePrecioSinIva3, MePrecioConIva3, MePrecioSinIva4, MePrecioConIva4, NombreOperador, FechaUltimaModificacion)");
            vSql.AppendLine("SELECT ConsecutivoCompania , 'ND-NC IGTF @',0,0,0,0,0,0,0,0, " + InsSql.ToSqlValue("JEFE") + ", " + vFecha + "FROM COMPANIA");
            vSql.AppendLine(" WHERE NOT EXISTS (SELECT ConsecutivoCompania , Codigo FROM dbo.CamposMonedaExtranjera WHERE ");
            vSql.AppendLine(" ConsecutivoCompania = COMPANIA.ConsecutivoCompania AND Codigo = 'ND-NC IGTF @')");
            Execute(vSql.ToString(), 0);
        }

        private void AgregarParametroAsociarCentroDeCostos() {
            AgregarNuevoParametro("AsociarCentroDeCostos", "Inventario", 5, "5.1.- Inventario", 1, "", eTipoDeDatoParametros.Enumerativo, "", 'N', "0");
            StringBuilder vSql = new StringBuilder();
            vSql.AppendLine("DELETE Comun.SettValueByCompany ");
            vSql.AppendLine("WHERE NameSettDefinition = 'AsociaCentroDeCostoyAlmacen' ");
            Execute(vSql.ToString());
            vSql.Clear();
            vSql.AppendLine("DELETE Comun.SettDefinition ");
            vSql.AppendLine("WHERE Name = 'AsociaCentroDeCostoyAlmacen' ");
            Execute(vSql.ToString());
        }

        private void CrearCampoExcluirDelInformeDeDeclaracionIGTF() {
            QAdvSql InsSql = new QAdvSql("");
            if (AddColumnBoolean("Saw.CuentaBancaria", "ExcluirDelInformeDeDeclaracionIGTF", "", false)) {
                AddDefaultConstraint("Saw.CuentaBancaria", "d_CueBanExcluirDel", InsSql.ToSqlValue(false), "ExcluirDelInformeDeDeclaracionIGTF");
            }
        }

        private void CrearCampoRegistroTXTEnCajaRegistradora() {
            if (AddColumnBoolean("Adm.Caja", "RegistroDeRetornoEnTxt", "", false)) {
                AddDefaultConstraint("Adm.Caja", "d_RegRetEnTxt", InsSql.ToSqlValue(false), "RegistroDeRetornoEnTxt");
            }
        }
    }
}