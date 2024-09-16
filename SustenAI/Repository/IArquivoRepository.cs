using SustenAI.Models;

namespace SustenAI.Repository
{
    public interface IArquivoRepository
    {
        Task<List<Arquivo>> BuscarTodosArquivos();
        Task<Arquivo> BuscarPorId(int id);
        Task<Arquivo> Adicionar(Arquivo arquivo);
        Task<Arquivo> Atualizar(Arquivo arquivo, int id);
        Task<bool> Apagar(int id);
    }
}
