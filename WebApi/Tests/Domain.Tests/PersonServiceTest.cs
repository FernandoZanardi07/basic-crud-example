using ExampleCrud.WebApi.Application.Interfaces;
using ExampleCrud.WebApi.Domain.Entities;
using WebApi.Domain.Services;
using Xunit;

namespace ExampleCrud.WebApi.Tests.Domain.Tests
{
    public class PersonServiceTest
    {
        private readonly IPersonService _personService;

        public PersonServiceTest()
        {
            _personService = new PersonService();
        }

        [Fact]
        public async Task HasValidDiferencesToUpdate_ShouldReturnFalse_WhenNoDifferences()
        {
            var oldPerson = new Person { CpfNumber = 16523024091, Name = "John Doe", DateOfBirth = new DateTime(1990, 1, 1), IsActive = true };
            var newPerson = new Person { CpfNumber = 22352929091, Name = "John Doe", DateOfBirth = new DateTime(1990, 1, 1), IsActive = true };

            var exception = await Assert.ThrowsAsync<Exception>(() => _personService.HasValidDiferencesToUpdate(oldPerson, newPerson));

            Assert.Equal("CpfNumber can't be changed", exception.Message);
        }
    }
}