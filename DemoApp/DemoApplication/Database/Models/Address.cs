using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Address : BaseEntity<int>, IAuditable
    {
        public string AcceptorFirstName { get; set; }
        public string AcceptorLastName { get; set; }
        public string AddressName { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
