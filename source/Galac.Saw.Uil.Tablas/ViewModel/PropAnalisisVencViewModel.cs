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
    public class PropAnalisisVencViewModel : LibInputViewModel<PropAnalisisVenc> {
        #region Constantes
        public const string PrimerVencimientoPropertyName = "PrimerVencimiento";
        public const string SegundoVencimientoPropertyName = "SegundoVencimiento";
        public const string TercerVencimientoPropertyName = "TercerVencimiento";
        public const string NombreOperadorPropertyName = "NombreOperador";
        public const string FechaUltimaModificacionPropertyName = "FechaUltimaModificacion";
        #endregion
        #region Propiedades

        public override string ModuleName {
            get { return "Prop Analisis Venc"; }
        }

        public int  SecuencialUnique0 {
            get {
                return Model.SecuencialUnique0;
            }
            set {
                if (Model.SecuencialUnique0 != value) {
                    Model.SecuencialUnique0 = value;
                }
            }
        }

        [LibGridColum("Primer Vencimiento",eGridColumType.Integer,Width = 130)]
        [LibCustomValidation("PrimerVencimientoValidate")]
        public int  PrimerVencimiento {
            get {
                return Model.PrimerVencimiento;
            }
            set {
                if (Model.PrimerVencimiento != value) {
                    Model.PrimerVencimiento = value;
                    IsDirty = true;
                    RaisePropertyChanged(PrimerVencimientoPropertyName);
                }
            }
        }
        
        [LibGridColum("Segundo Vencimiento", eGridColumType.Integer, Width = 135)]
        [LibCustomValidation("SegundoVencimientoValidate")]
        public int  SegundoVencimiento {
            get {
                return Model.SegundoVencimiento;
            }
            set {
                if (Model.SegundoVencimiento != value) {
                        Model.SegundoVencimiento = value;
                        IsDirty = true;
                        RaisePropertyChanged(SegundoVencimientoPropertyName);
                }
            }
        }

        [LibGridColum("Tercer Vencimiento", eGridColumType.Integer)]
        [LibCustomValidation("TercerVencimientoValidate")]
        public int  TercerVencimiento {
            get {
                return Model.TercerVencimiento;
            }
            set {
                if (Model.TercerVencimiento != value) {
                        Model.TercerVencimiento = value;
                        IsDirty = true;
                        RaisePropertyChanged(TercerVencimientoPropertyName);
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


        // validar lo de la cantidad de dias 

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
        public PropAnalisisVencViewModel()
            : this(new PropAnalisisVenc(), eAccionSR.Insertar) {
        }
        public PropAnalisisVencViewModel(PropAnalisisVenc initModel, eAccionSR initAction)
            : base(initModel, initAction) {
            DefaultFocusedPropertyName = PrimerVencimientoPropertyName;
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(PropAnalisisVenc valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override PropAnalisisVenc FindCurrentRecord(PropAnalisisVenc valModel) {
            if (valModel == null) {
                return null;
            }
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("SecuencialUnique0", valModel.SecuencialUnique0);
            return BusinessComponent.GetData(eProcessMessageType.SpName, "PropAnalisisVencGET", vParams.Get()).FirstOrDefault();
        }

        protected override ILibBusinessComponentWithSearch<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>> GetBusinessComponent() {
            return new clsPropAnalisisVencNav();
        }



        private ValidationResult PrimerVencimientoValidate() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                return ValidationResult.Success;
            } else {
                if (PrimerVencimiento == 0) {
                    vResult = new ValidationResult("El valor asignado al campo Primer Vencimiento debe ser mayor que cero");
                } else if ((PrimerVencimiento >= SegundoVencimiento) || (PrimerVencimiento >= TercerVencimiento)) {
                    vResult = new ValidationResult("El valor asignado al campo Primer Vencimiento debe ser  menor al Segundo y Tercer Vencimiento");
                }

            }
            return vResult;
        }


        private ValidationResult SegundoVencimientoValidate() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                return ValidationResult.Success;
            } else {
                if (SegundoVencimiento == 0) {
                    vResult = new ValidationResult("El valor asignado al campo Segundo Vencimiento debe ser mayor que cero");
                } else if ((PrimerVencimiento >= SegundoVencimiento) || (SegundoVencimiento >= TercerVencimiento)) {
                    vResult = new ValidationResult("El valor asignado al campo Segundo Vencimiento debe ser mayor al Primer Vencimiento y Menor al Tercer Vencimiento");
                }

            }
            return vResult;
        }

        private ValidationResult TercerVencimientoValidate() {
            ValidationResult vResult = ValidationResult.Success;
            if (Action == eAccionSR.Consultar || Action == eAccionSR.Eliminar) {
                return ValidationResult.Success;
            } else {
                if (TercerVencimiento == 0) {
                    vResult = new ValidationResult("El valor asignado al campo Tercer Vencimiento debe ser mayor que cero");
                } else if ((PrimerVencimiento >= TercerVencimiento) || (SegundoVencimiento >= TercerVencimiento)) {
                    vResult = new ValidationResult("El valor asignado al campo Tercer Vencimiento debe ser  mayor al Primer y Segundo Vencimiento");
                }

            }
            return vResult;
        }

        #endregion //Metodos Generados


    } //End of class PropAnalisisVencViewModel

} //End of namespace Galac.Saw.Uil.Tablas

