using MediatR;

public record AddNumbersQuery(int a , int b) : IRequest<int>;

public class AddNumbersQueryHandler : IRequestHandler<AddNumbersQuery, int>
{

    public async Task<int> Handle(AddNumbersQuery request, CancellationToken cancellationToken)
    {

        var  result = request.a + request.b;
        Console.WriteLine("Create Query");
        return  result;

    }
}