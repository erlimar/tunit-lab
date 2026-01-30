# 01 - Criar projeto

1) Arquivos globais .NET:

```sh
dotnet --version
dotnet new global.json --sdk-version $(dotnet --version) --roll-forward feature 
dotnet new .gitignore
dotnet new .editorconfig
```

2) Estrutura simples de diretórios:

```sh
mkdir src
mkdir test
```
```
```

3) Projetos e solução:

```sh
# Projeto de aplicação
dotnet new classlib -n TUnitLab.Application -o src/Application

# Projeto de testes
dotnet new console -n TUnitLab.ApplicationTests -o test/ApplicationTests

# Arquivo de solução
dotnet new sln -n TUnitLab # --format slnx
dotnet sln migrate && rm TUnitLab.sln
cat TUnitLab.slnx <<EOF
<Solution />
EOF

# Adicionando projetos a solução
dotnet sln add src/Application/TUnitLab.Application.csproj
dotnet sln add test/ApplicationTests/TUnitLab.ApplicationTests.csproj

cat TUnitLab.slnx <<EOF
<Solution>
  <Folder Name="/src/" />
  <Folder Name="/src/Application/">
    <Project Path="src/Application/TUnitLab.Application.csproj" />
  </Folder>
  <Folder Name="/test/" />
  <Folder Name="/test/ApplicationTests/">
    <Project Path="test/ApplicationTests/TUnitLab.ApplicationTests.csproj" />
  </Folder>
</Solution>
EOF
```

4) Projetos construtíveis vs projetos executáveis

```sh
cd src/Application
dotnet restore # ok
dotnet build   # ok
dotnet test    # ok
dotnet run     # fail


cd test/ApplicationTests
dotnet restore # ok
dotnet build   # ok
dotnet test    # ok
dotnet run     # ok

cd /
dotnet restore # ok
dotnet build   # ok
dotnet test    # ok
dotnet run     # ok
```

# Novidades importantes

- Uso de `global.json` para fixar a versão do .NET SDK usado no projeto
- Novo arquivo de soluções `.slnx` muito mais simples e legível

