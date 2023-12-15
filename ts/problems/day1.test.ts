import test, { describe } from "node:test";
import assert from "node:assert";

import { solve } from "./day1.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 1", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day1_1");
    const result = solve(input);
    assert.equal(result, 142);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day1_2");
    const result = solve(input);
    assert.equal(result, 55017);
  });
});
