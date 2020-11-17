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
using Galac.Adm.Brl.Banco;
using Galac.Adm.Ccl.Banco;

namespace Galac.Adm.Uil.Banco.ViewModel {
    public class ConceptoBancarioViewModel : LibInputViewModel<ConceptoBancario> {
        #region Constantes
        public const string CodigoPropertyName = "Codigo";
        public const string DescripcionPropertyName = "Descripcion";
        public const string TipoPropertyName = "Tipo";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Concepto Bancario"; }
        }

        public int  Consecutivo {
            get {
                return Model.Consecutivo;
            }
            set {
                if (Model.Consecutivo != value) {
                    Model.Consecutivo = value;
                }
            }
        }

        [LibRequired(ErrorMessage = "El campo Código es requerido.")]
        [LibGridColum("Código")]
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
        [LibGridColum("Descripción", Width = 300)]
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

        [LibGridColum("Tipo", eGridColumType.Enum, PrintingMemberPath = "TipoStr")]
        public eIngresoEgreso  Tipo {
            get {
                return Model.TipoAsEnum;
            }
            set {
                if (Model.TipoAsEnum != value) {
                    Model.TipoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoPropertyName);
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

        public eIngresoEgreso[] ArrayIngresoEgreso {
            get {
                return LibEnumHelper<eIngresoEgreso>.GetValuesInArray();
            }
        }

        public bool IsEnabledField
        {
            get
            {
                return Action == eAccionSR.Insertar;
            }
        }



        #endregion //Propiedades
        #region Constructores
        public ConceptoBancarioViewModel()
            : this(new ConceptoBancario(), eAccionSR.Insertar) {
        }
        public ConceptoBancarioViewModel(ConceptoBancario initModel, eAccionSR initAction)
            : base(initModel, initAction) {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(ConceptoBancario valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override ConceptoBancario FindCurrentRecord(ConceptoBancario valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("Consecutivo", valModel.Consecutivo);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "ConceptoBancarioGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<ConceptoBancario>, IList<ConceptoBancario>> GetBusinessComponent() {
            return new clsConceptoBancarioNav();
        }
        #endregion //Metodos Generados


    } //End of class ConceptoBancarioViewModel

} //End of namespace Galac.Adm.Uil.Banco

