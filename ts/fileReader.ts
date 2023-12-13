import fs from "fs";

export const readProblemInput = (id: string) => fs.readFileSync(`../input/${id}.txt`).toString();
