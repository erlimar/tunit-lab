using TUnitLab.Application;

namespace TUnitLab.ApplicationTests;

public class CriarTrilhaDeEstudoCommandTest
{
    [Test]
    public async Task TituloEhObrigatorio()
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

    [Test]
    public async Task DescricaoEhObrigatoria()
    {
        var handler = new CriarTrilhaDeEstudoCommandHandler(/* dependencies */);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(new CriarTrilhaDeEstudoCommand
            {
                Titulo = "Título válido",
                Descricao = null!
            })
        );

        await Assert.That(exception!.Message).IsEqualTo("A descrição é obrigatória.");
    }

    [Test]
    public async Task ComandoEhObrigatorio()
    {
        var handler = new CriarTrilhaDeEstudoCommandHandler(/* dependencies */);

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!)
        );

        await Assert.That(exception!.ParamName).IsEqualTo("command");
    }
}