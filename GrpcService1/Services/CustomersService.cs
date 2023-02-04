using Grpc.Core;
using GrpcService1.Protos;

namespace GrpcService1.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;
        
        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";

            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else if (request.UserId == 3)
            {
                output.FirstName = "Greg";
                output.LastName = "Thomas";
            }

            return Task.FromResult(output);

        }

        public override async Task GetNewCustomer(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Tim",
                    LastName = "cock",
                    Age=29,
                    EmailAddress = "tom cock",
                    IsAlive = true,
                },
                new CustomerModel
                {
                    FirstName = "Nas",
                    LastName = "Edd",
                    Age=29,
                    EmailAddress = "nass.edd@gmail.com",
                    IsAlive = true,
                },
                new CustomerModel
                {
                    FirstName = "Tom",
                    LastName = "Mark",
                    Age=29,
                    EmailAddress = "tom.mark@gmail.com",
                    IsAlive = true,
                },
                new CustomerModel
                {
                    FirstName = "Nancy",
                    LastName = "Albert",
                    Age=29,
                    EmailAddress = "nancy.albert@gmail.com",
                    IsAlive = true,
                }
            };

            foreach(var item in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(item);
            }
        }
    }
}
 