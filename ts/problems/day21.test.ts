import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day21.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 21", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day21_1");
    const result = solve(input);
    assert.equal(result, 29);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day21_2");
    const result = solve(input);
    assert.equal(result, 3776);
  });
});
