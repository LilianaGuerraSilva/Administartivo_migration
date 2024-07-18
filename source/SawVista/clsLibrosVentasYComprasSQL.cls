VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLibrosVentasYComprasSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsInfLibrosVentasYComprasSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Libros Ventas Y Compras"
Private Const CM_MODULO_CxP As Integer = 1
Private Const CM_MODULO_CxC As Integer = 2
Private Const CM_OPT_LIBRO_DE_VENTAS_FORMALES  As Integer = 0
Private Const CM_OPT_LIBRO_DE_COMPRAS_FORMALES  As Integer = 1
Private Const CM_OPT_LIBRO_DE_COMPRAS  As Integer = 2
Private Const CM_OPT_LIBRO_DE_VENTAS  As Integer = 3
Private Const TOP_FECHA_INICIO = 0
Private Const TOP_FECHA_FIN = 720

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLLibroDeComprasNAlicuotas(ByVal ConsecutivoCompania As Long, ByVal valMes As String, ByVal valAno As String, ByVal gMonedaLocalActual As Object, ByVal valFechaDesde As Date, ByVal valFechaHasta As Date, Optional ByVal valAplicaContribuyentesEspeciales As Boolean = False) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT proveedor.CodigoProveedor AS Codigo, "
   SQL = SQL & "NombreProveedor, "
   SQL = SQL & "NumeroRIF, "
   SQL = SQL & "Fecha, "
   SQL = SQL & "Numero, "
   SQL = SQL & "Status, "
   SQL = SQL & "TipoDeCompra, "
   SQL = SQL & "CreditoFiscal, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "((MontoExento + MontoGravado + MontoIva ) * CambioABolivares )", "cxP.Fecha") & " AS Monto, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "(MontoExento * CambioABolivares )", "cxP.Fecha") & " AS MontoExentoConMoneda, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "( MontoIva * CambioABolivares ) ", "cxP.Fecha") & " AS MontoIVAConMoneda, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "( MontoGravado * CambioABolivares )", "cxP.Fecha") & " AS MontoGravadoConMoneda, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "(MontoGravableAlicuotaGeneral * CambioABolivares)", "cxP.Fecha") & " AS MontoGravableAlicuotaGeneralxCambio, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "(MontoGravableAlicuota2 * CambioABolivares)", "cxP.Fecha") & " AS MontoGravableAlicuota2xCambio, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "(MontoGravableAlicuota3 * CambioABolivares)", "cxP.Fecha") & " AS MontoGravableAlicuota3xCambio, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "(MontoIVAAlicuotaGeneral * CambioABolivares)", "cxP.Fecha") & " AS MontoIVAAlicuotaGeneralxCambio, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "(MontoIVAAlicuota2 * CambioABolivares)", "cxP.Fecha") & " AS MontoIVAAlicuota2xCambio, "
   SQL = SQL & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)), "(MontoIVAAlicuota3 * CambioABolivares)", "cxP.Fecha") & " AS MontoIVAAlicuota3xCambio "
   SQL = SQL & " FROM proveedor INNER JOIN cxP" _
         & " ON proveedor.ConsecutivoCompania" _
         & " = cxP.ConsecutivoCompania" _
         & " AND proveedor.CodigoProveedor" _
         & " = cxP.CodigoProveedor"
   SQL = SQL & " WHERE proveedor.ConsecutivoCompania = " & ConsecutivoCompania
   
   If valAplicaContribuyentesEspeciales Then
      SQL = SQL & " AND CONVERT(DATETIME, " & gUtilSQL.fCast("cxP.DiaDeAplicacion", eTDSS_VARCHAR, "") & " + " & gUtilSQL.fSimpleSqlValue("/") & " + " & gUtilSQL.fCast("cxP.MesDeAplicacion", eTDSS_VARCHAR, "") & _
      " + " & gUtilSQL.fSimpleSqlValue("/") & "+" & gUtilSQL.fCast("cxP.AnoDeAplicacion", eTDSS_VARCHAR, "") & ", 103) BETWEEN " & gUtilSQL.fSimpleSqlValue(valFechaDesde) & " AND " & gUtilSQL.fSimpleSqlValue(valFechaHasta)
   Else
      SQL = SQL & " AND MesDeAplicacion = " & valMes
      SQL = SQL & " AND AnoDeAplicacion = " & valAno
   End If
   SQL = SQL & " AND AplicaParaLibrodeCompras = 'S'"
   SQL = SQL & " ORDER BY Fecha, Numero"
   fSQLLibroDeComprasNAlicuotas = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLLibroDeComprasNAlicuotas", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLLibroDeComprasEnXLSOrdinarios(ByVal valMesStr As String, ByVal valAnoStr As String, ByVal valSoloAjuste As Boolean, ByVal porcentajeDeProrrateo As Currency, ByRef gMonedaLocalActual As Object, ByRef insAlicuotaIva As Object, ByVal valConsecutivoCompania As Long, ByVal UsarOpcionesDeContribuyenteEspecial As Boolean, ByVal valUsaRetencion As Boolean) As String
   Dim SQL As String
   Dim EsNoDeducibleONoEsCompraNiInternaNiImportacion As String
   Dim totalMontoExentoGravadoIva As String
   Dim montoExento As String
   Dim montoGravado As String
   Dim montoIva As String
   Dim montoGravableGeneral As String
   Dim montoGravable2 As String
   Dim montoGravable3 As String
   Dim montoIvaGeneral As String
   Dim montoIva2 As String
   Dim montoIva3 As String
   Dim tipoDeCompraIgualNacional As String
   Dim tipoDeCompraIgualImportaciones As String
   Dim tipoDeCreditoFiscalDifNoDeducible As String
   Dim usarCambioOriginal As Boolean
   Dim CompraImportacion As String
   Dim CompraNacional As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   usarCambioOriginal = True
   montoIva = "(cxP.MontoIva * cxP.CambioABolivares)"
   montoIva = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoIva, "cxP.Fecha")
   montoGravado = "(cxP.MontoGravado * cxP.CambioABolivares)"
   montoGravado = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoGravado, "cxP.Fecha")
   montoExento = "(cxP.MontoExento * cxP.CambioABolivares)"
   montoExento = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoExento, "cxP.Fecha")
   montoGravableGeneral = "(cxP.MontoGravableAlicuotaGeneral * cxP.CambioABolivares)"
   montoGravableGeneral = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoGravableGeneral, "cxP.Fecha")
   montoGravable2 = "(cxP.MontoGravableAlicuota2 * cxP.CambioABolivares)"
   montoGravable2 = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoGravable2, "cxP.Fecha")
   montoGravable3 = "(cxP.MontoGravableAlicuota3 * cxP.CambioABolivares)"
   montoGravable3 = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoGravable3, "cxP.Fecha")
   montoIvaGeneral = "(cxP.MontoIVAAlicuotaGeneral * cxP.CambioABolivares)"
   montoIvaGeneral = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoIvaGeneral, "cxP.Fecha")
   montoIva2 = "(cxP.MontoIVAAlicuota2 * cxP.CambioABolivares)"
   montoIva2 = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoIva2, "cxP.Fecha")
   montoIva3 = "(cxP" & ".MontoIVAAlicuota3 * cxP.CambioABolivares)"
   montoIva3 = gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.fCodigoMoneda(gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMesStr, valAnoStr, False)), montoIva3, "cxP.Fecha")
   EsNoDeducibleONoEsCompraNiInternaNiImportacion = "((" & _
         "cxP.CreditoFiscal" & _
            " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_NoDeducible) & _
         ") OR ((cxP.TipoDeCompra" & _
            " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASNACIONALES) & _
         ") AND (cxP.TipoDeCompra" & _
            " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & ")))"
   totalMontoExentoGravadoIva = montoExento & " + " & montoGravado & " + " & montoIva
   tipoDeCompraIgualNacional = "cxP.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASNACIONALES)
   tipoDeCompraIgualImportaciones = "cxP.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION)
   tipoDeCreditoFiscalDifNoDeducible = "cxP.CreditoFiscal <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_NoDeducible)
   CompraImportacion = "(cxP.TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & ")"
   CompraNacional = "(cxP.TipoDeCompra <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & ")"
   SQL = "SELECT "
   SQL = SQL & "cxP.Fecha, "
   SQL = SQL & "proveedor.NumeroRIF, "
   SQL = SQL & "proveedor.NombreProveedor, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("proveedor.TipoDeProveedorDeLibrosFiscales", _
               enum_TipoDeProveedorDeLibrosFiscales.eTD_CON_RIF, gEnumProyecto.fEnumProveedorFiscalToStringInArray(True), "TipoDeProvvedor") & ", "
   SQL = SQL & "cxP.NumeroComprobanteRetencion AS NumeroComprobante, "
   SQL = SQL & "cxP.NumeroPlanillaDeImportacion AS NumeroPlanilla, "
   SQL = SQL & "cxP.NumeroExpedienteDeImportacion AS NumeroExpediente, "
   SQL = SQL & "cxP.NumeroDeclaracionAduana AS NumeroDeclaracionAduana, "
   SQL = SQL & "cxP.FechaDeclaracionAduana AS FechaDeclaracionAduana, "
   SQL = SQL & "cxP.UsaPrefijoSerie AS UsaPrefijoSerie, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP" & _
                        " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_FACTURA), _
                        "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " As NumeroDeFactura, "
   SQL = SQL & "cxP.NumeroControl AS NumeroControlFactura, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP" & _
                        " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADEDEBITO), _
                        "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " As NumeroDeNotaDeDebito, "
   SQL = SQL & gUtilSQL.getIIF("cxP.TipoDeCxP" & _
                        " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccion.eTD_NOTADECREDITO), _
                        "cxP.Numero", gUtilSQL.fSimpleSqlValue(""), True) & _
               " As NumeroDeNotaDeCredito, "
   SQL = SQL & gUtilSQL.DfSQLCaseIfForEnum("cxP.TipoDeTransaccion", _
                        enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO, _
                        gEnumProyecto.fEnumTransaccionFiscalToStringInArray(True), "") & _
               " AS TipoDeTransaccion, "
   SQL = SQL & "cxP.NumeroDeFacturaAfectada AS FacturaAfectada, "
   '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/
   'COMPRAS INTERNAS
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, gUtilSQL.getIIF("cxP.Status" & _
                  " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), _
                    totalMontoExentoGravadoIva, "0", True), "0", True) & " AS TotalComprasIncluyendoIva, "
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, _
                        totalMontoExentoGravadoIva, montoExento, True), True), "0", True) & _
               " AS ComprasSinDerechoACreditoFiscal, "
   'BaseImponible
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, "0", montoGravableGeneral, True), True), "0", True) & _
               " AS MontoGravableAlicuotaGeneral, "
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, "0", _
               gUtilSQL.getIIF(CompraNacional, montoGravable2, "0", True), True), True), "0", True) & _
               " AS MontoGravableAlicuota2, "
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, "0", montoGravable3, True), True), "0", True) & _
               " AS MontoGravableAlicuota3, "
   'Alicuota
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               insAlicuotaIva.fSQLSelectAlicuotaGeneral("cxP", "Fecha"), "0", True) & _
               " AS PorcentajeAlicGeneral, "
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               insAlicuotaIva.fSQLSelectAlicuota2("cxP", "Fecha"), "0", True) & _
               " AS PorcentajeAlic2, "
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               insAlicuotaIva.fSQLSelectAlicuota3("cxP", "Fecha"), "0", True) & _
               " AS PorcentajeAlic3, "
   'ImpuestoIVA
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               gUtilSQL.getIIF("cxP.Status" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               montoIvaGeneral, True), "0", True) & _
               " AS MontoIVAAlicuotaGeneral, "
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  montoIva2, True), "0", True) & _
                  " AS MontoIVAAlicuota2, "
   SQL = SQL & gUtilSQL.getIIF(CompraNacional, _
               gUtilSQL.getIIF("cxP.Status" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               montoIva3, True), "0", True) & _
               " AS MontoIVAAlicuota3, "
   '==========================================================
   '*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/*/
   'COMPRAS IMPORTACIÓN
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), _
                    totalMontoExentoGravadoIva, "0", True), "0", True) & " AS TotalImportacionIncluyendoIva, "
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, _
                        totalMontoExentoGravadoIva, montoExento, True), True), "0", True) & _
               " AS ImportacionesSinDerechoACreditoFiscal, "
               
   'BaseImponible
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, "0", montoGravableGeneral, True), True), "0", True) & _
               " AS MontoGravableAlicuotaGeneralIMP, "
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, "0", _
               gUtilSQL.getIIF(CompraImportacion, montoGravable2, "0", True), True), True), "0", True) & _
               " AS MontoGravableAlicuota2IMP, "
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               gUtilSQL.getIIF(EsNoDeducibleONoEsCompraNiInternaNiImportacion, "0", montoGravable3, True), True), "0", True) & _
               " AS MontoGravableAlicuota3IMP, "
   'Alicuota
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               insAlicuotaIva.fSQLSelectAlicuotaGeneral("cxP", "Fecha"), "0", True) & _
               " AS PorcentajeAlicGeneralIMP, "
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               insAlicuotaIva.fSQLSelectAlicuota2("cxP", "Fecha"), "0", True) & _
               " AS PorcentajeAlic2IMP, "
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               insAlicuotaIva.fSQLSelectAlicuota3("cxP", "Fecha"), "0", True) & _
               " AS PorcentajeAlic3IMP, "
   'ImpuestoIVA
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               gUtilSQL.getIIF("cxP.Status" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               montoIvaGeneral, True), "0", True) & _
               " AS MontoIVAAlicuotaGeneralIMP, "
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  montoIva2, True), "0", True) & _
                  " AS MontoIVAAlicuota2IMP, "
   SQL = SQL & gUtilSQL.getIIF(CompraImportacion, _
               gUtilSQL.getIIF("cxP.Status" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               montoIva3, True), "0", True) & _
               " AS MontoIVAAlicuota3IMP, "
   '==========================================================
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
               gUtilSQL.getIIF("cxP.SeHizoLaRetencionIVA" & _
               " = " & gUtilSQL.fBooleanToSqlValue(True), _
               "cxP.MontoRetenido", _
               "0", True), True) & " AS IVARetenidoAlVendedor, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS IVARetenidoATerceros, "
   SQL = SQL & gUtilSQL.fSimpleSqlValue("") & " AS AnticipoDeIVAImportacion, "
   'para totales generales
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualNacional & _
                  " AND " & tipoDeCreditoFiscalDifNoDeducible, montoGravableGeneral, "0", True), True) & _
               " AS MontoGravableAlicuotaGeneralComprasInternas, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualImportaciones & _
                  " AND " & tipoDeCreditoFiscalDifNoDeducible, montoGravableGeneral, "0", True), True) & _
               " AS MontoGravableAlicuotaGeneralComprasImportacion, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualNacional & _
                  " AND " & tipoDeCreditoFiscalDifNoDeducible, montoGravable2, "0", True), True) & _
               " AS MontoGravableAlicuota2ComprasInternas, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualImportaciones & _
                  " AND " & tipoDeCreditoFiscalDifNoDeducible, montoGravable2, "0", True), True) & _
               " AS MontoGravableAlicuota2ComprasImportacion, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualNacional & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoGravable3, "0", True), True) & _
               " AS MontoGravableAlicuota3ComprasInternas, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualImportaciones & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoGravable3, "0", True), True) & _
               " AS MontoGravableAlicuota3ComprasImportacion, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualNacional & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoIvaGeneral, "0", True), True) & _
               " AS MontoIVAAlicuotaGeneralComprasInternas, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualImportaciones & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoIvaGeneral, "0", True), True) & _
               " AS MontoIVAAlicuotaGeneralComprasImportacion, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualNacional & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoIva2, "0", True), True) & _
               " AS MontoIVAAlicuota2ComprasInternas, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualImportaciones & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoIva2, "0", True), True) & _
               " AS MontoIVAAlicuota2ComprasImportacion, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  gUtilSQL.getIIF(tipoDeCompraIgualNacional & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoIva3, "0", True), True) & _
               " AS MontoIVAAlicuota3ComprasInternas, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                     gUtilSQL.getIIF(tipoDeCompraIgualImportaciones & " AND " & tipoDeCreditoFiscalDifNoDeducible, montoIva3, "0", True), True) & _
               " AS MontoIVAAlicuota3ComprasImportacion, "
   SQL = SQL & gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  montoIva, True) & " AS MontoIva, "
   SQL = SQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE), gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  "(" & montoIva & ")", True), "0", True) & " AS MontoDeducible, "
   SQL = SQL & gUtilSQL.getIIF("cxP.CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE), gUtilSQL.getIIF("cxP.Status" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO), "0", _
                  "(" & montoIva & " * " & (porcentajeDeProrrateo / 100) & ")", True), "0", True) & " AS MontoIvaConProrrateo ,"
   SQL = SQL & fSQLTipoDeProveedorParaLibroDeCompra(UsarOpcionesDeContribuyenteEspecial, valUsaRetencion) & " As TipoDeProveedorLibros "
   SQL = SQL & " FROM proveedor"
   SQL = SQL & " INNER JOIN cxP"
   SQL = SQL & " ON proveedor.ConsecutivoCompania"
   SQL = SQL & " = cxP.ConsecutivoCompania"
   SQL = SQL & " AND proveedor.CodigoProveedor"
   SQL = SQL & " = cxP.CodigoProveedor"
   SQL = SQL & " WHERE proveedor.ConsecutivoCompania" & _
         " = " & valConsecutivoCompania
   SQL = SQL & " AND cxP.MesDeAplicacion" & _
         " = " & gConvert.fConvertStringToLong(valMesStr)
   SQL = SQL & " AND cxP.AnoDeAplicacion" & _
         " = " & gConvert.fConvertStringToLong(valAnoStr)
   SQL = SQL & " AND cxP.AplicaParaLibrodeCompras" & _
         " = " & gUtilSQL.fSimpleSqlValue(gConvert.ConvertBooleanToString(True))
   If valSoloAjuste Then
      SQL = SQL & " AND cxP.TipoDeTransaccion" & _
         " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE)
   Else
      SQL = SQL & " AND cxP.TipoDeTransaccion" & _
         " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE)
   End If
   SQL = SQL & " ORDER BY "
   SQL = SQL & "cxP.Fecha, "
   SQL = SQL & "cxP.AnoDeAplicacion, "
   SQL = SQL & "cxP.MesDeAplicacion, "
   SQL = SQL & "cxP.Numero"
   fSQLLibroDeComprasEnXLSOrdinarios = SQL
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLLibroDeComprasEnXLSOrdinarios", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLTipoDeProveedorParaLibroDeCompra(ByVal UsarOpcionesDeContribuyenteEspecial As Boolean, ByVal valUsaRetencion As Boolean) As String
   Dim SQL As String
   Dim sqlNumRif As String
   On Error GoTo h_ERROR
   sqlNumRif = "proveedor.NumeroRIF"
   sqlNumRif = sqlNumRif & " = " & gUtilSQL.fSimpleSqlValue("") & " OR "
   sqlNumRif = sqlNumRif & gUtilSQL.DfSQLIsNull("proveedor.NumeroRIF")
   sqlNumRif = sqlNumRif & " OR " & gUtilSQL.fLen("proveedor", "NumeroRIF")
   sqlNumRif = sqlNumRif & " = 0 "
   If UsarOpcionesDeContribuyenteEspecial Or valUsaRetencion Then
      If UsarOpcionesDeContribuyenteEspecial Then
         SQL = gUtilSQL.getIIF(sqlNumRif & " OR proveedor.TipoDeProveedorDeLibrosFiscales = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProveedorDeLibrosFiscales.eTD_SIN_RIF), gUtilSQL.fSimpleSqlValue("SR"), _
         gUtilSQL.getIIF("proveedor.TipoDeProveedorDeLibrosFiscales = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_RESIDENCIADO), gUtilSQL.fSimpleSqlValue("NR"), _
            gUtilSQL.getIIF("proveedor.TipoDeProveedorDeLibrosFiscales = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeProveedorDeLibrosFiscales.eTD_NO_DOMICILIADO), gUtilSQL.fSimpleSqlValue("ND"), _
         gUtilSQL.fSimpleSqlValue(""), True), True), True)
      Else
         SQL = gUtilSQL.getIIF(sqlNumRif, gUtilSQL.fSimpleSqlValue("SR"), _
         gUtilSQL.getIIF("proveedor.TipoDePersona = " & _
            gUtilSQL.fSQLSimpleValueForEnum(enum_TipodePersonaRetencion.eTP_PN_NORESIDENTE), gUtilSQL.fSimpleSqlValue("NR"), _
         gUtilSQL.getIIF("proveedor.TipoDePersona = " & _
            gUtilSQL.fSQLSimpleValueForEnum(enum_TipodePersonaRetencion.eTP_PJ_NODOMICILIADA), gUtilSQL.fSimpleSqlValue("ND"), _
         gUtilSQL.fSimpleSqlValue(""), True), True), True)
      End If
   Else
      SQL = gUtilSQL.getIIF(sqlNumRif, gUtilSQL.fSimpleSqlValue("SR"), gUtilSQL.fSimpleSqlValue(""), True)
   End If
h_EXIT: On Error GoTo 0
   fSQLTipoDeProveedorParaLibroDeCompra = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLTipoDeProveedorParaLibroDeCompra", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLRegistroDeVentasPeru(ByVal valMesInicialStr As String, _
                                          ByVal valAnoInicialStr As String, _
                                           ByVal valMesFinalStr As String, _
                                            ByVal valAnoFinalStr As String, _
                                             ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim SQLViewName As String
   Dim insLibroVCVista As clsLibrosVentasYComprasVista
   On Error GoTo h_ERROR
   Set insLibroVCVista = New clsLibrosVentasYComprasVista
   SQLViewName = insLibroVCVista.GetViewNameRegistroVentasPeru
   
   SQL = "SELECT "
   SQL = SQL & "1 AS ParaContadorRegistro,"
   SQL = SQL & SQLViewName & ".Numero, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL(SQLViewName & ".Numero", "-") & " - 1 > 0", gUtilSQL.fMid(SQLViewName & ".Numero", "1", gUtilSQL.DfInStrSQL(SQLViewName & ".Numero", "-") & "-1"), gUtilSQL.fSimpleSqlValue(""), True) & " AS SerieDeDocumento, "
   SQL = SQL & gUtilSQL.fMid(SQLViewName & ".Numero", gUtilSQL.DfInStrSQL(SQLViewName & ".Numero", "-") & "+1", gUtilSQL.fLen(SQLViewName, "Numero")) & " AS NumeroDeDocumento, "
   SQL = SQL & SQLViewName & ".Fecha, "
   SQL = SQL & gUtilSQL.fIfResultIsNullReplace(SQLViewName & ".FechaDeVencimiento", SQLViewName & ".Fecha") & "As FechaDeVencimiento, "
   SQL = SQL & SQLViewName & ".CodPeruTipoDocumentoFactura, "
   SQL = SQL & SQLViewName & ".CodPeruTipoDocumentoIdentificacion, "
   SQL = SQL & SQLViewName & ".NumeroRIF, "
   SQL = SQL & SQLViewName & ".Nombre, "
   SQL = SQL & SQLViewName & ".MontoExportacion , "
   SQL = SQL & SQLViewName & ".TotalBaseImponible, "
   SQL = SQL & SQLViewName & ".TotalMontoExento, "
   SQL = SQL & SQLViewName & ".MontoIVAAlicuota1, "
   SQL = SQL & SQLViewName & ".MontoIVAAlicuota3, "
   SQL = SQL & SQLViewName & ".TotalFactura, "
   SQL = SQL & SQLViewName & ".CambioABolivares, "
   SQL = SQL & SQLViewName & ".NumeroFacturaAfectada, "

   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fCompareWithNull(SQLViewName & ".NumeroFacturaAfectada", True) & _
                              " OR " & gUtilSQL.fLen(SQLViewName, "NumeroFacturaAfectada") & " = 0 ", _
                              gUtilSQL.fSimpleSqlValue(""), _
                              gUtilSQL.fMid(SQLViewName & ".NumeroFacturaAfectada", "1", gUtilSQL.DfInStrSQL(SQLViewName & ".NumeroFacturaAfectada", "-") & "-1"), True) & " AS SerieDocumentoAfectado, "
   
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fCompareWithNull(SQLViewName & ".NumeroFacturaAfectada", True) & _
                              " OR " & gUtilSQL.fLen(SQLViewName, "NumeroFacturaAfectada") & " = 0 ", _
                              gUtilSQL.fSimpleSqlValue(""), _
                              gUtilSQL.fMid(SQLViewName & ".NumeroFacturaAfectada", gUtilSQL.DfInStrSQL(SQLViewName & ".NumeroFacturaAfectada", "-") & "+1", gUtilSQL.fLen(SQLViewName, "NumeroFacturaAfectada")), True) & " AS NumeroDocumentoAfectado, "

   SQL = SQL & SQLViewName & ".FechaFacturaAfectada, "
   SQL = SQL & SQLViewName & ".TipoDeDocumentoOriginal "
   SQL = SQL & " FROM " & SQLViewName
   SQL = SQL & " WHERE "
   SQL = SQL & "(" & SQLViewName & ".ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & gUtilSQL.fSQLMonthAndYearValueBetween(gUtilSQL.DfSQLMonthOfDate(SQLViewName & ".Fecha", ""), _
                        gUtilSQL.DfSQLYearOfDate(SQLViewName & ".Fecha", ""), _
                        valMesInicialStr, valAnoInicialStr, valMesFinalStr, valAnoFinalStr) & ")"
   SQL = SQL & " ORDER BY "
   SQL = SQL & SQLViewName & ".Fecha, "
   SQL = SQL & SQLViewName & ".CodPeruTipoDocumentoFactura "
   
   Set insLibroVCVista = Nothing
h_EXIT:    On Error GoTo 0
   fSQLRegistroDeVentasPeru = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLRegistroDeVentasPeru", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSQLRegistroDeComprasPeru(ByVal valMesInicialStr As String, _
                                          ByVal valAnoInicialStr As String, _
                                           ByVal valMesFinalStr As String, _
                                            ByVal valAnoFinalStr As String, _
                                             ByVal valConsecutivoCompania As Long) As String
   Dim SQL As String
   Dim SQLRegistroDeCompras As String
   Dim insLibroVCVista As clsLibrosVentasYComprasVista
   On Error GoTo h_ERROR
   Set insLibroVCVista = New clsLibrosVentasYComprasVista
   SQLRegistroDeCompras = insLibroVCVista.GetViewNameRegistroComprasPeru
   
   SQL = "SELECT "
   SQL = SQL & "1 AS ParaContadorRegistro,"
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL(SQLRegistroDeCompras & ".Numero", "-") & " - 1 > 0", gUtilSQL.fMid(SQLRegistroDeCompras & ".Numero", "1", gUtilSQL.DfInStrSQL(SQLRegistroDeCompras & ".Numero", "-") & "-1"), gUtilSQL.fSimpleSqlValue(""), True) & " AS SerieDeDocumento, "
   SQL = SQL & gUtilSQL.fMid(SQLRegistroDeCompras & ".Numero", gUtilSQL.DfInStrSQL(SQLRegistroDeCompras & ".Numero", "-") & "+1", gUtilSQL.fLen(SQLRegistroDeCompras, "Numero")) & " AS NumeroDeDocumento, "
   SQL = SQL & SQLRegistroDeCompras & ".Fecha, "
   SQL = SQL & SQLRegistroDeCompras & ".FechaVencimiento, "
   SQL = SQL & SQLRegistroDeCompras & ".TipoDocumento, "
   SQL = SQL & SQLRegistroDeCompras & ".CodPeruTipoDocumentoIdentificacion, "
   SQL = SQL & SQLRegistroDeCompras & ".NumeroRIF, "
   SQL = SQL & SQLRegistroDeCompras & ".NombreProveedor, "
   SQL = SQL & SQLRegistroDeCompras & ".BaseImponibleOpGrav, "
   SQL = SQL & SQLRegistroDeCompras & ".MontoAlicuotaOpGrav, "
   SQL = SQL & SQLRegistroDeCompras & ".BaseImponibleOpGravYNoGrav, "
   SQL = SQL & SQLRegistroDeCompras & ".MontoAlicuotaOpGravYNoGrav, "
   SQL = SQL & SQLRegistroDeCompras & ".BaseImponibleOpNoGrav, "
   SQL = SQL & SQLRegistroDeCompras & ".MontoAlicuotaOpNoGrav, "
   SQL = SQL & SQLRegistroDeCompras & ".MontoExento, "
   SQL = SQL & SQLRegistroDeCompras & ".MontoIvaalicuota3, "
   SQL = SQL & SQLRegistroDeCompras & ".ImporteTotal, "
   SQL = SQL & SQLRegistroDeCompras & ".CambioABolivares, "
   'SQL = SQL & SQLRegistroDeCompras & ".FechaDetraccion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fLen(SQLRegistroDeCompras, "NumeroDetraccion") & " > 0 ", gUtilSQL.fCast("Day(FechaDetraccion)", eTDSS_VARCHAR, "") & " + " & gUtilSQL.fSimpleSqlValue("/") & " + " & gUtilSQL.fCast("Month(FechaDetraccion)", eTDSS_VARCHAR, "") & " + " & gUtilSQL.fSimpleSqlValue("/") & " + " & gUtilSQL.DfRightSQL(gUtilSQL.fCast("Year(FechaDetraccion)", eTDSS_VARCHAR, ""), 2), gUtilSQL.fSimpleSqlValue(""), True) & " AS FechaDetraccion,"
   SQL = SQL & SQLRegistroDeCompras & ".NumeroDetraccion, "
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fCompareWithNull(SQLRegistroDeCompras & ".NumeroDeFacturaAfectada", True) & _
                              " OR " & gUtilSQL.fLen(SQLRegistroDeCompras, "NumeroDeFacturaAfectada") & " = 0 ", _
                              gUtilSQL.fSimpleSqlValue(""), _
                              gUtilSQL.getIIF(gUtilSQL.DfInStrSQL(SQLRegistroDeCompras & ".NumeroDeFacturaAfectada", "-") & " - 1 > 0", gUtilSQL.fMid(SQLRegistroDeCompras & ".NumeroDeFacturaAfectada", "1", gUtilSQL.DfInStrSQL(SQLRegistroDeCompras & ".NumeroDeFacturaAfectada", "-") & "-1"), gUtilSQL.fSimpleSqlValue(""), True)) & " AS SerieDocumentoAfectado, "
                              'FALTO ENCERRAR ENTRE PARENTESIS
   
   SQL = SQL & gUtilSQL.getIIF(gUtilSQL.fCompareWithNull(SQLRegistroDeCompras & ".NumeroDeFacturaAfectada", True) & _
                              " OR " & gUtilSQL.fLen(SQLRegistroDeCompras, "NumeroDeFacturaAfectada") & " = 0 ", _
                              gUtilSQL.fSimpleSqlValue(""), _
                              gUtilSQL.fMid(SQLRegistroDeCompras & ".NumeroDeFacturaAfectada", gUtilSQL.DfInStrSQL(SQLRegistroDeCompras & ".NumeroDeFacturaAfectada", "-") & "+1", gUtilSQL.fLen(SQLRegistroDeCompras, "NumeroDeFacturaAfectada")), True) & " AS NumeroDocumentoAfectado, "
   SQL = SQL & SQLRegistroDeCompras & ".FechaDocumentoOriginal, "
   SQL = SQL & SQLRegistroDeCompras & ".TipoDocumentoOriginal "
   SQL = SQL & " FROM " & SQLRegistroDeCompras
   SQL = SQL & " WHERE "
   SQL = SQL & "(" & SQLRegistroDeCompras & ".ConsecutivoCompania = " & valConsecutivoCompania & ")"
   SQL = SQL & " AND (" & gUtilSQL.fSQLMonthAndYearValueBetween(gUtilSQL.DfSQLMonthOfDate(SQLRegistroDeCompras & ".Fecha", ""), _
                        gUtilSQL.DfSQLYearOfDate(SQLRegistroDeCompras & ".Fecha", ""), _
                        valMesInicialStr, valAnoInicialStr, valMesFinalStr, valAnoFinalStr) & ")"
   SQL = SQL & " ORDER BY "
   SQL = SQL & SQLRegistroDeCompras & ".Fecha, "
   SQL = SQL & SQLRegistroDeCompras & ".TipoDocumento "
   
   Set insLibroVCVista = Nothing
h_EXIT:    On Error GoTo 0
   fSQLRegistroDeComprasPeru = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
       "fSQLRegistroDeComprasPeru", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

