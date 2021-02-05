using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _appEnvironment;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="logger">Received logger object</param>
        public FileController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Get chat privacy method
        /// </summary>
        /// <returns>Privacy page</returns>
        public IActionResult Files()
        {
            try
            {
                return View(_unitOfWork.Files.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problem with unit of work to get all");
                return BadRequest();
            }
        }

        /// <summary>
        /// Get chat privacy method
        /// </summary>
        /// <returns>Privacy page</returns>
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                DAL.Entities.File file = new DAL.Entities.File { Name = uploadedFile.FileName, Path = path };
                _unitOfWork.Files.Create(file);
                _unitOfWork.Save();
            }

            return RedirectToAction("Files");
        }
    }
}
