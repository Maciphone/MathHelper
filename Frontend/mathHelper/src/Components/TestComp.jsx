import React, { useCallback, useEffect, useRef, useState } from "react";
import LevelButtons from "./LevelButtons";

// Don't forget to
// // download the CSS file too OR
// // remove the following line if you're already using Tailwind
import "../styles.css";

function TestComp({ operation, translatedOperation }) {
  const [matek, setMath] = useState([]);
  const [userAnswer, setUserAnswer] = useState("");
  const [feedback, setFeedback] = useState("");
  const [level, setLevel] = useState("1");
  const inputRef = useRef(null);

  const fetchData = useCallback(async () => {
    try {
      const response = await fetch(
        `api/algebra/GetExercise?type=${operation}`,
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
      inputRef.current.focus();
    } catch (error) {
      console.error(error);
    }
  }, [level, operation]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (parseInt(userAnswer) === matek.result) {
      setFeedback("Bravo!");
      await fetchData();
      inputRef.current.focus();
    } else {
      setFeedback("Rossz válasz, próbáld újra!");
    }
    setUserAnswer("");
    inputRef.current.focus();
  };

  const skip = () => {
    fetchData();
  };

  const handleLevel = (e) => {
    //const level = e.target.value;
    setLevel(e);
  };
  return (
    <div id="webcrumbs">
      <div className="w-[600px] min-h-[500px] bg-neutral-50 rounded-lg shadow-lg p-6">
        <div className="flex justify-between items-start">
          <section className="w-full">
            <h2 className="font-title text-xl mb-4">Művelet</h2>
            <div className="bg-neutral-100 p-4 rounded-md mb-6">
              <LevelButtons operation={operation} handleLevel={handleLevel} />
            </div>
            <h3 className="text-lg mb-4">{translatedOperation}</h3>
            {matek ? (
              <div className="p-4">
                <p className="mb-4 text-md">Kérdés: {matek.question}</p>
                <form className="mb-4" onSubmit={handleSubmit}>
                  <label className="block mb-2">A válaszom:</label>
                  <input
                    ref={inputRef}
                    type="number"
                    value={userAnswer}
                    onChange={(e) => setUserAnswer(e.target.value)}
                    className="rounded-md p-2 border border-neutral-300 w-full mb-4"
                  />
                  <button
                    type="submit"
                    className="bg-primary text-white py-2 px-4 rounded-md text-center w-full hover:bg-primary-600"
                  >
                    Ennyi :)
                  </button>
                </form>
                <button
                  className="bg-neutral-200 py-2 px-4 rounded-md text-center w-full hover:bg-neutral-300 mb-4"
                  onClick={skip}
                >
                  Másikat
                </button>
                {feedback && <p className="italic">{feedback}</p>}
              </div>
            ) : (
              <p className="text-center mt-4">Loading...</p>
            )}
          </section>
        </div>
      </div>
    </div>
  );
}

export default TestComp;
