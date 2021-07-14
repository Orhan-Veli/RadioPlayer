using MongoDB.Driver;
using Radio_Player.Dal.Data.Abstract;
using Radio_Player.Dal.Data.Concrete;
using Radio_Player.Dal.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Dal.Repositories.Concrete
{
    public class RadioRepository : IEntityRepository<Radio>,IRadioRepository
    {
        private readonly IMongoCollection<Radio> _radios;
        public RadioRepository(IRadioStationDatabaseSettings settings)        
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _radios = database.GetCollection<Radio>(settings.RadioCollectionName);
        }
        public async Task Create(Radio model)
        {           
            await _radios.InsertOneAsync(model);         
        }

        public async Task Delete(string id)
        {
            await _radios.DeleteOneAsync(x=> x.Id==id);
        }

        public async Task<Radio> Get(string id)
        {
            var model = await _radios.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (model == null)
            {
                throw new Exception("Radio is not found");
            }
            return model;
        }

        public async Task<List<Radio>> GetAll(string id)
        {
            var models = await _radios.Find(x => true).ToListAsync();
            return models;
        }

        public async Task<Radio> Update(Radio model)
        {
            var result = await _radios.Find(x => x.Id == model.Id).FirstOrDefaultAsync();
            if (result == null)
            {
                throw new Exception("Update model not found.");
            }
            result.Name = model.Name;
            result.RadioConnection = model.RadioConnection;
            result.Category = model.Category;
            var updatedResult = await _radios.FindOneAndReplaceAsync(x => x.Id == result.Id, result);
            return updatedResult;
        }
    }
}
