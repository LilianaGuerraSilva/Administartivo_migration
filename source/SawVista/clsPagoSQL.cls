VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPagoSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsPagoSQL"
Private Const CM_MESSAGE_NAME As String = "Pago SQL"
Private insFactura As Object
Private insRenglonFact As Object
Private inPago As Object
Private gUltimaTasaDeCambio As Object
Private gMonedaLocalActual As Object
Private gListLibrary As Object
Private mUsaMonedaExtranjera As Boolean
Private mUsaMultiplesAlicuotas As Boolean
Private Enum ePagoOPT
   ePagoOPT_PAGOS_ENTRE_FECHAS '0
   ePagoOPT_RET_EN_PAGOS '1
   ePagoOPT_RET_POR_CONCEPTO
   ePagoOPT_RET_MENSUALES_POR_TIPOPERSONA
   ePagoOPT_BORRADOR_DE_PLANILLA_DE_DECLARACION_Y_PAGO
   ePagoOPT_DECLARACION_INFORMATIVA '5
   ePagoOPT_COMPROBANTE_RESUMEN_IVA
   ePagoOPT_LIBRO_RETENCIONES_RENTA
   ePagoOPT_TIPO_SALARIOS_OTROS
   ePagoOPT_TIPO_DIVIDENDOS_ACCIONES
   ePagoOPT_TIPO_GANANCIAS_FORTUITAS '10
   ePagoOPT_TIPO_INFORME_PROVEEDORES
   ePagoOPT_TIPO_INFORME_PAGOS '12
End Enum
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function CM_DecInf_Compras_Desde_CxP() As Integer
   CM_DecInf_Compras_Desde_CxP = 1
End Function

Private Function CM_DecInf_Ventas_Desde_Factura() As Integer
   CM_DecInf_Ventas_Desde_Factura = 2
End Function

Private Function CM_DecInf_Ventas_Desde_CxC() As Integer
   CM_DecInf_Ventas_Desde_CxC = 3
End Function

Private Function CM_DecInf_Ventas_Desde_Resumen_Ventas() As Integer
   CM_DecInf_Ventas_Desde_Resumen_Ventas = 4
End Function

Private Function CM_TOE_Compras() As Integer
   CM_TOE_Compras = 0
End Function

Private Function CM_TOE_ComprasYVentas() As Integer
   CM_TOE_ComprasYVentas = 1
End Function

Private Function CM_QDI_PrimeraQuincena() As Integer
   CM_QDI_PrimeraQuincena = 0
End Function

Private Function CM_QDI_SegundaQuincena() As Integer
   CM_QDI_SegundaQuincena = 1
End Function

Private Function CM_QDI_TodoElMes() As Integer
   CM_QDI_TodoElMes = 2
End Function

Public Function fConstruirSQLDelReportePagosEntreFechas(ByVal valAgruparPorProveedor As Boolean, ByVal usarCambioOriginal As Boolean, _
                                          ByVal ReporteEnMonedaLocal As Boolean, ByVal UsaModuloDeContabilidad As Boolean, _
                                          ByVal valConsecutivoCompania As String, ByVal valFechaInicial As Date, _
                                          ByVal valFechaFinal As Date, ByVal CantidadAImprimirUno As Boolean, _
                                          ByVal valCodigoDeProveedor As String, ByVal valUsarCodigoProveedorEnPantalla As Boolean, _
                                          ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                          ByVal MostrarObservaciones As Boolean) As String
   Dim vSql As String
   Dim vSqlTotalOtros As String
   Dim vSqlTotalRetenidoISLR As String
   Dim vSqlTotalRetenidoIVA As String
   Dim vSqlTotalRetImpMunicipal As String
   Dim vSqlTotalCheque As String
   Dim vSqlTotalAnticipo As String
   Dim vSqlCambio As String
   
   Dim vSQLMoneda As String
   Dim vSqlCodigoMoneda As String
   Dim vSqlCodigoMonedaIgualMonedaLocal As String
   Dim vSqlCodigoMonedaIgualMonedaLocalAnt As String
   Dim vSqlCodigoMonedaEsLocal As String
   
   Dim vSqlNroComprobanteContable As String
   On Error GoTo h_ERROR
   vSqlTotalOtros = "Pago.TotalOtros"
   vSqlTotalRetenidoISLR = "Pago.TotalRetenido"
   vSqlTotalRetenidoIVA = "Pago.TotalRetenidoIVA"
   vSqlTotalRetImpMunicipal = "Pago.TotalRetenidoImpuestoMunicipal"
   vSqlTotalCheque = "Pago.MontoCheque"
   vSqlTotalAnticipo = "Pago.PagadoAnticipo"
   
   vSQLMoneda = "Pago.Moneda"
   vSqlCambio = "Pago.CambioaBolivares"
   vSqlCodigoMoneda = "Pago.CodigoMoneda"
   
   vSqlCodigoMonedaIgualMonedaLocal = vSqlCodigoMoneda & " = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda)
   vSqlCodigoMonedaIgualMonedaLocalAnt = vSqlCodigoMoneda & " = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt)
   vSqlCodigoMonedaEsLocal = vSqlCodigoMonedaIgualMonedaLocal & " OR " & vSqlCodigoMonedaIgualMonedaLocalAnt
   
   If ReporteEnMonedaLocal Then
      If usarCambioOriginal Then
         vSqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlCambio, "Pago.Fecha")
      Else
         vSqlCambio = gUtilSQL.getIIF(vSqlCodigoMonedaEsLocal, gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlCambio, "Pago.Fecha"), gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", vSQLMoneda, 1, usarCambioOriginal, ""), True)
      End If
      
      vSqlTotalOtros = gUtilSQL.getIIF(vSqlCodigoMonedaEsLocal, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlTotalOtros, "Pago.Fecha"), gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSqlCambio, vSQLMoneda, vSqlTotalOtros, usarCambioOriginal, ""), True)
      vSqlTotalRetenidoISLR = gUtilSQL.getIIF(vSqlCodigoMonedaEsLocal, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlTotalRetenidoISLR, "Pago.Fecha"), gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSqlCambio, vSQLMoneda, vSqlTotalRetenidoISLR, usarCambioOriginal, ""), True)
      vSqlTotalRetenidoIVA = gUtilSQL.getIIF(vSqlCodigoMonedaEsLocal, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlTotalRetenidoIVA, "Pago.Fecha"), gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSqlCambio, vSQLMoneda, vSqlTotalRetenidoIVA, usarCambioOriginal, ""), True)
      vSqlTotalRetImpMunicipal = gUtilSQL.getIIF(vSqlCodigoMonedaEsLocal, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlTotalRetImpMunicipal, "Pago.Fecha"), gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSqlCambio, vSQLMoneda, vSqlTotalRetImpMunicipal, usarCambioOriginal, ""), True)
      vSqlTotalCheque = gUtilSQL.getIIF(vSqlCodigoMonedaEsLocal, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlTotalCheque, "Pago.Fecha"), gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSqlCambio, vSQLMoneda, vSqlTotalCheque, usarCambioOriginal, ""), True)
      vSqlTotalAnticipo = gUtilSQL.getIIF(vSqlCodigoMonedaEsLocal, gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, vSqlTotalAnticipo, "Pago.Fecha"), gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(vSqlCambio, vSQLMoneda, vSqlTotalAnticipo, usarCambioOriginal, ""), True)
                                    
      vSQLMoneda = gUtilSQL.getIIF(vSqlCodigoMonedaIgualMonedaLocal, "Pago.Moneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True)
   End If
   If UsaModuloDeContabilidad Then
      vSqlNroComprobanteContable = fSQLNumeroComprobanteContablePago(valConsecutivoCompania, valFechaInicial, valFechaFinal, "Pago.NumeroComprobante", "Pago.Fecha")
   End If
   
   vSql = "SELECT "
   vSql = vSql & "Pago.NumeroComprobante, "
   vSql = vSql & "Pago.NumeroCheque, "
   vSql = vSql & "Pago.Fecha, "
   vSql = vSql & vSQLMoneda & " As Moneda, "
   vSql = vSql & vSqlCambio & " AS Cambio, "
   vSql = vSql & "Pago.CodigoProveedor, "
   vSql = vSql & "Proveedor.NombreProveedor, "
   vSql = vSql & vSqlTotalOtros & " AS TotalOtros, "
   vSql = vSql & vSqlTotalRetenidoISLR & " AS TotalRetenido, "
   vSql = vSql & vSqlTotalRetenidoIVA & " AS TotalRetenidoIVA, "
   vSql = vSql & vSqlTotalRetImpMunicipal & " AS TotalRetImpMunicipal, "
   vSql = vSql & vSqlTotalCheque & " AS MontoCheque,"
   vSql = vSql & vSqlTotalAnticipo & " AS TotalAnticipo, "
   vSql = vSql & "Pago.DescripcionPago  AS DescripcionPago"
   If UsaModuloDeContabilidad Then
      vSql = vSql & ", ISNULL(" & vSqlNroComprobanteContable & ", 'N/A')  AS ComprobanteContable"
   End If
   vSql = vSql & " FROM Pago INNER JOIN Proveedor "
   vSql = vSql & " ON Pago.ConsecutivoCompania = Proveedor.ConsecutivoCompania "
   vSql = vSql & " AND Pago.CodigoProveedor = Proveedor.CodigoProveedor "
   vSql = vSql & " WHERE Pago.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND " & gUtilSQL.DfSQLDateValueBetween("Pago.Fecha", valFechaInicial, valFechaFinal)
   vSql = vSql & " AND Pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   If CantidadAImprimirUno Then
      vSql = vSql & " AND Proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(valCodigoDeProveedor)
   End If
   vSql = vSql & " ORDER BY Pago.Moneda, "
   If valAgruparPorProveedor Then
      If valUsarCodigoProveedorEnPantalla Then
         vSql = vSql & "Pago.CodigoProveedor, Proveedor.NombreProveedor, "
      Else
         vSql = vSql & "Proveedor.NombreProveedor, Pago.CodigoProveedor, "
      End If
   End If
   vSql = vSql & "Pago.Fecha, Pago.NumeroCheque"
   
   fConstruirSQLDelReportePagosEntreFechas = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: vSql = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReportePagosEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLNumeroComprobanteContablePago(ByVal valConsecutivoCompania As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNumeroPago As String, ByVal valFechaPago As String) As String
   Dim Sql As String
   Dim SqlPeriodo As String
   On Error GoTo h_ERROR
   Sql = "SELECT PERIODO.ConsecutivoPeriodo FROM PERIODO WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(Sql, False)
   
   Sql = "(SELECT COMPROBANTE.Numero FROM COMPROBANTE"
   Sql = Sql & " WHERE COMPROBANTE.NoDocumentoOrigen = " & valNumeroPago
   Sql = Sql & " AND COMPROBANTE.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_ComprobanteGeneradoPor.eCG_PAGOS)
   Sql = Sql & " AND COMPROBANTE.ConsecutivoPeriodo IN (" & SqlPeriodo & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(valFechaPago, valFechaInicial, valFechaFinal) & ")"
   fSQLNumeroComprobanteContablePago = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLNumeroComprobanteContablePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRetencionesPorMesAnoTipoDePersona(ByVal valConsecutivoCompania As String, _
                                                       ByVal valtxtMes As String, _
                                                        ByVal valtxtAno As String, _
                                                         ByVal valCmbTipoDePersona As String, _
                                                          ByVal gUltimaTasaDeCambio As Object, _
                                                           ByVal gMonedaLocalActual As Object) As String
   Dim gEnumTablaRetencion As clsEnumTablaRetencion
   Dim SQL As String
   Dim sqlChequeComprobante As String
   Dim sqlMontoOriginal As String
   On Error GoTo h_ERROR
   Set gEnumTablaRetencion = New clsEnumTablaRetencion
   sqlChequeComprobante = gUtilSQL.fCast("RetPago.NumeroReferencia", eTDSS_VARCHAR, "") & _
               gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" / ") & gUtilSQL.CharConcat & _
               gUtilSQL.fCast("RetPago.NroComprobanteRetISLR", eTDSS_VARCHAR, "")
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Proveedor.TipoDePersona", enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA, _
               gEnumTablaRetencion.fEnumTipoDePersonaRetencionSiglasToStringInArray(True), "TipoDePersona") & ", "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion, "
   SQL = SQL & "Proveedor.NumeroRIF, "
   SQL = SQL & "Proveedor.NombreProveedor, "
   SQL = SQL & "RetPago.Fecha, "
   SQL = SQL & sqlChequeComprobante & " AS ChequeComprobante, "
   sqlMontoOriginal = "retDocumentoPagado.MontoOriginal"
   SQL = SQL & "SUM(" & sqlMontoOriginal & ") AS MontoOriginal, "
   SQL = SQL & "SUM( retDocumentoPagado.MontoBaseImponible) AS MontoBaseImponible, "
   SQL = SQL & "SUM( retDocumentoPagado.MontoRetencion) AS MontoRetencion,"
   SQL = SQL & "CxP.Fecha AS FechaCxP,"
   SQL = SQL & "CxP.Numero AS NumeroCxP,"
   SQL = SQL & "CxP.NumeroControl AS NumControlCxP"
   SQL = SQL & " FROM (Proveedor INNER JOIN RetPago"
   SQL = SQL & " ON (Proveedor.CodigoProveedor = RetPago.CodigoProveedor)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = RetPago.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN retDocumentoPagado ON (RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " AND (RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania)"
   SQL = SQL & "INNER JOIN " & _
               "cxP ON retDocumentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania " & _
               "AND retDocumentoPagado.NumeroDelDocumento = cxP.Numero " & _
               "AND retPago.CodigoProveedor = cxP.CodigoProveedor "
   SQL = SQL & " WHERE RetPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RetPago.MesAplicacion = " & Trim(valtxtMes)
   SQL = SQL & " AND RetPago.AnoAplicacion = " & Trim(valtxtAno)
   SQL = SQL & " AND retDocumentoPagado.montoretencion <> 0"
   If valCmbTipoDePersona <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND Proveedor.TipoDePersona = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumTablaRetencion.strTipodePersonaRetencionToNum(valCmbTipoDePersona, False))
   End If
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " GROUP BY "
   SQL = SQL & sqlChequeComprobante & ", "
   SQL = SQL & "RetPago.Fecha, Proveedor.NombreProveedor, Proveedor.NumeroRIF, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Proveedor.TipoDePersona", enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA, _
               gEnumTablaRetencion.fEnumTipoDePersonaRetencionSiglasToStringInArray(True), "") & ", "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion, CxP.Fecha, CxP.Numero, CxP.NumeroControl "
   ''******* UNION PARA BUSCAR LAS RETECIONES PERTENCIENTES A LAS CXP POR CUENTAS DE TERCEROS
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Proveedor.TipoDePersona", enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA, _
               gEnumTablaRetencion.fEnumTipoDePersonaRetencionSiglasToStringInArray(True), "TipoDePersona") & ", "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion, "
   SQL = SQL & "Proveedor.NumeroRIF, "
   SQL = SQL & "Proveedor.NombreProveedor, "
   SQL = SQL & "RetPago.Fecha, "
   SQL = SQL & sqlChequeComprobante & " AS ChequeComprobante, "
   sqlMontoOriginal = "retDocumentoPagado.MontoOriginal"
   SQL = SQL & "SUM(" & sqlMontoOriginal & ") AS MontoOriginal, "
   SQL = SQL & "SUM( retDocumentoPagado.MontoBaseImponible) AS MontoBaseImponible, "
   SQL = SQL & "SUM( retDocumentoPagado.MontoRetencion) AS MontoRetencion,"
   SQL = SQL & "CxP.Fecha AS FechaCxP,"
   SQL = SQL & "CxP.Numero AS NumeroCxP,"
   SQL = SQL & "CxP.NumeroControl AS NumControlCxP"
   SQL = SQL & " FROM (Proveedor INNER JOIN RetPago"
'   SQL = SQL & " ON (Proveedor.CodigoProveedor = RetPago.CodigoProveedor)"
'   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = RetPago.ConsecutivoCompania))"
   SQL = SQL & " ON (Proveedor.ConsecutivoCompania = RetPago.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN retDocumentoPagado"
   SQL = SQL & " ON (RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " AND (RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania)"
   SQL = SQL & "INNER JOIN " & _
               " cxP ON retDocumentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania " & _
               " AND (Proveedor.CodigoProveedor = retDocumentoPagado.CodigoProveedorOriginalServicio) " & _
               " AND retDocumentoPagado.NumeroDelDocumento = cxP.Numero " & _
               " AND retDocumentoPagado.CodigoProveedorOriginalServicio = cxP.CodigoProveedorOriginalServicio "
   SQL = SQL & " WHERE RetPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RetPago.MesAplicacion = " & Trim(valtxtMes)
   SQL = SQL & " AND RetPago.AnoAplicacion = " & Trim(valtxtAno)
   SQL = SQL & " AND retDocumentoPagado.montoretencion <> 0"
   If valCmbTipoDePersona <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND Proveedor.TipoDePersona = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumTablaRetencion.strTipodePersonaRetencionToNum(valCmbTipoDePersona, False))
   End If
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " GROUP BY "
   SQL = SQL & sqlChequeComprobante & ", "
   SQL = SQL & "RetPago.Fecha, Proveedor.NombreProveedor, Proveedor.NumeroRIF, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Proveedor.TipoDePersona", enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA, _
               gEnumTablaRetencion.fEnumTipoDePersonaRetencionSiglasToStringInArray(True), "") & ", "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion, CxP.Fecha, CxP.Numero, CxP.NumeroControl "
   
   SQL = SQL & " HAVING SUM(retDocumentoPagado.MontoRetencion) <> 0 "
   SQL = SQL & " ORDER BY RetPago.Fecha"
h_EXIT: On Error GoTo 0
   fSQLRetencionesPorMesAnoTipoDePersona = SQL
   Set gEnumTablaRetencion = Nothing
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLRetencionesPorMesAnoTipoDePersona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteRetXConcepto(ByVal valConsecutivoCompania As String, _
                                                     ByVal valtxtMes As String, _
                                                      ByVal valtxtAno As String, _
                                                       ByVal valtxtCodigoDeRetencion As String, _
                                                        ByVal valCantidadAImprimirUno As Boolean, _
                                                         ByVal valtxtCodigoDeProveedor As String, _
                                                          ByVal valCmbTipoDePersona As String, _
                                                           ByVal gUltimaTasaDeCambio As Object, _
                                                            ByVal gMonedaLocalActual As Object, _
                                                             ByVal valFechaInicioVigencia As Date) As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim sqlMontoOriginal As String
   Dim SQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = "SELECT Proveedor.NombreProveedor, "
   SQL = SQL & "Proveedor.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NumeroRIF, "
   SQL = SQL & "RetPago.Fecha, "
   SQL = SQL & "RetPago.NumeroReferencia, "
   SQL = SQL & "RetPago.NroComprobanteRetISLR AS Comprobante, "
   sqlMontoOriginal = "retDocumentoPagado.MontoOriginal"
   SQL = SQL & sqlMontoOriginal & " AS MontoOriginal, "
   SQL = SQL & "retDocumentoPagado.MontoRetencion, "
   SQL = SQL & "retDocumentoPagado.MontoBaseImponible, "
   SQL = SQL & "retDocumentoPagado.NumeroDelDocumento, "
   SQL = SQL & "TablaRetencion.Tarifa, "
   SQL = SQL & "TablaRetencion.BaseImponible, "
   SQL = SQL & "TablaRetencion.Sustraendo, "
   SQL = SQL & "TablaRetencion.ParaPagosMayoresDe"
   SQL = SQL & " FROM (Proveedor INNER JOIN "
   SQL = SQL & "(retDocumentoPagado INNER JOIN TablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = TablaRetencion.Codigo) ON "
   SQL = SQL & "Proveedor.TipoDePersona  = TablaRetencion.TipoDePersona)"
   SQL = SQL & " INNER JOIN RetPago ON "
   SQL = SQL & "(RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " AND (RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania)"
   SQL = SQL & " AND (Proveedor.CodigoProveedor = RetPago.CodigoProveedor)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = RetPago.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN " & _
               "cxP ON retDocumentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania " & _
               "AND retDocumentoPagado.NumeroDelDocumento = cxP.Numero " & _
               "AND RetPago.CodigoProveedor = Cxp.CodigoProveedor"
   SQL = SQL & " WHERE RetPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RetPago.MesAplicacion = " & Trim(valtxtMes)
   SQL = SQL & " AND RetPago.AnoAplicacion = " & Trim(valtxtAno)
   SQL = SQL & " AND TablaRetencion.Codigo = '" & Trim(valtxtCodigoDeRetencion) & "'"
   SQL = SQL & " AND TablaRetencion.FechaDeInicioDeVigencia = " & gUtilSQL.fSimpleSqlValue(valFechaInicioVigencia)
   If valCantidadAImprimirUno Then
      SQL = SQL & " AND RetPago.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoDeProveedor)
   End If
   If valCmbTipoDePersona <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND Proveedor.TipoDePersona = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strTipodePersonaRetencionToNum(valCmbTipoDePersona, False))
   End If
   SQL = SQL & " AND retDocumentoPagado.MontoRetencion <> 0 "
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT Proveedor.NombreProveedor, "
   SQL = SQL & "Proveedor.CodigoProveedor AS Codigo, "
   SQL = SQL & "Proveedor.NumeroRIF, "
   SQL = SQL & "RetPago.Fecha, "
   SQL = SQL & "RetPago.NumeroReferencia, "
   SQL = SQL & "RetPago.NroComprobanteRetISLR AS Comprobante, "
   sqlMontoOriginal = "retDocumentoPagado.MontoOriginal"
   SQL = SQL & sqlMontoOriginal & " AS MontoOriginal, "
   SQL = SQL & "retDocumentoPagado.MontoRetencion, "
   SQL = SQL & "retDocumentoPagado.MontoBaseImponible, "
   SQL = SQL & "retDocumentoPagado.NumeroDelDocumento, "
   SQL = SQL & "TablaRetencion.Tarifa, "
   SQL = SQL & "TablaRetencion.BaseImponible, "
   SQL = SQL & "TablaRetencion.Sustraendo, "
   SQL = SQL & "TablaRetencion.ParaPagosMayoresDe"
   SQL = SQL & " FROM (Proveedor INNER JOIN "
   SQL = SQL & "(retDocumentoPagado INNER JOIN TablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = TablaRetencion.Codigo) ON "
   SQL = SQL & "Proveedor.TipoDePersona  = TablaRetencion.TipoDePersona)"
   SQL = SQL & " INNER JOIN RetPago ON "
   SQL = SQL & "(RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " AND (RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania)"
   SQL = SQL & " AND (Proveedor.CodigoProveedor = retDocumentoPagado.CodigoProveedorOriginalServicio)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = RetPago.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN " & _
               " cxP ON retDocumentoPagado.ConsecutivoCompania = cxP.ConsecutivoCompania " & _
               " AND retDocumentoPagado.NumeroDelDocumento = cxP.Numero " & _
               " AND retDocumentoPagado.CodigoProveedorOriginalServicio = Cxp.CodigoProveedorOriginalServicio"
   SQL = SQL & " WHERE RetPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RetPago.MesAplicacion = " & Trim(valtxtMes)
   SQL = SQL & " AND RetPago.AnoAplicacion = " & Trim(valtxtAno)
   SQL = SQL & " AND TablaRetencion.Codigo = '" & Trim(valtxtCodigoDeRetencion) & "'"
   SQL = SQL & " AND TablaRetencion.FechaDeInicioDeVigencia = " & gUtilSQL.fSimpleSqlValue(valFechaInicioVigencia)
   If valCantidadAImprimirUno Then
      SQL = SQL & " AND RetPago.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(valtxtCodigoDeProveedor)
   End If
   If valCmbTipoDePersona <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND Proveedor.TipoDePersona = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumProyecto.strTipodePersonaRetencionToNum(valCmbTipoDePersona, False))
   End If
   SQL = SQL & " AND retDocumentoPagado.MontoRetencion <> 0 "
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   
   SQL = SQL & " ORDER BY RetPago.Fecha, RetPago.NumeroReferencia"
   fConstruirSQLDelReporteRetXConcepto = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteRetXConcepto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteRetMenXTipoPersona(ByVal valConsecutivoCompania As String, _
                                                  ByVal valtxtMes As String, _
                                                   ByVal valtxtAno As String, _
                                                    ByVal valCmbTipoDePersona As String) As String

   Dim gEnumTablaRetencion As clsEnumTablaRetencion
   Dim SQL As String
   On Error GoTo h_ERROR
   Set gEnumTablaRetencion = New clsEnumTablaRetencion

   SQL = " SELECT "
   SQL = SQL & "Proveedor.TipoDePersona, "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion AS CodigoRet, "
   SQL = SQL & "COUNT(retPago.NumeroComprobante) AS NumPago, "
   SQL = SQL & "SUM(MontoRetencion) AS TotalRetenido, "
   SQL = SQL & "SUM(MontoBaseImponible) AS TotalImponible "
   SQL = SQL & " FROM Proveedor"
   SQL = SQL & " INNER JOIN (retPago"
   SQL = SQL & " INNER JOIN retDocumentoPagado ON "
   SQL = SQL & "(retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " AND (retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania))"
   SQL = SQL & " ON (Proveedor.CodigoProveedor = retPago.CodigoProveedor)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = retPago.ConsecutivoCompania)"
   SQL = SQL & " WHERE retPago.MesAplicacion = " & Trim(valtxtMes)
   SQL = SQL & " AND retPago.AnoAplicacion = " & Trim(valtxtAno)
   SQL = SQL & " AND retPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND retDocumentoPagado.MontoRetencion <> 0"
   If valCmbTipoDePersona <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND Proveedor.TipoDePersona = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumTablaRetencion.strTipodePersonaRetencionToNum(valCmbTipoDePersona, False))
   End If
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " GROUP BY "
   SQL = SQL & "Proveedor.TipoDePersona, "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion"
   '*****UNION PARA MOSTRAR LAS CXP POR CUENTAS DE TERCERO
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & "Proveedor.TipoDePersona, "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion AS CodigoRet, "
   SQL = SQL & "COUNT(retPago.NumeroComprobante) AS NumPago, "
   SQL = SQL & "SUM(MontoRetencion) AS TotalRetenido, "
   SQL = SQL & "SUM(MontoBaseImponible) AS TotalImponible "
   SQL = SQL & " FROM Proveedor"
   SQL = SQL & " INNER JOIN (retPago"
   SQL = SQL & " INNER JOIN retDocumentoPagado ON "
   SQL = SQL & "(retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " AND (retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania))"
   SQL = SQL & " ON (Proveedor.CodigoProveedor = RetDocumentoPagado.CodigoProveedorOriginalServicio)"
   SQL = SQL & " AND (Proveedor.ConsecutivoCompania = retPago.ConsecutivoCompania)"
   SQL = SQL & " WHERE retPago.MesAplicacion = " & Trim(valtxtMes)
   SQL = SQL & " AND retPago.AnoAplicacion = " & Trim(valtxtAno)
   SQL = SQL & " AND retPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND retDocumentoPagado.MontoRetencion <> 0"
   If valCmbTipoDePersona <> gDefgen.getSearchAllComboBox Then
      SQL = SQL & " AND Proveedor.TipoDePersona = " & gUtilSQL.fSQLSimpleValueForEnum(gEnumTablaRetencion.strTipodePersonaRetencionToNum(valCmbTipoDePersona, False))
   End If
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " GROUP BY "
   SQL = SQL & "Proveedor.TipoDePersona, "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion"
   SQL = SQL & " ORDER BY Proveedor.TipoDePersona, retDocumentoPagado.CodigoRetencion"
   fSQLDelReporteRetMenXTipoPersona = SQL
   Set gEnumTablaRetencion = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelReporteRetMenXTipoPersona()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fGetQueryPagosPorTipoDePersonaMesAno(ByVal valMesDeclaracion As Integer, _
                                                      ByVal valAnoDeclaracion As Long, _
                                                       ByVal valTipoDePersona As String, _
                                                        ByVal valSoloRetencionesDiferentesDeCero As Boolean, _
                                                         ByVal tipoPersonaEnum As enum_TipodePersonaRetencion, _
                                                          ByVal valCentimosEnPlanillaMensual As Boolean, _
                                                           ByVal valConsecutivoCompania As String, _
                                                            ByVal valFechaInicioVigencia As Date) As String

   Dim tipoPersonChar As String
   Dim SQL As String
   Dim sqlSumMontoOriginal As String
   Dim sqlSumMontoBaseImp As String
   Dim sqlSumMontoRet As String
   On Error GoTo h_ERROR
   tipoPersonChar = gConvert.enumerativoAChar(tipoPersonaEnum)
   sqlSumMontoOriginal = " SUM(retDocumentoPagado.MontoOriginal) "
   sqlSumMontoBaseImp = " SUM(retDocumentoPagado.MontoBaseImponible) "
   sqlSumMontoRet = " SUM(retDocumentoPagado.MontoRetencion) "
   
   SQL = "SELECT SUM(NumeroDePagosODeAbonosEnCuenta) as NumeroDePagosODeAbonosEnCuenta, SUM(SumMtoOriginal) as SumMtoOriginal, SUM(SumMtoBaseImponible) as SumMtoBaseImponible"
   SQL = SQL & ", SUM(SumMtoRetencion) as SumMtoRetencion, TipoDePersona, Codigo, Articulo, Numeral, Literal, Descripcion FROM"
   SQL = SQL & " (SELECT COUNT(*) AS NumeroDePagosODeAbonosEnCuenta, "
   If valCentimosEnPlanillaMensual Then
      SQL = SQL & sqlSumMontoOriginal & " AS SumMtoOriginal, "
      SQL = SQL & sqlSumMontoBaseImp & " AS SumMtoBaseImponible, "
      SQL = SQL & sqlSumMontoRet & " AS SumMtoRetencion, "
   Else
      SQL = SQL & gUtilSQL.fRound(sqlSumMontoOriginal, "SumMtoOriginal") & ", "
      SQL = SQL & gUtilSQL.fRound(sqlSumMontoBaseImp, "SumMtoBaseImponible") & ", "
      SQL = SQL & gUtilSQL.fRound(sqlSumMontoRet, "SumMtoRetencion") & ", "
   End If
   SQL = SQL & "Proveedor.TipoDePersona AS TipoDePersona, "
   SQL = SQL & "plantillaRET.Codigo AS Codigo, "
   SQL = SQL & "plantillaRET.Articulo AS Articulo, "
   SQL = SQL & "plantillaRET.Numeral AS Numeral, "
   SQL = SQL & "plantillaRET.Literal AS Literal, "
   SQL = SQL & "plantillaRET.Descripcion AS Descripcion "
   SQL = SQL & "FROM (((RetPago INNER JOIN "
   SQL = SQL & "retDocumentoPagado ON "
   SQL = SQL & "RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania"
   SQL = SQL & " AND RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " INNER JOIN tablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = tablaRetencion.Codigo)"
   SQL = SQL & " INNER JOIN Proveedor ON "
   SQL = SQL & "RetPago.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   SQL = SQL & " AND RetPago.CodigoProveedor = Proveedor.CodigoProveedor"
   SQL = SQL & " AND tablaRetencion.TipoDePersona = Proveedor.TipoDePersona)"
   SQL = SQL & " INNER JOIN plantillaRET ON "
   SQL = SQL & "tablaRetencion.SecuencialDePlantilla = plantillaRET.Codigo"
   SQL = SQL & " WHERE "
   SQL = SQL & "Proveedor.TipoDePersona ='" & tipoPersonChar & "'"
   SQL = SQL & " AND TablaRetencion.FechaDeInicioDeVigencia = " & gUtilSQL.fSimpleSqlValue(valFechaInicioVigencia)
   Select Case tipoPersonaEnum
      Case enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA
         SQL = SQL & " AND plantillaRET.Codigo <> '13001' "
      Case enum_TipodePersonaRetencion.eTP_PJ_NODOMICILIADA
         SQL = SQL & " AND plantillaRET.Codigo <> '14001' "
      Case enum_TipodePersonaRetencion.eTP_PN_RESIDENTE
         SQL = SQL & " AND plantillaRET.Codigo <> '11001' "
      Case enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE
         SQL = SQL & " AND plantillaRET.Codigo <> '12001' "
   End Select
   SQL = SQL & " AND RetDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " GROUP BY "
   SQL = SQL & "plantillaRET.Codigo, "
   SQL = SQL & "Proveedor.TipoDePersona, "
   SQL = SQL & "RetPago.ConsecutivoCompania, "
   SQL = SQL & "RetPago.MesAplicacion, "
   SQL = SQL & "RetPago.AnoAplicacion, "
   SQL = SQL & "plantillaRET.Articulo, "
   SQL = SQL & "plantillaRET.Numeral, "
   SQL = SQL & "plantillaRET.Literal, "
   SQL = SQL & "plantillaRET.Descripcion"
   SQL = SQL & " HAVING "
   SQL = SQL & "RetPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RetPago.MesAplicacion = " & valMesDeclaracion
   SQL = SQL & " AND RetPago.AnoAplicacion = " & valAnoDeclaracion
   If valSoloRetencionesDiferentesDeCero Then
      If valCentimosEnPlanillaMensual Then
         SQL = SQL & " AND " & sqlSumMontoRet & " <> 0 "
      Else
         SQL = SQL & " AND " & gUtilSQL.fRound(sqlSumMontoRet) & " <> 0 "
      End If
   End If
   '******** BUSCO LOS REGISTROS PERTENECIENTES A LAS CUENTAS DE TERCEROS
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT COUNT(*) AS NumeroDePagosODeAbonosEnCuenta, "
   If valCentimosEnPlanillaMensual Then
      SQL = SQL & sqlSumMontoOriginal & " AS SumMtoOriginal, "
      SQL = SQL & sqlSumMontoBaseImp & " AS SumMtoBaseImponible, "
      SQL = SQL & sqlSumMontoRet & " AS SumMtoRetencion, "
   Else
      SQL = SQL & gUtilSQL.fRound(sqlSumMontoOriginal, "SumMtoOriginal") & ", "
      SQL = SQL & gUtilSQL.fRound(sqlSumMontoBaseImp, "SumMtoBaseImponible") & ", "
      SQL = SQL & gUtilSQL.fRound(sqlSumMontoRet, "SumMtoRetencion") & ", "
   End If
   SQL = SQL & "Proveedor.TipoDePersona AS TipoDePersona, "
   SQL = SQL & "plantillaRET.Codigo AS Codigo, "
   SQL = SQL & "plantillaRET.Articulo AS Articulo, "
   SQL = SQL & "plantillaRET.Numeral AS Numeral, "
   SQL = SQL & "plantillaRET.Literal AS Literal, "
   SQL = SQL & "plantillaRET.Descripcion AS Descripcion "
   SQL = SQL & "FROM (((RetPago INNER JOIN "
   SQL = SQL & "retDocumentoPagado ON "
   SQL = SQL & "RetPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania"
   SQL = SQL & " AND RetPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante)"
   SQL = SQL & " INNER JOIN tablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = TablaRetencion.Codigo)"
   SQL = SQL & " INNER JOIN Proveedor ON "
   SQL = SQL & "RetPago.ConsecutivoCompania = Proveedor.ConsecutivoCompania"
   SQL = SQL & " AND RetDocumentoPagado.CodigoProveedorOriginalServicio = Proveedor.CodigoProveedor"
   SQL = SQL & " AND tablaRetencion.TipoDePersona = Proveedor.TipoDePersona)"
   SQL = SQL & " INNER JOIN plantillaRET ON "
   SQL = SQL & "tablaRetencion.SecuencialDePlantilla = plantillaRET.Codigo"
   SQL = SQL & " WHERE "
   SQL = SQL & "Proveedor.TipoDePersona ='" & tipoPersonChar & "'"
   Select Case tipoPersonaEnum
      Case enum_TipodePersonaRetencion.eTP_PJ_DOMICILIADA
         SQL = SQL & " AND plantillaRET.Codigo <> '13001' "
      Case enum_TipodePersonaRetencion.eTP_PJ_NODOMICILIADA
         SQL = SQL & " AND plantillaRET.Codigo <> '14001' "
      Case enum_TipodePersonaRetencion.eTP_PN_RESIDENTE
         SQL = SQL & " AND plantillaRET.Codigo <> '11001' "
      Case enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE
         SQL = SQL & " AND plantillaRET.Codigo <> '12001' "
   End Select
   SQL = SQL & " AND TablaRetencion.FechaDeInicioDeVigencia = " & gUtilSQL.fSimpleSqlValue(valFechaInicioVigencia)
   SQL = SQL & " AND RetDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " GROUP BY "
   SQL = SQL & "plantillaRET.Codigo, "
   SQL = SQL & "Proveedor.TipoDePersona, "
   SQL = SQL & "RetPago.ConsecutivoCompania, "
   SQL = SQL & "RetPago.MesAplicacion, "
   SQL = SQL & "RetPago.AnoAplicacion, "
   SQL = SQL & "plantillaRET.Articulo, "
   SQL = SQL & "plantillaRET.Numeral, "
   SQL = SQL & "plantillaRET.Literal, "
   SQL = SQL & "plantillaRET.Descripcion"
   SQL = SQL & " HAVING "
   SQL = SQL & "RetPago.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND RetPago.MesAplicacion = " & valMesDeclaracion
   SQL = SQL & " AND RetPago.AnoAplicacion = " & valAnoDeclaracion
   If valSoloRetencionesDiferentesDeCero Then
      If valCentimosEnPlanillaMensual Then
         SQL = SQL & " AND " & sqlSumMontoRet & " <> 0 "
      Else
         SQL = SQL & " AND " & gUtilSQL.fRound(sqlSumMontoRet) & " <> 0 "
      End If
   End If
   
   SQL = SQL & ") AS Planilla"
   SQL = SQL & "  GROUP BY TipoDePersona, Codigo, Articulo, Numeral, Literal, Descripcion"
   SQL = SQL & " ORDER BY Codigo"
   fGetQueryPagosPorTipoDePersonaMesAno = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fGetQueryPagosPorTipoDePersonaMesAno", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Public Sub sDiaYMesDeGananciasFortuitas(ByRef valMes As Long, ByRef ValAno As Long, ByVal valTipoDePersonaStr As String, ByRef refDiaJuego As Integer, ByRef refMesJuego As Integer, _
'                                 ByRef insTablaRet As Object)
'   Dim SQL As String
'   Dim RS As ADODB.Recordset
'   Dim codigoPlantilla As String
'   Dim Fecha As Date
'   On Error GoTo h_ERROR
'   Set RS = New ADODB.Recordset
'   refDiaJuego = 0
'   refMesJuego = 0
'   SQL = ""
'   SQL = gUtilSQL.fSQLValueWithAnd(SQL, "descripcion", "*JUEGO*")
'   SQL = gUtilSQL.fSQLValueWithAnd(SQL, "TipoDePersona", gConvert.enumerativoAChar(gEnumTablaRetencion.strTipodePersonaRetencionToNum(valTipoDePersonaStr, False)))
'   SQL = "SELECT * FROM plantillaRET WHERE " & SQL
'   RS.Open SQL, Conexion, adOpenDynamic, adLockReadOnly
'   If Not RS.EOF And Not RS.BOF Then
'      RS.MoveFirst
'      codigoPlantilla = RS("Codigo").Value
'      insTablaRet.sClrRecord
'      insTablaRet.SetTipoDePersonaStr valTipoDePersonaStr
'      insTablaRet.SetSecuencialDePlantilla codigoPlantilla
'      If insTablaRet.fSearchSelectConnection(False) Then
'         SQL = "SELECT retPago.Fecha"
'         SQL = SQL & " FROM retPago INNER JOIN "
'         SQL = SQL & "retDocumentoPagado ON "
'         SQL = SQL & "retPago.NumeroComprobante = "
'         SQL = SQL & "retDocumentoPagado.NumeroComprobante"
'         SQL = SQL & " AND "
'         SQL = SQL & "retPago.ConsecutivoCompania = "
'         SQL = SQL & "retDocumentoPagado.ConsecutivoCompania"
'         SQL = SQL & " WHERE "
'         SQL = SQL & "retPago.MesAplicacion = "
'         SQL = SQL & valMes & " AND "
'         SQL = SQL & "retPago.AnoAplicacion = "
'         SQL = SQL & ValAno & " AND "
'         SQL = SQL & "retDocumentoPagado.CodigoRetencion"
'         SQL = SQL & " ='" & insTablaRet.GetCodigo & "'"
'         SQL = SQL & " ORDER BY retPago.Fecha"
'         Set RS = New ADODB.Recordset
'         RS.Open SQL, Conexion, adOpenDynamic, adLockReadOnly
'         If Not RS.EOF And Not RS.BOF Then
'            RS.MoveFirst
'            Fecha = RS("Fecha").Value
'            refDiaJuego = Day(Fecha)
'            refMesJuego = Month(Fecha)
'         End If
'      End If
'   End If
'   gDbUtil.sDestroyRecordSet RS
'h_EXIT: On Error GoTo 0
'   Exit Sub
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "sDiaYMesDeGananciasFortuitas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Sub
'
'
Public Function fSqlCorrigeMonedaEnPagos()
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " UPDATE PAGO SET PAGO.Moneda = CuentaBancaria.NombreDeLaMoneda,"
   SQL = SQL & " Pago.CodigoMoneda = CuentaBancaria.CodigoMoneda"
   SQL = SQL & " From Pago LEFT JOIN CuentaBancaria ON"
   SQL = SQL & " Pago.codigoCuentaBancaria = CuentaBancaria.codigo"
   SQL = SQL & " AND Pago.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania"
   SQL = SQL & " WHERE (PAGO.CodigoMoneda = 'VEB' OR  PAGO.CodigoMoneda = 'VEF')"
   SQL = SQL & " AND  (CuentaBancaria.CodigoMoneda <> 'VEF' AND CuentaBancaria.CodigoMoneda <> 'VEB')"
   fSqlCorrigeMonedaEnPagos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlCorrigeMonedaEnPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlLibroDeRetencionesRenta(ByVal valConsecutivoCompania As String, _
                                             ByVal valFechaInicial As Date, _
                                              ByVal valFechaFinal As Date, _
                                               ByVal valCodMonedaLocal As String, _
                                                ByVal gUltimaTasaDeCambio As Object) As String
   Dim vSQL As String
   Dim SQLMontoBruto As String
   Dim SQLLibroRetencionRenta As String
   Dim insLibroVista As clsPagoVista
   On Error GoTo h_ERROR
   Set insLibroVista = New clsPagoVista
   SQLLibroRetencionRenta = insLibroVista.GetViewNameLibroDeRetencionesRenta
   Set insLibroVista = Nothing
   
   SQLMontoBruto = gUtilSQL.getIIF("(" & SQLLibroRetencionRenta & ".CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(valCodMonedaLocal) & ")", _
                                 gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SQLLibroRetencionRenta & ".CambioaBolivares", _
                                 SQLLibroRetencionRenta & ".Moneda", SQLLibroRetencionRenta & ".TotalDocumentos", True, ""), _
                                 SQLLibroRetencionRenta & ".TotalDocumentos", True)
   vSQL = "SELECT "
   vSQL = vSQL & SQLLibroRetencionRenta & ".Fecha, "
   vSQL = vSQL & SQLLibroRetencionRenta & ".TipoDocumentoIdentificacion, "
   vSQL = vSQL & SQLLibroRetencionRenta & ".NumeroRIF, "
   vSQL = vSQL & SQLLibroRetencionRenta & ".NombreProveedor, "
   vSQL = vSQL & "(" & SQLLibroRetencionRenta & " .TotalRetenido + " & SQLMontoBruto & ") AS MontoBruto, "
   vSQL = vSQL & SQLLibroRetencionRenta & ".TotalRetenido, "
   vSQL = vSQL & SQLMontoBruto & " AS MontoNeto "
   vSQL = vSQL & " FROM " & SQLLibroRetencionRenta
   vSQL = vSQL & " WHERE "
   vSQL = vSQL & SQLLibroRetencionRenta & ".consecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND " & gUtilSQL.DfSQLDateValueBetween(SQLLibroRetencionRenta & ".Fecha", valFechaInicial, valFechaFinal)
   vSQL = vSQL & " ORDER BY " & SQLLibroRetencionRenta & ".Fecha, " & SQLLibroRetencionRenta & ".NombreProveedor"
   fSqlLibroDeRetencionesRenta = vSQL

h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSqlLibroDeRetencionesRenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeLaDeclaracionInformativa(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNumeroDeRif As String, ByVal valAno As String, ByVal valMes As String, ByVal valPreliminar As Boolean, _
                                                ByVal valTipoDeOperacionAExportar As String, ByVal valConsecutivoCompania As Long, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean, ByRef refFactura As Object, ByRef refRenglonFact As Object, _
                                                   ByRef refPago As Object, ByRef refUltimaTasaDeCambio As Object, ByRef refMonedaLocalActual As Object, ByRef refListLibrary As Object, ByVal valUsaMonedaExtranjera As Boolean, ByVal valUsaMultiplesAlicuotas As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   Set insFactura = refFactura
   Set insRenglonFact = refRenglonFact
   Set inPago = refPago
   Set gUltimaTasaDeCambio = refUltimaTasaDeCambio
   Set gMonedaLocalActual = refMonedaLocalActual
   Set gListLibrary = refListLibrary
   mUsaMonedaExtranjera = valUsaMonedaExtranjera
   mUsaMultiplesAlicuotas = valUsaMultiplesAlicuotas
   SQL = fSQLDeclaracionInformativaCompras(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
   If valTipoDeOperacionAExportar = CM_TipoOperacionAExportarToString(CM_TOE_ComprasYVentas) Then
      SQL = SQL & " UNION "
      SQL = SQL & fSQLDeclaracionInformativaVentas(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
   End If
   SQL = SQL & " ORDER BY TipoDeOperacion, FechaDocumento"
'   Set insFactura = Nothing
'   Set insRenglonFact = Nothing
'   Set inPago = Nothing
'   Set gUltimaTasaDeCambio = Nothing
'   Set gMonedaLocalActual = Nothing
'   Set gListLibrary = Nothing
h_EXIT:
   fSQLDeLaDeclaracionInformativa = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeLaDeclaracionInformativa", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDeclaracionInformativaCompras(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNumeroDeRif As String, ByVal valAno As String, ByVal valMes As String, ByVal valPreliminar As Boolean, ByVal valConsecutivoCompania As Long, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = fSQLDeclaracionInformativa(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, eTD_ALICUOTAGENERAL, CM_DecInf_Compras_Desde_CxP, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
   SQL = SQL & " UNION "
   SQL = SQL & fSQLDeclaracionInformativa(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, eTD_ALICUOTA2, CM_DecInf_Compras_Desde_CxP, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
   SQL = SQL & " UNION "
   SQL = SQL & fSQLDeclaracionInformativa(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, eTD_ALICUOTA3, CM_DecInf_Compras_Desde_CxP, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
h_EXIT:
   fSQLDeclaracionInformativaCompras = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeclaracionInformativaCompras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDeclaracionInformativaVentas(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNumeroDeRif As String, ByVal valAno As String, ByVal valMes As String, ByVal valPreliminar As Boolean, ByVal valConsecutivoCompania As Long, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = fSQLDeclaracionInformativa(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, eTD_ALICUOTAGENERAL, CM_DecInf_Ventas_Desde_Factura, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
   SQL = SQL & " UNION "
   SQL = SQL & fSQLDeclaracionInformativa(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, eTD_ALICUOTAGENERAL, CM_DecInf_Ventas_Desde_CxC, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
   SQL = SQL & " UNION "
   SQL = SQL & fSQLDeclaracionInformativa(valFechaInicial, valFechaFinal, valNumeroDeRif, valAno, valMes, valPreliminar, eTD_ALICUOTAGENERAL, CM_DecInf_Ventas_Desde_Resumen_Ventas, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
h_EXIT:
   fSQLDeclaracionInformativaVentas = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeclaracionInformativaVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDeclaracionInformativa(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNumeroDeRif As String, ByVal valAno As String, ByVal valMes As String, _
         ByVal valPreliminar As Boolean, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valTipoDeDocumento As Integer, ByVal valConsecutivoCompania As Long, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean) As String
   Dim SQL As String
   Dim periodoDeImposicion As String
   Dim rifContribuyente As String
   Dim sqlMontoIvaRetenido As String
   Dim sqlCondicionMtoIvaRetenido As String
   On Error GoTo h_ERROR
   rifContribuyente = fSQLDecInfRifDelContribuyente(valNumeroDeRif)
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
     periodoDeImposicion = gUtilSQL.getIIF(gUtilSQL.fMonth("cxP.FechaAplicacionRetIVA", "") & " < 10", gUtilSQL.fCast(gUtilSQL.fYear("cxP.FechaAplicacionRetIVA", ""), eTDSS_VARCHAR, "") & " + " & gUtilSQL.fSimpleSqlValue("0") & "+" & gUtilSQL.fCast(gUtilSQL.fMonth("cxP.FechaAplicacionRetIVA", ""), eTDSS_VARCHAR, ""), gUtilSQL.fCast(gUtilSQL.fYear("cxP.FechaAplicacionRetIVA", ""), eTDSS_VARCHAR, "") & "+" & gUtilSQL.fCast(gUtilSQL.fMonth("cxP.FechaAplicacionRetIVA", ""), eTDSS_VARCHAR, ""))
   Else
     periodoDeImposicion = gUtilSQL.getIIF(gUtilSQL.fMonth("DocumentoCobrado.FechaComprobanteRetIva", "") & " < 10", gUtilSQL.fCast(gUtilSQL.fYear("DocumentoCobrado.FechaComprobanteRetIva", ""), eTDSS_VARCHAR, "") & " + " & gUtilSQL.fSimpleSqlValue("0") & "+" & gUtilSQL.fCast(gUtilSQL.fMonth("DocumentoCobrado.FechaComprobanteRetIva", ""), eTDSS_VARCHAR, ""), gUtilSQL.fCast(gUtilSQL.fYear("DocumentoCobrado.FechaComprobanteRetIva", ""), eTDSS_VARCHAR, "") & "+" & gUtilSQL.fCast(gUtilSQL.fMonth("DocumentoCobrado.FechaComprobanteRetIva", ""), eTDSS_VARCHAR, ""))
   End If
   sqlMontoIvaRetenido = fSQLDecInfMontoIva(valTipoDeDocumento, valAlicuota, valFechaInicial, valFechaFinal, valConsecutivoCompania)
   sqlCondicionMtoIvaRetenido = fSQLDecInfCondicionMontoIvaRetenidoDifCero(valTipoDeDocumento, valAlicuota, valFechaInicial, valFechaFinal, valConsecutivoCompania)
   SQL = "SELECT "
   SQL = SQL & rifContribuyente & " AS RifDelContribuyente, "
   SQL = SQL & periodoDeImposicion & " AS PeriodoDeImposicion, "
   SQL = SQL & fSQLDecInfFechaDocumento(valTipoDeDocumento) & " AS FechaDocumento, "
   SQL = SQL & fSQLDecInfTipoDeOperacion(valTipoDeDocumento) & " AS TipoDeOperacion, "
   SQL = SQL & fSQLDecInfTipoDocumento(valTipoDeDocumento) & " AS TipoDocumento, "
   SQL = SQL & fSQLDecInfNumeroDeRif(valTipoDeDocumento, rifContribuyente) & " AS NumeroDeRif, "
   SQL = SQL & fSQLDecInfNumeroDeDocumento(valTipoDeDocumento) & " AS NumeroDeDocumento, "
   SQL = SQL & fSQLDecInfNumeroDeControl(valTipoDeDocumento) & " AS NumeroDeControlDeFactura, "
   SQL = SQL & fSQLDecInfMontoDocumento(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero) & " AS MontoDocumento, "
   SQL = SQL & fSQLDecInfBaseImponible(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero) & " AS BaseImponible, "
   SQL = SQL & sqlMontoIvaRetenido & " AS MontoIvaRet, "
   SQL = SQL & fSQLDecInfNumeroDeDocumentoAfectado(valTipoDeDocumento) & " AS NumeroDeDocumentoAfectado, "
   SQL = SQL & fSQLDecInfNumeroDeComprobante(valTipoDeDocumento, periodoDeImposicion, valConsecutivoCompania) & " AS NumeroDeComprobante, "
   SQL = SQL & fSQLDecInfMontoExentoDeIva(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero) & " AS MontoExentoDeIVA, "
   SQL = SQL & fSQLDecInfAlicuota(valTipoDeDocumento, valAlicuota) & " AS Alicuota, "
   SQL = SQL & fSQLDecInfNumeroDeExpediente(valTipoDeDocumento) & " AS NumeroDeExpediente "
   If valPreliminar Then
      SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue(valNumeroDeRif) & " AS RifCiaOrig "
      SQL = SQL & ", " & fSQLDecInfRifOriginal(valTipoDeDocumento) & " AS RifProvOrig "
   End If
   SQL = SQL & " FROM " & fSQLDecInfClausulaFrom(valTipoDeDocumento)
   SQL = SQL & " WHERE " & fSQLDecInfClausulaWhere(valTipoDeDocumento, valFechaInicial, valFechaFinal, sqlCondicionMtoIvaRetenido, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   End If
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      SQL = SQL & " UNION "
      SQL = SQL & " SELECT "
      SQL = SQL & rifContribuyente & " AS RifDelContribuyente, "
      SQL = SQL & periodoDeImposicion & " AS PeriodoDeImposicion, "
      SQL = SQL & fSQLDecInfFechaDocumento(valTipoDeDocumento) & " AS FechaDocumento, "
      SQL = SQL & fSQLDecInfTipoDeOperacion(valTipoDeDocumento) & " AS TipoDeOperacion, "
      SQL = SQL & fSQLDecInfTipoDocumento(valTipoDeDocumento) & " AS TipoDocumento, "
      SQL = SQL & fSQLDecInfNumeroDeRif(valTipoDeDocumento, rifContribuyente) & " AS NumeroDeRif, "
      SQL = SQL & fSQLDecInfNumeroDeDocumento(valTipoDeDocumento) & " AS NumeroDeDocumento, "
      SQL = SQL & fSQLDecInfNumeroDeControl(valTipoDeDocumento) & " AS NumeroDeControlDeFactura, "
      SQL = SQL & fSQLDecInfMontoDocumento(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero) & " AS MontoDocumento, "
      SQL = SQL & fSQLDecInfBaseImponible(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero) & " AS BaseImponible, "
      SQL = SQL & sqlMontoIvaRetenido & " AS MontoIvaRet, "
      SQL = SQL & fSQLDecInfNumeroDeDocumentoAfectado(valTipoDeDocumento) & " AS NumeroDeDocumentoAfectado, "
      SQL = SQL & fSQLDecInfNumeroDeComprobante(valTipoDeDocumento, periodoDeImposicion, valConsecutivoCompania) & " AS NumeroDeComprobante, "
      SQL = SQL & fSQLDecInfMontoExentoDeIva(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero) & " AS MontoExentoDeIVA, "
      SQL = SQL & fSQLDecInfAlicuota(valTipoDeDocumento, valAlicuota) & " AS Alicuota, "
      SQL = SQL & fSQLDecInfNumeroDeExpediente(valTipoDeDocumento) & " AS NumeroDeExpediente "
      If valPreliminar Then
         SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue(valNumeroDeRif) & " AS RifCiaOrig "
         SQL = SQL & ", " & fSQLDecInfRifOriginal(valTipoDeDocumento) & " AS RifProvOrig "
      End If
      SQL = SQL & " FROM alicuotaIVA, (CxP INNER JOIN proveedor ON Cxp.ConsecutivoCompania = proveedor.ConsecutivoCompania"
      SQL = SQL & " AND Cxp.CodigoProveedorOriginalServicio = proveedor.CodigoProveedor)"
      SQL = SQL & " WHERE " & fSQLDecInfClausulaWhere(valTipoDeDocumento, valFechaInicial, valFechaFinal, sqlCondicionMtoIvaRetenido, valConsecutivoCompania, valIncluirRegistrosMtoRetIvaCero)
      SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   End If
h_EXIT:
   fSQLDeclaracionInformativa = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeclaracionInformativa", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfFechaDocumento(ByVal valTipoDeDocumento As Integer) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      SQL = gUtilSQL.DfSQLFormat("Cxp.Fecha", "yyyy-mm-dd", "")
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      SQL = gUtilSQL.DfSQLFormat(insFactura.GetTableName & "." & insFactura.getFN_FECHA, "yyyy-mm-dd", "")
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      SQL = gUtilSQL.DfSQLFormat("cxC.Fecha", "yyyy-mm-dd", "")
   End If
h_EXIT:
   fSQLDecInfFechaDocumento = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfFechaDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfTipoDeOperacion(ByVal valTipoDeDocumento As Integer) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      SQL = gUtilSQL.fSimpleSqlValue("C")
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or _
      valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Or _
      valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      SQL = gUtilSQL.fSimpleSqlValue("V")
   End If
h_EXIT:
   fSQLDecInfTipoDeOperacion = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfTipoDeOperacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfTipoDocumento(ByVal valTipoDeDocumento As Integer)
   Dim sqlTipoDocumento As String
   Dim sqlTipoFactura As String
   Dim sqlTipoNotaCredito As String
   Dim sqlTipoNotaDebito As String
   Dim sqlTipoNotaEntrega As String
   Dim sqlTipoCompraImportacion As String
   Dim sqlTipoVentaExportacion As String
   Dim sqlTDFactura As String
   Dim sqlTDNDebito As String
   Dim sqlTDNCredito As String
   Dim sqlTDCertificacion As String
   Dim sqlTDImportacion As String
   Dim sqlTDExportacion As String
   On Error GoTo h_ERROR
   sqlTDFactura = "01"
   sqlTDNDebito = "02"
   sqlTDNCredito = "03"
   sqlTDCertificacion = "04"
   sqlTDImportacion = "05"
   sqlTDExportacion = "06"
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      sqlTipoFactura = "(" & "Cxp.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA) & ")"
      sqlTipoNotaCredito = "(" & "Cxp.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO) & ")"
      sqlTipoNotaDebito = "(" & "Cxp.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO) & ")"
      sqlTipoNotaEntrega = "(" & "Cxp.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTAENTREGA) & ")"
      sqlTipoCompraImportacion = "(" & "Cxp.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & ")"
      sqlTipoDocumento = gUtilSQL.getIIF("(" & sqlTipoFactura & " OR " & sqlTipoNotaEntrega & _
                                       ") AND " & sqlTipoCompraImportacion, gUtilSQL.fSimpleSqlValue(sqlTDImportacion), _
                     gUtilSQL.getIIF("(" & sqlTipoFactura & " OR " & sqlTipoNotaEntrega & ")", _
                                    gUtilSQL.fSimpleSqlValue(sqlTDFactura), _
                        gUtilSQL.getIIF(sqlTipoNotaDebito, gUtilSQL.fSimpleSqlValue(sqlTDNDebito), _
                           gUtilSQL.getIIF(sqlTipoNotaCredito, gUtilSQL.fSimpleSqlValue(sqlTDNCredito), _
                              gUtilSQL.fSimpleSqlValue(sqlTDFactura), True), True), True), True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlTipoFactura = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & ")"
      sqlTipoNotaCredito = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & ")"
      sqlTipoNotaDebito = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & ")"
      sqlTipoVentaExportacion = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_VENTA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVenta.eTD_EXPORTACION) & ")"
      sqlTipoDocumento = gUtilSQL.getIIF("(" & sqlTipoFactura & " AND " & sqlTipoVentaExportacion & ")", gUtilSQL.fSimpleSqlValue(sqlTDExportacion), _
                           gUtilSQL.getIIF(sqlTipoFactura, gUtilSQL.fSimpleSqlValue(sqlTDFactura), _
                              gUtilSQL.getIIF(sqlTipoNotaDebito, gUtilSQL.fSimpleSqlValue(sqlTDNDebito), _
                                 gUtilSQL.getIIF(sqlTipoNotaCredito, gUtilSQL.fSimpleSqlValue(sqlTDNCredito), _
                                    gUtilSQL.fSimpleSqlValue(sqlTDFactura), True), True), True), True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlTipoFactura = "(" & "cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA) & ")"
      sqlTipoNotaCredito = "(" & "cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO) & ")"
      sqlTipoNotaDebito = "(" & "cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO) & ")"
      sqlTipoNotaEntrega = "(" & "cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTAENTREGA) & ")"
      sqlTipoDocumento = gUtilSQL.getIIF("(" & sqlTipoFactura & " OR " & sqlTipoNotaEntrega & ")", gUtilSQL.fSimpleSqlValue(sqlTDFactura), _
                           gUtilSQL.getIIF(sqlTipoNotaDebito, gUtilSQL.fSimpleSqlValue(sqlTDNDebito), _
                              gUtilSQL.getIIF(sqlTipoNotaCredito, gUtilSQL.fSimpleSqlValue(sqlTDNCredito), _
                                 gUtilSQL.fSimpleSqlValue(sqlTDFactura), True), True), True)
   End If
h_EXIT:
   fSQLDecInfTipoDocumento = sqlTipoDocumento
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfTipoDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfNumeroDeRif(ByVal valTipoDeDocumento As Integer, ByVal valRifContribuyente As String)
   Dim sqlNumeroRif As String
   Dim sqlRifVacio  As String
   Dim sqlRIF As String
   Dim NumRif As String
   Dim sqlTipoFactura As String
   Dim sqlTipoNCredito As String
   Dim sqlTipoNDebito As String
   Dim sqlTipoNEntrega As String
   Dim sqlTipoCompraImportacion As String
   Dim sqlTipoVentaExportacion As String
   On Error GoTo h_ERROR
   sqlRifVacio = gUtilSQL.fSimpleSqlValue("J000000000")
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      sqlTipoFactura = "(Cxp.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA) & ")"
      sqlTipoNEntrega = "(Cxp.TipoDeCxP = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTAENTREGA) & ")"
      sqlTipoCompraImportacion = "(Cxp.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & ")"
      NumRif = "proveedor.NumeroRIF"
      sqlRIF = gUtilSQL.getIIF(gUtilSQL.fIsNumeric(gUtilSQL.DfLeftSQL(NumRif, 1)), _
                           gUtilSQL.DfRightSQL(NumRif, 10, "0000000000"), _
                           gUtilSQL.DfLeftSQL(NumRif, 1) & gUtilSQL.CharConcat & _
                              gUtilSQL.DfRightSQL(gUtilSQL.DfMidSQL(NumRif, 2, 9), 9, "000000000"), True)
      sqlRIF = gUtilSQL.getIIF(gUtilSQL.fLen("proveedor", "NumeroRIF") & " < 1 ", sqlRifVacio, sqlRIF, True)
      sqlNumeroRif = gUtilSQL.getIIF("(" & sqlTipoFactura & " OR " & sqlTipoNEntrega & ") AND " & sqlTipoCompraImportacion, valRifContribuyente, sqlRIF, True)
   Else
      NumRif = "cliente.NumeroRIF"
      sqlRIF = gUtilSQL.getIIF(gUtilSQL.fIsNumeric(gUtilSQL.DfLeftSQL(NumRif, 1)), _
                           gUtilSQL.DfRightSQL(NumRif, 10, "0000000000"), _
                           gUtilSQL.DfLeftSQL(NumRif, 1) & gUtilSQL.CharConcat & _
                              gUtilSQL.DfRightSQL(gUtilSQL.DfMidSQL(NumRif, 2, 9), 9, "000000000"), True)
      sqlRIF = gUtilSQL.getIIF(gUtilSQL.fLen("Cliente", "NumeroRIF") & " < 1 ", sqlRifVacio, sqlRIF, True)
      If valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Then
         insFactura.setClaseDeTrabajo eCTFC_Factura
         sqlTipoFactura = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & ")"
         sqlTipoNCredito = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & ")"
         sqlTipoNDebito = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & ")"
         sqlTipoVentaExportacion = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_VENTA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVenta.eTD_EXPORTACION) & ")"
         sqlNumeroRif = gUtilSQL.getIIF("(" & sqlTipoFactura & " AND " & sqlTipoVentaExportacion & ")", valRifContribuyente, sqlRIF, True)
      ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
         sqlNumeroRif = NumRif
      ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
         sqlTipoFactura = "(cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA) & ")"
         sqlTipoNCredito = "(cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO) & ")"
         sqlTipoNDebito = "(cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO) & ")"
         sqlTipoNEntrega = "(cxC.TipoCxC = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTAENTREGA) & ")"
         sqlNumeroRif = gUtilSQL.getIIF("(" & sqlTipoFactura & " OR " & sqlTipoNCredito & " OR " & sqlTipoNDebito & " OR " & sqlTipoNEntrega & ")", sqlRIF, valRifContribuyente, True)
      End If
   End If
h_EXIT:
   fSQLDecInfNumeroDeRif = sqlNumeroRif
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfNumeroDeRif", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfNumeroDeDocumento(ByVal valTipoDeDocumento As Integer) As String
   Dim sqlNumeroDeDocumento As String
   Dim sqlTipoCompraImportacion As String
   Dim sqlTipoVentaExportacion As String
   On Error GoTo h_ERROR
    
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      sqlTipoCompraImportacion = "(" & "Cxp.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & ")"
      sqlNumeroDeDocumento = gUtilSQL.getIIF(sqlTipoCompraImportacion, _
                                 "Cxp.NumeroExpedienteDeImportacion", _
                                 gUtilSQL.DfTrimSQL("Cxp.Numero"), True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlTipoVentaExportacion = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_VENTA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVenta.eTD_EXPORTACION) & ")"
      sqlNumeroDeDocumento = gUtilSQL.getIIF(sqlTipoVentaExportacion, _
                                 insFactura.GetTableName & "." & insFactura.getFN_NUMERO_PLANILLA_EXPORTACION, _
                                 insFactura.GetTableName & "." & insFactura.getFN_NUMERO, True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlNumeroDeDocumento = insFactura.GetTableName & "." & insFactura.getFN_NUMERO_DESDE
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlNumeroDeDocumento = "cxC.Numero"
   End If
h_EXIT:
   fSQLDecInfNumeroDeDocumento = sqlNumeroDeDocumento
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfNumeroDeDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfNumeroDeControl(ByVal valTipoDeDocumento As Integer) As String
   Dim sqlNumeroDeControl As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      sqlNumeroDeControl = gUtilSQL.getIIF("Cxp.NumeroControl = ''", _
                           gUtilSQL.fSimpleSqlValue("0"), "Cxp.NumeroControl", True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlNumeroDeControl = gUtilSQL.getIIF(insFactura.GetTableName & "." & insFactura.getFN_NUMERO_CONTROL & " = ''", _
                              gUtilSQL.fSimpleSqlValue("0"), insFactura.GetTableName & "." & insFactura.getFN_NUMERO_CONTROL, True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlNumeroDeControl = gUtilSQL.fSimpleSqlValue("0")
   End If
h_EXIT:
   fSQLDecInfNumeroDeControl = sqlNumeroDeControl
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfNumeroDeControl", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfMontoDocumento(ByVal valTipoDeDocumento As Integer, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valConsecutivoCompania As Long, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean) As String
   Dim sqlMontoDocumento As String
   Dim sqlSuma As String
   Dim sqlMtoGravable As String
   Dim SQL As String
   Dim sqlCampoCambio As String
   Dim sqlCampoMoneda As String
   Dim sqlMontoIva As String
   Dim varListaComparativa As String
   Dim varListaResultante As String
   Dim sqlFecha As String
   Dim sqlMontoExentoDeIvaConCambioSiAplica As String
   
   On Error GoTo h_ERROR
   varListaComparativa = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & gListLibrary.getSeparadorGenericoDeElementos & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt)
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      If valAlicuota = eTD_ALICUOTA3 Then
         sqlMtoGravable = gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuota3")
         sqlMontoIva = gUtilSQL.DfCDecSQL("Cxp.MontoIVAAlicuota3")
      ElseIf valAlicuota = eTD_ALICUOTA2 Then
         sqlMtoGravable = gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuota2")
         sqlMontoIva = gUtilSQL.DfCDecSQL("Cxp.MontoIVAAlicuota2")
      Else
        sqlMtoGravable = gUtilSQL.getIIF("Cxp.MontoGravableAlicuotaEspecial1 <> 0", "Cxp.MontoGravableAlicuotaEspecial1", _
            gUtilSQL.getIIF("Cxp.MontoGravableAlicuotaEspecial2 <> 0", "Cxp.MontoGravableAlicuotaEspecial2", "Cxp.MontoGravableAlicuotaGeneral"))
         sqlMontoIva = gUtilSQL.getIIF("Cxp.MontoIVAAlicuotaEspecial1 <> 0", "Cxp.MontoIVAAlicuotaEspecial1", _
            gUtilSQL.getIIF("Cxp.MontoIVAAlicuotaEspecial2 <> 0", "Cxp.MontoIVAAlicuotaEspecial2", "Cxp.MontoIVAAlicuotaGeneral"))
      End If
      sqlSuma = "ABS(" & fSQLDecInfMontoExentoDeIva(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, True) & " +  Abs(" & sqlMtoGravable & ") + Abs(" & sqlMontoIva & "))"
      sqlMontoDocumento = gUtilSQL.getIIF(sqlMtoGravable & " <> 0 ", sqlSuma, "0", True)
      varListaResultante = sqlMontoDocumento & gListLibrary.getSeparadorGenericoDeElementos & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoDocumento, "Cxp.Fecha")
      If mUsaMonedaExtranjera Then
         sqlCampoCambio = gUtilSQL.getIIF("Cxp.OrigenDeLaRetencion" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
               "Cxp.CambioABolivares", fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero), True)
         sqlCampoMoneda = gUtilSQL.getIIF("Cxp.OrigenDeLaRetencion" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
               "Cxp.Moneda", fSubSQLDecInfCambioOMonedaOriginalDesdePago(False, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero), True)
         'sqlMontoDocumento = gUtilSQL.DfSQLCaseIf(sqlCampoMoneda, varListaComparativa, varListaResultante, gListLibrary.getSeparadorGenericoDeElementos, "=", "")
         
         sqlSuma = "(ABS(" & sqlMtoGravable & ") + ABS(" & sqlMontoIva & "))"
         sqlMontoDocumento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlSuma, True, "")
         sqlMontoExentoDeIvaConCambioSiAplica = fSQLDecInfMontoExentoDeIva(valTipoDeDocumento, valAlicuota, valConsecutivoCompania, True, valIncluirRegistrosMtoRetIvaCero)
         sqlMontoDocumento = "ABS(" & sqlMontoDocumento & " + " & sqlMontoExentoDeIvaConCambioSiAplica & ")"
         sqlMontoDocumento = gUtilSQL.getIIF(sqlMtoGravable & " <> 0 ", sqlMontoDocumento, "0", True)
         
      Else
         sqlMontoDocumento = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoDocumento, "Cxp.Fecha")
      End If
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlMontoDocumento = "ABS(" & gUtilSQL.DfCDecSQL(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA) & ")"
      sqlMontoDocumento = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoDocumento, insFactura.GetTableName & ".Fecha")
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlMontoDocumento = "ABS(" & gUtilSQL.DfCDecSQL("cxC.MontoExento") & _
                           " + " & gUtilSQL.DfCDecSQL("cxC.MontoGravado") & _
                           " + " & gUtilSQL.DfCDecSQL("cxC.MontoIVA") & ")"
      sqlMontoDocumento = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMontoDocumento, "cxC.Fecha")
   End If
h_EXIT:
   fSQLDecInfMontoDocumento = sqlMontoDocumento
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfMontoDocumento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfBaseImponible(ByVal valTipoDeDocumento As Integer, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valConsecutivoCompania As Long, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean) As String
   Dim sqlBaseImponible As String
   Dim sqlMtoGravable As String
   Dim sqlCampoCambio As String
   Dim sqlCampoMoneda As String
   Dim varListaComparativa As String
   Dim varListaResultante As String
   Dim sqlFecha As String
   On Error GoTo h_ERROR
   varListaComparativa = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & gListLibrary.getSeparadorGenericoDeElementos & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt)
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      If valAlicuota = eTD_ALICUOTA3 Then
         sqlMtoGravable = gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuota3")
      ElseIf valAlicuota = eTD_ALICUOTA2 Then
         sqlMtoGravable = gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuota2")
      Else
         sqlMtoGravable = gUtilSQL.getIIF(gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuotaEspecial1") & " <> 0", gUtilSQL.DfCDecSQL("MontoGravableAlicuotaEspecial1"), _
            gUtilSQL.getIIF(gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuotaEspecial2") & " <> 0", "MontoGravableAlicuotaEspecial2", gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuotaGeneral")))
      End If
      sqlBaseImponible = gUtilSQL.getIIF(sqlMtoGravable & " <> 0 ", "ABS(" & sqlMtoGravable & ")", "0", True)
      varListaResultante = sqlBaseImponible & gListLibrary.getSeparadorGenericoDeElementos & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlBaseImponible, "Cxp.Fecha")
      If mUsaMonedaExtranjera Then
         sqlCampoCambio = gUtilSQL.getIIF("Cxp.OrigenDeLaRetencion" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
               "Cxp.CambioABolivares", fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero), True)
         sqlCampoMoneda = gUtilSQL.getIIF("Cxp.OrigenDeLaRetencion" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
               "Cxp.Moneda", fSubSQLDecInfCambioOMonedaOriginalDesdePago(False, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero), True)
         'sqlBaseImponible = gUtilSQL.DfSQLCaseIf(sqlCampoMoneda, varListaComparativa, varListaResultante, gListLibrary.getSeparadorGenericoDeElementos, "=", "")
         sqlBaseImponible = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlBaseImponible, True, "")
      Else
         sqlBaseImponible = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlBaseImponible, "Cxp.Fecha")
      End If
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlBaseImponible = "ABS(" & gUtilSQL.DfCDecSQL(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_BASE_IMPONIBLE) & ")"
      sqlBaseImponible = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlBaseImponible, insFactura.GetTableName & "." & insFactura.getFN_FECHA)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlBaseImponible = "ABS(" & gUtilSQL.DfCDecSQL("cxC.MontoGravado") & ")"
      sqlBaseImponible = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlBaseImponible, "cxC.Fecha")
   End If
h_EXIT:
   fSQLDecInfBaseImponible = sqlBaseImponible
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfBaseImponible", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfNumeroDeDocumentoAfectado(ByVal valTipoDeDocumento As Integer) As String
   Dim sqlNumeroDeDocumentoAfectado As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      sqlNumeroDeDocumentoAfectado = gUtilSQL.getIIF("Cxp.NumeroDeFacturaAfectada" & _
                     " = ''", gUtilSQL.fSimpleSqlValue("0"), _
                     "Cxp.NumeroDeFacturaAfectada", True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlNumeroDeDocumentoAfectado = gUtilSQL.getIIF(insFactura.GetTableName & "." & insFactura.getFN_NUMERO_FACTURA_AFECTADA & _
                     " = ''", gUtilSQL.fSimpleSqlValue("0"), _
                     insFactura.GetTableName & "." & insFactura.getFN_NUMERO_FACTURA_AFECTADA, True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlNumeroDeDocumentoAfectado = gUtilSQL.fSimpleSqlValue("0")
   End If
h_EXIT:
   fSQLDecInfNumeroDeDocumentoAfectado = sqlNumeroDeDocumentoAfectado
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfNumeroDeDocumentoAfectado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfNumeroDeComprobante(ByVal valTipoDeDocumento As Integer, ByVal valPeriodoDeImposicion As String, ByVal valConsecutivoCompania As Long) As String
   Dim sqlNumeroDeComprobante As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      sqlNumeroDeComprobante = valPeriodoDeImposicion & gUtilSQL.CharConcat & "Cxp.NumeroComprobanteRetencion"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      SQL = "(SELECT " & "documentoCobrado.NumeroComprobanteRetIVA"
      SQL = SQL & " FROM (" & "cobranza INNER JOIN "
      SQL = SQL & "documentoCobrado ON ("
      SQL = SQL & "cobranza.Numero" & _
                  " = " & "documentoCobrado.NumeroCobranza"
      SQL = SQL & ") AND (" & "cobranza.ConsecutivoCompania" & _
                  " = " & "documentoCobrado.ConsecutivoCompania"
      SQL = SQL & ")) INNER JOIN " & "cxC ON ("
      SQL = SQL & "documentoCobrado.NumeroDelDocumentoCobrado" & _
                  " = " & "cxC.Numero"
      SQL = SQL & ") AND (" & "documentoCobrado.TipoDeDocumentoCobrado = " & "cxC.TipoCxC"
      SQL = SQL & ") AND (" & "documentoCobrado.ConsecutivoCompania" & _
                  " = " & "cxC.ConsecutivoCompania)"
      SQL = SQL & " WHERE " & "cxC.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND documentoCobrado.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(True)
      SQL = SQL & " AND " & "cxC.NumeroDocumentoOrigen" & _
                  " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
      'SQL = SQL & " AND " & "cxC.TipoCxC = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & _
                  gUtilSQL.getIIF("cxC.TipoCxC" & _
                        " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                        gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO), _
                  gUtilSQL.getIIF("cxC.TipoCxC" & _
                        " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                        gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO), _
                        gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA), True), True)
      SQL = SQL & " AND " & "cobranza.StatusCobranza" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & ")"
      SQL = gUtilSQL.DfCStrSQL(SQL)
      SQL = gUtilSQL.DfRightSQL(SQL, 8, "00000000")
      sqlNumeroDeComprobante = valPeriodoDeImposicion & gUtilSQL.CharConcat & SQL
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlNumeroDeComprobante = insFactura.GetTableName & "." & insFactura.getFN_NUMERO_COMPROBANTE_RET_IVA
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      SQL = "(SELECT " & "documentoCobrado.NumeroComprobanteRetIVA"
      SQL = SQL & " FROM " & "cobranza INNER JOIN " & "documentoCobrado ON ("
      SQL = SQL & "cobranza.Numero" & _
                  " = " & "documentoCobrado.NumeroCobranza"
      SQL = SQL & ") AND (" & "cobranza.ConsecutivoCompania" & _
                  " = " & "documentoCobrado.ConsecutivoCompania)"
      SQL = SQL & " AND documentoCobrado.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(True)
      SQL = SQL & " WHERE " & "documentoCobrado.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND " & "documentoCobrado.NumeroDelDocumentoCobrado" & _
                  " = " & "cxC.Numero"
      SQL = SQL & " AND " & "cobranza.StatusCobranza" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & ")"
      SQL = gUtilSQL.DfCStrSQL(SQL)
      SQL = gUtilSQL.DfRightSQL(SQL, 8, "00000000")
      sqlNumeroDeComprobante = valPeriodoDeImposicion & gUtilSQL.CharConcat & SQL
   End If
   sqlNumeroDeComprobante = gUtilSQL.fCast(sqlNumeroDeComprobante, eTDSS_VARCHAR, "")
h_EXIT:
   fSQLDecInfNumeroDeComprobante = sqlNumeroDeComprobante
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfNumeroDeComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfMontoExentoDeIva(ByVal valTipoDeDocumento As Integer, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valConsecutivoCompania As Long, Optional ByVal valVieneDeOtroCalculo As Boolean = False, Optional ByVal valIncluirRegistrosMtoRetIvaCero As Boolean = False) As String
   Dim sqlMtoExento As String
   Dim SQL As String
   Dim cxpMtoGravG As String
   Dim cxpMtoGrav2 As String
   Dim cxpMtoGrav3 As String
   Dim cxpMtoExento As String
   Dim sqlCondicion As String
   Dim sqlCampoCambio As String
   Dim sqlCampoMoneda As String
   Dim varListaComparativa As String
   Dim varListaResultante As String
   Dim sqlFecha As String
   On Error GoTo h_ERROR
   varListaComparativa = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & gListLibrary.getSeparadorGenericoDeElementos & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt)
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      cxpMtoExento = "ABS(" & gUtilSQL.DfCDecSQL("Cxp.MontoExento") & ")"
      cxpMtoGravG = gUtilSQL.getIIF(gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuotaEspecial1") & " <> 0", gUtilSQL.DfCDecSQL("MontoGravableAlicuotaEspecial1"), _
         gUtilSQL.getIIF(gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuotaEspecial2") & " <> 0", "MontoGravableAlicuotaEspecial2", gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuotaGeneral")))
      cxpMtoGrav2 = gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuota2")
      cxpMtoGrav3 = gUtilSQL.DfCDecSQL("Cxp.MontoGravableAlicuota3")
'      sqlFecha = gUtilSQL.getIIF("Cxp.OrigenDeLaRetencion" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
'               "Cxp.Fecha", "Pago.Fecha", True)
      Select Case valAlicuota
         Case eTD_ALICUOTAGENERAL
            SQL = "((" & cxpMtoGravG & " <> 0) OR (" & cxpMtoGravG & " = 0 AND " & _
                  cxpMtoGrav2 & " = 0 AND " & cxpMtoGrav3 & " = 0))"
         Case eTD_ALICUOTA2
            SQL = "(" & cxpMtoGrav2 & " <> 0 AND " & cxpMtoGravG & " = 0 )"
         Case eTD_ALICUOTA3
            SQL = "(" & cxpMtoGrav3 & " <> 0 AND " & cxpMtoGravG & " = 0 AND " & cxpMtoGrav2 & " = 0)"
      End Select
      sqlMtoExento = gUtilSQL.getIIF(SQL, cxpMtoExento, "0", True)
      varListaResultante = sqlMtoExento & gListLibrary.getSeparadorGenericoDeElementos & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoExento, "Cxp.Fecha")
      If mUsaMonedaExtranjera Then
         sqlCampoCambio = gUtilSQL.getIIF("Cxp.OrigenDeLaRetencion" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
               "Cxp.CambioABolivares", fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero), True)
         sqlCampoMoneda = gUtilSQL.getIIF("Cxp.OrigenDeLaRetencion" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
               "Cxp.Moneda", fSubSQLDecInfCambioOMonedaOriginalDesdePago(False, valConsecutivoCompania, False, valIncluirRegistrosMtoRetIvaCero), True)
         If Not valVieneDeOtroCalculo Then
            sqlMtoExento = gUtilSQL.DfSQLCaseIf(sqlCampoMoneda, varListaComparativa, varListaResultante, gListLibrary.getSeparadorGenericoDeElementos, "=", "")
         End If
         sqlMtoExento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMtoExento, True, "")
      Else
         If Not valVieneDeOtroCalculo Then
            sqlMtoExento = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoExento, "Cxp.Fecha")
         End If
      End If
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlMtoExento = "ABS(" & gUtilSQL.DfCDecSQL(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_MONTO_EXENTO) & ")"
      If Not valVieneDeOtroCalculo Then
         sqlMtoExento = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoExento, insFactura.GetTableName & ".Fecha")
      End If
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlMtoExento = "ABS(" & gUtilSQL.DfCDecSQL("cxC.MontoExento") & ")"
      If Not valVieneDeOtroCalculo Then
         sqlMtoExento = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoExento, "cxC.Fecha")
      End If
   End If
h_EXIT:
   fSQLDecInfMontoExentoDeIva = sqlMtoExento
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfMontoExentoDeIva", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQLDecInfAlicuota(ByVal valTipoDeDocumento As Integer, ByVal valAlicuota As enum_TipoDeAlicuota) As String
   Dim sqlMontoAlicuota As String
   Dim sqlAlicuota As String
   Dim sqlTipoOper As String
   Dim sqlCountReng As String
   Dim sqlCountAlicG As String
   Dim sqlCountAlic2 As String
   Dim sqlCountAlic3 As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      If valAlicuota = eTD_ALICUOTA3 Then
         sqlMontoAlicuota = gUtilSQL.getIIF("Cxp.MontoGravableAlicuota3 <> 0 ", _
                        "alicuotaIVA.MontoAlicuota3", "0", True)
      ElseIf valAlicuota = eTD_ALICUOTA2 Then
         sqlMontoAlicuota = gUtilSQL.getIIF("Cxp.MontoGravableAlicuota2 <> 0 ", _
                        "alicuotaIVA.MontoAlicuota2", "0", True)
      Else
         sqlMontoAlicuota = gUtilSQL.getIIF("Cxp.MontoGravableAlicuotaGeneral <> 0 ", "alicuotaIVA.MontoAlicuotaGeneral", _
            gUtilSQL.getIIF("Cxp.MontoGravableAlicuotaEspecial1 <> 0 ", "Cxp.PorcentajeIvaAlicuotaEspecial1", _
            gUtilSQL.getIIF("Cxp.MontoGravableAlicuotaEspecial2 <> 0 ", "Cxp.PorcentajeIvaAlicuotaEspecial2", "0"), True))
      End If
      sqlTipoOper = "(" & "Cxp.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASEXENTAS)
      sqlTipoOper = sqlTipoOper & " OR " & "Cxp.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASEXONERADAS)
      sqlTipoOper = sqlTipoOper & " OR " & "Cxp.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASNOSUJETAS)
      sqlTipoOper = sqlTipoOper & " OR " & "Cxp.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASSINDERECHOACREDITO) & ")"
      sqlAlicuota = gUtilSQL.getIIF(sqlTipoOper, "0", sqlMontoAlicuota, True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      insRenglonFact.setClaseDeTrabajo eCTFC_Factura
      sqlTipoOper = "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_VENTA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVenta.eTD_EXPORTACION)
      sqlTipoOper = sqlTipoOper & " OR " & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_IVA & " = 0)"
      If mUsaMultiplesAlicuotas Then
         sqlCountReng = "(SELECT COUNT(*) FROM " & insRenglonFact.GetTableName
         sqlCountReng = sqlCountReng & " WHERE " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_NUMERO_FACTURA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
                     " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_CONSECUTIVO_COMPANIA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                     " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_TIPO_DE_DOCUMENTO & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & ")"
         sqlCountAlicG = "(SELECT COUNT(*) FROM " & insRenglonFact.GetTableName
         sqlCountAlicG = sqlCountAlicG & " WHERE " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_NUMERO_FACTURA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
                     " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_CONSECUTIVO_COMPANIA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                     " AND " & insRenglonFact.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
         sqlCountAlicG = sqlCountAlicG & " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_ALICUOTA_IVA & _
                     " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL) & ")"
         sqlCountAlic2 = "(SELECT COUNT(*) FROM " & insRenglonFact.GetTableName
         sqlCountAlic2 = sqlCountAlic2 & " WHERE " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_NUMERO_FACTURA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
                     " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_CONSECUTIVO_COMPANIA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                     " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_TIPO_DE_DOCUMENTO & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
         sqlCountAlic2 = sqlCountAlic2 & " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_ALICUOTA_IVA & _
                     " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA2) & ")"
         sqlCountAlic3 = "(SELECT COUNT(*) FROM " & insRenglonFact.GetTableName
         sqlCountAlic3 = sqlCountAlic3 & " WHERE " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_NUMERO_FACTURA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
                     " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_CONSECUTIVO_COMPANIA & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                     " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_TIPO_DE_DOCUMENTO & _
                     " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
         sqlCountAlic3 = sqlCountAlic3 & " AND " & insRenglonFact.GetTableName & "." & insRenglonFact.getFN_ALICUOTA_IVA & _
                     " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA3) & ")"
         sqlAlicuota = gUtilSQL.getIIF(sqlTipoOper, "0", gUtilSQL.getIIF(sqlCountReng & " = " & sqlCountAlic3, _
                     "alicuotaIVA.MontoAlicuota3", _
                     gUtilSQL.getIIF(sqlCountReng & " = " & sqlCountAlic2, _
                     "alicuotaIVA.MontoAlicuota2", _
                     "alicuotaIVA.MontoAlicuotaGeneral", True), True), True)
      Else
         sqlMontoAlicuota = "alicuotaIVA.MontoAlicuotaGeneral"
         sqlAlicuota = gUtilSQL.getIIF(sqlTipoOper, "0", sqlMontoAlicuota, True)
      End If
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlMontoAlicuota = "alicuotaIVA.MontoAlicuotaGeneral"
      sqlTipoOper = "(" & "cxC.MontoIVA = 0)"
      sqlAlicuota = gUtilSQL.getIIF(sqlTipoOper, "0", sqlMontoAlicuota)
   End If
h_EXIT:
   fSQLDecInfAlicuota = sqlAlicuota
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfAlicuota", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfNumeroDeExpediente(ByVal valTipoDeDocumento As Integer) As String
   Dim sqlNumeroDeExpediente As String
   Dim sqlNumExp As String
   Dim sqlComparacion As String
   Dim NumeroExpediente As String
   Dim i As Integer
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Or _
      valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
         NumeroExpediente = "Cxp.NumeroExpedienteDeImportacion"
      ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
         NumeroExpediente = insFactura.GetTableName & "." & insFactura.getFN_NUMERO_PLANILLA_EXPORTACION
      End If
'      If getIsSQLDataBase Then
'         sqlNumExp = "REPLACE(" & NumeroExpediente & ", '.', '')"
'         sqlNumExp = "REPLACE(" & sqlNumExp & ", '/', '')"
'         sqlNumExp = "REPLACE(" & sqlNumExp & ", '-', '')"
'         sqlNumExp = "REPLACE(" & sqlNumExp & ", '_', '')"
'      Else
         sqlNumExp = ""
         For i = 1 To 15
            sqlComparacion = "(" & gUtilSQL.DfMidSQL(NumeroExpediente, i, 1) & " = '.' OR "
            sqlComparacion = sqlComparacion & gUtilSQL.DfMidSQL(NumeroExpediente, i, 1) & " = '/' OR "
            sqlComparacion = sqlComparacion & gUtilSQL.DfMidSQL(NumeroExpediente, i, 1) & " = '-' OR "
            sqlComparacion = sqlComparacion & gUtilSQL.DfMidSQL(NumeroExpediente, i, 1) & " = '_' )"
            sqlNumExp = sqlNumExp & gUtilSQL.getIIF(sqlComparacion, "''", gUtilSQL.DfMidSQL(NumeroExpediente, i, 1), True)
            If i < 15 Then
               sqlNumExp = sqlNumExp & gUtilSQL.CharConcat
            End If
         Next
'      End If
      sqlNumeroDeExpediente = gUtilSQL.getIIF(NumeroExpediente & " = '' ", gUtilSQL.fSimpleSqlValue("0"), sqlNumExp, True)
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlNumeroDeExpediente = gUtilSQL.fSimpleSqlValue("0")
   End If
h_EXIT:
   fSQLDecInfNumeroDeExpediente = sqlNumeroDeExpediente
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfNumeroDeExpediente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfRifOriginal(ByVal valTipoDeDocumento As Integer) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      SQL = "proveedor.NumeroRIF"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or _
      valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Or _
      valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      SQL = "cliente.NumeroRIF"
   End If
h_EXIT:
   fSQLDecInfRifOriginal = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfRifOriginal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfClausulaFrom(ByVal valTipoDeDocumento As Integer) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      SQL = "alicuotaIVA, (CxP"
      SQL = SQL & " INNER JOIN proveedor"
      SQL = SQL & " ON " & "Cxp.ConsecutivoCompania" & _
                  " = " & "proveedor.ConsecutivoCompania"
      SQL = SQL & " AND " & "Cxp.CodigoProveedor" & _
                  " = " & "proveedor.CodigoProveedor)"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      SQL = "alicuotaIVA, (" & insFactura.GetTableName
      SQL = SQL & " INNER JOIN Cliente"
      SQL = SQL & " ON " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
                  " = " & "cliente.ConsecutivoCompania"
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & _
                  " = " & "cliente.Codigo"
      SQL = SQL & " INNER JOIN documentoCobrado"
      SQL = SQL & " ON " & insFactura.GetTableName & ".ConsecutivoCompania" & _
                  " = " & insFactura.GetTableName & ".ConsecutivoCompania"
      SQL = SQL & " AND " & insFactura.GetTableName & ".Numero" & _
                  " = " & "documentoCobrado.NumeroDelDocumentoCobrado)"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      SQL = "alicuotaIVA, (CxC"
      SQL = SQL & " INNER JOIN cliente"
      SQL = SQL & " ON " & "cxC.ConsecutivoCompania" & _
                  " = " & "cliente.ConsecutivoCompania"
      SQL = SQL & " AND " & "cxC.CodigoCliente" & _
                  " = " & "cliente.Codigo"
      SQL = SQL & " INNER JOIN documentoCobrado"
      SQL = SQL & " ON " & "cxC.ConsecutivoCompania" & _
                  " = " & "documentoCobrado.ConsecutivoCompania"
      SQL = SQL & " AND " & "cxC.Numero" & _
                  " = " & "documentoCobrado.NumeroDelDocumentoCobrado)"
   End If
h_EXIT:
   fSQLDecInfClausulaFrom = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfClausulaFrom", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfClausulaWhere(ByVal valTipoDeDocumento As Integer, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valSQLMontoIva As String, ByVal valConsecutivoCompania As Long, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean) As String
   Dim SQL As String
   Dim sqlCondicionDeAlicuota As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      sqlCondicionDeAlicuota = fSQLDecInfWhereAlicuota("Cxp.Fecha")
      SQL = "Cxp.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND " & "Cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
      SQL = SQL & " AND " & "Cxp.SeHizoLaRetencionIVA = " & gUtilSQL.fBooleanToSqlValue(True)
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Cxp.FechaAplicacionRetIVA", valFechaInicial, valFechaFinal)
      If valIncluirRegistrosMtoRetIvaCero = False Then
         SQL = SQL & " AND " & valSQLMontoIva & " <> 0"
      End If
      SQL = SQL & " AND " & sqlCondicionDeAlicuota
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Or valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlCondicionDeAlicuota = fSQLDecInfWhereAlicuota(insFactura.GetTableName & "." & insFactura.getFN_FECHA)
      SQL = insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & " = " & valConsecutivoCompania
      If valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
         SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
         SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      Else
         SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
         SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      End If
      SQL = SQL & " AND " & valSQLMontoIva & " <> 0 " 'esto es igual a "'se retuvo iva' = true"
      SQL = SQL & " AND " & sqlCondicionDeAlicuota
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      sqlCondicionDeAlicuota = fSQLDecInfWhereAlicuota("cxC.Fecha")
      SQL = "cxC.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND " & "cxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
      SQL = SQL & " AND " & "cxC.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(True)
      SQL = SQL & " AND " & valSQLMontoIva & " <> 0 "
      SQL = SQL & " AND " & "cxC.Origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
      SQL = SQL & " AND " & sqlCondicionDeAlicuota
   End If
h_EXIT:
   fSQLDecInfClausulaWhere = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfClausulaWhere", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfRifDelContribuyente(ByVal valNumeroDeRif As String) As String
   Dim RifDelContribuyente As String
   On Error GoTo h_ERROR
   RifDelContribuyente = valNumeroDeRif
   RifDelContribuyente = gTexto.fLimpiaRIFDeCaracteresNoValidos(RifDelContribuyente, False)
   If IsNumeric(gTexto.DfLeft(RifDelContribuyente, 1)) Then
      RifDelContribuyente = gTexto.DfRight("0000000000" & RifDelContribuyente, 10)
   Else
      RifDelContribuyente = gTexto.DfLeft(RifDelContribuyente, 1) & gTexto.DfRight("000000000" & gTexto.DfMid(RifDelContribuyente, 2, 9), 9)
   End If
   RifDelContribuyente = gUtilSQL.fSimpleSqlValue(RifDelContribuyente)
h_EXIT:
   fSQLDecInfRifDelContribuyente = RifDelContribuyente
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDecInfRifDelContribuyente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQLDecInfPeridoDeImposicion(ByVal valAno As String, ByVal valMes As String) As String
   Dim periodo As String
   On Error GoTo h_ERROR
   periodo = valAno
   periodo = periodo & gTexto.llenaConCaracterALaIzquierda(valMes, "0", 2)
   periodo = gUtilSQL.fSimpleSqlValue(periodo)
h_EXIT:
   fSQLDecInfPeridoDeImposicion = periodo
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDecInfPeridoDeImposicion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfMontoIva(ByVal valTipoDeDocumento As Integer, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
   Dim sqlMontoIva As String
   Dim sqlMtoRetenido As String
   Dim sqlIvaPorAlicuota As String
   Dim sqlTotalIva As String
   Dim SQL As String
   Dim varListaComparativa As String
   Dim varListaResultante As String
   Dim sqlFecha As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      If valAlicuota = eTD_ALICUOTA3 Then
         sqlIvaPorAlicuota = gUtilSQL.DfCDecSQL("Cxp.MontoIVAAlicuota3")
      ElseIf valAlicuota = eTD_ALICUOTA2 Then
         sqlIvaPorAlicuota = gUtilSQL.DfCDecSQL("Cxp.MontoIVAAlicuota2")
      Else
         sqlIvaPorAlicuota = gUtilSQL.getIIF(gUtilSQL.DfCDecSQL("CxP.MontoIVAAlicuotaEspecial1") & " <> 0", gUtilSQL.DfCDecSQL("MontoIVAAlicuotaEspecial1"), _
            gUtilSQL.getIIF(gUtilSQL.DfCDecSQL("MontoIVAAlicuotaEspecial2") & " <> 0", gUtilSQL.DfCDecSQL("MontoIVAAlicuotaEspecial2"), gUtilSQL.DfCDecSQL("Cxp.MontoIVAAlicuotaGeneral")))
      End If
      sqlMtoRetenido = gUtilSQL.DfCDecSQL("Cxp.MontoRetenido")
      sqlTotalIva = gUtilSQL.DfCDecSQL("Cxp.MontoIva")
      sqlMontoIva = "(" & sqlMtoRetenido & " * " & sqlIvaPorAlicuota & ") / " & sqlTotalIva
      sqlMontoIva = "ABS(" & sqlMontoIva & ")"
      sqlMontoIva = "(" & gUtilSQL.fRoundNDecimales(sqlMontoIva) & ")"
      sqlMontoIva = gUtilSQL.getIIF(sqlTotalIva & " = 0 ", "0", sqlMontoIva)
'      sqlMontoIva = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(valFechaFinal), sqlMontoIva, "Cxp.Fecha")
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      SQL = "(SELECT " & gUtilSQL.DfCDecSQL("documentoCobrado.MontoIvaRetenido")
      SQL = SQL & " FROM (" & "cobranza INNER JOIN "
      SQL = SQL & "documentoCobrado ON ("
      SQL = SQL & "cobranza.Numero" & _
                  " = " & "documentoCobrado.NumeroCobranza"
      SQL = SQL & ") AND (" & "cobranza.ConsecutivoCompania" & _
                  " = " & "documentoCobrado.ConsecutivoCompania"
      SQL = SQL & ")) INNER JOIN " & "cxC ON ("
      SQL = SQL & "documentoCobrado.NumeroDelDocumentoCobrado" & _
                  " = " & "cxC.Numero"
      SQL = SQL & ") AND (" & "documentoCobrado.TipoDeDocumentoCobrado" & _
                  " = " & "cxC.TipoCxC"
      SQL = SQL & ") AND (" & "documentoCobrado.ConsecutivoCompania" & _
                  " = " & "cxC.ConsecutivoCompania)"
      SQL = SQL & " WHERE " & "cxC.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND " & "cxC.NumeroDocumentoOrigen" & _
                  " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
      SQL = SQL & " AND documentoCobrado.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(True)
      'SQL = SQL & " AND " & "cxC.TipoCxC = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & _
                  gUtilSQL.getIIF("cxC.TipoCxC" & _
                           " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                           gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO), _
                  gUtilSQL.getIIF("cxC.TipoCxC" & _
                           " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                           gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO), _
                           gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA), True), True)
      SQL = SQL & " AND " & "cxC.Origen" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_FACTURA)
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", _
                  valFechaInicial, valFechaFinal)
      SQL = SQL & " AND " & "cobranza.StatusCobranza" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & ")"
      sqlMontoIva = "ABS(" & SQL & ")"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlMontoIva = "ABS(" & gUtilSQL.DfCDecSQL(insFactura.GetTableName & "." & insFactura.getFN_MONTO_IVA_RETENIDO) & ")"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      SQL = "(SELECT " & gUtilSQL.DfCDecSQL("documentoCobrado.MontoIvaRetenido")
      SQL = SQL & " FROM " & "cobranza INNER JOIN "
      SQL = SQL & "documentoCobrado ON ("
      SQL = SQL & "cobranza.Numero" & _
                  " = " & "documentoCobrado.NumeroCobranza"
      SQL = SQL & ") AND (" & "cobranza.ConsecutivoCompania" & _
                  " = " & "documentoCobrado.ConsecutivoCompania)"
      SQL = SQL & " WHERE " & "documentoCobrado.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND documentoCobrado.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(True)
      SQL = SQL & " AND " & "documentoCobrado.NumeroDelDocumentoCobrado" & _
                  " = " & "cxC.Numero"
      SQL = SQL & " AND " & "documentoCobrado.TipoDeDocumentoCobrado" & _
                  " = " & "cxC.TipoCxC"
      SQL = SQL & " AND " & "cxC.Origen" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", _
                  valFechaInicial, valFechaFinal)
      SQL = SQL & " AND " & "cobranza.StatusCobranza" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & ")"
      sqlMontoIva = "ABS(" & SQL & ")"
      sqlMontoIva = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(valFechaFinal), sqlMontoIva, "cxC.Fecha")
   End If
h_EXIT:
   fSQLDecInfMontoIva = sqlMontoIva
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfMontoIva", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfCondicionMontoIvaRetenidoDifCero(ByVal valTipoDeDocumento As Integer, ByVal valAlicuota As enum_TipoDeAlicuota, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long) As String
'   Dim sqlMontoIVA As String
   Dim sqlMtoRetenido As String
   Dim sqlIvaPorAlicuota As String
'   Dim sqlTotalIva As String
   Dim SQL As String
   Dim sqlCondicion As String
   On Error GoTo h_ERROR
   If valTipoDeDocumento = CM_DecInf_Compras_Desde_CxP Then
      If valAlicuota = eTD_ALICUOTA3 Then
         sqlIvaPorAlicuota = "Cxp.MontoIVAAlicuota3"
      ElseIf valAlicuota = eTD_ALICUOTA2 Then
         sqlIvaPorAlicuota = "Cxp.MontoIVAAlicuota2"
      Else
         sqlIvaPorAlicuota = gUtilSQL.getIIF("Cxp.MontoIVAAlicuotaEspecial1 <> 0", "Cxp.MontoIVAAlicuotaEspecial1", _
            gUtilSQL.getIIF("Cxp.MontoIVAAlicuotaEspecial2 <> 0", "Cxp.MontoIVAAlicuotaEspecial2", "Cxp.MontoIVAAlicuotaGeneral"))
      End If
      sqlMtoRetenido = "Cxp.MontoRetenido"
      sqlCondicion = sqlMtoRetenido & " <> 0 AND " & sqlIvaPorAlicuota
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Factura Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      SQL = "(SELECT documentoCobrado.MontoIvaRetenido"
      SQL = SQL & " FROM (cobranza INNER JOIN "
      SQL = SQL & "documentoCobrado ON ("
      SQL = SQL & "cobranza.Numero" & _
                  " = " & "documentoCobrado.NumeroCobranza"
      SQL = SQL & ") AND (cobranza.ConsecutivoCompania" & _
                  " = documentoCobrado.ConsecutivoCompania"
      SQL = SQL & ")) INNER JOIN cxC ON ("
      SQL = SQL & "documentoCobrado.NumeroDelDocumentoCobrado" & _
                  " = " & "cxC.Numero"
      SQL = SQL & ") AND (documentoCobrado.TipoDeDocumentoCobrado" & _
                  " = cxC.TipoCxC"
      SQL = SQL & ") AND (documentoCobrado.ConsecutivoCompania" & _
                  " = " & "cxC.ConsecutivoCompania)"
      SQL = SQL & " WHERE " & "cxC.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND cxC.NumeroDocumentoOrigen" & _
                  " = " & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
      SQL = SQL & " AND documentoCobrado.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(True)
      'SQL = SQL & " AND " & "cxC.TipoCxC = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
      SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " = " & _
                  gUtilSQL.getIIF("cxC.TipoCxC" & _
                           " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                           gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO), _
                  gUtilSQL.getIIF("cxC.TipoCxC" & _
                           " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                           gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO), _
                           gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA), True), True)
      SQL = SQL & " AND " & "cxC.Origen" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_FACTURA)
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", _
                  valFechaInicial, valFechaFinal)
      SQL = SQL & " AND " & "cobranza.StatusCobranza" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & ")"
      sqlCondicion = "ABS(" & SQL & ")"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_Resumen_Ventas Then
      insFactura.setClaseDeTrabajo eCTFC_Factura
      sqlCondicion = "ABS(" & insFactura.GetTableName & "." & insFactura.getFN_MONTO_IVA_RETENIDO & ")"
   ElseIf valTipoDeDocumento = CM_DecInf_Ventas_Desde_CxC Then
      SQL = "(SELECT " & "documentoCobrado.MontoIvaRetenido"
      SQL = SQL & " FROM " & "cobranza INNER JOIN "
      SQL = SQL & "documentoCobrado ON ("
      SQL = SQL & "cobranza.Numero" & _
                  " = " & "documentoCobrado.NumeroCobranza"
      SQL = SQL & ") AND (" & "cobranza.ConsecutivoCompania" & _
                  " = " & "documentoCobrado.ConsecutivoCompania)"
      SQL = SQL & " WHERE " & "documentoCobrado.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND " & "documentoCobrado.NumeroDelDocumentoCobrado" & _
                  " = " & "cxC.Numero"
      SQL = SQL & " AND " & "cxC.Origen" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", _
                  valFechaInicial, valFechaFinal)
      SQL = SQL & " AND " & "cobranza.StatusCobranza" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & ")"
      sqlCondicion = "ABS(" & SQL & ")"
   End If
h_EXIT:
   fSQLDecInfCondicionMontoIvaRetenidoDifCero = sqlCondicion
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfCondicionMontoIvaRetenidoDifCero", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function CM_TipoOperacionAExportarToString(ByVal valTipoOperacionAExportar As Integer) As String
   On Error GoTo h_ERROR
   Select Case valTipoOperacionAExportar
      Case CM_TOE_Compras: CM_TipoOperacionAExportarToString = "Compras"
      Case CM_TOE_ComprasYVentas: CM_TipoOperacionAExportarToString = "Compras Y Ventas"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "CM_TipoOperacionAExportarToString", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLDecInfWhereAlicuota(ByVal valTableNameFecha As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "alicuotaIVA.FechaDeInicioDeVigencia"
   SQL = SQL & " = " & "(SELECT MAX("
   SQL = SQL & "alicuotaIVA.FechaDeInicioDeVigencia"
   SQL = SQL & ") FROM alicuotaIVA"
   SQL = SQL & " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia"
   SQL = SQL & " <= " & valTableNameFecha & ")"
h_EXIT:
   fSQLDecInfWhereAlicuota = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDecInfWhereAlicuota", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelComprobanteResumenIVA(ByVal valCodigoDeProveedor As String, ByVal valQuincenaAGenerar As String, _
                           ByVal valCantidadAImprimir As Boolean, ByVal valIncluirRegistrosMtoRetIvaCero As Boolean, _
                                 ByVal valAno As String, ByVal valMes As String, ByVal valConsecutivoCompania As Long, _
                                            ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim fechaIni As Date
   Dim fechaFin As Date
   Dim sqlTotalCxP As String
   Dim sqlMontoGravado As String
   Dim sqlMontoExento As String
   Dim sqlMontoGravableAlicuotaGeneral As String
   Dim sqlMontoGravableAlicuota2 As String
   Dim sqlMontoGravableAlicuota3 As String
   Dim sqlMontoIva As String
   Dim sqlMontoIVAALicuotaGeneral As String
   Dim sqlMontoIVAALicuota2 As String
   Dim sqlMontoIVAALicuota3 As String
   Dim sqlCampoCambio As String
   Dim sqlCampoMoneda As String
   Dim insCxPSQL As clsCxpSQL
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim sqlAlicuotaG As String
   On Error GoTo h_ERROR
   Set insCxPSQL = New clsCxpSQL
   Set gEnumProyecto = New clsEnumAdministrativo
   If valQuincenaAGenerar = CM_QuincenaDeclaracionInformativaToString(CM_QDI_PrimeraQuincena) Then
      fechaIni = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, True)
      fechaFin = gUtilDate.fColocaDiaEnFecha(15, gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, True))
   ElseIf valQuincenaAGenerar = CM_QuincenaDeclaracionInformativaToString(CM_QDI_SegundaQuincena) Then
      fechaIni = gUtilDate.fColocaDiaEnFecha(16, gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, True))
      fechaFin = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)
   Else
      fechaIni = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, True)
      fechaFin = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)
   End If
   sqlCampoCambio = gUtilSQL.getIIF("CxP.OrigenDeLaRetencion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
         "CxP.CambioABolivares", fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania), True)
   sqlCampoMoneda = gUtilSQL.getIIF("CxP.OrigenDeLaRetencion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
         "CxP.Moneda", fSubSQLDecInfCambioOMonedaOriginalDesdePago(False, valConsecutivoCompania), True)
   sqlTotalCxP = "(" & "CxP.MontoExento" & _
               " + " & "CxP.MontoGravado" & _
               " + " & "CxP.MontoIva)"
   sqlMontoExento = "CxP.MontoExento"
   sqlMontoGravado = "CxP.MontoGravado"
   sqlMontoGravableAlicuotaGeneral = "CxP.MontoGravableAlicuotaGeneral"
   sqlMontoGravableAlicuota2 = "CxP.MontoGravableAlicuota2"
   sqlMontoGravableAlicuota3 = "CxP.MontoGravableAlicuota3"
   sqlMontoIva = "CxP.MontoIva"
   sqlMontoIVAALicuotaGeneral = "CxP.MontoIVAAlicuotaGeneral"
   sqlMontoIVAALicuota2 = "CxP.MontoIVAAlicuota2"
   sqlMontoIVAALicuota3 = "CxP.MontoIVAAlicuota3"
   sqlTotalCxP = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlTotalCxP, True, "")
   sqlMontoExento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoExento, True, "")
   sqlMontoGravado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravado, True, "")
   sqlMontoGravableAlicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuotaGeneral, True, "")
   sqlMontoGravableAlicuotaGeneral = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 <> 0", "cxP.MontoGravableAlicuotaEspecial1", "cxP.MontoGravableAlicuotaEspecial2", True), sqlMontoGravableAlicuotaGeneral, True)
   sqlMontoGravableAlicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota2, True, "")
   sqlMontoGravableAlicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota3, True, "")
   sqlMontoIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIva, True, "")
   sqlMontoIVAALicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuotaGeneral, True, "")
   sqlMontoIVAALicuotaGeneral = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoIVAAlicuotaEspecial1 <> 0", "cxP.MontoIVAAlicuotaEspecial1", "cxP.MontoIVAAlicuotaEspecial2", True), sqlMontoIVAALicuotaGeneral, True)
   sqlMontoIVAALicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota2, True, "")
   sqlMontoIVAALicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota3, True, "")
   sqlAlicuotaG = "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuotaGeneral FROM alicuotaIVA WHERE alicuotaIVA.FechaDeInicioDeVigencia <= CxP.Fecha ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC)"
   sqlAlicuotaG = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.PorcentajeIvaAlicuotaEspecial1 <> 0", "cxP.PorcentajeIvaAlicuotaEspecial1", "cxP.PorcentajeIvaAlicuotaEspecial2", True), sqlAlicuotaG, True)
   SQL = "SELECT "
   SQL = SQL & sqlTotalCxP & " As TotalCXPComprobanteRetIva, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "proveedor.CodigoProveedor, "
   SQL = SQL & "CxP.FechaAplicacionRetIVA, "
   SQL = SQL & "CxP.MesDeAplicacion, "
   SQL = SQL & "CxP.AnoDeAplicacion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfCIntSQL("CxP.NumeroComprobanteRetencion") & " <> " & _
                               "0", "CxP.NumeroComprobanteRetencion", _
                               gUtilSQL.fSimpleSqlValue("Sin N° Asignado"), True) & _
               " AS NumeroComprobanteRetencion, "
   SQL = SQL & "CxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "CxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeFactura, "
   SQL = SQL & "CxP.NumeroControl, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeTransaccion", _
                        enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
                        gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "CxP.NumeroDeFacturaAfectada, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, "
   SQL = SQL & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   SQL = SQL & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   SQL = SQL & sqlAlicuotaG & " AS AlicuotaG, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota2, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota3, "
   SQL = SQL & sqlMontoIva & " AS MontoIva, "
   SQL = SQL & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral, "
   SQL = SQL & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2, "
   SQL = SQL & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3, "
   SQL = SQL & "CxP.MontoRetenido, "
   SQL = SQL & "CxP.MontoRetenido AS MontoRetenidoIVA, "
   SQL = SQL & insCxPSQL.fSQLAnoYMesDeAplicacionIVAParaComprobante
   SQL = SQL & ", CxP.NumeroComprobanteRetencion"
   SQL = SQL & " FROM proveedor "
   SQL = SQL & " INNER JOIN CxP ON ("
   SQL = SQL & "proveedor.CodigoProveedor = CxP.CodigoProveedor"
   SQL = SQL & ") AND (" & "proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania)"
   SQL = SQL & " WHERE " & "proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxP.FechaAplicacionRetIVA", fechaIni, fechaFin)
   If valCantidadAImprimir Then
      SQL = SQL & " AND " & "proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeProveedor))
   End If
   SQL = SQL & " AND " & "CxP.SeHizoLaRetencionIVA = " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " AND " & "CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   If valIncluirRegistrosMtoRetIvaCero = False Then
      SQL = SQL & " AND " & "CxP.MontoRetenido <> 0 "
   End If
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & sqlTotalCxP & " As TotalCXPComprobanteRetIva, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "proveedor.CodigoProveedor, "
   SQL = SQL & "CxP.FechaAplicacionRetIVA, "
   SQL = SQL & "CxP.MesDeAplicacion, "
   SQL = SQL & "CxP.AnoDeAplicacion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfCIntSQL("CxP.NumeroComprobanteRetencion") & " <> " & _
                               "0", "CxP.NumeroComprobanteRetencion", _
                               gUtilSQL.fSimpleSqlValue("Sin N° Asignado"), True) & _
               " AS NumeroComprobanteRetencion, "
   SQL = SQL & "CxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "CxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeFactura, "
   SQL = SQL & "CxP.NumeroControl, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeTransaccion", _
                        enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
                        gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "CxP.NumeroDeFacturaAfectada, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, "
   SQL = SQL & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   SQL = SQL & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   SQL = SQL & sqlAlicuotaG & " AS AlicuotaG, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota2, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota3, "
   SQL = SQL & sqlMontoIva & " AS MontoIva, "
   SQL = SQL & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral, "
   SQL = SQL & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2, "
   SQL = SQL & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3, "
   SQL = SQL & "CxP.MontoRetenido, "
   SQL = SQL & "CxP.MontoRetenido AS MontoRetenidoIVA, "
   SQL = SQL & insCxPSQL.fSQLAnoYMesDeAplicacionIVAParaComprobante
   SQL = SQL & ", CxP.NumeroComprobanteRetencion"
   SQL = SQL & " FROM proveedor "
   SQL = SQL & " INNER JOIN CxP ON ("
   SQL = SQL & "proveedor.CodigoProveedor = CxP.CodigoProveedorOriginalServicio"
   SQL = SQL & ") AND (" & "proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania)"
   SQL = SQL & " WHERE " & "proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxP.FechaAplicacionRetIVA", fechaIni, fechaFin)
   If valCantidadAImprimir Then
      SQL = SQL & " AND " & "proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeProveedor))
   End If
   SQL = SQL & " AND " & "CxP.SeHizoLaRetencionIVA = " & gUtilSQL.fBooleanToSqlValue(True)
   SQL = SQL & " AND " & "CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   If valIncluirRegistrosMtoRetIvaCero = False Then
      SQL = SQL & " AND " & "CxP.MontoRetenido <> 0 "
   End If
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   
h_EXIT:
   Set insCxPSQL = Nothing
   Set gEnumProyecto = Nothing
   On Error GoTo 0
   fSQLDelComprobanteResumenIVA = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelComprobanteResumenIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function CM_QuincenaDeclaracionInformativaToString(ByVal valQuincenaADeclarar As Integer) As String
   On Error GoTo h_ERROR
   Select Case valQuincenaADeclarar
      Case CM_QDI_PrimeraQuincena: CM_QuincenaDeclaracionInformativaToString = "Primera Quincena"
      Case CM_QDI_SegundaQuincena: CM_QuincenaDeclaracionInformativaToString = "Segunda Quincena"
      Case CM_QDI_TodoElMes: CM_QuincenaDeclaracionInformativaToString = "Todo el Mes"
   End Select
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "enumQuincenaDeclaracionInformativaToString", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fGetQueryPagosDeclracionMensualXML(ByVal valMesDeclaracion As Integer, ByVal valAnoDeclaracion As Long, ByVal valExistenRegistros As Boolean, ByVal valTipoDeclaracion As Integer, _
               ByVal valParaInformes As Boolean, ByVal valConsecutivoCompania As Long, ByVal valNumeroDeRif As String, _
                  ByVal valMostrarMontoRetencionEnCero As Boolean, ByVal valFechaIniciodeVigenciaTablaRetencion As Date) As String
   Dim vFechaDesde As Date
   Dim vFechaHasta As Date
   Dim SQL As String
   Dim vSQLLimpiaDoc As String
   Dim vSQLLimpiaControl As String
   Dim insPagoVista As clsPagoVista
   Dim vSQLOrderBy As String
   Dim vSqlVistaParaUsar As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
   Set insPagoVista = New clsPagoVista
   vFechaDesde = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMesDeclaracion), "0", 2), gConvert.fConvierteAString(valAnoDeclaracion), True)
   vFechaHasta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMesDeclaracion), "0", 2), gConvert.fConvierteAString(valAnoDeclaracion), False)
   Select Case valTipoDeclaracion
      Case ePagoOPT_TIPO_SALARIOS_OTROS
         vSQLOrderBy = " ORDER BY RifRetenido, CodigoConcepto, NumeroFactura"
         vSqlVistaParaUsar = insPagoVista.GetViewNamePagosDeclracionMensualXMLSalariosOtros
      Case ePagoOPT_TIPO_DIVIDENDOS_ACCIONES
         vSQLOrderBy = " ORDER BY RifRetenido"
         vSqlVistaParaUsar = insPagoVista.GetViewNamePagosDeclracionMensualXMLDividendoYAcciones
   End Select
   SQL = " SELECT "
   If valParaInformes Then
'      SQL = SQL & " ROW_NUMBER() OVER(" & vSQLOrderBy & ") AS NumeroRegistro, "
   End If
   SQL = SQL & " RifRetenido, "
   If Not valParaInformes Then
      Select Case valTipoDeclaracion
      Case ePagoOPT_TIPO_DIVIDENDOS_ACCIONES
         SQL = SQL & "Tipo, "
      Case ePagoOPT_TIPO_SALARIOS_OTROS
         SQL = SQL & gUtilSQL.DfRightSQL("NumeroFactura", 10) & " AS NumeroFactura, "
         SQL = SQL & gUtilSQL.DfRightSQL("NumeroControl", 10) & " AS NumeroControl, "
         SQL = SQL & gUtilSQL.DfSQLFormat("Fecha", "DD/MM/YYYY", "FechaOperacion") & ", "
         SQL = SQL & "CodigoConcepto, "
      End Select
   Else
      SQL = SQL & "NumeroReferencia, "
      SQL = SQL & gUtilSQL.DfRightSQL("NumeroFactura", 10) & " AS NumeroFactura, "
      SQL = SQL & gUtilSQL.DfRightSQL("NumeroControl", 10) & " AS NumeroControl, "
      SQL = SQL & gUtilSQL.DfSQLFormat("Fecha", "DD/MM/YYYY", "FechaOperacion") & ", "
      SQL = SQL & "CodigoRetencion, "
      Select Case valTipoDeclaracion
      Case ePagoOPT_TIPO_DIVIDENDOS_ACCIONES
         SQL = SQL & "Tipo, "
      Case ePagoOPT_TIPO_SALARIOS_OTROS
         SQL = SQL & "CodigoConcepto, "
      End Select
      SQL = SQL & "MontoRetencion, "
      SQL = SQL & "MarcaNoDocumento, "
      SQL = SQL & "MarcaNoControl, "
   End If
   SQL = SQL & "MontoOperacion, "
   SQL = SQL & "PorcentajeRetencion, "
   SQL = SQL & "NumeroFactura as NumeroFacturaSinTruncar, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen(vSqlVistaParaUsar, "NumeroCxpOriginal") & "> 10", gUtilSQL.fBooleanToSqlValue(True), gUtilSQL.fBooleanToSqlValue(False), True) & " AS ExedeLimite"
   'SQL = SQL & "case when Len(NumeroReferencia)>10 then 'S' else 'N' end as ExedeLimite "
   SQL = SQL & " FROM "
   Select Case valTipoDeclaracion
   Case ePagoOPT_TIPO_DIVIDENDOS_ACCIONES
      SQL = SQL & insPagoVista.GetViewNamePagosDeclracionMensualXMLDividendoYAcciones
   Case ePagoOPT_TIPO_SALARIOS_OTROS
      SQL = SQL & insPagoVista.GetViewNamePagosDeclracionMensualXMLSalariosOtros
   End Select
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "ConsecutivoCompania", valConsecutivoCompania)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "MesAplicacion", valMesDeclaracion)
   vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "AnoAplicacion", valAnoDeclaracion)
   If valTipoDeclaracion = ePagoOPT_TIPO_SALARIOS_OTROS Then
      vSQLWhere = gUtilSQL.fSQLValueDate(vSQLWhere, "FechaDeInicioDeVigencia", valFechaIniciodeVigenciaTablaRetencion, "=")
   End If
   If Not valMostrarMontoRetencionEnCero Then
      vSQLWhere = vSQLWhere & " AND MontoRetencion > 0"
'      vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "MontoRetencion", "0", ">")
   End If
   
'   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween("Fecha", gUtilSQL.fSimpleSqlValue(vFechaDesde), gUtilSQL.fSimpleSqlValue(vFechaHasta))
   If Not valExistenRegistros And Not valParaInformes Then
      SQL = fSQLNoExistenRegistros(valTipoDeclaracion, valNumeroDeRif)
   Else
      SQL = SQL & gUtilSQL.getWhereSQL(vSQLWhere)
   End If
   SQL = SQL & vSQLOrderBy
   fGetQueryPagosDeclracionMensualXML = SQL
   Set insPagoVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fGetQueryPagosDeclracionMensualXML", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLGananciasFortuitas(ByVal valMesDeclaracion As String, ByVal valAnoDeclaracion As String, ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vFechaDesde As Date
   Dim vFechaHasta As Date
   Dim insPagoVista As clsPagoVista
   On Error GoTo h_ERROR
   Set insPagoVista = New clsPagoVista
   vFechaDesde = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMesDeclaracion), "0", 2), gConvert.fConvierteAString(valAnoDeclaracion), True)
   vFechaHasta = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(gTexto.llenaConCaracterALaIzquierda(gConvert.fConvierteAString(valMesDeclaracion), "0", 2), gConvert.fConvierteAString(valAnoDeclaracion), False)
   vSQL = ""
   vSQL = "SELECT "
   vSQL = vSQL & "SUM (" & gUtilSQL.fIfResultIsNullReplace("PagadoGanancias16 ", gUtilSQL.fNumToStrSQL(0)) & ") AS PagadoGanancias16, "
   vSQL = vSQL & "SUM (" & gUtilSQL.fIfResultIsNullReplace("retGanancias16 ", gUtilSQL.fNumToStrSQL(0)) & ") AS retGanancias16, "
   vSQL = vSQL & "SUM (" & gUtilSQL.fIfResultIsNullReplace("PagadoGanancias34 ", gUtilSQL.fNumToStrSQL(0)) & ") AS PagadoGanancias34, "
   vSQL = vSQL & "SUM (" & gUtilSQL.fIfResultIsNullReplace("retGanancias34 ", gUtilSQL.fNumToStrSQL(0)) & ") AS retGanancias34 "
   vSQL = vSQL & " FROM  " & insPagoVista.GetViewNamePagosDeclracionMensualXMLGananciasFortuitas
   vSQL = vSQL & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND MesAplicacion = " & gConvert.ConvierteAlong(valMesDeclaracion)
   vSQL = vSQL & " AND AnoAplicacion = " & gConvert.ConvierteAlong(valAnoDeclaracion)
   vSQL = vSQL & " AND " & gUtilSQL.fSQLValueBetween("Fecha", gUtilSQL.fSimpleSqlValue(vFechaDesde), gUtilSQL.fSimpleSqlValue(vFechaHasta))
   fSQLGananciasFortuitas = vSQL
   Set insPagoVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLGananciasFortuitas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLNoExistenRegistros(ByVal valTipoDeclaracion As Integer, ByVal valNumeroDeRif As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   Select Case valTipoDeclaracion
      Case ePagoOPT_TIPO_DIVIDENDOS_ACCIONES
         SQL = SQL & gUtilSQL.fSimpleSqlValue(valNumeroDeRif) & " AS RifRetenido, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue("00") & " AS Tipo, "
         SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "AS MontoOperacion, "
         SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "PorcentajeRetencion"
      Case ePagoOPT_TIPO_SALARIOS_OTROS
         SQL = SQL & gUtilSQL.fSimpleSqlValue(valNumeroDeRif) & " AS RifRetenido, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS NumeroFactura, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue("NA") & " AS NumeroControl, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue("000") & " AS CodigoConcepto, "
         SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "MontoOperacion, "
         SQL = SQL & gUtilSQL.fNumToStrSQL(0) & "PorcentajeRetencion"
   End Select
   fSQLNoExistenRegistros = SQL
h_EXIT:    On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLNoExistenRegistros", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSubSQLDecInfCambioOMonedaOriginalDesdePago(ByVal valQuieroElCampoCambio As Boolean, ByVal valConsecutivoCompania As Long, Optional ByVal valIncluirMontoComprobantesAnulados As Boolean, Optional ByVal valIncluirRegistrosMtoRetIvaCero As Boolean = False) As String
   Dim sqlCampoPago As String
   Dim Mensaje As String
   On Error GoTo h_ERROR
   Mensaje = "fSubSQLDecInfCambioOMonedaOriginalDesdePago" & vbCrLf & vbCrLf
   Mensaje = Mensaje & "De ser posible, no programe más subconsultas." & vbCrLf & vbCrLf
   Mensaje = Mensaje & "Esta función genera una subconsulta, " & vbCrLf
   Mensaje = Mensaje & "al usar cualquier subconsulta, recuerde chequear todas las cláusulas de unicidad" & vbCrLf
   gMessage.ProgrammerMessage Mensaje
   If valIncluirMontoComprobantesAnulados Then
      sqlCampoPago = "(SELECT "
      If valQuieroElCampoCambio Then
         sqlCampoPago = sqlCampoPago & " documentoPagado.CambioAMonedaDelPago"
      Else
         sqlCampoPago = sqlCampoPago & " documentoPagado.CodigoMonedaDeCxP "
      End If
   Else
      sqlCampoPago = "(SELECT (CASE WHEN pago.StatusOrdenDePago = " _
                      & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE) & " THEN "
      If valQuieroElCampoCambio Then
         sqlCampoPago = sqlCampoPago & " documentoPagado.CambioAMonedaDelPago"
         sqlCampoPago = sqlCampoPago & " ELSE 0 "
      Else
         sqlCampoPago = sqlCampoPago & " documentoPagado.CodigoMonedaDeCxP "
         sqlCampoPago = sqlCampoPago & " ELSE 'NADA' "
      End If
      sqlCampoPago = sqlCampoPago & " END)"
   End If
   sqlCampoPago = sqlCampoPago & " FROM documentoPagado INNER JOIN Pago"
   sqlCampoPago = sqlCampoPago & " ON pago.ConsecutivoCompania = documentoPagado.ConsecutivoCompania"
   sqlCampoPago = sqlCampoPago & " AND pago.NumeroComprobante = documentoPagado.NumeroComprobante"
   sqlCampoPago = sqlCampoPago & " WHERE documentoPagado.ConsecutivoCompania = " & valConsecutivoCompania
   sqlCampoPago = sqlCampoPago & " AND documentoPagado.NumeroDelDocumentoPagado = cxP.Numero"
   sqlCampoPago = sqlCampoPago & " AND pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   sqlCampoPago = sqlCampoPago & " AND pago.Fecha = cxP.FechaAplicacionRetIVA"
   sqlCampoPago = sqlCampoPago & " AND pago.CodigoProveedor = cxP.CodigoProveedor"
   If valIncluirRegistrosMtoRetIvaCero = False Then
      sqlCampoPago = sqlCampoPago & " AND documentoPagado.MontoIvaRetenido <> 0 "
   End If
   sqlCampoPago = sqlCampoPago & ")"
h_EXIT:
   fSubSQLDecInfCambioOMonedaOriginalDesdePago = sqlCampoPago
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSubSQLDecInfCambioOMonedaOriginalDesdePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteComprobanteDePago(ByVal valCodigoBanco As Long, ByVal ValCodigoProveedor As String, _
                                                         ByVal valNumeroComprobante As Long, ByVal valConsecutivoCompania As Long, _
                                                         ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object, _
                                                         ByVal valComprobanteAImprimir As enum_QueImprimirDesdePago, _
                                                         ByVal valUsaMonedaLocal As Boolean) As String
   Dim SQL As String
   Dim sqlCampoCambio As String
   Dim sqlCampoMoneda As String
   Dim sqlTotalCxP As String
   Dim sqlTotalCxPComprobanteRetIva As String
   Dim sqlMontoExento  As String
   Dim sqlMontoGravado As String
   Dim sqlMontoGravableAlicuotaGeneral As String
   Dim sqlMontoGravableAlicuota2 As String
   Dim sqlMontoGravableAlicuota3 As String
   Dim sqlMontoIva As String
   Dim sqlMontoIVAALicuotaGeneral As String
   Dim sqlMontoIVAALicuota2 As String
   Dim sqlMontoIVAALicuota3 As String
   Dim sqlMontoRetenidoCompDePago As String
   Dim sqlMontoRetenidoCompDeIVA As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim insCxPSQL As clsCxpSQL
   Dim sqlMontoAbonado As String
   Dim vMontoGeneral As String
   Dim vmontoreducida As String
   Dim vmontoadicional As String
   Dim vMontoAlicuotaIVAGeneral As String
   Dim vMontoAlicuotaIVAreducida As String
   Dim vMontoAlicuotaIVAadicional As String
   
   
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insCxPSQL = New clsCxpSQL
   sqlCampoCambio = gUtilSQL.getIIF("cxp.OrigenDeLaRetencion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP) & _
         " AND cxP.AnoDeAplicacion < " & gUtilDate.fYear(gMonedaLocalActual.GetHoyVigenteDesde), _
         "cxp.CambioABolivares", _
         gUtilSQL.getIIF("cxp.OrigenDeLaRetencion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_PAGO) & _
         " AND cxP.AnoDeAplicacion < " & gUtilDate.fYear(gMonedaLocalActual.GetHoyVigenteDesde) _
         , "cxp.CambioaBolivares", "cxp.CambioABolivares", True), True)
   sqlCampoMoneda = gUtilSQL.getIIF("cxP.OrigenDeLaRetencion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
         "cxp.CodigoMoneda", "pago.CodigoMoneda", True)
   sqlTotalCxP = "(cxP.MontoExento + cxP.MontoGravado + cxP.MontoIva)"
   sqlTotalCxPComprobanteRetIva = sqlTotalCxP
   sqlMontoExento = "cxP.MontoExento"
   sqlMontoGravado = "cxP.MontoGravado"
   
   vMontoGeneral = "cxP.MontoGravableAlicuotaGeneral "
   vmontoreducida = "cxP.MontoGravableAlicuotaEspecial2"
   vmontoadicional = "cxP.MontoGravableAlicuotaEspecial1"
   vMontoAlicuotaIVAGeneral = "cxP.MontoIVAAlicuotaGeneral "
   vMontoAlicuotaIVAreducida = "cxP.MontoIVAAlicuotaEspecial2"
   vMontoAlicuotaIVAadicional = "cxP.MontoIVAAlicuotaEspecial1"
   
   sqlMontoGravableAlicuotaGeneral = gUtilSQL.getIIF(vMontoGeneral & " <> " & "0", vMontoGeneral, gUtilSQL.getIIF(vmontoreducida & " <> " & "0", vmontoreducida, vmontoadicional))
   sqlMontoGravableAlicuota2 = "cxP.MontoGravableAlicuota2"
   sqlMontoGravableAlicuota3 = "cxP.MontoGravableAlicuota3"
   sqlMontoIva = "cxP.MontoIva"
   sqlMontoIVAALicuotaGeneral = gUtilSQL.getIIF(vMontoAlicuotaIVAGeneral & " <> " & "0", vMontoAlicuotaIVAGeneral, gUtilSQL.getIIF(vMontoAlicuotaIVAreducida & " <> " & "0", vMontoAlicuotaIVAreducida, vMontoAlicuotaIVAadicional))
   sqlMontoIVAALicuota2 = "cxP.MontoIVAAlicuota2"
   sqlMontoIVAALicuota3 = "cxP.MontoIVAAlicuota3"
   sqlMontoRetenidoCompDePago = "cxP.MontoRetenido"
   sqlTotalCxPComprobanteRetIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlTotalCxPComprobanteRetIva, True, "")
   sqlMontoExento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoExento, True, "")
   sqlMontoGravado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravado, True, "")
   sqlMontoGravableAlicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuotaGeneral, True, "")
   sqlMontoGravableAlicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota2, True, "")
   sqlMontoGravableAlicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota3, True, "")
   sqlMontoIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIva, True, "")
   sqlMontoIVAALicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuotaGeneral, True, "")
   sqlMontoIVAALicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota2, True, "")
   sqlMontoIVAALicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota3, True, "")
   If valComprobanteAImprimir = eQI_PAGO_MAS_CHEQUE Or eQI_PAGO_MAS_CHEQUE Then
      If valUsaMonedaLocal Then
         sqlMontoRetenidoCompDePago = "cxp.MontoRetenido"
         sqlMontoRetenidoCompDeIVA = "cxp.MontoRetenido"
         sqlMontoAbonado = "documentoPagado.MontoAbonado, "
         sqlTotalCxP = sqlTotalCxP & " / Pago.CambioABolivares"
      Else
         sqlMontoRetenidoCompDePago = "cxp.MontoRetenido / Pago.CambioABolivares"
         sqlMontoRetenidoCompDeIVA = "cxp.MontoRetenido"
         sqlMontoAbonado = "documentoPagado.MontoAbonado AS MontoAbonado, "
         sqlTotalCxP = gUtilSQL.getIIF("cxP.CodigoMoneda = Pago.CodigoMoneda", sqlTotalCxP, "DocumentoPagado.MontoTotalDeCxP", True) '& " / Pago.CambioABolivares"
      End If
   End If
'   If valComprobanteAImprimir = eQI_RETENCION_IVA Then
'      sqlMontoRetenidoCompDePago = "cxp.MontoRetenido"
'   End If
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.getIIF("cxP.UsaPrefijoSerie='S'", "'Serie ' + documentoPagado.NumeroDelDocumentoPagado", "documentoPagado.NumeroDelDocumentoPagado", True) & " AS NumeroDelDocumentoPagado, "
   SQL = SQL & "documentoPagado.NumeroComprobante, "
   SQL = SQL & sqlMontoAbonado
   SQL = SQL & "banco.Nombre, "
   SQL = SQL & sqlTotalCxP & " As TotalCXP, "
   SQL = SQL & gUtilSQL.getIIF("cxP.CodigoMoneda = '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "' AND Pago.CodigoMoneda <> '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "'", "DocumentoPagado.MontoTotalDeCxP * DocumentoPagado.CambioAMonedaDelPago", gUtilSQL.getIIF("cxP.CodigoMoneda <> '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "' AND Pago.CodigoMoneda = '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "'", "DocumentoPagado.MontoEnMonedaOriginalDeCxP * DocumentoPagado.CambioAMonedaDelPago", "DocumentoPagado.MontoEnMonedaOriginalDeCxP", True), True) & " AS TotalCXPCambio, "
   SQL = SQL & sqlTotalCxPComprobanteRetIva & " AS TotalCXPComprobanteRetIva, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "cxP.FechaAplicacionRetIVA, "
   SQL = SQL & "cxP.MesDeAplicacion, "
   SQL = SQL & "cxP.AnoDeAplicacion, "
   SQL = SQL & "cxP.NumeroComprobanteRetencion, "
   SQL = SQL & "cxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "cxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & _
               gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), _
               gUtilSQL.getIIF("cxP.UsaPrefijoSerie='S'", "'Serie ' + cxP.Numero", _
               "cxP.Numero", True), gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeFactura, "
   SQL = SQL & "cxP.NumeroControl, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & _
               gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
               "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & _
               gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
               "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeTransaccion", _
               enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
               gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "cxP.NumeroDeFacturaAfectada, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " As MontoGravado, "
   
   
   SQL = SQL & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   SQL = SQL & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   
   SQL = SQL & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial " & " = " & "'N'", _
                        "(SELECT TOP 1 alicuotaIVA.MontoAlicuotaGeneral FROM alicuotaIVA WHERE " & _
                        " alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC)", _
                        gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 " & " > " & "0", _
                        "cxP.PorcentajeIvaAlicuotaEspecial1 ", "cxP.PorcentajeIvaAlicuotaEspecial2 "), True) & "As AlicuotaG,"
                        
   SQL = SQL & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA " & _
                        " WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC) AS Alicuota2, "
   SQL = SQL & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA" & _
                        " WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC) AS Alicuota3, "
   SQL = SQL & sqlMontoIva & " AS MontoIva,"
   SQL = SQL & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral,"
   SQL = SQL & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2,"
   SQL = SQL & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3,"
   SQL = SQL & sqlMontoRetenidoCompDePago & " AS MontoRetenido, "
   SQL = SQL & sqlMontoRetenidoCompDeIVA & " AS MontoRetenidoIVA, "
   SQL = SQL & insCxPSQL.fSQLAnoYMesDeAplicacionIVAParaComprobante
   SQL = SQL & ", cxP.CodigoMoneda AS CodigoMonedaCxP, Pago.CodigoMoneda AS CodigoMonedaPago"
   SQL = SQL & " FROM Banco,("
   SQL = SQL & " proveedor INNER JOIN "
   SQL = SQL & " cxP ON (proveedor.CodigoProveedor = cxP.CodigoProveedor)"
   SQL = SQL & " AND (proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN (pago INNER JOIN "
   SQL = SQL & " documentoPagado ON (pago.NumeroComprobante"
   SQL = SQL & " = documentoPagado.NumeroComprobante) AND ("
   SQL = SQL & " pago.ConsecutivoCompania"
   SQL = SQL & " = documentoPagado.ConsecutivoCompania)) ON ("
   SQL = SQL & " documentoPagado.NumeroDelDocumentoPagado = cxP.Numero) AND ("
   SQL = SQL & " cxP.ConsecutivoCompania = documentoPagado.ConsecutivoCompania)"
   SQL = SQL & " WHERE Banco.Codigo = " & valCodigoBanco
   SQL = SQL & " AND proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND proveedor.CodigoProveedor ='" & ValCodigoProveedor & "'"
   SQL = SQL & " AND documentoPagado.NumeroComprobante = " & valNumeroComprobante
   SQL = SQL & " AND documentoPagado.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   ''*********** HAGO LA BUSQUEDA PARA CXP'S POR CUENTA DE TERCEROS
   SQL = SQL & " UNION "
   SQL = SQL & "SELECT "
   SQL = SQL & "documentoPagado.NumeroDelDocumentoPagado, "
   SQL = SQL & "documentoPagado.NumeroComprobante, "
   SQL = SQL & sqlMontoAbonado
   SQL = SQL & "banco.Nombre, "
   SQL = SQL & sqlTotalCxP & " As TotalCXP, "
   SQL = SQL & gUtilSQL.getIIF("cxP.CodigoMoneda = '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "' AND Pago.CodigoMoneda <> '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "'", "DocumentoPagado.MontoTotalDeCxP * DocumentoPagado.CambioAMonedaDelPago", gUtilSQL.getIIF("cxP.CodigoMoneda <> '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "' AND Pago.CodigoMoneda = '" & gMonedaLocalActual.GetHoyCodigoMoneda() & "'", "DocumentoPagado.MontoEnMonedaOriginalDeCxP * DocumentoPagado.CambioAMonedaDelPago", "DocumentoPagado.MontoEnMonedaOriginalDeCxP", True), True) & " AS TotalCXPCambio, "
   SQL = SQL & sqlTotalCxPComprobanteRetIva & " AS TotalCXPComprobanteRetIva, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "cxP.FechaAplicacionRetIVA, "
   SQL = SQL & "cxP.MesDeAplicacion, "
   SQL = SQL & "cxP.AnoDeAplicacion, "
   SQL = SQL & "cxP.NumeroComprobanteRetencion, "
   SQL = SQL & "cxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "cxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & _
               gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), _
               gUtilSQL.getIIF("cxP.UsaPrefijoSerie='S'", "'Serie ' + cxP.Numero", "cxP.Numero", True), gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeFactura, "
   SQL = SQL & "cxP.NumeroControl, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & _
               gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
               "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP = " & _
               gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
               "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeTransaccion", _
               enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
               gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "cxP.NumeroDeFacturaAfectada, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " As MontoGravado, "
   SQL = SQL & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   SQL = SQL & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   SQL = SQL & gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial " & " = " & "'N'", _
                        "(SELECT TOP 1 alicuotaIVA.MontoAlicuotaGeneral FROM alicuotaIVA WHERE " & _
                        " alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC)", _
                        gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 " & " > " & "0", _
                        "cxP.PorcentajeIvaAlicuotaEspecial1 ", "cxP.PorcentajeIvaAlicuotaEspecial2 "), True) & "As AlicuotaG,"
   SQL = SQL & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA " & _
                        " WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC) AS Alicuota2, "
   SQL = SQL & "(SELECT TOP 1 alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA" & _
                        " WHERE alicuotaIVA.FechaDeInicioDeVigencia <= cxP.Fecha" & _
                        " ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC) AS Alicuota3, "
   SQL = SQL & sqlMontoIva & " AS MontoIva,"
   SQL = SQL & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral,"
   SQL = SQL & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2,"
   SQL = SQL & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3,"
   SQL = SQL & sqlMontoRetenidoCompDePago & " AS MontoRetenido, "
   SQL = SQL & sqlMontoRetenidoCompDeIVA & " AS MontoRetenidoIVA, "
   SQL = SQL & insCxPSQL.fSQLAnoYMesDeAplicacionIVAParaComprobante
   SQL = SQL & ", cxP.CodigoMoneda AS CodigoMonedaCxP, Pago.CodigoMoneda AS CodigoMonedaPago"
   SQL = SQL & " FROM Banco,("
   SQL = SQL & " proveedor INNER JOIN "
   SQL = SQL & " cxP ON (proveedor.CodigoProveedor = cxP.CodigoProveedorOriginalServicio)"
   SQL = SQL & " AND (proveedor.ConsecutivoCompania = cxP.ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN (pago INNER JOIN "
   SQL = SQL & " documentoPagado ON (pago.NumeroComprobante"
   SQL = SQL & " = documentoPagado.NumeroComprobante) AND ("
   SQL = SQL & " pago.ConsecutivoCompania"
   SQL = SQL & " = documentoPagado.ConsecutivoCompania)) ON ("
   SQL = SQL & " documentoPagado.NumeroDelDocumentoPagado = cxP.Numero) AND ("
   SQL = SQL & " cxP.ConsecutivoCompania = documentoPagado.ConsecutivoCompania)"
   SQL = SQL & " WHERE Banco.Codigo = " & valCodigoBanco
   SQL = SQL & " AND proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cxp.CodigoProveedor ='" & ValCodigoProveedor & "'"
   SQL = SQL & " AND documentoPagado.NumeroComprobante = " & valNumeroComprobante
   SQL = SQL & " AND documentoPagado.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   Set gEnumProyecto = Nothing
   Set insCxPSQL = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteComprobanteDePago = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteComprobanteDePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelListadoRetencionIVA(ByVal valCodigoDeProveedor As String, ByVal valCantidadAImprimir As Boolean, _
                           ByVal valIncluirRegistrosMtoRetIvaCero As Boolean, ByVal valFechaInicial As Date, _
                                  ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal gUltimaTasaDeCambio As Object, ByVal valExcluirComprobantesSinNumero As Boolean, _
                                  Optional ByVal valIncluinrMontoCompAnulados As Boolean) As String
   Dim SQL As String
   Dim sqlTotalCxP As String
   Dim sqlMontoGravado As String
   Dim sqlMontoExento As String
   Dim sqlMontoGravableAlicuotaGeneral As String
   Dim sqlMontoGravableAlicuota2 As String
   Dim sqlMontoGravableAlicuota3 As String
   Dim sqlMontoIva As String
   Dim sqlMontoIVAALicuotaGeneral As String
   Dim sqlMontoIVAALicuota2 As String
   Dim sqlMontoIVAALicuota3 As String
   Dim sqlCampoCambio As String
   Dim sqlCampoMoneda As String
   Dim insCxPSQL As clsCxpSQL
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim sqlMontoRetenido As String
   Dim sqlValorElseMontoRetenido As String
   Dim sqlCampoSubSelectMontoRetenido As String
   Dim sqlAlicuotaG As String
   On Error GoTo h_ERROR
   Set insCxPSQL = New clsCxpSQL
   Set gEnumProyecto = New clsEnumAdministrativo
   If valIncluinrMontoCompAnulados Then
      sqlCampoCambio = gUtilSQL.getIIF("CxP.OrigenDeLaRetencion" & _
            " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
            "CxP.CambioABolivares", fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania, True), True)
   Else
      sqlCampoCambio = gUtilSQL.getIIF("CxP.OrigenDeLaRetencion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
         gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "CxP.CambioABolivares", "0", True), fSubSQLDecInfCambioOMonedaOriginalDesdePago(True, valConsecutivoCompania, False), True)
   End If
   
   sqlCampoMoneda = gUtilSQL.getIIF("CxP.OrigenDeLaRetencion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
         "CxP.Moneda", fSubSQLDecInfCambioOMonedaOriginalDesdePago(False, valConsecutivoCompania, valIncluinrMontoCompAnulados), True)
   sqlTotalCxP = "(" & "CxP.MontoExento" & _
               " + " & "CxP.MontoGravado" & _
               " + " & "CxP.MontoIva)"
   sqlMontoExento = "CxP.MontoExento"
   sqlMontoGravado = "CxP.MontoGravado"
   sqlMontoGravableAlicuotaGeneral = "CxP.MontoGravableAlicuotaGeneral"
   sqlMontoGravableAlicuota2 = "CxP.MontoGravableAlicuota2"
   sqlMontoGravableAlicuota3 = "CxP.MontoGravableAlicuota3"
   sqlMontoIva = "CxP.MontoIva"
   sqlMontoIVAALicuotaGeneral = "CxP.MontoIVAAlicuotaGeneral"
   sqlMontoIVAALicuota2 = "CxP.MontoIVAAlicuota2"
   sqlMontoIVAALicuota3 = "CxP.MontoIVAAlicuota3"
   sqlValorElseMontoRetenido = "CxP.MontoRetenido"
   sqlCampoSubSelectMontoRetenido = " CxP.MontoRetenido " '" Pago.TotalRetenidoIva " " CxP.MontoRetenido "
   If Not valIncluinrMontoCompAnulados Then
      sqlTotalCxP = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlTotalCxP, "0", True)
      sqlMontoExento = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoExento, "0", True)
      sqlMontoGravado = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoGravado, "0", True)
      sqlMontoGravableAlicuotaGeneral = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoGravableAlicuotaGeneral, "0", True)
      sqlMontoGravableAlicuotaGeneral = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 <> 0", "cxP.MontoGravableAlicuotaEspecial1", "cxP.MontoGravableAlicuotaEspecial2", True), sqlMontoGravableAlicuotaGeneral, True)
      sqlMontoGravableAlicuota2 = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoGravableAlicuota2, "0", True)
      sqlMontoGravableAlicuota3 = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoGravableAlicuota3, "0", True)
      sqlMontoIva = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoIva, "0", True)
      sqlMontoIVAALicuotaGeneral = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoIVAALicuotaGeneral, "0", True)
      sqlMontoIVAALicuotaGeneral = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoIVAAlicuotaEspecial1 <> 0", "cxP.MontoIVAAlicuotaEspecial1", "cxP.MontoIVAAlicuotaEspecial2", True), sqlMontoIVAALicuotaGeneral, True)
      sqlMontoIVAALicuota2 = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoIVAALicuota2, "0", True)
      sqlMontoIVAALicuota3 = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlMontoIVAALicuota3, "0", True)
      sqlValorElseMontoRetenido = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlValorElseMontoRetenido, "0", True)
      sqlCampoSubSelectMontoRetenido = gUtilSQL.getIIF(" cxp.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), sqlCampoSubSelectMontoRetenido, "0", True)
   End If
   sqlTotalCxP = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlTotalCxP, True, "")
   sqlMontoExento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoExento, True, "")
   sqlMontoGravado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravado, True, "")
   sqlMontoGravableAlicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuotaGeneral, True, "")
   sqlMontoGravableAlicuotaGeneral = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoGravableAlicuotaEspecial1 <> 0", "cxP.MontoGravableAlicuotaEspecial1", "cxP.MontoGravableAlicuotaEspecial2", True), sqlMontoGravableAlicuotaGeneral, True)
   sqlMontoGravableAlicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota2, True, "")
   sqlMontoGravableAlicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoGravableAlicuota3, True, "")
   sqlMontoIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIva, True, "")
   sqlMontoIVAALicuotaGeneral = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuotaGeneral, True, "")
   sqlMontoIVAALicuotaGeneral = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.MontoIVAAlicuotaEspecial1 <> 0", "cxP.MontoIVAAlicuotaEspecial1", "cxP.MontoIVAAlicuotaEspecial2", True), sqlMontoIVAALicuotaGeneral, True)
   sqlMontoIVAALicuota2 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota2, True, "")
   sqlMontoIVAALicuota3 = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCampoCambio, sqlCampoMoneda, sqlMontoIVAALicuota3, True, "")
   sqlMontoRetenido = gUtilSQL.getIIF("CxP.OrigenDeLaRetencion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
                                                     sqlValorElseMontoRetenido, fSQLDelMontoRetenidoIVA(valConsecutivoCompania, sqlCampoSubSelectMontoRetenido), True)
   sqlAlicuotaG = "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuotaGeneral FROM alicuotaIVA WHERE alicuotaIVA.FechaDeInicioDeVigencia <= CxP.Fecha ORDER BY alicuotaIVA.FechaDeInicioDeVigencia DESC)"
   sqlAlicuotaG = gUtilSQL.getIIF("cxp.AplicaIvaAlicuotaEspecial = " & gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.getIIF("cxP.PorcentajeIvaAlicuotaEspecial1 <> 0", "cxP.PorcentajeIvaAlicuotaEspecial1", "cxP.PorcentajeIvaAlicuotaEspecial2", True), sqlAlicuotaG, True)
   
   SQL = "SELECT CxP.Moneda,"
   SQL = SQL & sqlTotalCxP & " As TotalCXPComprobanteRetIva, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "proveedor.CodigoProveedor, "
   SQL = SQL & "CxP.FechaAplicacionRetIVA, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfCIntSQL("CxP.NumeroComprobanteRetencion") & " <> " & _
                               "0", "CxP.NumeroComprobanteRetencion", _
                               gUtilSQL.fSimpleSqlValue("Sin N° Asignado"), True) & _
               " AS NumeroComprobanteRetencion, "
   SQL = SQL & "CxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "CxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeFactura, "
   SQL = SQL & "CxP.NumeroControl, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeTransaccion", _
                        enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
                        gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "CxP.NumeroDeFacturaAfectada, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, "
   SQL = SQL & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   SQL = SQL & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   SQL = SQL & sqlAlicuotaG & " AS AlicuotaG, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota2, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota3, "
   SQL = SQL & sqlMontoIva & " AS MontoIva, "
   SQL = SQL & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral, "
   SQL = SQL & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2, "
   SQL = SQL & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3, "
   SQL = SQL & sqlMontoRetenido & " AS MontoRetenido, "
   SQL = SQL & gUtilSQL.getIIF("CxP.OrigenDeLaRetencion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
                                                      gUtilSQL.getIIF("cxp.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "'Anulado'", _
                                                      "'Vigente'", True), "(SELECT TOP 1" & gUtilSQL.getIIF("pago.StatusOrdenDePago = " _
                                                                                   & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE), "'Vigente'", "'Anulado'", True) & " As estatus " _
                                                                                   & " From Pago INNER JOIN DocumentoPagado ON Pago.consecutivoCompania = DocumentoPagado.consecutivoCompania" _
                                                                                   & " AND Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante " _
                                                                                   & " Where Pago.ConsecutivoCompania = " & valConsecutivoCompania & " And CxP.MontoRetenido <> 0" _
                                                                                   & " AND Pago.TotalRetenidoIva > 0  AND CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO) _
                                                                                   & " AND CXP.consecutivoCxP = DocumentoPagado.consecutivoCxP" _
                                                                                   & " AND CXP.consecutivoCompania = DocumentoPagado.consecutivoCompania" _
                                                                                   & " ORDER BY pago.numeroComprobante DESC)", True) & " AS estatus"
   SQL = SQL & ", cxP.NumeroComprobanteRetencion"
   SQL = SQL & " FROM proveedor "
   SQL = SQL & " INNER JOIN CxP ON ("
   SQL = SQL & "proveedor.CodigoProveedor = CxP.CodigoProveedor"
   SQL = SQL & ") AND (" & "proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania)"
   SQL = SQL & " WHERE " & "proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxP.FechaAplicacionRetIVA", valFechaInicial, valFechaFinal)
   If valCantidadAImprimir Then
      SQL = SQL & " AND " & "proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeProveedor))
   End If
   If valExcluirComprobantesSinNumero Then
      SQL = SQL & " AND " & "CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR)
   End If
   If valIncluirRegistrosMtoRetIvaCero = False Then
      SQL = SQL & " AND " & "CxP.MontoRetenido <> 0 "
   End If
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT CxP.Moneda,"
   SQL = SQL & sqlTotalCxP & " As TotalCXPComprobanteRetIva, "
   SQL = SQL & "proveedor.Direccion, "
   SQL = SQL & "proveedor.Telefonos, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NumeroNIT, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & "proveedor.CodigoProveedor, "
   SQL = SQL & "CxP.FechaAplicacionRetIVA, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfCIntSQL("CxP.NumeroComprobanteRetencion") & " <> " & _
                               "0", "CxP.NumeroComprobanteRetencion", _
                               gUtilSQL.fSimpleSqlValue("Sin N° Asignado"), True) & _
               " AS NumeroComprobanteRetencion, "
   SQL = SQL & "CxP.PorcentajeRetencionAplicado, "
   SQL = SQL & "CxP.Fecha AS FechaDelDocOrigen, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeFactura, "
   SQL = SQL & "CxP.NumeroControl, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaDebito, "
   SQL = SQL & gUtilSQL.getIIF("CxP.TipoDeCxP = " & _
                              gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                              "CxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " AS NumeroDeNotaCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeTransaccion", _
                        enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
                        gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "CxP.NumeroDeFacturaAfectada, "
   SQL = SQL & sqlMontoExento & " AS MontoExento, "
   SQL = SQL & sqlMontoGravado & " AS MontoGravado, "
   SQL = SQL & sqlMontoGravableAlicuotaGeneral & " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & sqlMontoGravableAlicuota2 & " AS MontoGravableAlicuota2, "
   SQL = SQL & sqlMontoGravableAlicuota3 & " AS MontoGravableAlicuota3, "
   SQL = SQL & sqlAlicuotaG & " AS AlicuotaG, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota2" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota2, "
   SQL = SQL & "(SELECT TOP 1 " & "alicuotaIVA.MontoAlicuota3" & _
                        " FROM alicuotaIVA " & _
                        " WHERE " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " <= " & "CxP.Fecha" & _
                        " ORDER BY " & "alicuotaIVA.FechaDeInicioDeVigencia" & _
                        " DESC) AS Alicuota3, "
   SQL = SQL & sqlMontoIva & " AS MontoIva, "
   SQL = SQL & sqlMontoIVAALicuotaGeneral & " AS MontoIVAAlicuotaGeneral, "
   SQL = SQL & sqlMontoIVAALicuota2 & " AS MontoIVAAlicuota2, "
   SQL = SQL & sqlMontoIVAALicuota3 & " AS MontoIVAAlicuota3, "
   SQL = SQL & sqlMontoRetenido & " AS MontoRetenido, "
   SQL = SQL & gUtilSQL.getIIF("CxP.OrigenDeLaRetencion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_DondeSeEfectuaLaRetencionIVA.eDS_CXP), _
                                                      gUtilSQL.getIIF("cxp.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "'Anulado'", _
                                                      "'Vigente'", True), "(SELECT TOP 1" & gUtilSQL.getIIF("pago.StatusOrdenDePago = " _
                                                                                   & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE), "'Vigente'", "'Anulado'", True) & " As estatus " _
                                                                                   & " From Pago INNER JOIN DocumentoPagado ON Pago.consecutivoCompania = DocumentoPagado.consecutivoCompania" _
                                                                                   & " AND Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante " _
                                                                                   & " Where Pago.ConsecutivoCompania = " & valConsecutivoCompania & " And CxP.MontoRetenido <> 0" _
                                                                                   & " AND Pago.TotalRetenidoIva > 0  AND CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO) _
                                                                                   & " AND CXP.consecutivoCxP = DocumentoPagado.consecutivoCxP" _
                                                                                   & " AND CXP.consecutivoCompania = DocumentoPagado.consecutivoCompania" _
                                                                                   & " ORDER BY pago.numeroComprobante DESC)", True) & " AS estatus"
   SQL = SQL & ", cxP.NumeroComprobanteRetencion"
   SQL = SQL & " FROM proveedor "
   SQL = SQL & " INNER JOIN CxP ON ("
   SQL = SQL & "proveedor.CodigoProveedor = CxP.CodigoProveedorOriginalServicio"
   SQL = SQL & ") AND (" & "proveedor.ConsecutivoCompania = CxP.ConsecutivoCompania)"
   SQL = SQL & " WHERE " & "proveedor.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxP.FechaAplicacionRetIVA", valFechaInicial, valFechaFinal)
   If valCantidadAImprimir Then
      SQL = SQL & " AND " & "proveedor.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoDeProveedor))
   End If
   If valExcluirComprobantesSinNumero Then
      SQL = SQL & " AND " & "CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR)
   End If
   If valIncluirRegistrosMtoRetIvaCero = False Then
      SQL = SQL & " AND " & "CxP.MontoRetenido <> 0 "
   End If
   SQL = SQL & " AND cxP.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   
h_EXIT:
   Set insCxPSQL = Nothing
   Set gEnumProyecto = Nothing
   On Error GoTo 0
   fSQLDelListadoRetencionIVA = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelListadoRetencionIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLRetDocPagados(ByVal refCodigoProveedor As String, ByVal valConjuntoCxP As String, ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT "
   vSQL = vSQL & " NumeroDelDocumento, "
   vSQL = vSQL & " CodigoRetencion, "
   vSQL = vSQL & " MontoOriginal, "
   vSQL = vSQL & " montoBaseImponible, "
   vSQL = vSQL & " MontoRetencion, "
   vSQL = vSQL & " PorcentajeDeRetencion, "
   vSQL = vSQL & " retDocumentoPagado.ConsecutivoCompania, "
   vSQL = vSQL & " retDocumentoPagado.NumeroComprobante, "
   vSQL = vSQL & " SecuencialDocPagado, "
   vSQL = vSQL & " retDocumentoPagado.EsUnaCuentaATerceros, "
   vSQL = vSQL & " retDocumentoPagado.CodigoProveedorOriginalServicio "
   vSQL = vSQL & " FROM RetPago "
   vSQL = vSQL & " INNER JOIN retDocumentoPagado "
   vSQL = vSQL & " ON retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania"
   vSQL = vSQL & " AND RetPago.numeroComprobante = retDocumentoPagado.numeroComprobante"
   vSQL = vSQL & " WHERE retDocumentoPagado.NumeroDelDocumento IN(" & valConjuntoCxP & ") "
   vSQL = vSQL & " AND RetPago.CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(refCodigoProveedor)
   vSQL = vSQL & " AND RetPago.ConsecutivoCompania = " & valConsecutivoCompania
h_EXIT:    On Error GoTo 0
   fSQLRetDocPagados = vSQL
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSearchRetPagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLISLRGridForInsertDetail() As String
   Dim SQL As String
   Dim strCampos As String
   Dim sqlFromInnerJoin As String
   Dim sqlCondicion As String
   On Error GoTo h_ERROR
   strCampos = strCampos & "cxP.Numero, "
   strCampos = strCampos & "seHizoLaRetencion, "
   strCampos = strCampos & "CodigoRetencion, "
   strCampos = strCampos & "MontoGravado, "
   strCampos = strCampos & "MontoBaseImponible, "
   strCampos = strCampos & "retDocumentoPagado.MontoRetencion, "
   strCampos = strCampos & "retPago.CampoExtra1, "
   strCampos = strCampos & "PorcentajeDeRetencion, "
   strCampos = strCampos & "cxP.EsUnaCuentaATerceros, "
   strCampos = strCampos & "cxP.CodigoProveedorOriginalServicio "
   sqlFromInnerJoin = "FROM CXP "
   sqlFromInnerJoin = sqlFromInnerJoin & "INNER JOIN retPago "
   sqlFromInnerJoin = sqlFromInnerJoin & "ON cxP.ConsecutivoCompania = retPago.ConsecutivoCompania "
   sqlFromInnerJoin = sqlFromInnerJoin & "AND cxP.Numero = retPago.NumeroReferencia "
   sqlFromInnerJoin = sqlFromInnerJoin & "AND cxP.CodigoProveedor = retPago.CodigoProveedor "
   sqlFromInnerJoin = sqlFromInnerJoin & "INNER JOIN retDocumentoPagado "
   sqlFromInnerJoin = sqlFromInnerJoin & "ON retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania "
   sqlFromInnerJoin = sqlFromInnerJoin & "AND retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante "
   sqlCondicion = "WHERE CXP.consecutivoCompania = -1 "
   sqlCondicion = sqlCondicion & "AND CXP.ConsecutivoCxP = -1 "
   SQL = "SELECT " & strCampos & sqlFromInnerJoin & sqlCondicion
   fSQLISLRGridForInsertDetail = SQL
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLISLRGridForInsertDetail", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLPagosAReversar(ByVal valFecha As Date, ByVal valConsecutivoCompania As Long)
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & " documentoPagado.NumeroComprobante, "
   SQL = SQL & " documentoPagado.NumeroDelDocumentoPagado, "
   SQL = SQL & "(" & " documentoPagado.MontoAbonado/" & " documentoPagado.CambioAMonedaDelPago) AS MontoAbonado,"
   SQL = SQL & " pago.CodigoProveedor, "
   SQL = SQL & " pago.Fecha, "
   SQL = SQL & " pago.FechaAnulacion, "
   SQL = SQL & " pago.StatusOrdenDePago"
   SQL = SQL & " FROM " & " documentoPagado INNER JOIN pago "
   SQL = SQL & " ON (" & " documentoPagado.ConsecutivoCompania" & _
               " = " & " pago.ConsecutivoCompania"
   SQL = SQL & ") AND (" & " documentoPagado.NumeroComprobante" & _
               " = " & " pago.NumeroComprobante)"
   SQL = SQL & " WHERE " & " pago.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND (" & " pago.Fecha" & _
               " > " & gUtilSQL.fDateToSQLValue(valFecha) & _
               " OR (" & _
                  " pago.StatusOrdenDePago" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA) & _
                  " AND   " & _
                  " pago.FechaAnulacion" & _
                  " > " & gUtilSQL.fDateToSQLValue(valFecha) & ")) and pago.StatusOrdenDePago = '0' "
   SQL = SQL & " ORDER BY "
   SQL = SQL & " pago.Fecha DESC, "
   SQL = SQL & " documentoPagado.NumeroComprobante, "
   SQL = SQL & " documentoPagado.secuencial"
h_EXIT: On Error GoTo 0
   fSQLPagosAReversar = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLPagosAReversar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLComprobanteContableDePagos(ByVal valConsecutivoCompania As Long, ByVal valComprobanteTableName As String, ByVal valAsientoTableName As String, ByVal valPeriodoActualTableName As String, ByVal valNumeroComprobante As Integer) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT " & valComprobanteTableName & "." & "NUMERO" & " AS NumeroComprobante, "
   SQL = SQL & valAsientoTableName & ".CodigoCuenta AS CodigoCuenta, "
   SQL = SQL & valComprobanteTableName & "." & "DESCRIPCION" & " AS DescripcionComprobante, "
   SQL = SQL & valComprobanteTableName & "." & "TotalDebe" & "  AS TotalDebe, "
   SQL = SQL & valComprobanteTableName & "." & "TotalHaber" & " AS TotalHaber, "
   SQL = SQL & valAsientoTableName & ".MontoDebe AS MontoDebe, "
   SQL = SQL & valAsientoTableName & ".MontoHaber AS MontoHaber, "
   SQL = SQL & valAsientoTableName & ".NumeroComprobante AS NumeroComprobanteAsiento, "
   SQL = SQL & valAsientoTableName & ".Descripcion AS DescripcionAsiento, "
   SQL = SQL & valAsientoTableName & ".FechaDeReferencia AS FechaReferencia, "
   SQL = SQL & valAsientoTableName & ".Auxiliar AS Auxiliar"
   SQL = SQL & " FROM " & valComprobanteTableName
   SQL = SQL & " INNER JOIN " & valAsientoTableName & " ON "
   SQL = SQL & valComprobanteTableName & "." & "ConsecutivoPeriodo" & _
         "=" & valAsientoTableName & ".ConsecutivoPeriodo"
   SQL = SQL & " AND " & valComprobanteTableName & "." & "NUMERO" & _
         "=" & valAsientoTableName & ".NumeroComprobante"
   SQL = SQL & " WHERE " & valComprobanteTableName & "." & "NoDocumentoOrigen" & _
         "= '" & valNumeroComprobante & "'" & _
         " AND " & valComprobanteTableName & "." & "GeneradoPor" & _
         "='" & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_PAGOS) & "'"
   SQL = SQL & " AND " & valComprobanteTableName & "." & "ConsecutivoPeriodo" _
         & " IN (SELECT " & "CONSECUTIVOPERIODO" & " FROM " & valPeriodoActualTableName _
         & " WHERE " & "CONSECUTIVOCOMPANIA" & " = " & valConsecutivoCompania & ") "
   fSQLComprobanteContableDePagos = SQL
h_EXIT:
  fSQLComprobanteContableDePagos = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLComprobantecontableDePagos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLRetPagoAEliminar(ByVal valNumeroComprobante As Long, ByVal gProyCompaniaActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT retPago.NumeroComprobante"
   SQL = SQL & " FROM (pago INNER JOIN retPago"
   SQL = SQL & " ON (pago.ConsecutivoCompania = retPago.ConsecutivoCompania"
   SQL = SQL & ") AND (pago.NumeroCheque = retPago.NumeroReferencia"
   SQL = SQL & ") AND (pago.CodigoProveedor = retPago.CodigoProveedor"
   SQL = SQL & ")) INNER JOIN retDocumentoPagado ON ("
   SQL = SQL & "retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante"
   SQL = SQL & ") AND (retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania)"
   SQL = SQL & " WHERE pago.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_ANULADA)
   SQL = SQL & " AND pago.NumeroComprobante = " & valNumeroComprobante
   fSQLRetPagoAEliminar = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLRetPagoAEliminar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDelCambioDeCodigoDelSeniatPago(ByVal valConsecutivoCompania As Long, ByVal valTipoPersona As String, _
                                             ByVal valMes As String, ByVal valAno As String, _
                                          ByVal valCodigoRetNuevo As String, ByVal valCodigoRetOriginal As String)
   
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "UPDATE RetDocumentoPagado "
   SQL = SQL & " SET codigoretencion = " & gUtilSQL.fSimpleSqlValue(valCodigoRetNuevo)
   SQL = SQL & " FROM RetDocumentoPagado INNER JOIN pago "
   SQL = SQL & " ON (pago.ConsecutivoCompania = RetDocumentoPagado.ConsecutivoCompania "
   SQL = SQL & " AND pago.NumeroComprobante = RetDocumentoPagado.NumeroComprobante) "
   SQL = SQL & " INNER JOIN proveedor "
   SQL = SQL & " ON (proveedor.ConsecutivoCompania = pago.ConsecutivoCompania "
   SQL = SQL & " AND proveedor.codigoProveedor = pago.CodigoProveedor) "
   SQL = SQL & " WHERE codigoretencion = " & gUtilSQL.fSimpleSqlValue(valCodigoRetOriginal)
   SQL = SQL & " AND proveedor.TipoDePersona = " & gUtilSQL.fSimpleSqlValue(valTipoPersona)
   SQL = SQL & " AND " & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLMonthOfDate("pago.Fecha"), eTDSS_VARCHAR, ""), 2, "0") & " = " & gUtilSQL.fSimpleSqlValue(valMes)
   SQL = SQL & " AND " & gUtilSQL.fCast(gUtilSQL.DfSQLYearOfDate("pago.Fecha"), eTDSS_VARCHAR, "") & " = " & gUtilSQL.fSimpleSqlValue(valAno)
   SQL = SQL & " AND pago.ConsecutivoCompania = " & valConsecutivoCompania
   
   On Error GoTo 0
   fSQLDelCambioDeCodigoDelSeniatPago = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelCambioDeCodigoDelSeniatPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function
Public Function fSQLDelMontoRetenidoIVA(ByVal valConsecutivoCompania As Long, ByVal valCampoMontoRetencionPago As String)
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "(SELECT TOP 1 " & valCampoMontoRetencionPago
   SQL = SQL & " FROM Pago INNER JOIN DocumentoPagado ON Pago.consecutivoCompania = DocumentoPagado.consecutivoCompania"
   SQL = SQL & " AND Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante "
   SQL = SQL & "  Where Pago.ConsecutivoCompania = " & valConsecutivoCompania & " And CxP.MontoRetenido <> 0"
   SQL = SQL & "  AND Pago.TotalRetenidoIva > 0  AND CxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   SQL = SQL & "  AND CXP.consecutivoCxP = DocumentoPagado.consecutivoCxP"
   SQL = SQL & "  AND CXP.consecutivoCompania = DocumentoPagado.consecutivoCompania"
   SQL = SQL & "  ORDER BY pago.numeroComprobante DESC) "
   On Error GoTo 0
   fSQLDelMontoRetenidoIVA = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelCambioDeCodigoDelSeniatPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)

End Function

Public Function fConstruirSQLDePagoEntreFechas(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valTableName As String, valProveedorTableName As String, ByVal valConsecutivoCompaniaActual As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & valTableName & ".Fecha AS FechaDocumento, "
   SQL = SQL & valProveedorTableName & ".CodigoProveedor"
   SQL = SQL & " AS CodigoProveedor , "
   SQL = SQL & valTableName & ".DescripcionPago AS Descripcion, "
   SQL = SQL & valProveedorTableName & ".NombreProveedor"
   SQL = SQL & " AS NombreProveedor , "
   SQL = SQL & valTableName & ".NumeroComprobante AS NumeroDocumento, "
   SQL = SQL & valTableName & ".TotalDocumentos"
   SQL = SQL & " AS TotalDocumento "
   SQL = SQL & " FROM " & valTableName & " INNER JOIN " & valProveedorTableName & " ON " & valTableName & ".ConsecutivoCompania = " & valProveedorTableName & ".ConsecutivoCompania" & " AND " & valTableName & ".CodigoProveedor = " & valProveedorTableName & ".CodigoProveedor"
   SQL = SQL & " WHERE " & valTableName & ".ConsecutivoCompania = "
   SQL = SQL & valConsecutivoCompaniaActual
   SQL = SQL & " AND " & valTableName & ".Fecha"
   SQL = SQL & " BETWEEN "
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
   SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaFinal)
   fConstruirSQLDePagoEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConstruirSQLDePagoEntreFechas = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDePagoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSqlParaPeriodoSinMovimiento(ByVal valFecha As String, ByVal valConsecutivoCompania As Long, ByVal valRifContribuyente As String) As String
   Dim SQL As String
   Dim VarPeriodoDeImposicion As String
   On Error GoTo h_ERROR
   VarPeriodoDeImposicion = gUtilDate.fYear(valFecha) & gConvert.MesAString2Digitos(valFecha)
   SQL = " SELECT "
         SQL = SQL & "Compania.NumeroDeRif AS RifDelContribuyente, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(VarPeriodoDeImposicion) & " AS PeriodoDeImposicion, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS FechaDocumento, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS TipoDeOperacion, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS TipoDocumentoDI, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS RifDelComprador, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS NumeroDeDocumento, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS NumeroDeControl, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS MontoDocumento, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS BaseImponible, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS MontoIva, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS NumeroFacturaAfectada, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS NumeroDeComprobante, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS MontoExcento, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS AlicuotaIva, "
         SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS NumeroDeExpediente "
         SQL = SQL & " FROM "
         SQL = SQL & " Compania"
         SQL = SQL & " WHERE "
         SQL = SQL & " Compania.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania)
         fConstruirSqlParaPeriodoSinMovimiento = SQL
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:    Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSqlParaPeriodoSinMovimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



