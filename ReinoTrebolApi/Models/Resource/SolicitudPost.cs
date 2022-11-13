using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ReinoTrebolApi.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ReinoTrebolApi.Models.Resource
{
    public class SolicitudPost
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Identificacion { get; set; }

        public string Edad { get; set; }

        public AfinidadMagica AfinidadMagica { get; set; }

    }
}
