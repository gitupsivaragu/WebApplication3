namespace WebApplication3
{
    public  interface  Ilogin
    {

        Task<string>  Login(Loginuser login);
    }
    public interface IJsonwebtoken
    {
      string GetToken(Loginuser loginuser);
    }
}
