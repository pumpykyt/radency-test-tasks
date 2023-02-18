using Newtonsoft.Json;
using Quartz;
using RadencyTestTasks.Task1.Global;
using RadencyTestTasks.Task1.Helpers;

namespace RadencyTestTasks.Task1.Jobs;

public class MidnightJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var data = context.JobDetail.JobDataMap.GetString("Directory");
        Console.WriteLine(data);
        //var subDirName = DateTime.UtcNow.ToString("MM-dd-yyyy");
        //var newDirPath = Path.Combine(data.Directory, subDirName);
        //if (!Directory.Exists(newDirPath)) Directory.CreateDirectory(newDirPath);
        //await File.WriteAllTextAsync(Path.Combine(newDirPath, "meta.log"), GlobalVariables.ToString());
    }
}