using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedtelligentMovies.Common.Services
{
    public interface IUserContextService : IService
    {
        string GetUserName();
    }
}
