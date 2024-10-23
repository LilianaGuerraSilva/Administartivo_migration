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
Private Const GetGender = Enum_Gender.eg_Male

Public Function fConstruirSQLDelReporteCotizacionesPorVendedorYLinea(ByVal valFechaInicial As Date, _
                                                                      ByVal valFechaFinal As Date, _
                                                                       ByVal valCantidadAImprimir As String, _
                                                                        ByVal valcodigoVendedor As String, _
                                                                         ByVal valConsecutivoCompaniaActual As Long, _
                                                                          ByVal valTipoGrupo As Boolean, _
                                                                           ByVal ReporteEnMonedaLocal As Boolean, _
                                                                            ByVal gMonedaLocalActual As Object, _
                                                                             ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
  
   Sql = "SELECT vendedor.Codigo, vendedor.Nombre, "
   Sql = Sql & " articuloInventario.LineaDeProducto, "
   Sql = Sql & " cotizacion.Fecha, "
   Sql = Sql & " cotizacion.Numero, "
   Sql = Sql & " cliente.Nombre AS NombreCliente, "
   Sql = Sql & " articuloInventario.Descripcion, "
   
   If ReporteEnMonedaLocal Then
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & ") AS MontoBruto, "
                     
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlDescuento, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlDescuento, "Cotizacion.Fecha"), True) & ") AS Descuento, "
         
      Sql = Sql & gUtilSQL.getIIF("cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlBaseImponible, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlBaseImponible, "Cotizacion.Fecha"), True) & " AS BaseImponible, "
   
         
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
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
           
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), _
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
      Sql = Sql & " AS MontoTotal, "
      
      Sql = Sql & gUtilSQL.getIIF("cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & " AS MontoBruto, "
                                         
      Sql = Sql & gUtilSQL.getIIF("cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                     "Cotizacion.Moneda", True) & " As Moneda "
   Else
      Sql = Sql & SQLMontoBruto & " AS Monto, "
      Sql = Sql & sqlDescuento & " AS Descuento,"
      Sql = Sql & sqlBaseImponible & " as BaseImponible, "
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), SQLIva1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), SQLIva2, SQLIva3)))
      Sql = Sql & " AS MontoIVA, "
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), sqlTotal, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), sqlTotal1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), sqlTotal2, sqlTotal3)))
      Sql = Sql & " AS MontoTotal, "
      Sql = Sql & SQLMontoBruto & " as MontoBruto, "
      Sql = Sql & "Cotizacion.Moneda AS Moneda "
   End If
  
   Sql = Sql & " FROM renglonCotizacion INNER JOIN"
   Sql = Sql & " cotizacion ON renglonCotizacion.ConsecutivoCompania = cotizacion.ConsecutivoCompania AND"
   Sql = Sql & " renglonCotizacion.NumeroCotizacion = cotizacion.Numero "
   Sql = Sql & " INNER JOIN articuloInventario ON renglonCotizacion.CodigoArticulo = articuloInventario.Codigo AND"
   Sql = Sql & " renglonCotizacion.ConsecutivoCompania = articuloInventario.ConsecutivoCompania "
   Sql = Sql & " INNER JOIN vendedor ON cotizacion.ConsecutivoCompania = vendedor.ConsecutivoCompania "
   Sql = Sql & " AND cotizacion.CodigoVendedor = vendedor.Codigo "
   Sql = Sql & " INNER JOIN cliente ON cotizacion.ConsecutivoCompania = cliente.ConsecutivoCompania "
   Sql = Sql & " AND cotizacion.CodigoCliente = cliente.Codigo"
   Sql = Sql & " GROUP BY vendedor.Codigo, vendedor.Nombre, cotizacion.Fecha, cotizacion.Numero, cliente.Nombre, articuloInventario.LineaDeProducto, Cotizacion.Moneda, "
   Sql = Sql & " articuloInventario.Descripcion, cotizacion.ConsecutivoCompania, renglonCotizacion.PrecioSinIVA, Cotizacion.PorcentajeDescuento,"
   Sql = Sql & " renglonCotizacion.PrecioConIVA, renglonCotizacion.Cantidad, renglonCotizacion.PorcentajeDescuento, articuloInventario.Descripcion,"
   Sql = Sql & " articuloInventario.AlicuotaIVA"
   Sql = Sql & " HAVING  (" & gUtilSQL.DfSQLDateValueBetween("cotizacion.Fecha", valFechaInicial, valFechaFinal) & ") "
   If valCantidadAImprimir = enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND vendedor.Codigo = '" & gTexto.fLimpiaStringdeBlancosAAmbosLados(valcodigoVendedor) & "'"
   End If
   Sql = Sql & " AND (cotizacion.ConsecutivoCompania = " & valConsecutivoCompaniaActual & ")"
   Sql = Sql & " ORDER BY "
   If valTipoGrupo Then
      Sql = Sql & "moneda, vendedor.Nombre, articuloInventario.LineaDeProducto, cotizacion.Fecha, cotizacion.Numero "
   Else
      Sql = Sql & "moneda, articuloInventario.LineaDeProducto, vendedor.Nombre, cotizacion.Fecha, cotizacion.Numero  "
   End If
   fConstruirSQLDelReporteCotizacionesPorVendedorYLinea = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCotizacionesPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesEntreFechas(ByVal ReporteEnMonedaLocal As Boolean, _
                                                                ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                  ByVal valFechaFinal As Date, _
                                                                   ByVal gUltimaTasaDeCambio As Object, _
                                                                    ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "Cotizacion.Fecha, "
   Sql = Sql & "Cotizacion.Numero, "
   Sql = Sql & "Cotizacion.CodigoCliente, "
   Sql = Sql & "Cliente.Nombre, "
   Sql = Sql & "Cotizacion.Contacto, "
   If ReporteEnMonedaLocal Then
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                        gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), "Cotizacion.Moneda", True) & " AS Moneda, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalRenglones", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalRenglones", False, ""), True) & " AS MontoBruto, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalBaseImponible", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalBaseImponible", False, ""), True) & " AS TotalBaseImponible, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalIVA", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalIVA", False, ""), True) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                        "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                        gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), _
                        gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), True) & " AS TotalFactura, "
   Else
      Sql = Sql & "Cotizacion.Moneda AS Moneda, "
      Sql = Sql & "Cotizacion.TotalRenglones AS MontoBruto, "
      Sql = Sql & "Cotizacion.TotalBaseImponible AS TotalBaseImponible, "
      Sql = Sql & "Cotizacion.TotalIVA AS TotalIVA, "
      Sql = Sql & "Cotizacion.TotalCotizacion AS TotalFactura, "
   End If
   Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", 1, False, "CambioABolivares")

   Sql = Sql & " FROM Cliente INNER JOIN Cotizacion"
   Sql = Sql & " ON (Cliente.Codigo = Cotizacion.CodigoCliente)"
   Sql = Sql & " AND (Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   Sql = Sql & " WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " ORDER BY"
   Sql = Sql & " Moneda, "
   Sql = Sql & " Cotizacion.Fecha, "
   Sql = Sql & " Cotizacion.Numero"
   fConstruirSqlDelReporteCotizacionesEntreFechas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSqlDelReporteCotizacionesEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesPorDia(ByVal valEfecturaCambioABs As Boolean, _
                                                           ByVal valConsecutivoCompania As String, _
                                                            ByVal valFechaInicial As Date, _
                                                             ByVal valFechaFinal As Date, _
                                                              ByVal gUltimaTasaDeCambio As Object, _
                                                               ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT Cotizacion.Fecha, "
   If valEfecturaCambioABs Then
      Sql = Sql & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " AS Moneda, "
      
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                              "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                              gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalRenglones", "Cotizacion.Fecha"), _
                              gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalRenglones", False, ""), True) & ") AS MontoBruto, "

      
      Sql = Sql & "MAX(" & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                              "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                              gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), _
                              gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), True) & ") AS CotizacionMayor, "
      
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                              "Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                              gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), _
                              gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), True) & ") AS MontoTotal, "
   Else
      Sql = Sql & "Cotizacion.moneda AS Moneda, "
      Sql = Sql & "SUM(Cotizacion.TotalRenglones) AS MontoBruto, "
      Sql = Sql & "MAX(Cotizacion.TotalCotizacion) AS CotizacionMayor, "
      Sql = Sql & "SUM(Cotizacion.TotalCotizacion) AS MontoTotal, "
   End If
   Sql = Sql & "COUNT(Cotizacion.CodigoCliente) AS NumeroDeCotizaciones "
   Sql = Sql & " FROM Cotizacion"
   Sql = Sql & " WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If valEfecturaCambioABs Then
      Sql = Sql & " GROUP BY Cotizacion.Fecha"
      Sql = Sql & " ORDER BY Cotizacion.Fecha"
   Else
      Sql = Sql & " GROUP BY Moneda, Cotizacion.Fecha"
      Sql = Sql & " ORDER BY Moneda, Cotizacion.Fecha"
   End If
   fConstruirSqlDelReporteCotizacionesPorDia = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSqlDelReporteCotizacionesPorDia", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
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
   
   Sql = "SELECT COUNT(Cotizacion.numero) AS NumCot, "
   Sql = Sql & "Cliente.Nombre AS NombreCliente, "
   Sql = Sql & "Cotizacion.NombreOperador AS Usuario, "
   Sql = Sql & "Cotizacion.Fecha, "
   Sql = Sql & "Cotizacion.Numero, "
   Sql = Sql & SQLCodigoArticulo
   Sql = Sql & "renglonCotizacion.PorcentajeDescuento AS PorcentajeDesc, "
   Sql = Sql & "renglonCotizacion.Cantidad as Cantidad, "
   Sql = Sql & "renglonCotizacion.ConsecutivoRenglon, "

   If valEfecturaCambioABs Then
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "renglonCotizacion.PrecioConIVA", False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "renglonCotizacion.PrecioConIVA", "Cotizacion.Fecha"), True) & ") AS PrecioConIva, "
                     
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "renglonCotizacion.PrecioSinIVA", False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "renglonCotizacion.PrecioSinIVA", "Cotizacion.Fecha"), True) & ") AS PrecioSinIva, "
                     
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & ") AS Monto, "
                     
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLDesc, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLDesc, "Cotizacion.Fecha"), True) & ") AS Descuento, "
                     
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
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
                     
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), _
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
      Sql = Sql & " AS Total, "
                     
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                     "Cotizacion.Moneda", True) & " As MonedaReporte, "
   Else
      Sql = Sql & "renglonCotizacion.PrecioConIVA, "
      Sql = Sql & "renglonCotizacion.PrecioSinIVA, "
      Sql = Sql & SQLMontoBruto & " AS Monto, "
      Sql = Sql & SQLDesc & " AS Descuento,"
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), SQLIva1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), SQLIva2, SQLIva3)))
      Sql = Sql & " AS iva, "
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), sqlTotal, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), sqlTotal1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), sqlTotal2, sqlTotal3)))
      Sql = Sql & " AS Total, "
      Sql = Sql & "Cotizacion.Moneda as MonedaReporte, "
   End If
   Sql = Sql & " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio,"
   Sql = Sql & " Cotizacion.PorcentajeDescuento,"
   Sql = Sql & " Cotizacion.Moneda AS NombreMoneda,"
   Sql = Sql & " renglonCotizacion.Serial AS Serial,"
   Sql = Sql & " renglonCotizacion.Rollo AS Rollo"
   Sql = Sql & " FROM Cliente INNER JOIN"
   Sql = Sql & " (Cotizacion INNER JOIN renglonCotizacion ON"
   Sql = Sql & " (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion)"
   Sql = Sql & " AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania))"
   Sql = Sql & " ON (Cliente.Codigo = Cotizacion.CodigoCliente)"
   Sql = Sql & " AND (Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN articuloInventario ON (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   Sql = Sql & " WHERE"
   Sql = Sql & " Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If ValCmbCantidadAImprimir = 0 Then
      Sql = Sql & " AND Cotizacion.NombreOperador = '" & Trim(ValtxtNombreOperador) & "' "
   End If
   Sql = Sql & " GROUP BY"
   Sql = Sql & " Cliente.Nombre,"
   Sql = Sql & " Cotizacion.Moneda,"
   Sql = Sql & " Cotizacion.NombreOperador,"
   Sql = Sql & " Cotizacion.Fecha,"
   Sql = Sql & " Cotizacion.Numero,"
   Sql = Sql & " renglonCotizacion.CodigoArticulo,"
   Sql = Sql & " renglonCotizacion.PorcentajeDescuento,"
   Sql = Sql & " renglonCotizacion.Cantidad,"
   Sql = Sql & " renglonCotizacion.ConsecutivoRenglon,"
   Sql = Sql & " renglonCotizacion.PrecioConIVA,"
   Sql = Sql & " renglonCotizacion.PrecioSinIVA,"
   Sql = Sql & " Cotizacion.PorcentajeDescuento,"
   Sql = Sql & " renglonCotizacion.Serial,"
   Sql = Sql & " renglonCotizacion.Rollo,"
   Sql = Sql & " articuloInventario.AlicuotaIVA,"
   Sql = Sql & " Cotizacion.CambioABolivares"
   Sql = Sql & " ORDER BY"
   Sql = Sql & " Cotizacion.NombreOperador,"
   Sql = Sql & " Cotizacion.Moneda desc, "
   Sql = Sql & " Cotizacion.Fecha, "
   Sql = Sql & " Cotizacion.Numero"
   fConstruirSqlDelReporteCotizacionesPorUsuario = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSqlDelReporteCotizacionesPorUsuario", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotizacionesPorCliente(ByVal valEfectuarCambioaBs As Boolean, _
                                                               ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                   ByVal valFechaFinal As Date, _
                                                                     ByVal valtxtCodigoDeCliente As String, _
                                                                       ByVal gUltimaTasaDeCambio As Object, _
                                                                         ByVal gMonedaLocalActual As Object) As String
   Dim conjuntoDeNumeroDeCotizacions As String
   Dim Sql As String
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
   
   Sql = "SELECT Cotizacion.Numero"
   Sql = Sql & " FROM Cotizacion"
   Sql = Sql & " WHERE Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Cotizacion.CodigoCliente = '" & Trim(valtxtCodigoDeCliente) & "'"
   conjuntoDeNumeroDeCotizacions = gDbUtil.fBuildResultSetAsString(Sql)
   SQLCodigo = gUtilSQL.getIIF("renglonCotizacion.Serial <> " & gUtilSQL.fSimpleSqlValue(0), "renglonCotizacion.CodigoArticulo" & gUtilSQL.CharConcat() & " ' Serial ' " & gUtilSQL.CharConcat() & "renglonCotizacion.Serial" & gUtilSQL.CharConcat() & " ' Rollo ' " & gUtilSQL.CharConcat() & " renglonCotizacion.Rollo", "renglonCotizacion.CodigoArticulo ", True)
   SQLCodigoArticulo = gUtilSQL.getIIF("renglonCotizacion.Serial <> " & gUtilSQL.fSimpleSqlValue(0) & " AND renglonCotizacion.rollo = " & gUtilSQL.fSimpleSqlValue(0), "renglonCotizacion.CodigoArticulo " & gUtilSQL.CharConcat() & " ' Serial ' " & gUtilSQL.CharConcat() & "renglonCotizacion.Serial", SQLCodigo, True) & " AS CodigoArticulo, "

   Sql = "SELECT "
   Sql = Sql & "Cotizacion.Fecha, "
   Sql = Sql & "Cotizacion.Numero, "
   Sql = Sql & "Cliente.codigo AS CodigoCliente, "
   Sql = Sql & "Cliente.nombre AS NombreCliente, "
   Sql = Sql & SQLCodigoArticulo
   Sql = Sql & "articuloInventario.Descripcion, "
   Sql = Sql & "renglonCotizacion.PorcentajeDescuento AS PorcDescuento, "
   Sql = Sql & "renglonCotizacion.Cantidad, "
   If valEfectuarCambioaBs Then
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLMontoBruto, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLMontoBruto, "Cotizacion.Fecha"), True) & ") AS Monto, "
                     
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", SQLDesc, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, SQLDesc, "Cotizacion.Fecha"), True) & ") AS Descuento, "
                     
      Sql = Sql & "(" & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda) & " OR " & _
                     "Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMonedaAnt), _
                     gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", sqlMontoTotal, False, ""), _
                     gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoTotal, "Cotizacion.Fecha"), True) & ") AS BaseImponible, "
                     
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
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
                     
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), _
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
      Sql = Sql & " AS Total, "
                     
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.Moneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), _
                     "Cotizacion.Moneda", True) & " As MonedaReporte, "
   Else
      Sql = Sql & SQLMontoBruto & " AS Monto, "
      Sql = Sql & SQLDesc & " AS Descuento,"
      Sql = Sql & sqlMontoTotal & " AS BaseImponible,"
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), gUtilSQL.fNumToStrSQL(0), _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), SQLIva1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), SQLIva2, SQLIva3)))
      Sql = Sql & " AS iva, "
      Sql = Sql & gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(0), sqlTotal, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(1), sqlTotal1, _
      gUtilSQL.getIIF("articuloInventario.AlicuotaIVA = " & gUtilSQL.fNumToStrSQL(2), sqlTotal2, sqlTotal3)))
      Sql = Sql & " AS Total, "
      Sql = Sql & "Cotizacion.Moneda as MonedaReporte, "
   End If
   Sql = Sql & " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio,"
   Sql = Sql & " Cotizacion.PorcentajeDescuento AS PorcentajeDescuentoGeneral "
   Sql = Sql & " FROM Cliente INNER JOIN Cotizacion "
   Sql = Sql & " ON (Cliente.codigo = Cotizacion.CodigoCliente)"
   Sql = Sql & " AND (Cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN renglonCotizacion"
   Sql = Sql & " ON (Cotizacion.Numero = renglonCotizacion.NumeroCotizacion)"
   Sql = Sql & " AND (Cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN articuloInventario ON (articuloInventario.Codigo = renglonCotizacion.CodigoArticulo)"
   Sql = Sql & " AND (articuloInventario.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   Sql = Sql & " WHERE Cotizacion.Numero IN (" & conjuntoDeNumeroDeCotizacions & ")"
   Sql = Sql & " AND Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " ORDER BY "
   Sql = Sql & "Cotizacion.Fecha, "
   Sql = Sql & "Cotizacion.Moneda, "
   Sql = Sql & "Cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionesPorCliente = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCotizacionesPorCliente", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteCotizacionesNoFacturadas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesPorArticuloResumido(ByVal valEfecturaCambioABs As Boolean, _
                                                                        ByVal valConsecutivoCompania As String, _
                                                                         ByVal valFechaInicial As Date, _
                                                                          ByVal valFechaFinal As Date, _
                                                                           ByVal gUltimaTasaDeCambio As Object, _
                                                                            ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "SUM(renglonCotizacion.Cantidad) AS Cant, "
    If valEfecturaCambioABs Then
      Sql = Sql & "SUM(renglonCotizacion.TotalRenglon * ISNULL(Cotizacion.CambioABolivares, 1)) AS TotalReng,"
   
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.Moneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)) & " AS MonedaReporte,"
   Else
      Sql = Sql & "SUM(renglonCotizacion.TotalRenglon) AS TotalReng, "
      Sql = Sql & "Cotizacion.Moneda AS MonedaReporte,"
   End If
   Sql = Sql & "renglonCotizacion.CodigoArticulo AS CodigoArt, "
   Sql = Sql & "renglonCotizacion.Descripcion AS DescripcionArticulo, "
   Sql = Sql & "Cotizacion.Moneda AS NombreMoneda, "
   Sql = Sql & " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio"
   Sql = Sql & " FROM renglonCotizacion INNER JOIN Cotizacion"
   Sql = Sql & " ON (renglonCotizacion.ConsecutivoCompania  = Cotizacion.ConsecutivoCompania)"
   Sql = Sql & " AND (renglonCotizacion.NumeroCotizacion  = Cotizacion.Numero)"
   Sql = Sql & " WHERE "
   Sql = Sql & " Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " GROUP BY"
   Sql = Sql & " renglonCotizacion.CodigoArticulo,"
   Sql = Sql & " Cotizacion.Moneda,"
   Sql = Sql & " renglonCotizacion.Descripcion,"
   Sql = Sql & " Cotizacion.CambioABolivares"
   Sql = Sql & " ORDER BY"
   If valEfecturaCambioABs Then
      Sql = Sql & " renglonCotizacion.CodigoArticulo,"
      Sql = Sql & " Cotizacion.Moneda"
   Else
      Sql = Sql & " Cotizacion.Moneda,"
      Sql = Sql & " renglonCotizacion.CodigoArticulo"
   End If
   fConstruirSqlDelReporteCotizacionesPorArticuloResumido = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSqlDelReporteCotizacionesPorArticuloResumido", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlDelReporteCotizacionesPorArticulo(ByVal efecturaCambioABs As Boolean, _
                                                                ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                  ByVal valFechaFinal As Date, _
                                                                   ByVal valtxtCodigoArticulo As String, _
                                                                    ByVal gUltimaTasaDeCambio As Object, _
                                                                     ByVal gMonedaLocalActual As Object, _
                                                                      ByVal valCantidadAImprimirListIndex As Integer) As String
   Dim Sql As String
   Dim sqlCambio As String
   On Error GoTo h_ERROR
   Sql = " SELECT Cliente.Nombre AS NombreCliente, "
   Sql = Sql & "Cotizacion.Numero AS NumeroCotizacion, "
   Sql = Sql & "Cotizacion.Fecha AS FechaCotizacion, "
   Sql = Sql & "RenglonCotizacion.Cantidad AS Cantidad, "
   If efecturaCambioABs Then
      Sql = Sql & " (RenglonCotizacion.TotalRenglon * ISNULL(cotizacion.CambioABolivares, 1)) AS TotalRenglon,"
      Sql = Sql & " (Cotizacion.TotalCotizacion * ISNULL(cotizacion.CambioABolivares, 1)) AS MontoCotizado,"
      Sql = Sql & " (RenglonCotizacion.PrecioSinIVA * ISNULL(cotizacion.CambioABolivares, 1)) AS PrecioSinIVA,"
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                                  "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS MonedaReporte, "
   Else
      Sql = Sql & "RenglonCotizacion.TotalRenglon AS TotalRenglon, "
      Sql = Sql & "Cotizacion.TotalCotizacion AS MontoCotizado, "
      Sql = Sql & "RenglonCotizacion.PrecioSinIVA AS PrecioSinIVA, "
      Sql = Sql & "Cotizacion.Moneda AS MonedaReporte, "
   End If
   Sql = Sql & "ArticuloInventario.Codigo AS CodigoArticulo, "
   Sql = Sql & "ArticuloInventario.Descripcion AS DescripcionArticulo, "

   Sql = Sql & "Cotizacion.Moneda AS NombreMoneda, "

   sqlCambio = " ISNULL(Cotizacion.CambioABolivares, 1) AS Cambio"
   Sql = Sql & sqlCambio & " FROM Cliente INNER JOIN "
   Sql = Sql & "(Cotizacion INNER JOIN (ArticuloInventario"
   Sql = Sql & " INNER JOIN RenglonCotizacion ON "
   Sql = Sql & "(ArticuloInventario.Codigo = RenglonCotizacion.CodigoArticulo) AND "
   Sql = Sql & "(ArticuloInventario.ConsecutivoCompania  = RenglonCotizacion.ConsecutivoCompania)) ON "
   Sql = Sql & "(Cotizacion.Numero  = RenglonCotizacion.NumeroCotizacion) AND "
   Sql = Sql & "(Cotizacion.ConsecutivoCompania = RenglonCotizacion.ConsecutivoCompania)) ON "
   Sql = Sql & "(Cliente.Codigo = Cotizacion.CodigoCliente) AND "
   Sql = Sql & "(Cliente.ConsecutivoCompania = Cotizacion.ConsecutivoCompania)"
   Sql = Sql & "WHERE "
    If valCantidadAImprimirListIndex = 0 Then
      Sql = Sql & "  RenglonCotizacion.CodigoArticulo  = '" & Trim(valtxtCodigoArticulo) & "' "
      Sql = Sql & " AND "
   End If
   Sql = Sql & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " UNION "
   Sql = Sql & " SELECT cliente.Nombre AS NombreCliente, cotizacion.Numero AS NumeroCotizacion, cotizacion.Fecha AS FechaCotizacion, " & _
                        "renglonCotizacion.Cantidad AS Cantidad, renglonCotizacion.TotalRenglon AS TotalRenglon, " & _
                        "cotizacion.TotalCotizacion AS montoCotizado, RenglonCotizacion.PrecioSinIVA AS PrecioSinIVA, " & _
                        "Cotizacion.Moneda AS MonedaReporte, renglonCotizacion.CodigoArticulo AS CodigoArticulo, " & _
                        "ArticuloInventario.Descripcion AS DescripcionArticulo, Cotizacion.Moneda AS NombreMoneda,  " & sqlCambio
   Sql = Sql & " FROM ((cliente INNER JOIN cotizacion ON (cliente.Codigo = cotizacion.CodigoCliente) AND " & _
                        "(cliente.ConsecutivoCompania = cotizacion.ConsecutivoCompania)) INNER JOIN renglonCotizacion " & _
                        "ON (cotizacion.Numero = renglonCotizacion.NumeroCotizacion) AND (cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania)) " & _
                        "INNER JOIN (articuloInventario INNER JOIN ExistenciaPorGrupo ON  (ExistenciaPorGrupo.ConsecutivoCompania = articuloInventario.ConsecutivoCompania) " & _
                        "AND (articuloInventario.Codigo = ExistenciaPorGrupo.CodigoArticulo)) ON " & _
                        "(renglonCotizacion.CodigoArticulo = ExistenciaPorGrupo.CodigoArticulo + existenciaPorgrupo.CodigoColor + existenciaPorgrupo.codigoTalla) AND " & _
                        "(renglonCotizacion.ConsecutivoCompania = ExistenciaPorGrupo.ConsecutivoCompania)"
   Sql = Sql & " WHERE "
   If valCantidadAImprimirListIndex = 0 Then
      Sql = Sql & " RenglonCotizacion.CodigoArticulo  = '" & Trim(valtxtCodigoArticulo) & "' "
      Sql = Sql & " AND "
   End If

   Sql = Sql & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND Cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " ORDER BY CodigoArticulo, MonedaReporte, NumeroCotizacion, FechaCotizacion"
h_EXIT: On Error GoTo 0
   fConstruirSqlDelReporteCotizacionesPorArticulo = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSqlDelReporteCotizacionesPorArticulo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotizacionesPorVendedor(ByVal valEfecturaCambioABs As Boolean, _
                                                                ByVal valConsecutivoCompania As String, _
                                                                 ByVal valFechaInicial As Date, _
                                                                  ByVal valFechaFinal As Date, _
                                                                   ByVal valtxtCodigoVendedor As String, _
                                                                    ByVal valCantidadAImprimirListIndex As Integer, _
                                                                     ByVal gUltimaTasaDeCambio As Object, _
                                                                      ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT cotizacion.Fecha AS fecha, cotizacion.Numero AS Numero, cotizacion.Moneda, "
   If valEfecturaCambioABs Then
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "cotizacion.TotalRenglones", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "cotizacion.TotalRenglones", "Cotizacion.Fecha"), True) & " AS TotalRenglones, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "cotizacion.TotalIVA", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "cotizacion.TotalIVA", "Cotizacion.Fecha"), True) & " AS TotalIva, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "cotizacion.TotalCotizacion", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "cotizacion.TotalCotizacion", "Cotizacion.Fecha"), True) & " AS TotalCotizacion, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                                  "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS MonedaReporte, "
   Else
      Sql = Sql & " cotizacion.TotalRenglones AS TotalRenglones, cotizacion.TotalIVA AS TotalIva, cotizacion.TotalCotizacion AS TotalCotizacion, Cotizacion.Moneda AS MonedaReporte, "
   End If
   Sql = Sql & " vendedor.Codigo AS CodigoVendedor, vendedor.Nombre AS NombreVendedor, cliente.Nombre AS NombreCliente"

   Sql = Sql & " FROM Cotizacion INNER JOIN Vendedor ON (vendedor.Codigo = cotizacion.CodigoVendedor AND vendedor.ConsecutivoCompania = cotizacion.ConsecutivoCompania)"
   Sql = Sql & " INNER JOIN Cliente ON (cotizacion.CodigoCliente = cliente.Codigo AND cliente.ConsecutivoCompania = cotizacion.ConsecutivoCompania)"
   Sql = Sql & " WHERE cotizacion.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimirListIndex = 0 Then
      Sql = Sql & " AND cotizacion.CodigoVendedor = '" & gTexto.fLimpiaStringdeBlancosAAmbosLados(valtxtCodigoVendedor) & "'"
   End If
   Sql = Sql & " order by vendedor.codigo, cotizacion.moneda, cotizacion.Fecha, cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionesPorVendedor = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCotizacionesPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteArticulosPorCotizacion(ByVal valConsecutivoCompania As String, _
                                                               ByVal valFechaInicial As Date, _
                                                                ByVal valFechaFinal As Date, _
                                                                 ByVal valOrder As String, _
                                                                  ByVal valAgrupaXCiudad As Boolean, _
                                                                   ByVal valSqlWhereCiudad As String) As String
   Dim Sql As String
   Dim sqlCiudad As String
   On Error GoTo h_ERROR
   sqlCiudad = gUtilSQL.getIIF("cotizacion.UsarDireccionFiscal = " & gUtilSQL.fSimpleSqlValue(gConvert.ConvertBooleanToString(False)), _
                                "DireccionDeDespacho.Ciudad", "Cliente.Ciudad", True)
   Sql = "SELECT "
   If valAgrupaXCiudad Then
      Sql = Sql & sqlCiudad & " AS Ciudad, "
   End If
   Sql = Sql & "cotizacion.CodigoCliente, Cliente.Nombre, cotizacion.Numero, "
   Sql = Sql & "renglonCotizacion.CodigoArticulo, renglonCotizacion.Descripcion, renglonCotizacion.Cantidad  "
   Sql = Sql & "FROM cotizacion INNER JOIN "
   Sql = Sql & "Cliente ON cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania "
   Sql = Sql & " AND cotizacion.CodigoCliente = Cliente.Codigo "
   Sql = Sql & " INNER JOIN renglonCotizacion ON cotizacion.ConsecutivoCompania = renglonCotizacion.ConsecutivoCompania"
   Sql = Sql & " AND cotizacion.Numero = renglonCotizacion.numeroCotizacion"
   If valAgrupaXCiudad Then
      Sql = Sql & " LEFT OUTER JOIN DireccionDeDespacho ON cotizacion.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
      Sql = Sql & " AND cotizacion.ConsecutivoCompania = DireccionDeDespacho.ConsecutivoCompania"
   End If
   Sql = Sql & " WHERE Cotizacion.consecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If valAgrupaXCiudad And valSqlWhereCiudad <> "" Then
      Sql = Sql & " AND " & sqlCiudad & " = " & gUtilSQL.fSimpleSqlValue(valSqlWhereCiudad)
   End If
   Sql = Sql & " ORDER BY "
   If valAgrupaXCiudad Then
      Sql = Sql & " Ciudad, "
   End If
   Sql = Sql & valOrder & ", cotizacion.Numero, renglonCotizacion.ConsecutivoRenglon"
   fConstruirSQLDelReporteArticulosPorCotizacion = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteArticulosPorCotizacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCotizacionPorCiudad(ByVal valConsecutivoCompania As String, _
                                                               ByVal valFechaInicial As Date, _
                                                                ByVal valFechaFinal As Date, _
                                                                  ByVal valEfecturaCambioABs As Boolean, _
                                                                   ByVal gUltimaTasaDeCambio As Object, _
                                                                      ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   Dim vDescuento As String
   Dim vMontoBruto As String
   On Error GoTo h_ERROR
   vDescuento = " ((Cotizacion.TotalRenglones * Cotizacion.PorcentajeDescuento) / 100)"
   vMontoBruto = " (Cotizacion.TotalBaseImponible + " & vDescuento & ")"
   
   Sql = Sql & " SELECT"
   Sql = Sql & " Cotizacion.Fecha, "
   Sql = Sql & " Cotizacion.Numero, "
   Sql = Sql & " Cliente.Codigo, "
   Sql = Sql & " Cliente.Nombre, "
 
   If valEfecturaCambioABs Then
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                            "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS Moneda, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalBaseImponible", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalBaseImponible", "Cotizacion.Fecha"), True) & " AS TotalBaseImponible, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalIVA", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalIVA", "Cotizacion.Fecha"), True) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), True) & " AS TotalCotizacion, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vDescuento, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vDescuento, "Cotizacion.Fecha"), True) & " AS Descuento, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vMontoBruto, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vMontoBruto, "Cotizacion.Fecha"), True) & " AS MontoBruto, "
   Else
      Sql = Sql & " Cotizacion.TotalBaseImponible, "
      Sql = Sql & " Cotizacion.TotalIVA, "
      Sql = Sql & " Cotizacion.TotalCotizacion, "
      Sql = Sql & vDescuento & " AS Descuento, "
      Sql = Sql & vMontoBruto & " AS MontoBruto, "
      Sql = Sql & " Cotizacion.Moneda, "
   End If
   Sql = Sql & " Cotizacion.PorcentajeDescuento, "
   Sql = Sql & " Cotizacion.NoDirDespachoAimprimir, "
   Sql = Sql & " (CASE WHEN Cotizacion.UsarDireccionFiscal = 'N' THEN DireccionDeDespacho.Ciudad ELSE Cliente.Ciudad END) AS Ciudad, "
   Sql = Sql & " (CASE WHEN Cotizacion.UsarDireccionFiscal = 'N' THEN DireccionDeDespacho.Direccion ELSE Cliente.Direccion END) AS Direccion "
   Sql = Sql & " FROM Cotizacion"
   Sql = Sql & " INNER JOIN Cliente"
   Sql = Sql & " ON Cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " AND Cotizacion.CodigoCliente = Cliente.Codigo"
   Sql = Sql & " LEFT JOIN DireccionDeDespacho"
   Sql = Sql & " ON Cotizacion.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
   Sql = Sql & " AND Cotizacion.ConsecutivoCompania = DireccionDeDespacho.ConsecutivoCompania"
   Sql = Sql & " AND Cotizacion.CodigoCliente = DireccionDeDespacho.CodigoCliente"
   Sql = Sql & " WHERE Cotizacion.consecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " ORDER BY "
   Sql = Sql & " Ciudad , Cotizacion.Fecha, Cotizacion.CodigoCliente, Cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionPorCiudad = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCotizacionPorCiudad", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
   Dim vDescuento As String
   Dim vMontoBruto As String
   On Error GoTo h_ERROR
   vDescuento = " ((Cotizacion.TotalRenglones * Cotizacion.PorcentajeDescuento) / 100)"
   vMontoBruto = " (Cotizacion.TotalBaseImponible + " & vDescuento & ")"
   Sql = Sql & " SELECT"
   Sql = Sql & " Cotizacion.Fecha,"
   Sql = Sql & " Cotizacion.Numero,"
   Sql = Sql & " Cliente.Codigo,"
   Sql = Sql & " Cliente.Nombre, "
 
   If valEfecturaCambioABs Then
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                            "Cotizacion.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS Moneda, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalBaseImponible", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalBaseImponible", "Cotizacion.Fecha"), True) & " AS TotalBaseImponible, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalIVA", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalIVA", "Cotizacion.Fecha"), True) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", "Cotizacion.TotalCotizacion", False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Cotizacion.TotalCotizacion", "Cotizacion.Fecha"), True) & " AS TotalCotizacion, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vDescuento, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vDescuento, "Cotizacion.Fecha"), True) & " AS Descuento, "
      Sql = Sql & gUtilSQL.getIIF("Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                                  "Cotizacion.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                                  gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "Cotizacion.Moneda", vMontoBruto, False, ""), _
                                  gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vMontoBruto, "Cotizacion.Fecha"), True) & " AS MontoBruto, "
   Else
      Sql = Sql & gUtilSQL.fRoundNDecimales("Cotizacion.TotalBaseImponible", 2) & " AS TotalBaseImponible, "
      Sql = Sql & gUtilSQL.fRoundNDecimales("Cotizacion.TotalIVA", 2) & " AS TotalIVA, "
      Sql = Sql & gUtilSQL.fRoundNDecimales("Cotizacion.TotalCotizacion", 2) & " AS TotalCotizacion, "
      Sql = Sql & gUtilSQL.fRoundNDecimales(vDescuento, 2) & " AS Descuento, "
      Sql = Sql & gUtilSQL.fRoundNDecimales(vMontoBruto, 2) & " AS MontoBruto,"
      Sql = Sql & " Cotizacion.Moneda, "
   End If
   Sql = Sql & " Cotizacion.PorcentajeDescuento, "
   Sql = Sql & " Vendedor.Codigo, "
   Sql = Sql & " Vendedor.Nombre AS NombreVendedor, "
   Sql = Sql & " (CASE WHEN Cotizacion.UsarDireccionFiscal = 'N' THEN DireccionDeDespacho.Ciudad ELSE Cliente.Ciudad END) AS Ciudad "
   Sql = Sql & " FROM Cotizacion"
   Sql = Sql & " INNER JOIN Cliente"
   Sql = Sql & " ON Cotizacion.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " AND Cotizacion.CodigoCliente = Cliente.Codigo"
   Sql = Sql & " INNER JOIN VENDEDOR ON Vendedor.Codigo = Cotizacion.CodigoVendedor"
   Sql = Sql & " AND Cotizacion.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   Sql = Sql & " LEFT JOIN DireccionDeDespacho"
   Sql = Sql & " ON Cotizacion.NoDirDespachoAimprimir = DireccionDeDespacho.ConsecutivoDireccion"
   Sql = Sql & " AND Cotizacion.ConsecutivoCompania = DireccionDeDespacho.ConsecutivoCompania"
   Sql = Sql & " AND Cotizacion.CodigoCliente = DireccionDeDespacho.CodigoCliente"
   Sql = Sql & " WHERE Cotizacion.consecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("Cotizacion.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimirListIndex = 0 Then
      Sql = Sql & " AND Cotizacion.codigoVendedor = '" & valcodigoVendedor & "'"
   End If
    If valCantidadAImprimirCiudadListIndex = 0 Then
      Sql = Sql & " AND Cliente.Ciudad = '" & valCodigoCiudad & "'"
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & " NombreVendedor, Cliente.Ciudad, Cotizacion.Fecha, Cotizacion.CodigoCliente, Cotizacion.Numero"
   fConstruirSQLDelReporteCotizacionPorCiudadYVendedor = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCotizacionPorCiudadYVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

