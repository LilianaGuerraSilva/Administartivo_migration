VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsClienteVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsClienteVista"
Private Const CM_MESSAGE_NAME As String = "Vista Cliente"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function getCodigoClienteResumen() As String
   getCodigoClienteResumen = "RD_Cliente"
End Function

Public Function GetViewNameFichaCliente() As String
   GetViewNameFichaCliente = "IGV_FichaCliente_B1"
End Function

Public Function fSqlViewFichaCliente() As String
   Dim vSQL As String
   Dim gEnumProyecto As clsEnumAdministrativo
   On Error GoTo h_ERROR
   Set gEnumProyecto = New clsEnumAdministrativo
   vSQL = ""
   vSQL = "SELECT "
   vSQL = vSQL & "Cliente.ConsecutivoCompania as ConsecutivoCompania, "
   vSQL = vSQL & "Cliente.Codigo as Codigo, "
   vSQL = vSQL & "Cliente.Nombre as NombreCliente, "
   vSQL = vSQL & gUtilSQL.DfSQLCaseIfForEnum("Cliente.Status", _
         enum_StatusCliente.eSC_ACTIVO, _
         gEnumProyecto.fEnumStatusClienteToStringInArray(True), "Status") & ","
   vSQL = vSQL & "Cliente.Direccion as Direccion, "
   vSQL = vSQL & "Cliente.Contacto as Contacto, "
   vSQL = vSQL & "Cliente.Telefono, "
   vSQL = vSQL & "Cliente.FAX as Fax, "
   vSQL = vSQL & "Cliente.Ciudad as Ciudad,  "
   vSQL = vSQL & "Cliente.ZonaPostal as ZonaPostal, "
   vSQL = vSQL & "Cliente.NumeroRIF as NumeroRIF, "
   vSQL = vSQL & "Cliente.NumeroNIT as NumeroNIT, "
   vSQL = vSQL & "Cliente.Email as Email, "
   vSQL = vSQL & "Adm.Vendedor.Nombre as NombreVendedor"
   vSQL = vSQL & " FROM Adm.Vendedor INNER JOIN Cliente"
   vSQL = vSQL & " ON (Adm.Vendedor.Consecutivo = Cliente.ConsecutivoVendedor)"
   vSQL = vSQL & " AND (Adm.Vendedor.ConsecutivoCompania = Cliente.ConsecutivoCompania)"
   vSQL = vSQL & " WHERE cliente.Codigo <> " & gUtilSQL.fSimpleSqlValue(getCodigoClienteResumen)
   fSqlViewFichaCliente = vSQL
   Set gEnumProyecto = Nothing
   
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewFichaCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function GetViewNameClientexFactura() As String
   GetViewNameClientexFactura = "IGV_ClientexFactura"
End Function

Public Function fSqlViewClientexFactura() As String
   Dim vSQL As String
   On Error GoTo h_ERROR
   vSQL = "SELECT Cliente.Codigo, Cliente.Nombre, factura.Numero, factura.Fecha, factura.TotalFactura, Cliente.ConsecutivoCompania"
   vSQL = vSQL & " FROM  factura INNER JOIN"
   vSQL = vSQL & " Cliente ON factura.ConsecutivoCompania = Cliente.ConsecutivoCompania AND factura.CodigoCliente = Cliente.Codigo"
   vSQL = vSQL & " WHERE (factura.StatusFactura = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ") "
   vSQL = vSQL & " AND (factura.TipoDeDocumento IN (" & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_FACTURA) & "," & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_COMPROBANTEFISCAL) & ")) "
h_EXIT:     On Error GoTo 0
   fSqlViewClientexFactura = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewClientexFactura", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
