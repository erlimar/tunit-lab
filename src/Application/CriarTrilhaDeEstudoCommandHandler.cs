namespace TUnitLab.Application;

public class CriarTrilhaDeEstudoCommandHandler
{
    private readonly ITrilhaDeEstudoRepository _repository;

    public CriarTrilhaDeEstudoCommandHandler(ITrilhaDeEstudoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task HandleAsync(CriarTrilhaDeEstudoCommand command)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Titulo, nameof(command.Titulo));
        ArgumentException.ThrowIfNullOrWhiteSpace(command.Descricao, nameof(command.Descricao));

        await _repository.GravarNovoAsync(new TrilhaDeEstudo
        {
            Titulo = command.Titulo,
            Descricao = command.Descricao
        });
    }
}