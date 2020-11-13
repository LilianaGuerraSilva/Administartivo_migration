VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSawEnumVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsSawEnumVista"
Private Const CM_MESSAGE_NAME As String = "Vista Saw Enum"
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fCreateProjectEnumViews(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibViews As clsUtilViews
   On Error GoTo h_ERROR
   Set insLibViews = New clsUtilViews
   vResult = True
'vResult = vResult And insLibViews.fCreateView(GetViewNameX(), fSqlViewX(), valShowMessage, valWithEncryption)
'El orden de estas debe ser dependiendo más de las referencias y dependencias
'que se usen que si es o no la más nueva
   vResult = vResult And insLibViews.fCreateView(GetViewNameEnumTipoDeTransaccion(), fSqlViewEnumTipoDeTransaccion, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(GetViewNameEnumTipoDocumentoIdentificacion(), fSqlViewEnumTipoDocumentoIdentificacion, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(GetViewNameEnumTipoDocumentoDeFactura(), fSqlViewEnumTipoDocumentoDeFactura, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(GetViewNameEnumTipoDocumentoIva, fSqlViewEnumTipoDocumentoIva, valShowMessage, valWithEncryption)
' Libro de Ventas Nelson
   vResult = vResult And insLibViews.fCreateView(GetViewNameEnumTipoDocumentoIvaFactura, fSqlViewEnumTipoDocumentoIvaFactura, valShowMessage, valWithEncryption)
   vResult = vResult And insLibViews.fCreateView(GetViewNameEnumTipoDeVentaIVA, fSqlTipoDeVentaIVA, valShowMessage, valWithEncryption)
' Libro de Ventas Nelson
   vResult = vResult And insLibViews.fCreateView(GetViewNameEnumTipoFormaDeCobro(), fSqlViewEnumTipoFormaDeCobro, valShowMessage, valWithEncryption)
   If vResult And valShowMessage Then
      gMessage.exito "Se Crearon con Exito las Vistas de Enumerativos"
   End If
   fCreateProjectEnumViews = vResult
   Set insLibViews = Nothing
h_EXIT:  On Error GoTo 0
   vResult = False
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateProjectEnumViews", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fDropProjectViews(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insLibViews As clsUtilViews
   On Error GoTo h_ERROR
   Set insLibViews = New clsUtilViews
   vResult = True
'vResult = vResult And insLibViews.fDropView(GetViewNameX(), valShowMessage)
'El orden de estas debe ser inverso al orden de fCreateProjectViews
   vResult = vResult And insLibViews.fDropView(GetViewNameEnumTipoFormaDeCobro, valShowMessage)
   ' Libro de Ventas Nelson
   vResult = vResult And insLibViews.fDropView(GetViewNameEnumTipoDeVentaIVA, valShowMessage)
   vResult = vResult And insLibViews.fDropView(GetViewNameEnumTipoDocumentoIvaFactura, valShowMessage)
   ' Libro de Ventas Nelson
   vResult = vResult And insLibViews.fDropView(GetViewNameEnumTipoDocumentoIva, valShowMessage)
   vResult = vResult And insLibViews.fDropView(GetViewNameEnumTipoDocumentoDeFactura, valShowMessage)
   vResult = vResult And insLibViews.fDropView(GetViewNameEnumTipoDocumentoIdentificacion, valShowMessage)
   vResult = vResult And insLibViews.fDropView(GetViewNameEnumTipoDeTransaccion, valShowMessage)
   If vResult And valShowMessage Then
      gMessage.exito "Se Eliminaron con Exito las Vistas de Enumerativos"
   End If
   fDropProjectViews = vResult
   Set insLibViews = Nothing
h_EXIT:  On Error GoTo 0
   vResult = False
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropProjectViews", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameEnumTipoDocumentoIdentificacion() As String
   GetViewNameEnumTipoDocumentoIdentificacion = "IGV_EnumTipoDocumentoIdentificacion"
End Function

Private Function fSqlViewEnumTipoDocumentoIdentificacion() As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vCadenaResultadoCod As String
   Dim vSeparador As String
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insEnum = New clsEnumAdministrativo
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoIdentificacionPeru.eTDIP_RUC)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoIdentificacionPeru.eTDIP_DNI)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoIdentificacionPeru.eTDIP_CARNET_DE_EXTRANJERIA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoIdentificacionPeru.eTDIP_PASAPORTE)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoIdentificacionPeru.eTDIP_OTROS_TIPOS_DE_DOCUMENTOS)
   
   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_RUC))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_DNI))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_CARNET_DE_EXTRANJERIA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_PASAPORTE))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIdentificacionPeruToString(enum_TipoDocumentoIdentificacionPeru.eTDIP_OTROS_TIPOS_DE_DOCUMENTOS))
   
   vCadenaResultadoCod = gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_RUC))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_DNI))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_CARNET_DE_EXTRANJERIA))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_PASAPORTE))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoIdentificacionPeruToCodigoPeru(eTDIP_OTROS_TIPOS_DE_DOCUMENTOS))
   
   vSQL = "SELECT "
   vSQL = vSQL & "IntValue AS EnumValue, "
   vSQL = vSQL & "DbValue, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "StrTipoDocumentoIdentificacion") & ", "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoCod, gTexto.fSeparadorStandardDeElementosString, "=", "CodPeruTipoDocumentoIdentificacion")
   vSQL = vSQL & " FROM Lib.EnumBase "
   vSQL = vSQL & " WHERE IntValue < 5 "
   vSQL = vSQL & " ORDER BY IntValue"
   fSqlViewEnumTipoDocumentoIdentificacion = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewEnumTipoDocumentoIdentificacion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameEnumTipoDeTransaccion() As String
   GetViewNameEnumTipoDeTransaccion = "IGV_EnumTipoDeTransaccion"
End Function

Private Function fSqlViewEnumTipoDeTransaccion() As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vCadenaResultadoCod As String
   Dim vSeparador As String
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insEnum = New clsEnumAdministrativo
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_GIRO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_NOTADECREDITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_NOTADEDEBITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_NOTAENTREGA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_CHEQUEDEVUELTO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_BOLETADEVENTA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_RECIBOPORHONORARIOS)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_LIQUIDACIONDECOMPRA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_OTROS)
   
   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_FACTURA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_GIRO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_NOTADECREDITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_NOTADEDEBITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_NOTAENTREGA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_CHEQUEDEVUELTO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_BOLETADEVENTA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_RECIBOPORHONORARIOS))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_LIQUIDACIONDECOMPRA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeCxCToString(enum_TipoDeTransaccion.eTD_OTROS))
   
   vCadenaResultadoCod = gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_FACTURA))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_NOTADECREDITO))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_NOTADEDEBITO))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_BOLETADEVENTA))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_TICKETMAQUINAREGISTRADORA))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_RECIBOPORHONORARIOS))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_LIQUIDACIONDECOMPRA))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_OTROS))
   
   vSQL = "SELECT "
   vSQL = vSQL & "IntValue AS EnumValue, "
   vSQL = vSQL & "DbValue, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "StrTipoTransaccion") & ", "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoCod, gTexto.fSeparadorStandardDeElementosString, "=", "CodPeruTipoTransaccion")
   vSQL = vSQL & " FROM Lib.EnumBase "
   vSQL = vSQL & " WHERE IntValue < 11"
   vSQL = vSQL & " ORDER BY IntValue"
   fSqlViewEnumTipoDeTransaccion = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewEnumTipoDeTransaccion", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameEnumTipoDocumentoDeFactura() As String
   GetViewNameEnumTipoDocumentoDeFactura = "IGV_EnumTipoDocumentoDeFactura"
End Function

Private Function fSqlViewEnumTipoDocumentoDeFactura() As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vCadenaResultadoCod As String
   Dim vSeparador As String
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insEnum = New clsEnumAdministrativo
   
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOASIGNADO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_BOLETA)
   
   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue(insEnum.enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_FACTURA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_NOTADECREDITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_NOASIGNADO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipodocumentoFacturaToString(enum_TipoDocumentoFactura.eTF_BOLETA))
   
   vCadenaResultadoCod = gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_FACTURA))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_NOTADECREDITO))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_NOTADEDEBITO))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_OTROS))
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDocumentoToCodigoPeru(eTD_BOLETADEVENTA))
   
   vSQL = "SELECT "
   vSQL = vSQL & "IntValue AS EnumValue, "
   vSQL = vSQL & "DbValue, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "StrTipoDocumentoFactura") & ", "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoCod, gTexto.fSeparadorStandardDeElementosString, "=", "CodPeruTipoDocumentoFactura")
   vSQL = vSQL & " FROM Lib.EnumBase "
   vSQL = vSQL & " WHERE IntValue < 7"
   vSQL = vSQL & " ORDER BY IntValue"
   fSqlViewEnumTipoDocumentoDeFactura = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewEnumTipoDocumentoDeFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameEnumTipoDocumentoIva() As String
   GetViewNameEnumTipoDocumentoIva = "IGV_EnumTipoDocumentoIva"
End Function

Private Function fSqlViewEnumTipoDocumentoIva() As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vCadenaResultadoCod As String
   Dim vSeparador As String
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insEnum = New clsEnumAdministrativo
   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_GIRO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_CHEQUEDEVUELTO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_NOTADECREDITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_NOTADEDEBITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_NOTAENTREGA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_NOASIGNADO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_BOLETADEVENTA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_TICKETMAQUINAREGISTRADORA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_RECIBOPORHONORARIOS)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_LIQUIDACIONDECOMPRA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeTransaccion.eTD_OTROS)


   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_FACTURA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_NOTADECREDITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_NOTADEDEBITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_NOTADEENTREGA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
    vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")

   vCadenaResultadoCod = gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_FACTURA)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_NOTADECREDITO)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_NOTADEDEBITO)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_NOTADEENTREGA)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
 '  vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_RESUMENDIARIODEVENTAS)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")


   vSQL = "SELECT "
   vSQL = vSQL & "IntValue AS EnumValue, "
   vSQL = vSQL & "DbValue, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "StrTipoDocumentoFacturaSawIva") & ", "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoCod, gTexto.fSeparadorStandardDeElementosString, "=", "CodTipoDocumentoFacturaSawIva")
   vSQL = vSQL & " FROM Lib.EnumBase "
   vSQL = vSQL & " WHERE IntValue < 13"
   vSQL = vSQL & " ORDER BY IntValue"
   fSqlViewEnumTipoDocumentoIva = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewEnumTipoDocumentoIva", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameEnumTipoDocumentoIvaFactura() As String
   GetViewNameEnumTipoDocumentoIvaFactura = "IGV_EnumTipoDocumentoIvaFactura"
End Function

Private Function fSqlViewEnumTipoDocumentoIvaFactura() As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vCadenaResultadoCod As String
   Dim vSeparador As String
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insEnum = New clsEnumAdministrativo

   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_FACTURA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADECREDITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOTADEDEBITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_NOASIGNADO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDocumentoFactura.eTF_BOLETA)

   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_FACTURA))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_NOTADECREDITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_NOTADEDEBITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDocumentoIvaToString(enum_TipoDocumentoIVA.eTD_TD_RESUMENDIARIODEVENTAS))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("Comprobante Fiscal")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("N/A")
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue("Nota de Credito Comprobante Fiscal")

   vCadenaResultadoCod = gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_FACTURA)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_NOTADECREDITO)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_NOTADEDEBITO)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_RESUMENDIARIODEVENTAS)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_RESUMENDIARIODEVENTAS)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue("0")
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDocumentoIVA.eTD_TD_RESUMENDIARIODEVENTAS)

   vSQL = "SELECT "
   vSQL = vSQL & "IntValue AS EnumValue, "
   vSQL = vSQL & "DbValue, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "StrTipoDocumentoSawIva") & ", "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoCod, gTexto.fSeparadorStandardDeElementosString, "=", "CodTipoDocumentoSawIva")
   vSQL = vSQL & " FROM Lib.EnumBase "
   vSQL = vSQL & " WHERE IntValue < 8"
   vSQL = vSQL & " ORDER BY IntValue"
   fSqlViewEnumTipoDocumentoIvaFactura = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewEnumTipoDocumentoIvaFactura", _
CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
Public Function GetViewNameEnumTipoDeVentaIVA() As String
   GetViewNameEnumTipoDeVentaIVA = "IGV_EnumTipoDeVentaIva"
End Function

Private Function fSqlTipoDeVentaIVA() As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vCadenaResultadoCod As String
   Dim vSeparador As String
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insEnum = New clsEnumAdministrativo

   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDeVenta.eTD_INTERNA)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeVenta.eTD_EXPORTACION)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeVenta.eTD_SIN_DERECHO_A_CREDITO_FISCAL)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeVenta.eTD_A_CONTRIBUYENTE)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeVenta.eTD_A_NO_CONTRIBUYENTE)

   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeVentaIvaToString(enum_TipoDeVentaIva.eTD_TV_VENTASCONTRIBUYENTES))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeVentaIvaToString(enum_TipoDeVentaIva.eTD_TV_VENTASEXPORTACION))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeVentaIvaToString(enum_TipoDeVentaIva.eTD_TV_VENTASSINDERECHOACREDITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeVentaIvaToString(enum_TipoDeVentaIva.eTD_TV_VENTASCONTRIBUYENTES))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.enumTipoDeVentaIvaToString(enum_TipoDeVentaIva.eTD_TV_VENTASNOCONTRIBUYENTES))

   vCadenaResultadoCod = gUtilSQL.fSimpleSqlValue(enum_TipoDeVentaIva.eTD_TV_VENTASCONTRIBUYENTES)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDeVentaIva.eTD_TV_VENTASEXPORTACION)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDeVentaIva.eTD_TV_VENTASSINDERECHOACREDITO)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDeVentaIva.eTD_TV_VENTASCONTRIBUYENTES)
   vCadenaResultadoCod = vCadenaResultadoCod & vSeparador & gUtilSQL.fSimpleSqlValue(enum_TipoDeVentaIva.eTD_TV_VENTASNOCONTRIBUYENTES)

   vSQL = "SELECT "
   vSQL = vSQL & "IntValue AS EnumValue, "
   vSQL = vSQL & "DbValue, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "StrTipoDeVentaIva") & ", "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoCod, gTexto.fSeparadorStandardDeElementosString, "=", "CodTipoDeVentaIva")
   vSQL = vSQL & " FROM Lib.EnumBase "
   vSQL = vSQL & " WHERE IntValue < 5"
   vSQL = vSQL & " ORDER BY IntValue"
   fSqlTipoDeVentaIVA = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
"fSqlTipoDeVentaIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameEnumTipoFormaDeCobro() As String
   GetViewNameEnumTipoFormaDeCobro = "IGV_EnumTipoFormaDeCobro"
End Function

Private Function fSqlViewEnumTipoFormaDeCobro() As String
   Dim vSQL As String
   Dim vCadenaComparacion As String
   Dim vCadenaResultadoStr As String
   Dim vCadenaResultadoCod As String
   Dim vSeparador As String
   Dim insEnum As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set insEnum = New clsEnumAdministrativo

   vSeparador = gTexto.fSeparadorStandardDeElementosString
   vCadenaComparacion = gConvert.fConvierteAString(enum_TipoDeFormaDeCobro.eTFC_EFECTIVO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeFormaDeCobro.eTFC_TARJETA_DEBITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeFormaDeCobro.eTFC_TARJETA_CREDITO)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeFormaDeCobro.eTFC_CHEQUE)
   vCadenaComparacion = vCadenaComparacion & vSeparador & gConvert.fConvierteAString(enum_TipoDeFormaDeCobro.eTFC_OTROS)

   vCadenaResultadoStr = gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_EFECTIVO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_TARJETA_DEBITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_TARJETA_CREDITO))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_CHEQUE))
   vCadenaResultadoStr = vCadenaResultadoStr & vSeparador & gUtilSQL.fSimpleSqlValue(insEnum.EnumTipoDeFormaDeCobroToString(enum_TipoDeFormaDeCobro.eTFC_OTROS))

   vSQL = "SELECT "
   vSQL = vSQL & "IntValue AS EnumValue, "
   vSQL = vSQL & "DbValue, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIf("IntValue", vCadenaComparacion, vCadenaResultadoStr, gTexto.fSeparadorStandardDeElementosString, "=", "StrTipoDocumentoFactura")
   vSQL = vSQL & " FROM Lib.EnumBase "
   vSQL = vSQL & " WHERE IntValue < 5"
   vSQL = vSQL & " ORDER BY IntValue"
   fSqlViewEnumTipoFormaDeCobro = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewEnumTipoFormaDeCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
