using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow;
using RestAPIFramework.Utilities;
using static NewAPITests.Enum.RequestCall;


namespace NewAPITests.StepDefinitions
{
    [Binding]
    public class SimpleGetOperationsSteps
    {
        private RestHelperUtils _settings;

        public SimpleGetOperationsSteps(RestHelperUtils settings)
        {
            _settings = settings;
        }

        [Given(@"I perform Get operation on ""(.*)""")]
        public void GivenIPerformGetOperationOn(string strEndpoint)
        {
            _settings.SetRequest(strEndpoint, Requests.GET);
        }
        
        [When(@"I pass valid GetID")]
        public void WhenIPassValidGetID()
        {
            _settings.Response = _settings.GetResponses("GetID", "1");
        }
        
        [Then(@"I should see the response details")]
        public void ThenIShouldSeeTheResponseDetails()
        {
            Assert.AreEqual(HttpStatusCode.OK, _settings.Response.StatusCode);
        }
    }
}
