using StackExchange.Redis;

namespace SecureRateLimitAPI.Middleware;

public class RateLimitMiddleware
{
private readonly RequestDelegate _next;
private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

public RateLimitMiddleware(RequestDelegate next)
{
_next = next;
}

public async Task Invoke(HttpContext context)
{
var db = redis.GetDatabase();

var ip = context.Connection.RemoteIpAddress.ToString();

var key = $"rate:{ip}";

var count = await db.StringIncrementAsync(key);

if (count == 1)
await db.KeyExpireAsync(key, TimeSpan.FromMinutes(1));

if (count > 100)
{
context.Response.StatusCode = 429;
await context.Response.WriteAsync("Too many requests");
return;
}

await _next(context);
}
}
