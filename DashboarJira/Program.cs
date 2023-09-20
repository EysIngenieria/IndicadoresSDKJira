﻿// See https://aka.ms/new-console-template for more information
using DashboarJira.Controller;
using DashboarJira.Model;
using DashboarJira.Services;
using System.Data;

JiraAccess jira = new JiraAccess();
//DbConnector dbConnector = new DbConnector();

// Retrieve messages as JSON
//string messagesJson = dbConnector.GetMessagesAsJson();
//Console.WriteLine(messagesJson);

// Retrieve messages as string representation
//string messagesString = dbConnector.GetMessagesAsString();
//Console.WriteLine(messagesString);
/*
IAIOController iaio = new IAIOController(jira);
IANOController iano = new IANOController(jira);
ICPMController icpm = new ICPMController(jira);
IEPMController iepm = new IEPMController(jira);
RAIOController raio = new RAIOController(jira);
*/
//RANOController rano = new RANOController(jira);
/*
IRFController IRF = new IRFController(jira);
*/
var fechainicio = "2023-05-01";
var fechaFinal = "2023-09-01";

/*
Console.WriteLine("IAIO: " + iaio.IAIOGeneral(fechainicio, fechaFinal).CalcularIndicadorIAIO());
Console.WriteLine("IAIO CONTRATISTA: " + iaio.IAIOContratista(fechainicio, fechaFinal).CalcularIndicadorIAIO());
Console.WriteLine("IAIO NO CONTRATISTA: " + iaio.IAIONoContratista(fechainicio, fechaFinal).CalcularIndicadorIAIO());
Console.WriteLine("IANO: " + iano.IANOGeneral(fechainicio, fechaFinal).CalcularIndicadorIANO());
Console.WriteLine("IANO contratista: " + iano.IANOContratista(fechainicio, fechaFinal).CalcularIndicadorIANO());
Console.WriteLine("IANO no contratista : " + iano.IANO_NO_Contratista(fechainicio, fechaFinal).CalcularIndicadorIANO());

Console.WriteLine("ICPM MTTO: " + icpm.ICPM_MTTO(fechainicio, fechaFinal).CalcularIndicadorICPM());
Console.WriteLine("ICPM itts: " + icpm.ICPM_ITTS(fechainicio, fechaFinal).CalcularIndicadorICPM());
Console.WriteLine("ICPM PUERTAS: " + icpm.ICPM_PUERTAS(fechainicio, fechaFinal).CalcularIndicadorICPM());
Console.WriteLine("ICPM RFID: " + icpm.ICPM_RFID(fechainicio, fechaFinal).CalcularIndicadorICPM());

Console.WriteLine("IEPM " + iepm.IEPM_GENERAL(fechainicio, fechaFinal).CalcularIndicadorIEPM());
Console.WriteLine("IEPM contratista " + iepm.IEPM_CONTRATISTA(fechainicio, fechaFinal).CalcularIndicadorIEPM());
Console.WriteLine("IEPM no contratista " + iepm.IEPM_NO_CONTRATISTA(fechainicio, fechaFinal).CalcularIndicadorIEPM());

Console.WriteLine("raio " + raio.RAIOGeneral(fechainicio, fechaFinal).CacularIndicadorRAIO());
Console.WriteLine("raio contratista " + raio.RAIOContratista(fechainicio, fechaFinal).CacularIndicadorRAIO());
Console.WriteLine("raio no contratista" + raio.RAIONoContratista(fechainicio, fechaFinal).CacularIndicadorRAIO());

Console.WriteLine("RANO " + rano.RANOGeneral(fechainicio, fechaFinal).CalcularIndicadorRANO());
Console.WriteLine("RANO contratista " + rano.RANOContratista(fechainicio, fechaFinal).CalcularIndicadorRANO());
*/
//Console.WriteLine("RANO no contratista: " + rano.RANONoContratista(fechainicio, fechaFinal).CalcularIndicadorRANO());
///*
//Console.WriteLine("IRF " + IRF.IRFGeneral(fechainicio, fechaFinal).calculoIRF());
//*/
//Indicadores indicadores = new Indicadores();
/*

foreach (IndicadoresEntity indicador in indicadores.ObtenerIndicadores("2023-05-01", "2023-06-01"))
{
    Console.WriteLine($"Nombre: {indicador.nombre}");
    Console.WriteLine($"Cálculo: {indicador.calculo}");
    Console.WriteLine($"Descripción: {indicador.descripcion}");
    Console.WriteLine();
}
Console.WriteLine();


byte[] bytes = jira.getIssueJira("TICKET-100").Archivos; // Aquí debes obtener tus bytes desde la fuente deseada

string rutaCompleta = Path.Combine("C:", "Users", "DesarrolloJC", "Desktop");

// Crea un FileStream para escribir los bytes en el archivo
using (FileStream archivo = new FileStream(rutaCompleta, FileMode.Create))
{
    archivo.Write(bytes, 0, bytes.Length);
}
*/
//Console.WriteLine("Archivo creado exitosamente.");
//byte[] bytes = jira.getIssueJira("TICKET-136").Archivos;
//jira.GetAttachmentImages("TICKET-136");

//var tickets  = jira.GetTikets(0, 0, fechainicio, fechaFinal, null);
//foreach (var indicador in jira.GetTikets(0, 0, fechainicio, fechaFinal, null))
//{
//    Console.WriteLine($"Nombre: {indicador.fecha_apertura.ToString()}");
//}
// Llamar a la función para obtener la DataTable
DataTable dataTable = jira.getEstaciones();

// Verificar si la DataTable contiene datos
if (dataTable.Rows.Count > 0)
{
    // Iterar a través de las filas de la DataTable
    foreach (DataRow row in dataTable.Rows)
    {
        // Acceder a los valores de las columnas por nombre o índice
        int id = (int)row["Id"];
        int idEstacion = (int)row["idEstacion"];
        string nombreEstacion = row["nombreEstacion"].ToString();

        // Imprimir los valores en la consola
        Console.WriteLine($"Id: {id}, Id Estación: {idEstacion}, Nombre Estación: {nombreEstacion}");
    }
}
else
{
    Console.WriteLine("La tabla no contiene datos.");
}
