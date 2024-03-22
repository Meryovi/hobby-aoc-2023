import { NEW_LINE } from "../inputReader.js";

const numberOfWaysToWin = (input: string) => calculateNumberOfWaysToWin(input.split(NEW_LINE));

const calculateNumberOfWaysToWin = ([timesString, distanceString]: string[]) => {
  const times = safelyParseNumbers(timesString);
  const distances = safelyParseNumbers(distanceString);

  const waysToWinProduct = times.reduce((waysToWinProduct, time, i) => {
    const wayToWin = Array.from({ length: time })
      .map((_, i) => i + 1)
      .reduce((wayToWin, j) => (j * (time - j) > distances[i] ? ++wayToWin : wayToWin), 0);

    return Math.max(waysToWinProduct, 1) * Math.max(wayToWin, 1);
  }, 0);

  return waysToWinProduct;
};

const safelyParseNumbers = (input: string) => input.match(/[\d]+/g)!.map(Number);

export const solve = numberOfWaysToWin;
