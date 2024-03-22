import { NEW_LINE } from "../inputReader.js";

const sumAllFirstLastDigits = (input: string) =>
  input
    .split(NEW_LINE)
    .map(sumFirstLastDigits)
    .reduce((sum, val) => sum + val, 0);

const sumFirstLastDigits = (input: string) => {
  const digits = input
    .split("")
    .map(Number)
    .filter((num) => !isNaN(num));
  return digits[0] * 10 + digits.at(-1)!;
};

export const solve = sumAllFirstLastDigits;
