VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRendicionRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRendicionRpt"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Configurar Reporte Rendicion"
End Function

Private Function CM_RPT_COMPROBANTE_DE_RENDICION() As Integer
   CM_RPT_COMPROBANTE_DE_RENDICION = 0
End Function

Private Function CM_RPT_CHEQUE_DE_RENDICION() As Integer
   CM_RPT_CHEQUE_DE_RENDICION = 1
End Function
Private Function CM_RPT_COMPROBANTE_DE_REPOSICION() As Integer
   CM_RPT_COMPROBANTE_DE_REPOSICION = 2
End Function

Private Function fNombreFileDeImpresionDelReporteDeRendicion(ByVal valReporte As Integer, Optional ByVal insCuentaBancaria As Object) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_COMPROBANTE_DE_RENDICION: nombreFileXML = "rpxComprobanteDeRendicion"
      Case CM_RPT_CHEQUE_DE_RENDICION: nombreFileXML = fNombrePlantillaCheque(insCuentaBancaria)
      Case CM_RPT_COMPROBANTE_DE_REPOSICION: nombreFileXML = "rpxComprobanteDeReposicion"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeRendicion = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeRendicion(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeRendicion = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeRendicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeRendicion(ByVal valReporte As Integer) As String
   Select Case valReporte
      Case CM_RPT_COMPROBANTE_DE_RENDICION: fNombreDelReporteDeRendicion = "Comprobante De Rendición"
      Case Else: fNombreDelReporteDeRendicion = ""
   End Select
End Function

Public Function fConfigurarDatosDelReporteComprobanteDeRendicion(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                                                                  ByRef gProyCompaniaActual As Object, ByVal montoCheque As String, ByVal Beneficiario As String, ByVal Fecha As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeRendicion(CM_RPT_COMPROBANTE_DE_REPOSICION)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", gProyCompaniaActual.GetNombreCompaniaParaInformes(False)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
'         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
'                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroReposicion", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "FechaCierre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusRendicion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCajaChica", "", "CodigoCtaBancariaCajaChica"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCuentaBancaria", "", "CodigoCuentaBancaria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBeneficiario", "", "BeneficiarioCheque"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoCheque", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDelCheque", "", "totalGastos"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBanco", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaDeLosDocumentos", "", "Simbolo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNoDocumento", "", "NumeroFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoExento", "", "MontoExento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoIVA", "", "MontoIVA"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRetencion", "", "MontoRetencion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotalFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoGravable", "", "MontoGravable"
      
        
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaCierre", eFT_DATE
        
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHDetalleDeDocumentos", "Orden", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoExento", "MontoExento", eSF_SUM, "GFDetalleDeDocumentos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoIVA", "MontoIVA", eSF_SUM, "GFDetalleDeDocumentos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoRetencion", "MontoRetencion", eSF_SUM, "GFDetalleDeDocumentos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDocumentos", "MontoTotalFactura", eSF_SUM, "GFDetalleDeDocumentos", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalMontoGravable", "MontoGravable", eSF_SUM, "GFDetalleDeDocumentos", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeRendicion(CM_RPT_COMPROBANTE_DE_RENDICION)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
Dim insNumeroEnLetras As clsNumeroEnLetras


  Set insNumeroEnLetras = New clsNumeroEnLetras
   If (True) Then
      gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportCheque(insNumeroEnLetras, gProyCompaniaActual, montoCheque, Beneficiario, Fecha), "srptCheque"
   End If
   
    gUtilMargins.sAsignarMargenesParticularesYLosGrabaSiEsNecesario refRpt, fNombreDelReporteDeRendicion(CM_RPT_COMPROBANTE_DE_RENDICION), True, 0.25, True, 0.25, True, 0.25, True, 0.25
  
   fConfigurarDatosDelReporteComprobanteDeRendicion = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteComprobanteDeRendicion = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteComprobanteDeRendicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSubReportCheque(ByVal insNumeroEnLetras As clsNumeroEnLetras, _
                     ByVal gProyCompaniaActual As Object, ByVal montoCheque As String, ByVal Beneficiario As String, ByVal Fecha As Date) As DDActiveReports2.ActiveReport
   Dim rptSubReport As DDActiveReports2.ActiveReport
   Dim montoEnLetras As String
   Dim montoEnLetrasLinea1 As String
   Dim montoEnLetrasLinea2 As String
   Dim puntoDeCorte As Long
   On Error GoTo h_ERROR
   Set rptSubReport = New DDActiveReports2.ActiveReport
  
  '#############################################
  
   'montoEnLetras = insNumeroEnLetras.fNumberToLetters(gConvert.fConvierteACurrency("5000"), False, valRendicionesNav.GetMoneda, False, False)
   montoEnLetras = insNumeroEnLetras.fNumberToLetters(gConvert.fConvierteACurrency(montoCheque), False, "", False, False)
   montoEnLetrasLinea1 = montoEnLetras
   montoEnLetrasLinea2 = ""
   If gTexto.DfLen(montoEnLetrasLinea1) > 85 Then
      puntoDeCorte = gTexto.fBuscaDondeCortar(montoEnLetrasLinea1, 80)
      montoEnLetrasLinea1 = gTexto.DfLeft(montoEnLetrasLinea1, puntoDeCorte)
      montoEnLetrasLinea2 = gTexto.DfMid(montoEnLetrasLinea1, puntoDeCorte + 1)
   End If
   If montoEnLetrasLinea2 = "" Then
      montoEnLetrasLinea1 = " *** " & montoEnLetrasLinea1 & " ***"
   Else
      montoEnLetrasLinea2 = montoEnLetrasLinea2 & " ***"
   End If
   
  '###############################################
  
  
  
   If gUtilReports.fLoadLayout(rptSubReport, fNombreFileDeImpresionDelReporteDeRendicion(CM_RPT_CHEQUE_DE_RENDICION)) Then
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblMontoCheque", "***" & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(montoCheque)) & "***"
      
      
'      If insProveedor.GetUsarBeneficiarioImpCheq Then
'         gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblBeneficiario", "*** " & insProveedor.GetBeneficiario & " ***"
'      Else
         gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblBeneficiario", "*** " & Beneficiario & " ***"
'      End If
      
      
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblMontoEnLetrasLinea1", montoEnLetrasLinea1
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblMontoEnLetrasLinea2", montoEnLetrasLinea2
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblFechaDeEmision", gProyCompaniaActual.GetCiudad & ", " & gConvert.fConvierteFechaAStringFechaCompleta(gConvert.dateToString(Fecha), False)
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblAno", gTexto.DfCStr(Year(gConvert.dateToString(Fecha)))
   End If
   Set GetSubReportCheque = rptSubReport
   Set insNumeroEnLetras = Nothing




h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetSubReportCheque", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombrePlantillaCheque(ByVal insCuentaBancaria As Object) As String
   On Error GoTo h_ERROR
   If insCuentaBancaria Is Nothing Then
      fNombrePlantillaCheque = "rpxChequeGenerico"
   Else
      fNombrePlantillaCheque = insCuentaBancaria.GetNombrePlantillaCheque
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fNombrePlantillaCheque", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
