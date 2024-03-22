import { NEW_LINE } from "../inputReader.js";

const calculateScratchCardPoints = (input: string) =>
  input
    .split(NEW_LINE)
    .map(calculateGamePoints)
    .reduce((sum, val) => sum + val, 0);

const calculateGamePoints = (game: string) => {
  const gameDraw = game.substring(game.indexOf(":") + 1);
  const [winnerCards, drawnCards] = gameDraw.split("|");

  return drawnCards
    .match(/.{3}/g)! // Split every 3 chars.
    .reduce((gamePoints, draw) => (winnerCards.includes(draw) ? Math.max(1, gamePoints * 2) : gamePoints), 0);
};

export const solve = calculateScratchCardPoints;
