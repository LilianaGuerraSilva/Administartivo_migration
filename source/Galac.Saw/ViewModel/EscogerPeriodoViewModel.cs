using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Brl.Contracts;
using LibGalac.Aos.Base;
using System.Xml.Linq;
using LibGalac.Aos.Brl;
using System.ComponentModel.Composition;
using LibGalac.Aos.UI.Mvvm;
using System.Collections;
using LibGalac.Aos.UI.Mvvm.Helpers;
using System.ComponentModel.DataAnnotations;
using LibGalac.Aos.UI.Mvvm.Command;
using LibGalac.Aos.UI.Mvvm.Messaging;
using Galac.Contab.Brl.WinCont;
using Galac.Contab.Ccl.WinCont;
using LibGalac.Aos.DefGen;
using LibGalac.Aos.Base.Dal;
using Galac.Contab.Uil.WinCont.ViewModel;

namespace Galac.Saw.ViewModel {
    /// <summary>
    /// Clase temporal por no existir el componente periodo
    /// </summary>
    [Export(typeof(LibMfcViewModelBase))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EscogerPeriodoViewModel : LibMfcViewModelBase {
        #region Constantes
        public const string PeriodoActualPropertyName = "PeriodoActual";
        public const string ArrayPeriodosPropertyName = "ArrayPeriodos";
        #endregion

        #region Variables
        private FkPeriodoViewModel _PeriodoActual = null;
        private List<FkPeriodoViewModel> _ArrayPeriodos = null;
        #endregion

        #region Propiedades
        public int ConsecutivoCompania {
            get;
            set;
        }

        public FkPeriodoViewModel PeriodoActual {
            get {
                return _PeriodoActual;
            }
            set {
                if (_PeriodoActual != value) {
                    _PeriodoActual = value;
                    SetGlobalValues(_PeriodoActual);
                    RaisePropertyChanged(PeriodoActualPropertyName);
                }
            }
        }

        public List<FkPeriodoViewModel> ArrayPeriodos {
            get {
                return _ArrayPeriodos;
            }
            private set {
                if (_ArrayPeriodos != value) {
                    _ArrayPeriodos = value;
                    RaisePropertyChanged(ArrayPeriodosPropertyName);
                }
            }
        }
        #endregion

        #region Métodos
        public EscogerPeriodoViewModel() {
            MFCFieldDisplayName = "Consecutivo Periodo";
            MFCFieldName = "ConsecutivoPeriodo";
            MFCFieldType = typeof(int);
            MFCRecordDisplayName = "Período";
            MFCRecordName = "Periodo";
        }
        public override void InitializeViewModel() {
            //if (LibDefGen.IsStudentEdition()) {
            //    if (!EsValidoComprobanteParaVesionEstudiantil() || !EsValidoPeriodoParaVesionEstudiantil()) {                   

            //    }
            //}
            PeriodoActual = null;
            int vConsecutivoPeriodo = TryGetConsecutivoPeriodo();
            ArrayPeriodos = ObtenerListaDePeriodos();
            if (ArrayPeriodos != null && ArrayPeriodos.Count > 0) {
                var vPeriodo = ArrayPeriodos.Where(i => i.ConsecutivoPeriodo == vConsecutivoPeriodo).FirstOrDefault();
                if (vPeriodo != null) {
                    PeriodoActual = vPeriodo;
                } else {
                    PeriodoActual = ArrayPeriodos.FirstOrDefault();
                }
            } else {
                PeriodoActual = null;
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesAddOrReplace(new XElement("Niveles"), "Niveles");
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesAddOrReplace(new XElement("DefinicionCuentas"), "DefinicionCuentas");
                LibGlobalValues.Instance.GetAppMemInfo().GlobalValuesAddOrReplace(new XElement("Periodo"), "Periodo");
                LibGlobalValues.Instance.LoadMFCInfoFromAppMemInfo("Periodo", "ConsecutivoPeriodo", typeof(int));
                XmlAppMemoryInfo = null;
            }
        }

        private void SetGlobalValues(FkPeriodoViewModel valPeriodo) {
            if (valPeriodo == null) {
                return;
            }
            XmlAppMemoryInfo = ((IPeriodoPdn)new clsPeriodoNav()).SetGlobalValues(valPeriodo.ConsecutivoCompania, valPeriodo.ConsecutivoPeriodo);
        }

        protected override ILibPdn GetBusinessInstance() {
            return new clsPeriodoNav();
        }

        protected override void ExecuteChooseCommand(string valParameter) {

        }

        protected override IEnumerable GetListFromModule(string valModuleName, LibSearchCriteria valCriteria, Type valRecordType, string valOrderByMember) {
            return base.GetListFromModule(valModuleName, valCriteria, valRecordType, valOrderByMember);
        }

        private List<FkPeriodoViewModel> ObtenerListaDePeriodos() {
            List<FkPeriodoViewModel> vResult = new List<FkPeriodoViewModel>();
            ConsecutivoCompania = TryGetConsecutivoCompania();
            if (ConsecutivoCompania > 0) {
                StringBuilder vSqlSb = new StringBuilder();
                vSqlSb.AppendLine("SELECT ConsecutivoCompania, ConsecutivoPeriodo, FechaAperturaDelPeriodo, FechaCierreDelPeriodo");
                vSqlSb.AppendLine("FROM Periodo WHERE ConsecutivoCompania = @ConsecutivoCompania");
                vSqlSb.AppendLine("ORDER BY FechaAperturaDelPeriodo  DESC ");
                XElement vElement = LibBusiness.ExecuteSelect(vSqlSb.ToString(), new LibGpParams().ToXmlParamInt("ConsecutivoCompania", ConsecutivoCompania), string.Empty, -1);
                if (vElement != null) {
                    vResult = LibParserHelper.ParseToList<FkPeriodoViewModel>(LibXml.ToXmlDocument(vElement));
                }
            }
            return vResult;
        }

        public override void OnOtherMfcChanged(string valRecordName, XElement valRecord) {
            try {
                if (LibString.S1IsEqualToS2(valRecordName, "Compania")) {
                    if (valRecord != null && valRecord.HasElements) {
                        ConsecutivoCompania = LibConvert.ToInt(valRecord.Element("ConsecutivoCompania").Value);
                    } else {
                        ConsecutivoCompania = 0;
                    }
                    InitializeViewModel();
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
        }

        private int TryGetConsecutivoCompania() {
            int vResult = 0;
            try {
                vResult = LibGlobalValues.Instance.GetMfcInfo().GetInt("Compania");
            } catch (Exception) {
            }
            if (vResult == 0 && ConsecutivoCompania >= 0) {
                vResult = ConsecutivoCompania;
            }
            return vResult;
        }

        private int TryGetConsecutivoPeriodo() {
            int vResult = 0;
            try {
                vResult = LibGlobalValues.Instance.GetMfcInfo().GetInt("Periodo");
            } catch (Exception) {
            }
            return vResult;
        }

        protected override LibSearchCriteria GetExistAnyParameters() {
            LibSearchCriteria vResult = new LibSearchCriteria();
            ConsecutivoCompania = TryGetConsecutivoCompania();
            vResult.Add("ConsecutivoCompania", ConsecutivoCompania);
            return vResult;
        }

        public override bool ExecuteCreate() {
            bool vResult = false;
            try {
                if (CanCreateOnStart()) {
                    LibMessages.MessageBox.Alert(this, "Aun no estan preparados los CU de periodo, solo el escoger.", "Insertar Primer Período");
                    //LibMessages.MessageBox.Alert(this, "A continuación se pedirán los datos del primer período.", "Insertar Primer Período");

                    //PeriodoViewModel vVieModel = new PeriodoViewModel();
                    //vVieModel.InitializeViewModel(eAccionSR.Insertar);
                    //vResult = LibMessages.EditViewModel.ShowEditor(vVieModel, true);
                }
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception vEx) {
                LibGalac.Aos.UI.Mvvm.Messaging.LibMessages.RaiseError.ShowError(vEx);
            }
            return vResult;
        }
        #endregion
        
        
    }
}