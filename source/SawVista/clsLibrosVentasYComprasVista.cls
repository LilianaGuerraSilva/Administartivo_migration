VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLibrosVentasYComprasVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsLibrosVentasYComprasVista"
Private Const CM_MESSAGE_NAME As String = "Vista Libros Ventas Y Compras"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Female
End Function

Public Function GetViewNameFacturaConCliente() As String
   GetViewNameFacturaConCliente = "IGV_FactInfoCliente_B1"
End Function

Public Function fSqlViewFacturaConCliente() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumVista As clsSawEnumVista
   Dim insLibVista As clsLibSawVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumVista = New clsSawEnumVista
   Set insLibVista = New clsLibSawVista

   vSQL = "SELECT "
   vSQL = vSQL & "factura.ConsecutivoCompania, "
   vSQL = vSQL & "factura.Numero, "
   vSQL = vSQL & "factura.Fecha, "
   vSQL = vSQL & "factura.FechaDeVencimiento, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                        gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDocumento, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoDeFactura & ".CodPeruTipoDocumentoFactura, "
   vSQL = vSQL & "factura.CodigoCliente, "
   vSQL = vSQL & "cliente.TipoDocumentoIdentificacion, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".CodPeruTipoDocumentoIdentificacion, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".StrTipoDocumentoIdentificacion, "
   vSQL = vSQL & "cliente.NumeroRIF, "
   vSQL = vSQL & "cliente.Nombre, "
   vSQL = vSQL & gUtilSQL.getIIF("factura.NumeroPlanillaExportacion <> " & gUtilSQL.fSimpleSqlValue(""), _
                           "factura.TotalFactura", 0, True) & " AS MontoExportacion, "
   vSQL = vSQL & "factura.TotalBaseImponible, "
   vSQL = vSQL & "factura.TotalMontoExento, "
   vSQL = vSQL & "factura.MontoIVAAlicuota1, "
   vSQL = vSQL & "factura.MontoIVAAlicuota3, "
   vSQL = vSQL & "factura.TotalFactura, "
   vSQL = vSQL & "factura.CambioABolivares, "
   vSQL = vSQL & "factura.NumeroFacturaAfectada "
   vSQL = vSQL & " FROM  factura INNER JOIN "
   vSQL = vSQL & " cliente ON factura.ConsecutivoCompania = cliente.ConsecutivoCompania "
   vSQL = vSQL & " AND factura.CodigoCliente = cliente.Codigo "
   vSQL = vSQL & " INNER JOIN " & gEnumVista.GetViewNameEnumTipoDocumentoDeFactura
   vSQL = vSQL & " ON factura.TipoDeDocumento " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = " & gEnumVista.GetViewNameEnumTipoDocumentoDeFactura & ".EnumValue "
   vSQL = vSQL & " INNER JOIN " & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion
   vSQL = vSQL & " ON cliente.TipoDocumentoIdentificacion " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = " & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".EnumValue "
   vSQL = vSQL & " WHERE (factura.StatusFactura <> " & gUtilSQL.fSimpleSqlValue(enum_StatusFactura.eSF_BORRADOR) & ") "

   Set gEnumProyecto = Nothing
   Set gEnumVista = Nothing
   Set insLibVista = Nothing
   fSqlViewFacturaConCliente = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewFacturaConCliente", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCxcConCliente() As String
   GetViewNameCxcConCliente = "IGV_CxcInfoCliente_B1"
End Function

Public Function fSqlViewCxcConCliente() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumVista As clsSawEnumVista
   Dim insLibVista As clsLibSawVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumVista = New clsSawEnumVista
   Set insLibVista = New clsLibSawVista

   vSQL = "SELECT "
   vSQL = vSQL & "cxC.ConsecutivoCompania, "
   vSQL = vSQL & "cxC.Numero, "
   vSQL = vSQL & "cxC.Fecha, "
   vSQL = vSQL & "cxC.FechaVencimiento, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("cxC.TipoCxc", enum_TipoDeTransaccion.eTD_FACTURA, _
                        gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "") & " AS TipoDocumento, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDeTransaccion & ".CodPeruTipoTransaccion, "
   vSQL = vSQL & "cxC.CodigoCliente, "
   vSQL = vSQL & "cliente.TipoDocumentoIdentificacion, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".CodPeruTipoDocumentoIdentificacion, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".StrTipoDocumentoIdentificacion, "
   vSQL = vSQL & "cliente.NumeroRIF, "
   vSQL = vSQL & "cliente.Nombre, "
   vSQL = vSQL & " 0 AS MontoExportacion, "
   vSQL = vSQL & gUtilSQL.getIIF("cxC.Status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO), "cxC.MontoGravado * cxC.CambioAbolivares", "0") & " As MontoGravado, "
   vSQL = vSQL & gUtilSQL.getIIF("cxC.Status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO), "cxC.MontoExento * cxC.CambioAbolivares", "0") & " As MontoExento, "
   vSQL = vSQL & gUtilSQL.getIIF("cxC.Status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO), "cxC.MontoIva * cxC.CambioAbolivares", "0") & " As TotalIVA, "
   'vSQL = vSQL & "cxC.MontoExento, "
   'vSQL = vSQL & "cxC.MontoIva AS TotalIVA, "
   vSQL = vSQL & " 0 AS AlicuotaExtendida, "
   vSQL = vSQL & gUtilSQL.getIIF("cxC.Status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO), "(cxC.MontoGravado + cxC.MontoExento + cxC.MontoIva) * cxC.CambioAbolivares", "0") & " As TotalFactura, "
   'vSQL = vSQL & "cxC.MontoGravado + cxC.MontoExento + cxC.MontoIva AS TotalFactura,"
   vSQL = vSQL & "cxC.CambioAbolivares, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("") & " AS NumeroFacturaAfectada "
   vSQL = vSQL & " FROM  cxC INNER JOIN "
   vSQL = vSQL & " cliente ON cxC.ConsecutivoCompania = cliente.ConsecutivoCompania "
   vSQL = vSQL & " AND cxC.CodigoCliente = cliente.Codigo "
   vSQL = vSQL & " INNER JOIN " & gEnumVista.GetViewNameEnumTipoDeTransaccion
   vSQL = vSQL & " ON cxC.TipoCxc " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = " & gEnumVista.GetViewNameEnumTipoDeTransaccion & ".EnumValue "
   vSQL = vSQL & " INNER JOIN " & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion
   vSQL = vSQL & " ON cliente.TipoDocumentoIdentificacion " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = " & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".EnumValue "
   vSQL = vSQL & " WHERE (cxC.Origen <> " & gUtilSQL.fSimpleSqlValue(enum_OrigenFacturacionOManual.eOF_FACTURA) & ")"
   'vSQL = vSQL & " AND (cxC.Status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO) & ")"

   Set gEnumProyecto = Nothing
   Set gEnumVista = Nothing
   Set insLibVista = Nothing
   fSqlViewCxcConCliente = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCxcConCliente", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameFacturaAfectada() As String
   GetViewNameFacturaAfectada = "IGV_FactAfectada_B2"
End Function

Public Function fSqlViewFacturaAfectada() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumVista As clsSawEnumVista
   Dim insLibVista As clsLibSawVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumVista = New clsSawEnumVista
   Set insLibVista = New clsLibSawVista
   
   vSQL = "SELECT "
   vSQL = vSQL & "factura.ConsecutivoCompania, "
   vSQL = vSQL & "factura.Numero, "
   vSQL = vSQL & "factura.Fecha, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("Factura.TipoDeDocumento", enum_TipoDocumentoFactura.eTF_FACTURA, _
                        gEnumProyecto.fenumTipoDocumentoFacturaToStringInArray(True, False), "") & " AS TipoDocumento, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoDeFactura & ".CodPeruTipoDocumentoFactura"
   vSQL = vSQL & " FROM Factura "
   vSQL = vSQL & " INNER JOIN " & gEnumVista.GetViewNameEnumTipoDocumentoDeFactura
   vSQL = vSQL & " ON factura.TipoDeDocumento " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = " & gEnumVista.GetViewNameEnumTipoDocumentoDeFactura & ".EnumValue"

   Set gEnumProyecto = Nothing
   Set gEnumVista = Nothing
   Set insLibVista = Nothing
   fSqlViewFacturaAfectada = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewFacturaAfectada", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameUnionFacturaYCxc() As String
   GetViewNameUnionFacturaYCxc = "IGV_FactUnionCxc_pFB1CB1_B3"
End Function

Public Function fSqlViewUnionFacturaYCxc() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
   vSQL = "SELECT "
   vSQL = vSQL & "ConsecutivoCompania, "
   vSQL = vSQL & "Numero, "
   vSQL = vSQL & "Fecha, "
   vSQL = vSQL & "FechaDeVencimiento, "
   vSQL = vSQL & "TipoDocumento, "
   vSQL = vSQL & "CodPeruTipoDocumentoFactura, "
   vSQL = vSQL & "TipoDocumentoIdentificacion, "
   vSQL = vSQL & "CodPeruTipoDocumentoIdentificacion, "
   vSQL = vSQL & "StrTipoDocumentoIdentificacion, "
   vSQL = vSQL & "CodigoCliente, "
   vSQL = vSQL & "NumeroRIF, "
   vSQL = vSQL & "Nombre, "
   vSQL = vSQL & "MontoExportacion, "
   vSQL = vSQL & "TotalBaseImponible, "
   vSQL = vSQL & "TotalMontoExento, "
   vSQL = vSQL & "MontoIVAAlicuota1, "
   vSQL = vSQL & "MontoIVAAlicuota3, "
   vSQL = vSQL & "TotalFactura, "
   vSQL = vSQL & "CambioAbolivares, "
   vSQL = vSQL & "NumeroFacturaAfectada "
   vSQL = vSQL & "FROM "
   vSQL = vSQL & "IGV_FactInfoCliente_B1 "
   
   vSQL = vSQL & "UNION "
   
   vSQL = vSQL & "SELECT "
   vSQL = vSQL & "ConsecutivoCompania, "
   vSQL = vSQL & "Numero, "
   vSQL = vSQL & "Fecha, "
   vSQL = vSQL & "FechaVencimiento, "
   vSQL = vSQL & "TipoDocumento, "
   vSQL = vSQL & "CodPeruTipoTransaccion, "
   vSQL = vSQL & "TipoDocumentoIdentificacion, "
   vSQL = vSQL & "CodPeruTipoDocumentoIdentificacion, "
   vSQL = vSQL & "StrTipoDocumentoIdentificacion, "
   vSQL = vSQL & "CodigoCliente, "
   vSQL = vSQL & "NumeroRIF, "
   vSQL = vSQL & "Nombre, "
   vSQL = vSQL & "MontoExportacion, "
   vSQL = vSQL & "MontoGravado,"
   vSQL = vSQL & "montoExento, "
   vSQL = vSQL & "TotalIVA, "
   vSQL = vSQL & "AlicuotaExtendida, "
   vSQL = vSQL & "TotalFactura, "
   vSQL = vSQL & "CambioAbolivares, "
   vSQL = vSQL & "NumeroFacturaAfectada "
   vSQL = vSQL & "FROM "
   vSQL = vSQL & "IGV_CxcInfoCliente_B1"
   
   fSqlViewUnionFacturaYCxc = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewUnionFacturaYCxc", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameRegistroVentasPeru() As String
   GetViewNameRegistroVentasPeru = "IGV_RegistroVentas_pFB3FB2"
End Function

Public Function fSqlViewRegistroVentasPeru() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   
   vSQL = "SELECT "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".ConsecutivoCompania, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".Numero, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".Fecha, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".FechaDeVencimiento, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".TipoDocumento, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".CodPeruTipoDocumentoFactura, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".TipoDocumentoIdentificacion, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".CodPeruTipoDocumentoIdentificacion, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".StrTipoDocumentoIdentificacion, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".CodigoCliente, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".NumeroRIF, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".Nombre, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".MontoExportacion, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".TotalBaseImponible, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".TotalMontoExento, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".MontoIVAAlicuota1, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".MontoIVAAlicuota3, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".TotalFactura, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".CambioABolivares, "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & ".NumeroFacturaAfectada, "
   vSQL = vSQL & GetViewNameFacturaAfectada & ".Fecha AS FechaFacturaAfectada, "
   vSQL = vSQL & GetViewNameFacturaAfectada & ".CodPeruTipoDocumentoFactura AS TipoDeDocumentoOriginal "
   vSQL = vSQL & " FROM  "
   vSQL = vSQL & GetViewNameFacturaAfectada & " RIGHT JOIN "
   vSQL = vSQL & GetViewNameUnionFacturaYCxc & " ON " & GetViewNameFacturaAfectada & ".ConsecutivoCompania = " & GetViewNameUnionFacturaYCxc & ".ConsecutivoCompania "
   vSQL = vSQL & " AND " & GetViewNameFacturaAfectada & ".Numero = " & GetViewNameUnionFacturaYCxc & ".NumeroFacturaAfectada"
   
   fSqlViewRegistroVentasPeru = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRegistroVentasPeru", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCxPparaRegistroComprasPeru() As String
   GetViewNameCxPparaRegistroComprasPeru = "IGV_CxPparaRegistroCompras_B2"
End Function

Public Function fSqlViewCxPparaRegistroComprasPeru() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumVista As clsSawEnumVista
   Dim insLibVista As clsLibSawVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumVista = New clsSawEnumVista
   Set insLibVista = New clsLibSawVista

   vSQL = "SELECT "
   vSQL = vSQL & "cxP.ConsecutivoCompania, "
   vSQL = vSQL & "cxP.Numero, "
   vSQL = vSQL & "cxP.Fecha, "
   vSQL = vSQL & "cxP.FechaVencimiento, "
   vSQL = vSQL & gUtilSQL.DfMidSQL("TipoDeDocumentoLey.Descripcion", 1, 22) & " AS TipoDocumentoDescripcion, "
   vSQL = vSQL & "cxP.CodigoTipoDeDocumentoLey AS TipoDocumento, "
   vSQL = vSQL & "cxP.CodigoProveedor, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".CodPeruTipoDocumentoIdentificacion, "
   vSQL = vSQL & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".StrTipoDocumentoIdentificacion, "
   vSQL = vSQL & "proveedor.NumeroRIF, "
   vSQL = vSQL & "proveedor.NombreProveedor, "
   vSQL = vSQL & "cxP.CreditoFiscal, "
   vSQL = vSQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSimpleSqlValue(enum_CreditoFiscal.eCF_DEDUCIBLE), _
                                 "cxP.MontoGravableAlicuotaGeneral", 0, True) & " AS BaseImponibleOpGrav, "
   vSQL = vSQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSimpleSqlValue(enum_CreditoFiscal.eCF_DEDUCIBLE), _
                                 "cxP.MontoIvaalicuotaGeneral", 0, True) & " AS MontoAlicuotaOpGrav, "
   vSQL = vSQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSimpleSqlValue(enum_CreditoFiscal.eCF_PRORRATEABLE), _
                                 "cxP.MontoGravableAlicuotaGeneral", 0, True) & " AS BaseImponibleOpGravYNoGrav, "
   vSQL = vSQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSimpleSqlValue(enum_CreditoFiscal.eCF_PRORRATEABLE), _
                                 "cxP.MontoIvaalicuotaGeneral", 0, True) & " AS MontoAlicuotaOpGravYNoGrav, "
   vSQL = vSQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSimpleSqlValue(enum_CreditoFiscal.eCF_NoDeducible), _
                                 "cxP.MontoGravableAlicuotaGeneral", 0, True) & " AS BaseImponibleOpNoGrav, "
   vSQL = vSQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSimpleSqlValue(enum_CreditoFiscal.eCF_NoDeducible), _
                                 "cxP.MontoIvaalicuotaGeneral", 0, True) & " AS MontoAlicuotaOpNoGrav,"
   vSQL = vSQL & gUtilSQL.getIIF("cxp.status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO), "cxP.MontoExento * cxp.CambioAbolivares", "0") & " AS MontoExento, "
   vSQL = vSQL & gUtilSQL.getIIF("cxp.status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO), "cxP.MontoIvaalicuota3 * cxp.CambioAbolivares", "0") & " AS MontoIvaalicuota3, "
   'vSQL = vSQL & "cxP.MontoIvaalicuota3, "
   vSQL = vSQL & gUtilSQL.getIIF("cxp.status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO), " (cxP.MontoGravado + cxP.MontoIvaalicuotaGeneral + cxP.MontoIvaalicuota3 + cxP.MontoExento) * cxp.CambioAbolivares", "0", True) & " AS ImporteTotal, "
   'vSQL = vSQL & "cxP.MontoGravado + cxP.MontoIvaalicuotaGeneral + cxP.MontoIvaalicuota3 + cxP.MontoExento AS ImporteTotal, "
   vSQL = vSQL & "cxp.CambioAbolivares, "
   vSQL = vSQL & "cxp.NumeroDetraccion, "
   vSQL = vSQL & "cxp.FechaDetraccion, "
   vSQL = vSQL & "cxP.NumeroDeFacturaAfectada "
   vSQL = vSQL & " FROM cxP INNER JOIN "
   vSQL = vSQL & " proveedor ON cxP.ConsecutivoCompania = proveedor.ConsecutivoCompania "
   vSQL = vSQL & " AND cxP.CodigoProveedor = proveedor.CodigoProveedor "
   vSQL = vSQL & " INNER JOIN " & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion
   vSQL = vSQL & " ON proveedor.TipoDocumentoIdentificacion " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = " & gEnumVista.GetViewNameEnumTipoDocumentoIdentificacion & ".EnumValue "
   vSQL = vSQL & " INNER JOIN "
   vSQL = vSQL & " TipoDeDocumentoLey ON cxP.CodigoTipoDeDocumentoLey = TipoDeDocumentoLey.Codigo "
   'vSQL = vSQL & " WHERE (cxp.status <> " & gUtilSQL.fSimpleSqlValue(enum_StatusDocumento.eSD_ANULADO) & ") "

   Set gEnumProyecto = Nothing
   Set gEnumVista = Nothing
   Set insLibVista = Nothing
   fSqlViewCxPparaRegistroComprasPeru = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCxPparaRegistroComprasPeru", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCxPAfectada() As String
   GetViewNameCxPAfectada = "IGV_CxPAfectada_B1"
End Function

Public Function fSqlViewCxPAfectada() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   Dim gEnumVista As clsSawEnumVista
   Dim insLibVista As clsLibSawVista
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   Set gEnumVista = New clsSawEnumVista
   Set insLibVista = New clsLibSawVista
   
   vSQL = "SELECT "
   vSQL = vSQL & "Cxp.ConsecutivoCompania, "
   vSQL = vSQL & "cxP.Numero, "
   vSQL = vSQL & "cxP.Fecha, "
   vSQL = vSQL & gUtilSQL.DfMidSQL("TipoDeDocumentoLey.Descripcion", 1, 22) & " AS TipoDocumentoDescripcion, "
   vSQL = vSQL & "CxP.CodigoTipoDeDocumentoLey AS TipoDocumento"
   vSQL = vSQL & " FROM cxP "
   vSQL = vSQL & " LEFT JOIN TipoDeDocumentoLey "
   vSQL = vSQL & " ON cxP.CodigoTipoDeDocumentoLey " & insLibVista.fCollateModernSpanishCaseSensitiveAccentSensitive & " = TipoDeDocumentoLey.Codigo "
   Set gEnumProyecto = Nothing
   Set gEnumVista = Nothing
   Set insLibVista = Nothing
   fSqlViewCxPAfectada = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCxPAfectada", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameRegistroComprasPeru() As String
   GetViewNameRegistroComprasPeru = "IGV_RegistroCompras"
End Function

Public Function fSqlViewRegistroComprasPeru() As String
   Dim vSQL As String
   On Error GoTo h_ERROR

   vSQL = "SELECT "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".ConsecutivoCompania, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".Numero, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".Fecha, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".FechaVencimiento, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".TipoDocumentoDescripcion, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".TipoDocumento, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".CodigoProveedor, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".CodPeruTipoDocumentoIdentificacion, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".StrTipoDocumentoIdentificacion, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".NumeroRIF, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".NombreProveedor, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".BaseImponibleOpGrav, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".MontoAlicuotaOpGrav, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".BaseImponibleOpGravYNoGrav, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".MontoAlicuotaOpGravYNoGrav, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".BaseImponibleOpNoGrav, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".MontoAlicuotaOpNoGrav, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".MontoExento, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".MontoIvaalicuota3, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".ImporteTotal, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".CambioAbolivares, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".FechaDetraccion, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".NumeroDetraccion, "
   vSQL = vSQL & GetViewNameCxPparaRegistroComprasPeru & ".NumeroDeFacturaAfectada, "
   vSQL = vSQL & GetViewNameCxPAfectada & ".Fecha AS FechaDocumentoOriginal, "
   vSQL = vSQL & GetViewNameCxPAfectada & ".TipoDocumento AS TipoDocumentoOriginal "
   vSQL = vSQL & " FROM " & GetViewNameCxPparaRegistroComprasPeru
   vSQL = vSQL & " LEFT JOIN " & GetViewNameCxPAfectada & " ON " & GetViewNameCxPparaRegistroComprasPeru & ".ConsecutivoCompania = " & GetViewNameCxPAfectada & ".ConsecutivoCompania"
   vSQL = vSQL & " AND " & GetViewNameCxPparaRegistroComprasPeru & ".NumeroDeFacturaAfectada = " & GetViewNameCxPAfectada & ".Numero"
   fSqlViewRegistroComprasPeru = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRegistroComprasPeru", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

