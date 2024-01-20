Pre-requisites: .NET 8+

To run this project:

`dotnet test -l "console;verbosity=normal"`

To run Benchmarks:

`dotnet run`

You can also go to Program.cs and tweak the runner to run the benchmark for a specific problem.

**Goals:**
All projects regardless of programming language should follow a similar structure and approach.
My personal goal for the C# problems is to solve them using a coding style that minimizes heap allocations, but keeping a balance with readability and conciseness.

Since my goal is not to boast some impressive mathematical skills, the following problems were solved with the help of the internet: 17, 20, 21, 22, 23, 25

Latest benchmark (ran on .NET 8, Windows 11, AMD Ryzen 7 5825U 8 cores, 16GB RAM)

| Type            | Method          |          Mean |         Error |       StdDev |           Min |           Max |   Gen0 | Allocated |
| --------------- | --------------- | ------------: | ------------: | -----------: | ------------: | ------------: | -----: | --------: |
| Day1Benchmarks  | 'Day1 problem'  |     158.41 ns |     49.452 ns |     2.711 ns |     155.29 ns |     160.16 ns |      - |         - |
| Day2Benchmarks  | 'Day2 problem'  |     726.47 ns |    207.270 ns |    11.361 ns |     713.36 ns |     733.40 ns |      - |         - |
| Day3Benchmarks  | 'Day3 problem'  |     777.10 ns |    304.040 ns |    16.665 ns |     758.87 ns |     791.55 ns | 0.0286 |     240 B |
| Day4Benchmarks  | 'Day4 problem'  |     579.40 ns |    745.799 ns |    40.880 ns |     545.61 ns |     624.84 ns |      - |         - |
| Day5Benchmarks  | 'Day5 problem'  |   1,303.29 ns |    107.317 ns |     5.882 ns |   1,297.42 ns |   1,309.19 ns | 0.0954 |     808 B |
| Day6Benchmarks  | 'Day6 problem'  |     219.66 ns |     72.537 ns |     3.976 ns |     217.03 ns |     224.23 ns |      - |         - |
| Day7Benchmarks  | 'Day7 problem'  |     412.56 ns |    129.166 ns |     7.080 ns |     407.39 ns |     420.63 ns | 0.0238 |     200 B |
| Day8Benchmarks  | 'Day8 problem'  |     361.13 ns |    368.712 ns |    20.210 ns |     343.89 ns |     383.37 ns | 0.0391 |     328 B |
| Day9Benchmarks  | 'Day9 problem'  |     492.91 ns |    164.095 ns |     8.995 ns |     482.81 ns |     500.06 ns |      - |         - |
| Day10Benchmarks | 'Day10 problem' |     127.86 ns |     30.932 ns |     1.695 ns |     126.88 ns |     129.82 ns |      - |         - |
| Day11Benchmarks | 'Day11 problem' |      544.3 ns |      49.83 ns |      2.73 ns |      541.4 ns |      546.9 ns | 0.0229 |     192 B |
| Day12Benchmarks | 'Day12 problem' |     530.48 ns |    199.630 ns |    10.942 ns |     520.10 ns |     541.91 ns | 0.0334 |     280 B |
| Day13Benchmarks | 'Day13 problem' |     775.69 ns |    363.676 ns |    19.934 ns |     759.66 ns |     798.01 ns | 0.0439 |     368 B |
| Day14Benchmarks | 'Day14 problem' |     580.95 ns |    116.418 ns |     6.381 ns |     573.83 ns |     586.15 ns | 0.0286 |     240 B |
| Day15Benchmarks | 'Day15 problem' |      42.44 ns |      3.479 ns |     0.191 ns |      42.25 ns |      42.63 ns |      - |         - |
| Day16Benchmarks | 'Day16 problem' |     762.81 ns |    194.163 ns |    10.643 ns |     752.97 ns |     774.11 ns | 0.0458 |     384 B |
| Day17Benchmarks | 'Day17 problem' |      64.42 ns |     71.213 ns |     3.903 ns |      60.49 ns |      68.30 ns | 0.0219 |     184 B |
| Day18Benchmarks | 'Day18 problem' |     764.78 ns |    736.416 ns |    40.365 ns |     732.20 ns |     809.93 ns |      - |         - |
| Day19Benchmarks | 'Day19 problem' |   2,494.62 ns |  1,032.136 ns |    56.575 ns |   2,431.08 ns |   2,539.52 ns | 0.1869 |    1576 B |
| Day20Benchmarks | 'Day20 problem' | 200,687.15 ns | 38,630.241 ns | 2,117.454 ns | 198,784.00 ns | 202,968.05 ns |      - |    1048 B |
| Day21Benchmarks | 'Day21 problem' | 118,370.56 ns | 35,304.717 ns | 1,935.171 ns | 116,274.15 ns | 120,088.60 ns |      - |     920 B |
| Day22Benchmarks | 'Day22 problem' |   2,919.15 ns |  1,071.649 ns |    58.741 ns |   2,851.37 ns |   2,955.29 ns | 0.4463 |    3736 B |
| Day23Benchmarks | 'Day23 problem' |  10,544.46 ns |  5,839.414 ns |   320.078 ns |  10,258.08 ns |  10,889.99 ns | 0.4578 |    3856 B |
| Day24Benchmarks | 'Day24 problem' |   1,992.63 ns |    691.898 ns |    37.925 ns |   1,961.99 ns |   2,035.04 ns |      - |         - |
| Day25Benchmarks | 'Day25 problem' |  23,030.64 ns |  4,844.720 ns |   265.555 ns |  22,783.44 ns |  23,311.36 ns | 1.5869 |   13304 B |
