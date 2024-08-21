using FormulaApp.Api.Models;

namespace FormulaApp.UnitTests.Fixtures
{
    public static class FansFixtures
    {
        public static new List<Fan> GetFans() => new()
        {
            new Fan() 
            {
                Id = 1,
                Name = "Test",
                Email = "test@test.com"
            },
            new Fan() 
            {
                Id = 2,
                Name = "Test 2",
                Email = "test2@test.com"
            },
        };
    }
}