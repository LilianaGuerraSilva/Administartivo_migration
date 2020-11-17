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
namespace Galac.SawBsF.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif


    public class wrpFacturaRapidaDetalle: IWrpMfCs {
#region Variables
        string _Title = "Punto de Venta Detalle";
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
            try {
                CreateGlobalValues(vfwCurrentParameters);
                //ILibMenu insMenu = new Galac.Adm.Uil.Venta.clsFacturaRapidaDetalleMenu();
                //insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpMfCs.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                //System.Xml.XmlDocument vXmlDocument = null;
                //if (Galac.Adm.Uil.Venta.clsFacturaRapidaDetalleMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                //    vResult = vXmlDocument.InnerXml;
                //}
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title +  " - Escoger");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }
#endregion //Miembros de IWrpMfCs

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
        }
#endregion //Metodos Generados


    } //End of class wrpFacturaRapidaDetalle

} //End of namespace Galac.Saw.Wrp.Venta

