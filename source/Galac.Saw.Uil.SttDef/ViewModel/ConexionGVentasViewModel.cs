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
using Galac.Saw.Brl.SttDef;
using Galac.Saw.Ccl.SttDef;
using Galac.Saw.Lib;
using System.Threading;
using System.Collections.ObjectModel;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class ConexionGVentasViewModel : LibGenericViewModel {
        #region Constantes
        public const string CompaniaActualNombrePropertyName = "CompaniaActualNombre";
        public const string CompaniaActualRIFPropertyName = "CompaniaActualRIF";
        public const string InquilinoNombrePropertyName = "InquilinoNombre";
        public const string CompaniaGVentasNombresPropertyName = "CompaniaGVentasNombres";
        public const string SerialConectorPropertyName = "SerialConector";
        public const string UsuarioDeOperacionesPropertyName = "UsuarioDeOperaciones";
        #endregion
        #region Variables
        #endregion //Variables
        #region Propiedades
        public override string ModuleName {
            get { return "Conexión G-Ventas"; }
        }
        public bool IsDirty { get; private set; }

        private eAccionSR mAction;

        public string CompaniaActualNombre { get; }
        public string CompaniaActualRIF { get; }
        public string InquilinoNombre { get; }

        public ObservableCollection<string> ListaCompaniaGeVentasNombres { get; set; }
        string _CompaniaGVentasNombres;
        public string  CompaniaGVentasNombres {
            get {
                return _CompaniaGVentasNombres;
            }
            set {
                if (_CompaniaGVentasNombres != value) {
                    _CompaniaGVentasNombres = value;
                    IsDirty = true;
                    RaisePropertyChanged(CompaniaGVentasNombresPropertyName);
                }
            }
        }
                
        string _SerialConector;
        public string  SerialConector {
            get {
                return _SerialConector;
            }
            set {
                if (_SerialConector != value) {
                    _SerialConector = value;
                    IsDirty = true;
                    RaisePropertyChanged(SerialConectorPropertyName);
                }
            }
        }

        public ObservableCollection<string> ListaUsuariosDeOperaciones { get; set; }
        string _UsuarioDeOperaciones;
        public string  UsuarioDeOperaciones {
            get {
                return _UsuarioDeOperaciones;
            }
            set {
                if (_UsuarioDeOperaciones != value) {
                    _UsuarioDeOperaciones = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsuarioDeOperacionesPropertyName);
                }
            }
        }

        #endregion //Propiedades
        #region Constructores
        public ConexionGVentasViewModel(eAccionSR valAction) {
            mAction = valAction;
            CompaniaActualNombre = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "Nombre");
            CompaniaActualRIF = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Compania", "NumeroDeRIF");
            UsuarioDeOperaciones = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
            InquilinoNombre = "Nombre-del-inquilino";// BuscaNombreInquilinoSobreSuscripcion();
            LlenaListaCompaniaGVentas();
            LlentaListaUsuariosSupervisoresActivos();
        }
        #endregion //Constructores
        #region Metodos Generados
        #endregion //Metodos Generados

        private string BuscaNombreInquilinoSobreSuscripcion() {
            return "Falta Programar";
        }

        private void LlenaListaCompaniaGVentas() {
            ListaCompaniaGeVentasNombres = new ObservableCollection<string>();// new LibWebConnector.clsSuscripcion().GetCompaniaGVentas("");
        }

        private void LlentaListaUsuariosSupervisoresActivos() {
            ListaUsuariosDeOperaciones = new ObservableCollection<string>();// ((ISettValueByCompanyPdn)new clsSettValueByCompanyNav()).ListaDeUsuariosSupervisoresActivos();
            //UsuarioDeOperaciones = ((CustomIdentity)Thread.CurrentPrincipal.Identity).Login;
        }

    } //End of class ConexionGVentasViewModel

} //End of namespace Galac..Uil.SttDef

