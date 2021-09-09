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
using Galac.Saw.Wrp.Impuesto;
#if IsExeBsF
namespace Galac.SawBsF.Wrp.Impuesto {
#elif IsExeBsS​
namespace Galac.SawBsS.Wrp.Impuesto {
#else
namespace Galac.Saw.Wrp.Impuesto {
#endif

    [ClassInterface(ClassInterfaceType.None)]

    public class wrpFormatosImpMunicipales : System.EnterpriseServices.ServicedComponent, IWrpFormatosImpMunicipalesVb {
        #region Variables
        string _Title = "Formatos ImpMunicipales";
        ILibBusinessComponentWithSearch<IList<FormatosImpMunicipales>, IList<FormatosImpMunicipales>> _Reglas;
     
        #endregion //Variables
        #region Propiedades

        private string Title {
            get { return _Title; }
        }
        #endregion //Propiedades
        #region Constructores
        private void RegistraCliente() {
            _Reglas = new Galac.Comun.Brl.Impuesto.clsFormatosImpMunicipalesNav();
        }
        #endregion //Constructores
        #region Metodos Generados
        #region Miembros de IWrpVb

       
        void IWrpFormatosImpMunicipalesVb.InitializeComponent(string vfwLogin, string vfwPassword, string vfwPath) {
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

        void IWrpFormatosImpMunicipalesVb.InitializeDefProg(string vfwProgramInitials, string vfwProgramVersion, string vfwDbVersion, string vfwStrDateOfVersion, string vfwStrHourOfVersion, string vfwValueSpecialCharacteristic, string vfwCountry, string vfwCMTO, bool vfwUsePASOnLine) {
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

        void IWrpFormatosImpMunicipalesVb.InitializeContext(string vfwInfo) {
            try {
                LibGalac.Aos.DefGen.LibDefGen.Initialize(vfwInfo);
            } catch (Exception vEx) {
                if (vEx is System.AccessViolationException) {
                    throw;
                }
                throw new GalacWrapperException(Title + " - Inicialización", vEx);
            }
        }
        #endregion //Miembros de IWrpVb

        #endregion //Metodos Generados



        void IWrpFormatosImpMunicipalesVb.Execute(string vfwAction) {
            try {
                ILibMenu insMenu = new Galac.Comun.Uil.Impuesto.clsFormatosImpMunicipalesMenu();
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

        string IWrpFormatosImpMunicipalesVb.Choose(string vfwParamInitializationList, string vfwParamFixedList) {
            string vResult = "";
            LibSearch insLibSearch = new LibSearch();
            List<LibSearchDefaultValues> vSearchValues = new List<LibSearchDefaultValues>();
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            try {
                vSearchValues = insLibSearch.CreateListOfParameter(vfwParamInitializationList);
                vFixedValues = insLibSearch.CreateListOfParameter(vfwParamFixedList);
                System.Xml.XmlDocument vXmlDocument = null;
                if (Galac.Comun.Uil.Impuesto.clsFormatosImpMunicipalesMenu.ChooseFromInterop(ref vXmlDocument, vSearchValues, vFixedValues)) {
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


        string IWrpFormatosImpMunicipalesVb.NumeroComprobanteFormateado(string valCodigoMunicipio, string valDatosAEvaluar) {
            string vResult = "";
            try {
                RegistraCliente();
                vResult = ((IFormatoImpMunicipalesPdn)_Reglas).NumeroComprobanteFormateado(valCodigoMunicipio, valDatosAEvaluar);
                vResult = GetNumeroComprobante(vResult);
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Comprobante Retencion");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }


        string IWrpFormatosImpMunicipalesVb.CondicionParaFiltroComprobante(string valCodigoMunicipio, string valFechaAplicacion) {
            string vResult = "";
            try {
                RegistraCliente();
                vResult = ((IFormatoImpMunicipalesPdn)_Reglas).CondicionParaFiltroComprobante(valCodigoMunicipio, LibGalac.Aos.Base.LibConvert.ToDate(valFechaAplicacion));
                vResult = GetCondicionParaFiltro(vResult);
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - Comprobante Retencion");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

        string IWrpFormatosImpMunicipalesVb.DatosDeltxtImpuestos(string valCodigoMunicipio, string valLinea) {
            string vResult = "";
            try {
                RegistraCliente();
                vResult = ((IFormatoImpMunicipalesPdn)_Reglas).DatosDeltxtImpuestos(valCodigoMunicipio, LibGalac.Aos.Base.LibConvert.ToInt(valLinea));

                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - DatosDeltxtImpuestos");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }


        string IWrpFormatosImpMunicipalesVb.CampoFormateado(string valCodigoMunicipio, string valColumna, string valValor, string valNumerico) {
            string vResult = "";
            try {
                RegistraCliente();
                vResult = ((IFormatoImpMunicipalesPdn)_Reglas).CampoFormateado(valCodigoMunicipio, valColumna, valValor, LibConvert.SNToBool(valNumerico));
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - DatosDeltxtImpuestos");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }


        string IWrpFormatosImpMunicipalesVb.UsaTipoDeTipodeOperacion(string valCodigoMunicipio) {
            try {
                string vValue = "";
                StringBuilder vResult = new StringBuilder();
                RegistraCliente();
                vValue = LibConvert.BoolToSN(((IFormatoImpMunicipalesPdn)_Reglas).UsaTipoDeTipodeOperacion(valCodigoMunicipio));
                vResult.Append("<GpData>");
                vResult.Append("<GpResult>");
                vResult.Append("<UsaTipoOperacion>");
                vResult.Append(vValue);
                vResult.Append("</UsaTipoOperacion>");
                vResult.Append("</GpResult>");
                vResult.Append("</GpData>");
                return vResult.ToString();
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - DatosDeltxtImpuestos");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return "";
        }

        private string GetCondicionParaFiltro(string valCondicion) {
            StringBuilder vResult = new StringBuilder();
            vResult.Append("<GpData>");
            vResult.Append("<GpResult>");
            vResult.Append("<Condicion>");
            vResult.Append(valCondicion);
            vResult.Append("</Condicion>");
            vResult.Append("</GpResult>");
            vResult.Append("</GpData>");
            return vResult.ToString();
        }

        private string GetNumeroComprobante(string valNumeroComprobante) {
            StringBuilder vResult = new StringBuilder();
            vResult.Append("<GpData>");
            vResult.Append("<GpResult>");
            vResult.Append("<NumeroComprobante>");
            vResult.Append(valNumeroComprobante);
            vResult.Append("</NumeroComprobante>");
            vResult.Append("</GpResult>");
            vResult.Append("</GpData>");
            return vResult.ToString();
        }

        bool IWrpFormatosImpMunicipalesVb.EscribirTxtImpuestoMunicipal(string valRutaArchivo, string valSQLWhere, int valConsecutivoCompania){
            bool vResult = false;
            try {
                RegistraCliente();
                vResult = ((IFormatoImpMunicipalesPdn)_Reglas).EscribirTxtImpuestoMunicipal(valRutaArchivo, valSQLWhere, valConsecutivoCompania);
                return vResult;
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title + " - EscribirTxtImpuestoMunicipal");
            } catch (Exception vEx) {
                if (vEx is AccessViolationException) {
                    throw;
                }
                LibExceptionDisplay.Show(vEx);
            }
            return false;
        }

    } //End of class wrpFormatosImpMunicipales

} //End of namespace Galac.Saw.Wrp.Impuesto

