using System;

namespace GistEd.GitHub
{
    public class GitHubUser
    {
        public virtual int Identity { get; set; }
        public virtual string Username { get; set; }
        public virtual string Name { get; set; }
        public virtual string Company { get; set; }
        public virtual Uri Url { get; set; }
        public virtual Uri GravatarUrl { get; set; }
        public virtual string Email { get; set; }
        public virtual string Biography { get; set; }
    }
}