# Testes unitários .NET usando TUnit


Exemplo de como criar testes unitários para projetos .NET
usando o framework [TUnit](https://tunit.dev).

![](tunit-banner.webp)

# Requisitos da aplicação testada

Trata-se de um cadastro de tilha de estudos, com título, descrição,
link de imagem e links externos diversos.

- Título e descrição são obrigatórios
- Título com no máximo 60 caracteres
- Descrição com no máximo 300 caracteres
- Não são permitidos links repetidos
- Não pode haver um link externo diverso igual ao link de imagem
- Não deixa apagar se houver aulas registradas
- Links devem ser URLs de internet válidos (FTP, HTTP ou HTTPS) com no máximo 100 caracteres
