VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCajaSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsCajaSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Caja"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function fSQLDelCuadreDeCaja(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valConsecutivoInicialDeCaja As Long, ByVal valNumeroComprobanteFiscal As String, _
                        ByVal valFiltrarPorCajero As Boolean, _
                        ByVal valNombreCajero As String, _
                        ByVal valPresentarLosDatosEnMonedaOriginal As Boolean, _
                        ByVal valCodigoMonedaLocalActual As String) As String
   Dim vSql As String
   Dim vSQLTotalFacturaEnMontoOriginal As String
   Dim vSQLMontoPago As String
   On Error GoTo h_ERROR
   If valPresentarLosDatosEnMonedaOriginal Then
      vSQLTotalFacturaEnMontoOriginal = "factura.TotalFactura"
      vSQLMontoPago = "renglonCobroDeFactura.Monto * ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1)"
   Else
      vSQLTotalFacturaEnMontoOriginal = "MAX(factura.TotalFactura * factura.CambioABolivares)"
      vSQLMontoPago = "                 ( CASE WHEN factura.CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(valCodigoMonedaLocalActual) & " THEN  " & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "       renglonCobroDeFactura.Monto * ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1) " & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "   ELSE " & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "       (renglonCobroDeFactura.Monto /  "
      vSQLMontoPago = vSQLMontoPago & "        (CASE WHEN ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1) = 0 THEN 1 " & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "         ELSE ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1) " & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "         END" & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "        )" & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "       )" & vbCrLf
      vSQLMontoPago = vSQLMontoPago & "   END" & vbCrLf
      vSQLMontoPago = vSQLMontoPago & " )"
   End If
   
   vSql = vSql & "SELECT" & vbCrLf
   vSql = vSql & "      '1' AS ORDEN" & vbCrLf
   vSql = vSql & "      , factura.Moneda AS Moneda" & vbCrLf
   vSql = vSql & "      , factura.ConsecutivoCaja" & vbCrLf
   vSql = vSql & "      , caja.NombreCaja" & vbCrLf
   vSql = vSql & "      , factura.Numero" & vbCrLf
   vSql = vSql & "      , factura.NumeroComprobanteFiscal" & vbCrLf
   vSql = vSql & "      , factura.Fecha" & vbCrLf
   vSql = vSql & "      , cliente.Nombre AS NombreCliente" & vbCrLf
   vSql = vSql & "      , " & vSQLTotalFacturaEnMontoOriginal & " AS MontoFactura" & vbCrLf
   
   vSql = vSql & "      , CASE WHEN factura.TipoDeDocumento IN ('1','7') THEN" & vbCrLf
   
   vSql = vSql & "      " & vSQLTotalFacturaEnMontoOriginal & vbCrLf
   vSql = vSql & "       ELSE" & vbCrLf
   vSql = vSql & "         SUM ( (CASE WHEN " & vSQLMontoPago & "  IS NULL  OR renglonCobroDeFactura.Monto * ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1)  < 0  THEN 0 ELSE " & vSQLMontoPago & "  END ) ) END   AS MontoPagado" & vbCrLf
   vSql = vSql & "      ,  (CASE WHEN (SUM (renglonCobroDeFactura.Monto * ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1)) - (factura.TotalFactura))  IS NULL  THEN" & vbCrLf
   vSql = vSql & "         0" & vbCrLf
   vSql = vSql & "         ELSE (SUM (" & vSQLMontoPago & ") - (" & vSQLTotalFacturaEnMontoOriginal & "))  END )  AS Vuelto" & vbCrLf
   vSql = vSql & "      , CASE WHEN SUM (CASE WHEN renglonCobroDeFactura.Monto IS NULL AND (factura.TotalFactura) > 0 THEN (factura.TotalFactura * factura.CambioABolivares)  ELSE 0 END )  > 0 AND factura.TipoDeDocumento IN ('0','5') THEN 'FACTURAS NO COBRADAS' ELSE  CASE WHEN factura.TipoDeDocumento IN ('0','5') THEN 'FACTURAS COBRADAS' ELSE  CASE WHEN factura.TipoDeDocumento IN ('1','7') THEN 'NOTAS DE CRÉDITO/DEVOLUCIONES' ELSE '' END   END   END   AS Grupo" & vbCrLf
   vSql = vSql & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '0' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Efectivo" & vbCrLf
   vSql = vSql & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '1' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Tarjeta" & vbCrLf
   vSql = vSql & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '2' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Cheque" & vbCrLf
   vSql = vSql & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '3' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Deposito" & vbCrLf
   vSql = vSql & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '4' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Anticipo" & vbCrLf
   vSql = vSql & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago <> '4' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS TotalSinAnticipo" & vbCrLf
   vSql = vSql & "      , SUM( " & vSQLMontoPago & " ) AS TotalConAnticipo" & vbCrLf
   vSql = vSql & "      , factura.StatusFactura AS Status" & vbCrLf
   If valFiltrarPorCajero Then
      vSql = vSql & ", factura.NombreOperador  AS NombreOperador"
   End If
   vSql = vSql & "   FROM" & vbCrLf
   vSql = vSql & "      Cliente" & vbCrLf
   vSql = vSql & "   INNER JOIN" & vbCrLf
   vSql = vSql & "      factura" & vbCrLf
   vSql = vSql & "      ON Cliente.Codigo = factura.CodigoCliente AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania" & vbCrLf
   vSql = vSql & "   INNER JOIN Caja" & vbCrLf
   vSql = vSql & "      ON Caja.ConsecutivoCompania = factura.ConsecutivoCompania" & vbCrLf
   vSql = vSql & "      AND factura.ConsecutivoCaja = Caja.ConsecutivoCaja" & vbCrLf
   vSql = vSql & "   LEFT OUTER JOIN Saw.FormaDelCobro" & vbCrLf
   vSql = vSql & "   INNER JOIN renglonCobroDeFactura" & vbCrLf
   vSql = vSql & "      ON Saw.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro" & vbCrLf
   vSql = vSql & "      ON factura.TipoDeDocumento = renglonCobroDeFactura.TipoDeDocumento" & vbCrLf
   vSql = vSql & "      AND factura.Numero = renglonCobroDeFactura.NumeroFactura" & vbCrLf
   vSql = vSql & "      AND factura.ConsecutivoCompania = renglonCobroDeFactura.ConsecutivoCompania" & vbCrLf
   vSql = vSql & "   WHERE" & vbCrLf
   vSql = vSql & "      (" & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   vSql = vSql & "      AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   vSql = vSql & "      AND (factura.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   vSql = vSql & "      AND (factura.ConsecutivoCaja <> " & valConsecutivoInicialDeCaja & ")"
   vSql = vSql & "      AND (factura.TipoDeDocumento IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL) & "))"
   vSql = vSql & "      AND (factura.GeneraCobroDirecto  = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   If valFiltrarPorCajero And gTexto.DfLen(valNombreCajero) > 0 Then
      vSql = vSql & "   AND factura.NombreOperador = " & gUtilSQL.fSimpleSqlValue(valNombreCajero)
   End If
   vSql = vSql & "    GROUP BY" & vbCrLf
   vSql = vSql & "      factura.Moneda" & vbCrLf
   vSql = vSql & "      , factura.ConsecutivoCaja" & vbCrLf
   vSql = vSql & "      , caja.NombreCaja" & vbCrLf
   vSql = vSql & "      , factura.Numero" & vbCrLf
   vSql = vSql & "      , factura.NumeroComprobanteFiscal" & vbCrLf
   vSql = vSql & "      , factura.Fecha, cliente.Nombre" & vbCrLf
   vSql = vSql & "      , factura.TotalFactura" & vbCrLf
   vSql = vSql & "      , factura.TipoDeDocumento" & vbCrLf
   vSql = vSql & "      , factura.StatusFactura" & vbCrLf
   If valFiltrarPorCajero Then
      vSql = vSql & "      , factura.NombreOperador " & vbCrLf
   End If
   vSql = vSql & "UNION" & vbCrLf
   vSql = vSql & "   SELECT" & vbCrLf
   vSql = vSql & "      '2' AS ORDEN" & vbCrLf
   vSql = vSql & "      , anticipo.Moneda, Caja.ConsecutivoCaja, Caja.NombreCaja, anticipo.Numero, '' AS NumeroComprobanteFiscal, anticipo.Fecha, Cliente.Nombre, anticipo.MontoTotal AS MontoTotalDocumento, anticipo.MontoTotal AS MontoPagado, '0,00' AS Vuelto, 'ANTICIPOS COBRADOS' AS Grupo, '0,00' AS Efectivo" & vbCrLf
   vSql = vSql & "      , '0,00' AS Tarjeta, '0,00' AS Cheque, anticipo.MontoTotal AS Deposito, '0,00' AS Anticipo" & vbCrLf
   vSql = vSql & "      , anticipo.MontoTotal AS TotalSinAnticipo" & vbCrLf
   vSql = vSql & "      , anticipo.MontoTotal AS TotalConAnticipo" & vbCrLf
   vSql = vSql & "      , anticipo.Status AS Status" & vbCrLf
   If valFiltrarPorCajero Then
      vSql = vSql & "  , anticipo.NombreOperador  AS NombreOperador " & vbCrLf
   End If
   vSql = vSql & "   FROM" & vbCrLf
   vSql = vSql & "      Caja" & vbCrLf
   vSql = vSql & "   INNER JOIN" & vbCrLf
   vSql = vSql & "      anticipo" & vbCrLf
   vSql = vSql & "      ON Caja.ConsecutivoCompania = anticipo.ConsecutivoCompania" & vbCrLf
   vSql = vSql & "      AND Caja.ConsecutivoCaja = anticipo.ConsecutivoCaja" & vbCrLf
   vSql = vSql & "   INNER JOIN Cliente" & vbCrLf
   vSql = vSql & "      ON anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania" & vbCrLf
   vSql = vSql & "      AND anticipo.CodigoCliente = Cliente.Codigo" & vbCrLf
   vSql = vSql & "   WHERE" & vbCrLf
   vSql = vSql & "      anticipo.AsociarAnticipoACaja ='S' AND "
   vSql = vSql & "      (Fecha BETWEEN '" & valFechaInicial & "' AND '" & valFechaFinal & "') AND "
   vSql = vSql & "      Tipo = '0' AND "
   vSql = vSql & "      anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   vSql = vSql & "      AND anticipo.Status <> '1'"
   If valFiltrarPorCajero And gTexto.DfLen(valNombreCajero) > 0 Then
      vSql = vSql & "   AND anticipo.NombreOperador = " & gUtilSQL.fSimpleSqlValue(valNombreCajero)
   End If
   vSql = vSql & "   ORDER BY" & vbCrLf
   If valFiltrarPorCajero Then
      vSql = vSql & "   NombreOperador ,"
   End If
   vSql = vSql & "      Moneda" & vbCrLf
   vSql = vSql & "      , factura.ConsecutivoCaja" & vbCrLf
   vSql = vSql & "      , ORDEN" & vbCrLf
   vSql = vSql & "      , Grupo" & vbCrLf
   vSql = vSql & "      , caja.NombreCaja" & vbCrLf
   vSql = vSql & "      , factura.Fecha" & vbCrLf
   vSql = vSql & "      , factura.Numero" & vbCrLf
   vSql = vSql & "      , cliente.Nombre" & vbCrLf

   fSQLDelCuadreDeCaja = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLDelCuadreDeCaja", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelCuadreDeCajaPorTipoDeCobro(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valFacturaTableName As String, ByVal valFacturaFecha As String, _
                        ByVal valFacturaConsecutivoCompania As String, ByVal valFacturaTipoDeDocumento As String, _
                        ByVal valFacturaNumero As String, ByVal valFacturaConsecutivoCaja As String, _
                        ByVal valFacturaMoneda As String, ByVal valFacturaCodigoCliente As String, ByVal valStatusFactura As String, _
                        ByVal valFacturaGeneraCobroDirecto As String, _
                        ByVal valMontoFacturaSegunMoneda As String, ByVal valPagoSegunMoneda As String, _
                        ByVal valVueltoSegunMoneda As String, ByVal valGroupByDeFacturaSegunMoneda As String, _
                        ByVal valConsecutivoInicialDeCaja As Long, ByVal valComprobanteFiscal As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & valFacturaTableName & "." & valComprobanteFiscal & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaFecha & ", "
   Sql = Sql & "cliente.Nombre AS NombreCliente, "
   Sql = Sql & valMontoFacturaSegunMoneda
   Sql = Sql & " AS MontoFactura, "
   Sql = Sql & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_CHEQUE), valPagoSegunMoneda, _
               0, True), True) & " ) AS Cheque, "
   Sql = Sql & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_EFECTIVO), valPagoSegunMoneda, _
               0, True), True) & ") AS Efectivo, "
   Sql = Sql & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_TARJETA), valPagoSegunMoneda, _
               0, True), True) & ") AS Tarjeta, "
   Sql = Sql & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_DEPOSITO), valPagoSegunMoneda, _
               0, True), True) & ") AS Deposito, "
   Sql = Sql & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, valPagoSegunMoneda, True) & ")"
   Sql = Sql & " AS MontoPagado, "
   Sql = Sql & gUtilSQL.getIIF(valVueltoSegunMoneda & " IS NULL ", 0, valVueltoSegunMoneda, True)
   Sql = Sql & " AS Vuelto "
   Sql = Sql & ", SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " < 0 OR " & valMontoFacturaSegunMoneda & " < 0", gUtilSQL.getIIF(valPagoSegunMoneda & " is null ", valMontoFacturaSegunMoneda, valPagoSegunMoneda, True), 0, True) & ") AS NotaCredito"
   Sql = Sql & ", SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL AND " & valMontoFacturaSegunMoneda & " > 0", valMontoFacturaSegunMoneda, 0, True) & ") AS Credito "
   Sql = Sql & " FROM Cliente INNER JOIN"
   Sql = Sql & " factura ON Cliente.Codigo = factura.CodigoCliente AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania INNER JOIN"
   Sql = Sql & " Caja ON Caja.ConsecutivoCompania = factura.ConsecutivoCompania AND factura.ConsecutivoCaja = Caja.ConsecutivoCaja LEFT OUTER JOIN"
   Sql = Sql & " FormaDelCobro INNER JOIN"
   Sql = Sql & " renglonCobroDeFactura ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro ON"
   Sql = Sql & " factura.TipoDeDocumento = renglonCobroDeFactura.TipoDeDocumento AND factura.Numero = renglonCobroDeFactura.NumeroFactura AND"
   Sql = Sql & " factura.ConsecutivoCompania = renglonCobroDeFactura.ConsecutivoCompania"
'   SQL = SQL & " FROM (formaDelCobro INNER JOIN "
'   SQL = SQL & "((cliente INNER JOIN "
'   SQL = SQL & valFacturaTableName & " ON "
'   SQL = SQL & "(cliente.Codigo" & _
'               " = " & valFacturaTableName & "." & valFacturaCodigoCliente & ") "
'   SQL = SQL & " AND (cliente.ConsecutivoCompania" & _
'              " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & "))"
'   SQL = SQL & " INNER JOIN renglonCobroDeFactura ON "
'   SQL = SQL & " (" & valFacturaTableName & "." & valFacturaTipoDeDocumento & _
'               " = renglonCobroDeFactura.TipoDeDocumento)"
'   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaNumero & _
'              " = renglonCobroDeFactura.NumeroFactura)"
'   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
'              " = renglonCobroDeFactura.ConsecutivoCompania)) ON "
'   SQL = SQL & "formaDelCobro.Codigo" & _
'              " = renglonCobroDeFactura.CodigoFormaDelCobro)"
'   SQL = SQL & " INNER JOIN caja ON "
'   SQL = SQL & "(caja.ConsecutivoCompania" & _
'               " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & ") "
'   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
'              " = caja.ConsecutivoCaja)"
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   Sql = Sql & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   Sql = Sql & " GROUP BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & valFacturaTableName & "." & valComprobanteFiscal & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaFecha & ", "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & valGroupByDeFacturaSegunMoneda
   Sql = Sql & " ORDER BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & "." & valFacturaFecha & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & "cliente.Nombre"
   fSQLDelCuadreDeCajaPorTipoDeCobro = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaPorTipoDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelCuadreDeCajaDetallado(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valFacturaTableName As String, ByVal valFacturaTipoDeDocumento As String, ByVal valFacturaNumero As String, _
                        ByVal valFacturaConsecutivoCaja As String, ByVal valFacturaMoneda As String, ByVal valFacturaCodigoCliente As String, _
                        ByVal valStatusFactura As String, ByVal valFacturaGeneraCobroDirecto As String, ByVal valMontoFacturaSegunMoneda As String, _
                        ByVal valPagoSegunMoneda As String, ByVal valConsecutivoInicialDeCaja As Long, ByVal valComprobanteFiscal As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & valFacturaTableName & "." & valComprobanteFiscal & ", "
   Sql = Sql & valFacturaTableName & ".Fecha, "
   Sql = Sql & "cliente.Nombre AS NombreCliente, "
   Sql = Sql & "formaDelCobro.TipoDePago, "
   Sql = Sql & "formaDelCobro.Nombre AS FormaDePago, "
   Sql = Sql & valMontoFacturaSegunMoneda
   Sql = Sql & " AS MontoFactura, "
   Sql = Sql & valPagoSegunMoneda
   Sql = Sql & " AS MontoPagado "
   Sql = Sql & " FROM (formaDelCobro INNER JOIN "
   Sql = Sql & "((cliente INNER JOIN "
   Sql = Sql & valFacturaTableName & " ON "
   Sql = Sql & "(cliente.Codigo" & _
               " = " & valFacturaTableName & "." & valFacturaCodigoCliente & ") "
   Sql = Sql & " AND (cliente.ConsecutivoCompania" & _
              " = " & valFacturaTableName & ".ConsecutivoCompania))"
   Sql = Sql & " INNER JOIN renglonCobroDeFactura ON "
   Sql = Sql & " (" & valFacturaTableName & "." & valFacturaTipoDeDocumento & _
               " = renglonCobroDeFactura.TipoDeDocumento)"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaNumero & _
              " = renglonCobroDeFactura.NumeroFactura)"
   Sql = Sql & " AND (" & valFacturaTableName & ".ConsecutivoCompania" & _
              " = renglonCobroDeFactura.ConsecutivoCompania)) ON "
   Sql = Sql & "formaDelCobro.Codigo" & _
              " = renglonCobroDeFactura.CodigoFormaDelCobro)"
   Sql = Sql & " INNER JOIN caja ON "
   Sql = Sql & "(caja.ConsecutivoCompania" & _
               " = " & valFacturaTableName & ".ConsecutivoCompania) "
   Sql = Sql & "AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja)"
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & ".ConsecutivoCompania" & _
               " = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   Sql = Sql & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   Sql = Sql & " ORDER BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & ".Fecha, "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & "formaDelCobro.TipoDePago, "
   Sql = Sql & "formaDelCobro.Nombre"
   fSQLDelCuadreDeCajaDetallado = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelCuadreDeCajaDetalladoResumido(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valFacturaTableName As String, ByVal valFacturaFecha As String, ByVal valFacturaConsecutivoCompania As String, _
                        ByVal valFacturaTipoDeDocumento As String, ByVal valFacturaNumero As String, ByVal valFacturaConsecutivoCaja As String, _
                        ByVal valFacturaMoneda As String, ByVal valStatusFactura As String, ByVal valFacturaGeneraCobroDirecto As String, _
                        ByVal valPagoSegunMoneda As String, ByVal valConsecutivoInicialDeCaja As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "2 AS ORDEN, "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & "0 AS MontoFactura, "
   Sql = Sql & "formaDelCobro.Nombre AS FormaDePago, "
   Sql = Sql & "SUM (" & valPagoSegunMoneda & ") AS MontoPagado "
   Sql = Sql & " FROM (formaDelCobro INNER JOIN "
   Sql = Sql & "(" & valFacturaTableName & " INNER JOIN "
   Sql = Sql & "renglonCobroDeFactura ON "
   Sql = Sql & "(" & valFacturaTableName & "." & valFacturaTipoDeDocumento & _
               " = renglonCobroDeFactura.TipoDeDocumento) AND "
   Sql = Sql & "(" & valFacturaTableName & "." & valFacturaNumero & _
              " = renglonCobroDeFactura.NumeroFactura) AND "
   Sql = Sql & "(" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
              " = renglonCobroDeFactura.ConsecutivoCompania)) ON "
   Sql = Sql & "formaDelCobro.Codigo" & _
              " = renglonCobroDeFactura.CodigoFormaDelCobro)"
   Sql = Sql & " INNER JOIN caja ON "
   Sql = Sql & "(" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja) AND "
   Sql = Sql & "(caja.ConsecutivoCompania" & _
               " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & ")"
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   Sql = Sql & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   Sql = Sql & " GROUP BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & "formaDelCobro.Nombre"
   Sql = Sql & " ORDER BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja"
   fSQLDelCuadreDeCajaDetalladoResumido = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaDetalladoResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelCuadreDeCajaDetalladoResumidoMontoFactura(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valFacturaTableName As String, ByVal valFacturaFecha As String, ByVal valFacturaConsecutivoCompania As String, _
                        ByVal valFacturaConsecutivoCaja As String, ByVal valFacturaMoneda As String, ByVal valStatusFactura As String, _
                        ByVal valFacturaGeneraCobroDirecto As String, ByVal valMontoFacturaSegunMoneda As String, ByVal valConsecutivoInicialDeCaja As Long) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "1 AS ORDEN, "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & "SUM (" & valMontoFacturaSegunMoneda & ") AS MontoFactura, "
   Sql = Sql & "'' AS FormaDePago, "
   Sql = Sql & "0 AS MontoPagado "
   Sql = Sql & "FROM " & valFacturaTableName & " INNER JOIN "
   Sql = Sql & "caja ON "
   Sql = Sql & "(" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
              " = caja.ConsecutivoCompania)"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja) "
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   Sql = Sql & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   Sql = Sql & " GROUP BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja"
   fSQLDelCuadreDeCajaDetalladoResumidoMontoFactura = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaDetalladoResumidoMontoFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFormarSqlCuadreDeCajaMontoPagado(ByVal valPagoSegunMoneda As String) As String
   Dim vSql As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL)
   
   vCadenaResultadoStr = "Sum(" & valPagoSegunMoneda & ")"
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & "factura.TotalFactura"

   vSql = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "MontoPagado")

   fFormarSqlCuadreDeCajaMontoPagado = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormarSqlCuadreDeCajaMontoPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFormarSqlCuadreDeCajaVuelto(ByVal valVueltoSegunMoneda As String) As String
   Dim vSql As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL)
   
   vCadenaResultadoStr = valVueltoSegunMoneda
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & 0

   vSql = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "Vuelto")

   fFormarSqlCuadreDeCajaVuelto = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormarSqlCuadreDeCajaVuelto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'Private Function fSQLCuadreCajaResumenesAlicuota(ByVal valFechaInicial As Date, ByVal valFechaFinal As Date) As String
'   Dim SQL As String
'   On Error GoTo h_ERROR
'   SQL = "SELECT "
'   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_MONEDA & ", "
'   SQL = SQL & "SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_EXENTO), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ") AS MontoExento, "
'   SQL = SQL & "SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ") AS AlicuotaGeneral, "
'   SQL = SQL & "SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA2), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ") AS AlicuotaReducida, "
'   SQL = SQL & "SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA3), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ") AS AlicuotaAdicional, "
'   SQL = SQL & "(SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA3), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ") + SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTA2), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ") + SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_ALICUOTAGENERAL), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ") + SUM (" & gUtilSQL.getIIF(insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ALICUOTA_IVA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAlicuota.eTD_EXENTO), insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA & _
'               " - " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA, _
'               0, True) & ")) AS TotalAlicuotas "
'   SQL = SQL & " FROM " & insFactura.GetTableName & " INNER JOIN "
'   SQL = SQL & "(" & insArticuloInventario.GetTableName & " INNER JOIN "
'   SQL = SQL & insRenglonFactura.GetTableName & " ON "
'   SQL = SQL & " (" & insArticuloInventario.GetTableName & "." & insArticuloInventario.getFN_CODIGO & _
'               " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO & ") "
'   SQL = SQL & "AND (" & insArticuloInventario.GetTableName & "." & insArticuloInventario.getFN_CONSECUTIVO_COMPANIA & _
'              " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA & ")) ON "
'   SQL = SQL & "(" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & _
'              " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_TIPO_DE_DOCUMENTO & ") "
'   SQL = SQL & "AND (" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & _
'              " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_NUMERO_FACTURA & ") "
'   SQL = SQL & "AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
'              " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_CONSECUTIVO_COMPANIA & ") "
'   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(insFactura.getFN_FECHA, valFechaInicial, valFechaFinal) & ") "
'   SQL = SQL & "AND (" & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & _
'               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ") "
'   SQL = SQL & "AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
'               " = " & gProyCompaniaActual.GetConsecutivoCompania & ")"
'   SQL = SQL & "AND (" & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_CAJA & " <> " & insCaja.CM_COSECUTIVO_INICIAL_DE_CAJA & ") "
'   SQL = SQL & "AND (" & insFactura.GetTableName & "." & insFactura.getFN_GENERA_COBRO_DIRECTO & " = 'S')"
'   SQL = SQL & " GROUP BY "
'   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_MONEDA
'   SQL = SQL & " ORDER BY "
'   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_MONEDA
'   Set insFactura = Nothing
'   Set insCliente = Nothing
'   Set insReglonCobroDeFactura = Nothing
'   Set insFormaDelCobro = Nothing
'  fSQLCuadreCajaResumenesAlicuota = SQL
'h_EXIT: On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
'      "fSQLCuadreCajaResumenesAlicuota", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'

Public Function fSQLDelCuadreDeCajaPorUsuario(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valFacturaTableName As String, ByVal valFacturaFecha As String, _
                        ByVal valFacturaConsecutivoCompania As String, ByVal valFacturaTipoDeDocumento As String, _
                        ByVal valFacturaNumero As String, ByVal valFacturaConsecutivoCaja As String, _
                        ByVal valFacturaMoneda As String, ByVal valFacturaCodigoCliente As String, ByVal valStatusFactura As String, _
                        ByVal valFacturaGeneraCobroDirecto As String, _
                        ByVal valMontoFacturaSegunMoneda As String, ByVal valPagoSegunMoneda As String, _
                        ByVal valVueltoSegunMoneda As String, ByVal valGroupByDeFacturaSegunMoneda As String, _
                        ByVal valConsecutivoInicialDeCaja As Long, ByVal valNumeroComprobanteFiscal As String, _
                        Optional ByVal valNombreCajero As String, Optional ByVal valCantidadAImprimirCajero As String) As String
                        
   Dim Sql As String
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   Sql = "SELECT "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & "cajaapertura.NombreDelUsuario, "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & valFacturaTableName & "." & valNumeroComprobanteFiscal & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaFecha & ", "
   Sql = Sql & "cliente.Nombre AS NombreCliente, "
   Sql = Sql & valMontoFacturaSegunMoneda
   Sql = Sql & " AS MontoFactura, "
   Sql = Sql & fFormarSqlCuadreDeCajaPorUsuarioMontoPagado(valPagoSegunMoneda) & ", "
   Sql = Sql & fFormarSqlCuadreDeCajaPorUsuarioVuelto(valVueltoSegunMoneda)
   Sql = Sql & " FROM (formaDelCobro INNER JOIN "
   Sql = Sql & "((cliente INNER JOIN "
   Sql = Sql & valFacturaTableName & " ON "
   Sql = Sql & "(cliente.Codigo" & _
               " = " & valFacturaTableName & "." & valFacturaCodigoCliente & ") "
   Sql = Sql & " AND (cliente.ConsecutivoCompania" & _
              " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & "))"
   Sql = Sql & " INNER JOIN renglonCobroDeFactura ON "
   Sql = Sql & " (" & valFacturaTableName & "." & valFacturaTipoDeDocumento & _
   " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL) & "))"
   Sql = Sql & " AND (" & valFacturaTableName & ".Numero" & _
              " = renglonCobroDeFactura.NumeroFactura)"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
              " = renglonCobroDeFactura.ConsecutivoCompania)) ON "
   Sql = Sql & "formaDelCobro.Codigo" & _
              " = renglonCobroDeFactura.CodigoFormaDelCobro)"
   Sql = Sql & " INNER JOIN caja ON "
   Sql = Sql & "(caja.ConsecutivoCompania" & _
               " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & ") "
   Sql = Sql & "AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja)"
   Sql = Sql & " INNER JOIN cajaapertura ON "
   Sql = Sql & "(caja.ConsecutivoCompania = cajaapertura.ConsecutivoCompania) "
   Sql = Sql & "AND (caja.ConsecutivoCaja = cajaapertura.ConsecutivoCaja) "
   Sql = Sql & "AND " & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("factura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF("factura.horamodificacion<>''", "factura.horamodificacion", gUtilSQL.fSimpleSqlValue("00:00"), True) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00")) & " >= "
   Sql = Sql & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("cajaapertura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "cajaapertura.horaapertura" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00"))
   Sql = Sql & "AND " & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("factura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF("factura.horamodificacion<>''", "factura.horamodificacion", gUtilSQL.fSimpleSqlValue("00:00"), True) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00")) & " <= "
   Sql = Sql & gUtilSQL.getIIF("cajaapertura.cajacerrada = " & gUtilSQL.fSimpleSqlValue("N"), gUtilSQL.DfCDateSQL("getdate()"), gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("cajaapertura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "cajaapertura.horacierre" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00")), True)

   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaTableName & "." & valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   Sql = Sql & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   If valCantidadAImprimirCajero = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      Sql = Sql & " AND (cajaapertura.NombreDelUsuario = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreCajero)) & ")"
   End If
   Sql = Sql & " GROUP BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & "cajaapertura.NombreDelUsuario, "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & valFacturaTableName & "." & valNumeroComprobanteFiscal & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaFecha & ", "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & valGroupByDeFacturaSegunMoneda
   Sql = Sql & ", factura.TipoDeDocumento"
   Sql = Sql & " ORDER BY "
   Sql = Sql & valFacturaTableName & "." & valFacturaMoneda & ", "
   Sql = Sql & "cajaapertura.NombreDelUsuario, "
   Sql = Sql & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   Sql = Sql & "caja.NombreCaja, "
   Sql = Sql & valFacturaTableName & "." & valFacturaFecha & ", "
   Sql = Sql & valFacturaTableName & "." & valFacturaNumero & ", "
   Sql = Sql & "cliente.Nombre"
   
   fSQLDelCuadreDeCajaPorUsuario = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaPorUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fFormarSqlCuadreDeCajaPorUsuarioMontoPagado(ByVal valPagoSegunMoneda As String) As String
   Dim vSql As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL)
   
   vCadenaResultadoStr = "Sum(" & valPagoSegunMoneda & ")"
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & "factura.TotalFactura"
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & "Sum(" & valPagoSegunMoneda & ")"

   vSql = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "MontoPagado")

   fFormarSqlCuadreDeCajaPorUsuarioMontoPagado = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormarSqlCuadreDeCajaPorUsuarioMontoPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFormarSqlCuadreDeCajaPorUsuarioVuelto(ByVal valVueltoSegunMoneda As String) As String
   Dim vSql As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL)
   
   vCadenaResultadoStr = valVueltoSegunMoneda
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & 0
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & valVueltoSegunMoneda

   vSql = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "Vuelto")

   fFormarSqlCuadreDeCajaPorUsuarioVuelto = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormarSqlCuadreDeCajaPorUsuarioVuelto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelCuadreDeCajaPorTipoDeCobroYPorCajero(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valFacturaTableName As String, ByVal valFacturaFecha As String, _
                        ByVal valFacturaConsecutivoCompania As String, ByVal valFacturaTipoDeDocumento As String, _
                        ByVal valFacturaNumero As String, ByVal valFacturaConsecutivoCaja As String, _
                        ByVal valFacturaMoneda As String, ByVal valFacturaCodigoCliente As String, ByVal valStatusFactura As String, _
                        ByVal valFacturaGeneraCobroDirecto As String, _
                        ByVal valMontoFacturaSegunMoneda As String, ByVal valPagoSegunMoneda As String, _
                        ByVal valVueltoSegunMoneda As String, ByVal valGroupByDeFacturaSegunMoneda As String, _
                        ByVal valConsecutivoInicialDeCaja As Long, ByVal valComprobanteFiscal As String, ByVal valNombreCajero As String) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT"
   Sql = Sql & " anticipo.NombreOperador, anticipo.Moneda, anticipo.ConsecutivoCaja, caja.NombreCaja"
   Sql = Sql & ", " & gUtilSQL.fSimpleSqlValue("Ant: ") & "+ anticipo.Numero AS Numero"
   Sql = Sql & ", " & gUtilSQL.fSimpleSqlValue("") & " AS NumeroComprobanteFiscal, anticipo.Fecha"
   Sql = Sql & ", cliente.Nombre AS NombreCliente, anticipo.MontoTotal AS MontoFactura"
   Sql = Sql & ", 0 AS Cheque"
   Sql = Sql & ", 0 AS Efectivo"
   Sql = Sql & ", 0 AS Tarjeta"
   Sql = Sql & ", 0 AS Deposito"
   Sql = Sql & ", 0 AS AnticipoUsado"
   Sql = Sql & ", (anticipo.MontoTotal - anticipo.MontoUsado) AS AnticipoRestante, anticipo.MontoTotal AS MontoCobrado"
   Sql = Sql & ", 0 AS Vuelto"
   Sql = Sql & ", 0 AS NotaCredito"
   Sql = Sql & ", 0 AS Credito"
   Sql = Sql & " FROM"
   Sql = Sql & " anticipo INNER JOIN Caja ON Caja.ConsecutivoCompania = anticipo.ConsecutivoCompania"
   Sql = Sql & " AND anticipo.ConsecutivoCaja = Caja.ConsecutivoCaja INNER JOIN Cliente"
   Sql = Sql & " ON Cliente.Codigo = anticipo.CodigoCliente AND Cliente.ConsecutivoCompania = anticipo.ConsecutivoCompania"
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("anticipo.Fecha", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_VIGENTE) & " OR anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO) & ")"
   Sql = Sql & " AND (anticipo.ConsecutivoCompania = " & valConsecutivoCompania & ") AND (anticipo.AsociarAnticipoACaja = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   Sql = Sql & " AND (Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO) & ")"
   Sql = Sql & " UNION"
   Sql = Sql & " SELECT"
   Sql = Sql & " factura.NombreOperador"
   Sql = Sql & ", factura.Moneda AS Moneda"
   Sql = Sql & ", factura.ConsecutivoCaja"
   Sql = Sql & ", caja.NombreCaja"
   Sql = Sql & ", " & gUtilSQL.fSimpleSqlValue("Fac: ") & "+ factura.Numero AS Numero"
   Sql = Sql & ", factura.NumeroComprobanteFiscal"
   Sql = Sql & ", factura.Fecha"
   Sql = Sql & ", cliente.Nombre AS NombreCliente"
   Sql = Sql & ", factura.TotalFactura AS MontoFactura,"
   Sql = Sql & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_CHEQUE), valPagoSegunMoneda, _
               0, True), True) & " ) AS Cheque,"
   Sql = Sql & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_EFECTIVO), valPagoSegunMoneda, _
               0, True), True) & ") AS Efectivo,"
   Sql = Sql & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_TARJETA), valPagoSegunMoneda, _
               0, True), True) & ") AS Tarjeta,"
   Sql = Sql & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_DEPOSITO), valPagoSegunMoneda, _
               0, True), True) & ") AS Deposito,"
   Sql = Sql & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_ANTICIPO), valPagoSegunMoneda, _
               0, True), True) & ") AS AnticipoUsado,"
   Sql = Sql & " '0,00' AS AnticipoRestante,"
   Sql = Sql & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, valPagoSegunMoneda, True) & ")"
   Sql = Sql & " AS MontoCobrado, "
   Sql = Sql & gUtilSQL.getIIF(valVueltoSegunMoneda & " IS NULL ", 0, valVueltoSegunMoneda, True)
   Sql = Sql & " AS Vuelto,"
   Sql = Sql & " SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " < 0 OR " & valMontoFacturaSegunMoneda & " < 0", gUtilSQL.getIIF(valPagoSegunMoneda & " is null ", _
                  valMontoFacturaSegunMoneda, valPagoSegunMoneda, True), 0, True) & ") AS NotaCredito,"
   Sql = Sql & " SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL AND " & valMontoFacturaSegunMoneda & " > 0", valMontoFacturaSegunMoneda, 0, True) & ") AS Credito "
   Sql = Sql & " FROM"
   Sql = Sql & " Cliente INNER JOIN factura ON Cliente.Codigo = factura.CodigoCliente AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania"
   Sql = Sql & " INNER JOIN Caja ON Caja.ConsecutivoCompania = factura.ConsecutivoCompania AND factura.ConsecutivoCaja = Caja.ConsecutivoCaja"
   Sql = Sql & " LEFT OUTER JOIN FormaDelCobro INNER JOIN renglonCobroDeFactura ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro"
   Sql = Sql & " ON factura.TipoDeDocumento = renglonCobroDeFactura.TipoDeDocumento AND factura.Numero = renglonCobroDeFactura.NumeroFactura"
   Sql = Sql & " AND factura.ConsecutivoCompania = renglonCobroDeFactura.ConsecutivoCompania"
   Sql = Sql & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   Sql = Sql & " AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   If gTexto.DfLen(valNombreCajero) <> 0 Then
      Sql = Sql & " AND (" & valFacturaTableName & ".NombreOperador = " & gUtilSQL.fSimpleSqlValue(valNombreCajero) & ")"
   End If
   Sql = Sql & " GROUP BY"
   Sql = Sql & " factura.Moneda"
   Sql = Sql & ", factura.NombreOperador"
   Sql = Sql & ", factura.ConsecutivoCaja"
   Sql = Sql & ", caja.NombreCaja"
   Sql = Sql & ", factura.Numero"
   Sql = Sql & ", factura.NumeroComprobanteFiscal"
   Sql = Sql & ", factura.Fecha"
   Sql = Sql & ", cliente.Nombre"
   Sql = Sql & ", factura.TotalFactura"
   Sql = Sql & ", factura.CambioABolivares"
   fSQLDelCuadreDeCajaPorTipoDeCobroYPorCajero = Sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaPorTipoDeCobroYPorCajero", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
