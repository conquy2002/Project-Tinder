using api_test.Data;
using Microsoft.EntityFrameworkCore;

namespace api_test.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new api_testContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<api_testContext>>()))
            {
                // Look for any movies.
                if (context.Hobby.Any())
                {
                    return;   // DB has been seeded
                }

                context.Hobby.AddRange(
                    new Hobby
                    {
                        Name = "Chơi game"
                    },
                    new Hobby
                    {
                        Name = "Nghe Nhạc"
                    },
                    new Hobby
                    {
                        Name = "Hát"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
