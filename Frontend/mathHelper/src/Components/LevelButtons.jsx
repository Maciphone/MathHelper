import React, { useState, useEffect, memo } from "react";

function LevelButtons({ operation, handleLevel }) {
  const [levels, setLevels] = useState(0);

  useEffect(() => {
    console.log(operation);
    fetch(`/api/level/${operation}`)
      .then((response) => response.json())
      .then((data) => setLevels(data));
  }, [operation]);

  // Eseménykezelő, ami feldolgozza a gomb kattintásakor átadott értéket
  const handleButtonClick = (level) => {
    console.log(`Selected level: ${level}`);
    handleLevel(level);
  };

  const renderButtons = () => {
    let buttons = [];
    for (let i = 1; i <= levels; i++) {
      buttons.push(
        <button key={i} onClick={() => handleButtonClick(i)}>
          {i}
        </button>
      );
    }
    return buttons;
  };

  return (
    <div>
      <h6>{operation} Levels</h6>
      {renderButtons()}
    </div>
  );
}

export default memo(LevelButtons);
