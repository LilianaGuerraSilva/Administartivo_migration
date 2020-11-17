using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Saw.Ccl.Vehiculo;

namespace Galac.Saw.Uil.Vehiculo {
    /// <summary>
    /// Implementación de la Lista para el objeto
    /// </summary>
    public sealed class clsModeloList: LibUic, ILibDbSearchModule {
        #region Variables
        Modelo _Record;
        ILibBusinessComponentWithSearch<IList<Modelo>, IList<Modelo>> _Reglas;
        #endregion //Variables
        #region Propiedades
        public Modelo Record {
            get { return _Record;}
            set {_Record = value;}
        }
        #endregion //Propiedades
        #region Constructores
        public clsModeloList() {
            _Record = new Modelo();
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Inicializacion BRL - a modificar si Remoting
        private void RegistraCliente() {
            if (WorkWithRemoting) {
                _Reglas = (ILibBusinessComponentWithSearch<IList<Modelo>, IList<Modelo>>)RegisterType();
            } else {
                _Reglas = new Galac.Saw.Brl.Vehiculo.clsModeloNav();
            }
        }
        #endregion //Inicializacion BRL - a modificar si Remoting

        public bool CanBeChoosen(IList<Modelo> valRecord, eAccionSR valAction) {
            bool vResult = false;
            //PRIMERO VALIDACIONES DE ESTA CAPA SI LAS HUBIERE, LUEGO IR A NEGOCIO
            RegistraCliente();
            vResult = _Reglas.CanBeChoosen(valRecord, valAction);
            return vResult;
        }

        private StringBuilder GetPkParams(XmlDocument valXmlRowDocument) {
            LibXmlDataParse insParser = new LibXmlDataParse(valXmlRowDocument);
            LibGpParams vParams = new LibGpParams();
            string vNombre = insParser.GetString(0, "Nombre","");
            vParams.AddInString("Nombre", vNombre, 20);
            insParser  = null;
            return vParams.Get();
        }

        #region Miembros de ILibDbSearchModule
        void ILibDbSearch.DoAction(object refCallingForm, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            if (valAction == eAccionSR.Escoger) {
                //do nothing;
            } else {
                RegistraCliente();
                IList<Modelo> vResulset = _Reglas.GetData(eProcessMessageType.SpName, "ModeloGET", GetPkParams(valXmlRow));
                if (vResulset != null && vResulset.Count > 0) {
                    if (_Reglas.CanBeChoosen(vResulset, valAction)) {
                        clsModeloIpl vRecord = new clsModeloIpl();
                        vRecord.ListModelo = vResulset;
                        frmModeloInput insFrmInput = new frmModeloInput("Modelo", valAction, valExtendedAction);
                        if (insFrmInput.InitLookAndFeelAndSetValues(vRecord.ListModelo, vRecord)) {
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
            vResult = _Reglas.GetDataFromModule("Modelo", ref refResulset, valXmlParamsExpression);
            return vResult;
        }

        LibGalac.Aos.Base.Report.ILibReportInfo ILibDbSearch.GetDataRetrievesInstance() {
            return new Galac.Saw.Brl.Vehiculo.Reportes.clsModeloRpt();
        }
        bool ILibDbSearchModule.GetDataFromModule(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            RegistraCliente();
            vResult = _Reglas.GetDataFromModule(valModule, ref refXmlDocument, valXmlParamsExpression);
            return vResult;
        }
        #endregion //Miembros de ILibDbSearchModule
        #region Métodos para Escoger

        public static bool ChooseModelo(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Vehiculo.Sch.GSModeloSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Modelo", new clsModeloList());
            return vResult;
        }

        public static bool ChooseMarca(System.Windows.Window refOwner, ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
            bool vResult = false;
            vResult = LibFkRetrieval.ChooseRecord(refOwner, new Galac.Saw.Uil.Vehiculo.Sch.GSMarcaSch(), ref refXmlDocument, valSearchCriteria, valFixedCriteria, "Marca", new clsModeloList());
            return vResult;
        }
        #endregion //Métodos para Escoger
        #endregion //Metodos Generados


    } //End of class clsModeloList

} //End of namespace Galac.Saw.Uil.Vehiculo

