using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using LibGalac.Aos.UI.Wpf;
using System.Windows;
using LibGalac.Aos.Base;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Uil.SttDef.Input {
    internal interface IInputView {
        void SetNavigatorValuesFromForm();
    }

    public class Tool {

        public static void AllDisabled(UIElementCollection UICol, eAccionSR vAction) {
            if (vAction == eAccionSR.Consultar || vAction == eAccionSR.Eliminar) {
                if (UICol != null) {
                    LibApiAwp.DisableAllFieldsIfActionIn(UICol, (int)vAction, new int[] { (int)eAccionSR.Consultar, (int)eAccionSR.Eliminar });
                    foreach (UIElement aux in UICol) {
                        try {
                            ContentControl cc;
                            Panel p;
                            if (aux is System.Windows.Controls.Button) {
                                ((System.Windows.Controls.Button)aux).IsEnabled = false;
                            }
                            if (aux is System.Windows.Controls.ComboBox) {
                                ((System.Windows.Controls.ComboBox)aux).IsEnabled = false;
                            }
                            if (aux is System.Windows.Controls.CheckBox) {
                                ((System.Windows.Controls.CheckBox)aux).IsEnabled = false;
                            }
                            if (!(aux is System.Windows.Controls.Panel)) {
                                cc = (System.Windows.Controls.ContentControl)aux;
                                p = (System.Windows.Controls.Panel)cc.Content;
                            } else {
                                p = (System.Windows.Controls.Panel)aux;
                            }
                            if (p != null)
                                AllDisabled(p.Children, vAction);
                        } catch (InvalidCastException e) { }

                    }
                }
            }
        }

        //public static List<Module> Modulos() {
        //    GSSettValueByCompany gs = new GSSettValueByCompany();
        //    return ((clsSettValueByCompanyIpl)gs.CurrentModel).ModuleList;
        //}


    }
}
