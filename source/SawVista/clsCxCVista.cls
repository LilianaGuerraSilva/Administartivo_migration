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
   Dim vSql As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = "SELECT "
   vSql = vSql & "ConsecutivoCompania, "
   vSql = vSql & "Numero, "
   vSql = vSql & gUtilSQL.getIIF("MontoExento IS NULL", 0, "MontoExento", True) & " AS ventasExentas, "
   vSql = vSql & gUtilSQL.getIIF("MontoExento IS NULL", 0, "MontoExento", True) & " AS VentasInternasNoGravadas, "
   vSql = vSql & "0 AS VentasDeExportacion, "
   vSql = vSql & gUtilSQL.getIIF("MontoGravado IS NULL", 0, "MontoGravado", True) & " AS VentasIntBaseAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("MontoIva IS NULL", 0, "MontoIva", True) & " AS VentasIntIvaAlicuotaGeneral, "
   vSql = vSql & "0 AS VentasIntBaseAlicuotaGeneralMasAdicional, "
   vSql = vSql & "0 AS VentasIntIvaAlicuotaGeneralMasAdicional, "
   vSql = vSql & "0 AS VentasIntBaseAlicuotaReducida, "
   vSql = vSql & "0 AS VentasIntIvaAlicuotaReducida, "
   vSql = vSql & "0 AS MontoEditableAjusteDebito, "
   vSql = vSql & "0 AS RetencionesDelPeriodo, "
   vSql = vSql & "0 AS DescVentasGeneral, "
   vSql = vSql & "0 AS DescIvaAlicuotaGeneralVentasInt, "
   vSql = vSql & "CambioAbolivares, "
   vSql = vSql & "Fecha, "
   vSql = vSql & "Origen, "
   vSql = vSql & "Status, "
   vSql = vSql & "Moneda "
   vSql = vSql & "FROM "
   vSql = vSql & "cxC "
   vSql = vSql & "WHERE "
   vSql = vSql & "Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   vSql = vSql & "AND "
   vSql = vSql & "Origen = " & gUtilSQL.fSimpleSqlValue(enum_OrigenFacturacionOManual.eOF_MANUAL)
   vSql = vSql & " AND NoAplicaParaLibroDeVentas = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlViewVentasGeneradasCxC = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasGeneradasCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlViewDocumentoCobrado() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = vSql & " SELECT SUM(DocumentoCobrado.MontoAbonado / CambioAMonedaDeCobranza ) as SumaMoneda,  "
   vSql = vSql & " Cobranza.ConsecutivoCompania, Cobranza.Numero as NumeroCobranza, Cobranza.CodigoCliente, DocumentoCobrado.CodigoMonedaDeCxC "
   vSql = vSql & " FROM DocumentoCobrado INNER JOIN COBRANZA ON "
   vSql = vSql & " (cobranza.Numero = documentoCobrado.NumeroCobranza) "
   vSql = vSql & " AND (cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania) "
   vSql = vSql & " WHERE StatusCobranza <> '1' "
   vSql = vSql & " GROUP BY DocumentoCobrado.CodigoMonedaDeCxC, Cobranza.ConsecutivoCompania, Cobranza.Numero, Cobranza.CodigoCliente"
   fSqlViewDocumentoCobrado = vSql
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
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = fSqlViewVentasFactura
   vSql = vSql & vbCrLf & " UNION " & vbCrLf & fSqlViewVentasCxC
   vSql = vSql & vbCrLf & " UNION " & vbCrLf & fSqlViewVentasResumen
   vSql = vSql & vbCrLf & " UNION " & vbCrLf & fSqlViewVentasResumenManual
   fSqlViewVentasIVA = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasFactura() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT factura.ConsecutivoCompania AS ConsecutivoCompania, "
   vSql = vSql & " factura.Numero AS Numero,"
   vSql = vSql & "(SELECT CodTipoDocumentoSawIva FROM IGV_EnumTipoDocumentoIvaFactura WHERE DbValue = factura.TipoDeDocumento COLLATE Modern_Spanish_CS_AS) AS TipoDeDocumento, "
   vSql = vSql & "(SELECT codTipoDeVentaIVA FROM IGV_EnumTipoDeVentaIva WHERE DbValue = factura.TipoDeVenta COLLATE Modern_Spanish_CS_AS ) AS TipoDeVenta , "
   vSql = vSql & " (CASE WHEN Factura.EsDiferida = 'S' THEN '1' ELSE '0' END) AS StatusVenta,"
   vSql = vSql & " (CASE WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END) AS StatusRetencion, "
   vSql = vSql & fSqlConvierteMonto("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", " Factura.Fecha") & " as MontoIvaRetenido, "
   vSql = vSql & " factura.TipoDeTransaccion AS TipoDeTransaccion,"
   vSql = vSql & " factura.Fecha AS Fecha,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN MONTH(Factura.FechaAplicacionRetIVA) ELSE MONTH(Factura.Fecha) END) AS MesDeAplicacion,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN YEAR(Factura.FechaAplicacionRetIVA) ELSE YEAR(Factura.Fecha) END) AS anoDeAplicacion,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN DAY(Factura.FechaAplicacionRetIVA) ELSE DAY(Factura.Fecha) END) AS DiaDeAplicacion,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN Factura.FechaAplicacionRetIVA ELSE Factura.Fecha END) AS FechaAplicacion,"
   vSql = vSql & " factura.CodigoCliente AS CodigoCliente,"
   vSql = vSql & " Factura.NumeroHasta AS NumeroHasta,"
   vSql = vSql & " '' AS PorcentajePasajeAereo,"
   vSql = vSql & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSql = vSql & " factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSql = vSql & " factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSql = vSql & " factura.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSql = vSql & " factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalFactura * factura.CambioABolivares", 2), True) & " as TotalFactura,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalMontoExento * factura.CambioABolivares", 2), True) & " as Montoexento,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True) & " as  MontogravableAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota2 * factura.CambioABolivares", 2), True) & " as  MontoGravableAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota3 * factura.CambioABolivares", 2), True) & " as  MontoGravableAlicuota3,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True) & " as MontoIVAAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota3 * factura.CambioABolivares", 2), True) & " as  MontoIVAAlicuota3,"
   vSql = vSql & " factura.NombreOperador AS NombreOperador,"
   vSql = vSql & " factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSql = vSql & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSql = vSql & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSql = vSql & " (CASE Factura.Talonario WHEN '0' THEN '1' ELSE '2'  END ) AS ConsecutivoTalonario,"
   vSql = vSql & " '' AS  NumeroResumenDiario,"
   vSql = vSql & " 'N' AS EsCuentaDeTercero,"
   vSql = vSql & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSql = vSql & " ,factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSql = vSql & " ,factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSql = vSql & " ," & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
         " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF), gUtilSQL.fSimpleSqlValue("igtf"), gUtilSQL.fSimpleSqlValue(""), True) & " AS GeneradoPorIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, fSQLRoundForLibrosSaw("factura.BaseImponibleIGTF * factura.CambioABolivares", 2), True) & " AS BaseImponibleIGTF, "
   vSql = vSql & "factura.AlicuotaIGTF AS PorcentajeIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "factura.IGTFML", True) & " AS AlicuotaIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "(" & fSQLRoundForLibrosSaw("factura.TotalFactura  * factura.CambioABolivares", 2) & " + factura.IGTFML)", True) & " AS VentasIncluyendoIvaeIGTF "
   vSql = vSql & vbCrLf & " FROM factura LEFT JOIN" & vbCrLf
   vSql = vSql & vbCrLf & " IGV_RetencionDocumentoCobrado ON factura.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND" & vbCrLf
   vSql = vSql & vbCrLf & " factura.Numero = IGV_RetencionDocumentoCobrado.NumeroDocumentoOrigen AND" & vbCrLf
   vSql = vSql & vbCrLf & " (CASE Factura.TipoDeDocumento WHEN '0' THEN '0' WHEN '1' THEN '3' WHEN '2' THEN '4' END) = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado " & vbCrLf
   vSql = vSql & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON Factura.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSql = vSql & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota" & vbCrLf
   vSql = vSql & vbCrLf & " WHERE factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL)
   vSql = vSql & vbCrLf & " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   fSqlViewVentasFactura = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasCxC() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT  CxC.ConsecutivoCompania AS ConsecutivoCompania,  CxC.Numero AS Numero, "
   vSql = vSql & " IGV_EnumTipoDocumentoIva.CodTipoDocumentoFacturaSawIva  AS TipoDeDocumento, "
   vSql = vSql & " '2' AS TipoDeVenta, "
   vSql = vSql & " '0' AS StatusVenta, "
   vSql = vSql & " ( CASE  WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSql = vSql & fSqlConvierteMontoCXC("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", "CxC.Fecha") & " as MontoIvaRetenido, "
   vSql = vSql & " cxc.TipoCxc AS TipoDeTransaccion, Cxc.Fecha AS Fecha, "
   vSql = vSql & " MONTH(CxC.Fecha) AS mesDeAplicacion, "
   vSql = vSql & " YEAR(CxC.Fecha) AS anoDeAplicacion, "
   vSql = vSql & " DAY(CxC.Fecha) AS DiaDeAplicacion, "
   vSql = vSql & " CxC.Fecha AS FechaAplicacion, "
   vSql = vSql & " CxC.CodigoCliente AS CodigoCliente, '' AS NumeroHasta, '' AS PorcentajePasajeAereo, "
   vSql = vSql & " Cxc.NumeroControl AS NumeroDeControlDeFacturaHasta, '' AS CodigoMaquinaRegistradora, '' AS NumeroDePlanillaExpotacion, "
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion, '' AS NumeroFacturaAfectada, Cxc.NumeroControl AS NumeroDeControlDeFactura, "
   vSql = vSql & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion, "
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion, "
   vSql = vSql & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw("(CxC.montoIva + CxC.montoGravado + CxC.montoExento) * CxC.CambioAbolivares ", 2)) & " AS TotalCxC, "
   vSql = vSql & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw(" CxC.montoExento * CxC.CambioAbolivares ", 2)) & " AS MontoExento, "
   vSql = vSql & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw(" CxC.montoGravado * CxC.CambioAbolivares ", 2)) & " AS MontoGravableAlicuotaGeneral, "
   vSql = vSql & " 0 AS MontoGravableAlicuota2, 0 AS MontoGravableAlicuota3,"
   vSql = vSql & gUtilSQL.getIIF("Cxc.Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), gUtilSQL.fSimpleSqlValueForNumeric(0), fSQLRoundForLibrosSaw(" CxC.montoIva * CxC.CambioAbolivares ", 2)) & " AS MontoIVAAlicuotaGeneral, "
   vSql = vSql & " 0 AS MontoIVAAlicuota2, 0 AS MontoIVAAlicuota3,  CxC.NombreOperador AS NombreOperador,"
   vSql = vSql & " CxC.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " (SELECT MontoAlicuotaGeneral FROM AlicuotaIva WHERE  FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA"
   vSql = vSql & " From AlicuotaIva  WHERE FechaDeInicioDeVigencia < Cxc.Fecha)) As PorcentajeIvaAlicuotaGeneral,"
   vSql = vSql & " (SELECT MontoAlicuota2 FROM AlicuotaIva WHERE FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA"
   vSql = vSql & " From AlicuotaIva  WHERE FechaDeInicioDeVigencia < Cxc.Fecha)) As PorcentajeIvaAlicuota2,"
   vSql = vSql & " (SELECT MontoAlicuota3 FROM AlicuotaIva WHERE  FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA"
   vSql = vSql & " From AlicuotaIva  WHERE FechaDeInicioDeVigencia < Cxc.Fecha)) As PorcentajeIvaAlicuota3,"
   vSql = vSql & " '' AS ConsecutivoTalonario, '' AS NumeroResumenDiario,"
   vSql = vSql & " 'N' AS EsCuentaDeTercero, 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSql = vSql & " ,'N' AS EsOriginalmenteDiferida"
   vSql = vSql & " ,'N' AS AplicaDecretoIvaEspecial"
   vSql = vSql & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoGravableAlicuotaEspecial1"
   vSql = vSql & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoGravableAlicuotaEspecial2"
   vSql = vSql & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoIVAAlicuotaEspecial1"
   vSql = vSql & " ," & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoIVAAlicuotaEspecial2"
   vSql = vSql & " ," & gUtilSQL.fSimpleSqlValue("") & " AS GeneradoPorIGTF"
   vSql = vSql & " ,0 AS BaseImponibleIGTF "
   vSql = vSql & " ,0 AS PorcentajeIGTF "
   vSql = vSql & " ,0 AS AlicuotaIGTF "
   vSql = vSql & " ,0 AS VentasIncluyendoIvaeIGTF "
   vSql = vSql & vbCrLf & " FROM CxC LEFT JOIN IGV_RetencionDocumentoCobrado ON"
   vSql = vSql & vbCrLf & " CxC.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND"
   vSql = vSql & vbCrLf & " Cxc.Numero = IGV_RetencionDocumentoCobrado.NumeroDelDocumentoCobrado AND"
   vSql = vSql & vbCrLf & " Cxc.TipoCxc = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado"
   vSql = vSql & vbCrLf & " INNER JOIN IGV_EnumTipoDocumentoIva ON"
   vSql = vSql & vbCrLf & " IGV_EnumTipoDocumentoIva.DbValue = Cxc.TipoCxc COLLATE Modern_Spanish_CS_AS"
   vSql = vSql & vbCrLf & " WHERE CxC.NoAplicaParaLibroDeVentas = " & gUtilSQL.fBooleanToSqlValue(False)
   vSql = vSql & vbCrLf & " AND CxC.Origen = " & gUtilSQL.fSimpleSqlValue(enum_OrigenFacturacionOManual.eOF_MANUAL)
   fSqlViewVentasCxC = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasCxC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasResumen() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT Factura.ConsecutivoCompania AS ConsecutivoCompania,"
   vSql = vSql & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.PrimerNumeroComprobante as Numero,"
   vSql = vSql & gUtilSQL.getIIF("IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVentaIva.eTD_TV_VENTASNOCONTRIBUYENTES), gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("4"), True) & " AS TipoDeDocumento, "
   vSql = vSql & " IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA AS TipoDeVenta, "
   vSql = vSql & gUtilSQL.fSimpleSqlValue("0") & " AS StatusVenta, "
   vSql = vSql & " (CASE  WHEN IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaRetenido <> 0 OR IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaRetenido IS NULL  THEN '1' ELSE '0' END ) AS StatusRetencion,"
   vSql = vSql & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaRetenido  as MontoIvaRetenido,"
   vSql = vSql & " factura.TipoDeTransaccion AS TipoDeTransaccion, factura.Fecha AS Fecha,"
   vSql = vSql & " MONTH(Factura.Fecha) AS MesDeAplicacion, YEAR(Factura.Fecha) AS anoDeAplicacion, DAY(Factura.Fecha) AS DiaDeAplicacion,Factura.Fecha as FechaAplicacion, "
   vSql = vSql & gUtilSQL.getIIF("IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeVentaIva.eTD_TV_VENTASNOCONTRIBUYENTES), gUtilSQL.fSimpleSqlValue("RD_Cliente"), "factura.CodigoCliente", True) & " AS TipoDeDocumento, "
   vSql = vSql & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.UltimoNumeroComprobante AS NumeroHasta, '' AS PorcentajePasajeAereo,"
   vSql = vSql & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta, factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSql = vSql & " factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSql = vSql & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSql = vSql & " factura.NumeroFacturaAfectada AS NumeroFacturaAfectada, factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSql = vSql & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSql = vSql & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.FechaComprobanteRetIva AS FechaAplicacionRetencion,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.TotalFactura * factura.CambioABolivares", 2), True) & " as TotalFactura,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.Montoexento * factura.CambioABolivares", 2), True) & " as Montoexento,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
            " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
            " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuotaGeneral * Factura.CambioABolivares", 2), True) & " as MontogravableAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
         fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota3,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
            " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
            " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuotaGeneral * Factura.CambioABolivares", 2), True) & " as  MontoIVAAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
         fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
         fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota3,"
   vSql = vSql & " factura.NombreOperador AS NombreOperador, factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSql = vSql & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSql = vSql & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSql = vSql & " (CASE WHEN Factura.SerialMaquinaFiscal <> '' THEN Factura.SerialMaquinaFiscal ELSE '0'  END ) AS ConsecutivoTalonario,"
   vSql = vSql & " factura.NumeroResumenDiario AS NumeroResumenDiario,"
   vSql = vSql & " 'N' AS EsCuentaDeTercero, 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSql = vSql & " ,factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSql = vSql & " ,factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSql = vSql & " ," & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
           " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
            " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontogravableAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
            " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
            " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
            gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
            fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.MontoIvaAlicuotaGeneral * Factura.CambioABolivares", 2), True), _
            gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF), gUtilSQL.fSimpleSqlValue("igtf"), gUtilSQL.fSimpleSqlValue(""), True) & " AS GeneradoPorIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.BaseImponibleIGTF * factura.CambioABolivares", 2), True) & "AS BaseImponibleIGTF, "
   vSql = vSql & "factura.AlicuotaIGTF AS PorcentajeIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "IGV_BaseimponibleAlicuotasVentasResumenDeVentas.IGTFML", True) & " AS AlicuotaIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "(" & fSQLRoundForLibrosSaw("IGV_BaseimponibleAlicuotasVentasResumenDeVentas.TotalFactura * factura.CambioABolivares", 2) & " + IGV_BaseimponibleAlicuotasVentasResumenDeVentas.IGTFML)", True) & " AS VentasIncluyendoIvaeIGTF "
   vSql = vSql & vbCrLf & " FROM factura"
   vSql = vSql & vbCrLf & " INNER JOIN IGV_BaseimponibleAlicuotasVentasResumenDeVentas ON"
   vSql = vSql & vbCrLf & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.ConsecutivoCompania   = Factura.ConsecutivoCompania AND"
   vSql = vSql & vbCrLf & " IGV_BaseimponibleAlicuotasVentasResumenDeVentas.PrimerNumeroFactura = factura.Numero "
   vSql = vSql & vbCrLf & " INNER JOIN"
   vSql = vSql & vbCrLf & " dbo.IGV_EnumTipoDeVentaIva ON"
   vSql = vSql & vbCrLf & " dbo.IGV_BaseimponibleAlicuotasVentasResumenDeVentas.TipoDeVenta COLLATE Modern_Spanish_CS_AS = dbo.IGV_EnumTipoDeVentaIva.DbValue"
   vSql = vSql & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON Factura.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSql = vSql & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota " & vbCrLf
   vSql = vSql & vbCrLf & " WHERE factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vSql = vSql & vbCrLf & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   vSql = vSql & vbCrLf & " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   vSql = vSql & vbCrLf & " AND IGV_BaseimponibleAlicuotasVentasResumenDeVentas.Fecha = factura.Fecha "
   fSqlViewVentasResumen = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasResumen", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasResumenManual() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT factura.ConsecutivoCompania AS ConsecutivoCompania, "
   vSql = vSql & " factura.NumeroDesde  AS Numero, "
   vSql = vSql & " IGV_EnumTipoDocumentoIvaFactura.CodTipoDocumentoSawIva  AS TipoDeDocumento,"
   vSql = vSql & " IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA AS TipoDeVenta,"
   vSql = vSql & gUtilSQL.fSimpleSqlValue(0) & " AS StatusVenta, "
   vSql = vSql & gUtilSQL.getIIF("Factura.SeRetuvoIva = " & gUtilSQL.fBooleanToSqlValue(False), gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("1"), True) & " AS StatusRetencion, "
   vSql = vSql & " Factura.MontoIvaRetenido  As MontoIvaRetenido, "
   vSql = vSql & " factura.TipoDeTransaccion AS TipoDeTransaccion,"
   vSql = vSql & " factura.Fecha AS Fecha,"
   vSql = vSql & " MONTH(Factura.Fecha) AS MesDeAplicacion,"
   vSql = vSql & " YEAR(Factura.Fecha) AS anoDeAplicacion,"
   vSql = vSql & " DAY(Factura.Fecha) AS DiaDeAplicacion,"
   vSql = vSql & " Factura.Fecha AS FechaAplicacion,"
   vSql = vSql & " factura.CodigoCliente AS CodigoCliente,"
   vSql = vSql & " Factura.NumeroHasta AS NumeroHasta,"
   vSql = vSql & " '' AS PorcentajePasajeAereo,"
   vSql = vSql & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSql = vSql & " Factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSql = vSql & " Factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSql = vSql & " Factura.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSql = vSql & " Factura.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSql = vSql & " Factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSql = vSql & " Factura.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSql = vSql & " Factura.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.TotalFactura ", True) & " AS TotalFactura,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.TotalMontoExento", True) & " AS TotalMontoExento,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.MontoGravableAlicuota1", True) & " AS MontoGravableAlicuota1,"
   vSql = vSql & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoGravableAlicuota2", True) & " AS MontoGravableAlicuota2, "
   vSql = vSql & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoGravableAlicuota3", True) & " AS MontoGravableAlicuota3, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura =" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "factura.MontoIVAAlicuota1 ", True) & " AS MontoIVAAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoIVAAlicuota2", True) & " AS MontoIVAAlicuota2, "
   vSql = vSql & gUtilSQL.getIIF("(Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & ") OR ((Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & ") AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))" _
               , gUtilSQL.fSimpleSqlValueForNumeric(0), "Factura.MontoIVAAlicuota3", True) & " AS MontoIVAAlicuota3, "
   vSql = vSql & " factura.NombreOperador AS NombreOperador,"
   vSql = vSql & " factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSql = vSql & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSql = vSql & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSql = vSql & " factura.SerialMaquinaFiscal AS ConsecutivoTalonario,"
   vSql = vSql & " factura.NumeroResumenDiario AS NumeroResumenDiario,"
   vSql = vSql & gUtilSQL.fBooleanToSqlValue(False) & " AS EsCuentaDeTercero,"
   vSql = vSql & gUtilSQL.fBooleanToSqlValue(False) & " AS MontosConvertido, " & gUtilSQL.fSimpleSqlValueForNumeric(0) & " AS MontoIvaPercibido"
   vSql = vSql & " ,factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSql = vSql & " ,factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
          "Factura.MontoGravableAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
          "Factura.MontoGravableAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
          "Factura.MontoIVAAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " AND (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
          "Factura.MontoIVAAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF), gUtilSQL.fSimpleSqlValue("igtf"), gUtilSQL.fSimpleSqlValue(""), True) & " AS GeneradoPorIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, fSQLRoundForLibrosSaw("factura.BaseImponibleIGTF * factura.CambioABolivares", 2), True) & " AS BaseImponibleIGTF, "
   vSql = vSql & "factura.AlicuotaIGTF AS PorcentajeIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "factura.IGTFML", True) & " AS AlicuotaIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "(" & fSQLRoundForLibrosSaw("factura.TotalFactura  * factura.CambioABolivares", 2) & " + factura.IGTFML)", True) & " AS VentasIncluyendoIvaeIGTF "
   vSql = vSql & vbCrLf & " FROM factura LEFT JOIN"
   vSql = vSql & vbCrLf & " Cxc ON CxC.ConsecutivoCompania = factura.ConsecutivoCompania AND CxC.NumeroDocumentoOrigen = factura.Numero AND"
   vSql = vSql & vbCrLf & " CxC.CodigoCliente = factura.CodigoCliente"
   vSql = vSql & vbCrLf & " INNER JOIN IGV_EnumTipoDocumentoIvaFactura"
   vSql = vSql & vbCrLf & " ON IGV_EnumTipoDocumentoIvaFactura.DbValue = Factura.TipoDeDocumento COLLATE Modern_Spanish_CS_AS"
   vSql = vSql & vbCrLf & " INNER JOIN IGV_EnumTipoDeVentaIva"
   vSql = vSql & vbCrLf & " ON IGV_EnumTipoDeVentaIva.DbValue = Factura.TipoDeVenta COLLATE Modern_Spanish_CS_AS"
   vSql = vSql & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial1 ON Factura.Fecha BETWEEN AlicuotaImpuestoEspecial1.FechaDesde AND AlicuotaImpuestoEspecial1.FechaHasta AND" & vbCrLf
   vSql = vSql & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota2 = AlicuotaImpuestoEspecial1.Alicuota" & vbCrLf
   vSql = vSql & vbCrLf & " AND AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida)
   vSql = vSql & vbCrLf & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial2 ON Factura.Fecha BETWEEN AlicuotaImpuestoEspecial2.FechaDesde AND AlicuotaImpuestoEspecial2.FechaHasta AND" & vbCrLf
   vSql = vSql & vbCrLf & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota3 = AlicuotaImpuestoEspecial2.Alicuota" & vbCrLf
   vSql = vSql & vbCrLf & " AND AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida)
   vSql = vSql & vbCrLf & " WHERE factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSql = vSql & vbCrLf & " AND RealizoCierreZ = " & gUtilSQL.fBooleanToSqlValue(False)
   fSqlViewVentasResumenManual = vSql
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
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSql = vSql & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" factura.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * factura.CambioABolivares * 0.001 ", False) & ")", _
                           "(" & gUtilSQL.getIIF("ABS(" & valValor & ") >= factura.MontoIvaRetenido ", valValor, valValor & " * factura.CambioABolivares ", False) & ")")
                           'valValor & " * factura.CambioABolivares")
   'OJO LA FUNCION NO GENERA ERRORES Y DEVUELVE LOS RESULTADO ESPERADOS PERO DEBE SER EVALUADA
   fSqlConvierteMonto = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlConvierteMonto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlConvierteMontoCXC(ByVal valValor As String, ByVal valAnoAplicacion As String, ByVal valFecha As String) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSql = vSql & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" cxC.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * cxC.CambioABolivares * 0.001 ", False) & ")", _
                           "(" & gUtilSQL.getIIF("ABS(" & valValor & ") >= cxC.MontoIva ", valValor, valValor & " * cxC.CambioABolivares ", False) & ")")
   'OJO LA FUNCION NO GENERA ERRORES Y DEVUELVE LOS RESULTADO ESPERADOS PERO DEBE SER EVALUADA
   fSqlConvierteMontoCXC = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlConvierteMontoCXC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameRetencionesEnVentas() As String
   GetViewNameRetencionesEnVentas = "IVG_RetencionesVentas"
End Function

Public Function fSqlViewRetencionesEnVentas() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT CXC.NumeroDocumentoOrigen as NumeroDocumentoOrigen,"
   vSql = vSql & " Cxc.CodigoCliente as CodigoCliente,"
   vSql = vSql & " Cxc.ConsecutivoCompania as ConsecutivoCompania,"
   vSql = vSql & " Cxc.TipoCxc as TipoCxc,"
   vSql = vSql & " Cxc.Fecha as Fecha,"
   vSql = vSql & " DocumentoCobrado.NumeroComprobanteRetIva as NumeroComprobanteRetIva,"
   vSql = vSql & " DocumentoCobrado.MontoIvaRetenido as MontoIvaRetenido,"
   vSql = vSql & " DocumentoCobrado.NumeroDelDocumentoCobrado as NumeroDelDocumentoCobrado,"
   vSql = vSql & " DocumentoCobrado.TipoDeDocumentoCobrado as TipoDeDocumentoCobrado,"
   vSql = vSql & " Cobranza.Fecha AS FechaComprobanteRetIva"
   vSql = vSql & " FROM Cxc"
   vSql = vSql & " INNER JOIN DocumentoCobrado ON"
   vSql = vSql & " DocumentoCobrado.NumeroComprobanteRetIva <> '0'"
   vSql = vSql & " AND DocumentoCobrado.MontoIvaRetenido > '0'"
   vSql = vSql & " AND DocumentoCobrado.NumeroDelDocumentoCobrado= Cxc.Numero"
   vSql = vSql & " AND DocumentoCobrado.TipoDeDocumentoCobrado = CXC.TipoCxc"
   vSql = vSql & " AND DocumentoCobrado.ConsecutivoCompania = CXC.ConsecutivoCompania"
   vSql = vSql & " INNER JOIN Cobranza ON"
   vSql = vSql & " Cobranza.CodigoCliente = Cxc.CodigoCliente"
   vSql = vSql & " AND Cobranza.Numero = DocumentoCobrado.NumeroCobranza"
   vSql = vSql & " AND Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   fSqlViewRetencionesEnVentas = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRetencionesEnVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameBaseimponibleAlicuotasVentasResumenDeVentas() As String
   GetViewNameBaseimponibleAlicuotasVentasResumenDeVentas = "IGV_BaseimponibleAlicuotasVentasResumenDeVentas"
End Function

Public Function fSqlViewBaseimponibleAlicuotasVentasResumenDeVentas() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT  dbo.factura.ConsecutivoCompania,"
   vSql = vSql & " MIN(factura.NumeroComprobanteFiscal) AS PrimerNumeroComprobante,"
   vSql = vSql & " MAX(factura.NumeroComprobanteFiscal) AS UltimoNumeroComprobante,"
   vSql = vSql & " MIN(factura.Numero) AS PrimerNumeroFactura,"
   vSql = vSql & " MAX(factura.Numero) AS UltimoNumeroFactura,"
   vSql = vSql & " SUM(factura.TotalFactura)AS TotalFactura,"
   vSql = vSql & " SUM(factura.TotalMontoExento) AS Montoexento,"
   vSql = vSql & " SUM(factura.MontoGravableAlicuota1) AS MontogravableAlicuotaGeneral,"
   vSql = vSql & " SUM(factura.MontoGravableAlicuota2) AS MontogravableAlicuota2,"
   vSql = vSql & " SUM(factura.MontoGravableAlicuota3) AS MontogravableAlicuota3,"
   vSql = vSql & " SUM(factura.MontoIVAAlicuota1) AS MontoIvaAlicuotaGeneral,"
   vSql = vSql & " SUM(factura.MontoIVAAlicuota2) AS MontoIvaAlicuota2,"
   vSql = vSql & " SUM(factura.MontoIVAAlicuota3) AS MontoIvaAlicuota3,"
   vSql = vSql & " dbo.factura.SerialMaquinaFiscal, dbo.factura.Fecha,"
   vSql = vSql & " (CASE factura.TipoDeVenta WHEN '3' THEN '0' WHEN '0' THEN '4' ELSE factura.TipoDeVenta END) AS TipoDeVenta, "
   vSql = vSql & " MAX(CASE WHEN Factura.TipoDeVenta = '3' OR Factura.TipoDeVenta = '0' THEN IVG_RetencionesVentas.NumeroComprobanteRetIva ELSE '0' END) AS NumeroComprobanteRetIva,"
   vSql = vSql & " MAX(CASE WHEN Factura.TipoDeVenta = '3' OR Factura.TipoDeVenta = '0' THEN IVG_RetencionesVentas.MontoIvaRetenido ELSE '0' END) AS MontoIvaRetenido,"
   vSql = vSql & " MAX(CASE WHEN Factura.TipoDeVenta = '3' OR Factura.TipoDeVenta = '0' THEN IVG_RetencionesVentas.FechaComprobanteRetIva ELSE '' END) as FechaComprobanteRetIva,"
   vSql = vSql & " SUM(factura.BaseImponibleIGTF) AS BaseImponibleIGTF,"
   vSql = vSql & " SUM(factura.IGTFML) AS IGTFML, "
   vSql = vSql & " factura.TipoDeDocumento"
   vSql = vSql & " FROM Factura "
   vSql = vSql & " LEFT JOIN IVG_RetencionesVentas ON"
   vSql = vSql & " IVG_RetencionesVentas.NumeroDocumentoOrigen = factura.Numero"
   vSql = vSql & " AND IVG_RetencionesVentas.CodigoCliente = factura.CodigoCliente"
   vSql = vSql & " AND IVG_RetencionesVentas.Fecha = factura.Fecha"
   vSql = vSql & " AND IVG_RetencionesVentas.TipoDeDocumentoCobrado = (CASE Factura.TipoDeDocumento WHEN '5' THEN '8' WHEN '7' THEN '<' WHEN '9' THEN '>' END)"
   vSql = vSql & " AND IVG_RetencionesVentas.ConsecutivoCompania  = factura.ConsecutivoCompania"
   vSql = vSql & " Where (dbo.factura.StatusFactura = 0)"
   vSql = vSql & " AND(dbo.factura.TipoDeDocumento = '5' OR dbo.factura.TipoDeDocumento = '7' OR dbo.factura.TipoDeDocumento = '9')"
   vSql = vSql & " GROUP BY dbo.factura.ConsecutivoCompania, dbo.factura.SerialMaquinaFiscal, dbo.factura.Fecha,"
   vSql = vSql & " dbo.factura.NumeroParaResumen , dbo.factura.TipoDeVenta, dbo.factura.TipoDeDocumento"
   fSqlViewBaseimponibleAlicuotasVentasResumenDeVentas = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewBaseimponibleAlicuotasVentasResumenDeVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCxCHistoricoClientes() As String
   GetViewNameCxCHistoricoClientes = "IGV_CxCHistoricoClientes"
End Function

Public Function fSqlViewCxCHistoricoClientes() As String
   Dim Sql As String
   Dim sqlCaseMontoCobrado As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlCaseMontoCobrado = "(cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_ANULADA) & " OR cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion) & ")"
   Sql = "SELECT "
   Sql = Sql & "cxC.ConsecutivoCompania, "
   Sql = Sql & "cxC.CodigoCliente AS Codigo, "
   Sql = Sql & "cliente.Nombre, "
   Sql = Sql & "cxC.Moneda AS MonedaReporte, "
   Sql = Sql & "cxc.CodigoMoneda AS CodMoneda, "
   Sql = Sql & "cxC.Fecha AS FechaDocumento, "
   Sql = Sql & gUtilSQL.getIIF("Cobranza.Moneda IS NULL", "cxC.Moneda", "Cobranza.Moneda") & "AS Moneda, "
   Sql = Sql & "Cobranza.CambioABolivares, "
   Sql = Sql & "cxC.FechaVencimiento, "
   Sql = Sql & gUtilSQL.DfSQLCaseIfForEnum("CxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "TipoDeDocumento") & ", "
   Sql = Sql & "cxC.Numero AS NumeroDocumento, "
   Sql = Sql & gUtilSQL.fCast("CxC.CodigoCliente" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxC.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   Sql = Sql & "cobranza.Numero AS NumeroCobranza, "
   Sql = Sql & "cobranza.Fecha AS FechaCobranza, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloTipoReporte, "
   Sql = Sql & gUtilSQL.getIIF("cobranza.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeDocumentoCobranza.eTDDC_CobranzaPorAplicacionDeRetencion), gUtilSQL.fSimpleSqlValue("Cobro(*)"), gUtilSQL.fSimpleSqlValue("Cobro"), True) & " AS TipoDocumentoDetalle,"
   Sql = Sql & gUtilSQL.getIIF(sqlCaseMontoCobrado, "0", "DocumentoCobrado.MontoAbonadoEnMonedaOriginal") & " AS MontoCobrado, "
   Sql = Sql & "documentoCobrado.CambioAMonedaDeCobranza AS CambioMonedaCobranza, "
   Sql = Sql & "documentoCobrado.CambioAMonedaLocal AS CambioALocalDesdeMonedaCxC, "
   Sql = Sql & "cobranza.StatusCobranza "
   Sql = Sql & "FROM cliente "
   Sql = Sql & "INNER JOIN cxC "
   Sql = Sql & "ON cliente.Codigo = cxC.CodigoCliente "
   Sql = Sql & "AND cliente.ConsecutivoCompania = cxC.ConsecutivoCompania "
   Sql = Sql & "RIGHT OUTER JOIN cobranza "
   Sql = Sql & "LEFT OUTER JOIN documentoCobrado "
   Sql = Sql & "ON cobranza.Numero = documentoCobrado.NumeroCobranza "
   Sql = Sql & "AND cobranza.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania "
   Sql = Sql & "ON cxC.Numero = documentoCobrado.NumeroDelDocumentoCobrado "
   Sql = Sql & "AND cxC.TipoCxc = documentoCobrado.TipoDeDocumentoCobrado "
   Sql = Sql & "AND cxC.ConsecutivoCompania = documentoCobrado.ConsecutivoCompania "
   Sql = Sql & "WHERE CxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   fSqlViewCxCHistoricoClientes = Sql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCxCHistoricoClientes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameSaldoInicialCxCHistoricoCliente() As String
   GetViewNameSaldoInicialCxCHistoricoCliente = "IGV_SaldoInicialCxCHistoricoCliente"
End Function

Public Function fSqlViewSaldoInicialCxCHistoricoCliente() As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Sql = "SELECT "
   Sql = Sql & "ConsecutivoCompania, "
   Sql = Sql & "CodigoCliente, "
   Sql = Sql & "Numero, "
   Sql = Sql & "TipoCxc, "
   Sql = Sql & "Fecha, "
   Sql = Sql & "CambioABolivares, "
   Sql = Sql & "Moneda, "
   Sql = Sql & "CodigoMoneda, "
   Sql = Sql & "MontoExento + MontoGravado + MontoIva AS SaldoInicial, "
   Sql = Sql & "MontoExento + MontoGravado + MontoIva - MontoAbonado AS SaldoInicialConAbonos "
   Sql = Sql & "FROM CxC "
   Sql = Sql & "WHERE " & " CxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   fSqlViewSaldoInicialCxCHistoricoCliente = Sql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewSaldoInicialCxCHistoricoCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameInfoCxCHistoricoCliente() As String
   GetViewNameInfoCxCHistoricoCliente = "IGV_InfoCxCHistoricoCliente"
End Function

Public Function fSqlViewInfoCxCHistoricoCliente() As String
   Dim Sql As String
   Dim sqlTipoDeCxC As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlTipoDeCxC = gUtilSQL.DfSQLCaseIfForEnum("CxC.TipoCxC", enum_TipoDeTransaccion.eTD_FACTURA, gEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, True), "")
   Sql = "SELECT "
   Sql = Sql & "cxC.ConsecutivoCompania AS ConsecutivoCompania, "
   Sql = Sql & "CodigoCliente AS Codigo, "
   Sql = Sql & "Nombre AS NombreCliente, "
   Sql = Sql & "Numero AS NumeroDocumento, "
   Sql = Sql & "Fecha AS FechaDocumento, "
   Sql = Sql & "FechaVencimiento AS FechaVencimiento, "
   Sql = Sql & "Moneda AS MonedaReporte, "
   Sql = Sql & "CodigoMoneda AS CodMoneda, "
   Sql = Sql & "CambioABolivares AS CambioABolivares, "
   Sql = Sql & sqlTipoDeCxC & " AS TipoDeDocumento, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cuentas por Cobrar") & " AS TituloTipoReporte, "
   Sql = Sql & gUtilSQL.fSimpleSqlValue("Cobro") & " AS TipoDocumentoDetalle,"
   Sql = Sql & gUtilSQL.fCast("CxC.CodigoCliente" & fSQLSeparadorDeCamposStandardDelNroDocumentoOrigen & "CxC.Numero", eTDSS_VARCHAR, "") & " AS NumeroDocumentoGrupo, "
   Sql = Sql & "MontoExento + MontoGravado + MontoIva AS MontoOriginal, "
   Sql = Sql & "MontoExento + MontoGravado + MontoIva - MontoAbonado AS SaldoActual "
   Sql = Sql & "FROM cliente "
   Sql = Sql & "INNER JOIN cxC "
   Sql = Sql & "ON cliente.Codigo = cxC.CodigoCliente "
   Sql = Sql & "AND cliente.ConsecutivoCompania = cxC.ConsecutivoCompania "
   Sql = Sql & "WHERE " & " CxC.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   fSqlViewInfoCxCHistoricoCliente = Sql
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
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = "ROUND ((" & valValueExpression & "), " & gUtilSQL.fSimpleSqlValueForNumeric(valDecimals) & ") "
   fSQLRoundForLibrosSaw = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR:
   fSQLRoundForLibrosSaw = valValueExpression
   On Error GoTo 0
End Function

Public Function fSqlViewVentasIVAAgrupado() As String 'datos tomado del cierre z
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = fSqlViewVentasFacturaAgrupado
   vSql = vSql & " UNION " & fSqlViewVentasCxC
   vSql = vSql & " UNION " & fSqlViewVentasResumenAgrupado
   fSqlViewVentasIVAAgrupado = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasIVAAgrupado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function fSqlViewVentasFacturaAgrupado() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT IGV_FacturaBase.ConsecutivoCompania AS ConsecutivoCompania, "
   vSql = vSql & " IGV_FacturaBase.Numero AS Numero,"
   vSql = vSql & vbCrLf & "(SELECT CodTipoDocumentoSawIva FROM IGV_EnumTipoDocumentoIvaFactura WHERE DbValue = IGV_FacturaBase.TipoDeDocumento COLLATE Modern_Spanish_CS_AS) AS TipoDeDocumento, "
   vSql = vSql & vbCrLf & "(SELECT codTipoDeVentaIVA FROM IGV_EnumTipoDeVentaIva WHERE DbValue = IGV_FacturaBase.TipoDeVenta COLLATE Modern_Spanish_CS_AS ) AS TipoDeVenta , "
   vSql = vSql & " (CASE WHEN IGV_FacturaBase.EsDiferida = 'S' THEN '1' ELSE '0' END) AS StatusVenta,"
   vSql = vSql & " ( CASE  WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSql = vSql & fSqlConvierteMontoFacturaBase("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", " IGV_FacturaBase.Fecha") & " as MontoIvaRetenido, "
   vSql = vSql & " IGV_FacturaBase.TipoDeTransaccion AS TipoDeTransaccion,"
   vSql = vSql & " IGV_FacturaBase.Fecha AS Fecha,"
   vSql = vSql & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN MONTH(IGV_FacturaBase.Fecha)"
   vSql = vSql & "  ELSE MONTH(CobranzaFactura.FechaCobranza) END)"
   vSql = vSql & "  ELSE MONTH(IGV_FacturaBase.Fecha) END) AS MesDeAplicacion,"
   vSql = vSql & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN YEAR(IGV_FacturaBase.Fecha)"
   vSql = vSql & "  ELSE YEAR(CobranzaFactura.FechaCobranza) END)"
   vSql = vSql & "  ELSE YEAR(IGV_FacturaBase.Fecha) END)AS anoDeAplicacion,"
   vSql = vSql & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN DAY(IGV_FacturaBase.Fecha)"
   vSql = vSql & "  ELSE DAY(CobranzaFactura.FechaCobranza) END)"
   vSql = vSql & "  ELSE DAY(IGV_FacturaBase.Fecha) END)AS DiaDeAplicacion,"
   vSql = vSql & " (CASE WHEN IGV_FacturaBase.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL  THEN IGV_FacturaBase.Fecha"
   vSql = vSql & "  ELSE CobranzaFactura.FechaCobranza END)"
   vSql = vSql & "  ELSE IGV_FacturaBase.Fecha END)AS FechaAplicacion,"
   vSql = vSql & " IGV_FacturaBase.CodigoCliente AS CodigoCliente,"
   vSql = vSql & " IGV_FacturaBase.NumeroHasta AS NumeroHasta,"
   vSql = vSql & " '' AS PorcentajePasajeAereo,"
   vSql = vSql & " IGV_FacturaBase.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSql = vSql & " IGV_FacturaBase.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSql = vSql & " IGV_FacturaBase.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSql = vSql & " IGV_FacturaBase.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSql = vSql & " IGV_FacturaBase.NumeroControl AS NumeroDeControlDeFactura,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("IGV_FacturaBase.TotalFactura * IGV_FacturaBase.CambioABolivares", 2), True) & " as TotalFactura,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("IGV_FacturaBase.TotalMontoExento * IGV_FacturaBase.CambioABolivares", 2), True) & " as Montoexento,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True) & " as MontogravableAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota2 * IGV_FacturaBase.CambioABolivares", 2), True) & " as MontoGravableAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota3 * IGV_FacturaBase.CambioABolivares", 2), True) & " as MontoGravableAlicuota3,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontoIvaAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True) & " as  MontoIVAAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontoIvaAlicuota2 * IGV_FacturaBase.CambioABolivares", 2), True) & " as  MontoIVAAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontoIvaAlicuota3 * IGV_FacturaBase.CambioABolivares", 2), True) & " as  MontoIVAAlicuota3,"
   vSql = vSql & " IGV_FacturaBase.NombreOperador AS NombreOperador,"
   vSql = vSql & " IGV_FacturaBase.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " IGV_FacturaBase.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSql = vSql & " IGV_FacturaBase.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSql = vSql & " IGV_FacturaBase.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSql = vSql & " IGV_FacturaBase.Talonario AS ConsecutivoTalonario,"
   vSql = vSql & " '' AS NumeroResumenDiario,"
   vSql = vSql & " 'N' AS EsCuentaDeTercero,"
   vSql = vSql & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSql = vSql & " ,IGV_FacturaBase.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSql = vSql & " ,IGV_FacturaBase.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSql = vSql & " ," & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
         " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
          " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("IGV_FacturaBase.MontogravableAlicuota1 * IGV_FacturaBase.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2, "
   vSql = vSql & "IGV_FacturaBase.GeneradoPorIGTF, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "IGV_FacturaBase.BaseImponibleIGTF", True) & " AS BaseImponibleIGTF, "
   vSql = vSql & "IGV_FacturaBase.PorcentajeIGTF AS PorcentajeIGTF, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "IGV_FacturaBase.AlicuotaIGTF", True) & " AS AlicuotaIGTF, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "IGV_FacturaBase.VentasIncluyendoIvaeIGTF", True) & " AS VentasIncluyendoIvaeIGTF "
   vSql = vSql & " FROM            IGV_FacturaBase LEFT JOIN" & vbCrLf
   vSql = vSql & " IGV_RetencionDocumentoCobrado ON IGV_FacturaBase.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND" & vbCrLf
   vSql = vSql & " IGV_FacturaBase.Numero = IGV_RetencionDocumentoCobrado.NumeroDocumentoOrigen AND" & vbCrLf
   vSql = vSql & " IGV_FacturaBase.TipoDeDocumento = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado " & vbCrLf
   vSql = vSql & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON IGV_FacturaBase.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSql = vSql & " IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND IGV_FacturaBase.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota LEFT JOIN" & vbCrLf
   vSql = vSql & " (SELECT DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen AS NumeroFacturaOrigen," & vbCrLf
   vSql = vSql & gUtilSQL.fMin("Cobranza.Fecha", "FechaCobranza") & vbCrLf
   vSql = vSql & " FROM DocumentoCobrado INNER JOIN" & vbCrLf
   vSql = vSql & " Cxc ON Cxc.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado AND" & vbCrLf
   vSql = vSql & " Cxc.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania INNER JOIN" & vbCrLf
   vSql = vSql & " IGV_FacturaBase ON IGV_FacturaBase.Numero = Cxc.NumeroDocumentoOrigen AND" & vbCrLf
   vSql = vSql & " IGV_FacturaBase.ConsecutivoCompania = Cxc.ConsecutivoCompania INNER JOIN" & vbCrLf
   vSql = vSql & " Cobranza ON Cobranza.Numero = DocumentoCobrado.NumeroCobranza AND" & vbCrLf
   vSql = vSql & " Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania" & vbCrLf
   vSql = vSql & " WHERE IGV_FacturaBase.EsDiferida = " & gUtilSQL.fBooleanToSqlValue(False) & vbCrLf
   vSql = vSql & " AND IGV_FacturaBase.EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True) & vbCrLf
   vSql = vSql & " AND Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & vbCrLf
   vSql = vSql & " GROUP BY DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen) AS CobranzaFactura" & vbCrLf
   vSql = vSql & " ON IGV_FacturaBase.Numero = CobranzaFactura.NumeroFacturaOrigen AND" & vbCrLf
   vSql = vSql & " IGV_FacturaBase.ConsecutivoCompania = CobranzaFactura.ConsecutivoCompania" & vbCrLf
   vSql = vSql & " WHERE IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vSql = vSql & " AND IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vSql = vSql & " AND IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
   vSql = vSql & " AND IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADECREDITOCOMPROBANTEFISCAL)
   vSql = vSql & " AND IGV_FacturaBase.TipoDeDocumentoFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTADEDEBITOCOMPROBANTEFISCAL)
   vSql = vSql & " AND IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_BORRADOR)
   fSqlViewVentasFacturaAgrupado = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasFacturaAgrupado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasResumenAgrupado() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT IGV_FacturaBase.ConsecutivoCompania AS ConsecutivoCompania, "
   vSql = vSql & " IGV_FacturaBase.NumeroDesde  AS Numero, "
   vSql = vSql & " IGV_EnumTipoDocumentoIvaFactura.CodTipoDocumentoSawIva  AS TipoDeDocumento,"
   vSql = vSql & " IGV_EnumTipoDeVentaIva.codTipoDeVentaIVA AS TipoDeVenta,"
   vSql = vSql & " '0' AS StatusVenta,"
   vSql = vSql & " ( CASE  WHEN IGV_FacturaBase.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSql = vSql & " IGV_FacturaBase.MontoIvaRetenido  As MontoIvaRetenido, "
   vSql = vSql & " IGV_FacturaBase.TipoDeTransaccion AS TipoDeTransaccion,"
   vSql = vSql & " IGV_FacturaBase.Fecha AS Fecha,"
   vSql = vSql & " MONTH(IGV_FacturaBase.Fecha) AS MesDeAplicacion,"
   vSql = vSql & " YEAR(IGV_FacturaBase.Fecha) AS anoDeAplicacion,"
   vSql = vSql & " DAY(IGV_FacturaBase.Fecha) AS DiaDeAplicacion,"
   vSql = vSql & " IGV_FacturaBase.Fecha AS FechaAplicacion,"
   vSql = vSql & " IGV_FacturaBase.CodigoCliente AS CodigoCliente,"
   vSql = vSql & " IGV_FacturaBase.NumeroHasta AS NumeroHasta,"
   vSql = vSql & " '' AS PorcentajePasajeAereo,"
   vSql = vSql & " IGV_FacturaBase.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSql = vSql & " IGV_FacturaBase.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSql = vSql & " IGV_FacturaBase.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSql = vSql & " IGV_FacturaBase.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSql = vSql & " IGV_FacturaBase.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSql = vSql & " IGV_FacturaBase.NumeroControl AS NumeroDeControlDeFactura,"
   vSql = vSql & " IGV_FacturaBase.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSql = vSql & " IGV_FacturaBase.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.TotalFactura", True) & " AS TotalFactura, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.TotalMontoExento", True) & " AS MontoExento, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoGravableAlicuota1", True) & " AS MontoGravableAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoGravableAlicuota2", True) & " AS MontoGravableAlicuota2, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoGravableAlicuota3", True) & " AS MontoGravableAlicuota3, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura =" & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoIVAAlicuota1", True) & " AS MontoIVAAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoIVAAlicuota2", True) & " AS MontoIVAAlicuota2, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " OR (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
         gUtilSQL.fSimpleSqlValueForNumeric(0), "IGV_FacturaBase.MontoIVAAlicuota3", True) & " AS MontoIVAAlicuota3, "
   vSql = vSql & " IGV_FacturaBase.NombreOperador AS NombreOperador,"
   vSql = vSql & " IGV_FacturaBase.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " IGV_FacturaBase.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSql = vSql & " IGV_FacturaBase.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSql = vSql & " IGV_FacturaBase.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSql = vSql & " IGV_FacturaBase.SerialMaquinaFiscal AS ConsecutivoTalonario,"
   vSql = vSql & " IGV_FacturaBase.NumeroResumenDiario AS NumeroResumenDiario,"
   vSql = vSql & " 'N' AS EsCuentaDeTercero,"
   vSql = vSql & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido"
   vSql = vSql & " ,IGV_FacturaBase.EsOriginalmenteDiferida AS EsOriginalmenteDiferida"
   vSql = vSql & " ,IGV_FacturaBase.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial,"
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         "IGV_FacturaBase.MontoGravableAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
         "IGV_FacturaBase.MontoGravableAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & "))", _
         "IGV_FacturaBase.MontoIVAAlicuota2", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
         " AND (IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
         " AND (AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", _
         "IGV_FacturaBase.MontoIVAAlicuota3", gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2,"
   vSql = vSql & " IGV_FacturaBase.GeneradoPorIGTF AS GeneradoPorIGTF, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, " IGV_FacturaBase.BaseImponibleIGTF", True) & " AS BaseImponibleIGTF, "
   vSql = vSql & " IGV_FacturaBase.PorcentajeIGTF AS PorcentajeIGTF, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, " IGV_FacturaBase.AlicuotaIGTF", True) & " AS AlicuotaIGTF, "
   vSql = vSql & gUtilSQL.getIIF("IGV_FacturaBase.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, " IGV_FacturaBase.VentasIncluyendoIvaeIGTF", True) & " AS VentasIncluyendoIvaeIGTF "
   vSql = vSql & " FROM IGV_FacturaBase LEFT JOIN"
   vSql = vSql & " Cxc ON CxC.ConsecutivoCompania = IGV_FacturaBase.ConsecutivoCompania AND CxC.NumeroDocumentoOrigen = IGV_FacturaBase.Numero AND"
   vSql = vSql & " CxC.CodigoCliente = IGV_FacturaBase.CodigoCliente"
   vSql = vSql & " INNER JOIN IGV_EnumTipoDocumentoIvaFactura"
   vSql = vSql & " ON IGV_EnumTipoDocumentoIvaFactura.DbValue = IGV_FacturaBase.TipoDeDocumentoFactura COLLATE Modern_Spanish_CS_AS"
   vSql = vSql & " INNER JOIN IGV_EnumTipoDeVentaIva"
   vSql = vSql & " ON IGV_EnumTipoDeVentaIva.DbValue COLLATE Modern_Spanish_CS_AS = IGV_FacturaBase.TipoDeVenta"
   vSql = vSql & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial1 ON IGV_FacturaBase.Fecha BETWEEN AlicuotaImpuestoEspecial1.FechaDesde AND AlicuotaImpuestoEspecial1.FechaHasta AND" & vbCrLf
   vSql = vSql & " IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND IGV_FacturaBase.PorcentajeAlicuota2 = AlicuotaImpuestoEspecial1.Alicuota" & vbCrLf
   vSql = vSql & " AND AlicuotaImpuestoEspecial1.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida)
   vSql = vSql & " LEFT JOIN Comun.AlicuotaImpuestoEspecial AS AlicuotaImpuestoEspecial2 ON IGV_FacturaBase.Fecha BETWEEN AlicuotaImpuestoEspecial2.FechaDesde AND AlicuotaImpuestoEspecial2.FechaHasta AND" & vbCrLf
   vSql = vSql & " IGV_FacturaBase.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND IGV_FacturaBase.PorcentajeAlicuota3 = AlicuotaImpuestoEspecial2.Alicuota" & vbCrLf
   vSql = vSql & " AND AlicuotaImpuestoEspecial2.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida)
   vSql = vSql & " WHERE IGV_FacturaBase.TipoDeDocumentoFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   fSqlViewVentasResumenAgrupado = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasResumenAgrupado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewVentasIVADetallado() As String ' detalle de todas las facturas
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = fSqlVentasDetalladoSeccionUnion(False)
   vSql = vSql & " UNION "
   vSql = vSql & fSqlVentasDetalladoSeccionUnion(True)
   fSqlViewVentasIVADetallado = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewVentasIVADetallado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlViewFacturaBase() As String
   Dim vSql As String
   Dim vCase1 As String
   Dim vCase2 As String
   Dim vCase3 As String
   On Error GoTo h_ERROR
   vCase1 = gUtilSQL.getIIF("factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("2"), gUtilSQL.fSimpleSqlValue("4"), gUtilSQL.fSimpleSqlValue(" "), True)
   vCase2 = gUtilSQL.getIIF("factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("1"), gUtilSQL.fSimpleSqlValue("3"), vCase1, True)
   vCase3 = gUtilSQL.getIIF("factura.TipoDeDocumento = " & gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("0"), vCase2, True)
   vSql = vSql & "SELECT factura.ConsecutivoCompania AS ConsecutivoCompania,  factura.Numero AS Numero," & vbCrLf
   vSql = vSql & vCase3 & " AS TipoDeDocumento, factura.TipoDeVenta AS TipoDeVenta, factura.TipoDeDocumento AS TipoDeDocumentoFactura," & vbCrLf
   vSql = vSql & "factura.Fecha, factura.CodigoMoneda AS CodigoMoneda, factura.CambioABolivares AS CambioABolivares," & vbCrLf
   vSql = vSql & "factura.TipoDeTransaccion AS TipoDeTransaccion, factura.CodigoCliente AS CodigoCliente, factura.NumeroHasta AS NumeroHasta," & vbCrLf
   vSql = vSql & "factura.NumeroDesde AS NumeroDesde, factura.SeRetuvoIVA AS SeRetuvoIVA, factura.MontoIvaRetenido, factura.FechaComprobanteRetIVA," & vbCrLf
   vSql = vSql & "factura.NumeroComprobanteRetIVA, factura.FechaAplicacionRetIVA,factura.TotalFactura, factura.TotalMontoExento, factura.MontoGravableAlicuota1," & vbCrLf
   vSql = vSql & "factura.MontoGravableAlicuota2, factura.MontoGravableAlicuota3, factura.MontoIVAAlicuota1, factura.MontoIVAAlicuota2," & vbCrLf
   vSql = vSql & "factura.MontoIVAAlicuota3, factura.SerialMaquinaFiscal," & vbCrLf
   vSql = vSql & "factura.NumeroControlHasta AS NumeroControlHasta, factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora, factura.NumeroResumenDiario AS NumeroResumenDiario," & vbCrLf
   vSql = vSql & "factura.NumeroPlanillaExportacion AS NumeroPlanillaExportacion, factura.NumeroFacturaAfectada AS NumeroFacturaAfectada," & vbCrLf
   vSql = vSql & "factura.NumeroControl AS NumeroControl, factura.StatusFactura AS StatusFactura, factura.PorcentajeDescuento," & vbCrLf
   vSql = vSql & "factura.NombreOperador AS NombreOperador, factura.FechaUltimaModificacion AS FechaUltimaModificacion," & vbCrLf
   vSql = vSql & "factura.PorcentajeAlicuota1 AS PorcentajeAlicuota1, factura.PorcentajeAlicuota2 AS PorcentajeAlicuota2," & vbCrLf
   vSql = vSql & "factura.PorcentajeAlicuota3 AS PorcentajeAlicuota3, " & vbCrLf
   vSql = vSql & gUtilSQL.getIIF("Factura.Talonario = " & gUtilSQL.fSimpleSqlValue("0"), gUtilSQL.fSimpleSqlValue("1"), gUtilSQL.fSimpleSqlValue("2")) & " AS Talonario," & vbCrLf
   vSql = vSql & gUtilSQL.fSimpleSqlValue("N") & " AS EsCuentaDeTercero, " & gUtilSQL.fSimpleSqlValue("N") & " AS MontosConvertido, " & vbCrLf
   vSql = vSql & gUtilSQL.fSimpleSqlValue("0") & " AS MontoIvaPercibido, " & vbCrLf
   vSql = vSql & "factura.EsDiferida, factura.EsOriginalmenteDiferida, factura.AplicaDecretoIvaEspecial," & vbCrLf
   vSql = vSql & gUtilSQL.getIIF("Factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF), gUtilSQL.fSimpleSqlValue("igtf"), gUtilSQL.fSimpleSqlValue(""), True) & " AS GeneradoPorIGTF, " & vbCrLf
   vSql = vSql & fSQLRoundForLibrosSaw("factura.BaseImponibleIGTF * factura.CambioABolivares", 2) & " AS BaseImponibleIGTF, " & vbCrLf
   vSql = vSql & "factura.AlicuotaIGTF AS PorcentajeIGTF, " & vbCrLf
   vSql = vSql & "factura.IGTFML AS AlicuotaIGTF, " & vbCrLf
   vSql = vSql & "(" & fSQLRoundForLibrosSaw("factura.TotalFactura  * factura.CambioABolivares", 2) & " + factura.IGTFML) AS VentasIncluyendoIvaeIGTF " & vbCrLf
   vSql = vSql & " FROM factura" & vbCrLf
   fSqlViewFacturaBase = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewFacturaBase", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameFacturaBase() As String
   GetViewNameFacturaBase = "IGV_FacturaBase"
End Function

Private Function fSqlConvierteMontoFacturaBase(ByVal valValor As String, ByVal valAnoAplicacion As String, ByVal valFecha As String) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSql = vSql & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" IGV_FacturaBase.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * IGV_FacturaBase.CambioABolivares * 0.001 ", False) & ")", _
                           "(" & gUtilSQL.getIIF("ABS(" & valValor & ") >= IGV_FacturaBase.MontoIvaRetenido ", valValor, valValor & " * IGV_FacturaBase.CambioABolivares ", False) & ")")
                           'valValor & " * IGV_FacturaBase.CambioABolivares")
   fSqlConvierteMontoFacturaBase = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlConvierteMontoFacturaBase", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlVentasDetalladoSeccionUnion(ByVal valEsTipoResumenDiario As Boolean) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT factura.ConsecutivoCompania AS ConsecutivoCompania, "
   If valEsTipoResumenDiario Then
      vSql = vSql & " Factura.NumeroDesde  AS Numero ,"
   Else
      vSql = vSql & " (CASE WHEN Factura.TipoDeDocumento ='5' or Factura.TipoDeDocumento ='7' or Factura.TipoDeDocumento ='9' THEN factura.NumeroComprobanteFiscal ELSE factura.Numero END )  AS Numero ,"
   End If
   vSql = vSql & "(SELECT codTipoDeVentaIVA FROM IGV_EnumTipoDeVentaIva WHERE DbValue = factura.TipoDeVenta COLLATE Modern_Spanish_CS_AS ) AS TipoDeVenta , "
   vSql = vSql & "(SELECT CodTipoDocumentoSawIva FROM IGV_EnumTipoDocumentoIvaFactura WHERE DbValue = factura.TipoDeDocumento COLLATE Modern_Spanish_CS_AS) AS TipoDeDocumento, "
   vSql = vSql & " (CASE WHEN Factura.EsDiferida = 'S' THEN '1' ELSE '0' END) AS StatusVenta,"
   vSql = vSql & " ( CASE  WHEN IGV_RetencionDocumentoCobrado.SeRetuvoIva = 'N' THEN '0' ELSE '1' END ) AS StatusRetencion, "
   vSql = vSql & fSqlConvierteMonto("IGV_RetencionDocumentoCobrado.MontoIvaRetenido", "Year(IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva)", " Factura.Fecha") & " as MontoIvaRetenido, "
   vSql = vSql & " factura.TipoDeTransaccion AS TipoDeTransaccion,"
   vSql = vSql & " factura.Fecha AS Fecha,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL THEN MONTH(Factura.Fecha)"
   vSql = vSql & "  ELSE MONTH(CobranzaFactura.FechaCobranza) END)"
   vSql = vSql & "  ELSE MONTH(Factura.Fecha) END) AS MesDeAplicacion,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN YEAR(Factura.Fecha)"
   vSql = vSql & "  ELSE YEAR(CobranzaFactura.FechaCobranza) END)"
   vSql = vSql & "  ELSE YEAR(Factura.Fecha) END)AS anoDeAplicacion,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL   THEN DAY(Factura.Fecha)"
   vSql = vSql & "  ELSE DAY(CobranzaFactura.FechaCobranza) END)"
   vSql = vSql & "  ELSE DAY(Factura.Fecha) END)AS DiaDeAplicacion,"
   vSql = vSql & " (CASE WHEN Factura.EsOriginalmenteDiferida = 'S' THEN "
   vSql = vSql & " (CASE WHEN CobranzaFactura.NumeroFacturaOrigen IS NULL  THEN Factura.Fecha"
   vSql = vSql & "  ELSE CobranzaFactura.FechaCobranza END)"
   vSql = vSql & "  ELSE Factura.Fecha END)AS FechaAplicacion,"
   vSql = vSql & "  CobranzaFactura.NumeroFacturaOrigen As CobranzaNumeroFacturaOrigen,"
   vSql = vSql & "  CobranzaFactura.FechaCobranza AS FechaCobranza,"
   vSql = vSql & " factura.CodigoCliente AS CodigoCliente,"
   vSql = vSql & " Factura.NumeroHasta AS NumeroHasta,"
   vSql = vSql & " '' AS PorcentajePasajeAereo,"
   vSql = vSql & " Factura.NumeroControlHasta AS NumeroDeControlDeFacturaHasta,"
   vSql = vSql & " factura.CodigoMaquinaRegistradora AS CodigoMaquinaRegistradora,"
   vSql = vSql & " factura.NumeroPlanillaExportacion AS NumeroDePlanillaExpotacion,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaComprobanteRetIva AS FechaComprobanteRetencion,"
   vSql = vSql & " factura.NumeroFacturaAfectada AS NumeroFacturaAfectada,"
   vSql = vSql & " factura.NumeroControl AS NumeroDeControlDeFactura,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.NumeroComprobanteRetIva AS ConsecutivoComprobanteRetencion,"
   vSql = vSql & " IGV_RetencionDocumentoCobrado.FechaAplicacionRetIVA AS FechaAplicacionRetencion,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalFactura * factura.CambioABolivares", 2), True) & " as TotalFactura,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
           fSQLRoundForLibrosSaw("Factura.TotalMontoExento * factura.CambioABolivares", 2), True) & " as Montoexento,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
           fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True) & " as MontogravableAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoGravableAlicuota3,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & _
          " OR (Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & _
          " AND (Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & " OR Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & "))", 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True) & " as MontoIVAAlicuotaGeneral,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota2 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota3 * factura.CambioABolivares", 2), True) & " as MontoIVAAlicuota3,"
   vSql = vSql & " factura.NombreOperador AS NombreOperador,"
   vSql = vSql & " factura.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " factura.PorcentajeAlicuota1 AS PorcentajeIvaAlicuotaGeneral,"
   vSql = vSql & " factura.PorcentajeAlicuota2 AS PorcentajeIvaAlicuota2,"
   vSql = vSql & " factura.PorcentajeAlicuota3 AS PorcentajeIvaAlicuota3,"
   vSql = vSql & " (CASE WHEN Factura.TipoDeDocumento ='5' or Factura.TipoDeDocumento ='7' or Factura.TipoDeDocumento ='9' THEN SerialMaquinaFiscal ELSE (CASE Factura.Talonario WHEN '0' THEN '1' ELSE '2'  END ) END )  AS ConsecutivoTalonario,"
   vSql = vSql & " '' AS NumeroResumenDiario,"
   vSql = vSql & " 'N' AS EsCuentaDeTercero,"
   vSql = vSql & " 'N' AS MontosConvertido, 0 AS MontoIvaPercibido,"
   vSql = vSql & " factura.EsOriginalmenteDiferida AS EsOriginalmenteDiferida,"
   vSql = vSql & " factura.AplicaDecretoIvaEspecial AS AplicaDecretoIvaEspecial"
   vSql = vSql & " ," & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
         " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontogravableAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoGravableAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Reducida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial1,"
   vSql = vSql & gUtilSQL.getIIF("Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Comun.AlicuotaImpuestoEspecial.AlicuotaIVAASustituir = " & gUtilSQL.fSQLSimpleValueForEnum(enum_AlicuotaIVA.eAIVA_Extendida) & _
          " AND Factura.StatusFactura <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), _
          gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, _
          fSQLRoundForLibrosSaw("Factura.MontoIvaAlicuota1 * Factura.CambioABolivares", 2), True), _
          gUtilSQL.fSimpleSqlValueForNumeric(0), True) & " AS MontoIVAAlicuotaEspecial2,"
   vSql = vSql & gUtilSQL.getIIF("Factura.GeneradoPor = " & gUtilSQL.fSQLSimpleValueForEnum(enum_FacturaGeneradaPor.eFGP_AjusteIGTF), gUtilSQL.fSimpleSqlValue("igtf"), gUtilSQL.fSimpleSqlValue(""), True) & " AS GeneradoPorIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, fSQLRoundForLibrosSaw("factura.BaseImponibleIGTF * factura.CambioABolivares", 2), True) & " AS BaseImponibleIGTF, "
   vSql = vSql & "factura.AlicuotaIGTF AS PorcentajeIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "factura.IGTFML", True) & " AS AlicuotaIGTF, "
   vSql = vSql & gUtilSQL.getIIF("Factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA), 0, "(" & fSQLRoundForLibrosSaw("factura.TotalFactura  * factura.CambioABolivares", 2) & " + factura.IGTFML)", True) & " AS VentasIncluyendoIvaeIGTF "
   vSql = vSql & " FROM factura LEFT JOIN"
   vSql = vSql & " Cxc ON CxC.ConsecutivoCompania = factura.ConsecutivoCompania AND "
   vSql = vSql & " CxC.NumeroDocumentoOrigen = factura.Numero AND "
   vSql = vSql & " CxC.CodigoCliente = factura.CodigoCliente"
   vSql = vSql & " AND Cxc.TipoCxc = (CASE Factura.TipoDeDocumento "
   vSql = vSql & " WHEN '0' THEN '0' " 'Factura
   vSql = vSql & " WHEN '1' THEN '3' " 'NC
   vSql = vSql & " WHEN '2' THEN '4' " 'ND
   vSql = vSql & " WHEN '5' THEN '8' " 'CF
   vSql = vSql & " WHEN '7' THEN '<' " 'NC-CF
   vSql = vSql & " WHEN '9' THEN '>' END)" 'ND-CF
   vSql = vSql & " LEFT JOIN IGV_RetencionDocumentoCobrado ON"
   vSql = vSql & " CxC.ConsecutivoCompania = IGV_RetencionDocumentoCobrado.ConsecutivoCompania AND"
   vSql = vSql & " Cxc.Numero = IGV_RetencionDocumentoCobrado.NumeroDelDocumentoCobrado AND"
   vSql = vSql & " Cxc.TipoCxc = IGV_RetencionDocumentoCobrado.TipoDeDocumentoCobrado"
   vSql = vSql & " LEFT JOIN Comun.AlicuotaImpuestoEspecial ON Factura.Fecha BETWEEN Comun.AlicuotaImpuestoEspecial.FechaDesde AND Comun.AlicuotaImpuestoEspecial.FechaHasta AND" & vbCrLf
   vSql = vSql & " Factura.AplicaDecretoIvaEspecial = " & gUtilSQL.fBooleanToSqlValue(True) & " AND Factura.PorcentajeAlicuota1 = Comun.AlicuotaImpuestoEspecial.Alicuota" & vbCrLf
   vSql = vSql & " LEFT JOIN (SELECT DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen AS NumeroFacturaOrigen," & vbCrLf
   vSql = vSql & " " & gUtilSQL.fMin("Cobranza.Fecha", "FechaCobranza") & vbCrLf
   vSql = vSql & " FROM DocumentoCobrado" & vbCrLf
   vSql = vSql & " INNER JOIN Cxc ON Cxc.Numero = DocumentoCobrado.NumeroDelDocumentoCobrado" & vbCrLf
   vSql = vSql & " AND Cxc.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania"
   vSql = vSql & " INNER JOIN Factura ON Factura.Numero = Cxc.NumeroDocumentoOrigen" & vbCrLf
   vSql = vSql & " AND Factura.ConsecutivoCompania = Cxc.ConsecutivoCompania" & vbCrLf
   vSql = vSql & " INNER JOIN Cobranza ON Cobranza.Numero = DocumentoCobrado.NumeroCobranza" & vbCrLf
   vSql = vSql & " AND Cobranza.ConsecutivoCompania = DocumentoCobrado.ConsecutivoCompania" & vbCrLf
   vSql = vSql & " WHERE Factura.EsDiferida = " & gUtilSQL.fBooleanToSqlValue(False) & vbCrLf
   vSql = vSql & " AND Factura.EsOriginalmenteDiferida = " & gUtilSQL.fBooleanToSqlValue(True) & vbCrLf
   vSql = vSql & " AND Cobranza.StatusCobranza = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCobranza.eSC_VIGENTE) & vbCrLf
   vSql = vSql & " GROUP BY DocumentoCobrado.ConsecutivoCompania, Cxc.NumeroDocumentoOrigen) AS CobranzaFactura" & vbCrLf
   vSql = vSql & " ON Factura.Numero = CobranzaFactura.NumeroFacturaOrigen" & vbCrLf
   vSql = vSql & " AND Factura.ConsecutivoCompania = CobranzaFactura.ConsecutivoCompania" & vbCrLf
   If valEsTipoResumenDiario Then
      vSql = vSql & " WHERE (Factura.TipoDeDocumento = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
      vSql = vSql & " AND Factura.RealizoCierreZ = " & gUtilSQL.fBooleanToSqlValue(False) & ")"
   Else
      vSql = vSql & " WHERE (Factura.StatusFactura = '" & gConvert.enumerativoAChar(enum_StatusFactura.eSF_EMITIDA) & "'"
      vSql = vSql & " OR Factura.StatusFactura = '" & gConvert.enumerativoAChar(enum_StatusFactura.eSF_ANULADA) & "')"
   End If
   vSql = vSql & " AND Factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_NOTAENTREGA)
h_EXIT:     On Error GoTo 0
   fSqlVentasDetalladoSeccionUnion = vSql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlVentasDetalladoSeccionUnion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
