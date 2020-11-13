VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsMovimientoBancarioSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCotizacionSQL"
Private Const CM_MESSAGE_NAME As String = "Cotizacion SQL"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSubQueryTransaccionesConBeneficiario(ByVal valEsSistemaAdmInfotax As Boolean, ByVal valConsecutivoCompaniaActual As Long) As String
   Dim SQL As String
   Dim SQLClienteConCobranza As String
   Dim SQLProveedorConPago As String
   Dim SQLProveedorconCxP As String
   Dim SQLClienteConCxC As String
   Dim SQLProveedorConCompras As String
   Dim SQLAnticipoConCliente As String
   Dim sqlAnticipoProveedor As String
   Dim sqlAnticipo As String
   Dim listaResultado As String
   Dim listaComparacion As String
   Dim sqlClienteGenerado As String
   Dim sqlBeneficiarioDeCheque As String
   On Error GoTo h_ERROR
   SQLClienteConCobranza = " (SELECT  TOP 1 cliente.Nombre "
   SQLClienteConCobranza = SQLClienteConCobranza & " FROM cobranza INNER JOIN "
   SQLClienteConCobranza = SQLClienteConCobranza & " cliente ON cobranza.ConsecutivoCompania = cliente.ConsecutivoCompania AND cobranza.CodigoCliente = cliente.Codigo "
   SQLClienteConCobranza = SQLClienteConCobranza & " WHERE (cobranza.GeneraMovBancario = " & gUtilSQL.fBooleanToStrSQL(True) & ") AND (cobranza.Numero = movimientoBancario.NumeroDocumento) AND cobranza.consecutivocompania = " & valConsecutivoCompaniaActual & ")"
   
   SQLProveedorConPago = " (SELECT TOP 1 proveedor.NombreProveedor"
   SQLProveedorConPago = SQLProveedorConPago & " FROM pago INNER JOIN"
   SQLProveedorConPago = SQLProveedorConPago & " proveedor ON pago.ConsecutivoCompania = proveedor.ConsecutivoCompania AND"
   SQLProveedorConPago = SQLProveedorConPago & " Pago.codigoProveedor = proveedor.codigoProveedor"
   SQLProveedorConPago = SQLProveedorConPago & " WHERE (pago.NumeroCheque = movimientoBancario.NumeroDocumento) AND pago.consecutivocompania = " & valConsecutivoCompaniaActual & ")"
   
   SQLProveedorconCxP = " (SELECT TOP 1 proveedor.NombreProveedor"
   SQLProveedorconCxP = SQLProveedorconCxP & " FROM cxP INNER JOIN"
   SQLProveedorconCxP = SQLProveedorconCxP & " proveedor ON cxP.ConsecutivoCompania = proveedor.ConsecutivoCompania AND"
   SQLProveedorconCxP = SQLProveedorconCxP & " CXP.codigoProveedor = proveedor.codigoProveedor"
   SQLProveedorconCxP = SQLProveedorconCxP & " WHERE (CxP.numero = movimientoBancario.NumeroDocumento) AND cxp.consecutivocompania = " & valConsecutivoCompaniaActual & ")"
   
   SQLClienteConCxC = " (SELECT TOP 1 cliente.Nombre"
   SQLClienteConCxC = SQLClienteConCxC & " FROM cliente INNER JOIN"
   SQLClienteConCxC = SQLClienteConCxC & " cxC ON cliente.ConsecutivoCompania = cxC.ConsecutivoCompania AND cliente.Codigo = cxC.CodigoCliente"
   SQLClienteConCxC = SQLClienteConCxC & " WHERE (CxC.numero = movimientoBancario.NumeroDocumento) AND CxC.consecutivocompania = " & valConsecutivoCompaniaActual & ")"
   
   If Not valEsSistemaAdmInfotax Then
      SQLProveedorConCompras = " (SELECT TOP 1 proveedor.NombreProveedor"
      SQLProveedorConCompras = SQLProveedorConCompras & " FROM compra INNER JOIN"
      SQLProveedorConCompras = SQLProveedorConCompras & " proveedor ON compra.ConsecutivoCompania = proveedor.ConsecutivoCompania AND"
      SQLProveedorConCompras = SQLProveedorConCompras & " compra.codigoProveedor = proveedor.codigoProveedor"
      SQLProveedorConCompras = SQLProveedorConCompras & " WHERE (compra.numero = movimientoBancario.NumeroDocumento) AND Compra.consecutivocompania = " & valConsecutivoCompaniaActual & ")"
   Else
      SQLProveedorConCompras = gUtilSQL.fSimpleSqlValue("")
   End If
   
   sqlAnticipoProveedor = " (SELECT TOP 1 Proveedor.NombreProveedor"
   sqlAnticipoProveedor = sqlAnticipoProveedor & " FROM anticipo INNER JOIN"
   sqlAnticipoProveedor = sqlAnticipoProveedor & " Proveedor ON anticipo.COnsecutivoCOmpania = proveedor.COnsecutivoCompania AND"
   sqlAnticipoProveedor = sqlAnticipoProveedor & " anticipo.codigoProveedor = proveedor.codigoProveedor"
   sqlAnticipoProveedor = sqlAnticipoProveedor & " WHERE Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO) & " AND Anticipo.Numero = movimientoBancario.NumeroDocumento AND anticipo.ConsecutivoCompania = " & valConsecutivoCompaniaActual & ")"
   
   SQLAnticipoConCliente = " ( SELECT  TOP 1 Cliente.Nombre"
   SQLAnticipoConCliente = SQLAnticipoConCliente & " FROM anticipo INNER JOIN"
   SQLAnticipoConCliente = SQLAnticipoConCliente & " cliente ON anticipo.ConsecutivoCOmpania = cliente.ConsecutivoCompania AND"
   SQLAnticipoConCliente = SQLAnticipoConCliente & " anticipo.codigoCliente = cliente.codigo"
   SQLAnticipoConCliente = SQLAnticipoConCliente & " WHERE Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO) & " AND Anticipo.Numero = movimientoBancario.NumeroDocumento AND anticipo.ConsecutivoCompania = " & valConsecutivoCompaniaActual & ")"
   
   sqlAnticipo = "(SELECT TOP 1 " & gUtilSQL.getIIF("anticipo.tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_PAGADO), sqlAnticipoProveedor, SQLAnticipoConCliente, True) & " FROM anticipo WHERE Anticipo.Numero = movimientoBancario.NumeroDocumento AND anticipo.ConsecutivoCompania = " & valConsecutivoCompaniaActual & ")"
   
   sqlBeneficiarioDeCheque = "(SELECT TOP 1 chequeDeMovBancario.Beneficiario FROM  movimientoBancario AS Mov1 INNER JOIN "
   sqlBeneficiarioDeCheque = sqlBeneficiarioDeCheque & " chequeDeMovBancario ON Mov1.ConsecutivoCompania = chequeDeMovBancario.ConsecutivoCompania AND "
   sqlBeneficiarioDeCheque = sqlBeneficiarioDeCheque & " Mov1.ConsecutivoMovimiento = chequeDeMovBancario.ConsecutivoMovimiento AND"
   sqlBeneficiarioDeCheque = sqlBeneficiarioDeCheque & " Mov1.ConsecutivoMovimiento = chequeDeMovBancario.ConsecutivoMovimiento WHERE chequeDeMovBancario.ConsecutivoMovimiento = movimientoBancario.ConsecutivoMovimiento"
   sqlBeneficiarioDeCheque = sqlBeneficiarioDeCheque & " AND Mov1.ConsecutivoCompania = " & valConsecutivoCompaniaActual & " ) "
   
   listaComparacion = gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_COBRANZA) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ORDEN_DE_PAGO) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_CXP) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_CXC) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_COMPRA) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ANTICIPO) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO) & " AND movimientoBancario.ImprimirCheque = " & gUtilSQL.fBooleanToSqlValue(True) & gTexto.fSeparadorStandardDeElementosString _
               & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO)
   listaResultado = SQLClienteConCobranza & gTexto.fSeparadorStandardDeElementosString _
                     & SQLProveedorConPago & gTexto.fSeparadorStandardDeElementosString _
                     & SQLProveedorconCxP & gTexto.fSeparadorStandardDeElementosString _
                     & SQLClienteConCxC & gTexto.fSeparadorStandardDeElementosString _
                     & SQLProveedorConCompras & gTexto.fSeparadorStandardDeElementosString _
                     & gUtilSQL.fSimpleSqlValue("Debito Bancario") & gTexto.fSeparadorStandardDeElementosString _
                     & sqlAnticipo & gTexto.fSeparadorStandardDeElementosString _
                     & sqlBeneficiarioDeCheque & gTexto.fSeparadorStandardDeElementosString _
                     & gUtilSQL.fSimpleSqlValue("")
   SQL = gUtilSQL.DfSQLCaseIf("movimientoBancario.generadopor", listaComparacion, listaResultado, gTexto.fSeparadorStandardDeElementosString, "=", "Beneficiario")
   fSubQueryTransaccionesConBeneficiario = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSubQueryTransaccionesConBeneficiario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLReporteMovimientoBancario(ByVal valConsecutivoCompania As Long, ByVal valConsecutivoMovimiento As Long, ByVal valCodigoCtaBancaria As String, ByVal valCodigoConcepto As String) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "movimientoBancario.Descripcion, "
   SQL = SQL & "movimientoBancario.CodigoCtaBancaria, "
   SQL = SQL & "cuentaBancaria.NombreCuenta, "
   SQL = SQL & "movimientoBancario.CodigoConcepto, "
   SQL = SQL & "conceptoBancario.Descripcion AS DescripcionConcepto, "
   SQL = SQL & "movimientoBancario.ConsecutivoMovimiento, "
   SQL = SQL & "movimientoBancario.Monto, "
   SQL = SQL & "banco.Nombre, "
   SQL = SQL & "movimientoBancario.Fecha, "
   SQL = SQL & "movimientoBancario.NumeroDocumento, "
   SQL = SQL & "movimientoBancario.NombreOperador "
   SQL = SQL & "FROM conceptoBancario "
   SQL = SQL & "INNER JOIN (movimientoBancario "
   SQL = SQL & "INNER JOIN (cuentaBancaria "
   SQL = SQL & "INNER JOIN banco ON "
   SQL = SQL & "cuentaBancaria.CodigoBanco = banco.Codigo) ON "
   SQL = SQL & "(movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo) "
   SQL = SQL & "AND (movimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania)) "
   SQL = SQL & "ON conceptoBancario.Codigo = movimientoBancario.CodigoConcepto "
   SQL = SQL & "WHERE ((movimientoBancario.ConsecutivoMovimiento = " & valConsecutivoMovimiento & ") "
   SQL = SQL & "AND ((movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo) "
   SQL = SQL & "AND (movimientoBancario.CodigoCtaBancaria = " & valCodigoCtaBancaria & " )) "
   SQL = SQL & "AND ((movimientoBancario.CodigoConcepto = conceptoBancario.Codigo) "
   SQL = SQL & "AND (movimientoBancario.CodigoConcepto = " & valCodigoConcepto & "))) "
   SQL = SQL & "AND movimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania

h_EXIT: On Error GoTo 0
   fSQLReporteMovimientoBancario = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLReporteMovimientoBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLReporteMovimientoBancarioEntreFechas(ByVal valConsecutivoCompania As Long, ByVal valCodigoCtaBancaria As String, _
                  ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                  ByVal valUsaCuentaBancaria As Boolean, ByVal valAgruparBeneficiario As Boolean, _
                  ByVal valMontoMayor As Boolean, ByVal valMonto As Currency, _
                  ByVal valEfecturaCambioABs As Boolean, ByVal gUltimaTasaDeCambio As Object, _
                  ByVal gMonedaLocalActual As Object, ByVal valUsarCambioOriginal As Boolean, _
                  ByVal valUsaBeneficiario As Boolean, ByVal valBeneficiario As String) As String
   Dim SQL As String
   Dim insEnumProyecto As clsEnumAdministrativo
   Dim sqlCambio As String
   Dim sqlMonto As String
   On Error GoTo h_ERROR
   Set insEnumProyecto = New clsEnumAdministrativo
   
   SQL = "SELECT CuentaBancaria.Codigo AS CodigoCuenta,"
   SQL = SQL & "CuentaBancaria.NombreCuenta AS NombreCuenta, "
   SQL = SQL & "MovimientoBancario.CodigoConcepto, "
   SQL = SQL & "MovimientoBancario.fecha AS FechaMovimiento, "
   SQL = SQL & gUtilSQL.getIIF("conceptoBancario.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
            gUtilSQL.fSimpleSqlValue(insEnumProyecto.enumIngresoEgresoToString(eIE_INGRESO)), _
            gUtilSQL.fSimpleSqlValue(insEnumProyecto.enumIngresoEgresoToString(eIE_EGRESO)), True) & " AS TipoConcepto, "
   SQL = SQL & gUtilSQL.getIIF("MovimientoBancario.Descripcion LIKE " & gUtilSQL.fSimpleSqlValue(gUtilSQL.getSimboloComodin & "Anulado" & gUtilSQL.getSimboloComodin), _
            gUtilSQL.fSimpleSqlValue("Anulado"), _
            gUtilSQL.fSimpleSqlValue("Activo"), True) & " AS Estatus, "
   If valEfecturaCambioABs Then
      If valUsarCambioOriginal Then
         sqlCambio = "MovimientoBancario.CambioABolivares"
      Else
         sqlCambio = gUltimaTasaDeCambio.getUltimaTasaDeCambio("CuentaBancaria.NombreDeLaMoneda", False)
      End If

      SQL = SQL & gUtilSQL.getIIF("CuentaBancaria.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                            "CuentaBancaria.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "CuentaBancaria.NombreDeLaMoneda", "MovimientoBancario.Monto", valUsarCambioOriginal, ""), _
                            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "MovimientoBancario.Monto", "MovimientoBancario.Fecha"), True) & " AS Monto, "

      SQL = SQL & gUtilSQL.getIIF("CuentaBancaria.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda), _
                      "CuentaBancaria.NombreDeLaMoneda", gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyNombreMoneda), True) & " AS Moneda, "

   Else
      SQL = SQL & gUtilSQL.fRoundNDecimales(gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "MovimientoBancario.Monto", "MovimientoBancario.Fecha"), 2) & " AS Monto, "
      SQL = SQL & " CuentaBancaria.NombreDeLaMoneda AS Moneda, "
   End If
   SQL = SQL & "MovimientoBancario.NumeroDocumento, "
   SQL = SQL & "MovimientoBancario.Descripcion, "
   SQL = SQL & "MovimientoBancario.Cambioabolivares, "
   SQL = SQL & "ChequeDeMovBancario.Beneficiario, "
   SQL = SQL & "ChequeDeMovBancario.NumeroDeCheque, "
   SQL = SQL & "ConceptoBancario.Descripcion AS Concepto "
   SQL = SQL & "FROM MovimientoBancario INNER JOIN CuentaBancaria "
   SQL = SQL & "ON (MovimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo "
   SQL = SQL & "AND MovimientoBancario.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania) "
   SQL = SQL & "LEFT JOIN ChequeDeMovBancario "
   SQL = SQL & "ON (MovimientoBancario.ConsecutivoCompania = ChequeDeMovBancario.ConsecutivoCompania "
   SQL = SQL & "AND MovimientoBancario.ConsecutivoMovimiento = ChequeDeMovBancario.ConsecutivoMovimiento) "
   SQL = SQL & "INNER JOIN ConceptoBancario "
   SQL = SQL & "ON (MovimientoBancario.CodigoConcepto = ConceptoBancario.Codigo) "
   SQL = SQL & "WHERE MovimientoBancario.ConsecutivoCompania = " & valConsecutivoCompania
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween("MovimientoBancario.fecha", valFechaInicial, valFechaFinal)
   If valUsaCuentaBancaria Then
      SQL = SQL & " AND CuentaBancaria.Codigo = " & gUtilSQL.fSimpleSqlValue(valCodigoCtaBancaria)
   End If
   If valUsaBeneficiario Then
      SQL = SQL & " AND ChequeDeMovBancario.Beneficiario = " & gUtilSQL.fSimpleSqlValue(gTexto.fLimpiaStringdeBlancosAAmbosLados(valBeneficiario))
   End If
   If valAgruparBeneficiario Then
      SQL = SQL & " AND movimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO)
   End If
   If valMontoMayor Then
      If valEfecturaCambioABs Then
         SQL = SQL & " AND " & gUtilSQL.getIIF("CuentaBancaria.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMoneda) & " OR " & _
                            "CuentaBancaria.CodigoMoneda <> " & gUtilSQL.fSimpleSqlValue(gMonedaLocalActual.GetHoyCodigoMonedaAnt), _
                            gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(sqlCambio, "CuentaBancaria.NombreDeLaMoneda", "MovimientoBancario.Monto", valUsarCambioOriginal, ""), _
                            gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "MovimientoBancario.Monto", "MovimientoBancario.Fecha"), True) & " >= " & gUtilSQL.fNumToStrSQL(valMonto)
      Else
         SQL = SQL & " AND MovimientoBancario.monto >= " & gUtilSQL.fNumToStrSQL(valMonto)
      End If
   End If
   SQL = SQL & " ORDER BY TipoConcepto, Moneda, CuentaBancaria.Codigo, "
   If valAgruparBeneficiario Then
      SQL = SQL & "  ChequeDeMovBancario.beneficiario, MovimientoBancario.Fecha "
   Else
      SQL = SQL & "  MovimientoBancario.Fecha"
   End If
   Set insEnumProyecto = Nothing
h_EXIT: On Error GoTo 0
   fSQLReporteMovimientoBancarioEntreFechas = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLReporteMovimientoBancarioEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
