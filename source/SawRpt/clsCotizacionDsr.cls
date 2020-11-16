VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCotizacionDsr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsCotizacionDsr"
Private Const CM_MESSAGE_NAME As String = "Dsr Cotización"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConfigurarDsrCotizacionPorDia(ByVal valValorDelSql As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valEfecturaCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String) As DDActiveReports2.ActiveReport
   Dim reporte As rptCotizacionPorDia
   On Error GoTo h_ERROR
   Set reporte = New rptCotizacionPorDia
   reporte.sConfigurarDatosDelReporte valValorDelSql, valFechaInicial, valFechaFinal, valEfecturaCambioABs, valNombreCompaniaParaInformes
   Set fConfigurarDsrCotizacionPorDia = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrCotizacionPorDia", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



