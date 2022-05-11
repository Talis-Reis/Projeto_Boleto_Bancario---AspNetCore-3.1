using System;
using System.Net;
using Api.Boleto.Domain.Entities;
using Api.Boleto.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Boleto.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class BoletoController : ControllerBase
    {

        private IBoletoService _service;

        public BoletoController(IBoletoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{linhaDigitavel}", Name = "GetInfosBoletoWithLinhaDigitavel")]
        public ActionResult<BoletoEntity> getInfosBoleto(string linhaDigitavel)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            try
            {
                if (_service.GetInformacoesBoleto(linhaDigitavel) != null)
                {
                    return Ok(_service.GetInformacoesBoleto(linhaDigitavel));
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
