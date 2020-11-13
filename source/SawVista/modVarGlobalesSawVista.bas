Attribute VB_Name = "modVarGlobalesSawVista"
Option Explicit
Public gError As clsError
Public gErrorCodesIG As clsErrorCodesIG
Public gMessage As clsMessage
Public gTexto As clsTexto
Public gDefgen As clsDefgen
Public gConvert As clsConvert
Public gApi As clsDefensiveAPI
Public gUtilFile As clsUtilFiles
Public gUtilDate As clsUtilDate
Public gUtilMathOperations As clsUtilMathOperations
Public gDefProg As clsImplementDefProg
Public gUtilSQL As clsUtilSQLStatement
Public gDbUtil As clsDBUtil
Public gEnumReport As clsEnumReport
Public Enum enum_CantidadAImprimir
   eCI_uno = 0
   eCI_TODOS
End Enum

Public Enum enum_TipoDeArticulo
   eTD_MERCANCIA = 0
   eTD_SERVICIO
   eTD_PRODUCTO_COMPUESTO
End Enum

Public Function enumCantidadAImprimirToString(CantidadAImprimir As enum_CantidadAImprimir) As String
   Select Case CantidadAImprimir
      Case eCI_uno: enumCantidadAImprimirToString = "Uno"
      Case eCI_TODOS: enumCantidadAImprimirToString = "Todos"
   End Select
End Function
Public Function enumTipoDeArticuloToString(ByVal tipoDeArticulo As enum_TipoDeArticulo) As String
   Select Case tipoDeArticulo
      Case eTD_MERCANCIA: enumTipoDeArticuloToString = "Mercancía"
      Case eTD_SERVICIO: enumTipoDeArticuloToString = "Servicio"
      Case eTD_PRODUCTO_COMPUESTO: enumTipoDeArticuloToString = "Producto Compuesto"
      Case Else: enumTipoDeArticuloToString = enumTipoDeArticuloToString(0)
   End Select
End Function

Public Function strTipoDeArticuloToNum(ByVal tipoDeArticulo As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_TipoDeArticulo
   Dim nCount As Integer
   nCount = 0
   For mEnum = eTD_MERCANCIA To eTD_PRODUCTO_COMPUESTO
      If UCase(tipoDeArticulo) = UCase(enumTipoDeArticuloToString(mEnum)) Then
         strTipoDeArticuloToNum = nCount
         GoTo h_EXIT
      End If
      nCount = nCount + 1
   Next
   strTipoDeArticuloToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & tipoDeArticulo & " no existe en tipo enumerativo tipoDeArticulo"
   End If
h_EXIT:
   Exit Function
End Function

Public Function enumStatusVendedorToString(ByVal statusVendedor As enum_StatusVendedor) As String
   Select Case statusVendedor
      Case eSV_ACTIVO: enumStatusVendedorToString = "Activo"
      Case eSV_INACTIVO: enumStatusVendedorToString = "Inactivo"
      Case eSV_RESTRINGIDO: enumStatusVendedorToString = "Restringido"
      Case Else: enumStatusVendedorToString = enumStatusVendedorToString(0)
   End Select
End Function

Public Function strStatusVendedorToNum(ByVal statusVendedor As String, Optional ByVal showMessage As Boolean = True) As Integer
   Dim mEnum As enum_StatusVendedor
   Dim nCount As Integer
   nCount = 0
   For mEnum = eSV_ACTIVO To eSV_RESTRINGIDO
   If UCase(statusVendedor) = UCase(enumStatusVendedorToString(mEnum)) Then
      strStatusVendedorToNum = nCount
      GoTo h_EXIT
   End If
   nCount = nCount + 1
   Next
   strStatusVendedorToNum = 0
   If showMessage Then
      gMessage.ProgrammerMessage "Valor " & statusVendedor & " no existe en tipo enumerativo statusVendedor"
   End If
h_EXIT:
   Exit Function
End Function

Public Function fEnumStatusDelVendedorToStringInArray(Optional ByVal valPrepararParaSql As Boolean = False) As Variant
   Dim varEnum As enum_StatusVendedor
   Dim varArray() As String
   Dim varLimiteSuperior As Integer
   Dim nCount As Integer
   Dim varCaracterDelimitador As String
   On Error Resume Next
   varCaracterDelimitador = ""
   If valPrepararParaSql Then
      varCaracterDelimitador = "'"
   End If
   varLimiteSuperior = enum_StatusVendedor.eSV_RESTRINGIDO - enum_StatusVendedor.eSV_ACTIVO
   nCount = 0
   ReDim varArray(0 To varLimiteSuperior)
   For varEnum = enum_StatusVendedor.eSV_ACTIVO To enum_StatusVendedor.eSV_RESTRINGIDO
      varArray(nCount) = varCaracterDelimitador & enumStatusVendedorToString(varEnum) & varCaracterDelimitador
      nCount = nCount + 1
   Next
   fEnumStatusDelVendedorToStringInArray = varArray
   On Error GoTo 0
End Function
