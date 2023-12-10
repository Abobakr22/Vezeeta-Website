using Core.Consts;
using Core.Dtos;
using Core.Dtos.BookingDtos;
using Core.Dtos.DoctorDtos;
using Core.Dtos.PatientDtos;
using Core.Dtos.StatisticsDtos;
using Core.Models;
using Core.Repository;


namespace Core.Service
{
    public interface IDoctorService : IBaseRepository<Doctor>
    {
        Task<bool> AddDoctorAsync(AddDoctorDto doctor);
        Task<bool> UpdateDoctorAsync(UpdateDoctorDto doctorModel);
        Task<bool> DeleteDoctorAsync(int DoctorId);
        Task<GetDoctorDto> GetDoctorByIdAsync(int DoctorId);
        Task<IEnumerable<GetDoctorDto>> GetAllDoctorsAsync(int pageNumber, int pageSize, string Search);

        Task<int> NumberOfDoctors();
        List<TopDoctorsDto> TopTenDoctors();
        List<TopSpecializationsDto> TopFiveSpecializations();

        Task<bool> ConfirmCheckUp(int BookingId);

        Task<IEnumerable<GetBookingDetailsDto>> GetAllBookingsOfDoctor(DateTime date, int pageSize, int pageNumber);
    }
}