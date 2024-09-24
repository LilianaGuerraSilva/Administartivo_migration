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
Private Const GetGender = Enum_Gender.eg_Female

Private insRenglonComicionesDeVendedor As Object
Private insRengFac As Object
Private insFactura As Object
Private insRenglonCobroDeFactura As Object
Public Enum Enum_Vendedores_Por_Renglon
   eVPR_Vendedor1 = 1
   eVPR_Vendedor2
   eVPR_Vendedor3
End Enum

Public Function fSQLFacturacionEntreFechasXCondicionDePago(ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                                    ByVal valReporteEnMonedaLocal As Boolean, ByVal valConsecutivoCompania As String, _
                                                      ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   Sql = "SELECT "
   Sql = Sql & "Factura.CondicionesDePago, "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS MonedaDelReporte, "
   Else
      Sql = Sql & "Factura.Moneda AS MonedaDelReporte, "
   End If
   Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "Factura.Numero" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" (*)"), "Factura.Numero", True) & " AS NumeroFactura, "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDeDocumento, "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), _
                                  0, gUtilSQL.getIIF("Factura.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & _
                                  " OR Factura.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", "Factura.TotalFactura", True, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", "Factura.Fecha"), _
                                  True), True) & " AS TotalFactura, "
   Else
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), 0, "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   Sql = Sql & "Factura.Moneda, "
   Sql = Sql & "Factura.CambioABolivares, "
   Sql = Sql & "Factura.CodigoMoneda "
   Sql = Sql & "FROM Factura INNER JOIN "
   Sql = Sql & "cliente ON Factura.ConsecutivoCompania = cliente.ConsecutivoCompania "
   Sql = Sql & "AND Factura.CodigoCliente = cliente.Codigo "
   Sql = Sql & "WHERE (Factura.consecutivoCompania = " & valConsecutivoCompania & ") "
   Sql = Sql & "AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & "AND (Factura.StatusFactura <> " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_BORRADOR) & ") "
   Sql = Sql & "AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valReporteEnMonedaLocal Then
      Sql = Sql & "ORDER BY Factura.CondicionesDePago, Factura.Fecha, Numero "
   Else
      Sql = Sql & "ORDER BY Factura.CondicionesDePago, MonedaDelReporte, Factura.Fecha, Numero "
   End If
   fSQLFacturacionEntreFechasXCondicionDePago = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturacionEntreFechasXCondicionDePago", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreFechas(ByVal valConsecutivoCompania As String, ByVal valCategoria As String, _
                                                                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = Sql & "SELECT Cliente.Nombre AS NombreCliente, "
   Sql = Sql & "MAX(Factura.Fecha) AS FechaUltima, "
   Sql = Sql & "Cliente.FAX AS Fax, "
   Sql = Sql & "Cliente.Telefono AS Telefono, "
   Sql = Sql & "Cliente.Ciudad, "
   Sql = Sql & "Cliente.Codigo AS Codigo"
   Sql = Sql & " FROM articuloInventario INNER JOIN"
   Sql = Sql & " ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente)"
   Sql = Sql & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)) INNER JOIN"
   Sql = Sql & " renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento)"
   Sql = Sql & " AND (factura.Numero = renglonFactura.NumeroFactura)"
   Sql = Sql & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
   Sql = Sql & " ON (articuloInventario.Codigo = renglonFactura.Articulo)"
   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania)
   Sql = Sql & " AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   Sql = Sql & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   Sql = Sql & " GROUP BY Cliente.Ciudad, Cliente.Codigo, Cliente.Nombre, Cliente.Telefono, Cliente.Fax"
   Sql = Sql & " ORDER BY Cliente.Ciudad, Cliente.Codigo"
   fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreFechas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreCiudades(ByVal valConsecutivoCompania As String, ByVal valCategoria As String, _
                                                                      ByVal valCiudadDesde As String, ByVal valCiudadHasta As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = Sql & "SELECT Cliente.Nombre AS NombreCliente, "
   Sql = Sql & "MAX(Factura.Fecha) AS FechaUltima, "
   Sql = Sql & "Cliente.FAX AS Fax, "
   Sql = Sql & "Cliente.Telefono AS Telefono, "
   Sql = Sql & "Cliente.Ciudad, "
   Sql = Sql & "Cliente.Codigo AS Codigo"
   Sql = Sql & " FROM articuloInventario INNER JOIN"
   Sql = Sql & " ((cliente INNER JOIN factura ON (cliente.Codigo = factura.CodigoCliente)"
   Sql = Sql & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN renglonFactura ON (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento)"
   Sql = Sql & " AND (factura.Numero = renglonFactura.NumeroFactura)"
   Sql = Sql & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
   Sql = Sql & " ON (articuloInventario.Codigo = renglonFactura.Articulo)"
   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) 'gProyCompaniaActual.GetConsecutivoCompania
   Sql = Sql & " AND articuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)     'Trim(txtCategoria.Text))
   Sql = Sql & " AND cliente.Ciudad BETWEEN " & gUtilSQL.fSimpleSqlValue(valCiudadDesde) & " AND " & gUtilSQL.fSimpleSqlValue(valCiudadHasta) 'Trim(txtCiudadDesde.Text) Trim(txtCiudadHasta.Text)
   Sql = Sql & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   Sql = Sql & " GROUP BY Cliente.Ciudad, Cliente.Codigo, Cliente.Nombre, Cliente.Telefono, Cliente.Fax  "
   Sql = Sql & " ORDER BY Cliente.Ciudad, Cliente.Codigo  "
   fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreCiudades = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteProdVenEntreCiudadesXCatEntreCiudades", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlDelReporteNotasDeCreditoAsociadasAFacturas(ByVal valUsaCambio As Boolean, ByVal gMonedaLocalActual As Object, _
                                                                 ByVal gUltimaTasaDeCambio As Object, ByVal valConsecutivoCompania As String, _
                                                                   ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                                     ByVal valCmbStatusNC As String, ByVal valCmbStatusNCNumero As Integer)
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Factura.StatusFactura", enum_StatusFactura.eSF_EMITIDA, gEnumProyecto.fEnumStatusFacturaToStringInArray(True, False), "") & " AS StatusNC, "
   If valUsaCambio Then
      Sql = Sql & "factura.Numero AS NumeroNC, factura.Fecha AS FechaNC, factura.NumeroFacturaAfectada, " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", "Factura.Fecha"), True, "TotalNotadeCredito") & ","
      Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.totalBaseImponible", "Factura.Fecha"), True, "TotalBaseImponible") & ","
      Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalIVA", "Factura.Fecha"), True, "TotalIVA") & ", factura.TipoDeDocumento, "
      Sql = Sql & " factura_1.Fecha, factura_1.numero, factura_1.Moneda, factura.Moneda AS MonedaNC, " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura_1.CambioABolivares", "factura_1.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura_1.TotalFactura", "Factura.Fecha"), True, "TotalFactura")
   Else
      Sql = Sql & "factura.Numero AS NumeroNC, factura.Fecha AS FechaNC, factura.NumeroFacturaAfectada, (factura.TotalFactura) AS TotalNotadeCredito, factura.TotalBaseImponible, factura.TotalIVA, factura.TipoDeDocumento, factura.Moneda AS MonedaNC, "
      Sql = Sql & "factura_1.Fecha, (factura_1.TotalFactura) AS TotalFactura, factura_1.Numero, factura_1.Moneda"
   End If
   Sql = Sql & " FROM factura INNER JOIN factura AS factura_1 ON (factura.NumeroFacturaAfectada = factura_1.Numero) AND (factura.ConsecutivoCompania = factura_1.ConsecutivoCompania)"
   Sql = Sql & " WHERE  factura.ConsecutivoCompania= " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueDatesBetween("", "factura.fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura_1.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
   Sql = Sql & " AND  Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   If valCmbStatusNC <> "Todos" Then
      Sql = Sql & " AND FACTURA.StatusFactura = " & gUtilSQL.fSimpleSqlValue(valCmbStatusNCNumero)
   End If
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   Sql = Sql & " GROUP BY factura_1.Numero,factura.numerofacturaafectada, factura.fecha,factura.numero, factura.TotalFactura, factura.TotalBaseImponible, Factura.TotalIva, Factura.TipodeDocumento, factura.StatusFactura,"
   Sql = Sql & " factura.StatusFactura, factura_1.Fecha, factura_1.StatusFactura, factura_1.TotalFactura,factura.CambioABolivares,factura_1.CambioABolivares,factura.Moneda,factura_1.Moneda"
   Sql = Sql & " ORDER BY Factura.NumeroFacturaAfectada"
   fSqlDelReporteNotasDeCreditoAsociadasAFacturas = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlDelReporteNotasDeCreditoAsociadasAFacturas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlDelReporteFacturacionConOtrosCargosYDescuentos(ByVal valUsaCambio As Boolean, ByVal gMonedaLocalActual As Object, _
                                                                      ByVal gUltimaTasaDeCambio As Object, ByVal valConsecutivoCompania As String, _
                                                                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCodigoCargo As String)
   Dim Sql As String
   Dim sqlMoneda As String
   On Error GoTo h_ERROR
   If valUsaCambio Then
      sqlMoneda = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   Else
      sqlMoneda = "Factura.Moneda"
   End If
   Sql = "SELECT "
   Sql = Sql & sqlMoneda & " AS Moneda"
   Sql = Sql & ", Factura.Fecha"
   Sql = Sql & ", Factura.Numero AS NumeroFactura"
   Sql = Sql & ", Factura.TipoDeDocumento"
   Sql = Sql & ", Cliente.Nombre"
   If valUsaCambio Then
      Sql = Sql & ", ROUND(Factura.TotalFactura * Factura.CambioABolivares, 2) AS TotalFactura"
      Sql = Sql & ", ROUND(Factura.CambioABolivares * RenglonDetalleDeOtrosCargosFactura.TotalRenglon, 2) AS TotalRenglonOtrosCargos"
   Else
      Sql = Sql & ", ROUND(Factura.TotalFactura, 2) AS TotalFactura"
      Sql = Sql & ", ROUND(RenglonDetalleDeOtrosCargosFactura.TotalRenglon, 2) AS TotalRenglonOtrosCargos"
   End If
   Sql = Sql & ", RenglonDetalleDeOtrosCargosFactura.CodigoDeCargo AS CodigoOtrosCargos"
   Sql = Sql & ", RenglonDetalleDeOtrosCargosFactura.Descripcion AS DescripcionOtrosCargos"
   Sql = Sql & " FROM Cliente"
   Sql = Sql & " INNER JOIN Factura"
   Sql = Sql & " INNER JOIN RenglonDetalleDeOtrosCargosFactura"
   Sql = Sql & " ON Factura.TipoDeDocumento = RenglonDetalleDeOtrosCargosFactura.TipoDeDocumento"
   Sql = Sql & " AND Factura.Numero = RenglonDetalleDeOtrosCargosFactura.NumeroFactura"
   Sql = Sql & " AND Factura.ConsecutivoCompania = RenglonDetalleDeOtrosCargosFactura.ConsecutivoCompania"
   Sql = Sql & " ON Cliente.Codigo = Factura.CodigoCliente"
   Sql = Sql & " AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania"
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueDatesBetween("", "Factura.Fecha", valFechaInicial, valFechaFinal)
   If (valCodigoCargo <> "") Then
      Sql = Sql & " AND RenglonDetalleDeOtrosCargosFactura.CodigoDeCargo = " & gUtilSQL.fSimpleSqlValue(valCodigoCargo)
   End If
   Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " ORDER BY Moneda, Factura.Fecha, Factura.Numero, factura.TipoDeDocumento"
   fSqlDelReporteFacturacionConOtrosCargosYDescuentos = Sql
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDelReporteFacturacionConOtrosCargosYDescuentos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturaPorArticuloYPorVendedor(ByVal valInformeDetallado As Boolean, ByVal valUsarCambio As Boolean, _
                                                                   ByVal valCompaniaUsaPrecioSinIvaSegunTipoDocumento As Boolean, _
                                                                    ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, _
                                                                      ByVal valcodigoDeVendedor As String, ByVal valConsecutivoCompania As String, _
                                                                        ByVal valImprimirSoloUnArticulo As Boolean, ByVal valCodigoDeArticulo As String, _
                                                                          ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                                            ByVal valImprimirSoloUnVendedor As Boolean, ByVal valComisiones As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   If valComisiones Then
      Sql = "SELECT "
      Sql = Sql & "Vendedor.Codigo AS CodigoVendedor, "
      Sql = Sql & "Vendedor.Nombre AS NombreVendedor, "
      Sql = Sql & "Factura.Moneda, "
      Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, "
      Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion, "
      Sql = Sql & "Factura.Fecha, "
      If valInformeDetallado Then
         Sql = Sql & "Factura.CodigoCliente, "
         Sql = Sql & "Cliente.Nombre, "
         Sql = Sql & "Factura.Numero, "
         Sql = Sql & "Factura.TipoDeDocumento, "
         Sql = Sql & "RenglonFactura.Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            Sql = Sql & "RenglonFactura.PrecioSinIVA AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * RenglonFactura.Cantidad) AS MargenGanancia, "
         Else
            Sql = Sql & "RenglonFactura.PrecioConIVA AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * RenglonFactura.Cantidad) AS MargenGanancia, "
         End If
         Sql = Sql & "ArticuloInventario.CostoUnitario "
         Sql = Sql & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 INNER JOIN ArticuloInventario "
         Sql = Sql & " ON  (ArticuloInventario.Codigo = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo)"
         Sql = Sql & " AND (ArticuloInventario.ConsecutivoCompania = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania)"
         Sql = Sql & " INNER JOIN (((Vendedor INNER JOIN Factura"
         Sql = Sql & " ON  (Vendedor.Codigo = Factura.CodigoVendedor)"
         Sql = Sql & " AND (Vendedor.ConsecutivoCompania = Factura.ConsecutivoCompania))"
         Sql = Sql & " INNER JOIN Cliente ON (Cliente.Codigo = Factura.CodigoCliente)"
         Sql = Sql & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
         Sql = Sql & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
         Sql = Sql & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
         Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
         Sql = Sql & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
         Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial)"
         Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
         Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania) "
      Else
         Sql = Sql & "SUM(RenglonFactura.Cantidad) AS Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            Sql = Sql & "SUM(RenglonFactura.PrecioSinIVA) AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales("SUM(" & fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon"
         Else
            Sql = Sql & "SUM(RenglonFactura.PrecioConIVA) AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales("SUM(" & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon"
         End If
         Sql = Sql & " FROM Vendedor"
         Sql = Sql & " INNER JOIN (ArticuloInventario"
         Sql = Sql & " INNER JOIN IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1 ON ArticuloInventario.Codigo= IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo "
         Sql = Sql & " AND ArticuloInventario.ConsecutivoCompania = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania )"
         Sql = Sql & " INNER JOIN (Factura"
         Sql = Sql & " INNER JOIN RenglonFactura"
         Sql = Sql & " ON  (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
         Sql = Sql & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
         Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
         Sql = Sql & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
         Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial)"
         Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
         Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
         Sql = Sql & " ON  (Vendedor.Codigo = Factura.CodigoVendedor)"
         Sql = Sql & " AND (Vendedor.ConsecutivoCompania = Factura.ConsecutivoCompania)"
      End If
      Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
      If valImprimirSoloUnArticulo Then
         Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeArticulo))
      End If
      Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
      If valImprimirSoloUnVendedor Then
         Sql = Sql & " AND " & "Factura.CodigoVendedor = " & gUtilSQL.fSimpleSqlValue(Trim(valcodigoDeVendedor))
      End If
      If Not valInformeDetallado Then
         Sql = Sql & " GROUP BY Vendedor.Codigo, Vendedor.Nombre, Factura.Moneda, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion, Factura.Fecha "
      End If
      Sql = Sql & " ORDER BY Vendedor.Codigo, Factura.Moneda, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, Factura.Fecha"
      If valInformeDetallado Then
         Sql = Sql & ", Factura.TipoDeDocumento, Factura.Numero"
      End If
   Else 'Facturación por renglones
      Sql = "SELECT "
      Sql = Sql & "IGV_VendedorXRenglonFactura.CodigoVendedor AS CodigoVendedor, "
      Sql = Sql & "IGV_VendedorXRenglonFactura.NombreVendedor AS NombreVendedor, "
      Sql = Sql & "Factura.Moneda, "
      Sql = Sql & "ArticuloInventario.Codigo, "
      Sql = Sql & "ArticuloInventario.Descripcion, "
      Sql = Sql & "Factura.Fecha, "
      If valInformeDetallado Then
         Sql = Sql & "Factura.CodigoCliente, "
         Sql = Sql & "Cliente.Nombre, "
         Sql = Sql & "Factura.Numero, "
         Sql = Sql & "Factura.TipoDeDocumento, "
         Sql = Sql & "IGV_VendedorXRenglonFactura.Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            Sql = Sql & "IGV_VendedorXRenglonFactura.PrecioSinIVA AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * IGV_VendedorXRenglonFactura.Cantidad) AS MargenGanancia, "
         Else
            Sql = Sql & "IGV_VendedorXRenglonFactura.PrecioConIVA AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " AS TotalRenglon, "
            Sql = Sql & gUtilSQL.fRoundNDecimales(fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual), 2) & " - (ArticuloInventario.CostoUnitario * IGV_VendedorXRenglonFactura.Cantidad) AS MargenGanancia, "
         End If
         Sql = Sql & "ArticuloInventario.CostoUnitario as CostoUnitario"
         Sql = Sql & " FROM ArticuloInventario INNER JOIN IGV_VendedorXRenglonFactura "
         Sql = Sql & " ON  (ArticuloInventario.Codigo = IGV_VendedorXRenglonFactura.Articulo"
         Sql = Sql & " AND ArticuloInventario.ConsecutivoCompania = IGV_VendedorXRenglonFactura.ConsecutivoCompania) "
         Sql = Sql & " INNER JOIN Factura"
         Sql = Sql & " ON  (IGV_VendedorXRenglonFactura.ConsecutivoCompania = Factura.ConsecutivoCompania"
         Sql = Sql & " AND Factura.TipoDeDocumento = IGV_VendedorXRenglonFactura.TipoDeDocumento"
         Sql = Sql & " AND Factura.Numero = IGV_VendedorXRenglonFactura.NumeroFactura)"
         Sql = Sql & " INNER JOIN Cliente ON (Cliente.Codigo = Factura.CodigoCliente"
         Sql = Sql & " AND Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania)"
      Else
         Sql = Sql & "SUM(IGV_VendedorXRenglonFactura.Cantidad) AS Cantidad, "
         If valCompaniaUsaPrecioSinIvaSegunTipoDocumento Then
            Sql = Sql & "SUM(IGV_VendedorXRenglonFactura.PrecioSinIVA) AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales("Sum(" & fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon"
         Else
            Sql = Sql & "SUM(IGV_VendedorXRenglonFactura.PrecioConIVA) AS  PrecioUnitario, "
            Sql = Sql & gUtilSQL.fRoundNDecimales("Sum(" & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(valUsarCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual) & ")", 2) & " AS TotalRenglon "
         End If
         Sql = Sql & " FROM IGV_VendedorXRenglonFactura"
         Sql = Sql & " INNER JOIN ArticuloInventario"
         Sql = Sql & " ON  (ArticuloInventario.Codigo = IGV_VendedorXRenglonFactura.Articulo"
         Sql = Sql & " AND ArticuloInventario.ConsecutivoCompania = IGV_VendedorXRenglonFactura.ConsecutivoCompania)"
         Sql = Sql & " INNER JOIN Factura"
         Sql = Sql & " ON  (Factura.TipoDeDocumento = IGV_VendedorXRenglonFactura.TipoDeDocumento"
         Sql = Sql & " AND Factura.Numero = IGV_VendedorXRenglonFactura.NumeroFactura"
         Sql = Sql & " AND Factura.ConsecutivoCompania = IGV_VendedorXRenglonFactura.ConsecutivoCompania)"
      End If
      Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
      If valImprimirSoloUnArticulo Then
         Sql = Sql & " AND IGV_VendedorXRenglonFactura.Articulo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeArticulo))
      End If
      Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
      If valImprimirSoloUnVendedor Then
         Sql = Sql & " AND " & "IGV_VendedorXRenglonFactura.CodigoVendedor = " & gUtilSQL.fSimpleSqlValue(Trim(valcodigoDeVendedor))
      End If
      Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      If Not valInformeDetallado Then
         Sql = Sql & " GROUP BY IGV_VendedorXRenglonFactura.CodigoVendedor, IGV_VendedorXRenglonFactura.NombreVendedor, Factura.Moneda, ArticuloInventario.Codigo, ArticuloInventario.Descripcion, Factura.Fecha "
      End If
      Sql = Sql & " ORDER BY IGV_VendedorXRenglonFactura.CodigoVendedor, Factura.Moneda, ArticuloInventario.Codigo, Factura.Fecha"
      If valInformeDetallado Then
         Sql = Sql & ", Factura.TipoDeDocumento, Factura.Numero"
      End If
   End If
   
   fConstruirSQLDelReporteFacturaPorArticuloYPorVendedor = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteFacturaPorArticuloYPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturaPorArticuloYPorAlamcen(ByVal valConsecutivoCompania As String, ByVal valImprimirSoloUnAlmacen As Boolean, _
                                    ByVal valConsecutivoAlmacen As String, ByVal valImprimirSoloUnArticulo As Boolean, _
                                    ByVal valCodigoDeArticulo As String, ByVal valFechaInicial As Date, _
                                    ByVal valFechaFinal As Date, ByVal valInformeDetallado As Boolean) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "Factura.ConsecutivoAlmacen AS CodigoAlmacen, "
   Sql = Sql & "Almacen.Codigo AS CodigoAlmacen, "
   Sql = Sql & "Almacen.NombreAlmacen AS NombreAlmacen, "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, "
   Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion, "
   Sql = Sql & "ExistenciaPorAlmacen.Cantidad as Existencia, "
   Sql = Sql & "Factura.Fecha, "
   If (valInformeDetallado) Then
      Sql = Sql & "Factura.CodigoCliente As CodigoCliente, "
      Sql = Sql & "Cliente.Nombre as NombreCliente, "
      Sql = Sql & "Factura.TipoDeDocumento as TipoDeDocumento, "
      Sql = Sql & "Factura.Numero as FacturaNumero, "
      Sql = Sql & "RenglonFactura.Cantidad as Cantidad "
   Else
      Sql = Sql & "SUM(RenglonFactura.Cantidad) AS Cantidad "
   End If
   Sql = Sql & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   Sql = Sql & " INNER JOIN ArticuloInventario"
   Sql = Sql & "  ON  (ArticuloInventario.Codigo = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo)"
   Sql = Sql & "  AND (ArticuloInventario.ConsecutivoCompania = IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN ((Cliente INNER JOIN Factura ON (Cliente.Codigo = Factura.CodigoCliente) AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   Sql = Sql & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN ExistenciaPorAlmacen ON (Factura.ConsecutivoAlmacen = ExistenciaPorAlmacen.ConsecutivoAlmacen)"
   Sql = Sql & "  AND (Factura.ConsecutivoCompania = ExistenciaPorAlmacen.ConsecutivoCompania)"
   Sql = Sql & "  AND (RenglonFactura.Articulo = ExistenciaPorAlmacen.CodigoArticulo)"
   Sql = Sql & " INNER JOIN Almacen"
   Sql = Sql & " ON ExistenciaPorAlmacen.ConsecutivoAlmacen = Almacen.Consecutivo"
   Sql = Sql & " AND ExistenciaPorAlmacen.ConsecutivoCompania= Almacen.ConsecutivoCompania"
   Sql = Sql & " AND Factura.ConsecutivoAlmacen  = Almacen.Consecutivo"
   Sql = Sql & " AND Factura.ConsecutivoCompania  = Almacen.ConsecutivoCompania )"
   Sql = Sql & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   If valImprimirSoloUnAlmacen Then
      Sql = Sql & " AND Factura.CodigoAlmacen = '" & valConsecutivoAlmacen & "'"
   End If
   If valImprimirSoloUnArticulo Then
      Sql = Sql & " AND RenglonFactura.Articulo = '" & valCodigoDeArticulo & "'"
   End If
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura.ConsecutivoCompania = " & valConsecutivoCompania
   If (valInformeDetallado) Then
      Sql = Sql & " ORDER BY Factura.ConsecutivoAlmacen, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, Factura.Fecha, Factura.TipoDeDocumento, Factura.Numero"
   Else
      Sql = Sql & " GROUP BY Factura.ConsecutivoAlmacen, Almacen.Codigo, Almacen.NombreAlmacen, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo,IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Descripcion,ExistenciaPorAlmacen.Cantidad, Factura.Fecha  "
      Sql = Sql & " ORDER BY Factura.ConsecutivoAlmacen, IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Codigo, Factura.Fecha"
   End If
   fConstruirSQLDelReporteFacturaPorArticuloYPorAlamcen = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteFacturaPorArticuloYPorAlamcen", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(ByVal valUsarCambioABolivares As Boolean, ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                         ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "(RenglonFactura.PrecioSinIVA * RenglonFactura.Cantidad)"
   Sql = "(" & Sql & " * (1 - (RenglonFactura.PorcentajeDescuento / 100.00))) "
   Sql = "(" & Sql & " * (1 - (Factura.PorcentajeDescuento / 100.00))) "
   If valUsarCambioABolivares Then
      Sql = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", Sql, True, "")
   End If
   If Not valVieneDelLibroDeVentas Then
   Sql = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, Sql, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(ByVal valUsarCambioABolivares As Boolean, ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                        ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "((RenglonFactura.PrecioSinIVA * RenglonFactura.Cantidad) * (1 + (PorcentajeAlicuota / 100)))"
   Sql = "(" & Sql & " * (1 - (RenglonFactura.PorcentajeDescuento / 100))) "
   Sql = "(" & Sql & " * (1 - (Factura.PorcentajeDescuento / 100))) "
   If valUsarCambioABolivares Then
      Sql = "(" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", Sql, True, "") & ")"
   End If
   If Not valVieneDelLibroDeVentas Then
      Sql = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, Sql, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasEntreFechasPorTipoDeArticulo(ByVal valCantidadAImprimirLineaDeProducto As String, ByVal valLineaDeProductoAImprimir As String, _
                                                              ByVal valTipoDeArticulo As String, ByVal usarCambioOriginal As Boolean, _
                                                                ByVal valReporteEnMonedaLocal As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                                                                  ByVal gMonedaLocalActual As Object, ByVal valImprimirUnaLineaDeProducto As Boolean, _
                                                                    ByVal valImprimirUnTipoDeArticulo As Boolean, ByVal valConsecutivoCompania As String, _
                                                                      ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                                        ByVal valUsaPrecioSinIva As Boolean, ByVal valInformeResumido As Boolean, ByVal valcodigoCliente As String) As String
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & "Factura.Numero, "
   Sql = Sql & "Factura.Moneda, "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   If valInformeResumido Then
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "SUM(RenglonFactura.Cantidad)", True) & " AS CantidadDeArticulo, "
      Sql = Sql & "SUM(" & SqlTotalRenglon & ") AS TotalRenglon "
   Else
      Sql = Sql & "RenglonFactura.Articulo AS CodigoArticulo, "
      Sql = Sql & "RenglonFactura.Descripcion AS DescripcionArticulo, "
      Sql = Sql & "RenglonFactura.PorcentajeDescuento AS PorcDectoRenglon, "
      Sql = Sql & "Factura.PorcentajeDescuento AS PorcDectoFactura, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "RenglonFactura.Cantidad", True) & " AS CantidadDeArticulo, "
      Sql = Sql & sqlPrecioConIva & " AS PrecioConIva, "
      Sql = Sql & sqlPrecioSinIva & " AS PrecioSinIva, "
      Sql = Sql & SqlTotalRenglon & " AS TotalRenglon, "
      Sql = Sql & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual) & " AS TotalRenglonConIva, "
      Sql = Sql & SqlStatusFactura & " AS StatusFactura, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", sqlDescMonto, True) & " AS MontoDescRenglon, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", sqlFactMonto, True) & " AS MontoDescFactura "
   End If
   Sql = Sql & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   Sql = Sql & " INNER JOIN ((Cliente INNER JOIN Factura"
   Sql = Sql & " ON  (Cliente.Codigo = Factura.CodigoCliente)"
   Sql = Sql & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   Sql = Sql & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
   Sql = Sql & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   If valImprimirUnaLineaDeProducto Then
      Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductoAImprimir))
   End If
   If valImprimirUnTipoDeArticulo Then
      Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strTipoDeArticuloToNum(valTipoDeArticulo))
   End If
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Sql = Sql & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      Sql = Sql & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valInformeResumido Then
      Sql = Sql & " GROUP BY Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.StatusFactura "
   End If
   Sql = Sql & " ORDER BY Factura.StatusFactura, Factura.Moneda, Factura.Fecha, Factura.Numero"

   fSQLDeFacturasEntreFechasPorTipoDeArticulo = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeFacturasEntreFechasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Sql = "SELECT "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         Sql = Sql & "Factura.Moneda, "
   End If
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "1 AS ORDEN, "
   Sql = Sql & valCampoAMostrar & " as Numero, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   Sql = Sql & "Factura.TipoDeDocumento, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                               gUtilSQL.fSimpleSqlValue(""), True) & " AS StatusFactura, "
   Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", "Factura.TotalFactura", True) & " AS MontoOriginal, "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalIva", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalBaseImponible", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalBaseImponible, "
'EXENTO
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalMontoExento", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalMontoExento, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalFactura, "
   Else
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "Factura.TotalIVA", True) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "Factura.TotalBaseImponible", True) & " AS TotalBaseImponible, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "Factura.TotalMontoExento", True) & " AS TotalMontoExento, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   Sql = Sql & "0 AS OtrosCargos, "
   Sql = Sql & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      Sql = Sql & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      Sql = Sql & " AS NumeroComprobante"
   End If
      Sql = Sql & ", Factura.NumeroComprobanteFiscal AS ComprobanteFiscal "
   Sql = Sql & " FROM Factura INNER JOIN "
   Sql = Sql & " Cliente ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " AND Factura.CodigoCliente = Cliente.Codigo"
   Sql = Sql & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   Sql = Sql & " AND factura.CodigoVendedor = Vendedor.Codigo"
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   If valInformeEsDeBorradores Then
      Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltroInforme <> "" Then
      Sql = Sql & " AND " & valFiltroInforme
   End If
    If valUsarCiudad Then
      Sql = Sql & " AND Cliente.Ciudad = " & gUtilSQL.fSimpleSqlValue(valCiudad)
   End If
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valMostrarDocumentosGeneradosconIVAal10 Then
      Sql = Sql & " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   End If
   If valAgrupaInforme Then
      Sql = Sql & " ORDER BY Moneda, Vendedor, TipoDeDocumentoStr, Fecha, " & valCampoAMostrar
   ElseIf valAgrupaInformeXCliente Then
      Sql = Sql & " ORDER BY Moneda, Cliente.Nombre, TipoDeDocumentoStr, Fecha, " & valCampoAMostrar
   Else
      Sql = Sql & " ORDER BY Moneda, TipoDeDocumentoStr, Fecha, " & valCampoAMostrar
   End If
   fSQLDeFacturasEntreFechas = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQGenerarCuentaContable(ByVal valConsecutivoCompania As String, _
                                           ByVal valFechaInicial As Date, _
                                            ByVal valFechaFinal As Date) As String
   Dim Sql As String
   Dim SqlPeriodo As String
   Sql = Sql & "SELECT Periodo.ConsecutivoPeriodo"
   Sql = Sql & " FROM Periodo "
   Sql = Sql & " WHERE Periodo.ConsecutivoCompania = " & valConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(Sql, False)

   Sql = "(SELECT Comprobante.Numero "
   Sql = Sql & " FROM Comprobante "
   Sql = Sql & " WHERE Comprobante.NoDocumentoOrigen = Factura.TipoDeDocumento "
   Sql = Sql & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & " Factura.Numero "
   Sql = Sql & " AND Comprobante.GeneradoPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_FACTURA))
   Sql = Sql & " AND Comprobante.ConsecutivoPeriodo IN (" & SqlPeriodo & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   fSQGenerarCuentaContable = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQGenerarCuentaContable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
   Dim SqlDelReporte As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Sql = "SELECT "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         Sql = Sql & "Factura.Moneda, "
   End If
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "2 AS ORDEN, "
   Sql = Sql & valCampoAMostrar & " as Numero, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   Sql = Sql & "Factura.TipoDeDocumento, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   Sql = Sql & "'' AS StatusFactura, "
   Sql = Sql & "0 AS MontoOriginal, "
   Sql = Sql & "0 AS TotalIVA, "
   Sql = Sql & "0 AS TotalBaseImponible, "
   Sql = Sql & "0 AS TotalMontoExento, "
   Sql = Sql & "0 AS TotalFactura, "
   Sql = Sql & "SUM (RenglonDetalleDeOtrosCargosFactura.TotalRenglon) AS OtrosCargos, "
   Sql = Sql & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      Sql = Sql & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      Sql = Sql & " AS NumeroComprobante"
   End If
   Sql = Sql & ", " & gUtilSQL.fSimpleSqlValue("") & " AS ComprobanteFiscal"
   Sql = Sql & " FROM (Cliente INNER JOIN "
   Sql = Sql & " Factura ON (Cliente.Codigo = Factura.CodigoCliente)"
   Sql = Sql & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN RenglonDetalleDeOtrosCargosFactura ON "
   Sql = Sql & " (Factura.TipoDeDocumento = RenglonDetalleDeOtrosCargosFactura.TipoDeDocumento)"
   Sql = Sql & " AND (Factura.Numero = RenglonDetalleDeOtrosCargosFactura.NumeroFactura)"
   Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonDetalleDeOtrosCargosFactura.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   Sql = Sql & " AND factura.CodigoVendedor = Vendedor.Codigo "
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " AND (renglonDetalleDeOtrosCargosFactura.ComoAplicaAlTotalFactura <> " & gUtilSQL.fSimpleSqlValue(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO) & ")"
   If valInformeEsDeBorradores Then
      Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltroInforme <> "" Then
      Sql = Sql & " AND " & valFiltroInforme
   End If
   Sql = Sql & " GROUP BY "
   If Not valReporteEnMonedaLocal Then
      Sql = Sql & "Factura.Moneda, "
   End If
      Sql = Sql & "Factura.Fecha, Factura.Numero, " & valCampoAMostrar & ", Factura.TipoDeDocumento, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Vendedor.Nombre"

   fSQLGenerarOtrosCargosyDescuentos = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarOtrosCargosyDescuentos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
         "fSQLGenerarDireccionDeDespachoXFactura", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLInformeFacturacionPorDia(ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object, _
                                              ByVal vReporteEnMonedaLocal As Boolean, ByVal valConsecutivoCompania As String, _
                                               ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                ByVal valMostrarDocumentosGeneradosconIVAal10 As Boolean) As String
   Dim Sql As String
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
   Sql = "SET DATEFIRST 7 " & vbCrLf
   Sql = Sql & "SELECT "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "(CASE DATEPART(dw, Factura.Fecha) WHEN 1 THEN 'Domingo' WHEN 2 THEN 'Lunes' WHEN 3 THEN 'Martes' WHEN 4 THEN 'Miércoles' WHEN 5 THEN 'Jueves' WHEN 6 THEN 'Viernes' WHEN 7 THEN 'Sábado' ELSE 'Domingo' END) AS DiaDeLaSemanaStr, "
   Sql = Sql & sqlMonedReporte & " AS MonedaDelReporte, "
   Sql = Sql & "Factura.Moneda, "
   Sql = Sql & sqlCambio & " AS CambioABolivares, "
   Sql = Sql & "SUM" & sqlTotalFactura & " AS MontoTotal, "
   Sql = Sql & "COUNT(Factura.Numero) AS ContadorFacturas, "
   Sql = Sql & "MAX" & sqlTotalFactura & " As FacturaMayor "
   Sql = Sql & "FROM Factura "
   Sql = Sql & "WHERE (Factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
   Sql = Sql & " AND (" & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & ")"
   Sql = Sql & " AND (StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valMostrarDocumentosGeneradosconIVAal10 Then
      Sql = Sql & " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   End If
   Sql = Sql & " GROUP BY Moneda, Fecha, CambioABolivares "
   Sql = Sql & " ORDER BY MonedaDelReporte, Fecha "
   fSQLInformeFacturacionPorDia = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInformeFacturacionPorDia", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturaInformeDeGananciasYPerdidas(ByVal valcodigoDeVendedor As String, ByVal valCantidadAImprimirVendedor As Object, ByVal valPorcentajeDescuentoTC As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal valNombreMonedaLocal As String, ByVal valVendedor As Enum_Vendedores_Por_Renglon, ByRef refRenglonComicionesDeVendedor As Object, ByRef refRengFac As Object, ByRef refFactura As Object, ByRef refRenglonCobroDeFactura As Object, ByVal valUsaCambio As Boolean, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object) As String
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & insRengFac.GetTableName & ".ConsecutivoRenglon, "
   Sql = Sql & insRengFac.GetTableName & "." & CodigoDelRenglonDeVendedor & " AS CodigoVendedor, "
   Sql = Sql & "vendedor.Nombre AS NombreVendedor, "
   Sql = Sql & "Factura.Fecha, "
   factura = "Factura.Numero "
   Sql = Sql & factura & ", "
   Sql = Sql & "articuloInventario.Codigo, "
   servicio = fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(valUsaCambio, False, gUltimaTasaDeCambio, gMonedaLocalActual)
   Sql = Sql & servicio & " AS TotalRenglon, "
   CostoProduc = gUtilSQL.getIIF("articuloInventario.TipoDeArticulo = " & gConvert.enumerativoAChar(enum_TipoDeArticulo.eTD_MERCANCIA), "0", "articuloInventario.CostoUnitario * RenglonFactura.Cantidad")
   Sql = Sql & CostoProduc & " AS CostoProducto, "
   PorcentajeVendedor = "RenglonComisionesDeVendedor.Porcentaje"
   Sql = Sql & PorcentajeVendedor & " AS PorcentajeParticipacion, "
   MontoVendedor = "(" & "RenglonComisionesDeVendedor.Monto * RenglonFactura.Cantidad )"
   refMontoParticipacion = gUtilSQL.getIIF("RenglonFactura.CodigoVendedor2 <> '' ", _
               "(" & fValorSINAyudanteSQLInformeGananciasYPerdidas(servicio, MontoVendedor) & ")", _
                  "(" & fValorSINAyudanteSQLInformeGananciasYPerdidas(servicio, MontoVendedor) & ")")
   refAyudante = gUtilSQL.getIIF("RenglonFactura." & CodigoDelRenglonDeVendedorAyudante & " <> '' ", _
                  "RenglonFactura." & CodigoDelRenglonDeVendedorAyudante & gUtilSQL.CharConcat() & gUtilSQL.fSimpleSqlValue(" - AYUD "), _
                  "RenglonFactura." & CodigoDelRenglonDeVendedorAyudante, True)
   Sql = Sql & refMontoParticipacion & " AS Participacion, "
   Sql = Sql & refAyudante & " AS Asociado, "
   Tarjeta = fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito(factura, valConsecutivoCompania, valNombreMonedaLocal)
   prorrateo = "(" & Tarjeta & ") / (Factura.TotalFactura)"
   DescuentoTC = "(( RenglonFactura.TotalRenglon ) * (" & PorcentajeDeCobroDeTarjeta & "/100.00)) * (" & prorrateo & ")"
   Sql = Sql & DescuentoTC & " AS MontoDescuentoTC, "
   valParticipacionNeta = "(" & refMontoParticipacion & ") - (" & DescuentoTC & ")"
   Sql = Sql & valParticipacionNeta & " AS ParticipacionNeta, "
   Sql = Sql & "(" & valParticipacionNeta & ") - (" & CostoProduc & ") AS MontoNeto, "
   Sql = Sql & fSQLRetirosACuenta(valcodigoDeVendedor, valConsecutivoCompania, valVendedor, valFechaInicial, valFechaFinal, valCantidadAImprimirVendedor) & " AS TOTALRETIROSACTA "
   Sql = Sql & " FROM " & "articuloInventario INNER JOIN (RenglonComisionesDeVendedor"
   Sql = Sql & " INNER JOIN ((vendedor"
   Sql = Sql & " INNER JOIN Factura"
   Sql = Sql & " ON(" & "vendedor.ConsecutivoCompania"
   Sql = Sql & " = Factura.ConsecutivoCompania ))"
   Sql = Sql & " INNER JOIN RenglonFactura"
   Sql = Sql & " ON (Factura.TipoDeDocumento"
   Sql = Sql & " = RenglonFactura.TipoDeDocumento)"
   Sql = Sql & " AND (Factura.Numero"
   Sql = Sql & " = RenglonFactura.NumeroFactura)"
   Sql = Sql & " AND (Factura.ConsecutivoCompania"
   Sql = Sql & " = RenglonFactura.ConsecutivoCompania)) "
   Sql = Sql & " ON (" & "RenglonComisionesDeVendedor.CodigoVendedor"
   Sql = Sql & " = " & "vendedor.Codigo)"
   Sql = Sql & " AND (" & "RenglonComisionesDeVendedor.ConsecutivoCompania"
   Sql = Sql & " = " & "vendedor.ConsecutivoCompania))"
   Sql = Sql & " ON (" & "articuloInventario.Codigo"
   Sql = Sql & " = RenglonFactura.Articulo)"
   Sql = Sql & " AND (" & "articuloInventario.ConsecutivoCompania"
   Sql = Sql & " = RenglonFactura.ConsecutivoCompania )"
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND RenglonFactura." & CodigoDelRenglonDeVendedor & " = " & "vendedor.Codigo"
   Sql = Sql & " AND RenglonFactura.Articulo = " & "articuloInventario.Codigo"
   Sql = Sql & " AND " & "articuloInventario.LineaDeProducto = " & "RenglonComisionesDeVendedor.NombreDeLineaDeProducto"
   Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valCantidadAImprimirVendedor <> gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
       Sql = Sql & " AND RenglonFactura." & CodigoDelRenglonDeVendedor & " = " & _
           gUtilSQL.fSimpleSqlValue(Trim(valcodigoDeVendedor))
   End If
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteFacturaInformeDeGananciasYPerdidas = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteFacturaInformeDeGananciasYPerdidas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
         "fValorSINAyudanteSQLInformeGananciasYPerdidas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFacturacionPorVendedor(ByVal valReporteEnMonedaLocal As Boolean, ByVal gMonedaLocalActual As Object, _
                                             ByVal valConsecutivoCompania As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                              ByVal valInformeSoloUnVendedor As Boolean, ByVal valNombreDeVendedor As String, _
                                               ByVal valComisiones As String) As String
   Dim Sql As String
   Dim valMontoIva As String
   Dim valMontoDesc As String
   Dim valSqlTotalFactura As String
   On Error GoTo h_ERROR
   If valComisiones Then
      valSqlTotalFactura = "factura.TotalMontoExento + factura.TotalIVA + factura.TotalBaseImponible"
      Sql = "SELECT "
      Sql = Sql & "vendedor.Nombre as NombreVendedor, "
      Sql = Sql & "vendedor.Codigo AS CodigoVendedor, "
      Sql = Sql & "cliente.Nombre AS NombreCliente, "
      Sql = Sql & "cliente.Codigo AS CodigoCliente, "
      Sql = Sql & "factura.Fecha, "
      Sql = Sql & "factura.Numero, "
      Sql = Sql & "factura.TipoDeDocumento, "
      Sql = Sql & "factura.StatusFactura, "
      If valReporteEnMonedaLocal Then
         'SQL = SQL & "(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalFactura", "Factura.Fecha") & " * factura.CambioABolivares) AS TotalFact, "
         'SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalFactura", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalFact, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valSqlTotalFactura, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalFact, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("factura.TotalFactura", 2) & " AS MontoOriginal, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalMontoExento", "Factura.Fecha") & " *factura.CambioABolivares", 2) & " AS TotalMontoExent, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalIVA", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalesIVA, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalBaseImponible", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalBaseImp, "
      Else
         Sql = Sql & gUtilSQL.fRoundNDecimales(valSqlTotalFactura, 2) & " AS TotalFact, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("factura.TotalFactura", 2) & " AS MontoOriginal, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("factura.TotalMontoExento", 2) & " AS TotalMontoExent, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("factura.TotalIVA", 2) & " AS TotalesIVA, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("factura.TotalBaseImponible", 2) & " AS TotalBaseImp, "
      End If
      Sql = Sql & "factura.Moneda, "
      Sql = Sql & "factura.CambioABolivares"
      Sql = Sql & " FROM cliente INNER JOIN (vendedor INNER JOIN factura"
      Sql = Sql & " ON (vendedor.Codigo = factura.CodigoVendedor)"
      Sql = Sql & " AND (vendedor.ConsecutivoCompania = factura.ConsecutivoCompania))"
      Sql = Sql & " ON (cliente.Codigo = factura.CodigoCliente)"
      Sql = Sql & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)"
      Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
      Sql = Sql & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      Sql = Sql & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      If valInformeSoloUnVendedor Then
         Sql = Sql & " AND vendedor.Nombre = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreDeVendedor))
      End If
      Sql = Sql & " ORDER BY Vendedor.Nombre, factura.moneda, factura.Fecha "
   Else
      valMontoDesc = "((IGV_VendedorXRenglonFactura.TotalRenglon * factura.PorcentajeDescuento) / 100)"
      valMontoIva = "(((IGV_VendedorXRenglonFactura.TotalRenglon -" & valMontoDesc & " ) * factura.PorcentajeAlicuota1) / 100)"
      valMontoDesc = gUtilSQL.fRoundNDecimales(valMontoDesc, 2)
      valMontoIva = gUtilSQL.fRoundNDecimales(valMontoIva, 2)
      Sql = "SELECT "
      Sql = Sql & "IGV_VendedorXRenglonFactura.NombreVendedor as NombreVendedor, "
      Sql = Sql & "IGV_VendedorXRenglonFactura.CodigoVendedor AS CodigoVendedor, "
      Sql = Sql & "cliente.Nombre AS NombreCliente, "
      Sql = Sql & "cliente.Codigo AS CodigoCliente, "
      Sql = Sql & "factura.Fecha, "
      Sql = Sql & "factura.Numero, "
      Sql = Sql & "factura.TipoDeDocumento, "
      Sql = Sql & "factura.StatusFactura, "
      If valReporteEnMonedaLocal Then
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalFact, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, 2) & " AS MontoOriginal, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "factura.TotalMontoExento", "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalMontoExent, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoIva, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalesIVA, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc, "Factura.Fecha") & " * factura.CambioABolivares", 2) & " AS TotalBaseImp, "
      Else
         Sql = Sql & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, 2) & " AS TotalFact, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc & "+" & valMontoIva, 2) & " AS MontoOriginal, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("factura.TotalMontoExento", 2) & " AS TotalMontoExent, "
         Sql = Sql & gUtilSQL.fRoundNDecimales(valMontoIva, 2) & " AS TotalesIVA, "
         Sql = Sql & gUtilSQL.fRoundNDecimales("IGV_VendedorXRenglonFactura.TotalRenglon" & "-" & valMontoDesc, 2) & " AS TotalBaseImp, "
      End If
      Sql = Sql & "factura.Moneda, "
      Sql = Sql & "factura.CambioABolivares"
      Sql = Sql & " FROM cliente INNER JOIN (IGV_VendedorXRenglonFactura INNER JOIN factura"
      Sql = Sql & " ON (IGV_VendedorXRenglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania)"
      Sql = Sql & " AND IGV_VendedorXRenglonFactura.NumeroFactura = factura.Numero "
      Sql = Sql & " AND IGV_VendedorXRenglonFactura.TipodeDocumento = factura.tipodedocumento )"
       Sql = Sql & " ON (cliente.Codigo = factura.CodigoCliente)"
      Sql = Sql & " AND (cliente.ConsecutivoCompania = factura.ConsecutivoCompania)"
      Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
      Sql = Sql & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      Sql = Sql & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      If valInformeSoloUnVendedor Then
         Sql = Sql & " AND IGV_VendedorXRenglonFactura.NombreVendedor = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreDeVendedor))
      End If
      Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
      Sql = Sql & " ORDER BY IGV_VendedorXRenglonFactura.NombreVendedor, factura.moneda, factura.Fecha "
   End If
h_EXIT: On Error GoTo 0
   fSQLFacturacionPorVendedor = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFacturacionPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLInformeDeFacturacionPorCliente(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                                                    ByVal valMostrarRenglones As Boolean, ByVal valUsaMonedaExtYLaMonedaDelReporteEnBs As Boolean, _
                                                     ByVal valImprimirVersion As Boolean, ByVal valImprimirFacturasAnuladas As Boolean, _
                                                      ByVal valMostrarLineaDeProducto As Boolean, ByVal valLineaDeProducto As String, _
                                                       ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, _
                                                        ByVal valConsecutivoCompania As String, ByVal valImprimirSoloUnCliente As Boolean) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Sql = "SELECT "
   Sql = Sql & "factura.CodigoCliente, "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & "factura.Fecha, "
   Sql = Sql & "factura.Numero, "
   Sql = Sql & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
               gUtilSQL.fSimpleSqlValue(""), True) & " AS StatusFactura, "
   If valMostrarRenglones Then
      If valMostrarLineaDeProducto Then
         Sql = Sql & "articuloInventario.LineaDeProducto, "
      End If
      Sql = Sql & "renglonFactura.Articulo, "
      Sql = Sql & "renglonFactura.PorcentajeDescuento, "
   End If
   If valUsaMonedaExtYLaMonedaDelReporteEnBs Then
      If valMostrarRenglones Then
         Sql = Sql & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(True, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonSinIva, "
         Sql = Sql & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(True, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonConIva, "
      End If
      Sql = Sql & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("factura.CambioABolivares", "factura.Moneda", _
                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", "Factura.Fecha"), True, ""), True) & _
                  " AS TotalFactura, "
   Else
      If valMostrarRenglones Then
         Sql = Sql & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(False, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonSinIva, "
         Sql = Sql & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                     fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(False, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonConIva, "
      End If
      Sql = Sql & gUtilSQL.getIIF("factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   If valImprimirVersion Then
      Sql = Sql & "articuloInventario.NombrePrograma, "
      Sql = Sql & "(SELECT MAX(versionPrograma.versionprograma) " & _
                  " FROM versionPrograma " & _
                  " WHERE versionPrograma.Fecha <= Factura.Fecha " & _
                  " AND versionPrograma.NombrePrograma = articuloInventario.NombrePrograma) " & _
                  " AS versionPrograma, "
   End If
   If valMostrarRenglones Then
      Sql = Sql & "renglonFactura.Cantidad, "
   End If
   If valUsaMonedaExtYLaMonedaDelReporteEnBs Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS MonedaReporte, "
   Else
      Sql = Sql & "factura.Moneda AS MonedaReporte, "
   End If
   Sql = Sql & "factura.Moneda AS MonedaDocumento, "
   Sql = Sql & "factura.CambioABolivares, "
   Sql = Sql & "factura.TipoDeDocumento, "
   Sql = Sql & "factura.TipoDeDocumento" & gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat & "factura.Numero AS CorteDeGrupo"
   If valImprimirVersion Then
      Sql = Sql & " FROM Cliente INNER JOIN (factura INNER JOIN (articuloInventario INNER JOIN renglonFactura "
      Sql = Sql & " ON (articuloInventario.Codigo = renglonFactura.Articulo) "
      Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
      Sql = Sql & " ON (factura.Numero = renglonFactura.NumeroFactura)"
      Sql = Sql & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
      Sql = Sql & " AND (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento))"
      Sql = Sql & " ON (Cliente.Codigo = factura.CodigoCliente)"
      Sql = Sql & " AND (Cliente.ConsecutivoCompania = factura.ConsecutivoCompania)"
   Else
      If valMostrarRenglones Then
         Sql = Sql & " FROM (Cliente INNER JOIN factura "
         Sql = Sql & " ON (Cliente.Codigo = factura.CodigoCliente)"
         Sql = Sql & " AND (Cliente.ConsecutivoCompania = factura.ConsecutivoCompania))"
         Sql = Sql & " INNER JOIN renglonFactura"
         Sql = Sql & " ON (factura.Numero = renglonFactura.NumeroFactura)"
         Sql = Sql & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
         Sql = Sql & " AND (factura.TipoDeDocumento = renglonFactura.TipoDeDocumento)"
         If valMostrarLineaDeProducto Then
            Sql = Sql & " INNER JOIN ArticuloInventario "
            Sql = Sql & " ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania "
            Sql = Sql & " AND renglonFactura.Articulo = ArticuloInventario.Codigo "
         End If
      Else
         Sql = Sql & " FROM Cliente INNER JOIN factura "
         Sql = Sql & " ON Cliente.ConsecutivoCompania = factura.ConsecutivoCompania "
         Sql = Sql & " AND Cliente.Codigo = factura.CodigoCliente "
      End If
   End If
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   If valImprimirSoloUnCliente Then
      Sql = Sql & " AND factura.CodigoCliente = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   If valImprimirFacturasAnuladas Then
      Sql = Sql & " AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR (factura.StatusFactura  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & "))"
   Else
      Sql = Sql & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   End If
   If valMostrarLineaDeProducto Then
      Sql = Sql & " AND (ArticuloInventario.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(valLineaDeProducto) & ")"
   End If
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   Sql = Sql & " ORDER BY "
   Sql = Sql & "factura.Moneda, factura.Fecha, "
   Sql = Sql & "factura.TipoDeDocumento" & gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat & "factura.Numero, "
   Sql = Sql & "factura.TipoDeDocumento, "
   Sql = Sql & "factura.Numero"
   Set gEnumProyecto = Nothing
h_EXIT:
   fSQLInformeDeFacturacionPorCliente = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLInformeDeFacturacionPorCliente", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito(ByVal factura As String, ByVal valConsecutivoCompania As Long, ByVal valNombreMonedaLocal As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "SUM (" & gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_TARJETA), "renglonCobroDeFactura.Monto", _
               0) & ")"
   Sql = Sql & " FROM formaDelCobro INNER JOIN "
   Sql = Sql & "(" & insFactura.GetTableName
   Sql = Sql & " As FacturaInner INNER JOIN renglonCobroDeFactura ON "
   Sql = Sql & " (FacturaInner.TipoDeDocumento" & _
               " = " & "renglonCobroDeFactura.TipoDeDocumento)"
   Sql = Sql & " AND (FacturaInner.Numero" & _
              " = " & "renglonCobroDeFactura.NumeroFactura)"
   Sql = Sql & " AND (FacturaInner.ConsecutivoCompania" & _
              " = " & "renglonCobroDeFactura.ConsecutivoCompania)) ON "
   Sql = Sql & "formaDelCobro." & "Codigo" & _
              " = " & "renglonCobroDeFactura.CodigoFormaDelCobro"
   Sql = Sql & " WHERE ( FacturaInner.Numero" & _
               " = " & factura & ")"
   Sql = Sql & " AND (FacturaInner.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania & ")"
  fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLCalculaElDescuentoConProrrateoDeTargetaDeCredito", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLRetirosACuenta(ByVal CodigoVendedor As String, ByVal valConsecutivoCompania As Long, ByVal valVendedor As Enum_Vendedores_Por_Renglon, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCantidadAImprimirVendedor As Object) As String

   Dim Sql As String
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
   Sql = "(SELECT SUM(" & "RetirosACuenta.Monto)"
   Sql = Sql & " FROM RetirosACuenta INNER JOIN vendedor ON RetirosACuenta.ConsecutivoCompania = vendedor.ConsecutivoCompania AND  RetirosACuenta.CodigoVendedor = Vendedor.codigo"
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("RetirosACuenta.Fecha", valFechaInicial, valFechaFinal) & ")"
   If valCantidadAImprimirVendedor = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND (" & "RetirosACuenta.CodigoVendedor = " & CodigoVendedor & ")"
   End If
   Sql = Sql & " AND (" & "vendedor.Codigo = " & CodigoVendedor & ")"
   Sql = Sql & " AND (" & "RetirosACuenta.ConsecutivoCompania = " & valConsecutivoCompania & "))"
   Set gEnumReport = Nothing
h_EXIT:    On Error GoTo 0
   fSQLRetirosACuenta = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLRetirosACuenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLVentasMensualesPorCategoria(ByVal valFacturaTableName As String, ByVal valAno As String, ByVal valTipoDeProducto As Integer, ByVal valConsecutivoCompania As String, ByRef insRenglonFactura As Object, ByRef insFactura As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   insRenglonFactura.setClaseDeTrabajo insFactura.getClaseDeTrabajo
   Sql = "SELECT " & gUtilSQL.DfSQLMonthOfDate(insFactura.GetTableName & "." & insFactura.getFN_FECHA, "Mes") & ", "
   Sql = Sql & "SUM(" & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CANTIDAD & ") AS Cant, "
   Sql = Sql & "articuloInventario.Categoria"
   Sql = Sql & " FROM " & insFactura.GetTableName & " INNER JOIN ("
   Sql = Sql & "articuloInventario INNER JOIN " & insRenglonFactura.GetTableName
   Sql = Sql & " ON (" & "articuloInventario.Codigo = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO
   Sql = Sql & ") AND (" & "articuloInventario.ConsecutivoCompania = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ")) ON (" & valFacturaTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_TIPO_DE_DOCUMENTO
   Sql = Sql & ") AND (" & valFacturaTableName & "." & insFactura.getFN_NUMERO & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_NUMERO_FACTURA
   Sql = Sql & ") AND (" & valFacturaTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA & ")   "
   Sql = Sql & " WHERE " & valFacturaTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & valConsecutivoCompania
   Sql = Sql & " AND " & "articuloInventario.TipoDeProducto = " & gUtilSQL.fSimpleSqlValue(valTipoDeProducto) & ""
   Sql = Sql & " AND " & gUtilSQL.DfSQLYearOfDate(valFacturaTableName & "." & insFactura.getFN_FECHA) & " = " & gConvert.fConvertStringToLong(Trim(valAno))
   Sql = Sql & " AND " & valFacturaTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND " & valFacturaTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " GROUP BY " & "articuloInventario.Categoria, "
   Sql = Sql & gUtilSQL.DfSQLMonthOfDate(valFacturaTableName & "." & insFactura.getFN_FECHA)
   Sql = Sql & " ORDER BY " & "articuloInventario.Categoria"
h_EXIT: On Error GoTo 0
   fSQLVentasMensualesPorCategoria = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLVentasMensualesPorCategoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLProductosVendidosPorZona(ByVal valSoloEntreFechas As Boolean, ByVal valConsecutivoCompania As Long, ByVal valZonaDesde As String, ByVal valZonaHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insFactura As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & "cliente.Contacto, "
   Sql = Sql & "cliente.Direccion, "
   Sql = Sql & "cliente.Telefono, "
   Sql = Sql & "cliente.ZonaPostal, "
   Sql = Sql & "cliente.Codigo, "
   Sql = Sql & "cliente.Ciudad"
   If valSoloEntreFechas = True Then
      Sql = Sql & ", MIN(" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & ")"
      Sql = Sql & " FROM " & "cliente INNER JOIN " & insFactura.GetTableName
      Sql = Sql & " ON (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                  " = " & "cliente.ConsecutivoCompania"
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & _
                  " = " & "cliente.Codigo)"
   Else
      Sql = Sql & " FROM cliente"
   End If
   Sql = Sql & " WHERE " & "cliente.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween("cliente.ZonaPostal", gConvert.fConvierteAString(Trim(valZonaDesde)), gConvert.fConvierteAString(Trim(valZonaHasta)))
   If valSoloEntreFechas = True Then
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaInicial, valFechaFinal)
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
      Sql = Sql & " GROUP BY "
      Sql = Sql & "cliente.Ciudad, "
      Sql = Sql & "cliente.Nombre, "
      Sql = Sql & "cliente.Contacto, "
      Sql = Sql & "cliente.Direccion, "
      Sql = Sql & "cliente.Telefono, "
      Sql = Sql & "cliente.ZonaPostal, "
      Sql = Sql & "cliente.Codigo"
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "cliente.ZonaPostal, "
   Sql = Sql & "cliente.Nombre"
h_EXIT: On Error GoTo 0
   fSQLProductosVendidosPorZona = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sEjecutaElInformeProductosPorZona", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLProductosVendidosPorCiudad(ByVal valSoloEntreFechas As Boolean, ByVal valConsecutivoCompania As Long, ByVal valCiudadDesde As String, ByVal valCiudadHasta As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insFactura As Object, ByVal valUnVendedor As Boolean, ByVal valcodigoVendedor As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & "cliente.Contacto, "
   Sql = Sql & "cliente.Direccion, "
   Sql = Sql & "cliente.Telefono, "
   Sql = Sql & "cliente.ZonaPostal, "
   Sql = Sql & "cliente.Codigo, "
   Sql = Sql & "cliente.Ciudad"
   If valSoloEntreFechas Then
      Sql = Sql & ", Factura.CodigoVendedor AS CodigoVendedor ,"
      Sql = Sql & "vendedor.Nombre AS NombreVendedor, "
      Sql = Sql & gUtilSQL.fCast("cliente.Ciudad", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fCast("Factura.CodigoVendedor", eTDSS_VARCHAR, "") & " AS CiudadVendedor"
      Sql = Sql & ", MIN(" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & ")"
      Sql = Sql & " FROM " & "cliente INNER JOIN " & insFactura.GetTableName
      Sql = Sql & " ON " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                  " = " & "cliente.ConsecutivoCompania"
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & _
                  " = " & "cliente.Codigo"
      Sql = Sql & " Inner Join vendedor ON factura.ConsecutivoCompania = vendedor.ConsecutivoCompania AND factura.CodigoVendedor = vendedor.Codigo"
   Else
      Sql = Sql & " FROM cliente"
   End If
   Sql = Sql & " WHERE "
   Sql = Sql & "cliente.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween("cliente.Ciudad", gConvert.fConvierteAString(Trim(valCiudadDesde)), gConvert.fConvierteAString(Trim(valCiudadHasta)))
   If valSoloEntreFechas Then
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, valFechaInicial, valFechaFinal)
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
      If valUnVendedor Then
         Sql = gUtilSQL.fSQLValueWithAnd(Sql, "factura.CodigoVendedor", valcodigoVendedor, False)
      End If
      Sql = Sql & " GROUP BY "
      Sql = Sql & "cliente.Ciudad, "
      Sql = Sql & "cliente.Nombre, "
      Sql = Sql & "cliente.Contacto, "
      Sql = Sql & "cliente.Direccion, "
      Sql = Sql & "cliente.Telefono, "
      Sql = Sql & "cliente.ZonaPostal, "
      Sql = Sql & "cliente.Codigo, "
      Sql = Sql & "Factura.CodigoVendedor ,"
      Sql = Sql & "vendedor.Nombre"
   End If
   If valSoloEntreFechas Then
      Sql = Sql & " ORDER BY " & "Factura.CodigoVendedor , cliente.Ciudad,"
   Else
      Sql = Sql & " ORDER BY " & "cliente.Ciudad,"
   End If
   Sql = Sql & "cliente.Nombre"
h_EXIT: On Error GoTo 0
   fSQLProductosVendidosPorCiudad = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLProductosVendidosPorCiudad", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteEstadisticoDeVentas(ByVal valPorTrimestre As Boolean, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valAno As String, ByVal valConsecutivoCompania As Long, ByVal valEsSistemaParaIG As Boolean, ByVal valPorTipoDeProducto As Boolean, ByVal usaCambioABs As Boolean, ByVal valHoyNombreMoneda As String, ByRef insFact As Object, ByRef insRengFact As Object, ByVal gEnumProyecto As Object) As String
   Dim Sql As String
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
   Sql = "SELECT "
   If usaCambioABs Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(valHoyNombreMoneda) & " AS MonedaReporte, "
   Else
      Sql = Sql & "factura.Moneda AS MonedaReporte, "
   End If
   If valPorTrimestre Then
      Sql = Sql & sqlTrimestre & " AS Trimestre, "
   End If
   If valEsSistemaParaIG Then
      Sql = Sql & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoCategoria, "
   Else
      Sql = Sql & "articuloInventario.Categoria AS TipoCategoria, "
      If valEsSistemaParaIG Then
         Sql = Sql & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoDeProd, "
      End If
   End If
    
   Sql = Sql & "SUM(" & insRengFact.GetTableName & "." & insRengFact.getFN_CANTIDAD & ") AS Cantidad, "
   Sql = Sql & "SUM(" & insRengFact.fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usaCambioABs, False) & ") AS MontoConIva, "
   Sql = Sql & "SUM(" & insRengFact.fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(usaCambioABs, False) & ") AS MontoSinIva, "
   Sql = Sql & "SUM(" & insRengFact.fSQLMontoTotalDelRenglonDeFactura(usaCambioABs, False) & ") AS MontoBrutoFactura "
   Sql = Sql & " FROM " & insFact.GetTableName & " INNER JOIN ("
   Sql = Sql & "articuloInventario INNER JOIN "
   Sql = Sql & insRengFact.GetTableName & " ON ("
   Sql = Sql & "articuloInventario.Codigo" & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_ARTICULO
   Sql = Sql & ") AND (" & "articuloInventario.ConsecutivoCompania" & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_CONSECUTIVO_COMPANIA
   Sql = Sql & ")) ON (" & insFact.GetTableName & "." & insFact.getFN_NUMERO & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_NUMERO_FACTURA
   Sql = Sql & ") AND (" & insFact.GetTableName & "." & insFact.getFN_TIPO_DE_DOCUMENTO & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_TIPO_DE_DOCUMENTO
   Sql = Sql & ") AND (" & insFact.GetTableName & "." & insFact.getFN_CONSECUTIVO_COMPANIA & _
               " = " & insRengFact.GetTableName & "." & insRengFact.getFN_CONSECUTIVO_COMPANIA & ")"
   Sql = Sql & " WHERE " & insFact.GetTableName & "." & insFact.getFN_CONSECUTIVO_COMPANIA & " = " & valConsecutivoCompania
   Sql = Sql & " AND " & insFact.GetTableName & "." & insFact.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND " & insFact.GetTableName & "." & insFact.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND " & insFact.GetTableName & "." & insFact.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   If valPorTrimestre Then
      Sql = Sql & " AND " & gUtilSQL.fYear(sqlFecha, "") & " = " & valAno
      Sql = Sql & " GROUP BY " & sqlTrimestre & ", "
   Else
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(sqlFecha, valFechaInicial, valFechaFinal)
      Sql = Sql & " GROUP BY "
   End If
   If valPorTipoDeProducto Then
      Sql = Sql & "articuloInventario.TipoDeProducto"
   Else
      Sql = Sql & "articuloInventario.Categoria"
      If valEsSistemaParaIG Then
         Sql = Sql & ", " & "articuloInventario.TipoDeProducto"
      End If
   End If
   If Not usaCambioABs Then
      Sql = Sql & ", " & "factura.Moneda"
   End If
   Sql = Sql & " ORDER BY "
   If valPorTrimestre Then
      Sql = Sql & sqlTrimestre & ", "
   End If
   If valPorTipoDeProducto Then
      Sql = Sql & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto)
   Else
      Sql = Sql & "articuloInventario.Categoria"
      If valEsSistemaParaIG Then
         Sql = Sql & ", " & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto)
      End If
   End If
   If valEsSistemaParaIG And Not valPorTipoDeProducto Then
      Sql = Sql & ", " & "articuloInventario.TipoDeProducto"
   End If
h_EXIT:
   fSQLDelReporteEstadisticoDeVentas = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelReporteEstadisticoDeVentas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComparativoPorAnoPorMeses(ByVal valAnoInicial As Integer, ByVal valAnoFinal As Integer, _
                                       ByVal gMonedaLocalActual As Object, ByVal valMontoSinIva As String, _
                                           ByVal valMontoConIva As String, ByVal valFechaDocumentoOrigen As String, _
                                              ByVal valConsecutivoCompania As Long, ByVal valSistemaInterno As Boolean) As String
   Dim Sql As String
   Dim i As Integer
   On Error GoTo h_ERROR
   Sql = "SELECT "
   For i = valAnoInicial To valAnoFinal
      If valSistemaInterno Then
         Sql = Sql & gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("Fecha") & " = " & gConvert.fConvierteAString(i), "SUM(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoSinIva, valFechaDocumentoOrigen) & ")", "0", True), 2) & " AS TotalFact" & gConvert.fConvierteAString(i) & ", " 'Facturas sin Iva
      Else
         Sql = Sql & gUtilSQL.fRoundNDecimales(gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("Fecha") & " = " & gConvert.fConvierteAString(i), "SUM(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, valMontoConIva, valFechaDocumentoOrigen) & ")", "0", True), 2) & " AS TotalFact" & gConvert.fConvierteAString(i) & ", " 'Facturas con Iva
      End If
   Next
   Sql = Sql & gUtilSQL.DfSQLMonthOfDate("Fecha", "Mes") & ", "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("Fecha", "Ano")
   Sql = Sql & " FROM Factura "
   Sql = Sql & " WHERE  StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween(gUtilSQL.DfSQLYearOfDate("Fecha"), valAnoInicial, valAnoFinal)
   Sql = Sql & " AND TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " AND ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " GROUP BY "
   Sql = Sql & gUtilSQL.DfSQLMonthOfDate("Fecha") & ", "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("Fecha")
   Sql = Sql & " ORDER BY MONTH(FECHA) "
h_EXIT: On Error GoTo 0
   fSQLComparativoPorAnoPorMeses = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLComparativoPorAnoPorMeses", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComparativoPorAnoPorTipoDeProductoOCategoria(ByVal valUsaPrecioSinIvaTipoDocumento As Boolean, ByVal valSistemaInterno As Boolean, _
                                                   ByVal valAnoInicial As Integer, ByVal valAnoFinal As Integer, _
                                                      ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                                            ByVal valFechaDocumentoOrigen As String, ByVal valStatusFactura As String, _
                                                              ByVal valConsecutivoCompania As Long, ByVal valTipoDocumento As String, _
                                                                 ByVal valEstDeVentas As Boolean, ByVal gEnumProyecto As Object) As String
   Dim Sql As String
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
   Sql = "SELECT "
   For i = valAnoInicial To valAnoFinal
      Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("factura.Fecha") & " = " & gConvert.fConvierteAString(i), _
                  "SUM(" & sqlMtoTotalRenglon & ")", "0", True) & " AS TotalFact" & gConvert.fConvierteAString(i) & ", "
      Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("factura.Fecha") & " = " & gConvert.fConvierteAString(i), _
                  "SUM(renglonFactura.Cantidad)", "0", True) & " AS UnidadesVendidas" & gConvert.fConvierteAString(i) & ", "
   Next
   If valEstDeVentas Then
      Sql = Sql & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoDeProducto, "
   Else 'If optEstDeVentas(1).value Then
      Sql = Sql & "articuloInventario.Categoria, "
      If valSistemaInterno Then
         Sql = Sql & fSQLCaseIfForEnumTipoDeProductoToStringInArray(gEnumProyecto) & " AS TipoDeProducto, "
      End If
   End If
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("Fecha", "Ano")
   Sql = Sql & " FROM factura INNER JOIN (articuloInventario INNER JOIN"
   Sql = Sql & " renglonFactura ON (articuloInventario.Codigo = renglonFactura.articulo) "
   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania))"
   Sql = Sql & " ON (factura.Numero = renglonFactura.NumeroFactura) "
   Sql = Sql & " AND (factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE  factura.StatusFactura = " & valStatusFactura
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween(gUtilSQL.DfSQLYearOfDate("factura.Fecha"), valAnoInicial, valAnoFinal)
   Sql = Sql & " AND factura.TipoDeDocumento <> " & valTipoDocumento
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   If valEstDeVentas Then
      Sql = Sql & " GROUP BY articuloInventario.TipoDeProducto, "
      Sql = Sql & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
      Sql = Sql & " ORDER BY articuloInventario.TipoDeProducto"
   Else 'If optEstDeVentas(1).value Then
      Sql = Sql & " GROUP BY articuloInventario.Categoria, articuloInventario.TipoDeProducto,  "
      Sql = Sql & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
      Sql = Sql & " ORDER BY articuloInventario.Categoria, articuloInventario.TipoDeProducto, "
      Sql = Sql & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
   End If
h_EXIT: On Error GoTo 0
   fSQLComparativoPorAnoPorTipoDeProductoOCategoria = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLComparativoPorAnoPorTipoDeProductoOCategoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas(ByVal valUsarCambioABolivares As Boolean, _
                                                                           ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                            ByVal gUltimaTasaDeCambio As Object, _
                                                                             ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   Dim vTabla As String
   On Error GoTo h_ERROR
   
   Sql = "(IGV_VendedorXRenglonFactura.PrecioSinIVA * IGV_VendedorXRenglonFactura.Cantidad)"
   Sql = "(" & Sql & " * (1 - (IGV_VendedorXRenglonFactura.PorcentajeDescuento / 100))) "
   Sql = "(" & Sql & " * (1 - (Factura.PorcentajeDescuento / 100))) "
   If valUsarCambioABolivares Then
      Sql = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", Sql, True, "")
   End If
   If Not valVieneDelLibroDeVentas Then
   Sql = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, Sql, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFacturaXVistas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas(ByVal valUsarCambioABolivares As Boolean, ByVal valVieneDelLibroDeVentas As Boolean, _
                                                                            ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "((IGV_VendedorXRenglonFactura.PrecioSinIVA * IGV_VendedorXRenglonFactura.Cantidad) * (1 + (IGV_VendedorXRenglonFactura.PorcentajeAlicuota / 100)))"
   Sql = "(" & Sql & " * (1 - (IGV_VendedorXRenglonFactura.PorcentajeDescuento / 100))) "
   Sql = "(" & Sql & " * (1 - (Factura.PorcentajeDescuento / 100))) "
   If valUsarCambioABolivares Then
      Sql = "(" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", Sql, True, "") & ")"
   End If
   If Not valVieneDelLibroDeVentas Then
   Sql = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, Sql, "Factura.Fecha")
   End If
   fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFacturaXVistas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLArticulosSobreGiradosEnPedido(ByVal valConsecutivoCompania As Long, ByVal valNumeroFactura As String, ByVal valTipoDocumento As Integer) As String
   Dim vSql As String
   Dim vSqlWhere  As String
   On Error GoTo h_ERROR
   vSqlWhere = gUtilSQL.fSQLValueWithAnd("", "renglonFactura.NumeroFactura", valNumeroFactura, False)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "renglonFactura.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "renglonFactura.TipoDeDocumento", valTipoDocumento)
 
   vSql = "SELECT renglonFactura.NumeroFactura, "
   vSql = vSql & "factura.NoCotizacionDeOrigen, "
   vSql = vSql & "renglonFactura.Articulo, "
   vSql = vSql & "renglonFactura.Cantidad, "
   vSql = vSql & "renglonCotizacion.Cantidad AS CantidadPedida, "
   vSql = vSql & "ISNULL(renglonCotizacion.CantidadDespachada, 0) AS CantidadDespachada, "
   vSql = vSql & "(renglonCotizacion.Cantidad - ISNULL(renglonCotizacion.CantidadDespachada, 0)) AS Resta  "
  
   vSql = vSql & " FROM renglonFactura INNER JOIN factura "
   vSql = vSql & "   ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania "
   vSql = vSql & "   AND renglonFactura.NumeroFactura = factura.Numero "
   vSql = vSql & "   AND renglonFactura.TipoDeDocumento = factura.TipoDeDocumento "
   vSql = vSql & "   INNER JOIN cotizacion "
   vSql = vSql & "   ON factura.ConsecutivoCompania = cotizacion.ConsecutivoCompania "
   vSql = vSql & "   AND factura.NoCotizacionDeOrigen = cotizacion.Numero "
   vSql = vSql & "   INNER JOIN renglonCotizacion "
   vSql = vSql & "   ON cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania "
   vSql = vSql & "   AND cotizacion.Numero = renglonCotizacion.NumeroCotizacion "
   vSql = vSql & "   AND renglonFactura.Articulo = renglonCotizacion.CodigoArticulo "
   vSql = vSql & "   AND ISNULL(renglonFactura.Serial, '') = ISNULL(renglonCotizacion.Serial, '') "
   vSql = vSql & "   AND ISNULL(renglonFactura.Rollo,'') = ISNULL(renglonCotizacion.Rollo,'') "
   
   
   vSql = vSql & gUtilSQL.getWhereSQL(vSqlWhere)
   vSql = vSql & "GROUP BY renglonFactura.NumeroFactura, factura.NoCotizacionDeOrigen, renglonFactura.Articulo, "
   vSql = vSql & "renglonFactura.Cantidad, renglonCotizacion.Cantidad, renglonCotizacion.CantidadDespachada "
   vSql = vSql & "HAVING ((renglonCotizacion.Cantidad - ISNULL(renglonCotizacion.CantidadDespachada, 0)) < renglonFactura.Cantidad) "
   fSQLArticulosSobreGiradosEnPedido = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLArticulosSobreGiradosEnPedido", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeNotasDeCreditoEntreFechasPorVendedor(ByVal valReporteEnMonedaLocal As Boolean, _
                                           ByVal gMonedaLocalActual As Object, _
                                            ByVal gUltimaTasaDeCambio As Object, _
                                             ByVal valUsaModuloDeContabilidad As Boolean, _
                                              ByVal valConsecutivoCompania As String, _
                                               ByVal valFechaInicial As Date, _
                                                ByVal valFechaFinal As Date, _
                                                  ByVal valFiltroInforme As String) As String
                                                  
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim insFacturaView As clsFacturaVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insFacturaView = New clsFacturaVista
   
   Sql = Sql & "SELECT "
   Sql = Sql & "TablaTotal.Moneda, "
   Sql = Sql & "TablaTotal.Fecha, "
   Sql = Sql & "TablaTotal.ORDEN, "
   Sql = Sql & "TablaTotal.Numero, "
   Sql = Sql & "TablaTotal.TipoDeDocumento, "
   Sql = Sql & "TablaTotal.TipoDeDocumentoStr, "
   Sql = Sql & "TablaTotal.CodigoCliente, "
   Sql = Sql & "TablaTotal.Cliente, "
   Sql = Sql & "TablaTotal.Status, "
   Sql = Sql & "TablaTotal.MontoOriginal, "
   Sql = Sql & "TablaTotal.PorcentajeDescuento, "
   Sql = Sql & "TablaTotal.MontoDescuento, "
   Sql = Sql & "TablaTotal.MontoIVA, "
   Sql = Sql & "TablaTotal.BaseImponible, "
   Sql = Sql & "TablaTotal.MontoExento, "
   Sql = Sql & "TablaTotal.MontoTotal, "
   Sql = Sql & "TablaTotal.OtrosCargos, "
   Sql = Sql & "TablaTotal.NombreVendedor, "
   Sql = Sql & "TablaTotal.ComprobanteFiscal, "
   Sql = Sql & "TablaTotal.Comprobante "
   Sql = Sql & " FROM ("
   
   Sql = Sql & "SELECT "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
      Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda, "
   End If
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".ORDEN, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Numero, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".TipoDeDocumento, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".TipoDeDocumentoStr, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CodigoCliente, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Cliente, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoOriginal, "
   If valReporteEnMonedaLocal Then
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".PorcentajeDescuento", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS PorcentajeDescuento, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoDescuento", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoDescuento, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoIVA", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoIVA, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".BaseImponible", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS BaseImponible, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoExento", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoExento, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".CambioABolivares", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoTotal", _
                               insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Fecha"), True, ""), True) & " AS MontoTotal, "
   Else
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".PorcentajeDescuento", True) & " AS PorcentajeDescuento, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoDescuento", True) & " AS MontoDescuento, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoIVA", True) & " AS MontoIVA, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".BaseImponible", True) & " AS BaseImponible, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoExento", True) & " AS MontoExento, "
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".MontoTotal", True) & " AS MontoTotal, "
   End If
   Sql = Sql & "0 AS OtrosCargos, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".NombreVendedor, "
   Sql = Sql & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".ComprobanteFiscal, "
   If valUsaModuloDeContabilidad Then
      Sql = Sql & gUtilSQL.getIIF(fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor(), valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor(), valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), False)
      Sql = Sql & " AS Comprobante "
   Else
      Sql = Sql & gUtilSQL.fSimpleSqlValue("No Aplica") & " AS Comprobante "
   End If
   Sql = Sql & " FROM " & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor()
   Sql = Sql & " WHERE "
   Sql = Sql & gUtilSQL.DfSQLDateValueBetween(insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor & ".Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND " & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".ConsecutivoCompania = " & valConsecutivoCompania
   If valFiltroInforme <> "" Then
      Sql = Sql & " AND " & insFacturaView.GetViewNameNotasDeCreditoEntreFechasPorVendedor() & ".NombreVendedor = " & valFiltroInforme
   End If
   Sql = Sql & " UNION "
   Sql = Sql & "SELECT "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
      Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Moneda, "
   End If
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Fecha, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".ORDEN, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Numero, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".TipoDeDocumento, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".TipoDeDocumentoStr, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".CodigoCliente, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Cliente, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Status, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoOriginal, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".PorcentajeDescuento, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoDescuento, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoIVA, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".BaseImponible, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoExento, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".MontoTotal, "
   If valReporteEnMonedaLocal Then
   Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".CambioABolivares", insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Moneda", _
                               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".OtrosCargos", _
                               insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Fecha"), True, "") & " AS OtrosCargos, "
   Else
   Sql = Sql & gUtilSQL.getIIF(insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".OtrosCargos", True) & " AS OtrosCargos, "
   End If
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".NombreVendedor, "
   Sql = Sql & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".ComprobanteFiscal, "
   If valUsaModuloDeContabilidad Then
      Sql = Sql & gUtilSQL.getIIF(fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(valConsecutivoCompania, insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), False)
      Sql = Sql & " AS Comprobante "
   Else
      Sql = Sql & gUtilSQL.fSimpleSqlValue("No Aplica") & " AS Comprobante "
   End If
   Sql = Sql & " FROM " & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito()
   Sql = Sql & " WHERE "
   Sql = Sql & gUtilSQL.DfSQLDateValueBetween(insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito & ".Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND " & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".ConsecutivoCompania = " & valConsecutivoCompania
   If valFiltroInforme <> "" Then
      Sql = Sql & "AND " & insFacturaView.GetViewNameGenerarOtrosCargosyDescuentosNotasDeCredito() & ".NombreVendedor = " & valFiltroInforme
   End If
   Sql = Sql & ") AS TablaTotal"
   Sql = Sql & " ORDER BY "
   Sql = Sql & "TablaTotal.Moneda,TablaTotal.NombreVendedor,TablaTotal.TipoDeDocumento,TablaTotal.Fecha,TablaTotal.Numero,TablaTotal.ORDEN"
   Set gEnumProyecto = Nothing
   Set insFacturaView = Nothing
   fSQLDeNotasDeCreditoEntreFechasPorVendedor = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeNotasDeCreditoEntreFechasPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor(ByVal valConsecutivoCompania As String, _
                                          ByVal valVista As String, _
                                           ByVal valFechaInicial As Date, _
                                            ByVal valFechaFinal As Date) As String
   Dim Sql As String
   Dim insFacturaView As clsFacturaVista
   Dim SqlPeriodo As String
   Set insFacturaView = New clsFacturaVista
   
   Sql = Sql & "SELECT Periodo.ConsecutivoPeriodo"
   Sql = Sql & " FROM Periodo "
   Sql = Sql & " WHERE Periodo.ConsecutivoCompania = " & valConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(Sql, False)

   Sql = "(SELECT Comprobante.Numero "
   Sql = Sql & " FROM Comprobante "
   Sql = Sql & " WHERE Comprobante.NoDocumentoOrigen = " & valVista & ".TipoDeDocumento "
   Sql = Sql & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & valVista & ".Numero "
   Sql = Sql & " AND Comprobante.GeneradoPor = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_FACTURA))
   Sql = Sql & " AND Comprobante.ConsecutivoPeriodo IN (" & SqlPeriodo & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(valVista & ".Fecha", valFechaInicial, valFechaFinal) & ")"
   
   Set insFacturaView = Nothing
   fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarCuentaContableNotasDeCreditoEntreFechasPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlFacturacionEntreFechaConCostoUltimaCompra(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = ";WITH "
   vSql = vSql & fSqlCteComprasTotalesSinOtrosGastosMonedaLocal(valConsecutivoCompania) & ", " & vbCrLf
   vSql = vSql & fSqlCteComprasOtrosGastosPorItem() & ", " & vbCrLf
   vSql = vSql & fSqlCteComprasPecioCostoPorItem() & ", " & vbCrLf
   vSql = vSql & fSqlCteFacturaMonedaLocal(valConsecutivoCompania, valFechaInicial, valFechaFinal, valcodigoCliente) & vbCrLf
   vSql = vSql & fSqlSelectFacturacionEntreFechaDesdeCte() & vbCrLf
   vSql = vSql & " FROM CTE_FacturaMonedaLocal " & vbCrLf
   vSql = vSql & " ORDER BY CTE_FacturaMonedaLocal.Numero" & vbCrLf
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoUltimaCompra = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlFacturacionEntreFechaConCostoUltimaCompra", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlFacturacionEntreFechaConCostoPromedio(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String) As String
   Dim vSql As String
   Dim vCondicion As String
   Dim vPrecioVenta As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
  vSqlWhere = ""
   If (valcodigoCliente <> "") Then
      vSqlWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente", valcodigoCliente, False)
   End If
   vSqlWhere = gUtilSQL.fSQLValueDatesBetween(vSqlWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.Fecha", valFechaInicial, valFechaFinal)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
   vCondicion = " dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento = " & gUtilSQL.fNumToStrSQL(0)
   vPrecioVenta = gUtilSQL.getIIF(vCondicion, "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual", "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual - (dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual * (dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento/100))", True)
   vSql = fSqlSelectFacturacionEntreFecha()
   vSql = vSql & vPrecioVenta & " AS PrecioVenta,"
   vSql = vSql & "(Gf_CostoPromedioPonderado_1.CostoFinal)  AS PrecioCosto,"
   vSql = vSql & vPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad  AS VentaTotal,"
   vSql = vSql & "(Gf_CostoPromedioPonderado_1.CostoFinal * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad)  AS CostoTotal,"
   vSql = vSql & "(" & vPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad ) " & "-" & "(Gf_CostoPromedioPonderado_1.CostoFinal * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad) AS Diferencia,"
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda "
   vSql = vSql & " FROM dbo.IGV_VentasConDescripcionArticulos_B1 "
   vSql = vSql & " INNER JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS Gf_CostoPromedioPonderado_1 ON "
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Codigo = Gf_CostoPromedioPonderado_1.Codigo AND "
   vSql = vSql & "  Gf_CostoPromedioPonderado_1.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("factura") & " AND "
   vSql = vSql & "  dbo.IGV_VentasConDescripcionArticulos_B1.numero = Gf_CostoPromedioPonderado_1.numerodocumento "
   vSql = vSql & " JOIN ArticuloInventario on ArticuloInventario.Codigo = dbo.IGV_VentasConDescripcionArticulos_B1.Codigo AND dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   vSql = vSql & vSqlWhere
   vSql = vSql & " Group By "
   vSql = vSql & fGroupbyCostoDeFacturaEntreFecha() & "  "
   vSql = vSql & " Order by "
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero"
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoPromedio = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlFacturacionEntreFechaConCostoPromedio", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlFacturacionEntreFechaConCostoUltimaCompraSegunLey(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String, ByVal valMaxGanancia As Currency) As String
Dim vSql As String
Dim vSQLPrecioVenta As String
Dim vSqlWhere As String
Dim vSQLGastoAdmisible As String
Dim vSQLCostoUnitario As String
Dim vSQLVentaTotal As String
Dim vSQLCostoTotal As String
Dim vSQLGanancia As String
Dim vSQLCostoSegunLey As String
Dim vSQLMayorAMaxGanancia As String
On Error GoTo h_ERROR
   vSqlWhere = ""
   
   If (valcodigoCliente <> "") Then
      vSqlWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente", valcodigoCliente, False)
   End If
  
   vSqlWhere = gUtilSQL.fSQLValueDatesBetween(vSqlWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.Fecha", valFechaInicial, valFechaFinal)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
   vSQLPrecioVenta = gUtilSQL.getIIF(" dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento = " & gUtilSQL.fNumToStrSQL(0), "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual", "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual - (dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual * (dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento/100))", True)
   vSQLVentaTotal = vSQLPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLCostoUnitario = " dbo.Gf_CompraUnaFecha(dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania, dbo.IGV_VentasConDescripcionArticulos_B1.Fecha, dbo.IGV_VentasConDescripcionArticulos_B1.Codigo) "
   vSQLCostoUnitario = gUtilSQL.getIIF(vSQLCostoUnitario & " IS NULL ", "0", vSQLCostoUnitario, True)
   vSQLCostoTotal = vSQLCostoUnitario & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLGastoAdmisible = vSQLCostoTotal & " * ArticuloInventario.GastosAdmisibles / 100"
   vSQLCostoSegunLey = vSQLCostoTotal & " + " & vSQLGastoAdmisible
   vSQLGanancia = gUtilSQL.getIIF(vSQLCostoSegunLey & " = 0", "0", "((" & vSQLVentaTotal & ") - (" & vSQLCostoSegunLey & ")) / (" & vSQLCostoSegunLey & ") * 100", True)
   vSQLMayorAMaxGanancia = gUtilSQL.getIIF("(" & vSQLGanancia & ") > " & gUtilSQL.fNumToStrSQL(valMaxGanancia), gUtilSQL.fSimpleSqlValue("*"), gUtilSQL.fSimpleSqlValue(""), True)
   
   vSql = fSqlSelectFacturacionEntreFecha()
   vSql = vSql & vSQLPrecioVenta & " AS PrecioVenta,"
   vSql = vSql & vSQLVentaTotal & " AS VentaTotal,"
   vSql = vSql & vSQLCostoUnitario & " AS PrecioCosto,"
   vSql = vSql & vSQLCostoTotal & " AS CostoTotal,"
   vSql = vSql & " ArticuloInventario.GastosAdmisibles AS PorcentajeGastoAdmisible,"
   vSql = vSql & vSQLGastoAdmisible & " AS GastoAdmisible,"
   vSql = vSql & vSQLCostoSegunLey & " AS CostoSegunLey, "
   vSql = vSql & vSQLGanancia & " AS PorcentajeGanancia, "
   vSql = vSql & vSQLMayorAMaxGanancia & " AS MayorAMaxGanancia,"
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda "
   
   vSql = vSql & " FROM dbo.IGV_VentasConDescripcionArticulos_B1"
    
   vSql = vSql & " JOIN ArticuloInventario on ArticuloInventario.Codigo = dbo.IGV_VentasConDescripcionArticulos_B1.Codigo AND dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   
   vSql = vSql & vSqlWhere
   
   vSql = vSql & " Order by "
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero"
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoUltimaCompraSegunLey = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCostoPromedioPonderadoArticulosCompuestos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlFacturacionEntreFechaConCostoPromedioSegunLey(ByVal valConsecutivoCompania As Long, ByVal valCodigoDesde As String, ByVal valCodigoHasta As String, ByVal valFechaCierre As Date, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String, ByVal valMaxGanancia As Currency) As String
Dim vSql As String
Dim vSQLPrecioVenta As String
Dim vSqlWhere As String
Dim vSQLGastoAdmisible As String
Dim vSQLCostoUnitario As String
Dim vSQLVentaTotal As String
Dim vSQLCostoTotal As String
Dim vSQLGanancia As String
Dim vSQLCostoSegunLey As String
Dim vSQLMayorAMaxGanancia As String
On Error GoTo h_ERROR
   vSqlWhere = ""
   
   If (valcodigoCliente <> "") Then
      vSqlWhere = gUtilSQL.fSQLValueWithAnd("", "dbo.IGV_VentasConDescripcionArticulos_B1.CodigoCliente", valcodigoCliente, False)
   End If
   
   vSqlWhere = gUtilSQL.fSQLValueDatesBetween(vSqlWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.Fecha", valFechaInicial, valFechaFinal)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
   vSQLPrecioVenta = gUtilSQL.getIIF(" dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento = " & gUtilSQL.fNumToStrSQL(0), "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual", "dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual - (dbo.IGV_VentasConDescripcionArticulos_B1.PrecioConDescuentoIndividual * (dbo.IGV_VentasConDescripcionArticulos_B1.PorcentajeDescuento/100))", True)
   vSQLVentaTotal = vSQLPrecioVenta & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLCostoUnitario = " Gf_CostoPromedioPonderado_1.CostoFinal "
   vSQLCostoUnitario = gUtilSQL.getIIF(vSQLCostoUnitario & " IS NULL ", "0", vSQLCostoUnitario, True)
   vSQLCostoTotal = vSQLCostoUnitario & " * dbo.IGV_VentasConDescripcionArticulos_B1.Cantidad "
   vSQLGastoAdmisible = vSQLCostoTotal & " * ArticuloInventario.GastosAdmisibles / 100"
   vSQLCostoSegunLey = vSQLCostoTotal & " + " & vSQLGastoAdmisible
   vSQLGanancia = gUtilSQL.getIIF(vSQLCostoSegunLey & " = 0", "0", "((" & vSQLVentaTotal & ") - (" & vSQLCostoSegunLey & ")) / (" & vSQLCostoSegunLey & ") * 100", True)
   vSQLMayorAMaxGanancia = gUtilSQL.getIIF("(" & vSQLGanancia & ") > " & gUtilSQL.fNumToStrSQL(valMaxGanancia), gUtilSQL.fSimpleSqlValue("*"), gUtilSQL.fSimpleSqlValue(""), True)
 
   vSql = fSqlSelectFacturacionEntreFecha()
   vSql = vSql & vSQLPrecioVenta & " AS PrecioVenta,"
   vSql = vSql & vSQLVentaTotal & " AS VentaTotal,"
   vSql = vSql & vSQLCostoUnitario & " AS PrecioCosto,"
   vSql = vSql & vSQLCostoTotal & " AS CostoTotal,"
   vSql = vSql & " ArticuloInventario.GastosAdmisibles AS PorcentajeGastoAdmisible,"
   vSql = vSql & vSQLGastoAdmisible & " AS GastoAdmisible,"
   vSql = vSql & vSQLCostoSegunLey & " AS CostoSegunLey, "
   vSql = vSql & vSQLGanancia & " AS PorcentajeGanancia, "
   vSql = vSql & vSQLMayorAMaxGanancia & " AS MayorAMaxGanancia,"
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Moneda "
   
   vSql = vSql & " FROM dbo.IGV_VentasConDescripcionArticulos_B1 "
   
   vSql = vSql & " INNER JOIN  dbo.Gf_CostoPromedioPonderado( " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania) & "," & gUtilSQL.fDateToSQLValue(valFechaCierre) & "," & gUtilSQL.fDateToSQLValue(valFechaFinal) & "," & gUtilSQL.fSimpleSqlValue(valCodigoDesde) & "," & gUtilSQL.fSimpleSqlValue(valCodigoHasta) & ") AS Gf_CostoPromedioPonderado_1 ON "
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Codigo = Gf_CostoPromedioPonderado_1.Codigo AND "
   vSql = vSql & " Gf_CostoPromedioPonderado_1.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("factura") & " AND "
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.numero = Gf_CostoPromedioPonderado_1.numerodocumento "
   
   vSql = vSql & " JOIN ArticuloInventario on ArticuloInventario.Codigo = dbo.IGV_VentasConDescripcionArticulos_B1.Codigo AND dbo.IGV_VentasConDescripcionArticulos_B1.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
   
   vSql = vSql & vSqlWhere
   
   vSql = vSql & " Group By "
   vSql = vSql & fGroupbyCostoDeFacturaEntreFecha() & " , ArticuloInventario.GastosAdmisibles"
   
   vSql = vSql & " Order by "
   vSql = vSql & " dbo.IGV_VentasConDescripcionArticulos_B1.Numero"
h_EXIT:  On Error GoTo 0
  fSqlFacturacionEntreFechaConCostoPromedioSegunLey = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlFacturacionEntreFechaConCostoPromedio", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
         "fSqlSelectFacturacionEntreFecha", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
 
Public Function fSQLServiciosXVehiculos(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valCategoria As String, ByVal valPlaca As String, ByVal valSerialVIN As String) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = "SELECT Vehiculo.Consecutivo, Vehiculo.Placa, Vehiculo.serialVIN, SAW.Modelo.Nombre, SAW.Modelo.Marca, Color.DescripcionColor, Vehiculo.Ano, Vehiculo.NumeroPoliza,"
   vSql = vSql & "  Cliente.Nombre AS NombreCliente, factura.Numero, factura.Fecha, camposDefFactura.CampoDefinible1 AS Kilometraje, ArticuloInventario.Descripcion,"
   vSql = vSql & "  renglonFactura.Cantidad,"
   vSql = vSql & "  renglonFactura.Cantidad * (renglonFactura.PrecioSinIVA - (renglonFactura.PrecioSinIVA * renglonFactura.PorcentajeDescuento / 100 + renglonFactura.PrecioSinIVA * factura.PorcentajeDescuento"
   vSql = vSql & "/ 100)) AS SuBTotal, renglonFactura.Descripcion AS Descripcionrenglon, ArticuloInventario.Categoria"
   vSql = vSql & " FROM factura INNER JOIN"
   vSql = vSql & "  renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSql = vSql & "  factura.TipoDeDocumento = renglonFactura.TipoDeDocumento INNER JOIN"
   vSql = vSql & "  Vehiculo ON factura.ConsecutivoCompania = Vehiculo.ConsecutivoCompania AND factura.ConsecutivoVehiculo = Vehiculo.Consecutivo INNER JOIN"
   vSql = vSql & "  SAW.Modelo ON Vehiculo.NombreModelo = SAW.Modelo.Nombre INNER JOIN"
   vSql = vSql & "  Color ON Vehiculo.ConsecutivoCompania = Color.ConsecutivoCompania AND Vehiculo.CodigoColor = Color.CodigoColor INNER JOIN"
   vSql = vSql & "  Cliente ON Vehiculo.ConsecutivoCompania = Cliente.ConsecutivoCompania AND Vehiculo.CodigoCliente = Cliente.Codigo LEFT JOIN"
   vSql = vSql & "  camposDefFactura ON factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania AND factura.Numero = camposDefFactura.NumeroFactura AND"
   vSql = vSql & "  factura.TipoDeDocumento = camposDefFactura.TipoDeDocumento INNER JOIN"
   vSql = vSql & "  ArticuloInventario ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND renglonFactura.Articulo = ArticuloInventario.Codigo"
   vSql = vSql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valCategoria) > 0 Then
      vSql = vSql & "  AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   End If
   vSql = vSql & "  AND factura.TipoDeDocumento <> (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & ")"
   vSql = vSql & "  AND factura.TipoDeDocumento <> (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ")"
   vSql = vSql & "  AND factura.StatusFactura = (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valPlaca) > 0 Then
      vSql = vSql & "  AND (Vehiculo.Placa = " & gUtilSQL.fSimpleSqlValue(valPlaca) & ")"
   End If
   If gTexto.DfLen(valSerialVIN) > 0 Then
         vSql = gUtilSQL.fSQLValueWithWildcardWithAnd(vSql, "Vehiculo.serialVIN", valSerialVIN)
   End If
   vSql = vSql & " ORDER BY Vehiculo.Placa, factura.Fecha"
h_EXIT: On Error GoTo 0
   fSQLServiciosXVehiculos = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLServiciosXVehiculos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLServiciosDeVehiculosXCategoria(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valCategoria As String) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = "SELECT Vehiculo.Consecutivo, Vehiculo.Placa, Vehiculo.serialVIN, SAW.Modelo.Nombre, SAW.Modelo.Marca, Color.DescripcionColor, Vehiculo.Ano,"
   vSql = vSql & " Vehiculo.NumeroPoliza, Cliente.Nombre AS NombreCliente, Cliente.Telefono, Cliente.Email, factura.Numero, MAX(factura.Fecha) AS FechaUltimoServicio,"
   vSql = vSql & " camposDefFactura.CampoDefinible1 AS Kilometraje, ArticuloInventario.Descripcion, renglonFactura.Descripcion AS Descripcionrenglon, ArticuloInventario.Categoria"
   vSql = vSql & " FROM factura INNER JOIN"
   vSql = vSql & " renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSql = vSql & " factura.TipoDeDocumento = renglonFactura.TipoDeDocumento INNER JOIN"
   vSql = vSql & " Vehiculo ON factura.ConsecutivoCompania = Vehiculo.ConsecutivoCompania AND factura.ConsecutivoVehiculo = Vehiculo.Consecutivo INNER JOIN"
   vSql = vSql & " SAW.Modelo ON Vehiculo.NombreModelo = SAW.Modelo.Nombre INNER JOIN"
   vSql = vSql & " Color ON Vehiculo.ConsecutivoCompania = Color.ConsecutivoCompania AND Vehiculo.CodigoColor = Color.CodigoColor INNER JOIN"
   vSql = vSql & " Cliente ON Vehiculo.ConsecutivoCompania = Cliente.ConsecutivoCompania AND Vehiculo.CodigoCliente = Cliente.Codigo INNER JOIN"
   vSql = vSql & " camposDefFactura ON factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania AND factura.Numero = camposDefFactura.NumeroFactura AND"
   vSql = vSql & " factura.TipoDeDocumento = camposDefFactura.TipoDeDocumento INNER JOIN"
   vSql = vSql & " ArticuloInventario ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND renglonFactura.Articulo = ArticuloInventario.Codigo"
   vSql = vSql & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valCategoria) <> 0 Then
      vSql = vSql & "  AND ArticuloInventario.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   End If
   vSql = vSql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   vSql = vSql & " GROUP BY Vehiculo.Consecutivo, Vehiculo.Placa, Vehiculo.serialVIN, SAW.Modelo.Nombre, SAW.Modelo.Marca, Color.DescripcionColor, Vehiculo.Ano,"
   vSql = vSql & " Vehiculo.NumeroPoliza, Cliente.Nombre, Cliente.Telefono, Cliente.Email, factura.Numero, camposDefFactura.CampoDefinible1, ArticuloInventario.Descripcion,"
   vSql = vSql & " renglonFactura.Descripcion, ArticuloInventario.Categoria"
h_EXIT: On Error GoTo 0
   fSQLServiciosDeVehiculosXCategoria = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLServiciosDeVehiculosXCategoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLHorasDeServicioXVendedor(ByVal valVendedor As Enum_Vendedores_Por_Renglon) As String
   Dim vSql As String
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
   vSql = "SELECT Vendedor.Codigo, Vendedor.Nombre, factura.Numero, factura.Fecha, renglonFactura.Descripcion, renglonFactura.Cantidad, CASE WHEN ISNUMERIC(CampoDefinible1)"
   vSql = vSql & " = 1 THEN CAST(CampoDefinible1 AS Money) ELSE 0 END AS Factor, CASE WHEN ISNUMERIC(CampoDefinible1) = 1 THEN CAST(CampoDefinible1 AS Money)"
   vSql = vSql & " * Cantidad ELSE 0 END AS FactorXCantidad"
   vSql = vSql & " FROM factura INNER JOIN"
   vSql = vSql & " renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSql = vSql & " factura.TipoDeDocumento = renglonFactura.TipoDeDocumento AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & " INNER JOIN"
   vSql = vSql & " ArticuloInventario ON renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND"
   vSql = vSql & " renglonFactura.Articulo = ArticuloInventario.Codigo INNER JOIN"
   vSql = vSql & " Vendedor ON renglonFactura." & vCodigoDeVendedor & " = Vendedor.Codigo AND renglonFactura.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   
   'vSQL = vSQL & " WHERE factura.StatusFactura = '0' AND factura.TipoDeDocumento <> '5'  and factura.ConsecutivoCompania = 1"
h_EXIT: On Error GoTo 0
   fSQLHorasDeServicioXVendedor = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHorasDeServicioXVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLHorasDeServicioXVendedorCompleto(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valcodigoVendedor As String) As String
   Dim vSql As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
   vSqlWhere = " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlWhere = vSqlWhere & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valcodigoVendedor) > 0 Then
      vSqlWhere = vSqlWhere & "  AND Vendedor.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
   End If
   vSqlWhere = vSqlWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSqlWhere = vSqlWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSqlWhere = vSqlWhere & "  AND factura.StatusFactura = (" & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   
   vSql = fSQLHorasDeServicioXVendedor(eVPR_Vendedor1) & vSqlWhere
   vSql = vSql & " UNION " & fSQLHorasDeServicioXVendedor(eVPR_Vendedor2) & vSqlWhere
   vSql = vSql & " UNION " & fSQLHorasDeServicioXVendedor(eVPR_Vendedor3) & vSqlWhere
   vSql = vSql & " ORDER BY " & "Vendedor.Codigo, factura.Fecha, factura.Numero"
h_EXIT: On Error GoTo 0
   fSQLHorasDeServicioXVendedorCompleto = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHorasDeServicioXVendedorCompleto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLComisionVendedoresXArticulo(ByVal valVendedor As Enum_Vendedores_Por_Renglon) As String
   Dim vSql As String
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
   vSql = " SELECT Vendedor.Codigo, Vendedor.Nombre, factura.Fecha, factura.Numero, renglonFactura.Descripcion, renglonFactura.Cantidad, renglonFactura.PrecioSinIVA, " & vSQLSubTotal & " As SubTotal, "
   vSql = vSql & vSQLSubTotal & " * ( renglonFactura.PorcentajeDescuento/100)+ ( " & vSQLSubTotal & " * ( factura.PorcentajeDescuento/100)) as Descuento, "
   vSql = vSql & vSQLSubTotal & " - ( " & vSQLSubTotal & "  * ( renglonFactura.PorcentajeDescuento/100)+ ( renglonFactura.Cantidad * renglonFactura.PrecioSinIVA*( factura.PorcentajeDescuento/100))) AS SubTotalConDescuento, "
   vSql = vSql & gUtilSQL.getIIF("Articuloinventario.ComisionaPorcentaje = " & gUtilSQL.fBooleanToSqlValue(True), "(" & vSQLSubTotal & " - ( " & vSQLSubTotal & " * ( renglonFactura.PorcentajeDescuento/100)+ ( " & vSQLSubTotal & " *( factura.PorcentajeDescuento/100)))) * (ArticuloInventario.PorcentajeComision/100)", "ArticuloInventario.PorcentajeComision * renglonFactura.Cantidad ", True) & " AS Comision "
   vSql = vSql & " FROM factura INNER JOIN"
   vSql = vSql & " renglonFactura ON factura.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND factura.Numero = renglonFactura.NumeroFactura AND"
   vSql = vSql & " factura.TipoDeDocumento = renglonFactura.TipoDeDocumento AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & " INNER JOIN"
   vSql = vSql & " Vendedor ON renglonFactura.ConsecutivoCompania = Vendedor.ConsecutivoCompania AND renglonFactura." & vCodigoDeVendedor & " = Vendedor.Codigo INNER JOIN"
   vSql = vSql & " ArticuloInventario ON renglonFactura.Articulo = ArticuloInventario.Codigo AND renglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania"
h_EXIT: On Error GoTo 0
   fSQLComisionVendedoresXArticulo = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComisionVendedoresXArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLComisionVendedoresXArticuloCompleto(ByVal valConsecutivoCompania As String, _
                                          ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal valcodigoVendedor As String) As String
   Dim vSql As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
   vSqlWhere = " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlWhere = vSqlWhere & "  AND " & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal)
   If gTexto.DfLen(valcodigoVendedor) > 0 Then
      vSqlWhere = vSqlWhere & "  AND Vendedor.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
   End If
   vSqlWhere = vSqlWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSqlWhere = vSqlWhere & "  AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSqlWhere = vSqlWhere & "  AND factura.StatusFactura = (" & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_EMITIDA) & ")"
   vSqlWhere = vSqlWhere & "  AND Articuloinventario.ExcluirDeComision = " & gUtilSQL.fBooleanToSqlValue(False)
   
   vSql = fSQLComisionVendedoresXArticulo(eVPR_Vendedor1) & vSqlWhere
   vSql = vSql & " UNION " & fSQLComisionVendedoresXArticulo(eVPR_Vendedor2) & vSqlWhere
   vSql = vSql & " UNION " & fSQLComisionVendedoresXArticulo(eVPR_Vendedor3) & vSqlWhere
   vSql = vSql & " ORDER BY " & "Vendedor.Codigo, factura.Fecha, factura.Numero"
h_EXIT: On Error GoTo 0
   fSQLComisionVendedoresXArticuloCompleto = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComisionVendedoresXArticuloCompleto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLListadoDeVehiculosXPropietario(ByVal valConsecutivoCompania As String, _
                                          ByVal valPlaca As String, ByVal valcodigoCliente As String)
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = "SELECT Vehiculo.Placa, Vehiculo.serialVIN, Vehiculo.Ano, Color.DescripcionColor, SAW.Modelo.Nombre as NombreModelo, SAW.Modelo.Marca, Cliente.Codigo, Cliente.Nombre AS NombreCliente,"
   vSql = vSql & " Cliente.NumeroRIF , Cliente.Direccion, Cliente.Telefono, Vehiculo.NumeroPoliza, Vehiculo.SerialMotor"
   vSql = vSql & " FROM         Vehiculo INNER JOIN"
   vSql = vSql & " Cliente ON Vehiculo.ConsecutivoCompania = Cliente.ConsecutivoCompania AND Vehiculo.CodigoCliente = Cliente.Codigo INNER JOIN"
   vSql = vSql & " SAW.Modelo ON Vehiculo.NombreModelo = SAW.Modelo.Nombre INNER JOIN"
   vSql = vSql & " Color ON Vehiculo.ConsecutivoCompania = Color.ConsecutivoCompania AND Vehiculo.CodigoColor = Color.CodigoColor"
   vSql = vSql & " WHERE Vehiculo.ConsecutivoCompania = " & valConsecutivoCompania
   If gTexto.DfLen(valPlaca) Then
      vSql = vSql & " AND Vehiculo.Placa = " & gUtilSQL.fSimpleSqlValue(valPlaca)
   End If
   If gTexto.DfLen(valcodigoCliente) Then
      vSql = vSql & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
h_EXIT: On Error GoTo 0
   fSQLListadoDeVehiculosXPropietario = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLListadoDeVehiculosXPropietario", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & "Factura.Numero, "
   Sql = Sql & "Factura.Moneda, "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   If valInformeResumido Then
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "SUM(RenglonFactura.Cantidad)", True) & " AS CantidadDeArticulo, "
      Sql = Sql & "SUM(" & SqlTotalRenglon & ") AS TotalRenglon "
      If valMuestraMontoIva Then
         Sql = Sql & "," & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
            "SUM(" & SqlRenglonTotalParaMontoIva & ")", True) & " AS TotalRenglonConIva, "
         Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
            "SUM(" & SqlRenglonTotalParaMontoIva & "-" & SqlTotalRenglon & ")", True) & " AS MontoIva "
      End If
   Else
      Sql = Sql & "RenglonFactura.Articulo AS CodigoArticulo, "
      Sql = Sql & "RenglonFactura.Descripcion AS DescripcionArticulo, "
      Sql = Sql & "RenglonFactura.PorcentajeDescuento AS PorcDectoRenglon, "
      Sql = Sql & "IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria, "
      Sql = Sql & "Factura.PorcentajeDescuento AS PorcDectoFactura, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "RenglonFactura.Cantidad", True) & " AS CantidadDeArticulo, "
      Sql = Sql & sqlPrecioConIva & " AS PrecioConIva, "
      Sql = Sql & sqlPrecioSinIva & " AS PrecioSinIva, "
      
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlPrecioConIva & "-" & sqlPrecioSinIva, True) & " AS MontoIva, "
      Sql = Sql & SqlTotalRenglon & " AS TotalRenglon, "
      
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual), True) & " AS TotalRenglonConIva, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual) & " - " & SqlTotalRenglon, True) & " AS TotalRenglonIva, "
     
      Sql = Sql & SqlStatusFactura & " AS StatusFactura, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlDescMonto, True) & " AS MontoDescRenglon, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlFactMonto, True) & " AS MontoDescFactura "
   End If
   Sql = Sql & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   
   
   Sql = Sql & " INNER JOIN ((Cliente INNER JOIN Factura"
   Sql = Sql & " ON  (Cliente.Codigo = Factura.CodigoCliente)"
   Sql = Sql & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   Sql = Sql & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania))"
   Sql = Sql & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   
   
   
   
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Sql = Sql & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      Sql = Sql & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   If valCantidadCategoria <> "Todos" Then
       Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria = " & gUtilSQL.fSimpleSqlValue(valCategoria)
   End If
   If valFiltrarPorVendedor Then
       Sql = Sql & " AND Factura.CodigoVendedor = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
   End If
   If valInformeResumido Then
      Sql = Sql & " GROUP BY  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Categoria,Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.StatusFactura "
   End If
   Sql = Sql & " ORDER BY Factura.StatusFactura, Factura.Moneda, Factura.Fecha, Factura.Numero"
   fSQLDeFacturasEntreFechasPorCategoria = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorCategoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLDeFacturasEntreFechasPorLinea(ByVal valConsecutivoCompania As String, _
                                                                      ByVal valFechaInicial As Date, _
                                                                       ByVal valFechaFinal As Date, _
                                                                        ByVal valCantidadLinea, _
                                                                         ByVal valLinea)

   Dim Sql As String
   On Error GoTo h_ERROR
   Dim sqlMontoExentoRenglon As String
   Dim sqlTotalRenglonConDscto As String
   Dim sqlIvaRenglon As String
   sqlMontoExentoRenglon = gUtilSQL.getIIF("IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.AlicuotaIva = '0'", "RenglonFactura.TotalRenglon * (1 - factura.PorcentajeDescuento / 100)", "0")
   sqlTotalRenglonConDscto = "RenglonFactura.TotalRenglon * (1 - factura.PorcentajeDescuento / 100)"
   sqlIvaRenglon = sqlTotalRenglonConDscto & " * RenglonFactura.PorcentajeAlicuota / 100"
   Sql = "SELECT  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto,"
   Sql = Sql & "SUM(" & sqlMontoExentoRenglon & " ) AS MontoExento,"
   Sql = Sql & "SUM(" & sqlTotalRenglonConDscto & " - " & sqlMontoExentoRenglon & " ) AS BaseImponible,"
   Sql = Sql & "SUM(" & sqlIvaRenglon & " ) AS TotalIva,"
   Sql = Sql & "SUM(" & sqlTotalRenglonConDscto & " + " & sqlIvaRenglon & ") AS TotalFactura,"
   Sql = Sql & "SUM((RenglonFactura.PrecioSinIva*RenglonFactura.Cantidad-TotalRenglon)+(TotalRenglon"
   Sql = Sql & " * Factura.PorcentajeDescuento/100)) AS Descuento From Factura"
   Sql = Sql & " INNER JOIN RenglonFactura ON (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania "
   Sql = Sql & " AND RenglonFactura.NumeroFactura = Factura.Numero"
   Sql = Sql & " AND RenglonFactura.TipoDeDocumento = Factura.TipoDeDocumento)"
   Sql = Sql & " INNER JOIN IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   Sql = Sql & " ON(IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania"
   Sql = Sql & " = RenglonFactura.ConsecutivoCompania AND  IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.codigodelArticulo"
   Sql = Sql & " = RenglonFactura.Articulo AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo) ) WHERE"
   Sql = Sql & " Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   If valCantidadLinea <> "Todos" Then
       Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(valLinea)
   End If
   Sql = Sql & " GROUP BY IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto"
   Sql = Sql & " ORDER BY IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto "
   fSQLDeFacturasEntreFechasPorLinea = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorLinea", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
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
      
   Sql = "SELECT "
   Sql = Sql & "Factura.Numero, "
   Sql = Sql & "Factura.Moneda, "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   If valInformeResumido Then
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "SUM(RenglonFactura.Cantidad)", True) & " AS CantidadDeArticulo, "
      Sql = Sql & "SUM(" & SqlTotalRenglon & ") AS TotalRenglon "
   Else
      Sql = Sql & "RenglonFactura.Articulo AS CodigoArticulo, "
      Sql = Sql & "RenglonFactura.Descripcion AS DescripcionArticulo, "
      Sql = Sql & "RenglonFactura.PorcentajeDescuento AS PorcDectoRenglon, "
      Sql = Sql & "Factura.PorcentajeDescuento AS PorcDectoFactura, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                  "RenglonFactura.Cantidad", True) & " AS CantidadDeArticulo, "
      Sql = Sql & sqlPrecioConIva & " AS PrecioConIva, "
      Sql = Sql & sqlPrecioSinIva & " AS PrecioSinIva, "
      Sql = Sql & SqlTotalRenglon & " AS TotalRenglon, "
      Sql = Sql & fSQLMontoTotalDelRenglonConIvaConElDescuentoDeFactura(usarCambioOriginal, False, gUltimaTasaDeCambio, gMonedaLocalActual) & " AS TotalRenglonConIva, "
      Sql = Sql & SqlStatusFactura & " AS StatusFactura, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlDescMonto, True) & " AS MontoDescRenglon, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_ANULADA), "0", _
                  sqlFactMonto, True) & " AS MontoDescFactura, "
   End If
   Sql = Sql & "RenglonFactura.Serial "
   Sql = Sql & " FROM IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1"
   Sql = Sql & " INNER JOIN ((Cliente INNER JOIN Factura"
   Sql = Sql & " ON  (Cliente.Codigo = Factura.CodigoCliente)"
   Sql = Sql & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN RenglonFactura ON (Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento)"
   Sql = Sql & " AND (Factura.Numero = RenglonFactura.NumeroFactura)"
   Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   Sql = Sql & " AND renglonFactura.Serial <> '0')"
   Sql = Sql & " ON  (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.CodigoDelArticulo = RenglonFactura.Articulo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Serial = RenglonFactura.Serial) "
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.Rollo = RenglonFactura.Rollo)"
   Sql = Sql & " AND (IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania)"
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   If valImprimirUnaLineaDeProducto Then
      Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.LineaDeProducto = " & gUtilSQL.fSimpleSqlValue(Trim(valLineaDeProductoAImprimir))
   End If
   If valImprimirUnTipoDeArticulo Then
      Sql = Sql & " AND IGV_ArticulosDeInventarioConOSinExistenPorGrupo_B1.TipoDeArticulo = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strTipoDeArticuloToNum(valTipoDeArticulo))
   End If
   Sql = Sql & " AND (Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & " OR  Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & " OR  Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & " )"
   Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Sql = Sql & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      Sql = Sql & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   If valInformeResumido Then
      Sql = Sql & " GROUP BY Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.StatusFactura "
   End If
   Sql = Sql & " ORDER BY Factura.StatusFactura, Factura.Moneda, Factura.Fecha, Factura.Numero"

   fSQLDeFacturasEntreFechasConSerial = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeFacturasPorTalonario(ByVal valConsecutivoCompania As String, _
                                                                              ByVal valFechaInicial As Date, _
                                                                                ByVal valFechaFinal As Date, _
                                                                                  ByVal valTalonario As String, _
                                                                                    ByVal valcodigoCliente As String, _
                                                                                      ByVal valFiltrarTalonario As String) As String
   Dim Sql As String
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
                                       
   Sql = "SELECT "
   Sql = Sql & "Factura.Numero, "
   Sql = Sql & "Factura.Moneda, "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   Sql = Sql & "Factura.PorcentajeDescuento, "
   Sql = Sql & sqlFactMonto & " AS TotalFactura,"
   Sql = Sql & SqlStatusFactura & " AS StatusFactura, "
   Sql = Sql & SqlTalonario & " AS Talonario"
   Sql = Sql & " FROM factura"
   Sql = Sql & " INNER JOIN Cliente ON Factura.CodigoCliente = Cliente.Codigo and factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Sql = Sql & " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   If valFiltrarTalonario = "1" Then
      Sql = Sql & " AND factura.Talonario = '" & valTalonario & "'"
   End If
   If gTexto.DfLen(valcodigoCliente) > 0 Then
      Sql = Sql & " AND Cliente.Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & "group by Factura.Numero, Factura.Moneda, Factura.Fecha, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Factura.PorcentajeDescuento,"
   Sql = Sql & " factura.TotalFactura , StatusFactura, Talonario"
   Sql = Sql & " ORDER BY factura.Talonario, factura.Numero, factura.Fecha"

   fSQLDeFacturasPorTalonario = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorTipoDeArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fGroupbyCostoDeFacturaEntreFecha()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
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
   
   Sql = "SELECT "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         Sql = Sql & "Factura.Moneda, "
   End If
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "2 AS ORDEN, "
   Sql = Sql & valCampoAMostrar & " as Numero, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   Sql = Sql & "Factura.TipoDeDocumento, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   Sql = Sql & "'' AS StatusFactura, "
   Sql = Sql & "0 AS MontoOriginal, "
   Sql = Sql & "0 AS TotalIVA, "
   Sql = Sql & "0 AS TotalBaseImponible, "
   Sql = Sql & "0 AS TotalMontoExento, "
   Sql = Sql & "0 AS TotalFactura, "
   Sql = Sql & "SUM (RenglonDetalleDeOtrosCargosFactura.TotalRenglon) AS OtrosCargos, "
   Sql = Sql & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      Sql = Sql & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      Sql = Sql & " AS NumeroComprobante"
   End If
   Sql = Sql & ", " & gUtilSQL.fSimpleSqlValue("") & " AS ComprobanteFiscal"
   Sql = Sql & ", camposDefFactura." & vCampoDefinible & " AS CampoDefinible"
   Sql = Sql & " FROM (Cliente INNER JOIN "
   Sql = Sql & " Factura ON (Cliente.Codigo = Factura.CodigoCliente)"
   Sql = Sql & " AND (Cliente.ConsecutivoCompania = Factura.ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN RenglonDetalleDeOtrosCargosFactura ON "
   Sql = Sql & " (Factura.TipoDeDocumento = RenglonDetalleDeOtrosCargosFactura.TipoDeDocumento)"
   Sql = Sql & " AND (Factura.Numero = RenglonDetalleDeOtrosCargosFactura.NumeroFactura)"
   Sql = Sql & " AND (Factura.ConsecutivoCompania = RenglonDetalleDeOtrosCargosFactura.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   Sql = Sql & " AND factura.CodigoVendedor = Vendedor.Codigo "
   Sql = Sql & " INNER JOIN camposDefFactura ON (Factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania"
   Sql = Sql & " AND factura.Numero = camposDefFactura.NumeroFactura)"
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " AND (renglonDetalleDeOtrosCargosFactura.ComoAplicaAlTotalFactura <> " & gUtilSQL.fSimpleSqlValue(enum_ComoAplicaOtrosCargosDeFactura.eCA_NO_APLICA_SOLO_INFORMATIVO) & ")"
   If valInformeEsDeBorradores Then
      Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltrarCampoDef Then
      Sql = gUtilSQL.fSQLValueWithWildcardWithAnd(Sql, "camposDefFactura." & vCampoDefinible, valValorCampoDef)
   End If
   If valFiltroInforme <> "" Then
      Sql = Sql & " AND " & valFiltroInforme
   End If

   Sql = Sql & " GROUP BY "
   If Not valReporteEnMonedaLocal Then
      Sql = Sql & "Factura.Moneda, "
   End If
      Sql = Sql & "Factura.Fecha, Factura.Numero, Factura.TipoDeDocumento, Factura.CodigoCliente, Cliente.Nombre, Cliente.Contacto, Vendedor.Nombre, camposDefFactura." & vCampoDefinible

   fSQLGenerarOtrosCargosyDescuentosPorCamposDefinibles = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLGenerarOtrosCargosyDescuentosPorCamposDefinibles", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
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
         
         
   Sql = "SELECT "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
   Else
         Sql = Sql & "Factura.Moneda, "
   End If
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "1 AS ORDEN, "
   Sql = Sql & valCampoAMostrar & " as Numero, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                                          gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") _
                                          & " AS TipoDeDocumentoStr, "
   Sql = Sql & "Factura.TipoDeDocumento, "
   Sql = Sql & "Factura.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cliente.Contacto, "
   Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
                               gUtilSQL.fSimpleSqlValue(""), True) & " AS StatusFactura, "
   Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                               "0", "Factura.TotalFactura", True) & " AS MontoOriginal, "
   If valReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalIva", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalBaseImponible", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalBaseImponible, "
'EXENTO
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalMontoExento", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalMontoExento, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalFactura", _
                                  "Factura.Fecha"), True, ""), True) & " AS TotalFactura, "
   Else
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalIVA", True) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalBaseImponible", True) & " AS TotalBaseImponible, "
     Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), "0", _
                          gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", _
                          gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalMontoExento", _
                          "Factura.Fecha"), True, ""), True) & " AS TotalMontoExento, "
     Sql = Sql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
                                  "0", "Factura.TotalFactura", True) & " AS TotalFactura, "
   End If
   Sql = Sql & "0 AS OtrosCargos, "
   Sql = Sql & "Vendedor.Nombre AS Vendedor "
   If valUsaModuloDeContabilidad Then
      Sql = Sql & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), _
                                        fSQGenerarCuentaContable(valConsecutivoCompania, valFechaInicial, valFechaFinal), _
                                        gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      Sql = Sql & " AS NumeroComprobante"
   End If
   Sql = Sql & ", Factura.NumeroComprobanteFiscal AS ComprobanteFiscal "
   Sql = Sql & ", camposDefFactura." & vCampoDefinible & " AS CampoDefinible"
   Sql = Sql & " FROM Factura INNER JOIN "
   Sql = Sql & " Cliente ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " AND Factura.CodigoCliente = Cliente.Codigo"
   Sql = Sql & " INNER JOIN Vendedor ON factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania "
   Sql = Sql & " AND factura.CodigoVendedor = Vendedor.Codigo"
   Sql = Sql & " INNER JOIN camposDefFactura ON (Factura.ConsecutivoCompania = camposDefFactura.ConsecutivoCompania"
   Sql = Sql & " AND factura.Numero = camposDefFactura.NumeroFactura)"
   Sql = Sql & " WHERE Factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   If valInformeEsDeBorradores Then
      Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   Else
      Sql = Sql & " AND (Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
                  " OR Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   End If
   If valFiltroInforme <> "" Then
      Sql = Sql & " AND " & valFiltroInforme
   End If
    If valUsarCiudad Then
      Sql = Sql & " AND Cliente.Ciudad = " & gUtilSQL.fSimpleSqlValue(valCiudad)
   End If
   Sql = Sql & " AND (Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & ") "
   If valFiltrarCampoDef Then
      Sql = gUtilSQL.fSQLValueWithWildcardWithAnd(Sql, "camposDefFactura." & vCampoDefinible, valValorCampoDef)
   End If
   
   If valAgrupaInforme Then
      Sql = Sql & " ORDER BY Moneda, Vendedor, TipoDeDocumentoStr, Fecha, camposDefFactura." & vCampoDefinible
   Else
      Sql = Sql & " ORDER BY Moneda, TipoDeDocumentoStr, Fecha, camposDefFactura." & vCampoDefinible
   End If
   
   fSQLDeFacturasEntreFechasPorCamposDefinibles = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeFacturasEntreFechasPorCamposDefinibles", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLCaseIfForEnumTipoDeProductoToStringInArray(ByRef gEnumProyecto As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = gUtilSQL.DfSQLCaseIfForEnum(" ArticuloInventario." & "TipoDeProducto", _
         enum_TipoDeProducto.eTD_TP_NUEVO, gEnumProyecto.fEnumTipoDeProductoToStringInArray, "")
   fSQLCaseIfForEnumTipoDeProductoToStringInArray = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLCaseIfForEnumTipoDeProductoToStringInArray", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlSelectFacturacionEntreFechaDesdeCte() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "SELECT CTE_FacturaMonedaLocal.Fecha, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Numero, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Cliente, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Vendedor, "
   vResult = vResult & "CTE_FacturaMonedaLocal.StatusFactura, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Articulo, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Serial, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Rollo, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Descripcion, "
   vResult = vResult & "CTE_FacturaMonedaLocal.Cantidad, "
   vResult = vResult & "CTE_FacturaMonedaLocal.PrecioSinIvaNetoConDescuentos AS PrecioVenta, "
   vResult = vResult & "CTE_FacturaMonedaLocal.PrecioCosto, "
   vResult = vResult & "CTE_FacturaMonedaLocal.TotalRenglonConDescuentos AS VentaTotal, "
   vResult = vResult & "CTE_FacturaMonedaLocal.PrecioCosto * CTE_FacturaMonedaLocal.Cantidad AS CostoTotal, "
   vResult = vResult & "(CTE_FacturaMonedaLocal.PrecioSinIvaNetoConDescuentos - CTE_FacturaMonedaLocal.PrecioCosto) * CTE_FacturaMonedaLocal.Cantidad AS Diferencia "
h_EXIT: On Error GoTo 0
   fSqlSelectFacturacionEntreFechaDesdeCte = vResult
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlSelectFacturacionEntreFechaDesdeCte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCteComprasOtrosGastosPorItem() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = "CTE_ComprasOtrosGastosPorItem AS (SELECT" & vbCrLf
   vSql = vSql & "   Numero," & vbCrLf
   vSql = vSql & "   Fecha," & vbCrLf
   vSql = vSql & "   CodigoArticulo," & vbCrLf
   vSql = vSql & "   ROUND((TotalOtrosGastos / (CASE WHEN SUM(CantidadRecibida) > 0 THEN SUM(CantidadRecibida) ELSE 1 END)), 2)  AS OtrosGastosPorItem " & vbCrLf
   vSql = vSql & "FROM CTE_ComprasTotalesSinOtrosGastosMonedaLocal " & vbCrLf
   vSql = vSql & "GROUP BY Numero, Fecha, CodigoArticulo, TotalOtrosGastos) " & vbCrLf
h_EXIT: On Error GoTo 0
   fSqlCteComprasOtrosGastosPorItem = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCteComprasOtrosGastosPorItem", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCteComprasTotalesSinOtrosGastosMonedaLocal(ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vSqlWhere As String
   On Error GoTo h_ERROR
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd("", "C.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "C.StatusCompra", enum_StatusCompra.eSCp_VIGENTE)
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)

   vSql = "CTE_ComprasTotalesSinOtrosGastosMonedaLocal AS (SELECT " & vbCrLf
   vSql = vSql & "C.Numero, " & vbCrLf
   vSql = vSql & "C.Fecha, " & vbCrLf
   vSql = vSql & "CDAI.CodigoArticulo, " & vbCrLf
   vSql = vSql & "ROUND(C.CambioABolivares * C.TotalOtrosGastos, 2) AS TotalOtrosGastos, " & vbCrLf
   vSql = vSql & "ROUND(C.CambioABolivares * CDAI.PrecioUnitario, 2) AS CostoUnitario, " & vbCrLf
   vSql = vSql & "ROUND(C.CambioABolivares * CDAI.CantidadRecibida * CDAI.PrecioUnitario, 2) As TotalCompraSinOtrosGastos, " & vbCrLf
   vSql = vSql & "CDAI.CantidadRecibida " & vbCrLf
   vSql = vSql & "FROM Adm.Compra C INNER JOIN Adm.CompraDetalleArticuloInventario CDAI " & vbCrLf
   vSql = vSql & "   ON C.ConsecutivoCompania = CDAI.ConsecutivoCompania " & vbCrLf
   vSql = vSql & "   AND C.Consecutivo = CDAI.ConsecutivoCompra" & vbCrLf
   vSql = vSql & vSqlWhere & ")" & vbCrLf
h_EXIT:  On Error GoTo 0
   fSqlCteComprasTotalesSinOtrosGastosMonedaLocal = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCteComprasTotalesSinOtrosGastosMonedaLocal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCteFacturaMonedaLocal(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String) As String
   Dim vSql As String
   Dim vSqlWhere As String
   Dim vPrecioSinIvaNtCnDesc As String
   Dim vTotRgConDescRg As String
   Dim vTotRengConDesc As String
   On Error GoTo h_ERROR
   vSqlWhere = "Factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_FACTURA)
   vSqlWhere = vSqlWhere & " OR Factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vSqlWhere = vSqlWhere & " OR Factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   vSqlWhere = vSqlWhere & " OR Factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vSqlWhere = vSqlWhere & " OR Factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd("(" & vSqlWhere & ")", "factura.ConsecutivoCompania", valConsecutivoCompania)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "Factura.StatusFactura", enum_StatusFactura.eSF_EMITIDA)
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "ArticuloInventario.TipoDeArticulo", enum_TipoDeArticulo.eTD_MERCANCIA)
   If (valcodigoCliente <> "") Then
      vSqlWhere = gUtilSQL.fSQLValueWithAnd(vSqlWhere, "Factura.CodigoCliente", valcodigoCliente, False)
   End If
   vSqlWhere = vSqlWhere & " AND " & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, valFechaFinal)
   
   vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
   
   vPrecioSinIvaNtCnDesc = "Factura.CambioABolivares * RenglonFactura.PrecioSinIVA"
   vPrecioSinIvaNtCnDesc = vPrecioSinIvaNtCnDesc & " * (1-ROUND(RenglonFactura.PorcentajeDescuento/100, 2))"

   vTotRgConDescRg = "Factura.CambioABolivares * RenglonFactura.PrecioSinIVA * RenglonFactura.Cantidad"
   vTotRgConDescRg = vTotRgConDescRg & " * (1-ROUND(RenglonFactura.PorcentajeDescuento/100, 2))"

   vTotRengConDesc = "Factura.CambioABolivares * RenglonFactura.PrecioSinIVA * RenglonFactura.Cantidad"
   vTotRengConDesc = vTotRengConDesc & " * (1-ROUND(RenglonFactura.PorcentajeDescuento/100, 2))"
   vTotRengConDesc = vTotRengConDesc & " * (1-ROUND(factura.PorcentajeDescuento/100, 2))"

   vSql = "CTE_FacturaMonedaLocal AS(SELECT " & vbCrLf
   vSql = vSql & "Factura.Numero, " & vbCrLf
   vSql = vSql & "Factura.Fecha, " & vbCrLf
   vSql = vSql & "Factura.CodigoCliente" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "Cliente.Nombre AS Cliente, " & vbCrLf
   vSql = vSql & "Factura.CodigoVendedor" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "Vendedor.Nombre AS Vendedor, " & vbCrLf
   vSql = vSql & "Factura.StatusFactura, " & vbCrLf
   vSql = vSql & "RenglonFactura.Articulo, " & vbCrLf
   vSql = vSql & "RenglonFactura.Serial, " & vbCrLf
   vSql = vSql & "RenglonFactura.Rollo, " & vbCrLf
   vSql = vSql & "RenglonFactura.Descripcion, " & vbCrLf
   vSql = vSql & "RenglonFactura.Cantidad, " & vbCrLf
   vSql = vSql & "RenglonFactura.PrecioSinIVA, " & vbCrLf
   vSql = vSql & "Factura.PorcentajeDescuento, " & vbCrLf
   vSql = vSql & "RenglonFactura.PorcentajeDescuento AS RenglonPorcentajeDescuento, " & vbCrLf
   vSql = vSql & "ROUND(" & vPrecioSinIvaNtCnDesc & ", 2) AS PrecioSinIvaNetoConDescuentos, " & vbCrLf
   vSql = vSql & "ROUND(" & vTotRgConDescRg & ", 2) AS TotalRenglonConDescuentoRenglon, " & vbCrLf
   vSql = vSql & "ROUND(" & vTotRengConDesc & ", 2) AS TotalRenglonConDescuentos, " & vbCrLf
   vSql = vSql & fSqlCteFacturaMonedaLocalCampoPrecioCosto() & " AS PrecioCosto " & vbCrLf
   vSql = vSql & "FROM Factura INNER JOIN RenglonFactura " & vbCrLf
   vSql = vSql & "ON Factura.ConsecutivoCompania = RenglonFactura.ConsecutivoCompania " & vbCrLf
   vSql = vSql & "AND Factura.Numero = RenglonFactura.NumeroFactura " & vbCrLf
   vSql = vSql & "AND Factura.TipoDeDocumento = RenglonFactura.TipoDeDocumento " & vbCrLf
   vSql = vSql & "INNER JOIN Vendedor " & vbCrLf
   vSql = vSql & "ON Factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania " & vbCrLf
   vSql = vSql & "AND Factura.CodigoVendedor = Vendedor.Codigo " & vbCrLf
   vSql = vSql & "INNER JOIN Cliente " & vbCrLf
   vSql = vSql & "ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania " & vbCrLf
   vSql = vSql & "AND Factura.CodigoCliente = Cliente.Codigo " & vbCrLf
   vSql = vSql & "INNER JOIN ArticuloInventario " & vbCrLf
   vSql = vSql & "ON RenglonFactura.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania " & vbCrLf
   vSql = vSql & "AND RenglonFactura.Articulo = ArticuloInventario.Codigo " & vbCrLf
   vSql = vSql & vSqlWhere
   vSql = vSql & ")" & vbCrLf
h_EXIT:  On Error GoTo 0
   fSqlCteFacturaMonedaLocal = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCteFacturaMonedaLocal", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCteFacturaMonedaLocalCampoPrecioCosto() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = vSql & "   ISNULL((SELECT TOP 1 CTE_PrecioCosto.PrecioCosto" & vbCrLf
   vSql = vSql & "         FROM CTE_PrecioCosto" & vbCrLf
   vSql = vSql & "         WHERE CTE_PrecioCosto.Fecha <= factura.Fecha And CTE_PrecioCosto.CodigoArticulo = RenglonFactura.Articulo" & vbCrLf
   vSql = vSql & "         ORDER BY CTE_PrecioCosto.Fecha DESC, CTE_PrecioCosto.Numero DESC), 0) "
h_EXIT:  On Error GoTo 0
   fSqlCteFacturaMonedaLocalCampoPrecioCosto = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlCteFacturaMonedaLocalCampoPrecioCosto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlDelReporteIGTFdesdeOtrosCargosYDescuentosFactura(ByVal valConsecutivoCompania As Integer, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCodigoCargo As String, ByVal valCodigoMonedaExtranjera As String)
   Dim Sql As String
   Dim vSqlCambio As String
   On Error GoTo h_ERROR
   vSqlCambio = "(SELECT TOP 1 CambioABolivares FROM Cambio WHERE CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(valCodigoMonedaExtranjera) & " AND FechaDeVigencia <= Factura.Fecha ORDER BY FechaDeVigencia DESC)"
   
   Sql = "SELECT "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & "Factura.Numero AS NumeroFactura, "
   Sql = Sql & "Factura.TipoDeDocumento, "
   Sql = Sql & "RenglonDetalleDeOtrosCargosFactura.CodigoDeCargo AS CodigoOtrosCargos, "
   Sql = Sql & "RenglonDetalleDeOtrosCargosFactura.Descripcion AS DescripcionOtrosCargos, "
   Sql = Sql & gUtilSQL.getIIF("Factura.CambioMostrarTotalEnDivisas = 1", gUtilSQL.getIIF("Factura.CambioABolivares = 1", vSqlCambio, "factura.CambioABolivares"), "factura.CambioMostrarTotalEnDivisas") & " AS Cambio, "
   Sql = Sql & "ROUND((ROUND(Factura.CambioABolivares * RenglonDetalleDeOtrosCargosFactura.TotalRenglon, 2) * 100 / 3), 2) AS BIIGTF, "
   Sql = Sql & "ROUND(Factura.CambioABolivares * RenglonDetalleDeOtrosCargosFactura.TotalRenglon, 2) AS IGTF "
   Sql = Sql & " FROM Factura"
   Sql = Sql & " INNER JOIN RenglonDetalleDeOtrosCargosFactura"
   Sql = Sql & " ON Factura.TipoDeDocumento = RenglonDetalleDeOtrosCargosFactura.TipoDeDocumento"
   Sql = Sql & " AND Factura.Numero = RenglonDetalleDeOtrosCargosFactura.NumeroFactura"
   Sql = Sql & " AND Factura.ConsecutivoCompania = RenglonDetalleDeOtrosCargosFactura.ConsecutivoCompania"
   Sql = Sql & " WHERE factura.ConsecutivoCompania = " & gConvert.fConvierteAString(valConsecutivoCompania)
   Sql = Sql & " AND " & gUtilSQL.fSQLValueDatesBetween("", "Factura.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND RenglonDetalleDeOtrosCargosFactura.CodigoDeCargo = " & gUtilSQL.fSimpleSqlValue(valCodigoCargo)
   Sql = Sql & " AND Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   Sql = Sql & " ORDER BY Factura.Fecha, Factura.Numero, factura.TipoDeDocumento"
   fSqlDelReporteIGTFdesdeOtrosCargosYDescuentosFactura = Sql
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDelReporteIGTFdesdeOtrosCargosYDescuentosFactura", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCteComprasPecioCostoPorItem() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = "CTE_PrecioCosto AS (SELECT"
   vSql = vSql & "   CTE_ComprasTotalesSinOtrosGastosMonedaLocal.Numero," & vbCrLf
   vSql = vSql & "   CTE_ComprasTotalesSinOtrosGastosMonedaLocal.Fecha," & vbCrLf
   vSql = vSql & "   CTE_ComprasTotalesSinOtrosGastosMonedaLocal.CodigoArticulo," & vbCrLf
   vSql = vSql & "   CTE_ComprasTotalesSinOtrosGastosMonedaLocal.CostoUnitario + CTE_ComprasOtrosGastosPorItem.OtrosGastosPorItem AS PrecioCosto" & vbCrLf
   vSql = vSql & "FROM CTE_ComprasTotalesSinOtrosGastosMonedaLocal INNER JOIN CTE_ComprasOtrosGastosPorItem" & vbCrLf
   vSql = vSql & "   ON CTE_ComprasTotalesSinOtrosGastosMonedaLocal.Numero = CTE_ComprasOtrosGastosPorItem.Numero" & vbCrLf
   vSql = vSql & "   AND CTE_ComprasTotalesSinOtrosGastosMonedaLocal.CodigoArticulo = CTE_ComprasOtrosGastosPorItem.CodigoArticulo)" & vbCrLf
   fSqlCteComprasPecioCostoPorItem = vSql
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCteComprasPecioCostoPorItem", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteEstadisticoDeVentasNew(ByVal valConsecutivoCompania As Integer, ByVal valUsaCambio As Boolean, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valHoyNombreMoneda As String) As String
   Dim vSql As String
   Dim vSqlCambio As String
   Dim vSqlCTEBaseRenglonFactura As String
   Dim vSqlCTEArticulos As String
   On Error GoTo h_ERROR
   
   vSqlCTEBaseRenglonFactura = "CTE_BaseRenglonFactura AS ("
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "SELECT "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   renglonFactura.Articulo, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   renglonFactura.Descripcion, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   renglonFactura.Cantidad AS Cantidad, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   factura.CodigoMoneda, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   factura.Moneda, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   factura.CambioABolivares, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   ROUND(ROUND(ROUND((renglonFactura.PrecioSinIVA + ROUND((renglonFactura.PrecioSinIVA * (renglonFactura.PorcentajeAlicuota / 100)), 2)) * renglonFactura.Cantidad, 2) * (1 - ROUND((renglonFactura.PorcentajeDescuento / 100), 2)), 2) * (1 - ROUND((factura.PorcentajeDescuento / 100), 2)), 2) AS MontoConIva, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   ROUND(ROUND(renglonFactura.PrecioSinIVA * renglonFactura.Cantidad, 2) * ROUND((1 - ROUND((renglonFactura.PorcentajeDescuento / 100), 2)), 2) * (1 - ROUND((factura.PorcentajeDescuento / 100),2)), 2) AS MontoSinIva, "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "   ROUND(renglonFactura.precioConIva * renglonFactura.Cantidad, 2) AS MontoBrutoFactura "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & " FROM factura INNER JOIN "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "      renglonFactura ON factura.Numero = renglonFactura.NumeroFactura AND "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & "      factura.TipoDeDocumento = renglonFactura.TipoDeDocumento AND factura.consecutivoCompania = renglonFactura.consecutivoCompania "
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & " WHERE factura.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & " AND factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & " AND  " & gUtilSQL.fSQLValueDatesBetween("", "factura.Fecha ", valFechaInicial, valFechaFinal)
   vSqlCTEBaseRenglonFactura = vSqlCTEBaseRenglonFactura & ")"
   
   vSqlCTEArticulos = "CTE_Articulos AS ("
   vSqlCTEArticulos = vSqlCTEArticulos & "SELECT"
   vSqlCTEArticulos = vSqlCTEArticulos & "   ArticuloInventario.Categoria,"
   vSqlCTEArticulos = vSqlCTEArticulos & "   ExistenciaPorGrupo.CodigoArticulo + ISNULL(ExistenciaPorGrupo.CodigoColor, '') + ISNULL(ExistenciaPorGrupo.CodigoTalla, '') AS CodigoArticulo "
   vSqlCTEArticulos = vSqlCTEArticulos & " FROM ExistenciaPorGrupo INNER JOIN"
   vSqlCTEArticulos = vSqlCTEArticulos & "      ArticuloInventario ON ExistenciaPorGrupo.ConsecutivoCompania = ArticuloInventario.ConsecutivoCompania AND ExistenciaPorGrupo.CodigoArticulo = ArticuloInventario.Codigo"
   vSqlCTEArticulos = vSqlCTEArticulos & "     LEFT JOIN Saw.Talla ON ExistenciaPorGrupo.ConsecutivoCompania = Saw.Talla.ConsecutivoCompania AND ExistenciaPorGrupo.CodigoTalla = Saw.Talla.CodigoTalla"
   vSqlCTEArticulos = vSqlCTEArticulos & "     LEFT JOIN Saw.Color ON ExistenciaPorGrupo.ConsecutivoCompania = Saw.Color.ConsecutivoCompania AND ExistenciaPorGrupo.CodigoColor = Saw.Color.CodigoColor"
   vSqlCTEArticulos = vSqlCTEArticulos & " WHERE ExistenciaPorGrupo.ConsecutivoCompania = " & valConsecutivoCompania
   vSqlCTEArticulos = vSqlCTEArticulos & " AND ArticuloInventario.TipoArticuloInv <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple)
   vSqlCTEArticulos = vSqlCTEArticulos & " UNION "
   vSqlCTEArticulos = vSqlCTEArticulos & " SELECT "
   vSqlCTEArticulos = vSqlCTEArticulos & "   Categoria, "
   vSqlCTEArticulos = vSqlCTEArticulos & "   Codigo "
   vSqlCTEArticulos = vSqlCTEArticulos & " FROM ArticuloInventario"
   vSqlCTEArticulos = vSqlCTEArticulos & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
   vSqlCTEArticulos = vSqlCTEArticulos & " AND TipoArticuloInv = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoArticuloInv.eTAI_Simple)
   vSqlCTEArticulos = vSqlCTEArticulos & ") "
   
   If valUsaCambio Then
      vSqlCambio = " RF.CambioABolivares "
   Else
      vSqlCambio = " 1 "
   End If
   
   vSql = ";WITH "
   vSql = vSql & vSqlCTEBaseRenglonFactura & ", " & vbCrLf
   vSql = vSql & vSqlCTEArticulos & vbCrLf & vbCrLf
   
   vSql = vSql & " SELECT "
   If valUsaCambio Then
      vSql = vSql & gUtilSQL.fSimpleSqlValue(valHoyNombreMoneda) & " AS MonedaReporte, "
   Else
      vSql = vSql & " RF.Moneda AS MonedaReporte, "
   End If
   vSql = vSql & "   AI.Categoria AS TipoCategoria, "
   vSql = vSql & "   SUM(RF.Cantidad) AS Cantidad, "
   vSql = vSql & "   SUM(ROUND(RF.MontoConIva * " & vSqlCambio & ", 2)) AS MontoConIva, "
   vSql = vSql & "   SUM(ROUND(RF.MontoSinIva * " & vSqlCambio & ", 2)) AS MontoSinIva, "
   vSql = vSql & "   SUM(ROUND(RF.MontoBrutoFactura * " & vSqlCambio & ", 2)) AS MontoBrutoFactura "
   vSql = vSql & " FROM CTE_BaseRenglonFactura RF INNER JOIN CTE_Articulos AI ON RF.Articulo = AI.CodigoArticulo "
   If valUsaCambio Then
      vSql = vSql & " GROUP BY Categoria "
      vSql = vSql & " ORDER BY MonedaReporte, Categoria "
   Else
      vSql = vSql & " GROUP BY RF.Moneda, Categoria "
      vSql = vSql & " ORDER BY RF.Moneda, Categoria "
   End If
   
   fSQLDelReporteEstadisticoDeVentasNew = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteEstadisticoDeVentasNew", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteComparativoCategVendXMeses(ByVal valConsecutivoCompania As Integer, ByVal valMesInicial As Integer, ByVal valMesFinal As Integer, ByVal valAno As Integer) As String
   Dim Sql As String
   Dim i As Integer
   On Error GoTo h_ERROR
   Sql = "SELECT "
   For i = valMesInicial To valMesFinal
      Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate("factura.Fecha") & " = " & gConvert.fConvierteAString(i), "SUM(renglonFactura.Cantidad)", "0") & " AS MES" & gConvert.fConvierteAString(i) & ", "
   Next
   Sql = Sql & gUtilSQL.DfUcaseSQL("articuloInventario.Categoria") & " AS CategoriaDeProducto, "
   Sql = Sql & "articuloInventario.TipoDeProducto AS TipoProducto "
   
   Sql = Sql & "FROM articuloInventario INNER JOIN renglonFactura ON "
   Sql = Sql & "articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND "
   Sql = Sql & "articuloInventario.Codigo = renglonFactura.Articulo INNER JOIN factura ON "
   Sql = Sql & "renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND "
   Sql = Sql & "renglonFactura.NumeroFactura = factura.Numero "
   
   Sql = Sql & "WHERE "
   Sql = Sql & gUtilSQL.DfSQLMonthOfDate("factura.Fecha") & " BETWEEN " & valMesInicial & " AND " & valMesFinal & " AND "
   Sql = Sql & "factura.StatusFactura = " & gConvert.enumerativoAChar(eSF_EMITIDA) & " AND "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("factura.Fecha") & " = " & valAno & " AND "
   Sql = Sql & "factura.ConsecutivoCompania = " & valConsecutivoCompania
   
   Sql = Sql & " GROUP BY "
   Sql = Sql & "articuloInventario.TipoDeProducto, "
   Sql = Sql & "articuloInventario.Categoria, "
   Sql = Sql & gUtilSQL.DfSQLMonthOfDate("factura.Fecha")
   
   Sql = Sql & " ORDER BY "
   Sql = Sql & "articuloInventario.Categoria, "
   Sql = Sql & "articuloInventario.TipoDeProducto, "
   Sql = Sql & gUtilSQL.DfSQLMonthOfDate("factura.Fecha")
   fConstruirSQLDelReporteComparativoCategVendXMeses = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteComparativoCategVendXMeses", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteComparativoCategVendXAño(ByVal valConsecutivoCompania As Integer, ByVal valAnoInicial As Integer, ByVal valAnoFinal As Integer, ByVal valMes As Integer) As String
   Dim fechaFinal As String
   Dim UltimoDiaDelMesHasta As Integer
   Dim Sql As String
   Dim i As Integer
   Dim numerodeanos As Integer
   Dim anocolumnas As Integer
   On Error GoTo h_ERROR
   UltimoDiaDelMesHasta = gUtilDate.LastDayOfTheMonth(valMes, valAnoFinal)
   fechaFinal = gTexto.DfCStr(UltimoDiaDelMesHasta) & "/" & valMes & "/" & valAnoFinal
   numerodeanos = valAnoFinal - valAnoInicial
   anocolumnas = valAnoInicial
   Sql = "SELECT "
   For i = 0 To numerodeanos
      Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfSQLYearOfDate("factura.Fecha") & " = " & gConvert.fConvierteAString(anocolumnas), "SUM(renglonFactura.Cantidad)", "0") & " AS ano" & gConvert.fConvierteAString(i) & ", "
      anocolumnas = anocolumnas + 1
   Next
   
   Sql = Sql & gUtilSQL.DfUcaseSQL("articuloInventario.Categoria") & " AS CategoriaDeProducto, "
   Sql = Sql & "articuloInventario.TipoDeProducto AS TipoProducto "
   
   Sql = Sql & "FROM articuloInventario INNER JOIN renglonFactura ON "
   Sql = Sql & "articuloInventario.ConsecutivoCompania = renglonFactura.ConsecutivoCompania AND "
   Sql = Sql & "articuloInventario.Codigo = renglonFactura.Articulo "
   Sql = Sql & "INNER JOIN factura ON "
   Sql = Sql & "renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND "
   Sql = Sql & "renglonFactura.NumeroFactura = factura.Numero "
   
   Sql = Sql & "WHERE "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("factura.Fecha") & " BETWEEN " & valAnoInicial & " AND " & valAnoFinal
   Sql = Sql & " AND factura.StatusFactura = " & gConvert.enumerativoAChar(eSF_EMITIDA) & " AND "
   Sql = Sql & gUtilSQL.DfSQLMonthOfDate("factura.Fecha") & " <= " & valMes & " AND "
   Sql = Sql & "factura.ConsecutivoCompania = " & valConsecutivoCompania
   
   Sql = Sql & " GROUP BY "
   Sql = Sql & "articuloInventario.TipoDeProducto, "
   Sql = Sql & "articuloInventario.Categoria, "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
   Sql = Sql & " ORDER BY "
   Sql = Sql & "articuloInventario.Categoria, "
   Sql = Sql & "articuloInventario.TipoDeProducto, "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("factura.Fecha")
   fConstruirSQLDelReporteComparativoCategVendXAño = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteComparativoCategVendXAño", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDetailRenglonFacturaDesglosado(ByVal valConsecutivoCompania As Long, ByVal valNumero As String, ByVal valTipoDeDocumento As enum_TipoDocumentoFactura) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.getIIF("renglonFactura.Articulo = ProductoCompuesto.CodigoConexionConElMaster", "ProductoCompuesto.CodigoArticulo", "renglonFactura.articulo", True) & " AS ArticuloFinal, "
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("renglonFactura.Articulo = ProductoCompuesto.CodigoConexionConElMaster", "(renglonFactura.Cantidad * ProductoCompuesto.Cantidad)", "renglonFactura.Cantidad", False) & ") AS CantidadFinal, "
   Sql = Sql & "ISNULL(renglonFactura.Serial, '0') AS Serial, "
   Sql = Sql & "ISNULL(renglonFactura.Rollo, '0') AS Rollo, "
   Sql = Sql & "ISNULL(renglonFactura.LoteDeInventario, '') AS LoteDeInventario "
   Sql = Sql & " FROM renglonFactura LEFT JOIN ProductoCompuesto "
   Sql = Sql & " ON renglonFactura.Articulo = ProductoCompuesto.CodigoConexionConElMaster AND renglonFactura.ConsecutivoCompania = ProductoCompuesto.ConsecutivoCompania "
   Sql = Sql & " WHERE renglonFactura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND renglonFactura.NumeroFactura = " & gUtilSQL.fSimpleSqlValue(valNumero)
   Sql = Sql & " AND renglonFactura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(valTipoDeDocumento)
   Sql = Sql & " GROUP BY renglonFactura.Articulo, ProductoCompuesto.CodigoConexionConElMaster, ProductoCompuesto.CodigoArticulo, renglonFactura.Cantidad, ProductoCompuesto.Cantidad, renglonFactura.Serial, renglonFactura.Rollo, renglonFactura.LoteDeInventario"
   Sql = Sql & " ORDER BY renglonFactura.Articulo"
   fSQLDetailRenglonFacturaDesglosado = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDetailRenglonFacturaDesglosado", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fEvaluaYPreparaMensajeDeErroEnComunicacionConCRM(ByVal valRespuesta As String, ByVal valAccion As String, ByVal valError As String, ByVal valNumero As String) As String
   Dim vMensaje As String
   On Error GoTo h_ERROR
   vMensaje = "Se produjo un error en fecha: " & gUtilDate.getFechaDeHoy & " a las " & gUtilDate.getActualTime
   If gTexto.DfLen(valError) = 0 Then
      If (valAccion = "EMITIR") Then
         vMensaje = vMensaje & ", al EMITIR la factura Nro:" & valNumero & vbCrLf & "Nro de Error: " & valRespuesta & vbCrLf
         Select Case valRespuesta
            Case "500"
               vMensaje = vMensaje & "Problemas de conexión con el CRM o el usuario en uso ha perdido los privilegios para ejecutar la actualización."
            Case "505"
               vMensaje = vMensaje & "GUID vacío o faltante."
            Case "506"
               vMensaje = vMensaje & "Número de Factura Faltante."
            Case "507"
               vMensaje = vMensaje & "Número de control Faltante o vacío."
            Case "508"
               vMensaje = vMensaje & "Fecha de emisión de la factura vacío o faltante."
            Case "509"
               vMensaje = vMensaje & "Código de cliente errado."
            Case "510"
               vMensaje = vMensaje & "Usuario creador de la Factura (uname) está vacío o nulo."
            Case Else
               vMensaje = vMensaje & "Error no manejado."
         End Select
      ElseIf (valAccion = "ANULAR") Then
         vMensaje = vMensaje & ", al ANULAR la factura Nro:" & valNumero & vbCrLf & "Nro de Error: " & valRespuesta & vbCrLf
         Select Case valRespuesta
            Case "503"
               vMensaje = vMensaje & "La factura ya se encuentra anulada."
            Case "502"
               vMensaje = vMensaje & "La factura no existe."
            Case Else
               vMensaje = vMensaje & "Error no manejado"
         End Select
      End If
   Else
      vMensaje = vMensaje & vbCrLf & valError
   End If
   vMensaje = vMensaje & vbCrLf & " Es recomendable que revise la conexión con el CRM y los datos enviados"
   vMensaje = vMensaje & vbCrLf & "========================================================================"
   fEvaluaYPreparaMensajeDeErroEnComunicacionConCRM = vMensaje
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEvaluaYPreparaMensajeDeErroEnComunicacionConCRM", CM_FILE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

