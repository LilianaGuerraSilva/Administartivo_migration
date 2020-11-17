using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Controls ;
using System.Windows.Controls.Primitives ;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Uil;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.CajaChica {
#else
namespace Galac.Saw.Wrp.CajaChica {
#endif


    public class wrpDetalleDeRendicion: IWrpMfCs {
        #region Variables
        string _Title = "Detalle De Rendicion";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpMfCs

        void IWrpMfCs.Execute(string vfwAction, string vfwCurrentMfc, string vfwCurrentParameters) {
            //try {
            //    LibGlobalValues insGV = CreateGlobalValues(vfwCurrentMfc, vfwCurrentParameters);
            //    ILibMenuMultiFile insMenu = new Galac.Saw.Uil.Rendicion.clsDetalleDeRendicionMenu();
            //    insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1, insGV.GVDictionary);
            //} catch (GalacException gEx) {
            //    LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            //} catch (Exception vEx) {
            //    if (vEx is AccessViolationException) {
            //        throw;
            //    }
            //    LibExceptionDisplay.Show(vEx);
            //}
        }

        string IWrpMfCs.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            //string vResult = "";
            //LibSearch insLibSearch = new LibSearch();
            //List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            //List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            //try {
            //    vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
            //    vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
            //    System.Xml.XmlDocument vXmlDocument = null;
            //    if (Galac.Saw.Uil.Rendicion.clsDetalleDeRendicionList.ChooseDetalleDeRendicion(null, ref vXmlDocument, vSearchValues, vFixedValues)) {
            //        vResult = vXmlDocument.InnerXml;
            //    }
            //    return vResult;
            //} catch (GalacException gEx) {
            //    LibExceptionDisplay.Show(gEx, null, Title +  " - Escoger");
            //} catch (Exception vEx) {
            //    if (vEx is AccessViolationException) {
            //        throw;
            //    }
            //    LibExceptionDisplay.Show(vEx);
            //}
            return "";
        }
        #endregion //Miembros de IWrpMfCs

        private LibGlobalValues CreateGlobalValues(string valCurrentMfc, string valCurrentParameters) {
            LibGlobalValues insGV = new LibGlobalValues();
            insGV.LoadCompleteAppMemInfo(valCurrentParameters);
            ((LibXmlMFC)insGV.GVDictionary[LibGlobalValues.NameMFCInfo]).Add("Compania",LibConvert.ToInt(valCurrentMfc));
            return insGV;
        }
        #endregion //Metodos Generados


    } //End of class wrpDetalleDeRendicion

} //End of namespace Galac.Saw.Wrp.Rendicion

