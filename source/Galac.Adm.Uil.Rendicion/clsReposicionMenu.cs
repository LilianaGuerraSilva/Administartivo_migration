using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;
using LibGalac.Aos.Uil;
using Galac.Adm.Ccl.CajaChica;
using System.Windows.Data;

namespace Galac.Adm.Uil.CajaChica {
    public class clsReposicionMenu: ILibMenuMultiFileStr { 
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsReposicionMenu() {
        }
        #endregion //Constructores
        #region Metodos Generados 
         
        #region Miembros de ILibMenuMultiFile
        void ILibMenuMultiFile.Ejecuta(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues) {
            if (valAction == eAccionSR.Insertar) {
                clsReposicionIpl insReposicion = new clsReposicionIpl((LibXmlMemInfo)refGlobalValues[LibGlobalValues.NameAppXmlInfo], (LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]);
                frmReposicionInput insFrmInput = new frmReposicionInput("Reposicion", eAccionSR.Insertar, "");
                insFrmInput.InitLookAndFeelAndSetValues(insReposicion.ListRendicion, insReposicion);
                if (valUseInterop == 0) {
                    insFrmInput.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmInput.Show();
                } else {
                    insFrmInput.ShowDialog();
                }
            } else {
                List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
                vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.ConsecutivoCompania", ((LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]).GetInt("Compania").ToString(), false, typeof(int)));
                vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.TipoDeDocumento", LibConvert.EnumToDbValue((int)eTipoDeDocumentoRendicion.Reposicion), false, typeof(eTipoDeDocumentoRendicion)));
                
                if (valAction == eAccionSR.Modificar || valAction == eAccionSR.Eliminar || valAction == eAccionSR.Cerrar)
                    vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.StatusRendicion", ((int)eStatusRendicion.EnProceso).ToString(), false, typeof(eStatusRendicion)));
                if (valAction == eAccionSR.Anular)
                    vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.StatusRendicion", ((int)eStatusRendicion.Cerrada).ToString(), false, typeof(eStatusRendicion)));
                LibFrmSearch insFrmSearch = new LibFrmSearch("Reposicion", new Galac.Adm.Uil.CajaChica.Sch.GSReposicionSch());
                insFrmSearch.DbQuery = "ADM.Gp_RendicionSCH";
                insFrmSearch.LeaveOpenOnSelect = true;
                insFrmSearch.Entity = new clsReposicionList((LibXmlMemInfo)refGlobalValues[LibGlobalValues.NameAppXmlInfo], (LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]);
                insFrmSearch.CurrentAction = valAction;
                insFrmSearch.FixedCriteria = vFixedValues;
                if (valUseInterop == 0) {
                    insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                    insFrmSearch.Show();
                } else {
                    insFrmSearch.ShowDialog();
                }
            }
        }
        #endregion //Miembros de ILibMenu
        #endregion //Metodos Generados
        #region Metodos Creados
        string ILibMenuMultiFileStr.EjecutaStr(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues)
        {
            string vResult = "";
            clsReposicionIpl insReposicion =new clsReposicionIpl((LibXmlMemInfo)refGlobalValues[LibGlobalValues.NameAppXmlInfo], (LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]);
            if (valAction == eAccionSR.Insertar)
            {
                configurarFrmParaInsertar(valUseInterop, insReposicion);
            }
            else
            {
                vResult = configurarParaOtrasOperaciones(valAction, valUseInterop, refGlobalValues, vResult, insReposicion);

            }

            return vResult;

        }

        private  string configurarParaOtrasOperaciones(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues, string vResult, clsReposicionIpl insReposicion)
        {
            string msj;
            int consecutivo = getConsecutivoDocumento(refGlobalValues);
            if (consecutivo > 0)
            {
                msj = completarOperacion(valAction, refGlobalValues, ref vResult, insReposicion, consecutivo);

            }
            else
            {
                vResult = iniciarOperacion(valAction, valUseInterop, refGlobalValues, vResult);
            }
            return vResult;
        }

        private void configurarFrmParaInsertar(int valUseInterop, clsReposicionIpl insReposicion)
        {
            frmReposicionInput insFrmInput = new frmReposicionInput("Reposicion", eAccionSR.Insertar, "");
            insFrmInput.InitLookAndFeelAndSetValues(insReposicion.ListRendicion, insReposicion);
            if (valUseInterop == 0)
            {
                insFrmInput.Owner = System.Windows.Application.Current.MainWindow;
                insFrmInput.Show();
            }
            else
            {
                insFrmInput.ShowDialog();
            }
        }

        private  string iniciarOperacion(eAccionSR valAction, int valUseInterop, IDictionary<string, XmlDocument> refGlobalValues, string vResult)
        {
            LibFrmSearch insFrmSearch = configurarLibFrmSearch(valAction, refGlobalValues);
            if (valUseInterop == 0)
            {
                insFrmSearch.Owner = System.Windows.Application.Current.MainWindow;
                insFrmSearch.Show();
            }
            else
            {  
                insFrmSearch.ShowDialog();
            }
            vResult = ((clsReposicionList)insFrmSearch.Entity).ResultadoOperacion;
            return vResult;
        }

        private static LibFrmSearch configurarLibFrmSearch(eAccionSR valAction, IDictionary<string, XmlDocument> refGlobalValues)
        {
            List<LibSearchDefaultValues> vFixedValues = new List<LibSearchDefaultValues>();
            vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.ConsecutivoCompania", ((LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]).GetInt("Compania").ToString(), false, typeof(int)));
            vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.TipoDeDocumento", LibConvert.EnumToDbValue((int)eTipoDeDocumentoRendicion.Reposicion), false, typeof(eTipoDeDocumentoRendicion)));

            if (valAction == eAccionSR.Modificar || valAction == eAccionSR.Eliminar || valAction == eAccionSR.Cerrar)
                vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.StatusRendicion", ((int)eStatusRendicion.EnProceso).ToString(), false, typeof(eStatusRendicion)));
            if (valAction == eAccionSR.Anular || valAction == eAccionSR.Contabilizar || valAction == eAccionSR.ReImprimir)
                vFixedValues.Add(new LibSearchDefaultValues("ADM.Gv_Rendicion_B1.StatusRendicion", ((int)eStatusRendicion.Cerrada).ToString(), false, typeof(eStatusRendicion)));

            Galac.Adm.Uil.CajaChica.Sch.GSReposicionSch ReposicionSch = new Galac.Adm.Uil.CajaChica.Sch.GSReposicionSch();
            LibFrmSearch insFrmSearch = new LibFrmSearch("Reposicion", ReposicionSch);

            if (valAction == eAccionSR.Contabilizar)
            {
                insFrmSearch.DbQuery = "ContabilizarRendicion";
                ReposicionSch.lblStatusRendicion.Visibility = System.Windows.Visibility.Hidden;
                ReposicionSch.cmbStatusRendicion.Visibility = System.Windows.Visibility.Hidden;
                ReposicionSch.cmbSortOrder.Items.RemoveAt(ReposicionSch.cmbSortOrder.Items.Count - 1);
                RemoveColumnEstadoSCH(ReposicionSch);

            }
            else
            {
                insFrmSearch.DbQuery = "ADM.Gp_RendicionSCH";
            }

            insFrmSearch.LeaveOpenOnSelect = true;
            insFrmSearch.Entity = new clsReposicionList((LibXmlMemInfo)refGlobalValues[LibGlobalValues.NameAppXmlInfo], (LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]);
            insFrmSearch.CurrentAction = valAction;
            insFrmSearch.FixedCriteria = vFixedValues;
            return insFrmSearch;
        }

        private static void RemoveColumnEstadoSCH(Galac.Adm.Uil.CajaChica.Sch.GSReposicionSch ReposicionSch)
        {
            var doc = ((XmlDataProvider)ReposicionSch.Resources["listFormat"]).Document;
            var nodos = doc.GetElementsByTagName("Field");
            XmlNode Nodo = null;
            foreach (XmlNode nodo in nodos)
            {
                if (nodo.Attributes["Header"].InnerText.Equals("Estado"))
                {
                    Nodo = nodo;
                    break;
                }
            }
            ((XmlDataProvider)ReposicionSch.Resources["listFormat"]).Document.FirstChild.RemoveChild(Nodo);
        }

        private  string completarOperacion(eAccionSR valAction, IDictionary<string, XmlDocument> refGlobalValues, ref string vResult, clsReposicionIpl insReposicion, int consecutivo)
        {
            string msj;
            insReposicion.FindAndSetObject(((LibXmlMFC)refGlobalValues[LibGlobalValues.NameMFCInfo]).GetInt("Compania"), consecutivo);

            if (!((ILibView)insReposicion).SpecializedUpdateRecord(insReposicion.ListRendicion[0], valAction.ToString(), out msj))
            {
                LibNotifier.Alert(null, msj, "Error");
            }
            else
            {


                vResult = ((clsReposicionIpl)insReposicion).ResultadoOperacion;
            }
            return msj;
        }

        private  int getConsecutivoDocumento(IDictionary<string, XmlDocument> refGlobalValues)
        {
            return ((LibXmlMemInfo)refGlobalValues[LibGlobalValues.NameAppXmlInfo]).GlobalValuesGetInt("DatosDocumento", "Consecutivo");
        }
        
        #endregion
    
    
    } //End of class clsRendicionesMenu

} //End of namespace Galac.Saw.Uil.Rendicion

