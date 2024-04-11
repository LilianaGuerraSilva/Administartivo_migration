using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Adm.Brl.CAnticipo;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Uil.CAnticipo.Reportes;
using Galac.Saw.Lib;

namespace Galac.Adm.Uil.CAnticipo {
    public class clsAnticipoMenu : ILibMenu {
        #region Metodos Generados
        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            eSystemModules vSystemModule = (eSystemModules)valUseInterop;
            if (valAction == eAccionSR.InformesPantalla) {
                bool vEsClienteOProveedor = (vSystemModule == eSystemModules.CxC);
                LibReportsViewModel _LibReportsViewModel = new clsAnticipoInformesViewModel(vEsClienteOProveedor);
                if (LibMessages.ReportsView.ShowReportsView(_LibReportsViewModel, false)) {
                }
            }
        }
        #endregion //Metodos Generados
    } //End of class clsAnticipoMenu

} //End of namespace Galac.Adm.Uil.CAnticipo

