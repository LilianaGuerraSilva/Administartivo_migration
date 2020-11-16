VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsAssemblys"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsAssemblys"
End Function

Private Function CM_MESSAGE_NAME() As String
   CM_MESSAGE_NAME = "clsAssemblys"
End Function

Public Function fCreateDropSawS10AssemblysFunctions(ByVal valCreate As Boolean, ByVal valShowMessage As Boolean, ByVal valDllFullPath As String) As Boolean
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   If valCreate Then
      vResult = fCreateSawS10AssemblysFunctions(valDllFullPath)
      If valShowMessage Then
         If vResult Then
            gMessage.exito "El Assembly SawS10 y las funciones que lo invocan han sido creadas con éxito."
         Else
            gMessage.Advertencia "El Assembly SawS10 y sus funciones no han podido ser creadas."
         End If
      End If
   Else
      vResult = fDropSawS10AssemblysFunctions()
      If valShowMessage Then
         If vResult Then
            gMessage.exito "El Assembly SawS10 y las funciones que lo invocan han sido eliminadas con éxito."
         Else
            gMessage.Advertencia "El Assembly SawS10 y sus funciones no han podido ser eliminadas."
         End If
      End If
   End If
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateDropSawS10AssemblysFunctions", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fCreateSawS10AssemblysFunctions(ByVal valDllFullPathSawS10Assemby As String) As Boolean
   Dim vResult As Boolean
   Dim vSql As String
   On Error GoTo h_ERROR
   vResult = False
   If (valDllFullPathSawS10Assemby <> "") Then
      vSql = fSqlCreateGalacSawS10AssemblysFunctions(valDllFullPathSawS10Assemby)
      gDbUtil.sExecuteWithoutAddSetDateFormat gDefDatabase.Conexion, vSql
      vResult = True
   End If
   fCreateSawS10AssemblysFunctions = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSawS10AssemblysFunctions", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fDropSawS10AssemblysFunctions() As Boolean
   Dim vResult As Boolean
   Dim vSql As String
   On Error GoTo h_ERROR
   vResult = False
   vSql = fSqlDropGalacSawS10AssemblysFunctions()
   gDbUtil.sExecuteWithoutAddSetDateFormat gDefDatabase.Conexion, vSql
   vSql = fSqlDropSawS10Assembly()
   gDbUtil.sExecuteWithoutAddSetDateFormat gDefDatabase.Conexion, vSql
   vResult = True
   fDropSawS10AssemblysFunctions = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSawS10AssemblysFunctions", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDropGalacSawS10AssemblysFunctions() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Gf_CuadrarCodigo') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))" & vbCrLf
   vResult = vResult & "   DROP FUNCTION Gf_CuadrarCodigo" & vbCrLf
   vResult = vResult & "GO" & vbCrLf & vbCrLf
   
   vResult = vResult & "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Gf_CuentaNivelAnterior') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))" & vbCrLf
   vResult = vResult & "   DROP FUNCTION Gf_CuentaNivelAnterior" & vbCrLf
   vResult = vResult & "GO" & vbCrLf & vbCrLf
   
   vResult = vResult & "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Gf_CuentaPrimerNivel') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))" & vbCrLf
   vResult = vResult & "   DROP FUNCTION Gf_CuentaPrimerNivel" & vbCrLf
   vResult = vResult & "GO" & vbCrLf & vbCrLf
   
   vResult = vResult & "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Gf_CuentasNivelesPrevios') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))" & vbCrLf
   vResult = vResult & "   DROP FUNCTION Gf_CuentasNivelesPrevios" & vbCrLf
   vResult = vResult & "GO" & vbCrLf & vbCrLf
   
   vResult = vResult & "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.Gf_NivelDelCodigo') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))" & vbCrLf
   vResult = vResult & "   DROP FUNCTION Gf_NivelDelCodigo" & vbCrLf
   vResult = vResult & "GO" & vbCrLf & vbCrLf
   
   fSqlDropGalacSawS10AssemblysFunctions = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDropGalacSawS10AssemblysFunctions", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlDropSawS10Assembly() As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "EXEC sp_configure 'CLR_ENABLED', 1;" & vbCrLf
   vResult = vResult & "RECONFIGURE" & vbCrLf
   vResult = vResult & "IF (SELECT ASSEMBLYPROPERTY('GalacSawS10', 'SimpleName')) IS NOT NULL" & vbCrLf
   vResult = vResult & "      DROP ASSEMBLY GalacSawS10" & vbCrLf & vbCrLf

   fSqlDropSawS10Assembly = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlDropSawS10Assembly", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fSqlCreateGalacSawS10AssemblysFunctions(ByVal valDllFullPath As String) As String
   Dim vResult As String
   On Error GoTo h_ERROR
   vResult = "SET ANSI_NULLS ON" & vbCrLf
   vResult = vResult & "SET QUOTED_IDENTIFIER ON" & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   
   'Se eliminan aquellas funciones que usen el Assembly, validando primero si existen
   vResult = vResult & fSqlDropGalacSawS10AssemblysFunctions()
   
   'Si el Assemby existe, se elimina para volver a crearlo
   vResult = vResult & fSqlDropSawS10Assembly()
   
   'Se crea el Assembly segun el path proporcionado
   vResult = vResult & "DECLARE @Sql varchar(max)" & vbCrLf
   vResult = vResult & "DECLARE @Path varchar(max)" & vbCrLf
   vResult = vResult & "SET @Path = '" & valDllFullPath & vbCrLf '   vResult = vResult & "SET @Path = 'C:\cvs\dotnet\Administrativo\Galac.Saw.S10\bin\Galac.Saw.S10.dll'" & vbCrLf
   vResult = vResult & "SET @Sql = 'CREATE ASSEMBLY GalacSawS10 AUTHORIZATION dbo FROM ''' + @Path + ''' WITH PERMISSION_SET = SAFE '" & vbCrLf
   vResult = vResult & "EXEC(@Sql)" & vbCrLf
   vResult = vResult & "GO" & vbCrLf & vbCrLf
   
   'Se vuelven a crear las funciones que usan el Assembly
   vResult = vResult & "CREATE FUNCTION dbo.Gf_CuadrarCodigo(@CodigoCuentaContable nvarchar(max), @Niveles nvarchar(max))" & vbCrLf
   vResult = vResult & "RETURNS nvarchar(max) WITH EXECUTE AS CALLER" & vbCrLf
   vResult = vResult & "AS EXTERNAL NAME GalacSawS10.SPComplements.CuadrarCodigo" & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   vResult = vResult & "PRINT 'CREATED Gf_CuadrarCodigo'" & vbCrLf & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   
   vResult = vResult & "CREATE FUNCTION dbo.Gf_CuentaNivelAnterior(@CodigoCuentaContable nvarchar(max), @Niveles nvarchar(max))" & vbCrLf
   vResult = vResult & "RETURNS nvarchar(max) WITH EXECUTE AS CALLER" & vbCrLf
   vResult = vResult & "AS EXTERNAL NAME GalacSawS10.SPComplements.CuentaNivelAnterior" & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   vResult = vResult & "PRINT 'CREATED Gf_CuentaNivelAnterior'" & vbCrLf & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   
   vResult = vResult & "CREATE FUNCTION dbo.Gf_CuentaPrimerNivel(@CodigoCuentaContable nvarchar(max), @Niveles nvarchar(max))" & vbCrLf
   vResult = vResult & "RETURNS nvarchar(max) WITH EXECUTE AS CALLER" & vbCrLf
   vResult = vResult & "AS EXTERNAL NAME GalacSawS10.SPComplements.CuentaPrimerNivel" & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   vResult = vResult & "PRINT 'CREATED Gf_CuentaPrimerNivel'" & vbCrLf & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   
   vResult = vResult & "CREATE FUNCTION dbo.Gf_CuentasNivelesPrevios(@CodigoCuentaContable nvarchar(max), @Niveles nvarchar(max))" & vbCrLf
   vResult = vResult & "RETURNS nvarchar(max) WITH EXECUTE AS CALLER" & vbCrLf
   vResult = vResult & "AS EXTERNAL NAME GalacSawS10.SPComplements.CuentasNivelesPrevios" & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   vResult = vResult & "PRINT 'CREATED Gf_CuentasNivelesPrevios'" & vbCrLf & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   
   vResult = vResult & "CREATE FUNCTION dbo.Gf_NivelDelCodigo(@CodigoCuentaContable nvarchar(max), @Niveles nvarchar(max))" & vbCrLf
   vResult = vResult & "RETURNS int WITH EXECUTE AS CALLER" & vbCrLf
   vResult = vResult & "AS EXTERNAL NAME GalacSawS10.SPComplements.NivelDelCodigo" & vbCrLf
   vResult = vResult & "GO" & vbCrLf
   vResult = vResult & "PRINT 'CREATED Gf_NivelDelCodigo'" & vbCrLf & vbCrLf
   
   fSqlCreateGalacSawS10AssemblysFunctions = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSqlCreateGalacSawS10AssemblysFunctions", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
