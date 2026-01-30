namespace TUnitLab.Application;

public interface ITrilhaDeEstudoRepository
{
    Task GravarNovoAsync(TrilhaDeEstudo trilhaDeEstudo);
}