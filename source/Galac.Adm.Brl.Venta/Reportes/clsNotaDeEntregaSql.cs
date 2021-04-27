using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using Galac.Comun.Ccl.TablasGen;
using Galac.Comun.Brl.TablasGen;
using LibGalac.Aos.DefGen;

namespace Galac.Adm.Brl.Venta.Reportes {
    public class clsNotaDeEntregaSql {
        #region Metodos Generados
		public string SqlNotaDeEntregaEntreFechasPorCliente(int valConsecutivoCompania,
															DateTime valFechaDesde,
															DateTime valFechaHasta,
															bool valIncluirNotasDeEntregasAnuladas,
															Galac.Saw.Lib.eCantidadAImprimir valCantidadAImprimir,
															Galac.Saw.Lib.eMonedaParaImpresion valMonedaDelReporte,
															string valCodigoCliente) {
			QAdvSql vUtilSql = new QAdvSql("");
			StringBuilder vSql = new StringBuilder();
			IMonedaLocalActual vMonedaLocalActual = new clsMonedaLocalActual();
			vMonedaLocalActual.CargarTodasEnMemoriaYAsignarValoresDeLaActual(LibDefGen.ProgramInfo.Country, LibDate.Today());
			string vSQLWhereBetweenDates = string.Empty;
			vSQLWhereBetweenDates = vUtilSql.SqlDateValueBetween(vSQLWhereBetweenDates, "[F].[Fecha]", valFechaDesde, valFechaHasta);
			vSql.AppendLine($"{vUtilSql.SetDateFormat()}");
			vSql.AppendLine("SET NOCOUNT ON");
			vSql.AppendLine("SELECT	[F].[CodigoCliente]			AS CodigoCliente,");
			vSql.AppendLine("		[C].[Nombre]				AS Cliente,");
			vSql.AppendLine("       [F].[Fecha]					AS Fecha,");
			vSql.AppendLine("       [F].[Numero]				AS Numero,");
            if (valMonedaDelReporte != Saw.Lib.eMonedaParaImpresion.EnMonedaOriginal) {
				string vMonedaLocal = vMonedaLocalActual.GetHoyNombreMoneda();
				vSql.AppendLine($"   {vUtilSql.ToSqlValue(vMonedaLocal)} AS Moneda,");
			} else {
				vSql.AppendLine("   [F].[Moneda] AS Moneda,");
			}
			vSql.AppendLine("       [F].[CambioABolivares]		AS Cambio,");
			if (valMonedaDelReporte == Saw.Lib.eMonedaParaImpresion.EnBolivares) {
				vSql.AppendLine("   [F].[TotalFactura] * [F].[CambioABolivares] AS TotalFactura");
			} else {
				vSql.AppendLine("   [F].[TotalFactura] AS TotalFactura");
			}
			vSql.AppendLine("FROM [dbo].[factura]			AS [F]");
			vSql.AppendLine("INNER JOIN [dbo].[Cliente]		AS [C] ON [C].[Codigo] = [F].[CodigoCliente] AND [C].[ConsecutivoCompania] = [F].[ConsecutivoCompania]");
			vSql.AppendLine("WHERE [F].[TipoDeDocumento] = 8");
			vSql.AppendLine($"AND {vSQLWhereBetweenDates}");
            if (valCantidadAImprimir == Galac.Saw.Lib.eCantidadAImprimir.Uno) {
				vSql.AppendLine($"AND [F].[CodigoCliente] = {vUtilSql.ToSqlValue(valCodigoCliente)}");
			}	
            if (valIncluirNotasDeEntregasAnuladas) {
				vSql.AppendLine("AND ([F].[StatusFactura] = 0 OR [F].[StatusFactura] = 1)");
			} else {
				vSql.AppendLine("AND [F].[StatusFactura] = 0");
			}
			vSql.AppendLine($"AND [F].[ConsecutivoCompania] = {valConsecutivoCompania}");
			vSql.AppendLine("ORDER BY    [C].[Nombre],");
			vSql.AppendLine("            [F].[Moneda],");
			vSql.AppendLine("            [F].[Fecha]");
			return vSql.ToString();
		}
        #endregion //Metodos Generados


    } //End of class clsNotaDeEntregaSql

} //End of namespace Galac..Brl.ComponenteNoEspecificado

