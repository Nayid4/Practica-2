using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Delete
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
