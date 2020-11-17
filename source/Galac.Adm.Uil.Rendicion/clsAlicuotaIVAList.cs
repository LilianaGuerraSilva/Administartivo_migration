using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.CajaChica;

namespace Galac.Adm.Uil.CajaChica {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsAlicuotaIVAList: LibUic, ILibDbSearchModule {
        #region Variables
        AlicuotaIVA _Record;
        ILibBusinessComponentWithSearch<IList<AlicuotaIVA>, IList<AlicuotaIVA>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public AlicuotaIVA Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsAlicuotaIVAList() {
            _Record = new AlicuotaIVA();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<AlicuotaIVA>, IList<AlicuotaIVA>>)RegisterType();
            } else {
                _Reglas = new Galac.Adm.Brl.CajaChica.clsAlicuotaIVANav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<AlicuotaIVA> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            DateTime vFechaDeInicioDeVigencia = insParser.GetDateTime(0, "FechaDeInicioDeVigencia", LibDate.Today());
            vParams.AddInDateTime ("FechaDeInicioDeVigencia", vFechaDeInicioDeVigencia);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<AlicuotaIVA> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "AlicuotaIVAGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    //if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                    //    clsAlicuotaIVAIpl vRecord = new clsAlicuotaIVAIpl();
                    //    vRecord.ListAlicuotaIVA = vResulset;
                    //    frmAlicuotaIVAInput insFrmInput = new frmAlicuotaIVAInput("Alicuota IVA", valAction, valExtendedAction);
                    //    if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListAlicuotaIVA, vRecord)) {
                    //        LibApiAwp.SetOwnerToWindow(insFrmInput, refCallingForm);
                    //        insFrmInput.Show();
                    //    } else {
                    //        insFrmInput.Close();
                    //    }
                    //} else {
                    //    throw new GalacAlertException("No se puede escoger el registro.");
                    //}
                } else {
                    throw new GalacAlertException("El conjunto de registros seleccionados por usted en la búsqueda original ha sido modificado en la base de datos."
                        + "\r\nPor favor, salga de la lista y vuelva a ejecutar la consulta para que se hagan visibles los cambios de los otros usuarios.");
                }
            }
        }

        bool ILibDbSearch.GetData(string valQuery, ref XmlDocument refResulset, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule("Alicuota IVA", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return null;
            //return new Galac.Adm.Brl.CajaChica.Reportes.clsAlicuotaIVARpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseAlicuotaIVA(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Adm.Uil.CajaChica.Sch.GSAlicuotaIVASch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Alicuota IVA", new clsAlicuotaIVAList());
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsAlicuotaIVAList

} //End of namespace Galac.Dbo.Uil.CajaChica

