using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Bootstrappers.AutoMapper
{
    public class BorrowerProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BorrowerViewModel, Borrower>()
                .ForMember(b => b.ContentType, x => x.Ignore())
                .ForMember(b => b.Title, x => x.Ignore());
            Mapper.CreateMap<Borrower, BorrowerViewModel>()
                .ForMember(b=>b.TitlesSelectList, x => x.Ignore());
        }
    }
}