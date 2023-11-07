using Nest;
using WebApi.Models;

namespace WebApi.Persistance.EntityFramework
{
    public class PermissionElastic : IRepository<Permission>
    {
        private static string INDEX_NAME = "ES_PERM_INDEX" ;
        private readonly IElasticClient _elasticClient;

        public PermissionElastic(string elasticsearchUri)
        {
            var uri = new Uri(elasticsearchUri); 

            var settings = new ConnectionSettings(uri)
                .DefaultIndex(INDEX_NAME) 
                .DefaultMappingFor<Permission>(m => m
                    .IndexName(INDEX_NAME) 
                );

            _elasticClient = new ElasticClient(settings);
        }

        public IEnumerable<Permission> GetAll()
        {
            var response = _elasticClient.Search<Permission>(s => s
                .MatchAll()
                .Size(1000) // Adjust the size as needed
            );

            return response.Documents;
        }

        public Permission? GetById(int id)
        {
            var response = _elasticClient.Get<Permission>(id);
            Permission? perm = null;

            if (response.Found)
            {
                perm = response.Source;
            }

            return perm;
        }

        public bool Add(Permission entity)
        {
            var response = _elasticClient.IndexDocument(entity);

            return response.Result == Result.Created;
        }

        public bool Update(Permission entity)
        {
            var response = _elasticClient.IndexDocument(entity);

            return response.Result == Result.Updated;
        }
    }
}
