import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day23.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 23", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day23_1");
    const result = solve(input);
    assert.equal(result, 94);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day23_2");
    const result = solve(input);
    assert.equal(result, 2362);
  });
});
