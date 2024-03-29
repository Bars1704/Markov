using System;
using Markov.MarkovTest.Sequences;
using Markov.MarkovTest.TwoDimension;
using Markov.MarkovTest.TwoDimension.Patterns;
using Markov.MarkovTest.TwoDimension.Rules;

namespace Markov.MarkovTest
{
    public class SimulationFabric
    {
        public static MarkovSimulation<byte> MarginBoxes()
        {
            var startMap = CreateMatrixOfType<byte>(5, 5, 1);
            var simulation = new MarkovSimulation<byte>(startMap);

            var sequence = new CycleSequence<byte, MarkovSimulation<byte>>(1);

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

            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern, default), stamp));

            b++;
            var stamp1 = CreateMatrixOfType<byte>(3, 3, 3);
            var pattern1 = new IEquatable<byte>[,]
            {
                { b, b, b },
                { b, b, b },
                { b, b, b }
            };
            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern1, default), stamp1));

            b++;
            var stamp2 = CreateMatrixOfType<byte>(2, 2, 4);
            var pattern2 = new IEquatable<byte>[,]
            {
                { b, b },
                { b, b }
            };
            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern2, default), stamp2));

            b++;
            var stamp3 = SimulationFabric.CreateMatrixOfType<byte>(1, 1, 5);
            var pattern3 = new IEquatable<byte>[,] { { b } };
            sequence.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern3, default), stamp3));
            return simulation;
        }

        public static MarkovSimulation<byte> Labirynth(int size = 21)
        {
            var startMap = CreateMatrixOfType<byte>(size, size, 1);
            startMap[size/2, size/2] = 2;


            var simulation = new MarkovSimulation<byte>(startMap);


            var sequence = new MarkovSequence<byte, MarkovSimulation<byte>>();
            var stamp = new byte[,] { { (byte)2, (byte)3, (byte)2 } };
            var pattern = new IEquatable<byte>[,]
                { { (byte)2, (byte)1, (byte)1 } };
            sequence.Playables.Add(
                new RandomRule<byte>(new Pattern<byte>(pattern, RotationSettingsFlags.Rotate), stamp));

            var sequence1 = new MarkovSequence<byte, MarkovSimulation<byte>>();
            var stamp1 = new byte[,] { { (byte)2 } };
            var pattern1 = new IEquatable<byte>[,] { { (byte)3 } };
            sequence1.Playables.Add(new AllRule<byte>(new Pattern<byte>(pattern1, default), stamp1));

            var sequence2 = new CycleSequence<byte, MarkovSimulation<byte>>(1);
            var stamp2 = new byte[,] { { (byte)4 } };
            var pattern2 = new IEquatable<byte>[,] { { (byte)1 } };
            sequence2.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern2, default), stamp2));

            var sequence3 = new MarkovSequence<byte, MarkovSimulation<byte>>();
            var stamp3 = new byte[,] { { (byte)4, (byte)4, } };
            var pattern3 = new IEquatable<byte>[,] { { (byte)4, (byte)1 } };
            sequence3.Playables.Add(
                new RandomRule<byte>(new Pattern<byte>(pattern3, RotationSettingsFlags.Rotate), stamp3));

            var sequence4 = new CycleSequence<byte, MarkovSimulation<byte>>(1);
            var stamp4 = new byte[,] { { (byte)4, (byte)4, (byte)4, } };
            var pattern4 = new IEquatable<byte>[,] { { (byte)4, (byte)2, (byte)1 } };
            sequence4.Playables.Add(
                new RandomRule<byte>(new Pattern<byte>(pattern4,RotationSettingsFlags.Rotate), stamp4));
            
            var sequence5 = new MarkovSequence<byte, MarkovSimulation<byte>>();
            sequence5.Playables.Add(sequence3);
            sequence5.Playables.Add(sequence4);

            var sequence6 = new MarkovSequence<byte, MarkovSimulation<byte>>();
            var stamp6 = new byte[,] { { (byte)1 } };
            var pattern6 = new IEquatable<byte>[,] { { (byte)4 } };
            sequence6.Playables.Add(new AllRule<byte>(new Pattern<byte>(pattern6, default), stamp6));

            
            simulation.Playables.Add(sequence);
            simulation.Playables.Add(sequence1);
            simulation.Playables.Add(sequence2);
            simulation.Playables.Add(sequence5);
            simulation.Playables.Add(sequence6);
            return simulation;
        }

        public static T[,] CreateMatrixOfType<T>(int xSize, int ySize, T fillWith)
        {
            var result = new T[xSize, ySize];
            for (var x = 0; x < xSize; x++)
            for (var y = 0; y < ySize; y++)
                result[x, y] = fillWith;

            return result;
        }

        private static CycleSequence<byte, MarkovSimulation<byte>> MarchingSquares()
        {
            var startMap = CreateMatrixOfType<byte>(10, 10, 1);
            var simulation = new MarkovSimulation<byte>(startMap);

            var sequence = new CycleSequence<byte, MarkovSimulation<byte>>(50);

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

            var rule = new RandomRule<byte>(new Pattern<byte>(pattern, RotationSettingsFlags.Rotate), stamp);

            sequence.Playables.Add(rule);
            return sequence;
        }
    }
}