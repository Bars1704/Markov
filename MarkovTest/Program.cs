using System.Text.Json;
using MarkovTest.TwoDimension;
using MarkovTest.TwoDimension.Patterns;
using MarkovTest.TwoDimension.Rules;
using MarkovTest.TwoDimension.Sequences;
using Newtonsoft.Json;

namespace MarkovTest
{
    public class Program
    {
        public static void Main()
        {
            // PatternRotationTest();
            // MarginBoxes();
            // MarchingSquares();
            LabirynthTest();
        }

        private static void LabirynthTest()
        {
            var startMap = CreateMatrixOfType<byte>(51, 51, 1);
            startMap[6, 6] = 2;


            var simulation = new MarkovSimulationTwoDim<byte>(startMap);

            simulation.OnSimulationChanged += x => PrintMatrix(x);

            var sequence = new MarkovSequence<byte>();
            var stamp = new byte[,] { { (byte)2, (byte)3, (byte)2 } };
            var pattern = new IEquatable<byte>[,] { { (byte)2, (byte)1, (byte)1 } }; //!!!!!! Я ОТРЕДАЧИЛ ТУТ НЕ ЗАБУДЬ!!!!!
            //var pattern = new IEquatable<byte>[,] { { (byte)1, (byte)2, (byte)3 } ,{ (byte)4, (byte)5, (byte)6 } }; //!!!!!! Я ОТРЕДАЧИЛ ТУТ НЕ ЗАБУДЬ!!!!!
            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern), stamp,
                RotationSettingsFlagsExtensions.All()));

            var sequence1 = new MarkovSequence<byte>();
            var stamp1 = new byte[,] { { (byte)2 } };
            var pattern1 = new IEquatable<byte>[,] { { (byte)3 } };
            sequence1.Playables.Add(new AllRule<byte>(new Pattern<byte>(pattern1), stamp1, default));

            var sequence2 = new CycleSequence<byte>(1);
            var stamp2 = new byte[,] { { (byte)4 } };
            var pattern2 = new IEquatable<byte>[,] { { (byte)1 } };
            sequence2.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern2), stamp2, default));

            var sequence3 = new MarkovSequence<byte>();
            var stamp3 = new byte[,] { { (byte)4, (byte)4, } };
            var pattern3 = new IEquatable<byte>[,] { { (byte)4, (byte)1 } };
            sequence3.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern3), stamp3,
                RotationSettingsFlagsExtensions.All()));

            var sequence4 = new CycleSequence<byte>(1);
            var stamp4 = new byte[,] { { (byte)4, (byte)4, (byte)4, } };
            var pattern4 = new IEquatable<byte>[,] { { (byte)4, (byte)2, (byte)1 } };
            sequence4.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern4), stamp4,
                RotationSettingsFlagsExtensions.All()));

            var sequence5 = new MarkovSequence<byte>();
            sequence5.Playables.Add(sequence3);
            sequence5.Playables.Add(sequence4);

            var sequence6 = new MarkovSequence<byte>();
            var stamp6 = new byte[,] { { (byte)1 } };
            var pattern6 = new IEquatable<byte>[,] { { (byte)4 } };
            sequence6.Playables.Add(new AllRule<byte>(new Pattern<byte>(pattern6), stamp6, default));

            simulation.Playables.Add(sequence);
            simulation.Playables.Add(sequence1);
            simulation.Playables.Add(sequence2);
            simulation.Playables.Add(sequence5);
            simulation.Playables.Add(sequence6);
            //simulation.Play(simulation);

            Console.Clear();
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var serializeObject = JsonConvert.SerializeObject(simulation, Formatting.Indented, settings);
            File.WriteAllText("C:\\Users\\Maks\\Desktop\\Sim.json", serializeObject);

            simulation = JsonConvert.DeserializeObject<MarkovSimulationTwoDim<byte>>(serializeObject, settings);
            simulation.OnSimulationChanged += x => PrintMatrix(x);
            simulation.Play(simulation);
        }

        private static void MarchingSquares()
        {
            var startMap = CreateMatrixOfType<byte>(10, 10, 1);
            var simulation = new MarkovSimulationTwoDim<byte>(startMap);
            simulation.OnSimulationChanged += x => PrintMatrix(x);

            var sequence = new CycleSequence<byte>(50);

            var stamp = new byte[,]
            {
                { 0, 2 },
                { 2, 2 },
                { 0, 2 },
            };

            var pattern = new IEquatable<byte>[,]
            {
                { (byte)0, (byte)1 },
                { (byte)1, (byte)1 },
                { (byte)0, (byte)1 },
            };

            var rule = new RandomRule<byte>(new Pattern<byte>(pattern), stamp, RotationSettingsFlags.Rotate);

            sequence.Playables.Add(rule);

            sequence.Play(simulation);
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
            var stamp = CreateMatrixOfType<byte>(2, 2, 2);
            var ruleTest =
                new AllRule<byte>(new Pattern<byte>(patternMatrix), stamp,
                    RotationSettingsFlags.FlipY | RotationSettingsFlags.FlipX);
            foreach (var rule in ruleTest.Patterns)
            {
                Console.WriteLine(rule.Key);
                PrintMatrix(rule.Value.PatternForm);
                Console.WriteLine();
            }
        }

        private static void MarginBoxes()
        {
            var startMap = CreateMatrixOfType<byte>(5, 5, 1);
            var simulation = new MarkovSimulationTwoDim<byte>(startMap);
            simulation.OnSimulationChanged += x => PrintMatrix(x);

            var sequence = new CycleSequence<byte>(1);

            byte b = 0;

            b++;
            var stamp = CreateMatrixOfType<byte>(4, 4, 2);
            var pattern = new IEquatable<byte>[,]
            {
                { b, b, b, b },
                { b, b, b, b },
                { b, b, b, b },
                { b, b, b, b }
            };

            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern), stamp, default));

            b++;
            var stamp1 = CreateMatrixOfType<byte>(3, 3, 3);
            var pattern1 = new IEquatable<byte>[,]
            {
                { b, b, b },
                { b, b, b },
                { b, b, b }
            };
            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern1), stamp1, default));

            b++;
            var stamp2 = CreateMatrixOfType<byte>(2, 2, 4);
            var pattern2 = new IEquatable<byte>[,]
            {
                { b, b },
                { b, b }
            };
            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern2), stamp2, default));

            b++;
            var stamp3 = CreateMatrixOfType<byte>(1, 1, 5);
            var pattern3 = new IEquatable<byte>[,] { { b } };
            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern3), stamp3, default));

            sequence.Reset();
            sequence.Playables.ForEach(x => x.Play(simulation));
        }

        private static T[,] CreateMatrixOfType<T>(int xSize, int ySize, T fillWith)
        {
            var result = new T[xSize, ySize];
            for (var x = 0; x < xSize; x++)
            for (var y = 0; y < ySize; y++)
                result[x, y] = fillWith;

            return result;
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