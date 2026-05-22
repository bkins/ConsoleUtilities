
using Spectre.Console;

namespace ConsoleUtilities.Spinners;

public static class SpinnerRunner
{
    public static async Task RunAsync( string                     message
                                     , Func<Action<string>, Task> action
                                     , Color?                     color         = null
                                     , string?                    initialStatus = null )
    {
        initialStatus ??= message;

        await AnsiConsole.Status()
                         .Spinner(new CaseSwappingSpinner(message))
                         .SpinnerStyle(color ?? Color.White)
                         .StartAsync(initialStatus
                                   , async ctx =>
                                     {
                                         void Report( string status )
                                         {
                                             ctx.Status(status);
                                         }

                                         await action(Report);
                                     });
    }

    public static async Task<T> RunAsync<T>( string                        message
                                           , Func<Action<string>, Task<T>> action
                                           , Color?                        color         = null
                                           , string?                       initialStatus = null )
    {
        initialStatus ??= message;

        return await AnsiConsole.Status()
                                .Spinner(new CaseSwappingSpinner(message))
                                .SpinnerStyle(color ?? Color.White)
                                .StartAsync(initialStatus
                                          , async ctx =>
                                            {
                                                void Report( string status )
                                                {
                                                    ctx.Status(status);
                                                }

                                                return await action(Report);
                                            });
    }
}