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

    public ActionResult Details(int doctorId)
    {
      var thisDoctor = _db.Doctors
        .Include(doctor => doctor.JoinEntities)
        .ThenInclude(join => join.Patient)
        .FirstOrDefault(doctor => doctor.DoctorId == doctorId);

      return View(thisDoctor);
    }

    public ActionResult Edit(int doctorId)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == doctorId);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult Edit(Doctor doctor, int PatientId)
    {
      if(PatientId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = doctor.DoctorId, PatientId = PatientId});
      }

      _db.Entry(doctor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Delete(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
      _db.Doctors.Remove(thisDoctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPatient(int doctorId)
    {
      var thisDoctor = _db.Doctors.FirstOrDefault(doctor => doctor.DoctorId == doctorId);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "Name");
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult AddPatient(Doctor doctor, int patientId)
    {
      if(patientId != 0)
      {
        _db.DoctorPatient.Add(new DoctorPatient() { DoctorId = doctor.DoctorId, PatientId = patientId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}