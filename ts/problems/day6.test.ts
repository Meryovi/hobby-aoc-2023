import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day6.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 6", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day6_1");
    const result = solve(input);
    assert.equal(result, 288);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day6_2");
    const result = solve(input);
    assert.equal(result, 138915);
  });
});
