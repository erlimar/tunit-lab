# Iniciar com TDD

1) Instalar biblioteca TUnit no projeto de teste

```sh
cd test/ApplicationTests
dotnet package add TUnit

cat TUnitLab.ApplicationTests.csproj <<EOF
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="TUnit" Version="1.12.93" />
  </ItemGroup>

</Project>
EOF

```

2) O projeto de teste agora tem algumas mudanças:

```sh
cd test/ApplicationTests

dotnet restore # ok

dotnet build   # warning CS7022:
  # O ponto de entrada do programa é o código global.
  # Ignorando o ponto de entrada 'MicrosoftTestingPlatformEntryPoint.Main(string[])

dotnet run     # ok
dotnet test    # fail
```

Removendo `Program.cs`

```sh
rm Program.cs

dotnet restore # ok
dotnet build   # ok
dotnet test    # error Testing with VSTest target is no longer supported by
  # Microsoft.Testing.Platform on .NET 10 SDK and later. If you use dotnet test,
  # you should opt-in to the new dotnet test experience.
  # For more information, see https://aka.ms/dotnet-test-mtp-error
```

```console
dotnet run

████████╗██╗   ██╗███╗   ██╗██╗████████╗
╚══██╔══╝██║   ██║████╗  ██║██║╚══██╔══╝
   ██║   ██║   ██║██╔██╗ ██║██║   ██║
   ██║   ██║   ██║██║╚██╗██║██║   ██║
   ██║   ╚██████╔╝██║ ╚████║██║   ██║
   ╚═╝    ╚═════╝ ╚═╝  ╚═══╝╚═╝   ╚═╝

   TUnit v1.12.93.0 | ... | Microsoft Testing Platform v2.0.2

   Engine Mode: SourceGenerated
```
```
```

Corrigindo comando `dotnet test`:

```diff
{
  "sdk": {
     "rollForward": "feature",
     "version": "10.0.100"
+  },
+  "test": {
+    "runner": "Microsoft.Testing.Platform"
   }
}
```

```sh
dotnet test  # ok
```

```
```
3) Usar TDD para "Título e descrição são obrigatórios"

Façamos "uma coisa de cada vez"

Primeiro teste falha **(RED)**:

```cs
// test/ApplicationTests/CriarTrilhaDeEstudoCommandTest.cs
using TUnitLab.Application;

namespace TUnitLab.ApplicationTests;

public class CriarTrilhaDeEstudoCommandTest
{
    [Test]
    public async Task TituloEhObrigatorioAoCriar()
    {
        var handler = new CriarTrilhaDeEstudoCommandHandler(/* dependencies */);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => handler.HandleAsync(new CriarTrilhaDeEstudoCommand
            {
                Titulo = null!,
                Descricao = "Descricao válida"
            })
        );

        await Assert.That(exception!.Message).IsEqualTo("O título é obrigatório.");
    }
}
```

Criamos as classes desejadas, para pelo menos compilar, mas ainda falhando **(RED)**:

```sh
rm src/Application/Class1.cs

dotnet reference --project test/ApplicationTests/TUnitLab.ApplicationTests.csproj add src/Application/TUnitLab.Application.csproj
```

```cs
// src/Application/CriarTrilhaDeEstudoCommand.cs
namespace TUnitLab.Application;

public class CriarTrilhaDeEstudoCommand
{
    public string Titulo { get; set; } = null!;
    public string Descricao { get; set; } = null!;
}
```

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
namespace TUnitLab.Application;

public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        throw new NotImplementedException();
    }
}
```

Implementamos versão inicial da funcionalidade para passar **(GREEN)**:

```cs
// src/Application/CriarTrilhaDeEstudoCommandHandler.cs
namespace TUnitLab.Application;

public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        if (string.IsNullOrEmpty(command.Titulo))
        {
            throw new ArgumentException("O título é obrigatório.");
        }

        throw new NotImplementedException();
    }
}
```