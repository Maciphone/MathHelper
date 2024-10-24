import React, { useCallback, useEffect, useState } from "react";
import LevelButtons from "./LevelButtons";

function ExercisePage({ operation, translatedOperation }) {
  const [matek, setMath] = useState([]);
  const [userAnswer, setUserAnswer] = useState("");
  const [userSecondAnswer, setUserSecondAnswer] = useState("");
  const [feedback, setFeedback] = useState("");
  const [level, setLevel] = useState("1");

  const fetchData = useCallback(async () => {
    try {
      const response = await fetch(
        `api/algebra/TestForDatabase?type=${operation}`,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Level: `${level}`,
          },
        }
      );
      if (!response.ok) {
        throw new Error("failed to fetch");
      }
      const data = await response.json();
      console.log(data);
      setMath(data);
    } catch (error) {
      console.error(error);
    }
  }, [level, operation]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (
      operation == "RemainDivision" &&
      parseInt(userAnswer) === matek.result[0] &&
      parseInt(userSecondAnswer) === matek.result[1]
    ) {
      setFeedback("Bravo!");
      await fetchData();
    } else if (parseInt(userAnswer) === matek.result[0]) {
      setFeedback("Bravo!");
      await fetchData();
    } else {
      setFeedback("Rossz válasz, próbáld újra!");
    }
    setUserAnswer("");
    setUserSecondAnswer("");
  };

  const skip = () => {
    fetchData();
  };

  const handleLevel = (e) => {
    //const level = e.target.value;
    setLevel(e);
  };

  return (
    <div>
      <div>
        <LevelButtons operation={operation} handleLevel={handleLevel} />
      </div>
      <h2>{translatedOperation}</h2>
      {matek ? (
        <div>
          <p>Kérdés: {matek.question}</p>
          <form onSubmit={handleSubmit}>
            <label>
              {operation == "RemainDivision" ? "maradék" : "A válaszom:"}
              <input
                type="number"
                value={userAnswer}
                onChange={(e) => setUserAnswer(e.target.value)}
              />
            </label>

            {operation === "RemainDivision" && (
              <label>
                Osztó:
                <input
                  type="number"
                  value={userSecondAnswer}
                  onChange={(e) => setUserSecondAnswer(e.target.value)}
                />
              </label>
            )}
            <button type="submit">ennyi :)</button>
          </form>
          <button onClick={skip}>másikat</button>
          {feedback && <p>{feedback}</p>}
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}

export default ExercisePage;
