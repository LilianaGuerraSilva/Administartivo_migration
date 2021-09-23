using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls ;
using System.Windows.Controls.Primitives ;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.WpfControls;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil.Usal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.Venta;
using Galac.Adm.Ccl.DispositivosExternos;
using System.Xml.Linq;
using Galac.Saw.Wrp.Venta;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Venta {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Venta {
#else
namespace Galac.Saw.Wrp.Venta {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpCaja:System.EnterpriseServices.ServicedComponent, IWrpCaja {
        #region Variables
        string _Title = "Caja";
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpMfVb
        void IWrpCaja.Execute(string vfwAction,string vfwCurrentMfc,string vfwCurrentParameters) {
            try {
                CreateGlobalValues(vfwCurrentParameters);
                ILibMenu insMenu = new Galac.Adm.Uil.Venta.clsCajaMenu();
                insMenu.Ejecuta((eAccionSR)new LibEAccionSR().ToInt(vfwAction),1);
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx,null,Title + " - " + vfwAction);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
        }

        string IWrpCaja.Choose(string vfwParamInitializationList,string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if(Galac.Adm.Uil.Venta.clsCajaMenu.ChooseFromInterop(ref vXmlDocument,vSearchValues,vFixedValues)) {
                    vResult = vXmlDocument.InnerXml;
                }
                return vResult;
            } catch(GalacException gEx) {
                LibExceptionDisplay.Show(gEx,null,Title + " - Escoger");
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

        void IWrpCaja.InitializeComponent(string vfwLogin,string vfwPassword,string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibGalac.Aos.Vbwa.LibWrpHelper.ConfigureRuntimeContext(vfwLogin,vfwPassword);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }

        void IWrpCaja.InitializeDefProg(string vfwProgramInitials,string vfwProgramVersion,string vfwDbVersion,string vfwStrDateOfVersion,string vfwStrHourOfVersion,string vfwValueSpecialCharacteristic,string vfwCountry,string vfwCMTO,bool vfwUsePASOnLine) {
            try {
                string vLogicUnitDir = LibGalac.Aos.Cnf.LibAppSettings.ULS;
                LibGalac.Aos.DefGen.LibDefGen.InitializeProgramInfo(vfwProgramInitials,vfwProgramVersion,vfwDbVersion,LibConvert.ToDate(vfwStrDateOfVersion),vfwStrHourOfVersion,"",vfwCountry,LibConvert.ToInt(vfwCMTO));
                LibGalac.Aos.DefGen.LibDefGen.InitializeWorkPaths("",vLogicUnitDir,LibApp.AppPath(),LibGalac.Aos.DefGen.LibDefGen.ProgramInfo.ProgramInitials);
            } catch(Exception vEx) {
                if(vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar",vEx);
            }
        }

        void IWrpCaja.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch(Exception vEx) {
                if(vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización",vEx);
            }
        }
        #endregion //Miembros de IWrpMfVb

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania",LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania","ConsecutivoCompania"));
        }

        bool IWrpCaja.InsertarCajaPorDefecto(int vfwConsecutivoCompania) {
            ICajaPdn CajaNav = new Galac.Adm.Brl.Venta.clsCajaNav() as ICajaPdn;
            try {
                return CajaNav.InsertarCajaPorDefecto(vfwConsecutivoCompania);
            } catch(GalacException vEx) {
                LibExceptionDisplay.Show(vEx);
                return false;
            }
        }

        bool IWrpCaja.AbrirGaveta(int valConsecutivoCompania,int valConsecutivoCaja) {
            bool vReturn = false;
            ICajaPdn insCaja = new Galac.Adm.Brl.Venta.clsCajaNav();
            XElement vXMLCaja = null;
            insCaja.FindByConsecutivoCaja(valConsecutivoCompania,valConsecutivoCaja,"",ref vXMLCaja);
            bool vUsaGaveta = LibConvert.SNToBool(LibXml.GetPropertyString(vXMLCaja,"UsaGaveta"));
            if(vUsaGaveta) {
                ePuerto vPuertoDeGaveta = (ePuerto)LibConvert.DbValueToEnum(LibXml.GetPropertyString(vXMLCaja,"Puerto"));
                string vComandoGaveta = LibXml.GetPropertyString(vXMLCaja,"Comando");
                IGavetaPdn vGaveta = new Galac.Adm.Brl.DispositivosExternos.CajaGaveta.clsGavetaNav();
                vReturn = vGaveta.AbrirGaveta(vPuertoDeGaveta,vComandoGaveta);
            } else {
                vReturn = true;
            }
            return vReturn;
        }

        bool IWrpCaja.ActualizaUltimoNumComprobante(int valConsecutivoCompania,int valConsecutivoCaja,string valNumero,bool valEsNotaDeCredito) {
            bool vReturn = false;
            ICajaPdn insCaja = new Galac.Adm.Brl.Venta.clsCajaNav();
            try {
                vReturn = insCaja.ActualizaUltimoNumComprobante(valConsecutivoCompania,valConsecutivoCaja,valNumero,valEsNotaDeCredito);
                return vReturn;
            } catch(Exception vEx) {
                LibExceptionDisplay.Show(vEx);
            }
            return vReturn;
        }

        bool IWrpCaja.FindBySearchValues(int valConsecutivoCompania,int valConsecutivo,string valSqlWhere,ref string refXElement) {
            bool vResult = false;
            XElement xElementResult = null;
            ICajaPdn insCaja = new Galac.Adm.Brl.Venta.clsCajaNav();                        
            vResult = insCaja.FindByConsecutivoCaja(valConsecutivoCompania,valConsecutivo,valSqlWhere,ref xElementResult);
            if(xElementResult != null) {
                refXElement = xElementResult.ToString();                
            }
            return vResult;
        }
        #endregion //Metodos Generados
    } //End of class wrpCaja

} //End of namespace Galac.Saw.Wrp.Venta

