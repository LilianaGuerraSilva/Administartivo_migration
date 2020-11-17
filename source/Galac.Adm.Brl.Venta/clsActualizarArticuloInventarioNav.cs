using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Galac.Adm.Ccl.Venta;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using Galac.Comun.Ccl.SttDef;
using System.Xml.Linq;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Adm.Brl.Venta {
    public class clsActualizarArticuloInventarioNav : IActualizarArticuloInventarioPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsActualizarArticuloInventarioNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #endregion Metodos Generados


        bool IActualizarArticuloInventarioPdn.DescontarExistencia(int valConsecutivoCompania, string valNumeroDeFactura, string valCodigoAlmacen, eTipoDocumentoFactura valTipodeDocumento, DateTime valFechaFactura) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valNumeroDeFactura, 11);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 11);
            vParams.AddInDateTime("Fecha", valFechaFactura);
            vParams.AddInEnum("TipoDeDocumento", (int)valTipodeDocumento);
            vParams.AddInEnum("StatusFactura", (int)eStatusFactura.Emitida);
            vParams.AddInEnum("TipoDeArticulo", (int)eTipoDeArticulo.ProductoCompuesto);

            vSql.AppendLine(" UPDATE ");
            vSql.AppendLine(" ArticuloInventario ");
            vSql.AppendLine(" SET ");
            vSql.AppendLine(" Existencia=Existencia-Cantidad ");
            vSql.AppendLine(" FROM");
            vSql.AppendLine(" ArticuloInventario ");
            vSql.AppendLine(" INNER JOIN ");
            vSql.AppendLine(" renglonFactura ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND ");
            vSql.AppendLine(" renglonFactura.Articulo = ArticuloInventario.Codigo ");
            vSql.AppendLine(" INNER JOIN ");
            vSql.AppendLine(" factura ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND ");
            vSql.AppendLine(" renglonFactura.NumeroFactura = factura.Numero AND ");
            vSql.AppendLine(" renglonFactura.TipoDeDocumento = factura.TipoDeDocumento ");
            vSql.AppendLine(" WHERE ");
            vSql.AppendLine("ArticuloInventario.ConsecutivoCompania = @ConsecutivoCompania AND ");
            vSql.AppendLine("RenglonFactura.NumeroFactura=@NumeroFactura AND ");
            vSql.AppendLine("factura.CodigoAlmacen = @CodigoAlmacen AND ");
            vSql.AppendLine("factura.Fecha=@Fecha AND ");
            vSql.AppendLine("factura.TipoDeDocumento=@TipoDeDocumento AND ");
            vSql.AppendLine("factura.StatusFactura=@StatusFactura ");
            vSql.AppendLine(" AND ArticuloInventario.TipoDeArticulo<>@TipoDeArticulo ");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        bool IActualizarArticuloInventarioPdn.DescontarEnAlmacen(int valConsecutivoCompania, string valNumeroFactura, string valCodigoAlmacen, eTipoDocumentoFactura valTipodeDocumento, DateTime valFechaFactura) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valNumeroFactura, 11);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 11);
            vParams.AddInDateTime("Fecha", valFechaFactura);
            vParams.AddInEnum("TipoDeDocumento", (int)valTipodeDocumento);
            vParams.AddInEnum("StatusFactura", (int)eStatusFactura.Emitida);

            vSql.AppendLine(" UPDATE");
            vSql.AppendLine(" ExistenciaPorAlmacen ");
            vSql.AppendLine(" SET");
            vSql.AppendLine(" ExistenciaPorAlmacen.Cantidad=ExistenciaPorAlmacen.Cantidad- renglonFactura.Cantidad ");
            vSql.AppendLine(" FROM ");
            vSql.AppendLine(" ExistenciaPorAlmacen ");
            vSql.AppendLine(" INNER JOIN ");
            vSql.AppendLine(" renglonFactura ON renglonFactura.ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania AND ");
            vSql.AppendLine(" renglonFactura.Articulo = ExistenciaPorAlmacen.CodigoArticulo ");
            vSql.AppendLine(" INNER JOIN ");
            vSql.AppendLine(" factura ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND ");
            vSql.AppendLine(" renglonFactura.NumeroFactura = factura.Numero AND ");
            vSql.AppendLine(" renglonFactura.TipoDeDocumento = factura.TipoDeDocumento AND ");
            vSql.AppendLine(" factura.CodigoAlmacen = ExistenciaPorAlmacen.CodigoAlmacen ");
            vSql.AppendLine(" WHERE ");
            vSql.AppendLine(" ExistenciaPorAlmacen.ConsecutivoCompania = @ConsecutivoCompania AND ");
            vSql.AppendLine(" RenglonFactura.NumeroFactura=@NumeroFactura AND ");
            vSql.AppendLine(" factura.CodigoAlmacen = @CodigoAlmacen AND ");
            vSql.AppendLine(" factura.Fecha=@Fecha AND ");
            vSql.AppendLine(" factura.TipoDeDocumento=@TipoDeDocumento AND ");
            vSql.AppendLine(" factura.StatusFactura=@StatusFactura ");
            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        bool IActualizarArticuloInventarioPdn.DescontarExistenciaProductoCompuesto(int valConsecutivoCompania, string valNumeroDeFactura, string valCodigoAlmacen, eTipoDocumentoFactura valTipodeDocumento, DateTime valFechaFactura) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valNumeroDeFactura, 11);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 11);
            vParams.AddInDateTime("Fecha", valFechaFactura);
            vParams.AddInEnum("TipoDeDocumento", (int)valTipodeDocumento);
            vParams.AddInEnum("StatusFactura", (int)eStatusFactura.Emitida);

            vSql.AppendLine(" UPDATE AI2 ");
            vSql.AppendLine(" SET AI2.Existencia = AI2.Existencia - (RenglonFactura.Cantidad * ProductoCompuesto.Cantidad) ");
            vSql.AppendLine(" FROM ArticuloInventario AI1 ");
            vSql.AppendLine(" INNER JOIN ProductoCompuesto ");
            vSql.AppendLine(" ON (AI1.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania");
            vSql.AppendLine(" AND AI1.Codigo = ProductoCompuesto.CodigoConexionConElMaster)");
            vSql.AppendLine(" INNER JOIN ArticuloInventario AI2 ");
            vSql.AppendLine(" ON (AI2.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania");
            vSql.AppendLine(" AND AI2.Codigo = ProductoCompuesto.CodigoArticulo)");
            vSql.AppendLine(" INNER JOIN RenglonFactura ");
            vSql.AppendLine(" ON RenglonFactura.ConsecutivoCompania = AI1.ConsecutivoCompania ");
            vSql.AppendLine(" AND RenglonFactura.Articulo = AI1.Codigo ");
            vSql.AppendLine(" INNER JOIN Factura ");
            vSql.AppendLine(" ON RenglonFactura.ConsecutivoCompania = Factura.ConsecutivoCompania ");
            vSql.AppendLine(" AND RenglonFactura.NumeroFactura = Factura.Numero ");
            vSql.AppendLine(" AND RenglonFactura.TipoDeDocumento = Factura.TipoDeDocumento ");
            vSql.AppendLine(" WHERE AI1.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine(" AND RenglonFactura.NumeroFactura = @NumeroFactura ");
            vSql.AppendLine(" AND Factura.CodigoAlmacen = @CodigoAlmacen");
            vSql.AppendLine(" AND Factura.Fecha = @Fecha");
            vSql.AppendLine(" AND Factura.TipoDeDocumento = @TipoDeDocumento");
            vSql.AppendLine(" AND Factura.StatusFactura=@StatusFactura ");

            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        bool IActualizarArticuloInventarioPdn.DescontarEnAlmacenProductoCompuesto(int valConsecutivoCompania, string valNumeroFactura, string valCodigoAlmacen, eTipoDocumentoFactura valTipodeDocumento, DateTime valFechaFactura) {
            bool vResult = false;
            LibGpParams vParams = new LibGpParams();
            StringBuilder vSql = new StringBuilder();

            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valNumeroFactura, 11);
            vParams.AddInString("CodigoAlmacen", valCodigoAlmacen, 11);
            vParams.AddInDateTime("Fecha", valFechaFactura);
            vParams.AddInEnum("TipoDeDocumento", (int)valTipodeDocumento);
            vParams.AddInEnum("StatusFactura", (int)eStatusFactura.Emitida);

            vSql.AppendLine(" UPDATE");
            vSql.AppendLine(" ExistenciaPorAlmacen ");
            vSql.AppendLine(" SET");
            vSql.AppendLine(" ExistenciaPorAlmacen.Cantidad=ExistenciaPorAlmacen.Cantidad - (RenglonFactura.Cantidad * ProductoCompuesto.Cantidad) ");
            vSql.AppendLine(" FROM RenglonFactura ");
            vSql.AppendLine(" INNER JOIN ProductoCompuesto ON (RenglonFactura.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania");
            vSql.AppendLine(" AND RenglonFactura.Articulo = ProductoCompuesto.CodigoConexionConElMaster)");
            vSql.AppendLine(" INNER JOIN ArticuloInventario AI1  ");
            vSql.AppendLine(" ON (AI1.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania");
            vSql.AppendLine(" AND AI1.Codigo = ProductoCompuesto.CodigoArticulo)");
            vSql.AppendLine(" INNER JOIN ExistenciaPorAlmacen");
            vSql.AppendLine(" ON (ExistenciaPorAlmacen.ConsecutivoCompania = AI1.ConsecutivoCompania");
            vSql.AppendLine(" AND ExistenciaPorAlmacen.CodigoArticulo = AI1.Codigo)");
            vSql.AppendLine(" INNER JOIN Factura ");
            vSql.AppendLine(" ON RenglonFactura.ConsecutivoCompania = Factura.ConsecutivoCompania ");
            vSql.AppendLine(" AND RenglonFactura.NumeroFactura = Factura.Numero ");
            vSql.AppendLine(" AND RenglonFactura.TipoDeDocumento = Factura.TipoDeDocumento ");
            vSql.AppendLine(" WHERE ExistenciaPorAlmacen.ConsecutivoCompania = @ConsecutivoCompania");
            vSql.AppendLine(" AND RenglonFactura.NumeroFactura=@NumeroFactura ");
            vSql.AppendLine(" AND Factura.CodigoAlmacen = @CodigoAlmacen");
            vSql.AppendLine(" AND Factura.Fecha=@Fecha ");
            vSql.AppendLine(" AND Factura.TipoDeDocumento=@TipoDeDocumento ");
            vSql.AppendLine(" AND Factura.StatusFactura=@StatusFactura ");

            vResult = LibBusiness.ExecuteUpdateOrDelete(vSql.ToString(), vParams.Get(), "", 0) >= 0;
            return vResult;
        }

        XElement IActualizarArticuloInventarioPdn.ConjuntoProductosCompuestos(int valConsecutivoCompania, string valNumeroFactura, eTipoDocumentoFactura valTipoDeDocumento) {
            StringBuilder Sql = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("NumeroFactura", valNumeroFactura, 11);
            vParams.AddInEnum("TipoDeDocumento", (int)valTipoDeDocumento);

            Sql.AppendLine("SELECT  (CASE WHEN RenglonFactura.Articulo = ProductoCompuesto.CodigoConexionConElMaster THEN ProductoCompuesto.CodigoArticulo ");
            Sql.AppendLine(" ELSE RenglonFactura.articulo END ) AS ArticuloFinal");
            Sql.AppendLine(", SUM( CASE WHEN RenglonFactura.Articulo = ProductoCompuesto.CodigoConexionConElMaster ");
            Sql.AppendLine(" THEN (RenglonFactura.Cantidad * ProductoCompuesto.cantidad) ELSE RenglonFactura.cantidad END  ) AS CantidadFinal");
            Sql.AppendLine(", ( CASE WHEN  (RenglonFactura.Serial IS NULL )  THEN '0' ELSE RenglonFactura.Serial END   ) as Serial");
            Sql.AppendLine(", ( CASE WHEN  (RenglonFactura.Rollo IS NULL )  THEN '0' ELSE RenglonFactura.Rollo END   ) as Rollo ");
            Sql.AppendLine(" FROM RenglonFactura LEFT JOIN ProductoCompuesto  ");
            Sql.AppendLine(" ON (RenglonFactura.Articulo = ProductoCompuesto.CodigoConexionConElMaster ");
            Sql.AppendLine(" AND RenglonFactura.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania) ");
            Sql.AppendLine(" WHERE RenglonFactura.ConsecutivoCompania = @ConsecutivoCompania ");
            Sql.AppendLine(" AND RenglonFactura.NumeroFactura = @NumeroFactura");
            Sql.AppendLine(" AND tipodedocumento = @TipoDeDocumento ");
            Sql.AppendLine(" GROUP BY RenglonFactura.Articulo, ProductoCompuesto.CodigoConexionConElMaster,ProductoCompuesto.CodigoArticulo");
            Sql.AppendLine(" , RenglonFactura.Cantidad, ProductoCompuesto.cantidad,RenglonFactura.Serial, RenglonFactura.Rollo ");
            Sql.AppendLine(" ORDER BY RenglonFactura.Articulo");

            return LibBusiness.ExecuteSelect(Sql.ToString(), vParams.Get(), "", -1);
        }



    }
}
