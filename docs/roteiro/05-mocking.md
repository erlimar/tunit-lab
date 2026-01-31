# 05 - Mocking

Revise o link ["Práticas recomendadas de teste de unidade para .NET"](https://learn.microsoft.com/pt-br/dotnet/core/testing/unit-testing-best-practices) para entender os termos **_MOCK_**, **_FAKE_** e **_STUB_**.

Em resumo:

- **Fake** é um objeto falso. Ou seja, um objeto qualquer que não é o real.
- **Stub** é uma substituição controlada de objetos dentro do sistema. Normalmente se refere ao fato de substituirmos objetos reais por _fakes_ para conseguir testar sem ter que lidar com as dependências.
- **Mock** é um objeto simulado. Além de ser falso, e obviamente substituir outro real de forma controlada dentro do sistema testado, ele também auxilia nas operações de asserção (_Assert_).

Vamos usar a biblioteca [Moq](https://github.com/devlooped/moq) para conseguir alcançar esses objetivos em nossos testes.

1) Adicionando a biblioteca Moq no projeto de testes:

```sh
cd test/ApplicationTests
dotnet package add Moq
```

2) Vamos testar a gravação no banco de dados através de uma dependência de repositório (**RED**):

Aqui estamos imaginando que nosso comando agora dependerá de um `ITrilhaDeEstudoRepository` para
ser executado. E que se um comando válido for fornecido e processado, espera-se que o método de
gravação no repositório seja chamado uma única vez, recebendo um outro objeto de dado chamado
`TrilhaDeEstudo`. E isso é o suficiente para entendermos que nosso objeto está gravando no banco,
ainda que a implementação de gravação no banco em si não foi implementada.

> Observe aqui que estamos praticando, primeiramente pensar e escrever (o teste) como queremos que
> as coisas se desenrolem, e só depois vamos codificar.

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
using Moq;

// ...

public class CriarTrilhaDeEstudoCommandTest
{
    // ...

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
        var mock = new Mock<ITrilhaDeEstudoRepository>();

        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);

        var command = new CriarTrilhaDeEstudoCommand
        {
            Titulo = "Título válido",
            Descricao = "Descrição válida"
        };

        await handler.HandleAsync(command);

        mock.Verify(r => r.GravarNovoAsync(It.IsAny<TrilhaDeEstudo>()), Times.AtMostOnce());
    }
}
```

O teste obviamente falha, porque o código não compila devido não existirem ainda os objetos referenciados.

3) Vamos incluir as classes novas mencionadas, e pelo menos compilar o código (**RED**):

```cs
// src/Application/ITrilhaDeEstudoRepository.cs
namespace TUnitLab.Application;

public interface ITrilhaDeEstudoRepository
{
    Task GravarNovoAsync(TrilhaDeEstudo trilhaDeEstudo);
}
```

```cs
// src/Application/TrilhaDeEstudo.cs
namespace TUnitLab.Application;

public class TrilhaDeEstudo { }
```

```cs
// src/Application/CriarTrilhaDeEstudoCommand.cs
```

4) Não foi suficiente, precisamos ajustar a assinatura do construtor de nosso manipulador de comando (**RED**):

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
public class CriarTrilhaDeEstudoCommandHandler
{
    public CriarTrilhaDeEstudoCommandHandler(ITrilhaDeEstudoRepository repository) { }

    // ...
}
```

Também precisamos ajustar todos nossos métodos de testes anteriores que instanciavam o
manipulador, para incluir nossa nova dependência. E para isso usaremos um objeto falso:

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
public class CriarTrilhaDeEstudoCommandTest
{
    public async Task TituloEhObrigatorio(string? tituloInvalido)
    {
        var mock = new Mock<ITrilhaDeEstudoRepository>();
        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);
        // ...
    }

    public async Task DescricaoEhObrigatoria(string? descricaoInvalida)
    {
        var mock = new Mock<ITrilhaDeEstudoRepository>();
        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);
        // ...
    }

    public async Task ComandoEhObrigatorio()
    {
        var mock = new Mock<ITrilhaDeEstudoRepository>();
        var handler = new CriarTrilhaDeEstudoCommandHandler(mock.Object);
        // ...
    }
}
```

Isso é o suficiente para compilar e manter os testes anteriores passando, ainda que
os novos testes estejam falhando, conforme já esperado (**RED**).

5) Podemos codificar as funcionalidades, até os novos testes passarem (**GREEN #1**):

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs

public class CriarTrilhaDeEstudoCommandHandler
{
    public CriarTrilhaDeEstudoCommandHandler(ITrilhaDeEstudoRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository, nameof(repository));
    }

    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Titulo, nameof(command.Titulo));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Descricao, nameof(command.Descricao));

        throw new NotImplementedException();
    }
}
```

6) Uma refatoração aqui para referenciar o repositório para uso posterior (**YELLOW #1**):

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs

public class CriarTrilhaDeEstudoCommandHandler
{
    private readonly ITrilhaDeEstudoRepository _repository;

    public CriarTrilhaDeEstudoCommandHandler(ITrilhaDeEstudoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    // ...
}
```

7) Uma última implementação e tudo passa (**GREEN**):

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs

public class CriarTrilhaDeEstudoCommandHandler
{
    private readonly ITrilhaDeEstudoRepository _repository;

    public CriarTrilhaDeEstudoCommandHandler(ITrilhaDeEstudoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Titulo, nameof(command.Titulo));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Descricao, nameof(command.Descricao));

        await _repository.GravarNovoAsync(new TrilhaDeEstudo());
    }
}
```

8) Melhorar o teste (**RED**):

Estamos apenas verificando se o método de gravação no banco foi chamado, mas não
verificamos se os dados enviados estão conforme esperado.

Então precisamos configurar nosso objeto simulado (mock) para simular a chamada
do método, e para isso vamos observar qual objeto foi tentado incluir no banco,
e então podemos verificá-lo em seguida.

Com a nova verificação, vemos que o teste falha e precisa ser corrigido.

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
public class CriarTrilhaDeEstudoCommandTest
{
    // ...

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
```

9) Agora sim, uma última implementação e tudo passa de verdade (**GREEN**):

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs

public class CriarTrilhaDeEstudoCommandHandler
{
    // ...

    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Titulo, nameof(command.Titulo));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Descricao, nameof(command.Descricao));

        await _repository.GravarNovoAsync(new TrilhaDeEstudo
        {
            Titulo = command.Titulo,
            Descricao = command.Descricao
        });
    }
}
```

# Novidades importantes

- A biblioteca Moq nos ajuda com _fakes_, _stubs_ e _mocks_
- Trabalhar com objetos simulados pode ser uma tarefa muito árdua
  - Fazê-la a mão pode custar mais tempo simulando do que de fato testando
  - Usar uma biblioteca como Moq é essencial
- É comum escrever mais código de teste do que código funcional
