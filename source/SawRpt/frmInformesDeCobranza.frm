VERSION 5.00
Object = "{86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0"; "MSCOMCT2.OCX"
Begin VB.Form frmInformesDeCobranza 
   BackColor       =   &H00F3F3F3&
   Caption         =   "Informes De Cobranza"
   ClientHeight    =   6750
   ClientLeft      =   315
   ClientTop       =   495
   ClientWidth     =   11775
   LinkTopic       =   "Form1"
   ScaleHeight     =   6750
   ScaleWidth      =   11775
   Begin VB.Frame frameAgrupacion 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Agrupación y Filtro del Informe"
      ForeColor       =   &H00808080&
      Height          =   1455
      Left            =   3480
      TabIndex        =   64
      Top             =   1800
      Width           =   4215
      Begin VB.ComboBox CmbCantidadImprimir 
         Height          =   315
         Index           =   1
         Left            =   1640
         TabIndex        =   28
         Top             =   600
         Visible         =   0   'False
         Width           =   2415
      End
      Begin VB.TextBox txtFiltro 
         Height          =   285
         Left            =   800
         TabIndex        =   29
         Top             =   1000
         Visible         =   0   'False
         Width           =   3255
      End
      Begin VB.CheckBox chkAgrupacionInforme 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Agrupar por..."
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   120
         TabIndex        =   27
         Top             =   320
         Width           =   2415
      End
      Begin VB.Label lblNombreDeCliente 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   66
         Top             =   1080
         Visible         =   0   'False
         Width           =   555
      End
      Begin VB.Label lblCantidadimprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   65
         Top             =   680
         Visible         =   0   'False
         Width           =   1335
      End
   End
   Begin VB.CheckBox chkComisionDetalladoResumido 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Por Documentos Cobrados"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   7260
      TabIndex        =   30
      Top             =   2460
      Width           =   2475
   End
   Begin VB.Frame frameTipoDetalleComisionCob 
      BackColor       =   &H00F3F3F3&
      BorderStyle     =   0  'None
      Caption         =   "Documentos Cobrados"
      ForeColor       =   &H00808080&
      Height          =   1035
      Left            =   7190
      TabIndex        =   75
      Top             =   2676
      Visible         =   0   'False
      Width           =   2790
      Begin VB.OptionButton optTipoDetalleComisionCob 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Sólo Documentos Cobrados"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   60
         TabIndex        =   78
         Top             =   675
         Width           =   2470
      End
      Begin VB.OptionButton optTipoDetalleComisionCob 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Docs. Cobrados y Cobranza"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   60
         TabIndex        =   77
         Top             =   60
         Width           =   2470
      End
      Begin VB.CheckBox chkDiasTranscurridosCXC 
         Alignment       =   1  'Right Justify
         Caption         =   "Mostrar Días Transcurridos"
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   60
         TabIndex        =   76
         Top             =   360
         Visible         =   0   'False
         Width           =   2470
      End
   End
   Begin VB.Frame frameZona 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Zona"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   3840
      TabIndex        =   69
      Top             =   4680
      Width           =   6675
      Begin VB.ComboBox CmbCantidadImprimirZona 
         Height          =   315
         Left            =   1635
         TabIndex        =   35
         Top             =   240
         Width           =   1575
      End
      Begin VB.TextBox TxtNombreZona 
         Height          =   285
         Left            =   1635
         TabIndex        =   36
         Top             =   585
         Width           =   3735
      End
      Begin VB.Label lblNombreDeZona 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre Zona"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   68
         Top             =   675
         Width           =   975
      End
      Begin VB.Label lblCantidadimprimirZona 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   67
         Top             =   300
         Width           =   1335
      End
   End
   Begin VB.Frame frameVentasDiferidas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Ventas Diferidas"
      ForeColor       =   &H00808080&
      Height          =   735
      Left            =   3480
      TabIndex        =   72
      Top             =   4920
      Visible         =   0   'False
      Width           =   4095
      Begin VB.CheckBox chkMostrasSoloVentasDiferidas 
         BackColor       =   &H00F3F3F3&
         Caption         =   "Mostrar Solo Cobranzas con Débito Fiscal Diferido"
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   120
         TabIndex        =   73
         Top             =   320
         Width           =   3855
      End
   End
   Begin VB.CheckBox chkOrdenarPorComprobante 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Ordenar Por Fecha del Comprobante de Retención"
      ForeColor       =   &H00A84439&
      Height          =   375
      Left            =   3480
      TabIndex        =   71
      Top             =   1920
      Visible         =   0   'False
      Width           =   3975
   End
   Begin VB.Frame frameTipoDeInforme 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Informe"
      ForeColor       =   &H00808080&
      Height          =   1395
      Left            =   5880
      TabIndex        =   63
      Top             =   600
      Width           =   1815
      Begin VB.OptionButton optInforme 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Por Cta.Bancaria."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   15
         Top             =   1000
         Width           =   1575
      End
      Begin VB.OptionButton optInforme 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Por Cliente .........."
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   14
         Top             =   640
         Width           =   1575
      End
      Begin VB.OptionButton optInforme 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Por Cobrador ......"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   13
         Top             =   280
         Value           =   -1  'True
         Width           =   1575
      End
   End
   Begin VB.CheckBox chkMontoBruto 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Monto Bruto"
      ForeColor       =   &H00A84439&
      Height          =   255
      Left            =   7260
      TabIndex        =   31
      Top             =   2640
      Width           =   2475
   End
   Begin VB.Frame frameTipoDeDesglose 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Tipo de Desglose"
      ForeColor       =   &H00808080&
      Height          =   1155
      Left            =   3480
      TabIndex        =   62
      Top             =   2100
      Width           =   1815
      Begin VB.OptionButton optTipoDesglose 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Por Cliente"
         ForeColor       =   &H00A84439&
         Height          =   255
         Index           =   1
         Left            =   120
         TabIndex        =   21
         Top             =   660
         Value           =   -1  'True
         Width           =   1455
      End
      Begin VB.OptionButton optTipoDesglose 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Por Día"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   20
         Top             =   360
         Width           =   1455
      End
   End
   Begin VB.ComboBox cmbOpcionesComisionesSobreCobranza 
      Height          =   315
      Left            =   8640
      TabIndex        =   26
      Top             =   2040
      Width           =   1695
   End
   Begin VB.ComboBox cmbMonedaDeLosReportes 
      Height          =   315
      Left            =   7890
      TabIndex        =   16
      Text            =   "En Moneda Original"
      Top             =   855
      Width           =   1935
   End
   Begin VB.CheckBox chkIncluirDocumentosCobrados 
      Alignment       =   1  'Right Justify
      BackColor       =   &H00F3F3F3&
      Caption         =   "Incluir Documentos Cobrados"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   3480
      TabIndex        =   19
      Top             =   1800
      Width           =   2655
   End
   Begin VB.Frame frameVendedor 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Vendedor"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   3480
      TabIndex        =   43
      Top             =   3720
      Width           =   6675
      Begin VB.TextBox txtNombreDeVendedor 
         Height          =   285
         Left            =   2790
         TabIndex        =   39
         Top             =   600
         Width           =   3735
      End
      Begin VB.ComboBox CmbCantidadAImprimir 
         Height          =   315
         Left            =   1635
         TabIndex        =   37
         Top             =   240
         Width           =   1575
      End
      Begin VB.TextBox txtCodigoDeVendedor 
         Height          =   285
         Left            =   1635
         TabIndex        =   38
         Top             =   630
         Width           =   1095
      End
      Begin VB.Label lblCantidadAimprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   50
         Top             =   300
         Width           =   1335
      End
      Begin VB.Label lblNombreDeVendedor 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre  Vendedor"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   49
         Top             =   675
         Width           =   1335
      End
   End
   Begin VB.Frame frameInformes 
      BackColor       =   &H00F3F3F3&
      ForeColor       =   &H80000010&
      Height          =   5655
      Left            =   120
      TabIndex        =   45
      Top             =   240
      Width           =   3135
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Listado de Retenciones ISLR..........."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   315
         Index           =   11
         Left            =   120
         TabIndex        =   74
         Top             =   2160
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Retenciones IVA ............................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   315
         Index           =   10
         Left            =   120
         TabIndex        =   70
         Top             =   4320
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Cobranzas por &zona ........................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   9
         Left            =   120
         TabIndex        =   8
         Top             =   3960
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Desglose de Cobranza ....................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   435
         Index           =   8
         Left            =   120
         TabIndex        =   6
         Top             =   3480
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Comisiones de Agentes ...................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   375
         Index           =   7
         Left            =   120
         TabIndex        =   7
         Top             =   4680
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Cobranzas con Retención de       Iva Pendiente por Distribuir .............."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   375
         Index           =   6
         Left            =   120
         TabIndex        =   5
         Top             =   3000
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Cobranzas por &Vendedor ................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   0
         Top             =   240
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Cobranzas por &día ..........................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   2
         Left            =   120
         TabIndex        =   1
         Top             =   720
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Listado de Retenciones IVA ............"
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   315
         Index           =   4
         Left            =   120
         TabIndex        =   3
         Top             =   1680
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Comparativo &por Año ......................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   5
         Left            =   120
         TabIndex        =   4
         Top             =   2640
         Width           =   2895
      End
      Begin VB.OptionButton optInformesDeCobranzas 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "&Comisión de Vendedores ................."
         CausesValidation=   0   'False
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   3
         Left            =   120
         TabIndex        =   2
         Top             =   1200
         Width           =   2895
      End
   End
   Begin VB.Frame frameTasaDeCambio 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Tasa de cambio"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   9960
      TabIndex        =   44
      Top             =   600
      Visible         =   0   'False
      Width           =   1575
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Del día"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   18
         Top             =   675
         Width           =   1335
      End
      Begin VB.OptionButton optTasaDeCambio 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Original"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   17
         Top             =   300
         Width           =   1335
      End
   End
   Begin VB.CommandButton cmdImpresora 
      Caption         =   "&Impresora"
      Height          =   375
      Left            =   120
      TabIndex        =   40
      Top             =   6000
      Width           =   1335
   End
   Begin VB.CommandButton cmdGrabar 
      Caption         =   "&Pantalla"
      Height          =   375
      Left            =   1800
      TabIndex        =   41
      Top             =   6000
      Width           =   1335
   End
   Begin VB.CommandButton cmdSalir 
      Cancel          =   -1  'True
      Caption         =   "&Salir"
      CausesValidation=   0   'False
      Height          =   375
      Left            =   3360
      TabIndex        =   42
      Top             =   6000
      Width           =   1335
   End
   Begin VB.Frame frameClaseDeInforme 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Informe"
      ForeColor       =   &H00808080&
      Height          =   1155
      Left            =   5355
      TabIndex        =   48
      Top             =   2100
      Width           =   1815
      Begin VB.OptionButton optDetalladoResumido 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Resumido"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   25
         Top             =   615
         Width           =   1335
      End
      Begin VB.OptionButton optDetalladoResumido 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Detallado"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   24
         Top             =   300
         Value           =   -1  'True
         Width           =   1335
      End
   End
   Begin VB.Frame frameTipoDeComision 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Tipo de Comisión"
      ForeColor       =   &H00808080&
      Height          =   1155
      Left            =   3480
      TabIndex        =   47
      Top             =   2100
      Width           =   1815
      Begin VB.OptionButton optTipoDeComision 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Por Cobranzas"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   1
         Left            =   120
         TabIndex        =   23
         Top             =   660
         Value           =   -1  'True
         Width           =   1455
      End
      Begin VB.OptionButton optTipoDeComision 
         Alignment       =   1  'Right Justify
         BackColor       =   &H00F3F3F3&
         Caption         =   "Por Ventas"
         ForeColor       =   &H00A84439&
         Height          =   255
         Index           =   0
         Left            =   120
         TabIndex        =   22
         Top             =   300
         Width           =   1455
      End
   End
   Begin VB.Frame frameFechas 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Fechas"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   3495
      TabIndex        =   51
      Top             =   600
      Width           =   2235
      Begin MSComCtl2.DTPicker dtpFechaFinal 
         Height          =   270
         Left            =   645
         TabIndex        =   12
         Top             =   637
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   97976323
         CurrentDate     =   36978
      End
      Begin MSComCtl2.DTPicker dtpFechaInicial 
         Height          =   270
         Left            =   660
         TabIndex        =   11
         Top             =   262
         Width           =   1335
         _ExtentX        =   2355
         _ExtentY        =   476
         _Version        =   393216
         CustomFormat    =   "dd/MM/yyyy"
         Format          =   97976323
         CurrentDate     =   36978
      End
      Begin VB.Label lblFechaFinal 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Final"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   53
         Top             =   675
         Width           =   330
      End
      Begin VB.Label lblFechaInicial 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Inicial"
         ForeColor       =   &H00A84439&
         Height          =   195
         Left            =   120
         TabIndex        =   52
         Top             =   300
         Width           =   405
      End
   End
   Begin VB.Frame FrameAnos 
      BackColor       =   &H00F3F3F3&
      Height          =   1095
      Left            =   3480
      TabIndex        =   54
      Top             =   600
      Width           =   2415
      Begin VB.TextBox txtAnoFinal 
         Height          =   285
         Left            =   1160
         TabIndex        =   10
         Top             =   640
         Width           =   1095
      End
      Begin VB.TextBox txtAnoInicial 
         Height          =   285
         Left            =   1160
         TabIndex        =   9
         Top             =   240
         Width           =   1095
      End
      Begin VB.Label lblAnoFinal 
         BackStyle       =   0  'Transparent
         Caption         =   "Año Final:"
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   180
         TabIndex        =   56
         Top             =   720
         Width           =   975
      End
      Begin VB.Label lblAnoInicial 
         BackStyle       =   0  'Transparent
         Caption         =   "Año Inicial:"
         ForeColor       =   &H00A84439&
         Height          =   255
         Left            =   180
         TabIndex        =   55
         Top             =   280
         Width           =   975
      End
   End
   Begin VB.Frame frameCliente 
      BackColor       =   &H00F3F3F3&
      Caption         =   "Cliente"
      ForeColor       =   &H00808080&
      Height          =   1095
      Left            =   3480
      TabIndex        =   59
      Top             =   3720
      Width           =   6675
      Begin VB.TextBox txtNombreDeCliente 
         Height          =   285
         Left            =   2760
         TabIndex        =   34
         Top             =   630
         Width           =   3735
      End
      Begin VB.ComboBox CmbCantidadImprimir 
         Height          =   315
         Index           =   0
         Left            =   1635
         TabIndex        =   32
         Top             =   240
         Width           =   1575
      End
      Begin VB.TextBox txtCodigoDeCliente 
         Height          =   285
         Left            =   1635
         TabIndex        =   33
         Top             =   630
         Width           =   1095
      End
      Begin VB.Label lblCantidadimprimir 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Cantidad a Imprimir"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   61
         Top             =   300
         Width           =   1335
      End
      Begin VB.Label lblNombreDeCliente 
         AutoSize        =   -1  'True
         BackColor       =   &H00F3F3F3&
         BackStyle       =   0  'Transparent
         Caption         =   "Nombre Cliente"
         ForeColor       =   &H00A84439&
         Height          =   195
         Index           =   0
         Left            =   120
         TabIndex        =   60
         Top             =   675
         Width           =   1080
      End
   End
   Begin VB.Label lblOpcionesComisionesSobreCobranza 
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Tipo de Comisión"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   7260
      TabIndex        =   58
      Top             =   2100
      Width           =   1215
   End
   Begin VB.Label lblMonedaDeLosReportes 
      AutoSize        =   -1  'True
      BackColor       =   &H80000005&
      BackStyle       =   0  'Transparent
      Caption         =   "Moneda en el Informe"
      ForeColor       =   &H00A84439&
      Height          =   195
      Left            =   7890
      TabIndex        =   57
      Top             =   600
      Width           =   1545
   End
   Begin VB.Label lblDatosDelInforme 
      BackColor       =   &H00F3F3F3&
      BackStyle       =   0  'Transparent
      Caption         =   "lblDatosDelInforme"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H000040C0&
      Height          =   255
      Left            =   3480
      TabIndex        =   46
      Top             =   180
      Width           =   8055
   End
End
Attribute VB_Name = "frmInformesDeCobranza"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit
Private Const CM_FILE_NAME As String = "frmInformesDeCobranza"
Private Const CM_MESSAGE_NAME As String = "Informes De Cobranza"
Private mInformeSeleccionado As Integer
Private mUsarCambioOriginal As Boolean
Private Const CM_OPT_DETALLADO   As Integer = 0
Private Const CM_OPT_RESUMIDO    As Integer = 1
Private Const CM_OPT_DIA As Integer = 1
Private Const CM_OPT_CLIENTE As Integer = 0
Private Const CM_OPT_COMISION_COBRANZA    As Integer = 1
Private Const CM_OPT_TASA_DEL_DIA  As Integer = 1
Private Const CM_OPT_COBRADOS_Y_COBRANZA As Integer = 0
Private gFechasDeLosInformes As clsFechasDeLosInformesNav
Private insVendedor As Object
Private insCliente As Object
Private insCuentaBancaria As Object
Private insCobranzaSQL As clsCobranzaSQL
Private insComprobante As Object
Private insCnxAos As Object
Private insCobranza As Object
Private insRenglonFactura As Object
Private insFactura As Object
Private insArticuloInventario As Object
Private insOtrosCargosDeFacturaNavigator As Object
Private insRenglonOtrosCargosFacturaNav As Object
Private insZona As Object
Private gProyCompaniaActual As Object
Private mReporteDeComisionesPorVenta As Boolean
Private mDondeImprimir As enum_DondeImprimir
Private mOpcionInformeCobranzaEntreFecha As Integer

Private Enum enum_OpcionInfCobranzaEntreFecha
   eOC_Cobrador = 0
   eOC_Cliente
   eOC_Banco
End Enum

Private Function CM_OPT_COBRANZAS_POR_VENDEDOR() As Integer
   CM_OPT_COBRANZAS_POR_VENDEDOR = 0
End Function

Private Function CM_OPT_COBRANZAS_POR_DIA() As Integer
   CM_OPT_COBRANZAS_POR_DIA = 2
End Function

Private Function CM_OPT_COMISION_DE_VENDEDOR() As Integer
   CM_OPT_COMISION_DE_VENDEDOR = 3
End Function

Private Function CM_OPT_RETENCION_IVA() As Integer
   CM_OPT_RETENCION_IVA = 4
End Function

Private Function CM_OPT_RETENCION_ISLR() As Integer
   CM_OPT_RETENCION_ISLR = 11
End Function

Private Function CM_OPT_COMPARATIVO_COBRANZA_POR_ANO() As Integer
   CM_OPT_COMPARATIVO_COBRANZA_POR_ANO = 5
End Function

Private Function CM_OPT_COBRANZAS_CON_RET_IVA_PENDIENTE_POR_DISTRIBUIR() As Integer
   CM_OPT_COBRANZAS_CON_RET_IVA_PENDIENTE_POR_DISTRIBUIR = 6
End Function

Private Function CM_OPT_COMISION_DE_AGENTES() As Integer
   CM_OPT_COMISION_DE_AGENTES = 7
End Function

Private Function CM_OPT_DESGLOSE_DE_COBRANZAS() As Integer
   CM_OPT_DESGLOSE_DE_COBRANZAS = 8
End Function

Private Function CM_OPT_COBRANZA_POR_ZONA() As Integer
   CM_OPT_COBRANZA_POR_ZONA = 9
End Function

Private Function CM_OPT_RETENCION_IVA_FORMAL() As Integer
   CM_OPT_RETENCION_IVA_FORMAL = 10
End Function

Private Function GetGender() As Enum_Gender
   GetGender = eg_Male
End Function

Private Sub sCheckForSpecialKeys(ByVal valKeyCode As Integer, ByVal valShift As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, valKeyCode) Then
      End If
   Const ASC_CR = 13
   If (valKeyCode = vbKeyF6) Or (valKeyCode = ASC_CR) Then
      cmdGrabar_Click
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sCheckForSpecialKeys", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub chkAgrupacionInforme_Click()
   On Error GoTo h_ERROR
   lblCantidadimprimir(1).Visible = chkAgrupacionInforme.Value
   If chkAgrupacionInforme.Value Then
      CmbCantidadImprimir(1).ListIndex = 1
      CmbCantidadImprimir(1).Visible = chkAgrupacionInforme.Value
   Else
      CmbCantidadImprimir(1).Visible = chkAgrupacionInforme.Value
      txtFiltro.Visible = chkAgrupacionInforme.Value
      txtFiltro.Text = ""
      lblNombreDeCliente(1).Visible = chkAgrupacionInforme.Value
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "chkAgrupacionInforme_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub chkComisionDetalladoResumido_Click()
    On Error GoTo h_ERROR
    If chkComisionDetalladoResumido.Value = vbChecked Then
      frameTipoDetalleComisionCob.Visible = True
      optTipoDetalleComisionCob(CM_OPT_COBRADOS_Y_COBRANZA).Value = True
      chkMontoBruto.Value = 0
      chkMontoBruto.Visible = False
      chkDiasTranscurridosCXC.Visible = True
    Else
      chkMontoBruto.Visible = True
      chkDiasTranscurridosCXC.Visible = False
      chkDiasTranscurridosCXC.Value = vbUnchecked
      frameTipoDetalleComisionCob.Visible = False
    End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "chkComisionDetalladoResumido_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub CmbCantidadImprimir_Click(Index As Integer)
   Dim Cualcombo As Integer
   On Error GoTo h_ERROR
   Cualcombo = Index
   Select Case Cualcombo
      Case 0:
         If CmbCantidadImprimir(Cualcombo).ListIndex = 1 Then
            lblNombreDeCliente(0).Visible = False
            txtNombreDeCliente.Visible = False
            txtCodigoDeCliente.Visible = False
            txtNombreDeCliente.Text = ""
            txtCodigoDeCliente.Text = ""
         Else
            lblNombreDeCliente(0).Visible = True
            txtNombreDeCliente.Visible = True
            If gProyParametros.GetUsaCodigoClienteEnPantalla Then
               txtCodigoDeCliente.Visible = True
               txtNombreDeCliente.Enabled = False
            Else
               txtCodigoDeCliente.Visible = False
               txtNombreDeCliente.Enabled = True
               txtNombreDeCliente.Left = txtCodigoDeVendedor.Left
            End If
         End If
      Case 1:
         If CmbCantidadImprimir(Cualcombo).ListIndex = 1 Then
            lblNombreDeCliente(1).Visible = False
            txtFiltro.Visible = False
         Else
            lblNombreDeCliente(1).Visible = True
            txtFiltro.Text = ""
            txtFiltro.Visible = True
         End If
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmbCantidadImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbOpcionesComisionesSobreCobranza_Click()
   On Error GoTo h_ERROR
   chkComisionDetalladoResumido.Value = vbUnchecked
   chkComisionDetalladoResumido.Visible = False
   chkMontoBruto.Value = vbUnchecked
   chkMontoBruto.Visible = False
   frameTipoDetalleComisionCob.Visible = False
   If optTipoDeComision(CM_OPT_COMISION_COBRANZA).Value Then
      If cmbOpcionesComisionesSobreCobranza.ListCount >= 0 Then
         If gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza) = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO) Or gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza) = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_DIASVENCIDOS) Then
            chkComisionDetalladoResumido.Visible = True
            frameTipoDeComision.Visible = True
            chkMontoBruto.Value = vbUnchecked
            chkMontoBruto.Visible = True
         End If
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbOpcionesComisionesSobreCobranza_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_PANTALLA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdImpresora_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_Click()
   On Error GoTo h_ERROR
   Unload Me
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdGrabar_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdGrabar_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdSalir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdSalir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmdImpresora_Click()
   On Error GoTo h_ERROR
   mDondeImprimir = eDI_IMPRESORA
   sEjecutaElInformeSeleccionado
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmdImpresora_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sInitLookAndFeel()
   Dim mAction As AccionSobreRecord
   On Error GoTo h_ERROR
   Me.Caption = CM_MESSAGE_NAME
   gAPI.ssSetFocus optInformesDeCobranzas(CM_OPT_COBRANZAS_POR_VENDEDOR)
   mInformeSeleccionado = CM_OPT_COBRANZAS_POR_VENDEDOR
   mAction = Abrir
   gEnumProyecto.FillComboBoxWithCalculoParaComisionesSobreCobranzaEnBaseA cmbOpcionesComisionesSobreCobranza, mAction
   mReporteDeComisionesPorVenta = True
   sInitDefaultValues
   sInitDefaultValuesCasoSistemaInternoParaNivelPorUsuario
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      frameVentasDiferidas.Visible = True
   End If
   sConfiguracionSegunPais
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitLookAndFeel", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Load()
   On Error GoTo h_ERROR
   Me.ZOrder 0
   If gDefgen.getMainForm.Width > Width Then
      Left = (gDefgen.getMainForm.Width - Width) / 4
      Top = (gDefgen.getMainForm.Height - Height) / 4
   Else
      Left = 0
      Top = 0
   End If
   Me.Width = 11955
   Me.Height = 7080
   mInformeSeleccionado = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Load", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub Form_Unload(Cancel As Integer)
   On Error GoTo h_ERROR
   Set insVendedor = Nothing
   Set insCliente = Nothing
   Set insCuentaBancaria = Nothing
   Set insCobranzaSQL = Nothing
   Set insComprobante = Nothing
   Set insCnxAos = Nothing
   Set insCobranza = Nothing
   Set insRenglonFactura = Nothing
   Set insFactura = Nothing
   Set insArticuloInventario = Nothing
   Set insOtrosCargosDeFacturaNavigator = Nothing
   Set insRenglonOtrosCargosFacturaNav = Nothing
   Set insZona = Nothing
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.Value, dtpFechaFinal.Value, gProyUsuarioActual.GetNombreDelUsuario
   Set gFechasDeLosInformes = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "Form_Unload", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optDetalladoResumido_Click(Index As Integer)
   On Error GoTo h_ERROR
   chkComisionDetalladoResumido.Visible = False
   If mInformeSeleccionado = CM_OPT_COMISION_DE_VENDEDOR Then
      frameVendedor.Visible = True
      lblMonedaDeLosReportes.Visible = True
      cmbMonedaDeLosReportes.Visible = True
      lblOpcionesComisionesSobreCobranza.Visible = optDetalladoResumido(CM_OPT_DETALLADO).Value
      cmbOpcionesComisionesSobreCobranza.Visible = optDetalladoResumido(CM_OPT_DETALLADO).Value
      If optTipoDeComision(CM_OPT_COMISION_COBRANZA).Value And optDetalladoResumido(CM_OPT_DETALLADO).Value Then
         chkComisionDetalladoResumido.Visible = (gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza) = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO) Or gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza) = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_DIASVENCIDOS))
         chkComisionDetalladoResumido.Value = vbUnchecked
         chkMontoBruto.Visible = (gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza) = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO) Or gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza) = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO))
         chkMontoBruto.Value = 0
      End If
      If optDetalladoResumido(CM_OPT_RESUMIDO) Then
         frameTipoDetalleComisionCob.Visible = Not optDetalladoResumido(CM_OPT_RESUMIDO).Value And Not optTipoDeComision(CM_OPT_COMISION_COBRANZA).Value
         gEnumProyecto.FillComboBoxWithCalculoParaComisionesSobreCobranzaEnBaseA cmbOpcionesComisionesSobreCobranza, Abrir, False
         frameTipoDetalleComisionCob.Visible = False
         cmbOpcionesComisionesSobreCobranza.Visible = True
         lblOpcionesComisionesSobreCobranza.Visible = True
      End If
      cmbOpcionesComisionesSobreCobranza.Width = 2640
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optDetalladoResumido_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInforme_Click(Index As Integer)
   Dim opcion As String
   On Error GoTo h_ERROR
   Select Case Index
      Case 0:
         mOpcionInformeCobranzaEntreFecha = eOC_Cobrador
         opcion = "Cobrador"
      Case 1:
         mOpcionInformeCobranzaEntreFecha = eOC_Cliente
         opcion = "Cliente"
      Case 2:
         mOpcionInformeCobranzaEntreFecha = eOC_Banco
         opcion = "Cuenta Bancaria"
   End Select
   chkAgrupacionInforme.Caption = "Agrupar por " & opcion
   txtFiltro.Text = ""
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInforme_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformesDeCobranzas_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformesDeCobranzas_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optInformesDeCobranzas_Click(Index As Integer)
   On Error GoTo h_ERROR
   mInformeSeleccionado = Index
   sOcultaCampos
   Select Case mInformeSeleccionado
      Case CM_OPT_COBRANZAS_POR_VENDEDOR: sActivarCamposDeCobranzasXVendedor
      Case CM_OPT_COBRANZAS_POR_DIA: sActivarCamposDeCobranzaXDia
      Case CM_OPT_COMISION_DE_VENDEDOR: sActivarCamposDeComisionDeVendedores
      Case CM_OPT_RETENCION_IVA: sActivarCamposDeRetencionIVA (Index)
      Case CM_OPT_COMPARATIVO_COBRANZA_POR_ANO: sActivarCamposComparativoCobranzaPorAno
      Case CM_OPT_COBRANZAS_CON_RET_IVA_PENDIENTE_POR_DISTRIBUIR: sActivarCamposDeCobConRetIvaPendiente
      Case CM_OPT_COMISION_DE_AGENTES: sActivarCamposDeComisionDeAgentes
      Case CM_OPT_DESGLOSE_DE_COBRANZAS: sActivarCamposDeDesgloseDeCobranza
      Case CM_OPT_COBRANZA_POR_ZONA: sActivarCamposDeCobranzaPorZona
      Case CM_OPT_RETENCION_IVA_FORMAL: sActivarCamposDeRetencionIVA (Index)
      Case CM_OPT_RETENCION_ISLR: sActivarCamposDeRetencionISLR (Index)
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optInformesDeCobranzas_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTipoDesglose_Click(Index As Integer)
   On Error GoTo h_ERROR
   If mInformeSeleccionado = CM_OPT_DESGLOSE_DE_COBRANZAS Then
      frameCliente.Visible = optTipoDesglose(CM_OPT_DIA).Value
      lblCantidadimprimir(0).Visible = optTipoDesglose(CM_OPT_DIA).Value
      CmbCantidadImprimir(0).Visible = optTipoDesglose(CM_OPT_DIA).Value
   End If
   txtNombreDeCliente.Text = ""
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTipoDesglose_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTipoDetalleComisionCob_Click(Index As Integer)
   On Error GoTo h_ERROR
   If Index = 0 Then
      chkDiasTranscurridosCXC.Visible = True
   Else
      chkDiasTranscurridosCXC.Value = False
      chkDiasTranscurridosCXC.Visible = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTipoDetalleComisionCob_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoFinal_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAnoFinal
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoFinal_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoFinal_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoFinal_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoFinal_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoFinal_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoInicial_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtAnoInicial
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoInicial_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoInicial_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoInicial_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtAnoInicial_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtAnoInicial_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeCliente_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeCliente_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeCliente_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   Cancel = False
   If LenB(txtCodigoDeCliente.Text) = 0 Then
      txtCodigoDeCliente.Text = "*"
   End If
   insCliente.sClrRecord
   insCliente.SetCodigo txtCodigoDeCliente.Text
   If insCliente.fSearchSelectConnection(False, False, False, 0, False, True) Then
      sSelectAndSetValuesOfCliente insCliente
   Else
      Cancel = True
      gAPI.ssSetFocus txtCodigoDeCliente
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeCliente_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeVendedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtCodigoDeVendedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeVendedor_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeVendedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeVendedor_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeVendedor_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeVendedor_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtCodigoDeVendedor_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtCodigoDeVendedor) = 0 Then
      txtCodigoDeVendedor = "*"
   End If
   insVendedor.sClrRecord
   insVendedor.SetCodigo txtCodigoDeVendedor
   If insCnxAos.fSelectAndSetValuesOfVendedorFromAOS(insVendedor, txtCodigoDeVendedor.Text, "") Then
      sAssignFieldsFromConnectionVendedor
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Set insCnxAos = Nothing
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Cancel = True
   gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtCodigoDeVendedor_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaInicial_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaInicial_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub dtpFechaFinal_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "dtpFechaFinal_KeyDown()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeSeleccionado()
   On Error GoTo h_ERROR
   Select Case mInformeSeleccionado
      Case CM_OPT_COBRANZAS_POR_VENDEDOR: sEjecutaCobranzasXVendedor
      Case CM_OPT_COBRANZAS_POR_DIA: sEjecutaCobranzaXDia
      Case CM_OPT_COMISION_DE_VENDEDOR: sEjecutaComisionDeVendedores
      Case CM_OPT_RETENCION_IVA: sEjecutaElInformeDeRetencionIVA
      Case CM_OPT_COMPARATIVO_COBRANZA_POR_ANO: sEjecutaComparativoCobranzaPorAnos
      Case CM_OPT_COBRANZAS_CON_RET_IVA_PENDIENTE_POR_DISTRIBUIR: sEjecutaCobConRetIvaPendiente
      Case CM_OPT_COMISION_DE_AGENTES: sEjecutaComisionDeAgentes
      Case CM_OPT_DESGLOSE_DE_COBRANZAS: sEjecutaElInformeDesgloseDeCobranza
      Case CM_OPT_COBRANZA_POR_ZONA: sEjecutaCobranzaPorZona
      Case CM_OPT_RETENCION_IVA_FORMAL: sEjecutaElInformeDeRetencionIVAFormal
      Case CM_OPT_RETENCION_ISLR: sEjecutaElInformeDeRetencionISLR
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeSeleccionado", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCobranzasXVendedor()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Cobranzas por Vendedor"
   chkIncluirDocumentosCobrados.Visible = True
   frameVendedor.Visible = True
   If CmbCantidadAImprimir.ListIndex = 1 Then
      lblNombreDeVendedor.Visible = False
      txtNombreDeVendedor.Visible = False
      txtCodigoDeVendedor.Visible = False
   Else
      lblNombreDeVendedor.Visible = True
      txtNombreDeVendedor.Visible = True
      If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
         txtCodigoDeVendedor.Visible = True
      Else
         txtCodigoDeVendedor.Visible = False
         txtNombreDeVendedor.Left = txtCodigoDeVendedor.Left
      End If
   End If
   frameFechas.Visible = True
   frameTasaDeCambio.Visible = True
   cmbMonedaDeLosReportes.Visible = True
   lblMonedaDeLosReportes.Visible = True
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      frameVentasDiferidas.Visible = True
      chkMostrasSoloVentasDiferidas.Value = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCobranzasXVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCobranzasXVendedor()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   Dim Titulo As String
   Dim incluirDocs As Boolean
   Dim EsMonedaLocal As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   If CmbCantidadAImprimir.Text = "Uno" And LenB(txtNombreDeVendedor.Text) = 0 Then
      sShowMessageForRequiredFields
   Else
      Set insConfigurar = New clsCobranzaRpt
      Set reporte = New DDActiveReports2.ActiveReport
      EsMonedaLocal = (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal))
      If gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value) Then
         If gConvert.ConvertByteToBoolean(chkIncluirDocumentosCobrados.Value) Then
            SqlDelReporte = insCobranzaSQL.fConstruirSQLDelReporteCobranzasXVendedorDocumentosCobradosConVentasDiferidas(optTasaDeCambio(0).Value, gProyParametros.GetNombreMonedaLocal, EsMonedaLocal, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetUsaModuloDeContabilidad, chkIncluirDocumentosCobrados.Value, gProyCompaniaActual.GetConsecutivoCompania, gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir), txtCodigoDeVendedor.Text, gProyParametros.GetUsaCodigoVendedorEnPantalla, insComprobante.GetTableName, gMonedaLocalActual, gUltimaTasaDeCambio, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value))
         Else
            SqlDelReporte = insCobranzaSQL.fConstruirSQLDelReporteCobranzasXVendedorConVentasDiferidas(optTasaDeCambio(0).Value, gProyParametros.GetNombreMonedaLocal, EsMonedaLocal, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetUsaModuloDeContabilidad, chkIncluirDocumentosCobrados.Value, gProyCompaniaActual.GetConsecutivoCompania, gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir), txtCodigoDeVendedor.Text, gProyParametros.GetUsaCodigoVendedorEnPantalla, insComprobante.GetTableName, gMonedaLocalActual, gUltimaTasaDeCambio, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value))
         End If
      Else
         SqlDelReporte = insCobranzaSQL.fConstruirSQLDelReporteCobranzasXVendedor(optTasaDeCambio(0).Value, gProyParametros.GetNombreMonedaLocal, EsMonedaLocal, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetUsaModuloDeContabilidad, chkIncluirDocumentosCobrados.Value, gProyCompaniaActual.GetConsecutivoCompania, gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir), txtCodigoDeVendedor.Text, gProyParametros.GetUsaCodigoVendedorEnPantalla, insComprobante.GetTableName, gMonedaLocalActual, gUltimaTasaDeCambio, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value))
      End If
      If chkIncluirDocumentosCobrados.Value = vbChecked Then
         Titulo = "Cobranzas por Vendedor(Detallado)"
         incluirDocs = True
      Else
         Titulo = "Cobranzas por Vendedor"
         incluirDocs = False
      End If
      If insConfigurar.fConfiguraElInformeDeCobranzasPorVendedor(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, mUsarCambioOriginal, incluirDocs, cmbMonedaDeLosReportes.Text, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gProyCompaniaActual.GetUsaModuloDeContabilidad, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, Titulo
      End If
      Set insConfigurar = Nothing
      Set reporte = Nothing
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCobranzasXVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValues()
   Dim i As Integer
   On Error GoTo h_ERROR
   Set gFechasDeLosInformes = New clsFechasDeLosInformesNav
   Set insCobranzaSQL = New clsCobranzaSQL
   gFechasDeLosInformes.sLeeLasFechasDeInformes dtpFechaInicial, dtpFechaFinal, gProyUsuarioActual.GetNombreDelUsuario
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadAImprimir
   CmbCantidadAImprimir.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS)
   CmbCantidadAImprimir.ListIndex = 1
   For i = 0 To 1
      gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadImprimir(i)
      CmbCantidadImprimir(i).Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS)
      CmbCantidadImprimir(i).ListIndex = 1
   Next
   gEnumReport.FillComboBoxWithCantidadAImprimir CmbCantidadImprimirZona
   CmbCantidadImprimirZona.Text = gEnumReport.enumCantidadAImprimirToString(eCI_TODOS)
   CmbCantidadImprimirZona.ListIndex = 1
   gEnumReport.FillComboBoxWithMonedaDeLosReportes cmbMonedaDeLosReportes, gProyParametros.GetNombreMonedaLocal
   cmbMonedaDeLosReportes.Width = 1935
   cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)
   gAPI.SelectTheElementInComboBox cmbOpcionesComisionesSobreCobranza, gProyParametrosCompania.GetFormaDeCalcularComisionesSobreCobranzaStr
   cmbOpcionesComisionesSobreCobranza.Width = 2640
   optDetalladoResumido(CM_OPT_DETALLADO).Value = True
   optTasaDeCambio(CM_OPT_TASA_DEL_DIA).Value = True
   optInformesDeCobranzas(CM_OPT_COMISION_DE_AGENTES).Visible = gProyParametros.GetEsSistemaParaIG
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaultValues", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_Click()
   On Error GoTo h_ERROR
   If CmbCantidadAImprimir.ListIndex = 1 Then
      lblNombreDeVendedor.Visible = False
      txtNombreDeVendedor.Visible = False
      txtCodigoDeVendedor.Visible = False
   Else
      lblNombreDeVendedor.Visible = True
      txtNombreDeVendedor.Visible = True
      If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
         txtCodigoDeVendedor.Visible = True
         txtNombreDeVendedor.Enabled = False
      Else
         txtCodigoDeVendedor.Visible = False
         txtNombreDeVendedor.Enabled = True
         txtNombreDeVendedor.Left = txtCodigoDeVendedor.Left
      End If
   End If
   sInitDefaultValuesCasoSistemaInternoParaNivelPorUsuario
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub cmbCantidadAImprimir_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub CmbCantidadAImprimir_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "cmbCantidadAImprimir_KeyPress()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtFiltro_GotFocus()
 On Error GoTo h_ERROR
   gAPI.SelectAllText txtFiltro
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtFiltro_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtFiltro_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtFiltro_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtFiltro_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtFiltro.Text) = 0 Then
      txtFiltro = "*"
   End If
   Select Case mOpcionInformeCobranzaEntreFecha
      Case eOC_Cobrador
         insVendedor.sClrRecord
         insVendedor.SetNombre txtFiltro.Text
         insVendedor.SetStatusVendedorStr gEnumProyecto.enumStatusVendedorToString(enum_StatusVendedor.eSV_ACTIVO)
         If insVendedor.fSearchSelectConnection() Then
            sSelectAndSetValuesOfFiltro
            Cancel = False
         Else
            Cancel = True
            GoTo h_EXIT
         End If
      Case eOC_Cliente
         insCliente.sClrRecord
         insCliente.SetNombre txtFiltro.Text
         If insCliente.fSearchSelectConnection(True, False, False, 0, False, True) Then
            sSelectAndSetValuesOfFiltro
            Cancel = False
         Else
            Cancel = True
            GoTo h_EXIT
         End If
      Case eOC_Banco
         If insCnxAos.fSelectAndSetValuesOfCuentaBancariaFromAOS(insCuentaBancaria, txtFiltro.Text, "NombreCuenta", enum_StatusCtaBancaria.eSC_ACTIVA, "Gv_CuentaBancaria_B1.Status", "", False, False, False, True) Then
            sSelectAndSetValuesOfFiltro
            txtFiltro.Text = insCuentaBancaria.GetNombreCuenta
            Cancel = False
         Else
            Cancel = True
            GoTo h_EXIT
         End If
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtFiltro_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_GotFocus()
 On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeCliente
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeCliente_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeCliente_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtNombreDeCliente) = 0 Then
      txtNombreDeCliente = "*"
   End If
   insCliente.sClrRecord
   insCliente.SetNombre txtNombreDeCliente.Text
   If insCliente.fSearchSelectConnection(True, False, False, 0, False, True) Then
      sSelectAndSetValuesOfCliente insCliente
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtNombreDeCliente_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfCliente(ByRef refCliente As Object)
   On Error GoTo h_ERROR
   Set refCliente = insCliente
   If refCliente.fRsRecordCount(False) = 1 Then
      sAssignFieldsFromConnectionCliente refCliente
   ElseIf refCliente.fRsRecordCount(False) > 1 Then
      If gProyParametros.GetUsaCodigoClienteEnPantalla Then
         refCliente.sShowListSelect "Codigo", txtCodigoDeCliente.Text
      Else
         refCliente.sShowListSelect "Nombre", txtNombreDeCliente.Text
      End If
      sAssignFieldsFromConnectionCliente refCliente
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionCliente(ByVal valCliente As Object)
   On Error GoTo h_ERROR
   txtNombreDeCliente.Text = valCliente.GetNombre
   txtCodigoDeCliente.Text = valCliente.GetCodigo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionCliente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
Private Sub txtNombreDeVendedor_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtNombreDeVendedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeVendedor_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
 
Private Sub txtNombreDeVendedor_KeyPress(KeyAscii As Integer)
   On Error GoTo h_ERROR
   If gAPI.sfEnterLikeTab(Me, KeyAscii) Then
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "txtNombreDeVendedor_KeyPress", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub txtNombreDeVendedor_Validate(Cancel As Boolean)
   On Error GoTo h_ERROR
   If LenB(txtNombreDeVendedor) = 0 Then
      txtNombreDeVendedor = "*"
   End If
   insVendedor.sClrRecord
   insVendedor.SetNombre txtNombreDeVendedor
   If insCnxAos.fSelectAndSetValuesOfVendedorFromAOS(insVendedor, txtNombreDeVendedor.Text, "Nombre") Then
     sAssignFieldsFromConnectionVendedor
   Else
      Cancel = True
      GoTo h_EXIT
   End If
   Set insCnxAos = Nothing
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, " txtNombreDeVendedor_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAssignFieldsFromConnectionVendedor()
   On Error GoTo h_ERROR
   txtNombreDeVendedor.Text = insVendedor.GetNombre
   txtCodigoDeVendedor.Text = insVendedor.GetCodigo
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAssignFieldsFromConnectionVendedor", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFields()
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields "Nombre Vendedor"
   gAPI.ssSetFocus txtNombreDeVendedor
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFields", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCobranzasEntreFechas()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Cobranzas entre fechas"
   frameFechas.Visible = True
   frameTipoDeInforme.Visible = True
   cmbMonedaDeLosReportes.Visible = True
   lblMonedaDeLosReportes.Visible = True
   frameTasaDeCambio.Visible = True
   frameAgrupacion.Visible = True
   optInforme_Click (0)
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      frameVentasDiferidas.Visible = True
      chkMostrasSoloVentasDiferidas.Value = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCobranzasEntreFechas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCobranzaXDia()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Cobranza X Día"
   frameFechas.Visible = True
   frameTasaDeCambio.Visible = True
   cmbMonedaDeLosReportes.Visible = True
   lblMonedaDeLosReportes.Visible = True
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      frameVentasDiferidas.Visible = True
      chkMostrasSoloVentasDiferidas.Value = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCobranzaXDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposComparativoCobranzaPorAno()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Comparativo Por Años"
   FrameAnos.Visible = True
   txtAnoInicial.Text = gUtilDate.fYear(gUtilDate.getFechaDeHoy)
   txtAnoFinal.Text = gUtilDate.fYear(gUtilDate.getFechaDeHoy)
   gAPI.ssSetFocus txtAnoInicial
   gAPI.SelectAllText txtAnoInicial
   frameVentasDiferidas.Visible = False
   chkMostrasSoloVentasDiferidas.Value = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposComparativoCobranzaPorAno", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeCobConRetIvaPendiente()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Cobranzas con Retención de " & gGlobalization.fPromptIVA & " Pendiente por Distribuir"
   frameVentasDiferidas.Visible = False
   chkMostrasSoloVentasDiferidas.Value = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCobConRetIvaPendiente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeDesgloseDeCobranza()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Desglose de Cobranza"
   frameFechas.Visible = True
   frameTipoDeDesglose.Visible = True
   If optTipoDesglose(CM_OPT_CLIENTE).Value Then
      CmbCantidadImprimir(0).Visible = False
      lblCantidadimprimir(0).Visible = False
      frameCliente.Visible = False
   Else
      CmbCantidadImprimir(0).Visible = True
      lblCantidadimprimir(0).Visible = True
      frameCliente.Visible = True
   End If
   If gProyParametrosCompania.GetUsarVentasConIvaDiferido Then
      frameVentasDiferidas.Visible = True
      chkMostrasSoloVentasDiferidas.Value = 0
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeDesgloseDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTasaDeCambio_Click(Index As Integer)
   On Error GoTo h_ERROR
   If Index = 0 Then
      mUsarCambioOriginal = True
   Else
      mUsarCambioOriginal = False
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTasaDeCambio_Click()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTasaDeCambio_KeyDown(Index As Integer, KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTasaDeCambio_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCobranzaXDia()
   Dim rptMostrarReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   Dim ReporteEnMonedaLocal As Boolean
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   gFechasDeLosInformes.sGrabasLasFechasDeInformes dtpFechaInicial.Value, dtpFechaFinal.Value, gProyUsuarioActual.GetNombreDelUsuario
   If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
     ReporteEnMonedaLocal = True
   Else
     ReporteEnMonedaLocal = False
   End If
   If gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value) Then
      SqlDelReporte = insCobranzaSQL.fConstruirSQLDelReporteCobranzaPorDiaConVentasDiferidas(ReporteEnMonedaLocal, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio, optTasaDeCambio(0).Value, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value))
   Else
      SqlDelReporte = insCobranzaSQL.fConstruirSqlDelReporteCobranzaPorDia(ReporteEnMonedaLocal, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio, optTasaDeCambio(0).Value, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value))
   End If
   Set rptMostrarReporte = New DDActiveReports2.ActiveReport
   Set insConfigurar = New clsCobranzaRpt
   If insConfigurar.fConfigurarDatosDelReporteCobranzaXDia(rptMostrarReporte, SqlDelReporte, dtpFechaInicial, dtpFechaFinal, mUsarCambioOriginal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False), gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value)) Then
      gUtilReports.sMostrarOImprimirReporte rptMostrarReporte, 1, mDondeImprimir, "Cobranza por Dia"
   End If
   Set rptMostrarReporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCobranzaXDia", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultaCampos()
   On Error GoTo h_ERROR
   frameVendedor.Visible = False
   frameZona.Visible = False
   frameTasaDeCambio.Visible = False
   frameTipoDeComision.Visible = False
   frameClaseDeInforme.Visible = False
   FrameAnos.Visible = False
   frameFechas.Visible = False
   frameTipoDeDesglose.Visible = False
   frameCliente.Visible = False
   chkIncluirDocumentosCobrados.Visible = False
   cmbMonedaDeLosReportes.Visible = False
   lblMonedaDeLosReportes.Visible = False
   lblOpcionesComisionesSobreCobranza.Visible = False
   cmbOpcionesComisionesSobreCobranza.Visible = False
   chkComisionDetalladoResumido.Visible = False
   frameTipoDetalleComisionCob.Visible = False
   chkMontoBruto.Value = 0
   chkMontoBruto.Visible = False
   frameTipoDeInforme.Visible = False
   frameAgrupacion.Visible = False
   chkOrdenarPorComprobante.Visible = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sOcultaCampos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeComisionDeVendedores()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Comision de Vendedores"
   frameClaseDeInforme.Visible = True
   If optDetalladoResumido(CM_OPT_RESUMIDO).Value Then
      CmbCantidadAImprimir.Visible = False
      lblCantidadAimprimir.Visible = False
      frameTipoDeComision.Visible = False
   Else 'If optDetalladoResumido(CM_OPT_DETALLADO).Value Then
      CmbCantidadAImprimir.Visible = True
      lblCantidadAimprimir.Visible = True
      frameTipoDeComision.Visible = True
      If cmbOpcionesComisionesSobreCobranza.Text = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO) Or cmbOpcionesComisionesSobreCobranza.Text = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_DIASVENCIDOS) Then
            chkComisionDetalladoResumido.Visible = True
            chkComisionDetalladoResumido.Value = 0
            chkMontoBruto.Visible = True
            chkMontoBruto.Value = 0
      Else
         chkComisionDetalladoResumido.Visible = False
         chkComisionDetalladoResumido.Value = 0
         chkMontoBruto.Visible = False
            chkMontoBruto.Value = 0
      End If
      If CmbCantidadAImprimir.ListIndex = 1 Then
          lblNombreDeVendedor.Visible = False
          txtNombreDeVendedor.Visible = False
          txtCodigoDeVendedor.Visible = False
      Else
          lblNombreDeVendedor.Visible = True
          txtNombreDeVendedor.Visible = True
          If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
             txtCodigoDeVendedor.Visible = True
          Else
             txtCodigoDeVendedor.Visible = False
             txtNombreDeVendedor.Left = txtCodigoDeVendedor.Left
          End If
      End If
   End If
   frameTipoDeComision.Visible = True
   frameFechas.Visible = True
   frameTasaDeCambio.Visible = True
   cmbMonedaDeLosReportes.Visible = True
   lblMonedaDeLosReportes.Visible = True
   optDetalladoResumido_Click CM_OPT_DETALLADO
   optTipoDeComision.Item(0).Value = True
   frameTipoDetalleComisionCob.Visible = False
   frameVentasDiferidas.Visible = False
   chkMostrasSoloVentasDiferidas.Value = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeComisionDeVendedores", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaComisionDeVendedores()
   Dim rptMostrarReporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   Dim totalizarPorVendedor As Boolean
   Dim MonedaDelReporteBs As Boolean
   Dim CobranzaporDiasVencidos As Boolean
   Dim cobradosYcobranza As Boolean
   Dim documentosCobrados As Boolean
   Dim tipoDeMensajeDeCambio As enum_MensajeMonedaParaInformes
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCobranzaRpt
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   CobranzaporDiasVencidos = False
   MonedaDelReporteBs = False
   documentosCobrados = False
   cobradosYcobranza = False
   If optDetalladoResumido(CM_OPT_DETALLADO).Value Then
      If CmbCantidadAImprimir.Text = "Uno" And LenB(txtNombreDeVendedor.Text) = 0 Then
         sShowMessageForRequiredFields
      Else
         If mReporteDeComisionesPorVenta Then
            SqlDelReporte = fEjecutaElReportedetalladoPorVentas(cmbOpcionesComisionesSobreCobranza.Text)
         Else
            SqlDelReporte = fEjecutaElReportedetalladoPorCobro(cmbOpcionesComisionesSobreCobranza.Text, CobranzaporDiasVencidos)
         End If
         Set rptMostrarReporte = New DDActiveReports2.ActiveReport
         
         totalizarPorVendedor = Not (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
         If (cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal)) Then
            tipoDeMensajeDeCambio = eMM_NotaFinal
         Else
            tipoDeMensajeDeCambio = eMM_CambioOriginal
         End If
         If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
             MonedaDelReporteBs = True
         End If
         If chkComisionDetalladoResumido.Value = vbChecked Then
            If optTipoDetalleComisionCob(CM_OPT_COBRADOS_Y_COBRANZA).Value Then
               cobradosYcobranza = True
            Else
               documentosCobrados = True
            End If
         End If
         Dim tipoReporte As enum_ReporteComisionesPorMontoYDiasVenc
         If chkComisionDetalladoResumido.Value = vbChecked Then
            If optTipoDetalleComisionCob(CM_OPT_COBRADOS_Y_COBRANZA).Value Then
               tipoReporte = eRC_DocumentoCobradosYCobranza
            Else
               tipoReporte = eRC_SoloDocumentosCobrados
            End If
         Else
            tipoReporte = eRC_Sencillo
         End If
        If insConfigurar.fConfigurarDatosDelReporteComisionXVendedorDetallado(rptMostrarReporte, SqlDelReporte, dtpFechaInicial, dtpFechaFinal, mReporteDeComisionesPorVenta, mUsarCambioOriginal, MonedaDelReporteBs, totalizarPorVendedor, CobranzaporDiasVencidos, tipoDeMensajeDeCambio, cobradosYcobranza, documentosCobrados, gEnumProyecto.strCalculoParaComisionesSobreCobranzaEnBaseAToNum(gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza)), tipoReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(True), gProyParametrosCompania.GetAsignarComisionDeVendedorEnCobranza, insVendedor, chkDiasTranscurridosCXC.Value) Then
            gUtilReports.sMostrarOImprimirReporte rptMostrarReporte, 1, mDondeImprimir, "Comisión de Vendedores"
         End If
      End If
   Else
      If mReporteDeComisionesPorVenta Then
         SqlDelReporte = fEjecutaElReportedetalladoPorVentasResumidas(cmbOpcionesComisionesSobreCobranza.Text)
      ElseIf cmbOpcionesComisionesSobreCobranza.Text = gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO) Then
         SqlDelReporte = fEjecutaElReportePorCobroPorArcituloResumido(cmbOpcionesComisionesSobreCobranza.Text, CobranzaporDiasVencidos)
      Else
         SqlDelReporte = fEjecutaElReportedetalladoPorCobro(cmbOpcionesComisionesSobreCobranza.Text, CobranzaporDiasVencidos)
      End If
      Set rptMostrarReporte = New DDActiveReports2.ActiveReport
         If insConfigurar.fConfigurarDatosDelReporteComisionXVendedorResumido(rptMostrarReporte, SqlDelReporte, dtpFechaInicial, dtpFechaFinal, mReporteDeComisionesPorVenta, mUsarCambioOriginal, gEnumProyecto.strCalculoParaComisionesSobreCobranzaEnBaseAToNum(gAPI.SelectedElementInComboBoxToString(cmbOpcionesComisionesSobreCobranza)), gProyCompaniaActual.GetNombreCompaniaParaInformes(True), gProyCompaniaActual.GetNombreCompaniaParaInformes(False), insVendedor) Then
            gUtilReports.sMostrarOImprimirReporte rptMostrarReporte, 1, mDondeImprimir, "Comisión x Vendedor Resumido"
         End If
   End If
   Set insConfigurar = Nothing
   Set rptMostrarReporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaComisionDeVendedores", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub optTipoDeComision_Click(Index As Integer)
    On Error GoTo h_ERROR
    cmbOpcionesComisionesSobreCobranza.Clear
    If Index = 0 Then
      mReporteDeComisionesPorVenta = True
      gEnumProyecto.FillComboBoxWithCalculoParaComisionesSobreCobranzaEnBaseA cmbOpcionesComisionesSobreCobranza, Abrir, False
      chkComisionDetalladoResumido.Visible = False
      frameTipoDetalleComisionCob.Visible = False
    Else
      gEnumProyecto.FillComboBoxWithCalculoParaComisionesSobreCobranzaEnBaseA cmbOpcionesComisionesSobreCobranza, Abrir
      mReporteDeComisionesPorVenta = False
      lblOpcionesComisionesSobreCobranza.Visible = True
      chkComisionDetalladoResumido.Visible = optDetalladoResumido(CM_OPT_DETALLADO).Value And optTipoDeComision(CM_OPT_COMISION_COBRANZA).Value
    End If
    cmbOpcionesComisionesSobreCobranza.Width = 2640
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "optTipoDeComision_Click()", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeRetencionIVA(Index As Integer)
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Listado de Retenciones " & gGlobalization.fPromptIVA & " Entre Fechas "
   frameFechas.Visible = True
   If (Index = CM_OPT_RETENCION_IVA_FORMAL) Then
      chkOrdenarPorComprobante.Visible = True
   End If
   frameVentasDiferidas.Visible = False
   chkMostrasSoloVentasDiferidas.Value = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeRetencionIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeDeRetencionIVA()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCobranzaRpt
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insCobranzaSQL.fSQLRetencionIVA(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, gMonedaLocalActual)
   If insConfigurar.fConfiguraElInformeDeRetencionIVA(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(True)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Retención " & gGlobalization.fPromptIVA
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeDeRetencionIVA", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaComparativoCobranzaPorAnos()
   Dim SqlDelReporte As String
   Dim insCobranzaConfigurar As clsCobranzaRpt
   Dim rptMostrarReporte As DDActiveReports2.ActiveReport
   On Error GoTo h_ERROR
   If (gConvert.FormatoNumerico(txtAnoFinal, False) - gConvert.FormatoNumerico(txtAnoInicial, False)) > 4 Then
      gMessage.Advertencia "Este informe solo muestra hasta un intervalo de cinco (5) años."
      GoTo h_EXIT
   End If
   Set insCobranzaConfigurar = New clsCobranzaRpt
   Set rptMostrarReporte = New DDActiveReports2.ActiveReport
   If gConvert.ConvierteAInteger(txtAnoFinal.Text) < gConvert.ConvierteAInteger(txtAnoInicial.Text) Then
      txtAnoFinal = gConvert.ConvierteAInteger(txtAnoFinal.Text) + 1
   End If
   SqlDelReporte = insCobranzaSQL.fSqlDeComparativoCobranzaPorAnos(gConvert.ConvierteAInteger(txtAnoInicial.Text), gConvert.ConvierteAInteger(txtAnoFinal.Text), gProyCompaniaActual.GetConsecutivoCompania, gMonedaLocalActual, gUltimaTasaDeCambio)
   If insCobranzaConfigurar.fConfigurarComparativoCobranzaPorAnos(rptMostrarReporte, SqlDelReporte, gConvert.ConvierteAInteger(txtAnoInicial.Text), gConvert.ConvierteAInteger(txtAnoFinal.Text), gProyCompaniaActual.GetNombreCompaniaParaInformes(True)) Then
      gUtilReports.sMostrarOImprimirReporte rptMostrarReporte, 1, mDondeImprimir, "Comparativo de Cobranza Entre Años"
   End If
   Set insCobranzaConfigurar = Nothing
   Set rptMostrarReporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaComparativoCobranzaPorAnos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCobConRetIvaPendiente()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCobranzaRpt
   Dim reporte As DDActiveReports2.ActiveReport
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCobranzaRpt
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insCobranzaSQL.fSQLCobranzasConRetencionIVAPendientePorDistribuir(gProyCompaniaActual.GetConsecutivoCompania)
   If insConfigurar.fConfiguraElInformeDeCobranzasConRetencionIVAPendientePorDistribuir(reporte, SqlDelReporte, gProyCompaniaActual.GetNombreCompaniaParaInformes(False)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Cobranzas con Retención " & gGlobalization.fPromptIVA & " Pendiente de Distribuir"
   End If
   Set insConfigurar = Nothing
   Set reporte = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCobConRetIvaPendiente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEjecutaElReportedetalladoPorCobro(ByVal OpcionesComisionesSobreCobranza As String, ByRef refCobranzaporDiasVencidos As Boolean) As String
   Dim sql As String
   Dim MonedaDelReporteBs As Boolean
   Dim ValimprimirSoloUno As Boolean
   Dim MostrarDiasTranscurridos As Boolean
   On Error GoTo h_ERROR
   refCobranzaporDiasVencidos = False
   Select Case OpcionesComisionesSobreCobranza
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO)
         sql = "(" & insCobranzaSQL.fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetallado(mUsarCambioOriginal, gUltimaTasaDeCambio, cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal), gMonedaLocalActual, gProyParametrosCompania.GetAsignarComisionDeVendedorEnCobranza, chkMontoBruto = vbChecked, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text) & ")"
         sql = sql & " UNION "
         sql = sql & "(" & insCobranzaSQL.fConstruirSQLPorCobrosDelReporteComisionDeVendedorDetalladoOrigenFactura(mUsarCambioOriginal, gUltimaTasaDeCambio, cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal), gMonedaLocalActual, gProyParametrosCompania.GetAsignarComisionDeVendedorEnCobranza, chkMontoBruto = vbChecked, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text, gProyParametrosCompania.GetUsarOtrosCargoDeFactura) & ")"
         sql = sql & " ORDER BY "
         If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
            sql = sql & "vendedor.Codigo, "
         Else
            sql = sql & insCobranza.GetTableName & ".Moneda, "
            sql = sql & "vendedor.Codigo, "
         End If
         sql = sql & insCobranza.GetTableName & ".Fecha, "
         sql = sql & insCobranza.GetTableName & ".Numero"
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_DIASVENCIDOS)
         If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
             MonedaDelReporteBs = True
         Else
             MonedaDelReporteBs = False
         End If
         If CmbCantidadAImprimir.Text <> gEnumReport.enumCantidadAImprimirToString(eCI_TODOS) Then
            ValimprimirSoloUno = True
         Else
            ValimprimirSoloUno = False
         End If
         sql = insCobranzaSQL.fConstruirSQLPorCobrosDelReporteComisionDeVendedorPorDiasVencidoDetallado(MonedaDelReporteBs, mUsarCambioOriginal, gMonedaLocalActual, gUltimaTasaDeCambio, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, ValimprimirSoloUno, txtNombreDeVendedor.Text, chkDiasTranscurridosCXC.Value)
         refCobranzaporDiasVencidos = True
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO)
         sql = insCobranzaSQL.fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloDetallado(mUsarCambioOriginal, gUltimaTasaDeCambio, cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal), gMonedaLocalActual, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text)
   End Select
   fEjecutaElReportedetalladoPorCobro = sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaElReportedetalladoPorCobro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaElReportedetalladoPorVentas(ByVal OpcionesComisionesSobreCobranza As String) As String
   Dim sql As String
   Dim sqlMontoRenglonSinIvaConDescFact As String
   Dim ReporteEnMonedaLocal As Boolean
   On Error GoTo h_ERROR
   If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
      ReporteEnMonedaLocal = True
   Else
      ReporteEnMonedaLocal = False
   End If
   Select Case OpcionesComisionesSobreCobranza
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO)
         sqlMontoRenglonSinIvaConDescFact = insRenglonFactura.fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(False, False)
         sql = insCobranzaSQL.fConstruirSQLPorVentasDelReporteComisionDeVendedorDetallado(sqlMontoRenglonSinIvaConDescFact, ReporteEnMonedaLocal, mUsarCambioOriginal, gUltimaTasaDeCambio, gMonedaLocalActual, dtpFechaInicial.Value, dtpFechaFinal.Value, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text, gProyCompaniaActual.GetConsecutivoCompania)
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO)
         sql = "(" & insCobranzaSQL.fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticulo(mUsarCambioOriginal, ReporteEnMonedaLocal, gUltimaTasaDeCambio, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, gMonedaLocalActual, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text) & ")"
         sql = sql & " UNION "
         sql = sql & "(" & insCobranzaSQL.fConstruirSQLPorVentasDelReporteComisionDeVendedorDetalladoPoArticuloDeOtrosCargos(mUsarCambioOriginal, ReporteEnMonedaLocal, gMonedaLocalActual, gUltimaTasaDeCambio, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value) & ")"
         sql = sql & " ORDER BY CodigoVendedor,ColumnaParaGrupoTipoReporte,CodigoArticulo, FechaDeFactura, NumeroDeFactura"
   End Select
   fEjecutaElReportedetalladoPorVentas = sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaElReportedetalladoPorVentas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fEjecutaElReportedetalladoPorVentasResumidas(ByVal OpcionesComisionesSobreCobranza As String) As String
   Dim sql As String
   Dim sqlSumaTotalRenglon As String
   On Error GoTo h_ERROR
   sqlSumaTotalRenglon = ""
   sqlSumaTotalRenglon = insRenglonFactura.fSQLMontoTotalDelRenglonSinIvaConElDescuentoDeFactura(False, False)
   sqlSumaTotalRenglon = gUtilSQL.DfCDecSQL(sqlSumaTotalRenglon)
   sqlSumaTotalRenglon = " SUM(" & sqlSumaTotalRenglon & ")"
   sqlSumaTotalRenglon = gUtilSQL.getIIF(gUtilSQL.DfSQLIsNull(sqlSumaTotalRenglon), 0, sqlSumaTotalRenglon, True)
   Select Case OpcionesComisionesSobreCobranza
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_MONTO)
         sql = insCobranzaSQL.fConstruirSQLPorVentasTotalesDeCadaVendedor(insFactura.getFN_TOTAL_MONTO_EXENTO, insFactura.getFN_TOTAL_FACTURA, mUsarCambioOriginal, gUltimaTasaDeCambio, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetConsecutivoCompania, sqlSumaTotalRenglon, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text)
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO)
         sql = "(" & fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo(mUsarCambioOriginal, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text) & ")"
         sql = sql & " UNION "
         sql = sql & "(" & fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos(mUsarCambioOriginal) & ")"
         sql = sql & " ORDER BY CodigoVendedor,ColumnaParaGrupoTipoReporte,CodigoArticulo"
   End Select
   fEjecutaElReportedetalladoPorVentasResumidas = sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaElReportedetalladoPorVentasResumidas", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo(ByVal usarCambioOriginal As Boolean, ByVal valCantidadAImprimir As String, ByVal valNombreDeVendedor As String) As String
   Dim sql As String
   Dim sqlPrecioSinIva As String
   Dim sqlPrecioConIva As String
   Dim sqlMoneda As String
   Dim SqlTasaDeCambio As String
   On Error GoTo h_ERROR
   insFactura.setClaseDeTrabajo eCTFC_Factura
   sqlPrecioConIva = insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_CON_IVA
   sqlPrecioSinIva = insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_PRECIO_SIN_IVA
   sqlMoneda = insFactura.GetTableName & "." & insFactura.getFN_MONEDA
   SqlTasaDeCambio = insFactura.GetTableName & "." & insFactura.getFN_CAMBIO_ABOLIVARES
   If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
      sqlPrecioConIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, usarCambioOriginal, "")
      sqlPrecioSinIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, usarCambioOriginal, "")
   End If
   sql = "SELECT "
   sql = sql & gUtilSQL.fSimpleSqlValue("Articulo") & " AS ColumnaParaGrupoTipoReporte, "
   sql = sql & insVendedor.GetTableName & ".Codigo AS CodigoVendedor,"
   sql = sql & insVendedor.GetTableName & ".Nombre AS NombreVendedor,"
   sql = sql & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO & " AS CodigoArticulo,"
   sql = sql & insFactura.GetTableName & "." & insFactura.getFN_MONEDA & " AS MonedaDeFactura, "
   sql = sql & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_DESCRIPCION & " AS DescripcionArticulo, "
   sql = sql & insArticuloInventario.GetTableName & ".PorcentajeComision AS PorcentajeComision,"
   sql = sql & "SUM(" & sqlPrecioConIva & " * renglonFactura.Cantidad  " & ") AS PrecioConIVA, "
   sql = sql & "SUM((" & sqlPrecioSinIva & "* renglonFactura.Cantidad " & "-( " & sqlPrecioSinIva & "* renglonFactura.Cantidad  * (renglonFactura.PorcentajeDescuento)/100 ) )" & "- ((" & sqlPrecioSinIva & "* renglonFactura.Cantidad " & "-( " & sqlPrecioSinIva & "* renglonFactura.Cantidad  * (renglonFactura.PorcentajeDescuento)/100 ) )*(Factura.PorcentajeDescuento)/100 ))" & " AS PrecioSinIVA, "
   sql = sql & "SUM(renglonFactura.Cantidad) AS Cantidad ,"
   sql = sql & "SUM(((" & sqlPrecioSinIva & "* renglonFactura.Cantidad " & "-( " & sqlPrecioSinIva & "* renglonFactura.Cantidad  * (renglonFactura.PorcentajeDescuento)/100 ) )" & "- ((" & sqlPrecioSinIva & "* renglonFactura.Cantidad " & "-( " & sqlPrecioSinIva & "* renglonFactura.Cantidad  * (renglonFactura.PorcentajeDescuento)/100 ) )*(Factura.PorcentajeDescuento)/100 ))" & "* (PorcentajeComision/100  " & ")) AS Comision "
   sql = sql & " FROM ((" & insVendedor.GetTableName
   sql = sql & " INNER JOIN " & insFactura.GetTableName
   sql = sql & " ON (" & insVendedor.GetTableName & ".Codigo"
   sql = sql & " = " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_VENDEDOR & ")"
   sql = sql & " AND (" & insVendedor.GetTableName & ".ConsecutivoCompania"
   sql = sql & " = " & insFactura.GetTableName & "." & "ConsecutivoCompania" & "))"
   sql = sql & " INNER JOIN (" & insArticuloInventario.GetTableName
   sql = sql & " INNER JOIN " & insRenglonFactura.GetTableName
   sql = sql & " ON (" & insArticuloInventario.GetTableName & ".Codigo"
   sql = sql & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO & ")"
   sql = sql & " AND (" & insArticuloInventario.GetTableName & ".ConsecutivoCompania"
   sql = sql & " = " & insRenglonFactura.GetTableName & "." & "ConsecutivoCompania" & "))"
   sql = sql & " ON (" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
   sql = sql & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_TIPO_DE_DOCUMENTO & ")"
   sql = sql & " AND (" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
   sql = sql & " = " & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_NUMERO_FACTURA & ")"
   sql = sql & " AND (" & insFactura.GetTableName & "." & "ConsecutivoCompania"
   sql = sql & " = " & insRenglonFactura.GetTableName & "." & "ConsecutivoCompania" & "))"
   sql = sql & " INNER JOIN " & insCliente.GetTableName
   sql = sql & " ON (" & insCliente.GetTableName & ".Codigo"
   sql = sql & " = " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE & ")"
   sql = sql & " AND (" & insCliente.GetTableName & ".ConsecutivoCompania"
   sql = sql & " = " & insFactura.GetTableName & "." & "ConsecutivoCompania" & ")"
   sql = sql & " WHERE " & insFactura.GetTableName & "." & "ConsecutivoCompania" & " = " & gProyCompaniaActual.GetConsecutivoCompania
   sql = sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, dtpFechaInicial.Value, dtpFechaFinal.Value)
   sql = sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   If valCantidadAImprimir <> "Todos" Then
      sql = sql & " AND " & gUtilSQL.fSQLValue("vendedor.Nombre", valNombreDeVendedor, False)
   End If
   sql = sql & " AND (" & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   sql = sql & " GROUP BY "
   sql = sql & insVendedor.GetTableName & ".Codigo,"
   sql = sql & insVendedor.GetTableName & ".Nombre,"
   sql = sql & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_ARTICULO & ","
   sql = sql & insFactura.GetTableName & "." & insFactura.getFN_MONEDA & ", "
   sql = sql & insRenglonFactura.GetTableName & "." & insRenglonFactura.getFN_DESCRIPCION & ", "
   sql = sql & insArticuloInventario.GetTableName & ".PorcentajeComision"
   fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo = sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticulo", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Function fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos(ByVal usarCambioOriginal As Boolean) As String
   Dim sql As String
   Dim sqlPrecioSinIva As String
   Dim sqlPrecioConIva As String
   Dim sqlMoneda As String
   Dim SqlTasaDeCambio As String
   On Error GoTo h_ERROR
   insFactura.setClaseDeTrabajo eCTFC_Factura
   sqlPrecioConIva = insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_TOTAL_RENGLON
   sqlPrecioSinIva = insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_TOTAL_RENGLON
   sqlMoneda = insFactura.GetTableName & "." & insFactura.getFN_MONEDA
   SqlTasaDeCambio = insFactura.GetTableName & "." & insFactura.getFN_CAMBIO_ABOLIVARES
   If cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal) Then
      sqlPrecioConIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioConIva, usarCambioOriginal, "")
      sqlPrecioSinIva = gUltimaTasaDeCambio.fSQLCampoMontoPorTasaDeCambio(SqlTasaDeCambio, sqlMoneda, sqlPrecioSinIva, usarCambioOriginal, "")
   End If
   sql = "SELECT "
   sql = sql & gUtilSQL.fSimpleSqlValue("Otros Cargos Factura") & " AS ColumnaParaGrupoTipoReporte, "
   sql = sql & insVendedor.GetTableName & ".Codigo AS CodigoVendedor,"
   sql = sql & insVendedor.GetTableName & ".Nombre AS NombreVendedor,"
   sql = sql & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_CODIGO_DE_CARGO & " AS CodigoArticulo,"
   sql = sql & insFactura.GetTableName & "." & insFactura.getFN_MONEDA & " AS MonedaDeFactura, "
   sql = sql & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_DESCRIPCION & " AS DescripcionArticulo, "
   sql = sql & insOtrosCargosDeFacturaNavigator.GetTableName & ".PorcentajeComision AS PorcentajeComision,"
   sql = sql & "SUM(" & sqlPrecioConIva & ") AS PrecioConIVA, "
   sql = sql & "SUM(" & sqlPrecioSinIva & ") AS PrecioSinIVA, "
   sql = sql & " 0 , 0"
   sql = sql & " FROM " & insVendedor.GetTableName
   sql = sql & " INNER JOIN (" & insCliente.GetTableName
   sql = sql & " INNER JOIN (" & insFactura.GetTableName
   sql = sql & " INNER JOIN (" & insOtrosCargosDeFacturaNavigator.GetTableName
   sql = sql & " INNER JOIN " & insRenglonOtrosCargosFacturaNav.GetTableName
   sql = sql & " ON (" & insOtrosCargosDeFacturaNavigator.GetTableName & ".Codigo"
   sql = sql & " = " & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_CODIGO_DE_CARGO
   sql = sql & ") AND (" & insOtrosCargosDeFacturaNavigator.GetTableName & ".ConsecutivoCompania"
   sql = sql & " = " & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_CONSECUTIVO_COMPANIA
   sql = sql & ")) ON (" & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO
   sql = sql & " = " & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_TIPO_DE_DOCUMENTO
   sql = sql & ") AND (" & insFactura.GetTableName & "." & insFactura.getFN_NUMERO
   sql = sql & " = " & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_NUMERO_FACTURA
   sql = sql & ") AND (" & insFactura.GetTableName & "." & "ConsecutivoCompania"
   sql = sql & " = " & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_CONSECUTIVO_COMPANIA
   sql = sql & ")) ON (" & insCliente.GetTableName & ".Codigo"
   sql = sql & " = " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_CLIENTE
   sql = sql & ") AND (" & insCliente.GetTableName & ".ConsecutivoCompania"
   sql = sql & " = " & insFactura.GetTableName & "." & "ConsecutivoCompania"
   sql = sql & ")) ON (" & insVendedor.GetTableName & ".Codigo"
   sql = sql & " = " & insFactura.GetTableName & "." & insFactura.getFN_CODIGO_VENDEDOR
   sql = sql & ") AND (" & insVendedor.GetTableName & ".ConsecutivoCompania"
   sql = sql & " = " & insFactura.GetTableName & "." & "ConsecutivoCompania"
   sql = sql & ") AND (" & insVendedor.GetTableName & ".ConsecutivoCompania"
   sql = sql & " = " & insCliente.GetTableName & ".ConsecutivoCompania)"
   sql = sql & " WHERE " & insFactura.GetTableName & "." & "ConsecutivoCompania" & " = " & gProyCompaniaActual.GetConsecutivoCompania
   sql = sql & " AND " & gUtilSQL.DfSQLDateValueBetween(insFactura.GetTableName & "." & insFactura.getFN_FECHA, dtpFechaInicial.Value, dtpFechaFinal.Value)
   sql = sql & " AND " & insFactura.GetTableName & "." & insFactura.getFN_TIPO_DE_DOCUMENTO & " <> " & gUtilSQL.fSQLSimpleValueForEnum(enum_TipoDocumentoFactura.eTF_RESUMENDIARIODEVENTAS)
   sql = sql & " AND (" & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_ANULADA) & " OR " & insFactura.GetTableName & "." & insFactura.getFN_STATUS_FACTURA & " = " & gUtilSQL.fSQLSimpleValueForEnum(enum_StatusFactura.eSF_EMITIDA) & ")"
   sql = sql & " GROUP BY "
   sql = sql & insVendedor.GetTableName & ".Codigo,"
   sql = sql & insVendedor.GetTableName & ".Nombre,"
   sql = sql & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_CODIGO_DE_CARGO & ","
   sql = sql & insFactura.GetTableName & "." & insFactura.getFN_MONEDA & ", "
   sql = sql & insRenglonOtrosCargosFacturaNav.GetTableName & "." & insRenglonOtrosCargosFacturaNav.getFN_DESCRIPCION & ","
   sql = sql & insOtrosCargosDeFacturaNavigator.GetTableName & ".PorcentajeComision"
   fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos = sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fConstruirSQLPorVentasDelReporteComisionDeVendedorResumidoPoArticuloDeOtrosCargos", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaElInformeDesgloseDeCobranza()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   Dim ImprimirClienteUnico As Boolean
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCobranzaRpt
   Set reporte = New DDActiveReports2.ActiveReport
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadImprimir(0)) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      ImprimirClienteUnico = True
   Else
      ImprimirClienteUnico = False
   End If
   If gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value) Then
      SqlDelReporte = insCobranzaSQL.fSQLDesgloseCobranzaConVentasDiferidas(optTipoDesglose(1).Value, gProyCompaniaActual.GetConsecutivoCompania, _
                                          dtpFechaInicial.Value, dtpFechaFinal.Value, ImprimirClienteUnico, _
                                           txtNombreDeCliente.Text, gMonedaLocalActual, gUltimaTasaDeCambio, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value))
   Else
      SqlDelReporte = insCobranzaSQL.fSQLDesgloseCobranza(optTipoDesglose(1).Value, gProyCompaniaActual.GetConsecutivoCompania, _
                                          dtpFechaInicial.Value, dtpFechaFinal.Value, ImprimirClienteUnico, _
                                           txtNombreDeCliente.Text, gMonedaLocalActual, gUltimaTasaDeCambio, gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value))
   End If
   If insConfigurar.fConfiguraElInformeDesgloseDeCobranza(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, optTipoDesglose(1).Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(True), gConvert.ConvertByteToBoolean(chkMostrasSoloVentasDiferidas.Value)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Desglose de Cobranza por Día"
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeDesgloseDeCobranza", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sSelectAndSetValuesOfFiltro()
   On Error GoTo h_ERROR
   Select Case mOpcionInformeCobranzaEntreFecha
      Case eOC_Cobrador
         If insVendedor.fRsRecordCount(False) = 1 Then
            txtFiltro.Text = insVendedor.GetNombre
         ElseIf insVendedor.fRsRecordCount(False) > 1 Then
            insVendedor.sShowListSelect "Nombre", txtFiltro.Text, "vendedor.StatusVendedor = " & gUtilSQL.fSimpleSqlValue(enum_StatusVendedor.eSV_ACTIVO)
            txtFiltro.Text = insVendedor.GetNombre
         End If
      Case eOC_Cliente
         If insCliente.fRsRecordCount(False) = 1 Then
            txtFiltro.Text = insCliente.GetNombre
         ElseIf insCliente.fRsRecordCount(False) > 1 Then
            insCliente.sShowListSelect "Nombre", txtFiltro.Text
            txtFiltro.Text = insCliente.GetNombre
         End If
   End Select
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sSelectAndSetValuesOfFiltro", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sActivarCamposDeComisionDeAgentes()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Comisión de Agentes"
   frameVendedor.Visible = True
   frameFechas.Visible = True
   frameVentasDiferidas.Visible = False
   chkMostrasSoloVentasDiferidas.Value = vbUnchecked
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeComisionDeAgentes", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaComisionDeAgentes()
   Dim SqlDelReporte As String
   Dim insConfigurar As clsCobranzaRpt
   Dim rpt As DDActiveReports2.ActiveReport
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCobranzaRpt
   Set rpt = New DDActiveReports2.ActiveReport
   SqlDelReporte = fSQLComisionAgentesXLineaDeProducto
   If insConfigurar.fConfiguraElInformeDeComisionDeAgentesXLineaDeProducto(rpt, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(False)) Then
      gUtilReports.sMostrarOImprimirReporte rpt, 1, mDondeImprimir, "Comisión de Agentes"
   End If
   Set insConfigurar = Nothing
   Set rpt = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCobConRetIvaPendiente", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSQLComisionAgentesXLineaDeProducto() As String
   Dim sql As String
   On Error GoTo h_ERROR
   sql = "SELECT dbo.comisionesAgentesVistaBase.codigovendedor AS CodigoVendedor, MAX(dbo.comisionesAgentesVistaBase.nombreVendedor) AS NombreVendedor, " & _
               "dbo.comisionesAgentesVistaBase.NumeroCobranza AS NumeroCobranza, dbo.comisionesAgentesVistaBase.FechaCobranza, MIN(dbo.factura.Numero) AS NumeroFactura, " & _
               "MIN(" & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda(), "dbo.factura.TotalBaseImponible", "factura.Fecha") & ") AS MontoFactura, dbo.articuloInventario.LineaDeProducto AS NombreLineaProducto, " & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "SUM(dbo.renglonFactura.TotalRenglon) " & _
               "/ (1 + dbo.factura.PorcentajeDescuento / 100) / (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2009-04-01 00:00:00', 102) THEN 1.09 ELSE 1.12 END) END)END)END) " & _
               "", "factura.Fecha") & "AS MontoXLinea, SUM(dbo.renglonFactura.TotalRenglon) / (1 + dbo.factura.PorcentajeDescuento / 100)" & _
               "/ (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2009-04-01 00:00:00', 102) THEN 1.09 ELSE 1.12 END) END)END)END) / MIN( " & gUtilSQL.getIIF("dbo.factura.TotalBaseImponible = 0 ", "dbo.factura.TotalFactura", "dbo.factura.TotalBaseImponible", False) & ") " & _
               "* 100 AS PorcentajeLinea, " & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "dbo.comisionesAgentesVistaBase.MontoAbonado", "dbo.comisionesAgentesVistaBase.FechaCobranza") & " / (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2009-04-01 00:00:00', 102) THEN 1.09 ELSE 1.12 END)END)END)END)" & _
               "AS MontoAbonado, " & gMonedaLocalActual.fSQLConvierteMontoSiAplica(gMonedaLocalActual.GetHoyCodigoMoneda, "SUM(dbo.renglonFactura.TotalRenglon) / (1 + dbo.factura.PorcentajeDescuento / 100)/ (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2009-04-01 00:00:00', 102) THEN 1.09 ELSE 1.12 END) END)END)END) / MIN(" & gUtilSQL.getIIF("dbo.factura.TotalBaseImponible = 0", "dbo.factura.TotalFactura", "dbo.factura.TotalBaseImponible") & ")" & _
               "* MIN(dbo.comisionesAgentesVistaBase.MontoAbonado) / (CASE WHEN Factura.fecha < CONVERT(DATETIME, '2005-10-01 00:00:00', 102)THEN 1.15 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-03-01 00:00:00', 102) THEN 1.14 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2007-07-01 00:00:00', 102) THEN 1.11 ELSE (CASE WHEN Factura.fecha <CONVERT(DATETIME, '2009-04-01 00:00:00', 102) THEN 1.09 ELSE 1.12 END) END)END)END)" & _
               "", "dbo.comisionesAgentesVistaBase.FechaCobranza") & " AS MontoComisionable, cliente.Nombre as NombreCliente"
   sql = sql & " FROM renglonFactura INNER JOIN " & _
               "factura ON renglonFactura.ConsecutivoCompania = factura.ConsecutivoCompania AND renglonFactura.NumeroFactura = factura.Numero AND " & _
               "renglonFactura.TipoDeDocumento = factura.TipoDeDocumento INNER JOIN " & _
               "articuloInventario ON renglonFactura.ConsecutivoCompania = articuloInventario.ConsecutivoCompania AND " & _
               "renglonFactura.Articulo = articuloInventario.Codigo INNER JOIN " & _
               "comisionesAgentesVistaBase ON factura.Numero = comisionesAgentesVistaBase.NumeroDocumentoOrigen INNER JOIN " & _
               " cliente ON factura.ConsecutivoCompania = cliente.ConsecutivoCompania AND factura.CodigoCliente = cliente.Codigo "
   sql = sql & "GROUP BY dbo.articuloInventario.LineaDeProducto, dbo.comisionesAgentesVistaBase.NumeroCobranza, dbo.comisionesAgentesVistaBase.MontoAbonado, " & _
               "dbo.comisionesAgentesVistaBase.FechaCobranza , fecha, dbo.factura.porcentajeDescuento, dbo.comisionesAgentesVistaBase.CodigoVendedor, cliente.nombre, dbo.comisionesAgentesVistaBase.FechaCxC " & _
               "HAVING      FechaCobranza BETWEEN " & gUtilSQL.fDateToSQLValue(dtpFechaInicial.Value) & " AND " & gUtilSQL.fDateToSQLValue(dtpFechaFinal.Value) & " "
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadAImprimir) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) Then
      sql = gUtilSQL.fSQLValueWithAnd(sql, "dbo.comisionesAgentesVistaBase.codigovendedor", txtCodigoDeVendedor.Text, False)
   End If
   sql = sql & "ORDER BY dbo.comisionesAgentesVistaBase.codigovendedor, dbo.comisionesAgentesVistaBase.FechaCobranza, numeroCobranza, numerofactura, lineadeproducto"
h_EXIT: On Error GoTo 0
   fSQLComisionAgentesXLineaDeProducto = sql
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSQLComisionAgentesXLineaDeProducto", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function


Private Sub sActivarCamposDeCobranzaPorZona()
   On Error GoTo h_ERROR
   lblDatosDelInforme.Caption = "Datos del Informe - Cobranza Por Zona"
   frameZona.Visible = True
   frameFechas.Visible = True
   frameVendedor.Visible = True
   cmbMonedaDeLosReportes.Visible = True
   lblMonedaDeLosReportes.Visible = True
   frameTasaDeCambio.Visible = True
   If CmbCantidadAImprimir.ListIndex = 1 Then
      lblNombreDeVendedor.Visible = False
      txtNombreDeVendedor.Visible = False
      txtCodigoDeVendedor.Visible = False
   Else
      lblNombreDeVendedor.Visible = True
      txtNombreDeVendedor.Visible = True
      If gProyParametros.GetUsaCodigoVendedorEnPantalla Then
         txtCodigoDeVendedor.Visible = True
      Else
         txtCodigoDeVendedor.Visible = False
         txtNombreDeVendedor.Left = txtCodigoDeVendedor.Left
      End If
   End If
   frameVentasDiferidas.Visible = False
   chkMostrasSoloVentasDiferidas.Value = 0
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeCobranzaPorZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub TxtNombreZona_GotFocus()
   On Error GoTo h_ERROR
   gAPI.SelectAllText txtFiltro
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "TxtNombreZona_GotFocus", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub TxtNombreZona_KeyDown(KeyCode As Integer, Shift As Integer)
   On Error GoTo h_ERROR
   sCheckForSpecialKeys KeyCode, Shift
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "TxtNombreZona_KeyDown", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub TxtNombreZona_Validate(Cancel As Boolean)
   Dim vDescripcion As String
   On Error GoTo h_ERROR
   If gTexto.DfLen(TxtNombreZona.Text) = 0 Then
      TxtNombreZona.Text = "*"
   End If
   If fSelectAndSetValuesOfZonaCobranzaAOS(vDescripcion, TxtNombreZona.Text) Then
      TxtNombreZona.Text = vDescripcion
   Else
      Cancel = True
      gAPI.ssSetFocus TxtNombreZona
      GoTo h_EXIT
   End If
   Cancel = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "TxtNombreZona_Validate", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaCobranzaPorZona()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   Dim Titulo As String
   Dim ReporteEnMonedaLocal As Boolean
   Dim valoptTasaDeCambio As Boolean
   
   On Error GoTo h_ERROR
   If dtpFechaFinal.Value < dtpFechaInicial.Value Then
      dtpFechaFinal.Value = dtpFechaInicial.Value
   End If
   
   ReporteEnMonedaLocal = (gAPI.SelectedElementInComboBoxToString(cmbMonedaDeLosReportes) <> gEnumReport.enumMonedaDeLosReportesToString(enum_MonedaDeLosReportes.eMR_EnMonedaOriginal, gProyParametros.GetNombreMonedaLocal))
   valoptTasaDeCambio = optTasaDeCambio(0).Value
   If gAPI.SelectedElementInComboBoxToString(CmbCantidadImprimirZona) = gEnumReport.enumCantidadAImprimirToString(eCI_uno) And LenB(TxtNombreZona.Text) = 0 Then
      sShowMessageForRequiredFields
   Else
      Set insConfigurar = New clsCobranzaRpt
      Set reporte = New DDActiveReports2.ActiveReport
      SqlDelReporte = insCobranzaSQL.fSqlCobranzaPorZona(dtpFechaInicial.Value, dtpFechaFinal.Value, _
                        gAPI.SelectedElementInComboBoxToString(CmbCantidadImprimirZona), _
                        gProyCompaniaActual.GetConsecutivoCompania, TxtNombreZona.Text, valoptTasaDeCambio, _
                        ReporteEnMonedaLocal, gUltimaTasaDeCambio, txtCodigoDeVendedor.Text)
      Titulo = "Cobranza por Zona"
      If insConfigurar.fConfiguraElInformeDeCobranzasPorZona(reporte, SqlDelReporte, dtpFechaInicial, dtpFechaFinal, gProyCompaniaActual.GetNombreCompaniaParaInformes(False)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, Titulo
      End If
      Set insConfigurar = Nothing
      Set reporte = Nothing
  End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaCobranzaPorZona", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub CmbCantidadImprimirZona_Click()
   On Error GoTo h_ERROR
   If CmbCantidadImprimirZona.ListIndex = 1 Then
      lblNombreDeZona.Visible = False
      TxtNombreZona.Visible = False
   Else
      lblNombreDeZona.Visible = True
      TxtNombreZona.Text = ""
      TxtNombreZona.Visible = True
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: gError.sErrorMessage Err.Number, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "CmbCantidadImprimirZona_Click", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sInitDefaultValuesCasoSistemaInternoParaNivelPorUsuario()
   On Error GoTo h_ERROR
   If (gProyParametros.GetEsSistemaParaIG) Then
      If (gProyUsuarioActual.GetInformesPorVendedorComisiones() And Not gProyUsuarioActual.GetEsSupervisor) Then
         sAsignaCodigoVendedorFromConexionUsuario
      End If
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sInitDefaultValuesCasoSistemaInternoParaNivelPorUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sAsignaCodigoVendedorFromConexionUsuario()
   On Error GoTo h_ERROR
   sOcultarOtrasOpciones
   CmbCantidadAImprimir.ListIndex = 0
   CmbCantidadAImprimir.Text = "Uno"
   If (insVendedor.fSearchByField("email", gProyUsuarioActual.GetEmail(), False, True, True)) Then
       txtCodigoDeVendedor.Text = insVendedor.GetCodigo
       txtNombreDeVendedor.Text = insVendedor.GetNombre
   End If
   txtNombreDeVendedor.Enabled = False
   CmbCantidadAImprimir.Enabled = False
   optInformesDeCobranzas(3).Visible = True
   optInformesDeCobranzas(3).Value = True
   frameInformes.Enabled = False
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignaCodigoVendedorFromConexionUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sOcultarOtrasOpciones()
   Dim vIndx As Long
   On Error GoTo h_ERROR
   For vIndx = 0 To optInformesDeCobranzas.Count - 1 Step 1
      optInformesDeCobranzas(vIndx).Visible = False
   Next vIndx
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sAsignaCodigoVendedorFromConexionUsuario", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fSelectAndSetValuesOfZonaCobranzaAOS(ByRef refDescripcion, ByVal valDescripcionBusqueda As String) As Boolean
   Dim xmlResultado As String
   Dim vResult As Boolean
   On Error GoTo h_ERROR
   vResult = True
   If insCnxAos.fSelectAndSetValuesOfZonaCobranzaFromAOS(valDescripcionBusqueda, xmlResultado) Then
      gLibGalacDataParse.Initialize xmlResultado
      refDescripcion = gLibGalacDataParse.GetString(0, "Nombre", "")
   Else
      vResult = False
   End If
   fSelectAndSetValuesOfZonaCobranzaAOS = vResult
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: fSelectAndSetValuesOfZonaCobranzaAOS = False
   Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fSelectAndSetValuesOfZonaCobranzaAOS", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sEjecutaElInformeDeRetencionIVAFormal()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   Set insConfigurar = New clsCobranzaRpt
   Set reporte = New DDActiveReports2.ActiveReport
   SqlDelReporte = insCobranzaSQL.fSQLRetencionIVAFormal(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, gMonedaLocalActual, gConvert.ConvertByteToBoolean(chkOrdenarPorComprobante.Value))
   If insConfigurar.fConfigurarDatosDelReporteCobranzaRentencionIvaFormal(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(True), gConvert.ConvertByteToBoolean(chkOrdenarPorComprobante.Value)) Then
      gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Retención " & gGlobalization.fPromptIVA
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeDeRetencionIVAFormal", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Function fEjecutaElReportePorCobroPorArcituloResumido(ByVal OpcionesComisionesSobreCobranza As String, ByRef refCobranzaporDiasVencidos As Boolean) As String
   Dim sql As String
   Dim MonedaDelReporteBs As Boolean
   Dim ValimprimirSoloUno As Boolean
   On Error GoTo h_ERROR
   refCobranzaporDiasVencidos = False
   Select Case OpcionesComisionesSobreCobranza
      Case gEnumProyecto.enumCalculoParaComisionesSobreCobranzaEnBaseAToString(eCP_PORCENTAJE_POR_ARTICULO)
         sql = insCobranzaSQL.fConstruirSQLDelReporteComisionDeVendedorPorCobranzaPorArticuloResumido(mUsarCambioOriginal, gUltimaTasaDeCambio, cmbMonedaDeLosReportes.Text = gEnumReport.enumMonedaDeLosReportesToString(eMR_EnBs, gProyParametros.GetNombreMonedaLocal), gMonedaLocalActual, gProyParametrosCompania.GetAsignarComisionDeVendedorEnCobranza, chkMontoBruto = vbChecked, gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, CmbCantidadAImprimir.Text, txtNombreDeVendedor.Text)
   End Select
   fEjecutaElReportePorCobroPorArcituloResumido = sql
h_EXIT: On Error GoTo 0
   Exit Function
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "fEjecutaElReportePorCobroPorArcituloResumido", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Function

Private Sub sActivarCamposDeRetencionISLR(Index As Integer)
   On Error GoTo h_ERROR
   If gGlobalization.fEsCodigoPeru Then
      lblDatosDelInforme.Caption = "Datos del Informe - Listado de Detracciones Por Cliente "
   Else
      lblDatosDelInforme.Caption = "Datos del Informe - Listado de Retenciones " & gGlobalization.fPromptISLR & " Por Cliente "
   End If
   frameFechas.Visible = True
   If (Index = CM_OPT_RETENCION_ISLR) Then
      frameCliente.Visible = True
      txtNombreDeCliente.Text = ""
      txtCodigoDeCliente.Text = ""
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sActivarCamposDeRetencionISLR", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sEjecutaElInformeDeRetencionISLR()
   Dim reporte As DDActiveReports2.ActiveReport
   Dim insConfigurar As clsCobranzaRpt
   Dim SqlDelReporte As String
   On Error GoTo h_ERROR
   Set insCobranzaSQL = New clsCobranzaSQL
   Set insConfigurar = New clsCobranzaRpt
   Set reporte = New DDActiveReports2.ActiveReport
   If CmbCantidadImprimir(0).ListIndex = 1 Then
      SqlDelReporte = insCobranzaSQL.fSQLRetencionISLR(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, gMonedaLocalActual, txtNombreDeCliente.Text)
      If insConfigurar.fConfiguraElInformeDeRetencionISLR(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(True)) Then
         gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Retención " & gGlobalization.fPromptISLR
      End If
   Else
      If CmbCantidadImprimir(0).ListIndex = 0 And txtNombreDeCliente.Text <> "" Then
         SqlDelReporte = insCobranzaSQL.fSQLRetencionISLR(gProyCompaniaActual.GetConsecutivoCompania, dtpFechaInicial.Value, dtpFechaFinal.Value, gMonedaLocalActual, txtNombreDeCliente.Text)
         If insConfigurar.fConfiguraElInformeDeRetencionISLR(reporte, SqlDelReporte, dtpFechaInicial.Value, dtpFechaFinal.Value, gProyCompaniaActual.GetNombreCompaniaParaInformes(True)) Then
            gUtilReports.sMostrarOImprimirReporte reporte, 1, mDondeImprimir, "Retención " & gGlobalization.fPromptISLR
         End If
      Else
         sShowMessageForRequiredFieldsCobranzaInformeISLR
      End If
   End If
   Set reporte = Nothing
   Set insConfigurar = Nothing
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sEjecutaElInformeDeRetencionISLR", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sShowMessageForRequiredFieldsCobranzaInformeISLR()
   On Error GoTo h_ERROR
   gMessage.ShowRequiredFields "Nombre Cliente"
   gAPI.ssSetFocus txtNombreDeCliente
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sShowMessageForRequiredFieldsCobranzaInformeISLR", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Private Sub sConfiguracionSegunPais()
   On Error GoTo h_ERROR
   optInformesDeCobranzas(4).Caption = "&Listado de Retenciones " & gGlobalization.fPromptIVA & " ............"
   optInformesDeCobranzas(6).Caption = "Cobranzas con Retención de       " & gGlobalization.fPromptIVA & " Pendiente por Distribuir ............."
   optInformesDeCobranzas(10).Caption = "&Retenciones " & gGlobalization.fPromptIVA & " ............................."
   If gGlobalization.fEsCodigoPeru Then
      optInformesDeCobranzas(11).Caption = "&Listado de Detracciones .................."
   End If
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sConfiguracionSegunPais", CM_MESSAGE_NAME, GetGender(), Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub

Public Sub sLoadObjectValues(ByVal valVendedorNavigator As Object, ByVal valClienteNavigator As Object, ByVal valCuentaBancariaNavigator As Object, _
                                    ByVal valComprobanteNavigator As Object, ByVal valCnxAos As Object, ByVal valCobranzaNavigator As Object, _
                                    ByVal valRenglonFacturaNavigator As Object, ByVal valFacturaNavigator As Object, ByVal valArticuloInventarioNavigator As Object, _
                                    ByVal valOtrosCargosDeFacturaNavigator As Object, ByVal valRenglonOtrosCargosFacturaNav As Object, _
                                    ByVal valZonaCobranza As Object, ByVal valProyCompaniaActual As Object)
   On Error GoTo h_ERROR
   Set insVendedor = valVendedorNavigator
   Set insCliente = valClienteNavigator
   Set insCuentaBancaria = valCuentaBancariaNavigator
   Set insComprobante = valComprobanteNavigator
   Set insCnxAos = valCnxAos
   Set insCobranza = valCobranzaNavigator
   Set insRenglonFactura = valRenglonFacturaNavigator
   Set insFactura = valFacturaNavigator
   Set insArticuloInventario = valArticuloInventarioNavigator
   Set insOtrosCargosDeFacturaNavigator = valOtrosCargosDeFacturaNavigator
   Set insRenglonOtrosCargosFacturaNav = valRenglonOtrosCargosFacturaNav
   Set insZona = valZonaCobranza
   Set gProyCompaniaActual = valProyCompaniaActual
   sInitDefaultValues
h_EXIT: On Error GoTo 0
   Exit Sub
h_ERROR: Err.Raise Err.Number, Err.Source, gError.fAddMethodToStackTrace(Err.Description, CM_FILE_NAME, "sLoadObjectValues", CM_MESSAGE_NAME, GetGender, Err.HelpContext, Err.HelpFile, Err.LastDllError)
End Sub
