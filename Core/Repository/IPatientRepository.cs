using Core.Dtos.BookingDtos;
using Core.Dtos.DoctorDtos;
using Core.Dtos.PatientDtos;
using Core.Models;

namespace Core.Repository
{
    public interface IPatientRepository : IBaseRepository<ApplicationUser>
    {
        Task<bool> AddNewPatient(AddPatientDto addPatientDto); //Register
        Task<IEnumerable<GetAllDoctorsDto>> GetAllDoctorsSearch(int Page, int PageSize, string Search);
        Task<bool> AddBooking(AddBookingDto addBookingDto);
        IEnumerable<GetBookingDetailsDto> GetAllBooking();
        Task<bool> CancelBooking(int BookingId);
        Task<GetPatientDto> GetPatientById(string PatientId);
        Task<IEnumerable<GetAllPatientsDto>> GetAllPatients(int Page, int PageSize, string Search);

        Task<int> NumberOfPatients();
        dynamic NumberOfRequests();
    }
}