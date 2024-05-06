import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day17.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 17", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day17_1");
    const result = solve(input);
    assert.equal(result, 102);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day17_2");
    const result = solve(input);
    assert.equal(result, 970);
  });
});
