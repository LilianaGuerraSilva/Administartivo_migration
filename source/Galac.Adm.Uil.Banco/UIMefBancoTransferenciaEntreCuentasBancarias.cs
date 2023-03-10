using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Contracts;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Mvvm.Ribbon;
using Galac.Adm.Uil.Banco.ViewModel;

namespace Galac.Adm.Uil.Banco {
	[LibMefUilComponentMetadata(typeof(UIMefBancoTransferenciaEntreCuentasBancarias), "Bancos")]
	public class UIMefBancoTransferenciaEntreCuentasBancarias : ILibMefUilComponent {
		#region Variables
		private TransferenciaEntreCuentasBancariasMngViewModel _ViewModel;
		private ContentControl _View;
		#endregion //Variables

		#region Propiedades
		public string Name {
			get { return "Transferencia entre Cuentas Bancarias"; }
		}

		public Uri Image {
			get { return null; }
		}

		public ObservableCollection<LibRibbonTabData> RibbonTabData {
			get {
				if (_ViewModel != null) {
					return _ViewModel.RibbonData.TabDataCollection;
				} else {
					return null;
				}
			}
		}

		public ContentControl View {
			get {
				if (_View == null) {
					_View = new GSSearchView() {
						DataContext = _ViewModel
					};
				}
				return _View;
			}
		}

		public bool IsInitialized {
			get;
			private set;
		}

		public LibXmlMemInfo AppMemoryInfo { get; set; }

		public LibXmlMFC Mfc { get; set; }
		#endregion //Propiedades

		#region Constructores
		public UIMefBancoTransferenciaEntreCuentasBancarias() {
		}
		#endregion //Constructores

		#region Metodos Generados
		public void InitializeIfNecessary() {
			try {
				if (!IsInitialized) {
					IsInitialized = true;
					_ViewModel = new TransferenciaEntreCuentasBancariasMngViewModel();
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
			}
		}

		public void Reload() {
			try {
				InitializeIfNecessary();
				if (_ViewModel != null) {
					_ViewModel.ExecuteSearchAndInitLookAndFeel();
				}
			} catch (AccessViolationException) {
				throw;
			} catch (Exception vEx) {
				LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx, Name);
			}
		}
		#endregion //Metodos Generados

	} //End of class UIMefBancoTransferenciaEntreCuentasBancarias

} //End of namespace Galac.Adm.Uil.Banco

