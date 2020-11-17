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
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.UI.Wpf;
namespace Galac.Saw.Uil.Inventario.Controles {
    /// <summary>
    /// Control de Conexion
    /// </summary>
    public partial class GSCategoriaFk: UserControl {
        #region Variables
        private XmlDocument _XmlProperties;
        private bool _CancelValidations = false;
        private string _Title;
        #endregion //Variables
        #region Propiedades

        public XmlDocument XmlProperties {
            get { return _XmlProperties; }
        }

        public bool CancelValidations {
            get { return _CancelValidations; }
            set { _CancelValidations = value; }
        }

        private string Title {
            get { return _Title; }
            set { _Title = value; }
        }
        #endregion //Propiedades
        #region Constructores
        public GSCategoriaFk() {
            InitializeComponent();
            Title = "Categoria";
            InitializeEvents();
        }
        #endregion //Constructores
        #region Metodos Generados

        void InitializeEvents() {
            this.txtDescripcion.Validating += new System.ComponentModel.CancelEventHandler(txtDescripcion_Validating);
        }

        void txtDescripcion_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
            if (_CancelValidations) {
                return;
            }
            try {
                _XmlProperties = new XmlDocument();
                if (clsConexionInventario.ChooseCategoria(LibApiAwp.GetWindow(sender), ref _XmlProperties, null, null)) {
                    LibXmlDataParse insParse = new LibXmlDataParse(_XmlProperties);
                    txtDescripcion.Text = insParse.GetString(0, "Descripcion", "");
                } else {
                    e.Cancel = true;
                }
            } catch (GalacException gEx) {
                LibExceptionDisplay.Show(gEx, null, Title);
            } catch (Exception vEx) {
                LibExceptionDisplay.Show(vEx);
            }
        }
        #endregion //Metodos Generados




    } //End of class GSCategoriaFk.xaml

} //End of namespace Galac.Saw.Uil.Inventario

