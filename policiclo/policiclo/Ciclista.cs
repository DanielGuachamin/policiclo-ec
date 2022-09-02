using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace policiclo
{
    public class Ciclista
    {

        FirebaseClient firebaseClient = new FirebaseClient("https://politrip-default-rtdb.firebaseio.com/");

        public async Task<bool> SaveCoordenadas(CiclistaModel ciclistaModel)
        {
            var data = await firebaseClient.Child(nameof(CiclistaModel)).PostAsync(JsonConvert.SerializeObject(ciclistaModel));

            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<List<CiclistaModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(CiclistaModel)).OnceAsync<CiclistaModel>()).Select(item => new CiclistaModel
            {
                Latitud = item.Object.Latitud,
                Longitud = item.Object.Longitud,
                Ubication = item.Object.Ubication,
                Id = item.Key,
                Nombre = item.Object.Nombre
            }).ToList();
        }
    }
}
