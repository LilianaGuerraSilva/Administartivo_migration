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
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Uil;
using Galac.Saw.Brl.SttDef;

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaCobroFacturaViewModel : LibInputViewModelMfc<FacturaCobroFacturaStt> {
        #region Constantes
        public const string EmitirDirectoPropertyName = "EmitirDirecto";
        public const string UsaCobroDirectoPropertyName = "UsaCobroDirecto";
        public const string UsaCobroDirectoEnMultimonedaPropertyName = "UsaCobroDirectoEnMultimoneda";
        public const string CuentaBancariaCobroDirectoPropertyName = "CuentaBancariaCobroDirecto";
        public const string UsaMediosElectronicosDeCobroPropertyName = "UsaMediosElectronicosDeCobro";
        public const string ConceptoBancarioCobroDirectoPropertyName = "ConceptoBancarioCobroDirecto";
        public const string CuentaBancariaCobroMultimonedaPropertyName = "CuentaBancariaCobroMultimoneda";
        public const string ConceptoBancarioCobroMultimonedaPropertyName = "ConceptoBancarioCobroMultimoneda";

        public const string UsaCreditoElectronicoPropertyName = "UsaCreditoElectronico";
        public const string NombreCreditoElectronicoPropertyName = "NombreCreditoElectronico";
        public const string DiasDeCreditoPorCuotaCreditoElectronicoPropertyName = "DiasDeCreditoPorCuotaCreditoElectronico";
        public const string CantidadCuotasUsualesCreditoElectronicoPropertyName = "CantidadCuotasUsualesCreditoElectronico";
        public const string MaximaCantidadCuotasCreditoElectronicoPropertyName = "MaximaCantidadCuotasCreditoElectronico";
        public const string UsaClienteUnicoCreditoElectronicoPropertyName = "UsaClienteUnicoCreditoElectronico";
        public const string CodigoClienteCreditoElectronicoPropertyName = "CodigoClienteCreditoElectronico";
        public const string GenerarUnaUnicaCuotaCreditoElectronicoPropertyName = "GenerarUnaUnicaCuotaCreditoElectronico";
        #endregion
        #region Variables
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioCobroDirecto = null;
        private FkCuentaBancariaViewModel _ConexionCuentaBancariaCobroDirecto = null;
        private FkConceptoBancarioViewModel _ConexionConceptoBancarioCobroMultimoneda = null;
        private FkCuentaBancariaViewModel _ConexionCuentaBancariaCobroMultimoneda = null;
        private FkClienteViewModel _ConexionCodigoClienteCreditoElectronico = null;
        #endregion //Variables
        #region Propiedades
        public override string ModuleName {
            get { return "2.9.- Cobro de Factura"; }
        }

        public bool EmitirDirecto {
            get {
                return Model.EmitirDirectoAsBool;
            }
            set {
                if (Model.EmitirDirectoAsBool != value) {
                    Model.EmitirDirectoAsBool = value;
                    if (!Model.EmitirDirectoAsBool) {
                        LibMessages.MessageBox.Warning(this, "Al no tener activado Emitir Directo, la opción de Cobro Directo será desactivada.", string.Empty);
                        CuentaBancariaCobroDirecto = string.Empty;
                        ConceptoBancarioCobroDirecto = string.Empty;
                        UsaCobroDirecto = EmitirDirecto;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(EmitirDirectoPropertyName);
                    RaisePropertyChanged(() => IsEnabledEmitirDirecto);
                    LibMessages.Notification.Send<bool>(Model.EmitirDirectoAsBool, EmitirDirectoPropertyName);
                }
            }
        }

        [LibCustomValidation("CobroDirectoValidating")]
        public bool UsaCobroDirecto {
            get {
                return Model.UsaCobroDirectoAsBool;
            }
            set {
                if (Model.UsaCobroDirectoAsBool != value) {
                    Model.UsaCobroDirectoAsBool = value;
                    if (!Model.UsaCobroDirectoAsBool) {
                        CuentaBancariaCobroDirecto = string.Empty;
                        ConceptoBancarioCobroDirecto = string.Empty;
                        UsaCobroDirectoEnMultimoneda = false;
                        CuentaBancariaCobroMultimoneda = string.Empty;
                        ConceptoBancarioCobroMultimoneda = string.Empty;
                    } else {
                        EmitirDirecto = true;
                        if (LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto)) {
                            ReloadCodigoGenericoCuentaBancaria();
                        }
                    }
                    IsDirty = true;
                    RaisePropertyChanged(UsaCobroDirectoPropertyName);
                    RaisePropertyChanged(() => IsEnabledUsaCobroDirecto);
                    RaisePropertyChanged(() => ConceptoBancarioCobroDirecto);
                    RaisePropertyChanged(() => ConceptoBancarioCobroMultimoneda);
                    RaisePropertyChanged(() => CuentaBancariaCobroMultimoneda);
                    RaisePropertyChanged(() => IsEnabledCuentaBancariaCobroDirecto);
                    RaisePropertyChanged(() => IsEnabledConceptoBancarioCobroDirecto);
                    RaisePropertyChanged(() => UsaCobroDirectoEnMultimoneda);
                    RaisePropertyChanged(() => IsEnabledUsaCobroDirectoEnMultimoneda);
                    RaisePropertyChanged(() => CuentaBancariaCobroMultimoneda);
                    RaisePropertyChanged(() => ConceptoBancarioCobroMultimoneda);
                    RaisePropertyChanged(() => IsEnabledConceptoBancarioCobroMultimoneda);
                    RaisePropertyChanged(() => IsEnabledCuentaBancariaCobroMultimoneda);
                    LibMessages.Notification.Send<bool>(Model.UsaCobroDirectoAsBool, UsaCobroDirectoPropertyName);
                }
            }
        }

        public bool UsaCobroDirectoEnMultimoneda {
            get {
                return Model.UsaCobroDirectoEnMultimonedaAsBool;
            }
            set {
                if (Model.UsaCobroDirectoEnMultimonedaAsBool != value) {
                    Model.UsaCobroDirectoEnMultimonedaAsBool = value;
                    if (!Model.UsaCobroDirectoEnMultimonedaAsBool) {
                        CuentaBancariaCobroMultimoneda = string.Empty;
                        ConceptoBancarioCobroMultimoneda = string.Empty;
                        UsaMediosElectronicosDeCobro = false;
                        UsaCreditoElectronico = false;
                    }
                    RaisePropertyChanged(() => UsaCobroDirectoEnMultimoneda);
                    RaisePropertyChanged(() => ConceptoBancarioCobroMultimoneda);
                    RaisePropertyChanged(() => CuentaBancariaCobroMultimoneda);
                    RaisePropertyChanged(() => UsaMediosElectronicosDeCobro);
                    RaisePropertyChanged(() => IsEnabledUsaCobroDirectoEnMultimoneda);
                    RaisePropertyChanged(() => IsEnabledCuentaBancariaCobroMultimoneda);
                    RaisePropertyChanged(() => IsEnabledConceptoBancarioCobroMultimoneda);
                    RaisePropertyChanged(() => UsaCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledCreditoElectronico);
                }
            }
        }

        [LibCustomValidation("CuentaBancariaCobroDirectoValidating")]
        public string CuentaBancariaCobroDirecto {
            get {
                return Model.CuentaBancariaCobroDirecto;
            }
            set {
                if (Model.CuentaBancariaCobroDirecto != value) {
                    if (UsaCobroDirecto && value == null) {
                        ExecuteChooseCuentaBancariaCobroDirectoCommand(string.Empty);
                    } else {
                        Model.CuentaBancariaCobroDirecto = value;
                    }
                    IsDirty = true;
                    RaisePropertyChanged(CuentaBancariaCobroDirectoPropertyName);
                    LibMessages.Notification.Send<string>(Model.CuentaBancariaCobroDirecto, CuentaBancariaCobroDirectoPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto, true)) {
                        ConexionCuentaBancariaCobroDirecto = null;
                    }
                }
            }
        }

        public bool UsaMediosElectronicosDeCobro {
            get {
                return Model.UsaMediosElectronicosDeCobroAsBool;
            }
            set {
                if (Model.UsaMediosElectronicosDeCobroAsBool != value) {
                    Model.UsaMediosElectronicosDeCobroAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaMediosElectronicosDeCobroPropertyName);
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioCobroDirectoValidating")]
        public string ConceptoBancarioCobroDirecto {
            get {
                return Model.ConceptoBancarioCobroDirecto;
            }
            set {
                if (Model.ConceptoBancarioCobroDirecto != value) {
                    Model.ConceptoBancarioCobroDirecto = value;
                    IsDirty = true;
                    RaisePropertyChanged(ConceptoBancarioCobroDirectoPropertyName);
                    LibMessages.Notification.Send<string>(Model.ConceptoBancarioCobroDirecto, ConceptoBancarioCobroDirectoPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioCobroDirecto, true)) {
                        ConexionConceptoBancarioCobroDirecto = null;
                    }
                }
            }
        }

        [LibCustomValidation("CuentaBancariaCobroMultimonedaValidating")]
        public string CuentaBancariaCobroMultimoneda {
            get {
                return Model.CuentaBancariaCobroMultimoneda;
            }
            set {
                if (Model.CuentaBancariaCobroMultimoneda != value) {
                    if (UsaCobroDirectoEnMultimoneda && value == null) {
                        ExecuteChooseCuentaBancariaCobroMultimonedaCommand(string.Empty);
                    } else {
                        Model.CuentaBancariaCobroMultimoneda = value;
                    }
                    RaisePropertyChanged(CuentaBancariaCobroMultimonedaPropertyName);
                    LibMessages.Notification.Send<string>(Model.CuentaBancariaCobroMultimoneda, CuentaBancariaCobroMultimonedaPropertyName);
                    if (LibString.IsNullOrEmpty(CuentaBancariaCobroMultimoneda, true)) {
                        ConexionCuentaBancariaCobroMultimoneda = null;
                    }
                }
            }
        }

        [LibCustomValidation("ConceptoBancarioCobroMultimonedaValidating")]
        public string ConceptoBancarioCobroMultimoneda {
            get {
                return Model.ConceptoBancarioCobroMultimoneda;
            }
            set {
                if (Model.ConceptoBancarioCobroMultimoneda != value) {
                    Model.ConceptoBancarioCobroMultimoneda = value;
                    RaisePropertyChanged(ConceptoBancarioCobroMultimonedaPropertyName);
                    LibMessages.Notification.Send<string>(Model.ConceptoBancarioCobroMultimoneda, ConceptoBancarioCobroMultimonedaPropertyName);
                    if (LibString.IsNullOrEmpty(ConceptoBancarioCobroMultimoneda, true)) {
                        ConexionConceptoBancarioCobroMultimoneda = null;
                    }
                }
            }
        }

        public bool UsaCreditoElectronico {
            get {
                return Model.UsaCreditoElectronicoAsBool;
            }
            set {
                if (Model.UsaCreditoElectronicoAsBool != value) {
                    Model.UsaCreditoElectronicoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaCreditoElectronicoPropertyName);
                    RaisePropertyChanged(NombreCreditoElectronicoPropertyName);
                    RaisePropertyChanged(DiasDeCreditoPorCuotaCreditoElectronicoPropertyName);
                    RaisePropertyChanged(CantidadCuotasUsualesCreditoElectronicoPropertyName);
                    RaisePropertyChanged(MaximaCantidadCuotasCreditoElectronicoPropertyName);
                    RaisePropertyChanged(UsaClienteUnicoCreditoElectronicoPropertyName);
                    RaisePropertyChanged(CodigoClienteCreditoElectronicoPropertyName);
                    RaisePropertyChanged(GenerarUnaUnicaCuotaCreditoElectronicoPropertyName);
                    RaisePropertyChanged(() => IsEnabledCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledNombreCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledDiasDeCreditoPorCuotaCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledCantidadCuotasUsualesCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledMaximaCantidadCuotasCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledUsaClienteUnicoCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledCodigoClienteCreditoElectronico);
                    RaisePropertyChanged(() => IsEnabledGenerarUnaUnicaCuotaCreditoElectronico);
                    InicalizacionParametrosCreditoElectronico();
                }
            }
        }

        [LibCustomValidation("NombreCreditoElectronicoValidating")]
        public string NombreCreditoElectronico {
            get {
                return Model.NombreCreditoElectronico;
            }
            set {
                if (Model.NombreCreditoElectronico != value) {
                    Model.NombreCreditoElectronico = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreCreditoElectronicoPropertyName);
                }
            }
        }

        public int DiasDeCreditoPorCuotaCreditoElectronico {
            get {
                return Model.DiasDeCreditoPorCuotaCreditoElectronico;
            }
            set {
                if (Model.DiasDeCreditoPorCuotaCreditoElectronico != value) {
                    Model.DiasDeCreditoPorCuotaCreditoElectronico = value;
                    IsDirty = true;
                    RaisePropertyChanged(DiasDeCreditoPorCuotaCreditoElectronicoPropertyName);
                }
            }
        }

        [LibCustomValidation("CantidadCuotasUsualesCreditoElectronicoValidating")]
        public int CantidadCuotasUsualesCreditoElectronico {
            get {
                return Model.CantidadCuotasUsualesCreditoElectronico;
            }
            set {
                if (Model.CantidadCuotasUsualesCreditoElectronico != value) {
                    Model.CantidadCuotasUsualesCreditoElectronico = value;
                    IsDirty = true;
                    RaisePropertyChanged(CantidadCuotasUsualesCreditoElectronicoPropertyName);
                }
            }
        }

        [LibCustomValidation("MaximaCantidadCuotasCreditoElectronicoValidating")]
        public int MaximaCantidadCuotasCreditoElectronico {
            get {
                return Model.MaximaCantidadCuotasCreditoElectronico;
            }
            set {
                if (Model.MaximaCantidadCuotasCreditoElectronico != value) {
                    Model.MaximaCantidadCuotasCreditoElectronico = value;
                    IsDirty = true;
                    RaisePropertyChanged(MaximaCantidadCuotasCreditoElectronicoPropertyName);
                }
            }
        }

        public bool UsaClienteUnicoCreditoElectronico {
            get {
                return Model.UsaClienteUnicoCreditoElectronicoAsBool;
            }
            set {
                if (Model.UsaClienteUnicoCreditoElectronicoAsBool != value) {
                    Model.UsaClienteUnicoCreditoElectronicoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaClienteUnicoCreditoElectronicoPropertyName);
                    RaisePropertyChanged(() => IsEnabledCodigoClienteCreditoElectronico);
                    CodigoClienteCreditoElectronico = string.Empty;
                    RaisePropertyChanged(CodigoClienteCreditoElectronicoPropertyName);
                }
            }
        }

        [LibCustomValidation("CodigoClienteCreditoElectronicoValidating")]
        public string CodigoClienteCreditoElectronico {
            get {
                return Model.CodigoClienteCreditoElectronico;
            }
            set {
                if (Model.CodigoClienteCreditoElectronico != value) {
                    Model.CodigoClienteCreditoElectronico = value;
                    IsDirty = true;
                    RaisePropertyChanged(CodigoClienteCreditoElectronicoPropertyName);
                    if (LibString.IsNullOrEmpty(CodigoClienteCreditoElectronico, true)) {
                        ConexionCodigoClienteCreditoElectronico = null;
                    }
                    RaisePropertyChanged(CodigoClienteCreditoElectronicoPropertyName);
                }
            }
        }

        public bool GenerarUnaUnicaCuotaCreditoElectronico {
            get {
                return Model.GenerarUnaUnicaCuotaCreditoElectronicoAsBool;
            }
            set {
                if (Model.GenerarUnaUnicaCuotaCreditoElectronicoAsBool != value) {
                    Model.GenerarUnaUnicaCuotaCreditoElectronicoAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(GenerarUnaUnicaCuotaCreditoElectronicoPropertyName);
                }
            }
        }

        private ParametersViewModel _ParametrosBancoMoneda;
        public ParametersViewModel ParametrosViewModel {
            get {
                return _ParametrosBancoMoneda;
            }
            set {
                if (_ParametrosBancoMoneda != value) {
                    _ParametrosBancoMoneda = value;                    
                    RaisePropertyChanged("ParametrosBancoMoneda");
                }
            }
        }

        #region IsEnabled
        public bool IsEnabledEmitirDirecto {
            get { return IsEnabled && EmitirDirecto; }
        }

        public bool IsEnabledUsaCobroDirecto {
            get { return IsEnabled && UsaCobroDirecto; }
        }

        public bool IsEnabledCuentaBancariaCobroDirecto {
            get { return IsEnabled && UsaCobroDirecto; }
        }

        public bool IsEnabledUsaCobroDirectoEnMultimoneda {
            get {
                return IsEnabled && UsaCobroDirecto;
            }
        }
        public bool IsEnabledConceptoBancarioCobroDirecto {
            get {
                return IsEnabled && UsaCobroDirecto;
            }
        }

        public bool IsEnabledConceptoBancarioCobroMultimoneda {
            get {
                return IsEnabled && UsaCobroDirectoEnMultimoneda;
            }
        }
        public bool IsEnabledCuentaBancariaCobroMultimoneda {
            get {
                return IsEnabled && UsaCobroDirectoEnMultimoneda;
            }
        }

        public bool IsEnabledCreditoElectronico {
            get {
                return IsEnabled && UsaCobroDirectoEnMultimoneda && UsaMonedaExtranjera && CodigoMonedaExtranjera == "USD"; ;
            }
        }
        public bool IsEnabledNombreCreditoElectronico {
            get { return IsEnabled && UsaCreditoElectronico; }
        }
        public bool IsEnabledDiasDeCreditoPorCuotaCreditoElectronico {
            get { return IsEnabled && UsaCreditoElectronico; }
        }
        public bool IsEnabledCantidadCuotasUsualesCreditoElectronico {
            get { return IsEnabled && UsaCreditoElectronico; }
        }
        public bool IsEnabledMaximaCantidadCuotasCreditoElectronico {
            get { return IsEnabled && UsaCreditoElectronico; }
        }
        public bool IsEnabledUsaClienteUnicoCreditoElectronico {
            get { return IsEnabled && UsaCreditoElectronico; }
        }
        public bool IsEnabledCodigoClienteCreditoElectronico {
            get { return IsEnabled && UsaCreditoElectronico && UsaClienteUnicoCreditoElectronico; }
        }
        public bool IsEnabledGenerarUnaUnicaCuotaCreditoElectronico {
            get { return IsEnabled && UsaCreditoElectronico; }
        }
        #endregion IsEnabled

        #region Fk Conexion
        public FkCuentaBancariaViewModel ConexionCuentaBancariaCobroDirecto {
            get {
                return _ConexionCuentaBancariaCobroDirecto;
            }
            set {
                if (_ConexionCuentaBancariaCobroDirecto != value) {
                    _ConexionCuentaBancariaCobroDirecto = value;
                    if (_ConexionCuentaBancariaCobroDirecto != null) {
                        CuentaBancariaCobroDirecto = _ConexionCuentaBancariaCobroDirecto.Codigo;
                    } else if (_ConexionCuentaBancariaCobroDirecto == null) {
                        CuentaBancariaCobroDirecto = string.Empty;
                    }
                }
                if (_ConexionCuentaBancariaCobroDirecto == null) {
                    CuentaBancariaCobroDirecto = string.Empty;
                }
                RaisePropertyChanged(CuentaBancariaCobroDirectoPropertyName);
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioCobroDirecto {
            get {
                return _ConexionConceptoBancarioCobroDirecto;
            }
            set {
                if (_ConexionConceptoBancarioCobroDirecto != value) {
                    _ConexionConceptoBancarioCobroDirecto = value;
                    if (_ConexionConceptoBancarioCobroDirecto != null) {
                        ConceptoBancarioCobroDirecto = _ConexionConceptoBancarioCobroDirecto.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioCobroDirecto == null) {
                    ConceptoBancarioCobroDirecto = string.Empty;
                }
                RaisePropertyChanged(ConceptoBancarioCobroDirectoPropertyName);
            }
        }

        public FkCuentaBancariaViewModel ConexionCuentaBancariaCobroMultimoneda {
            get {
                return _ConexionCuentaBancariaCobroMultimoneda;
            }
            set {
                if (_ConexionCuentaBancariaCobroMultimoneda != value) {
                    _ConexionCuentaBancariaCobroMultimoneda = value;
                    if (_ConexionCuentaBancariaCobroMultimoneda != null) {
                        CuentaBancariaCobroMultimoneda = _ConexionCuentaBancariaCobroMultimoneda.Codigo;
                    } else if (_ConexionCuentaBancariaCobroMultimoneda == null) {
                        CuentaBancariaCobroMultimoneda = string.Empty;
                    }
                }
                if (_ConexionCuentaBancariaCobroMultimoneda == null) {
                    CuentaBancariaCobroMultimoneda = string.Empty;
                }
                RaisePropertyChanged(CuentaBancariaCobroMultimonedaPropertyName);
            }
        }

        public FkConceptoBancarioViewModel ConexionConceptoBancarioCobroMultimoneda {
            get {
                return _ConexionConceptoBancarioCobroMultimoneda;
            }
            set {
                if (_ConexionConceptoBancarioCobroMultimoneda != value) {
                    _ConexionConceptoBancarioCobroMultimoneda = value;
                    if (_ConexionConceptoBancarioCobroMultimoneda != null) {
                        ConceptoBancarioCobroMultimoneda = _ConexionConceptoBancarioCobroMultimoneda.Codigo;
                    }
                }
                if (_ConexionConceptoBancarioCobroMultimoneda == null) {
                    ConceptoBancarioCobroMultimoneda = string.Empty;
                }
                RaisePropertyChanged(ConceptoBancarioCobroMultimonedaPropertyName);
            }
        }

        public FkClienteViewModel ConexionCodigoClienteCreditoElectronico {
            get {
                return _ConexionCodigoClienteCreditoElectronico;
            }
            set {
                if (_ConexionCodigoClienteCreditoElectronico != value) {
                    _ConexionCodigoClienteCreditoElectronico = value;
                    if (_ConexionCodigoClienteCreditoElectronico == null) {
                        CodigoClienteCreditoElectronico = string.Empty;
                    } else {
                        CodigoClienteCreditoElectronico = _ConexionCodigoClienteCreditoElectronico.Codigo;
                    }
                    RaisePropertyChanged(CodigoClienteCreditoElectronicoPropertyName);
                }
            }
        }
        #endregion Fk Conexion

        #region Choose
        public RelayCommand<string> ChooseCuentaBancariaCobroDirectoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoBancarioCobroDirectoCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCuentaBancariaCobroMultimonedaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseConceptoBancarioCobroMultimonedaCommand {
            get;
            private set;
        }

        public RelayCommand<string> ChooseCodigoClienteCreditoElectronicoCommand {
            get;
            private set;
        }
        #endregion Choose

        #endregion //Propiedades
        #region Propiedades Externas
        bool UsaMonedaExtranjera {
            get {
                var vBancosMonedaVM = ParametrosViewModel.ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Bancos)).FirstOrDefault().Groups.Where(y => y.DisplayName == new BancosMonedaViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as BancosMonedaViewModel;
                return vBancosMonedaVM.UsaMonedaExtranjera;
            }
        }

        string CodigoMonedaExtranjera {
            get {
                var vBancosMonedaVM = ParametrosViewModel.ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Bancos)).FirstOrDefault().Groups.Where(y => y.DisplayName == new BancosMonedaViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as BancosMonedaViewModel;
                return vBancosMonedaVM.CodigoMonedaExtranjera;
            }
        }
        bool UsaListaDePrecioEnMonedaExtranjeraCXC {
            get {
                var vFactContVM = ParametrosViewModel.ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.Factura)).FirstOrDefault().Groups.Where(y => y.DisplayName == new FacturaFacturacionContViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as FacturaFacturacionContViewModel;
                return vFactContVM.UsaListaDePrecioEnMonedaExtranjeraCXC;
            }
        }

        string CodigoGenericoCliente {
            get {
                var vCxCCobranzaVM = ParametrosViewModel.ModuleList.Where(w => w.DisplayName == LibEnumHelper.GetDescription(eModulesLevelName.CXCCobranzas)).FirstOrDefault().Groups.Where(y => y.DisplayName == new CXCCobranzasClienteViewModel(null, eAccionSR.Consultar).ModuleName).FirstOrDefault().Content as CXCCobranzasClienteViewModel;
                return vCxCCobranzaVM.CodigoGenericoCliente;
            }
        }
        #endregion //Propiedades Externas
        #region Constructores
        public FacturaCobroFacturaViewModel()
            : this(new FacturaCobroFacturaStt(), eAccionSR.Insertar) {
        }
        public FacturaCobroFacturaViewModel(FacturaCobroFacturaStt initModel, eAccionSR initAction)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
            DefaultFocusedPropertyName = EmitirDirectoPropertyName;
            RaisePropertyChanged(() => IsEnabledCreditoElectronico);
        }
        #endregion //Constructores
        #region Metodos Generados
        protected override void InitializeLookAndFeel(FacturaCobroFacturaStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override FacturaCobroFacturaStt FindCurrentRecord(FacturaCobroFacturaStt valModel) {
            if (valModel == null) {
                return new FacturaCobroFacturaStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<FacturaCobroFacturaStt>, IList<FacturaCobroFacturaStt>> GetBusinessComponent() {
            return null;
        }
        #endregion //Metodos Generados
        protected override void InitializeCommands() {
            base.InitializeCommands();
            ChooseConceptoBancarioCobroDirectoCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioCobroDirectoCommand);
            ChooseCuentaBancariaCobroDirectoCommand = new RelayCommand<string>(ExecuteChooseCuentaBancariaCobroDirectoCommand);
            ChooseConceptoBancarioCobroMultimonedaCommand = new RelayCommand<string>(ExecuteChooseConceptoBancarioCobroMultimonedaCommand);
            ChooseCuentaBancariaCobroMultimonedaCommand = new RelayCommand<string>(ExecuteChooseCuentaBancariaCobroMultimonedaCommand);
            ChooseCodigoClienteCreditoElectronicoCommand = new RelayCommand<string>(ExecuteChooseCodigoClienteCreditoElectronicoCommand);
        }

        protected override void ReloadRelatedConnections() {
            base.ReloadRelatedConnections();
            ConexionConceptoBancarioCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioCobroDirecto), new clsSettValueByCompanyNav());
            ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", CuentaBancariaCobroDirecto), new clsSettValueByCompanyNav());
            ConexionConceptoBancarioCobroMultimoneda = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkConceptoBancarioViewModel>("Concepto Bancario", LibSearchCriteria.CreateCriteria("codigo", ConceptoBancarioCobroMultimoneda), new clsSettValueByCompanyNav());
            ConexionCuentaBancariaCobroMultimoneda = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", CuentaBancariaCobroMultimoneda), new clsSettValueByCompanyNav());
            ConexionCodigoClienteCreditoElectronico = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkClienteViewModel>("Cliente", LibSearchCriteria.CreateCriteria("Codigo", CodigoClienteCreditoElectronico), new clsSettValueByCompanyNav());
        }

        #region ExecuteChoose-Command
        private void ExecuteChooseConceptoBancarioCobroDirectoCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioCobroDirecto = null;
                ConexionConceptoBancarioCobroDirecto = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaBancariaCobroDirectoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false));
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
                ConexionCuentaBancariaCobroDirecto = null;
                ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaBancariaCobroDirecto != null) {
                    CuentaBancariaCobroDirecto = ConexionCuentaBancariaCobroDirecto.Codigo;
                } else {
                    CuentaBancariaCobroDirecto = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseConceptoBancarioCobroMultimonedaCommand(string valcodigo) {
            try {
                if (valcodigo == null) {
                    valcodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Codigo", valcodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Adm.Gv_ConceptoBancario_B1.Tipo", LibConvert.EnumToDbValue((int)eIngresoEgreso.Ingreso));
                ConexionConceptoBancarioCobroMultimoneda = null;
                ConexionConceptoBancarioCobroMultimoneda = LibFKRetrievalHelper.ChooseRecord<FkConceptoBancarioViewModel>("Concepto Bancario", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCuentaBancariaCobroMultimonedaCommand(string valCodigo) {
            try {
                string vCodigoMonedaLocal = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "CodigoMonedaCompania");
                vCodigoMonedaLocal = (LibString.IsNullOrEmpty(vCodigoMonedaLocal) ? (LibDate.F1IsLessThanF2(LibDate.Today(), Galac.Saw.Reconv.clsUtilReconv.GetFechaReconversion()) ? "VES" : "VED") : vCodigoMonedaLocal);
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_CuentaBancaria_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.EsCajaChica", LibConvert.BoolToSN(false));
                vFixedCriteria.Add("Gv_CuentaBancaria_B1.CodigoMoneda", eBooleanOperatorType.IdentityInequality, vCodigoMonedaLocal);
                vFixedCriteria.Add(LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.ConsecutivoCompania", Mfc.GetInt("Compania")), eLogicOperatorType.And);
                ConexionCuentaBancariaCobroMultimoneda = null;
                ConexionCuentaBancariaCobroMultimoneda = LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", vDefaultCriteria, vFixedCriteria, string.Empty);
                if (ConexionCuentaBancariaCobroMultimoneda != null) {
                    CuentaBancariaCobroMultimoneda = ConexionCuentaBancariaCobroMultimoneda.Codigo;
                } else {
                    CuentaBancariaCobroMultimoneda = string.Empty;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void ExecuteChooseCodigoClienteCreditoElectronicoCommand(string valCodigo) {
            try {
                if (valCodigo == null) {
                    valCodigo = string.Empty;
                }
                LibSearchCriteria vDefaultCriteria = LibSearchCriteria.CreateCriteriaFromText("Gv_Cliente_B1.Codigo", valCodigo);
                LibSearchCriteria vFixedCriteria = LibSearchCriteria.CreateCriteria("Gv_Cliente_B1.ConsecutivoCompania", LibConvert.ToStr(Mfc.GetInt("Compania")));
                ConexionCodigoClienteCreditoElectronico = null;
                ConexionCodigoClienteCreditoElectronico = LibFKRetrievalHelper.ChooseRecord<FkClienteViewModel>("Cliente", vDefaultCriteria, vFixedCriteria, string.Empty);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }
        #endregion //ExecuteChoose-Command

        private void ReloadCodigoGenericoCuentaBancaria() {
            Galac.Adm.Ccl.Banco.ICuentaBancariaPdn insCuentaBancariaPdn = new Galac.Adm.Brl.Banco.clsCuentaBancariaNav();
            ConexionCuentaBancariaCobroDirecto = LibFKRetrievalHelper.FirstConnectionRecordOrDefault<FkCuentaBancariaViewModel>("Cuenta Bancaria", LibSearchCriteria.CreateCriteria("Gv_CuentaBancaria_B1.Codigo", insCuentaBancariaPdn.GetCuentaBancariaGenericaPorDefecto()), new clsSettValueByCompanyNav());
        }

        #region Validating
        private ValidationResult CobroDirectoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCobroDirecto && UsaListaDePrecioEnMonedaExtranjeraCXC) {
                vResult = new ValidationResult($"No es posible activar los parámetros \"Generar CxC en Moneda Extranjera\" y \"Cobro Directo\" simultaneamente. Para hacer uso del parámetro \"Cobro Directo\", por favor desactive \"Generar CXC en Moneda Extranjera\".");
            }
            return vResult;
        }
        private ValidationResult CuentaBancariaCobroDirectoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCobroDirecto && LibString.IsNullOrEmpty(CuentaBancariaCobroDirecto)) {
                vResult = new ValidationResult(ModuleName + " -> Debe indicar una cuenta bancaria cobro directo");
            }
            return vResult;
        }
        private ValidationResult ConceptoBancarioCobroDirectoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCobroDirecto && LibString.IsNullOrEmpty(ConceptoBancarioCobroDirecto)) {
                vResult = new ValidationResult(ModuleName + " -> Debe indicar un Concepto Bancario de Cobro Directo");
            }
            return vResult;
        }

        private ValidationResult CuentaBancariaCobroMultimonedaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCobroDirectoEnMultimoneda && LibString.IsNullOrEmpty(CuentaBancariaCobroMultimoneda)) {
                vResult = new ValidationResult(ModuleName + " -> Debe indicar una cuenta bancaria en moneda extranjera para Cobro en Multimoneda");
            }
            return vResult;
        }

        private ValidationResult ConceptoBancarioCobroMultimonedaValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCobroDirectoEnMultimoneda && LibString.IsNullOrEmpty(ConceptoBancarioCobroMultimoneda)) {
                vResult = new ValidationResult(ModuleName + " -> Debe indicar un Concepto Bancario de Cobro en Multimoneda");
            }
            return vResult;
        }

        private ValidationResult NombreCreditoElectronicoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCreditoElectronico && NombreCreditoElectronico.Length <= 0) {
                vResult = new ValidationResult($"Debe indicar un nombre para el uso de Crédito Electrónico.");
            }
            return vResult;
        }

        private ValidationResult CantidadCuotasUsualesCreditoElectronicoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCreditoElectronico && (CantidadCuotasUsualesCreditoElectronico < 1 || CantidadCuotasUsualesCreditoElectronico > 100)) {
                vResult = new ValidationResult($"Debe indicar una Cantidad de Cuotas.");
            }
            return vResult;
        }

        private ValidationResult MaximaCantidadCuotasCreditoElectronicoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCreditoElectronico && (MaximaCantidadCuotasCreditoElectronico < CantidadCuotasUsualesCreditoElectronico)) {                
                vResult = new ValidationResult($"Debe indicar una Máxima Cantidad de Cuotas válida.");
            }
            return vResult;
        }

        private ValidationResult CodigoClienteCreditoElectronicoValidating() {
            ValidationResult vResult = ValidationResult.Success;
            if ((Action == eAccionSR.Consultar) || (Action == eAccionSR.Eliminar)) {
                return ValidationResult.Success;
            } else if (UsaCreditoElectronico && UsaClienteUnicoCreditoElectronico && LibString.IsNullOrEmpty(CodigoClienteCreditoElectronico)) {
                vResult = new ValidationResult($"Debe indicar un Código de Cliente genérico válido.");
            }
            return vResult;

        }
        #endregion //Validating

        private void InicalizacionParametrosCreditoElectronico() {
            NombreCreditoElectronico = UsaCreditoElectronico ? "CASHEA" : "Crédito Electrónico";
            DiasDeCreditoPorCuotaCreditoElectronico = 14;
            CantidadCuotasUsualesCreditoElectronico = 6;
            MaximaCantidadCuotasCreditoElectronico = 6;
            UsaClienteUnicoCreditoElectronico = true;
            CodigoClienteCreditoElectronico = CodigoGenericoCliente;
            GenerarUnaUnicaCuotaCreditoElectronico = true;
        }

    } //End of class FacturaCobroFacturaViewModel
} //End of namespace Galac.Saw.Uil.SttDef.ViewModel