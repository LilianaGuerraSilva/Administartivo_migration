using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using Galac.Adm.Ccl.Venta;
using System;

namespace Galac.Adm.Brl.Venta.Reportes {
	public class clsContratoSql {
		#region Metodos Generados
		public string SqlContratoPorNumero(int valConsecutivoCompania, string valNumeroContrato) {
			StringBuilder vSql = new StringBuilder();
			QAdvSql vUtilSql = new QAdvSql("");

			StringBuilder vSQLWhere = new StringBuilder();

			vSQLWhere.AppendLine("       Contrato.ConsecutivoCompania = " + vUtilSql.ToSqlValue(valConsecutivoCompania));
			vSQLWhere.AppendLine("            AND");
			vSQLWhere.AppendLine("             Contrato.NumeroContrato = " + vUtilSql.ToSqlValue(valNumeroContrato));

			vSql.AppendLine("SELECT Contrato.ConsecutivoCompania,");
			vSql.AppendLine("       Contrato.NumeroContrato,");
			vSql.AppendLine(vUtilSql.IIF("Contrato.StatusContrato = " + vUtilSql.EnumToSqlValue((int)eStatusContrato.Vigente), vUtilSql.ToSqlValue("Vigente"), vUtilSql.ToSqlValue("Desactivado"), false) + " AS StatusContrato,");
			vSql.AppendLine("       Contrato.CodigoCliente,");
			vSql.AppendLine("       Cliente.Nombre,");
			vSql.AppendLine(vUtilSql.IIF("Contrato.DuracionDelContrato = " + vUtilSql.EnumToSqlValue((int)eDuracionDelContrato.DuracionFija), vUtilSql.ToSqlValue("Duración Fija"), vUtilSql.ToSqlValue("Duración indefinida"), false) + " AS Duracion,");
			vSql.AppendLine("	    Contrato.FechaDeInicio,");
			vSql.AppendLine("	    Contrato.FechaFinal,");
			vSql.AppendLine("       Contrato.Observaciones,");
			vSql.AppendLine("       Contrato.Moneda,");
			vSql.AppendLine("       Contrato.CodigoVendedor,");
			vSql.AppendLine("       Contrato.NombreVendedor,");
			vSql.AppendLine("       Contrato.FacturarAOtroCliente,");
			vSql.AppendLine("       Contrato.CodigoClienteAFacturar,");
			vSql.AppendLine("       Contrato.NombreClienteAFacturar,");
			vSql.AppendLine("       Contrato.ContratoFueModificado,");
			vSql.AppendLine("       ContratoFueModificadoPor,");
			vSql.AppendLine("       RenglonContrato.articulo,");
			vSql.AppendLine("       RenglonContrato.descripcion,");
			vSql.AppendLine(vUtilSql.IIF("RenglonContrato.CambioDeValorDelItem = " + vUtilSql.EnumToSqlValue((int)eValorDelRenglon.ValorenArchivoArticulos), " ArticuloInventario.PrecioSinIva ", " RenglonContrato.Imponible ", false) + " AS Imponible,");
			vSql.AppendLine("       RenglonContrato.PorcentajeDescuento,");
			vSql.AppendLine("       RenglonContrato.Cantidad,");
			vSql.AppendLine("       ((RenglonContrato.Cantidad * ( ");
			vSql.AppendLine(vUtilSql.IIF("RenglonContrato.CambioDeValorDelItem = " + vUtilSql.EnumToSqlValue((int)eValorDelRenglon.ValorenArchivoArticulos), " ArticuloInventario.PrecioSinIva ", " RenglonContrato.Imponible ", false) + " ) ");
			vSql.AppendLine("       - ((RenglonContrato.Cantidad * ( ");
			vSql.AppendLine(vUtilSql.IIF("RenglonContrato.CambioDeValorDelItem = " + vUtilSql.EnumToSqlValue((int)eValorDelRenglon.ValorenArchivoArticulos), " ArticuloInventario.PrecioSinIva ", " RenglonContrato.Imponible ", false) + " ) ");
			vSql.AppendLine("       * RenglonContrato.PorcentajeDescuento) / 100))) AS TotalRenglon");
			vSql.AppendLine("FROM Contrato");
			vSql.AppendLine("INNER JOIN Cliente ON Contrato.ConsecutivoCompania = Cliente.consecutivoCompania");
			vSql.AppendLine("       AND");
			vSql.AppendLine("Contrato.CodigoCliente = Cliente.Codigo");
			vSql.AppendLine("INNER JOIN RenglonContrato ON Contrato.ConsecutivoCompania = RenglonContrato.consecutivoCompania");
			vSql.AppendLine("       AND");
			vSql.AppendLine("Contrato.NumeroContrato = RenglonContrato.NumeroContrato");
			vSql.AppendLine("INNER JOIN ArticuloInventario ON ArticuloInventario.ConsecutivoCompania = RenglonContrato.consecutivoCompania");
			vSql.AppendLine("       AND");
			vSql.AppendLine("ArticuloInventario.Codigo = RenglonContrato.Articulo");

			vSql.AppendLine(" WHERE " + vSQLWhere);
			
			return vSql.ToString();
		}

		public string SqlContratoEntreFechas(int valConsecutivoCompania, bool valFiltrarPorStatus, bool valFiltrarPorFechaFinal, DateTime valFechaInicio, DateTime valFechaFinal, eStatusContrato valStatusContrato) {
			StringBuilder vSql = new StringBuilder();
			QAdvSql vUtilSql = new QAdvSql("");

			string vSQLWhere = "";
			vSQLWhere = new QAdvSql("").SqlIntValueWithAnd(vSQLWhere, "dbo.Gv_Contrato_B1.ConsecutivoCompania", valConsecutivoCompania);

			vSql.AppendLine("SELECT Contrato.ConsecutivoCompania,");
			vSql.AppendLine("	    Contrato.NumeroContrato,");
			vSql.AppendLine(vUtilSql.IIF("Contrato.StatusContrato = " + vUtilSql.EnumToSqlValue((int)eStatusContrato.Vigente), vUtilSql.ToSqlValue("Vigente"), vUtilSql.ToSqlValue("Desactivado"), false) + " AS Status,");
			vSql.AppendLine("	    Contrato.CodigoCliente,");
			vSql.AppendLine("	    Cliente.Nombre AS NombreCliente,");
			vSql.AppendLine(vUtilSql.IIF("Contrato.DuracionDelContrato = " + vUtilSql.EnumToSqlValue((int)eDuracionDelContrato.DuracionFija), vUtilSql.ToSqlValue("Duración Fija"), vUtilSql.ToSqlValue("Duración indefinida"), false) + " AS Duracion,");
			vSql.AppendLine("       CONVERT(NVARCHAR, Contrato.FechaDeInicio, 3) AS FechaDeInicio,");
			vSql.AppendLine("       CONVERT(NVARCHAR, Contrato.FechaFinal, 3) AS FechaFinal,");
			vSql.AppendLine("	    Contrato.Observaciones,");
			vSql.AppendLine("	    Contrato.CodigoVendedor,");
			vSql.AppendLine("	    Contrato.NombreVendedor,");
			vSql.AppendLine("	    Contrato.FacturarAOtroCliente,");
			vSql.AppendLine("	    Contrato.CodigoClienteAFacturar,");
			vSql.AppendLine("	    Contrato.NombreClienteAFacturar,");
			vSql.AppendLine("	    Contrato.ContratoFueModificado,");
			vSql.AppendLine("	    ContratoFueModificadoPor");
			vSql.AppendLine("FROM Contrato");
			vSql.AppendLine("INNER JOIN Cliente ON Contrato.ConsecutivoCompania = Cliente.ConsecutivoCompania");
			vSql.AppendLine(" AND		   Contrato.CodigoCliente = Cliente.Codigo");
			vSql.AppendLine("		   WHERE Contrato.ConsecutivoCompania = " + vUtilSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("		   AND   Contrato.FechaDeInicio >= " + vUtilSql.ToSqlValue(valFechaInicio));
			if (valFiltrarPorFechaFinal) {
				vSql.AppendLine("		   AND   Contrato.FechaFinal <= " + vUtilSql.ToSqlValue(valFechaFinal));
			}
			if (valFiltrarPorStatus) {
				vSql.AppendLine("		   AND   Contrato.StatusContrato = " + vUtilSql.EnumToSqlValue((int)valStatusContrato));
			}
			vSql.AppendLine("ORDER BY Contrato.StatusContrato,");
			vSql.AppendLine("         Contrato.DuracionDelContrato,");
			vSql.AppendLine("		 Contrato.NumeroContrato");
			return vSql.ToString();
		}
		#endregion //Metodos Generados

	} //End of class clsContratoSql

} //End of namespace Galac.Adm.Brl.Venta

