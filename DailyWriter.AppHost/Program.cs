var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DailyWriter_Web>("dailywriter-web");

builder.Build().Run();
