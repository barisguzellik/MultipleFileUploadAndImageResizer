using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultipleFileUpload.Entities;
using MultipleFileUpload.Interfaces;
using SixLabors.ImageSharp;

namespace MultipleFileUpload.Controllers
{
    public class MediaController : Controller
    {
        private IImageResizer _imageResizer { get; }
        public MediaController(IImageResizer imageResizer)
        {
            _imageResizer = imageResizer;
        }

        [Route("/media/multipleupload")]
        public IActionResult MultiUpload(List<IFormFile> files)
        {
            var uploadedImages = new List<UploadFile> { };

            if (files == null || files.Count == 0)
            {
                return BadRequest(new { Error = "No file selected..." });
            }

            foreach (var item in files)
            {
                var extension = Path.GetExtension(item.FileName).ToLower();
                if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                {
                    return BadRequest(new { Error = "Incorrect format..." });
                }

                using var image = Image.Load(item.OpenReadStream());
                uploadedImages.Add(new UploadFile { FileName = _imageResizer.Resizer(image, extension) });
            }

            return Ok(Json(uploadedImages.ToList()));
        }

        [Route("/media/upload")]
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            var uploadedImage = new UploadFile { };

            if (file == null)
            {
                return BadRequest(new { Error = "No file selected..." });
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
            {
                return BadRequest(new { Error = "Incorrect format..." });
            }

            using var image = Image.Load(file.OpenReadStream());
            uploadedImage = new UploadFile { FileName = _imageResizer.Resizer(image, extension) };

            return Ok(Json(uploadedImage.FileName));
        }
    }
}
