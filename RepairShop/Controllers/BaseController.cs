using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairShop.Controllers
{
    public class BaseController : Controller
    {
		protected const string SessionKeyUserName = "_UserName";
		protected const string SessionKeyUserId = "_UserId";
		protected const string SessionCart = "_Cart";

		protected const string AuthController = nameof(UsersController);
		protected const string AuthAction = nameof(UsersController.Login);
	}
}
