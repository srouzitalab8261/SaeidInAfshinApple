using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Program_PersonASP_NETCoreWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<Doctor> Get()
        {
            var doctors = _doctorServices.GetAll();

            return doctors;

        }
    
        [HttpGet("{name}")]
        public Doctor Get(string name)
        {
            var doctor = _doctorServices.GetByName(name);

            return doctor;

        }

        [HttpGet("The Oldest Doctor")]
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

        [HttpPost]
        public ActionResult Post([FromBody]Doctor doctor)
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

        //[HttpPut("Doctor MOdification")]
        //public ActionResult Put2(int id,[FromBody] Doctor doctor)
        //{
        //    if (doctor == null)
        //        return BadRequest();
        //    _doctorServices.ModifyDoctor(doctor);
        //    return Ok("Doctor Modified!");

        //}
    }
}
