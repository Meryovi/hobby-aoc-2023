const dayToRun = 1;

(async () => {
  console.log(`Running problem 'Day ${dayToRun}', answer:`);
  const { default: problem } = await import(`./problems/day${dayToRun}.js`);
  problem();
})();
