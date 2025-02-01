using Microsoft.AspNetCore.Identity;

namespace ShortLink.Domain.Entities
{
    public class User: IdentityUser<Guid>
    {
        public ICollection<DoubleUrl> DoubleUrls { get; set; } = new List<DoubleUrl>();
    }
}
