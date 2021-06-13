using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Utility.Classes
{
    public class CustomValidations
    {
        /// <summary>
        ///  Verify if the Domain Name from Email String matches the specified domain name
        /// </summary>
        public class EmailDomainValidation : ValidationAttribute
        {
            string _domainName;
            public EmailDomainValidation(string domainName, string errorMessage):base(errorMessage: errorMessage)
            {
                _domainName = domainName;
            }            
            
            public override bool IsValid(object emailString)
            {
                var domainString = emailString.ToString().Split('@')[1];

                return domainString.ToUpper() == _domainName.ToUpper();
            }
        }


    }
}
