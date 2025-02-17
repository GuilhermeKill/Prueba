using Shared.FiltersModels;

namespace Shared.Dto
{
    public class CarDto
    {
        public int Id { get; set; }
        public string country { get; set; }
        public int year { get; set; }
        public string model { get; set; }
        public string brand { get; set; }
        public string codeVin { get; set; }
        public string patent { get; set; }

    }
}
