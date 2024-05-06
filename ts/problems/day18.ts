import { NEW_LINE } from "../inputReader.js";

const calculateLavaCubicMeters = (input: string) => navigateAndMeasureLava(input.split(NEW_LINE));

const navigateAndMeasureLava = (puzzle: string[]) => {
  let digger: Point = { x: 0, y: 0 };
  const totalCubes = puzzle.reduce((totalCubes, line) => {
    const [direction, stepsStr] = line.split(" ");
    const steps = Number(stepsStr);
    const destination = movePoint(digger, direction, steps);
    totalCubes += digger.x * destination.y - digger.y * destination.x + steps; // Gauss
    digger = destination;
    return totalCubes;
  }, 0);
  return totalCubes / 2 + 1;
};

const instructions: Record<string, (point: Point, steps: number) => Point> = {
  U: (point, steps) => ({ ...point, y: point.y - steps }),
  D: (point, steps) => ({ ...point, y: point.y + steps }),
  L: (point, steps) => ({ ...point, x: point.x - steps }),
  R: (point, steps) => ({ ...point, x: point.x + steps }),
};

const movePoint = (point: Point, direction: string, steps: number) =>
  direction in instructions ? instructions[direction](point, steps) : point;

type Point = {
  x: number;
  y: number;
};

export default calculateLavaCubicMeters;
