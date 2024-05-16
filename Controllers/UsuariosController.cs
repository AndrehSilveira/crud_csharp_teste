using Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;
using csharp_crud_api.DTO;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
  private readonly UsuariosContext _context;
    
  public UsuariosController(UsuariosContext context)
  {
    _context = context;
  }
 
  [NonAction]
  //FAZER A BUSCA DOS USUÁRIOS CADASTRADOS
  public List<UsuarioDTO> BuscarUsuarios(List<Usuarios> usuario)
  {
    var usuariosDTO = new List<UsuarioDTO>();
    foreach(var item in usuario)
    {
      var usuarioView = new UsuarioDTO()
      {
        Id = item.Id,
        Nome = item.Nome,
        Status = item.Status
      };
      usuariosDTO.Add(usuarioView);
    }
    return usuariosDTO;
  }

  // GET: usuarios
  [HttpGet]
  public ActionResult<IEnumerable<UsuarioDTO>> GetUsers()
  {

    var usuario = _context.Usuario.ToList();

    if (usuario.Count == 0)
    {
      return NotFound("Nenhum usuário encontrado...");
    }

    var usuarioModel = BuscarUsuarios(usuario);

    return Ok(usuarioModel);
    //return Ok();
  }

  //GET: usuarios/lista/status
  [HttpGet("lista/{status}")]
  public ActionResult<IEnumerable<Usuarios>> GetUsers([FromRoute] bool status)
  {

    var usuario = _context.Usuario.Where(x => x.Status == status).ToList();

    if (usuario.Count == 0)
    {
      return NotFound("Nenhum usuário com status informado encontrado...");
    }

    var usuarioModel = BuscarUsuarios(usuario);

    return Ok(usuarioModel);
    return Ok();
  }

  //GET: usuarios/5
  [HttpGet("{id}")]
  public ActionResult<Usuarios> GetUser(int id)
  {
    //var user = await _context.Usuario.FindAsync(id);

    var usuario = _context.Usuario.Where(x => x.Id == id).ToList();

    if (usuario.Count == 0)
    {
      return NotFound("Usuario Não Encontrado...");
    }

    var usuarioModel = BuscarUsuarios(usuario);
    
    return Ok(usuarioModel);
    return Ok();
  }

  //POST usuarios
  [HttpPost]
  public async Task<ActionResult<Usuarios>> PostUser(Usuarios user)
  {
    _context.Usuario.Add(user);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
  }

  //PUT usuarios/5
  [HttpPut("{id}")]
  public async Task<IActionResult> PutUser(int id, Usuarios user)
  {
    user.Id = id;
 
    try{
      _context.Entry(user).State = EntityState.Modified;
      await _context.SaveChangesAsync();

      UsuarioResposta resposta = new UsuarioResposta();
      resposta.Sucesso = $"Usuário {id} alterado!";

      return Ok(resposta);
    }
    catch
    {
      return BadRequest($"Usuário com ID {id} não encontrado...");
    }
    
  }

  //DELETE usuarios/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id)
  {
    var user = await _context.Usuario.FindAsync(id);

    if (user == null)
    {
      return NotFound($"Usuário {id} não encontrado para excluir");
    }

    _context.Usuario.Remove(user);
    await _context.SaveChangesAsync();

    UsuarioResposta resposta = new UsuarioResposta();
      resposta.Sucesso = $"Usuário {id} excluido!";

      return Ok(resposta);
  }

  // dummy endpoint to test the database connection
  [HttpGet("teste")]
  public string Test()
  {
    return "API funcionando!";
  }
}