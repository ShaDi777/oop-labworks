using Core.Models.Users;
using Itmo.Dev.Platform.Postgres.Plugins;
using Npgsql;

namespace DataAccess.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.MapEnum<UserRole>();
    }
}