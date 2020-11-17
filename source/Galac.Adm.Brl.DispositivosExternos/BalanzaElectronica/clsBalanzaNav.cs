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
using Galac.Adm.Ccl.DispositivosExternos;
using LibGalac.Aos.Catching;
using Galac.Saw.Lib;

namespace Galac.Adm.Brl.DispositivosExternos.BalanzaElectronica {
    public partial class clsBalanzaNav :LibBaseNav<IList<Balanza>,IList<Balanza>>,IBalanzaPdn {
        #region Variables
        #endregion //Variables
        #region Propiedades
        #endregion //Propiedades
        #region Constructores

        public clsBalanzaNav() {           
        }
        #endregion //Constructores
        #region Metodos Generados

        protected override ILibDataComponentWithSearch<IList<Balanza>,IList<Balanza>> GetDataInstance() {
            return new Galac.Adm.Dal.DispositivosExternos.clsBalanzaDat();
        }
        #region Miembros de ILibPdn

        bool ILibPdn.CanBeChoosen(string valCallingModule,eAccionSR valAction,string valExtendedAction,XmlDocument valXmlRow) {
            bool vResult = false;
            //ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.DispositivosExternos.clsBalanzaDat();
            switch(valCallingModule) {
            default:
            vResult = true;
            break;
            }
            return vResult;
        }

        bool ILibPdn.GetDataForList(string valCallingModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            ILibDataFKSearch instanciaDal = new Galac.Adm.Dal.DispositivosExternos.clsBalanzaDat();
            return instanciaDal.ConnectFk(ref refXmlDocument,eProcessMessageType.SpName,"Adm.Gp_BalanzaSCH",valXmlParamsExpression);
        }

        System.Xml.Linq.XElement ILibPdn.GetFk(string valCallingModule,StringBuilder valParameters) {
            ILibDataComponent<IList<Balanza>,IList<Balanza>> instanciaDal = new Galac.Adm.Dal.DispositivosExternos.clsBalanzaDat();
            return instanciaDal.QueryInfo(eProcessMessageType.SpName,"Adm.Gp_BalanzaGetFk",valParameters);
        }
        #endregion //Miembros de ILibPdn

        protected override bool RetrieveListInfo(string valModule,ref XmlDocument refXmlDocument,StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            switch(valModule) {
            case "Balanza":
            vResult = ((ILibPdn)this).GetDataForList(valModule,ref refXmlDocument,valXmlParamsExpression);
            break;
            default: throw new NotImplementedException();
            }
            return vResult;
        }

        protected override void FillWithForeignInfo(ref IList<Balanza> refData) {
        }

        XElement IBalanzaPdn.FindByConsecutivoCompaniaModeloNombre(int valConsecutivoCompania, eModeloDeBalanza valModelo, string valNombre) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInEnum("Modelo",(int)valModelo);
            vParams.AddInString("Nombre",valNombre,40);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.Balanza");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Modelo = @Modelo");
            SQL.AppendLine("AND Nombre = @Nombre");
            return LibBusiness.ExecuteSelect(SQL.ToString(),vParams.Get(),"",-1);
        }
        #endregion //Metodos Generados
        #region Codigo Ejemplo
        /* Codigo de Ejemplo

        bool IBalanzaPdn.InsertDefaultRecord(int valConsecutivoCompania) {
            ILibDataComponent<IList<Balanza>, IList<Balanza>> instanciaDal = new clsBalanzaDat();
            IList<Balanza> vLista = new List<Balanza>();
            Balanza vCurrentRecord = new Galac.Adm.Dal.DispositivosExternosBalanza();
            vCurrentRecord.ConsecutivoCompania = valConsecutivoCompania;
            vCurrentRecord.ConsecutivoCompania = 0;
            vCurrentRecord.Consecutivo = 0;
            vCurrentRecord.ModeloAsEnum = eModeloBalanza.Xacta;
            vCurrentRecord.Nombre = "";
            vCurrentRecord.PuertoAsEnum = ePuerto.COM1;
            vCurrentRecord.BitsDatosAsEnum = eBitsDatos.d6;
            vCurrentRecord.ParidadAsEnum = eParidad.Ninguna;
            vCurrentRecord.BitDeParadaAsEnum = eBitDeParada.Ninguno;
            vCurrentRecord.BaudRateAsEnum = eBaudRate.b600;
            vCurrentRecord.ControlDeFlujoAsEnum = eControlFlujo.Ninguno;
            vLista.Add(vCurrentRecord);
            return instanciaDal.Insert(vLista).Success;
        }

        private List<Balanza> ParseToListEntity(XElement valXmlEntity) {
            List<Balanza> vResult = new List<Balanza>();
            var vEntity = from vRecord in valXmlEntity.Descendants("GpResult")
                          select vRecord;
            foreach (XElement vItem in vEntity) {
                Balanza vRecord = new Balanza();
                vRecord.Clear();
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ConsecutivoCompania"), null))) {
                    vRecord.ConsecutivoCompania = LibConvert.ToInt(vItem.Element("ConsecutivoCompania"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Consecutivo"), null))) {
                    vRecord.Consecutivo = LibConvert.ToInt(vItem.Element("Consecutivo"));
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Modelo"), null))) {
                    vRecord.Modelo = vItem.Element("Modelo").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Nombre"), null))) {
                    vRecord.Nombre = vItem.Element("Nombre").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Puerto"), null))) {
                    vRecord.Puerto = vItem.Element("Puerto").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("BitsDatos"), null))) {
                    vRecord.BitsDatos = vItem.Element("BitsDatos").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("Paridad"), null))) {
                    vRecord.Paridad = vItem.Element("Paridad").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("BitDeParada"), null))) {
                    vRecord.BitDeParada = vItem.Element("BitDeParada").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("BaudRate"), null))) {
                    vRecord.BaudRate = vItem.Element("BaudRate").Value;
                }
                if (!(System.NullReferenceException.ReferenceEquals(vItem.Element("ControlDeFlujo"), null))) {
                    vRecord.ControlDeFlujo = vItem.Element("ControlDeFlujo").Value;
                }
                vResult.Add(vRecord);
            }
            return vResult;
        }
        */
        #endregion //Codigo Ejemplo

        public clsBalanza CreateBalanza(int valConsecutivoCompania,int valConsecutivoBalanza) {
            XElement vBalanzaDat = null;
            vBalanzaDat = FindByConsecutivoBalanza(valConsecutivoCompania,valConsecutivoBalanza);            
            return clsBalanzaCreator.Create(vBalanzaDat);
        }

        private XElement FindByConsecutivoBalanza(int valConsecutivoCompania,int valConsecutivoBalanza) {
            LibGpParams vParams = new LibGpParams();
            vParams.AddInInteger("ConsecutivoCompania",valConsecutivoCompania);
            vParams.AddInEnum("Consecutivo",(int)valConsecutivoBalanza);
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("SELECT * FROM Adm.Balanza");
            SQL.AppendLine("WHERE ConsecutivoCompania = @ConsecutivoCompania");
            SQL.AppendLine("AND Consecutivo = @Consecutivo");
            return LibBusiness.ExecuteSelect(SQL.ToString(),vParams.Get(),"",-1);
        }
		
        public bool EscogerBalanzaEnPOS(int valConsecitivo) {
            bool vResult = false;
            string ConsecutivoBalanza = LibConvert.ToStr(valConsecitivo);
            try {
                ConfigHelper.AddKeyToAppSettings("ConsecutivoBalanza",LibConvert.ToStr(valConsecitivo,0));                
                vResult = true;
            } catch(GalacException) {                
                throw;
            }
            return vResult;
        }        
    } //End of class clsBalanzaNav
} //End of namespace Galac.Adm.Brl.DispositivosExternos

