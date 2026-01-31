# Testes unitários .NET usando TUnit

![](tunit-banner.webp)

Repositório de exemplo e material didático para aprender [TDD (Test Driven Development)](https://martinfowler.com/bliki/TestDrivenDevelopment.html)
com .NET, usando o framework de testes [TUnit](https://tunit.dev).

Principais objetivos:
- Demonstrar ciclo TDD (RED / GREEN / REFACTOR)
- Mostrar uso de mocks com [Moq](https://github.com/devlooped/moq)
- Fornecer um exemplo simples de projeto com testes automatizados

Requisitos
- [.NET 10 SDK](https://dot.net)

# Início rápido

1. Restaurar dependências e compilar:
```sh
dotnet restore
dotnet build
```

2. Executar testes:
```sh
dotnet test
```

# Sobre aplicação de exemplo

Este repositório também é um projeto de exemplo que contém uma aplicação
para cadastro de trilha de estudos. Uma trilha de estudos tem um título,
uma descrição, um link de imagem e alguns outros links externos diversos.

Essas são as regras que queremos garantir com os testes:

- Título e descrição são obrigatórios
- Título com no máximo 60 caracteres
- Descrição com no máximo 300 caracteres
- Não são permitidos links repetidos
- Permitidos apenas 5 links externos diversos
- Não pode haver um link externo diverso igual ao link de imagem
- Não deixa apagar se houver aulas registradas
- Links devem ser URLs de internet válidos (FTP, HTTP ou HTTPS) com no máximo 100 caracteres

Estrutura do projeto
- [src/Application](src/Application) — Código da camada de aplicação
- [test/ApplicationTests](test/ApplicationTests) — testes unitários da aplicação

# Sobre como estudar

- Siga o roteiro em [docs/README.md](docs/README.md)
- Procure não "copiar e colar" os textos, mas reproduzí-los você mesmo um passo de cada vez
- Siga os links apresentados em cada passo e se aprofunde em cada assunto mencionado


> Este projeto está disponível sob a licença _Apache versão 2.0_, descrita em [LICENSE](LICENSE).

