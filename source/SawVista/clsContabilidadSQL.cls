VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsContabilidadSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsContabilidadSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Contabilidad"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConstruirSQLDelReporteAuditoriaContable(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insComprobante As Object, ByRef insFactura As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   gMessage.ProgrammerMessage "Falta agregar el query de Resumen Diario de Ventas"
   SQL = fConstruirSQLDelReporteAuditoriaContableFactura(valConsecutivoCompania, valFechaInicial, valFechaFinal, insFactura, insComprobante)
   SQL = SQL & " UNION "
   SQL = SQL & fConstruirSQLDelReporteAuditoriaContableCxC(valConsecutivoCompania, valFechaInicial, valFechaFinal, insComprobante)
   SQL = SQL & " UNION "
   SQL = SQL & fConstruirSQLDelReporteAuditoriaContableCxP(valConsecutivoCompania, valFechaInicial, valFechaFinal, insComprobante)
   SQL = SQL & " UNION "
   SQL = SQL & fConstruirSQLDelReporteAuditoriaContableMovBancario(valConsecutivoCompania, valFechaInicial, valFechaFinal, insComprobante)
   SQL = SQL & " UNION "
   SQL = SQL & fConstruirSQLDelReporteAuditoriaContableConbranza(valConsecutivoCompania, valFechaInicial, valFechaFinal, insComprobante)
   SQL = SQL & " UNION "
   SQL = SQL & fConstruirSQLDelReporteAuditoriaContablePago(valConsecutivoCompania, valFechaInicial, valFechaFinal, insComprobante)
h_EXIT:
   fConstruirSQLDelReporteAuditoriaContable = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAuditoriaContable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLDelReporteAuditoriaContableFactura(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insFactura As Object, ByRef insComprobante As Object)
   Dim SQL As String
   On Error GoTo h_ERROR
   insFactura.setClaseDeTrabajo eCTFC_Factura
   SQL = ""
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("FACTURA") & " AS GeneradoPor, "
   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_NUMERO & " AS NumeroDoc, "
   SQL = SQL & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & " AS MontoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA _
                & "<>" & insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalHaber", True) & " AS MontoComprobante,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fBooleanToStrSQL(True), _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS FueModificado,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), _
                insComprobante.GetTableName & "." & "NombreOperador", _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS QuienModifico"
   SQL = SQL & " FROM " & insComprobante.GetTableName & " INNER JOIN " & insFactura.GetTableName
   SQL = SQL & " ON (" & insComprobante.GetTableName & "." & "NoDocumentoOrigen" & _
               " = " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & _
               gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & _
               insFactura.GetTableName & "." & insFactura.getFN_NUMERO & ") "
   SQL = SQL & " WHERE " & insFactura.GetTableName & "." & insFactura.getFN_CONSECUTIVO_COMPANIA & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND (ABS(" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalDebe" & ")" & _
                   " OR ABS(" & insFactura.GetTableName & "." & insFactura.getFN_TOTAL_FACTURA & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalHaber" & ")) "
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "ConsecutivoPeriodo" & " IN (SELECT " & _
               "PERIODO.ConsecutivoPeriodo" & _
               " FROM  PERIODO WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "GeneradoPor" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_FACTURA))
   SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & _
               " <> " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusFactura.eSF_ANULADA))
   SQL = SQL & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA(), valFechaInicial, valFechaFinal)
   SQL = SQL & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
h_EXIT:
   fConstruirSQLDelReporteAuditoriaContableFactura = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAuditoriaContableFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLDelReporteAuditoriaContableCxC(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insComprobante As Object)
   Dim SQL As String
   Dim valMontoCXC As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("CUENTA POR COBRAR") & " AS GeneradoPor, "
   SQL = SQL & "cxC.Numero AS NumeroDoc, "
   valMontoCXC = "(" & "cxC.MontoExento" & _
                " + " & "cxC.MontoGravado" & _
                " + " & "cxC.MontoIVA)"
                '" - " & "cxC.MontoAbonado)"
   SQL = SQL & valMontoCXC & " AS MontoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(valMontoCXC _
                & "<>" & insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalHaber", True) & " AS MontoComprobante,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fBooleanToStrSQL(True), _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS FueModificado,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), _
                insComprobante.GetTableName & "." & "NombreOperador", _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS QuienModifico"
   SQL = SQL & " FROM " & insComprobante.GetTableName & " INNER JOIN cxC"
   SQL = SQL & " ON (" & insComprobante.GetTableName & "." & "NoDocumentoOrigen" & _
               " = " & "cxC.TipoCxC" & _
               gUtilSQL.CharConcat & "'" & gTexto.fSeparadorStandardDeElementosString & "'" & gUtilSQL.CharConcat & _
               "cxC.Numero) "
   SQL = SQL & " WHERE " & " cxC.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND (ABS(" & valMontoCXC & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalDebe" & ")" & _
                   " OR ABS(" & valMontoCXC & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalHaber" & ")) "
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "ConsecutivoPeriodo" & " IN (SELECT " & _
               "PERIODO.ConsecutivoPeriodo" & _
               " FROM  PERIODO WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "GeneradoPor" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXC))
   SQL = SQL & " AND " & "cxC.Status" & _
               " <> " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusDocumento.eSD_CANCELADO))
   SQL = SQL & " AND " & "cxC.Status" & _
               " <> " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO))
   SQL = SQL & " AND " & _
      gUtilSQL.DfSQLDateValueBetween("cxC.Fecha", _
      valFechaInicial, valFechaFinal)
h_EXIT:
   fConstruirSQLDelReporteAuditoriaContableCxC = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAuditoriaContableCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLDelReporteAuditoriaContableCxP(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insComprobante As Object)
   Dim SQL As String
   Dim valMontocxp As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("CUENTA POR PAGAR") & " AS GeneradoPor, "
   SQL = SQL & "cxP.Numero AS NumeroDoc, "
   valMontocxp = "(" & "cxP.MontoExento" & _
                " + " & "cxP.MontoGravado" & _
                " + " & "cxP.MontoIva)"
                '" - " & "cxP.MontoAbonado)"
   SQL = SQL & valMontocxp & " AS MontoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(valMontocxp _
                & "<>" & insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalHaber", True) & " AS MontoComprobante,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fBooleanToStrSQL(True), _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS FueModificado,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), _
                insComprobante.GetTableName & "." & "NombreOperador", _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS QuienModifico"
   SQL = SQL & " FROM " & insComprobante.GetTableName & " INNER JOIN cxP "
   SQL = SQL & " ON (" & insComprobante.GetTableName & "." & "NoDocumentoOrigen"
   SQL = SQL & " = " & "cxP.ConsecutivoCxP)"
   SQL = SQL & " WHERE " & " cxP.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND (ABS(" & valMontocxp & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalDebe" & ")" & _
                   " OR ABS(" & valMontocxp & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalHaber" & ")) "
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "ConsecutivoPeriodo" & " IN (SELECT " & _
               "PERIODO.ConsecutivoPeriodo" & _
               " FROM  PERIODO WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "GeneradoPor" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_CXP))
   SQL = SQL & " AND " & "cxP.Status" & _
               " <> " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusDocumento.eSD_CANCELADO))
   SQL = SQL & " AND " & "cxP.Status" & _
               " <> " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO))
   SQL = SQL & " AND " & _
      gUtilSQL.DfSQLDateValueBetween("cxP.Fecha", _
      valFechaInicial, valFechaFinal)
h_EXIT:
   fConstruirSQLDelReporteAuditoriaContableCxP = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAuditoriaContableCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLDelReporteAuditoriaContableMovBancario(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insComprobante As Object)
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("MOVIMIENTO BANCARIO") & " AS GeneradoPor, "
   SQL = SQL & "movimientoBancario.NumeroDocumento AS NumeroDoc, "
   SQL = SQL & "movimientoBancario.Monto AS MontoDocumento, "
   SQL = SQL & gUtilSQL.getIIF("movimientoBancario.Monto" _
                & "<>" & insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalHaber", True) & " AS MontoComprobante,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fBooleanToStrSQL(True), _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS FueModificado,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), _
                insComprobante.GetTableName & "." & "NombreOperador", _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS QuienModifico"
   SQL = SQL & " FROM " & insComprobante.GetTableName & " INNER JOIN movimientoBancario "
   SQL = SQL & " ON (" & insComprobante.GetTableName & "." & "NoDocumentoOrigen" & _
               " = " & gUtilSQL.DfCStrSQL("movimientoBancario.ConsecutivoMovimiento") & ") "
   SQL = SQL & " WHERE " & " movimientoBancario.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND (ABS(" & "movimientoBancario.Monto)" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalDebe" & ")" & _
                   " OR ABS(" & "movimientoBancario.Monto)" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalHaber" & ")) "
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "GeneradoPor" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_MOVIMIENTO_BANCARIO))
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "ConsecutivoPeriodo" & " IN (SELECT " & _
               "PERIODO.ConsecutivoPeriodo" & _
               " FROM  PERIODO WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND " & "movimientoBancario.GeneradoPor" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_USUARIO))
   SQL = SQL & " AND " & _
   gUtilSQL.DfSQLDateValueBetween("movimientoBancario.Fecha", _
   valFechaInicial, valFechaFinal)
h_EXIT:
   fConstruirSQLDelReporteAuditoriaContableMovBancario = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAuditoriaContableMovBancario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Function fConstruirSQLDelReporteAuditoriaContablePago(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insComprobante As Object)
   Dim SQL As String
   Dim valMontoPago As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("PAGO") & " AS GeneradoPor, "
   SQL = SQL & "pago.NumeroCheque AS NumeroDoc, "
   valMontoPago = "(" & "pago.MontoCheque" & _
                " + " & "pago.TotalRetenido" & _
                " + " & "pago.TotalRetenidoIVA" & _
                " - " & "pago.TotalOtros)"
   SQL = SQL & valMontoPago & " AS MontoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(valMontoPago _
                & "<>" & insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalHaber", True) & " AS MontoComprobante,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fBooleanToStrSQL(True), _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS FueModificado,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), _
                insComprobante.GetTableName & "." & "NombreOperador", _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS QuienModifico"
   SQL = SQL & " FROM " & insComprobante.GetTableName & " INNER JOIN Pago "
   SQL = SQL & " ON (" & insComprobante.GetTableName & "." & "NoDocumentoOrigen" & _
                " = " & gUtilSQL.DfCStrSQL("pago.NumeroComprobante") & ") "
   SQL = SQL & " WHERE " & "pago.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND (ABS(" & valMontoPago & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalDebe" & ")" & _
                   " OR ABS(" & valMontoPago & ")" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalHaber" & ")) "
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "GeneradoPor" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_PAGOS))
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "ConsecutivoPeriodo" & " IN (SELECT " & _
               "PERIODO.ConsecutivoPeriodo" & _
               " FROM  PERIODO WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND " & "pago.StatusOrdenDePago" & _
               " <> " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusPago.eSP_ANULADA))
   SQL = SQL & " AND " & _
      gUtilSQL.DfSQLDateValueBetween("pago.Fecha", _
      valFechaInicial, valFechaFinal)
h_EXIT:
   fConstruirSQLDelReporteAuditoriaContablePago = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAuditoriaContablePago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLDelReporteAuditoriaContableConbranza(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef insComprobante As Object)
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = ""
   SQL = "SELECT "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("COBRANZAS") & " AS GeneradoPor, "
   SQL = SQL & "cobranza.Numero AS NumeroDoc, "
   SQL = SQL & "cobranza.TotalCobrado AS MontoDocumento, "
   SQL = SQL & gUtilSQL.getIIF("cobranza.TotalCobrado" _
                & "<>" & insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalDebe", _
                insComprobante.GetTableName & "." & "TotalHaber", True) & " AS MontoComprobante,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), gUtilSQL.fBooleanToStrSQL(True), _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS FueModificado,"
   SQL = SQL & gUtilSQL.getIIF(insComprobante.GetTableName & "." & "FueModificado" _
                & "=" & gUtilSQL.fBooleanToStrSQL(True), _
                insComprobante.GetTableName & "." & "NombreOperador", _
                gUtilSQL.fSimpleSqlValue(" "), True) & "AS QuienModifico"
   SQL = SQL & " FROM " & insComprobante.GetTableName & " INNER JOIN cobranza "
   SQL = SQL & " ON (" & insComprobante.GetTableName & "." & "NoDocumentoOrigen" & _
               " = " & "cobranza.Numero) "
   SQL = SQL & " WHERE " & "cobranza.ConsecutivoCompania" & _
               " = " & valConsecutivoCompania
   SQL = SQL & " AND (ABS(" & "cobranza.TotalCobrado)" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalDebe" & ")" & _
                   " OR ABS(" & "cobranza.TotalCobrado)" & _
                   " <> ABS(" & insComprobante.GetTableName & "." & "TotalHaber" & ")) "
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "ConsecutivoPeriodo" & " IN (SELECT " & _
               "PERIODO.ConsecutivoPeriodo" & _
               " FROM  PERIODO WHERE PERIODO.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND " & insComprobante.GetTableName & "." & "GeneradoPor" & _
               " = " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_ComprobanteGeneradoPor.eCG_COBRANZA))
   SQL = SQL & " AND " & "cobranza.StatusCobranza" & _
               " <> " & gUtilSQL.fSimpleSqlValue(gConvert.enumerativoAChar(enum_StatusCobranza.eSC_ANULADA))
   SQL = SQL & " AND " & _
   gUtilSQL.DfSQLDateValueBetween("cobranza.Fecha", _
   valFechaInicial, valFechaFinal)
h_EXIT:
   fConstruirSQLDelReporteAuditoriaContableConbranza = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDelReporteAuditoriaContableConbranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


