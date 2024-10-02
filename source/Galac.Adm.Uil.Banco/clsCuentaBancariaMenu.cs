using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Uil.Banco.ViewModel;
using Galac.Adm.Brl.Banco;

namespace Galac.Adm.Uil.Banco {
	public class clsCuentaBancariaMenu : ILibMenuMultiFile {
		#region Variables
		#endregion //Variables

		#region Propiedades
		#endregion //Propiedades

		#region Constructores
		public clsCuentaBancariaMenu() {
		}
		#endregion //Constructores

		#region Metodos Generados
		void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
			try {
				CuentaBancariaMngViewModel vViewModel = new CuentaBancariaMngViewModel();
				vViewModel.ExecuteSearchAndInitLookAndFeel();
				LibSearchView insFrmSearch = new LibSearchView(vViewModel);
                if (valUseInterop == 0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else if (valUseInterop == 1) {
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
				}
			} catch (Exception) {
				throw;
			}
		}

		public static bool ChooseCuentaBancariaFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
			return LibFKRetrievalHelper.ChooseRecord<FkCuentaBancariaViewModel>("Cuenta Bancaria", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsCuentaBancariaNav());
		}
		#endregion //Metodos Generados

	} //End of class clsCuentaBancariaMenu

} //End of namespace Galac.Adm.Uil.Banco