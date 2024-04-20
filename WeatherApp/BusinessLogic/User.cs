using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.BusinessLogic
{
    public class User
    {
        private string _username;
        public string Username { get { return _username; } }
        private string _postalCode;
        public string PostalCode { get { return _postalCode; } set { _postalCode = value; } }

        public User(string username)
        {
            _username = username;
        }
    }
}
