using System;
using System.Collections.Generic;
using System.IO;
using Markov.MarkovTest.Sequences;
using Markov.MarkovTest.Serialization;
using Markov.MarkovTest.TwoDimension;
using Markov.MarkovTest.TwoDimension.Patterns;
using Markov.MarkovTest.TwoDimension.Rules;

namespace Markov.MarkovTest
{
    public class Program
    {
        public static void Main()
        {

            LabirynthTest();
            
            return;
            
            var startMap = SimulationFabric.CreateMatrixOfType<byte>(9, 9, 1);
            startMap[3, 3] = 2;


            var simulation = new MarkovSimulation<byte>(startMap);


            var sequence = new MarkovSequence<byte, MarkovSimulation<byte>>();
            var stamp = new byte[,] { { (byte)2, (byte)3, (byte)2 } };
            var pattern = new IEquatable<byte>[,]
                { { (byte)2, (byte)1, (byte)1 } };
            sequence.Playables.Add(
                new RandomRule<byte>(new Pattern<byte>(pattern, RotationSettingsFlags.Rotate), stamp));
            
            simulation.Playables.Add(sequence);
            simulation.OnSimulationChanged += x => PrintMatrix(x);
            simulation.Play(2);
        }

        private static void LabirynthTest()
        {
            var simulation = SimulationFabric.Labirynth(40);
            var serializeObject = SimulationSerializer.SerializeSim(simulation);
            File.WriteAllText("C:\\Users\\Maks\\Desktop\\Sim.json", serializeObject);
            simulation = SimulationSerializer.DeserializeSim2D(serializeObject);
            //simulation.OnSimulationChanged += x => PrintMatrix(x);
            simulation.Play();
            PrintMatrix(simulation.Simulation);
        }
        
        private static void PrintMatrix(Array arr, bool readKey = false)
        {
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