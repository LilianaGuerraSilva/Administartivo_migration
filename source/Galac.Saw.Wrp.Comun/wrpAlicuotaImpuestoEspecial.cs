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
using System.Runtime.InteropServices;
using Galac.Comun.Brl.Impuesto;
using Galac.Comun.Ccl.Impuesto;
using System.Xml.Linq;
using Galac.Saw.Wrp.Impuesto;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Impuesto {
#else
namespace Galac.Saw.Wrp.Impuesto {
#endif

    [ClassInterface(ClassInterfaceType.None)]
    public class wrpAlicuotaImpuestoEspecial : System.EnterpriseServices.ServicedComponent, IWrpAlicuotaImpuestoEspecial {
        #region Variables
        string _Title = "Alicuota Impuesto Especial";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpVb

        void IWrpAlicuotaImpuestoEspecial.Execute(string vfwAction) {
            try {
                ILibMenu insMenu = new Galac.Comun.Uil.Impuesto.clsAlicuotaImpuestoEspecialMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction), 1);
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + vfwAction);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpAlicuotaImpuestoEspecial.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Comun.Uil.Impuesto.clsAlicuotaImpuestoEspecialMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
                    vResult = vXmlDocument.InnerXml;
                }
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

        void IWrpAlicuotaImpuestoEspecial.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpAlicuotaImpuestoEspecial.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpAlicuotaImpuestoEspecial.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

        string IWrpAlicuotaImpuestoEspecial.ObtenerAlicuotaEspecial(DateTime valFecha) {
            string vResult = string.Empty;
            try {
                IAlicuotaImpuestoEspecialPdn insAlicuotaEspecial = new clsAlicuotaImpuestoEspecialNav();
                XElement vData = insAlicuotaEspecial.ObtenerAlicuotaEspecial(valFecha);
                if (vData != null  ) {
                    vResult = vData.ToString();
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "ObtenerAlicuotaEspecial");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }

        string IWrpAlicuotaImpuestoEspecial.ObtenerAlicuotaEspecial(DateTime valFecha, string valSqlWhereAND) {
            string vResult = string.Empty;
            try {
                IAlicuotaImpuestoEspecialPdn insAlicuotaEspecial = new clsAlicuotaImpuestoEspecialNav();
                XElement vData = insAlicuotaEspecial.ObtenerAlicuotaEspecial(valFecha, valSqlWhereAND);
                if (vData != null) {
                    vResult = vData.ToString();
                }


            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "ObtenerAlicuotaEspecial");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }

        string IWrpAlicuotaImpuestoEspecial.ObtenerAlicuotaEspecialConFiltro(DateTime valFecha, string valSqlWhereAND) {
            string vResult = string.Empty;
            try {
                IAlicuotaImpuestoEspecialPdn insAlicuotaEspecial = new clsAlicuotaImpuestoEspecialNav();
                XElement vData = insAlicuotaEspecial.ObtenerAlicuotaEspecial(valFecha, valSqlWhereAND);
                if (vData != null) {
                    vResult = vData.ToString();
                }

              
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - " + "ObtenerAlicuotaEspecial");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return vResult;
        }
        #endregion //Miembros de IWrpVb
        #endregion //Metodos Generados


    } //End of class wrpAlicuotaImpuestoEspecial

} //End of namespace Galac.Saw.Wrp.Impuesto

