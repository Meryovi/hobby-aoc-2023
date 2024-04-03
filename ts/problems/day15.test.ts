import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day15.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 15", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day15_1");
    const result = solve(input);
    assert.equal(result, 1320);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day15_2");
    const result = solve(input);
    assert.equal(result, 505379);
  });
});
