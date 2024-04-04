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

Private Enum eNotaESRpt
   CM_RPT_NOTA_DE_ENTRADA_SALIDA = 0
   CM_RPT_NOTA_DE_ENTRADA_SALIDA_CON_PRECIO = 1
End Enum
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporteDeNotaES(ByVal valReporte As eNotaESRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_NOTA_DE_ENTRADA_SALIDA: nombreFileXML = "rpxNotaES"
      Case CM_RPT_NOTA_DE_ENTRADA_SALIDA_CON_PRECIO: nombreFileXML = "rpxNotaESconPrecioUnitario"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeNotaES = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeNotaES(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeNotaES = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDeImpresionDelReporteDeNotaES", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fNombreDelReporteDeNotaES(ByVal valReporte As eNotaESRpt) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_RPT_NOTA_DE_ENTRADA_SALIDA: fNombreDelReporteDeNotaES = "Nota E/S"
      Case CM_RPT_NOTA_DE_ENTRADA_SALIDA_CON_PRECIO: fNombreDelReporteDeNotaES = "Nota E/S"
      Case Else: fNombreDelReporteDeNotaES = ""
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fNombreDelReporteDeNotaES", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteNotaES(ByRef refRpt As DDActiveReports2.ActiveReport, _
                                                  ByVal valSqlDelReporte As String, _
                                                   ByVal valFecha As String, _
                                                    ByVal valcodigocliente As String, _
                                                     ByVal valNombreCliente As String, _
                                                      ByVal valCodigoAlmacen As String, _
                                                       ByVal valNombreAlmacen As String, _
                                                        ByVal valNumeroNE As String, _
                                                         ByVal valTipoDeOperacion As String, _
                                                          ByVal valComentarios As String, _
                                                           ByVal valNombreCampoRollo As String, _
                                                            ByVal valNombreCampoSerial As String, _
                                                             ByVal valUsaSerial As Boolean, _
                                                              ByVal valUsaRollo As Boolean, _
                                                               ByVal valNombreCompaniaParaInformes As String, _
                                                                ByVal valMostrarPrecio As Boolean, _
                                                                 ByVal valPrecioSinIva As Boolean, _
                                                                  ByVal valUsaListaPreciosME As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim formatoNDecimales As String
   On Error GoTo h_ERROR
   formatoNDecimales = gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(gConvert.ConvierteAInteger(gProyParametrosCompania.GetCantidadDeDecimalesStr))
   exito = False
   If valMostrarPrecio Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeNotaES(CM_RPT_NOTA_DE_ENTRADA_SALIDA_CON_PRECIO)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeNotaES(CM_RPT_NOTA_DE_ENTRADA_SALIDA)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "DcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaYHoraDeEmision", "Emitido el  " & gConvert.dateToString(gUtilDate.getFechaDeHoy) & " a las " & gUtilDate.getHoraActualString
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", _
                        "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", valFecha, "", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", valNumeroNE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoDeOperacion", valTipoDeOperacion
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCliente", valcodigocliente
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", valNombreCliente
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
         If valMostrarPrecio Then
            If valPrecioSinIva Then
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioUnitario", "", "PrecioSinIva", False, ddTXRight, formatoNDecimales
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioUnitarioME", "", "PrecioSinIvaME", False, ddTXRight, formatoNDecimales
            Else
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioUnitario", "", "PrecioConIva", False, ddTXRight, formatoNDecimales
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPrecioUnitarioME", "", "PrecioConIvaME", False, ddTXRight, formatoNDecimales
            End If
            If valUsaListaPreciosME Then
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioABolivares", False, ddTXRight, gUtilReports.getDefaultNumericFormatWithNDecimalPlaces(4)
            Else
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtPrecioUnitarioME", False
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblPrecioUnitarioME", ""
               gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", ""
            End If
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
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteNotaES", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


