using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todo.Core;
using Todo.Server.Exceptions;

namespace Todo.Server.Controllers
{
    public class ApiControllerBase: ControllerBase
    {
        private Guid? _userId;
        private bool? _isAdmin;

        protected Guid UserId => _userId ??= GetUserId();
        protected bool IsAdmin => _isAdmin ??= User.IsInRole(RoleConstants.Admin); 


        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                throw new UnauthorizedException();
            }

            return Guid.Parse(userId);
        }
    }
}
