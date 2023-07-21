using Azure.Security.KeyVault.Secrets;
using EDG.LoyaltyGames.Core.Interfaces;

namespace EDG.LoyaltyGames.Infrastructure.KeyVault
{
    public class KeyVaultManager : IKeyVaultManager
    {
        private readonly SecretClient _secretClient;

        public KeyVaultManager(SecretClient secretClient)
        {
            _secretClient = secretClient ?? throw new ArgumentNullException(nameof(secretClient));
        }


        public async Task<string> GetSecret(string secretName)
        {
            try
            {
                KeyVaultSecret keyVaultSecret = await _secretClient.GetSecretAsync(secretName);
                return keyVaultSecret.Value;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
