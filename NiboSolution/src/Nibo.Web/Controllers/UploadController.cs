using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nibo.Domain.Interfaces.Services;
using Nibo.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Nibo.Web.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly ITransactionServices transactionService;
        private readonly IMapper _mapper;

        public UploadController(IHostingEnvironment env, ITransactionServices transactionService, IMapper mapper)
        {
            _appEnvironment = env;
            _mapper = mapper;
            this.transactionService = transactionService;
        }

        public IActionResult IndexUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendFile(List<IFormFile> files)
        {
            List<TransactionViewModel> list = new List<TransactionViewModel>();
            foreach (var item in files)
            {
                if (item == null || item.Length == 0)
                {
                    ViewData["Erro"] = "Error: File(s) not selected(s)";
                    return View(ViewData);
                }

                string folder = "\\files\\";

                string fileName = $"user_file_{DateTime.Now.Millisecond}";

                if (item.FileName.Contains(".ofx"))
                    fileName += ".ofx";            

                string path_WebRoot = _appEnvironment.WebRootPath;
                string filePathDest = path_WebRoot + folder;
                string filePathDestOrigin = filePathDest + fileName;

                using (var stream = new FileStream(filePathDestOrigin, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                var trans = transactionService.ReadOfxFile(filePathDestOrigin);
                await transactionService.Add(trans);
                list = _mapper.Map<List<TransactionViewModel>>(trans);                             
            }

            return View("Save", list);
        }        
    }
}
