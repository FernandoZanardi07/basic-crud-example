using ExampleCrud.WebApi.Domain.Entities;

public static class PersonFixture
{
    public static Person CreateValidPerson()
    {
        return new Person
        {
            CpfNumber = 16523024091,
            Name = "John Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            IsActive = true
        };
    }
}