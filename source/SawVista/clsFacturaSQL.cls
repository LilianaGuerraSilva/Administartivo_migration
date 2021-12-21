VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsFacturaSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsFacturaSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Factura"
Private insRenglonComicionesDeVendedor As Object
Private insRengFac As Object
Private insFactura As Object
Private insRenglonCobroDeFactura As Object
Public Enum Enum_Vendedores_Por_Renglon
   eVPR_Vendedor1 = 1
   eVPR_Vendedor2
   eVPR_Vendedor3
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function fSQLFacturacionEntreFechasXCondicionDePago(ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                                    ByVal valReporteEnMonedaLocal As Boolean, ByVal valConsecutivoCompania As String, _
                                                      ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   SQL = "SELECT "
   SQL = SQL & "Factura.CondicionesDePago, "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS MonedaDelReporte, "
   Else
      SQL = SQL & "Factura.Moneda AS MonedaDelReporte, "
   End If
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "Factura.Numero" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" (*)"), "Factura.Numero", True) & " AS NumeroFactura, "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDeDocumento, "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), _
                                  0, gUtilSQL.getIIF("Factura.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & _
                                  " OR Factura.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", "Factura.TotalFactura", True, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", "Factura.Fecha"), _
                                  True), True) & " AS TotalFactura, "
   Else
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), 0, "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   SQL = SQL & "Factura.Moneda, "
   SQL = SQL & "Factura.CambioABolivares, "
   SQL = SQL & "Factura.CodigoMoneda "
   SQL = SQL & "FROM Factura INNER JOIN "
   SQL = SQL & "cliente ON Factura.ConsecutivoCompania = cliente.ConsecutivoCompania "
   SQL = SQL & "AND Factura.CodigoCliente = cliente.Codigo "
   SQL = SQL & "WHERE (Factura.consecutivoCompania = " & valConsecutivoCompania & ") "
   SQL = SQL & "AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & "AND (Factura.StatusFactura <> " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_BORRADOR) & ") "
   SQL = SQL & "AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valReporteEnMonedaLocal Then
      SQL = SQL & "ORDER BY Factura.CondicionesDePago, Factura.Fecha, Numero "
   Else
      SQL = SQL & "ORDER BY Factura.CondicionesDePago, MonedaDelReporte, Factura.Fecha, Numero "
   End If
   fSQLFacturacionEntreFechasXCondicionDePago = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturacionEntreFechasXCondicionDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreFechas(ByVal valConsecutivoCompania As String, ByVal valCategoria As String, _
                                                                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = SQL & "SELECT Cliente.Nombre AS NombreCliente, "
   SQL = SQL & "MAX(Factura.Fecha) AS FechaUltima, "
   SQL = SQL & "Cliente.FAX AS Fax, "
   SQL = SQL & "Cliente.Telefono AS Telefono, "
   SQL = SQL & "Cliente.Ciudad, "
   SQL = SQL & "Cliente.Codigo AS Codigo"
   SQL = SQL & " FROM articuloInventario INNER JOIN"
   SQL = SQL & " ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente)"
   SQL = SQL & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) INNER JOIN"
   SQL = SQL & " renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (factura.Numero = renglonFactura.NumeroFactura)"
   SQL = SQL & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
   SQL = SQL & " ON (articuloInventario.Codigo = renglonFactura.Articulo)"
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania)
   SQL = SQL & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   SQL = SQL & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   SQL = SQL & " GROUP BY Cliente.Ciudad, Cliente.Codigo, Cliente.Nombre, Cliente.Telefono, Cliente.Fax"
   SQL = SQL & " ORDER BY Cliente.Ciudad, Cliente.Codigo"
   fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreCiudades(ByVal valConsecutivoCompania As String, ByVal valCategoria As String, _
                                                                      ByVal valCiudadDesde As String, ByVal valCiudadHasta As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = SQL & "SELECT Cliente.Nombre AS NombreCliente, "
   SQL = SQL & "MAX(Factura.Fecha) AS FechaUltima, "
   SQL = SQL & "Cliente.FAX AS Fax, "
   SQL = SQL & "Cliente.Telefono AS Telefono, "
   SQL = SQL & "Cliente.Ciudad, "
   SQL = SQL & "Cliente.Codigo AS Codigo"
   SQL = SQL & " FROM articuloInventario INNER JOIN"
   SQL = SQL & " ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente)"
   SQL = SQL & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (factura.Numero = renglonFactura.NumeroFactura)"
   SQL = SQL & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
   SQL = SQL & " ON (articuloInventario.Codigo = renglonFactura.Articulo)"
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) 'gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND articuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)     'Trim(txtCategoria.Text))
   SQL = SQL & " AND cliente.Ciudad BETWEEN " & gUtilSQL.fSimpleSqlValue(valCiudadDesde) & " AND " & gUtilSQL.fSimpleSqlValue(valCiudadHasta) 'Trim(txtCiudadDesde.Text) Trim(txtCiudadHasta.Text)
   SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   SQL = SQL & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   SQL = SQL & " GROUP BY Cliente.Ciudad, Cliente.Codigo, Cliente.Nombre, Cliente.Telefono, Cliente.Fax  "
   SQL = SQL & " ORDER BY Cliente.Ciudad, Cliente.Codigo  "
   fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreCiudades = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreCiudades", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlDelReporteNotasDeCreditoAsociadasAFacturas(ByVal valUsaCambio As Boolean, ByVal gMonedaLocalActual As Object, _
                                                                 ByVal gUltimaTasaDeCambio As Object, ByVal valConsecutivoCompania As String, _
                                                                   ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                                     ByVal valCmbStatusNC As String, ByVal valCmbStatusNCNumero As Integer)
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.StatusFactura", enum_StatusFactura.eSF_EMITIDA, gEnumProyecto.fEnumStatusFacturaToStringInArray(True, False), "") & " AS StatusNC, "
   If valUsaCambio Then
      SQL = SQL & "factura.Numero AS NumeroNC, factura.Fecha AS FechaNC, factura.NumeroFacturaAfectada, " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", "Factura.Fecha"), True, "TotalNotadeCredito") & ","
      SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.totalBaseImponible", "Factura.Fecha"), True, "TotalBaseImponible") & ","
      SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalIVA", "Factura.Fecha"), True, "TotalIVA") & ", factura.TipoDeDocumento, "
      SQL = SQL & " factura_1.Fecha, factura_1.numero, factura_1.Moneda, factura.Moneda AS MonedaNC, " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura_1.CambioABolivares", "factura_1.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura_1.TotalFactura", "Factura.Fecha"), True, "TotalFactura")
   Else
      SQL = SQL & "factura.Numero AS NumeroNC, factura.Fecha AS FechaNC, factura.NumeroFacturaAfectada, (factura.TotalFactura) AS TotalNotadeCredito, factura.TotalBaseImponible, factura.TotalIVA, factura.TipoDeDocumento, factura.Moneda AS MonedaNC, "
      SQL = SQL & "factura_1.Fecha, (factura_1.TotalFactura) AS TotalFactura, factura_1.Numero, factura_1.Moneda"
   End If
   SQL = SQL & " FROM factura INNER JOIN factura AS factura_1 ON (factura.NumeroFacturaAfectada = factura_1.Numero) AND (factura.ConsecutivoCompania = factura_1.ConsecutivoCompania)"
   SQL = SQL & " WHERE  factura.ConsecutivoCompania= " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueDatesBetween("", "factura.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura_1.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
   SQL = SQL & " AND  Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   If valCmbStatusNC <> "Todos" Then
      SQL = SQL & " AND FACTURA.StatusFactura = " & gUtilSQL.fSimpleSqlValue(valCmbStatusNCNumero)
   End If
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   SQL = SQL & " GROUP BY factura_1.Numero,factura.numerofacturaafectada, factura.fecha,factura.numero, factura.TotalFactura, factura.TotalBaseImponible, Factura.TotalIva, Factura.TipodeDocumento, factura.StatusFactura,"
   SQL = SQL & " factura.StatusFactura, factura_1.Fecha, factura_1.StatusFactura, factura_1.TotalFactura,factura.CambioABolivares,factura_1.CambioABolivares,factura.Moneda,factura_1.Moneda"
   SQL = SQL & " ORDER BY Factura.NumeroFacturaAfectada"
   fSqlDelReporteNotasDeCreditoAsociadasAFacturas = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlDelReporteNotasDeCreditoAsociadasAFacturas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlDelReporteFacturacionConOtrosCargosYDescuentos(ByVal valUsaCambio As Boolean, ByVal gMonedaLocalActual As Object, _
                                                                      ByVal gUltimaTasaDeCambio As Object, ByVal valConsecutivoCompania As String, _
                                                                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date)
   Dim SQL As String
   Dim sqlTotalFactura As String
   Dim SQLTotalRenglonOtrosCargos As String
   On Error GoTo h_ERROR
   SQL = "SELECT factura.Numero AS NumeroFactura, factura.Fecha, cliente.nombre, factura.moneda, "
   If valUsaCambio Then
      sqlTotalFactura = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Sum(renglonFactura.PrecioSinIVA)", "Factura.Fecha")
      SQLTotalRenglonOtrosCargos = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "renglonDetalleDeOtrosCargosFactura.TotalRenglon", "Factura.Fecha")
      SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", sqlTotalFactura, True, "TotalFactura") & ", "
      SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", SQLTotalRenglonOtrosCargos, True, "TotalRenglonOtrosCargos") & ", "
   Else
      SQL = SQL & "Sum(renglonFactura.PrecioSinIVA) AS TotalFactura, renglonDetalleDeOtrosCargosFactura.TotalRenglon AS TotalRenglonOtrosCargos, "
   End If
   SQL = SQL & " RenglonDetalleDeOtrosCargosFactura.Descripcion AS DescripcionOtrosCargos"
   SQL = SQL & " FROM cliente INNER JOIN ((factura INNER JOIN renglonDetalleDeOtrosCargosFactura ON (factura.TipoDeDocumento = renglonDetalleDeOtrosCargosFactura.TipoDeDocumento) "
   SQL = SQL & " AND (factura.Numero = renglonDetalleDeOtrosCargosFactura.NumeroFactura) "
   SQL = SQL & " AND (factura.ConsecutivoCompania = renglonDetalleDeOtrosCargosFactura.ConsecutivoCompania)) INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) "
   SQL = SQL & " AND (factura.Numero = renglonFactura.NumeroFactura) "
   SQL = SQL & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)) ON (cliente.Codigo = factura.CodigoCliente) "
   SQL = SQL & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania) "
   SQL = SQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueDatesBetween("", "factura.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   SQL = SQL & " GROUP BY factura.Numero, factura.Fecha, renglonDetalleDeOtrosCargosFactura.TotalRenglon, renglonDetalleDeOtrosCargosFactura.Descripcion, factura.moneda, cliente.nombre, factura.CambioABolivares"
   fSqlDelReporteFacturacionConOtrosCargosYDescuentos = SQL
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDelReporteFacturacionConOtrosCargosYDescuentos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturaPorArticuloYPorVendedor(ByVal valInformeDetallado As Boolean, ByVal valUsarCambio As Boolean, _
                                                                   ByVal valCompaniaUsaPrecioSinIvaSegunTipoDocumento As Boolean, _
                                                                    ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, _
                                                                      ByVal valcodigoDeVendedor As String, ByVal valConsecutivoCompania As String, _
                                                                        ByVal valImprimirSoloUnArticulo As Boolean, ByVal valCodigoDeArticulo As String, _
                                                                          ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                                            ByVal valImprimirSoloUnVendedor As Boolean, ByVal valComisiones As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valComisiones Then
      SQL = "SELECT "
      SQL = SQL & "Vendedor.Codigo AS CodigoVendedor, "
      SQL = SQL & "Vendedor.Nombre AS NombreVendedor, "
      SQL = SQL & "Factura.Moneda, "
      SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, "
      SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion, "
      SQL = SQL & "Factura.Fecha, "
      If valInformeDetallado Then
         SQL = SQL & "Factura.CodigoCliente, "
         SQL = SQL & "Cliente.Nombre, "
         SQL = SQL & "Factura.Numero, "
         SQL = SQL & "Factura.TipoDeDocumento, "
         SQL = SQL & "RenglonFactura.Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            SQL = SQL & "RenglonFactura.PrecioSinIVA AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * RenglonFactura.Cantidad) AS MargenGanancia, "
         Else
            SQL = SQL & "RenglonFactura.PrecioConIVA AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * RenglonFactura.Cantidad) AS MargenGanancia, "
         End If
         SQL = SQL & "ArticuloInventario.CostoUnitario "
         SQL = SQL & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 INNER JOIN ArticuloInventario "
         SQL = SQL & " ON  (ArticuloInventario.Codigo = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo)"
         SQL = SQL & " AND (ArticuloInventario.ConsecutivoCompania = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania)"
         SQL = SQL & " INNER JOIN (((Vendedor INNER JOIN Factura"
         SQL = SQL & " ON  (Vendedor.Codigo = Factura.CodigoVendedor)"
         SQL = SQL & " AND (Vendedor.ConsecutivoCompania = Factura.ConsecutivoCompania))"
         SQL = SQL & " INNER JOIN Cliente ON (Cliente.Codigo = Factura.CodigoCliente)"
         SQL = SQL & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
         SQL = SQL & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
         SQL = SQL & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
         SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
         SQL = SQL & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
         SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial)"
         SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
         SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania) "
      Else
         SQL = SQL & "SUM(RenglonFactura.Cantidad) AS Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            SQL = SQL & "SUM(RenglonFactura.PrecioSinIVA) AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales("SUM(" & fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon"
         Else
            SQL = SQL & "SUM(RenglonFactura.PrecioConIVA) AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales("SUM(" & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon"
         End If
         SQL = SQL & " FROM Vendedor"
         SQL = SQL & " INNER JOIN (ArticuloInventario"
         SQL = SQL & " INNER JOIN IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 ON ArticuloInventario.Codigo= IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo "
         SQL = SQL & " AND ArticuloInventario.ConsecutivoCompania = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania )"
         SQL = SQL & " INNER JOIN (Factura"
         SQL = SQL & " INNER JOIN RenglonFactura"
         SQL = SQL & " ON  (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
         SQL = SQL & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
         SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
         SQL = SQL & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
         SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial)"
         SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
         SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
         SQL = SQL & " ON  (Vendedor.Codigo = Factura.CodigoVendedor)"
         SQL = SQL & " AND (Vendedor.ConsecutivoCompania = Factura.ConsecutivoCompania)"
      End If
      SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
      If valImprimirSoloUnArticulo Then
         SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeArticulo))
      End If
      SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
      If valImprimirSoloUnVendedor Then
         SQL = SQL & " AND " & "Factura.CodigoVendedor = " & gUtilSQL.fSimpleSqlValue(Trim(valcodigoDeVendedor))
      End If
      If Not valInformeDetallado Then
         SQL = SQL & " GROUP BY Vendedor.Codigo, Vendedor.Nombre, Factura.Moneda, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion, Factura.Fecha "
      End If
      SQL = SQL & " ORDER BY Vendedor.Codigo, Factura.Moneda, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, Factura.Fecha"
      If valInformeDetallado Then
         SQL = SQL & ", Factura.TipoDeDocumento, Factura.Numero"
      End If
   Else 'Facturación por renglones
      SQL = "SELECT "
      SQL = SQL & "IGV_VendedorXRenglonFactura.CodigoVendedor AS CodigoVendedor, "
      SQL = SQL & "IGV_VendedorXRenglonFactura.NombreVendedor AS NombreVendedor, "
      SQL = SQL & "Factura.Moneda, "
      SQL = SQL & "ArticuloInventario.Codigo, "
      SQL = SQL & "ArticuloInventario.Descripcion, "
      SQL = SQL & "Factura.Fecha, "
      If valInformeDetallado Then
         SQL = SQL & "Factura.CodigoCliente, "
         SQL = SQL & "Cliente.Nombre, "
         SQL = SQL & "Factura.Numero, "
         SQL = SQL & "Factura.TipoDeDocumento, "
         SQL = SQL & "IGV_VendedorXRenglonFactura.Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            SQL = SQL & "IGV_VendedorXRenglonFactura.PrecioSinIVA AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * IGV_VendedorXRenglonFactura.Cantidad) AS MargenGanancia, "
         Else
            SQL = SQL & "IGV_VendedorXRenglonFactura.PrecioConIVA AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            SQL = SQL & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * IGV_VendedorXRenglonFactura.Cantidad) AS MargenGanancia, "
         End If
         SQL = SQL & "ArticuloInventario.CostoUnitario as CostoUnitario"
         SQL = SQL & " FROM ArticuloInventario INNER JOIN IGV_VendedorXRenglonFactura "
         SQL = SQL & " ON  (ArticuloInventario.Codigo = IGV_VendedorXRenglonFactura.Articulo"
         SQL = SQL & " AND ArticuloInventario.ConsecutivoCompania = IGV_VendedorXRenglonFactura.ConsecutivoCompania) "
         SQL = SQL & " INNER JOIN Factura"
         SQL = SQL & " ON  (IGV_VendedorXRenglonFactura.ConsecutivoCompania = Factura.ConsecutivoCompania"
         SQL = SQL & " AND Factura.TipoDeDocumento = IGV_VendedorXRenglonFactura.TipoDeDocumento"
         SQL = SQL & " AND Factura.Numero = IGV_VendedorXRenglonFactura.NumeroFactura)"
         SQL = SQL & " INNER JOIN Cliente ON (Cliente.Codigo = Factura.CodigoCliente"
         SQL = SQL & " AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania)"
      Else
         SQL = SQL & "SUM(IGV_VendedorXRenglonFactura.Cantidad) AS Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            SQL = SQL & "SUM(IGV_VendedorXRenglonFactura.PrecioSinIVA) AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales("Sum(" & fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon"
         Else
            SQL = SQL & "SUM(IGV_VendedorXRenglonFactura.PrecioConIVA) AS  PrecioUnitario, "
            SQL = SQL & gUtilSQL.fRoundNDecimales("Sum(" & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon "
         End If
         SQL = SQL & " FROM IGV_VendedorXRenglonFactura"
         SQL = SQL & " INNER JOIN ArticuloInventario"
         SQL = SQL & " ON  (ArticuloInventario.Codigo = IGV_VendedorXRenglonFactura.Articulo"
         SQL = SQL & " AND ArticuloInventario.ConsecutivoCompania = IGV_VendedorXRenglonFactura.ConsecutivoCompania)"
         SQL = SQL & " INNER JOIN Factura"
         SQL = SQL & " ON  (Factura.TipoDeDocumento = IGV_VendedorXRenglonFactura.TipoDeDocumento"
         SQL = SQL & " AND Factura.Numero = IGV_VendedorXRenglonFactura.NumeroFactura"
         SQL = SQL & " AND Factura.ConsecutivoCompania = IGV_VendedorXRenglonFactura.ConsecutivoCompania)"
      End If
      SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
      If valImprimirSoloUnArticulo Then
         SQL = SQL & " AND IGV_VendedorXRenglonFactura.Articulo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeArticulo))
      End If
      SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
      If valImprimirSoloUnVendedor Then
         SQL = SQL & " AND " & "IGV_VendedorXRenglonFactura.CodigoVendedor = " & gUtilSQL.fSimpleSqlValue(Trim(valcodigoDeVendedor))
      End If
      SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      If Not valInformeDetallado Then
         SQL = SQL & " GROUP BY IGV_VendedorXRenglonFactura.CodigoVendedor, IGV_VendedorXRenglonFactura.NombreVendedor, Factura.Moneda, ArticuloInventario.Codigo, ArticuloInventario.Descripcion, Factura.Fecha "
      End If
      SQL = SQL & " ORDER BY IGV_VendedorXRenglonFactura.CodigoVendedor, Factura.Moneda, ArticuloInventario.Codigo, Factura.Fecha"
      If valInformeDetallado Then
         SQL = SQL & ", Factura.TipoDeDocumento, Factura.Numero"
      End If
   End If
   
   fConstruirSQLDelReporteFacturaPorArticuloYPorVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteFacturaPorArticuloYPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturaPorArticuloYPorAlamcen(ByVal valConsecutivoCompania As String, ByVal valImprimirSoloUnAlmacen As Boolean, _
                                    ByVal valConsecutivoAlmacen As String, ByVal valImprimirSoloUnArticulo As Boolean, _
                                    ByVal valCodigoDeArticulo As String, ByVal valFechaInicial As Date, _
                                    ByVal valFechaFinal As Date, ByVal valInformeDetallado As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "Factura.ConsecutivoAlmacen AS CodigoAlmacen, "
   SQL = SQL & "Almacen.Codigo AS CodigoAlmacen, "
   SQL = SQL & "Almacen.NombreAlmacen AS NombreAlmacen, "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, "
   SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion, "
   SQL = SQL & "ExistenciaPorAlmacen.Cantidad as Existencia, "
   SQL = SQL & "Factura.Fecha, "
   If (valInformeDetallado) Then
      SQL = SQL & "Factura.CodigoCliente As CodigoCliente, "
      SQL = SQL & "Cliente.Nombre as NombreCliente, "
      SQL = SQL & "Factura.TipoDeDocumento as TipoDeDocumento, "
      SQL = SQL & "Factura.Numero as FacturaNumero, "
      SQL = SQL & "RenglonFactura.Cantidad as Cantidad "
   Else
      SQL = SQL & "SUM(RenglonFactura.Cantidad) AS Cantidad "
   End If
   SQL = SQL & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   SQL = SQL & " INNER JOIN ArticuloInventario"
   SQL = SQL & "  ON  (ArticuloInventario.Codigo = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo)"
   SQL = SQL & "  AND (ArticuloInventario.ConsecutivoCompania = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN ((Cliente INNER JOIN Factura ON (Cliente.Codigo = Factura.CodigoCliente) AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN ExistenciaPorAlmacen ON (Factura.ConsecutivoAlmacen = ExistenciaPorAlmacen.ConsecutivoAlmacen)"
   SQL = SQL & "  AND (Factura.ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania)"
   SQL = SQL & "  AND (RenglonFactura.Articulo = ExistenciaPorAlmacen.CodigoArticulo)"
   SQL = SQL & " INNER JOIN Almacen"
   SQL = SQL & " ON ExistenciaPorAlmacen.ConsecutivoAlmacen = Almacen.Consecutivo"
   SQL = SQL & " AND ExistenciaPorAlmacen.ConsecutivoCompania= Almacen.ConsecutivoCompania"
   SQL = SQL & " AND Factura.ConsecutivoAlmacen  = Almacen.Consecutivo"
   SQL = SQL & " AND Factura.ConsecutivoCompania  = Almacen.ConsecutivoCompania )"
   SQL = SQL & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   If valImprimirSoloUnAlmacen Then
      SQL = SQL & " AND Factura.CodigoAlmacen = '" & valConsecutivoAlmacen & "'"
   End If
   If valImprimirSoloUnArticulo Then
      SQL = SQL & " AND RenglonFactura.Articulo = '" & valCodigoDeArticulo & "'"
   End If
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura.ConsecutivoCompania = " & valConsecutivoCompania
   If (valInformeDetallado) Then
      SQL = SQL & " ORDER BY Factura.ConsecutivoAlmacen, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, Factura.Fecha, Factura.TipoDeDocumento, Factura.Numero"
   Else
      SQL = SQL & " GROUP BY Factura.ConsecutivoAlmacen, Almacen.Codigo, Almacen.NombreAlmacen, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion,ExistenciaPorAlmacen.Cantidad, Factura.Fecha  "
      SQL = SQL & " ORDER BY Factura.ConsecutivoAlmacen, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, Factura.Fecha"
   End If
   fConstruirSQLDelReporteFacturaPorArticuloYPorAlamcen = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteFacturaPorArticuloYPorAlamcen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(ByVal valUsarCambioABolivares As Boolean, ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                         ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "(RenglonFactura.PrecioSinIVA * RenglonFactura.Cantidad)"
   SQL = "(" & SQL & " * (1 - (RenglonFactura.PorcentajeDescuento / 100.00))) "
   SQL = "(" & SQL & " * (1 - (Factura.PorcentajeDescuento / 100.00))) "
   If valUsarCambioABolivares Then
      SQL = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", SQL, True, "")
   End If
   If Not valVieneDelLibroDeVentas Then
   SQL = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQL, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(ByVal valUsarCambioABolivares As Boolean, ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                        ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "((RenglonFactura.PrecioSinIVA * RenglonFactura.Cantidad) * (1 + (PorcentajeAlicuota / 100)))"
   SQL = "(" & SQL & " * (1 - (RenglonFactura.PorcentajeDescuento / 100))) "
   SQL = "(" & SQL & " * (1 - (Factura.PorcentajeDescuento / 100))) "
   If valUsarCambioABolivares Then
      SQL = "(" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", SQL, True, "") & ")"
   End If
   If Not valVieneDelLibroDeVentas Then
      SQL = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQL, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasEntreFechasPorTipoDeArticulo(ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valLineaDeProductoAImprimir As String, _
                                                              ByVal valTipoDeArticulo As String, ByVal usarCambioOriginal As Boolean, _
                                                                ByVal valReporteEnMonedaLocal As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                                                                  ByVal gMonedaLocalActual As Object, ByVal valImprimirUnaLineaDeProducto As Boolean, _
                                                                    ByVal valImprimirUnTipoDeArticulo As Boolean, ByVal valConsecutivoCompania As String, _
                                                                      ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                                        ByVal valUsaPrecioSinIva As Boolean, ByVal valInformeResumido As Boolean, ByVal valcodigoCliente As String) As String
   Dim SQL As String
   Dim sqlPrecioSinIva As String
   Dim sqlPrecioConIva As String
   Dim SqlTotalRenglon As String
   Dim SqlTotalCantidad As String
   Dim SqlTotalMonto As String
   Dim SqlInnerJoin As String
   Dim SqlStatusFactura As String
   Dim sqlRenglon As String
   Dim sqlMontoRenglon As String
   Dim sqlDescMonto As String
   Dim sqlFactMonto As String
   Dim SqlRenglonTotal As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vPrecio As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SqlStatusFactura = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), gUtilSQL.fSimpleSqlValue("Vigente"), True)
   If valUsaPrecioSinIva Then
      vPrecio = "RenglonFactura.PrecioSinIva"
   Else
      vPrecio = "RenglonFactura.PrecioConIva"
   End If
   sqlRenglon = "(" & vPrecio & " * RenglonFactura.Cantidad)"
   sqlMontoRenglon = "(" & sqlRenglon & " * (1 - (RenglonFactura.PorcentajeDescuento / 100)))"
   sqlDescMonto = "((" & sqlRenglon & ") - (" & sqlMontoRenglon & "))"
   sqlFactMonto = "((( " & sqlRenglon & " * (1 - (RenglonFactura.PorcentajeDescuento / 100))) * (factura.MontoDescuento1*1000 / factura.TotalRenglones*1000)/1000000))"
   SqlRenglonTotal = "(" & sqlRenglon & " - " & sqlDescMonto & " - " & sqlFactMonto & ")"
     
   If valReporteEnMonedaLocal Then
      sqlPrecioConIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioConIva", "Factura.Fecha"), usarCambioOriginal, ""), True)
      sqlPrecioSinIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioSinIva", "Factura.Fecha"), usarCambioOriginal, ""), True)
      SqlTotalRenglon = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SqlRenglonTotal, "Factura.Fecha"), usarCambioOriginal, ""), True)
   Else
      sqlPrecioConIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "RenglonFactura.PrecioConIva", True)
      sqlPrecioSinIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "RenglonFactura.PrecioSinIva", True)
      SqlTotalRenglon = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", SqlRenglonTotal, True)
   End If
   SQL = "SELECT "
   SQL = SQL & "Factura.Numero, "
   SQL = SQL & "Factura.Moneda, "
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   If valInformeResumido Then
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "SUM(RenglonFactura.Cantidad)", True) & " AS CantidadDeArticulo, "
      SQL = SQL & "SUM(" & SqlTotalRenglon & ") AS TotalRenglon "
   Else
      SQL = SQL & "RenglonFactura.Articulo AS CodigoArticulo, "
      SQL = SQL & "RenglonFactura.Descripcion AS DescripcionArticulo, "
      SQL = SQL & "RenglonFactura.PorcentajeDescuento AS PorcDectoRenglon, "
      SQL = SQL & "Factura.PorcentajeDescuento AS PorcDectoFactura, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "RenglonFactura.Cantidad", True) & " AS CantidadDeArticulo, "
      SQL = SQL & sqlPrecioConIva & " AS PrecioConIva, "
      SQL = SQL & sqlPrecioSinIva & " AS PrecioSinIva, "
      SQL = SQL & SqlTotalRenglon & " AS TotalRenglon, "
      SQL = SQL & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual) & " AS TotalRenglonConIva, "
      SQL = SQL & SqlStatusFactura & " AS StatusFactura, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", sqlDescMonto, True) & " AS MontoDescRenglon, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", sqlFactMonto, True) & " AS MontoDescFactura "
   End If
   SQL = SQL & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   SQL = SQL & " INNER JOIN ((Cliente INNER JOIN Factura"
   SQL = SQL & " ON  (Cliente.Codigo = Factura.CodigoCliente)"
   SQL = SQL & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
   SQL = SQL & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   If valImprimirUnaLineaDeProducto Then
      SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductoAImprimir))
   End If
   If valImprimirUnTipoDeArticulo Then
      SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strTipoDeArticuloToNum(valTipoDeArticulo))
   End If
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   SQL = SQL & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      SQL = SQL & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valInformeResumido Then
      SQL = SQL & " GROUP BY Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.StatusFactura "
   End If
   SQL = SQL & " ORDER BY Factura.StatusFactura, Factura.Moneda, Factura.Fecha, Factura.Numero"

   fSQLDeFacturasEntreFechasPorTipoDeArticulo = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeFacturasEntreFechasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasEntreFechas(ByVal valReporteEnMonedaLocal As Boolean, _
                                           ByVal gMonedaLocalActual As Object, _
                                            ByVal gUltimaTasaDeCambio As Object, _
                                             ByVal valUsaModuloDeContabilidad As Boolean, _
                                              ByVal valConsecutivoCompania As String, _
                                               ByVal valFechaInicial As Date, _
                                                ByVal valFechaFinal As Date, _
                                                 ByVal valInformeEsDeBorradores As Boolean, _
                                                  ByVal valFiltroInforme As String, _
                                                   ByVal valAgrupaInforme As Boolean, _
                                                    ByVal valUsarCiudad As Boolean, ByVal valCiudad As String, ByVal valCampoAMostrar As String, ByVal valAgrupaInformeXCliente As Boolean, ByVal valMostrarDocumentosGeneradosconIVAal10 As Boolean) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         SQL = SQL & "Factura.Moneda, "
   End If
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "1 AS ORDEN, "
   SQL = SQL & valCampoAMostrar & " as Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   SQL = SQL & "Factura.TipoDeDocumento, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                               gUtilSQL.fSimpleSqlValue(""), True) & " AS StatusFactura, "
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", "Factura.TotalFactura", True) & " AS MontoOriginal, "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalIva", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalBaseImponible", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalBaseImponible, "
'EXENTO
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalMontoExento", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalMontoExento, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalFactura, "
   Else
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalIVA", True) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalBaseImponible", True) & " AS TotalBaseImponible, "
     SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                          gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                          gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalMontoExento", _
                          "Factura.Fecha"), True, ""), True) & " AS TotalMontoExento, "
     SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   SQL = SQL & "0 AS OtrosCargos, "
   SQL = SQL & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      SQL = SQL & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      SQL = SQL & " AS NumeroComprobante"
   End If
      SQL = SQL & ", Factura.NumeroComprobanteFiscal AS ComprobanteFiscal "
   SQL = SQL & " FROM Factura INNER JOIN "
   SQL = SQL & " Cliente ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND Factura.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   SQL = SQL & " AND factura.CodigoVendedor = Vendedor.Codigo"
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   If valInformeEsDeBorradores Then
      SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltroInforme <> "" Then
      SQL = SQL & " AND " & valFiltroInforme
   End If
    If valUsarCiudad Then
      SQL = SQL & " AND Cliente.Ciudad = " & gUtilSQL.fSimpleSqlValue(valCiudad)
   End If
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valMostrarDocumentosGeneradosconIVAal10 Then
      SQL = SQL & " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   End If
   If valAgrupaInforme Then
      SQL = SQL & " ORDER BY Moneda, Vendedor, TipoDeDocumentoStr, Fecha, " & valCampoAMostrar
   ElseIf valAgrupaInformeXCliente Then
      SQL = SQL & " ORDER BY Moneda, Cliente.Nombre, TipoDeDocumentoStr, Fecha, " & valCampoAMostrar
   Else
      SQL = SQL & " ORDER BY Moneda, TipoDeDocumentoStr, Fecha, " & valCampoAMostrar
   End If
   fSQLDeFacturasEntreFechas = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQGenerarCuentaContable(ByVal valConsecutivoCompania As String, _
                                           ByVal valFechaInicial As Date, _
                                            ByVal valFechaFinal As Date) As String
   Dim SQL As String
   Dim SqlPeriodo As String
   SQL = SQL & "SELECT Periodo.ConsecutivoPeriodo"
   SQL = SQL & " FROM Periodo "
   SQL = SQL & " WHERE Periodo.ConsecutivoCompania = " & valConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(SQL, False)

   SQL = "(SELECT Comprobante.Numero "
   SQL = SQL & " FROM Comprobante "
   SQL = SQL & " WHERE Comprobante.NoDocumentoOrigen = Factura.TipoDeDocumento "
   SQL = SQL & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & " Factura.Numero "
   SQL = SQL & " AND Comprobante.GeneradoPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_FACTURA))
   SQL = SQL & " AND Comprobante.ConsecutivoPeriodo IN (" & SqlPeriodo & ")"
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   fSQGenerarCuentaContable = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQGenerarCuentaContable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLGenerarOtrosCargosyDescuentos(ByVal valUsaModuloDeContabilidad As Boolean, _
                                                    ByVal valConsecutivoCompania As String, _
                                                     ByVal valFechaInicial As Date, _
                                                      ByVal valFechaFinal As Date, _
                                                       ByVal valInformeEsDeBorradores As Boolean, _
                                                        ByVal valReporteEnMonedaLocal As Boolean, _
                                                         ByVal gMonedaLocalActual As Object, _
                                                          ByVal valFiltroInforme As String, _
                                                           ByVal valCampoAMostrar As String) As String
   Dim SQL As String
   Dim SqlDelReporte As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         SQL = SQL & "Factura.Moneda, "
   End If
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "2 AS ORDEN, "
   SQL = SQL & valCampoAMostrar & " as Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   SQL = SQL & "Factura.TipoDeDocumento, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   SQL = SQL & "'' AS StatusFactura, "
   SQL = SQL & "0 AS MontoOriginal, "
   SQL = SQL & "0 AS TotalIVA, "
   SQL = SQL & "0 AS TotalBaseImponible, "
   SQL = SQL & "0 AS TotalMontoExento, "
   SQL = SQL & "0 AS TotalFactura, "
   SQL = SQL & "SUM (RenglonDetalleDeOtrosCargosFactura.TotalRenglon) AS OtrosCargos, "
   SQL = SQL & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      SQL = SQL & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      SQL = SQL & " AS NumeroComprobante"
   End If
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS ComprobanteFiscal"
   SQL = SQL & " FROM (Cliente INNER JOIN "
   SQL = SQL & " Factura ON (Cliente.Codigo = Factura.CodigoCliente)"
   SQL = SQL & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN RenglonDetalleDeOtrosCargosFactura ON "
   SQL = SQL & " (Factura.TipoDeDocumento = RenglonDetalleDeOtrosCargosFactura.TipoDeDocumento)"
   SQL = SQL & " AND (Factura.Numero = RenglonDetalleDeOtrosCargosFactura.NumeroFactura)"
   SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonDetalleDeOtrosCargosFactura.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   SQL = SQL & " AND factura.CodigoVendedor = Vendedor.Codigo "
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " AND (renglonDetalleDeOtrosCargosFactura.ComoAplicaAlTotalFactura <> " & gUtilSQL.fSimpleSqlValue(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO) & ")"
   If valInformeEsDeBorradores Then
      SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltroInforme <> "" Then
      SQL = SQL & " AND " & valFiltroInforme
   End If
   SQL = SQL & " GROUP BY "
   If Not valReporteEnMonedaLocal Then
      SQL = SQL & "Factura.Moneda, "
   End If
      SQL = SQL & "Factura.Fecha, Factura.Numero, " & valCampoAMostrar & ", Factura.TipoDeDocumento, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Vendedor.Nombre"

   fSQLGenerarOtrosCargosyDescuentos = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarOtrosCargosyDescuentos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLGenerarDireccionDeDespachoXFactura(ByVal ChkFactura As Boolean, _
                                                        ByVal ChkCotizacion As Boolean, _
                                                         ByVal valConsecutivoCompania As String, _
                                                          ByVal valFechaInicial As Date, _
                                                           ByVal valFechaFinal As Date) As String
   Dim SQLFactura As String
   Dim SqlCotizacion As String
   Dim SqlDelReporte As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   If ChkFactura Then
      SQLFactura = "SELECT "
      SQLFactura = SQLFactura & gUtilSQL.fSimpleSqlValue("Factura") & " AS TipoDocumento, factura.Numero, factura.Fecha, Cliente.Nombre AS NombreCliente, Cliente.NumeroRIF, Cliente.Telefono AS TelefonoFiscal, "
      SQLFactura = SQLFactura & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.PersonaContacto"), _
                                                gUtilSQL.getIIF("Cliente.Contacto = " & gUtilSQL.fSimpleSqlValue(""), "Cliente.Nombre", "Cliente.Contacto", True), _
                                                "DireccionDeDespacho.PersonaContacto", True) & " AS PersonaContactoDespacho, "
      SQLFactura = SQLFactura & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.Ciudad"), "Cliente.Ciudad", "DireccionDeDespacho.Ciudad", True) & " AS CiudadDespacho, "
      SQLFactura = SQLFactura & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.Direccion"), "Cliente.Direccion", "DireccionDeDespacho.Direccion", True) & " AS DireccionDespacho, "
      SQLFactura = SQLFactura & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.ZonaPostal"), "cliente.ZonaPostal", "DireccionDeDespacho.ZonaPostal", True) & " AS ZonaPostalDespacho "
      SQLFactura = SQLFactura & " FROM Factura INNER JOIN"
      SQLFactura = SQLFactura & " Cliente ON factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
      SQLFactura = SQLFactura & " AND factura.CodigoCliente = Cliente.Codigo LEFT OUTER JOIN DireccionDeDespacho ON factura.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
      SQLFactura = SQLFactura & " AND factura.consecutivoCompania = DireccionDeDespacho.consecutivoCompania And factura.codigoCliente = DireccionDeDespacho.codigoCliente"
      SQLFactura = SQLFactura & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
      SQLFactura = SQLFactura & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
      SQLFactura = SQLFactura & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
      SQLFactura = SQLFactura & " AND factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   End If
   If ChkCotizacion Then
      SqlCotizacion = "SELECT "
      SqlCotizacion = SqlCotizacion & gUtilSQL.fSimpleSqlValue("Cotización") & " AS TipoDocumento, Cotizacion.Numero, Cotizacion.Fecha, Cliente.Nombre AS NombreCliente, Cliente.NumeroRIF, Cliente.Telefono AS TelefonoFiscal, "
      SqlCotizacion = SqlCotizacion & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.PersonaContacto"), _
                                                gUtilSQL.getIIF("Cliente.Contacto = " & gUtilSQL.fSimpleSqlValue(""), "Cliente.Nombre", "Cliente.Contacto", True), _
                                                "DireccionDeDespacho.PersonaContacto", True) & " AS PersonaContactoDespacho, "
      SqlCotizacion = SqlCotizacion & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.Ciudad"), "Cliente.Ciudad", "DireccionDeDespacho.Ciudad", True) & " AS CiudadDespacho, "
      SqlCotizacion = SqlCotizacion & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.Direccion"), "Cliente.Direccion", "DireccionDeDespacho.Direccion", True) & " AS DireccionDespacho, "
      SqlCotizacion = SqlCotizacion & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("DireccionDeDespacho.ZonaPostal"), "cliente.ZonaPostal", "DireccionDeDespacho.ZonaPostal", True) & " AS ZonaPostalDespacho "
      SqlCotizacion = SqlCotizacion & " FROM Cotizacion INNER JOIN"
      SqlCotizacion = SqlCotizacion & " Cliente ON Cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania"
      SqlCotizacion = SqlCotizacion & " AND Cotizacion.CodigoCliente = Cliente.Codigo LEFT OUTER JOIN DireccionDeDespacho ON Cotizacion.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
      SqlCotizacion = SqlCotizacion & " AND Cotizacion.consecutivoCompania = DireccionDeDespacho.consecutivoCompania And Cotizacion.codigoCliente = DireccionDeDespacho.codigoCliente"
      SqlCotizacion = SqlCotizacion & " WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
      SqlCotizacion = SqlCotizacion & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   End If
   If ChkFactura And Not ChkCotizacion Then
      SqlDelReporte = SQLFactura
   ElseIf Not ChkFactura And ChkCotizacion Then
      SqlDelReporte = SqlCotizacion
   Else
      SqlDelReporte = SQLFactura & " UNION " & SqlCotizacion
   End If
   SqlDelReporte = SqlDelReporte & " ORDER BY TipoDocumento, Numero"
   
   fSQLGenerarDireccionDeDespachoXFactura = SqlDelReporte
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarDireccionDeDespachoXFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLInformeFacturacionPorDia(ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object, _
                                              ByVal vReporteEnMonedaLocal As Boolean, ByVal valConsecutivoCompania As String, _
                                               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                ByVal valMostrarDocumentosGeneradosconIVAal10 As Boolean) As String
   Dim SQL As String
   Dim sqlTotalFactura As String
   Dim sqlMonedReporte As String
   Dim sqlCambio As String
   On Error GoTo h_ERROR
   
   sqlTotalFactura = "Factura.TotalMontoExento + Factura.TotalBaseImponible"
   sqlMonedReporte = "Factura.Moneda"
   sqlCambio = "Factura.CambioABolivares"
   If vReporteEnMonedaLocal Then
      sqlCambio = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Factura.Moneda", 1, False, "")
      sqlTotalFactura = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Factura.Moneda", sqlTotalFactura, False, "")
      sqlTotalFactura = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlTotalFactura, "Factura.Fecha")
      sqlMonedReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.fNombreMoneda(gUtilDate.getFechaDeHoy))
   Else
      sqlTotalFactura = "(Factura.TotalMontoExento + Factura.TotalBaseImponible)"
   End If
   SQL = "SET DATEFIRST 7 " & vbCrLf
   SQL = SQL & "SELECT "
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "(CASE DATEPART(dw, Factura.Fecha) WHEN 1 THEN 'Domingo' WHEN 2 THEN 'Lunes' WHEN 3 THEN 'Martes' WHEN 4 THEN 'Miércoles' WHEN 5 THEN 'Jueves' WHEN 6 THEN 'Viernes' WHEN 7 THEN 'Sábado' ELSE 'Domingo' END) AS DiaDeLaSemanaStr, "
   SQL = SQL & sqlMonedReporte & " AS MonedaDelReporte, "
   SQL = SQL & "Factura.Moneda, "
   SQL = SQL & sqlCambio & " AS CambioABolivares, "
   SQL = SQL & "SUM" & sqlTotalFactura & " AS MontoTotal, "
   SQL = SQL & "COUNT(Factura.Numero) AS ContadorFacturas, "
   SQL = SQL & "MAX" & sqlTotalFactura & " As FacturaMayor "
   SQL = SQL & "FROM Factura "
   SQL = SQL & "WHERE (Factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
   SQL = SQL & " AND (" & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & ")"
   SQL = SQL & " AND (StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valMostrarDocumentosGeneradosconIVAal10 Then
      SQL = SQL & " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   End If
   SQL = SQL & " GROUP BY Moneda, Fecha, CambioABolivares "
   SQL = SQL & " ORDER BY MonedaDelReporte, Fecha "
   fSQLInformeFacturacionPorDia = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInformeFacturacionPorDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturaInformeDeGananciasYPerdidas(ByVal valcodigoDeVendedor As String, ByVal valCantidadAImprimirVendedor As Object, ByVal valPorcentajeDescuentoTC As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal valNombreMonedaLocal As String, ByVal valVendedor As Enum_Vendedores_Por_Renglon, ByRef refRenglonComicionesDeVendedor As Object, ByRef refRengFac As Object, ByRef refFactura As Object, ByRef refRenglonCobroDeFactura As Object, ByVal valUsaCambio As Boolean, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim servicio As String
   Dim refMontoParticipacion As String
   Dim DescuentoTC As String
   Dim CostoProduc As String
   Dim factura As String
   Dim valParticipacionNeta As String
   Dim prorrateo As String
   Dim Tarjeta As String
   Dim PorcentajeDeCobroDeTarjeta As Currency
   Dim refAyudante As String
   Dim PorcentajeVendedor As String
   Dim MontoVendedor As String
   Dim CodigoDelRenglonDeVendedor As String
   Dim CodigoDelRenglonDeVendedorAyudante As String
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set insRenglonComicionesDeVendedor = refRenglonComicionesDeVendedor
   Set insRengFac = refRengFac
   Set insFactura = refFactura
   Set insRenglonCobroDeFactura = refRenglonCobroDeFactura
   Set gEnumReport = New clsEnumReport
   PorcentajeDeCobroDeTarjeta = gConvert.fConvierteACurrency(valPorcentajeDescuentoTC)
   Select Case valVendedor
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor1
         CodigoDelRenglonDeVendedor = "CodigoVendedor1"
         CodigoDelRenglonDeVendedorAyudante = "CodigoVendedor2"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor2
         CodigoDelRenglonDeVendedor = "CodigoVendedor2"
         CodigoDelRenglonDeVendedorAyudante = "CodigoVendedor1"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor3
         CodigoDelRenglonDeVendedor = "CodigoVendedor3"
         CodigoDelRenglonDeVendedorAyudante = ""
   End Select
   SQL = "SELECT "
   SQL = SQL & insRengFac.GetTableName & ".ConsecutivoRenglon, "
   SQL = SQL & insRengFac.GetTableName & "." & CodigoDelRenglonDeVendedor & " AS CodigoVendedor, "
   SQL = SQL & "vendedor.Nombre AS NombreVendedor, "
   SQL = SQL & "Factura.Fecha, "
   factura = "Factura.Numero "
   SQL = SQL & factura & ", "
   SQL = SQL & "articuloInventario.Codigo, "
   servicio = fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsaCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual)
   SQL = SQL & servicio & " AS TotalRenglon, "
   CostoProduc = gUtilSQL.getIIF("articuloInventario.TipoDeArticulo = " & gConvert.enumerativoAChar(enum_TipoDeArticulo.eTD_MERCANCIA), "0", "articuloInventario.CostoUnitario * RenglonFactura.Cantidad")
   SQL = SQL & CostoProduc & " AS CostoProducto, "
   PorcentajeVendedor = "RenglonComisionesDeVendedor.Porcentaje"
   SQL = SQL & PorcentajeVendedor & " AS PorcentajeParticipacion, "
   MontoVendedor = "(" & "RenglonComisionesDeVendedor.Monto * RenglonFactura.Cantidad )"
   refMontoParticipacion = gUtilSQL.getIIF("RenglonFactura.CodigoVendedor2 <> '' ", _
               "(" & fValorSINAyudanteSQLInformeGananciasYPerdidas(servicio, MontoVendedor) & ")", _
                  "(" & fValorSINAyudanteSQLInformeGananciasYPerdidas(servicio, MontoVendedor) & ")")
   refAyudante = gUtilSQL.getIIF("RenglonFactura." & CodigoDelRenglonDeVendedorAyudante & " <> '' ", _
                  "RenglonFactura." & CodigoDelRenglonDeVendedorAyudante & gUtilSQL.CharConcat() & gUtilSQL.fSimpleSqlValue(" - AYUD "), _
                  "RenglonFactura." & CodigoDelRenglonDeVendedorAyudante, True)
   SQL = SQL & refMontoParticipacion & " AS Participacion, "
   SQL = SQL & refAyudante & " AS Asociado, "
   Tarjeta = fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito(factura, valConsecutivoCompania, valNombreMonedaLocal)
   prorrateo = "(" & Tarjeta & ") / (Factura.TotalFactura)"
   DescuentoTC = "(( RenglonFactura.TotalRenglon ) * (" & PorcentajeDeCobroDeTarjeta & "/100.00)) * (" & prorrateo & ")"
   SQL = SQL & DescuentoTC & " AS MontoDescuentoTC, "
   valParticipacionNeta = "(" & refMontoParticipacion & ") - (" & DescuentoTC & ")"
   SQL = SQL & valParticipacionNeta & " AS ParticipacionNeta, "
   SQL = SQL & "(" & valParticipacionNeta & ") - (" & CostoProduc & ") AS MontoNeto, "
   SQL = SQL & fSQLRetirosACuenta(valcodigoDeVendedor, valConsecutivoCompania, valVendedor, valFechaInicial, valFechaFinal, valCantidadAImprimirVendedor) & " AS TOTALRETIROSACTA "
   SQL = SQL & " FROM " & "articuloInventario INNER JOIN (RenglonComisionesDeVendedor"
   SQL = SQL & " INNER JOIN ((vendedor"
   SQL = SQL & " INNER JOIN Factura"
   SQL = SQL & " ON(" & "vendedor.ConsecutivoCompania"
   SQL = SQL & " = Factura.ConsecutivoCompania ))"
   SQL = SQL & " INNER JOIN RenglonFactura"
   SQL = SQL & " ON (Factura.TipoDeDocumento"
   SQL = SQL & " = RenglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (Factura.Numero"
   SQL = SQL & " = RenglonFactura.NumeroFactura)"
   SQL = SQL & " AND (Factura.ConsecutivoCompania"
   SQL = SQL & " = RenglonFactura.ConsecutivoCompania)) "
   SQL = SQL & " ON (" & "RenglonComisionesDeVendedor.CodigoVendedor"
   SQL = SQL & " = " & "vendedor.Codigo)"
   SQL = SQL & " AND (" & "RenglonComisionesDeVendedor.ConsecutivoCompania"
   SQL = SQL & " = " & "vendedor.ConsecutivoCompania))"
   SQL = SQL & " ON (" & "articuloInventario.Codigo"
   SQL = SQL & " = RenglonFactura.Articulo)"
   SQL = SQL & " AND (" & "articuloInventario.ConsecutivoCompania"
   SQL = SQL & " = RenglonFactura.ConsecutivoCompania )"
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RenglonFactura." & CodigoDelRenglonDeVendedor & " = " & "vendedor.Codigo"
   SQL = SQL & " AND RenglonFactura.Articulo = " & "articuloInventario.Codigo"
   SQL = SQL & " AND " & "articuloInventario.LineaDeProducto = " & "RenglonComisionesDeVendedor.NombreDeLineaDeProducto"
   SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valCantidadAImprimirVendedor <> gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
       SQL = SQL & " AND RenglonFactura." & CodigoDelRenglonDeVendedor & " = " & _
           gUtilSQL.fSimpleSqlValue(Trim(valcodigoDeVendedor))
   End If
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteFacturaInformeDeGananciasYPerdidas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteFacturaInformeDeGananciasYPerdidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fValorSINAyudanteSQLInformeGananciasYPerdidas(ByVal servicio As String, ByVal MontoVendedor As String) As String
   Dim MontoParticipacion As String
   On Error GoTo h_ERROR
   MontoParticipacion = gUtilSQL.getIIF("RenglonComisionesDeVendedor.Porcentaje = 0 ", _
                        "(" & MontoVendedor & ")", _
         "(" & servicio & " * (" & "RenglonComisionesDeVendedor.Porcentaje/100.00))", True)
   fValorSINAyudanteSQLInformeGananciasYPerdidas = MontoParticipacion
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fValorSINAyudanteSQLInformeGananciasYPerdidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFacturacionPorVendedor(ByVal valReporteEnMonedaLocal As Boolean, ByVal gMonedaLocalActual As Object, _
                                             ByVal valConsecutivoCompania As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                              ByVal valInformeSoloUnVendedor As Boolean, ByVal valNombreDeVendedor As String, _
                                               ByVal valComisiones As String) As String
   Dim SQL As String
   Dim valMontoIva As String
   Dim valMontoDesc As String
   Dim valSqlTotalFactura As String
   On Error GoTo h_ERROR
   If valComisiones Then
      valSqlTotalFactura = "factura.TotalMontoExento + factura.TotalIVA + factura.TotalBaseImponible"
      SQL = "SELECT "
      SQL = SQL & "vendedor.Nombre as NombreVendedor, "
      SQL = SQL & "vendedor.Codigo AS CodigoVendedor, "
      SQL = SQL & "cliente.Nombre AS NombreCliente, "
      SQL = SQL & "cliente.Codigo AS CodigoCliente, "
      SQL = SQL & "factura.Fecha, "
      SQL = SQL & "factura.Numero, "
      SQL = SQL & "factura.TipoDeDocumento, "
      SQL = SQL & "factura.StatusFactura, "
      If valReporteEnMonedaLocal Then
         'SQL = SQL & "(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalFactura", "Factura.Fecha") & " * factura.CambioABolivares) AS TotalFact, "
         'SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalFactura", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalFact, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valSqlTotalFactura, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalFact, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("factura.TotalFactura", 2) & " AS MontoOriginal, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalMontoExento", "Factura.Fecha") & " *factura.CambioABolivares", 2) & " AS TotalMontoExent, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalIVA", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalesIVA, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalBaseImponible", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalBaseImp, "
      Else
         SQL = SQL & gUtilSQL.fRoundNDecimales(valSqlTotalFactura, 2) & " AS TotalFact, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("factura.TotalFactura", 2) & " AS MontoOriginal, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("factura.TotalMontoExento", 2) & " AS TotalMontoExent, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("factura.TotalIVA", 2) & " AS TotalesIVA, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("factura.TotalBaseImponible", 2) & " AS TotalBaseImp, "
      End If
      SQL = SQL & "factura.Moneda, "
      SQL = SQL & "factura.CambioABolivares"
      SQL = SQL & " FROM cliente INNER JOIN (vendedor INNER JOIN factura"
      SQL = SQL & " ON (vendedor.Codigo = factura.CodigoVendedor)"
      SQL = SQL & " AND (vendedor.ConsecutivoCompania = factura.ConsecutivoCompania))"
      SQL = SQL & " ON (cliente.Codigo = factura.CodigoCliente)"
      SQL = SQL & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)"
      SQL = SQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
      SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      SQL = SQL & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      If valInformeSoloUnVendedor Then
         SQL = SQL & " AND vendedor.Nombre = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreDeVendedor))
      End If
      SQL = SQL & " ORDER BY Vendedor.Nombre, factura.moneda, factura.Fecha "
   Else
      valMontoDesc = "((IGV_VendedorXRenglonFactura.TotalRenglon * factura.PorcentajeDescuento) / 100)"
      valMontoIva = "(((IGV_VendedorXRenglonFactura.TotalRenglon -" & valMontoDesc & " ) * factura.PorcentajeAlicuota1) / 100)"
      valMontoDesc = gUtilSQL.fRoundNDecimales(valMontoDesc, 2)
      valMontoIva = gUtilSQL.fRoundNDecimales(valMontoIva, 2)
      SQL = "SELECT "
      SQL = SQL & "IGV_VendedorXRenglonFactura.NombreVendedor as NombreVendedor, "
      SQL = SQL & "IGV_VendedorXRenglonFactura.CodigoVendedor AS CodigoVendedor, "
      SQL = SQL & "cliente.Nombre AS NombreCliente, "
      SQL = SQL & "cliente.Codigo AS CodigoCliente, "
      SQL = SQL & "factura.Fecha, "
      SQL = SQL & "factura.Numero, "
      SQL = SQL & "factura.TipoDeDocumento, "
      SQL = SQL & "factura.StatusFactura, "
      If valReporteEnMonedaLocal Then
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalFact, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, 2) & " AS MontoOriginal, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalMontoExento", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalMontoExent, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoIva, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalesIVA, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalBaseImp, "
      Else
         SQL = SQL & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, 2) & " AS TotalFact, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, 2) & " AS MontoOriginal, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("factura.TotalMontoExento", 2) & " AS TotalMontoExent, "
         SQL = SQL & gUtilSQL.fRoundNDecimales(valMontoIva, 2) & " AS TotalesIVA, "
         SQL = SQL & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc, 2) & " AS TotalBaseImp, "
      End If
      SQL = SQL & "factura.Moneda, "
      SQL = SQL & "factura.CambioABolivares"
      SQL = SQL & " FROM cliente INNER JOIN (IGV_VendedorXRenglonFactura INNER JOIN factura"
      SQL = SQL & " ON (IGV_VendedorXRenglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania)"
      SQL = SQL & " AND IGV_VendedorXRenglonFactura.NumeroFactura = factura.Numero "
      SQL = SQL & " AND IGV_VendedorXRenglonFactura.TipodeDocumento = factura.tipodedocumento )"
       SQL = SQL & " ON (cliente.Codigo = factura.CodigoCliente)"
      SQL = SQL & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)"
      SQL = SQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
      SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      SQL = SQL & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      If valInformeSoloUnVendedor Then
         SQL = SQL & " AND IGV_VendedorXRenglonFactura.NombreVendedor = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreDeVendedor))
      End If
      SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      SQL = SQL & " ORDER BY IGV_VendedorXRenglonFactura.NombreVendedor, factura.moneda, factura.Fecha "
   End If
h_EXIT: On Error GoTo 0
   fSQLFacturacionPorVendedor = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturacionPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLInformeDeFacturacionPorCliente(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                    ByVal valMostrarRenglones As Boolean, ByVal valUsaMonedaExtYLaMonedaDelReporteEnBs As Boolean, _
                                                     ByVal valImprimirVersion As Boolean, ByVal valImprimirFacturasAnuladas As Boolean, _
                                                      ByVal valMostrarLineaDeProducto As Boolean, ByVal valLineaDeProducto As String, _
                                                       ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, _
                                                        ByVal valConsecutivoCompania As String, ByVal valImprimirSoloUnCliente As Boolean) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT "
   SQL = SQL & "factura.CodigoCliente, "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "factura.Fecha, "
   SQL = SQL & "factura.Numero, "
   SQL = SQL & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
               gUtilSQL.fSimpleSqlValue(""), True) & " AS StatusFactura, "
   If valMostrarRenglones Then
      If valMostrarLineaDeProducto Then
         SQL = SQL & "articuloInventario.LineaDeProducto, "
      End If
      SQL = SQL & "renglonFactura.Articulo, "
      SQL = SQL & "renglonFactura.PorcentajeDescuento, "
   End If
   If valUsaMonedaExtYLaMonedaDelReporteEnBs Then
      If valMostrarRenglones Then
         SQL = SQL & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(True, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonSinIva, "
         SQL = SQL & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(True, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonConIva, "
      End If
      SQL = SQL & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", _
                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", "Factura.Fecha"), True, ""), True) & _
                  " AS TotalFactura, "
   Else
      If valMostrarRenglones Then
         SQL = SQL & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(False, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonSinIva, "
         SQL = SQL & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(False, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonConIva, "
      End If
      SQL = SQL & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   If valImprimirVersion Then
      SQL = SQL & "articuloInventario.NombrePrograma, "
      SQL = SQL & "(SELECT MAX(versionPrograma.versionprograma) " & _
                  " FROM versionPrograma " & _
                  " WHERE versionPrograma.Fecha <= Factura.Fecha " & _
                  " AND versionPrograma.NombrePrograma = articuloInventario.NombrePrograma) " & _
                  " AS versionPrograma, "
   End If
   If valMostrarRenglones Then
      SQL = SQL & "renglonFactura.Cantidad, "
   End If
   If valUsaMonedaExtYLaMonedaDelReporteEnBs Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS MonedaReporte, "
   Else
      SQL = SQL & "factura.Moneda AS MonedaReporte, "
   End If
   SQL = SQL & "factura.Moneda AS MonedaDocumento, "
   SQL = SQL & "factura.CambioABolivares, "
   SQL = SQL & "factura.TipoDeDocumento, "
   SQL = SQL & "factura.TipoDeDocumento" & gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat & "factura.Numero AS CorteDeGrupo"
   If valImprimirVersion Then
      SQL = SQL & " FROM Cliente INNER JOIN (factura INNER JOIN (articuloInventario INNER JOIN renglonFactura "
      SQL = SQL & " ON (articuloInventario.Codigo = renglonFactura.Articulo) "
      SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
      SQL = SQL & " ON (factura.Numero = renglonFactura.NumeroFactura)"
      SQL = SQL & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
      SQL = SQL & " AND (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento))"
      SQL = SQL & " ON (Cliente.Codigo = factura.CodigoCliente)"
      SQL = SQL & " AND (Cliente.ConsecutivoCompania = factura.ConsecutivoCompania)"
   Else
      If valMostrarRenglones Then
         SQL = SQL & " FROM (Cliente INNER JOIN factura "
         SQL = SQL & " ON (Cliente.Codigo = factura.CodigoCliente)"
         SQL = SQL & " AND (Cliente.ConsecutivoCompania = factura.ConsecutivoCompania))"
         SQL = SQL & " INNER JOIN renglonFactura"
         SQL = SQL & " ON (factura.Numero = renglonFactura.NumeroFactura)"
         SQL = SQL & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
         SQL = SQL & " AND (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento)"
         If valMostrarLineaDeProducto Then
            SQL = SQL & " INNER JOIN ArticuloInventario "
            SQL = SQL & " ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
            SQL = SQL & " AND renglonFactura.Articulo = ArticuloInventario.Codigo "
         End If
      Else
         SQL = SQL & " FROM Cliente INNER JOIN factura "
         SQL = SQL & " ON Cliente.ConsecutivoCompania = factura.ConsecutivoCompania "
         SQL = SQL & " AND Cliente.Codigo = factura.CodigoCliente "
      End If
   End If
   SQL = SQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimirSoloUnCliente Then
      SQL = SQL & " AND factura.CodigoCliente = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   If valImprimirFacturasAnuladas Then
      SQL = SQL & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR (factura.StatusFactura  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & "))"
   Else
      SQL = SQL & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   End If
   If valMostrarLineaDeProducto Then
      SQL = SQL & " AND (ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(valLineaDeProducto) & ")"
   End If
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   SQL = SQL & " ORDER BY "
   SQL = SQL & "factura.Moneda, factura.Fecha, "
   SQL = SQL & "factura.TipoDeDocumento" & gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat & "factura.Numero, "
   SQL = SQL & "factura.TipoDeDocumento, "
   SQL = SQL & "factura.Numero"
   Set gEnumProyecto = Nothing
h_EXIT:
   fSQLInformeDeFacturacionPorCliente = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInformeDeFacturacionPorCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito(ByVal factura As String, ByVal valConsecutivoCompania As Long, ByVal valNombreMonedaLocal As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "SUM (" & gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_TARJETA), "renglonCobroDeFactura.Monto", _
               0) & ")"
   SQL = SQL & " FROM formaDelCobro INNER JOIN "
   SQL = SQL & "(" & insFactura.GetTableName
   SQL = SQL & " As FacturaInner INNER JOIN renglonCobroDeFactura ON "
   SQL = SQL & " (FacturaInner.TipoDeDocumento" & _
               " = " & "renglonCobroDeFactura.TipoDeDocumento)"
   SQL = SQL & " AND (FacturaInner.Numero" & _
              " = " & "renglonCobroDeFactura.NumeroFactura)"
   SQL = SQL & " AND (FacturaInner.ConsecutivoCompania" & _
              " = " & "renglonCobroDeFactura.ConsecutivoCompania)) ON "
   SQL = SQL & "formaDelCobro." & "Codigo" & _
              " = " & "renglonCobroDeFactura.CodigoFormaDelCobro"
   SQL = SQL & " WHERE ( FacturaInner.Numero" & _
               " = " & factura & ")"
   SQL = SQL & " AND (FacturaInner.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania & ")"
  fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLRetirosACuenta(ByVal CodigoVendedor As String, ByVal valConsecutivoCompania As Long, ByVal valVendedor As Enum_Vendedores_Por_Renglon, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCantidadAImprimirVendedor As Object) As String

   Dim SQL As String
   Set gEnumReport = New clsEnumReport
   On Error GoTo h_ERROR
    Select Case valVendedor
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor1
         CodigoVendedor = "RenglonFactura.CodigoVendedor1"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor2
         CodigoVendedor = "RenglonFactura.CodigoVendedor2"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor3
         CodigoVendedor = "RenglonFactura.CodigoVendedor3"
   End Select
   SQL = "(SELECT SUM(" & "RetirosACuenta.Monto)"
   SQL = SQL & " FROM RetirosACuenta INNER JOIN vendedor ON RetirosACuenta.ConsecutivoCompania = vendedor.ConsecutivoCompania AND  RetirosACuenta.CodigoVendedor = Vendedor.codigo"
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("RetirosACuenta.Fecha", valFechaInicial, valFechaFinal) & ")"
   If valCantidadAImprimirVendedor = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND (" & "RetirosACuenta.CodigoVendedor = " & CodigoVendedor & ")"
   End If
   SQL = SQL & " AND (" & "vendedor.Codigo = " & CodigoVendedor & ")"
   SQL = SQL & " AND (" & "RetirosACuenta.ConsecutivoCompania = " & valConsecutivoCompania & "))"
   Set gEnumReport = Nothing
h_EXIT:    On Error GoTo 0
   fSQLRetirosACuenta = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLRetirosACuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLVentasMensualesPorCategoria(ByVal valFacturaTableName As String, ByVal valAno As String, ByVal valTipoDeProducto As Integer, ByVal valConsecutivoCompania As String, ByRef insRenglonFactura As Object, ByRef insFactura As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   insRenglonFactura.setClaseDeTrabajo insFactura.getClaseDeTrabajo
   SQL = "SELECT " & gUtilSQL.DfSQLMonthOfDate(insFactura.GetTableName & "." & insFactura.getFN_FECHA, "Mes") & ", "
   SQL = SQL & "SUM(" & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CANTIDAD & ") AS Cant, "
   SQL = SQL & "articuloInventario.Categoria"
   SQL = SQL & " FROM " & insFactura.GetTableName & " INNER JOIN ("
   SQL = SQL & "articuloInventario INNER JOIN " & insRenglonFactura.GetTableName
   SQL = SQL & " ON (" & "articuloInventario.Codigo = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO
   SQL = SQL & ") AND (" & "articuloInventario.ConsecutivoCompania = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA
   SQL = SQL & ")) ON (" & valFacturaTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_TIPO_DE_DOCUMENTO
   SQL = SQL & ") AND (" & valFacturaTableName & "." & insFactura.getFN_NUMERO & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_NUMERO_FACTURA
   SQL = SQL & ") AND (" & valFacturaTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA & ")   "
   SQL = SQL & " WHERE " & valFacturaTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & valConsecutivoCompania
   SQL = SQL & " AND " & "articuloInventario.TipoDeProducto = " & gUtilSQL.fSimpleSqlValue(valTipoDeProducto) & ""
   SQL = SQL & " AND " & gUtilSQL.DfSQLYearOfDate(valFacturaTableName & "." & insFactura.getFN_FECHA) & " = " & gConvert.fConvertStringToLong(Trim(valAno))
   SQL = SQL & " AND " & valFacturaTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND " & valFacturaTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " GROUP BY " & "articuloInventario.Categoria, "
   SQL = SQL & gUtilSQL.DfSQLMonthOfDate(valFacturaTableName & "." & insFactura.getFN_FECHA)
   SQL = SQL & " ORDER BY " & "articuloInventario.Categoria"
h_EXIT: On Error GoTo 0
   fSQLVentasMensualesPorCategoria = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLVentasMensualesPorCategoria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLProductosVendidosPorZona(ByVal valSoloEntreFechas As Boolean, ByVal valConsecutivoCompania As Long, ByVal valZonaDesde As String, ByVal valZonaHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insFactura As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "cliente.Contacto, "
   SQL = SQL & "cliente.Direccion, "
   SQL = SQL & "cliente.Telefono, "
   SQL = SQL & "cliente.ZonaPostal, "
   SQL = SQL & "cliente.Codigo, "
   SQL = SQL & "cliente.Ciudad"
   If valSoloEntreFechas = True Then
      SQL = SQL & ", MIN(" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & ")"
      SQL = SQL & " FROM " & "cliente INNER JOIN " & insFactura.GetTableName
      SQL = SQL & " ON (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                  " = " & "cliente.ConsecutivoCompania"
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & _
                  " = " & "cliente.Codigo)"
   Else
      SQL = SQL & " FROM cliente"
   End If
   SQL = SQL & " WHERE " & "cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween("cliente.ZonaPostal", gConvert.fConvierteAString(Trim(valZonaDesde)), gConvert.fConvierteAString(Trim(valZonaHasta)))
   If valSoloEntreFechas = True Then
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaInicial, valFechaFinal)
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
      SQL = SQL & " GROUP BY "
      SQL = SQL & "cliente.Ciudad, "
      SQL = SQL & "cliente.Nombre, "
      SQL = SQL & "cliente.Contacto, "
      SQL = SQL & "cliente.Direccion, "
      SQL = SQL & "cliente.Telefono, "
      SQL = SQL & "cliente.ZonaPostal, "
      SQL = SQL & "cliente.Codigo"
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "cliente.ZonaPostal, "
   SQL = SQL & "cliente.Nombre"
h_EXIT: On Error GoTo 0
   fSQLProductosVendidosPorZona = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaElInformeProductosPorZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLProductosVendidosPorCiudad(ByVal valSoloEntreFechas As Boolean, ByVal valConsecutivoCompania As Long, ByVal valCiudadDesde As String, ByVal valCiudadHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insFactura As Object, ByVal valUnVendedor As Boolean, ByVal valcodigoVendedor As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "cliente.Contacto, "
   SQL = SQL & "cliente.Direccion, "
   SQL = SQL & "cliente.Telefono, "
   SQL = SQL & "cliente.ZonaPostal, "
   SQL = SQL & "cliente.Codigo, "
   SQL = SQL & "cliente.Ciudad"
   If valSoloEntreFechas Then
      SQL = SQL & ", Factura.CodigoVendedor AS CodigoVendedor ,"
      SQL = SQL & "vendedor.Nombre AS NombreVendedor, "
      SQL = SQL & gUtilSQL.fCast("cliente.Ciudad", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fCast("Factura.CodigoVendedor", eTDSS_VARCHAR, "") & " AS CiudadVendedor"
      SQL = SQL & ", MIN(" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & ")"
      SQL = SQL & " FROM " & "cliente INNER JOIN " & insFactura.GetTableName
      SQL = SQL & " ON " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                  " = " & "cliente.ConsecutivoCompania"
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & _
                  " = " & "cliente.Codigo"
      SQL = SQL & " Inner Join vendedor ON factura.ConsecutivoCompania = vendedor.ConsecutivoCompania AND factura.CodigoVendedor = vendedor.Codigo"
   Else
      SQL = SQL & " FROM cliente"
   End If
   SQL = SQL & " WHERE "
   SQL = SQL & "cliente.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween("cliente.Ciudad", gConvert.fConvierteAString(Trim(valCiudadDesde)), gConvert.fConvierteAString(Trim(valCiudadHasta)))
   If valSoloEntreFechas Then
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaInicial, valFechaFinal)
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
      If valUnVendedor Then
         SQL = gUtilSQL.fSQLValueWithAnd(SQL, "factura.CodigoVendedor", valcodigoVendedor, False)
      End If
      SQL = SQL & " GROUP BY "
      SQL = SQL & "cliente.Ciudad, "
      SQL = SQL & "cliente.Nombre, "
      SQL = SQL & "cliente.Contacto, "
      SQL = SQL & "cliente.Direccion, "
      SQL = SQL & "cliente.Telefono, "
      SQL = SQL & "cliente.ZonaPostal, "
      SQL = SQL & "cliente.Codigo, "
      SQL = SQL & "Factura.CodigoVendedor ,"
      SQL = SQL & "vendedor.Nombre"
   End If
   If valSoloEntreFechas Then
      SQL = SQL & " ORDER BY " & "Factura.CodigoVendedor , cliente.Ciudad,"
   Else
      SQL = SQL & " ORDER BY " & "cliente.Ciudad,"
   End If
   SQL = SQL & "cliente.Nombre"
h_EXIT: On Error GoTo 0
   fSQLProductosVendidosPorCiudad = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLProductosVendidosPorCiudad", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteEstadisticoDeVentas(ByVal valPorTrimestre As Boolean, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valAno As String, ByVal valConsecutivoCompania As Long, ByVal valEsSistemaParaIG As Boolean, ByVal valPorTipoDeProducto As Boolean, ByVal usaCambioABs As Boolean, ByVal valHoyNombreMoneda As String, ByRef insFact As Object, ByRef insRengFact As Object, ByVal gEnumProyecto As Object) As String
   Dim SQL As String
   Dim enero As Date
   Dim abril As Date
   Dim julio As Date
   Dim octubre As Date
   Dim diciembre As Date
   Dim sqlTrimestre As String
   Dim sqlFecha As String
'   Dim usaCambioABs As Boolean
   On Error GoTo h_ERROR
   insFact.setClaseDeTrabajo eCTFC_Factura
   insRengFact.setClaseDeTrabajo eCTFC_Factura
'   usaCambioABs = True
   sqlFecha = insFact.GetTableName & "." & insFact.getFN_FECHA
   enero = gConvert.fConvertStringToDate("01/01/" & valAno, True)
   abril = gConvert.fConvertStringToDate("01/04/" & valAno, True)
   julio = gConvert.fConvertStringToDate("01/07/" & valAno, True)
   octubre = gConvert.fConvertStringToDate("01/10/" & valAno, True)
   diciembre = gConvert.fConvertStringToDate("31/12/" & valAno, True)
   sqlTrimestre = sqlTrimestre & gUtilSQL.getIIF(sqlFecha & " >= " & gUtilSQL.fDateToSQLValue(enero) & _
               " AND " & sqlFecha & " < " & gUtilSQL.fDateToSQLValue(abril), gUtilSQL.fSimpleSqlValue("1 (Ene-Feb-Mar)"), _
               gUtilSQL.getIIF(sqlFecha & " >= " & gUtilSQL.fDateToSQLValue(abril) & _
               " AND " & sqlFecha & " < " & gUtilSQL.fDateToSQLValue(julio), gUtilSQL.fSimpleSqlValue("2 (Abr-May-Jun)"), _
               gUtilSQL.getIIF(sqlFecha & " >= " & gUtilSQL.fDateToSQLValue(julio) & _
               " AND " & sqlFecha & " < " & gUtilSQL.fDateToSQLValue(octubre), gUtilSQL.fSimpleSqlValue("3 (Jul-Ago-Sep)"), _
               gUtilSQL.fSimpleSqlValue("4 (Oct-Nov-Dic)"), True), True), True)
   SQL = "SELECT "
   If usaCambioABs Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(valHoyNombreMoneda) & " AS MonedaReporte, "
   Else
      SQL = SQL & "factura.Moneda AS MonedaReporte, "
   End If
   If valPorTrimestre Then
      SQL = SQL & sqlTrimestre & " AS Trimestre, "
   End If
   If valEsSistemaParaIG Then
      SQL = SQL & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoCategoria, "
   Else
      SQL = SQL & "articuloInventario.Categoria AS TipoCategoria, "
      If valEsSistemaParaIG Then
         SQL = SQL & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoDeProd, "
      End If
   End If
    
   SQL = SQL & "SUM(" & insRengFact.GetTableName & "." & insRengFact.getFN_CANTIDAD & ") AS Cantidad, "
   SQL = SQL & "SUM(" & insRengFact.fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usaCambioABs, False) & ") AS MontoConIva, "
   SQL = SQL & "SUM(" & insRengFact.fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(usaCambioABs, False) & ") AS MontoSinIva, "
   SQL = SQL & "SUM(" & insRengFact.fSQLMontoTotalDelRenglonDeFactura(usaCambioABs, False) & ") AS MontoBrutoFactura "
   SQL = SQL & " FROM " & insFact.GetTableName & " INNER JOIN ("
   SQL = SQL & "articuloInventario INNER JOIN "
   SQL = SQL & insRengFact.GetTableName & " ON ("
   SQL = SQL & "articuloInventario.Codigo" & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_ARTICULO
   SQL = SQL & ") AND (" & "articuloInventario.ConsecutivoCompania" & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_CONSECUTIVO_COMPANIA
   SQL = SQL & ")) ON (" & insFact.GetTableName & "." & insFact.getFN_NUMERO & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_NUMERO_FACTURA
   SQL = SQL & ") AND (" & insFact.GetTableName & "." & insFact.getFN_TIPO_DE_DOCUMENTO & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_TIPO_DE_DOCUMENTO
   SQL = SQL & ") AND (" & insFact.GetTableName & "." & insFact.getFN_CONSECUTIVO_COMPANIA & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_CONSECUTIVO_COMPANIA & ")"
   SQL = SQL & " WHERE " & insFact.GetTableName & "." & insFact.getFN_CONSECUTIVO_COMPANIA & " = " & valConsecutivoCompania
   SQL = SQL & " AND " & insFact.GetTableName & "." & insFact.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   SQL = SQL & " AND " & insFact.GetTableName & "." & insFact.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND " & insFact.GetTableName & "." & insFact.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   If valPorTrimestre Then
      SQL = SQL & " AND " & gUtilSQL.fYear(sqlFecha, "") & " = " & valAno
      SQL = SQL & " GROUP BY " & sqlTrimestre & ", "
   Else
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(sqlFecha, valFechaInicial, valFechaFinal)
      SQL = SQL & " GROUP BY "
   End If
   If valPorTipoDeProducto Then
      SQL = SQL & "articuloInventario.TipoDeProducto"
   Else
      SQL = SQL & "articuloInventario.Categoria"
      If valEsSistemaParaIG Then
         SQL = SQL & ", " & "articuloInventario.TipoDeProducto"
      End If
   End If
   If Not usaCambioABs Then
      SQL = SQL & ", " & "factura.Moneda"
   End If
   SQL = SQL & " ORDER BY "
   If valPorTrimestre Then
      SQL = SQL & sqlTrimestre & ", "
   End If
   If valPorTipoDeProducto Then
      SQL = SQL & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto)
   Else
      SQL = SQL & "articuloInventario.Categoria"
      If valEsSistemaParaIG Then
         SQL = SQL & ", " & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto)
      End If
   End If
   If valEsSistemaParaIG And Not valPorTipoDeProducto Then
      SQL = SQL & ", " & "articuloInventario.TipoDeProducto"
   End If
h_EXIT:
   fSQLDelReporteEstadisticoDeVentas = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelReporteEstadisticoDeVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteFacturacionPorUsuario(ByVal usarCambioOriginal As Boolean, ByVal valConsecutivoCompania As String, _
                                       ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                              ByVal valUsuario As String, ByVal valMoneda As Boolean, _
                                                ByVal valUsaModuloContabilidad As Boolean, ByVal valMostrarDetalle As Boolean, _
                                                    ByVal valCantidadAImprimir As Boolean, ByVal gMonedaLocalActual As Object, _
                                                       ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   
   SQL = "SELECT factura.NombreOperador, "
   SQL = SQL & "factura.Fecha, "
   SQL = SQL & "factura.Numero, "
   SQL = SQL & "factura.TipoDeDocumento, "
   SQL = SQL & "factura.PorcentajeDescuento AS PorcDesctoFact, "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "factura.TipoDeDocumento +  CHAR(9)  + factura.Numero AS CampoDeGrupo, "
   SQL = SQL & "factura.Moneda, "
   SQL = SQL & "factura.CambioABolivares, "
   If valMoneda Then
      SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalBaseImponible + Factura.TotalMontoExento", "Factura.Fecha"), usarCambioOriginal, "TotalFactura")
   Else
      SQL = SQL & " factura.TotalFactura "
   End If
   If valUsaModuloContabilidad Then
      SQL = SQL & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), gUtilSQL.fSimpleSqlValue("No Aplica"), True) & " AS NumeroComprobante "
   End If
   If valMostrarDetalle Then
      SQL = SQL & " , renglonFactura.Articulo, renglonFactura.Descripcion, renglonFactura.PorcentajeDescuento AS PorcDectoRenglon, renglonFactura.Cantidad,"
      If valMoneda Then
         SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioSinIVA", "factura.Fecha"), usarCambioOriginal, "PrecioSinIVA") & ", "
         SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioConIva", "factura.Fecha"), usarCambioOriginal, "PrecioConIva") & ", "
         SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.TotalRenglon", "factura.Fecha"), usarCambioOriginal, "TotalRenglon")
      Else
         SQL = SQL & " renglonFactura.PrecioConIVA , renglonFactura.PrecioSinIVA, renglonFactura.TotalRenglon"
      End If
      SQL = SQL & " FROM (cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) INNER JOIN renglonFactura ON (factura.Numero = renglonFactura.NumeroFactura) AND (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento) AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   Else
      SQL = SQL & " FROM cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente) AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)"
   End If
   SQL = SQL & " WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   If valCantidadAImprimir Then
      SQL = SQL & "  AND factura.NombreOperador = " & valUsuario
   End If
   SQL = SQL & " ORDER BY factura.NombreOperador, factura.Moneda, factura.Fecha, factura.TipoDeDocumento +  CHAR(9)  + factura.Numero, factura.TipoDeDocumento, factura.Numero"
h_EXIT: On Error GoTo 0
   fSQLDelReporteFacturacionPorUsuario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLDelReporteFacturacionPorUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComparativoPorAnoPorMeses(ByVal valAnoInicial As Integer, ByVal valAnoFinal As Integer, _
                                       ByVal gMonedaLocalActual As Object, ByVal valMontoSinIva As String, _
                                           ByVal valMontoConIva As String, ByVal valFechaDocumentoOrigen As String, _
                                              ByVal valConsecutivoCompania As Long, ByVal valSistemaInterno As Boolean) As String
   Dim SQL As String
   Dim i As Integer
   On Error GoTo h_ERROR
   SQL = "SELECT "
   For i = valAnoInicial To valAnoFinal
      If valSistemaInterno Then
         SQL = SQL & gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("Fecha") & " = " & gConvert.fConvierteAString(i), "SUM(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoSinIva, valFechaDocumentoOrigen) & ")", "0", True), 2) & " AS TotalFact" & gConvert.fConvierteAString(i) & ", " 'Facturas sin Iva
      Else
         SQL = SQL & gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("Fecha") & " = " & gConvert.fConvierteAString(i), "SUM(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoConIva, valFechaDocumentoOrigen) & ")", "0", True), 2) & " AS TotalFact" & gConvert.fConvierteAString(i) & ", " 'Facturas con Iva
      End If
   Next
   SQL = SQL & gUtilSQL.DfSQLMonthOfDate("Fecha", "Mes") & ", "
   SQL = SQL & gUtilSQL.DfSQLYearOfDate("Fecha", "Ano")
   SQL = SQL & " FROM Factura "
   SQL = SQL & " WHERE  StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween(gUtilSQL.DfSQLYearOfDate("Fecha"), valAnoInicial, valAnoFinal)
   SQL = SQL & " AND TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " AND ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " GROUP BY "
   SQL = SQL & gUtilSQL.DfSQLMonthOfDate("Fecha") & ", "
   SQL = SQL & gUtilSQL.DfSQLYearOfDate("Fecha")
   SQL = SQL & " ORDER BY MONTH(FECHA) "
h_EXIT: On Error GoTo 0
   fSQLComparativoPorAnoPorMeses = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLComparativoPorAnoPorMeses", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComparativoPorAnoPorTipoDeProductoOCategoria(ByVal valUsaPrecioSinIvaTipoDocumento As Boolean, ByVal valSistemaInterno As Boolean, _
                                                   ByVal valAnoInicial As Integer, ByVal valAnoFinal As Integer, _
                                                      ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                                            ByVal valFechaDocumentoOrigen As String, ByVal valStatusFactura As String, _
                                                              ByVal valConsecutivoCompania As Long, ByVal valTipoDocumento As String, _
                                                                 ByVal valEstDeVentas As Boolean, ByVal gEnumProyecto As Object) As String
   Dim SQL As String
   Dim i As Integer
   Dim sqlMtoTotalRenglon As String
   On Error GoTo h_ERROR
   If valUsaPrecioSinIvaTipoDocumento Or valSistemaInterno Then
      sqlMtoTotalRenglon = "(renglonFactura.PrecioSinIVA * renglonFactura.Cantidad) "
      sqlMtoTotalRenglon = "(" & sqlMtoTotalRenglon & " * (1 - (renglonFactura.PorcentajeDescuento / 100))) "
      sqlMtoTotalRenglon = "(" & sqlMtoTotalRenglon & " * (1 - (factura.PorcentajeDescuento / 100))) "
      sqlMtoTotalRenglon = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoTotalRenglon, valFechaDocumentoOrigen)
   Else
      sqlMtoTotalRenglon = "(renglonFactura.PrecioConIVA * renglonFactura.Cantidad) "
      sqlMtoTotalRenglon = "(" & sqlMtoTotalRenglon & " * (1 - (renglonFactura.PorcentajeDescuento / 100))) "
      sqlMtoTotalRenglon = "(" & sqlMtoTotalRenglon & " * (1 - (factura.PorcentajeDescuento / 100))) "
      sqlMtoTotalRenglon = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoTotalRenglon, valFechaDocumentoOrigen)
   End If
      sqlMtoTotalRenglon = "(" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", sqlMtoTotalRenglon, True, "") & ")"
   SQL = "SELECT "
   For i = valAnoInicial To valAnoFinal
      SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("factura.Fecha") & " = " & gConvert.fConvierteAString(i), _
                  "SUM(" & sqlMtoTotalRenglon & ")", "0", True) & " AS TotalFact" & gConvert.fConvierteAString(i) & ", "
      SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("factura.Fecha") & " = " & gConvert.fConvierteAString(i), _
                  "SUM(renglonFactura.Cantidad)", "0", True) & " AS UnidadesVendidas" & gConvert.fConvierteAString(i) & ", "
   Next
   If valEstDeVentas Then
      SQL = SQL & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoDeProducto, "
   Else 'If optEstDeVentas(1).value Then
      SQL = SQL & "articuloInventario.Categoria, "
      If valSistemaInterno Then
         SQL = SQL & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoDeProducto, "
      End If
   End If
   SQL = SQL & gUtilSQL.DfSQLYearOfDate("Fecha", "Ano")
   SQL = SQL & " FROM factura INNER JOIN (articuloInventario INNER JOIN"
   SQL = SQL & " renglonFactura ON (articuloInventario.Codigo = renglonFactura.articulo) "
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
   SQL = SQL & " ON (factura.Numero = renglonFactura.NumeroFactura) "
   SQL = SQL & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE  factura.StatusFactura = " & valStatusFactura
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween(gUtilSQL.DfSQLYearOfDate("factura.Fecha"), valAnoInicial, valAnoFinal)
   SQL = SQL & " AND factura.TipoDeDocumento <> " & valTipoDocumento
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   If valEstDeVentas Then
      SQL = SQL & " GROUP BY articuloInventario.TipoDeProducto, "
      SQL = SQL & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
      SQL = SQL & " ORDER BY articuloInventario.TipoDeProducto"
   Else 'If optEstDeVentas(1).value Then
      SQL = SQL & " GROUP BY articuloInventario.Categoria, articuloInventario.TipoDeProducto,  "
      SQL = SQL & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
      SQL = SQL & " ORDER BY articuloInventario.Categoria, articuloInventario.TipoDeProducto, "
      SQL = SQL & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
   End If
h_EXIT: On Error GoTo 0
   fSQLComparativoPorAnoPorTipoDeProductoOCategoria = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLComparativoPorAnoPorTipoDeProductoOCategoria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(ByVal valUsarCambioABolivares As Boolean, _
                                                                           ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                            ByVal gUltimaTasaDeCambio As Object, _
                                                                             ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim vTabla As String
   On Error GoTo h_ERROR
   
   SQL = "(IGV_VendedorXRenglonFactura.PrecioSinIVA * IGV_VendedorXRenglonFactura.Cantidad)"
   SQL = "(" & SQL & " * (1 - (IGV_VendedorXRenglonFactura.PorcentajeDescuento / 100))) "
   SQL = "(" & SQL & " * (1 - (Factura.PorcentajeDescuento / 100))) "
   If valUsarCambioABolivares Then
      SQL = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", SQL, True, "")
   End If
   If Not valVieneDelLibroDeVentas Then
   SQL = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQL, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(ByVal valUsarCambioABolivares As Boolean, ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                            ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "((IGV_VendedorXRenglonFactura.PrecioSinIVA * IGV_VendedorXRenglonFactura.Cantidad) * (1 + (IGV_VendedorXRenglonFactura.PorcentajeAlicuota / 100)))"
   SQL = "(" & SQL & " * (1 - (IGV_VendedorXRenglonFactura.PorcentajeDescuento / 100))) "
   SQL = "(" & SQL & " * (1 - (Factura.PorcentajeDescuento / 100))) "
   If valUsarCambioABolivares Then
      SQL = "(" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", SQL, True, "") & ")"
   End If
   If Not valVieneDelLibroDeVentas Then
   SQL = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQL, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLArticulosSobreGiradosEnPedido(ByVal valConsecutivoCompania As Long, ByVal valNumeroFactura As String, ByVal valTipoDocumento As Integer) As String
   Dim vSQL As String
   Dim vSQLWhere  As String
   On Error GoTo h_ERROR
   vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "renglonFactura.NumeroFactura", valNumeroFactura, False)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "renglonFactura.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "renglonFactura.TipoDeDocumento", valTipoDocumento)
 
   vSQL = "SELECT renglonFactura.NumeroFactura, "
   vSQL = vSQL & "factura.NoCotizacionDeOrigen, "
   vSQL = vSQL & "renglonFactura.Articulo, "
   vSQL = vSQL & "renglonFactura.Cantidad, "
   vSQL = vSQL & "renglonCotizacion.Cantidad AS CantidadPedida, "
   vSQL = vSQL & "ISNULL(renglonCotizacion.CantidadDespachada, 0) AS CantidadDespachada, "
   vSQL = vSQL & "(renglonCotizacion.Cantidad - ISNULL(renglonCotizacion.CantidadDespachada, 0)) AS Resta  "
  
   vSQL = vSQL & " FROM renglonFactura INNER JOIN factura "
   vSQL = vSQL & "   ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania "
   vSQL = vSQL & "   AND renglonFactura.NumeroFactura = factura.Numero "
   vSQL = vSQL & "   AND renglonFactura.TipoDeDocumento = factura.TipoDeDocumento "
   vSQL = vSQL & "   INNER JOIN cotizacion "
   vSQL = vSQL & "   ON factura.ConsecutivoCompania = cotizacion.ConsecutivoCompania "
   vSQL = vSQL & "   AND factura.NoCotizacionDeOrigen = cotizacion.Numero "
   vSQL = vSQL & "   INNER JOIN renglonCotizacion "
   vSQL = vSQL & "   ON cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania "
   vSQL = vSQL & "   AND cotizacion.Numero = renglonCotizacion.NumeroCotizacion "
   vSQL = vSQL & "   AND renglonFactura.Articulo = renglonCotizacion.CodigoArticulo "
   vSQL = vSQL & "   AND ISNULL(renglonFactura.Serial, '') = ISNULL(renglonCotizacion.Serial, '') "
   vSQL = vSQL & "   AND ISNULL(renglonFactura.Rollo,'') = ISNULL(renglonCotizacion.Rollo,'') "
   
   
   vSQL = vSQL & gUtilSQL.getWhereSQL(vSQLWhere)
   vSQL = vSQL & "GROUP BY renglonFactura.NumeroFactura, factura.NoCotizacionDeOrigen, renglonFactura.Articulo, "
   vSQL = vSQL & "renglonFactura.Cantidad, renglonCotizacion.Cantidad, renglonCotizacion.CantidadDespachada "
   vSQL = vSQL & "HAVING ((renglonCotizacion.Cantidad - ISNULL(renglonCotizacion.CantidadDespachada, 0)) < renglonFactura.Cantidad) "
   fSQLArticulosSobreGiradosEnPedido = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLArticulosSobreGiradosEnPedido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeNotasDeCreditoEntreFechasPorVendedor(ByVal valReporteEnMonedaLocal As Boolean, _
                                           ByVal gMonedaLocalActual As Object, _
                                            ByVal gUltimaTasaDeCambio As Object, _
                                             ByVal valUsaModuloDeContabilidad As Boolean, _
                                              ByVal valConsecutivoCompania As String, _
                                               ByVal valFechaInicial As Date, _
                                                ByVal valFechaFinal As Date, _
                                                  ByVal valFiltroInforme As String) As String
                                                  
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim insFacturaView As clsFacturaVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insFacturaView = New clsFacturaVista
   
   SQL = SQL & "SELECT "
   SQL = SQL & "TablaTotal.Moneda, "
   SQL = SQL & "TablaTotal.Fecha, "
   SQL = SQL & "TablaTotal.ORDEN, "
   SQL = SQL & "TablaTotal.Numero, "
   SQL = SQL & "TablaTotal.TipoDeDocumento, "
   SQL = SQL & "TablaTotal.TipoDeDocumentoStr, "
   SQL = SQL & "TablaTotal.CodigoCliente, "
   SQL = SQL & "TablaTotal.Cliente, "
   SQL = SQL & "TablaTotal.Status, "
   SQL = SQL & "TablaTotal.MontoOriginal, "
   SQL = SQL & "TablaTotal.PorcentajeDescuento, "
   SQL = SQL & "TablaTotal.MontoDescuento, "
   SQL = SQL & "TablaTotal.MontoIVA, "
   SQL = SQL & "TablaTotal.BaseImponible, "
   SQL = SQL & "TablaTotal.MontoExento, "
   SQL = SQL & "TablaTotal.MontoTotal, "
   SQL = SQL & "TablaTotal.OtrosCargos, "
   SQL = SQL & "TablaTotal.NombreVendedor, "
   SQL = SQL & "TablaTotal.ComprobanteFiscal, "
   SQL = SQL & "TablaTotal.Comprobante "
   SQL = SQL & " FROM ("
   
   SQL = SQL & "SELECT "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
      SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda, "
   End If
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".ORDEN, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Numero, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".TipoDeDocumento, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".TipoDeDocumentoStr, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CodigoCliente, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Cliente, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoOriginal, "
   If valReporteEnMonedaLocal Then
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".PorcentajeDescuento", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS PorcentajeDescuento, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoDescuento", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoDescuento, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoIVA", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoIVA, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".BaseImponible", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS BaseImponible, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoExento", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoExento, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoTotal", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoTotal, "
   Else
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".PorcentajeDescuento", True) & " AS PorcentajeDescuento, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoDescuento", True) & " AS MontoDescuento, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoIVA", True) & " AS MontoIVA, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".BaseImponible", True) & " AS BaseImponible, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoExento", True) & " AS MontoExento, "
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoTotal", True) & " AS MontoTotal, "
   End If
   SQL = SQL & "0 AS OtrosCargos, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".NombreVendedor, "
   SQL = SQL & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".ComprobanteFiscal, "
   If valUsaModuloDeContabilidad Then
      SQL = SQL & gUtilSQL.getIIF(fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor(), valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor(), valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), False)
      SQL = SQL & " AS Comprobante "
   Else
      SQL = SQL & gUtilSQL.fSimpleSqlValue("No Aplica") & " AS Comprobante "
   End If
   SQL = SQL & " FROM " & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor()
   SQL = SQL & " WHERE "
   SQL = SQL & gUtilSQL.DfSQLDateValueBetween(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor & ".Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND " & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".ConsecutivoCompania = " & valConsecutivoCompania
   If valFiltroInforme <> "" Then
      SQL = SQL & " AND " & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".NombreVendedor = " & valFiltroInforme
   End If
   SQL = SQL & " UNION "
   SQL = SQL & "SELECT "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
      SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Moneda, "
   End If
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Fecha, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".ORDEN, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Numero, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".TipoDeDocumento, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".TipoDeDocumentoStr, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".CodigoCliente, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Cliente, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Status, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoOriginal, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".PorcentajeDescuento, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoDescuento, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoIVA, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".BaseImponible, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoExento, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoTotal, "
   If valReporteEnMonedaLocal Then
   SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".CambioABolivares", insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".OtrosCargos", _
                               insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Fecha"), True, "") & " AS OtrosCargos, "
   Else
   SQL = SQL & gUtilSQL.getIIF(insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".OtrosCargos", True) & " AS OtrosCargos, "
   End If
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".NombreVendedor, "
   SQL = SQL & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".ComprobanteFiscal, "
   If valUsaModuloDeContabilidad Then
      SQL = SQL & gUtilSQL.getIIF(fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), False)
      SQL = SQL & " AS Comprobante "
   Else
      SQL = SQL & gUtilSQL.fSimpleSqlValue("No Aplica") & " AS Comprobante "
   End If
   SQL = SQL & " FROM " & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito()
   SQL = SQL & " WHERE "
   SQL = SQL & gUtilSQL.DfSQLDateValueBetween(insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito & ".Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND " & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".ConsecutivoCompania = " & valConsecutivoCompania
   If valFiltroInforme <> "" Then
      SQL = SQL & "AND " & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".NombreVendedor = " & valFiltroInforme
   End If
   SQL = SQL & ") AS TablaTotal"
   SQL = SQL & " ORDER BY "
   SQL = SQL & "TablaTotal.Moneda,TablaTotal.NombreVendedor,TablaTotal.TipoDeDocumento,TablaTotal.Fecha,TablaTotal.Numero,TablaTotal.ORDEN"
   Set gEnumProyecto = Nothing
   Set insFacturaView = Nothing
   fSQLDeNotasDeCreditoEntreFechasPorVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeNotasDeCreditoEntreFechasPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(ByVal valConsecutivoCompania As String, _
                                          ByVal valVista As String, _
                                           ByVal valFechaInicial As Date, _
                                            ByVal valFechaFinal As Date) As String
   Dim SQL As String
   Dim insFacturaView As clsFacturaVista
   Dim SqlPeriodo As String
   Set insFacturaView = New clsFacturaVista
   
   SQL = SQL & "SELECT Periodo.ConsecutivoPeriodo"
   SQL = SQL & " FROM Periodo "
   SQL = SQL & " WHERE Periodo.ConsecutivoCompania = " & valConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(SQL, False)

   SQL = "(SELECT Comprobante.Numero "
   SQL = SQL & " FROM Comprobante "
   SQL = SQL & " WHERE Comprobante.NoDocumentoOrigen = " & valVista & ".TipoDeDocumento "
   SQL = SQL & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & valVista & ".Numero "
   SQL = SQL & " AND Comprobante.GeneradoPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_FACTURA))
   SQL = SQL & " AND Comprobante.ConsecutivoPeriodo IN (" & SqlPeriodo & ")"
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(valVista & ".Fecha", valFechaInicial, valFechaFinal) & ")"
   
   Set insFacturaView = Nothing
   fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlFacturacionEntreFechaConCostoUltimaCompra(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String) As String
Dim vSQL As String
Dim vCondicion As String
Dim vPrecioVenta As String
Dim vSQLWhere As String
On Error GoTo h_ERROR
   vSQLWhere = ""
   If (valcodigoCliente <> "") Then
      vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente", valcodigoCliente, False)
   End If
  
   vSQLWhere = gUtilSQL.fSQLValueDatesBetween(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.Fecha", valFechaInicial, valFechaFinal)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vCondicion = " dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento = " & gUtilSQL.fNumToStrSQL(0)
   vPrecioVenta = gUtilSQL.getIIF(vCondicion, "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual", "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual - (dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual * (dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento/100))", True)
   vSQL = fSqlSelectFacturacionEntreFecha()
   vSQL = vSQL & vPrecioVenta & " AS PrecioVenta,"
   vSQL = vSQL & "dbo.Gf_CompraUnaFecha(dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania, dbo.IGV_VentasConDescripcionArticulos_B1.Fecha,dbo.IGV_VentasConDescripcionArticulos_B1.Codigo)  AS PrecioCosto,"
   vSQL = vSQL & vPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad  AS VentaTotal,"
   vSQL = vSQL & "dbo.Gf_CompraUnaFecha(dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania, dbo.IGV_VentasConDescripcionArticulos_B1.Fecha,dbo.IGV_VentasConDescripcionArticulos_B1.Codigo)  * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad   AS CostoTotal,"
   vSQL = vSQL & "(" & vPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad ) " & "-" & "(dbo.Gf_CompraUnaFecha(dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania, dbo.IGV_VentasConDescripcionArticulos_B1.Fecha,dbo.IGV_VentasConDescripcionArticulos_B1.Codigo)  * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad) AS Diferencia,"
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda "
   vSQL = vSQL & " FROM dbo.IGV_VentasConDescripcionArticulos_B1"
   vSQL = vSQL & " JOIN ArticuloInventario on ArticuloInventario.Codigo = dbo.IGV_VentasConDescripcionArticulos_B1.Codigo AND dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Order by "
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero"
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoUltimaCompra = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoArticulosCompuestos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlFacturacionEntreFechaConCostoPromedio(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String) As String
Dim vSQL As String
Dim vCondicion As String
Dim vPrecioVenta As String
Dim vSQLWhere As String
On Error GoTo h_ERROR
  vSQLWhere = ""
   If (valcodigoCliente <> "") Then
      vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente", valcodigoCliente, False)
   End If
   
   vSQLWhere = gUtilSQL.fSQLValueDatesBetween(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.Fecha", valFechaInicial, valFechaFinal)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vCondicion = " dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento = " & gUtilSQL.fNumToStrSQL(0)
   vPrecioVenta = gUtilSQL.getIIF(vCondicion, "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual", "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual - (dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual * (dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento/100))", True)
   vSQL = fSqlSelectFacturacionEntreFecha()
   vSQL = vSQL & vPrecioVenta & " AS PrecioVenta,"
   vSQL = vSQL & "(Gf_CostoPromedioPonderado_1.CostoFinal)  AS PrecioCosto,"
   vSQL = vSQL & vPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad  AS VentaTotal,"
   vSQL = vSQL & "(Gf_CostoPromedioPonderado_1.CostoFinal * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad)  AS CostoTotal,"
   vSQL = vSQL & "(" & vPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad ) " & "-" & "(Gf_CostoPromedioPonderado_1.CostoFinal * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad) AS Diferencia,"
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda "
   vSQL = vSQL & " FROM dbo.IGV_VentasConDescripcionArticulos_B1 "
   vSQL = vSQL & " INNER JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS Gf_CostoPromedioPonderado_1 ON "
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Codigo = Gf_CostoPromedioPonderado_1.Codigo AND "
   vSQL = vSQL & "  Gf_CostoPromedioPonderado_1.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("factura") & " AND "
   vSQL = vSQL & "  dbo.IGV_VentasConDescripcionArticulos_B1.numero = Gf_CostoPromedioPonderado_1.numerodocumento "
   vSQL = vSQL & " JOIN ArticuloInventario on ArticuloInventario.Codigo = dbo.IGV_VentasConDescripcionArticulos_B1.Codigo AND dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & " Group By "
   vSQL = vSQL & fGroupbyCostoDeFacturaEntreFecha() & "  "
   vSQL = vSQL & " Order by "
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero"
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoPromedio = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlFacturacionEntreFechaConCostoPromedio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlFacturacionEntreFechaConCostoUltimaCompraSegunLey(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String, ByVal valMaxGanancia As Currency) As String
Dim vSQL As String
Dim vSQLPrecioVenta As String
Dim vSQLWhere As String
Dim vSQLGastoAdmisible As String
Dim vSQLCostoUnitario As String
Dim vSQLVentaTotal As String
Dim vSQLCostoTotal As String
Dim vSQLGanancia As String
Dim vSQLCostoSegunLey As String
Dim vSQLMayorAMaxGanancia As String
On Error GoTo h_ERROR
   vSQLWhere = ""
   
   If (valcodigoCliente <> "") Then
      vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente", valcodigoCliente, False)
   End If
  
   vSQLWhere = gUtilSQL.fSQLValueDatesBetween(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.Fecha", valFechaInicial, valFechaFinal)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQLPrecioVenta = gUtilSQL.getIIF(" dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento = " & gUtilSQL.fNumToStrSQL(0), "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual", "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual - (dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual * (dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento/100))", True)
   vSQLVentaTotal = vSQLPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLCostoUnitario = " dbo.Gf_CompraUnaFecha(dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania, dbo.IGV_VentasConDescripcionArticulos_B1.Fecha, dbo.IGV_VentasConDescripcionArticulos_B1.Codigo) "
   vSQLCostoUnitario = gUtilSQL.getIIF(vSQLCostoUnitario & " IS NULL ", "0", vSQLCostoUnitario, True)
   vSQLCostoTotal = vSQLCostoUnitario & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLGastoAdmisible = vSQLCostoTotal & " * ArticuloInventario.GastosAdmisibles / 100"
   vSQLCostoSegunLey = vSQLCostoTotal & " + " & vSQLGastoAdmisible
   vSQLGanancia = gUtilSQL.getIIF(vSQLCostoSegunLey & " = 0", "0", "((" & vSQLVentaTotal & ") - (" & vSQLCostoSegunLey & ")) / (" & vSQLCostoSegunLey & ") * 100", True)
   vSQLMayorAMaxGanancia = gUtilSQL.getIIF("(" & vSQLGanancia & ") > " & gUtilSQL.fNumToStrSQL(valMaxGanancia), gUtilSQL.fSimpleSqlValue("*"), gUtilSQL.fSimpleSqlValue(""), True)
   
   vSQL = fSqlSelectFacturacionEntreFecha()
   vSQL = vSQL & vSQLPrecioVenta & " AS PrecioVenta,"
   vSQL = vSQL & vSQLVentaTotal & " AS VentaTotal,"
   vSQL = vSQL & vSQLCostoUnitario & " AS PrecioCosto,"
   vSQL = vSQL & vSQLCostoTotal & " AS CostoTotal,"
   vSQL = vSQL & " ArticuloInventario.GastosAdmisibles AS PorcentajeGastoAdmisible,"
   vSQL = vSQL & vSQLGastoAdmisible & " AS GastoAdmisible,"
   vSQL = vSQL & vSQLCostoSegunLey & " AS CostoSegunLey, "
   vSQL = vSQL & vSQLGanancia & " AS PorcentajeGanancia, "
   vSQL = vSQL & vSQLMayorAMaxGanancia & " AS MayorAMaxGanancia,"
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda "
   
   vSQL = vSQL & " FROM dbo.IGV_VentasConDescripcionArticulos_B1"
    
   vSQL = vSQL & " JOIN ArticuloInventario on ArticuloInventario.Codigo = dbo.IGV_VentasConDescripcionArticulos_B1.Codigo AND dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   
   vSQL = vSQL & vSQLWhere
   
   vSQL = vSQL & " Order by "
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero"
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoUltimaCompraSegunLey = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoArticulosCompuestos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlFacturacionEntreFechaConCostoPromedioSegunLey(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String, ByVal valMaxGanancia As Currency) As String
Dim vSQL As String
Dim vSQLPrecioVenta As String
Dim vSQLWhere As String
Dim vSQLGastoAdmisible As String
Dim vSQLCostoUnitario As String
Dim vSQLVentaTotal As String
Dim vSQLCostoTotal As String
Dim vSQLGanancia As String
Dim vSQLCostoSegunLey As String
Dim vSQLMayorAMaxGanancia As String
On Error GoTo h_ERROR
   vSQLWhere = ""
   
   If (valcodigoCliente <> "") Then
      vSQLWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente", valcodigoCliente, False)
   End If
   
   vSQLWhere = gUtilSQL.fSQLValueDatesBetween(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.Fecha", valFechaInicial, valFechaFinal)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
   
   vSQLPrecioVenta = gUtilSQL.getIIF(" dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento = " & gUtilSQL.fNumToStrSQL(0), "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual", "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual - (dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual * (dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento/100))", True)
   vSQLVentaTotal = vSQLPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLCostoUnitario = " Gf_CostoPromedioPonderado_1.CostoFinal "
   vSQLCostoUnitario = gUtilSQL.getIIF(vSQLCostoUnitario & " IS NULL ", "0", vSQLCostoUnitario, True)
   vSQLCostoTotal = vSQLCostoUnitario & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLGastoAdmisible = vSQLCostoTotal & " * ArticuloInventario.GastosAdmisibles / 100"
   vSQLCostoSegunLey = vSQLCostoTotal & " + " & vSQLGastoAdmisible
   vSQLGanancia = gUtilSQL.getIIF(vSQLCostoSegunLey & " = 0", "0", "((" & vSQLVentaTotal & ") - (" & vSQLCostoSegunLey & ")) / (" & vSQLCostoSegunLey & ") * 100", True)
   vSQLMayorAMaxGanancia = gUtilSQL.getIIF("(" & vSQLGanancia & ") > " & gUtilSQL.fNumToStrSQL(valMaxGanancia), gUtilSQL.fSimpleSqlValue("*"), gUtilSQL.fSimpleSqlValue(""), True)
 
   vSQL = fSqlSelectFacturacionEntreFecha()
   vSQL = vSQL & vSQLPrecioVenta & " AS PrecioVenta,"
   vSQL = vSQL & vSQLVentaTotal & " AS VentaTotal,"
   vSQL = vSQL & vSQLCostoUnitario & " AS PrecioCosto,"
   vSQL = vSQL & vSQLCostoTotal & " AS CostoTotal,"
   vSQL = vSQL & " ArticuloInventario.GastosAdmisibles AS PorcentajeGastoAdmisible,"
   vSQL = vSQL & vSQLGastoAdmisible & " AS GastoAdmisible,"
   vSQL = vSQL & vSQLCostoSegunLey & " AS CostoSegunLey, "
   vSQL = vSQL & vSQLGanancia & " AS PorcentajeGanancia, "
   vSQL = vSQL & vSQLMayorAMaxGanancia & " AS MayorAMaxGanancia,"
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda "
   
   vSQL = vSQL & " FROM dbo.IGV_VentasConDescripcionArticulos_B1 "
   
   vSQL = vSQL & " INNER JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS Gf_CostoPromedioPonderado_1 ON "
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Codigo = Gf_CostoPromedioPonderado_1.Codigo AND "
   vSQL = vSQL & " Gf_CostoPromedioPonderado_1.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("factura") & " AND "
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.numero = Gf_CostoPromedioPonderado_1.numerodocumento "
   
   vSQL = vSQL & " JOIN ArticuloInventario on ArticuloInventario.Codigo = dbo.IGV_VentasConDescripcionArticulos_B1.Codigo AND dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   
   vSQL = vSQL & vSQLWhere
   
   vSQL = vSQL & " Group By "
   vSQL = vSQL & fGroupbyCostoDeFacturaEntreFecha() & " , ArticuloInventario.GastosAdmisibles"
   
   vSQL = vSQL & " Order by "
   vSQL = vSQL & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero"
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoPromedioSegunLey = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlFacturacionEntreFechaConCostoPromedio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlSelectFacturacionEntreFecha() As String
Dim vResult As String
On Error GoTo h_ERROR
 vResult = " SELECT "
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Fecha,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente+' '+ dbo.IGV_VentasConDescripcionArticulos_B1.Nombre AS Cliente,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.CodigoVendedor+' '+ dbo.IGV_VentasConDescripcionArticulos_B1.NombreVendedor AS Vendedor,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Articulo,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Serial,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Rollo,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Descripcion,"
 vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad,"
h_EXIT: On Error GoTo 0
  fSqlSelectFacturacionEntreFecha = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlSelectFacturacionEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 
Public Function fSQLServiciosXVehiculos(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valCategoria As String, ByVal valPlaca As String, ByVal valSerialVIN As String) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT Vehiculo.Consecutivo, Vehiculo.Placa, Vehiculo.serialVIN, SAW.Modelo.Nombre, SAW.Modelo.Marca, Color.DescripcionColor, Vehiculo.Ano, Vehiculo.NumeroPoliza,"
   vSQL = vSQL & "  Cliente.Nombre AS NombreCliente, factura.Numero, factura.Fecha, camposDefFactura.CampoDefinible1 AS Kilometraje, ArticuloInventario.Descripcion,"
   vSQL = vSQL & "  renglonFactura.Cantidad,"
   vSQL = vSQL & "  renglonFactura.Cantidad * (renglonFactura.PrecioSinIVA - (renglonFactura.PrecioSinIVA * renglonFactura.PorcentajeDescuento / 100 + renglonFactura.PrecioSinIVA * factura.PorcentajeDescuento"
   vSQL = vSQL & "/ 100)) AS SuBTotal, renglonFactura.Descripcion AS Descripcionrenglon, ArticuloInventario.Categoria"
   vSQL = vSQL & " FROM factura INNER JOIN"
   vSQL = vSQL & "  renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSQL = vSQL & "  factura.TipoDeDocumento = renglonFactura.TipoDeDocumento INNER JOIN"
   vSQL = vSQL & "  Vehiculo ON factura.ConsecutivoCompania = Vehiculo.ConsecutivoCompania AND factura.ConsecutivoVehiculo = Vehiculo.Consecutivo INNER JOIN"
   vSQL = vSQL & "  SAW.Modelo ON Vehiculo.NombreModelo = SAW.Modelo.Nombre INNER JOIN"
   vSQL = vSQL & "  Color ON Vehiculo.ConsecutivoCompania = Color.ConsecutivoCompania AND Vehiculo.CodigoColor = Color.CodigoColor INNER JOIN"
   vSQL = vSQL & "  Cliente ON Vehiculo.ConsecutivoCompania = Cliente.ConsecutivoCompania AND Vehiculo.CodigoCliente = Cliente.Codigo INNER JOIN"
   vSQL = vSQL & "  camposDefFactura ON factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania AND factura.Numero = camposDefFactura.NumeroFactura AND"
   vSQL = vSQL & "  factura.TipoDeDocumento = camposDefFactura.TipoDeDocumento INNER JOIN"
   vSQL = vSQL & "  ArticuloInventario ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND renglonFactura.Articulo = ArticuloInventario.Codigo"
   vSQL = vSQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valCategoria) > 0 Then
      vSQL = vSQL & "  AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   End If
   vSQL = vSQL & "  AND factura.TipoDeDocumento <> (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & ")"
   vSQL = vSQL & "  AND factura.TipoDeDocumento <> (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ")"
   vSQL = vSQL & "  AND factura.StatusFactura = (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valPlaca) > 0 Then
      vSQL = vSQL & "  AND (Vehiculo.Placa = " & gUtilSQL.fSimpleSqlValue(valPlaca) & ")"
   End If
   If gTexto.DfLen(valSerialVIN) > 0 Then
         vSQL = gUtilSQL.fSQLValueWithWildcardWithAnd(vSQL, "Vehiculo.serialVIN", valSerialVIN)
   End If
   vSQL = vSQL & " ORDER BY Vehiculo.Placa, factura.Fecha"
h_EXIT: On Error GoTo 0
   fSQLServiciosXVehiculos = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLServiciosXVehiculos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLServiciosDeVehiculosXCategoria(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valCategoria As String) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT Vehiculo.Consecutivo, Vehiculo.Placa, Vehiculo.serialVIN, SAW.Modelo.Nombre, SAW.Modelo.Marca, Color.DescripcionColor, Vehiculo.Ano,"
   vSQL = vSQL & " Vehiculo.NumeroPoliza, Cliente.Nombre AS NombreCliente, Cliente.Telefono, Cliente.Email, factura.Numero, MAX(factura.Fecha) AS FechaUltimoServicio,"
   vSQL = vSQL & " camposDefFactura.CampoDefinible1 AS Kilometraje, ArticuloInventario.Descripcion, renglonFactura.Descripcion AS Descripcionrenglon, ArticuloInventario.Categoria"
   vSQL = vSQL & " FROM factura INNER JOIN"
   vSQL = vSQL & " renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSQL = vSQL & " factura.TipoDeDocumento = renglonFactura.TipoDeDocumento INNER JOIN"
   vSQL = vSQL & " Vehiculo ON factura.ConsecutivoCompania = Vehiculo.ConsecutivoCompania AND factura.ConsecutivoVehiculo = Vehiculo.Consecutivo INNER JOIN"
   vSQL = vSQL & " SAW.Modelo ON Vehiculo.NombreModelo = SAW.Modelo.Nombre INNER JOIN"
   vSQL = vSQL & " Color ON Vehiculo.ConsecutivoCompania = Color.ConsecutivoCompania AND Vehiculo.CodigoColor = Color.CodigoColor INNER JOIN"
   vSQL = vSQL & " Cliente ON Vehiculo.ConsecutivoCompania = Cliente.ConsecutivoCompania AND Vehiculo.CodigoCliente = Cliente.Codigo INNER JOIN"
   vSQL = vSQL & " camposDefFactura ON factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania AND factura.Numero = camposDefFactura.NumeroFactura AND"
   vSQL = vSQL & " factura.TipoDeDocumento = camposDefFactura.TipoDeDocumento INNER JOIN"
   vSQL = vSQL & " ArticuloInventario ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND renglonFactura.Articulo = ArticuloInventario.Codigo"
   vSQL = vSQL & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valCategoria) <> 0 Then
      vSQL = vSQL & "  AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   End If
   vSQL = vSQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   vSQL = vSQL & " GROUP BY Vehiculo.Consecutivo, Vehiculo.Placa, Vehiculo.serialVIN, SAW.Modelo.Nombre, SAW.Modelo.Marca, Color.DescripcionColor, Vehiculo.Ano,"
   vSQL = vSQL & " Vehiculo.NumeroPoliza, Cliente.Nombre, Cliente.Telefono, Cliente.Email, factura.Numero, camposDefFactura.CampoDefinible1, ArticuloInventario.Descripcion,"
   vSQL = vSQL & " renglonFactura.Descripcion, ArticuloInventario.Categoria"
h_EXIT: On Error GoTo 0
   fSQLServiciosDeVehiculosXCategoria = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLServiciosDeVehiculosXCategoria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLHorasDeServicioXVendedor(ByVal valVendedor As Enum_Vendedores_Por_Renglon) As String
   Dim vSQL As String
   Dim vCodigoDeVendedor As String
   On Error GoTo h_ERROR
   Select Case valVendedor
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor1
         vCodigoDeVendedor = "CodigoVendedor1"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor2
         vCodigoDeVendedor = "CodigoVendedor2"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor3
         vCodigoDeVendedor = "CodigoVendedor3"
   End Select
   vSQL = "SELECT Vendedor.Codigo, Vendedor.Nombre, factura.Numero, factura.Fecha, renglonFactura.Descripcion, renglonFactura.Cantidad, CASE WHEN ISNUMERIC(CampoDefinible1)"
   vSQL = vSQL & " = 1 THEN CAST(CampoDefinible1 AS Money) ELSE 0 END AS Factor, CASE WHEN ISNUMERIC(CampoDefinible1) = 1 THEN CAST(CampoDefinible1 AS Money)"
   vSQL = vSQL & " * Cantidad ELSE 0 END AS FactorXCantidad"
   vSQL = vSQL & " FROM factura INNER JOIN"
   vSQL = vSQL & " renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSQL = vSQL & " factura.TipoDeDocumento = renglonFactura.TipoDeDocumento AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & " INNER JOIN"
   vSQL = vSQL & " ArticuloInventario ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND"
   vSQL = vSQL & " renglonFactura.Articulo = ArticuloInventario.Codigo INNER JOIN"
   vSQL = vSQL & " Vendedor ON renglonFactura." & vCodigoDeVendedor & " = Vendedor.Codigo AND renglonFactura.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   
   'vSQL = vSQL & " WHERE factura.StatusFactura = '0' AND factura.TipoDeDocumento <> '5'  and factura.ConsecutivoCompania = 1"
h_EXIT: On Error GoTo 0
   fSQLHorasDeServicioXVendedor = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHorasDeServicioXVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLHorasDeServicioXVendedorCompleto(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valcodigoVendedor As String) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
   vSQLWhere = " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSQLWhere = vSQLWhere & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valcodigoVendedor) > 0 Then
      vSQLWhere = vSQLWhere & "  AND Vendedor.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
   End If
   vSQLWhere = vSQLWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSQLWhere = vSQLWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSQLWhere = vSQLWhere & "  AND factura.StatusFactura = (" & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   
   vSQL = fSQLHorasDeServicioXVendedor(eVPR_Vendedor1) & vSQLWhere
   vSQL = vSQL & " UNION " & fSQLHorasDeServicioXVendedor(eVPR_Vendedor2) & vSQLWhere
   vSQL = vSQL & " UNION " & fSQLHorasDeServicioXVendedor(eVPR_Vendedor3) & vSQLWhere
   vSQL = vSQL & " ORDER BY " & "Vendedor.Codigo, factura.Fecha, factura.Numero"
h_EXIT: On Error GoTo 0
   fSQLHorasDeServicioXVendedorCompleto = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHorasDeServicioXVendedorCompleto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLComisionVendedoresXArticulo(ByVal valVendedor As Enum_Vendedores_Por_Renglon) As String
   Dim vSQL As String
   Dim vSQLSubTotal As String
   Dim vCodigoDeVendedor As String
   On Error GoTo h_ERROR
   Select Case valVendedor
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor1
         vCodigoDeVendedor = "CodigoVendedor1"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor2
         vCodigoDeVendedor = "CodigoVendedor2"
      Case Enum_Vendedores_Por_Renglon.eVPR_Vendedor3
         vCodigoDeVendedor = "CodigoVendedor3"
   End Select
   vSQLSubTotal = "renglonFactura.Cantidad * renglonFactura.PrecioSinIVA"
   vSQL = " SELECT Vendedor.Codigo, Vendedor.Nombre, factura.Fecha, factura.Numero, renglonFactura.Descripcion, renglonFactura.Cantidad, renglonFactura.PrecioSinIVA, " & vSQLSubTotal & " As SubTotal, "
   vSQL = vSQL & vSQLSubTotal & " * ( renglonFactura.PorcentajeDescuento/100)+ ( " & vSQLSubTotal & " * ( factura.PorcentajeDescuento/100)) as Descuento, "
   vSQL = vSQL & vSQLSubTotal & " - ( " & vSQLSubTotal & "  * ( renglonFactura.PorcentajeDescuento/100)+ ( renglonFactura.Cantidad * renglonFactura.PrecioSinIVA*( factura.PorcentajeDescuento/100))) AS SubTotalConDescuento, "
   vSQL = vSQL & gUtilSQL.getIIF("Articuloinventario.ComisionaPorcentaje = " & gUtilSQL.fBooleanToSqlValue(True), "(" & vSQLSubTotal & " - ( " & vSQLSubTotal & " * ( renglonFactura.PorcentajeDescuento/100)+ ( " & vSQLSubTotal & " *( factura.PorcentajeDescuento/100)))) * (ArticuloInventario.PorcentajeComision/100)", "ArticuloInventario.PorcentajeComision * renglonFactura.Cantidad ", True) & " AS Comision "
   vSQL = vSQL & " FROM factura INNER JOIN"
   vSQL = vSQL & " renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSQL = vSQL & " factura.TipoDeDocumento = renglonFactura.TipoDeDocumento AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & " INNER JOIN"
   vSQL = vSQL & " Vendedor ON renglonFactura.ConsecutivoCompania = Vendedor.ConsecutivoCompania AND renglonFactura." & vCodigoDeVendedor & " = Vendedor.Codigo INNER JOIN"
   vSQL = vSQL & " ArticuloInventario ON renglonFactura.Articulo = ArticuloInventario.Codigo AND renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
h_EXIT: On Error GoTo 0
   fSQLComisionVendedoresXArticulo = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComisionVendedoresXArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComisionVendedoresXArticuloCompleto(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valcodigoVendedor As String) As String
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
   vSQLWhere = " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSQLWhere = vSQLWhere & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valcodigoVendedor) > 0 Then
      vSQLWhere = vSQLWhere & "  AND Vendedor.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
   End If
   vSQLWhere = vSQLWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSQLWhere = vSQLWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSQLWhere = vSQLWhere & "  AND factura.StatusFactura = (" & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   vSQLWhere = vSQLWhere & "  AND Articuloinventario.ExcluirDeComision = " & gUtilSQL.fBooleanToSqlValue(False)
   
   vSQL = fSQLComisionVendedoresXArticulo(eVPR_Vendedor1) & vSQLWhere
   vSQL = vSQL & " UNION " & fSQLComisionVendedoresXArticulo(eVPR_Vendedor2) & vSQLWhere
   vSQL = vSQL & " UNION " & fSQLComisionVendedoresXArticulo(eVPR_Vendedor3) & vSQLWhere
   vSQL = vSQL & " ORDER BY " & "Vendedor.Codigo, factura.Fecha, factura.Numero"
h_EXIT: On Error GoTo 0
   fSQLComisionVendedoresXArticuloCompleto = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComisionVendedoresXArticuloCompleto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLListadoDeVehiculosXPropietario(ByVal valConsecutivoCompania As String, _
                                          ByVal valPlaca As String, ByVal valcodigoCliente As String)
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT Vehiculo.Placa, Vehiculo.serialVIN, Vehiculo.Ano, Color.DescripcionColor, SAW.Modelo.Nombre as NombreModelo, SAW.Modelo.Marca, Cliente.Codigo, Cliente.Nombre AS NombreCliente,"
   vSQL = vSQL & " Cliente.NumeroRIF , Cliente.Direccion, Cliente.Telefono, Vehiculo.NumeroPoliza, Vehiculo.SerialMotor"
   vSQL = vSQL & " FROM         Vehiculo INNER JOIN"
   vSQL = vSQL & " Cliente ON Vehiculo.ConsecutivoCompania = Cliente.ConsecutivoCompania AND Vehiculo.CodigoCliente = Cliente.Codigo INNER JOIN"
   vSQL = vSQL & " SAW.Modelo ON Vehiculo.NombreModelo = SAW.Modelo.Nombre INNER JOIN"
   vSQL = vSQL & " Color ON Vehiculo.ConsecutivoCompania = Color.ConsecutivoCompania AND Vehiculo.CodigoColor = Color.CodigoColor"
   vSQL = vSQL & " WHERE Vehiculo.ConsecutivoCompania = " & valConsecutivoCompania
   If gTexto.DfLen(valPlaca) Then
      vSQL = vSQL & " AND Vehiculo.Placa = " & gUtilSQL.fSimpleSqlValue(valPlaca)
   End If
   If gTexto.DfLen(valcodigoCliente) Then
      vSQL = vSQL & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
h_EXIT: On Error GoTo 0
   fSQLListadoDeVehiculosXPropietario = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLListadoDeVehiculosXPropietario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasEntreFechasPorCategoria(ByVal usarCambioOriginal As Boolean, _
                                                                ByVal valReporteEnMonedaLocal As Boolean, _
                                                                 ByVal gUltimaTasaDeCambio As Object, _
                                                                  ByVal gMonedaLocalActual As Object, _
                                                                     ByVal valConsecutivoCompania As String, _
                                                                      ByVal valFechaInicial As Date, _
                                                                       ByVal valFechaFinal As Date, _
                                                                        ByVal valUsaPrecioSinIva As Boolean, _
                                                                         ByVal valInformeResumido As Boolean, _
                                                                           ByVal valcodigoCliente As String, _
                                                                             ByVal valCantidadCategoria, _
                                                                               ByVal valCategoria, _
                                                                                ByVal valFiltrarPorVendedor, _
                                                                                 ByVal valcodigoVendedor, _
                                                                                   Optional ByVal valMuestraMontoIva As Boolean) As String
   Dim SQL As String
   Dim sqlPrecioSinIva As String
   Dim sqlPrecioConIva As String
   Dim SqlTotalRenglon As String
   Dim SqlTotalCantidad As String
   Dim SqlTotalMonto As String
   Dim SqlInnerJoin As String
   Dim SqlStatusFactura As String
   Dim sqlRenglon As String
   Dim sqlMontoRenglon As String
   Dim sqlDescMonto As String
   Dim sqlFactMonto As String
   Dim SqlRenglonTotal As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vPrecio As String
   Dim vPrecioParaMontoIva As String
   Dim sqlRenglonParaMontoIva As String
   Dim sqlMontoRenglonParaMontoIva As String
   Dim sqlDescMontoParaMontoIva As String
   Dim sqlFactMontoParaMontoIva As String
   Dim SqlRenglonTotalParaMontoIva As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SqlStatusFactura = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), _
                                       gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                                       gUtilSQL.fSimpleSqlValue("Vigente"), True)
   If valUsaPrecioSinIva Then
      vPrecio = "RenglonFactura.PrecioSinIva"
   Else
      vPrecio = "RenglonFactura.PrecioConIva"
   End If
   sqlRenglon = "(" & vPrecio & " * RenglonFactura.Cantidad)"
   sqlMontoRenglon = "(" & sqlRenglon & " * (1 - (RenglonFactura.PorcentajeDescuento / 100)))"
   sqlDescMonto = "((" & sqlRenglon & ") - (" & sqlMontoRenglon & "))"
   sqlFactMonto = "((( " & sqlRenglon & " * (1 - (RenglonFactura.PorcentajeDescuento / 100))) * (factura.MontoDescuento1*1000 / factura.TotalRenglones*1000)/1000000))"
   SqlRenglonTotal = "(" & sqlRenglon & " - " & sqlDescMonto & " - " & sqlFactMonto & ")"
   If valMuestraMontoIva Then
      vPrecioParaMontoIva = "RenglonFactura.PrecioConIva"
      sqlRenglonParaMontoIva = "(" & vPrecioParaMontoIva & " * RenglonFactura.Cantidad)"
      sqlMontoRenglonParaMontoIva = "(" & sqlRenglonParaMontoIva & " * (1 - (RenglonFactura.PorcentajeDescuento / 100)))"
      sqlDescMontoParaMontoIva = "((" & sqlRenglonParaMontoIva & ") - (" & sqlMontoRenglonParaMontoIva & "))"
      sqlFactMontoParaMontoIva = "((( " & sqlRenglonParaMontoIva & " * (1 - (RenglonFactura.PorcentajeDescuento / 100))) * (factura.MontoDescuento1*1000 / factura.TotalRenglones*1000)/1000000))"
      SqlRenglonTotalParaMontoIva = "(" & sqlRenglonParaMontoIva & " - " & sqlDescMontoParaMontoIva & " - " & sqlFactMontoParaMontoIva & ")"
   End If
   If valReporteEnMonedaLocal Then
      sqlPrecioConIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioConIva", "Factura.Fecha"), _
            usarCambioOriginal, ""), True)
      sqlPrecioSinIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioSinIva", "Factura.Fecha"), _
            usarCambioOriginal, ""), True)
      SqlTotalRenglon = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SqlRenglonTotal, "Factura.Fecha"), _
            usarCambioOriginal, ""), True)
   Else
      sqlPrecioConIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                        "RenglonFactura.PrecioConIva", True)
      sqlPrecioSinIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                        "RenglonFactura.PrecioSinIva", True)
      SqlTotalRenglon = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                        SqlRenglonTotal, True)
   End If
   SQL = "SELECT "
   SQL = SQL & "Factura.Numero, "
   SQL = SQL & "Factura.Moneda, "
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   If valInformeResumido Then
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "SUM(RenglonFactura.Cantidad)", True) & " AS CantidadDeArticulo, "
      SQL = SQL & "SUM(" & SqlTotalRenglon & ") AS TotalRenglon "
      If valMuestraMontoIva Then
         SQL = SQL & "," & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
            "SUM(" & SqlRenglonTotalParaMontoIva & ")", True) & " AS TotalRenglonConIva, "
         SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
            "SUM(" & SqlRenglonTotalParaMontoIva & "-" & SqlTotalRenglon & ")", True) & " AS MontoIva "
      End If
   Else
      SQL = SQL & "RenglonFactura.Articulo AS CodigoArticulo, "
      SQL = SQL & "RenglonFactura.Descripcion AS DescripcionArticulo, "
      SQL = SQL & "RenglonFactura.PorcentajeDescuento AS PorcDectoRenglon, "
      SQL = SQL & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria, "
      SQL = SQL & "Factura.PorcentajeDescuento AS PorcDectoFactura, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "RenglonFactura.Cantidad", True) & " AS CantidadDeArticulo, "
      SQL = SQL & sqlPrecioConIva & " AS PrecioConIva, "
      SQL = SQL & sqlPrecioSinIva & " AS PrecioSinIva, "
      
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlPrecioConIva & "-" & sqlPrecioSinIva, True) & " AS MontoIva, "
      SQL = SQL & SqlTotalRenglon & " AS TotalRenglon, "
      
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonConIva, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual) & " - " & SqlTotalRenglon, True) & " AS TotalRenglonIva, "
     
      SQL = SQL & SqlStatusFactura & " AS StatusFactura, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlDescMonto, True) & " AS MontoDescRenglon, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlFactMonto, True) & " AS MontoDescFactura "
   End If
   SQL = SQL & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   
   
   SQL = SQL & " INNER JOIN ((Cliente INNER JOIN Factura"
   SQL = SQL & " ON  (Cliente.Codigo = Factura.CodigoCliente)"
   SQL = SQL & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
   SQL = SQL & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   
   
   
   
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   SQL = SQL & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      SQL = SQL & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   If valCantidadCategoria <> "Todos" Then
       SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   End If
   If valFiltrarPorVendedor Then
       SQL = SQL & " AND Factura.CodigoVendedor = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
   End If
   If valInformeResumido Then
      SQL = SQL & " GROUP BY  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria,Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.StatusFactura "
   End If
   SQL = SQL & " ORDER BY Factura.StatusFactura, Factura.Moneda, Factura.Fecha, Factura.Numero"
   fSQLDeFacturasEntreFechasPorCategoria = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorCategoria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLDeFacturasEntreFechasPorLinea(ByVal valConsecutivoCompania As String, _
                                                                      ByVal valFechaInicial As Date, _
                                                                       ByVal valFechaFinal As Date, _
                                                                        ByVal valCantidadLinea, _
                                                                         ByVal valLinea)

   Dim SQL As String
   On Error GoTo h_ERROR
   Dim sqlMontoExentoRenglon As String
   Dim sqlTotalRenglonConDscto As String
   Dim sqlIvaRenglon As String
   sqlMontoExentoRenglon = gUtilSQL.getIIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.AlicuotaIva = '0'", "RenglonFactura.TotalRenglon * (1 - factura.PorcentajeDescuento / 100)", "0")
   sqlTotalRenglonConDscto = "RenglonFactura.TotalRenglon * (1 - factura.PorcentajeDescuento / 100)"
   sqlIvaRenglon = sqlTotalRenglonConDscto & " * RenglonFactura.PorcentajeAlicuota / 100"
   SQL = "SELECT  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto,"
   SQL = SQL & "SUM(" & sqlMontoExentoRenglon & " ) AS MontoExento,"
   SQL = SQL & "SUM(" & sqlTotalRenglonConDscto & " - " & sqlMontoExentoRenglon & " ) AS BaseImponible,"
   SQL = SQL & "SUM(" & sqlIvaRenglon & " ) AS TotalIva,"
   SQL = SQL & "SUM(" & sqlTotalRenglonConDscto & " + " & sqlIvaRenglon & ") AS TotalFactura,"
   SQL = SQL & "SUM((RenglonFactura.PrecioSinIva*RenglonFactura.Cantidad-TotalRenglon)+(TotalRenglon"
   SQL = SQL & " * Factura.PorcentajeDescuento/100)) AS Descuento From Factura"
   SQL = SQL & " INNER JOIN RenglonFactura ON (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania "
   SQL = SQL & " AND RenglonFactura.NumeroFactura = Factura.Numero"
   SQL = SQL & " AND RenglonFactura.TipoDeDocumento = Factura.TipoDeDocumento)"
   SQL = SQL & " INNER JOIN IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   SQL = SQL & " ON(IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania"
   SQL = SQL & " = RenglonFactura.ConsecutivoCompania AND  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.codigodelArticulo"
   SQL = SQL & " = RenglonFactura.Articulo AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo) ) WHERE"
   SQL = SQL & " Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   If valCantidadLinea <> "Todos" Then
       SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(valLinea)
   End If
   SQL = SQL & " GROUP BY IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto"
   SQL = SQL & " ORDER BY IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto "
   fSQLDeFacturasEntreFechasPorLinea = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorLinea", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasEntreFechasConSerial(ByVal valCantidadAImprimirLineaDeProducto As String, _
                                                             ByVal valLineaDeProductoAImprimir As String, _
                                                              ByVal valTipoDeArticulo As String, _
                                                               ByVal usarCambioOriginal As Boolean, _
                                                                ByVal valReporteEnMonedaLocal As Boolean, _
                                                                 ByVal gUltimaTasaDeCambio As Object, _
                                                                  ByVal gMonedaLocalActual As Object, _
                                                                   ByVal valImprimirUnaLineaDeProducto As Boolean, _
                                                                    ByVal valImprimirUnTipoDeArticulo As Boolean, _
                                                                     ByVal valConsecutivoCompania As String, _
                                                                      ByVal valFechaInicial As Date, _
                                                                       ByVal valFechaFinal As Date, _
                                                                        ByVal valUsaPrecioSinIva As Boolean, _
                                                                         ByVal valInformeResumido As Boolean, _
                                                                            ByVal valcodigoCliente As String) As String
   Dim SQL As String
   Dim sqlPrecioSinIva As String
   Dim sqlPrecioConIva As String
   Dim SqlTotalRenglon As String
   Dim SqlTotalCantidad As String
   Dim SqlTotalMonto As String
   Dim SqlInnerJoin As String
   Dim SqlStatusFactura As String
   Dim sqlRenglon As String
   Dim sqlMontoRenglon As String
   Dim sqlDescMonto As String
   Dim sqlFactMonto As String
   Dim SqlRenglonTotal As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vPrecio As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo

   SqlStatusFactura = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), _
                                       gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                                       gUtilSQL.fSimpleSqlValue("Vigente"), True)
                                       
   If valUsaPrecioSinIva Then
      vPrecio = "RenglonFactura.PrecioSinIva"
   Else
      vPrecio = "RenglonFactura.PrecioConIva"
   End If
   sqlRenglon = "(" & vPrecio & " * RenglonFactura.Cantidad)"
   sqlMontoRenglon = "(" & sqlRenglon & " * (1 - (RenglonFactura.PorcentajeDescuento / 100)))"
   sqlDescMonto = "((" & sqlRenglon & ") - (" & sqlMontoRenglon & "))"
   sqlFactMonto = "((( " & sqlRenglon & " * (1 - (RenglonFactura.PorcentajeDescuento / 100))) * (factura.MontoDescuento1*1000 / factura.TotalRenglones*1000)/1000000))"
   SqlRenglonTotal = "(" & sqlRenglon & " - " & sqlDescMonto & " - " & sqlFactMonto & ")"
     
   If valReporteEnMonedaLocal Then
      sqlPrecioConIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioConIva", "Factura.Fecha"), _
            usarCambioOriginal, ""), True)
            
      sqlPrecioSinIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "RenglonFactura.PrecioSinIva", "Factura.Fecha"), _
            usarCambioOriginal, ""), True)
           
      SqlTotalRenglon = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SqlRenglonTotal, "Factura.Fecha"), _
            usarCambioOriginal, ""), True)
   Else
      sqlPrecioConIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                        "RenglonFactura.PrecioConIva", True)
      sqlPrecioSinIva = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                        "RenglonFactura.PrecioSinIva", True)
      SqlTotalRenglon = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                        SqlRenglonTotal, True)
   End If
      
   SQL = "SELECT "
   SQL = SQL & "Factura.Numero, "
   SQL = SQL & "Factura.Moneda, "
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   If valInformeResumido Then
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "SUM(RenglonFactura.Cantidad)", True) & " AS CantidadDeArticulo, "
      SQL = SQL & "SUM(" & SqlTotalRenglon & ") AS TotalRenglon "
   Else
      SQL = SQL & "RenglonFactura.Articulo AS CodigoArticulo, "
      SQL = SQL & "RenglonFactura.Descripcion AS DescripcionArticulo, "
      SQL = SQL & "RenglonFactura.PorcentajeDescuento AS PorcDectoRenglon, "
      SQL = SQL & "Factura.PorcentajeDescuento AS PorcDectoFactura, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "RenglonFactura.Cantidad", True) & " AS CantidadDeArticulo, "
      SQL = SQL & sqlPrecioConIva & " AS PrecioConIva, "
      SQL = SQL & sqlPrecioSinIva & " AS PrecioSinIva, "
      SQL = SQL & SqlTotalRenglon & " AS TotalRenglon, "
      SQL = SQL & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual) & " AS TotalRenglonConIva, "
      SQL = SQL & SqlStatusFactura & " AS StatusFactura, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlDescMonto, True) & " AS MontoDescRenglon, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlFactMonto, True) & " AS MontoDescFactura, "
   End If
   SQL = SQL & "RenglonFactura.Serial "
   SQL = SQL & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   SQL = SQL & " INNER JOIN ((Cliente INNER JOIN Factura"
   SQL = SQL & " ON  (Cliente.Codigo = Factura.CodigoCliente)"
   SQL = SQL & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   SQL = SQL & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   SQL = SQL & " AND renglonFactura.Serial <> '0')"
   SQL = SQL & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   SQL = SQL & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   If valImprimirUnaLineaDeProducto Then
      SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductoAImprimir))
   End If
   If valImprimirUnTipoDeArticulo Then
      SQL = SQL & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strTipoDeArticuloToNum(valTipoDeArticulo))
   End If
   SQL = SQL & " AND (Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & " OR  Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & " OR  Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & " )"
   SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   SQL = SQL & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      SQL = SQL & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   If valInformeResumido Then
      SQL = SQL & " GROUP BY Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.StatusFactura "
   End If
   SQL = SQL & " ORDER BY Factura.StatusFactura, Factura.Moneda, Factura.Fecha, Factura.Numero"

   fSQLDeFacturasEntreFechasConSerial = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasPorTalonario(ByVal valConsecutivoCompania As String, _
                                                                              ByVal valFechaInicial As Date, _
                                                                                ByVal valFechaFinal As Date, _
                                                                                  ByVal valTalonario As String, _
                                                                                    ByVal valcodigoCliente As String, _
                                                                                      ByVal valFiltrarTalonario As String) As String
   Dim SQL As String
   Dim SqlStatusFactura As String
   Dim sqlFactMonto As String
   Dim SqlTalonario As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vPrecio As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
    SqlStatusFactura = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), _
                                       gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                                       gUtilSQL.fSimpleSqlValue("Vigente"), True)
  
   sqlFactMonto = gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", "factura.TotalFactura", True)
                                       
   SqlTalonario = gUtilSQL.getIIF("factura.Talonario = '0' ", "'Talonario 1'", "'Talonario 2'", True)
                                       
   SQL = "SELECT "
   SQL = SQL & "Factura.Numero, "
   SQL = SQL & "Factura.Moneda, "
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   SQL = SQL & "Factura.PorcentajeDescuento, "
   SQL = SQL & sqlFactMonto & " AS TotalFactura,"
   SQL = SQL & SqlStatusFactura & " AS StatusFactura, "
   SQL = SQL & SqlTalonario & " AS Talonario"
   SQL = SQL & " FROM factura"
   SQL = SQL & " INNER JOIN Cliente ON Factura.CodigoCliente = Cliente.Codigo and factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   SQL = SQL & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If valFiltrarTalonario = "1" Then
      SQL = SQL & " AND factura.Talonario = '" & valTalonario & "'"
   End If
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      SQL = SQL & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & "group by Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.PorcentajeDescuento,"
   SQL = SQL & " factura.TotalFactura , StatusFactura, Talonario"
   SQL = SQL & " ORDER BY factura.Talonario, factura.Numero, factura.Fecha"

   fSQLDeFacturasPorTalonario = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fGroupbyCostoDeFacturaEntreFecha() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = " dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Fecha,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Nombre ,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.CodigoVendedor,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.NombreVendedor,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Articulo,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Serial,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Rollo,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Descripcion,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual,"
   vResult = vResult & " Gf_CostoPromedioPonderado_1.CostoFinal ,"
   vResult = vResult & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda"
h_EXIT: On Error GoTo 0
  fGroupbyCostoDeFacturaEntreFecha = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGroupbyCostoDeFacturaEntreFecha()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLGenerarOtrosCargosyDescuentosPorCamposDefinibles(ByVal valUsaModuloDeContabilidad As Boolean, _
                                                    ByVal valConsecutivoCompania As String, _
                                                     ByVal valFechaInicial As Date, _
                                                      ByVal valFechaFinal As Date, _
                                                       ByVal valInformeEsDeBorradores As Boolean, _
                                                        ByVal valReporteEnMonedaLocal As Boolean, _
                                                         ByVal gMonedaLocalActual As Object, _
                                                          ByVal valFiltroInforme As String, _
                                                           ByVal valCampoAMostrar As String, _
                                                            ByVal valCampoDefinible As Integer, _
                                                             ByVal valFiltrarCampoDef As Boolean, _
                                                              ByVal valValorCampoDef As String) As String
   Dim SQL As String
   Dim SqlDelReporte As String
   Dim vCampoDefinible As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   Select Case valCampoDefinible
   Case 0
      vCampoDefinible = "CampoDefinible1"
   Case 1
      vCampoDefinible = "CampoDefinible2"
   Case 2
      vCampoDefinible = "CampoDefinible3"
   Case 3
      vCampoDefinible = "CampoDefinible4"
   Case 4
      vCampoDefinible = "CampoDefinible5"
   Case 5
      vCampoDefinible = "CampoDefinible6"
   Case 6
      vCampoDefinible = "CampoDefinible7"
   Case 7
      vCampoDefinible = "CampoDefinible8"
   Case 8
      vCampoDefinible = "CampoDefinible9"
   Case 9
      vCampoDefinible = "CampoDefinible10"
   Case 10
      vCampoDefinible = "CampoDefinible11"
   Case 11
      vCampoDefinible = "CampoDefinible12"
   End Select
   
   SQL = "SELECT "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         SQL = SQL & "Factura.Moneda, "
   End If
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "2 AS ORDEN, "
   SQL = SQL & valCampoAMostrar & " as Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   SQL = SQL & "Factura.TipoDeDocumento, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   SQL = SQL & "'' AS StatusFactura, "
   SQL = SQL & "0 AS MontoOriginal, "
   SQL = SQL & "0 AS TotalIVA, "
   SQL = SQL & "0 AS TotalBaseImponible, "
   SQL = SQL & "0 AS TotalMontoExento, "
   SQL = SQL & "0 AS TotalFactura, "
   SQL = SQL & "SUM (RenglonDetalleDeOtrosCargosFactura.TotalRenglon) AS OtrosCargos, "
   SQL = SQL & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      SQL = SQL & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      SQL = SQL & " AS NumeroComprobante"
   End If
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS ComprobanteFiscal"
   SQL = SQL & ", camposDefFactura." & vCampoDefinible & " AS CampoDefinible"
   SQL = SQL & " FROM (Cliente INNER JOIN "
   SQL = SQL & " Factura ON (Cliente.Codigo = Factura.CodigoCliente)"
   SQL = SQL & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN RenglonDetalleDeOtrosCargosFactura ON "
   SQL = SQL & " (Factura.TipoDeDocumento = RenglonDetalleDeOtrosCargosFactura.TipoDeDocumento)"
   SQL = SQL & " AND (Factura.Numero = RenglonDetalleDeOtrosCargosFactura.NumeroFactura)"
   SQL = SQL & " AND (Factura.ConsecutivoCompania = RenglonDetalleDeOtrosCargosFactura.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   SQL = SQL & " AND factura.CodigoVendedor = Vendedor.Codigo "
   SQL = SQL & " INNER JOIN camposDefFactura ON (Factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania"
   SQL = SQL & " AND factura.Numero = camposDefFactura.NumeroFactura)"
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   SQL = SQL & " AND (renglonDetalleDeOtrosCargosFactura.ComoAplicaAlTotalFactura <> " & gUtilSQL.fSimpleSqlValue(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO) & ")"
   If valInformeEsDeBorradores Then
      SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltrarCampoDef Then
      SQL = gUtilSQL.fSQLValueWithWildcardWithAnd(SQL, "camposDefFactura." & vCampoDefinible, valValorCampoDef)
   End If
   If valFiltroInforme <> "" Then
      SQL = SQL & " AND " & valFiltroInforme
   End If

   SQL = SQL & " GROUP BY "
   If Not valReporteEnMonedaLocal Then
      SQL = SQL & "Factura.Moneda, "
   End If
      SQL = SQL & "Factura.Fecha, Factura.Numero, Factura.TipoDeDocumento, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Vendedor.Nombre, camposDefFactura." & vCampoDefinible

   fSQLGenerarOtrosCargosyDescuentosPorCamposDefinibles = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarOtrosCargosyDescuentosPorCamposDefinibles", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasEntreFechasPorCamposDefinibles(ByVal valReporteEnMonedaLocal As Boolean, _
                                           ByVal gMonedaLocalActual As Object, _
                                            ByVal gUltimaTasaDeCambio As Object, _
                                             ByVal valUsaModuloDeContabilidad As Boolean, _
                                              ByVal valConsecutivoCompania As String, _
                                               ByVal valFechaInicial As Date, _
                                                ByVal valFechaFinal As Date, _
                                                 ByVal valInformeEsDeBorradores As Boolean, _
                                                  ByVal valFiltroInforme As String, _
                                                   ByVal valAgrupaInforme As Boolean, _
                                                    ByVal valUsarCiudad As Boolean, _
                                                     ByVal valCiudad As String, _
                                                      ByVal valCampoAMostrar As String, _
                                                       ByVal valCampoDefinible As Integer, _
                                                        ByVal valFiltrarCampoDef As Boolean, _
                                                         ByVal valValorCampoDef As String) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim vCampoDefinible As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   Select Case valCampoDefinible
      Case 0
         vCampoDefinible = "CampoDefinible1"
      Case 1
         vCampoDefinible = "CampoDefinible2"
      Case 2
         vCampoDefinible = "CampoDefinible3"
      Case 3
         vCampoDefinible = "CampoDefinible4"
      Case 4
         vCampoDefinible = "CampoDefinible5"
      Case 5
         vCampoDefinible = "CampoDefinible6"
      Case 6
         vCampoDefinible = "CampoDefinible7"
      Case 7
         vCampoDefinible = "CampoDefinible8"
      Case 8
         vCampoDefinible = "CampoDefinible9"
      Case 9
         vCampoDefinible = "CampoDefinible10"
      Case 10
         vCampoDefinible = "CampoDefinible11"
      Case 11
         vCampoDefinible = "CampoDefinible12"
   End Select
         
         
   SQL = "SELECT "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         SQL = SQL & "Factura.Moneda, "
   End If
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & "1 AS ORDEN, "
   SQL = SQL & valCampoAMostrar & " as Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   SQL = SQL & "Factura.TipoDeDocumento, "
   SQL = SQL & "Factura.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cliente.Contacto, "
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                               gUtilSQL.fSimpleSqlValue(""), True) & " AS StatusFactura, "
   SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", "Factura.TotalFactura", True) & " AS MontoOriginal, "
   If valReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalIva", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalBaseImponible", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalBaseImponible, "
'EXENTO
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalMontoExento", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalMontoExento, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalFactura, "
   Else
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalIVA", True) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalBaseImponible", True) & " AS TotalBaseImponible, "
     SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                          gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                          gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalMontoExento", _
                          "Factura.Fecha"), True, ""), True) & " AS TotalMontoExento, "
     SQL = SQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   SQL = SQL & "0 AS OtrosCargos, "
   SQL = SQL & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      SQL = SQL & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      SQL = SQL & " AS NumeroComprobante"
   End If
   SQL = SQL & ", Factura.NumeroComprobanteFiscal AS ComprobanteFiscal "
   SQL = SQL & ", camposDefFactura." & vCampoDefinible & " AS CampoDefinible"
   SQL = SQL & " FROM Factura INNER JOIN "
   SQL = SQL & " Cliente ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND Factura.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   SQL = SQL & " AND factura.CodigoVendedor = Vendedor.Codigo"
   SQL = SQL & " INNER JOIN camposDefFactura ON (Factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania"
   SQL = SQL & " AND factura.Numero = camposDefFactura.NumeroFactura)"
   SQL = SQL & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   If valInformeEsDeBorradores Then
      SQL = SQL & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      SQL = SQL & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltroInforme <> "" Then
      SQL = SQL & " AND " & valFiltroInforme
   End If
    If valUsarCiudad Then
      SQL = SQL & " AND Cliente.Ciudad = " & gUtilSQL.fSimpleSqlValue(valCiudad)
   End If
   SQL = SQL & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valFiltrarCampoDef Then
      SQL = gUtilSQL.fSQLValueWithWildcardWithAnd(SQL, "camposDefFactura." & vCampoDefinible, valValorCampoDef)
   End If
   
   If valAgrupaInforme Then
      SQL = SQL & " ORDER BY Moneda, Vendedor, TipoDeDocumentoStr, Fecha, camposDefFactura." & vCampoDefinible
   Else
      SQL = SQL & " ORDER BY Moneda, TipoDeDocumentoStr, Fecha, camposDefFactura." & vCampoDefinible
   End If
   
   fSQLDeFacturasEntreFechasPorCamposDefinibles = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorCamposDefinibles", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCaseIfForEnumTipoDeProductoToStringInArray(ByRef gEnumProyecto As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.DfSQLCaseIfForEnum(" ArticuloInventario." & "TipoDeProducto", _
         enum_TipoDeProducto.eTD_TP_NUEVO, gEnumProyecto.fEnumTipoDeProductoToStringInArray, "")
   fSQLCaseIfForEnumTipoDeProductoToStringInArray = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCaseIfForEnumTipoDeProductoToStringInArray", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

