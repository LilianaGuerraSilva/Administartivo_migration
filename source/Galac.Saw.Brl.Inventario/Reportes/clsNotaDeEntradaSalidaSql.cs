using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;

namespace Galac.Saw.Brl.Inventario.Reportes {
    public class clsNotaDeEntradaSalidaSql {
		QAdvSql insSql;

        public clsNotaDeEntradaSalidaSql() {
            insSql = new QAdvSql("");
        }
        #region Metodos Generados
        public string SqlNotaDeEntradaSalidaDeInventario(int valConsecutivoCompania, string valNumeroDocumento){
            StringBuilder vSql = new StringBuilder();
			vSql.AppendLine("SELECT ");
			vSql.AppendLine("	NES.NumeroDocumento,");
			vSql.AppendLine("	NES.Fecha,");
			vSql.AppendLine("	NES.TipodeOperacion,");
			vSql.AppendLine("	(CASE NES.TipodeOperacion ");
			vSql.AppendLine("		WHEN '3' THEN 'Retiro' ");
			vSql.AppendLine("		WHEN '2' THEN 'Autoconsumo' ");
			vSql.AppendLine("		WHEN '1' THEN 'Salida de Inventario' ");
			vSql.AppendLine("		ELSE 'Entrada de Inventario' END) AS TipodeOperacionStr,");
			vSql.AppendLine("	NES.StatusNotaEntradaSalida,");
			vSql.AppendLine("	(CASE NES.StatusNotaEntradaSalida ");
			vSql.AppendLine("		WHEN '1' THEN 'Anulada' ");
			vSql.AppendLine("		ELSE 'Vigente' END) AS StatusNotaEntradaSalidaStr,");
			vSql.AppendLine("	NES.CodigoCliente + ' - ' + Cli.Nombre AS CodigoNombreCliente,");
			vSql.AppendLine("	NES.CodigoAlmacen + ' - ' + Alm.NombreAlmacen AS CodigoNombreAlmacen,");
			vSql.AppendLine("	NES.Comentarios,");
			vSql.AppendLine("	RNES.CodigoArticulo,");
			vSql.AppendLine("	AI.Descripcion AS DescripcionArticulo,");
			vSql.AppendLine("	RNES.Cantidad,");
			vSql.AppendLine("	AI.TipoArticuloInv,");
			vSql.AppendLine("	ISNULL(RNES.LoteDeInventario, '') AS LoteDeInventario,");
			vSql.AppendLine("	ISNULL(LI.FechaDeElaboracion, '') AS FechaDeElaboracion,");
			vSql.AppendLine("	ISNULL(LI.FechaDeVencimiento, '') AS FechaDeVencimiento");
			vSql.AppendLine("FROM NotaDeEntradaSalida NES INNER JOIN RenglonNotaES RNES ON NES.ConsecutivoCompania = RNES.ConsecutivoCompania AND NES.NumeroDocumento = RNES.NumeroDocumento");
			vSql.AppendLine("	INNER JOIN ArticuloInventario AI ON RNES.ConsecutivoCompania = AI.ConsecutivoCompania AND RNES.CodigoArticulo = AI.Codigo");
			vSql.AppendLine("	INNER JOIN Saw.Almacen Alm ON NES.ConsecutivoCompania = Alm.ConsecutivoCompania AND NES.ConsecutivoAlmacen = Alm.Consecutivo");
			vSql.AppendLine("	INNER JOIN Cliente Cli ON NES.ConsecutivoCompania = Cli.ConsecutivoCompania AND NES.CodigoCliente = Cli.Codigo");
			vSql.AppendLine("	LEFT JOIN Saw.LoteDeInventario LI ON RNES.ConsecutivoCompania = LI.ConsecutivoCompania AND RNES.CodigoArticulo = LI.CodigoArticulo AND RNES.LoteDeInventario = LI.CodigoLote");
			vSql.AppendLine("WHERE ");
			vSql.AppendLine("	NES.ConsecutivoCompania = " + insSql.ToSqlValue(valConsecutivoCompania));
			vSql.AppendLine("	AND NES.NumeroDocumento = " + insSql.ToSqlValue(valNumeroDocumento));
			vSql.AppendLine("ORDER BY RNES.ConsecutivoRenglon");
			return vSql.ToString();
		}
		#endregion //Metodos Generados


	} //End of class clsNotaDeEntradaSalidaSql

} //End of namespace Galac.Saw.Brl.Inventario

