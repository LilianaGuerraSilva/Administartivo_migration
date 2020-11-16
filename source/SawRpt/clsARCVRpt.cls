VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsARCVRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsARCVRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte ARCV"

Private Enum eARCVRpt
   eARCV_COMPROBANTE_ARCV = 0
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporteDeARCV(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eARCV_COMPROBANTE_ARCV: nombreFileXML = "rpxComprobanteARCV"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeARCV = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeARCV(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeARCV = ""
   End If
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeARCV", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeARCV(ByVal valReporte As Integer) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eARCV_COMPROBANTE_ARCV: fNombreDelReporteDeARCV = "Comprobante ARCV"
      Case Else: fNombreDelReporteDeARCV = ""
   End Select
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelReporteDeARCV", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteARCV(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompania As String, ByVal valNumeroDeRif As String, ByVal valTipoDeContribuyenteStr As String, ByVal valDireccion As String, ByVal valTelefonosDeLaCompaniaEnUnaSolaCadena As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeARCV(eARCV_COMPROBANTE_ARCV)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAgente", valNombreCompania
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRifAgente", valNumeroDeRif
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeAgenteDeRetencion", valTipoDeContribuyenteStr
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccionAgente", valDireccion
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefonosAgente", valTelefonosDeLaCompaniaEnUnaSolaCadena
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreProveedor", "", "NombreProveedor"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRifProveedor", "", "NumeroRif"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDePersona", "", "TipoDePersona"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccionProveedor", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefonosProveedor", "", "Telefonos"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtPeriodo", "", "Periodo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMeses", "", "MesParaARCV"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalPagadoAbonadoEnCta", "", "PagadoAbonado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadObjetoDeRetencion", "", "BaseImponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImpuestoRetenido", "", "ImpuestoRetenido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadObjetoRetencionAcumulada", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtImpuestoRetencionAcumulado", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAcumTotalPagadoAbonadoEnCta", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAcumCantidadObjetoDeRetencion", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAcumImpuestoRetenido", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRIF2", "R.I.F. Nº " & valNumeroDeRif
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "Numero", ddGrpNone, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDelReporteARCV = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteARCV", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


