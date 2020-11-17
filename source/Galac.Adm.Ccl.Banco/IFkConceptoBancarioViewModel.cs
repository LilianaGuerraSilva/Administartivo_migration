namespace Galac.Adm.Ccl.Banco {
    public interface IFkConceptoBancarioViewModel
    {
        int Consecutivo { get; set; }
        string Codigo { get; set; }
        string Descripcion { get; set; }
        eIngresoEgreso Tipo { get; set; }
    }
}
