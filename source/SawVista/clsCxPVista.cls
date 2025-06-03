VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxPVista"
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

Public Function GetViewNameComprasGeneradasCxP() As String
   GetViewNameComprasGeneradasCxP = "IGV_ComprasGeneradasCxP"
End Function

'***Probado
Public Function fSqlViewComprasGeneradasCxP() As String
   Dim vSql As String
   Dim sqlTipoDeCompraImportacion As String
   Dim sqlTipoDeCompraNacional As String
   Dim sqlTipoDeCompraExenta As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   sqlTipoDeCompraImportacion = "TipoDeCompra" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION)
   sqlTipoDeCompraNacional = "TipoDeCompra" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASNACIONALES)
   sqlTipoDeCompraExenta = "TipoDeCompra" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASEXENTAS) & "OR TipoDeCompra" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASEXONERADAS)

   vSql = ""
   vSql = "SELECT "
   vSql = vSql & "ConsecutivoCompania, "
   vSql = vSql & "TipoDeCompra, "
   vSql = vSql & "CreditoFiscal, "
   vSql = vSql & "MesDeAplicacion, "
   vSql = vSql & "AnoDeAplicacion, "
   vSql = vSql & "Fecha, "
   vSql = vSql & "Status, "
   vSql = vSql & "TipoDeTransaccion, "
   vSql = vSql & "AplicaParaLibrodeCompras, "
   vSql = vSql & "Moneda, "
   vSql = vSql & gUtilSQL.getIIF(sqlTipoDeCompraExenta, "MontoExento", "0", True) & " AS ComprasExentas, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_NoDeducible) & " AND " & sqlTipoDeCompraNacional, "MontoGravado", "0", True) & " AS ComprasExentasInt, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_NoDeducible) & " AND " & sqlTipoDeCompraImportacion, "MontoGravado", "0", True) & " AS ComprasExentasImp, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraImportacion, "MontoGravableAlicuotaGeneral", "0", True) & " AS ComprasImpBaseAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraImportacion, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasImpIvaAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraNacional, "MontoGravableAlicuotaGeneral", "0", True) & " AS ComprasIntBaseAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraNacional, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasIntIvaAlicuotaGeneral, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE) & " AND " & sqlTipoDeCompraNacional, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasIntGravadasNoGravadasInt, "
   vSql = vSql & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE) & " AND " & sqlTipoDeCompraNacional, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasIntGravadasNoGravadasImp, "
   vSql = vSql & gUtilSQL.getIIF("TipoDeTransaccion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE), "MontoIva", "0", True) & " AS MontoEditableAjusteCredito, "
   vSql = vSql & "CambioABolivares "
   vSql = vSql & "FROM "
   vSql = vSql & "cxP "
   vSql = vSql & "WHERE "
   vSql = vSql & "(TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & "OR TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASNACIONALES) & "OR TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASEXONERADAS) & ")"
   vSql = vSql & "AND (CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " OR CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE) & ")"
   vSql = vSql & "AND Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   vSql = vSql & "AND AplicaParaLibrodeCompras = " & gUtilSQL.fBooleanToSqlValue(True)
   fSqlViewComprasGeneradasCxP = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasGeneradas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCompras() As String
   GetViewNameCompras = "IGV_Compras"
End Function
Public Function fSqlViewCompras() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " SELECT     cxP.ConsecutivoCompania AS ConsecutivoCompania, cxP.Numero AS Numero, cxP.CodigoLote AS NumeroLote,"
   vSql = vSql & " IGV_EnumTipoDocumentoIva.CodTipoDocumentoFacturaSawIva AS TipoDocumento,"
   vSql = vSql & " cxP.TipoDeCompra AS TipoDeCompra,"
   vSql = vSql & " '0' AS StatusCompra, "
   vSql = vSql & " (CASE  WHEN (cxP.OrigenDeLaRetencion  = '1' or (cxP.OrigenDeLaRetencion  = '2' AND cxP.MontoAbonado <> 0)) THEN '1' ELSE '0' END ) AS StatusRetencion, "
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", "cxP.MontoRetenido" & _
                    "," & " 0 ", ",", " <> ", "MontoIvaRetenido, ")
   vSql = vSql & " cxP.TipoDeTransaccion AS TipoDeTransaccion, Fecha AS Fecha,"
   vSql = vSql & " cxP.MesDeAplicacion AS MesDeAplicacion, cxP.AnoDeAplicacion AS AnoDeAplicacion,"
   vSql = vSql & " cxP.DiaDeAplicacion,"
   vSql = vSql & " cxP.CodigoProveedor AS CodigoProveedor, cxP.Observaciones AS Observaciones,"
   vSql = vSql & " cxP.CreditoFiscal AS CreditoFiscal,"
   vSql = vSql & " cxP.PorcentajeRetencionAplicado AS PorcentajeRetencionIva,"
   vSql = vSql & gUtilSQL.fSimpleSqlValue("0") & " AS TipoDeCompraEspecial,"
   vSql = vSql & " cxP.NumeroExpedienteDeImportacion AS NumeroDeExpedienteImpotacion,"
   vSql = vSql & " cxP.NumeroPlanillaDeImportacion AS NumeroDePlanillaImpotacion, cxP.NumeroDeFacturaAfectada AS NumeroFacturaAfectada,"
   vSql = vSql & " cxP.NumeroDeclaracionAduana AS NumeroDeclaracionAduana, cxP.FechaDeclaracionAduana AS FechaDeclaracionAduana, cxP.UsaPrefijoSerie AS UsaPrefijoSerie,"
   vSql = vSql & " cxP.NumeroControl AS NumeroDeControlDeFactura, cxp.NumeroComprobanteRetencion AS ConsecutivoComprobanteRetencion, cxP.FechaAplicacionRetIva AS FechaAplicacionRetencion,"
 
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoExento", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoExento, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuotaGeneral, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuota2, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
            "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                       "," & " 0 ", ",", " <> ", "MontoGravableAlicuota3, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuotaGeneral, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuota2, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
            "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                       "," & " 0 ", ",", " <> ", "MontoIVAAlicuota3, ")
   
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoExento", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoExento, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoGravableAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoGravableAlicuotaGeneral, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoGravableAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoGravableAlicuota2, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoGravableAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoGravableAlicuota3, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoIVAAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoIVAAlicuotaGeneral, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoIVAAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoIVAAlicuota2, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoIVAAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoIVAAlicuota3, "
   vSql = vSql & " cxP.CodigoProveedorOriginalServicio ,cxP.EsUnaCuentaATerceros, "
   vSql = vSql & " cxP.NombreOperador AS NombreOperador, cxP.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSql = vSql & " 0 AS ComplementoConseccomprobanteRet, "
   vSql = vSql & " (SELECT MontoAlicuotaGeneral FROM AlicuotaIva WHERE "
   vSql = vSql & " FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA "
   vSql = vSql & " FROM AlicuotaIva "
   vSql = vSql & " WHERE FechaDeInicioDeVigencia < cxp.Fecha)) As PorcentajeIvaAlicuotaGeneral, "
   vSql = vSql & " (SELECT MontoAlicuota2 FROM AlicuotaIva WHERE "
   vSql = vSql & " FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA "
   vSql = vSql & " FROM AlicuotaIva "
   vSql = vSql & " WHERE FechaDeInicioDeVigencia < cxp.Fecha)) As PorcentajeIvaAlicuota2, "
   vSql = vSql & " (SELECT MontoAlicuota3 FROM AlicuotaIva WHERE "
   vSql = vSql & " FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA "
   vSql = vSql & " FROM AlicuotaIva "
   vSql = vSql & " WHERE FechaDeInicioDeVigencia < cxp.Fecha)) As PorcentajeIvaAlicuota3, "
   vSql = vSql & gUtilSQL.fBooleanToSqlValue(False) & " AS AplicaIva10Porciento, "
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuotaEspecial1", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuotaEspecial1, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuotaEspecial1", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuotaEspecial1, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuotaEspecial2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuotaEspecial2, ")
   vSql = vSql & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuotaEspecial2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuotaEspecial2, ")
   vSql = vSql & " BaseImponibleIGTFML, "
   vSql = vSql & " AlicuotaIGTFML, "
   vSql = vSql & " MontoIGTFML, "
   vSql = vSql & "ROUND((MontoExento + MontoGravableAlicuotaGeneral + MontoIvaalicuotaGeneral + MontoGravableAlicuota2 + MontoIvaalicuota2 + MontoGravableAlicuota3 + MontoIvaalicuota3 ) * CambioAbolivares, 2)+ MontoIGTFML AS TotalComprasIVAeIGTF "
   vSql = vSql & " FROM cxP INNER JOIN IGV_EnumTipoDocumentoIva "
   vSql = vSql & " ON IGV_EnumTipoDocumentoIva.DbValue COLLATE Modern_Spanish_CI_AS = cxP.TipoDeCxp "
   vSql = vSql & " WHERE     (AplicaParaLibrodeCompras = 'S') "
   
   fSqlViewCompras = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCompras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameDocPagadosPorMoneda() As String
   GetViewNameDocPagadosPorMoneda = "IGV_DocPagadosPorMoneda"
End Function
Public Function fSqlViewDocumentoPagado() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSql = ""
   vSql = vSql & " SELECT SUM(DocumentoPagado.MontoAbonado / (CASE DocumentoPagado.CambioAMonedaDelPago WHEN 0 THEN 1 ELSE  CambioAMonedaDelPago END) ) as SumaMoneda, "
   vSql = vSql & " Pago.ConsecutivoCompania, Pago.NumeroComprobante as NumeroPago, Pago.CodigoProveedor, DocumentoPagado.CodigoMonedaDeCxP "
   vSql = vSql & " FROM DocumentoPagado INNER JOIN PAGO ON "
   vSql = vSql & " (Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante) AND "
   vSql = vSql & " (Pago.ConsecutivoCompania = DocumentoPagado.ConsecutivoCompania) "
   vSql = vSql & " WHERE StatusOrdenDePago <> '1' "
   vSql = vSql & " GROUP BY  DocumentoPagado.CodigoMonedaDeCxP, Pago.ConsecutivoCompania, Pago.NumeroComprobante, Pago.CodigoProveedor "
   fSqlViewDocumentoPagado = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlViewDocumentoPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlConvierteMonto(ByVal valValor As String, ByVal valAnoAplicacion As String, ByVal valFecha As String) As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSql = vSql & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" cxp.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * Cxp.CambioABolivares * 0.001 ", False) & ")", valValor & " * Cxp.CambioABolivares")
   'OJO LA FUNCION NO GENERA ERRORES Y DEVUELVE LOS RESULTADO ESPERADOS PERO DEBE SER EVALUADA
   fSqlConvierteMonto = vSql
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlConvierteMonto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function GetViewNameRetencionImpuestoMunicipalCxP() As String
  GetViewNameRetencionImpuestoMunicipalCxP = "IGV_RetencionImpuestoMunicipalCxP"
End Function

Public Function fSqlViewRetencionImpuestoMunicipalCxP() As String
   Dim vSql As String
   On Error GoTo h_ERROR
        vSql = "  "
        vSql = "SELECT "
        vSql = vSql & fSqlSelectCompaniaImpuestoMunicipalCxP() & " , "
        vSql = vSql & fSqlSelectProveedorImpuestoMunicipalCxP() & " , "
        vSql = vSql & fSqlSelectCxPImpuestoMunicipalCxP() & " , "
        vSql = vSql & fSqlSelectRenglonImpuestoImpuestoMunicipalCxP & " , "
        vSql = vSql & "Comun.ClasificadorActividadEconomica.CodigoActividad,"
        vSql = vSql & "Comun.ClasificadorActividadEconomica.Descripcion, "
        vSql = vSql & "Moneda AS MonedaDocumento "
        vSql = vSql & "FROM dbo.cxP "
        vSql = vSql & "INNER JOIN  dbo.RenglonImpuestoMunicipalRet ON "
        vSql = vSql & "dbo.cxP.ConsecutivoCompania = dbo.RenglonImpuestoMunicipalRet.ConsecutivoCompania AND "
        vSql = vSql & "dbo.cxP.ConsecutivoCxp = dbo.RenglonImpuestoMunicipalRet.ConsecutivoCxp "
        vSql = vSql & "INNER JOIN  dbo.Proveedor ON "
        vSql = vSql & "dbo.cxP.ConsecutivoCompania = dbo.Proveedor.ConsecutivoCompania AND "
        vSql = vSql & gUtilSQL.getIIF("CxP.EsUnaCuentaATerceros =" & gUtilSQL.fBooleanToSqlValue(True), "CxP.CodigoProveedorOriginalServicio", "CxP.CodigoProveedor", True) & " = Proveedor.CodigoProveedor "
        vSql = vSql & "INNER JOIN "
        vSql = vSql & "dbo.COMPANIA ON "
        vSql = vSql & "dbo.cxP.ConsecutivoCompania = dbo.COMPANIA.ConsecutivoCompania "
        vSql = vSql & "INNER JOIN "
        vSql = vSql & "Comun.ClasificadorActividadEconomica ON "
        vSql = vSql & "dbo.RenglonImpuestoMunicipalRet.CodigoRetencion = Comun.ClasificadorActividadEconomica.Codigo "
   fSqlViewRetencionImpuestoMunicipalCxP = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRetencionImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlSelectCompaniaImpuestoMunicipalCxP() As String
   Dim vSql As String
   On Error GoTo h_ERROR
        vSql = "  "
        vSql = vSql & "dbo.COMPANIA.ConsecutivoCompania,"
        vSql = vSql & "dbo.COMPANIA.NumeroDeRif AS RifCompania,"
        vSql = vSql & "dbo.COMPANIA.Nombre AS NombreCompania,"
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 2, 8) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 10, 10) & " AS RifCompaniaModelo1,"
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 2, 10) & " AS RifCompaniaModelo2,"
        vSql = vSql & gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", gUtilSQL.fSimpleSqlValue("J"), ""), gUtilSQL.fSimpleSqlValue("V"), ""), "-", "") & " AS RifCompaniaModelo3,"
        vSql = vSql & "dbo.COMPANIA.Direccion,"
        vSql = vSql & "dbo.COMPANIA.Ciudad,"
        vSql = vSql & "dbo.COMPANIA.ConsecutivoMunicipio,"
        vSql = vSql & "dbo.COMPANIA.CodigoContribuyente,"
        vSql = vSql & "dbo.COMPANIA.Estado "
 fSqlSelectCompaniaImpuestoMunicipalCxP = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectCompaniaImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlSelectProveedorImpuestoMunicipalCxP() As String
   Dim vSql As String
   On Error GoTo h_ERROR
        vSql = ""
        vSql = "dbo.Proveedor.CodigoProveedor,"
        vSql = vSql & "dbo.Proveedor.NombreProveedor,"
        vSql = vSql & "dbo.Proveedor.Contacto,"
        vSql = vSql & "dbo.Proveedor.NumeroRIF AS RifProveedor,"
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 2, 8) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 10, 10) & " AS RifProveedorModelo1,"
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSql = vSql & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 2, 10) & " AS RifProveedorModelo2,"
        vSql = vSql & gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", gUtilSQL.fSimpleSqlValue("J"), ""), gUtilSQL.fSimpleSqlValue("V"), ""), "-", "") & " AS RifProveedorModelo3,"
        vSql = vSql & "dbo.Proveedor.Telefonos,"
        vSql = vSql & "dbo.Proveedor.Direccion AS DireccionProveedor ,"
        vSql = vSql & "dbo.Proveedor.Email,"
        vSql = vSql & "dbo.Proveedor.Fax,"
        vSql = vSql & "dbo.Proveedor.CodigoContribuyente AS CodigoContribuyenteProveedor,"
        vSql = vSql & "dbo.Proveedor.NumeroRUC"
        fSqlSelectProveedorImpuestoMunicipalCxP = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectProveedorImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlSelectCxPImpuestoMunicipalCxP() As String
   Dim vSql As String
   Dim vgEnumProyecto As clsEnumAdministrativo
   Dim VsqlTipoDeCxP As String
   On Error GoTo h_ERROR
   Set vgEnumProyecto = New clsEnumAdministrativo
        VsqlTipoDeCxP = gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeCxP", enum_TipoDeTransaccion.eTD_FACTURA, vgEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "")
        vSql = ""
        vSql = vSql & "dbo.cxP.ConsecutivoCxp,"
        vSql = vSql & "dbo.cxP.Numero,"
        vSql = vSql & "dbo.cxP.NumeroControl,"
        vSql = vSql & "dbo.cxP.NumeroControlDeFacturaAfectada,"
        vSql = vSql & "dbo.cxP.Fecha,"
        vSql = vSql & "dbo.cxP.FechaCancelacion,"
        vSql = vSql & "dbo.cxP.FechaAnulacion,"
        vSql = vSql & "dbo.cxP.CreditoFiscal,"
        vSql = vSql & "dbo.cxP.NumeroDeFacturaAfectada,"
        vSql = vSql & "dbo.cxP.TipoDeTransaccion,"
        vSql = vSql & "dbo.cxP.CodigoMoneda,"
        vSql = vSql & "dbo.cxP.TipoDeCxp, "
        vSql = vSql & VsqlTipoDeCxP & "AS TipoCxpString , "
        vSql = vSql & "dbo.cxP.NumeroComprobanteImpuestoMunicipal,"
        vSql = vSql & "dbo.cxP.MontoRetenidoImpuestoMunicipal,"
        vSql = vSql & "dbo.cxP.ImpuestoMunicipalRetenido,"
        vSql = vSql & "dbo.cxP.FechaAplicacionImpuestoMunicipal,"
        vSql = vSql & "dbo.cxP.CambioABolivares,"
        vSql = vSql & "dbo.cxP.MontoExento,"
        vSql = vSql & "dbo.cxP.MontoGravado,"
        vSql = vSql & "dbo.cxP.MontoIva,"
        vSql = vSql & "dbo.cxP.MontoAbonado,"
        vSql = vSql & "dbo.cxP.TotalOtrosImpuestos, "
        vSql = vSql & "dbo.cxP.Status"
        fSqlSelectCxPImpuestoMunicipalCxP = vSql
  Set vgEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectCxPImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlSelectRenglonImpuestoImpuestoMunicipalCxP() As String
   Dim vSql As String
   On Error GoTo h_ERROR
        vSql = ""
        vSql = "dbo.RenglonImpuestoMunicipalRet.Consecutivo,"
        vSql = vSql & "dbo.RenglonImpuestoMunicipalRet.MontoBaseImponible,"
        vSql = vSql & "dbo.RenglonImpuestoMunicipalRet.AlicuotaRetencion,"
        vSql = vSql & "dbo.RenglonImpuestoMunicipalRet.MontoRetencion,"
        vSql = vSql & "dbo.RenglonImpuestoMunicipalRet.TipoDeTransaccion AS TipoDeTransaccionImpuesto "
       fSqlSelectRenglonImpuestoImpuestoMunicipalCxP = vSql
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectRenglonImpuestoImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
