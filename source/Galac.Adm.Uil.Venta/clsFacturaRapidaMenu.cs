using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Catching;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Brl.Venta;
using Galac.Adm.Uil.Venta.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Adm.Ccl.DispositivosExternos;
using Galac.Adm.Brl.DispositivosExternos.ImpresoraFiscal;
using System.Xml.Linq;
using Galac.Adm.Uil.DispositivosExternos.ViewModel;
using Galac.Adm.Uil.Venta.Reportes;

namespace Galac.Adm.Uil.Venta {

    public class clsFacturaRapidaMenu:ILibMenu {
        #region Variables
        #endregion //Variables

        #region Metodos Generados
        void ILibMenu.Ejecuta(eAccionSR valAction,int valUseInterop) {
            BalanzaTomarPesoViewModel vBalanzaTomarPesoViewModel = null;
            bool vUsarBalanza = false;
            bool vBalanzaIsValid = InitializeBalanza(ref vBalanzaTomarPesoViewModel,ref vUsarBalanza);
            bool parametrosBancarios = SeDefinieronParametrosBancarios();
            if(vBalanzaIsValid) {
                if(parametrosBancarios) {
                    FacturaRapidaViewModel vViewModel = new FacturaRapidaViewModel();
                    vViewModel.InitializeViewModel(eAccionSR.Insertar);
                    if(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","SeMuestraTotalEnDivisas") 
                        || LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros", "UsaListaDePrecioEnMonedaExtranjera")) {
                        if(!vViewModel.AsignarTasaDeCambioDeMonedaDeCobroYParaMostrarTotales()) {
                            LibMessages.MessageBox.Information(this,"Esta usando la opción de punto de venta y no se ha ingresado la tasa de cambio del día, favor ingrese un cambio válido para continuar","Punto de Venta");
                            return;
                        }
                    }
                    if(vUsarBalanza) {
                        vViewModel.BalanzaTomarPesoViewModel = vBalanzaTomarPesoViewModel;
                        vViewModel.UsaBalanzaEnPOS = vUsarBalanza;
                    }
                    LibMessages.EditViewModel.ShowTopmostEditor(vViewModel);
                } else {
                    LibMessages.MessageBox.Information(this,"No se han definido los parametros bancarios de Cobro Directo, Debe configurar y asignarlos para continuar","");
                }
            }
        }
        

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument,List<LibSearchDefaultValues> valSearchCriteria,List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkFacturaRapidaViewModel>("Punto de Venta",ref refXmlDocument,valSearchCriteria,valFixedCriteria,new clsFacturaRapidaNav());
        }       

        private bool SeDefinieronParametrosBancarios() {
            bool SeDetectaronParametrosBancarios = false;
            SeDetectaronParametrosBancarios = false;
            try {
                string vCodigoCuentaBancaria = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","CuentaBancariaCobroDirecto");
                string vConceptoBancario = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","ConceptoBancarioCobroDirecto");
                if(!LibString.IsNullOrEmpty(vCodigoCuentaBancaria) && !LibString.IsNullOrEmpty(vConceptoBancario)) {
                    SeDetectaronParametrosBancarios = true;
                }
            } catch(GalacException vEx) {
                SeDetectaronParametrosBancarios = false;
                throw vEx;
            }
            return SeDetectaronParametrosBancarios;
        }

        private bool InitializeBalanza(ref BalanzaTomarPesoViewModel valBalanzaTomarPesoViewModel,ref bool refUsaBalanzaEnPOS) {
            bool vResult = false;
            refUsaBalanzaEnPOS = LibConvert.SNToBool(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("FacturaRapida","UsarBalanza"));
            try {
                if(refUsaBalanzaEnPOS && valBalanzaTomarPesoViewModel == null) {
                    valBalanzaTomarPesoViewModel = new BalanzaTomarPesoViewModel();
                    vResult = valBalanzaTomarPesoViewModel.ComprobarEstado();
                    if(!vResult) {
                        if(LibMessages.MessageBox.YesNo(this,"No hay comunicación con la balanza, debe verificar configuración y cableados. Desea continuar sin tomar el peso en los artículos?","")) {
                            refUsaBalanzaEnPOS = vResult;
                            vResult = !vResult;
                        }
                    } else {
                        refUsaBalanzaEnPOS = vResult;
                    }
                } else {
                    return true;
                }
            } catch(GalacException vEx) {
                if(vEx.ExceptionManagementType == eExceptionManagementType.Validation) {
                    if(LibMessages.MessageBox.YesNo(this,vEx.Message + ".\r\nDesea continuar sin tomar el peso en los artículos?","")) {
                        refUsaBalanzaEnPOS = false;
                        vResult = true;
                    }
                } else {
                    LibMessages.MessageBox.Information(this,vEx.Message,"");
                }
            } catch(Exception vEx) {
                LibMessages.MessageBox.Information(this,vEx.Message,"");
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class clsFacturaRapidaMenu
} //End of namespace Galac.Adm.Uil.Venta

