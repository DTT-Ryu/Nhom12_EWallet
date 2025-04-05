namespace Nhom12_EWallet.Service
{
    public class ResultService
    {
        public bool Success { get; set; }
        public Dictionary<string, string> Error { get; set; } = new();
    }
}
