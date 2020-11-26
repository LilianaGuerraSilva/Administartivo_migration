using System;
using System.Collections.Generic;
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
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Saw.Ccl.Inventario;
using Galac.Adm.Uil.GestionCompras.ViewModel;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Adm.Uil.GestionCompras.Reportes {
    public class clsImpresionDeEtiquetasPorComprasViewModel : LibInputRptViewModelBase<Compra> {
        #region Constantes
        public const string NivelDePrecioPropertyName = "NivelDePrecio";
        public const string NumeroCompraPropertyName = "NumeroCompra";
        const string MostrarProveedorPropertyName = "MostrarProveedor";
        #endregion
        #region Variables
        eNivelDePrecio  _NivelDePrecio;
        string _NumeroCompra;
        bool _MostrarProveedor;
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get { return "Impresión de Etiquetas por Compra";}
        }

        public override bool IsSSRS {
            get {
                return false;
            }
        }

        public eNivelDePrecio  NivelDePrecio {
            get {
                return _NivelDePrecio;
            }
            set {
                if (_NivelDePrecio != value) {
                    _NivelDePrecio = value;
                    RaisePropertyChanged(NivelDePrecioPropertyName);
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Codigo Producto es requerido.")]
        [LibGridColum("Producto", eGridColumType.Connection, ConnectionDisplayMemberPath = "Numero", ConnectionModelPropertyName = "CodigoProducto", ConnectionSearchCommandName = "ChooseCodigoProductoCommand", MaxWidth = 120)]
        public string NumeroCompra {
            get {
                return _NumeroCompra;
            }
            set {
                if (_NumeroCompra != value) {
                    _NumeroCompra = value;
                    RaisePropertyChanged(NumeroCompraPropertyName);
                }
            }
        }

        public eNivelDePrecio[] ArrayNivelDePrecio {
            get {
                return LibEnumHelper<eNivelDePrecio>.GetValuesInArray().Where(p => p != eNivelDePrecio.Todos).ToArray<eNivelDePrecio>();
            }
        }

        public bool PrecioSinIva {
            get;
            set;
        }

        public bool MostrarProveedor {
            get {
                return _MostrarProveedor;
            }
            set {
                if (_MostrarProveedor != value) {
                    _MostrarProveedor = value;
                    RaisePropertyChanged(MostrarProveedorPropertyName);
                }
            }
        }

        public LibXmlMemInfo AppMemoryInfo { get; set; }

        public LibXmlMFC Mfc { get; set; }

        public RelayCommand<string> ChooseNumeroCompraCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores
        public clsImpresionDeEtiquetasPorComprasViewModel() {

        }
        #endregion //Constructores
        #region Metodos Generados


        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsCompraNav();
        }


        private void ExecuteChooseNumeroCompra(string valCodigo) {
            if (valCodigo == null) {
                valCodigo = string.Empty;
            }
            LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_Compra_B1.ConsecutivoCompania", Mfc.GetInt("Compania"));
            FkCompraViewModel ConexionNumeroCompra = ChooseRecord<FkCompraViewModel>("Importación", vFixedCriteria, null, string.Empty);
            if (ConexionNumeroCompra != null) {
                //NumeroCompra = ConexionNumeroCompra.Serie + "-" + ConexionNumeroCompra.Numero;
                NumeroCompra = ConexionNumeroCompra.Numero;
            } else {
                NumeroCompra = string.Empty;
            }
        }
        
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNumeroCompraCommand = new RelayCommand<string>(ExecuteChooseNumeroCompra);
        }
        #endregion //Metodos Generados


    } //End of class ImpresionDeEtiquetasPorComprasViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

