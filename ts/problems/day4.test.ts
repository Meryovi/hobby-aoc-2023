import test, { describe } from "node:test";
import assert from "node:assert";

import { solve } from "./day4.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 4", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day4_1");
    const result = solve(input);
    assert.equal(result, 13);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day4_2");
    const result = solve(input);
    assert.equal(result, 17803);
  });
});
