using System;
using System.Linq;
using Newtonsoft.Json;
using Ninject;
using ReactiveUI;
using ReactiveUI.Xaml;
using RestSharp;

namespace GistEd.Debugging.RestClient.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private bool? _DialogResult;

        private string _Request;
        private string _Response;

        private string _ResponseStatusCode;

        [Inject]
        public MainWindowViewModel(IRestClient restClient)
        {
            Request = "GET /gists";

            IssueRequestCommand = new ReactiveCommand(this.WhenAny(x => x.Request, x => !String.IsNullOrWhiteSpace(x.Value)));
            IssueRequestCommand.Subscribe(_ => IssueRequest(restClient));

            CloseCommand = new ReactiveCommand();
            CloseCommand.Subscribe(_ => DialogResult = false);
        }

        public bool? DialogResult
        {
            get { return _DialogResult; }
            set { this.RaiseAndSetIfChanged(x => x.DialogResult, value); }
        }

        public string Request
        {
            get { return _Request; }
            set { this.RaiseAndSetIfChanged(x => x.Request, value); }
        }

        public string ResponseStatusCode
        {
            get { return _ResponseStatusCode; }
            set { this.RaiseAndSetIfChanged(x => x.ResponseStatusCode, value); }
        }

        public string Response
        {
            get { return _Response; }
            set { this.RaiseAndSetIfChanged(x => x.Response, value); }
        }

        #region Commands

        public IReactiveCommand IssueRequestCommand { get; private set; }
        public IReactiveCommand CloseCommand { get; private set; }

        private void IssueRequest(IRestClient restClient)
        {
            string[] requestTokens = Request.Split(' ');

            Method requestMethod;
            Enum.TryParse(requestTokens.First(), true, out requestMethod);

            string requestResource = requestTokens.Skip(1).Take(1).Single();

            var request = new RestRequest
                              {
                                  Resource = requestResource,
                                  Method = requestMethod,
                                  RequestFormat = DataFormat.Json
                              };

            RestResponse response = restClient.Execute(request);

            Response = GetPrettyPrintedJson(response.Content);
            ResponseStatusCode = String.Format("{0} {1}", (int) response.StatusCode, response.StatusDescription);
        }

        private static string GetPrettyPrintedJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        #endregion
    }
}