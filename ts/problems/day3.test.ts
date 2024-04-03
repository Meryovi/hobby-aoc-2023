import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day3.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 3", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day3_1");
    const result = solve(input);
    assert.equal(result, 4361);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day3_2");
    const result = solve(input);
    assert.equal(result, 533784);
  });
});
