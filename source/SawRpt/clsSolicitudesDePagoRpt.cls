VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSolicitudesDePagoRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRptSolicitudesDePagoRpt"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Configurar Reporte SolicitudesDePago"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_RPT_SOLICITUD_DE_PAGO_ENTRE_FECHA() As Integer
   CM_RPT_SOLICITUD_DE_PAGO_ENTRE_FECHA = 0
End Function
Private Function CM_RPT_SOLICITUD_DE_PAGO_POR_FORMA_DE_PAGO() As Integer
   CM_RPT_SOLICITUD_DE_PAGO_POR_FORMA_DE_PAGO = 1
End Function
Private Function CM_RPT_SOLICITUD_DE_PAGO() As Integer
   CM_RPT_SOLICITUD_DE_PAGO = 2
End Function

Private Function fNombreFileDeImpresionDelReporteDeSolicitudesDePago(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_SOLICITUD_DE_PAGO_ENTRE_FECHA: nombreFileXML = "rpxSolicitudDePagoEntreFecha"
      Case CM_RPT_SOLICITUD_DE_PAGO_POR_FORMA_DE_PAGO: nombreFileXML = "rpxSolicitudDePagoPorFormaDePago"
      Case CM_RPT_SOLICITUD_DE_PAGO: nombreFileXML = "rpxSolicitudDePago"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeSolicitudesDePago = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeSolicitudesDePago(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeSolicitudesDePago = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeSolicitudesDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeSolicitudesDePago(ByVal valReporte As Integer) As String
   Select Case valReporte
      Case CM_RPT_SOLICITUD_DE_PAGO_ENTRE_FECHA: fNombreDelReporteDeSolicitudesDePago = "Solicitudes De Pago Entre Fecha"
      Case CM_RPT_SOLICITUD_DE_PAGO_POR_FORMA_DE_PAGO: fNombreDelReporteDeSolicitudesDePago = "Solicitudes De Pago Por Forma de pago"
      Case CM_RPT_SOLICITUD_DE_PAGO: fNombreDelReporteDeSolicitudesDePago = "Solicitud De Pago "
      Case Else: fNombreDelReporteDeSolicitudesDePago = ""
   End Select
End Function

Public Function fConfigurarDatosDelReporteSolicitudDePagoEntreFecha(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeSolicitudesDePago(CM_RPT_SOLICITUD_DE_PAGO_ENTRE_FECHA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaSolicitud", "", "FechaSolicitud", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumentoOrigen", "", "NumeroDocumentoOrigen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusSolicitudStr", "", "StatusSolicitudStr"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "Monto"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_MontoSecTotalGeneral", "Monto", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_MontoSecTotalGeneral", "", "Monto"

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeSolicitudesDePago(CM_RPT_SOLICITUD_DE_PAGO_ENTRE_FECHA)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteSolicitudDePagoEntreFecha = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteSolicitudDePagoEntreFecha = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteSolicitudDePagoEntreFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteSolicitudDePagoPorFormaDePago(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeSolicitudesDePago(CM_RPT_SOLICITUD_DE_PAGO_POR_FORMA_DE_PAGO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
           gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & valFechaInicial & " al " & valFechaFinal
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
    
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaSolicitud", "", "FechaSolicitud", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumentoOrigen", "", "NumeroDocumentoOrigen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusSolicitudStr", "", "StatusSolicitudStr"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEfectivo", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTransferencia", "", "Transferencia"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCheque", "", "Cheque"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Efectivo_SecTotalGeneral", "Efectivo", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Transferencia_SecTotalGeneral", "Transferencia", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Cheque_SecTotalGeneral", "Cheque", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Efectivo_SecTotalGeneral", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Transferencia_SecTotalGeneral", "", "Transferencia"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Cheque_SecTotalGeneral", "", "Cheque"

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeSolicitudesDePago(CM_RPT_SOLICITUD_DE_PAGO_POR_FORMA_DE_PAGO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteSolicitudDePagoPorFormaDePago = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteSolicitudDePagoPorFormaDePago = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteSolicitudDePagoPorFormaDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteSolicitudDePago(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeSolicitudesDePago(CM_RPT_SOLICITUD_DE_PAGO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaSolicitud", "", "FechaSolicitud", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumentoOrigen", "", "NumeroDocumentoOrigen"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusSolicitudStr", "", "StatusSolicitudStr"
      
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRIF", "", "RIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFormaDePagoStr", "", "FormaDePagoStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreBeneficiario", "", "NombreBeneficiario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusStr", "", "StatusStr"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumentoMovimiento", "", "NumeroDocumentoMovimiento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCuentaBancaria", "", "CuentaBancaria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobante", "", "NumeroComprobante"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "Monto"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Monto_SecFormaDePago", "Monto", eSF_SUM, "GHSecFormaDePago", eSR_GROUP, eST_SUB_TOTAL

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtT_Monto_SecFormaDePago", "", "Monto"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtT_Total_SecTotalGeneral", "Monto", eSF_SUM, "GHSecTotalGeneral", eSR_GROUP, eST_SUB_TOTAL


         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecTotalGeneral", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
 
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecSolicitud", "", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecFormaDePago", "FormaDePagoStr", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
   
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeSolicitudesDePago(CM_RPT_SOLICITUD_DE_PAGO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteSolicitudDePago = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteSolicitudDePago = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteSolicitudDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
