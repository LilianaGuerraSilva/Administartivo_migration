VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsFacturaVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsPdtVista"
Private Const CM_MESSAGE_NAME As String = "Vista PDTs"
Private gEnumProyecto As clsEnumAdministrativo

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNameVentasGeneradasFactura() As String
   GetViewNameVentasGeneradasFactura = "IGV_VentasGeneradasFactura_B1"
End Function

Public Function fSqlViewVentasGeneradasFactura() As String
   Dim vSQL As String
   Dim sqlTipoTransaccion As String
   Dim sqlAlicuotaExenta As String
   Dim sqlAlicuotaG As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT "
   vSQL = vSQL & "factura.ConsecutivoCompania, "
   vSQL = vSQL & "factura.Numero, "
   vSQL = vSQL & "factura.StatusFactura, "
   vSQL = vSQL & "renglonFactura.AlicuotaIVA, "
   vSQL = vSQL & "factura.TipoDeVenta, "
   vSQL = vSQL & "factura.TipoDeTransaccion, "
   vSQL = vSQL & "factura.TipoDeDocumento, "
   vSQL = vSQL & fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(False) & " AS TotalSinIvaDelRenglonesConDescuento, "
   vSQL = vSQL & fSQLMontoIvaDelRenglonConElDescuentoDeFactura(False) & " AS TotalIvaDelRenglonConElDescuento, "
   vSQL = vSQL & fSQLMontoTotalDescuentoRenglonDeFactura(False) & " AS TotalDescuentoRenglon, "
   vSQL = vSQL & fSQLMontoDescIvaAlicuotaGeneralVentasInt(False) & " AS TotalDescuentoFactura, "
   vSQL = vSQL & "renglonFactura.PorcentajeDescuento AS DescuentoRenglon, "
   vSQL = vSQL & "Factura.PorcentajeDescuento AS DescuentoFactura, "
   vSQL = vSQL & "factura.Fecha, "
   vSQL = vSQL & "factura.CambioABolivares, "
   vSQL = vSQL & "factura.Moneda "
   vSQL = vSQL & "FROM "
   vSQL = vSQL & "factura "
   vSQL = vSQL & "INNER JOIN "
   vSQL = vSQL & "renglonFactura "
   vSQL = vSQL & "ON factura.Numero = renglonFactura.NumeroFactura "
   vSQL = vSQL & "AND factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania "
   vSQL = vSQL & "WHERE "
   vSQL = vSQL & "factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   vSQL = vSQL & " OR factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   fSqlViewVentasGeneradasFactura = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasGeneradas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(ByVal valVieneDelLibroDeVentas As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "(renglonFactura.PrecioSinIva * renglonFactura.Cantidad)"
   SQL = "(" & SQL & " * (1 - (renglonFactura.PorcentajeDescuento / 100.00))) "
   SQL = "(" & SQL & " * (1 - (Factura.PorcentajeDescuento / 100.00))) "
   fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoIvaDelRenglonConElDescuentoDeFactura(ByVal valVieneDelLibroDeVentas As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "(renglonFactura.PrecioConIva " & " * renglonFactura.Cantidad " & ") "
   SQL = "(" & SQL & " * (1 - (renglonFactura.PorcentajeDescuento " & " / 100))) "
   SQL = "(" & SQL & " * (1 - (factura.PorcentajeDescuento " & " / 100))) "
   fSQLMontoIvaDelRenglonConElDescuentoDeFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoIvaDelRenglonConElDescuentoDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDescuentoRenglonDeFactura(ByVal valVieneDelLibroDeVentas As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "(((renglonFactura.PrecioSinIva * renglonFactura.Cantidad)"
   SQL = SQL & " * (renglonFactura.PorcentajeDescuento / 100.00))"
   SQL = SQL & " + ((renglonFactura.PrecioSinIva * renglonFactura.Cantidad)"
   SQL = SQL & " * (factura.PorcentajeDescuento / 100.00))) "
   fSQLMontoTotalDescuentoRenglonDeFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoTotalDescuentoRenglonDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoDescIvaAlicuotaGeneralVentasInt(ByVal valVieneDelLibroDeVentas As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "((((renglonFactura.PrecioSinIva * renglonFactura.Cantidad)"
   SQL = SQL & " - (renglonFactura.PrecioConIva * renglonFactura.Cantidad))"
   SQL = SQL & " * (renglonFactura.PorcentajeDescuento / 100.00))"
   SQL = SQL & " + ((renglonFactura.PrecioSinIva * renglonFactura.CANTIDAD)"
   SQL = SQL & " * (factura.PorcentajeDescuento/ 100.00))) "
   fSQLMontoDescIvaAlicuotaGeneralVentasInt = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoDescIvaAlicuotaGeneralVentasInt", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameVendedorXRenglonFactura() As String
   GetViewNameVendedorXRenglonFactura = "IGV_VendedorXRenglonFactura"
End Function

Public Function fSqlViewVendedorXRenglonFactura() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & " Vendedor.Codigo AS CodigoVendedor, Vendedor.Nombre AS NombreVendedor, renglonFactura.NumeroFactura, renglonFactura.ConsecutivoCompania,"
   SQL = SQL & " renglonFactura.TipoDeDocumento, (CASE WHEN CodigoVendedor2 <> '' THEN SUM(RenglonFactura.TotalRenglon / 2)"
   SQL = SQL & " WHEN CodigoVendedor3 <> '' THEN SUM(RenglonFactura.TotalRenglon / 3) ELSE SUM(RenglonFactura.TotalRenglon) END) AS TotalRenglon, RenglonFactura.cantidad AS cantidad,"
   SQL = SQL & " renglonFactura.PrecioSinIva,  renglonFactura.PrecioConIva, RenglonFactura.Articulo, RenglonFactura.PorcentajeDescuento, RenglonFactura.PorcentajeAlicuota "
   SQL = SQL & " FROM Vendedor INNER JOIN"
   SQL = SQL & " renglonFactura ON Vendedor.Codigo = renglonFactura.CodigoVendedor1 "
   SQL = SQL & " AND RenglonFactura.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   SQL = SQL & " GROUP BY Vendedor.Codigo, Vendedor.Nombre, renglonFactura.NumeroFactura, renglonFactura.CodigoVendedor2, renglonFactura.CodigoVendedor3,"
   SQL = SQL & " RenglonFactura.ConsecutivoCompania , RenglonFactura.TipoDeDocumento, RenglonFactura.cantidad, RenglonFactura.PrecioSinIva, RenglonFactura.PrecioConIva, RenglonFactura.Articulo, RenglonFactura.PorcentajeDescuento, RenglonFactura.PorcentajeAlicuota"
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT Vendedor.Codigo AS CodigoVendedor, Vendedor.Nombre AS NombreVendedor, renglonFactura.NumeroFactura, renglonFactura.ConsecutivoCompania,"
   SQL = SQL & " renglonFactura.TipoDeDocumento, (CASE WHEN CodigoVendedor2 <> '' THEN SUM(RenglonFactura.TotalRenglon / 2)"
   SQL = SQL & " WHEN CodigoVendedor3 <> '' THEN SUM(RenglonFactura.TotalRenglon / 3) ELSE SUM(RenglonFactura.TotalRenglon) END) AS TotalRenglon, RenglonFactura.cantidad AS cantidad,"
   SQL = SQL & " renglonFactura.PrecioSinIva,  renglonFactura.PrecioConIva, RenglonFactura.Articulo, RenglonFactura.PorcentajeDescuento, RenglonFactura.PorcentajeAlicuota"
   SQL = SQL & " FROM Vendedor INNER JOIN"
   SQL = SQL & " renglonFactura ON Vendedor.Codigo = renglonFactura.CodigoVendedor2 "
   SQL = SQL & " AND RenglonFactura.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   SQL = SQL & " GROUP BY Vendedor.Codigo, Vendedor.Nombre, renglonFactura.NumeroFactura, renglonFactura.CodigoVendedor2, renglonFactura.CodigoVendedor3,"
   SQL = SQL & " RenglonFactura.ConsecutivoCompania , RenglonFactura.TipoDeDocumento, RenglonFactura.cantidad, RenglonFactura.PrecioSinIva, RenglonFactura.PrecioConIva, RenglonFactura.Articulo, RenglonFactura.PorcentajeDescuento, RenglonFactura.PorcentajeAlicuota"
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT Vendedor.Codigo AS CodigoVendedor, Vendedor.Nombre AS NombreVendedor, renglonFactura.NumeroFactura, renglonFactura.ConsecutivoCompania,"
   SQL = SQL & " renglonFactura.TipoDeDocumento, (CASE WHEN CodigoVendedor2 <> '' THEN SUM(RenglonFactura.TotalRenglon / 2)"
   SQL = SQL & " WHEN CodigoVendedor3 <> '' THEN SUM(RenglonFactura.TotalRenglon / 3) ELSE SUM(RenglonFactura.TotalRenglon) END) AS TotalRenglon, RenglonFactura.cantidad AS cantidad,"
   SQL = SQL & " renglonFactura.PrecioSinIva,  renglonFactura.PrecioConIva, RenglonFactura.Articulo, RenglonFactura.PorcentajeDescuento, RenglonFactura.PorcentajeAlicuota"
   SQL = SQL & " FROM Vendedor INNER JOIN"
   SQL = SQL & " renglonFactura ON Vendedor.Codigo = renglonFactura.CodigoVendedor3 "
   SQL = SQL & " AND RenglonFactura.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   SQL = SQL & " GROUP BY Vendedor.Codigo, Vendedor.Nombre, renglonFactura.NumeroFactura, renglonFactura.CodigoVendedor2, renglonFactura.CodigoVendedor3,"
   SQL = SQL & " RenglonFactura.ConsecutivoCompania , RenglonFactura.TipoDeDocumento, RenglonFactura.cantidad, RenglonFactura.PrecioSinIva, RenglonFactura.PrecioConIva, RenglonFactura.Articulo, RenglonFactura.PorcentajeDescuento, RenglonFactura.PorcentajeAlicuota"
   fSqlViewVendedorXRenglonFactura = SQL

h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlViewVendedorXRenglonFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameNotasDeCreditoEntreFechasPorVendedor() As String
   GetViewNameNotasDeCreditoEntreFechasPorVendedor = "IGV_NotasDeCreditoEntreFechasPorVendedor"
End Function

Public Function fSqlViewNotasDeCreditoEntreFechasPorVendedor() As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT "
   SQL = SQL & "Factura.ConsecutivoCompania as ConsecutivoCompania, "
   SQL = SQL & "Factura.Moneda as Moneda, "
   SQL = SQL & "Factura.Fecha as Fecha, "
   SQL = SQL & "1 AS ORDEN, "
   SQL = SQL & "Factura.Numero as Numero, "
   SQL = SQL & "Factura.TipoDeDocumento as TipoDeDocumento, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   SQL = SQL & "Cliente.Codigo as CodigoCliente, "
   SQL = SQL & "Cliente.Nombre as Cliente, "
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                               gUtilSQL.fSimpleSqlValue(""), True) & " AS Status, "
   SQL = SQL & "Factura.CambioABolivares as CambioABolivares, "
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", "Factura.TotalFactura", True) & " AS MontoOriginal, "
   SQL = SQL & "Factura.PorcentajeDescuento AS PorcentajeDescuento, "
   SQL = SQL & "Factura.MontoDescuento1 AS MontoDescuento, "
   SQL = SQL & "Factura.TotalIva AS MontoIVA, "
   SQL = SQL & "Factura.TotalBaseImponible AS BaseImponible, "
   SQL = SQL & "Factura.TotalMontoExento AS MontoExento, "
   SQL = SQL & "Factura.TotalFactura AS MontoTotal, "
   SQL = SQL & "0 AS OtrosCargos, "
   SQL = SQL & "Vendedor.Nombre AS NombreVendedor, "
   SQL = SQL & "Factura.NumeroComprobanteFiscal AS ComprobanteFiscal "
   SQL = SQL & " FROM "
   SQL = SQL & " Factura INNER JOIN Cliente"
   SQL = SQL & "     ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & "     AND Factura.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " INNER JOIN Adm.Vendedor "
   SQL = SQL & "     ON factura.ConsecutivoCompania = Adm.Vendedor.ConsecutivoCompania "
   SQL = SQL & "     AND factura.ConsecutivoVendedor = Adm.Vendedor.Consecutivo"
   SQL = SQL & " WHERE "
   SQL = SQL & " (Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & _
                  " OR Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & ") "
   SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   fSqlViewNotasDeCreditoEntreFechasPorVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlViewNotasDeCreditoEntreFechasPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() As String
   GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito = "IGV_GenerarOtrosCargosyDescuentosNotasDeCredito"
End Function

Public Function fSqlViewGenerarOtrosCargosyDescuentosNotasDeCredito() As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT "
   SQL = SQL & "Factura.ConsecutivoCompania as ConsecutivoCompania, "
   SQL = SQL & "Factura.Moneda as Moneda, "
   SQL = SQL & "Factura.Fecha as Fecha, "
   SQL = SQL & "2 AS ORDEN, "
   SQL = SQL & "Factura.Numero as Numero, "
   SQL = SQL & "Factura.TipoDeDocumento as TipoDeDocumento, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   SQL = SQL & "Cliente.Codigo as CodigoCliente, "
   SQL = SQL & "Cliente.Nombre as Cliente, "
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                               gUtilSQL.fSimpleSqlValue(""), True) & " AS Status, "
   SQL = SQL & "Factura.CambioABolivares as CambioABolivares, "
   SQL = SQL & "0 AS MontoOriginal, "
   SQL = SQL & "0 AS PorcentajeDescuento, "
   SQL = SQL & "0 AS MontoDescuento, "
   SQL = SQL & "0 AS MontoIVA, "
   SQL = SQL & "0 AS BaseImponible, "
   SQL = SQL & "0 AS MontoExento, "
   SQL = SQL & "0 MontoTotal, "
   SQL = SQL & "SUM(RenglonDetalleDeOtrosCargosFactura.TotalRenglon) AS OtrosCargos, "
   SQL = SQL & "Vendedor.Nombre AS NombreVendedor, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS ComprobanteFiscal"
   SQL = SQL & " FROM "
   SQL = SQL & " (Cliente INNER JOIN Factura "
   SQL = SQL & "     ON (Cliente.Codigo = Factura.CodigoCliente)"
   SQL = SQL & "     AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN RenglonDetalleDeOtrosCargosFactura "
   SQL = SQL & "     ON (Factura.TipoDeDocumento = RenglonDetalleDeOtrosCargosFactura.TipoDeDocumento)"
   SQL = SQL & "     AND (Factura.Numero = RenglonDetalleDeOtrosCargosFactura.NumeroFactura)"
   SQL = SQL & "     AND (Factura.ConsecutivoCompania = RenglonDetalleDeOtrosCargosFactura.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN Vendedor "
   SQL = SQL & "     ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   SQL = SQL & "     AND factura.CodigoVendedor = Vendedor.Codigo "
   SQL = SQL & " WHERE "
   SQL = SQL & " (Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & _
                 " OR Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & ") "
   SQL = SQL & " AND (renglonDetalleDeOtrosCargosFactura.ComoAplicaAlTotalFactura <> " & gUtilSQL.fSimpleSqlValue(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO) & ")"
   SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                 " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " GROUP BY "
   SQL = SQL & " Factura.ConsecutivoCompania, Factura.Moneda, Factura.Fecha, Factura.Numero, Factura.TipoDeDocumento, Cliente.Codigo, "
   SQL = SQL & " Cliente.Nombre, Factura.StatusFactura, Factura.CambioABolivares, Vendedor.Nombre"
   fSqlViewGenerarOtrosCargosyDescuentosNotasDeCredito = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlViewGenerarOtrosCargosyDescuentosNotasDeCredito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameVentasConVendedorCliente() As String
   GetViewNameVentasConVendedorCliente = "IGV_VentasConVendedorCliente"
End Function
Public Function fSqlViewVentasConVendedorCliente() As String
   Dim vSQL As String
   Dim vWhere As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
  
    vWhere = "dbo.factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
    vWhere = gUtilSQL.fSQLEnumValueWithAnd(vWhere, "dbo.factura.StatusFactura", enum_StatusFactura.eSF_EMITIDA)
    vWhere = gUtilSQL.getWhereSQL(vWhere)
    vSQL = "SELECT "
    vSQL = vSQL & "dbo.factura.ConsecutivoCompania,"
    vSQL = vSQL & "dbo.factura.Numero,"
    vSQL = vSQL & "dbo.factura.Fecha,"
    vSQL = vSQL & "dbo.factura.CodigoCliente,"
    vSQL = vSQL & "dbo.Cliente.Nombre,"
    vSQL = vSQL & "Adm.Vendedor.Codigo AS CodigoVendedor,"
    vSQL = vSQL & "Adm.Vendedor.Nombre AS NombreVendedor,"
    vSQL = vSQL & "dbo.renglonFactura.Articulo,"
    vSQL = vSQL & "dbo.renglonFactura.Serial,"
    vSQL = vSQL & "dbo.renglonFactura.Rollo,"
    vSQL = vSQL & "dbo.renglonFactura.Descripcion,"
    vSQL = vSQL & "dbo.renglonFactura.Cantidad,"
    vSQL = vSQL & "dbo.renglonFactura.PrecioSinIVA,"
    vSQL = vSQL & "dbo.factura.CambioABolivares,"
    vSQL = vSQL & "dbo.factura.Moneda,"
    vSQL = vSQL & "dbo.factura.StatusFactura,"
    vSQL = vSQL & "dbo.factura.TipoDeDocumento,"
    vSQL = vSQL & "dbo.factura.PorcentajeDescuento,"
    vSQL = vSQL & "dbo.renglonFactura.PorcentajeDescuento AS DescuentoIndividual"
    vSQL = vSQL & " FROM dbo.factura"
    vSQL = vSQL & " INNER JOIN   dbo.renglonFactura ON"
    vSQL = vSQL & " dbo.factura.ConsecutivoCompania = dbo.renglonFactura.ConsecutivoCompania AND"
    vSQL = vSQL & " dbo.factura.Numero = dbo.renglonFactura.NumeroFactura AND"
    vSQL = vSQL & " dbo.factura.TipoDeDocumento = dbo.renglonFactura.TipoDeDocumento"
    vSQL = vSQL & " INNER JOIN   Adm.Vendedor ON"
    vSQL = vSQL & " dbo.factura.ConsecutivoCompania = Adm.Vendedor.ConsecutivoCompania AND"
    vSQL = vSQL & " factura.ConsecutivoVendedor = Adm.Vendedor.Consecutivo"
    vSQL = vSQL & " INNER JOIN  dbo.Cliente ON"
    vSQL = vSQL & " dbo.factura.ConsecutivoCompania = dbo.Cliente.ConsecutivoCompania AND"
    vSQL = vSQL & " dbo.factura.CodigoCliente = dbo.Cliente.Codigo  "
    vSQL = vSQL & vWhere
   fSqlViewVentasConVendedorCliente = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlViewVentasConVendedorCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameVentasConDescripcionArticulos() As String
   GetViewNameVentasConDescripcionArticulos = "IGV_VentasConDescripcionArticulos_B1"
End Function
Public Function fSqlViewVentasConDescripcionArticulos() As String
   Dim vSQL As String
   Dim vWhere As String
   Dim vComparacion As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
    vComparacion = "dbo.IGV_VentasConVendedorCliente.DescuentoIndividual" & " = " & gUtilSQL.fNumToStrSQL(0)
    vSQL = "SELECT "
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.ConsecutivoCompania,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Numero,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Fecha,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.CodigoCliente,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Nombre,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.CodigoVendedor,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.NombreVendedor,"
    vSQL = vSQL & "dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Articulo,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Serial,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Rollo,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Descripcion,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.Cantidad,"
    vSQL = vSQL & "dbo.IGV_VentasConVendedorCliente.PrecioSinIVA, "
    vSQL = vSQL & gUtilSQL.getIIF(vComparacion, "dbo.IGV_VentasConVendedorCliente.PrecioSinIVA", "dbo.IGV_VentasConVendedorCliente.PrecioSinIVA - (dbo.IGV_VentasConVendedorCliente.PrecioSinIVA * (dbo.IGV_VentasConVendedorCliente.DescuentoIndividual / 100))", True) & " AS PrecioConDescuentoIndividual,"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.CambioABolivares,"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.Moneda,"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.StatusFactura,"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.TipoDeDocumento,"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.PorcentajeDescuento,"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.DescuentoIndividual "
    vSQL = vSQL & " From dbo.IGV_VentasConVendedorCliente"
    vSQL = vSQL & " INNER JOIN  dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 ON "
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.ConsecutivoCompania = dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania AND"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.Articulo = dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo AND"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.Serial = dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial AND"
    vSQL = vSQL & " dbo.IGV_VentasConVendedorCliente.Rollo = dbo.IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo"
   fSqlViewVentasConDescripcionArticulos = vSQL
     Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlViewVentasConDescripcionArticulos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



 



