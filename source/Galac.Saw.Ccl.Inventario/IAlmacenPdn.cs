using LibGalac.Aos.Base;

namespace Galac.Saw.Ccl.Inventario {
    public interface IAlmacenPdn : ILibPdn {
        void InsertarAlmacenPorDefecto(int vfwConsecutivoCompania);
        string GetCodigoAlmacenPorDefecto(int valConsecutivoCompania);
        int ObtenerConsecutivoAlmacen(string valCodigoAlmacenGenerico, int valConsecutivoCompania);
    }
}
