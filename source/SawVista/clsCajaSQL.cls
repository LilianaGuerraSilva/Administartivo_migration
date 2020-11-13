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
   Dim vSQL As String
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
   
   vSQL = vSQL & "SELECT" & vbCrLf
   vSQL = vSQL & "      '1' AS ORDEN" & vbCrLf
   vSQL = vSQL & "      , factura.Moneda AS Moneda" & vbCrLf
   vSQL = vSQL & "      , factura.ConsecutivoCaja" & vbCrLf
   vSQL = vSQL & "      , caja.NombreCaja" & vbCrLf
   vSQL = vSQL & "      , factura.Numero" & vbCrLf
   vSQL = vSQL & "      , factura.NumeroComprobanteFiscal" & vbCrLf
   vSQL = vSQL & "      , factura.Fecha" & vbCrLf
   vSQL = vSQL & "      , cliente.Nombre AS NombreCliente" & vbCrLf
   vSQL = vSQL & "      , " & vSQLTotalFacturaEnMontoOriginal & " AS MontoFactura" & vbCrLf
   
   vSQL = vSQL & "      , CASE WHEN factura.TipoDeDocumento IN ('1','7') THEN" & vbCrLf
   
   vSQL = vSQL & "      " & vSQLTotalFacturaEnMontoOriginal & vbCrLf
   vSQL = vSQL & "       ELSE" & vbCrLf
   vSQL = vSQL & "         SUM ( (CASE WHEN " & vSQLMontoPago & "  IS NULL  OR renglonCobroDeFactura.Monto * ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1)  < 0  THEN 0 ELSE " & vSQLMontoPago & "  END ) ) END   AS MontoPagado" & vbCrLf
   vSQL = vSQL & "      ,  (CASE WHEN (SUM (renglonCobroDeFactura.Monto * ISNULL(renglonCobroDeFactura.CambioAMonedaLocal, 1)) - (factura.TotalFactura))  IS NULL  THEN" & vbCrLf
   vSQL = vSQL & "         0" & vbCrLf
   vSQL = vSQL & "         ELSE (SUM (" & vSQLMontoPago & ") - (" & vSQLTotalFacturaEnMontoOriginal & "))  END )  AS Vuelto" & vbCrLf
   vSQL = vSQL & "      , CASE WHEN SUM (CASE WHEN renglonCobroDeFactura.Monto IS NULL AND (factura.TotalFactura) > 0 THEN (factura.TotalFactura * factura.CambioABolivares)  ELSE 0 END )  > 0 AND factura.TipoDeDocumento IN ('0','5') THEN 'FACTURAS NO COBRADAS' ELSE  CASE WHEN factura.TipoDeDocumento IN ('0','5') THEN 'FACTURAS COBRADAS' ELSE  CASE WHEN factura.TipoDeDocumento IN ('1','7') THEN 'NOTAS DE CRÉDITO/DEVOLUCIONES' ELSE '' END   END   END   AS Grupo" & vbCrLf
   vSQL = vSQL & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '0' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Efectivo" & vbCrLf
   vSQL = vSQL & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '1' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Tarjeta" & vbCrLf
   vSQL = vSQL & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '2' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Cheque" & vbCrLf
   vSQL = vSQL & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '3' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Deposito" & vbCrLf
   vSQL = vSQL & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago = '4' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS Anticipo" & vbCrLf
   vSQL = vSQL & "      , SUM((CASE WHEN SAW.FormaDelCobro.TipoDePago <> '4' THEN " & vSQLMontoPago & " ELSE 0 END) ) AS TotalSinAnticipo" & vbCrLf
   vSQL = vSQL & "      , SUM( " & vSQLMontoPago & " ) AS TotalConAnticipo" & vbCrLf
   vSQL = vSQL & "      , factura.StatusFactura AS Status" & vbCrLf
   If valFiltrarPorCajero Then
      vSQL = vSQL & ", factura.NombreOperador  AS NombreOperador"
   End If
   vSQL = vSQL & "   FROM" & vbCrLf
   vSQL = vSQL & "      Cliente" & vbCrLf
   vSQL = vSQL & "   INNER JOIN" & vbCrLf
   vSQL = vSQL & "      factura" & vbCrLf
   vSQL = vSQL & "      ON Cliente.Codigo = factura.CodigoCliente AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & "   INNER JOIN Caja" & vbCrLf
   vSQL = vSQL & "      ON Caja.ConsecutivoCompania = factura.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & "      AND factura.ConsecutivoCaja = Caja.ConsecutivoCaja" & vbCrLf
   vSQL = vSQL & "   LEFT OUTER JOIN Saw.FormaDelCobro" & vbCrLf
   vSQL = vSQL & "   INNER JOIN renglonCobroDeFactura" & vbCrLf
   vSQL = vSQL & "      ON Saw.FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro" & vbCrLf
   vSQL = vSQL & "      ON factura.TipoDeDocumento = renglonCobroDeFactura.TipoDeDocumento" & vbCrLf
   vSQL = vSQL & "      AND factura.Numero = renglonCobroDeFactura.NumeroFactura" & vbCrLf
   vSQL = vSQL & "      AND factura.ConsecutivoCompania = renglonCobroDeFactura.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & "   WHERE" & vbCrLf
   vSQL = vSQL & "      (" & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   vSQL = vSQL & "      AND (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   vSQL = vSQL & "      AND (factura.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   vSQL = vSQL & "      AND (factura.ConsecutivoCaja <> " & valConsecutivoInicialDeCaja & ")"
   vSQL = vSQL & "      AND (factura.TipoDeDocumento IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & "))"
   vSQL = vSQL & "      AND (factura.GeneraCobroDirecto  = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   If valFiltrarPorCajero And gTexto.DfLen(valNombreCajero) > 0 Then
      vSQL = vSQL & "   AND factura.NombreOperador = " & gUtilSQL.fSimpleSqlValue(valNombreCajero)
   End If
   vSQL = vSQL & "    GROUP BY" & vbCrLf
   vSQL = vSQL & "      factura.Moneda" & vbCrLf
   vSQL = vSQL & "      , factura.ConsecutivoCaja" & vbCrLf
   vSQL = vSQL & "      , caja.NombreCaja" & vbCrLf
   vSQL = vSQL & "      , factura.Numero" & vbCrLf
   vSQL = vSQL & "      , factura.NumeroComprobanteFiscal" & vbCrLf
   vSQL = vSQL & "      , factura.Fecha, cliente.Nombre" & vbCrLf
   vSQL = vSQL & "      , factura.TotalFactura" & vbCrLf
   vSQL = vSQL & "      , factura.TipoDeDocumento" & vbCrLf
   vSQL = vSQL & "      , factura.StatusFactura" & vbCrLf
   If valFiltrarPorCajero Then
      vSQL = vSQL & "      , factura.NombreOperador " & vbCrLf
   End If
   vSQL = vSQL & "UNION" & vbCrLf
   vSQL = vSQL & "   SELECT" & vbCrLf
   vSQL = vSQL & "      '2' AS ORDEN" & vbCrLf
   vSQL = vSQL & "      , anticipo.Moneda, Caja.ConsecutivoCaja, Caja.NombreCaja, anticipo.Numero, '' AS NumeroComprobanteFiscal, anticipo.Fecha, Cliente.Nombre, anticipo.MontoTotal AS MontoTotalDocumento, anticipo.MontoTotal AS MontoPagado, '0,00' AS Vuelto, 'ANTICIPOS COBRADOS' AS Grupo, '0,00' AS Efectivo" & vbCrLf
   vSQL = vSQL & "      , '0,00' AS Tarjeta, '0,00' AS Cheque, anticipo.MontoTotal AS Deposito, '0,00' AS Anticipo" & vbCrLf
   vSQL = vSQL & "      , anticipo.MontoTotal AS TotalSinAnticipo" & vbCrLf
   vSQL = vSQL & "      , anticipo.MontoTotal AS TotalConAnticipo" & vbCrLf
   vSQL = vSQL & "      , anticipo.Status AS Status" & vbCrLf
   If valFiltrarPorCajero Then
      vSQL = vSQL & "  , anticipo.NombreOperador  AS NombreOperador " & vbCrLf
   End If
   vSQL = vSQL & "   FROM" & vbCrLf
   vSQL = vSQL & "      Caja" & vbCrLf
   vSQL = vSQL & "   INNER JOIN" & vbCrLf
   vSQL = vSQL & "      anticipo" & vbCrLf
   vSQL = vSQL & "      ON Caja.ConsecutivoCompania = anticipo.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & "      AND Caja.ConsecutivoCaja = anticipo.ConsecutivoCaja" & vbCrLf
   vSQL = vSQL & "   INNER JOIN Cliente" & vbCrLf
   vSQL = vSQL & "      ON anticipo.ConsecutivoCompania = Cliente.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & "      AND anticipo.CodigoCliente = Cliente.Codigo" & vbCrLf
   vSQL = vSQL & "   WHERE" & vbCrLf
   vSQL = vSQL & "      anticipo.AsociarAnticipoACaja ='S' AND "
   vSQL = vSQL & "      (Fecha BETWEEN '" & valFechaInicial & "' AND '" & valFechaFinal & "') AND "
   vSQL = vSQL & "      Tipo = '0' AND "
   vSQL = vSQL & "      anticipo.ConsecutivoCompania = " & valConsecutivoCompania
   vSQL = vSQL & "      AND anticipo.Status <> '1'"
   If valFiltrarPorCajero And gTexto.DfLen(valNombreCajero) > 0 Then
      vSQL = vSQL & "   AND anticipo.NombreOperador = " & gUtilSQL.fSimpleSqlValue(valNombreCajero)
   End If
   vSQL = vSQL & "   ORDER BY" & vbCrLf
   If valFiltrarPorCajero Then
      vSQL = vSQL & "   NombreOperador ,"
   End If
   vSQL = vSQL & "      Moneda" & vbCrLf
   vSQL = vSQL & "      , factura.ConsecutivoCaja" & vbCrLf
   vSQL = vSQL & "      , ORDEN" & vbCrLf
   vSQL = vSQL & "      , Grupo" & vbCrLf
   vSQL = vSQL & "      , caja.NombreCaja" & vbCrLf
   vSQL = vSQL & "      , factura.Fecha" & vbCrLf
   vSQL = vSQL & "      , factura.Numero" & vbCrLf
   vSQL = vSQL & "      , cliente.Nombre" & vbCrLf

   fSQLDelCuadreDeCaja = vSQL
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
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & valFacturaTableName & "." & valComprobanteFiscal & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaFecha & ", "
   SQL = SQL & "cliente.Nombre AS NombreCliente, "
   SQL = SQL & valMontoFacturaSegunMoneda
   SQL = SQL & " AS MontoFactura, "
   SQL = SQL & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_CHEQUE), valPagoSegunMoneda, _
               0, True), True) & " ) AS Cheque, "
   SQL = SQL & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_EFECTIVO), valPagoSegunMoneda, _
               0, True), True) & ") AS Efectivo, "
   SQL = SQL & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_TARJETA), valPagoSegunMoneda, _
               0, True), True) & ") AS Tarjeta, "
   SQL = SQL & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_DEPOSITO), valPagoSegunMoneda, _
               0, True), True) & ") AS Deposito, "
   SQL = SQL & "SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, valPagoSegunMoneda, True) & ")"
   SQL = SQL & " AS MontoPagado, "
   SQL = SQL & gUtilSQL.getIIF(valVueltoSegunMoneda & " IS NULL ", 0, valVueltoSegunMoneda, True)
   SQL = SQL & " AS Vuelto "
   SQL = SQL & ", SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " < 0 OR " & valMontoFacturaSegunMoneda & " < 0", gUtilSQL.getIIF(valPagoSegunMoneda & " is null ", valMontoFacturaSegunMoneda, valPagoSegunMoneda, True), 0, True) & ") AS NotaCredito"
   SQL = SQL & ", SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL AND " & valMontoFacturaSegunMoneda & " > 0", valMontoFacturaSegunMoneda, 0, True) & ") AS Credito "
   SQL = SQL & " FROM Cliente INNER JOIN"
   SQL = SQL & " factura ON Cliente.Codigo = factura.CodigoCliente AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania INNER JOIN"
   SQL = SQL & " Caja ON Caja.ConsecutivoCompania = factura.ConsecutivoCompania AND factura.ConsecutivoCaja = Caja.ConsecutivoCaja LEFT OUTER JOIN"
   SQL = SQL & " FormaDelCobro INNER JOIN"
   SQL = SQL & " renglonCobroDeFactura ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro ON"
   SQL = SQL & " factura.TipoDeDocumento = renglonCobroDeFactura.TipoDeDocumento AND factura.Numero = renglonCobroDeFactura.NumeroFactura AND"
   SQL = SQL & " factura.ConsecutivoCompania = renglonCobroDeFactura.ConsecutivoCompania"
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
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   SQL = SQL & " GROUP BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & valFacturaTableName & "." & valComprobanteFiscal & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaFecha & ", "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & valGroupByDeFacturaSegunMoneda
   SQL = SQL & " ORDER BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & "." & valFacturaFecha & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & "cliente.Nombre"
   fSQLDelCuadreDeCajaPorTipoDeCobro = SQL
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
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & valFacturaTableName & "." & valComprobanteFiscal & ", "
   SQL = SQL & valFacturaTableName & ".Fecha, "
   SQL = SQL & "cliente.Nombre AS NombreCliente, "
   SQL = SQL & "formaDelCobro.TipoDePago, "
   SQL = SQL & "formaDelCobro.Nombre AS FormaDePago, "
   SQL = SQL & valMontoFacturaSegunMoneda
   SQL = SQL & " AS MontoFactura, "
   SQL = SQL & valPagoSegunMoneda
   SQL = SQL & " AS MontoPagado "
   SQL = SQL & " FROM (formaDelCobro INNER JOIN "
   SQL = SQL & "((cliente INNER JOIN "
   SQL = SQL & valFacturaTableName & " ON "
   SQL = SQL & "(cliente.Codigo" & _
               " = " & valFacturaTableName & "." & valFacturaCodigoCliente & ") "
   SQL = SQL & " AND (cliente.ConsecutivoCompania" & _
              " = " & valFacturaTableName & ".ConsecutivoCompania))"
   SQL = SQL & " INNER JOIN renglonCobroDeFactura ON "
   SQL = SQL & " (" & valFacturaTableName & "." & valFacturaTipoDeDocumento & _
               " = renglonCobroDeFactura.TipoDeDocumento)"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaNumero & _
              " = renglonCobroDeFactura.NumeroFactura)"
   SQL = SQL & " AND (" & valFacturaTableName & ".ConsecutivoCompania" & _
              " = renglonCobroDeFactura.ConsecutivoCompania)) ON "
   SQL = SQL & "formaDelCobro.Codigo" & _
              " = renglonCobroDeFactura.CodigoFormaDelCobro)"
   SQL = SQL & " INNER JOIN caja ON "
   SQL = SQL & "(caja.ConsecutivoCompania" & _
               " = " & valFacturaTableName & ".ConsecutivoCompania) "
   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja)"
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("Fecha", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & ".ConsecutivoCompania" & _
               " = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   SQL = SQL & " ORDER BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & ".Fecha, "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "formaDelCobro.TipoDePago, "
   SQL = SQL & "formaDelCobro.Nombre"
   fSQLDelCuadreDeCajaDetallado = SQL
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
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "2 AS ORDEN, "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & "0 AS MontoFactura, "
   SQL = SQL & "formaDelCobro.Nombre AS FormaDePago, "
   SQL = SQL & "SUM (" & valPagoSegunMoneda & ") AS MontoPagado "
   SQL = SQL & " FROM (formaDelCobro INNER JOIN "
   SQL = SQL & "(" & valFacturaTableName & " INNER JOIN "
   SQL = SQL & "renglonCobroDeFactura ON "
   SQL = SQL & "(" & valFacturaTableName & "." & valFacturaTipoDeDocumento & _
               " = renglonCobroDeFactura.TipoDeDocumento) AND "
   SQL = SQL & "(" & valFacturaTableName & "." & valFacturaNumero & _
              " = renglonCobroDeFactura.NumeroFactura) AND "
   SQL = SQL & "(" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
              " = renglonCobroDeFactura.ConsecutivoCompania)) ON "
   SQL = SQL & "formaDelCobro.Codigo" & _
              " = renglonCobroDeFactura.CodigoFormaDelCobro)"
   SQL = SQL & " INNER JOIN caja ON "
   SQL = SQL & "(" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja) AND "
   SQL = SQL & "(caja.ConsecutivoCompania" & _
               " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & ")"
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   SQL = SQL & " GROUP BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & "formaDelCobro.Nombre"
   SQL = SQL & " ORDER BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja"
   fSQLDelCuadreDeCajaDetalladoResumido = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaDetalladoResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLDelCuadreDeCajaDetalladoResumidoMontoFactura(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, _
                        ByVal valFacturaTableName As String, ByVal valFacturaFecha As String, ByVal valFacturaConsecutivoCompania As String, _
                        ByVal valFacturaConsecutivoCaja As String, ByVal valFacturaMoneda As String, ByVal valStatusFactura As String, _
                        ByVal valFacturaGeneraCobroDirecto As String, ByVal valMontoFacturaSegunMoneda As String, ByVal valConsecutivoInicialDeCaja As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "1 AS ORDEN, "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & "SUM (" & valMontoFacturaSegunMoneda & ") AS MontoFactura, "
   SQL = SQL & "'' AS FormaDePago, "
   SQL = SQL & "0 AS MontoPagado "
   SQL = SQL & "FROM " & valFacturaTableName & " INNER JOIN "
   SQL = SQL & "caja ON "
   SQL = SQL & "(" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
              " = caja.ConsecutivoCompania)"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja) "
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   SQL = SQL & " GROUP BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja"
   fSQLDelCuadreDeCajaDetalladoResumidoMontoFactura = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaDetalladoResumidoMontoFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFormarSqlCuadreDeCajaMontoPagado(ByVal valPagoSegunMoneda As String) As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   
   vCadenaResultadoStr = "Sum(" & valPagoSegunMoneda & ")"
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & "factura.TotalFactura"

   vSQL = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "MontoPagado")

   fFormarSqlCuadreDeCajaMontoPagado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormarSqlCuadreDeCajaMontoPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFormarSqlCuadreDeCajaVuelto(ByVal valVueltoSegunMoneda As String) As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   
   vCadenaResultadoStr = valVueltoSegunMoneda
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & 0

   vSQL = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "Vuelto")

   fFormarSqlCuadreDeCajaVuelto = vSQL
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
                        
   Dim SQL As String
   Dim gEnumReport As clsEnumReport
   On Error GoTo h_ERROR
   Set gEnumReport = New clsEnumReport
   SQL = "SELECT "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & "cajaapertura.NombreDelUsuario, "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & valFacturaTableName & "." & valNumeroComprobanteFiscal & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaFecha & ", "
   SQL = SQL & "cliente.Nombre AS NombreCliente, "
   SQL = SQL & valMontoFacturaSegunMoneda
   SQL = SQL & " AS MontoFactura, "
   SQL = SQL & fFormarSqlCuadreDeCajaPorUsuarioMontoPagado(valPagoSegunMoneda) & ", "
   SQL = SQL & fFormarSqlCuadreDeCajaPorUsuarioVuelto(valVueltoSegunMoneda)
   SQL = SQL & " FROM (formaDelCobro INNER JOIN "
   SQL = SQL & "((cliente INNER JOIN "
   SQL = SQL & valFacturaTableName & " ON "
   SQL = SQL & "(cliente.Codigo" & _
               " = " & valFacturaTableName & "." & valFacturaCodigoCliente & ") "
   SQL = SQL & " AND (cliente.ConsecutivoCompania" & _
              " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & "))"
   SQL = SQL & " INNER JOIN renglonCobroDeFactura ON "
   SQL = SQL & " (" & valFacturaTableName & "." & valFacturaTipoDeDocumento & _
   " IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & ", " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL) & "))"
   SQL = SQL & " AND (" & valFacturaTableName & ".Numero" & _
              " = renglonCobroDeFactura.NumeroFactura)"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
              " = renglonCobroDeFactura.ConsecutivoCompania)) ON "
   SQL = SQL & "formaDelCobro.Codigo" & _
              " = renglonCobroDeFactura.CodigoFormaDelCobro)"
   SQL = SQL & " INNER JOIN caja ON "
   SQL = SQL & "(caja.ConsecutivoCompania" & _
               " = " & valFacturaTableName & "." & valFacturaConsecutivoCompania & ") "
   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & _
              " = caja.ConsecutivoCaja)"
   SQL = SQL & " INNER JOIN cajaapertura ON "
   SQL = SQL & "(caja.ConsecutivoCompania = cajaapertura.ConsecutivoCompania) "
   SQL = SQL & "AND (caja.ConsecutivoCaja = cajaapertura.ConsecutivoCaja) "
   SQL = SQL & "AND " & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("factura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF("factura.horamodificacion<>''", "factura.horamodificacion", gUtilSQL.fSimpleSqlValue("00:00"), True) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00")) & " >= "
   SQL = SQL & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("cajaapertura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "cajaapertura.horaapertura" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00"))
   SQL = SQL & "AND " & gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("factura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & gUtilSQL.getIIF("factura.horamodificacion<>''", "factura.horamodificacion", gUtilSQL.fSimpleSqlValue("00:00"), True) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00")) & " <= "
   SQL = SQL & gUtilSQL.getIIF("cajaapertura.cajacerrada = " & gUtilSQL.fSimpleSqlValue("N"), gUtilSQL.DfCDateSQL("getdate()"), gUtilSQL.DfCDateSQL(gUtilSQL.DfSQLFormat("cajaapertura.fecha", "dd/MM/yyyy", "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(" ") & gUtilSQL.CharConcat & "cajaapertura.horacierre" & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue(":00")), True)

   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween(valFacturaTableName & "." & valFacturaFecha, valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   SQL = SQL & "AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = 'S')"
   If valCantidadAImprimirCajero = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND (cajaapertura.NombreDelUsuario = " & gUtilSQL.fSimpleSqlValue(Trim(valNombreCajero)) & ")"
   End If
   SQL = SQL & " GROUP BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & "cajaapertura.NombreDelUsuario, "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & valFacturaTableName & "." & valNumeroComprobanteFiscal & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaFecha & ", "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & valGroupByDeFacturaSegunMoneda
   SQL = SQL & ", factura.TipoDeDocumento"
   SQL = SQL & " ORDER BY "
   SQL = SQL & valFacturaTableName & "." & valFacturaMoneda & ", "
   SQL = SQL & "cajaapertura.NombreDelUsuario, "
   SQL = SQL & valFacturaTableName & "." & valFacturaConsecutivoCaja & ", "
   SQL = SQL & "caja.NombreCaja, "
   SQL = SQL & valFacturaTableName & "." & valFacturaFecha & ", "
   SQL = SQL & valFacturaTableName & "." & valFacturaNumero & ", "
   SQL = SQL & "cliente.Nombre"
   
   fSQLDelCuadreDeCajaPorUsuario = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaPorUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fFormarSqlCuadreDeCajaPorUsuarioMontoPagado(ByVal valPagoSegunMoneda As String) As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_FACTURA)
   
   vCadenaResultadoStr = "Sum(" & valPagoSegunMoneda & ")"
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & "factura.TotalFactura"
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & "Sum(" & valPagoSegunMoneda & ")"

   vSQL = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "MontoPagado")

   fFormarSqlCuadreDeCajaPorUsuarioMontoPagado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFormarSqlCuadreDeCajaPorUsuarioMontoPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fFormarSqlCuadreDeCajaPorUsuarioVuelto(ByVal valVueltoSegunMoneda As String) As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vSeparador As String
   On Error GoTo h_ERROR
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_FACTURA)
   
   vCadenaResultadoStr = valVueltoSegunMoneda
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & 0
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & valVueltoSegunMoneda

   vSQL = gUtilSQL.DfSQLCaseIf("factura.TipoDeDocumento", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "Vuelto")

   fFormarSqlCuadreDeCajaPorUsuarioVuelto = vSQL
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
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT"
   SQL = SQL & " anticipo.NombreOperador, anticipo.Moneda, anticipo.ConsecutivoCaja, caja.NombreCaja"
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("Ant: ") & "+ anticipo.Numero AS Numero"
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("") & " AS NumeroComprobanteFiscal, anticipo.Fecha"
   SQL = SQL & ", cliente.Nombre AS NombreCliente, anticipo.MontoTotal AS MontoFactura"
   SQL = SQL & ", 0 AS Cheque"
   SQL = SQL & ", 0 AS Efectivo"
   SQL = SQL & ", 0 AS Tarjeta"
   SQL = SQL & ", 0 AS Deposito"
   SQL = SQL & ", 0 AS AnticipoUsado"
   SQL = SQL & ", (anticipo.MontoTotal - anticipo.MontoUsado) AS AnticipoRestante, anticipo.MontoTotal AS MontoCobrado"
   SQL = SQL & ", 0 AS Vuelto"
   SQL = SQL & ", 0 AS NotaCredito"
   SQL = SQL & ", 0 AS Credito"
   SQL = SQL & " FROM"
   SQL = SQL & " anticipo INNER JOIN Caja ON Caja.ConsecutivoCompania = anticipo.ConsecutivoCompania"
   SQL = SQL & " AND anticipo.ConsecutivoCaja = Caja.ConsecutivoCaja INNER JOIN Cliente"
   SQL = SQL & " ON Cliente.Codigo = anticipo.CodigoCliente AND Cliente.ConsecutivoCompania = anticipo.ConsecutivoCompania"
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("anticipo.Fecha", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_VIGENTE) & " OR anticipo.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusAnticipo.eSDA_PARCIALMENTE_USADO) & ")"
   SQL = SQL & " AND (anticipo.ConsecutivoCompania = " & valConsecutivoCompania & ") AND (anticipo.AsociarAnticipoACaja = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   SQL = SQL & " AND (Anticipo.Tipo = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeAnticipo.eTDA_COBRADO) & ")"
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT"
   SQL = SQL & " factura.NombreOperador"
   SQL = SQL & ", factura.Moneda AS Moneda"
   SQL = SQL & ", factura.ConsecutivoCaja"
   SQL = SQL & ", caja.NombreCaja"
   SQL = SQL & ", " & gUtilSQL.fSimpleSqlValue("Fac: ") & "+ factura.Numero AS Numero"
   SQL = SQL & ", factura.NumeroComprobanteFiscal"
   SQL = SQL & ", factura.Fecha"
   SQL = SQL & ", cliente.Nombre AS NombreCliente"
   SQL = SQL & ", factura.TotalFactura AS MontoFactura,"
   SQL = SQL & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_CHEQUE), valPagoSegunMoneda, _
               0, True), True) & " ) AS Cheque,"
   SQL = SQL & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_EFECTIVO), valPagoSegunMoneda, _
               0, True), True) & ") AS Efectivo,"
   SQL = SQL & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_TARJETA), valPagoSegunMoneda, _
               0, True), True) & ") AS Tarjeta,"
   SQL = SQL & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_DEPOSITO), valPagoSegunMoneda, _
               0, True), True) & ") AS Deposito,"
   SQL = SQL & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, gUtilSQL.getIIF("formaDelCobro.TipoDePago" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeFormaDePago.eTDA_ANTICIPO), valPagoSegunMoneda, _
               0, True), True) & ") AS AnticipoUsado,"
   SQL = SQL & " '0,00' AS AnticipoRestante,"
   SQL = SQL & " SUM (" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL  OR " & valPagoSegunMoneda & " < 0 ", 0, valPagoSegunMoneda, True) & ")"
   SQL = SQL & " AS MontoCobrado, "
   SQL = SQL & gUtilSQL.getIIF(valVueltoSegunMoneda & " IS NULL ", 0, valVueltoSegunMoneda, True)
   SQL = SQL & " AS Vuelto,"
   SQL = SQL & " SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " < 0 OR " & valMontoFacturaSegunMoneda & " < 0", gUtilSQL.getIIF(valPagoSegunMoneda & " is null ", _
                  valMontoFacturaSegunMoneda, valPagoSegunMoneda, True), 0, True) & ") AS NotaCredito,"
   SQL = SQL & " SUM(" & gUtilSQL.getIIF(valPagoSegunMoneda & " IS NULL AND " & valMontoFacturaSegunMoneda & " > 0", valMontoFacturaSegunMoneda, 0, True) & ") AS Credito "
   SQL = SQL & " FROM"
   SQL = SQL & " Cliente INNER JOIN factura ON Cliente.Codigo = factura.CodigoCliente AND Cliente.ConsecutivoCompania = factura.ConsecutivoCompania"
   SQL = SQL & " INNER JOIN Caja ON Caja.ConsecutivoCompania = factura.ConsecutivoCompania AND factura.ConsecutivoCaja = Caja.ConsecutivoCaja"
   SQL = SQL & " LEFT OUTER JOIN FormaDelCobro INNER JOIN renglonCobroDeFactura ON FormaDelCobro.Codigo = renglonCobroDeFactura.CodigoFormaDelCobro"
   SQL = SQL & " ON factura.TipoDeDocumento = renglonCobroDeFactura.TipoDeDocumento AND factura.Numero = renglonCobroDeFactura.NumeroFactura"
   SQL = SQL & " AND factura.ConsecutivoCompania = renglonCobroDeFactura.ConsecutivoCompania"
   SQL = SQL & " WHERE (" & gUtilSQL.DfSQLDateValueBetween("factura.Fecha", valFechaInicial, valFechaFinal) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valStatusFactura & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCompania & _
               " = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaConsecutivoCaja & " <> " & valConsecutivoInicialDeCaja & ")"
   SQL = SQL & " AND (" & valFacturaTableName & "." & valFacturaGeneraCobroDirecto & " = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   If gTexto.DfLen(valNombreCajero) <> 0 Then
      SQL = SQL & " AND (" & valFacturaTableName & ".NombreOperador = " & gUtilSQL.fSimpleSqlValue(valNombreCajero) & ")"
   End If
   SQL = SQL & " GROUP BY"
   SQL = SQL & " factura.Moneda"
   SQL = SQL & ", factura.NombreOperador"
   SQL = SQL & ", factura.ConsecutivoCaja"
   SQL = SQL & ", caja.NombreCaja"
   SQL = SQL & ", factura.Numero"
   SQL = SQL & ", factura.NumeroComprobanteFiscal"
   SQL = SQL & ", factura.Fecha"
   SQL = SQL & ", cliente.Nombre"
   SQL = SQL & ", factura.TotalFactura"
   SQL = SQL & ", factura.CambioABolivares"
   fSQLDelCuadreDeCajaPorTipoDeCobroYPorCajero = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLDelCuadreDeCajaPorTipoDeCobroYPorCajero", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
