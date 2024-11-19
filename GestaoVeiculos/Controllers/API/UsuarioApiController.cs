using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using GestaoVeiculos.Models;
using GestaoVeiculos.Repositories.Interfaces;
using GestaoVeiculos.DTOs.Request;
using GestaoVeiculos.DTOs.Response;
using System.Collections.Generic;
using GestaoVeiculos.Filters;
using Microsoft.AspNetCore.Authorization;

namespace GestaoVeiculos.Controllers.API
{
    /// <summary>
    /// Controller responsável pela gestão dos usuários.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioApiController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Construtor para inicializar o repositório de usuários.
        /// </summary>
        /// <param name="usuarioRepository">Repositório de usuários.</param>
        public UsuarioApiController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/UsuarioApi
        /// <summary>
        /// Retorna todos os usuários (sem senha).
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/UsuarioApi
        ///
        /// </remarks>
        /// <response code="200">Retorna a lista de usuários</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            // Mapeando para UsuarioResponseDTO para garantir que a senha não seja retornada
            var responseDtos = new List<UsuarioResponseDTO>();
            foreach (var usuario in usuarios)
            {
                responseDtos.Add(new UsuarioResponseDTO
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email
                });
            }

            return Ok(responseDtos);
        }

        // POST: api/UsuarioApi
        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <param name="usuarioDTO">Dados do usuário a ser adicionado.</param>
        /// <returns>Usuário criado (sem senha).</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/UsuarioApi
        ///     {
        ///         "nome": "João Silva",
        ///         "email": "joao@exemplo.com",
        ///         "senha": "password123"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna o usuário recém-criado</response>
        /// <response code="400">Se o objeto enviado for inválido</response>
        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDTO>> PostUsuario(UsuarioRequestDTO usuarioDTO)
        {
            // Criar um novo usuário
            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Senha = usuarioDTO.Senha // Não retornaremos a senha depois de inserida
            };

            await _usuarioRepository.AddAsync(usuario);

            // Mapeando para UsuarioResponseDTO para garantir que a senha não seja retornada
            var responseDto = new UsuarioResponseDTO
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.ID_Usuario }, responseDto);
        }

        // PUT: api/UsuarioApi/5
        /// <summary>
        /// Atualiza os dados de um usuário específico.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="usuarioDTO">Dados atualizados do usuário.</param>
        /// <returns>Status da operação.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/UsuarioApi/5
        ///     {
        ///         "nome": "João Silva",
        ///         "email": "joao_atualizado@exemplo.com",
        ///         "senha": "novaSenha123"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Quando a atualização for bem-sucedida</response>
        /// <response code="400">Se o ID do usuário for inválido</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioRequestDTO usuarioDTO)
        {
            if (id <= 0)
            {
                return BadRequest("ID de usuário inválido.");
            }

            var usuarioExistente = await _usuarioRepository.GetByIdAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Atualizar os campos editáveis
            usuarioExistente.Nome = usuarioDTO.Nome;
            usuarioExistente.Email = usuarioDTO.Email;
            usuarioExistente.Senha = usuarioDTO.Senha;

            await _usuarioRepository.UpdateAsync(usuarioExistente);

            return NoContent();
        }

        // GET: api/UsuarioApi/5
        /// <summary>
        /// Retorna um usuário específico pelo ID (sem senha).
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Usuário correspondente.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     GET /api/UsuarioApi/5
        ///
        /// </remarks>
        /// <response code="200">Retorna o usuário correspondente</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Mapeando para UsuarioResponseDTO para garantir que a senha não seja retornada
            var responseDto = new UsuarioResponseDTO
            {
                Nome = usuario.Nome,
                Email = usuario.Email
            };

            return Ok(responseDto);
        }

        // DELETE: api/UsuarioApi/5
        /// <summary>
        /// Exclui um usuário específico pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Status da operação.</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     DELETE /api/UsuarioApi/5
        ///
        /// </remarks>
        /// <response code="204">Quando a exclusão for bem-sucedida</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            await _usuarioRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
