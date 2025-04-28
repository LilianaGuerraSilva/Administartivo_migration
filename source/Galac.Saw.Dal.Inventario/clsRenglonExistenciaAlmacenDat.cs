using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using LibGalac.Aos.Base;
using LibGalac.Aos.Base.Dal;
using LibGalac.Aos.Catching;
using LibGalac.Aos.Dal;
using LibGalac.Aos.DefGen;
using Galac.Saw.Ccl.Inventario;

namespace Galac.Saw.Dal.Inventario {
    public class clsRenglonExistenciaAlmacenDat: LibData, ILibDataComponentWithSearch<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>> {
        #region Variables
        RenglonExistenciaAlmacen _CurrentRecord;
        #endregion //Variables
        #region Propiedades
        private RenglonExistenciaAlmacen CurrentRecord {
            get { return _CurrentRecord; }
            set { _CurrentRecord = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public clsRenglonExistenciaAlmacenDat() {
            DbSchema = "dbo";
        }
        #endregion //Constructores
        #region Metodos Generados

        #region Miembros de ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>

        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.CanBeChoosen(IList<RenglonExistenciaAlmacen> refRecord, eAccionSR valAction) {
            LibResponse vResult = new LibResponse();

                return vResult;
        }

        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.Delete(IList<RenglonExistenciaAlmacen> refRecord) {
            LibResponse vResult = new LibResponse();

            return vResult;
        }

        IList<RenglonExistenciaAlmacen> ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.GetData(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            List<RenglonExistenciaAlmacen> vResult = new List<RenglonExistenciaAlmacen>();

            return vResult;
        }

        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.Insert(IList<RenglonExistenciaAlmacen> refRecord) {
            LibResponse vResult = new LibResponse();

            return vResult;
        }

        XElement ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.QueryInfo(eProcessMessageType valType, string valProcessMessage, StringBuilder valParameters) {
            XElement vResult = null;
            LibDatabase insDb = new LibDatabase();
            switch (valType) {
                case eProcessMessageType.SpName:
                    vResult = LibXml.ToXElement(insDb.LoadFromSp(valProcessMessage, valParameters, CmdTimeOut));
                    break;
                case eProcessMessageType.Query:
                    vResult = LibXml.ToXElement(insDb.LoadData(valProcessMessage, CmdTimeOut, valParameters));
                    break;
                default: throw new ProgrammerMissingCodeException();
            }
            insDb.Dispose();
            return vResult;
        }

        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.Update(IList<RenglonExistenciaAlmacen> refRecord) {
            LibResponse vResult = new LibResponse();

            return vResult;
        }

        bool ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.ValidateAll(IList<RenglonExistenciaAlmacen> refRecords, eAccionSR valAction, StringBuilder refErrorMessage) {
            bool vResult = true;
            return vResult;
        }

        LibResponse ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>.SpecializedUpdate(IList<RenglonExistenciaAlmacen> refRecord, string valSpecializedAction) {
            throw new ProgrammerMissingCodeException();
        }
        #endregion //ILibDataComponent<IList<RenglonExistenciaAlmacen>, IList<RenglonExistenciaAlmacen>>
        #region Validaciones
        protected override bool Validate(eAccionSR valAction, out string outErrorMessage) {
            bool vResult = true;
            ClearValidationInfo();

            outErrorMessage = Information.ToString();
            return vResult;
        }

        #endregion //Validaciones
        #region Miembros de ILibDataFKSearch
        bool ILibDataFKSearch.ConnectFk(ref XmlDocument refResulset, eProcessMessageType valType, string valProcessMessage, StringBuilder valXmlParamsExpression) {
            bool vResult = false;
            LibDatabase insDb = new LibDatabase();
            refResulset = insDb.LoadForConnect(valProcessMessage, valXmlParamsExpression, CmdTimeOut);
            if (refResulset != null && refResulset.DocumentElement != null && refResulset.DocumentElement.HasChildNodes) {
                vResult = true;
            }
            return vResult;
        }
        #endregion //Miembros de ILibDataFKSearch
        #endregion //Metodos Generados


    } //End of class clsRenglonExistenciaAlmacenDat

} //End of namespace Galac.Saw.Dal.Inventario

