using IhChegou.DTO.Client;

namespace IhChegou.Tools.Contract
{
    public interface IGeocodeApi
    {
        DtoAddress GetAddress(double lat, double lng);
        DtoAddress GetAddress(DtoAddress address);
        DtoAddress GetAddress(string postalcode);
    }
}