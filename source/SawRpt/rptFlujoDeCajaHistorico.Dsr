VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptFlujoDeCajaHistorico 
   Caption         =   "Flujo de caja historico"
   ClientHeight    =   8595
   ClientLeft      =   165
   ClientTop       =   450
   ClientWidth     =   11880
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   _ExtentX        =   20955
   _ExtentY        =   15161
   SectionData     =   "rptFlujoDeCajaHistorico.dsx":0000
End
Attribute VB_Name = "rptFlujoDeCajaHistorico"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptFlujoDeCajaHistorico"
Private Const CM_MESSAGE_NAME As String = "Reporte Flujo De Caja Historico"
Private Const ERR_NOHAYIMPRESORA = 5007
Private mFechaInicial As Date
Private mFechaFinal As Date
Private FechaObtenida As String
Private mTextBox As String
Private mValComboCantidadAImprimir As Integer
Private mIngresos As Currency
Private mEgresos As Currency
Private mAcumuladorIngresos As Currency
Private mAcumuladorIngresoPorConcepto As Currency
Private mAcumuladorEgresos As Currency
Private maAcumuladorEgresoPorConcepto As Currency
Private mAcumuladorSaldos As Currency
Private mSaldoInicial As Currency
Private mSaldo As Currency
Private mrsIngreso As ADODB.Recordset
Private mrsEgreso As ADODB.Recordset
Private gUtilSQL As Object
Private gProyCompaniaActual As Object

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valFechaInicial As Date, _
                                       ByVal valFechaFinal As Date, ByVal seleccion As Integer, ByVal valNombreCompaniaParaInformes As String, ByRef refInsUtilSQL As Object, ByRef refInsCompaniaActual As Object)
   On Error GoTo h_ERROR
   Set gUtilSQL = refInsUtilSQL
   Set gProyCompaniaActual = refInsCompaniaActual
   mFechaInicial = valFechaInicial
   mFechaFinal = valFechaFinal
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "DataControl1", valSqlDelReporte) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreDeLaCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sDefaultConfigurationForLabels Me, "lblFechaInicialYFinal", "Del " & gConvert.dateToString(mFechaInicial) & " al " & gConvert.dateToString(mFechaFinal)
      gUtilReports.sConfiguraEncabezado Me, "lblNombreDeLaCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", "LblNumeroDePagina", "lblFechaInicialYFinal", True, True
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtDescripcionEgresoIngreso", "", "DescripcionDeConcepto"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtMoneda", "", "NombreMoneda"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtSaldoDebe", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtSaldoHaber", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtSaldoInicialNegativo", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtSaldoInicialPositivo", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalDelReporte", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalDiferencia", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalDiferenciaIngresoEgreso", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotales", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalSaldoDebe", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalSaldoDebe2", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalSaldoHaber", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalSaldoHaber2", "", ""
      sOutPutFormatCamposNumerico
      'este código estaba en reportstart
      GroupHeader2.DataField = "TipoDeConcepto"
      If seleccion = 1 Then
        Detail.Visible = False
        txtDescripcionEgresoIngresoAgrupado.DataField = txtDescripcionEgresoIngreso.DataField
        gUtilReports.sDefaultConfigurationForNumericFields Me, "txtSaldoDebeAgrupado", "", ""
        gUtilReports.sDefaultConfigurationForNumericFields Me, "txtSaldoHaberAgrupado", "", ""
        GroupHeader3.DataField = "DescripcionDeConcepto"
      End If
      gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter Me, "GroupHeader1", "NombreMoneda", ddGrpFirstDetail, ddRepeatOnPageIncludeNoDetail, True, ddNPBeforeAfter
   '   GroupHeader1.GrpKeepTogether = ddGrpFirstDetail
   '   GroupHeader1.KeepTogether = True
   '   GroupHeader1.Repeat = ddRepeatOnPageIncludeNoDetail
      If Not DataControl1.Recordset.EOF Then
         Set mrsIngreso = New ADODB.Recordset
         Set mrsEgreso = New ADODB.Recordset
         sConstruirSQLIngreso
         sConstruirSQLEgreso
         If mrsIngreso.RecordCount > 0 Then
            If mrsIngreso!MontoIngreso <> "" Then
               mIngresos = mrsIngreso!MontoIngreso
            End If
         End If
         If mrsEgreso.RecordCount > 0 Then
            If mrsEgreso!MontoEgreso <> "" Then
               mEgresos = mrsEgreso!MontoEgreso
            End If
         End If
         mSaldoInicial = mIngresos - mEgresos
         Set mrsIngreso = Nothing
         Set mrsEgreso = Nothing
      End If
      txtSaldoInicialPositivo.Text = ""
      txtSaldoInicialNegativo.Text = ""
         If mSaldoInicial > 0 Then
            txtSaldoInicialPositivo.Text = gConvert.FormatoNumerico(CStr(mSaldoInicial), False)
         Else
            txtSaldoInicialNegativo.Text = gConvert.FormatoNumerico(CStr(mSaldoInicial), False)
         End If
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
   WindowState = vbNormal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_Terminate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_NoData()
   On Error GoTo h_ERROR
   gMessage.InformationMessage "No se encontró información para imprimir", "RESULTADO"
   Cancel
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_NoData", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Error(ByVal Number As Integer, ByVal Description As DDActiveReports2.IReturnString, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, ByVal CancelDisplay As DDActiveReports2.IReturnBool)
   On Error GoTo h_ERROR
   If Number <> ERR_NOHAYIMPRESORA Then  'Ignore No printer warning
      gMessage.AlertMessage " " & Description, "ERROR PROCESANDO DATOS"
   End If
   CancelDisplay = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_Error", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_PageStart()
   On Error GoTo h_ERROR
   LblNumeroDePagina.Caption = "Pág " & pageNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "ActiveReport_PageStar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub Detail_Format()
   On Error GoTo h_ERROR
   txtSaldoDebe.Text = "0.00"
   txtSaldoHaber.Text = "0.00"
   If DataControl1.Recordset!TipoDeConcepto = "0" Then
      txtSaldoDebe.Text = ""
      txtSaldoDebe.Text = gConvert.FormatoNumerico(DataControl1.Recordset!montoIE, False)
      mAcumuladorIngresos = mAcumuladorIngresos + txtSaldoDebe.Text
      mAcumuladorIngresoPorConcepto = mAcumuladorIngresoPorConcepto + txtSaldoDebe.Text
   Else
      txtSaldoHaber.Text = ""
      txtSaldoHaber.Text = gConvert.FormatoNumerico(DataControl1.Recordset!montoIE, False)
      mAcumuladorEgresos = mAcumuladorEgresos + txtSaldoHaber.Text
      maAcumuladorEgresoPorConcepto = maAcumuladorEgresoPorConcepto + txtSaldoHaber.Text
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "Detail_Format", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOutPutFormatCamposNumerico()
   On Error GoTo h_ERROR
   txtSaldoDebe.OutputFormat = gUtilReports.getDefaultNumericFormat
   txtSaldoHaber.OutputFormat = gUtilReports.getDefaultNumericFormat
   txtSaldoDebeAgrupado.OutputFormat = gUtilReports.getDefaultNumericFormat
   txtSaldoHaberAgrupado.OutputFormat = gUtilReports.getDefaultNumericFormat
   txtSaldoInicialNegativo.OutputFormat = gUtilReports.getDefaultNumericFormat
   txtSaldoInicialPositivo.OutputFormat = gUtilReports.getDefaultNumericFormat
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sOutPutFormatCamposNumerico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sConstruirSQLIngreso()
   Dim SQLIngreso As String
   On Error GoTo h_ERROR
   If Not DataControl1.Recordset.EOF Then
   SQLIngreso = ""
   SQLIngreso = " SELECT SUM(Monto) as MontoIngreso "
   'SQLIngreso = SQLIngreso & " FROM MovimientoBancario, ConceptoBancario, CuentaBancaria "
   SQLIngreso = SQLIngreso & " FROM cuentaBancaria INNER JOIN (conceptoBancario INNER JOIN movimientoBancario ON conceptoBancario.Codigo = movimientoBancario.CodigoConcepto) ON  (cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria) AND (cuentaBancaria.ConsecutivoCompania = movimientoBancario.ConsecutivoCompania) "
   SQLIngreso = SQLIngreso & " WHERE MovimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo "
   SQLIngreso = SQLIngreso & " AND MovimientoBancario.CodigoConcepto =  conceptobancario.codigo "
   SQLIngreso = SQLIngreso & " AND Fecha < " & gUtilSQL.fDateToSQLValue(mFechaInicial)
   SQLIngreso = SQLIngreso & " AND MovimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQLIngreso = SQLIngreso & " AND ConceptoBancario.Tipo = '" & gConvert.enumerativoAChar(eIE_INGRESO) & "'"
   'debug.print SQLIngreso
   gDbUtil.openRecordSet mrsIngreso, SQLIngreso
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConstruirSQLIngreso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sConstruirSQLEgreso()
   Dim SQLEgreso As String
   On Error GoTo h_ERROR
   If Not DataControl1.Recordset.EOF Then
   SQLEgreso = ""
   SQLEgreso = " SELECT SUM(Monto) as MontoEgreso "
   'SQLEgreso = SQLEgreso & " FROM MovimientoBancario, ConceptoBancario, CuentaBancaria "
   SQLEgreso = SQLEgreso & " FROM cuentaBancaria INNER JOIN (conceptoBancario INNER JOIN movimientoBancario ON conceptoBancario.Codigo = movimientoBancario.CodigoConcepto) ON  (cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria) AND (cuentaBancaria.ConsecutivoCompania = movimientoBancario.ConsecutivoCompania) "
   SQLEgreso = SQLEgreso & " WHERE MovimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo "
   SQLEgreso = SQLEgreso & " AND MovimientoBancario.CodigoConcepto =  conceptobancario.codigo "
   SQLEgreso = SQLEgreso & " AND Fecha <  " & gUtilSQL.fDateToSQLValue(mFechaInicial)
   SQLEgreso = SQLEgreso & " AND MovimientoBancario.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQLEgreso = SQLEgreso & " AND ConceptoBancario.Tipo = '" & gConvert.enumerativoAChar(eIE_EGRESO) & "'"
   'debug.print SQLEgreso
   gDbUtil.openRecordSet mrsEgreso, SQLEgreso
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "sConstruirSQLEgreso", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter1_AfterPrint()
   On Error GoTo h_ERROR
   mAcumuladorIngresos = 0
   mAcumuladorEgresos = 0
   txtTotalSaldoDebe2.Text = ""
   txtTotalSaldoHaber2.Text = ""
   txtTotalDiferenciaIngresoEgreso.Text = ""
   txtSaldoAlFinalDelPeriodo.Text = ""
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupFooter1_AfterPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter1_BeforePrint()
   On Error GoTo h_ERROR
   txtTotalSaldoDebe2.Text = mAcumuladorIngresos
   txtTotalSaldoDebe2.Text = gConvert.FormatoNumerico(CStr(txtTotalSaldoDebe2.Text), False)
   txtTotalSaldoHaber2.Text = txtTotalSaldoHaber.Text
   txtTotalSaldoHaber2.Text = gConvert.FormatoNumerico(CStr(txtTotalSaldoHaber2.Text), False)
   txtTotalDiferenciaIngresoEgreso.Text = txtTotalSaldoDebe2.Text - txtTotalSaldoHaber2.Text
   txtTotalDiferenciaIngresoEgreso.Text = gConvert.FormatoNumerico(CStr(txtTotalDiferenciaIngresoEgreso.Text), False)
   txtSaldoAlFinalDelPeriodo.Text = txtTotalDiferenciaIngresoEgreso.Text
   txtSaldoAlFinalDelPeriodo.Text = gConvert.FormatoNumerico(CStr(txtSaldoAlFinalDelPeriodo.Text), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupFooter1_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub GroupFooter2_AfterPrint()
   On Error GoTo h_ERROR
  ' mAcumuladorIngresos = 0
    mAcumuladorEgresos = 0
   txtSaldoDebe.Text = ""
   txtSaldoHaber.Text = ""
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupFooter2_AfterPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter3_AfterPrint()
   On Error GoTo h_ERROR
   mAcumuladorIngresoPorConcepto = 0
   maAcumuladorEgresoPorConcepto = 0
   txtSaldoDebeAgrupado.Text = ""
   txtSaldoHaberAgrupado.Text = ""
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupFooter3_AfterPrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter3_BeforePrint()
   On Error GoTo h_ERROR
   If txtSaldoDebeAgrupado.Text <> "" And txtSaldoHaberAgrupado.Text <> "0.00" Then
      txtSaldoDebeAgrupado.Text = "0.00"
   Else
      txtSaldoDebeAgrupado.Text = mAcumuladorIngresoPorConcepto
      txtSaldoDebeAgrupado.Text = gConvert.FormatoNumerico(CStr(txtSaldoDebeAgrupado), False)
   End If
   txtSaldoHaberAgrupado.Text = maAcumuladorEgresoPorConcepto
   txtSaldoHaberAgrupado.Text = gConvert.FormatoNumerico(CStr(txtSaldoHaberAgrupado), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupFooter3_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupHeader1_AfterPrint()
   On Error GoTo h_ERROR
   GroupHeader1.NewPage = ddNPAfter
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupHeader1", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub


Private Sub GroupHeader2_Format()
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   If DataControl1.Recordset!TipoDeConcepto = "0" Then
      txtEgresosIngresos.Text = UCase(gEnumProyecto.enumIngresoEgresoToString(DataControl1.Recordset!TipoDeConcepto) _
      & "s" & " Operativos y Financieros")
      txtTotales.Text = "Total " & gEnumProyecto.enumIngresoEgresoToString(DataControl1.Recordset!TipoDeConcepto) _
      & "s : ....."
   Else
      txtEgresosIngresos.Text = UCase(gEnumProyecto.enumIngresoEgresoToString(DataControl1.Recordset!TipoDeConcepto) _
      & "s" & " Operativos y Financieros")
      txtTotales.Text = "Total " & gEnumProyecto.enumIngresoEgresoToString(DataControl1.Recordset!TipoDeConcepto) _
      & "s : ....."
   End If
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupHeader2_Format()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub GroupFooter2_BeforePrint()
   On Error GoTo h_ERROR
   If mAcumuladorEgresos > 0 Then
      txtTotalSaldoDebe.Text = "0,00"
   Else
      txtTotalSaldoDebe.Text = mAcumuladorIngresos
      txtTotalSaldoDebe.Text = gConvert.FormatoNumerico(CStr(txtTotalSaldoDebe), False)
   End If
   txtTotalSaldoHaber.Text = mAcumuladorEgresos
   txtTotalSaldoHaber.Text = gConvert.FormatoNumerico(CStr(txtTotalSaldoHaber), False)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GroupFooter2_BeforePrint", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

