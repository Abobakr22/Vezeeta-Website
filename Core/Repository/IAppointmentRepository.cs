using Core.Dtos.AppointmentDtos;
using Core.Models;

namespace Core.Repository
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<bool> AddAppointment(AddApointmentDto addApointmentDto);
        Task<bool> UpdateAppointment(UpdateAppointmentDto updateAppointmentDto);
        Task<bool> DeleteAppointment(int AppointmentHourId);
    }
}
