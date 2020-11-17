using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class ArticuloInventarioViewModel : LibInputMasterViewModelMfc<ArticuloInventario> {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string DescripcionPropertyName = "Descripcion";
        public const string LineaDeProductoPropertyName = "LineaDeProducto";
        public const string StatusdelArticuloPropertyName = "StatusdelArticulo";
        public const string TipoDeArticuloPropertyName = "TipoDeArticulo";
        public const string AlicuotaIVAPropertyName = "AlicuotaIVA";
        public const string PrecioSinIVAPropertyName = "PrecioSinIVA";
        public const string PrecioConIVAPropertyName = "PrecioConIVA";
        public const string PrecioSinIVA2PropertyName = "PrecioSinIVA2";
        public const string PrecioConIVA2PropertyName = "PrecioConIVA2";
        public const string PrecioSinIVA3PropertyName = "PrecioSinIVA3";
        public const string PrecioConIVA3PropertyName = "PrecioConIVA3";
        public const string PrecioSinIVA4PropertyName = "PrecioSinIVA4";
        public const string PrecioConIVA4PropertyName = "PrecioConIVA4";
        public const string PorcentajeBaseImponiblePropertyName = "PorcentajeBaseImponible";
        public const string CostoUnitarioPropertyName = "CostoUnitario";
        public const string ExistenciaPropertyName = "Existencia";
        public const string CantidadMinimaPropertyName = "CantidadMinima";
        public const string CantidadMaximaPropertyName = "CantidadMaxima";
        public const string CategoriaPropertyName = "Categoria";
        public const string TipoDeProductoPropertyName = "TipoDeProducto";
        public const string NombreProgramaPropertyName = "NombrePrograma";
        public const string MarcaPropertyName = "Marca";
        public const string FechaDeVencimientoPropertyName = "FechaDeVencimiento";
        public const string UnidadDeVentaPropertyName = "UnidadDeVenta";
        public const string CampoDefinible1PropertyName = "CampoDefinible1";
        public const string CampoDefinible2PropertyName = "CampoDefinible2";
        public const string CampoDefinible3PropertyName = "CampoDefinible3";
        public const string CampoDefinible4PropertyName = "CampoDefinible4";
        public const string CampoDefinible5PropertyName = "CampoDefinible5";
        public const string MeCostoUnitarioPropertyName = "MeCostoUnitario";
        public const string UnidadDeVentaSecundariaPropertyName = "UnidadDeVentaSecundaria";
        public const string PorcentajeComisionPropertyName = "PorcentajeComision";
        public const string ExcluirDeComisionPropertyName = "ExcluirDeComision";
        public const string CantArtReservadoPropertyName = "CantArtReservado";
        public const string CodigoGrupoPropertyName = "CodigoGrupo";
        public const string RetornaAAlmacenPropertyName = "RetornaAAlmacen";
        public const string PedirDimensionPropertyName = "PedirDimension";
        public const string MargenGananciaPropertyName = "MargenGanancia";
        public const string MargenGanancia2PropertyName = "MargenGanancia2";
        public const string MargenGanancia3PropertyName = "MargenGanancia3";
        public const string MargenGanancia4PropertyName = "MargenGanancia4";
        public const string TipoArticuloInvPropertyName = "TipoArticuloInv";
        public const string ComisionaPorcentajePropertyName = "ComisionaPorcentaje";
        public const string UsaBalanzaPropertyName = "UsaBalanza";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Variables
        private FkLineaDeProductoViewModel _ConexionLineaDeProducto = null;
        private FkCategoriaViewModel _ConexionCategoria = null;
        //private FkUnidadDeVentaViewModel _ConexionUnidadDeVenta = null;
        #endregion //Variables
        #region Propiedades

        public override string ModuleName {
            get { return "Artículo Inventario"; }
        }

        public int  ConsecutivoCompania {
            get {
                return Model.ConsecutivoCompania;
            }
            set {
                if (Model.ConsecutivoCompania != value) {
                    Model.ConsecutivoCompania = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código del Artículo es requerido.")]
        [LibGridColum("Código del Artículo", MaxLength=15)]
        public string  Codigo {
            get {
                return Model.Codigo;
            }
            set {
                if (Model.Codigo != value) {
                    Model.Codigo = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", MaxLength=255)]
        public string  Descripcion {
            get {
                return Model.Descripcion;
            }
            set {
                if (Model.Descripcion != value) {
                    Model.Descripcion = value;
                    IsDirty = true;
                    RaisePropertyChanged(DescripcionPropertyName);
                }
            }
        }

        [LibGridColum("Linea De Producto", eGridColumType.Connection, ConnectionDisplayMemberPath = "Nombre", ConnectionModelPropertyName = "LineaDeProducto", ConnectionSearchCommandName = "ChooseLineaDeProductoCommand", MaxWidth=120)]
        public string  LineaDeProducto {
            get {
                return Model.LineaDeProducto;
            }
            set {
                if (Model.LineaDeProducto != value) {
                    Model.LineaDeProducto = value;
                    IsDirty = true;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                    if (LibString.IsNullOrEmpty(LineaDeProducto, true)) {
                        ConexionLineaDeProducto = null;
                    }
                }
            }
        }

        public eStatusArticulo  StatusdelArticulo {
            get {
                return Model.StatusdelArticuloAsEnum;
            }
            set {
                if (Model.StatusdelArticuloAsEnum != value) {
                    Model.StatusdelArticuloAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(StatusdelArticuloPropertyName);
                }
            }
        }

        public eTipoDeArticulo  TipoDeArticulo {
            get {
                return Model.TipoDeArticuloAsEnum;
            }
            set {
                if (Model.TipoDeArticuloAsEnum != value) {
                    Model.TipoDeArticuloAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeArticuloPropertyName);
                }
            }
        }

        [LibGridColum("Alícuota IVA", eGridColumType.Enum, PrintingMemberPath = "AlicuotaIVAStr")]
        public eTipoDeAlicuota  AlicuotaIVA {
            get {
                return Model.AlicuotaIVAAsEnum;
            }
            set {
                if (Model.AlicuotaIVAAsEnum != value) {
                    Model.AlicuotaIVAAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(AlicuotaIVAPropertyName);
                }
            }
        }

        [LibGridColum("Precio Sin IVA", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  PrecioSinIVA {
            get {
                return Model.PrecioSinIVA;
            }
            set {
                if (Model.PrecioSinIVA != value) {
                    Model.PrecioSinIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrecioSinIVAPropertyName);
                }
            }
        }

        [LibGridColum("Precio Con IVA", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  PrecioConIVA {
            get {
                return Model.PrecioConIVA;
            }
            set {
                if (Model.PrecioConIVA != value) {
                    Model.PrecioConIVA = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrecioConIVAPropertyName);
                }
            }
        }

        //public decimal  PrecioSinIVA2 {
        //    get {
        //        return Model.PrecioSinIVA2;
        //    }
        //    set {
        //        if (Model.PrecioSinIVA2 != value) {
        //            Model.PrecioSinIVA2 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PrecioSinIVA2PropertyName);
        //        }
        //    }
        //}

        //public decimal  PrecioConIVA2 {
        //    get {
        //        return Model.PrecioConIVA2;
        //    }
        //    set {
        //        if (Model.PrecioConIVA2 != value) {
        //            Model.PrecioConIVA2 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PrecioConIVA2PropertyName);
        //        }
        //    }
        //}

        //public decimal  PrecioSinIVA3 {
        //    get {
        //        return Model.PrecioSinIVA3;
        //    }
        //    set {
        //        if (Model.PrecioSinIVA3 != value) {
        //            Model.PrecioSinIVA3 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PrecioSinIVA3PropertyName);
        //        }
        //    }
        //}

        //public decimal  PrecioConIVA3 {
        //    get {
        //        return Model.PrecioConIVA3;
        //    }
        //    set {
        //        if (Model.PrecioConIVA3 != value) {
        //            Model.PrecioConIVA3 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PrecioConIVA3PropertyName);
        //        }
        //    }
        //}

        //public decimal  PrecioSinIVA4 {
        //    get {
        //        return Model.PrecioSinIVA4;
        //    }
        //    set {
        //        if (Model.PrecioSinIVA4 != value) {
        //            Model.PrecioSinIVA4 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PrecioSinIVA4PropertyName);
        //        }
        //    }
        //}

        //public decimal  PrecioConIVA4 {
        //    get {
        //        return Model.PrecioConIVA4;
        //    }
        //    set {
        //        if (Model.PrecioConIVA4 != value) {
        //            Model.PrecioConIVA4 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PrecioConIVA4PropertyName);
        //        }
        //    }
        //}

        //public decimal  PorcentajeBaseImponible {
        //    get {
        //        return Model.PorcentajeBaseImponible;
        //    }
        //    set {
        //        if (Model.PorcentajeBaseImponible != value) {
        //            Model.PorcentajeBaseImponible = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PorcentajeBaseImponiblePropertyName);
        //        }
        //    }
        //}

        //public decimal  CostoUnitario {
        //    get {
        //        return Model.CostoUnitario;
        //    }
        //    set {
        //        if (Model.CostoUnitario != value) {
        //            Model.CostoUnitario = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CostoUnitarioPropertyName);
        //        }
        //    }
        //}

        public decimal  Existencia {
            get {
                return Model.Existencia;
            }
            set {
                if (Model.Existencia != value) {
                    Model.Existencia = value;
                    IsDirty = true;
                    RaisePropertyChanged(ExistenciaPropertyName);
                }
            }
        }

        //public decimal  CantidadMinima {
        //    get {
        //        return Model.CantidadMinima;
        //    }
        //    set {
        //        if (Model.CantidadMinima != value) {
        //            Model.CantidadMinima = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CantidadMinimaPropertyName);
        //        }
        //    }
        //}

        //public decimal  CantidadMaxima {
        //    get {
        //        return Model.CantidadMaxima;
        //    }
        //    set {
        //        if (Model.CantidadMaxima != value) {
        //            Model.CantidadMaxima = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CantidadMaximaPropertyName);
        //        }
        //    }
        //}

        //public string  EstadisticasdeProducto {
        //    get {
        //        return Model.EstadisticasdeProducto;
        //    }
        //    set {
        //        if (Model.EstadisticasdeProducto != value) {
        //            Model.EstadisticasdeProducto = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(EstadisticasdeProductoPropertyName);
        //        }
        //    }
        //}

        [LibGridColum("Categoria", eGridColumType.Connection, ConnectionDisplayMemberPath = "Descripcion", ConnectionModelPropertyName = "Categoria", ConnectionSearchCommandName = "ChooseCategoriaCommand", MaxWidth=120)]
        public string  Categoria {
            get {
                return Model.Categoria;
            }
            set {
                if (Model.Categoria != value) {
                    Model.Categoria = value;
                    IsDirty = true;
                    RaisePropertyChanged(CategoriaPropertyName);
                    if (LibString.IsNullOrEmpty(Categoria, true)) {
                        ConexionCategoria = null;
                    }
                }
            }
        }

        //public eTipoDeProducto  TipoDeProducto {
        //    get {
        //        return Model.TipoDeProductoAsEnum;
        //    }
        //    set {
        //        if (Model.TipoDeProductoAsEnum != value) {
        //            Model.TipoDeProductoAsEnum = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(TipoDeProductoPropertyName);
        //        }
        //    }
        //}

        //public string  NombrePrograma {
        //    get {
        //        return Model.NombrePrograma;
        //    }
        //    set {
        //        if (Model.NombrePrograma != value) {
        //            Model.NombrePrograma = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(NombreProgramaPropertyName);
        //        }
        //    }
        //}

        //public string  OtrosDatos {
        //    get {
        //        return Model.OtrosDatos;
        //    }
        //    set {
        //        if (Model.OtrosDatos != value) {
        //            Model.OtrosDatos = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(OtrosDatosPropertyName);
        //        }
        //    }
        //}

        public string  Marca {
            get {
                return Model.Marca;
            }
            set {
                if (Model.Marca != value) {
                    Model.Marca = value;
                    IsDirty = true;
                    RaisePropertyChanged(MarcaPropertyName);
                }
            }
        }

        [LibCustomValidation("FechaDeVencimientoValidating")]
        public DateTime  FechaDeVencimiento {
            get {
                return Model.FechaDeVencimiento;
            }
            set {
                if (Model.FechaDeVencimiento != value) {
                    Model.FechaDeVencimiento = value;
                    IsDirty = true;
                    RaisePropertyChanged(FechaDeVencimientoPropertyName);
                }
            }
        }

        public string  UnidadDeVenta {
            get {
                return Model.UnidadDeVenta;
            }
            set {
                if (Model.UnidadDeVenta != value) {
                    Model.UnidadDeVenta = value;
                    IsDirty = true;
                    RaisePropertyChanged(UnidadDeVentaPropertyName);
                    if (LibString.IsNullOrEmpty(UnidadDeVenta, true)) {
                        //ConexionUnidadDeVenta = null;
                    }
                }
            }
        }

        //public string  CampoDefinible1 {
        //    get {
        //        return Model.CampoDefinible1;
        //    }
        //    set {
        //        if (Model.CampoDefinible1 != value) {
        //            Model.CampoDefinible1 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CampoDefinible1PropertyName);
        //        }
        //    }
        //}

        //public string  CampoDefinible2 {
        //    get {
        //        return Model.CampoDefinible2;
        //    }
        //    set {
        //        if (Model.CampoDefinible2 != value) {
        //            Model.CampoDefinible2 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CampoDefinible2PropertyName);
        //        }
        //    }
        //}

        //public string  CampoDefinible3 {
        //    get {
        //        return Model.CampoDefinible3;
        //    }
        //    set {
        //        if (Model.CampoDefinible3 != value) {
        //            Model.CampoDefinible3 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CampoDefinible3PropertyName);
        //        }
        //    }
        //}

        //public string  CampoDefinible4 {
        //    get {
        //        return Model.CampoDefinible4;
        //    }
        //    set {
        //        if (Model.CampoDefinible4 != value) {
        //            Model.CampoDefinible4 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CampoDefinible4PropertyName);
        //        }
        //    }
        //}

        //public string  CampoDefinible5 {
        //    get {
        //        return Model.CampoDefinible5;
        //    }
        //    set {
        //        if (Model.CampoDefinible5 != value) {
        //            Model.CampoDefinible5 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CampoDefinible5PropertyName);
        //        }
        //    }
        //}

        //public decimal  MeCostoUnitario {
        //    get {
        //        return Model.MeCostoUnitario;
        //    }
        //    set {
        //        if (Model.MeCostoUnitario != value) {
        //            Model.MeCostoUnitario = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(MeCostoUnitarioPropertyName);
        //        }
        //    }
        //}

        //public decimal  UnidadDeVentaSecundaria {
        //    get {
        //        return Model.UnidadDeVentaSecundaria;
        //    }
        //    set {
        //        if (Model.UnidadDeVentaSecundaria != value) {
        //            Model.UnidadDeVentaSecundaria = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(UnidadDeVentaSecundariaPropertyName);
        //        }
        //    }
        //}

        //public string  CodigoLote {
        //    get {
        //        return Model.CodigoLote;
        //    }
        //    set {
        //        if (Model.CodigoLote != value) {
        //            Model.CodigoLote = value;
        //        }
        //    }
        //}

        //public decimal  PorcentajeComision {
        //    get {
        //        return Model.PorcentajeComision;
        //    }
        //    set {
        //        if (Model.PorcentajeComision != value) {
        //            Model.PorcentajeComision = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PorcentajeComisionPropertyName);
        //        }
        //    }
        //}

        //public bool  ExcluirDeComision {
        //    get {
        //        return Model.ExcluirDeComisionAsBool;
        //    }
        //    set {
        //        if (Model.ExcluirDeComisionAsBool != value) {
        //            Model.ExcluirDeComisionAsBool = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(ExcluirDeComisionPropertyName);
        //        }
        //    }
        //}

        //public decimal  CantArtReservado {
        //    get {
        //        return Model.CantArtReservado;
        //    }
        //    set {
        //        if (Model.CantArtReservado != value) {
        //            Model.CantArtReservado = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CantArtReservadoPropertyName);
        //        }
        //    }
        //}

        //public string  CodigoGrupo {
        //    get {
        //        return Model.CodigoGrupo;
        //    }
        //    set {
        //        if (Model.CodigoGrupo != value) {
        //            Model.CodigoGrupo = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(CodigoGrupoPropertyName);
        //        }
        //    }
        //}

        //public bool  RetornaAAlmacen {
        //    get {
        //        return Model.RetornaAAlmacenAsBool;
        //    }
        //    set {
        //        if (Model.RetornaAAlmacenAsBool != value) {
        //            Model.RetornaAAlmacenAsBool = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(RetornaAAlmacenPropertyName);
        //        }
        //    }
        //}

        //public bool  PedirDimension {
        //    get {
        //        return Model.PedirDimensionAsBool;
        //    }
        //    set {
        //        if (Model.PedirDimensionAsBool != value) {
        //            Model.PedirDimensionAsBool = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(PedirDimensionPropertyName);
        //        }
        //    }
        //}

        //public decimal  MargenGanancia {
        //    get {
        //        return Model.MargenGanancia;
        //    }
        //    set {
        //        if (Model.MargenGanancia != value) {
        //            Model.MargenGanancia = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(MargenGananciaPropertyName);
        //        }
        //    }
        //}

        //public decimal  MargenGanancia2 {
        //    get {
        //        return Model.MargenGanancia2;
        //    }
        //    set {
        //        if (Model.MargenGanancia2 != value) {
        //            Model.MargenGanancia2 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(MargenGanancia2PropertyName);
        //        }
        //    }
        //}

        //public decimal  MargenGanancia3 {
        //    get {
        //        return Model.MargenGanancia3;
        //    }
        //    set {
        //        if (Model.MargenGanancia3 != value) {
        //            Model.MargenGanancia3 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(MargenGanancia3PropertyName);
        //        }
        //    }
        //}

        //public decimal  MargenGanancia4 {
        //    get {
        //        return Model.MargenGanancia4;
        //    }
        //    set {
        //        if (Model.MargenGanancia4 != value) {
        //            Model.MargenGanancia4 = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(MargenGanancia4PropertyName);
        //        }
        //    }
        //}

        //[LibRequired(ErrorMessage = "El campo Tipo de Artículo es requerido.")]
        //public eTipoArticuloInv  TipoArticuloInv {
        //    get {
        //        return Model.TipoArticuloInvAsEnum;
        //    }
        //    set {
        //        if (Model.TipoArticuloInvAsEnum != value) {
        //            Model.TipoArticuloInvAsEnum = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(TipoArticuloInvPropertyName);
        //        }
        //    }
        //}

        //public bool  ComisionaPorcentaje {
        //    get {
        //        return Model.ComisionaPorcentajeAsBool;
        //    }
        //    set {
        //        if (Model.ComisionaPorcentajeAsBool != value) {
        //            Model.ComisionaPorcentajeAsBool = value;
        //            IsDirty = true;
        //            RaisePropertyChanged(ComisionaPorcentajePropertyName);
        //        }
        //    }
        //}

        [LibGridColum("Usa Balanza")]
        public bool UsaBalanza {
            get {
                return Model.UsaBalanzaAsBool;
            }
            set {
                if (Model.UsaBalanzaAsBool != value) {
                    Model.UsaBalanzaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaBalanzaPropertyName);
                }
            }
        }

        public string  NombreOperador {
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

        public DateTime  FechaUltimaModificacion {
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

        public eStatusArticulo[] ArrayStatusArticulo {
            get {
                return LibEnumHelper<eStatusArticulo>.GetValuesInArray();
            }
        }

        public eTipoDeArticulo[] ArrayTipoDeArticulo {
            get {
                return LibEnumHelper<eTipoDeArticulo>.GetValuesInArray();
            }
        }

        public eTipoDeAlicuota[] ArrayTipoDeAlicuota {
            get {
                return LibEnumHelper<eTipoDeAlicuota>.GetValuesInArray();
            }
        }

        public eTipoDeProducto[] ArrayTipoDeProducto {
            get {
                return LibEnumHelper<eTipoDeProducto>.GetValuesInArray();
            }
        }

        public eTipoArticuloInv[] ArrayTipoArticuloInv {
            get {
                return LibEnumHelper<eTipoArticuloInv>.GetValuesInArray();
            }
        }

        //[LibDetailRequired(ErrorMessage = "Producto Compuesto es requerido.")]
        //public ProductoCompuestoMngViewModel DetailProductoCompuesto {
        //    get;
        //    set;
        //}

        //[LibDetailRequired(ErrorMessage = "Existencia Por Grupo es requerido.")]
        //public ExistenciaPorGrupoMngViewModel DetailExistenciaPorGrupo {
        //    get;
        //    set;
        //}

        //[LibDetailRequired(ErrorMessage = "Codigo De Barras es requerido.")]
        //public CodigoDeBarrasMngViewModel DetailCodigoDeBarras {
        //    get;
        //    set;
        //}

        public FkLineaDeProductoViewModel ConexionLineaDeProducto {
            get {
                return _ConexionLineaDeProducto;
            }
            set {
                if (_ConexionLineaDeProducto != value) {
                    _ConexionLineaDeProducto = value;
                    RaisePropertyChanged(LineaDeProductoPropertyName);
                }
                if (_ConexionLineaDeProducto == null) {
                    LineaDeProducto = string.Empty;
                }
            }
        }

        public FkCategoriaViewModel ConexionCategoria {
            get {
                return _ConexionCategoria;
            }
            set {
                if (_ConexionCategoria != value) {
                    _ConexionCategoria = value;
                    RaisePropertyChanged(CategoriaPropertyName);
                }
                if (_ConexionCategoria == null) {
                    Categoria = string.Empty;
                }
            }
        }

        //public FkUnidadDeVentaViewModel ConexionUnidadDeVenta {
        //    get {
        //        return _ConexionUnidadDeVenta;
        //    }
        //    set {
        //        if (_ConexionUnidadDeVenta != value) {
        //            _ConexionUnidadDeVenta = value;
        //            RaisePropertyChanged(UnidadDeVentaPropertyName);
        //        }
        //        if (_ConexionUnidadDeVenta == null) {
        //            UnidadDeVenta = string.Empty;
        //        }
        //    }
        //}

        public RelayCommand<string> ChooseLineaDeProductoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCategoriaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseUnidadDeVentaCommand {
            get;
            private set;
        }

        //public RelayCommand<string> CreateProductoCompuestoCommand {
        //    get { return DetailProductoCompuesto.CreateCommand; }
        //}

        //public RelayCommand<string> UpdateProductoCompuestoCommand {
        //    get { return DetailProductoCompuesto.UpdateCommand; }
        //}

        //public RelayCommand<string> DeleteProductoCompuestoCommand {
        //    get { return DetailProductoCompuesto.DeleteCommand; }
        //}

        //public RelayCommand<string> CreateExistenciaPorGrupoCommand {
        //    get { return DetailExistenciaPorGrupo.CreateCommand; }
        //}

        //public RelayCommand<string> UpdateExistenciaPorGrupoCommand {
        //    get { return DetailExistenciaPorGrupo.UpdateCommand; }
        //}

        //public RelayCommand<string> DeleteExistenciaPorGrupoCommand {
        //    get { return DetailExistenciaPorGrupo.DeleteCommand; }
        //}

        //public RelayCommand<string> CreateCodigoDeBarrasCommand {
        //    get { return DetailCodigoDeBarras.CreateCommand; }
        //}

        //public RelayCommand<string> UpdateCodigoDeBarrasCommand {
        //    get { return DetailCodigoDeBarras.UpdateCommand; }
        //}

        //public RelayCommand<string> DeleteCodigoDeBarrasCommand {
        //    get { return DetailCodigoDeBarras.DeleteCommand; }
        //}
        #endregion //Propiedades
        #region Constructores
        public ArticuloInventarioViewModel()
            : this(new ArticuloInventario(), eAccionSR.Insertar) {
        }
        public ArticuloInventarioViewModel(ArticuloInventario initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = CodigoPropertyName;
            Model.ConsecutivoCompania = Mfc.GetInt("Compania");
            InitializeDetails();
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ArticuloInventario valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ArticuloInventario FindCurrentRecord(ArticuloInventario valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("Codigo", valModel.Codigo, 15);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ArticuloInventarioGET", vParams.Get(), UseDetail).FirstOrDefault();
        }

        protected override ILibBusinessMasterComponentWithSearch<IList<ArticuloInventario>, IList<ArticuloInventario>> GetBusinessComponent() {
            return new clsArticuloInventarioNav();
        }

        protected override void InitializeDetails() {
            //DetailProductoCompuesto = new ProductoCompuestoMngViewModel(this, Model.DetailProductoCompuesto, Action);
            //DetailProductoCompuesto.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<ProductoCompuestoViewModel>>(DetailProductoCompuesto_OnCreated);
            //DetailProductoCompuesto.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<ProductoCompuestoViewModel>>(DetailProductoCompuesto_OnUpdated);
            //DetailProductoCompuesto.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<ProductoCompuestoViewModel>>(DetailProductoCompuesto_OnDeleted);
            //DetailProductoCompuesto.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<ProductoCompuestoViewModel>>(DetailProductoCompuesto_OnSelectedItemChanged);
            //DetailExistenciaPorGrupo = new ExistenciaPorGrupoMngViewModel(this, Model.DetailExistenciaPorGrupo, Action);
            //DetailExistenciaPorGrupo.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel>>(DetailExistenciaPorGrupo_OnCreated);
            //DetailExistenciaPorGrupo.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel>>(DetailExistenciaPorGrupo_OnUpdated);
            //DetailExistenciaPorGrupo.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel>>(DetailExistenciaPorGrupo_OnDeleted);
            //DetailExistenciaPorGrupo.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel>>(DetailExistenciaPorGrupo_OnSelectedItemChanged);
            //DetailCodigoDeBarras = new CodigoDeBarrasMngViewModel(this, Model.DetailCodigoDeBarras, Action);
            //DetailCodigoDeBarras.OnCreated += new EventHandler<SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel>>(DetailCodigoDeBarras_OnCreated);
            //DetailCodigoDeBarras.OnUpdated += new EventHandler<SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel>>(DetailCodigoDeBarras_OnUpdated);
            //DetailCodigoDeBarras.OnDeleted += new EventHandler<SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel>>(DetailCodigoDeBarras_OnDeleted);
            //DetailCodigoDeBarras.OnSelectedItemChanged += new EventHandler<SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel>>(DetailCodigoDeBarras_OnSelectedItemChanged);
        }
        //#region ProductoCompuesto

        //private void DetailProductoCompuesto_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<ProductoCompuestoViewModel> e) {
        //    try {
        //        UpdateProductoCompuestoCommand.RaiseCanExecuteChanged();
        //        DeleteProductoCompuestoCommand.RaiseCanExecuteChanged();
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailProductoCompuesto_OnDeleted(object sender, SearchCollectionChangedEventArgs<ProductoCompuestoViewModel> e) {
        //    try {
        //        IsDirty = true;
        //        Model.DetailProductoCompuesto.Remove(e.ViewModel.GetModel());
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailProductoCompuesto_OnUpdated(object sender, SearchCollectionChangedEventArgs<ProductoCompuestoViewModel> e) {
        //    try {
        //        IsDirty = e.ViewModel.IsDirty;
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailProductoCompuesto_OnCreated(object sender, SearchCollectionChangedEventArgs<ProductoCompuestoViewModel> e) {
        //    try {
        //        Model.DetailProductoCompuesto.Add(e.ViewModel.GetModel());
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}
        //#endregion //ProductoCompuesto
        //#region ExistenciaPorGrupo

        //private void DetailExistenciaPorGrupo_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel> e) {
        //    try {
        //        UpdateExistenciaPorGrupoCommand.RaiseCanExecuteChanged();
        //        DeleteExistenciaPorGrupoCommand.RaiseCanExecuteChanged();
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailExistenciaPorGrupo_OnDeleted(object sender, SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel> e) {
        //    try {
        //        IsDirty = true;
        //        Model.DetailExistenciaPorGrupo.Remove(e.ViewModel.GetModel());
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailExistenciaPorGrupo_OnUpdated(object sender, SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel> e) {
        //    try {
        //        IsDirty = e.ViewModel.IsDirty;
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailExistenciaPorGrupo_OnCreated(object sender, SearchCollectionChangedEventArgs<ExistenciaPorGrupoViewModel> e) {
        //    try {
        //        Model.DetailExistenciaPorGrupo.Add(e.ViewModel.GetModel());
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}
        //#endregion //ExistenciaPorGrupo
        //#region CodigoDeBarras

        //private void DetailCodigoDeBarras_OnSelectedItemChanged(object sender, SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel> e) {
        //    try {
        //        UpdateCodigoDeBarrasCommand.RaiseCanExecuteChanged();
        //        DeleteCodigoDeBarrasCommand.RaiseCanExecuteChanged();
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailCodigoDeBarras_OnDeleted(object sender, SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel> e) {
        //    try {
        //        IsDirty = true;
        //        Model.DetailCodigoDeBarras.Remove(e.ViewModel.GetModel());
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailCodigoDeBarras_OnUpdated(object sender, SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel> e) {
        //    try {
        //        IsDirty = e.ViewModel.IsDirty;
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}

        //private void DetailCodigoDeBarras_OnCreated(object sender, SearchCollectionChangedEventArgs<CodigoDeBarrasViewModel> e) {
        //    try {
        //        Model.DetailCodigoDeBarras.Add(e.ViewModel.GetModel());
        //    } catch (System.AccessViolationException) {
        //        throw;
        //    } catch (System.Exception vEx) {
        //        LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
        //    }
        //}
        //#endregion //CodigoDeBarras

        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseLineaDeProductoCommand = new RelayCommand<string>(ExecuteChooseLineaDeProductoCommand);
            ChooseCategoriaCommand = new RelayCommand<string>(ExecuteChooseCategoriaCommand);
            ChooseUnidadDeVentaCommand = new RelayCommand<string>(ExecuteChooseUnidadDeVentaCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionLineaDeProducto = FirstConnectionRecordOrDefault<FkLineaDeProductoViewModel>("Linea De Producto", LibSearchCriteria.CreateCriteria("Nombre", LineaDeProducto));
            ConexionCategoria = FirstConnectionRecordOrDefault<FkCategoriaViewModel>("Categoria", LibSearchCriteria.CreateCriteria("Descripcion", Categoria));
            //ConexionUnidadDeVenta = FirstConnectionRecordOrDefault<FkUnidadDeVentaViewModel>("Unidad De Venta", LibSearchCriteria.CreateCriteria("Nombre", UnidadDeVenta));
        }

        private void ExecuteChooseLineaDeProductoCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionLineaDeProducto = ChooseRecord<FkLineaDeProductoViewModel>("Línea de Producto", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionLineaDeProducto != null) {
                    LineaDeProducto = ConexionLineaDeProducto.Nombre;
                } else {
                    LineaDeProducto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCategoriaCommand(string valDescripcion) {
            try {
                if (valDescripcion == null) {
                    valDescripcion = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Descripcion", valDescripcion);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionCategoria = ChooseRecord<FkCategoriaViewModel>("Categoria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCategoria != null) {
                    Categoria = ConexionCategoria.Descripcion;
                } else {
                    Categoria = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseUnidadDeVentaCommand(string valNombre) {
            try {
                if (valNombre == null) {
                    valNombre = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Nombre", valNombre);
                LibSearchCriteria vFixedCriteria = null;
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
                vFixedCriteria = LibSearchCriteria.CreateCriteria("NombreCampoEnLaTablaConLaQueSeConecta", valorAUsarComoFiltroFijo);
        */
        #endregion //Codigo Ejemplo
                //ConexionUnidadDeVenta = ChooseRecord<FkUnidadDeVentaViewModel>("Unidad De Venta", vDefaultCriteria, vFixedCriteria, string.Empty);
                //if (ConexionUnidadDeVenta != null) {
                //    UnidadDeVenta = ConexionUnidadDeVenta.Nombre;
                //} else {
                //    UnidadDeVenta = string.Empty;
                //}
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private ValidationResult FechaDeVencimientoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else {
                if (LibDefGen.DateIsGreaterThanDateLimitForEnterData(FechaDeVencimiento, false, Action)) {
                    vResult = new ValidationResult(LibDefGen.TooltipMessageDateRestrictionDemoProgram("Fecha De Vencimiento"));
                }
            }
            return vResult;
        }
        #endregion //Metodos Generados


    } //End of class ArticuloInventarioViewModel

} //End of namespace Galac.Saw.Uil.Inventario

