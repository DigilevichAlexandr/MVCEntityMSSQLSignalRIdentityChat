using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using MVCEntityMSSQLSignalR.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Controllers
{
    /// <summary>
    /// Main controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="logger">Received logger object</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get chat page method
        /// </summary>
        /// <returns>chat page representation</returns>
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                return await Task.Run(() => View());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.Run(() => BadRequest());
            }
        }

        /// <summary>
        /// Get chat privacy method
        /// </summary>
        /// <returns>Privacy page</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Get error page method
        /// </summary>
        /// <returns>Error page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
