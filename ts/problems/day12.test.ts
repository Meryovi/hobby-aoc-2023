import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day12.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 12", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day12_1");
    const result = solve(input);
    assert.equal(result, 21);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day12_2");
    const result = solve(input);
    assert.equal(result, 7017);
  });
});
