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
using Galac.Saw.Brl.Vehiculo;
using Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo.ViewModel {
    public class MarcaViewModel : LibInputViewModel<Marca> {
        #region Constantes
        public const string NombrePropertyName = "Nombre";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Marca"; }
        }

        [LibRequired(ErrorMessage = "El campo Nombre es requerido.")]
        [LibGridColum("Nombre", Width = 300)]
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
        #endregion //Propiedades
        #region Constructores
        public MarcaViewModel()
            : this(new Marca(), eAccionSR.Insertar) {
        }
        public MarcaViewModel(Marca initModel, eAccionSR initAction)
            : base(initModel, initAction) {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(Marca valModel) {
            base.InitializeLookAndFeel(valModel);            
        }

        protected override Marca FindCurrentRecord(Marca valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInString("Nombre", valModel.Nombre, 20);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "MarcaGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<Marca>, IList<Marca>> GetBusinessComponent() {
            return new clsMarcaNav();
        }
        #endregion //Metodos Generados


    } //End of class MarcaViewModel

} //End of namespace Galac.Saw.Uil.Vehiculo

