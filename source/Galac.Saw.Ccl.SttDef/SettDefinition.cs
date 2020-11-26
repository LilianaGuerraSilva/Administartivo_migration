using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml;
using LibGalac.Aos.Base;
using LibGalac.Aos.Catching;
using Galac.Saw.Ccl.SttDef;

namespace Galac.Saw.Ccl.SttDef {
    [Serializable]
    public class SettDefinition {
        #region Variables
        private string _Name;
        private string _Module;
        private int _LevelModule;
        private string _GroupName;
        private int _LevelGroup;
        private string _Label;
        private eTipoDeDatoParametros _DataType;
        private string _Validationrules;
        private bool _IsSetForAllEnterprise;
        private long _fldTimeStamp;
        XmlDocument _datos;
        #endregion //Variables
        #region Propiedades

        public string Name {
            get { return _Name; }
            set { _Name = LibString.Mid(value, 0, 50); }
        }

        public string Module {
            get { return _Module; }
            set { _Module = LibString.Mid(value, 0, 50); }
        }

        public int LevelModule {
            get { return _LevelModule; }
            set { _LevelModule = value; }
        }

        public string GroupName {
            get { return _GroupName; }
            set { _GroupName = LibString.Mid(value, 0, 50); }
        }

        public int LevelGroup {
            get { return _LevelGroup; }
            set { _LevelGroup = value; }
        }

        public string Label {
            get { return _Label; }
            set { _Label = LibString.Mid(value, 0, 50); }
        }

        public eTipoDeDatoParametros DataTypeAsEnum {
            get { return _DataType; }
            set { _DataType = value; }
        }

        public string DataType {
            set { _DataType = (eTipoDeDatoParametros)LibConvert.DbValueToEnum(value); }
        }

        public string DataTypeAsDB {
            get { return LibConvert.EnumToDbValue((int) _DataType); }
        }

        public string DataTypeAsString {
            get { return LibEnumHelper.GetDescription(_DataType); }
        }

        public string Validationrules {
            get { return _Validationrules; }
            set { _Validationrules = LibString.Mid(value, 0, 300); }
        }

        public bool IsSetForAllEnterpriseAsBool {
            get { return _IsSetForAllEnterprise; }
            set { _IsSetForAllEnterprise = value; }
        }

        public string IsSetForAllEnterprise {
            set { _IsSetForAllEnterprise = LibConvert.SNToBool(value); }
        }


        public long fldTimeStamp {
            get { return _fldTimeStamp; }
            set { _fldTimeStamp = value; }
        }

        public XmlDocument Datos {
            get { return _datos; }
            set { _datos = value; }
        }
        #endregion //Propiedades
        #region Constructores

        public SettDefinition() {
            Clear();
        }
        #endregion //Constructores
        #region Metodos Generados

        public object TextDateLastModifiedForInput() {
            return "";
        }

        public void Clear() {
            Name = string.Empty;
            Module = string.Empty;
            LevelModule = 0;
            GroupName = string.Empty;
            LevelGroup = 0;
            Label = string.Empty;
            DataTypeAsEnum = eTipoDeDatoParametros.Enumerativo;
            Validationrules = string.Empty;
            IsSetForAllEnterpriseAsBool = false;
            fldTimeStamp = 0;
        }

        public SettDefinition Clone() {
            SettDefinition vResult = new SettDefinition();
            vResult.Name = _Name;
            vResult.Module = _Module;
            vResult.LevelModule = _LevelModule;
            vResult.GroupName = _GroupName;
            vResult.LevelGroup = _LevelGroup;
            vResult.Label = _Label;
            vResult.DataTypeAsEnum = _DataType;
            vResult.Validationrules = _Validationrules;
            vResult.IsSetForAllEnterpriseAsBool = _IsSetForAllEnterprise;
            vResult.fldTimeStamp = _fldTimeStamp;
            return vResult;
        }

        public override string ToString() {
           return "Name = " + _Name +
               "\nNombre Módulo = " + _Module +
               "\nNivel Modulo = " + _LevelModule.ToString() +
               "\nNombre del Grupo = " + _GroupName +
               "\nNivel del Grupo = " + _LevelGroup.ToString() +
               "\nEtiqueta = " + _Label +
               "\nTipo de Dato = " + _DataType.ToString() +
               "\nReglasValidacion = " + _Validationrules +
               "\nIs Set For All Enterprise = " + _IsSetForAllEnterprise;
        }
        #endregion //Metodos Generados


    } //End of class SettDefinition

} //End of namespace Galac.Saw.Ccl.SttDef

