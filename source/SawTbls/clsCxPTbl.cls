VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsCxPTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsCxPTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "CxP"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania, Numero, CodigoProveedor"
End Function
Public Function GetTableName() As String
   GetTableName = "cxP"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Adm.Proveedor", GetTableName(), "CodigoProveedor", _
         "CodigoProveedor", Not usarSoloUnElementoEnLaRelacion, _
         "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Moneda", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "CodigoMoneda", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania, ConsecutivoCxp)"
   gDbUtil.fAddUniqueConstraint "cxP", "u_cxPNumeroCodigoProveedor", "ConsecutivoCompania, Numero, CodigoProveedor"
   sAddForeignKeys
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Moneda"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Observaciones"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroPlanillaDeImportacion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroExpedienteDeImportacion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroDeFacturaAfectada"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroControl"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroComprobanteRetencion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroDocOrigen"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoLote"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreOperador"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoMoneda"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "Fecha", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaCancelacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaVencimiento", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaAnulacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaAplicacionRetIVA", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaUltimaModificacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sModifyOtherPropertiesOfFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Public Function fCreateTable(ByVal valShowMessage As Boolean) As Boolean
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = gUtilSQL.fCreateTable(GetTableName()) & " ( "
   SQL = SQL & "ConsecutivoCompania " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "ConsecutivoCxp " & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "ConsCxP NOT NULL, "
   SQL = SQL & "Numero " & gUtilSQL.getVarCharTypeForDB(25) & " CONSTRAINT nn" & GetTableName() & "Numero NOT NULL, "
   SQL = SQL & "TipoDeCxp " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "Status " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CodigoProveedor " & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "CodigoProv NOT NULL, "
   SQL = SQL & "Fecha " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaCancelacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaVencimiento " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaAnulacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "Moneda " & gUtilSQL.getVarCharTypeForDB(80) & ", "
   SQL = SQL & "CambioAbolivares " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "AplicaParaLibrodeCompras " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AplicaPara NOT NULL, "
   SQL = SQL & "MontoExento " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoGravado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIva " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoAbonado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "DiaDeAplicacion " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "MesDeAplicacion " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "AnoDeAplicacion " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "Observaciones " & gUtilSQL.getVarCharTypeForLargeTextForDB & ", "
   SQL = SQL & "CreditoFiscal " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TipoDeCompra " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "SeHizoLaRetencion " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeHizoLaRe NOT NULL, "
   SQL = SQL & "MontoGravableAlicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoGravableAlicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoGravableAlicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIvaalicuotaGeneral " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIvaalicuota2 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIvaalicuota3 " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NumeroPlanillaDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NumeroExpedienteDeImportacion " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "TipoDeTransaccion " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NumeroDeFacturaAfectada " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NumeroControl " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "SeHizoLaRetencionIva " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeHizoLaRe NOT NULL, "
   SQL = SQL & "NumeroComprobanteRetencion " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "FechaAplicacionRetIva " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "PorcentajeRetencionAplicado " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoRetenido " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "OrigenDeLaRetencion " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "RetencionAplicadaEnPago " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RetencionA NOT NULL, "
   SQL = SQL & "OrigenInformacionRetencion " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CxPgeneradaPor " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "EsCxPhistorica " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsCxPhisto NOT NULL, "
   SQL = SQL & "NumDiasDeVencimiento " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "NumeroDocOrigen " & gUtilSQL.getVarCharTypeForDB(15) & ", "
   SQL = SQL & "CodigoLote " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "GenerarAsientoDeRetiroEnCuenta" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "GenerarAsi NOT NULL, "
   SQL = SQL & "TotalOtrosImpuestos" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "SeContabilRetIva " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeContabil NOT NULL, "
   SQL = SQL & "DondeContabilRetIva " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CodigoMoneda " & gUtilSQL.getVarCharTypeForDB(4) & " CONSTRAINT nn" & GetTableName() & "CodigoMoneda NOT NULL, "
   SQL = SQL & "EstaAsociadoARendicion" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EstaAsocia NOT NULL, "
   SQL = SQL & "ConsecutivoRendicion" & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   SQL = SQL & "OrigenDeLaRetencionISLR " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "DondeContabilISLR " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ISLRAplicadaEnPago " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ISLRAplica NOT NULL, "
   SQL = SQL & "MontoRetenidoISLR " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "SeContabilISLR " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeContabil NOT NULL, "
   SQL = SQL & "FechaAplicacionImpuestoMunicipal" & gUtilSQL.getDateTypeForDB() & " CONSTRAINT nn" & GetTableName() & "FechaAplic NOT NULL, "
   SQL = SQL & "NumeroComprobanteImpuestoMunicipal" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "MontoRetenidoImpuestoMunicipal" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ImpuestoMunicipalRetenido" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImpuestoMu NOT NULL, "
   SQL = SQL & "NumeroControlDeFacturaAfectada" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NumeroDeclaracionAduana" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "FechaDeclaracionAduana" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "UsaPrefijoSerie" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaPrefijo NOT NULL, "
   SQL = SQL & "EsUnaCuentaATerceros" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsUnaCuent NOT NULL, "
   SQL = SQL & "CodigoProveedorOriginalServicio" & gUtilSQL.getVarCharTypeForDB(10) & " CONSTRAINT nn" & GetTableName() & "CodigoProv NOT NULL, "
   SQL = SQL & "SeHizoLaDetraccion " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SeHizoLaDe NOT NULL, "
   SQL = SQL & "AplicaIvaAlicuotaEspecial" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AplicaIvaA NOT NULL, "
   SQL = SQL & "MontoGravableAlicuotaEspecial1" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIVAAlicuotaEspecial1" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeIvaAlicuotaEspecial1" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoGravableAlicuotaEspecial2" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "MontoIVAAlicuotaEspecial2" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "PorcentajeIvaAlicuotaEspecial2" & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "NumeroSerie" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NumeroDeDocumento" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NumeroSerieDocAfectado" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NumeroDeDocumentoAfectado" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CodigoTipoDeComprobante" & gUtilSQL.getVarCharTypeForDB(2) & ", "
   SQL = SQL & "BaseImponibleIGTFML" & gUtilSQL.getNumericTypeForDB(12, 2) & " Constraint cBiG Default(0), "
   SQL = SQL & "AlicuotaIGTFML" & gUtilSQL.getNumericTypeForDB(12, 2) & " Constraint cAiG Default(0), "
   SQL = SQL & "MontoIGTFML" & gUtilSQL.getNumericTypeForDB(12, 2) & " Constraint cMiG Default(0), "
   SQL = SQL & "NombreOperador" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "FechaUltimaModificacion" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   sModifyOtherPropertiesOfFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If

   Dim insRenglonImpuestoMunicipalRet As clsRenglonImpuestoMuniRetTbl
   Set insRenglonImpuestoMunicipalRet = New clsRenglonImpuestoMuniRetTbl
   If insRenglonImpuestoMunicipalRet.fCreateTable(valShowMessage) Then
      fCreateTable = True
   End If
   Set insRenglonImpuestoMunicipalRet = Nothing
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
