using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace WebApiCFT.Models
{
    /*
     * Model for pagination
     */
    public class FilterModelBase<T> where T : class
    {
        [Range(1, Int64.MaxValue)]
        public int PageSize { get; set; }
        
        [Range(1, Int64.MaxValue)]
        public int PageNumber { get; set; }

        public FilterModelBase()
        {
            this.PageSize = 5;
            this.PageNumber = 1;
        }

        /*
         * Filtration of data
         */
        public virtual IQueryable<T> Filter(IQueryable<T> data)
        {
            return data
                .Skip((this.PageNumber - 1) * this.PageSize)
                .Take(this.PageSize);
        }

        /*
         * Checking correctness of filtering parameters 
         */
        public virtual Boolean IsValid(out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
    }
}