using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Vendedor;
using Galac.Adm.Ccl.Vendedor;
using Galac.Saw.Ccl.SttDef;
using System.Xml.Linq;
using LibGalac.Aos.Uil;

namespace Galac.Adm.Uil.Vendedor.ViewModel {
    public class VendedorViewModel: LibInputMasterViewModelMfc<Ccl.Vendedor.Vendedor> {     
        #region Constantes
        public const string ConsecutivoCompaniaPropertyName = "ConsecutivoCompania";
        public const string ConsecutivoPropertyName = "Consecutivo";
        public const string CodigoPropertyName = "Codigo";
        public const string NombrePropertyName = "Nombre";
        public const string RIFPropertyName = "RIF";
        public const string StatusVendedorPropertyName = "StatusVendedor";
        public const string DireccionPropertyName = "Direccion";
        public const string CiudadPropertyName = "Ciudad";
        public const string ZonaPostalPropertyName = "ZonaPostal";
        public const string TelefonoPropertyName = "Telefono";
        public const string FaxPropertyName = "Fax";
        public const string EmailPropertyName = "Email";
        public const string NotasPropertyName = "Notas";
        public const string RutaDeComercializacionPropertyName = "RutaDeComercializacion";
        public const string ComisionPorVentaPropertyName = "ComisionPorVenta";
        public const string ComisionPorCobroPropertyName = "ComisionPorCobro";
        public const string TopeInicialVenta1PropertyName = "TopeInicialVenta1";
        public const string TopeFinalVenta1PropertyName = "TopeFinalVenta1";
        public const string PorcentajeVentas1PropertyName = "PorcentajeVentas1";
        public const string TopeInicialVenta2PropertyName = "TopeInicialVenta2";
        public const string TopeFinalVenta2PropertyName = "TopeFinalVenta2";
        public const string PorcentajeVentas2PropertyName = "PorcentajeVentas2";
        public const string TopeInicialVenta3PropertyName = "TopeInicialVenta3";
        public const string TopeFinalVenta3PropertyName = "TopeFinalVenta3";
        public const string PorcentajeVentas3PropertyName = "PorcentajeVentas3";
        public const string TopeInicialVenta4PropertyName = "TopeInicialVenta4";
        public const string TopeFinalVenta4PropertyName = "TopeFinalVenta4";
        public const string PorcentajeVentas4PropertyName = "PorcentajeVentas4";
        public const string TopeFinalVenta5PropertyName = "TopeFinalVenta5";
        public const string PorcentajeVentas5PropertyName = "PorcentajeVentas5";
        public const string TopeInicialCobranza1PropertyName = "TopeInicialCobranza1";
        public const string TopeFinalCobranza1PropertyName = "TopeFinalCobranza1";
        public const string PorcentajeCobranza1PropertyName = "PorcentajeCobranza1";
        public const string TopeInicialCobranza2PropertyName = "TopeInicialCobranza2";
        public const string TopeFinalCobranza2PropertyName = "TopeFinalCobranza2";
        public const string PorcentajeCobranza2PropertyName = "PorcentajeCobranza2";
        public const string TopeInicialCobranza3PropertyName = "TopeInicialCobranza3";
        public const string TopeFinalCobranza3PropertyName = "TopeFinalCobranza3";
        public const string PorcentajeCobranza3PropertyName = "PorcentajeCobranza3";
        public const string TopeInicialCobranza4PropertyName = "TopeInicialCobranza4";
        public const string TopeFinalCobranza4PropertyName = "TopeFinalCobranza4";
        public const string PorcentajeCobranza4PropertyName = "PorcentajeCobranza4";
        public const string TopeFinalCobranza5PropertyName = "TopeFinalCobranza5";
        public const string PorcentajeCobranza5PropertyName = "PorcentajeCobranza5";
        public const string UsaComisionPorVentaPropertyName = "UsaComisionPorVenta";
        public const string UsaComisionPorCobranzaPropertyName = "UsaComisionPorCobranza";
        public const string CodigoLotePropertyName = "CodigoLote";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        public const string IsEnabledDetalleComisionesPorVentasPropertyName = "IsEnabledDetalleComisionesPorVentas";
        public const string IsEnabledDetalleComisionesPorCobranzasPropertyName = "IsEnabledDetalleComisionesPorCobranzas";
        public const string MensajeTopeFinalDeVentaMenorAlTopeInicial = "El tope final debe ser mayor al tope inicial.";
        public const string MensajePorcentajeDeComisionConValorNoAceptado = "Recuerde: los porcentajes de comisiones deben tener sentido lógico.";
        public const string MensajePorcentajeDeComisionMayorA100 = "El porcentaje de comisión debe ser menor al 100%";
        private FkCiudadViewModel _ConexionCiudad = null;
        private FkRutaDeComercializacionViewModel _ConexionRutaDeComercializacion = null;

        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Vendedor"; }
        }

        public int ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                    RaisePropertyChanged(ConsecutivoCompaniaPropertyName);
                }
            }
        }

        public int Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                    RaisePropertyChanged(ConsecutivoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "Código del Vendedor es requerido.")]
        [LibGridColum("Código", ColumnOrder = 0, Width = 70)]
        public string Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = (LibString.IsNullOrEmpty(value) ? String.Empty : LibText.FillWithCharToLeft(value, "0", 5));
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "Nombre del Vendedor es requerido.")]
        [LibGridColum("Nombre", ColumnOrder = 1, Width = 300)]
        public string Nombre {
            get {
                return Model.Nombre;
            }
            set {
                if (Model.Nombre != value) {
                    Model.Nombre = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombrePropertyName);
                }
            }
        }

        [LibGridColum("N° Identificación", ColumnOrder = 2)]
        public string RIF {
            get {
                return Model.RIF;
            }
            set {
                if (Model.RIF != value) {
                    Model.RIF = value;
                    IsDirty = true;
                    RaisePropertyChanged(RIFPropertyName);
                }
            }
        }

        public string PromptNumeroRif {
            get {
                return "N° R.I.F.";
            }
        }

        [LibGridColum("Estado", eGridColumType.Enum, PrintingMemberPath = "StatusVendedorStr", ColumnOrder = 4, Width = 70)]
        public eStatusVendedor StatusVendedor {
            get {
                return Model.StatusVendedorAsEnum;
            }
            set {
                if (Model.StatusVendedorAsEnum != value) {
                    Model.StatusVendedorAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusVendedorPropertyName);
                }
            }
        }

        public string Direccion {
            get {
                return Model.Direccion;
            }
            set {
                if (Model.Direccion != value) {
                    Model.Direccion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DireccionPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Ciudad es requerido.")]
        [LibGridColum("Ciudad", ColumnOrder = 3)]
        public string Ciudad {
            get {
                return Model.Ciudad;
            }
            set {
                if (Model.Ciudad != value) {
                    Model.Ciudad = value;
                    IsDirty = true;
                    RaisePropertyChanged(CiudadPropertyName);
                }
            }
        }

        public string ZonaPostal {
            get {
                return Model.ZonaPostal;
            }
            set {
                if (Model.ZonaPostal != value) {
                    Model.ZonaPostal = value;
                    IsDirty = true;
                    RaisePropertyChanged(ZonaPostalPropertyName);
                }
            }
        }

        public string Telefono {
            get {
                return Model.Telefono;
            }
            set {
                if (Model.Telefono != value) {
                    Model.Telefono = value;
                    IsDirty = true;
                    RaisePropertyChanged(TelefonoPropertyName);
                }
            }
        }

        public string Fax {
            get {
                return Model.Fax;
            }
            set {
                if (Model.Fax != value) {
                    Model.Fax = value;
                    IsDirty = true;
                    RaisePropertyChanged(FaxPropertyName);
                }
            }
        }

        public string Email {
            get {
                return Model.Email;
            }
            set {
                if (Model.Email != value) {
                    Model.Email = value;
                    IsDirty = true;
                    RaisePropertyChanged(EmailPropertyName);
                }
            }
        }

        public string Notas {
            get {
                return Model.Notas;
            }
            set {
                if (Model.Notas != value) {
                    Model.Notas = value;
                    IsDirty = true;
                    RaisePropertyChanged(NotasPropertyName);
                }
            }
        }

        public int ConsecutivoRutaDeComercializacion {
            get {
                return Model.ConsecutivoRutaDeComercializacion;
            }
            set {
                if (Model.ConsecutivoRutaDeComercializacion != value) {
                    Model.ConsecutivoRutaDeComercializacion = value;
                }
            }
        }

        [LibCustomValidation("RutaDeComercializacionValidate")]
        [LibGridColum("Ruta de Comercialización", ColumnOrder = 4, Width = 300)]
        public string RutaDeComercializacion {
            get {
                return Model.RutaDeComercializacion;
            }
            set {
                if (Model.RutaDeComercializacion != value) {
                    Model.RutaDeComercializacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(RutaDeComercializacionPropertyName);
                    if (LibString.IsNullOrEmpty(RutaDeComercializacion, true)) {
                        ConexionRutaDeComercializacion = null;
                    }
                }
            }
        }

        public decimal ComisionPorVenta {
            get {
                return Model.ComisionPorVenta;
            }
            set {
                if (Model.ComisionPorVenta != value) {
                    Model.ComisionPorVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComisionPorVentaPropertyName);
                }
            }
        }

        public decimal ComisionPorCobro {
            get {
                return Model.ComisionPorCobro;
            }
            set {
                if (Model.ComisionPorCobro != value) {
                    Model.ComisionPorCobro = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComisionPorCobroPropertyName);
                }
            }
        }

        public decimal TopeInicialVenta1 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeInicialVenta1, 2);
            }
            set {
                if (Model.TopeInicialVenta1 != value) {
                    Model.TopeInicialVenta1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeInicialVenta1PropertyName);
                }
            }
        }

        [LibCustomValidation("TopeFinalVenta1Validating")]
        public decimal TopeFinalVenta1 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeFinalVenta1, 2);
            }
            set {
                if (Model.TopeFinalVenta1 != value) {
                    Model.TopeFinalVenta1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta1PropertyName);
                    RaisePropertyChanged(TopeInicialVenta2PropertyName);
                    if (value != 0) {
                        TopeFinalVenta5 = TopeFinalDeVentaMasAlto();                        
                    }                   
                }
            }
        }

        [LibCustomValidation("PorcentajeVentas1Validating")]
        public decimal PorcentajeVentas1 {
            get {
                return LibMath.RoundToNDecimals(Model.PorcentajeVentas1, 2);
            }
            set {
                if (Model.PorcentajeVentas1 != value) {
                    Model.PorcentajeVentas1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas1PropertyName);
                }
            }
        }

        public decimal TopeInicialVenta2 {
            get {
                decimal vTopeInicialVenta2=0;
                if (UsaComisionPorVenta && (Action == eAccionSR.Consultar || Action == eAccionSR.Modificar) && TopeFinalVenta1 != 0) {
                    vTopeInicialVenta2 = TopeFinalVenta1 + 1;
                }
                return LibMath.RoundToNDecimals(vTopeInicialVenta2, 2);
            }           
        }    

        [LibCustomValidation("TopeFinalVenta2Validating")]
        public decimal TopeFinalVenta2 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeFinalVenta2, 2);
            }
            set {
                if (Model.TopeFinalVenta2 != value) {
                    Model.TopeFinalVenta2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta2PropertyName);
                    RaisePropertyChanged(TopeInicialVenta3PropertyName);
                    if (value != 0) {                        
                        TopeFinalVenta5 = TopeFinalDeVentaMasAlto();                        
                    }                     
                }
            }
        }

        [LibCustomValidation("PorcentajeVentas2Validating")]
        public decimal PorcentajeVentas2 {
            get {
                return LibMath.RoundToNDecimals(Model.PorcentajeVentas2, 2);
            }
            set {
                if (Model.PorcentajeVentas2 != value) {
                    Model.PorcentajeVentas2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas2PropertyName);
                }
            }
        }

        public decimal TopeInicialVenta3 {
            get {
                decimal vTopeInicialVenta3 = 0;
                if (UsaComisionPorVenta && (Action == eAccionSR.Consultar || Action == eAccionSR.Modificar) && TopeFinalVenta2 != 0) {
                    vTopeInicialVenta3 = TopeFinalVenta2 + 1;
                }
                return LibMath.RoundToNDecimals(vTopeInicialVenta3, 2);
            }          
        }

        [LibCustomValidation("TopeFinalVenta3Validating")]
        public decimal TopeFinalVenta3 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeFinalVenta3, 2);
            }
            set {
                if (Model.TopeFinalVenta3 != value) {
                    Model.TopeFinalVenta3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta3PropertyName);
                    RaisePropertyChanged(TopeInicialVenta4PropertyName);
                    if (value != 0) {                       
                        TopeFinalVenta5 = TopeFinalDeVentaMasAlto();                        
                    }                    
                }
            }
        }

        [LibCustomValidation("PorcentajeVentas3Validating")]
        public decimal PorcentajeVentas3 {
            get {
                return LibMath.RoundToNDecimals(Model.PorcentajeVentas3, 2);
            }
            set {
                if (Model.PorcentajeVentas3 != value) {
                    Model.PorcentajeVentas3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas3PropertyName);
                }
            }
        }

        public decimal TopeInicialVenta4 {
            get {
                decimal vTopeInicialVenta4 = 0;
                if (UsaComisionPorVenta && (Action == eAccionSR.Consultar || Action == eAccionSR.Modificar) && TopeFinalVenta3 != 0) {
                    vTopeInicialVenta4 = TopeFinalVenta3 + 1;
                }
                return LibMath.RoundToNDecimals(vTopeInicialVenta4, 2);
            }
        }

        [LibCustomValidation("TopeFinalVenta4Validating")]
        public decimal TopeFinalVenta4 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeFinalVenta4, 2);
            }
            set {
                if (Model.TopeFinalVenta4 != value) {
                    Model.TopeFinalVenta4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta4PropertyName);
                    if (value != 0) {
                        TopeFinalVenta5 = TopeFinalDeVentaMasAlto();                        
                    }
                }
            }
        }

        [LibCustomValidation("PorcentajeVentas4Validating")]
        public decimal PorcentajeVentas4 {
            get {
                return LibMath.RoundToNDecimals(Model.PorcentajeVentas4, 2);
            }
            set {
                if (Model.PorcentajeVentas4 != value) {
                    Model.PorcentajeVentas4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeVentas4PropertyName);
                }
            }
        }

        public decimal TopeFinalVenta5 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeFinalVenta5, 2);
            }
            set {
                if (Model.TopeFinalVenta5 != value) {
                    Model.TopeFinalVenta5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalVenta5PropertyName);
                }
            }
        }

        [LibCustomValidation("PorcentajeVentas5Validating")]
        public decimal PorcentajeVentas5 {
            get {
                return LibMath.RoundToNDecimals(Model.PorcentajeVentas5, 2);
            }
            set {
                if (Model.PorcentajeVentas5 != value) {
                    Model.PorcentajeVentas5 = value;
                    RaisePropertyChanged(PorcentajeVentas5PropertyName);
                }
            }
        }

        public decimal TopeInicialCobranza1 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeInicialCobranza1, 2);
            }
            set {
                if (Model.TopeInicialCobranza1 != value) {
                    Model.TopeInicialCobranza1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeInicialCobranza1PropertyName);                  
                }
            }
        }

        [LibCustomValidation("TopeFinalCobranza1Validating")]
        public decimal TopeFinalCobranza1 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeFinalCobranza1, 2);
            }
            set {
                if (Model.TopeFinalCobranza1 != value) {
                    Model.TopeFinalCobranza1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza1PropertyName);
                    RaisePropertyChanged(TopeInicialCobranza2PropertyName);
                    if (value != 0) {                        
                        TopeFinalCobranza5 = TopeFinalDeCobranzaMasAlto();                      
                    }                    
                }
            }
        }

        [LibCustomValidation("PorcentajeCobranza1Validating")]
        public decimal PorcentajeCobranza1 {
            get {
                return LibMath.RoundToNDecimals(Model.PorcentajeCobranza1, 2);
            }
            set {
                if (Model.PorcentajeCobranza1 != value) {
                    Model.PorcentajeCobranza1 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza1PropertyName);
                }
            }
        }

        public decimal TopeInicialCobranza2 {
            get {
                decimal vTopeInicialCobranza2 = 0;
                if (UsaComisionPorCobranza && (Action == eAccionSR.Consultar || Action == eAccionSR.Modificar) && TopeFinalCobranza1 != 0) {
                    vTopeInicialCobranza2 = TopeFinalCobranza1 + 1;
                }
                return LibMath.RoundToNDecimals(vTopeInicialCobranza2, 2);
            }           
        }

        [LibCustomValidation("TopeFinalCobranza2Validating")]
        public decimal TopeFinalCobranza2 {
            get {
                return LibMath.RoundToNDecimals(Model.TopeFinalCobranza2, 2);
            }
            set {
                if (Model.TopeFinalCobranza2 != value) {
                    Model.TopeFinalCobranza2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza2PropertyName);
                    RaisePropertyChanged(TopeInicialCobranza3PropertyName);
                    if (value != 0) {                        
                        TopeFinalCobranza5 = TopeFinalDeCobranzaMasAlto();                        
                    }                   
                }
            }
        }

        [LibCustomValidation("PorcentajeCobranza2Validating")]
        public decimal PorcentajeCobranza2 {
            get {
                return Model.PorcentajeCobranza2;
            }
            set {
                if (Model.PorcentajeCobranza2 != value) {
                    Model.PorcentajeCobranza2 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza2PropertyName);
                }
            }
        }

        public decimal TopeInicialCobranza3 {
            get {
                decimal TopeInicialCobranza3 = 0;
                if (UsaComisionPorCobranza && (Action == eAccionSR.Consultar || Action == eAccionSR.Modificar) && TopeFinalCobranza2 != 0) {
                    TopeInicialCobranza3 = TopeFinalCobranza2 + 1;
                }
                return LibMath.RoundToNDecimals(TopeInicialCobranza3, 2);
            }         
        }

        [LibCustomValidation("TopeFinalCobranza3Validating")]
        public decimal TopeFinalCobranza3 {
            get {
                return Model.TopeFinalCobranza3;
            }
            set {
                if (Model.TopeFinalCobranza3 != value) {
                    Model.TopeFinalCobranza3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza3PropertyName);
                    RaisePropertyChanged(TopeInicialCobranza4PropertyName);
                    if (value != 0) {                        
                        TopeFinalCobranza5 = TopeFinalDeCobranzaMasAlto();                        
                    }                   
                }
            }
        }

        [LibCustomValidation("PorcentajeCobranza3Validating")]
        public decimal PorcentajeCobranza3 {
            get {
                return Model.PorcentajeCobranza3;
            }
            set {
                if (Model.PorcentajeCobranza3 != value) {
                    Model.PorcentajeCobranza3 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza3PropertyName);
                }
            }
        }

        public decimal TopeInicialCobranza4 {
            get {
                decimal vTopeInicialCobranza4 = 0;
                if (UsaComisionPorCobranza && (Action == eAccionSR.Consultar || Action == eAccionSR.Modificar) && TopeFinalCobranza3 != 0) {
                    vTopeInicialCobranza4 = TopeFinalCobranza3 + 1;
                }
                return LibMath.RoundToNDecimals(vTopeInicialCobranza4, 2);
            }          
        }

        [LibCustomValidation("TopeFinalCobranza4Validating")]
        public decimal TopeFinalCobranza4 {
            get {
                return Model.TopeFinalCobranza4;
            }
            set {
                if (Model.TopeFinalCobranza4 != value) {
                    Model.TopeFinalCobranza4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza4PropertyName);
                    if (value != 0) {
                        TopeFinalCobranza5 = TopeFinalDeCobranzaMasAlto();                       
                    }
                }
            }
        }

        [LibCustomValidation("PorcentajeCobranza4Validating")]
        public decimal PorcentajeCobranza4 {
            get {
                return Model.PorcentajeCobranza4;
            }
            set {
                if (Model.PorcentajeCobranza4 != value) {
                    Model.PorcentajeCobranza4 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza4PropertyName);
                }
            }
        }

        [LibCustomValidation("TopeFinalCobranza5Validating")]
        public decimal TopeFinalCobranza5 {
            get {
                return Model.TopeFinalCobranza5;
            }
            set {
                if (Model.TopeFinalCobranza5 != value) {
                    Model.TopeFinalCobranza5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(TopeFinalCobranza5PropertyName);
                }
            }
        }

        [LibCustomValidation("PorcentajeCobranza5Validating")]
        public decimal PorcentajeCobranza5 {
            get {
                return Model.PorcentajeCobranza5;
            }
            set {
                if (Model.PorcentajeCobranza5 != value) {
                    Model.PorcentajeCobranza5 = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajeCobranza5PropertyName);
                }
            }
        }

        public bool UsaComisionPorVenta {
            get {
                return Model.UsaComisionPorVentaAsBool;
            }
            set {
                if (Model.UsaComisionPorVentaAsBool != value) {
                    Model.UsaComisionPorVentaAsBool = value;
                    IsDirty = true;
                    if (!value) {
                        LimpiarTopesDeVenta();
                        LimpiarPorcentajesDeVenta();
                    }
                    RaisePropertyChanged(UsaComisionPorVentaPropertyName);
                    RaisePropertyChanged(IsEnabledDetalleComisionesPorVentasPropertyName);
                }
            }
        }

        public bool UsaComisionPorCobranza {
            get {
                return Model.UsaComisionPorCobranzaAsBool;
            }
            set {
                if (Model.UsaComisionPorCobranzaAsBool != value) {
                    Model.UsaComisionPorCobranzaAsBool = value;
                    IsDirty = true;
                    if (!value) {
                        LimpiarTopesDeCobranza();
                        LimpiarPorcentajesDeCobranza();
                    }
                    RaisePropertyChanged(UsaComisionPorCobranzaPropertyName);
                    RaisePropertyChanged(IsEnabledDetalleComisionesPorCobranzasPropertyName);
                }
            }
        }

        public string CodigoLote {
            get {
                return Model.CodigoLote;
            }
            set {
                if (Model.CodigoLote != value) {
                    Model.CodigoLote = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoLotePropertyName);
                }
            }
        }

        public string NombreOperador {
            get {
                return Model.NombreOperador;
            }
            set {
                if (Model.NombreOperador != value) {
                    Model.NombreOperador = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreOperadorPropertyName);
                }
            }
        }

        public bool IsEnabledDetalleComisionesPorVentas {
            get {
                if (Action == eAccionSR.Insertar) {
                    return UsaComisionPorVenta;
                } else {
                    return IsEnabled && UsaComisionPorVenta;
                }
            }
        }

        public bool IsEnabledDetalleComisionesPorCobranzas {
            get {
                if (Action == eAccionSR.Insertar) {
                    return UsaComisionPorCobranza;
                } else {
                    return IsEnabled && UsaComisionPorCobranza;
                }
            }
        }

        public bool IsEnabledAsignacionDeComisiones {
            get {
                return CalculaComisionesSobreRenglones();
            }
        }


        public bool IsEnabledComisionesPorLineaDeProducto {
            get {
                return CalculaComisionesSobreElTotal();
            }
        }

        public bool IsVisibleAsignacionDeComisiones {
            get {
                return CalculaComisionesSobreRenglones();
            }
        }

        public bool IsVisibleComisionesPorLineaDeProducto {
            get {
                return CalculaComisionesSobreElTotal();
            }
        }

        public bool IsVisibleRutaDeComercializacion {
            get {
                return LibDefGen.IsInternalSystem();
            }
        }

        public DateTime FechaUltimaModificacion {
            get {
                return Model.FechaUltimaModificacion;
            }
            set {
                if (Model.FechaUltimaModificacion != value) {
                    Model.FechaUltimaModificacion = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaUltimaModificacionPropertyName);
                }
            }
        }

        public eStatusVendedor[] ArrayStatusVendedor {
            get {
                return LibEnumHelper<eStatusVendedor>.GetValuesInArray();
            }
        }
        //[LibRequired(ErrorMessage = "Comisiones de Vendedor por Linea de Producto es requerido.")]
        public VendedorDetalleComisionesMngViewModel DetailVendedorDetalleComisiones {
            get;
            set;
        }

        public FkCiudadViewModel ConexionCiudad {
            get {
                return _ConexionCiudad;
            }
            set {
                if (_ConexionCiudad != value) {
                    _ConexionCiudad = value;
                    if (_ConexionCiudad != null) {
                        Ciudad = _ConexionCiudad.NombreCiudad;
                    }
                }
                if (_ConexionCiudad == null) {
                    Ciudad = string.Empty;
                }
            }
        }

        public string LabelMontosDeComisiones {
            get {
                return "Los montos de las comisiones están expresados en " + ObtenerMonedaLocal();
            }
        }

        public FkRutaDeComercializacionViewModel ConexionRutaDeComercializacion {
            get {
                return _ConexionRutaDeComercializacion;
            }
            set {
                if (_ConexionRutaDeComercializacion != value) {
                    _ConexionRutaDeComercializacion = value;
                    if (_ConexionRutaDeComercializacion != null) {
                        ConsecutivoRutaDeComercializacion = _ConexionRutaDeComercializacion.Consecutivo;
                        RutaDeComercializacion = _ConexionRutaDeComercializacion.Descripcion;
                    } else {
                        ConsecutivoRutaDeComercializacion = 0;
                        RutaDeComercializacion = string.Empty;
                    }
                    RaisePropertyChanged(RutaDeComercializacionPropertyName);
                }
                if (_ConexionRutaDeComercializacion == null) {
                    RutaDeComercializacion = string.Empty;
                }
            }
        }

        public RelayCommand<string> ChooseCiudadCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseRutaDeComercializacionCommand {
            get;
            private set;
        }

        public RelayCommand<string> CreateVendedorDetalleComisionesCommand {
            get { return DetailVendedorDetalleComisiones.CreateCommand; }
        }

        public RelayCommand<string> UpdateVendedorDetalleComisionesCommand {
            get { return DetailVendedorDetalleComisiones.UpdateCommand; }
        }

        public RelayCommand<string> DeleteVendedorDetalleComisionesCommand {
            get { return DetailVendedorDetalleComisiones.DeleteCommand; }
        }
        #endregion //Propiedades
        #region Constructores
        public VendedorViewModel()
            : this(new Ccl.Vendedor.Vendedor(), eAccionSR.Insertar) {
        }

        public VendedorViewModel(Ccl.Vendedor.Vendedor initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = ConsecutivoCompaniaPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Ccl.Vendedor.Vendedor valModel) {
            base.InitializeLookAndFeel(valModel);
            if (LibString.IsNullOrEmpty(Codigo, true)) {
                Codigo = GenerarProximoCodigo();
            }
            if (Action == eAccionSR.Insertar) {
                CargarRutaDeComercializacionPorDefecto();
            }
        }

        protected override Ccl.Vendedor.Vendedor FindCurrentRecord(Ccl.Vendedor.Vendedor valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "VendedorGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<Ccl.Vendedor.Vendedor>, IList<Ccl.Vendedor.Vendedor>> GetBusinessComponent() {
            return new clsVendedorNav();
        }

        protected override void InitializeDetails() {
            DetailVendedorDetalleComisiones = new VendedorDetalleComisionesMngViewModel(this, Model.DetailVendedorDetalleComisiones, Action);
            DetailVendedorDetalleComisiones.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnCreated);
            DetailVendedorDetalleComisiones.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnUpdated);
            DetailVendedorDetalleComisiones.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnDeleted);
            DetailVendedorDetalleComisiones.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel>>(DetailVendedorDetalleComisiones_OnSelectedItemChanged);
        }
        #region VendedorDetalleComisiones

        private void DetailVendedorDetalleComisiones_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                UpdateVendedorDetalleComisionesCommand.RaiseCanExecuteChanged();
                DeleteVendedorDetalleComisionesCommand.RaiseCanExecuteChanged();
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailVendedorDetalleComisiones_OnDeleted(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                IsDirty = true;
                Model.DetailVendedorDetalleComisiones.Remove(e.ViewModel.GetModel());
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailVendedorDetalleComisiones_OnUpdated(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                IsDirty = e.ViewModel.IsDirty;
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void DetailVendedorDetalleComisiones_OnCreated(object sender, SearchCollectionChangedEventArgs<VendedorDetalleComisionesViewModel> e) {
            try {
                Model.DetailVendedorDetalleComisiones.Add(e.ViewModel.GetModel());
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //VendedorDetalleComisiones

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseCiudadCommand = new RelayCommand<string>(ExecuteChooseCiudadCommand);
            ChooseRutaDeComercializacionCommand = new RelayCommand<string>(ExecuteChooseRutaDeComercializacionCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            //ConexionCiudad = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCiudadViewModel>("Ciudad", LibSearchCriteria.CreateCriteria("NombreCiudad", Ciudad), new Saw.Brl.SttDef.clsSettValueByCompanyNav());
            ConexionRutaDeComercializacion = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkRutaDeComercializacionViewModel>("Vendedor", LibSearchCriteria.CreateCriteria("Consecutivo", ConsecutivoRutaDeComercializacion), new Saw.Brl.Tablas.clsRutaDeComercializacionNav());
        }

        protected override void ExecuteAction() {
            if (Action == eAccionSR.Insertar || Action == eAccionSR.Modificar) {
                if (!UsaComisionPorVenta && !UsaComisionPorCobranza) {
                    LibMessages.MessageBox.Alert(this, "Recuerde asignar Comisiones de Venta y/o Cobranza a este Vendedor.", ModuleName);
                }
            }
            base.ExecuteAction();
            if (Action == eAccionSR.Insertar) {
                LimpiarTopesDeVenta();
                LimpiarPorcentajesDeVenta();
                LimpiarPorcentajesDeCobranza();
                LimpiarTopesDeCobranza();
            }
        }

        private void ExecuteChooseCiudadCommand(string valNombreCiudad) {
            try {
                if (valNombreCiudad == null) {
                    valNombreCiudad = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("NombreCiudad", valNombreCiudad);
                ConexionCiudad = null;
                ConexionCiudad = ChooseRecord<FkCiudadViewModel>("Ciudad", vDefaultCriteria, null, string.Empty);
            } catch (AccessViolationException) {
                throw;
            } catch (Exception vEx) {
                LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseRutaDeComercializacionCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionRutaDeComercializacion = ChooseRecord<FkRutaDeComercializacionViewModel>("Ruta de Comercialización", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionRutaDeComercializacion != null) {
                    Model.ConsecutivoRutaDeComercializacion = ConexionRutaDeComercializacion.Consecutivo;
                    RutaDeComercializacion = ConexionRutaDeComercializacion.Descripcion;
                } else {
                    Model.ConsecutivoRutaDeComercializacion = 0;
                    RutaDeComercializacion = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private string ObtenerMonedaLocal() {
            Saw.Lib.clsNoComunSaw vMoneda = new Saw.Lib.clsNoComunSaw();
            return vMoneda.InstanceMonedaLocalActual.NombreMoneda(LibDate.Today()) + "es.";
        }

        private decimal TopeFinalDeVentaMasAlto() {
            decimal vTopeMasAlto = 0;
            vTopeMasAlto = vTopeMasAlto < TopeFinalVenta1 ? TopeFinalVenta1 : vTopeMasAlto;
            vTopeMasAlto = vTopeMasAlto < TopeFinalVenta2 ? TopeFinalVenta2 : vTopeMasAlto;
            vTopeMasAlto = vTopeMasAlto < TopeFinalVenta3 ? TopeFinalVenta3 : vTopeMasAlto;
            vTopeMasAlto = vTopeMasAlto < TopeFinalVenta4 ? TopeFinalVenta4 : vTopeMasAlto;
            return vTopeMasAlto;
        }

        private decimal TopeFinalDeCobranzaMasAlto() {
            decimal vTopeMasAlto = 0;
            vTopeMasAlto = vTopeMasAlto < TopeFinalCobranza1 ? TopeFinalCobranza1 : vTopeMasAlto;
            vTopeMasAlto = vTopeMasAlto < TopeFinalCobranza2 ? TopeFinalCobranza2 : vTopeMasAlto;
            vTopeMasAlto = vTopeMasAlto < TopeFinalCobranza3 ? TopeFinalCobranza3 : vTopeMasAlto;
            vTopeMasAlto = vTopeMasAlto < TopeFinalCobranza4 ? TopeFinalCobranza4 : vTopeMasAlto;
            return vTopeMasAlto;
        }

        private string GenerarProximoCodigo() {
            XElement vResulset = GetBusinessComponent().QueryInfo(eProcessMessageType.Message, "ProximoCodigo", Mfc.GetIntAsParam("Compania"), false);
            return LibXml.GetPropertyString(vResulset, "Codigo");
        }

        private static bool CalculaComisionesSobreRenglones() {
            eComisionesEnFactura vFormaDeAsignarComisionesDeVendedor = (eComisionesEnFactura)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ComisionesEnFactura");
            return vFormaDeAsignarComisionesDeVendedor != eComisionesEnFactura.SobreRenglones;
        }

        private static bool CalculaComisionesSobreElTotal() {
            eComisionesEnFactura vFormaDeAsignarComisionesDeVendedor = (eComisionesEnFactura)LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Parametros", "ComisionesEnFactura");
            return vFormaDeAsignarComisionesDeVendedor != eComisionesEnFactura.SobreTotalFactura;
        }

        #region Limpieza de Campos de Comisiones
        private void LimpiarTopesDeVenta() {
            TopeInicialVenta1 = 0;
            RaisePropertyChanged(TopeInicialVenta1PropertyName);                       
            TopeFinalVenta1 = 0;
            RaisePropertyChanged(TopeFinalVenta1PropertyName);
            TopeFinalVenta2 = 0;
            RaisePropertyChanged(TopeFinalVenta2PropertyName);
            TopeFinalVenta3 = 0;
            RaisePropertyChanged(TopeFinalVenta3PropertyName);
            TopeFinalVenta4 = 0;
            RaisePropertyChanged(TopeFinalVenta4PropertyName);
            TopeFinalVenta5 = 0;
            RaisePropertyChanged(TopeFinalVenta5PropertyName);
        }

        private void LimpiarPorcentajesDeVenta() {
            PorcentajeVentas1 = 0;
            RaisePropertyChanged(PorcentajeVentas1PropertyName);
            PorcentajeVentas2 = 0;
            RaisePropertyChanged(PorcentajeVentas2PropertyName);
            PorcentajeVentas3 = 0;
            RaisePropertyChanged(PorcentajeVentas3PropertyName);
            PorcentajeVentas4 = 0;
            RaisePropertyChanged(PorcentajeVentas4PropertyName);
            PorcentajeVentas5 = 0;
            RaisePropertyChanged(PorcentajeVentas5PropertyName);
        }

        private void LimpiarTopesDeCobranza() {
            TopeInicialCobranza1 = 0;
            RaisePropertyChanged(TopeInicialCobranza1PropertyName);                      
            TopeFinalCobranza1 = 0;
            RaisePropertyChanged(TopeFinalCobranza1PropertyName);
            TopeFinalCobranza2 = 0;
            RaisePropertyChanged(TopeFinalCobranza2PropertyName);
            TopeFinalCobranza3 = 0;
            RaisePropertyChanged(TopeFinalCobranza3PropertyName);
            TopeFinalCobranza4 = 0;
            RaisePropertyChanged(TopeFinalCobranza4PropertyName);
            TopeFinalCobranza5 = 0;
            RaisePropertyChanged(TopeFinalCobranza5PropertyName);
        }

        private void LimpiarPorcentajesDeCobranza() {
            PorcentajeCobranza1 = 0;
            RaisePropertyChanged(PorcentajeCobranza1PropertyName);
            PorcentajeCobranza2 = 0;
            RaisePropertyChanged(PorcentajeCobranza2PropertyName);
            PorcentajeCobranza3 = 0;
            RaisePropertyChanged(PorcentajeCobranza3PropertyName);
            PorcentajeCobranza4 = 0;
            RaisePropertyChanged(PorcentajeCobranza4PropertyName);
            PorcentajeCobranza5 = 0;
            RaisePropertyChanged(PorcentajeCobranza5PropertyName);
        }
        #endregion //Limpieza de Campos de Comisiones

        #region Validaciones de Topes de Comisiones
        private ValidationResult ValidarTopesDeComisiones(decimal valTopeInicial, decimal valTopeFinal, decimal valPorcentaje, bool valUsaEsteTipoDeComision) {
            ValidationResult vResult = ValidationResult.Success;
            if (valPorcentaje != 0 && valTopeFinal != 0 && valTopeInicial != 0) {
                if (valUsaEsteTipoDeComision && (valTopeFinal < valTopeInicial)) {
                    vResult = new ValidationResult(MensajeTopeFinalDeVentaMenorAlTopeInicial);
                }
            }
            return vResult;
        }

        private ValidationResult TopeFinalVenta1Validating() {
            return ValidarTopesDeComisiones(TopeInicialVenta1, TopeFinalVenta1, PorcentajeVentas1, UsaComisionPorVenta);
        }

        private ValidationResult TopeFinalVenta2Validating() {
            return ValidarTopesDeComisiones(TopeInicialVenta2, TopeFinalVenta2, PorcentajeVentas2, UsaComisionPorVenta);
        }

        private ValidationResult TopeFinalVenta3Validating() {
            return ValidarTopesDeComisiones(TopeInicialVenta3, TopeFinalVenta3, PorcentajeVentas3, UsaComisionPorVenta);
        }

        private ValidationResult TopeFinalVenta4Validating() {
            return ValidarTopesDeComisiones(TopeInicialVenta4, TopeFinalVenta4, PorcentajeVentas4, UsaComisionPorVenta);
        }

        private ValidationResult TopeFinalCobranza1Validating() {
            return ValidarTopesDeComisiones(TopeInicialCobranza1, TopeFinalCobranza1, PorcentajeCobranza1, UsaComisionPorCobranza);
        }

        private ValidationResult TopeFinalCobranza2Validating() {
            return ValidarTopesDeComisiones(TopeInicialCobranza2, TopeFinalCobranza2, PorcentajeCobranza2, UsaComisionPorCobranza);
        }

        private ValidationResult TopeFinalCobranza3Validating() {
            return ValidarTopesDeComisiones(TopeInicialCobranza3, TopeFinalCobranza3, PorcentajeCobranza3, UsaComisionPorCobranza);
        }

        private ValidationResult TopeFinalCobranza4Validating() {
            return ValidarTopesDeComisiones(TopeInicialCobranza4, TopeFinalCobranza4, PorcentajeCobranza4, UsaComisionPorCobranza);
        }

        private void CargarRutaDeComercializacionPorDefecto() {
            ConsecutivoRutaDeComercializacion = 1;
            ConexionRutaDeComercializacion = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkRutaDeComercializacionViewModel>("Vendedor", LibSearchCriteria.CreateCriteria("Consecutivo", ConsecutivoRutaDeComercializacion), new Saw.Brl.Tablas.clsRutaDeComercializacionNav());
        }

        #endregion //Validaciones de Topes de Comisiones
        #region Validaciones de Porcentajes de Comisiones
        private ValidationResult ValidarPorcentajesDeComisionesNivel1(decimal valPorcentajeComisionActual, decimal valPorcentajeComisionSiguiente, decimal valTopeFinalDelSiguienteNivel, bool valUsaEsteTipoDeComision) {
            ValidationResult vResult = ValidationResult.Success;
            if (valUsaEsteTipoDeComision) {
                if (valPorcentajeComisionActual > valPorcentajeComisionSiguiente && valTopeFinalDelSiguienteNivel != 0) {
                    vResult = new ValidationResult(MensajePorcentajeDeComisionConValorNoAceptado);
                } else if (valPorcentajeComisionActual > 100) {
                    vResult = new ValidationResult(MensajePorcentajeDeComisionMayorA100);
                }
            }
            return vResult;
        }

        private ValidationResult ValidarPorcentajesDeComisiones(decimal valPorcentajeComisionActual, decimal valPorcentajeComisionAnterior, decimal valPorcentajeComisionSiguiente, decimal valTopeFinalNivelActual, decimal valTopeFinalSiguienteNivel, bool valUsaEsteTipoDeComision) {
            ValidationResult vResult = ValidationResult.Success;
            if (valUsaEsteTipoDeComision) {
                if (valTopeFinalNivelActual > 0 || valPorcentajeComisionActual > 0) {
                    if (valPorcentajeComisionActual <= valPorcentajeComisionAnterior || (valPorcentajeComisionActual > valPorcentajeComisionSiguiente && valTopeFinalSiguienteNivel != 0)) {
                        vResult = new ValidationResult(MensajePorcentajeDeComisionConValorNoAceptado);
                    } else if (valPorcentajeComisionActual > 100) {
                        vResult = new ValidationResult(MensajePorcentajeDeComisionMayorA100);
                    }
                }
            }
            return vResult;
        }

        private ValidationResult ValidarPorcentajesDeComisionesNivel5(decimal valPorcentajeComisionActual, decimal valPorcentajeComisionAnterior, bool valUsaEsteTipoDeComision) {
            ValidationResult vResult = ValidationResult.Success;
            if (valUsaEsteTipoDeComision) {
                if (valPorcentajeComisionActual <= valPorcentajeComisionAnterior) {
                    //vResult = new ValidationResult(MensajePorcentajeDeComisionConValorNoAceptado);
                } else if (valPorcentajeComisionActual > 100) {
                    vResult = new ValidationResult(MensajePorcentajeDeComisionMayorA100);
                }
            }
            return vResult;
        }

        private ValidationResult PorcentajeVentas1Validating() {
            return ValidarPorcentajesDeComisionesNivel1(PorcentajeVentas1, PorcentajeVentas2, TopeFinalVenta2, UsaComisionPorVenta);
        }

        private ValidationResult PorcentajeVentas2Validating() {
            return ValidarPorcentajesDeComisiones(PorcentajeVentas2, PorcentajeVentas1, PorcentajeVentas3, TopeFinalVenta2, TopeFinalVenta3, UsaComisionPorVenta);
        }

        private ValidationResult PorcentajeVentas3Validating() {
            return ValidarPorcentajesDeComisiones(PorcentajeVentas3, PorcentajeVentas2, PorcentajeVentas4, TopeFinalVenta3, TopeFinalVenta4, UsaComisionPorVenta);
        }

        private ValidationResult PorcentajeVentas4Validating() {
            return ValidarPorcentajesDeComisiones(PorcentajeVentas4, PorcentajeVentas3, (PorcentajeVentas5 == 0 ? PorcentajeVentas4 : PorcentajeVentas5), TopeFinalVenta4, TopeFinalVenta5, UsaComisionPorVenta); //Nivel4y5
        }

        private ValidationResult PorcentajeVentas5Validating() {
            return ValidarPorcentajesDeComisionesNivel5(PorcentajeVentas5, PorcentajeVentas4, UsaComisionPorVenta);
        }

        private ValidationResult PorcentajeCobranza1Validating() {
            return ValidarPorcentajesDeComisionesNivel1(PorcentajeCobranza1, PorcentajeCobranza2, TopeFinalCobranza2, UsaComisionPorCobranza);
        }
        private ValidationResult PorcentajeCobranza2Validating() {
            return ValidarPorcentajesDeComisiones(PorcentajeCobranza2, PorcentajeCobranza1, PorcentajeCobranza3, TopeFinalCobranza2, TopeFinalCobranza3, UsaComisionPorCobranza);
        }

        private ValidationResult PorcentajeCobranza3Validating() {
            return ValidarPorcentajesDeComisiones(PorcentajeCobranza3, PorcentajeCobranza2, PorcentajeCobranza4, TopeFinalCobranza3, TopeFinalCobranza4, UsaComisionPorCobranza);
        }
        private ValidationResult PorcentajeCobranza4Validating() {
            return ValidarPorcentajesDeComisiones(PorcentajeCobranza4, PorcentajeCobranza3, (PorcentajeCobranza5 == 0 ? PorcentajeCobranza4 : PorcentajeCobranza5), TopeFinalCobranza4, TopeFinalCobranza5, UsaComisionPorCobranza);
        }

        private ValidationResult PorcentajeCobranza5Validating() {
            return ValidarPorcentajesDeComisionesNivel5(PorcentajeCobranza5, PorcentajeCobranza4, UsaComisionPorCobranza);
        }

        private ValidationResult RutaDeComercializacionValidate() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Insertar) {
                if (LibString.IsNullOrEmpty((RutaDeComercializacion))) {
                    vResult = new ValidationResult("La ruta de comerciaización es requerida.");
                }
            }
            return vResult;
        }
        #endregion //Validaciones de Porcentajes de Comisiones

        #endregion //Metodos Generados
    } //End of class VendedorViewModel
} //End of namespace Galac.Adm.Uil.Vendedor

