using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.WebUI.Repository.Concrete.EntityFramework
{


    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ETicaretContext>
    {
        public ETicaretContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var builder = new DbContextOptionsBuilder<ETicaretContext>();
            var connectionstring = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionstring);
            return new ETicaretContext(builder.Options);
        }
    }


}
