using MarkovTest.TwoDimension;
using MarkovTest.TwoDimension.Patterns;
using MarkovTest.TwoDimension.Rules;
using MarkovTest.TwoDimension.Sequences;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using MarkovTest.Serialization;

namespace MarkovTest
{
    public class Program
    {
        public static void Main()
        {
            LabirynthTest();
        }

        private static void LabirynthTest()
        {
            var simulation = SimulationFabric.Labirynth();
            var serializeObject = SimulationSerializer.SerializeSim(simulation);
            File.WriteAllText("C:\\Users\\Maks\\Desktop\\Sim.json", serializeObject);
            simulation = SimulationSerializer.DeserializeSim<byte>(serializeObject);
            simulation.OnSimulationChanged += x => PrintMatrix(x);
            simulation.Play(simulation);
        }


        private static void PatternRotationTest()
        {
            var patternMatrix = new IEquatable<byte>[,]
            {
                { (byte)1, (byte)2, (byte)3, (byte)3 },
                { (byte)1, (byte)2, (byte)3, (byte)3 },
                { (byte)4, (byte)5, (byte)6, (byte)6 },
                { (byte)4, (byte)5, (byte)6, (byte)6 },
            };
            var stamp = SimulationFabric.CreateMatrixOfType<byte>(2, 2, 2);
            var ruleTest =
                new AllRule<byte>(
                    new Pattern<byte>(patternMatrix, RotationSettingsFlags.FlipY | RotationSettingsFlags.FlipX), stamp);
            foreach (var rule in ruleTest.MainPattern.Patterns)
            {
                Console.WriteLine(rule.Key);
                PrintMatrix(rule.Value);
                Console.WriteLine();
            }
        }

        private static void PrintMatrix(Array arr, bool readKey = false)
        {
            //Console.ReadKey();
            var colorMap = new Dictionary<byte, ConsoleColor>()
            {
                { 0, ConsoleColor.Black },
                { 1, ConsoleColor.Blue },
                { 2, ConsoleColor.Yellow },
                { 3, ConsoleColor.Red },
                { 4, ConsoleColor.Magenta },
                { 5, ConsoleColor.Cyan },
                { 6, ConsoleColor.DarkCyan },
                { 7, ConsoleColor.Green },
                { 8, ConsoleColor.Gray },
                { 9, ConsoleColor.White },
            };

            for (var x = 0; x < arr.GetLength(0); x++)
            {
                for (var y = 0; y < arr.GetLength(1); y++)
                {
                    Console.BackgroundColor = colorMap[(byte)arr.GetValue(x, y)!];
                    Console.Write($"  ");
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.ResetColor();
            if (readKey)
                Console.ReadKey();
            Console.WriteLine();
        }
    }
}