using Api.ViewModels.AbstractionVMs;

namespace Api.ViewModels.DbInvestementVMs
{
    public class FileTypeViewModel : BaseViewModel
    {
        public FileTypeViewModel()
        {
        }

        public string FileTypeValue { get; set; }
        public string Description { get; set; }
    }
}
