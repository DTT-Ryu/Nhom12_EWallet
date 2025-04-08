namespace Nhom12_EWallet.Service
{
    public class Util
    {
        public static async Task<string> SaveImage(IFormFile ImageFile)
        {
            // Đường dẫn thư mục lưu ảnh
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bank");

            // Tạo tên file gốc
            string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            string uniqueFileName = fileName + extension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Kiểm tra nếu file đã tồn tại, thêm (1), (2),...
            int count = 1;
            while (System.IO.File.Exists(filePath))
            {
                uniqueFileName = $"{fileName}({count}){extension}";
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                count++;
            }

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return uniqueFileName;
        }
    }
}
