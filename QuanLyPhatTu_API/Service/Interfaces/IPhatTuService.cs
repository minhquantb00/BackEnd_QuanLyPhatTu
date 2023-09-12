using QuanLyPhatTu_API.Payloads.DTOs;

namespace QuanLyPhatTu_API.Service.Interfaces
{
    public interface IPhatTuService
    {
        Task<IEnumerable<PhatTuDTO>> LayTatCaPhatTu(int pageSize, int pageNumber);
    }
}
