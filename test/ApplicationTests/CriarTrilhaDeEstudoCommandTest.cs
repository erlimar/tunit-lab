using TUnitLab.Application;

namespace TUnitLab.ApplicationTests;

public class CriarTrilhaDeEstudoCommandTest
{
    [Test]
    public async Task TituloEhObrigatorioAoCriar()
    {
        var handler = new CriarTrilhaDeEstudoCommandHandler(/* dependencies */);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(new CriarTrilhaDeEstudoCommand
            {
                Titulo = null!,
                Descricao = "Descricao válida"
            })
        );

        await Assert.That(exception!.Message).IsEqualTo("O título é obrigatório.");
    }
}