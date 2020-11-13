VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLoteSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsLoteSQL"
Private Const CM_MESSAGE_NAME As String = "SQL Lote"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLListadoDeDocumentosAsociadosAUnLote(ByVal valConsecutivoCompania As Long, ByVal valCodigoLote As String)
   Dim vSQL As String
   Dim vSQLWhere As String
   On Error GoTo h_ERROR
   vSQLWhere = "WHERE ConsecutivoCompania = " & valConsecutivoCompania & " AND CodigoLote = " & gUtilSQL.fSimpleSqlValue(valCodigoLote) & " "
   vSQL = "SELECT Numero AS DocumentNumber, " & gUtilSQL.fSimpleSqlValue("FACTURA") & " AS TypeDoc, CodigoLote FROM factura "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & "UNION "
   vSQL = vSQL & "SELECT Numero AS DocumentNumber, " & gUtilSQL.fSimpleSqlValue("CxC") & " AS TypeDoc, CodigoLote FROM cxC "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & "UNION "
   vSQL = vSQL & "SELECT Numero AS DocumentNumber, " & gUtilSQL.fSimpleSqlValue("CxP") & " AS TypeDoc, CodigoLote FROM cxP "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & "UNION "
   vSQL = vSQL & "SELECT Codigo AS DocumentNumber, " & gUtilSQL.fSimpleSqlValue("CLIENTE") & " AS TypeDoc, CodigoLote FROM cliente "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & "UNION "
   vSQL = vSQL & "SELECT CodigoProveedor  AS DocumentNumber, " & gUtilSQL.fSimpleSqlValue("PROVEEDOR") & " AS TypeDoc, CodigoLote FROM proveedor "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & "UNION "
   vSQL = vSQL & "SELECT Codigo  AS DocumentNumber, " & gUtilSQL.fSimpleSqlValue("VENDEDOR") & " AS TypeDoc, CodigoLote FROM vendedor "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & "UNION "
   vSQL = vSQL & "SELECT Codigo  AS DocumentNumber, " & gUtilSQL.fSimpleSqlValue("ARTICULO INVENTARIO") & " AS TypeDoc, CodigoLote FROM articuloInventario "
   vSQL = vSQL & vSQLWhere
   vSQL = vSQL & "ORDER BY TypeDoc, CodigoLote "
h_EXIT: On Error GoTo 0
   fSQLListadoDeDocumentosAsociadosAUnLote = vSQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLListadoDeDocumentosAsociadosAUnLote", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
