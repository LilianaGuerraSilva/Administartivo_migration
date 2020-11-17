using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Cliente;
namespace Galac.Saw.Uil.Cliente.ViewModel {

      public class FkClienteViewModel : IFkClienteViewModel {
          [LibGridColum("Código")]
          public string Codigo { get; set; }
          [LibGridColum("Nombre", Width = 300)]
          public string Nombre { get; set; }
          [LibGridColum("N° R.I.F.")]
          public string NumeroRIF { get; set; }
          public int ConsecutivoCompania { get; set; }
          public int Consecutivo { get; set; }
          public string StatusStr { get; set; }
          public string Direccion { get; set; }
          public DateTime ClienteDesdeFecha { get; set; }
          public string TipoDeContribuyente { get; set; }
    }
	

}
