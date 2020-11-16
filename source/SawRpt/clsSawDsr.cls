VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSawDsr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsSawDsr"
Private Const CM_MESSAGE_NAME As String = "Dsr Saw"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConfigurarDsrInformeDeDocumentosContabilizados(ByVal valSqlDelReporte As String, ByVal valOpcionTipoDeDocumento As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByVal valopcionTipoDocumentoEnum As String, ByRef valEnumContabilidad As Object, ByRef valEnumProyecto As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptInformeDeDocumentosContabilizados
   On Error GoTo h_ERROR
   Set reporte = New rptInformeDeDocumentosContabilizados
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valOpcionTipoDeDocumento, valFechaInicial, valFechaFinal, valNombreCompaniaParaInformes, valopcionTipoDocumentoEnum, valEnumContabilidad, valEnumProyecto
   Set fConfigurarDsrInformeDeDocumentosContabilizados = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrInformeDeDocumentosContabilizados", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrInformeDocumentosSinComp(ByVal valSqlDelReporte As String, ByVal valOpcionTipoDeDocumento As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef valEnumProyectoWincont As Object, ByRef valEnumProyecto As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptInformeDocumentosSinComp
   On Error GoTo h_ERROR
   Set reporte = New rptInformeDocumentosSinComp
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valOpcionTipoDeDocumento, valFechaInicial, valFechaFinal, valNombreCompaniaParaInformes, valEnumProyectoWincont, valEnumProyecto
   Set fConfigurarDsrInformeDocumentosSinComp = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrInformeDocumentosSinComp", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrInformeDeCompSinDocOrigen(ByVal valSqlDelReporte As String, ByVal valOpcionTipoDeDocumento As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As DDActiveReports2.ActiveReport
   Dim reporte As rptInformeDeCompSinDocOrigen
   On Error GoTo h_ERROR
   Set reporte = New rptInformeDeCompSinDocOrigen
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valOpcionTipoDeDocumento, valFechaInicial, valFechaFinal, valNombreCompaniaParaInformes
   Set fConfigurarDsrInformeDeCompSinDocOrigen = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrInformeDeCompSinDocOrigen", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


