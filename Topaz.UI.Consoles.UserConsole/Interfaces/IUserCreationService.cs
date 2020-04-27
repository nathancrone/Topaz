using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Topaz.UI.Consoles.UserConsole.Interfaces
{
    public interface IUserCreationService
    {
        Task CreateRole();
        Task CreateUser();
    }
}