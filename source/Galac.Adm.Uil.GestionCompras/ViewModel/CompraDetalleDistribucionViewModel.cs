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


namespace Galac.Adm.Uil.GestionCompras.ViewModel {
    public class CompraDetalleDistribucionViewModel : LibGenericViewModel  {
        #region Constantes
        public const string CompraPropertyName = "Compra";        
        private CompraViewModel _Compra;
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Detalle Distribución"; }
        }

        public CompraViewModel Compra {
            get {
                return _Compra;
            }
            set {
                if (_Compra != value) {
                    _Compra = value;
                    RaisePropertyChanged(CompraPropertyName);
                }
            }
        }

        #endregion //Propiedades
        #region Constructores
        public CompraDetalleDistribucionViewModel(CompraViewModel initCompra) 
            : base() {
            Compra = initCompra;
            }

        #endregion //Constructores
        #region Metodos Generados

       

        #endregion //Metodos Generados


    } //End of class CompraDetalleDistribucionViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras.ViewModel

