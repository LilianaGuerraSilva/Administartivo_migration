using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Galac.Saw.Lib.Uil {
    public class DoubleClickCommand : AttachedPropertyBaseClass<ICommand, DoubleClickCommand> {

        public override void ValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            ListBoxItem element = sender as ListBoxItem;
            if (element != null) {
                if (e.NewValue != null && e.OldValue == null) {
                    element.MouseDoubleClick += DoubleClickCommand_MouseDoubleClick;
                } else if (e.NewValue == null && e.OldValue != null) {
                    element.MouseDoubleClick -= DoubleClickCommand_MouseDoubleClick;
                }
            }
        }

        private static void DoubleClickCommand_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            ListBoxItem element = sender as ListBoxItem;
            if (element == null) {
                return;
            }
            ICommand command = (ICommand)element.GetValue(ValueProperty);
            command.Execute(null);
        }
    }
}
