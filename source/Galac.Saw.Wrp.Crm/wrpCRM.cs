using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Cib;

#if IsExeBsF
namespace Galac.SawBsF.Wrp.Crm {
#else
namespace Galac.Saw.Wrp.Crm {
#endif
    [ClassInterface(ClassInterfaceType.None)]
   public class wrpCRM :System.EnterpriseServices.ServicedComponent, Galac.Saw.Wrp.Crm.IwrpCRM {
#region Variables
      string _Title = "Conexion con CRM;";
#endregion //Variables
#region Propiedades

      private string Title {
         get {
            return _Title;
         }
      }
#endregion //Propiedades

      void Galac.Saw.Wrp.Crm.IwrpCRM.IncializeConfig(string vfwPath) {
         try {
            LibWrp.SetAppConfigToCurrentDomain(vfwPath);
         } catch(Exception vEx) {
            if(vEx is AccessViolationException) {
               throw;
            }
            throw new GalacWrapperException(Title + " - Inicializar", vEx);
         }
      }

      string  Galac.Saw.Wrp.Crm.IwrpCRM.wsActualizarFactura(string GUIDFactura, string NumeroFactura, string NumeroControl, string CodigoCliente, DateTime FechaFactura, string UserName) {
         string  result = new Galac.Saw.Wrp.Crm.CRMServiceReference.Service1Client().ActualizarFactura(GUIDFactura, NumeroFactura, NumeroControl, CodigoCliente, FechaFactura, UserName);
         return result;
      }

      string Galac.Saw.Wrp.Crm.IwrpCRM.wsAnularFactura(string GUIDFactura) {
         string result = new Galac.Saw.Wrp.Crm.CRMServiceReference.Service1Client().AnularFactura(GUIDFactura);
         return result;
      }
   }
}
