using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.SttDef;
using System.Threading.Tasks;
using Galac.Adm.Brl.ImprentaDigital;
using LibGalac.Aos.UI.Mvvm.Messaging;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.ImprentaDigital {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.ImprentaDigital {
#else
namespace Galac.Saw.Wrp.ImprentaDigital {
#endif
    [ClassInterface(ClassInterfaceType.None)]
    public class wrpImprentaDigital: System.EnterpriseServices.ServicedComponent, IWrpImprentaDigitalVb {
        #region Variables
        string _Title = "Imprenta Digital";
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

        bool IWrpImprentaDigitalVb.EnviarDocumento(int vfwTipoDocumento, string vfwNumeroFactura, string vfwCurrentParameters, ref string vfwNumeroControl, ref string vfwMensaje) {
            try {
                string vNumeroControl = "";
                bool vResult = false;
                CreateGlobalValues(vfwCurrentParameters);
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)vfwTipoDocumento;
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, vTipoDeDocumento, vfwNumeroFactura);                
                Task vTask = Task.Factory.StartNew(() => {
                    vResult = _insImprentaDigital.EnviarDocumento();
                    vNumeroControl = _insImprentaDigital.NumeroControl;
                });
                vTask.Wait();
                vfwNumeroControl = vNumeroControl;
                vfwMensaje = _insImprentaDigital.Mensaje;
                return vResult;
            } catch (AggregateException vEx) {
                vfwMensaje = vEx.InnerException.Message;
                return false;
            } catch (GalacException gEx) {
                vfwMensaje = gEx.Message;
                return false;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                vfwMensaje = vEx.Message;
                return false;
            }
        }

        bool IWrpImprentaDigitalVb.AnularDocumento(int vfwTipoDocumento, string vfwNumeroFactura, string vfwCurrentParameters, ref string vfwMensaje) {
            try {               
                bool vResult = false;
                CreateGlobalValues(vfwCurrentParameters);
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)vfwTipoDocumento;                
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, vTipoDeDocumento, vfwNumeroFactura);
                Task vTask = Task.Factory.StartNew(() => {
                    vResult = _insImprentaDigital.EstadoDocumento();
                    if (vResult) {
                        if (_insImprentaDigital.EstatusDocumento != "Anulada") {
                            vResult = _insImprentaDigital.AnularDocumento();
                        }
                    }
                });
                vTask.Wait();
                vfwMensaje = _insImprentaDigital.Mensaje;
                if (!vResult) {
                    vfwMensaje = "El documento que desea anular no pudo ser encontrado en la Imprenta Digital.\r\nSincronice sus documentos antes de volver a intentar."; 
                }                
                return vResult;
            } catch (AggregateException vEx) {
                vfwMensaje = vEx.InnerException.Message;
                return false;
            } catch (GalacException gEx) {
                vfwMensaje = gEx.Message;
                return false;
            } catch (Exception vEx) {
                vfwMensaje = vEx.Message;
                return false;
            }
        }

        bool IWrpImprentaDigitalVb.SincronizarDocumento(int vfwTipoDocumento, string vfwNumeroFactura, string vfwCurrentParameters, ref string vfwNumeroControl, ref string vfwMensaje) {
            try {
                string vNumeroControl = "";
                bool vResult = false;
                CreateGlobalValues(vfwCurrentParameters);
                eTipoDocumentoFactura vTipoDeDocumento = (eTipoDocumentoFactura)vfwTipoDocumento;
                eProveedorImprentaDigital vProveedorImprentaDigital = (eProveedorImprentaDigital)LibConvert.DbValueToEnum(LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("Parametros", "ProveedorImprentaDigital"));
                var _insImprentaDigital = ImprentaDigitalCreator.Create(vProveedorImprentaDigital, vTipoDeDocumento, vfwNumeroFactura);
                Task vTask = Task.Factory.StartNew(() => {
                    vResult = _insImprentaDigital.SincronizarDocumentos();
                    vNumeroControl = _insImprentaDigital.NumeroControl;
                });
                vTask.Wait();
                vfwNumeroControl = vNumeroControl;
                vfwMensaje = _insImprentaDigital.Mensaje;
                return vResult;
            } catch (AggregateException vEx) {
                vfwMensaje = vEx.InnerException.Message;
                return false;
            } catch (GalacException gEx) {
                vfwMensaje = gEx.Message;
                return false;
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                vfwMensaje = vEx.Message;
                return false;
            }
        }       

        void IWrpImprentaDigitalVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpImprentaDigitalVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpImprentaDigitalVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpMfVb

        private void CreateGlobalValues(string valCurrentParameters) {
            LibGlobalValues.Instance.LoadCompleteAppMemInfo(valCurrentParameters);
            LibGlobalValues.Instance.GetMfcInfo().Add("Compania", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Compania", "ConsecutivoCompania"));
            LibGlobalValues.Instance.GetMfcInfo().Add("Periodo", LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("Periodo", "ConsecutivoPeriodo"));
        }
        #endregion //Metodos Generados
    }  //End of class wrpImprentaDigital
}//End of namespace Galac.Saw.Wrp.ImprentaDigital

