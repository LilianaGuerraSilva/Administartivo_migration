VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsPdtSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private gUltimaTasaDeCambio As Object
Private Const CM_FILE_NAME As String = "clsPdtSQL"
Private Const CM_MESSAGE_NAME As String = "SQL PDTs"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fConstruirSQLVentasRentaMensual(ByVal valConsecutivoCompania As Long, ByVal valMes As Integer, ByVal valAno As Integer, ByVal refTotalTributosVenta As Currency, ByRef valUltimaTasaDeCambio As Object) As String
   Dim vSQL As String
   Dim sqlWhere As String
   Dim insPDTVista As clsPDTVista
   On Error GoTo h_ERROR
   Set insPDTVista = New clsPDTVista
   Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
   sqlWhere = "ConsecutivoCompania " & " = " & valConsecutivoCompania
   sqlWhere = sqlWhere & " AND " & gUtilSQL.fYear("Fecha ", "") & " = " & valAno
   sqlWhere = sqlWhere & " AND " & gUtilSQL.fMonth("Fecha ", "") & " = " & valMes
   vSQL = "SELECT "

   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "ventasExentas", True, "") & ") AS ventasExentas, "
   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "VentasInternasNoGravadas", True, "") & ") AS VentasInternasNoGravadas, "
   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "VentasDeExportacion", True, "") & ") AS VentasDeExportacion, "
   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "VentasIntBaseAlicuotaGeneral", True, "") & ") AS VentasIntBaseAlicuotaGeneral, "
   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "VentasIntIvaAlicuotaGeneral", True, "") & ") AS VentasIntIvaAlicuotaGeneral, "
   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "MontoEditableAjusteDebito", True, "") & ") AS MontoEditableAjusteDebito, "
   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "DescRenglonVentasGeneral", True, "") & ") AS DescVentasGeneral, "
   vSQL = vSQL & "SUM (" & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(insPDTVista.GetViewNamePDTVentasRentaMensual & ".CambioABolivares", _
            insPDTVista.GetViewNamePDTVentasRentaMensual & ".Moneda", "DescIvaAlicuotaGeneralVentasInt", True, "") & ") AS DescIvaAlicuotaGeneralVentasInt, "
   vSQL = vSQL & "0 AS VentasAsumidasEstado, "
   vSQL = vSQL & "0 AS DescConcebidosDevVentasAsumidasEstado, "
   vSQL = vSQL & "0 AS VentasBienes, "
   vSQL = vSQL & "0 AS VentasNoGravadasSinRatio, "
   vSQL = vSQL & "0 AS OtrasVentas, "
   vSQL = vSQL & "0 AS DescYDev, "
   vSQL = vSQL & "0 AS TributoVentasAsumidasEstado, "
   vSQL = vSQL & "0 AS TributoDescConcebidosDevVentasAsumidasEstado, "
   vSQL = vSQL & "0 AS TributoDescConcebidosDevVentas, "
   vSQL = vSQL & "0 AS TributoVentasBienes, "
   vSQL = vSQL & "0 AS TributoDescYDev, "
   vSQL = vSQL & "( " & refTotalTributosVenta & ") AS TotaIvaGeneral"
   vSQL = vSQL & " FROM " & insPDTVista.GetViewNamePDTVentasRentaMensual
   vSQL = vSQL & " WHERE " & sqlWhere
   Set gUltimaTasaDeCambio = Nothing
   Set insPDTVista = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLVentasRentaMensual = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLVentasRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLComprasRentaMensual(ByVal valMes As Integer, ByVal valAno As Integer, ByVal valContribuyenteFormal As Boolean, ByVal valConsecutivoCompania As Long, ByRef valUltimaTasaDeCambio As Object, ByVal refCoeficienteAplicable As Currency, ByVal refTotalCreditosDeducibles As Currency) As String
   Dim Sql As String
   On Error GoTo h_ERROR
   Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
   Sql = fSQLComprasRentaMensual(valMes, valAno, valContribuyenteFormal, valConsecutivoCompania, refCoeficienteAplicable, refTotalCreditosDeducibles)
   Set gUltimaTasaDeCambio = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLComprasRentaMensual = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLComprasRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSQLComprasRentaMensual(ByVal valMes As Integer, ByVal valAno As Integer, ByVal valContribuyenteFormal As Boolean, ByVal valConsecutivoCompania As Long, ByVal refCoeficienteAplicable As Currency, ByVal refTotalCreditosDeducibles As Currency) As String
   Dim Sql As String
   Dim sqlWhere As String
   Dim sqlTipoTransaccion As String
   Dim sqlTipoCreditoGravado As String
   Dim sqlTipoCreditoGravadoNoGravado As String
   Dim sqlTipoDeCompraImportacion As String
   Dim sqlTipoDeCompraNacional As String
   Dim prorrateoDiv100 As Currency
   Dim insCxPVista As clsCxPVista
   On Error GoTo h_ERROR
   Set insCxPVista = New clsCxPVista
   prorrateoDiv100 = gConvert.fTruncaANDecimales(refCoeficienteAplicable / 100, 4)
   sqlWhere = "ConsecutivoCompania = " & valConsecutivoCompania
   sqlWhere = sqlWhere & " AND AnoDeAplicacion = " & valAno

   sqlWhere = sqlWhere & " AND MesDeAplicacion = " & valMes
   sqlTipoTransaccion = " AND ( TipoDeTransaccion" & _
                     " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A01_REGISTRO) & _
            " OR TipoDeTransaccion" & _
                     " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A02_COMPLEMENTO) & _
            " OR TipoDeTransaccion" & _
                     " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A03_ANULACION) & " ) "
   sqlTipoCreditoGravado = " AND ( CreditoFiscal" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_DEDUCIBLE) & " ) "
                  
  sqlTipoCreditoGravadoNoGravado = " AND (CreditoFiscal" & _
                  " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_CreditoFiscal.eCF_PRORRATEABLE) & " ) "

   sqlTipoDeCompraImportacion = "TipoDeCompra" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASIMPORTACION)
   sqlTipoDeCompraNacional = "TipoDeCompra" & _
               " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeCompra.eTD_COMPRASNACIONALES)
   
   Sql = "SELECT "
      Sql = Sql & "SUM( " & _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasExentasInt", True, "") & ") AS MontoExentoInt, "
                     
      Sql = Sql & "SUM( " & _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasExentasImp", True, "") & ") AS MontoExentoImp, "
      
      Sql = Sql & "SUM( " & _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasExentas", True, "") & ") AS MontoExento, "

      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraImportacion & sqlTipoTransaccion & sqlTipoCreditoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasImpBaseAlicuotaGeneral", True, ""), "0", True) & ") AS ComprasImpBaseAlicuotaGeneral, "

      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraImportacion & sqlTipoTransaccion & sqlTipoCreditoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasImpIvaAlicuotaGeneral", True, ""), "0", True) & ") AS ComprasImpIvaAlicuotaGeneral, "

      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraNacional & sqlTipoTransaccion & sqlTipoCreditoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasIntBaseAlicuotaGeneral", True, ""), "0", True) & ") AS ComprasIntBaseAlicuotaGeneral, "

      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraNacional & sqlTipoTransaccion & sqlTipoCreditoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasIntIvaAlicuotaGeneral", True, ""), "0", True) & ") AS ComprasIntIvaAlicuotaGeneral, "
                     
      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraNacional & sqlTipoTransaccion & sqlTipoCreditoGravadoNoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasIntGravadasNoGravadasInt", True, ""), "0", True) & ") AS ComprasIntGravadasNoGravadasInt, "
               
      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraNacional & sqlTipoTransaccion & sqlTipoCreditoGravadoNoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasIntIvaAlicuotaGeneral  * " & prorrateoDiv100, True, ""), "0", True) & ") AS ComprasIvaGenGravadasNoGravadasInt, "
      
      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraNacional & sqlTipoTransaccion & sqlTipoCreditoGravadoNoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasIntGravadasNoGravadasImp", True, ""), "0", True) & ") AS ComprasIntGravadasNoGravadasImp, "
               
      Sql = Sql & "SUM(" & gUtilSQL.getIIF(sqlTipoDeCompraImportacion & sqlTipoTransaccion & sqlTipoCreditoGravadoNoGravado, _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "ComprasImpIvaAlicuotaGeneral  * " & prorrateoDiv100, True, ""), "0", True) & ") AS ComprasIvaGenGravadasNoGravadasImp, "

      Sql = Sql & "SUM(" & gUtilSQL.getIIF("TipoDeTransaccion = " & _
               gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDeTransaccionDeLibrosFiscales.eTD_A04_AJUSTE), _
               gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", _
               "Moneda", "MontoEditableAjusteCredito", True, ""), "0", True) & ") AS MontoEditableAjusteCredito, "
               
      Sql = Sql & "(" & refTotalCreditosDeducibles & ") as TotalCreditosDeducibles, "
'      Sql = Sql & "(" & refCreditosParcialesAjustados & ") as CreditosParcialesAjustados, "
'      Sql = Sql & "(" & refTotalCreditosParcialDeducibles & ") as TotalCreditosParcialDeducibles, "
      Sql = Sql & "(" & prorrateoDiv100 & ") as CoeficienteAplicable "
   Sql = Sql & " FROM " & insCxPVista.GetViewNameComprasGeneradasCxP
   Sql = Sql & " WHERE " & sqlWhere
   fSQLComprasRentaMensual = Sql
   Set insCxPVista = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
      "fSQLComprasRentaMensual", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLImpuestoTransaccionesFinancieras(ByVal valConsecutivoCompania As Long, ByVal valMes As Integer, ByVal valAno As Integer, ByVal valCodigoCtaBancaria As String, ByVal valMonedaLocalActual As String, ByRef valUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim insBancoVista As clsBancoVista
   On Error GoTo h_ERROR
   Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
   Set insBancoVista = New clsBancoVista
   Sql = ""
   Sql = "SELECT "
   Sql = Sql & "'06' AS TipoDocumento, " '***Corregir una vez listo en el Saw
   Sql = Sql & "NumeroDeRIF, "
   Sql = Sql & gUtilSQL.getIIF("CodigoPais = 'PE'", "''", "CodigoPais", True) & "AS CodigoPaisExtranjero, "
   Sql = Sql & gUtilSQL.getIIF("GeneraImpuestoBancario = 'S'", "'01'", "'02'", True) & "AS TipoDeOperación, "
   Sql = Sql & "'04' AS Usandocuenta,"
   Sql = Sql & "CodigoConcepto, "
   Sql = Sql & "(CASE WHEN GeneraImpuestoBancario = 'N' THEN CodPaisCta ELSE '' END) AS CodPaisOperacion,"
   Sql = Sql & gUtilSQL.getIIF("CodigoMoneda = " & gUtilSQL.fSimpleSqlValue(valMonedaLocalActual), "''", "Monto", True) & "AS MontoAcumulado, "
   Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "CodigoMoneda", "MontoImp", True, "") & " AS MontoImpuesto, "
   Sql = Sql & "NumeroRIF "
   Sql = Sql & "From " & insBancoVista.GetViewNameMovimientosXCuentaBancaria0695
   Sql = Sql & " WHERE (GeneraImpuestoBancario = 'S') "
   Sql = Sql & "AND (ConsecutivoCompania = " & valConsecutivoCompania & ") AND (CodigoCtaBancaria = " & valCodigoCtaBancaria & ")  "
   Sql = Sql & "AND (MONTH(Fecha) = " & valMes & ") AND (YEAR(Fecha) = " & valAno & ")"
   Sql = Sql & "AND (" & insBancoVista.GetViewNameMovimientosXCuentaBancaria0695 & ".Status = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusCtaBancaria.eSC_ACTIVA) & ") "
   Sql = Sql & " ORDER BY Fecha, ConsecutivoMovimiento"
   Set insBancoVista = Nothing
   Set gUltimaTasaDeCambio = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLImpuestoTransaccionesFinancieras = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLImpuestoTransaccionesFinancieras", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLAgentesDeRetencion(ByVal valConsecutivoCompania As Long, ByVal valMes As Integer, ByVal valAno As Integer, ByVal valMonedaLocalActual As String, ByVal valCodigo As String, ByRef valUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim insPagoVista As clsPagoVista
   Dim valFechaInicial As Date
   Dim valFechaFinal As Date
   Dim insBancoVista As clsBancoVista
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insPagoVista = New clsPagoVista
   Set insBancoVista = New clsBancoVista
   Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
   Set insEnum = New clsEnumAdministrativo
   valFechaInicial = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, True)
   valFechaFinal = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)
   Sql = ""
   Sql = "SELECT "
   Sql = Sql & "NumeroRIF, "
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion = " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "NombreProveedor", "' '", True) & " AS RazonSocial, "
                  
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion <> " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "ApellidoPaterno", "' '", True) & " AS ApellidoPaterno, "
                  
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion <> " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "ApellidoMaterno", "' '", True) & " AS ApellidoMaterno, "
                         
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion <> " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "Nombre", "' '", True) & " AS Nombres, "
                       
   Sql = Sql & "SerieComprobanteRetencion, "
   Sql = Sql & "NumeroComprobanteRetencion, "
   Sql = Sql & "FechaAplicacionRetIva, "
   Sql = Sql & "MontoIvaRetenido, "
   Sql = Sql & "Codigo, "
   Sql = Sql & "SerieNumeroComprobantePago, "
   Sql = Sql & "NumeroComprobantePago, "
   Sql = Sql & "Fecha, "
   Sql = Sql & "MontoAplicado "
   Sql = Sql & "From " & insPagoVista.GetViewNameRetencionXDocumentoPagado
   Sql = Sql & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insPagoVista.GetViewNameRetencionXDocumentoPagado & ".FechaAplicacionRetIva", _
                  valFechaInicial, valFechaFinal)
                     Sql = Sql & "AND (Codigo <> '04' "
   Sql = Sql & "OR Codigo <> '07' "
   Sql = Sql & "OR Codigo <> '11' "
   Sql = Sql & "OR Codigo <> '23') "
   Sql = Sql & " AND CodigoProveedor = " & gUtilSQL.fSimpleSqlValue(valCodigo)
   Sql = Sql & " ORDER BY NumeroRIF,FechaAplicacionRetIva "
   Set insBancoVista = Nothing
   Set gUltimaTasaDeCambio = Nothing
   Set insEnum = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLAgentesDeRetencion = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLAgentesDeRetencion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLOtrasRetencionesRentaIGV(ByVal valConsecutivoCompania As Long, ByVal valMes As Integer, ByVal valAno As Integer, ByVal valMonedaLocalActual As String, ByRef valUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim insPagoVista As clsPagoVista
   Dim valFechaInicial As Date
   Dim valFechaFinal As Date
   Dim insBancoVista As clsBancoVista
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insPagoVista = New clsPagoVista
   Set insBancoVista = New clsBancoVista
   Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
   Set insEnum = New clsEnumAdministrativo
   valFechaInicial = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, True)
   valFechaFinal = gUtilDate.fObtenLaFechaAPartirDelMesYAnoAplicacion(valMes, valAno, False)
   Sql = ""
   Sql = "SELECT "
   Sql = Sql & "CodPeruTipoDocumentoIdentificacion, "
   Sql = Sql & "NumeroRIF, "
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion = " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "2", "1", True) & " AS TipoDescripcion, "
                                    
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion <> " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "ApellidoPaterno", "' '", True) & " AS ApellidoPaterno, "
                  
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion <> " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "ApellidoMaterno", "' '", True) & " AS ApellidoMaterno, "
                         
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion <> " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "Nombre", "' '", True) & " AS Nombres, "
                  
   Sql = Sql & gUtilSQL.getIIF("CodPeruTipoDocumentoIdentificacion = " & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC)), _
                  "NombreProveedor", "' '", True) & " AS RazonSocial, "
                  
   Sql = Sql & "Codigo, "
   Sql = Sql & "SerieNumeroComprobantePago, "
   Sql = Sql & "NumeroComprobantePago, "
   Sql = Sql & "Fecha, "
   Sql = Sql & "MontoIvaRetenido "
   Sql = Sql & "From " & insPagoVista.GetViewNameRetencionXDocumentoPagado
   Sql = Sql & " WHERE ConsecutivoCompania = " & valConsecutivoCompania
   Sql = Sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insPagoVista.GetViewNameRetencionXDocumentoPagado & ".FechaAplicacionRetIva", _
                  valFechaInicial, valFechaFinal)
   Sql = Sql & "AND (Codigo = '04' "
   Sql = Sql & "OR Codigo = '07' "
   Sql = Sql & "OR Codigo = '11' "
   Sql = Sql & "OR Codigo = '23') "
   Sql = Sql & "ORDER BY NumeroRIF,FechaAplicacionRetIva "
   Set insBancoVista = Nothing
   Set gUltimaTasaDeCambio = Nothing
   Set insEnum = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLOtrasRetencionesRentaIGV = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLOtrasRetencionesRentaIGV", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'***************************************************************************************
Public Function fConstruirSQLDetalleDeOperaciones(ByVal valConsecutivoCompania As Long, ByVal valAno As Integer, ByRef valUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim sqlWhere As String
   Dim sqlOrder As String
   Dim insEnumSaw As clsSawEnumVista
   On Error GoTo h_ERROR
   Set insEnumSaw = New clsSawEnumVista
   Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
   sqlOrder = " ORDER BY Mes, TipoComprobante, NumeroSerie"
   sqlWhere = " AND " & gUtilSQL.fYear("Fecha ", "") & " = " & valAno
   sqlWhere = sqlWhere & " AND ConsecutivoCompania = " & valConsecutivoCompania

   Sql = "SELECT "
   Sql = Sql & insEnumSaw.GetViewNameEnumTipoDocumentoDeFactura & ".CodPeruTipoDocumentoFactura AS TipoComprobante, "
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("factura.Numero", "-") & " - 1 > 0", gUtilSQL.fMid("factura.Numero", "1", gUtilSQL.DfInStrSQL("factura.Numero", "-") & " - 1"), "factura.Numero", True) & " AS NumeroSerie, "
   Sql = Sql & gUtilSQL.fMonth("Fecha ", "Mes") & ", "
   Sql = Sql & "SUM " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Moneda", "TotalMontoExento + TotalBaseImponible", True, "") & " AS VentasTotalesSinIGV, "
   Sql = Sql & "SUM " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Moneda", "TotalBaseImponible", True, "") & " As VentasGravadas "
   Sql = Sql & " FROM factura "
   Sql = Sql & " INNER JOIN " & insEnumSaw.GetViewNameEnumTipoDocumentoDeFactura
   Sql = Sql & " ON factura.TipoDeDocumento = " & insEnumSaw.GetViewNameEnumTipoDocumentoDeFactura & ".EnumValue"
   Sql = Sql & " WHERE StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA)
   Sql = Sql & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   Sql = Sql & " AND factura.TipoDeDocumento <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   Sql = Sql & sqlWhere
   Sql = Sql & " GROUP BY Numero," & insEnumSaw.GetViewNameEnumTipoDocumentoDeFactura & ".CodPeruTipoDocumentoFactura, " & gUtilSQL.fMonth("Fecha ", "")
   
   Sql = Sql & " UNION SELECT "
   Sql = Sql & " TipoDeDocumentoLey.Codigo AS TipoComprobante, "
   Sql = Sql & gUtilSQL.getIIF(gUtilSQL.DfInStrSQL("cxC.Numero", "-") & " - 1 > 0", gUtilSQL.fMid("cxC.Numero", "1", gUtilSQL.DfInStrSQL("cxC.Numero", "-") & " - 1"), "cxC.Numero", True) & " AS NumeroSerie, "
   Sql = Sql & gUtilSQL.fMonth("Fecha ", "Mes") & ", "
   Sql = Sql & "SUM " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Moneda", "MontoExento + MontoGravado", True, "") & " AS VentasTotalesSinIGV, "
   Sql = Sql & "SUM " & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Moneda", "montoGravado", True, "") & " As VentasGravadas "
   Sql = Sql & " FROM cxC "
   Sql = Sql & " INNER JOIN TipoDeDocumentoLey"
   Sql = Sql & " ON cxC.TipoCxc = TipoDeDocumentoLey.TipoDeDocumento"
   Sql = Sql & " WHERE Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO)
   Sql = Sql & " AND Origen = " & gUtilSQL.fSQLSimpleValueForEnum(enum_OrigenFacturacionOManual.eOF_MANUAL)
   Sql = Sql & sqlWhere
   Sql = Sql & " GROUP BY Numero, TipoDeDocumentoLey.Codigo, " & gUtilSQL.fMonth("Fecha ", "")
   
   Sql = Sql & sqlOrder
   
   Set insEnumSaw = Nothing
h_EXIT: On Error GoTo 0
   fConstruirSQLDetalleDeOperaciones = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLDetalleDeOperaciones", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fConstruirSQLISC(ByVal valConsecutivoCompania As Long, ByVal valFechaInicial As Date, ByVal valFechaFinal As Date, ByRef valUltimaTasaDeCambio As Object) As String
   Dim Sql As String
   Dim sqlWhere As String
   Dim sqlOrder As String
   On Error GoTo h_ERROR
   Set gUltimaTasaDeCambio = valUltimaTasaDeCambio
   Sql = "SELECT"
   Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Moneda", "MontoGravado", True, "") & " AS BaseImponible, "
   Sql = Sql & gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio("CambioABolivares", "Moneda", "OtrosImpuestosCxP.Monto", True, "") & " AS Tributo, "
   Sql = Sql & gUtilSQL.fMonth("Fecha ", "Mes") & " , "
   Sql = Sql & " proveedor.NombreProveedor"
   Sql = Sql & " FROM OtrosImpuestosCxP"
   Sql = Sql & " INNER JOIN cxP"
   Sql = Sql & " ON OtrosImpuestosCxP.ConsecutivoCompania = cxP.ConsecutivoCompania"
   Sql = Sql & " AND OtrosImpuestosCxP.ConsecutivoCxP = cxP.ConsecutivoCxP"
   Sql = Sql & " INNER JOIN proveedor"
   Sql = Sql & " ON cxP.ConsecutivoCompania = proveedor.ConsecutivoCompania"
   Sql = Sql & " AND cxP.CodigoProveedor = proveedor.CodigoProveedor"
   Sql = Sql & " WHERE (OtrosImpuestosCxP.Siglas = 'ISC')"
   Sql = Sql & " AND (cxP.Status <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusDocumento.eSD_ANULADO) & ")"
   Sql = Sql & " AND (cxP.ConsecutivoCompania = " & valConsecutivoCompania & ")"
   Sql = Sql & " AND (" & gUtilSQL.fSQLValueBetween("cxP.Fecha", valFechaInicial, valFechaFinal) & ")"
   Sql = Sql & " AND (cxP.AnoDeAplicacion = " & gUtilDate.fMonth(valFechaInicial) & ")"
   Sql = Sql & " AND (cxP.MesDeAplicacion = " & gUtilDate.fYear(valFechaInicial) & ")"
h_EXIT:    On Error GoTo 0
   fConstruirSQLISC = Sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fConstruirSQLISC", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

