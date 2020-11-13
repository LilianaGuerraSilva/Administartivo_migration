VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsLibSawVista"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
Private Const CM_FILE_NAME As String = "clsLibSawVista"
Private Const CM_MESSAGE_NAME As String = "Lib Saw Vista"

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

'***Sacar una vez que este en libreria
Public Function fCollateModernSpanishCaseSensitiveAccentSensitive() As String
   Dim vSql As String
   On Error GoTo h_ERROR
   vSql = " COLLATE Modern_Spanish_CS_AS "
   fCollateModernSpanishCaseSensitiveAccentSensitive = vSql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, _
         "fCollateModernSpanishCaseSensitiveAccentSensitive", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

'*****Sacar una vez que esté en librería
Public Function fSqlReplaceSet(ByVal valSqlExpression As String, ByVal valSetToSearch As Variant, ByVal valSetForReplace As Variant) As String
   Dim vUseCaseSensitive As Boolean
   Dim vSql As String
   Dim i As Long
   Dim vStartToSearch As Long
   Dim vEndToSearch As Long
   Dim vEndToReplace As Long
   Dim vSqlCollationCaseSensitive As String
   Dim vTextToSearch As String
   Dim vTextToReplace As String
   On Error GoTo h_ERROR
   vSql = valSqlExpression
   If (gTexto.DfLen(vSql) > 0) Then
      vUseCaseSensitive = False
      vStartToSearch = LBound(valSetToSearch)
      vEndToSearch = UBound(valSetToSearch) - 1
      
      vEndToReplace = UBound(valSetForReplace) - 1
      
      If (vUseCaseSensitive) Then
         vSqlCollationCaseSensitive = " COLLATE Modern_Spanish_CS_AS "
      Else
         vSqlCollationCaseSensitive = ""
      End If
      If (vEndToSearch > vEndToReplace) Then 'Quitar mensajes al pasar a LibGalac
         gMessage.ProgrammerMessage "El arreglo de búsqueda es más grande que el arreglo de reemplazo. Los reemplazos faltantes serán sustituidos por ''. Debe corregir esta situación"
      ElseIf (vEndToSearch < vEndToReplace) Then
         gMessage.ProgrammerMessage "El arreglo de reemplazo es más grande que el arreglo de búsqueda. Los valores de remplazo sobrantes no serán tomados en cuenta. Debe corregir esta situación."
      End If
      
      For i = vStartToSearch To vEndToSearch
         vTextToSearch = valSetToSearch(i)
         If (i <= vEndToReplace) Then
            vTextToReplace = valSetForReplace(i)
         Else
            vTextToReplace = ""
         End If
         vSql = " REPLACE( " & vSql & vSqlCollationCaseSensitive & ", " & gUtilSQL.fSimpleSqlValue(vTextToSearch) & ", " & gUtilSQL.fSimpleSqlValue(vTextToReplace) & ") "
      Next
   End If
   fSqlReplaceSet = vSql
h_EXIT:
   On Error GoTo 0
   Exit Function
h_ERROR:
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlReplaceSet", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


