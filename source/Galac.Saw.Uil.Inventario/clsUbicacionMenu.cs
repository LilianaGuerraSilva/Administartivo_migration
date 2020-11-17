using Galac.Saw.Brl.Inventario;
using Galac.Saw.Uil.Inventario.ViewModel;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.UI.Mvvm.Messaging;
using LibGalac.Aos.Uil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace Galac.Saw.Uil.Inventario {
    
        public class clsUbicacionMenu {
            private FkArticuloInventarioViewModel _ConexionArticulo;
            private static UbicacionViewModel _vUbicacionViewModel;

            public FkArticuloInventarioViewModel ConexionArticulo {
                get { return _ConexionArticulo; }
                set { if (_ConexionArticulo != value) _ConexionArticulo = value; }
            }
     
            public clsUbicacionMenu() {
            } 

            private void SetArticulo(ref FkArticuloInventarioViewModel refConexionArticulo) {
                refConexionArticulo.Ubicacion = refConexionArticulo.Ubicacion;
            }

            public static bool ChooseFromInterop(ref XmlDocument refXmlDocument, List<LibSearchDefaultValues> valSearchCriteria, List<LibSearchDefaultValues> valFixedCriteria) {
                return LibFKRetrievalHelper.ChooseRecord<FkArticuloInventarioViewModel>("Artículo Inventario", ref refXmlDocument, valSearchCriteria, valFixedCriteria, new clsArticuloInventarioNav());
            }
     
            public void MostrarPantallaUbicacionArticulos(int valConsecutivoCompania, int valConsecutivoAlmacen, XElement valXmlCodigoArticulos){
                UbicacionViewModel vViewModel = new UbicacionViewModel();
                vViewModel.BuscarUbicacionDeArticulos(valConsecutivoCompania, valConsecutivoAlmacen, valXmlCodigoArticulos);
                LibMessages.EditViewModel.ShowEditor(vViewModel, true);
				
            } 

        }
}
