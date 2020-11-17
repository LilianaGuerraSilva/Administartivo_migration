using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using System.Threading;

namespace Galac.Saw.Dal.Tablas {
    public class clsLineaDeProductoMD {

        public void MigrarLineaDeProducto() {
            QAdvSql insSql = new QAdvSql("");
            LibDatabase insDb = new LibDatabase();
            StringBuilder vSql = new StringBuilder();
            vSql.Append(" INSERT INTO Adm.LineaDeProducto (ConsecutivoCompania, Consecutivo ,"); 
            vSql.Append(" Nombre ,PorcentajeComision ,CentroDeCosto)");
            vSql.Append(" SELECT ConsecutivoCompania,ROW_NUMBER() OVER (PARTITION BY ConsecutivoCompania ORDER BY ConsecutivoCompania) AS Consecutivo,");
            vSql.Append(" Nombre, PorcentajeComision , CentroDeCosto");
            vSql.Append(" FROM dbo.LineaDeProducto ");
            try {
                insDb.Execute(vSql.ToString());
            } catch (Exception) {
                throw;
            }
        }
        public bool CrearVistaDboLineaDeProducto() {
            bool vResult = false;
            StringBuilder vSqlCreateView = new StringBuilder();
            try {
                vSqlCreateView.AppendLine(" SELECT ConsecutivoCompania, Consecutivo, Nombre,PorcentajeComision As ProcentajeDeComisión");
                vSqlCreateView.AppendLine(" ,CentroDeCosto");
                vSqlCreateView.AppendLine(" FROM Adm.LineaDeProducto ");
                vResult = LibViews.CreateCompatibilityView("LineaDeProducto", vSqlCreateView.ToString(), true);
            } catch (Exception) {
                throw;
            }
            return vResult;
        }
        public void EliminarTablaDboLineaDeProducto() {

               LibDatabase insDb = new LibDatabase();
               StringBuilder vSql = new StringBuilder();
               vSql.Append("DROP TABLE dbo.LineaDeProducto ");
               try {
                   insDb.Execute(vSql.ToString());
               } catch (Exception) {
                   throw;
               }
           }
        }
    } //End of class clsLineaDeProducto

//      public void MigrarMonedaLocal() {
