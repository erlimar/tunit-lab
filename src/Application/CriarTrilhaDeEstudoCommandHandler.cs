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