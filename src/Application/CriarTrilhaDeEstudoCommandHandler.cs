namespace TUnitLab.Application;

public class CriarTrilhaDeEstudoCommandHandler
{
    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }

        if (command.Titulo == null)
        {
            throw new ArgumentException("O título é obrigatório.");
        }

        if (command.Descricao == null)
        {
            throw new ArgumentException("A descrição é obrigatória.");
        }

        throw new NotImplementedException();
    }
}