using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MQTT.Infrastructure.DAL;
using MQTT.Infrastructure.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MQTT.FunctionApp
{
    public static class GetIssuesJira
    {
        [FunctionName("GetIssuesJira")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            var guid = Guid.NewGuid();
            log.LogInformation($"{guid}====== START PROCESS ======");
            string msgError = string.Empty;
            string uri = string.Empty;
            var logRequestIn = new Infrastructure.Models.DTO.LogRequestInDTO();
            logRequestIn.IdEndPoint = (int)EndPointEnum.GetIssueJira;

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStringDB", EnvironmentVariableTarget.Process);
            string token = Environment.GetEnvironmentVariable("TokenJira", EnvironmentVariableTarget.Process).ToString();
            string timeZone = Environment.GetEnvironmentVariable("TimeZone", EnvironmentVariableTarget.Process).ToString();
            //var connectionString = "Server=manatee.database.windows.net;Database=PuertasTransmilenioDB;User Id=administrador;Password=2022/M4n4t334zur3;";
            //string token = "anVhbl9rXzk2MkBob3RtYWlsLmNvbTpxcDlJdHBjVVhOY2VaUHhlRGg3ZjkwOTk=";
            General DBAccess = new General(connectionString);

            try
            {
                log.LogInformation($"{guid}=== Log request in...");
                logRequestIn.DataQuery = req.QueryString.ToString();
                logRequestIn.DataBody = await new StreamReader(req.Body).ReadToEndAsync();
                logRequestIn.DataBody = string.IsNullOrEmpty(logRequestIn.DataBody) ? null : logRequestIn.DataBody;
                LogRequestInDAL.Add(DBAccess, ref logRequestIn);
                log.LogInformation($"{guid}=== IdRequestIn: {logRequestIn.Id}");

                log.LogInformation($"{guid}=== dataSource: {connectionString}");
                log.LogInformation($"{guid}=== token: {token}");

                log.LogInformation($"{guid}=== Getting equivalences...");
                var equivalenceServiceType = EquivalencesDAL.GetAllEquivalences(DBAccess);

                var dateIni = req.Query["InitialDate"];
                log.LogInformation($"{guid}=== Initial Date: {dateIni}");

                var dateEnd = req.Query["EndDate"];
                log.LogInformation($"{guid}=== End Date: {dateEnd}");

                uri = "https://manateecc.atlassian.net/rest/api/2/search";
                string parameters = $"jql=created >= {dateIni} AND created <= {dateEnd}  AND issuetype in ('Solicitud de Mantenimiento') order by created DESC";
                string resultJira;

                uri = $"{uri}?{parameters}";

                log.LogInformation($"{guid}=== Endpoint Jira: {uri}");
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                request.Headers.Add("Authorization", $"Basic {token}");

                try
                {
                    using (var response = request.GetResponse())
                    {
                        using (Stream strReader = response.GetResponseStream())
                        {
                            if (strReader == null) return null;

                            using (StreamReader objReader = new StreamReader(strReader))
                            {
                                resultJira = objReader.ReadToEnd();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    msgError = $"Error at {uri} {ex.Message} {ex.InnerException}";
                    throw new Exception(msgError);
                }

                log.LogInformation($"{guid}=== Response from Jira: {resultJira}");
                var json = JsonConvert.DeserializeObject<Models.Issue>(resultJira);

                log.LogInformation($"{guid}=== Total Issues: {json.Issues.Count}");
                List<Models.IssueDTO> result = new List<Models.IssueDTO>();
                foreach (var item in json.Issues)
                {
                    var fields = item.Fields;

                    var typeSettingConfiguration = (fields.customfield_10075 == null || fields.customfield_10075[0] == null) ? string.Empty : fields.customfield_10075[0].Value;
                    var typeSettingConfiguration2 = (fields.customfield_10076 == null || fields.customfield_10076[0] == null) ? string.Empty : fields.customfield_10076[0].Value;
                    var typeSettingConfiguration3 = (fields.customfield_10077 == null || fields.customfield_10077[0] == null) ? string.Empty : fields.customfield_10077[0].Value;
                    var typeSettingConfiguration4 = (fields.customfield_10078 == null || fields.customfield_10078[0] == null) ? string.Empty : fields.customfield_10078[0].Value;
                    var typeSettingConfiguration5 = (fields.customfield_10086 == null || fields.customfield_10086[0] == null) ? string.Empty : fields.customfield_10086[0].Value;

                    Models.IssueDTO issueDTO = new Models.IssueDTO
                    {
                        id_ticket = item.Key,
                        idEstacion = fields.customfield_10057 == null ? null : fields.customfield_10057.Value,
                        idVagon = fields.customfield_10058 == null ? null : fields.customfield_10058.Value,
                        Puerta = fields.customfield_10060 != null ? fields.customfield_10060 : null,
                        Componente = fields.customfield_10088 == null ? null : fields.customfield_10088.Value,
                        Identificacion = fields.customfield_10059 != null ? fields.customfield_10059 : null,
                        tipo_mantenimiento = fields.customfield_10061 == null ? null : fields.customfield_10061.Value,
                        nivel_falla = fields.customfield_10064 == null ? null : fields.customfield_10064.Value,
                        codigo_falla = (fields.customfield_10069 == null || fields.customfield_10069[0] == null) ? null : fields.customfield_10069[0].Value,
                        Fecha_apertura = fields.created != null ? TimeZoneInfo.ConvertTime(Convert.ToDateTime(fields.created), TimeZoneInfo.FindSystemTimeZoneById(timeZone)) : (DateTime?)null,
                        fecha_cierre = fields.customfield_10101 != null ? TimeZoneInfo.ConvertTime(Convert.ToDateTime(fields.customfield_10101), TimeZoneInfo.FindSystemTimeZoneById(timeZone)) : (DateTime?)null,
                        fecha_arribo_locacion = fields.customfield_10071 != null ? TimeZoneInfo.ConvertTime(Convert.ToDateTime(fields.customfield_10071), TimeZoneInfo.FindSystemTimeZoneById(timeZone)) : (DateTime?)null,
                        Componente_Parte = (fields.customfield_10072 == null || fields.customfield_10072[0] == null) ? null : fields.customfield_10072[0].Value,
                        Tipo_reparacion = (fields.customfield_10081 == null || fields.customfield_10081[0] == null) ? null : fields.customfield_10081[0].Value,
                        Tipo_ajuste_configuracion = $"{typeSettingConfiguration}{typeSettingConfiguration2}{typeSettingConfiguration3}{typeSettingConfiguration4}{typeSettingConfiguration5}",
                        Descripcion_reparacion = fields.description,
                        Diagnostico_causa = fields.customfield_10067 != null ? fields.customfield_10067.Value : null,
                        Estado_ticket = fields.status == null ? string.Empty : fields.status.name
                    };

                    var equivalence = equivalenceServiceType.Where(e => e.Name == issueDTO.tipo_mantenimiento).Select(e => e.Value).FirstOrDefault();
                    issueDTO.tipo_mantenimiento = string.IsNullOrEmpty(equivalence) ? issueDTO.tipo_mantenimiento : equivalence;

                    equivalence = string.Empty;
                    equivalence = equivalenceServiceType.Where(e => e.Name == issueDTO.Estado_ticket).Select(e => e.Value).FirstOrDefault();
                    issueDTO.Estado_ticket = string.IsNullOrEmpty(equivalence) ? "Abierta" : equivalence;

                    result.Add(issueDTO);
                }

                log.LogInformation($"{guid}==== END PROCESS ======");
                logRequestIn.Processed = true;
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                msgError = $"{ex.Message} {ex.InnerException}|Uri:{uri}";
                log.LogError($"{guid}===ERROR: {msgError}");
                logRequestIn.Observations = msgError;
                return new ConflictObjectResult(msgError);
            }
            finally
            {
                LogRequestInDAL.Update(DBAccess, logRequestIn);
            }
        }
    }
}
