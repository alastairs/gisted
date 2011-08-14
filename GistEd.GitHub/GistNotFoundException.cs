using System;

namespace GistEd.GitHub
{
    public class GistNotFoundException : Exception
    {
        public int Identity { get; private set; }

        public GistNotFoundException(int identity)
        {
            Identity = identity;
        }
    }
}