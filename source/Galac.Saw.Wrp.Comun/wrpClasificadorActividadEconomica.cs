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
using Galac.Comun.Ccl.Impuesto;
using System.Xml.Linq;
using Galac.Saw.Wrp.Impuesto;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Impuesto {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Impuesto {
#else
namespace Galac.Saw.Wrp.Impuesto {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpClasificadorActividadEconomica : System.EnterpriseServices.ServicedComponent, IWrpClasificadorActividadVb {
        #region Variables
        string _Title = "Clasificador Actividad Económica";
        ILibBusinessComponentWithSearch<IList<ClasificadorActividadEconomica>, IList<ClasificadorActividadEconomica>> _Reglas;
     
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

        private void RegistraCliente() {
            _Reglas = new Galac.Comun.Brl.Impuesto.clsClasificadorActividadEconomicaNav();
        }
       
        #endregion //Miembros de IWrpVb

        #endregion //Metodos Generados


        void IWrpClasificadorActividadVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
            try {
                LibWrp.SetAppConfigToCurrentDomain(vfwPath);
                LibWrpHelper.ConfigureRuntimeContext(vfwLogin, vfwPassword);
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicializar", vEx);
            }
        }

        void IWrpClasificadorActividadVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpClasificadorActividadVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }

         
        string IWrpClasificadorActividadVb.CalculoRetencion(string valCodigoActividad, string valFechaAplicasion, string valMontoFactura) {
            string vResult = "";
            decimal vMontoRetenido = 0;
            try {
                RegistraCliente();
                vMontoRetenido = ((IClasificadorActividadEconomicaPdn)_Reglas).CalculoRetencion(valCodigoActividad, LibGalac.Aos.Base.LibConvert.ToDate(valFechaAplicasion), LibGalac.Aos.Base.LibConvert.ToDec(valMontoFactura));
                vResult = GetValorConFormatoInterop(LibGalac.Aos.Base.LibConvert.ToStr(vMontoRetenido), "MontoRetencion");  
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - CalculoRetencion");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }
      
       void IWrpClasificadorActividadVb.Execute(string vfwAction, string vfwCodigoMunicipio) {
            try {
                LibGlobalValues insGV = CreateGlobalValues(vfwCodigoMunicipio);
                ILibMenu insMenu = new Galac.Comun.Uil.Impuesto.clsClasificadorActividadEconomicaMenu();
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

        string IWrpClasificadorActividadVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
          
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if(Galac.Comun.Uil.Impuesto.clsClasificadorActividadEconomicaMenu.ChooseFromInterop( ref vXmlDocument, vSearchValues, vFixedValues)) {
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


      string  IWrpClasificadorActividadVb.PuedeActivarModulo(string valCondigoMunicipio) {
            string vResult = "";
          
            try {
                RegistraCliente();
                vResult = LibConvert.BoolToSN(((IClasificadorActividadEconomicaPdn)_Reglas).TieneConceptos(valCondigoMunicipio));
                vResult = GetValorConFormatoInterop(vResult,"TieneConceptos");
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Puede Activar Modulo");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

      private string GetValorConFormatoInterop(string valValue,string valNombreColumna) {
          StringBuilder vResult = new StringBuilder();
          vResult.AppendLine("<GpData>");
          vResult.AppendLine("<GpResult>");
          vResult.Append("<"+ valNombreColumna +">");
          vResult.Append(valValue);
          vResult.AppendLine("</" + valNombreColumna + ">");
          vResult.AppendLine("</GpResult>");
          vResult.AppendLine("</GpData>");
          return vResult.ToString();
      }



      private LibGlobalValues CreateGlobalValues(string valCurrentParameters) {
          XElement vGlobalValues = new XElement("GpParameters",
                                            new XElement("Compania", new XElement("CodigoMunicipio", valCurrentParameters)));

          LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesAdd(vGlobalValues.ToString());
          return LibGlobalValues.Instance;
      }

      void RegisterDefaultTypesIfMissing() {
          LibGalac.Aos.Uil.LibMessagesHandler.RegisterMessages();
      }

    } //End of class wrpClasificadorActividadEconomica

} //End of namespace Galac.Saw.Wrp.Impuesto

