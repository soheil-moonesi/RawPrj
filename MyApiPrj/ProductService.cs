
public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public void AddProduct(Product product)
    {
        //we get Repository with Product generic from unit of work
        var ProductRepository = _unitOfWork.GetRepository<Product>();

        //we get Repository class with product generic with GetRepository<Product> 
        //it that means we can access "Add" method is implement in repository class
        ProductRepository.Add(product);
        
        _unitOfWork.SaveChanges();

    }


}