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
    public sealed class clsMaquinaFiscalList: LibUicMF, ILibDbSearchModule {
        #region Variables
        MaquinaFiscal _Record;
        ILibBusinessComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public MaquinaFiscal Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsMaquinaFiscalList(LibXmlMemInfo initAppMemoryInfo, LibXmlMFC initMFC):base(initAppMemoryInfo, initMFC) {
            _Record = new MaquinaFiscal();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<MaquinaFiscal>, IList<MaquinaFiscal>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Tablas.clsMaquinaFiscalNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<MaquinaFiscal> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            int vConsecutivoCompania = insParser.GetInt(0, "ConsecutivoCompania",0);
            vParams.AddInInteger("ConsecutivoCompania", vConsecutivoCompania);
            string vConsecutivoMaquinaFiscal = insParser.GetString(0, "ConsecutivoMaquinaFiscal","");
            vParams.AddInString("ConsecutivoMaquinaFiscal", vConsecutivoMaquinaFiscal, 9);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<MaquinaFiscal> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "MaquinaFiscalGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsMaquinaFiscalIpl vRecord = new clsMaquinaFiscalIpl(AppMemoryInfo, Mfc);
                        vRecord.ListMaquinaFiscal = vResulset;
                        frmMaquinaFiscalInput insFrmInput = new frmMaquinaFiscalInput("Máquina Fiscal", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListMaquinaFiscal, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Máquina Fiscal", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return new Galac.Saw.Brl.Tablas.Reportes.clsMaquinaFiscalRpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseMaquinaFiscal(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Tablas.Sch.GSMaquinaFiscalSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Máquina Fiscal", new clsMaquinaFiscalList(null, null));
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsMaquinaFiscalList

} //End of namespace Galac.Saw.Uil.Tablas

