VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCotizacionSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCotizacionSQL"
Private Const CM_MESSAGE_NAME As String = "Cotizacion SQL"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConstruirSQLDelReporteCotizacionesPorVendedorYLinea(ByVal valFechaInicial As Date, _
                                                                      ByVal valFechaFinal As Date, _
                                                                       ByVal valCantidadAImprimir As String, _
                                                                        ByVal valcodigoVendedor As String, _
                                                                         ByVal valConsecutivoCompaniaActual As Long, _
                                                                          ByVal valTipoGrupo As Boolean, _
                                                                           ByVal ReporteEnMonedaLocal As Boolean, _
                                                                            ByVal gMonedaLocalActual As Object, _
                                                                             ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlBaseImponible As String
   Dim sqlDescuento As String
   Dim SQLMontoBruto As String
   Dim SQLIva1, SQLIva2, SQLIva3 As String
   Dim sqlPorcIva1, sqlPorcIva2, sqlPorcIva3 As String
   Dim sqlTotal, sqlTotal1, sqlTotal2, sqlTotal3 As String
   Dim sqlMontoTotal1, sqlMontoTotal2, sqlMontoTotal3 As String
   Dim SQLPorcIva As String
   Dim sqlMonto As String
   Dim sqlSelectIva1, sqlSelectIva2, sqlSelectIva3 As String
   On Error GoTo h_ERROR
   sqlSelectIva1 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuotaGeneral FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   sqlSelectIva2 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuota2 FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   sqlSelectIva3 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuota3 FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"

   sqlBaseImponible = "SUM(((renglonCotizacion.PrecioSinIVA * renglonCotizacion.Cantidad) * (1 - renglonCotizacion.PorcentajeDescuento / 100)) * (1 - cotizacion.PorcentajeDescuento / 100)) "
   sqlDescuento = "((renglonCotizacion.PrecioSinIVA * renglonCotizacion.Cantidad) - (((renglonCotizacion.PrecioSinIVA * renglonCotizacion.Cantidad) * (1 - renglonCotizacion.PorcentajeDescuento / 100)) * (1 - cotizacion.PorcentajeDescuento / 100)))"
   SQLMontoBruto = sqlDescuento & " + " & sqlBaseImponible
   
   sqlPorcIva1 = "(SELECT "
   sqlPorcIva1 = sqlPorcIva1 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuotaGeneral) > 0", sqlSelectIva1, 0)
   sqlPorcIva1 = sqlPorcIva1 & " FROM " & sqlSelectIva1 & " AS alicuotaIVA)"
   
   sqlPorcIva2 = "(SELECT "
   sqlPorcIva2 = sqlPorcIva2 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuota2) > 0", sqlSelectIva2, 0)
   sqlPorcIva2 = sqlPorcIva2 & " FROM " & sqlSelectIva2 & " AS alicuotaIVA)"

   sqlPorcIva3 = "(SELECT "
   sqlPorcIva3 = sqlPorcIva3 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuota3) > 0", sqlSelectIva3, 0)
   sqlPorcIva3 = sqlPorcIva3 & " FROM " & sqlSelectIva3 & " AS alicuotaIVA)"
   
   SQLIva1 = "ROUND((" & sqlBaseImponible & ") * (" & sqlPorcIva1 & "/100),2)"
   SQLIva2 = "ROUND((" & sqlBaseImponible & ") * (" & sqlPorcIva2 & "/100),2)"
   SQLIva3 = "ROUND((" & sqlBaseImponible & ") * (" & sqlPorcIva3 & "/100),2)"
      
   sqlTotal = "(" & SQLMontoBruto & " - " & sqlDescuento & ")"
   sqlTotal1 = "(" & SQLMontoBruto & " - " & sqlDescuento & " + " & SQLIva1 & ")"
   sqlTotal2 = "(" & SQLMontoBruto & " - " & sqlDescuento & " + " & SQLIva2 & ")"
   sqlTotal3 = "(" & SQLMontoBruto & " - " & sqlDescuento & " + " & SQLIva3 & ")"
  
   SQL = "SELECT vendedor.Codigo, vendedor.Nombre, "
   SQL = SQL & " articuloInventario.LineaDeProducto, "
   SQL = SQL & " cotizacion.Fecha, "
   SQL = SQL & " cotizacion.Numero, "
   SQL = SQL & " cliente.Nombre AS NombreCliente, "
   SQL = SQL & " articuloInventario.Descripcion, "
   
   If ReporteEnMonedaLocal Then
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & ") AS MontoBruto, "
                     
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlDescuento, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlDescuento, "Cotizacion.Fecha"), True) & ") AS Descuento, "
         
      SQL = SQL & gUtilSQL.getIIF("cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlBaseImponible, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlBaseImponible, "Cotizacion.Fecha"), True) & " AS BaseImponible, "
   
         
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
          gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), _
              gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva1, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva1, "Cotizacion.Fecha")), _
         gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                        "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva2, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva2, "Cotizacion.Fecha")), _
         gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                 "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva3, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva3, "Cotizacion.Fecha"))))) _
           & " AS MontoIVA, "
           
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal, False, ""), _
                gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal, "Cotizacion.Fecha")), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal1, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal1, "Cotizacion.Fecha")), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal2, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal2, "Cotizacion.Fecha")), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal3, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal3, "Cotizacion.Fecha")))))
      SQL = SQL & " AS MontoTotal, "
      
      SQL = SQL & gUtilSQL.getIIF("cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & " AS MontoBruto, "
                                         
      SQL = SQL & gUtilSQL.getIIF("cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                     "Cotizacion.Moneda", True) & " As Moneda "
   Else
      SQL = SQL & SQLMontoBruto & " AS Monto, "
      SQL = SQL & sqlDescuento & " AS Descuento,"
      SQL = SQL & sqlBaseImponible & " as BaseImponible, "
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), SQLIva1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), SQLIva2, SQLIva3)))
      SQL = SQL & " AS MontoIVA, "
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), sqlTotal, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), sqlTotal1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), sqlTotal2, sqlTotal3)))
      SQL = SQL & " AS MontoTotal, "
      SQL = SQL & SQLMontoBruto & " as MontoBruto, "
      SQL = SQL & "Cotizacion.Moneda AS Moneda "
   End If
  
   SQL = SQL & " FROM renglonCotizacion INNER JOIN"
   SQL = SQL & " cotizacion ON renglonCotizacion.ConsecutivoCompania = cotizacion.ConsecutivoCompania AND"
   SQL = SQL & " renglonCotizacion.NumeroCotizacion = cotizacion.Numero "
   SQL = SQL & " INNER JOIN articuloInventario ON renglonCotizacion.CodigoArticulo = articuloInventario.Codigo AND"
   SQL = SQL & " renglonCotizacion.ConsecutivoCompania = articuloInventario.ConsecutivoCompania "
   SQL = SQL & " INNER JOIN vendedor ON cotizacion.ConsecutivoCompania = vendedor.ConsecutivoCompania "
   SQL = SQL & " AND cotizacion.CodigoVendedor = vendedor.Codigo "
   SQL = SQL & " INNER JOIN cliente ON cotizacion.ConsecutivoCompania = cliente.ConsecutivoCompania "
   SQL = SQL & " AND cotizacion.CodigoCliente = cliente.Codigo"
   SQL = SQL & " GROUP BY vendedor.Codigo, vendedor.Nombre, cotizacion.Fecha, cotizacion.Numero, cliente.Nombre, articuloInventario.LineaDeProducto, Cotizacion.Moneda, "
   SQL = SQL & " articuloInventario.Descripcion, cotizacion.ConsecutivoCompania, renglonCotizacion.PrecioSinIVA, Cotizacion.PorcentajeDescuento,"
   SQL = SQL & " renglonCotizacion.PrecioConIVA, renglonCotizacion.Cantidad, renglonCotizacion.PorcentajeDescuento, articuloInventario.Descripcion,"
   SQL = SQL & " articuloInventario.AlicuotaIVA"
   SQL = SQL & " HAVING  (" & gUtilSQL.DfSQLDateValueBetween("cotizacion.Fecha", valFechaInicial, valFechaFinal) & ") "
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND vendedor.Codigo = '" & gTexto.fLimpiaStringdeBlancosAAmbosLados(valcodigoVendedor) & "'"
   End If
   SQL = SQL & " AND (cotizacion.ConsecutivoCompania = " & valConsecutivoCompaniaActual & ")"
   SQL = SQL & " ORDER BY "
   If valTipoGrupo Then
      SQL = SQL & "moneda, vendedor.Nombre, articuloInventario.LineaDeProducto, cotizacion.Fecha, cotizacion.Numero "
   Else
      SQL = SQL & "moneda, articuloInventario.LineaDeProducto, vendedor.Nombre, cotizacion.Fecha, cotizacion.Numero  "
   End If
   fConstruirSQLDelReporteCotizacionesPorVendedorYLinea = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteCotizacionesPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesEntreFechas(ByVal ReporteEnMonedaLocal As Boolean, _
                                                                ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                  ByVal valFechaFinal As Date, _
                                                                   ByVal gUltimaTasaDeCambio As Object, _
                                                                    ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "Cotizacion.Fecha, "
   SQL = SQL & "Cotizacion.Numero, "
   SQL = SQL & "Cotizacion.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre, "
   SQL = SQL & "Cotizacion.Contacto, "
   If ReporteEnMonedaLocal Then
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                        gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), "Cotizacion.Moneda", True) & " AS Moneda, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalRenglones", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalRenglones", False, ""), True) & " AS MontoBruto, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalBaseImponible", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalBaseImponible", False, ""), True) & " AS TotalBaseImponible, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalIVA", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalIVA", False, ""), True) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), True) & " AS TotalFactura, "
   Else
      SQL = SQL & "Cotizacion.Moneda AS Moneda, "
      SQL = SQL & "Cotizacion.TotalRenglones AS MontoBruto, "
      SQL = SQL & "Cotizacion.TotalBaseImponible AS TotalBaseImponible, "
      SQL = SQL & "Cotizacion.TotalIVA AS TotalIVA, "
      SQL = SQL & "Cotizacion.TotalCotizacion AS TotalFactura, "
   End If
   SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", 1, False, "CambioABolivares")

   SQL = SQL & " FROM Cliente INNER JOIN Cotizacion"
   SQL = SQL & " ON (Cliente.Codigo = Cotizacion.CodigoCliente)"
   SQL = SQL & " AND (Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   SQL = SQL & " WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " ORDER BY"
   SQL = SQL & " Moneda, "
   SQL = SQL & " Cotizacion.Fecha, "
   SQL = SQL & " Cotizacion.Numero"
   fConstruirSqlDelReporteCotizacionesEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDelReporteCotizacionesEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesPorDia(ByVal valEfecturaCambioABs As Boolean, _
                                                           ByVal valConsecutivoCompania As String, _
                                                            ByVal valFechaInicial As Date, _
                                                             ByVal valFechaFinal As Date, _
                                                              ByVal gUltimaTasaDeCambio As Object, _
                                                               ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT Cotizacion.Fecha, "
   If valEfecturaCambioABs Then
      SQL = SQL & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
      
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                              "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                              gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalRenglones", "Cotizacion.Fecha"), _
                              gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalRenglones", False, ""), True) & ") AS MontoBruto, "

      
      SQL = SQL & "MAX(" & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                              "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                              gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), _
                              gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), True) & ") AS CotizacionMayor, "
      
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                              "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                              gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), _
                              gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), True) & ") AS MontoTotal, "
   Else
      SQL = SQL & "Cotizacion.moneda AS Moneda, "
      SQL = SQL & "SUM(Cotizacion.TotalRenglones) AS MontoBruto, "
      SQL = SQL & "MAX(Cotizacion.TotalCotizacion) AS CotizacionMayor, "
      SQL = SQL & "SUM(Cotizacion.TotalCotizacion) AS MontoTotal, "
   End If
   SQL = SQL & "COUNT(Cotizacion.CodigoCliente) AS NumeroDeCotizaciones "
   SQL = SQL & " FROM Cotizacion"
   SQL = SQL & " WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If valEfecturaCambioABs Then
      SQL = SQL & " GROUP BY Cotizacion.Fecha"
      SQL = SQL & " ORDER BY Cotizacion.Fecha"
   Else
      SQL = SQL & " GROUP BY Moneda, Cotizacion.Fecha"
      SQL = SQL & " ORDER BY Moneda, Cotizacion.Fecha"
   End If
   fConstruirSqlDelReporteCotizacionesPorDia = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDelReporteCotizacionesPorDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesPorUsuario(ByVal valEfecturaCambioABs As Boolean, _
                                                               ByVal valConsecutivoCompania As String, _
                                                                ByVal valFechaInicial As Date, _
                                                                 ByVal valFechaFinal As Date, _
                                                                  ByVal ValtxtNombreOperador As String, _
                                                                   ByVal ValCmbCantidadAImprimir As Integer, _
                                                                    ByVal gUltimaTasaDeCambio As Object, _
                                                                     ByVal gMonedaLocalActual As Object, _
                                                                      ByRef insAlicuota As Object) As String
   Dim SQL As String
   Dim SQLCodigo As String
   Dim SQLCodigoArticulo As String
   Dim SQLMontoBruto As String
   Dim SQLDesc As String
   Dim SQLIva1, SQLIva2, SQLIva3 As String
   Dim sqlTotal, sqlTotal1, sqlTotal2, sqlTotal3 As String
   Dim sqlPorcIva1, sqlPorcIva2, sqlPorcIva3 As String
   Dim sqlMontoTotal As String
   Dim sqlSelectIva1, sqlSelectIva2, sqlSelectIva3 As String
   On Error GoTo h_ERROR
   sqlSelectIva1 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuotaGeneral FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   sqlSelectIva2 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuota2 FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   sqlSelectIva3 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuota3 FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   
   SQLCodigo = gUtilSQL.getIIF("renglonCotizacion.Serial <> " & gUtilSQL.fSimpleSqlValue(0), "renglonCotizacion.CodigoArticulo" & gUtilSQL.CharConcat() & " ' Serial ' " & gUtilSQL.CharConcat() & "renglonCotizacion.Serial" & gUtilSQL.CharConcat() & " ' Rollo ' " & gUtilSQL.CharConcat() & " renglonCotizacion.Rollo", "renglonCotizacion.CodigoArticulo ", True)
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonCotizacion.Serial <> " & gUtilSQL.fSimpleSqlValue(0) & " AND renglonCotizacion.rollo = " & gUtilSQL.fSimpleSqlValue(0), "renglonCotizacion.CodigoArticulo " & gUtilSQL.CharConcat() & " ' Serial ' " & gUtilSQL.CharConcat() & "renglonCotizacion.Serial", SQLCodigo, True) & " AS CodigoArticulo, "
   SQLMontoBruto = "(renglonCotizacion.Cantidad * renglonCotizacion.PrecioSinIVA)"

   SQLDesc = "(" & SQLMontoBruto & " - ((" & SQLMontoBruto & " * (1 - renglonCotizacion.PorcentajeDescuento / 100)) * (1 - cotizacion.PorcentajeDescuento / 100)))"
   sqlMontoTotal = "(" & SQLMontoBruto & " - " & SQLDesc & ")"
   
   sqlPorcIva1 = "(SELECT "
   sqlPorcIva1 = sqlPorcIva1 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuotaGeneral) > 0", sqlSelectIva1, 0)
   sqlPorcIva1 = sqlPorcIva1 & " FROM " & sqlSelectIva1 & " AS alicuotaIVA)"
   
   sqlPorcIva2 = "(SELECT "
   sqlPorcIva2 = sqlPorcIva2 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuota2) > 0", sqlSelectIva2, 0)
   sqlPorcIva2 = sqlPorcIva2 & " FROM " & sqlSelectIva2 & " AS alicuotaIVA)"

   sqlPorcIva3 = "(SELECT "
   sqlPorcIva3 = sqlPorcIva3 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuota3) > 0", sqlSelectIva3, 0)
   sqlPorcIva3 = sqlPorcIva3 & " FROM " & sqlSelectIva3 & " AS alicuotaIVA)"

   SQLIva1 = "((" & sqlMontoTotal & " * " & sqlPorcIva1 & ")/100)"
   SQLIva2 = "((" & sqlMontoTotal & " * " & sqlPorcIva2 & ")/100)"
   SQLIva3 = "((" & sqlMontoTotal & " * " & sqlPorcIva3 & ")/100)"
   
   sqlTotal = "(" & SQLMontoBruto & " - " & SQLDesc & ")"
   sqlTotal1 = "(" & SQLMontoBruto & " - " & SQLDesc & " + " & SQLIva1 & ")"
   sqlTotal2 = "(" & SQLMontoBruto & " - " & SQLDesc & " + " & SQLIva2 & ")"
   sqlTotal3 = "(" & SQLMontoBruto & " - " & SQLDesc & " + " & SQLIva3 & ")"
   
   SQL = "SELECT COUNT(Cotizacion.numero) AS NumCot, "
   SQL = SQL & "Cliente.Nombre AS NombreCliente, "
   SQL = SQL & "Cotizacion.NombreOperador AS Usuario, "
   SQL = SQL & "Cotizacion.Fecha, "
   SQL = SQL & "Cotizacion.Numero, "
   SQL = SQL & SQLCodigoArticulo
   SQL = SQL & "renglonCotizacion.PorcentajeDescuento AS PorcentajeDesc, "
   SQL = SQL & "renglonCotizacion.Cantidad as Cantidad, "
   SQL = SQL & "renglonCotizacion.ConsecutivoRenglon, "

   If valEfecturaCambioABs Then
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "renglonCotizacion.PrecioConIVA", False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "renglonCotizacion.PrecioConIVA", "Cotizacion.Fecha"), True) & ") AS PrecioConIva, "
                     
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "renglonCotizacion.PrecioSinIVA", False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "renglonCotizacion.PrecioSinIVA", "Cotizacion.Fecha"), True) & ") AS PrecioSinIva, "
                     
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & ") AS Monto, "
                     
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLDesc, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLDesc, "Cotizacion.Fecha"), True) & ") AS Descuento, "
                     
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
          gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), _
              gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva1, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva1, "Cotizacion.Fecha")), _
         gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                        "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva2, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva2, "Cotizacion.Fecha")), _
         gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                 "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva3, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva3, "Cotizacion.Fecha"))))) _
           & " AS Iva, "
                     
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal, False, ""), _
                gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal, "Cotizacion.Fecha")), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal1, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal1, "Cotizacion.Fecha")), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal2, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal2, "Cotizacion.Fecha")), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal3, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal3, "Cotizacion.Fecha")))))
      SQL = SQL & " AS Total, "
                     
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                     "Cotizacion.Moneda", True) & " As MonedaReporte, "
   Else
      SQL = SQL & "renglonCotizacion.PrecioConIVA, "
      SQL = SQL & "renglonCotizacion.PrecioSinIVA, "
      SQL = SQL & SQLMontoBruto & " AS Monto, "
      SQL = SQL & SQLDesc & " AS Descuento,"
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), SQLIva1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), SQLIva2, SQLIva3)))
      SQL = SQL & " AS iva, "
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), sqlTotal, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), sqlTotal1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), sqlTotal2, sqlTotal3)))
      SQL = SQL & " AS Total, "
      SQL = SQL & "Cotizacion.Moneda as MonedaReporte, "
   End If
   SQL = SQL & " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio,"
   SQL = SQL & " Cotizacion.PorcentajeDescuento,"
   SQL = SQL & " Cotizacion.Moneda AS NombreMoneda,"
   SQL = SQL & " renglonCotizacion.Serial AS Serial,"
   SQL = SQL & " renglonCotizacion.Rollo AS Rollo"
   SQL = SQL & " FROM Cliente INNER JOIN"
   SQL = SQL & " (Cotizacion INNER JOIN renglonCotizacion ON"
   SQL = SQL & " (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion)"
   SQL = SQL & " AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania))"
   SQL = SQL & " ON (Cliente.Codigo = Cotizacion.CodigoCliente)"
   SQL = SQL & " AND (Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN articuloInventario ON (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   SQL = SQL & " WHERE"
   SQL = SQL & " Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If ValCmbCantidadAImprimir = 0 Then
      SQL = SQL & " AND Cotizacion.NombreOperador = '" & Trim(ValtxtNombreOperador) & "' "
   End If
   SQL = SQL & " GROUP BY"
   SQL = SQL & " Cliente.Nombre,"
   SQL = SQL & " Cotizacion.Moneda,"
   SQL = SQL & " Cotizacion.NombreOperador,"
   SQL = SQL & " Cotizacion.Fecha,"
   SQL = SQL & " Cotizacion.Numero,"
   SQL = SQL & " renglonCotizacion.CodigoArticulo,"
   SQL = SQL & " renglonCotizacion.PorcentajeDescuento,"
   SQL = SQL & " renglonCotizacion.Cantidad,"
   SQL = SQL & " renglonCotizacion.ConsecutivoRenglon,"
   SQL = SQL & " renglonCotizacion.PrecioConIVA,"
   SQL = SQL & " renglonCotizacion.PrecioSinIVA,"
   SQL = SQL & " Cotizacion.PorcentajeDescuento,"
   SQL = SQL & " renglonCotizacion.Serial,"
   SQL = SQL & " renglonCotizacion.Rollo,"
   SQL = SQL & " articuloInventario.AlicuotaIVA,"
   SQL = SQL & " Cotizacion.CambioABolivares"
   SQL = SQL & " ORDER BY"
   SQL = SQL & " Cotizacion.NombreOperador,"
   SQL = SQL & " Cotizacion.Moneda desc, "
   SQL = SQL & " Cotizacion.Fecha, "
   SQL = SQL & " Cotizacion.Numero"
   fConstruirSqlDelReporteCotizacionesPorUsuario = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDelReporteCotizacionesPorUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotizacionesPorCliente(ByVal valEfectuarCambioaBs As Boolean, _
                                                               ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                   ByVal valFechaFinal As Date, _
                                                                     ByVal valtxtCodigoDeCliente As String, _
                                                                       ByVal gUltimaTasaDeCambio As Object, _
                                                                         ByVal gMonedaLocalActual As Object) As String
   Dim conjuntoDeNumeroDeCotizacions As String
   Dim SQL As String
   Dim SQLCodigo As String
   Dim SQLCodigoArticulo As String
   
   Dim SQLMontoBruto As String
   Dim SQLDesc As String
   Dim SQLIva1, SQLIva2, SQLIva3 As String
   Dim sqlTotal, sqlTotal1, sqlTotal2, sqlTotal3 As String
   Dim sqlPorcIva1, sqlPorcIva2, sqlPorcIva3 As String
   Dim SQLPorcIva As String
   Dim sqlMontoTotal As String
   Dim sqlDescRenglon As String
   Dim sqlSelectIva1, sqlSelectIva2, sqlSelectIva3 As String
   On Error GoTo h_ERROR
   sqlSelectIva1 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuotaGeneral FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   sqlSelectIva2 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuota2 FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   sqlSelectIva3 = "(SELECT top 1 alicuotaIVA.MontoAlicuotaGeneral AS MontoAlicuota3 FROM alicuotaIVA WHERE FechaDeInicioDeVigencia <= cotizacion.fecha ORDER BY FechaDeInicioDeVigencia DESC)"
   
   SQLMontoBruto = "(renglonCotizacion.Cantidad * renglonCotizacion.PrecioSinIVA)"
   SQLDesc = "(" & SQLMontoBruto & " - ((" & SQLMontoBruto & " * (1 - renglonCotizacion.PorcentajeDescuento / 100)) * (1 - cotizacion.PorcentajeDescuento / 100)))"
   sqlMontoTotal = "(" & SQLMontoBruto & " - " & SQLDesc & ")"
   
   sqlPorcIva1 = "(SELECT "
   sqlPorcIva1 = sqlPorcIva1 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuotaGeneral) > 0", sqlSelectIva1, 0)
   sqlPorcIva1 = sqlPorcIva1 & " FROM " & sqlSelectIva1 & " AS alicuotaIVA)"
   
   sqlPorcIva2 = "(SELECT "
   sqlPorcIva2 = sqlPorcIva2 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuota2) > 0", sqlSelectIva2, 0)
   sqlPorcIva2 = sqlPorcIva2 & " FROM " & sqlSelectIva2 & " AS alicuotaIVA)"

   sqlPorcIva3 = "(SELECT "
   sqlPorcIva3 = sqlPorcIva3 & gUtilSQL.getIIF("Count(alicuotaIVA.MontoAlicuota3) > 0", sqlSelectIva3, 0)
   sqlPorcIva3 = sqlPorcIva3 & " FROM " & sqlSelectIva3 & " AS alicuotaIVA)"

   SQLIva1 = "((" & sqlMontoTotal & " * " & sqlPorcIva1 & ")/100)"
   SQLIva2 = "((" & sqlMontoTotal & " * " & sqlPorcIva2 & ")/100)"
   SQLIva3 = "((" & sqlMontoTotal & " * " & sqlPorcIva3 & ")/100)"
   
   sqlTotal = "(" & SQLMontoBruto & " - " & SQLDesc & ")"
   sqlTotal1 = "(" & SQLMontoBruto & " - " & SQLDesc & " + " & SQLIva1 & ")"
   sqlTotal2 = "(" & SQLMontoBruto & " - " & SQLDesc & " + " & SQLIva2 & ")"
   sqlTotal3 = "(" & SQLMontoBruto & " - " & SQLDesc & " + " & SQLIva3 & ")"
   
   SQL = "SELECT Cotizacion.Numero"
   SQL = SQL & " FROM Cotizacion"
   SQL = SQL & " WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Cotizacion.CodigoCliente = '" & Trim(valtxtCodigoDeCliente) & "'"
   conjuntoDeNumeroDeCotizacions = gDbUtil.fBuildResultSetAsString(SQL)
   SQLCodigo = gUtilSQL.getIIF("renglonCotizacion.Serial <> " & gUtilSQL.fSimpleSqlValue(0), "renglonCotizacion.CodigoArticulo" & gUtilSQL.CharConcat() & " ' Serial ' " & gUtilSQL.CharConcat() & "renglonCotizacion.Serial" & gUtilSQL.CharConcat() & " ' Rollo ' " & gUtilSQL.CharConcat() & " renglonCotizacion.Rollo", "renglonCotizacion.CodigoArticulo ", True)
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonCotizacion.Serial <> " & gUtilSQL.fSimpleSqlValue(0) & " AND renglonCotizacion.rollo = " & gUtilSQL.fSimpleSqlValue(0), "renglonCotizacion.CodigoArticulo " & gUtilSQL.CharConcat() & " ' Serial ' " & gUtilSQL.CharConcat() & "renglonCotizacion.Serial", SQLCodigo, True) & " AS CodigoArticulo, "

   SQL = "SELECT "
   SQL = SQL & "Cotizacion.Fecha, "
   SQL = SQL & "Cotizacion.Numero, "
   SQL = SQL & "Cliente.codigo AS CodigoCliente, "
   SQL = SQL & "Cliente.nombre AS NombreCliente, "
   SQL = SQL & SQLCodigoArticulo
   SQL = SQL & "articuloInventario.Descripcion, "
   SQL = SQL & "renglonCotizacion.PorcentajeDescuento AS PorcDescuento, "
   SQL = SQL & "renglonCotizacion.Cantidad, "
   If valEfectuarCambioaBs Then
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & ") AS Monto, "
                     
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLDesc, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLDesc, "Cotizacion.Fecha"), True) & ") AS Descuento, "
                     
      SQL = SQL & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlMontoTotal, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoTotal, "Cotizacion.Fecha"), True) & ") AS BaseImponible, "
                     
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
          gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), _
              gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva1, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva1, "Cotizacion.Fecha")), _
         gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                        "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva2, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva2, "Cotizacion.Fecha")), _
         gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                 "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLIva3, False, ""), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLIva3, "Cotizacion.Fecha"))))) _
           & " AS Iva, "
                     
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal, False, ""), _
                gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal, "Cotizacion.Fecha")), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal1, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal1, "Cotizacion.Fecha")), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal2, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal2, "Cotizacion.Fecha")), _
               gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
               "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlTotal3, False, ""), _
               gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlTotal3, "Cotizacion.Fecha")))))
      SQL = SQL & " AS Total, "
                     
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                     "Cotizacion.Moneda", True) & " As MonedaReporte, "
   Else
      SQL = SQL & SQLMontoBruto & " AS Monto, "
      SQL = SQL & SQLDesc & " AS Descuento,"
      SQL = SQL & sqlMontoTotal & " AS BaseImponible,"
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), SQLIva1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), SQLIva2, SQLIva3)))
      SQL = SQL & " AS iva, "
      SQL = SQL & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), sqlTotal, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), sqlTotal1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), sqlTotal2, sqlTotal3)))
      SQL = SQL & " AS Total, "
      SQL = SQL & "Cotizacion.Moneda as MonedaReporte, "
   End If
   SQL = SQL & " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio,"
   SQL = SQL & " Cotizacion.PorcentajeDescuento AS PorcentajeDescuentoGeneral "
   SQL = SQL & " FROM Cliente INNER JOIN Cotizacion "
   SQL = SQL & " ON (Cliente.codigo = Cotizacion.CodigoCliente)"
   SQL = SQL & " AND (Cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN renglonCotizacion"
   SQL = SQL & " ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion)"
   SQL = SQL & " AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN articuloInventario ON (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   SQL = SQL & " AND (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   SQL = SQL & " WHERE Cotizacion.Numero IN (" & conjuntoDeNumeroDeCotizacions & ")"
   SQL = SQL & " AND Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY "
   SQL = SQL & "Cotizacion.Fecha, "
   SQL = SQL & "Cotizacion.Moneda, "
   SQL = SQL & "Cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionesPorCliente = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteCotizacionesPorCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteCotizacionesNoFacturadas(ByVal valConsecutivoCompania As String, _
                                                        ByVal valFechaInicial As Date, _
                                                         ByVal valFechaFinal As Date, _
                                                          ByVal valchkSoloUltimaCotizacion As Boolean, _
                                                            ByVal valEsSistemaAdmInfotax As Boolean, _
                                                             ByVal valImprimirUno As Boolean, _
                                                              ByVal valtxtNumero As String, _
                                                               ByVal ReporteCotizacionesNoFacturadas As Boolean, _
                                                                ByVal valCantidadImprimirUnaCiudad As Boolean, _
                                                                 ByVal valtxtCiudad As String, _
                                                                  ByVal valtxtZonaPostal As String, _
                                                                   ByVal valAgrupadoPorCiudad As Boolean) As String
   Dim sqlCot As String
   Dim sqlFac As String
   On Error GoTo h_ERROR
   sqlFac = "SELECT DISTINCT "
   sqlFac = sqlFac & "Factura.NoCotizacionDeOrigen"
   sqlFac = sqlFac & " FROM Factura"
   sqlFac = sqlFac & " WHERE "
   sqlFac = sqlFac & gUtilSQL.DfSQLDateValueBetween("Factura.Fecha", valFechaInicial, gUtilDate.getFechaDeHoy)
   sqlFac = sqlFac & " AND factura.StatusFactura = " & eSF_EMITIDA
   sqlFac = sqlFac & " AND Factura.NoCotizacionDeOrigen <> " & gUtilSQL.fSimpleSqlValue("")
   sqlFac = sqlFac & " AND Factura.ConsecutivoCompania = " & valConsecutivoCompania
  
   If Not valEsSistemaAdmInfotax Then
      sqlFac = gDbUtil.fBuildResultSetAsString(sqlFac)
   End If
   If valchkSoloUltimaCotizacion Then
      sqlCot = "SELECT "
      sqlCot = sqlCot & gUtilSQL.fFirst("Cliente.Nombre", "NombreCliente") & ", "
      sqlCot = sqlCot & gUtilSQL.fFirst("Cotizacion.Fecha", "FechaDeCotizacion") & ", "
      sqlCot = sqlCot & gUtilSQL.fMax("Cotizacion.Numero", "Numero") & ", "
      sqlCot = sqlCot & "Cliente.Codigo, "
      sqlCot = sqlCot & gUtilSQL.fFirst("Cliente.Contacto", "Contacto") & ", "
      sqlCot = sqlCot & gUtilSQL.fFirst("Cliente.Telefono", "Telefono") & ", "
      sqlCot = sqlCot & gUtilSQL.fFirst("Cliente.FAX", "FAX") & ", "
      sqlCot = sqlCot & gUtilSQL.fFirst("Cliente.Ciudad", "Ciudad")
      If valEsSistemaAdmInfotax Then
         sqlCot = sqlCot & ", " & gUtilSQL.fFirst("Cotizacion.Promocion", "Promocion")
      End If
      sqlCot = sqlCot & " FROM Cliente INNER JOIN Cotizacion ON "
      sqlCot = sqlCot & "(Cliente.Codigo = Cotizacion.CodigoCliente)"
      sqlCot = sqlCot & " AND (Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
      
      If Not ReporteCotizacionesNoFacturadas Then
         sqlCot = sqlCot & " INNER JOIN FACTURA ON "
         sqlCot = sqlCot & "(Cotizacion.CodigoCliente = Factura.CodigoCliente)"
         sqlCot = sqlCot & " AND (Cotizacion.Numero = Factura.NoCotizacionDeOrigen)"
         sqlCot = sqlCot & " AND (Factura.StatusFactura = " & eSF_EMITIDA & ")"
         sqlCot = sqlCot & " AND (Cotizacion.ConsecutivoCompania = Factura.ConsecutivoCompania)"
      End If
      
      sqlCot = sqlCot & " WHERE "
      sqlCot = sqlCot & "(" & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal) & ")"
      
      If ReporteCotizacionesNoFacturadas Then
         sqlCot = sqlCot & " AND (Cotizacion.Numero"
         sqlCot = sqlCot & " NOT IN "
         sqlCot = sqlCot & "(" & sqlFac & "))"
      Else
         sqlCot = sqlCot & " AND factura.StatusFactura = " & eSF_EMITIDA
      End If
      If valEsSistemaAdmInfotax Then
         If valImprimirUno Then
            sqlCot = sqlCot & " AND (Cotizacion.Numero = " & gUtilSQL.fSimpleSqlValue(Trim(valtxtNumero)) & ")"
         End If
      End If
      If valCantidadImprimirUnaCiudad Then
         If (gTexto.DfLen(valtxtCiudad) > 0) Then
            sqlCot = sqlCot & " AND (Cliente.Ciudad = " & gUtilSQL.fSimpleSqlValue(valtxtCiudad) & ")"
         End If
      End If
      If Trim(valtxtZonaPostal) <> "" Then
            sqlCot = sqlCot & " AND (" & gUtilSQL.fSQLValue("Cliente.ZonaPostal", valtxtZonaPostal, True) & ")"
      End If
      sqlCot = sqlCot & " AND (Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania & ")"
      sqlCot = sqlCot & " GROUP BY Cliente.Codigo "
      sqlCot = sqlCot & " ORDER BY "
      If valEsSistemaAdmInfotax Then
         sqlCot = sqlCot & gUtilSQL.fFirst("Cliente.Ciudad", "NONE") & ", "
         sqlCot = sqlCot & gUtilSQL.fFirst("Cliente.ZonaPostal", "NONE") & ", "
      End If
      sqlCot = sqlCot & gUtilSQL.fMax("Cotizacion.Fecha", "ORDER BY")
   Else
      sqlCot = "SELECT "
      sqlCot = sqlCot & "Cliente.Nombre AS NombreCliente, "
      sqlCot = sqlCot & "Cotizacion.Fecha AS FechaDeCotizacion, "
      sqlCot = sqlCot & "Cotizacion.Numero, "
      sqlCot = sqlCot & "Cotizacion.CodigoCliente, "
      sqlCot = sqlCot & "Cliente.Contacto, "
      sqlCot = sqlCot & "Cliente.Telefono, "
      sqlCot = sqlCot & "Cliente.FAX, "
      sqlCot = sqlCot & "Cliente.Ciudad"
      If valEsSistemaAdmInfotax Then
         sqlCot = sqlCot & ", Cotizacion.Promocion"
      End If
      sqlCot = sqlCot & " FROM Cliente INNER JOIN Cotizacion ON "
      sqlCot = sqlCot & "(Cliente.Codigo = Cotizacion.CodigoCliente)"
      sqlCot = sqlCot & " AND (Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
      
      If Not ReporteCotizacionesNoFacturadas Then
         sqlCot = sqlCot & " INNER JOIN FACTURA ON "
         sqlCot = sqlCot & "(Cotizacion.CodigoCliente = Factura.CodigoCliente)"
         sqlCot = sqlCot & " AND (Cotizacion.Numero = Factura.NoCotizacionDeOrigen)"
         sqlCot = sqlCot & " AND (Cotizacion.ConsecutivoCompania = Factura.ConsecutivoCompania)"
      End If
      
      sqlCot = sqlCot & " WHERE "
      sqlCot = sqlCot & "(" & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal) & ")"
      If ReporteCotizacionesNoFacturadas Then
         sqlCot = sqlCot & " AND (Cotizacion.Numero"
         sqlCot = sqlCot & " NOT IN "
         sqlCot = sqlCot & "(" & sqlFac & "))"
      Else
         sqlCot = sqlCot & " AND factura.StatusFactura = " & eSF_EMITIDA
      End If
      
      If valImprimirUno Then
         sqlCot = sqlCot & " AND (Cotizacion.Numero = " & gUtilSQL.fSimpleSqlValue(Trim(valtxtNumero)) & ")"
      End If
      If valCantidadImprimirUnaCiudad Then
         If (gTexto.DfLen(valtxtCiudad) > 0) Then
            sqlCot = sqlCot & " AND (Cliente.Ciudad = " & gUtilSQL.fSimpleSqlValue(valtxtCiudad) & ")"
         End If
      End If
      If Trim(valtxtZonaPostal) <> "" Then
            sqlCot = sqlCot & " AND (" & gUtilSQL.fSQLValue("Cliente.ZonaPostal", valtxtZonaPostal, True) & ")"
      End If
      sqlCot = sqlCot & " AND (Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania & ")"
      sqlCot = sqlCot & " ORDER BY "
      If valEsSistemaAdmInfotax Then
         sqlCot = sqlCot & "Cliente.Ciudad, "
         sqlCot = sqlCot & "Cliente.ZonaPostal, "
      Else
         If valAgrupadoPorCiudad Then
            sqlCot = sqlCot & "Cliente.Ciudad, "
         End If
      End If
      sqlCot = sqlCot & "Cotizacion.Fecha, Cotizacion.Numero "
   End If
   fSQLDelReporteCotizacionesNoFacturadas = sqlCot
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelReporteCotizacionesNoFacturadas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesPorArticuloResumido(ByVal valEfecturaCambioABs As Boolean, _
                                                                        ByVal valConsecutivoCompania As String, _
                                                                         ByVal valFechaInicial As Date, _
                                                                          ByVal valFechaFinal As Date, _
                                                                           ByVal gUltimaTasaDeCambio As Object, _
                                                                            ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "SUM(renglonCotizacion.Cantidad) AS Cant, "
    If valEfecturaCambioABs Then
      SQL = SQL & "SUM(renglonCotizacion.TotalRenglon * ISNULL(Cotizacion.CambioABolivares, 1)) AS TotalReng,"
   
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.Moneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)) & " AS MonedaReporte,"
   Else
      SQL = SQL & "SUM(renglonCotizacion.TotalRenglon) AS TotalReng, "
      SQL = SQL & "Cotizacion.Moneda AS MonedaReporte,"
   End If
   SQL = SQL & "renglonCotizacion.CodigoArticulo AS CodigoArt, "
   SQL = SQL & "renglonCotizacion.Descripcion AS DescripcionArticulo, "
   SQL = SQL & "Cotizacion.Moneda AS NombreMoneda, "
   SQL = SQL & " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio"
   SQL = SQL & " FROM renglonCotizacion INNER JOIN Cotizacion"
   SQL = SQL & " ON (renglonCotizacion.ConsecutivoCompania  = Cotizacion.ConsecutivoCompania)"
   SQL = SQL & " AND (renglonCotizacion.NumeroCotizacion  = Cotizacion.Numero)"
   SQL = SQL & " WHERE "
   SQL = SQL & " Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " GROUP BY"
   SQL = SQL & " renglonCotizacion.CodigoArticulo,"
   SQL = SQL & " Cotizacion.Moneda,"
   SQL = SQL & " renglonCotizacion.Descripcion,"
   SQL = SQL & " Cotizacion.CambioABolivares"
   SQL = SQL & " ORDER BY"
   If valEfecturaCambioABs Then
      SQL = SQL & " renglonCotizacion.CodigoArticulo,"
      SQL = SQL & " Cotizacion.Moneda"
   Else
      SQL = SQL & " Cotizacion.Moneda,"
      SQL = SQL & " renglonCotizacion.CodigoArticulo"
   End If
   fConstruirSqlDelReporteCotizacionesPorArticuloResumido = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDelReporteCotizacionesPorArticuloResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesPorArticulo(ByVal efecturaCambioABs As Boolean, _
                                                                ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                  ByVal valFechaFinal As Date, _
                                                                   ByVal valtxtCodigoArticulo As String, _
                                                                    ByVal gUltimaTasaDeCambio As Object, _
                                                                     ByVal gMonedaLocalActual As Object, _
                                                                      ByVal valCantidadAImprimirListIndex As Integer) As String
   Dim SQL As String
   Dim sqlCambio As String
   On Error GoTo h_ERROR
   SQL = " SELECT Cliente.Nombre AS NombreCliente, "
   SQL = SQL & "Cotizacion.Numero AS NumeroCotizacion, "
   SQL = SQL & "Cotizacion.Fecha AS FechaCotizacion, "
   SQL = SQL & "RenglonCotizacion.Cantidad AS Cantidad, "
   If efecturaCambioABs Then
      SQL = SQL & " (RenglonCotizacion.TotalRenglon * ISNULL(cotizacion.CambioABolivares, 1)) AS TotalRenglon,"
      SQL = SQL & " (Cotizacion.TotalCotizacion * ISNULL(cotizacion.CambioABolivares, 1)) AS MontoCotizado,"
      SQL = SQL & " (RenglonCotizacion.PrecioSinIVA * ISNULL(cotizacion.CambioABolivares, 1)) AS PrecioSinIVA,"
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                                  "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS MonedaReporte, "
   Else
      SQL = SQL & "RenglonCotizacion.TotalRenglon AS TotalRenglon, "
      SQL = SQL & "Cotizacion.TotalCotizacion AS MontoCotizado, "
      SQL = SQL & "RenglonCotizacion.PrecioSinIVA AS PrecioSinIVA, "
      SQL = SQL & "Cotizacion.Moneda AS MonedaReporte, "
   End If
   SQL = SQL & "ArticuloInventario.Codigo AS CodigoArticulo, "
   SQL = SQL & "ArticuloInventario.Descripcion AS DescripcionArticulo, "

   SQL = SQL & "Cotizacion.Moneda AS NombreMoneda, "

   sqlCambio = " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio"
   SQL = SQL & sqlCambio & " FROM Cliente INNER JOIN "
   SQL = SQL & "(Cotizacion INNER JOIN (ArticuloInventario"
   SQL = SQL & " INNER JOIN RenglonCotizacion ON "
   SQL = SQL & "(ArticuloInventario.Codigo = RenglonCotizacion.CodigoArticulo) AND "
   SQL = SQL & "(ArticuloInventario.ConsecutivoCompania  = RenglonCotizacion.ConsecutivoCompania)) ON "
   SQL = SQL & "(Cotizacion.Numero  = RenglonCotizacion.NumeroCotizacion) AND "
   SQL = SQL & "(Cotizacion.ConsecutivoCompania = RenglonCotizacion.ConsecutivoCompania)) ON "
   SQL = SQL & "(Cliente.Codigo = Cotizacion.CodigoCliente) AND "
   SQL = SQL & "(Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   SQL = SQL & "WHERE "
    If valCantidadAImprimirListIndex = 0 Then
      SQL = SQL & "  RenglonCotizacion.CodigoArticulo  = '" & Trim(valtxtCodigoArticulo) & "' "
      SQL = SQL & " AND "
   End If
   SQL = SQL & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT cliente.Nombre AS NombreCliente, cotizacion.Numero AS NumeroCotizacion, cotizacion.Fecha AS FechaCotizacion, " & _
                        "renglonCotizacion.Cantidad AS Cantidad, renglonCotizacion.TotalRenglon AS TotalRenglon, " & _
                        "cotizacion.TotalCotizacion AS montoCotizado, RenglonCotizacion.PrecioSinIVA AS PrecioSinIVA, " & _
                        "Cotizacion.Moneda AS MonedaReporte, renglonCotizacion.CodigoArticulo AS CodigoArticulo, " & _
                        "ArticuloInventario.Descripcion AS DescripcionArticulo, Cotizacion.Moneda AS NombreMoneda,  " & sqlCambio
   SQL = SQL & " FROM ((cliente INNER JOIN cotizacion ON (cliente.Codigo = cotizacion.CodigoCliente) AND " & _
                        "(cliente.ConsecutivoCompania = cotizacion.ConsecutivoCompania)) INNER JOIN renglonCotizacion " & _
                        "ON (cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) " & _
                        "INNER JOIN (articuloInventario INNER JOIN ExistenciaPorGrupo ON  (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) " & _
                        "AND (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)) ON " & _
                        "(renglonCotizacion.CodigoArticulo = ExistenciaPorGrupo.CodigoArticulo + existenciaPorgrupo.CodigoColor + existenciaPorgrupo.codigoTalla) AND " & _
                        "(renglonCotizacion.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   SQL = SQL & " WHERE "
   If valCantidadAImprimirListIndex = 0 Then
      SQL = SQL & " RenglonCotizacion.CodigoArticulo  = '" & Trim(valtxtCodigoArticulo) & "' "
      SQL = SQL & " AND "
   End If

   SQL = SQL & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " ORDER BY CodigoArticulo, MonedaReporte, NumeroCotizacion, FechaCotizacion"
h_EXIT: On Error GoTo 0
   fConstruirSqlDelReporteCotizacionesPorArticulo = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSqlDelReporteCotizacionesPorArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotizacionesPorVendedor(ByVal valEfecturaCambioABs As Boolean, _
                                                                ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                  ByVal valFechaFinal As Date, _
                                                                   ByVal valtxtCodigoVendedor As String, _
                                                                    ByVal valCantidadAImprimirListIndex As Integer, _
                                                                     ByVal gUltimaTasaDeCambio As Object, _
                                                                      ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT cotizacion.Fecha AS fecha, cotizacion.Numero AS Numero, cotizacion.Moneda, "
   If valEfecturaCambioABs Then
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "cotizacion.TotalRenglones", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "cotizacion.TotalRenglones", "Cotizacion.Fecha"), True) & " AS TotalRenglones, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "cotizacion.TotalIVA", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "cotizacion.TotalIVA", "Cotizacion.Fecha"), True) & " AS TotalIva, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "cotizacion.TotalCotizacion", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "cotizacion.TotalCotizacion", "Cotizacion.Fecha"), True) & " AS TotalCotizacion, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                                  "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS MonedaReporte, "
   Else
      SQL = SQL & " cotizacion.TotalRenglones AS TotalRenglones, cotizacion.TotalIVA AS TotalIva, cotizacion.TotalCotizacion AS TotalCotizacion, Cotizacion.Moneda AS MonedaReporte, "
   End If
   SQL = SQL & " vendedor.Codigo AS CodigoVendedor, vendedor.Nombre AS NombreVendedor, cliente.Nombre AS NombreCliente"

   SQL = SQL & " FROM Cotizacion INNER JOIN Vendedor ON (vendedor.Codigo = cotizacion.CodigoVendedor AND vendedor.ConsecutivoCompania = cotizacion.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN Cliente ON (cotizacion.CodigoCliente = cliente.Codigo AND cliente.ConsecutivoCompania = cotizacion.ConsecutivoCompania)"
   SQL = SQL & " WHERE cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimirListIndex = 0 Then
      SQL = SQL & " AND cotizacion.CodigoVendedor = '" & gTexto.fLimpiaStringdeBlancosAAmbosLados(valtxtCodigoVendedor) & "'"
   End If
   SQL = SQL & " order by vendedor.codigo, cotizacion.moneda, cotizacion.Fecha, cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionesPorVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteCotizacionesPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteArticulosPorCotizacion(ByVal valConsecutivoCompania As String, _
                                                               ByVal valFechaInicial As Date, _
                                                                ByVal valFechaFinal As Date, _
                                                                 ByVal valOrder As String, _
                                                                  ByVal valAgrupaXCiudad As Boolean, _
                                                                   ByVal valSqlWhereCiudad As String) As String
   Dim SQL As String
   Dim sqlCiudad As String
   On Error GoTo h_ERROR
   sqlCiudad = gUtilSQL.getIIF("cotizacion.UsarDireccionFiscal = " & gUtilSQL.fSimpleSqlValue(gConvert.ConvertBooleanToString(False)), _
                                "DireccionDeDespacho.Ciudad", "Cliente.Ciudad", True)
   SQL = "SELECT "
   If valAgrupaXCiudad Then
      SQL = SQL & sqlCiudad & " AS Ciudad, "
   End If
   SQL = SQL & "cotizacion.CodigoCliente, Cliente.Nombre, cotizacion.Numero, "
   SQL = SQL & "renglonCotizacion.CodigoArticulo, renglonCotizacion.Descripcion, renglonCotizacion.Cantidad  "
   SQL = SQL & "FROM cotizacion INNER JOIN "
   SQL = SQL & "Cliente ON cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania "
   SQL = SQL & " AND cotizacion.CodigoCliente = Cliente.Codigo "
   SQL = SQL & " INNER JOIN renglonCotizacion ON cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania"
   SQL = SQL & " AND cotizacion.Numero = renglonCotizacion.numeroCotizacion"
   If valAgrupaXCiudad Then
      SQL = SQL & " LEFT OUTER JOIN DireccionDeDespacho ON cotizacion.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
      SQL = SQL & " AND cotizacion.ConsecutivoCompania = DireccionDeDespacho.ConsecutivoCompania"
   End If
   SQL = SQL & " WHERE Cotizacion.consecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If valAgrupaXCiudad And valSqlWhereCiudad <> "" Then
      SQL = SQL & " AND " & sqlCiudad & " = " & gUtilSQL.fSimpleSqlValue(valSqlWhereCiudad)
   End If
   SQL = SQL & " ORDER BY "
   If valAgrupaXCiudad Then
      SQL = SQL & " Ciudad, "
   End If
   SQL = SQL & valOrder & ", cotizacion.Numero, renglonCotizacion.ConsecutivoRenglon"
   fConstruirSQLDelReporteArticulosPorCotizacion = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteArticulosPorCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotizacionPorCiudad(ByVal valConsecutivoCompania As String, _
                                                               ByVal valFechaInicial As Date, _
                                                                ByVal valFechaFinal As Date, _
                                                                  ByVal valEfecturaCambioABs As Boolean, _
                                                                   ByVal gUltimaTasaDeCambio As Object, _
                                                                      ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim vDescuento As String
   Dim vMontoBruto As String
   On Error GoTo h_ERROR
   vDescuento = " ((Cotizacion.TotalRenglones * Cotizacion.PorcentajeDescuento) / 100)"
   vMontoBruto = " (Cotizacion.TotalBaseImponible + " & vDescuento & ")"
   
   SQL = SQL & " SELECT"
   SQL = SQL & " Cotizacion.Fecha, "
   SQL = SQL & " Cotizacion.Numero, "
   SQL = SQL & " Cliente.Codigo, "
   SQL = SQL & " Cliente.Nombre, "
 
   If valEfecturaCambioABs Then
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                            "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS Moneda, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalBaseImponible", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalBaseImponible", "Cotizacion.Fecha"), True) & " AS TotalBaseImponible, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalIVA", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalIVA", "Cotizacion.Fecha"), True) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), True) & " AS TotalCotizacion, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vDescuento, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vDescuento, "Cotizacion.Fecha"), True) & " AS Descuento, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vMontoBruto, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vMontoBruto, "Cotizacion.Fecha"), True) & " AS MontoBruto, "
   Else
      SQL = SQL & " Cotizacion.TotalBaseImponible, "
      SQL = SQL & " Cotizacion.TotalIVA, "
      SQL = SQL & " Cotizacion.TotalCotizacion, "
      SQL = SQL & vDescuento & " AS Descuento, "
      SQL = SQL & vMontoBruto & " AS MontoBruto, "
      SQL = SQL & " Cotizacion.Moneda, "
   End If
   SQL = SQL & " Cotizacion.PorcentajeDescuento, "
   SQL = SQL & " Cotizacion.NoDirDespachoAimprimir, "
   SQL = SQL & " (CASE WHEN Cotizacion.UsarDireccionFiscal = 'N' THEN DireccionDeDespacho.Ciudad ELSE Cliente.Ciudad END) AS Ciudad, "
   SQL = SQL & " (CASE WHEN Cotizacion.UsarDireccionFiscal = 'N' THEN DireccionDeDespacho.Direccion ELSE Cliente.Direccion END) AS Direccion "
   SQL = SQL & " FROM Cotizacion"
   SQL = SQL & " INNER JOIN Cliente"
   SQL = SQL & " ON Cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND Cotizacion.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " LEFT JOIN DireccionDeDespacho"
   SQL = SQL & " ON Cotizacion.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
   SQL = SQL & " AND Cotizacion.ConsecutivoCompania = DireccionDeDespacho.ConsecutivoCompania"
   SQL = SQL & " AND Cotizacion.CodigoCliente = DireccionDeDespacho.CodigoCliente"
   SQL = SQL & " WHERE Cotizacion.consecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " ORDER BY "
   SQL = SQL & " Ciudad , Cotizacion.Fecha, Cotizacion.CodigoCliente, Cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionPorCiudad = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteCotizacionPorCiudad", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotizacionPorCiudadYVendedor(ByVal valConsecutivoCompania As String, _
                                                               ByVal valFechaInicial As Date, _
                                                                ByVal valFechaFinal As Date, _
                                                                 ByVal valEfecturaCambioABs As Boolean, _
                                                                  ByVal valCantidadAImprimirListIndex As Integer, _
                                                                   ByVal valCantidadAImprimirCiudadListIndex As Integer, _
                                                                    ByVal valcodigoVendedor As String, _
                                                                     ByVal valCodigoCiudad As String, _
                                                                      ByVal gUltimaTasaDeCambio As Object, _
                                                                       ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim vDescuento As String
   Dim vMontoBruto As String
   On Error GoTo h_ERROR
   vDescuento = " ((Cotizacion.TotalRenglones * Cotizacion.PorcentajeDescuento) / 100)"
   vMontoBruto = " (Cotizacion.TotalBaseImponible + " & vDescuento & ")"
   SQL = SQL & " SELECT"
   SQL = SQL & " Cotizacion.Fecha,"
   SQL = SQL & " Cotizacion.Numero,"
   SQL = SQL & " Cliente.Codigo,"
   SQL = SQL & " Cliente.Nombre, "
 
   If valEfecturaCambioABs Then
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                            "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS Moneda, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalBaseImponible", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalBaseImponible", "Cotizacion.Fecha"), True) & " AS TotalBaseImponible, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalIVA", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalIVA", "Cotizacion.Fecha"), True) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), True) & " AS TotalCotizacion, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vDescuento, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vDescuento, "Cotizacion.Fecha"), True) & " AS Descuento, "
      SQL = SQL & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vMontoBruto, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vMontoBruto, "Cotizacion.Fecha"), True) & " AS MontoBruto, "
   Else
      SQL = SQL & gUtilSQL.fRoundNDecimales("Cotizacion.TotalBaseImponible", 2) & " AS TotalBaseImponible, "
      SQL = SQL & gUtilSQL.fRoundNDecimales("Cotizacion.TotalIVA", 2) & " AS TotalIVA, "
      SQL = SQL & gUtilSQL.fRoundNDecimales("Cotizacion.TotalCotizacion", 2) & " AS TotalCotizacion, "
      SQL = SQL & gUtilSQL.fRoundNDecimales(vDescuento, 2) & " AS Descuento, "
      SQL = SQL & gUtilSQL.fRoundNDecimales(vMontoBruto, 2) & " AS MontoBruto,"
      SQL = SQL & " Cotizacion.Moneda, "
   End If
   SQL = SQL & " Cotizacion.PorcentajeDescuento, "
   SQL = SQL & " Vendedor.Codigo, "
   SQL = SQL & " Vendedor.Nombre AS NombreVendedor, "
   SQL = SQL & " (CASE WHEN Cotizacion.UsarDireccionFiscal = 'N' THEN DireccionDeDespacho.Ciudad ELSE Cliente.Ciudad END) AS Ciudad "
   SQL = SQL & " FROM Cotizacion"
   SQL = SQL & " INNER JOIN Cliente"
   SQL = SQL & " ON Cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND Cotizacion.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " INNER JOIN VENDEDOR ON Vendedor.Codigo = Cotizacion.CodigoVendedor"
   SQL = SQL & " AND Cotizacion.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   SQL = SQL & " LEFT JOIN DireccionDeDespacho"
   SQL = SQL & " ON Cotizacion.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
   SQL = SQL & " AND Cotizacion.ConsecutivoCompania = DireccionDeDespacho.ConsecutivoCompania"
   SQL = SQL & " AND Cotizacion.CodigoCliente = DireccionDeDespacho.CodigoCliente"
   SQL = SQL & " WHERE Cotizacion.consecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimirListIndex = 0 Then
      SQL = SQL & " AND Cotizacion.codigoVendedor = '" & valcodigoVendedor & "'"
   End If
    If valCantidadAImprimirCiudadListIndex = 0 Then
      SQL = SQL & " AND Cliente.Ciudad = '" & valCodigoCiudad & "'"
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & " NombreVendedor, Cliente.Ciudad, Cotizacion.Fecha, Cotizacion.CodigoCliente, Cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionPorCiudadYVendedor = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteCotizacionPorCiudadYVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
