using System;

namespace InternetAuction.Web.Infrastructure
{
    /// <summary>
    /// Represents page info class
    /// </summary>
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}