using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Bootstrappers.AutoMapper
{
    public class BorrowerItemProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<BorrowerItemViewModel, BorrowersItem>();
            Mapper.CreateMap<BorrowersItem, BorrowerItemViewModel>();

            Mapper.CreateMap<BorrowersItem, BorrowerItemRowViewModel>();
            Mapper.CreateMap<BorrowerItemRowViewModel, BorrowersItem>();
        }
    }
}