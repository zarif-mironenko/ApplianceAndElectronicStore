using System;
using System.Collections.Generic;

namespace ApplianceAndElectronicStore.Models.ViewModels {

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int? CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
        public Dictionary<string, object> PageUrlValues { get; set; }
           = new Dictionary<string, object>();
        public string ElementSizeClass { get; set; }
    }
}
