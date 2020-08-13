using System.Configuration;
namespace BatMes.Client
{
    public static class ConnectionStrings
    {
        public static readonly string BATMES_CLIENT = ConfigurationManager.ConnectionStrings["batmes_client"].ToString();
        public static readonly string BATMES_CLIENT_ENTITY = ConfigurationManager.ConnectionStrings["batmes_client_entity"].ToString();
    }
}
