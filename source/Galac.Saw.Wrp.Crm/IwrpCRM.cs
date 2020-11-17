using System;

namespace Galac.Saw.Wrp.Crm {
   public interface IwrpCRM {
      void IncializeConfig(string vfwPath);
      string wsActualizarFactura(string GUIDFactura, string NumeroFactura, string NumeroControl, string CodigoCliente, DateTime FechaFactura, string UserName);
      string wsAnularFactura(string GUIDFactura);
   }
}
