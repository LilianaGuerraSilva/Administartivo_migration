using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Contabilizacion;

namespace Galac.Saw.Uil.Contabilizacion {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsReglasDeContabilizacionList: LibUicMF, ILibDbSearchModule {
        #region Variables
        ILibBusinessComponentWithSearch<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>> _Reglas;
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores
        public clsReglasDeContabilizacionList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<ReglasDeContabilizacion>, IList<ReglasDeContabilizacion>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Contabilizacion.clsReglasDeContabilizacionNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            throw new NotImplementedException();
        }

        bool ILibDbSearch.GetData(string valQuery, ref XmlDocument refResulset, StringBuilder valXmlParamsExpression) {
            throw new NotImplementedException();
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            throw new NotImplementedException();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseCuenta(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria, bool valEscogerAuxiliar, bool valUsaAuxiliares, bool valUsaModuloDeActivoFijo, int valConsecutivoPeriodo, XmlReader valListaGrupoDeActivos, string valGetCierreDelEjercicio, LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMfc) {
            bool vResult = false;
            string vMensaje ="";
            if(LibFkRetrieval.ChooseRecord(refOwner, new Galac.Contab.Uil.WinCont.Sch.GSCuentaSch(valUsaAuxiliares, valUsaModuloDeActivoFijo, valListaGrupoDeActivos), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Cuenta", new clsReglasDeContabilizacionList(null, null))) {
               clsReglasDeContabilizacionIpl insReglasDeContabilizacion = new clsReglasDeContabilizacionIpl(  initAppMemoryInfo,  initMfc);
               if (insReglasDeContabilizacion.SePuedeUsarEstaCuenta(valUsaAuxiliares, valUsaModuloDeActivoFijo, valEscogerAuxiliar, valGetCierreDelEjercicio, ref vMensaje, refXmlDocument)) {
                   vResult = true;
               } else {
                   LibNotifier.Information(refOwner, vMensaje, "Información");
               }
           }
            return vResult;
        }

        public static bool ChooseTipoDeComprobante(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Contab.Uil.Tablas.Sch.GSTipoDeComprobanteSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Tipo De Comprobante", new clsReglasDeContabilizacionList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsReglasDeContabilizacionList

} //End of namespace Galac.Saw.Uil.Contabilizacion

