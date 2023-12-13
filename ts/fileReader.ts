import fs from "fs";

export const readDayInput = (day: number) => fs.readFileSync(`../input/day${day}.txt`).toString();
