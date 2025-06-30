using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Naitv1.Data;
using Naitv1.Models;

namespace Naitv1.Helpers
{
    public class ExportadorDashboard
    {
        private readonly AppDbContext _context;

        public ExportadorDashboard(AppDbContext context)
        {
            _context = context;
        }

        public string GenerarCsv(string ciudad, DateTime fechaInicio, DateTime fechaFin)
        {
            var query = _context.Actividades
                .Where(a => a.FechaCreacion >= fechaInicio && a.FechaCreacion <= fechaFin);

            if (!string.IsNullOrEmpty(ciudad))
                query = query.Where(a => a.Ciudad == ciudad);

            var lista = query
                .Select(a => new ActividadExportada
                {
                    Id = a.Id,
                    MensajeDelAnfitrion = a.MensajeDelAnfitrion,
                    TipoActividad = a.TipoActividad,
                    Lat = a.Lat,
                    Lon = a.Lon,
                    Ciudad = a.Ciudad ?? "Desconocida",
                    FechaCreacion = a.FechaCreacion
                })
                .OrderBy(a => a.FechaCreacion)
                .ToList();

            using var writer = new StringWriter();
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csv.WriteRecords(lista);

            return writer.ToString();
        }

        private class ActividadExportada
        {
            public int Id { get; set; }
            public string MensajeDelAnfitrion { get; set; }
            public string TipoActividad { get; set; }
            public double Lat { get; set; }
            public double Lon { get; set; }
            public string Ciudad { get; set; }
            public DateTime FechaCreacion { get; set; }
        }
    }
}
