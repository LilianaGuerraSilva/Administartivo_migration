using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Galac.Saw.Ccl.SttDef;
using LibGalac.Aos.Base;

namespace Galac.Saw.Uil.SttDef.Input {
    /// <summary>
    /// Interaction logic for GSSttDef.xaml
    /// </summary>
    public partial class GSParametros : UserControl {
        public GSParametros() {
            InitializeComponent();
            PopulateData();
            gbMenu.ItemsSource = PopulateData();
        }

        private ObservableCollection<Module> PopulateData() {
            var GroupItems = new ObservableCollection<Module>();
            GroupItems.Add(new Module("Modulo 1Modulo 1Modulo 1Modulo 1", new GroupCollection { new Group("Item 1.1", "Contenido 1.1"), new Group("Item 1.2", "Contenido 1.2") }));
            GroupItems.Add(new Module("Modulo 2", new GroupCollection { new Group("Item 2.1", "Contenido 2.1"), new Group("Item 2.2", "Contenido 2.2") }));
            GroupItems.Add(new Module("Modulo 3", new GroupCollection { new Group("Item 3.1", "Contenido 3.1"), new Group("Item 3.2", "Contenido 3.2"), new Group("Item 3.3", "Contenido 3.3") }));
            return GroupItems;
        }
    }

    //public class Module {
    //    public string DisplayName { get; set; }
    //    public GroupCollection Groups { get; set; }

    //    public Module(string initDisplayName, GroupCollection initGroups) {
    //        DisplayName = initDisplayName;
    //        Groups = initGroups;
    //    }
    //}

    //public class Group {
    //    public string DisplayName { get; set; }
    //    public object Content { get; set; }

    //    public Group(string initDisplayName, object initContent) {
    //        DisplayName = initDisplayName;
    //        Content = initContent;
    //    }
    //}

    //public class GroupCollection : ObservableCollection<Group> {
    //    private Group _SelectedItem;
    //    public Group SelectedItem {
    //        get { return _SelectedItem; }
    //        set {
    //            _SelectedItem = value;
    //            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("SelectedItem"));
    //        }
    //    }
    //}
}
