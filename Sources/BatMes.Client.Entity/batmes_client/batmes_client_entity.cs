using System.Data.Entity;

namespace BatMes.Client.Entity.batmes_client
{
    public partial class batmes_client_entity : DbContext
    {
        public batmes_client_entity(string connectionString)
            : base(connectionString)
        {
        }
    }
}
