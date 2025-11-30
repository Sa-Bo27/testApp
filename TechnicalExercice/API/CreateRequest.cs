using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalExercice.API
{
    public record CreateRequest(string? FirstName, string? LastName, string? CompanyName, decimal? Revenue);
    
}