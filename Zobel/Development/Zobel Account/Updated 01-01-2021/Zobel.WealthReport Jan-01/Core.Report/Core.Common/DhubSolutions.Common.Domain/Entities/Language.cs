using DhubSolutions.Core.Domain.Entity;

namespace DhubSolutions.Common.Domain.Entities
{
    public class Language : BaseEntity
    {
        public string LanguageName { get; set; }

        public int LanguageCode { get; set; }
    }
}
