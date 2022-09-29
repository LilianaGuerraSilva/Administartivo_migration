VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsComprasVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsComprasVista"
Private Const CM_MESSAGE_NAME As String = "Vista Compras"
Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function
Public Function GetViewNameComprasTotalCompraSinOtrosGastos() As String
   GetViewNameComprasTotalCompraSinOtrosGastos = "IGV_ComprasTotalesSinOtrosGastos"
End Function

Public Function fSqlViewComprasTotalCompraSinGastos() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
   vSQL = ""
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & " Compra.ConsecutivoCompania,"
   vSQL = vSQL & " Compra.Consecutivo AS ConsecutivoCompra,"
   vSQL = vSQL & " Compra.Numero,"
   vSQL = vSQL & " Compra.NumeroSecuencial,"
   vSQL = vSQL & " Compra.StatusCompra,"
   vSQL = vSQL & " Compra.Fecha,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial() & ".CodigoArticulo,"
   vSQL = vSQL & " EsUnaOrdenDeCompra,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial() & ".CantidadRecibida,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial() & ".CostoUnitario,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial() & ".CantidadRecibida * " & GetViewNameRenglonComprasConOSinSerial() & ".CostoUnitario AS TotalCompraSinOtrosGastos, "
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial() & ".Serial" & " AS Serial, "
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial() & ".Rollo" & " AS Rollo, "
   vSQL = vSQL & " Compra.CodigoAlmacen "
   
   vSQL = vSQL & "  FROM "
   
   vSQL = vSQL & " Compra  INNER JOIN "
   
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial() & " ON "
   vSQL = vSQL & " dbo.compra.ConsecutivoCompania = " & GetViewNameRenglonComprasConOSinSerial() & ".ConsecutivoCompania AND "
   vSQL = vSQL & " dbo.compra.NumeroSecuencial = " & GetViewNameRenglonComprasConOSinSerial() & ".NumeroSecuencialCompra"
   
   fSqlViewComprasTotalCompraSinGastos = vSQL
   
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasTotalCompraSinGastos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameComprasOtroGastosPorItem() As String
   GetViewNameComprasOtroGastosPorItem = "IGV_ComprasOtroGastosPorItem"
End Function

Public Function fSqlViewComprasOtroGastosPorItem() As String
   Dim vSQL As String
   Dim vWhere As String
   Dim vCondicion As String
   On Error GoTo h_ERROR
   vWhere = "Compra.EsUnaOrdenDeCompra = " & gUtilSQL.fSimpleSqlValue("N")
   vWhere = gUtilSQL.getWhereSQL(vWhere)
  vSQL = ""
  vSQL = vSQL & " SELECT "
  vSQL = vSQL & " Compra.ConsecutivoCompania, "
  vSQL = vSQL & " Compra.Consecutivo AS ConsecutivoCompra, "
  vSQL = vSQL & " Compra.Numero,"
  vSQL = vSQL & " Compra.NumeroSecuencial,"
  vSQL = vSQL & " Compra.StatusCompra,"
  vSQL = vSQL & " Compra.Fecha,"
  vSQL = vSQL & "Compra.EsUnaOrdenDeCompra,"
  vSQL = vSQL & fSqlOtrosGastosPorItemSegunCondicion() & " /  " & fSqlDivOtrosGastosSegunCondicion() & " AS OtrosGastosPorItem "
  vSQL = vSQL & " FROM     Compra INNER JOIN "
  vSQL = vSQL & " RenglonCompra ON"
  vSQL = vSQL & " Compra.ConsecutivoCompania = RenglonCompra.ConsecutivoCompania AND "
  vSQL = vSQL & " Compra.NumeroSecuencial = RenglonCompra.NumeroSecuencialCompra"
  vSQL = vSQL & vWhere
  vSQL = vSQL & " Group By "
  vSQL = vSQL & " Compra.ConsecutivoCompania,"
  vSQL = vSQL & " Compra.Consecutivo,"
  vSQL = vSQL & " Compra.Numero,"
  vSQL = vSQL & " Compra.NumeroSecuencial,"
  vSQL = vSQL & " Compra.StatusCompra,"
  vSQL = vSQL & " Compra.Fecha,"
  vSQL = vSQL & " Compra.EsUnaOrdenDeCompra,"
  vSQL = vSQL & " Compra.OtrosGastos,"
  vSQL = vSQL & " Compra.DistribuirGastos,"
  vSQL = vSQL & " Compra.PorcentajeDeDistribucion"
 
 fSqlViewComprasOtroGastosPorItem = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasOtroGastosPorItem", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlOtrosGastosPorItemSegunCondicion() As String
 Dim vResult As String
 Dim vCondicion As String
 Dim vOperacion As String
 On Error GoTo h_ERROR
 vOperacion = ""
 vResult = ""
 vOperacion = " (Compra.OtrosGastos * ( Compra.PorcentajeDeDistribucion / 100 ) )  "
 vCondicion = gUtilSQL.fSQLEnumValueWithAnd("", " Compra.DistribuirGastos", enum_MontoPorcentaje.eMP_POR_PORCENTAJE)
 vResult = gUtilSQL.getIIF(vCondicion, vOperacion, "Compra.OtrosGastos", True)
 fSqlOtrosGastosPorItemSegunCondicion = vResult
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlOtrosGastosPorItemSegunCondicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 
 End Function
Private Function fSqlDivOtrosGastosSegunCondicion() As String
 Dim vResult As String
 Dim vCondicion  As String
 On Error GoTo h_ERROR
  
  vCondicion = " SUM(RenglonCompra.CantidadRecibida) > 0 "
  vResult = gUtilSQL.getIIF(vCondicion, " SUM(RenglonCompra.CantidadRecibida)", "1", True)
  fSqlDivOtrosGastosSegunCondicion = vResult
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDivOtrosGastosSegunCondicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 
 End Function

Public Function GetViewNameRenglonComprasConOSinSerial() As String
   GetViewNameRenglonComprasConOSinSerial = "IGV_RenglonComprasConOSinSerial_B1"
End Function
Public Function fSqlViewRenglonComprasConOSinSerial() As String
  Dim vSQL As String
 On Error GoTo h_ERROR
   
   vSQL = vSQL & " SELECT"
   vSQL = vSQL & " dbo.RenglonCompra.ConsecutivoCompania,"
   vSQL = vSQL & " dbo.RenglonCompra.NumeroSecuencialCompra,"
   vSQL = vSQL & " dbo.RenglonCompra.CodigoArticulo,"
   vSQL = vSQL & " dbo.RenglonCompra.CostoUnitario,"
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.RenglonCompraXSerial.Serial"), gUtilSQL.fSimpleSqlValue("0"), "dbo.RenglonCompraXSerial.Serial", True) & " AS Serial, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.RenglonCompraXSerial.Rollo"), gUtilSQL.fSimpleSqlValue("0"), "dbo.RenglonCompraXSerial.Rollo", True) & " AS Rollo, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("dbo.RenglonCompraXSerial.Serial"), "dbo.RenglonCompra.CantidadRecibida", "RenglonCompraXSerial.Cantidad", True) & " AS cantidadrecibida "
   vSQL = vSQL & " FROM dbo.RenglonCompra"
   vSQL = vSQL & " LEFT JOIN  dbo.RenglonCompraXSerial ON"
   vSQL = vSQL & " dbo.RenglonCompra.ConsecutivoCompania = dbo.RenglonCompraXSerial.ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.RenglonCompra.CodigoArticulo = dbo.RenglonCompraXSerial.CodigoArticulo AND"
   vSQL = vSQL & " dbo.RenglonCompra.NumeroSecuencialCompra = dbo.RenglonCompraXSerial.NumeroSecuencialCompra"
   fSqlViewRenglonComprasConOSinSerial = vSQL
h_EXIT:      On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRenglonComprasConOSinSerial", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
 
Public Function GetViewNameComprasConCambioOriginal() As String
   GetViewNameComprasConCambioOriginal = "IGV_ComprasConCambioOriginal"
End Function

Public Function fSqlViewComprasConCambioOriginal() As String
  Dim insCambioVista As clsCambioVista
  Dim vSQL As String
 On Error GoTo h_ERROR
   Set insCambioVista = New clsCambioVista
   
   vSQL = " SELECT"
   vSQL = vSQL & " dbo.Compra.ConsecutivoCompania,"
   vSQL = vSQL & " dbo.Compra.Numero,"
   vSQL = vSQL & " dbo.Compra.Fecha,"
   vSQL = vSQL & " dbo.Compra.CodigoProveedor,"
   vSQL = vSQL & " dbo.Proveedor.NombreProveedor,"
   vSQL = vSQL & " dbo.Compra.Moneda, "
   vSQL = vSQL & " dbo.Compra.CambioAbolivares, "
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".cantidadrecibida * " & GetViewNameRenglonComprasConOSinSerial & ".CostoUnitario AS TotalRenglones, "
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".cantidadrecibida * " & GetViewNameRenglonComprasConOSinSerial & ".CostoUnitario * " & "dbo.Compra.CambioAbolivares AS  TotalRenglonesCambio, "
   vSQL = vSQL & " dbo.IGV_CambioAlaFecha.CambioAbolivares AS cambioalafecha,"
   vSQL = vSQL & " dbo.Compra.NumeroSecuencial,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".CodigoArticulo,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".CostoUnitario,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".Serial,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".Rollo,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".cantidadrecibida,"
   vSQL = vSQL & " dbo.Compra.CodigoAlmacen,"
   vSQL = vSQL & " dbo.Compra.EsUnaOrdenDeCompra,"
   vSQL = vSQL & " dbo.Compra.StatusCompra "
   vSQL = vSQL & " FROM dbo.Compra"
   vSQL = vSQL & " INNER JOIN  dbo.Proveedor ON"
   vSQL = vSQL & " dbo.Compra.ConsecutivoCompania = dbo.Proveedor.ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.Compra.CodigoProveedor = dbo.Proveedor.CodigoProveedor"
   vSQL = vSQL & " INNER JOIN " & GetViewNameRenglonComprasConOSinSerial & " ON"
   vSQL = vSQL & " dbo.Compra.ConsecutivoCompania = " & GetViewNameRenglonComprasConOSinSerial & ".ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.Compra.NumeroSecuencial = " & GetViewNameRenglonComprasConOSinSerial & ".NumeroSecuencialCompra"
   vSQL = vSQL & " LEFT JOIN " & insCambioVista.GetViewNameCambioAlaFecha() & " ON"
   vSQL = vSQL & " dbo.Compra.CodigoMoneda = " & insCambioVista.GetViewNameCambioAlaFecha() & ".CodigoMoneda "
    Set insCambioVista = Nothing
   fSqlViewComprasConCambioOriginal = vSQL
h_EXIT:      On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasConCambioOriginal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
  
Public Function GetViewNameComprasConCambioAlDia() As String
   GetViewNameComprasConCambioAlDia = "IGV_ComprasConCambioAlDia"
End Function
  
Public Function fSqlViewComprasConCambioAlDia() As String
  Dim insCambioVista As clsCambioVista
  Dim vSQL As String
  On Error GoTo h_ERROR
   Set insCambioVista = New clsCambioVista
   vSQL = " SELECT"
   vSQL = vSQL & " dbo.Compra.ConsecutivoCompania,"
   vSQL = vSQL & " dbo.Compra.Numero,"
   vSQL = vSQL & " dbo.Compra.Fecha,"
   vSQL = vSQL & " dbo.Compra.CodigoProveedor,"
   vSQL = vSQL & " dbo.Proveedor.NombreProveedor,"
   vSQL = vSQL & " dbo.Compra.Moneda, "
   vSQL = vSQL & " dbo.Compra.CambioAbolivares, "
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".cantidadrecibida * " & GetViewNameRenglonComprasConOSinSerial & ".CostoUnitario AS TotalRenglones, "
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".cantidadrecibida * " & GetViewNameRenglonComprasConOSinSerial & ".CostoUnitario"
   vSQL = vSQL & " * " & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insCambioVista.GetViewNameCambioAlaFecha() & ".CambioAbolivares"), "dbo.Compra.CambioAbolivares", insCambioVista.GetViewNameCambioAlaFecha() & ".CambioAbolivares") & " AS TotalRenglonesCambio, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(insCambioVista.GetViewNameCambioAlaFecha() & ".CambioAbolivares"), "dbo.Compra.CambioAbolivares", insCambioVista.GetViewNameCambioAlaFecha() & ".CambioAbolivares") & " AS  cambioalafecha, "
   vSQL = vSQL & " dbo.Compra.NumeroSecuencial,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".CodigoArticulo,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".CostoUnitario,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".Serial,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".Rollo,"
   vSQL = vSQL & GetViewNameRenglonComprasConOSinSerial & ".cantidadrecibida,"
   vSQL = vSQL & " dbo.Compra.CodigoAlmacen,"
   vSQL = vSQL & " dbo.Compra.EsUnaOrdenDeCompra,"
   vSQL = vSQL & " dbo.Compra.StatusCompra "
   vSQL = vSQL & " FROM dbo.Compra"
   vSQL = vSQL & " INNER JOIN  dbo.Proveedor ON"
   vSQL = vSQL & " dbo.Compra.ConsecutivoCompania = dbo.Proveedor.ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.Compra.CodigoProveedor = dbo.Proveedor.CodigoProveedor"
   vSQL = vSQL & " INNER JOIN " & GetViewNameRenglonComprasConOSinSerial & " ON"
   vSQL = vSQL & " dbo.Compra.ConsecutivoCompania = " & GetViewNameRenglonComprasConOSinSerial & ".ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.Compra.NumeroSecuencial = " & GetViewNameRenglonComprasConOSinSerial & ".NumeroSecuencialCompra"
   vSQL = vSQL & " LEFT JOIN " & insCambioVista.GetViewNameCambioAlaFecha() & " ON"
   vSQL = vSQL & " dbo.Compra.CodigoMoneda = " & insCambioVista.GetViewNameCambioAlaFecha() & ".CodigoMoneda "
   Set insCambioVista = Nothing
   fSqlViewComprasConCambioAlDia = vSQL
h_EXIT:      On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasConCambioAlDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
  
Public Function GetViewNameComprasConCuentasArticulos() As String
   GetViewNameComprasConCuentasArticulos = "IGV_ComprasConCuentasArticulos"
End Function
  
  
 Public Function fSqlViewComprasConCuentasArticulos() As String
  Dim insArticuloInvVista As clsArticuloInvVista
  Dim vSQL As String
  On Error GoTo h_ERROR
  Set insArticuloInvVista = New clsArticuloInvVista
 
 vSQL = " SELECT "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania, "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
 vSQL = vSQL & "dbo.ArticuloInventario.CuentaCostoDeVenta, "
 vSQL = vSQL & "dbo.ArticuloInventario.CuentaInventario, "
 vSQL = vSQL & GetViewNameComprasConOtroGastos() & ".Numero, "
 vSQL = vSQL & GetViewNameComprasConOtroGastos() & ".StatusCompra, "
 vSQL = vSQL & GetViewNameComprasConOtroGastos() & ".TotalRenglon "
 vSQL = vSQL & " FROM "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1()
 vSQL = vSQL & " INNER JOIN     dbo.ArticuloInventario ON "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = dbo.ArticuloInventario.ConsecutivoCompania AND "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo = dbo.ArticuloInventario.Codigo"
 vSQL = vSQL & " INNER JOIN  " & GetViewNameComprasConOtroGastos() & " ON "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".CodigoDelArticulo = " & GetViewNameComprasConOtroGastos() & ".CodigoArticulo AND "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania = " & GetViewNameComprasConOtroGastos() & ".ConsecutivoCompania"
 
 vSQL = vSQL & " Group By "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".ConsecutivoCompania, "
 vSQL = vSQL & insArticuloInvVista.GetViewNameArticulosDeInventarioConOSinExistenPorGrupoB1() & ".Codigo, "
 vSQL = vSQL & " dbo.ArticuloInventario.CuentaCostoDeVenta, "
 vSQL = vSQL & " dbo.ArticuloInventario.CuentaInventario, "
 vSQL = vSQL & GetViewNameComprasConOtroGastos() & ".Numero,"
 vSQL = vSQL & GetViewNameComprasConOtroGastos() & ".StatusCompra,"
 vSQL = vSQL & GetViewNameComprasConOtroGastos() & ".ConsecutivoCompania,"
 vSQL = vSQL & GetViewNameComprasConOtroGastos() & ".TotalRenglon "
 Set insArticuloInvVista = Nothing
 fSqlViewComprasConCuentasArticulos = vSQL
h_EXIT:      On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasConCuentasArticulos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function
  
Public Function GetViewNameComprasConOtroGastos() As String
  GetViewNameComprasConOtroGastos = "IGV_ComprasConOtroGastos"
End Function

Public Function fSqlViewComprasConOtroGastos() As String
  Dim vSQL As String
  On Error GoTo h_ERROR
 
 vSQL = " SELECT "
 vSQL = vSQL & " dbo.compra.ConsecutivoCompania,"
 vSQL = vSQL & " dbo.compra.Numero,"
 vSQL = vSQL & " dbo.compra.Fecha,"
 vSQL = vSQL & "  Compra.StatusCompra,"
 vSQL = vSQL & " dbo.renglonCompra.CodigoArticulo,"
 vSQL = vSQL & " dbo.renglonCompra.Cantidad,"
 vSQL = vSQL & " dbo.renglonCompra.CostoUnitario,"
 vSQL = vSQL & GetViewNameComprasOtroGastosPorItem() & ".OtrosGastosPorItem,"
 vSQL = vSQL & " dbo.compra.OtrosGastos,"
 vSQL = vSQL & " dbo.renglonCompra.CantidadRecibida * " & GetViewNameComprasOtroGastosPorItem() & ".OtrosGastosPorItem + dbo.renglonCompra.CostoUnitario * dbo.renglonCompra.CantidadRecibida AS TotalRenglon,"
 vSQL = vSQL & " dbo.compra.TotalCompra"
 vSQL = vSQL & " From dbo.compra"
 vSQL = vSQL & " INNER JOIN  dbo.renglonCompra ON"
 vSQL = vSQL & " dbo.compra.ConsecutivoCompania = dbo.renglonCompra.ConsecutivoCompania AND"
 vSQL = vSQL & " dbo.compra.NumeroSecuencial = dbo.renglonCompra.NumeroSecuencialCompra"
 vSQL = vSQL & " INNER JOIN   " & GetViewNameComprasOtroGastosPorItem() & " ON"
 vSQL = vSQL & " dbo.compra.ConsecutivoCompania = " & GetViewNameComprasOtroGastosPorItem() & ".ConsecutivoCompania AND"
 vSQL = vSQL & " dbo.compra.Numero = " & GetViewNameComprasOtroGastosPorItem() & ".Numero"

 fSqlViewComprasConOtroGastos = vSQL
h_EXIT:      On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasConOtroGastos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
 End Function

Public Function GetViewNameComprasImprimirEtiquetasPorCompras() As String
   GetViewNameComprasImprimirEtiquetasPorCompras = "IGV_ComprasImprimirEtiquetasPorCompras"
End Function

Public Function fSqlViewComprasImprimirEtiquetasPorCompras() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
   vSQL = ""
   vSQL = vSQL & "SELECT"
   vSQL = vSQL & " dbo.Compra.ConsecutivoCompania AS Compania"
   vSQL = vSQL & ", dbo.Compra.Numero"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion"
   vSQL = vSQL & ", dbo.IGV_ExistenciaPorGrupoConTallasyColores.DescripcionColor"
   vSQL = vSQL & ", dbo.IGV_ExistenciaPorGrupoConTallasyColores.DescripcionTalla"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioSinIva"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioConIva"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioSinIva2"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioConIva2"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioSinIva3"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioConIva3"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioSinIva4"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.PrecioConIva4"
   vSQL = vSQL & ", dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo"
   vSQL = vSQL & ", dbo.RenglonCompra.CantidadRecibida"
   vSQL = vSQL & ", dbo.Compra.Fecha"
   vSQL = vSQL & ", dbo.RenglonCompraXSerial.Serial"
   vSQL = vSQL & ", dbo.RenglonCompraXSerial.Rollo"
   vSQL = vSQL & ", dbo.Compra.CodigoProveedor"
   vSQL = vSQL & ", dbo.Proveedor.NombreProveedor"
   vSQL = vSQL & " FROM"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 INNER JOIN"
   vSQL = vSQL & " dbo.Compra INNER JOIN"
   vSQL = vSQL & " dbo.RenglonCompra ON dbo.Compra.ConsecutivoCompania = dbo.RenglonCompra.ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.Compra.NumeroSecuencial = dbo.RenglonCompra.NumeroSecuencialCompra ON"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = dbo.RenglonCompra.ConsecutivoCompania AND"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = dbo.RenglonCompra.CodigoArticulo LEFT OUTER JOIN"
   vSQL = vSQL & " dbo.RenglonCompraXSerial ON dbo.RenglonCompra.NumeroSecuencialCompra = dbo.RenglonCompraXSerial.NumeroSecuencialCompra AND"
   vSQL = vSQL & " dbo.RenglonCompra.CodigoArticulo = dbo.RenglonCompraXSerial.CodigoArticulo"
   vSQL = vSQL & " INNER JOIN dbo.Proveedor ON dbo.Proveedor.CodigoProveedor = dbo.Compra.CodigoProveedor AND"
   vSQL = vSQL & " dbo.Proveedor.ConsecutivoCompania = dbo.Compra.ConsecutivoCompania FULL OUTER JOIN"
   vSQL = vSQL & " dbo.IGV_ExistenciaPorGrupoConTallasyColores ON"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoColor = dbo.IGV_ExistenciaPorGrupoConTallasyColores.CodigoColor AND"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoTalla = dbo.IGV_ExistenciaPorGrupoConTallasyColores.CodigoTalla AND"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoGrupo = dbo.IGV_ExistenciaPorGrupoConTallasyColores.CodigoGrupo AND"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo = dbo.IGV_ExistenciaPorGrupoConTallasyColores.CodigoArticulo AND"
   vSQL = vSQL & " dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = dbo.IGV_ExistenciaPorGrupoConTallasyColores.ConsecutivoCompania"
    
   fSqlViewComprasImprimirEtiquetasPorCompras = vSQL
   
h_EXIT:      On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasConOtroGastos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



































