namespace Backend.Application.DTOs.Responses
{
    public class JwtResponse
    {
        public string Jwt { get; }

        public JwtResponse(string jwt)
        {
            Jwt = jwt;
        }
    }
}