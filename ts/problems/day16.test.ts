import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day16.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 16", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day16_1");
    const result = solve(input);
    assert.equal(result, 46);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day16_2");
    const result = solve(input);
    assert.equal(result, 7067);
  });
});
