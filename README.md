# SampleAzureFunctions
This Repo is to store sample Azure Functions developed to support my blog posts at my blog :http://mythoughtlab.co.uk

##GeoCodeFile
This Azure Function will take a CSV file in the monitored Azure blob container and use Bing Maps to assign Lat & Long to each address entry.  

The sample input file for this was an extract from the UK Gov open data initiave.  It can be found in the inputfile/GeoCodeFile directory called address.csv

Note you will need a Bing Maps API key for this - these are available here: https://www.microsoft.com/maps/create-a-bing-maps-key.aspx 

##RegisterApp
In this Azure Function I have been trying to explore if Serverless architectures could provide a light-weight application discovery service.

The sample uses Azure Table storage in the backend and function level authentication.  A server calls to the Function via a HTTP WebHook, the function registers its details and then returns a list of any servers also registered for that app.  Subsequent calls to the WebHook will update the timestamp for the server but not introduce duplicate records, each call always returns the list of servers for that app.

As a next step some form of tombstoning could be introduced to find old servers, potentially meta data could be supported.

As this is a very basic PoC there is no validation (I merely check the arguments are present).
