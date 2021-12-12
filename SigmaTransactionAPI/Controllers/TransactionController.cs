using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SigmaTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        [HttpGet]
        public List<TransactionModel> Get()
        {
            var Manager = DataManager.GetInstance();
            var list = Manager.GetTransactionModels();
            return list;
        }

        [HttpGet("{id}")]
        public ObjectResult? Get(string guid)
        {
            var Manger = DataManager.GetInstance();
            var TransactionModel = Manger.GetTransactionModels()
                .Find(TransModel => TransModel.Id == guid);

            return StatusCode(200, TransactionModel);

        }

        [HttpPost]
        public ObjectResult Post([FromBody] TransactionModel transactionModel)
        {
            var Manger = DataManager.GetInstance();
            transactionModel.Id = Guid.NewGuid().ToString();
            transactionModel.Time = DateTime.Now;

            Manger.AddItem(transactionModel);

            return StatusCode(200, transactionModel);
        }

        [HttpPut("{id}")]
        public int Put(string id, [FromBody] TransactionModel transactionModel)
        {
            var Manger = DataManager.GetInstance();
            var Model = Manger.GetTransactionModels()
                .Find(model => model.Id == id);

            if (Model == null)
            {
                return 43;
            }

            Model = transactionModel;
            return 0;

        }

        [HttpDelete("{id}")]
        public int Delete(string id)
        {
            var Manger = DataManager.GetInstance();
            var Chuck = Manger.GetTransactionModels()
                .RemoveAll(model => model.Id == id);


            return Chuck > 0 ? 95 : 0;
        }
    }
}
