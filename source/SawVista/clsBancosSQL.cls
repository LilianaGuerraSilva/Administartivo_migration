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
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSubQuerySaldoInicial", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
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
   Dim Sql As String
   Dim insgEnumProyecto As clsEnumAdministrativo
   Dim insgEnumReport As clsEnumReport
   Dim insMovBancSQl As clsMovimientoBancarioSQL
   On Error GoTo h_ERROR
   Set insgEnumProyecto = New clsEnumAdministrativo
   Set insgEnumReport = New clsEnumReport
   Set insMovBancSQl = New clsMovimientoBancarioSQL
   Sql = "SELECT "
   Sql = Sql & "cuentaBancaria.Codigo, "
   Sql = Sql & "cuentaBancaria.NombreCuenta, "
   Sql = Sql & "cuentaBancaria.NumeroCuenta, "
   Sql = Sql & "cuentaBancaria.NombreDeLaMoneda, "
   Sql = Sql & "(" & fSubQuerySaldoInicial(valConsecutivoCompania, valFechaInicial) & ") AS SaldoInicial, "
   Sql = Sql & "movimientoBancario.Fecha, "
   Sql = Sql & "movimientoBancario.NumeroDocumento, "
   Sql = Sql & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), True) & _
               " AS Concepto, "
   Sql = Sql & "movimientoBancario.ConsecutivoMovimiento, "
   Sql = Sql & "movimientoBancario.Descripcion, "
   Sql = Sql & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  "movimientoBancario.Monto", "0", True) & _
               " AS MontoIngreso, "
   Sql = Sql & gUtilSQL.getIIF("movimientoBancario.TipoConcepto" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                  "movimientoBancario.Monto", "0", True) & _
               " AS MontoEgreso, "
   Sql = Sql & "movimientoBancario.NombreOperador"
   
   If gGlobalization.fEsCodigoPeru Then
      Dim insBancoVista As clsBancoVista
      Set insBancoVista = New clsBancoVista
      
      Sql = Sql & ", (SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoCobrado
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & " UNION "
      
      Sql = Sql & " SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXAnticipoPagado
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & " UNION "
      
      Sql = Sql & " SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXCobranza
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & " UNION "
      
      Sql = Sql & " SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXDebitoBancario
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & " UNION "
      
      Sql = Sql & " SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXPago
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & " UNION "
      
      Sql = Sql & " SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXRestantes
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & " UNION "
      
      Sql = Sql & " SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXUsuario
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & " UNION "
      
      Sql = Sql & " SELECT  RazonSocial"
      Sql = Sql & " From " & insBancoVista.GetViewNameMovBCtaBGeneradaXUsuarioSinCheque
      Sql = Sql & " Where (ConsecutivoCompania = " & gUtilSQL.fSimpleSqlValue(valConsecutivoCompania) & ")"
      Sql = Sql & " AND (ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento)"
      Sql = Sql & " AND (CodigoCtaBancaria = cuentaBancaria.Codigo)"
      Sql = Sql & ") AS RazonSocial, "
        
      Sql = Sql & "(SELECT Banco.Nombre FROM Banco WHERE  banco.Codigo = cuentaBancaria.CodigoBanco) AS Banco, "
      Sql = Sql & "(SELECT ConceptoBancario.Descripcion FROM ConceptoBancario WHERE conceptoBancario.Codigo = movimientoBancario.CodigoConcepto) AS DescripcionConcepto "
      
      Set insBancoVista = Nothing
   End If
   
   
   If valEsElInformeDeTrasaccionesPorBeneficiario Then
      Sql = Sql & ", " & insMovBancSQl.fSubQueryTransaccionesConBeneficiario(valEsSistemaAdmInfotax, valConsecutivoCompania)
   End If
   Sql = Sql & " FROM cuentaBancaria"
   Sql = Sql & " INNER JOIN movimientoBancario ON "
   Sql = Sql & "(cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria)"
   Sql = Sql & " AND (cuentaBancaria.ConsecutivoCompania = movimientoBancario.ConsecutivoCompania)"
   Sql = Sql & " WHERE cuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", _
                valFechaInicial, valFechaFinal)
   If valImprimirSoloCuentasActivas Then
      Sql = Sql & " AND cuentaBancaria.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND cuentaBancaria.Codigo" & _
                  " = " & gUtilSQL.fSimpleSqlValue(Trim(valNumeroDeCuenta))
   End If
   Sql = Sql & " ORDER BY "
   If valEsElInformeDeTrasaccionesPorBeneficiario Then
      If valAgruparPorBeneficiario And valIsSQLDataBase Then
        Sql = Sql & "beneficiario, "
      End If
   End If
   Sql = Sql & "cuentaBancaria.Codigo, "
   Sql = Sql & "movimientoBancario.Fecha, "
   Sql = Sql & "movimientoBancario.ConsecutivoMovimiento, "
   Sql = Sql & "movimientoBancario.NumeroDocumento, "
   Sql = Sql & "movimientoBancario.TipoConcepto"
   Set insMovBancSQl = Nothing
   Set insgEnumProyecto = Nothing
   Set insgEnumReport = Nothing
   fSQLTransaccionesPorCuenta = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLTransaccionesPorCuenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLTransaccionesPorConcepto(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valCantidadAImprimir As String, ByVal valConcepto As String) As String
   Dim Sql As String
   Dim insgEnumProyecto As clsEnumAdministrativo
   Dim insgEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set insgEnumProyecto = New clsEnumAdministrativo
   Set insgEnumReport = New clsEnumReport
   Sql = "SELECT "
   Sql = Sql & "conceptoBancario.Codigo AS CodigoConcepto, "
   Sql = Sql & "conceptoBancario.Descripcion AS DescripcionConcepto,"
   Sql = Sql & gUtilSQL.getIIF("conceptoBancario.Tipo" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), True) & _
                  " AS TipoConcepto, "
   Sql = Sql & "cuentaBancaria.NombreDeLaMoneda, "
   Sql = Sql & "cuentaBancaria.Codigo AS CodigoCuenta, "
   Sql = Sql & "cuentaBancaria.NombreCuenta, "
   Sql = Sql & "cuentaBancaria.NumeroCuenta, "
   Sql = Sql & "movimientoBancario.Fecha, "
   Sql = Sql & "movimientoBancario.ConsecutivoMovimiento, "
   Sql = Sql & "movimientoBancario.NumeroDocumento, "
   Sql = Sql & "movimientoBancario.Descripcion AS DescripcionMovimiento, "
   Sql = Sql & "movimientoBancario.Monto"
   Sql = Sql & " FROM conceptoBancario INNER JOIN ("
   Sql = Sql & "cuentaBancaria INNER JOIN movimientoBancario ON ("
   Sql = Sql & "cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria"
   Sql = Sql & ") AND (cuentaBancaria.ConsecutivoCompania" & _
               " = movimientoBancario.ConsecutivoCompania"
   Sql = Sql & ")) ON (conceptoBancario.Codigo" & _
               " = movimientoBancario.CodigoConcepto)"
   Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", _
               valFechaInicial, valFechaFinal)
   If valCantidadAImprimir = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND conceptoBancario.Codigo = " & gUtilSQL.fSimpleSqlValue(valConcepto)
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "conceptoBancario.Codigo, cuentaBancaria.NombreDeLaMoneda, "
   Sql = Sql & "movimientoBancario.Fecha, movimientoBancario.NumeroDocumento"
   fSQLTransaccionesPorConcepto = Sql
   Set insgEnumProyecto = Nothing
   Set insgEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLTransaccionesPorConcepto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLTransaccionesPorDia(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, _
                                       ByVal valSQLMontoConvertido As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "cuentaBancaria.NombreDeLaMoneda, "
   Sql = Sql & "cuentaBancaria.Codigo AS CodigoCuenta, "
   Sql = Sql & "cuentaBancaria.NombreCuenta, "
   Sql = Sql & "cuentaBancaria.NumeroCuenta, "
   Sql = Sql & "conceptoBancario.Codigo AS CodigoConcepto, "
   Sql = Sql & "movimientoBancario.Descripcion AS DescripcionMovimiento, "
   Sql = Sql & gUtilSQL.getIIF("conceptoBancario.Tipo" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  "movimientoBancario.Monto", "0", True) & _
               " AS MontoIngreso, "
   Sql = Sql & gUtilSQL.getIIF("conceptoBancario.Tipo" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                  valSQLMontoConvertido, "0", True) & _
               " AS MontoEgreso, "
   Sql = Sql & "movimientoBancario.NombreOperador"
   Sql = Sql & " FROM cuentaBancaria"
   Sql = Sql & " INNER JOIN (conceptoBancario"
   Sql = Sql & " INNER JOIN movimientoBancario ON "
   Sql = Sql & "conceptoBancario.Codigo" & _
               " = movimientoBancario.CodigoConcepto"
   Sql = Sql & " ) ON ("
   Sql = Sql & "cuentaBancaria.Codigo" & _
               " = movimientoBancario.CodigoCtaBancaria"
   Sql = Sql & ") AND ("
   Sql = Sql & "cuentaBancaria.ConsecutivoCompania" & _
               " = movimientoBancario.ConsecutivoCompania)"
   Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   Sql = Sql & " AND movimientoBancario.Fecha" & _
               " = " & gUtilSQL.fDateToSQLValue(valFechaInicial)
   Sql = Sql & " ORDER BY "
   Sql = Sql & "cuentaBancaria.NombreDeLaMoneda, "
   Sql = Sql & "cuentaBancaria.Codigo, "
   Sql = Sql & "movimientoBancario.ConsecutivoMovimiento"
   fSQLTransaccionesPorDia = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLTransaccionesPorDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLFlujoDeCajaHistorico(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, _
                                       ByVal valFechaFinal As Date, ByVal valSQLMontoConvertido As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT MovimientoBancario.CodigoCtaBancaria as CodigoCuenta,  " _
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
   Sql = Sql & " FROM MovimientoBancario, CuentaBancaria, ConceptoBancario, Moneda "
   Sql = Sql & " WHERE MovimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo " _
               & " AND MovimientoBancario.CodigoConcepto = ConceptoBancario.Codigo " _
               & " AND CuentaBancaria.NombreDeLaMoneda = Moneda.Nombre " _
               & " AND MovimientoBancario.Fecha BETWEEN " & gUtilSQL.fDateToSQLValue(valFechaInicial) & " AND " & gUtilSQL.fDateToSQLValue(valFechaFinal) _
               & " AND CuentaBancaria.ConsecutivoCompania = " & valConsecutivoCompania _
               & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania _
               & " ORDER BY CuentaBancaria.NombreDeLaMoneda, MovimientoBancario.TipoConcepto,ConceptoBancario.Codigo"
   fSQLFlujoDeCajaHistorico = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLFlujoDeCajaHistorico", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDeclaracionITF(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal CodigoCuenta As String, ByVal ConsecutivoCompania As String, ByVal ImprimirSoloCuentasActivas As Boolean, _
                                    ByVal valCantidadAImprimir As Boolean) As String
   Dim Sql As String
   Dim ComparativoFecha As String
   Dim insViews As clsBancoVista
   On Error GoTo h_ERROR
   Set insViews = New clsBancoVista
   ComparativoFecha = gUtilSQL.fSQLValueBetween("FECHA", valFechaInicial, valFechaFinal)
   Sql = ""
   Sql = "SELECT " & gUtilSQL.fSimpleSqlValue("Efectivo") & " as Concepto, "
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "Monto", "0") & ") as MontoEfectivo, "
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("generadoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "Monto", "0") & ") as MontoITF, "
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") as DocumentosEfectivo, "
   Sql = Sql & "NombreCuenta "
   Sql = Sql & "FROM " & insViews.GetViewNameUnionMovBCtaB
   Sql = Sql & " WHERE " & insViews.GetViewNameUnionMovBCtaB & ".ConsecutivoCompania" & _
                     " = " & ConsecutivoCompania & " AND " & ComparativoFecha & " and monto > 0 "
   If valCantidadAImprimir Then
      Sql = Sql & "AND CodigoCtaBancaria = " & gUtilSQL.fSimpleSqlValue(CodigoCuenta)
   End If
      If ImprimirSoloCuentasActivas Then
      Sql = Sql & " AND Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   Sql = Sql & " GROUP BY NombreCuenta"
   fSQLDeclaracionITF = Sql
   Set insViews = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeclaracionITF", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLTransaccionesPorConceptoYCuentaBancaria(ByVal valConsecutivoCompania As Long, _
                        ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valCantidadAImprimirConcepto As String, ByVal valConcepto As String, _
                        ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimirCuenta As String, _
                        ByVal valNumeroDeCuenta As String) As String
   Dim Sql As String
   Dim insgEnumProyecto As clsEnumAdministrativo
   Dim insgEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set insgEnumProyecto = New clsEnumAdministrativo
   Set insgEnumReport = New clsEnumReport
   Sql = "SELECT "
   Sql = Sql & "conceptoBancario.Codigo AS CodigoConcepto, "
   Sql = Sql & "conceptoBancario.Descripcion AS DescripcionConcepto,"
   Sql = Sql & gUtilSQL.getIIF("conceptoBancario.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
                  gUtilSQL.fSimpleSqlValue(insgEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), True) & " AS TipoConcepto, "
   Sql = Sql & "cuentaBancaria.NombreDeLaMoneda, "
   Sql = Sql & "cuentaBancaria.Codigo AS CodigoCuenta, "
   Sql = Sql & "cuentaBancaria.NombreCuenta, "
   Sql = Sql & "cuentaBancaria.NumeroCuenta, "
   Sql = Sql & "movimientoBancario.Fecha, "
   Sql = Sql & "movimientoBancario.ConsecutivoMovimiento, "
   Sql = Sql & "movimientoBancario.NumeroDocumento, "
   Sql = Sql & "movimientoBancario.Descripcion AS DescripcionMovimiento, "
   Sql = Sql & "movimientoBancario.Monto"
   Sql = Sql & " FROM conceptoBancario INNER JOIN "
   Sql = Sql & "(cuentaBancaria INNER JOIN movimientoBancario "
   Sql = Sql & " ON (cuentaBancaria.Codigo = movimientoBancario.CodigoCtaBancaria)"
   Sql = Sql & " AND (cuentaBancaria.ConsecutivoCompania = movimientoBancario.ConsecutivoCompania))"
   Sql = Sql & " ON conceptoBancario.Codigo = movimientoBancario.CodigoConcepto"
   Sql = Sql & " WHERE movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", valFechaInicial, valFechaFinal)
   If valCantidadAImprimirConcepto = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND conceptoBancario.Codigo = " & gUtilSQL.fSimpleSqlValue(valConcepto)
   End If
   If valCantidadAImprimirCuenta = insgEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(Trim(valNumeroDeCuenta))
   End If
   If valImprimirSoloCuentasActivas Then
      Sql = Sql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   Sql = Sql & " ORDER BY "
   Sql = Sql & "conceptoBancario.Codigo, cuentaBancaria.Codigo, cuentaBancaria.NombreDeLaMoneda, "
   Sql = Sql & "movimientoBancario.Fecha, movimientoBancario.NumeroDocumento"
   fSQLTransaccionesPorConceptoYCuentaBancaria = Sql
   Set insgEnumProyecto = Nothing
   Set insgEnumReport = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLTransaccionesPorConceptoYCuentaBancaria", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSQLDeclaracionITFGO6687(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valCodigoCuenta As String, ByVal valConsecutivoCompania As String, ByVal valImprimirSoloCuentasActivas As Boolean, _
                                    ByVal valCantidadAImprimir As Boolean, ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valCodigoConceptoDebitoBancario As String, ByVal valConceptoBancarioReversoDePago As String, ByVal valConceptoBancarioReversoDeTransferencia As String) As String
   Dim Sql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Sql = fSqlCTEDeclaracionIGTFGeneraMovBan(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta, valCodigoConceptoDebitoBancario, valConceptoBancarioReversoDePago, valConceptoBancarioReversoDeTransferencia)
   Sql = Sql & fSqlDeclaracionIGTFGeneraMovBan_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania)
   Sql = Sql & " UNION "
   Sql = Sql & fSqlDeclaracionIGTFNoGeneraMovBan_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania)
   Sql = Sql & " UNION "
   Sql = Sql & fSqlDeclaracionIGTFFactura_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   Sql = Sql & " UNION "
   Sql = Sql & fSqlDeclaracionIGTF_RDV_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   Sql = Sql & " UNION "
   Sql = Sql & fSqlDeclaracionIGTF_RDV_CierreZ_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   Sql = Sql & " UNION "
   Sql = Sql & fSqlDeclaracionIGTFFactura_NC_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   Sql = Sql & " UNION "
   Sql = Sql & fSqlDeclaracionIGTFFactura_ND_SQL(valRedondeaMontoDebitoBancario, valConsecutivoCompania, valFechaInicial, valFechaFinal)
   Sql = Sql & " ORDER BY TipoDeAlicuotaPorContribuyenteStr, AlicuotaImpBancario, NombreCuenta, CodigoMoneda, Fecha "
   fSQLDeclaracionITFGO6687 = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDeclaracionITFGO6687", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBan(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String, ByVal valCodigoConceptoDebitoBancario As String, ByVal valConceptoBancarioReversoDePago As String, ByVal valConceptoBancarioReversoDeTransferencia As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = ";WITH CTE_DeclaracionIGTFGeneraMovBan AS ( "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanMB(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta, valConceptoBancarioReversoDePago, valConceptoBancarioReversoDeTransferencia)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanPago(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanCobranza(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanRendicion(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta, valCodigoConceptoDebitoBancario)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlCTEDeclaracionIGTFGeneraMovBanTransferenciaBancaria(valConsecutivoCompania, valFechaInicial, valFechaFinal, valImprimirSoloCuentasActivas, valCantidadAImprimir, valCodigoCuenta)
   vSql = vSql & " ) "
   fSqlCTEDeclaracionIGTFGeneraMovBan = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBan", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanMB(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSql = vSql & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSql = vSql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO)
   vSql = vSql & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanMB = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanMB", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String, ByVal valConceptoBancarioReversoDePago As String, ByVal valConceptoBancarioReversoDeTransferencia As String) As String
   Dim vSql As String
   Dim vSqlSubSelect As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   
   vSqlSubSelect = " SELECT MB.ConsecutivoMovimiento "
   vSqlSubSelect = vSqlSubSelect & " FROM MovimientoBancario AS MB "
   vSqlSubSelect = vSqlSubSelect & " WHERE " & gUtilSQL.fSQLValueBetween("MB.Fecha", valFechaInicial, valFechaFinal)
   vSqlSubSelect = vSqlSubSelect & " AND MB.ConsecutivoCompania = MovimientoBancario.ConsecutivoCompania AND MB.CodigoCtaBancaria = MovimientoBancario.CodigoCtaBancaria "
   vSqlSubSelect = vSqlSubSelect & " AND (MB.CodigoConcepto = " & gUtilSQL.fSimpleSqlValue(valConceptoBancarioReversoDePago) & " OR MB.CodigoConcepto = " & gUtilSQL.fSimpleSqlValue(valConceptoBancarioReversoDeTransferencia) & " )"
   
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSql = vSql & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSQL = vSQL & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   vSQL = vSQL & " AND MovimientoBancario.ConsecutivoMovimiento NOT IN (" & vSqlSubSelect & ") "
   fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanDebitoBancario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanPago(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSql = vSql & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "INNER JOIN Pago ON MovimientoBancario.ConsecutivoCompania = Pago.ConsecutivoCompania "
   vSql = vSql & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(Pago.NumeroComprobante AS varchar) AND MovimientoBancario.Fecha = Pago.Fecha "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSql = vSql & " AND Pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSql = vSql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ORDEN_DE_PAGO)
   vSql = vSql & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanPago = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanPago", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanCobranza(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSql = vSql & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "INNER JOIN Cobranza ON MovimientoBancario.ConsecutivoCompania = Cobranza.ConsecutivoCompania "
   vSql = vSql & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(Cobranza.Numero AS varchar) AND MovimientoBancario.Fecha = Cobranza.Fecha "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSql = vSql & " AND Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   vSql = vSql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_COBRANZA)
   vSql = vSql & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanCobranza = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanCobranza", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSql = vSql & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "INNER JOIN anticipo ON MovimientoBancario.ConsecutivoCompania = anticipo.ConsecutivoCompania "
   vSql = vSql & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(anticipo.ConsecutivoAnticipo AS varchar) AND MovimientoBancario.Fecha = anticipo.Fecha "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSql = vSql & " AND anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE)
   vSql = vSql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ANTICIPO)
   vSql = vSql & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanAnticipoP", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanRendicion(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String, ByVal valCodigoConceptoDebitoBancario As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSql = vSql & gUtilSQL.getIIF("MovimientoBancario.CodigoConcepto = " & gUtilSQL.fSimpleSqlValue(valCodigoConceptoDebitoBancario), gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO), "MovimientoBancario.GeneradoPor", True) & " AS GeneradoPor, "
   vSql = vSql & " MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "INNER JOIN Adm.Rendicion ON MovimientoBancario.ConsecutivoCompania = Adm.Rendicion.ConsecutivoCompania "
   vSql = vSql & "AND CAST(MovimientoBancario.NumeroDocumento AS varchar) = CAST(Adm.Rendicion.NumeroDocumento AS varchar) AND MovimientoBancario.Fecha = Adm.Rendicion.FechaCierre "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria = Adm.Rendicion.CodigoCuentaBancaria "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSql = vSql & " AND Adm.Rendicion.StatusRendicion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusRendicion.eSRd_CERRADA)
   vSql = vSql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_REPOSICION_DE_CAJACHICA)
   vSql = vSql & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanRendicion = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanRendicion", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, MovimientoBancario.GeneradoPor, "
   vSql = vSql & " MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "INNER JOIN Saw.SolicitudesDePago ON MovimientoBancario.ConsecutivoCompania = Saw.SolicitudesDePago.ConsecutivoCompania "
   vSql = vSql & "INNER JOIN Saw.RenglonSolicitudesDePago ON Saw.SolicitudesDePago.ConsecutivoCompania = Saw.RenglonSolicitudesDePago.ConsecutivoCompania "
   vSql = vSql & "AND Saw.SolicitudesDePago.ConsecutivoSolicitud = Saw.RenglonSolicitudesDePago.ConsecutivoSolicitud  "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria = Saw.RenglonSolicitudesDePago.CuentaBancaria "
   vSql = vSql & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(Saw.RenglonSolicitudesDePago.ConsecutivoSolicitud AS varchar) + CAST(Saw.RenglonSolicitudesDePago.ConsecutivoRenglon AS varchar) "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND MovimientoBancario.Monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSql = vSql & " AND (Saw.SolicitudesDePago.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusSolicitud.eSS_ParcialmenteProcesada)
   vSql = vSql & " OR Saw.SolicitudesDePago.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusSolicitud.eSS_Procesada) & " ) "
   vSql = vSql & " AND Saw.RenglonSolicitudesDePago.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusSolicitudRenglon.eSSR_Procesada)
   vSql = vSql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_SOLICITUD_DE_PAGO)
   vSql = vSql & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanSolicitudPago", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCTEDeclaracionIGTFGeneraMovBanTransferenciaBancaria(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByVal valImprimirSoloCuentasActivas As Boolean, ByVal valCantidadAImprimir As Boolean, ByVal valCodigoCuenta As String) As String
   Dim vSql As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT MovimientoBancario.ConsecutivoMovimiento, MovimientoBancario.Fecha, MovimientoBancario.TipoConcepto, "
   vSql = vSql & "MovimientoBancario.CodigoConcepto, MovimientoBancario.GeneraImpuestoBancario, MovimientoBancario.Monto, "
   vSql = vSql & "MovimientoBancario.GeneradoPor, MovimientoBancario.CambioAbolivares, MovimientoBancario.CodigoCtaBancaria, "
   vSql = vSql & "cuentaBancaria.NombreCuenta, cuentaBancaria.CodigoMoneda, MovimientoBancario.AlicuotaImpBancario, "
   vSql = vSql & "( " & gUtilSQL.DfSQLCaseIfForEnum("cuentaBancaria.TipoDeAlicuotaPorContribuyente", enum_TipoAlicPorContIGTF.eTAPCIGTF_NoAsignado, gEnumProyecto.fEnumTipoAlicPorContIGTFToStringInArray(True), "") & " ) AS TipoDeAlicuotaPorContribuyenteStr, "
   vSql = vSql & "(" & gUtilSQL.getIIF("cuentaBancaria.GeneraMovBancarioPorIGTF = " & gUtilSQL.fBooleanToSqlValue(True), "'Genera IGTF'", "'No Genera IGTF'") & ") AS GeneraMovBancarioPorIGTFStr "
   vSql = vSql & "FROM MovimientoBancario "
   vSql = vSql & "INNER JOIN saw.CuentaBancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSql = vSql & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   vSql = vSql & "INNER JOIN Adm.TransferenciaEntreCuentasBancarias ON MovimientoBancario.ConsecutivoCompania = Adm.TransferenciaEntreCuentasBancarias.ConsecutivoCompania "
   vSql = vSql & "AND CAST(MovimientoBancario.NroMovimientoRelacionado AS varchar) = CAST(Adm.TransferenciaEntreCuentasBancarias.Consecutivo AS varchar) AND MovimientoBancario.Fecha = Adm.TransferenciaEntreCuentasBancarias.Fecha "
   vSql = vSql & "WHERE " & gUtilSQL.fSQLValueBetween("MovimientoBancario.Fecha", valFechaInicial, valFechaFinal) & " AND monto > 0 "
   If valImprimirSoloCuentasActivas Then
      vSql = vSql & " AND cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA)
   End If
   If valCantidadAImprimir Then
      vSql = vSql & "AND cuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCuenta)
   End If
   vSql = vSql & " AND Adm.TransferenciaEntreCuentasBancarias.Status = " & gUtilSQL.fSQLSimpleValueForEnum(0) 'enum_StatusTransferenciaEntreCuentas.eSTEC_VIGENTE
   vSql = vSql & " AND MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_TRANSFERENCIA_BANCARIA)
   vSql = vSql & " AND MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & " AND Saw.CuentaBancaria.ExcluirDelInformeDeDeclaracionIGTF = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlCTEDeclaracionIGTFGeneraMovBanTransferenciaBancaria = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCTEDeclaracionIGTFGeneraMovBanTransferenciaBancaria", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTFGeneraMovBan_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(False) & " OR GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fRoundNDecimales(" Monto * CambioAbolivares ", 2)) & ") AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), gUtilSQL.fRoundNDecimales(" Monto * CambioAbolivares ", 2), "0") & ") AS MontoITF, "
   Else
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), gUtilSQL.fSQLTruncaA2Decimales("Monto * CambioAbolivares"), "0") & ") AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), gUtilSQL.fSQLTruncaA2Decimales("Monto * CambioAbolivares"), "0") & ") AS MontoITF, "
   End If
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") AS ParaContar, "
   Sql = Sql & "AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr "
   Sql = Sql & " FROM CTE_DeclaracionIGTFGeneraMovBan "
   Sql = Sql & "WHERE GeneraMovBancarioPorIGTFStr = 'Genera IGTF'"
   Sql = Sql & " GROUP BY TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, Fecha, AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " HAVING SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") > 0 "
  
   fSqlDeclaracionIGTFGeneraMovBan_SQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFGeneraMovBan_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTFNoGeneraMovBan_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(False) & " OR GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fRoundNDecimales(" Monto * CambioAbolivares ", 2)) & ") AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fRoundNDecimales(" ((Monto * AlicuotaImpBancario)/100) * CambioAbolivares ", 2)) & ") AS MontoITF, "
   Else
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), gUtilSQL.fSQLTruncaA2Decimales(" Monto * CambioAbolivares "), "0") & ") AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(eGP_DEBITO_BANCARIO), "0", gUtilSQL.fSQLTruncaA2Decimales(" ((Monto * AlicuotaImpBancario)/100) * CambioAbolivares ")) & ") AS MontoITF, "
   End If
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") AS ParaContar, "
   Sql = Sql & "AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " FROM CTE_DeclaracionIGTFGeneraMovBan "
   Sql = Sql & "WHERE GeneraMovBancarioPorIGTFStr = 'No Genera IGTF'"
   Sql = Sql & " GROUP BY TipoDeAlicuotaPorContribuyenteStr, NombreCuenta, CodigoMoneda, fecha, AlicuotaImpBancario, GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " HAVING SUM(" & gUtilSQL.getIIF("GeneraImpuestoBancario = " & gUtilSQL.fBooleanToSqlValue(True), "1", "0") & ") > 0 "
   
   fSqlDeclaracionIGTFNoGeneraMovBan_SQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFNoGeneraMovBan_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTFFactura_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Facturas Cobradas en Divisas' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   Sql = Sql & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " FROM factura "
   Sql = Sql & " WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
   Sql = Sql & " AND factura.BaseImponibleIGTF <> 0 AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTFFactura_SQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFFactura_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTF_RDV_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Resumen Diario de Ventas (manual)' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   Sql = Sql & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " FROM factura "
   Sql = Sql & "WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & "  AND factura.RealizoCierreZ = " & gUtilSQL.fBooleanToSqlValue(False) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND factura.BaseImponibleIGTF <> 0 AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTF_RDV_SQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTF_RDV_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTFFactura_NC_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Ajuste por IGTF (Notas de Crédito)' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   Sql = Sql & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " FROM factura "
   Sql = Sql & " WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   Sql = Sql & " AND factura.BaseImponibleIGTF <> 0 AND factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF) & " "
   Sql = Sql & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTFFactura_NC_SQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFFactura_NC_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTFFactura_ND_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Ajuste por IGTF (Notas de Débito)' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   Sql = Sql & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " FROM factura "
   Sql = Sql & " WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   Sql = Sql & " AND factura.BaseImponibleIGTF <> 0 AND factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF) & ""
   Sql = Sql & " AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTFFactura_ND_SQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTFFactura_ND_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDeclaracionIGTF_RDV_CierreZ_SQL(ByVal valRedondeaMontoDebitoBancario As Boolean, ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = " SELECT 'Contribuyente 6' AS TipoDeAlicuotaPorContribuyenteStr, 'Resumen Diario de Ventas (Cierre Z)' AS NombreCuenta, CodigoMoneda, Fecha, "
   If valRedondeaMontoDebitoBancario Then
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.BaseImponibleIGTF * CambioAbolivares ", 2) & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fRoundNDecimales(" factura.IGTFML ", 2) & " ) AS MontoITF, "
   Else
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.BaseImponibleIGTF * CambioAbolivares ") & " ) AS BaseImponible, "
      Sql = Sql & "SUM(" & gUtilSQL.fSQLTruncaA2Decimales(" factura.IGTFML ") & " ) AS MontoITF, "
   End If
   Sql = Sql & "SUM(" & gUtilSQL.getIIF("factura.IGTFML = 0", "0", "1") & ") AS ParaContar, "
   Sql = Sql & "factura.AlicuotaIGTF AS AlicuotaImpBancario, 'No Genera IGTF' AS GeneraMovBancarioPorIGTFStr"
   Sql = Sql & " FROM factura "
   Sql = Sql & "WHERE factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR) & "  AND factura.RealizoCierreZ = " & gUtilSQL.fBooleanToSqlValue(True) & " AND factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND factura.BaseImponibleIGTF <> 0 AND factura.ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.fSQLValueBetween("factura.fecha", valFechaInicial, valFechaFinal)
   Sql = Sql & " GROUP BY CodigoMoneda, fecha, factura.AlicuotaIGTF"
   fSqlDeclaracionIGTF_RDV_CierreZ_SQL = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDeclaracionIGTF_RDV_CierreZ_SQL", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
