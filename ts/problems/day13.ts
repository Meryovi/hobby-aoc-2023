import { NEW_LINE } from "../inputReader.js";

const sumOfReflectionValues = (input: string) =>
  input
    .split(NEW_LINE + NEW_LINE)
    .reduce(computePuzzles, [])
    .map(computeMirrorValue)
    .reduce((sum, val) => sum + val, 0);

const computePuzzles = (puzzles: string[][], nextPuzzle: string) => {
  puzzles.push(nextPuzzle.split(NEW_LINE));
  return puzzles;
};

const computeMirrorValue = (puzzle: string[]) => {
  const height = puzzle.length;
  const width = puzzle[0].length;
  return Math.max(getHorizontalMirrorValue(puzzle, height, width), getVerticalMirrorValue(puzzle, height, width));
};

const getHorizontalMirrorValue = (puzzle: string[], height: number, width: number) => {
  for (let i = 0; i < height - 1; i++) {
    for (let ji = i + 1, jd = i; ; jd--, ji++) {
      let reflections = 0;

      for (let x = 0; x < width; x++) {
        if (puzzle[jd][x] != puzzle[ji][x]) break;
        reflections++;
      }

      if (reflections != width) break;
      if (jd == 0 || ji == height - 1) return (i + 1) * 100;
    }
  }
  return -1;
};

const getVerticalMirrorValue = (puzzle: string[], height: number, width: number) => {
  for (let i = 0; i < width - 1; i++) {
    for (let ji = i + 1, jd = i; ; jd--, ji++) {
      let reflections = 0;

      for (let y = 0; y < height; y++) {
        if (puzzle[y][ji] != puzzle[y][jd]) break;
        reflections++;
      }

      if (reflections != height) break;
      if (jd == 0 || ji == width - 1) return i + 1;
    }
  }
  return -1;
};

export default sumOfReflectionValues;
