using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todo.Server.Exceptions;

namespace Todo.Server.Controllers
{
    public class ApiControllerBase: ControllerBase
    {
        private Guid? _userId;
        protected Guid UserId => _userId ??= GetUserId();

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
