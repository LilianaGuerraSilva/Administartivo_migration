using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Brl;
using LibGalac.Aos.Base.Dal;
using Galac.Saw.Ccl.Tablas;

namespace Galac.Saw.Brl.Tablas {
    public partial class clsRutaDeComercializacionNav: LibBaseNav<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>>, IRutaDeComercializacionPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsRutaDeComercializacionNav() {
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>> GetDataInstance() {
            return new Galac.Saw.Dal.Tablas.clsRutaDeComercializacionDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule, eAccionSR valAction, string valExtendedAction, XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsRutaDeComercializacionDat();
            switch (valCallingModule) {
                default:
                    vResult = true;
                    break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Saw.Dal.Tablas.clsRutaDeComercializacionDat();
            return instanciaDal.ConnectFk(ref refXmlDocument, eProcessMessageType.SpName, "Saw.Gp_RutaDeComercializacionSCH", valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule, StringBuilder valParameters) {
            ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>> instanciaDal = new Galac.Saw.Dal.Tablas.clsRutaDeComercializacionDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName, "Saw.Gp_RutaDeComercializacionGetFk", valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule, ref XmlDocument refXmlDocument, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch (valModule) {
                case "Ruta de Comercialización":
                    vResult = ((ILibPdn)this).GetDataForList(valModule, ref refXmlDocument, valXmlParamsExpression);
                    break;
                default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<RutaDeComercializacion> refData) {
        }

        XElement IRutaDeComercializacionPdn.FindByConsecutivoCompaniaDescripcion(int valConsecutivoCompania, string valDescripcion) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania", valConsecutivoCompania);
            vParams.AddInString("Descripcion", valDescripcion, 100);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Saw.RutaDeComercializacion");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Descripcion = @Descripcion");
            return LibBusiness.ExecuteSelect(SQL.ToString(), vParams.Get(), "", -1);
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo        

        bool IRutaDeComercializacionPdn.InsertarRutaDeComercializacionPorDefecto(int valConsecutivoCompania) {
            try {
                ILibDataComponent<IList<RutaDeComercializacion>, IList<RutaDeComercializacion>> instanciaDal = new Dal.Tablas.clsRutaDeComercializacionDat();
                IList<RutaDeComercializacion> vLista = new List<RutaDeComercializacion>();
                RutaDeComercializacion vCurrentRecord = new Galac.Saw.Ccl.Tablas.RutaDeComercializacion();
                vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
                vCurrentRecord.Consecutivo = 0;
                vCurrentRecord.Descripcion = "NO ASIGNADA";
                vCurrentRecord.NombreOperador = "JEFE";
                vCurrentRecord.FechaUltimaModificacion = LibDate.Today();
                vLista.Add(vCurrentRecord);
                return instanciaDal.Insert(vLista).Success;
            } catch (Exception) {
                throw;
            }            
        }

        /* Codigo de Ejemplo
        private List<RutaDeComercializacion> ParseToListEntity(XElement valXmlEntity) {
            List<RutaDeComercializacion> vResult = new List<RutaDeComercializacion>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                RutaDeComercializacion vRecord = new RutaDeComercializacion();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Descripcion"), null))) {
                    vRecord.Descripcion = vItem.Element("Descripcion").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("NombreOperador"), null))) {
                    vRecord.NombreOperador = vItem.Element("NombreOperador").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("FechaUltimaModificacion"), null))) {
                    vRecord.FechaUltimaModificacion = LibConvert.ToDate(vItem.Element("FechaUltimaModificacion"));
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo
    } //End of class clsRutaDeComercializacionNav

} //End of namespace Galac.Saw.Brl.Tablas

