﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboarJira.Model
{
    public class IEPMEntity
    {
        public IEPMEntity( List<Ticket> ticketsANP, List<Ticket> ticketsAME)
        {
            this.ticketsANP = ticketsANP;
            this.ticketsAME = ticketsAME;
        }
       

        private List<Ticket> ticketsANP { get; set; }

        private List<Ticket> ticketsAME { get; set; }



        public double CalcularIndicadorIEPM()
        {
            double iepm = 0.0;

            if (ticketsAME.Count > 0)
            {
                iepm = ((double)ticketsAME.Count-(double)ticketsANP.Count)/(double)ticketsAME.Count;

            }
            else
            {
                iepm = 100;
            }

            return iepm;
        }
    }
}
