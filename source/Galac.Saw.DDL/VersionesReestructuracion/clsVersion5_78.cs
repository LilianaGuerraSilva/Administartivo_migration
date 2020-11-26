using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.Dal;
using LibGalac.Aos.Base;
using LibGalac.Aos.Dal.Usal;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.SttDef;


namespace Galac.Saw.DDL.VersionesReestructuracion {
   class clsVersion5_78 : clsVersionARestructurar {

      public clsVersion5_78(string valCurrentDataBaseName)
         : base(valCurrentDataBaseName) {
         _VersionDataBase = "5.78";
      }

      public override bool UpdateToVersion() {
         StartConnectionNoTransaction();
         CrearParametrosParaCodigosConPesoPrecio();
         DisposeConnectionNoTransaction();
         return true;
      }

      private void CrearParametrosParaCodigosConPesoPrecio() {
          AgregarNuevoParametro("UsaPesoEnCodigo", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.String, "", 'N', "N");
          AgregarNuevoParametro("PrefijoCodigoPeso", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.String, "", 'N', "");
          AgregarNuevoParametro("NumDigitosCodigoArticuloPeso", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("PosicionCodigoArticuloPeso", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("NumDigitosPeso", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("NumDecimalesPeso", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("UsaPrecioEnCodigo", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.String, "", 'N', "N");
          AgregarNuevoParametro("PrefijoCodigoPrecio", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.String, "", 'N', "");
          AgregarNuevoParametro("NumDigitosCodigoArticuloPrecio", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("PosicionCodigoArticuloPrecio", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("NumDigitosPrecio", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("NumDecimalesPrecio", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.Int, "", 'N', "0");
          AgregarNuevoParametro("PrecioIncluyeIva", "Factura", 2, "2.7.- Balanza - Etiqueta", 7, "", eTipoDeDatoParametros.String, "", 'N', "S");
      }      
   }
}
