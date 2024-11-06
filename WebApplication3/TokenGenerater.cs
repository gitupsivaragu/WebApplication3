using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Web;
using Swashbuckle.Swagger;


namespace WebApplication3
{
    public class jsonWebtoken : IJsonwebtoken
    {

        public  string GetToken(Loginuser loginuser)
        {
            var authClaim = new List<Claim>();

            /// After Authendication to check dB for user Role Based on 

            try
            {
                if (loginuser!=null)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role, "user"));
                    authClaim.Add(new Claim(ClaimTypes.Name, loginuser.UserName));

                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));

                var token = new JwtSecurityToken(

                    issuer: "http://localhost:61955",
                    audience: "http://localhost:4200",
                    expires: DateTime.Now.AddMinutes(3),
                    claims: authClaim,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return  new JwtSecurityTokenHandler().WriteToken(token).ToString();

                 


            }
            catch (Exception ex)
            {

                //Logging 

            }

            return string.Empty;
        }


    }
 
}
