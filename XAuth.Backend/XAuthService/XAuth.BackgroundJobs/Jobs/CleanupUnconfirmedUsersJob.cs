using Quartz;
using XAuth.Application.Interfaces;

namespace XAuth.BackgroundJobs.Jobs;

public class CleanupUnconfirmedUsersJob : IJob
{
    private readonly IUserRepository _userRepository;

    public async Task Execute(IJobExecutionContext context)
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-7);
        var unconfirmedUsers = await _userRepository.GetUnconfirmedUsersAsync(cutoffDate);

        foreach (var user in unconfirmedUsers)
        {
            await _userRepository.DeleteAsync(user);
        }
    }
}