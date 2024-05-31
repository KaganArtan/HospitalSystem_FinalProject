namespace HospitalSystem_FinalProject.Models
{
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

    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SpecializationId { get; set; } // Add this property
        public Specialization? Specialization { get; set; }
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }

    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; } 
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; } 
        public DateTime AppointmentDateTime { get; set; }
    }
}
