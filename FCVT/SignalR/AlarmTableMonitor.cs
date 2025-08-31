using FCVT.Models;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace FCVT.SignalR
{
    public class AlarmTableMonitor : BackgroundService
    {
        private readonly IHubContext<AlarmHub> _hubContext;
        private readonly IConfiguration _config;
        private SqlTableDependency<ViolationAlarms>? _dependency;

        public AlarmTableMonitor(IHubContext<AlarmHub> hubContext, IConfiguration config)
        {
            _hubContext = hubContext;
            _config = config;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🚀 AlarmTableMonitor started.");


            string connectionString = _config.GetConnectionString("GpsSignalR");

            // IMPORTANT: Use the actual table name, not a view
            _dependency = new SqlTableDependency<ViolationAlarms>(connectionString, "Violations");
            _dependency.OnChanged += Changed;
            _dependency.OnError += Error;
            _dependency.Start();

            return Task.CompletedTask;
        }

        private async void Changed(object? sender, RecordChangedEventArgs<ViolationAlarms> e)
        {
            Console.WriteLine($"🔥 Alarm Received");

            if (e.ChangeType == ChangeType.Insert)
            {
                var alarm = e.Entity;

                Console.WriteLine($"🔥 Alarm Received: {alarm.DeviceID} at {alarm.Violation}");

                await _hubContext.Clients.All.SendAsync("ReceiveAlarm", alarm);
            }
        }

        private void Error(object? sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"❌ TableDependency error: {e.Error.Message}");

        }

        public override void Dispose()
        {
            _dependency?.Stop();
            base.Dispose();
        }
    }
}
