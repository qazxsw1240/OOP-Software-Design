﻿using System;

namespace Zoo
{
    public static class Program
    {
        private static void Main()
        {
            Application application = new(Console.Out, Console.In, [
                new AnimalPrintAction(),
                new AnimalAddAction()
            ]);
            application.Start();
        }
    }
}
