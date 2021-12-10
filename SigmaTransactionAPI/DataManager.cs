using Newtonsoft.Json;

namespace SigmaTransactionAPI
{
    public class DataManager
    {
        private string FileName = "DataPers.json";
        public List<TransactionModel> Models = new List<TransactionModel>();
        private static DataManager Manager = null;


        private DataManager()
        {

            if (!File.Exists(FileName))
            {
                File.Create(FileName);
                return;
            }

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(FileName))
            {
                JsonSerializer serializer = new JsonSerializer();

                Models = (List<TransactionModel>)serializer.Deserialize(file, typeof(List<TransactionModel>));

                if (Models == null)
                    Models = new List<TransactionModel>();
            }
        }

        public static DataManager GetInstance()
        {
            if (Manager == null)
            {
                lock (typeof (DataManager))
                {
                    if (Manager == null)
                    {
                        Manager = new DataManager();
                    }
                }
            }

            return Manager;
        }

        public void AddItem(TransactionModel model)
        {

            if (!File.Exists(FileName))
            {
                File.Create(FileName);
            }

            Models.Add(model);


            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(FileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, Models);
            }
        }

        public bool RemoveItem(TransactionModel model)
        {

            if (!File.Exists(FileName))
            {
                File.Create(FileName);
            }

           var remove =  Models.Remove(model);


            if (!remove) return false;

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(FileName))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, Models);
                return true;
            }
        }

        public List<TransactionModel> GetTransactionModels()
        {
            return Models;
        }
    }
}
