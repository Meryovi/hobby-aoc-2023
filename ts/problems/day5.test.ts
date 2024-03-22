import test, { describe } from "node:test";
import assert from "node:assert";

import { solve } from "./day5.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 5", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day5_1");
    const result = solve(input);
    assert.equal(result, 35);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day5_2");
    const result = solve(input);
    assert.equal(result, 486613012);
  });
});
