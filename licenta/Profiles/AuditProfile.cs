using AutoMapper;
using licenta.Models.Audits;

namespace licenta.Profiles
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<Entities.Audit, AuditDto>();
        }
    }
}
