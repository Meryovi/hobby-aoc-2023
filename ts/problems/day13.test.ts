import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day13.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 13", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day13_1");
    const result = solve(input);
    assert.equal(result, 405);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day13_2");
    const result = solve(input);
    assert.equal(result, 29213);
  });
});
