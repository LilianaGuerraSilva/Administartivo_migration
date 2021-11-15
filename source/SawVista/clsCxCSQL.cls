VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxCSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCxCSQL"
Private Const CM_MESSAGE_NAME As String = "SQL de CxC"

Private Function CM_MODULO_CxP() As Integer
   CM_MODULO_CxP = 3
End Function
Private Function CM_MODULO_CxC() As Integer
   CM_MODULO_CxC = 2
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConstruirSQLDelReporteFacturasSinCxC(ByVal valConsecutivoCompania As String, _
                                                       ByVal valIncluirFacturaHistoricas As Boolean) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   SQL = "SELECT "
   SQL = SQL & "Factura.Numero, "
   SQL = SQL & "Factura.Fecha, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                        gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDeDocumento, "
   SQL = SQL & "Factura.TotalMontoExento, "
   SQL = SQL & "Factura.TotalBaseImponible, "
   SQL = SQL & "Factura.TotalIVA, "
   SQL = SQL & "Factura.TotalFactura, "
   SQL = SQL & "Cliente.Nombre "
   SQL = SQL & "FROM Factura INNER JOIN "
   SQL = SQL & "Cliente ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania "
   SQL = SQL & "AND Factura.CodigoCliente = Cliente.Codigo LEFT OUTER JOIN "
   SQL = SQL & "CxC ON Factura.ConsecutivoCompania = CxC.ConsecutivoCompania "
   SQL = SQL & "AND Factura.Numero = CxC.NumeroDocumentoOrigen "
   SQL = SQL & "WHERE (Factura.StatusFactura = " & enum_StatusFactura.eSF_EMITIDA & ") "
   SQL = SQL & "AND " & gUtilSQL.DfSQLIsNull("CxC.NumeroDocumentoOrigen")
   SQL = SQL & "AND  (Factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ") "
   If Not valIncluirFacturaHistoricas Then
      SQL = SQL & "AND (factura.FacturaHistorica <> " & gUtilSQL.fSimpleSqlValue("S") & ") "
   End If
   SQL = SQL & "ORDER BY factura.Numero, factura.Fecha"
   fConstruirSQLDelReporteFacturasSinCxC = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLDelReporteFacturasSinCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'nelson nuevo

Public Function fSQLHistoricoDeCliente(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String, ByVal valUsarCambioBs As Boolean, _
                                 ByVal valOrdenaPorCodigo As Boolean, ByVal mConsecutivoCompania As String, ByVal valUsarCambioOriginal As Boolean, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlCxC As String
   Dim sqlAnticipo As String
   Dim sqlOrderBy As String
   On Error GoTo h_ERROR
   sqlCxC = fSQLHistoricoDeClienteCxCNew(valFechaInicial, valFechaFinal, valcodigoCliente, valUsarCambioBs, mConsecutivoCompania, valUsarCambioOriginal, gMonedaLocalActual, gUltimaTasaDeCambio)
   sqlAnticipo = fSQLHistoricoDeClienteAnticiposNew(valFechaInicial, valFechaFinal, valcodigoCliente, valUsarCambioBs, mConsecutivoCompania, valUsarCambioOriginal, gMonedaLocalActual, gUltimaTasaDeCambio)
'   sqlCxC = fSQLHistoricoDeClienteCxC(valFechaInicial, valFechaFinal, valcodigocliente, valUsarCambioBs, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
'   sqlAnticipo = fSQLHistoricoDeClienteAnticipos(valFechaInicial, valFechaFinal, valcodigocliente, valUsarCambioBs, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   If valOrdenaPorCodigo Then
      sqlOrderBy = " ORDER BY Codigo, TipoReporte, MonedaReporte, NumeroDocumentoGrupo, FechaDocumento, FechaCobranza, NumeroCobranza"
   Else
      sqlOrderBy = " ORDER BY Nombre, TipoReporte, MonedaReporte,  NumeroDocumentoGrupo, FechaDocumento, FechaCobranza, NumeroCobranza"
   End If
   SQL = sqlCxC
   SQL = SQL & " UNION " & sqlAnticipo
   SQL = SQL & sqlOrderBy
h_EXIT: On Error GoTo 0
   fSQLHistoricoDeCliente = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHistoricoDeCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Private Function fSQLHistoricoDeClienteCxC(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigocliente As String, _
'                     ByVal valUsarCambioBs As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String  'fSQLDelReporteHistoricoDeCliente
'   Dim SQL As String
'   Dim sqlSaldoActual As String
'   Dim sqlMonto As String
'   Dim sqlMontoCobrado As String
'   Dim SqlSaldoInicial As String
'   Dim sqlTipoDeCxC As String
'   Dim sqlTipoDeDocCobrado As String
'   Dim sqlStatusCobranza As String
'   Dim sqlStatusCobranzaAnulado As String
'   Dim sqlMonedaCxC As String
'   Dim sqlCambioCxC As String
'   Dim sqlMonedaCob As String
'   Dim sqlCambioCob As String
'   Dim SaldoInicialConAbonos As String
'   Dim gEnumProyecto As clsEnumAdministrativo
'   Dim sqlMonedaReporte As String
'   On Error GoTo h_ERROR
'   Set gEnumProyecto = New clsEnumAdministrativo
'   SqlSaldoInicial = fSQLSaldoInicialAnalisisCxCHistoricoCxC(valFechaInicial, valUsarCambioBs, False, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
'   SaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxCHistoricoCxC(valFechaInicial, valUsarCambioBs, True, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio) ' prueba
'   sqlMonedaCxC = " CxC.Moneda"
'   sqlCambioCxC = " CxC.CambioABolivares"
'   sqlMonedaCob = " Cobranza.Moneda"
'   sqlCambioCob = " Cobranza.CambioABolivares"
'   sqlStatusCobranza = " Cobranza.StatusCobranza"
'   sqlStatusCobranzaAnulado = sqlStatusCobranza & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA)
'   sqlMontoCobrado = "(DocumentoCobrado.MontoAbonado )"
''   sqlMontoCobrado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoCobrado, "cxC.fecha")
'   sqlMontoCobrado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, "0", sqlMontoCobrado)
'   sqlMonto = "(" & " CxC.MontoExento" & _
'              " + " & " CxC.MontoGravado" & _
'              " + " & " CxC.MontoIVA)"
''   sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, "cxC.fecha")
'   sqlSaldoActual = "(" & sqlMonto & " - " & " CxC.MontoAbonado)"
'   sqlTipoDeCxC = gUtilSQL.DfSQLCaseIfForEnum(" CxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "")
'   sqlTipoDeDocCobrado = gUtilSQL.fSimpleSqlValue("Cobro")
'   sqlMonedaReporte = " CxC.Moneda"
'   If valUsarCambioBs Then
'      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCxC, sqlMonto, True, "")
'      sqlSaldoActual = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCxC, sqlMonedaCxC, sqlSaldoActual, True, "")
'      sqlMontoCobrado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCob, sqlMonedaCob, sqlMontoCobrado, True, "")
'      sqlMontoCobrado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoCobrado, "Cobranza.fecha")
'      sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, "cxC.fecha")
'      sqlSaldoActual = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlSaldoActual, "cxC.fecha")
'      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
'   Else
'      sqlMontoCobrado = "(DocumentoCobrado.MontoAbonado / DocumentoCobrado.CambioAMonedaDeCobranza )"
'   End If
'   SQL = "SELECT "
'   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
'   SQL = SQL & " CxC.CodigoCliente AS Codigo, "
'   SQL = SQL & " Cliente.Nombre AS Nombre, "
'   SQL = SQL & sqlMonedaCxC & " AS Moneda, "
'   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
'   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloTipoReporte, "
'   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
'   SQL = SQL & SaldoInicialConAbonos & " As SaldoInicialConAbonos, "
'   SQL = SQL & " CxC.Fecha AS FechaDocumento, "
'   SQL = SQL & " CxC.FechaVencimiento AS FechaVencimiento, "
'   SQL = SQL & sqlTipoDeCxC & " AS TipoDeDocumento, "
'   SQL = SQL & " CxC.Numero AS NumeroDocumento, "
'   SQL = SQL & gUtilSQL.fCast("CxC.CodigoCliente" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxC.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
'   SQL = SQL & sqlMonto & " AS MontoOriginal, "
'   SQL = SQL & sqlSaldoActual & " AS SaldoActual, "
'   SQL = SQL & sqlTipoDeDocCobrado & " AS TipoDocumentoDetalle, "
'   SQL = SQL & " Cobranza.Numero AS NumeroCobranza, "
'   SQL = SQL & " Cobranza.Fecha AS FechaCobranza, "
'   SQL = SQL & sqlMontoCobrado & " AS MontoCobrado, "
'   SQL = SQL & sqlStatusCobranza & " AS StatusCobranza "
'   SQL = SQL & " FROM ( Cliente INNER JOIN " & " CxC ON ("
'   SQL = SQL & "Cliente.Codigo = " & " CxC.CodigoCliente"
'   SQL = SQL & ") AND (Cliente.ConsecutivoCompania = " & " CxC.ConsecutivoCompania"
'   SQL = SQL & ")) LEFT JOIN (" & " Cobranza RIGHT JOIN DocumentoCobrado ON ("
'   SQL = SQL & " Cobranza.Numero = DocumentoCobrado.NumeroCobranza"
'   SQL = SQL & ") AND (" & " Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
'   SQL = SQL & ")) ON (" & " CxC.Numero =  DocumentoCobrado.NumeroDelDocumentoCobrado"
'   SQL = SQL & ") AND (" & " CxC.TipoCxC = DocumentoCobrado.TipoDeDocumentoCobrado"
'   SQL = SQL & ") AND (" & " CxC.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania)"
'   SQL = SQL & " WHERE " & " CxC.ConsecutivoCompania = " & mConsecutivoCompania
'   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(" CxC.Fecha", valFechaInicial, valFechaFinal)
'   SQL = SQL & " AND " & " CxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
'   If valcodigocliente <> "" Then
'      SQL = SQL & " AND " & " CxC.CodigoCliente = " & gUtilSQL.fSimpleSqlValue(valcodigocliente)
'   End If
'      Set gEnumProyecto = Nothing
'h_EXIT: On Error GoTo 0
'   fSQLHistoricoDeClienteCxC = SQL
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fSQLHistoricoDeClienteCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fSQLHistoricoDeClienteAnticipos(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigocliente As String, _
'               ByVal valUsarCambioBs As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String 'fSQLDelReporteHistoricoDeCliente
'   Dim SQL As String
'   Dim sqlMontoOriginal As String
'   Dim SqlSaldoInicial As String
'   Dim sqlSaldoActual As String
'   Dim sqlMontoNoDisponible As String
'   Dim sqlTipoDeDocumento As String
'   Dim sqlStatusCobranza As String
'   Dim sqlStatusCobranzaAnulada As String
'   Dim sqlMontoAplicado As String
'   Dim sqlMonedaAnticipo As String
'   Dim sqlCambioAnticipo As String
'   Dim sqlMonedaCob As String
'   Dim sqlCambioCob As String
'   Dim SqlSaldoInicialConAbonos As String
'   Dim sqlMonedaReporte As String
'   Dim gEnumProyecto As clsEnumAdministrativo
'   On Error GoTo h_ERROR
'   Set gEnumProyecto = New clsEnumAdministrativo
'   SqlSaldoInicial = fSQLSaldoInicialAnalisisCxCHistoricoAnticipos(valFechaInicial, valUsarCambioBs, False, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
'   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxCHistoricoAnticipos(valFechaInicial, valUsarCambioBs, True, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)   ' prueba
'   sqlMonedaAnticipo = "Anticipo.Moneda"
'   sqlCambioAnticipo = "Anticipo.Cambio"
'   sqlMonedaCob = " Cobranza." & "Moneda"
'   sqlCambioCob = " Cobranza." & "CambioAbolivares"
'   sqlMontoOriginal = "Anticipo.MontoTotal"
''   sqlMontoOriginal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoOriginal, insAnticipo.GetTableName & ".fecha")
'   sqlMontoNoDisponible = "(Anticipo.MontoUsado" & _
'                        " + Anticipo.MontoDevuelto" & _
'                        " + Anticipo.MontoDiferenciaEnDevolucion " & ")"
''   sqlMontoNoDisponible = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoNoDisponible, insAnticipo.GetTableName & ".fecha")
'   sqlSaldoActual = "(" & sqlMontoOriginal & " - " & sqlMontoNoDisponible & ")"
'   sqlTipoDeDocumento = gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO))
'   sqlStatusCobranza = " Cobranza.StatusCobranza"
'   sqlStatusCobranzaAnulada = sqlStatusCobranza & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA)
'   sqlMontoAplicado = " AnticipoCobrado.MontoAplicado " 'insAnticipoCob.GetTableName & "." & insAnticipoCob.getFN_MONTO_APLICADO
'   If Not valUsarCambioBs Then
'      'sqlMontoAplicado = insAnticipoCob.GetTableName & "." & insAnticipoCob.getFN_MONTO_APLICADO & "/" & insAnticipoCob.GetTableName & "." & insAnticipoCob.getFN_CAMBIO_MONEDA
'      sqlMontoAplicado = " AnticipoCobrado.MontoAplicado/AnticipoCobrado.Cambio"
'   End If
'   sqlMontoAplicado = gUtilSQL.getIIF(sqlStatusCobranzaAnulada, "0", sqlMontoAplicado)
'   sqlMonedaReporte = "Anticipo.Moneda"
'   If valUsarCambioBs Then
'      sqlMontoOriginal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioAnticipo, sqlMonedaAnticipo, sqlMontoOriginal, True, "")
'      sqlSaldoActual = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioAnticipo, sqlMonedaAnticipo, sqlSaldoActual, True, "")
'      sqlMontoAplicado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioCob, sqlMonedaCob, sqlMontoAplicado, True, "")
'      sqlMontoOriginal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoOriginal, "Anticipo.fecha")
'      sqlMontoAplicado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoAplicado, " Cobranza.fecha")
'      sqlSaldoActual = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlSaldoActual, "Anticipo.fecha")
'      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
'   End If
'   SQL = " SELECT "
'   SQL = SQL & gUtilSQL.fSimpleSqlValue("1") & " AS TipoReporte, "
'   SQL = SQL & "Anticipo.CodigoCliente" & " AS Codigo, "
'   SQL = SQL & "Cliente.Nombre AS Nombre, "
'   SQL = SQL & sqlMonedaAnticipo & " AS Moneda, "
'   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
'   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloTipoReporte, "
'   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
'   SQL = SQL & SqlSaldoInicialConAbonos & " As SaldoInicialConAbonos, "
'   SQL = SQL & "Anticipo.Fecha" & " AS FechaDocumento, "
'   SQL = SQL & " '' AS FechaVencimiento, "
'   SQL = SQL & sqlTipoDeDocumento & " AS TipoDeDocumento, "
'   SQL = SQL & "Anticipo.Numero" & " AS NumeroDocumento, "
'   SQL = SQL & gUtilSQL.fCast("Anticipo.ConsecutivoAnticipo", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
'   SQL = SQL & sqlMontoOriginal & " AS MontoOriginal, "
'   SQL = SQL & sqlSaldoActual & " AS SaldoActual, "
'   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cobro") & " AS TipoDocumentoDetalle, "
'   SQL = SQL & " Cobranza.Numero AS NumeroCobranza, "
'   SQL = SQL & " Cobranza.Fecha AS FechaCobranza, "
'   SQL = SQL & sqlMontoAplicado & " AS MontoCobrado, "
'   SQL = SQL & sqlStatusCobranza & " AS StatusCobranza "
'   SQL = SQL & " FROM (Anticipo LEFT JOIN ( Cobranza"
'   SQL = SQL & " RIGHT JOIN AnticipoCobrado ON ("
'   SQL = SQL & " Cobranza.Numero = AnticipoCobrado.NumeroCobranza"
'   SQL = SQL & ") AND (" & " Cobranza.ConsecutivoCompania = AnticipoCobrado.ConsecutivoCompania"
'   SQL = SQL & ")) ON ( Anticipo.ConsecutivoAnticipo" & " = AnticipoCobrado.ConsecutivoAnticipoUsado"
'   SQL = SQL & ") AND ( Anticipo.ConsecutivoCompania" & " = AnticipoCobrado.ConsecutivoCompania"
'   SQL = SQL & ")) INNER JOIN  Cliente ON ("
'   SQL = SQL & " Anticipo.CodigoCliente = Cliente.Codigo"
'   SQL = SQL & ") AND (Anticipo.ConsecutivoCompania =  Cliente.ConsecutivoCompania) "
'   SQL = SQL & " WHERE Anticipo.ConsecutivoCompania = " & mConsecutivoCompania
'   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("Anticipo.Fecha", valFechaInicial, valFechaFinal)
'   SQL = SQL & " AND Anticipo.Status" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
'   SQL = SQL & " AND Anticipo.EsUnaDevolucion" & " = " & gUtilSQL.fBooleanToSqlValue(False)
'   SQL = SQL & " AND Anticipo.Tipo" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
'   If valcodigocliente <> "" Then
'      SQL = SQL & " AND Anticipo.CodigoCliente" & " = " & gUtilSQL.fSimpleSqlValue(valcodigocliente)
'   End If
'    Set gEnumProyecto = Nothing
'h_EXIT: On Error GoTo 0
'   fSQLHistoricoDeClienteAnticipos = SQL
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'         "fSQLHistoricoDeClienteAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function

Private Function fSQLSaldoInicialAnalisisCxCHistoricoCxC(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, _
         ByVal valDescontarAbonos As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlCxCTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlCxCTableName = "CxC_1"
   sqlMonto = "(" & sqlCxCTableName & ".MontoExento" & _
               " + " & sqlCxCTableName & ".MontoGravado" & _
               " + " & sqlCxCTableName & ".MontoIVA) "
   If valDescontarAbonos Then
      sqlMonto = "(" & sqlMonto & " - " & sqlCxCTableName & ".MontoAbonado)"
   End If
   If valUsarCambio Then
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCxCTableName & ".CambioABolivares", _
                  sqlCxCTableName & ".Moneda", sqlMonto, True, "")
      sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, sqlCxCTableName & ".fecha")
   End If
'   sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, sqlCxCTableName & ".fecha")
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM CxC AS " & sqlCxCTableName
   SQL = SQL & " WHERE " & sqlCxCTableName & ".ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & sqlCxCTableName & ".CodigoCliente = CxC.CodigoCliente"
   SQL = SQL & " AND " & sqlCxCTableName & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   SQL = SQL & " AND " & sqlCxCTableName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoCxC = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxCHistoricoCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen() As String
   On Error Resume Next
   fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen = gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat
   On Error GoTo 0
End Function
Private Function fSQLSaldoInicialAnalisisCxCHistoricoAnticipos(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, ByVal valDescontarAbonos As Boolean, _
                     ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlAnticipoTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlAnticipoTableName = "Anticipo_1"
   sqlMonto = sqlAnticipoTableName & "." & "MontoTotal"
   If valDescontarAbonos Then
      sqlMonto = "(" & sqlMonto & _
            " - (" & sqlAnticipoTableName & "." & "MontoUsado" & _
            " + " & sqlAnticipoTableName & "." & "MontoDevuelto" & _
            " + " & sqlAnticipoTableName & "." & "MontoDiferenciaEnDevolucion" & "))"
   End If
   If valUsarCambio Then
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlAnticipoTableName & "." & "Cambio", _
               sqlAnticipoTableName & "." & "Moneda", sqlMonto, True, "")
   End If
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM ANTICIPO AS " & sqlAnticipoTableName
   SQL = SQL & " WHERE " & sqlAnticipoTableName & "." & "ConsecutivoCompania" & " = " & mConsecutivoCompania
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "Tipo" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "CodigoCliente" & " = ANTICIPO.CodigoCliente"
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "Moneda" & " = ANTICIPO.Moneda"
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "Status" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "Fecha" & " < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoAnticipos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxCHistoricoAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'*************************************************************
'mas nuevo nelson
Public Function fSQLAnalisisCXCHistorico(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
            ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
            ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlCxC As String
   Dim SqlAnticipos As String
   Dim sqlCobros As String
   Dim sqlOrderBy As String
   On Error GoTo h_ERROR
       
   sqlCxC = fSQLAnalisisCxCHistoricoCxC(valcodigoCliente, valFechaInicial, valFechaFinal, valUsarCambio, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SqlAnticipos = fSQLAnalisisCxCHistoricoAnticipos(valcodigoCliente, valFechaInicial, valFechaFinal, valUsarCambio, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   sqlCobros = fSQLAnalisisCxCHistoricoCobros(valcodigoCliente, valFechaInicial, valFechaFinal, valUsarCambio, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   sqlOrderBy = " ORDER BY " & "Codigo, MonedaReporte,TitulodelGrupo desc, ColumnaParaGrupoTipoReporte, Fecha, ColumnaParaGrupoDetalle"
   SQL = sqlCxC
   SQL = SQL & " UNION " & SqlAnticipos
   SQL = SQL & " UNION " & sqlCobros
   SQL = SQL & sqlOrderBy
   fSQLAnalisisCXCHistorico = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCXCHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxCHistoricoCxC(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
               ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
               ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlTipoDeCxC As String
   Dim sqlStatusCxC As String
   Dim sqlMontoTotalCxC As String
   Dim sqlMontoActualCxC As String
   Dim sqlColumnaParaGrupoDetalle As String
   Dim SqlSaldoInicialConAbonos As String
   Dim SqlSaldoInicialSinAbonos As String
   Dim sqlStatusCxCAnulado As String
   Dim sqlMoneda As String
   Dim sqlCambio As String
   Dim sqlMonedaReporte  As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlTipoDeCxC = gUtilSQL.DfSQLCaseIfForEnum(" CxC.TipoCxC", _
                  enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "")
   sqlStatusCxC = gUtilSQL.DfSQLCaseIfForEnum(" CxC.Status", _
                  enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True, False), "")
   sqlStatusCxCAnulado = " CxC.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   sqlMontoTotalCxC = "(CxC.MontoExento" & _
                    " + CxC.MontoGravado" & _
                    " + CxC.MontoIVA)"
   sqlMontoTotalCxC = gUtilSQL.getIIF(sqlStatusCxCAnulado, "0", sqlMontoTotalCxC, True)
   sqlMontoActualCxC = "(" & sqlMontoTotalCxC & " - CxC.MontoAbonado)"
   sqlMontoActualCxC = gUtilSQL.getIIF(sqlStatusCxCAnulado, "0", sqlMontoActualCxC, True)
   sqlMoneda = " CxC.Moneda"
   sqlCambio = " CxC.CambioABolivares"
   sqlMonedaReporte = " CxC.Moneda"
      If valUsarCambio Then
         sqlMontoTotalCxC = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoTotalCxC, True, "")
         sqlMontoActualCxC = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoActualCxC, True, "")
         sqlMontoTotalCxC = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoTotalCxC, "cxC.fecha")
         sqlMontoActualCxC = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoActualCxC, "cxC.fecha")
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      End If
   SqlSaldoInicialSinAbonos = fSQLSaldoInicialAnalisisCxCHistoricoCxC(valFechaInicial, valUsarCambio, False, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxCHistoricoCxC(valFechaInicial, valUsarCambio, True, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloDelGrupo, "
   SQL = SQL & " CxC.CodigoCliente AS Codigo, "
   SQL = SQL & sqlMoneda & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & sqlTipoDeCxC & " AS TipoDocumento, "
   SQL = SQL & gUtilSQL.fCast("CxC.TipoCxC" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "Cxc.Numero", eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & " CxC.Numero AS Numero, "
   SQL = SQL & gUtilSQL.fCast(" CxC.Fecha", eTDSS_VARCHAR, "") & " AS Fecha, "
   SQL = SQL & gUtilSQL.fCast(" CxC.FechaVencimiento", eTDSS_VARCHAR, "") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusCxC & " AS Status, "
   SQL = SQL & sqlMontoActualCxC & " AS MontoActual, "
   SQL = SQL & sqlMontoTotalCxC & " AS MontoTotal, "
   SQL = SQL & "Cliente.Nombre AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocCobrado, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocCobrado, "
   SQL = SQL & "0 AS MontoAbonado "
   SQL = SQL & " FROM Cliente INNER JOIN CxC ON "
   SQL = SQL & " CxC.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND CxC.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " WHERE CxC.ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValue(" CxC.CodigoCliente", Trim(valcodigoCliente), False)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", valFechaInicial, valFechaFinal)
   fSQLAnalisisCxCHistoricoCxC = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCxCHistoricoCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxCHistoricoAnticipos(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
         ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
         ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlTipoDeAnticipo As String
   Dim sqlStatusDeAnticipo As String
   Dim sqlMontoTotal As String
   Dim sqlMontoActual As String
   Dim sqlMontoNoDisponible As String
   Dim SqlSaldoInicialConAbonos As String
   Dim SqlSaldoInicialSinAbonos As String
   Dim sqlStatusAnticipoAnulado As String
   Dim sqlMoneda As String
   Dim sqlCambio As String
   Dim sqlMonedaReporte  As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
'   insAnticipos.SetTipoStr gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO)
   sqlTipoDeAnticipo = gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO))
   sqlStatusDeAnticipo = gUtilSQL.DfSQLCaseIfForEnum("Anticipo.Status", _
                        enum_StatusAnticipo.eSDA_VIGENTE, gEnumProyecto.fenumStatusAnticipoToStrinInArray(True), "")
   sqlMontoNoDisponible = "(Anticipo.MontoUsado" & _
                        " + Anticipo.MontoDevuelto" & _
                        " + Anticipo.MontoDiferenciaEnDevolucion" & ")"
   sqlStatusAnticipoAnulado = "Anticipo.Status" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   sqlMontoTotal = "Anticipo.MontoTotal"
   sqlMontoTotal = gUtilSQL.getIIF(sqlStatusAnticipoAnulado, "0", sqlMontoTotal, True)
   sqlMontoActual = "(" & sqlMontoTotal & " - " & sqlMontoNoDisponible & ")"
   sqlMontoActual = gUtilSQL.getIIF(sqlStatusAnticipoAnulado, "0", sqlMontoActual, True)
   sqlMoneda = "Anticipo.Moneda"
   sqlCambio = "Anticipo.Cambio"
   sqlMonedaReporte = "Anticipo.Moneda"
      If valUsarCambio Then
         sqlMontoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoTotal, True, "")
         sqlMontoActual = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoActual, True, "")
         sqlMontoNoDisponible = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoNoDisponible, "Anticipo.fecha")
         sqlMontoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoTotal, "Anticipo.fecha")
         sqlMontoActual = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoActual, "Anticipo.fecha")
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      End If
   SqlSaldoInicialSinAbonos = fSQLSaldoInicialAnalisisCxCHistoricoAnticipos(valFechaInicial, valUsarCambio, False, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxCHistoricoAnticipos(valFechaInicial, valUsarCambio, True, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("1") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloDelGrupo, "
   SQL = SQL & " Anticipo.CodigoCliente" & " AS Codigo, "
   SQL = SQL & sqlMoneda & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & sqlTipoDeAnticipo & " AS TipoDocumento, "
   SQL = SQL & gUtilSQL.fCast("Anticipo.ConsecutivoAnticipo", eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & " Anticipo.Numero" & " AS Numero, "
   SQL = SQL & gUtilSQL.fCast(" Anticipo.Fecha", eTDSS_VARCHAR, "") & " AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusDeAnticipo & " AS Status, "
   SQL = SQL & sqlMontoActual & " AS MontoActual, "
   SQL = SQL & sqlMontoTotal & " AS MontoTotal, "
   SQL = SQL & " Cliente.Nombre AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocCobrado, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocCobrado, "
   SQL = SQL & "0 AS MontoAbonado "
   SQL = SQL & " FROM Anticipo INNER JOIN Cliente ON "
   SQL = SQL & " Anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND Anticipo.CodigoCliente = Cliente.Codigo"
   SQL = SQL & " WHERE Anticipo.ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValue(" Anticipo.CodigoCliente", Trim(valcodigoCliente), False)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(" Anticipo.Fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " AND " & " Anticipo.Tipo" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   SQL = SQL & " AND " & " Anticipo.EsUnaDevolucion" & " = " & gUtilSQL.fBooleanToSqlValue(False)
   Set gEnumProyecto = Nothing
   fSQLAnalisisCxCHistoricoAnticipos = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLAnalisisCXCHistoricoAnticipos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Private Function fSQLAnalisisCxCHistoricoCobros(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
            ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
            ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim sqlStatusCobranza As String
   Dim sqlMontoCobranza As String
   Dim sqlTipoDeDocCobrado As String
   Dim sqlColumnaParaGrupoDetalle As String
   Dim sqlNumeroDocumentoCobrado As String
   Dim sqlMontoAbonado As String
   Dim SqlSaldoInicialConAbonos As String
   Dim SqlSaldoInicialSinAbonos As String
   Dim sqlStatusCobranzaAnulado As String
   Dim sqlMonedaDocs As String
   Dim sqlMonedaCob As String
   Dim sqlCambio As String
   Dim sqlMonedaReporte  As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlStatusCobranza = sqlStatusCobranza
   sqlStatusCobranza = gUtilSQL.getIIF(" Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA), _
            gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusCobranzaToString(eSC_ANULADA)), _
            gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusCobranzaToString(eSC_VIGENTE)), True)
   sqlStatusCobranzaAnulado = " Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA)
   sqlMontoCobranza = "IGV_DocCobradosPorMoneda.SumaMoneda"
   sqlMontoCobranza = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, "0", sqlMontoCobranza, True)
   sqlTipoDeDocCobrado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, gUtilSQL.fSimpleSqlValue(""), gUtilSQL.DfSQLCaseIfForEnum(" DocumentoCobrado.TipoDeDocumentoCobrado", _
                  enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), ""), True)
   sqlNumeroDocumentoCobrado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, gUtilSQL.fSimpleSqlValue(""), " DocumentoCobrado.NumeroDelDocumentoCobrado", True)
   sqlMontoAbonado = " DocumentoCobrado.MontoAbonado"
   sqlMontoAbonado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, "0", sqlMontoAbonado, True)
   sqlMonedaDocs = " Cobranza.Moneda"
   sqlMonedaCob = " CuentaBancaria.NombreDeLaMoneda"
   sqlCambio = " Cobranza.CambioABolivares"
   sqlMonedaReporte = "Moneda.Nombre"     '" Cobranza.Moneda"
      If valUsarCambio Then
         sqlMontoCobranza = " Cobranza.TotalCobrado"
         sqlMontoCobranza = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, "0", sqlMontoCobranza, True)
         sqlMontoCobranza = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaCob, sqlMontoCobranza, True, "")
         sqlMontoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaDocs, sqlMontoAbonado, True, "")
         sqlMontoCobranza = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoCobranza, " Cobranza.fecha")
         sqlMontoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoAbonado, " Cobranza.fecha")
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
      Else
         sqlMontoAbonado = " DocumentoCobrado.MontoAbonado / DocumentoCobrado.CambioAMonedaDeCobranza"
      End If
   sqlMontoAbonado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado & " OR Cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
                      "0", sqlMontoAbonado, True)
   sqlMontoCobranza = gUtilSQL.getIIF(sqlStatusCobranzaAnulado & " OR Cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
                      "0", sqlMontoCobranza, True)
   sqlColumnaParaGrupoDetalle = gUtilSQL.fCast("Numero", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.fCast(gUtilSQL.fSimpleSqlValue("0"), eTDSS_VARCHAR, "")
   SqlSaldoInicialSinAbonos = "0"
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxCHistoricoCobranzas(valFechaInicial, valUsarCambio, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cobranzas") & " AS TituloDelGrupo, "
   SQL = SQL & " Cobranza.CodigoCliente AS Codigo, "
   SQL = SQL & sqlMonedaCob & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.getIIF("Cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
            gUtilSQL.fSimpleSqlValue("Cobranza(*)"), gUtilSQL.fSimpleSqlValue("Cobranza"), True) & " AS TipoDocumento, "
   SQL = SQL & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & " Cobranza.Numero AS Numero, "
   SQL = SQL & gUtilSQL.fCast(" Cobranza.Fecha", eTDSS_VARCHAR, "") & " AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusCobranza & " AS Status, "
   SQL = SQL & sqlMontoCobranza & " AS MontoActual, "
   SQL = SQL & " 0 AS MontoTotal, "
   SQL = SQL & " Cliente.Nombre AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & sqlTipoDeDocCobrado & " AS TipoDocCobrado, "
   SQL = SQL & sqlNumeroDocumentoCobrado & " AS NumeroDocCobrado, "
   SQL = SQL & sqlMontoAbonado & " AS MontoAbonado "
   
   SQL = SQL & " FROM ((Cliente INNER JOIN " & " Cobranza ON ("
   SQL = SQL & "Cliente.Codigo = " & " Cobranza.CodigoCliente"
   SQL = SQL & ") AND (Cliente.ConsecutivoCompania = " & " Cobranza.ConsecutivoCompania"
   SQL = SQL & ")) INNER JOIN  DocumentoCobrado ON ("
   SQL = SQL & " Cobranza.Numero = DocumentoCobrado.NumeroCobranza"
   SQL = SQL & ") AND ( Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   SQL = SQL & ")) LEFT JOIN CuentaBancaria ON ("
   SQL = SQL & " Cobranza.CodigoCuentaBancaria =  CuentaBancaria.Codigo"
   SQL = SQL & ") AND ( Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania)"
   SQL = SQL & "   INNER JOIN Moneda ON DocumentoCobrado.CodigoMonedaDeCxC = Moneda.Codigo"
   SQL = SQL & " INNER JOIN IGV_DocCobradosPorMoneda  On"
   SQL = SQL & " (cobranza.ConsecutivoCompania = IGV_DocCobradosPorMoneda.ConsecutivoCompania)"
   SQL = SQL & " AND (cobranza.Numero = IGV_DocCobradosPorMoneda.NumeroCobranza) "
   SQL = SQL & " AND (Moneda.Codigo = IGV_DocCobradosPorMoneda.CodigoMonedaDeCxc) "
   
   SQL = SQL & " WHERE  Cobranza.ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValue(" Cobranza.CodigoCliente", Trim(valcodigoCliente), False)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(" Cobranza.Fecha", valFechaInicial, valFechaFinal)
   sqlTipoDeDocCobrado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, gUtilSQL.fSimpleSqlValue(""), gUtilSQL.fSimpleSqlValue("Anticipo"), True)
   sqlNumeroDocumentoCobrado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado, gUtilSQL.fSimpleSqlValue(""), "AnticipoCobrado.NumeroAnticipo", True)
   If valUsarCambio Then
      sqlMontoAbonado = "AnticipoCobrado.MontoAplicado "
   Else
      sqlMontoAbonado = "AnticipoCobrado.MontoAplicado / AnticipoCobrado.Cambio"
   End If
   sqlMontoAbonado = gUtilSQL.getIIF(sqlStatusCobranzaAnulado & " OR Cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
                      "0", sqlMontoAbonado, True)
   sqlColumnaParaGrupoDetalle = gUtilSQL.fCast("Numero", eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.fCast(gUtilSQL.fSimpleSqlValue("1"), eTDSS_VARCHAR, "")
   
   SQL = SQL & " UNION "
   SQL = SQL & "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cobranzas") & " AS TituloDelGrupo, "
   SQL = SQL & " Cobranza.CodigoCliente AS Codigo, "
   SQL = SQL & sqlMonedaCob & " AS Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & gUtilSQL.getIIF("Cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
            gUtilSQL.fSimpleSqlValue("Cobranza(*)"), gUtilSQL.fSimpleSqlValue("Cobranza"), True) & " AS TipoDocumento, "
   SQL = SQL & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   SQL = SQL & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   SQL = SQL & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   SQL = SQL & " Cobranza.Numero AS Numero, "
   SQL = SQL & " Cobranza.Fecha AS Fecha, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   SQL = SQL & sqlStatusCobranza & " AS Status, "
   SQL = SQL & "0 AS MontoActual, "
   SQL = SQL & sqlMontoCobranza & " AS MontoTotal, "
   SQL = SQL & " Cliente.Nombre AS Nombre, "
   SQL = SQL & sqlCambio & " AS CambioBs, "
   SQL = SQL & sqlTipoDeDocCobrado & " AS TipoDocCobrado, "
   SQL = SQL & sqlNumeroDocumentoCobrado & " AS NumeroDocCobrado, "
   SQL = SQL & sqlMontoAbonado & " AS MontoAbonado "
   SQL = SQL & " FROM ((Cliente INNER JOIN Cobranza ON ("
   SQL = SQL & " Cliente.Codigo = Cobranza.CodigoCliente"
   SQL = SQL & ") AND (Cliente.ConsecutivoCompania = Cobranza.ConsecutivoCompania"
   SQL = SQL & ")) INNER JOIN AnticipoCobrado ON ("
   SQL = SQL & " Cobranza.Numero = AnticipoCobrado.NumeroCobranza "
   SQL = SQL & ") AND (Cobranza.ConsecutivoCompania =  AnticipoCobrado.ConsecutivoCompania"
   SQL = SQL & ")) LEFT JOIN CuentaBancaria ON ("
   SQL = SQL & " Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo"
   SQL = SQL & ") AND ( Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania)"
   SQL = SQL & "   INNER JOIN Moneda ON AnticipoCobrado.CodigoMoneda = Moneda.Codigo"
   SQL = SQL & " INNER JOIN IGV_DocCobradosPorMoneda  On"
   SQL = SQL & " (cobranza.ConsecutivoCompania = IGV_DocCobradosPorMoneda.ConsecutivoCompania)"
   SQL = SQL & " AND (cobranza.Numero = IGV_DocCobradosPorMoneda.NumeroCobranza) "
   SQL = SQL & " AND (Moneda.Codigo = IGV_DocCobradosPorMoneda.CodigoMonedaDeCxc) "
   SQL = SQL & " WHERE Cobranza.ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValue(" Cobranza.CodigoCliente", Trim(valcodigoCliente), False)
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(" Cobranza.Fecha", valFechaInicial, valFechaFinal)
   fSQLAnalisisCxCHistoricoCobros = SQL
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fsqlAnalisisCxCHistoricoCobros", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fSQLSaldoInicialAnalisisCxCHistoricoCobranzas(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, _
   ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL  As String
   Dim sqlCobranzaTableName As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   sqlCobranzaTableName = "Cobranza_1"
   sqlMonto = "(" & sqlCobranzaTableName & ".TotalCobrado)"
   If valUsarCambio Then
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCobranzaTableName & ".CambioABolivares", _
               sqlCobranzaTableName & ".Moneda", sqlMonto, True, "")
   End If
   sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, sqlCobranzaTableName & ".fecha")
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM Cobranza AS " & sqlCobranzaTableName
   SQL = SQL & " WHERE " & sqlCobranzaTableName & ".ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & sqlCobranzaTableName & ".CodigoCliente = Cobranza.CodigoCliente"
   SQL = SQL & " AND " & sqlCobranzaTableName & ".Moneda = Cobranza.Moneda"
   SQL = SQL & " AND StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   SQL = SQL & " AND " & sqlCobranzaTableName & ".TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaDeFactura)
   SQL = SQL & " AND Fecha < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoCobranzas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxCHistoricoCobranzas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'************************************************************
' mas nuevo aun
Public Function fConstruirSQLDelReporteDeCXCPorVendedor(ByVal mConsecutivoCompania As String, ByVal mMonedaLocal As String, _
            ByVal mMontoTotalYRestanteConIva As Boolean, ByVal mTasaDeCambio As Boolean, ByVal mCantidadAImprimir As String, _
            ByVal mCodigoVendedor As String, ByVal dtpFechaInicial As Date, ByVal dtpFechaFinal As Date, ByVal gUltimaTasaDeCambio As Object, _
            ByVal gMonedaLocalActual As Object, ByVal valMostrarUltimaFechaPago As Boolean, ByVal usarContacto As Boolean) As String
   Dim SQL As String
   Dim sqlDateDiff As String
   Dim sqlMtoTotal As String
   Dim sqlMtoAbonado As String
   Dim sqlMtoRestante As String
   Dim sqlTasaIVA As String
   Dim tasaOrig As Boolean
   Dim sqlMonedaReporte As String
   Dim sqlCambio As String
   Dim gEnumReport As clsEnumReport
   Dim insCobranzaVista As clsCobranzaVista
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   Set insCobranzaVista = New clsCobranzaVista
   sqlDateDiff = gUtilSQL.getDateDiff("d", "Cxc.FechaVencimiento", _
                  gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
   If mMontoTotalYRestanteConIva = True Then
      sqlMtoAbonado = "Cxc.MontoAbonado"
      sqlMtoRestante = "((" & "Cxc.MontoExento" & _
                        " + Cxc.MontoGravado" & _
                        " + Cxc.MontoIVA" & _
                        ") - Cxc.MontoAbonado)"
      sqlMtoTotal = "(" & "Cxc.MontoExento" & _
                        " + Cxc.MontoGravado" & _
                        " + Cxc.MontoIVA)"
   Else
      sqlTasaIVA = "(( Cxc.MontoIVA * 100 ) /" & _
                                 "Cxc.MontoGravado))"
      sqlMtoAbonado = gUtilSQL.getIIF("Cxc.MontoGravado = 0 ", _
                     "Cxc.MontoAbonado", _
                     "(" & "Cxc.MontoAbonado / (1 + " & sqlTasaIVA & ")", True)
      sqlMtoRestante = "((" & "Cxc.MontoExento" & _
                        " + " & "Cxc.MontoGravado" & _
                        ") - " & sqlMtoAbonado & ")"
      sqlMtoTotal = "(" & "Cxc.MontoExento" & _
                        " + Cxc.MontoGravado)"
   End If
   sqlCambio = "Cxc.CambioABolivares"
   sqlMonedaReporte = "Cxc.Moneda"
   If mMonedaLocal = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gMonedaLocalActual.GetHoyNombreMoneda) Then
     '   tasaOrig = optTasaDeCambio(0).Value
      tasaOrig = mTasaDeCambio
      If tasaOrig Then
         sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoTotal, tasaOrig, "")
         sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoRestante, tasaOrig, "")
         sqlMtoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoAbonado, tasaOrig, "")
         sqlMtoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoAbonado, "Cxc.Fecha")
         sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoRestante, "Cxc.Fecha")
         sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoTotal, "Cxc.Fecha")
      Else
         sqlCambio = gUltimaTasaDeCambio.fSQLCampoTasaDeCambio(sqlCambio, sqlMonedaReporte, tasaOrig, "")
         sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoTotal, tasaOrig, "")
         sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoRestante, tasaOrig, "")
         sqlMtoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoAbonado, tasaOrig, "")
      End If
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   End If
   SQL = "SELECT "
   SQL = SQL & " Cxc.CodigoVendedor, "
   If (usarContacto) Then
      SQL = SQL & " Cliente.Contacto, "
   End If
   SQL = SQL & gUtilSQL.DfCStrSQL(" Cxc.CodigoVendedor")
   SQL = SQL & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat
   SQL = SQL & gUtilSQL.DfCStrSQL("Vendedor.Nombre") & " AS NombreVendedor, "
   SQL = SQL & " Cxc.Moneda, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & " Cxc.CodigoCliente, "
   SQL = SQL & gUtilSQL.DfCStrSQL(" Cxc.CodigoCliente") ' & ", "
   SQL = SQL & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat
   SQL = SQL & gUtilSQL.DfCStrSQL(" Cliente.Nombre")
    If (valMostrarUltimaFechaPago) Then
   SQL = SQL & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat
   SQL = SQL & gUtilSQL.DfCStrSQL(" Cliente.Telefono")
   End If
   SQL = SQL & " AS NombreCliente, "
   SQL = SQL & " Cxc.Numero, "
   SQL = SQL & " Cxc.Fecha AS FechaDoc, "
   SQL = SQL & " Cxc.FechaVencimiento, "
   SQL = SQL & sqlDateDiff & " AS DiasVencidos, "
   SQL = SQL & sqlMtoTotal & " AS MontoTotal, "
   SQL = SQL & sqlMtoAbonado & " AS MontoAbonado, "
   SQL = SQL & sqlMtoRestante & " AS MontoRestante "
   If (valMostrarUltimaFechaPago) Then
   SQL = SQL & ", ISNULL(CONVERT(VARCHAR(10), IGV_CobranzaUltimaFechaAbono.UltimaFechAbono,103) ,'') AS UltimaFechAbono"
   End If
   SQL = SQL & " FROM Cliente INNER JOIN ("
   SQL = SQL & " Vendedor INNER JOIN " & " Cxc ON ("
   SQL = SQL & " Vendedor.Codigo" & _
               " = " & " Cxc.CodigoVendedor) AND ("
   SQL = SQL & " Vendedor.ConsecutivoCompania" & _
               " = Cxc.ConsecutivoCompania)) ON ("
   SQL = SQL & " Cliente.Codigo" & _
               " = Cxc.CodigoCliente) AND ("
   SQL = SQL & " Cliente.ConsecutivoCompania" & _
               " =  Cxc.ConsecutivoCompania)"
   If (valMostrarUltimaFechaPago) Then
   SQL = SQL & "   LEFT JOIN " & insCobranzaVista.GetViewNameCobranzaUltimaFecha() & " AS IGV_CobranzaUltimaFechaAbono ON "
   SQL = SQL & " Cxc.ConsecutivoCompania = IGV_CobranzaUltimaFechaAbono.ConsecutivoCompania AND "
   SQL = SQL & " Cxc.NumeroDocumentoOrigen = IGV_CobranzaUltimaFechaAbono.NumeroDelDocumentoCobrado "
   End If
   SQL = SQL & " WHERE  Cxc.ConsecutivoCompania" & _
               " = " & mConsecutivoCompania
   SQL = SQL & " AND  Cxc.Status IN ('" & _
               gConvert.charAEnumerativoInt(gConvert.enumerativoAChar(eSD_PORCANCELAR)) & "', '" & _
               gConvert.charAEnumerativoInt(gConvert.enumerativoAChar(eSD_ABONADO)) & "')"
   If mCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND  Cxc.CodigoVendedor" & _
                  " = '" & Trim(mCodigoVendedor) & "' "
   End If
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(" Cxc.Fecha", dtpFechaInicial, dtpFechaFinal)
   SQL = SQL & " ORDER BY "
   SQL = SQL & " Cxc.CodigoVendedor, "
   SQL = SQL & " Cxc.Moneda, "
   SQL = SQL & " Cxc.CodigoCliente, "
   SQL = SQL & " Cxc.Fecha, "
   SQL = SQL & " Cxc.FechaVencimiento, "
   SQL = SQL & " Cxc.Numero"
   fConstruirSQLDelReporteDeCXCPorVendedor = SQL
   Set gEnumReport = Nothing
   Set insCobranzaVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLDelReporteDeCXCPorVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteDeEstadisticoDeVendedor(ByVal mConsecutivoCompania As String, ByVal mMonedaReporte As String, ByVal mDtpFechaInicial As Date, _
                     ByVal mDtpFechaFinal As Date, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim sqlTotalPorCambio As String
   Dim adicionalAlGroupBy As String
   Dim Formato As String
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   sqlTotalPorCambio = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalBaseImponible", "Factura.Fecha"), True, "")
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.DfSQLYearOfDate("Factura.Fecha", "Anno") & ", "
   SQL = SQL & gUtilSQL.fFirst("Vendedor.Nombre", "NombreDelVendedor") & ", "
   SQL = SQL & " Factura.CodigoVendedor AS CodigoDelVendedor, "
      If mMonedaReporte = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gMonedaLocalActual.GetHoyNombreMoneda) Then
         SQL = SQL & " Factura.Moneda, "
         SQL = SQL & "SUM(" & sqlTotalPorCambio & ") AS AcumBaseImponible, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 1 ", sqlTotalPorCambio, 0) & ") AS Ene, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 2 ", sqlTotalPorCambio, 0) & ") AS Feb, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 3 ", sqlTotalPorCambio, 0) & ") AS Mar, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 4 ", sqlTotalPorCambio, 0) & ") AS Abr, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 5 ", sqlTotalPorCambio, 0) & ") AS May, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 6 ", sqlTotalPorCambio, 0) & ") AS Jun, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 7 ", sqlTotalPorCambio, 0) & ") AS Jul, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 8 ", sqlTotalPorCambio, 0) & ") AS Ago, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 9 ", sqlTotalPorCambio, 0) & ") AS Sep, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 10", sqlTotalPorCambio, 0) & ") AS Oct, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 11", sqlTotalPorCambio, 0) & ") AS Nov, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 12", sqlTotalPorCambio, 0) & ") AS Dic  "
         adicionalAlGroupBy = ", " & " Factura.Moneda"
      Else
         SQL = SQL & " Factura.Moneda, "
         SQL = SQL & "SUM(" & " Factura.TotalBaseImponible) AS AcumBaseImponible, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 1 ", " Factura.TotalBaseImponible ", 0) & ") AS Ene, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 2 ", " Factura.TotalBaseImponible ", 0) & ") AS Feb, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 3 ", " Factura.TotalBaseImponible ", 0) & ") AS Mar, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 4 ", " Factura.TotalBaseImponible ", 0) & ") AS Abr, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 5 ", " Factura.TotalBaseImponible ", 0) & ") AS May, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 6 ", " Factura.TotalBaseImponible ", 0) & ") AS Jun, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 7 ", " Factura.TotalBaseImponible ", 0) & ") AS Jul, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 8 ", " Factura.TotalBaseImponible ", 0) & ") AS Ago, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 9 ", " Factura.TotalBaseImponible ", 0) & ") AS Sep, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 10", " Factura.TotalBaseImponible ", 0) & ") AS Oct, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 11", " Factura.TotalBaseImponible ", 0) & ") AS Nov, "
         SQL = SQL & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 12", " Factura.TotalBaseImponible ", 0) & ") AS Dic  "
         adicionalAlGroupBy = ", " & " Factura.Moneda"
      End If
   SQL = SQL & " FROM Factura "
   SQL = SQL & " INNER JOIN Vendedor"
   SQL = SQL & " ON Vendedor.Codigo = " & " Factura.CodigoVendedor"
   SQL = SQL & " AND " & " Factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(" Factura.Fecha", mDtpFechaInicial, mDtpFechaFinal)
   SQL = SQL & ") AND (" & " Factura.ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & ") AND (" & " Factura.StatusFactura ='" & gConvert.enumerativoAChar(enum_StatusFactura.eSF_EMITIDA) & "')"
   SQL = SQL & " GROUP BY " & " Factura.CodigoVendedor, "
   SQL = SQL & " Vendedor.Nombre, "
   SQL = SQL & gUtilSQL.DfSQLYearOfDate(" Factura.Fecha")
   SQL = SQL & adicionalAlGroupBy
   SQL = SQL & " ORDER BY " & gUtilSQL.DfSQLYearOfDate(" Factura.Fecha") & ", "
   SQL = SQL & " Factura.CodigoVendedor, "
   SQL = SQL & " Vendedor .Nombre"
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteDeEstadisticoDeVendedor = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteDeEstadisticoDeVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'********************************************************************
' mas nuevo aun q el resto

Public Function fSQLDelReporteCxCEntreFechas(ByVal mUsarCambioOriginal As Boolean, ByVal insCamposDefFactura As Object, _
               ByVal mMonedaDeLosReportes As String, ByVal mConsecutivoCompania As String, _
               ByVal mCamposDefinibles As Object, ByVal gProyParametrosCompania As Object, ByVal mStatus As String, _
               ByVal mDtpFechaInicial As Date, ByVal mDtpFechaFinal As Date, _
               ByVal mUsaModuloDeContabilidad As Boolean, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object, _
               ByVal gListLibrary As Object, _
               ByVal AgruparTipo As String, _
               ByVal FiltroPorTipo As String, _
               ByVal usarContacto As Boolean) As String
   Dim statusCxC As String
   Dim sqlCamposDefFactura As String
   Dim SQL As String
   Dim sqlStatusCxC As String
   Dim sqlMonto As String
   Dim sqlCambioAMonedaLocal As String
   Dim sqlMonedaGrupo As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
  
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumReport = New clsEnumReport
   insCamposDefFactura.setClaseDeTrabajo eCTFC_Factura
   sqlStatusCxC = gUtilSQL.DfSQLCaseIfForEnum(" CxC.Status", _
                  enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True, False), "StatusStr")
   sqlMonto = gUtilSQL.getIIF(" CxC.Status" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               "( CxC.MontoExento" & _
               " + CxC.MontoGravado" & _
               " + CxC.MontoIVA) - " & _
               " CxC.MontoAbonado", True)
   sqlCambioAMonedaLocal = gUltimaTasaDeCambio.fSQLCampoTasaDeCambio(" CxC.CambioABolivares", _
                              " CxC.Moneda", Not mUsarCambioOriginal, "")
   sqlMonedaGrupo = " CxC.Moneda"
      If (mMonedaDeLosReportes = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gMonedaLocalActual.GetHoyNombreMoneda)) Then
         sqlMonedaGrupo = ""
         sqlMonedaGrupo = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
         sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMonto, " CxC.Fecha")
      End If

      sqlCamposDefFactura = insCamposDefFactura.fSqlSubConsultSelectCamposDefinibles(" CxC.Numero", _
                     gListLibrary.fAmpersand & gListLibrary.fSeparadorDeColumnasParaSQLSearch(Chr(13)) & gListLibrary.fAmpersand, _
                     True, gProyParametrosCompania.fSelectedIndexCampoDefFromComboBox(mCamposDefinibles))
                     
      If sqlCamposDefFactura <> "" Then
         sqlCamposDefFactura = "(" & sqlCamposDefFactura & ")"
      Else
         sqlCamposDefFactura = "''"
      End If
      Select Case mStatus
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_PORCANCELAR)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_CANCELADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_CHEQUEDEVUELTO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CHEQUEDEVUELTO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_ABONADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_ANULADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_REFINANCIADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_REFINANCIADO)
         Case Else: statusCxC = ""
      End Select
      
   SQL = "SELECT "
   If AgruparTipo = "SectorDeNegocio" Then
      SQL = SQL & " Cliente.SectorDeNegocio, "
   ElseIf AgruparTipo = "ZonaDeCobranza" Then
      SQL = SQL & " Cliente.ZonaDeCobranza, "
   End If
   
   If (usarContacto) Then
      SQL = SQL & " Cliente.Contacto,"
   End If
   SQL = SQL & " Cliente.Codigo, "
   SQL = SQL & " Cliente.Nombre, "
   SQL = SQL & " CxC.Fecha, "
   SQL = SQL & " CxC.Numero, "
   SQL = SQL & " CxC.Moneda, "
   SQL = SQL & " CxC.CambioABolivares, "
   SQL = SQL & sqlStatusCxC & ", "
   SQL = SQL & sqlCambioAMonedaLocal & " AS CambioAMonedaLocal, "
   SQL = SQL & sqlCamposDefFactura & " AS InformacionAdicional, "
   SQL = SQL & sqlMonto & " AS Monto, "
   SQL = SQL & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioAMonedaLocal, "Moneda", _
                         sqlMonto, mUsarCambioOriginal, "MontoEnMonedaLocal") & ", "
   SQL = SQL & sqlMonedaGrupo & " AS MonedaParaGrupo"
   If mUsaModuloDeContabilidad Then
      SQL = SQL & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(mConsecutivoCompania, mDtpFechaInicial, mDtpFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQGenerarCuentaContable(mConsecutivoCompania, mDtpFechaInicial, mDtpFechaFinal), gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      SQL = SQL & " AS NumeroComprobante"
   End If
   SQL = SQL & " FROM  Cliente INNER JOIN CxC ON "
   SQL = SQL & " Cliente.Codigo" & _
               " = CxC.CodigoCliente"
   SQL = SQL & " AND Cliente.ConsecutivoCompania" & _
               " = CxC.ConsecutivoCompania"
   SQL = SQL & " WHERE "
   If AgruparTipo = "SectorDeNegocio" Then
     If FiltroPorTipo <> "TODOS" Then
       SQL = SQL & " Cliente.SectorDeNegocio= '" & FiltroPorTipo & "' AND "
     End If
   End If
   
   If AgruparTipo = "ZonaDeCobranza" Then
      If FiltroPorTipo <> "TODAS" Then
        SQL = SQL & " Cliente.ZonaDeCobranza='" & FiltroPorTipo & "' AND "
     End If
   End If
   
   SQL = SQL & " Cliente.ConsecutivoCompania" & _
               " = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(" CxC.Fecha", _
                        mDtpFechaInicial, mDtpFechaFinal)
                        
      If statusCxC <> "" Then
         SQL = SQL & " AND  CxC.Status = " & statusCxC
      End If

   SQL = SQL & " ORDER BY "
   If AgruparTipo = "SectorDeNegocio" Then
      SQL = SQL & " Cliente.SectorDeNegocio, "
   ElseIf AgruparTipo = "ZonaDeCobranza" Then
      SQL = SQL & " Cliente.ZonaDeCobranza, "
   End If
  
   SQL = SQL & " CxC.Status, "
'   If getIsSQLDataBase Then
      SQL = SQL & " MonedaParaGrupo,"
'   Else
'      SQL = SQL & sqlMonedaGrupo & ", "
'   End If
   SQL = SQL & " CxC.Fecha, "
   SQL = SQL & " CxC.Numero"
   fSQLDelReporteCxCEntreFechas = SQL
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLDelReporteCxCEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQGenerarCuentaContable(ByVal mConsecutivoCompania As String, ByVal mDtpFechaInicial As Date, ByVal mDtpFechaFinal As Date) As String
   Dim SQL As String
   Dim SqlPeriodo As String
   On Error GoTo h_ERROR
   
   SQL = SQL & "SELECT Periodo.ConsecutivoPeriodo"
   SQL = SQL & " FROM Periodo WHERE "
   SQL = SQL & "Periodo.ConsecutivoCompania = "
   SQL = SQL & mConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(SQL, False)
   SQL = ""
   SQL = "(SELECT  Comprobante.NUMERO"
   SQL = SQL & " FROM Comprobante WHERE "
   SQL = SQL & " Comprobante.NoDocumentoOrigen " & "="
   SQL = SQL & " Cxc.TipoCxC"
   SQL = SQL & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat
   SQL = SQL & " Cxc.Numero"
   SQL = SQL & " AND Comprobante.GeneradoPor" & " = "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXC))
   SQL = SQL & " AND Comprobante.ConsecutivoPeriodo"
   SQL = SQL & " IN (" & SqlPeriodo & ")"
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", _
                        mDtpFechaInicial, mDtpFechaFinal) & ")"
   fSQGenerarCuentaContable = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQGenerarCuentaContable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLChequesDevueltos(ByVal vReporteEnMonedaLocal As Boolean, ByVal vUsarCambioOriginal As Boolean, ByVal gMonedaLocalActual As Object, _
                                      ByVal gUltimaTasaDeCambio As Object, ByVal valConsecutivoCompania As String, ByVal valFechaInicial As Date, _
                                       ByVal valFechaFinal As Date, ByVal valcodigoVendedor As String, valStatus As String) As String
   Dim SQL As String
   Dim sqlCambio As String
   Dim sqlMoneda As String
   Dim sqlMonedaReporte As String
   Dim sqlMontoCheque As String
   Dim sqlMontoAbonado As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim statusCxC As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo

   sqlCambio = "CxC.cambioABolivares"
   sqlMoneda = "CxC.Moneda"
   sqlMonedaReporte = "CxC.Moneda"
   sqlMontoCheque = "SUM(CxC.MontoExento + CxC.MontoGravado + CxC.MontoIva)"
   sqlMontoAbonado = "SUM(CxC.MontoAbonado)"
   If vReporteEnMonedaLocal Then
      If vUsarCambioOriginal Then
         sqlCambio = gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.GetHoyCodigoMoneda, sqlCambio, "CxC.Fecha")
      Else
         sqlCambio = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", sqlMoneda, 1, vUsarCambioOriginal, "")
      End If
      sqlMontoCheque = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoCheque, vUsarCambioOriginal, "")
      sqlMontoCheque = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoCheque, "CxC.Fecha")
      sqlMontoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMontoAbonado, vUsarCambioOriginal, "")
      sqlMontoAbonado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoAbonado, "CxC.Fecha")
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.fNombreMoneda(gUtilDate.getFechaDeHoy))
   End If
   
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("CxC.Status", enum_StatusDocumento.eSD_PORCANCELAR, _
                        gEnumProyecto.fEnumStatusCxCToStringInArray(True), "") & " AS StatusStr, "
   SQL = SQL & "CxC.Status, CxC.Numero, CxC.Fecha, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaDelReporte, "
   SQL = SQL & sqlMontoCheque & " AS MontoCheque, "
   SQL = SQL & sqlMontoAbonado & " AS TotalAbonado, "
   SQL = SQL & "CxC.FechaCancelacion, CxC.FechaVencimiento, CxC.FechaAnulacion, "
   SQL = SQL & sqlMoneda & " AS Moneda, "
   SQL = SQL & sqlCambio & " AS CambioABolivares, "
   SQL = SQL & "CxC.NumeroDocumentoOrigen, CxC.CodigoMoneda, "
   SQL = SQL & "Vendedor.Nombre AS Vendedor, Cliente.Nombre AS Cliente "
   SQL = SQL & "FROM CxC INNER JOIN "
   SQL = SQL & " Cliente ON CxC.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   SQL = SQL & " AND CxC.CodigoCliente = Cliente.Codigo INNER JOIN"
   SQL = SQL & " Vendedor ON CxC.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   SQL = SQL & " AND CxC.CodigoVendedor = Vendedor.Codigo"
   SQL = SQL & " WHERE (CxC.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (CxC.TipoCxc = " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoCxP.eTCxP_CHEQUEDEVUELTO) & ")"
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", valFechaInicial, valFechaFinal)
   If LenB(Trim(valcodigoVendedor)) > 0 Then
      SQL = SQL & " AND (CxC.codigoVendedor = " & valcodigoVendedor & ")"
   End If
   Select Case valStatus
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_PORCANCELAR)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_CANCELADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CANCELADO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_CHEQUEDEVUELTO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_CHEQUEDEVUELTO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_ABONADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_ANULADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
         Case gEnumProyecto.enumStatusDocumentoToString(enum_StatusDocumento.eSD_REFINANCIADO)
               statusCxC = gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_REFINANCIADO)
         Case Else: statusCxC = ""
      End Select
   
   If LenB(Trim(valcodigoVendedor)) > 0 Then
      SQL = SQL & " AND (CxC.codigoVendedor = " & valcodigoVendedor & ")"
   End If
    If statusCxC <> "" Then
         SQL = SQL & " AND  CxC.Status = " & statusCxC
   End If
      
   SQL = SQL & " GROUP BY CxC.ConsecutivoCompania, CxC.Numero, CxC.Status, CxC.TipoCxc, CxC.Fecha, CxC.FechaCancelacion, CxC.Moneda, CxC.CambioAbolivares, "
   SQL = SQL & " CxC.NumeroDocumentoOrigen, CxC.CodigoMoneda, Vendedor.Nombre, Cliente.Nombre, CxC.MontoAbonado, CxC.FechaVencimiento, cxC.fechaAnulacion "
   SQL = SQL & " ORDER BY MonedaDelReporte, StatusStr, CxC.Fecha, CxC.Numero"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLChequesDevueltos = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLChequesDevueltos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLHistoricoDeClienteCxCNew(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String, _
                     ByVal valUsarCambioBs As Boolean, ByVal mConsecutivoCompania As String, ByVal usarCambioOriginal As Boolean, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim insCxCVista As clsCxCVista
   Dim sqlMonto As String
   Dim sqlSaldoActual As String
   Dim sqlMontoCobrado As String
   Dim sqlMonedaReporte As String
   Dim sqlCliente As String
   Dim SqlSaldoInicial As String
   Dim SaldoInicialConAbonos As String
   Dim sqlCambioCobrado As String
   Dim sqlCambioMonto As String
   Dim sqlCambioSaldoActual As String
   On Error GoTo h_ERROR
   Set insCxCVista = New clsCxCVista
   sqlMontoCobrado = "(MontoCobrado)"
   sqlMontoCobrado = gUtilSQL.getIIF("StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA), "0", sqlMontoCobrado)
   sqlMonto = "MontoOriginal"
   sqlSaldoActual = "SaldoActual"
   sqlMonedaReporte = "MonedaReporte"
   If valUsarCambioBs Then
      If usarCambioOriginal Then
         sqlCambioMonto = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "CambioABolivares", "FechaDocumento"), 2)
         sqlCambioSaldoActual = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "CambioABolivares", "FechaDocumento"), 2)
         sqlCambioCobrado = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "CambioABolivares", "FechaCobranza"), 2)
      Else
         sqlCambioMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "MonedaReporte", 1, usarCambioOriginal, "")
         sqlCambioSaldoActual = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "MonedaReporte", 1, usarCambioOriginal, "")
         sqlCambioCobrado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "MonedaReporte", "MontoCobrado", usarCambioOriginal, "")
      End If
      sqlMontoCobrado = gUtilSQL.getIIF("(CodMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                                   "(CodMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                                   gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoCobrado, "FechaCobranza") _
                                   , sqlCambioCobrado, True)

      sqlMonto = gUtilSQL.getIIF("(CodMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                                   "(CodMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                                   gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, "FechaDocumento") _
                                   , gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioMonto, insCxCVista.GetViewNameInfoCxCHistoricoCliente & ".MonedaReporte", "MontoOriginal", usarCambioOriginal, ""), True)

      sqlSaldoActual = gUtilSQL.getIIF("(CodMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                                   "(CodMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                                   gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlSaldoActual, "FechaDocumento") _
                                   , gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioSaldoActual, insCxCVista.GetViewNameInfoCxCHistoricoCliente & ".MonedaReporte", "SaldoActual", usarCambioOriginal, ""), True)
      sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   Else
      sqlMontoCobrado = "(MontoCobrado)"
   End If
   If valcodigoCliente <> "" Then
      sqlCliente = " AND " & " Codigo = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
   SqlSaldoInicial = fSQLSaldoInicialAnalisisCxCHistoricoCxCVista(valFechaInicial, valUsarCambioBs, False, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxCHistoricoCxCVista(valFechaInicial, valUsarCambioBs, True, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio) ' prueba
'*************************************************
   SQL = SQL & "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   SQL = SQL & "Codigo, "
   SQL = SQL & "NombreCliente AS Nombre, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & "MonedaReporte AS Moneda, "
   SQL = SQL & " TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & SaldoInicialConAbonos & " As SaldoInicialConAbonos, "
   SQL = SQL & "FechaDocumento, "
   SQL = SQL & "FechaVencimiento, "
   SQL = SQL & "TipoDeDocumento AS TipoDeDocumento, "
   SQL = SQL & "NumeroDocumento, "
   SQL = SQL & "NumeroDocumentoGrupo, "
   SQL = SQL & sqlMonto & " AS MontoOriginal, "
   SQL = SQL & sqlSaldoActual & " AS SaldoActual, "
   SQL = SQL & " TipoDocumentoDetalle,"
   SQL = SQL & "'' AS NumeroCobranza, '' AS FechaCobranza, 0 AS MontoCobrado, '' AS StatusCobranza "
   SQL = SQL & " FROM " & insCxCVista.GetViewNameInfoCxCHistoricoCliente
   SQL = SQL & " WHERE " & insCxCVista.GetViewNameInfoCxCHistoricoCliente & ".ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(insCxCVista.GetViewNameInfoCxCHistoricoCliente & ".FechaDocumento", valFechaInicial, valFechaFinal)
   SQL = SQL & sqlCliente
'*************************************************
   SQL = SQL & " UNION SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoReporte, "
   SQL = SQL & "Codigo, "
   SQL = SQL & "Nombre, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & "Moneda, "
   SQL = SQL & " TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & SaldoInicialConAbonos & " As SaldoInicialConAbonos, "
   SQL = SQL & "FechaDocumento, "
   SQL = SQL & "FechaVencimiento, "
   SQL = SQL & "TipoDeDocumento, "
   SQL = SQL & "NumeroDocumento, "
   SQL = SQL & "NumeroDocumentoGrupo, "
   SQL = SQL & "0 AS MontoOriginal, 0 AS SaldoActual, "
   SQL = SQL & " TipoDocumentoDetalle, "
   SQL = SQL & "NumeroCobranza, "
   SQL = SQL & "FechaCobranza, "
   SQL = SQL & sqlMontoCobrado & " AS MontoCobrado, "
   SQL = SQL & "StatusCobranza "
   SQL = SQL & " FROM " & insCxCVista.GetViewNameCxCHistoricoClientes
   SQL = SQL & " WHERE " & insCxCVista.GetViewNameCxCHistoricoClientes & ".ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(insCxCVista.GetViewNameCxCHistoricoClientes & ".FechaDocumento", valFechaInicial, valFechaFinal)
   SQL = SQL & sqlCliente
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLHistoricoDeClienteCxCNew = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLHistoricoDeClienteCxCNew", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLHistoricoDeClienteAnticiposNew(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valcodigoCliente As String, _
               ByVal valUsarCambioBs As Boolean, ByVal mConsecutivoCompania As String, ByVal usarCambioOriginal As Boolean, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String 'fSQLDelReporteHistoricoDeCliente
   Dim SQL As String
   Dim sqlMontoOriginal As String
   Dim SqlSaldoInicial As String
   Dim sqlSaldoActual As String
   Dim sqlMontoAplicado As String
   Dim SqlSaldoInicialConAbonos As String
   Dim sqlMonedaReporte As String
   Dim insAnticipoVista As clsAnticipoVista
   Dim sqlStatusCobranzaAnulada  As String
   Dim sqlCambioOriginal As String
   Dim sqlCambioAplicado As String
   Dim sqlCambioActual As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set insAnticipoVista = New clsAnticipoVista
   sqlMontoOriginal = "MontoOriginal"
   sqlSaldoActual = "SaldoActual"
   'sqlMontoAplicado = "MontoAplicado"
   SqlSaldoInicial = fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista(valFechaInicial, valUsarCambioBs, False, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SqlSaldoInicialConAbonos = fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista(valFechaInicial, valUsarCambioBs, True, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)   ' prueba
   sqlStatusCobranzaAnulada = "StatusCobranza  = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA)
   sqlMontoAplicado = "MontoCobrado"
   sqlMonedaReporte = "MonedaAnticipo"
   If Not valUsarCambioBs Then
      sqlMontoAplicado = "(MontoCobrado/CambioCobrado)"
   End If
   sqlMontoAplicado = gUtilSQL.getIIF(sqlStatusCobranzaAnulada, "0", sqlMontoAplicado, True)
   If valUsarCambioBs Then
      If usarCambioOriginal Then
         sqlCambioOriginal = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "Cambio", "FechaDocumento"), 3)
         sqlCambioActual = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "Cambio", "FechaDocumento"), 3)
         sqlCambioAplicado = gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplicaMasDecimales(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "Cambio", " FechaCobranza"), 3)
      Else
         sqlMontoOriginal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "MonedaAnticipo", 1, usarCambioOriginal, "")
         sqlSaldoActual = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "MonedaAnticipo", 1, usarCambioOriginal, "")
         sqlMontoAplicado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("", "MonedaAnticipo", 1, usarCambioOriginal, "")
      End If
      sqlMontoOriginal = gUtilSQL.getIIF("(MonedaAnticipo = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                                "(MonedaAnticipo = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                                gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "MontoOriginal", "FechaDocumento") _
                                , gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioOriginal, "MonedaAnticipo", "MontoOriginal", usarCambioOriginal, ""), True)
                               ' , gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(gUtilSQL.getIIF("CambioCobrado IS NULL OR CambioCobrado = 0", sqlCambioOriginal, "CambioCobrado", True), "MonedaAnticipo", "MontoOriginal", usarCambioOriginal, ""), True)

      sqlSaldoActual = gUtilSQL.getIIF("(MonedaAnticipo = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                                   "(MonedaAnticipo = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                                   gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), "SaldoActual", "FechaDocumento") _
                                   , gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioActual, "MonedaAnticipo", "SaldoActual", usarCambioOriginal, ""), True)
      
    sqlMontoAplicado = gUtilSQL.getIIF(sqlMontoAplicado & " Is Null Or Not" & sqlMontoAplicado & " > 0 ", 0, gUtilSQL.getIIF("(MonedaAnticipo = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & ") OR " & _
                         "(MonedaAnticipo = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt) & ")", _
                         gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMontoAplicado, " FechaCobranza") _
                         , gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioAplicado, "MonedaAnticipo", sqlMontoAplicado, usarCambioOriginal, ""), True))
    
    
    sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda)
   End If
  
  
   SQL = " SELECT "
   SQL = SQL & "TipoReporte, "
   SQL = SQL & "Codigo, "
   SQL = SQL & "Nombre, "
   SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
   SQL = SQL & "MonedaReporte AS Moneda, "
   SQL = SQL & "TituloTipoReporte, "
   SQL = SQL & SqlSaldoInicial & " AS SaldoInicial, "
   SQL = SQL & SqlSaldoInicialConAbonos & " As SaldoInicialConAbonos, "
   SQL = SQL & "FechaDocumento, "
   SQL = SQL & " '' AS FechaVencimiento, "
   SQL = SQL & "TipoDeDocumento, "
   SQL = SQL & "NumeroDocumento, "
   SQL = SQL & "NumeroDocumentoGrupo, "
   SQL = SQL & sqlMontoOriginal & " AS MontoOriginal, "
   SQL = SQL & sqlSaldoActual & " AS SaldoActual, "
   SQL = SQL & "TipoDocumentoDetalle, "
   SQL = SQL & "NumeroCobranza, "
   SQL = SQL & "FechaCobranza, "
   SQL = SQL & sqlMontoAplicado & " AS MontoCobrado, "
   SQL = SQL & "StatusCobranza "
   SQL = SQL & " FROM " & insAnticipoVista.GetViewNameAnticipoHistoricoCliente
   SQL = SQL & " WHERE ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("FechaDocumento", valFechaInicial, valFechaFinal)
   If valcodigoCliente <> "" Then
      SQL = SQL & " AND Codigo" & " = " & gUtilSQL.fSimpleSqlValue(valcodigoCliente)
   End If
    Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLHistoricoDeClienteAnticiposNew = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLHistoricoDeClienteAnticiposNew", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxCHistoricoCxCVista(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, _
         ByVal valDescontarAbonos As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlVistaName As String
   Dim sqlMonto As String
   Dim insCxCVista As clsCxCVista
   On Error GoTo h_ERROR
   Set insCxCVista = New clsCxCVista
   sqlVistaName = insCxCVista.GetViewNameSaldoInicialCxCHistoricoCliente
   sqlMonto = sqlVistaName & ".SaldoInicial"
   If valDescontarAbonos Then
      sqlMonto = sqlVistaName & ".SaldoInicialConAbonos"
   End If
   If valUsarCambio Then
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlVistaName & ".CambioABolivares", _
                  sqlVistaName & ".Moneda", sqlMonto, True, "")
      sqlMonto = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMonto, sqlVistaName & ".fecha")
   End If
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM " & sqlVistaName
   SQL = SQL & " WHERE " & sqlVistaName & ".ConsecutivoCompania = " & mConsecutivoCompania
   SQL = SQL & " AND " & sqlVistaName & ".CodigoCliente = Codigo"
   SQL = SQL & " AND " & sqlVistaName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoCxCVista = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxCHistoricoCxCVista", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, ByVal valDescontarAbonos As Boolean, _
                     ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim SQL As String
   Dim sqlAnticipoTableName As String
   Dim insAnticipoVista As clsAnticipoVista
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   Set insAnticipoVista = New clsAnticipoVista
   sqlAnticipoTableName = insAnticipoVista.GetViewNameSaldoInicialAnticipoHistoricoCliente
   sqlMonto = insAnticipoVista.GetViewNameSaldoInicialAnticipoHistoricoCliente & ".SaldoInicial"
   If valDescontarAbonos Then
      sqlMonto = insAnticipoVista.GetViewNameSaldoInicialAnticipoHistoricoCliente & ".SaldoInicialConAbonos"
   End If
   If valUsarCambio Then
      sqlMonto = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Cambio", _
               "Moneda", sqlMonto, True, "")
   End If
   SQL = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   SQL = SQL & " FROM " & sqlAnticipoTableName
   SQL = SQL & " WHERE " & sqlAnticipoTableName & "." & "ConsecutivoCompania" & " = " & mConsecutivoCompania
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "CodigoCliente = " & insAnticipoVista.GetViewNameAnticipoHistoricoCliente & ".Codigo"
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "Moneda = " & insAnticipoVista.GetViewNameAnticipoHistoricoCliente & ".MonedaAnticipo"
   SQL = SQL & " AND " & sqlAnticipoTableName & "." & "Fecha" & " < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCxCconDescripcion(ByVal valZonaCobranza As String, ByVal valAnalisisDeVencimientoPor As String, ByVal valMonedaDeLosReportes As String, ByVal valConsecutivoCompania As Long, ByVal valHoyNombreMoneda As String, ByRef gUltimaTasaDeCambio As Object, ByVal usarContacto As Boolean) As String
   Dim SQL As String
   Dim valMontoExento As String
   Dim valMontoGravado As String
   Dim valMontoIva As String
   Dim valMontoAbonado As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   SQL = "SELECT "
   If (usarContacto) Then
      SQL = SQL & " Cliente.Contacto,"
   End If
   SQL = SQL & "CxC.Numero, "
   SQL = SQL & "CxC.Descripcion, "
   SQL = SQL & "(SELECT " & "Moneda.simbolo" & _
               " FROM " & "Moneda WHERE " & "CxC.Moneda" & _
               " = " & "Moneda.Nombre) AS SimboloDeLaMoneda, "
   SQL = SQL & "CxC.FechaVencimiento, "
   SQL = SQL & "CxC.CodigoCliente, "
   If valMonedaDeLosReportes = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, valHoyNombreMoneda) Then
      valMontoExento = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CxC." & _
               "CambioABolivares", "CxC." & _
               "Moneda", "CxC.MontoExento", False, "")
      valMontoGravado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CxC." & _
                     "CambioABolivares", "CxC." & _
                     "Moneda", "CxC.MontoGravado", False, "")
      valMontoIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CxC." & _
                     "CambioABolivares", "CxC." & _
                     "Moneda", "CxC.MontoIVA", False, "")
      valMontoAbonado = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CxC." & _
                     "CambioABolivares", "CxC." & _
                     "Moneda", "CxC.MontoAbonado", False, "")
      SQL = SQL & "(((" & valMontoExento & _
               " + " & valMontoGravado & _
               " + " & valMontoIva & _
               ") - " & valMontoAbonado & ")"
   Else
      SQL = SQL & "(((" & "CxC.MontoExento" & _
               " + " & "CxC.MontoGravado" & _
               " + " & "CxC.MontoIVA" & _
               ") - " & "CxC.MontoAbonado)"
   End If
   SQL = SQL & ") AS Monto, "
   SQL = SQL & "cliente.Nombre AS NombreCliente, "
   SQL = SQL & "cliente.Telefono, "
   SQL = SQL & "cliente.Direccion, "
   SQL = SQL & "cliente.ZonaDeCobranza AS ZonaCobranza, "
   SQL = SQL & "CxC.Moneda"
   SQL = SQL & " FROM cliente INNER JOIN " & "CxC ON ("
   SQL = SQL & "cliente.Codigo" & _
               " = CxC.CodigoCliente) AND ("
   SQL = SQL & "cliente.ConsecutivoCompania" & _
               " = " & "CxC.ConsecutivoCompania)"
   SQL = SQL & " WHERE CxC.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND CxC.Status IN ('" & _
               gConvert.enumerativoAChar(enum_StatusDocumento.eSD_PORCANCELAR) & "','" & _
               gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ABONADO) & "')"
   If valZonaCobranza <> "TODAS" Then
      SQL = SQL & " AND cliente.ZonaDeCobranza" & _
                  " ='" & valZonaCobranza & "'"
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "CxC.Moneda, "
   SQL = SQL & "cliente.ZonaDeCobranza,"
   If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Codigo) Then
      SQL = SQL & "CxC.CodigoCliente, "
   Else
      SQL = SQL & "cliente.Nombre, "
   End If
   SQL = SQL & "CxC.FechaVencimiento"
   fConstruirSQLDelReporteCxCconDescripcion = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCxCconDescripcion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteAnalisisDeVencimiento(ByVal valcodigoVendedor As String, ByVal valCantidadAImprimir As String, ByVal valZonaCobranza As String, ByVal valAnalisisDeVencimientoPor As String, ByVal valConsecutivoCompania As String, ByVal valMonedaDeLosReportes As String, ByVal valNombreMonedaLocal As String, ByRef InsAdmPropAnalisisVenc As Object, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim sqlMtoRestante As String
   Dim sqlDateDiff As String
   Dim sqlMtoTotal As String
   Dim sqlMonedaReporte As String
   Dim sqlCambio As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   SQL = ""
   If InsAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual Then
      sqlMtoRestante = "((" & "cxC.MontoExento" & _
                        " + " & "cxC.MontoGravado" & _
                        " + " & "cxC.MontoIVA" & _
                        ") - " & "cxC.MontoAbonado)"
      sqlMtoTotal = "(" & "cxC.MontoExento" & _
                        " + " & "cxC.MontoGravado" & _
                        " + " & "cxC.MontoIVA)"
      sqlCambio = "cxC.CambioABolivares"
      sqlMonedaReporte = "cxC.Moneda"
      If valMonedaDeLosReportes = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, valNombreMonedaLocal) Then
         sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoTotal, True, "")
         sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMonedaReporte, sqlMtoRestante, True, "")
         sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoRestante, "cxC.Fecha")
         sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, sqlMtoTotal, "cxC.Fecha")
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal)
      End If
      sqlDateDiff = gUtilSQL.getDateDiff("d", "cxC.FechaVencimiento", gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
      SQL = "SELECT "
      SQL = SQL & "cliente.ZonaDeCobranza, "
      SQL = SQL & "cxC.CodigoCliente, "
      SQL = SQL & "cliente.Nombre, "
      SQL = SQL & "cxC.Moneda, "
      SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      SQL = SQL & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AS Vto1, "
      SQL = SQL & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AS Vto2, "
      SQL = SQL & InsAdmPropAnalisisVenc.GetTercerVencimiento & " AS Vto3, "
      SQL = SQL & sqlDateDiff & " AS DiasVencidos, "
      SQL = SQL & sqlMtoRestante & " AS MtoRestante, "
      SQL = SQL & "cxC.FechaVencimiento, "
      SQL = SQL & "cxC.Numero, "
      SQL = SQL & "cxC.Fecha, "
      SQL = SQL & sqlMtoTotal & " AS MtoTotal"
      SQL = SQL & " FROM " & "cxC INNER JOIN "
      SQL = SQL & "cliente ON "
      SQL = SQL & "cxC.ConsecutivoCompania" & _
                  " = " & "cliente.ConsecutivoCompania"
      SQL = SQL & " AND "
      SQL = SQL & "cxC.CodigoCliente" & _
                  " = " & "cliente.Codigo "
      SQL = SQL & " WHERE "
      SQL = SQL & "cxC.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND (" & "cxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & _
                  " OR " & "cxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      If valCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And _
         Len(valcodigoVendedor) > 0 Then
         SQL = SQL & " AND " & "cxC.CodigoVendedor" & _
                     " = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
      End If
      If valZonaCobranza <> "TODAS" Then
         SQL = SQL & " AND " & "cliente.ZonaDeCobranza" & _
                     " = " & gUtilSQL.fSimpleSqlValue(valZonaCobranza)
      End If
      SQL = SQL & " ORDER BY "
      SQL = SQL & "MonedaReporte, "
      SQL = SQL & "cliente.ZonaDeCobranza, "
      If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Nombre) Then
         SQL = SQL & "cliente.Nombre, "
      Else
         SQL = SQL & "cxC.CodigoCliente, "
      End If
      SQL = SQL & "cxC.FechaVencimiento"
   End If
   fConstruirSQLDelReporteAnalisisDeVencimiento = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturasAnuladasVsCxCVigentes(ByVal valConsecutivoCompania As Long, ByRef insFactura As Object, ByRef gEnumProyecto As Object)
   Dim SQL As String
   On Error GoTo h_ERROR
   insFactura.setClaseDeTrabajo eCTFC_Factura
   SQL = "SELECT "
   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " AS NumeroFact, "
   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_FECHA & " AS FechaFact, "
   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & ", "
   SQL = SQL & gUtilSQL.getIIF(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA _
               & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
               gUtilSQL.getIIF(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA _
               & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_BORRADOR)), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_EMITIDA)), True), True) & _
               " AS StatusFactura, "
   SQL = SQL & "cxC.Numero AS NumeroCxC, "
   SQL = SQL & "(" & "cxC.MontoExento" & _
               " + " & "cxC.MontoGravado" & _
               " + " & "cxC.MontoIVA) AS TotalCxC, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxC.Status", enum_StatusDocumento.eSD_PORCANCELAR, _
               gEnumProyecto.fEnumStatusCxCToStringInArray(True, False), "StatusCxC")
   SQL = SQL & " FROM " & "cxC INNER JOIN " & insFactura.GetTableName
   SQL = SQL & " ON (" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
               " = " & "cxC.NumeroDocumentoOrigen"
   SQL = SQL & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & "cxC.ConsecutivoCompania"
   SQL = SQL & ") AND (" & "cxC.TipoCxC" & _
               " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & ")"
   SQL = SQL & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   SQL = SQL & " AND " & "cxC.Status" & _
               " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   SQL = SQL & " ORDER BY "
   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_FECHA & ", "
   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
h_EXIT:
   fConstruirSQLDelReporteFacturasAnuladasVsCxCVigentes = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteFacturasAnuladasVsCxCVigentes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelAnalisisDeCxCAUnaFecha(ByVal valTableName As String, ByVal valnombreParaTabla As String, _
                                 ByVal valFecha As Date, ByVal valOrdenadoPorNombre As Boolean, ByVal valAgruparPorTipo As Boolean, ByVal valAgruparTodos As Boolean, ByVal valConsecutivoCompania As Long, ByVal valTipoDeDocumento As enum_TipoDeTransaccion, ByRef InsAdmPropAnalisisVenc As Object, ByRef gEnumProyecto As Object) As String
   Dim SQL As String
   Dim sqlMtoRestante As String
   Dim sqlDateDiff As String
   Dim sqlMtoTotal As String
   Dim tasaOrig As Boolean
   On Error GoTo h_ERROR
   SQL = ""
   If InsAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual Then
      sqlMtoRestante = "((" & valTableName & ".MontoExento" & _
                        " + " & valTableName & ".MontoGravado" & _
                        " + " & valTableName & ".MontoIva" & _
                        ") - " & valTableName & ".MontoAbonado)"
      sqlMtoTotal = "(" & valTableName & ".MontoExento" & _
                        " + " & valTableName & ".MontoGravado" & _
                        " + " & valTableName & ".MontoIva)"
      sqlDateDiff = gUtilSQL.getDateDiff("d", valTableName & ".FechaVencimiento", _
                     gUtilSQL.fDateToSQLValue(valFecha))
      SQL = "SELECT "
      SQL = SQL & "cliente.ZonaDeCobranza, "
      SQL = SQL & valTableName & ".CodigoCliProv AS CodigoCliente, "
      SQL = SQL & "cliente.Nombre, "
      SQL = SQL & valTableName & ".Moneda, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & _
                  " AS MtoNoVencido, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & _
                  sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto1Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & _
                  sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto2Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & _
                  sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto3Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, _
                  sqlMtoRestante, "0", True) & _
                  " AS Mto4Vto, "
      SQL = SQL & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AS Vto1, "
      SQL = SQL & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AS Vto2, "
      SQL = SQL & InsAdmPropAnalisisVenc.GetTercerVencimiento & " AS Vto3, "
      SQL = SQL & sqlDateDiff & " AS DiasVencidos, "
      SQL = SQL & sqlMtoRestante & " AS MtoRestante, "
      SQL = SQL & valTableName & ".FechaVencimiento, "
      SQL = SQL & valTableName & ".Numero, "
      SQL = SQL & valTableName & ".Fecha, "
      SQL = SQL & sqlMtoTotal & " AS MtoTotal, "
'      SQL = SQL & valTableName & ".Tipo "
       SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum(valTableName & ".Tipo ", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "Tipo")
      SQL = SQL & " FROM " & valTableName & " INNER JOIN "
      SQL = SQL & "cliente ON "
      SQL = SQL & valTableName & ".ConsecutivoCompania" & _
                  " = " & "cliente.ConsecutivoCompania"
      SQL = SQL & " AND "
      SQL = SQL & valTableName & ".CodigoCliProv" & _
                  " = " & "cliente.Codigo"
      SQL = SQL & " WHERE " & valTableName & ".ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      SQL = SQL & " AND " & valTableName & ".Status" & _
                  " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & _
                            gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      SQL = SQL & " AND " & valTableName & ".Idusuario" & _
                  " = " & gUtilSQL.fSimpleSqlValue(valnombreParaTabla)
      SQL = SQL & " AND " & valTableName & ".Idmodulo = " & gUtilSQL.fSimpleSqlValue(CM_MODULO_CxC)
      SQL = SQL & " AND " & valTableName & ".Fecha <= " & gUtilSQL.fDateToSQLValue(valFecha)
      If valAgruparPorTipo Then
         If valAgruparTodos Then
            SQL = SQL & " AND " & valTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(valTipoDeDocumento)
         End If
      End If
      SQL = SQL & " ORDER BY "
      SQL = SQL & valTableName & ".Moneda, "
      SQL = SQL & valTableName & ".Tipo, "
      SQL = SQL & "cliente.ZonaDeCobranza, "
      If valOrdenadoPorNombre Then
         SQL = SQL & "cliente.Nombre, "
      Else
         SQL = SQL & valTableName & ".CodigoCliProv, "
      End If
      SQL = SQL & valTableName & ".FechaVencimiento"
   End If
h_EXIT: On Error GoTo 0
   fSQLDelAnalisisDeCxCAUnaFecha = SQL
   Exit Function
h_ERROR: SQL = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteAnalisisDeVencimientoEntreFechas(ByVal valAgruparPorVendedor As Boolean, ByVal valAnalisisDeVencimientoPor As String, ByVal valMonedaDeLosReportes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreMonedaLocal As String, ByVal valConsecutivoCompania As Long, ByRef InsAdmPropAnalisisVenc As Object, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object, ByVal usarFiltroZonaCobranza As Boolean, ByVal FiltroPorTipo As String) As String
   Dim SQL As String
   Dim sqlMtoRestante As String
   Dim sqlDateDiff As String
   Dim sqlMtoTotal As String
   Dim sqlCambio As String
   Dim sqlMoneda As String
   Dim sqlFecha As String
   Dim sqlMonedaReporte  As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   If InsAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual Then
      sqlMtoTotal = "(" & "CxC.MontoExento" & _
                        " + " & "CxC.MontoGravado" & _
                        " + " & "CxC.MontoIVA)"
      sqlMtoRestante = "(" & sqlMtoTotal & " - " & "CxC.MontoAbonado)"
      sqlDateDiff = gUtilSQL.getDateDiff("d", "CxC.FechaVencimiento", gUtilSQL.fDateToSQLValue(gUtilDate.getFechaDeHoy))
      sqlCambio = "CxC.CambioABolivares"
      sqlMoneda = "CxC.Moneda"
      sqlFecha = "CxC.Fecha"
      sqlMonedaReporte = "CxC.Moneda"
      If valMonedaDeLosReportes = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, valNombreMonedaLocal) Then
         sqlMtoRestante = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMtoRestante, True, "")
         sqlMtoTotal = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, sqlMoneda, sqlMtoTotal, True, "")
         sqlMtoTotal = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoTotal, "cxC.fecha")
         sqlMtoRestante = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), sqlMtoRestante, "cxC.fecha")
         sqlMonedaReporte = gUtilSQL.fSimpleSqlValue(valNombreMonedaLocal)
      End If
      SQL = "SELECT "
      If usarFiltroZonaCobranza Then
         SQL = SQL & " Cliente.ZonaDeCobranza, "
      End If
      SQL = SQL & sqlMoneda & ", "
      SQL = SQL & sqlMonedaReporte & " AS MonedaReporte, "
      SQL = SQL & gUtilSQL.fMonth(sqlFecha, "") & " AS Mes, "
      SQL = SQL & gUtilSQL.fYear(sqlFecha, "") & " AS Ano, "
      SQL = SQL & "CxC.CodigoCliente, "
      SQL = SQL & "cliente.Nombre AS NombreCliente, "
      SQL = SQL & "CxC.Numero, "
      SQL = SQL & sqlFecha & ", "
      SQL = SQL & "CxC.FechaVencimiento, "
      SQL = SQL & sqlDateDiff & " AS DiasVencidos, "
      SQL = SQL & "CxC.CodigoVendedor, "
      SQL = SQL & "vendedor.Nombre AS NombreVendedor, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      SQL = SQL & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      SQL = SQL & sqlMtoRestante & " AS MtoTotal"
      SQL = SQL & " FROM " & "vendedor INNER JOIN ("
      SQL = SQL & "cliente INNER JOIN " & "CxC ON ("
      SQL = SQL & "cliente.Codigo = " & "CxC.CodigoCliente"
      SQL = SQL & ") AND (" & "cliente.ConsecutivoCompania = " & "CxC.ConsecutivoCompania"
      SQL = SQL & ")) ON (" & "vendedor.Codigo = " & "CxC.CodigoVendedor"
      SQL = SQL & ") AND (" & "vendedor.ConsecutivoCompania = " & "CxC.ConsecutivoCompania)"
      SQL = SQL & " WHERE " & "CxC.ConsecutivoCompania = " & valConsecutivoCompania
      SQL = SQL & " AND (" & "CxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & _
                  " OR " & "CxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
   If usarFiltroZonaCobranza Then
      If FiltroPorTipo <> "TODAS" Then
         SQL = SQL & "AND Cliente.ZonaDeCobranza='" & FiltroPorTipo & "' "
      End If
   End If
   
      SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", valFechaInicial, valFechaFinal)
      SQL = SQL & " ORDER BY "
      If usarFiltroZonaCobranza Then
         SQL = SQL & " Cliente.ZonaDeCobranza, "
      End If
     
      If valAgruparPorVendedor = True Then
         SQL = SQL & "vendedor.Codigo, "
      End If
      SQL = SQL & sqlMoneda & ", "
      SQL = SQL & sqlFecha & ", "
      If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Nombre) Then
         SQL = SQL & "cliente.Nombre, "
      Else
         SQL = SQL & "CxC.CodigoCliente, "
      End If
      SQL = SQL & "CxC.FechaVencimiento"
   End If
   fSQLDelReporteAnalisisDeVencimientoEntreFechas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLNroComprobanteFiscalEnCXC(ByVal valPorCompania As Boolean, Optional ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vSQLWhere  As String
   On Error GoTo h_ERROR
   
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vSQLWhere = "(" & vSQLWhere & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & ")"
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "factura.STATUSFACTURA", enum_StatusFactura.eSF_EMITIDA)
   
   If (valPorCompania) Then
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania)
   End If
   
  vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
 
  vSQL = " UPDATE CxC "
  vSQL = vSQL & " SET "
  vSQL = vSQL & " CxC.NumeroComprobanteFiscal = factura.NumeroComprobanteFiscal"
  vSQL = vSQL & " FROM factura"
  vSQL = vSQL & " INNER JOIN  CxC ON"
  vSQL = vSQL & " factura.ConsecutivoCompania =  CxC.ConsecutivoCompania AND "
  vSQL = vSQL & " factura.Numero = CxC.NumeroDocumentoOrigen AND "
  vSQL = vSQL & ftipoCXC() & " = Cxc.TipoCxC "
  vSQL = vSQL & vSQLWhere
  fSQLNroComprobanteFiscalEnCXC = vSQL
  
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLNroComprobanteFiscalEnCXC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLNroDeControlEnCXCAsociada(ByVal valPorCompania As Boolean, Optional ByVal valConsecutivoCompania As Long) As String
   Dim vSQL As String
   Dim vSQLWhere  As String
   On Error GoTo h_ERROR
   
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA)
   vSQLWhere = "(" & vSQLWhere & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vSQLWhere = vSQLWhere & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & ")"
   vSQLWhere = gUtilSQL.fSQLEnumValueWithAnd(vSQLWhere, "factura.STATUSFACTURA", enum_StatusFactura.eSF_EMITIDA)
   
   If (valPorCompania) Then
    vSQLWhere = gUtilSQL.fSQLNumberValueWithAnd(vSQLWhere, "factura.ConsecutivoCompania", valConsecutivoCompania)
   End If
   
  vSQLWhere = gUtilSQL.getWhereSQL(vSQLWhere)
 
  vSQL = " UPDATE CxC "
  vSQL = vSQL & " SET "
  vSQL = vSQL & " CxC.NumeroControl = factura.NumeroControl "
  vSQL = vSQL & " FROM factura"
  vSQL = vSQL & " INNER JOIN  CxC ON"
  vSQL = vSQL & " factura.ConsecutivoCompania =  CxC.ConsecutivoCompania AND "
  vSQL = vSQL & " factura.Numero = CxC.NumeroDocumentoOrigen AND "
  vSQL = vSQL & ftipoCXC() & " = Cxc.TipoCxC "
  vSQL = vSQL & vSQLWhere
 fSQLNroDeControlEnCXCAsociada = vSQL
  
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLNroDeControlEnCXCAsociada", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function ftipoCXC() As String
 Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   
   vCadenaResultadoStr = gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA)
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO)
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO)
   vSQL = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "")
   Set gEnumProyecto = Nothing
   ftipoCXC = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ftipoCXC()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDeComprobanteCxC(ByVal valNumero As String, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim sqlTotal As String
   Dim sqlMontoRestante As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = " SELECT "
   SQL = SQL & "cxC" & ".Numero AS Numero, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxC" & ".TipoCxc", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True), "TipoCxC") & ","
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxC" & ".Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True), "Status") & ","
   SQL = SQL & "cxC" & ".Moneda AS Moneda, "
   SQL = SQL & "cxC" & ".CambioAbolivares AS CambioABolivares, "
   SQL = SQL & "cliente" & ".Codigo As CodigoCliente, "
   SQL = SQL & "cliente" & ".Nombre As NombreCliente, "
   SQL = SQL & "vendedor" & ".Codigo As CodigoVendedor, "
   SQL = SQL & "vendedor" & ".Nombre As NombreVendedor, "
   SQL = SQL & "cxC" & ".Fecha AS Fecha, "
   SQL = SQL & "cxC" & ".FechaVencimiento AS FechaVencimiento, "
   SQL = SQL & "cxC" & ".Descripcion AS Descripcion, "
   SQL = SQL & "cxC" & ".CentroDeCostos AS CentroDeCostos, "
   SQL = SQL & "cxC" & ".MontoExento AS MontoExento,"
   SQL = SQL & "cxC" & ".MontoGravado AS MontoGravado,"
   SQL = SQL & "cxC" & ".MontoIva AS MontoIVA,"
   SQL = SQL & "cxC" & ".Numero AS NumeroDocumento, "
   sqlTotal = "(" & "cxC" & ".MontoExento + " & "cxC" & ".MontoGravado + " & "cxC" & ".MontoIva)"
   SQL = SQL & sqlTotal & " AS MontoTotal, "
   SQL = SQL & "cxC" & ".MontoAbonado AS MontoAbonado,"
   sqlMontoRestante = sqlTotal & "-" & "cxC" & ".MontoAbonado"
   SQL = SQL & sqlMontoRestante & " AS RestaPorPagar, "
   SQL = SQL & "cxC" & ".CodigoMoneda AS CodigoMoneda"
   SQL = SQL & " FROM  " & "vendedor" & " INNER JOIN "
   SQL = SQL & "cliente" & " INNER JOIN " & "cxC"
   SQL = SQL & " ON " & "cliente" & ".Codigo = " & "cxC" & ".CodigoCliente "
   SQL = SQL & " AND " & "cliente" & ".ConsecutivoCompania = " & "cxC" & ".ConsecutivoCompania "
   SQL = SQL & " ON " & "vendedor" & ".Codigo = " & "cxC" & ".CodigoVendedor "
   SQL = SQL & " AND " & "vendedor" & ".ConsecutivoCompania = " & "cxC" & ".ConsecutivoCompania "
   SQL = SQL & " WHERE " & "cxC" & ".ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   SQL = SQL & " AND Numero = " & gUtilSQL.fSimpleSqlValue(valNumero)
   Set gEnumProyecto = Nothing

h_EXIT:   On Error GoTo 0
   fConstruirSQLDeComprobanteCxC = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDeComprobanteCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDeCxCConComprobante(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal valComprobanteNavGetTableName As String, ByVal valNoDocOrigenContabilizacion As String, Optional ByVal valConGeneradoPor As Boolean = True) As String
   Dim SQL As String
   Dim GetTableName As String
   On Error GoTo h_ERROR
   GetTableName = "cxC"
   SQL = " SELECT "
   SQL = SQL & GetTableName & ".Fecha AS FechaDocumento, "
   SQL = SQL & GetTableName & ".Descripcion AS Descripcion, "
   SQL = SQL & GetTableName & ".TipoCxc AS Tipo, "
   SQL = SQL & GetTableName & ".Status AS Status, "
   SQL = SQL & GetTableName & ".Numero AS NumeroDocumento, "
   SQL = SQL & valComprobanteNavGetTableName & "." & "GeneradoPor"
   SQL = SQL & " AS TipoDeDocumento, "
   SQL = SQL & valComprobanteNavGetTableName & "." & "Fecha"
   SQL = SQL & " AS FechaCombrobante, "
   SQL = SQL & valComprobanteNavGetTableName & "." & "TotalHaber"
   SQL = SQL & " AS TotalHaber, "
   SQL = SQL & valComprobanteNavGetTableName & "." & "NUMERO"
   SQL = SQL & " AS NumeroComprobante, "
   SQL = SQL & valComprobanteNavGetTableName & "." & "DESCRIPCION"
   SQL = SQL & " AS DescripcionComprobante, "
   SQL = SQL & valComprobanteNavGetTableName & "." & "NoDocumentoOrigen"
   SQL = SQL & " AS NumeroDocumentoOrigen, "
   SQL = SQL & "(" & GetTableName & ".MontoExento + "
   SQL = SQL & GetTableName & ".MontoGravado + "
   SQL = SQL & GetTableName & ".MontoIva) "
   SQL = SQL & " AS TotalDocumento "
   SQL = SQL & " FROM " & GetTableName & ", " & valComprobanteNavGetTableName
   SQL = SQL & " WHERE (" & valNoDocOrigenContabilizacion & " = "
   SQL = SQL & valComprobanteNavGetTableName & "." & "NoDocumentoOrigen"
   SQL = SQL & ") AND " & GetTableName & ".ConsecutivoCompania = "
   SQL = SQL & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   SQL = SQL & " AND " & GetTableName & ".Origen = '" & gConvert.enumerativoAChar(enum_OrigenFacturacionOManual.eOF_MANUAL) & "'"
   If valConGeneradoPor Then
      SQL = SQL & " AND " & valComprobanteNavGetTableName & "." & "GeneradoPor" & " ='"
      SQL = SQL & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXC) & "'"
      SQL = SQL & " AND " & GetTableName & ".Fecha"
      SQL = SQL & " BETWEEN "
      SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
      SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaFinal)
   Else
      SQL = SQL & " AND " & valComprobanteNavGetTableName & "." & "GeneradoPor" & " <>'"
      SQL = SQL & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXC) & "'"
      SQL = SQL & " AND " & valComprobanteNavGetTableName & "." & "Fecha"
      SQL = SQL & " BETWEEN "
      SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
      SQL = SQL & gUtilSQL.fDateToSQLValue(valFechaFinal)
   End If
   fConstruirSQLDeCxCConComprobante = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConstruirSQLDeCxCConComprobante = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDeCxCConComprobante", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCxCPendientes(ByVal valConsecutivoCompania As Long, ByRef refCodigoCliente As String, ByVal valTraerTotales As Boolean, ByVal EnCualquierMoneda As Boolean, ByVal valFecha As Date) As String    'PRIVATE
   Dim SQL As String
   On Error GoTo h_ERROR
   If valTraerTotales Then
      SQL = "SELECT " & "cxC" & ".Numero, "
      SQL = SQL & "((" & "cxC" & ".MontoGravado" & " + " & "cxC" & ".MontoIva" & " + MontoExento" & ") - (" & "cxC" & ".MontoAbonado)) AS MontoRestante, "
      SQL = SQL & "((" & "cxC" & ".MontoGravado" & " + " & "cxC" & ".MontoIva" & ") + (" & "cxC" & ".MontoExento)) AS MontoOriginal, "
      SQL = SQL & "cxC" & ".Moneda, " & "cxC" & ".CodigoVendedor, "
      SQL = SQL & "cxC" & ".SeRetuvoIva," & " 0 AS MontoIvaRetenido, "
      SQL = SQL & "cxC" & ".TipoCxc, "
      SQL = SQL & "cxC" & ".CodigoMoneda,"
      SQL = SQL & "cxC" & ".CambioAbolivares, "
      SQL = SQL & "ISNULL( cxC" & ".FechaLimiteCambioAMonedaLocal, cxC.Fecha) AS  FechaLimiteCambioAMonedaLocal,"
      SQL = SQL & " CASE  WHEN  cxc.origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_FACTURA) & " THEN factura.TotalIVA "
      SQL = SQL & " ELSE cxC.MontoIva  "
      SQL = SQL & " END  AS MontoIva  "
      SQL = SQL & ", ISNULL( factura.Moneda, cxC.Moneda) AS MonedaFactura"
      SQL = SQL & " FROM " & "cxC"
      SQL = SQL & " LEFT JOIN factura ON cxC.NumeroDocumentoOrigen = factura.Numero "
      SQL = SQL & " AND cxC.CodigoCliente = factura.CodigoCliente "
        
      SQL = SQL & " AND cxc.TipoCxc = "
      SQL = SQL & " CASE WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_BOLETADEVENTA)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOASIGNADO) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOASIGNADO)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_BOLETA) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_BOLETADEVENTA)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITOCOMPROBANTEFISCAL)
      SQL = SQL & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTAENTREGA)
      SQL = SQL & " END "
      SQL = SQL & " AND cxC.ConsecutivoCompania = factura.ConsecutivoCompania "
      
      SQL = SQL & " WHERE "
      SQL = SQL & " cxc.ConsecutivoCompania" & " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      SQL = SQL & " AND cxc.CodigoCliente = " & gUtilSQL.fSimpleSqlValue(refCodigoCliente)
      SQL = SQL & " AND Status" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
      SQL = SQL & " AND (MontoAbonado" & ") <> (MontoExento" & " + MontoGravado" & " + MontoIva)"
      SQL = SQL & " ORDER BY Numero, TipoCxc"
   Else
      SQL = "SELECT "
      SQL = SQL & "Numero"
      SQL = SQL & " FROM " & "cxC" & " WHERE "
      SQL = SQL & "ConsecutivoCompania" & " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      SQL = SQL & " AND Status" & " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ")"
      SQL = SQL & " AND CodigoCliente" & " = " & gUtilSQL.fSimpleSqlValue(refCodigoCliente)
      SQL = SQL & " ORDER BY Numero, TipoCxc"
   End If
   fSQLSearchCxCPendientes = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchCxCPendientes = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLSearchCxCPendientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

