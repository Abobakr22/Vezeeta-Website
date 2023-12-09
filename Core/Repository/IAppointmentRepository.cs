using Core.Dtos;
using Core.Dtos.DoctorDtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
        Task<bool> AddAppointment (AddApointmentDto addApointmentDto);
        Task<bool> UpdateAppointment (UpdateAppointmentDto updateAppointmentDto);
        Task<bool> DeleteAppointment (int AppointmentHourId);
    }
}
