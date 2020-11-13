VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAnticipoVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsAnticipoVista"
Private Const CM_MESSAGE_NAME As String = "Vista Anticipo"
Private gEnumProyecto As clsEnumAdministrativo
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNameAnticipoHistoricoCliente() As String
   GetViewNameAnticipoHistoricoCliente = "IGV_AnticipoHistoricoCliente"
End Function
Public Function fSqlViewAnticipoHistoricoCliente() As String
   Dim SQL As String
   Dim sqlMontoNoDisponible As String
   Dim sqlSaldoActual As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlMontoNoDisponible = "(Anticipo.MontoUsado" & _
                        " + Anticipo.MontoDevuelto + Anticipo.MontoDiferenciaEnDevolucion " & ")"
   sqlSaldoActual = "(Anticipo.MontoTotal - " & sqlMontoNoDisponible & ")"
   SQL = " SELECT "
   SQL = SQL & "Anticipo.ConsecutivoCompania, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("1") & " AS TipoReporte, "
   SQL = SQL & "Anticipo.CodigoCliente" & " AS Codigo, "
   SQL = SQL & "Cliente.Nombre AS Nombre, "
   SQL = SQL & "Anticipo.Moneda AS MonedaAnticipo, "
   SQL = SQL & "Anticipo.Cambio, "
   SQL = SQL & "Cobranza.Moneda, "
   SQL = SQL & " Cobranza.CambioAbolivares, "
   SQL = SQL & "Anticipo.Moneda AS MonedaReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloTipoReporte, "
   SQL = SQL & "Anticipo.Fecha" & " AS FechaDocumento, "
   SQL = SQL & " '' AS FechaVencimiento, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumTipoDeAnticipoToString(eTDA_COBRADO)) & " AS TipoDeDocumento, "
   SQL = SQL & "Anticipo.Numero" & " AS NumeroDocumento, "
   SQL = SQL & gUtilSQL.fCast("Anticipo.ConsecutivoAnticipo", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & gUtilSQL.getIIF("AnticipoCobrado.MontoRestanteAlDia < Anticipo.MontoTotal", gUtilSQL.fNumToStrSQL("0.00"), "Anticipo.MontoTotal") & " AS MontoOriginal, "
   SQL = SQL & gUtilSQL.getIIF("AnticipoCobrado.MontoRestanteAlDia < Anticipo.MontoTotal", gUtilSQL.fNumToStrSQL("0.00"), sqlSaldoActual) & " AS SaldoActual, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cobro") & " AS TipoDocumentoDetalle, "
   SQL = SQL & " Cobranza.Numero AS NumeroCobranza, "
   SQL = SQL & " Cobranza.Fecha AS FechaCobranza, "
   SQL = SQL & gUtilSQL.getIIF("AnticipoCobrado.MontoAplicado IS NULL", "0", "AnticipoCobrado.MontoAplicado") & " AS MontoCobrado, "
   SQL = SQL & " AnticipoCobrado.Cambio AS CambioCobrado, "
   SQL = SQL & "Cobranza.StatusCobranza AS StatusCobranza "
   SQL = SQL & " FROM (Anticipo LEFT JOIN ( Cobranza"
   SQL = SQL & " RIGHT JOIN AnticipoCobrado ON ("
   SQL = SQL & " Cobranza.Numero = AnticipoCobrado.NumeroCobranza"
   SQL = SQL & ") AND (" & " Cobranza.ConsecutivoCompania = AnticipoCobrado.ConsecutivoCompania"
   SQL = SQL & ")) ON ( Anticipo.ConsecutivoAnticipo" & " = AnticipoCobrado.ConsecutivoAnticipoUsado"
   SQL = SQL & ") AND ( Anticipo.ConsecutivoCompania" & " = AnticipoCobrado.ConsecutivoCompania"
   SQL = SQL & ")) INNER JOIN  Cliente ON ("
   SQL = SQL & " Anticipo.CodigoCliente = Cliente.Codigo"
   SQL = SQL & ") AND (Anticipo.ConsecutivoCompania =  Cliente.ConsecutivoCompania) "
   SQL = SQL & " WHERE Anticipo.Status" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   SQL = SQL & " AND Anticipo.EsUnaDevolucion" & " = " & gUtilSQL.fBooleanToSqlValue(False)
   SQL = SQL & " AND Anticipo.Tipo" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   SQL = SQL & " AND Cobranza.StatusCobranza" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA)
   fSqlViewAnticipoHistoricoCliente = SQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewAnticipoHistoricoCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameSaldoInicialAnticipoHistoricoCliente() As String
   GetViewNameSaldoInicialAnticipoHistoricoCliente = "IGV_SaldoInicialAnticipoHistoricoCliente"
End Function

Public Function fSqlViewSaldoInicialAnticipoHistoricoCliente() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & " ConsecutivoCompania, "
   SQL = SQL & " CodigoCliente, "
   SQL = SQL & " ConsecutivoAnticipo, "
   SQL = SQL & " Fecha, "
   SQL = SQL & " Cambio, "
   SQL = SQL & " Moneda, "
   SQL = SQL & " CodigoMoneda, "
   SQL = SQL & " MontoTotal AS SaldoInicial, "
   SQL = SQL & "(MontoTotal - (MontoUsado + MontoDevuelto + MontoDiferenciaEnDevolucion)) AS SaldoInicialConAbonos "
   SQL = SQL & " FROM Anticipo "
   SQL = SQL & " WHERE Anticipo.Status" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   SQL = SQL & " AND " & "Anticipo.Tipo" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   fSqlViewSaldoInicialAnticipoHistoricoCliente = SQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlViewSaldoInicialAnticipoHistoricoCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameAnticiposCobradosAUnaFecha() As String
   GetViewNameAnticiposCobradosAUnaFecha = "IGV_AnticiposCobradosAUnaFecha"
End Function

Public Function GetViewNameAnticiposPagadosAUnaFecha() As String
   GetViewNameAnticiposPagadosAUnaFecha = "IGV_AnticiposPagadosAUnaFecha"
End Function

Public Function fSqlViewAnticiposCobradosAUnaFecha() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT DISTINCT "
   SQL = SQL & "Anticipo.ConsecutivoCompania, "
   SQL = SQL & "anticipo.Fecha, "
   SQL = SQL & "anticipo.Fecha AS FechaAnticipo, "
   SQL = SQL & "anticipo.Tipo, "
   SQL = SQL & "Anticipo.Numero, "
   SQL = SQL & "Anticipo.CodigoCliente, "
   SQL = SQL & "Cliente.Nombre AS Nombre, "
   SQL = SQL & "Anticipo.Moneda, "
   SQL = SQL & "Anticipo.Cambio, "
   SQL = SQL & " Anticipo.MontoTotal, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoUsado, "
   SQL = SQL & " anticipo.MontoDevuelto, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoDiferenciaEnDevolucion, "
   SQL = SQL & "anticipo.EsUnaDevolucion,  "
   SQL = SQL & "anticipo.CodigoMoneda, "
   SQL = SQL & " anticipo.Status, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue(0) & " AS NumeroCobranza, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoRestanteAlDia "
   SQL = SQL & "FROM Anticipo INNER JOIN Cliente ON "
   SQL = SQL & "anticipo.CodigoCliente = Cliente.Codigo "
   SQL = SQL & "AND anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania LEFT JOIN AnticipoCobrado ON "
   SQL = SQL & "anticipo.Numero = anticipoCobrado.NumeroAnticipo AND "
   SQL = SQL & "anticipo.ConsecutivoCompania = anticipoCobrado.ConsecutivoCompania "
   SQL = SQL & "WHERE anticipo.Tipo = " & enum_TipoDeAnticipo.eTDA_COBRADO
   SQL = SQL & " And EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(False)
   SQL = SQL & " UNION  "
   SQL = SQL & "SELECT "
   SQL = SQL & "Cobranza.ConsecutivoCompania, "
   SQL = SQL & "Cobranza.Fecha, "
   SQL = SQL & "anticipo_1.Fecha AS FechaAnticipo, "
   SQL = SQL & "anticipo_1.Tipo, "
   SQL = SQL & "anticipoCobrado_1.NumeroAnticipo, "
   SQL = SQL & "Cobranza.CodigoCliente, "
   SQL = SQL & "Cliente_1.Nombre AS Nombre, "
   SQL = SQL & "Moneda.Nombre as Moneda, "
   SQL = SQL & "anticipoCobrado_1.Cambio AS Cambio, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoTotal, "
   SQL = SQL & "anticipoCobrado_1.MontoAplicado AS MontoUsado, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoDevuelto, "
   SQL = SQL & "Anticipo_1.MontoDiferenciaEnDevolucion AS MontoDiferenciaEnDevolucion, "
   SQL = SQL & gUtilSQL.fBooleanToStrSQL(False) & " AS EsUnaDevolucion,  "
   SQL = SQL & "anticipoCobrado_1.CodigoMoneda, "
   SQL = SQL & "anticipo_1.Status, "
   SQL = SQL & "anticipoCobrado_1.NumeroCobranza AS NumeroCobranza, "
   SQL = SQL & "anticipoCobrado_1.MontoRestanteAlDia "
   SQL = SQL & "FROM Cobranza INNER JOIN anticipoCobrado AS anticipoCobrado_1 ON "
   SQL = SQL & "Cobranza.ConsecutivoCompania = anticipoCobrado_1.ConsecutivoCompania AND "
   SQL = SQL & "Cobranza.Numero = anticipoCobrado_1.NumeroCobranza "
   SQL = SQL & "AND Cobranza.StatusCobranza = " & gUtilSQL.fSimpleSqlValue(enum_StatusCobranza.eSC_VIGENTE)
   SQL = SQL & " INNER JOIN  "
   SQL = SQL & "anticipo AS anticipo_1 ON anticipo_1.ConsecutivoAnticipo = anticipoCobrado_1.ConsecutivoAnticipoUsado AND "
   SQL = SQL & "anticipo_1.ConsecutivoCompania = anticipoCobrado_1.ConsecutivoCompania INNER JOIN "
   SQL = SQL & "Cliente AS Cliente_1 ON anticipo_1.CodigoCliente = Cliente_1.Codigo "
   SQL = SQL & "AND anticipo_1.ConsecutivoCompania = Cliente_1.ConsecutivoCompania "
   SQL = SQL & "INNER JOIN Moneda ON "
   SQL = SQL & "anticipoCobrado_1.CodigoMoneda = Moneda.Codigo "
   SQL = SQL & "WHERE anticipo_1.Tipo = " & enum_TipoDeAnticipo.eTDA_COBRADO
   SQL = SQL & " And EsUnaDevolucion = " & gUtilSQL.fBooleanToStrSQL(False)
   fSqlViewAnticiposCobradosAUnaFecha = SQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewAnticiposCobradosAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewAnticiposPagadosAUnaFecha() As String
   Dim SQL As String
  On Error GoTo h_ERROR
   SQL = " SELECT DISTINCT "
   SQL = SQL & "Anticipo.ConsecutivoCompania, "
   SQL = SQL & "anticipo.Fecha, "
   SQL = SQL & "anticipo.Fecha AS FechaAnticipo, "
   SQL = SQL & "anticipo.Tipo, "
   SQL = SQL & "Anticipo.Numero, "
   SQL = SQL & "Anticipo.CodigoProveedor, "
   SQL = SQL & "Proveedor.NombreProveedor AS Nombre, "
   SQL = SQL & "Anticipo.Moneda, "
   SQL = SQL & "Anticipo.Cambio, "
   SQL = SQL & "anticipo.MontoTotal As MontoTotal, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoUsado, "
   SQL = SQL & "anticipo.MontoDevuelto AS MontoDevuelto,"
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoDiferenciaEnDevolucion, "
   SQL = SQL & "anticipo.EsUnaDevolucion,  "
   SQL = SQL & "anticipo.CodigoMoneda, "
   SQL = SQL & "anticipo.Status as Status, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS NumeroComprobante, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoRestanteAlDia "
   SQL = SQL & "FROM Anticipo INNER JOIN Proveedor ON "
   SQL = SQL & "anticipo.CodigoProveedor = Proveedor.CodigoProveedor "
   SQL = SQL & "AND anticipo.ConsecutivoCompania = Proveedor.ConsecutivoCompania LEFT JOIN anticipoPagado ON "
   SQL = SQL & "anticipo.Numero = anticipoPagado.NumeroAnticipo AND "
   SQL = SQL & "anticipo.ConsecutivoCompania = anticipoPagado.ConsecutivoCompania "
   SQL = SQL & "WHERE anticipo.Tipo = " & enum_TipoDeAnticipo.eTDA_PAGADO
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & "Pago.ConsecutivoCompania, "
   SQL = SQL & "Pago.Fecha, "
   SQL = SQL & "anticipo_1.Fecha AS FechaAnticipo, "
   SQL = SQL & "anticipo_1.Tipo, "
   SQL = SQL & "anticipoPagado_1.NumeroAnticipo, "
   SQL = SQL & "Pago.CodigoProveedor, "
   SQL = SQL & "Proveedor_1.NombreProveedor AS Nombre, "
   SQL = SQL & "Pago.Moneda, "
   SQL = SQL & "Pago.CambioAbolivares AS Cambio, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoTotal, "
   SQL = SQL & "anticipoPagado_1.MontoAplicado AS MontoUsado, "
   SQL = SQL & gUtilSQL.fNumToStrSQL(0) & " AS MontoDevuelto, "
   SQL = SQL & "Anticipo_1.MontoDiferenciaEnDevolucion AS MontoDiferenciaEnDevolucion, "
   SQL = SQL & gUtilSQL.fBooleanToStrSQL(False) & " AS EsUnaDevolucion, "
   SQL = SQL & "anticipoPagado_1.CodigoMoneda, "
   SQL = SQL & "anticipo_1.Status as Status, "
   SQL = SQL & "anticipoPagado_1.NumeroComprobante AS NumeroComprobante, "
   SQL = SQL & "anticipoPagado_1.MontoRestanteAlDia "
   SQL = SQL & "FROM Pago INNER JOIN anticipoPagado AS anticipoPagado_1 ON "
   SQL = SQL & "Pago.ConsecutivoCompania = anticipoPagado_1.ConsecutivoCompania AND "
   SQL = SQL & "Pago.NumeroComprobante = anticipoPagado_1.NumeroComprobante "
   SQL = SQL & " AND Pago.StatusOrdenDePago = " & gUtilSQL.fSimpleSqlValue(enum_StatusPago.eSP_VIGENTE)
   SQL = SQL & " INNER JOIN "
   SQL = SQL & "anticipo AS anticipo_1 ON anticipo_1.ConsecutivoAnticipo = anticipoPagado_1.ConsecutivoAnticipoUsado AND "
   SQL = SQL & "anticipo_1.ConsecutivoCompania = anticipoPagado_1.ConsecutivoCompania INNER JOIN "
   SQL = SQL & "Proveedor AS Proveedor_1 ON anticipo_1.CodigoProveedor = Proveedor_1.CodigoProveedor AND "
   SQL = SQL & "anticipo_1.ConsecutivoCompania = Proveedor_1.ConsecutivoCompania "
   SQL = SQL & "WHERE anticipo_1.Tipo = " & enum_TipoDeAnticipo.eTDA_PAGADO
   fSqlViewAnticiposPagadosAUnaFecha = SQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewAnticiposPagadosAUnaFecha", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
