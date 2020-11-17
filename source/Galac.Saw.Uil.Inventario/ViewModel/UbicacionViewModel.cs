using LibGalac.Aos.UI.Mvvm;
using System;
using System.Collections.ObjectModel;
using Galac.Saw.Ccl.Inventario;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using Galac.Saw.Brl.Inventario;

namespace Galac.Saw.Uil.Inventario.ViewModel {
    public class UbicacionViewModel:LibGenericViewModel {

        #region Variables        
        private string _Articulo;
        private string _ConsecutivoCompañia;
        private string _ConsecutivoAlmacen;
        private string _Descripcion;
        private string _Ubicacion;
        private ObservableCollection<UbicacionViewModel> _DetalleUbicacion;
        #endregion

        #region Propiedades
        public override string ModuleName { get => "Ubicación de Artículos"; }
        public string Articulo { get => _Articulo; set => _Articulo = value; }
        public string ConsecutivoCompañia { get => _ConsecutivoCompañia; set => _ConsecutivoCompañia = value; }
        public string ConsecutivoAlmacen { get => _ConsecutivoAlmacen; set => _ConsecutivoAlmacen = value; }
        public string Descripcion { get => _Descripcion; set => _Descripcion = value; }
        public string Ubicacion { get => _Ubicacion; set => _Ubicacion = value; }
        #endregion

        public ObservableCollection<UbicacionViewModel> DetalleUbicacion {
            get { return _DetalleUbicacion; }
            set { _DetalleUbicacion = value; }
        }
     
        public UbicacionViewModel(ObservableCollection<UbicacionViewModel> detalleUbicacion) {
            DetalleUbicacion = detalleUbicacion;
        }

        public UbicacionViewModel(string articulo, string descripcion, string ubicacion) {
            Articulo = articulo;
            Descripcion = descripcion;
            Ubicacion = ubicacion;
        }

        public UbicacionViewModel() {
            DetalleUbicacion = new ObservableCollection<UbicacionViewModel>();
            InitializeViewModel();
        }

        public void InitializeViewModel() {
            Articulo = "";
            ConsecutivoCompañia = "";
            ConsecutivoAlmacen = "";
            Descripcion = "";
            Ubicacion = "";
 
        }

        public void clear() {
            throw new NotImplementedException();
        }

        public void BuscarUbicacionDeArticulos(int valConsecutivoCompania, int valConsecutivoAlmacen, XElement valXmlCodigoArticulos) {
            XElement vXmlSearchArticulos = ((IArticuloInventarioPdn)new clsArticuloInventarioNav()).BuscarUbicacionDeArticulos(valConsecutivoCompania, valConsecutivoAlmacen, valXmlCodigoArticulos);
            setUbicacionArticulos(vXmlSearchArticulos);
        }

        public void setUbicacionArticulos(XElement vXmlArticuloUbicaciones) {
            if (!LibXml.IsEmptyOrNull(LibXml.CreateXmlDocument(vXmlArticuloUbicaciones))) {
                foreach (XElement vItem in vXmlArticuloUbicaciones.Descendants("GpResult")) {
                    string vArticulo = LibConvert.ToStr(vItem.Element("CodigoDelArticulo")?.Value);
                    string vDescripcion = LibConvert.ToStr(vItem.Element("Descripcion")?.Value);
                    string vUbicacion = LibConvert.ToStr(vItem.Element("Ubicacion")?.Value);
                    vUbicacion = vUbicacion == string.Empty ? "Sin Ubicación" : vUbicacion;
                    this.DetalleUbicacion.Add(new UbicacionViewModel(vArticulo, vDescripcion, vUbicacion));
					
                }
            }
        }
    }
}