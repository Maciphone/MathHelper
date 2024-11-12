import React, { useEffect, useState } from "react";
import "./TaskSlider.css";

const TaskSlider = ({ handleOperation }) => {
  const [activeIndex, setActiveIndex] = useState(0);
  const tasks = ["Összeadás", "Osztás", "Szorzás", "MaradékOsztás"];

  const nextSlide = () => {
    setActiveIndex((prevIndex) => (prevIndex + 1) % tasks.length);
  };

  const prevSlide = () => {
    setActiveIndex(
      (prevIndex) => (prevIndex - 1 + tasks.length) % tasks.length
    );
  };
  useEffect(() => {
    handleOperation(tasks[activeIndex]);
  }, [activeIndex, handleOperation, tasks]);

  return (
    <div className="slider-container">
      <button onClick={prevSlide} className="slider-button">
        ◀
      </button>
      <div className="slider-content">
        <div className="task-card">{tasks[activeIndex]}</div>
      </div>
      <button onClick={nextSlide} className="slider-button">
        ▶
      </button>
    </div>
  );
};

export default TaskSlider;
