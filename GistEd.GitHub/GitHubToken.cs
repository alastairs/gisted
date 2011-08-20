using System.Linq;
using System.Security;

namespace GistEd.GitHub
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

        public string Username { get { return username; } }
    }
}
