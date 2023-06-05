﻿using DashboarJira.Model;
using DashboarJira.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboarJira.Controller
{
    public class IAIOController
    {
        private readonly double TOTAL_PUERTAS = 146.0;
        const string JQL_GENERAL = "created >= {0} AND created <= {1} AND issuetype = 'Solicitud de Mantenimiento' AND status = Cerrado AND 'Clase de fallo' = AIO AND 'Tipo de componente' = Puerta ORDER BY key DESC, 'Time to resolution' ASC";
        const string JQL_CONTRATISTA = "created >= {0} AND created <= {1} AND issuetype = 'Solicitud de Mantenimiento' AND 'Tipo de causa' = 'A cargo del contratista' AND status = Cerrado AND 'Clase de fallo' = AIO AND 'Tipo de componente' = Puerta ORDER BY key DESC, 'Time to resolution' ASC";
        const string JQL_NO_CONTRATISTA = "created >= {0} AND created <= {1} AND issuetype = 'Solicitud de Mantenimiento' AND 'Tipo de causa' != 'A cargo del contratista' AND status = Cerrado AND 'Clase de fallo' = AIO AND 'Tipo de componente' = Puerta ORDER BY key DESC, 'Time to resolution' ASC";
        JiraAccess jiraAccess;
        public IAIOController(JiraAccess jira)
        {
            jiraAccess = jira;
        }
        
        public IAIOEntity IAIOGeneral(string start, string end) {
            string jql = string.Format(JQL_GENERAL, start, end);
            List<Ticket> total_tickets = jiraAccess.GetTiketsIndicadores(jql);
            return new IAIOEntity(AIO_POR_PUERTA(total_tickets),TOTAL_PUERTAS);
        }
        public IAIOEntity IAIOContratista(string start, string end)
        {
            string jql = string.Format(JQL_CONTRATISTA, start, end);
            List<Ticket> total_tickets = jiraAccess.GetTiketsIndicadores(jql);
            return new IAIOEntity(AIO_POR_PUERTA(total_tickets), TOTAL_PUERTAS);
        }

        public IAIOEntity IAIONoContratista(string start, string end)
        {
            string jql = string.Format(JQL_NO_CONTRATISTA, start, end);
            List<Ticket> total_tickets = jiraAccess.GetTiketsIndicadores(jql);
            return new IAIOEntity(AIO_POR_PUERTA(total_tickets), TOTAL_PUERTAS);
        }

        public List<List<Ticket>> AIO_POR_PUERTA(List<Ticket> Ticket)
        {
            List<List<Ticket>> gruposPuertasAIO = new List<List<Ticket>>();
            
            var ticketANIOPuertaGroup = Ticket.GroupBy(ticket => ticket.id_puerta);


            foreach (var group in ticketANIOPuertaGroup)
            {
                List<Ticket> auxiliar = new List<Ticket>();
                foreach (var ticket in group)
                {
                    auxiliar.Add(ticket);
                }
                gruposPuertasAIO.Add(auxiliar);
            }
            return gruposPuertasAIO;
        }
    }
}
