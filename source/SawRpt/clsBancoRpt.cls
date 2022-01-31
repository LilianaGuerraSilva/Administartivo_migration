VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBancoRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsBancoRpt"
Private Const CM_MESSAGE_NAME As String = "Configurar Reporte Banco"

Private Enum eBancoRpt
   eBcoRpt_SALDOS_BANCARIOS = 0
   eBcoRpt_TRANSACCIONES_POR_CUENTA_BANCARIA
   eBcoRpt_TRANSACCIONES_POR_CONCEPTO
   eBcoRpt_TRANSACCIONES_POR_DIA
   eBcoRpt_TRANSACCIONES_POR_BENEFICIARIO
   eBcoRpt_TRANSACCIONES_AGRUPADAS_POR_BENEFICIARIO
   eBcoRpt_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS
   ebcoRpt_LIBRO_BANCOS_DETALLE_MOVIMIENTOS_CUENTA_BANCARIA
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreReporteParaMensajes(ByVal valIndexDelReporte As eBancoRpt) As String
   Dim varNombreReporte As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eBcoRpt_SALDOS_BANCARIOS: varNombreReporte = "Saldos Bancarios"
      Case eBcoRpt_TRANSACCIONES_POR_CUENTA_BANCARIA: varNombreReporte = "Transacciones por Cuenta Bancaria"
      Case eBcoRpt_TRANSACCIONES_POR_CONCEPTO: varNombreReporte = "Transacciones por Concepto"
      Case eBcoRpt_TRANSACCIONES_POR_DIA: varNombreReporte = "Transacciones por Día"
      Case eBcoRpt_TRANSACCIONES_POR_BENEFICIARIO: varNombreReporte = "Transacciones por Beneficiario"
      Case eBcoRpt_TRANSACCIONES_AGRUPADAS_POR_BENEFICIARIO: varNombreReporte = "Transacciones Agrupadas Por Beneficiario"
      Case eBcoRpt_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS: varNombreReporte = "Declaración del Impuesto a las Transacciones Financieras"
      Case ebcoRpt_LIBRO_BANCOS_DETALLE_MOVIMIENTOS_CUENTA_BANCARIA: varNombreReporte = "Formato 1.1 Libro Caja y Bancos - Detalle de los Movimientos de la Cuenta Corriente"
      Case Else: varNombreReporte = ""
   End Select
h_EXIT: On Error GoTo 0
   fNombreReporteParaMensajes = varNombreReporte
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreReporteParaMensajes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreFileDeImpresionDelReporte(ByVal valIndexDelReporte As eBancoRpt) As String
   Dim varNombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valIndexDelReporte
      Case eBcoRpt_SALDOS_BANCARIOS: varNombreFileXML = "rpxSaldosBancarios"
      Case eBcoRpt_TRANSACCIONES_POR_CUENTA_BANCARIA: varNombreFileXML = "rpxTransaccionesPorCuentaBancaria"
      Case eBcoRpt_TRANSACCIONES_POR_CONCEPTO: varNombreFileXML = "rpxTransaccionesPorConcepto"
      Case eBcoRpt_TRANSACCIONES_POR_DIA: varNombreFileXML = "rpxTransaccionesPorDia"
      Case eBcoRpt_TRANSACCIONES_POR_BENEFICIARIO: varNombreFileXML = "rpxTransaccionesPorBeneficiario"
      Case eBcoRpt_TRANSACCIONES_AGRUPADAS_POR_BENEFICIARIO: varNombreFileXML = "rpxTransaccionesAgrupadasPorBeneficiario"
      Case eBcoRpt_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS: varNombreFileXML = "rpxDeclaracionImpuestoTransaccionesFinancieras"
      Case ebcoRpt_LIBRO_BANCOS_DETALLE_MOVIMIENTOS_CUENTA_BANCARIA: varNombreFileXML = "rpxLibroDeBancosDetalleDeLosMovimientosDeLaCuentaCorriente"
      Case Else: varNombreFileXML = ""
   End Select
   If varNombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(varNombreFileXML, True, fNombreReporteParaMensajes(valIndexDelReporte))
   Else
      fNombreFileDeImpresionDelReporte = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fNombreFileDeImpresionDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelReporteDeSaldosBancarios(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFile = fNombreFileDeImpresionDelReporte(eBcoRpt_SALDOS_BANCARIOS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCuenta", "", "NumeroCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDeLaMoneda", "", "NombreDeLaMoneda"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicial", "", "SaldoInicial"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtIngresos", "", "Ingresos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEgresos", "", "Egresos"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoFinal", ""
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelReporteDeSaldosBancarios = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelReporteDeSaldosBancarios", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelReporteDeTransaccionesPorCuentaBancaria(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String, ByRef gGlobalization As Object) As Boolean
   Dim nombreDelFile As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If gGlobalization.fEsCodigoPeru Then
      nombreDelFile = fNombreFileDeImpresionDelReporte(ebcoRpt_LIBRO_BANCOS_DETALLE_MOVIMIENTOS_CUENTA_BANCARIA)
   Else
      nombreDelFile = fNombreFileDeImpresionDelReporte(eBcoRpt_TRANSACCIONES_POR_CUENTA_BANCARIA)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDeLaMoneda", "", "NombreDeLaMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCuenta", "", "NumeroCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicial", "", "SaldoInicial"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConcepto", "", "Concepto"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtConsecutivo", "", "ConsecutivoMovimiento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConceptoDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDebe", "", "MontoIngreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtHaber", "", "MontoEgreso"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtOperador", "", "NombreOperador"
         If gGlobalization.fEsCodigoPeru Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Período del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRazonSocial", "Sin RazonSocial", "RazonSocial"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtEntidadFinanciera", "", "Banco"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConceptoDescripcion", "", "DescripcionConcepto"
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMedioDePago", "", ""
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelReporteDeTransaccionesPorCuentaBancaria = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelReporteDeTransaccionesPorCuentaBancaria", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelReporteDeTransaccionesPorConcepto(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFile = fNombreFileDeImpresionDelReporte(eBcoRpt_TRANSACCIONES_POR_CONCEPTO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConcepto", "", "CodigoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionConcepto", "", "DescripcionConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "TipoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "NombreDeLaMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "CodigoCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtConsecutivo", "", "ConsecutivoMovimiento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "DescripcionMovimiento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMonto", "", "Monto"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelReporteDeTransaccionesPorConcepto = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelReporteDeTransaccionesPorConcepto", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelReporteDeTransaccionesPorDia(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFecha As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFile = fNombreFileDeImpresionDelReporte(eBcoRpt_TRANSACCIONES_POR_DIA)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFecha", gConvert.dateToString(valFecha)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFecha", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "NombreDeLaMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoCuenta", "", "CodigoCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCuenta", "", "NumeroCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigoConcepto", "", "CodigoConcepto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcionMovimiento", "", "DescripcionMovimiento"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDebe", "", "MontoIngreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtHaber", "", "MontoEgreso"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtOperador", "", "NombreOperador"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelReporteDeTransaccionesPorDia = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelReporteDeTransaccionesPorDia", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelReporteDeTransaccionesPorBeneficiario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valAgruparPorBeneficiario As Boolean, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   If valAgruparPorBeneficiario Then
        nombreDelFile = fNombreFileDeImpresionDelReporte(eBcoRpt_TRANSACCIONES_AGRUPADAS_POR_BENEFICIARIO)
   Else
        nombreDelFile = fNombreFileDeImpresionDelReporte(eBcoRpt_TRANSACCIONES_POR_BENEFICIARIO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDeLaMoneda", "", "NombreDeLaMoneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCodigo", "", "Codigo"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroCuenta", "", "NumeroCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCuenta", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSaldoInicial", "", "SaldoInicial"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroDeDocumento", "", "NumeroDocumento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConcepto", "", "Concepto"
         gUtilReports.sDefaultConfigurationForIntegerFields refRpt, "txtConsecutivo", "", "ConsecutivoMovimiento"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConceptoDescripcion", "", "Descripcion"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDebe", "", "MontoIngreso"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtHaber", "", "MontoEgreso"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtOperador", "", "NombreOperador"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtBeneficiario", "", "Beneficiario"
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelReporteDeTransaccionesPorBeneficiario = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelReporteDeTransaccionesPorBeneficiario", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraDatosDelReporteDeDeclaracionImpuestoTransaccionesFinancieras(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim nombreDelFile As String
   Dim valTituloReporte As String
   Dim exito As Boolean
   On Error GoTo h_ERROR
   exito = False
   nombreDelFile = fNombreFileDeImpresionDelReporte(eBcoRpt_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS)
   valTituloReporte = fNombreReporteParaMensajes(eBcoRpt_DECLARACION_IMPUESTO_TRANSACCIONES_FINANCIERAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFile) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSqlDelReporte) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblTituloDelReporte", valTituloReporte
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GroupHeader1", "NombreCuenta", ddGrpAll, ddRepeatOnPage, True, ddNPBefore
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCuentaBancaria", "", "NombreCuenta"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConceptos", "", "Concepto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCantOperaciones", "", "DocumentosEfectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtBaseImponible", "", "MontoEfectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalITF", "", "MontoITF"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalBaseImponible", "MontoEfectivo", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalImpuesto", "MontoITF", eSF_SUM, "GroupHeader1", eSR_GROUP, eST_SUB_TOTAL
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT: On Error GoTo 0
   fConfiguraDatosDelReporteDeDeclaracionImpuestoTransaccionesFinancieras = exito
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfiguraDatosDelReporteDeDeclaracionImpuestoTransaccionesFinancieras", "Reporte", eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

