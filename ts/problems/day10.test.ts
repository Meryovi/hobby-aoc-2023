import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day10.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 10", () => {
  test("test set 1 should yield expected result", () => {
    const input = readProblemInput("day10_1");
    const result = solve(input);
    assert.equal(result, 4);
  });

  test("test set 2 should yield expected result", () => {
    const input = readProblemInput("day10_2");
    const result = solve(input);
    assert.equal(result, 8);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day10_3");
    const result = solve(input);
    assert.equal(result, 6867);
  });
});
