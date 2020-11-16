VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsControlDespachoRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsControlDespachoRpt"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Configurar Reporte ControlDespacho"
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function CM_RPT_IMPRESION_COMPROBANTE_DE_DESPACHO() As Integer
   CM_RPT_IMPRESION_COMPROBANTE_DE_DESPACHO = 0
End Function
Private Function CM_RPT_LISTADO_DE_DESPACHOS() As Integer
   CM_RPT_LISTADO_DE_DESPACHOS = 1
End Function

Private Function fNombreFileDeImpresionDelReporteDeControlDespacho(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_IMPRESION_COMPROBANTE_DE_DESPACHO: nombreFileXML = "rpxImpresionComprobanteDeDespacho"
      Case CM_RPT_LISTADO_DE_DESPACHOS: nombreFileXML = "rpxListadoDeDespachos"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeControlDespacho = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeControlDespacho(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeControlDespacho = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeControlDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeControlDespacho(ByVal valReporte As Integer) As String
   Select Case valReporte
      Case CM_RPT_IMPRESION_COMPROBANTE_DE_DESPACHO: fNombreDelReporteDeControlDespacho = "Comprobante de despacho"
      Case CM_RPT_LISTADO_DE_DESPACHOS: fNombreDelReporteDeControlDespacho = "Listado de Despachos"
      Case Else: fNombreDelReporteDeControlDespacho = ""
   End Select
End Function

Public Function fConfigurarDatosDelReporteImpresionComprobanteDeDespacho(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeControlDespacho(CM_RPT_IMPRESION_COMPROBANTE_DE_DESPACHO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroEmpaque", "", "NumeroEmpaque"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCotizacion", "", "NumeroCotizacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaApertura", "", "FechaApertura", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCierre", "", "FechaCierre", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusControl", "", "StatusControl"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtObservaciones", "", "Observaciones"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoArticulo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionArticulo", "", "DescripcionArticulo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidadDespachada", "", "CantidadDespachada"

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecDespacho", "Numero", ddGrpAll, ddRepeatAll, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecNumeroEmpaque", "NumeroEmpaque", ddGrpAll, ddRepeatAll, True, ddNPBeforeAfter
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeControlDespacho(CM_RPT_IMPRESION_COMPROBANTE_DE_DESPACHO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteImpresionComprobanteDeDespacho = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteImpresionComprobanteDeDespacho = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteImpresionComprobanteDeDespacho", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteListadoDeDespachos(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeControlDespacho(CM_RPT_LISTADO_DE_DESPACHOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCotizacion", "", "NumeroCotizacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaApertura", "", "FechaApertura", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaCierre", "", "FechaCierre", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusControl", "", "StatusControl"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtObservaciones", "", "Observaciones"

         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecStatus", "StatusControl", ddGrpAll, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeControlDespacho(CM_RPT_LISTADO_DE_DESPACHOS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteListadoDeDespachos = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteListadoDeDespachos = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteListadoDeDespachos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

