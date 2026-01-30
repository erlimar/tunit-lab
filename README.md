# Testes unitários .NET usando TUnit


Acompanhe o [roteiro](docs/README.md) e veja o exemplo de como criar
testes unitários para projetos .NET usando o framework [TUnit](https://tunit.dev).

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

