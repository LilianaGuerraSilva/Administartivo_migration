VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsConciliacionSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsConciliacionSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Conciliación"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function fSQLMovimientosDeConciliacion(ByVal valNroConciliacion As String, ByVal valConsecutivoCompanias As Long, ByRef insConciliacion As Object, ByRef gEnumProyecto As Object, ByRef gMonedaLocalActual As Object) As String
   Dim SQL As String
   Dim SqlStatus As String
   On Error GoTo h_ERROR
   SQL = ""
   SqlStatus = ""
   SqlStatus = " , " & gUtilSQL.DfSQLCaseIfForEnum("conciliacion.Status", _
          enum_StatusConciliacion.eSC_ENPROCESO, gEnumProyecto.fenumStatusConciliacionToStringInArray(True), "Status")
   SQL = "SELECT " & "conciliacion.MesDeAplicacion, "
   SQL = SQL & "conciliacion.AnoDeAplicacion, "
   SQL = SQL & "conciliacion.CodigoCuenta, "
   SQL = SQL & "movimientoBancario.NumeroDocumento, "
   SQL = SQL & "movimientoBancario.Fecha, "
   SQL = SQL & "movimientoBancario.Descripcion, "
   SQL = SQL & "movimientoBancario.CodigoCtaBancaria, "
   SQL = SQL & "movimientoBancario.NroConciliacion, "
   SQL = SQL & "movimientoBancario.TipoConcepto, "
   If Not insConciliacion.GetAnoDeAplicacion >= gUtilDate.fYear(gMonedaLocalActual.GetHoyVigenteDesde) Then
      SQL = SQL & gUtilSQL.getIIF("movimientoBancario.TipoConcepto = '0'", "movimientoBancario.Monto", "0") & " AS MontoEgreso, "
      SQL = SQL & gUtilSQL.getIIF("movimientoBancario.TipoConcepto = '1'", "movimientoBancario.Monto", "0") & " AS MontoIngreso, "
   Else
      SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), gUtilSQL.getIIF("movimientoBancario.TipoConcepto = '1'", "movimientoBancario.Monto", "0"), "movimientoBancario.Fecha") & "  AS MontoIngreso, "
      SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.getFechaDeHoy), gUtilSQL.getIIF("movimientoBancario.TipoConcepto = '0'", "movimientoBancario.Monto", "0"), "movimientoBancario.Fecha") & "  AS MontoEgreso, "
   End If
   SQL = SQL & gUtilSQL.getIIF("movimientoBancario.ConciliadoSN = 'S'", "-1", "0") & " AS ConciliadoSN,  "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento, "
   SQL = SQL & "movimientoBancario.CodigoConcepto, "
   SQL = SQL & "cuentaBancaria.Codigo, "
   SQL = SQL & "cuentaBancaria.ConsecutivoCompania"
   SQL = SQL & SqlStatus
   SQL = SQL & " FROM (" & "cuentaBancaria INNER JOIN " & "movimientoBancario ON ("
   SQL = SQL & "cuentaBancaria.Codigo"
   SQL = SQL & " = " & "movimientoBancario.CodigoCtaBancaria) AND ("
   SQL = SQL & "cuentaBancaria.ConsecutivoCompania = "
   SQL = SQL & "movimientoBancario.ConsecutivoCompania)) INNER JOIN " & "conciliacion ON ("
   SQL = SQL & "cuentaBancaria.Codigo = "
   SQL = SQL & "conciliacion.CodigoCuenta) AND ( "
   SQL = SQL & "cuentaBancaria.ConsecutivoCompania = "
   SQL = SQL & "conciliacion.ConsecutivoCompania) "
   SQL = SQL & " WHERE "
   SQL = SQL & "conciliacion.NroConciliacion = " & gUtilSQL.fSimpleSqlValue(valNroConciliacion) & " AND "
   SQL = SQL & "movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompanias & " AND "
   SQL = SQL & "movimientoBancario.NroConciliacion = " & gUtilSQL.fSimpleSqlValue(valNroConciliacion) & " AND "
   SQL = SQL & "movimientoBancario.CodigoCtaBancaria = CodigoCuenta AND "
   SQL = SQL & "cuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompanias & " AND "
   SQL = SQL & "cuentaBancaria.Codigo = CodigoCuenta"
   SQL = SQL & " ORDER BY " & "movimientoBancario.Fecha,"
   SQL = SQL & "movimientoBancario.NumeroDocumento, "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento"
   fSQLMovimientosDeConciliacion = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMovimientosDeConciliacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLMovimientosBancariosConciliadosONo(ByVal valNroConciliacion As String, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valTipoMovimiento As String, ByVal valConsecutivoCompanias As Long, ByRef gEnumProyecto As Object) As String
   Dim SQL As String
   Dim SqlStatus As String
   
   Dim SqlSelectStatus As String
   Dim SqlStatusSiEsConciliado As String
   On Error GoTo h_ERROR
    SqlStatus = gUtilSQL.DfSQLCaseIfForEnum("conciliacion.Status", _
          enum_StatusConciliacion.eSC_ENPROCESO, gEnumProyecto.fenumStatusConciliacionToStringInArray(True), "")
   SqlSelectStatus = "(SELECT " & SqlStatus & " FROM conciliacion WHERE " & "conciliacion.NroConciliacion" & _
                     " = " & "movimientoBancario.NroConciliacion" & _
                     " AND " & "conciliacion.ConsecutivoCompania" & _
                     " = " & "movimientoBancario.ConsecutivoCompania)"
   SqlStatusSiEsConciliado = ""
   SqlStatusSiEsConciliado = ", " & gUtilSQL.getIIF("MovimientoBancario.ConciliadoSN = 'S'", SqlSelectStatus, "''") & " AS Status"
   SQL = "SELECT " _
         & "cuentaBancaria.Codigo, " _
         & "cuentaBancaria.NumeroCuenta, " _
         & "cuentaBancaria.NombreCuenta, " _
         & "CodigoCtaBancaria AS CodigoCuenta, " _
         & "Fecha, " _
         & "NumeroDocumento AS NDoc, " _
         & "CodigoConcepto, " _
         & "ConsecutivoMovimiento AS ConsecutivoMov, " _
         & "Descripcion, " _
         & "movimientoBancario.NroConciliacion AS NumConciliacion, " _
         & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = '0'", "movimientoBancario.Monto", "0") & " AS MontoIngreso, " _
         & "0 as MontoEgreso, " _
         & gUtilSQL.getIIF("MovimientoBancario.ConciliadoSN = 'S'", "-1", "0") & " AS ConciliadoSN, " _
         & "cuentaBancaria.ConsecutivoCompania" & _
            SqlStatusSiEsConciliado & _
          " FROM " & "movimientoBancario, " & _
         "cuentaBancaria, " & "conciliacion WHERE " & _
         "cuentaBancaria.Codigo = '" & Trim(valNroConciliacion) & "' AND " & _
         "movimientoBancario.CodigoCtaBancaria = '" & Trim(valNroConciliacion) & "' AND " & _
         "cuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompanias & " AND " & _
         "movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompanias & " AND " & _
         "Fecha BETWEEN " & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND " & gUtilSQL.fDateToSQLValue(valFechaFinal) & " AND " & _
         "movimientoBancario.NumeroDocumento <> 'AJUSTE_REC' and " & _
         "movimientoBancario.TipoConcepto = '0'"
'         "cuentaBancaria.ConsecutivoCompania = " & "movimientoBancario.ConsecutivoCompania" & _
'         "cuentaBancaria.Codigo = '" & Trim(valNroConciliacion) & "' "
      If valTipoMovimiento = "Conciliados" Then
         SQL = SQL & " AND MovimientoBancario.ConciliadoSN = 'S'"
      ElseIf valTipoMovimiento = "No Conciliados" Then
         SQL = SQL & " AND MovimientoBancario.ConciliadoSN = 'N'"
      End If
   SQL = SQL & " UNION (SELECT " _
         & "cuentaBancaria.Codigo, " _
         & "cuentaBancaria.NumeroCuenta, " _
         & "cuentaBancaria.NombreCuenta, " _
         & "CodigoCtaBancaria AS CodigoCuenta, " _
         & "Fecha, " _
         & "NumeroDocumento AS NDoc, " _
         & "CodigoConcepto, " _
         & "ConsecutivoMovimiento AS ConsecutivoMov, " _
         & "Descripcion, " _
         & "movimientoBancario.NroConciliacion AS NumConciliacion, 0, " _
         & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = '1'", "movimientoBancario.Monto", "0") & ", " _
         & gUtilSQL.getIIF("MovimientoBancario.ConciliadoSN = 'S'", "-1", "0") & " AS ConciliadoSN, " _
         & "cuentaBancaria.ConsecutivoCompania" & _
               SqlStatusSiEsConciliado & _
         " FROM " & "movimientoBancario, " & _
         "cuentaBancaria, " & "conciliacion WHERE " & _
         "cuentaBancaria.Codigo = '" & Trim(valNroConciliacion) & "' AND " & _
         "movimientoBancario.CodigoCtaBancaria = '" & Trim(valNroConciliacion) & "' AND " & _
         "cuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompanias & " AND " & _
         "movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompanias & " AND " & _
         "Fecha BETWEEN " & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND " & gUtilSQL.fDateToSQLValue(valFechaFinal) & " AND " & _
         "movimientoBancario.NumeroDocumento <> 'AJUSTE_REC' AND " & _
         "movimientoBancario.TipoConcepto = '1'"
'         "cuentaBancaria.Codigo = '" & Trim(valNroConciliacion) & "' "
      If valTipoMovimiento = "Conciliados" Then
         SQL = SQL & " AND MovimientoBancario.ConciliadoSN = 'S'"
      ElseIf valTipoMovimiento = "No Conciliados" Then
         SQL = SQL & " AND MovimientoBancario.ConciliadoSN = 'N'"
      End If
      SQL = SQL & ") ORDER BY CuentaBancaria.Codigo, MovimientoBancario.Fecha "
   fSQLMovimientosBancariosConciliadosONo = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMovimientosBancariosConciliadosONo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSaldoBancario(ByVal valConsecutivoCompania As Long, ByVal valFechaFinal As Date, ByVal valCodigoCuenta As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "cuentaBancaria.Codigo, "
   SQL = SQL & "(" & "SELECT SUM("
   SQL = SQL & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  "movimientoBancario.Monto", "0", True)
   SQL = SQL & ") - SUM(" & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                  "movimientoBancario.Monto", "0", True)
   SQL = SQL & ") FROM movimientoBancario"
   SQL = SQL & " WHERE movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND movimientoBancario.Fecha <= " & gUtilSQL.fDateToSQLValue(valFechaFinal)
   SQL = SQL & " AND movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo) AS SaldoFinal "
   SQL = SQL & " FROM cuentaBancaria LEFT JOIN "
   SQL = SQL & " movimientoBancario ON "
   SQL = SQL & " cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria"
   SQL = SQL & " AND cuentaBancaria.ConsecutivoCompania = movimientoBancario.ConsecutivoCompania"
   SQL = SQL & " WHERE cuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valCodigoCuenta))
   SQL = SQL & " GROUP BY "
   SQL = SQL & " cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & " cuentaBancaria.Codigo "
h_EXIT: On Error GoTo 0
   fSQLSaldoBancario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLSaldoBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLDelReporteConciliacionDatosResumidos(ByVal valConsecutivoCompania As Long, _
               ByVal valNroConciliacion As String, ByRef gEnumProyecto As Object, _
               ByVal valMes As String, ByVal valAno As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR

   SQL = "SELECT Conciliacion.NroConciliacion,"
   SQL = SQL & " Conciliacion.SaldoEdoCuentaBancario,"
   SQL = SQL & " CuentaBancaria.Codigo,"
   SQL = SQL & " CuentaBancaria.NumeroCuenta,"
   SQL = SQL & " CuentaBancaria.NombreCuenta,"
   SQL = SQL & " CuentaBancaria.NombreDeLaMoneda,"
   SQL = SQL & " Moneda.Simbolo AS SimboloDeLaMoneda,"
   SQL = SQL & " Conciliacion.MesDeAplicacion AS mes,"
   SQL = SQL & " Conciliacion.AnoDeAplicacion AS Ano,"
   SQL = SQL & " SUM" & gUtilSQL.getIIF("DetalleDeConciliacion.TipoConcepto" & _
                  " = " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
                  "DetalleDeConciliacion.Monto", "0", True) & " AS Ingreso,"
   SQL = SQL & " SUM" & gUtilSQL.getIIF("DetalleDeConciliacion.TipoConcepto" & _
                  " = " & gUtilSQL.fSimpleSqlValue(gEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), _
                  "DetalleDeConciliacion.Monto", "0", True) & " AS Egreso, "
                  
   SQL = SQL & "(" & "SELECT SUM("
   SQL = SQL & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  "movimientoBancario.Monto", "0", True)
   SQL = SQL & ") - SUM(" & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                  "movimientoBancario.Monto", "0", True)
   SQL = SQL & ") FROM movimientoBancario"
   SQL = SQL & " WHERE movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   'sql = sql & " AND " & gUtilSQL.fSQLMonthAndYearValueBetween(gUtilSQL.fMonth("movimientobancario.Fecha", ""), gUtilSQL.fYear("movimientobancario.fecha", ""), "01", "1996", valMesFinal, valAnoFinal)
   SQL = SQL & " AND movimientobancario.Fecha <= " & gUtilSQL.fDateToSQLValue(gUtilDate.fColocaUltimoDiaDelMes(DateSerial(gConvert.ConvierteAInteger(valAno), gConvert.ConvierteAInteger(valMes), 1)))
   SQL = SQL & " AND movimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo) AS SaldoFinal "
   
   SQL = SQL & " FROM Conciliacion INNER JOIN CuentaBancaria"
   SQL = SQL & " ON (Conciliacion.CodigoCuenta = CuentaBancaria.Codigo"
   SQL = SQL & " AND Conciliacion.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania)"
   SQL = SQL & " LEFT JOIN DetalleDeConciliacion"
   SQL = SQL & " ON (Conciliacion.NroConciliacion = DetalleDeConciliacion.NroConciliacion"
   SQL = SQL & " AND CuentaBancaria.Codigo =  DetalleDeConciliacion.CodigoCuenta"
   SQL = SQL & " AND Conciliacion.ConsecutivoCompania = DetalleDeConciliacion.ConsecutivoCompania)"
   SQL = SQL & " INNER JOIN Moneda"
   SQL = SQL & " ON (Moneda.Codigo = CuentaBancaria.CodigoMoneda)"
   SQL = SQL & " WHERE Conciliacion.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND Conciliacion.NroConciliacion = " & gUtilSQL.fSimpleSqlValue(Trim(valNroConciliacion))
   SQL = SQL & " AND Conciliacion.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusConciliacion.eSC_CERRADA)
   SQL = SQL & " GROUP BY Conciliacion.NroConciliacion,"
   SQL = SQL & " Conciliacion.SaldoEdoCuentaBancario,"
   SQL = SQL & " CuentaBancaria.Codigo,"
   SQL = SQL & " CuentaBancaria.NumeroCuenta,"
   SQL = SQL & " CuentaBancaria.NombreCuenta,"
   SQL = SQL & " Conciliacion.MesDeAplicacion,"
   SQL = SQL & " Conciliacion.AnoDeAplicacion,"
   SQL = SQL & " DetalleDeConciliacion.TipoConcepto,"
   SQL = SQL & " CuentaBancaria.NombreDeLaMoneda,"
   SQL = SQL & " Moneda.Simbolo"
   
h_EXIT: On Error GoTo 0
   fConstruirSQLDelReporteConciliacionDatosResumidos = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fConstruirSQLDelReporteConciliacionDatosResumidos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
