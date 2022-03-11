VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsBancoVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsBancoVista"
Private Const CM_MESSAGE_NAME As String = "Vista Banco"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetViewNameUnionMovBCtaB() As String
   GetViewNameUnionMovBCtaB = "IGV_UnionMovBCtaB"
End Function

Public Function fSqlViewUnionMovBCtaB() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.GeneraImpuestoBancario, "
   vSQL = vSQL & "MovimientoBancario.Monto, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "movimientoBancario.GeneradoPor, "
   vSQL = vSQL & "movimientoBancario.CambioAbolivares, "
   vSQL = vSQL & "cuentaBancaria.Codigo, "
   vSQL = vSQL & "cuentaBancaria.ConsecutivoCompania as Consecutivo, "
   vSQL = vSQL & "cuentaBancaria.NombreCuenta, "
   vSQL = vSQL & "cuentaBancaria.status, "
   vSQL = vSQL & "cuentaBancaria.CodigoMoneda "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "INNER JOIN cuentabancaria ON MovimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND MovimientoBancario.CodigoCtaBancaria  = cuentaBancaria.Codigo "
   fSqlViewUnionMovBCtaB = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBDatosPrincipales", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXCobranza() As String
   GetViewNameMovBCtaBGeneradaXCobranza = "IGV_MovBCtaBGeneradaXCobranza"
End Function

Public Function fSqlViewMovBCtaBGeneradaXCobranza() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & "Cliente.Nombre AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario INNER JOIN "
   vSQL = vSQL & "Cobranza ON MovimientoBancario.NumeroDocumento = Cobranza.Numero "
   vSQL = vSQL & "AND MovimientoBancario.ConsecutivoCompania = Cobranza.ConsecutivoCompania INNER JOIN "
   vSQL = vSQL & "Cliente ON Cobranza.CodigoCliente = Cliente.Codigo "
   vSQL = vSQL & "AND Cobranza.ConsecutivoCompania = Cliente.ConsecutivoCompania "
   vSQL = vSQL & "WHERE (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_COBRANZA) & ")"
   fSqlViewMovBCtaBGeneradaXCobranza = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXCobranza", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXPago() As String
   GetViewNameMovBCtaBGeneradaXPago = "IGV_MovBCtaBGeneradaXPago"
End Function

Public Function fSqlViewMovBCtaBGeneradaXPago() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & "Proveedor.NombreProveedor AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario INNER JOIN "
   vSQL = vSQL & "Pago ON MovimientoBancario.NumeroDocumento = Pago.NumeroCheque "
   vSQL = vSQL & "AND MovimientoBancario.ConsecutivoCompania = Pago.ConsecutivoCompania INNER JOIN "
   vSQL = vSQL & "Proveedor ON Pago.CodigoProveedor = Proveedor.CodigoProveedor "
   vSQL = vSQL & "AND Pago.ConsecutivoCompania = Proveedor.ConsecutivoCompania "
   vSQL = vSQL & "WHERE (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ORDEN_DE_PAGO) & ")"
   fSqlViewMovBCtaBGeneradaXPago = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXPago", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXAnticipoCobrado() As String
   GetViewNameMovBCtaBGeneradaXAnticipoCobrado = "IGV_MovBCtaBGeneradaXAnticipoCobrado"
End Function

Public Function fSqlViewMovBCtaBGeneradaXAnticipoCobrado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & "Cliente.Nombre AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario INNER JOIN "
   vSQL = vSQL & "Anticipo ON MovimientoBancario.NumeroDocumento = Anticipo.ConsecutivoAnticipo "
   vSQL = vSQL & "AND MovimientoBancario.ConsecutivoCompania = Anticipo.ConsecutivoCompania INNER JOIN "
   vSQL = vSQL & "Cliente ON Anticipo.CodigoCliente = Cliente.Codigo "
   vSQL = vSQL & "AND Anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania "
   vSQL = vSQL & "WHERE (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ANTICIPO) & ") "
   vSQL = vSQL & "AND (MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO) & ")"
   fSqlViewMovBCtaBGeneradaXAnticipoCobrado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXAnticipoCobrado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXAnticipoPagado() As String
   GetViewNameMovBCtaBGeneradaXAnticipoPagado = "IGV_MovBCtaBGeneradaXAnticipoPagado"
End Function

Public Function fSqlViewMovBCtaBGeneradaXAnticipoPagado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & "Proveedor.NombreProveedor AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario INNER JOIN "
   vSQL = vSQL & "Anticipo ON MovimientoBancario.NumeroDocumento = Anticipo.ConsecutivoAnticipo "
   vSQL = vSQL & "AND MovimientoBancario.ConsecutivoCompania = Anticipo.ConsecutivoCompania INNER JOIN "
   vSQL = vSQL & "Proveedor ON Anticipo.CodigoProveedor = Proveedor.CodigoProveedor "
   vSQL = vSQL & "AND Anticipo.ConsecutivoCompania = Proveedor.ConsecutivoCompania "
   vSQL = vSQL & "WHERE (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_ANTICIPO) & ") "
   vSQL = vSQL & "AND (MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO) & ")"
   fSqlViewMovBCtaBGeneradaXAnticipoPagado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXAnticipoPagado", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXUsuario() As String
   GetViewNameMovBCtaBGeneradaXUsuario = "IGV_MovBCtaBGeneradaXUsuario"
End Function

Public Function fSqlViewMovBCtaBGeneradaXUsuario() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & "ChequeDeMovBancario.Beneficiario AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario INNER JOIN "
   vSQL = vSQL & "chequeDeMovBancario ON MovimientoBancario.ConsecutivoMovimiento = chequeDeMovBancario.ConsecutivoMovimiento "
   vSQL = vSQL & "AND MovimientoBancario.ConsecutivoCompania = chequeDeMovBancario.ConsecutivoCompania "
   vSQL = vSQL & "WHERE (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO) & ") "
   vSQL = vSQL & "AND (MovimientoBancario.ImprimirCheque = 'S')"
   fSqlViewMovBCtaBGeneradaXUsuario = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXUsuario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXDebitoBancario() As String
   GetViewNameMovBCtaBGeneradaXDebitoBancario = "IGV_MovBCtaBGeneradaXDebitoBancario"
End Function

Public Function fSqlViewMovBCtaBGeneradaXDebitoBancario() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & "CuentaBancaria.NombreCuenta AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario INNER JOIN "
   vSQL = vSQL & "CuentaBancaria ON MovimientoBancario.CodigoCtaBancaria = CuentaBancaria.Codigo "
   vSQL = vSQL & "AND MovimientoBancario.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "WHERE (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_DEBITO_BANCARIO) & ") "
   fSqlViewMovBCtaBGeneradaXDebitoBancario = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXDebitoBancario", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXRestantes() As String
   GetViewNameMovBCtaBGeneradaXRestantes = "IGV_MovBCtaBGeneradaXRestantes"
End Function

Public Function fSqlViewMovBCtaBGeneradaXRestantes() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("Sin Razon Social") & " AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "WHERE "
   vSQL = vSQL & "(MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_CXP) & ")"
   vSQL = vSQL & " OR (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_CXC) & ")"
   vSQL = vSQL & " OR (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_GESTION_COBRANZA) & ")"
   vSQL = vSQL & " OR (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_COMPRA) & ")"
   vSQL = vSQL & " OR (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_RECONVERSION_MONETARIA) & ")"
   fSqlViewMovBCtaBGeneradaXRestantes = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXRestantes", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovBCtaBGeneradaXUsuarioSinCheque() As String
   GetViewNameMovBCtaBGeneradaXUsuarioSinCheque = "IGV_MovBCtaBGeneradaXUsuarioSinCheque"
End Function

Public Function fSqlViewMovBCtaBGeneradaXUsuarioSinCheque() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "movimientoBancario.Descripcion, "
   vSQL = vSQL & "MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_INGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoIngreso,"
   vSQL = vSQL & gUtilSQL.getIIF("MovimientoBancario.TipoConcepto = " & gUtilSQL.fSQLSimpleValueForEnum(enum_IngresoEgreso.eIE_EGRESO), _
                                 "MovimientoBancario.Monto", _
                                 0, True) & " AS MontoEgreso,"
   vSQL = vSQL & "movimientoBancario.Descripcion AS RazonSocial "
   vSQL = vSQL & "FROM MovimientoBancario "
   vSQL = vSQL & "WHERE (MovimientoBancario.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_GeneradoPor.eGP_USUARIO) & ") "
   vSQL = vSQL & "AND (MovimientoBancario.ImprimirCheque = 'N')"
   fSqlViewMovBCtaBGeneradaXUsuarioSinCheque = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovBCtaBGeneradaXUsuarioSinCheque", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMovimientosXCuentaBancaria0695() As String
   GetViewNameMovimientosXCuentaBancaria0695 = "IGV_MovimientosXCuentaBancaria0695"
End Function

Public Function fSqlViewMovimientosXCuentaBancaria0695() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "MovimientoBancario.Fecha, "
   vSQL = vSQL & "MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "MovimientoBancario.GeneraImpuestoBancario, "
   vSQL = vSQL & "MovimientoBancario.Monto, "
   vSQL = vSQL & "MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & "MovimientoBancario.CambioABolivares, "
   vSQL = vSQL & "CuentaBancaria.CodigoMoneda, "
   vSQL = vSQL & "CuentaBancaria.CodigoBanco, "
   vSQL = vSQL & "movimientoBancario_1.Monto AS MontoImp, "
   vSQL = vSQL & "movimientoBancario_1.NroMovimientoRelacionado, "
   vSQL = vSQL & "cuentaBancaria.Status, "
   vSQL = vSQL & "COMPANIA.NumeroDeRIF, "
   vSQL = vSQL & "COMPANIA.CodigoPais, "
   vSQL = vSQL & "Pais.Codigo AS CodPaisCta "
   vSQL = vSQL & "FROM movimientoBancario "
   vSQL = vSQL & " INNER JOIN movimientoBancario AS movimientoBancario_1 "
   vSQL = vSQL & "ON movimientoBancario_1.ConsecutivoCompania = " & "MovimientoBancario.ConsecutivoCompania "
   vSQL = vSQL & "AND movimientoBancario_1.NroMovimientoRelacionado = " & "MovimientoBancario.ConsecutivoMovimiento "
   vSQL = vSQL & "INNER JOIN COMPANIA "
   vSQL = vSQL & "ON COMPANIA.ConsecutivoCompania = " & "MovimientoBancario.ConsecutivoCompania "
   vSQL = vSQL & "INNER JOIN cuentaBancaria "
   vSQL = vSQL & "ON movimientoBancario.ConsecutivoCompania = cuentaBancaria.ConsecutivoCompania AND "
   vSQL = vSQL & "movimientoBancario.CodigoCtaBancaria = cuentaBancaria.Codigo "
   vSQL = vSQL & "INNER JOIN MonedaLocal "
   vSQL = vSQL & "ON " & "CuentaBancaria.CodigoMoneda = MonedaLocal.CodigoMoneda "
   vSQL = vSQL & "INNER JOIN Pais "
   vSQL = vSQL & "ON MonedaLocal.CodigoPais = Pais.Codigo "
   vSQL = vSQL & "INNER JOIN banco ON " & "CuentaBancaria.CodigoBanco = banco.Codigo "
   vSQL = vSQL & "WHERE (" & "cuentaBancaria.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA) & ") "
   fSqlViewMovimientosXCuentaBancaria0695 = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovimientosXCuentaBancaria0695", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



Public Function GetViewNameMovimientosBancarios_B1() As String
   GetViewNameMovimientosBancarios_B1 = "IGV_MovimientosBancarios_B1"
End Function

Public Function fSqlViewMovimientosBancarios_B1() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & "dbo.MovimientoBancario.ConsecutivoCompania, "
   vSQL = vSQL & "dbo.MovimientoBancario.ConsecutivoMovimiento, "
   vSQL = vSQL & "dbo.MovimientoBancario.CodigoCtaBancaria, "
   vSQL = vSQL & "dbo.MovimientoBancario.CodigoConcepto, "
   vSQL = vSQL & "dbo.MovimientoBancario.Fecha, "
   vSQL = vSQL & "dbo.MovimientoBancario.TipoConcepto, "
   vSQL = vSQL & "dbo.MovimientoBancario.Monto, "
   vSQL = vSQL & "dbo.MovimientoBancario.NumeroDocumento, "
   vSQL = vSQL & "dbo.MovimientoBancario.Descripcion, "
   vSQL = vSQL & "dbo.MovimientoBancario.GeneraImpuestoBancario, "
   vSQL = vSQL & "dbo.MovimientoBancario.NroMovimientoRelacionado, "
   vSQL = vSQL & "dbo.MovimientoBancario.GeneradoPor, "
   vSQL = vSQL & "dbo.MovimientoBancario.CambioAbolivares, "
   vSQL = vSQL & "dbo.MovimientoBancario.ImprimirCheque, "
   vSQL = vSQL & "dbo.MovimientoBancario.ConciliadoSn, "
   vSQL = vSQL & "dbo.MovimientoBancario.NroConciliacion, "
   vSQL = vSQL & "dbo.MovimientoBancario.GenerarAsientoDeRetiroEnCuenta, "
   vSQL = vSQL & "dbo.CuentaBancaria.CuentaContable, "
   vSQL = vSQL & "dbo.CuentaBancaria.Status, "
   vSQL = vSQL & "dbo.CuentaBancaria.NumeroCuenta, "
   vSQL = vSQL & "dbo.CuentaBancaria.NombreCuenta, "
   vSQL = vSQL & "dbo.CuentaBancaria.CodigoBanco, "
   vSQL = vSQL & "dbo.CuentaBancaria.NombreSucursal, "
   vSQL = vSQL & "dbo.CuentaBancaria.TipoCtaBancaria "
   vSQL = vSQL & " FROM dbo.MovimientoBancario "
   vSQL = vSQL & " INNER JOIN  dbo.CuentaBancaria ON "
   vSQL = vSQL & " dbo.MovimientoBancario.CodigoCtaBancaria = dbo.CuentaBancaria.Codigo AND  "
   vSQL = vSQL & " dbo.MovimientoBancario.ConsecutivoCompania = dbo.CuentaBancaria.ConsecutivoCompania "
  
  fSqlViewMovimientosBancarios_B1 = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewMovimientosBancarios_B1", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
