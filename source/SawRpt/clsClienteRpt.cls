VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClienteRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME = "clsClienteRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte Cliente"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporte(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case 0: nombreFileXML = "rpxFichaDelCliente"
      Case 1: nombreFileXML = "rpxEstadoDeCuentaCliente"
      Case 3: nombreFileXML = "rpxClienteXZona"
      Case 4: nombreFileXML = "rpxClienteXCiudad"
      Case 6: nombreFileXML = "rpxListadoDeClientes"
      Case 9: nombreFileXML = "rpxListadoDeProveedoresSinMov"
      Case 10: nombreFileXML = "rpxListadoDeClientesProvSinMov"
      Case 11: nombreFileXML = "rpxListadoDeCumpleanosClientes"
      Case 12: nombreFileXML = "rpxListadoDeClientesConCorrespondenciaXEnviar"
      Case 13: nombreFileXML = "rpxClienteXSectorDeNegocio"
      Case 14: nombreFileXML = "rpxEstadoDeCuentaClienteConNroComprobanteFiscal"
      Case 15: nombreFileXML = "rpxEstadoDeCuentaClienteConCamposDef"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporte(valReporte))
   Else
      fNombreFileDeImpresionDelReporte = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fNombreFileDeImpresionDelReporte = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporte(ByVal valReporte As Integer) As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case 0: fNombreDelReporte = "Ficha Del Cliente"
      Case 1: fNombreDelReporte = "Estado de Cuente de Cliente"
      Case 3: fNombreDelReporte = "Reporte Cliente por Zona"
      Case 4: fNombreDelReporte = "Reporte Cliente por Ciudad"
      Case 6: fNombreDelReporte = "Listado De Clientes"
      Case 9: fNombreDelReporte = "Listado De Proveedores Sin Movimientos"
      Case 10: fNombreDelReporte = "Listado De Clientes Sin Movimientos"
      Case 11: fNombreDelReporte = "Listado de Cumpleaños de Clientes"
      Case 12: fNombreDelReporte = "Listado de Clientes por Enviar Correspondencia"
      Case 13: fNombreDelReporte = "Clientes por sector de Negocio"
      Case 14: fNombreDelReporte = "Estado de Cuente de Cliente con Numero Comprobante Fiscal"
      Case 15: fNombreDelReporte = "Estado de Cuente de Cliente"
      Case Else: fNombreDelReporte = ""
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fNombreDelReporte = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByRef refReporteActual As DDActiveReports2.ActiveReport, valReporte As Integer, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varNombreDelFile As String
   On Error GoTo h_ERROR
   fConfigurarDatosDelReporte = False
   varNombreDelFile = fNombreFileDeImpresionDelReporte(valReporte)
   If varNombreDelFile <> "" Then
      If gUtilReports.fLoadLayout(refReporteActual, varNombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refReporteActual, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
            gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refReporteActual, "lblFechaYHoraDeEmision"
            gUtilReports.sConfiguraEncabezado refReporteActual, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
               "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtRif", "", "NumeroDeRif"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtZonaCobranza", "", "ZonasCobranzas"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtFax", "", "Fax"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtContacto", "", "Contacto"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtTelefono", "", "Telefono"
            If valReporte = 3 Then 'clientes x zona
               gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtCiudad", "", "Direccion"
            Else
               gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtCiudad", "", "Ciudades"
            End If
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtNombreCliente", "", "NombreCliente"
            gUtilMargins.sAsignarMargenesGenerales refReporteActual
         End If
      End If
   End If
   fConfigurarDatosDelReporte = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
 "fConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteListadoClientes(ByVal valSqlDelReporte As String, ByRef refReporteActual As DDActiveReports2.ActiveReport, ByVal valNombreCompaniaParaInformes As String, Optional ByVal valIncluirEmail As Boolean) As Boolean
   Dim varNombreDelFile As String
   On Error GoTo h_ERROR
   fConfigurarDatosDelReporteListadoClientes = False
   varNombreDelFile = fNombreFileDeImpresionDelReporte(6)
   If varNombreDelFile <> "" Then
      If gUtilReports.fLoadLayout(refReporteActual, varNombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refReporteActual, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
            gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refReporteActual, "lblFechaYHoraDeEmision"
            gUtilReports.sConfiguraEncabezado refReporteActual, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtZonaCobranza", "", "ZonasCobranzas"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtFax", "", "Fax"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtContacto", "", "Contacto"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtTelefono", "", "Telefono"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtNombreCliente", "", "NombreCliente"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtRif", "", "NumeroRif"
            If valIncluirEmail Then
               gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtEmail", "", "Email"
            Else
               gUtilReports.sConfigurationForNotvisibleOrVisible refReporteActual, "txtEmail", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refReporteActual, "lblEmail", False
               gUtilReports.sChangePositionAndSize refReporteActual, "lblZonaCobranza", 1495, 540, 8820, 225
               gUtilReports.sChangePositionAndSize refReporteActual, "txtZonaCobranza", 7495, 0, 8820, 225
            End If
            If valIncluirEmail Then
               gUtilMargins.sAsignarMargenesGenerales refReporteActual, ddOLandscape, fNombreDelReporte(6)
            Else
               gUtilMargins.sAsignarMargenesGenerales refReporteActual
            End If
         End If
      End If
   End If
   fConfigurarDatosDelReporteListadoClientes = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteListadoClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteFichaClientes(ByVal valSqlDelReporte As String, ByRef refReporte As DDActiveReports2.ActiveReport, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim varNombreDelFile As String
   On Error GoTo h_ERROR
   fConfigurarDatosDelReporteFichaClientes = False
   varNombreDelFile = fNombreFileDeImpresionDelReporte(0)
   If LenB(varNombreDelFile) > 0 Then
      If gUtilReports.fLoadLayout(refReporte, varNombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refReporte, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sConfiguraEncabezado refReporte, "lblNombreDeLaCompania", "", "lblNombreDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, False
            gUtilReports.sDefaultConfigurationForLabels refReporte, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
            gUtilReports.sDefaultConfigurationForLabels refReporte, "lblNumeroDERIF", "Número de " & gGlobalization.fPromptRIF & " :"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNombre", "", "NombreCliente"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtCodigo", "", "Codigo"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtStatus", "", "Status"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtContacto", "", "Contacto"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtDireccion", "", "Direccion"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtTelefono", "", "Telefono"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtFAX", "", "Fax"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtEmail", "", "Email"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtCiudad", "", "Ciudad"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtZonaPostal", "", "ZonaPostal"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtTipoPersona", "", "TipoPersona"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroRIF", "", "NumeroRIF"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNumeroNIT", "", "NumeroNIT"
            gUtilReports.sDefaultConfigurationForStrFields refReporte, "txtNombreVendedor", "", "NombreVendedor"
            gUtilMargins.sAsignarMargenesGenerales refReporte
            fConfigurarDatosDelReporteFichaClientes = True
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fConfigurarDatosDelReporteFichaClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteListadoDeClientesProvSinMov(ByVal valSQL As String, ByVal valModulo As enum_SystemModules, ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valFNCodigo As String, _
                        ByVal valFNNombre As String, ByVal valFNCiudad As String, ByVal valFNContacto As String, _
                        ByVal valFNFax As String, ByVal valFNTelefono As String, ByVal valNombreCompaniaParaInformes As String, ByVal valConexion As Connection) As Boolean
   Dim varResult As ADODB.Recordset
   Dim varNombreDelFile As String
   Dim gDbUtil As clsDBUtil
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gTexto As clsTexto
   On Error GoTo h_ERROR
   fConfigurarDatosDelReporteListadoDeClientesProvSinMov = False
   Set varResult = New ADODB.Recordset
   Set gDbUtil = New clsDBUtil
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gTexto = New clsTexto
   If valModulo = eSM_Cliente Then
      varNombreDelFile = fNombreFileDeImpresionDelReporte(10)
   Else
      varNombreDelFile = fNombreFileDeImpresionDelReporte(9)
   End If
   If gDbUtil.fOpenRecordSetAllParameters(varResult, valSQL, valConexion, adLockReadOnly, adUseClient, adOpenForwardOnly) Then
   End If
   sFiltraElRecordset varResult, valModulo
   If varResult.RecordCount > 0 Then
      If gUtilReports.fLoadLayout(refRpt, varNombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
            gUtilReports.sConfiguraEncabezado refRpt, "lblNombreDeLaCompania", "lblFechaDeEmisionDelReporte", "lblNombreDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, True
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreCompania", valNombreCompaniaParaInformes
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNombreDelReporte", "Listado de " & gTexto.fPlural(gEnumProyecto.enum_SystemModulesToStr(valModulo)) & " Sin Movimientos", True, ddTXCenter
            gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaDeEmisionDelReporte"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", valFNCodigo
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", valFNNombre
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFax", "", valFNFax
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", valFNContacto
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", valFNTelefono
            If valModulo = eSM_Cliente Then
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCiudad", "", valFNCiudad
            Else
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCiudad", False
               gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCiudad", False
            End If
            gUtilMargins.sAsignarMargenesGenerales refRpt
            Call refRpt.AddNamedItem("varResult", varResult)
            fConfigurarDatosDelReporteListadoDeClientesProvSinMov = True
         End If
      End If
   End If
   Set gDbUtil = Nothing
   Set gEnumProyecto = Nothing
   Set varResult = Nothing
   Set gTexto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConfigurarDatos", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sFiltraElRecordset(ByRef refRecordset As ADODB.Recordset, ByVal valModulo As enum_SystemModules)
   On Error GoTo h_ERROR
   If valModulo = eSM_Cliente Then
      If refRecordset.RecordCount > 0 Then
         refRecordset.Filter = "T1 = '0' AND T2 = '0' AND T3 = '0' AND T4 = '0' AND T5 = '0' AND T6 = '0' AND T7 = '0' AND T8 = '0'"
      End If
   ElseIf valModulo = eSM_Proveedor Then
      If refRecordset.RecordCount > 0 Then
         If GetEsSistemaInfotax Then
            refRecordset.Filter = "T2 = '0' AND T3 = '0' AND T4 = '0' AND T5 = '0'"
         Else
            refRecordset.Filter = "T1 = '0' AND T2 = '0' AND T3 = '0' AND T4 = '0' AND T5 = '0'"
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sFiltraElRecordset", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Function fConfigurarReporteEstadoDeCuenta(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, ByVal ReporteEnMonedaLocal As Boolean, ByVal valUsarCambioOriginal As Boolean, ByVal valUnaPaginaPorCliente As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeNotaDeCambio As String, ByVal gGlobalization As Object, ByVal ValMontoOriginal As Boolean, ByVal valImprimeNroComprobanteFiscal As Boolean, ByVal valImprimeCamposDef As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim notaDeCambio As String
   Dim insUtDDScript As clsUtilDDScript
   On Error GoTo h_ERROR
   exito = False
   If valImprimeNroComprobanteFiscal And Not valImprimeCamposDef Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(14)
   ElseIf valImprimeCamposDef Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(15)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(1)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCiudad", "", "Ciudad"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoOriginal", "", "MontoOriginal"
         If valImprimeNroComprobanteFiscal Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         End If
         If ReporteEnMonedaLocal Then
            If valUsarCambioOriginal Then
               notaDeCambio = valMensajesDeNotaDeCambio
               gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "CambioBs"
            Else
               notaDeCambio = valMensajesDeNotaDeCambio
               gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambio", "", "CambioBs"
            End If
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", "Cambio BsF"
         Else
            notaDeCambio = ""
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCambio", "", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtCambio", False
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCambio", ""
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblNotaDeCambio", False
         End If
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", notaDeCambio
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipoReporte", "", "TipoReporte"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTituloTipoReporte", "", "TituloTipoReporte"
         If gGlobalization.fEsCodigoPeru Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoDocumentoLey"
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoDocumento"
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaVencimiento", "", "FechaVencimiento", eFT_DATE
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtDiasVencidos", "", "DiasVencidos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoVencido", "", "MontoVencido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoNoVencido", "", "MontoNoVencido"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoVencido", "", ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoNoVencido", "", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef1", gProyParametrosCompania.GetNombreCampoDefinible1, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef2", gProyParametrosCompania.GetNombreCampoDefinible2, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef3", gProyParametrosCompania.GetNombreCampoDefinible3, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef4", gProyParametrosCompania.GetNombreCampoDefinible4, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef5", gProyParametrosCompania.GetNombreCampoDefinible5, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef6", gProyParametrosCompania.GetNombreCampoDefinible6, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef7", gProyParametrosCompania.GetNombreCampoDefinible7, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef8", gProyParametrosCompania.GetNombreCampoDefinible8, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef9", gProyParametrosCompania.GetNombreCampoDefinible9, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef10", gProyParametrosCompania.GetNombreCampoDefinible10, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef11", gProyParametrosCompania.GetNombreCampoDefinible11, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCampDef12", gProyParametrosCompania.GetNombreCampoDefinible12, False, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef1", "", "CampoDefinible1", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef2", "", "CampoDefinible2", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef3", "", "CampoDefinible3", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef4", "", "CampoDefinible4", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef5", "", "CampoDefinible5", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef6", "", "CampoDefinible6", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef7", "", "CampoDefinible7", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef8", "", "CampoDefinible8", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef9", "", "CampoDefinible9", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef10", "", "CampoDefinible10", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef11", "", "CampoDefinible11", True, ddTXLeft
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCampDef12", "", "CampoDefinible12", True, ddTXLeft
         If valUnaPaginaPorCliente Then
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GFCliente", "", ddGrpAll, ddRepeatOnPage, True, ddNPAfter
         End If
         If valImprimeNroComprobanteFiscal Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporte(14)
         Else
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape, fNombreDelReporte(1)
         End If
         
         If Not (ValMontoOriginal) Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoOriginal", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblMontoOriginal", False
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait
         End If
         If valImprimeCamposDef Then
            gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         End If
         Set insUtDDScript = New clsUtilDDScript
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         Call refRpt.AddNamedItem("insUtDDScript", insUtDDScript)
         Set insUtDDScript = Nothing
         exito = True
      End If
   End If
   fConfigurarReporteEstadoDeCuenta = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarReporteEstadoDeCuenta", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteCumpleanosClientes(ByVal valSqlDelReporte As String, ByRef refReporteActual As DDActiveReports2.ActiveReport, ByVal valMes As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varNombreDelFile As String
   On Error GoTo h_ERROR
   fConfigurarDatosDelReporteCumpleanosClientes = False
   varNombreDelFile = fNombreFileDeImpresionDelReporte(11)
   If varNombreDelFile <> "" Then
      If gUtilReports.fLoadLayout(refReporteActual, varNombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refReporteActual, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
            gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refReporteActual, "lblFechaYHoraDeEmision"
            gUtilReports.sConfiguraEncabezado refReporteActual, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblTituloDelReporte", fNombreDelReporte(11) & " - " & valMes
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtNombreCliente", "", "NombreCliente"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtTelefono", "", "Telefono"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtDiaCumpleano", "", "DiaCumpleano"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtCorreo", "", "Correo"
            gUtilMargins.sAsignarMargenesGenerales refReporteActual
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteCumpleanosClientes = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteCumpleanosClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteClientesPorEnviarCorrespondencia(ByVal valSqlDelReporte As String, ByRef refReporteActual As DDActiveReports2.ActiveReport, ByVal valMes As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim varNombreDelFile As String
   On Error GoTo h_ERROR
   fConfigurarDatosDelReporteClientesPorEnviarCorrespondencia = False
   varNombreDelFile = fNombreFileDeImpresionDelReporte(12)
   If varNombreDelFile <> "" Then
      If gUtilReports.fLoadLayout(refReporteActual, varNombreDelFile) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refReporteActual, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
            gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refReporteActual, "lblFechaYHoraDeEmision"
            gUtilReports.sConfiguraEncabezado refReporteActual, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
            gUtilReports.sDefaultConfigurationForLabels refReporteActual, "lblTituloDelReporte", fNombreDelReporte(12)
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtNombreCliente", "", "NombreCliente"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtTelefono", "", "Telefono"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtDireccion", "", "Direccion"
            gUtilReports.sDefaultConfigurationForStrFields refReporteActual, "txtCorreo", "", "Correo"
            gUtilMargins.sAsignarMargenesGenerales refReporteActual
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDelReporteClientesPorEnviarCorrespondencia = True
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteClientesPorEnviarCorrespondencia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelReporteClienteXSectorDeNegocio(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporte(13)
   If nombreDelFileXML <> "" Then
      If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
         If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "LblNumeroDePagina", "", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroRif", "", "NumeroRif"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtContacto", "", "Contacto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", "Telefono"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFAX", "", "FAX"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtSectorDeNegocio", "", "SectorDeNegocio"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHSecSectorDeNegocio", "SectorDeNegocio", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOPortrait, fNombreDelReporte(13)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
         End If
      End If
   End If
   fConfigurarDatosDelReporteClienteXSectorDeNegocio = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConfigurarDatosDelReporteClienteXSectorDeNegocio = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosDelReporteClienteXSectorDeNegocio", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
