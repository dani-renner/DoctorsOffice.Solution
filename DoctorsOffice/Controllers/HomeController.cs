using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System.Collections.Generic;
using System.Linq;

// namespace DoctorsOffice.Controllers
// {
//   public class HomeController : Controller
//   {
//     private readonly DoctorsOfficeContext _db;

//     public HomeController(DoctorsOfficeContext db)
//     {
//       _db = db;
//     }

//     [HttpGet("/")]
//     public ActionResult Index()
//     {
//       return View();
//     }

//     public ActionResult Search(string name)
//     {
//       ViewBag.DoctorsFound = _db.Doctors.Where(doctor => doctor.Name.Contains(name));
//       return View();
//     }

//     [HttpPost]
//     public ActionResult Search(Doctor doctor)
//     {
//       var doctorsFound = _db.Doctors.Where(doc => doc.Name.Contains(doctor.Name));
//       return RedirectToAction("Search");
//     }

//   }
// }


namespace DoctorsOffice.Controllers
{
  public class HomeController : Controller
  {
    private readonly DoctorsOfficeContext _db;
    private List<SelectListItem> _selectList;

    public HomeController(DoctorsOfficeContext db)
    {
      _db = db;
      _selectList = new List<SelectListItem>()
      { 
        new SelectListItem {Text = "All", Value = "All"},
        new SelectListItem {Text = "Doctor", Value = "Doctor"},
        new SelectListItem {Text = "Patient", Value = "Patient"}
      };
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
    
    public ActionResult Search()
    {
      ViewBag.Category = _selectList;
      return View();
    }

    [HttpPost]
    public ActionResult Search(string category, string name)
    {
      ViewBag.Category = _selectList;
      List<Doctor> doctorSearch = new List<Doctor>{};
      List<Patient> patientSearch = new List<Patient> {};
      if(category != "Patient")
      {
        doctorSearch = _db.Doctors.Where(Doctor => Doctor.Name.Contains(name)).ToList();
      }
      if(category != "Doctor")
      {
        patientSearch = _db.Patients.Where(Patient => Patient.Name.Contains(name)).ToList();
      }
      Dictionary<string,object> model = new Dictionary<string,object>(); // dynamic 
      model.Add("DoctorResults",doctorSearch);
      model.Add("PatientResults",patientSearch);
      return View(model); //model gets lost between here and View. or. something. idk. 
    }

  }
}