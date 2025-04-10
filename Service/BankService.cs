using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.Service.Interfaces;

namespace Nhom12_EWallet.Service
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;
        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }


        public async Task<TblBank> GetBankByID(string id)
        {
            return await _bankRepository.GetBankByID(id);
        }

        public async Task<TblBank> GetBankByName(string name)
        {
            return await _bankRepository.GetBankByName(name);
        }

        public async Task<List<TblBank>> GetAllBank()
        {
            return await _bankRepository.GetAll();
        }

        public async Task CreateBank(TblBank bank, IFormFile ImageFile)
        {
            bank.SImage = await Util.SaveImage(ImageFile);
            await _bankRepository.Add(bank);
        }

        public async Task UpdateBank(TblBank bank, IFormFile? ImageFile)
        {
            if (ImageFile != null)
            {
                // Đường dẫn thư mục lưu ảnh
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/bank");

                // Kiểm tra và xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(bank.SImage))
                {
                    string oldFilePath = Path.Combine(uploadsFolder, bank.SImage);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath); // Xóa ảnh cũ
                    }
                }
                bank.SImage = await Util.SaveImage(ImageFile);
            }
            await _bankRepository.Update(bank);
        }
        public async Task<bool> UpdateBankDeleted(string id)
        {
            return await _bankRepository.UpdateBankDeleted(id);
        }

        
    }
}
