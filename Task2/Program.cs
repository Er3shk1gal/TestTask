﻿using Task2;

var cts = new CancellationTokenSource();
var token = cts.Token;

for (int i = 0; i < 3; i++)
{
    Task.Run(() =>
    {
        while (!token.IsCancellationRequested)
        {
            Console.WriteLine($"Reader {Task.CurrentId}: summ = {Server.GetCount()}");
            Thread.Sleep(500);
        }
    }, token);
}

for (int i = 0; i < 2; i++)
{
    Task.Run(() =>
    {
        var random = new Random();
        while (!token.IsCancellationRequested)
        {
            int value = random.Next(1, 5);
            Server.AddToCount(value);
            Console.WriteLine($"Writer {Task.CurrentId}: added {value}");
            Thread.Sleep(1500);
        }
    }, token);
}

Console.ReadLine();
cts.Cancel();