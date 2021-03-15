using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoctorsOffice.Models;
using System.Collections.Generic;
using System.Linq;

namespace DoctorsOffice.Controllers
{
  public class DoctorsController : Controller
  {
    private readonly DoctorsOfficeContext _db;
    public DoctorsController(DoctorsOfficeContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      // List<Doctor> model = _db.Doctors.ToList();
      return View(_db.Doctors.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Doctor doctor, int PatientId)
    {
      _db.Doctors.Add(doctor);
      _db.SaveChanges();

      if(PatientId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = doctor.DoctorId, PatientId = PatientId });
      }

      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}