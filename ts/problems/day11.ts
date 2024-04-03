import { NEW_LINE } from "../inputReader.js";

const sumDistanceBetweenGalaxies = (input: string) => sumGalaxiesDistances(expandUniverse(parseGalaxies(input.split(NEW_LINE))));

const parseGalaxies = (universeMap: string[]) => {
  // Find all galaxies (#) and map into their coords (X, Y).
  const galaxies: Point[] = universeMap.flatMap((galaxyRow, j) =>
    [...galaxyRow.matchAll(/#/g)].map((match) => ({ Y: j, X: match.index! }))
  );
  return { galaxies, universeSize: universeMap.length };
};

const expandUniverse = ({ galaxies, universeSize }: { galaxies: Point[]; universeSize: number }) => {
  const allCols = Array.from({ length: universeSize }).map((_, i) => universeSize - i);
  const emptyCols = allCols.filter((y) => galaxies.every((g) => g.Y !== y));
  const emptyRows = allCols.filter((x) => galaxies.every((g) => g.X !== x));

  galaxies.forEach((galaxy) => {
    emptyCols.forEach((col) => (galaxy.Y += col <= galaxy.Y ? 1 : 0));
    emptyRows.forEach((row) => (galaxy.X += row <= galaxy.X ? 1 : 0));
  });
  return galaxies;
};

const sumGalaxiesDistances = (galaxies: Point[]) => {
  let sumOfDistances = 0;
  for (let i = 0; i < galaxies.length - 1; i++) {
    for (let j = i + 1; j < galaxies.length; j++) {
      const distance = Math.abs(galaxies[j].X - galaxies[i].X) + Math.abs(galaxies[j].Y - galaxies[i].Y);
      sumOfDistances += distance;
    }
  }
  return sumOfDistances;
};

type Point = {
  X: number;
  Y: number;
};

export default sumDistanceBetweenGalaxies;
