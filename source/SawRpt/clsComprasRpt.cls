VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsComprasRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private gUtilDate As clsUtilDate

Private Const CM_FILE_NAME As String = "clsComprasRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reportes De Compras"

Private Enum eComprasRpt

   eCOMPRA_HISTORICO_COMPRAS = 0
   eCOMPRA_MARGEN_SOBRE_COSTO_PROMEDIO_DE_COMPRAS
   eCOMPRA_COMPRAS_ENTRE_FECHAS
   'eCOMPRA_COMPRAS_ENTRE_FECHAS_TOTALES
   eCOMPRA_COSTO_DE_COMPRAS_ENTRE_FECHAS
   eCOMPRA__COMPRA
   eCOMPRA_ORDEN_DE_COMPRA
   eCOMPRA_ORDEN_DE_COMPRA_ENTRE_FECHAS
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub Class_Initialize()
   Set gUtilDate = New clsUtilDate
End Sub

Private Sub Class_Terminate()
   Set gUtilDate = Nothing
End Sub

Private Function fNombreFileDeImpresionDelReporteDeCompra(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eCOMPRA_HISTORICO_COMPRAS: nombreFileXML = "rpxHistoricoCompras"
      Case eCOMPRA_MARGEN_SOBRE_COSTO_PROMEDIO_DE_COMPRAS: nombreFileXML = "rpxMargenSobreCostoPromDeComp"
      Case eCOMPRA_COMPRAS_ENTRE_FECHAS: nombreFileXML = "rpxComprasEntreFechas"
      'Case eCOMPRA_COMPRAS_ENTRE_FECHAS_TOTALES: nombreFileXML = "rpxComprasEntreFechasTotales"
      Case eCOMPRA_COSTO_DE_COMPRAS_ENTRE_FECHAS:  nombreFileXML = "rpxCostoDeCompraEntreFechas"
      Case eCOMPRA_ORDEN_DE_COMPRA_ENTRE_FECHAS: nombreFileXML = "rpxOrdenDeCompraEntreFechas"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeCompra = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeCompras(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeCompra = ""
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeCompra", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeCompras(ByVal valReporte As Integer) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eCOMPRA_HISTORICO_COMPRAS: fNombreDelReporteDeCompras = "Histórico de Compras"
      Case eCOMPRA_MARGEN_SOBRE_COSTO_PROMEDIO_DE_COMPRAS: fNombreDelReporteDeCompras = "Margen Sobre Costo Promedio De Compra"
      Case eCOMPRA_COMPRAS_ENTRE_FECHAS: fNombreDelReporteDeCompras = "Compras entre Fechas"
      'Case eCOMPRA_COMPRAS_ENTRE_FECHAS_TOTALES: fNombreDelReporteDeCompras = "Compras entre Fechas Totales"
      Case eCOMPRA_COSTO_DE_COMPRAS_ENTRE_FECHAS: fNombreDelReporteDeCompras = "Costo de Compras entre Fechas"
      Case eCOMPRA__COMPRA: fNombreDelReporteDeCompras = "compras"
      Case eCOMPRA_ORDEN_DE_COMPRA: fNombreDelReporteDeCompras = "Orden de Compras"
      Case eCOMPRA_ORDEN_DE_COMPRA_ENTRE_FECHAS: fNombreDelReporteDeCompras = "Orden de Compras Entre Fechas"
      Case Else: fNombreDelReporteDeCompras = ""
   End Select
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelReporteDeCompras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function sConfigurarDatosYSeccionesDelReporteHistoricoCompras( _
                        ByRef refRpt As DDActiveReports2.ActiveReport, _
                              ByVal valSQL As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim exito As Boolean
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   NombreDelReporte = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_HISTORICO_COMPRAS)
   If gUtilReports.fLoadLayout(refRpt, NombreDelReporte) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
        
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
       gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "costoAlcambio", False, ddTXRight, formatoNDecimales

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_cantidadrecibida_SecLineaProducto", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_CostoUnitario_SecLineaProducto", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_cambio_SecLineaProducto", "", "cambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_costoAlcambio_SecLineaProducto", "", "costoAlcambio", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecLineaProducto", "LineaDeProducto", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHStatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, NombreDelReporte
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT:
   sConfigurarDatosYSeccionesDelReporteHistoricoCompras = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfigurarDatosYSeccionesDelReporteHistoricoCompras", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosYSeccionesDelReporteMargenSobreCostoPromedioDeCompra(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, ByVal byvalNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_MARGEN_SOBRE_COSTO_PROMEDIO_DE_COMPRAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If Not gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         GoTo h_EXIT
      End If
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", byvalNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "Codigo"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionDelArticulo", "", "Descripcion"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPromedioPonderadoCompEntreFeha", "", "PromDeComp", False, ddTXRight, formatoNDecimales
         
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioDeVenta", "", "PV", False, ddTXRight, formatoNDecimales
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMargen", "", "Margen"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPorcentajeDelMargen", "", "PorcentajeMargen"
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPrecioDeVenta", "Precio de Venta "
      gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaHoraDeEmision", _
                                        "lblComprasEntreFechas", "lblNumeroDePagina", _
                                         "lblFechaInicialYFinal", False, True
      gUtilMargins.sAsignarMargenesGenerales refRpt
      refRpt.Sections("GHLineaDeProducto").DataField = "LineaDeProducto"
      refRpt.Sections("GHLineaDeProducto").GrpKeepTogether = ddGrpFirstDetail
      refRpt.Sections("GHLineaDeProducto").KeepTogether = True
      refRpt.Sections("GHLineaDeProducto").Repeat = ddRepeatOnPage
       Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
      Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
        exito = True
   End If
    fConfigurarDatosYSeccionesDelReporteMargenSobreCostoPromedioDeCompra = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "sConfigurarDatosYSeccionesDelReporteMargenSobreCostoPromedioDeCompra", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function sConfigurarDatosYSeccionesDelReporteComprasEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean) As Boolean

                                                       

   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra Entre Fecha"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecEstatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
          gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalCompra", Not valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", Not valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
         If False Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         End If
            
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteComprasEntreFechas = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteComprasEntreFechas = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function sConfigurarDatosYSeccionesDelReporteComprasEntreFechasTotales(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean, ByVal valPorMonedasOriginal As Boolean) As Boolean

                                                       

   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
If valPorMonedasOriginal = True Or valTotales = True Then
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra Entre Fecha con Totales"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecEstatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         If valPorMonedasOriginal = True Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "TotalCompra", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
            gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 450, 5490, 225
            gUtilReports.sChangePositionAndSize refRpt, "txtTotalCompra", 4252, 0, 5490, 225
         Else
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "CompraTotalAlcambio", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
         End If
          gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle

          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcambio", Not valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblcambioMaster", Not valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCompraAlCambio", Not valPorMonedasOriginal
          
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalCompra", Not valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", Not valMuestraDetalle
          'gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
         If False Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         End If
            
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteComprasEntreFechasTotales = exito
End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteComprasEntreFechasTotales = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function sConfigurarDatosYSeccionesDelReporteComprasEntreFechasMonedaOriginal(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean, ByVal valPorMonedasOriginal As Boolean) As Boolean

                                                       

   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra Entre Fecha Moneda Original"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecEstatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         If valTotales = True Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "TotalCompra", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
         Else
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
         End If
         
         If valMuestraDetalle = False Then
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
         Else
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            If valTotales = False Then
             gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "TotalCompra", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", Not valMuestraDetalle
         End If
         
          gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 450, 5490, 225
          gUtilReports.sChangePositionAndSize refRpt, "txtTotalCompra", 4252, 0, 5490, 225

          gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle

          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcambio", valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblcambioMaster", valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCompraAlCambio", valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
         
         If False Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         End If
            
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteComprasEntreFechasMonedaOriginal = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteComprasEntreFechasMonedaOriginal = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivar(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean, ByVal valPorMonedasOriginal As Boolean) As Boolean

                                                       

   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra Entre Fecha en Bolívar"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecEstatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "CompraTotalAlcambio", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
         
         gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 450, 5490, 225
         gUtilReports.sChangePositionAndSize refRpt, "txtCompraTotalAlcambio", 4252, 0, 5490, 225

          gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle

          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcambio", valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblcambioMaster", valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valPorMonedasOriginal
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCompraAlCambio", valPorMonedasOriginal
          
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalCompra", Not valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
         
         If False Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         End If
            
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivar = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivar = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function
'**************************************************************
Public Function sConfigurarDatosYSeccionesDelReporteComprasEntreFechasSoloMonedaOriginal(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean, ByVal valPorMonedasOriginal As Boolean) As Boolean
Dim exito As Boolean
Dim nombreDelFileXML As String
Dim formatoNDecimales As String
   
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra Entre Fechas Cambio Original"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecEstatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         If valTotales = True Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "TotalCompra", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
         Else
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
           gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
         End If
         
         If valMuestraDetalle = False Then
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcambio", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblcambioMaster", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCompraAlCambio", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", valMuestraDetalle
            
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcostoAlCambio", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCostoUnitario", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcantidadrecibida", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoDelArticulo", valMuestraDetalle
            
            gUtilReports.sAsignaAlturaDeSeccion refRpt, "GHSecMoneda", 450
            gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblNombreProveedor", 2250, 225, 3240, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblFecha", 1125, 225, 2115, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblNumero", 2115, 225, 0, 225
            
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", True
         Else
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            If valTotales = False Then
               gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "TotalCompra", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", Not valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcambio", Not valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblcambioMaster", Not valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCompraAlCambio", Not valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valMuestraDetalle
            
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcostoAlCambio", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCostoUnitario", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcantidadrecibida", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoDelArticulo", valMuestraDetalle
            
            gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 0

            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", True
          End If
         
         gUtilReports.sChangePositionAndSize refRpt, "txtTotalCompra", 4252, 0, 5490, 225

         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
         
         If False Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         End If
            
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteComprasEntreFechasSoloMonedaOriginal = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteComprasEntreFechasSoloMonedaOriginal = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaOriginal(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean, ByVal valPorMonedasOriginal As Boolean) As Boolean
Dim exito As Boolean
Dim nombreDelFileXML As String
Dim formatoNDecimales As String
   
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra Entre Fechas en Bolivares con Cambio Original"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalCompra", "Compra Bolivares"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecEstatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         If valTotales = True Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "CompraTotalAlcambio", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valPorMonedasOriginal
         End If
         
         If valMuestraDetalle = False Then
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", valPorMonedasOriginal
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", False
            
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcostoAlCambio", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCostoUnitario", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcantidadrecibida", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoDelArticulo", valMuestraDetalle
            
            gUtilReports.sAsignaAlturaDeSeccion refRpt, "GHSecMoneda", 450
            gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblNombreProveedor", 2250, 225, 3240, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblFecha", 1125, 225, 2115, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblNumero", 2115, 225, 0, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblcambioMaster", 885, 225, 7200, 225
            
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", True
         Else
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL
            If valTotales = False Then
               gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "TotalCompra", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", Not valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valPorMonedasOriginal
            
            gUtilReports.sAsignaAlturaDeSeccion refRpt, "GHSecMoneda", 675
            gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 0
            gUtilReports.sChangePositionAndSize refRpt, "lblcambioMaster", 885, 225, 7200, 0
            
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", valMuestraDetalle
         End If
         
         gUtilReports.sChangePositionAndSize refRpt, "txtCompraTotalAlcambio", 4252, 0, 5490, 225
         
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle

         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcambio", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblcambioMaster", True
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCompraAlCambio", Not valPorMonedasOriginal
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
         
         If False Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         End If
            
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaOriginal = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaOriginal = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaDelDia(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean, ByVal valTotales As Boolean, ByVal valPorMonedasOriginal As Boolean) As Boolean
Dim exito As Boolean
Dim nombreDelFileXML As String
Dim formatoNDecimales As String
   
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra Entre Fechas en Bolivares con Ultimo Cambio"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalCompra", "Compra Bolivares"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcostoAlcambio", "", "CompraTotalAlcambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusCompra", "", "StatusCompra"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecEstatusCompra", "StatusCompra", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         
         If valTotales = True Then
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "CompraTotalAlcambio", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
         Else
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalMoneda", valTotales
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valPorMonedasOriginal
         End If
         
         If valMuestraDetalle = False Then
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", Not valPorMonedasOriginal
         
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcostoAlCambio", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCostoUnitario", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcantidadrecibida", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCodigoDelArticulo", valMuestraDetalle
            
            gUtilReports.sAsignaAlturaDeSeccion refRpt, "GHSecMoneda", 450
            gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblNombreProveedor", 2250, 225, 3240, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblFecha", 1125, 225, 2115, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblNumero", 2115, 225, 0, 225
            gUtilReports.sChangePositionAndSize refRpt, "lblcambioMaster", 885, 225, 7200, 225
                        
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", True
         Else
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_costoAlcambio_SecCompra", "CompraTotalAlcambio", eSF_SUM, "GHSecCompra", eSR_GROUP, eST_SUB_TOTAL  '
            If valTotales = False Then
               gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMoneda", "TotalCompra", eSF_SUM, "GHSecMoneda", eSR_ALL, eST_SUB_TOTAL
            End If
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalCompra", valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalCompra", Not valMuestraDetalle
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCompraTotalAlcambio", valPorMonedasOriginal
            
            gUtilReports.sAsignaAlturaDeSeccion refRpt, "GHSecMoneda", 675
            gUtilReports.sChangePositionAndSize refRpt, "lblTotalCompra", 4252, 225, 5490, 0
            gUtilReports.sChangePositionAndSize refRpt, "lblcambioMaster", 885, 225, 7200, 0
            
            gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", valMuestraDetalle
         End If
         
         gUtilReports.sChangePositionAndSize refRpt, "txtCompraTotalAlcambio", 4252, 0, 5490, 225
         
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle

         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtcambio", Not valPorMonedasOriginal
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblcambioMaster", Not valPorMonedasOriginal
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCompraAlCambio", valPorMonedasOriginal
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblRollo", valMuestraDetalle
         gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
         
         If False Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         End If
            
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         Call refRpt.AddNamedItem("gConvert", gConvert)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaDelDia = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteComprasEntreFechasBolivaresMonedaDelDia = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprasEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fConfigurarDatosDelReporteCostoDeCompraEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
    Dim exito As Boolean
    Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_COSTO_DE_COMPRAS_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If Not gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         GoTo h_EXIT
      End If
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
      gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionDelArticulo", "", "Descripcion"
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtLineaDeProducto", "", "LineaDeProducto"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadTotalDelArticulo", "", "CantArtComp", False, ddTXRight, formatoNDecimales
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtVecesComp", "", "VecesComprados"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPromedioPonderadoCompEntreFeha", "", "PromPonDeComp", False, ddTXRight, formatoNDecimales
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMaximoCostoDeCompra", "", "CostoMaximo"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMinimoCostoDeCompra", "", "CostoMinimo"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "TotalCompXArt"
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalPorGrupo", ""
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDelReporte", ""
      gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDeLaUltimaCompra", "", "FechaUltCompra", eFT_DATE
      gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaHoraDeEmision", _
                                        "lblComprasEntreFechas", "lblNumeroDePagina", _
                                        "lblFechaInicialYFinal", False, True
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHLineaDeProducto", "LineaDeProducto", ddGrpAll, ddRepeatAll, True, ddNPNone
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
      gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotalPorGrupo", "TotalCompXArt", eSF_SUM, "GHLineaDeProducto", eSR_ALL, eST_SUB_TOTAL
      gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotalDelReporte", "TotalCompXArt", eSF_SUM, "GHMoneda", eSR_ALL, eST_SUB_TOTAL
      gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
    
      Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
      Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
      exito = True
   End If
   fConfigurarDatosDelReporteCostoDeCompraEntreFechas = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteCostoDeCompraEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfiguraLasSeccionesCompra(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                             ByVal valSQL As String, _
                                             ByVal valNombreCampoRollo As String, _
                                            ByVal valNombreCampoSerial As String, ByVal valNombreFileXML As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNombreProveedor As String, _
                                            ByVal valNombreAlmacen As String, _
                                            ByVal valDireccion As String, _
                                            ByVal valNumeroRIF As String, ByVal valNumeroNIT As String, _
                                            ByVal valTelefonos As String, ByVal valNombreOperador As String) As Boolean
   Dim exito As Boolean
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   NombreDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(valNombreFileXML, True, fNombreDelReporteDeCompras(eCOMPRA__COMPRA))
   If gUtilReports.fLoadLayout(refRpt, NombreDelReporte) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Compra"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", " ", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen ", "", "CodigoAlmacen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCompra", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "Fecha"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoProveedor", "", "CodigoProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", valNombreProveedor
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", valDireccion
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoRIF", valNumeroRIF
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoNIT", valNumeroNIT
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefonos", valTelefonos
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtEnvia", valNombreOperador
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtObservaciones", "", "Comentarios"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTotalRenglon", "", "TotalRenglon"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtOtrosGastos", "", "OtrosGastos"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTotal", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtUnidad", "", "Unidad"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMuestra", "", "OpcionParaMostrar"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblSerial", valNombreCampoSerial
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRollo", valNombreCampoRollo
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHArticulo", "ConsecutivoRenglon", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraLasSeccionesCompra = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLasSeccionesCompra", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fConfiguraLaOrdenDeCompra(ByRef refRptOrdenDeCompra As DDActiveReports2.ActiveReport, _
            ByVal valNombreYPathDelXML As String, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNumeroOC As String, ByVal valFecha As Date, ByVal valCodigoAlmacen As String, _
                                            ByVal valNombreProveedor As String, ByVal valCodigoProveedor As String, ByVal valDireccion As String, ByVal valNumeroRIF As String, ByVal valNumeroNIT As String, _
                                            ByVal valTelefonos As String, ByVal valNombreOperador As String, ByVal valCompaniaCodigo As String) As Boolean
   Dim exito As Boolean
   Dim NombreDelReporte As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
    NombreDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(valNombreYPathDelXML, True, valCompaniaCodigo & " - " & fNombreDelReporteDeCompras(eCOMPRA_ORDEN_DE_COMPRA))
 
   If gUtilReports.fLoadLayout(refRptOrdenDeCompra, NombreDelReporte) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRptOrdenDeCompra, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRptOrdenDeCompra, "lblTituloDelReporte", "Orden de Compra"
         gUtilReports.sConfiguraEncabezado refRptOrdenDeCompra, "lblCompania", "lblFechaYHoraDeEmision", _
                                                   "lblTituloDelReporte", "lblNumeroDePagina", _
                                                   "", gProyParametros.GetImprimirNoPagina, _
                                                   gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRptOrdenDeCompra, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRptOrdenDeCompra, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtCodigoAlmacen ", valCodigoAlmacen
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtNumeroOC", valNumeroOC
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtFecha", valFecha
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtCodigoProveedor", valCodigoProveedor
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtNombreProveedor", valNombreProveedor
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtDireccion", valDireccion
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtNoRIF", valNumeroRIF
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtNoNIT", valNumeroNIT
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtTelefonos", valTelefonos
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtEnvia", valNombreOperador
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtObservaciones", "", "Comentarios"
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRptOrdenDeCompra, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRptOrdenDeCompra, "txtPrecio", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtTotalRenglon", "", "TotalRenglon"
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtTotal", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForStrFields refRptOrdenDeCompra, "txtUnidad", "", "Unidad"
         gUtilMargins.sAsignarMargenesParticularesYLosGrabaSiEsNecesario refRptOrdenDeCompra, NombreDelReporte, True, 0.75, True, 0.5, True, 0.5, True, 0.25, True
         Call refRptOrdenDeCompra.AddNamedItem("gUtilReports", gUtilReports)
         Call refRptOrdenDeCompra.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
          exito = True
      End If
    End If
    fConfiguraLaOrdenDeCompra = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraLaOrdenDeCompra", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function sConfigurarDatosYSeccionesDelReporteOrdenesDeComprasEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                         ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valMuestraDetalle As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCompra(eCOMPRA_ORDEN_DE_COMPRA_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Orden de Compra Entre Fechas"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", _
                                                   "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                                   gProyParametros.GetImprimirNoPagina, _
                                                   gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCompra", "", "TotalCompra"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCompraTotalAlcambio", "", "Total al Cambio"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcambio", "", "cambioABolivares"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoDelArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidad", "", "cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtcantidadrecibida", "", "cantidadrecibida", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCostoUnitario", "", "CostoUnitario", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecMoneda", "Moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecCompra", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "GFSecCompra", valMuestraDetalle
          gUtilReports.sConfigurationForSectionPropertiesVisibleAndHeight refRpt, "Detail", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCodigoDelArticulo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblDescripcion", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidadRecibida", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCostoUnitarioTitulo", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblAuxiliar", valMuestraDetalle
          gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCantidad", valMuestraDetalle
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, nombreDelFileXML
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
   sConfigurarDatosYSeccionesDelReporteOrdenesDeComprasEntreFechas = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: sConfigurarDatosYSeccionesDelReporteOrdenesDeComprasEntreFechas = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sConfigurarDatosYSeccionesDelReporteOrdenesDeComprasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function fConfigurarDatosDelReporteComprasImprimirEtiquetas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valNombreDelFileXML As String, ByVal valNombreDelReporte As String, ByVal valTipodeIVA As Boolean) As Boolean
   Dim exito As Boolean
   Dim valConSerial As Boolean
   On Error GoTo h_ERROR
   exito = False
   If gUtilReports.fLoadLayout(refRpt, valNombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigendata", valSqlDelReporte) Then
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForBarcode refRpt, "bcCodigoArticulo", "", "CodigoDelArticulo"
         If valTipodeIVA = True Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPrecio", "Precio Sin IVA:"
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPrecio", "Precio Con IVA:"
         End If
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtColor", "", "Color"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTalla", "", "Talla"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioSinIVA", "", "PrecioSinIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioConIVA", "", "PrecioConIva"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoDelArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "Fecha"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHArticulo", "Concatenado", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPAfter

         gUtilMargins.sAsignarMargenesParticularesYLosGrabaSiEsNecesario refRpt, valNombreDelReporte, True, 0.5, True, 0.5, True, 0.5, True, 0.25, True, ddOPortrait, False, True, 5.5, True, 5.5
         exito = True
      End If
   End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDelReporteComprasImprimirEtiquetas = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteComprasImprimirEtiquetas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

