VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsParametrosCompaniaTbl"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsParametrosCompaniaTbl"
End Function
Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "Parametros Compania"
End Function
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function
Private Function fPrimaryKeyExistMessage() As String
   fPrimaryKeyExistMessage = "Ya existe ConsecutivoCompania"
End Function
Public Function GetTableName() As String
   GetTableName = "ParametrosCompania"
End Function
Private Sub sAddForeignKeys()
   Dim usarSoloUnElementoEnLaRelacion As Boolean
   Dim deleteOnCascade As Boolean
   On Error GoTo h_ERROR
   usarSoloUnElementoEnLaRelacion = True
   deleteOnCascade = True
   gDbUtil.AddForeignKeySegunTablaPrimaria "Cliente", GetTableName(), "codigo", _
         "CodigoGenericoCliente", Not usarSoloUnElementoEnLaRelacion, _
         "", "", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Vendedor", GetTableName(), "codigo", _
         "CodigoGenericoVendedor", Not usarSoloUnElementoEnLaRelacion, _
         "", "", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoReversoCobranza", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoDebitoBancario", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoCreditoBancario", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Ciudad", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "NombreCiudad", "CiudadRepLegal", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "CuentaBancaria", GetTableName(), "Codigo", _
         "CuentaBancariaAnticipo", Not usarSoloUnElementoEnLaRelacion, _
         "", "", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoBancarioAnticipoCobrado", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoBancarioAnticipoPagado", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoBancarioReversoAnticipoCobrado", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoBancarioReversoAnticipoPagado", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "CuentaBancaria", GetTableName(), "Codigo", _
         "CuentaBancariaCobroDirecto", Not usarSoloUnElementoEnLaRelacion, _
         "", "", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoBancarioCobroDirecto", Not deleteOnCascade
   gDbUtil.AddForeignKeySegunTablaPrimaria "Beneficiario", GetTableName(), "Consecutivo", _
         "BeneficiarioGenerico", Not usarSoloUnElementoEnLaRelacion, _
        "ConsecutivoCompania", "ConsecutivoCompania", Not deleteOnCascade
         
   gDbUtil.AddForeignKeySegunTablaPrimaria "ConceptoBancario", GetTableName(), "", "", _
         usarSoloUnElementoEnLaRelacion, "Codigo", "ConceptoBancarioReversoSolicitudDePago", Not deleteOnCascade
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddForeignKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAddPrimaryKeyAndOtherConstrains()
   On Error GoTo h_ERROR
   gDbUtil.Execute gDefDatabase.Conexion, "ALTER TABLE " & GetTableName() & "  add CONSTRAINT p_" & GetTableName() & " primary key (ConsecutivoCompania)"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAddPrimaryKeyAndOtherConstrains", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sAllowCeroLengthFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible1"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible2"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible3"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible4"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible5"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible6"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible7"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible8"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible9"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible10"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible11"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinible12"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "PrimeraFactura"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Prefijo"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Prefijo2"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinibleInventario1"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinibleInventario2"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinibleInventario3"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinibleInventario4"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreCampoDefinibleInventario5"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaComprobanteDePago"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaFactura"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodigoGenericoCuentaBancaria"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaCotizacion"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaCompobanteCobranza"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaOrdenDeCompra"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaNotaDeCredito"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaNotaDeDebito"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaBoleta"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaComisionSobreCobranza"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaNotaEntradaSalida"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "PrimeraNotaDeCredito"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "PrimeraNotaDeDebito"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "PrimeraBoleta"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaFactura2"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "DiaDelCierreFiscal"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "MesDelCierreFiscal"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreYapellidor"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CodTelfr"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Telefonor"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "Direccionr"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroRifr"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CorreoElectronicoRepLegal"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaComprobanteDeMovBancario"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreContador"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CedulaContador"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NumeroCpc"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaOrdenDeDespacho"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaCompra"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaComprobanteDeRetIva"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "CuentaBancariaAnticipo"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ConceptoBancarioAnticipoCobrado"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ConceptoBancarioAnticipoPagado"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ConceptoBancarioReversoAnticipoCobrado"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ConceptoBancarioReversoAnticipoPagado"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaReciboDeAnticipoCobrado"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombrePlantillaReciboDeAnticipoPagado"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreVendedorUno"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreVendedorDos"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "NombreVendedorTres"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ConceptoBancarioReversoDePago"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ModeloFacturaModoTexto"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "ModeloFacturaModoTexto2"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "SinonimoGrupo"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "SinonimoTalla"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "SinonimoColor"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "SinonimoSerial"
   gDbUtil.sModificarLaPropiedadPermitirLongitudCeroDelcampo GetTableName, "SinonimoRollo"
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "sAllowCeroLengthFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub sModifyOtherPropertiesOfFields()
   On Error GoTo h_ERROR
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaDeInicioContabilizacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "UltimaFechaDeFacturacionHistorica", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
   gDbUtil.sModificarLaPropiedadFormatDeLaFecha GetTableName, "FechaDeInicioConciliacion", gDefDatabase.getDatabaseName, gDefDatabase.getPasswordBaseDeDatos
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
   SQL = SQL & "UsaMonedaExtranjera " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "usaMonedaE NOT NULL, "
   SQL = SQL & "UsaPrecioSinIva " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaPrecioS NOT NULL, "
   SQL = SQL & "EmitirDirecto " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EmitirDire NOT NULL, "
   SQL = SQL & "CantidadDeCopiasDeLaFacturaAlImprimir " & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "CantidadDe NOT NULL, "
   SQL = SQL & "UsaAlmacen " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaAlmacen NOT NULL, "
   SQL = SQL & "CodigoGenericoCliente " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "CodigoGenericoVendedor " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "ConceptoReversoCobranza " & gUtilSQL.getVarCharTypeForDB(8) & " CONSTRAINT nn" & GetTableName() & "ConceptoRe NOT NULL, "
   SQL = SQL & "ConceptoDebitoBancario " & gUtilSQL.getVarCharTypeForDB(8) & " CONSTRAINT nn" & GetTableName() & "ConceptoDe NOT NULL, "
   SQL = SQL & "ConceptoCreditoBancario" & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "ModeloDeFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ModeloDeFa NOT NULL, "
   SQL = SQL & "UsaCamposDefinibles " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCamposD NOT NULL, "
   SQL = SQL & "NombreCampoDefinible1 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible2 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible3 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible4 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible5 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible6 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible7 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible8 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible9 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible10 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible11 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinible12 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "FacturaPreNumerada " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "FacturaPre NOT NULL, "
   SQL = SQL & "PrimeraFactura " & gUtilSQL.getVarCharTypeForDB(11) & ", "
   SQL = SQL & "NumeroDeCerosAlaIzquierda " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "TipoDePrefijo " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TipoDePref NOT NULL, "
   SQL = SQL & "Prefijo " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "CodigoAlmacenGenerico " & gUtilSQL.getVarCharTypeForDB(5) & " CONSTRAINT nn" & GetTableName() & "CodigoAlma NOT NULL, "
   SQL = SQL & "NoImprimirFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "NoImprimir NOT NULL, "
   SQL = SQL & "RellenaCerosAlaIzquierda " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RellenaCer NOT NULL, "
   SQL = SQL & "NumCopiasComprobanteRetencion " & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "NumCopiasC NOT NULL, "
   SQL = SQL & "NumCopiasComprobantepago " & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "NumCopiasC NOT NULL, "
   SQL = SQL & "NombreCampoDefinibleInventario1 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinibleInventario2 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinibleInventario3 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinibleInventario4 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "NombreCampoDefinibleInventario5 " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "PermitirFacturarConCantidadCero " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "PermitirFa NOT NULL, "
   SQL = SQL & "UsaRetencion " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaRetenci NOT NULL, "
   SQL = SQL & "InsertandoPorPrimeraVez " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Insertando NOT NULL, "
   SQL = SQL & "NombrePlantillaComprobanteDePago " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "NombrePlantillaFactura " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "TipoDeOrdenDePagoAimprimir " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CodigoGenericoCuentaBancaria " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "ImprimirFacturaConSubtotalesPorLineaDeProducto " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombrePlantillaCotizacion " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "NombrePlantillaCompobanteCobranza " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ImprimirCombrobanteAlIngresarCobranza " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   SQL = SQL & "NoImprimirComprobanteDePago " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "NoImprimir NOT NULL, "
   SQL = SQL & "UsarOtrosCargoDeFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsarOtrosC NOT NULL, "
   SQL = SQL & "UsarBaseImponibleDiferenteA0Y100 " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsarBaseIm NOT NULL, "
   SQL = SQL & "SolicitarIngresoDeTasaDeCambioAlEmitir " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombrePlantillaOrdenDeCompra " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ImprimirOrdenDeCompra " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirOr NOT NULL, "
   SQL = SQL & "UsarDecimalesAlImprimirCantidad " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombrePlantillaNotaDeCredito " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "NombrePlantillaNotaDeDebito " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "NombrePlantillaBoleta" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "FormaDeCalcularComisionesSobreCobranza " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombrePlantillaComisionSobreCobranza " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ImprimirReporteAlIngresarNotaEntradaSalida " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombrePlantillaNotaEntradaSalida " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "PrimeraNotaDeCredito " & gUtilSQL.getVarCharTypeForDB(11) & ", "
   SQL = SQL & "PrimeraNotaDeDebito " & gUtilSQL.getVarCharTypeForDB(11) & ", "
   SQL = SQL & "PrimeraBoleta" & gUtilSQL.getVarCharTypeForDB(11) & ", "
   SQL = SQL & "UsaMultiplesAlicuotas " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ForzarFechaFacturaAmesEspecifico " & gUtilSQL.getCharTypeForDB(1) & ", " ' & " CONSTRAINT nn" & GetTableName() & "ForzarFech NOT NULL, "
   SQL = SQL & "MesFacturacionEnCurso " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "FechaDeInicioContabilizacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "PermitirIncluirFacturacionHistorica " & gUtilSQL.getCharTypeForDB(1) & ", " '& " CONSTRAINT nn" & GetTableName() & "PermitirIn NOT NULL, "
   SQL = SQL & "UltimaFechaDeFacturacionHistorica " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "GenerarCxCalEmitirUnaFacturaHistorica " & gUtilSQL.getCharTypeForDB(1) & ", " '& " CONSTRAINT nn" & GetTableName() & "GenerarCxC NOT NULL, "
   SQL = SQL & "UsarDosTalonarios " & gUtilSQL.getCharTypeForDB(1) & ", " ' & " CONSTRAINT nn" & GetTableName() & "UsarDosTal NOT NULL, "
   SQL = SQL & "FacturaPreNumerada2 " & gUtilSQL.getCharTypeForDB(1) & ", " '& " CONSTRAINT nn" & GetTableName() & "FacturaPre NOT NULL, "
   SQL = SQL & "ModeloDeFactura2 " & gUtilSQL.getCharTypeForDB(1) & ", " '& " CONSTRAINT nn" & GetTableName() & "ModeloDeFa NOT NULL, "
   SQL = SQL & "PrimeraFactura2 " & gUtilSQL.getVarCharTypeForDB(11) & ", "
   SQL = SQL & "TipoDePrefijo2 " & gUtilSQL.getCharTypeForDB(1) & ", " '" CONSTRAINT nn" & GetTableName() & "TipoDePref NOT NULL, "
   SQL = SQL & "Prefijo2 " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "NombrePlantillaFactura2 " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ConsideraConciliadosLosMovIngresadosAntesDeFecha " & gUtilSQL.getCharTypeForDB(1) & ", " '& " CONSTRAINT nn" & GetTableName() & "ConsideraC NOT NULL, "
   SQL = SQL & "FechaDeInicioConciliacion " & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "DiaDelCierreFiscal " & gUtilSQL.getVarCharTypeForDB(2) & ", "
   SQL = SQL & "MesDelCierreFiscal " & gUtilSQL.getVarCharTypeForDB(2) & ", "
   SQL = SQL & "NombreYapellidor " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "CodTelfr " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "Telefonor " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "Direccionr " & gUtilSQL.getVarCharTypeForDB(255) & ", "
   SQL = SQL & "CiudadRepLegal" & gUtilSQL.getVarCharTypeForDB(100) & ", "
   SQL = SQL & "NumeroRifr " & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "CorreoElectronicoRepLegal " & gUtilSQL.getVarCharTypeForDB(40) & ", "
   SQL = SQL & "UsaCamposExtrasEnRenglonFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCamposE NOT NULL, "
   SQL = SQL & "PedirInformacionLibroVentasXlsalEmitirFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "PedirInfor NOT NULL, "
   SQL = SQL & "SugerirNumeroControlFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SugerirNum NOT NULL, "
   SQL = SQL & "EnDondeRetenerIva " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "PrimerNumeroComprobanteRetIva " & gUtilSQL.getNumericTypeForDB(9, 0) & ", "
   SQL = SQL & "FormaDeReiniciarElNumeroDeComprobanteRetIva " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ImprimirComprobanteContableDePago " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   SQL = SQL & "NumCopiasComprobanteMovBancario " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "NombrePlantillaComprobanteDeMovBancario " & gUtilSQL.getVarCharTypeForDB(60) & ", "
   SQL = SQL & "ConfirmarImpresionMovBancarioPorSecciones " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ConfirmarI NOT NULL, "
   SQL = SQL & "ImprimirComprobanteDeMovBancario " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   SQL = SQL & "IvaEsCostoEnCompras " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "IvaEsCosto NOT NULL, "
   SQL = SQL & "ImprimirComprobanteDeRetIva " & gUtilSQL.getCharTypeForDB(1) & ", " '" CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   SQL = SQL & "NumeroDeCopiasComprobanteRetencionIva " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "NombreContador " & gUtilSQL.getVarCharTypeForDB(20) & " CONSTRAINT nn" & GetTableName() & "NombreCont NOT NULL, "
   SQL = SQL & "CedulaContador " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NumeroCpc " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "ImprimirCentimosEnPlanilla " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCe NOT NULL, "
   SQL = SQL & "ModeloPlanillaForma00030 " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ImprimirCompContDespuesDeChequeMovBancario " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   SQL = SQL & "ImprimirCompraAlInsertar " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirCo NOT NULL, "
   SQL = SQL & "NombrePlantillaCompra " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ExigirRifdeClienteAlEmitirFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ExigirRIFD NOT NULL, "
   SQL = SQL & "UsarResumenDiarioDeVentas " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaResumen NOT NULL, "
   SQL = SQL & "UsaPrecioSinIvaEnResumenVtas " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaPrecioS NOT NULL, "
   SQL = SQL & "UsarRenglonesEnResumenVtas " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsarRenglo NOT NULL, "
   SQL = SQL & "ResumenVtasAfectaInventario " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ResumenVta NOT NULL, "
   SQL = SQL & "UnComprobanteDeRetIvaporHoja " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombrePlantillaComprobanteDeRetIva " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "GenerarNumCompDeRetIvasoloSiPorcentajeEsMayorAcero " & gUtilSQL.getCharTypeForDB(1) & ", " '& " CONSTRAINT nn" & GetTableName() & "GenerarNum NOT NULL, "
   SQL = SQL & "ExigirInformacionLibroDeCompras " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ExigirInfo NOT NULL, "
   SQL = SQL & "DetalleProdCompFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "DetallePro NOT NULL, "
   SQL = SQL & "DetalleProdCompCotizacion " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "DetallePro NOT NULL, "
   SQL = SQL & "FormaDeOrdenarDetalleFactura " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ImprimirBorradorAlInsertarFactura " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirBo NOT NULL, "
   SQL = SQL & "CuentaBancariaAnticipo " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "ConceptoBancarioAnticipoCobrado " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "ConceptoBancarioAnticipoPagado " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "ConceptoBancarioReversoAnticipoCobrado " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "ConceptoBancarioReversoAnticipoPagado " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "NombrePlantillaReciboDeAnticipoCobrado " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "NombrePlantillaReciboDeAnticipoPagado " & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "UsaCobroDirecto " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaCobroDi NOT NULL, "
   SQL = SQL & "CuentaBancariaCobroDirecto " & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "ConceptoBancarioCobroDirecto " & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "TipoDeNivelDePrecios " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "SugerirConsecutivoAnticipo " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "SugerirCon NOT NULL, "
   SQL = SQL & "TipoComprobanteDeAnticipoAimprimir " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "TomarEnCuentaRetencionesCeroParaArcvyRa " & gUtilSQL.getCharTypeForDB(1) & ", " '& " CONSTRAINT nn" & GetTableName() & "TomarEnCue NOT NULL, "
   SQL = SQL & "CambiarFechaEnCuotasLuegoDeFijarFechaEntrega " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "EsAsociadoEnCtaDeParticipacion " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "EsAsociado NOT NULL, "
   SQL = SQL & "ComisionesEnFactura " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ComisionesEnRenglones " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "NombreVendedorUno " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NombreVendedorDos " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "NombreVendedorTres " & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "MaximoDescuentoEnFactura " & gUtilSQL.getNumericTypeForDB(12, 2) & ", "
   SQL = SQL & "ItemsMonto " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "GenerarMovReversoSiAnulaPago" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ConceptoBancarioReversoDePago" & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "AccionAlAnularFactDeMesesAnt" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ColocarEnFacturaElVendedorAsinagoAlCliente" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ColocarEnF NOT NULL, "
   SQL = SQL & "ModeloFacturaModoTexto" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ModeloFacturaModoTexto2" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "PermitirDobleDescuentoEnFactura" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "PermitirDo NOT NULL, "
   SQL = SQL & "ComplConComodinEnBusqDeArtInv" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ComplConCo NOT NULL, "
   SQL = SQL & "ImprimirAnexoDeSerial" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirAn NOT NULL, "
   SQL = SQL & "NombrePlantillaAnexoSeriales" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "SinonimoGrupo" & gUtilSQL.getVarCharTypeForDB(30) & ", "
   SQL = SQL & "SinonimoTalla" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "SinonimoColor" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "SinonimoSerial" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "SinonimoRollo" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "ImprimirDatosClienteEnCompFiscal" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirDa NOT NULL, "
   SQL = SQL & "AsignarComisionDeVendedorEnCobranza" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AsignarCom NOT NULL, "
   SQL = SQL & "ManejaDebitoBancario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ManejaDebi NOT NULL, "
   SQL = SQL & "RedondeaMontoDebitoBancario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RedondeaMo NOT NULL, "
   SQL = SQL & "ManejaCreditoBancario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ManejaCred NOT NULL, "
   SQL = SQL & "RedondeaMontoCreditoBancario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RedondeaMo NOT NULL, "
   SQL = SQL & "FormatoDeFecha" & gUtilSQL.getVarCharTypeForDB(10) & ", "
   SQL = SQL & "ConcatenaLetraEaArticuloExento" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ConcatenaL NOT NULL, "
   SQL = SQL & "NumItemImprimirFactura" & gUtilSQL.getNumericTypeForDB(4, 0) & " CONSTRAINT nn" & GetTableName() & "NumItemImp NOT NULL, "
   SQL = SQL & "AccionLimiteItemsFactura" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AccionLimi NOT NULL, "
   SQL = SQL & "LimpiezaDeCotizacionXFactura" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "LimpiezaDe NOT NULL, "
   SQL = SQL & "VerificarDocumentoSinContabilizar" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "LimpiezaDe NOT NULL, "
   SQL = SQL & "NombrePlantillaCodigoDeBarras" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ImprimeDireccionAlFinalDelComprobanteFiscal" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimeDir NOT NULL, "
   SQL = SQL & "BloquearNumeroCobranza" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "BloquearNu NOT NULL, "
   SQL = SQL & "AsociaCentroDeCostoyAlmacen" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "BloquearNu NOT NULL, "
   SQL = SQL & "CambiarCobradorVendedor" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "CambiarCob NOT NULL, "
   SQL = SQL & "MetodoDeCosteo" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "FechaDesdeUsoMetodoDeCosteo" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "FechaContabilizacionDeCosteo" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "CalculoAutomaticoDeCosto" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "CalculoAut NOT NULL, "
   SQL = SQL & "ComprobanteCostoDetallado" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Comprobant NOT NULL, "
   SQL = SQL & "NCPreNumerada" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "NCPreNumer NOT NULL, "
   SQL = SQL & "TipoDePrefijoNC" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "PrefijoNC" & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "NDPreNumerada" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "NDPreNumer NOT NULL, "
   SQL = SQL & "TipoDePrefijoND" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "PrefijoND" & gUtilSQL.getVarCharTypeForDB(5) & ", "
   SQL = SQL & "TipoDeAgrupacionParaLibrosDeVenta" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "CantidadDeDecimales" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "IntegracionRIS" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "IntegRIS NOT NULL, "
   SQL = SQL & "AvisoDeFacturacionMenor" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AvisoDeFac NOT NULL, "
   SQL = SQL & "UsaMismoNumeroCompRetTodasCxP" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaMismoNu NOT NULL, "
   SQL = SQL & "TipoNegocio" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "FechaMinimaIngresarDatos" & gUtilSQL.getDateTypeForDB() & ", "
   SQL = SQL & "NombreCampoDefinibleCliente1" & gUtilSQL.getVarCharTypeForDB(20) & ", "
   SQL = SQL & "OrdenarCxPPorFacturaDocumento" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "OrdenarCxP NOT NULL, "
   SQL = SQL & "BuscarArticuloXSerialAlFacturar" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "BuscarArti NOT NULL, "
   SQL = SQL & "AvisoDeReservasvencidas" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "AvisoDeRes NOT NULL, "
   SQL = SQL & "ImprimirTipoCobroEnFactura" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirTi NOT NULL, "
   SQL = SQL & "VerificarStock" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "VerificarS NOT NULL, "
   SQL = SQL & "ImprimirTransferenciaAlInsertar" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirTransferencia NOT NULL, "
   SQL = SQL & "BeneficiarioGenerico" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Beneficiar NOT NULL, "
   SQL = SQL & "ConceptoBancarioReversoSolicitudDePago" & gUtilSQL.getVarCharTypeForDB(8) & ", "
   SQL = SQL & "NombrePlantillaComprobanteDePagoSueldo" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ActivarFacturacionPorAlmacen" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ActivarFac NOT NULL, "
   SQL = SQL & "EnDondeRetenerISLR " & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ImprimirNotaESconPrecio " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimirNo NOT NULL, "
   SQL = SQL & "AutorellenaResumenDiario" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Autorellen NOT NULL, "
   SQL = SQL & "RetieneImpuestoMunicipal" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "RetieneImp NOT NULL, "
   SQL = SQL & "NombrePlantillaRetencionImpuestoMunicipal" & gUtilSQL.getVarCharTypeForDB(50) & ", "
   SQL = SQL & "ModeloNotaEntrega " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ModeloNoEn NOT NULL, "
   SQL = SQL & "NotaEntregaPreNumerada " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "NotaEntregaPre NOT NULL, "
   SQL = SQL & "PrimeraNotaEntrega " & gUtilSQL.getVarCharTypeForDB(11) & " CONSTRAINT nn" & GetTableName() & "PrimeraNoEn NOT NULL, "
   SQL = SQL & "TipoPrefijoNotaEntrega " & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "TipoPrefijoNoEn NOT NULL, "
   SQL = SQL & "PrefijoNotaEntrega " & gUtilSQL.getVarCharTypeForDB(5) & " CONSTRAINT nn" & GetTableName() & "PrefijoNoEn NOT NULL, "
   SQL = SQL & "NombrePlantillaNotaEntrega " & gUtilSQL.getVarCharTypeForDB(50) & " CONSTRAINT nn" & GetTableName() & "NombrePlantillaNoEn NOT NULL, "
   SQL = SQL & "NombrePlantillaOrdenDeDespacho " & gUtilSQL.getVarCharTypeForDB(50) & " CONSTRAINT nn" & GetTableName() & "NombrePlantillaOrDe NOT NULL, "
   SQL = SQL & "NumCopiasOrdenDeDespacho " & gUtilSQL.getNumericTypeForDB(4, 0) & ", "
   SQL = SQL & "UsaNotaEntrega" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "UsaNoEn NOT NULL, "
   SQL = SQL & "ConsecutivoAlmacenGenerico" & gUtilSQL.getNumericTypeForDB(9, 0) & " CONSTRAINT nn" & GetTableName() & "Consecutiv NOT NULL, "
   SQL = SQL & "ValidarArticulosAlGenerarFactura" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "Validarart NOT NULL, "
   SQL = SQL & "CampoCodigoAlternativoDeArticulo" & gUtilSQL.getCharTypeForDB(1) & ", "
   SQL = SQL & "ImprimeSerialRolloLuegoDeDescripArticulo" & gUtilSQL.getCharTypeForDB(1) & " CONSTRAINT nn" & GetTableName() & "ImprimeSer NOT NULL, "
   SQL = SQL & gDefDatabase.getFN_TIMESTAMP() & " timestamp )"
   gDbUtil.Execute gDefDatabase.Conexion, SQL
   sAddPrimaryKeyAndOtherConstrains
   sAllowCeroLengthFields
   sModifyOtherPropertiesOfFields
   If valShowMessage Then
      gMessage.exito "La Tabla '" & CM_MESSAGE_NAME & "' fue creada exitosamente"
   End If
   fCreateTable = True
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fCreateTable = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
   "fCreateTable", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
