using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WooliesXTechnicalChallenge.Enums;
using WooliesXTechnicalChallenge.Models;
using WooliesXTechnicalChallenge.Options;
using WooliesXTechnicalChallenge.Services;

namespace WooliesXTechnicalChallenge.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersService _answersService;

        public AnswersController(IAnswersService answersService)
        {
            _answersService = answersService;
        }

        // GET api/answers/user
        [HttpGet]
        [Route("answers/user")]
        public TesterSettings GetUser()
        {
            return _answersService.GetTesterSettings();
        }

        // Get api/answers/sort
        [HttpGet]
        [Route("answers/sort")]
        public IEnumerable<Product> Sort([FromQuery]string sortOption)
        {
            if (Enum.TryParse(sortOption, true, out SortOptions sortOptionEnum))
            {
                return _answersService.GetProducts(sortOptionEnum);
            }
            else
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }

        // Post api/sort
        [HttpPost]
        [Route("trolleyCalculator")]
        public decimal TrolleyCalculator([FromBody]TrolleyCalculatorRequest request)
        {
            return _answersService.GetTrolleyCalculator(request);
        }
    }
}
