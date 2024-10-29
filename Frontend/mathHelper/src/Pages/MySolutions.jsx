import React, { useEffect, useState } from "react";

export default function MySolutions() {
  const [solutions, setSolutions] = useState([]);
  const [isBuilt, setIsBuilt] = useState(false);
  const [Loading, setLoading] = useState(false);
  console.log(isBuilt);

  useEffect(() => {
    setLoading(true);
    const fetchSolutions = async () => {
      console.log("fecsben", isBuilt);
      try {
        const response = await fetch(`/api/solution/userSolutions`);
        const data = await response.json();
        setSolutions(data);
        setIsBuilt(true);
        console.log(data); // Csak ha valóban logolni akarod
      } catch (error) {
        console.error(error);
      }
    };
    if (!isBuilt) {
      fetchSolutions();
    }
  }, [isBuilt]);

  if (setLoading & (solutions == null)) {
    return <div>...loading</div>;
  }

  return (
    <div>
      <h1>Megoldásaim</h1>
      <table>
        <thead>
          <tr>
            <th>Matematika Típus</th>
            <th>Szint</th>
            <th>Kezdési Dátum</th>
            <th>Megoldási Idő (másodperc)</th>
            <th>Feladat</th>
            <th>Eredmény</th>
          </tr>
        </thead>
        <tbody>
          {solutions.map((solution) => (
            <tr key={solution.solutionId}>
              <td>{solution.mathTypeName}</td>
              <td>{solution.level}</td>
              <td>{new Date(solution.createdAt).toLocaleString()}</td>
              <td>{solution.elapsedTime / 100} s</td>
              <td>{solution.question}</td>
              <td>{solution.resultValue.join(", ")}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
