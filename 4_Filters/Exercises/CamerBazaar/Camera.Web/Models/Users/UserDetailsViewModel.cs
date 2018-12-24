using Camera.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Camera.Web.Models.Users
{
    public class UserDetailsViewModel : CameraUserDetails
    {        
        public string LastLoginTime { get; set; }       
    }
}
