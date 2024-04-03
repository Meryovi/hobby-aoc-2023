import { NEW_LINE } from "../inputReader.js";

const calculateTotalCardWinnings = (input: string) =>
  input
    .split(NEW_LINE)
    .map(parseCardDraw)
    .sort(sortByDrawValue)
    .reduce((sum, _, i, draws) => sum + draws[i].bid * (i + 1), 0);

const parseCardDraw = (cardDrawString: string): CardDraw => {
  const [draw, bidValue] = cardDrawString.split(" ");
  const drawValue = calculateDrawValue(draw);
  return { draw, drawValue, bid: Number(bidValue) };
};

const calculateDrawValue = (drawnCards: string) => {
  let maxCount = 1;
  let currentCount = 1;
  let typeCount = 0;
  let drawValue = 0;

  [...drawnCards].sort().forEach((_, i, cards) => {
    currentCount = i > 0 && cards[i] === cards[i - 1] ? currentCount + 1 : 1;
    typeCount = currentCount === 1 ? typeCount + 1 : typeCount;

    maxCount = Math.max(maxCount, currentCount);
    drawValue = drawValue * 100 + (CARD_TYPES.indexOf(drawnCards[i]) + 10);
  });

  // prettier-ignore
  const drawType =
    (typeCount === 1) ? 7 :
    (typeCount === 2 && maxCount === 4) ? 6 :
    (typeCount === 2) ? 5 :
    (typeCount === 3 && maxCount === 3) ? 4 :
    (typeCount === 3) ? 3 :
    (typeCount === 4) ? 2 : 1;

  return drawType * 10_000_000_000 + drawValue;
};

const sortByDrawValue = (a: CardDraw, b: CardDraw) => a.drawValue - b.drawValue;

const CARD_TYPES = "23456789TJQKA";

type CardDraw = {
  draw: string;
  drawValue: number;
  bid: number;
};

export default calculateTotalCardWinnings;
