using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras {
    public class clsTablaRetencionMenu: ILibMenu {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsTablaRetencionMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenu
        void ILibMenu.Ejecuta(eAccionSR valAction, int valUseInterop) {
            if (valAction == eAccionSR.Insertar) {
                clsTablaRetencionIpl insTablaRetencion = new clsTablaRetencionIpl();
                frmTablaRetencionInput insFrmInput = new frmTablaRetencionInput("Tabla Retencion", eAccionSR.Insertar, "");
                insFrmInput.InitLookAndFeelAndSetValues(insTablaRetencion.ListTablaRetencion, new clsTablaRetencionIpl());
                if (valUseInterop ==0) {
                    insFrmInput.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmInput.Show();
                } else {
                    insFrmInput.ShowDialog();
                }
            } else {
                LibFrmSearch insFrmSearch = new LibFrmSearch("Tabla Retención", new Galac.Adm.Uil.GestionCompras.Sch.GSTablaRetencionSch());
                insFrmSearch.DbQuery = "Adm.Gp_TablaRetencionSCH";
                insFrmSearch.LeaveOpenOnSelect = true;
                insFrmSearch.Entity = new clsTablaRetencionList();
                insFrmSearch.CurrentAction = valAction;
                if (valUseInterop ==0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
                }
            }
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados


    } //End of class clsTablaRetencionMenu

} //End of namespace Galac.Adm.Uil.GestionCompras

