using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Banco;
using Galac.Adm.Brl.Banco;
using Galac.Adm.Uil.Banco.ViewModel;
using LibGalac.Aos.UI.Mvvm.Messaging;
using System.Xml.Linq;

namespace Galac.Adm.Uil.Banco {
	public class clsTransferenciaEntreCuentasBancariasMenu : ILibMenuMultiFile {
		#region Variables
		string _ViewModelStr;
		
		#endregion //Variables
		#region Propiedades        

		public string ViewModelStr {
			get { return _ViewModelStr; }
			private set { _ViewModelStr = value; }
		}
		#endregion //Propiedades
		#region Metodos Generados
		void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
			if (valAction == eAccionSR.Contabilizar) {
				ViewModelStr = EjecutaStr(valAction, valUseInterop != 0);
			} else {
				TransferenciaEntreCuentasBancariasMngViewModel vViewModel = new TransferenciaEntreCuentasBancariasMngViewModel();
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
			return LibFKRetrievalHelper.ChooseRecord<FkTransferenciaEntreCuentasBancariasViewModel>("Transferencia entre Cuentas Bancarias", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsTransferenciaEntreCuentasBancariasNav());
		}
		string EjecutaStr(eAccionSR valAction, bool valUseInterop) {
			string vResult = string.Empty;
			TransferenciaEntreCuentasBancariasViewModel vViewModel = new TransferenciaEntreCuentasBancariasViewModel(valAction);
			vViewModel.InitializeViewModel(valAction);            
			LibMessages.EditViewModel.ShowEditor(vViewModel, valUseInterop);
            if (vViewModel.DialogResult) {
                XElement vXmlResult = new XElement("Datos",
                    new XElement("Documento",
                        new XElement("Fecha", LibConvert.ToStr(vViewModel.Fecha, "dd/MM/yyyy"))
                        , new XElement("NumeroDocumento", vViewModel.NumeroDocumento)
                        , new XElement("Consecutivo", vViewModel.Consecutivo)
                        , new XElement("CodigoCuentaBancariaOrigen", vViewModel.CodigoCuentaBancariaOrigen)
                        , new XElement("CodigoCuentaBancariaDestino", vViewModel.CodigoCuentaBancariaDestino)
                        , new XElement("FechaDeAnulacion", LibConvert.ToStr(vViewModel.FechaDeAnulacion, "dd/MM/yyyy"))
                        , new XElement("Status", LibConvert.ToStr(vViewModel.Status))
                        , new XElement("Descripcion", LibConvert.ToStr(vViewModel.Descripcion))
                        , new XElement("CodigoConceptoEgreso", LibConvert.ToStr(vViewModel.CodigoConceptoEgreso))
                        , new XElement("GeneraComisionEgreso", LibConvert.ToStr(vViewModel.GeneraComisionEgreso))
                        , new XElement("CodigoConceptoComisionEgreso", LibConvert.ToStr(vViewModel.CodigoConceptoComisionEgreso))
                        , new XElement("GeneraIGTFComisionEgreso", LibConvert.ToStr(vViewModel.GeneraIGTFComisionEgreso))
                        , new XElement("CodigoConceptoIngreso", LibConvert.ToStr(vViewModel.CodigoConceptoIngreso))
                        , new XElement("GeneraComisionIngreso", LibConvert.ToStr(vViewModel.GeneraComisionIngreso))
                        , new XElement("CodigoConceptoComisionIngreso", LibConvert.ToStr(vViewModel.CodigoConceptoComisionIngreso))
                        , new XElement("GeneraIGTFComisionIngreso", LibConvert.ToStr(vViewModel.GeneraIGTFComisionIngreso))
                        , new XElement("CambioABolivaresEgreso", LibConvert.NumToString(vViewModel.CambioABolivaresEgreso, 2))
                        , new XElement("MontoTransferenciaEgreso", LibConvert.NumToString(vViewModel.MontoTransferenciaEgreso, 2))
                        , new XElement("MontoComisionEgreso", LibConvert.NumToString(vViewModel.MontoComisionEgreso, 2))
                        , new XElement("MontoTransferenciaIngreso", LibConvert.NumToString(vViewModel.MontoTransferenciaIngreso, 2))
                        , new XElement("MontoComisionIngreso", LibConvert.NumToString(vViewModel.MontoComisionIngreso, 2))
                        , new XElement("CambioABolivaresIngreso", LibConvert.NumToString(vViewModel.CambioABolivaresIngreso, 2))
                        ));
                vResult = vXmlResult.ToString();
            }
            return vResult;
		}
		#endregion //Metodos Generados

	} //End of class clsTransferenciaEntreCuentasBancariasMenu

} //End of namespace Galac.Adm.Uil.Banco

