using System.Collections.Generic;

namespace DoctorsOffice.Models
{
  public class Patient
  {
    public Patient()
    {
      this.JoinEntities = new HashSet<DoctorPatient>();
    }

    public int PatientId { get; set; }
    public string Name { get; set; }
    public string BirthDate { get; set; }
    public virtual ICollection<DoctorPatient> JoinEntities { get; }
  }
}