namespace Backend.Application.Services.Reads
{
    public class ReadProfileService : IRequest<Profile?>
    {
        public Guid? Id { get; }
        public string? Name { get; }

        public ReadProfileService(Guid id)
        {
            Id = id;
        }

        public ReadProfileService(string name)
        {
            Name = name;
        }
    }
}