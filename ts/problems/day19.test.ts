import test, { describe } from "node:test";
import assert from "node:assert";

import solve from "./day19.js";
import { readProblemInput } from "../inputReader.js";

describe("Day 19", () => {
  test("test set should yield expected result", () => {
    const input = readProblemInput("day19_1");
    const result = solve(input);
    assert.equal(result, 19114);
  });

  test("full set should yield expected result", () => {
    const input = readProblemInput("day19_2");
    const result = solve(input);
    assert.equal(result, 353046);
  });
});
