using Core.Dtos.AppointmentDtos;
using Core.Models;
using Core.Repository;

namespace Core.Service
{
    public interface IAppointmentService : IBaseRepository<Appointment>
    {
        Task<bool> AddAppointment(AddApointmentDto addApointmentDto);
        Task<bool> UpdateAppointment(UpdateAppointmentDto updateAppointmentDto);
        Task<bool> DeleteAppointment(int AppointmentHourId);
    }
}
