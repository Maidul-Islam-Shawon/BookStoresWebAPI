using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Models
{
    public class UserWithToken:User
    {
       public string Token { get; set; }

        public UserWithToken(User user)
        {
            this.UserId = user.UserId;
            this.EmailAddress = user.EmailAddress;
            this.Password = user.Password;
            this.FirstName = user.FirstName;
            this.MiddleName = user.MiddleName;
            this.LastName = user.LastName;
            this.PubId = user.PubId;
            this.HireDate = user.HireDate;

            this.Role = user.Role;
        }

        
   

    }
}
