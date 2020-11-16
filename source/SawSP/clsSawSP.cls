VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsSawSP"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Function CM_FILE_NAME() As String
   CM_FILE_NAME = "clsSawSp"
End Function

Private Function CM_MESSAGE_NAME() As String
CM_MESSAGE_NAME = "Saw Stored Procedures"
End Function

Public Function fCreateProjectStoredProcedures(ByVal valDllFullPathSawS10Assemby As String, ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insAssemblys As clsAssemblys
   Dim insAsiento As clsAsientoSp
   Dim insCentroDeCostos As clsCentroDeCostosSp
   Dim insCompania As clsCompaniaSP
   Dim insComprobante As clsComprobanteSp
   Dim insCuenta As clsCuentaSp
   Dim insCxC As clsCxCSp
   Dim insCxP As clsCxPSp
   Dim insGenerarComprobante As clsGenerarComprobanteSp
   Dim insGrupoDeActivos As clsGrupoDeActivosSp
   Dim insParametrosAdministrativos As clsParametrosAdministrativoSP
   Dim insParametros As clsParametrosSp
   Dim insProveedor As clsProveedorSP
   Dim insRendicion As clsRendicionSp
   Dim insTablaRetencion As clsTablaRetencionSp
   Dim insTipoProveedor As clsTipoProveedorSp
   Dim insUsuario As clsUsuarioSP
   Dim insArticuloInventarioSp As clsArticuloInventarioSp
   Dim insWcoSp  As clsWcoSp
   Dim insAnticipoSp As clsAnticipoSP

   On Error GoTo h_ERROR
   Set insAssemblys = New clsAssemblys
   Set insAsiento = New clsAsientoSp
   Set insCentroDeCostos = New clsCentroDeCostosSp
   Set insCompania = New clsCompaniaSP
   Set insComprobante = New clsComprobanteSp
   Set insCuenta = New clsCuentaSp
   Set insCxC = New clsCxCSp
   Set insCxP = New clsCxPSp
   Set insGenerarComprobante = New clsGenerarComprobanteSp
   Set insGrupoDeActivos = New clsGrupoDeActivosSp
   Set insParametrosAdministrativos = New clsParametrosAdministrativoSP
   Set insParametros = New clsParametrosSp
   Set insProveedor = New clsProveedorSP
   Set insRendicion = New clsRendicionSp
   Set insTablaRetencion = New clsTablaRetencionSp
   Set insTipoProveedor = New clsTipoProveedorSp
   Set insUsuario = New clsUsuarioSP
   Set insArticuloInventarioSp = New clsArticuloInventarioSp
   Set insWcoSp = New clsWcoSp
   Set insAnticipoSp = New clsAnticipoSP
   vResult = True
'   vResult = vResult And insAssemblys.fCreateDropSawS10AssemblysFunctions(True, valShowMessage, valDllFullPathSawS10Assemby)
   vResult = vResult And insParametros.fCreateDropParametrosSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insParametrosAdministrativos.fCreateDropParametrosAdministrativosSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insUsuario.fCreateDropUsuarioSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insCompania.fCreateDropCompaniaSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insCxC.fCreateDropCxCSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insTipoProveedor.fCreateDropTipoProveedorSP(True, valShowMessage, valWithEncryption)

   vResult = vResult And insCxP.fCreateDropCxPSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insGrupoDeActivos.fCreateDropGrupoDeActivosSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insCuenta.fCreateDropCuentaSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insCentroDeCostos.fCreateDropCentroDeCostosSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insComprobante.fCreateDropAsientoSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insAsiento.fCreateDropAsientoSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insGenerarComprobante.fCreateDropGenerarComprobanteSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insRendicion.fCreateDropRendicionSP(True, valShowMessage, valWithEncryption)
   vResult = vResult And insArticuloInventarioSp.fCreateDropGrupoDeArticuloInventarioSp(True, valShowMessage, valWithEncryption)
   vResult = vResult And insWcoSp.fCreateProjectStoredProcedures(valShowMessage, valWithEncryption)
   vResult = vResult And insAnticipoSp.fCreateDropAnticipoSp(True, valShowMessage, valWithEncryption)
   Set insWcoSp = Nothing
   Set insAssemblys = Nothing
   Set insAsiento = Nothing
   Set insCentroDeCostos = Nothing
   Set insCompania = Nothing
   Set insComprobante = Nothing
   Set insCuenta = Nothing
   Set insCxC = Nothing
   Set insCxP = Nothing
   Set insGenerarComprobante = Nothing
   Set insGrupoDeActivos = Nothing
   Set insParametrosAdministrativos = Nothing
   Set insParametros = Nothing
   Set insProveedor = Nothing
   Set insRendicion = Nothing
   Set insTablaRetencion = Nothing
   Set insTipoProveedor = Nothing
   Set insUsuario = Nothing
   Set insArticuloInventarioSp = Nothing
   Set insAnticipoSp = Nothing
   fCreateProjectStoredProcedures = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateProjectStoredProcedures", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Public Function fDropProjectStoredProcedures(ByVal valShowMessage As Boolean) As Boolean
   Dim vResult As Boolean
   Dim insAssemblys As clsAssemblys
   Dim insAsiento As clsAsientoSp
   Dim insCentroDeCostos As clsCentroDeCostosSp
   Dim insCompania As clsCompaniaSP
   Dim insComprobante As clsComprobanteSp
   Dim insCuenta As clsCuentaSp
   Dim insCxC As clsCxCSp
   Dim insCxP As clsCxPSp
   Dim insGenerarComprobante As clsGenerarComprobanteSp
   Dim insGrupoDeActivos As clsGrupoDeActivosSp
   Dim insParametrosAdministrativos As clsParametrosAdministrativoSP
   Dim insParametros As clsParametrosSp
   Dim insProveedor As clsProveedorSP
   Dim insRendicion As clsRendicionSp
   Dim insTablaRetencion As clsTablaRetencionSp
   Dim insTipoProveedor As clsTipoProveedorSp
   Dim insUsuario As clsUsuarioSP
   Dim insArticuloInventarioSp As clsArticuloInventarioSp
   Dim insWcoSp As clsWcoSp
   Dim insAnticipoSp As clsAnticipoSP
   On Error GoTo h_ERROR
   Set insAssemblys = New clsAssemblys
   Set insAsiento = New clsAsientoSp
   Set insCentroDeCostos = New clsCentroDeCostosSp
   Set insCompania = New clsCompaniaSP
   Set insComprobante = New clsComprobanteSp
   Set insCuenta = New clsCuentaSp
   Set insCxC = New clsCxCSp
   Set insCxP = New clsCxPSp
   Set insGenerarComprobante = New clsGenerarComprobanteSp
   Set insGrupoDeActivos = New clsGrupoDeActivosSp
   Set insParametrosAdministrativos = New clsParametrosAdministrativoSP
   Set insParametros = New clsParametrosSp
   Set insProveedor = New clsProveedorSP
   Set insRendicion = New clsRendicionSp
   Set insTablaRetencion = New clsTablaRetencionSp
   Set insTipoProveedor = New clsTipoProveedorSp
   Set insUsuario = New clsUsuarioSP
   Set insArticuloInventarioSp = New clsArticuloInventarioSp
   Set insWcoSp = New clsWcoSp
   Set insAnticipoSp = New clsAnticipoSP
   vResult = True
   vResult = vResult And insGenerarComprobante.fCreateDropGenerarComprobanteSP(False, valShowMessage)
   vResult = vResult And insAsiento.fCreateDropAsientoSP(False, valShowMessage)
   vResult = vResult And insComprobante.fCreateDropAsientoSP(False, valShowMessage)
   vResult = vResult And insCentroDeCostos.fCreateDropCentroDeCostosSP(False, valShowMessage)
   vResult = vResult And insCuenta.fCreateDropCuentaSP(False, valShowMessage)
   vResult = vResult And insGrupoDeActivos.fCreateDropGrupoDeActivosSP(False, valShowMessage)
   vResult = vResult And insCxP.fCreateDropCxPSP(False, valShowMessage)
   vResult = vResult And insTipoProveedor.fCreateDropTipoProveedorSP(False, valShowMessage)
   vResult = vResult And insCxC.fCreateDropCxCSP(False, valShowMessage)
   vResult = vResult And insCompania.fCreateDropCompaniaSP(False, valShowMessage)
   vResult = vResult And insUsuario.fCreateDropUsuarioSP(False, valShowMessage)
   vResult = vResult And insParametrosAdministrativos.fCreateDropParametrosAdministrativosSP(False, valShowMessage)
   vResult = vResult And insParametros.fCreateDropParametrosSP(False, valShowMessage)
   vResult = vResult And insParametros.fCreateDropParametrosSP(False, valShowMessage)
   vResult = vResult And insRendicion.fCreateDropRendicionSP(False, valShowMessage)
   vResult = vResult And insArticuloInventarioSp.fCreateDropGrupoDeArticuloInventarioSp(False, valShowMessage)
   vResult = vResult And insWcoSp.fDropProjectStoredProcedures(valShowMessage)
   vResult = vResult And insAnticipoSp.fCreateDropAnticipoSp(False, valShowMessage)
'   vResult = vResult And insAssemblys.fCreateDropSawS10AssemblysFunctions(False, valShowMessage, "")
   Set insWcoSp = Nothing
   Set insAssemblys = Nothing
   Set insAsiento = Nothing
   Set insCentroDeCostos = Nothing
   Set insCompania = Nothing
   Set insComprobante = Nothing
   Set insCuenta = Nothing
   Set insCxC = Nothing
   Set insCxP = Nothing
   Set insGenerarComprobante = Nothing
   Set insGrupoDeActivos = Nothing
   Set insParametrosAdministrativos = Nothing
   Set insParametros = Nothing
   Set insProveedor = Nothing
   Set insRendicion = Nothing
   Set insTablaRetencion = Nothing
   Set insTipoProveedor = Nothing
   Set insUsuario = Nothing
   Set insArticuloInventarioSp = Nothing
   Set insAnticipoSp = Nothing
   fDropProjectStoredProcedures = vResult
h_EXIT:  On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropProjectStoredProcedures", CM_MESSAGE_NAME, eg_Male, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'Public Function fCreateSp123(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
'   Dim vResult As Boolean
'   Dim insLibSp As clsUtilStoredProcedures
'   On Error GoTo h_ERROR
'   Set insLibSp = New clsUtilStoredProcedures
'   vResult = insLibSp.fCreateStoredProcedure(GetSpName123(), fSpParameters123(), fStatementsSp123(), valShowMessage, valWithEncryption)
'   Set insLibSp = Nothing
'   fCreateSp123 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateSp123", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'Public Function fDropSp123(ByVal valShowMessage As Boolean) As Boolean
'   Dim vResult As Boolean
'   Dim insLibSp As clsUtilStoredProcedures
'   On Error GoTo h_ERROR
'   Set insLibSp = New clsUtilStoredProcedures
'   vResult = insLibSp.fDropStoredProc(GetSpName123(), valShowMessage)
'   Set insLibSp = Nothing
'   fDropSp123 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropSp123", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'Private Function GetSpName123() As String
'   GetSpName123 = ""
'End Function
'Private Function fSpParameters123() As String
'   Dim vResult As String
'   On Error GoTo h_ERROR
'   vResult = ""
'   fSpParameters123 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSpParameters123", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'Private Function fStatementsSp123() As String
'   Dim vResult As String
'   On Error GoTo h_ERROR
'   vResult = "BEGIN " & vbCrLf
'   vResult = vResult & "END "
'   fStatementsSp123 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsSp123", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'Private Function fCreateFn456(ByVal valShowMessage As Boolean, ByVal valWithEncryption As Boolean) As Boolean
'   Dim vResult As Boolean
'   Dim insLibSf As clsUtilStoredFunctions
'   On Error GoTo h_ERROR
'   Set insLibSf = New clsUtilStoredFunctions
'   vResult = insLibSf.fCreateScalarFunction(GetFnName456(), fFnParameters456(), fFnScalarReturnDataType456(), fStatementsFn456(), valShowMessage, valWithEncryption)
'   Set insLibSf = Nothing
'   fCreateFn456 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fCreateFn456", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fDropFn456(ByVal valShowMessage As Boolean) As Boolean
'   Dim vResult As Boolean
'   Dim insLibSf As clsUtilStoredFunctions
'   On Error GoTo h_ERROR
'   Set insLibSf = New clsUtilStoredFunctions
'   vResult = insLibSf.fDropFunction(GetFnName456(), valShowMessage)
'   Set insLibSf = Nothing
'   fDropFn456 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fDropFn456", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function GetFnName456() As String
'   GetFnName456 = ""
'End Function
'
'Private Function fFnParameters456() As String
'   Dim vResult As String
'   On Error GoTo h_ERROR
'   vResult = ""
'   fFnParameters456 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnParameters456", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fFnScalarReturnDataType456() As String
'   Dim vResult As String
'   On Error GoTo h_ERROR
'   vResult = ""
'   fFnScalarReturnDataType456 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fFnScalarReturnDataType456", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'
'Private Function fStatementsFn456() As String
'   Dim vResult As String
'   On Error GoTo h_ERROR
'   vResult = "BEGIN " & vbCrLf
'   vResult = vResult & "END "
'   fStatementsFn456 = vResult
'h_EXIT:  On Error GoTo 0
'   Exit Function
'h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fStatementsFn456", CM_MESSAGE_NAME, eg_Female, Err.HelpContext, Err.HelpFile, Err.LastDllError)
'End Function
'---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- * ---------- *
'char(1)
'" & gUtilSQL.getCharTypeForDB(1) & "
'money
'" & gUtilSQL.getNumericTypeForDB(12, 2) & "
