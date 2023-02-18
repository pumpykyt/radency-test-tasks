using Quartz;
using RadencyTestTasks.Task1.Global;

namespace RadencyTestTasks.Task1.Jobs;

public class MidnightJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var directory = context.JobDetail.JobDataMap.GetString("Directory");
        var subDirName = DateTime.UtcNow.ToString("MM-dd-yyyy");
        var newDirPath = Path.Combine(directory, subDirName);
        if (!Directory.Exists(newDirPath)) Directory.CreateDirectory(newDirPath);
        await File.WriteAllTextAsync(Path.Combine(newDirPath, "meta.log"), GlobalVariables.ToString());
    }
}