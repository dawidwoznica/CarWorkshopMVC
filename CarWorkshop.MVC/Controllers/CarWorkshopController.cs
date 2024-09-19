using AutoMapper;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshopCommand;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshopCommand;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshopsByEncodedName;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWorkshop.MVC.Controllers
{
    using Application.CarWorkshopService.Commands;
    using Application.CarWorkshopService.Queries.GetAllCarWorkshopsByEncodedNameQuery;
    using Extensions;
    using Models;
    using Newtonsoft.Json;

    public class CarWorkshopController(IMediator mediator, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await mediator.Send(new GetAllCarWorkshopsQuery());

            return View(model);
        }
        
        [Route("CarWorkshop/{encodedName}/Details")]
        public async Task<IActionResult> Details(string encodedName)
        {
            var dto = await mediator.Send(new GetAllCarWorkshopsByEncodedNameQuery(encodedName));
            return View(dto);
        }


        [Route("CarWorkshop/{encodedName}/Edit")]
        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Edit(string encodedName)
        {
            var dto = await mediator.Send(new GetAllCarWorkshopsByEncodedNameQuery(encodedName));

            if (!dto.IsEditable)
                return RedirectToAction("NoAccess", "Home");

            var model = mapper.Map<EditCarWorkshopCommand>(dto);

            return View(model);
        }

        [HttpPost]
        [Route("CarWorkshop/{encodedName}/Edit")]
        public async Task<IActionResult> Edit(string encodedName, EditCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCarWorkshopCommand command)
        {
            if (!ModelState.IsValid)
                return View(command);

            await mediator.Send(command);

            this.SetNotification("success", $"Created car workshop {command.Name}");

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [Authorize]
        [Route("CarWorkshop/CarWorkshopService")]
        public async Task<IActionResult> CreateCarWorkshopService(CreateCarWorkshopServiceCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("CarWorkshop/{encodedName}/CarWorkshopService")]
        public async Task<IActionResult> GetCarWorkshopService(string encodedName)
        {
            var dtos = await mediator.Send(new GetAllCarWorkshopServicesByEncodedNameQuery() { EncodedName = encodedName });
            return Ok(dtos);
        }
    }
}
