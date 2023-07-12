﻿using DashboarJira.Model;
using DashboarJira.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MQTT.Web.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        public IActionResult Index(int max, string componente)
        {
            // Obtiene la identidad del usuario actual
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;

            // Verifica si el usuario tiene el rol de "Administrador"
            if (identity != null && identity.HasClaim(System.Security.Claims.ClaimTypes.Name, "admin@admin.com"))
            {
                ViewBag.Menu = "admin";
            }
            else
            {
                ViewBag.Menu = "user";
            }

            //return View();

            // Obtener la fecha actual
            DateTime currentDateTime = DateTime.Now;

            // Restar un mes a la fecha actual
            DateTime startDateTime = currentDateTime.AddMonths(-1);

            // Formatear las fechas en el formato deseado
           /* string startDate = startDateTime.ToString("yyyy-MM-dd");
            string endDate = currentDateTime.ToString("yyyy-MM-dd");
            max = 0;
            List<Ticket> tickets = getTickets(startDate, endDate, max, componente);
            */return View();
        }


        int start = 0;

        public List<Ticket> getTickets(string startDate, string endDate, int max, string componente)
        {
            try
            {
                string formattedStartDate;
                string formattedEndDate;

                if (startDate != null || endDate != null)
                {
                    //max = 10;
                    DateTime startDateTime = DateTime.Parse(startDate);
                    DateTime endDateTime = DateTime.Parse(endDate).AddDays(1); //agrega 1 día y resta 1 segundo para obtener el final del día

                    formattedStartDate = startDateTime.ToString("yyyy-MM-dd");
                    formattedEndDate = endDateTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    formattedStartDate = startDate;
                    formattedEndDate = endDate;
                }

                JiraAccess jiraAccess = new JiraAccess();
                max = 0;
                return jiraAccess.GetTikets(start, max, formattedStartDate, formattedEndDate, componente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IActionResult consultarTicket(string idTicket)
        {
            try
            {
                JiraAccess jira = new JiraAccess();
                IssueJira ticket = jira.getIssueJira(idTicket);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}