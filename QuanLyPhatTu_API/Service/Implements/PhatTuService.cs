using QuanLyPhatTu_API.Payloads.Converters;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Service.Implements
{
    public class PhatTuService : BaseService, IPhatTuService
    {
        public async Task<IEnumerable<PhatTuDTO>> LayTatCaPhatTu(int pageSize, int pageNumber)
        {
            var list = await _context.phatTus.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => _phatTuConverter.EntityToDTO(x)).ToListAsync();
            return list;
        }
    }
}
