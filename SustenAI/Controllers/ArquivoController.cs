using Microsoft.AspNetCore.Mvc;
using SustenAI.Models;
using SustenAI.Repository;

namespace SustenAI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly IArquivoRepository _arquivoRepository;

        public ArquivoController(IArquivoRepository arquivoRepository)
        {
            _arquivoRepository = arquivoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Arquivo>>> BuscarTodosArquivos()
        {
            List<Arquivo> arquivos = await _arquivoRepository.BuscarTodosArquivos();
            return Ok(arquivos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Arquivo>> BuscarPorId(int id)
        {
            Arquivo arquivo = await _arquivoRepository.BuscarPorId(id);

            if (arquivo == null)
            {
                return NotFound($"Arquivo com ID {id} não encontrado.");
            }

            return Ok(arquivo);
        }

        [HttpPost]
        public async Task<ActionResult<Arquivo>> Cadastrar([FromBody] Arquivo arquivoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Arquivo arquivo = await _arquivoRepository.Adicionar(arquivoModel);
            return Ok(arquivo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Arquivo>> Atualizar([FromBody] Arquivo arquivoModel, int id)
        {
            arquivoModel.IdArq = id;
            Arquivo arquivoAtualizado = await _arquivoRepository.Atualizar(arquivoModel, id);

            if (arquivoAtualizado == null)
            {
                return NotFound($"Arquivo com ID {id} não encontrado.");
            }

            return Ok(arquivoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _arquivoRepository.Apagar(id);

            if (!apagado)
            {
                return NotFound($"Arquivo com ID {id} não encontrado.");
            }

            return Ok(apagado);
        }
    }
}
