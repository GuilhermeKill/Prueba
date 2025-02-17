using Shared;
using Shared.FiltersModels;

namespace Shared { 
public class FilterCarModel : FilterModel
    {
        public int Id { get; set; }
        public string SearchByText { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
    }
}