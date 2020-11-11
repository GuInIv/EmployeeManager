using System;
using System.Collections.Generic;
using AutoMapper;
using EmployeeManager.ServerApp.Models;
using EmployeeManager.ServerApp.Models.BindingTargets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace EmployeeManager.ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : Controller
    {
        private DataContext context;
        IMapper mapper;

        public PositionsController(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Position> Getpositions()
        {
            return context.Positions;
        }

        [HttpPost]
        public IActionResult CreatePosition([FromBody] PositionData pdata)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var p = mapper.Map<PositionData, Position>(pdata);

            try
            {
                context.Add(p);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest(ModelState);
            }

            return Ok(p.Id);
        }
    }
}