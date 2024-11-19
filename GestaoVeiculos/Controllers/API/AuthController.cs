using Microsoft.AspNetCore.Mvc;
using GestaoVeiculos.DTOs;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System;
using GestaoVeiculos.Repositories.Interfaces;
using GestaoVeiculos.DTOs.Request;

namespace GestaoVeiculos.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação dos usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor para inicializar o repositório de usuário e a configuração do token JWT.
        /// </summary>
        /// <param name="usuarioRepository">Repositório para interagir com dados de usuário.</param>
        /// <param name="configuration">Configurações do sistema, incluindo chave JWT.</param>
        public AuthController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Endpoint para login do usuário.
        /// </summary>
        /// <param name="loginDTO">Objeto contendo os dados de login do usuário (email e senha).</param>
        /// <returns>Retorna um token JWT se o login for bem-sucedido, ou uma mensagem de erro caso contrário.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Auth/login
        ///     {
        ///        "email": "usuario@exemplo.com",
        ///        "senha": "password123"
        ///     }
        /// </remarks>
        /// <response code="200">Retorna o token JWT para o usuário autenticado</response>
        /// <response code="400">Retorna erro se o usuário não for encontrado ou se a senha estiver incorreta</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar se o usuário existe pelo email fornecido
            var usuario = await _usuarioRepository.GetByEmailAsync(loginDTO.Email);
            if (usuario == null)
            {
                return BadRequest("Usuário não encontrado.");
            }

            // Verificar se a senha está correta
            if (usuario.Senha != loginDTO.Senha)
            {
                return BadRequest("Senha incorreta.");
            }

            // Gerar o token JWT
            var token = GenerateJwtToken(usuario);

            return Ok(new { Token = token });
        }

        /// <summary>
        /// Método privado para gerar o token JWT com as informações do usuário.
        /// </summary>
        /// <param name="usuario">Objeto contendo os dados do usuário autenticado.</param>
        /// <returns>Token JWT como string.</returns>
        private string GenerateJwtToken(Models.Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.ID_Usuario.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
