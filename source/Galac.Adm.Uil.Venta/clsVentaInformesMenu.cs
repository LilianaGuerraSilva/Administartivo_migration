using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Adm.Uil.Venta.Reportes;
using Galac.Saw.Lib;
using LibGalac.Aos.UI.Mvvm;

namespace Galac.Adm.Uil.Venta {

    public class clsVentaInformesMenu : ILibMenu {
        #region Variables
        #endregion //Variables
        #region Metodos Generados       
        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            eSystemModules vSystemModule = (eSystemModules)valUseInterop;
            LibReportsViewModel _LibReportsViewModel = null;
            switch (vSystemModule) {
                case eSystemModules.Cobranza: _LibReportsViewModel = new clsCobranzaInformesViewModel(); break;
                case eSystemModules.Caja: _LibReportsViewModel = new clsCajaInformesViewModel(); break;
                case eSystemModules.NotaDeEntrega: _LibReportsViewModel = new clsNotaDeEntregaInformesViewModel(); break;
                case eSystemModules.CxC: _LibReportsViewModel = new clsCxCInformesViewModel(); break;
                case eSystemModules.Factura: _LibReportsViewModel = new clsFacturaInformesViewModel(); break;
                case eSystemModules.ResumenDiarioVentas: _LibReportsViewModel = new clsResumenDiarioDeVentasViewModel(); break;
                case eSystemModules.Contrato: _LibReportsViewModel = new clsContratoInformesViewModel(); break;
            }
            
            if (LibMessages.ReportsView.ShowReportsView(_LibReportsViewModel, false)) {
            }
        }
        #endregion //Metodos Generados
    } //End of class clsVentaInfromesMenu
} //End of namespace Galac.Adm.Uil.Venta