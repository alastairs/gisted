using System.Linq;
using System.Security;

namespace GistEd.Debugging.RestClient
{
    public class GitHubToken
    {
        private readonly string username;
        private readonly SecureString password;

        public GitHubToken(string username, string password)
        {
            this.username = username;
            this.password = new SecureString();
            password.ToList().ForEach(c => this.password.AppendChar(c));
        }
    }
}
