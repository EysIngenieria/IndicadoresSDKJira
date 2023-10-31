﻿// See https://aka.ms/new-console-template for more information
using DashboarJira.Controller;
using DashboarJira.Model;
using DashboarJira.Services;
using System.Data;


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
JiraAccess jira = new JiraAccess();
var fechainicio = "2023-05-01";
var fechaFinal = "2023-09-01";


// Llamar a la función para obtener la DataTable
DataTable dataTable = jira.getEstaciones();
List<TicketHV> tickets = jira.GetTicketHVs(0,0, "9110-WA-OR-2");
 jira.ExportTicketsToExcel(tickets);


