using System.Text;
using System.Data;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.SttDef;
using System.Threading;
using LibGalac.Aos.Dal;
using System;
using LibGalac.Aos.Base.Dal;

namespace Galac.Saw.DDL.VersionesReestructuracion {
    class clsVersionTemporalNoOficial :clsVersionARestructurar {
        public clsVersionTemporalNoOficial(string valCurrentDataBaseName) : base(valCurrentDataBaseName) { }
        public override bool UpdateToVersion() {
            StartConnectionNoTransaction();
			//CrearTablaTransferenciaEntreCuentasBancarias(); Nota: se Oculta temporalmente
			//AgregaColumnasReglasDeContabilizacion();
			InsertarSiNoExisteLineaDeProducto();
			CrearArticulosEspecialesIGTF_ML();
			CrearArticulosEspecialesIGTF_ME();
			AgregarParametroAsociarCentroDeCostos();
            DisposeConnectionNoTransaction();
            return true;
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

		private void CrearTablaTransferenciaEntreCuentasBancarias() {
			if (!TableExists("Adm.TransferenciaEntreCuentasBancarias")) {
				new Galac.Adm.Dal.Banco.clsTransferenciaEntreCuentasBancariasED().InstalarTabla();
			}
		}
		private void AgregaColumnasReglasDeContabilizacion() {
			if (!ColumnExists("Saw.ReglasDeContabilizacion", "TipoContabilizacionTransfCtas")) {
				if( AddColumnEnumerative("Saw.ReglasDeContabilizacion", "TipoContabilizacionTransfCtas", "", LibConvert.EnumToDbValue(0))) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConTiCoTrCt", "'0'", "TipoContabilizacionTransfCtas");
                }
				if (AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabIndividualTransfCtas", "", LibConvert.EnumToDbValue(1))){
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCoInTrCt", "'1'", "ContabIndividualTransfCtas");
				}
				if (AddColumnEnumerative("Saw.ReglasDeContabilizacion", "ContabPorLoteTransfCtas", "", LibConvert.EnumToDbValue(0))) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtBaDe", "'0'", "ContabPorLoteTransfCtas");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasBancoDestino", 30, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCoPoLoTrCt", _insSql.ToSqlValue(""), "CuentaTransfCtasBancoDestino");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasGastoComOrigen", 30, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtGaCoOr", _insSql.ToSqlValue(""), "CuentaTransfCtasGastoComOrigen");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasGastoComDestino", 30, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtGaCoDe", _insSql.ToSqlValue(""), "CuentaTransfCtasGastoComDestino");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "CuentaTransfCtasBancoOrigen", 30, "", "")){
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConCuTrCtBaOr", _insSql.ToSqlValue(""), "CuentaTransfCtasBancoOrigen");
				}
				if (AddColumnString("Saw.ReglasDeContabilizacion", "TransfCtasSigasTipoComprobante", 2, "", "")) {
					AddDefaultConstraint("Saw.ReglasDeContabilizacion", "d_RegDeConTrCtSiTiCo", _insSql.ToSqlValue(""), "TransfCtasSigasTipoComprobante");
				}
				if (AddColumnBoolean("Saw.ReglasDeContabilizacion", "EditarComprobanteAfterInsertTransfCtas", "", false)) {
					AddNotNullConstraint("Saw.ReglasDeContabilizacion", "EditarComprobanteAfterInsertTransfCtas", InsSql.CharTypeForDb(1)) ;
				}
			}
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
	}
}
