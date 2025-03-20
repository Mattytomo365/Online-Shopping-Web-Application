namespace ShoppingToYou.Database
{
    public interface ICustomerRepo
    {
        int CheckCustomerByEmail(string email);
        int InsertCustomer(string email, string firstName, string surname, string address1, string address2, string address3,
                                   string address4, string postcode, string phone);
    }
}