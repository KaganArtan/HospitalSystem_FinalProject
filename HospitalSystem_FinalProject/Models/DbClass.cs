
namespace HospitalSystem_FinalProject.Models;



public class City
{
    public int CityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Hospital>? Hospitals { get; set; }
}

public class Hospital
{
    public int HospitalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int CityId { get; set; }
    public City? City { get; set; }
    public List<Doctor>? Doctors { get; set; }
    public List<Appointment>? Appointments { get; set; }
}

public class Specialization
{
    public int SpecializationId { get; set; }
    public string Domain { get; set; } = string.Empty;
}

public class Comment
{
    public int CommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; } // 1 to 5
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
}

public class Doctor
{
    public int DoctorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }
    public int HospitalId { get; set; }
    public Hospital? Hospital { get; set; }
    public List<Appointment>? Appointments { get; set; }
    public List<Comment>? Comments { get; set; } // Add this property
}

public class Appointment
{
    public int AppointmentId { get; set; }
    public string? UserId { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public int HospitalId { get; set; }
    public Hospital? Hospital { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public bool IsValidAppointmentTime()
    {
        if (AppointmentDateTime < DateTime.Now)
        {
            return false;
        }

        if (AppointmentDateTime.DayOfWeek == DayOfWeek.Saturday || AppointmentDateTime.DayOfWeek == DayOfWeek.Sunday)
        {
            return false;
        }

        var appointmentTime = AppointmentDateTime.TimeOfDay;
        var startTime = new TimeSpan(8, 0, 0); // 08:00
        var endTime = new TimeSpan(17, 0, 0); // 17:00

        if (appointmentTime < startTime || appointmentTime >= endTime)
        {
            return false;
        }

        if (appointmentTime.Minutes % 15 != 0)
        {
            return false;
        }

        return true;
    }

}
