using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Handlers.Interfaces
{
#pragma warning disable S3246 // Generic type parameters should be co/contravariant when possible

    public interface IHandler<T> where T : ICommand
#pragma warning restore S3246 // Generic type parameters should be co/contravariant when possible
    {
        ICommandResult Handle(T command);

        void Validate(T command);
    }
}