VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLibrosCompraVentaRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsLibrosCompraVentaRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Libros de Compra y Venta"

Private Enum eCxPRpt
   eCxPRpt_REGISTRO_DE_COMPRAS_XLS_PERU = 0
   eCxPRpt_REGISTRO_DE_VENTAS_XLS_PERU
End Enum
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDeLibroCompraVenta(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eCxPRpt_REGISTRO_DE_COMPRAS_XLS_PERU: nombreFileXML = "rpxRegistroDeComprasAXLS"
      Case eCxPRpt_REGISTRO_DE_VENTAS_XLS_PERU: nombreFileXML = "rpxRegistroDeVentasAXLS"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDeLibroCompraVenta = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fEnumReporteDeLibroCompraVentaToString(valReporte))
   Else
      fNombreFileDeImpresionDeLibroCompraVenta = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDeImpresionDeLibroCompraVenta", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEnumReporteDeLibroCompraVentaToString(ByVal valReporte As Integer) As String
   Select Case valReporte
      Case eCxPRpt_REGISTRO_DE_COMPRAS_XLS_PERU: fEnumReporteDeLibroCompraVentaToString = "Registro de Compras"
      Case Else: fEnumReporteDeLibroCompraVentaToString = ""
   End Select
End Function

Public Function fConfigurarDatosDelRegistroDeComprasPeruEnXLS(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                               ByVal valSqlDelReporte As String, _
                                                                ByRef refNombreXLS As String, _
                                                                 ByVal valMesDesde As String, _
                                                                  ByVal valAnoDesde As String, _
                                                                   ByVal valMesHasta As String, _
                                                                    ByVal valAnoHasta As String, _
                                                                     ByRef gGlobalization As Object, _
                                                                      ByRef Conexion As Object, _
                                                                       ByVal valNombredeCompaniaParaReporte As String, _
                                                                        ByVal valCodigoCompania As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   refNombreXLS = ""
   nombreDelFileXML = fNombreFileDeImpresionDeLibroCompraVenta(eCxPRpt_REGISTRO_DE_COMPRAS_XLS_PERU)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte, Conexion) Then
         If gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesDesde, valAnoDesde, True) <> _
            gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesHasta, valAnoHasta, True) Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Formato 8.1 Registro de Compras Período desde el Mes de " & gEnumReport.enumMesToString(valMesDesde) & " - " & valAnoDesde _
                  & " hasta " & gEnumReport.enumMesToString(valMesHasta) & " - " & valAnoHasta
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Formato 8.1 Registro de Compras Período del Mes de " & gEnumReport.enumMesToString(valMesDesde) & " - " & valAnoDesde
         End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombredeCompaniaParaReporte
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtNoOperacion", "ParaContadorRegistro", eSF_COUNT, "Detail", eSR_ALL, eST_SUB_TOTAL, "#0"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaEmision", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumento", "", "TipoDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroSerieDocumento", "", "SerieDeDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDeDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoProveedor", "", "CodPeruTipoDocumentoIdentificacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumentoProveedor", "", "NumeroRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtlRazonSocial", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "0.00", "BaseImponibleOpGrav", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIGV", "0.00", "MontoAlicuotaOpGrav", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible2", "0.00", "BaseImponibleOpGravYNoGrav", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIGV2", "0.00", "MontoAlicuotaOpGravYNoGrav", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible3", "0.00", "BaseImponibleOpNoGrav", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIGV3", "0.00", "MontoAlicuotaOpNoGrav", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExonerado", "0.00", "MontoExento", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtISC", "0.00", "MontoIvaalicuota3", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImporteTotalDocumento", "0.00", "ImporteTotal", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "0.00", "CambioABolivares", True, ddTXRight
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDocumentoOriginal", "", "FechaDocumentoOriginal", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoOriginal", "", "TipoDocumentoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroSerieOriginal", "", "SerieDocumentoAfectado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroOriginal", "", "NumeroDocumentoAfectado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDepositoDetraccion", "", "NumeroDetraccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoComprobanteDePagoEmitido", "", "NumeroDetraccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaEmisionDepositoDetraccion", "", "FechaDetraccion"
         
         'campos sin valor asignado
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAnoDua", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtOtrosTributosYCargos", "0.00", "", True, ddTXRight
         
          
         'campos totalizadores Formato
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalIGV", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseimponible2", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalIGV2", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseimponible3", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalIGV3", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoExonerado", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalISC", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalOtrosTributosYCargos", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalImporteTotalDocumento", "0.00", "", True, ddTXRight

         'campos totalizadores asigancion
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "BaseImponibleOpGrav", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIGV", "MontoAlicuotaOpGrav", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseimponible2", "BaseImponibleOpGravYNoGrav", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIGV2", "MontoAlicuotaOpGravYNoGrav", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseimponible3", "BaseImponibleOpNoGrav", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIGV3", "MontoAlicuotaOpNoGrav", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoExonerado", "MontoExento", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalISC", "MontoIvaalicuota3", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalOtrosTributosYCargos", "", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalImporteTotalDocumento", "ImporteTotal", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         refNombreXLS = "RegistroDeCompras_" & valAnoDesde & "_" & gTexto.llenaConCaracterALaIzquierda(valMesDesde, "0", 2) & _
                   "_" & valCodigoCompania & "_"
         exito = True
      End If
   End If
   fConfigurarDatosDelRegistroDeComprasPeruEnXLS = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelRegistroDeComprasPeruEnXLS", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelRegistroDeVentasPeruEnXLS(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                               ByVal valSqlDelReporte As String, _
                                                                ByRef refNombreXLS As String, _
                                                                 ByVal valMesDesde As String, _
                                                                  ByVal valAnoDesde As String, _
                                                                   ByVal valMesHasta As String, _
                                                                    ByVal valAnoHasta As String, _
                                                                     ByRef gGlobalization As Object, _
                                                                      ByRef Conexion As Object, _
                                                                       ByVal valNombredeCompaniaParaReporte As String, _
                                                                        ByVal valCodigoCompania As String) As Boolean
   Dim nombreDelFileXML As String
   Dim NombreDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   refNombreXLS = ""
   nombreDelFileXML = fNombreFileDeImpresionDeLibroCompraVenta(eCxPRpt_REGISTRO_DE_VENTAS_XLS_PERU)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte, Conexion) Then
         If gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesDesde, valAnoDesde, True) <> _
            gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesHasta, valAnoHasta, True) Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Formato 14.1 Registro de Ventas e Ingresos Período desde el mes de " & gEnumReport.enumMesToString(valMesDesde) & " - " & valAnoDesde _
                  & " hasta " & gEnumReport.enumMesToString(valMesHasta) & " - " & valAnoHasta
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", "Formato 14.1 Registro de Ventas e Ingresos Período Mes de  " & gEnumReport.enumMesToString(valMesDesde) & " - " & valAnoDesde
         End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombredeCompaniaParaReporte
         
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtNoOperacion", "ParaContadorRegistro", eSF_COUNT, "Detail", eSR_ALL, eST_SUB_TOTAL, "#0"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaEmision", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechadeVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumento", "", "CodPeruTipoDocumentoFactura"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroSerieDocumento", "", "SerieDeDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDeDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoCliente", gGlobalization.fPromptRIF, "CodPeruTipoDocumentoIdentificacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumentoCliente", "", "NumeroRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtlRazonSocial", "", "Nombre"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtValorFacturadoExportado", "0.00", "MontoExportacion", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "0.00", "TotalBaseImponible", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExonerado", "0.00", "TotalMontoExento", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtISC", "0.00", "MontoIVAAlicuota3", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIGV", "0.00", "MontoIVAAlicuota1", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImporteTotalDocumento", "0.00", "TotalFactura", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "0.00", "CambioABolivares", True, ddTXRight
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDocumentoOriginal", "", "FechaFacturaAfectada", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDocumentoOriginal", "", "TipoDeDocumentoOriginal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroSerieOriginal", "", "SerieDocumentoAfectado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroOriginal", "", "NumeroDocumentoAfectado"
         
         'campos sin valor asignado
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtInafecta", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtOtrosTributosYCargos", "0.00", "", True, ddTXRight
         
         'campos totalizadores Formato
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalValorFacturadoExportado", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalBaseImponible", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoExonerado", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalInafecta", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalISC", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalIGV", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalOtrosTributosYCargos", "0.00", "", True, ddTXRight
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalImporteTotalDocumento", "0.00", "", True, ddTXRight
      
         'campos totalizadores asigancion
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalValorFacturadoExportado", "MontoExportacion", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "TotalBaseImponible", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoExonerado", "TotalMontoExento", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalInafecta", "", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalISC", "MontoIVAAlicuota3", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalIGV", "MontoIVAAlicuota1", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalOtrosTributosYCargos", "", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalImporteTotalDocumento", "TotalFactura", eSF_SUM, "GHTitulos", eSR_GROUP, eST_SUB_TOTAL
   
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         refNombreXLS = "RegistroDeVentas_" & valAnoDesde & "_" & gTexto.llenaConCaracterALaIzquierda(valMesDesde, "0", 2) & _
                   "_" & valCodigoCompania & "_"
         exito = True
      End If
   End If
   fConfigurarDatosDelRegistroDeVentasPeruEnXLS = exito
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelRegistroDeVentasPeruEnXLS", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

