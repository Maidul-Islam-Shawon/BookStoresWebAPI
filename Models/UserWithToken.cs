using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Models
{
    public class UserWithToken:User
    {
       0 public UserWithToken(User _user)
        {
            user = _user;
        }

        public User user { get; set; }
        public string Token { get; set; }
   

    }
}
