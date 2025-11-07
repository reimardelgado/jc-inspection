
using Backend.Application.DTOs.Responses.ZohoResponses;

namespace Backend.Application.Queries.ZohoQueries
{
    public class ReadZohoTokenQuery : IRequest<EntityResponse<ZohoTokenResponse>>
    {
        public ReadZohoTokenQuery()
        {
        }
    }
}