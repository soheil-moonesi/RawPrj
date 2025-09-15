using MediatR;

namespace Application.Common.Behaviour
{
    public class RequestResponseMediateRDemo<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            Console.WriteLine("mediateR is ok");
            
           //Calls the actual handler: await next() passes the request to the AddNumbersQueryHandler
            var response = await next();

            Console.WriteLine("mediateR is Okey after next");

            return response;
        }
    }


}