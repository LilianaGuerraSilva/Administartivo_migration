VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCuadreCajaRpt"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME = "clsRptCuadreCajaConfigurar"
Private Const CM_MESSAGE_NAME = "Configuracion de Informes de Caja"

Private Enum eCajaRpt
   eCajaRpt_CUADRE_CAJA = 0
   eCajaRpt_TIPO_COBRO
   eCajaRpt_CUADRE_DETALLADO
   eCajaRpt_CUADRE_CAJA_CON_RESUMENES_ALICUOTA
   eCajaRpt_CUADRE_CAJA_CON_RESUMENES_TIPO_COBRO
   eCajaRpt_CUADRE_CAJA_CON_RESUMENES_FORMA_PAGO
   eCajaRpt_CUADRE_CAJA_RESUMIDO
   eCajaRpt_TIPO_COBRO_RESUMIDO
   eCajaRpt_CUADRE_CAJA_RESUMIDO_DETALLADO
   eCajaRpt_LISTADO_CAJAS_ABIERTAS
   eCajaRpt_CUADRE_CAJA_POR_USUARIO
   eCajaRpt_CUADRE_CAJA_POR_USUARIO_RESUMIDO
   eCajaRpt_CUADRE_CAJA_POR_TIPO_COBRO_Y_USUARIO
   ecajarpt_CUADRE_CAJA_RESUMEN_ANTICIPO_CAJA
   ecajarpt_CUADRE_CAJA_X_USUARIO
End Enum

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Function fNombreFileDeImpresionDelReporteDeCaja(ByVal valReporte As eCajaRpt) As String
   Dim nombreFileXML As String
   On Error GoTo h_ERROR
   Select Case valReporte
      Case eCajaRpt_CUADRE_CAJA: nombreFileXML = "rpxCuadreDeCaja"
      Case eCajaRpt_CUADRE_CAJA_POR_USUARIO: nombreFileXML = "rpxCuadreDeCajaPorUsuario"
      Case eCajaRpt_CUADRE_CAJA_POR_USUARIO_RESUMIDO: nombreFileXML = "rpxCuadreDeCajaPorUsuarioResumido"
      Case eCajaRpt_CUADRE_CAJA_RESUMIDO: nombreFileXML = "rpxCuadreDeCajaResumido"
      Case eCajaRpt_CUADRE_CAJA_RESUMIDO_DETALLADO: nombreFileXML = "rpxCuadreDeCajaDetalladoResumido"
      Case eCajaRpt_CUADRE_CAJA_CON_RESUMENES_ALICUOTA: nombreFileXML = "rpxSubCuadreDeCajaResumenesAlicuotas"
      Case eCajaRpt_CUADRE_CAJA_CON_RESUMENES_TIPO_COBRO: nombreFileXML = "rpxSubCuadreDeCajaResumenesTipoDeCobro"
      Case eCajaRpt_TIPO_COBRO: nombreFileXML = "rpxReporteDeCajaPorTipoDeCobro"
      Case eCajaRpt_TIPO_COBRO_RESUMIDO: nombreFileXML = "rpxReporteDeCajaPorTipoDeCobroResumido"
      Case eCajaRpt_CUADRE_DETALLADO: nombreFileXML = "rpxCuadreDeCajaDetallado"
      Case eCajaRpt_CUADRE_CAJA_CON_RESUMENES_FORMA_PAGO: nombreFileXML = "rpxSubCuadreDeCajaResumenesFormasDePago"
      Case eCajaRpt_LISTADO_CAJAS_ABIERTAS: nombreFileXML = "rpxReporteDeCajasAperturadas"
      Case eCajaRpt_CUADRE_CAJA_POR_TIPO_COBRO_Y_USUARIO: nombreFileXML = "rpxReporteDeCajaPorTipoDeCobroYPorUsuario"
      Case ecajarpt_CUADRE_CAJA_RESUMEN_ANTICIPO_CAJA: nombreFileXML = "rpxSubCuadreDeCajaResumenAnticipoCaja"
      Case ecajarpt_CUADRE_CAJA_X_USUARIO: nombreFileXML = "rpxCuadreDeCajaXCajero"
      Case Else: nombreFileXML = ""
   End Select
   If nombreFileXML <> "" Then
      fNombreFileDeImpresionDelReporteDeCaja = gUtilReports.getCompletePathDelFileDeImpresionRpx(nombreFileXML, True, fNombreDelReporteDeCaja(valReporte))
   Else
      fNombreFileDeImpresionDelReporteDeCaja = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fNombreFileDeImpresionDelReporteDeCaja", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fNombreDelReporteDeCaja(ByVal valReporte As eCajaRpt) As String
   Select Case valReporte
      Case eCajaRpt_CUADRE_CAJA: fNombreDelReporteDeCaja = "Cuadre de Cajas General"
      Case eCajaRpt_CUADRE_CAJA_POR_USUARIO: fNombreDelReporteDeCaja = "Cuadre de Cajas por Usuario"
      Case eCajaRpt_CUADRE_CAJA_POR_USUARIO_RESUMIDO: fNombreDelReporteDeCaja = "Cuadre de Cajas por Usuario Resumido"
      Case eCajaRpt_CUADRE_CAJA_RESUMIDO: fNombreDelReporteDeCaja = "Cuadre de Cajas General"
      Case eCajaRpt_CUADRE_CAJA_CON_RESUMENES_ALICUOTA: fNombreDelReporteDeCaja = "Cuadre de Cajas General con Resumenes"
      Case eCajaRpt_CUADRE_CAJA_CON_RESUMENES_TIPO_COBRO: fNombreDelReporteDeCaja = "Cuadre de Cajas General con Resumenes"
      Case eCajaRpt_CUADRE_CAJA_CON_RESUMENES_FORMA_PAGO: fNombreDelReporteDeCaja = "Cuadre de Cajas Detallado con Resumenes"
      Case eCajaRpt_TIPO_COBRO: fNombreDelReporteDeCaja = "Cuadre de Cajas Según Tipo de Cobro"
      Case eCajaRpt_TIPO_COBRO_RESUMIDO: fNombreDelReporteDeCaja = "Cuadre de Cajas Resumido Según Tipo de Cobro"
      Case eCajaRpt_CUADRE_DETALLADO: fNombreDelReporteDeCaja = "Cuadre de Cajas Detallado"
      Case eCajaRpt_CUADRE_CAJA_RESUMIDO_DETALLADO: fNombreDelReporteDeCaja = "Cuadre de Cajas Resumido por Detalle"
      Case eCajaRpt_LISTADO_CAJAS_ABIERTAS: fNombreDelReporteDeCaja = "Informe de Cajas Abiertas"
      Case eCajaRpt_CUADRE_CAJA_POR_TIPO_COBRO_Y_USUARIO: fNombreDelReporteDeCaja = "Cuadre de Caja por Tipo de Cobro y Usuario"
      Case ecajarpt_CUADRE_CAJA_RESUMEN_ANTICIPO_CAJA: fNombreDelReporteDeCaja = "Cuadre de Caja Resumido con Anticipo Caja"
      Case ecajarpt_CUADRE_CAJA_X_USUARIO: fNombreDelReporteDeCaja = "Cuadre de Cajas General por Cajero"
      Case Else: fNombreDelReporteDeCaja = ""
   End Select
End Function

Public Function fConfiguraElInformeDeCuadreDeCaja(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valResumenAlicuota As String, ByVal valResumenTipoCobro As String, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, _
                  ByVal valSqlDelResumenAlicuota As String, ByVal valSqlDelResumenTipoDeCobro As String, ByVal valMensajesDeMonedaParaInformes As String, ByVal valAgruparPorCajero As Boolean) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim rptSubReport As ActiveReport
   On Error GoTo h_ERROR
   exito = False
   If Not valAgruparPorCajero Then
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA)
   Else
      nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(ecajarpt_CUADRE_CAJA_X_USUARIO)
   End If
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
         "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaTotalPagos", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucion", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "Grupo"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSubtotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSubtotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubtotalDevolucion", "", "Vuelto"
       
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaGeneral", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoGeneral", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionGeneral", "", "Vuelto"
       
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaMoneda", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoMoneda", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionMoneda", "", "Vuelto"
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaTotal", "", "Moneda"
         
         gUtilReports.sAsignaAlturaDeSeccion refRpt, "GFCaja", 450
       
         If Not valCambioABs Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblBorderGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoTotalFacturaGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoTotalCanceladoGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalDevolucionGeneral", False
         End If
         
         If valResumenAlicuota = vbChecked Then
            gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportCuadreCajaResumenesAlicuota(valSqlDelResumenAlicuota), "srptSubCuadreDeCajaResumenesAlicuota"
         End If
         If valAgruparPorCajero Then
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCajero", "", "NombreOperador"
            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCajero", "NombreOperador", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPAfter
         End If
         If valResumenTipoCobro = vbChecked Then
            gUtilReports.sAsignaAlturaDeSeccion refRpt, "GFCaja", 2925
               
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblResumenTipoDeCobro", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaTotalPagos", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblEfectivo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtEfectivoTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTargeta", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTarjetaTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCheque", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtChequeTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblDepósito", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtDepositoTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalesTipoDeCobro", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalTiposDeCobro", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblAnticipo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtAnticipoTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalGeneralMasAnticipo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalGeneral", True
            
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtEfectivoTotal", "Efectivo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTarjetaTotal", "Tarjeta", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtChequeTotal", "Cheque", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtDepositoTotal", "Deposito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTiposDeCobro", "TotalSinAnticipo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtAnticipoTotal", "Anticipo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalGeneral", "TotalConAnticipo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            
            'gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportCuadreCajaResumenesTipoDeCobro(valSqlDelResumenTipoDeCobro), "srptSubCuadreDeCajaResumenesTipoDeCobro"
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCaja = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCaja", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCuadreDeCajaResumido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valResumenAlicuota As String, ByVal valResumenTipoCobro As String, ByVal valCambioABs As Boolean, _
                  ByVal valNombreCompaniaParaInformes As String, ByVal valSqlDelResumenAlicuota As String, ByVal valSqlDelResumenTipoDeCobro As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean 'ByVal valSqlDelResumenAnticipoCaja As String,
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim rptSubReport As ActiveReport
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_RESUMIDO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
         "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
         
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucion", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtTipo", "", "Grupo"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSubtotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSubtotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtSubtotalDevolucion", "", "Vuelto"
       
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaGeneral", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoGeneral", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionGeneral", "", "Vuelto"
       
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaMoneda", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoMoneda", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionMoneda", "", "Vuelto"
         
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSubTotalAnticipo", "", "MontoFactura"

         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaTotal", "", "Moneda"
         
         gUtilReports.sAsignaAlturaDeSeccion refRpt, "GFCaja", 450
         
         'gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubCuadreDeCajaResumenAnticipoCaja(valSQL), "srptSubCuadreDeCajaResumenAnticipoCaja"

          If Not valCambioABs Then
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblBorderGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoTotalFacturaGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMontoTotalCanceladoGeneral", False
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalDevolucionGeneral", False
         End If
         
         If valResumenAlicuota = vbChecked Then
            gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportCuadreCajaResumenesAlicuota(valSqlDelResumenAlicuota), "srptSubCuadreDeCajaResumenesAlicuota"
         End If
         If valResumenTipoCobro = vbChecked Then
            gUtilReports.sAsignaAlturaDeSeccion refRpt, "GFCaja", 2925
                     
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblResumenTipoDeCobro", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtMonedaTotalPagos", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblEfectivo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtEfectivoTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTargeta", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTarjetaTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblCheque", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtChequeTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblDepósito", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtDepositoTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalesTipoDeCobro", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalTiposDeCobro", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblAnticipo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtAnticipoTotal", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "lblTotalGeneralMasAnticipo", True
            gUtilReports.sConfigurationForNotvisibleOrVisible refRpt, "txtTotalGeneral", True
            
            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMonedaTotalPagos", "", "Moneda"
            
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtEfectivoTotal", "Efectivo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTarjetaTotal", "Tarjeta", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtChequeTotal", "Cheque", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtDepositoTotal", "Deposito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTiposDeCobro", "TotalSinAnticipo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtAnticipoTotal", "Anticipo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
            gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalGeneral", "TotalConAnticipo", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL

            'gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportCuadreCajaResumenesTipoDeCobro(valSqlDelResumenTipoDeCobro), "srptSubCuadreDeCajaResumenesTipoDeCobro"
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaResumido = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobro(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_TIPO_COBRO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
'         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEfectivo", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTarjeta", "", "Tarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCheque", "", "Cheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDeposito", "", "Deposito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalEfectivo", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalTarjeta", "", "Tarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCheque", "", "Cheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDeposito", "", "Deposito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucion", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtNotaCredito", "", "NotaCredito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCredito", "", "Credito"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalNotaCredito", "NotaCredito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCredito", "Credito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobro = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobroResumido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_TIPO_COBRO_RESUMIDO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
 '        gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEfectivo", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTarjeta", "", "Tarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCheque", "", "Cheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDeposito", "", "Deposito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalEfectivo", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalTarjeta", "", "Tarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCheque", "", "Cheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDeposito", "", "Deposito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucion", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalEfectivoCajas", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalTarjetaCajas", "", "Tarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalChequeCajas", "", "Cheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDepositoCajas", "", "Deposito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaCajas", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoCajas", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionCajas", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalNotaCredito", "NotaCredito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCredito", "Credito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalNotaCreditoMoneda", "NotaCredito", eSF_SUM, "GHEncabezado", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCreditoMoneda", "Credito", eSF_SUM, "GHEncabezado", eSR_GROUP, eST_SUB_TOTAL
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobroResumido = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobroResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCuadreDeCajaDetallado(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valResumen As String, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valSqlDelResumenFormaDePago As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
                  Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim rptSubReport As ActiveReport
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_DETALLADO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
         'gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoSubPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtFormaDePago", "", "FormaDePago"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", ""
         If valResumen = vbChecked Then
            gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportCuadreCajaResumenesFormaDePago(valSqlDelResumenFormaDePago), "srptSubCuadreDeCajaResumenesFormaDePago"
         End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaDetallado = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCuadreDeCajaDetalladoResumido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valResumen As String, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valSqlDelResumenFormaDePago As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_RESUMIDO_DETALLADO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
'         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalPagosPorDescripcion", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtDescripcion", "", "FormaDePago"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
            If valResumen = vbChecked Then
               gUtilReports.fAsignaSubReporteSiExite refRpt, GetSubReportCuadreCajaResumenesFormaDePago(valSqlDelResumenFormaDePago), "srptSubCuadreDeCajaResumenesFormaDePago"
            End If
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaDetalladoResumido = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaDetalladoResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetSubReportCuadreCajaResumenesAlicuota(ByVal valSqlDelReporte As String) As DDActiveReports2.ActiveReport
   Dim rptSubReport As DDActiveReports2.ActiveReport
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   Set rptSubReport = New ActiveReport
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_CON_RESUMENES_ALICUOTA)
   If gUtilReports.fLoadLayout(rptSubReport, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(rptSubReport, "dcOrigenData", valSqlDelReporte) Then
            'gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
            gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtMoneda", "", "Moneda"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoExento", "", "MontoExento"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaGeneral", "", "AlicuotaGeneral"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaReducida", "", "AlicuotaReducida"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaAdicional", "", "AlicuotaAdicional"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaAdicionalDetalle", "", "TotalAlicuotas"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalesMontosTipodeAlicuota", "", "TotalAlicuotas"
            Call rptSubReport.AddNamedItem("gUtilReports", gUtilReports)
      End If
   End If
   Set GetSubReportCuadreCajaResumenesAlicuota = rptSubReport
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetSubReportCuadreCajaResumenesAlicuota", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Public Function GetSubCuadreDeCajaResumenAnticipoCaja(ByVal valSqlDelReporte As String) As DDActiveReports2.ActiveReport
'   Dim rptSubReport As DDActiveReports2.ActiveReport
'   Dim nombreDelFileXML As String
'   On Error GoTo h_ERROR
'   Set rptSubReport = New ActiveReport
'   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(ecajarpt_CUADRE_CAJA_RESUMEN_ANTICIPO_CAJA)
'   If gUtilReports.fLoadLayout(rptSubReport, nombreDelFileXML) Then
'      If gUtilReports.fDefaultConfigurationForDataControl(rptSubReport, "dcOrigenData", valSqlDelReporte) Then
'            gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
'            gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtMoneda", "", "Moneda"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoExento", "", "MontoExento"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaGeneral", "", "AlicuotaGeneral"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaReducida", "", "AlicuotaReducida"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaAdicional", "", "AlicuotaAdicional"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoAlicuotaAdicionalDetalle", "", "TotalAlicuotas"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalesMontosTipodeAlicuota", "", "TotalAlicuotas"
'            gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
'            gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtCaja", "", "NombreCaja"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoSubtotalFactura", "", "MontoTotal"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoSubtotalCancelado", "", "MontoTotal"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtSubtoalDevolucion", "", ""
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoTotalFactura", "", "MontoTotal"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtMontoTotalCancelado", "", "MontoTotal"
'            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalDevolucion", "", ""
'            gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter rptSubReport, "GHCaja", "txtNumeroConsecutivoDeCaja", ddGrpAll, ddRepeatAll, True, ddNPNone
'            Call rptSubReport.AddNamedItem("gUtilReports", gUtilReports)
'      End If
'   End If
'   Set GetSubCuadreDeCajaResumenAnticipoCaja = rptSubReport
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "GetSubReportCuadreCajaResumenesAlicuota", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function

Public Function GetSubReportCuadreCajaResumenesTipoDeCobro(ByVal valSqlDelReporte As String) As DDActiveReports2.ActiveReport
   Dim rptSubReport As DDActiveReports2.ActiveReport
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   Set rptSubReport = New ActiveReport
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_CON_RESUMENES_TIPO_COBRO)
   If gUtilReports.fLoadLayout(rptSubReport, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(rptSubReport, "dcOrigenData", valSqlDelReporte) Then
            'gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
            gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtMoneda", "", "Moneda"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtEfectivo", "", "Efectivo"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTarjeta", "", "Tarjeta"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtCheque", "", "Cheque"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtEfectivoTotal", "", "Efectivo"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTarjetaTotal", "", "Tarjeta"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtChequeTotal", "", "Cheque"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtDeposito", "", "Deposito"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtDepositoTotal", "", "Deposito"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalTiposDeCobro", "", "TotalesTiposDeCobro"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtAnticipo", "", "Anticipo"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtAnticipoTotal", "", "Anticipo"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalGeneral", "", "TotalesTiposDeCobroGeneral"
            Call rptSubReport.AddNamedItem("gUtilReports", gUtilReports)
      End If
   End If
   Set GetSubReportCuadreCajaResumenesTipoDeCobro = rptSubReport
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetSubReportCuadreCajaResumenesTipoDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetSubReportCuadreCajaResumenesFormaDePago(ByVal valSqlDelReporte As String) As DDActiveReports2.ActiveReport
   Dim rptSubReport As DDActiveReports2.ActiveReport
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   Set rptSubReport = New DDActiveReports2.ActiveReport
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_CON_RESUMENES_FORMA_PAGO)
   If gUtilReports.fLoadLayout(rptSubReport, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(rptSubReport, "dcOrigenData", valSqlDelReporte) Then
            gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtMoneda", "", "Moneda"
            gUtilReports.sDefaultConfigurationForStrFields rptSubReport, "txtDescripcion", "", "FormaDePago"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalPagosPorDescripcion", "", "MontoPagado"
            gUtilReports.sDefaultConfigurationForNumericFields rptSubReport, "txtTotalesMontosFormasDePago", "", "MontoPagado"
            Call rptSubReport.AddNamedItem("gUtilReports", gUtilReports)
      End If
   End If
   Set GetSubReportCuadreCajaResumenesFormaDePago = rptSubReport
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "GetSubReportCuadreCajaResumenesFormaDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeListadoCajasAbiertas(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                   ByVal valNombreCompaniaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_LISTADO_CAJAS_ABIERTAS)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         'gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
         "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         '''****'''
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCaja", "", "consecutivocaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreUsuario", "", "nombredelusuario"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFechaApertura", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoApertura", "", "montoapertura"
         gUtilMargins.sAsignarMargenesGenerales refRpt, , fNombreDelReporteDeCaja(eCajaRpt_LISTADO_CAJAS_ABIERTAS)
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeListadoCajasAbiertas = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeListadoCajasAbiertas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fConfiguraElInformeDeCuadreDeCajaPorUsuario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, _
                  ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim rptSubReport As ActiveReport
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_POR_USUARIO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
         "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCajero", "", "NombreDelUsuario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucion", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaUsuario", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoUsuario", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionUsuario", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHCajero", "NombreDelUsuario", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaPorUsuario = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaPorUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCuadreDeCajaPorUsuarioResumido(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, _
                  ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   Dim rptSubReport As ActiveReport
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_POR_USUARIO_RESUMIDO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
         "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCajero", "", "NombreDelUsuario"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoPagado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCancelado", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucion", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaCajasSum", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoCajasSum", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionCajasSum", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFacturaCajas", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCanceladoCajas", "", "MontoPagado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucionCajas", "", "Vuelto"
         gUtilMargins.sAsignarMargenesGenerales refRpt
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaPorUsuarioResumido = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaPorUsuarioResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobroYUsuario(ByRef refRpt As DDActiveReports2.ActiveReport, ByVal valSQL As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCambioABs As Boolean, ByVal valNombreCompaniaParaInformes As String, ByVal valMensajesDeMonedaParaInformes As String) As Boolean
   Dim exito As Boolean
   Dim nombreDelFileXML As String
   On Error GoTo h_ERROR
   exito = False
   nombreDelFileXML = fNombreFileDeImpresionDelReporteDeCaja(eCajaRpt_CUADRE_CAJA_POR_TIPO_COBRO_Y_USUARIO)
   If gUtilReports.fLoadLayout(refRpt, nombreDelFileXML) Then
      If gUtilReports.fDefaultConfigurationForDataControl(refRpt, "dcOrigenData", valSQL) Then
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblCompania", valNombreCompaniaParaInformes
         gUtilReports.sDefaultValueForLabelHoraYFechaDeEmision refRpt, "lblFechaYHoraDeEmision"
         gUtilReports.sDefaultConfigurationForLabels refRpt, "lblFechaInicialYFinal", "del " & gConvert.dateToString(valFechaInicial) & " al " & gConvert.dateToString(valFechaFinal)
         gUtilReports.sConfiguraEncabezado refRpt, "lblCompania", "lblFechaYHoraDeEmision", "lblTituloDelReporte", _
            "lblNumeroDePagina", "lblFechaInicialYFinal", gProyParametros.GetImprimirNoPagina, gProyParametros.GetImprimirFechaDeEmision
         If valCambioABs Then
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", valMensajesDeMonedaParaInformes
         Else
            gUtilReports.sDefaultConfigurationForLabels refRpt, "lblNotaDeCambio", ""
         End If
'         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtConsecutivoCompania", "", "ConsecutivoCompania"
      
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtMoneda", "", "Moneda"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCajero", "", "NombreOperador"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroConsecutivoDeCaja", "", "ConsecutivoCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtCaja", "", "NombreCaja"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumero", "", "Numero"
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNumeroComprobanteFiscal", "", "NumeroComprobanteFiscal"
         gUtilReports.sDefaultConfigurationForDateFields refRpt, "txtFecha", "", "Fecha", eFT_DATE
         gUtilReports.sDefaultConfigurationForStrFields refRpt, "txtNombreCliente", "", "NombreCliente"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtEfectivo", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTarjeta", "", "Tarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCheque", "", "Cheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtDeposito", "", "Deposito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAnticipoUsado", "", "AnticipoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtAnticipoRestante", "", "AnticipoRestante"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoCobrado", "", "MontoCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtVuelto", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalEfectivo", "", "Efectivo"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalTarjeta", "", "Tarjeta"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalCheque", "", "Cheque"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDeposito", "", "Deposito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalAnticipoUsado", "", "AnticipoUsado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalAnticipoRestante", "", "AnticipoRestante"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalFactura", "", "MontoFactura"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtMontoTotalCobrado", "", "MontoCobrado"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtTotalDevolucion", "", "Vuelto"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtNotaCredito", "", "NotaCredito"
         gUtilReports.sDefaultConfigurationForNumericFields refRpt, "txtCredito", "", "Credito"
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalNotaCredito", "NotaCredito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCredito", "Credito", eSF_SUM, "GHCaja", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotalFacturaXCajero", "MontoFactura", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalEfectivoXCajero", "Efectivo", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalTarjetaXCajero", "Tarjeta", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalChequeXCajero", "Cheque", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDepositoXCajero", "Deposito", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalAnticipoUsadoXCajero", "AnticipoUsado", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalAnticipoRestanteXCajero", "AnticipoRestante", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtMontoTotalCobradoXCajero", "MontoCobrado", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalDevolucionXCajero", "Vuelto", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalNotaCreditoXCajero", "NotaCredito", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         gUtilReports.sDefaultConfigurationForSummaryFields refRpt, "txtTotalCreditoXCajero", "Credito", eSF_SUM, "GHUsuario", eSR_GROUP, eST_SUB_TOTAL
         
         gUtilReports.sDefaultConfigurationForGroupHeaderOrFooter refRpt, "GHUsuario", "NombreOperador", ddGrpFirstDetail, ddRepeatOnPage, True, ddNPNone
         gUtilMargins.sAsignarMargenesGenerales refRpt, ddOLandscape
         Call refRpt.AddNamedItem("gUtilReports", gUtilReports)
         exito = True
      End If
   End If
h_EXIT:
   fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobroYUsuario = exito
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConfiguraElInformeDeCuadreDeCajaPorTipoDeCobroYUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


