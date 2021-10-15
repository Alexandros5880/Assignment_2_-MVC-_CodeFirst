using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment_2__MVC__CodeFirst.CustomAnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class DateBiggerAttribute : ValidationAttribute
    {
        public int AddMonths { get; set; }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt > DateTime.Now.AddMonths(this.AddMonths))
            {
                return true;
            }
            return false;
        }
    }
}