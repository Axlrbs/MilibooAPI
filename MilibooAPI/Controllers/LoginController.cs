using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilibooAPI.Models.EntityFramework;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace MilibooAPI.Controllers
{
    public class ClientDTO
    {
        public string NomPersonne { get; set; }
        public string MdpClient { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly MilibooDBContext _dbContext;
        private readonly IConfiguration _config;


        public LoginController(MilibooDBContext milibooDbContext, IConfiguration config) {
            _dbContext = milibooDbContext;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] ClientDTO login)
        {
            IActionResult response = Unauthorized();
            Client client = AuthenticateClient(login);
            if (client != null)
            {
                var tokenString = GenerateJwtToken(login);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = client
                });
            }
            return response;
        }

        private Client AuthenticateClient(ClientDTO login)
        {
            return _dbContext.Clients.SingleOrDefault(x => x.NomPersonne.ToLower() == login.NomPersonne.ToLower() && x.MdpClient == login.MdpClient); //Vérifier si mdp hash dans la base    
        }



        private string GenerateJwtToken(ClientDTO login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, login.NomPersonne),
                new Claim("role", "Authorized"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }; //Info a passer

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
