using Nest;
using WebApi.Models; // Make sure you import the namespace containing PermissionType

namespace WebApi.Persistance
{
    public class PermissionTypeElastic : IRepository<PermissionType>
    {
        private static string INDEX_NAME = "ES_PERM_TYPE_INDEX";
        private readonly IElasticClient _elasticClient;

        public PermissionTypeElastic(string elasticsearchUri)
        {
            var uri = new Uri(elasticsearchUri);

            var settings = new ConnectionSettings(uri)
                .DefaultIndex(INDEX_NAME) // Set the default index name for your PermissionType documents
                .DefaultMappingFor<PermissionType>(m => m
                    .IndexName(INDEX_NAME) // Set the index name for PermissionType documents
                );

            _elasticClient = new ElasticClient(settings);
        }

        public IEnumerable<PermissionType> GetAll()
        {
            var response = _elasticClient.Search<PermissionType>(s => s
                .MatchAll()
                .Size(1000) // Adjust the size as needed
            );

            return response.Documents;
        }

        public PermissionType? GetById(int id)
        {
            var response = _elasticClient.Get<PermissionType>(id);

            if (response.Found)
            {
                return response.Source;
            }

            return null;
        }

        public bool Add(PermissionType entity)
        {
            var response = _elasticClient.IndexDocument(entity);

            return response.Result == Result.Created;
        }

        public bool Update(PermissionType entity)
        {
            var response = _elasticClient.IndexDocument(entity);

            return response.Result == Result.Updated;
        }
    }
}
