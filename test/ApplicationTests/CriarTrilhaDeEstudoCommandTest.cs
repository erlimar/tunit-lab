using TUnitLab.Application;

namespace TUnitLab.ApplicationTests;

public class CriarTrilhaDeEstudoCommandTest
{
    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("      ")]
    public async Task TituloEhObrigatorio(string? tituloInvalido)
    {
        var handler = new CriarTrilhaDeEstudoCommandHandler(/* dependencies */);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(new CriarTrilhaDeEstudoCommand
            {
                Titulo = tituloInvalido!,
                Descricao = "Descricao válida"
            })
        );

        await Assert.That(exception!.ParamName).IsEqualTo(nameof(CriarTrilhaDeEstudoCommand.Titulo));
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("      ")]
    public async Task DescricaoEhObrigatoria(string? descricaoInvalida)
    {
        var handler = new CriarTrilhaDeEstudoCommandHandler(/* dependencies */);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(new CriarTrilhaDeEstudoCommand
            {
                Titulo = "Título válido",
                Descricao = descricaoInvalida!
            })
        );

        await Assert.That(exception!.ParamName).IsEqualTo(nameof(CriarTrilhaDeEstudoCommand.Descricao));
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