namespace Requests.Domain.Models.RequestStructure
{
    public class RequestData
    {
        public string Title { get; set; }

        public RequestDataControl Controls { get; set; }

        public string[] ApproverGroups { get; set; }

        public string[] SolutionGroups { get; set; }
    }
}
