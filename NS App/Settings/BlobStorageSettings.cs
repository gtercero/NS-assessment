
namespace NS_App.Settings
{
    public class BlobStorageSettings
    {

        public string AccountName { get; set; }

        public string ContainerName { get; set; }
        public string AccountKey { get; set; }
        public string ConnectionString
        {
            get
            {
                return $"DefaultEndpointsProtocol=https;AccountName={AccountName};AccountKey={AccountKey};EndpointSuffix=core.windows.net";
            }
        }

    }
}