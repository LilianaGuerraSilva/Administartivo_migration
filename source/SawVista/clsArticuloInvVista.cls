VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsArticuloInvVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsArticuloInvVista"
Private Const CM_MESSAGE_NAME As String = "Vista Artículo Inv"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetViewNameDisponibilidadXArticulo() As String
   GetViewNameDisponibilidadXArticulo = "IGV_DisponibilidadXArticulo"
End Function

Public Function fSqlViewDisponibilidadXArticulo() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   vSQL = "SELECT articuloInventario.ConsecutivoCompania, articuloInventario.Codigo, articuloInventario.Descripcion, "
   vSQL = " articuloInventario.LineaDeProducto, articuloInventario.StatusdelArticulo, articuloInventario.Existencia, "
   vSQL = " articuloInventario.CantArtReservado, articuloInventario.Existencia - articuloInventario.CantArtReservado AS Disponibilidad, "
   vSQL = " ExistenciaPorGrupo.Serial, ExistenciaPorGrupo.Rollo, ExistenciaPorGrupo.Existencia AS ExistenciaXRollo, ExistenciaPorGrupo.CantReservada, "
   vSQL = " ExistenciaPorGrupo.Existencia - ExistenciaPorGrupo.CantReservada AS DisponibilidaPorExistencia "
   vSQL = " FROM SAW.Talla RIGHT OUTER JOIN"
   vSQL = " ExistenciaPorGrupo ON Talla.CodigoTalla = ExistenciaPorGrupo.CodigoTalla AND"
   vSQL = " Talla.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania LEFT OUTER JOIN"
   vSQL = " Color ON ExistenciaPorGrupo.CodigoColor = Color.CodigoColor AND"
   vSQL = " ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania RIGHT OUTER JOIN"
   vSQL = " articuloInventario ON ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND"
   vSQL = " ExistenciaPorGrupo.CodigoArticulo = articuloInventario.Codigo"
   vSQL = " WHERE     (articuloInventario.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusArticulo.eSA_VIGENTE) & ")"
   fSqlViewDisponibilidadXArticulo = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewDisponibilidadXArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCompraDeArticulo() As String
   GetViewNameCompraDeArticulo = "IGV_ArticuloCompra_B1"
End Function

Public Function fSqlViewCompraDeArticulo() As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
   vSQLWhere = ""
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "compra.StatusCompra", enum_StatusCompra.eSCp_VIGENTE)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
       
   vSQL = "SELECT "
   vSQL = vSQL & "compra.ConsecutivoCompania, "
   vSQL = vSQL & "compra.Fecha, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("Compra") & " AS TipoDeOperacion, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("Compra") & " AS TipoDeDocumento, "
   vSQL = vSQL & "compra.Numero AS NumeroDeDocumento, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & gUtilSQL.fCast("CompraDetalleArticuloInventario.CantidadRecibida", eTDSS_MONEY, "Entradas") & ", "
   vSQL = vSQL & " 0 AS Salidas , "
   vSQL = vSQL & "CompraDetalleArticuloInventario.Consecutivo AS ConsecutivoRenglon "
   vSQL = vSQL & " FROM "
   vSQL = vSQL & "adm.compra INNER JOIN "
   vSQL = vSQL & "Adm.CompraDetalleArticuloInventario ON (compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania) "
   vSQL = vSQL & " AND (compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra) "
   vSQL = vSQL & " INNER JOIN " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON "
   vSQL = vSQL & "(CompraDetalleArticuloInventario.ConsecutivoCompania = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania) "
   vSQL = vSQL & " AND (CompraDetalleArticuloInventario.CodigoArticulo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo) "
   vSQL = vSQL & vSQLWhere
   
   vSQL = vSQL & " GROUP BY "
   vSQL = vSQL & " compra.ConsecutivoCompania,compra.Fecha, "
   vSQL = vSQL & gUtilSQL.DfMidSQL("compra.StatusCompra", 1, 0) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("Compra") & ","
   vSQL = vSQL & gUtilSQL.DfMidSQL("compra.StatusCompra", 1, 0) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("Compra") & ","
   vSQL = vSQL & " compra.Numero, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & "CompraDetalleArticuloInventario.CantidadRecibida, "
   vSQL = vSQL & "CompraDetalleArticuloInventario.Consecutivo-CompraDetalleArticuloInventario.Consecutivo, "
   vSQL = vSQL & "CompraDetalleArticuloInventario.Consecutivo "
   fSqlViewCompraDeArticulo = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCompraDeArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameVentaDeArticulo() As String
   GetViewNameVentaDeArticulo = "IGV_ArticuloVenta_B2"
End Function

Public Function fSqlViewVentaDeArticulo() As String
   Dim vSQL As String
   Dim vGroupby As String
   

   
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
     vGroupby = " Group by factura.ConsecutivoCompania, factura.Fecha, factura.TipoDeDocumento, factura.Numero ,factura.NumeroFacturaAfectada , " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo," & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ". Descripcion, " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, renglonFactura.ConsecutivoRenglon,factura.CodigoAlmacen,factura.FechaDeFacturaAfectada "
 
   'Con Articulo Simple
   vSQL = "SELECT "
   vSQL = vSQL & "factura.ConsecutivoCompania, "
   vSQL = vSQL & "factura.Fecha, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("Venta") & " AS TipoDeOperacion, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                        gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDeDocumento, "
   vSQL = vSQL & "factura.Numero AS NumeroDocumento, "
   vSQL = vSQL & "factura.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & " SUM ( 0 ) AS Entradas, "
   vSQL = vSQL & " SUM (renglonFactura.Cantidad ) AS Salidas, "
   vSQL = vSQL & " renglonFactura.ConsecutivoRenglon AS ConsecutivoRenglon, "
   vSQL = vSQL & "factura.CodigoAlmacen,"
   vSQL = vSQL & "factura.FechaDeFacturaAfectada AS FechaParaBusqueda "
   vSQL = vSQL & " FROM "
   vSQL = vSQL & " factura INNER JOIN "
   vSQL = vSQL & " renglonFactura ON (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania) "
   vSQL = vSQL & " AND (factura.Numero = renglonFactura.NumeroFactura) "
   vSQL = vSQL & " AND (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) INNER JOIN "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON (renglonFactura.Articulo =" & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " .CodigoDelArticulo) AND "
   vSQL = vSQL & " (renglonFactura.serial = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial  ) AND "
   vSQL = vSQL & " (renglonFactura.rollo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo  ) AND "
   
   vSQL = vSQL & " (renglonFactura.ConsecutivoCompania =" & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania)"
   vSQL = vSQL & " LEFT JOIN "
   vSQL = vSQL & " productoCompuesto ON (" & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = productoCompuesto.ConsecutivoCompania) "
   vSQL = vSQL & " AND (" & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = productoCompuesto.CodigoConexionConElMaster) "
   vSQL = vSQL & " WHERE "
   vSQL = vSQL & " (factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   vSQL = vSQL & " AND factura.GeneradaPorNotaEntrega = " & gUtilSQL.fSimpleSqlValue(enum_FacturaGeneraNotaEntrega.eFGNE_NoGenerada)
   'vSQL = vSQL & " AND " & gUtilSQL.fSimpleSqlValue("sNull(factura.GeneradaPorNotaEntrega," & enum_FacturaGeneraNotaEntrega.eFGNE_NoGenerada & "))") & " <> " & gUtilSQL.fSimpleSqlValue(enum_FacturaGeneraNotaEntrega.eFGNE_Generada)
   vSQL = vSQL & " AND (productoCompuesto.CodigoConexionConElMaster IS NULL) "
   vSQL = vSQL & vGroupby
   
   vSQL = vSQL & " UNION "
   'Con Articulo Compuesto
   vSQL = vSQL & "SELECT "
   vSQL = vSQL & "factura.ConsecutivoCompania, "
   vSQL = vSQL & "factura.Fecha, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("Venta") & " AS TipoDeOperacion, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                        gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDeDocumento, "
   vSQL = vSQL & "factura.Numero " & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("(*)") & " AS NumeroDocumento, "
   vSQL = vSQL & "factura.NumeroFacturaAfectada AS NumeroFacturaAfectada, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & " SUM ( 0 )  AS Entradas , "
   vSQL = vSQL & " SUM (productoCompuesto.Cantidad * renglonFactura.Cantidad) AS Salidas,  "
   vSQL = vSQL & " renglonFactura.ConsecutivoRenglon AS ConsecutivoRenglon, "
   vSQL = vSQL & "factura.CodigoAlmacen,"
   vSQL = vSQL & "factura.FechaDeFacturaAfectada AS FechaParaBusqueda "
   vSQL = vSQL & " FROM "
   vSQL = vSQL & " factura INNER JOIN "
   vSQL = vSQL & " renglonFactura ON (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania) "
   vSQL = vSQL & " AND (factura.Numero = renglonFactura.NumeroFactura) "
   vSQL = vSQL & " AND (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) INNER JOIN "
   vSQL = vSQL & " productoCompuesto ON (renglonFactura.Articulo = productoCompuesto.CodigoConexionConElMaster) "
   vSQL = vSQL & " AND (renglonFactura.ConsecutivoCompania = productoCompuesto.ConsecutivoCompania) INNER JOIN "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON (productoCompuesto.ConsecutivoCompania = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania) "
   vSQL = vSQL & " AND (productoCompuesto.CodigoArticulo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo) "
   vSQL = vSQL & " AND (renglonFactura.rollo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo  ) "
   vSQL = vSQL & " AND (renglonFactura.ConsecutivoCompania = productoCompuesto.ConsecutivoCompania) "
   
   vSQL = vSQL & "WHERE (factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   vSQL = vSQL & " AND factura.GeneradaPorNotaEntrega = " & gUtilSQL.fSimpleSqlValue(enum_FacturaGeneraNotaEntrega.eFGNE_NoGenerada)
   'vSQL = vSQL & " AND " & gUtilSQL.fSimpleSqlValue("sNull(factura.GeneradaPorNotaEntrega," & enum_FacturaGeneraNotaEntrega.eFGNE_NoGenerada & "))") & " <> " & gUtilSQL.fSimpleSqlValue(enum_FacturaGeneraNotaEntrega.eFGNE_Generada)
   vSQL = vSQL & vGroupby
   
   fSqlViewVentaDeArticulo = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentaDeArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameNotaEntradaSalidaDeArticulo() As String
   GetViewNameNotaEntradaSalidaDeArticulo = "IGV_ArticuloNotaEntradaSalida_B3"
End Function

Public Function fSqlViewNotaEntradaSalidaDeArticulo() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim sqlWhere As String
   Set gEnumProyecto = New clsEnumAdministrativo
   On Error GoTo h_ERROR
   sqlWhere = ""
   sqlWhere = gUtilSQL.fSQLEnumValueWithAnd("", "notaDeEntradaSalida.TipodeOperacion", enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO)
   sqlWhere = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere, "notaDeEntradaSalida.StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   vSQL = "SELECT "
   vSQL = vSQL & "notaDeEntradaSalida.ConsecutivoCompania, "
   vSQL = vSQL & "notaDeEntradaSalida.Fecha, "
   vSQL = vSQL & gUtilSQL.getIIF("NotaDeEntradaSalida.GeneradoPor = '0' ", gUtilSQL.fSimpleSqlValue("Consignación Recibida"), gUtilSQL.fSimpleSqlValue("Consignación Recibida - Orden Prod.")) & " AS TipoDeOperacion,"
   vSQL = vSQL & gUtilSQL.getIIF("NotaDeEntradaSalida.GeneradoPor = '0' ", gUtilSQL.fSimpleSqlValue("Entrada de Inventario"), gUtilSQL.fSimpleSqlValue("Entrada producto terminado")) & " AS TipoDeDocumento,"
   vSQL = vSQL & " notaDeEntradaSalida.NumeroDocumento, "
   vSQL = vSQL & " renglonNotaES.CodigoArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & " SUM (renglonNotaES.Cantidad ) AS Entradas, "
   vSQL = vSQL & " 0 AS Salidas,  "
   vSQL = vSQL & " renglonNotaES.ConsecutivoRenglon,  "
   vSQL = vSQL & " notaDeEntradaSalida.CodigoAlmacen"
   vSQL = vSQL & " FROM "
   vSQL = vSQL & " notaDeEntradaSalida INNER JOIN "
   vSQL = vSQL & " renglonNotaES ON (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania) "
   vSQL = vSQL & " AND (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento) INNER JOIN "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON ( renglonNotaES.CodigoArticulo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo) "
   
   vSQL = vSQL & " AND (renglonNotaES.serial = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial) "
   vSQL = vSQL & " AND (renglonNotaES.rollo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo) "
   vSQL = vSQL & " AND (renglonNotaES.ConsecutivoCompania = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania) "
   
   vSQL = vSQL & sqlWhere
   vSQL = vSQL & " GROUP BY "
   vSQL = vSQL & "notaDeEntradaSalida.ConsecutivoCompania, "
   vSQL = vSQL & "notaDeEntradaSalida.Fecha, "
   vSQL = vSQL & " notaDeEntradaSalida.NumeroDocumento, "
   vSQL = vSQL & " renglonNotaES.CodigoArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & "renglonNotaES.Cantidad, "
   vSQL = vSQL & "renglonNotaES.ConsecutivoRenglon,  "
   vSQL = vSQL & " notaDeEntradaSalida.CodigoAlmacen"
   vSQL = vSQL & " , NotaDeEntradaSalida.GeneradoPor"
   sqlWhere = ""
   vSQL = vSQL & " UNION "
   'con Articulo Simple
   
   sqlWhere = "notaDeEntradaSalida.TipodeOperacion <> " & gUtilSQL.fSimpleSqlValue(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO)
   sqlWhere = sqlWhere & " AND " & gUtilSQL.DfSQLIsNull("productoCompuesto.CodigoConexionConElMaster")
   sqlWhere = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere, "notaDeEntradaSalida.StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & "notaDeEntradaSalida.ConsecutivoCompania, "
   vSQL = vSQL & "notaDeEntradaSalida.Fecha, "
   vSQL = vSQL & gUtilSQL.getIIF(" NotaDeEntradaSalida.GeneradoPor = '0' ", gUtilSQL.fSimpleSqlValue("Consignación Entregada"), gUtilSQL.fSimpleSqlValue("Consignación Entregada - Orden Prod.")) & " AS TipoDeOperacion,"
   vSQL = vSQL & gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Entrada de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Entrada producto terminado'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Salida de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Salida Inventario - Orden Prod.'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_Autoconsumo) _
   , "'Autoconsumo'", "'Retiro'"))))) & " AS TipoOperacion, "
   
   
   vSQL = vSQL & "notaDeEntradaSalida.NumeroDocumento, "
   vSQL = vSQL & "renglonNotaES.CodigoArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & " 0 AS Entradas, "
   vSQL = vSQL & " SUM (renglonNotaES.Cantidad) AS Salidas, "
   vSQL = vSQL & " renglonNotaES.ConsecutivoRenglon,  "
   vSQL = vSQL & " notaDeEntradaSalida.CodigoAlmacen"
   vSQL = vSQL & " FROM "
   vSQL = vSQL & " notaDeEntradaSalida INNER JOIN "
   vSQL = vSQL & " renglonNotaES ON (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania) "
   vSQL = vSQL & " AND (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento) INNER JOIN "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON (renglonNotaES.CodigoArticulo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo) "
   vSQL = vSQL & " AND (renglonNotaES.serial = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial) "
   vSQL = vSQL & " AND (renglonNotaES.rollo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo) "
   vSQL = vSQL & " AND (renglonNotaES.ConsecutivoCompania = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania ) LEFT JOIN "
   vSQL = vSQL & " productoCompuesto ON (" & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = productoCompuesto.ConsecutivoCompania) "
   vSQL = vSQL & " AND (" & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = productoCompuesto.CodigoConexionConElMaster) "
   vSQL = vSQL & sqlWhere
   vSQL = vSQL & " GROUP BY "
   
   vSQL = vSQL & "notaDeEntradaSalida.ConsecutivoCompania, "
   vSQL = vSQL & "notaDeEntradaSalida.Fecha, "
   vSQL = vSQL & "notaDeEntradaSalida.TipoDeOperacion, "
   vSQL = vSQL & "notaDeEntradaSalida.NumeroDocumento, "
   vSQL = vSQL & "renglonNotaES.CodigoArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & "renglonNotaES.Cantidad, "
   vSQL = vSQL & "renglonNotaES.ConsecutivoRenglon,  "
   vSQL = vSQL & " notaDeEntradaSalida.CodigoAlmacen"
   vSQL = vSQL & " ,NotaDeEntradaSalida.GeneradoPor"
   sqlWhere = ""

  vSQL = vSQL & " UNION "
   'Con Articulo Compuesto
   
   sqlWhere = "notaDeEntradaSalida.TipodeOperacion <> " & gUtilSQL.fSimpleSqlValue(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO)
   sqlWhere = gUtilSQL.fSQLEnumValueWithAnd(sqlWhere, "notaDeEntradaSalida.StatusNotaEntradaSalida", enum_StatusNotaEntradaSalida.eSNES_Vigente)
   sqlWhere = gUtilSQL.getWhereSQL(sqlWhere)
   
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & "notaDeEntradaSalida.ConsecutivoCompania, "
   vSQL = vSQL & "notaDeEntradaSalida.Fecha, "
   vSQL = vSQL & gUtilSQL.getIIF(" NotaDeEntradaSalida.GeneradoPor = '0' ", gUtilSQL.fSimpleSqlValue("Consignación Entregada"), gUtilSQL.fSimpleSqlValue("Consignación Entregada - Orden Prod.")) & " AS TipoDeOperacion,"
   vSQL = vSQL & gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Entrada de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_ENTRADA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Entrada producto terminado'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '0' " _
   , "'Salida de Inventario'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_SALIDA_DE_INVENTARIO) & " AND " & " NotaDeEntradaSalida.GeneradoPor = '1' " _
   , "'Salida Inventario - Orden Prod.'", gUtilSQL.getIIF("notaDeEntradaSalida.TipoDeOperacion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipodeOperacion.eTO_Autoconsumo) _
   , "'Autoconsumo'", "'Retiro'"))))) & " AS TipoOperacion, "
   
   vSQL = vSQL & "notaDeEntradaSalida.NumeroDocumento " & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("(*)") & " AS NumeroDocumento, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo AS CodigoArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & "0 AS Entradas, "
   vSQL = vSQL & "(renglonNotaES.Cantidad * productoCompuesto.Cantidad) AS Salidas, "
   vSQL = vSQL & "renglonNotaES.ConsecutivoRenglon,"
   vSQL = vSQL & "notaDeEntradaSalida.CodigoAlmacen"
   vSQL = vSQL & " FROM "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " INNER JOIN "
   vSQL = vSQL & " productoCompuesto ON ( " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = productoCompuesto.ConsecutivoCompania) "
   vSQL = vSQL & " AND (" & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = productoCompuesto.CodigoArticulo) INNER JOIN "
   vSQL = vSQL & " notaDeEntradaSalida INNER JOIN "
   vSQL = vSQL & " renglonNotaES ON (notaDeEntradaSalida.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania) "
   vSQL = vSQL & " AND (notaDeEntradaSalida.NumeroDocumento = renglonNotaES.NumeroDocumento) "
   vSQL = vSQL & " ON (productoCompuesto.ConsecutivoCompania = renglonNotaES.ConsecutivoCompania) "
   vSQL = vSQL & " AND (productoCompuesto.CodigoConexionConElMaster = renglonNotaES.CodigoArticulo) "
   vSQL = vSQL & " AND (renglonNotaES.serial = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial) "
   vSQL = vSQL & " AND (renglonNotaES.rollo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo) "
   
   vSQL = vSQL & sqlWhere
   fSqlViewNotaEntradaSalidaDeArticulo = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewNotaEntradaSalidaDeArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameConteoFisicoDeArticulo() As String
   GetViewNameConteoFisicoDeArticulo = "IGV_ArticuloConteoFisico_B4"
End Function

Public Function fSqlViewConteoFisicoDeArticulo() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
   vSQL = "SELECT "
   vSQL = vSQL & "conteoFisico.ConsecutivoCompania, "
   vSQL = vSQL & "conteoFisico.Fecha, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("Saldo Inicial") & " AS TipoDeOperacion, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("Conteo Físico") & " AS TipoDeDocumento, "
   vSQL = vSQL & gUtilSQL.fCast("conteoFisico.ConsecutivoConteo", eTDSS_VARCHAR, "") & " AS NumeroDocumento, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".UnidadDeVenta, "
   vSQL = vSQL & gUtilSQL.getIIF("renglonConteoFisico.Diferencia >= 0", "renglonConteoFisico.Diferencia", 0, True) & " AS Entradas, "
   vSQL = vSQL & gUtilSQL.getIIF("renglonConteoFisico.Diferencia < 0 ", "renglonConteoFisico.Diferencia * (-1)", 0, True) & " AS Salidas,  "
   vSQL = vSQL & " renglonConteoFisico.ConsecutivoRenglon, "
   vSQL = vSQL & " conteoFisico.CodigoAlmacen"
   vSQL = vSQL & " FROM "
   vSQL = vSQL & " conteoFisico INNER JOIN "
   vSQL = vSQL & " renglonConteoFisico ON conteoFisico.ConsecutivoCompania = renglonConteoFisico.ConsecutivoCompania "
   vSQL = vSQL & " AND conteoFisico.ConsecutivoConteo = renglonConteoFisico.ConsecutivoConteo INNER JOIN "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " ON "
   vSQL = vSQL & " (renglonConteoFisico.serial = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial)  AND "
   vSQL = vSQL & " (renglonConteoFisico.rollo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo) AND  "
   
   vSQL = vSQL & " renglonConteoFisico.ConsecutivoCompania = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania "
   vSQL = vSQL & " AND renglonConteoFisico.CodigoArticulo = " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo  "
   vSQL = vSQL & " WHERE "
   vSQL = vSQL & " (conteoFisico.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusConteoFisico.eSC_PROCESADO) & ")"
   
   fSqlViewConteoFisicoDeArticulo = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewConteoFisicoDeArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameRegistroInventarioPermanente() As String
   GetViewNameRegistroInventarioPermanente = "IGV_InventarioPermanente_pAB1B2B3B4_B1"
End Function

Public Function fSqlViewRegistroInventarioPermanente() As String
   Dim vSQL As String
   Dim vGroupby As String
   On Error GoTo h_ERROR
   vGroupby = " Group by ConsecutivoCompania, Fecha,TipoDeOperacion,TipoDeDocumento, NumeroDocumento , Codigo, Descripcion, UnidadDeVenta"
  
   vSQL = vSQL & " SELECT ConsecutivoCompania, Fecha, TipodeOperacion, TipoDeDocumento, NumeroDeDocumento AS NumeroDeDocumento, Codigo AS CodigoArticulo, Descripcion, UnidadDeVenta, SUM (Entradas) AS Entradas, SUM (Salidas) AS Salidas"
   vSQL = vSQL & " From " & GetViewNameCompraDeArticulo
   vSQL = vSQL & " Group by ConsecutivoCompania, Fecha,TipoDeOperacion,TipoDeDocumento, NumeroDeDocumento , Codigo, Descripcion, UnidadDeVenta"
   
   vSQL = vSQL & " Union"
   
   vSQL = vSQL & " SELECT ConsecutivoCompania, Fecha, TipodeOperacion, TipoDeDocumento, NumeroDocumento AS NumeroDeDocumento, Codigo AS CodigoArticulo, Descripcion, UnidadDeVenta, SUM (Entradas) AS Entradas , SUM (Salidas) AS Salidas"
   vSQL = vSQL & " From " & GetViewNameVentaDeArticulo
   vSQL = vSQL & vGroupby
   
   vSQL = vSQL & " Union"
   vSQL = vSQL & " SELECT ConsecutivoCompania, Fecha, TipodeOperacion, TipoDeDocumento, NumeroDocumento AS NumeroDeDocumento, Codigo AS CodigoArticulo , Descripcion, UnidadDeVenta, SUM (Entradas) AS Entradas , SUM (Salidas) AS Salidas"
   vSQL = vSQL & " From " & GetViewNameNotaEntradaSalidaDeArticulo
   vSQL = vSQL & vGroupby
   
   vSQL = vSQL & " Union"
   vSQL = vSQL & " SELECT ConsecutivoCompania, Fecha, TipodeOperacion, TipoDeDocumento, NumeroDocumento AS NumeroDeDocumento, Codigo AS CodigoArticulo, Descripcion, UnidadDeVenta, SUM (Entradas) AS Entradas , SUM (Salidas) AS Salidas"
   vSQL = vSQL & " From " & GetViewNameConteoFisicoDeArticulo
   vSQL = vSQL & vGroupby
   
   fSqlViewRegistroInventarioPermanente = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRegistroInventarioPermanente", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameRegistroInventarioPermanenteValorizado() As String
   GetViewNameRegistroInventarioPermanenteValorizado = "IGV_InventarioPermanenteValorizado_pIB1_B2"
End Function

Public Function fSqlViewRegistroInventarioPermanenteValorizado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".ConsecutivoCompania, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".Fecha, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".TipoDeDocumento, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".TipodeOperacion, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".NumeroDeDocumento, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".CodigoArticulo, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".Descripcion, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".UnidadDeVenta, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".Entradas, "
   vSQL = vSQL & " articuloInventario.CostoUnitario, "
   vSQL = vSQL & gUtilSQL.getIIF(GetViewNameRegistroInventarioPermanente & ".Entradas <> 0", "articuloInventario.CostoUnitario", "0", True) & " AS CostoUnitarioEntrada, "
   vSQL = vSQL & "(" & GetViewNameRegistroInventarioPermanente & ".Entradas * articuloInventario.CostoUnitario) AS CostoTotalEntrante, "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & ".Salidas, "
   vSQL = vSQL & gUtilSQL.getIIF(GetViewNameRegistroInventarioPermanente & ".Salidas <> 0", "articuloInventario.CostoUnitario", "0", True) & " AS CostoUnitarioSalida, "
   vSQL = vSQL & "(" & GetViewNameRegistroInventarioPermanente & ".Salidas * articuloInventario.CostoUnitario) AS CostoTotalSaliente"
   vSQL = vSQL & " FROM "
   vSQL = vSQL & GetViewNameRegistroInventarioPermanente & " INNER JOIN "
   vSQL = vSQL & " articuloInventario ON " & GetViewNameRegistroInventarioPermanente & ".ConsecutivoCompania = articuloInventario.ConsecutivoCompania"
   vSQL = vSQL & " AND " & GetViewNameRegistroInventarioPermanente & ".CodigoArticulo = articuloInventario.Codigo "

   fSqlViewRegistroInventarioPermanenteValorizado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRegistroInventarioPermanenteValorizado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameUbicacionXAlmacenDeArticulo() As String
   GetViewNameUbicacionXAlmacenDeArticulo = "IGV_UBICACION"
End Function

Public Function fSqlViewUbicacionXAlmacenDeArticulo() As String
   Dim vSQL As String
   Dim vSQLCondicionComun As String
   Dim vSQLWhere As String
    
   
   On Error GoTo h_ERROR
   
   vSQLCondicionComun = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLCondicionComun = gUtilSQL.fSQLEnumValueWithAnd(vSQLCondicionComun, "ArticuloInventario.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   
   
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLCondicionComun, "ArticuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_UsaTalla_Color)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
  
    vSQL = "SELECT "
    vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania,"
    vSQL = vSQL & " ArticuloInventario.Codigo,"
    vSQL = vSQL & " ArticuloInventario.Descripcion,"
    vSQL = vSQL & " ArticuloInventario.LineaDeProducto,"
    vSQL = vSQL & " ArticuloInventario.Marca,"
    vSQL = vSQL & " ExistenciaPorAlmacen.CodigoAlmacen,"
    vSQL = vSQL & " 'N/A' AS Serial,"
    vSQL = vSQL & " 'N/A' AS Rollo,"
    vSQL = vSQL & "  ExistenciaPorAlmacen.Ubicacion,"
    vSQL = vSQL & "  ArticuloInventario.Categoria, "
    vSQL = vSQL & "  ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla AS CodigoArticulo, "
    vSQL = vSQL & " ArticuloInventario.TipoArticuloInv AS TipoArticuloInv, "
    vSQL = vSQL & " ArticuloInventario.StatusdelArticulo, "
    vSQL = vSQL & " ArticuloInventario.TipoDeArticulo "
    
    vSQL = vSQL & "  From ExistenciaPorAlmacen"
    vSQL = vSQL & "  Inner Join"
    vSQL = vSQL & "  ExistenciaPorGrupo ON"
    vSQL = vSQL & "  ExistenciaPorAlmacen.ConsecutivoCompania =  ExistenciaPorGrupo.ConsecutivoCompania AND "
    vSQL = vSQL & "  ExistenciaPorAlmacen.CodigoArticulo =  ExistenciaPorGrupo.CodigoArticulo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla "
    vSQL = vSQL & "  Inner Join"
    vSQL = vSQL & "  ArticuloInventario ON"
    vSQL = vSQL & "  ExistenciaPorGrupo.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND"
    vSQL = vSQL & " ExistenciaPorGrupo.CodigoArticulo = ArticuloInventario.Codigo AND "
    vSQL = vSQL & " ExistenciaPorGrupo.CodigoGrupo = ArticuloInventario.CodigoGrupo "
    
    vSQL = vSQL & vSQLWhere
    
    vSQLWhere = ""
    vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLCondicionComun, "ArticuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_Simple)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
     
   vSQL = vSQL & " UNION "
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania, ArticuloInventario.Codigo,  ArticuloInventario.Descripcion,"
   vSQL = vSQL & " ArticuloInventario.LineaDeProducto, ArticuloInventario.Marca,ExistenciaPorAlmacen.CodigoAlmacen, 'N/A' AS Serial, 'N/A' AS Rollo,"
   vSQL = vSQL & " ExistenciaPorAlmacen.Ubicacion , ArticuloInventario.Categoria, "
   vSQL = vSQL & " ArticuloInventario.Codigo  AS CodigoArticulo, "
   vSQL = vSQL & " ArticuloInventario.TipoArticuloInv AS TipoArticuloInv, "
   vSQL = vSQL & " ArticuloInventario.StatusdelArticulo,"
   vSQL = vSQL & " ArticuloInventario.TipoDeArticulo "
  
  
   vSQL = vSQL & " FROM ExistenciaPorAlmacen INNER JOIN "
   vSQL = vSQL & " ArticuloInventario ON ExistenciaPorAlmacen.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND "
   vSQL = vSQL & " ExistenciaPorAlmacen.CodigoArticulo = ArticuloInventario.Codigo "
   vSQL = vSQL & vSQLWhere
   
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario_1.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario_1.StatusdelArticulo", enum_StatusArticulo.eSA_VIGENTE)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQL = vSQL & " UNION "
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " ArticuloInventario_1.ConsecutivoCompania, ArticuloInventario_1.Codigo, ArticuloInventario_1.Descripcion, ArticuloInventario_1.LineaDeProducto,"
   vSQL = vSQL & " ArticuloInventario_1.Marca, RenglonExistenciaAlmacen.CodigoAlmacen, "
   vSQL = vSQL & gUtilSQL.getIIF("RenglonExistenciaAlmacen.CodigoSerial = " & gUtilSQL.fSimpleSqlValue(""), "'N/A'", "RenglonExistenciaAlmacen.CodigoSerial", True) & " AS Serial, "
   vSQL = vSQL & gUtilSQL.getIIF("RenglonExistenciaAlmacen.CodigoRollo = " & gUtilSQL.fSimpleSqlValue(""), "'N/A'", "RenglonExistenciaAlmacen.CodigoRollo", True) & " AS CodigoRollo, "
   vSQL = vSQL & " RenglonExistenciaAlmacen.Ubicacion , ArticuloInventario_1.Categoria, "
   vSQL = vSQL & " ArticuloInventario_1.Codigo AS CodigoArticulo, "
   vSQL = vSQL & " ArticuloInventario_1.TipoArticuloInv AS TipoArticuloInv, "
   vSQL = vSQL & " ArticuloInventario_1.StatusdelArticulo,"
   vSQL = vSQL & " ArticuloInventario_1.TipoDeArticulo "
   
   vSQL = vSQL & " FROM RenglonExistenciaAlmacen INNER JOIN"
   vSQL = vSQL & " ArticuloInventario AS ArticuloInventario_1 ON"
   vSQL = vSQL & " RenglonExistenciaAlmacen.ConsecutivoCompania = ArticuloInventario_1.ConsecutivoCompania AND"
   vSQL = vSQL & " RenglonExistenciaAlmacen.CodigoArticulo = ArticuloInventario_1.Codigo "
   vSQL = vSQL & vSQLWhere
   
   fSqlViewUbicacionXAlmacenDeArticulo = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewUbicacionXAlmacenDeArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameGrupoTallaColorB1() As String
   GetViewNameGrupoTallaColorB1 = "IGV_GrupoTallaColor_B1"
End Function

Public Function fSQL() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT GrupoTallaColor.ConsecutivoCompania, GrupoTallaColor.CodigoGrupo," & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("RenglonGrupoTalla.CodigoTalla"), gUtilSQL.fSimpleSqlValue(""), "RenglonGrupoTalla.CodigoTalla", True)
   vSQL = vSQL & gUtilSQL.CharConcat() & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("RenglonGrupoColor.CodigoColor"), gUtilSQL.fSimpleSqlValue(""), "RenglonGrupoColor.CodigoColor", True) & " AS CodigoTallaColor"  '"+ ISNULL(RenglonGrupoColor.CodigoColor, '') AS CodigoTallaColor"
   vSQL = vSQL & " FROM GrupoTallaColor LEFT OUTER JOIN "
   vSQL = vSQL & " RenglonGrupoColor ON GrupoTallaColor.ConsecutivoCompania = RenglonGrupoColor.ConsecutivoCompania AND"
   vSQL = vSQL & " GrupoTallaColor.CodigoGrupo = RenglonGrupoColor.CodigoGrupo LEFT OUTER JOIN"
   vSQL = vSQL & " RenglonGrupoTalla ON GrupoTallaColor.ConsecutivoCompania = RenglonGrupoTalla.ConsecutivoCompania AND"
   vSQL = vSQL & " GrupoTallaColor.CodigoGrupo = RenglonGrupoTalla.CodigoGrupo"
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewUbicacionXAlmacenDeArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() As String
   GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 = "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
End Function

Public Function fSqlViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() As String
   Dim vSQL As String
   Dim vSQLCondicion As String
   Dim vSQLOperacion As String
   Dim vSQLCondicionTipoArticuloInv As String
   On Error GoTo h_ERROR
  vSQLCondicionTipoArticuloInv = gUtilSQL.fSQLEnumValueWithAnd("", "ArticuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_Simple)
  vSQLCondicion = "(" & gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.CodigoColor") & "OR" & gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.CodigoTalla") & ")"
  vSQLOperacion = "ArticuloInventario.Codigo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla"
  vSQL = "SELECT "
  vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania, "
  vSQL = vSQL & " ArticuloInventario.Codigo, "
  vSQL = vSQL & " ArticuloInventario.Categoria,"
  vSQL = vSQL & " ArticuloInventario.TipoArticuloInv, "
  vSQL = vSQL & " ArticuloInventario.TipoDeArticulo, "
  vSQL = vSQL & " ArticuloInventario.LineaDeProducto, "
  vSQL = vSQL & " ArticuloInventario.AlicuotaIva, "
  vSQL = vSQL & " ArticuloInventario.PorcentajeBaseImponible, "
  vSQL = vSQL & " ArticuloInventario.PrecioSinIva, "
  vSQL = vSQL & " ArticuloInventario.PrecioConIva, "
  vSQL = vSQL & " ArticuloInventario.PrecioSinIva2, "
  vSQL = vSQL & " ArticuloInventario.PrecioConIva2, "
  vSQL = vSQL & " ArticuloInventario.PrecioSinIva3, "
  vSQL = vSQL & " ArticuloInventario.PrecioConIva3, "
  vSQL = vSQL & " ArticuloInventario.PrecioSinIva4, "
  vSQL = vSQL & " ArticuloInventario.PrecioConIva4, "
  vSQL = vSQL & " ArticuloInventario.StatusdelArticulo, "
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoTalla, "
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoColor, "
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoGrupo, "
  vSQL = vSQL & gUtilSQL.getIIF(vSQLCondicionTipoArticuloInv, "ArticuloInventario.Existencia", "ExistenciaPorGrupo.Existencia", True) & " AS Existencia, "
  vSQL = vSQL & gUtilSQL.getIIF(vSQLCondicionTipoArticuloInv, "ArticuloInventario.CantArtReservado", "ExistenciaPorGrupo.CantReservada", True) & " AS CantArtReservado, "
  
  vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.Serial"), gUtilSQL.fSimpleSqlValue("0"), "ExistenciaPorGrupo.Serial", True) & " AS Serial, "
  vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("ExistenciaPorGrupo.Rollo"), gUtilSQL.fSimpleSqlValue("0"), "ExistenciaPorGrupo.Rollo", True) & " AS Rollo, "

  vSQL = vSQL & " ArticuloInventario.Descripcion, "
  vSQL = vSQL & " ArticuloInventario.UnidadDeVenta, "
  vSQL = vSQL & gUtilSQL.getIIF(vSQLCondicion, "ArticuloInventario.Codigo", vSQLOperacion, True) & " AS CodigoDelArticulo, "
  vSQL = vSQL & " ArticuloInventario.TipoDeProducto, "
  vSQL = vSQL & " ArticuloInventario.UnidadDeVentaSecundaria "
  vSQL = vSQL & " From ArticuloInventario "
  vSQL = vSQL & " LEFT OUTER JOIN ExistenciaPorGrupo ON "
  vSQL = vSQL & " ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo AND"
  vSQL = vSQL & " ArticuloInventario.CodigoGrupo = " & gUtilSQL.getIIF("ExistenciaPorGrupo.CodigoGrupo = " & gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue(""), "ExistenciaPorGrupo.CodigoGrupo", True) & " AND "
  vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania"
        
  fSqlViewNameArticulosDeInventarioConOSinExistenPorGrupoB1 = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewNameArticulosDeInventarioConOSinExistenPorGrupoB1", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameExistenciaPorGrupoConTallasyColores() As String
   GetViewNameExistenciaPorGrupoConTallasyColores = "IGV_ExistenciaPorGrupoConTallasyColores"
End Function

Public Function fSqlViewNameExistenciaPorGrupoConTallasyColores() As String
   Dim vSQL As String
   Dim vSQLCondicion As String
   Dim vSQLOperacion As String
  
  On Error GoTo h_ERROR
  
  vSQLOperacion = "ExistenciaPorGrupo.Existencia -  ExistenciaPorGrupo.CantReservada"
  vSQL = "SELECT "
  vSQL = vSQL & " ExistenciaPorGrupo.ConsecutivoCompania,"
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoArticulo,"
  vSQL = vSQL & " ExistenciaPorGrupo.Serial,"
  vSQL = vSQL & " ExistenciaPorGrupo.Rollo,"
  vSQL = vSQL & " ExistenciaPorGrupo.Existencia,"
  vSQL = vSQL & " ExistenciaPorGrupo.CantReservada,"
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoGrupo,"
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoTalla,"
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoColor,"
  vSQL = vSQL & vSQLOperacion & " AS DisponibilidadDetalle,"
  vSQL = vSQL & " Color.DescripcionColor,"
  vSQL = vSQL & " Talla.DescripcionTalla"
  vSQL = vSQL & " From"
  vSQL = vSQL & " ExistenciaPorGrupo"
  vSQL = vSQL & " LEFT OUTER JOIN  SAW.Talla ON"
  vSQL = vSQL & " Talla.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania AND"
  vSQL = vSQL & " Talla.CodigoTalla = ExistenciaPorGrupo.CodigoTalla"
  vSQL = vSQL & " LEFT OUTER JOIN Color ON"
  vSQL = vSQL & " ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania AND"
  vSQL = vSQL & " ExistenciaPorGrupo.CodigoColor = Color.CodigoColor"
  
  fSqlViewNameExistenciaPorGrupoConTallasyColores = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewNameExistenciaPorGrupoConTallasyColores", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() As String
   GetViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 = "IGV_ExistenciaPorAlmacenArticulosDeInven_pIB1"
End Function

Public Function fSqlViewNameExistenciaPorAlmacenArticulosDeInvenpIB1() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
 vSQL = " SELECT "
 vSQL = vSQL & "ExistenciaPorAlmacen.ConsecutivoCompania,"
 vSQL = vSQL & " ExistenciaPorAlmacen.CodigoAlmacen,"
 vSQL = vSQL & "ExistenciaPorAlmacen.CodigoArticulo,"
 vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("RenglonExistenciaAlmacen.CodigoSerial") & " OR " & " RenglonExistenciaAlmacen.CodigoSerial = " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue("0"), "RenglonExistenciaAlmacen.CodigoSerial", True) & " AS Serial, "
 vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("RenglonExistenciaAlmacen.CodigoRollo") & " OR " & " RenglonExistenciaAlmacen.CodigoRollo = " & gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue("0"), "RenglonExistenciaAlmacen.CodigoRollo", True) & " AS Rollo, "
 vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("RenglonExistenciaAlmacen.CodigoSerial") & " OR " & gUtilSQL.DfSQLIsNull("RenglonExistenciaAlmacen.CodigoRollo") & " OR " & " RenglonExistenciaAlmacen.CodigoSerial = " & gUtilSQL.fSimpleSqlValue(""), "ExistenciaPorAlmacen.Cantidad", "RenglonExistenciaAlmacen.Cantidad", True) & " AS ExistenciaAlmacen "
 vSQL = vSQL & " FROM "
 vSQL = vSQL & " ExistenciaPorAlmacen LEFT JOIN "
 vSQL = vSQL & " RenglonExistenciaAlmacen ON "
 vSQL = vSQL & "  ExistenciaPorAlmacen.ConsecutivoCompania =  RenglonExistenciaAlmacen.ConsecutivoCompania AND "
 vSQL = vSQL & "  ExistenciaPorAlmacen.CodigoAlmacen = dbo.RenglonExistenciaAlmacen.CodigoAlmacen AND "
 vSQL = vSQL & " ExistenciaPorAlmacen.CodigoArticulo = dbo.RenglonExistenciaAlmacen.CodigoArticulo"
  
  fSqlViewNameExistenciaPorAlmacenArticulosDeInvenpIB1 = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewNameExistenciaPorAlmacenArticulosDeInvenpIB1", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 
Public Function GetViewNameInventarioMercanciaEnTransito() As String
   GetViewNameInventarioMercanciaEnTransito = "IGV_InventarioMercanciaEnTransito"
End Function

Public Function fSqlViewInventarioMercanciaEnTransito() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT CompraDetalleArticuloInventario.CodigoArticulo,"
   vSQL = vSQL & "CompraDetalleArticuloInventario.ConsecutivoCompania,"
   vSQL = vSQL & gUtilSQL.fCast("SUM(CompraDetalleArticuloInventario.Cantidad - CompraDetalleArticuloInventario.CantidadRecibida)", eTDSS_MONEY, "CantidadEnTransito") & ","
   vSQL = vSQL & "'S' AS EsUnaOrdenDeCompra"
   vSQL = vSQL & " FROM Adm.Compra INNER JOIN"
   vSQL = vSQL & " Adm.CompraDetalleArticuloInventario ON Compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania "
   vSQL = vSQL & " AND Compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra"
   vSQL = vSQL & " WHERE (CompraDetalleArticuloInventario.Cantidad > CompraDetalleArticuloInventario.CantidadRecibida)"
   vSQL = vSQL & " GROUP BY CompraDetalleArticuloInventario.CodigoArticulo, CompraDetalleArticuloInventario.ConsecutivoCompania "
   fSqlViewInventarioMercanciaEnTransito = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewInventarioMercanciaEnTransito", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameInventarioExistenciaXGrupo() As String
   GetViewNameInventarioExistenciaXGrupo = "IGV_InventarioExistenciaXGrupo"
End Function

Public Function fSqlViewInventarioExistenciaXGrupo() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT ArticuloInventario.Codigo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla AS codigo, "
   vSQL = vSQL & " ArticuloInventario.Descripcion + ' ' + Color.DescripcionColor + ' Talla ' + Talla.DescripcionTalla AS descripcion,"
   vSQL = vSQL & " ArticuloInventario.ConsecutivoCompania, (CASE WHEN ((ExistenciaPorGrupo.CodigoColor IS NULL) OR"
   vSQL = vSQL & " (ExistenciaPorGrupo.CodigoTalla IS NULL))"
   vSQL = vSQL & " THEN ArticuloInventario.Codigo ELSE ArticuloInventario.Codigo + ExistenciaPorGrupo.CodigoColor + ExistenciaPorGrupo.CodigoTalla END) AS CodigoDelArticulo,"
   vSQL = vSQL & " ExistenciaPorGrupo.Existencia, ExistenciaPorGrupo.CantReservada AS CantidadReservada, ArticuloInventario.TipoArticuloInv,"
   vSQL = vSQL & " ArticuloInventario.LineaDeProducto, ArticuloInventario.Categoria, ArticuloInventario.Marca, ArticuloInventario.StatusdelArticulo,"
   vSQL = vSQL & " ArticuloInventario.tipoDeArticulo"
   vSQL = vSQL & " FROM ArticuloInventario "
   vSQL = vSQL & " LEFT  JOIN ExistenciaPorGrupo ON ArticuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo "
   vSQL = vSQL & " AND ArticuloInventario.CodigoGrupo = ExistenciaPorGrupo.CodigoGrupo "
   vSQL = vSQL & " AND ArticuloInventario.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania"
   vSQL = vSQL & " INNER JOIN Color ON ExistenciaPorGrupo.CodigoColor = Color.CodigoColor "
   vSQL = vSQL & " AND ExistenciaPorGrupo.ConsecutivoCompania = Color.ConsecutivoCompania"
   vSQL = vSQL & " INNER JOIN SAW.Talla ON ExistenciaPorGrupo.CodigoTalla = Talla.CodigoTalla "
   vSQL = vSQL & " AND ExistenciaPorGrupo.ConsecutivoCompania = Talla.ConsecutivoCompania"
   fSqlViewInventarioExistenciaXGrupo = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewInventarioExistenciaXGrupo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameArticuloInvMovimiento() As String
  GetViewNameArticuloInvMovimiento = "IGV_ArticuloInvMovimiento"
End Function
Public Function fSqlViewArticuloInvMovimiento() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSqlSelectViewArticuloInvMovimientoNS()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloInvMovimientoVenta()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloConteoFisico()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloCompra()
   fSqlViewArticuloInvMovimiento = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticuloInvMovimiento", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlSelectViewArticuloInvMovimientoNS() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
h_EXIT:   On Error GoTo 0
    vSQL = "SELECT IGV_ArticuloNotaEntradaSalida_B3.ConsecutivoCompania AS ConsecutivoCompania,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Codigo AS Codigo,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.NumeroDocumento AS NumeroDocumento,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento  , "
    vSQL = vSQL & fPrioridadOperacion() & ","
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Descripcion AS Descripcion, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameNotaEntradaSalidaDeArticulo() & ".Entradas"), gUtilSQL.fNumToStrSQL(0), GetViewNameNotaEntradaSalidaDeArticulo() & ".Entradas", True) & ") " & " AS Entrada, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameNotaEntradaSalidaDeArticulo() & ".Salidas"), gUtilSQL.fNumToStrSQL(0), GetViewNameNotaEntradaSalidaDeArticulo() & ".Salidas", True) & ") " & " AS Salidad, "
    vSQL = vSQL & gUtilSQL.getIIF("NotaDeEntradaSalida.TipodeOperacion = '0' AND NotaDeEntradaSalida.GeneradoPor = '1'", "IGV_ArticuloNotaEntradaSalida_B3.Entradas * OrdenDeProduccionDetalleArticulo.CostoUnitario", "0", True) & " AS MontoOperacion, "
    vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS NotaDeCredito, "
    vSQL = vSQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroFacturaAfectada, "
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Fecha AS FechaParaBusqueda "
    vSQL = vSQL & " FROM " & GetViewNameNotaEntradaSalidaDeArticulo()
    vSQL = vSQL & " INNER JOIN dbo.NotaDeEntradaSalida"
    vSQL = vSQL & " ON NotaDeEntradaSalida.NumeroDocumento = IGV_ArticuloNotaEntradaSalida_B3.NumeroDocumento"
    vSQL = vSQL & " AND NotaDeEntradaSalida.ConsecutivoCompania = IGV_ArticuloNotaEntradaSalida_B3.ConsecutivoCompania"
    vSQL = vSQL & " LEFT JOIN Adm.OrdenDeProduccion"
    vSQL = vSQL & " ON OrdenDeProduccion.Consecutivo = NotaDeEntradaSalida.ConsecutivoDocumentoOrigen"
    vSQL = vSQL & " AND OrdenDeProduccion.ConsecutivoCompania = NotaDeEntradaSalida.ConsecutivoCompania"
    vSQL = vSQL & " LEFT JOIN Adm.OrdenDeProduccionDetalleArticulo"
    vSQL = vSQL & " ON OrdenDeProduccionDetalleArticulo.ConsecutivoOrdenDeProduccion = OrdenDeProduccion.Consecutivo"
    vSQL = vSQL & " AND OrdenDeProduccionDetalleArticulo.ConsecutivoCompania = OrdenDeProduccion.ConsecutivoCompania"
    vSQL = vSQL & " GROUP BY IGV_ArticuloNotaEntradaSalida_B3.ConsecutivoCompania,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Codigo,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.NumeroDocumento,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Fecha,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.TipoDeDocumento,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.TipodeOperacion,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Descripcion,"
    vSQL = vSQL & " IGV_ArticuloNotaEntradaSalida_B3.Entradas,"
    vSQL = vSQL & " NotaDeEntradaSalida.TipodeOperacion,"
    vSQL = vSQL & " NotaDeEntradaSalida.GeneradoPor,"
    vSQL = vSQL & " OrdenDeProduccionDetalleArticulo.CostoUnitario"
    fSqlSelectViewArticuloInvMovimientoNS = vSQL
 Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fSqlSelectViewArticuloInvMovimientoNS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fPrioridadOperacion() As String
 Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_ENTRADA_DE_INVENTARIO))
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_SALIDA_DE_INVENTARIO))
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Autoconsumo))
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeOperacionToString(eTO_Retiro))
   vCadenaResultadoStr = gUtilSQL.fNumToStrSQL(3)
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fNumToStrSQL(4)
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fNumToStrSQL(5)
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fNumToStrSQL(6)

   vSQL = gUtilSQL.DfSQLCaseIf(GetViewNameNotaEntradaSalidaDeArticulo() & ".TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "PrioridadOperacion")
     Set gEnumProyecto = Nothing
   fPrioridadOperacion = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fPrioridadOperacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlSelectViewArticuloInvMovimientoVenta() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " Codigo AS  Codigo ,"
    vSQL = vSQL & " NumeroDocumento AS NumeroDocumento,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento AS TipoDeDocumento , "
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(7) & " AS PrioridadOperacion, "
    vSQL = vSQL & "Descripcion AS Descripcion, "
    vSQL = vSQL & " SUM ( " & fEntradaSegunElCaso() & ") AS entrada,"
    vSQL = vSQL & " SUM ( " & fSalidasSegunElCaso() & ") AS salidad,"
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoOperacion, "
    vSQL = vSQL & gUtilSQL.getIIF(GetViewNameVentaDeArticulo() & ".Salidas" & " < 0 ", gUtilSQL.fBooleanToSqlValue(True), gUtilSQL.fBooleanToSqlValue(False), True) & " AS NotaDeCredito, "
    vSQL = vSQL & "NumeroFacturaAfectada AS NumeroFacturaAfectada, "
    vSQL = vSQL & "FechaParaBusqueda AS FechaParaBusqueda "
    vSQL = vSQL & " FROM  " & GetViewNameVentaDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania, Codigo, NumeroDocumento, Fecha, TipoDeDocumento, Descripcion, Salidas, NumeroFacturaAfectada, FechaParaBusqueda"
   fSqlSelectViewArticuloInvMovimientoVenta = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectViewArticuloInvMovimientoVenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fEntradaSegunElCaso() As String
   Dim vCadenaComparacion As String
   On Error GoTo h_ERROR
   vCadenaComparacion = " (CASE WHEN " & gUtilSQL.DfSQLIsNull(GetViewNameVentaDeArticulo() & ".Entradas")
   vCadenaComparacion = vCadenaComparacion & " THEN " & gUtilSQL.fNumToStrSQL(0)
   vCadenaComparacion = vCadenaComparacion & " WHEN " & GetViewNameVentaDeArticulo() & ".Salidas < 0 THEN " & GetViewNameVentaDeArticulo() & ".Salidas *-1 ELSE " & GetViewNameVentaDeArticulo() & ".Entradas END)"
   fEntradaSegunElCaso = vCadenaComparacion
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEntradaSegunElCaso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
    

Private Function fSalidasSegunElCaso() As String
   Dim vCadenaComparacion As String
   On Error GoTo h_ERROR
   vCadenaComparacion = " (CASE WHEN " & gUtilSQL.DfSQLIsNull(GetViewNameVentaDeArticulo() & ".Salidas")
   vCadenaComparacion = vCadenaComparacion & " THEN " & gUtilSQL.fNumToStrSQL(0)
   vCadenaComparacion = vCadenaComparacion & " WHEN " & GetViewNameVentaDeArticulo() & ".Salidas < 0 THEN " & gUtilSQL.fNumToStrSQL(0) & " ELSE " & GetViewNameVentaDeArticulo() & ".Salidas END)"
   fSalidasSegunElCaso = vCadenaComparacion
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fSalidasSegunElCaso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlSelectViewArticuloConteoFisico() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " Codigo AS  Codigo ,"
    vSQL = vSQL & " NumeroDocumento AS NumeroDocumento,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento AS TipoDeDocumento , "
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS PrioridadOperacion, "
    vSQL = vSQL & "Descripcion AS Descripcion, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameConteoFisicoDeArticulo() & ".Entradas"), gUtilSQL.fNumToStrSQL(0), GetViewNameConteoFisicoDeArticulo() & ".Entradas", True) & " ) " & " AS Entrada, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameConteoFisicoDeArticulo() & ".Salidas"), gUtilSQL.fNumToStrSQL(0), GetViewNameConteoFisicoDeArticulo() & ".Salidas", True) & " ) " & " AS Salidad, "
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoOperacion, "
    vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS NotaDeCredito, "
    vSQL = vSQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroFacturaAfectada, "
    vSQL = vSQL & " Fecha AS FechaParaBusqueda "
    vSQL = vSQL & " FROM  " & GetViewNameConteoFisicoDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania, Codigo, NumeroDocumento, Fecha, TipoDeDocumento, Descripcion, Salidas"
    fSqlSelectViewArticuloConteoFisico = vSQL
 Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectViewArticuloConteoFisico()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
  
Private Function fSqlSelectViewArticuloCompra() As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQL As String
   Dim vSQLWhere   As String
   Dim insComprasVista As clsComprasVista
   On Error GoTo h_ERROR
   Set insComprasVista = New clsComprasVista
   Set gEnumProyecto = New clsEnumAdministrativo
   
   vSQLWhere = insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".StatusCompra", enum_StatusArticulo.eSA_VIGENTE)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQL = " SELECT "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Numero AS NumeroDocumento,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Fecha,"
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("COMPRA") & " AS TipoDeDocumento, "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(2) & " AS PrioridadOperacion,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
   vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida"), gUtilSQL.fNumToStrSQL(0), insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida", True) & " ) " & " AS Entrada, "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS Salidad,"
   vSQL = vSQL & " SUM ( " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".TotalCompraSinOtrosGastos + " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".OtrosGastosPorItem  * " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida)  * Compra.CambioAbolivares AS MontoOperacion, "
   vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS NotaDeCredito, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroFacturaAfectada, "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Fecha AS FechaParaBusqueda "
   vSQL = vSQL & " FROM " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & " ON "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".ConsecutivoCompania AND "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo =   " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CodigoArticulo "
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & insComprasVista.GetViewNameComprasOtroGastosPorItem & " ON "
   
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Serial AND "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Rollo AND "
  
   
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".ConsecutivoCompania  AND "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".NumeroSecuencial = " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".NumeroSecuencial AND "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Numero = " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".Numero "
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & " Compra ON "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = Compra.ConsecutivoCompania " & " AND "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Numero = Compra.Numero  "
   
   
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group By "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Numero,   "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Fecha, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".NumeroSecuencial, Compra.CambioAbolivares "
  
   Set gEnumProyecto = Nothing
  fSqlSelectViewArticuloCompra = vSQL
   Exit Function
h_ERROR:    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fSqlSelectViewArticuloCompra()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameArticuloInvMovimientoDetallado() As String
  GetViewNameArticuloInvMovimientoDetallado = "IGV_ArticuloInvMovimientoDetallado"
End Function
Public Function fSqlViewArticuloInvMovimientoDetallado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSqlSelectViewArticuloInvMovimientoNSDetallado()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloInvMovimientoVentaDetallado()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloConteoFisicoDetallado()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloCompraDetallado()
   fSqlViewArticuloInvMovimientoDetallado = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticuloInvMovimiento", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlSelectViewArticuloInvMovimientoNSDetallado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
h_EXIT:   On Error GoTo 0
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " Codigo AS  Codigo ,"
      vSQL = vSQL & " CodigoArticulo AS  CodigoDelArticulo ,"
    vSQL = vSQL & " NumeroDocumento AS NumeroDocumento,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento  , "
    vSQL = vSQL & fPrioridadOperacion() & ", "
    vSQL = vSQL & "Descripcion AS Descripcion, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameNotaEntradaSalidaDeArticulo() & ".Entradas"), gUtilSQL.fNumToStrSQL(0), GetViewNameNotaEntradaSalidaDeArticulo() & ".Entradas", True) & ") " & " AS Entrada, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameNotaEntradaSalidaDeArticulo() & ".Salidas"), gUtilSQL.fNumToStrSQL(0), GetViewNameNotaEntradaSalidaDeArticulo() & ".Salidas", True) & ") " & " AS Salidad, "
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoOperacion, "
    vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS NotaDeCredito, "
    vSQL = vSQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroFacturaAfectada "
    vSQL = vSQL & " FROM " & GetViewNameNotaEntradaSalidaDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania, Codigo, CodigoArticulo,NumeroDocumento, Fecha, TipoDeDocumento, TipodeOperacion,Descripcion "
   fSqlSelectViewArticuloInvMovimientoNSDetallado = vSQL
 Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fSqlSelectViewArticuloInvMovimientoNS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlSelectViewArticuloInvMovimientoVentaDetallado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " Codigo AS  Codigo ,"
    vSQL = vSQL & " CodigoDelArticulo AS  CodigoDelArticulo ,"
    vSQL = vSQL & " NumeroDocumento AS NumeroDocumento,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento AS TipoDeDocumento , "
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(7) & " AS PrioridadOperacion, "
    vSQL = vSQL & "Descripcion AS Descripcion, "
    vSQL = vSQL & " SUM ( " & fEntradaSegunElCaso() & ") AS entrada,"
    vSQL = vSQL & " SUM ( " & fSalidasSegunElCaso() & ") AS salidad,"
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoOperacion, "
    vSQL = vSQL & gUtilSQL.getIIF(GetViewNameVentaDeArticulo() & ".Salidas" & " < 0 ", gUtilSQL.fBooleanToSqlValue(True), gUtilSQL.fBooleanToSqlValue(False), True) & " AS NotaDeCredito, "
    vSQL = vSQL & "NumeroFacturaAfectada AS NumeroFacturaAfectada"
    vSQL = vSQL & " FROM  " & GetViewNameVentaDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania, Codigo,CodigoDelArticulo, NumeroDocumento, Fecha, TipoDeDocumento, Descripcion, Salidas, NumeroFacturaAfectada"
   fSqlSelectViewArticuloInvMovimientoVentaDetallado = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectViewArticuloInvMovimientoVenta", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlSelectViewArticuloConteoFisicoDetallado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " Codigo AS  Codigo ,"
    vSQL = vSQL & " CodigoDelArticulo AS  CodigoDelArticulo ,"
    vSQL = vSQL & " NumeroDocumento AS NumeroDocumento,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento AS TipoDeDocumento , "
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS PrioridadOperacion, "
    vSQL = vSQL & "Descripcion AS Descripcion, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameConteoFisicoDeArticulo() & ".Entradas"), gUtilSQL.fNumToStrSQL(0), GetViewNameConteoFisicoDeArticulo() & ".Entradas", True) & " ) " & " AS Entrada, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameConteoFisicoDeArticulo() & ".Salidas"), gUtilSQL.fNumToStrSQL(0), GetViewNameConteoFisicoDeArticulo() & ".Salidas", True) & " ) " & " AS Salidad, "
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoOperacion, "
    vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS NotaDeCredito, "
    vSQL = vSQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroFacturaAfectada "
    vSQL = vSQL & " FROM  " & GetViewNameConteoFisicoDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania, Codigo,CodigoDelArticulo, NumeroDocumento, Fecha, TipoDeDocumento, Descripcion, Salidas"
    fSqlSelectViewArticuloConteoFisicoDetallado = vSQL
 Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectViewArticuloConteoFisico()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlSelectViewArticuloCompraDetallado() As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQL As String
   Dim vSQLWhere   As String
   Dim insComprasVista As clsComprasVista
   On Error GoTo h_ERROR
   Set insComprasVista = New clsComprasVista
   Set gEnumProyecto = New clsEnumAdministrativo
   
   vSQLWhere = insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".StatusCompra", enum_StatusArticulo.eSA_VIGENTE)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQL = " SELECT "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Numero AS NumeroDocumento,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Fecha,"
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("COMPRA") & " AS TipoDeDocumento, "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(2) & " AS PrioridadOperacion,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
   vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida"), gUtilSQL.fNumToStrSQL(0), insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida", True) & " ) " & " AS Entrada, "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS Salidad,"
   vSQL = vSQL & " SUM ( " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".TotalCompraSinOtrosGastos + " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".OtrosGastosPorItem  * " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida)   AS MontoOperacion, "
   vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS NotaDeCredito, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroFacturaAfectada "
   vSQL = vSQL & " FROM " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & " ON "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".ConsecutivoCompania AND "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo =   " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CodigoArticulo "
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & insComprasVista.GetViewNameComprasOtroGastosPorItem & " ON "
   
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Serial AND "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Rollo AND "

   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".ConsecutivoCompania  AND "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".NumeroSecuencial = " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".NumeroSecuencial AND "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Numero = " & insComprasVista.GetViewNameComprasOtroGastosPorItem & ".Numero "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group By "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Numero,   "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Fecha, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".NumeroSecuencial"
  
   Set gEnumProyecto = Nothing
  fSqlSelectViewArticuloCompraDetallado = vSQL
   Exit Function
h_ERROR:    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fSqlSelectViewArticuloCompra()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameCompraFechaUltimaCompra() As String
 GetViewNameCompraFechaUltimaCompra = "IGV_CompraFechaUltimaCompra"
End Function
Public Function fSqlViewCompraFechaUltimaCompra() As String
   Dim vSQL As String
   Dim vSQLCondicionTipoArticuloInv As String
   On Error GoTo h_ERROR
   
    vSQLCondicionTipoArticuloInv = gUtilSQL.fSQLEnumValueWithAnd("", "articuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_Simple)
    vSQLCondicionTipoArticuloInv = "(" & vSQLCondicionTipoArticuloInv & ")" & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "articuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_UsaTalla_Color)
    vSQLCondicionTipoArticuloInv = "(" & vSQLCondicionTipoArticuloInv & ")"
    vSQLCondicionTipoArticuloInv = gUtilSQL.getWhereSQL(vSQLCondicionTipoArticuloInv)

     
     vSQL = vSQL & " SELECT   Compra.ConsecutivoCompania,  "
     vSQL = vSQL & " Almacen.Codigo AS CodigoAlmacen, "
     vSQL = vSQL & " Almacen.Consecutivo AS ConsecutivoAlmacen, "
     vSQL = vSQL & " MAX(Adm.Compra.Fecha) AS FechaMax, "
     vSQL = vSQL & " CompraDetalleArticuloInventario.CodigoArticulo, "
     vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Serial, "
     vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Rollo "
     vSQL = vSQL & "  FROM   Adm.Compra  "
     vSQL = vSQL & "  INNER JOIN  Adm.CompraDetalleArticuloInventario ON  "
     vSQL = vSQL & "  Compra.ConsecutivoCompania =  CompraDetalleArticuloInventario.ConsecutivoCompania AND "
     vSQL = vSQL & "  Compra.Consecutivo =     CompraDetalleArticuloInventario.ConsecutivoCompra "
     vSQL = vSQL & "  INNER JOIN Saw.Almacen ON"
     vSQL = vSQL & "  Compra.ConsecutivoCompania = Almacen.ConsecutivoCompania AND"
     vSQL = vSQL & "  Compra.ConsecutivoAlmacen = Almacen.Consecutivo"
     vSQL = vSQL & "  INNER JOIN articuloInventario ON  "
     vSQL = vSQL & "  CompraDetalleArticuloInventario.ConsecutivoCompania =  articuloInventario.ConsecutivoCompania And "
     vSQL = vSQL & "  Adm.CompraDetalleArticuloInventario.CodigoArticulo = articuloInventario.Codigo "
     vSQL = vSQL & vSQLCondicionTipoArticuloInv
     vSQL = vSQL & "  GROUP BY  "
     vSQL = vSQL & "  Compra.ConsecutivoCompania ,"
     vSQL = vSQL & "  Almacen.Codigo, "
     vSQL = vSQL & "  Almacen.Consecutivo, "
     vSQL = vSQL & "  CompraDetalleArticuloInventario.CodigoArticulo "
     vSQL = vSQL & " UNION "
     vSQL = vSQL & " SELECT   Compra.ConsecutivoCompania,  "
     vSQL = vSQL & " Almacen.Codigo As CodigoAlmacen, "
     vSQL = vSQL & " Almacen.Consecutivo As ConsecutivoAlmacen, "
     vSQL = vSQL & " MAX(Compra.Fecha) AS FechaMax, "
     vSQL = vSQL & " CompraDetalleSerialRollo.CodigoArticulo, "
     vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CompraDetalleSerialRollo.Serial"), gUtilSQL.fSimpleSqlValue("0"), "CompraDetalleSerialRollo.Serial", True) & " AS Serial, "
     vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CompraDetalleSerialRollo.Rollo"), gUtilSQL.fSimpleSqlValue("0"), "CompraDetalleSerialRollo.Rollo", True) & " AS Rollo "
     vSQL = vSQL & "  FROM   Adm.Compra  "
     vSQL = vSQL & "  INNER   JOIN  Adm.CompraDetalleSerialRollo ON "
     vSQL = vSQL & "  Compra.ConsecutivoCompania =  CompraDetalleSerialRollo.ConsecutivoCompania AND "
     vSQL = vSQL & "  Compra.Consecutivo = CompraDetalleSerialRollo.ConsecutivoCompra "
     vSQL = vSQL & "  INNER JOIN Saw.Almacen ON"
     vSQL = vSQL & "  Compra.ConsecutivoCompania = Almacen.ConsecutivoCompania AND"
     vSQL = vSQL & "  Compra.ConsecutivoAlmacen = Almacen.Consecutivo"
     vSQL = vSQL & "  GROUP BY  "
     vSQL = vSQL & "  Compra.ConsecutivoCompania ,"
     vSQL = vSQL & "  Almacen.Codigo, "
     vSQL = vSQL & "  Almacen.Consecutivo , "
     vSQL = vSQL & "  CompraDetalleSerialRollo.CodigoArticulo ,"
     vSQL = vSQL & "  CompraDetalleSerialRollo.Serial, "
     vSQL = vSQL & "  CompraDetalleSerialRollo.Rollo "
   fSqlViewCompraFechaUltimaCompra = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticuloInvMovimiento", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameComprasSecUltCom() As String
       GetViewNameComprasSecUltCom = "IGV_ComprasSecUltCom"
End Function
Public Function fSqlViewComprasSecUltCom() As String
   Dim vSQL As String
   Dim vSQLCondicionTipoArticuloInv As String
   On Error GoTo h_ERROR
    
    vSQLCondicionTipoArticuloInv = gUtilSQL.fSQLEnumValueWithAnd("", "articuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_Simple)
    vSQLCondicionTipoArticuloInv = "(" & vSQLCondicionTipoArticuloInv & ")" & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "articuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_UsaTalla_Color)
    vSQLCondicionTipoArticuloInv = "(" & vSQLCondicionTipoArticuloInv & ")"
    vSQLCondicionTipoArticuloInv = gUtilSQL.getWhereSQL(vSQLCondicionTipoArticuloInv)
 
    vSQL = " SELECT "
    vSQL = vSQL & " Compra.ConsecutivoCompania,"
    vSQL = vSQL & " Almacen.Codigo As CodigoAlmacen,"
    vSQL = vSQL & " CompraDetalleArticuloInventario.CodigoArticulo,"
    vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Serial, "
    vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Rollo,  "
    vSQL = vSQL & " MAX(Compra.Consecutivo) AS Consecutivo, "
    vSQL = vSQL & GetViewNameCompraFechaUltimaCompra() & ".FechaMax  "
    vSQL = vSQL & " FROM Adm.Compra "
    
    vSQL = vSQL & " INNER JOIN  Adm.CompraDetalleArticuloInventario ON"
    vSQL = vSQL & " Compra.ConsecutivoCompania =  CompraDetalleArticuloInventario.ConsecutivoCompania AND "
    vSQL = vSQL & " Compra.Consecutivo =   CompraDetalleArticuloInventario.ConsecutivoCompra "
    vSQL = vSQL & "  INNER JOIN Saw.Almacen ON"
    vSQL = vSQL & "  Compra.ConsecutivoCompania = Almacen.ConsecutivoCompania AND"
    vSQL = vSQL & "  Compra.ConsecutivoAlmacen = Almacen.Consecutivo"
    vSQL = vSQL & " INNER JOIN " & GetViewNameCompraFechaUltimaCompra() & " ON "
    vSQL = vSQL & "  Compra.ConsecutivoCompania =  " & GetViewNameCompraFechaUltimaCompra() & ".ConsecutivoCompania AND "
    vSQL = vSQL & "  Compra.ConsecutivoAlmacen = " & GetViewNameCompraFechaUltimaCompra() & ".ConsecutivoAlmacen AND "
    vSQL = vSQL & "  Compra.Fecha = " & GetViewNameCompraFechaUltimaCompra() & ".FechaMax AND   "
    vSQL = vSQL & "   CompraDetalleArticuloInventario.CodigoArticulo = " & GetViewNameCompraFechaUltimaCompra() & ".CodigoArticulo"
    
    vSQL = vSQL & "  INNER JOIN articuloInventario ON  "
    vSQL = vSQL & " CompraDetalleArticuloInventario.ConsecutivoCompania = articuloInventario.ConsecutivoCompania And "
    vSQL = vSQL & " CompraDetalleArticuloInventario.CodigoArticulo = articuloInventario.Codigo "
    
  
    vSQL = vSQL & vSQLCondicionTipoArticuloInv
    
    vSQL = vSQL & "  GROUP BY  "
    
    vSQL = vSQL & "  Compra.ConsecutivoCompania, "
    vSQL = vSQL & "  Almacen.Codigo, "
    vSQL = vSQL & "  CompraDetalleArticuloInventario.CodigoArticulo, "
  
    vSQL = vSQL & GetViewNameCompraFechaUltimaCompra() & ".FechaMax "
      
    vSQL = vSQL & " UNION "
   
    vSQL = vSQL & " SELECT "
    vSQL = vSQL & " Compra.ConsecutivoCompania,"
    vSQL = vSQL & " Almacen.Codigo As CodigoAlmacen,"
    vSQL = vSQL & " CompraDetalleSerialRollo.CodigoArticulo,"
    vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CompraDetalleSerialRollo.Serial"), gUtilSQL.fSimpleSqlValue("0"), "CompraDetalleSerialRollo.Serial", True) & " AS Serial, "
    vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CompraDetalleSerialRollo.Rollo"), gUtilSQL.fSimpleSqlValue("0"), "CompraDetalleSerialRollo.Rollo", True) & " AS Rollo,  "
    vSQL = vSQL & " MAX(Compra.Consecutivo) AS Consecutivo, "
    vSQL = vSQL & GetViewNameCompraFechaUltimaCompra() & ".FechaMax  "
    vSQL = vSQL & " FROM Adm.Compra "
    
    vSQL = vSQL & " INNER  JOIN Adm.CompraDetalleSerialRollo ON "
    vSQL = vSQL & "  Compra.ConsecutivoCompania =  CompraDetalleSerialRollo.ConsecutivoCompania AND "
    vSQL = vSQL & "  Compra.Consecutivo =     CompraDetalleSerialRollo.ConsecutivoCompra "
    
    vSQL = vSQL & "  INNER JOIN Saw.Almacen ON"
    vSQL = vSQL & "  Compra.ConsecutivoCompania = Almacen.ConsecutivoCompania AND"
    vSQL = vSQL & "  Compra.ConsecutivoAlmacen = Almacen.Consecutivo"
    
    vSQL = vSQL & " INNER JOIN " & GetViewNameCompraFechaUltimaCompra() & " ON "
    vSQL = vSQL & "  Compra.ConsecutivoCompania =  " & GetViewNameCompraFechaUltimaCompra() & ".ConsecutivoCompania AND "
    vSQL = vSQL & "  Almacen.Codigo = " & GetViewNameCompraFechaUltimaCompra() & ".CodigoAlmacen AND "
    vSQL = vSQL & "  Compra.Fecha = " & GetViewNameCompraFechaUltimaCompra() & ".FechaMax AND   "
    vSQL = vSQL & GetViewNameCompraFechaUltimaCompra() & ".CodigoArticulo =  CompraDetalleSerialRollo.CodigoArticulo  AND "
    vSQL = vSQL & GetViewNameCompraFechaUltimaCompra() & ".Serial = " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CompraDetalleSerialRollo.Serial"), gUtilSQL.fSimpleSqlValue("0"), "CompraDetalleSerialRollo.Serial", True) & " AND "
    vSQL = vSQL & GetViewNameCompraFechaUltimaCompra() & ".Rollo = " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("CompraDetalleSerialRollo.Rollo"), gUtilSQL.fSimpleSqlValue("0"), "CompraDetalleSerialRollo.Rollo", True)
    
    vSQL = vSQL & "  GROUP BY  "
    
    vSQL = vSQL & "  Compra.ConsecutivoCompania, "
    vSQL = vSQL & "  Almacen.Codigo, "
    vSQL = vSQL & "  CompraDetalleSerialRollo.CodigoArticulo, "
    vSQL = vSQL & "  CompraDetalleSerialRollo.Serial, "
    vSQL = vSQL & "  CompraDetalleSerialRollo.Rollo, "
    vSQL = vSQL & GetViewNameCompraFechaUltimaCompra() & ".FechaMax "
  fSqlViewComprasSecUltCom = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasSecUltCom", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameComprasConOsinSeriales() As String
       GetViewNameComprasConOsinSeriales = "IGV_ComprasConOsinSeriales"
End Function

Public Function fSqlViewComprasConOsinSeriales() As String
   Dim vSQL As String
   Dim vCondicion As String
   Dim vSQLWhere As String
   Dim vSQLCondicionTipoArticuloInv As String
   Dim vSQLWhere1 As String
   On Error GoTo h_ERROR
     
  vSQLCondicionTipoArticuloInv = gUtilSQL.fSQLEnumValueWithAnd(vCondicion, "articuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_Simple)
  vSQLCondicionTipoArticuloInv = "(" & vSQLCondicionTipoArticuloInv & ")" & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "articuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_UsaTalla_Color)
  vSQLCondicionTipoArticuloInv = "(" & vSQLCondicionTipoArticuloInv & ")"
  vSQLWhere = gUtilSQL.getWhereSQL(vSQLCondicionTipoArticuloInv)
  
   vSQL = vSQL & "  SELECT"
   vSQL = vSQL & "  Compra.ConsecutivoCompania,"
   vSQL = vSQL & "  Almacen.Codigo As CodigoAlmacen,"
   vSQL = vSQL & "  Compra.Numero,"
   vSQL = vSQL & "  Compra.StatusCompra,"
   vSQL = vSQL & "  CompraDetalleSerialRollo.CodigoArticulo,"
   vSQL = vSQL & "  Compra.Consecutivo,"
   vSQL = vSQL & gUtilSQL.fCast("Sum(CompraDetalleSerialRollo.Cantidad)", eTDSS_MONEY, "CantidadRecibida") & ", "
   vSQL = vSQL & "  CompraDetalleSerialRollo.Serial,"
   vSQL = vSQL & "  CompraDetalleSerialRollo.Rollo"
   vSQL = vSQL & "  From Adm.Compra"
   vSQL = vSQL & "  INNER JOIN Adm.CompraDetalleSerialRollo ON"
   vSQL = vSQL & "  Compra.ConsecutivoCompania = CompraDetalleSerialRollo.ConsecutivoCompania AND"
   vSQL = vSQL & "  Compra.Consecutivo = CompraDetalleSerialRollo.ConsecutivoCompra"
   vSQL = vSQL & "  INNER JOIN Saw.Almacen ON"
   vSQL = vSQL & "  Compra.ConsecutivoCompania = Almacen.ConsecutivoCompania AND"
   vSQL = vSQL & "  Compra.ConsecutivoAlmacen = Almacen.Consecutivo"
   vSQL = vSQL & "  Group By"
   vSQL = vSQL & "  Compra.ConsecutivoCompania,"
   vSQL = vSQL & "  Compra.ConsecutivoAlmacen,"
   vSQL = vSQL & "  Compra.Numero,"
   vSQL = vSQL & "  Compra.StatusCompra,"
   vSQL = vSQL & "  CompraDetalleSerialRollo.CodigoArticulo,"
   vSQL = vSQL & "  Compra.Consecutivo,"
   vSQL = vSQL & "  CompraDetalleSerialRollo.Serial,"
   vSQL = vSQL & "  CompraDetalleSerialRollo.Rollo,Almacen.Codigo"
   
   vSQL = vSQL & "   UNION "
   
   vSQL = vSQL & "   SELECT"
   vSQL = vSQL & "  Compra.ConsecutivoCompania,"
   vSQL = vSQL & "  Almacen.Codigo As CodigoAlmacen,"
   vSQL = vSQL & "  Compra.Numero,"
   vSQL = vSQL & "  Compra.StatusCompra,"
   vSQL = vSQL & "  CompraDetalleArticuloInventario.CodigoArticulo,"
   vSQL = vSQL & "  Compra.Consecutivo As ConsecutivoCompra,"
   vSQL = vSQL & gUtilSQL.fCast("SUM(Adm.CompraDetalleArticuloInventario.CantidadRecibida)", eTDSS_MONEY, "CantidadRecibida") & ","
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Serial,"
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS Rollo "
   vSQL = vSQL & "  FROM       Adm.Compra "
   vSQL = vSQL & " INNER JOIN Adm.CompraDetalleArticuloInventario ON"
   vSQL = vSQL & "  Compra.ConsecutivoCompania = CompraDetalleArticuloInventario.ConsecutivoCompania AND"
   vSQL = vSQL & "  Compra.Consecutivo = CompraDetalleArticuloInventario.ConsecutivoCompra"
   vSQL = vSQL & " INNER JOIN  articuloInventario ON "
   vSQL = vSQL & " CompraDetalleArticuloInventario.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND"
   vSQL = vSQL & " CompraDetalleArticuloInventario.CodigoArticulo = articuloInventario.Codigo"
   vSQL = vSQL & "  INNER JOIN Saw.Almacen ON"
   vSQL = vSQL & "  Compra.ConsecutivoCompania = Almacen.ConsecutivoCompania AND"
   vSQL = vSQL & "  Compra.ConsecutivoAlmacen = Almacen.Consecutivo"
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group By"
   vSQL = vSQL & "  Compra.ConsecutivoCompania,"
   vSQL = vSQL & "  Compra.ConsecutivoAlmacen,"
   vSQL = vSQL & "  Compra.Numero,"
   vSQL = vSQL & "  Compra.StatusCompra,"
   vSQL = vSQL & "  CompraDetalleArticuloInventario.CodigoArticulo,"
   vSQL = vSQL & "  Compra.Consecutivo,Almacen.Codigo"
   fSqlViewComprasConOsinSeriales = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasConOsinSeriales", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameComprasUltimaConOsinSeriales() As String
      GetViewNameComprasUltimaConOsinSeriales = "IGV_ComprasUltimaConOsinSeriales"
End Function
Public Function fSqlViewComprasUltimaConOsinSeriales() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
 
 vSQL = "  SELECT "
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".ConsecutivoCompania,"
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".CodigoAlmacen,"
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".CodigoArticulo,"
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".Serial,"
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".Rollo,"
 vSQL = vSQL & " IGV_ComprasSecUltCom.Consecutivo,"
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".CantidadRecibida"
 vSQL = vSQL & " FROM " & GetViewNameComprasConOsinSeriales()
 vSQL = vSQL & " INNER JOIN " & GetViewNameComprasSecUltCom() & " ON "
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".ConsecutivoCompania = " & GetViewNameComprasSecUltCom() & ".ConsecutivoCompania AND "
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".CodigoAlmacen = " & GetViewNameComprasSecUltCom() & ".CodigoAlmacen AND "
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".CodigoArticulo = " & GetViewNameComprasSecUltCom() & ".CodigoArticulo AND "
 vSQL = vSQL & GetViewNameComprasConOsinSeriales() & ".Consecutivo = " & GetViewNameComprasSecUltCom() & ".Consecutivo "
 fSqlViewComprasUltimaConOsinSeriales = vSQL
h_EXIT:    On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasUltimaConOsinSeriales", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewArticuloXAlmacen_pIEB1() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT  "
   vSQL = vSQL & "ExistenciaPorAlmacen.CodigoAlmacen, "
   vSQL = vSQL & "Almacen.NombreAlmacen, "
   vSQL = vSQL & "ExistenciaPorAlmacen.CodigoArticulo, "
   vSQL = vSQL & gUtilSQL.getIIF("ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_UsaTalla_Color), GetViewNameInventarioExistenciaXGrupoMod_B1() & ".Descripcion", _
   gUtilSQL.getIIF("ArticuloInventario.TipoArticuloInv = " & gUtilSQL.fSimpleSqlValue(enum_TipoArticuloInv.eTAI_UsaTalla_ColorySerial), GetViewNameInventarioExistenciaXGrupoMod_B1() & ".Descripcion", "ArticuloInventario.Descripcion")) & " as Descripcion, "
   vSQL = vSQL & "ArticuloInventario.precioConIva, "
   vSQL = vSQL & "ArticuloInventario.precioSinIva, "
   vSQL = vSQL & "ExistenciaPorAlmacen.Cantidad as Existencia, "
   vSQL = vSQL & "ArticuloInventario.TipoArticuloInv, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("ArticuloInventario.TipoArticuloInv", enum_TipoArticuloInv.eTAI_Simple, gEnumProyecto.fEnumTipoArticuloInvToStringInArray(True), "DescTipoArticuloInv") & ", "
   vSQL = vSQL & "ArticuloInventario.ConsecutivoCompania, "
   vSQL = vSQL & "Compania.Nombre as NombreCompania "
   vSQL = vSQL & " FROM "
   vSQL = vSQL & " ExistenciaPorAlmacen"
   vSQL = vSQL & " INNER JOIN ArticuloInventario ON (ExistenciaPorAlmacen.CodigoArticulo = ArticuloInventario.Codigo  and ExistenciaPorAlmacen.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania )"
   vSQL = vSQL & " LEFT OUTER JOIN " & GetViewNameInventarioExistenciaXGrupoMod_B1() & _
         " ON (" & GetViewNameInventarioExistenciaXGrupoMod_B1() & ".codigo = ExistenciaPorAlmacen.CodigoArticulo and " & GetViewNameInventarioExistenciaXGrupoMod_B1() & ".ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania)"
   vSQL = vSQL & " INNER JOIN Almacen ON (ExistenciaPorAlmacen.CodigoAlmacen = Almacen.Codigo and ExistenciaPorAlmacen.ConsecutivoCompania = Almacen.ConsecutivoCompania) "
   vSQL = vSQL & " INNER JOIN Compania ON (ExistenciaPorAlmacen.ConsecutivoCompania = Compania.ConsecutivoCompania)"
   
   Set gEnumProyecto = Nothing
   fSqlViewArticuloXAlmacen_pIEB1 = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticuloXAlmacen_pIEB1", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameArticuloXAlmacen_pIEB1() As String
   GetViewNameArticuloXAlmacen_pIEB1 = "IGV_ArticuloXAlmacen_pIEB1"
End Function

Public Function fSqlViewInventarioExistenciaXGrupoMod_B1() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT "
   vSQL = vSQL & GetViewNameInventarioExistenciaXGrupo() & ".codigo,"
   vSQL = vSQL & GetViewNameInventarioExistenciaXGrupo() & ".descripcion,"
   vSQL = vSQL & GetViewNameInventarioExistenciaXGrupo() & ".ConsecutivoCompania"
   vSQL = vSQL & " FROM " & GetViewNameInventarioExistenciaXGrupo()
   vSQL = vSQL & " GROUP BY "
   vSQL = vSQL & GetViewNameInventarioExistenciaXGrupo() & ".codigo,"
   vSQL = vSQL & GetViewNameInventarioExistenciaXGrupo() & ".descripcion,"
   vSQL = vSQL & GetViewNameInventarioExistenciaXGrupo() & ".ConsecutivoCompania"
   fSqlViewInventarioExistenciaXGrupoMod_B1 = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewInventarioExistenciaXGrupoMod_B1", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameInventarioExistenciaXGrupoMod_B1() As String
   GetViewNameInventarioExistenciaXGrupoMod_B1 = "IGV_InventarioExistenciaXGrupoMod_B1"
End Function
Private Function fSqlSelectViewArticuloCompraAlmacen() As String
   Dim vSQL As String
   Dim vSQLWhere   As String
   Dim insComprasVista As clsComprasVista
   On Error GoTo h_ERROR
   Set insComprasVista = New clsComprasVista
   
   vSQLWhere = insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".EsUnaOrdenDeCompra = " & gUtilSQL.fBooleanToSqlValue(False)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".StatusCompra", enum_StatusArticulo.eSA_VIGENTE)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQL = " SELECT "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos() & ".CodigoAlmacen AS CodigoAlmacen,"
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Fecha,"
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("COMPRA") & " AS TipoDeDocumento, "
   vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida"), gUtilSQL.fNumToStrSQL(0), insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CantidadRecibida", True) & " ) " & " AS Entrada, "
   vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS Salidad"
   
   vSQL = vSQL & " FROM " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & " ON "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".ConsecutivoCompania AND "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo =   " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".CodigoArticulo AND "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".serial = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Serial AND "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".rollo = " & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Rollo "

   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group By "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania, "
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos() & ".CodigoAlmacen, "
   vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
   vSQL = vSQL & insComprasVista.GetViewNameComprasTotalCompraSinOtrosGastos & ".Fecha "
   Set insComprasVista = Nothing
   
  fSqlSelectViewArticuloCompraAlmacen = vSQL
   Exit Function
h_ERROR:    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fSqlSelectViewArticuloCompraAlmacen()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlSelectViewArticuloConteoFisicoAlmacen() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " CodigoAlmacen AS CodigoAlmacen,"
    vSQL = vSQL & " Codigo AS  Codigo ,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento AS TipoDeDocumento , "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameConteoFisicoDeArticulo() & ".Entradas"), gUtilSQL.fNumToStrSQL(0), GetViewNameConteoFisicoDeArticulo() & ".Entradas", True) & " ) " & " AS Entrada, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameConteoFisicoDeArticulo() & ".Salidas"), gUtilSQL.fNumToStrSQL(0), GetViewNameConteoFisicoDeArticulo() & ".Salidas", True) & " ) " & " AS Salidad "
    vSQL = vSQL & " FROM  " & GetViewNameConteoFisicoDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania, CodigoAlmacen,Codigo,  Fecha, TipoDeDocumento"
   fSqlSelectViewArticuloConteoFisicoAlmacen = vSQL
 Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectViewArticuloConteoFisicoAlmacen()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fSqlSelectViewArticuloInvMovimientoVentaAlmacen() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " CodigoAlmacen AS CodigoAlmacen,"
    vSQL = vSQL & " Codigo AS  Codigo ,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento AS TipoDeDocumento , "
    vSQL = vSQL & " SUM ( " & fEntradaSegunElCaso() & ") AS entrada,"
    vSQL = vSQL & " SUM ( " & fSalidasSegunElCaso() & ") AS salidad"
    vSQL = vSQL & " FROM  " & GetViewNameVentaDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania, CodigoAlmacen, Codigo, Fecha,TipoDeDocumento"
   fSqlSelectViewArticuloInvMovimientoVentaAlmacen = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectViewArticuloInvMovimientoVentaAlmacen", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlSelectViewArticuloInvMovimientoNSAlmacen() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
h_EXIT:   On Error GoTo 0
    vSQL = " SELECT     ConsecutivoCompania AS ConsecutivoCompania, "
    vSQL = vSQL & " CodigoAlmacen AS CodigoAlmacen,"
    vSQL = vSQL & " Codigo AS  Codigo ,"
    vSQL = vSQL & " Fecha AS Fecha,"
    vSQL = vSQL & " TipoDeDocumento  , "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameNotaEntradaSalidaDeArticulo() & ".Entradas"), gUtilSQL.fNumToStrSQL(0), GetViewNameNotaEntradaSalidaDeArticulo() & ".Entradas", True) & ") " & " AS Entrada, "
    vSQL = vSQL & " SUM ( " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(GetViewNameNotaEntradaSalidaDeArticulo() & ".Salidas"), gUtilSQL.fNumToStrSQL(0), GetViewNameNotaEntradaSalidaDeArticulo() & ".Salidas", True) & ") " & " AS Salidad  "
    vSQL = vSQL & " FROM " & GetViewNameNotaEntradaSalidaDeArticulo()
    vSQL = vSQL & " GROUP BY ConsecutivoCompania,CodigoAlmacen, Codigo, Fecha, TipoDeDocumento "
   fSqlSelectViewArticuloInvMovimientoNSAlmacen = vSQL
 Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " fSqlSelectViewArticuloInvMovimientoAlmacenNS", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameArticuloInvMovimientoAlmacen() As String
  GetViewNameArticuloInvMovimientoAlmacen = "IGV_ArticuloInvMovimientosAlmacen"
End Function
Public Function fSqlViewArticuloInvMovimientosAlmacen() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSqlSelectViewArticuloInvMovimientoNSAlmacen()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloInvMovimientoVentaAlmacen()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloConteoFisicoAlmacen()
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlSelectViewArticuloCompraAlmacen()
   fSqlViewArticuloInvMovimientosAlmacen = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticuloInvMovimientosAlmacen", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameTransferenciasPorArticulo() As String
  GetViewNameTransferenciasPorArticulo = "IGV_TransferenciasPorArticulo"
End Function
Public Function fSqlViewTransferenciasPorArticulo() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
      vSQL = " SELECT "
      vSQL = vSQL & " almacen.ConsecutivoCompania,"
      vSQL = vSQL & " almacen.Codigo,"
      vSQL = vSQL & "transferencia.Fecha,"
      vSQL = vSQL & "renglonTransferencia.CodigoArticulo,"
      vSQL = vSQL & "renglonTransferencia.Serial,"
      vSQL = vSQL & "renglonTransferencia.Rollo,"
      vSQL = vSQL & " SUM(renglonTransferencia.Cantidad) AS Entrada,"
      vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS salida"
      vSQL = vSQL & " FROM almacen "
      vSQL = vSQL & " INNER JOIN      transferencia ON "
      vSQL = vSQL & " almacen.ConsecutivoCompania = transferencia.ConsecutivoCompania AND"
      vSQL = vSQL & " almacen.Codigo = transferencia.CodigoAlmacenEntrada"
      vSQL = vSQL & " INNER JOIN  renglonTransferencia ON "
      vSQL = vSQL & " transferencia.ConsecutivoCompania = renglonTransferencia.ConsecutivoCompania AND"
      vSQL = vSQL & " transferencia.NumeroDocumento = renglonTransferencia.NumeroDocumento"
      vSQL = vSQL & " Group By "
      vSQL = vSQL & " almacen.ConsecutivoCompania,"
      vSQL = vSQL & " almacen.Codigo,"
      vSQL = vSQL & " transferencia.Fecha,"
      vSQL = vSQL & " renglonTransferencia.CodigoArticulo,"
      vSQL = vSQL & " renglonTransferencia.Serial,"
      vSQL = vSQL & " renglonTransferencia.Rollo"
      vSQL = vSQL & " UNION  "
      vSQL = vSQL & " SELECT"
      vSQL = vSQL & " almacen_1.ConsecutivoCompania,"
      vSQL = vSQL & " almacen_1.Codigo,"
      vSQL = vSQL & " transferencia_1.Fecha,"
      vSQL = vSQL & " renglonTransferencia_1.CodigoArticulo,"
      vSQL = vSQL & " renglonTransferencia_1.Serial,"
      vSQL = vSQL & " renglonTransferencia_1.Rollo,"
      vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS Entrada,"
      vSQL = vSQL & " SUM(renglonTransferencia_1.Cantidad) As salida"
      vSQL = vSQL & " FROM         dbo.almacen AS almacen_1"
      vSQL = vSQL & " INNER JOIN  dbo.transferencia AS transferencia_1 ON"
      vSQL = vSQL & " almacen_1.ConsecutivoCompania = transferencia_1.ConsecutivoCompania AND"
      vSQL = vSQL & " almacen_1.Codigo = transferencia_1.CodigoAlmacenSalida"
      vSQL = vSQL & " INNER JOIN   dbo.renglonTransferencia AS renglonTransferencia_1 ON"
      vSQL = vSQL & " transferencia_1.ConsecutivoCompania = renglonTransferencia_1.ConsecutivoCompania AND"
      vSQL = vSQL & " transferencia_1.NumeroDocumento = renglonTransferencia_1.NumeroDocumento"
      vSQL = vSQL & " Group By"
      vSQL = vSQL & " almacen_1.ConsecutivoCompania,"
      vSQL = vSQL & " almacen_1.Codigo,"
      vSQL = vSQL & " transferencia_1.Fecha,"
      vSQL = vSQL & " renglonTransferencia_1.CodigoArticulo,"
      vSQL = vSQL & " renglonTransferencia_1.Serial,"
      vSQL = vSQL & " renglonTransferencia_1.Rollo"
fSqlViewTransferenciasPorArticulo = vSQL

h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewTransferenciasPorArticulo", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameTransferenciasPorAlmacen() As String
  GetViewNameTransferenciasPorAlmacen = "IGV_TransferenciasPorAlmacen"
End Function

Public Function fSqlViewTransferenciasPorAlmacen() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
      vSQL = " SELECT "
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania,"
      vSQL = vSQL & " TransferenciasPorArticulo.CODIGO  AS CodigoAlmacen,"
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,"
      vSQL = vSQL & " TransferenciasPorArticulo.Fecha,"
      vSQL = vSQL & " SUM(TransferenciasPorArticulo.Entrada) AS Entrada,"
      vSQL = vSQL & " SUM(TransferenciasPorArticulo.salida) As salida"
      vSQL = vSQL & " FROM " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & " AS ArticulosDeInventarioConOSinExistenPorGrupo_B1 "
      vSQL = vSQL & " INNER JOIN " & GetViewNameTransferenciasPorArticulo & " AS TransferenciasPorArticulo ON "
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = TransferenciasPorArticulo.ConsecutivoCompania AND"
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = TransferenciasPorArticulo.CodigoArticulo AND"
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = TransferenciasPorArticulo.Serial AND"
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = TransferenciasPorArticulo.Rollo"
      vSQL = vSQL & " Group By "
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania,"
      vSQL = vSQL & " TransferenciasPorArticulo.CODIGO ,"
      vSQL = vSQL & " ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,"
      vSQL = vSQL & " TransferenciasPorArticulo.Fecha"
      fSqlViewTransferenciasPorAlmacen = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewTransferenciasPorAlmacen", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameArticulosMovimientoTransferencia() As String
  GetViewNameArticulosMovimientoTransferencia = "IGV_ArticulosMovimientoTransferencia"
End Function

Public Function fSqlViewArticulosMovimientoTransferencia() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " ConsecutivoCompania,"
   vSQL = vSQL & " CodigoAlmacen,"
   vSQL = vSQL & " 'Transferencias' AS TipoDeDocumento,"
   vSQL = vSQL & " Codigo,"
   vSQL = vSQL & " Fecha,"
   vSQL = vSQL & " Entrada,"
   vSQL = vSQL & " Salida"
   vSQL = vSQL & " FROM " & GetViewNameTransferenciasPorAlmacen()
   vSQL = vSQL & " Union"
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " ConsecutivoCompania,"
   vSQL = vSQL & " CodigoAlmacen,"
   vSQL = vSQL & " TipoDeDocumento,"
   vSQL = vSQL & " Codigo,"
   vSQL = vSQL & " Fecha,"
   vSQL = vSQL & " Entrada,"
   vSQL = vSQL & " Salidad As Salida"
   vSQL = vSQL & " FROM " & GetViewNameArticuloInvMovimientoAlmacen()
fSqlViewArticulosMovimientoTransferencia = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticulosMovimientoTransferencia", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function GetViewNameProductosCompuestos_B1() As String
  GetViewNameProductosCompuestos_B1 = "IGV_ProductosCompuestos_B1"
End Function

Public Function fSqlViewProductosCompuestos_B1() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " dbo.ArticuloInventario.ConsecutivoCompania,"
   vSQL = vSQL & " dbo.ArticuloInventario.Codigo,"
   vSQL = vSQL & " dbo.ArticuloInventario.Descripcion,"
   vSQL = vSQL & " dbo.ProductoCompuesto.CodigoArticulo,"
   vSQL = vSQL & " dbo.ProductoCompuesto.Cantidad,"
   vSQL = vSQL & " dbo.ArticuloInventario.AlicuotaIva,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioSinIva,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioSinIva2,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioConIva,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioConIva2,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioSinIva3,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioConIva3,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioSinIva4,"
   vSQL = vSQL & " dbo.ArticuloInventario.PrecioConIva4"
   vSQL = vSQL & " FROM dbo.ArticuloInventario"
   vSQL = vSQL & " INNER JOIN   dbo.ProductoCompuesto ON "
   vSQL = vSQL & " dbo.ArticuloInventario.ConsecutivoCompania = dbo.ProductoCompuesto.ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.ArticuloInventario.Codigo = dbo.ProductoCompuesto.CodigoConexionConElMaster"
  fSqlViewProductosCompuestos_B1 = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewProductosCompuestos_B1", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function GetViewNameArticulosProductosCompuestos() As String
  GetViewNameArticulosProductosCompuestos = "IGV_ArticulosCompuestos"
End Function

Public Function fSqlViewArticulosCompuestos() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
  vSQL = vSQL & " SELECT "
  vSQL = vSQL & " dbo.ArticuloInventario.ConsecutivoCompania,"
  vSQL = vSQL & " dbo.ArticuloInventario.Codigo,"
  vSQL = vSQL & " dbo.ArticuloInventario.Descripcion,"
  vSQL = vSQL & " dbo.ArticuloInventario.LineaDeProducto,"
  vSQL = vSQL & " dbo.ArticuloInventario.StatusdelArticulo,"
  vSQL = vSQL & " dbo.ArticuloInventario.TipoDeArticulo,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".AlicuotaIva,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioSinIva,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioSinIva2,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioConIva,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioConIva2,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioSinIva3,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioConIva3,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioSinIva4,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".PrecioConIva4,"
  vSQL = vSQL & " dbo.ArticuloInventario.PorcentajeBaseImponible,"
  vSQL = vSQL & " dbo.ArticuloInventario.CostoUnitario,"
  vSQL = vSQL & " dbo.ArticuloInventario.Existencia,"
  vSQL = vSQL & " dbo.ArticuloInventario.Categoria,"
  vSQL = vSQL & " dbo.ArticuloInventario.TipoDeProducto,"
  vSQL = vSQL & " dbo.ArticuloInventario.Marca,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".Descripcion AS DescripcionCompuesto,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".Codigo AS Compuesto,"
  vSQL = vSQL & GetViewNameProductosCompuestos_B1() & ".Cantidad"
  vSQL = vSQL & " FROM dbo.ArticuloInventario "
  vSQL = vSQL & " INNER JOIN  " & GetViewNameProductosCompuestos_B1() & " ON "
  vSQL = vSQL & " dbo.ArticuloInventario.ConsecutivoCompania = " & GetViewNameProductosCompuestos_B1() & ".ConsecutivoCompania AND "
  vSQL = vSQL & " dbo.ArticuloInventario.Codigo = " & GetViewNameProductosCompuestos_B1() & ".CodigoArticulo "
  fSqlViewArticulosCompuestos = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticulosCompuestos", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameInventarioReservas() As String
  GetViewNameInventarioReservas = "IGV_InventarioReservas_B1"
End Function

Public Function fSqlViewInventarioReservas() As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
    vSQLWhere = "dbo.factura.ReservarMercancia = " & gUtilSQL.fBooleanToSqlValue(True)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)

    vSQL = " SELECT "
    vSQL = vSQL & " dbo.factura.ConsecutivoCompania,"
    vSQL = vSQL & " dbo.factura.Numero,"
    vSQL = vSQL & " dbo.factura.Fecha,"
    vSQL = vSQL & " dbo.renglonFactura.Articulo AS CodigoArticulo,"
    vSQL = vSQL & " dbo.renglonFactura.Serial,"
    vSQL = vSQL & " dbo.renglonFactura.Rollo,"
    vSQL = vSQL & " dbo.renglonFactura.Cantidad,"
    vSQL = vSQL & gUtilSQL.fNumToStrSQL(0) & " AS CantidadDespachada,"
    vSQL = vSQL & " dbo.factura.ReservarMercancia,"
    vSQL = vSQL & " dbo.factura.FechaDeRetiro "
    vSQL = vSQL & " FROM dbo.factura"
    vSQL = vSQL & " INNER JOIN   dbo.renglonFactura ON "
    vSQL = vSQL & " dbo.factura.ConsecutivoCompania = dbo.renglonFactura.ConsecutivoCompania AND"
    vSQL = vSQL & " dbo.factura.Numero = dbo.renglonFactura.NumeroFactura AND"
    vSQL = vSQL & " dbo.factura.TipoDeDocumento = dbo.renglonFactura.TipoDeDocumento"
    vSQL = vSQL & vSQLWhere
    vSQL = vSQL & " AND dbo.factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
  
    vSQLWhere = "dbo.cotizacion.ReservarMercancia = " & gUtilSQL.fBooleanToSqlValue(True)
    vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
    
    vSQL = vSQL & " UNION "
    vSQL = vSQL & " SELECT "
    vSQL = vSQL & " dbo.cotizacion.ConsecutivoCompania,"
    vSQL = vSQL & " dbo.cotizacion.Numero,"
    vSQL = vSQL & " dbo.cotizacion.Fecha,"
    vSQL = vSQL & " dbo.renglonCotizacion.CodigoArticulo,"
    vSQL = vSQL & " dbo.renglonCotizacion.Serial,"
    vSQL = vSQL & " dbo.renglonCotizacion.Rollo,"
    vSQL = vSQL & " dbo.renglonCotizacion.Cantidad,"
    vSQL = vSQL & " isnull(dbo.renglonCotizacion.CantidadDespachada, 0) AS CantidadDespachada,"
    vSQL = vSQL & " dbo.cotizacion.ReservarMercancia,"
    vSQL = vSQL & " dbo.cotizacion.FechaDeRetiro"
    vSQL = vSQL & " FROM "
    vSQL = vSQL & " dbo.cotizacion "
    vSQL = vSQL & " INNER JOIN  dbo.renglonCotizacion ON "
    vSQL = vSQL & " dbo.cotizacion.ConsecutivoCompania = dbo.renglonCotizacion.ConsecutivoCompania AND "
    vSQL = vSQL & " dbo.cotizacion.Numero = dbo.renglonCotizacion.NumeroCotizacion "
    vSQL = vSQL & vSQLWhere
  
  fSqlViewInventarioReservas = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "SqlViewInventarioReservas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameInventarioConOSinReservas() As String
  GetViewNameInventarioConOSinReservas = "IGV_InventarioConOSinReservas_B1"
End Function

Public Function fSqlViewInventarioConOSinReservas() As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
    vSQL = " SELECT "
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Existencia,"
    vSQL = vSQL & " SUM(ISNULL( " & GetViewNameInventarioReservas() & ".Cantidad, 0)) AS CantidadReservada,"
    vSQL = vSQL & " SUM(isnull(" & GetViewNameInventarioReservas() & ".CantidadDespachada, 0)) As CantidadDespachada"
    vSQL = vSQL & " FROM " & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
    vSQL = vSQL & " LEFT  JOIN  " & GetViewNameInventarioReservas() & "  ON "
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & GetViewNameInventarioReservas() & ".ConsecutivoCompania AND "
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & GetViewNameInventarioReservas() & ".CodigoArticulo AND "
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo = " & GetViewNameInventarioReservas() & ".Rollo AND "
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial = " & GetViewNameInventarioReservas() & ".Serial"
    vSQL = vSQL & " Group By "
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Serial,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Rollo,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Descripcion,"
    vSQL = vSQL & GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Existencia"
    fSqlViewInventarioConOSinReservas = vSQL
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "SqlViewInventarioConOSinReservas", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameArticulosReservadosPorAlmacen() As String
   GetViewNameArticulosReservadosPorAlmacen = "IGV_ArticulosReservadosPorAlmacen"
End Function

Public Function fSqlViewArticulosReservadosPorAlmacen() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   vSQL = vSQL & "SELECT Factura.ConsecutivoCompania,   "
   vSQL = vSQL & " Factura.CodigoAlmacen,"
   vSQL = vSQL & " Articulo AS CodigoArticulo ,"
   vSQL = vSQL & " Sum(Cantidad) AS CantArtReservados"
   vSQL = vSQL & " FROM RenglonFactura "
   vSQL = vSQL & " INNER JOIN  Factura ON "
   vSQL = vSQL & " RenglonFactura.NumeroFactura = Factura.Numero AND "
   vSQL = vSQL & " RenglonFactura.ConsecutivoCompania = Factura.ConsecutivoCompania "
   vSQL = vSQL & " WHERE Factura.ReservarMercancia = 'S'  AND "
   vSQL = vSQL & " RenglonFactura.TipoDeDocumento NOT IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & "," & _
                   gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & ")" & _
                   " GROUP BY Factura.ConsecutivoCompania,Factura.CodigoAlmacen,Articulo"
   fSqlViewArticulosReservadosPorAlmacen = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticulosReservadosPorAlmacen", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameArticulosReservadosPorCotizacion() As String
   GetViewNameArticulosReservadosPorCotizacion = "IGV_ArticulosReservadosPorCotizacion"
End Function

Public Function fSqlViewArticulosReservadosPorCotizacion() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   vSQL = vSQL & "SELECT Cotizacion.ConsecutivoCompania, CodigoArticulo,Sum(Cantidad) AS CantArtReservados "
   vSQL = vSQL & " FROM RenglonCotizacion "
   vSQL = vSQL & " INNER JOIN  Cotizacion ON "
   vSQL = vSQL & " RenglonCotizacion.NumeroCotizacion = Cotizacion.Numero AND "
   vSQL = vSQL & " RenglonCotizacion.ConsecutivoCompania = Cotizacion.ConsecutivoCompania "
   vSQL = vSQL & " WHERE Cotizacion.ReservarMercancia = 'S'  "
   vSQL = vSQL & " GROUP BY Cotizacion.ConsecutivoCompania,CodigoArticulo"
   fSqlViewArticulosReservadosPorCotizacion = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewArticulosReservadosPorCotizacion", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

