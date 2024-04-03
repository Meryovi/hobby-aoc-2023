import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day9.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 9", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day9_1");
    const result = solve(input);
    assert.equal(result, 114);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day9_2");
    const result = solve(input);
    assert.equal(result, 2043183816);
  });
});
