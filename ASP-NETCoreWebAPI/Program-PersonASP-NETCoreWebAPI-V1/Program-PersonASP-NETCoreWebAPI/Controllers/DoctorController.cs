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
    }
}
