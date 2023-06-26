using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using DashboarJira.Model;
using DashboarJira.Services;
using Microsoft.AspNetCore.Authorization;

namespace MQTT.Web.Controllers
{

    [Authorize]
    public class IndicadoresController : Controller
    {
        public IActionResult Index()
        {
            var identity = User.Identity as System.Security.Claims.ClaimsIdentity;

            if (identity != null && identity.HasClaim(System.Security.Claims.ClaimTypes.Name, "admin@admin.com"))
            {
                ViewBag.Menu = "admin";
            }
            else
            {
                ViewBag.Menu = "user";
                return View();
            }

            return View();
        }


        int start = 0;

        public List<IndicadoresEntity> getIndicadores(string startDate, string endDate)
        {
            try
            {
                string formattedStartDate;
                string formattedEndDate;
                /**/
                if (startDate != null || endDate != null)
                {
                    DateTime startDateTime = DateTime.Parse(startDate);
                    DateTime endDateTime = DateTime.Parse(endDate).AddDays(1).AddSeconds(-1);

                    formattedStartDate = startDateTime.ToString("yyyy-MM-dd");
                    formattedEndDate = endDateTime.ToString("yyyy-MM-dd");
                }
                else
                {
                    formattedStartDate = startDate;
                    formattedEndDate = endDate;
                }

                Indicadores indicadores = new Indicadores();
               Console.WriteLine(indicadores.indicadores(formattedStartDate, formattedEndDate));
                return indicadores.indicadores(formattedStartDate, formattedEndDate);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
