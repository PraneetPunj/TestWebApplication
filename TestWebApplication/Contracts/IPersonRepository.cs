using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApplication.Models;

namespace TestWebApplication.Contracts
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAll();
    }
}
