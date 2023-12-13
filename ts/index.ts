(async () => {
  const problem = await parseProblem();
  console.log(`Running problem '${problem.name}', answer:`);
  problem.solve();
})();

async function parseProblem(): Promise<IProblem> {
  const dayToRun = process.argv.length > 2 ? Number(process.argv[2]) : 1;
  try {
    const { default: problem } = await import(`./problems/day${dayToRun}.js`);
    if (!problem.name || typeof problem.solve != "function") throw new Error("Invalid problem file");
    return problem;
  } catch {
    throw new Error("Could not parse problem number, provide --prob arg with valid value");
  }
}

export default interface IProblem {
  name: string;
  solve: () => void;
}
