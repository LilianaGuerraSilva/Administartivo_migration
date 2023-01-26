using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using LibGalac.Aos.Vbwa;
using System.Xml.Linq;
using Galac.Saw.Wrp.TransferenciaEntreCuentasBancarias;

namespace Galac.Saw.Wrp.TransferenciaEntreCuentasBancarias {

	[ClassInterface(ClassInterfaceType.None)]

	public class wrpTransferenciaEntreCuentasBancarias : System.EnterpriseServices.ServicedComponent, IWrpTransferenciaEntreCuentasBancariasVb {
		#region Variables
		string _Title = "Transferencia entre Cuentas Bancarias";
		#endregion //Variables

		#region Propiedades
		private string Title {
			get { return _Title; }
		}
		#endregion //Propiedades

		#region Metodos Generados

		#region Miembros de IWrpMfVb
		void IWrpTransferenciaEntreCuentasBancariasVb.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
			try {
				LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc,vfwCurrentParameters);
				ILibMenuMultiFile insMenu = new Galac.Adm.Uil.Banco.clsTransferenciaEntreCuentasBancariasMenu();
				insMenu.Ejecuta((eAccionSR) new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
			} catch (GalacException gEx) {
				LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
			} catch (Exception vEx) {
				if (vEx is AccessViolationException) {
					throw;
				}
				LibExceptionDisplay.Show(vEx);
			}
		}

		string IWrpTransferenciaEntreCuentasBancariasVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
			string vResult = "";
			LibSearch insLibSearch = new LibSearch();
			List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
			List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
			try {
				vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
				vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
				System.Xml.XmlDocument vXmlDocument = null;
				if (Adm.Uil.Banco.clsTransferenciaEntreCuentasBancariasMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
					vResult = vXmlDocument.InnerXml;
				}
				return vResult;
			} catch (GalacException gEx) {
				LibExceptionDisplay.Show(gEx, null, Title + " - Escoger");
			} catch (Exception vEx) {
				if (vEx is AccessViolationException) {
					throw;
				}
				LibExceptionDisplay.Show(vEx);
			}
			return "";
		}

		void IWrpTransferenciaEntreCuentasBancariasVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
			try {
				LibWrp.SetAppConfigToCurrentDomain(vfwPath);
				LibGalac.Aos.Vbwa.LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
			} catch (Exception vEx) {
				if (vEx is AccessViolationException) {
					throw;
				}
				throw new GalacWrapperException(Title + " - Inicializar", vEx);
			}
		}

		void IWrpTransferenciaEntreCuentasBancariasVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
			try {
				string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
				LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials, vfwProgramVersion, vfwDbVersion, LibConvert.ToDate(vfwStrDateOfVersion), vfwStrHourOfVersion, "", vfwCountry, LibConvert.ToInt(vfwCMTO));
				LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("", vLogicUnitDir, LibApp.AppPath(), LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
			} catch (Exception vEx) {
				if (vEx is AccessViolationException) {
					throw;
				}
				throw new GalacWrapperException(Title + " - Inicializar", vEx);
			}
		}
		
		void IWrpTransferenciaEntreCuentasBancariasVb.InitializeContext(string vfwInfo) {
			try {
				LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
			} catch (Exception vEx) {
				if (vEx is AccessViolationException) {
					throw;
				}
				throw new GalacWrapperException(Title + " - Inicialización", vEx);
			}
		}
		#endregion //Miembros de IWrpMfVb

		private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
			LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
			LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibConvert.ToInt(valCurrentMfc));
			LibGlobalValues.Instance.LoadMFCInfoFromAppMemInfo("Periodo", "Consecutivo");
			//LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
			return LibGlobalValues.Instance;
		}
		#endregion //Metodos Generados

		string IWrpTransferenciaEntreCuentasBancariasVb.ExecuteAndReturnValue(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
			string vResult = String.Empty;
			LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
			ILibMenuMultiFile insMenu = new Galac.Adm.Uil.Banco.clsTransferenciaEntreCuentasBancariasMenu();
			insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
			vResult = ((Galac.Adm.Uil.Banco.clsTransferenciaEntreCuentasBancariasMenu)insMenu).ViewModelStr;
			return vResult;
		}
	} //End of class wrpTransferenciaEntreCuentasBancarias

} //End of namespace Galac.Saw.Wrp.Banco

