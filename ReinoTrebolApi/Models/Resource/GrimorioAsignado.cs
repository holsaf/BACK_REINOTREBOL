namespace ReinoTrebolApi.Models.Resource
{
    using ReinoTrebolApi.Models.Enums;

    public class GrimorioAsignado
    {
        public Guid IdSolicitud { get; set; }

        public GrimorioType Grimorio { get; set; }

    }
}
