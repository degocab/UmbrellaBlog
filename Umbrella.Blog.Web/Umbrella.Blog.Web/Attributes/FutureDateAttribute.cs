using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Umbrella.Blog.Web.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
        {
        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                DateTime checkDate = (DateTime)value;
                if (DateTime.Today.AddDays(1) > checkDate)
                    return false;
                else
                    return true;
            }

            return false;
        }
    }
}