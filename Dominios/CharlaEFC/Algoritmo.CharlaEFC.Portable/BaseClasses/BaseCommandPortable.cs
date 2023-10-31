using MediatR;
using Algoritmo.Microservices.Shared.Portable.BaseClasses;

namespace Algoritmo.CharlaEFC.Portable.BaseClasses
{
  /// <summary>
  /// Clase base usada por API requests
  /// </summary>
  public abstract class BaseCommandPortable<TCommandResponse>: BaseCommand, IRequest<TCommandResponse> where TCommandResponse : BaseCommandPortableResponse
    {

    }
}
