using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var random = new Random();
        var names = new List<string> { "Alice", "Bob", "Charlie", "Diana", "Edward" };

        var nameList = new List<string>();
        var numberList = new List<int>();

        var sensorStates = new List<string> { "Red", "Green", "White" };
        var currentSensorState = "Red";

        var leftPanel = new Panel("Waiting for name...")
            .Header("Left Column")
            .BorderColor(Color.Green)
            .Expand();

        var rightPanel = new Panel("Waiting for number...")
            .Header("Right Column")
            .BorderColor(Color.Blue)
            .Expand();

        var sensorPanel = new Panel("Sensor Status")
            .Header("Sensor Column")
            .BorderColor(Color.Cyan1)
            .Expand();

        var grid = new Grid();
        
        grid.AddColumn(new GridColumn().PadRight(1));
        grid.AddColumn(new GridColumn().PadLeft(1));
        grid.AddColumn(new GridColumn().PadLeft(1));
        grid.AddRow(leftPanel, rightPanel, sensorPanel);

        await AnsiConsole.Live(grid).StartAsync(async ctx =>
        {
            while (true)
            {
                await Task.Delay(1000);

                // Add a random number to the list every second
                int randomNumber = random.Next(1000);
                numberList.Add(randomNumber);

                // Add a random name to the list every three seconds
                if (DateTime.Now.Second % 3 == 0)
                {
                    string randomName = names[random.Next(names.Count)];
                    nameList.Add(randomName);
                }

                // Change sensor state every second
                currentSensorState = sensorStates[random.Next(sensorStates.Count)];

                // Update panels with the accumulated values
                leftPanel = new Panel(string.Join("\n", nameList))
                    .Header("Left Column")
                    .BorderColor(Color.Green)
                    .Expand();

                rightPanel = new Panel(string.Join("\n", numberList))
                    .Header("Right Column")
                    .BorderColor(Color.Blue)
                    .Expand();

                sensorPanel = new Panel($"Current Sensor State: {currentSensorState}")
                    .Header("Sensor Column")
                    .BorderColor(Color.Cyan1)
                    .Expand();

                grid = new Grid();
                grid.AddColumn(new GridColumn().PadRight(1));
                grid.AddColumn(new GridColumn().PadLeft(1));
                grid.AddColumn(new GridColumn().PadLeft(1));
                grid.AddRow(leftPanel, rightPanel, sensorPanel);

                ctx.UpdateTarget(grid);
            }
        });
    }
}