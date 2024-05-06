import { NEW_LINE } from "../inputReader.js";

const findFarthestTileFromStart = (input: string) => findFarthestTile(input.split(NEW_LINE));

const findFarthestTile = (matrix: string[]) => {
  const startingPoint = matrix.reduce((point, row, j) => {
    const i = row.indexOf("S");
    return i > -1 ? new Point(i, j) : point;
  }, new Point(0, 0));

  let totalSteps = 0;
  let direction = Direction.Right;
  let currentPoint = new Point(startingPoint.x + 1, startingPoint.y);

  while (!currentPoint.equals(startingPoint)) {
    const pipe = matrix[currentPoint.y][currentPoint.x];
    [currentPoint, direction] = moveBasedOnChar(pipe, currentPoint, direction);
    totalSteps++;
  }

  const midPoint = Math.ceil(totalSteps / 2.0);
  return midPoint;
};

enum Direction {
  Up,
  Down,
  Left,
  Right,
}

class Point {
  x: number;
  y: number;

  constructor(x: number, y: number) {
    this.x = x;
    this.y = y;
  }

  move(direction: Direction) {
    if (direction === Direction.Up) this.y--;
    else if (direction === Direction.Down) this.y++;
    else if (direction === Direction.Left) this.x--;
    else if (direction === Direction.Right) this.x++;
    return [this, direction] as const;
  }

  equals(other: Point) {
    return this.x === other.x && this.y === other.y;
  }
}

const moveBasedOnChar = (char: string, point: Point, direction: Direction): readonly [Point, Direction] => {
  switch (char) {
    case "|":
    case "-":
      return point.move(direction);
    case "L":
      if (direction === Direction.Down) return point.move(Direction.Right);
      if (direction === Direction.Left) return point.move(Direction.Up);
      break;
    case "J":
      if (direction === Direction.Down) return point.move(Direction.Left);
      if (direction === Direction.Right) return point.move(Direction.Up);
      break;
    case "7":
      if (direction === Direction.Up) return point.move(Direction.Left);
      if (direction === Direction.Right) return point.move(Direction.Down);
      break;
    case "F":
      if (direction === Direction.Up) return point.move(Direction.Right);
      if (direction === Direction.Left) return point.move(Direction.Down);
      break;
  }
  return [point, direction];
};

export default findFarthestTileFromStart;
