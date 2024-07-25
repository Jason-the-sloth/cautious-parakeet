using Control.Controllers;
using Control.Models;
using LiteDB;

namespace Control.Services
{
    public class CaptureService : ICaptureService
    {
        private readonly ILogger<BotController> _logger;

        public CaptureService(ILogger<BotController> logger)
        {
            _logger = logger;
        }

        public async Task CaptureData(CaptureRequest request)
        {
            try
            {
                using (var db = new LiteDatabase(@"MyData.db"))
                {
                    // Get customer collection
                    var col = db.GetCollection<CaptureRequest>("request");

                    // Insert new customer document (Id will be auto-incremented)
                    col.Insert(request);

                    //// Update a document inside a collection
                    //customer.Name = "Joana Doe";

                    //col.Update(customer);

                    //// Use LINQ to query documents (with no index)
                    //var results = col.Find(x => x.Age > 20);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to write to DB: {error}", ex);
                throw;
            }
        }
    }
}
