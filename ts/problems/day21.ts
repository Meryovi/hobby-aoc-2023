import { NEW_LINE } from "../inputReader.js";

const countNumberOfPlotsInSteps = (input: string) => countNumberOfPlots(input.split(NEW_LINE));

const countNumberOfPlots = (lines: string[]) => {
  let nextVisits: Point[] = [];
  let lastVisits: Point[] = [{ x: Math.floor(lines.length / 2), y: Math.floor(lines.length / 2) }];

  for (let step = 0; step < 64; step++) {
    for (const visit of lastVisits) {
      for (const direction of NAVIGABLE_DIRECTIONS) {
        const next = movePoint(visit, direction);
        if (isOffset(next, lines.length)) continue;

        const tile = lines[next.y][next.x];
        if (tile != "#" && !nextVisits.some((p) => p.x === next.x && p.y === next.y)) nextVisits.push(next);
      }
    }
    lastVisits = nextVisits;
    nextVisits = [];
  }

  return lastVisits.length;
};

const movePoint = (point: Point, direction: Direction): Point => {
  if (direction == Direction.Up) return { x: point.x, y: point.y - 1 };
  if (direction == Direction.Right) return { x: point.x + 1, y: point.y };
  if (direction == Direction.Down) return { x: point.x, y: point.y + 1 };
  if (direction == Direction.Left) return { x: point.x - 1, y: point.y };
  return point;
};

const isOffset = (point: Point, size: number) => point.x >= size - 1 || point.x < 0 || point.y >= size - 1 || point.y < 0;

enum Direction {
  Up = 0,
  Right = 1,
  Down = 2,
  Left = 3,
}

const NAVIGABLE_DIRECTIONS = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];

type Point = {
  x: number;
  y: number;
};

export default countNumberOfPlotsInSteps;
