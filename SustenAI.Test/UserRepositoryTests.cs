using Microsoft.EntityFrameworkCore;
using SustenAI.DataBase;
using SustenAI.Models;
using SustenAI.Repository;

namespace SustenAI.Test
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _repository;
        private readonly DbContextOptions<SustenAIDBContext> _options;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<SustenAIDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Ensure database is created
            using (var context = new SustenAIDBContext(_options))
            {
                context.Database.EnsureCreated();
            }

            _repository = new UserRepository(new SustenAIDBContext(_options));
        }

        [Fact]
        public async Task Adicionar_Usuario_Deve_Salvar_Usuario()
        {
            // Arrange
            var usuario = new Usuario
            {
                NomeEmp = "Teste",
                Email = "teste@teste.com",
                Cnpj = "12345678000195", // Adicione um CNPJ válido
                Senha = "senhaSegura123" // Adicione uma senha válida
            };

            // Act
            var result = await _repository.Adicionar(usuario);

            // Assert
            using (var context = new SustenAIDBContext(_options))
            {
                var savedUsuario = await context.Usuarios.FirstOrDefaultAsync(u => u.IdUser == result.IdUser);
                Assert.NotNull(savedUsuario);
                Assert.Equal("Teste", savedUsuario.NomeEmp);
                Assert.Equal("teste@teste.com", savedUsuario.Email);
                Assert.Equal("12345678000195", savedUsuario.Cnpj);
                Assert.Equal("senhaSegura123", savedUsuario.Senha);
            }
        }
    }
}