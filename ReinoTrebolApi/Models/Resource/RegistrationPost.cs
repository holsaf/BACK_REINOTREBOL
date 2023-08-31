namespace ReinoTrebolApi.Models.Resource
{
    using ReinoTrebolApi.Models.Enums;

    public class RegistrationPost
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Identification { get; set; }

        public string Age { get; set; }

        public ArtType Art { get; set; }
    }

}
