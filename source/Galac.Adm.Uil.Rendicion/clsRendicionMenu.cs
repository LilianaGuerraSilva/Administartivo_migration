using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.CajaChica;
using Galac.Adm.Brl.CajaChica;
using Galac.Adm.Uil.CajaChica.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System.Xml.Linq;

namespace Galac.Adm.Uil.CajaChica {
    public class clsRendicionMenu : ILibMenuMultiFile {
        #region Variables
        string _ViewModelStr;
        #endregion //Variables
        #region Propiedades        

        public string ViewModelStr {
            get { return _ViewModelStr; }
            private set { _ViewModelStr = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRendicionMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibMenuMultiFile
        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
            if (valAction == eAccionSR.Cerrar || valAction == eAccionSR.Anular || valAction == eAccionSR.ReImprimir || valAction == eAccionSR.Contabilizar) {
                ViewModelStr = EjecutaStr(valAction, valUseInterop != 0);                
            } else {
                RendicionMngViewModel vViewModel = new RendicionMngViewModel();
                vViewModel.ExecuteSearchAndInitLookAndFeel();
                LibSearchView insFrmSearch = new LibSearchView(vViewModel);
                if (valUseInterop == 0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
                }
            }
        }

        public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            return LibFKRetrievalHelper.ChooseRecord<FkRendicionViewModel>("Reposición de Caja Chica", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsRendicionNav());
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados



        string EjecutaStr(eAccionSR valAction, bool valUseInterop) {
            string vResult = string.Empty;
            RendicionViewModel vViewModel = new RendicionViewModel(valAction);
            vViewModel.InitializeViewModel(valAction);
            LibMessages.EditViewModel.ShowEditor(vViewModel, valUseInterop);
            if (vViewModel.DialogResult) {
                XElement vXmlResult = new XElement("Datos",
                    new XElement("Documento",
                        new XElement("Fecha", LibConvert.ToStr(vViewModel.FechaApertura, "dd/MM/yyyy"))
                        , new XElement("Numero", vViewModel.Numero)
                        , new XElement("Consecutivo", vViewModel.Consecutivo)
                        , new XElement("CodigoCuentaCajaChica", vViewModel.CodigoCtaBancariaCajaChica)
                        , new XElement("CodigoCuentaBancariaDeReposicion", vViewModel.NombreCuentaBancaria)
                        , new XElement("FechaAnulacion", LibConvert.ToStr(vViewModel.FechaAnulacion, "dd/MM/yyyy"))
                        , new XElement("FechaCierre", LibConvert.ToStr(vViewModel.FechaCierre, "dd/MM/yyyy"))
                        , new XElement("MontoCheque", LibConvert.NumToString(vViewModel.TotalGastos,2) )
                        , new XElement("BeneficiarioCheque", vViewModel.BeneficiarioCheque)
                        ));
                vResult = vXmlResult.ToString();
            }
            return vResult;
        }

    } //End of class clsRendicionesMenu

} //End of namespace Galac.Saw.Uil.Rendicion

