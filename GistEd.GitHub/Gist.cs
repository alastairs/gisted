namespace GistEd.GitHub
{
    public class Gist
    {
        private readonly int identity;

        public Gist(int identity)
        {
            this.identity = identity;
        }

        public int Identity
        {
            get { return identity; }
        }
    }
}