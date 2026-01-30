namespace TUnitLab.Application;

public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (string.IsNullOrEmpty(command.Titulo))
        {
            throw new ArgumentException("O título é obrigatório.");
        }

        if (string.IsNullOrEmpty(command.Descricao))
        {
            throw new ArgumentException("A descrição é obrigatória.");
        }

        throw new NotImplementedException();
    }
}