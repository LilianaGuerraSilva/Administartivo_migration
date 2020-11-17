using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGalac.Aos.UI.Mvvm.Helpers;
using Galac.Saw.Ccl.Cliente;
using Galac.Saw.Ccl.Inventario;
using LibGalac.Aos.UI.Mvvm.Command;
using System.ComponentModel;
namespace Galac.Saw.Uil.Vehiculo.ViewModel {

    public class FkMarcaViewModel : Galac.Saw.Ccl.Vehiculo.IFkMarcaViewModel {        
        [LibGridColum("Nombre", Width=300)]
        public string Nombre { get; set; }
    }
    public class FkModeloViewModel : Galac.Saw.Ccl.Vehiculo.IFkModeloViewModel, INotifyPropertyChanged {
        private string _Marca;
        public FkModeloViewModel(){
            Marca = ""; 
            ChooseMarcaCommand = new RelayCommand<string>(ExecuteChooseMarcaCommand);
        }

        [LibGridColum("Nombre", DbMemberPath = "Saw.Gv_Modelo_B1.Nombre", Width=300)]
        public string Nombre { get; set; }
        [LibGridColum("Marca", Type = eGridColumType.Connection, ConnectionDisplayMemberPath = "Nombre", ConnectionModelPropertyName = "Marca", ConnectionSearchCommandName = "ChooseMarcaCommand", DbMemberPath = "Saw.Gv_Marca_B1.Nombre", Width = 300)]
        public string Marca {
            get {
                return _Marca;
            }
            set {
                _Marca = value;
                 RaisePropertyChanged("MarcaVehiculo");
            }
        }

        public RelayCommand<string> ChooseMarcaCommand {
            get;
            set;
        }
        FkMarcaViewModel ConexionMarca;

        private void ExecuteChooseMarcaCommand(string valNombre) {
            ModeloViewModel vViewModel = new ModeloViewModel();
            vViewModel.ExecuteChooseMarcaCommand(valNombre);
            Marca = vViewModel.ConexionMarca.Nombre;            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string valPropertyName) {
            var handle = PropertyChanged;
            if (handle != null) {
                handle(this, new PropertyChangedEventArgs(valPropertyName));
            }
        }

    }

    public class FkClienteViewModel : IFkClienteViewModel {
        [LibGridColum("Código")]
        public string Codigo { get; set; }
        [LibGridColum("Nombre", DbMemberPath = "dbo.Gv_Cliente_B1.Nombre", Width = 300)]
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
	 public class FkVehiculoViewModel : Galac.Saw.Ccl.Vehiculo.IFkVehiculoViewModel {
        public int ConsecutivoCompania { get; set; }
        public int Consecutivo { get; set; }

        [LibGridColum("Placa", Width=70)]
        public string Placa { get; set; }
        [LibGridColum("Serial VIN", Width=150)]
        public string serialVIN { get; set; }
        [LibGridColum("Modelo", Width=200)]
        public string NombreModelo { get; set; }
        //[LibGridColum("Año del Vehículo")]
        //public int Ano { get; set; }
        [LibGridColum("Código Cliente")]
        public string CodigoCliente { get; set; }
        [LibGridColum("Nombre Cliente", DbMemberPath = "dbo.Gv_Cliente_B1.Nombre")]
        public string NombreCliente { get; set; }
        public string CodigoColor { get; set; }
       //[LibGridColum("Color")]
        //public string DescripcionColor { get; set; }
        //public string Marca { get; set; }
    }

    public class FkColorViewModel : IFkColorViewModel {
       public int ConsecutivoCompania { get; set; }
       [LibGridColum("Código", Width = 200)]
       public string CodigoColor { get; set; }
       [LibGridColum("Descripción", Width = 300 )]
       public string DescripcionColor { get; set; }

    }

}