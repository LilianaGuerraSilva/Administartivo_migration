VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAuditoriaContableRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsAuditoriaContableRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte Auditoria Contable"

Private Enum eAuditoriaContableRpt
   eConRpt_AUDITORIA_CONTABLE = 0
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fNombreFileDeImpresionDelReporteDeAuditoria(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eConRpt_AUDITORIA_CONTABLE: nombreFileXML = "rpxAuditoriaContable"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeAuditoria = _
           gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, _
                                              fNombreDelReporteDeDeAuditoria(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeAuditoria = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDeImpresionDelReporteDeAuditoria", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeDeAuditoria(ByVal valReporte As Integer) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eConRpt_AUDITORIA_CONTABLE: fNombreDelReporteDeDeAuditoria = "Auditoria Contable"
        Case Else: fNombreDelReporteDeDeAuditoria = ""
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fNombreDelReporteDeDeAuditoria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraLasSeccionesDelReporteDeAuditoriaContable( _
            ByRef refRpt As DDActiveReports2.ActiveReport, _
            ByVal valSQL As String, ByVal valFechaInicial As Date, _
            ByVal valFechaFinal As Date, ByVal valTituloDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim NombreDelReporte As String
   On Error GoTo h_ERROR
   exito = False
   NombreDelReporte = fNombreFileDeImpresionDelReporteDeAuditoria(eConRpt_AUDITORIA_CONTABLE)
   If gUtilReports.fLoadLayout(refRpt, NombreDelReporte) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
          gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloDelReporte
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                                       "lblNumeroDePagina", "lblFechaInicialYFinal", _
                                       gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtGeneradoPor", "", "GeneradoPor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDoc", "", "NumeroDoc"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDocumento", "", "MontoDocumento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoComprobante", "", "MontoComprobante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFueModificado", "", "FueModificado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtQuienModifico", "", "QuienModifico"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraLasSeccionesDelReporteDeAuditoriaContable = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfiguraLasSeccionesDelReporteDeAuditoriaContable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



