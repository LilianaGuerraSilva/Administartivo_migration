VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCobranzaVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCobranzaVista"
Private Const CM_MESSAGE_NAME As String = "Vista Cobranza"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetViewNameCambioConPeriodoVigencia() As String
   GetViewNameCambioConPeriodoVigencia = "IGV_CambioConPeriodoVigencia"
End Function

Public Function fSqlViewCambioConPeriodoVigencia() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   vSQL = "SELECT "
   vSQL = vSQL & "C1.CodigoMoneda as CodigoMoneda, "
   vSQL = vSQL & "C1.CambioAbolivares as TasaCambio, "
   vSQL = vSQL & "C1.FechaDeVigencia as FechaDeVigenciaDesde, "
   vSQL = vSQL & "(SELECT MIN(DATEADD('d', -1, FechaDeVigencia)) "
   vSQL = vSQL & "FROM Cambio "
   vSQL = vSQL & "WHERE FechaDeVigencia > C1.fechadevigencia "
   vSQL = vSQL & "AND CodigoMoneda = C1.CodigoMoneda) AS VigenteHasta "
   vSQL = vSQL & "FROM Cambio As C1"

   fSqlViewCambioConPeriodoVigencia = vSQL
   
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCambioConPeriodoVigencia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMontoNoComisibleFactura() As String
   GetViewNameMontoNoComisibleFactura = "IGV_MontoNoComisibleFactura_B1"
End Function

Public Function fSqlViewMontoNoComisibleFactura() As String
   Dim vSQL As String
   Dim vSQLPrecioXCantidad As String
   Dim vSQLDescEnRenglon As String
   Dim vSQLDescEnTotFact As String
   On Error GoTo h_ERROR
   vSQLPrecioXCantidad = "(renglonFactura.PrecioSinIVA * renglonFactura.Cantidad)"
   vSQLDescEnRenglon = "(1 - renglonFactura.PorcentajeDescuento / 100)"
   vSQLDescEnTotFact = "(1 - factura.PorcentajeDescuento / 100)"
   
   vSQL = "SELECT SUM(" & vSQLPrecioXCantidad & " * " & vSQLDescEnRenglon & "*" & vSQLDescEnTotFact & ") AS MontoNoComisionable, "
   vSQL = vSQL & "factura.Numero, "
   vSQL = vSQL & "factura.TotalBaseImponible + factura.TotalMontoExento AS TotalFacturaSinIVA,factura.ConsecutivoCompania "
   vSQL = vSQL & "FROM articuloInventario INNER JOIN "
   vSQL = vSQL & "renglonFactura ON articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND "
   vSQL = vSQL & "articuloInventario.Codigo = renglonFactura.Articulo INNER JOIN "
   vSQL = vSQL & "factura ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND "
   vSQL = vSQL & "renglonFactura.NumeroFactura = factura.Numero And renglonFactura.TipoDeDocumento = factura.TipoDeDocumento "
   vSQL = vSQL & "WHERE (articuloInventario.ExcluirDeComision = " & gUtilSQL.fBooleanToSqlValue(False) & ") "
   vSQL = vSQL & "GROUP BY factura.Numero, factura.TotalBaseImponible + factura.TotalMontoExento,factura.ConsecutivoCompania"
h_EXIT:     On Error GoTo 0
   fSqlViewMontoNoComisibleFactura = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMontoNoComisibleFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMontoComisionXCobranza() As String
   GetViewNameMontoComisionXCobranza = "IGV_MontoComisionXCobranza_B1"
End Function

Public Function fSqlViewMontoComisionXCobranza() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT cobranza.Fecha AS FechaCobro, cobranza.Numero AS NumeroCobro, cobranza.Moneda AS MonedaDocumentos, "
   vSQL = vSQL & "documentoCobrado.MontoAbonado AS MontoAbonadoACxC, "
   vSQL = vSQL & "documentoCobrado.MontoAbonado - documentoCobrado.MontoAbonado * cxC.MontoIVA / CASE WHEN (cxC.MontoIVA + cxC.MontoExento "
   vSQL = vSQL & "+ cxC.MontoGravado) = 0 THEN 1 ELSE (cxC.MontoIVA + cxC.MontoExento + cxC.MontoGravado) END AS CobroSinIVA, "
   vSQL = vSQL & "documentoCobrado.NumeroDelDocumentoCobrado, cxC.NumeroDocumentoOrigen, cobranza.CodigoCliente, "
   vSQL = vSQL & "cobranza.CodigoCobrador, cxC.Fecha AS FechaCxC, cobranza.ConsecutivoCompania "
   vSQL = vSQL & "FROM cobranza INNER JOIN "
   vSQL = vSQL & "documentoCobrado ON cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania AND "
   vSQL = vSQL & "cobranza.Numero = documentoCobrado.NumeroCobranza INNER JOIN "
   vSQL = vSQL & "cxC ON documentoCobrado.ConsecutivoCompania = cxC.ConsecutivoCompania AND "
   vSQL = vSQL & "documentoCobrado.NumeroDelDocumentoCobrado = cxC.Numero"
h_EXIT:     On Error GoTo 0
   fSqlViewMontoComisionXCobranza = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMontoComisionXCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameComisionDeVendedoresPorCobroDetallado() As String
   GetViewNameComisionDeVendedoresPorCobroDetallado = "IGV_ComisionDeVendedoresPorCobroDetallado"
End Function

Public Function fSqlViewComisionDeVendedoresPorCobroDetallado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT IGV_MontoComisionXCobranza_B1.CodigoCobrador, vendedor.Nombre AS NombreVenededor, "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.FechaCobro, IGV_MontoComisionXCobranza_B1.NumeroCobro, cliente.Nombre AS NombreCliente, "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.MontoAbonadoACxC, "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.CobroSinIVA * (IGV_MontoNoComisibleFactura_B1.TotalFacturaSinIVA - IGV_MontoNoComisibleFactura_B1.MontoNoComisionable) "
   vSQL = vSQL & "/ IGV_MontoNoComisibleFactura_B1.TotalFacturaSinIVA AS MontoComisionable, IGV_MontoComisionXCobranza_B1.FechaCxC, "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.NumeroDelDocumentoCobrado , IGV_MontoComisionXCobranza_B1.ConsecutivoCompania "
   vSQL = vSQL & "FROM         IGV_MontoComisionXCobranza_B1 INNER JOIN "
   vSQL = vSQL & "IGV_MontoNoComisibleFactura_B1 ON "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.NumeroDocumentoOrigen = IGV_MontoNoComisibleFactura_B1.Numero AND "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.ConsecutivoCompania = IGV_MontoNoComisibleFactura_B1.ConsecutivoCompania INNER JOIN "
   vSQL = vSQL & "cliente ON IGV_MontoComisionXCobranza_B1.CodigoCliente = cliente.Codigo AND "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.ConsecutivoCompania = cliente.ConsecutivoCompania INNER JOIN "
   vSQL = vSQL & "vendedor ON IGV_MontoComisionXCobranza_B1.CodigoCobrador = vendedor.Codigo AND "
   vSQL = vSQL & "IGV_MontoComisionXCobranza_B1.ConsecutivoCompania = vendedor.ConsecutivoCompania "
h_EXIT:     On Error GoTo 0
   fSqlViewComisionDeVendedoresPorCobroDetallado = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComisionDeVendedoresPorCobroDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewRetencionesDelPeriodo() As String
   GetViewRetencionesDelPeriodo = "IGV_RetencionesDelPeriodo"
End Function

Public Function fSQLViewRetencionesDelPeriodo() As String
   Dim SQL As String
   On Error GoTo h_ERROR
    SQL = SQL & "SELECT "
      SQL = SQL & "Cobranza.ConsecutivoCompania, "
      SQL = SQL & "Cobranza.Fecha, "
      SQL = SQL & "Cobranza.CambioABolivares, "
      SQL = SQL & "Cobranza.Moneda, "
      SQL = SQL & "DocumentoCobrado.MontoIvaRetenido "
      SQL = SQL & " FROM Cobranza INNER JOIN "
      SQL = SQL & "DocumentoCobrado ON ("
      SQL = SQL & "Cobranza.Numero" & _
                  " = DocumentoCobrado.NumeroCobranza"
      SQL = SQL & ") AND ("
      SQL = SQL & "Cobranza.ConsecutivoCompania" & _
                  " = DocumentoCobrado.ConsecutivoCompania)"
      SQL = SQL & " WHERE Cobranza.StatusCobranza" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   fSQLViewRetencionesDelPeriodo = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLViewRetencionesDelPeriodo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCobranzaPorZona() As String
   GetViewNameCobranzaPorZona = "IGV_CobranzaPorZona"
End Function

Public Function fSqlViewCobranzaPorZona() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   
   Set gEnumReport = New clsEnumReport
   SQL = "SELECT "
   SQL = SQL & "zonaCobranza.ConsecutivoCompania, "
   SQL = SQL & "zonaCobranza.Nombre AS NombreZona, "
   SQL = SQL & "Cobranza.Numero, "
   SQL = SQL & "Cobranza.Fecha, "
   SQL = SQL & "Cobranza.CodigoMoneda, "
   SQL = SQL & "Cobranza.Moneda, "
   SQL = SQL & "Cobranza.CambioAbolivares, "
   SQL = SQL & "Cobranza.TotalCobrado, "
   SQL = SQL & "Cobranza.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre AS NombreCliente, "
   SQL = SQL & "Cobranza.CodigoCobrador, "
   SQL = SQL & "Vendedor.Nombre AS NombreCobrador "
   SQL = SQL & "FROM zonaCobranza INNER JOIN "
   SQL = SQL & "Cliente ON zonaCobranza.ConsecutivoCompania = Cliente.ConsecutivoCompania AND "
   SQL = SQL & "zonaCobranza.Nombre = Cliente.ZonaDeCobranza INNER JOIN "
   SQL = SQL & "Cobranza ON Cliente.ConsecutivoCompania = Cobranza.ConsecutivoCompania AND "
   SQL = SQL & "Cliente.Codigo = Cobranza.CodigoCliente AND "
   SQL = SQL & "zonaCobranza.ConsecutivoCompania = Cobranza.ConsecutivoCompania INNER JOIN "
   SQL = SQL & "Vendedor ON Cobranza.ConsecutivoCompania = Vendedor.ConsecutivoCompania AND "
   SQL = SQL & "Cobranza.CodigoCobrador = Vendedor.Codigo "
   
   fSqlViewCobranzaPorZona = SQL
   
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlViewCobranzaPorZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameCobranzaUltimaFecha() As String
   GetViewNameCobranzaUltimaFecha = "IGV_CobranzaUltimaFecha"
End Function

Public Function fSqlViewCobranzaUltimaFecha() As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
     
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd("", "Cobranza.StatusCobranza", enum_StatusCobranza.eSC_VIGENTE)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQL = vSQL & " SELECT "
   vSQL = vSQL & " Cobranza.ConsecutivoCompania,"
   vSQL = vSQL & " DocumentoCobrado.NumeroDelDocumentoCobrado,"
   vSQL = vSQL & " MAX(dbo.Cobranza.Fecha) As UltimaFechAbono"
   vSQL = vSQL & " FROM   Cobranza INNER JOIN"
   vSQL = vSQL & " DocumentoCobrado ON dbo.Cobranza.ConsecutivoCompania =  DocumentoCobrado.ConsecutivoCompania AND "
   vSQL = vSQL & " Cobranza.Numero =  DocumentoCobrado.NumeroCobranza"
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " GROUP BY Cobranza.ConsecutivoCompania, DocumentoCobrado.NumeroDelDocumentoCobrado"
   fSqlViewCobranzaUltimaFecha = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: vSQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlViewCobranzaUltimaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameComisionVendedorXCobranzaXArticulo() As String
   GetViewNameComisionVendedorXCobranzaXArticulo = "IGV_ComisionVendedorCobranzaXArticulo"
End Function

Public Function fSqlViewComisionVendedorXCobranzaXArticulo() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT factura.Numero, rf.Articulo, rf.Descripcion, rf.Cantidad,"
   vSQL = vSQL & "CASE WHEN ai.comisionaporcentaje = 'S' THEN (rf.preciosiniva * rf.cantidad) "
   vSQL = vSQL & "ELSE (ai.porcentajecomision * rf.cantidad) END AS totalsiniva, rf.PrecioConIVA * rf.Cantidad AS totalrengloniva, ai.PorcentajeComision / 100 AS porcentajecomision, "
   vSQL = vSQL & "factura.ConsecutivoCompania, factura.CodigoVendedor, rf.PrecioSinIVA, ai.ComisionaPorcentaje, factura.TotalFactura, factura.StatusFactura, "
   vSQL = vSQL & "factura.Moneda AS monedafactura, factura.CodigoCliente, factura.ConsecutivoCompania AS Expr1, factura.Fecha "
   vSQL = vSQL & "FROM renglonFactura AS rf INNER JOIN "
   vSQL = vSQL & "factura ON factura.Numero = rf.NumeroFactura AND factura.ConsecutivoCompania = rf.ConsecutivoCompania AND factura.StatusFactura = '0' AND "
   vSQL = vSQL & "factura.TipoDeDocumento = '0' INNER JOIN "
   vSQL = vSQL & "ArticuloInventario AS ai ON ai.ConsecutivoCompania = rf.ConsecutivoCompania AND ai.Codigo = rf.Articulo "
   'vSQL = vSQL & "ORDER BY factura.Numero "
h_EXIT:     On Error GoTo 0
   fSqlViewComisionVendedorXCobranzaXArticulo = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComisionVendedorXCobranzaXArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

