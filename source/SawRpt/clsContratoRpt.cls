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

Private Function fNombreDelReporteDeContrato(ByVal valReporte As Integer) As String
   Select Case valReporte
      Case CM_RPT_IMPRIMIR_CONTRATO: fNombreDelReporteDeContrato = "Contrato"
      Case CM_RPT_CONTRATO_ENTRE_FECHAS: fNombreDelReporteDeContrato = "Contratos Entre Fechas"
      Case Else: fNombreDelReporteDeContrato = ""
   End Select
End Function

Public Function fConfigurarDatosDelReporteImprimirContrato(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeContrato(CM_RPT_IMPRIMIR_CONTRATO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroContrato", "", "NumeroContrato"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoClienteAFacturar", "", "CodigoClienteAFacturar"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreClienteAFacturar", "", "NombreClienteAFacturar"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "StatusContrato"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDuracion", "", "Duracion"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaInicio", "", "FechaDeInicio", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaFinal", "", "FechaFinal", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtObservaciones", "", "observaciones"
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtArticulo", "", "Articulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecio", "", "Imponible"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeDescuento", "", "PorcentajeDescuento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDetail", "", "TotalRenglon"

         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotal", "TotalRenglon", eSF_SUM, "GHSecContrato", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecContrato", "NumeroContrato", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporteDeContrato(CM_RPT_IMPRIMIR_CONTRATO)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteImprimirContrato = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteImprimirContrato = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteImprimirContrato", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteContratoEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                                                               ByVal valNombreCompaniaParaInformes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeContrato(CM_RPT_CONTRATO_ENTRE_FECHAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
                  "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroContrato", "", "NumeroContrato"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", "", "CodigoCliente"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusContrato", "", "StatusContrato"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtObservaciones", "", "Observaciones"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoClienteAFacturar", "", "CodigoClienteAFacturar"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreClienteAFacturar", "", "NombreClienteAFacturar"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDuracionDelContrato", "", "DuracionDelContrato"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaDeInicio", "", "FechaDeInicio", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaFinal", "", "FechaFinal", eFT_DATE


         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecContrato", "StatusContrato", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporteDeContrato(CM_RPT_CONTRATO_ENTRE_FECHAS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelReporteContratoEntreFechas = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteContratoEntreFechas = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteContratoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

