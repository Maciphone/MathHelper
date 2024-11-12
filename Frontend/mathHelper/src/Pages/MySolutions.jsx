import React, { useCallback, useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

export default function MySolutions() {
  const [solutions, setSolutions] = useState([]);
  const [isBuilt, setIsBuilt] = useState(false);
  const [Loading, setLoading] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);
  const userReduxName = useSelector((state) => state.userData.value);
  const navigator = useNavigate();
  console.log(isBuilt);

  useEffect(() => {
    const fetchRole = async () => {
      try {
        const response = await fetch(`api/solutions/GetRole`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
          credentials: "include",
        });
        if (response.ok) {
          const data = await response.json();
          setIsAdmin(true);
          console.log("Admin");
        } else {
          console.log("nem admin");
        }
      } catch (error) {
        console.error(error);
      }
    };
    fetchRole();
  }, []);

  useEffect(() => {
    if (userReduxName == null) {
      navigator("/login");
    }
  }, [userReduxName, navigator]);

  const fetchSolutions = useCallback(async () => {
    console.log("fetchSolutions fut", isBuilt);
    try {
      const response = await fetch(`/api/solution/userSolutions`);
      const data = await response.json();
      setSolutions(data);
      setIsBuilt(true);
      console.log(data);
    } catch (error) {
      console.error(error);
    }
  }, [isBuilt]);

  useEffect(() => {
    setLoading(true);

    if (!isBuilt && userReduxName != null) {
      fetchSolutions();
    }
  }, [isBuilt, userReduxName, fetchSolutions]);

  const handleDelete = async (solutionId) => {
    console.log(solutionId);
    try {
      const response = await fetch(`api/solution/DeleteSolution${solutionId}`, {
        method: "DELETE",
        credentials: "include",
      });
      if (!response.ok) {
        alert("törlés nem sikerült");
      } else {
        alert("törlés sikerült");
        fetchSolutions();
      }
    } catch (error) {
      console.error(error);
    }
  };

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
              <button onClick={() => handleDelete(solution.solutionId)}>
                <a
                  href="https://www.flaticon.com/free-icons/recycle-bin"
                  title="recycle bin icons"
                ></a>
              </button>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
