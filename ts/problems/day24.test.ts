import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day24.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 24", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day24_1");
    const result = solve(input);
    assert.equal(result, 2);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day24_2");
    const result = solve(input);
    assert.equal(result, 20336);
  });
});
