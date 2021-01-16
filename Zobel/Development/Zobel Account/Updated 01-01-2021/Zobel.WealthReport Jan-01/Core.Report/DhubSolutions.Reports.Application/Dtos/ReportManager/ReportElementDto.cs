using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DhubSolutions.Reports.Application.Dtos.ReportManager
{
    public class ReportElementDto
    {
        public string Id { get; set; }

        public string ReportId { get; set; }

        public string ContainerId { get; set; }

        public string Type { get; set; }

        public JObject Config { get; set; }

        public ICollection<ReportElementDto> Children { get; set; }

        public bool UpdateUpReferences(string reportId, string containerId)
        {
            //The id of this reportElement for some reason its not been set
            if (string.IsNullOrEmpty(Id) || string.IsNullOrWhiteSpace(Id))
                return false;

            ReportId = reportId;
            ContainerId = containerId;

            //Checks if all the children where updated ok
            bool result = true;

            foreach (var child in Children)
                result &= child.UpdateUpReferences(reportId, Id);

            return result;
        }

        public IEnumerable<ReportElementDto> GetAllContent()
        {
            yield return this;

            foreach (var element in Children)
            {
                foreach (var child in element.GetAllContent())
                {
                    yield return child;
                }
            }
        }
    }
}