using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectWebAppMock.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Error() => View();
       

    }
}
