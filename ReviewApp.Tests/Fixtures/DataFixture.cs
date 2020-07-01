using ReviewApp.Data;

namespace ReviewApp.Tests.Fixtures
{
    public class DataFixture
    {
        public static Company BuildCompany(string name)
        {
            return new Company()
            {
                Id = 1,
                Name = name,
                Description = "Test"
            };
        }
    }
}