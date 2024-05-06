import { NEW_LINE } from "../inputReader.js";

const sumDistanceBetweenGalaxies = (input: string) => sumGalaxiesDistances(expandUniverse(parseGalaxies(input.split(NEW_LINE))));

const parseGalaxies = (universeMap: string[]) => {
  // Find all galaxies (#) and map into their coords (X, Y).
  const galaxies: Point[] = universeMap.flatMap((galaxyRow, j) =>
    [...galaxyRow.matchAll(/#/g)].map((match) => ({ y: j, x: match.index! }))
  );
  return { galaxies, universeSize: universeMap.length };
};

const expandUniverse = ({ galaxies, universeSize }: { galaxies: Point[]; universeSize: number }) => {
  const allCols = Array.from({ length: universeSize }).map((_, i) => universeSize - i);
  const emptyCols = allCols.filter((y) => galaxies.every((g) => g.y !== y));
  const emptyRows = allCols.filter((x) => galaxies.every((g) => g.x !== x));

  galaxies.forEach((galaxy) => {
    emptyCols.forEach((col) => (galaxy.y += col <= galaxy.y ? 1 : 0));
    emptyRows.forEach((row) => (galaxy.x += row <= galaxy.x ? 1 : 0));
  });
  return galaxies;
};

const sumGalaxiesDistances = (galaxies: Point[]) => {
  let sumOfDistances = 0;
  for (let i = 0; i < galaxies.length - 1; i++) {
    for (let j = i + 1; j < galaxies.length; j++) {
      const distance = Math.abs(galaxies[j].x - galaxies[i].x) + Math.abs(galaxies[j].y - galaxies[i].y);
      sumOfDistances += distance;
    }
  }
  return sumOfDistances;
};

type Point = {
  x: number;
  y: number;
};

export default sumDistanceBetweenGalaxies;
