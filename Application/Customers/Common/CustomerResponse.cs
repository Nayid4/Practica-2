using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Common
{
    public record CustomerResponse(
        Guid Id,
        string FullName,
        string Email,
        string PhoneNumber,
        AddressResponse Address,
        bool Active);

    public record AddressResponse(
        string Country,
        string Line1,
        string Line2,
        string City,
        string State,
        string ZipCode);
}
