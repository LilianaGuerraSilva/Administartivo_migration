VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContratoRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsRptContratoConfigurar"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Configurar Reporte Contrato"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_RPT_IMPRIMIR_CONTRATO() As Integer
   CM_RPT_IMPRIMIR_CONTRATO = 0
End Function
Private Function CM_RPT_CONTRATO_ENTRE_FECHAS() As Integer
   CM_RPT_CONTRATO_ENTRE_FECHAS = 1
End Function

Private Function fNombreFileDeImpresionDelReporteDeContrato(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_IMPRIMIR_CONTRATO: nombreFileXML = "rpxImprimirContrato"
      Case CM_RPT_CONTRATO_ENTRE_FECHAS: nombreFileXML = "rpxContratoEntreFechas"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeContrato = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeContrato(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeContrato = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeContrato", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
