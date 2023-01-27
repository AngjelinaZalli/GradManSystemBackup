//using System.Security.Claims;
//using Microsoft.AspNetCore.Http;

//namespace GradManSystem1.Models
//{
//    public class UserService : IUserService
////    {
//        private readonly IHttpContextAccessor _contextAccessor;
//        public UserService(IHttpContextAccessor _context)
//        {
//            _contextAccessor = _context;
//        }
//        public string getUserId()
//        {
//            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//        }
//        public bool isAuthenticated()
//        {
//            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
//        }
//        public string getUserName()
//        {
//            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
//        }
//    }
//}
