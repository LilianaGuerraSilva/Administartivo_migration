VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxCVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsCxPVista"
Private Const CM_MESSAGE_NAME As String = "Vista CxP"
Private gEnumProyecto As clsEnumAdministrativo
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNameVentasGeneradasCxC() As String
   GetViewNameVentasGeneradasCxC = "IGV_VentasGeneradasCxC_B1"
End Function
Public Function GetViewNameDocCobradosPorMoneda() As String
   GetViewNameDocCobradosPorMoneda = "IGV_DocCobradosPorMoneda"
End Function

Public Function fSqlViewVentasGeneradasCxC() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = "SELECT "
   vSQL = vSQL & "ConsecutivoCompania, "
   vSQL = vSQL & "Numero, "
   vSQL = vSQL & gUtilSQL.getIIF("MontoExento IS NULL", 0, "MontoExento", True) & " AS ventasExentas, "
   vSQL = vSQL & gUtilSQL.getIIF("MontoExento IS NULL", 0, "MontoExento", True) & " AS VentasInternasNoGravadas, "
   vSQL = vSQL & "0 AS VentasDeExportacion, "
   vSQL = vSQL & gUtilSQL.getIIF("MontoGravado IS NULL", 0, "MontoGravado", True) & " AS VentasIntBaseAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("MontoIva IS NULL", 0, "MontoIva", True) & " AS VentasIntIvaAlicuotaGeneral, "
   vSQL = vSQL & "0 AS VentasIntBaseAlicuotaGeneralMasAdicional, "
   vSQL = vSQL & "0 AS VentasIntIvaAlicuotaGeneralMasAdicional, "
   vSQL = vSQL & "0 AS VentasIntBaseAlicuotaReducida, "
   vSQL = vSQL & "0 AS VentasIntIvaAlicuotaReducida, "
   vSQL = vSQL & "0 AS MontoEditableAjusteDebito, "
   vSQL = vSQL & "0 AS RetencionesDelPeriodo, "
   vSQL = vSQL & "0 AS DescVentasGeneral, "
   vSQL = vSQL & "0 AS DescIvaAlicuotaGeneralVentasInt, "
   vSQL = vSQL & "CambioAbolivares, "
   vSQL = vSQL & "Fecha, "
   vSQL = vSQL & "Origen, "
   vSQL = vSQL & "Status, "
   vSQL = vSQL & "Moneda "
   vSQL = vSQL & "FROM "
   vSQL = vSQL & "cxC "
   vSQL = vSQL & "WHERE "
   vSQL = vSQL & "Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   vSQL = vSQL & "AND "
   vSQL = vSQL & "Origen = " & gUtilSQL.fSimpleSqlValue(enum_OrigenFacturacionOManual.eOF_MANUAL)
   vSQL = vSQL & " AND NoAplicaParaLibroDeVentas = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlViewVentasGeneradasCxC = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasGeneradasCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlViewDocumentoCobrado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = vSQL & " SELECT SUM(DocumentoCobrado.MontoAbonado / CambioAMonedaDeCobranza ) as SumaMoneda,  "
   vSQL = vSQL & " Cobranza.ConsecutivoCompania, Cobranza.Numero as NumeroCobranza, Cobranza.CodigoCliente, DocumentoCobrado.CodigoMonedaDeCxC "
   vSQL = vSQL & " FROM DocumentoCobrado INNER JOIN COBRANZA ON "
   vSQL = vSQL & " (cobranza.Numero = documentoCobrado.NumeroCobranza) "
   vSQL = vSQL & " AND (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania) "
   vSQL = vSQL & " WHERE StatusCobranza <> '1' "
   vSQL = vSQL & " GROUP BY DocumentoCobrado.CodigoMonedaDeCxC, Cobranza.ConsecutivoCompania, Cobranza.Numero, Cobranza.CodigoCliente"
   fSqlViewDocumentoCobrado = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewDocumentoCobrado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameVentas() As String
   GetViewNameVentas = "IGV_Ventas"
End Function
Public Function GetViewNameVentasAgrupado() As String
   GetViewNameVentasAgrupado = "IGV_VentasAgrupado"
End Function
Public Function GetViewNameVentasDetallado() As String
   GetViewNameVentasDetallado = "IGV_VentasDetallado"
End Function
Public Function fSqlViewVentasIVA() As String 'Agrupado por tipo de contribuyente
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSqlViewVentasFactura
   vSQL = vSQL & vbCrLf & " UNION " & vbCrLf & fSqlViewVentasCxC
   vSQL = vSQL & vbCrLf & " UNION " & vbCrLf & fSqlViewVentasResumen
   vSQL = vSQL & vbCrLf & " UNION " & vbCrLf & fSqlViewVentasResumenManual
   fSqlViewVentasIVA = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasFactura() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT factura.ConsecutivoCompania AS ConsecutivoCompania, "
   vSQL = vSQL & " factura.Numero AS Numero,"
   vSQL = vSQL & "(SELECT CodTipoDocumentoSawIva FROM IGV_EnumTipoDocumentoIvaFactura WHERE DbValue = factura.TipoDeDocumento COLLATE Modern_Spanish_CS_AS) AS TipoDeDocumento, "
   vSQL = vSQL & "(SELECT codTipoDeVentaIVA FROM IGV_EnumTipoDeVentaIva WHERE DbValue = factura.TipoDeVenta COLLATE Modern_Spanish_CS_AS ) AS TipoDeVenta , "
   vSQL = vSQL & " (CASE WHEN Factura.EsDiferida = 'S' THEN '1' ELSE '0' END) AS StatusVenta,"
   vSQL = vSQL & " (CASE WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END) AS StatusRetencion, "
   vSQL = vSQL & fSqlConvierteMonto("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", " Factura.Fecha") & " as MontoIvaRetenido, "
   vSQL = vSQL & " factura.TipoDeTransaccion AS TipoDeTransaccion,"
   vSQL = vSQL & " factura.Fecha AS Fecha,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN MONTH(Factura.FechaAplicacionRetIVA) ELSE MONTH(Factura.Fecha) END) AS MesDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN YEAR(Factura.FechaAplicacionRetIVA) ELSE YEAR(Factura.Fecha) END) AS anoDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN DAY(Factura.FechaAplicacionRetIVA) ELSE DAY(Factura.Fecha) END) AS DiaDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN Factura.FechaAplicacionRetIVA ELSE Factura.Fecha END) AS FechaAplicacion,"
   vSQL = vSQL & " factura.CodigoCliente AS CodigoCliente,"
   vSQL = vSQL & " Factura.NumeroHasta AS NumeroHasta,"
   vSQL = vSQL & " '' AS PorcentajePasajeAereo,"
   vSQL = vSQL & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSQL = vSQL & " factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSQL = vSQL & " factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSQL = vSQL & " factura.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSQL = vSQL & " factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalFactura * factura.CambioABolivares", 2), True) & " as TotalFactura,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalMontoExento * factura.CambioABolivares", 2), True) & " as Montoexento,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True) & " as  MontogravableAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota2 * factura.CambioABolivares", 2), True) & " as  MontoGravableAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota3 * factura.CambioABolivares", 2), True) & " as  MontoGravableAlicuota3,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True) & " as MontoIVAAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota3 * factura.CambioABolivares", 2), True) & " as  MontoIVAAlicuota3,"
   vSQL = vSQL & " factura.NombreOperador AS NombreOperador,"
   vSQL = vSQL & " factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSQL = vSQL & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSQL = vSQL & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSQL = vSQL & " (CASE Factura.Talonario WHEN '0' THEN '1' ELSE '2'  END ) AS ConsecutivoTalonario,"
   vSQL = vSQL & " '' AS  NumeroResumenDiario,"
   vSQL = vSQL & " 'N' AS EsCuentaDeTercero,"
   vSQL = vSQL & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSQL = vSQL & " ,factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSQL = vSQL & " ,factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSQL = vSQL & " ," & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
         " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2"
   vSQL = vSQL & vbCrLf & " FROM factura LEFT JOIN" & vbCrLf
   vSQL = vSQL & vbCrLf & " IGV_RetencionDocumentoCobrado ON factura.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND" & vbCrLf
   vSQL = vSQL & vbCrLf & " factura.Numero = IGV_RetencionDocumentoCobrado.NumeroDocumentoOrigen AND" & vbCrLf
   vSQL = vSQL & vbCrLf & " (CASE Factura.TipoDeDocumento WHEN '0' THEN '0' ELSE (CASE Factura.TipoDeDocumento WHEN '1' THEN '3' ELSE (CASE Factura.TipoDeDocumento WHEN '2' THEN" & vbCrLf
   vSQL = vSQL & vbCrLf & " '4' END) END) END) = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado " & vbCrLf
   vSQL = vSQL & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON Factura.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSQL = vSQL & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota" & vbCrLf
   vSQL = vSQL & vbCrLf & " WHERE factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSQL = vSQL & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vSQL = vSQL & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vSQL = vSQL & vbCrLf & " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   fSqlViewVentasFactura = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasCxC() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT  CxC.ConsecutivoCompania AS ConsecutivoCompania,  CxC.Numero AS Numero, "
   vSQL = vSQL & " IGV_EnumTipoDocumentoIva.CodTipoDocumentoFacturaSawIva  AS TipoDeDocumento, "
   vSQL = vSQL & " '2' AS TipoDeVenta, "
   vSQL = vSQL & " '0' AS StatusVenta, "
   vSQL = vSQL & " ( CASE  WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSQL = vSQL & fSqlConvierteMontoCXC("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", "CxC.Fecha") & " as MontoIvaRetenido, "
   vSQL = vSQL & " cxc.TipoCxc AS TipoDeTransaccion, Cxc.Fecha AS Fecha, "
   vSQL = vSQL & " MONTH(CxC.Fecha) AS mesDeAplicacion, "
   vSQL = vSQL & " YEAR(CxC.Fecha) AS anoDeAplicacion, "
   vSQL = vSQL & " DAY(CxC.Fecha) AS DiaDeAplicacion, "
   vSQL = vSQL & " CxC.Fecha AS FechaAplicacion, "
   vSQL = vSQL & " CxC.CodigoCliente AS CodigoCliente, '' AS NumeroHasta, '' AS PorcentajePasajeAereo, "
   vSQL = vSQL & " Cxc.NumeroControl AS NumeroDeControlDeFacturaHasta, '' AS CodigoMaquinaRegistradora, '' AS NumeroDePlanillaExpotacion, "
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion, '' AS NumeroFacturaAfectada, Cxc.NumeroControl AS NumeroDeControlDeFactura, "
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion, "
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion, "
   vSQL = vSQL & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw("(CxC.montoIva + CxC.montoGravado + CxC.montoExento) * CxC.CambioAbolivares ", 2)) & " AS TotalCxC, "
   vSQL = vSQL & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw(" CxC.montoExento * CxC.CambioAbolivares ", 2)) & " AS MontoExento, "
   vSQL = vSQL & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw(" CxC.montoGravado * CxC.CambioAbolivares ", 2)) & " AS MontoGravableAlicuotaGeneral, "
   vSQL = vSQL & " 0 AS MontoGravableAlicuota2, 0 AS MontoGravableAlicuota3,"
   vSQL = vSQL & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw(" CxC.montoIva * CxC.CambioAbolivares ", 2)) & " AS MontoIVAAlicuotaGeneral, "
   vSQL = vSQL & " 0 AS MontoIVAAlicuota2, 0 AS MontoIVAAlicuota3,  CxC.NombreOperador AS NombreOperador,"
   vSQL = vSQL & " CxC.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " (SELECT MontoAlicuotaGeneral FROM AlicuotaIva WHERE  FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA"
   vSQL = vSQL & " From AlicuotaIva  WHERE FechaDeInicioDeVigencia < Cxc.Fecha)) As PorcentajeIvaAlicuotaGeneral,"
   vSQL = vSQL & " (SELECT MontoAlicuota2 FROM AlicuotaIva WHERE FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA"
   vSQL = vSQL & " From AlicuotaIva  WHERE FechaDeInicioDeVigencia < Cxc.Fecha)) As PorcentajeIvaAlicuota2,"
   vSQL = vSQL & " (SELECT MontoAlicuota3 FROM AlicuotaIva WHERE  FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA"
   vSQL = vSQL & " From AlicuotaIva  WHERE FechaDeInicioDeVigencia < Cxc.Fecha)) As PorcentajeIvaAlicuota3,"
   vSQL = vSQL & " '' AS ConsecutivoTalonario, '' AS NumeroResumenDiario,"
   vSQL = vSQL & " 'N' AS EsCuentaDeTercero, 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSQL = vSQL & " ,'N' AS EsOriginalmenteDiferida"
   vSQL = vSQL & " ,'N' AS AplicaDecretoIvaEspecial"
   vSQL = vSQL & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoGravableAlicuotaEspecial1"
   vSQL = vSQL & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoGravableAlicuotaEspecial2"
   vSQL = vSQL & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoIVAAlicuotaEspecial1"
   vSQL = vSQL & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoIVAAlicuotaEspecial2"
   vSQL = vSQL & vbCrLf & " FROM CxC LEFT JOIN IGV_RetencionDocumentoCobrado ON"
   vSQL = vSQL & vbCrLf & " CxC.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND"
   vSQL = vSQL & vbCrLf & " Cxc.Numero = IGV_RetencionDocumentoCobrado.NumeroDelDocumentoCobrado AND"
   vSQL = vSQL & vbCrLf & " Cxc.TipoCxc = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado"
   vSQL = vSQL & vbCrLf & " INNER JOIN IGV_EnumTipoDocumentoIva ON"
   vSQL = vSQL & vbCrLf & " IGV_EnumTipoDocumentoIva.DbValue = Cxc.TipoCxc COLLATE Modern_Spanish_CS_AS"
   vSQL = vSQL & vbCrLf & " WHERE CxC.NoAplicaParaLibroDeVentas = " & gUtilSQL.fBooleanToSqlValue(False)
   vSQL = vSQL & vbCrLf & " AND CxC.Origen = " & gUtilSQL.fSimpleSqlValue(enum_OrigenFacturacionOManual.eOF_MANUAL)
   fSqlViewVentasCxC = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasResumen() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT Factura.ConsecutivoCompania AS ConsecutivoCompania,"
   vSQL = vSQL & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.PrimerNumeroComprobante as Numero,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVentaIva.eTD_TV_VENTASNOCONTRIBUYENTES), gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("4"), True) & " AS TipoDeDocumento, "
   vSQL = vSQL & " IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA AS TipoDeVenta, "
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS StatusVenta, "
   vSQL = vSQL & " (CASE  WHEN IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaRetenido <> 0 OR IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaRetenido IS NULL  THEN '1' ELSE '0' END ) AS StatusRetencion,"
   vSQL = vSQL & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaRetenido  as MontoIvaRetenido,"
   vSQL = vSQL & " factura.TipoDeTransaccion AS TipoDeTransaccion, factura.Fecha AS Fecha,"
   vSQL = vSQL & " MONTH(Factura.Fecha) AS MesDeAplicacion, YEAR(Factura.Fecha) AS anoDeAplicacion, DAY(Factura.Fecha) AS DiaDeAplicacion,Factura.Fecha as FechaAplicacion, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVentaIva.eTD_TV_VENTASNOCONTRIBUYENTES), gUtilSQL.fSimpleSqlValue("RD_Cliente"), "factura.CodigoCliente", True) & " AS TipoDeDocumento, "
   vSQL = vSQL & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.UltimoNumeroComprobante AS NumeroHasta, '' AS PorcentajePasajeAereo,"
   vSQL = vSQL & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta, factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSQL = vSQL & " factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSQL = vSQL & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSQL = vSQL & " factura.NumeroFacturaAfectada AS NumeroFacturaAfectada, factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSQL = vSQL & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSQL = vSQL & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.FechaComprobanteRetIva AS FechaAplicacionRetencion,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.TotalFactura * factura.CambioABolivares", 2), True) & " as TotalFactura,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.Montoexento * factura.CambioABolivares", 2), True) & " as Montoexento,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
            " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
            " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuotaGeneral * Factura.CambioABolivares", 2), True) & " as MontogravableAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
         fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota3,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
            " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
            " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuotaGeneral * Factura.CambioABolivares", 2), True) & " as  MontoIVAAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
         fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
         fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota3,"
   vSQL = vSQL & " factura.NombreOperador AS NombreOperador, factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSQL = vSQL & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSQL = vSQL & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSQL = vSQL & " (CASE WHEN Factura.SerialMaquinaFiscal <> '' THEN Factura.SerialMaquinaFiscal ELSE '0'  END ) AS ConsecutivoTalonario,"
   vSQL = vSQL & " factura.NumeroResumenDiario AS NumeroResumenDiario,"
   vSQL = vSQL & " 'N' AS EsCuentaDeTercero, 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSQL = vSQL & " ,factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSQL = vSQL & " ,factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSQL = vSQL & " ," & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
           " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
            " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
            " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
            " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2"
   vSQL = vSQL & vbCrLf & " FROM factura"
   vSQL = vSQL & vbCrLf & " INNER JOIN IGV_BaseimponibleAlicuotasVentasResumenDeVentas ON"
   vSQL = vSQL & vbCrLf & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.ConsecutivoCompania   = Factura.ConsecutivoCompania AND"
   vSQL = vSQL & vbCrLf & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.PrimerNumeroFactura = factura.Numero "
   vSQL = vSQL & vbCrLf & " INNER JOIN"
   vSQL = vSQL & vbCrLf & " dbo.IGV_EnumTipoDeVentaIva ON"
   vSQL = vSQL & vbCrLf & " dbo.IGV_BaseimponibleAlicuotasVentasResumenDeVentas.TipoDeVenta COLLATE Modern_Spanish_CS_AS = dbo.IGV_EnumTipoDeVentaIva.DbValue"
   vSQL = vSQL & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON Factura.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSQL = vSQL & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota " & vbCrLf
   vSQL = vSQL & vbCrLf & " WHERE factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSQL = vSQL & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
   vSQL = vSQL & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vSQL = vSQL & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   vSQL = vSQL & vbCrLf & " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   vSQL = vSQL & vbCrLf & " AND IGV_BaseimponibleAlicuotasVentasResumenDeVentas.Fecha = factura.Fecha "
   fSqlViewVentasResumen = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasResumen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasResumenManual() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT factura.ConsecutivoCompania AS ConsecutivoCompania, "
   vSQL = vSQL & " factura.NumeroDesde  AS Numero, "
   vSQL = vSQL & " IGV_EnumTipoDocumentoIvaFactura.CodTipoDocumentoSawIva  AS TipoDeDocumento,"
   vSQL = vSQL & " IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA AS TipoDeVenta,"
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue(0) & " AS StatusVenta, "
   vSQL = vSQL & gUtilSQL.getIIF("Factura.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(False), gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("1"), True) & " AS StatusRetencion, "
   vSQL = vSQL & " Factura.MontoIvaRetenido  As MontoIvaRetenido, "
   vSQL = vSQL & " factura.TipoDeTransaccion AS TipoDeTransaccion,"
   vSQL = vSQL & " factura.Fecha AS Fecha,"
   vSQL = vSQL & " MONTH(Factura.Fecha) AS MesDeAplicacion,"
   vSQL = vSQL & " YEAR(Factura.Fecha) AS anoDeAplicacion,"
   vSQL = vSQL & " DAY(Factura.Fecha) AS DiaDeAplicacion,"
   vSQL = vSQL & " Factura.Fecha AS FechaAplicacion,"
   vSQL = vSQL & " factura.CodigoCliente AS CodigoCliente,"
   vSQL = vSQL & " Factura.NumeroHasta AS NumeroHasta,"
   vSQL = vSQL & " '' AS PorcentajePasajeAereo,"
   vSQL = vSQL & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSQL = vSQL & " Factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSQL = vSQL & " Factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSQL = vSQL & " Factura.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSQL = vSQL & " Factura.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSQL = vSQL & " Factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSQL = vSQL & " Factura.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSQL = vSQL & " Factura.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.TotalFactura ", True) & " AS TotalFactura,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.TotalMontoExento", True) & " AS TotalMontoExento,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.MontoGravableAlicuota1", True) & " AS MontoGravableAlicuota1,"
   vSQL = vSQL & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoGravableAlicuota2", True) & " AS MontoGravableAlicuota2, "
   vSQL = vSQL & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoGravableAlicuota3", True) & " AS MontoGravableAlicuota3, "
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura =" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.MontoIVAAlicuota1 ", True) & " AS MontoIVAAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoIVAAlicuota2", True) & " AS MontoIVAAlicuota2, "
   vSQL = vSQL & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoIVAAlicuota3", True) & " AS MontoIVAAlicuota3, "
   vSQL = vSQL & " factura.NombreOperador AS NombreOperador,"
   vSQL = vSQL & " factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSQL = vSQL & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSQL = vSQL & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSQL = vSQL & " factura.SerialMaquinaFiscal AS ConsecutivoTalonario,"
   vSQL = vSQL & " factura.NumeroResumenDiario AS NumeroResumenDiario,"
   vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS EsCuentaDeTercero,"
   vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS MontosConvertido, " & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoIvaPercibido"
   vSQL = vSQL & " ,factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSQL = vSQL & " ,factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial, "
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
          "Factura.MontoGravableAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1, "
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
          "Factura.MontoGravableAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2, "
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
          "Factura.MontoIVAAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1, "
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
          "Factura.MontoIVAAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2"
   vSQL = vSQL & vbCrLf & " FROM factura LEFT JOIN"
   vSQL = vSQL & vbCrLf & " Cxc ON CxC.ConsecutivoCompania = factura.ConsecutivoCompania AND CxC.NumeroDocumentoOrigen = factura.Numero AND"
   vSQL = vSQL & vbCrLf & " CxC.CodigoCliente = factura.CodigoCliente"
   vSQL = vSQL & vbCrLf & " INNER JOIN IGV_EnumTipoDocumentoIvaFactura"
   vSQL = vSQL & vbCrLf & " ON IGV_EnumTipoDocumentoIvaFactura.DbValue = Factura.TipoDeDocumento COLLATE Modern_Spanish_CS_AS"
   vSQL = vSQL & vbCrLf & " INNER JOIN IGV_EnumTipoDeVentaIva"
   vSQL = vSQL & vbCrLf & " ON IGV_EnumTipoDeVentaIva.DbValue = Factura.TipoDeVenta COLLATE Modern_Spanish_CS_AS"
   vSQL = vSQL & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial1 ON Factura.Fecha BETWEEN AlicuotaImpuestoEspecial1.FechaDesde AND AlicuotaImpuestoEspecial1.FechaHasta AND" & vbCrLf
   vSQL = vSQL & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota2 = AlicuotaImpuestoEspecial1.Alicuota" & vbCrLf
   vSQL = vSQL & vbCrLf & " AND AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida)
   vSQL = vSQL & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial2 ON Factura.Fecha BETWEEN AlicuotaImpuestoEspecial2.FechaDesde AND AlicuotaImpuestoEspecial2.FechaHasta AND" & vbCrLf
   vSQL = vSQL & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota3 = AlicuotaImpuestoEspecial2.Alicuota" & vbCrLf
   vSQL = vSQL & vbCrLf & " AND AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida)
   vSQL = vSQL & vbCrLf & " WHERE factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSQL = vSQL & vbCrLf & " AND RealizoCierreZ = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlViewVentasResumenManual = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasResumenManual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameRetencionDocumentoCobrado() As String
   GetViewNameRetencionDocumentoCobrado = "IGV_RetencionDocumentoCobrado"
End Function

Public Function fSqlViewRetencionDocumentoCobrado() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "SELECT dbo.documentoCobrado.ConsecutivoCompania, dbo.documentoCobrado.NumeroComprobanteRetIVA, dbo.documentoCobrado.FechaComprobanteRetIVA, dbo.documentoCobrado.NumeroDelDocumentoCobrado AS NumeroDelDocumentoCobrado, " & vbCrLf
   vResult = vResult & "        dbo.documentoCobrado.MontoIvaRetenido, dbo.documentoCobrado.SeRetuvoIVA, dbo.documentoCobrado.TipoDeDocumentoCobrado," & vbCrLf
   vResult = vResult & "        dbo.cobranza.Fecha AS FechaAplicacionRetIVA, dbo.cobranza.CodigoCliente, dbo.cxC.NumeroDocumentoOrigen" & vbCrLf
   vResult = vResult & " FROM   dbo.cxC INNER JOIN" & vbCrLf
   vResult = vResult & "        dbo.documentoCobrado ON dbo.cxC.Numero = dbo.documentoCobrado.NumeroDelDocumentoCobrado AND" & vbCrLf
   vResult = vResult & "        dbo.cxC.TipoCxC = dbo.documentoCobrado.TipoDeDocumentoCobrado AND" & vbCrLf
   vResult = vResult & "        dbo.cxC.ConsecutivoCompania = dbo.documentoCobrado.ConsecutivoCompania INNER JOIN" & vbCrLf
   vResult = vResult & "        dbo.cobranza ON dbo.documentoCobrado.ConsecutivoCompania = dbo.cobranza.ConsecutivoCompania AND" & vbCrLf
   vResult = vResult & "        dbo.documentoCobrado.NumeroCobranza = dbo.cobranza.Numero" & vbCrLf
   vResult = vResult & " WHERE        (dbo.documentoCobrado.SeRetuvoIVA = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (dbo.documentoCobrado.MontoIvaRetenido <> 0) AND (dbo.cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & ")" & vbCrLf
   fSqlViewRetencionDocumentoCobrado = vResult
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRetencionDocumentoCobrado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlConvierteMonto(ByVal valValor As String, ByVal valAnoAplicacion As String, ByVal valFecha As String) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSQL = vSQL & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" factura.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * factura.CambioABolivares * 0.001 ", False) & ")", _
                           "(" & gUtilSQL.getIIF("ABS(" & valValor & ") >= factura.MontoIvaRetenido ", valValor, valValor & " * factura.CambioABolivares ", False) & ")")
                           'valValor & " * factura.CambioABolivares")
   'OJO LA FUNCION NO GENERA ERRORES Y DEVUELVE LOS RESULTADO ESPERADOS PERO DEBE SER EVALUADA
   fSqlConvierteMonto = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlConvierteMonto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlConvierteMontoCXC(ByVal valValor As String, ByVal valAnoAplicacion As String, ByVal valFecha As String) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSQL = vSQL & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" cxC.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * cxC.CambioABolivares * 0.001 ", False) & ")", _
                           "(" & gUtilSQL.getIIF("ABS(" & valValor & ") >= cxC.MontoIva ", valValor, valValor & " * cxC.CambioABolivares ", False) & ")")
   'OJO LA FUNCION NO GENERA ERRORES Y DEVUELVE LOS RESULTADO ESPERADOS PERO DEBE SER EVALUADA
   fSqlConvierteMontoCXC = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlConvierteMontoCXC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameRetencionesEnVentas() As String
   GetViewNameRetencionesEnVentas = "IVG_RetencionesVentas"
End Function

Public Function fSqlViewRetencionesEnVentas() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT CXC.NumeroDocumentoOrigen as NumeroDocumentoOrigen,"
   vSQL = vSQL & " Cxc.CodigoCliente as CodigoCliente,"
   vSQL = vSQL & " Cxc.ConsecutivoCompania as ConsecutivoCompania,"
   vSQL = vSQL & " Cxc.TipoCxc as TipoCxc,"
   vSQL = vSQL & " Cxc.Fecha as Fecha,"
   vSQL = vSQL & " DocumentoCobrado.NumeroComprobanteRetIva as NumeroComprobanteRetIva,"
   vSQL = vSQL & " DocumentoCobrado.MontoIvaRetenido as MontoIvaRetenido,"
   vSQL = vSQL & " DocumentoCobrado.NumeroDelDocumentoCobrado as NumeroDelDocumentoCobrado,"
   vSQL = vSQL & " DocumentoCobrado.TipoDeDocumentoCobrado as TipoDeDocumentoCobrado,"
   vSQL = vSQL & " Cobranza.Fecha AS FechaComprobanteRetIva"
   vSQL = vSQL & " FROM Cxc"
   vSQL = vSQL & " INNER JOIN DocumentoCobrado ON"
   vSQL = vSQL & " DocumentoCobrado.NumeroComprobanteRetIva <> '0'"
   vSQL = vSQL & " AND DocumentoCobrado.MontoIvaRetenido > '0'"
   vSQL = vSQL & " AND DocumentoCobrado.NumeroDelDocumentoCobrado= Cxc.Numero"
   vSQL = vSQL & " AND DocumentoCobrado.TipoDeDocumentoCobrado = CXC.TipoCxc"
   vSQL = vSQL & " AND DocumentoCobrado.ConsecutivoCompania = CXC.ConsecutivoCompania"
   vSQL = vSQL & " INNER JOIN Cobranza ON"
   vSQL = vSQL & " Cobranza.CodigoCliente = Cxc.CodigoCliente"
   vSQL = vSQL & " AND Cobranza.Numero = DocumentoCobrado.NumeroCobranza"
   vSQL = vSQL & " AND Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   fSqlViewRetencionesEnVentas = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRetencionesEnVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameBaseimponibleAlicuotasVentasResumenDeVentas() As String
   GetViewNameBaseimponibleAlicuotasVentasResumenDeVentas = "IGV_BaseimponibleAlicuotasVentasResumenDeVentas"
End Function

Public Function fSqlViewBaseimponibleAlicuotasVentasResumenDeVentas() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT  dbo.factura.ConsecutivoCompania,"
   vSQL = vSQL & " MIN(factura.NumeroComprobanteFiscal) AS PrimerNumeroComprobante,"
   vSQL = vSQL & " MAX(factura.NumeroComprobanteFiscal) AS UltimoNumeroComprobante,"
   vSQL = vSQL & " MIN(factura.Numero) AS PrimerNumeroFactura,"
   vSQL = vSQL & " MAX(factura.Numero) AS UltimoNumeroFactura,"
   vSQL = vSQL & " SUM(factura.TotalFactura)AS TotalFactura,"
   vSQL = vSQL & " SUM(factura.TotalMontoExento) AS Montoexento,"
   vSQL = vSQL & " SUM(factura.MontoGravableAlicuota1) AS MontogravableAlicuotaGeneral,"
   vSQL = vSQL & " SUM(factura.MontoGravableAlicuota2) AS MontogravableAlicuota2,"
   vSQL = vSQL & " SUM(factura.MontoGravableAlicuota3) AS MontogravableAlicuota3,"
   vSQL = vSQL & " SUM(factura.MontoIVAAlicuota1) AS MontoIvaAlicuotaGeneral,"
   vSQL = vSQL & " SUM(factura.MontoIVAAlicuota2) AS MontoIvaAlicuota2,"
   vSQL = vSQL & " SUM(factura.MontoIVAAlicuota3) AS MontoIvaAlicuota3,"
   vSQL = vSQL & " dbo.factura.SerialMaquinaFiscal, dbo.factura.Fecha,"
   vSQL = vSQL & " (CASE factura.TipoDeVenta WHEN '3' THEN '0' WHEN '0' THEN '4' ELSE factura.TipoDeVenta END) AS TipoDeVenta, "
   vSQL = vSQL & " MAX(CASE WHEN Factura.TipoDeVenta = '3' OR Factura.TipoDeVenta = '0' THEN IVG_RetencionesVentas.NumeroComprobanteRetIva ELSE '0'END) AS NumeroComprobanteRetIva,"
   vSQL = vSQL & " MAX(CASE WHEN Factura.TipoDeVenta = '3' OR Factura.TipoDeVenta = '0' THEN IVG_RetencionesVentas.MontoIvaRetenido ELSE '0' END) AS MontoIvaRetenido,"
   vSQL = vSQL & " MAX(CASE WHEN Factura.TipoDeVenta = '3' OR Factura.TipoDeVenta = '0' THEN IVG_RetencionesVentas.FechaComprobanteRetIva ELSE '' END) as FechaComprobanteRetIva,"
   vSQL = vSQL & " dbo.factura.TipoDeDocumento"
   vSQL = vSQL & " FROM Factura "
   vSQL = vSQL & " LEFT JOIN IVG_RetencionesVentas ON"
   vSQL = vSQL & " IVG_RetencionesVentas.NumeroDocumentoOrigen = factura.Numero"
   vSQL = vSQL & " AND IVG_RetencionesVentas.CodigoCliente = factura.CodigoCliente"
   vSQL = vSQL & " AND IVG_RetencionesVentas.Fecha = factura.Fecha"
   vSQL = vSQL & " AND IVG_RetencionesVentas.TipoDeDocumentoCobrado = (CASE Factura.TipoDeDocumento WHEN '5' THEN '8' ELSE (CASE Factura.TipoDeDocumento WHEN '7' THEN '<'  END) END)"
   vSQL = vSQL & " AND IVG_RetencionesVentas.ConsecutivoCompania  = factura.ConsecutivoCompania"
   vSQL = vSQL & " Where (dbo.factura.StatusFactura = 0)"
   vSQL = vSQL & " AND(dbo.factura.TipoDeDocumento = '5' OR dbo.factura.TipoDeDocumento = '7')"
   vSQL = vSQL & " GROUP BY dbo.factura.ConsecutivoCompania, dbo.factura.SerialMaquinaFiscal, dbo.factura.Fecha,"
   vSQL = vSQL & " dbo.factura.NumeroParaResumen , dbo.factura.TipoDeVenta, dbo.factura.TipoDeDocumento"
   fSqlViewBaseimponibleAlicuotasVentasResumenDeVentas = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewBaseimponibleAlicuotasVentasResumenDeVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCxCHistoricoClientes() As String
   GetViewNameCxCHistoricoClientes = "IGV_CxCHistoricoClientes"
End Function

Public Function fSqlViewCxCHistoricoClientes() As String
   Dim SQL As String
   Dim sqlCaseMontoCobrado As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlCaseMontoCobrado = "(cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA) & _
               " OR cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion) & ")"
   SQL = "SELECT "
   SQL = SQL & "cxC.ConsecutivoCompania, "
   SQL = SQL & "cxC.CodigoCliente AS Codigo, "
   SQL = SQL & "cliente.Nombre, "
   SQL = SQL & "cxC.Moneda AS MonedaReporte, "
   SQL = SQL & "cxc.CodigoMoneda AS CodMoneda, "
   SQL = SQL & "cxC.Fecha AS FechaDocumento, "
   SQL = SQL & gUtilSQL.getIIF("Cobranza.Moneda IS NULL", "cxC.Moneda", "Cobranza.Moneda") & "AS Moneda, "
   SQL = SQL & "Cobranza.CambioABolivares, "
   SQL = SQL & "cxC.FechaVencimiento, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("CxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "TipoDeDocumento") & ", "
   SQL = SQL & "cxC.Numero AS NumeroDocumento, "
   SQL = SQL & gUtilSQL.fCast("CxC.CodigoCliente" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxC.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & "cobranza.Numero AS NumeroCobranza, "
   SQL = SQL & "cobranza.Fecha AS FechaCobranza, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloTipoReporte, "
   SQL = SQL & gUtilSQL.getIIF("cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), _
                  gUtilSQL.fSimpleSqlValue("Cobro(*)"), gUtilSQL.fSimpleSqlValue("Cobro"), True) & " AS TipoDocumentoDetalle,"
   SQL = SQL & gUtilSQL.getIIF(sqlCaseMontoCobrado, "0", "DocumentoCobrado.MontoAbonadoEnMonedaOriginal") & " AS MontoCobrado, "
   SQL = SQL & "documentoCobrado.CambioAMonedaDeCobranza AS CambioMonedaCobranza, "
   SQL = SQL & "documentoCobrado.CambioAMonedaLocal AS CambioALocalDesdeMonedaCxC, "
   SQL = SQL & "cobranza.StatusCobranza "
   SQL = SQL & "FROM cliente "
   SQL = SQL & "INNER JOIN cxC "
   SQL = SQL & "ON cliente.Codigo = cxC.CodigoCliente "
   SQL = SQL & "AND cliente.ConsecutivoCompania = cxC.ConsecutivoCompania "
   SQL = SQL & "RIGHT OUTER JOIN cobranza "
   SQL = SQL & "LEFT OUTER JOIN documentoCobrado "
   SQL = SQL & "ON cobranza.Numero = documentoCobrado.NumeroCobranza "
   SQL = SQL & "AND cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania "
   SQL = SQL & "ON cxC.Numero = documentoCobrado.NumeroDelDocumentoCobrado "
   SQL = SQL & "AND cxC.TipoCxc = documentoCobrado.TipoDeDocumentoCobrado "
   SQL = SQL & "AND cxC.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania "
   SQL = SQL & "WHERE CxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   fSqlViewCxCHistoricoClientes = SQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCxCHistoricoClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameSaldoInicialCxCHistoricoCliente() As String
   GetViewNameSaldoInicialCxCHistoricoCliente = "IGV_SaldoInicialCxCHistoricoCliente"
End Function

Public Function fSqlViewSaldoInicialCxCHistoricoCliente() As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT "
   SQL = SQL & "ConsecutivoCompania, "
   SQL = SQL & "CodigoCliente, "
   SQL = SQL & "Numero, "
   SQL = SQL & "TipoCxc, "
   SQL = SQL & "Fecha, "
   SQL = SQL & "CambioABolivares, "
   SQL = SQL & "Moneda, "
   SQL = SQL & "CodigoMoneda, "
   SQL = SQL & "MontoExento + MontoGravado + MontoIva AS SaldoInicial, "
   SQL = SQL & "MontoExento + MontoGravado + MontoIva - MontoAbonado AS SaldoInicialConAbonos "
   SQL = SQL & "FROM CxC "
   SQL = SQL & "WHERE " & " CxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   fSqlViewSaldoInicialCxCHistoricoCliente = SQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewSaldoInicialCxCHistoricoCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameInfoCxCHistoricoCliente() As String
   GetViewNameInfoCxCHistoricoCliente = "IGV_InfoCxCHistoricoCliente"
End Function

Public Function fSqlViewInfoCxCHistoricoCliente() As String
   Dim SQL As String
   Dim sqlTipoDeCxC As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlTipoDeCxC = gUtilSQL.DfSQLCaseIfForEnum("CxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, True), "")
   SQL = "SELECT "
   SQL = SQL & "cxC.ConsecutivoCompania AS ConsecutivoCompania, "
   SQL = SQL & "CodigoCliente AS Codigo, "
   SQL = SQL & "Nombre AS NombreCliente, "
   SQL = SQL & "Numero AS NumeroDocumento, "
   SQL = SQL & "Fecha AS FechaDocumento, "
   SQL = SQL & "FechaVencimiento AS FechaVencimiento, "
   SQL = SQL & "Moneda AS MonedaReporte, "
   SQL = SQL & "CodigoMoneda AS CodMoneda, "
   SQL = SQL & "CambioABolivares AS CambioABolivares, "
   SQL = SQL & sqlTipoDeCxC & " AS TipoDeDocumento, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloTipoReporte, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("Cobro") & " AS TipoDocumentoDetalle,"
   SQL = SQL & gUtilSQL.fCast("CxC.CodigoCliente" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxC.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   SQL = SQL & "MontoExento + MontoGravado + MontoIva AS MontoOriginal, "
   SQL = SQL & "MontoExento + MontoGravado + MontoIva - MontoAbonado AS SaldoActual "
   SQL = SQL & "FROM cliente "
   SQL = SQL & "INNER JOIN cxC "
   SQL = SQL & "ON cliente.Codigo = cxC.CodigoCliente "
   SQL = SQL & "AND cliente.ConsecutivoCompania = cxC.ConsecutivoCompania "
   SQL = SQL & "WHERE " & " CxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   fSqlViewInfoCxCHistoricoCliente = SQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewInfoCxCHistoricoCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen() As String
   On Error Resume Next
   fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen = gUtilSQL.CharConcat & gUtilSQL.DfChar(9) & gUtilSQL.CharConcat
   On Error GoTo 0
End Function

Public Function fSQLRoundForLibrosSaw(ByVal valValueExpression As String, ByVal valDecimals As Integer) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "ROUND ((" & valValueExpression & "), " & gUtilSQL.fSimpleSqlValueForNumeric(valDecimals) & ") "
   fSQLRoundForLibrosSaw = vSQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR:
   fSQLRoundForLibrosSaw = valValueExpression
   On Error GoTo 0
End Function

Public Function fSqlViewVentasIVAAgrupado() As String 'datos tomado del cierre z
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSqlViewVentasFacturaAgrupado
   vSQL = vSQL & " UNION " & fSqlViewVentasCxC
   vSQL = vSQL & " UNION " & fSqlViewVentasResumenAgrupado
   fSqlViewVentasIVAAgrupado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasIVAAgrupado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlViewVentasFacturaAgrupado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT IGV_FacturaBase.ConsecutivoCompania AS ConsecutivoCompania, "
   vSQL = vSQL & " IGV_FacturaBase.Numero AS Numero,"
   vSQL = vSQL & vbCrLf & "(SELECT CodTipoDocumentoSawIva FROM IGV_EnumTipoDocumentoIvaFactura WHERE DbValue = IGV_FacturaBase.TipoDeDocumento COLLATE Modern_Spanish_CS_AS) AS TipoDeDocumento, "
   vSQL = vSQL & vbCrLf & "(SELECT codTipoDeVentaIVA FROM IGV_EnumTipoDeVentaIva WHERE DbValue = IGV_FacturaBase.TipoDeVenta COLLATE Modern_Spanish_CS_AS ) AS TipoDeVenta , "
   vSQL = vSQL & " (CASE WHEN IGV_FacturaBase.EsDiferida = 'S' THEN '1' ELSE '0' END) AS StatusVenta,"
   vSQL = vSQL & " ( CASE  WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSQL = vSQL & fSqlConvierteMontoFacturaBase("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", " IGV_FacturaBase.Fecha") & " as MontoIvaRetenido, "
   vSQL = vSQL & " IGV_FacturaBase.TipoDeTransaccion AS TipoDeTransaccion,"
   vSQL = vSQL & " IGV_FacturaBase.Fecha AS Fecha,"
   vSQL = vSQL & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN MONTH(IGV_FacturaBase.Fecha)"
   vSQL = vSQL & "  ELSE MONTH(CobranzaFactura.FechaCobranza) END)"
   vSQL = vSQL & "  ELSE MONTH(IGV_FacturaBase.Fecha) END) AS MesDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN YEAR(IGV_FacturaBase.Fecha)"
   vSQL = vSQL & "  ELSE YEAR(CobranzaFactura.FechaCobranza) END)"
   vSQL = vSQL & "  ELSE YEAR(IGV_FacturaBase.Fecha) END)AS anoDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN DAY(IGV_FacturaBase.Fecha)"
   vSQL = vSQL & "  ELSE DAY(CobranzaFactura.FechaCobranza) END)"
   vSQL = vSQL & "  ELSE DAY(IGV_FacturaBase.Fecha) END)AS DiaDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL  THEN IGV_FacturaBase.Fecha"
   vSQL = vSQL & "  ELSE CobranzaFactura.FechaCobranza END)"
   vSQL = vSQL & "  ELSE IGV_FacturaBase.Fecha END)AS FechaAplicacion,"
   vSQL = vSQL & " IGV_FacturaBase.CodigoCliente AS CodigoCliente,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroHasta AS NumeroHasta,"
   vSQL = vSQL & " '' AS PorcentajePasajeAereo,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSQL = vSQL & " IGV_FacturaBase.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroControl AS NumeroDeControlDeFactura,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("IGV_FacturaBase.TotalFactura * IGV_FacturaBase.CambioABolivares", 2), True) & " as TotalFactura,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("IGV_FacturaBase.TotalMontoExento * IGV_FacturaBase.CambioABolivares", 2), True) & " as Montoexento,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True) & " as MontogravableAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota2 * IGV_FacturaBase.CambioABolivares", 2), True) & " as MontoGravableAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota3 * IGV_FacturaBase.CambioABolivares", 2), True) & " as MontoGravableAlicuota3,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontoIvaAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True) & " as  MontoIVAAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontoIvaAlicuota2 * IGV_FacturaBase.CambioABolivares", 2), True) & " as  MontoIVAAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontoIvaAlicuota3 * IGV_FacturaBase.CambioABolivares", 2), True) & " as  MontoIVAAlicuota3,"
   vSQL = vSQL & " IGV_FacturaBase.NombreOperador AS NombreOperador,"
   vSQL = vSQL & " IGV_FacturaBase.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " IGV_FacturaBase.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSQL = vSQL & " IGV_FacturaBase.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSQL = vSQL & " IGV_FacturaBase.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSQL = vSQL & " IGV_FacturaBase.Talonario AS ConsecutivoTalonario,"
   vSQL = vSQL & " '' AS NumeroResumenDiario,"
   vSQL = vSQL & " 'N' AS EsCuentaDeTercero,"
   vSQL = vSQL & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSQL = vSQL & " ,IGV_FacturaBase.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSQL = vSQL & " ,IGV_FacturaBase.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSQL = vSQL & " ," & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
         " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
          " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2"
   vSQL = vSQL & " FROM            IGV_FacturaBase LEFT JOIN" & vbCrLf
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado ON IGV_FacturaBase.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase.Numero = IGV_RetencionDocumentoCobrado.NumeroDocumentoOrigen AND" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase.TipoDeDocumento = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado " & vbCrLf
   vSQL = vSQL & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON IGV_FacturaBase.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND IGV_FacturaBase.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota LEFT JOIN" & vbCrLf
   vSQL = vSQL & " (SELECT DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen AS NumeroFacturaOrigen," & vbCrLf
   vSQL = vSQL & gUtilSQL.fMin("Cobranza.Fecha", "FechaCobranza") & vbCrLf
   vSQL = vSQL & " FROM DocumentoCobrado INNER JOIN" & vbCrLf
   vSQL = vSQL & " Cxc ON Cxc.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado AND" & vbCrLf
   vSQL = vSQL & " Cxc.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania INNER JOIN" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase ON IGV_FacturaBase.Numero = Cxc.NumeroDocumentoOrigen AND" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase.ConsecutivoCompania = Cxc.ConsecutivoCompania INNER JOIN" & vbCrLf
   vSQL = vSQL & " Cobranza ON Cobranza.Numero = DocumentoCobrado.NumeroCobranza AND" & vbCrLf
   vSQL = vSQL & " Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " WHERE IGV_FacturaBase.EsDiferida = " & gUtilSQL.fBooleanToSqlValue(False) & vbCrLf
   vSQL = vSQL & " AND IGV_FacturaBase.EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True) & vbCrLf
   vSQL = vSQL & " AND Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & vbCrLf
   vSQL = vSQL & " GROUP BY DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen) AS CobranzaFactura" & vbCrLf
   vSQL = vSQL & " ON IGV_FacturaBase.Numero = CobranzaFactura.NumeroFacturaOrigen AND" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase.ConsecutivoCompania = CobranzaFactura.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " WHERE IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSQL = vSQL & " AND IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vSQL = vSQL & " AND IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vSQL = vSQL & " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   fSqlViewVentasFacturaAgrupado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasFacturaAgrupado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasResumenAgrupado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT IGV_FacturaBase.ConsecutivoCompania AS ConsecutivoCompania, "
   vSQL = vSQL & " IGV_FacturaBase.NumeroDesde  AS Numero, "
   vSQL = vSQL & " IGV_EnumTipoDocumentoIvaFactura.CodTipoDocumentoSawIva  AS TipoDeDocumento,"
   vSQL = vSQL & " IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA AS TipoDeVenta,"
   vSQL = vSQL & " '0' AS StatusVenta,"
   vSQL = vSQL & " ( CASE  WHEN IGV_FacturaBase.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSQL = vSQL & " IGV_FacturaBase.MontoIvaRetenido  As MontoIvaRetenido, "
   vSQL = vSQL & " IGV_FacturaBase.TipoDeTransaccion AS TipoDeTransaccion,"
   vSQL = vSQL & " IGV_FacturaBase.Fecha AS Fecha,"
   vSQL = vSQL & " MONTH(IGV_FacturaBase.Fecha) AS MesDeAplicacion,"
   vSQL = vSQL & " YEAR(IGV_FacturaBase.Fecha) AS anoDeAplicacion,"
   vSQL = vSQL & " DAY(IGV_FacturaBase.Fecha) AS DiaDeAplicacion,"
   vSQL = vSQL & " IGV_FacturaBase.Fecha AS FechaAplicacion,"
   vSQL = vSQL & " IGV_FacturaBase.CodigoCliente AS CodigoCliente,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroHasta AS NumeroHasta,"
   vSQL = vSQL & " '' AS PorcentajePasajeAereo,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSQL = vSQL & " IGV_FacturaBase.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSQL = vSQL & " IGV_FacturaBase.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroControl AS NumeroDeControlDeFactura,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSQL = vSQL & " IGV_FacturaBase.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.TotalFactura", True) & " AS TotalFactura, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.TotalMontoExento", True) & " AS MontoExento, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoGravableAlicuota1", True) & " AS MontoGravableAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoGravableAlicuota2", True) & " AS MontoGravableAlicuota2, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoGravableAlicuota3", True) & " AS MontoGravableAlicuota3, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura =" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoIVAAlicuota1", True) & " AS MontoIVAAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoIVAAlicuota2", True) & " AS MontoIVAAlicuota2, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoIVAAlicuota3", True) & " AS MontoIVAAlicuota3, "
   vSQL = vSQL & " IGV_FacturaBase.NombreOperador AS NombreOperador,"
   vSQL = vSQL & " IGV_FacturaBase.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " IGV_FacturaBase.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSQL = vSQL & " IGV_FacturaBase.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSQL = vSQL & " IGV_FacturaBase.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSQL = vSQL & " IGV_FacturaBase.SerialMaquinaFiscal AS ConsecutivoTalonario,"
   vSQL = vSQL & " IGV_FacturaBase.NumeroResumenDiario AS NumeroResumenDiario,"
   vSQL = vSQL & " 'N' AS EsCuentaDeTercero,"
   vSQL = vSQL & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSQL = vSQL & " ,IGV_FacturaBase.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSQL = vSQL & " ,IGV_FacturaBase.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial,"
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         "IGV_FacturaBase.MontoGravableAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
         "IGV_FacturaBase.MontoGravableAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         "IGV_FacturaBase.MontoIVAAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1, "
   vSQL = vSQL & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
         "IGV_FacturaBase.MontoIVAAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2"
   vSQL = vSQL & " FROM IGV_FacturaBase LEFT JOIN"
   vSQL = vSQL & " Cxc ON CxC.ConsecutivoCompania = IGV_FacturaBase.ConsecutivoCompania AND CxC.NumeroDocumentoOrigen = IGV_FacturaBase.Numero AND"
   vSQL = vSQL & " CxC.CodigoCliente = IGV_FacturaBase.CodigoCliente"
   vSQL = vSQL & " INNER JOIN IGV_EnumTipoDocumentoIvaFactura"
   vSQL = vSQL & " ON IGV_EnumTipoDocumentoIvaFactura.DbValue = IGV_FacturaBase.TipoDeDocumentoFactura COLLATE Modern_Spanish_CS_AS"
   vSQL = vSQL & " INNER JOIN IGV_EnumTipoDeVentaIva"
   vSQL = vSQL & " ON IGV_EnumTipoDeVentaIva.DbValue COLLATE Modern_Spanish_CS_AS = IGV_FacturaBase.TipoDeVenta"
   vSQL = vSQL & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial1 ON IGV_FacturaBase.Fecha BETWEEN AlicuotaImpuestoEspecial1.FechaDesde AND AlicuotaImpuestoEspecial1.FechaHasta AND" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND IGV_FacturaBase.PorcentajeAlicuota2 = AlicuotaImpuestoEspecial1.Alicuota" & vbCrLf
   vSQL = vSQL & " AND AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida)
   vSQL = vSQL & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial2 ON IGV_FacturaBase.Fecha BETWEEN AlicuotaImpuestoEspecial2.FechaDesde AND AlicuotaImpuestoEspecial2.FechaHasta AND" & vbCrLf
   vSQL = vSQL & " IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND IGV_FacturaBase.PorcentajeAlicuota3 = AlicuotaImpuestoEspecial2.Alicuota" & vbCrLf
   vSQL = vSQL & " AND AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida)
   vSQL = vSQL & " WHERE IGV_FacturaBase.TipoDeDocumentoFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   fSqlViewVentasResumenAgrupado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasResumenAgrupado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlViewVentasIVADetallado() As String ' detalle de todas las facturas
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSqlViewVentasFacturaDetallado
   fSqlViewVentasIVADetallado = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasIVADetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasFacturaDetallado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = fSqlVentasDetalladoSeccionUnion(False)
   vSQL = vSQL & " UNION "
   vSQL = vSQL & fSqlVentasDetalladoSeccionUnion(True)
   fSqlViewVentasFacturaDetallado = vSQL
   Debug.Print vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasFacturaDetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewFacturaBase() As String
   Dim vSQL As String
   Dim vCase1 As String
   Dim vCase2 As String
   Dim vCase3 As String
   On Error GoTo h_ERROR
   vCase1 = gUtilSQL.getIIF("factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("2"), gUtilSQL.fSimpleSqlValue("4"), gUtilSQL.fSimpleSqlValue(" "), True)
   vCase2 = gUtilSQL.getIIF("factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("1"), gUtilSQL.fSimpleSqlValue("3"), vCase1, True)
   vCase3 = gUtilSQL.getIIF("factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("0"), vCase2, True)
   vSQL = vSQL & "SELECT factura.ConsecutivoCompania AS ConsecutivoCompania,  factura.Numero AS Numero," & vbCrLf
   vSQL = vSQL & vCase3 & " AS TipoDeDocumento, factura.TipoDeVenta AS TipoDeVenta, factura.TipoDeDocumento AS TipoDeDocumentoFactura," & vbCrLf
   vSQL = vSQL & "factura.Fecha, factura.CodigoMoneda AS CodigoMoneda, factura.CambioABolivares AS CambioABolivares," & vbCrLf
   vSQL = vSQL & "factura.TipoDeTransaccion AS TipoDeTransaccion, factura.CodigoCliente AS CodigoCliente, factura.NumeroHasta AS NumeroHasta," & vbCrLf
   vSQL = vSQL & "factura.NumeroDesde AS NumeroDesde, factura.SeRetuvoIVA AS SeRetuvoIVA, factura.MontoIvaRetenido, factura.FechaComprobanteRetIVA," & vbCrLf
   vSQL = vSQL & "factura.NumeroComprobanteRetIVA, factura.FechaAplicacionRetIVA,factura.TotalFactura, factura.TotalMontoExento, factura.MontoGravableAlicuota1," & vbCrLf
   vSQL = vSQL & "factura.MontoGravableAlicuota2, factura.MontoGravableAlicuota3, factura.MontoIVAAlicuota1, factura.MontoIVAAlicuota2," & vbCrLf
   vSQL = vSQL & "factura.MontoIVAAlicuota3, factura.SerialMaquinaFiscal," & vbCrLf
   vSQL = vSQL & "factura.NumeroControlHasta AS NumeroControlHasta, factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora, factura.NumeroResumenDiario AS NumeroResumenDiario," & vbCrLf
   vSQL = vSQL & "factura.NumeroPlanillaExportacion AS NumeroPlanillaExportacion, factura.NumeroFacturaAfectada AS NumeroFacturaAfectada," & vbCrLf
   vSQL = vSQL & "factura.NumeroControl AS NumeroControl, factura.StatusFactura AS StatusFactura, factura.PorcentajeDescuento," & vbCrLf
   vSQL = vSQL & "factura.NombreOperador AS NombreOperador, factura.FechaUltimaModificacion AS FechaUltimaModificacion," & vbCrLf
   vSQL = vSQL & "factura.PorcentajeAlicuota1 AS PorcentajeAlicuota1, factura.PorcentajeAlicuota2 AS PorcentajeAlicuota2," & vbCrLf
   vSQL = vSQL & "factura.PorcentajeAlicuota3 AS PorcentajeAlicuota3, " & vbCrLf
   vSQL = vSQL & gUtilSQL.getIIF("Factura.Talonario = " & gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("1"), gUtilSQL.fSimpleSqlValue("2")) & " AS Talonario," & vbCrLf
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("N") & " AS EsCuentaDeTercero, " & gUtilSQL.fSimpleSqlValue("N") & " AS MontosConvertido, " & vbCrLf
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS MontoIvaPercibido, " & vbCrLf
   vSQL = vSQL & "factura.EsDiferida, factura.EsOriginalmenteDiferida, factura.AplicaDecretoIvaEspecial" & vbCrLf
   vSQL = vSQL & " FROM factura" & vbCrLf
   fSqlViewFacturaBase = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewFacturaBase", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameFacturaBase() As String
   GetViewNameFacturaBase = "IGV_FacturaBase"
End Function

Private Function fSqlConvierteMontoFacturaBase(ByVal valValor As String, ByVal valAnoAplicacion As String, ByVal valFecha As String) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSQL = vSQL & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" IGV_FacturaBase.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * IGV_FacturaBase.CambioABolivares * 0.001 ", False) & ")", _
                           "(" & gUtilSQL.getIIF("ABS(" & valValor & ") >= IGV_FacturaBase.MontoIvaRetenido ", valValor, valValor & " * IGV_FacturaBase.CambioABolivares ", False) & ")")
                           'valValor & " * IGV_FacturaBase.CambioABolivares")
   fSqlConvierteMontoFacturaBase = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlConvierteMontoFacturaBase", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlVentasDetalladoSeccionUnion(ByVal valEsTipoResumenDiario As Boolean) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT factura.ConsecutivoCompania AS ConsecutivoCompania, "
   If valEsTipoResumenDiario Then
      vSQL = vSQL & " Factura.NumeroDesde  AS Numero ,"
   Else
      vSQL = vSQL & " (CASE WHEN Factura.TipoDeDocumento ='5' or Factura.TipoDeDocumento ='7' THEN factura.NumeroComprobanteFiscal ELSE factura.Numero END )  AS Numero ,"
   End If
   vSQL = vSQL & "(SELECT codTipoDeVentaIVA FROM IGV_EnumTipoDeVentaIva WHERE DbValue = factura.TipoDeVenta COLLATE Modern_Spanish_CS_AS ) AS TipoDeVenta , "
   vSQL = vSQL & "(SELECT CodTipoDocumentoSawIva FROM IGV_EnumTipoDocumentoIvaFactura WHERE DbValue = factura.TipoDeDocumento COLLATE Modern_Spanish_CS_AS) AS TipoDeDocumento, "
   vSQL = vSQL & " (CASE WHEN Factura.EsDiferida = 'S' THEN '1' ELSE '0' END) AS StatusVenta,"
   vSQL = vSQL & " ( CASE  WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSQL = vSQL & fSqlConvierteMonto("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", " Factura.Fecha") & " as MontoIvaRetenido, "
   vSQL = vSQL & " factura.TipoDeTransaccion AS TipoDeTransaccion,"
   vSQL = vSQL & " factura.Fecha AS Fecha,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL THEN MONTH(Factura.Fecha)"
   vSQL = vSQL & "  ELSE MONTH(CobranzaFactura.FechaCobranza) END)"
   vSQL = vSQL & "  ELSE MONTH(Factura.Fecha) END) AS MesDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN YEAR(Factura.Fecha)"
   vSQL = vSQL & "  ELSE YEAR(CobranzaFactura.FechaCobranza) END)"
   vSQL = vSQL & "  ELSE YEAR(Factura.Fecha) END)AS anoDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN DAY(Factura.Fecha)"
   vSQL = vSQL & "  ELSE DAY(CobranzaFactura.FechaCobranza) END)"
   vSQL = vSQL & "  ELSE DAY(Factura.Fecha) END)AS DiaDeAplicacion,"
   vSQL = vSQL & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSQL = vSQL & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL  THEN Factura.Fecha"
   vSQL = vSQL & "  ELSE CobranzaFactura.FechaCobranza END)"
   vSQL = vSQL & "  ELSE Factura.Fecha END)AS FechaAplicacion,"
   vSQL = vSQL & "  CobranzaFactura.NumeroFacturaOrigen As CobranzaNumeroFacturaOrigen,"
   vSQL = vSQL & "  CobranzaFactura.FechaCobranza AS FechaCobranza,"
   vSQL = vSQL & " factura.CodigoCliente AS CodigoCliente,"
   vSQL = vSQL & " Factura.NumeroHasta AS NumeroHasta,"
   vSQL = vSQL & " '' AS PorcentajePasajeAereo,"
   vSQL = vSQL & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSQL = vSQL & " factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSQL = vSQL & " factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSQL = vSQL & " factura.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSQL = vSQL & " factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSQL = vSQL & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalFactura * factura.CambioABolivares", 2), True) & " as TotalFactura,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalMontoExento * factura.CambioABolivares", 2), True) & " as Montoexento,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
           fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True) & " as MontogravableAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota3,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True) & " as MontoIVAAlicuotaGeneral,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota3,"
   vSQL = vSQL & " factura.NombreOperador AS NombreOperador,"
   vSQL = vSQL & " factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSQL = vSQL & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSQL = vSQL & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSQL = vSQL & " (CASE WHEN Factura.TipoDeDocumento ='5' or Factura.TipoDeDocumento ='7' THEN SerialMaquinaFiscal ELSE (CASE Factura.Talonario WHEN '0' THEN '1' ELSE '2'  END ) END )  AS ConsecutivoTalonario,"
   vSQL = vSQL & " '' AS NumeroResumenDiario,"
   vSQL = vSQL & " 'N' AS EsCuentaDeTercero,"
   vSQL = vSQL & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido,"
   vSQL = vSQL & " factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida,"
   vSQL = vSQL & " factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSQL = vSQL & " ," & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
         " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSQL = vSQL & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2"
   vSQL = vSQL & " FROM factura LEFT JOIN"
   vSQL = vSQL & " Cxc ON CxC.ConsecutivoCompania = factura.ConsecutivoCompania AND "
   vSQL = vSQL & " CxC.NumeroDocumentoOrigen = factura.Numero AND "
   vSQL = vSQL & " CxC.CodigoCliente = factura.CodigoCliente"
   vSQL = vSQL & " AND Cxc.TipoCxc = (CASE Factura.TipoDeDocumento WHEN '0' THEN '0' ELSE"
   vSQL = vSQL & " (CASE Factura.TipoDeDocumento WHEN '1' THEN '3' ELSE"
   vSQL = vSQL & " (CASE Factura.TipoDeDocumento WHEN '2' THEN '4' ELSE"
   vSQL = vSQL & " (CASE Factura.TipoDeDocumento WHEN '5' THEN '8' ELSE"
   vSQL = vSQL & " (CASE Factura.TipoDeDocumento WHEN '7' THEN '<' END)END) END) END) END)"
   vSQL = vSQL & " LEFT JOIN IGV_RetencionDocumentoCobrado ON"
   vSQL = vSQL & " CxC.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND"
   vSQL = vSQL & " Cxc.Numero = IGV_RetencionDocumentoCobrado.NumeroDelDocumentoCobrado AND"
   vSQL = vSQL & " Cxc.TipoCxc = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado"
   vSQL = vSQL & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON Factura.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSQL = vSQL & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota" & vbCrLf
   vSQL = vSQL & " LEFT JOIN (SELECT DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen AS NumeroFacturaOrigen," & vbCrLf
   vSQL = vSQL & " " & gUtilSQL.fMin("Cobranza.Fecha", "FechaCobranza") & vbCrLf
   vSQL = vSQL & " FROM DocumentoCobrado" & vbCrLf
   vSQL = vSQL & " INNER JOIN Cxc ON Cxc.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado" & vbCrLf
   vSQL = vSQL & " AND Cxc.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   vSQL = vSQL & " INNER JOIN Factura ON Factura.Numero = Cxc.NumeroDocumentoOrigen" & vbCrLf
   vSQL = vSQL & " AND Factura.ConsecutivoCompania = Cxc.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " INNER JOIN Cobranza ON Cobranza.Numero = DocumentoCobrado.NumeroCobranza" & vbCrLf
   vSQL = vSQL & " AND Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania" & vbCrLf
   vSQL = vSQL & " WHERE Factura.EsDiferida = " & gUtilSQL.fBooleanToSqlValue(False) & vbCrLf
   vSQL = vSQL & " AND Factura.EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True) & vbCrLf
   vSQL = vSQL & " AND Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & vbCrLf
   vSQL = vSQL & " GROUP BY DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen) AS CobranzaFactura" & vbCrLf
   vSQL = vSQL & " ON Factura.Numero = CobranzaFactura.NumeroFacturaOrigen" & vbCrLf
   vSQL = vSQL & " AND Factura.ConsecutivoCompania = CobranzaFactura.ConsecutivoCompania" & vbCrLf
   If valEsTipoResumenDiario Then
      vSQL = vSQL & " WHERE (Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      vSQL = vSQL & " AND Factura.RealizoCierreZ = " & gUtilSQL.fBooleanToSqlValue(False) & ")"
   Else
      vSQL = vSQL & " WHERE (Factura.StatusFactura = '" & gConvert.enumerativoAChar(enum_StatusFactura.eSF_EMITIDA) & "'"
      vSQL = vSQL & " OR Factura.StatusFactura = '" & gConvert.enumerativoAChar(enum_StatusFactura.eSF_ANULADA) & "')"
   End If
h_EXIT:     On Error GoTo 0
   fSqlVentasDetalladoSeccionUnion = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlVentasDetalladoSeccionUnion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
