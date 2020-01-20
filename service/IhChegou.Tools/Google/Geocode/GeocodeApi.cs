using GoogleApi;
using GoogleApi.Entities.Common;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Geocode.Response;
using IhChegou.DTO.Client;
using IhChegou.Tools.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Tools.Google.Geocode
{
    public class GeocodeApi : GoogleApiConfig, IGeocodeApi
    {
        public GeocodeApi() : base("AIzaSyAkhNIrEw0w4BboauEm7D55R_WLPsDI7GI")
        {

        }

        public  DtoAddress GetAddress(double lat, double lng)
        {
            var response = GoogleMaps.Geocode.QueryAsync(new GoogleApi.Entities.Maps.Geocode.Request.GeocodingRequest()
            {
                Key = this.ApiKey,
                Language = Language.PortugueseBrazil,
                Region = "br",
                Location = new Location(lat, lng)
            }).Result;
            return ParserToAddress(response);
        }
        public DtoAddress GetAddress(DtoAddress address)
        {
            string addressstr = null;

            if (address.Latitude != null && address.Longitude != null)
                return GetAddress(address.Latitude.Value, address.Longitude.Value);

            if (!string.IsNullOrEmpty(address.Street))
                addressstr += address.Street;
            if (!string.IsNullOrEmpty(address.Number))
                addressstr += ", " + address.Number;

            if (!string.IsNullOrEmpty(address.Neighborhood))
                addressstr += ", " + address.Neighborhood;

            if (!string.IsNullOrEmpty(address.City))
                addressstr += ", " + address.City;

            if (!string.IsNullOrEmpty(address.State))
                addressstr += " - " + address.State;

            if (addressstr == null && address.PostalCode != null)
                return GetAddress(address.PostalCode);

            if (addressstr?.FirstOrDefault() == ',')
                addressstr.Remove(0, 1);

            if (addressstr?.LastOrDefault() == ',')
                addressstr.Remove(addressstr.Length - 1, 1);

            var response = GoogleMaps.Geocode.QueryAsync(new GoogleApi.Entities.Maps.Geocode.Request.GeocodingRequest()
            {
                Key = this.ApiKey,
                Language = Language.PortugueseBrazil,
                Region = "br",
                Address = $"{address.Street}, {address.Number}, {address.Neighborhood}, {address.City} - {address.State}"
            }).Result;

            var result = ParserToAddress(response);
            result.Id = address.Id;
            result.City = result.City ?? address.City;
            result.Complement = result.Complement?? address.Complement;
            result.Latitude= result.Latitude ?? address.Latitude;
            result.Longitude = result.Longitude ?? address.Longitude;
            result.Name = result.Name ?? address.Name;
            result.Neighborhood = result.Neighborhood?? address.Neighborhood;
            result.Number= result.Number ?? address.Number;
            result.PostalCode = result.PostalCode ?? address.PostalCode;
            result.Reference= result.Reference?? address.Reference;
            result.State = result.State?? address.State;
            result.Street = result.Street?? address.Street;
            return result;
        }
        public  DtoAddress GetAddress(string postalcode)
        {
            var response = GoogleMaps.Geocode.QueryAsync(new GoogleApi.Entities.Maps.Geocode.Request.GeocodingRequest()
            {
                Key = this.ApiKey,
                Language = Language.PortugueseBrazil,
                Region = "br",
                Address = postalcode,
            });
            return ParserToAddress(response.Result);
        }



        private static DtoAddress ParserToAddress(GeocodingResponse response)
        {
            var addressComponents = response.Results?.FirstOrDefault().AddressComponents;
            var geolocation = response.Results?.FirstOrDefault().Geometry.Location;


            var number = addressComponents?
                  .Where(i => i.Types.Contains(LocationType.StreetNumber)).FirstOrDefault();

            var street = addressComponents?
                .Where(i => i.Types.Contains(LocationType.Route) ||
                i.Types.Contains(LocationType.StreetAddress)).FirstOrDefault();

            var neigh = addressComponents?
                .Where(i => i.Types.Contains(LocationType.Sublocality)).FirstOrDefault();

            var city = addressComponents?
                .Where(i => i.Types.Contains(LocationType.Locality) ||
                i.Types.Contains(LocationType.AdministrativeAreaLevel2)).FirstOrDefault();

            var State = addressComponents?
                .Where(i => i.Types.Contains(LocationType.AdministrativeAreaLevel1)).FirstOrDefault();

            var postalcode = addressComponents?
          .Where(i => i.Types.Contains(LocationType.PostalCode)).FirstOrDefault();

            return new DtoAddress()
            {
                City = city?.ShortName,
                Neighborhood = neigh?.ShortName,
                State = State?.ShortName,
                Street = street?.ShortName,
                Latitude = geolocation?.Latitude,
                Longitude = geolocation?.Longitude,
                PostalCode = postalcode?.ShortName

            };
        }

    }
}
