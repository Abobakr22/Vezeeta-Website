using Core.Dtos.BookingDtos;
using Core.Dtos.DoctorDtos;
using Core.Dtos.PatientDtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IPatientRepository : IBaseRepository<ApplicationUser>
    {
        //Task<bool> LoginPatient(LoginPatientDto loginPatientDto);
        Task<bool> AddNewPatient(AddPatientDto addPatientDto); //Register
        Task<IEnumerable<GetAllDoctorsDto>> GetAllDoctorsSearch(int Page, int PageSize, string Search);
        Task<bool> AddBooking(AddBookingDto addBookingDto);
        IEnumerable<GetBookingDetailsDto> GetAllBooking();
        Task<bool> CancelBooking(int BookingId);
        Task<GetPatientDto> GetPatientById(string PatientId);
        Task<IEnumerable<GetAllPatientsDto>> GetAllPatients(int Page , int PageSize , string Search);


    }
}
