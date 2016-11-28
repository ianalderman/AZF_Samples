#r "Microsoft.WindowsAzure.Storage"

using System;
using Geocoding.Microsoft;
using CsvHelper;
using Microsoft.WindowsAzure.Storage.Blob;

public struct LatLong {
            public double Lat;
            public double Long;
}

public class AddressEntry {
    public string AddressLine1 {get;set;}
    public string AddressLine2 {get;set;}
    public string AddressLine3 {get;set;}
    public string AddressLine4 {get;set;}
    public string PostCode {get;set;}
}

public static void Run(CloudBlockBlob myBlob, out string outputBlob,  out string outputSbMsg, TraceWriter log)
{
    log.Info($"Processing file:{myBlob}");
    string markedUp = string.Empty;


    using (var stream = myBlob.OpenRead())
    {
        using (CsvReader csv = new CsvReader(new StreamReader(stream))){
            while (csv.Read()) {
                log.Info($"Input Post Code:{csv.GetField(4)}");
                LatLong geocoderesult = GeoCodePostCode(csv.GetField(0) + "," + csv.GetField(1) + "," + csv.GetField(2) + ", " + csv.GetField(3) + "," + csv.GetField(4));
                markedUp += csv.GetField(0) + "," + csv.GetField(1) + "," + csv.GetField(2) + "," + csv.GetField(3) + "," + csv.GetField(4) +  "," + geocoderesult.Lat + "," + geocoderesult.Long +"\r\n";
                log.Info($"Lat:{geocoderesult.Lat}");
            }
        }
        
    }

    outputBlob = markedUp;
    string sbMsg = $"{outputBlob}";
    outputSbMsg = sbMsg;
}

private static LatLong GeoCodePostCode(string AddressToParse) {
    BingMapsGeocoder geocoder = new BingMapsGeocoder(Environment.GetEnvironmentVariable("BingMapsKey"));
    IEnumerable<BingAddress> addresses = geocoder.Geocode(AddressToParse);
    LatLong result = new LatLong();
    result.Lat = addresses.First().Coordinates.Latitude;
    result.Long = addresses.First().Coordinates.Longitude;
    return result;
}
