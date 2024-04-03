import { NEW_LINE } from "../inputReader.js";

const calculateLavaCubicMeters = (input: string) => navigateAndMeasureLava(input.split(NEW_LINE));

const navigateAndMeasureLava = (puzzle: string[]) => {
  let digger: Point = { X: 0, Y: 0 };
  const totalCubes = puzzle.reduce((totalCubes, line) => {
    const [direction, stepsStr] = line.split(" ");
    const steps = Number(stepsStr);
    const destination = movePoint(digger, direction, steps);
    totalCubes += digger.X * destination.Y - digger.Y * destination.X + steps; // Gauss
    digger = destination;
    return totalCubes;
  }, 0);
  return totalCubes / 2 + 1;
};

const instructions: Record<string, (point: Point, steps: number) => Point> = {
  U: (point, steps) => ({ ...point, Y: point.Y - steps }),
  D: (point, steps) => ({ ...point, Y: point.Y + steps }),
  L: (point, steps) => ({ ...point, X: point.X - steps }),
  R: (point, steps) => ({ ...point, X: point.X + steps }),
};

const movePoint = (point: Point, direction: string, steps: number) =>
  direction in instructions ? instructions[direction](point, steps) : point;

type Point = {
  X: number;
  Y: number;
};

export default calculateLavaCubicMeters;
