using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Models;

namespace Chillisoft.LendingLibrary.Web.Bootstrappers.AutoMapper
{
    public class ItemProfile: Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ItemViewModel, Item>();
            Mapper.CreateMap<Item, ItemViewModel>();
        }
    }
}