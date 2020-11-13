VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPagoVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsPagoVista"
Private Const CM_MESSAGE_NAME As String = "Vista Pago"
Private gEnumProyecto As clsEnumAdministrativo

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNameRetencionXDocumentoPagado() As String
   GetViewNameRetencionXDocumentoPagado = "IGV_RetencionXDocumentoPagado"
End Function

Public Function fSqlViewRetencionXDocumentoPagado() As String
   Dim vSQL As String
   Dim insLibVista As clsLibSawVista
   On Error GoTo h_ERROR
   Set insLibVista = New clsLibSawVista
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT "
   vSQL = vSQL & "proveedor.ConsecutivoCompania, "
   vSQL = vSQL & "proveedor.CodigoProveedor, "
   vSQL = vSQL & "proveedor.NumeroRIF, "
   vSQL = vSQL & "proveedor.NombreProveedor, "
   vSQL = vSQL & "proveedor.ApellidoPaterno, "
   vSQL = vSQL & "proveedor.ApellidoMaterno, "
   vSQL = vSQL & "proveedor.Nombre, "
   vSQL = vSQL & "cxP.FechaAplicacionRetIva, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("cxP.NumeroComprobanteRetencion", "-") & " - 1 > 0", gUtilSQL.fMid("cxP.NumeroComprobanteRetencion", "1", gUtilSQL.DfInStrSQL("cxP.NumeroComprobanteRetencion", "-") & " - 1"), "0", True) & " AS SerieComprobanteRetencion, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("cxP.NumeroComprobanteRetencion", "-") & " - 1 > 0", gUtilSQL.fMid("cxP.NumeroComprobanteRetencion", gUtilSQL.DfInStrSQL("cxP.NumeroComprobanteRetencion", "-") & " + 1", "10"), "cxP.NumeroComprobanteRetencion", True) & " AS NumeroComprobanteRetencion, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("cxp.Numero", "-") & " - 1 > 0", gUtilSQL.fMid("cxp.Numero", "1", gUtilSQL.DfInStrSQL("cxp.Numero", "-") & " - 1"), "0", True) & " AS SerieNumeroComprobantePago, "
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("cxp.Numero", "-") & " - 1 > 0", gUtilSQL.fMid("cxp.Numero", gUtilSQL.DfInStrSQL("cxp.Numero", "-") & " + 1", "10"), "cxp.Numero", True) & " AS NumeroComprobantePago, "
   vSQL = vSQL & gUtilSQL.DfSQLFormat("cxp.Fecha", "dd/mm/yyyy", "") & " AS Fecha, "
   vSQL = vSQL & "(cxp.MontoExento + cxp.MontoGravado + cxp.MontoIva)AS MontoAplicado, "
   vSQL = vSQL & "cxP.CambioAbolivares, "
   vSQL = vSQL & "cxP.TipoDeTransaccion, "
   vSQL = vSQL & "documentoPagado.RetenerIva, "
   vSQL = vSQL & "documentoPagado.CodigoMonedaDeCxP, "
   vSQL = vSQL & "documentoPagado.CambioAMonedaDelPago, "
   vSQL = vSQL & "documentoPagado.MontoIvaRetenido, "
   vSQL = vSQL & "pago.StatusOrdenDePago, "
   vSQL = vSQL & "TipoDeDocumentoLey.Codigo, "
   vSQL = vSQL & "IGV_EnumTipoDocumentoIdentificacion.CodPeruTipoDocumentoIdentificacion "
   vSQL = vSQL & "FROM cxP "
   vSQL = vSQL & "INNER JOIN proveedor "
   vSQL = vSQL & "ON cxP.ConsecutivoCompania = proveedor.ConsecutivoCompania "
   vSQL = vSQL & " AND cxP.CodigoProveedor = proveedor.CodigoProveedor "
   vSQL = vSQL & "INNER JOIN pago "
   vSQL = vSQL & "ON proveedor.ConsecutivoCompania = pago.ConsecutivoCompania "
   vSQL = vSQL & " AND proveedor.CodigoProveedor = pago.CodigoProveedor "
   vSQL = vSQL & "INNER JOIN documentoPagado "
   vSQL = vSQL & "ON pago.ConsecutivoCompania = documentoPagado.ConsecutivoCompania "
   vSQL = vSQL & " AND pago.NumeroComprobante = documentoPagado.NumeroComprobante "
   vSQL = vSQL & " AND cxP.Numero = documentoPagado.NumeroDelDocumentoPagado "
   vSQL = vSQL & "INNER JOIN TipoDeDocumentoLey "
   vSQL = vSQL & "ON TipoDeDocumentoLey.TipoDeDocumento = cxP.TipoDeTransaccion "
   vSQL = vSQL & "INNER JOIN IGV_EnumTipoDocumentoIdentificacion "
   vSQL = vSQL & "ON  proveedor.TipoDocumentoIdentificacion " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = IGV_EnumTipoDocumentoIdentificacion.EnumValue "
   vSQL = vSQL & "WHERE (cxp.SeHizoLaRetencionIva = " & gUtilSQL.fBooleanToSqlValue(True) & ")"
   vSQL = vSQL & " AND (cxp.MontoRetenido <> 0)"
   vSQL = vSQL & " AND (CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_NoDeducible) & ")"
   vSQL = vSQL & " AND (documentoPagado.RetenerIva = " & gUtilSQL.fBooleanToSqlValue(False) & ")"
   vSQL = vSQL & " AND (pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE) & ")"
   fSqlViewRetencionXDocumentoPagado = vSQL
   Set insLibVista = Nothing
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRetencionXDocumentoPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameLibroDeRetencionesRenta() As String
   GetViewNameLibroDeRetencionesRenta = "IGV_LibroRetencionesRenta_B1"
End Function

Public Function fSqlViewLibroDeRetencionesRenta() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo

   vSQL = "SELECT "
   vSQL = vSQL & " pago.ConsecutivoCompania, "
   vSQL = vSQL & " pago.NumeroComprobante, "
   vSQL = vSQL & " pago.Fecha, "
   vSQL = vSQL & " pago.CodigoProveedor, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("proveedor.TipoDocumentoIdentificacion", enum_TipoDocumentoIdentificacionPeru.eTDIP_RUC, _
                     gEnumProyecto.fEnumTipoDocumentoIdentificacionPeruToStringInArray(True), "") & " AS TipoDocumentoIdentificacion, "
   vSQL = vSQL & " proveedor.NumeroRIF, "
   vSQL = vSQL & " proveedor.NombreProveedor, "
   vSQL = vSQL & " pago.CodigoMoneda, "
   vSQL = vSQL & " pago.Moneda, "
   vSQL = vSQL & " pago.CambioaBolivares, "
   vSQL = vSQL & " pago.TotalDocumentos, "
   vSQL = vSQL & " pago.TotalRetenido "
   vSQL = vSQL & " FROM pago INNER JOIN "
   vSQL = vSQL & " proveedor ON pago.ConsecutivoCompania = proveedor.ConsecutivoCompania "
   vSQL = vSQL & " AND pago.CodigoProveedor = proveedor.CodigoProveedor "
   vSQL = vSQL & " WHERE (pago.StatusOrdenDePago = " & gUtilSQL.fSimpleSqlValue(enum_StatusPago.eSP_VIGENTE) & ")"

   fSqlViewLibroDeRetencionesRenta = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewLibroDeRetencionesRenta", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlViewRetencionesIGV() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = ""
   vSQL = "SELECT "
   fSqlViewRetencionesIGV = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRetencionesIGV", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNamePagosDeclracionMensualXMLDividendoYAcciones() As String
   GetViewNamePagosDeclracionMensualXMLDividendoYAcciones = "IGV_PagosDeclracionMensualXMLDividendoYAcciones"
End Function
'Manejo de Declaracion en xml
Public Function fsqlViewPagosDeclracionMensualXMLDividendoYAcciones() As String
   Dim vSQLLimpiaDoc As String
   Dim vSQLLimpiaControl As String
   Dim SQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQLLimpiaDoc = fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar("numeroDelDocumento", "/", ""), "*", ""), "_", ""), "-", ""), ".", ""), ",", "")
   vSQLLimpiaControl = fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar("NumeroControl", "/", ""), "*", ""), "_", ""), "-", ""), ".", ""), ",", "")
   SQL = " SELECT "
   SQL = SQL & vSQLLimpiaDoc & " AS NumeroCxpOriginal,  "
   SQL = SQL & " retPago.ConsecutivoCompania, "
   SQL = SQL & " proveedor.NumeroRIF AS RifRetenido, "
   SQL = SQL & " retPago.Fecha, "
   SQL = SQL & " retPago.MesAplicacion, "
   SQL = SQL & " retPago.AnoAplicacion, "
   SQL = SQL & " RetPago.NumeroReferencia AS NumeroReferencia, "
   SQL = SQL & gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25" & " - (LEN(" & vSQLLimpiaDoc) & ")) + (" & vSQLLimpiaDoc & ") AS NumeroFactura, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("NumeroControl"), _
               gUtilSQL.fSimpleSqlValue("NA"), gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroControl") _
               & "= 0", gUtilSQL.fSimpleSqlValue("NA"), _
               gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25 - LEN (" & vSQLLimpiaControl) & ") + (" & vSQLLimpiaControl & ")", True), True) & " AS NumeroControl, "
   SQL = SQL & " retDocumentoPagado.CodigoRetencion AS CodigoRetencion, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("retDocumentoPagado.TipoOperacion", enum_TipoDeOperacionSeniat.eTO_NO_APLICA, gEnumProyecto.fEnumTipoDeOperacionSeniatToCodigoSeniatInArray(True), "") & " AS Tipo , "
   SQL = SQL & "retDocumentoPagado.MontoBaseImponible AS MontoOperacion, "
   SQL = SQL & "retDocumentoPagado.PorcentajeDeRetencion AS PorcentajeRetencion, "
   SQL = SQL & " retDocumentoPagado.MontoRetencion AS MontoRetencion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 11", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroDelDocumento")) & " AS MarcaNoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 9", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroControl")) & " AS MarcaNoControl "
   
   SQL = SQL & "FROM ((((" & "retPago INNER JOIN "
   SQL = SQL & "retDocumentoPagado ON "
   SQL = SQL & "retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania"
   SQL = SQL & " AND " & "retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante"
   SQL = SQL & ") INNER JOIN " & "Comun.TablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = Comun.TablaRetencion.CODIGO "
   SQL = SQL & ") INNER JOIN " & "proveedor ON "
   SQL = SQL & "retPago.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   SQL = SQL & " AND retPago.CodigoProveedor = proveedor.CodigoProveedor "
   SQL = SQL & "AND Comun.TablaRetencion.TIPODEPERSONA = proveedor.TipoDePersona"
   SQL = SQL & ") "
   'INNER JOIN " & "plantillaRET ON "
   'SQL = SQL & "Comun.TablaRetencion.SECUENCIALDEPLANTILLA = plantillaRET.Codigo "
   SQL = SQL & "INNER JOIN Pago "
   SQL = SQL & "ON Proveedor.ConsecutivoCompania = Pago.ConsecutivoCompania "
   SQL = SQL & "AND Proveedor.CodigoProveedor = Pago.CodigoProveedor "
   SQL = SQL & "AND RetPago.NumeroComprobante = Pago.NumeroComprobante "
   SQL = SQL & "AND RetPago.NumeroReferencia = Pago.NumeroCheque) "
   
   SQL = SQL & " WHERE "
   SQL = SQL & "(retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("ACCION")
   SQL = SQL & " OR retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("BOLSA") & ")"
   SQL = SQL & " AND (retDocumentoPagado.MontoBaseImponible > 0)"
   SQL = SQL & " AND (Pago.StatusOrdenDePago <> 1)"
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   'SE HACE UNION PARA TRAER LAS RETENCIONES CORRESPONDIENTES POR CUENTAS DE TERCEROS
   SQL = SQL & " UNION "
   SQL = SQL & " SELECT "
   SQL = SQL & vSQLLimpiaDoc & " AS NumeroCxpOriginal,  "
   SQL = SQL & " retPago.ConsecutivoCompania, "
   SQL = SQL & " proveedor.NumeroRIF AS RifRetenido, "
   SQL = SQL & " retPago.Fecha, "
   SQL = SQL & " retPago.MesAplicacion, "
   SQL = SQL & " retPago.AnoAplicacion, "
   SQL = SQL & " RetPago.NumeroReferencia AS NumeroReferencia, "
   SQL = SQL & gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25" & " - (LEN(" & vSQLLimpiaDoc) & ")) + (" & vSQLLimpiaDoc & ") AS NumeroFactura, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("NumeroControl"), _
               gUtilSQL.fSimpleSqlValue("NA"), gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroControl") _
               & "= 0", gUtilSQL.fSimpleSqlValue("NA"), _
               gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25 - LEN (" & vSQLLimpiaControl) & ") + (" & vSQLLimpiaControl & ")", True), True) & " AS NumeroControl, "
   SQL = SQL & " retDocumentoPagado.CodigoRetencion AS CodigoRetencion, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("retDocumentoPagado.TipoOperacion", enum_TipoDeOperacionSeniat.eTO_NO_APLICA, gEnumProyecto.fEnumTipoDeOperacionSeniatToCodigoSeniatInArray(True), "") & " AS Tipo , "
   SQL = SQL & "retDocumentoPagado.MontoBaseImponible AS MontoOperacion, "
   SQL = SQL & "retDocumentoPagado.PorcentajeDeRetencion AS PorcentajeRetencion, "
   SQL = SQL & " retDocumentoPagado.MontoRetencion AS MontoRetencion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 11", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroDelDocumento")) & " AS MarcaNoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 9", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroControl")) & " AS MarcaNoControl "
   SQL = SQL & "FROM ((((" & "retPago INNER JOIN "
   SQL = SQL & "retDocumentoPagado ON "
   SQL = SQL & "retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania"
   SQL = SQL & " AND " & "retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante"
   SQL = SQL & ") INNER JOIN " & "Comun.TablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = Comun.TablaRetencion.CODIGO "
   SQL = SQL & ") INNER JOIN " & "proveedor ON "
   SQL = SQL & "retPago.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   SQL = SQL & " AND retDocumentoPagado.CodigoProveedorOriginalServicio = proveedor.CodigoProveedor "
   SQL = SQL & "AND Comun.TablaRetencion.TIPODEPERSONA = proveedor.TipoDePersona"
   SQL = SQL & ") "
   'INNER JOIN " & "plantillaRET ON "
   'SQL = SQL & "Comun.TablaRetencion.SECUENCIALDEPLANTILLA = plantillaRET.Codigo "
   SQL = SQL & " INNER JOIN Pago "
   SQL = SQL & " ON Proveedor.ConsecutivoCompania = Pago.ConsecutivoCompania "
   SQL = SQL & " AND Proveedor.CodigoProveedor = retDocumentoPagado.CodigoProveedorOriginalServicio "
   SQL = SQL & " AND RetPago.NumeroComprobante = Pago.NumeroComprobante "
   SQL = SQL & " AND RetPago.NumeroReferencia = Pago.NumeroCheque) "
   SQL = SQL & " WHERE "
   SQL = SQL & "(retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("ACCION")
   SQL = SQL & " OR retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("BOLSA") & ")"
   SQL = SQL & " AND (retDocumentoPagado.MontoBaseImponible > 0)"
   SQL = SQL & " AND (Pago.StatusOrdenDePago <> 1) "
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   
'   SQL = SQL & " AND (RetDocumentoPagado.MontoRetencion <> 0)"
   fsqlViewPagosDeclracionMensualXMLDividendoYAcciones = SQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fsqlViewPagosDeclracionMensualXMLDividendoYAcciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNamePagosDeclracionMensualXMLSalariosOtros() As String
   GetViewNamePagosDeclracionMensualXMLSalariosOtros = "IGV_PagosDeclracionMensualXMLSalariosOtros"
End Function

Public Function fsqlViewPagosDeclracionMensualXMLSalariosOtros() As String
   Dim SQL As String
   Dim vSQLLimpiaDoc As String
   Dim vSQLLimpiaControl As String
   On Error GoTo h_ERROR
   vSQLLimpiaDoc = fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar("numeroDelDocumento", "/", ""), "*", ""), "_", ""), "-", ""), ".", ""), ",", "")
   vSQLLimpiaControl = fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar(fReplaceChar("NumeroControl", "/", ""), "*", ""), "_", ""), "-", ""), ".", ""), ",", "")
   SQL = " SELECT "
   SQL = SQL & vSQLLimpiaDoc & " AS NumeroCxpOriginal,  "
   SQL = SQL & " retPago.ConsecutivoCompania, "
   SQL = SQL & " proveedor.NumeroRIF AS RifRetenido, "
   SQL = SQL & " retPago.Fecha, "
   SQL = SQL & " retPago.MesAplicacion, "
   SQL = SQL & " retPago.AnoAplicacion, "
   SQL = SQL & " RetPago.NumeroReferencia AS NumeroReferencia, "
   SQL = SQL & gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25" & " - (LEN(" & vSQLLimpiaDoc) & ")) + (" & vSQLLimpiaDoc & ") AS NumeroFactura, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("NumeroControl"), _
               gUtilSQL.fSimpleSqlValue("NA"), gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroControl") _
               & "= 0", gUtilSQL.fSimpleSqlValue("NA"), _
               gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25" & " - LEN (" & vSQLLimpiaControl) & ") + (" & vSQLLimpiaControl & ")", True), True) & " AS NumeroControl, "
   SQL = SQL & " retDocumentoPagado.CodigoRetencion AS CodigoRetencion, "
   SQL = SQL & " Comun.TablaRetencion.CodigoSeniat AS CodigoConcepto, "
   SQL = SQL & " retDocumentoPagado.MontoBaseImponible AS MontoOperacion, "
   SQL = SQL & gUtilSQL.getIIF("CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("SUELDO"), gUtilSQL.getIIF("(retDocumentoPagado.PorcentajeDeRetencion = 0 AND MontoRetencion > 0)", "(MontoRetencion * 100)/MontoBaseImponible", "retDocumentoPagado.PorcentajeDeRetencion"), "retDocumentoPagado.PorcentajeDeRetencion") & " AS PorcentajeRetencion, "
   SQL = SQL & " retDocumentoPagado.MontoRetencion AS MontoRetencion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 11", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroDelDocumento")) & " AS MarcaNoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 9", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroControl")) & " AS MarcaNoControl "
   SQL = SQL & ", Comun.TablaRetencion.FechaDeInicioDeVigencia "
   '***
   SQL = SQL & "FROM ((((" & "retPago INNER JOIN "
   SQL = SQL & "retDocumentoPagado ON "
   SQL = SQL & "retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania"
   SQL = SQL & " AND " & "retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante"
   SQL = SQL & ") INNER JOIN " & "Comun.TablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = Comun.TablaRetencion.CODIGO "
   SQL = SQL & ") INNER JOIN " & "proveedor ON "
   SQL = SQL & "retPago.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   SQL = SQL & " AND retPago.CodigoProveedor = proveedor.CodigoProveedor "
   SQL = SQL & "AND Comun.TablaRetencion.TIPODEPERSONA = proveedor.TipoDePersona"
   SQL = SQL & ") "
   'sql = sql & " INNER JOIN " & "plantillaRET ON "
   'SQL = SQL & "TablaRetencion.SECUENCIALDEPLANTILLA = plantillaRET.Codigo "
   SQL = SQL & "INNER JOIN Pago "
   SQL = SQL & "ON Proveedor.ConsecutivoCompania = Pago.ConsecutivoCompania "
   SQL = SQL & "AND Proveedor.CodigoProveedor = Pago.CodigoProveedor "
   SQL = SQL & "AND RetPago.NumeroComprobante = Pago.NumeroComprobante "
   SQL = SQL & "AND RetPago.NumeroReferencia = Pago.NumeroCheque) "
   '***
   SQL = SQL & " WHERE "
   SQL = SQL & "(retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("ACCION")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("BOLSA")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("APUEST")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("GANANC")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("NORET") & ")"
   SQL = SQL & " AND (Pago.StatusOrdenDePago <> 1)"
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(False)
   ''Hago un union para buscar las retenciones que son por Cuentas de Terceros
   SQL = SQL & " UNION"
   SQL = SQL & " SELECT "
   SQL = SQL & vSQLLimpiaDoc & " AS NumeroCxpOriginal,  "
   SQL = SQL & " retPago.ConsecutivoCompania, "
   SQL = SQL & " proveedor.NumeroRIF AS RifRetenido, "
   SQL = SQL & " retPago.Fecha, "
   SQL = SQL & " retPago.MesAplicacion, "
   SQL = SQL & " retPago.AnoAplicacion, "
   SQL = SQL & " RetPago.NumeroReferencia AS NumeroReferencia, "
   SQL = SQL & gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25" & " - (LEN(" & vSQLLimpiaDoc) & ")) + (" & vSQLLimpiaDoc & ") AS NumeroFactura, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("NumeroControl"), _
               gUtilSQL.fSimpleSqlValue("NA"), gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroControl") _
               & "= 0", gUtilSQL.fSimpleSqlValue("NA"), _
               gUtilSQL.fMid(gUtilSQL.fSimpleSqlValue(""), "0", "25" & " - LEN (" & vSQLLimpiaControl) & ") + (" & vSQLLimpiaControl & ")", True), True) & " AS NumeroControl, "
   SQL = SQL & " retDocumentoPagado.CodigoRetencion AS CodigoRetencion, "
   SQL = SQL & " Comun.TablaRetencion.CodigoSeniat AS CodigoConcepto, "
   SQL = SQL & " retDocumentoPagado.MontoBaseImponible AS MontoOperacion, "
   SQL = SQL & gUtilSQL.getIIF("CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("SUELDO"), gUtilSQL.getIIF("(retDocumentoPagado.PorcentajeDeRetencion = 0 AND MontoRetencion > 0)", "(MontoRetencion * 100)/MontoBaseImponible", "retDocumentoPagado.PorcentajeDeRetencion"), "retDocumentoPagado.PorcentajeDeRetencion") & " AS PorcentajeRetencion, "
   SQL = SQL & " retDocumentoPagado.MontoRetencion AS MontoRetencion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 11", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroDelDocumento")) & " AS MarcaNoDocumento, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen("retDocumentoPagado", "NumeroDelDocumento") & " >= 9", gUtilSQL.fSimpleSqlValue("S"), fSQLMarcarCaracteresInvalidos("retDocumentoPagado.NumeroControl")) & " AS MarcaNoControl "
   SQL = SQL & ", Comun.TablaRetencion.FechaDeInicioDeVigencia "
   SQL = SQL & "FROM ((((" & "retPago INNER JOIN "
   SQL = SQL & "retDocumentoPagado ON "
   SQL = SQL & "retPago.ConsecutivoCompania = retDocumentoPagado.ConsecutivoCompania"
   SQL = SQL & " AND " & "retPago.NumeroComprobante = retDocumentoPagado.NumeroComprobante"
   SQL = SQL & ") INNER JOIN " & "Comun.TablaRetencion ON "
   SQL = SQL & "retDocumentoPagado.CodigoRetencion = Comun.TablaRetencion.CODIGO "
   SQL = SQL & ") INNER JOIN " & "proveedor ON "
   SQL = SQL & "retPago.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   SQL = SQL & " AND RetDocumentoPagado.CodigoProveedorOriginalServicio = proveedor.CodigoProveedor "
   SQL = SQL & "AND Comun.TablaRetencion.TIPODEPERSONA = proveedor.TipoDePersona"
   SQL = SQL & ") "
   SQL = SQL & "INNER JOIN Pago "
   SQL = SQL & "ON Proveedor.ConsecutivoCompania = Pago.ConsecutivoCompania "
   SQL = SQL & "AND Proveedor.CodigoProveedor = RetDocumentoPagado.CodigoProveedorOriginalServicio "
   SQL = SQL & "AND RetPago.NumeroComprobante = Pago.NumeroComprobante "
   SQL = SQL & "AND RetPago.NumeroReferencia = Pago.NumeroCheque) "
   SQL = SQL & " WHERE "
   SQL = SQL & "(retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("ACCION")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("BOLSA")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("APUEST")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("GANANC")
   SQL = SQL & " AND retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("NORET") & ")"
   SQL = SQL & " AND (Pago.StatusOrdenDePago <> 1)"
   SQL = SQL & " AND retDocumentoPagado.EsUnaCuentaATerceros = " & gUtilSQL.fBooleanToSqlValue(True)
   fsqlViewPagosDeclracionMensualXMLSalariosOtros = SQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fsqlViewPagosDeclracionMensualXMLSalariosOtros", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNamePagosDeclracionMensualXMLGananciasFortuitas() As String
   GetViewNamePagosDeclracionMensualXMLGananciasFortuitas = "IGV_PagosDeclracionMensualXMLGananciasFortuitas"
End Function

Public Function fsqlViewGananciasFortuitas() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   vSQL = "SELECT retDocumentoPagado.ConsecutivoCompania,"
   vSQL = vSQL & " retPago.Fecha, "
   vSQL = vSQL & " retPago.MesAplicacion, "
   vSQL = vSQL & " retPago.AnoAplicacion, "
   vSQL = vSQL & gUtilSQL.getIIF("retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("APUEST"), _
               "retDocumentoPagado.MontoOriginal", gUtilSQL.fNumToStrSQL("0"), True) & " AS PagadoGanancias16, "
   vSQL = vSQL & gUtilSQL.getIIF("retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("APUEST"), _
               "retDocumentoPagado.MontoRetencion", gUtilSQL.fNumToStrSQL("0"), True) & " AS retGanancias16, "
   vSQL = vSQL & gUtilSQL.getIIF("retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("GANANC"), _
               "retDocumentoPagado.MontoOriginal", gUtilSQL.fNumToStrSQL("0"), True) & " AS PagadoGanancias34, "
   vSQL = vSQL & gUtilSQL.getIIF("retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("GANANC"), _
               "retDocumentoPagado.MontoRetencion", gUtilSQL.fNumToStrSQL("0"), True) & " AS retGanancias34 "
   vSQL = vSQL & " FROM retDocumentoPagado "
   vSQL = vSQL & " INNER JOIN RetPago "
   vSQL = vSQL & " ON retDocumentoPagado.NumeroComprobante = RetPago.NumeroComprobante "
   vSQL = vSQL & " AND retDocumentoPagado.ConsecutivoCompania = RetPago.ConsecutivoCompania "
   vSQL = vSQL & " WHERE  (retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("GANANC")
   vSQL = vSQL & " OR retDocumentoPagado.CodigoRetencion = " & gUtilSQL.fSimpleSqlValue("APUEST") & ")"
   vSQL = vSQL & " AND (retDocumentoPagado.MontoBaseImponible > 0)"
   vSQL = vSQL & " AND (RetDocumentoPagado.MontoRetencion <> 0)"
   fsqlViewGananciasFortuitas = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewGananciasFortuitas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fReplaceChar(ByVal valValue As String, ByVal valCharToReplace As String, ByVal valAddChar As String) As String
   Dim vReplaceChar As String
   On Error GoTo h_ERROR
   vReplaceChar = "REPLACE(" & valValue & "," & _
                  gUtilSQL.fSimpleSqlValue(valCharToReplace) & "," & _
                  gUtilSQL.fSimpleSqlValue(valAddChar) & ")"
   fReplaceChar = vReplaceChar
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fReplaceChar", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLMarcarCaracteresInvalidos(ByVal vCampoDeBusqueda As String) As String
   Dim vSQL As String
   Dim vCadenaBusqueda As String
   On Error GoTo h_ERROR
   vCadenaBusqueda = gUtilSQL.getIIF("(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, "(") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, ")") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, "+") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, ";") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, "|") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, "/") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, "*") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, "-") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, "_") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, ".") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, " ") & " > 0) OR" _
   & "(" & gUtilSQL.DfInStrSQL(vCampoDeBusqueda, ",") & " > 0)" _
   , gUtilSQL.fSimpleSqlValue("S"), gUtilSQL.fSimpleSqlValue("N"), True)
   fSQLMarcarCaracteresInvalidos = vCadenaBusqueda
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLMarcarCaracteresInvalidos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameMercantilCabecera() As String
   GetViewNameMercantilCabecera = "IGV_MercantilCabecera"
End Function

Public Function fsqlViewMercantilCabecera() As String
   Dim vSQL As String
   Dim vSeparador As String
   Dim vConjuntoBusqueda As Variant
   Dim vConjuntoReemplazo As Variant
   Dim insClsLibSaw As clsLibSawVista
   On Error GoTo h_ERROR
   Set insClsLibSaw = New clsLibSawVista
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vConjuntoBusqueda = gTexto.fConvertCadenaEnArray("." & vSeparador, vSeparador)
   vConjuntoReemplazo = gTexto.fConvertCadenaEnArray("" & vSeparador, vSeparador)

   vSQL = "SELECT compania.ConsecutivoCompania, "
   vSQL = vSQL & "'BAMRVECA    ' AS IdBanco, "
   vSQL = vSQL & gUtilSQL.fCast(gUtilDate.fYear(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fMonth(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, ""), 2, 0) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fDay(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, ""), 2, 0) & gUtilSQL.CharConcat & gUtilSQL.fSQLFillRightWithCaracter("compania.codigo", 5, 0) & " AS NumeroArchivo, "
   vSQL = vSQL & "'PROVE' AS TipoProducto, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0000000062") & " AS TipoPago, "
   vSQL = vSQL & gUtilSQL.DfMidSQL("compania.NumeroDeRif", 1, 1) & " as TipoId, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.DfMidSQL("compania.NumeroDeRif", 2, 15), 15, 0) & " as Rif, "
   vSQL = vSQL & gUtilSQL.fCOUNT("pago.CodigoCuentaBancaria", "") & " as TotalRegistro, "
   vSQL = vSQL & "Pago.MontoCheque As TotalDocumentos, "
   vSQL = vSQL & gUtilSQL.fCast(gUtilDate.fYear(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.CharConcat & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fMonth(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, ""), 2, 0) & gUtilSQL.CharConcat & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilDate.fDay(gUtilDate.getFechaDeHoy), eTDSS_VARCHAR, ""), 2, 0) & " as FechaGestion, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 7, 0) & " As AreaReservada1, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 8, 0) & " As NumeroSerial, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 4, 0) & " As CodigoRespuesta, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 8, 0) & " As FechaProceso, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 261, 0) & " As AreaReservada2, "
   vSQL = vSQL & "pago.CodigoCuentaBancaria, "
   vSQL = vSQL & "pago.fecha, "
   vSQL = vSQL & "CuentaBancaria.NumeroCuenta, "
   vSQL = vSQL & "pago.NumeroCheque "
   vSQL = vSQL & "FROM compania INNER JOIN pago "
   vSQL = vSQL & "ON (compania.ConsecutivoCompania = pago.ConsecutivoCompania) "
   vSQL = vSQL & "INNER JOIN CuentaBancaria "
   vSQL = vSQL & "ON (compania.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND pago.CodigoCuentaBancaria = CuentaBancaria.Codigo) "
   vSQL = vSQL & "INNER JOIN Banco "
   vSQL = vSQL & "ON (CuentaBancaria.CodigoBanco = Banco.Codigo) "
   vSQL = vSQL & "WHERE pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSQL = vSQL & " AND pago.MontoCheque > 0 "
   vSQL = vSQL & "GROUP BY compania.ConsecutivoCompania, "
   vSQL = vSQL & "compania.codigo, "
   vSQL = vSQL & "compania.NumeroDeRif, "
   vSQL = vSQL & "banco.Codigo, "
   vSQL = vSQL & "pago.CodigoCuentaBancaria, "
   vSQL = vSQL & "Pago.MontoCheque, "
   vSQL = vSQL & "pago.fecha, "
   vSQL = vSQL & "CuentaBancaria.NumeroCuenta, "
   vSQL = vSQL & "pago.NumeroCheque "

   fsqlViewMercantilCabecera = vSQL
   Set insClsLibSaw = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewMercantilCabecera", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function



Public Function GetViewNameMercantilPago() As String
   GetViewNameMercantilPago = "IGV_MercantilPago"
End Function

Public Function fsqlViewMercantilPago() As String
   Dim vSQL As String
   Dim vSeparador As String
   Dim vConjuntoBusqueda As Variant
   Dim vConjuntoReemplazo As Variant
   Dim vConjuntoBusquedaMonto As Variant
   Dim vConjuntoReemplazoMonto As Variant
   Dim insClsLibSaw As clsLibSawVista
   On Error GoTo h_ERROR
   Set insClsLibSaw = New clsLibSawVista
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vConjuntoBusqueda = gTexto.fConvertCadenaEnArray("á" & vSeparador & "é" & vSeparador & "í" & vSeparador & "ó" & vSeparador & "ú" & vSeparador & "ñ" & vSeparador & "à" & vSeparador & "è" & vSeparador & "ì" & vSeparador & "ò" & vSeparador & "ù" & vSeparador, vSeparador)
   vConjuntoReemplazo = gTexto.fConvertCadenaEnArray("a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "n" & vSeparador & "a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador, vSeparador)
   
   vConjuntoBusquedaMonto = gTexto.fConvertCadenaEnArray("." & vSeparador, vSeparador)
   vConjuntoReemplazoMonto = gTexto.fConvertCadenaEnArray("" & vSeparador, vSeparador)
   
   vSQL = "SELECT "
   vSQL = vSQL & "pago.ConsecutivoCompania, "
   vSQL = vSQL & gUtilSQL.DfMidSQL("proveedor.NumeroRif", 1, 1) & " as TipoId, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.DfMidSQL("proveedor.NumeroRif", 2, 15), 15, 0) & " as Rif, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 12, 0) & " As AreaReservada1, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres(gUtilSQL.fSimpleSqlValue(""), 15) & " as AreaReservada2,"
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 15, 0) & " As AreaReservada3, "
   vSQL = vSQL & "Proveedor.NumeroCuentaBancaria As NumeroCuentaProv, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.DfSQLReplace(gUtilSQL.fCast("pago.MontoCheque", eTDSS_VARCHAR, ""), ".", ""), 17, 0) & " as TotalDocumentos, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres("proveedor.NumeroRif", 16) & " AS IdCliente, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0000000062") & " As TipoPago, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 3, 0) & " As AreaReservada4, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres(gUtilSQL.DfUcaseSQL(insClsLibSaw.fSqlReplaceSet("proveedor.NombreProveedor", vConjuntoBusqueda, vConjuntoReemplazo)), 60) & " as Proveedor, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 7, 0) & " As AreaReservada5, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast("Pago.NumeroComprobante", eTDSS_VARCHAR, ""), 8, 0) & " AS NumeroComprobante, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres("Proveedor.email", 50) & " AS email, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 4, 0) & " as CodRespuesta, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres("", 30) & " AS MsjRespuesta, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres("", 80) & " AS ConceptoPago, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 35, 0) & " as AreaReservada6, "
   vSQL = vSQL & "pago.fecha as FechaPago, "
   vSQL = vSQL & "pago.CodigoCuentaBancaria AS CuentaBancariaPago, "
   vSQL = vSQL & "pago.NumeroCheque "
   vSQL = vSQL & " FROM pago "
   vSQL = vSQL & " INNER JOIN CuentaBancaria AS CuentaBancaria "
   vSQL = vSQL & " ON (CuentaBancaria.ConsecutivoCompania = Pago.ConsecutivoCompania "
   vSQL = vSQL & " AND Pago.CodigoCuentaBancaria = CuentaBancaria.Codigo) "
   vSQL = vSQL & " INNER JOIN Proveedor "
   vSQL = vSQL & " ON (pago.CodigoProveedor = proveedor.CodigoProveedor "
   vSQL = vSQL & " AND pago.ConsecutivoCompania = proveedor.ConsecutivoCompania) "
   vSQL = vSQL & " WHERE pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSQL = vSQL & " AND pago.MontoCheque > 0 "
   vSQL = vSQL & " GROUP BY  pago.ConsecutivoCompania, proveedor.NumeroRif, pago.MontoCheque, proveedor.NombreProveedor, "
   vSQL = vSQL & " Pago.NumeroComprobante, Proveedor.email, Proveedor.NumeroCuentaBancaria, pago.fecha, pago.CodigoCuentaBancaria, "
   vSQL = vSQL & " pago.NumeroCheque "
   Set insClsLibSaw = Nothing
   fsqlViewMercantilPago = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewMercantilPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function GetViewNameMercantilOperaciones() As String
   GetViewNameMercantilOperaciones = "IGV_MercantilOperaciones"
End Function

Public Function fsqlViewMercantilOperaciones() As String
   Dim vSQL As String
   Dim vSeparador As String
   Dim vConjuntoBusqueda As Variant
   Dim vConjuntoReemplazo As Variant
   Dim vConjuntoBusqueda2 As Variant
   Dim vConjuntoReemplazo2 As Variant
   Dim insClsLibSaw As clsLibSawVista
   On Error GoTo h_ERROR
   Set insClsLibSaw = New clsLibSawVista
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vConjuntoBusqueda = gTexto.fConvertCadenaEnArray("á" & vSeparador & "é" & vSeparador & "í" & vSeparador & "ó" & vSeparador & "ú" & vSeparador & "ñ" & vSeparador & "à" & vSeparador & "è" & vSeparador & "ì" & vSeparador & "ò" & vSeparador & "ù" & vSeparador & "?" & vSeparador & "!" & vSeparador, vSeparador)
   vConjuntoReemplazo = gTexto.fConvertCadenaEnArray("a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "n" & vSeparador & "a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "" & vSeparador & "" & vSeparador, vSeparador)
   vConjuntoBusqueda2 = gTexto.fConvertCadenaEnArray("á" & vSeparador & "é" & vSeparador & "í" & vSeparador & "ó" & vSeparador & "ú" & vSeparador & "ñ" & vSeparador & "à" & vSeparador & "è" & vSeparador & "ì" & vSeparador & "ò" & vSeparador & "ù" & vSeparador & "?" & vSeparador & "!" & vSeparador & "." & vSeparador, vSeparador)
   vConjuntoReemplazo2 = gTexto.fConvertCadenaEnArray("a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "n" & vSeparador & "a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "" & vSeparador & "" & vSeparador & "" & vSeparador, vSeparador)
   
   vSQL = "SELECT "
   vSQL = vSQL & "pago.ConsecutivoCompania, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres(insClsLibSaw.fSqlReplaceSet("pago.NumeroCheque", vConjuntoBusqueda2, vConjuntoReemplazo2), 12) & " AS NumeroCheque, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("OP") & " as TipoDocumento, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.DfSQLReplace(gUtilSQL.fCast("pago.MontoCheque", eTDSS_VARCHAR, ""), ".", ""), 17, 0) & " as TotalDocumento, "
   vSQL = vSQL & gUtilSQL.fCast(gUtilSQL.DfSQLYearOfDate("Pago.Fecha"), eTDSS_VARCHAR, "") & gUtilSQL.CharConcat & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLMonthOfDate("Pago.Fecha"), eTDSS_VARCHAR, ""), 2, 0) & gUtilSQL.CharConcat & gUtilSQL.DfRightSQL(gUtilSQL.fCast(gUtilSQL.DfSQLDayOfDate("Pago.Fecha"), eTDSS_VARCHAR, ""), 2, 0) & " As Fecha, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 7, 0) & " as AreaReservada1, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast("Pago.NumeroComprobante", eTDSS_VARCHAR, ""), 8, 0) & " As NumeroComprobante, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres("", 12) & " AS NumeroOrden, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres(gUtilSQL.DfUcaseSQL(insClsLibSaw.fSqlReplaceSet("Pago.DescripcionPago", vConjuntoBusqueda, vConjuntoReemplazo)), 80) & " as DescripcionPago, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fSimpleSqlValue(""), 236, 0) & " As AreaReservada2, "
   vSQL = vSQL & "pago.fecha as FechaPago, "
   vSQL = vSQL & "pago.CodigoCuentaBancaria AS CuentaBancariaPago "
   vSQL = vSQL & " FROM pago INNER JOIN PROVEEDOR "
   vSQL = vSQL & " ON (pago.CodigoProveedor = proveedor.CodigoProveedor "
   vSQL = vSQL & " AND pago.ConsecutivoCompania = proveedor.ConsecutivoCompania) "
   vSQL = vSQL & " INNER JOIN CuentaBancaria "
   vSQL = vSQL & " ON (pago.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & " AND pago.CodigoCuentaBancaria = CuentaBancaria.Codigo) "
   vSQL = vSQL & " INNER JOIN Banco "
   vSQL = vSQL & " ON (CuentaBancaria.CodigoBanco = Banco.Codigo) "
   vSQL = vSQL & " WHERE pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSQL = vSQL & " AND pago.MontoCheque > 0 "
   vSQL = vSQL & " GROUP BY pago.ConsecutivoCompania, pago.NumeroCheque, Pago.MontoCheque, Pago.Fecha, Pago.NumeroComprobante, Pago.DescripcionPago, pago.fecha, pago.CodigoCuentaBancaria "
   Set insClsLibSaw = Nothing
   fsqlViewMercantilOperaciones = vSQL

h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewMercantilOperaciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCompraRetencionesPorConceptoISLR() As String
   GetViewNameCompraRetencionesPorConceptoISLR = "IGV_Compra_RetencionesPorConcepto_ISLR"
End Function

Public Function fsqlViewCompraRetencionesPorConceptoISLR() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT "
   vSQL = vSQL & " retPago.ConsecutivoCompania,"
   vSQL = vSQL & " retDocumentoPagado.SecuencialDocPagado,"
   vSQL = vSQL & " retPago.NumeroReferencia,"
   vSQL = vSQL & " retDocumentoPagado.NumeroComprobante,"
   vSQL = vSQL & " retDocumentoPagado.NumeroDelDocumento,"
   vSQL = vSQL & gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull("retDocumentoPagado.NumeroControl"), _
            gUtilSQL.fSimpleSqlValue("NA"), "retDocumentoPagado.NumeroControl", True) & " AS NumeroControl, "
   vSQL = vSQL & " retDocumentoPagado.MontoOriginal,"
   vSQL = vSQL & " retDocumentoPagado.MontoBaseImponible, retDocumentoPagado.MontoRetencion, retDocumentoPagado.PorcentajeDeRetencion,"
   vSQL = vSQL & " retPago.Fecha,"
   vSQL = vSQL & " retDocumentoPagado.CodigoRetencion,"
   vSQL = vSQL & " retPago.MesAplicacion,"
   vSQL = vSQL & " retPago.AnoAplicacion,"
   vSQL = vSQL & " proveedor.NombreProveedor,"
   vSQL = vSQL & " proveedor.CodigoProveedor,"
   vSQL = vSQL & " proveedor.NumeroRIF,"
   vSQL = vSQL & " proveedor.TipoDePersona,"
   vSQL = vSQL & " Comun.TablaRetencion.FechaDeInicioDeVigencia,"
   vSQL = vSQL & " Comun.TablaRetencion.BaseImponible,"
   vSQL = vSQL & " Comun.TablaRetencion.Sustraendo,"
   vSQL = vSQL & " Comun.TablaRetencion.TipoDePago,"
   vSQL = vSQL & " Comun.TablaRetencion.Tarifa,"
   vSQL = vSQL & " Comun.TablaRetencion.ParaPagosMayoresDe"
   vSQL = vSQL & " FROM retDocumentoPagado  INNER JOIN retPago"
   vSQL = vSQL & " ON retDocumentoPagado.ConsecutivoCompania = retPago.ConsecutivoCompania AND"
   vSQL = vSQL & " retDocumentoPagado.NumeroComprobante = retPago.NumeroComprobante "
   vSQL = vSQL & " INNER JOIN proveedor ON retPago.ConsecutivoCompania = proveedor.ConsecutivoCompania "
   vSQL = vSQL & " AND retPago.CodigoProveedor = proveedor.CodigoProveedor"
   vSQL = vSQL & " INNER JOIN Comun.TablaRetencion ON proveedor.TipoDePersona = Comun.TablaRetencion.TipoDePersona AND"
   vSQL = vSQL & " retDocumentoPagado.CodigoRetencion = Comun.TablaRetencion.Codigo"
   vSQL = vSQL & " WHERE "
   vSQL = vSQL & " retDocumentoPagado.CodigoRetencion <> " & gUtilSQL.fSimpleSqlValue("NORET")
   fsqlViewCompraRetencionesPorConceptoISLR = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewCompraRetencionesPorConceptoISLR", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameBODCabecera() As String
   GetViewNameBODCabecera = "IGV_BODCabecera"
End Function

Public Function fsqlViewBODCabecera() As String

   Dim vSQL As String
   Dim vSeparador As String
   Dim vConjuntoBusqueda As Variant
   Dim vConjuntoReemplazo As Variant
   Dim insClsLibSaw As clsLibSawVista
   On Error GoTo h_ERROR
   Set insClsLibSaw = New clsLibSawVista
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vConjuntoBusqueda = gTexto.fConvertCadenaEnArray("." & vSeparador, vSeparador)
   vConjuntoReemplazo = gTexto.fConvertCadenaEnArray("" & vSeparador, vSeparador)

   vSQL = "SELECT compania.ConsecutivoCompania, "
   vSQL = vSQL & gUtilSQL.DfMidSQL("compania.NumeroDeRif", 1, 1) & " as TipoId, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.DfMidSQL("compania.NumeroDeRif", 2, 9), 9, 0) & " as Rif, "
   vSQL = vSQL & gUtilSQL.fCOUNT("pago.CodigoCuentaBancaria", "") & " as TotalRegistro, "
   vSQL = vSQL & "Pago.MontoCheque As TotalDocumentos, "
   vSQL = vSQL & "pago.CodigoCuentaBancaria, "
   vSQL = vSQL & "pago.fecha, "
   vSQL = vSQL & "CuentaBancaria.NumeroCuenta, "
   vSQL = vSQL & "pago.NumeroCheque, "
   vSQL = vSQL & "dbo.Pago.NumeroLote "

   vSQL = vSQL & "FROM compania INNER JOIN pago "
   vSQL = vSQL & "ON (compania.ConsecutivoCompania = pago.ConsecutivoCompania) "
   vSQL = vSQL & "INNER JOIN CuentaBancaria "
   vSQL = vSQL & "ON (compania.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & "AND pago.CodigoCuentaBancaria = CuentaBancaria.Codigo) "
   vSQL = vSQL & "INNER JOIN Banco "
   vSQL = vSQL & "ON (CuentaBancaria.CodigoBanco = Banco.Codigo) "

   vSQL = vSQL & "WHERE pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSQL = vSQL & " AND pago.MontoCheque > 0 "

   vSQL = vSQL & "GROUP BY compania.ConsecutivoCompania, "
   vSQL = vSQL & "compania.codigo, "
   vSQL = vSQL & "compania.NumeroDeRif, "
   vSQL = vSQL & "banco.Codigo, "
   vSQL = vSQL & "pago.CodigoCuentaBancaria, "
   vSQL = vSQL & "Pago.MontoCheque, "
   vSQL = vSQL & "pago.fecha, "
   vSQL = vSQL & "CuentaBancaria.NumeroCuenta, "
   vSQL = vSQL & "pago.NumeroCheque, "
   vSQL = vSQL & "pago.NumeroLote"

   fsqlViewBODCabecera = vSQL
   Set insClsLibSaw = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewBODCabecera", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameBODPago() As String
   GetViewNameBODPago = "IGV_BODPago"
End Function

Public Function fsqlViewBODPago() As String
   
   Dim vSQL As String
   Dim vSeparador As String
   Dim vConjuntoBusqueda As Variant
   Dim vConjuntoReemplazo As Variant
   Dim vConjuntoBusquedaTelefono As Variant
   Dim vConjuntoReemplazoTelefono As Variant
   Dim vSqlSinDescripcion As String
   Dim vSqlConDescripcion As String
   Dim insClsLibSaw As clsLibSawVista
   On Error GoTo h_ERROR
   Set insClsLibSaw = New clsLibSawVista
   
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vConjuntoBusqueda = gTexto.fConvertCadenaEnArray("á" & vSeparador & "é" & vSeparador & "í" & vSeparador & "ó" & vSeparador & "ú" & vSeparador & "ñ" & vSeparador & "à" & vSeparador & "è" & vSeparador & "ì" & vSeparador & "ò" & vSeparador & "ù" & vSeparador, vSeparador)
   vConjuntoReemplazo = gTexto.fConvertCadenaEnArray("a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "n" & vSeparador & "a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador, vSeparador)
   
   vConjuntoBusquedaTelefono = gTexto.fConvertCadenaEnArray("(" & vSeparador & ")" & vSeparador & "-" & vSeparador & "." & vSeparador & "" & vSeparador, vSeparador)
   vConjuntoReemplazoTelefono = gTexto.fConvertCadenaEnArray("" & vSeparador & "" & vSeparador & "" & vSeparador & "" & vSeparador & "" & vSeparador & "" & vSeparador, vSeparador)

   vSQL = "SELECT "
   vSQL = vSQL & "Pago.ConsecutivoCompania, "
   vSQL = vSQL & gUtilSQL.DfMidSQL("Proveedor.NumeroRif", 1, 1) & " as TipoId, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.DfMidSQL("Proveedor.NumeroRif", 2, 9), 9, 0) & " as Rif, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres(gUtilSQL.DfUcaseSQL(insClsLibSaw.fSqlReplaceSet("Proveedor.NombreProveedor", vConjuntoBusqueda, vConjuntoReemplazo)), 60) & " AS Proveedor, "
   vSQL = vSQL & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast("Pago.NumeroComprobante", eTDSS_VARCHAR, ""), 9, 0) & " AS NumeroComprobante, "
   vSqlSinDescripcion = gUtilSQL.fCast(gUtilSQL.DfMidSQL("'SIN DESCRIPCION'", 0, 31), eTDSS_VARCHAR, "") & "+ REPLICATE(' ', 30 - " & "LEN(" & (gUtilSQL.DfMidSQL("'SIN DESCRIPCION'", 0, 30)) & "))"
   vSqlConDescripcion = gUtilSQL.fCast(gUtilSQL.DfMidSQL("DescripcionPago", 0, 31), eTDSS_VARCHAR, "") & "+ REPLICATE(' ', 30 - " & "LEN(" & (gUtilSQL.DfMidSQL("DescripcionPago", 0, 30)) & "))"
   vSQL = vSQL & gUtilSQL.getIIF("DescripcionPago = " & gUtilSQL.fSimpleSqlValue(" "), vSqlSinDescripcion, vSqlConDescripcion) & " AS DescripcionPago, "
   vSQL = vSQL & gUtilSQL.getIIF(" LEN(Proveedor.NumeroCuentaBancaria) = 20 ", gUtilSQL.getIIF(gUtilSQL.DfMidSQL("Proveedor.NumeroCuentaBancaria", 0, 5) & " = '0116' ", gUtilSQL.fSimpleSqlValue("CTA"), gUtilSQL.fSimpleSqlValue("BAN")), gUtilSQL.fSimpleSqlValue("CHQ")) & " AS ModalidadPago, "
   vSQL = vSQL & gUtilSQL.getIIF(" LEN (Proveedor.NumeroCuentaBancaria) = 20 ", gUtilSQL.DfMidSQL("Proveedor.NumeroCuentaBancaria", 0, 5), gUtilSQL.fSimpleSqlValue("0000")) & " AS CodigoBanco, "
   vSQL = vSQL & gUtilSQL.getIIF(" LEN (Proveedor.NumeroCuentaBancaria) = 20 ", "Proveedor.NumeroCuentaBancaria", gUtilSQL.fSimpleSqlValue("00000000000000000000")) & " AS CuentaAcreditar, "
   vSQL = vSQL & gUtilSQL.fCast(gUtilSQL.fYear("Pago.Fecha", ""), eTDSS_VARCHAR, "") & "+" & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.fMonth("Pago.Fecha", ""), eTDSS_VARCHAR, ""), 2, 0) & "+" & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.DfSQLDayOfDate("Pago.Fecha", ""), eTDSS_VARCHAR, ""), 2, 0) & " AS FechaValor, "
   vSQL = vSQL & "Pago.Fecha AS FechaPago, "
   vSQL = vSQL & "RIGHT('000000000000000' + REPLACE(CAST(Pago.MontoCheque AS VARCHAR), '.', ''), 15) + 'VEB' AS TotalDocumentos, "
   vSQL = vSQL & "'000000000000000' AS ImpuestoRetenido, "
   vSQL = vSQL & gUtilSQL.DfSQLJustifyLeftNCaracteres("Proveedor.email", 40) & " AS CorreoElectronico, "
   vSQL = vSQL & "CASE WHEN LEN(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Telefonos,"
   vSQL = vSQL & "'(', ''), ')', ''), '-', ''), '/', ''), '.', ''),' ', '')) = 11 THEN REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(Telefonos, '(', ''), ')', ''), '-', ''), '/', ''), '.', ''), ' ', '') "
   vSQL = vSQL & "ELSE '00000000000' END AS Telefonos, "
   vSQL = vSQL & "CuentaBancaria.Codigo AS CuentaBancariaPago, "
   vSQL = vSQL & "Pago.NumeroComprobante AS NumeroDelComprobante "
'   vSQL = vSQL & "DocumentoPagado.NumeroDelDocumentoPagado AS NumeroDelDocumentoPagado "
   
   vSQL = vSQL & " FROM Pago "
   vSQL = vSQL & " INNER JOIN CuentaBancaria AS CuentaBancaria "
   vSQL = vSQL & " ON (CuentaBancaria.ConsecutivoCompania = Pago.ConsecutivoCompania "
   vSQL = vSQL & " AND Pago.CodigoCuentaBancaria = CuentaBancaria.Codigo) "
   vSQL = vSQL & " INNER JOIN Proveedor "
   vSQL = vSQL & " ON (Pago.CodigoProveedor = proveedor.CodigoProveedor "
   vSQL = vSQL & " AND Pago.ConsecutivoCompania = proveedor.ConsecutivoCompania) "
   vSQL = vSQL & " INNER JOIN DocumentoPagado "
   vSQL = vSQL & " ON (Pago.ConsecutivoCompania = dbo.DocumentoPagado.ConsecutivoCompania)"
   vSQL = vSQL & " AND Pago.NumeroComprobante = dbo.DocumentoPagado.NumeroComprobante "

   vSQL = vSQL & " WHERE pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSQL = vSQL & " AND pago.MontoCheque > 0 "

   vSQL = vSQL & " GROUP BY  pago.ConsecutivoCompania, Proveedor.NumeroRif, Proveedor.NombreProveedor,"
   vSQL = vSQL & " Pago.NumeroComprobante, Pago.DescripcionPago, CuentaBancaria.NumeroCuenta, Pago.Fecha,CuentaBancaria.Codigo,"
   vSQL = vSQL & " Pago.MontoCheque, Pago.TotalRetenido, Proveedor.email, Proveedor.Telefonos, Proveedor.NumeroCuentaBancaria,"
   vSQL = vSQL & " DocumentoPagado.NumeroDelDocumentoPagado"
      
   Set insClsLibSaw = Nothing
   fsqlViewBODPago = vSQL
      
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewBODPago", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameBODOperaciones() As String
   GetViewNameBODOperaciones = "IGV_BODOperaciones"
End Function

Public Function fsqlViewBODOperaciones() As String
   Dim vSQL As String
   Dim vSeparador As String
   Dim vConjuntoBusqueda As Variant
   Dim vConjuntoReemplazo As Variant
   Dim vConjuntoBusqueda2 As Variant
   Dim vConjuntoReemplazo2 As Variant
   Dim insClsLibSaw As clsLibSawVista
   On Error GoTo h_ERROR
   Set insClsLibSaw = New clsLibSawVista
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vConjuntoBusqueda = gTexto.fConvertCadenaEnArray("á" & vSeparador & "é" & vSeparador & "í" & vSeparador & "ó" & vSeparador & "ú" & vSeparador & "ñ" & vSeparador & "à" & vSeparador & "è" & vSeparador & "ì" & vSeparador & "ò" & vSeparador & "ù" & vSeparador & "?" & vSeparador & "!" & vSeparador, vSeparador)
   vConjuntoReemplazo = gTexto.fConvertCadenaEnArray("a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "n" & vSeparador & "a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "" & vSeparador & "" & vSeparador, vSeparador)
   vConjuntoBusqueda2 = gTexto.fConvertCadenaEnArray("á" & vSeparador & "é" & vSeparador & "í" & vSeparador & "ó" & vSeparador & "ú" & vSeparador & "ñ" & vSeparador & "à" & vSeparador & "è" & vSeparador & "ì" & vSeparador & "ò" & vSeparador & "ù" & vSeparador & "?" & vSeparador & "!" & vSeparador & "." & vSeparador, vSeparador)
   vConjuntoReemplazo2 = gTexto.fConvertCadenaEnArray("a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "n" & vSeparador & "a" & vSeparador & "e" & vSeparador & "i" & vSeparador & "o" & vSeparador & "u" & vSeparador & "" & vSeparador & "" & vSeparador & "" & vSeparador, vSeparador)
   
   vSQL = "SELECT "
   vSQL = vSQL & "pago.ConsecutivoCompania, "
   vSQL = vSQL & "'                    ' AS FacturaAsociado, "
   vSQL = vSQL & "RIGHT('000000000000000' + REPLACE(CAST(Pago.MontoCheque AS VARCHAR), '.', ''), 15) + 'VEB' AS TotalDocumentos, "
   vSQL = vSQL & "Pago.TotalRetenido, "
   vSQL = vSQL & "'000000000000000' AS ImpuestoRetenido, "
   vSQL = vSQL & "Pago.NumeroComprobante, "
   vSQL = vSQL & "Pago.CodigoCuentaBancaria AS CuentaBancariaPago, "
   vSQL = vSQL & gUtilSQL.fCast(gUtilSQL.fYear("Pago.Fecha", ""), eTDSS_VARCHAR, "") & "+" & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.fMonth("Pago.Fecha", ""), eTDSS_VARCHAR, ""), 2, 0) & "+" & gUtilSQL.fSQLFillRightWithCaracter(gUtilSQL.fCast(gUtilSQL.DfSQLDayOfDate("Pago.Fecha", ""), eTDSS_VARCHAR, ""), 2, 0) & " AS FechaPago "
   
   vSQL = vSQL & " FROM pago INNER JOIN PROVEEDOR "
   vSQL = vSQL & " ON (pago.CodigoProveedor = proveedor.CodigoProveedor "
   vSQL = vSQL & " AND pago.ConsecutivoCompania = proveedor.ConsecutivoCompania) "
   vSQL = vSQL & " INNER JOIN CuentaBancaria "
   vSQL = vSQL & " ON (pago.ConsecutivoCompania = CuentaBancaria.ConsecutivoCompania "
   vSQL = vSQL & " AND pago.CodigoCuentaBancaria = CuentaBancaria.Codigo) "
   vSQL = vSQL & " INNER JOIN Banco "
   vSQL = vSQL & " ON (CuentaBancaria.CodigoBanco = Banco.Codigo) "
   vSQL = vSQL & " INNER JOIN dbo.DocumentoPagado "
   vSQL = vSQL & " ON dbo.Pago.ConsecutivoCompania = dbo.DocumentoPagado.ConsecutivoCompania "
   vSQL = vSQL & " AND dbo.Pago.NumeroComprobante = dbo.DocumentoPagado.NumeroComprobante"
   
   vSQL = vSQL & " WHERE pago.StatusOrdenDePago = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusPago.eSP_VIGENTE)
   vSQL = vSQL & " AND pago.MontoCheque > 0 "
   
   vSQL = vSQL & " GROUP BY Pago.ConsecutivoCompania, Pago.NumeroCheque, Pago.MontoCheque, "
   vSQL = vSQL & " Pago.Fecha , Pago.NumeroComprobante, Pago.DescripcionPago, "
   vSQL = vSQL & " Pago.Fecha , Pago.CodigoCuentaBancaria, Pago.TotalRetenido,Pago.Fecha,"
   vSQL = vSQL & " DocumentoPagado.NumeroDelDocumentoPagado"
   
   Set insClsLibSaw = Nothing
   fsqlViewBODOperaciones = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fsqlViewBODOperaciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

