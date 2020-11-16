VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsMovimientoBancarioRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsMovimientoBancarioRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte Movimiento Bancario"

Private Enum eBancoRpt
   eMovBanRpt_MOV_BAN_ENTRE_FECHAS = 0
   eMovBanRpt_MOV_BAN_ENTRE_FECHAS_BENEFICIARIO
   eMovBanRpt_MOV_BAN_COMPROBANTE
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreReporteParaMensajes(ByVal valIndexDelReporte As eBancoRpt) As String
   Dim varNombreReporte As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eMovBanRpt_MOV_BAN_ENTRE_FECHAS: varNombreReporte = "Movimientos Bancarios Entre Fechas"
      Case eMovBanRpt_MOV_BAN_ENTRE_FECHAS_BENEFICIARIO: varNombreReporte = "Movimientos Bancarios Entre Fechas por Beneficiario"
      Case eMovBanRpt_MOV_BAN_COMPROBANTE: varNombreReporte = "Comprobante De Movimiento Bancario"
      Case Else: varNombreReporte = ""
   End Select
h_EXIT: On Error GoTo 0
   fNombreReporteParaMensajes = varNombreReporte
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreReporteParaMensajes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreFileDeImpresionDelReporte(ByVal valIndexDelReporte As eBancoRpt) As String
   Dim varNombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eMovBanRpt_MOV_BAN_ENTRE_FECHAS: varNombreFileXML = "rpxMovBancarioEntreFechas"
      Case eMovBanRpt_MOV_BAN_ENTRE_FECHAS_BENEFICIARIO: varNombreFileXML = "rpxMovBancarioEntreFechasXBeneficiarios"
      Case eMovBanRpt_MOV_BAN_COMPROBANTE: varNombreFileXML = "rpxComprobanteMovimientoBancario"
      Case Else: varNombreFileXML = ""
   End Select
   If varNombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(varNombreFileXML, True, fNombreReporteParaMensajes(valIndexDelReporte))
   Else
      fNombreFileDeImpresionDelReporte = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreFileDeImpresionDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelReporteDeSaldosBancarios(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, _
         ByVal valAgrupaBeneficiario As Boolean) As Boolean
   Dim nombreDelFile As String
   Dim tituloDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valAgrupaBeneficiario Then
      nombreDelFile = fNombreFileDeImpresionDelReporte(eMovBanRpt_MOV_BAN_ENTRE_FECHAS_BENEFICIARIO)
      tituloDelReporte = fNombreReporteParaMensajes(eMovBanRpt_MOV_BAN_ENTRE_FECHAS_BENEFICIARIO)
   Else
      nombreDelFile = fNombreFileDeImpresionDelReporte(eMovBanRpt_MOV_BAN_ENTRE_FECHAS)
      tituloDelReporte = fNombreReporteParaMensajes(eMovBanRpt_MOV_BAN_ENTRE_FECHAS)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "FechaMovimiento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "Monto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         'gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambioabolivares", "", "Cambioabolivares"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBeneficiario", "", "Beneficiario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeCheque", "", "NumeroDeCheque"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConcepto", "", "CodigoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConcepto", "", "Concepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtEstatus", "", "Estatus"
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoConcepto", "", "TipoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCuenta", "", "CodigoCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHTipo", "TipoConcepto", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHMoneda", "moneda", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCuenta", "CodigoCuenta", ddGrpAll, ddRepeatAll, True, ddNPNone
         
         If valAgrupaBeneficiario Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHBeneficiario", "Beneficiario", ddGrpAll, ddRepeatAll, True, ddNPNone
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtBeneficiarioMonto", "Monto", eSF_SUM, "GHBeneficiario", eSR_GROUP, eST_SUB_TOTAL
         End If
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoCuenta", "Monto", eSF_SUM, "GHCuenta", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoMoneda", "Monto", eSF_SUM, "GHMoneda", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTipoMonto", "Monto", eSF_SUM, "GHTipo", eSR_GROUP, eST_SUB_TOTAL

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelReporteDeSaldosBancarios = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelReporteDeSaldosBancarios", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelComprobanteDeMovimientoBancario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim tituloDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
      nombreDelFile = fNombreFileDeImpresionDelReporte(eMovBanRpt_MOV_BAN_COMPROBANTE)
      tituloDelReporte = fNombreReporteParaMensajes(eMovBanRpt_MOV_BAN_COMPROBANTE)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFecha", "", "Fecha"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "Monto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBeneficiario", "", "Beneficiario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConcepto", "", "CodigoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConcepto", "", "DescripcionConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtEstatus", "", "Estatus"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCuentaBancaria", "", "CodigoCtaBancaria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBanco", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGeneradoPor", "", "NombreOperador"
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelComprobanteDeMovimientoBancario = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelComprobanteDeMovimientoBancario", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



