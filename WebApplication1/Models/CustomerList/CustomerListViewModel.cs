using Ecommerce.Models.UtilityModel;

namespace WebApplication1.Models.CustomerList
{
    public class CustomerListViewModel
    {
        public CustomerSearchCriteria customerSearchCriteria { get; set; }
        public ICollection<CustomerListItem> customerList { get; set; }
    }
}
