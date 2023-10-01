using AutoMapper;
using Ecommerce.Models.EntityModels;
using WebApplication1.Models;

namespace WebApplication1.Automapper
{
    public class EcommerceWebAutomapper : Profile
    {
        public EcommerceWebAutomapper()
        {
            CreateMap<CustomerCreate, Customer>();
            CreateMap<CustomerEditViewModel, Customer>();
        }
    }
}
