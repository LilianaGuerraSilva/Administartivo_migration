VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsProveedorVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsProveedorVista"
Private Const CM_MESSAGE_NAME As String = "Vista Proveedor"
Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function GetViewNameProveedor() As String
   GetViewNameProveedor = "IGV_Proveedor"
End Function
Public Function fSqlViewProveedor() As String
   Dim vSQL As String
   vSQL = " SELECT ConsecutivoCompania AS ConsecutivoCompania, CodigoProveedor AS CodigoProveedor, NombreProveedor AS NombreProveedor,"
   vSQL = vSQL & " Contacto AS Contacto, NumeroRIF AS NumeroRIF, NumeroNIT AS NumeroNIT, Telefonos AS Telefonos,"
   vSQL = vSQL & " Direccion AS Direccion, Fax AS Fax, email AS email, PorcentajeRetencionIva AS PorcentajeRetencionIva   ,"
   vSQL = vSQL & " TipoDeProveedorDeLibrosFiscales AS TipoProveedor, CuentaContableCxP AS CuentaContableCxP, CuentaContableGastos AS CuentaContableGastos  ,"
   vSQL = vSQL & " NombreOperador AS NombreOperador, FechaUltimaModificacion AS FechaUltimaModificacion   ,"
   vSQL = vSQL & " CuentaContableGastos As CuentaIvaGastoCosto"
   vSQL = vSQL & " From Proveedor"
   fSqlViewProveedor = vSQL
h_EXIT:     On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlViewProveedor", _
         CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
