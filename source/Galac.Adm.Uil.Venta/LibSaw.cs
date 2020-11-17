using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using LibGalac.Aos.Base;
using LibGalac.Aos.UI.Wpf;

namespace Galac.Adm.Uil.Venta {
    class LibSaw {

        #region GotFocusedPropertyName
        public static readonly DependencyProperty GotFocusPropertyNameProperty = DependencyProperty.RegisterAttached("GotFocusPropertyName", typeof(string), typeof(LibFocusManager), new UIPropertyMetadata(GotFocusPropertyNamePropertyChangedCallback));

        public static string GetGotFocusPropertyName(DependencyObject element) {
            return (string)element.GetValue(GotFocusPropertyNameProperty);
        }

        public static void SetGotFocusPropertyName(DependencyObject element, string value) {
            element.SetValue(GotFocusPropertyNameProperty, value);
        }

        private static void GotFocusPropertyNamePropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            var target = o as FrameworkElement;
            if (target == null) {
                return;
            }
            target.DataContextChanged += new DependencyPropertyChangedEventHandler(targetGotFocus_DataContextChanged);
            target.Unloaded += new RoutedEventHandler(targetGotFocus_Unloaded);
        }

        static void targetGotFocus_Unloaded(object sender, RoutedEventArgs e) {
            try {
                var target = sender as FrameworkElement;
                if (target == null) {
                    return;
                }
                target.DataContextChanged -= new DependencyPropertyChangedEventHandler(targetGotFocus_DataContextChanged);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception) { }
        }

        static void targetGotFocus_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var target = sender as FrameworkElement;
            if (target == null) {
                return;
            }
            target.GotFocus += (s, ars) => {
                try {
                    bool vProperty = LibReflection.GetPropertyValue<bool>(target.DataContext, GetCancelGotFocusPropertyName(target));

                } catch (System.AccessViolationException) {
                    throw;
                } catch (System.Exception) { }
            };
        }
        #endregion

        #region CancelGotFocusedPropertyName
        public static readonly DependencyProperty CancelGotFocusPropertyNameProperty = DependencyProperty.RegisterAttached("CancelGotFocusPropertyName", typeof(string), typeof(LibFocusManager), new UIPropertyMetadata(CancelGotFocusPropertyNamePropertyChangedCallback));

        public static string GetCancelGotFocusPropertyName(DependencyObject element) {
            return (string)element.GetValue(CancelGotFocusPropertyNameProperty);
        }

        public static void SetCancelGotFocusPropertyName(DependencyObject element, string value) {
            element.SetValue(CancelGotFocusPropertyNameProperty, value);
        }

        private static void CancelGotFocusPropertyNamePropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs e) {
            var target = o as FrameworkElement;
            if (target == null) {
                return;
            }
            target.DataContextChanged += new DependencyPropertyChangedEventHandler(targetCancelGotFocus_DataContextChanged);
            target.Unloaded += new RoutedEventHandler(targetCancelGotFocus_Unloaded);
        }

        static void targetCancelGotFocus_Unloaded(object sender, RoutedEventArgs e) {
            try {
                var target = sender as FrameworkElement;
                if (target == null) {
                    return;
                }
                target.DataContextChanged -= new DependencyPropertyChangedEventHandler(targetCancelGotFocus_DataContextChanged);
            } catch (System.AccessViolationException) {
                throw;
            } catch (System.Exception) { }
        }

        static void targetCancelGotFocus_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var target = sender as FrameworkElement;
            if (target == null) {
                return;
            }
            target.PreviewGotKeyboardFocus += (s, ars) => {
                try {
                    bool vProperty = LibReflection.GetPropertyValue<bool>(target.DataContext, GetCancelGotFocusPropertyName(target));
                    ars.Handled = vProperty;
                    LibGalac.Aos.UI.WpfControls.GSTextBoxWpf vFocusedControl = System.Windows.Input.Keyboard.FocusedElement as LibGalac.Aos.UI.WpfControls.GSTextBoxWpf;
                    if (vProperty) {
                        vFocusedControl.SelectAll();
                    }
                    //else {
                    //    LibReflection.SetPropertyValue(target.DataContext, GetCancelGotFocusPropertyName(target), true);
                    //}
                } catch (System.AccessViolationException) {
                    throw;
                } catch (System.Exception) { }
            };
        }
        #endregion

        
    }
}
