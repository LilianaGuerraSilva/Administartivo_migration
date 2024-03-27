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
   Dim vSQL As String
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

   vSQL = ""
   vSQL = "SELECT "
   vSQL = vSQL & "ConsecutivoCompania, "
   vSQL = vSQL & "TipoDeCompra, "
   vSQL = vSQL & "CreditoFiscal, "
   vSQL = vSQL & "MesDeAplicacion, "
   vSQL = vSQL & "AnoDeAplicacion, "
   vSQL = vSQL & "Fecha, "
   vSQL = vSQL & "Status, "
   vSQL = vSQL & "TipoDeTransaccion, "
   vSQL = vSQL & "AplicaParaLibrodeCompras, "
   vSQL = vSQL & "Moneda, "
   vSQL = vSQL & gUtilSQL.getIIF(sqlTipoDeCompraExenta, "MontoExento", "0", True) & " AS ComprasExentas, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_NoDeducible) & " AND " & sqlTipoDeCompraNacional, "MontoGravado", "0", True) & " AS ComprasExentasInt, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_NoDeducible) & " AND " & sqlTipoDeCompraImportacion, "MontoGravado", "0", True) & " AS ComprasExentasImp, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraImportacion, "MontoGravableAlicuotaGeneral", "0", True) & " AS ComprasImpBaseAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraImportacion, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasImpIvaAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraNacional, "MontoGravableAlicuotaGeneral", "0", True) & " AS ComprasIntBaseAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " AND " & sqlTipoDeCompraNacional, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasIntIvaAlicuotaGeneral, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE) & " AND " & sqlTipoDeCompraNacional, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasIntGravadasNoGravadasInt, "
   vSQL = vSQL & gUtilSQL.getIIF("CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE) & " AND " & sqlTipoDeCompraNacional, "MontoIVAAlicuotaGeneral", "0", True) & " AS ComprasIntGravadasNoGravadasImp, "
   vSQL = vSQL & gUtilSQL.getIIF("TipoDeTransaccion = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE), "MontoIva", "0", True) & " AS MontoEditableAjusteCredito, "
   vSQL = vSQL & "CambioABolivares "
   vSQL = vSQL & "FROM "
   vSQL = vSQL & "cxP "
   vSQL = vSQL & "WHERE "
   vSQL = vSQL & "(TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION) & "OR TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASNACIONALES) & "OR TipoDeCompra = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASEXONERADAS) & ")"
   vSQL = vSQL & "AND (CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " OR CreditoFiscal = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE) & ")"
   vSQL = vSQL & "AND Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   vSQL = vSQL & "AND AplicaParaLibrodeCompras = " & gUtilSQL.fBooleanToSqlValue(True)
   fSqlViewComprasGeneradasCxP = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewComprasGeneradas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameCompras() As String
   GetViewNameCompras = "IGV_Compras"
End Function
Public Function fSqlViewCompras() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = " SELECT     cxP.ConsecutivoCompania AS ConsecutivoCompania, cxP.Numero AS Numero, cxP.CodigoLote AS NumeroLote,"
   vSQL = vSQL & " IGV_EnumTipoDocumentoIva.CodTipoDocumentoFacturaSawIva AS TipoDocumento,"
   vSQL = vSQL & " cxP.TipoDeCompra AS TipoDeCompra,"
   vSQL = vSQL & " '0' AS StatusCompra, "
   vSQL = vSQL & " (CASE  WHEN (cxP.OrigenDeLaRetencion  = '1' or (cxP.OrigenDeLaRetencion  = '2' AND cxP.MontoAbonado <> 0)) THEN '1' ELSE '0' END ) AS StatusRetencion, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", "cxP.MontoRetenido" & _
                    "," & " 0 ", ",", " <> ", "MontoIvaRetenido, ")
   vSQL = vSQL & " cxP.TipoDeTransaccion AS TipoDeTransaccion, Fecha AS Fecha,"
   vSQL = vSQL & " cxP.MesDeAplicacion AS MesDeAplicacion, cxP.AnoDeAplicacion AS AnoDeAplicacion,"
   vSQL = vSQL & " cxP.DiaDeAplicacion,"
   vSQL = vSQL & " cxP.CodigoProveedor AS CodigoProveedor, cxP.Observaciones AS Observaciones,"
   vSQL = vSQL & " cxP.CreditoFiscal AS CreditoFiscal,"
   vSQL = vSQL & " cxP.PorcentajeRetencionAplicado AS PorcentajeRetencionIva,"
   vSQL = vSQL & gUtilSQL.fSimpleSqlValue("0") & " AS TipoDeCompraEspecial,"
   vSQL = vSQL & " cxP.NumeroExpedienteDeImportacion AS NumeroDeExpedienteImpotacion,"
   vSQL = vSQL & " cxP.NumeroPlanillaDeImportacion AS NumeroDePlanillaImpotacion, cxP.NumeroDeFacturaAfectada AS NumeroFacturaAfectada,"
   vSQL = vSQL & " cxP.NumeroDeclaracionAduana AS NumeroDeclaracionAduana, cxP.FechaDeclaracionAduana AS FechaDeclaracionAduana, cxP.UsaPrefijoSerie AS UsaPrefijoSerie,"
   vSQL = vSQL & " cxP.NumeroControl AS NumeroDeControlDeFactura, cxp.NumeroComprobanteRetencion AS ConsecutivoComprobanteRetencion, cxP.FechaAplicacionRetIva AS FechaAplicacionRetencion,"
 
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoExento", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoExento, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuotaGeneral, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuota2, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
            "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                       "," & " 0 ", ",", " <> ", "MontoGravableAlicuota3, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuotaGeneral, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuota2, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
            "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                       "," & " 0 ", ",", " <> ", "MontoIVAAlicuota3, ")
   
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoExento", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoExento, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoGravableAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoGravableAlicuotaGeneral, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoGravableAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoGravableAlicuota2, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoGravableAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoGravableAlicuota3, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoIVAAlicuotaGeneral", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoIVAAlicuotaGeneral, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoIVAAlicuota2", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoIVAAlicuota2, "
'   vSQL = vSQL & fSqlConvierteMonto("cxP.MontoIVAAlicuota3", "cxP.AnoDeAplicacion", "cxP.Fecha") & " AS MontoIVAAlicuota3, "
   vSQL = vSQL & " cxP.CodigoProveedorOriginalServicio ,cxP.EsUnaCuentaATerceros, "
   vSQL = vSQL & " cxP.NombreOperador AS NombreOperador, cxP.FechaUltimaModificacion AS FechaUltimaModificacion,"
   vSQL = vSQL & " 0 AS ComplementoConseccomprobanteRet, "
   vSQL = vSQL & " (SELECT MontoAlicuotaGeneral FROM AlicuotaIva WHERE "
   vSQL = vSQL & " FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA "
   vSQL = vSQL & " FROM AlicuotaIva "
   vSQL = vSQL & " WHERE FechaDeInicioDeVigencia < cxp.Fecha)) As PorcentajeIvaAlicuotaGeneral, "
   vSQL = vSQL & " (SELECT MontoAlicuota2 FROM AlicuotaIva WHERE "
   vSQL = vSQL & " FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA "
   vSQL = vSQL & " FROM AlicuotaIva "
   vSQL = vSQL & " WHERE FechaDeInicioDeVigencia < cxp.Fecha)) As PorcentajeIvaAlicuota2, "
   vSQL = vSQL & " (SELECT MontoAlicuota3 FROM AlicuotaIva WHERE "
   vSQL = vSQL & " FechaDeInicioDeVigencia= (SELECT MAX(FechaDeInicioDeVigencia)AS FECHA "
   vSQL = vSQL & " FROM AlicuotaIva "
   vSQL = vSQL & " WHERE FechaDeInicioDeVigencia < cxp.Fecha)) As PorcentajeIvaAlicuota3, "
   vSQL = vSQL & gUtilSQL.fBooleanToSqlValue(False) & " AS AplicaIva10Porciento, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuotaEspecial1", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuotaEspecial1, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuotaEspecial1", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuotaEspecial1, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoGravableAlicuotaEspecial2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoGravableAlicuotaEspecial2, ")
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("Cxp.Status", _
         "'" & gConvert.enumerativoAChar(enum_StatusDocumento.eSD_ANULADO) & "'", fSqlConvierteMonto("cxP.MontoIVAAlicuotaEspecial2", "cxP.AnoDeAplicacion", "cxP.Fecha") & _
                    "," & " 0 ", ",", " <> ", "MontoIVAAlicuotaEspecial2, ")
   vSQL = vSQL & " cxP.BaseImponibleIGTFML, "
   vSQL = vSQL & " cxP.AlicuotaIGTFML, "
   vSQL = vSQL & " cxP.MontoIGTFML "
   vSQL = vSQL & " FROM cxP INNER JOIN IGV_EnumTipoDocumentoIva "
   vSQL = vSQL & " ON IGV_EnumTipoDocumentoIva.DbValue COLLATE Modern_Spanish_CI_AS = cxP.TipoDeCxp "
   vSQL = vSQL & " WHERE     (AplicaParaLibrodeCompras = 'S') "
   
   fSqlViewCompras = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewCompras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameDocPagadosPorMoneda() As String
   GetViewNameDocPagadosPorMoneda = "IGV_DocPagadosPorMoneda"
End Function
Public Function fSqlViewDocumentoPagado() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = ""
   vSQL = vSQL & " SELECT SUM(DocumentoPagado.MontoAbonado / (CASE DocumentoPagado.CambioAMonedaDelPago WHEN 0 THEN 1 ELSE  CambioAMonedaDelPago END) ) as SumaMoneda, "
   vSQL = vSQL & " Pago.ConsecutivoCompania, Pago.NumeroComprobante as NumeroPago, Pago.CodigoProveedor, DocumentoPagado.CodigoMonedaDeCxP "
   vSQL = vSQL & " FROM DocumentoPagado INNER JOIN PAGO ON "
   vSQL = vSQL & " (Pago.NumeroComprobante = DocumentoPagado.NumeroComprobante) AND "
   vSQL = vSQL & " (Pago.ConsecutivoCompania = DocumentoPagado.ConsecutivoCompania) "
   vSQL = vSQL & " WHERE StatusOrdenDePago <> '1' "
   vSQL = vSQL & " GROUP BY  DocumentoPagado.CodigoMonedaDeCxP, Pago.ConsecutivoCompania, Pago.NumeroComprobante, Pago.CodigoProveedor "
   fSqlViewDocumentoPagado = vSQL
   Set gEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSqlViewDocumentoPagado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Private Function fSqlConvierteMonto(ByVal valValor As String, ByVal valAnoAplicacion As String, ByVal valFecha As String) As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = ""
   ' los datos referentes a las fechas y el codigo de la moneda deben hacerse con la clases moneda local
   ' de forma tal que si llegase a ocurir otro proceso de reconversion el proceso continue funcionando
   ' de forma correcta
   vSQL = vSQL & gUtilSQL.getIIF("((" & valFecha & " <= '31/12/2007' AND " & valAnoAplicacion & " >= '2008'))", _
                           "(" & gUtilSQL.getIIF(" cxp.CodigoMoneda = 'VEB' ", valValor & " * 0.001 ", valValor & " * Cxp.CambioABolivares * 0.001 ", False) & ")", valValor & " * Cxp.CambioABolivares")
   'OJO LA FUNCION NO GENERA ERRORES Y DEVUELVE LOS RESULTADO ESPERADOS PERO DEBE SER EVALUADA
   fSqlConvierteMonto = vSQL
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
   Dim vSQL As String
   On Error GoTo h_ERROR
        vSQL = "  "
        vSQL = "SELECT "
        vSQL = vSQL & fSqlSelectCompaniaImpuestoMunicipalCxP() & " , "
        vSQL = vSQL & fSqlSelectProveedorImpuestoMunicipalCxP() & " , "
        vSQL = vSQL & fSqlSelectCxPImpuestoMunicipalCxP() & " , "
        vSQL = vSQL & fSqlSelectRenglonImpuestoImpuestoMunicipalCxP & " , "
        vSQL = vSQL & "Comun.ClasificadorActividadEconomica.CodigoActividad,"
        vSQL = vSQL & "Comun.ClasificadorActividadEconomica.Descripcion "
        vSQL = vSQL & "FROM dbo.cxP "
        vSQL = vSQL & "INNER JOIN  dbo.RenglonImpuestoMunicipalRet ON "
        vSQL = vSQL & "dbo.cxP.ConsecutivoCompania = dbo.RenglonImpuestoMunicipalRet.ConsecutivoCompania AND "
        vSQL = vSQL & "dbo.cxP.ConsecutivoCxp = dbo.RenglonImpuestoMunicipalRet.ConsecutivoCxp "
        vSQL = vSQL & "INNER JOIN  dbo.Proveedor ON "
        vSQL = vSQL & "dbo.cxP.ConsecutivoCompania = dbo.Proveedor.ConsecutivoCompania AND "
        vSQL = vSQL & "dbo.cxP.CodigoProveedor = dbo.Proveedor.CodigoProveedor "
        vSQL = vSQL & "INNER JOIN "
        vSQL = vSQL & "dbo.COMPANIA ON "
        vSQL = vSQL & "dbo.cxP.ConsecutivoCompania = dbo.COMPANIA.ConsecutivoCompania "
        vSQL = vSQL & "INNER JOIN "
        vSQL = vSQL & "Comun.ClasificadorActividadEconomica ON "
        vSQL = vSQL & "dbo.RenglonImpuestoMunicipalRet.CodigoRetencion = Comun.ClasificadorActividadEconomica.Codigo "
  fSqlViewRetencionImpuestoMunicipalCxP = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewRetencionImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlSelectCompaniaImpuestoMunicipalCxP() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
        vSQL = "  "
        vSQL = vSQL & "dbo.COMPANIA.ConsecutivoCompania,"
        vSQL = vSQL & "dbo.COMPANIA.NumeroDeRif AS RifCompania,"
        vSQL = vSQL & "dbo.COMPANIA.Nombre AS NombreCompania,"
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 2, 8) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 10, 10) & " AS RifCompaniaModelo1,"
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", "-", ""), 2, 10) & " AS RifCompaniaModelo2,"
        vSQL = vSQL & gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace("dbo.COMPANIA.NumeroDeRif", gUtilSQL.fSimpleSqlValue("J"), ""), gUtilSQL.fSimpleSqlValue("V"), ""), "-", "") & " AS RifCompaniaModelo3,"
        vSQL = vSQL & "dbo.COMPANIA.Direccion,"
        vSQL = vSQL & "dbo.COMPANIA.Ciudad,"
        vSQL = vSQL & "dbo.COMPANIA.ConsecutivoMunicipio,"
        vSQL = vSQL & "dbo.COMPANIA.CodigoContribuyente,"
        vSQL = vSQL & "dbo.COMPANIA.Estado "
 fSqlSelectCompaniaImpuestoMunicipalCxP = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectCompaniaImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlSelectProveedorImpuestoMunicipalCxP() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
        vSQL = ""
        vSQL = "dbo.Proveedor.CodigoProveedor,"
        vSQL = vSQL & "dbo.Proveedor.NombreProveedor,"
        vSQL = vSQL & "dbo.Proveedor.Contacto,"
        vSQL = vSQL & "dbo.Proveedor.NumeroRIF AS RifProveedor,"
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 2, 8) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 10, 10) & " AS RifProveedorModelo1,"
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 1, 1) & gUtilSQL.CharConcat & gUtilSQL.fSimpleSqlValue("-") & gUtilSQL.CharConcat
        vSQL = vSQL & gUtilSQL.DfMidSQL(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", "-", ""), 2, 10) & " AS RifProveedorModelo2,"
        vSQL = vSQL & gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace(gUtilSQL.DfSQLReplace("dbo.Proveedor.NumeroRIF", gUtilSQL.fSimpleSqlValue("J"), ""), gUtilSQL.fSimpleSqlValue("V"), ""), "-", "") & " AS RifProveedorModelo3,"
        vSQL = vSQL & "dbo.Proveedor.Telefonos,"
        vSQL = vSQL & "dbo.Proveedor.Direccion AS DireccionProveedor ,"
        vSQL = vSQL & "dbo.Proveedor.Email,"
        vSQL = vSQL & "dbo.Proveedor.Fax,"
        vSQL = vSQL & "dbo.Proveedor.CodigoContribuyente AS CodigoContribuyenteProveedor,"
        vSQL = vSQL & "dbo.Proveedor.NumeroRUC"
        fSqlSelectProveedorImpuestoMunicipalCxP = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectProveedorImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSqlSelectCxPImpuestoMunicipalCxP() As String
   Dim vSQL As String
   Dim vgEnumProyecto As clsEnumAdministrativo
   Dim VsqlTipoDeCxP As String
   On Error GoTo h_ERROR
   Set vgEnumProyecto = New clsEnumAdministrativo
        VsqlTipoDeCxP = gUtilSQL.DfSQLCaseIfForEnum("CxP.TipoDeCxP", enum_TipoDeTransaccion.eTD_FACTURA, vgEnumProyecto.fEnumTipoDeTransaccioToStringInArray(True, False), "")
        vSQL = ""
        vSQL = vSQL & "dbo.cxP.ConsecutivoCxp,"
        vSQL = vSQL & "dbo.cxP.Numero,"
        vSQL = vSQL & "dbo.cxP.NumeroControl,"
        vSQL = vSQL & "dbo.cxP.NumeroControlDeFacturaAfectada,"
        vSQL = vSQL & "dbo.cxP.Fecha,"
        vSQL = vSQL & "dbo.cxP.FechaCancelacion,"
        vSQL = vSQL & "dbo.cxP.FechaAnulacion,"
        vSQL = vSQL & "dbo.cxP.CreditoFiscal,"
        vSQL = vSQL & "dbo.cxP.NumeroDeFacturaAfectada,"
        vSQL = vSQL & "dbo.cxP.TipoDeTransaccion,"
        vSQL = vSQL & "dbo.cxP.CodigoMoneda,"
        vSQL = vSQL & "dbo.cxP.TipoDeCxp, "
        vSQL = vSQL & VsqlTipoDeCxP & "AS TipoCxpString , "
        vSQL = vSQL & "dbo.cxP.NumeroComprobanteImpuestoMunicipal,"
        vSQL = vSQL & "dbo.cxP.MontoRetenidoImpuestoMunicipal,"
        vSQL = vSQL & "dbo.cxP.ImpuestoMunicipalRetenido,"
        vSQL = vSQL & "dbo.cxP.FechaAplicacionImpuestoMunicipal,"
        vSQL = vSQL & "dbo.cxP.CambioABolivares,"
        vSQL = vSQL & "dbo.cxP.MontoExento,"
        vSQL = vSQL & "dbo.cxP.MontoGravado,"
        vSQL = vSQL & "dbo.cxP.MontoIva,"
        vSQL = vSQL & "dbo.cxP.MontoAbonado,"
        vSQL = vSQL & "dbo.cxP.TotalOtrosImpuestos, "
        vSQL = vSQL & "dbo.cxP.Status"
        fSqlSelectCxPImpuestoMunicipalCxP = vSQL
  Set vgEnumProyecto = Nothing
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectCxPImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Public Function fSqlSelectRenglonImpuestoImpuestoMunicipalCxP() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
        vSQL = ""
        vSQL = "dbo.RenglonImpuestoMunicipalRet.Consecutivo,"
        vSQL = vSQL & "dbo.RenglonImpuestoMunicipalRet.MontoBaseImponible,"
        vSQL = vSQL & "dbo.RenglonImpuestoMunicipalRet.AlicuotaRetencion,"
        vSQL = vSQL & "dbo.RenglonImpuestoMunicipalRet.MontoRetencion,"
        vSQL = vSQL & "dbo.RenglonImpuestoMunicipalRet.TipoDeTransaccion AS TipoDeTransaccionImpuesto "
       fSqlSelectRenglonImpuestoImpuestoMunicipalCxP = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlSelectRenglonImpuestoImpuestoMunicipalCxP", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
