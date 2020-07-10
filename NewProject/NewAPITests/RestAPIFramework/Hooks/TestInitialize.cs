using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using RestAPIFramework.Utilities;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using TechTalk.SpecFlow;

namespace RestAPIFramework.Hooks
{
    [Binding]
    public class TestInitialize
    {
        private RestHelperUtils _settings;
        private static ExtentTest _featureName;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;
        private static ScenarioContext _scenarioContext;
        public static string ProjectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\.."));

        public TestInitialize(RestHelperUtils settings, ScenarioContext scenarioContext)
        {
            _settings = settings;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void TestSetUp()
        {
            _settings.BaseUrl = new Uri(ConfigurationManager.AppSettings["AppUrl"].ToString());
            _settings.Client.BaseUrl = _settings.BaseUrl;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        [BeforeTestRun]
        public static void InitializeReport()
        {
            var _pathString = Path.Combine(ProjectDir, "Report");
            Directory.CreateDirectory(_pathString);
            var _htmlReporter = new ExtentHtmlReporter(_pathString + "//");
            _htmlReporter.Config.Theme = Theme.Dark;
            _extent = new ExtentReports();
            _extent.AttachReporter(_htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            _extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureName = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }


        [AfterStep]
        public static void AfterStepReport()
        {

            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }


            if (_scenarioContext.ScenarioExecutionStatus != ScenarioExecutionStatus.OK)
            {
                if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
                {
                    if (stepType == "Given")
                    {
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("This Step Has Been Skipped And Not Executed.");
                    }
                    else if (stepType == "When")
                    {
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("This Step Has Been Skipped And Not Executed.");
                    }
                    else if (stepType == "Then")
                    {
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("This Step Has Been Skipped And Not Executed.");
                    }
                }

                if (_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.BindingError ||
                    _scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError ||
                   _scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.UndefinedStep)
                {
                    if (stepType == "Given")
                    {
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                    }
                    else if (stepType == "When")
                    {
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                    }
                    else if (stepType == "Then")
                    {
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                    }
                }
            }

        }
    }
}
