VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsRetDsr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsRetDsr"
Private Const CM_MESSAGE_NAME As String = "Dsr Ret"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConfigurarDsrRelacionAnual(ByVal valValorDelSql As String, ByVal valImprimirResumen As Boolean, ByVal valSaltarPaginaCuandoCambiaElMes As Boolean, ByVal valMesAplicacionInicial As String, ByVal valAnoAplicacionInicial As String, ByVal valMesAplicacionFinal As String, ByVal valAnoAplicacionFinal As String, ByVal valClausulaFromWhereGeneral As String, ByRef valProveedorNav As Object, ByRef valProyCompaniaActual As Object, ByRef valProyParametrosCompania As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptRelacionAnual
   On Error GoTo h_ERROR
   Set reporte = New rptRelacionAnual
   reporte.sConfigurarDatosDelReporte valValorDelSql, valImprimirResumen, valSaltarPaginaCuandoCambiaElMes, valMesAplicacionInicial, valAnoAplicacionInicial, valMesAplicacionFinal, valAnoAplicacionFinal, valClausulaFromWhereGeneral, valProveedorNav, valProyCompaniaActual, valProyParametrosCompania
   Set fConfigurarDsrRelacionAnual = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrRelacionAnual", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDsrResumenRelacionAnual(ByVal valFechaDeCierre As Date, _
                                          ByVal valNumeroDePaginas As Integer, _
                                             ByVal valTotalMontoPagadoAbonado As Currency, _
                                                ByVal valTotalMontoRetenido As Currency, _
                                                   ByVal valNumeroDeContribuyente As Long, _
                                                      ByVal valMedioDeEntrega As String, _
                                                         ByVal valEntregaDisco As Boolean, Optional ByRef gProyCompaniaActual As Object, Optional ByRef gProyParametrosCompania As Object) As DDActiveReports2.ActiveReport
   Dim reporte As rptSubRepResumenRelacionAnual
   On Error GoTo h_ERROR
   Set reporte = New rptSubRepResumenRelacionAnual
   reporte.sConfigurarDatosDelSubReporte valFechaDeCierre, valNumeroDePaginas, valTotalMontoPagadoAbonado, valTotalMontoRetenido, valNumeroDeContribuyente, valMedioDeEntrega, valEntregaDisco, gProyCompaniaActual, gProyParametrosCompania
   Set fConfigurarDsrResumenRelacionAnual = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDsrResumenRelacionAnual", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

