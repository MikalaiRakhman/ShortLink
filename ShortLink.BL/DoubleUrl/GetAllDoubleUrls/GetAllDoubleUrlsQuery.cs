using MediatR;

namespace ShortLink.BL.DoubleUrl.GetAllDoubleUrls
{
    public record GetAllDoubleUrlsQuery: IRequest<List<Domain.Entities.DoubleUrl>>
    {
    }
}
