import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day14.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 14", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day14_1");
    const result = solve(input);
    assert.equal(result, 136);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day14_2");
    const result = solve(input);
    assert.equal(result, 107142);
  });
});
