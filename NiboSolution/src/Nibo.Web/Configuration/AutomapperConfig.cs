using AutoMapper;
using Nibo.Domain.Entities.Transactions;
using Nibo.Web.Models;

namespace Nibo.Web.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();            
        }
    }
}