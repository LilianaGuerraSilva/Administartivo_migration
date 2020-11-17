using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Report;
using LibGalac.Aos.UI.Cib;
using LibGalac.Aos.UI.Mvvm;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Helpers;
using LibGalac.Aos.UI.Mvvm.Validation;
using Galac.Adm.Ccl.GestionCompras;
using Galac.Adm.Brl.GestionCompras;
using Galac.Adm.Uil.GestionCompras.ViewModel;
using LibGalac.Aos.DefGen;
namespace Galac.Adm.Uil.GestionCompras.Reportes {

    public class clsImprimirCotizacionOrdenDeCompraViewModel : LibInputRptViewModelBase<Compra> {
        #region Variables
        string _NumeroCotizacion = "";
        bool _IsEnabled = false;
        private FkCotizacionViewModel _ConexionNumeroCotizacion = null;
        public const string NumeroCotizacionPropertyName = "NumeroCotizacion";
        public const string IsEnabledPropertyName = "IsEnabled";
        #endregion //Variables
        #region Propiedades

        public override string DisplayName {
            get {
                return "Cotizacion Orden de Compra";
            }
        }
        public LibXmlMemInfo AppMemoryInfo {
            get;
            set;
        }

        public LibXmlMFC Mfc {
            get;
            set;
        }


        public override bool IsSSRS {
            get {
                return false;
            }
        }

        public string NumeroCotizacion {
            get {
                return _NumeroCotizacion;
            }
            set {
                if (_NumeroCotizacion != value) {
                    _NumeroCotizacion = value;
                    RaisePropertyChanged(NumeroCotizacionPropertyName);
                    if (LibString.IsNullOrEmpty(NumeroCotizacion, true)) {
                        ConexionNumeroCotizacion = null;
                    }
                }
            }
        }

        public FkCotizacionViewModel ConexionNumeroCotizacion {
            get {
                return _ConexionNumeroCotizacion;
            }
            set {
                if (_ConexionNumeroCotizacion != value) {
                    _ConexionNumeroCotizacion = value;
                    RaisePropertyChanged(NumeroCotizacionPropertyName);
                }
                if (_ConexionNumeroCotizacion == null) {
                    NumeroCotizacion = string.Empty;
                }
            }
        }

        public bool IsEnabled {
            get {
                return _IsEnabled;
            }
            set {
                if (_IsEnabled != value) {
                    _IsEnabled = value;
                    if (!_IsEnabled) {
                        NumeroCotizacion = "";
                    }
                    RaisePropertyChanged(IsEnabledPropertyName);
                }
            }
        }

        public RelayCommand<string> ChooseNumeroCotizacionCommand {
            get;
            private set;
        }
        #endregion //Propiedades
        #region Constructores

        public clsImprimirCotizacionOrdenDeCompraViewModel() {
        #region Codigo Ejemplo
        /* Codigo de Ejemplo
            FechaDesde = LibDate.AddsNMonths(LibDate.Today(), - 1, false);
            FechaHasta = LibDate.Today();
        */
        #endregion //Codigo Ejemplo
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibBusinessSearch GetBusinessComponent() {
            return new clsOrdenDeCompraNav();
        }
        #endregion //Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseNumeroCotizacionCommand = new RelayCommand<string>(ExecuteChooseNumeroCotizacionCommand);
        }

        private void ExecuteChooseNumeroCotizacionCommand(string valNumero) {
            try {
                if (valNumero == null) {
                    valNumero = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("dbo.cotizacion.Numero", valNumero);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("dbo.cotizacion.ConsecutivoCompania", Mfc.GetInt("Compania"));
                ConexionNumeroCotizacion = ChooseRecord<FkCotizacionViewModel>("Cotizacion", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionNumeroCotizacion != null) {
                    NumeroCotizacion = ConexionNumeroCotizacion.Numero;
                } else {
                    NumeroCotizacion = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, DisplayName);
            }
        }

    } //End of class clsImprimirCotizacionOrdenDeCompraViewModel

} //End of namespace Galac.Adm.Uil.GestionCompras

