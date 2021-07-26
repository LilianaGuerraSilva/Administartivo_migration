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

namespace Galac.Saw.Uil.SttDef.ViewModel {
    public class FacturaFacturacionViewModel : LibInputViewModelMfc<FacturacionStt> {
        #region Constantes
        public const string BuscarArticuloXSerialAlFacturarPropertyName = "BuscarArticuloXSerialAlFacturar";
        public const string CambiarFechaEnCuotasLuegoDeFijarFechaEntregaPropertyName = "CambiarFechaEnCuotasLuegoDeFijarFechaEntrega";
        public const string ComisionesEnFacturaPropertyName = "ComisionesEnFactura";
        public const string ComisionesEnRenglonesPropertyName = "ComisionesEnRenglones";
        public const string ComplConComodinEnBusqDeArtInvPropertyName = "ComplConComodinEnBusqDeArtInv";
        public const string DevolucionReversoSeGeneraComoPropertyName = "DevolucionReversoSeGeneraComo";
        public const string ExigirRifdeClienteAlEmitirFacturaPropertyName = "ExigirRifdeClienteAlEmitirFactura";
        public const string ItemsMontoPropertyName = "ItemsMonto";
        public const string NombreVendedorDosPropertyName = "NombreVendedorDos";
        public const string NombreVendedorTresPropertyName = "NombreVendedorTres";
        public const string NombreVendedorUnoPropertyName = "NombreVendedorUno";

        public const string NumFacturasManualesFaltantesPropertyName = "NumFacturasManualesFaltantes";
        public const string PedirInformacionLibroVentasXlsalEmitirFacturaPropertyName = "PedirInformacionLibroVentasXlsalEmitirFactura";
        public const string PermitirFacturarConCantidadCeroPropertyName = "PermitirFacturarConCantidadCero";
        public const string ResumenVtasAfectaInventarioPropertyName = "ResumenVtasAfectaInventario";
        public const string SugerirNumeroControlFacturaPropertyName = "SugerirNumeroControlFactura";
        public const string TipoDeNivelDePreciosPropertyName = "TipoDeNivelDePrecios";
        public const string UsaPrecioSinIvaPropertyName = "UsaPrecioSinIva";
        public const string UsaPrecioSinIvaEnResumenVtasPropertyName = "UsaPrecioSinIvaEnResumenVtas";
        public const string UsarRenglonesEnResumenVtasPropertyName = "UsarRenglonesEnResumenVtas";
        public const string UsarResumenDiarioDeVentasPropertyName = "UsarResumenDiarioDeVentas";
        public const string VerificarFacturasManualesFaltantesPropertyName = "VerificarFacturasManualesFaltantes";
        public const string PermitirCambioTasaMondExtrajalEmitirFacturaPropertyName = "PermitirCambioTasaMondExtrajalEmitirFactura";
        public const string IsVisibleRenglonVendedor1PropertyName = "IsVisibleRenglonVendedor1";
        public const string IsVisibleRenglonVendedor2PropertyName = "IsVisibleRenglonVendedor2";
        public const string IsVisibleRenglonVendedor3PropertyName = "IsVisibleRenglonVendedor3";
        public const string EnabledComisionRenglonesPropertyName = "EnabledComisionRenglones";     
        public const string FormaDeCalculoDePrecioRenglonFacturaPropertyName = "FormaDeCalculoDePrecioRenglonFactura";
	    #endregion
        #region Propiedades

        public bool InitFirstTime { get; set; }

        public override string ModuleName {
            get { return "2.1.- Facturación"; }
        }

        public bool  BuscarArticuloXSerialAlFacturar {
            get {
                return Model.BuscarArticuloXSerialAlFacturarAsBool;
            }
            set {
                if (Model.BuscarArticuloXSerialAlFacturarAsBool != value) {
                    Model.BuscarArticuloXSerialAlFacturarAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(BuscarArticuloXSerialAlFacturarPropertyName);
                }
            }
        }

        public bool  CambiarFechaEnCuotasLuegoDeFijarFechaEntrega {
            get {
                return Model.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool;
            }
            set {
                if (Model.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool != value) {
                    Model.CambiarFechaEnCuotasLuegoDeFijarFechaEntregaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(CambiarFechaEnCuotasLuegoDeFijarFechaEntregaPropertyName);
                }
            }
        }

        public eComisionesEnFactura  ComisionesEnFactura {
            get {
                return Model.ComisionesEnFacturaAsEnum;
            }
            set {
                if (Model.ComisionesEnFacturaAsEnum != value) {
                    Model.ComisionesEnFacturaAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComisionesEnFacturaPropertyName);
                    RaisePropertyChanged(EnabledComisionRenglonesPropertyName);
                    RaisePropertyChanged(IsVisibleRenglonVendedor1PropertyName);
                    RaisePropertyChanged(IsVisibleRenglonVendedor2PropertyName);
                    RaisePropertyChanged(IsVisibleRenglonVendedor3PropertyName);
                }
            }
        }

        public eComisionesEnRenglones  ComisionesEnRenglones {
            get {
                return Model.ComisionesEnRenglonesAsEnum;
            }
            set {
                if (Model.ComisionesEnRenglonesAsEnum != value) {
                    Model.ComisionesEnRenglonesAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComisionesEnRenglonesPropertyName);
                    RaisePropertyChanged(IsVisibleRenglonVendedor2PropertyName);
                    RaisePropertyChanged(IsVisibleRenglonVendedor3PropertyName);
                }
            }
        }

        public bool  ComplConComodinEnBusqDeArtInv {
            get {
                return Model.ComplConComodinEnBusqDeArtInvAsBool;
            }
            set {
                if (Model.ComplConComodinEnBusqDeArtInvAsBool != value) {
                    Model.ComplConComodinEnBusqDeArtInvAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ComplConComodinEnBusqDeArtInvPropertyName);
                }
            }
        }

        public eTipoDocumentoFactura  DevolucionReversoSeGeneraComo {
            get {
                return Model.DevolucionReversoSeGeneraComoAsEnum;
            }
            set {
                if (Model.DevolucionReversoSeGeneraComoAsEnum != value) {
                    Model.DevolucionReversoSeGeneraComoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(DevolucionReversoSeGeneraComoPropertyName);
                }
            }
        }

        public bool  ExigirRifdeClienteAlEmitirFactura {
            get {
                return Model.ExigirRifdeClienteAlEmitirFacturaAsBool;
            }
            set {
                if (Model.ExigirRifdeClienteAlEmitirFacturaAsBool != value) {
                    Model.ExigirRifdeClienteAlEmitirFacturaAsBool = value;
                    if(!Model.ExigirRifdeClienteAlEmitirFacturaAsBool) {
                        LibMessages.MessageBox.Warning(this, "De no imprimir el Número de RIF del Cliente en la Factura \n" + "incurrirá en un incumplimiento de Deberes Formales, \n" + "que acarrea sanciones por parte de la Administración Tributaria.", string.Empty);
                    }                    
                    IsDirty = true;
                    RaisePropertyChanged(ExigirRifdeClienteAlEmitirFacturaPropertyName);
                }
            }
        }

        public eItemsMontoFactura  ItemsMonto {
            get {
                return Model.ItemsMontoAsEnum;
            }
            set {
                if (Model.ItemsMontoAsEnum != value) {
                    Model.ItemsMontoAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(ItemsMontoPropertyName);
                }
            }
        }

        public string  NombreVendedorDos {
            get {
                return Model.NombreVendedorDos;
            }
            set {
                if (Model.NombreVendedorDos != value) {
                    Model.NombreVendedorDos = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreVendedorDosPropertyName);
                }
            }
        }

        public string  NombreVendedorTres {
            get {
                return Model.NombreVendedorTres;
            }
            set {
                if (Model.NombreVendedorTres != value) {
                    Model.NombreVendedorTres = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreVendedorTresPropertyName);
                }
            }
        }

        public string  NombreVendedorUno {
            get {
                return Model.NombreVendedorUno;
            }
            set {
                if (Model.NombreVendedorUno != value) {
                    Model.NombreVendedorUno = value;
                    IsDirty = true;
                    RaisePropertyChanged(NombreVendedorUnoPropertyName);
                }
            }
        }

        public int  NumFacturasManualesFaltantes {
            get {
                return Model.NumFacturasManualesFaltantes;
            }
            set {
                if (Model.NumFacturasManualesFaltantes != value) {
                    Model.NumFacturasManualesFaltantes = value;
                    IsDirty = true;
                    RaisePropertyChanged(NumFacturasManualesFaltantesPropertyName);
                }
            }
        }

        public bool  PedirInformacionLibroVentasXlsalEmitirFactura {
            get {
                return Model.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool;
            }
            set {
                if (Model.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool != value) {
                    Model.PedirInformacionLibroVentasXlsalEmitirFacturaAsBool = value;


                    IsDirty = true;
                    RaisePropertyChanged(PedirInformacionLibroVentasXlsalEmitirFacturaPropertyName);
                }
            }
        }

        public bool  PermitirFacturarConCantidadCero {
            get {
                return Model.PermitirFacturarConCantidadCeroAsBool;
            }
            set {
                if (Model.PermitirFacturarConCantidadCeroAsBool != value) {
                    Model.PermitirFacturarConCantidadCeroAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(PermitirFacturarConCantidadCeroPropertyName);
                }
            }
        }

        public bool  ResumenVtasAfectaInventario {
            get {
                return Model.ResumenVtasAfectaInventarioAsBool;
            }
            set {
                if (Model.ResumenVtasAfectaInventarioAsBool != value) {
                    Model.ResumenVtasAfectaInventarioAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(ResumenVtasAfectaInventarioPropertyName);
                }
            }
        }

        public bool  SugerirNumeroControlFactura {
            get {
                return Model.SugerirNumeroControlFacturaAsBool;
            }
            set {
                if (Model.SugerirNumeroControlFacturaAsBool != value) {
                    Model.SugerirNumeroControlFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(SugerirNumeroControlFacturaPropertyName);
                }
            }
        }

        public eTipoDeNivelDePrecios  TipoDeNivelDePrecios {
            get {
                return Model.TipoDeNivelDePreciosAsEnum;
            }
            set {
                if (Model.TipoDeNivelDePreciosAsEnum != value) {
                    Model.TipoDeNivelDePreciosAsEnum = value;
                    IsDirty = true;
                    RaisePropertyChanged(TipoDeNivelDePreciosPropertyName);
                    LibMessages.Notification.Send<eTipoDeNivelDePrecios>(Model.TipoDeNivelDePreciosAsEnum, TipoDeNivelDePreciosPropertyName);
                }
            }
        }

        public bool  UsaPrecioSinIva {
            get {
                return Model.UsaPrecioSinIvaAsBool;
            }
            set {
                if (Model.UsaPrecioSinIvaAsBool != value) {
                    Model.UsaPrecioSinIvaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaPrecioSinIvaPropertyName);
                    LibMessages.Notification.Send<bool>(Model.UsaPrecioSinIvaAsBool, UsaPrecioSinIvaPropertyName);
                }
            }
        }

        public bool  UsaPrecioSinIvaEnResumenVtas {
            get {
                return Model.UsaPrecioSinIvaEnResumenVtasAsBool;
            }
            set {
                if (Model.UsaPrecioSinIvaEnResumenVtasAsBool != value) {
                    Model.UsaPrecioSinIvaEnResumenVtasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsaPrecioSinIvaEnResumenVtasPropertyName);
                }
            }
        }

        public bool  UsarRenglonesEnResumenVtas {
            get {
                return Model.UsarRenglonesEnResumenVtasAsBool;
            }
            set {
                if (Model.UsarRenglonesEnResumenVtasAsBool != value) {
                    Model.UsarRenglonesEnResumenVtasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarRenglonesEnResumenVtasPropertyName);
                }
            }
        }

        public bool  UsarResumenDiarioDeVentas {
            get {
                return Model.UsarResumenDiarioDeVentasAsBool;
            }
            set {
                if (Model.UsarResumenDiarioDeVentasAsBool != value) {
                    Model.UsarResumenDiarioDeVentasAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(UsarResumenDiarioDeVentasPropertyName);
                }
            }
        }

        

        public bool  VerificarFacturasManualesFaltantes {
            get {
                return Model.VerificarFacturasManualesFaltantesAsBool;
            }
            set {
                if (Model.VerificarFacturasManualesFaltantesAsBool != value) {
                    Model.VerificarFacturasManualesFaltantesAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(VerificarFacturasManualesFaltantesPropertyName);
                }
            }
        }

        public bool  PermitirCambioTasaMondExtrajalEmitirFactura {
            get {
                return Model.PermitirCambioTasaMondExtrajalEmitirFacturaAsBool;
            }
            set {
                if (Model.PermitirCambioTasaMondExtrajalEmitirFacturaAsBool != value) {
                    Model.PermitirCambioTasaMondExtrajalEmitirFacturaAsBool = value;
                    IsDirty = true;
                    RaisePropertyChanged(PermitirCambioTasaMondExtrajalEmitirFacturaPropertyName);
                }
            }
        }

        public eFormaDeCalculoDePrecioRenglonFactura  FormaDeCalculoDePrecioRenglonFactura {
            get {
                return Model.FormaDeCalculoDePrecioRenglonFacturaAsEnum;
            }
            set {
                if (Model.FormaDeCalculoDePrecioRenglonFacturaAsEnum != value) {
                    Model.FormaDeCalculoDePrecioRenglonFacturaAsEnum = value;                   
                    RaisePropertyChanged(FormaDeCalculoDePrecioRenglonFacturaPropertyName);
                }
            }
        }

        public eComisionesEnFactura[] ArrayComisionesEnFactura {
            get {
                return LibEnumHelper<eComisionesEnFactura>.GetValuesInArray();
            }
        }

        public eComisionesEnRenglones[] ArrayComisionesEnRenglones {
            get {
                return LibEnumHelper<eComisionesEnRenglones>.GetValuesInArray();
            }
        }

        public eTipoDocumentoFactura[] ArrayTipoReverso {
            get {
                return (from eTipoDocumentoFactura e in LibEnumHelper.GetValuesInEnumeration(typeof(eTipoDocumentoFactura)) where (e.Equals(eTipoDocumentoFactura.Factura) || e.Equals(eTipoDocumentoFactura.NotaDeCredito)) select e).ToArray();
            }
        }

        public eItemsMontoFactura[] ArrayItemsMontoFactura {
            get {
                return LibEnumHelper<eItemsMontoFactura>.GetValuesInArray();
            }
        }

        public eTipoDeNivelDePrecios[] ArrayTipoDeNivelDePrecios {
            get {
                return LibEnumHelper<eTipoDeNivelDePrecios>.GetValuesInArray();
            }
        }
		
        public eFormaDeCalculoDePrecioRenglonFactura[] ArrayFormaDeCalculoDePrecioRenglonFactura {
            get {
                return LibEnumHelper<eFormaDeCalculoDePrecioRenglonFactura>.GetValuesInArray();
            }
        }
		
        public bool IsEnabledUsaPrecioSinIva {
            get {
                if (LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros", "SesionEspecialPrecioSinIva"))) {
                    return false;
                } else {
                    return (IsEnabled && InitFirstTime) || (IsEnabled && AppMemoryInfo.GlobalValuesGetBool("Parametros", "SesionEspecialPrecioSinIva"));
                }
            }
        }

        public bool IsVisibleArticuloSerial {
            get {
                if(LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros", "HayArticuloSerial"))) {
                    return false;
                } else {
                    return AppMemoryInfo.GlobalValuesGetBool("Parametros", "HayArticuloSerial");
                }
            }
        }

        public bool IsVisibleRenglonVendedor1 {
            get {
                return EnabledComisionRenglones;
            }
        }

        public bool IsVisibleRenglonVendedor2 {
            get {
                if (EnabledComisionRenglones && (Model.ComisionesEnRenglonesAsEnum == eComisionesEnRenglones.PordosVendedores ||
                    Model.ComisionesEnRenglonesAsEnum == eComisionesEnRenglones.PorTresVendedores)) {
                    return true;
                }else{
                    return false;
                }
            }
        }
        
        public bool IsVisibleRenglonVendedor3 {
            get {
                if(EnabledComisionRenglones && Model.ComisionesEnRenglonesAsEnum == eComisionesEnRenglones.PorTresVendedores) {
                    return true;
                }else{
                    return false;
                }
            }
        }

        public bool EnabledComisionRenglones {
            get {
                return (Model.ComisionesEnFacturaAsEnum != eComisionesEnFactura.SobreTotalFactura);
            }
        }

        public bool IsEnabledUsaPrecioSinIvaEnResumenVtas {
            get {
                if (LibString.IsNullOrEmpty(AppMemoryInfo.GlobalValuesGetString("Parametros", "SesionEspecialPrecioSinIva"))) {
                    return false;
                } else {
                    return (IsEnabled && InitFirstTime) || (IsEnabled && AppMemoryInfo.GlobalValuesGetBool("Parametros", "SesionEspecialPrecioSinIva"));
                }
            }
        }

        public string PromptIVA {
           get {
              return string.Format("Usa Precio Sin {0}.............................................", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptIVA"));
           }
        }

        public string PromptRIF {
           get {
              return string.Format("Nombre, Número de {0}, Teléfono, Dirección.", AppMemoryInfo.GlobalValuesGetString("Parametros", "PromptRIF"));
           }
        }

        #endregion //Propiedades
        #region Constructores
        public FacturaFacturacionViewModel()
            : this(new FacturacionStt(), eAccionSR.Insertar, true) {
        }
        public FacturaFacturacionViewModel(FacturacionStt initModel, eAccionSR initAction, bool firstTime)
            : base(initModel, initAction, LibGlobalValues.Instance.GetAppMemInfo(), LibGlobalValues.Instance.GetMfcInfo()) {
                InitFirstTime = firstTime;
                LibMessages.Notification.Register<bool>(this, OnBooleanParametrosComunesChanged);
                LibMessages.Notification.Register<eTipoDeNivelDePrecios>(this, OnTipoDeNivelDePreciosChanged);
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override void InitializeLookAndFeel(FacturacionStt valModel) {
            base.InitializeLookAndFeel(valModel);
        }

        protected override FacturacionStt FindCurrentRecord(FacturacionStt valModel) {
            if(valModel == null) {
                return new FacturacionStt();
            }
            return valModel;
        }

        protected override ILibBusinessComponentWithSearch<IList<FacturacionStt>, IList<FacturacionStt>> GetBusinessComponent() {
            return null;
        }
        
        private void OnBooleanParametrosComunesChanged(NotificationMessage<bool> valMessage) {
            try {
                if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), UsaPrecioSinIvaPropertyName)) {
                    UsaPrecioSinIva = valMessage.Content;
                }

            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        private void OnTipoDeNivelDePreciosChanged(NotificationMessage<eTipoDeNivelDePrecios> valMessage) {
            try {
                if (LibString.S1IsEqualToS2(LibConvert.ToStr(valMessage.Notification), TipoDeNivelDePreciosPropertyName)) {
                    TipoDeNivelDePrecios = valMessage.Content;
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, ModuleName);
            }
        }

        public bool isVisibleParaPeru {
            get {
                bool vResult = true;
                if (LibDefGen.ProgramInfo.IsCountryPeru()) {
                    vResult = false;
                }
                return vResult;
            }
        }

        public bool IsEnabledFormaDeCalculoDePrecioRenglonFactura {
            get {
                return IsEnabled && LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "EsPrimeraVezCompania");
            }
        }
        #endregion //Metodos Generados
    } //End of class FacturaFacturacionViewModel

} //End of namespace Galac.Saw.Uil.SttDef

