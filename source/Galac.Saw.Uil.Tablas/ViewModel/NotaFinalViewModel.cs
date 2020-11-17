using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Saw.Brl.Tablas;
using Galac.Saw.Ccl.Tablas;
using Galac.Saw.Uil.Tablas.ViewModel;


namespace Galac.Saw.Uil.Tablas.ViewModel {
    public class NotaFinalViewModel : LibInputViewModelMfc<NotaFinal> {
        #region Constantes
        public const string CodigoDeLaNotaPropertyName = "CodigoDeLaNota";
        public const string DescripcionPropertyName = "Descripcion";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Nota Final"; }
        }

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código")]
        public string  CodigoDeLaNota {
            get {
                return Model.CodigoDeLaNota;
            }
            set {
                if (Model.CodigoDeLaNota != value) {
                    Model.CodigoDeLaNota = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoDeLaNotaPropertyName);
                }
            }
        }
        
        [LibRequired(ErrorMessage = "El campo Descripción es requerido.")]
        [LibGridColum("Descripción", Width=800)]
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

        public bool IsEnabledCodigo {
            get {
                return Action == eAccionSR.Insertar;
            }
        }

        #endregion //Propiedades
        #region Constructores
        public NotaFinalViewModel()
            : this(new NotaFinal(), eAccionSR.Insertar) {
        }
        public NotaFinalViewModel(NotaFinal initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
                Model.ConsecutivoCompania = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(NotaFinal valModel) {
            Model.ConsecutivoCompania =  LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania"); 
            base.InitializeLookAndFeel(valModel);
        }

        protected override NotaFinal FindCurrentRecord(NotaFinal valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valModel.ConsecutivoCompania);
            vParams.AddInString("CodigoDeLaNota", valModel.CodigoDeLaNota, 10);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "NotaFinalGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<NotaFinal>, IList<NotaFinal>> GetBusinessComponent() {
            return new clsNotaFinalNav();
        }
       
        #endregion //Metodos Generados


    } //End of class NotaFinalViewModel

} //End of namespace Galac.Saw.Uil.Tablas

