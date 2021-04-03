namespace DevToDev.Application.Common.Interfaces
{
    // TODO: Change class and method naming.
    public interface IHashService
    {
        public string Hash(string stringToHashing);

        public bool Verify(string firstString, string secondString);
    }
}