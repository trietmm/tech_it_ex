using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Models
{
    /// <summary>
    /// Paging model
    /// </summary>
    public class PagingModel
    {
        /// <summary>
        /// Page index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Number of page
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Number of item per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Base url - the url not contain query string
        /// </summary>
        public string BaseUrl { get; set; }
    }
}