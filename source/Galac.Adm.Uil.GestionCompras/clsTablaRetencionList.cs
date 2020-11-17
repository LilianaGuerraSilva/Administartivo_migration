using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.GestionCompras;

namespace Galac.Adm.Uil.GestionCompras {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsTablaRetencionList: LibUic, ILibDbSearchModule {
        #region Variables
        TablaRetencion _Record;
        ILibBusinessComponentWithSearch<IList<TablaRetencion>, IList<TablaRetencion>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public TablaRetencion Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsTablaRetencionList() {
            _Record = new TablaRetencion();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<TablaRetencion>, IList<TablaRetencion>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.GestionCompras.clsTablaRetencionNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<TablaRetencion> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            int vTipoDePersona = insParser.GetEnum(0, "TipoDePersona", (int) eTipodePersonaRetencion.PJ_Domiciliada);
            vParams.AddInEnum("TipoDePersona", LibConvert.EnumToDbValue(vTipoDePersona));
            string vCodigo = insParser.GetString(0, "Codigo","");
            vParams.AddInString("Codigo", vCodigo, 6);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<TablaRetencion> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "TablaRetencionGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsTablaRetencionIpl vRecord = new clsTablaRetencionIpl();
                        vRecord.ListTablaRetencion = vResulset;
                        frmTablaRetencionInput insFrmInput = new frmTablaRetencionInput("Tabla Retencion", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListTablaRetencion, vRecord)) {
                            LibApiAwp.SetOwnerToWindow(insFrmInput, refCallingForm);
                            insFrmInput.Show();
                        } else {
                            insFrmInput.Close();
                        }
                    } else {
                        throw new GalacAlertException("No se puede escoger el registro.");
                    }
                } else {
                    throw new GalacAlertException("El conjunto de registros seleccionados por usted en la búsqueda original ha sido modificado en la base de datos."
                        + "\r\nPor favor, salga de la lista y vuelva a ejecutar la consulta para que se hagan visibles los cambios de los otros usuarios.");
                }
            }
        }

        bool ILibDbSearch.GetData(string valQuery, ref XmlDocument refResulset, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule("Tabla Retencion", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
        //    //return new Galac.Adm.Brl.GestionCompras.Reportes.clsTablaRetencionRpt();
            return null;
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseTablaRetencion(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.GestionCompras.Sch.GSTablaRetencionSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Tabla Retencion", new clsTablaRetencionList());
            return vResult;
        }

        public static bool ChooseMoneda(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Comun.Uil.TablasGen.Sch.GSMonedaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Moneda", new clsTablaRetencionList());
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsTablaRetencionList

} //End of namespace Galac.Adm.Uil.GestionCompras

