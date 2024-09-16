using Microsoft.EntityFrameworkCore;
using SustenAI.DataBase;
using SustenAI.Models;

namespace SustenAI.Repository
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly SustenAIDBContext _dbContext;

        public ArquivoRepository(SustenAIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Arquivo>> BuscarTodosArquivos()
        {
            return await _dbContext.Arquivos.ToListAsync();
        }

        public async Task<Arquivo> BuscarPorId(int id)
        {
            return await _dbContext.Arquivos.FirstOrDefaultAsync(x => x.IdArq == id);
        }

        public async Task<Arquivo> Adicionar(Arquivo arquivo)
        {
            await _dbContext.Arquivos.AddAsync(arquivo);
            await _dbContext.SaveChangesAsync();
            return arquivo;
        }

        public async Task<Arquivo> Atualizar(Arquivo arquivo, int id)
        {
            Arquivo arquivoPorId = await BuscarPorId(id);

            if (arquivoPorId == null)
            {
                throw new Exception($"Arquivo para o ID: {id} não foi encontrado.");
            }

            arquivoPorId.NomeArq = arquivo.NomeArq;
            arquivoPorId.TipoArq = arquivo.TipoArq;
            arquivoPorId.DataUpload = arquivo.DataUpload;

            _dbContext.Arquivos.Update(arquivoPorId);
            await _dbContext.SaveChangesAsync();

            return arquivoPorId;
        }

        public async Task<bool> Apagar(int id)
        {
            Arquivo arquivoPorId = await BuscarPorId(id);

            if (arquivoPorId == null)
            {
                throw new Exception($"Arquivo para o ID: {id} não foi encontrado.");
            }

            _dbContext.Arquivos.Remove(arquivoPorId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}