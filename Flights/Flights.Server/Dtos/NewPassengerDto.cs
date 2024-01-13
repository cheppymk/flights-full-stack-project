using System.ComponentModel.DataAnnotations;

namespace Flights.Domain.Entities
{
    public record NewPassengerDto(
         string Email,
         string FirstName,
         string LastName,
         bool Gender);


}
  
