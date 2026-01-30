# Mais TDD

Continuando o clico TDD para "Título e descrição são obrigatórios"

1) Mais um teste falho **(RED)**:

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

2) Mais uma implementação funcional para passar **(GREEN)**:

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

3) Mais um teste falho **(RED)**, que muitos deixam passar _desapercebidamente_:

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

4) E mais uma implementação funcional para passar **(GREEN)**:

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

- Escrever o teste antes do código é cultural, e seu cérebro já funciona assim.
  - Só é necessário disciplina para entrar no ritmo
  - A dica? Ao invés de "pensar & fazer", "pense escrevendo" e se tornará muscular
- `ArgumentException` é diferente de `ArgumentNullException`
