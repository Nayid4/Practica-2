using Application.Customers.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.GetAll
{
    public record GetAllCustomersQuery() : IRequest<ErrorOr<IReadOnlyList<CustomerResponse>>>;
}
