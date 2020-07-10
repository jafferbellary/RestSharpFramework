using RestSharp;
using System;
using System.Collections.Generic;

namespace RestAPIFramework.Utilities
{
    public class RestHelperUtils
    {
        public Uri BaseUrl { get; set; }
        public RestRequest Request { get; set; }
        public IRestResponse Response { get; set; }
        public RestClient Client { get; set; } = new RestClient();

        public RestRequest SetRequest(string _strEndPoint, Enum _requestCallType)
        {
            if (_requestCallType.ToString().Equals("GET"))
            {
                Request = new RestRequest(_strEndPoint, Method.GET);
                Request.RequestFormat = DataFormat.Json;
            }
            else if (_requestCallType.ToString().Equals("POST"))
            {
                Request = new RestRequest(_strEndPoint, Method.POST);
                Request.RequestFormat = DataFormat.Json;
            }
            else if (_requestCallType.ToString().Equals("PUT"))
            {
                Request = new RestRequest(_strEndPoint, Method.PUT);
                Request.RequestFormat = DataFormat.Json;
            }
            return Request;
        }

        public IRestResponse GetResponses(object _requestBody)
        {
            Request.AddParameter("application/json", _requestBody, ParameterType.RequestBody);
            Response = Client.Execute(Request);
            return Response;
        }

        public IRestResponse GetResponses()
        {
            Response = Client.Execute(Request);
            return Response;
        }

        public IRestResponse GetResponses(string _strPathParam, string _strPathParamVal)
        {
            Request.AddUrlSegment(_strPathParam, _strPathParamVal);
            Response = Client.Execute(Request);
            return Response;
        }

        public IRestResponse GetResponses(string _strPathParam, string _strPathParamVal, object _requestBody)
        {
            Request.AddUrlSegment(_strPathParam, _strPathParamVal);
            Request.AddParameter("application/json", _requestBody, ParameterType.RequestBody);
            Response = Client.Execute(Request);
            return Response;
        }

        public IRestResponse GetResponses(Dictionary<string, string> _dict)
        {
            foreach (var key in _dict.Keys)
            {
                Request.AddUrlSegment(key, _dict[key]);
            }
            Response = Client.Execute(Request);
            return Response;
        }
    }
}
