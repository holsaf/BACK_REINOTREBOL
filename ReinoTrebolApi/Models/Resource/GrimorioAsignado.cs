using ReinoTrebolApi.Models.Enums;

namespace ReinoTrebolApi.Models.Resource
{
    public class GrimorioAsignado
    {
        public Guid IdSolicitud { get; set; }
        public GrimorioType Grimorio { get; set; }

    }
}
