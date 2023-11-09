﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboarJira.Model
{
    public class ComponenteHV
    {
        public String IdComponente { get; set; }
        public string Serial { get; set; }
        public int AnioFabricacion { get; set; }
        public string Modelo { get; set; }
        public DateTime FechaInicio { get; set; }
        public Double horasDeOperacion { get; set; }


        public string GetTemplateFileName(string marca)
        {
            // Implementa un switch para asignar el nombre de la plantilla según el modelo
            switch (this.Modelo)
            {
                case "MTE-MT-22":
                    return "Plantilla MTE-MT-22 Assa.xlsx"; 
                case "MTE-SS-22":
                    if (marca == "https://assaabloymda.atlassian.net/") {
                        return "Plantilla MTE-SS-22 Assa.xlsx";
                    }
                    return "Plantilla MTE-SS-22 Nautilus.xlsx";
                case "MTE-TEL-22":
                    if (marca == "https://assaabloymda.atlassian.net/")
                    {
                        return "Plantilla MTE-TEL-22 Assa.xlsx";
                    }
                    return "Plantilla MTE-TEL-22 Nautilus.xlsx";

                default:
                    // Si el modelo no coincide con ninguno de los casos anteriores, usa una plantilla predeterminada
                    return "Plantilla_Predeterminada.xlsx";
            }
        }

    }

}