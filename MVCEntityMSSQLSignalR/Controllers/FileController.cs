using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCEntityMSSQLSignalR.DAL.Entities;
using MVCEntityMSSQLSignalR.DAL.Interfaces;
using MVCEntityMSSQLSignalR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="logger">Received logger object</param>
        public FileController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment appEnvironment,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        /// <summary>
        /// Get chat privacy method
        /// </summary>
        /// <returns>Privacy page</returns>
        public async Task<IActionResult> Files()
        {
            try
            {
                var files = _mapper.Map<List<FileViewModel>>(await _unitOfWork.Files.GetAll());

                return View(files);
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

                var usersGuid = new List<User>(await _unitOfWork.Users.Find(u => u.Email == User.Identity.Name))[0].UserGuid;
                DAL.Entities.File file = new DAL.Entities.File { Name = uploadedFile.FileName, Path = path, UserGuid = usersGuid };
                _unitOfWork.Files.Create(file);
                _unitOfWork.Save();
            }

            return RedirectToAction("Files");
        }
    }
}
