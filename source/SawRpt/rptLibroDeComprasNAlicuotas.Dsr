VERSION 5.00
Begin {9EB8768B-CDFA-44DF-8F3E-857A8405E1DB} rptLibroDeComprasNAlicuotas 
   Caption         =   "Libro De Compras"
   ClientHeight    =   10950
   ClientLeft      =   225
   ClientTop       =   555
   ClientWidth     =   15240
   StartUpPosition =   3  'Windows Default
   WindowState     =   2  'Maximized
   _ExtentX        =   26882
   _ExtentY        =   19315
   SectionData     =   "rptLibroDeComprasNAlicuotas.dsx":0000
End
Attribute VB_Name = "rptLibroDeComprasNAlicuotas"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "rptLibroDeCompras"
Private Const CM_MESSAGE_NAME As String = "Reporte Libro de Compras"
Private Const GetGender = Enum_Gender.eg_Male
Private Const ERR_NOHAYIMPRESORA = 5007
Private Const CM_SUB_INFORME_DE_PRORRATEO As Integer = 0
Private mRepFechaInicioMes As Date
Private mFechaDeHoy As String
Private mSumExoneradas As Currency
Private mSumNoSujetas As Currency
Private mSumSinDerechoCredito As Currency
Private mSumExentas As Currency
Private mSumNoCausaImpuesto As Currency
Private mSumCINBaseImponibleAlicuotaGeneral As Currency
Private mSumCINBaseImponibleAlicuota2 As Currency
Private mSumCINBaseImponibleAlicuota3 As Currency
Private mSumCINImpuestoAlicuotaGeneral As Currency
Private mSumCINImpuestoAlicuota2 As Currency
Private mSumCINImpuestoAlicuota3 As Currency
Private mSumCIMBaseImponibleAlicuotaGeneral As Currency
Private mSumCIMBaseImponibleAlicuota2 As Currency
Private mSumCIMBaseImponibleAlicuota3 As Currency
Private mSumCIMImpuestoAlicuotaGeneral As Currency
Private mSumCIMImpuestoAlicuota2 As Currency
Private mSumCIMImpuestoAlicuota3 As Currency
Private mPorcentajeProrrateo As Currency
Private mPorcentajeIVAAlicuotaGeneral As Currency
Private mPorcentajeIVAAlicuota2 As Currency
Private mPorcentajeIVAAlicuota3 As Currency
Private mPorcentajeIVAAlicuota3ParaPrompt As Currency
Private mTotalCreditosFiscalesDeducibles As Currency
Private mCreditosFiscalesTotalmenteDeducibles As Currency
Private mCreditosProrrateables As Currency
Private gFacturaNav As Object
Private gProyCompaniaActual As Object
Private gNoComun As Object
Private gRPTFacturaConfigurar As Object

Public Sub sConfigurarDatosDelReporte(ByVal valSqlDelReporte As String, ByVal valMes As String, ByVal ValAno As String, ByVal valPorcentajeProrrateo As Currency, ByVal valMostrarInformeDeProrrateo As Boolean, ByVal valMostrarResumenDeVentas As Boolean, ByVal valNombreCompaniaParaInformes As String, ByRef refInsFactura As Object, ByRef refInsCompaniaActual As Object, ByRef refInsNoComun As Object, ByRef refInsRPTFacturaConfigurar As Object, ByRef gGlobalization As Object, ByRef gAdmAlicuotaIvaActual As Object, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByRef valCreditosFiscalesTotalmenteDeducibles As Currency, ByRef valCreditosProrrateablesParcialmenteDeducibles As Currency, Optional ByVal valAplicaAContribuyentesEpeciales As Boolean = False)
   Dim SubRptInformeDeProrrateo As DDActiveReports2.ActiveReport
   Dim nombreDelFileXML As String
   Dim vCreditosFiscalesTotalmenteDeducibles As Currency
   Dim vCreditosProrrateables As Currency
   Dim vTotalCreditosFiscalesDeducibles As Currency
   Set gFacturaNav = refInsFactura
   Set gProyCompaniaActual = refInsCompaniaActual
   Set gNoComun = refInsNoComun
   Set gRPTFacturaConfigurar = refInsRPTFacturaConfigurar
   Dim rsVentas As ADODB.Recordset
   On Error GoTo h_ERROR
   Set rsVentas = New ADODB.Recordset
   vCreditosFiscalesTotalmenteDeducibles = valCreditosFiscalesTotalmenteDeducibles
   vCreditosProrrateables = valCreditosProrrateablesParcialmenteDeducibles
   vTotalCreditosFiscalesDeducibles = vCreditosFiscalesTotalmenteDeducibles + vCreditosProrrateables
   mRepFechaInicioMes = gConvert.fConvertStringToDate("01/" & valMes & "/" & ValAno, True)
   lblNumeroDePagina.Visible = gProyParametros.GetImprimirNoPagina
   sConfiguraLosPorcentajesDeLasAlicuotasDelIVA gAdmAlicuotaIvaActual
   If gUtilReports.fDefaultConfigurationForDataControl(Me, "dcOrigenData", valSqlDelReporte) Then
      gUtilReports.sDefaultConfigurationForLabels Me, "lblCompania", valNombreCompaniaParaInformes
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNombreProveedor", "Nombre Proveedor/ Nº " & gGlobalization.fPromptRIF
      gUtilReports.sDefaultConfigurationForLabels Me, "lblTotalComprasIncluyendoIva", "Total Compras + " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultConfigurationForLabels Me, "lblNoCausaImpuesto", "No Causa " & gGlobalization.fPromptIVA
      gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision Me, "lblFechaYHoraDeEmision"
      gUtilReports.sConfiguraEncabezado Me, "lblCompania", "lblFechaYHoraDeEmision", "lblLibroDeCompra", "LblNumeroDePagina", "", True, True
      If valAplicaAContribuyentesEpeciales Then
        lblLibroDeCompra.Caption = "Informe del Libro de Compra del " & gConvert.dateToString(valFechaDesde) & " al " & gConvert.dateToString(valFechaHasta)
      Else
        lblLibroDeCompra.Caption = lblLibroDeCompra.Caption & " " & gEnumReport.enumMesToString(valMes) & " - " & ValAno
      End If
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoFactura", "", "Numero"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNombreProveedor", "", "NombreProveedor"
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalComprasIncluyendoIva", "", "Monto"
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtNoRif", "", "NumeroRif"
      gUtilReports.sDefaultConfigurationForDateFields Me, "txtDia", eFT_DATE, "Fecha", eFT_DATE
      gUtilReports.sDefaultConfigurationForStrFields Me, "txtCreditosND", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtNoCausaImpuesto", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtTotalComprasIncluyendoIva", "", ""
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtRFTotalCIMTodasLasAlicDeducibles", vCreditosFiscalesTotalmenteDeducibles
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtRFTotalCIMTodasLasAlicConProrrateo", vCreditosProrrateables
      gUtilReports.sDefaultConfigurationForNumericFields Me, "txtRFTotalCreditosCIM", vTotalCreditosFiscalesDeducibles
      nombreDelFileXML = fNombreFileDeImpresionDelReporte(CM_SUB_INFORME_DE_PRORRATEO)
      lblRFPorcionDeducible.Caption = lblRFPorcionDeducible.Caption & " " & gConvert.fNumToStringConSeparadorDeMiles(valPorcentajeProrrateo) & "%"
      mPorcentajeProrrateo = valPorcentajeProrrateo
      sAsignaLosValoresParaLosCreditosDeducibles valMes, ValAno, valFechaDesde, valFechaHasta
      If valMostrarInformeDeProrrateo Then
         pbrInformesDeProrrateo.Enabled = True
         Set SubRptInformeDeProrrateo = New DDActiveReports2.ActiveReport
         nombreDelFileXML = fNombreFileDeImpresionDelReporte(CM_SUB_INFORME_DE_PRORRATEO)
         gNoComun.sCalculaVentasYPorcentaje valMes, ValAno, rsVentas, eP_Mensual, valFechaDesde, valFechaHasta
         If fConfigurarDatosSubReporteInformeDeProrrateo(SubRptInformeDeProrrateo, nombreDelFileXML, valMes, ValAno, valFechaDesde, valFechaHasta, vCreditosFiscalesTotalmenteDeducibles, vCreditosProrrateables, vTotalCreditosFiscalesDeducibles, gNoComun, rsVentas) Then
            gUtilReports.fAsignaSubReporteSiExite Me, SubRptInformeDeProrrateo, "SubRptInformeDeProrrateo"
            Call SubRptInformeDeProrrateo.AddNamedItem("gUtilReports", gUtilReports)
         End If
         Set SubRptInformeDeProrrateo = Nothing
      End If
      gRPTFacturaConfigurar.sCargarResumenDesdePlanilla Me, valMes, ValAno, True, valMostrarResumenDeVentas, valFechaDesde, valFechaHasta, valCreditosFiscalesTotalmenteDeducibles, valCreditosProrrateablesParcialmenteDeducibles
      sInicializaAcumuladores
      gUtilMargins.sAsignarMargenesGenerales Me
   End If
   Set gFacturaNav = Nothing
   Set gProyCompaniaActual = Nothing
   gDbUtil.sDestroyRecordSet rsVentas
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sConfigurarDatosDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fConfigurarDatosSubReporteInformeDeProrrateo(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valNombreDelFileXML As String, ByVal valMes As String, ByVal ValAno As String, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, ByVal valCreditosFiscalesTotalmenteDeducibles As Currency, ByVal valCreditosProrrateables As Currency, ByVal valTotalCreditosFiscalesDeducibles As Currency, ByRef refInsNoComun As Object, ByRef refrsVentas As ADODB.Recordset) As Boolean
   Dim exito As Boolean
   Dim ventasGravadas As Currency
   Dim ventasTotales As Currency
   Dim porcentajeDeProrrateo As Currency
   Dim mVentasGravadasTemp As Currency
   On Error GoTo h_ERROR
   Set gNoComun = refInsNoComun
   Dim NumeroDeDecimales As Byte
   exito = False
   ventasGravadas = 0
   ventasTotales = 0
   NumeroDeDecimales = 2
   
   ventasTotales = gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasInternasNoGravadas"), False, NumeroDeDecimales))
   ventasTotales = ventasTotales + gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasDeExportacion"), False, NumeroDeDecimales))
   ventasTotales = ventasTotales + gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasIntBaseAlicuotaGeneral"), False, NumeroDeDecimales))
   ventasTotales = ventasTotales + gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasIntBaseAlicuotaGeneralMasAdicional"), False, NumeroDeDecimales))
   ventasTotales = ventasTotales + gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasIntBaseAlicuotaReducida"), False, NumeroDeDecimales))
   
   ventasGravadas = gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasIntBaseAlicuotaGeneral"), False, NumeroDeDecimales))
   ventasGravadas = ventasGravadas + gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasIntBaseAlicuotaGeneralMasAdicional"), False, NumeroDeDecimales))
   ventasGravadas = ventasGravadas + gConvert.fConvertStringToCurrency(gConvert.fNumToStringConSeparadorDeMiles(fValorNumericoDelRegistro(refrsVentas, "VentasIntBaseAlicuotaReducida"), False, NumeroDeDecimales))

   If ventasTotales = 0 Then
      porcentajeDeProrrateo = 0
   Else
      porcentajeDeProrrateo = ((ventasGravadas / ventasTotales) * 100)
      porcentajeDeProrrateo = gConvert.fTruncaANDecimales(porcentajeDeProrrateo, 2)
   End If

   If gUtilReports.fLoadLayout(refRpt, valNombreDelFileXML) Then
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreDelContribuyente", gProyCompaniaActual.GetNombre
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtRifDelContribuyente", gProyCompaniaActual.getNumeroDeRif
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMesAplicacion", gConvert.ConvierteAInteger(valMes)
      gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtAnoAplicacion", gConvert.ConvierteAInteger(ValAno)
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVentasGravadas", ventasGravadas
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVentasTotales", ventasTotales
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtPorcentajeProrrateo", porcentajeDeProrrateo
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComprasDeducibles", valCreditosFiscalesTotalmenteDeducibles
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalComprasProrrateables", valCreditosProrrateables
      gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCreditosFiscales", valTotalCreditosFiscalesDeducibles
      exito = True
   End If
h_EXIT: On Error GoTo 0
   fConfigurarDatosSubReporteInformeDeProrrateo = exito
   Exit Function
h_ERROR: fConfigurarDatosSubReporteInformeDeProrrateo = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConfigurarDatosSubReporteInformeDeProrrateo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub ActiveReport_Terminate()
   On Error GoTo h_ERROR
   WindowState = vbNormal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ActiveReport_Te rminate", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_NoData()
   On Error GoTo h_ERROR
   gMessage.InformationMessage "No se encontró información para imprimir", "RESULTADO"
   Cancel
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ActiveReport_NoData", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_Error(ByVal Number As Integer, ByVal Description As DDActiveReports2.IReturnString, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, ByVal CancelDisplay As DDActiveReports2.IReturnBool)
   On Error GoTo h_ERROR
   If Number <> ERR_NOHAYIMPRESORA Then  'Ignore No printer warning
      gMessage.AlertMessage " " & Description, "ERROR PROCESANDO DATOS"
   End If
   CancelDisplay = True
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ActiveReport_Error", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ActiveReport_PageStart()
   On Error GoTo h_ERROR
   lblNumeroDePagina.Caption = "Pág. " & pageNumber
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ActiveReport_PageStar", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Detail_Format()
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   txtTipoDeCompra.Text = gEnumProyecto.enumTipoDeCompraToStringSiglas(dcOrigenData.Recordset!tipoDeCompra)
   txtTotalComprasIncluyendoIva.Text = gConvert.FormatoNumerico(txtTotalComprasIncluyendoIva, False)
   sInicializarCampos
   If DcOrigenData.Recordset!Status = eSD_ANULADO Then
      txtCreditosND.Text = "Anulada"
      txtCreditosND.Width = 800
      txtCreditosND.Visible = True
      txtTotalComprasIncluyendoIva.Text = "0,00"
      txtNoCausaImpuesto.Text = "0,00"
   Else
      Select Case DcOrigenData.Recordset!tipoDeCompra
         Case eTD_COMPRASNACIONALES: sLlenarCamposDeComprasInternas
         Case eTD_COMPRASIMPORTACION: sLlenarCamposDeComprasDeImportacion
         Case eTD_COMPRASEXENTAS, eTD_COMPRASEXONERADAS, eTD_COMPRASNOSUJETAS, eTD_COMPRASSINDERECHOACREDITO: sLlenarCamposYAcumuladoresDeComprasSinImpuesto
      End Select
   End If
   mSumNoCausaImpuesto = mSumNoCausaImpuesto + gConvert.fConvierteACurrency(txtNoCausaImpuesto.Text)
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Detail_Format", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub ReportFooter_Format()
   On Error GoTo h_ERROR
   sReportFooterLlenaLasLineasDeSubTotalesPorAlicuota
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ReportFooter_Format", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sLlenarCamposDeComprasInternas()
   Dim varMontoBaseImponibleProrrateda As Currency
   Dim varMontoImpuestoProrrateado As Currency
   On Error GoTo h_ERROR
   If Not DcOrigenData.Recordset.EOF Then
      Select Case DcOrigenData.Recordset!creditoFiscal
         Case eCF_DEDUCIBLE
            txtCreditosDEPD.Visible = True
            txtNoCausaImpuesto.Text = gConvert.FormatoNumerico(DcOrigenData.Recordset!MontoExentoConMoneda, False)
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio) <> 0 Then
               txtBasesComprasInternasConcatenadas.Text = txtBasesComprasInternasConcatenadas.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio), False) & vbCrLf
               txtImpuestosComprasInternasConcatenados.Text = txtImpuestosComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio), False) & vbCrLf
               txtPorcentajesAlicuotasComprasInternasConcatenados.Text = txtPorcentajesAlicuotasComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuotaGeneral, False) & vbCrLf
               txtCreditosDEPD.Text = txtCreditosDEPD.Text & "DE" & vbCrLf
               mSumCINBaseImponibleAlicuotaGeneral = mSumCINBaseImponibleAlicuotaGeneral + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio)
               mSumCINImpuestoAlicuotaGeneral = mSumCINImpuestoAlicuotaGeneral + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio) <> 0 Then
               txtBasesComprasInternasConcatenadas.Text = txtBasesComprasInternasConcatenadas.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio), False) & vbCrLf
               txtImpuestosComprasInternasConcatenados.Text = txtImpuestosComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota2xCambio), False) & vbCrLf
               txtPorcentajesAlicuotasComprasInternasConcatenados.Text = txtPorcentajesAlicuotasComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota2, False) & vbCrLf
               txtCreditosDEPD.Text = txtCreditosDEPD.Text & "DE" & vbCrLf
               mSumCINBaseImponibleAlicuota2 = mSumCINBaseImponibleAlicuota2 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio)
               mSumCINImpuestoAlicuota2 = mSumCINImpuestoAlicuota2 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota2xCambio)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio) <> 0 Then
               txtBasesComprasInternasConcatenadas.Text = txtBasesComprasInternasConcatenadas.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio), False) & vbCrLf
               txtImpuestosComprasInternasConcatenados.Text = txtImpuestosComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota3xCambio), False) & vbCrLf
               txtPorcentajesAlicuotasComprasInternasConcatenados.Text = txtPorcentajesAlicuotasComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota3, False) & vbCrLf
               txtCreditosDEPD.Text = txtCreditosDEPD.Text & "DE" & vbCrLf
               mSumCINBaseImponibleAlicuota3 = mSumCINBaseImponibleAlicuota3 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio)
               mSumCINImpuestoAlicuota3 = mSumCINImpuestoAlicuota3 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota3xCambio)
            End If
          Case eCF_NoDeducible
             txtCreditosND.Visible = True
             txtNoCausaImpuesto.Text = txtTotalComprasIncluyendoIva
          Case eCF_PRORRATEABLE
             txtCreditosDEPD.Visible = True
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio) <> 0 Then
               txtBasesComprasInternasConcatenadas.Text = txtBasesComprasInternasConcatenadas.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtImpuestosComprasInternasConcatenados.Text = txtImpuestosComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtPorcentajesAlicuotasComprasInternasConcatenados.Text = txtPorcentajesAlicuotasComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuotaGeneral, False) & vbCrLf
               txtCreditosDEPD.Text = txtCreditosDEPD.Text & "PD" & vbCrLf
               mSumCINBaseImponibleAlicuotaGeneral = mSumCINBaseImponibleAlicuotaGeneral + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio) * mPorcentajeProrrateo) / 100, 2)
               mSumCINImpuestoAlicuotaGeneral = mSumCINImpuestoAlicuotaGeneral + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio) * mPorcentajeProrrateo) / 100, 2)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio) <> 0 Then
               txtBasesComprasInternasConcatenadas.Text = txtBasesComprasInternasConcatenadas.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoGravableAlicuota2xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtImpuestosComprasInternasConcatenados.Text = txtImpuestosComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoIVAAlicuota2xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtPorcentajesAlicuotasComprasInternasConcatenados.Text = txtPorcentajesAlicuotasComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota2, False) & vbCrLf
               txtCreditosDEPD.Text = txtCreditosDEPD.Text & "PD" & vbCrLf
               mSumCINBaseImponibleAlicuota2 = mSumCINBaseImponibleAlicuota2 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio) * mPorcentajeProrrateo) / 100, 2)
               mSumCINImpuestoAlicuota2 = mSumCINImpuestoAlicuota2 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota2xCambio) * mPorcentajeProrrateo) / 100, 2)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio) <> 0 Then
               txtBasesComprasInternasConcatenadas.Text = txtBasesComprasInternasConcatenadas.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoGravableAlicuota3xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtImpuestosComprasInternasConcatenados.Text = txtImpuestosComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoIVAAlicuota3xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtPorcentajesAlicuotasComprasInternasConcatenados.Text = txtPorcentajesAlicuotasComprasInternasConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota3, False) & vbCrLf
               txtCreditosDEPD.Text = txtCreditosDEPD.Text & "PD" & vbCrLf
               mSumCINBaseImponibleAlicuota3 = mSumCINBaseImponibleAlicuota3 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio) * mPorcentajeProrrateo) / 100, 2)
               mSumCINImpuestoAlicuota3 = mSumCINImpuestoAlicuota3 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota3xCambio) * mPorcentajeProrrateo) / 100, 2)
            End If
            sCalculaYDevuelveBaseImponibleEImpuestoConProrrateo varMontoBaseImponibleProrrateda, varMontoImpuestoProrrateado
            txtNoCausaImpuesto.Text = gConvert.fNumToStringConSeparadorDeMiles(DcOrigenData.Recordset!Monto - varMontoBaseImponibleProrrateda - varMontoImpuestoProrrateado)
      End Select
      mSumExentas = mSumExentas + gConvert.fConvierteACurrency(txtNoCausaImpuesto.Text)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLlenarCamposDeComprasInternas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sLlenarCamposDeComprasDeImportacion()
   Dim varMontoBaseImponibleProrrateda As Currency
   Dim varMontoImpuestoProrrateado As Currency
   On Error GoTo h_ERROR
   If Not DcOrigenData.Recordset.EOF Then
      Select Case DcOrigenData.Recordset!creditoFiscal
         Case eCF_DEDUCIBLE
            txtCreditosDEPDImport.Visible = True
            txtNoCausaImpuesto.Text = gConvert.FormatoNumerico(DcOrigenData.Recordset!MontoExentoConMoneda, False)
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio) <> 0 Then
               txtBasesComprasDeImportacionConcatenados.Text = txtBasesComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio), False) & vbCrLf
               txtImpuestosComprasDeImportacionConcatenados.Text = txtImpuestosComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio), False) & vbCrLf
               txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text = txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuotaGeneral, False) & vbCrLf
               txtCreditosDEPDImport.Text = txtCreditosDEPDImport.Text & "DE" & vbCrLf
               mSumCIMBaseImponibleAlicuotaGeneral = mSumCIMBaseImponibleAlicuotaGeneral + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio)
               mSumCIMImpuestoAlicuotaGeneral = mSumCIMImpuestoAlicuotaGeneral + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio) <> 0 Then
               txtBasesComprasDeImportacionConcatenados.Text = txtBasesComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio), False) & vbCrLf
               txtImpuestosComprasDeImportacionConcatenados.Text = txtImpuestosComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota2xCambio), False) & vbCrLf
               txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text = txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota2, False) & vbCrLf
               txtCreditosDEPDImport.Text = txtCreditosDEPDImport.Text & "DE" & vbCrLf
               mSumCIMBaseImponibleAlicuota2 = mSumCIMBaseImponibleAlicuota2 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio)
               mSumCIMImpuestoAlicuota2 = mSumCIMImpuestoAlicuota2 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota2xCambio)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio) <> 0 Then
               txtBasesComprasDeImportacionConcatenados.Text = txtBasesComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio), False) & vbCrLf
               txtImpuestosComprasDeImportacionConcatenados.Text = txtImpuestosComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota3xCambio), False) & vbCrLf
               txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text = txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota3, False) & vbCrLf
               txtCreditosDEPDImport.Text = txtCreditosDEPDImport.Text & "DE" & vbCrLf
               mSumCIMBaseImponibleAlicuota3 = mSumCIMBaseImponibleAlicuota3 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio)
               mSumCIMImpuestoAlicuota3 = mSumCIMImpuestoAlicuota3 + gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota3xCambio)
            End If
         Case eCF_NoDeducible
            txtCreditosND.Visible = True
            txtNoCausaImpuesto.Text = txtTotalComprasIncluyendoIva.Text
         Case eCF_PRORRATEABLE
            txtCreditosDEPDImport.Visible = True
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio) <> 0 Then
               txtBasesComprasDeImportacionConcatenados.Text = txtBasesComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtImpuestosComprasDeImportacionConcatenados.Text = txtImpuestosComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text = txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuotaGeneral, False) & vbCrLf
               txtCreditosDEPDImport.Text = txtCreditosDEPDImport.Text & "PD" & vbCrLf
               mSumCIMBaseImponibleAlicuotaGeneral = mSumCIMBaseImponibleAlicuotaGeneral + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio) * mPorcentajeProrrateo) / 100, 2)
               mSumCIMImpuestoAlicuotaGeneral = mSumCIMImpuestoAlicuotaGeneral + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio) * mPorcentajeProrrateo) / 100, 2)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio) <> 0 Then
               txtBasesComprasDeImportacionConcatenados.Text = txtBasesComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoGravableAlicuota2xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtImpuestosComprasDeImportacionConcatenados.Text = txtImpuestosComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoIVAAlicuota2xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text = txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota2, False) & vbCrLf
               txtCreditosDEPDImport.Text = txtCreditosDEPDImport.Text & "PD" & vbCrLf
               mSumCIMBaseImponibleAlicuota2 = mSumCIMBaseImponibleAlicuota2 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota2xCambio) * mPorcentajeProrrateo) / 100, 2)
               mSumCIMImpuestoAlicuota2 = mSumCIMImpuestoAlicuota2 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota2xCambio) * mPorcentajeProrrateo) / 100, 2)
            End If
            If gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio) <> 0 Then
               txtBasesComprasDeImportacionConcatenados.Text = txtBasesComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoGravableAlicuota3xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtImpuestosComprasDeImportacionConcatenados.Text = txtImpuestosComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((DcOrigenData.Recordset!MontoIVAAlicuota3xCambio * mPorcentajeProrrateo) / 100), 2), False) & vbCrLf
               txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text = txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text & gConvert.fNumToStringConSeparadorDeMiles(mPorcentajeIVAAlicuota3, False) & vbCrLf
               txtCreditosDEPDImport.Text = txtCreditosDEPDImport.Text & "PD" & vbCrLf
               mSumCIMBaseImponibleAlicuota3 = mSumCIMBaseImponibleAlicuota3 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoGravableAlicuota3xCambio) * mPorcentajeProrrateo) / 100, 2)
               mSumCIMImpuestoAlicuota3 = mSumCIMImpuestoAlicuota3 + gConvert.fTruncaANDecimales((gConvert.fConvierteACurrency(DcOrigenData.Recordset!MontoIVAAlicuota3xCambio) * mPorcentajeProrrateo) / 100, 2)
            End If
            sCalculaYDevuelveBaseImponibleEImpuestoConProrrateo varMontoBaseImponibleProrrateda, varMontoImpuestoProrrateado
            txtNoCausaImpuesto.Text = gConvert.fNumToStringConSeparadorDeMiles(DcOrigenData.Recordset!Monto - varMontoBaseImponibleProrrateda - varMontoImpuestoProrrateado)
      End Select
      mSumExentas = mSumExentas + gConvert.fConvierteACurrency(txtNoCausaImpuesto.Text)
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLlenarCamposDeComprasDeImportacion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sLlenarCamposYAcumuladoresDeComprasSinImpuesto()
   On Error GoTo h_ERROR
   txtNoCausaImpuesto.Text = txtTotalComprasIncluyendoIva.Text
   If Not DcOrigenData.Recordset.EOF Then
      Select Case DcOrigenData.Recordset!tipoDeCompra
         Case eTD_COMPRASEXENTAS: mSumExentas = mSumExentas + gConvert.fConvierteACurrency(txtNoCausaImpuesto)
         Case eTD_COMPRASEXONERADAS: mSumExoneradas = mSumExoneradas + gConvert.fConvierteACurrency(txtNoCausaImpuesto)
         Case eTD_COMPRASSINDERECHOACREDITO: mSumSinDerechoCredito = mSumSinDerechoCredito + gConvert.fConvierteACurrency(txtNoCausaImpuesto)
         Case eTD_COMPRASNOSUJETAS: mSumNoSujetas = mSumNoSujetas + gConvert.fConvierteACurrency(txtNoCausaImpuesto)
      End Select
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLlenarCamposYAcumuladoresDeComprasSinImpuesto", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInicializarCampos()
   On Error GoTo h_ERROR
   txtCreditosND.Visible = False
   txtCreditosND.Text = "ND"
   txtCreditosDEPD.Visible = False
   txtCreditosDEPDImport.Visible = False
   txtCreditosDEPD.Text = ""
   txtCreditosDEPDImport.Text = ""
   txtBasesComprasDeImportacionConcatenados.Text = ""
   txtImpuestosComprasDeImportacionConcatenados.Text = ""
   txtPorcentajesAlicuotasComprasDeImportacionConcatenados.Text = ""
   txtBasesComprasInternasConcatenadas.Text = ""
   txtImpuestosComprasInternasConcatenados.Text = ""
   txtPorcentajesAlicuotasComprasInternasConcatenados.Text = ""
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInicializarCampos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInicializaAcumuladores()
   On Error GoTo h_ERROR
   mSumExoneradas = 0
   mSumNoSujetas = 0
   mSumSinDerechoCredito = 0
   mSumExentas = 0
   mSumNoCausaImpuesto = 0
   mSumCINBaseImponibleAlicuota2 = 0
   mSumCINBaseImponibleAlicuota3 = 0
   mSumCINBaseImponibleAlicuotaGeneral = 0
   mSumCINImpuestoAlicuota2 = 0
   mSumCINImpuestoAlicuota3 = 0
   mSumCINImpuestoAlicuotaGeneral = 0
   mSumCIMBaseImponibleAlicuota2 = 0
   mSumCIMBaseImponibleAlicuota3 = 0
   mSumCIMBaseImponibleAlicuotaGeneral = 0
   mSumCIMImpuestoAlicuota2 = 0
   mSumCIMImpuestoAlicuota3 = 0
   mSumCIMImpuestoAlicuotaGeneral = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInicializaAcumuladores", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sConfiguraLosPorcentajesDeLasAlicuotasDelIVA(ByRef gAdmAlicuotaIvaActual As Object)
   On Error GoTo h_ERROR
   mPorcentajeIVAAlicuotaGeneral = gAdmAlicuotaIvaActual.GetAlicuotaIVA(mRepFechaInicioMes, eTD_ALICUOTAGENERAL)
   mPorcentajeIVAAlicuota2 = gAdmAlicuotaIvaActual.GetAlicuotaIVA(mRepFechaInicioMes, eTD_ALICUOTA2)
   mPorcentajeIVAAlicuota3 = gAdmAlicuotaIvaActual.GetAlicuotaIVA(mRepFechaInicioMes, eTD_ALICUOTA3)
   mPorcentajeIVAAlicuota3ParaPrompt = mPorcentajeIVAAlicuota3 - mPorcentajeIVAAlicuotaGeneral
h_EXIT:
   On Error GoTo 0
   Exit Sub
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sConfiguraLosPorcentajesDeLasAlicuotasDelIVA", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sReportFooterLlenaLasLineasDeSubTotalesPorAlicuota()
   Dim varTotalComprasAlicuotaGeneral As Currency
   Dim varTotalComprasAlicuota2 As Currency
   Dim varTotalComprasAlicuota3 As Currency
   On Error GoTo h_ERROR
   txtRFSubTotalBaseImponibleAlicuotaGeneralComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINBaseImponibleAlicuotaGeneral)
   txtRFSubTotalBaseImponibleAlicuota2ComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINBaseImponibleAlicuota2)
   txtRFSubTotalBaseImponibleAlicuota3ComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINBaseImponibleAlicuota3)
   txtRFSubTotalImpuestoAlicuotaGeneralComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINImpuestoAlicuotaGeneral)
   txtRFSubTotalImpuestoAlicuota2ComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINImpuestoAlicuota2)
   txtRFSubTotalImpuestoAlicuota3ComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINImpuestoAlicuota3)
   txtRFSubTotalBaseImponibleAlicuotaGeneralComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMBaseImponibleAlicuotaGeneral)
   txtRFSubTotalBaseImponibleAlicuota2ComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMBaseImponibleAlicuota2)
   txtRFSubTotalBaseImponibleAlicuota3ComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMBaseImponibleAlicuota3)
   txtRFSubTotalImpuestoAlicuotaGeneralComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMImpuestoAlicuotaGeneral)
   txtRFSubTotalImpuestoAlicuota2ComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMImpuestoAlicuota2)
   txtRFSubTotalImpuestoAlicuota3ComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMImpuestoAlicuota3)
   varTotalComprasAlicuotaGeneral = mSumNoCausaImpuesto + mSumCIMBaseImponibleAlicuotaGeneral + mSumCINBaseImponibleAlicuotaGeneral + mSumCIMImpuestoAlicuotaGeneral + mSumCINImpuestoAlicuotaGeneral
   varTotalComprasAlicuota2 = mSumCIMBaseImponibleAlicuota2 + mSumCINBaseImponibleAlicuota2 + mSumCIMImpuestoAlicuota2 + mSumCINImpuestoAlicuota2
   varTotalComprasAlicuota3 = mSumCIMBaseImponibleAlicuota3 + mSumCINBaseImponibleAlicuota3 + mSumCIMImpuestoAlicuota3 + mSumCINImpuestoAlicuota3
   txtRFSubTotalNoCausaImpuestoAlicuotaGeneral.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumNoCausaImpuesto)
   txtRFSubTotalComprasAlicuotaGeneral.Text = gConvert.fNumToStringConSeparadorDeMiles(varTotalComprasAlicuotaGeneral)
   txtRFSubTotalComprasAlicuota2.Text = gConvert.fNumToStringConSeparadorDeMiles(varTotalComprasAlicuota2)
   txtRFSubTotalComprasAlicuota3.Text = gConvert.fNumToStringConSeparadorDeMiles(varTotalComprasAlicuota3)
   txtRFSubTotalCompras.Text = gConvert.fNumToStringConSeparadorDeMiles(varTotalComprasAlicuotaGeneral + varTotalComprasAlicuota2 + varTotalComprasAlicuota3)
   txtRFSubTotalNoCausaImpuesto.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumNoCausaImpuesto)
   txtRFSubTotalBaseComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMBaseImponibleAlicuota2 + mSumCIMBaseImponibleAlicuota3 + mSumCIMBaseImponibleAlicuotaGeneral)
   txtRFSubTotalBaseComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINBaseImponibleAlicuota2 + mSumCINBaseImponibleAlicuota3 + mSumCINBaseImponibleAlicuotaGeneral)
   txtRFSubTotalImpuestoComprasDeImportacion.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCIMImpuestoAlicuota2 + mSumCIMImpuestoAlicuota3 + mSumCIMImpuestoAlicuotaGeneral)
   txtRFSubTotalImpuestoComprasInternas.Text = gConvert.fNumToStringConSeparadorDeMiles(mSumCINImpuestoAlicuota2 + mSumCINImpuestoAlicuota3 + mSumCINImpuestoAlicuotaGeneral)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sReportFooterLlenaLasLineasDeSubTotalesPorAlicuota", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sCalculaYDevuelveBaseImponibleEImpuestoConProrrateo(ByRef refBaseImponibleProrrateda As Currency, ByRef refImpuestoIVAProrrateado As Currency)
   Dim varBaseImponibleProrrateable As Currency
   On Error GoTo h_ERROR
   refBaseImponibleProrrateda = gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((dcOrigenData.Recordset!MontoGravableAlicuotaGeneralxCambio * mPorcentajeProrrateo) / 100), 2)
   refBaseImponibleProrrateda = refBaseImponibleProrrateda + gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((dcOrigenData.Recordset!MontoGravableAlicuota2xCambio * mPorcentajeProrrateo) / 100), 2)
   refBaseImponibleProrrateda = refBaseImponibleProrrateda + gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((dcOrigenData.Recordset!MontoGravableAlicuota3xCambio * mPorcentajeProrrateo) / 100), 2)
   
   refImpuestoIVAProrrateado = gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((dcOrigenData.Recordset!MontoIVAAlicuotaGeneralxCambio * mPorcentajeProrrateo) / 100), 2)
   refImpuestoIVAProrrateado = refImpuestoIVAProrrateado + gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((dcOrigenData.Recordset!MontoIVAAlicuota2xCambio * mPorcentajeProrrateo) / 100), 2)
   refImpuestoIVAProrrateado = refImpuestoIVAProrrateado + gConvert.fTruncaANDecimales(gConvert.fConvierteACurrency((dcOrigenData.Recordset!MontoIVAAlicuota3xCambio * mPorcentajeProrrateo) / 100), 2)
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCalculaYDevuelveBaseImponibleEImpuestoConProrrateo", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fNombreFileDeImpresionDelReporte(ByVal valReporte As Integer) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case CM_SUB_INFORME_DE_PRORRATEO: nombreFileXML = "rpxSubInformeDeProrrateo"
      Case Else: nombreFileXML = ""
   End Select
   If LenB(nombreFileXML) > 0 Then
      fNombreFileDeImpresionDelReporte = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, "")
   Else
      fNombreFileDeImpresionDelReporte = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fNombreFileDeImpresionDelReporte", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sAsignaLosValoresParaLosCreditosDeducibles(ByVal valMes As String, ByVal ValAno As String, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date)
   Dim porcentaje As Currency
   Dim TotalCreditosParcialDeducibles As Currency
   On Error GoTo h_ERROR
   gNoComun.sCalculaPorcentajeAplicable porcentaje, mCreditosFiscalesTotalmenteDeducibles, mCreditosProrrateables, TotalCreditosParcialDeducibles, valMes, ValAno, valFechaDesde, valFechaHasta
   mTotalCreditosFiscalesDeducibles = mCreditosFiscalesTotalmenteDeducibles + mCreditosProrrateables
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignaLosValoresParaLosCreditosDeducibles", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fValorNumericoDelRegistro(ByVal rsValor As ADODB.Recordset, ByVal valFieldName As String) As Currency
   On Error GoTo h_ERROR
   fValorNumericoDelRegistro = 0
   If Not IsNull(rsValor.Fields(valFieldName).Value) Then
      If IsNumeric(rsValor.Fields(valFieldName).Value) Then
         fValorNumericoDelRegistro = gConvert.fConvertStringToCurrency(rsValor.Fields(valFieldName).Value)
      Else
         gMessage.ProgrammerMessage "Atención Programador" & vbCrLf & "Este Registro -->'" & valFieldName & "'<-- contiene Datos no númericos."
      End If
   End If
h_EXIT:   On Error GoTo 0
   Exit Function
h_ERROR:    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fValorNumericoDelregistro", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
