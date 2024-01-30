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
Private Const CM_MODULO_CxP As Integer = 3
Private Const CM_MODULO_CxC As Integer = 2
Private Const GetGender = Enum_Gender.eg_Male

Public Function fConstruirSQLDelReporteFacturasSinCxC(ByVal valConsecutivoCompania As String, ByVal valIncluirFacturaHistoricas As Boolean) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   Sql = "SELECT "
   Sql = Sql & "Factura.Numero, "
   Sql = Sql & "Factura.Fecha, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDeDocumento, "
   Sql = Sql & "Factura.TotalMontoExento, "
   Sql = Sql & "Factura.TotalBaseImponible, "
   Sql = Sql & "Factura.TotalIVA, "
   Sql = Sql & "Factura.TotalFactura, "
   Sql = Sql & "Cliente.Nombre "
   Sql = Sql & "FROM Factura INNER JOIN "
   Sql = Sql & "Cliente ON Factura.ConsecutivoCompania = Cliente.ConsecutivoCompania "
   Sql = Sql & "AND Factura.CodigoCliente = Cliente.Codigo LEFT OUTER JOIN "
   Sql = Sql & "CxC ON Factura.ConsecutivoCompania = CxC.ConsecutivoCompania "
   Sql = Sql & "AND Factura.Numero = CxC.NumeroDocumentoOrigen "
   Sql = Sql & "WHERE (Factura.StatusFactura = " & enum_StatusFactura.eSF_EMITIDA & ") "
   Sql = Sql & "AND " & gUtilSQL.DfSQLIsNull("CxC.NumeroDocumentoOrigen")
   Sql = Sql & "AND  (Factura.ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ") "
   If Not valIncluirFacturaHistoricas Then
      Sql = Sql & "AND (factura.FacturaHistorica <> " & gUtilSQL.fSimpleSqlValue("S") & ") "
   End If
   Sql = Sql & "ORDER BY factura.Numero, factura.Fecha"
   fConstruirSQLDelReporteFacturasSinCxC = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteFacturasSinCxC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxCHistoricoCxC(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, _
         ByVal valDescontarAbonos As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
   
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM CxC AS " & sqlCxCTableName
   Sql = Sql & " WHERE " & sqlCxCTableName & ".ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & " AND " & sqlCxCTableName & ".CodigoCliente = CxC.CodigoCliente"
   Sql = Sql & " AND " & sqlCxCTableName & ".Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   Sql = Sql & " AND " & sqlCxCTableName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoCxC = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSaldoInicialAnalisisCxCHistoricoCxC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen() As String
   On Error Resume Next
   fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen = gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat
   On Error GoTo 0
End Function

Private Function fSQLSaldoInicialAnalisisCxCHistoricoAnticipos(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, ByVal valDescontarAbonos As Boolean, _
                     ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM ANTICIPO AS " & sqlAnticipoTableName
   Sql = Sql & " WHERE " & sqlAnticipoTableName & "." & "ConsecutivoCompania" & " = " & mConsecutivoCompania
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "Tipo" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "CodigoCliente" & " = ANTICIPO.CodigoCliente"
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "Moneda" & " = ANTICIPO.Moneda"
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "Status" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_ANULADO)
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "Fecha" & " < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoAnticipos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSaldoInicialAnalisisCxCHistoricoAnticipos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLAnalisisCXCHistorico(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
            ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
            ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim sqlCxC As String
   Dim SqlAnticipos As String
   Dim sqlCobros As String
   Dim sqlOrderBy As String
   On Error GoTo h_ERROR
   sqlCxC = fSQLAnalisisCxCHistoricoCxC(valcodigoCliente, valFechaInicial, valFechaFinal, valUsarCambio, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   SqlAnticipos = fSQLAnalisisCxCHistoricoAnticipos(valcodigoCliente, valFechaInicial, valFechaFinal, valUsarCambio, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   sqlCobros = fSQLAnalisisCxCHistoricoCobros(valcodigoCliente, valFechaInicial, valFechaFinal, valUsarCambio, mConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   sqlOrderBy = " ORDER BY " & "Codigo, MonedaReporte,TitulodelGrupo desc, ColumnaParaGrupoTipoReporte, Fecha, ColumnaParaGrupoDetalle"
   Sql = sqlCxC
   Sql = Sql & " UNION " & SqlAnticipos
   Sql = Sql & " UNION " & sqlCobros
   Sql = Sql & sqlOrderBy
   fSQLAnalisisCXCHistorico = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLAnalisisCXCHistorico", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxCHistoricoCxC(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
               ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
               ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("0") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloDelGrupo, "
   Sql = Sql & " CxC.CodigoCliente AS Codigo, "
   Sql = Sql & sqlMoneda & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & sqlTipoDeCxC & " AS TipoDocumento, "
   Sql = Sql & gUtilSQL.fCast("CxC.TipoCxC" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "Cxc.Numero", eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & " CxC.Numero AS Numero, "
   Sql = Sql & gUtilSQL.fCast(" CxC.Fecha", eTDSS_VARCHAR, "") & " AS Fecha, "
   Sql = Sql & gUtilSQL.fCast(" CxC.FechaVencimiento", eTDSS_VARCHAR, "") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusCxC & " AS Status, "
   Sql = Sql & sqlMontoActualCxC & " AS MontoActual, "
   Sql = Sql & sqlMontoTotalCxC & " AS MontoTotal, "
   Sql = Sql & "Cliente.Nombre AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocCobrado, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocCobrado, "
   Sql = Sql & "0 AS MontoAbonado "
   Sql = Sql & " FROM Cliente INNER JOIN CxC ON "
   Sql = Sql & " CxC.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " AND CxC.CodigoCliente = Cliente.Codigo"
   Sql = Sql & " WHERE CxC.ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValue(" CxC.CodigoCliente", Trim(valcodigoCliente), False)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", valFechaInicial, valFechaFinal)
   fSQLAnalisisCxCHistoricoCxC = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLAnalisisCxCHistoricoCxC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLAnalisisCxCHistoricoAnticipos(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
         ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
         ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("1") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Anticipos") & " AS TituloDelGrupo, "
   Sql = Sql & " Anticipo.CodigoCliente" & " AS Codigo, "
   Sql = Sql & sqlMoneda & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & sqlTipoDeAnticipo & " AS TipoDocumento, "
   Sql = Sql & gUtilSQL.fCast("Anticipo.ConsecutivoAnticipo", eTDSS_VARCHAR, "") & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & " Anticipo.Numero" & " AS Numero, "
   Sql = Sql & gUtilSQL.fCast(" Anticipo.Fecha", eTDSS_VARCHAR, "") & " AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusDeAnticipo & " AS Status, "
   Sql = Sql & sqlMontoActual & " AS MontoActual, "
   Sql = Sql & sqlMontoTotal & " AS MontoTotal, "
   Sql = Sql & " Cliente.Nombre AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS TipoDocCobrado, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS NumeroDocCobrado, "
   Sql = Sql & "0 AS MontoAbonado "
   Sql = Sql & " FROM Anticipo INNER JOIN Cliente ON "
   Sql = Sql & " Anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " AND Anticipo.CodigoCliente = Cliente.Codigo"
   Sql = Sql & " WHERE Anticipo.ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValue(" Anticipo.CodigoCliente", Trim(valcodigoCliente), False)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(" Anticipo.Fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " AND " & " Anticipo.Tipo" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO)
   Sql = Sql & " AND " & " Anticipo.EsUnaDevolucion" & " = " & gUtilSQL.fBooleanToSqlValue(False)
   Set gEnumProyecto = Nothing
   fSQLAnalisisCxCHistoricoAnticipos = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLAnalisisCXCHistoricoAnticipos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
   
Private Function fSQLAnalisisCxCHistoricoCobros(ByVal valcodigoCliente As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
            ByVal valUsarCambio As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, _
            ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cobranzas") & " AS TituloDelGrupo, "
   Sql = Sql & " Cobranza.CodigoCliente AS Codigo, "
   Sql = Sql & sqlMonedaCob & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & gUtilSQL.getIIF("Cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
            gUtilSQL.fSimpleSqlValue("Cobranza(*)"), gUtilSQL.fSimpleSqlValue("Cobranza"), True) & " AS TipoDocumento, "
   Sql = Sql & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & " Cobranza.Numero AS Numero, "
   Sql = Sql & gUtilSQL.fCast(" Cobranza.Fecha", eTDSS_VARCHAR, "") & " AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusCobranza & " AS Status, "
   Sql = Sql & sqlMontoCobranza & " AS MontoActual, "
   Sql = Sql & " 0 AS MontoTotal, "
   Sql = Sql & " Cliente.Nombre AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & sqlTipoDeDocCobrado & " AS TipoDocCobrado, "
   Sql = Sql & sqlNumeroDocumentoCobrado & " AS NumeroDocCobrado, "
   Sql = Sql & sqlMontoAbonado & " AS MontoAbonado "
   
   Sql = Sql & " FROM ((Cliente INNER JOIN " & " Cobranza ON ("
   Sql = Sql & "Cliente.Codigo = " & " Cobranza.CodigoCliente"
   Sql = Sql & ") AND (Cliente.ConsecutivoCompania = " & " Cobranza.ConsecutivoCompania"
   Sql = Sql & ")) INNER JOIN  DocumentoCobrado ON ("
   Sql = Sql & " Cobranza.Numero = DocumentoCobrado.NumeroCobranza"
   Sql = Sql & ") AND ( Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   Sql = Sql & ")) LEFT JOIN CuentaBancaria ON ("
   Sql = Sql & " Cobranza.CodigoCuentaBancaria =  CuentaBancaria.Codigo"
   Sql = Sql & ") AND ( Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania)"
   Sql = Sql & "   INNER JOIN Moneda ON DocumentoCobrado.CodigoMonedaDeCxC = Moneda.Codigo"
   Sql = Sql & " INNER JOIN IGV_DocCobradosPorMoneda  On"
   Sql = Sql & " (cobranza.ConsecutivoCompania = IGV_DocCobradosPorMoneda.ConsecutivoCompania)"
   Sql = Sql & " AND (cobranza.Numero = IGV_DocCobradosPorMoneda.NumeroCobranza) "
   Sql = Sql & " AND (Moneda.Codigo = IGV_DocCobradosPorMoneda.CodigoMonedaDeCxc) "
   
   Sql = Sql & " WHERE  Cobranza.ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValue(" Cobranza.CodigoCliente", Trim(valcodigoCliente), False)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(" Cobranza.Fecha", valFechaInicial, valFechaFinal)
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
   
   Sql = Sql & " UNION "
   Sql = Sql & "SELECT "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("2") & " AS ColumnaParaGrupoTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cobranzas") & " AS TituloDelGrupo, "
   Sql = Sql & " Cobranza.CodigoCliente AS Codigo, "
   Sql = Sql & sqlMonedaCob & " AS Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & gUtilSQL.getIIF("Cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
            gUtilSQL.fSimpleSqlValue("Cobranza(*)"), gUtilSQL.fSimpleSqlValue("Cobranza"), True) & " AS TipoDocumento, "
   Sql = Sql & sqlColumnaParaGrupoDetalle & " AS ColumnaParaGrupoDetalle, "
   Sql = Sql & SqlSaldoInicialSinAbonos & " AS SaldoInicialSinAbonos, "
   Sql = Sql & SqlSaldoInicialConAbonos & " AS SaldoInicialConAbonos, "
   Sql = Sql & " Cobranza.Numero AS Numero, "
   Sql = Sql & " Cobranza.Fecha AS Fecha, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("") & " AS FechaVencimiento, "
   Sql = Sql & sqlStatusCobranza & " AS Status, "
   Sql = Sql & "0 AS MontoActual, "
   Sql = Sql & sqlMontoCobranza & " AS MontoTotal, "
   Sql = Sql & " Cliente.Nombre AS Nombre, "
   Sql = Sql & sqlCambio & " AS CambioBs, "
   Sql = Sql & sqlTipoDeDocCobrado & " AS TipoDocCobrado, "
   Sql = Sql & sqlNumeroDocumentoCobrado & " AS NumeroDocCobrado, "
   Sql = Sql & sqlMontoAbonado & " AS MontoAbonado "
   Sql = Sql & " FROM ((Cliente INNER JOIN Cobranza ON ("
   Sql = Sql & " Cliente.Codigo = Cobranza.CodigoCliente"
   Sql = Sql & ") AND (Cliente.ConsecutivoCompania = Cobranza.ConsecutivoCompania"
   Sql = Sql & ")) INNER JOIN AnticipoCobrado ON ("
   Sql = Sql & " Cobranza.Numero = AnticipoCobrado.NumeroCobranza "
   Sql = Sql & ") AND (Cobranza.ConsecutivoCompania =  AnticipoCobrado.ConsecutivoCompania"
   Sql = Sql & ")) LEFT JOIN CuentaBancaria ON ("
   Sql = Sql & " Cobranza.CodigoCuentaBancaria = CuentaBancaria.Codigo"
   Sql = Sql & ") AND ( Cobranza.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania)"
   Sql = Sql & "   INNER JOIN Moneda ON AnticipoCobrado.CodigoMoneda = Moneda.Codigo"
   Sql = Sql & " INNER JOIN IGV_DocCobradosPorMoneda  On"
   Sql = Sql & " (cobranza.ConsecutivoCompania = IGV_DocCobradosPorMoneda.ConsecutivoCompania)"
   Sql = Sql & " AND (cobranza.Numero = IGV_DocCobradosPorMoneda.NumeroCobranza) "
   Sql = Sql & " AND (Moneda.Codigo = IGV_DocCobradosPorMoneda.CodigoMonedaDeCxc) "
   Sql = Sql & " WHERE Cobranza.ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValue(" Cobranza.CodigoCliente", Trim(valcodigoCliente), False)
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(" Cobranza.Fecha", valFechaInicial, valFechaFinal)
   fSQLAnalisisCxCHistoricoCobros = Sql
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fsqlAnalisisCxCHistoricoCobros", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fSQLSaldoInicialAnalisisCxCHistoricoCobranzas(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, _
   ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql  As String
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
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM Cobranza AS " & sqlCobranzaTableName
   Sql = Sql & " WHERE " & sqlCobranzaTableName & ".ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & " AND " & sqlCobranzaTableName & ".CodigoCliente = Cobranza.CodigoCliente"
   Sql = Sql & " AND " & sqlCobranzaTableName & ".Moneda = Cobranza.Moneda"
   Sql = Sql & " AND StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   Sql = Sql & " AND " & sqlCobranzaTableName & ".TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaDeFactura)
   Sql = Sql & " AND Fecha < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoCobranzas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSaldoInicialAnalisisCxCHistoricoCobranzas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteDeCXCPorVendedor(ByVal mConsecutivoCompania As String, ByVal mMonedaLocal As String, _
            ByVal mMontoTotalYRestanteConIva As Boolean, ByVal mTasaDeCambio As Boolean, ByVal mCantidadAImprimir As String, _
            ByVal mCodigoVendedor As String, ByVal dtpFechaInicial As Date, ByVal dtpFechaFinal As Date, ByVal gUltimaTasaDeCambio As Object, _
            ByVal gMonedaLocalActual As Object, ByVal valMostrarUltimaFechaPago As Boolean, ByVal usarContacto As Boolean) As String
   Dim Sql As String
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
   Sql = "SELECT "
   Sql = Sql & " Cxc.CodigoVendedor, "
   If (usarContacto) Then
      Sql = Sql & " Cliente.Contacto, "
   End If
   Sql = Sql & gUtilSQL.DfCStrSQL(" Cxc.CodigoVendedor")
   Sql = Sql & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat
   Sql = Sql & gUtilSQL.DfCStrSQL("Vendedor.Nombre") & " AS NombreVendedor, "
   Sql = Sql & " Cxc.Moneda, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
   Sql = Sql & " Cxc.CodigoCliente, "
   Sql = Sql & gUtilSQL.DfCStrSQL(" Cxc.CodigoCliente") ' & ", "
   Sql = Sql & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat
   Sql = Sql & gUtilSQL.DfCStrSQL(" Cliente.Nombre")
    If (valMostrarUltimaFechaPago) Then
   Sql = Sql & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" - ") & gUtilSQL.CharConcat
   Sql = Sql & gUtilSQL.DfCStrSQL(" Cliente.Telefono")
   End If
   Sql = Sql & " AS NombreCliente, "
   Sql = Sql & " Cxc.Numero, "
   Sql = Sql & " Cxc.Fecha AS FechaDoc, "
   Sql = Sql & " Cxc.FechaVencimiento, "
   Sql = Sql & sqlDateDiff & " AS DiasVencidos, "
   Sql = Sql & sqlMtoTotal & " AS MontoTotal, "
   Sql = Sql & sqlMtoAbonado & " AS MontoAbonado, "
   Sql = Sql & sqlMtoRestante & " AS MontoRestante "
   If (valMostrarUltimaFechaPago) Then
   Sql = Sql & ", ISNULL(CONVERT(VARCHAR(10), IGV_CobranzaUltimaFechaAbono.UltimaFechAbono,103) ,'') AS UltimaFechAbono"
   End If
   Sql = Sql & " FROM Cliente INNER JOIN ("
   Sql = Sql & " Vendedor INNER JOIN " & " Cxc ON ("
   Sql = Sql & " Vendedor.Codigo" & _
               " = " & " Cxc.CodigoVendedor) AND ("
   Sql = Sql & " Vendedor.ConsecutivoCompania" & _
               " = Cxc.ConsecutivoCompania)) ON ("
   Sql = Sql & " Cliente.Codigo" & _
               " = Cxc.CodigoCliente) AND ("
   Sql = Sql & " Cliente.ConsecutivoCompania" & _
               " =  Cxc.ConsecutivoCompania)"
   If (valMostrarUltimaFechaPago) Then
   Sql = Sql & "   LEFT JOIN " & insCobranzaVista.GetViewNameCobranzaUltimaFecha() & " AS IGV_CobranzaUltimaFechaAbono ON "
   Sql = Sql & " Cxc.ConsecutivoCompania = IGV_CobranzaUltimaFechaAbono.ConsecutivoCompania AND "
   Sql = Sql & " Cxc.NumeroDocumentoOrigen = IGV_CobranzaUltimaFechaAbono.NumeroDelDocumentoCobrado "
   End If
   Sql = Sql & " WHERE  Cxc.ConsecutivoCompania" & _
               " = " & mConsecutivoCompania
   Sql = Sql & " AND  Cxc.Status IN ('" & _
               gConvert.charAEnumerativoInt(gConvert.enumerativoAChar(eSD_PORCANCELAR)) & "', '" & _
               gConvert.charAEnumerativoInt(gConvert.enumerativoAChar(eSD_ABONADO)) & "')"
   If mCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND  Cxc.CodigoVendedor" & _
                  " = '" & Trim(mCodigoVendedor) & "' "
   End If
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(" Cxc.Fecha", dtpFechaInicial, dtpFechaFinal)
   Sql = Sql & " ORDER BY "
   Sql = Sql & " Cxc.CodigoVendedor, "
   Sql = Sql & " Cxc.Moneda, "
   Sql = Sql & " Cxc.CodigoCliente, "
   Sql = Sql & " Cxc.Fecha, "
   Sql = Sql & " Cxc.FechaVencimiento, "
   Sql = Sql & " Cxc.Numero"
   fConstruirSQLDelReporteDeCXCPorVendedor = Sql
   Set gEnumReport = Nothing
   Set insCobranzaVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteDeCXCPorVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteDeEstadisticoDeVendedor(ByVal mConsecutivoCompania As String, ByVal mMonedaReporte As String, ByVal mDtpFechaInicial As Date, _
                     ByVal mDtpFechaFinal As Date, ByVal gUltimaTasaDeCambio As Object, ByVal gMonedaLocalActual As Object) As String
   Dim Sql As String
   Dim sqlTotalPorCambio As String
   Dim adicionalAlGroupBy As String
   Dim Formato As String
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   sqlTotalPorCambio = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("Factura.CambioABolivares", "Factura.Moneda", gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "Factura.TotalBaseImponible", "Factura.Fecha"), True, "")
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate("Factura.Fecha", "Anno") & ", "
   Sql = Sql & gUtilSQL.fFirst("Vendedor.Nombre", "NombreDelVendedor") & ", "
   Sql = Sql & " Factura.CodigoVendedor AS CodigoDelVendedor, "
      If mMonedaReporte = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gMonedaLocalActual.GetHoyNombreMoneda) Then
         Sql = Sql & " Factura.Moneda, "
         Sql = Sql & "SUM(" & sqlTotalPorCambio & ") AS AcumBaseImponible, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 1 ", sqlTotalPorCambio, 0) & ") AS Ene, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 2 ", sqlTotalPorCambio, 0) & ") AS Feb, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 3 ", sqlTotalPorCambio, 0) & ") AS Mar, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 4 ", sqlTotalPorCambio, 0) & ") AS Abr, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 5 ", sqlTotalPorCambio, 0) & ") AS May, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 6 ", sqlTotalPorCambio, 0) & ") AS Jun, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 7 ", sqlTotalPorCambio, 0) & ") AS Jul, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 8 ", sqlTotalPorCambio, 0) & ") AS Ago, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 9 ", sqlTotalPorCambio, 0) & ") AS Sep, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 10", sqlTotalPorCambio, 0) & ") AS Oct, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 11", sqlTotalPorCambio, 0) & ") AS Nov, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 12", sqlTotalPorCambio, 0) & ") AS Dic  "
         adicionalAlGroupBy = ", " & " Factura.Moneda"
      Else
         Sql = Sql & " Factura.Moneda, "
         Sql = Sql & "SUM(" & " Factura.TotalBaseImponible) AS AcumBaseImponible, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 1 ", " Factura.TotalBaseImponible ", 0) & ") AS Ene, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 2 ", " Factura.TotalBaseImponible ", 0) & ") AS Feb, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 3 ", " Factura.TotalBaseImponible ", 0) & ") AS Mar, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 4 ", " Factura.TotalBaseImponible ", 0) & ") AS Abr, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 5 ", " Factura.TotalBaseImponible ", 0) & ") AS May, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 6 ", " Factura.TotalBaseImponible ", 0) & ") AS Jun, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 7 ", " Factura.TotalBaseImponible ", 0) & ") AS Jul, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 8 ", " Factura.TotalBaseImponible ", 0) & ") AS Ago, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 9 ", " Factura.TotalBaseImponible ", 0) & ") AS Sep, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 10", " Factura.TotalBaseImponible ", 0) & ") AS Oct, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 11", " Factura.TotalBaseImponible ", 0) & ") AS Nov, "
         Sql = Sql & "SUM(" & gUtilSQL.getIIF(gUtilSQL.DfSQLMonthOfDate(" Factura.Fecha") & " = 12", " Factura.TotalBaseImponible ", 0) & ") AS Dic  "
         adicionalAlGroupBy = ", " & " Factura.Moneda"
      End If
   Sql = Sql & " FROM Factura "
   Sql = Sql & " INNER JOIN Vendedor"
   Sql = Sql & " ON Vendedor.Codigo = " & " Factura.CodigoVendedor"
   Sql = Sql & " AND " & " Factura.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(" Factura.Fecha", mDtpFechaInicial, mDtpFechaFinal)
   Sql = Sql & ") AND (" & " Factura.ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & ") AND (" & " Factura.StatusFactura ='" & gConvert.enumerativoAChar(enum_StatusFactura.eSF_EMITIDA) & "')"
   Sql = Sql & " GROUP BY " & " Factura.CodigoVendedor, "
   Sql = Sql & " Vendedor.Nombre, "
   Sql = Sql & gUtilSQL.DfSQLYearOfDate(" Factura.Fecha")
   Sql = Sql & adicionalAlGroupBy
   Sql = Sql & " ORDER BY " & gUtilSQL.DfSQLYearOfDate(" Factura.Fecha") & ", "
   Sql = Sql & " Factura.CodigoVendedor, "
   Sql = Sql & " Vendedor .Nombre"
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteDeEstadisticoDeVendedor = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteDeEstadisticoDeVendedor", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

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
   Dim Sql As String
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
      
   Sql = "SELECT "
   If AgruparTipo = "SectorDeNegocio" Then
      Sql = Sql & " Cliente.SectorDeNegocio, "
   ElseIf AgruparTipo = "ZonaDeCobranza" Then
      Sql = Sql & " Cliente.ZonaDeCobranza, "
   End If
   
   If (usarContacto) Then
      Sql = Sql & " Cliente.Contacto,"
   End If
   Sql = Sql & " Cliente.Codigo, "
   Sql = Sql & " Cliente.Nombre, "
   Sql = Sql & " CxC.Fecha, "
   Sql = Sql & " CxC.Numero, "
   Sql = Sql & " CxC.Moneda, "
   Sql = Sql & " CxC.CambioABolivares, "
   Sql = Sql & sqlStatusCxC & ", "
   Sql = Sql & sqlCambioAMonedaLocal & " AS CambioAMonedaLocal, "
   Sql = Sql & sqlCamposDefFactura & " AS InformacionAdicional, "
   Sql = Sql & sqlMonto & " AS Monto, "
   Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambioAMonedaLocal, "Moneda", _
                         sqlMonto, mUsarCambioOriginal, "MontoEnMonedaLocal") & ", "
   Sql = Sql & sqlMonedaGrupo & " AS MonedaParaGrupo"
   If mUsaModuloDeContabilidad Then
      Sql = Sql & "," & gUtilSQL.getIIF(fSQGenerarCuentaContable(mConsecutivoCompania, mDtpFechaInicial, mDtpFechaFinal) & " <> " & gUtilSQL.fSimpleSqlValue(""), fSQGenerarCuentaContable(mConsecutivoCompania, mDtpFechaInicial, mDtpFechaFinal), gUtilSQL.fSimpleSqlValue("No Aplica"), True)
      Sql = Sql & " AS NumeroComprobante"
   End If
   Sql = Sql & " FROM  Cliente INNER JOIN CxC ON "
   Sql = Sql & " Cliente.Codigo" & _
               " = CxC.CodigoCliente"
   Sql = Sql & " AND Cliente.ConsecutivoCompania" & _
               " = CxC.ConsecutivoCompania"
   Sql = Sql & " WHERE "
   If AgruparTipo = "SectorDeNegocio" Then
     If FiltroPorTipo <> "TODOS" Then
       Sql = Sql & " Cliente.SectorDeNegocio= '" & FiltroPorTipo & "' AND "
     End If
   End If
   
   If AgruparTipo = "ZonaDeCobranza" Then
      If FiltroPorTipo <> "TODAS" Then
        Sql = Sql & " Cliente.ZonaDeCobranza='" & FiltroPorTipo & "' AND "
     End If
   End If
   
   Sql = Sql & " Cliente.ConsecutivoCompania" & _
               " = " & mConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(" CxC.Fecha", _
                        mDtpFechaInicial, mDtpFechaFinal)
                        
      If statusCxC <> "" Then
         Sql = Sql & " AND  CxC.Status = " & statusCxC
      End If

   Sql = Sql & " ORDER BY "
   If AgruparTipo = "SectorDeNegocio" Then
      Sql = Sql & " Cliente.SectorDeNegocio, "
   ElseIf AgruparTipo = "ZonaDeCobranza" Then
      Sql = Sql & " Cliente.ZonaDeCobranza, "
   End If
  
   Sql = Sql & " CxC.Status, "
'   If getIsSQLDataBase Then
      Sql = Sql & " MonedaParaGrupo,"
'   Else
'      SQL = SQL & sqlMonedaGrupo & ", "
'   End If
   Sql = Sql & " CxC.Fecha, "
   Sql = Sql & " CxC.Numero"
   fSQLDelReporteCxCEntreFechas = Sql
   Set gEnumProyecto = Nothing
   Set gEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteCxCEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQGenerarCuentaContable(ByVal mConsecutivoCompania As String, ByVal mDtpFechaInicial As Date, ByVal mDtpFechaFinal As Date) As String
   Dim Sql As String
   Dim SqlPeriodo As String
   On Error GoTo h_ERROR
   
   Sql = Sql & "SELECT Periodo.ConsecutivoPeriodo"
   Sql = Sql & " FROM Periodo WHERE "
   Sql = Sql & "Periodo.ConsecutivoCompania = "
   Sql = Sql & mConsecutivoCompania
   SqlPeriodo = gDbUtil.fBuildResultSetAsString(Sql, False)
   Sql = ""
   Sql = "(SELECT  Comprobante.NUMERO"
   Sql = Sql & " FROM Comprobante WHERE "
   Sql = Sql & " Comprobante.NoDocumentoOrigen " & "="
   Sql = Sql & " Cxc.TipoCxC"
   Sql = Sql & gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat
   Sql = Sql & " Cxc.Numero"
   Sql = Sql & " AND Comprobante.GeneradoPor" & " = "
   Sql = Sql & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXC))
   Sql = Sql & " AND Comprobante.ConsecutivoPeriodo"
   Sql = Sql & " IN (" & SqlPeriodo & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", _
                        mDtpFechaInicial, mDtpFechaFinal) & ")"
   fSQGenerarCuentaContable = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQGenerarCuentaContable", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLChequesDevueltos(ByVal vReporteEnMonedaLocal As Boolean, ByVal vUsarCambioOriginal As Boolean, ByVal gMonedaLocalActual As Object, _
                                      ByVal gUltimaTasaDeCambio As Object, ByVal valConsecutivoCompania As String, ByVal valFechaInicial As Date, _
                                       ByVal valFechaFinal As Date, ByVal valcodigoVendedor As String, valStatus As String) As String
   Dim Sql As String
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
   
   Sql = "SELECT "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("CxC.Status", enum_StatusDocumento.eSD_PORCANCELAR, _
                        gEnumProyecto.fEnumStatusCxCToStringInArray(True), "") & " AS StatusStr, "
   Sql = Sql & "CxC.Status, CxC.Numero, CxC.Fecha, "
   Sql = Sql & sqlMonedaReporte & " AS MonedaDelReporte, "
   Sql = Sql & sqlMontoCheque & " AS MontoCheque, "
   Sql = Sql & sqlMontoAbonado & " AS TotalAbonado, "
   Sql = Sql & "CxC.FechaCancelacion, CxC.FechaVencimiento, CxC.FechaAnulacion, "
   Sql = Sql & sqlMoneda & " AS Moneda, "
   Sql = Sql & sqlCambio & " AS CambioABolivares, "
   Sql = Sql & "CxC.NumeroDocumentoOrigen, CxC.CodigoMoneda, "
   Sql = Sql & "Vendedor.Nombre AS Vendedor, Cliente.Nombre AS Cliente "
   Sql = Sql & "FROM CxC INNER JOIN "
   Sql = Sql & " Cliente ON CxC.ConsecutivoCompania = Cliente.ConsecutivoCompania"
   Sql = Sql & " AND CxC.CodigoCliente = Cliente.Codigo INNER JOIN"
   Sql = Sql & " Vendedor ON CxC.ConsecutivoCompania = Vendedor.ConsecutivoCompania"
   Sql = Sql & " AND CxC.CodigoVendedor = Vendedor.Codigo"
   Sql = Sql & " WHERE (CxC.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (CxC.TipoCxc = " & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoCxP.eTCxP_CHEQUEDEVUELTO) & ")"
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", valFechaInicial, valFechaFinal)
   If LenB(Trim(valcodigoVendedor)) > 0 Then
      Sql = Sql & " AND (CxC.codigoVendedor = " & valcodigoVendedor & ")"
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
      Sql = Sql & " AND (CxC.codigoVendedor = " & valcodigoVendedor & ")"
   End If
    If statusCxC <> "" Then
         Sql = Sql & " AND  CxC.Status = " & statusCxC
   End If
      
   Sql = Sql & " GROUP BY CxC.ConsecutivoCompania, CxC.Numero, CxC.Status, CxC.TipoCxc, CxC.Fecha, CxC.FechaCancelacion, CxC.Moneda, CxC.CambioAbolivares, "
   Sql = Sql & " CxC.NumeroDocumentoOrigen, CxC.CodigoMoneda, Vendedor.Nombre, Cliente.Nombre, CxC.MontoAbonado, CxC.FechaVencimiento, cxC.fechaAnulacion "
   Sql = Sql & " ORDER BY MonedaDelReporte, StatusStr, CxC.Fecha, CxC.Numero"
   Set gEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLChequesDevueltos = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLChequesDevueltos", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLSaldoInicialAnalisisCxCHistoricoCxCVista(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, _
         ByVal valDescontarAbonos As Boolean, ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM " & sqlVistaName
   Sql = Sql & " WHERE " & sqlVistaName & ".ConsecutivoCompania = " & mConsecutivoCompania
   Sql = Sql & " AND " & sqlVistaName & ".CodigoCliente = Codigo"
   Sql = Sql & " AND " & sqlVistaName & ".Fecha < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoCxCVista = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSaldoInicialAnalisisCxCHistoricoCxCVista", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista(ByVal valALaFecha As Date, ByVal valUsarCambio As Boolean, ByVal valDescontarAbonos As Boolean, _
                     ByVal mConsecutivoCompania As String, ByVal gMonedaLocalActual As Object, ByVal gUltimaTasaDeCambio As Object) As String
   Dim Sql As String
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
   Sql = "(SELECT SUM(" & sqlMonto & ") AS SaldoInicial"
   Sql = Sql & " FROM " & sqlAnticipoTableName
   Sql = Sql & " WHERE " & sqlAnticipoTableName & "." & "ConsecutivoCompania" & " = " & mConsecutivoCompania
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "CodigoCliente = " & insAnticipoVista.GetViewNameAnticipoHistoricoCliente & ".Codigo"
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "Moneda = " & insAnticipoVista.GetViewNameAnticipoHistoricoCliente & ".MonedaAnticipo"
   Sql = Sql & " AND " & sqlAnticipoTableName & "." & "Fecha" & " < " & gUtilSQL.fDateToSQLValue(valALaFecha) & ")"
   fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSaldoInicialAnalisisCxCHistoricoAnticiposVista", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteCxCconDescripcion(ByVal valZonaCobranza As String, ByVal valAnalisisDeVencimientoPor As String, ByVal valMonedaDeLosReportes As String, ByVal valConsecutivoCompania As Long, ByVal valHoyNombreMoneda As String, ByRef gUltimaTasaDeCambio As Object, ByVal usarContacto As Boolean) As String
   Dim Sql As String
   Dim valMontoExento As String
   Dim valMontoGravado As String
   Dim valMontoIva As String
   Dim valMontoAbonado As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   Sql = "SELECT "
   If (usarContacto) Then
      Sql = Sql & " Cliente.Contacto,"
   End If
   Sql = Sql & "CxC.Numero, "
   Sql = Sql & "CxC.Descripcion, "
   Sql = Sql & "(SELECT " & "Moneda.simbolo" & _
               " FROM " & "Moneda WHERE " & "CxC.Moneda" & _
               " = " & "Moneda.Nombre) AS SimboloDeLaMoneda, "
   Sql = Sql & "CxC.FechaVencimiento, "
   Sql = Sql & "CxC.CodigoCliente, "
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
      Sql = Sql & "(((" & valMontoExento & _
               " + " & valMontoGravado & _
               " + " & valMontoIva & _
               ") - " & valMontoAbonado & ")"
   Else
      Sql = Sql & "(((" & "CxC.MontoExento" & _
               " + " & "CxC.MontoGravado" & _
               " + " & "CxC.MontoIVA" & _
               ") - " & "CxC.MontoAbonado)"
   End If
   Sql = Sql & ") AS Monto, "
   Sql = Sql & "cliente.Nombre AS NombreCliente, "
   Sql = Sql & "cliente.Telefono, "
   Sql = Sql & "cliente.Direccion, "
   Sql = Sql & "cliente.ZonaDeCobranza AS ZonaCobranza, "
   Sql = Sql & "CxC.Moneda"
   Sql = Sql & " FROM cliente INNER JOIN " & "CxC ON ("
   Sql = Sql & "cliente.Codigo" & _
               " = CxC.CodigoCliente) AND ("
   Sql = Sql & "cliente.ConsecutivoCompania" & _
               " = " & "CxC.ConsecutivoCompania)"
   Sql = Sql & " WHERE CxC.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   Sql = Sql & " AND CxC.Status IN ('" & _
               gConvert.enumerativoAChar(enum_StatusDocumento.eSD_PORCANCELAR) & "','" & _
               gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ABONADO) & "')"
   If valZonaCobranza <> "TODAS" Then
      Sql = Sql & " AND cliente.ZonaDeCobranza" & _
                  " ='" & valZonaCobranza & "'"
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "CxC.Moneda, "
   Sql = Sql & "cliente.ZonaDeCobranza,"
   If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Codigo) Then
      Sql = Sql & "CxC.CodigoCliente, "
   Else
      Sql = Sql & "cliente.Nombre, "
   End If
   Sql = Sql & "CxC.FechaVencimiento"
   fConstruirSQLDelReporteCxCconDescripcion = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteCxCconDescripcion", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteAnalisisDeVencimiento(ByVal valcodigoVendedor As String, ByVal valCantidadAImprimir As String, ByVal valZonaCobranza As String, ByVal valAnalisisDeVencimientoPor As String, ByVal valConsecutivoCompania As String, ByVal valMonedaDeLosReportes As String, ByVal valNombreMonedaLocal As String, ByRef InsAdmPropAnalisisVenc As Object, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object) As String
   Dim Sql As String
   Dim sqlMtoRestante As String
   Dim sqlDateDiff As String
   Dim sqlMtoTotal As String
   Dim sqlMonedaReporte As String
   Dim sqlCambio As String
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   Sql = ""
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
      Sql = "SELECT "
      Sql = Sql & "cliente.ZonaDeCobranza, "
      Sql = Sql & "cxC.CodigoCliente, "
      Sql = Sql & "cliente.Nombre, "
      Sql = Sql & "cxC.Moneda, "
      Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      Sql = Sql & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AS Vto1, "
      Sql = Sql & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AS Vto2, "
      Sql = Sql & InsAdmPropAnalisisVenc.GetTercerVencimiento & " AS Vto3, "
      Sql = Sql & sqlDateDiff & " AS DiasVencidos, "
      Sql = Sql & sqlMtoRestante & " AS MtoRestante, "
      Sql = Sql & "cxC.FechaVencimiento, "
      Sql = Sql & "cxC.Numero, "
      Sql = Sql & "cxC.Fecha, "
      Sql = Sql & sqlMtoTotal & " AS MtoTotal"
      Sql = Sql & " FROM " & "cxC INNER JOIN "
      Sql = Sql & "cliente ON "
      Sql = Sql & "cxC.ConsecutivoCompania" & _
                  " = " & "cliente.ConsecutivoCompania"
      Sql = Sql & " AND "
      Sql = Sql & "cxC.CodigoCliente" & _
                  " = " & "cliente.Codigo "
      Sql = Sql & " WHERE "
      Sql = Sql & "cxC.ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      Sql = Sql & " AND (" & "cxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & _
                  " OR " & "cxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      If valCantidadAImprimir = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And _
         Len(valcodigoVendedor) > 0 Then
         Sql = Sql & " AND " & "cxC.CodigoVendedor" & _
                     " = " & gUtilSQL.fSimpleSqlValue(valcodigoVendedor)
      End If
      If valZonaCobranza <> "TODAS" Then
         Sql = Sql & " AND " & "cliente.ZonaDeCobranza" & _
                     " = " & gUtilSQL.fSimpleSqlValue(valZonaCobranza)
      End If
      Sql = Sql & " ORDER BY "
      Sql = Sql & "MonedaReporte, "
      Sql = Sql & "cliente.ZonaDeCobranza, "
      If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Nombre) Then
         Sql = Sql & "cliente.Nombre, "
      Else
         Sql = Sql & "cxC.CodigoCliente, "
      End If
      Sql = Sql & "cxC.FechaVencimiento"
   End If
   fConstruirSQLDelReporteAnalisisDeVencimiento = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteFacturasAnuladasVsCxCVigentes(ByVal valConsecutivoCompania As Long, ByRef insFactura As Object, ByRef gEnumProyecto As Object)
   Dim Sql As String
   On Error GoTo h_ERROR
   insFactura.setClaseDeTrabajo eCTFC_Factura
   Sql = "SELECT "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " AS NumeroFact, "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_FECHA & " AS FechaFact, "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & ", "
   Sql = Sql & gUtilSQL.getIIF(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA _
               & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_ANULADA)), _
               gUtilSQL.getIIF(insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA _
               & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_BORRADOR)), _
               gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumStatusFacturaToString(eSF_EMITIDA)), True), True) & _
               " AS StatusFactura, "
   Sql = Sql & "cxC.Numero AS NumeroCxC, "
   Sql = Sql & "(" & "cxC.MontoExento" & _
               " + " & "cxC.MontoGravado" & _
               " + " & "cxC.MontoIVA) AS TotalCxC, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxC.Status", enum_StatusDocumento.eSD_PORCANCELAR, _
               gEnumProyecto.fEnumStatusCxCToStringInArray(True, False), "StatusCxC")
   Sql = Sql & " FROM " & "cxC INNER JOIN " & insFactura.GetTableName
   Sql = Sql & " ON (" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
               " = " & "cxC.NumeroDocumentoOrigen"
   Sql = Sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & "cxC.ConsecutivoCompania"
   Sql = Sql & ") AND (" & "cxC.TipoCxC" & _
               " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & ")"
   Sql = Sql & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & valConsecutivoCompania
   Sql = Sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA)
   Sql = Sql & " AND " & "cxC.Status" & _
               " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   Sql = Sql & " ORDER BY "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_FECHA & ", "
   Sql = Sql & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
h_EXIT:
   fConstruirSQLDelReporteFacturasAnuladasVsCxCVigentes = Sql
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteFacturasAnuladasVsCxCVigentes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelAnalisisDeCxCAUnaFecha(ByVal valTableName As String, ByVal valnombreParaTabla As String, _
                                 ByVal valFecha As Date, ByVal valOrdenadoPorNombre As Boolean, ByVal valAgruparPorTipo As Boolean, ByVal valAgruparTodos As Boolean, ByVal valConsecutivoCompania As Long, ByVal valTipoDeDocumento As enum_TipoDeTransaccion, ByRef InsAdmPropAnalisisVenc As Object, ByRef gEnumProyecto As Object) As String
   Dim Sql As String
   Dim sqlMtoRestante As String
   Dim sqlDateDiff As String
   Dim sqlMtoTotal As String
   Dim tasaOrig As Boolean
   On Error GoTo h_ERROR
   Sql = ""
   If InsAdmPropAnalisisVenc.fBuscaValoresDeLasPropAnalisisVencActual Then
      sqlMtoRestante = "((" & valTableName & ".MontoExento" & " + " & valTableName & ".MontoGravado" & " + " & valTableName & ".MontoIva" & ") - " & valTableName & ".MontoAbonado)"
      sqlMtoTotal = "(" & valTableName & ".MontoExento" & " + " & valTableName & ".MontoGravado" & " + " & valTableName & ".MontoIva)"
      sqlDateDiff = gUtilSQL.getDateDiff("d", valTableName & ".FechaVencimiento", _
                     gUtilSQL.fDateToSQLValue(valFecha))
      Sql = "SELECT "
      Sql = Sql & "cliente.ZonaDeCobranza, "
      Sql = Sql & valTableName & ".CodigoCliProv AS CodigoCliente, "
      Sql = Sql & "cliente.Nombre, "
      Sql = Sql & valTableName & ".Moneda, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & _
                  " AS MtoNoVencido, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & _
                  sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto1Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & _
                  sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto2Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & _
                  sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & _
                  " AS Mto3Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, _
                  sqlMtoRestante, "0", True) & _
                  " AS Mto4Vto, "
      Sql = Sql & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AS Vto1, "
      Sql = Sql & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AS Vto2, "
      Sql = Sql & InsAdmPropAnalisisVenc.GetTercerVencimiento & " AS Vto3, "
      Sql = Sql & sqlDateDiff & " AS DiasVencidos, "
      Sql = Sql & sqlMtoRestante & " AS MtoRestante, "
      Sql = Sql & valTableName & ".FechaVencimiento, "
      Sql = Sql & valTableName & ".Numero, "
      Sql = Sql & valTableName & ".Fecha, "
      Sql = Sql & sqlMtoTotal & " AS MtoTotal, "
'      SQL = SQL & valTableName & ".Tipo "
       Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum(valTableName & ".Tipo ", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "Tipo")
      Sql = Sql & " FROM " & valTableName & " INNER JOIN "
      Sql = Sql & "cliente ON "
      Sql = Sql & valTableName & ".ConsecutivoCompania" & _
                  " = " & "cliente.ConsecutivoCompania"
      Sql = Sql & " AND "
      Sql = Sql & valTableName & ".CodigoCliProv" & _
                  " = " & "cliente.Codigo"
      Sql = Sql & " WHERE " & valTableName & ".ConsecutivoCompania" & _
                  " = " & valConsecutivoCompania
      Sql = Sql & " AND " & valTableName & ".Status" & _
                  " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ", " & _
                            gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
      Sql = Sql & " AND " & valTableName & ".Idusuario" & _
                  " = " & gUtilSQL.fSimpleSqlValue(valnombreParaTabla)
      Sql = Sql & " AND " & valTableName & ".Idmodulo = " & gUtilSQL.fSimpleSqlValue(CM_MODULO_CxC)
      Sql = Sql & " AND " & valTableName & ".Fecha <= " & gUtilSQL.fDateToSQLValue(valFecha)
      If valAgruparPorTipo Then
         If valAgruparTodos Then
            Sql = Sql & " AND " & valTableName & ".Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(valTipoDeDocumento)
         End If
      End If
      Sql = Sql & " ORDER BY "
      Sql = Sql & valTableName & ".Moneda, "
      Sql = Sql & valTableName & ".Tipo, "
      Sql = Sql & "cliente.ZonaDeCobranza, "
      If valOrdenadoPorNombre Then
         Sql = Sql & "cliente.Nombre, "
      Else
         Sql = Sql & valTableName & ".CodigoCliProv, "
      End If
      Sql = Sql & valTableName & ".FechaVencimiento"
   End If
h_EXIT: On Error GoTo 0
   fSQLDelAnalisisDeCxCAUnaFecha = Sql
   Exit Function
h_ERROR: Sql = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDelReporteAnalisisDeVencimiento", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelReporteAnalisisDeVencimientoEntreFechas(ByVal valAgruparPorVendedor As Boolean, ByVal valAnalisisDeVencimientoPor As String, ByVal valMonedaDeLosReportes As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valNombreMonedaLocal As String, ByVal valConsecutivoCompania As Long, ByRef InsAdmPropAnalisisVenc As Object, ByRef gUltimaTasaDeCambio As Object, ByRef gMonedaLocalActual As Object, ByVal usarFiltroZonaCobranza As Boolean, ByVal FiltroPorTipo As String) As String
   Dim Sql As String
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
      sqlMtoTotal = "(" & "CxC.MontoExento" & " + " & "CxC.MontoGravado" & " + " & "CxC.MontoIVA)"
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
      Sql = "SELECT "
      If usarFiltroZonaCobranza Then
         Sql = Sql & " Cliente.ZonaDeCobranza, "
      End If
      Sql = Sql & sqlMoneda & ", "
      Sql = Sql & sqlMonedaReporte & " AS MonedaReporte, "
      Sql = Sql & gUtilSQL.fMonth(sqlFecha, "") & " AS Mes, "
      Sql = Sql & gUtilSQL.fYear(sqlFecha, "") & " AS Ano, "
      Sql = Sql & "CxC.CodigoCliente, "
      Sql = Sql & "cliente.Nombre AS NombreCliente, "
      Sql = Sql & "CxC.Numero, "
      Sql = Sql & sqlFecha & ", "
      Sql = Sql & "CxC.FechaVencimiento, "
      Sql = Sql & sqlDateDiff & " AS DiasVencidos, "
      Sql = Sql & "CxC.CodigoVendedor, "
      Sql = Sql & "vendedor.Nombre AS NombreVendedor, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " <= 0 ", sqlMtoRestante, "0", True) & " AS MtoNoVencido, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > 0 AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetPrimerVencimiento, sqlMtoRestante, "0", True) & " AS Mto1Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetPrimerVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetSegundoVencimiento, sqlMtoRestante, "0", True) & " AS Mto2Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetSegundoVencimiento & " AND " & sqlDateDiff & " <= " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto3Vto, "
      Sql = Sql & gUtilSQL.getIIF(sqlDateDiff & " > " & InsAdmPropAnalisisVenc.GetTercerVencimiento, sqlMtoRestante, "0", True) & " AS Mto4Vto, "
      Sql = Sql & sqlMtoRestante & " AS MtoTotal"
      Sql = Sql & " FROM " & "vendedor INNER JOIN ("
      Sql = Sql & "cliente INNER JOIN " & "CxC ON ("
      Sql = Sql & "cliente.Codigo = " & "CxC.CodigoCliente"
      Sql = Sql & ") AND (" & "cliente.ConsecutivoCompania = " & "CxC.ConsecutivoCompania"
      Sql = Sql & ")) ON (" & "vendedor.Codigo = " & "CxC.CodigoVendedor"
      Sql = Sql & ") AND (" & "vendedor.ConsecutivoCompania = " & "CxC.ConsecutivoCompania)"
      Sql = Sql & " WHERE " & "CxC.ConsecutivoCompania = " & valConsecutivoCompania
      Sql = Sql & " AND (" & "CxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & _
                  " OR " & "CxC.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ")"
   If usarFiltroZonaCobranza Then
      If FiltroPorTipo <> "TODAS" Then
         Sql = Sql & "AND Cliente.ZonaDeCobranza='" & FiltroPorTipo & "' "
      End If
   End If
   
      Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("CxC.Fecha", valFechaInicial, valFechaFinal)
      Sql = Sql & " ORDER BY "
      If usarFiltroZonaCobranza Then
         Sql = Sql & " Cliente.ZonaDeCobranza, "
      End If
     
      If valAgruparPorVendedor = True Then
         Sql = Sql & "vendedor.Codigo, "
      End If
      Sql = Sql & sqlMoneda & ", "
      Sql = Sql & sqlFecha & ", "
      If valAnalisisDeVencimientoPor = gEnumReport.enumReporteOrdenadoPorToString(enum_ReporteOrdenadoPor.eRO_Nombre) Then
         Sql = Sql & "cliente.Nombre, "
      Else
         Sql = Sql & "CxC.CodigoCliente, "
      End If
      Sql = Sql & "CxC.FechaVencimiento"
   End If
   fSQLDelReporteAnalisisDeVencimientoEntreFechas = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelReporteAnalisisDeVencimientoEntreFechas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLNroComprobanteFiscalEnCXC(ByVal valPorCompania As Boolean, Optional ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vSqlWhere  As String
   On Error GoTo h_ERROR
   
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vSqlWhere = "(" & vSqlWhere & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & ")"
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "factura.STATUSFACTURA", enum_StatusFactura.eSF_EMITIDA)
   
   If (valPorCompania) Then
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "factura.ConsecutivoCompania", valConsecutivoCompania)
   End If
   
  vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
 
  vSql = " UPDATE CxC "
  vSql = vSql & " SET "
  vSql = vSql & " CxC.NumeroComprobanteFiscal = factura.NumeroComprobanteFiscal"
  vSql = vSql & " FROM factura"
  vSql = vSql & " INNER JOIN  CxC ON"
  vSql = vSql & " factura.ConsecutivoCompania =  CxC.ConsecutivoCompania AND "
  vSql = vSql & " factura.Numero = CxC.NumeroDocumentoOrigen AND "
  vSql = vSql & ftipoCXC() & " = Cxc.TipoCxC "
  vSql = vSql & vSqlWhere
  fSQLNroComprobanteFiscalEnCXC = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLNroComprobanteFiscalEnCXC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLNroDeControlEnCXCAsociada(ByVal valPorCompania As Boolean, Optional ByVal valConsecutivoCompania As Long) As String
   Dim vSql As String
   Dim vSqlWhere  As String
   On Error GoTo h_ERROR
   
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA)
   vSqlWhere = "(" & vSqlWhere & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vSqlWhere = vSqlWhere & " OR " & gUtilSQL.fSQLEnumValueWithAnd("", "factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & ")"
   vSqlWhere = gUtilSQL.fSQLEnumValueWithAnd(vSqlWhere, "factura.STATUSFACTURA", enum_StatusFactura.eSF_EMITIDA)
   
   If (valPorCompania) Then
    vSqlWhere = gUtilSQL.fSQLNumberValueWithAnd(vSqlWhere, "factura.ConsecutivoCompania", valConsecutivoCompania)
   End If
   
  vSqlWhere = gUtilSQL.getWhereSQL(vSqlWhere)
 
  vSql = " UPDATE CxC "
  vSql = vSql & " SET "
  vSql = vSql & " CxC.NumeroControl = factura.NumeroControl "
  vSql = vSql & " FROM factura"
  vSql = vSql & " INNER JOIN  CxC ON"
  vSql = vSql & " factura.ConsecutivoCompania =  CxC.ConsecutivoCompania AND "
  vSql = vSql & " factura.Numero = CxC.NumeroDocumentoOrigen AND "
  vSql = vSql & ftipoCXC() & " = Cxc.TipoCxC "
  vSql = vSql & vSqlWhere
 fSQLNroDeControlEnCXCAsociada = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLNroDeControlEnCXCAsociada", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function ftipoCXC() As String
 Dim gEnumProyecto As clsEnumAdministrativo
   Dim vSql As String
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
   vSql = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "")
   Set gEnumProyecto = Nothing
   ftipoCXC = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "ftipoCXC()", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDeComprobanteCxC(ByVal valNumero As String, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   Dim sqlTotal As String
   Dim sqlMontoRestante As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Sql = " SELECT "
   Sql = Sql & "cxC" & ".Numero AS Numero, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxC" & ".TipoCxc", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True), "TipoCxC") & ","
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("cxC" & ".Status", enum_StatusDocumento.eSD_PORCANCELAR, gEnumProyecto.fEnumStatusCxCToStringInArray(True), "Status") & ","
   Sql = Sql & "cxC" & ".Moneda AS Moneda, "
   Sql = Sql & "cxC" & ".CambioAbolivares AS CambioABolivares, "
   Sql = Sql & "cliente" & ".Codigo As CodigoCliente, "
   Sql = Sql & "cliente" & ".Nombre As NombreCliente, "
   Sql = Sql & "vendedor" & ".Codigo As CodigoVendedor, "
   Sql = Sql & "vendedor" & ".Nombre As NombreVendedor, "
   Sql = Sql & "cxC" & ".Fecha AS Fecha, "
   Sql = Sql & "cxC" & ".FechaVencimiento AS FechaVencimiento, "
   Sql = Sql & "cxC" & ".Descripcion AS Descripcion, "
   Sql = Sql & "cxC" & ".CentroDeCostos AS CentroDeCostos, "
   Sql = Sql & "cxC" & ".MontoExento AS MontoExento,"
   Sql = Sql & "cxC" & ".MontoGravado AS MontoGravado,"
   Sql = Sql & "cxC" & ".MontoIva AS MontoIVA,"
   Sql = Sql & "cxC" & ".Numero AS NumeroDocumento, "
   sqlTotal = "(" & "cxC" & ".MontoExento + " & "cxC" & ".MontoGravado + " & "cxC" & ".MontoIva)"
   Sql = Sql & sqlTotal & " AS MontoTotal, "
   Sql = Sql & "cxC" & ".MontoAbonado AS MontoAbonado,"
   sqlMontoRestante = sqlTotal & "-" & "cxC" & ".MontoAbonado"
   Sql = Sql & sqlMontoRestante & " AS RestaPorPagar, "
   Sql = Sql & "cxC" & ".CodigoMoneda AS CodigoMoneda"
   Sql = Sql & " FROM  " & "vendedor" & " INNER JOIN "
   Sql = Sql & "cliente" & " INNER JOIN " & "cxC"
   Sql = Sql & " ON " & "cliente" & ".Codigo = " & "cxC" & ".CodigoCliente "
   Sql = Sql & " AND " & "cliente" & ".ConsecutivoCompania = " & "cxC" & ".ConsecutivoCompania "
   Sql = Sql & " ON " & "vendedor" & ".Codigo = " & "cxC" & ".CodigoVendedor "
   Sql = Sql & " AND " & "vendedor" & ".ConsecutivoCompania = " & "cxC" & ".ConsecutivoCompania "
   Sql = Sql & " WHERE " & "cxC" & ".ConsecutivoCompania = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   Sql = Sql & " AND Numero = " & gUtilSQL.fSimpleSqlValue(valNumero)
   Set gEnumProyecto = Nothing
h_EXIT:   On Error GoTo 0
   fConstruirSQLDeComprobanteCxC = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDeComprobanteCxC", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDeCxCConComprobante(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valConsecutivoCompania As Long, ByVal valComprobanteNavGetTableName As String, ByVal valNoDocOrigenContabilizacion As String, Optional ByVal valConGeneradoPor As Boolean = True) As String
   Dim Sql As String
   Dim GetTableName As String
   On Error GoTo h_ERROR
   GetTableName = "cxC"
   Sql = " SELECT "
   Sql = Sql & GetTableName & ".Fecha AS FechaDocumento, "
   Sql = Sql & GetTableName & ".Descripcion AS Descripcion, "
   Sql = Sql & GetTableName & ".TipoCxc AS Tipo, "
   Sql = Sql & GetTableName & ".Status AS Status, "
   Sql = Sql & GetTableName & ".Numero AS NumeroDocumento, "
   Sql = Sql & valComprobanteNavGetTableName & "." & "GeneradoPor"
   Sql = Sql & " AS TipoDeDocumento, "
   Sql = Sql & valComprobanteNavGetTableName & "." & "Fecha"
   Sql = Sql & " AS FechaCombrobante, "
   Sql = Sql & valComprobanteNavGetTableName & "." & "TotalHaber"
   Sql = Sql & " AS TotalHaber, "
   Sql = Sql & valComprobanteNavGetTableName & "." & "NUMERO"
   Sql = Sql & " AS NumeroComprobante, "
   Sql = Sql & valComprobanteNavGetTableName & "." & "DESCRIPCION"
   Sql = Sql & " AS DescripcionComprobante, "
   Sql = Sql & valComprobanteNavGetTableName & "." & "NoDocumentoOrigen"
   Sql = Sql & " AS NumeroDocumentoOrigen, "
   Sql = Sql & "(" & GetTableName & ".MontoExento + "
   Sql = Sql & GetTableName & ".MontoGravado + "
   Sql = Sql & GetTableName & ".MontoIva) "
   Sql = Sql & " AS TotalDocumento "
   Sql = Sql & " FROM " & GetTableName & ", " & valComprobanteNavGetTableName
   Sql = Sql & " WHERE (" & valNoDocOrigenContabilizacion & " = "
   Sql = Sql & valComprobanteNavGetTableName & "." & "NoDocumentoOrigen"
   Sql = Sql & ") AND " & GetTableName & ".ConsecutivoCompania = "
   Sql = Sql & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
   Sql = Sql & " AND " & GetTableName & ".Origen = '" & gConvert.enumerativoAChar(enum_OrigenFacturacionOManual.eOF_MANUAL) & "'"
   If valConGeneradoPor Then
      Sql = Sql & " AND " & valComprobanteNavGetTableName & "." & "GeneradoPor" & " ='"
      Sql = Sql & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXC) & "'"
      Sql = Sql & " AND " & GetTableName & ".Fecha"
      Sql = Sql & " BETWEEN "
      Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
      Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaFinal)
   Else
      Sql = Sql & " AND " & valComprobanteNavGetTableName & "." & "GeneradoPor" & " <>'"
      Sql = Sql & gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXC) & "'"
      Sql = Sql & " AND " & valComprobanteNavGetTableName & "." & "Fecha"
      Sql = Sql & " BETWEEN "
      Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND "
      Sql = Sql & gUtilSQL.fDateToSQLValue(valFechaFinal)
   End If
   fConstruirSQLDeCxCConComprobante = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fConstruirSQLDeCxCConComprobante = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLDeCxCConComprobante", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSearchCxCPendientes(ByVal valConsecutivoCompania As Long, ByRef refCodigoCliente As String, ByVal valTraerTotales As Boolean, ByVal EnCualquierMoneda As Boolean, ByVal valFecha As Date) As String    'PRIVATE
   Dim Sql As String
   On Error GoTo h_ERROR
   If valTraerTotales Then
      Sql = "SELECT " & "cxC" & ".Numero, "
      Sql = Sql & "((" & "cxC" & ".MontoGravado" & " + " & "cxC" & ".MontoIva" & " + MontoExento" & ") - (" & "cxC" & ".MontoAbonado)) AS MontoRestante, "
      Sql = Sql & "((" & "cxC" & ".MontoGravado" & " + " & "cxC" & ".MontoIva" & ") + (" & "cxC" & ".MontoExento)) AS MontoOriginal, "
      Sql = Sql & "cxC" & ".Moneda, " & "cxC" & ".CodigoVendedor, "
      Sql = Sql & "cxC" & ".SeRetuvoIva," & " 0 AS MontoIvaRetenido, "
      Sql = Sql & "cxC" & ".TipoCxc, "
      Sql = Sql & "cxC" & ".CodigoMoneda,"
      Sql = Sql & "cxC" & ".CambioAbolivares, "
      Sql = Sql & "ISNULL( cxC" & ".FechaLimiteCambioAMonedaLocal, cxC.Fecha) AS  FechaLimiteCambioAMonedaLocal,"
      Sql = Sql & " CASE  WHEN  cxc.origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_FACTURA) & " THEN factura.TotalIVA "
      Sql = Sql & " ELSE cxC.MontoIva  "
      Sql = Sql & " END  AS MontoIva  "
      Sql = Sql & ", ISNULL( factura.Moneda, cxC.Moneda) AS MonedaFactura"
      Sql = Sql & " FROM " & "cxC"
      Sql = Sql & " LEFT JOIN factura ON cxC.NumeroDocumentoOrigen = factura.Numero "
      Sql = Sql & " AND cxC.CodigoCliente = factura.CodigoCliente "
        
      Sql = Sql & " AND cxc.TipoCxc = "
      Sql = Sql & " CASE WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_BOLETADEVENTA)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOASIGNADO) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOASIGNADO)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_BOLETA) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_BOLETADEVENTA)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITOCOMPROBANTEFISCAL)
      Sql = Sql & " WHEN factura.TipoDeDocumento" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA) & " THEN " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTAENTREGA)
      Sql = Sql & " END "
      Sql = Sql & " AND cxC.ConsecutivoCompania = factura.ConsecutivoCompania "
      
      Sql = Sql & " WHERE "
      Sql = Sql & " cxc.ConsecutivoCompania" & " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      Sql = Sql & " AND cxc.CodigoCliente = " & gUtilSQL.fSimpleSqlValue(refCodigoCliente)
      Sql = Sql & " AND Status" & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
      Sql = Sql & " AND (MontoAbonado" & ") <> (MontoExento" & " + MontoGravado" & " + MontoIva)"
      Sql = Sql & " ORDER BY Numero, TipoCxc"
   Else
      Sql = "SELECT "
      Sql = Sql & "Numero"
      Sql = Sql & " FROM " & "cxC" & " WHERE "
      Sql = Sql & "ConsecutivoCompania" & " = " & gUtilSQL.fNumToStrSQL(valConsecutivoCompania)
      Sql = Sql & " AND Status" & " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_PORCANCELAR) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ABONADO) & ")"
      Sql = Sql & " AND CodigoCliente" & " = " & gUtilSQL.fSimpleSqlValue(refCodigoCliente)
      Sql = Sql & " ORDER BY Numero, TipoCxc"
   End If
   fSQLSearchCxCPendientes = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSQLSearchCxCPendientes = ""
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLSearchCxCPendientes", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

