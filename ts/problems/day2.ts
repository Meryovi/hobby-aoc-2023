import { NEW_LINE } from "../inputReader.js";

const numberOfPossibleGames = (input: string) =>
  input
    .split(NEW_LINE)
    .map(getGameValueIfValid)
    .reduce((sum, val) => sum + val, 0);

const getGameValueIfValid = (game: string, gameIndex: number) => {
  const draws = game.substring(game.indexOf(":") + 1).split(/[,|;]/);
  const isValidGame = draws.every((draw) => isValidDraw(draw.substring(1).split(" ")));
  return isValidGame ? gameIndex + 1 : 0;
};

const isValidDraw = ([count, cube]: string[]) =>
  (cube === "red" && Number(count) <= 12) ||
  (cube === "green" && Number(count) <= 13) ||
  (cube === "blue" && Number(count) <= 14);

export const solve = numberOfPossibleGames;
