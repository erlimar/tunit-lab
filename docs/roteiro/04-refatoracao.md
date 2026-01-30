# Refatoração

Podemos refatorar qualquer código, inclusive código de teste.

1) Refatorando código de teste liga sinal **YELLOW** (atenção! cuidado! nada pode quebrar.)

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
public class CriarTrilhaDeEstudoCommandTest
{
    // ...

    [Test]
    [Arguments(null)]
    public async Task TituloEhObrigatorio(string? tituloInvalido)
    {
        /* ... */ new CriarTrilhaDeEstudoCommand
        {
            Titulo = tituloInvalido!,
            Descricao = "Descricao válida"
        };
        // ...
    }

    [Test]
    [Arguments(null)]
    public async Task DescricaoEhObrigatoria(string? descricaoInvalida)
    {
        /* ... */ new CriarTrilhaDeEstudoCommand
        {
            Titulo = "Título válido",
            Descricao = descricaoInvalida!
        };
        // ...
    }
}
```

Mas na maioria das vezes a refatoração de código de testes, visam apenas a possibilidade
de fazer mais testes, ou mudar o que estamos validando.

2) Com código de teste refatorado, agora podemos adicionar mais testes de maneira simplificada:

> Reiniciamos o ciclo e deixamos tudo **RED** novamente!

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
public class CriarTrilhaDeEstudoCommandTest
{
    // ...

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("      ")]
    public async Task TituloEhObrigatorio(string? tituloInvalido)
    {
        /* ... */ new CriarTrilhaDeEstudoCommand
        {
            Titulo = tituloInvalido!,
            Descricao = "Descricao válida"
        };
        // ...
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("      ")]
    public async Task DescricaoEhObrigatoria(string? descricaoInvalida)
    {
        /* ... */ new CriarTrilhaDeEstudoCommand
        {
            Titulo = "Título válido",
            Descricao = descricaoInvalida!
        };
        // ...
    }
}
```

3) Continuamos com as implementações funcionais para passar **(GREEN)**:

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

        if (command.Titulo == null || command.Titulo == "" || command.Titulo.Trim() == "")
        {
            throw new ArgumentException("O título é obrigatório.");
        }

        if (command.Descricao == null || command.Descricao == "" || command.Descricao.Trim() == "")
        {
            throw new ArgumentException("A descrição é obrigatória.");
        }

        throw new NotImplementedException();
    }
}
```

4) Refatoremos mais código de teste, agora para verificar algo de forma diferente (**RED**):

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
public class CriarTrilhaDeEstudoCommandTest
{
    // ...

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("      ")]
    public async Task TituloEhObrigatorio(string? tituloInvalido)
    {
        // ...
        await Assert.That(exception!.ParamName).IsEqualTo(nameof(CriarTrilhaDeEstudoCommand.Titulo));
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("      ")]
    public async Task DescricaoEhObrigatoria(string? descricaoInvalida)
    {
        // ...
        await Assert.That(exception!.ParamName).IsEqualTo(nameof(CriarTrilhaDeEstudoCommand.Descricao));
    }
}
```

5) Ao quebrar o código com as novas verificações, precisamos reimplementar para passar **(GREEN)**:

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        // ...

        if (command.Titulo == null || command.Titulo == "" || command.Titulo.Trim() == "")
        {
            throw new ArgumentException("O título é obrigatório.", nameof(command.Titulo));
        }

        if (command.Descricao == null || command.Descricao == "" || command.Descricao.Trim() == "")
        {
            throw new ArgumentException("A descrição é obrigatória.", nameof(command.Descricao));
        }

        throw new NotImplementedException();
    }
}
```

Mas o sentido real da fase de refatoração no TDD é percebida quando na verdade não
escrevemos nenhum código de teste novo, e todos os atuais estão passando (**GREEN**).

Isso quer dizer que agora podemos _"dar uma melhorada em nosso código funcional"_
sem medo de quabrar tudo. Basicamente modificamos o código e executamos os testes,
e se algum falhar, quer dizer que aquela mudança "QUEBROU" nosso código e temos a
certeza do **que não fazer**.

6) Vamos refatorar o código sem quebrar **(GREEN)**:

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
namespace TUnitLab.Application;

public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (string.IsNullOrWhiteSpace(command.Titulo))
        {
            throw new ArgumentException("O título é obrigatório.", nameof(command.Titulo));
        }

        if (string.IsNullOrWhiteSpace(command.Descricao))
        {
            throw new ArgumentException("A descrição é obrigatória.", nameof(command.Descricao));
        }

        throw new NotImplementedException();
    }
}
```

7) Mais um pouco de refatoração de código sem quebrar **(GREEN)**:

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Titulo, nameof(command.Titulo));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Descricao, nameof(command.Descricao));

        throw new NotImplementedException();
    }
}
```

8) Agora se ficarmos empolgados demais podemos deixar tudo **(RED)**

> Podemos fazer uma pequena mudança na ordenação das linhas de código, que apesar
> de compilar `dotnet build`, não passa nos testes `dotnet test`. Então desista
> dessa mudança e deixe tudo como estava antes.

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Titulo, nameof(command.Titulo));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Descricao, nameof(command.Descricao));
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        throw new NotImplementedException();
    }
}
```

# Novidades importantes

- Métodos de testes podem conter argumentos para simplificar as verificações
- Podemos refatorar código de teste
- O verdadeiro sentido do estágio **YELLOW** no TDD é percebido ao refatorar código funcional
