VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxPDsr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsCxPDsr"
Private Const CM_MESSAGE_NAME As String = "Dsr CxP"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConfigurarDsrLibroDeComprasNAlicuotas(ByVal valSqlDelReporte As String, ByVal valMes As String, ByVal ValAno As String, _
                     ByVal valPorcentajeProrrateo As Currency, ByVal valMostrarInformeDeProrrateo As Boolean, _
                     ByVal valMostrarResumenDeVentas As Boolean, ByVal valNombreCompaniaParaInformes As String, _
                     ByRef refInsFactura As Object, ByRef refInsCompaniaActual As Object, ByRef refInsEnumProyecto As Object, _
                     ByRef refInsNoComun As Object, ByRef refInsRPTFacturaConfigurar As Object, ByRef gGlobalization As Object, ByRef gAdmAlicuotaIvaActual As Object, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, _
                     ByRef valCreditosFiscalesTotalmenteDeducibles As Currency, ByRef valCreditosProrrateablesParcialmenteDeducibles As Currency, _
                     Optional ByVal valAplicaAContribuyenteEspecial As Boolean = False) As DDActiveReports2.ActiveReport
   Dim reporte As rptLibroDeComprasNAlicuotas
   On Error GoTo h_ERROR
   Set reporte = New rptLibroDeComprasNAlicuotas
   reporte.sConfigurarDatosDelReporte valSqlDelReporte, valMes, ValAno, valPorcentajeProrrateo, valMostrarInformeDeProrrateo, valMostrarResumenDeVentas, valNombreCompaniaParaInformes, refInsFactura, refInsCompaniaActual, refInsNoComun, refInsRPTFacturaConfigurar, gGlobalization, gAdmAlicuotaIvaActual, valFechaDesde, valFechaHasta, valCreditosFiscalesTotalmenteDeducibles, valCreditosProrrateablesParcialmenteDeducibles, valAplicaAContribuyenteEspecial
   Set fConfigurarDsrLibroDeComprasNAlicuotas = reporte
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDsrLibroDeComprasNAlicuotas", "Reporte", GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


