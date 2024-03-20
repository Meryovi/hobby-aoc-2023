Pre-requisites: .NET 8+

To run this project:

`dotnet test -l "console;verbosity=normal"`

To run Benchmarks:

`dotnet run`

You can also go to Program.cs and tweak the runner to run the benchmark for a specific problem.

**Goals:**
All projects regardless of programming language should follow a similar structure and approach.
My personal goal for the C# problems is to solve them using a coding style that minimizes heap allocations, but keeping a balance with readability and conciseness.

Since my goal is not to boast some impressive mathematical skills, the following problems were solved with help from the internet:
17, 20, 21, 22, 23, 25

Latest benchmark, ran on .NET 8, Windows 11, 13th Gen Intel Core i5 - 16 Cores, 16GB RAM, on input 1 of each problem, results as follows:

| Method          |          Mean |         Error |       StdDev |           Min |           Max |   Gen0 | Allocated |
| --------------- | ------------: | ------------: | -----------: | ------------: | ------------: | -----: | --------: |
| 'Day1 problem'  |      98.14 ns |     31.417 ns |     1.722 ns |      96.77 ns |     100.07 ns |      - |         - |
| 'Day2 problem'  |     539.98 ns |    508.785 ns |    27.888 ns |     515.92 ns |     570.54 ns |      - |         - |
| 'Day3 problem'  |     676.28 ns |    315.191 ns |    17.277 ns |     665.82 ns |     696.23 ns | 0.0381 |     240 B |
| 'Day4 problem'  |     466.99 ns |    491.688 ns |    26.951 ns |     436.32 ns |     486.87 ns |      - |         - |
| 'Day5 problem'  |   1,031.08 ns |    236.857 ns |    12.983 ns |   1,020.32 ns |   1,045.50 ns | 0.1278 |     808 B |
| 'Day6 problem'  |     164.23 ns |    106.640 ns |     5.845 ns |     158.85 ns |     170.45 ns |      - |         - |
| 'Day7 problem'  |     311.49 ns |    125.943 ns |     6.903 ns |     303.62 ns |     316.49 ns | 0.0315 |     200 B |
| 'Day8 problem'  |     229.23 ns |     60.614 ns |     3.322 ns |     225.82 ns |     232.45 ns | 0.0520 |     328 B |
| 'Day9 problem'  |     341.14 ns |    150.309 ns |     8.239 ns |     332.94 ns |     349.42 ns |      - |         - |
| 'Day10 problem' |     123.05 ns |    608.752 ns |    33.368 ns |      94.25 ns |     159.62 ns |      - |         - |
| 'Day11 problem' |     438.28 ns |    176.557 ns |     9.678 ns |     430.26 ns |     449.03 ns | 0.0305 |     192 B |
| 'Day12 problem' |     653.81 ns |    602.778 ns |    33.040 ns |     621.54 ns |     687.57 ns | 0.0439 |     280 B |
| 'Day13 problem' |     521.87 ns |    638.486 ns |    34.998 ns |     498.60 ns |     562.12 ns | 0.0534 |     336 B |
| 'Day14 problem' |     452.46 ns |    180.051 ns |     9.869 ns |     444.50 ns |     463.50 ns | 0.0381 |     240 B |
| 'Day15 problem' |      35.73 ns |      7.103 ns |     0.389 ns |      35.29 ns |      36.01 ns |      - |         - |
| 'Day16 problem' |     551.33 ns |    194.925 ns |    10.684 ns |     542.95 ns |     563.36 ns | 0.0610 |     384 B |
| 'Day17 problem' |      44.74 ns |     28.257 ns |     1.549 ns |      43.77 ns |      46.53 ns | 0.0293 |     184 B |
| 'Day18 problem' |     538.05 ns |    476.532 ns |    26.120 ns |     509.22 ns |     560.15 ns |      - |         - |
| 'Day19 problem' |   1,816.61 ns |    545.593 ns |    29.906 ns |   1,788.44 ns |   1,848.00 ns | 0.2460 |    1544 B |
| 'Day20 problem' | 149,962.87 ns | 83,620.592 ns | 4,583.527 ns | 147,267.97 ns | 155,255.18 ns |      - |    1048 B |
| 'Day21 problem' |  98,525.92 ns | 68,102.685 ns | 3,732.938 ns |  94,215.49 ns | 100,683.18 ns | 0.1221 |     920 B |
| 'Day22 problem' |   2,101.71 ns |  3,358.475 ns |   184.089 ns |   1,990.65 ns |   2,314.20 ns | 0.5951 |    3736 B |
| 'Day23 problem' |   8,558.24 ns |  5,574.310 ns |   305.547 ns |   8,314.04 ns |   8,900.87 ns | 0.5493 |    3488 B |
| 'Day24 problem' |   1,420.83 ns |    704.720 ns |    38.628 ns |   1,378.40 ns |   1,453.96 ns |      - |         - |
| 'Day25 problem' |  17,999.46 ns |  8,307.824 ns |   455.380 ns |  17,659.25 ns |  18,516.79 ns | 2.1057 |   13304 B |
