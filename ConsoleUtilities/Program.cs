using ConsoleUtilities.Spinners;
using Spectre.Console;

Console.WriteLine("Spinner Test Harness");
Console.WriteLine("====================");
Console.WriteLine();

await BasicSpinnerDemo();
await ProgressUpdatesDemo();
await ReturnValueDemo();
await ExceptionDemo();

Console.WriteLine();
AnsiConsole.MarkupLine("[green]All demos completed.[/]");

return;

static async Task BasicSpinnerDemo()
{
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[yellow]Basic Spinner Demo[/]");

    await SpinnerRunner.RunAsync("Loading133./?"
                               , async _ =>
                                 {
                                     await Task.Delay(1500);
                                 });
}

static async Task ProgressUpdatesDemo()
{
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[yellow]Progress Update Demo[/]");

    await SpinnerRunner.RunAsync("Benchmark"
                               , async report =>
                                 {
                                     report("Loading benchmark files...");
                                     await Task.Delay(500);

                                     report("Running qwen2.5...");
                                     await Task.Delay(500);

                                     report("Running llama3...");
                                     await Task.Delay(500);

                                     report("Exporting results...");
                                     await Task.Delay(500);
                                 }
                               , Color.Cyan);
}

static async Task ReturnValueDemo()
{
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[yellow]Return Value Demo[/]");

    var result = await SpinnerRunner.RunAsync("Calculating"
                                            , async report =>
                                              {
                                                  report("Performing calculations...");
                                                  await Task.Delay(1500);

                                                  return 42;
                                              }
                                            , Color.Green);

    AnsiConsole.MarkupLine($"[green]Result:[/] {result}");
}

static async Task ExceptionDemo()
{
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine("[yellow]Exception Demo[/]");

    try
    {
        await SpinnerRunner.RunAsync("FailingOperation"
                                   , async report =>
                                     {
                                         report("Doing dangerous things...");
                                         await Task.Delay(1000);

                                         throw new InvalidOperationException(
                                             "Something exploded.");
                                     }
                                   , Color.Red);
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]Caught Exception:[/] {ex.Message}");
    }
}