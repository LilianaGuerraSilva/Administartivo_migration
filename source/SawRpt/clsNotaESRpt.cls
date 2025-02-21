VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsNotaESRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsRptNotaESConfigurar"
Private Const CM_MESSAGE_NAME As String = "Configuracion de NotaES"
Private Const GetGender = Enum_Gender.eg_Male

Private Enum eNotaESRpt
   CM_RPT_NOTA_DE_ENTRADA_SALIDA = 0
End Enum

Private Function fNombreFileDeImpresionDelReporteDeNotaES(ByVal valReporte As eNotaESRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_NOTA_DE_ENTRADA_SALIDA: nombreFileXML = "rpxNotaES"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeNotaES = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeNotaES(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeNotaES = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporteDeNotaES", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fNombreDelReporteDeNotaES(ByVal valReporte As eNotaESRpt) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_NOTA_DE_ENTRADA_SALIDA: fNombreDelReporteDeNotaES = "Nota E/S"
      Case Else: fNombreDelReporteDeNotaES = ""
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelReporteDeNotaES", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteNotaES(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, _
                  ByVal valFecha As String, ByVal valCodigoAlmacen As String, _
                  ByVal valNombreAlmacen As String, ByVal valNumeroNE As String, ByVal valTipoDeOperacion As String, ByVal valComentarios As String, _
                  ByVal valNombreCampoRollo As String, ByVal valNombreCampoSerial As String, ByVal valUsaSerial As Boolean, ByVal valUsaRollo As Boolean, _
                  ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeNotaES(CM_RPT_NOTA_DE_ENTRADA_SALIDA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", valFecha, "", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", valNumeroNE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeOperacion", valTipoDeOperacion
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoAlmacen", valCodigoAlmacen
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreAlmacen", valNombreAlmacen
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtComentarios", valComentarios
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoArticulo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantidad", "", "Cantidad", False, ddTXRight, formatoNDecimales
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", "Cambio a " & gProyParametros.GetSimboloMonedaLocal
         If valUsaSerial Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblSerial", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblSerial", "Serial"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblSerial", valNombreCampoSerial
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSerial", "", "Serial"
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblSerial", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtSerial", False
         End If
         If valUsaRollo Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRollo", "Rollo"
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRollo", valNombreCampoRollo
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRollo", "", "Rollo"
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblRollo", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtRollo", False
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("gProyParametrosCompania", gProyParametrosCompania)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteNotaES = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteNotaES", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


