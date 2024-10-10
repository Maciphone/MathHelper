import React, { useCallback, useEffect, useState } from "react";
import LevelButtons from "../Components/LevelButtons";

export default function Algbera() {
  const [matek, setMath] = useState([]);
  const [userAnswer, setUserAnswer] = useState("");
  const [feedback, setFeedback] = useState("");
  const [level, setLevel] = useState("1");

  const fetchData = useCallback(async () => {
    try {
      const response = await fetch(`api/algebra/GetAiExercise?type=algebra`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Level: `${level}`,
        },
      });
      if (!response.ok) {
        throw new Error("failed to fetch");
      }
      const data = await response.json();
      console.log(data);
      setMath(data);
    } catch (error) {
      console.error(error);
    }
  }, [level]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (parseInt(userAnswer) === matek.result) {
      setFeedback("Bravo!");
      await fetchData();
    } else {
      setFeedback("Rossz válasz, próbáld újra!");
    }
    setUserAnswer("");
  };

  const skip = () => {
    fetchData();
  };

  const handleLevel = (e) => {
    //  const level = e.target.value;
    setLevel(e);
  };
  const handleLevelLocal = (e) => {
    const level = e.target.value;
    setLevel(level);
  };

  return (
    <div>
      <div>
        <LevelButtons operation={"algebra"} handleLevel={handleLevel} />
        <p>szint</p>
        <button value="1" onClick={handleLevelLocal}>
          1
        </button>
        <button value="2" onClick={handleLevelLocal}>
          2
        </button>
        <button value="3" onClick={handleLevelLocal}>
          3
        </button>
      </div>
      <h2>Összeadás</h2>
      {matek ? (
        <div>
          <p>Kérdés: {matek.question}</p>
          <form onSubmit={handleSubmit}>
            <label>
              A válaszom:
              <input
                type="number"
                value={userAnswer}
                onChange={(e) => setUserAnswer(e.target.value)}
              />
            </label>
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
