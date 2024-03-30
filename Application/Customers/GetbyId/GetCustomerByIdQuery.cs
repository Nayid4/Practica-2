using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Customers.Common;
using ErrorOr;
using MediatR;

namespace Application.Customers.GetbyId
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<ErrorOr<CustomerResponse>>;
}
