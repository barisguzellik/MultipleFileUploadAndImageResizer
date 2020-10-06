using MultipleFileUpload.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
namespace MultipleFileUpload.Services
{
    public class ImageResizer : IImageResizer
    {
        private INameCreator _nameCreator { get; }
        public ImageResizer(INameCreator nameCreator)
        {
            _nameCreator = nameCreator;
        }

        public Size GetSize(int size, int width, int height)
        {
            double scale = 1;

            if (width > height)
                scale = Convert.ToSingle(size) / width;
            else if (height > width)
                scale = Convert.ToSingle(size) / height;

            if (scale < 0 || scale > 1) { scale = 1; }

            double newWidth = Math.Floor(Convert.ToSingle(width) * scale);
            double newHeight = Math.Floor(Convert.ToSingle(height) * scale);

            return new Size((int)newWidth, (int)newHeight);
        }

        public string Resizer(Image image, string extension)
        {
            var basePath = System.IO.Directory.GetCurrentDirectory() + "/wwwroot/upload/";
            var fileName = _nameCreator.Create() + extension;

            //original
            image.Save(basePath + "/original/" + fileName);
            //large
            image.Mutate(x => x.Resize(GetSize(1024, image.Width, image.Height)));
            image.Save(basePath + "/large/" + fileName);
            //medium
            image.Mutate(x => x.Resize(GetSize(800, image.Width, image.Height)));
            image.Save(basePath + "/medium/" + fileName);
            //small
            image.Mutate(x => x.Resize(GetSize(250, image.Width, image.Height)));
            image.Save(basePath + "/small/" + fileName);

            return fileName;
        }
    }
}
