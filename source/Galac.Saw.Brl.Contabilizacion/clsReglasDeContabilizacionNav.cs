using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.Contabilizacion;
using Galac.Contab.Ccl.WinCont;
using System.Windows.Forms;

namespace Galac.Saw.Brl.Contabilizacion {
    public partial class clsReglasDeContabilizacionNav:LibBaseNavRU<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>>, IReglasDeContabilizacionPdn {
        #region Constantes
        string ModuleName = "Reglas de Contabilización";
        #endregion //Constantes
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsReglasDeContabilizacionNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponent<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>> GetDataInstance() {
            return new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule,eAccionSR valAction,string valExtendedAction,XmlDocument valXmlRow) {
            return true;
        }

        bool ILibPdn.GetDataForList(string valCallingModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            throw new NotImplementedException("Listar no aplica para este objeto");
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule,StringBuilder valParameters) {
            throw new NotImplementedException("GetFk no aplica para este objeto");
        }
        #endregion //Miembros de ILibPdn


        protected override bool RetrieveListInfo(string valModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            ILibPdn vPdnModule;
            switch(valModule) {
            case "Cuenta":
            vPdnModule = new Galac.Contab.Brl.WinCont.clsCuentaNav();
            vResult = vPdnModule.GetDataForList("Cuenta Reglas",ref refXmlDocument,valXmlParamsExpression);
            break;
            case "Tipo de Comprobante":
            vPdnModule = new Galac.Contab.Brl.Tablas.clsTipoDeComprobanteNav();
            vResult = vPdnModule.GetDataForList(ModuleName,ref refXmlDocument,valXmlParamsExpression);
            break;
            default: throw new NotImplementedException();
            }
            return vResult;
        }
        #endregion //Metodos Generados
        System.Xml.Linq.XElement IReglasDeContabilizacionPdn.GetListaGrupoDeActivos(string valModule,int valConsecutivoPeriodo) {
            XElement vResult;
            IGrupoDeActivosPdn vPdnModule;
            vPdnModule = new Galac.Contab.Brl.WinCont.clsGrupoDeActivosNav();
            vResult = vPdnModule.GetListaGrupoDeActivos(valModule,valConsecutivoPeriodo);
            return vResult;
        }

        private StringBuilder ParametrosProximoNumero(int valConsecutivoCompania) {
            StringBuilder vResult = new StringBuilder();
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vResult = vParams.Get();
            return vResult;
        }

        private string ProximoNumero(int valConsecutivoCompania) {
            string vResult = "";
            XElement vNumero;
            ILibDataComponent<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>> instanciaDal = new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionDat();
            vNumero = instanciaDal.QueryInfo(eProcessMessageType.Message,"ProximoNumero",ParametrosProximoNumero(valConsecutivoCompania));
            vResult = LibXml.GetPropertyString(vNumero,"Numero");
            return vResult;
        }

        private void InsertarValorPorDefecto(int valConsecutivoCompania) {
            IList<ReglasDeContabilizacion> vBusinessObject = new List<ReglasDeContabilizacion>();
            ReglasDeContabilizacion insReglasDeContabilizacion = new ReglasDeContabilizacion();
            insReglasDeContabilizacion.Clear();
            insReglasDeContabilizacion.ConsecutivoCompania = valConsecutivoCompania;
            insReglasDeContabilizacion.Numero = ProximoNumero(valConsecutivoCompania);
            vBusinessObject.Add(insReglasDeContabilizacion);
            ILibDataComponent<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>> instanciaDal = new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionDat();
            instanciaDal.Insert(vBusinessObject);
        }

        void IReglasDeContabilizacionPdn.InsertarRegistroPorDefecto(int valConsecutivoCompania) {
            InsertarValorPorDefecto(valConsecutivoCompania);
        }

        private StringBuilder GetPkParams(int valConsecutivoCompania,string valNumero) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            //vParams.AddInString("Numero", valNumero, 11);
            return vParams.Get();
        }

        private IList<ReglasDeContabilizacion> BuscarReglasDeContabilizacionParaEscoger(int valConsecutivoCompania,string valNumero) {
            ILibDataComponent<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>> instanciaDal = new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionDat();
            IList<ReglasDeContabilizacion> vResulset = instanciaDal.GetData(eProcessMessageType.SpName,"ReglasDeContabilizacionGET",GetPkParams(valConsecutivoCompania,valNumero));
            return vResulset;
        }

        private void BuscarEInsertaCopiaDeReglasContables(int valConsecutivoCompania,string valNumero,int valConsecutivoCompaniaDestino) {
            IList<ReglasDeContabilizacion> vBusinessObject = new List<ReglasDeContabilizacion>();
            ReglasDeContabilizacion insReglasDeContabilizacion = new ReglasDeContabilizacion();
            insReglasDeContabilizacion.Clear();
            IList<ReglasDeContabilizacion> vReglasDeContabilizacion = BuscarReglasDeContabilizacionParaEscoger(valConsecutivoCompania,valNumero);
            if(vReglasDeContabilizacion.Count > 0) {
                insReglasDeContabilizacion = vReglasDeContabilizacion[0];
                insReglasDeContabilizacion.ConsecutivoCompania = valConsecutivoCompaniaDestino;
                insReglasDeContabilizacion.Numero = ProximoNumero(valConsecutivoCompania);
                vBusinessObject.Add(insReglasDeContabilizacion);
                ILibDataComponent<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>> instanciaDal = new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionDat();
                instanciaDal.Insert(vBusinessObject);
            }
        }

        void IReglasDeContabilizacionPdn.CopiarReglasDeContabilizacion(int valConsecutivoCompania,string valNumero,int valConsecutivoCompaniaDestino) {
            BuscarEInsertaCopiaDeReglasContables(valConsecutivoCompania,valNumero,valConsecutivoCompaniaDestino);
        }

        bool IReglasDeContabilizacionPdn.SePuedeUsarEstaCuenta(bool valUsaAuxiliares,bool valUsaModuloDeActivoFijo,bool valEscogerAuxiliares,string valGetCierreDelEjercicio,ref string valMensaje,XmlDocument valXmlDocument) {
            bool vResult = false;
            vResult = IndicaSiPuedeUsarEstaCuenta(valUsaAuxiliares,valUsaModuloDeActivoFijo,valEscogerAuxiliares,valGetCierreDelEjercicio,ref valMensaje,valXmlDocument);
            return vResult;

        }

        bool IndicaSiPuedeUsarEstaCuenta(bool valUsaAuxiliares,bool valUsaModuloDeActivoFijo,bool valEscogerAuxiliares,string valGetCierreDelEjercicio,ref string valMensaje,XmlDocument valXmlDocument) {
            bool vResult = true;
            bool vUsaAuxiliares = valUsaAuxiliares;
            bool vUsaModuloDeActivoFijo = valUsaModuloDeActivoFijo;
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlDocument);
            if(insParser.GetBool(0,"TieneSubCuentas",true)) {
                valMensaje = "La Cuenta Seleccionada es de Título.";
                vResult = false;
            }
            if(insParser.GetBool(0,"EsActivoFijo",false)) {
                valMensaje = "Esta es una Cuenta de Activo Fijo, por lo tanto no puede ser utilizada en esta Definición.";
                vResult = false;
            }
            if((insParser.GetBool(0,"TieneAuxiliar",false)) && (!valEscogerAuxiliares)) {
                valMensaje = "Esta Cuenta Usa Manejo de Auxiliar, por lo tanto no puede ser asignada en esta Definición.";
                vResult = false;
            }
            eTipoCuenta vtipocuenta = (eTipoCuenta)insParser.GetEnum(0,"TipoCuenta",0);
            if(vtipocuenta == eTipoCuenta.OrdenDeudora || vtipocuenta == eTipoCuenta.OrdenAcreedora) {
                valMensaje = "Esta es una Cuenta de Orden, por lo tanto no puede ser asignada en esta Definición.";
                vResult = false;
            }
            if(LibString.S1IsEqualToS2(insParser.GetString(0,"Codigo",""),valGetCierreDelEjercicio)) {
                valMensaje = "Esta es la Cuenta de Resultado del Ejercicio, por lo tanto no puede ser utilizada en esta Definición.";
                vResult = false;
            }

            return vResult;
        }

        string IReglasDeContabilizacionPdn.CorrigeYAjustaLaCuenta(string valCode) {
            string vResul;

            int valMaxNumLevels = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion","MaxNumLevels");
            int valMaxNumLevelsAtMatrix = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion","MaxNumLevelsAtMatrix");
            int valMinNumLevels = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion","MinNumLevels");
            int valMaxLength = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetInt("ReglasDeContabilizacion","MaxLength");
            bool valUseZeroAtRigth = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","UseZeroAtRigth");
            string valNiveles = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetString("ReglasDeContabilizacion","Niveles");

            LibGalac.CommonModules.LibLevel insNivelesCuenta = new LibGalac.CommonModules.LibLevel(valMaxNumLevels,valMaxNumLevelsAtMatrix,valMinNumLevels,valMaxLength,valUseZeroAtRigth);
            insNivelesCuenta.ArrayOfLevels = ArrayOfLevels(LibString.Split(valNiveles,','),insNivelesCuenta.InitArrayOfLevels());
            vResul = insNivelesCuenta.AdjustCode(valCode,true);
            return vResul;
        }


        private int[] ArrayOfLevels(string[] valNiveles,int[] valInitArrayOfLevel) {
            int[] vResult;
            int i = 0;
            vResult = valInitArrayOfLevel;
            if(valNiveles != null) {
                foreach(string value in valNiveles) {
                    vResult[i] = LibGalac.Aos.Base.LibConvert.ToInt(value);
                    i += 1;
                }
            }
            return vResult;
        }


        bool IReglasDeContabilizacionPdn.LasCuentasDeReglasDeContabilizacionEstanCompletas(int valConsecutivoCompania,string valModule,bool valUsaModuloDeActivoFijo,bool valConMensaje,int valTipoDocumento) {
            ILibDataComponent<IList<ReglasDeContabilizacion>,IList<ReglasDeContabilizacion>> instanciaDal = new Galac.Saw.Dal.Contabilizacion.clsReglasDeContabilizacionDat();
            IList<ReglasDeContabilizacion> vResulset = instanciaDal.GetData(eProcessMessageType.SpName,"ReglasDeContabilizacionGET",GetPkParams(valConsecutivoCompania,""));
            ReglasDeContabilizacion insReglasDeContabilizacion = new ReglasDeContabilizacion();
            insReglasDeContabilizacion = vResulset[0];
            bool vResult = false;
            bool vEstancompletas;
            string vMensaje;
            //valModule = valModule;
            vEstancompletas = fLasCuentasDeIvaEstanCompletas(insReglasDeContabilizacion)
                              && fLosTiposDeComprobantesEstanCompletos(insReglasDeContabilizacion);
            vEstancompletas = true;
            if(vEstancompletas) {
                switch(valModule) {
                case "CxC":
                vEstancompletas = fLasCuentasDeCxCEstanCompletas(insReglasDeContabilizacion);
                break;
                case "Factura":
                vEstancompletas = fLasCuentasDeFacturacionEstanCompletas(insReglasDeContabilizacion);
                break;
                case "CxP":
                vEstancompletas = fLasCuentasDeCxPEstanCompletas(insReglasDeContabilizacion);
                break;
                case "Cobranza":
                vEstancompletas = fLasCuentasDeCobranzaEstanCompletas(insReglasDeContabilizacion);
                break;
                case "Pagos":
                vEstancompletas = fLasCuentasDePagosEstanCompletas(insReglasDeContabilizacion);
                break;
                case "MovimientoBancario":
                vEstancompletas = fLasCuentasDeMovBancarioEstanCompletas(insReglasDeContabilizacion);
                break;
                case "ResumenDiarioDeVentas":
                vEstancompletas = fLasCuentasDeResumenDeVentasEstanCompletas(insReglasDeContabilizacion);
                break;
                case "Anticipo":
                vEstancompletas = fLasCuentasDeAnticipoEstanCompletas(valTipoDocumento,insReglasDeContabilizacion);
                break;
                case "Inventario":
                vEstancompletas = fLasCuentasDeInventarioEstanCompletas(insReglasDeContabilizacion);
                break;
                case "PagoSueldos":
                vEstancompletas = fLasCuentasDePagoSueldoEstanCompletas(insReglasDeContabilizacion);
                break;
                case "ReposicionDeCajaChica":
                vEstancompletas = fLasCuentasDeReposicionEstanCompletas(insReglasDeContabilizacion);
                break;

                default: throw new NotImplementedException();
                }
            }
            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDeIvaEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = true;
            if((LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaIva1Credito)))
                || (LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaIva1Debito)))) {
                vResult = false;
            } else if(LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.DiferenciaEnCambioyCalculo))) {
                vResult = false;
            }
            return vResult;
        }

        bool fLosTiposDeComprobantesEstanCompletos(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            int vTipoNumeracion = LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetEnum("Periodo","TipoDeNumeracion");
            if(vTipoNumeracion != 0) { // Se coloca  0 mientras se realiza la migracion AOS completa debe ir enumerativo eTipoNumeracionComprobante
                vResult = true;
            } else {

                vResult = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.FacturaTipoComprobante))) &&
                          (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CxCTipoComprobante))) &&
                          (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CxPTipoComprobante))) &&
                          (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CobranzaTipoComprobante))) &&
                          (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.PagoTipoComprobante))) &&
                          (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.MovimientoBancarioTipoComprobante)));
            }
            return vResult;
        }

        bool fLasCuentasDeCxCEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = true;
            if((LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCxCClientes)))
                 || (LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCxCIngresos)))) {
                vResult = false;
            }
            return vResult;
        }

        bool fLasCuentasDeFacturacionEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = true;
            if((LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaFacturacionCxCClientes)))
                 || (LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaFacturacionMontoTotalFactura)))
                 || (LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaFacturacionCargos)))
                 || (LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaFacturacionDescuentos)))
                ) {
                vResult = false;
            }
            if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","UsarVentasConIvaDiferido"))) {
                vResult = vResult && (LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaFacturacionIvaDiferido)));
            }
            return vResult;
        }

        bool fLasCuentasDeCxPEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            // analizar para unir en uno solo
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCxPGasto)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCxPProveedores)));
            if(vEstancompletas) {
                //If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
                //     If GetDondeContabilizarRetIvaAsEnum = eDC_NoContabilizada Then
                //        EstanCompletas = False
                //     ElseIf GetDondeContabilizarRetIvaAsEnum = eDC_CxP Then
                //        EstanCompletas = EstanCompletas And GetCuentaRetencionIVA <> ""
                //     End If
                //End If
            }
            return vResult;
        }

        bool fLasCuentasDeCobranzaEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCobranzaCobradoEnEfectivo)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCobranzaCobradoEnCheque)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCobranzaCobradoEnTarjeta)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.cuentaCobranzaRetencionISLR)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.cuentaCobranzaRetencionIVA)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCobranzaOtros)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCobranzaCxCClientes)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCobranzaCobradoAnticipo)));
            if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaCreditoBancario"))) {
                vEstancompletas = (vEstancompletas && fLasCuentasDeCreditoBancarioEstanCompletas(insReglasDeContabilizacion));
            }
            if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("ReglasDeContabilizacion","UsarVentasConIvaDiferido"))) {
                vEstancompletas = (vEstancompletas && (LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCobranzaIvaDiferido))));
            }

            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDePagosEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaPagosCxPProveedores)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaPagosRetencionISLR)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaPagosOtros)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaPagosBanco)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaPagosPagadoAnticipo)));
            if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaDebitoBancario"))) {
                vEstancompletas = (vEstancompletas && fLasCuentasDeDebitoBancarioEstanCompletas(insReglasDeContabilizacion));
            }
            // If EstanCompletas Then
            //   If gProyCompaniaActual.fPuedoUsarOpcionesDeContribuyenteEspecial Then
            //      If GetDondeContabilizarRetIvaAsEnum = eDC_NoContabilizada Then
            //         EstanCompletas = False
            //      ElseIf GetDondeContabilizarRetIvaAsEnum = eDC_Pago Then
            //         EstanCompletas = EstanCompletas And GetCuentaRetencionIVA <> ""
            //      End If
            //   End If
            //End If
            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDeMovBancarioEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaMovBancarioGasto)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaMovBancarioBancosHaber)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaMovBancarioBancosDebe)));
            vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaMovBancarioIngresos)));
            if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaDebitoBancario"))) {
                vEstancompletas = (vEstancompletas && fLasCuentasDeDebitoBancarioEstanCompletas(insReglasDeContabilizacion));
            }
            if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaCreditoBancario"))) {
                vEstancompletas = (vEstancompletas && fLasCuentasDeCreditoBancarioEstanCompletas(insReglasDeContabilizacion));
            }
            vResult = vEstancompletas;
            return vResult;
        }


        bool fLasCuentasDeResumenDeVentasEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaRDVtasCaja))) &&
                               (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaRDVtasMontoTotal)));
            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDeAnticipoEstanCompletas(int valTipoDocumento,ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            int vTipoDocumento;
            vTipoDocumento = valTipoDocumento;
            if((vTipoDocumento == 0)) { // enum_TipoDeAnticipo.eTDA_COBRADO
                vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoCaja)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoCobrado)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoOtrosIngresos)));
                if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaCreditoBancario"))) {
                    vEstancompletas = (vEstancompletas && fLasCuentasDeCreditoBancarioEstanCompletas(insReglasDeContabilizacion));
                }
            } else if((vTipoDocumento == 1)) { // enum_TipoDeAnticipo.eTDA_PAGADO 
                vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoBanco)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoPagado)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoOtrosEgresos)));
                if((LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesGetBool("Parametros","ManejaDebitoBancario"))) {
                    vEstancompletas = (vEstancompletas && fLasCuentasDeDebitoBancarioEstanCompletas(insReglasDeContabilizacion));
                }
            } else {
                vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoCaja)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoCobrado)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoOtrosIngresos)));
                vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoBanco)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoPagado)));
                vEstancompletas = vEstancompletas && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaAnticipoOtrosEgresos)));
                vEstancompletas = (vEstancompletas && fLasCuentasDeCreditoBancarioEstanCompletas(insReglasDeContabilizacion));
                vEstancompletas = (vEstancompletas && fLasCuentasDeDebitoBancarioEstanCompletas(insReglasDeContabilizacion));
            }
            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDeInventarioEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCostoDeVenta)))
                           && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaInventario)));
            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDePagoSueldoEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CtaDePagosSueldosBanco)))
                           && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CtaDePagosSueldos)));
            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDeReposicionEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCajaChicaGasto)))
                              && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCajaChicaBanco)))
                              && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCajaChicaBancoDebe)))
                              && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCajaChicaBancoHaber)));
            vResult = vEstancompletas;
            return vResult;
        }


        bool fLasCuentasDeCreditoBancarioEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCreditoBancarioBancos)))
                           && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaCreditoBancarioGasto)));
            vResult = vEstancompletas;
            return vResult;
        }

        bool fLasCuentasDeDebitoBancarioEstanCompletas(ReglasDeContabilizacion insReglasDeContabilizacion) {
            bool vResult = false;
            bool vEstancompletas;
            vEstancompletas = (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaDebitoBancarioBancos)))
                           && (!LibString.IsNullOrEmpty(LibString.Trim(insReglasDeContabilizacion.CuentaDebitoBancarioGasto)));
            vResult = vEstancompletas;
            return vResult;
        }

        public int valTipoDocumento { get; set; }
    } //End of class clsReglasDeContabilizacionNav

} //End of namespace Galac.Saw.Brl.Contabilizacion

