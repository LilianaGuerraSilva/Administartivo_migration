using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Galac.Saw.LibWebConnector {
    #region TheFactory
    internal struct stUserLoginCnn {
        public string usuario {
            get; set;
        }
        public string clave {
            get; set;
        }
    }
    public struct stRespuestaTF {
        [JsonProperty("token")]
        public string token {
            get; set;
        }

        [JsonProperty("expiracion")]
        public string expiracion {
            get; set;
        }

        [JsonProperty("codigo")]
        public string codigo {
            get; set;
        }

        [JsonProperty("mensaje")]
        public string mensaje {
            get; set;
        }

        [JsonProperty("validaciones")]
        public List<string> validaciones {
            get; set;
        }

        [JsonProperty("resultado")]
        public stResultadoTF resultados {
            get; set;
        }

        [JsonProperty("estado")]
        public stEstado estado {
            get; set;
        }

        public bool Aprobado {
            get; set;
        }

    }
    public struct stResultadoTF {
        [JsonProperty("tipoDocumento")]
        public string tipoDocumento {
            get; set;
        }

        [JsonProperty("numeroDocumento")]
        public string numeroDocumento {
            get; set;
        }

        [JsonProperty("numeroControl")]
        public string numeroControl {
            get; set;
        }

        [JsonProperty("fechaAsignacionNumeroControl")]
        public string fechaAsignacionNumeroControl {
            get; set;
        }

        [JsonProperty("horaAsignacionNumeroControl")]
        public string horaAsignacionNumeroControl {
            get; set;
        }

        [JsonProperty("estado")]
        public string Estado {
            get; set;
        }
    }
    public struct stEstado {
        [JsonProperty("estadoDocumento")]
        public string estadoDocumento {
            get; set;
        }

        [JsonProperty("tipoDocumento")]
        public string tipoDocumento {
            get; set;
        }

        [JsonProperty("numeroDocumento")]
        public string numeroDocumento {
            get; set;
        }

        [JsonProperty("numeroControl")]
        public string numeroControl {
            get; set;
        }

        [JsonProperty("fechaAsignacion")]
        public string fechaAsignacion {
            get; set;
        }

        [JsonProperty("horaAsignacion")]
        public string horaAsignacion {
            get; set;
        }
    }
    public struct stSolicitudDeAccion {
        [JsonProperty("serie")]
        public string Serie {
            get; set;
        }
        [JsonProperty("tipoDocumento")]
        public string TipoDocumento {
            get; set;
        }
        [JsonProperty("numeroDocumento")]
        public string NumeroDocumento {
            get; set;
        }
        [JsonProperty("motivoAnulacion")]
        public string MotivoAnulacion {
            get; set;
        }
    }
    public struct stSolicitudDeConsulta {
        [JsonProperty("serie")]
        public string Serie {
            get; set;
        }
        [JsonProperty("tipoDocumento")]
        public string TipoDocumento {
            get; set;
        }
        [JsonProperty("numeroDocumento")]
        public string NumeroDocumento {
            get; set;
        }
    }
    #endregion TheFactory    
    /////////////////////////////////////////////////////////////////////////////////////////    
    #region NOVUS
    public struct stErrorRespuestaNV {
        [JsonProperty("message")]
        public string messageNV {
            get; set;
        }
        [JsonProperty("code")]
        public string codeNV {
            get; set;
        }
    }

    public struct stDataRespuestaNV {
        [JsonProperty("numerodocumento")]
        public string numerodocumento {
            get; set;
        }
        [JsonProperty("identificador")]
        public string identificador {
            get; set;
        }
        [JsonProperty("corelativo")]
        public string corelativo {
            get; set;
        }
        [JsonProperty("documento")]
        public string documento {
            get; set;
        }
        [JsonProperty("datetime")]
        public DateTime datetimeNV {
            get; set;
        }
        [JsonProperty("fecha")]
        public string fecha {
            get; set;
        }
        [JsonProperty("hora")]
        public string hora {
            get; set;
        }
    }

    public struct stRespuestaNV {
        [JsonProperty("success")]
        public bool success {
            get; set;
        }
        [JsonProperty("message")]
        public string messageNV {
            get; set;
        }
        [JsonProperty("data")]
        public stDataRespuestaNV? dataNV {
            get; set;
        }
        [JsonProperty("error")]
        public stErrorRespuestaNV? errorNV {
            get; set;
        }

    }

    public struct stDataRespuestaStatusNV {
        [JsonProperty("numerodocumento")]
        public string numerodocumento {
            get; set;
        }
        [JsonProperty("numerointerno")]
        public string numerointerno {
            get; set;
        }
        [JsonProperty("fecha")]
        public string fecha {
            get; set;
        }
        [JsonProperty("documento")]
        public string documento {
            get; set;
        }
        [JsonProperty("idtipodocumento")]
        public string idtipodocumento {
            get; set;
        }

    }

    public struct stRespuestaStatusNV {
        [JsonProperty("success")]
        public bool success {
            get; set;
        }
        [JsonProperty("message")]
        public string messageNV {
            get; set;
        }
        [JsonProperty("data")]
        public stDataRespuestaStatusNV?[] dataNV {
            get; set;
        }
        [JsonProperty("error")]
        public stErrorRespuestaNV? errorNV {
            get; set;
        }
    }
    #endregion NOVUS
    /////////////////////////////////////////////////////////////////////////////////////////    
    #region UNIDIGITAL	
    public struct stRespuestaUD {
        public bool Exitoso {
            get; set;
        }
        public bool hasErrors {
            get; set;
        }
        public string NumeroControl {
            get; set;
        }
        public string TipoDocumento {
            get; set;
        }
        public string FechaAsignacion {
            get; set;
        }
        [JsonProperty("Message")]
        public string MessageUD {
            get; set;
        }
        public string StrongeID {
            get; set;
        }
        public string Codigo {
            get; set;
        }
        public string tokenUD {
            get; set;
        }
        public string[] result {
            get; set;
        }

        [JsonProperty("errors")]
        public errorsUD[] errorsUD {
            get; set;
        }
        public string[] success {
            get; set;
        }
        public string[] information {
            get; set;
        }

    }

    public struct errorsUD {
        [JsonProperty("code")]
        public string codeUD {
            get; set;
        }
        [JsonProperty("message")]
        public string messageUD {
            get; set;
        }
        public string extra {
            get; set;
        }

    }

    public struct stRespuestaLoginUD {
        [JsonProperty("userName")]
        public string userName {
            get; set;
        }
        [JsonProperty("accessToken")]
        public string accessToken {
            get; set;
        }
        [JsonProperty("refreshToken")]
        public string refreshToken {
            get; set;
        }

        [JsonProperty("series")]
        public seriesUD[] seriesUD {
            get; set;
        }

        [JsonProperty("templates")]
        public templatesUD[] templatesUD {
            get; set;
        }
        public string[] information {
            get; set;
        }
        [JsonProperty("errors")]
        public errorsUD[] errorsUD {
            get; set;
        }

    }

    public struct seriesUD {
        [JsonProperty("name")]
        public string nameUD {
            get; set;
        }
        public string strongId {
            get; set;
        }
    }

    public struct templatesUD {
        [JsonProperty("name")]
        public string nameUD {
            get; set;
        }
        public string description {
            get; set;
        }
        public string created {
            get; set;
        }
    }

    public struct stRespuestaEnvioUD {
        [JsonProperty("result")]
        public string resultsUD {
            get; set;
        }
        [JsonProperty("errors")]
        public errorsUD[] errorsUD {
            get; set;
        }
        public string[] information {
            get; set;
        }
        public string[] success {
            get; set;
        }
        public bool hasErrors {
            get; set;
        }

    }

    public struct stRespuestaErrorEnvioUD {
        [JsonProperty("result")]
        public envioResultsUD resultsUD {
            get; set;
        }
        [JsonProperty("errors")]
        public errorsUD[] errorsUD {
            get; set;
        }
        public string[] information {
            get; set;
        }
        public string[] success {
            get; set;
        }
        public bool hasErrors {
            get; set;
        }

    }

    public struct envioResultsUD {
        [JsonProperty("result")]
        public string resultUD {
            get; set;
        }
        [JsonProperty("errors")]
        public ErrorsenvioUD[] errorsUD {
            get; set;
        }
    }

    public struct ErrorsenvioUD {
        public int internalPosition {
            get; set;
        }
        public string documentType {
            get; set;
        }
        [JsonProperty("number")]
        public int numberUD {
            get; set;
        }
        public string fiscalRegistry {
            get; set;
        }
        [JsonProperty("name")]
        public string nameUD {
            get; set;
        }
        public DateTime emissionDateAndTime {
            get; set;
        }
        public string address {
            get; set;
        }

        [JsonProperty("currency")]
        public string currencyUD {
            get; set;
        }

        [JsonProperty("errors")]
        public internalErrorEnvioUD[] ErroresInternos {
            get; set;
        }

        [JsonProperty("message")]
        public string messageUD {
            get; set;
        }
        [JsonProperty("code")]
        public string codeUD {
            get; set;
        }
        public string extra {
            get; set;
        }
        public string batch {
            get; set;
        }
    }

    public struct internalErrorEnvioUD {
        public string errorType {
            get; set;
        }
        public string whatIsEval {
            get; set;
        }
        public string errorMessage {
            get; set;
        }
    }

    public struct stRespuestaStatusUD {
        [JsonProperty("result")]
        public stResultStatusUD[] resultUD {
            get; set;
        }
        [JsonProperty("errors")]
        public errorsUD[] errorsUD {
            get; set;
        }
        public string[] information {
            get; set;
        }
        public string[] success {
            get; set;
        }
        public bool hasErrors {
            get; set;
        }
    }

    public struct stResultStatusUD {
        public string strongId {
            get; set;
        }
        [JsonProperty("control")]
        public string controlUD {
            get; set;
        }
        [JsonProperty("number")]
        public string numberUD {
            get; set;
        }
        public string emissionDate {
            get; set;
        }
        public string documentType {
            get; set;
        }
        [JsonProperty("name")]
        public string nameUD {
            get; set;
        }
        public decimal total {
            get; set;
        }

        [JsonProperty("currency")]
        public string currencyUD {
            get; set;
        }
        public bool annulled {
            get; set;
        }
    }

    public struct stRespuestaStatusGUIDUD {
        public stResultStatusGUID? result {
            get; set;
        }
        [JsonProperty("errors")]
        public errorsUD[] errorsUD {
            get; set;
        }
        public string[] information {
            get; set;
        }
        public string[] success {
            get; set;
        }
        public bool hasErrors {
            get; set;
        }
    }

    public struct stResultStatusGUID {
        public string strongId {
            get; set;
        }
        public string codeName {
            get; set;
        }
        public string serieStrongId {
            get; set;
        }
        [JsonProperty("serie")]
        public string serieUD {
            get; set;
        }
        [JsonProperty("number")]
        public int numberUD {
            get; set;
        }
        public string fiscalRegistry {
            get; set;
        }
        [JsonProperty("name")]
        public string nameUD {
            get; set;
        }
        [JsonProperty("currency")]
        public string currencyUD {
            get; set;
        }
        public decimal exemptAmount {
            get; set;
        }
        public decimal taxBase {
            get; set;
        }
        public decimal taxBaseReduced {
            get; set;
        }
        public decimal taxAmount {
            get; set;
        }
        public decimal total {
            get; set;
        }
        public decimal igtfBaseAmount {
            get; set;
        }
        public decimal igtfAmount {
            get; set;
        }
        public decimal grandTotal {
            get; set;
        }
        public string controlNumber {
            get; set;
        }
        public string batchStrongId {
            get; set;
        }
        [JsonProperty("status")]
        public string statusUD {
            get; set;
        }
    }
    #endregion UNIDIGITAL    
}
