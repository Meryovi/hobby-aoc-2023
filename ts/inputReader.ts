import fs from "fs";

export const NEW_LINE = "\r\n";

export const readProblemInput = (id: string) => fs.readFileSync(`../input/${id}.txt`).toString();
