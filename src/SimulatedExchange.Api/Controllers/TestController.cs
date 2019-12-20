using Microsoft.AspNetCore.Mvc;
using SimulatedExchange.Api.DTO;
using SimulatedExchange.Applications.Services;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.Controllers
{
    [Route("api/test/order")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestServices testServices;

        public TestController(ITestServices testServices)
        {
            this.testServices = testServices;
        }

        /// <summary>
        /// 成交
        /// </summary>
        [HttpPut]
        [Route("{id}/deal")]
        public async Task Deal([FromRoute]string id, [FromBody]OrderDealRequest request)
        {
            await testServices.TransactionAsync(id, request.Amount, request.Price);
        }
    }
}