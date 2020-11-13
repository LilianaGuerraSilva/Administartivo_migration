VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsVendedorSQL"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Const CM_FILE_NAME As String = "clsCotizacionSQL"
Private Const CM_MESSAGE_NAME As String = "Cotizacion SQL"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Public Function fSQLVendedoresXLinea(ByVal valCantidadAImprimirVendedor As String, ByVal valcodigoDeVendedor As String, ByVal valConsecutivoCompaniaActual As Long)
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT vendedor.Codigo, "
   SQL = SQL & "vendedor.Nombre, "
   SQL = SQL & "RenglonComisionesDeVendedor.NombreDeLineaDeProducto as Linea, "
   SQL = SQL & "RenglonComisionesDeVendedor.Monto as Monto, "
   SQL = SQL & "RenglonComisionesDeVendedor.Porcentaje as Porcentaje, "
   SQL = SQL & "RenglonComisionesDeVendedor.TipoDeComision"
   SQL = SQL & " FROM vendedor INNER JOIN RenglonComisionesDeVendedor"
   SQL = SQL & " ON vendedor.ConsecutivoCompania = "
   SQL = SQL & "RenglonComisionesDeVendedor.ConsecutivoCompania"
   SQL = SQL & " AND vendedor.Codigo = "
   SQL = SQL & "RenglonComisionesDeVendedor.CodigoVendedor"
   SQL = SQL & " WHERE ( vendedor.ConsecutivoCompania"
   SQL = SQL & " = " & valConsecutivoCompaniaActual & "  )"
   If valCantidadAImprimirVendedor = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND ( vendedor.Codigo"
      SQL = SQL & " = '" & valcodigoDeVendedor & "') "
   End If
   SQL = SQL & " ORDER BY vendedor.Codigo"
h_EXIT: On Error GoTo 0
   fSQLVendedoresXLinea = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLVendedoresXLinea", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLListadoDeVendedores(ByVal valCantidadAImprimirVendedor As String, ByVal valcodigoDeVendedor As String, ByVal valConsecutivoCompaniaActual As Long) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "SELECT vendedor.Codigo, vendedor.Nombre, vendedor.RIF, " & gUtilSQL.DfSQLCaseIfForEnum("vendedor.StatusVendedor", enum_StatusVendedor.eSV_ACTIVO, fEnumStatusDelVendedorToStringInArray(True), "Status") & ", vendedor.Direccion, vendedor.Telefono, vendedor.email"
   SQL = SQL & " FROM vendedor "
   SQL = SQL & " WHERE vendedor.ConsecutivoCompania = " & valConsecutivoCompaniaActual
   If valCantidadAImprimirVendedor = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND ( vendedor.Codigo"
      SQL = SQL & " = '" & valcodigoDeVendedor & "')"
   End If
h_EXIT: On Error GoTo 0
   fSQLListadoDeVendedores = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLListadoDeVendedores", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLVendedoresXCliente(ByVal valCantidadAImprimirVendedor As String, ByVal valcodigoDeVendedor As String, ByVal valConsecutivoCompaniaActual As Long, ByVal valFiltrarPorClientes As Boolean, ByVal valFiltroCliente As Integer)
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = " SELECT vendedor.codigo as codigoVendedor, vendedor.Nombre as nombreVendedor,  cliente.Codigo as CodigoCliente, cliente.Nombre as NombreCliente, Cliente.NumeroRif as NumeroRif, cliente.Direccion as Direccion, cliente.Telefono as telefono"
   SQL = SQL & " FROM vendedor INNER JOIN cliente ON (vendedor.Codigo = cliente.CodigoVendedor) AND (vendedor.ConsecutivoCompania = cliente.ConsecutivoCompania)"
   SQL = SQL & " WHERE  Vendedor.consecutivoCompania = " & valConsecutivoCompaniaActual
   If valCantidadAImprimirVendedor = enumCantidadAImprimirToString(eCI_uno) Then
      SQL = SQL & " AND ( VENDEDOR.Codigo"
      SQL = SQL & " = '" & valcodigoDeVendedor & "') "
   End If
   If valFiltrarPorClientes Then
      SQL = SQL & " AND ( cliente.status = " & gUtilSQL.fSQLSimpleValueForEnum(valFiltroCliente) & " ) "
   End If
   SQL = SQL & " Order by vendedor.Codigo "
h_EXIT: On Error GoTo 0
   fSQLVendedoresXCliente = SQL
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fSQLVendedoresXCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fSQLUpdateCambiaElNombreDeLaCompania(ByVal valNombreCiudadOriginal As String, ByVal valNuevoNombreCiudad As String, ByVal gProyCompaniaActual As Object) As String
   Dim SQL As String
   On Error GoTo h_ERROR
   SQL = "UPDATE vendedor "
   SQL = SQL & "SET vendedor.Ciudad = " & gUtilSQL.fSimpleSqlValue(valNuevoNombreCiudad)
   SQL = SQL & " WHERE vendedor.ConsecutivoCompania = " & gProyCompaniaActual.GetConsecutivoCompania
   SQL = SQL & " AND vendedor.Ciudad = " & gUtilSQL.fSimpleSqlValue(valNombreCiudadOriginal)
h_EXIT:
   fSQLUpdateCambiaElNombreDeLaCompania = SQL
   On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLUpdateCambiaElNombreDeLaCompania", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
