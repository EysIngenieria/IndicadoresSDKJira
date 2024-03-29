﻿using DashboarJira.Model;

namespace DashboarJiraTest
{


    [TestFixture]
    public class IRFEntityTests
    {
        [Test]
        public void CalculoIRF_WithEmptyList_ReturnsOneHundred()
        {
            // Arrange
            List<ReporteFallasPorPuerta> fallasPorPuerta = new List<ReporteFallasPorPuerta>();
            double total_puertas = 1.0;
            List<Ticket> tickets = new List<Ticket>();

            IRFEntity irfEntity = new IRFEntity(fallasPorPuerta, total_puertas, tickets);

            // Act
            double result = irfEntity.calculoIRF();

            // Assert
            Assert.That(result, Is.EqualTo(100.0));
        }

        [Test]
        public void CalculoIRF_WithSingleReporteFallasPorPuertaWithNoFailures_ReturnsZero()
        {
            // Arrange
            List<ReporteFallasPorPuerta> fallasPorPuerta = new List<ReporteFallasPorPuerta>()
        {
            new ReporteFallasPorPuerta()
            {
                Puerta = "Puerta 1",
                Fallas = new List<FallaPorPuerta>()
            }
        };
            double total_puertas = 1.0;
            List<Ticket> tickets = new List<Ticket>();

            IRFEntity irfEntity = new IRFEntity(fallasPorPuerta, total_puertas, tickets);

            // Act
            double result = irfEntity.calculoIRF();

            // Assert
            Assert.That(result, Is.EqualTo(0.0));
        }
    }

}
