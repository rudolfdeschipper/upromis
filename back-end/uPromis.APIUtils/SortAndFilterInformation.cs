using System.Collections.Generic;

namespace uPromis.APIUtils.APIMessaging
{
    public class SortAndFilterInformation
    {
        // properties are not capital due to json mapping
//        public int draw { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public List<Search> filtered { get; set; }
        public List<Order> sorted { get; set; }
    }
}
