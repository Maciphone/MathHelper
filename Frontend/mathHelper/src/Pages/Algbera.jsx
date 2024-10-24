import React, { useCallback, useEffect, useRef, useState } from "react";
import LevelButtons from "../Components/LevelButtons";
import Stopwatch from "../Components/StopWatch";
import { useSelector } from "react-redux";

export default function Algbera() {
  const [matek, setMath] = useState([]);
  const [userAnswer, setUserAnswer] = useState("");
  const [feedback, setFeedback] = useState("");
  const [level, setLevel] = useState("1");
  const userReduxName = useSelector((state) => state.userData.value);
  const inputRef = useRef(null); // hivatkozási pont létrehozása

  // if (userReduxName) {
  //   console.log(userReduxName);
  // } else {
  //   console.log("not loged in");
  // }

  //stopwatch
  const [isRunning, setIsRunning] = useState(false);
  const [reset, setReset] = useState(false);
  const [elapsedTime, setElapsedTime] = useState([]);
  const [isTimeRequested, setIsTimeRequested] = useState(false);

  useEffect(() => {
    const fetcher = async () => {
      try {
        const response = await fetch(
          `api/algebra/TestForDatabase?type=Algebra`,
          {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
              Level: `${level}`,
            },
            credentials: "include",
          }
        );
        if (!response.ok) {
          const errorData = await response.json();
          console.error("Error from backend:", errorData);
          throw new Error(errorData.message || "Failed to fetch");
        }
        const data = await response.json();
        console.log(data);
        setMath(data);
        setIsRunning(true);
        inputRef.current.focus();
      } catch (error) {
        console.error(error);
      }
    };
    fetcher();
  }, [level]);

  const fetchData = async () => {
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
      setIsRunning(true);
      inputRef.current.focus();
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    setReset((prevReset) => !prevReset);
  }, [matek]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (parseInt(userAnswer) === matek.result[0]) {
      setFeedback("Bravo!");
      elapsedTimeSetting();
      await fetchData();
      inputRef.current.focus();
    } else {
      setFeedback("Rossz válasz, próbáld újra!");
    }
    setUserAnswer("");
    inputRef.current.focus();
  };

  const elapsedTimeSetting = () => {
    setIsRunning(false);
    setIsTimeRequested(true);
    setReset(!reset);
  };

  const skip = () => {
    fetchData();
  };

  //set time required to solve the exercise,
  const handleElapsedTime = (time) => {
    setElapsedTime(time);
    setIsTimeRequested(false);
    console.log(`Eltelt idő: ${time} ms`);
  };

  const handleLevel = (e) => {
    setLevel(e);
    setIsRunning(true);
  };
  const handleLevelLocal = (e) => {
    const level = e.target.value;
    setLevel(level);
  };

  return (
    <div>
      <div>{userReduxName && <p1>Szia {userReduxName}</p1>}</div>

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
                ref={inputRef}
                type="number"
                value={userAnswer}
                onChange={(e) => setUserAnswer(e.target.value)}
              />
            </label>
            <button type="submit">ennyi :)</button>
          </form>
          <button onClick={skip}>másikat</button>

          <div onClick={() => setReset(!reset)}>
            <Stopwatch
              isRunning={isRunning}
              onReset={reset}
              handleElapsedTime={handleElapsedTime}
              isTimeRequested={isTimeRequested}
            />
          </div>

          {feedback && <p>{feedback}</p>}
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}
