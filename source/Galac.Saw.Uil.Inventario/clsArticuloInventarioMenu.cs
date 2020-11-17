using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Inventario;
using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using Galac.Saw.Uil.Inventario.Reportes;
using LibGalac.Aos.UI.Mvvm.Messaging;

namespace Galac.Saw.Uil.Inventario {
    public class clsArticuloInventarioMenu:ILibMenu {
        #region Metodos Generados

        void ILibMenu.Ejecuta(eAccionSR valAction,int valUseInterop) {
            switch(valAction) {
            case eAccionSR.InformesPantalla:
                clsArticuloInventarioInformesViewModel vInformeViewModel = new clsArticuloInventarioInformesViewModel();
                if(LibMessages.ReportsView.ShowReportsView(vInformeViewModel,true)) {
                }
                break;
            default:
                ArticuloInventarioMngViewModel vViewModel = new ArticuloInventarioMngViewModel();
                vViewModel.ExecuteSearchAndInitLookAndFeel();
                LibSearchView insFrmSearch = new LibSearchView(vViewModel);
                if(valUseInterop == 0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
                }
                break;
            }

        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument,List<LibSearchDefaultValues> valSearchCriteria,List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario",ref refXmlDocument,valSearchCriteria,valFixedCriteria,new clsArticuloInventarioNav());
        }

        public void MostrarPantallaDeImportarPreciosDesdeArchivo() {
            ImportarPreciosDeArticuloViewModel vViewModel = new ImportarPreciosDeArticuloViewModel();
            LibMessages.EditViewModel.ShowEditor(vViewModel, true);
        }

        #endregion //Metodos Generados


    } //End of class clsArticuloInventarioMenu

} //End of namespace Galac.Saw.Uil.Inventario

