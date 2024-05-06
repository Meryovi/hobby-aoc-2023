import { NEW_LINE } from "../inputReader.js";
import { PriorityQueue, SerializedKeySet } from "../structures.js";

const calculateMinimumHeatLoss = (input: string) => traverseAnCalculateHeatLoss(input.split(NEW_LINE));

const traverseAnCalculateHeatLoss = (input: string[]) => {
  const partsFactory: Point = { x: input.length - 1, y: input.length - 1 };
  const queue = new PriorityQueue<Crucible>();
  const history = new SerializedKeySet<Crucible>(serializeCrucible);

  queue.enqueue({ location: { x: 0, y: 0 }, direction: Direction.Down, consecutive: 0 }, 0);
  queue.enqueue({ location: { x: 0, y: 0 }, direction: Direction.Left, consecutive: 0 }, 0);

  let minHeatLoss = Number.MAX_VALUE;

  let [_, crucible, heatLoss] = queue.dequeue();
  while (crucible) {
    if (equalPoints(crucible.location, partsFactory)) {
      minHeatLoss = heatLoss;
      break;
    }

    for (let direction of navigableDirections[crucible.direction]) {
      const pushed = tryMoveCrucible(crucible, direction, input.length, 3);
      if (pushed && history.add(pushed)) {
        const nextHeat = Number(input[pushed.location.y][pushed.location.x]);
        queue.enqueue(pushed, heatLoss + nextHeat);
      }
    }
    [_, crucible, heatLoss] = queue.dequeue();
  }

  return minHeatLoss;
};

const tryMoveCrucible = (crucible: Crucible, direction: Direction, size: number, maxConsecutive: number): Crucible | null => {
  var nextStep = movePoint(crucible.location, direction);

  if (nextStep.y < 0 || nextStep.x < 0 || nextStep.y >= size || nextStep.x >= size) return null;

  let nextConsecutive = direction == crucible.direction ? crucible.consecutive + 1 : 1;
  if (direction == crucible.direction && nextConsecutive > maxConsecutive) return null;

  return { location: nextStep, direction: direction, consecutive: nextConsecutive };
};

const movePoint = (point: Point, direction: Direction): Point => {
  if (direction === Direction.Up) return { ...point, y: point.y - 1 };
  if (direction === Direction.Down) return { ...point, y: point.y + 1 };
  if (direction === Direction.Left) return { ...point, x: point.x - 1 };
  if (direction === Direction.Right) return { ...point, x: point.x + 1 };
  return point;
};

const equalPoints = (a: Point, b: Point) => a.x === b.x && b.y === a.y;

const serializeCrucible = (crucible: Crucible | null) =>
  crucible ? `${crucible.location.x},${crucible.location.y},${crucible.direction},${crucible.consecutive}` : "";

enum Direction {
  Up,
  Down,
  Left,
  Right,
}

const navigableDirections = {
  [Direction.Up]: [Direction.Left, Direction.Right, Direction.Up],
  [Direction.Down]: [Direction.Left, Direction.Right, Direction.Down],
  [Direction.Left]: [Direction.Up, Direction.Down, Direction.Left],
  [Direction.Right]: [Direction.Up, Direction.Down, Direction.Right],
};

type Point = {
  x: number;
  y: number;
};

type Crucible = {
  location: Point;
  direction: Direction;
  consecutive: number;
};

export default calculateMinimumHeatLoss;
