using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BDSA2019.Lecture11.MobileApp.Models
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISettings _settings;
        private readonly IPublicClientApplication _application;

        public AuthenticationService(ISettings settings, IPublicClientApplication application)
        {
            _settings = settings;
            _application = application;
        }

        public async Task<string> AcquireTokenAsync()
        {
            var accounts = await _application.GetAccountsAsync();

            var token = await AcquireTokenAsync(accounts);

            return token.AccessToken;
        }

        public async Task LogoutAsync()
        {
            var accounts = await _application.GetAccountsAsync();
            while (accounts.Any())
            {
                await _application.RemoveAsync(accounts.First());
                accounts = await _application.GetAccountsAsync();
            }
        }

        private async Task<AuthenticationResult> AcquireTokenAsync(IEnumerable<IAccount> accounts)
        {
            try
            {
                var account = accounts.FirstOrDefault();

                return await _application.AcquireTokenSilent(_settings.Scopes, account)
                                              .ExecuteAsync();
            }
            catch (MsalUiRequiredException)
            {
            }

            return await _application.AcquireTokenInteractive(_settings.Scopes)
                                          .WithAccount(accounts.FirstOrDefault())
                                          .ExecuteAsync();
        }
    }
}
