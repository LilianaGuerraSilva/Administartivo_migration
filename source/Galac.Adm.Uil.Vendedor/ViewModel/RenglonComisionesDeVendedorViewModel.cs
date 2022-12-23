using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Brl.Vendedor;
using Galac.Adm.Ccl.Vendedor;

namespace Galac.Adm.Uil.Vendedor.ViewModel {
    public class RenglonComisionesDeVendedorViewModel : LibInputDetailViewModelMfc<RenglonComisionesDeVendedor> {
        #region Constantes
        public const string ConsecutivoVendedorPropertyName = "ConsecutivoVendedor";
        public const string ConsecutivoRenglonPropertyName = "ConsecutivoRenglon";
        public const string NombreDeLineaDeProductoPropertyName = "NombreDeLineaDeProducto";
        public const string TipoDeComisionPropertyName = "TipoDeComision";
        public const string MontoPropertyName = "Monto";
        public const string PorcentajePropertyName = "Porcentaje";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Renglon Comisiones De Vendedor"; }
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

        [LibRequired(ErrorMessage = "El campo Consecutivo Vendedor es requerido.")]
        public int  ConsecutivoVendedor {
            get {
                return Model.ConsecutivoVendedor;
            }
            set {
                if (Model.ConsecutivoVendedor != value) {
                    Model.ConsecutivoVendedor = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoVendedorPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Consecutivo Renglon es requerido.")]
        public int  ConsecutivoRenglon {
            get {
                return Model.ConsecutivoRenglon;
            }
            set {
                if (Model.ConsecutivoRenglon != value) {
                    Model.ConsecutivoRenglon = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConsecutivoRenglonPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Nombre De Linea De Producto es requerido.")]
        [LibGridColum("Nombre De Linea De Producto", MaxLength=20)]
        public string  NombreDeLineaDeProducto {
            get {
                return Model.NombreDeLineaDeProducto;
            }
            set {
                if (Model.NombreDeLineaDeProducto != value) {
                    Model.NombreDeLineaDeProducto = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreDeLineaDeProductoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Tipo De Comision es requerido.")]
        [LibGridColum("Tipo De Comision", eGridColumType.Enum, PrintingMemberPath = "TipoDeComisionStr")]
        public eTipoComision  TipoDeComision {
            get {
                return Model.TipoDeComisionAsEnum;
            }
            set {
                if (Model.TipoDeComisionAsEnum != value) {
                    Model.TipoDeComisionAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeComisionPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Monto es requerido.")]
        [LibGridColum("Monto", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  Monto {
            get {
                return Model.Monto;
            }
            set {
                if (Model.Monto != value) {
                    Model.Monto = value;
                    IsDirty = true;
                    RaisePropertyChanged(MontoPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Porcentaje es requerido.")]
        [LibGridColum("Porcentaje", eGridColumType.Numeric, Alignment = eTextAlignment.Right)]
        public decimal  Porcentaje {
            get {
                return Model.Porcentaje;
            }
            set {
                if (Model.Porcentaje != value) {
                    Model.Porcentaje = value;
                    IsDirty = true;
                    RaisePropertyChanged(PorcentajePropertyName);
                }
            }
        }

        public eTipoComision[] ArrayTipoComision {
            get {
                return LibEnumHelper<eTipoComision>.GetValuesInArray();
            }
        }

        public VendedorViewModel Master {
            get;
            set;
        }
        #endregion //Propiedades
        #region Constructores
        public RenglonComisionesDeVendedorViewModel()
            : base(new RenglonComisionesDeVendedor(), eAccionSR.Insertar, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
        }
        public RenglonComisionesDeVendedorViewModel(VendedorViewModel initMaster, RenglonComisionesDeVendedor initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            Master = initMaster;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(RenglonComisionesDeVendedor valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ILibBusinessDetailComponent<IList<RenglonComisionesDeVendedor>, IList<RenglonComisionesDeVendedor>> GetBusinessComponent() {
            return new clsRenglonComisionesDeVendedorNav();
        }
        #endregion //Metodos Generados


    } //End of class RenglonComisionesDeVendedorViewModel

} //End of namespace Galac..Uil.ComponenteNoEspecificado

