
public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public void AddProduct(Product product)
    {

        var ProductRepository = _unitOfWork.GetRepository<Product>();

        ProductRepository.Add(product);
        
        _unitOfWork.SaveChanges();

    }


}