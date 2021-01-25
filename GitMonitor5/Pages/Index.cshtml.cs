// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Index.cshtml.cs">(c) 2021 Mike Fourie and Contributors (https://github.com/mikefourie/GitMonitor) under MIT License. See https://opensource.org/licenses/MIT</copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace GitMonitor.Pages
{
    using System;
    using GitMonitor.Models;
    using GitMonitor.Repositories;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class IndexModel : PageModel
    {
        private readonly IOptions<MonitoredPathConfig> localMonitoredPathConfig;
        private readonly ILogger<CommitRepository> logger;

        public IndexModel(ILogger<CommitRepository> logger, IOptions<MonitoredPathConfig> monitoredPathConfig)
        {
            this.logger = logger;
            this.localMonitoredPathConfig = monitoredPathConfig;
        }

        public MonitoredPathConfig MPConfig { get; set; }

        public void OnGet(string monitoredPathName, string branchName, int days, DateTime startDateTime, DateTime endDateTime)
        {
            CommitRepository localRepository = new CommitRepository(this.logger);
            if (startDateTime == DateTime.MinValue)
            {
                this.MPConfig = localRepository.Get(this.localMonitoredPathConfig.Value, monitoredPathName, branchName, days);
                return;
            }

            this.MPConfig = localRepository.Get(this.localMonitoredPathConfig.Value, monitoredPathName, branchName, startDateTime, endDateTime);
        }
    }
}