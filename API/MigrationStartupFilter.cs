using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
  public class MigrationStartupFilter<TContext> : IStartupFilter where TContext : DbContext
  {
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
      return async app =>
      {
        using (var scope = app.ApplicationServices.CreateScope())
        {

          var services = scope.ServiceProvider;
          var loggerFactory = services.GetRequiredService<ILoggerFactory>();
          foreach (var context in scope.ServiceProvider.GetServices<TContext>())
          {
            try
            {
              await context.Database.MigrateAsync();
              if (context is StoreContext) {

                await StoreContextSeed.SeedAsync(context as StoreContext, loggerFactory);

              }
            }
            catch (Exception ex)
            {
              var logger = loggerFactory.CreateLogger<Program>();
              logger.LogError(ex, "An error ocurred during Migration");
            }

          }

        }
        next(app);
      };
    }
  }
}
