import { NEW_LINE } from "../inputReader.js";

const countEnergizedLightTiles = (input: string) => startBeamAndCountTiles(input.split(NEW_LINE));

const startBeamAndCountTiles = (matrix: string[]) => {
  const history: number[][] = Array.from(matrix).map(() => new Array(matrix[0].length).fill(0));
  const beam: FacingPoint = { X: 0, Y: 0, Direction: Direction.Right };

  startLightBeam(beam, matrix, history);

  // By this point history will be complete. Count all items > 0.
  return history.flat().reduce((tileCount, cell) => tileCount + Number(cell > 0), 0);
};

const startLightBeam = (beam: FacingPoint, matrix: string[], history: number[][]) => {
  const height = matrix.length;
  const width = matrix[0].length;

  while (!isOffsetBeam(beam, width, height)) {
    const visits = history[beam.Y][beam.X];
    if (visits & beam.Direction) break;

    history[beam.Y][beam.X] |= beam.Direction;

    const instruction = matrix[beam.Y][beam.X];
    const navigation = `${instruction},${beam.Direction}`;

    if (navigation === "|,1" || navigation === "|,2") {
      startLightBeam(splitLightBeam(beam, Direction.Up), matrix, history);
    } else if (navigation === "-,4" || navigation === "-,8") {
      startLightBeam(splitLightBeam(beam, Direction.Right), matrix, history);
    } else if (navigation in beamInstructionMap) {
      beamInstructionMap[navigation](beam);
    } else {
      beamInstructionMap.wildcard(beam);
    }
  }
};

const moveLightBeam = (lightBeam: FacingPoint, direction: Direction) => navigationMap[direction](lightBeam);

const splitLightBeam = (lightBeam: FacingPoint, direction: Direction) => {
  const newBeam = moveLightBeam({ ...lightBeam }, direction);
  moveLightBeam(lightBeam, oppositeDirectionMap[direction]);
  return newBeam;
};

const isOffsetBeam = (beam: FacingPoint, width: number, height: number) =>
  beam.X < 0 || beam.Y < 0 || beam.X >= width || beam.Y >= height;

enum Direction {
  Right = 1,
  Left = 2,
  Up = 4,
  Down = 8,
}

const navigationMap: Record<Direction, (point: FacingPoint) => FacingPoint> = {
  [Direction.Up]: (point: FacingPoint) => (point.Y--, (point.Direction = Direction.Up), point),
  [Direction.Down]: (point: FacingPoint) => (point.Y++, (point.Direction = Direction.Down), point),
  [Direction.Left]: (point: FacingPoint) => (point.X--, (point.Direction = Direction.Left), point),
  [Direction.Right]: (point: FacingPoint) => (point.X++, (point.Direction = Direction.Right), point),
};

const oppositeDirectionMap = {
  [Direction.Up]: Direction.Down,
  [Direction.Down]: Direction.Up,
  [Direction.Left]: Direction.Right,
  [Direction.Right]: Direction.Left,
};

const beamInstructionMap: Record<string, (beam: FacingPoint) => void> = {
  "/,1": (beam) => moveLightBeam(beam, Direction.Up),
  "/,2": (beam) => moveLightBeam(beam, Direction.Down),
  "/,4": (beam) => moveLightBeam(beam, Direction.Right),
  "/,8": (beam) => moveLightBeam(beam, Direction.Left),
  "\\,1": (beam) => moveLightBeam(beam, Direction.Down),
  "\\,2": (beam) => moveLightBeam(beam, Direction.Up),
  "\\,4": (beam) => moveLightBeam(beam, Direction.Left),
  "\\,8": (beam) => moveLightBeam(beam, Direction.Right),
  wildcard: (beam) => moveLightBeam(beam, beam.Direction),
};

type FacingPoint = {
  X: number;
  Y: number;
  Direction: Direction;
};

export default countEnergizedLightTiles;
