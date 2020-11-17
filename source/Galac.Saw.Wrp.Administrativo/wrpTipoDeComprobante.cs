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
using LibGalac.Aos.Vbwa;
using Galac.Contab.Core;
using Galac.Contab.Brl.Tablas;
using Galac.Contab.Ccl.Tablas;
using System.Xml.Linq;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.Tablas {
#else
namespace Galac.Saw.Wrp.Tablas {
#endif
    [ClassInterface(ClassInterfaceType.None)]

    public class wrpTipoDeComprobante : System.EnterpriseServices.ServicedComponent, IWrpTipoDeComprobante {
        #region Variables
        string _Title = "Tipo De Comprobante";
        #endregion //Variables
        #region Propiedades
        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpTipoDeComprobante

        void IWrpTipoDeComprobante.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                GSSearchCriteria SearchCriteria = new GSSearchCriteria();
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpTipoDeComprobante.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpTipoDeComprobante.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpTipoDeComprobante

        #endregion //Metodos Generados

        bool IWrpTipoDeComprobante.InsertRecord(string vfwCodigo, string vfwNombre) {
            //ILibBusinessComponent<TipoDeComprobante, TipoDeComprobante> vTipoDeComprobanteNav = new clsTipoDeComprobanteNav() as ILibBusinessComponent<TipoDeComprobante, TipoDeComprobante>;
            //return vTipoDeComprobanteNav.DoAction(new TipoDeComprobante(){Codigo= vfwCodigo , Nombre=vfwNombre},eAccionSR.Insertar, null).Success;
            ITipoDeComprobantePdn vTipoDeComprobanteNav = new clsTipoDeComprobanteNav();
            return vTipoDeComprobanteNav.InsertRecord(vfwCodigo, vfwNombre);
        }

        string IWrpTipoDeComprobante.BuscarPorCodigoONombre(string vfwFieldName, string vfwFieldValue) {
            string vResult = "";
            ITipoDeComprobantePdn vTipoDeComprobanteNav = new clsTipoDeComprobanteNav();
            XElement vXmlResult = vTipoDeComprobanteNav.BuscarPorCodigoONombre(vfwFieldName, vfwFieldValue);
            if (vXmlResult != null) {
                vResult = vXmlResult.ToString();
            }
            return vResult;
        }
    } //End of class wrpTipoDeComprobante
} //End of namespace Galac.Saw.Wrp.Tablas