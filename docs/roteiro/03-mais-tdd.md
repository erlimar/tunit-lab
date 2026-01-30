# Mais TDD

1) Continuar o clico TDD para "Título e descrição são obrigatórios"

Primeiro teste falha **(RED)**:

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
public class CriarTrilhaDeEstudoCommandTest
{
    // ...

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
}
```

Implementamos versão funcional para passar **(GREEN)**:

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        if (string.IsNullOrEmpty(command.Titulo))
        {
            throw new ArgumentException("O título é obrigatório.");
        }

        if (string.IsNullOrEmpty(command.Descricao))
        {
            throw new ArgumentException("A descrição é obrigatória.");
        }

        throw new NotImplementedException();
    }
}
```

Mais um teste falho **(RED)**, que muitos deixam passar _desapercebidamente_:

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
public class CriarTrilhaDeEstudoCommandTest
{
    // ...

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
```

Implementamos versão funcional para passar **(GREEN)**:

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (string.IsNullOrEmpty(command.Titulo))
        {
            throw new ArgumentException("O título é obrigatório.");
        }

        if (string.IsNullOrEmpty(command.Descricao))
        {
            throw new ArgumentException("A descrição é obrigatória.");
        }

        throw new NotImplementedException();
    }
}
```

# Novidades importantes

- `ArgumentException` é diferente de `ArgumentNullException`