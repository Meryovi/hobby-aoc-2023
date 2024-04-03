import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day2.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 2", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day2_1");
    const result = solve(input);
    assert.equal(result, 8);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day2_2");
    const result = solve(input);
    assert.equal(result, 2331);
  });
});
