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

namespace Galac.Saw.Wrp.Inventario {

    public interface IWpfLoteDeInventario {
        void InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath);
        void InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine);
        void InitializeContext(string vfwInfo);
        void Execute(string valConsecutivoCompania, string valCodigoArticulo);

    }

    public class wrpLoteDeInventario: IWpfLoteDeInventario {
        #region Variables
        string _Title = "Lote de Inventario";
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

        void IWpfLoteDeInventario.Execute(string valConsecutivoCompania, string valCodigoArticulo) {
            try {
                //CreateGlobalValues(vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Saw.Uil.Inventario.clsLoteDeInventarioMenu(LibGalac.Aos.Base.LibConvert.ToInt(valConsecutivoCompania), valCodigoArticulo);
                insMenu.Ejecuta(eAccionSR.Consultar, 1);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Consultar");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        void IWpfLoteDeInventario.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWpfLoteDeInventario.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWpfLoteDeInventario.InitializeContext(string vfwInfo) {
            try {
                //no hace falta inicializar nada
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        //string IWpfLoteDeInventario.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
        //    string vResult = "";
        //    LibSearch insLibSearch = new LibSearch();
        //    List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
        //    List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
        //    try {
        //        vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
        //        vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
        //        System.Xml.XmlDocument vXmlDocument = null;
        //        if (Galac.Saw.Uil.Inventario.clsLoteDeInventarioMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
        //            vResult = vXmlDocument.InnerXml;
        //        }
        //        return vResult;
        //    } catch (GalacException gEx) {
        //        LibExceptionDisplay.Show(gEx, null, Title +  " - Escoger");
        //    } catch (Exception vEx) {
        //        if (vEx is AccessViolationException) {
        //            throw;
        //        }
        //        LibExceptionDisplay.Show(vEx);
        //    }
        //    return "";
        //}
        #endregion //Miembros de IWrpMfCs

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
        }
        #endregion //Metodos Generados


    } //End of class wrpLoteDeInventario

} //End of namespace Galac.Saw.Wrp.Inventario

