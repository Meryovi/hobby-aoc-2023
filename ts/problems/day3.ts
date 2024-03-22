import { NEW_LINE } from "../inputReader.js";

const extractPartNumber = (input: string) => reducePartNumber(input.split(NEW_LINE));

const reducePartNumber = (matrix: string[]) =>
  matrix.reduce((partNumber, row, j) => {
    const result = row
      .split("")
      .map(Number)
      .reduce(
        (result, value, i) => {
          if (!isNaN(value)) {
            result.accumulator = result.accumulator * 10 + value;
            result.adjacent ||= hasAdjacentSymbol(matrix, j, i);
          } else if (result.accumulator > 0) {
            result.partNumber += result.adjacent ? result.accumulator : 0;
            result.accumulator = 0;
            result.adjacent = false;
          }
          if (i === matrix.length - 1 && result.adjacent) result.partNumber += result.accumulator;
          return result;
        },
        { partNumber, adjacent: false, accumulator: 0 }
      );
    return result.partNumber;
  }, 0);

const hasAdjacentSymbol = (matrix: string[], col: number, row: number) => {
  const size = matrix.length;
  for (let y = Math.max(0, col - 1); y <= Math.min(size - 1, col + 1); y++)
    for (let x = Math.max(0, row - 1); x <= Math.min(size - 1, row + 1); x++)
      if ((x != row || y != col) && matrix[y][x] != "." && isNaN(Number(matrix[y][x]))) return true;
  return false;
};

export const solve = extractPartNumber;
