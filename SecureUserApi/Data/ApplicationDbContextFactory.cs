// Data/ApplicationDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SecureUserApi.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Lấy cấu hình từ appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Tạo DbContextOptionsBuilder
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Lấy chuỗi kết nối
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Cấu hình để sử dụng SQL Server
        builder.UseSqlServer(connectionString);

        // Trả về một instance của ApplicationDbContext
        return new ApplicationDbContext(builder.Options);
    }
}