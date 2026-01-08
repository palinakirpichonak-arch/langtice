using System.Text.Json;
using AuthService.IL.Options;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace AuthService.IL.Sessions;

public class SessionStore : ISessionStore
{
    private readonly IConnectionMultiplexer _redisConnection;
    private readonly RedisOptions _options;
    private readonly IDatabase _redisDb;

    public SessionStore(IOptions<RedisOptions> redisOptions)
    {
        _redisConnection = ConnectionMultiplexer.Connect(redisOptions.Value.RedisConnectionString);;
        _redisDb = _redisConnection.GetDatabase();
    }
    
    public async Task StoreAsync(string jti, RefreshTokenState record, TimeSpan ttl, CancellationToken ct)
    {
        var paylaoad = JsonSerializer.Serialize(record);
        
        var key = new RedisKey(jti);
        
        await _redisDb.StringSetAsync(key, paylaoad, ttl);
    }

    public async Task<RefreshTokenState?> GetAsync(string jti, CancellationToken ct)
    {
        var key = new RedisKey(jti);
        
        var data = await _redisDb.StringGetAsync(key);
        if (data.IsNullOrEmpty) return null;

        return JsonSerializer.Deserialize<RefreshTokenState>(data.ToString()!)!;
    }

    public async ValueTask MarkRotatedAsync(string jti, CancellationToken ct)
    {
        var key = new RedisKey(jti);
        
        var data = await _redisDb.StringGetAsync(key);
        if (data.IsNullOrEmpty) return;
        
        var rec = JsonSerializer.Deserialize<RefreshTokenState>(data.ToString()!)!;
        rec.Rotated = true;
        
        var ttl = await _redisDb.KeyTimeToLiveAsync(key) ?? TimeSpan.FromHours(1);
        await _redisDb.StringSetAsync(key, JsonSerializer.Serialize(rec), ttl);
    }

    public async Task RevokeFamilyAsync(Guid familyId, CancellationToken ct)
    {
        var endpoints = _redisConnection.GetEndPoints();
        var server =  _redisConnection.GetServer(endpoints[0]);
        
        foreach (var key in server.Keys(pattern: "refresh:*"))
        {
            var data = await _redisDb.StringGetAsync(key);
            if (data.IsNullOrEmpty) continue;
            var rec = JsonSerializer.Deserialize<RefreshTokenState>(data.ToString()!)!;
            if (rec.FamilyId == familyId)
            {
                await _redisDb.KeyDeleteAsync(key);
            }
        }
    }
}