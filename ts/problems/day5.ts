import { NEW_LINE } from "../inputReader.js";

const findLowestSeedLocation = (input: string) => findLowestSeed(input.split(NEW_LINE));

const findLowestSeed = (lines: string[]) => {
  // Skip first two lines, then parse the ranges.
  const maps = lines.slice(2).reduce<RangeDiff[][]>((maps, line) => {
    if (line.endsWith(":")) maps.push([]); // Header (:) = new section.
    else if (!!line) maps[maps.length - 1].push(parseRangeDiff(line));
    return maps;
  }, []);

  const seeds = lines[0]
    .substring(lines[0].indexOf(":") + 2)
    .split(" ")
    .map(Number);

  const lowest = seeds.reduce((lowest, seed) => {
    const destination = maps.reduce((dest, ranges) => dest + getDiffForValueInRange(ranges, dest), seed);
    return Math.min(lowest, destination);
  }, Number.MAX_VALUE);

  return lowest;
};

const getDiffForValueInRange = (ranges: RangeDiff[], value: number) => {
  const range = ranges.find((r) => value >= r.start && value <= r.end);
  return range?.diff ?? 0;
};

const parseRangeDiff = (mapString: string): RangeDiff => {
  const [destination, source, range] = mapString.split(" ").map(Number);
  return { start: source, end: source + range - 1, diff: destination - source };
};

type RangeDiff = {
  start: number;
  end: number;
  diff: number;
};

export default findLowestSeedLocation;
