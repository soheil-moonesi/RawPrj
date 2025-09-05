using MediatR;

namespace Application.Common.Behaviour
{
    public class RequestResponseMediateRDemo<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("mediateR is ok");
            throw new NotImplementedException();
        }
    }


}