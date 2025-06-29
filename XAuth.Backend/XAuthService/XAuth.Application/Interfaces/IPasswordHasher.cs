namespace XAuth.Application.Interfaces;

public interface IPasswordHasher
{
    string Hash(string requestPassword);
}