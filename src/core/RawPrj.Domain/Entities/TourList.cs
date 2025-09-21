//bring in System.Collections.Generic and define the TourList entity
using system.collection.generic;

namespace RawPrj.Domain.Entities
{
    public class TourList
    {
        public TourList()
        {

            //The TourList entity has a constructor that initializes the Tours property
            // with a new List type of TourPackage, new List<TourPackage>(), which
            // sets a one-to-many relationship
            Tours = new List<TourPackage>();
        }
        public IList<TourPackage> Tours { get; set; }

        public int Id { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public string About { get; set; }
    }
}