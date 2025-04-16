﻿using BachataApi.Configuration;
using BachataApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BachataApi.Services
{
    public class FiguraService
    {
        private readonly IMongoCollection<Figura> _figurasCollection;

        public FiguraService(IOptions<MongoDbSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _figurasCollection = database.GetCollection<Figura>(settings.Value.FigurasCollectionName);
        }

        // CRUD Figura

        public async Task<List<Figura>> GetAllAsync() =>
            await _figurasCollection.Find(_ => true).ToListAsync();

        public async Task<Figura?> GetByIdAsync(string id) =>
            await _figurasCollection.Find(f => f.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Figura figura) =>
            await _figurasCollection.InsertOneAsync(figura);

        public async Task UpdateAsync(string id, Figura updatedFigura) =>
            await _figurasCollection.ReplaceOneAsync(f => f.Id == id, updatedFigura);

        public async Task DeleteAsync(string id) =>
            await _figurasCollection.DeleteOneAsync(f => f.Id == id);


        // PASOS: Agregar un nuevo paso a una figura
        public async Task AddPasoAsync(string figuraId, Paso paso)
        {
            var update = Builders<Figura>.Update.Push(f => f.Pasos, paso);
            await _figurasCollection.UpdateOneAsync(f => f.Id == figuraId, update);
        }

        // PASOS: Editar un paso existente
        public async Task UpdatePasoAsync(string figuraId, Paso paso)
        {
            var figura = await GetByIdAsync(figuraId);
            if (figura is null) return;

            var index = figura.Pasos.FindIndex(p => p.Id == paso.Id);
            if (index == -1) return;

            figura.Pasos[index] = paso;
            await UpdateAsync(figuraId, figura);
        }

        // PASOS: Eliminar un paso
        public async Task DeletePasoAsync(string figuraId, string pasoId)
        {
            var update = Builders<Figura>.Update.PullFilter(f => f.Pasos, p => p.Id == pasoId);
            await _figurasCollection.UpdateOneAsync(f => f.Id == figuraId, update);
        }

        // PASOS: Intercambiar orden entre dos pasos
        public async Task SwapPasosAsync(string figuraId, string pasoId1, string pasoId2)
        {
            var figura = await GetByIdAsync(figuraId);
            if (figura is null) return;


            var paso1 = figura.Pasos.Find(p => p.Id == pasoId1);
            var paso2 = figura.Pasos.Find(p => p.Id == pasoId2);            
            if (paso1 is null || paso2 is null) return;

            (paso2.Orden, paso1.Orden) = (paso1.Orden, paso2.Orden);
            await UpdateAsync(figuraId, figura);
        }

    }
}
