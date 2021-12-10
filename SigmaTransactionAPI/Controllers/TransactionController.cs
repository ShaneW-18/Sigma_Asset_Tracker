using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SigmaTransactionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        // GET: api/<TransactionController>
        [HttpGet]
        public List<TransactionModel> Get()
        {
            var Manager = DataManager.GetInstance();
            var list = Manager.GetTransactionModels();
            return list;
        }
        // GET api/<ChuckController>/{guid}
        [HttpGet("{id}")]
        public TransactionModel? Get(string guid)
        {
            var Manger = DataManager.GetInstance();
            var TransactionModel = Manger.GetTransactionModels()
                .Find(TransModel => TransModel.Id == guid);

            return TransactionModel;

        }
        // POST api/<TransactionController>
        [HttpPost]
        public TransactionModel Post([FromBody] TransactionModel transactionModel)
        {
            var Manger = DataManager.GetInstance();
            transactionModel.Id = Guid.NewGuid().ToString();
            transactionModel.DateTime = DateTimeOffset.Now;

            Manger.AddItem(transactionModel);

            return transactionModel;
        }
        // PUT api/<ChuckController>/5
        [HttpPut("{id}")]
        public int Put(string id, [FromBody] TransactionModel ChuckModel)
        {
            var Manger = DataManager.GetInstance();
            var Chuck = Manger.GetTransactionModels()
                .Find(model => model.Id == id);

            if (Chuck == null)
            {
                return 43;
            }

            Chuck = ChuckModel;
            return 0;

        }

        // DELETE api/<ChuckController>/5
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
