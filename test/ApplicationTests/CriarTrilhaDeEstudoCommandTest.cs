using Moq;

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
        var mock = new Mock<ITrilhaDeEstudoRepository>();
        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);

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
        var mock = new Mock<ITrilhaDeEstudoRepository>();
        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);

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
        var mock = new Mock<ITrilhaDeEstudoRepository>();
        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);

        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => handler.HandleAsync(null!)
        );

        await Assert.That(exception!.ParamName).IsEqualTo("command");
    }

    [Test]
    public async Task RepositorioEhObrigatorio()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new CriarTrilhaDeEstudoCommandHandler(null!)
        );

        await Assert.That(exception!.ParamName).IsEqualTo("repository");
    }

    [Test]
    public async Task ComandoValidoDeveGravarNoBanco()
    {
        TrilhaDeEstudo? dbInsertedRecord = null;

        var mock = new Mock<ITrilhaDeEstudoRepository>();

        mock.Setup(m => m.GravarNovoAsync(It.IsAny<TrilhaDeEstudo>()))
            .Callback<TrilhaDeEstudo>(t => dbInsertedRecord = t);

        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);

        var command = new CriarTrilhaDeEstudoCommand
        {
            Titulo = "Título válido",
            Descricao = "Descrição válida"
        };

        await handler.HandleAsync(command);

        await Assert.That(dbInsertedRecord).IsNotNull();
        await Assert.That(dbInsertedRecord!.Titulo).IsEqualTo(command.Titulo);
        await Assert.That(dbInsertedRecord!.Descricao).IsEqualTo(command.Descricao);
    }

}