# Testes unitários .NET usando TUnit


Este repositório é um projeto de exemplo quanto a criar testes unitários .NET
usando o framework [TUnit](https://tunit.dev). Mas também é uma
[documentação](docs/README.md) passo a passo para que você possa aprender
a fazê-lo.

Seguindo esta documentação você conseguirá reproduzir o projeto como está,
além de aprender na prática sobre:

- [TDD (Test Driven Development)](https://martinfowler.com/bliki/TestDrivenDevelopment.html)
- [TUnit](https://tunit.dev)
- [Testes no .NET](https://learn.microsoft.com/pt-br/dotnet/core/testing/)
- [Visão geral do Microsoft.Testing.Platform](https://learn.microsoft.com/pt-br/dotnet/core/testing/microsoft-testing-platform-intro)
- [New, Simpler Solution File Format](https://devblogs.microsoft.com/visualstudio/new-simpler-solution-file-format/)
- [Biblioteca Moq](https://github.com/devlooped/moq)

![](tunit-banner.webp)

# Requisitos da aplicação testada

Trata-se de um cadastro de trilha de estudos, com título, descrição,
link de imagem e links externos diversos.

- Título e descrição são obrigatórios
- Título com no máximo 60 caracteres
- Descrição com no máximo 300 caracteres
- Não são permitidos links repetidos
- Permitidos apenas 5 links externos diversos
- Não pode haver um link externo diverso igual ao link de imagem
- Não deixa apagar se houver aulas registradas
- Links devem ser URLs de internet válidos (FTP, HTTP ou HTTPS) com no máximo 100 caracteres

