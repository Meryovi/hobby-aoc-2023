import { NEW_LINE } from "../inputReader.js";

const countHailIntersections = (input: string) => {
  const [firstLine, ...hailStrings] = input.split(NEW_LINE);
  const [minTime, maxTime] = firstLine.split(" ").map(Number);

  return hailStrings
    .map(parseHailstone)
    .reduce((acc, _, i, hailStones) => acc + countNextIntersections(hailStones, i, minTime, maxTime), 0);
};

const countNextIntersections = (hailStones: Hailstone[], from: number, minTime: number, maxTime: number) =>
  hailStones
    .slice(from + 1)
    .reduce((acc, curr) => acc + Number(hailstonesIntersect(hailStones[from], curr, minTime, maxTime)), 0);

const parseHailstone = (hailString: string): Hailstone => {
  const [positions, velocities] = hailString.split(" @ ");
  const [x, y] = positions.split(", ").map(Number);
  const [vx, vy] = velocities.split(", ").map(Number);

  return { position: { x, y }, velocity: { vx, vy } };
};

const hailstonesIntersect = (first: Hailstone, second: Hailstone, minTime: number, maxTime: number) => {
  if (first === second) return false; // Can't intersect themselves.

  // Intersection of dimensional lines formula, found it somewhere else.
  const determinant = first.velocity.vx * second.velocity.vy - first.velocity.vy * second.velocity.vx;

  if (determinant == 0) return false;

  const t1 = first.velocity.vx * first.position.y - first.velocity.vy * first.position.x;
  const t2 = second.velocity.vx * second.position.y - second.velocity.vy * second.position.x;

  const xIntersect = (second.velocity.vx * t1 - first.velocity.vx * t2) / determinant;
  const yIntersect = (second.velocity.vy * t1 - first.velocity.vy * t2) / determinant;

  if (xIntersect < minTime || xIntersect > maxTime || yIntersect < minTime || yIntersect > maxTime) return false;

  const thisIntersect = Math.sign(xIntersect - first.position.x) != Math.sign(first.velocity.vx);
  const otherIntersect = Math.sign(xIntersect - second.position.x) != Math.sign(second.velocity.vx);

  if (thisIntersect || otherIntersect) return false;

  return true;
};

type Hailstone = { position: Point; velocity: Velocity };

type Point = { x: number; y: number };

type Velocity = { vx: number; vy: number };

export default countHailIntersections;
