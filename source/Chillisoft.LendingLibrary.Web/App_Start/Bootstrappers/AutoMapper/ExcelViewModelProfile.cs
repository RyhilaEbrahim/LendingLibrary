using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Bootstrappers.AutoMapper
{
    public class ExcelViewModelProfile : Profile
    {
        protected override void Configure()
        {

            Mapper.CreateMap<ExcelViewModel, BorrowersItem>();
            Mapper.CreateMap<BorrowersItem, ExcelViewModel>();
        }
    }
}