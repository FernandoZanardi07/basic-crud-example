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

        public sealed class HasValidDiferencesToUpdate : PersonServiceTest
        {
            [Fact]
            public async Task HasValidDiferencesToUpdate_GetError()
            {
                var oldPerson = new Person { CpfNumber = 16523024091, Name = "John Doe", DateOfBirth = new DateTime(1990, 1, 1), IsActive = true };
                var newPerson = new Person { CpfNumber = 22352929091, Name = "John Doe", DateOfBirth = new DateTime(1990, 1, 1), IsActive = true };

                var exception = await Assert.ThrowsAsync<Exception>(() => _personService.HasValidDiferencesToUpdate(oldPerson, newPerson));

                Assert.Equal("CpfNumber can't be changed", exception.Message);
            }

            [Theory]
            [MemberData(nameof(GetPersonDataToValidateDiferences))]
            public async Task HasValidDiferencesToUpdate_ShouldValidateDifferences(
                Person oldPerson,
                Person newPerson,
                bool expectedResult
            )
            {
                var result = await _personService.HasValidDiferencesToUpdate(oldPerson, newPerson);
                Assert.Equal(expectedResult, result);
            }

            public static IEnumerable<object[]> GetPersonDataToValidateDiferences()
            {
                var result = new List<object[]>();

                var basePerson = PersonFixture.CreateValidPerson();

                result.Add(new object[]
                {
                basePerson,
                new Person { CpfNumber = basePerson.CpfNumber, Name = "New Name", DateOfBirth = basePerson.DateOfBirth, IsActive = basePerson.IsActive },
                true
                });

                result.Add(new object[]
                {
                basePerson,
                new Person { CpfNumber = basePerson.CpfNumber, Name = basePerson.Name, DateOfBirth = new DateTime(1999, 1, 1), IsActive = basePerson.IsActive },
                true
                });

                result.Add(new object[]
                {
                basePerson,
                new Person { CpfNumber = basePerson.CpfNumber, Name = basePerson.Name, DateOfBirth = basePerson.DateOfBirth, IsActive = false },
                true
                });

                result.Add(new object[]
                {
                basePerson,
                new Person { CpfNumber = basePerson.CpfNumber, Name = basePerson.Name, DateOfBirth = basePerson.DateOfBirth, IsActive = basePerson.IsActive },
                false
                });

                return result;
            }
        }
    }
}