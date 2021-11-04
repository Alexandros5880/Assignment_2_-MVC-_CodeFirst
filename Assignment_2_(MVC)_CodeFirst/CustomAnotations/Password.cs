using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Assignment_2__MVC__CodeFirst.CustomAnotations
{
    [AttributeUsage(AttributeTargets.All)]
    public class Password : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value.ToString().Length < 4)
            {
                ErrorMessage = "Password length must have under of 4 letters.";
                return false;
            }
            else if (!value.ToString().Any(char.IsUpper))
            {
                ErrorMessage = "Password length must have at list one Upper Case Letter.";
                return false;
            }
            else if (!value.ToString().Any(char.IsLower))
            {
                ErrorMessage = "Password length must have at list one Lower Case Letter.";
                return false;
            }
            else if (!value.ToString().Any(char.IsDigit))
            {
                ErrorMessage = "Password length must have at list one digit character.";
                return false;
            }
            else if (!value.ToString().Any(char.IsNumber))
            {
                ErrorMessage = "Password length must have at list one number.";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}