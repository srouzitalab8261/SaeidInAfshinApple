using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Program_PersonASP_NETCoreWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Program_PersonASP_NETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly DoctorServices _doctorServices;

        public DoctorController(ILogger<DoctorController> logger, DoctorServices doctorServices)
        {
            _logger = logger;
            _doctorServices = doctorServices;
        }

        [HttpGet]

        // :port/Doctor
        public IEnumerable<Doctor> Get()
        {
            var doctors = _doctorServices.GetAll();

            return doctors;

        }

        [HttpPost]
        public ActionResult Post([FromBody] Doctor doctor)
        {
            if (doctor == null)
                return BadRequest();
            _doctorServices.AddNew(doctor);
            return Ok("Doctor Created!");

        }
        [HttpPut]
        public ActionResult Put([FromBody] Doctor doctor)
        {
            if (doctor == null)
                return BadRequest();
            _doctorServices.ModifyDoctor(doctor);
            return Ok("Doctor Modified!");

        }

        [HttpDelete]
        public ActionResult Delete(int id)

        {
            var item = _doctorServices.GetAll().FirstOrDefault(x => x.Id == id);
            if (item == null)
                return NotFound();

            _doctorServices.DeleteDoctor(item);
            return Ok("Doctor Deleted!");
        }

        [HttpGet("{name}")]
        // Port/Doctor/Afshin
        public Doctor GetByName(string name)
        {
            var doctor = _doctorServices.GetByName(name);

            return doctor;

        }

        [HttpGet("Oldest")]
        //port/Doctor/Oldest
        public IEnumerable<Doctor> GetOldst()
        {
            var doctors = _doctorServices.GetOldest();

            return doctors;

        }

        [HttpGet("~/SpecialDoctor/{specialist}")]
        public IEnumerable<Doctor> GetDocBySpeciality(string specialist)
        {
            var doctors = _doctorServices.GetDoctorbySpeciality(specialist);

            return doctors;

        }

       
        [HttpPut("Doctor MOdification")]
        public ActionResult Put2(int id, [FromBody] Doctor doctor)
        {
            if (doctor == null)
                return BadRequest();
            _doctorServices.ModifyDoctor2(id,doctor);
            return Ok("Doctor Modified!");

        }

 
        [HttpDelete("Delete")]
        public ActionResult DeleteDoc2(int id)
        {
            var item = _doctorServices.DeleteDoctor(id);
            if (item == 0)
                return NotFound($"Doctor Id {id} is not found!");

            return Ok($"Doctor Id {id} is deleted!");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDoc_new(int id)
        {

            var item = _doctorServices.DeleteDoctor(id);
            if (item == 0)
                return NotFound($"Doctor Id {id} is not found!");

            return Ok($"Doctor Id {id} is deleted!");
        }


    }
}
