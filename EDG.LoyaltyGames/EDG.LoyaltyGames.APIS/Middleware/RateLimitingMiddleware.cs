using System.Collections.Concurrent;

namespace EDG.LoyaltyGames.APIS.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConcurrentDictionary<string, DateTime> _throttleTracker = new ConcurrentDictionary<string, DateTime>();
        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string clientId = GetClientId(httpContext);
            DateTime lastRequest;
            bool shouldThrottle = false;

            if (_throttleTracker.TryGetValue(clientId, out lastRequest))
            {
                TimeSpan timeSinceLastRequest = DateTime.UtcNow - lastRequest;
                // Define your rate limiting and throttling policies here
                TimeSpan minimumTimeBetweenRequests = TimeSpan.FromSeconds(1); // Example: allow one request per second

                if (timeSinceLastRequest < minimumTimeBetweenRequests)
                {
                    shouldThrottle = true;
                }
            }

            if (shouldThrottle)
            {
                httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await httpContext.Response.WriteAsync("Too Many Requests. Please try again later.");
                return;
            }

            _throttleTracker.AddOrUpdate(clientId, DateTime.UtcNow, (key, value) => DateTime.UtcNow);

            // Continue to the next middleware in the pipeline
            await _next(httpContext);
        }

        private string GetClientId(HttpContext httpContext)
        {
            // Implement a method to uniquely identify clients based on their request, e.g., IP address or authenticated user ID.
            // For simplicity, we'll use the IP address in this example.
            return httpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
