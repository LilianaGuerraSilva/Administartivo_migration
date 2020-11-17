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
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas.ViewModel {
    public class UnidadDeVentaViewModel : LibInputViewModel<UnidadDeVenta> {
        #region Constantes
        public const string NombrePropertyName = "Nombre";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Unidad De Venta"; }
        }

        [LibRequired(ErrorMessage = "El campo Unidad de Venta es requerido.")]
        [LibGridColum("Unidad de Venta")]
        public string  Nombre {
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
        #endregion //Propiedades
        #region Constructores
        public UnidadDeVentaViewModel()
            : this(new UnidadDeVenta(), eAccionSR.Insertar) {
        }
        public UnidadDeVentaViewModel(UnidadDeVenta initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = NombrePropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(UnidadDeVenta valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override UnidadDeVenta FindCurrentRecord(UnidadDeVenta valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valModel.Nombre, 20);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "UnidadDeVentaGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<UnidadDeVenta>, IList<UnidadDeVenta>> GetBusinessComponent() {
            return new clsUnidadDeVentaNav();
        }
        #endregion //Metodos Generados


    } //End of class UnidadDeVentaViewModel

} //End of namespace Galac.Saw.Uil.Tablas

