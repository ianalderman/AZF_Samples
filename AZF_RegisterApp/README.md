#RegisterApp
In this Azure Function I have been trying to explore if Serverless architectures could provide a light-weight application discovery service.

The sample uses Azure Table storage in the backend and function level authentication. A server calls to the Function via a HTTP WebHook, the function registers its details and then returns a list of any servers also registered for that app. Subsequent calls to the WebHook will update the timestamp for the server but not introduce duplicate records, each call always returns the list of servers for that app.

As a next step some form of tombstoning could be introduced to find old servers, potentially meta data could be supported.

As this is a very basic PoC there is no validation (I merely check the arguments are present).
