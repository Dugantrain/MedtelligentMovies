using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MedtelligentMovies.Common.Services;

namespace MedtelligentMovies.Web.UserContext
{
    //A web-specific implementation of the IUserContextService
    public class HttpUserContextService : IUserContextService
    {
        public string GetUserName()
        {
            string userName = null;
            if (HttpContext.Current.User != null)
            {
                userName =  HttpContext.Current.User.Identity.Name;
            }
            return userName;

        }
    }
}