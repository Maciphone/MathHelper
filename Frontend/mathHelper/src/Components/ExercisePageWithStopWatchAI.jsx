import React, { useCallback, useEffect, useRef, useState } from "react";
import LevelButtons from "./LevelButtons";
import Stopwatch from "./StopWatch";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

export default function ExercisePageWithStopWatchAI({
  operation,
  translatedOperation,
  ai = false,
}) {
  const [matek, setMath] = useState([]);
  const [userAnswer, setUserAnswer] = useState("");
  const [userSecondAnswer, setUserSecondAnswer] = useState("");
  const [feedback, setFeedback] = useState("");
  const [level, setLevel] = useState("");
  const [isLevelSetted, setIsLeveleSetted] = useState(false);
  const navigator = useNavigate();

  const [calculate, setCalculate] = useState("");
  const [date, setDate] = useState(new Date().getDate());

  const userReduxName = useSelector((state) => state.userData.value);
  const inputRef = useRef(null); // hivatkozási pont létrehozása

  //stopwatch
  const [isRunning, setIsRunning] = useState(false);
  const [reset, setReset] = useState(false);
  const [elapsedTime, setElapsedTime] = useState(null);
  const [isTimeRequested, setIsTimeRequested] = useState(false);
  const [isBuilt, setIsBuilt] = useState(false);
  const fetchUrl = ai == false ? "TestForDatabase" : "GetAiExercise";
  console.log("fetchurl", fetchUrl);

  useEffect(() => {
    if (userReduxName == null) {
      navigator("/login");
    }
  }, [userReduxName, navigator]);

  const fetchData = useCallback(async () => {
    try {
      const response = await fetch(
        `api/algebra/${fetchUrl}?type=${operation}`,
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
        throw new Error("failed to fetch");
      }
      const data = await response.json();
      console.log(data);
      setMath(data);
      setIsRunning(true);
      setLevel("");
      setIsLeveleSetted(false);

      //  inputRef.current.focus();
    } catch (error) {
      console.error(error);
    }
  }, [level, operation, fetchUrl]);

  useEffect(() => {
    if (level) {
      setCalculate(date * level);
    }
  }, [level, date]);

  useEffect(() => {
    inputRef.current, focus;
  }, [matek]);

  useEffect(() => {
    if (isBuilt && isLevelSetted) {
      fetchData();
    }
  }, [isBuilt, fetchData, fetchUrl, level, operation, isLevelSetted]);

  const updateSolution = async () => {
    var solvedAt = new Date().toISOString();
    var exerciseId = matek.exerciseId;

    var solution = {
      exerciseId,
      elapsedTime,
      solvedAt: solvedAt,
    };
    console.log(solution);

    try {
      var response = await fetch("api/solution/UpdateSolution", {
        method: "Post",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(solution),
      });
      if (!response.ok) {
        throw new Error(response.statusText);
      }
    } catch (error) {
      console.error("Failed to update solution:", error.message);
    }
  };
  useEffect(() => {
    setMath("");
  }, [operation]);

  useEffect(() => {
    setReset((prevReset) => !prevReset);
  }, [matek]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (
      (operation == "RemainDivision" &&
        parseInt(userAnswer) === matek.result[0] / calculate &&
        parseInt(userSecondAnswer) === matek.result[1]) / calculate ||
      parseInt(userAnswer) === matek.result[0] / calculate
    ) {
      setFeedback("Bravo!");
      setIsTimeRequested(true); //triger stopwatch
      setUserAnswer("");
      setUserSecondAnswer("");
      inputRef.current.focus();
    } else {
      setFeedback("Rossz válasz, próbáld újra!");
    }
    setUserAnswer("");
    inputRef.current.focus();
  };

  useEffect(() => {
    const update = async () => {
      await updateSolution();
      await fetchData();
    };
    if (isBuilt) {
      update();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [elapsedTime]);

  const skip = async () => {
    await fetchData();
  };

  //set time required to solve the exercise, passed to stopwatch comp.
  const handleElapsedTime = (time) => {
    setElapsedTime(time);
    setIsTimeRequested(false);
    console.log(`Eltelt idő: ${time} ms`);
  };

  const handleLevel = (e) => {
    setIsLeveleSetted(true);
    setLevel(e);
    setIsRunning(true);
    setIsBuilt(true);
  };

  if (userReduxName == null) {
    return (
      <div>
        <button onClick={() => navigator("/login")}>login</button>
      </div>
    );
  }

  return (
    <div>
      <div className="text-container">
        {userReduxName && <p>Szia {userReduxName}</p>}
      </div>

      <div>
        <LevelButtons operation={operation} handleLevel={handleLevel} />
      </div>

      {matek ? (
        <div>
          <div className="text-container">
            <p>Kérdés: {matek.question}</p>
          </div>
          <div className="input-button-container">
            <form onSubmit={handleSubmit}>
              <label>
                {operation == "RemainDivision" ? "maradék" : "A válaszom:"}
                <input
                  ref={inputRef}
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

            <div className="text-container" onClick={() => setReset(!reset)}>
              <Stopwatch
                isRunning={isRunning}
                onReset={reset}
                handleElapsedTime={handleElapsedTime}
                isTimeRequested={isTimeRequested}
              />
            </div>
          </div>

          {feedback && <p>{feedback}</p>}
        </div>
      ) : !level && !matek ? (
        <div>válassz szintet</div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}

// const handleSubmit = async (e) => {
//   e.preventDefault();

//   if (parseInt(userAnswer) === matek.result[0]) {
//     setFeedback("Bravo!");
//     elapsedTimeSetting();
//     setIsSubmitPending(true);
//     if (elapsedTime) {
//       console.log("elapsedtime", elapsedTime);
//       await updateSolution();
//       await fetchData();
//       inputRef.current.focus();
//     }
//   } else {
//     setFeedback("Rossz válasz, próbáld újra!");
//   }
//   setUserAnswer("");
//   inputRef.current.focus();
// };

// useEffect(() => {
//   const fetcher = async () => {
//     try {
//       const response = await fetch(
//         `api/algebra/${fetchUrl}?type=${operation}`,
//         {
//           method: "GET",
//           headers: {
//             "Content-Type": "application/json",
//             Level: `${level}`,
//           },
//           credentials: "include",
//         }
//       );
//       if (!response.ok) {
//         const errorData = await response.json();
//         console.error("Error from backend:", errorData);
//         throw new Error(errorData.message || "Failed to fetch");
//       }
//       const data = await response.json();
//       console.log(data);

//       setMath(data);
//       setIsRunning(true);
//       inputRef.current.focus();
//       setLevel("");
//     } catch (error) {
//       console.error(error);
//     }
//   };
//   if (isBuilt) {
//     fetchData();
//   }
// }, [isBuilt, fetchData, fetchUrl, level, operation]);
