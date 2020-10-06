using SixLabors.ImageSharp;

namespace MultipleFileUpload.Interfaces
{
    public interface IImageResizer
    {
        string Resizer(Image image, string extension);
        Size GetSize(int size, int width, int height);
    }
}
