using System.Collections.Generic;

namespace Shared.Dto
{
    public class FilterResultDto<T>
    {
        public PagerModel Pager { get; set; }
        public List<T> List { get; set; }
    }
}
