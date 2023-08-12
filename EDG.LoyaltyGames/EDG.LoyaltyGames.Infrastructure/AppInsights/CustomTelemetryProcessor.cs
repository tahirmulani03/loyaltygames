using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace EDG.LoyaltyGames.Infrastructure.AppInsights
{
    public class CustomTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor _processor;

        public CustomTelemetryProcessor(ITelemetryProcessor telemetryProcessor)
        {
            _processor = telemetryProcessor ?? throw new ArgumentNullException(nameof(telemetryProcessor));
        }
        public void Process(ITelemetry item)
        {
            if (item is ISupportProperties telemetryWithProperties) {

                var context = telemetryWithProperties.Properties;
                var requestId = context.ContainsKey("Request-Id") ? context["Request-Id"] : null;
                if (!string.IsNullOrEmpty(requestId)) {
                    item.Context.Operation.Id = requestId;
                }
            }
            _processor.Process(item);
            
        }
    }
}
