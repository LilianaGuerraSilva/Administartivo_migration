VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBancosSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsBancosSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Bancos"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Private Function fSubQuerySaldoInicial(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date) As String
   Dim sqlSI As String
   On Error GoTo h_ERROR
   sqlSI = "SELECT SUM("
   sqlSI = sqlSI & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), "movimientoBancario.Monto", "0", True)
   sqlSI = sqlSI & ") - SUM("
   sqlSI = sqlSI & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), "movimientoBancario.Monto", "0", True)
   sqlSI = sqlSI & ") FROM movimientoBancario WHERE "
   sqlSI = sqlSI & "movimientoBancario.ConsecutivoCompania" & " = " & valConsecutivoCompania
   sqlSI = sqlSI & " AND movimientoBancario.Fecha" & " < " & gUtilSQL.fDateToSQLValue(valFechaInicial)
   sqlSI = sqlSI & " AND movimientoBancario.CodigoCtaBancaria" & " = cuentaBancaria.Codigo"
   fSubQuerySaldoInicial = sqlSI
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSubQuerySaldoInicial", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLTransaccionesPorCuenta(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valEsElInformeDeTrasaccionesPorBeneficiario As Boolean, _
                        ByVal valEsSistemaAdmInfotax As Boolean, _
                        ByVal valImprimirSoloCuentasActivas As Boolean, _
                        ByVal valCantidadAImprimir As String, _
                        ByVal valNumeroDeCuenta As String, _
                        ByVal valAgruparPorBeneficiario, ByVal valIsSQLDataBase As Boolean, _
                        ByRef gGlobalization As Object) As String
   Dim SQL As String
   Dim insgEnumProyecto As clsEnumAdministrativo
   Dim insgEnumReport As clsEnumReport
   Dim insMovBancSQl As clsMovimientoBancarioSQL
   On Error GoTo h_ERROR
   Set insgEnumProyecto = New clsEnumAdministrativo
   Set insgEnumReport = New clsEnumReport
   Set insMovBancSQl = New clsMovimientoBancarioSQL
   SQL = "SELECT "
   SQL = SQL & "cuentaBancaria.Codigo, "
   SQL = SQL & "cuentaBancaria.NombreCuenta, "
   SQL = SQL & "cuentaBancaria.NumeroCuenta, "
   SQL = SQL & "cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & "(" & fSubQuerySaldoInicial(valConsecutivoCompania, valFechaInicial) & ") AS SaldoInicial, "
   SQL = SQL & "movimientoBancario.Fecha, "
   SQL = SQL & "movimientoBancario.NumeroDocumento, "
   SQL = SQL & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), True) & _
               " AS Concepto, "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento, "
   SQL = SQL & "movimientoBancario.Descripcion, "
   SQL = SQL & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  "movimientoBancario.Monto", "0", True) & _
               " AS MontoIngreso, "
   SQL = SQL & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                  "movimientoBancario.Monto", "0", True) & _
               " AS MontoEgreso, "
   SQL = SQL & "movimientoBancario.NombreOperador"
   
   If gGlobalization.fEsCodigoPeru Then
      Dim insBancoVista As clsBancoVista
      Set insBancoVista = New clsBancoVista
      
      SQL = SQL & ", (SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoCobrado
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & " UNION "
      
      SQL = SQL & " SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoPagado
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & " UNION "
      
      SQL = SQL & " SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXCobranza
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & " UNION "
      
      SQL = SQL & " SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXDebitoBancario
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & " UNION "
      
      SQL = SQL & " SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXPago
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & " UNION "
      
      SQL = SQL & " SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXRestantes
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & " UNION "
      
      SQL = SQL & " SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXUsuario
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & " UNION "
      
      SQL = SQL & " SELECT  RazonSocial"
      SQL = SQL & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXUsuarioSinCheque
      SQL = SQL & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      SQL = SQL & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      SQL = SQL & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      SQL = SQL & ") AS RazonSocial, "
        
      SQL = SQL & "(SELECT Banco.Nombre FROM Banco WHERE  banco.Codigo = cuentaBancaria.CodigoBanco) AS Banco, "
      SQL = SQL & "(SELECT ConceptoBancario.Descripcion FROM ConceptoBancario WHERE conceptoBancario.Codigo = movimientoBancario.CodigoConcepto) AS DescripcionConcepto "
      
      Set insBancoVista = Nothing
   End If
   
   
   If valEsElInformeDeTrasaccionesPorBeneficiario Then
      SQL = SQL & ", " & insMovBancSQl.fSubQueryTransaccionesConBeneficiario(valEsSistemaAdmInfotax, valConsecutivoCompania)
   End If
   SQL = SQL & " FROM cuentaBancaria"
   SQL = SQL & " INNER JOIN movimientoBancario ON "
   SQL = SQL & "(cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria)"
   SQL = SQL & " AND (cuentaBancaria.ConsecutivoCompania = movimientoBancario.ConsecutivoCompania)"
   SQL = SQL & " WHERE cuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", _
                valFechaInicial, valFechaFinal)
   If valImprimirSoloCuentasActivas Then
      SQL = SQL & " AND cuentaBancaria.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND cuentaBancaria.Codigo" & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valNumeroDeCuenta))
   End If
   SQL = SQL & " ORDER BY "
   If valEsElInformeDeTrasaccionesPorBeneficiario Then
      If valAgruparPorBeneficiario And valIsSQLDataBase Then
        SQL = SQL & "beneficiario, "
      End If
   End If
   SQL = SQL & "cuentaBancaria.Codigo, "
   SQL = SQL & "movimientoBancario.Fecha, "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento, "
   SQL = SQL & "movimientoBancario.NumeroDocumento, "
   SQL = SQL & "movimientoBancario.TipoConcepto"
   Set insMovBancSQl = Nothing
   Set insgEnumProyecto = Nothing
   Set insgEnumReport = Nothing
   fSQLTransaccionesPorCuenta = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLTransaccionesPorCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLTransaccionesPorConcepto(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valCantidadAImprimir As String, ByVal valConcepto As String) As String
   Dim SQL As String
   Dim insgEnumProyecto As clsEnumAdministrativo
   Dim insgEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set insgEnumProyecto = New clsEnumAdministrativo
   Set insgEnumReport = New clsEnumReport
   SQL = "SELECT "
   SQL = SQL & "conceptoBancario.Codigo AS CodigoConcepto, "
   SQL = SQL & "conceptoBancario.Descripcion AS DescripcionConcepto,"
   SQL = SQL & gUtilSQL.getIIF("conceptoBancario.Tipo" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), True) & _
                  " AS TipoConcepto, "
   SQL = SQL & "cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & "cuentaBancaria.Codigo AS CodigoCuenta, "
   SQL = SQL & "cuentaBancaria.NombreCuenta, "
   SQL = SQL & "cuentaBancaria.NumeroCuenta, "
   SQL = SQL & "movimientoBancario.Fecha, "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento, "
   SQL = SQL & "movimientoBancario.NumeroDocumento, "
   SQL = SQL & "movimientoBancario.Descripcion AS DescripcionMovimiento, "
   SQL = SQL & "movimientoBancario.Monto"
   SQL = SQL & " FROM conceptoBancario INNER JOIN ("
   SQL = SQL & "cuentaBancaria INNER JOIN movimientoBancario ON ("
   SQL = SQL & "cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria"
   SQL = SQL & ") AND (cuentaBancaria.ConsecutivoCompania" & _
               " = movimientoBancario.ConsecutivoCompania"
   SQL = SQL & ")) ON (conceptoBancario.Codigo" & _
               " = movimientoBancario.CodigoConcepto)"
   SQL = SQL & " WHERE movimientoBancario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", _
               valFechaInicial, valFechaFinal)
   If valCantidadAImprimir = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND conceptoBancario.Codigo = " & gUtilSQL.fSimpleSqlValue(valConcepto)
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "conceptoBancario.Codigo, cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & "movimientoBancario.Fecha, movimientoBancario.NumeroDocumento"
   fSQLTransaccionesPorConcepto = SQL
   Set insgEnumProyecto = Nothing
   Set insgEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLTransaccionesPorConcepto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLTransaccionesPorDia(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, _
                                       ByVal valSQLMontoConvertido As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & "cuentaBancaria.Codigo AS CodigoCuenta, "
   SQL = SQL & "cuentaBancaria.NombreCuenta, "
   SQL = SQL & "cuentaBancaria.NumeroCuenta, "
   SQL = SQL & "conceptoBancario.Codigo AS CodigoConcepto, "
   SQL = SQL & "movimientoBancario.Descripcion AS DescripcionMovimiento, "
   SQL = SQL & gUtilSQL.getIIF("conceptoBancario.Tipo" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  "movimientoBancario.Monto", "0", True) & _
               " AS MontoIngreso, "
   SQL = SQL & gUtilSQL.getIIF("conceptoBancario.Tipo" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                  valSQLMontoConvertido, "0", True) & _
               " AS MontoEgreso, "
   SQL = SQL & "movimientoBancario.NombreOperador"
   SQL = SQL & " FROM cuentaBancaria"
   SQL = SQL & " INNER JOIN (conceptoBancario"
   SQL = SQL & " INNER JOIN movimientoBancario ON "
   SQL = SQL & "conceptoBancario.Codigo" & _
               " = movimientoBancario.CodigoConcepto"
   SQL = SQL & " ) ON ("
   SQL = SQL & "cuentaBancaria.Codigo" & _
               " = movimientoBancario.CodigoCtaBancaria"
   SQL = SQL & ") AND ("
   SQL = SQL & "cuentaBancaria.ConsecutivoCompania" & _
               " = movimientoBancario.ConsecutivoCompania)"
   SQL = SQL & " WHERE movimientoBancario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND movimientoBancario.Fecha" & _
               " = " & gUtilSQL.fDateToSQLValue(valFechaInicial)
   SQL = SQL & " ORDER BY "
   SQL = SQL & "cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & "cuentaBancaria.Codigo, "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento"
   fSQLTransaccionesPorDia = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLTransaccionesPorDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFlujoDeCajaHistorico(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, _
                                       ByVal valFechaFinal As Date, ByVal valSQLMontoConvertido As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT MovimientoBancario.CodigoCtaBancaria as CodigoCuenta,  " _
               & valSQLMontoConvertido & " AS MontoIE,  " _
               & " MovimientoBancario.ConsecutivoMovimiento as ConsecutivoMov, " _
               & " MovimientoBancario.Fecha as FechaMovimiento,  " _
               & " MovimientoBancario.NumeroDocumento as NDoc,  " _
               & " MovimientoBancario.NombreOperador as NombreOperadorMovimiento,  " _
               & " MovimientoBancario.TipoConcepto as TipoDeConcepto,  " _
               & " MovimientoBancario.Descripcion as DescripcionDelMov,  " _
               & " CuentaBancaria.codigo as CodigoCuentaBan,  " _
               & " CuentaBancaria.TipoCtaBancaria  AS TipoCtaBan,  " _
               & " CuentaBancaria.NombreCuenta as NomCta, " _
               & " CuentaBancaria.NumeroCuenta as NumCta,  " _
               & " CuentaBancaria.NombreDeLaMoneda as NombreMoneda,  " _
               & " Moneda.Simbolo,  " _
               & " ConceptoBancario.Codigo as CodigoConcepto, " _
               & " ConceptoBancario.Descripcion as DescripcionDeConcepto "
   SQL = SQL & " FROM MovimientoBancario, CuentaBancaria, ConceptoBancario, Moneda "
   SQL = SQL & " WHERE MovimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo " _
               & " AND MovimientoBancario.CodigoConcepto = ConceptoBancario.Codigo " _
               & " AND CuentaBancaria.NombreDeLaMoneda = Moneda.Nombre " _
               & " AND MovimientoBancario.Fecha BETWEEN " & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND " & gUtilSQL.fDateToSQLValue(valFechaFinal) _
               & " AND CuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompania _
               & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania _
               & " ORDER BY CuentaBancaria.NombreDeLaMoneda, MovimientoBancario.TipoConcepto,ConceptoBancario.Codigo"
   fSQLFlujoDeCajaHistorico = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLFlujoDeCajaHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeclaracionITF(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal CodigoCuenta As String, ByVal ConsecutivoCompania As String, ByVal ImprimirSoloCuentasActivas As Boolean, _
                                    ByVal valCantidadAImprimir As Boolean) As String
   Dim SQL As String
   Dim ComparativoFecha As String
   Dim insViews As clsBancoVista
   On Error GoTo h_ERROR
   Set insViews = New clsBancoVista
   ComparativoFecha = gUtilSQL.fSQLValueBetween("FECHA", valFechaInicial, valFechaFinal)
   SQL = ""
   SQL = "SELECT " & gUtilSQL.fSimpleSqlValue("Efectivo") & " as Concepto, "
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "Monto", "0") & ") as MontoEfectivo, "
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("generadoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "Monto", "0") & ") as MontoITF, "
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") as DocumentosEfectivo, "
   SQL = SQL & "NombreCuenta "
   SQL = SQL & "FROM " & insViews.GetViewNameUnionMovBCtaB
   SQL = SQL & " WHERE " & insViews.GetViewNameUnionMovBCtaB & ".ConsecutivoCompania" & _
                     " = " & ConsecutivoCompania & " AND " & ComparativoFecha & " and monto > 0 "
   If valCantidadAImprimir Then
      SQL = SQL & "AND CodigoCtaBancaria = " & gUtilSQL.fSimpleSqlValue(CodigoCuenta)
   End If
      If ImprimirSoloCuentasActivas Then
      SQL = SQL & " AND Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   SQL = SQL & " GROUP BY NombreCuenta"
   fSQLDeclaracionITF = SQL
   Set insViews = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDeclaracionITF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLTransaccionesPorConceptoYCuentaBancaria(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valCantidadAImprimirConcepto As String, ByVal valConcepto As String, _
                        ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimirCuenta As String, _
                        ByVal valNumeroDeCuenta As String) As String
   Dim SQL As String
   Dim insgEnumProyecto As clsEnumAdministrativo
   Dim insgEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set insgEnumProyecto = New clsEnumAdministrativo
   Set insgEnumReport = New clsEnumReport
   SQL = "SELECT "
   SQL = SQL & "conceptoBancario.Codigo AS CodigoConcepto, "
   SQL = SQL & "conceptoBancario.Descripcion AS DescripcionConcepto,"
   SQL = SQL & gUtilSQL.getIIF("conceptoBancario.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), True) & " AS TipoConcepto, "
   SQL = SQL & "cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & "cuentaBancaria.Codigo AS CodigoCuenta, "
   SQL = SQL & "cuentaBancaria.NombreCuenta, "
   SQL = SQL & "cuentaBancaria.NumeroCuenta, "
   SQL = SQL & "movimientoBancario.Fecha, "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento, "
   SQL = SQL & "movimientoBancario.NumeroDocumento, "
   SQL = SQL & "movimientoBancario.Descripcion AS DescripcionMovimiento, "
   SQL = SQL & "movimientoBancario.Monto"
   SQL = SQL & " FROM conceptoBancario INNER JOIN "
   SQL = SQL & "(cuentaBancaria INNER JOIN movimientoBancario "
   SQL = SQL & " ON (cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria)"
   SQL = SQL & " AND (cuentaBancaria.ConsecutivoCompania = movimientoBancario.ConsecutivoCompania))"
   SQL = SQL & " ON conceptoBancario.Codigo = movimientoBancario.CodigoConcepto"
   SQL = SQL & " WHERE movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimirConcepto = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND conceptoBancario.Codigo = " & gUtilSQL.fSimpleSqlValue(valConcepto)
   End If
   If valCantidadAImprimirCuenta = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valNumeroDeCuenta))
   End If
   If valImprimirSoloCuentasActivas Then
      SQL = SQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "conceptoBancario.Codigo, cuentaBancaria.Codigo, cuentaBancaria.NombreDeLaMoneda, "
   SQL = SQL & "movimientoBancario.Fecha, movimientoBancario.NumeroDocumento"
   fSQLTransaccionesPorConceptoYCuentaBancaria = SQL
   Set insgEnumProyecto = Nothing
   Set insgEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLTransaccionesPorConceptoYCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDeclaracionITFGO6687(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCodigoCuenta As String, ByVal valConsecutivoCompania As String, ByVal valImprimirSoloCuentasActivas As Boolean, _
                                    ByVal valCantidadAImprimir As Boolean, ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valCodigoConceptoDebitoBancario As String, ByVal valConceptoBancarioReversoDePago As String) As String
   Dim SQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   SQL = fSqlCTEDeclaracionIGTFGeneraMovBan(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta, valCodigoConceptoDebitoBancario, valConceptoBancarioReversoDePago)
   SQL = SQL & fSqlDeclaracionIGTFGeneraMovBan_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania)
   SQL = SQL & " UNION "
   SQL = SQL & fSqlDeclaracionIGTFNoGeneraMovBan_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania)
   SQL = SQL & " UNION "
   SQL = SQL & fSqlDeclaracionIGTFFactura_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   SQL = SQL & " UNION "
   SQL = SQL & fSqlDeclaracionIGTF_RDV_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   SQL = SQL & " UNION "
   SQL = SQL & fSqlDeclaracionIGTFFactura_NC_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   SQL = SQL & " UNION "
   SQL = SQL & fSqlDeclaracionIGTFFactura_ND_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   SQL = SQL & " ORDER BY TipoDeAlicuotaPorContribuyenteStr, AlicuotaImpBancario, NombreCuenta, CodigoMoneda, Fecha "
   fSQLDeclaracionITFGO6687 = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeclaracionITFGO6687", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBan(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String, ByVal valCodigoConceptoDebitoBancario As String, ByVal valConceptoBancarioReversoDePago As String) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = ";WITH CTE_DeclaracionIGTFGeneraMovBan AS ( "
   vSQL = vSQL & fSqlCTEDeclaracionIGTFGeneraMovBanMB(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta, valConceptoBancarioReversoDePago)
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlCTEDeclaracionIGTFGeneraMovBanPago(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlCTEDeclaracionIGTFGeneraMovBanCobranza(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlCTEDeclaracionIGTFGeneraMovBanRendicion(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta, valCodigoConceptoDebitoBancario)
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSQL = vSQL & " ) "
   fSqlCTEDeclaracionIGTFGeneraMovBan = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBan", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBanMB(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSQL = vSQL & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSQL = vSQL & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSQL = vSQL & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSQL = vSQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSQL = vSQL & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanMB = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanMB", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String, ByVal valConceptoBancarioReversoDePago As String) As String
   Dim vSQL As String
   Dim vSqlSubSelect As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   vSqlSubSelect = " SELECT MB.NumeroDocumento "
   vSqlSubSelect = vSqlSubSelect & " FROM MovimientoBancario AS MB "
   vSqlSubSelect = vSqlSubSelect & " WHERE " & gUtilSQL.fSQLValueBetween("MB.Fecha", valFechaInicial, valFechaFinal)
   vSqlSubSelect = vSqlSubSelect & " AND MB.ConsecutivoCompania = MovimientoBancario.ConsecutivoCompania AND MB.CodigoCtaBancaria = MovimientoBancario.CodigoCtaBancaria "
   vSqlSubSelect = vSqlSubSelect & " AND MB.CodigoConcepto = " & gUtilSQL.fSimpleSqlValue(valConceptoBancarioReversoDePago)
   
   vSQL = "SELECT MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSQL = vSQL & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSQL = vSQL & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSQL = vSQL & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSQL = vSQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSQL = vSQL & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   vSQL = vSQL & " AND MovimientoBancario.NumeroDocumento NOT IN (" & vSqlSubSelect & ") "
   fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBanPago(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSQL = vSQL & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSQL = vSQL & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSQL = vSQL & "INNER JOIN Pago ON MovimientoBancario.ConsecutivoCompania = Pago.ConsecutivoCompania "
   vSQL = vSQL & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(Pago.NumeroComprobante AS varchar) AND MovimientoBancario.Fecha = Pago.Fecha "
   vSQL = vSQL & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSQL = vSQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSQL = vSQL & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND Pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ORDEN_DE_PAGO)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanPago = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanPago", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBanCobranza(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSQL = vSQL & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSQL = vSQL & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSQL = vSQL & "INNER JOIN Cobranza ON MovimientoBancario.ConsecutivoCompania = Cobranza.ConsecutivoCompania "
   vSQL = vSQL & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(Cobranza.Numero AS varchar) AND MovimientoBancario.Fecha = Cobranza.Fecha "
   vSQL = vSQL & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSQL = vSQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSQL = vSQL & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_COBRANZA)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanCobranza = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanCobranza", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSQL = vSQL & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSQL = vSQL & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSQL = vSQL & "INNER JOIN anticipo ON MovimientoBancario.ConsecutivoCompania = anticipo.ConsecutivoCompania "
   vSQL = vSQL & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(anticipo.ConsecutivoAnticipo AS varchar) AND MovimientoBancario.Fecha = anticipo.Fecha "
   vSQL = vSQL & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSQL = vSQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSQL = vSQL & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ANTICIPO)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBanRendicion(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String, ByVal valCodigoConceptoDebitoBancario As String) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.CodigoConcepto = " & gUtilSQL.fSimpleSqlValue(valCodigoConceptoDebitoBancario), gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO), "MovimientoBancario.GeneradoPor", True) & " AS GeneradoPor, "
   vSQL = vSQL & " MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSQL = vSQL & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSQL = vSQL & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSQL = vSQL & "INNER JOIN Adm.Rendicion ON MovimientoBancario.ConsecutivoCompania = Adm.Rendicion.ConsecutivoCompania "
   vSQL = vSQL & "AND CAST(MovimientoBancario.NumeroDocumento AS varchar) = CAST(Adm.Rendicion.NumeroDocumento AS varchar) AND MovimientoBancario.Fecha = Adm.Rendicion.FechaCierre "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria = Adm.Rendicion.CodigoCuentaBancaria "
   vSQL = vSQL & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSQL = vSQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSQL = vSQL & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND Adm.Rendicion.StatusRendicion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_CERRADA)
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_REPOSICION_DE_CAJACHICA)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanRendicion = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanRendicion", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & " MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSQL = vSQL & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSQL = vSQL & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSQL = vSQL & "INNER JOIN Saw.SolicitudesDePago ON MovimientoBancario.ConsecutivoCompania = Saw.SolicitudesDePago.ConsecutivoCompania "
   vSQL = vSQL & "INNER JOIN Saw.RenglonSolicitudesDePago ON Saw.SolicitudesDePago.ConsecutivoCompania = Saw.RenglonSolicitudesDePago.ConsecutivoCompania "
   vSQL = vSQL & "AND Saw.SolicitudesDePago.ConsecutivoSolicitud = Saw.RenglonSolicitudesDePago.ConsecutivoSolicitud  "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria = Saw.RenglonSolicitudesDePago.CuentaBancaria "
   vSQL = vSQL & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(Saw.RenglonSolicitudesDePago.ConsecutivoSolicitud AS varchar) + CAST(Saw.RenglonSolicitudesDePago.ConsecutivoRenglon AS varchar) "
   vSQL = vSQL & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND MovimientoBancario.Monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSQL = vSQL & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSQL = vSQL & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND (Saw.SolicitudesDePago.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusSolicitud.eSS_ParcialmenteProcesada)
   vSQL = vSQL & " OR Saw.SolicitudesDePago.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusSolicitud.eSS_Procesada) & " ) "
   vSQL = vSQL & " AND Saw.RenglonSolicitudesDePago.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusSolicitudRenglon.eSSR_Procesada)
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_SOLICITUD_DE_PAGO)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlDeclaracionIGTFGeneraMovBan_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(False) & " OR GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fRoundNDecimales(" Monto * CambioAbolivares ", 2)) & ") AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), gUtilSQL.fRoundNDecimales(" Monto * CambioAbolivares ", 2), "0") & ") AS MontoITF, "
   Else
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), gUtilSQL.fSQLTruncaA2Decimales("Monto * CambioAbolivares"), "0") & ") AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), gUtilSQL.fSQLTruncaA2Decimales("Monto * CambioAbolivares"), "0") & ") AS MontoITF, "
   End If
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") AS ParaContar, "
   SQL = SQL & "AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr "
   SQL = SQL & " FROM CTE_DeclaracionIGTFGeneraMovBan "
   SQL = SQL & "WHERE GeneraMovBancarioPorIGTFStr = 'Genera IGTF'"
   SQL = SQL & " GROUP BY TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, Fecha, AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr"
   SQL = SQL & " HAVING SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") > 0 "
  
   fSqlDeclaracionIGTFGeneraMovBan_SQL = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFGeneraMovBan_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTFNoGeneraMovBan_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(False) & " OR GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fRoundNDecimales(" Monto * CambioAbolivares ", 2)) & ") AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fRoundNDecimales(" ((Monto * AlicuotaImpBancario)/100) * CambioAbolivares ", 2)) & ") AS MontoITF, "
   Else
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), gUtilSQL.fSQLTruncaA2Decimales(" Monto * CambioAbolivares "), "0") & ") AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fSQLTruncaA2Decimales(" ((Monto * AlicuotaImpBancario)/100) * CambioAbolivares ")) & ") AS MontoITF, "
   End If
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") AS ParaContar, "
   SQL = SQL & "AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr"
   SQL = SQL & " FROM CTE_DeclaracionIGTFGeneraMovBan "
   SQL = SQL & "WHERE GeneraMovBancarioPorIGTFStr = 'No Genera IGTF'"
   SQL = SQL & " GROUP BY TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, fecha, AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr"
   SQL = SQL & " HAVING SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") > 0 "
   
   fSqlDeclaracionIGTFNoGeneraMovBan_SQL = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFNoGeneraMovBan_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTFFactura_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Facturas Cobradas en Divisas' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   SQL = SQL & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   SQL = SQL & " FROM factura "
   SQL = SQL & " WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
   SQL = SQL & " AND factura.BaseImponibleIGTF <> 0 AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTFFactura_SQL = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFFactura_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlDeclaracionIGTF_RDV_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Resumen Diario de Ventas (manual)' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   SQL = SQL & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   SQL = SQL & " FROM factura "
   SQL = SQL & "WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & "  AND factura.RealizoCierreZ = " & gUtilSQL.fBooleanToSqlValue(False) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   SQL = SQL & " AND factura.BaseImponibleIGTF <> 0 AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTF_RDV_SQL = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTF_RDV_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlDeclaracionIGTFFactura_NC_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Ajuste por IGTF (Notas de Crédito)' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   SQL = SQL & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   SQL = SQL & " FROM factura "
   SQL = SQL & " WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   SQL = SQL & " AND factura.BaseImponibleIGTF <> 0 AND factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF) & " "
   SQL = SQL & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTFFactura_NC_SQL = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFFactura_NC_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlDeclaracionIGTFFactura_ND_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Ajuste por IGTF (Notas de Débito)' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      SQL = SQL & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   SQL = SQL & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   SQL = SQL & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   SQL = SQL & " FROM factura "
   SQL = SQL & " WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   SQL = SQL & " AND factura.BaseImponibleIGTF <> 0 AND factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF) & ""
   SQL = SQL & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   SQL = SQL & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTFFactura_ND_SQL = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFFactura_ND_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
