# Markov

## What is it?
Markov is an package for procedural generatrion based on [Markov algorithm](https://en.wikipedia.org/wiki/Markov_algorithm)

## Example

### Code
```Csharp
        public static MarkovSimulationTwoDim<byte> Labirynth()
        {
            var startMap = CreateMatrixOfType<byte>(21, 21, 1);
            startMap[6, 6] = 2;


            var simulation = new MarkovSimulationTwoDim<byte>(startMap);


            var sequence = new MarkovSequence<byte>();
            var stamp = new byte[,] { { (byte)2, (byte)3, (byte)2 } };
            var pattern = new IEquatable<byte>[,]
                { { (byte)2, (byte)1, (byte)1 } };
            sequence.Playables.Add(
                new RandomRule<byte>(new Pattern<byte>(pattern, RotationSettingsFlagsExtensions.All()), stamp));

            var sequence1 = new MarkovSequence<byte>();
            var stamp1 = new byte[,] { { (byte)2 } };
            var pattern1 = new IEquatable<byte>[,] { { (byte)3 } };
            sequence1.Playables.Add(new AllRule<byte>(new Pattern<byte>(pattern1, default), stamp1));

            var sequence2 = new CycleSequence<byte>(1);
            var stamp2 = new byte[,] { { (byte)4 } };
            var pattern2 = new IEquatable<byte>[,] { { (byte)1 } };
            sequence2.Playables.Add(new RandomRule<byte>(new Pattern<byte>(pattern2, default), stamp2));

            var sequence3 = new MarkovSequence<byte>();
            var stamp3 = new byte[,] { { (byte)4, (byte)4, } };
            var pattern3 = new IEquatable<byte>[,] { { (byte)4, (byte)1 } };
            sequence3.Playables.Add(
                new RandomRule<byte>(new Pattern<byte>(pattern3, RotationSettingsFlagsExtensions.All()), stamp3));

            var sequence4 = new CycleSequence<byte>(1);
            var stamp4 = new byte[,] { { (byte)4, (byte)4, (byte)4, } };
            var pattern4 = new IEquatable<byte>[,] { { (byte)4, (byte)2, (byte)1 } };
            sequence4.Playables.Add(
                new RandomRule<byte>(new Pattern<byte>(pattern4, RotationSettingsFlagsExtensions.All()), stamp4));

            var sequence5 = new MarkovSequence<byte>();
            sequence5.Playables.Add(sequence3);
            sequence5.Playables.Add(sequence4);

            var sequence6 = new MarkovSequence<byte>();
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
```

### Visualized result:

![image](https://user-images.githubusercontent.com/33464332/223000679-70b9c985-04ba-4927-abc0-a08e91ebedf3.png)


## Dependencies
1. Newtonsoft JSON

## Roadmap

1. Documentation
2. Third dimension 
