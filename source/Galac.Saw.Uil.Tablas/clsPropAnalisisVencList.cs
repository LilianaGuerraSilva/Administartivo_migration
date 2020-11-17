using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Uil.Tablas {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsPropAnalisisVencList: LibUic, ILibDbSearchModule {
        #region Variables
        PropAnalisisVenc _Record;
        ILibBusinessComponentWithSearch<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public PropAnalisisVenc Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsPropAnalisisVencList() {
            _Record = new PropAnalisisVenc();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<PropAnalisisVenc>, IList<PropAnalisisVenc>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsPropAnalisisVencNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<PropAnalisisVenc> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            int vSecuencialUnique0 = insParser.GetInt(0, "SecuencialUnique0",0);
            vParams.AddInInteger("SecuencialUnique0", vSecuencialUnique0);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<PropAnalisisVenc> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "PropAnalisisVencGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsPropAnalisisVencIpl vRecord = new clsPropAnalisisVencIpl();
                        vRecord.ListPropAnalisisVenc = vResulset;
                        frmPropAnalisisVencInput insFrmInput = new frmPropAnalisisVencInput("Prop Analisis Venc", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListPropAnalisisVenc, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Prop Analisis Venc", ref refResulset, valXmlParamsExpression);
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
        public static bool ChoosePropAnalisisVenc(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            LibGalac.Aos.Uil.LibFkRetrieval insConexion = new LibGalac.Aos.Uil.LibFkRetrieval();
            LibSearch insSearch = new LibSearch();
            vResult = insSearch.ShowListSelect(refOwner, ref refXmlDocument, valSearchCriteria, "Propiedades de Analisis de Vencimiento", new Galac.Saw.Uil.Tablas.Sch.GSPropAnalisisVencSch(), "Saw.Gp_PropAnalisisVencSCH", false, insConexion, valFixedCriteria);
            insConexion = null;
            insSearch = null;
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsPropAnalisisVencList

} //End of namespace Galac.Saw.Uil.Tablas
