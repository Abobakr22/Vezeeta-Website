using Core.Consts;
using Core.Dtos;
using Core.Dtos.DoctorDtos;
using Core.Models;


namespace Core.Repository
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        
        Task<bool> AddDoctorAsync (AddDoctorDto doctor );
        Task<bool> UpdateDoctorAsync(UpdateDoctorDto doctorModel);
        Task<bool> DeleteDoctorAsync(int DoctorId);
        Task<GetDoctorDto> GetDoctorByIdAsync(int DoctorId);
        Task<IEnumerable<GetDoctorDto>> GetAllDoctorsAsync( int pageNumber, int pageSize , string Search);

    }
}
