using System.Collections.Generic;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Uil;
using Galac.Adm.Brl.CAnticipo;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.UI.Mvvm;
using Galac.Adm.Uil.CAnticipo.Reportes;

namespace Galac.Adm.Uil.CAnticipo {
    public class clsAnticipoMenu : ILibMenu {
        #region Metodos Generados
        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            if (valAction == eAccionSR.InformesPantalla) {
                LibReportsViewModel _LibReportsViewModel = new clsAnticipoInformesViewModel();
                if (LibMessages.ReportsView.ShowReportsView(_LibReportsViewModel, false)) {
                }
            }
        }
        #endregion //Metodos Generados
    } //End of class clsAnticipoMenu

} //End of namespace Galac.Adm.Uil.CAnticipo

