namespace RegistrationPortal.Data.Models;
public class AddressInfo
{
    public string Country { get; set; }
    public string StateProvince { get; set; }
    public string City { get; set; }

    public string GetRegionalAddress()
    {
        return $"{City}, {StateProvince}, {Country}";
    }
    public string GetRegion()
    {
        return $"{StateProvince}, {Country}";
    }

}
