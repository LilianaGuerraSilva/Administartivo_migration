using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Integracion;

namespace Galac.Saw.Uil.Integracion {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
   public  sealed class clsIntegracionSawList: LibUic, ILibDbSearchModule {
        #region Variables
        IntegracionSaw _Record;
        ILibBusinessComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public IntegracionSaw Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsIntegracionSawList() {
            _Record = new IntegracionSaw();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<IntegracionSaw>, IList<IntegracionSaw>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Integracion.clsIntegracionSawNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<IntegracionSaw> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            int vTipoIntegracion = insParser.GetEnum(0, "TipoIntegracion", (int) eTipoIntegracion.NOMINA);
            vParams.AddInEnum("TipoIntegracion", LibConvert.EnumToDbValue(vTipoIntegracion));
            string vversion = insParser.GetString(0, "version","");
            vParams.AddInString("version", vversion, 8);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<IntegracionSaw> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "IntegracionSawGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsIntegracionSawIpl vRecord = new clsIntegracionSawIpl();
                        vRecord.ListIntegracionSaw = vResulset;
                        frmIntegracionSawInput insFrmInput = new frmIntegracionSawInput("Integracion Saw", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListIntegracionSaw, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Integracion Saw", ref refResulset, valXmlParamsExpression);
            return vResult;
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger
        #endregion //Métodos para Escoger
        public static bool ChooseCompania(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Compania.Controles.GSCompaniaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Compania", new clsIntegracionSawList());
            return vResult;
        }
        #endregion //Metodos Generados




        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            throw new NotImplementedException();
        }
   } //End of class clsIntegracionSawList

} //End of namespace Galac.Saw.Uil.Integracion

