import { NEW_LINE } from "../inputReader.js";

const findLongestTrail = (input: string) => {
  var { start, end, grid } = parseGridPuzzle(input);

  const mapGraph = new MapGraph(start, end);
  mapGraph.buildGraph(grid);

  return mapGraph.getLongestTrail(start);
};

class MapGraph {
  private readonly graph: Map<string, Edge[]> = new Map();

  constructor(private readonly start: Point, private readonly end: Point) {}

  buildGraph(grid: string[][]) {
    const maxY = grid.length;
    const maxX = grid[0].length;

    const nodes = this.findAllNodes(grid, maxY, maxX);
    const queue: State[] = [];
    const next: Point[] = [];

    this.graph.clear();
    for (const node of nodes) {
      queue.length = 0;
      queue.push({ current: node, previous: node, steps: 0 });

      while (queue.length > 0) {
        const state = queue.shift()!;

        if (state.current !== node && nodes.some((n) => n.y === state.current.y && n.x === state.current.x)) {
          const key = node.getKey();
          if (!this.graph.has(key)) this.graph.set(key, []);
          this.graph.get(key)!.push({ to: state.current, length: state.steps });
          continue;
        }

        next.length = 0;
        for (const direction of Point.AllDirections) {
          const point = state.current.move(direction);

          if (point.equals(state.previous) || point.isOffset(maxY, maxX)) continue;
          if (grid[point.y][point.x] === "#") continue;

          if (grid[point.y][point.x] === ".") {
            next.push(point);
          } else if (
            (grid[point.y][point.x] === "v" && point.y - state.current.y > 0) ||
            (grid[point.y][point.x] === ">" && point.x - state.current.x > 0)
          ) {
            next.push(point);
          }
        }

        next.forEach((following) => queue.push({ current: following, previous: state.current, steps: state.steps + 1 }));
      }
    }
  }

  getLongestTrail(current: Point, length = 0) {
    const currentKey = current.getKey();
    if (currentKey === this.end.getKey()) {
      return length;
    }

    let max = Number.MIN_SAFE_INTEGER;
    for (const edge of this.graph.get(currentKey) || []) {
      max = Math.max(this.getLongestTrail(edge.to, length + edge.length), max);
    }
    return max;
  }

  private findAllNodes(grid: string[][], maxY: number, maxX: number) {
    const nodes: Point[] = [this.start, this.end];
    for (let j = 0; j < maxY; j++) {
      for (let i = 0; i < maxX; i++) {
        if (grid[j][i] === "#") continue;
        if (this.countNeighbors(grid, maxY, maxX, j, i) >= 3) nodes.push(new Point(j, i));
      }
    }
    return nodes;
  }

  private countNeighbors(grid: string[][], maxY: number, maxX: number, y: number, x: number) {
    let count = 0;
    for (const direction of Point.AllDirections) {
      const moved = direction.moveBy(y, x);
      if (!moved.isOffset(maxY, maxX) && grid[moved.y][moved.x] !== "#") count++;
    }
    return count;
  }
}

function parseGridPuzzle(input: string): { start: Point; end: Point; grid: string[][] } {
  const lineRanges = input.split(NEW_LINE);
  const height = lineRanges.length;
  const width = lineRanges[0].length;

  const grid: string[][] = Array.from({ length: height }, () => Array.from({ length: width }));
  let start = new Point(0, 0);
  let end = new Point(0, 0);

  for (let j = 0; j < height; j++) {
    for (let i = 0; i < width; i++) {
      grid[j][i] = lineRanges[j][i];
      start = j === 0 && grid[j][i] === "." ? start.moveBy(j, i) : start;
      end = j === height - 1 && grid[j][i] === "." ? end.moveBy(j, i) : end;
    }
  }

  return { start, end, grid };
}

class Point {
  static readonly AllDirections: Point[] = [new Point(0, -1), new Point(-1, 0), new Point(0, 1), new Point(1, 0)];

  constructor(public readonly y: number, public readonly x: number) {}

  move(direction: { y: number; x: number }) {
    return this.moveBy(direction.y, direction.x);
  }

  moveBy(y: number, x: number) {
    return new Point(this.y + y, this.x + x);
  }

  equals(other: { y: number; x: number }) {
    return this.y === other.y && this.x === other.x;
  }

  isOffset(maxY: number, maxX: number) {
    return this.y < 0 || this.x < 0 || this.y >= maxY || this.x >= maxX;
  }

  getKey() {
    return `${this.y},${this.x}`;
  }
}

interface Edge {
  to: Point;
  length: number;
}

interface State {
  current: Point;
  previous: Point;
  steps: number;
}

export default findLongestTrail;
