using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using PoisnFang.Todo.Entities;

namespace PoisnFang.Todo.Services
{
    public class TodoRepoService<TEntity> : ServiceBase, IService, ITodoRepoService<TEntity> where TEntity : Entity
    {
        private readonly SiteState _siteState;
        private readonly string _controllerName;

        public TodoRepoService(HttpClient http, SiteState siteState, string controllerName) : base(http)
        {
            _siteState = siteState;
            _controllerName = controllerName;
        }

        private string Apiurl => CreateApiUrl(_siteState.Alias, _controllerName);

        public async Task<List<TEntity>> GetAllByRouteAsync(string route)
        {
            var uri = string.Join('/', Apiurl, route);
            return await GetJsonAsync<List<TEntity>>(uri);
        }

        public async Task<TEntity> GetByRouteAsync(string route)
        {
            var uri = string.Join('/', Apiurl, route);
            return await GetJsonAsync<TEntity>(uri);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await GetJsonAsync<TEntity>($"{Apiurl}/{id}");
        }

        public async Task<TEntity> AddNewAsync(TEntity entity)
        {
            return await PostJsonAsync($"{Apiurl}?entityid={entity.Id}", entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await PutJsonAsync($"{Apiurl}/{entity.Id}?entityid={entity.Id}", entity);
        }

        public async Task DeleteAsync(int pkId)
        {
            await DeleteAsync($"{Apiurl}/{pkId}");
        }
    }
}