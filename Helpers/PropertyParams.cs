namespace RstateAPI.Helpers {
    public class PropertyParams {
        private const int MaxPageSize = 25;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 3;
        public int PageSize {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public int propertyId { get; set; }
        public string propertyType { get; set; }
        public string propertyAddress { get; set; }
        public string OrderBy { get; set; }
    }
}