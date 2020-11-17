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
    public sealed class clsFormaDelCobroList: LibUic, ILibDbSearchModule {
        #region Variables
        FormaDelCobro _Record;
        ILibBusinessComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public FormaDelCobro Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsFormaDelCobroList() {
            _Record = new FormaDelCobro();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<FormaDelCobro>, IList<FormaDelCobro>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsFormaDelCobroNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<FormaDelCobro> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            string vCodigo = insParser.GetString(0, "Codigo","");
            vParams.AddInString("Codigo", vCodigo, 5);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<FormaDelCobro> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "FormaDelCobroGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsFormaDelCobroIpl vRecord = new clsFormaDelCobroIpl();
                        vRecord.ListFormaDelCobro = vResulset;
                        frmFormaDelCobroInput insFrmInput = new frmFormaDelCobroInput("Forma Del Cobro", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListFormaDelCobro, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Forma Del Cobro", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return new Galac.Saw.Brl.Tablas.Reportes.clsFormaDelCobroRpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseFormaDelCobro(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Tablas.Sch.GSFormaDelCobroSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Forma Del Cobro", new clsFormaDelCobroList());
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsFormaDelCobroList

} //End of namespace Galac.Saw.Uil.Tablas

