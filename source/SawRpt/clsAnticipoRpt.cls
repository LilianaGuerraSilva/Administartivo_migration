VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAnticipoRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsAnticipoRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reportes Anticipo"

Private Enum eAnticipoRpt
   eARpt_ANTICIPOS_COBRAD0S_POR_CLIENTE = 0
   eARpt_ANTICIPOS_COBRADOS_ENTRE_FECHAS
   eARpt_ANTICIPOS_PAGADOS_POR_PROVEEDOR
   eARpt_ANTICIPOS_PAGADOS_ENTRE_FECHAS
   eARpt_ANTICIPOS_COBRADOS_ASOCIADOS_CON_COTIZACION
   eARpt_ANTICIPOS_COBRADOS_A_UNA_FECHA
   eARpt_ANTICIPOS_PAGADOS_A_UNA_FECHA
   eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO
   eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDelRptDeAnticiposCobrados(ByVal valReporte As eAnticipoRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eARpt_ANTICIPOS_COBRAD0S_POR_CLIENTE: nombreFileXML = "rpxAnticipoCobradosXCliente"
      Case eARpt_ANTICIPOS_COBRADOS_ENTRE_FECHAS: nombreFileXML = "rpxAnticipoCobradosEntreFecha"
      Case eARpt_ANTICIPOS_PAGADOS_POR_PROVEEDOR: nombreFileXML = "rpxAnticipoPagadosXProveedor"
      Case eARpt_ANTICIPOS_PAGADOS_ENTRE_FECHAS: nombreFileXML = "rpxAnticipoPagadosEntreFecha"
      Case eARpt_ANTICIPOS_COBRADOS_ASOCIADOS_CON_COTIZACION: nombreFileXML = "rpxAnticipCobradosAsociadosConCotizacion"
      Case eARpt_ANTICIPOS_COBRADOS_A_UNA_FECHA: nombreFileXML = "rpxAnticiposAUnaFecha"
      Case eARpt_ANTICIPOS_PAGADOS_A_UNA_FECHA: nombreFileXML = "rpxAnticiposAUnaFecha"
      Case eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO: nombreFileXML = "rpxComprobanteDeAnticipoCobrado"
      Case eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO: nombreFileXML = "rpxComprobanteDeAnticipoPagado"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDelRptDeAnticiposCobrados = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeAnticipos(valReporte))
   Else
      fNombreFileDelRptDeAnticiposCobrados = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDelRptDeAnticiposCobrados", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeAnticipos(ByVal valReporte As eAnticipoRpt) As String
   Select Case valReporte
      Case eARpt_ANTICIPOS_COBRAD0S_POR_CLIENTE: fNombreDelReporteDeAnticipos = "Anticipos Cobrados por Clientes"
      Case eARpt_ANTICIPOS_COBRADOS_ENTRE_FECHAS: fNombreDelReporteDeAnticipos = "Anticipos Cobrados entre Fechas"
      Case eARpt_ANTICIPOS_PAGADOS_POR_PROVEEDOR: fNombreDelReporteDeAnticipos = "Anticipos Pagados por Proveedor"
      Case eARpt_ANTICIPOS_PAGADOS_ENTRE_FECHAS: fNombreDelReporteDeAnticipos = "Anticipos Pagados entre Fecha"
      Case eARpt_ANTICIPOS_COBRADOS_ASOCIADOS_CON_COTIZACION: fNombreDelReporteDeAnticipos = "Anticipos Cobrados Asociados con Cotización"
      Case eARpt_ANTICIPOS_COBRADOS_A_UNA_FECHA: fNombreDelReporteDeAnticipos = "Anticipos Cobrados A Una Fecha"
      Case eARpt_ANTICIPOS_PAGADOS_A_UNA_FECHA: fNombreDelReporteDeAnticipos = "Anticipos Pagados A Una Fecha"
      Case eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO: fNombreDelReporteDeAnticipos = "Comprobante de Anticipo Cobrado"
      Case eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO: fNombreDelReporteDeAnticipos = "Comprobante de Anticipo Pagado"
      Case Else: fNombreDelReporteDeAnticipos = ""
   End Select
End Function

Public Function fConfigurarDatosDeAntiposCobradosPagadosXClienteProveedor(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlReporte As String, ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, _
                ByVal valNombreCompaniaParaInformes As String, ByVal valOrdenadorPor As Boolean) As Boolean
   Dim nombreDelFileXML As String
   Dim tituloDelReporte As String
   Dim exito As Boolean
   Dim valNombreCliePro As String
   On Error GoTo h_ERROR
   If valTipoDeAnticipo = eTDA_COBRADO Then
      nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_ANTICIPOS_COBRAD0S_POR_CLIENTE)
      tituloDelReporte = fNombreDelReporteDeAnticipos(eARpt_ANTICIPOS_COBRAD0S_POR_CLIENTE)
      valNombreCliePro = "Cliente"
   Else
      nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_ANTICIPOS_PAGADOS_POR_PROVEEDOR)
      tituloDelReporte = fNombreDelReporteDeAnticipos(eARpt_ANTICIPOS_PAGADOS_POR_PROVEEDOR)
      valNombreCliePro = "Proveedor"
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
    If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
           "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         
         If valOrdenadorPor Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusNombre", "", ""
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCliente", valNombreCliePro
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblEstatus", "Estatus"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalStatus", "Total por Estatus"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalCliente", "Total por " & valNombreCliePro
         Else
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Codigo"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatusNombre", "", "Nombre"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Status"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", ""
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCliente", "Estatus"
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblEstatus", valNombreCliePro
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalStatus", "Total por " & valNombreCliePro
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTotalCliente", "Total por Estatus"
         End If
         
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCheque", "", "NumeroCheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAnulado", "", "MontoAnulado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevuelto", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferenciaDevolucion", "", "MontoDiferenciaEnDevolucion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoAnulado", "", "MontoAnulado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoDev", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDiferenciaDevol", "", "MontoDiferenciaEnDevolucion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAnuladoX", "", "MontoAnulado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalX", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsadoX", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevX", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDifX", "", "MontoDiferenciaEnDevolucion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAnuladoXMoneda", "", "MontoAnulado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalXMoneda", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoUsadoXMoneda", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMtoDevtoXMoneda", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotaldifDevolucionXMoneda", "", "MontoDiferenciaEnDevolucion"

         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
    End If
  End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDeAntiposCobradosPagadosXClienteProveedor = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDeAntiposCobradosPagadosXClienteProveedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDeAntiposCobradosPagadosEntreFechas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim exito As Boolean
   Dim tituloDelReporte As String
   On Error GoTo h_ERROR
    If valTipoDeAnticipo = eTDA_COBRADO Then
      nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_ANTICIPOS_COBRADOS_ENTRE_FECHAS)
      tituloDelReporte = fNombreDelReporteDeAnticipos(eARpt_ANTICIPOS_COBRADOS_ENTRE_FECHAS)
   Else
      nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_ANTICIPOS_PAGADOS_ENTRE_FECHAS)
      tituloDelReporte = fNombreDelReporteDeAnticipos(eARpt_ANTICIPOS_PAGADOS_ENTRE_FECHAS)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
    If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del  " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
           "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCheque", "", "NumeroCheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevuelto", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferenciaDevolucion", "", "MontoDiferenciaEnDevolucion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoDev", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDiferenciaDevol", "", "MontoDiferenciaEnDevolucion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalXMoneda", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoUsadoXMoneda", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMtoDevtoXMoneda", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotaldifDevolucionXMoneda", "", "MontoDiferenciaEnDevolucion"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
    End If
  End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosDeAntiposCobradosPagadosEntreFechas = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDeAntiposCobradosPagadosEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDeAntiposCobradosAsociadosConCotizacion(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlReporte As String, ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFileXML As String
   Dim tituloDelReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   If valTipoDeAnticipo = eTDA_COBRADO Then
      nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_ANTICIPOS_COBRADOS_ASOCIADOS_CON_COTIZACION)
      tituloDelReporte = fNombreDelReporteDeAnticipos(eARpt_ANTICIPOS_COBRADOS_ASOCIADOS_CON_COTIZACION)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
    If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", ""
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
           "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
          gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCotizacion", "", "NumeroCotizacion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTotalCotizacion", "", "TotalCotizacion"
        
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "numero"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevuelto", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferenciaDevolucion", "", "MontoDiferenciaEnDevolucion"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMonto", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoDev", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDiferenciaDevol", "", "MontoDiferenciaEnDevolucion"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalX", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsadoX", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevX", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDifX", "", "MontoDiferenciaEnDevolucion"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalXMoneda", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMontoUsadoXMoneda", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalMtoDevtoXMoneda", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotaldifDevolucionXMoneda", "", "MontoDiferenciaEnDevolucion"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCliente", "NumeroCotizacion", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone

         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
    End If
  End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDeAntiposCobradosAsociadosConCotizacion = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDeAntiposCobradosAsociadosConCotizacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDeAnticipoAUnaFecha(ByRef refRpt As DDActiveReports2.ActiveReport, _
                ByVal valSQL As String, ByVal valTipoDeAnticipo As enum_TipoDeAnticipo, _
                 ByVal valNombreCompaniaParaInformes As String, ByVal valFechaFinal As Date) As Boolean
   Dim nombreDelFileXML As String
   Dim tituloDelReporte As String
   Dim exito As Boolean
   
   On Error GoTo h_ERROR
   exito = False
   If valTipoDeAnticipo = eTDA_COBRADO Then
      nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_ANTICIPOS_COBRADOS_A_UNA_FECHA)
      tituloDelReporte = fNombreDelReporteDeAnticipos(eARpt_ANTICIPOS_COBRADOS_A_UNA_FECHA)
   Else
      nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_ANTICIPOS_PAGADOS_A_UNA_FECHA)
      tituloDelReporte = fNombreDelReporteDeAnticipos(eARpt_ANTICIPOS_PAGADOS_A_UNA_FECHA)
   End If
   
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
    If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", " Hasta " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", tituloDelReporte
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
           "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision

         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Nombre"
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtestatus", "", "Status"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "FechaAnticipo", eFT_DATE
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAnulado", "", "MontoAnulado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevuelto", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferenciaDevolucion", "", "MontoDiferenciaEnDevolucion"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAnuladoSecCli", "", "MontoAnulado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalsecCliente", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsadosecCliente", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevueltosecCliente", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferenciaDevolucionsecCliente", "", "MontoDiferenciaEnDevolucion"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAnuladoSecMon", "", "MontoAnulado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalsecMoneda", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsadosecMoneda", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevueltosecMoneda", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDiferenciaDevolucionsecMoneda", "", "MontoDiferenciaEnDevolucion"
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHsecMoneda", "CodigoMoneda", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHsecCliente", "Nombre", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHsecNumero", "Numero", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
    End If
  End If
h_EXIT:
   On Error GoTo 0
   fConfigurarDatosDeAnticipoAUnaFecha = exito
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDeAnticipoAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfigurarDatosDelComprobanteDeAnticipo(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valCodigoCompania As String, _
                ByVal valAnticipoCobrado As Boolean, ByVal valNombreCompaniaParaInformes As String, _
                ByVal valSqlDelReporte As String, ByVal valMontoAnticipo As String, ByVal valFecha As Date, _
                ByVal valComprobanteConCheque As Boolean, ByVal valComprobanteSeparadoCheque As Boolean, _
                ByVal valMontoTotal As Currency, ByVal valMoneda As String, ByVal valCuentaBancaria As String, _
                ByVal valNombreProveedor As String, ByVal valCiudad As String, ByVal valPlantillaCheque As String, _
                ByVal valUsuarioActual As String, ByVal valUsuarioCargo As String, ByVal valComprobanteAImprimir As Boolean, _
                ByVal valPlantillaComprobanteAnticipo As String, ByVal insCuentaBancaria As Object, ByRef insCnxSawAos As Object, ByVal valCampoParaBusquedaCtaBancaria As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim NombreParaMargenes As String
   Dim vFechaCompleta As String
   On Error GoTo h_ERROR
   exito = False
   vFechaCompleta = gConvert.fConvierteFechaAStringFechaCompleta(valFecha, True)
   NombreParaMargenes = valCodigoCompania & " - "
   If valPlantillaComprobanteAnticipo = "" Then
      If valAnticipoCobrado Then
         nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO)
         NombreParaMargenes = NombreParaMargenes & fNombreDelReporteDeAnticipos(eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO) & " - " & fNombreFileDelRptDeAnticiposCobrados(eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO)
      Else
         nombreDelFileXML = fNombreFileDelRptDeAnticiposCobrados(eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO)
         NombreParaMargenes = NombreParaMargenes & fNombreDelReporteDeAnticipos(eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO) & " - " & fNombreFileDelRptDeAnticiposCobrados(eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO)
      End If
   Else
      If valAnticipoCobrado Then
         nombreDelFileXML = gUtilReports.getCompletePathDelFileDeImpresionRpx(valPlantillaComprobanteAnticipo, True, fNombreDelReporteDeAnticipos(eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO))
         NombreParaMargenes = NombreParaMargenes & fNombreDelReporteDeAnticipos(eARpt_COMPROBANTE_DE_ANTICIPO_COBRADO) & " - " & valPlantillaComprobanteAnticipo
      Else
         nombreDelFileXML = gUtilReports.getCompletePathDelFileDeImpresionRpx(valPlantillaComprobanteAnticipo, True, fNombreDelReporteDeAnticipos(eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO))
         NombreParaMargenes = NombreParaMargenes & fNombreDelReporteDeAnticipos(eARpt_COMPROBANTE_DE_ANTICIPO_PAGADO) & " - " & valPlantillaComprobanteAnticipo
      End If
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtConsecutivo", "", "ConsecutivoAnticipo"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtStatus", "", "Status"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCambio", "", "Cambio"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombre", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCuentaBancaria", "", "CodigoCuentaBancaria"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuentaBancaria", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConceptoBancario", "", "CodigoConceptoBancario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionConceptoBancario", "", "DescripcionConceptoBancario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "DescripcionAnticipo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotal", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoUsado", "", "MontoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDevuelto", "", "MontoDevuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoDifDevolucion", "", "MontoDiferenciaEnDevolucion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoRestante", "", "MontoRestante"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCheque", "", "NumeroCheque"
'***********************Informe Especial de Anticipo
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRif", "", "NumeroRIF"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDireccion", "", "Direccion"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTelefono", "", "Telefono"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCantidadEnLetras", gConvert.fNumeroEnLetrasConNombreMoneda(gConvert.fConvertStringToCurrency(valMontoAnticipo), valMoneda), ""
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoAnticipo", "", "MontoTotal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCliente", "", "Nombre"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtUsuario", valUsuarioActual, ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCargo", valUsuarioCargo, ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFechaCompleta", vFechaCompleta, ""
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBanco", "", "Banco"
'****************************************************
         If valComprobanteConCheque Or valComprobanteSeparadoCheque Then
            If gUtilReports.fAsignaSubReporteSiExite(refRpt, GetSubReportCheque(valMontoTotal, valMoneda, valCuentaBancaria, valNombreProveedor, valFecha, valCiudad, valPlantillaCheque, insCuentaBancaria, insCnxSawAos, valCampoParaBusquedaCtaBancaria), "srptCheque") Then
               If valComprobanteAImprimir Then
                  gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCheque", "", ddGrpAll, ddRepeatNone, True, ddNPAfter
               End If
            End If
         End If
         gUtilMargins.sAsignarMargenesParticularesYLosGrabaSiEsNecesario refRpt, NombreParaMargenes, True, 0.25, True, 0.25, True, 0.25, True, 0.25
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
   fConfigurarDatosDelComprobanteDeAnticipo = exito
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConfigurarDatosDelComprobanteDeAnticpo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function GetSubReportCheque(ByVal valMontoTotal As Currency, ByVal valMoneda As String, _
                                 ByVal valCuentaBancaria As String, ByVal valNombreProveedor As String, ByVal valFecha As Date, _
                                 ByVal valCiudad As String, ByVal valPlantillaCheque As String, ByVal insCuentaBancaria As Object, ByRef insCnxSawAos As Object, ByVal valCampoParaBusquedaCtaBancaria As String) As DDActiveReports2.ActiveReport
   Dim rptSubReport As DDActiveReports2.ActiveReport
   Dim montoEnLetras As String
   Dim montoEnLetrasLinea1 As String
   Dim montoEnLetrasLinea2 As String
   Dim puntoDeCorte As Long
   Dim insNumeroEnLetras As clsNumeroEnLetras
   On Error GoTo h_ERROR
   Set rptSubReport = New DDActiveReports2.ActiveReport
   Set insNumeroEnLetras = New clsNumeroEnLetras
   
   insNumeroEnLetras.CreaElNumeroEnLetras gConvert.fConvierteACurrency(valMontoTotal), montoEnLetras, False, valMoneda
  
   montoEnLetrasLinea1 = montoEnLetras
   montoEnLetrasLinea2 = ""
   If gTexto.DfLen(montoEnLetrasLinea1) > 85 Then
      puntoDeCorte = gTexto.fBuscaDondeCortar(montoEnLetrasLinea1, 80)
      montoEnLetrasLinea1 = gTexto.DfLeft(montoEnLetrasLinea1, puntoDeCorte)
      montoEnLetrasLinea2 = gTexto.DfMid(montoEnLetras, puntoDeCorte + 1)
   End If
   If montoEnLetrasLinea2 = "" Then
      montoEnLetrasLinea1 = montoEnLetrasLinea1 & " ***"
   Else
      montoEnLetrasLinea2 = montoEnLetrasLinea2 & " ***"
   End If
   If gUtilReports.fLoadLayout(rptSubReport, fNombrePlantillaCheque(valCuentaBancaria, valPlantillaCheque, insCuentaBancaria, valCampoParaBusquedaCtaBancaria, insCnxSawAos)) Then
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblMontoCheque", "***" & gConvert.fNumToStringConSeparadorDeMiles(valMontoTotal) & "***"
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblBeneficiario", "*** " & valNombreProveedor & " ***"
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblMontoEnLetrasLinea1", montoEnLetrasLinea1
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblMontoEnLetrasLinea2", montoEnLetrasLinea2
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblFechaDeEmision", valCiudad & ", " & gConvert.fConvierteFechaAStringFechaCompleta(valFecha, False)
      gUtilReports.sDefaultConfigurationForLabels rptSubReport, "lblAno", gTexto.DfCStr(Year(valFecha))
   End If
   Set insNumeroEnLetras = Nothing
   Set GetSubReportCheque = rptSubReport
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetSubReportCheque", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombrePlantillaCheque(ByVal valCodigoCuentaBancaria As String, ByVal valPlantillaCheque As String, ByVal insCuentaBancaria As Object, ByVal valCampoParaBusquedaCtaBancaria As String, ByRef insCnxSawAos As Object) As String
   Dim nombrePlantilla As String
   On Error GoTo h_ERROR
   nombrePlantilla = "rpxChequeGenerico"
   If valCodigoCuentaBancaria <> "" Then
      If insCnxSawAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, valCodigoCuentaBancaria, valCampoParaBusquedaCtaBancaria, "", "", "", False, False, False, True) Then
         nombrePlantilla = valPlantillaCheque
      End If
   End If
   fNombrePlantillaCheque = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombrePlantilla, True, "Cheque")
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
     "fNombrePlantillaCheque", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

