namespace TUnitLab.Application;

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