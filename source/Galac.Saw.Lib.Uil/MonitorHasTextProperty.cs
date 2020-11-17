using System.Windows;
using System.Windows.Controls;

namespace Galac.Saw.Lib.Uil
{

    public class MonitorHasTextProperty : AttachedPropertyBaseClass<bool, MonitorHasTextProperty> {

        public override void ValueChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) {
            TextBox element = target as TextBox;
            if (element == null) {
                return;
            }
            if ((bool)e.NewValue) {
                element.TextChanged += Element_TextChanged;
            } else {
                element.TextChanged -= Element_TextChanged;
            }
        }

        private void Element_TextChanged(object sender, TextChangedEventArgs e) {
            TextBox textbox = sender as TextBox;
            HasTextProperty.Instance.ValueChanged(textbox, 
                new DependencyPropertyChangedEventArgs(HasTextProperty.ValueProperty, false, textbox.Text.Length > 0));
        }
    }

    public class HasTextProperty : AttachedPropertyBaseClass<bool, HasTextProperty> {
        
        public override void ValueChanged(DependencyObject target, DependencyPropertyChangedEventArgs e) {
            TextBox element = target as TextBox;
            if (element == null) {
                return;
            }
            if ((bool)e.NewValue) {
                SetValue(element, true);
            } else {
                SetValue(element, false);
            }
        }
    }
}